// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.IDLDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.IDLDESC" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct IDLDESC
  {
    /// <summary>
    ///   Защищены. значение <see langword="null" />.
    /// </summary>
    public int dwReserved;
    /// <summary>
    ///   Указывает <see cref="T:System.Runtime.InteropServices.IDLFLAG" /> значение, описывающее тип.
    /// </summary>
    public IDLFLAG wIDLFlags;
  }
}
