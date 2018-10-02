// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreOperationInstallDeployment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  internal struct StoreOperationInstallDeployment
  {
    [MarshalAs(UnmanagedType.U4)]
    public uint Size;
    [MarshalAs(UnmanagedType.U4)]
    public StoreOperationInstallDeployment.OpFlags Flags;
    [MarshalAs(UnmanagedType.Interface)]
    public IDefinitionAppId Application;
    public IntPtr Reference;

    public StoreOperationInstallDeployment(IDefinitionAppId App, StoreApplicationReference reference)
    {
      this = new StoreOperationInstallDeployment(App, true, reference);
    }

    [SecuritySafeCritical]
    public StoreOperationInstallDeployment(IDefinitionAppId App, bool UninstallOthers, StoreApplicationReference reference)
    {
      this.Size = (uint) Marshal.SizeOf(typeof (StoreOperationInstallDeployment));
      this.Flags = StoreOperationInstallDeployment.OpFlags.Nothing;
      this.Application = App;
      if (UninstallOthers)
        this.Flags |= StoreOperationInstallDeployment.OpFlags.UninstallOthers;
      this.Reference = reference.ToIntPtr();
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
      UninstallOthers = 1,
    }

    public enum Disposition
    {
      Failed,
      AlreadyInstalled,
      Installed,
    }
  }
}
