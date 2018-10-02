// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyName
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Reflection
{
  /// <summary>Подробно описывает уникальный идентификатор сборки.</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_AssemblyName))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class AssemblyName : _AssemblyName, ICloneable, ISerializable, IDeserializationCallback
  {
    private string _Name;
    private byte[] _PublicKey;
    private byte[] _PublicKeyToken;
    private CultureInfo _CultureInfo;
    private string _CodeBase;
    private Version _Version;
    private StrongNameKeyPair _StrongNameKeyPair;
    private SerializationInfo m_siInfo;
    private byte[] _HashForControl;
    private AssemblyHashAlgorithm _HashAlgorithm;
    private AssemblyHashAlgorithm _HashAlgorithmForControl;
    private AssemblyVersionCompatibility _VersionCompatibility;
    private AssemblyNameFlags _Flags;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AssemblyName" />.
    /// </summary>
    [__DynamicallyInvokable]
    public AssemblyName()
    {
      this._HashAlgorithm = AssemblyHashAlgorithm.None;
      this._VersionCompatibility = AssemblyVersionCompatibility.SameMachine;
      this._Flags = AssemblyNameFlags.None;
    }

    /// <summary>
    ///   Получает или задает простое имя сборки.
    ///    Обычно это, но не обязательно, имя файла манифеста сборки без указания его расширения файла.
    /// </summary>
    /// <returns>Простое имя сборки.</returns>
    [__DynamicallyInvokable]
    public string Name
    {
      [__DynamicallyInvokable] get
      {
        return this._Name;
      }
      [__DynamicallyInvokable] set
      {
        this._Name = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает основной и дополнительный номер сборки и номер редакции сборки.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий основной и дополнительный номера, построения и редакции сборки.
    /// </returns>
    [__DynamicallyInvokable]
    public Version Version
    {
      [__DynamicallyInvokable] get
      {
        return this._Version;
      }
      [__DynamicallyInvokable] set
      {
        this._Version = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает культуру, поддерживаемую сборкой.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий культуру, поддерживаемую сборкой.
    /// </returns>
    [__DynamicallyInvokable]
    public CultureInfo CultureInfo
    {
      [__DynamicallyInvokable] get
      {
        return this._CultureInfo;
      }
      [__DynamicallyInvokable] set
      {
        this._CultureInfo = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает имя языка и региональных параметров, связанные со сборкой.
    /// </summary>
    /// <returns>Имя языка и региональных параметров.</returns>
    [__DynamicallyInvokable]
    public string CultureName
    {
      [__DynamicallyInvokable] get
      {
        if (this._CultureInfo != null)
          return this._CultureInfo.Name;
        return (string) null;
      }
      [__DynamicallyInvokable] set
      {
        this._CultureInfo = value == null ? (CultureInfo) null : new CultureInfo(value);
      }
    }

    /// <summary>
    ///   Возвращает или задает местонахождение сборки в виде URL-адреса.
    /// </summary>
    /// <returns>Строка, представляющая URL-адрес сборки.</returns>
    public string CodeBase
    {
      get
      {
        return this._CodeBase;
      }
      set
      {
        this._CodeBase = value;
      }
    }

    /// <summary>
    ///   Получает универсальный код доступа (URI), предоставляющий базовый код, включая escape-символы.
    /// </summary>
    /// <returns>Универсальный код доступа (URI) с escape-символами.</returns>
    public string EscapedCodeBase
    {
      [SecuritySafeCritical] get
      {
        if (this._CodeBase == null)
          return (string) null;
        return AssemblyName.EscapeCodeBase(this._CodeBase);
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее процессор и бит слова платформы исполняемый файл.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, которое определяет процессор и бит слова платформы исполняемый файл.
    /// </returns>
    [__DynamicallyInvokable]
    public ProcessorArchitecture ProcessorArchitecture
    {
      [__DynamicallyInvokable] get
      {
        int num = (int) (this._Flags & (AssemblyNameFlags) 112) >> 4;
        if (num > 5)
          num = 0;
        return (ProcessorArchitecture) num;
      }
      [__DynamicallyInvokable] set
      {
        int num = (int) (value & (ProcessorArchitecture.IA64 | ProcessorArchitecture.Amd64));
        if (num > 5)
          return;
        this._Flags = (AssemblyNameFlags) ((long) this._Flags & 4294967055L);
        this._Flags |= (AssemblyNameFlags) (num << 4);
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, какой тип содержимого содержит сборку.
    /// </summary>
    /// <returns>
    ///   Содержит значение, указывающее, какой тип содержимого сборки.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public AssemblyContentType ContentType
    {
      [__DynamicallyInvokable] get
      {
        int num = (int) (this._Flags & (AssemblyNameFlags) 3584) >> 9;
        if (num > 1)
          num = 0;
        return (AssemblyContentType) num;
      }
      [__DynamicallyInvokable] set
      {
        int num = (int) (value & (AssemblyContentType) 7);
        if (num > 1)
          return;
        this._Flags = (AssemblyNameFlags) ((long) this._Flags & 4294963711L);
        this._Flags |= (AssemblyNameFlags) (num << 9);
      }
    }

    /// <summary>
    ///   Создает копию данного объекта <see cref="T:System.Reflection.AssemblyName" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект, являющийся копией этого <see cref="T:System.Reflection.AssemblyName" /> объекта.
    /// </returns>
    public object Clone()
    {
      AssemblyName assemblyName = new AssemblyName();
      assemblyName.Init(this._Name, this._PublicKey, this._PublicKeyToken, this._Version, this._CultureInfo, this._HashAlgorithm, this._VersionCompatibility, this._CodeBase, this._Flags, this._StrongNameKeyPair);
      assemblyName._HashForControl = this._HashForControl;
      assemblyName._HashAlgorithmForControl = this._HashAlgorithmForControl;
      return (object) assemblyName;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.AssemblyName" /> для заданного файла.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Путь к сборке, <see cref="T:System.Reflection.AssemblyName" /> должен быть возвращен.
    /// </param>
    /// <returns>Объект, представляющий файл заданной сборки.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="assemblyFile" /> является недопустимым, например, сборки с недопустимый язык и региональные параметры.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyFile" /> не найден.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект не имеет разрешения на обнаружение пути.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными наборами свидетельств.
    /// </exception>
    [SecuritySafeCritical]
    public static AssemblyName GetAssemblyName(string assemblyFile)
    {
      if (assemblyFile == null)
        throw new ArgumentNullException(nameof (assemblyFile));
      string fullPathInternal = Path.GetFullPathInternal(assemblyFile);
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullPathInternal).Demand();
      return AssemblyName.nGetFileInformation(fullPathInternal);
    }

    internal void SetHashControl(byte[] hash, AssemblyHashAlgorithm hashAlgorithm)
    {
      this._HashForControl = hash;
      this._HashAlgorithmForControl = hashAlgorithm;
    }

    /// <summary>Возвращает открытый ключ сборки.</summary>
    /// <returns>Массив байтов, содержащий открытый ключ сборки.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Был предоставлен открытый ключ (например, с помощью <see cref="M:System.Reflection.AssemblyName.SetPublicKey(System.Byte[])" /> метод), но не маркер открытого ключа.
    /// </exception>
    [__DynamicallyInvokable]
    public byte[] GetPublicKey()
    {
      return this._PublicKey;
    }

    /// <summary>Задает открытый ключ, идентифицирующий сборку.</summary>
    /// <param name="publicKey">
    ///   Массив байтов, содержащий открытый ключ сборки.
    /// </param>
    [__DynamicallyInvokable]
    public void SetPublicKey(byte[] publicKey)
    {
      this._PublicKey = publicKey;
      if (publicKey == null)
        this._Flags &= ~AssemblyNameFlags.PublicKey;
      else
        this._Flags |= AssemblyNameFlags.PublicKey;
    }

    /// <summary>
    ///   Возвращает токен открытого ключа, которая представляет последние 8 байтов хэша SHA-1 открытого ключа, которым подписывается приложение или сборка.
    /// </summary>
    /// <returns>Массив байтов, содержащий токен открытого ключа.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public byte[] GetPublicKeyToken()
    {
      if (this._PublicKeyToken == null)
        this._PublicKeyToken = this.nGetPublicKeyToken();
      return this._PublicKeyToken;
    }

    /// <summary>
    ///   Задает токен открытого ключа, которая представляет последние 8 байтов хэша SHA-1 открытого ключа, которым подписывается приложение или сборка.
    /// </summary>
    /// <param name="publicKeyToken">
    ///   Массив байтов, содержащий токен открытого ключа сборки.
    /// </param>
    [__DynamicallyInvokable]
    public void SetPublicKeyToken(byte[] publicKeyToken)
    {
      this._PublicKeyToken = publicKeyToken;
    }

    /// <summary>Возвращает или задает атрибуты сборки.</summary>
    /// <returns>Значение, представляющее атрибуты сборки.</returns>
    [__DynamicallyInvokable]
    public AssemblyNameFlags Flags
    {
      [__DynamicallyInvokable] get
      {
        return this._Flags & (AssemblyNameFlags) -3825;
      }
      [__DynamicallyInvokable] set
      {
        this._Flags &= (AssemblyNameFlags) 3824;
        this._Flags |= value & (AssemblyNameFlags) -3825;
      }
    }

    /// <summary>
    ///   Возвращает или задает алгоритм хэширования, используемый манифестом сборки.
    /// </summary>
    /// <returns>
    ///   Алгоритм хэширования, используемый манифестом сборки.
    /// </returns>
    public AssemblyHashAlgorithm HashAlgorithm
    {
      get
      {
        return this._HashAlgorithm;
      }
      set
      {
        this._HashAlgorithm = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает сведения, связанные с совместимостью сборки с другими сборками.
    /// </summary>
    /// <returns>
    ///   Значение, представляющее сведения о совместимости сборки с другими сборками.
    /// </returns>
    public AssemblyVersionCompatibility VersionCompatibility
    {
      get
      {
        return this._VersionCompatibility;
      }
      set
      {
        this._VersionCompatibility = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает пару открытого и закрытого криптографических ключей, используемый для создания подписи строгого имени для сборки.
    /// </summary>
    /// <returns>
    ///   Пара открытого и закрытого криптографических ключей для создания строгого имени для сборки.
    /// </returns>
    public StrongNameKeyPair KeyPair
    {
      get
      {
        return this._StrongNameKeyPair;
      }
      set
      {
        this._StrongNameKeyPair = value;
      }
    }

    /// <summary>
    ///   Возвращает полное имя сборки, также известный как отображаемое имя.
    /// </summary>
    /// <returns>
    ///   Строка, представляющая полное имя сборки, также известный как отображаемое имя.
    /// </returns>
    [__DynamicallyInvokable]
    public string FullName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        string str = this.nToString();
        if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && string.IsNullOrEmpty(str))
          return base.ToString();
        return str;
      }
    }

    /// <summary>
    ///   Возвращает полное имя сборки, также называемое отображаемым именем.
    /// </summary>
    /// <returns>
    ///   Полное имя сборки или имя класса, если полное имя не может быть определено.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.FullName ?? base.ToString();
    }

    /// <summary>
    ///   Возвращает сведения сериализации со всеми данными, необходимыми для повторного создания экземпляра этого <see langword="AssemblyName" />.
    /// </summary>
    /// <param name="info">
    ///   Объект, для которого будут заполнены сведения о сериализации.
    /// </param>
    /// <param name="context">Контекст назначения сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.AddValue("_Name", (object) this._Name);
      info.AddValue("_PublicKey", (object) this._PublicKey, typeof (byte[]));
      info.AddValue("_PublicKeyToken", (object) this._PublicKeyToken, typeof (byte[]));
      info.AddValue("_CultureInfo", this._CultureInfo == null ? -1 : this._CultureInfo.LCID);
      info.AddValue("_CodeBase", (object) this._CodeBase);
      info.AddValue("_Version", (object) this._Version);
      info.AddValue("_HashAlgorithm", (object) this._HashAlgorithm, typeof (AssemblyHashAlgorithm));
      info.AddValue("_HashAlgorithmForControl", (object) this._HashAlgorithmForControl, typeof (AssemblyHashAlgorithm));
      info.AddValue("_StrongNameKeyPair", (object) this._StrongNameKeyPair, typeof (StrongNameKeyPair));
      info.AddValue("_VersionCompatibility", (object) this._VersionCompatibility, typeof (AssemblyVersionCompatibility));
      info.AddValue("_Flags", (object) this._Flags, typeof (AssemblyNameFlags));
      info.AddValue("_HashForControl", (object) this._HashForControl, typeof (byte[]));
    }

    /// <summary>
    ///   Реализует интерфейс <see cref="T:System.Runtime.Serialization.ISerializable" /> и вызывается событием десериализации после завершения десериализации в ходе обратного вызова.
    /// </summary>
    /// <param name="sender">Источник события десериализации.</param>
    public void OnDeserialization(object sender)
    {
      if (this.m_siInfo == null)
        return;
      this._Name = this.m_siInfo.GetString("_Name");
      this._PublicKey = (byte[]) this.m_siInfo.GetValue("_PublicKey", typeof (byte[]));
      this._PublicKeyToken = (byte[]) this.m_siInfo.GetValue("_PublicKeyToken", typeof (byte[]));
      int int32 = this.m_siInfo.GetInt32("_CultureInfo");
      if (int32 != -1)
        this._CultureInfo = new CultureInfo(int32);
      this._CodeBase = this.m_siInfo.GetString("_CodeBase");
      this._Version = (Version) this.m_siInfo.GetValue("_Version", typeof (Version));
      this._HashAlgorithm = (AssemblyHashAlgorithm) this.m_siInfo.GetValue("_HashAlgorithm", typeof (AssemblyHashAlgorithm));
      this._StrongNameKeyPair = (StrongNameKeyPair) this.m_siInfo.GetValue("_StrongNameKeyPair", typeof (StrongNameKeyPair));
      this._VersionCompatibility = (AssemblyVersionCompatibility) this.m_siInfo.GetValue("_VersionCompatibility", typeof (AssemblyVersionCompatibility));
      this._Flags = (AssemblyNameFlags) this.m_siInfo.GetValue("_Flags", typeof (AssemblyNameFlags));
      try
      {
        this._HashAlgorithmForControl = (AssemblyHashAlgorithm) this.m_siInfo.GetValue("_HashAlgorithmForControl", typeof (AssemblyHashAlgorithm));
        this._HashForControl = (byte[]) this.m_siInfo.GetValue("_HashForControl", typeof (byte[]));
      }
      catch (SerializationException ex)
      {
        this._HashAlgorithmForControl = AssemblyHashAlgorithm.None;
        this._HashForControl = (byte[]) null;
      }
      this.m_siInfo = (SerializationInfo) null;
    }

    internal AssemblyName(SerializationInfo info, StreamingContext context)
    {
      this.m_siInfo = info;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AssemblyName" /> с заданным отображаемым именем.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки, возвращаемое свойством <see cref="P:System.Reflection.AssemblyName.FullName" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="assemblyName" /> — пустая строка.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.IO.IOException" />.
    /// 
    ///   Не удалось найти или загрузить сборку, на которую указывает ссылка.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public AssemblyName(string assemblyName)
    {
      if (assemblyName == null)
        throw new ArgumentNullException(nameof (assemblyName));
      if (assemblyName.Length == 0 || assemblyName[0] == char.MinValue)
        throw new ArgumentException(Environment.GetResourceString("Format_StringZeroLength"));
      this._Name = assemblyName;
      this.nInit();
    }

    /// <summary>
    ///   Возвращает значение, указывающее, совпадают ли два имени сборки.
    ///    Сравнение основано на именах простой сборки.
    /// </summary>
    /// <param name="reference">Ссылочное имя сборки.</param>
    /// <param name="definition">
    ///   Имя сборки, которое сравнивается с базовой сборкой.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если имена простой сборок одинаковы; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    public static bool ReferenceMatchesDefinition(AssemblyName reference, AssemblyName definition)
    {
      if (reference == definition)
        return true;
      return AssemblyName.ReferenceMatchesDefinitionInternal(reference, definition, true);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool ReferenceMatchesDefinitionInternal(AssemblyName reference, AssemblyName definition, bool parse);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern void nInit(out RuntimeAssembly assembly, bool forIntrospection, bool raiseResolveEvent);

    [SecurityCritical]
    internal void nInit()
    {
      RuntimeAssembly assembly = (RuntimeAssembly) null;
      this.nInit(out assembly, false, false);
    }

    internal void SetProcArchIndex(PortableExecutableKinds pek, ImageFileMachine ifm)
    {
      this.ProcessorArchitecture = AssemblyName.CalculateProcArchIndex(pek, ifm, this._Flags);
    }

    internal static ProcessorArchitecture CalculateProcArchIndex(PortableExecutableKinds pek, ImageFileMachine ifm, AssemblyNameFlags flags)
    {
      if ((flags & (AssemblyNameFlags) 240) == (AssemblyNameFlags) 112)
        return ProcessorArchitecture.None;
      if ((pek & PortableExecutableKinds.PE32Plus) == PortableExecutableKinds.PE32Plus)
      {
        switch (ifm)
        {
          case ImageFileMachine.I386:
            if ((pek & PortableExecutableKinds.ILOnly) == PortableExecutableKinds.ILOnly)
              return ProcessorArchitecture.MSIL;
            break;
          case ImageFileMachine.IA64:
            return ProcessorArchitecture.IA64;
          case ImageFileMachine.AMD64:
            return ProcessorArchitecture.Amd64;
        }
      }
      else
      {
        switch (ifm)
        {
          case ImageFileMachine.I386:
            return (pek & PortableExecutableKinds.Required32Bit) == PortableExecutableKinds.Required32Bit || (pek & PortableExecutableKinds.ILOnly) != PortableExecutableKinds.ILOnly ? ProcessorArchitecture.X86 : ProcessorArchitecture.MSIL;
          case ImageFileMachine.ARM:
            return ProcessorArchitecture.Arm;
        }
      }
      return ProcessorArchitecture.None;
    }

    internal void Init(string name, byte[] publicKey, byte[] publicKeyToken, Version version, CultureInfo cultureInfo, AssemblyHashAlgorithm hashAlgorithm, AssemblyVersionCompatibility versionCompatibility, string codeBase, AssemblyNameFlags flags, StrongNameKeyPair keyPair)
    {
      this._Name = name;
      if (publicKey != null)
      {
        this._PublicKey = new byte[publicKey.Length];
        Array.Copy((Array) publicKey, (Array) this._PublicKey, publicKey.Length);
      }
      if (publicKeyToken != null)
      {
        this._PublicKeyToken = new byte[publicKeyToken.Length];
        Array.Copy((Array) publicKeyToken, (Array) this._PublicKeyToken, publicKeyToken.Length);
      }
      if (version != (Version) null)
        this._Version = (Version) version.Clone();
      this._CultureInfo = cultureInfo;
      this._HashAlgorithm = hashAlgorithm;
      this._VersionCompatibility = versionCompatibility;
      this._CodeBase = codeBase;
      this._Flags = flags;
      this._StrongNameKeyPair = keyPair;
    }

    void _AssemblyName.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _AssemblyName.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _AssemblyName.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _AssemblyName.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    internal string GetNameWithPublicKey()
    {
      return this.Name + ", PublicKey=" + Hex.EncodeHexString(this.GetPublicKey());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern AssemblyName nGetFileInformation(string s);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern string nToString();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern byte[] nGetPublicKeyToken();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string EscapeCodeBase(string codeBase);
  }
}
