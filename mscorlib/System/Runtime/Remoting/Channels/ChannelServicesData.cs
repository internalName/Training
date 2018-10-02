// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ChannelServicesData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Remoting.Channels
{
  internal class ChannelServicesData
  {
    internal long remoteCalls;
    internal CrossContextChannel xctxmessageSink;
    internal CrossAppDomainChannel xadmessageSink;
    internal bool fRegisterWellKnownChannels;
  }
}
