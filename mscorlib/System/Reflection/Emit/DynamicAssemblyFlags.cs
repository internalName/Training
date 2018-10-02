// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.DynamicAssemblyFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection.Emit
{
  [Flags]
  internal enum DynamicAssemblyFlags
  {
    None = 0,
    AllCritical = 1,
    Aptca = 2,
    Critical = 4,
    Transparent = 8,
    TreatAsSafe = 16, // 0x00000010
  }
}
