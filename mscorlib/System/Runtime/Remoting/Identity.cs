// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Identity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Cryptography;
using System.Threading;

namespace System.Runtime.Remoting
{
  internal class Identity
  {
    private static string s_originalAppDomainGuid = Guid.NewGuid().ToString().Replace('-', '_');
    private static string s_configuredAppDomainGuid = (string) null;
    private static string s_originalAppDomainGuidString = "/" + Identity.s_originalAppDomainGuid.ToLower(CultureInfo.InvariantCulture) + "/";
    private static string s_configuredAppDomainGuidString = (string) null;
    private static string s_IDGuidString = "/" + Identity.s_originalAppDomainGuid.ToLower(CultureInfo.InvariantCulture) + "/";
    private static RNGCryptoServiceProvider s_rng = new RNGCryptoServiceProvider();
    protected const int IDFLG_DISCONNECTED_FULL = 1;
    protected const int IDFLG_DISCONNECTED_REM = 2;
    protected const int IDFLG_IN_IDTABLE = 4;
    protected const int IDFLG_CONTEXT_BOUND = 16;
    protected const int IDFLG_WELLKNOWN = 256;
    protected const int IDFLG_SERVER_SINGLECALL = 512;
    protected const int IDFLG_SERVER_SINGLETON = 1024;
    internal int _flags;
    internal object _tpOrObject;
    protected string _ObjURI;
    protected string _URL;
    internal object _objRef;
    internal object _channelSink;
    internal object _envoyChain;
    internal DynamicPropertyHolder _dph;
    internal Lease _lease;
    private volatile bool _initializing;

    internal static string ProcessIDGuid
    {
      get
      {
        return SharedStatics.Remoting_Identity_IDGuid;
      }
    }

    internal static string AppDomainUniqueId
    {
      get
      {
        if (Identity.s_configuredAppDomainGuid != null)
          return Identity.s_configuredAppDomainGuid;
        return Identity.s_originalAppDomainGuid;
      }
    }

    internal static string IDGuidString
    {
      get
      {
        return Identity.s_IDGuidString;
      }
    }

    internal static string RemoveAppNameOrAppGuidIfNecessary(string uri)
    {
      if (uri == null || uri.Length <= 1 || uri[0] != '/')
        return uri;
      if (Identity.s_configuredAppDomainGuidString != null)
      {
        string domainGuidString = Identity.s_configuredAppDomainGuidString;
        if (uri.Length > domainGuidString.Length && Identity.StringStartsWith(uri, domainGuidString))
          return uri.Substring(domainGuidString.Length);
      }
      string domainGuidString1 = Identity.s_originalAppDomainGuidString;
      if (uri.Length > domainGuidString1.Length && Identity.StringStartsWith(uri, domainGuidString1))
        return uri.Substring(domainGuidString1.Length);
      string applicationName = RemotingConfiguration.ApplicationName;
      if (applicationName != null && uri.Length > applicationName.Length + 2 && (string.Compare(uri, 1, applicationName, 0, applicationName.Length, true, CultureInfo.InvariantCulture) == 0 && uri[applicationName.Length + 1] == '/'))
        return uri.Substring(applicationName.Length + 2);
      uri = uri.Substring(1);
      return uri;
    }

    private static bool StringStartsWith(string s1, string prefix)
    {
      if (s1.Length < prefix.Length)
        return false;
      return string.CompareOrdinal(s1, 0, prefix, 0, prefix.Length) == 0;
    }

    internal static string ProcessGuid
    {
      get
      {
        return Identity.ProcessIDGuid;
      }
    }

    private static int GetNextSeqNum()
    {
      return SharedStatics.Remoting_Identity_GetNextSeqNum();
    }

    private static byte[] GetRandomBytes()
    {
      byte[] data = new byte[18];
      Identity.s_rng.GetBytes(data);
      return data;
    }

    internal Identity(string objURI, string URL)
    {
      if (URL != null)
      {
        this._flags |= 256;
        this._URL = URL;
      }
      this.SetOrCreateURI(objURI, true);
    }

    internal Identity(bool bContextBound)
    {
      if (!bContextBound)
        return;
      this._flags |= 16;
    }

    internal bool IsContextBound
    {
      get
      {
        return (this._flags & 16) == 16;
      }
    }

    internal bool IsInitializing
    {
      get
      {
        return this._initializing;
      }
      set
      {
        this._initializing = value;
      }
    }

    internal bool IsWellKnown()
    {
      return (this._flags & 256) == 256;
    }

    internal void SetInIDTable()
    {
      int flags;
      int num;
      do
      {
        flags = this._flags;
        num = this._flags | 4;
      }
      while (flags != Interlocked.CompareExchange(ref this._flags, num, flags));
    }

