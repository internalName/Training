// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PolicyLevel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security.Policy
{
  /// <summary>
  ///   Предоставляет уровни политики безопасности для среды CLR.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class PolicyLevel
  {
    private static readonly string[] s_reservedNamedPermissionSets = new string[7]
    {
      "FullTrust",
      "Nothing",
      "Execution",
      "SkipVerification",
      "Internet",
      "LocalIntranet",
      "Everything"
    };
    private static string[] EcmaFullTrustAssemblies = new string[9]
    {
      "mscorlib.resources",
      "System",
      "System.resources",
      "System.Xml",
      "System.Xml.resources",
      "System.Windows.Forms",
      "System.Windows.Forms.resources",
      "System.Data",
      "System.Data.resources"
    };
    private static string[] MicrosoftFullTrustAssemblies = new string[12]
    {
      "System.Security",
      "System.Security.resources",
      "System.Drawing",
      "System.Drawing.resources",
      "System.Messaging",
      "System.Messaging.resources",
      "System.ServiceProcess",
      "System.ServiceProcess.resources",
      "System.DirectoryServices",
      "System.DirectoryServices.resources",
      "System.Deployment",
      "System.Deployment.resources"
    };
    private ArrayList m_fullTrustAssemblies;
    private ArrayList m_namedPermissionSets;
    private CodeGroup m_rootCodeGroup;
    private string m_label;
    [OptionalField(VersionAdded = 2)]
    private PolicyLevelType m_type;
    private ConfigId m_configId;
    private bool m_useDefaultCodeGroupsOnReset;
    private bool m_generateQuickCacheOnLoad;
    private bool m_caching;
    private bool m_throwOnLoadError;
    private Encoding m_encoding;
    private bool m_loaded;
    private SecurityElement m_permSetElement;
    private string m_path;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (PolicyLevel.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange(ref PolicyLevel.s_InternalSyncObject, obj, (object) null);
        }
        return PolicyLevel.s_InternalSyncObject;
      }
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_label == null)
        return;
      this.DeriveTypeFromLabel();
    }

    private void DeriveTypeFromLabel()
    {
      if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_User")))
        this.m_type = PolicyLevelType.User;
      else if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_Machine")))
        this.m_type = PolicyLevelType.Machine;
      else if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_Enterprise")))
      {
        this.m_type = PolicyLevelType.Enterprise;
      }
      else
      {
        if (!this.m_label.Equals(Environment.GetResourceString("Policy_PL_AppDomain")))
          throw new ArgumentException(Environment.GetResourceString("Policy_Default"));
        this.m_type = PolicyLevelType.AppDomain;
      }
    }

    private string DeriveLabelFromType()
    {
      switch (this.m_type)
      {
        case PolicyLevelType.User:
          return Environment.GetResourceString("Policy_PL_User");
        case PolicyLevelType.Machine:
          return Environment.GetResourceString("Policy_PL_Machine");
        case PolicyLevelType.Enterprise:
          return Environment.GetResourceString("Policy_PL_Enterprise");
        case PolicyLevelType.AppDomain:
          return Environment.GetResourceString("Policy_PL_AppDomain");
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) this.m_type));
      }
    }

    private PolicyLevel()
    {
    }

    [SecurityCritical]
    internal PolicyLevel(PolicyLevelType type)
      : this(type, PolicyLevel.GetLocationFromType(type))
    {
    }

    internal PolicyLevel(PolicyLevelType type, string path)
      : this(type, path, ConfigId.None)
    {
    }

    internal PolicyLevel(PolicyLevelType type, string path, ConfigId configId)
    {
      this.m_type = type;
      this.m_path = path;
      this.m_loaded = path == null;
      if (this.m_path == null)
      {
        this.m_rootCodeGroup = this.CreateDefaultAllGroup();
        this.SetFactoryPermissionSets();
        this.SetDefaultFullTrustAssemblies();
      }
      this.m_configId = configId;
    }

    [SecurityCritical]
    internal static string GetLocationFromType(PolicyLevelType type)
    {
      switch (type)
      {
        case PolicyLevelType.User:
          return Config.UserDirectory + "security.config";
        case PolicyLevelType.Machine:
          return Config.MachineDirectory + "security.config";
        case PolicyLevelType.Enterprise:
          return Config.MachineDirectory + "enterprisesec.config";
        default:
          return (string) null;
      }
    }

    /// <summary>
    ///   Создает новый уровень политики для использования на уровне политики домена приложения.
    /// </summary>
    /// <returns>
    ///   Вновь созданный <see cref="T:System.Security.Policy.PolicyLevel" />.
    /// </returns>
    [SecuritySafeCritical]
    [Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static PolicyLevel CreateAppDomainLevel()
    {
      return new PolicyLevel(PolicyLevelType.AppDomain);
    }

    /// <summary>Получает описательную метку для уровня политики.</summary>
    /// <returns>Метка, связанная с уровнем политики.</returns>
    public string Label
    {
      get
      {
        if (this.m_label == null)
          this.m_label = this.DeriveLabelFromType();
        return this.m_label;
      }
    }

    /// <summary>Возвращает тип уровня политики.</summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.PolicyLevelType" />.
    /// </returns>
    [ComVisible(false)]
    public PolicyLevelType Type
    {
      get
      {
        return this.m_type;
      }
    }

    internal ConfigId ConfigId
    {
      get
      {
        return this.m_configId;
      }
    }

    internal string Path
    {
      get
      {
        return this.m_path;
      }
    }

    /// <summary>Возвращает путь, где хранится файл политики.</summary>
    /// <returns>
    ///   Путь, где хранится файл политики, или <see langword="null" /> Если <see cref="T:System.Security.Policy.PolicyLevel" /> имеет место хранения.
    /// </returns>
    public string StoreLocation
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)] get
      {
        return PolicyLevel.GetLocationFromType(this.m_type);
      }
    }

    /// <summary>
    ///   Возвращает или задает корневую группу кода для уровня политики.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Policy.CodeGroup" /> Это корень дерева групп кода уровня политики.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение <see cref="P:System.Security.Policy.PolicyLevel.RootCodeGroup" /> является <see langword="null" />.
    /// </exception>
    public CodeGroup RootCodeGroup
    {
      [SecuritySafeCritical] get
      {
        this.CheckLoaded();
        return this.m_rootCodeGroup;
      }
      [SecuritySafeCritical] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (RootCodeGroup));
        this.CheckLoaded();
        this.m_rootCodeGroup = value.Copy();
      }
    }

    /// <summary>
    ///   Получает список именованных наборов разрешений для уровня политики.
    /// </summary>
    /// <returns>
    ///   Список именованных наборов разрешений для уровня политики.
    /// </returns>
    public IList NamedPermissionSets
    {
      [SecuritySafeCritical] get
      {
        this.CheckLoaded();
        this.LoadAllPermissionSets();
        ArrayList arrayList = new ArrayList(this.m_namedPermissionSets.Count);
        foreach (PermissionSet namedPermissionSet in this.m_namedPermissionSets)
          arrayList.Add((object) namedPermissionSet.Copy());
        return (IList) arrayList;
      }
    }

    /// <summary>
    ///   Обрабатывает политику на уровне политики и возвращает корень дерева групп кода, соответствующих свидетельству.
    /// </summary>
    /// <param name="evidence">
    ///   <see cref="T:System.Security.Policy.Evidence" /> Используется для разрешения политики.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Policy.CodeGroup" /> представляет корень дерева групп кода, соответствующих указанному свидетельству.
    /// </returns>
    /// <exception cref="T:System.Security.Policy.PolicyException">
    ///   Уровень политики содержит несколько соответствующих групп кода, помеченным атрибутом exclusive.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="evidence" /> имеет значение <see langword="null" />.
    /// </exception>
    public CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException(nameof (evidence));
      return this.RootCodeGroup.ResolveMatchingCodeGroups(evidence);
    }

    /// <summary>
    ///   Добавляет <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> соответствующий указанному <see cref="T:System.Security.Policy.StrongName" /> в список <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> объекты, используемые для определения, является ли сборка членом группы сборок, которые не должны оцениваться.
    /// </summary>
    /// <param name="sn">
    ///   <see cref="T:System.Security.Policy.StrongName" /> Используется для создания <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> для добавления в список <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> объекты, используемые для определения, является ли сборка членом группы сборок, которые не должны оцениваться.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="sn" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="T:System.Security.Policy.StrongName" /> Определяется <paramref name="sn" /> параметр уже имеет полное доверие.
    /// </exception>
    [Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
    public void AddFullTrustAssembly(StrongName sn)
    {
      if (sn == null)
        throw new ArgumentNullException(nameof (sn));
      this.AddFullTrustAssembly(new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version));
    }

    /// <summary>
    ///   Добавляет указанный <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> в список <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> объекты, используемые для определения, является ли сборка членом группы сборок, которые не должны оцениваться.
    /// </summary>
    /// <param name="snMC">
    ///   <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> Для добавления в список <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> объекты, используемые для определения, является ли сборка членом группы сборок, которые не должны оцениваться.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="snMC" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> Определяется <paramref name="snMC" /> параметр уже имеет полное доверие.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
    public void AddFullTrustAssembly(StrongNameMembershipCondition snMC)
    {
      if (snMC == null)
        throw new ArgumentNullException(nameof (snMC));
      this.CheckLoaded();
      foreach (StrongNameMembershipCondition fullTrustAssembly in this.m_fullTrustAssemblies)
      {
        if (fullTrustAssembly.Equals((object) snMC))
          throw new ArgumentException(Environment.GetResourceString("Argument_AssemblyAlreadyFullTrust"));
      }
      lock (this.m_fullTrustAssemblies)
        this.m_fullTrustAssemblies.Add((object) snMC);
    }

    /// <summary>
    ///   Удаляет сборку с указанным <see cref="T:System.Security.Policy.StrongName" /> список сборок, на уровне политики используется для оценки политики.
    /// </summary>
    /// <param name="sn">
    ///   <see cref="T:System.Security.Policy.StrongName" /> Сборки, которую необходимо удалить из списка сборок, используемых для оценки политики.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="sn" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сборка с <see cref="T:System.Security.Policy.StrongName" /> определяется <paramref name="sn" /> параметр не имеет полное доверие.
    /// </exception>
    [Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
    public void RemoveFullTrustAssembly(StrongName sn)
    {
      if (sn == null)
        throw new ArgumentNullException("assembly");
      this.RemoveFullTrustAssembly(new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version));
    }

    /// <summary>
    ///   Удаляет сборку с указанным <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> список сборок, на уровне политики используется для оценки политики.
    /// </summary>
    /// <param name="snMC">
    ///   <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> Сборки, которую необходимо удалить из списка сборок, используемых для оценки политики.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="snMC" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> Определяется <paramref name="snMC" /> параметр не имеет полное доверие.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
    public void RemoveFullTrustAssembly(StrongNameMembershipCondition snMC)
    {
      if (snMC == null)
        throw new ArgumentNullException(nameof (snMC));
      this.CheckLoaded();
      object obj = (object) null;
      IEnumerator enumerator = this.m_fullTrustAssemblies.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (((StrongNameMembershipCondition) enumerator.Current).Equals((object) snMC))
        {
          obj = enumerator.Current;
          break;
        }
      }
      if (obj == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_AssemblyNotFullTrust"));
      lock (this.m_fullTrustAssemblies)
        this.m_fullTrustAssemblies.Remove(obj);
    }

    /// <summary>
    ///   Получает список <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> объекты, используемые для определения, является ли сборка членом группы сборок, используемых для оценки политики безопасности.
    /// </summary>
    /// <returns>
    ///   Список <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> объекты, используемые для определения, является ли сборка членом группы сборок, используемых для оценки политики безопасности.
    ///    Этим сборкам предоставляется полное доверие во время оценки политики безопасности сборок, не содержащихся в списке.
    /// </returns>
    [Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
    public IList FullTrustAssemblies
    {
      [SecuritySafeCritical] get
      {
        this.CheckLoaded();
        return (IList) new ArrayList((ICollection) this.m_fullTrustAssemblies);
      }
    }

    /// <summary>
    ///   Добавляет <see cref="T:System.Security.NamedPermissionSet" /> для текущего уровня политики.
    /// </summary>
    /// <param name="permSet">
    ///   <see cref="T:System.Security.NamedPermissionSet" /> Добавление текущего уровня политики.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="permSet" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="permSet" /> Параметр имеет то же имя, что и у существующей <see cref="T:System.Security.NamedPermissionSet" /> в <see cref="T:System.Security.Policy.PolicyLevel" />.
    /// </exception>
    [SecuritySafeCritical]
    public void AddNamedPermissionSet(NamedPermissionSet permSet)
    {
      if (permSet == null)
        throw new ArgumentNullException(nameof (permSet));
      this.CheckLoaded();
      this.LoadAllPermissionSets();
      lock (this)
      {
        foreach (NamedPermissionSet namedPermissionSet in this.m_namedPermissionSets)
        {
          if (namedPermissionSet.Name.Equals(permSet.Name))
            throw new ArgumentException(Environment.GetResourceString("Argument_DuplicateName"));
        }
        NamedPermissionSet namedPermissionSet1 = (NamedPermissionSet) permSet.Copy();
        namedPermissionSet1.IgnoreTypeLoadFailures = true;
        this.m_namedPermissionSets.Add((object) namedPermissionSet1);
      }
    }

    /// <summary>
    ///   Удаляет указанный <see cref="T:System.Security.NamedPermissionSet" /> из текущего уровня политики.
    /// </summary>
    /// <param name="permSet">
    ///   <see cref="T:System.Security.NamedPermissionSet" /> Для удаления из текущего уровня политики.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Security.NamedPermissionSet" /> Был удален.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="T:System.Security.NamedPermissionSet" /> Определяется <paramref name="permSet" /> параметр не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="permSet" /> имеет значение <see langword="null" />.
    /// </exception>
    public NamedPermissionSet RemoveNamedPermissionSet(NamedPermissionSet permSet)
    {
      if (permSet == null)
        throw new ArgumentNullException(nameof (permSet));
      return this.RemoveNamedPermissionSet(permSet.Name);
    }

    /// <summary>
    ///   Удаляет <see cref="T:System.Security.NamedPermissionSet" /> с указанным именем из текущего уровня политики.
    /// </summary>
    /// <param name="name">
    ///   Имя удаляемого объекта <see cref="T:System.Security.NamedPermissionSet" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Security.NamedPermissionSet" /> Был удален.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> Параметр совпадает с именем зарезервированного набора разрешений.
    /// 
    ///   -или-
    /// 
    ///   Объект <see cref="T:System.Security.NamedPermissionSet" /> с указанным именем не найден.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public NamedPermissionSet RemoveNamedPermissionSet(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      this.CheckLoaded();
      this.LoadAllPermissionSets();
      int index1 = -1;
      for (int index2 = 0; index2 < PolicyLevel.s_reservedNamedPermissionSets.Length; ++index2)
      {
        if (PolicyLevel.s_reservedNamedPermissionSets[index2].Equals(name))
          throw new ArgumentException(Environment.GetResourceString("Argument_ReservedNPMS", (object) name));
      }
      ArrayList namedPermissionSets = this.m_namedPermissionSets;
      for (int index2 = 0; index2 < namedPermissionSets.Count; ++index2)
      {
        if (((NamedPermissionSet) namedPermissionSets[index2]).Name.Equals(name))
        {
          index1 = index2;
          break;
        }
      }
      if (index1 == -1)
        throw new ArgumentException(Environment.GetResourceString("Argument_NoNPMS"));
      ArrayList arrayList = new ArrayList();
      arrayList.Add((object) this.m_rootCodeGroup);
      for (int index2 = 0; index2 < arrayList.Count; ++index2)
      {
        CodeGroup codeGroup = (CodeGroup) arrayList[index2];
        if (codeGroup.PermissionSetName != null && codeGroup.PermissionSetName.Equals(name))
          throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInUse", (object) name));
        IEnumerator enumerator = codeGroup.Children.GetEnumerator();
        if (enumerator != null)
        {
          while (enumerator.MoveNext())
            arrayList.Add(enumerator.Current);
        }
      }
      NamedPermissionSet namedPermissionSet = (NamedPermissionSet) namedPermissionSets[index1];
      namedPermissionSets.RemoveAt(index1);
      return namedPermissionSet;
    }

    /// <summary>
    ///   Заменяет <see cref="T:System.Security.NamedPermissionSet" /> на текущем уровне политики с указанным <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    /// <param name="name">
    ///   Имя <see cref="T:System.Security.NamedPermissionSet" /> для замены.
    /// </param>
    /// <param name="pSet">
    ///   <see cref="T:System.Security.PermissionSet" /> Заменяет <see cref="T:System.Security.NamedPermissionSet" /> определяется <paramref name="name" /> параметр.
    /// </param>
    /// <returns>
    ///   Копия <see cref="T:System.Security.NamedPermissionSet" /> было заменено.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="pSet" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> Параметр совпадает с именем зарезервированного набора разрешений.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Security.PermissionSet" /> Определяется <paramref name="pSet" /> не удается найти параметр.
    /// </exception>
    [SecuritySafeCritical]
    public NamedPermissionSet ChangeNamedPermissionSet(string name, PermissionSet pSet)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (pSet == null)
        throw new ArgumentNullException(nameof (pSet));
      for (int index = 0; index < PolicyLevel.s_reservedNamedPermissionSets.Length; ++index)
      {
        if (PolicyLevel.s_reservedNamedPermissionSets[index].Equals(name))
          throw new ArgumentException(Environment.GetResourceString("Argument_ReservedNPMS", (object) name));
      }
      NamedPermissionSet permissionSetInternal = this.GetNamedPermissionSetInternal(name);
      if (permissionSetInternal == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NoNPMS"));
      NamedPermissionSet namedPermissionSet = (NamedPermissionSet) permissionSetInternal.Copy();
      permissionSetInternal.Reset();
      permissionSetInternal.SetUnrestricted(pSet.IsUnrestricted());
      foreach (IPermission p in pSet)
        permissionSetInternal.SetPermission(p.Copy());
      if (pSet is NamedPermissionSet)
        permissionSetInternal.Description = ((NamedPermissionSet) pSet).Description;
      return namedPermissionSet;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.NamedPermissionSet" /> на текущем уровне политики с указанным именем.
    /// </summary>
    /// <param name="name">
    ///   Имя <see cref="T:System.Security.NamedPermissionSet" /> для поиска.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Security.NamedPermissionSet" /> На текущем уровне политики с указанным именем, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public NamedPermissionSet GetNamedPermissionSet(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      NamedPermissionSet permissionSetInternal = this.GetNamedPermissionSetInternal(name);
      if (permissionSetInternal != null)
        return new NamedPermissionSet(permissionSetInternal);
      return (NamedPermissionSet) null;
    }

    /// <summary>
    ///   Заменяет файл конфигурации для этого <see cref="T:System.Security.Policy.PolicyLevel" /> с последней резервной копии (отражающей состояние политики до момента последнего сохранения) и возвращает его в состояние последнего сохранения.
    /// </summary>
    /// <exception cref="T:System.Security.Policy.PolicyException">
    ///   Уровень политики не имеет допустимого файла конфигурации.
    /// </exception>
    [SecuritySafeCritical]
    public void Recover()
    {
      if (this.m_configId == ConfigId.None)
        throw new PolicyException(Environment.GetResourceString("Policy_RecoverNotFileBased"));
      lock (this)
      {
        if (!Config.RecoverData(this.m_configId))
          throw new PolicyException(Environment.GetResourceString("Policy_RecoverNoConfigFile"));
        this.m_loaded = false;
        this.m_rootCodeGroup = (CodeGroup) null;
        this.m_namedPermissionSets = (ArrayList) null;
        this.m_fullTrustAssemblies = new ArrayList();
      }
    }

    /// <summary>
    ///   Возвращает текущий уровень политики в состояние по умолчанию.
    /// </summary>
    [SecuritySafeCritical]
    public void Reset()
    {
      this.SetDefault();
    }

    /// <summary>
    ///   Обрабатывает политику на основе свидетельства для уровня политики и возвращает итоговый <see cref="T:System.Security.Policy.PolicyStatement" />.
    /// </summary>
    /// <param name="evidence">
    ///   <see cref="T:System.Security.Policy.Evidence" /> Используется для разрешения <see cref="T:System.Security.Policy.PolicyLevel" />.
    /// </param>
    /// <returns>
    ///   Итоговый <see cref="T:System.Security.Policy.PolicyStatement" />.
    /// </returns>
    /// <exception cref="T:System.Security.Policy.PolicyException">
    ///   Уровень политики содержит несколько соответствующих групп кода, помеченным атрибутом exclusive.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="evidence" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public PolicyStatement Resolve(Evidence evidence)
    {
      return this.Resolve(evidence, 0, (byte[]) null);
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    [SecuritySafeCritical]
    public SecurityElement ToXml()
    {
      this.CheckLoaded();
      this.LoadAllPermissionSets();
      SecurityElement securityElement = new SecurityElement(nameof (PolicyLevel));
      securityElement.AddAttribute("version", "1");
      Hashtable classes = new Hashtable();
      lock (this)
      {
        SecurityElement child1 = new SecurityElement("NamedPermissionSets");
        foreach (PermissionSet namedPermissionSet in this.m_namedPermissionSets)
          child1.AddChild(this.NormalizeClassDeep(namedPermissionSet.ToXml(), classes));
        SecurityElement child2 = this.NormalizeClassDeep(this.m_rootCodeGroup.ToXml(this), classes);
        SecurityElement child3 = new SecurityElement("FullTrustAssemblies");
        foreach (StrongNameMembershipCondition fullTrustAssembly in this.m_fullTrustAssemblies)
          child3.AddChild(this.NormalizeClassDeep(fullTrustAssembly.ToXml(), classes));
        SecurityElement child4 = new SecurityElement("SecurityClasses");
        IDictionaryEnumerator enumerator = classes.GetEnumerator();
        while (enumerator.MoveNext())
        {
          SecurityElement child5 = new SecurityElement("SecurityClass");
          child5.AddAttribute("Name", (string) enumerator.Value);
          child5.AddAttribute("Description", (string) enumerator.Key);
          child4.AddChild(child5);
        }
        securityElement.AddChild(child4);
        securityElement.AddChild(child1);
        securityElement.AddChild(child2);
        securityElement.AddChild(child3);
      }
      return securityElement;
    }

    /// <summary>
    ///   Восстанавливает объект безопасности с данным состоянием из кодировки XML.
    /// </summary>
    /// <param name="e">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="e" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="T:System.Security.SecurityElement" /> Определяется <paramref name="e" /> указан недопустимый параметр.
    /// </exception>
    public void FromXml(SecurityElement e)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      lock (this)
      {
        ArrayList arrayList1 = new ArrayList();
        SecurityElement securityElement1 = e.SearchForChildByTag("SecurityClasses");
        Hashtable classes;
        if (securityElement1 != null)
        {
          classes = new Hashtable();
          foreach (SecurityElement child in securityElement1.Children)
          {
            if (child.Tag.Equals("SecurityClass"))
            {
              string str1 = child.Attribute("Name");
              string str2 = child.Attribute("Description");
              if (str1 != null && str2 != null)
                classes.Add((object) str1, (object) str2);
            }
          }
        }
        else
          classes = (Hashtable) null;
        SecurityElement securityElement2 = e.SearchForChildByTag("FullTrustAssemblies");
        if (securityElement2 != null && securityElement2.InternalChildren != null)
        {
          string assemblyQualifiedName = typeof (StrongNameMembershipCondition).AssemblyQualifiedName;
          foreach (SecurityElement child in securityElement2.Children)
          {
            StrongNameMembershipCondition membershipCondition = new StrongNameMembershipCondition();
            membershipCondition.FromXml(child);
            arrayList1.Add((object) membershipCondition);
          }
        }
        this.m_fullTrustAssemblies = arrayList1;
        ArrayList arrayList2 = new ArrayList();
        SecurityElement elem = e.SearchForChildByTag("NamedPermissionSets");
        SecurityElement element = (SecurityElement) null;
        if (elem != null && elem.InternalChildren != null)
        {
          element = this.UnnormalizeClassDeep(elem, classes);
          foreach (string namedPermissionSet in PolicyLevel.s_reservedNamedPermissionSets)
            this.FindElement(element, namedPermissionSet);
        }
        if (element == null)
          element = new SecurityElement("NamedPermissionSets");
        arrayList2.Add((object) BuiltInPermissionSets.FullTrust);
        arrayList2.Add((object) BuiltInPermissionSets.Everything);
        arrayList2.Add((object) BuiltInPermissionSets.SkipVerification);
        arrayList2.Add((object) BuiltInPermissionSets.Execution);
        arrayList2.Add((object) BuiltInPermissionSets.Nothing);
        arrayList2.Add((object) BuiltInPermissionSets.Internet);
        arrayList2.Add((object) BuiltInPermissionSets.LocalIntranet);
        foreach (PermissionSet permissionSet in arrayList2)
          permissionSet.IgnoreTypeLoadFailures = true;
        this.m_namedPermissionSets = arrayList2;
        this.m_permSetElement = element;
        SecurityElement securityElement3 = e.SearchForChildByTag("CodeGroup");
        if (securityElement3 == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", (object) "CodeGroup", (object) this.GetType().FullName));
        CodeGroup codeGroup = XMLUtil.CreateCodeGroup(this.UnnormalizeClassDeep(securityElement3, classes));
        if (codeGroup == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", (object) "CodeGroup", (object) this.GetType().FullName));
        codeGroup.FromXml(securityElement3, this);
        this.m_rootCodeGroup = codeGroup;
      }
    }

    [SecurityCritical]
    internal static PermissionSet GetBuiltInSet(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (PermissionSet) null;
      if (name.Equals("FullTrust"))
        return (PermissionSet) BuiltInPermissionSets.FullTrust;
      if (name.Equals("Nothing"))
        return (PermissionSet) BuiltInPermissionSets.Nothing;
      if (name.Equals("Execution"))
        return (PermissionSet) BuiltInPermissionSets.Execution;
      if (name.Equals("SkipVerification"))
        return (PermissionSet) BuiltInPermissionSets.SkipVerification;
      if (name.Equals("Internet"))
        return (PermissionSet) BuiltInPermissionSets.Internet;
      if (name.Equals("LocalIntranet"))
        return (PermissionSet) BuiltInPermissionSets.LocalIntranet;
      return (PermissionSet) null;
    }

    [SecurityCritical]
    internal NamedPermissionSet GetNamedPermissionSetInternal(string name)
    {
      this.CheckLoaded();
      lock (PolicyLevel.InternalSyncObject)
      {
        foreach (NamedPermissionSet namedPermissionSet in this.m_namedPermissionSets)
        {
          if (namedPermissionSet.Name.Equals(name))
            return namedPermissionSet;
        }
        if (this.m_permSetElement != null)
        {
          SecurityElement element = this.FindElement(this.m_permSetElement, name);
          if (element != null)
          {
            NamedPermissionSet namedPermissionSet = new NamedPermissionSet();
            namedPermissionSet.Name = name;
            this.m_namedPermissionSets.Add((object) namedPermissionSet);
            try
            {
              namedPermissionSet.FromXml(element, false, true);
            }
            catch
            {
              this.m_namedPermissionSets.Remove((object) namedPermissionSet);
              return (NamedPermissionSet) null;
            }
            if (namedPermissionSet.Name != null)
              return namedPermissionSet;
            this.m_namedPermissionSets.Remove((object) namedPermissionSet);
          }
        }
      }
      return (NamedPermissionSet) null;
    }

    [SecurityCritical]
    internal PolicyStatement Resolve(Evidence evidence, int count, byte[] serializedEvidence)
    {
      if (evidence == null)
        throw new ArgumentNullException(nameof (evidence));
      PolicyStatement policy1 = (PolicyStatement) null;
      if (serializedEvidence != null)
        policy1 = this.CheckCache(count, serializedEvidence);
      if (policy1 == null)
      {
        this.CheckLoaded();
        bool allConst;
        if (this.m_fullTrustAssemblies != null && PolicyLevel.IsFullTrustAssembly(this.m_fullTrustAssemblies, evidence))
        {
          policy1 = new PolicyStatement(new PermissionSet(true), PolicyStatementAttribute.Nothing);
          allConst = true;
        }
        else
        {
          ArrayList arrayList = this.GenericResolve(evidence, out allConst);
          policy1 = new PolicyStatement();
          policy1.PermissionSet = (PermissionSet) null;
          foreach (CodeGroupStackFrame codeGroupStackFrame in arrayList)
          {
            PolicyStatement policy2 = codeGroupStackFrame.policy;
            if (policy2 != null)
            {
              policy1.GetPermissionSetNoCopy().InplaceUnion(policy2.GetPermissionSetNoCopy());
              policy1.Attributes |= policy2.Attributes;
              if (policy2.HasDependentEvidence)
              {
                foreach (IDelayEvaluatedEvidence evaluatedEvidence in policy2.DependentEvidence)
                  evaluatedEvidence.MarkUsed();
              }
            }
          }
        }
        if (allConst)
          this.Cache(count, evidence.RawSerialize(), policy1);
      }
      return policy1;
    }

    [SecurityCritical]
    private void CheckLoaded()
    {
      if (this.m_loaded)
        return;
      lock (PolicyLevel.InternalSyncObject)
      {
        if (this.m_loaded)
          return;
        this.LoadPolicyLevel();
      }
    }

    private static byte[] ReadFile(string fileName)
    {
      using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
      {
        int length = (int) fileStream.Length;
        byte[] buffer = new byte[length];
        fileStream.Read(buffer, 0, length);
        fileStream.Close();
        return buffer;
      }
    }

    [SecurityCritical]
    private void LoadPolicyLevel()
    {
      Exception exception = (Exception) null;
      CodeAccessPermission.Assert(true);
      if (File.InternalExists(this.m_path))
      {
        Encoding utF8 = Encoding.UTF8;
        SecurityElement securityElement1;
        try
        {
          securityElement1 = SecurityElement.FromString(utF8.GetString(PolicyLevel.ReadFile(this.m_path)));
        }
        catch (Exception ex)
        {
          exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParseEx", (object) this.Label, (object) (string.IsNullOrEmpty(ex.Message) ? ex.GetType().AssemblyQualifiedName : ex.Message)));
          goto label_17;
        }
        if (securityElement1 == null)
        {
          exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", (object) this.Label));
        }
        else
        {
          SecurityElement securityElement2 = securityElement1.SearchForChildByTag("mscorlib");
          if (securityElement2 == null)
          {
            exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", (object) this.Label));
          }
          else
          {
            SecurityElement securityElement3 = securityElement2.SearchForChildByTag("security");
            if (securityElement3 == null)
            {
              exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", (object) this.Label));
            }
            else
            {
              SecurityElement securityElement4 = securityElement3.SearchForChildByTag("policy");
              if (securityElement4 == null)
              {
                exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", (object) this.Label));
              }
              else
              {
                SecurityElement e = securityElement4.SearchForChildByTag(nameof (PolicyLevel));
                if (e != null)
                {
                  try
                  {
                    this.FromXml(e);
                  }
                  catch (Exception ex)
                  {
                    exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", (object) this.Label));
                    goto label_17;
                  }
                  this.m_loaded = true;
                  return;
                }
                exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", (object) this.Label));
              }
            }
          }
        }
      }
label_17:
      this.SetDefault();
      this.m_loaded = true;
      if (exception != null)
        throw exception;
    }

    [SecurityCritical]
    private Exception LoadError(string message)
    {
      if (this.m_type != PolicyLevelType.User && this.m_type != PolicyLevelType.Machine && this.m_type != PolicyLevelType.Enterprise)
        return (Exception) new ArgumentException(message);
      Config.WriteToEventLog(message);
      return (Exception) null;
    }

    [SecurityCritical]
    private void Cache(int count, byte[] serializedEvidence, PolicyStatement policy)
    {
      if (this.m_configId == ConfigId.None || serializedEvidence == null)
        return;
      byte[] data = new SecurityDocument(policy.ToXml((PolicyLevel) null, true)).m_data;
      Config.AddCacheEntry(this.m_configId, count, serializedEvidence, data);
    }

    [SecurityCritical]
    private PolicyStatement CheckCache(int count, byte[] serializedEvidence)
    {
      if (this.m_configId == ConfigId.None)
        return (PolicyStatement) null;
      if (serializedEvidence == null)
        return (PolicyStatement) null;
      byte[] data;
      if (!Config.GetCacheEntry(this.m_configId, count, serializedEvidence, out data))
        return (PolicyStatement) null;
      PolicyStatement policyStatement = new PolicyStatement();
      SecurityDocument doc = new SecurityDocument(data);
      policyStatement.FromXml(doc, 0, (PolicyLevel) null, true);
      return policyStatement;
    }

    [SecurityCritical]
    private static bool IsFullTrustAssembly(ArrayList fullTrustAssemblies, Evidence evidence)
    {
      if (fullTrustAssemblies.Count == 0 || evidence == null)
        return false;
      lock (fullTrustAssemblies)
      {
        foreach (StrongNameMembershipCondition fullTrustAssembly in fullTrustAssemblies)
        {
          if (fullTrustAssembly.Check(evidence))
          {
            if (Environment.GetCompatibilityFlag(CompatibilityFlag.FullTrustListAssembliesInGac))
            {
              if (new ZoneMembershipCondition().Check(evidence))
                return true;
            }
            else if (new GacMembershipCondition().Check(evidence))
              return true;
          }
        }
      }
      return false;
    }

    private CodeGroup CreateDefaultAllGroup()
    {
      UnionCodeGroup unionCodeGroup = new UnionCodeGroup();
      unionCodeGroup.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new AllMembershipCondition().ToXml()), this);
      unionCodeGroup.Name = Environment.GetResourceString("Policy_AllCode_Name");
      unionCodeGroup.Description = Environment.GetResourceString("Policy_AllCode_DescriptionFullTrust");
      return (CodeGroup) unionCodeGroup;
    }

    [SecurityCritical]
    private CodeGroup CreateDefaultMachinePolicy()
    {
      UnionCodeGroup unionCodeGroup1 = new UnionCodeGroup();
      unionCodeGroup1.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Nothing", new AllMembershipCondition().ToXml()), this);
      unionCodeGroup1.Name = Environment.GetResourceString("Policy_AllCode_Name");
      unionCodeGroup1.Description = Environment.GetResourceString("Policy_AllCode_DescriptionNothing");
      UnionCodeGroup unionCodeGroup2 = new UnionCodeGroup();
      unionCodeGroup2.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new ZoneMembershipCondition(SecurityZone.MyComputer).ToXml()), this);
      unionCodeGroup2.Name = Environment.GetResourceString("Policy_MyComputer_Name");
      unionCodeGroup2.Description = Environment.GetResourceString("Policy_MyComputer_Description");
      StrongNamePublicKeyBlob blob1 = new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293");
      UnionCodeGroup unionCodeGroup3 = new UnionCodeGroup();
      unionCodeGroup3.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new StrongNameMembershipCondition(blob1, (string) null, (Version) null).ToXml()), this);
      unionCodeGroup3.Name = Environment.GetResourceString("Policy_Microsoft_Name");
      unionCodeGroup3.Description = Environment.GetResourceString("Policy_Microsoft_Description");
      unionCodeGroup2.AddChildInternal((CodeGroup) unionCodeGroup3);
      StrongNamePublicKeyBlob blob2 = new StrongNamePublicKeyBlob("00000000000000000400000000000000");
      UnionCodeGroup unionCodeGroup4 = new UnionCodeGroup();
      unionCodeGroup4.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new StrongNameMembershipCondition(blob2, (string) null, (Version) null).ToXml()), this);
      unionCodeGroup4.Name = Environment.GetResourceString("Policy_Ecma_Name");
      unionCodeGroup4.Description = Environment.GetResourceString("Policy_Ecma_Description");
      unionCodeGroup2.AddChildInternal((CodeGroup) unionCodeGroup4);
      unionCodeGroup1.AddChildInternal((CodeGroup) unionCodeGroup2);
      CodeGroup group1 = (CodeGroup) new UnionCodeGroup();
      group1.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "LocalIntranet", new ZoneMembershipCondition(SecurityZone.Intranet).ToXml()), this);
      group1.Name = Environment.GetResourceString("Policy_Intranet_Name");
      group1.Description = Environment.GetResourceString("Policy_Intranet_Description");
      CodeGroup group2 = (CodeGroup) new NetCodeGroup((IMembershipCondition) new AllMembershipCondition());
      group2.Name = Environment.GetResourceString("Policy_IntranetNet_Name");
      group2.Description = Environment.GetResourceString("Policy_IntranetNet_Description");
      group1.AddChildInternal(group2);
      CodeGroup group3 = (CodeGroup) new FileCodeGroup((IMembershipCondition) new AllMembershipCondition(), FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery);
      group3.Name = Environment.GetResourceString("Policy_IntranetFile_Name");
      group3.Description = Environment.GetResourceString("Policy_IntranetFile_Description");
      group1.AddChildInternal(group3);
      unionCodeGroup1.AddChildInternal(group1);
      CodeGroup group4 = (CodeGroup) new UnionCodeGroup();
      group4.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Internet", new ZoneMembershipCondition(SecurityZone.Internet).ToXml()), this);
      group4.Name = Environment.GetResourceString("Policy_Internet_Name");
      group4.Description = Environment.GetResourceString("Policy_Internet_Description");
      CodeGroup group5 = (CodeGroup) new NetCodeGroup((IMembershipCondition) new AllMembershipCondition());
      group5.Name = Environment.GetResourceString("Policy_InternetNet_Name");
      group5.Description = Environment.GetResourceString("Policy_InternetNet_Description");
      group4.AddChildInternal(group5);
      unionCodeGroup1.AddChildInternal(group4);
      CodeGroup group6 = (CodeGroup) new UnionCodeGroup();
      group6.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Nothing", new ZoneMembershipCondition(SecurityZone.Untrusted).ToXml()), this);
      group6.Name = Environment.GetResourceString("Policy_Untrusted_Name");
      group6.Description = Environment.GetResourceString("Policy_Untrusted_Description");
      unionCodeGroup1.AddChildInternal(group6);
      CodeGroup group7 = (CodeGroup) new UnionCodeGroup();
      group7.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Internet", new ZoneMembershipCondition(SecurityZone.Trusted).ToXml()), this);
      group7.Name = Environment.GetResourceString("Policy_Trusted_Name");
      group7.Description = Environment.GetResourceString("Policy_Trusted_Description");
      CodeGroup group8 = (CodeGroup) new NetCodeGroup((IMembershipCondition) new AllMembershipCondition());
      group8.Name = Environment.GetResourceString("Policy_TrustedNet_Name");
      group8.Description = Environment.GetResourceString("Policy_TrustedNet_Description");
      group7.AddChildInternal(group8);
      unionCodeGroup1.AddChildInternal(group7);
      return (CodeGroup) unionCodeGroup1;
    }

    private static SecurityElement CreateCodeGroupElement(string codeGroupType, string permissionSetName, SecurityElement mshipElement)
    {
      SecurityElement securityElement = new SecurityElement("CodeGroup");
      securityElement.AddAttribute("class", "System.Security." + codeGroupType + ", mscorlib, Version={VERSION}, Culture=neutral, PublicKeyToken=b77a5c561934e089" ?? "");
      securityElement.AddAttribute("version", "1");
      securityElement.AddAttribute("PermissionSetName", permissionSetName);
      securityElement.AddChild(mshipElement);
      return securityElement;
    }

    private void SetDefaultFullTrustAssemblies()
    {
      this.m_fullTrustAssemblies = new ArrayList();
      StrongNamePublicKeyBlob blob1 = new StrongNamePublicKeyBlob("00000000000000000400000000000000");
      for (int index = 0; index < PolicyLevel.EcmaFullTrustAssemblies.Length; ++index)
        this.m_fullTrustAssemblies.Add((object) new StrongNameMembershipCondition(blob1, PolicyLevel.EcmaFullTrustAssemblies[index], new Version("4.0.0.0")));
      StrongNamePublicKeyBlob blob2 = new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293");
      for (int index = 0; index < PolicyLevel.MicrosoftFullTrustAssemblies.Length; ++index)
        this.m_fullTrustAssemblies.Add((object) new StrongNameMembershipCondition(blob2, PolicyLevel.MicrosoftFullTrustAssemblies[index], new Version("4.0.0.0")));
    }

    [SecurityCritical]
    private void SetDefault()
    {
      lock (this)
      {
        string path = PolicyLevel.GetLocationFromType(this.m_type) + ".default";
        if (File.InternalExists(path))
        {
          PolicyLevel policyLevel = new PolicyLevel(this.m_type, path);
          this.m_rootCodeGroup = policyLevel.RootCodeGroup;
          this.m_namedPermissionSets = (ArrayList) policyLevel.NamedPermissionSets;
          this.m_fullTrustAssemblies = (ArrayList) policyLevel.FullTrustAssemblies;
          this.m_loaded = true;
        }
        else
        {
          this.m_namedPermissionSets = (ArrayList) null;
          this.m_rootCodeGroup = (CodeGroup) null;
          this.m_permSetElement = (SecurityElement) null;
          this.m_rootCodeGroup = this.m_type == PolicyLevelType.Machine ? this.CreateDefaultMachinePolicy() : this.CreateDefaultAllGroup();
          this.SetFactoryPermissionSets();
          this.SetDefaultFullTrustAssemblies();
          this.m_loaded = true;
        }
      }
    }

    private void SetFactoryPermissionSets()
    {
      lock (PolicyLevel.InternalSyncObject)
      {
        this.m_namedPermissionSets = new ArrayList();
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.FullTrust);
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.Everything);
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.Nothing);
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.SkipVerification);
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.Execution);
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.Internet);
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.LocalIntranet);
      }
    }

    private SecurityElement FindElement(SecurityElement element, string name)
    {
      foreach (SecurityElement child in element.Children)
      {
        if (child.Tag.Equals("PermissionSet"))
        {
          string str = child.Attribute("Name");
          if (str != null && str.Equals(name))
          {
            element.InternalChildren.Remove((object) child);
            return child;
          }
        }
      }
      return (SecurityElement) null;
    }

    [SecurityCritical]
    private void LoadAllPermissionSets()
    {
      if (this.m_permSetElement == null || this.m_permSetElement.InternalChildren == null)
        return;
      lock (PolicyLevel.InternalSyncObject)
      {
        while (this.m_permSetElement != null && this.m_permSetElement.InternalChildren.Count != 0)
        {
          SecurityElement child = (SecurityElement) this.m_permSetElement.Children[this.m_permSetElement.InternalChildren.Count - 1];
          this.m_permSetElement.InternalChildren.RemoveAt(this.m_permSetElement.InternalChildren.Count - 1);
          if (child.Tag.Equals("PermissionSet") && child.Attribute("class").Equals("System.Security.NamedPermissionSet"))
          {
            NamedPermissionSet namedPermissionSet = new NamedPermissionSet();
            namedPermissionSet.FromXmlNameOnly(child);
            if (namedPermissionSet.Name != null)
            {
              this.m_namedPermissionSets.Add((object) namedPermissionSet);
              try
              {
                namedPermissionSet.FromXml(child, false, true);
              }
              catch
              {
                this.m_namedPermissionSets.Remove((object) namedPermissionSet);
              }
            }
          }
        }
        this.m_permSetElement = (SecurityElement) null;
      }
    }

    [SecurityCritical]
    private ArrayList GenericResolve(Evidence evidence, out bool allConst)
    {
      CodeGroupStack codeGroupStack = new CodeGroupStack();
      CodeGroup rootCodeGroup = this.m_rootCodeGroup;
      if (rootCodeGroup == null)
        throw new PolicyException(Environment.GetResourceString("Policy_NonFullTrustAssembly"));
      codeGroupStack.Push(new CodeGroupStackFrame()
      {
        current = rootCodeGroup,
        parent = (CodeGroupStackFrame) null
      });
      ArrayList arrayList = new ArrayList();
      bool flag = false;
      allConst = true;
      Exception exception = (Exception) null;
      while (!codeGroupStack.IsEmpty())
      {
        CodeGroupStackFrame codeGroupStackFrame = codeGroupStack.Pop();
        FirstMatchCodeGroup current1 = codeGroupStackFrame.current as FirstMatchCodeGroup;
        UnionCodeGroup current2 = codeGroupStackFrame.current as UnionCodeGroup;
        if (!(codeGroupStackFrame.current.MembershipCondition is IConstantMembershipCondition) || current2 == null && current1 == null)
          allConst = false;
        try
        {
          codeGroupStackFrame.policy = PolicyManager.ResolveCodeGroup(codeGroupStackFrame.current, evidence);
        }
        catch (Exception ex)
        {
          if (exception == null)
            exception = ex;
        }
        if (codeGroupStackFrame.policy != null)
        {
          if ((codeGroupStackFrame.policy.Attributes & PolicyStatementAttribute.Exclusive) != PolicyStatementAttribute.Nothing)
          {
            if (flag)
              throw new PolicyException(Environment.GetResourceString("Policy_MultipleExclusive"));
            arrayList.RemoveRange(0, arrayList.Count);
            arrayList.Add((object) codeGroupStackFrame);
            flag = true;
          }
          if (!flag)
            arrayList.Add((object) codeGroupStackFrame);
        }
      }
      if (exception != null)
        throw exception;
      return arrayList;
    }

    private static string GenerateFriendlyName(string className, Hashtable classes)
    {
      if (classes.ContainsKey((object) className))
        return (string) classes[(object) className];
      System.Type type = System.Type.GetType(className, false, false);
      if (type != (System.Type) null && !type.IsVisible)
        type = (System.Type) null;
      if (type == (System.Type) null)
        return className;
      if (!classes.ContainsValue((object) type.Name))
      {
        classes.Add((object) className, (object) type.Name);
        return type.Name;
      }
      if (!classes.ContainsValue((object) type.FullName))
      {
        classes.Add((object) className, (object) type.FullName);
        return type.FullName;
      }
      classes.Add((object) className, (object) type.AssemblyQualifiedName);
      return type.AssemblyQualifiedName;
    }

    private SecurityElement NormalizeClassDeep(SecurityElement elem, Hashtable classes)
    {
      this.NormalizeClass(elem, classes);
      if (elem.InternalChildren != null && elem.InternalChildren.Count > 0)
      {
        foreach (SecurityElement child in elem.Children)
          this.NormalizeClassDeep(child, classes);
      }
      return elem;
    }

    private SecurityElement NormalizeClass(SecurityElement elem, Hashtable classes)
    {
      if (elem.m_lAttributes == null || elem.m_lAttributes.Count == 0)
        return elem;
      int count = elem.m_lAttributes.Count;
      int index = 0;
      while (index < count)
      {
        if (((string) elem.m_lAttributes[index]).Equals("class"))
        {
          string lAttribute = (string) elem.m_lAttributes[index + 1];
          elem.m_lAttributes[index + 1] = (object) PolicyLevel.GenerateFriendlyName(lAttribute, classes);
          break;
        }
        index += 2;
      }
      return elem;
    }

    private SecurityElement UnnormalizeClassDeep(SecurityElement elem, Hashtable classes)
    {
      this.UnnormalizeClass(elem, classes);
      if (elem.InternalChildren != null && elem.InternalChildren.Count > 0)
      {
        foreach (SecurityElement child in elem.Children)
          this.UnnormalizeClassDeep(child, classes);
      }
      return elem;
    }

    private SecurityElement UnnormalizeClass(SecurityElement elem, Hashtable classes)
    {
      if (classes == null || elem.m_lAttributes == null || elem.m_lAttributes.Count == 0)
        return elem;
      int count = elem.m_lAttributes.Count;
      int index = 0;
      while (index < count)
      {
        if (((string) elem.m_lAttributes[index]).Equals("class"))
        {
          string lAttribute = (string) elem.m_lAttributes[index + 1];
          string str = (string) classes[(object) lAttribute];
          if (str != null)
          {
            elem.m_lAttributes[index + 1] = (object) str;
            break;
          }
          break;
        }
        index += 2;
      }
      return elem;
    }
  }
}
