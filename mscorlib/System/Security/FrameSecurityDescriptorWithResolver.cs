// Decompiled with JetBrains decompiler
// Type: System.Security.FrameSecurityDescriptorWithResolver
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection.Emit;

namespace System.Security
{
  internal class FrameSecurityDescriptorWithResolver : FrameSecurityDescriptor
  {
    private DynamicResolver m_resolver;

    public DynamicResolver Resolver
    {
      get
      {
        return this.m_resolver;
      }
    }
  }
}
