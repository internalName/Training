// Decompiled with JetBrains decompiler
// Type: System.Threading.SafeCompressedStackHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  [SecurityCritical]
  internal class SafeCompressedStackHandle : SafeHandle
  {
    public SafeCompressedStackHandle()
      : base(IntPtr.Zero, true)
    {
    }

    public override bool IsInvalid
    {
      [SecurityCritical] get
      {
        return this.handle == IntPtr.Zero;
      }
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      CompressedStack.DestroyDelayedCompressedStack(this.handle);
      this.handle = IntPtr.Zero;
      return true;
    }
  }
}