    [SecurityCritical]
    internal void ResetInIDTable(bool bResetURI)
    {
      int flags;
      int num;
      do
      {
        flags = this._flags;
        num = this._flags & -5;
      }
      while (flags != Interlocked.CompareExchange(ref this._flags, num, flags));
      if (!bResetURI)
        return;
      ((ObjRef) this._objRef).URI = (string) null;
      this._ObjURI = (string) null;
    }

    internal bool IsInIDTable()
    {
      return (this._flags & 4) == 4;
    }

    internal void SetFullyConnected()
    {
      int flags;
      int num;
      do
      {
        flags = this._flags;
        num = this._flags & -4;
      }
      while (flags != Interlocked.CompareExchange(ref this._flags, num, flags));
    }

    internal bool IsFullyDisconnected()
    {
      return (this._flags & 1) == 1;
    }

    internal bool IsRemoteDisconnected()
    {
      return (this._flags & 2) == 2;
    }

    internal bool IsDisconnected()
    {
      if (!this.IsFullyDisconnected())
        return this.IsRemoteDisconnected();
      return true;
    }

    internal string URI
    {
      get
      {
        if (this.IsWellKnown())
          return this._URL;
        return this._ObjURI;
      }
    }

    internal string ObjURI
    {
      get
      {
        return this._ObjURI;
      }
    }

    internal MarshalByRefObject TPOrObject
    {
      get
      {
        return (MarshalByRefObject) this._tpOrObject;
      }
    }

    internal object RaceSetTransparentProxy(object tpObj)
    {
      if (this._tpOrObject == null)
        Interlocked.CompareExchange(ref this._tpOrObject, tpObj, (object) null);
      return this._tpOrObject;
    }

    internal ObjRef ObjectRef
    {
      [SecurityCritical] get
      {
        return (ObjRef) this._objRef;
      }
    }

    [SecurityCritical]
    internal ObjRef RaceSetObjRef(ObjRef objRefGiven)
    {
      if (this._objRef == null)
        Interlocked.CompareExchange(ref this._objRef, (object) objRefGiven, (object) null);
      return (ObjRef) this._objRef;
    }

    internal IMessageSink ChannelSink
    {
      get
      {
        return (IMessageSink) this._channelSink;
      }
    }

    internal IMessageSink RaceSetChannelSink(IMessageSink channelSink)
    {
      if (this._channelSink == null)
        Interlocked.CompareExchange(ref this._channelSink, (object) channelSink, (object) null);
      return (IMessageSink) this._channelSink;
    }

    internal IMessageSink EnvoyChain
    {
      get
      {
        return (IMessageSink) this._envoyChain;
      }
    }

    internal Lease Lease
    {
      get
      {
        return this._lease;
      }
      set
      {
        this._lease = value;
      }
    }

    internal IMessageSink RaceSetEnvoyChain(IMessageSink envoyChain)
    {
      if (this._envoyChain == null)
        Interlocked.CompareExchange(ref this._envoyChain, (object) envoyChain, (object) null);
      return (IMessageSink) this._envoyChain;
    }

    internal void SetOrCreateURI(string uri)
    {
      this.SetOrCreateURI(uri, false);
    }

    internal void SetOrCreateURI(string uri, bool bIdCtor)
    {
      if (!bIdCtor && this._ObjURI != null)
        throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
      if (uri == null)
        this._ObjURI = (Identity.IDGuidString + Convert.ToBase64String(Identity.GetRandomBytes()).Replace('/', '_') + "_" + Identity.GetNextSeqNum().ToString((IFormatProvider) CultureInfo.InvariantCulture.NumberFormat) + ".rem").ToLower(CultureInfo.InvariantCulture);
      else if (this is ServerIdentity)
        this._ObjURI = Identity.IDGuidString + uri;
      else
        this._ObjURI = uri;
    }

    internal static string GetNewLogicalCallID()
    {
      return Identity.IDGuidString + (object) Identity.GetNextSeqNum();
    }

    [SecurityCritical]
    [Conditional("_DEBUG")]
    internal virtual void AssertValid()
    {
      if (this.URI == null)
        return;
      IdentityHolder.ResolveIdentity(this.URI);
    }

    [SecurityCritical]
    internal bool AddProxySideDynamicProperty(IDynamicProperty prop)
    {
      lock (this)
      {
        if (this._dph == null)
        {
          DynamicPropertyHolder dynamicPropertyHolder = new DynamicPropertyHolder();
          lock (this)
          {
            if (this._dph == null)
              this._dph = dynamicPropertyHolder;
          }
        }
        return this._dph.AddDynamicProperty(prop);
      }
    }

    [SecurityCritical]
    internal bool RemoveProxySideDynamicProperty(string name)
    {
      lock (this)
      {
        if (this._dph == null)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Contexts_NoProperty"), (object) name));
        return this._dph.RemoveDynamicProperty(name);
      }
    }

    internal ArrayWithSize ProxySideDynamicSinks
    {
      [SecurityCritical] get
      {
        if (this._dph == null)
          return (ArrayWithSize) null;
        return this._dph.DynamicSinks;
      }
    }
  }
}
