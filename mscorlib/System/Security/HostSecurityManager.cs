// Decompiled with JetBrains decompiler
// Type: System.Security.HostSecurityManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Deployment.Internal.Isolation.Manifest;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Security
{
  /// <summary>
  ///   Позволяет настраивать работу системы безопасности для доменов приложений и управления.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class HostSecurityManager
  {
    /// <summary>
    ///   Возвращает флаги, представляющие компоненты политики безопасности узла.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, указывающее компоненты политики безопасности.
    ///    Значение по умолчанию — <see cref="F:System.Security.HostSecurityManagerOptions.AllFlags" />.
    /// </returns>
    public virtual HostSecurityManagerOptions Flags
    {
      get
      {
        return HostSecurityManagerOptions.AllFlags;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает политику безопасности для текущего домена приложения.
    /// </summary>
    /// <returns>
    ///   Политика безопасности для текущего домена приложения.
    ///    Значение по умолчанию — <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод использует политику разграничения доступа кода (CAS), которая является устаревшей для .NET Framework 4.
    ///    Чтобы включить политику CAS для обеспечения совместимости с предыдущими версиями .NET Framework, используйте элемент &lt;legacyCasPolicy&gt;.
    /// </exception>
    [Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public virtual PolicyLevel DomainPolicy
    {
      get
      {
        if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
        return (PolicyLevel) null;
      }
    }

    /// <summary>
    ///   Предоставляет свидетельство домена приложения для загружаемой сборки.
    /// </summary>
    /// <param name="inputEvidence">
    ///   Добавление дополнительного свидетельства <see cref="T:System.AppDomain" /> свидетельства.
    /// </param>
    /// <returns>
    ///   Свидетельство, используемое для <see cref="T:System.AppDomain" />.
    /// </returns>
    public virtual Evidence ProvideAppDomainEvidence(Evidence inputEvidence)
    {
      return inputEvidence;
    }

    /// <summary>
    ///   Предоставляет свидетельство сборки для загружаемой сборки.
    /// </summary>
    /// <param name="loadedAssembly">Загруженная сборка.</param>
    /// <param name="inputEvidence">
    ///   Дополнительные свидетельство, чтобы добавить свидетельство сборки.
    /// </param>
    /// <returns>Свидетельство для сборки.</returns>
    public virtual Evidence ProvideAssemblyEvidence(Assembly loadedAssembly, Evidence inputEvidence)
    {
      return inputEvidence;
    }

    /// <summary>Определяет, следует ли выполнять приложение.</summary>
    /// <param name="applicationEvidence">
    ///   Свидетельство для активируемого приложения.
    /// </param>
    /// <param name="activatorEvidence">
    ///   При необходимости свидетельство для активации домена приложения.
    /// </param>
    /// <param name="context">Контекст доверия.</param>
    /// <returns>Объект, содержащий доверия сведения о приложении.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="applicationEvidence" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="T:System.Runtime.Hosting.ActivationArguments" /> Не удалось найти объект свидетельства приложения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="P:System.Runtime.Hosting.ActivationArguments.ActivationContext" /> Является свойство аргументы активизации <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="T:System.Security.Policy.ApplicationTrust" /> Предоставить набор не содержит минимальное значение определяется запрос <see cref="T:System.ActivationContext" />.
    /// </exception>
    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
    public virtual ApplicationTrust DetermineApplicationTrust(Evidence applicationEvidence, Evidence activatorEvidence, TrustManagerContext context)
    {
      if (applicationEvidence == null)
        throw new ArgumentNullException(nameof (applicationEvidence));
      ActivationArguments hostEvidence = applicationEvidence.GetHostEvidence<ActivationArguments>();
      if (hostEvidence == null)
        throw new ArgumentException(Environment.GetResourceString("Policy_MissingActivationContextInAppEvidence"));
      ActivationContext activationContext = hostEvidence.ActivationContext;
      if (activationContext == null)
        throw new ArgumentException(Environment.GetResourceString("Policy_MissingActivationContextInAppEvidence"));
      ApplicationTrust applicationTrust = applicationEvidence.GetHostEvidence<ApplicationTrust>();
      if (applicationTrust != null && !CmsUtils.CompareIdentities(applicationTrust.ApplicationIdentity, hostEvidence.ApplicationIdentity, ApplicationVersionMatch.MatchExactVersion))
        applicationTrust = (ApplicationTrust) null;
      if (applicationTrust == null)
        applicationTrust = AppDomain.CurrentDomain.ApplicationTrust == null || !CmsUtils.CompareIdentities(AppDomain.CurrentDomain.ApplicationTrust.ApplicationIdentity, hostEvidence.ApplicationIdentity, ApplicationVersionMatch.MatchExactVersion) ? ApplicationSecurityManager.DetermineApplicationTrustInternal(activationContext, context) : AppDomain.CurrentDomain.ApplicationTrust;
      ApplicationSecurityInfo applicationSecurityInfo = new ApplicationSecurityInfo(activationContext);
      if (applicationTrust != null && applicationTrust.IsApplicationTrustedToRun && !applicationSecurityInfo.DefaultRequestSet.IsSubsetOf(applicationTrust.DefaultGrantSet.PermissionSet))
        throw new InvalidOperationException(Environment.GetResourceString("Policy_AppTrustMustGrantAppRequest"));
      return applicationTrust;
    }

    /// <summary>
    ///   Определяет, какие разрешения следует предоставить коду на основе заданного свидетельства.
    /// </summary>
    /// <param name="evidence">
    ///   Набор свидетельств, используемых для оценки политики.
    /// </param>
    /// <returns>
    ///   Набор разрешений, которые могут быть предоставлены системой безопасности.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="evidence" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual PermissionSet ResolvePolicy(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException(nameof (evidence));
      if (evidence.GetHostEvidence<GacInstalled>() != null)
        return new PermissionSet(PermissionState.Unrestricted);
      if (AppDomain.CurrentDomain.IsHomogenous)
        return AppDomain.CurrentDomain.GetHomogenousGrantSet(evidence);
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        return new PermissionSet(PermissionState.Unrestricted);
      return SecurityManager.PolicyManager.CodeGroupResolve(evidence, false);
    }

    /// <summary>
    ///   Определяет какие типы свидетельства узел может предоставить для домена приложения, при запросе.
    /// </summary>
    /// <returns>Массив типов свидетельства.</returns>
    public virtual Type[] GetHostSuppliedAppDomainEvidenceTypes()
    {
      return (Type[]) null;
    }

    /// <summary>
    ///   Определяет какие типы свидетельства узел может предоставить сборке при запросе.
    /// </summary>
    /// <param name="assembly">Целевой сборки.</param>
    /// <returns>Массив типов свидетельства.</returns>
    public virtual Type[] GetHostSuppliedAssemblyEvidenceTypes(Assembly assembly)
    {
      return (Type[]) null;
    }

    /// <summary>
    ///   Запрашивает определенный тип свидетельства для домена приложения.
    /// </summary>
    /// <param name="evidenceType">Тип свидетельства.</param>
    /// <returns>Свидетельство домена запрошенного приложения.</returns>
    public virtual EvidenceBase GenerateAppDomainEvidence(Type evidenceType)
    {
      return (EvidenceBase) null;
    }

    /// <summary>
    ///   Запрашивает определенный тип свидетельства для сборки.
    /// </summary>
    /// <param name="evidenceType">Тип свидетельства.</param>
    /// <param name="assembly">Целевой сборки.</param>
    /// <returns>Запрошенное свидетельство сборки.</returns>
    public virtual EvidenceBase GenerateAssemblyEvidence(Type evidenceType, Assembly assembly)
    {
      return (EvidenceBase) null;
    }
  }
}
