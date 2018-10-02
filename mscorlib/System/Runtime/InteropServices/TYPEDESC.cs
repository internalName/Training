// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TYPEDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEDESC" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct TYPEDESC
  {
    /// <summary>
    ///   Если переменная является <see langword="VT_SAFEARRAY" /> или <see langword="VT_PTR" />,  <see cref="F:System.Runtime.InteropServices.TYPEDESC.lpValue" /> поле содержит указатель на <see langword="TYPEDESC" /> указывающий тип элемента.
    /// </summary>
    public IntPtr lpValue;
    /// <summary>
    ///   Показывает тип variant элемента, описанного в этом <see langword="TYPEDESC" />.
    /// </summary>
    public short vt;
  }
}
