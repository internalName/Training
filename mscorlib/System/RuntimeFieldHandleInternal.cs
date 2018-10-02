// Decompiled with JetBrains decompiler
// Type: System.RuntimeFieldHandleInternal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System
{
  internal struct RuntimeFieldHandleInternal
  {
    internal IntPtr m_handle;

    internal static RuntimeFieldHandleInternal EmptyHandle
    {
      get
      {
        return new RuntimeFieldHandleInternal();
      }
    }

    internal bool IsNullHandle()
    {
      return this.m_handle.IsNull();
    }

    internal IntPtr Value
    {
      [SecurityCritical] get
      {
        return this.m_handle;
      }
    }

    [SecurityCritical]
    internal RuntimeFieldHandleInternal(IntPtr value)
    {
      this.m_handle = value;
    }
  }
}
