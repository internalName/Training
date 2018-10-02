// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ServerIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting
{
  internal class ServerIdentity : Identity
  {
    internal Context _srvCtx;
    internal IMessageSink _serverObjectChain;
    internal StackBuilderSink _stackBuilderSink;
    internal DynamicPropertyHolder _dphSrv;
    internal Type _srvType;
    private ServerIdentity.LastCalledType _lastCalledType;
    internal bool _bMarshaledAsSpecificType;
    internal int _firstCallDispatched;
    internal GCHandle _srvIdentityHandle;

    internal Type GetLastCalledType(string newTypeName)
    {
      ServerIdentity.LastCalledType lastCalledType = this._lastCalledType;
      if (lastCalledType == null)
        return (Type) null;
      string typeName = lastCalledType.typeName;
      Type type = lastCalledType.type;
      if (typeName == null || type == (Type) null)
        return (Type) null;
      if (typeName.Equals(newTypeName))
        return type;
      return (Type) null;
    }

    internal void SetLastCalledType(string newTypeName, Type newType)
    {
      this._lastCalledType = new ServerIdentity.LastCalledType()
      {
        typeName = newTypeName,
        type = newType
      };
    }

    [SecurityCritical]
    internal void SetHandle()
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter((object) this, ref lockTaken);
        if (!this._srvIdentityHandle.IsAllocated)
          this._srvIdentityHandle = new GCHandle((object) this, GCHandleType.Normal);
        else
          this._srvIdentityHandle.Target = (object) this;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) this);
      }
    }

    [SecurityCritical]
    internal void ResetHandle()
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter((object) this, ref lockTaken);
        this._srvIdentityHandle.Target = (object) null;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) this);
      }
    }

    internal GCHandle GetHandle()
    {
      return this._srvIdentityHandle;
    }

    [SecurityCritical]
    internal ServerIdentity(MarshalByRefObject obj, Context serverCtx)
      : base(obj is ContextBoundObject)
    {
      if (obj != null)
        this._srvType = RemotingServices.IsTransparentProxy((object) obj) ? RemotingServices.GetRealProxy((object) obj).GetProxiedType() : obj.GetType();
      this._srvCtx = serverCtx;
      this._serverObjectChain = (IMessageSink) null;
      this._stackBuilderSink = (StackBuilderSink) null;
    }

    [SecurityCritical]
    internal ServerIdentity(MarshalByRefObject obj, Context serverCtx, string uri)
      : this(obj, serverCtx)
    {
      this.SetOrCreateURI(uri, true);
    }

    internal Context ServerContext
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this._srvCtx;
      }
    }

    internal void SetSingleCallObjectMode()
    {
      this._flags |= 512;
    }

    internal void SetSingletonObjectMode()
    {
      this._flags |= 1024;
    }

    internal bool IsSingleCall()
    {
      return (uint) (this._flags & 512) > 0U;
    }

    internal bool IsSingleton()
    {
      return (uint) (this._flags & 1024) > 0U;
    }

    [SecurityCritical]
    internal IMessageSink GetServerObjectChain(out MarshalByRefObject obj)
    {
      obj = (MarshalByRefObject) null;
      if (!this.IsSingleCall())
      {
        if (this._serverObjectChain == null)
        {
          bool lockTaken = false;
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
            Monitor.Enter((object) this, ref lockTaken);
            if (this._serverObjectChain == null)
              this._serverObjectChain = this._srvCtx.CreateServerObjectChain(this.TPOrObject);
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit((object) this);
          }
        }
        return this._serverObjectChain;
      }
      MarshalByRefObject serverObj;
      IMessageSink messageSink;
      if (this._tpOrObject != null && this._firstCallDispatched == 0 && Interlocked.CompareExchange(ref this._firstCallDispatched, 1, 0) == 0)
      {
        serverObj = (MarshalByRefObject) this._tpOrObject;
        messageSink = this._serverObjectChain ?? this._srvCtx.CreateServerObjectChain(serverObj);
      }
      else
      {
        serverObj = (MarshalByRefObject) Activator.CreateInstance(this._srvType, true);
        if (RemotingServices.GetObjectUri(serverObj) != null)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_WellKnown_CtorCantMarshal"), (object) this.URI));
        if (!RemotingServices.IsTransparentProxy((object) serverObj))
          serverObj.__RaceSetServerIdentity(this);
        else
          RemotingServices.GetRealProxy((object) serverObj).IdentityObject = (Identity) this;
        messageSink = this._srvCtx.CreateServerObjectChain(serverObj);
      }
      obj = serverObj;
      return messageSink;
    }

    internal Type ServerType
    {
      get
      {
        return this._srvType;
      }
      set
      {
        this._srvType = value;
      }
    }

    internal bool MarshaledAsSpecificType
    {
      get
      {
        return this._bMarshaledAsSpecificType;
      }
      set
      {
        this._bMarshaledAsSpecificType = value;
      }
    }

    [SecurityCritical]
    internal IMessageSink RaceSetServerObjectChain(IMessageSink serverObjectChain)
    {
      if (this._serverObjectChain == null)
      {
        bool lockTaken = false;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          Monitor.Enter((object) this, ref lockTaken);
          if (this._serverObjectChain == null)
            this._serverObjectChain = serverObjectChain;
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit((object) this);
        }
      }
      return this._serverObjectChain;
    }

    [SecurityCritical]
    internal bool AddServerSideDynamicProperty(IDynamicProperty prop)
    {
      if (this._dphSrv == null)
      {
        DynamicPropertyHolder dynamicPropertyHolder = new DynamicPropertyHolder();
        bool lockTaken = false;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          Monitor.Enter((object) this, ref lockTaken);
          if (this._dphSrv == null)
            this._dphSrv = dynamicPropertyHolder;
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit((object) this);
        }
      }
      return this._dphSrv.AddDynamicProperty(prop);
    }

    [SecurityCritical]
    internal bool RemoveServerSideDynamicProperty(string name)
    {
      if (this._dphSrv == null)
        throw new ArgumentException(Environment.GetResourceString("Arg_PropNotFound"));
      return this._dphSrv.RemoveDynamicProperty(name);
    }

    internal ArrayWithSize ServerSideDynamicSinks
    {
      [SecurityCritical] get
      {
        if (this._dphSrv == null)
          return (ArrayWithSize) null;
        return this._dphSrv.DynamicSinks;
      }
    }

    [SecurityCritical]
    internal override void AssertValid()
    {
      if (this.TPOrObject == null)
        return;
      RemotingServices.IsTransparentProxy((object) this.TPOrObject);
    }

    private class LastCalledType
    {
      public string typeName;
      public Type type;
    }
  }
}
