// Decompiled with JetBrains decompiler
// Type: System.AppDomainSetup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Deployment.Internal.Isolation.Manifest;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;

namespace System
{
  /// <summary>
  ///   Предоставляет сведения о привязке сборок, которые могут быть добавлены в экземпляр класса <see cref="T:System.AppDomain" />.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComVisible(true)]
  [Serializable]
  public sealed class AppDomainSetup : IAppDomainSetup
  {
    private string[] _Entries;
    private LoaderOptimization _LoaderOptimization;
    private string _AppBase;
    [OptionalField(VersionAdded = 2)]
    private AppDomainInitializer _AppDomainInitializer;
    [OptionalField(VersionAdded = 2)]
    private string[] _AppDomainInitializerArguments;
    [OptionalField(VersionAdded = 2)]
    private ActivationArguments _ActivationArguments;
    [OptionalField(VersionAdded = 2)]
    private string _ApplicationTrust;
    [OptionalField(VersionAdded = 2)]
    private byte[] _ConfigurationBytes;
    [OptionalField(VersionAdded = 3)]
    private bool _DisableInterfaceCache;
    [OptionalField(VersionAdded = 4)]
    private string _AppDomainManagerAssembly;
    [OptionalField(VersionAdded = 4)]
    private string _AppDomainManagerType;
    [OptionalField(VersionAdded = 4)]
    private string[] _AptcaVisibleAssemblies;
    [OptionalField(VersionAdded = 4)]
    private Dictionary<string, object> _CompatFlags;
    [OptionalField(VersionAdded = 5)]
    private string _TargetFrameworkName;
    [NonSerialized]
    internal AppDomainSortingSetupInfo _AppDomainSortingSetupInfo;
    [OptionalField(VersionAdded = 5)]
    private bool _CheckedForTargetFrameworkName;
    [OptionalField(VersionAdded = 5)]
    private bool _UseRandomizedStringHashing;

