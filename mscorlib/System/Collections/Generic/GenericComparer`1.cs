// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.GenericComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  [Serializable]
  internal class GenericComparer<T> : Comparer<T> where T : IComparable<T>
  {
    public override int Compare(T x, T y)
    {
      if ((object) x != null)
      {
        if ((object) y != null)
          return x.CompareTo(y);
        return 1;
      }
      return (object) y != null ? -1 : 0;
    }

    public override bool Equals(object obj)
    {
      return obj is GenericComparer<T>;
    }

    public override int GetHashCode()
    {
      return this.GetType().Name.GetHashCode();
    }
  }
}
