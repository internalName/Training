// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.NullableComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  [Serializable]
  internal class NullableComparer<T> : Comparer<T?> where T : struct, IComparable<T>
  {
    public override int Compare(T? x, T? y)
    {
      if (x.HasValue)
      {
        if (y.HasValue)
          return x.value.CompareTo(y.value);
        return 1;
      }
      return y.HasValue ? -1 : 0;
    }

    public override bool Equals(object obj)
    {
      return obj is NullableComparer<T>;
    }

    public override int GetHashCode()
    {
      return this.GetType().Name.GetHashCode();
    }
  }
}
