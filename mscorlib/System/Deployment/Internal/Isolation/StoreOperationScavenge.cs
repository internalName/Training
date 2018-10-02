// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreOperationScavenge
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
  internal struct StoreOperationScavenge
  {
    [MarshalAs(UnmanagedType.U4)]
    public uint Size;
    [MarshalAs(UnmanagedType.U4)]
    public StoreOperationScavenge.OpFlags Flags;
    [MarshalAs(UnmanagedType.U8)]
    public ulong SizeReclaimationLimit;
    [MarshalAs(UnmanagedType.U8)]
    public ulong RuntimeLimit;
    [MarshalAs(UnmanagedType.U4)]
    public uint ComponentCountLimit;

    public StoreOperationScavenge(bool Light, ulong SizeLimit, ulong RunLimit, uint ComponentLimit)
    {
      this.Size = (uint) Marshal.SizeOf(typeof (StoreOperationScavenge));
      this.Flags = StoreOperationScavenge.OpFlags.Nothing;
      if (Light)
        this.Flags |= StoreOperationScavenge.OpFlags.Light;
      this.SizeReclaimationLimit = SizeLimit;
      if (SizeLimit != 0UL)
        this.Flags |= StoreOperationScavenge.OpFlags.LimitSize;
      this.RuntimeLimit = RunLimit;
      if (RunLimit != 0UL)
        this.Flags |= StoreOperationScavenge.OpFlags.LimitTime;
      this.ComponentCountLimit = ComponentLimit;
      if (ComponentLimit == 0U)
        return;
      this.Flags |= StoreOperationScavenge.OpFlags.LimitCount;
    }

    public StoreOperationScavenge(bool Light)
    {
      this = new StoreOperationScavenge(Light, 0UL, 0UL, 0U);
    }

    public void Destroy()
    {
    }

    [System.Flags]
    public enum OpFlags
    {
      Nothing = 0,
      Light = 1,
      LimitSize = 2,
      LimitTime = 4,
      LimitCount = 8,
    }
  }
}
