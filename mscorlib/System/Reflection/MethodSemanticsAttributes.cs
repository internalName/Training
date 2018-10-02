// Decompiled with JetBrains decompiler
// Type: System.Reflection.MethodSemanticsAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  [Flags]
  [Serializable]
  internal enum MethodSemanticsAttributes
  {
    Setter = 1,
    Getter = 2,
    Other = 4,
    AddOn = 8,
    RemoveOn = 16, // 0x00000010
    Fire = 32, // 0x00000020
  }
}
