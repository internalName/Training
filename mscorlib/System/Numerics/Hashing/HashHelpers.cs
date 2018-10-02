// Decompiled with JetBrains decompiler
// Type: System.Numerics.Hashing.HashHelpers
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Numerics.Hashing
{
  internal static class HashHelpers
  {
    public static int Combine(int h1, int h2)
    {
      return (int) ((uint) (h1 << 5) | (uint) h1 >> 27) + h1 ^ h2;
    }
  }
}
