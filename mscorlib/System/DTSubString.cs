// Decompiled with JetBrains decompiler
// Type: System.DTSubString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  internal struct DTSubString
  {
    internal string s;
    internal int index;
    internal int length;
    internal DTSubStringType type;
    internal int value;

    internal char this[int relativeIndex]
    {
      get
      {
        return this.s[this.index + relativeIndex];
      }
    }
  }
}
