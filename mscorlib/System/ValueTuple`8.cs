// Decompiled with JetBrains decompiler
// Type: System.ValueTuple`8
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Представляет кортеж значений из n компонентов, где n больше или равно 8.
  /// </summary>
  /// <typeparam name="T1">
  ///   Тип первого элемента кортежа значений.
  /// </typeparam>
  /// <typeparam name="T2">
  ///   Тип второго элемента кортежа значений.
  /// </typeparam>
  /// <typeparam name="T3">
  ///   Тип третьего элемента кортежа значений.
  /// </typeparam>
  /// <typeparam name="T4">
  ///   Тип четвертого элемента кортежа значений.
  /// </typeparam>
  /// <typeparam name="T5">
  ///   Тип пятого элемента кортежа значений.
  /// </typeparam>
  /// <typeparam name="T6">
  ///   Тип шестого элемента кортежа значений.
  /// </typeparam>
  /// <typeparam name="T7">
  ///   Тип седьмого элемента кортежа значений.
  /// </typeparam>
  /// <typeparam name="TRest">
  ///   Любой универсальный экземпляр кортежа значений, который определяет типы остальных элементов кортежа.
  /// </typeparam>
  [Serializable]
  [StructLayout(LayoutKind.Auto)]
  public struct ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> : IEquatable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>, IValueTupleInternal, ITuple where TRest : struct
  {
    /// <summary>
    ///   Получает значение первого элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </summary>
    /// <returns>
    ///   Значение первого элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </returns>
    public T1 Item1;
    /// <summary>
    ///   Получает значение второго элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </summary>
    /// <returns>
    ///   Значение второго элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </returns>
    public T2 Item2;
    /// <summary>
    ///   Получает значение третьего элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </summary>
    /// <returns>
    ///   Значение третьего элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </returns>
    public T3 Item3;
    /// <summary>
    ///   Получает значение четвертого элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </summary>
    /// <returns>
    ///   Значение четвертого элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </returns>
    public T4 Item4;
    /// <summary>
    ///   Получает значение пятого элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </summary>
    /// <returns>
    ///   Значение пятого элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </returns>
    public T5 Item5;
    /// <summary>
    ///   Получает значение шестого элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </summary>
    /// <returns>
    ///   Значение шестого элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </returns>
    public T6 Item6;
    /// <summary>
    ///   Получает значение седьмого элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </summary>
    /// <returns>
    ///   Значение седьмого элемента текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </returns>
    public T7 Item7;
    /// <summary>
    ///   Получает остальные элементы текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </summary>
    /// <returns>
    ///   Значение остальных элементов текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </returns>
    public TRest Rest;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ValueTuple`8" />.
    /// </summary>
    /// <param name="item1">Первый элемент кортежа значений.</param>
    /// <param name="item2">Второй элемент кортежа значений.</param>
    /// <param name="item3">Третий элемент кортежа значений.</param>
    /// <param name="item4">Четвертый элемент кортежа значений.</param>
    /// <param name="item5">Пятый элемент кортежа значений.</param>
    /// <param name="item6">Шестой элемент кортежа значений.</param>
    /// <param name="item7">Седьмой элемент кортежа значений.</param>
    /// <param name="rest">
    ///   Экземпляр любого типа кортежа значений, который содержит значения остальных элементов кортежа значений.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="rest" /> не является общим типом кортежа значений.
    /// </exception>
    public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
    {
      if (!((ValueType) rest is IValueTupleInternal))
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleLastArgumentNotAValueTuple"));
      this.Item1 = item1;
      this.Item2 = item2;
      this.Item3 = item3;
      this.Item4 = item4;
      this.Item5 = item5;
      this.Item6 = item6;
      this.Item7 = item7;
      this.Rest = rest;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли текущий экземпляр <see cref="T:System.ValueTuple`8" /> указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с данным экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий экземпляр равен указанному объекту; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)
        return this.Equals((ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>) obj);
      return false;
    }

    public bool Equals(ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> other)
    {
      if (EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2) && (EqualityComparer<T3>.Default.Equals(this.Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(this.Item4, other.Item4)) && (EqualityComparer<T5>.Default.Equals(this.Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(this.Item6, other.Item6) && EqualityComparer<T7>.Default.Equals(this.Item7, other.Item7)))
        return EqualityComparer<TRest>.Default.Equals(this.Rest, other.Rest);
      return false;
    }

    bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
    {
      if (other == null || !(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
        return false;
      ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> valueTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>) other;
      if (comparer.Equals((object) this.Item1, (object) valueTuple.Item1) && comparer.Equals((object) this.Item2, (object) valueTuple.Item2) && (comparer.Equals((object) this.Item3, (object) valueTuple.Item3) && comparer.Equals((object) this.Item4, (object) valueTuple.Item4)) && (comparer.Equals((object) this.Item5, (object) valueTuple.Item5) && comparer.Equals((object) this.Item6, (object) valueTuple.Item6) && comparer.Equals((object) this.Item7, (object) valueTuple.Item7)))
        return comparer.Equals((object) this.Rest, (object) valueTuple.Rest);
      return false;
    }

    int IComparable.CompareTo(object other)
    {
      if (other == null)
        return 1;
      if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleIncorrectType", (object) this.GetType().ToString()), nameof (other));
      return this.CompareTo((ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>) other);
    }

    public int CompareTo(ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> other)
    {
      int num1 = Comparer<T1>.Default.Compare(this.Item1, other.Item1);
      if (num1 != 0)
        return num1;
      int num2 = Comparer<T2>.Default.Compare(this.Item2, other.Item2);
      if (num2 != 0)
        return num2;
      int num3 = Comparer<T3>.Default.Compare(this.Item3, other.Item3);
      if (num3 != 0)
        return num3;
      int num4 = Comparer<T4>.Default.Compare(this.Item4, other.Item4);
      if (num4 != 0)
        return num4;
      int num5 = Comparer<T5>.Default.Compare(this.Item5, other.Item5);
      if (num5 != 0)
        return num5;
      int num6 = Comparer<T6>.Default.Compare(this.Item6, other.Item6);
      if (num6 != 0)
        return num6;
      int num7 = Comparer<T7>.Default.Compare(this.Item7, other.Item7);
      if (num7 != 0)
        return num7;
      return Comparer<TRest>.Default.Compare(this.Rest, other.Rest);
    }

    int IStructuralComparable.CompareTo(object other, IComparer comparer)
    {
      if (other == null)
        return 1;
      if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_ValueTupleIncorrectType", (object) this.GetType().ToString()), nameof (other));
      ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> valueTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>) other;
      int num1 = comparer.Compare((object) this.Item1, (object) valueTuple.Item1);
      if (num1 != 0)
        return num1;
      int num2 = comparer.Compare((object) this.Item2, (object) valueTuple.Item2);
      if (num2 != 0)
        return num2;
      int num3 = comparer.Compare((object) this.Item3, (object) valueTuple.Item3);
      if (num3 != 0)
        return num3;
      int num4 = comparer.Compare((object) this.Item4, (object) valueTuple.Item4);
      if (num4 != 0)
        return num4;
      int num5 = comparer.Compare((object) this.Item5, (object) valueTuple.Item5);
      if (num5 != 0)
        return num5;
      int num6 = comparer.Compare((object) this.Item6, (object) valueTuple.Item6);
      if (num6 != 0)
        return num6;
      int num7 = comparer.Compare((object) this.Item7, (object) valueTuple.Item7);
      if (num7 != 0)
        return num7;
      return comparer.Compare((object) this.Rest, (object) valueTuple.Rest);
    }

    /// <summary>
    ///   Вычисляет хэш-код для текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </returns>
    public override int GetHashCode()
    {
      IValueTupleInternal rest = (ValueType) this.Rest as IValueTupleInternal;
      if (rest == null)
        return ValueTuple.CombineHashCodes(EqualityComparer<T1>.Default.GetHashCode(this.Item1), EqualityComparer<T2>.Default.GetHashCode(this.Item2), EqualityComparer<T3>.Default.GetHashCode(this.Item3), EqualityComparer<T4>.Default.GetHashCode(this.Item4), EqualityComparer<T5>.Default.GetHashCode(this.Item5), EqualityComparer<T6>.Default.GetHashCode(this.Item6), EqualityComparer<T7>.Default.GetHashCode(this.Item7));
      int length = rest.Length;
      if (length >= 8)
        return rest.GetHashCode();
      switch (8 - length)
      {
        case 1:
          return ValueTuple.CombineHashCodes(EqualityComparer<T7>.Default.GetHashCode(this.Item7), rest.GetHashCode());
        case 2:
          return ValueTuple.CombineHashCodes(EqualityComparer<T6>.Default.GetHashCode(this.Item6), EqualityComparer<T7>.Default.GetHashCode(this.Item7), rest.GetHashCode());
        case 3:
          return ValueTuple.CombineHashCodes(EqualityComparer<T5>.Default.GetHashCode(this.Item5), EqualityComparer<T6>.Default.GetHashCode(this.Item6), EqualityComparer<T7>.Default.GetHashCode(this.Item7), rest.GetHashCode());
        case 4:
          return ValueTuple.CombineHashCodes(EqualityComparer<T4>.Default.GetHashCode(this.Item4), EqualityComparer<T5>.Default.GetHashCode(this.Item5), EqualityComparer<T6>.Default.GetHashCode(this.Item6), EqualityComparer<T7>.Default.GetHashCode(this.Item7), rest.GetHashCode());
        case 5:
          return ValueTuple.CombineHashCodes(EqualityComparer<T3>.Default.GetHashCode(this.Item3), EqualityComparer<T4>.Default.GetHashCode(this.Item4), EqualityComparer<T5>.Default.GetHashCode(this.Item5), EqualityComparer<T6>.Default.GetHashCode(this.Item6), EqualityComparer<T7>.Default.GetHashCode(this.Item7), rest.GetHashCode());
        case 6:
          return ValueTuple.CombineHashCodes(EqualityComparer<T2>.Default.GetHashCode(this.Item2), EqualityComparer<T3>.Default.GetHashCode(this.Item3), EqualityComparer<T4>.Default.GetHashCode(this.Item4), EqualityComparer<T5>.Default.GetHashCode(this.Item5), EqualityComparer<T6>.Default.GetHashCode(this.Item6), EqualityComparer<T7>.Default.GetHashCode(this.Item7), rest.GetHashCode());
        case 7:
        case 8:
          return ValueTuple.CombineHashCodes(EqualityComparer<T1>.Default.GetHashCode(this.Item1), EqualityComparer<T2>.Default.GetHashCode(this.Item2), EqualityComparer<T3>.Default.GetHashCode(this.Item3), EqualityComparer<T4>.Default.GetHashCode(this.Item4), EqualityComparer<T5>.Default.GetHashCode(this.Item5), EqualityComparer<T6>.Default.GetHashCode(this.Item6), EqualityComparer<T7>.Default.GetHashCode(this.Item7), rest.GetHashCode());
        default:
          return -1;
      }
    }

    int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
    {
      return this.GetHashCodeCore(comparer);
    }

    private int GetHashCodeCore(IEqualityComparer comparer)
    {
      IValueTupleInternal rest = (ValueType) this.Rest as IValueTupleInternal;
      if (rest == null)
        return ValueTuple.CombineHashCodes(comparer.GetHashCode((object) this.Item1), comparer.GetHashCode((object) this.Item2), comparer.GetHashCode((object) this.Item3), comparer.GetHashCode((object) this.Item4), comparer.GetHashCode((object) this.Item5), comparer.GetHashCode((object) this.Item6), comparer.GetHashCode((object) this.Item7));
      int length = rest.Length;
      if (length >= 8)
        return rest.GetHashCode(comparer);
      switch (8 - length)
      {
        case 1:
          return ValueTuple.CombineHashCodes(comparer.GetHashCode((object) this.Item7), rest.GetHashCode(comparer));
        case 2:
          return ValueTuple.CombineHashCodes(comparer.GetHashCode((object) this.Item6), comparer.GetHashCode((object) this.Item7), rest.GetHashCode(comparer));
        case 3:
          return ValueTuple.CombineHashCodes(comparer.GetHashCode((object) this.Item5), comparer.GetHashCode((object) this.Item6), comparer.GetHashCode((object) this.Item7), rest.GetHashCode(comparer));
        case 4:
          return ValueTuple.CombineHashCodes(comparer.GetHashCode((object) this.Item4), comparer.GetHashCode((object) this.Item5), comparer.GetHashCode((object) this.Item6), comparer.GetHashCode((object) this.Item7), rest.GetHashCode(comparer));
        case 5:
          return ValueTuple.CombineHashCodes(comparer.GetHashCode((object) this.Item3), comparer.GetHashCode((object) this.Item4), comparer.GetHashCode((object) this.Item5), comparer.GetHashCode((object) this.Item6), comparer.GetHashCode((object) this.Item7), rest.GetHashCode(comparer));
        case 6:
          return ValueTuple.CombineHashCodes(comparer.GetHashCode((object) this.Item2), comparer.GetHashCode((object) this.Item3), comparer.GetHashCode((object) this.Item4), comparer.GetHashCode((object) this.Item5), comparer.GetHashCode((object) this.Item6), comparer.GetHashCode((object) this.Item7), rest.GetHashCode(comparer));
        case 7:
        case 8:
          return ValueTuple.CombineHashCodes(comparer.GetHashCode((object) this.Item1), comparer.GetHashCode((object) this.Item2), comparer.GetHashCode((object) this.Item3), comparer.GetHashCode((object) this.Item4), comparer.GetHashCode((object) this.Item5), comparer.GetHashCode((object) this.Item6), comparer.GetHashCode((object) this.Item7), rest.GetHashCode(comparer));
        default:
          return -1;
      }
    }

    int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
    {
      return this.GetHashCodeCore(comparer);
    }

    /// <summary>
    ///   Возвращает строковое представление значения этого экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </summary>
    /// <returns>
    ///   Строковое представление этого экземпляра <see cref="T:System.ValueTuple`8" />.
    /// </returns>
    public override string ToString()
    {
      IValueTupleInternal rest = (ValueType) this.Rest as IValueTupleInternal;
      if (rest == null)
      {
        string[] strArray = new string[17];
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
            goto label_5;
          }
          else
            local1 = ref local2;
        }
        str1 = local1.ToString();
label_5:
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
            goto label_9;
          }
          else
            local3 = ref local2;
        }
        str2 = local3.ToString();
