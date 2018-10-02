// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreOperationUninstallDeployment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  internal struct StoreOperationUninstallDeployment
  {
    [MarshalAs(UnmanagedType.U4)]
    public uint Size;
    [MarshalAs(UnmanagedType.U4)]
    public StoreOperationUninstallDeployment.OpFlags Flags;
    [MarshalAs(UnmanagedType.Interface)]
    public IDefinitionAppId Application;
    public IntPtr Reference;

    [SecuritySafeCritical]
    public StoreOperationUninstallDeployment(IDefinitionAppId appid, StoreApplicationReference AppRef)
    {
      this.Size = (uint) Marshal.SizeOf(typeof (StoreOperationUninstallDeployment));
      this.Flags = StoreOperationUninstallDeployment.OpFlags.Nothing;
      this.Application = appid;
      this.Reference = AppRef.ToIntPtr();
    }

    [SecurityCritical]
    public void Destroy()
    {
      StoreApplicationReference.Destroy(this.Reference);
    }

    [System.Flags]
    public enum OpFlags
    {
      Nothing = 0,
    }

    public enum Disposition
    {
      Failed,
      DidNotExist,
      Uninstalled,
    }
  }
}
