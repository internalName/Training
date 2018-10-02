// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreOperationSetCanonicalizationContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  internal struct StoreOperationSetCanonicalizationContext
  {
    [MarshalAs(UnmanagedType.U4)]
    public uint Size;
    [MarshalAs(UnmanagedType.U4)]
    public StoreOperationSetCanonicalizationContext.OpFlags Flags;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string BaseAddressFilePath;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string ExportsFilePath;

    [SecurityCritical]
    public StoreOperationSetCanonicalizationContext(string Bases, string Exports)
    {
      this.Size = (uint) Marshal.SizeOf(typeof (StoreOperationSetCanonicalizationContext));
      this.Flags = StoreOperationSetCanonicalizationContext.OpFlags.Nothing;
      this.BaseAddressFilePath = Bases;
      this.ExportsFilePath = Exports;
    }

    public void Destroy()
    {
    }

    [System.Flags]
    public enum OpFlags
    {
      Nothing = 0,
    }
  }
}
