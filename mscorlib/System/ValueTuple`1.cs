// Decompiled with JetBrains decompiler
// Type: System.ValueTuple`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
  /// <summary>Представляет кортеж значений с одним компонентом.</summary>
  /// <typeparam name="T1">
  ///   Тип единственного элемента кортежа значений.
  /// </typeparam>
  [Serializable]
  public struct ValueTuple<T1> : IEquatable<ValueTuple<T1>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1>>, IValueTupleInternal, ITuple
  {
    /// <summary>
    ///   Получает значение первого элемента текущего экземпляра <see cref="T:System.ValueTuple`1" />.
    /// </summary>
    /// <returns>
    ///   Значение первого элемента текущего экземпляра <see cref="T:System.ValueTuple`1" />.
    /// </returns>
    public T1 Item1;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ValueTuple`1" />.
    /// </summary>
    /// <param name="item1">Первый элемент кортежа значений.</param>
    public ValueTuple(T1 item1)
    {
      this.Item1 = item1;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли текущий экземпляр <see cref="T:System.ValueTuple`1" /> указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с данным экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий экземпляр равен указанному объекту; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is ValueTuple<T1>)
        return this.Equals((ValueTuple<T1>) obj);
      return false;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли текущий экземпляр <see cref="T:System.ValueTuple`1" /> указанному экземпляру <see cref="T:System.ValueTuple`1" />.
    /// </summary>
    /// <param name="other">
    ///   Кортеж значений для сравнения с этим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий экземпляр равен указанному кортежу; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(ValueTuple<T1> other)
    {
      return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1);
    }

    bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
    {
      if (other == null || !(other is ValueTuple<T1>))
        return false;
      ValueTuple<T1> valueTuple = (ValueTuple<T1>) other;
      return comparer.Equals((object) this.Item1, (object) valueTuple.Item1);
    }

    int IComparable.CompareTo(object other)
    {
      if (other == null)
        return 1;
      if (!(other is ValueTuple<T1>))
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleIncorrectType", (object) this.GetType().ToString()), nameof (other));
      return Comparer<T1>.Default.Compare(this.Item1, ((ValueTuple<T1>) other).Item1);
    }

    /// <summary>
    ///   Сравнивает текущий экземпляр <see cref="T:System.ValueTuple`1" /> с указанным экземпляром <see cref="T:System.ValueTuple`1" />.
    /// </summary>
    /// <param name="other">
    ///   Кортеж значений для сравнения с этим экземпляром.
    /// </param>
    /// <returns>
    /// Целое число со знаком, определяющее относительное положение этого экземпляра и параметра <paramref name="other" /> в порядке сортировки, как показано в следующей таблице.
    /// 
    ///         Значение
    /// 
    ///         Описание
    /// 
    ///         Отрицательное целое число
    /// 
    ///         Данный экземпляр предшествует параметру <paramref name="other" />.
    /// 
    ///         Нуль
    /// 
    ///         У этого экземпляра та же позиция в порядке сортировки, что и у <paramref name="other" />.
    /// 
    ///         Положительное целое число
    /// 
    ///         Данный экземпляр стоит после параметра <paramref name="other" />.
    ///       </returns>
    public int CompareTo(ValueTuple<T1> other)
    {
      return Comparer<T1>.Default.Compare(this.Item1, other.Item1);
    }

    int IStructuralComparable.CompareTo(object other, IComparer comparer)
    {
      if (other == null)
        return 1;
      if (!(other is ValueTuple<T1>))
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleIncorrectType", (object) this.GetType().ToString()), nameof (other));
      ValueTuple<T1> valueTuple = (ValueTuple<T1>) other;
      return comparer.Compare((object) this.Item1, (object) valueTuple.Item1);
    }

    /// <summary>
    ///   Вычисляет хэш-код для текущего экземпляра <see cref="T:System.ValueTuple`1" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего экземпляра <see cref="T:System.ValueTuple`1" />.
    /// </returns>
    public override int GetHashCode()
    {
      return EqualityComparer<T1>.Default.GetHashCode(this.Item1);
    }

    int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
    {
      return comparer.GetHashCode((object) this.Item1);
    }

    int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
    {
      return comparer.GetHashCode((object) this.Item1);
    }

    /// <summary>
    ///   Возвращает строковое представление значения этого экземпляра <see cref="T:System.ValueTuple`1" />.
    /// </summary>
    /// <returns>
    ///   Строковое представление этого экземпляра <see cref="T:System.ValueTuple`1" />.
    /// </returns>
    public override string ToString()
    {
      string str1 = "(";
      ref T1 local1 = ref this.Item1;
      string str2;
      if ((object) default (T1) == null)
      {
        T1 obj = local1;
        ref T1 local2 = ref obj;
        if ((object) obj == null)
        {
          str2 = (string) null;
          goto label_4;
        }
        else
          local1 = ref local2;
      }
      str2 = local1.ToString();
label_4:
      string str3 = ")";
      return str1 + str2 + str3;
    }

    string IValueTupleInternal.ToStringEnd()
    {
      ref T1 local1 = ref this.Item1;
      string str1;
      if ((object) default (T1) == null)
      {
        T1 obj = local1;
        ref T1 local2 = ref obj;
        if ((object) obj == null)
        {
          str1 = (string) null;
          goto label_4;
        }
        else
          local1 = ref local2;
      }
      str1 = local1.ToString();
label_4:
      string str2 = ")";
      return str1 + str2;
    }

    int ITuple.Length
    {
      get
      {
        return 1;
      }
    }

    object ITuple.this[int index]
    {
      get
      {
        if (index != 0)
          throw new IndexOutOfRangeException();
        return (object) this.Item1;
      }
    }
  }
}
