// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.NullableEqualityComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  [Serializable]
  internal class NullableEqualityComparer<T> : EqualityComparer<T?> where T : struct, IEquatable<T>
  {
    public override bool Equals(T? x, T? y)
    {
      if (x.HasValue)
      {
        if (y.HasValue)
          return x.value.Equals(y.value);
        return false;
      }
      return !y.HasValue;
    }

    public override int GetHashCode(T? obj)
    {
      return obj.GetHashCode();
    }

    internal override int IndexOf(T?[] array, T? value, int startIndex, int count)
    {
      int num = startIndex + count;
      if (!value.HasValue)
      {
        for (int index = startIndex; index < num; ++index)
        {
          if (!array[index].HasValue)
            return index;
        }
      }
      else
      {
        for (int index = startIndex; index < num; ++index)
        {
          if (array[index].HasValue && array[index].value.Equals(value.value))
            return index;
        }
      }
      return -1;
    }

    internal override int LastIndexOf(T?[] array, T? value, int startIndex, int count)
    {
      int num = startIndex - count + 1;
      if (!value.HasValue)
      {
        for (int index = startIndex; index >= num; --index)
        {
          if (!array[index].HasValue)
            return index;
        }
      }
      else
      {
        for (int index = startIndex; index >= num; --index)
        {
          if (array[index].HasValue && array[index].value.Equals(value.value))
            return index;
        }
      }
      return -1;
    }

    public override bool Equals(object obj)
    {
      return obj is NullableEqualityComparer<T>;
    }

    public override int GetHashCode()
    {
      return this.GetType().Name.GetHashCode();
    }
  }
}
