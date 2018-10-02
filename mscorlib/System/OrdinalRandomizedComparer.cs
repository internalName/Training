// Decompiled with JetBrains decompiler
// Type: System.OrdinalRandomizedComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Security;

namespace System
{
  internal sealed class OrdinalRandomizedComparer : StringComparer, IWellKnownStringEqualityComparer
  {
    private bool _ignoreCase;
    private long _entropy;

    internal OrdinalRandomizedComparer(bool ignoreCase)
    {
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
      if (this._ignoreCase)
        return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
      return string.CompareOrdinal(x, y);
    }

    public override bool Equals(string x, string y)
    {
      if ((object) x == (object) y)
        return true;
      if (x == null || y == null)
        return false;
      if (!this._ignoreCase)
        return x.Equals(y);
      if (x.Length != y.Length)
        return false;
      return string.Compare(x, y, StringComparison.OrdinalIgnoreCase) == 0;
    }

    [SecuritySafeCritical]
    public override int GetHashCode(string obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (this._ignoreCase)
        return TextInfo.GetHashCodeOrdinalIgnoreCase(obj, true, this._entropy);
      return string.InternalMarvin32HashString(obj, obj.Length, this._entropy);
    }

    public override bool Equals(object obj)
    {
      OrdinalRandomizedComparer randomizedComparer = obj as OrdinalRandomizedComparer;
      if (randomizedComparer == null || this._ignoreCase != randomizedComparer._ignoreCase)
        return false;
      return this._entropy == randomizedComparer._entropy;
    }

    public override int GetHashCode()
    {
      int hashCode = nameof (OrdinalRandomizedComparer).GetHashCode();
      return (this._ignoreCase ? ~hashCode : hashCode) ^ (int) (this._entropy & (long) int.MaxValue);
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
    {
      return (IEqualityComparer) new OrdinalRandomizedComparer(this._ignoreCase);
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
    {
      return (IEqualityComparer) new OrdinalComparer(this._ignoreCase);
    }
  }
}
