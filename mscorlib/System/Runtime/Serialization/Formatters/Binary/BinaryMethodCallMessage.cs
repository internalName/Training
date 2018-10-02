// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryMethodCallMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  [Serializable]
  internal sealed class BinaryMethodCallMessage
  {
    private object[] _inargs;
    private string _methodName;
    private string _typeName;
    private object _methodSignature;
    private Type[] _instArgs;
    private object[] _args;
    [SecurityCritical]
    private LogicalCallContext _logicalCallContext;
    private object[] _properties;

    [SecurityCritical]
    internal BinaryMethodCallMessage(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, LogicalCallContext callContext, object[] properties)
    {
      this._methodName = methodName;
      this._typeName = typeName;
      if (args == null)
        args = new object[0];
      this._inargs = args;
      this._args = args;
      this._instArgs = instArgs;
      this._methodSignature = methodSignature;
      this._logicalCallContext = callContext != null ? callContext : new LogicalCallContext();
      this._properties = properties;
    }

    public string MethodName
    {
      get
      {
        return this._methodName;
      }
    }

    public string TypeName
    {
      get
      {
        return this._typeName;
      }
    }

    public Type[] InstantiationArgs
    {
      get
      {
        return this._instArgs;
      }
    }

    public object MethodSignature
    {
      get
      {
        return this._methodSignature;
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
