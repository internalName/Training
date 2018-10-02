// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.DispatchChannelSinkProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting.Channels
{
  internal class DispatchChannelSinkProvider : IServerChannelSinkProvider
  {
    internal DispatchChannelSinkProvider()
    {
    }

    [SecurityCritical]
    public void GetChannelData(IChannelDataStore channelData)
    {
    }

    [SecurityCritical]
    public IServerChannelSink CreateSink(IChannelReceiver channel)
    {
      return (IServerChannelSink) new DispatchChannelSink();
    }

    public IServerChannelSinkProvider Next
    {
      [SecurityCritical] get
      {
        return (IServerChannelSinkProvider) null;
      }
      [SecurityCritical] set
      {
        throw new NotSupportedException();
      }
    }
  }
}
