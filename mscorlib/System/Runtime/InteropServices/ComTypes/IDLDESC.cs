// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IDLDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Содержит информацию, необходимую для передачи элемента структуры, параметра или возвращаемого функцией значения из одного процесса в другой.
  /// </summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct IDLDESC
  {
    /// <summary>
    ///   Защищены. значение <see langword="null" />.
    /// </summary>
    public IntPtr dwReserved;
    /// <summary>
    ///   Указывает <see cref="T:System.Runtime.InteropServices.IDLFLAG" /> значение, описывающее тип.
    /// </summary>
    [__DynamicallyInvokable]
    public IDLFLAG wIDLFlags;
  }
}
