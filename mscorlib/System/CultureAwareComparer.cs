// Decompiled with JetBrains decompiler
// Type: System.CultureAwareComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
  [Serializable]
  internal sealed class CultureAwareComparer : StringComparer, IWellKnownStringEqualityComparer
  {
    private CompareInfo _compareInfo;
    private bool _ignoreCase;
    [OptionalField]
    private CompareOptions _options;
    [NonSerialized]
    private bool _initializing;

    internal CultureAwareComparer(CultureInfo culture, bool ignoreCase)
    {
      this._compareInfo = culture.CompareInfo;
      this._ignoreCase = ignoreCase;
      this._options = ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None;
    }

    internal CultureAwareComparer(CompareInfo compareInfo, bool ignoreCase)
    {
      this._compareInfo = compareInfo;
      this._ignoreCase = ignoreCase;
      this._options = ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None;
    }

    internal CultureAwareComparer(CompareInfo compareInfo, CompareOptions options)
    {
      this._compareInfo = compareInfo;
      this._options = options;
      this._ignoreCase = (options & CompareOptions.IgnoreCase) == CompareOptions.IgnoreCase || (options & CompareOptions.OrdinalIgnoreCase) == CompareOptions.OrdinalIgnoreCase;
    }

    public override int Compare(string x, string y)
    {
      this.EnsureInitialization();
      if ((object) x == (object) y)
        return 0;
      if (x == null)
        return -1;
      if (y == null)
        return 1;
      return this._compareInfo.Compare(x, y, this._options);
    }

    public override bool Equals(string x, string y)
    {
      this.EnsureInitialization();
      if ((object) x == (object) y)
        return true;
      if (x == null || y == null)
        return false;
      return this._compareInfo.Compare(x, y, this._options) == 0;
    }

    public override int GetHashCode(string obj)
    {
      this.EnsureInitialization();
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      return this._compareInfo.GetHashCodeOfString(obj, this._options);
    }

    public override bool Equals(object obj)
    {
      this.EnsureInitialization();
      CultureAwareComparer cultureAwareComparer = obj as CultureAwareComparer;
      if (cultureAwareComparer == null || this._ignoreCase != cultureAwareComparer._ignoreCase || !this._compareInfo.Equals((object) cultureAwareComparer._compareInfo))
        return false;
      return this._options == cultureAwareComparer._options;
    }

    public override int GetHashCode()
    {
      this.EnsureInitialization();
      int hashCode = this._compareInfo.GetHashCode();
      if (!this._ignoreCase)
        return hashCode;
      return ~hashCode;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnsureInitialization()
    {
      if (!this._initializing)
        return;
      this._options |= this._ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None;
      this._initializing = false;
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this._initializing = true;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this.EnsureInitialization();
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
    {
      return (IEqualityComparer) new CultureAwareRandomizedComparer(this._compareInfo, this._ignoreCase);
    }

    IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
    {
      return (IEqualityComparer) this;
    }
  }
}
