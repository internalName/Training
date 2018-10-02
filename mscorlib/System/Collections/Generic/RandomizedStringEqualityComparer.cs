// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.RandomizedStringEqualityComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Collections.Generic
{
  internal sealed class RandomizedStringEqualityComparer : IEqualityComparer<string>, IEqualityComparer, IWellKnownStringEqualityComparer
  {
    private long _entropy;

    public RandomizedStringEqualityComparer()
    {
      this._entropy = HashHelpers.GetEntropy();
    }

    public bool Equals(object x, object y)
    {
      if (x == y)
        return true;
      if (x == null || y == null)
        return false;
      if (x is string && y is string)
        return this.Equals((string) x, (string) y);
      ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
      return false;
    }

    public bool Equals(string x, string y)
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
    public int GetHashCode(string obj)
    {
      if (obj == null)
        return 0;
      return string.InternalMarvin32HashString(obj, obj.Length, this._entropy);
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
      RandomizedStringEqualityComparer equalityComparer = obj as RandomizedStringEqualityComparer;
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
      return (IEqualityComparer) new RandomizedStringEqualityComparer();
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
    {
      return (IEqualityComparer) EqualityComparer<string>.Default;
    }
  }
}
