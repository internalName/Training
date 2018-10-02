// Decompiled with JetBrains decompiler
// Type: System.Security.SafeBSTRHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Security
{
  [SecurityCritical]
  [SuppressUnmanagedCodeSecurity]
  internal sealed class SafeBSTRHandle : SafeBuffer
  {
    internal SafeBSTRHandle()
      : base(true)
    {
    }

    internal static SafeBSTRHandle Allocate(string src, uint len)
    {
      SafeBSTRHandle safeBstrHandle = SafeBSTRHandle.SysAllocStringLen(src, len);
      safeBstrHandle.Initialize((ulong) (len * 2U));
      return safeBstrHandle;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
    private static extern SafeBSTRHandle SysAllocStringLen(string src, uint len);

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      Win32Native.ZeroMemory(this.handle, (UIntPtr) (Win32Native.SysStringLen(this.handle) * 2U));
      Win32Native.SysFreeString(this.handle);
      return true;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal unsafe void ClearBuffer()
    {
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.AcquirePointer(ref pointer);
        Win32Native.ZeroMemory((IntPtr) ((void*) pointer), (UIntPtr) (Win32Native.SysStringLen((IntPtr) ((void*) pointer)) * 2U));
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          this.ReleasePointer();
      }
    }

    internal int Length
    {
      get
      {
        return (int) Win32Native.SysStringLen(this);
      }
    }

    internal static unsafe void Copy(SafeBSTRHandle source, SafeBSTRHandle target)
    {
      byte* pointer1 = (byte*) null;
      byte* pointer2 = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        source.AcquirePointer(ref pointer1);
        target.AcquirePointer(ref pointer2);
        Buffer.Memcpy(pointer2, pointer1, (int) Win32Native.SysStringLen((IntPtr) ((void*) pointer1)) * 2);
      }
      finally
      {
        if ((IntPtr) pointer1 != IntPtr.Zero)
          source.ReleasePointer();
        if ((IntPtr) pointer2 != IntPtr.Zero)
          target.ReleasePointer();
      }
    }
  }
}
