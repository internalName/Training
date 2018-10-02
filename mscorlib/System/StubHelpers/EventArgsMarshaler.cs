// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.EventArgsMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
  [FriendAccessAllowed]
  internal static class EventArgsMarshaler
  {
    [SecurityCritical]
    [FriendAccessAllowed]
    internal static IntPtr CreateNativeNCCEventArgsInstance(int action, object newItems, object oldItems, int newIndex, int oldIndex)
    {
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        if (newItems != null)
          num1 = Marshal.GetComInterfaceForObject(newItems, typeof (IBindableVector));
        if (oldItems != null)
          num2 = Marshal.GetComInterfaceForObject(oldItems, typeof (IBindableVector));
        return EventArgsMarshaler.CreateNativeNCCEventArgsInstanceHelper(action, num1, num2, newIndex, oldIndex);
      }
      finally
      {
        if (!num2.IsNull())
          Marshal.Release(num2);
        if (!num1.IsNull())
          Marshal.Release(num1);
      }
    }

    [SecurityCritical]
    [FriendAccessAllowed]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall")]
    internal static extern IntPtr CreateNativePCEventArgsInstance([MarshalAs(UnmanagedType.HString)] string name);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall")]
    internal static extern IntPtr CreateNativeNCCEventArgsInstanceHelper(int action, IntPtr newItem, IntPtr oldItem, int newIndex, int oldIndex);
  }
}
