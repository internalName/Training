// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.TYPEATTR
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Содержит атрибуты <see langword="UCOMITypeInfo" />.
  /// </summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct TYPEATTR
  {
    /// <summary>
    ///   Константа, которая используется с <see cref="F:System.Runtime.InteropServices.TYPEATTR.memidConstructor" /> и <see cref="F:System.Runtime.InteropServices.TYPEATTR.memidDestructor" /> полей.
    /// </summary>
    [__DynamicallyInvokable]
    public const int MEMBER_ID_NIL = -1;
    /// <summary>Идентификатор GUID информации о типе.</summary>
    [__DynamicallyInvokable]
    public Guid guid;
    /// <summary>
    ///   Языковой стандарт имен элементов и строк документации.
    /// </summary>
    [__DynamicallyInvokable]
    public int lcid;
    /// <summary>Зарезервировано для будущего использования.</summary>
    [__DynamicallyInvokable]
    public int dwReserved;
    /// <summary>
    ///   Идентификатор конструктора или <see cref="F:System.Runtime.InteropServices.TYPEATTR.MEMBER_ID_NIL" /> Если нет.
    /// </summary>
    [__DynamicallyInvokable]
    public int memidConstructor;
    /// <summary>
    ///   Идентификатор деструктора, или <see cref="F:System.Runtime.InteropServices.TYPEATTR.MEMBER_ID_NIL" /> Если нет.
    /// </summary>
    [__DynamicallyInvokable]
    public int memidDestructor;
    /// <summary>Зарезервировано для будущего использования.</summary>
    public IntPtr lpstrSchema;
    /// <summary>Размер экземпляра этого типа.</summary>
    [__DynamicallyInvokable]
    public int cbSizeInstance;
    /// <summary>
    ///   A <see cref="T:System.Runtime.InteropServices.TYPEKIND" /> значение, описывающее тип эти сведения.
    /// </summary>
    [__DynamicallyInvokable]
    public TYPEKIND typekind;
    /// <summary>
    ///   Указывает количество функций в интерфейсе, описанном структурой.
    /// </summary>
    [__DynamicallyInvokable]
    public short cFuncs;
    /// <summary>
    ///   Указывает число переменных и полей данных в интерфейсе, описанном структурой.
    /// </summary>
    [__DynamicallyInvokable]
    public short cVars;
    /// <summary>
    ///   Указывает количество реализованных интерфейсов в интерфейсе, описанном структурой.
    /// </summary>
    [__DynamicallyInvokable]
    public short cImplTypes;
    /// <summary>
    ///   Размер таблицы виртуальных методов этого типа (VTBL).
    /// </summary>
    [__DynamicallyInvokable]
    public short cbSizeVft;
    /// <summary>
    ///   Задает выравнивание по границе байта для экземпляра этого типа.
    /// </summary>
    [__DynamicallyInvokable]
    public short cbAlignment;
    /// <summary>
    ///   A <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> значение, описывающее эту информацию.
    /// </summary>
    [__DynamicallyInvokable]
    public TYPEFLAGS wTypeFlags;
    /// <summary>Основной номер версии.</summary>
    [__DynamicallyInvokable]
    public short wMajorVerNum;
    /// <summary>Дополнительный номер версии.</summary>
    [__DynamicallyInvokable]
    public short wMinorVerNum;
    /// <summary>
    ///   Если <see cref="F:System.Runtime.InteropServices.TYPEATTR.typekind" /> == <see cref="F:System.Runtime.InteropServices.TYPEKIND.TKIND_ALIAS" />, указывает тип, для которого этот тип является псевдонимом.
    /// </summary>
    [__DynamicallyInvokable]
    public TYPEDESC tdescAlias;
    /// <summary>Атрибуты IDL описанного типа.</summary>
    [__DynamicallyInvokable]
    public IDLDESC idldescType;
  }
}
