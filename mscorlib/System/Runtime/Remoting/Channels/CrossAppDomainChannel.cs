// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.CrossAppDomainChannel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
  [Serializable]
  internal class CrossAppDomainChannel : IChannel, IChannelSender, IChannelReceiver
  {
    private static object staticSyncObject = new object();
    private static PermissionSet s_fullTrust = new PermissionSet(PermissionState.Unrestricted);
    private const string _channelName = "XAPPDMN";
    private const string _channelURI = "XAPPDMN_URI";

    private static CrossAppDomainChannel gAppDomainChannel
    {
      get
      {
        return Thread.GetDomain().RemotingData.ChannelServicesData.xadmessageSink;
      }
      set
      {
        Thread.GetDomain().RemotingData.ChannelServicesData.xadmessageSink = value;
      }
    }

    internal static CrossAppDomainChannel AppDomainChannel
    {
      get
      {
        if (CrossAppDomainChannel.gAppDomainChannel == null)
        {
          CrossAppDomainChannel appDomainChannel = new CrossAppDomainChannel();
          lock (CrossAppDomainChannel.staticSyncObject)
          {
            if (CrossAppDomainChannel.gAppDomainChannel == null)
              CrossAppDomainChannel.gAppDomainChannel = appDomainChannel;
          }
        }
        return CrossAppDomainChannel.gAppDomainChannel;
      }
    }

    [SecurityCritical]
    internal static void RegisterChannel()
    {
      ChannelServices.RegisterChannelInternal((IChannel) CrossAppDomainChannel.AppDomainChannel, false);
    }

    public virtual string ChannelName
    {
      [SecurityCritical] get
      {
        return "XAPPDMN";
      }
    }

    public virtual string ChannelURI
    {
      get
      {
        return "XAPPDMN_URI";
      }
    }

    public virtual int ChannelPriority
    {
      [SecurityCritical] get
      {
        return 100;
      }
    }

    [SecurityCritical]
    public string Parse(string url, out string objectURI)
    {
      objectURI = url;
      return (string) null;
    }

    public virtual object ChannelData
    {
      [SecurityCritical] get
      {
        return (object) new CrossAppDomainData(Context.DefaultContext.InternalContextID, Thread.GetDomain().GetId(), Identity.ProcessGuid);
      }
    }

    [SecurityCritical]
    public virtual IMessageSink CreateMessageSink(string url, object data, out string objectURI)
    {
      objectURI = (string) null;
      IMessageSink messageSink = (IMessageSink) null;
      if (url != null && data == null)
      {
        if (url.StartsWith("XAPPDMN", StringComparison.Ordinal))
          throw new RemotingException(Environment.GetResourceString("Remoting_AppDomains_NYI"));
      }
      else
      {
        CrossAppDomainData xadData = data as CrossAppDomainData;
        if (xadData != null && xadData.ProcessGuid.Equals(Identity.ProcessGuid))
          messageSink = (IMessageSink) CrossAppDomainSink.FindOrCreateSink(xadData);
      }
      return messageSink;
    }

    [SecurityCritical]
    public virtual string[] GetUrlsForUri(string objectURI)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    [SecurityCritical]
    public virtual void StartListening(object data)
    {
    }

    [SecurityCritical]
    public virtual void StopListening(object data)
    {
    }
  }
}
