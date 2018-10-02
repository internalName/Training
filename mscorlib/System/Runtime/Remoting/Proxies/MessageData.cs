// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Proxies.MessageData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Remoting.Proxies
{
  internal struct MessageData
  {
    internal IntPtr pFrame;
    internal IntPtr pMethodDesc;
    internal IntPtr pDelegateMD;
    internal IntPtr pSig;
    internal IntPtr thGoverningType;
    internal int iFlags;
  }
}
