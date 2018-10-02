// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IWinRTClassActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("86ddd2d7-ad80-44f6-a12e-63698b52825d")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IWinRTClassActivator
  {
    [SecurityCritical]
    [return: MarshalAs(UnmanagedType.IInspectable)]
    object ActivateInstance([MarshalAs(UnmanagedType.HString)] string activatableClassId);

    [SecurityCritical]
    IntPtr GetActivationFactory([MarshalAs(UnmanagedType.HString)] string activatableClassId, ref Guid iid);
  }
}
