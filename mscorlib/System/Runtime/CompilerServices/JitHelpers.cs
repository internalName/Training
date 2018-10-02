// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.JitHelpers
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Threading;

namespace System.Runtime.CompilerServices
{
  [FriendAccessAllowed]
  internal static class JitHelpers
  {
    internal const string QCall = "QCall";

    [SecurityCritical]
    internal static StringHandleOnStack GetStringHandleOnStack(ref string s)
    {
      return new StringHandleOnStack(JitHelpers.UnsafeCastToStackPointer<string>(ref s));
    }

    [SecurityCritical]
    internal static ObjectHandleOnStack GetObjectHandleOnStack<T>(ref T o) where T : class
    {
      return new ObjectHandleOnStack(JitHelpers.UnsafeCastToStackPointer<T>(ref o));
    }

    [SecurityCritical]
    internal static StackCrawlMarkHandle GetStackCrawlMarkHandle(ref StackCrawlMark stackMark)
    {
      return new StackCrawlMarkHandle(JitHelpers.UnsafeCastToStackPointer<StackCrawlMark>(ref stackMark));
    }

    [SecurityCritical]
    [FriendAccessAllowed]
    internal static T UnsafeCast<T>(object o) where T : class
    {
      throw new InvalidOperationException();
    }

    internal static int UnsafeEnumCast<T>(T val) where T : struct
    {
      throw new InvalidOperationException();
    }

    internal static long UnsafeEnumCastLong<T>(T val) where T : struct
    {
      throw new InvalidOperationException();
    }

    [SecurityCritical]
    internal static IntPtr UnsafeCastToStackPointer<T>(ref T val)
    {
      throw new InvalidOperationException();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void UnsafeSetArrayElement(object[] target, int index, object element);

    [SecurityCritical]
    internal static PinningHelper GetPinningHelper(object o)
    {
      return JitHelpers.UnsafeCast<PinningHelper>(o);
    }
  }
}
