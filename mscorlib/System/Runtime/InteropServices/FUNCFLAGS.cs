// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.FUNCFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCFLAGS" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Flags]
  [Serializable]
  public enum FUNCFLAGS : short
  {
    FUNCFLAG_FRESTRICTED = 1,
    FUNCFLAG_FSOURCE = 2,
    FUNCFLAG_FBINDABLE = 4,
    FUNCFLAG_FREQUESTEDIT = 8,
    FUNCFLAG_FDISPLAYBIND = 16, // 0x0010
    FUNCFLAG_FDEFAULTBIND = 32, // 0x0020
    FUNCFLAG_FHIDDEN = 64, // 0x0040
    FUNCFLAG_FUSESGETLASTERROR = 128, // 0x0080
    FUNCFLAG_FDEFAULTCOLLELEM = 256, // 0x0100
    FUNCFLAG_FUIDEFAULT = 512, // 0x0200
    FUNCFLAG_FNONBROWSABLE = 1024, // 0x0400
    FUNCFLAG_FREPLACEABLE = 2048, // 0x0800
    FUNCFLAG_FIMMEDIATEBIND = 4096, // 0x1000
  }
}
