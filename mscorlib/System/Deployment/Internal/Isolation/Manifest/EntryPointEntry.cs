﻿// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.EntryPointEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [StructLayout(LayoutKind.Sequential)]
  internal class EntryPointEntry
  {
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Name;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string CommandLine_File;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string CommandLine_Parameters;
    public IReferenceIdentity Identity;
    public uint Flags;
  }
}
