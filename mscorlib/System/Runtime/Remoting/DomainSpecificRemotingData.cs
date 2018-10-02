// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.DomainSpecificRemotingData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting
{
  internal class DomainSpecificRemotingData
  {
    private const int ACTIVATION_INITIALIZING = 1;
    private const int ACTIVATION_INITIALIZED = 2;
    private const int ACTIVATOR_LISTENING = 4;
    [SecurityCritical]
    private LocalActivator _LocalActivator;
    private ActivationListener _ActivationListener;
    private IContextProperty[] _appDomainProperties;
    private int _flags;
    private object _ConfigLock;
    private ChannelServicesData _ChannelServicesData;
    private LeaseManager _LeaseManager;
    private ReaderWriterLock _IDTableLock;

    internal DomainSpecificRemotingData()
    {
      this._flags = 0;
      this._ConfigLock = new object();
      this._ChannelServicesData = new ChannelServicesData();
      this._IDTableLock = new ReaderWriterLock();
      this._appDomainProperties = new IContextProperty[1];
      this._appDomainProperties[0] = (IContextProperty) new LeaseLifeTimeServiceProperty();
    }

    internal LeaseManager LeaseManager
    {
      get
      {
        return this._LeaseManager;
      }
      set
      {
        this._LeaseManager = value;
      }
    }

    internal object ConfigLock
    {
      get
      {
        return this._ConfigLock;
      }
    }

    internal ReaderWriterLock IDTableLock
    {
      get
      {
        return this._IDTableLock;
      }
    }

    internal LocalActivator LocalActivator
    {
      [SecurityCritical] get
      {
        return this._LocalActivator;
      }
      [SecurityCritical] set
      {
        this._LocalActivator = value;
      }
    }

    internal ActivationListener ActivationListener
    {
      get
      {
        return this._ActivationListener;
      }
      set
      {
        this._ActivationListener = value;
      }
    }

    internal bool InitializingActivation
    {
      get
      {
        return (this._flags & 1) == 1;
      }
      set
      {
        if (value)
          this._flags |= 1;
        else
          this._flags &= -2;
      }
    }

    internal bool ActivationInitialized
    {
      get
      {
        return (this._flags & 2) == 2;
      }
      set
      {
        if (value)
          this._flags |= 2;
        else
          this._flags &= -3;
      }
    }

    internal bool ActivatorListening
    {
      get
      {
        return (this._flags & 4) == 4;
      }
      set
      {
        if (value)
          this._flags |= 4;
        else
          this._flags &= -5;
      }
    }

    internal IContextProperty[] AppDomainContextProperties
    {
      get
      {
        return this._appDomainProperties;
      }
    }

    internal ChannelServicesData ChannelServicesData
    {
      get
      {
        return this._ChannelServicesData;
      }
    }
  }
}
