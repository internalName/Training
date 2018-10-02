// Decompiled with JetBrains decompiler
// Type: System.Resources.FastResourceComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace System.Resources
{
  internal sealed class FastResourceComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
  {
    internal static readonly FastResourceComparer Default = new FastResourceComparer();

    public int GetHashCode(object key)
    {
      return FastResourceComparer.HashFunction((string) key);
    }

    public int GetHashCode(string key)
    {
      return FastResourceComparer.HashFunction(key);
    }

    internal static int HashFunction(string key)
    {
      uint num = 5381;
      for (int index = 0; index < key.Length; ++index)
        num = (num << 5) + num ^ (uint) key[index];
      return (int) num;
    }

    public int Compare(object a, object b)
    {
      if (a == b)
        return 0;
      return string.CompareOrdinal((string) a, (string) b);
    }

    public int Compare(string a, string b)
    {
      return string.CompareOrdinal(a, b);
    }

    public bool Equals(string a, string b)
    {
      return string.Equals(a, b);
    }

    public bool Equals(object a, object b)
    {
      if (a == b)
        return true;
      return string.Equals((string) a, (string) b);
    }

    [SecurityCritical]
    public static unsafe int CompareOrdinal(string a, byte[] bytes, int bCharLength)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = a.Length;
      if (num3 > bCharLength)
        num3 = bCharLength;
      if (bCharLength == 0)
        return a.Length != 0 ? -1 : 0;
      fixed (byte* numPtr = bytes)
      {
        while (num1 < num3 && num2 == 0)
        {
          int num4 = (int) numPtr[0] | (int) numPtr[1] << 8;
          num2 = (int) a[num1++] - num4;
          numPtr += 2;
        }
      }
      if (num2 != 0)
        return num2;
      return a.Length - bCharLength;
    }

    [SecurityCritical]
    public static int CompareOrdinal(byte[] bytes, int aCharLength, string b)
    {
      return -FastResourceComparer.CompareOrdinal(b, bytes, aCharLength);
    }

    [SecurityCritical]
    internal static unsafe int CompareOrdinal(byte* a, int byteLen, string b)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = byteLen >> 1;
      if (num3 > b.Length)
        num3 = b.Length;
      while (num2 < num3 && num1 == 0)
        num1 = (int) (char) ((int) *a++ | (int) *a++ << 8) - (int) b[num2++];
      if (num1 != 0)
        return num1;
      return byteLen - b.Length * 2;
    }
  }
}
