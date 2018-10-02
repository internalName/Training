// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.ELEMDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Содержит описание типа и сведения о процессе передачи для переменной, функции или параметра функции.
  /// </summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct ELEMDESC
  {
    /// <summary>Определяет тип элемента.</summary>
    [__DynamicallyInvokable]
    public TYPEDESC tdesc;
    /// <summary>Содержит сведения об элементе.</summary>
    [__DynamicallyInvokable]
    public ELEMDESC.DESCUNION desc;

    /// <summary>Содержит сведения об элементе.</summary>
    [__DynamicallyInvokable]
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct DESCUNION
    {
      /// <summary>
      ///   Содержит сведения для удаленного доступа к элементу.
      /// </summary>
      [__DynamicallyInvokable]
      [FieldOffset(0)]
      public IDLDESC idldesc;
      /// <summary>Содержит сведения о параметре.</summary>
      [__DynamicallyInvokable]
      [FieldOffset(0)]
      public PARAMDESC paramdesc;
    }
  }
}
