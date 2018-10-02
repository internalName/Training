// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreOperationStageComponent
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
  internal struct StoreOperationStageComponent
  {
    [MarshalAs(UnmanagedType.U4)]
    public uint Size;
    [MarshalAs(UnmanagedType.U4)]
    public StoreOperationStageComponent.OpFlags Flags;
    [MarshalAs(UnmanagedType.Interface)]
    public IDefinitionAppId Application;
    [MarshalAs(UnmanagedType.Interface)]
    public IDefinitionIdentity Component;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string ManifestPath;

    public void Destroy()
    {
    }

    public StoreOperationStageComponent(IDefinitionAppId app, string Manifest)
    {
      this = new StoreOperationStageComponent(app, (IDefinitionIdentity) null, Manifest);
    }

    public StoreOperationStageComponent(IDefinitionAppId app, IDefinitionIdentity comp, string Manifest)
    {
      this.Size = (uint) Marshal.SizeOf(typeof (StoreOperationStageComponent));
      this.Flags = StoreOperationStageComponent.OpFlags.Nothing;
      this.Application = app;
      this.Component = comp;
      this.ManifestPath = Manifest;
    }

    [System.Flags]
    public enum OpFlags
    {
      Nothing = 0,
    }

    public enum Disposition
    {
      Failed,
      Installed,
      Refreshed,
      AlreadyInstalled,
    }
  }
}
