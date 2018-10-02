// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.RegisteredChannel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Remoting.Channels
{
  internal class RegisteredChannel
  {
    private IChannel channel;
    private byte flags;
    private const byte SENDER = 1;
    private const byte RECEIVER = 2;

    internal RegisteredChannel(IChannel chnl)
    {
      this.channel = chnl;
      this.flags = (byte) 0;
      if (chnl is IChannelSender)
        this.flags |= (byte) 1;
      if (!(chnl is IChannelReceiver))
        return;
      this.flags |= (byte) 2;
    }

    internal virtual IChannel Channel
    {
      get
      {
        return this.channel;
      }
    }

    internal virtual bool IsSender()
    {
      return ((uint) this.flags & 1U) > 0U;
    }

    internal virtual bool IsReceiver()
    {
      return ((uint) this.flags & 2U) > 0U;
    }
  }
}
