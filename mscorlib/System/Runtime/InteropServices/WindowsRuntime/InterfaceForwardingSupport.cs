// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.InterfaceForwardingSupport
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Flags]
  internal enum InterfaceForwardingSupport
  {
    None = 0,
    IBindableVector = 1,
    IVector = 2,
    IBindableVectorView = 4,
    IVectorView = 8,
    IBindableIterableOrIIterable = 16, // 0x00000010
  }
}
