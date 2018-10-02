// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreOperationStageComponentFile
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
  internal struct StoreOperationStageComponentFile
  {
    [MarshalAs(UnmanagedType.U4)]
    public uint Size;
    [MarshalAs(UnmanagedType.U4)]
    public StoreOperationStageComponentFile.OpFlags Flags;
    [MarshalAs(UnmanagedType.Interface)]
    public IDefinitionAppId Application;
    [MarshalAs(UnmanagedType.Interface)]
    public IDefinitionIdentity Component;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string ComponentRelativePath;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string SourceFilePath;

    public StoreOperationStageComponentFile(IDefinitionAppId App, string CompRelPath, string SrcFile)
    {
      this = new StoreOperationStageComponentFile(App, (IDefinitionIdentity) null, CompRelPath, SrcFile);
    }

    public StoreOperationStageComponentFile(IDefinitionAppId App, IDefinitionIdentity Component, string CompRelPath, string SrcFile)
    {
      this.Size = (uint) Marshal.SizeOf(typeof (StoreOperationStageComponentFile));
      this.Flags = StoreOperationStageComponentFile.OpFlags.Nothing;
      this.Application = App;
      this.Component = Component;
      this.ComponentRelativePath = CompRelPath;
      this.SourceFilePath = SrcFile;
    }

    public void Destroy()
    {
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
