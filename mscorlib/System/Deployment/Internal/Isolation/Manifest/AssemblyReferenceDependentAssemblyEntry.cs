// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.AssemblyReferenceDependentAssemblyEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [StructLayout(LayoutKind.Sequential)]
  internal class AssemblyReferenceDependentAssemblyEntry : IDisposable
  {
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Group;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Codebase;
    public ulong Size;
    [MarshalAs(UnmanagedType.SysInt)]
    public IntPtr HashValue;
    public uint HashValueSize;
    public uint HashAlgorithm;
    public uint Flags;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string ResourceFallbackCulture;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Description;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string SupportUrl;
    public ISection HashElements;

    ~AssemblyReferenceDependentAssemblyEntry()
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
      if (this.HashValue != IntPtr.Zero)
      {
        Marshal.FreeCoTaskMem(this.HashValue);
        this.HashValue = IntPtr.Zero;
      }
      if (!fDisposing)
        return;
      GC.SuppressFinalize((object) this);
    }
  }
}
