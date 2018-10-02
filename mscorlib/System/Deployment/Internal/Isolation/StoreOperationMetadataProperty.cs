// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreOperationMetadataProperty
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
  internal struct StoreOperationMetadataProperty
  {
    public Guid GuidPropertySet;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Name;
    [MarshalAs(UnmanagedType.SysUInt)]
    public IntPtr ValueSize;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Value;

    public StoreOperationMetadataProperty(Guid PropertySet, string Name)
    {
      this = new StoreOperationMetadataProperty(PropertySet, Name, (string) null);
    }

    public StoreOperationMetadataProperty(Guid PropertySet, string Name, string Value)
    {
      this.GuidPropertySet = PropertySet;
      this.Name = Name;
      this.Value = Value;
      this.ValueSize = Value != null ? new IntPtr((Value.Length + 1) * 2) : IntPtr.Zero;
    }
  }
}
