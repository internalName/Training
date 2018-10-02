// Decompiled with JetBrains decompiler
// Type: System.Reflection.SecurityContextFrame
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Reflection
{
  internal struct SecurityContextFrame
  {
    private IntPtr m_GSCookie;
    private IntPtr __VFN_table;
    private IntPtr m_Next;
    private IntPtr m_Assembly;

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern void Push(RuntimeAssembly assembly);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern void Pop();
  }
}