label_9:
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
            goto label_13;
          }
          else
            local4 = ref local2;
        }
        str3 = local4.ToString();
label_13:
        strArray[index3] = str3;
        strArray[6] = ", ";
        int index4 = 7;
        ref T4 local5 = ref this.Item4;
        string str4;
        if ((object) default (T4) == null)
        {
          T4 obj = local5;
          ref T4 local2 = ref obj;
          if ((object) obj == null)
          {
            str4 = (string) null;
            goto label_17;
          }
          else
            local5 = ref local2;
        }
        str4 = local5.ToString();
label_17:
        strArray[index4] = str4;
        strArray[8] = ", ";
        int index5 = 9;
        ref T5 local6 = ref this.Item5;
        string str5;
        if ((object) default (T5) == null)
        {
          T5 obj = local6;
          ref T5 local2 = ref obj;
          if ((object) obj == null)
          {
            str5 = (string) null;
            goto label_21;
          }
          else
            local6 = ref local2;
        }
        str5 = local6.ToString();
label_21:
        strArray[index5] = str5;
        strArray[10] = ", ";
        int index6 = 11;
        ref T6 local7 = ref this.Item6;
        string str6;
        if ((object) default (T6) == null)
        {
          T6 obj = local7;
          ref T6 local2 = ref obj;
          if ((object) obj == null)
          {
            str6 = (string) null;
            goto label_25;
          }
          else
            local7 = ref local2;
        }
        str6 = local7.ToString();
