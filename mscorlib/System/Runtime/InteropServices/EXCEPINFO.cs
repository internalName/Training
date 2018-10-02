// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.EXCEPINFO
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.EXCEPINFO" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.EXCEPINFO instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct EXCEPINFO
  {
    /// <summary>Представляет код ошибки, определяющий ошибку.</summary>
    public short wCode;
    /// <summary>
    ///   Это поле является зарезервированным; должно быть равным 0.
    /// </summary>
    public short wReserved;
    /// <summary>
    ///   Указывает имя источника исключения.
    ///    Как правило это имя приложения.
    /// </summary>
    [MarshalAs(UnmanagedType.BStr)]
    public string bstrSource;
    /// <summary>Описывает ошибку, предназначенную для клиента.</summary>
    [MarshalAs(UnmanagedType.BStr)]
    public string bstrDescription;
    /// <summary>
    ///   Содержит полное диска, путь и имя файла справки с дополнительными сведениями об ошибке.
    /// </summary>
    [MarshalAs(UnmanagedType.BStr)]
    public string bstrHelpFile;
    /// <summary>
    ///   Указывает идентификатор контекста справки раздела в файле справки.
    /// </summary>
    public int dwHelpContext;
    /// <summary>
    ///   Это поле является зарезервированным; необходимо задать значение <see langword="null" />.
    /// </summary>
    public IntPtr pvReserved;
    /// <summary>
    ///   Представляет указатель на функцию, которая принимает <see cref="T:System.Runtime.InteropServices.EXCEPINFO" /> структуру в качестве аргумента и возвращает значение HRESULT.
    ///    Если нежелательно отложенное заполнить это поле имеет значение <see langword="null" />.
    /// </summary>
    public IntPtr pfnDeferredFillIn;
  }
}
