// Decompiled with JetBrains decompiler
// Type: System.Tuple`4
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
  /// <summary>Представляет кортеж из четырех компонентов.</summary>
  /// <typeparam name="T1">Тип первого компонента кортежа.</typeparam>
  /// <typeparam name="T2">Тип второго компонента кортежа.</typeparam>
  /// <typeparam name="T3">Тип третьего компонента кортежа.</typeparam>
  /// <typeparam name="T4">
  ///   Тип четвертого компонента кортежа.
  /// </typeparam>
  [__DynamicallyInvokable]
  [Serializable]
  public class Tuple<T1, T2, T3, T4> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
  {
    private readonly T1 m_Item1;
    private readonly T2 m_Item2;
    private readonly T3 m_Item3;
    private readonly T4 m_Item4;

    /// <summary>
    ///   Возвращает значение текущего <see cref="T:System.Tuple`4" /> компонента первого объекта.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`4" /> компонента первого объекта.
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
    ///   Возвращает значение текущего <see cref="T:System.Tuple`4" /> компонента второго объекта.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`4" /> компонента второго объекта.
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
    ///   Возвращает значение текущего <see cref="T:System.Tuple`4" /> третьего компонента объекта.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`4" /> третьего компонента объекта.
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
    ///   Возвращает значение текущего <see cref="T:System.Tuple`4" /> четвертого компонента объекта.
    /// </summary>
    /// <returns>
    ///   Значение текущего <see cref="T:System.Tuple`4" /> четвертого компонента объекта.
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
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Tuple`4" />.
    /// </summary>
    /// <param name="item1">Значение первого компонента кортежа.</param>
    /// <param name="item2">Значение второго компонента кортежа.</param>
    /// <param name="item3">Значение третьего компонента кортежа.</param>
    /// <param name="item4">Значение четвертого компонента кортежа</param>
    [__DynamicallyInvokable]
    public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
    {
      this.m_Item1 = item1;
      this.m_Item2 = item2;
      this.m_Item3 = item3;
      this.m_Item4 = item4;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий <see cref="T:System.Tuple`4" /> объект равен указанному объекту.
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
      Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
      if (tuple == null || !comparer.Equals((object) this.m_Item1, (object) tuple.m_Item1) || (!comparer.Equals((object) this.m_Item2, (object) tuple.m_Item2) || !comparer.Equals((object) this.m_Item3, (object) tuple.m_Item3)))
        return false;
      return comparer.Equals((object) this.m_Item4, (object) tuple.m_Item4);
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
      Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
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
      return comparer.Compare((object) this.m_Item4, (object) tuple.m_Item4);
    }

    /// <summary>
    ///   Возвращает хэш-код для текущего <see cref="T:System.Tuple`4" /> объекта.
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
      return Tuple.CombineHashCodes(comparer.GetHashCode((object) this.m_Item1), comparer.GetHashCode((object) this.m_Item2), comparer.GetHashCode((object) this.m_Item3), comparer.GetHashCode((object) this.m_Item4));
    }

    int ITupleInternal.GetHashCode(IEqualityComparer comparer)
    {
      return ((IStructuralEquatable) this).GetHashCode(comparer);
    }

    /// <summary>
    ///   Возвращает строку, представляющую значение этой <see cref="T:System.Tuple`4" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   Строковое представление <see cref="T:System.Tuple`4" /> объекта.
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
      sb.Append(")");
      return sb.ToString();
    }

    int ITuple.Length
    {
      get
      {
        return 4;
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
          default:
            throw new IndexOutOfRangeException();
        }
      }
    }
  }
}
