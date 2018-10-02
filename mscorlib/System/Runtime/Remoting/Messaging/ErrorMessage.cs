// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ErrorMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class ErrorMessage : IMethodCallMessage, IMethodMessage, IMessage
  {
    private string m_URI = "Exception";
    private string m_MethodName = "Unknown";
    private string m_TypeName = "Unknown";
    private string m_ArgName = "Unknown";
    private object m_MethodSignature;
    private int m_ArgCount;

    public IDictionary Properties
    {
      [SecurityCritical] get
      {
        return (IDictionary) null;
      }
    }

    public string Uri
    {
      [SecurityCritical] get
      {
        return this.m_URI;
      }
    }

    public string MethodName
    {
      [SecurityCritical] get
      {
        return this.m_MethodName;
      }
    }

    public string TypeName
    {
      [SecurityCritical] get
      {
        return this.m_TypeName;
      }
    }

    public object MethodSignature
    {
      [SecurityCritical] get
      {
        return this.m_MethodSignature;
      }
    }

    public MethodBase MethodBase
    {
      [SecurityCritical] get
      {
        return (MethodBase) null;
      }
    }

    public int ArgCount
    {
      [SecurityCritical] get
      {
        return this.m_ArgCount;
      }
    }

    [SecurityCritical]
    public string GetArgName(int index)
    {
      return this.m_ArgName;
    }

    [SecurityCritical]
    public object GetArg(int argNum)
    {
      return (object) null;
    }

    public object[] Args
    {
      [SecurityCritical] get
      {
        return (object[]) null;
      }
    }

    public bool HasVarArgs
    {
      [SecurityCritical] get
      {
        return false;
      }
    }

    public int InArgCount
    {
      [SecurityCritical] get
      {
        return this.m_ArgCount;
      }
    }

    [SecurityCritical]
    public string GetInArgName(int index)
    {
      return (string) null;
    }

    [SecurityCritical]
    public object GetInArg(int argNum)
    {
      return (object) null;
    }

    public object[] InArgs
    {
      [SecurityCritical] get
      {
        return (object[]) null;
      }
    }

    public LogicalCallContext LogicalCallContext
    {
      [SecurityCritical] get
      {
        return (LogicalCallContext) null;
      }
    }
  }
}
