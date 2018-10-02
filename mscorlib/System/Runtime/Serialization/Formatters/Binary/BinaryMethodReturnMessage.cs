// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryMethodReturnMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  [Serializable]
  internal class BinaryMethodReturnMessage
  {
    private object[] _outargs;
    private Exception _exception;
    private object _returnValue;
    private object[] _args;
    [SecurityCritical]
    private LogicalCallContext _logicalCallContext;
    private object[] _properties;

    [SecurityCritical]
    internal BinaryMethodReturnMessage(object returnValue, object[] args, Exception e, LogicalCallContext callContext, object[] properties)
    {
      this._returnValue = returnValue;
      if (args == null)
        args = new object[0];
      this._outargs = args;
      this._args = args;
      this._exception = e;
      this._logicalCallContext = callContext != null ? callContext : new LogicalCallContext();
      this._properties = properties;
    }

    public Exception Exception
    {
      get
      {
        return this._exception;
      }
    }

    public object ReturnValue
    {
      get
      {
        return this._returnValue;
      }
    }

    public object[] Args
    {
      get
      {
        return this._args;
      }
    }

    public LogicalCallContext LogicalCallContext
    {
      [SecurityCritical] get
      {
        return this._logicalCallContext;
      }
    }

    public bool HasProperties
    {
      get
      {
        return this._properties != null;
      }
    }

    internal void PopulateMessageProperties(IDictionary dict)
    {
      foreach (DictionaryEntry property in this._properties)
        dict[property.Key] = property.Value;
    }
  }
}