    [SecuritySafeCritical]
    internal AppDomainSetup(AppDomainSetup copy, bool copyDomainBoundData)
    {
      string[] strArray1 = this.Value;
      if (copy != null)
      {
        string[] strArray2 = copy.Value;
        int length1 = this._Entries.Length;
        int length2 = strArray2.Length;
        int num = length2 < length1 ? length2 : length1;
        for (int index = 0; index < num; ++index)
          strArray1[index] = strArray2[index];
        if (num < length1)
        {
          for (int index = num; index < length1; ++index)
            strArray1[index] = (string) null;
        }
        this._LoaderOptimization = copy._LoaderOptimization;
        this._AppDomainInitializerArguments = copy.AppDomainInitializerArguments;
        this._ActivationArguments = copy.ActivationArguments;
        this._ApplicationTrust = copy._ApplicationTrust;
        this._AppDomainInitializer = !copyDomainBoundData ? (AppDomainInitializer) null : copy.AppDomainInitializer;
        this._ConfigurationBytes = copy.GetConfigurationBytes();
        this._DisableInterfaceCache = copy._DisableInterfaceCache;
        this._AppDomainManagerAssembly = copy.AppDomainManagerAssembly;
        this._AppDomainManagerType = copy.AppDomainManagerType;
        this._AptcaVisibleAssemblies = copy.PartialTrustVisibleAssemblies;
        if (copy._CompatFlags != null)
          this.SetCompatibilitySwitches((IEnumerable<string>) copy._CompatFlags.Keys);
        if (copy._AppDomainSortingSetupInfo != null)
          this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo(copy._AppDomainSortingSetupInfo);
        this._TargetFrameworkName = copy._TargetFrameworkName;
        this._UseRandomizedStringHashing = copy._UseRandomizedStringHashing;
      }
      else
        this._LoaderOptimization = LoaderOptimization.NotSpecified;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AppDomainSetup" />.
    /// </summary>
    public AppDomainSetup()
    {
      this._LoaderOptimization = LoaderOptimization.NotSpecified;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AppDomainSetup" /> заданным контекстом активации для использования при активации домена приложения на основе манифеста.
    /// </summary>
    /// <param name="activationContext">
    ///   Контекст активации, который необходимо использовать для текущего домена приложения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="activationContext" /> имеет значение <see langword="null" />.
    /// </exception>
    public AppDomainSetup(ActivationContext activationContext)
      : this(new ActivationArguments(activationContext))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AppDomainSetup" /> с заданными аргументами активации, необходимыми для активации домена приложения на основе манифеста.
    /// </summary>
    /// <param name="activationArguments">
    ///   Объект, определяющий сведения, необходимые для активации нового домена приложения на основе манифеста.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="activationArguments" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public AppDomainSetup(ActivationArguments activationArguments)
    {
      if (activationArguments == null)
        throw new ArgumentNullException(nameof (activationArguments));
      this._LoaderOptimization = LoaderOptimization.NotSpecified;
      this.ActivationArguments = activationArguments;
      string entryPointFullPath = CmsUtils.GetEntryPointFullPath(activationArguments);
      if (!string.IsNullOrEmpty(entryPointFullPath))
        this.SetupDefaults(entryPointFullPath, false);
      else
        this.ApplicationBase = activationArguments.ActivationContext.ApplicationDirectory;
    }

    internal void SetupDefaults(string imageLocation, bool imageLocationAlreadyNormalized = false)
    {
      char[] anyOf = new char[2]{ '\\', '/' };
      int num = imageLocation.LastIndexOfAny(anyOf);
      if (num == -1)
      {
        this.ApplicationName = imageLocation;
      }
      else
      {
        this.ApplicationName = imageLocation.Substring(num + 1);
        string str = imageLocation.Substring(0, num + 1);
        if (imageLocationAlreadyNormalized)
          this.Value[0] = str;
        else
          this.ApplicationBase = str;
      }
      this.ConfigurationFile = this.ApplicationName + AppDomainSetup.ConfigurationExtension;
    }

    internal string[] Value
    {
      get
      {
        if (this._Entries == null)
          this._Entries = new string[18];
        return this._Entries;
      }
    }

    internal string GetUnsecureApplicationBase()
    {
      return this.Value[0];
    }

    /// <summary>
    ///   Возвращает или задает отображаемое имя сборки, предоставляющей тип диспетчера доменов приложений для доменов приложений, созданных с помощью объекта <see cref="T:System.AppDomainSetup" />.
    /// </summary>
    /// <returns>
    ///   Отображаемое имя сборки, предоставляющей <see cref="T:System.Type" /> диспетчера доменов приложений.
    /// </returns>
    public string AppDomainManagerAssembly
    {
      get
      {
        return this._AppDomainManagerAssembly;
      }
      set
      {
        this._AppDomainManagerAssembly = value;
      }
    }

    /// <summary>
    ///   Получает или задает полное имя типа, содержащего диспетчер доменов приложений, созданных с помощью данного объекта <see cref="T:System.AppDomainSetup" />.
    /// </summary>
    /// <returns>Полное имя типа, включая пространство имен.</returns>
    public string AppDomainManagerType
    {
      get
      {
        return this._AppDomainManagerType;
      }
      set
      {
        this._AppDomainManagerType = value;
      }
    }

    /// <summary>
    ///   Получает или задает список помеченных флагом <see cref="F:System.Security.PartialTrustVisibilityLevel.NotVisibleByDefault" /> сборок, которые доступны коду с частичным доверием в изолированном домене приложения.
    /// </summary>
    /// <returns>
    ///   Массив частичных имен сборок, каждое из которых состоит из простого имени сборки и открытого ключа.
    /// </returns>
    public string[] PartialTrustVisibleAssemblies
    {
      get
      {
        return this._AptcaVisibleAssemblies;
      }
      set
      {
        if (value != null)
        {
          this._AptcaVisibleAssemblies = (string[]) value.Clone();
          Array.Sort<string>(this._AptcaVisibleAssemblies, (IComparer<string>) StringComparer.OrdinalIgnoreCase);
        }
        else
          this._AptcaVisibleAssemblies = (string[]) null;
      }
    }

    /// <summary>
    ///   Возвращает или задает имя каталога, содержащего приложение.
    /// </summary>
    /// <returns>Имя базовой папки приложения.</returns>
    public string ApplicationBase
    {
      [SecuritySafeCritical] get
      {
        return this.VerifyDir(this.GetUnsecureApplicationBase(), false);
      }
      set
      {
        this.Value[0] = this.NormalizePath(value, false);
      }
    }

    [SecuritySafeCritical]
    private string NormalizePath(string path, bool useAppBase)
    {
      if (path == null)
        return (string) null;
      if (!useAppBase)
        path = URLString.PreProcessForExtendedPathRemoval(false, path, false);
      int length1 = path.Length;
      if (length1 == 0)
        return (string) null;
      bool flag1 = false;
      if (length1 > 7 && string.Compare(path, 0, "file:", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
      {
        int startIndex;
        if (path[6] == '\\')
        {
          if (path[7] == '\\' || path[7] == '/')
          {
            if (length1 > 8 && (path[8] == '\\' || path[8] == '/'))
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
            startIndex = 8;
          }
          else
          {
            startIndex = 5;
            flag1 = true;
          }
        }
        else if (path[7] == '/')
        {
          startIndex = 8;
        }
        else
        {
          if (length1 > 8 && path[7] == '\\' && path[8] == '\\')
          {
            startIndex = 7;
          }
          else
          {
            startIndex = 5;
            StringBuilder stringBuilder = new StringBuilder(length1);
            for (int index = 0; index < length1; ++index)
            {
              char ch = path[index];
              if (ch == '/')
                stringBuilder.Append('\\');
              else
                stringBuilder.Append(ch);
            }
            path = stringBuilder.ToString();
          }
          flag1 = true;
        }
        path = path.Substring(startIndex);
        length1 -= startIndex;
      }
      bool flag2;
      if (flag1 || length1 > 1 && (path[0] == '/' || path[0] == '\\') && (path[1] == '/' || path[1] == '\\'))
      {
        flag2 = false;
      }
      else
      {
        int index = path.IndexOf(':') + 1;
        flag2 = index == 0 || length1 <= index + 1 || path[index] != '/' && path[index] != '\\' || path[index + 1] != '/' && path[index + 1] != '\\';
      }
      if (flag2)
      {
        if (useAppBase && (length1 == 1 || path[1] != ':'))
        {
          string path1 = this.Value[0];
          if (path1 == null || path1.Length == 0)
            throw new MemberAccessException(Environment.GetResourceString("AppDomain_AppBaseNotSet"));
          StringBuilder sb = StringBuilderCache.Acquire(16);
          bool flag3 = false;
          if (path[0] == '/' || path[0] == '\\')
          {
            string path2 = AppDomain.NormalizePath(path1, false);
            string str = path2.Substring(0, PathInternal.GetRootLength(path2));
            if (str.Length == 0)
            {
              int num = path1.IndexOf(":/", StringComparison.Ordinal);
              if (num == -1)
                num = path1.IndexOf(":\\", StringComparison.Ordinal);
              int length2 = path1.Length;
              int length3 = num + 1;
              while (length3 < length2 && (path1[length3] == '/' || path1[length3] == '\\'))
                ++length3;
              while (length3 < length2 && path1[length3] != '/' && path1[length3] != '\\')
                ++length3;
              str = path1.Substring(0, length3);
            }
            sb.Append(str);
            flag3 = true;
          }
          else
            sb.Append(path1);
          int startIndex = sb.Length - 1;
          if (sb[startIndex] != '/' && sb[startIndex] != '\\')
          {
            if (!flag3)
            {
              if (path1.IndexOf(":/", StringComparison.Ordinal) == -1)
                sb.Append('\\');
              else
                sb.Append('/');
            }
          }
          else if (flag3)
            sb.Remove(startIndex, 1);
          sb.Append(path);
          path = StringBuilderCache.GetStringAndRelease(sb);
        }
        else
          path = AppDomain.NormalizePath(path, true);
      }
      return path;
    }

    private bool IsFilePath(string path)
    {
      if (path[1] == ':')
        return true;
      if (path[0] == '\\')
        return path[1] == '\\';
      return false;
    }

    internal static string ApplicationBaseKey
    {
      get
      {
        return "APPBASE";
      }
    }

    /// <summary>
    ///   Возвращает или задает имя файла конфигурации для домена приложения.
    /// </summary>
    /// <returns>Имя файла конфигурации.</returns>
    public string ConfigurationFile
    {
      [SecuritySafeCritical] get
      {
        return this.VerifyDir(this.Value[1], true);
      }
      set
      {
        this.Value[1] = value;
      }
    }

    internal string ConfigurationFileInternal
    {
      get
      {
        return this.NormalizePath(this.Value[1], true);
      }
    }

    internal static string ConfigurationFileKey
    {
      get
      {
        return "APP_CONFIG_FILE";
      }
    }

    /// <summary>
    ///   Возвращает XML-данные конфигурации, заданные методом <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" />, который переопределяет XML-данные конфигурации приложения.
    /// </summary>
    /// <returns>
    ///   Массив, содержащий XML-данные конфигурации, заданные с помощью метода <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" />, или <see langword="null" />, если метод <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> не вызывался.
    /// </returns>
    public byte[] GetConfigurationBytes()
    {
      if (this._ConfigurationBytes == null)
        return (byte[]) null;
      return (byte[]) this._ConfigurationBytes.Clone();
    }

    /// <summary>
    ///   Предоставляет XML-данные конфигурации для домена приложения, заменяя XML-данные конфигурации приложения.
    /// </summary>
    /// <param name="value">
    ///   Массив, содержащий XML-данные конфигурации для домена приложения.
    /// </param>
    public void SetConfigurationBytes(byte[] value)
    {
      this._ConfigurationBytes = value;
    }

    private static string ConfigurationBytesKey
    {
      get
      {
        return "APP_CONFIG_BLOB";
      }
    }

    internal Dictionary<string, object> GetCompatibilityFlags()
    {
      return this._CompatFlags;
    }

    /// <summary>
    ///   Устанавливает заданные переключатели, благодаря чему домен приложения становится совместимым с предыдущими версиями платформы .NET Framework для указанных аспектов.
    /// </summary>
    /// <param name="switches">
    ///   Перечислимый набор строковых значений, задающий переключатели совместимости, или значение <see langword="null" /> для снятия существующих переключателей совместимости.
    /// </param>
    public void SetCompatibilitySwitches(IEnumerable<string> switches)
    {
      if (this._AppDomainSortingSetupInfo != null)
      {
        this._AppDomainSortingSetupInfo._useV2LegacySorting = false;
        this._AppDomainSortingSetupInfo._useV4LegacySorting = false;
      }
      this._UseRandomizedStringHashing = false;
      if (switches != null)
      {
        this._CompatFlags = new Dictionary<string, object>();
        foreach (string str in switches)
        {
          if (StringComparer.OrdinalIgnoreCase.Equals("NetFx40_Legacy20SortingBehavior", str))
          {
            if (this._AppDomainSortingSetupInfo == null)
              this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo();
            this._AppDomainSortingSetupInfo._useV2LegacySorting = true;
          }
          if (StringComparer.OrdinalIgnoreCase.Equals("NetFx45_Legacy40SortingBehavior", str))
          {
            if (this._AppDomainSortingSetupInfo == null)
              this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo();
            this._AppDomainSortingSetupInfo._useV4LegacySorting = true;
          }
          if (StringComparer.OrdinalIgnoreCase.Equals("UseRandomizedStringHashAlgorithm", str))
            this._UseRandomizedStringHashing = true;
          this._CompatFlags.Add(str, (object) null);
        }
      }
      else
        this._CompatFlags = (Dictionary<string, object>) null;
    }

    /// <summary>
    ///   Получает или задает строку, которая определяет целевую версию и профиль .NET Framework для домена приложения в формате, который может быть проанализирован конструктором <see cref="M:System.Runtime.Versioning.FrameworkName.#ctor(System.String)" />.
    /// </summary>
    /// <returns>Целевая версия и профиль платформы .NET Framework.</returns>
    public string TargetFrameworkName
    {
      get
      {
        return this._TargetFrameworkName;
      }
      set
      {
        this._TargetFrameworkName = value;
      }
    }

    internal bool CheckedForTargetFrameworkName
    {
      get
      {
        return this._CheckedForTargetFrameworkName;
      }
      set
      {
        this._CheckedForTargetFrameworkName = value;
      }
    }

    /// <summary>
    ///   Предоставляет среду CLR с резервной реализацией функции сравнения строк.
    /// </summary>
    /// <param name="functionName">
    ///   Имя переопределяемой функции сравнения строк.
    /// </param>
    /// <param name="functionVersion">
    ///   Версия функции.
    ///    Для .NET Framework 4.5 значение должно быть больше или равно 1.
    /// </param>
    /// <param name="functionPointer">
    ///   Указатель на функцию, переопределяющую <paramref name="functionName" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="functionName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="functionVersion" /> не является значением, равным или большим 1.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="functionPointer" /> имеет значение <see cref="F:System.IntPtr.Zero" />.
    /// </exception>
    [SecurityCritical]
    public void SetNativeFunction(string functionName, int functionVersion, IntPtr functionPointer)
    {
      if (functionName == null)
        throw new ArgumentNullException(nameof (functionName));
      if (functionPointer == IntPtr.Zero)
        throw new ArgumentNullException(nameof (functionPointer));
      if (string.IsNullOrWhiteSpace(functionName))
        throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInvalidName"), nameof (functionName));
      if (functionVersion < 1)
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_MinSortingVersion", (object) 1, (object) functionName));
      if (this._AppDomainSortingSetupInfo == null)
        this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo();
      if (string.Equals(functionName, "IsNLSDefinedString", StringComparison.OrdinalIgnoreCase))
        this._AppDomainSortingSetupInfo._pfnIsNLSDefinedString = functionPointer;
      if (string.Equals(functionName, "CompareStringEx", StringComparison.OrdinalIgnoreCase))
        this._AppDomainSortingSetupInfo._pfnCompareStringEx = functionPointer;
      if (string.Equals(functionName, "LCMapStringEx", StringComparison.OrdinalIgnoreCase))
        this._AppDomainSortingSetupInfo._pfnLCMapStringEx = functionPointer;
      if (string.Equals(functionName, "FindNLSStringEx", StringComparison.OrdinalIgnoreCase))
        this._AppDomainSortingSetupInfo._pfnFindNLSStringEx = functionPointer;
      if (string.Equals(functionName, "CompareStringOrdinal", StringComparison.OrdinalIgnoreCase))
        this._AppDomainSortingSetupInfo._pfnCompareStringOrdinal = functionPointer;
      if (string.Equals(functionName, "GetNLSVersionEx", StringComparison.OrdinalIgnoreCase))
        this._AppDomainSortingSetupInfo._pfnGetNLSVersionEx = functionPointer;
      if (!string.Equals(functionName, "FindStringOrdinal", StringComparison.OrdinalIgnoreCase))
        return;
      this._AppDomainSortingSetupInfo._pfnFindStringOrdinal = functionPointer;
    }

    /// <summary>
    ///   Возвращает или задает базовую папку, в которой находится папка для динамически создаваемых файлов.
    /// </summary>
    /// <returns>
    /// Каталог, в котором расположена папка <see cref="P:System.AppDomain.DynamicDirectory" />.
    /// 
    ///   Возвращаемое значение этого свойства отличается от присвоенного значения.
    ///    См. раздел примeчаний.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно задать это свойство, так как имя приложения в домене приложения — <see langword="null" />.
    /// </exception>
    public string DynamicBase
    {
      [SecuritySafeCritical] get
      {
        return this.VerifyDir(this.Value[2], true);
      }
      [SecuritySafeCritical] set
      {
        if (value == null)
        {
          this.Value[2] = (string) null;
        }
        else
        {
          if (this.ApplicationName == null)
            throw new MemberAccessException(Environment.GetResourceString("AppDomain_RequireApplicationName"));
          StringBuilder stringBuilder = new StringBuilder(this.NormalizePath(value, false));
          stringBuilder.Append('\\');
          string str = ParseNumbers.IntToString(this.ApplicationName.GetLegacyNonRandomizedHashCode(), 16, 8, '0', 256);
          stringBuilder.Append(str);
          this.Value[2] = stringBuilder.ToString();
        }
      }
    }

    internal static string DynamicBaseKey
    {
      get
      {
        return "DYNAMIC_BASE";
      }
    }

    /// <summary>
    ///   Получает или задает значение, указывающее, применяется ли к домену приложения раздел &lt;publisherPolicy&gt; файла конфигурации.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если раздел <see langword="&lt;publisherPolicy&gt;" /> файла конфигурации для домена приложения игнорируется; значение <see langword="false" />, если применяется объявленная политика издателя.
    /// </returns>
    public bool DisallowPublisherPolicy
    {
      get
      {
        return this.Value[11] != null;
      }
      set
      {
        if (value)
          this.Value[11] = "true";
        else
          this.Value[11] = (string) null;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, определяющее, допускает ли домен приложения перенаправление привязки сборок.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если перенаправление сборок запрещено; значение <see langword="false" />, если оно разрешено.
    /// </returns>
    public bool DisallowBindingRedirects
    {
      get
      {
        return this.Value[13] != null;
      }
      set
      {
        if (value)
          this.Value[13] = "true";
        else
          this.Value[13] = (string) null;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, разрешена ли загрузка сборок для этого домена приложения по протоколу HTTP.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если загрузка сборок по протоколу HTTP запрещена; значение <see langword="false" />, если она разрешена.
    /// </returns>
    public bool DisallowCodeDownload
    {
      get
      {
        return this.Value[12] != null;
      }
      set
      {
        if (value)
          this.Value[12] = "true";
        else
          this.Value[12] = (string) null;
      }
    }

    /// <summary>
    ///   Указывает, выполняется ли поиск загружаемых сборок в базовой папке приложения и папке приватных двоичных файлов.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если проверка не разрешена. В противном случае — значение <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public bool DisallowApplicationBaseProbing
    {
      get
      {
        return this.Value[14] != null;
      }
      set
      {
        if (value)
          this.Value[14] = "true";
        else
          this.Value[14] = (string) null;
      }
    }

    [SecurityCritical]
    private string VerifyDir(string dir, bool normalize)
    {
      if (dir != null)
      {
        if (dir.Length == 0)
        {
          dir = (string) null;
        }
        else
        {
          if (normalize)
            dir = this.NormalizePath(dir, true);
          if (this.IsFilePath(dir))
            new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[1]
            {
              dir
            }, false, false).Demand();
        }
      }
      return dir;
    }

    [SecurityCritical]
    private void VerifyDirList(string dirs)
    {
      if (dirs == null)
        return;
      string[] strArray = dirs.Split(';');
      int length = strArray.Length;
      for (int index = 0; index < length; ++index)
        this.VerifyDir(strArray[index], true);
    }

    internal string DeveloperPath
    {
      [SecurityCritical] get
      {
        string dirs = this.Value[3];
        this.VerifyDirList(dirs);
        return dirs;
      }
      set
      {
        if (value == null)
        {
          this.Value[3] = (string) null;
        }
        else
        {
          string[] strArray = value.Split(';');
          int length = strArray.Length;
          StringBuilder sb = StringBuilderCache.Acquire(16);
          bool flag = false;
          for (int index = 0; index < length; ++index)
          {
            if (strArray[index].Length != 0)
            {
              if (flag)
                sb.Append(";");
              else
                flag = true;
              sb.Append(Path.GetFullPathInternal(strArray[index]));
            }
          }
          string stringAndRelease = StringBuilderCache.GetStringAndRelease(sb);
          if (stringAndRelease.Length == 0)
            this.Value[3] = (string) null;
          else
            this.Value[3] = stringAndRelease;
        }
      }
    }

    internal static string DisallowPublisherPolicyKey
    {
      get
      {
        return "DISALLOW_APP";
      }
    }

    internal static string DisallowCodeDownloadKey
    {
      get
      {
        return "CODE_DOWNLOAD_DISABLED";
      }
    }

    internal static string DisallowBindingRedirectsKey
    {
      get
      {
        return "DISALLOW_APP_REDIRECTS";
      }
    }

    internal static string DeveloperPathKey
    {
      get
      {
        return "DEV_PATH";
      }
    }

    internal static string DisallowAppBaseProbingKey
    {
      get
      {
        return "DISALLOW_APP_BASE_PROBING";
      }
    }

    /// <summary>Возвращает или задает имя приложения.</summary>
    /// <returns>Имя приложения.</returns>
    public string ApplicationName
    {
      get
      {
        return this.Value[4];
      }
      set
      {
        this.Value[4] = value;
      }
    }

    internal static string ApplicationNameKey
    {
      get
      {
        return "APP_NAME";
      }
    }

    /// <summary>
    ///   Возвращает или задает делегат <see cref="T:System.AppDomainInitializer" />, представляющий метод обратного вызова, вызываемый при инициализации домена приложения.
    /// </summary>
    /// <returns>
    ///   Делегат, представляющий метод обратного вызова, вызываемый при инициализации домена приложения.
    /// </returns>
    [XmlIgnoreMember]
    public AppDomainInitializer AppDomainInitializer
    {
      get
      {
        return this._AppDomainInitializer;
      }
      set
      {
        this._AppDomainInitializer = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает аргументы, которые передаются в метод обратного вызова, представленный делегатом <see cref="T:System.AppDomainInitializer" />.
    ///    Этот метод обратного вызова вызывается при инициализации домена приложения.
    /// </summary>
    /// <returns>
    ///   Массив строк, который передается методу обратного вызова, представленному делегатом <see cref="T:System.AppDomainInitializer" />, когда метод обратного вызова вызывается при инициализации <see cref="T:System.AppDomain" />.
    /// </returns>
    public string[] AppDomainInitializerArguments
    {
      get
      {
        return this._AppDomainInitializerArguments;
      }
      set
      {
        this._AppDomainInitializerArguments = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает данные об активации домена приложения.
    /// </summary>
    /// <returns>
    ///   Объект, содержащий данные об активации домена приложения.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство имеет значение объекта <see cref="T:System.Runtime.Hosting.ActivationArguments" />, удостоверение приложения которого не соответствует удостоверению приложения объекта <see cref="T:System.Security.Policy.ApplicationTrust" />, возвращенного свойством <see cref="P:System.AppDomainSetup.ApplicationTrust" />.
    ///    Если свойство <see cref="P:System.AppDomainSetup.ApplicationTrust" /> имеет значение <see langword="null" />, исключение не вызывается.
    /// </exception>
    [XmlIgnoreMember]
    public ActivationArguments ActivationArguments
    {
      get
      {
        return this._ActivationArguments;
      }
      set
      {
        this._ActivationArguments = value;
      }
    }

    internal ApplicationTrust InternalGetApplicationTrust()
    {
      if (this._ApplicationTrust == null)
        return (ApplicationTrust) null;
      SecurityElement element = SecurityElement.FromString(this._ApplicationTrust);
      ApplicationTrust applicationTrust = new ApplicationTrust();
      applicationTrust.FromXml(element);
      return applicationTrust;
    }

    internal void InternalSetApplicationTrust(ApplicationTrust value)
    {
      if (value != null)
        this._ApplicationTrust = value.ToXml().ToString();
      else
        this._ApplicationTrust = (string) null;
    }

    /// <summary>
    ///   Возвращает или задает объект, содержащий сведения о безопасности и доверии.
    /// </summary>
    /// <returns>
    ///   Объект, содержащий сведения о безопасности и доверии.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство имеет значение объекта <see cref="T:System.Security.Policy.ApplicationTrust" />, удостоверение приложения которого не соответствует удостоверению приложения объекта <see cref="T:System.Runtime.Hosting.ActivationArguments" />, возвращенного свойством <see cref="P:System.AppDomainSetup.ActivationArguments" />.
    ///    Если свойство <see cref="P:System.AppDomainSetup.ActivationArguments" /> имеет значение <see langword="null" />, исключение не вызывается.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задано значение <see langword="null" />.
    /// </exception>
    [XmlIgnoreMember]
    public ApplicationTrust ApplicationTrust
    {
      get
      {
        return this.InternalGetApplicationTrust();
      }
      set
      {
        this.InternalSetApplicationTrust(value);
      }
    }

    /// <summary>
    ///   Возвращает или задает список каталогов в базовой папке приложения, в которых выполняется поиск закрытых сборок.
    /// </summary>
    /// <returns>
    ///   Список имен каталогов, разделенных точкой с запятой.
    /// </returns>
    public string PrivateBinPath
    {
      [SecuritySafeCritical] get
      {
        string dirs = this.Value[5];
        this.VerifyDirList(dirs);
        return dirs;
      }
      set
      {
        this.Value[5] = value;
      }
    }

    internal static string PrivateBinPathKey
    {
      get
      {
        return "PRIVATE_BINPATH";
      }
    }

    /// <summary>
    ///   Получает или задает строковое значение, включающее <see cref="P:System.AppDomainSetup.ApplicationBase" /> в путь поиска для приложения или исключающее его из этого пути, чтобы поиск выполнялся только в <see cref="P:System.AppDomainSetup.PrivateBinPath" />.
    /// </summary>
    /// <returns>
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для включения пути к базовой папке приложения при поиске сборок; любое непустое строковое значение для исключения пути.
    ///    Значение по умолчанию — <see langword="null" />.
    /// </returns>
    public string PrivateBinPathProbe
    {
      get
      {
        return this.Value[6];
      }
      set
      {
        this.Value[6] = value;
      }
    }

    internal static string PrivateBinPathProbeKey
    {
      get
      {
        return "BINPATH_PROBE_ONLY";
      }
    }

    /// <summary>
    ///   Возвращает или задает имена каталогов, содержащих сборки, для которых будут созданы теневые копии.
    /// </summary>
    /// <returns>
    ///   Список имен каталогов, разделенных точкой с запятой.
    /// </returns>
    public string ShadowCopyDirectories
    {
      [SecuritySafeCritical] get
      {
        string dirs = this.Value[7];
        this.VerifyDirList(dirs);
        return dirs;
      }
      set
      {
        this.Value[7] = value;
      }
    }

    internal static string ShadowCopyDirectoriesKey
    {
      get
      {
        return "SHADOW_COPY_DIRS";
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, позволяющую определить, включено ли теневое копирование.
    /// </summary>
    /// <returns>
    ///   Строковое значение true, свидетельствующее о том, что теневое копирование включено, или значение false, указывающее на то, что оно отключено.
    /// </returns>
    public string ShadowCopyFiles
    {
      get
      {
        return this.Value[8];
      }
      set
      {
        if (value != null && string.Compare(value, "true", StringComparison.OrdinalIgnoreCase) == 0)
          this.Value[8] = value;
        else
          this.Value[8] = (string) null;
      }
    }

    internal static string ShadowCopyFilesKey
    {
      get
      {
        return "FORCE_CACHE_INSTALL";
      }
    }

    /// <summary>
    ///   Возвращает или задает имя области, определенной для приложения, где создаются теневые копии файлов.
    /// </summary>
    /// <returns>
    ///   Полный путь к каталогу и полное имя файла, соответствующие создаваемой теневой копии.
    /// </returns>
    public string CachePath
    {
      [SecuritySafeCritical] get
      {
        return this.VerifyDir(this.Value[9], false);
      }
      set
      {
        this.Value[9] = this.NormalizePath(value, false);
      }
    }

    internal static string CachePathKey
    {
      get
      {
        return "CACHE_BASE";
      }
    }

    /// <summary>
    ///   Возвращает или задает расположение файла лицензии, связанного с этим доменом.
    /// </summary>
    /// <returns>Имя и расположение файла лицензии.</returns>
    public string LicenseFile
    {
      [SecuritySafeCritical] get
      {
        return this.VerifyDir(this.Value[10], true);
      }
      set
      {
        this.Value[10] = value;
      }
    }

    /// <summary>
    ///   Определяет политику оптимизации, используемую для загрузки исполняемого файла.
    /// </summary>
    /// <returns>
    ///   Константа перечисляемого типа, которая используется с <see cref="T:System.LoaderOptimizationAttribute" />.
    /// </returns>
    public LoaderOptimization LoaderOptimization
    {
      get
      {
        return this._LoaderOptimization;
      }
      set
      {
        this._LoaderOptimization = value;
      }
    }

    internal static string LoaderOptimizationKey
    {
      get
      {
        return "LOADER_OPTIMIZATION";
      }
    }

    internal static string ConfigurationExtension
    {
      get
      {
        return ".config";
      }
    }

    internal static string PrivateBinPathEnvironmentVariable
    {
      get
      {
        return "RELPATH";
      }
    }

    internal static string RuntimeConfigurationFile
    {
      get
      {
        return "config\\machine.config";
      }
    }

    internal static string MachineConfigKey
    {
      get
      {
        return "MACHINE_CONFIG";
      }
    }

    internal static string HostBindingKey
    {
      get
      {
        return "HOST_CONFIG";
      }
    }

    [SecurityCritical]
    internal bool UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation FieldValue, string FieldKey, string UpdatedField, IntPtr fusionContext, AppDomainSetup oldADS)
    {
      string str1 = this.Value[(int) FieldValue];
      string str2 = oldADS == null ? (string) null : oldADS.Value[(int) FieldValue];
      if (!(str1 != str2))
        return false;
      AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, UpdatedField == null ? (object) str1 : (object) UpdatedField);
      return true;
    }

    [SecurityCritical]
    internal void UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation FieldValue, string FieldKey, IntPtr fusionContext, AppDomainSetup oldADS)
    {
      if (this.Value[(int) FieldValue] != null)
      {
        AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, (object) "true");
      }
      else
      {
        if (oldADS == null || oldADS.Value[(int) FieldValue] == null)
          return;
        AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, (object) "false");
      }
    }

    [SecurityCritical]
    internal static bool ByteArraysAreDifferent(byte[] A, byte[] B)
    {
      int length = A.Length;
      if (length != B.Length)
        return true;
      for (int index = 0; index < length; ++index)
      {
        if ((int) A[index] != (int) B[index])
          return true;
      }
      return false;
    }

    [SecurityCritical]
    internal static void UpdateByteArrayContextPropertyIfNeeded(byte[] NewArray, byte[] OldArray, string FieldKey, IntPtr fusionContext)
    {
      if ((NewArray == null || OldArray != null) && (NewArray != null || OldArray == null) && (NewArray == null || OldArray == null || !AppDomainSetup.ByteArraysAreDifferent(NewArray, OldArray)))
        return;
      AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, (object) NewArray);
    }

    [SecurityCritical]
    internal void SetupFusionContext(IntPtr fusionContext, AppDomainSetup oldADS)
    {
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ApplicationBaseValue, AppDomainSetup.ApplicationBaseKey, (string) null, fusionContext, oldADS);
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.PrivateBinPathValue, AppDomainSetup.PrivateBinPathKey, (string) null, fusionContext, oldADS);
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DevPathValue, AppDomainSetup.DeveloperPathKey, (string) null, fusionContext, oldADS);
      this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowPublisherPolicyValue, AppDomainSetup.DisallowPublisherPolicyKey, fusionContext, oldADS);
      this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowCodeDownloadValue, AppDomainSetup.DisallowCodeDownloadKey, fusionContext, oldADS);
      this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowBindingRedirectsValue, AppDomainSetup.DisallowBindingRedirectsKey, fusionContext, oldADS);
      this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowAppBaseProbingValue, AppDomainSetup.DisallowAppBaseProbingKey, fusionContext, oldADS);
      if (this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ShadowCopyFilesValue, AppDomainSetup.ShadowCopyFilesKey, this.ShadowCopyFiles, fusionContext, oldADS))
      {
        if (this.Value[7] == null)
          this.ShadowCopyDirectories = this.BuildShadowCopyDirectories();
        this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ShadowCopyDirectoriesValue, AppDomainSetup.ShadowCopyDirectoriesKey, (string) null, fusionContext, oldADS);
      }
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.CachePathValue, AppDomainSetup.CachePathKey, (string) null, fusionContext, oldADS);
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.PrivateBinPathProbeValue, AppDomainSetup.PrivateBinPathProbeKey, this.PrivateBinPathProbe, fusionContext, oldADS);
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ConfigurationFileValue, AppDomainSetup.ConfigurationFileKey, (string) null, fusionContext, oldADS);
      AppDomainSetup.UpdateByteArrayContextPropertyIfNeeded(this._ConfigurationBytes, oldADS == null ? (byte[]) null : oldADS.GetConfigurationBytes(), AppDomainSetup.ConfigurationBytesKey, fusionContext);
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ApplicationNameValue, AppDomainSetup.ApplicationNameKey, this.ApplicationName, fusionContext, oldADS);
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DynamicBaseValue, AppDomainSetup.DynamicBaseKey, (string) null, fusionContext, oldADS);
      AppDomainSetup.UpdateContextProperty(fusionContext, AppDomainSetup.MachineConfigKey, (object) (RuntimeEnvironment.GetRuntimeDirectoryImpl() + AppDomainSetup.RuntimeConfigurationFile));
      string hostBindingFile = RuntimeEnvironment.GetHostBindingFile();
      if (hostBindingFile == null && oldADS == null)
        return;
      AppDomainSetup.UpdateContextProperty(fusionContext, AppDomainSetup.HostBindingKey, (object) hostBindingFile);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void UpdateContextProperty(IntPtr fusionContext, string key, object value);

    internal static int Locate(string s)
    {
      if (string.IsNullOrEmpty(s))
        return -1;
      switch (s[0])
      {
        case 'A':
          if (s == "APP_CONFIG_FILE")
            return 1;
          if (s == "APP_NAME")
            return 4;
          if (s == "APPBASE")
            return 0;
          if (s == "APP_CONFIG_BLOB")
            return 15;
          break;
        case 'B':
          if (s == "BINPATH_PROBE_ONLY")
            return 6;
          break;
        case 'C':
          if (s == "CACHE_BASE")
            return 9;
          if (s == "CODE_DOWNLOAD_DISABLED")
            return 12;
          break;
        case 'D':
          if (s == "DEV_PATH")
            return 3;
          if (s == "DYNAMIC_BASE")
            return 2;
          if (s == "DISALLOW_APP")
            return 11;
          if (s == "DISALLOW_APP_REDIRECTS")
            return 13;
          if (s == "DISALLOW_APP_BASE_PROBING")
            return 14;
          break;
        case 'F':
          if (s == "FORCE_CACHE_INSTALL")
            return 8;
          break;
        case 'L':
          if (s == "LICENSE_FILE")
            return 10;
          break;
        case 'P':
          if (s == "PRIVATE_BINPATH")
            return 5;
          break;
        case 'S':
          if (s == "SHADOW_COPY_DIRS")
            return 7;
          break;
      }
      return -1;
    }

    private string BuildShadowCopyDirectories()
    {
      string str1 = this.Value[5];
      if (str1 == null)
        return (string) null;
      StringBuilder sb = StringBuilderCache.Acquire(16);
      string str2 = this.Value[0];
      if (str2 != null)
      {
        char[] chArray = new char[1]{ ';' };
        string[] strArray = str1.Split(chArray);
        int length = strArray.Length;
        bool flag = str2[str2.Length - 1] != '/' && str2[str2.Length - 1] != '\\';
        if (length == 0)
        {
          sb.Append(str2);
          if (flag)
            sb.Append('\\');
          sb.Append(str1);
        }
        else
        {
          for (int index = 0; index < length; ++index)
          {
            sb.Append(str2);
            if (flag)
              sb.Append('\\');
            sb.Append(strArray[index]);
            if (index < length - 1)
              sb.Append(';');
          }
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, отключено ли кэширование интерфейсов для вызовов взаимодействия в домене приложения, чтобы QueryInterface выполняется при каждом вызове.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если кэширование интерфейсов отключено для вызовов взаимодействия в доменах приложений, созданных с текущим <see cref="T:System.AppDomainSetup" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    public bool SandboxInterop
    {
      get
      {
        return this._DisableInterfaceCache;
      }
      set
      {
        this._DisableInterfaceCache = value;
      }
    }

    [Serializable]
    internal enum LoaderInformation
    {
      ApplicationBaseValue = 0,
      ConfigurationFileValue = 1,
      DynamicBaseValue = 2,
      DevPathValue = 3,
      ApplicationNameValue = 4,
      PrivateBinPathValue = 5,
      PrivateBinPathProbeValue = 6,
      ShadowCopyDirectoriesValue = 7,
      ShadowCopyFilesValue = 8,
      CachePathValue = 9,
      LicenseFileValue = 10, // 0x0000000A
      DisallowPublisherPolicyValue = 11, // 0x0000000B
      DisallowCodeDownloadValue = 12, // 0x0000000C
      DisallowBindingRedirectsValue = 13, // 0x0000000D
      DisallowAppBaseProbingValue = 14, // 0x0000000E
      ConfigurationBytesValue = 15, // 0x0000000F
      LoaderMaximum = 18, // 0x00000012
    }
  }
}
