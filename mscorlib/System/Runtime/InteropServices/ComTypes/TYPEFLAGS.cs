// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.TYPEFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>Определяет свойства и атрибуты описания типа.</summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum TYPEFLAGS : short
  {
    [__DynamicallyInvokable] TYPEFLAG_FAPPOBJECT = 1,
    [__DynamicallyInvokable] TYPEFLAG_FCANCREATE = 2,
    [__DynamicallyInvokable] TYPEFLAG_FLICENSED = 4,
    [__DynamicallyInvokable] TYPEFLAG_FPREDECLID = 8,
    [__DynamicallyInvokable] TYPEFLAG_FHIDDEN = 16, // 0x0010
    [__DynamicallyInvokable] TYPEFLAG_FCONTROL = 32, // 0x0020
    [__DynamicallyInvokable] TYPEFLAG_FDUAL = 64, // 0x0040
    [__DynamicallyInvokable] TYPEFLAG_FNONEXTENSIBLE = 128, // 0x0080
    [__DynamicallyInvokable] TYPEFLAG_FOLEAUTOMATION = 256, // 0x0100
    [__DynamicallyInvokable] TYPEFLAG_FRESTRICTED = 512, // 0x0200
    [__DynamicallyInvokable] TYPEFLAG_FAGGREGATABLE = 1024, // 0x0400
    [__DynamicallyInvokable] TYPEFLAG_FREPLACEABLE = 2048, // 0x0800
    [__DynamicallyInvokable] TYPEFLAG_FDISPATCHABLE = 4096, // 0x1000
    [__DynamicallyInvokable] TYPEFLAG_FREVERSEBIND = 8192, // 0x2000
    [__DynamicallyInvokable] TYPEFLAG_FPROXY = 16384, // 0x4000
  }
}
