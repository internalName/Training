// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.DelayLoadClientChannelEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting
{
  internal class DelayLoadClientChannelEntry
  {
    private RemotingXmlConfigFileData.ChannelEntry _entry;
    private IChannelSender _channel;
    private bool _bRegistered;
    private bool _ensureSecurity;

    internal DelayLoadClientChannelEntry(RemotingXmlConfigFileData.ChannelEntry entry, bool ensureSecurity)
    {
      this._entry = entry;
      this._channel = (IChannelSender) null;
      this._bRegistered = false;
      this._ensureSecurity = ensureSecurity;
    }

    internal IChannelSender Channel
    {
      [SecurityCritical] get
      {
        if (this._channel == null && !this._bRegistered)
        {
          this._channel = (IChannelSender) RemotingConfigHandler.CreateChannelFromConfigEntry(this._entry);
          this._entry = (RemotingXmlConfigFileData.ChannelEntry) null;
        }
        return this._channel;
      }
    }

    internal void RegisterChannel()
    {
      ChannelServices.RegisterChannel((IChannel) this._channel, this._ensureSecurity);
      this._bRegistered = true;
      this._channel = (IChannelSender) null;
    }
  }
}
