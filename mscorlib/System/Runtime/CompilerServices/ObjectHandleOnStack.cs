// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ObjectHandleOnStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  internal struct ObjectHandleOnStack
  {
    private IntPtr m_ptr;

    internal ObjectHandleOnStack(IntPtr pObject)
    {
      this.m_ptr = pObject;
    }
  }
}
