// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.ObjectEqualityComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  [Serializable]
  internal class ObjectEqualityComparer<T> : EqualityComparer<T>
  {
    public override bool Equals(T x, T y)
    {
      if ((object) x != null)
      {
        if ((object) y != null)
          return x.Equals((object) y);
        return false;
      }
      return (object) y == null;
    }

    public override int GetHashCode(T obj)
    {
      if ((object) obj == null)
        return 0;
      return obj.GetHashCode();
    }

    internal override int IndexOf(T[] array, T value, int startIndex, int count)
    {
      int num = startIndex + count;
      if ((object) value == null)
      {
        for (int index = startIndex; index < num; ++index)
        {
          if ((object) array[index] == null)
            return index;
        }
      }
      else
      {
        for (int index = startIndex; index < num; ++index)
        {
          if ((object) array[index] != null && array[index].Equals((object) value))
            return index;
        }
      }
      return -1;
    }

    internal override int LastIndexOf(T[] array, T value, int startIndex, int count)
    {
      int num = startIndex - count + 1;
      if ((object) value == null)
      {
        for (int index = startIndex; index >= num; --index)
        {
          if ((object) array[index] == null)
            return index;
        }
      }
      else
      {
        for (int index = startIndex; index >= num; --index)
        {
          if ((object) array[index] != null && array[index].Equals((object) value))
            return index;
        }
      }
      return -1;
    }

    public override bool Equals(object obj)
    {
      return obj is ObjectEqualityComparer<T>;
    }

    public override int GetHashCode()
    {
      return this.GetType().Name.GetHashCode();
    }
  }
}