label_25:
        strArray[index6] = str6;
        strArray[12] = ", ";
        int index7 = 13;
        ref T7 local8 = ref this.Item7;
        string str7;
        if ((object) default (T7) == null)
        {
          T7 obj = local8;
          ref T7 local2 = ref obj;
          if ((object) obj == null)
          {
            str7 = (string) null;
            goto label_29;
          }
          else
            local8 = ref local2;
        }
        str7 = local8.ToString();
label_29:
        strArray[index7] = str7;
        strArray[14] = ", ";
        strArray[15] = this.Rest.ToString();
        strArray[16] = ")";
        return string.Concat(strArray);
      }
      string[] strArray1 = new string[16];
      strArray1[0] = "(";
      int index8 = 1;
      ref T1 local9 = ref this.Item1;
      string str8;
      if ((object) default (T1) == null)
      {
        T1 obj = local9;
        ref T1 local1 = ref obj;
        if ((object) obj == null)
        {
          str8 = (string) null;
          goto label_34;
        }
        else
          local9 = ref local1;
      }
      str8 = local9.ToString();
label_34:
      strArray1[index8] = str8;
      strArray1[2] = ", ";
      int index9 = 3;
      ref T2 local10 = ref this.Item2;
      string str9;
      if ((object) default (T2) == null)
      {
        T2 obj = local10;
        ref T2 local1 = ref obj;
        if ((object) obj == null)
        {
          str9 = (string) null;
          goto label_38;
        }
        else
          local10 = ref local1;
      }
      str9 = local10.ToString();
