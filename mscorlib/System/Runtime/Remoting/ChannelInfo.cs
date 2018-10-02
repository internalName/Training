// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ChannelInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting
{
  [Serializable]
  internal sealed class ChannelInfo : IChannelInfo
  {
    private object[] channelData;

    [SecurityCritical]
    internal ChannelInfo()
    {
      this.ChannelData = ChannelServices.CurrentChannelData;
    }

    public object[] ChannelData
    {
      [SecurityCritical] get
      {
        return this.channelData;
      }
      [SecurityCritical] set
      {
        this.channelData = value;
      }
    }
  }
}
