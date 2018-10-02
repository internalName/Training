// Decompiled with JetBrains decompiler
// Type: System.Resources.ResourceManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Resources
{
  /// <summary>
  ///   Представляет диспетчер ресурсов, обеспечивающий удобный доступ к ресурсам, связанным с языком и региональными параметрами, во время выполнения.
  /// 
  ///   Примечание по безопасности. Вызов методов в классе с недоверенными данными представляет угрозу безопасности.
  ///    Вызывайте методы только в классе с доверенными данными.
  ///    Дополнительные сведения см. в разделе Угрозы безопасности при работе с недоверенными данными.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ResourceManager
  {
    /// <summary>
    ///   Содержит номер, используемый для идентификации файлов ресурсов.
    /// </summary>
    public static readonly int MagicNumber = -1091581234;
    /// <summary>
    ///    Указывает версию заголовков файлов ресурсов, которую текущая реализация <see cref="T:System.Resources.ResourceManager" /> может интерпретировать и создавать.
    /// </summary>
    public static readonly int HeaderVersionNumber = 1;
    private static readonly Type _minResourceSet = typeof (ResourceSet);
    internal static readonly string ResReaderTypeName = typeof (ResourceReader).FullName;
    internal static readonly string ResSetTypeName = typeof (RuntimeResourceSet).FullName;
    internal static readonly string MscorlibName = typeof (ResourceReader).Assembly.FullName;
    internal static readonly int DEBUG = 0;
    /// <summary>
    ///   Указывает имя корневой папки файлов ресурсов, в которой класс <see cref="T:System.Resources.ResourceManager" /> ищет ресурсы.
    /// </summary>
    protected string BaseNameField;
    /// <summary>
    ///   Содержит объект <see cref="T:System.Collections.Hashtable" />, который возвращает сопоставление языков и региональных параметров с объектами <see cref="T:System.Resources.ResourceSet" />.
    /// </summary>
    [Obsolete("call InternalGetResourceSet instead")]
    protected Hashtable ResourceSets;
    [NonSerialized]
    private Dictionary<string, ResourceSet> _resourceSets;
    private string moduleDir;
    /// <summary>Задает главную сборку, содержащую ресурсы.</summary>
    protected Assembly MainAssembly;
    private Type _locationInfo;
    private Type _userResourceSet;
    private CultureInfo _neutralResourcesCulture;
    [NonSerialized]
    private ResourceManager.CultureNameResourceSetPair _lastUsedResourceCache;
    private bool _ignoreCase;
    private bool UseManifest;
    [OptionalField(VersionAdded = 1)]
    private bool UseSatelliteAssem;
    private static volatile Hashtable _installedSatelliteInfo;
    private static volatile bool _checkedConfigFile;
    [OptionalField]
    private UltimateResourceFallbackLocation _fallbackLoc;
    [OptionalField]
    private Version _satelliteContractVersion;
    [OptionalField]
    private bool _lookedForSatelliteContractVersion;
    [OptionalField(VersionAdded = 1)]
    private Assembly _callingAssembly;
    [OptionalField(VersionAdded = 4)]
    private RuntimeAssembly m_callingAssembly;
    [NonSerialized]
    private IResourceGroveler resourceGroveler;
    internal const string ResFileExtension = ".resources";
    internal const int ResFileExtensionLength = 10;
    private static volatile bool s_IsAppXModel;
    [NonSerialized]
    private bool _bUsingModernResourceManagement;
    [SecurityCritical]
    [NonSerialized]
    private WindowsRuntimeResourceManagerBase _WinRTResourceManager;
    [NonSerialized]
    private bool _PRIonAppXInitialized;
    [NonSerialized]
    private PRIExceptionInfo _PRIExceptionInfo;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Init()
    {
      this.m_callingAssembly = (RuntimeAssembly) Assembly.GetCallingAssembly();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.ResourceManager" /> со значениями по умолчанию.
    /// </summary>
    protected ResourceManager()
    {
      this.Init();
      this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
      this.resourceGroveler = (IResourceGroveler) new ManifestBasedResourceGroveler(new ResourceManager.ResourceManagerMediator(this));
    }

    private ResourceManager(string baseName, string resourceDir, Type usingResourceSet)
    {
      if (baseName == null)
        throw new ArgumentNullException(nameof (baseName));
      if (resourceDir == null)
        throw new ArgumentNullException(nameof (resourceDir));
      this.BaseNameField = baseName;
      this.moduleDir = resourceDir;
      this._userResourceSet = usingResourceSet;
      this.ResourceSets = new Hashtable();
      this._resourceSets = new Dictionary<string, ResourceSet>();
      this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
      this.UseManifest = false;
      this.resourceGroveler = (IResourceGroveler) new FileBasedResourceGroveler(new ResourceManager.ResourceManagerMediator(this));
      if (!FrameworkEventSource.IsInitialized || !FrameworkEventSource.Log.IsEnabled())
        return;
      CultureInfo invariantCulture = CultureInfo.InvariantCulture;
      string resourceFileName = this.GetResourceFileName(invariantCulture);
      if (this.resourceGroveler.HasNeutralResources(invariantCulture, resourceFileName))
        FrameworkEventSource.Log.ResourceManagerNeutralResourcesFound(this.BaseNameField, this.MainAssembly, resourceFileName);
      else
        FrameworkEventSource.Log.ResourceManagerNeutralResourcesNotFound(this.BaseNameField, this.MainAssembly, resourceFileName);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.ResourceManager" />, который ищет ресурсы, содержащиеся в файлах с указанным корневым именем, в данной сборке.
    /// </summary>
    /// <param name="baseName">
    ///   Корневое имя файла ресурсов без расширения, но включающее какое-либо полное имя пространства имен.
    ///    Например, имя корневой папки для файла ресурсов MyApplication.MyResource.en-US.resources будет MyApplication.MyResource.
    /// </param>
    /// <param name="assembly">Главная сборка для ресурсов.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="baseName" /> или параметра <paramref name="assembly" /> — <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ResourceManager(string baseName, Assembly assembly)
    {
      if (baseName == null)
        throw new ArgumentNullException(nameof (baseName));
      if ((Assembly) null == assembly)
        throw new ArgumentNullException(nameof (assembly));
      if (!(assembly is RuntimeAssembly))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
      this.MainAssembly = assembly;
      this.BaseNameField = baseName;
      this.SetAppXConfiguration();
      this.CommonAssemblyInit();
      this.m_callingAssembly = (RuntimeAssembly) Assembly.GetCallingAssembly();
      if (!(assembly == typeof (object).Assembly) || !((Assembly) this.m_callingAssembly != assembly))
        return;
      this.m_callingAssembly = (RuntimeAssembly) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.ResourceManager" />, который использует указанный класс <see cref="T:System.Resources.ResourceSet" /> для поиска ресурсов, содержащихся в файлах с указанным корневым именем, в данной сборке.
    /// </summary>
    /// <param name="baseName">
    ///   Корневое имя файла ресурсов без расширения, но включающее какое-либо полное имя пространства имен.
    ///    Например, имя корневой папки для файла ресурсов MyApplication.MyResource.en-US.resources будет MyApplication.MyResource.
    /// </param>
    /// <param name="assembly">Главная сборка для ресурсов.</param>
    /// <param name="usingResourceSet">
    ///   Тип пользовательского объекта <see cref="T:System.Resources.ResourceSet" /> для использования.
    ///    При значении <see langword="null" /> используется объект времени выполнения по умолчанию <see cref="T:System.Resources.ResourceSet" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="usingResourceset" /> не является производным от класса <see cref="T:System.Resources.ResourceSet" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="baseName" /> или параметра <paramref name="assembly" /> — <see langword="null" />.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ResourceManager(string baseName, Assembly assembly, Type usingResourceSet)
    {
      if (baseName == null)
        throw new ArgumentNullException(nameof (baseName));
      if ((Assembly) null == assembly)
        throw new ArgumentNullException(nameof (assembly));
      if (!(assembly is RuntimeAssembly))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
      this.MainAssembly = assembly;
      this.BaseNameField = baseName;
      if (usingResourceSet != (Type) null && usingResourceSet != ResourceManager._minResourceSet && !usingResourceSet.IsSubclassOf(ResourceManager._minResourceSet))
        throw new ArgumentException(Environment.GetResourceString("Arg_ResMgrNotResSet"), nameof (usingResourceSet));
      this._userResourceSet = usingResourceSet;
      this.CommonAssemblyInit();
      this.m_callingAssembly = (RuntimeAssembly) Assembly.GetCallingAssembly();
      if (!(assembly == typeof (object).Assembly) || !((Assembly) this.m_callingAssembly != assembly))
        return;
      this.m_callingAssembly = (RuntimeAssembly) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.ResourceManager" />, который ищет ресурсы в сопутствующих сборках, используя сведения из указанного объекта типа.
    /// </summary>
    /// <param name="resourceSource">
    ///   Тип, из которого диспетчер ресурсов получает все сведения, необходимые для поиска RESOURCES-файлов.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="resourceSource" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ResourceManager(Type resourceSource)
    {
      if ((Type) null == resourceSource)
        throw new ArgumentNullException(nameof (resourceSource));
      if ((object) (resourceSource as RuntimeType) == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      this._locationInfo = resourceSource;
      this.MainAssembly = this._locationInfo.Assembly;
      this.BaseNameField = resourceSource.Name;
      this.SetAppXConfiguration();
      this.CommonAssemblyInit();
      this.m_callingAssembly = (RuntimeAssembly) Assembly.GetCallingAssembly();
      if (!(this.MainAssembly == typeof (object).Assembly) || !((Assembly) this.m_callingAssembly != this.MainAssembly))
        return;
      this.m_callingAssembly = (RuntimeAssembly) null;
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this._resourceSets = (Dictionary<string, ResourceSet>) null;
      this.resourceGroveler = (IResourceGroveler) null;
      this._lastUsedResourceCache = (ResourceManager.CultureNameResourceSetPair) null;
    }

    [SecuritySafeCritical]
    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this._resourceSets = new Dictionary<string, ResourceSet>();
      this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
      ResourceManager.ResourceManagerMediator mediator = new ResourceManager.ResourceManagerMediator(this);
      this.resourceGroveler = !this.UseManifest ? (IResourceGroveler) new FileBasedResourceGroveler(mediator) : (IResourceGroveler) new ManifestBasedResourceGroveler(mediator);
      if ((Assembly) this.m_callingAssembly == (Assembly) null)
        this.m_callingAssembly = (RuntimeAssembly) this._callingAssembly;
      if (!this.UseManifest || this._neutralResourcesCulture != null)
        return;
      this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this._callingAssembly = (Assembly) this.m_callingAssembly;
      this.UseSatelliteAssem = this.UseManifest;
      this.ResourceSets = new Hashtable();
    }

    [SecuritySafeCritical]
    private void CommonAssemblyInit()
    {
      if (!this._bUsingModernResourceManagement)
      {
        this.UseManifest = true;
        this._resourceSets = new Dictionary<string, ResourceSet>();
        this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
        this._fallbackLoc = UltimateResourceFallbackLocation.MainAssembly;
        this.resourceGroveler = (IResourceGroveler) new ManifestBasedResourceGroveler(new ResourceManager.ResourceManagerMediator(this));
      }
      this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
      if (this._bUsingModernResourceManagement)
        return;
      if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled())
      {
        CultureInfo invariantCulture = CultureInfo.InvariantCulture;
        string resourceFileName = this.GetResourceFileName(invariantCulture);
        if (this.resourceGroveler.HasNeutralResources(invariantCulture, resourceFileName))
        {
          FrameworkEventSource.Log.ResourceManagerNeutralResourcesFound(this.BaseNameField, this.MainAssembly, resourceFileName);
        }
        else
        {
          string resName = resourceFileName;
          if (this._locationInfo != (Type) null && this._locationInfo.Namespace != null)
            resName = this._locationInfo.Namespace + Type.Delimiter.ToString() + resourceFileName;
          FrameworkEventSource.Log.ResourceManagerNeutralResourcesNotFound(this.BaseNameField, this.MainAssembly, resName);
        }
      }
      this.ResourceSets = new Hashtable();
    }

    /// <summary>
    ///   Возвращает имя корневой папки файлов ресурсов, в которой класс <see cref="T:System.Resources.ResourceManager" /> ищет ресурсы.
    /// </summary>
    /// <returns>
    ///   Имя корневой папки файлов ресурсов, в которой класс <see cref="T:System.Resources.ResourceManager" /> ищет ресурсы.
    /// </returns>
    public virtual string BaseName
    {
      get
      {
        return this.BaseNameField;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, которое указывает, позволяет ли диспетчер ресурсов выполнять поиск ресурсов с учетом регистра в методах <see cref="M:System.Resources.ResourceManager.GetString(System.String)" /> и <see cref="M:System.Resources.ResourceManager.GetObject(System.String)" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, чтобы не учитывать регистр при поиске ресурсов, в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool IgnoreCase
    {
      get
      {
        return this._ignoreCase;
      }
      set
      {
        this._ignoreCase = value;
      }
    }

    /// <summary>
    ///   Возвращает тип объекта набора ресурсов, используемый диспетчером ресурсов для построения объекта <see cref="T:System.Resources.ResourceSet" />.
    /// </summary>
    /// <returns>
    ///   Тип объекта набора ресурсов, используемый диспетчером ресурсов для построения объекта <see cref="T:System.Resources.ResourceSet" />.
    /// </returns>
    public virtual Type ResourceSetType
    {
      get
      {
        if (!(this._userResourceSet == (Type) null))
          return this._userResourceSet;
        return typeof (RuntimeResourceSet);
      }
    }

    /// <summary>
    ///   Возвращает или задает расположение, из которого извлекаются резервные ресурсы по умолчанию.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, которое указывает, где диспетчер ресурсов может выполнять поиск резервных ресурсов.
    /// </returns>
    protected UltimateResourceFallbackLocation FallbackLocation
    {
      get
      {
        return this._fallbackLoc;
      }
      set
      {
        this._fallbackLoc = value;
      }
    }

    /// <summary>
    ///   Сообщает диспетчеру ресурсов, что следует вызвать метод <see cref="M:System.Resources.ResourceSet.Close" /> на всех объектах <see cref="T:System.Resources.ResourceSet" /> и освободить все ресурсы.
    /// </summary>
    public virtual void ReleaseAllResources()
    {
      if (FrameworkEventSource.IsInitialized)
        FrameworkEventSource.Log.ResourceManagerReleasingResources(this.BaseNameField, this.MainAssembly);
      Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
      this._resourceSets = new Dictionary<string, ResourceSet>();
      this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
      lock (resourceSets)
      {
        IDictionaryEnumerator enumerator = (IDictionaryEnumerator) resourceSets.GetEnumerator();
        IDictionaryEnumerator dictionaryEnumerator = (IDictionaryEnumerator) null;
        if (this.ResourceSets != null)
          dictionaryEnumerator = this.ResourceSets.GetEnumerator();
        this.ResourceSets = new Hashtable();
        while (enumerator.MoveNext())
          ((ResourceSet) enumerator.Value).Close();
        if (dictionaryEnumerator == null)
          return;
        while (dictionaryEnumerator.MoveNext())
          ((ResourceSet) dictionaryEnumerator.Value).Close();
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Resources.ResourceManager" />, который ищет ресурсы в конкретном каталоге вместо манифеста сборки.
    /// </summary>
    /// <param name="baseName">
    ///   Имя корневой папки ресурсов.
    ///    Например, имя корневой папки для файла ресурсов "MyResource.en-US.resources" будет "MyResource".
    /// </param>
    /// <param name="resourceDir">
    ///   Имя папки, в которой производится поиск ресурсов.
    ///   <paramref name="resourceDir" /> может быть абсолютным или относительным путем из каталога приложения.
    /// </param>
    /// <param name="usingResourceSet">
    ///   Тип пользовательского объекта <see cref="T:System.Resources.ResourceSet" /> для использования.
    ///    При значении <see langword="null" /> используется объект времени выполнения по умолчанию <see cref="T:System.Resources.ResourceSet" />.
    /// </param>
    /// <returns>
    ///   Новый экземпляр диспетчера ресурсов, который выполняет поиск ресурсов в указанном каталоге вместо манифеста сборки.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="baseName" /> или параметра <paramref name="resourceDir" /> — <see langword="null" />.
    /// </exception>
    public static ResourceManager CreateFileBasedResourceManager(string baseName, string resourceDir, Type usingResourceSet)
    {
      return new ResourceManager(baseName, resourceDir, usingResourceSet);
    }

    /// <summary>
    ///   Создает имя файла ресурсов для заданного объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </summary>
    /// <param name="culture">
    ///   Объект языка и региональных параметров, для которого создается имя файла ресурсов.
    /// </param>
    /// <returns>
    ///   Имя, которое может быть использовано для файла ресурсов для заданного объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </returns>
    protected virtual string GetResourceFileName(CultureInfo culture)
    {
      StringBuilder stringBuilder = new StringBuilder((int) byte.MaxValue);
      stringBuilder.Append(this.BaseNameField);
      if (!culture.HasInvariantCultureName)
      {
        CultureInfo.VerifyCultureName(culture.Name, true);
        stringBuilder.Append('.');
        stringBuilder.Append(culture.Name);
      }
      stringBuilder.Append(".resources");
      return stringBuilder.ToString();
    }

    internal ResourceSet GetFirstResourceSet(CultureInfo culture)
    {
      if (this._neutralResourcesCulture != null && culture.Name == this._neutralResourcesCulture.Name)
        culture = CultureInfo.InvariantCulture;
      if (this._lastUsedResourceCache != null)
      {
        lock (this._lastUsedResourceCache)
        {
          if (culture.Name == this._lastUsedResourceCache.lastCultureName)
            return this._lastUsedResourceCache.lastResourceSet;
        }
      }
      Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
      ResourceSet resourceSet = (ResourceSet) null;
      if (resourceSets != null)
      {
        lock (resourceSets)
          resourceSets.TryGetValue(culture.Name, out resourceSet);
      }
      if (resourceSet == null)
        return (ResourceSet) null;
      if (this._lastUsedResourceCache != null)
      {
        lock (this._lastUsedResourceCache)
        {
          this._lastUsedResourceCache.lastCultureName = culture.Name;
          this._lastUsedResourceCache.lastResourceSet = resourceSet;
        }
      }
      return resourceSet;
    }

    /// <summary>
    ///   Извлекает набор ресурсов для определенного языка и региональных параметров.
    /// </summary>
    /// <param name="culture">
    ///   Язык и региональные параметры, ресурсы для которых необходимо получить.
    /// </param>
    /// <param name="createIfNotExists">
    ///   Значение <see langword="true" /> для загрузки набора ресурсов, если он еще не загружен; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="tryParents">
    ///   Значение <see langword="true" />, чтобы соответствующий ресурс загружался с использованием резервных ресурсов, если набор ресурсов не удается найти. Значение <see langword="false" /> для обхода процесса использования резервных ресурсов.
    ///    (См. раздел примeчаний.)
    /// </param>
    /// <returns>
    ///   Набор ресурсов для указанного языка и региональных параметров.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">
    ///   <paramref name="tryParents" /> — <see langword="true" />, не было найдено никаких возможных наборов ресурсов и нет ресурсов языка и региональных параметров по умолчанию.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public virtual ResourceSet GetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
      if (resourceSets != null)
      {
        lock (resourceSets)
        {
          ResourceSet resourceSet;
          if (resourceSets.TryGetValue(culture.Name, out resourceSet))
            return resourceSet;
        }
      }
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (this.UseManifest && culture.HasInvariantCultureName)
      {
        Stream manifestResourceStream = ((RuntimeAssembly) this.MainAssembly).GetManifestResourceStream(this._locationInfo, this.GetResourceFileName(culture), (Assembly) this.m_callingAssembly == this.MainAssembly, ref stackMark);
        if (createIfNotExists && manifestResourceStream != null)
        {
          ResourceSet resourceSet = ((ManifestBasedResourceGroveler) this.resourceGroveler).CreateResourceSet(manifestResourceStream, this.MainAssembly);
          ResourceManager.AddResourceSet(resourceSets, culture.Name, ref resourceSet);
          return resourceSet;
        }
      }
      return this.InternalGetResourceSet(culture, createIfNotExists, tryParents);
    }

    /// <summary>
    ///   Предоставляет реализацию для обнаружения набора ресурсов.
    /// </summary>
    /// <param name="culture">
    ///   Искомый объект языка и региональных параметров.
    /// </param>
    /// <param name="createIfNotExists">
    ///   Значение <see langword="true" /> для загрузки набора ресурсов, если он еще не загружен; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="tryParents">
    ///   Значение <see langword="true" /> для проверки родительских объектов <see cref="T:System.Globalization.CultureInfo" />, если невозможно загрузить набор ресурсов; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>Указанный набор ресурсов.</returns>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">
    ///   Главная сборка не содержит RESOURCES-файл, который необходим для поиска ресурса.
    /// </exception>
    /// <exception cref="T:System.ExecutionEngineException">
    ///   Произошла внутренняя ошибка во время выполнения.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">
    ///   Вспомогательную сборку, связанную с <paramref name="culture" /> не удалось найти.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    protected virtual ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalGetResourceSet(culture, createIfNotExists, tryParents, ref stackMark);
    }

    [SecurityCritical]
    private ResourceSet InternalGetResourceSet(CultureInfo requestedCulture, bool createIfNotExists, bool tryParents, ref StackCrawlMark stackMark)
    {
      Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
      ResourceSet rs = (ResourceSet) null;
      CultureInfo cultureInfo1 = (CultureInfo) null;
      lock (resourceSets)
      {
        if (resourceSets.TryGetValue(requestedCulture.Name, out rs))
        {
          if (FrameworkEventSource.IsInitialized)
            FrameworkEventSource.Log.ResourceManagerFoundResourceSetInCache(this.BaseNameField, this.MainAssembly, requestedCulture.Name);
          return rs;
        }
      }
      ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(requestedCulture, this._neutralResourcesCulture, tryParents);
      foreach (CultureInfo culture in resourceFallbackManager)
      {
        if (FrameworkEventSource.IsInitialized)
          FrameworkEventSource.Log.ResourceManagerLookingForResourceSet(this.BaseNameField, this.MainAssembly, culture.Name);
        lock (resourceSets)
        {
          if (resourceSets.TryGetValue(culture.Name, out rs))
          {
            if (FrameworkEventSource.IsInitialized)
              FrameworkEventSource.Log.ResourceManagerFoundResourceSetInCache(this.BaseNameField, this.MainAssembly, culture.Name);
            if (requestedCulture != culture)
            {
              cultureInfo1 = culture;
              break;
            }
            break;
          }
        }
        rs = this.resourceGroveler.GrovelForResourceSet(culture, resourceSets, tryParents, createIfNotExists, ref stackMark);
        if (rs != null)
        {
          cultureInfo1 = culture;
          break;
        }
      }
      if (rs != null && cultureInfo1 != null)
      {
        foreach (CultureInfo cultureInfo2 in resourceFallbackManager)
        {
          ResourceManager.AddResourceSet(resourceSets, cultureInfo2.Name, ref rs);
          if (cultureInfo2 == cultureInfo1)
            break;
        }
      }
      return rs;
    }

    private static void AddResourceSet(Dictionary<string, ResourceSet> localResourceSets, string cultureName, ref ResourceSet rs)
    {
      lock (localResourceSets)
      {
        ResourceSet resourceSet;
        if (localResourceSets.TryGetValue(cultureName, out resourceSet))
        {
          if (resourceSet == rs)
            return;
          if (!localResourceSets.ContainsValue(rs))
            rs.Dispose();
          rs = resourceSet;
        }
        else
          localResourceSets.Add(cultureName, rs);
      }
    }

    /// <summary>
    ///   Возвращает версию, указанную атрибутом <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> в заданной сборке.
    /// </summary>
    /// <param name="a">
    ///   Сборка для проверки атрибута <see cref="T:System.Resources.SatelliteContractVersionAttribute" />.
    /// </param>
    /// <returns>
    ///   Версия сопутствующего контракта данной сборки либо значение <see langword="null" />, если версия не была найдена.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="T:System.Version" /> Найденные в сборке <paramref name="a" /> является недопустимым.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="a" /> имеет значение <see langword="null" />.
    /// </exception>
    protected static Version GetSatelliteContractVersion(Assembly a)
    {
      if (a == (Assembly) null)
        throw new ArgumentNullException(nameof (a), Environment.GetResourceString("ArgumentNull_Assembly"));
      string version = (string) null;
      if (a.ReflectionOnly)
      {
        foreach (CustomAttributeData customAttribute in (IEnumerable<CustomAttributeData>) CustomAttributeData.GetCustomAttributes(a))
        {
          if (customAttribute.Constructor.DeclaringType == typeof (SatelliteContractVersionAttribute))
          {
            version = (string) customAttribute.ConstructorArguments[0].Value;
            break;
          }
        }
        if (version == null)
          return (Version) null;
      }
      else
      {
        object[] customAttributes = a.GetCustomAttributes(typeof (SatelliteContractVersionAttribute), false);
        if (customAttributes.Length == 0)
          return (Version) null;
        version = ((SatelliteContractVersionAttribute) customAttributes[0]).Version;
      }
      try
      {
        return new Version(version);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        if (a == typeof (object).Assembly)
          return (Version) null;
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidSatelliteContract_Asm_Ver", (object) a.ToString(), (object) version), (Exception) ex);
      }
    }

    /// <summary>
    ///   Возвращает сведения, относящиеся к конкретному языку и региональным параметрам, для ресурсов главной сборки по умолчанию путем получения значения атрибута <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> в указанной сборке.
    /// </summary>
    /// <param name="a">
    ///   Сборка, для которой требуется получить сведения, относящиеся к конкретному языку и региональным параметрам.
    /// </param>
    /// <returns>
    ///   Язык и региональные параметры из атрибута <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" />, если они найдены; в противном случае — инвариантный язык и региональные параметры.
    /// </returns>
    [SecuritySafeCritical]
    protected static CultureInfo GetNeutralResourcesLanguage(Assembly a)
    {
      UltimateResourceFallbackLocation fallbackLocation = UltimateResourceFallbackLocation.MainAssembly;
      return ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(a, ref fallbackLocation);
    }

    internal static bool CompareNames(string asmTypeName1, string typeName2, AssemblyName asmName2)
    {
      int startIndex = asmTypeName1.IndexOf(',');
      if ((startIndex == -1 ? asmTypeName1.Length : startIndex) != typeName2.Length || string.Compare(asmTypeName1, 0, typeName2, 0, typeName2.Length, StringComparison.Ordinal) != 0)
        return false;
      if (startIndex == -1)
        return true;
      do
        ;
      while (char.IsWhiteSpace(asmTypeName1[++startIndex]));
      AssemblyName assemblyName = new AssemblyName(asmTypeName1.Substring(startIndex));
      if (string.Compare(assemblyName.Name, asmName2.Name, StringComparison.OrdinalIgnoreCase) != 0)
        return false;
      if (string.Compare(assemblyName.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) == 0)
        return true;
      if (assemblyName.CultureInfo != null && asmName2.CultureInfo != null && assemblyName.CultureInfo.LCID != asmName2.CultureInfo.LCID)
        return false;
      byte[] publicKeyToken1 = assemblyName.GetPublicKeyToken();
      byte[] publicKeyToken2 = asmName2.GetPublicKeyToken();
      if (publicKeyToken1 != null && publicKeyToken2 != null)
      {
        if (publicKeyToken1.Length != publicKeyToken2.Length)
          return false;
        for (int index = 0; index < publicKeyToken1.Length; ++index)
        {
          if ((int) publicKeyToken1[index] != (int) publicKeyToken2[index])
            return false;
        }
      }
      return true;
    }

    [SecuritySafeCritical]
    private string GetStringFromPRI(string stringName, string startingCulture, string neutralResourcesCulture)
    {
      if (stringName.Length == 0)
        return (string) null;
      return this._WinRTResourceManager.GetString(stringName, string.IsNullOrEmpty(startingCulture) ? (string) null : startingCulture, string.IsNullOrEmpty(neutralResourcesCulture) ? (string) null : neutralResourcesCulture);
    }

    [SecurityCritical]
    internal static WindowsRuntimeResourceManagerBase GetWinRTResourceManager()
    {
      return (WindowsRuntimeResourceManagerBase) Activator.CreateInstance(Type.GetType("System.Resources.WindowsRuntimeResourceManager, System.Runtime.WindowsRuntime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", true), true);
    }

    [SecuritySafeCritical]
    private bool ShouldUseSatelliteAssemblyResourceLookupUnderAppX(RuntimeAssembly resourcesAssembly)
    {
      return resourcesAssembly.IsFrameworkAssembly();
    }

    [SecuritySafeCritical]
    private void SetAppXConfiguration()
    {
      bool flag1 = false;
      RuntimeAssembly resourcesAssembly = (RuntimeAssembly) this.MainAssembly;
      if ((Assembly) resourcesAssembly == (Assembly) null)
        resourcesAssembly = this.m_callingAssembly;
      if (!((Assembly) resourcesAssembly != (Assembly) null) || !((Assembly) resourcesAssembly != typeof (object).Assembly) || (!AppDomain.IsAppXModel() || AppDomain.IsAppXNGen))
        return;
      ResourceManager.s_IsAppXModel = true;
      string reswFilename = (this._locationInfo == (Type) null ? this.BaseNameField : this._locationInfo.FullName) ?? string.Empty;
      WindowsRuntimeResourceManagerBase resourceManagerBase = (WindowsRuntimeResourceManagerBase) null;
      bool flag2 = false;
      if (AppDomain.IsAppXDesignMode())
      {
        resourceManagerBase = ResourceManager.GetWinRTResourceManager();
        try
        {
          PRIExceptionInfo exceptionInfo;
          flag2 = resourceManagerBase.Initialize(resourcesAssembly.Location, reswFilename, out exceptionInfo);
          flag1 = !flag2;
        }
        catch (Exception ex)
        {
          flag1 = true;
          if (ex.IsTransient)
            throw;
        }
      }
      if (flag1)
        return;
      this._bUsingModernResourceManagement = !this.ShouldUseSatelliteAssemblyResourceLookupUnderAppX(resourcesAssembly);
      if (!this._bUsingModernResourceManagement)
        return;
      if (resourceManagerBase != null & flag2)
      {
        this._WinRTResourceManager = resourceManagerBase;
        this._PRIonAppXInitialized = true;
      }
      else
      {
        this._WinRTResourceManager = ResourceManager.GetWinRTResourceManager();
        try
        {
          this._PRIonAppXInitialized = this._WinRTResourceManager.Initialize(resourcesAssembly.Location, reswFilename, out this._PRIExceptionInfo);
        }
        catch (FileNotFoundException ex)
        {
        }
        catch (Exception ex)
        {
          if (ex.HResult == -2147009761)
            return;
          throw;
        }
      }
    }

    /// <summary>Возвращает значение указанного строкового ресурса.</summary>
    /// <param name="name">Имя извлекаемого ресурса.</param>
    /// <returns>
    ///   Значение ресурса, локализованное для языка и региональных параметров текущего пользовательского интерфейса вызывающего объекта, или значение <see langword="null" />, если не удается найти <paramref name="name" /> в наборе ресурсов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Значение указанного ресурса не является строкой.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">
    ///   Найдено никакого подходящего набора ресурсов, и нет ресурсов для культуры по умолчанию.
    ///    Сведения о способах обработки этого исключения см. в разделе «Обработка MissingManifestResourceException и MissingSatelliteAssemblyException исключений» в <see cref="T:System.Resources.ResourceManager" /> разделе, посвященном классу.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">
    ///   Ресурсы для культуры по умолчанию находятся во вспомогательную сборку, не найден.
    ///    Сведения о способах обработки этого исключения см. в разделе «Обработка MissingManifestResourceException и MissingSatelliteAssemblyException исключений» в <see cref="T:System.Resources.ResourceManager" /> разделе, посвященном классу.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual string GetString(string name)
    {
      return this.GetString(name, (CultureInfo) null);
    }

    /// <summary>
    ///   Возвращает значение строкового ресурса, локализованного для указанного языка и региональных параметров.
    /// </summary>
    /// <param name="name">Имя извлекаемого ресурса.</param>
    /// <param name="culture">
    ///   Объект, предоставляющий язык и региональные параметры, для которых локализуется ресурс.
    /// </param>
    /// <returns>
    ///   Значение ресурса, локализованное для указанного языка и региональных параметров, или значение <see langword="null" />, если не удается найти <paramref name="name" /> в наборе ресурсов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Значение указанного ресурса не является строкой.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">
    ///   Найдено никакого подходящего набора ресурсов, и нет ресурсов для культуры по умолчанию.
    ///    Сведения о способах обработки этого исключения см. в разделе «Обработка MissingManifestResourceException и MissingSatelliteAssemblyException исключений» в <see cref="T:System.Resources.ResourceManager" /> разделе, посвященном классу.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">
    ///   Ресурсы для культуры по умолчанию находятся во вспомогательную сборку, не найден.
    ///    Сведения о способах обработки этого исключения см. в разделе «Обработка MissingManifestResourceException и MissingSatelliteAssemblyException исключений» в <see cref="T:System.Resources.ResourceManager" /> разделе, посвященном классу.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual string GetString(string name, CultureInfo culture)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (ResourceManager.s_IsAppXModel && culture == CultureInfo.CurrentUICulture)
        culture = (CultureInfo) null;
      if (this._bUsingModernResourceManagement)
      {
        if (this._PRIonAppXInitialized)
          return this.GetStringFromPRI(name, culture == null ? (string) null : culture.Name, this._neutralResourcesCulture.Name);
        if (this._PRIExceptionInfo != null && this._PRIExceptionInfo._PackageSimpleName != null && this._PRIExceptionInfo._ResWFile != null)
          throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_ResWFileNotLoaded", (object) this._PRIExceptionInfo._ResWFile, (object) this._PRIExceptionInfo._PackageSimpleName));
        throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_NoPRIresources"));
      }
      if (culture == null)
        culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
      if (FrameworkEventSource.IsInitialized)
        FrameworkEventSource.Log.ResourceManagerLookupStarted(this.BaseNameField, this.MainAssembly, culture.Name);
      ResourceSet resourceSet1 = this.GetFirstResourceSet(culture);
      if (resourceSet1 != null)
      {
        string str = resourceSet1.GetString(name, this._ignoreCase);
        if (str != null)
          return str;
      }
      foreach (CultureInfo culture1 in new ResourceFallbackManager(culture, this._neutralResourcesCulture, true))
      {
        ResourceSet resourceSet2 = this.InternalGetResourceSet(culture1, true, true);
        if (resourceSet2 != null)
        {
          if (resourceSet2 != resourceSet1)
          {
            string str = resourceSet2.GetString(name, this._ignoreCase);
            if (str != null)
            {
              if (this._lastUsedResourceCache != null)
              {
                lock (this._lastUsedResourceCache)
                {
                  this._lastUsedResourceCache.lastCultureName = culture1.Name;
                  this._lastUsedResourceCache.lastResourceSet = resourceSet2;
                }
              }
              return str;
            }
            resourceSet1 = resourceSet2;
          }
        }
        else
          break;
      }
      if (FrameworkEventSource.IsInitialized)
        FrameworkEventSource.Log.ResourceManagerLookupFailed(this.BaseNameField, this.MainAssembly, culture.Name);
      return (string) null;
    }

    /// <summary>
    ///   Возвращает значение указанного нестрокового ресурса.
    /// </summary>
    /// <param name="name">Имя получаемого ресурса.</param>
    /// <returns>
    ///   Значение ресурса, локализованного для текущих настроек языка и региональных параметров вызывающего объекта.
    ///    Если соответствующий набор ресурсов существует, но параметр <paramref name="name" /> не найден, то метод возвращает значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">
    ///   Найдено никаких возможных наборов локализованные ресурсы, и нет ресурсов языка и региональных параметров по умолчанию.
    ///    Сведения о способах обработки этого исключения см. в разделе «Обработка MissingManifestResourceException и MissingSatelliteAssemblyException исключений» в <see cref="T:System.Resources.ResourceManager" /> разделе, посвященном классу.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">
    ///   Ресурсы для культуры по умолчанию находятся во вспомогательную сборку, не найден.
    ///    Сведения о способах обработки этого исключения см. в разделе «Обработка MissingManifestResourceException и MissingSatelliteAssemblyException исключений» в <see cref="T:System.Resources.ResourceManager" /> разделе, посвященном классу.
    /// </exception>
    public virtual object GetObject(string name)
    {
      return this.GetObject(name, (CultureInfo) null, true);
    }

    /// <summary>
    ///   Возвращает значение указанного нестрокового ресурса, локализованного для указанного языка и региональных параметров.
    /// </summary>
    /// <param name="name">Имя получаемого ресурса.</param>
    /// <param name="culture">
    ///   Язык и региональные параметры, для которых локализуется этот ресурс.
    ///    Если ресурс для языка и региональных параметров не локализован, диспетчер ресурсов использует резервные правила для поиска подходящего ресурса.
    /// 
    ///   Если это значение равно <see langword="null" />, объект <see cref="T:System.Globalization.CultureInfo" /> получается с помощью свойства <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" />.
    /// </param>
    /// <returns>
    ///   Значение ресурса, локализованного для указанного языка и региональных параметров.
    ///    Если соответствующий набор ресурсов существует, но параметр <paramref name="name" /> не найден, то метод возвращает значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">
    ///   Не было найдено никакого подходящего набора ресурсов, и нет ресурсов языка и региональных параметров по умолчанию.
    ///    Сведения о способах обработки этого исключения см. в разделе «Обработка MissingManifestResourceException и MissingSatelliteAssemblyException исключений» в <see cref="T:System.Resources.ResourceManager" /> разделе, посвященном классу.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">
    ///   Ресурсы для культуры по умолчанию находятся во вспомогательную сборку, не найден.
    ///    Сведения о способах обработки этого исключения см. в разделе «Обработка MissingManifestResourceException и MissingSatelliteAssemblyException исключений» в <see cref="T:System.Resources.ResourceManager" /> разделе, посвященном классу.
    /// </exception>
    public virtual object GetObject(string name, CultureInfo culture)
    {
      return this.GetObject(name, culture, true);
    }

    private object GetObject(string name, CultureInfo culture, bool wrapUnmanagedMemStream)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (ResourceManager.s_IsAppXModel && culture == CultureInfo.CurrentUICulture)
        culture = (CultureInfo) null;
      if (culture == null)
        culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
      if (FrameworkEventSource.IsInitialized)
        FrameworkEventSource.Log.ResourceManagerLookupStarted(this.BaseNameField, this.MainAssembly, culture.Name);
      ResourceSet resourceSet1 = this.GetFirstResourceSet(culture);
      if (resourceSet1 != null)
      {
        object obj = resourceSet1.GetObject(name, this._ignoreCase);
        if (obj != null)
        {
          UnmanagedMemoryStream stream = obj as UnmanagedMemoryStream;
          if (stream != null & wrapUnmanagedMemStream)
            return (object) new UnmanagedMemoryStreamWrapper(stream);
          return obj;
        }
      }
      foreach (CultureInfo culture1 in new ResourceFallbackManager(culture, this._neutralResourcesCulture, true))
      {
        ResourceSet resourceSet2 = this.InternalGetResourceSet(culture1, true, true);
        if (resourceSet2 != null)
        {
          if (resourceSet2 != resourceSet1)
          {
            object obj = resourceSet2.GetObject(name, this._ignoreCase);
            if (obj != null)
            {
              if (this._lastUsedResourceCache != null)
              {
                lock (this._lastUsedResourceCache)
                {
                  this._lastUsedResourceCache.lastCultureName = culture1.Name;
                  this._lastUsedResourceCache.lastResourceSet = resourceSet2;
                }
              }
              UnmanagedMemoryStream stream = obj as UnmanagedMemoryStream;
              if (stream != null & wrapUnmanagedMemStream)
                return (object) new UnmanagedMemoryStreamWrapper(stream);
              return obj;
            }
            resourceSet1 = resourceSet2;
          }
        }
        else
          break;
      }
      if (FrameworkEventSource.IsInitialized)
        FrameworkEventSource.Log.ResourceManagerLookupFailed(this.BaseNameField, this.MainAssembly, culture.Name);
      return (object) null;
    }

    /// <summary>
    ///   Возвращает объект потока неуправляемой памяти из заданного ресурса.
    /// </summary>
    /// <param name="name">Имя ресурса.</param>
    /// <returns>
    ///   Объект потока неуправляемой памяти, представляющий ресурс.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Значение указанного ресурса не <see cref="T:System.IO.MemoryStream" /> объекта.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">
    ///   Не найдено никаких возможных наборов ресурсов, и нет ресурсов по умолчанию.
    ///    Сведения о способах обработки этого исключения см. в разделе «Обработка MissingManifestResourceException и MissingSatelliteAssemblyException исключений» в <see cref="T:System.Resources.ResourceManager" /> разделе, посвященном классу.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">
    ///   Ресурсы для культуры по умолчанию находятся во вспомогательную сборку, не найден.
    ///    Сведения о способах обработки этого исключения см. в разделе «Обработка MissingManifestResourceException и MissingSatelliteAssemblyException исключений» в <see cref="T:System.Resources.ResourceManager" /> разделе, посвященном классу.
    /// </exception>
    [ComVisible(false)]
    public UnmanagedMemoryStream GetStream(string name)
    {
      return this.GetStream(name, (CultureInfo) null);
    }

    /// <summary>
    ///   Возвращает объект потока неуправляемой памяти из заданного ресурса, используя заданный язык и региональные параметры.
    /// </summary>
    /// <param name="name">Имя ресурса.</param>
    /// <param name="culture">
    ///   Объект, задающий язык и региональные параметры для использования при поиске ресурса.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, используется язык и региональные параметры для текущего потока.
    /// </param>
    /// <returns>
    ///   Объект потока неуправляемой памяти, представляющий ресурс.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Значение указанного ресурса не <see cref="T:System.IO.MemoryStream" /> объекта.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">
    ///   Не найдено никаких возможных наборов ресурсов, и нет ресурсов по умолчанию.
    ///    Сведения о способах обработки этого исключения см. в разделе «Обработка MissingManifestResourceException и MissingSatelliteAssemblyException исключений» в <see cref="T:System.Resources.ResourceManager" /> разделе, посвященном классу.
    /// </exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">
    ///   Ресурсы для культуры по умолчанию находятся во вспомогательную сборку, не найден.
    ///    Сведения о способах обработки этого исключения см. в разделе «Обработка MissingManifestResourceException и MissingSatelliteAssemblyException исключений» в <see cref="T:System.Resources.ResourceManager" /> разделе, посвященном классу.
    /// </exception>
    [ComVisible(false)]
    public UnmanagedMemoryStream GetStream(string name, CultureInfo culture)
    {
      object obj = this.GetObject(name, culture, false);
      UnmanagedMemoryStream unmanagedMemoryStream = obj as UnmanagedMemoryStream;
      if (unmanagedMemoryStream == null && obj != null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotStream_Name", (object) name));
      return unmanagedMemoryStream;
    }

    [SecurityCritical]
    private bool TryLookingForSatellite(CultureInfo lookForCulture)
    {
      if (!ResourceManager._checkedConfigFile)
      {
        lock (this)
        {
          if (!ResourceManager._checkedConfigFile)
          {
            ResourceManager._checkedConfigFile = true;
            ResourceManager._installedSatelliteInfo = this.GetSatelliteAssembliesFromConfig();
          }
        }
      }
      if (ResourceManager._installedSatelliteInfo == null)
        return true;
      string[] array = (string[]) ResourceManager._installedSatelliteInfo[(object) this.MainAssembly.FullName];
      if (array == null)
        return true;
      int num = Array.IndexOf<string>(array, lookForCulture.Name);
      if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled())
      {
        if (num < 0)
          FrameworkEventSource.Log.ResourceManagerCultureNotFoundInConfigFile(this.BaseNameField, this.MainAssembly, lookForCulture.Name);
        else
          FrameworkEventSource.Log.ResourceManagerCultureFoundInConfigFile(this.BaseNameField, this.MainAssembly, lookForCulture.Name);
      }
      return num >= 0;
    }

    [SecurityCritical]
    private Hashtable GetSatelliteAssembliesFromConfig()
    {
      string configurationFileInternal = AppDomain.CurrentDomain.FusionStore.ConfigurationFileInternal;
      if (configurationFileInternal == null)
        return (Hashtable) null;
      if (configurationFileInternal.Length >= 2 && ((int) configurationFileInternal[1] == (int) Path.VolumeSeparatorChar || (int) configurationFileInternal[0] == (int) Path.DirectorySeparatorChar && (int) configurationFileInternal[1] == (int) Path.DirectorySeparatorChar) && !File.InternalExists(configurationFileInternal))
        return (Hashtable) null;
      ConfigTreeParser configTreeParser = new ConfigTreeParser();
      string configPath = "/configuration/satelliteassemblies";
      ConfigNode configNode = (ConfigNode) null;
      try
      {
        configNode = configTreeParser.Parse(configurationFileInternal, configPath, true);
      }
      catch (Exception ex)
      {
      }
      if (configNode == null)
        return (Hashtable) null;
      Hashtable hashtable = new Hashtable((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
      foreach (ConfigNode child1 in configNode.Children)
      {
        if (!string.Equals(child1.Name, "assembly"))
          throw new ApplicationException(Environment.GetResourceString("XMLSyntax_InvalidSyntaxSatAssemTag", (object) Path.GetFileName(configurationFileInternal), (object) child1.Name));
        if (child1.Attributes.Count == 0)
          throw new ApplicationException(Environment.GetResourceString("XMLSyntax_InvalidSyntaxSatAssemTagNoAttr", (object) Path.GetFileName(configurationFileInternal)));
        DictionaryEntry attribute = child1.Attributes[0];
        string str = (string) attribute.Value;
        if (!object.Equals(attribute.Key, (object) "name") || string.IsNullOrEmpty(str) || child1.Attributes.Count > 1)
          throw new ApplicationException(Environment.GetResourceString("XMLSyntax_InvalidSyntaxSatAssemTagBadAttr", (object) Path.GetFileName(configurationFileInternal), attribute.Key, attribute.Value));
        ArrayList arrayList = new ArrayList(5);
        foreach (ConfigNode child2 in child1.Children)
        {
          if (child2.Value != null)
            arrayList.Add((object) child2.Value);
        }
        string[] strArray = new string[arrayList.Count];
        for (int index = 0; index < strArray.Length; ++index)
        {
          string cultureName = (string) arrayList[index];
          strArray[index] = cultureName;
          if (FrameworkEventSource.IsInitialized)
            FrameworkEventSource.Log.ResourceManagerAddingCultureFromConfigFile(this.BaseNameField, this.MainAssembly, cultureName);
        }
        hashtable.Add((object) str, (object) strArray);
      }
      return hashtable;
    }

    internal class CultureNameResourceSetPair
    {
      public string lastCultureName;
      public ResourceSet lastResourceSet;
    }

    internal class ResourceManagerMediator
    {
      private ResourceManager _rm;

      internal ResourceManagerMediator(ResourceManager rm)
      {
        if (rm == null)
          throw new ArgumentNullException(nameof (rm));
        this._rm = rm;
      }

      internal string ModuleDir
      {
        get
        {
          return this._rm.moduleDir;
        }
      }

      internal Type LocationInfo
      {
        get
        {
          return this._rm._locationInfo;
        }
      }

      internal Type UserResourceSet
      {
        get
        {
          return this._rm._userResourceSet;
        }
      }

      internal string BaseNameField
      {
        get
        {
          return this._rm.BaseNameField;
        }
      }

      internal CultureInfo NeutralResourcesCulture
      {
        get
        {
          return this._rm._neutralResourcesCulture;
        }
        set
        {
          this._rm._neutralResourcesCulture = value;
        }
      }

      internal string GetResourceFileName(CultureInfo culture)
      {
        return this._rm.GetResourceFileName(culture);
      }

      internal bool LookedForSatelliteContractVersion
      {
        get
        {
          return this._rm._lookedForSatelliteContractVersion;
        }
        set
        {
          this._rm._lookedForSatelliteContractVersion = value;
        }
      }

      internal Version SatelliteContractVersion
      {
        get
        {
          return this._rm._satelliteContractVersion;
        }
        set
        {
          this._rm._satelliteContractVersion = value;
        }
      }

      internal Version ObtainSatelliteContractVersion(Assembly a)
      {
        return ResourceManager.GetSatelliteContractVersion(a);
      }

      internal UltimateResourceFallbackLocation FallbackLoc
      {
        get
        {
          return this._rm.FallbackLocation;
        }
        set
        {
          this._rm._fallbackLoc = value;
        }
      }

      internal RuntimeAssembly CallingAssembly
      {
        get
        {
          return this._rm.m_callingAssembly;
        }
      }

      internal RuntimeAssembly MainAssembly
      {
        get
        {
          return (RuntimeAssembly) this._rm.MainAssembly;
        }
      }

      internal string BaseName
      {
        get
        {
          return this._rm.BaseName;
        }
      }

      [SecurityCritical]
      internal bool TryLookingForSatellite(CultureInfo lookForCulture)
      {
        return this._rm.TryLookingForSatellite(lookForCulture);
      }
    }
  }
}
