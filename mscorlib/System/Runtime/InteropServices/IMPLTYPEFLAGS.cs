// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.IMPLTYPEFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Flags]
  [Serializable]
  public enum IMPLTYPEFLAGS
  {
    IMPLTYPEFLAG_FDEFAULT = 1,
    IMPLTYPEFLAG_FSOURCE = 2,
    IMPLTYPEFLAG_FRESTRICTED = 4,
    IMPLTYPEFLAG_FDEFAULTVTABLE = 8,
  }
}
