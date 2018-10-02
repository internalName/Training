// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.ComparisonComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  [Serializable]
  internal class ComparisonComparer<T> : Comparer<T>
  {
    private readonly Comparison<T> _comparison;

    public ComparisonComparer(Comparison<T> comparison)
    {
      this._comparison = comparison;
    }

    public override int Compare(T x, T y)
    {
      return this._comparison(x, y);
    }
  }
}
