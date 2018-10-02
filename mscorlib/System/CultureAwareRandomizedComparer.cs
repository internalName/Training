// Decompiled with JetBrains decompiler
// Type: System.CultureAwareRandomizedComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;

namespace System
{
  internal sealed class CultureAwareRandomizedComparer : StringComparer, IWellKnownStringEqualityComparer
  {
    private CompareInfo _compareInfo;
    private bool _ignoreCase;
    private long _entropy;

    internal CultureAwareRandomizedComparer(CompareInfo compareInfo, bool ignoreCase)
    {
      this._compareInfo = compareInfo;
      this._ignoreCase = ignoreCase;
      this._entropy = HashHelpers.GetEntropy();
    }

    public override int Compare(string x, string y)
    {
      if ((object) x == (object) y)
        return 0;
      if (x == null)
        return -1;
      if (y == null)
        return 1;
      return this._compareInfo.Compare(x, y, this._ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
    }

    public override bool Equals(string x, string y)
    {
      if ((object) x == (object) y)
        return true;
      if (x == null || y == null)
        return false;
      return this._compareInfo.Compare(x, y, this._ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None) == 0;
    }

    public override int GetHashCode(string obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      CompareOptions options = CompareOptions.None;
      if (this._ignoreCase)
        options |= CompareOptions.IgnoreCase;
      return this._compareInfo.GetHashCodeOfString(obj, options, true, this._entropy);
    }

    public override bool Equals(object obj)
    {
      CultureAwareRandomizedComparer randomizedComparer = obj as CultureAwareRandomizedComparer;
      if (randomizedComparer == null || this._ignoreCase != randomizedComparer._ignoreCase || !this._compareInfo.Equals((object) randomizedComparer._compareInfo))
        return false;
      return this._entropy == randomizedComparer._entropy;
    }

    public override int GetHashCode()
    {
      int hashCode = this._compareInfo.GetHashCode();
      return (this._ignoreCase ? ~hashCode : hashCode) ^ (int) (this._entropy & (long) int.MaxValue);
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
    {
      return (IEqualityComparer) new CultureAwareRandomizedComparer(this._compareInfo, this._ignoreCase);
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
    {
      return (IEqualityComparer) new CultureAwareComparer(this._compareInfo, this._ignoreCase);
    }
  }
}
