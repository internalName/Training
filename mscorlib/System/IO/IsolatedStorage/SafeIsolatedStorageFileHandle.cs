// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.SafeIsolatedStorageFileHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.IO.IsolatedStorage
{
  [SecurityCritical]
  internal sealed class SafeIsolatedStorageFileHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    [SuppressUnmanagedCodeSecurity]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void Close(IntPtr file);

    private SafeIsolatedStorageFileHandle()
      : base(true)
    {
      this.SetHandle(IntPtr.Zero);
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      SafeIsolatedStorageFileHandle.Close(this.handle);
      return true;
    }
  }
}
