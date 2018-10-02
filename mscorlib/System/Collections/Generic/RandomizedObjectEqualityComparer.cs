// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.RandomizedObjectEqualityComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Collections.Generic
{
  internal sealed class RandomizedObjectEqualityComparer : IEqualityComparer, IWellKnownStringEqualityComparer
  {
    private long _entropy;

    public RandomizedObjectEqualityComparer()
    {
      this._entropy = HashHelpers.GetEntropy();
    }

    public bool Equals(object x, object y)
    {
      if (x != null)
      {
        if (y != null)
          return x.Equals(y);
        return false;
      }
      return y == null;
    }

    [SecuritySafeCritical]
    public int GetHashCode(object obj)
    {
      if (obj == null)
        return 0;
      string s = obj as string;
      if (s != null)
        return string.InternalMarvin32HashString(s, s.Length, this._entropy);
      return obj.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      RandomizedObjectEqualityComparer equalityComparer = obj as RandomizedObjectEqualityComparer;
      if (equalityComparer != null)
        return this._entropy == equalityComparer._entropy;
      return false;
    }

    public override int GetHashCode()
    {
      return this.GetType().Name.GetHashCode() ^ (int) (this._entropy & (long) int.MaxValue);
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
    {
      return (IEqualityComparer) new RandomizedObjectEqualityComparer();
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
    {
      return (IEqualityComparer) null;
    }
  }
}
