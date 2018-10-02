// Decompiled with JetBrains decompiler
// Type: System.Threading.OverlappedData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  internal sealed class OverlappedData
  {
    internal IAsyncResult m_asyncResult;
    [SecurityCritical]
    internal IOCompletionCallback m_iocb;
    internal _IOCompletionCallback m_iocbHelper;
    internal Overlapped m_overlapped;
    private object m_userObject;
    private IntPtr m_pinSelf;
    private IntPtr m_userObjectInternal;
    private int m_AppDomainId;
    private byte m_isArray;
    private byte m_toBeCleaned;
    internal NativeOverlapped m_nativeOverlapped;

    [SecurityCritical]
    internal void ReInitialize()
    {
      this.m_asyncResult = (IAsyncResult) null;
      this.m_iocb = (IOCompletionCallback) null;
      this.m_iocbHelper = (_IOCompletionCallback) null;
      this.m_overlapped = (Overlapped) null;
      this.m_userObject = (object) null;
      this.m_pinSelf = (IntPtr) 0;
      this.m_userObjectInternal = (IntPtr) 0;
      this.m_AppDomainId = 0;
      this.m_nativeOverlapped.EventHandle = (IntPtr) 0;
      this.m_isArray = (byte) 0;
      this.m_nativeOverlapped.InternalLow = (IntPtr) 0;
      this.m_nativeOverlapped.InternalHigh = (IntPtr) 0;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
    {
      if (!this.m_pinSelf.IsNull())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_Overlapped_Pack"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (iocb != null)
      {
        this.m_iocbHelper = new _IOCompletionCallback(iocb, ref stackMark);
        this.m_iocb = iocb;
      }
      else
      {
        this.m_iocbHelper = (_IOCompletionCallback) null;
        this.m_iocb = (IOCompletionCallback) null;
      }
      this.m_userObject = userData;
      if (this.m_userObject != null)
        this.m_isArray = !(this.m_userObject.GetType() == typeof (object[])) ? (byte) 0 : (byte) 1;
      return this.AllocateNativeOverlapped();
    }

    [SecurityCritical]
    internal unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
    {
      if (!this.m_pinSelf.IsNull())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_Overlapped_Pack"));
      this.m_userObject = userData;
      if (this.m_userObject != null)
        this.m_isArray = !(this.m_userObject.GetType() == typeof (object[])) ? (byte) 0 : (byte) 1;
      this.m_iocb = iocb;
      this.m_iocbHelper = (_IOCompletionCallback) null;
      return this.AllocateNativeOverlapped();
    }

    [ComVisible(false)]
    internal IntPtr UserHandle
    {
      get
      {
        return this.m_nativeOverlapped.EventHandle;
      }
      set
      {
        this.m_nativeOverlapped.EventHandle = value;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern unsafe NativeOverlapped* AllocateNativeOverlapped();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe void FreeNativeOverlapped(NativeOverlapped* nativeOverlappedPtr);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe OverlappedData GetOverlappedFromNative(NativeOverlapped* nativeOverlappedPtr);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CheckVMForIOPacket(out NativeOverlapped* pOVERLAP, out uint errorCode, out uint numBytes);
  }
}
