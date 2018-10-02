// Decompiled with JetBrains decompiler
// Type: System.ArraySegment`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System
{
  /// <summary>Определяет границы фрагмента одномерного массива.</summary>
  /// <typeparam name="T">
  ///   Тип элементов во фрагменте массива.
  /// </typeparam>
  [__DynamicallyInvokable]
  [Serializable]
  public struct ArraySegment<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
  {
    private T[] _array;
    private int _offset;
    private int _count;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ArraySegment`1" /> структуры, разделяющий элементы в указанном массиве.
    /// </summary>
    /// <param name="array">
    ///   Массив, для которого создается программа-оболочка.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public ArraySegment(T[] array)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      this._array = array;
      this._offset = 0;
      this._count = array.Length;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ArraySegment`1" /> структуру, которая разделяет указанный диапазон элементов в указанном массиве.
    /// </summary>
    /// <param name="array">
    ///   Массив, содержащий диапазон элементов, границы которого необходимо задать.
    /// </param>
    /// <param name="offset">
    ///   Индекс первого элемента диапазона при индексации массива с нуля.
    /// </param>
    /// <param name="count">Число элементов в диапазоне.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="offset" /> или <paramref name="count" /> меньше 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="offset" /> и <paramref name="count" /> не указывают допустимый диапазон в <paramref name="array" />.
    /// </exception>
    [__DynamicallyInvokable]
    public ArraySegment(T[] array, int offset, int count)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      this._array = array;
      this._offset = offset;
      this._count = count;
    }

    /// <summary>
    ///   Возвращает исходный массив, который содержит диапазон элементов, находящийся в пределах установленных границ фрагмента массива.
    /// </summary>
    /// <returns>
    ///   Исходный массив, который был передан в конструктор и содержит диапазоне, ограниченном <see cref="T:System.ArraySegment`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public T[] Array
    {
      [__DynamicallyInvokable] get
      {
        return this._array;
      }
    }

    /// <summary>
    ///   Возвращает положение первого элемента в диапазоне, ограниченном фрагментом массива, относительно начала исходного массива.
    /// </summary>
    /// <returns>
    ///   Положение первого элемента в диапазоне, ограниченном <see cref="T:System.ArraySegment`1" />, относительно начала исходного массива.
    /// </returns>
    [__DynamicallyInvokable]
    public int Offset
    {
      [__DynamicallyInvokable] get
      {
        return this._offset;
      }
    }

    /// <summary>
    ///   Возвращает количество элементов в диапазоне, ограниченном фрагментом массива.
    /// </summary>
    /// <returns>
    ///   Число элементов в диапазоне, ограниченном <see cref="T:System.ArraySegment`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this._count;
      }
    }

    /// <summary>Возвращает хэш-код текущего экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      if (this._array != null)
        return this._array.GetHashCode() ^ this._offset ^ this._count;
      return 0;
    }

    /// <summary>
    ///   Определяет, равен ли указанный объект текущему экземпляру.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанный объект <see cref="T:System.ArraySegment`1" /> структуры и равен текущему экземпляру; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj is ArraySegment<T>)
        return this.Equals((ArraySegment<T>) obj);
      return false;
    }

    /// <summary>
    ///   Определяет, является ли указанный <see cref="T:System.ArraySegment`1" /> структуры равен текущему экземпляру.
    /// </summary>
    /// <param name="obj">
    ///   Структура для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанный <see cref="T:System.ArraySegment`1" /> структуры равен текущему экземпляру; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(ArraySegment<T> obj)
    {
      if (obj._array == this._array && obj._offset == this._offset)
        return obj._count == this._count;
      return false;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.ArraySegment`1" /> структуры равны.
    /// </summary>
    /// <param name="a">
    ///   Структура, которая находится слева от оператора равенства.
    /// </param>
    /// <param name="b">
    ///   Структура, которая находится справа от оператора равенства.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(ArraySegment<T> a, ArraySegment<T> b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.ArraySegment`1" /> структуры не равны.
    /// </summary>
    /// <param name="a">
    ///   Структура, которая находится слева от оператора неравенства.
    /// </param>
    /// <param name="b">
    ///   Структура, которая находится справа от оператора неравенства.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(ArraySegment<T> a, ArraySegment<T> b)
    {
      return !(a == b);
    }

    [__DynamicallyInvokable]
    T IList<T>.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        if (this._array == null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
        if (index < 0 || index >= this._count)
          throw new ArgumentOutOfRangeException(nameof (index));
        return this._array[this._offset + index];
      }
      [__DynamicallyInvokable] set
      {
        if (this._array == null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
        if (index < 0 || index >= this._count)
          throw new ArgumentOutOfRangeException(nameof (index));
        this._array[this._offset + index] = value;
      }
    }

    [__DynamicallyInvokable]
    int IList<T>.IndexOf(T item)
    {
      if (this._array == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
      int num = System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
      if (num < 0)
        return -1;
      return num - this._offset;
    }

    [__DynamicallyInvokable]
    void IList<T>.Insert(int index, T item)
    {
      throw new NotSupportedException();
    }

    [__DynamicallyInvokable]
    void IList<T>.RemoveAt(int index)
    {
      throw new NotSupportedException();
    }

    [__DynamicallyInvokable]
    T IReadOnlyList<T>.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        if (this._array == null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
        if (index < 0 || index >= this._count)
          throw new ArgumentOutOfRangeException(nameof (index));
        return this._array[this._offset + index];
      }
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    void ICollection<T>.Add(T item)
    {
      throw new NotSupportedException();
    }

    [__DynamicallyInvokable]
    void ICollection<T>.Clear()
    {
      throw new NotSupportedException();
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.Contains(T item)
    {
      if (this._array == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
      return System.Array.IndexOf<T>(this._array, item, this._offset, this._count) >= 0;
    }

    [__DynamicallyInvokable]
    void ICollection<T>.CopyTo(T[] array, int arrayIndex)
    {
      if (this._array == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
      System.Array.Copy((System.Array) this._array, this._offset, (System.Array) array, arrayIndex, this._count);
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.Remove(T item)
    {
      throw new NotSupportedException();
    }

    [__DynamicallyInvokable]
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      if (this._array == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
      return (IEnumerator<T>) new ArraySegment<T>.ArraySegmentEnumerator(this);
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      if (this._array == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
      return (IEnumerator) new ArraySegment<T>.ArraySegmentEnumerator(this);
    }

    [Serializable]
    private sealed class ArraySegmentEnumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
      private T[] _array;
      private int _start;
      private int _end;
      private int _current;

      internal ArraySegmentEnumerator(ArraySegment<T> arraySegment)
      {
        this._array = arraySegment._array;
        this._start = arraySegment._offset;
        this._end = this._start + arraySegment._count;
        this._current = this._start - 1;
      }

      public bool MoveNext()
      {
        if (this._current >= this._end)
          return false;
        ++this._current;
        return this._current < this._end;
      }

      public T Current
      {
        get
        {
          if (this._current < this._start)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._current >= this._end)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          return this._array[this._current];
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      void IEnumerator.Reset()
      {
        this._current = this._start - 1;
      }

      public void Dispose()
      {
      }
    }
  }
}
