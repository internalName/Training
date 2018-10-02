// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Threading;

namespace System.Security
{
  /// <summary>
  ///   Предоставляет главную точку доступа для классов, взаимодействующих с системой безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public static class SecurityManager
  {
    private static volatile SecurityPermission executionSecurityPermission = (SecurityPermission) null;
    private static PolicyManager polmgr = new PolicyManager();
    private static int[][] s_BuiltInPermissionIndexMap = new int[6][]
    {
      new int[2]{ 0, 10 },
      new int[2]{ 1, 11 },
      new int[2]{ 2, 12 },
      new int[2]{ 4, 13 },
      new int[2]{ 6, 14 },
      new int[2]{ 7, 9 }
    };
    private static CodeAccessPermission[] s_UnrestrictedSpecialPermissionMap = new CodeAccessPermission[6]
    {
      (CodeAccessPermission) new EnvironmentPermission(PermissionState.Unrestricted),
      (CodeAccessPermission) new FileDialogPermission(PermissionState.Unrestricted),
      (CodeAccessPermission) new FileIOPermission(PermissionState.Unrestricted),
      (CodeAccessPermission) new ReflectionPermission(PermissionState.Unrestricted),
      (CodeAccessPermission) new SecurityPermission(PermissionState.Unrestricted),
      (CodeAccessPermission) new UIPermission(PermissionState.Unrestricted)
    };

    internal static PolicyManager PolicyManager
    {
      get
      {
        return SecurityManager.polmgr;
      }
    }

    /// <summary>
    ///   Определяет, предоставлено ли разрешение вызывающему объекту.
    /// </summary>
    /// <param name="perm">
    ///   Разрешение для проверки разрешений вызывающего объекта.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если разрешений, предоставленных вызывающему включает разрешение <paramref name="perm" />; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [Obsolete("IsGranted is obsolete and will be removed in a future release of the .NET Framework.  Please use the PermissionSet property of either AppDomain or Assembly instead.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool IsGranted(IPermission perm)
    {
      if (perm == null)
        return true;
      PermissionSet o1 = (PermissionSet) null;
      PermissionSet o2 = (PermissionSet) null;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityManager.GetGrantedPermissions(JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o1), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o2), JitHelpers.GetStackCrawlMarkHandle(ref stackMark));
      if (!o1.Contains(perm))
        return false;
      if (o2 != null)
        return !o2.Contains(perm);
      return true;
    }

    /// <summary>
    ///   Возвращает набор разрешений, который можно безопасно выдать приложению, имеющему предоставленное свидетельство.
    /// </summary>
    /// <param name="evidence">
    ///   Свидетельство узла для сопоставления в набор разрешений.
    /// </param>
    /// <returns>
    ///   Набор разрешений, который может использоваться для выдачи для приложения, которое имеет предоставленного свидетельства.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="evidence" /> имеет значение <see langword="null" />.
    /// </exception>
    public static PermissionSet GetStandardSandbox(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException(nameof (evidence));
      Zone hostEvidence = evidence.GetHostEvidence<Zone>();
      if (hostEvidence == null)
        return new PermissionSet(PermissionState.None);
      if (hostEvidence.SecurityZone == SecurityZone.MyComputer)
        return new PermissionSet(PermissionState.Unrestricted);
      if (hostEvidence.SecurityZone == SecurityZone.Intranet)
      {
        PermissionSet localIntranet = (PermissionSet) BuiltInPermissionSets.LocalIntranet;
        PolicyStatement policyStatement1 = new NetCodeGroup((IMembershipCondition) new AllMembershipCondition()).Resolve(evidence);
        PolicyStatement policyStatement2 = new FileCodeGroup((IMembershipCondition) new AllMembershipCondition(), FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery).Resolve(evidence);
        if (policyStatement1 != null)
          localIntranet.InplaceUnion(policyStatement1.PermissionSet);
        if (policyStatement2 != null)
          localIntranet.InplaceUnion(policyStatement2.PermissionSet);
        return localIntranet;
      }
      if (hostEvidence.SecurityZone != SecurityZone.Internet && hostEvidence.SecurityZone != SecurityZone.Trusted)
        return new PermissionSet(PermissionState.None);
      PermissionSet internet = (PermissionSet) BuiltInPermissionSets.Internet;
      PolicyStatement policyStatement = new NetCodeGroup((IMembershipCondition) new AllMembershipCondition()).Resolve(evidence);
      if (policyStatement != null)
        internet.InplaceUnion(policyStatement.PermissionSet);
      return internet;
    }

    /// <summary>
    ///   Получает идентификатор разрешенной зоны и наборы разрешений идентификатора URL для текущей сборки.
    /// </summary>
    /// <param name="zone">
    ///   Выходной параметр, содержащий <see cref="T:System.Collections.ArrayList" /> из предоставленных <see cref="P:System.Security.Permissions.ZoneIdentityPermissionAttribute.Zone" /> объектов.
    /// </param>
    /// <param name="origin">
    ///   Выходной параметр, содержащий <see cref="T:System.Collections.ArrayList" /> из предоставленных <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> объектов.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Запрос <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> не удалось.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void GetZoneAndOrigin(out ArrayList zone, out ArrayList origin)
    {
      StackCrawlMark mark = StackCrawlMark.LookForMyCaller;
      CodeAccessSecurityEngine.GetZoneAndOrigin(ref mark, out zone, out origin);
    }

    /// <summary>
    ///   Загружает <see cref="T:System.Security.Policy.PolicyLevel" /> из указанного файла.
    /// </summary>
    /// <param name="path">
    ///   Физический путь к файлу, содержащему сведения о политике безопасности.
    /// </param>
    /// <param name="type">
    ///   Одно из значений перечисления, указывающее тип уровня политики для загрузки.
    /// </param>
    /// <returns>Уровень загруженной политики.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Файл, указанный параметром <paramref name="path" />, не существует.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   В коде, который вызывает этот метод, отсутствует <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.
    /// 
    ///   -или-
    /// 
    ///   В коде, который вызывает этот метод, отсутствует <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />.
    /// 
    ///   -или-
    /// 
    ///   В коде, который вызывает этот метод, отсутствует <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />.
    /// 
    ///   -или-
    /// 
    ///   В коде, который вызывает этот метод, отсутствует <see cref="F:System.Security.Permissions.FileIOPermissionAccess.PathDiscovery" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод использует политику разграничения доступа кода (CAS), которая является устаревшей для .NET Framework 4.
    ///    Чтобы включить политику CAS для обеспечения совместимости с предыдущими версиями .NET Framework, используйте элемент &lt;legacyCasPolicy&gt;.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
    public static PolicyLevel LoadPolicyLevelFromFile(string path, PolicyLevelType type)
    {
      if (path == null)
        throw new ArgumentNullException(nameof (path));
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      if (!File.InternalExists(path))
        throw new ArgumentException(Environment.GetResourceString("Argument_PolicyFileDoesNotExist"));
      string fullPath = Path.GetFullPath(path);
      FileIOPermission fileIoPermission = new FileIOPermission(PermissionState.None);
      fileIoPermission.AddPathList(FileIOPermissionAccess.Read, fullPath);
      fileIoPermission.AddPathList(FileIOPermissionAccess.Write, fullPath);
      fileIoPermission.Demand();
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        using (StreamReader streamReader = new StreamReader((Stream) fileStream))
          return SecurityManager.LoadPolicyLevelFromStringHelper(streamReader.ReadToEnd(), path, type);
      }
    }

    /// <summary>
    ///   Загружает <see cref="T:System.Security.Policy.PolicyLevel" /> из указанной строки.
    /// </summary>
    /// <param name="str">
    ///   XML-представление уровня политики безопасности в той же форме, в которой оно содержится в файле конфигурации.
    /// </param>
    /// <param name="type">
    ///   Одно из значений перечисления, указывающее тип уровня политики для загрузки.
    /// </param>
    /// <returns>Уровень загрузки политики.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="str" /> Указан недопустимый параметр.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Код, который вызывает этот метод не <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
    public static PolicyLevel LoadPolicyLevelFromString(string str, PolicyLevelType type)
    {
      return SecurityManager.LoadPolicyLevelFromStringHelper(str, (string) null, type);
    }

    private static PolicyLevel LoadPolicyLevelFromStringHelper(string str, string path, PolicyLevelType type)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      PolicyLevel policyLevel = new PolicyLevel(type, path);
      SecurityElement topElement = new Parser(str).GetTopElement();
      if (topElement == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), (object) "configuration"));
      SecurityElement securityElement1 = topElement.SearchForChildByTag("mscorlib");
      if (securityElement1 == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), (object) "mscorlib"));
      SecurityElement securityElement2 = securityElement1.SearchForChildByTag("security");
      if (securityElement2 == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), (object) "security"));
      SecurityElement securityElement3 = securityElement2.SearchForChildByTag("policy");
      if (securityElement3 == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), (object) "policy"));
      SecurityElement e = securityElement3.SearchForChildByTag("PolicyLevel");
      if (e == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), (object) "PolicyLevel"));
      policyLevel.FromXml(e);
      return policyLevel;
    }

    /// <summary>
    ///   Сохраняет измененный уровень политики безопасности, скачанный вместе с <see cref="M:System.Security.SecurityManager.LoadPolicyLevelFromFile(System.String,System.Security.PolicyLevelType)" />.
    /// </summary>
    /// <param name="level">
    ///   Объект уровня политики, который требуется сохранить.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   В коде, который вызывает этот метод, отсутствует <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод использует политику разграничения доступа кода (CAS), которая является устаревшей для .NET Framework 4.
    ///    Чтобы включить политику CAS для обеспечения совместимости с предыдущими версиями .NET Framework, используйте элемент &lt;legacyCasPolicy&gt;.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
    public static void SavePolicyLevel(PolicyLevel level)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      PolicyManager.EncodeLevel(level);
    }

    /// <summary>
    ///   Определяет разрешения, которые нужно предоставить коду, на основе определенного свидетельства и запросов.
    /// </summary>
    /// <param name="evidence">
    ///   Набор свидетельств, используемых для оценки политики.
    /// </param>
    /// <param name="reqdPset">
    ///   Необходимые разрешения для выполнения кода.
    /// </param>
    /// <param name="optPset">
    ///   Необязательные разрешения, которые можно предоставить, но они не нужны для выполнения кода.
    /// </param>
    /// <param name="denyPset">
    ///   Запрещенные разрешения, которые нельзя предоставлять для кода, даже если это разрешено политикой.
    /// </param>
    /// <param name="denied">
    ///   Выходной параметр, содержащий набор непредоставленных разрешений.
    /// </param>
    /// <returns>
    ///   Набор разрешений, предоставляемых системой безопасности.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод использует политику разграничения доступа кода (CAS), которая является устаревшей для .NET Framework 4.
    ///    Чтобы включить политику CAS для обеспечения совместимости с предыдущими версиями .NET Framework, используйте элемент &lt;legacyCasPolicy&gt;.
    /// </exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">
    ///   Политике не удалось предоставить минимальные необходимые разрешения, определенные параметром <paramref name="reqdPset" />.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static PermissionSet ResolvePolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      return SecurityManager.ResolvePolicy(evidence, reqdPset, optPset, denyPset, out denied, true);
    }

    /// <summary>
    ///   Определяет разрешения, которые нужно предоставить коду, на основе определенного свидетельства.
    /// </summary>
    /// <param name="evidence">
    ///   Набор свидетельств, используемых для оценки политики.
    /// </param>
    /// <returns>
    ///   Набор разрешений, которые могут предоставляться системой безопасности.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод использует политику разграничения доступа кода (CAS), которая является устаревшей для .NET Framework 4.
    ///    Чтобы включить политику CAS для обеспечения совместимости с предыдущими версиями .NET Framework, используйте элемент &lt;legacyCasPolicy&gt;.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static PermissionSet ResolvePolicy(Evidence evidence)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      if (evidence == null)
        evidence = new Evidence();
      return SecurityManager.polmgr.Resolve(evidence);
    }

    /// <summary>
    ///   Определяет разрешения, которые нужно предоставить коду, на основе определенного свидетельства.
    /// </summary>
    /// <param name="evidences">
    ///   Массив объектов свидетельства, используемых для оценки политики.
    /// </param>
    /// <returns>
    ///   Набор разрешений, которые подходят для всех указанных свидетельств.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод использует политику разграничения доступа кода (CAS), которая является устаревшей для .NET Framework 4.
    ///    Чтобы включить политику CAS для обеспечения совместимости с предыдущими версиями .NET Framework, используйте элемент &lt;legacyCasPolicy&gt;.
    /// </exception>
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static PermissionSet ResolvePolicy(Evidence[] evidences)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      if (evidences == null || evidences.Length == 0)
        evidences = new Evidence[1];
      PermissionSet permissionSet = SecurityManager.ResolvePolicy(evidences[0]);
      if (permissionSet == null)
        return (PermissionSet) null;
      for (int index = 1; index < evidences.Length; ++index)
      {
        permissionSet = permissionSet.Intersect(SecurityManager.ResolvePolicy(evidences[index]));
        if (permissionSet == null || permissionSet.IsEmpty())
          return permissionSet;
      }
      return permissionSet;
    }

    /// <summary>
    ///   Определяет, требует ли текущий поток перенаправления контекста безопасности, если состояние безопасности нельзя воссоздать в более позднее время.
    /// </summary>
    /// <returns>
    ///   <see langword="false" /> Если содержит стек не частично доверенные домены приложений, не частично доверенным сборкам и нет активного <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> или <see cref="M:System.Security.CodeAccessPermission.Deny" /> модификаторы; <see langword="true" /> Если общеязыковая среда выполнения не гарантирует, что стек не содержит ни одного из них.
    /// </returns>
    [SecurityCritical]
    public static bool CurrentThreadRequiresSecurityContextCapture()
    {
      return !CodeAccessSecurityEngine.QuickCheckForAllDemands();
    }

    /// <summary>
    ///   Определяет, какие разрешения следует предоставить коду на основе указанного свидетельства (кроме политики для уровня <see cref="T:System.AppDomain" />).
    /// </summary>
    /// <param name="evidence">
    ///   Набор свидетельств, используемых для оценки политики.
    /// </param>
    /// <returns>
    ///   Набор разрешений, которые могут предоставляться системой безопасности.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод использует политику разграничения доступа кода (CAS), которая является устаревшей для .NET Framework 4.
    ///    Чтобы включить политику CAS для обеспечения совместимости с предыдущими версиями .NET Framework, используйте элемент &lt;legacyCasPolicy&gt;.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static PermissionSet ResolveSystemPolicy(Evidence evidence)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      if (PolicyManager.IsGacAssembly(evidence))
        return new PermissionSet(PermissionState.Unrestricted);
      return SecurityManager.polmgr.CodeGroupResolve(evidence, true);
    }

    /// <summary>
    ///   Возвращает коллекцию групп кода, соответствующих указанному свидетельству.
    /// </summary>
    /// <param name="evidence">
    ///   Набор свидетельств, по которому оценивается политика.
    /// </param>
    /// <returns>
    ///   Перечисление набора групп кода, соответствующих свидетельству.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод использует политику разграничения доступа кода (CAS), которая является устаревшей для .NET Framework 4.
    ///    Чтобы включить политику CAS для обеспечения совместимости с предыдущими версиями .NET Framework, используйте элемент &lt;legacyCasPolicy&gt;.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static IEnumerator ResolvePolicyGroups(Evidence evidence)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      return SecurityManager.polmgr.ResolveCodeGroups(evidence);
    }

    /// <summary>
    ///   Предоставляет перечислитель для доступа к иерархии политик безопасности по уровням, например к политике компьютера и политике пользователя.
    /// </summary>
    /// <returns>
    ///   Перечислитель для объектов <see cref="T:System.Security.Policy.PolicyLevel" />, образующих иерархию политик безопасности.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод использует политику разграничения доступа кода (CAS), которая является устаревшей для .NET Framework 4.
    ///    Чтобы включить политику CAS для обеспечения совместимости с предыдущими версиями .NET Framework, используйте элемент &lt;legacyCasPolicy&gt;.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   В коде, который вызывает этот метод, отсутствует <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static IEnumerator PolicyHierarchy()
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      return SecurityManager.polmgr.PolicyHierarchy();
    }

    /// <summary>
    ///   Сохраняет измененное состояние политики безопасности.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод использует политику разграничения доступа кода (CAS), которая является устаревшей для .NET Framework 4.
    ///    Чтобы включить политику CAS для обеспечения совместимости с предыдущими версиями .NET Framework, используйте элемент &lt;legacyCasPolicy&gt;.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   В коде, который вызывает этот метод, отсутствует <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
    public static void SavePolicy()
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      SecurityManager.polmgr.Save();
    }

    [SecurityCritical]
    private static PermissionSet ResolveCasPolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied, out int securitySpecialFlags, bool checkExecutionPermission)
    {
      CodeAccessPermission.Assert(true);
      PermissionSet grantSet = SecurityManager.ResolvePolicy(evidence, reqdPset, optPset, denyPset, out denied, checkExecutionPermission);
      securitySpecialFlags = SecurityManager.GetSpecialFlags(grantSet, denied);
      return grantSet;
    }

    [SecurityCritical]
    private static PermissionSet ResolvePolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied, bool checkExecutionPermission)
    {
      if (SecurityManager.executionSecurityPermission == null)
        SecurityManager.executionSecurityPermission = new SecurityPermission(SecurityPermissionFlag.Execution);
      Exception exception = (Exception) null;
      PermissionSet other1 = optPset;
      PermissionSet other2 = reqdPset != null ? (other1 == null ? (PermissionSet) null : reqdPset.Union(other1)) : other1;
      if (other2 != null && !other2.IsUnrestricted())
        other2.AddPermission((IPermission) SecurityManager.executionSecurityPermission);
      if (evidence == null)
        evidence = new Evidence();
      PermissionSet target = SecurityManager.polmgr.Resolve(evidence);
      if (other2 != null)
        target.InplaceIntersect(other2);
      if (checkExecutionPermission && (!target.Contains((IPermission) SecurityManager.executionSecurityPermission) || denyPset != null && denyPset.Contains((IPermission) SecurityManager.executionSecurityPermission)))
        throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"), -2146233320, exception);
      if (reqdPset != null && !reqdPset.IsSubsetOf(target))
        throw new PolicyException(Environment.GetResourceString("Policy_NoRequiredPermission"), -2146233321, exception);
      if (denyPset != null)
      {
        denied = denyPset.Copy();
        target.MergeDeniedSet(denied);
        if (denied.IsEmpty())
          denied = (PermissionSet) null;
      }
      else
        denied = (PermissionSet) null;
      target.IgnoreTypeLoadFailures = true;
      return target;
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, требуется ли коду <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> для выполнения.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если код должен иметь <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> для выполнения; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Код, который вызывает этот метод не <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.
    /// </exception>
    [Obsolete("Because execution permission checks can no longer be turned off, the CheckExecutionRights property no longer has any effect.")]
    public static bool CheckExecutionRights
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, включена ли безопасность.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если включена безопасность; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Код, который вызывает этот метод не <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.
    /// </exception>
    [Obsolete("Because security can no longer be turned off, the SecurityEnabled property no longer has any effect.")]
    public static bool SecurityEnabled
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    internal static int GetSpecialFlags(PermissionSet grantSet, PermissionSet deniedSet)
    {
      if (grantSet != null && grantSet.IsUnrestricted() && (deniedSet == null || deniedSet.IsEmpty()))
        return -1;
      SecurityPermissionFlag securityPermissionFlags = SecurityPermissionFlag.NoFlags;
      ReflectionPermissionFlag reflectionPermissionFlags = ReflectionPermissionFlag.NoFlags;
      CodeAccessPermission[] accessPermissionArray = new CodeAccessPermission[6];
      if (grantSet != null)
      {
        if (grantSet.IsUnrestricted())
        {
          securityPermissionFlags = SecurityPermissionFlag.AllFlags;
          reflectionPermissionFlags = ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess;
          for (int index = 0; index < accessPermissionArray.Length; ++index)
            accessPermissionArray[index] = SecurityManager.s_UnrestrictedSpecialPermissionMap[index];
        }
        else
        {
          SecurityPermission permission1 = grantSet.GetPermission(6) as SecurityPermission;
          if (permission1 != null)
            securityPermissionFlags = permission1.Flags;
          ReflectionPermission permission2 = grantSet.GetPermission(4) as ReflectionPermission;
          if (permission2 != null)
            reflectionPermissionFlags = permission2.Flags;
          for (int index = 0; index < accessPermissionArray.Length; ++index)
            accessPermissionArray[index] = grantSet.GetPermission(SecurityManager.s_BuiltInPermissionIndexMap[index][0]) as CodeAccessPermission;
        }
      }
      if (deniedSet != null)
      {
        if (deniedSet.IsUnrestricted())
        {
          securityPermissionFlags = SecurityPermissionFlag.NoFlags;
          reflectionPermissionFlags = ReflectionPermissionFlag.NoFlags;
          for (int index = 0; index < SecurityManager.s_BuiltInPermissionIndexMap.Length; ++index)
            accessPermissionArray[index] = (CodeAccessPermission) null;
        }
        else
        {
          SecurityPermission permission1 = deniedSet.GetPermission(6) as SecurityPermission;
          if (permission1 != null)
            securityPermissionFlags &= ~permission1.Flags;
          ReflectionPermission permission2 = deniedSet.GetPermission(4) as ReflectionPermission;
          if (permission2 != null)
            reflectionPermissionFlags &= ~permission2.Flags;
          for (int index = 0; index < SecurityManager.s_BuiltInPermissionIndexMap.Length; ++index)
          {
            CodeAccessPermission permission3 = deniedSet.GetPermission(SecurityManager.s_BuiltInPermissionIndexMap[index][0]) as CodeAccessPermission;
            if (permission3 != null && !permission3.IsSubsetOf((IPermission) null))
              accessPermissionArray[index] = (CodeAccessPermission) null;
          }
        }
      }
      int specialFlags = SecurityManager.MapToSpecialFlags(securityPermissionFlags, reflectionPermissionFlags);
      if (specialFlags != -1)
      {
        for (int index = 0; index < accessPermissionArray.Length; ++index)
        {
          if (accessPermissionArray[index] != null && ((IUnrestrictedPermission) accessPermissionArray[index]).IsUnrestricted())
            specialFlags |= 1 << SecurityManager.s_BuiltInPermissionIndexMap[index][1];
        }
      }
      return specialFlags;
    }

    private static int MapToSpecialFlags(SecurityPermissionFlag securityPermissionFlags, ReflectionPermissionFlag reflectionPermissionFlags)
    {
      int num = 0;
      if ((securityPermissionFlags & SecurityPermissionFlag.UnmanagedCode) == SecurityPermissionFlag.UnmanagedCode)
        num |= 1;
      if ((securityPermissionFlags & SecurityPermissionFlag.SkipVerification) == SecurityPermissionFlag.SkipVerification)
        num |= 2;
      if ((securityPermissionFlags & SecurityPermissionFlag.Assertion) == SecurityPermissionFlag.Assertion)
        num |= 8;
      if ((securityPermissionFlags & SecurityPermissionFlag.SerializationFormatter) == SecurityPermissionFlag.SerializationFormatter)
        num |= 32;
      if ((securityPermissionFlags & SecurityPermissionFlag.BindingRedirects) == SecurityPermissionFlag.BindingRedirects)
        num |= 256;
      if ((securityPermissionFlags & SecurityPermissionFlag.ControlEvidence) == SecurityPermissionFlag.ControlEvidence)
        num |= 65536;
      if ((securityPermissionFlags & SecurityPermissionFlag.ControlPrincipal) == SecurityPermissionFlag.ControlPrincipal)
        num |= 131072;
      if ((reflectionPermissionFlags & ReflectionPermissionFlag.RestrictedMemberAccess) == ReflectionPermissionFlag.RestrictedMemberAccess)
        num |= 64;
      if ((reflectionPermissionFlags & ReflectionPermissionFlag.MemberAccess) == ReflectionPermissionFlag.MemberAccess)
        num |= 16;
      return num;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool IsSameType(string strLeft, string strRight);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool _SetThreadSecurity(bool bThreadSecurity);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void GetGrantedPermissions(ObjectHandleOnStack retGranted, ObjectHandleOnStack retDenied, StackCrawlMarkHandle stackMark);
  }
}
