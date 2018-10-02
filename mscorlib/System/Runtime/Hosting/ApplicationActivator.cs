// Decompiled with JetBrains decompiler
// Type: System.Runtime.Hosting.ApplicationActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
  /// <summary>
  ///   Предоставляет базовый класс для активации сборок на основе манифеста.
  /// </summary>
  [ComVisible(true)]
  public class ApplicationActivator
  {
    /// <summary>
    ///   Создает экземпляр приложения, которое необходимо активировать, используя указанный контекст активации.
    /// </summary>
    /// <param name="activationContext">
    ///   <see cref="T:System.ActivationContext" /> Определяющий приложение для активации.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.ObjectHandle" /> Это программа-оболочка для возвращаемого значения выполнения приложения.
    ///    Возвращаемое значение должно быть без оболочки, получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="activationContext" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual ObjectHandle CreateInstance(ActivationContext activationContext)
    {
      return this.CreateInstance(activationContext, (string[]) null);
    }

    /// <summary>
    ///   Создает экземпляр приложения, которое необходимо активировать, используя заданный контекст активации и пользовательские данные активации.
    /// </summary>
    /// <param name="activationContext">
    ///   <see cref="T:System.ActivationContext" /> Определяющий приложение для активации.
    /// </param>
    /// <param name="activationCustomData">
    ///   Пользовательские данные активации.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.ObjectHandle" /> Это программа-оболочка для возвращаемого значения выполнения приложения.
    ///    Возвращаемое значение должно быть без оболочки, получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="activationContext" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public virtual ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData)
    {
      if (activationContext == null)
        throw new ArgumentNullException(nameof (activationContext));
      if (CmsUtils.CompareIdentities(AppDomain.CurrentDomain.ActivationContext, activationContext))
        return new ObjectHandle((object) new ManifestRunner(AppDomain.CurrentDomain, activationContext).ExecuteAsAssembly());
      AppDomainSetup adSetup = new AppDomainSetup(new ActivationArguments(activationContext, activationCustomData));
      AppDomainSetup setupInformation = AppDomain.CurrentDomain.SetupInformation;
      adSetup.AppDomainManagerType = setupInformation.AppDomainManagerType;
      adSetup.AppDomainManagerAssembly = setupInformation.AppDomainManagerAssembly;
      return ApplicationActivator.CreateInstanceHelper(adSetup);
    }

    /// <summary>
    ///   Создает экземпляр приложения с использованием заданного <see cref="T:System.AppDomainSetup" />  объекта.
    /// </summary>
    /// <param name="adSetup">
    ///   <see cref="T:System.AppDomainSetup" /> Которого <see cref="P:System.AppDomainSetup.ActivationArguments" /> свойство идентифицирует приложение, чтобы активировать.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.ObjectHandle" /> Это программа-оболочка для возвращаемого значения выполнения приложения.
    ///    Возвращаемое значение должно быть без оболочки, получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.AppDomainSetup.ActivationArguments" /> Свойство <paramref name="adSetup " />— <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">
    ///   Не удалось выполнить, поскольку настройки политики в текущем домене приложения не разрешают выполнение приложения экземпляра приложения.
    /// </exception>
    [SecuritySafeCritical]
    protected static ObjectHandle CreateInstanceHelper(AppDomainSetup adSetup)
    {
      if (adSetup.ActivationArguments == null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MissingActivationArguments"));
      adSetup.ActivationArguments.ActivateInstance = true;
      Evidence evidence1 = AppDomain.CurrentDomain.Evidence;
      Evidence evidence2 = CmsUtils.MergeApplicationEvidence((Evidence) null, adSetup.ActivationArguments.ApplicationIdentity, adSetup.ActivationArguments.ActivationContext, adSetup.ActivationArguments.ActivationData);
      ApplicationTrust applicationTrust = AppDomain.CurrentDomain.HostSecurityManager.DetermineApplicationTrust(evidence2, evidence1, new TrustManagerContext());
      if (applicationTrust == null || !applicationTrust.IsApplicationTrustedToRun)
        throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"), -2146233320, (Exception) null);
      ObjRef instance = AppDomain.nCreateInstance(adSetup.ActivationArguments.ApplicationIdentity.FullName, adSetup, evidence2, evidence2 == null ? AppDomain.CurrentDomain.InternalEvidence : (Evidence) null, AppDomain.CurrentDomain.GetSecurityDescriptor());
      if (instance == null)
        return (ObjectHandle) null;
      return RemotingServices.Unmarshal(instance) as ObjectHandle;
    }
  }
}