label_38:
      strArray1[index9] = str9;
      strArray1[4] = ", ";
      int index10 = 5;
      ref T3 local11 = ref this.Item3;
      string str10;
      if ((object) default (T3) == null)
      {
        T3 obj = local11;
        ref T3 local1 = ref obj;
        if ((object) obj == null)
        {
          str10 = (string) null;
          goto label_42;
        }
        else
          local11 = ref local1;
      }
      str10 = local11.ToString();
label_42:
      strArray1[index10] = str10;
      strArray1[6] = ", ";
      int index11 = 7;
      ref T4 local12 = ref this.Item4;
      string str11;
      if ((object) default (T4) == null)
      {
        T4 obj = local12;
        ref T4 local1 = ref obj;
        if ((object) obj == null)
        {
          str11 = (string) null;
          goto label_46;
        }
        else
          local12 = ref local1;
      }
      str11 = local12.ToString();
label_46:
      strArray1[index11] = str11;
      strArray1[8] = ", ";
      int index12 = 9;
      ref T5 local13 = ref this.Item5;
      string str12;
      if ((object) default (T5) == null)
      {
        T5 obj = local13;
        ref T5 local1 = ref obj;
        if ((object) obj == null)
        {
          str12 = (string) null;
          goto label_50;
        }
        else
          local13 = ref local1;
      }
      str12 = local13.ToString();
