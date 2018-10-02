// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TYPEFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEFLAGS" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Flags]
  [Serializable]
  public enum TYPEFLAGS : short
  {
    TYPEFLAG_FAPPOBJECT = 1,
    TYPEFLAG_FCANCREATE = 2,
    TYPEFLAG_FLICENSED = 4,
    TYPEFLAG_FPREDECLID = 8,
    TYPEFLAG_FHIDDEN = 16, // 0x0010
    TYPEFLAG_FCONTROL = 32, // 0x0020
    TYPEFLAG_FDUAL = 64, // 0x0040
    TYPEFLAG_FNONEXTENSIBLE = 128, // 0x0080
    TYPEFLAG_FOLEAUTOMATION = 256, // 0x0100
    TYPEFLAG_FRESTRICTED = 512, // 0x0200
    TYPEFLAG_FAGGREGATABLE = 1024, // 0x0400
    TYPEFLAG_FREPLACEABLE = 2048, // 0x0800
    TYPEFLAG_FDISPATCHABLE = 4096, // 0x1000
    TYPEFLAG_FREVERSEBIND = 8192, // 0x2000
    TYPEFLAG_FPROXY = 16384, // 0x4000
  }
}
