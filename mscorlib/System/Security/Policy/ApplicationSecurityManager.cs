// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationSecurityManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Deployment.Internal.Isolation.Manifest;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Управляет решениями о доверии для манифеста приложения.
  /// </summary>
  [ComVisible(true)]
  public static class ApplicationSecurityManager
  {
    private static volatile IApplicationTrustManager m_appTrustManager = (IApplicationTrustManager) null;
    private static string s_machineConfigFile = Config.MachineDirectory + "applicationtrust.config";

    [SecuritySafeCritical]
    static ApplicationSecurityManager()
    {
    }

    /// <summary>
    ///   Определяет, утверждает ли пользователь указанное приложение для выполнения с запрошенным набором разрешений.
    /// </summary>
    /// <param name="activationContext">
    ///   <see cref="T:System.ActivationContext" /> Определение контекст активации для приложения.
    /// </param>
    /// <param name="context">
    ///   A <see cref="T:System.Security.Policy.TrustManagerContext" />  Определение контекст диспетчер доверия для приложения.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> для выполнения указанного приложения; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="activationContext" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
    public static bool DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context)
    {
      if (activationContext == null)
        throw new ArgumentNullException(nameof (activationContext));
      AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
      if (domainManager != null)
      {
        HostSecurityManager hostSecurityManager = domainManager.HostSecurityManager;
        if (hostSecurityManager != null && (hostSecurityManager.Flags & HostSecurityManagerOptions.HostDetermineApplicationTrust) == HostSecurityManagerOptions.HostDetermineApplicationTrust)
        {
          ApplicationTrust applicationTrust = hostSecurityManager.DetermineApplicationTrust(CmsUtils.MergeApplicationEvidence((Evidence) null, activationContext.Identity, activationContext, (string[]) null), (Evidence) null, context);
          if (applicationTrust == null)
            return false;
          return applicationTrust.IsApplicationTrustedToRun;
        }
      }
      ApplicationTrust applicationTrustInternal = ApplicationSecurityManager.DetermineApplicationTrustInternal(activationContext, context);
      if (applicationTrustInternal == null)
        return false;
      return applicationTrustInternal.IsApplicationTrustedToRun;
    }

    /// <summary>
    ///   Получает коллекцию доверия приложения, которая содержит кэшированные решения о доверии для пользователя.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> Содержащая кэшированные решения о доверии для пользователя.
    /// </returns>
    public static ApplicationTrustCollection UserApplicationTrusts
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)] get
      {
        return new ApplicationTrustCollection(true);
      }
    }

    /// <summary>Получает текущий диспетчер доверия приложения.</summary>
    /// <returns>
    ///   <see cref="T:System.Security.Policy.IApplicationTrustManager" /> Представляющий текущий диспетчер доверия.
    /// </returns>
    /// <exception cref="T:System.Security.Policy.PolicyException">
    ///   Политика на этом приложении не имеет диспетчера доверия.
    /// </exception>
    public static IApplicationTrustManager ApplicationTrustManager
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)] get
      {
        if (ApplicationSecurityManager.m_appTrustManager == null)
        {
          ApplicationSecurityManager.m_appTrustManager = ApplicationSecurityManager.DecodeAppTrustManager();
          if (ApplicationSecurityManager.m_appTrustManager == null)
            throw new PolicyException(Environment.GetResourceString("Policy_NoTrustManager"));
        }
        return ApplicationSecurityManager.m_appTrustManager;
      }
    }

    [SecurityCritical]
    internal static ApplicationTrust DetermineApplicationTrustInternal(ActivationContext activationContext, TrustManagerContext context)
    {
      ApplicationTrustCollection applicationTrustCollection = new ApplicationTrustCollection(true);
      if (context == null || !context.IgnorePersistedDecision)
      {
        ApplicationTrust applicationTrust = applicationTrustCollection[activationContext.Identity.FullName];
        if (applicationTrust != null)
          return applicationTrust;
      }
      ApplicationTrust trust = ApplicationSecurityManager.ApplicationTrustManager.DetermineApplicationTrust(activationContext, context) ?? new ApplicationTrust(activationContext.Identity);
      trust.ApplicationIdentity = activationContext.Identity;
      if (trust.Persist)
        applicationTrustCollection.Add(trust);
      return trust;
    }

    [SecurityCritical]
    private static IApplicationTrustManager DecodeAppTrustManager()
    {
      if (File.InternalExists(ApplicationSecurityManager.s_machineConfigFile))
      {
        string end;
        using (FileStream fileStream = new FileStream(ApplicationSecurityManager.s_machineConfigFile, FileMode.Open, FileAccess.Read))
          end = new StreamReader((Stream) fileStream).ReadToEnd();
        SecurityElement securityElement1 = SecurityElement.FromString(end).SearchForChildByTag("mscorlib");
        if (securityElement1 != null)
        {
          SecurityElement securityElement2 = securityElement1.SearchForChildByTag("security");
          if (securityElement2 != null)
          {
            SecurityElement securityElement3 = securityElement2.SearchForChildByTag("policy");
            if (securityElement3 != null)
            {
              SecurityElement securityElement4 = securityElement3.SearchForChildByTag(nameof (ApplicationSecurityManager));
              if (securityElement4 != null)
              {
                SecurityElement elTrustManager = securityElement4.SearchForChildByTag("IApplicationTrustManager");
                if (elTrustManager != null)
                {
                  IApplicationTrustManager applicationTrustManager = ApplicationSecurityManager.DecodeAppTrustManagerFromElement(elTrustManager);
                  if (applicationTrustManager != null)
                    return applicationTrustManager;
                }
              }
            }
          }
        }
      }
      return ApplicationSecurityManager.DecodeAppTrustManagerFromElement(ApplicationSecurityManager.CreateDefaultApplicationTrustManagerElement());
    }

    [SecurityCritical]
    private static SecurityElement CreateDefaultApplicationTrustManagerElement()
    {
      SecurityElement securityElement = new SecurityElement("IApplicationTrustManager");
      securityElement.AddAttribute("class", "System.Security.Policy.TrustManager, System.Windows.Forms, Version=" + (object) ((RuntimeAssembly) Assembly.GetExecutingAssembly()).GetVersion() + ", Culture=neutral, PublicKeyToken=b77a5c561934e089");
      securityElement.AddAttribute("version", "1");
      return securityElement;
    }

    [SecurityCritical]
    private static IApplicationTrustManager DecodeAppTrustManagerFromElement(SecurityElement elTrustManager)
    {
      new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
      Type type = Type.GetType(elTrustManager.Attribute("class"), false, false);
      if (type == (Type) null)
        return (IApplicationTrustManager) null;
      IApplicationTrustManager instance = Activator.CreateInstance(type) as IApplicationTrustManager;
      instance?.FromXml(elTrustManager);
      return instance;
    }
  }
}
