// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.VARFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Указывает константы, определяющие свойства переменной.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum VARFLAGS : short
  {
    [__DynamicallyInvokable] VARFLAG_FREADONLY = 1,
    [__DynamicallyInvokable] VARFLAG_FSOURCE = 2,
    [__DynamicallyInvokable] VARFLAG_FBINDABLE = 4,
    [__DynamicallyInvokable] VARFLAG_FREQUESTEDIT = 8,
    [__DynamicallyInvokable] VARFLAG_FDISPLAYBIND = 16, // 0x0010
    [__DynamicallyInvokable] VARFLAG_FDEFAULTBIND = 32, // 0x0020
    [__DynamicallyInvokable] VARFLAG_FHIDDEN = 64, // 0x0040
    [__DynamicallyInvokable] VARFLAG_FRESTRICTED = 128, // 0x0080
    [__DynamicallyInvokable] VARFLAG_FDEFAULTCOLLELEM = 256, // 0x0100
    [__DynamicallyInvokable] VARFLAG_FUIDEFAULT = 512, // 0x0200
    [__DynamicallyInvokable] VARFLAG_FNONBROWSABLE = 1024, // 0x0400
    [__DynamicallyInvokable] VARFLAG_FREPLACEABLE = 2048, // 0x0800
    [__DynamicallyInvokable] VARFLAG_FIMMEDIATEBIND = 4096, // 0x1000
  }
}
