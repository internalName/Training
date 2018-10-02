// Decompiled with JetBrains decompiler
// Type: System.Tuple`8
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
  /// <summary>Представляет n- кортеж, где n равно 8 или больше.</summary>
  /// <typeparam name="T1">Тип первого компонента кортежа.</typeparam>
  /// <typeparam name="T2">Тип второго компонента кортежа.</typeparam>
  /// <typeparam name="T3">Тип третьего компонента кортежа.</typeparam>
  /// <typeparam name="T4">
  ///   Тип четвертого компонента кортежа.
  /// </typeparam>
  /// <typeparam name="T5">Тип пятого компонента кортежа.</typeparam>
  /// <typeparam name="T6">Тип шестого компонента кортежа.</typeparam>
  /// <typeparam name="T7">Тип седьмого компонента кортежа.</typeparam>
  /// <typeparam name="TRest">
  ///   Всех универсальных <see langword="Tuple" /> объект, который определяет типы кортежа остальных компонентов.
  /// </typeparam>
  [__DynamicallyInvokable]
  [Serializable]
  public class Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
  {
    private readonly T1 m_Item1;
    private readonly T2 m_Item2;
    private readonly T3 m_Item3;
    private readonly T4 m_Item4;
    private readonly T5 m_Item5;
    private readonly T6 m_Item6;
    private readonly T7 m_Item7;
    private readonly TRest m_Rest;

    /// <summary>
    ///   Возвращает значение текущего <see cref="T:System.Tuple`8" /> компонента первого объекта.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`8" /> компонента первого объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public T1 Item1
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Item1;
      }
    }

    /// <summary>
    ///   Возвращает значение текущего <see cref="T:System.Tuple`8" /> компонента второго объекта.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`8" /> компонента второго объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public T2 Item2
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Item2;
      }
    }

    /// <summary>
    ///   Возвращает значение текущего <see cref="T:System.Tuple`8" /> третьего компонента объекта.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`8" /> третьего компонента объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public T3 Item3
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Item3;
      }
    }

    /// <summary>
    ///   Возвращает значение текущего <see cref="T:System.Tuple`8" /> четвертого компонента объекта.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`8" /> четвертого компонента объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public T4 Item4
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Item4;
      }
    }

    /// <summary>
    ///   Возвращает значение текущего <see cref="T:System.Tuple`8" /> пятого компонента объекта.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`8" /> пятого компонента объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public T5 Item5
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Item5;
      }
    }

    /// <summary>
    ///   Возвращает значение текущего <see cref="T:System.Tuple`8" /> шестого компонента объекта.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`8" /> шестого компонента объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public T6 Item6
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Item6;
      }
    }

    /// <summary>
    ///   Возвращает значение текущего <see cref="T:System.Tuple`8" /> седьмого компонента объекта.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`8" /> седьмого компонента объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public T7 Item7
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Item7;
      }
    }

    /// <summary>
    ///   Возвращает текущий <see cref="T:System.Tuple`8" /> объект остальных компонентов.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`8" /> объект остальных компонентов.
    /// </returns>
    [__DynamicallyInvokable]
    public TRest Rest
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Rest;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Tuple`8" />.
    /// </summary>
    /// <param name="item1">Значение первого компонента кортежа.</param>
    /// <param name="item2">Значение второго компонента кортежа.</param>
    /// <param name="item3">Значение третьего компонента кортежа.</param>
    /// <param name="item4">Значение четвертого компонента кортежа</param>
    /// <param name="item5">Значение пятого компонента кортежа.</param>
    /// <param name="item6">Значение шестого компонента кортежа.</param>
    /// <param name="item7">Значение седьмого компонента кортежа.</param>
    /// <param name="rest">
    ///   Всех универсальных <see langword="Tuple" /> объект, содержащий значения кортежа остальных компонентов.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="rest" />не является универсальным <see langword="Tuple" /> объекта.
    /// </exception>
    [__DynamicallyInvokable]
    public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
    {
      if (!((object) rest is ITupleInternal))
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_TupleLastArgumentNotATuple"));
      this.m_Item1 = item1;
      this.m_Item2 = item2;
      this.m_Item3 = item3;
      this.m_Item4 = item4;
      this.m_Item5 = item5;
      this.m_Item6 = item6;
      this.m_Item7 = item7;
      this.m_Rest = rest;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий <see cref="T:System.Tuple`8" /> объект равен указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если текущий экземпляр равен заданному объекту; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      return ((IStructuralEquatable) this).Equals(obj, (IEqualityComparer) EqualityComparer<object>.Default);
    }

    [__DynamicallyInvokable]
    bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
    {
      if (other == null)
        return false;
      Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
      if (tuple == null || !comparer.Equals((object) this.m_Item1, (object) tuple.m_Item1) || (!comparer.Equals((object) this.m_Item2, (object) tuple.m_Item2) || !comparer.Equals((object) this.m_Item3, (object) tuple.m_Item3)) || (!comparer.Equals((object) this.m_Item4, (object) tuple.m_Item4) || !comparer.Equals((object) this.m_Item5, (object) tuple.m_Item5) || (!comparer.Equals((object) this.m_Item6, (object) tuple.m_Item6) || !comparer.Equals((object) this.m_Item7, (object) tuple.m_Item7))))
        return false;
      return comparer.Equals((object) this.m_Rest, (object) tuple.m_Rest);
    }

    [__DynamicallyInvokable]
    int IComparable.CompareTo(object obj)
    {
      return ((IStructuralComparable) this).CompareTo(obj, (IComparer) Comparer<object>.Default);
    }

    [__DynamicallyInvokable]
    int IStructuralComparable.CompareTo(object other, IComparer comparer)
    {
      if (other == null)
        return 1;
      Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
      if (tuple == null)
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_TupleIncorrectType", (object) this.GetType().ToString()), nameof (other));
      int num1 = comparer.Compare((object) this.m_Item1, (object) tuple.m_Item1);
      if (num1 != 0)
        return num1;
      int num2 = comparer.Compare((object) this.m_Item2, (object) tuple.m_Item2);
      if (num2 != 0)
        return num2;
      int num3 = comparer.Compare((object) this.m_Item3, (object) tuple.m_Item3);
      if (num3 != 0)
        return num3;
      int num4 = comparer.Compare((object) this.m_Item4, (object) tuple.m_Item4);
      if (num4 != 0)
        return num4;
      int num5 = comparer.Compare((object) this.m_Item5, (object) tuple.m_Item5);
      if (num5 != 0)
        return num5;
      int num6 = comparer.Compare((object) this.m_Item6, (object) tuple.m_Item6);
      if (num6 != 0)
        return num6;
      int num7 = comparer.Compare((object) this.m_Item7, (object) tuple.m_Item7);
      if (num7 != 0)
        return num7;
      return comparer.Compare((object) this.m_Rest, (object) tuple.m_Rest);
    }

    /// <summary>
    ///   Вычисляет хэш-код для текущего <see cref="T:System.Tuple`8" /> объекта.
    /// </summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return ((IStructuralEquatable) this).GetHashCode((IEqualityComparer) EqualityComparer<object>.Default);
    }

    [__DynamicallyInvokable]
    int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
    {
      ITupleInternal rest = (ITupleInternal) (object) this.m_Rest;
      if (rest.Length >= 8)
        return rest.GetHashCode(comparer);
      switch (8 - rest.Length)
      {
        case 1:
          return Tuple.CombineHashCodes(comparer.GetHashCode((object) this.m_Item7), rest.GetHashCode(comparer));
        case 2:
          return Tuple.CombineHashCodes(comparer.GetHashCode((object) this.m_Item6), comparer.GetHashCode((object) this.m_Item7), rest.GetHashCode(comparer));
        case 3:
          return Tuple.CombineHashCodes(comparer.GetHashCode((object) this.m_Item5), comparer.GetHashCode((object) this.m_Item6), comparer.GetHashCode((object) this.m_Item7), rest.GetHashCode(comparer));
        case 4:
          return Tuple.CombineHashCodes(comparer.GetHashCode((object) this.m_Item4), comparer.GetHashCode((object) this.m_Item5), comparer.GetHashCode((object) this.m_Item6), comparer.GetHashCode((object) this.m_Item7), rest.GetHashCode(comparer));
        case 5:
          return Tuple.CombineHashCodes(comparer.GetHashCode((object) this.m_Item3), comparer.GetHashCode((object) this.m_Item4), comparer.GetHashCode((object) this.m_Item5), comparer.GetHashCode((object) this.m_Item6), comparer.GetHashCode((object) this.m_Item7), rest.GetHashCode(comparer));
        case 6:
          return Tuple.CombineHashCodes(comparer.GetHashCode((object) this.m_Item2), comparer.GetHashCode((object) this.m_Item3), comparer.GetHashCode((object) this.m_Item4), comparer.GetHashCode((object) this.m_Item5), comparer.GetHashCode((object) this.m_Item6), comparer.GetHashCode((object) this.m_Item7), rest.GetHashCode(comparer));
        case 7:
          return Tuple.CombineHashCodes(comparer.GetHashCode((object) this.m_Item1), comparer.GetHashCode((object) this.m_Item2), comparer.GetHashCode((object) this.m_Item3), comparer.GetHashCode((object) this.m_Item4), comparer.GetHashCode((object) this.m_Item5), comparer.GetHashCode((object) this.m_Item6), comparer.GetHashCode((object) this.m_Item7), rest.GetHashCode(comparer));
        default:
          return -1;
      }
    }

    int ITupleInternal.GetHashCode(IEqualityComparer comparer)
    {
      return ((IStructuralEquatable) this).GetHashCode(comparer);
    }

    /// <summary>
    ///   Возвращает строку, представляющую значение этой <see cref="T:System.Tuple`8" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   Строковое представление <see cref="T:System.Tuple`8" /> объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("(");
      return ((ITupleInternal) this).ToString(sb);
    }

    string ITupleInternal.ToString(StringBuilder sb)
    {
      sb.Append((object) this.m_Item1);
      sb.Append(", ");
      sb.Append((object) this.m_Item2);
      sb.Append(", ");
      sb.Append((object) this.m_Item3);
      sb.Append(", ");
      sb.Append((object) this.m_Item4);
      sb.Append(", ");
      sb.Append((object) this.m_Item5);
      sb.Append(", ");
      sb.Append((object) this.m_Item6);
      sb.Append(", ");
      sb.Append((object) this.m_Item7);
      sb.Append(", ");
      return ((ITupleInternal) (object) this.m_Rest).ToString(sb);
    }

    int ITuple.Length
    {
      get
      {
        return 7 + ((ITuple) (object) this.Rest).Length;
      }
    }

    object ITuple.this[int index]
    {
      get
      {
        switch (index)
        {
          case 0:
            return (object) this.Item1;
          case 1:
            return (object) this.Item2;
          case 2:
            return (object) this.Item3;
          case 3:
            return (object) this.Item4;
          case 4:
            return (object) this.Item5;
          case 5:
            return (object) this.Item6;
          case 6:
            return (object) this.Item7;
          default:
            return ((ITuple) (object) this.Rest)[index - 7];
        }
      }
    }
  }
}
