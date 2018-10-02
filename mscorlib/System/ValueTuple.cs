// Decompiled with JetBrains decompiler
// Type: System.ValueTuple
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Предоставляет статические методы для создания кортежей значений.
  /// </summary>
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct ValueTuple : IEquatable<ValueTuple>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple>, IValueTupleInternal, ITuple
  {
    /// <summary>
    ///   Возвращает значение, показывающее, равен ли текущий экземпляр <see cref="T:System.ValueTuple" /> указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="obj" /> является экземпляром <see cref="T:System.ValueTuple" />; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      return obj is ValueTuple;
    }

    /// <summary>
    ///   Определяет, равны ли два экземпляра <see cref="T:System.ValueTuple" />.
    ///    Этот метод всегда возвращает значение <see langword="true" />.
    /// </summary>
    /// <param name="other">
    ///   Кортеж значений для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Этот метод всегда возвращает значение <see langword="true" />.
    /// </returns>
    public bool Equals(ValueTuple other)
    {
      return true;
    }

    bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
    {
      return other is ValueTuple;
    }

    int IComparable.CompareTo(object other)
    {
      if (other == null)
        return 1;
      if (!(other is ValueTuple))
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleIncorrectType", (object) this.GetType().ToString()), nameof (other));
      return 0;
    }

    /// <summary>
    ///   Сравнивает текущий экземпляр <see cref="T:System.ValueTuple" /> с указанным объектом.
    /// </summary>
    /// <param name="other">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Возвращает значение "0", если <paramref name=" other" /> является экземпляром <see cref="T:System.ValueTuple" />, и значение "1", если <paramref name="other" /> является <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="other" /> не является экземпляром <see cref="T:System.ValueTuple" />.
    /// </exception>
    public int CompareTo(ValueTuple other)
    {
      return 0;
    }

    int IStructuralComparable.CompareTo(object other, IComparer comparer)
    {
      if (other == null)
        return 1;
      if (!(other is ValueTuple))
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleIncorrectType", (object) this.GetType().ToString()), nameof (other));
      return 0;
    }

    /// <summary>
    ///   Возвращает хэш-код текущего экземпляра <see cref="T:System.ValueTuple" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего экземпляра <see cref="T:System.ValueTuple" />.
    /// </returns>
    public override int GetHashCode()
    {
      return 0;
    }

    int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
    {
      return 0;
    }

    int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
    {
      return 0;
    }

    /// <summary>
    ///   Возвращает строковое представление этого экземпляра <see cref="T:System.ValueTuple" />.
    /// </summary>
    /// <returns>Этот метод всегда возвращает "()".</returns>
    public override string ToString()
    {
      return "()";
    }

    string IValueTupleInternal.ToStringEnd()
    {
      return ")";
    }

    int ITuple.Length
    {
      get
      {
        return 0;
      }
    }

    object ITuple.this[int index]
    {
      get
      {
        throw new IndexOutOfRangeException();
      }
    }

    /// <summary>Создает новый кортеж значений без компонентов.</summary>
    /// <returns>Новый кортеж значений без компонентов.</returns>
    public static ValueTuple Create()
    {
      return new ValueTuple();
    }

    /// <summary>Создает новый кортеж значений с 1 компонентом.</summary>
    /// <param name="item1">
    ///   Значение единственного компонента кортежа значений.
    /// </param>
    /// <typeparam name="T1">
    ///   Тип единственного компонента кортежа значений.
    /// </typeparam>
    /// <returns>Кортеж значений с 1 компонентом.</returns>
    public static ValueTuple<T1> Create<T1>(T1 item1)
    {
      return new ValueTuple<T1>(item1);
    }

    /// <summary>
    /// 
    /// Создает новый кортеж значений с 2 компонентами.
    ///     </summary>
    /// <param name="item1">
    ///   Значение первого компонента кортежа значений.
    /// </param>
    /// <param name="item2">
    ///   Значение второго компонента кортежа значений.
    /// </param>
    /// <typeparam name="T1">
    ///   Тип первого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T2">
    ///   Тип второго компонента кортежа значений.
    /// </typeparam>
    /// <returns>Кортеж значений с 2 компонентами.</returns>
    public static ValueTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
    {
      return new ValueTuple<T1, T2>(item1, item2);
    }

    /// <summary>Создает новый кортеж значений с тремя компонентами.</summary>
    /// <param name="item1">
    ///   Значение первого компонента кортежа значений.
    /// </param>
    /// <param name="item2">
    ///   Значение второго компонента кортежа значений.
    /// </param>
    /// <param name="item3">
    ///   Значение третьего компонента кортежа значений.
    /// </param>
    /// <typeparam name="T1">
    ///   Тип первого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T2">
    ///   Тип второго компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T3">
    ///   Тип третьего компонента кортежа значений.
    /// </typeparam>
    /// <returns>Кортеж значений с 3 компонентами.</returns>
    public static ValueTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
    {
      return new ValueTuple<T1, T2, T3>(item1, item2, item3);
    }

    /// <summary>Создает новый кортеж значений с 4 компонентами.</summary>
    /// <param name="item1">
    ///   Значение первого компонента кортежа значений.
    /// </param>
    /// <param name="item2">
    ///   Значение второго компонента кортежа значений.
    /// </param>
    /// <param name="item3">
    ///   Значение третьего компонента кортежа значений.
    /// </param>
    /// <param name="item4">
    ///   Значение четвертого компонента кортежа значений.
    /// </param>
    /// <typeparam name="T1">
    ///   Тип первого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T2">
    ///   Тип второго компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T3">
    ///   Тип третьего компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T4">
    ///   Тип четвертого компонента кортежа значений.
    /// </typeparam>
    /// <returns>Кортеж значений с 4 компонентами.</returns>
    public static ValueTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
    {
      return new ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4);
    }

    /// <summary>Создает новый кортеж значений с 5 компонентами.</summary>
    /// <param name="item1">
    ///   Значение первого компонента кортежа значений.
    /// </param>
    /// <param name="item2">
    ///   Значение второго компонента кортежа значений.
    /// </param>
    /// <param name="item3">
    ///   Значение третьего компонента кортежа значений.
    /// </param>
    /// <param name="item4">
    ///   Значение четвертого компонента кортежа значений.
    /// </param>
    /// <param name="item5">
    ///   Значение пятого компонента кортежа значений.
    /// </param>
    /// <typeparam name="T1">
    ///   Тип первого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T2">
    ///   Тип второго компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T3">
    ///   Тип третьего компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T4">
    ///   Тип четвертого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T5">
    ///   Тип пятого компонента кортежа значений.
    /// </typeparam>
    /// <returns>Кортеж значений с 5 компонентами.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    {
      return new ValueTuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
    }

    /// <summary>Создает новый кортеж значений с 6 компонентами.</summary>
    /// <param name="item1">
    ///   Значение первого компонента кортежа значений.
    /// </param>
    /// <param name="item2">
    ///   Значение второго компонента кортежа значений.
    /// </param>
    /// <param name="item3">
    ///   Значение третьего компонента кортежа значений.
    /// </param>
    /// <param name="item4">
    ///   Значение четвертого компонента кортежа значений.
    /// </param>
    /// <param name="item5">
    ///   Значение пятого компонента кортежа значений.
    /// </param>
    /// <param name="item6">
    ///   Значение шестого компонента кортежа значений.
    /// </param>
    /// <typeparam name="T1">
    ///   Тип первого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T2">
    ///   Тип второго компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T3">
    ///   Тип третьего компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T4">
    ///   Тип четвертого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T5">
    ///   Тип пятого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T6">
    ///   Тип шестого компонента кортежа значений.
    /// </typeparam>
    /// <returns>Кортеж значений с 6 компонентами.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
    {
      return new ValueTuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
    }

    /// <summary>Создает новый кортеж значений с 7 компонентами.</summary>
    /// <param name="item1">
    ///   Значение первого компонента кортежа значений.
    /// </param>
    /// <param name="item2">
    ///   Значение второго компонента кортежа значений.
    /// </param>
    /// <param name="item3">
    ///   Значение третьего компонента кортежа значений.
    /// </param>
    /// <param name="item4">
    ///   Значение четвертого компонента кортежа значений.
    /// </param>
    /// <param name="item5">
    ///   Значение пятого компонента кортежа значений.
    /// </param>
    /// <param name="item6">
    ///   Значение шестого компонента кортежа значений.
    /// </param>
    /// <param name="item7">
    ///   Значение седьмого компонента кортежа значений.
    /// </param>
    /// <typeparam name="T1">
    ///   Тип первого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T2">
    ///   Тип второго компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T3">
    ///   Тип третьего компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T4">
    ///   Тип четвертого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T5">
    ///   Тип пятого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T6">
    ///   Тип шестого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T7">
    ///   Тип седьмого компонента кортежа значений.
    /// </typeparam>
    /// <returns>Кортеж значений с 7 компонентами.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
    {
      return new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
    }

    /// <summary>Создает новый кортеж значений с 8 компонентами.</summary>
    /// <param name="item1">
    ///   Значение первого компонента кортежа значений.
    /// </param>
    /// <param name="item2">
    ///   Значение второго компонента кортежа значений.
    /// </param>
    /// <param name="item3">
    ///   Значение третьего компонента кортежа значений.
    /// </param>
    /// <param name="item4">
    ///   Значение четвертого компонента кортежа значений.
    /// </param>
    /// <param name="item5">
    ///   Значение пятого компонента кортежа значений.
    /// </param>
    /// <param name="item6">
    ///   Значение шестого компонента кортежа значений.
    /// </param>
    /// <param name="item7">
    ///   Значение седьмого компонента кортежа значений.
    /// </param>
    /// <param name="item8">
    ///   Значение восьмого компонента кортежа значений.
    /// </param>
    /// <typeparam name="T1">
    ///   Тип первого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T2">
    ///   Тип второго компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T3">
    ///   Тип третьего компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T4">
    ///   Тип четвертого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T5">
    ///   Тип пятого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T6">
    ///   Тип шестого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T7">
    ///   Тип седьмого компонента кортежа значений.
    /// </typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого компонента кортежа значений.
    /// </typeparam>
    /// <returns>Кортеж значений с 8 компонентами.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
    {
      return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(item1, item2, item3, item4, item5, item6, item7, ValueTuple.Create<T8>(item8));
    }

    internal static int CombineHashCodes(int h1, int h2)
    {
      return System.Numerics.Hashing.HashHelpers.Combine(h1, h2);
    }

    internal static int CombineHashCodes(int h1, int h2, int h3)
    {
      return ValueTuple.CombineHashCodes(ValueTuple.CombineHashCodes(h1, h2), h3);
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
    {
      return ValueTuple.CombineHashCodes(ValueTuple.CombineHashCodes(h1, h2, h3), h4);
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
    {
      return ValueTuple.CombineHashCodes(ValueTuple.CombineHashCodes(h1, h2, h3, h4), h5);
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
    {
      return ValueTuple.CombineHashCodes(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5), h6);
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
    {
      return ValueTuple.CombineHashCodes(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6), h7);
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
    {
      return ValueTuple.CombineHashCodes(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7), h8);
    }
  }
}
