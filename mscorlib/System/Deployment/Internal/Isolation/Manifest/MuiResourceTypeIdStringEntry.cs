// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.MuiResourceTypeIdStringEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [StructLayout(LayoutKind.Sequential)]
  internal class MuiResourceTypeIdStringEntry : IDisposable
  {
    [MarshalAs(UnmanagedType.SysInt)]
    public IntPtr StringIds;
    public uint StringIdsSize;
    [MarshalAs(UnmanagedType.SysInt)]
    public IntPtr IntegerIds;
    public uint IntegerIdsSize;

    ~MuiResourceTypeIdStringEntry()
    {
      this.Dispose(false);
    }

    void IDisposable.Dispose()
    {
      this.Dispose(true);
    }

    [SecuritySafeCritical]
    public void Dispose(bool fDisposing)
    {
      if (this.StringIds != IntPtr.Zero)
      {
        Marshal.FreeCoTaskMem(this.StringIds);
        this.StringIds = IntPtr.Zero;
      }
      if (this.IntegerIds != IntPtr.Zero)
      {
        Marshal.FreeCoTaskMem(this.IntegerIds);
        this.IntegerIds = IntPtr.Zero;
      }
      if (!fDisposing)
        return;
      GC.SuppressFinalize((object) this);
    }
  }
}
