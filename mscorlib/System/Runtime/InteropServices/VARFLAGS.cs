// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.VARFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.VARFLAGS" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.VARFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Flags]
  [Serializable]
  public enum VARFLAGS : short
  {
    VARFLAG_FREADONLY = 1,
    VARFLAG_FSOURCE = 2,
    VARFLAG_FBINDABLE = 4,
    VARFLAG_FREQUESTEDIT = 8,
    VARFLAG_FDISPLAYBIND = 16, // 0x0010
    VARFLAG_FDEFAULTBIND = 32, // 0x0020
    VARFLAG_FHIDDEN = 64, // 0x0040
    VARFLAG_FRESTRICTED = 128, // 0x0080
    VARFLAG_FDEFAULTCOLLELEM = 256, // 0x0100
    VARFLAG_FUIDEFAULT = 512, // 0x0200
    VARFLAG_FNONBROWSABLE = 1024, // 0x0400
    VARFLAG_FREPLACEABLE = 2048, // 0x0800
    VARFLAG_FIMMEDIATEBIND = 4096, // 0x1000
  }
}
