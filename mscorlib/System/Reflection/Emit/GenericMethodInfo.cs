// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.GenericMethodInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection.Emit
{
  internal sealed class GenericMethodInfo
  {
    internal RuntimeMethodHandle m_methodHandle;
    internal RuntimeTypeHandle m_context;

    internal GenericMethodInfo(RuntimeMethodHandle methodHandle, RuntimeTypeHandle context)
    {
      this.m_methodHandle = methodHandle;
      this.m_context = context;
    }
  }
}
