// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.StackGuard
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Security;

namespace System.Threading.Tasks
{
  internal class StackGuard
  {
    private int m_inliningDepth;
    private const int MAX_UNCHECKED_INLINING_DEPTH = 20;
    private ulong m_lastKnownWatermark;
    private static int s_pageSize;
    private const long STACK_RESERVED_SPACE = 65536;

    [SecuritySafeCritical]
    internal bool TryBeginInliningScope()
    {
      if (this.m_inliningDepth >= 20 && !this.CheckForSufficientStack())
        return false;
      ++this.m_inliningDepth;
      return true;
    }

    internal void EndInliningScope()
    {
      --this.m_inliningDepth;
      if (this.m_inliningDepth >= 0)
        return;
      this.m_inliningDepth = 0;
    }

    [SecurityCritical]
    private unsafe bool CheckForSufficientStack()
    {
      int num1 = StackGuard.s_pageSize;
      if (num1 == 0)
      {
        Win32Native.SYSTEM_INFO lpSystemInfo = new Win32Native.SYSTEM_INFO();
        Win32Native.GetSystemInfo(ref lpSystemInfo);
        StackGuard.s_pageSize = num1 = lpSystemInfo.dwPageSize;
      }
      Win32Native.MEMORY_BASIC_INFORMATION buffer = new Win32Native.MEMORY_BASIC_INFORMATION();
      UIntPtr num2 = new UIntPtr((void*) (&buffer - num1));
      ulong uint64 = num2.ToUInt64();
      if (this.m_lastKnownWatermark != 0UL && uint64 > this.m_lastKnownWatermark)
        return true;
      IntPtr num3 = (IntPtr) Win32Native.VirtualQuery(num2.ToPointer(), ref buffer, (UIntPtr) ((ulong) sizeof (Win32Native.MEMORY_BASIC_INFORMATION)));
      if (uint64 - (UIntPtr) buffer.AllocationBase.ToUInt64() <= 65536UL)
        return false;
      this.m_lastKnownWatermark = uint64;
      return true;
    }
  }
}
