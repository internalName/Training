// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.DynamicScope
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Security;

namespace System.Reflection.Emit
{
  internal class DynamicScope
  {
    internal List<object> m_tokens;

    internal DynamicScope()
    {
      this.m_tokens = new List<object>();
      this.m_tokens.Add((object) null);
    }

    internal object this[int token]
    {
      get
      {
        token &= 16777215;
        if (token < 0 || token > this.m_tokens.Count)
          return (object) null;
        return this.m_tokens[token];
      }
    }

    internal int GetTokenFor(VarArgMethod varArgMethod)
    {
      this.m_tokens.Add((object) varArgMethod);
      return this.m_tokens.Count - 1 | 167772160;
    }

    internal string GetString(int token)
    {
      return this[token] as string;
    }

    internal byte[] ResolveSignature(int token, int fromMethod)
    {
      if (fromMethod == 0)
        return (byte[]) this[token];
      return (this[token] as VarArgMethod)?.m_signature.GetSignature(true);
    }

    [SecuritySafeCritical]
    public int GetTokenFor(RuntimeMethodHandle method)
    {
      IRuntimeMethodInfo methodInfo = method.GetMethodInfo();
      RuntimeMethodHandleInternal method1 = methodInfo.Value;
      if (methodInfo != null && !RuntimeMethodHandle.IsDynamicMethod(method1))
      {
        RuntimeType declaringType = RuntimeMethodHandle.GetDeclaringType(method1);
        if (declaringType != (RuntimeType) null && RuntimeTypeHandle.HasInstantiation(declaringType))
        {
          MethodBase methodBase = RuntimeType.GetMethodBase(methodInfo);
          Type genericTypeDefinition = methodBase.DeclaringType.GetGenericTypeDefinition();
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_MethodDeclaringTypeGenericLcg"), (object) methodBase, (object) genericTypeDefinition));
        }
      }
      this.m_tokens.Add((object) method);
      return this.m_tokens.Count - 1 | 100663296;
    }

    public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle typeContext)
    {
      this.m_tokens.Add((object) new GenericMethodInfo(method, typeContext));
      return this.m_tokens.Count - 1 | 100663296;
    }

    public int GetTokenFor(DynamicMethod method)
    {
      this.m_tokens.Add((object) method);
      return this.m_tokens.Count - 1 | 100663296;
    }

    public int GetTokenFor(RuntimeFieldHandle field)
    {
      this.m_tokens.Add((object) field);
      return this.m_tokens.Count - 1 | 67108864;
    }

    public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle typeContext)
    {
      this.m_tokens.Add((object) new GenericFieldInfo(field, typeContext));
      return this.m_tokens.Count - 1 | 67108864;
    }

    public int GetTokenFor(RuntimeTypeHandle type)
    {
      this.m_tokens.Add((object) type);
      return this.m_tokens.Count - 1 | 33554432;
    }

    public int GetTokenFor(string literal)
    {
      this.m_tokens.Add((object) literal);
      return this.m_tokens.Count - 1 | 1879048192;
    }

    public int GetTokenFor(byte[] signature)
    {
      this.m_tokens.Add((object) signature);
      return this.m_tokens.Count - 1 | 285212672;
    }
  }
}
