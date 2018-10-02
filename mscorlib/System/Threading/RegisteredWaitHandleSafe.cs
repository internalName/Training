// Decompiled with JetBrains decompiler
// Type: System.Threading.RegisteredWaitHandleSafe
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  internal sealed class RegisteredWaitHandleSafe : CriticalFinalizerObject
  {
    private IntPtr registeredWaitHandle;
    private WaitHandle m_internalWaitObject;
    private bool bReleaseNeeded;
    private volatile int m_lock;

    private static IntPtr InvalidHandle
    {
      [SecuritySafeCritical] get
      {
        return Win32Native.INVALID_HANDLE_VALUE;
      }
    }

    internal RegisteredWaitHandleSafe()
    {
      this.registeredWaitHandle = RegisteredWaitHandleSafe.InvalidHandle;
    }

    internal IntPtr GetHandle()
    {
      return this.registeredWaitHandle;
    }

    internal void SetHandle(IntPtr handle)
    {
      this.registeredWaitHandle = handle;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal void SetWaitObject(WaitHandle waitObject)
    {
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        this.m_internalWaitObject = waitObject;
        if (waitObject != null)
          this.m_internalWaitObject.SafeWaitHandle.DangerousAddRef(ref this.bReleaseNeeded);
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal bool Unregister(WaitHandle waitObject)
    {
      bool flag1 = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        bool flag2 = false;
        do
        {
          if (Interlocked.CompareExchange(ref this.m_lock, 1, 0) == 0)
          {
            flag2 = true;
            try
            {
              if (this.ValidHandle())
              {
                flag1 = RegisteredWaitHandleSafe.UnregisterWaitNative(this.GetHandle(), waitObject == null ? (SafeHandle) null : (SafeHandle) waitObject.SafeWaitHandle);
                if (flag1)
                {
                  if (this.bReleaseNeeded)
                  {
                    this.m_internalWaitObject.SafeWaitHandle.DangerousRelease();
                    this.bReleaseNeeded = false;
                  }
                  this.SetHandle(RegisteredWaitHandleSafe.InvalidHandle);
                  this.m_internalWaitObject = (WaitHandle) null;
                  GC.SuppressFinalize((object) this);
                }
              }
            }
            finally
            {
              this.m_lock = 0;
            }
          }
          Thread.SpinWait(1);
        }
        while (!flag2);
      }
      return flag1;
    }

    private bool ValidHandle()
    {
      if (this.registeredWaitHandle != RegisteredWaitHandleSafe.InvalidHandle)
        return this.registeredWaitHandle != IntPtr.Zero;
      return false;
    }

    [SecuritySafeCritical]
    ~RegisteredWaitHandleSafe()
    {
      if (Interlocked.CompareExchange(ref this.m_lock, 1, 0) != 0)
        return;
      try
      {
        if (!this.ValidHandle())
          return;
        RegisteredWaitHandleSafe.WaitHandleCleanupNative(this.registeredWaitHandle);
        if (this.bReleaseNeeded)
        {
          this.m_internalWaitObject.SafeWaitHandle.DangerousRelease();
          this.bReleaseNeeded = false;
        }
        this.SetHandle(RegisteredWaitHandleSafe.InvalidHandle);
        this.m_internalWaitObject = (WaitHandle) null;
      }
      finally
      {
        this.m_lock = 0;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void WaitHandleCleanupNative(IntPtr handle);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool UnregisterWaitNative(IntPtr handle, SafeHandle waitObject);
  }
}
