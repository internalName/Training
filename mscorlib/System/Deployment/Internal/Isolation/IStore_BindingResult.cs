// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IStore_BindingResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
  internal struct IStore_BindingResult
  {
    [MarshalAs(UnmanagedType.U4)]
    public uint Flags;
    [MarshalAs(UnmanagedType.U4)]
    public uint Disposition;
    public IStore_BindingResult_BoundVersion Component;
    public Guid CacheCoherencyGuid;
    [MarshalAs(UnmanagedType.SysInt)]
    public IntPtr Reserved;
  }
}
