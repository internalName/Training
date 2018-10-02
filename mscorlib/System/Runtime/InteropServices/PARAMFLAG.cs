// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.PARAMFLAG
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.PARAMFLAG" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.PARAMFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Flags]
  [Serializable]
  public enum PARAMFLAG : short
  {
    PARAMFLAG_NONE = 0,
    PARAMFLAG_FIN = 1,
    PARAMFLAG_FOUT = 2,
    PARAMFLAG_FLCID = 4,
    PARAMFLAG_FRETVAL = 8,
    PARAMFLAG_FOPT = 16, // 0x0010
    PARAMFLAG_FHASDEFAULT = 32, // 0x0020
    PARAMFLAG_FHASCUSTDATA = 64, // 0x0040
  }
}