label_50:
      strArray1[index12] = str12;
      strArray1[10] = ", ";
      int index13 = 11;
      ref T6 local14 = ref this.Item6;
      string str13;
      if ((object) default (T6) == null)
      {
        T6 obj = local14;
        ref T6 local1 = ref obj;
        if ((object) obj == null)
        {
          str13 = (string) null;
          goto label_54;
        }
        else
          local14 = ref local1;
      }
      str13 = local14.ToString();
label_54:
      strArray1[index13] = str13;
      strArray1[12] = ", ";
      int index14 = 13;
      ref T7 local15 = ref this.Item7;
      string str14;
      if ((object) default (T7) == null)
      {
        T7 obj = local15;
        ref T7 local1 = ref obj;
        if ((object) obj == null)
        {
          str14 = (string) null;
          goto label_58;
        }
        else
          local15 = ref local1;
      }
      str14 = local15.ToString();
label_58:
      strArray1[index14] = str14;
      strArray1[14] = ", ";
      strArray1[15] = rest.ToStringEnd();
      return string.Concat(strArray1);
    }

    string IValueTupleInternal.ToStringEnd()
    {
      IValueTupleInternal rest = (ValueType) this.Rest as IValueTupleInternal;
      if (rest == null)
      {
        string[] strArray = new string[16];
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
            goto label_5;
          }
          else
            local1 = ref local2;
        }
        str1 = local1.ToString();
label_5:
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
            goto label_9;
          }
          else
            local3 = ref local2;
        }
        str2 = local3.ToString();
label_9:
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
            goto label_13;
          }
          else
            local4 = ref local2;
        }
        str3 = local4.ToString();
label_13:
        strArray[index3] = str3;
        strArray[5] = ", ";
        int index4 = 6;
        ref T4 local5 = ref this.Item4;
        string str4;
        if ((object) default (T4) == null)
        {
          T4 obj = local5;
          ref T4 local2 = ref obj;
          if ((object) obj == null)
          {
            str4 = (string) null;
            goto label_17;
          }
          else
            local5 = ref local2;
        }
        str4 = local5.ToString();
label_17:
        strArray[index4] = str4;
        strArray[7] = ", ";
        int index5 = 8;
        ref T5 local6 = ref this.Item5;
        string str5;
        if ((object) default (T5) == null)
        {
          T5 obj = local6;
          ref T5 local2 = ref obj;
          if ((object) obj == null)
          {
            str5 = (string) null;
            goto label_21;
          }
          else
            local6 = ref local2;
        }
        str5 = local6.ToString();
label_21:
        strArray[index5] = str5;
        strArray[9] = ", ";
        int index6 = 10;
        ref T6 local7 = ref this.Item6;
        string str6;
        if ((object) default (T6) == null)
        {
          T6 obj = local7;
          ref T6 local2 = ref obj;
          if ((object) obj == null)
          {
            str6 = (string) null;
            goto label_25;
          }
          else
            local7 = ref local2;
        }
        str6 = local7.ToString();
