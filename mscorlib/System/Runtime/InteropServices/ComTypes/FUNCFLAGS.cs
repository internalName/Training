// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.FUNCFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>Указывает константы, определяющие свойства функции.</summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum FUNCFLAGS : short
  {
    [__DynamicallyInvokable] FUNCFLAG_FRESTRICTED = 1,
    [__DynamicallyInvokable] FUNCFLAG_FSOURCE = 2,
    [__DynamicallyInvokable] FUNCFLAG_FBINDABLE = 4,
    [__DynamicallyInvokable] FUNCFLAG_FREQUESTEDIT = 8,
    [__DynamicallyInvokable] FUNCFLAG_FDISPLAYBIND = 16, // 0x0010
    [__DynamicallyInvokable] FUNCFLAG_FDEFAULTBIND = 32, // 0x0020
    [__DynamicallyInvokable] FUNCFLAG_FHIDDEN = 64, // 0x0040
    [__DynamicallyInvokable] FUNCFLAG_FUSESGETLASTERROR = 128, // 0x0080
    [__DynamicallyInvokable] FUNCFLAG_FDEFAULTCOLLELEM = 256, // 0x0100
    [__DynamicallyInvokable] FUNCFLAG_FUIDEFAULT = 512, // 0x0200
    [__DynamicallyInvokable] FUNCFLAG_FNONBROWSABLE = 1024, // 0x0400
    [__DynamicallyInvokable] FUNCFLAG_FREPLACEABLE = 2048, // 0x0800
    [__DynamicallyInvokable] FUNCFLAG_FIMMEDIATEBIND = 4096, // 0x1000
  }
}
