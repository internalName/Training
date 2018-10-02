// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.StackBasedReturnMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class StackBasedReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage, IInternalMessage
  {
    private Message _m;
    private Hashtable _h;
    private MRMDictionary _d;
    private ArgMapper _argMapper;

    internal StackBasedReturnMessage()
    {
    }

    internal void InitFields(Message m)
    {
      this._m = m;
      if (this._h != null)
        this._h.Clear();
      if (this._d == null)
        return;
      this._d.Clear();
    }

    public string Uri
    {
      [SecurityCritical] get
      {
        return this._m.Uri;
      }
    }

    public string MethodName
    {
      [SecurityCritical] get
      {
        return this._m.MethodName;
      }
    }

    public string TypeName
    {
      [SecurityCritical] get
      {
        return this._m.TypeName;
      }
    }

    public object MethodSignature
    {
      [SecurityCritical] get
      {
        return this._m.MethodSignature;
      }
    }

    public MethodBase MethodBase
    {
      [SecurityCritical] get
      {
        return this._m.MethodBase;
      }
    }

    public bool HasVarArgs
    {
      [SecurityCritical] get
      {
        return this._m.HasVarArgs;
      }
    }

    public int ArgCount
    {
      [SecurityCritical] get
      {
        return this._m.ArgCount;
      }
    }

    [SecurityCritical]
    public object GetArg(int argNum)
    {
      return this._m.GetArg(argNum);
    }

    [SecurityCritical]
    public string GetArgName(int index)
    {
      return this._m.GetArgName(index);
    }

    public object[] Args
    {
      [SecurityCritical] get
      {
        return this._m.Args;
      }
    }

    public LogicalCallContext LogicalCallContext
    {
      [SecurityCritical] get
      {
        return this._m.GetLogicalCallContext();
      }
    }

    [SecurityCritical]
    internal LogicalCallContext GetLogicalCallContext()
    {
      return this._m.GetLogicalCallContext();
    }

    [SecurityCritical]
    internal LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
    {
      return this._m.SetLogicalCallContext(callCtx);
    }

    public int OutArgCount
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, true);
        return this._argMapper.ArgCount;
      }
    }

    [SecurityCritical]
    public object GetOutArg(int argNum)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, true);
      return this._argMapper.GetArg(argNum);
    }

    [SecurityCritical]
    public string GetOutArgName(int index)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, true);
      return this._argMapper.GetArgName(index);
    }

    public object[] OutArgs
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, true);
        return this._argMapper.Args;
      }
    }

    public Exception Exception
    {
      [SecurityCritical] get
      {
        return (Exception) null;
      }
    }

    public object ReturnValue
    {
      [SecurityCritical] get
      {
        return this._m.GetReturnValue();
      }
    }

    public IDictionary Properties
    {
      [SecurityCritical] get
      {
        lock (this)
        {
          if (this._h == null)
            this._h = new Hashtable();
          if (this._d == null)
            this._d = new MRMDictionary((IMethodReturnMessage) this, (IDictionary) this._h);
          return (IDictionary) this._d;
        }
      }
    }

    ServerIdentity IInternalMessage.ServerIdentityObject
    {
      [SecurityCritical] get
      {
        return (ServerIdentity) null;
      }
      [SecurityCritical] set
      {
      }
    }

    Identity IInternalMessage.IdentityObject
    {
      [SecurityCritical] get
      {
        return (Identity) null;
      }
      [SecurityCritical] set
      {
      }
    }

    [SecurityCritical]
    void IInternalMessage.SetURI(string val)
    {
      this._m.Uri = val;
    }

    [SecurityCritical]
    void IInternalMessage.SetCallContext(LogicalCallContext newCallContext)
    {
      this._m.SetLogicalCallContext(newCallContext);
    }

    [SecurityCritical]
    bool IInternalMessage.HasProperties()
    {
      return this._h != null;
    }
  }
}