label_25:
        strArray[index6] = str6;
        strArray[11] = ", ";
        int index7 = 12;
        ref T7 local8 = ref this.Item7;
        string str7;
        if ((object) default (T7) == null)
        {
          T7 obj = local8;
          ref T7 local2 = ref obj;
          if ((object) obj == null)
          {
            str7 = (string) null;
            goto label_29;
          }
          else
            local8 = ref local2;
        }
        str7 = local8.ToString();
label_29:
        strArray[index7] = str7;
        strArray[13] = ", ";
        strArray[14] = this.Rest.ToString();
        strArray[15] = ")";
        return string.Concat(strArray);
      }
      string[] strArray1 = new string[15];
      int index8 = 0;
      ref T1 local9 = ref this.Item1;
      string str8;
      if ((object) default (T1) == null)
      {
        T1 obj = local9;
        ref T1 local1 = ref obj;
        if ((object) obj == null)
        {
          str8 = (string) null;
          goto label_34;
        }
        else
          local9 = ref local1;
      }
      str8 = local9.ToString();
label_34:
      strArray1[index8] = str8;
      strArray1[1] = ", ";
      int index9 = 2;
      ref T2 local10 = ref this.Item2;
      string str9;
      if ((object) default (T2) == null)
      {
        T2 obj = local10;
        ref T2 local1 = ref obj;
        if ((object) obj == null)
        {
          str9 = (string) null;
          goto label_38;
        }
        else
          local10 = ref local1;
      }
      str9 = local10.ToString();
label_38:
      strArray1[index9] = str9;
      strArray1[3] = ", ";
      int index10 = 4;
      ref T3 local11 = ref this.Item3;
      string str10;
      if ((object) default (T3) == null)
      {
        T3 obj = local11;
        ref T3 local1 = ref obj;
        if ((object) obj == null)
        {
          str10 = (string) null;
          goto label_42;
        }
        else
          local11 = ref local1;
      }
      str10 = local11.ToString();
label_42:
      strArray1[index10] = str10;
      strArray1[5] = ", ";
      int index11 = 6;
      ref T4 local12 = ref this.Item4;
      string str11;
      if ((object) default (T4) == null)
      {
        T4 obj = local12;
        ref T4 local1 = ref obj;
        if ((object) obj == null)
        {
          str11 = (string) null;
          goto label_46;
        }
        else
          local12 = ref local1;
      }
      str11 = local12.ToString();
label_46:
      strArray1[index11] = str11;
      strArray1[7] = ", ";
      int index12 = 8;
      ref T5 local13 = ref this.Item5;
      string str12;
      if ((object) default (T5) == null)
      {
        T5 obj = local13;
        ref T5 local1 = ref obj;
        if ((object) obj == null)
        {
          str12 = (string) null;
          goto label_50;
        }
        else
          local13 = ref local1;
      }
      str12 = local13.ToString();
label_50:
      strArray1[index12] = str12;
      strArray1[9] = ", ";
      int index13 = 10;
      ref T6 local14 = ref this.Item6;
      string str13;
      if ((object) default (T6) == null)
      {
        T6 obj = local14;
        ref T6 local1 = ref obj;
        if ((object) obj == null)
        {
          str13 = (string) null;
          goto label_54;
        }
        else
          local14 = ref local1;
      }
      str13 = local14.ToString();
label_54:
      strArray1[index13] = str13;
      strArray1[11] = ", ";
      int index14 = 12;
      ref T7 local15 = ref this.Item7;
      string str14;
      if ((object) default (T7) == null)
      {
        T7 obj = local15;
        ref T7 local1 = ref obj;
        if ((object) obj == null)
        {
          str14 = (string) null;
          goto label_58;
        }
        else
          local15 = ref local1;
      }
      str14 = local15.ToString();
label_58:
      strArray1[index14] = str14;
      strArray1[13] = ", ";
      strArray1[14] = rest.ToStringEnd();
      return string.Concat(strArray1);
    }

    int ITuple.Length
    {
      get
      {
        IValueTupleInternal rest = (ValueType) this.Rest as IValueTupleInternal;
        if (rest != null)
          return 7 + rest.Length;
        return 8;
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
            IValueTupleInternal rest = (ValueType) this.Rest as IValueTupleInternal;
            if (rest != null)
              return rest[index - 7];
            if (index == 7)
              return (object) this.Rest;
            throw new IndexOutOfRangeException();
        }
      }
    }
  }
}
