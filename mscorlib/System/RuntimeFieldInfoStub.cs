// Decompiled with JetBrains decompiler
// Type: System.RuntimeFieldInfoStub
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  [StructLayout(LayoutKind.Sequential)]
  internal class RuntimeFieldInfoStub : IRuntimeFieldInfo
  {
    private object m_keepalive;
    private object m_c;
    private object m_d;
    private int m_b;
    private object m_e;
    private object m_f;
    private RuntimeFieldHandleInternal m_fieldHandle;

    [SecuritySafeCritical]
    public RuntimeFieldInfoStub(IntPtr methodHandleValue, object keepalive)
    {
      this.m_keepalive = keepalive;
      this.m_fieldHandle = new RuntimeFieldHandleInternal(methodHandleValue);
    }

    RuntimeFieldHandleInternal IRuntimeFieldInfo.Value
    {
      get
      {
        return this.m_fieldHandle;
      }
    }
  }
}
