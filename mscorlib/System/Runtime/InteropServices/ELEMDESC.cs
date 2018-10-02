// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ELEMDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.ELEMDESC" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.ELEMDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct ELEMDESC
  {
    /// <summary>Определяет тип элемента.</summary>
    public TYPEDESC tdesc;
    /// <summary>Содержит сведения об элементе.</summary>
    public ELEMDESC.DESCUNION desc;

    /// <summary>
    ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.ELEMDESC.DESCUNION" />.
    /// </summary>
    [ComVisible(false)]
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct DESCUNION
    {
      /// <summary>
      ///   Содержит сведения для удаленного доступа к элементу.
      /// </summary>
      [FieldOffset(0)]
      public IDLDESC idldesc;
      /// <summary>Содержит сведения о параметре.</summary>
      [FieldOffset(0)]
      public PARAMDESC paramdesc;
    }
  }
}
