// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.MuiResourceMapEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [StructLayout(LayoutKind.Sequential)]
  internal class MuiResourceMapEntry : IDisposable
  {
    [MarshalAs(UnmanagedType.SysInt)]
    public IntPtr ResourceTypeIdInt;
    public uint ResourceTypeIdIntSize;
    [MarshalAs(UnmanagedType.SysInt)]
    public IntPtr ResourceTypeIdString;
    public uint ResourceTypeIdStringSize;

    ~MuiResourceMapEntry()
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
      if (this.ResourceTypeIdInt != IntPtr.Zero)
      {
        Marshal.FreeCoTaskMem(this.ResourceTypeIdInt);
        this.ResourceTypeIdInt = IntPtr.Zero;
      }
      if (this.ResourceTypeIdString != IntPtr.Zero)
      {
        Marshal.FreeCoTaskMem(this.ResourceTypeIdString);
        this.ResourceTypeIdString = IntPtr.Zero;
      }
      if (!fDisposing)
        return;
      GC.SuppressFinalize((object) this);
    }
  }
}
