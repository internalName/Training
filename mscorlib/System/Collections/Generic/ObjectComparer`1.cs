// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.ObjectComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  [Serializable]
  internal class ObjectComparer<T> : Comparer<T>
  {
    public override int Compare(T x, T y)
    {
      return Comparer.Default.Compare((object) x, (object) y);
    }

    public override bool Equals(object obj)
    {
      return obj is ObjectComparer<T>;
    }

    public override int GetHashCode()
    {
      return this.GetType().Name.GetHashCode();
    }
  }
}
