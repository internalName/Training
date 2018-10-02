﻿// Decompiled with JetBrains decompiler
// Type: System.ValueTuple`3
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>Представляет кортеж значений с 3 компонентами.</summary>
  /// <typeparam name="T1">
  ///   Тип первого элемента кортежа значений.
  /// </typeparam>
  /// <typeparam name="T2">
  ///   Тип второго элемента кортежа значений.
  /// </typeparam>
  /// <typeparam name="T3">
  ///   Тип третьего элемента кортежа значений.
  /// </typeparam>
  [Serializable]
  [StructLayout(LayoutKind.Auto)]
  public struct ValueTuple<T1, T2, T3> : IEquatable<ValueTuple<T1, T2, T3>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3>>, IValueTupleInternal, ITuple
  {
    /// <summary>
    ///   Получает значение первого элемента текущего экземпляра <see cref="T:System.ValueTuple`3" />.
    /// </summary>
    /// <returns>
    ///   Значение первого элемента текущего экземпляра <see cref="T:System.ValueTuple`3" />.
    /// </returns>
    public T1 Item1;
    /// <summary>
    ///   Получает значение второго элемента текущего экземпляра <see cref="T:System.ValueTuple`3" />.
    /// </summary>
    /// <returns>
    ///   Значение второго элемента текущего экземпляра <see cref="T:System.ValueTuple`3" />.
    /// </returns>
    public T2 Item2;
    /// <summary>
    ///   Получает значение третьего элемента текущего экземпляра <see cref="T:System.ValueTuple`3" />.
    /// </summary>
    /// <returns>
    ///   Значение третьего элемента текущего экземпляра <see cref="T:System.ValueTuple`3" />.
    /// </returns>
    public T3 Item3;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ValueTuple`3" />.
    /// </summary>
    /// <param name="item1">Первый элемент кортежа значений.</param>
    /// <param name="item2">Второй элемент кортежа значений.</param>
    /// <param name="item3">Третий элемент кортежа значений.</param>
    public ValueTuple(T1 item1, T2 item2, T3 item3)
    {
      this.Item1 = item1;
      this.Item2 = item2;
      this.Item3 = item3;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли текущий экземпляр <see cref="T:System.ValueTuple`3" /> указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с данным экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий экземпляр равен указанному объекту; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is ValueTuple<T1, T2, T3>)
        return this.Equals((ValueTuple<T1, T2, T3>) obj);
      return false;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли текущий экземпляр <see cref="T:System.ValueTuple`3" /> указанному экземпляру <see cref="T:System.ValueTuple`3" />.
    /// </summary>
    /// <param name="other">
    ///   Кортеж значений для сравнения с этим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий экземпляр равен указанному кортежу; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(ValueTuple<T1, T2, T3> other)
    {
      if (EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2))
        return EqualityComparer<T3>.Default.Equals(this.Item3, other.Item3);
      return false;
    }

    bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
    {
      if (other == null || !(other is ValueTuple<T1, T2, T3>))
        return false;
      ValueTuple<T1, T2, T3> valueTuple = (ValueTuple<T1, T2, T3>) other;
      if (comparer.Equals((object) this.Item1, (object) valueTuple.Item1) && comparer.Equals((object) this.Item2, (object) valueTuple.Item2))
        return comparer.Equals((object) this.Item3, (object) valueTuple.Item3);
      return false;
    }

    int IComparable.CompareTo(object other)
    {
      if (other == null)
        return 1;
      if (!(other is ValueTuple<T1, T2, T3>))
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleIncorrectType", (object) this.GetType().ToString()), nameof (other));
      return this.CompareTo((ValueTuple<T1, T2, T3>) other);
    }

    /// <summary>
    ///   Сравнивает текущий экземпляр <see cref="T:System.ValueTuple`3" /> с указанным экземпляром <see cref="T:System.ValueTuple`3" />.
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
    public int CompareTo(ValueTuple<T1, T2, T3> other)
    {
      int num1 = Comparer<T1>.Default.Compare(this.Item1, other.Item1);
      if (num1 != 0)
        return num1;
      int num2 = Comparer<T2>.Default.Compare(this.Item2, other.Item2);
      if (num2 != 0)
        return num2;
      return Comparer<T3>.Default.Compare(this.Item3, other.Item3);
    }

    int IStructuralComparable.CompareTo(object other, IComparer comparer)
    {
      if (other == null)
        return 1;
      if (!(other is ValueTuple<T1, T2, T3>))
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleIncorrectType", (object) this.GetType().ToString()), nameof (other));
      ValueTuple<T1, T2, T3> valueTuple = (ValueTuple<T1, T2, T3>) other;
      int num1 = comparer.Compare((object) this.Item1, (object) valueTuple.Item1);
      if (num1 != 0)
        return num1;
      int num2 = comparer.Compare((object) this.Item2, (object) valueTuple.Item2);
      if (num2 != 0)
        return num2;
      return comparer.Compare((object) this.Item3, (object) valueTuple.Item3);
    }

    /// <summary>
    ///   Вычисляет хэш-код для текущего экземпляра <see cref="T:System.ValueTuple`3" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего экземпляра <see cref="T:System.ValueTuple`3" />.
    /// </returns>
    public override int GetHashCode()
    {
      return ValueTuple.CombineHashCodes(EqualityComparer<T1>.Default.GetHashCode(this.Item1), EqualityComparer<T2>.Default.GetHashCode(this.Item2), EqualityComparer<T3>.Default.GetHashCode(this.Item3));
    }

    int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
    {
      return this.GetHashCodeCore(comparer);
    }

    private int GetHashCodeCore(IEqualityComparer comparer)
    {
      return ValueTuple.CombineHashCodes(comparer.GetHashCode((object) this.Item1), comparer.GetHashCode((object) this.Item2), comparer.GetHashCode((object) this.Item3));
    }

    int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
    {
      return this.GetHashCodeCore(comparer);
    }

    /// <summary>
    ///   Возвращает строковое представление значения этого экземпляра <see cref="T:System.ValueTuple`3" />.
    /// </summary>
    /// <returns>
    ///   Строковое представление этого экземпляра <see cref="T:System.ValueTuple`2" />.
    /// </returns>
    public override string ToString()
    {
      string[] strArray = new string[7];
      strArray[0] = "(";
      int index1 = 1;
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
      strArray[index1] = str1;
      strArray[2] = ", ";
      int index2 = 3;
      ref T2 local3 = ref this.Item2;
      string str2;
      if ((object) default (T2) == null)
      {
        T2 obj = local3;
        ref T2 local2 = ref obj;
        if ((object) obj == null)
        {
          str2 = (string) null;
          goto label_8;
        }
        else
          local3 = ref local2;
      }
      str2 = local3.ToString();
label_8:
      strArray[index2] = str2;
      strArray[4] = ", ";
      int index3 = 5;
      ref T3 local4 = ref this.Item3;
      string str3;
      if ((object) default (T3) == null)
      {
        T3 obj = local4;
        ref T3 local2 = ref obj;
        if ((object) obj == null)
        {
          str3 = (string) null;
          goto label_12;
        }
        else
          local4 = ref local2;
      }
      str3 = local4.ToString();
label_12:
      strArray[index3] = str3;
      strArray[6] = ")";
      return string.Concat(strArray);
    }

    string IValueTupleInternal.ToStringEnd()
    {
      string[] strArray = new string[6];
      int index1 = 0;
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
      strArray[index1] = str1;
      strArray[1] = ", ";
      int index2 = 2;
      ref T2 local3 = ref this.Item2;
      string str2;
      if ((object) default (T2) == null)
      {
        T2 obj = local3;
        ref T2 local2 = ref obj;
        if ((object) obj == null)
        {
          str2 = (string) null;
          goto label_8;
        }
        else
          local3 = ref local2;
      }
      str2 = local3.ToString();
label_8:
      strArray[index2] = str2;
      strArray[3] = ", ";
      int index3 = 4;
      ref T3 local4 = ref this.Item3;
      string str3;
      if ((object) default (T3) == null)
      {
        T3 obj = local4;
        ref T3 local2 = ref obj;
        if ((object) obj == null)
        {
          str3 = (string) null;
          goto label_12;
        }
        else
          local4 = ref local2;
      }
      str3 = local4.ToString();
label_12:
      strArray[index3] = str3;
      strArray[5] = ")";
      return string.Concat(strArray);
    }

    int ITuple.Length
    {
      get
      {
        return 3;
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
          default:
            throw new IndexOutOfRangeException();
        }
      }
    }
  }
}
