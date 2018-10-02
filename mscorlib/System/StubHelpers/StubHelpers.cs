// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.StubHelpers
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.StubHelpers
{
  [SecurityCritical]
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  [SuppressUnmanagedCodeSecurity]
  internal static class StubHelpers
  {
    [ThreadStatic]
    private static CopyCtorStubDesc s_copyCtorStubDesc;

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsQCall(IntPtr pMD);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InitDeclaringType(IntPtr pMD);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetNDirectTarget(IntPtr pMD);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetDelegateTarget(Delegate pThis, ref IntPtr pStubArg);

    internal static void SetCopyCtorCookieChain(IntPtr pStubArg, IntPtr pUnmngThis, int dwStubFlags, IntPtr pCookie)
    {
      System.StubHelpers.StubHelpers.s_copyCtorStubDesc.m_pCookie = pCookie;
      System.StubHelpers.StubHelpers.s_copyCtorStubDesc.m_pTarget = System.StubHelpers.StubHelpers.GetFinalStubTarget(pStubArg, pUnmngThis, dwStubFlags);
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetFinalStubTarget(IntPtr pStubArg, IntPtr pUnmngThis, int dwStubFlags);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void DemandPermission(IntPtr pNMD);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void SetLastError();

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ThrowInteropParamException(int resID, int paramIdx);

    [SecurityCritical]
    internal static IntPtr AddToCleanupList(ref CleanupWorkList pCleanupWorkList, SafeHandle handle)
    {
      if (pCleanupWorkList == null)
        pCleanupWorkList = new CleanupWorkList();
      CleanupWorkListElement elem = new CleanupWorkListElement(handle);
      pCleanupWorkList.Add(elem);
      return System.StubHelpers.StubHelpers.SafeHandleAddRef(handle, ref elem.m_owned);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static void DestroyCleanupList(ref CleanupWorkList pCleanupWorkList)
    {
      if (pCleanupWorkList == null)
        return;
      pCleanupWorkList.Destroy();
      pCleanupWorkList = (CleanupWorkList) null;
    }

    internal static Exception GetHRExceptionObject(int hr)
    {
      Exception hrExceptionObject = System.StubHelpers.StubHelpers.InternalGetHRExceptionObject(hr);
      hrExceptionObject.InternalPreserveStackTrace();
      return hrExceptionObject;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Exception InternalGetHRExceptionObject(int hr);

    internal static Exception GetCOMHRExceptionObject(int hr, IntPtr pCPCMD, object pThis)
    {
      Exception comhrExceptionObject = System.StubHelpers.StubHelpers.InternalGetCOMHRExceptionObject(hr, pCPCMD, pThis, false);
      comhrExceptionObject.InternalPreserveStackTrace();
      return comhrExceptionObject;
    }

    internal static Exception GetCOMHRExceptionObject_WinRT(int hr, IntPtr pCPCMD, object pThis)
    {
      Exception comhrExceptionObject = System.StubHelpers.StubHelpers.InternalGetCOMHRExceptionObject(hr, pCPCMD, pThis, true);
      comhrExceptionObject.InternalPreserveStackTrace();
      return comhrExceptionObject;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Exception InternalGetCOMHRExceptionObject(int hr, IntPtr pCPCMD, object pThis, bool fForWinRT);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr CreateCustomMarshalerHelper(IntPtr pMD, int paramToken, IntPtr hndManagedType);

    [SecurityCritical]
    internal static IntPtr SafeHandleAddRef(SafeHandle pHandle, ref bool success)
    {
      if (pHandle == null)
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_SafeHandle"));
      pHandle.DangerousAddRef(ref success);
      if (!success)
        return IntPtr.Zero;
      return pHandle.DangerousGetHandle();
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static void SafeHandleRelease(SafeHandle pHandle)
    {
      if (pHandle == null)
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_SafeHandle"));
      try
      {
        pHandle.DangerousRelease();
      }
      catch (Exception ex)
      {
        Mda.ReportErrorSafeHandleRelease(ex);
      }
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetCOMIPFromRCW(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget, out bool pfNeedsRelease);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetCOMIPFromRCW_WinRT(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetCOMIPFromRCW_WinRTSharedGeneric(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetCOMIPFromRCW_WinRTDelegate(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool ShouldCallWinRTInterface(object objSrc, IntPtr pCPCMD);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Delegate GetTargetForAmbiguousVariantCall(object objSrc, IntPtr pMT, out bool fUseString);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void StubRegisterRCW(object pThis);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void StubUnregisterRCW(object pThis);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetDelegateInvokeMethod(Delegate pThis);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object GetWinRTFactoryObject(IntPtr pCPCMD);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetWinRTFactoryReturnValue(object pThis, IntPtr pCtorEntry);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetOuterInspectable(object pThis, IntPtr pCtorMD);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Exception TriggerExceptionSwallowedMDA(Exception ex, IntPtr pManagedTarget);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CheckCollectedDelegateMDA(IntPtr pEntryThunk);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr ProfilerBeginTransitionCallback(IntPtr pSecretParam, IntPtr pThread, object pThis);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ProfilerEndTransitionCallback(IntPtr pMD, IntPtr pThread);

    internal static void CheckStringLength(int length)
    {
      System.StubHelpers.StubHelpers.CheckStringLength((uint) length);
    }

    internal static void CheckStringLength(uint length)
    {
      if (length > 2147483632U)
        throw new MarshalDirectiveException(Environment.GetResourceString("Marshaler_StringTooLong"));
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe int strlen(sbyte* ptr);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void DecimalCanonicalizeInternal(ref Decimal dec);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe void FmtClassUpdateNativeInternal(object obj, byte* pNative, ref CleanupWorkList pCleanupWorkList);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe void FmtClassUpdateCLRInternal(object obj, byte* pNative);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe void LayoutDestroyNativeInternal(byte* pNative, IntPtr pMT);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object AllocateInternal(IntPtr typeHandle);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void MarshalToUnmanagedVaListInternal(IntPtr va_list, uint vaListSize, IntPtr pArgIterator);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void MarshalToManagedVaListInternal(IntPtr va_list, IntPtr pArgIterator);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern uint CalcVaListSize(IntPtr va_list);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ValidateObject(object obj, IntPtr pMD, object pThis);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void LogPinnedArgument(IntPtr localDesc, IntPtr nativeArg);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ValidateByref(IntPtr byref, IntPtr pMD, object pThis);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetStubContext();

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void TriggerGCForMDA();
  }
}
