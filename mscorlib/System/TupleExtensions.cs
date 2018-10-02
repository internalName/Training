// Decompiled with JetBrains decompiler
// Type: System.TupleExtensions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System
{
  /// <summary>
  ///   Предоставляет методы расширения для кортежей для взаимодействия с языковой поддержкой для кортежей в C#.
  /// </summary>
  public static class TupleExtensions
  {
    /// <summary>
    ///   Разбивает кортеж с 1 элементом на отдельную переменную.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 1 элемента, разбиваемый на отдельную переменную.
    /// </param>
    /// <param name="item1">Значение единственного элемента.</param>
    /// <typeparam name="T1">Тип единственного элемента.</typeparam>
    public static void Deconstruct<T1>(this Tuple<T1> value, out T1 item1)
    {
      item1 = value.Item1;
    }

    /// <summary>
    ///   Разбивает кортеж с 2 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 2 элементов, разбиваемый на 2 отдельные переменные.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    public static void Deconstruct<T1, T2>(this Tuple<T1, T2> value, out T1 item1, out T2 item2)
    {
      item1 = value.Item1;
      item2 = value.Item2;
    }

    /// <summary>
    ///   Разбивает кортеж с 3 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 3 элементов, разбиваемый на 3 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3>(this Tuple<T1, T2, T3> value, out T1 item1, out T2 item2, out T3 item3)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
    }

    /// <summary>
    ///   Разбивает кортеж с 4 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 4 элементов, разбиваемый на 4 отдельные переменные.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
    }

    /// <summary>
    ///   Разбивает кортеж с 5 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 5 элементов, разбиваемый на 5 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3, T4, T5> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
    }

    /// <summary>
    ///   Разбивает кортеж с 6 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 6 элементов, разбиваемый на 6 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3, T4, T5, T6> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
    }

    /// <summary>
    ///   Разбивает кортеж с 7 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 7 элементов, разбиваемый на 7 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4, T5, T6, T7> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
    }

    /// <summary>
    ///   Разбивает кортеж с 8 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 8 элементов, разбиваемый на 8 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
    }

    /// <summary>
    ///   Разбивает кортеж с 9 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 9 элементов, разбиваемый на 9 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
    }

    /// <summary>
    ///   Разбивает кортеж с 10 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 10 элементов, разбиваемый на 10 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <param name="item10">
    ///   Значение десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    /// <typeparam name="T10">Тип десятого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
      item10 = value.Rest.Item3;
    }

    /// <summary>
    ///   Разбивает кортеж с 11 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 11 элементов, разбиваемый на 11 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <param name="item10">
    ///   Значение десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </param>
    /// <param name="item11">
    ///   Значение одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    /// <typeparam name="T10">Тип десятого элемента.</typeparam>
    /// <typeparam name="T11">Тип одиннадцатого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
      item10 = value.Rest.Item3;
      item11 = value.Rest.Item4;
    }

    /// <summary>
    ///   Разбивает кортеж с 12 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 12 элементов, разбиваемый на 12 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <param name="item10">
    ///   Значение десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </param>
    /// <param name="item11">
    ///   Значение одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </param>
    /// <param name="item12">
    ///   Значение двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    /// <typeparam name="T10">Тип десятого элемента.</typeparam>
    /// <typeparam name="T11">Тип одиннадцатого элемента.</typeparam>
    /// <typeparam name="T12">Тип двенадцатого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
      item10 = value.Rest.Item3;
      item11 = value.Rest.Item4;
      item12 = value.Rest.Item5;
    }

    /// <summary>
    ///   Разбивает кортеж с 13 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 13 элементов, разбиваемый на 13 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <param name="item10">
    ///   Значение десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </param>
    /// <param name="item11">
    ///   Значение одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </param>
    /// <param name="item12">
    ///   Значение двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </param>
    /// <param name="item13">
    ///   Значение тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    /// <typeparam name="T10">Тип десятого элемента.</typeparam>
    /// <typeparam name="T11">Тип одиннадцатого элемента.</typeparam>
    /// <typeparam name="T12">Тип двенадцатого элемента.</typeparam>
    /// <typeparam name="T13">Тип тринадцатого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
      item10 = value.Rest.Item3;
      item11 = value.Rest.Item4;
      item12 = value.Rest.Item5;
      item13 = value.Rest.Item6;
    }

    /// <summary>
    ///   Разбивает кортеж с 14 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 14 элементов, разбиваемый на 14 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <param name="item10">
    ///   Значение десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </param>
    /// <param name="item11">
    ///   Значение одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </param>
    /// <param name="item12">
    ///   Значение двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </param>
    /// <param name="item13">
    ///   Значение тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </param>
    /// <param name="item14">
    ///   Значение четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    /// <typeparam name="T10">Тип десятого элемента.</typeparam>
    /// <typeparam name="T11">Тип одиннадцатого элемента.</typeparam>
    /// <typeparam name="T12">Тип двенадцатого элемента.</typeparam>
    /// <typeparam name="T13">Тип тринадцатого элемента.</typeparam>
    /// <typeparam name="T14">Тип четырнадцатого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
      item10 = value.Rest.Item3;
      item11 = value.Rest.Item4;
      item12 = value.Rest.Item5;
      item13 = value.Rest.Item6;
      item14 = value.Rest.Item7;
    }

    /// <summary>
    ///   Разбивает кортеж с 15 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 15 элементов, разбиваемый на 15 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <param name="item10">
    ///   Значение десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </param>
    /// <param name="item11">
    ///   Значение одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </param>
    /// <param name="item12">
    ///   Значение двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </param>
    /// <param name="item13">
    ///   Значение тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </param>
    /// <param name="item14">
    ///   Значение четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </param>
    /// <param name="item15">
    ///   Значение пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1 " />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    /// <typeparam name="T10">Тип десятого элемента.</typeparam>
    /// <typeparam name="T11">Тип одиннадцатого элемента.</typeparam>
    /// <typeparam name="T12">Тип двенадцатого элемента.</typeparam>
    /// <typeparam name="T13">Тип тринадцатого элемента.</typeparam>
    /// <typeparam name="T14">Тип четырнадцатого элемента.</typeparam>
    /// <typeparam name="T15">Тип пятнадцатого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
      item10 = value.Rest.Item3;
      item11 = value.Rest.Item4;
      item12 = value.Rest.Item5;
      item13 = value.Rest.Item6;
      item14 = value.Rest.Item7;
      item15 = value.Rest.Rest.Item1;
    }

    /// <summary>
    ///   Разбивает кортеж с 16 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 16 элементов, разбиваемый на 16 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <param name="item10">
    ///   Значение десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </param>
    /// <param name="item11">
    ///   Значение одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </param>
    /// <param name="item12">
    ///   Значение двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </param>
    /// <param name="item13">
    ///   Значение тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </param>
    /// <param name="item14">
    ///   Значение четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </param>
    /// <param name="item15">
    ///   Значение пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1 " />.
    /// </param>
    /// <param name="item16">
    ///   Значение шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    /// <typeparam name="T10">Тип десятого элемента.</typeparam>
    /// <typeparam name="T11">Тип одиннадцатого элемента.</typeparam>
    /// <typeparam name="T12">Тип двенадцатого элемента.</typeparam>
    /// <typeparam name="T13">Тип тринадцатого элемента.</typeparam>
    /// <typeparam name="T14">Тип четырнадцатого элемента.</typeparam>
    /// <typeparam name="T15">Тип пятнадцатого элемента.</typeparam>
    /// <typeparam name="T16">Тип шестнадцатого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
      item10 = value.Rest.Item3;
      item11 = value.Rest.Item4;
      item12 = value.Rest.Item5;
      item13 = value.Rest.Item6;
      item14 = value.Rest.Item7;
      item15 = value.Rest.Rest.Item1;
      item16 = value.Rest.Rest.Item2;
    }

    /// <summary>
    ///   Разбивает кортеж с 17 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 17 элементов, разбиваемый на 17 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <param name="item10">
    ///   Значение десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </param>
    /// <param name="item11">
    ///   Значение одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </param>
    /// <param name="item12">
    ///   Значение двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </param>
    /// <param name="item13">
    ///   Значение тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </param>
    /// <param name="item14">
    ///   Значение четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </param>
    /// <param name="item15">
    ///   Значение пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1 " />.
    /// </param>
    /// <param name="item16">
    ///   Значение шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </param>
    /// <param name="item17">
    ///   Значение семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    /// <typeparam name="T10">Тип десятого элемента.</typeparam>
    /// <typeparam name="T11">Тип одиннадцатого элемента.</typeparam>
    /// <typeparam name="T12">Тип двенадцатого элемента.</typeparam>
    /// <typeparam name="T13">Тип тринадцатого элемента.</typeparam>
    /// <typeparam name="T14">Тип четырнадцатого элемента.</typeparam>
    /// <typeparam name="T15">Тип пятнадцатого элемента.</typeparam>
    /// <typeparam name="T16">Тип шестнадцатого элемента.</typeparam>
    /// <typeparam name="T17">Тип семнадцатого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
      item10 = value.Rest.Item3;
      item11 = value.Rest.Item4;
      item12 = value.Rest.Item5;
      item13 = value.Rest.Item6;
      item14 = value.Rest.Item7;
      item15 = value.Rest.Rest.Item1;
      item16 = value.Rest.Rest.Item2;
      item17 = value.Rest.Rest.Item3;
    }

    /// <summary>
    ///   Разбивает кортеж с 18 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 18 элементов, разбиваемый на 18 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <param name="item10">
    ///   Значение десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </param>
    /// <param name="item11">
    ///   Значение одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </param>
    /// <param name="item12">
    ///   Значение двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </param>
    /// <param name="item13">
    ///   Значение тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </param>
    /// <param name="item14">
    ///   Значение четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </param>
    /// <param name="item15">
    ///   Значение пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1 " />.
    /// </param>
    /// <param name="item16">
    ///   Значение шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </param>
    /// <param name="item17">
    ///   Значение семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </param>
    /// <param name="item18">
    ///   Значение восемнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item4" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    /// <typeparam name="T10">Тип десятого элемента.</typeparam>
    /// <typeparam name="T11">Тип одиннадцатого элемента.</typeparam>
    /// <typeparam name="T12">Тип двенадцатого элемента.</typeparam>
    /// <typeparam name="T13">Тип тринадцатого элемента.</typeparam>
    /// <typeparam name="T14">Тип четырнадцатого элемента.</typeparam>
    /// <typeparam name="T15">Тип пятнадцатого элемента.</typeparam>
    /// <typeparam name="T16">Тип шестнадцатого элемента.</typeparam>
    /// <typeparam name="T17">Тип семнадцатого элемента.</typeparam>
    /// <typeparam name="T18">Тип восемнадцатого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
      item10 = value.Rest.Item3;
      item11 = value.Rest.Item4;
      item12 = value.Rest.Item5;
      item13 = value.Rest.Item6;
      item14 = value.Rest.Item7;
      item15 = value.Rest.Rest.Item1;
      item16 = value.Rest.Rest.Item2;
      item17 = value.Rest.Rest.Item3;
      item18 = value.Rest.Rest.Item4;
    }

    /// <summary>
    ///   Разбивает кортеж с 19 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 19 элементов, разбиваемый на 19 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <param name="item10">
    ///   Значение десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </param>
    /// <param name="item11">
    ///   Значение одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </param>
    /// <param name="item12">
    ///   Значение двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </param>
    /// <param name="item13">
    ///   Значение тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </param>
    /// <param name="item14">
    ///   Значение четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </param>
    /// <param name="item15">
    ///   Значение пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1 " />.
    /// </param>
    /// <param name="item16">
    ///   Значение шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </param>
    /// <param name="item17">
    ///   Значение семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </param>
    /// <param name="item18">
    ///   Значение восемнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item4" />.
    /// </param>
    /// <param name="item19">
    ///   Значение девятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item5" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    /// <typeparam name="T10">Тип десятого элемента.</typeparam>
    /// <typeparam name="T11">Тип одиннадцатого элемента.</typeparam>
    /// <typeparam name="T12">Тип двенадцатого элемента.</typeparam>
    /// <typeparam name="T13">Тип тринадцатого элемента.</typeparam>
    /// <typeparam name="T14">Тип четырнадцатого элемента.</typeparam>
    /// <typeparam name="T15">Тип пятнадцатого элемента.</typeparam>
    /// <typeparam name="T16">Тип шестнадцатого элемента.</typeparam>
    /// <typeparam name="T17">Тип семнадцатого элемента.</typeparam>
    /// <typeparam name="T18">Тип восемнадцатого элемента.</typeparam>
    /// <typeparam name="T19">Тип девятнадцатого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
      item10 = value.Rest.Item3;
      item11 = value.Rest.Item4;
      item12 = value.Rest.Item5;
      item13 = value.Rest.Item6;
      item14 = value.Rest.Item7;
      item15 = value.Rest.Rest.Item1;
      item16 = value.Rest.Rest.Item2;
      item17 = value.Rest.Rest.Item3;
      item18 = value.Rest.Rest.Item4;
      item19 = value.Rest.Rest.Item5;
    }

    /// <summary>
    ///   Разбивает кортеж с 20 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 20 элементов, разбиваемый на 20 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <param name="item10">
    ///   Значение десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </param>
    /// <param name="item11">
    ///   Значение одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </param>
    /// <param name="item12">
    ///   Значение двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </param>
    /// <param name="item13">
    ///   Значение тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </param>
    /// <param name="item14">
    ///   Значение четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </param>
    /// <param name="item15">
    ///   Значение пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1 " />.
    /// </param>
    /// <param name="item16">
    ///   Значение шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </param>
    /// <param name="item17">
    ///   Значение семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </param>
    /// <param name="item18">
    ///   Значение восемнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item4" />.
    /// </param>
    /// <param name="item19">
    ///   Значение девятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item5" />.
    /// </param>
    /// <param name="item20">
    ///   Значение двадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item6" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    /// <typeparam name="T10">Тип десятого элемента.</typeparam>
    /// <typeparam name="T11">Тип одиннадцатого элемента.</typeparam>
    /// <typeparam name="T12">Тип двенадцатого элемента.</typeparam>
    /// <typeparam name="T13">Тип тринадцатого элемента.</typeparam>
    /// <typeparam name="T14">Тип четырнадцатого элемента.</typeparam>
    /// <typeparam name="T15">Тип пятнадцатого элемента.</typeparam>
    /// <typeparam name="T16">Тип шестнадцатого элемента.</typeparam>
    /// <typeparam name="T17">Тип семнадцатого элемента.</typeparam>
    /// <typeparam name="T18">Тип восемнадцатого элемента.</typeparam>
    /// <typeparam name="T19">Тип девятнадцатого элемента.</typeparam>
    /// <typeparam name="T20">Тип двадцатого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19, out T20 item20)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
      item10 = value.Rest.Item3;
      item11 = value.Rest.Item4;
      item12 = value.Rest.Item5;
      item13 = value.Rest.Item6;
      item14 = value.Rest.Item7;
      item15 = value.Rest.Rest.Item1;
      item16 = value.Rest.Rest.Item2;
      item17 = value.Rest.Rest.Item3;
      item18 = value.Rest.Rest.Item4;
      item19 = value.Rest.Rest.Item5;
      item20 = value.Rest.Rest.Item6;
    }

    /// <summary>
    ///   Разбивает кортеж с 21 элементами на отдельные переменные.
    /// </summary>
    /// <param name="value">
    ///   Кортеж из 21 элементов, разбиваемый на 21 отдельных переменных.
    /// </param>
    /// <param name="item1">Значение первого элемента.</param>
    /// <param name="item2">Значение второго элемента.</param>
    /// <param name="item3">Значение третьего элемента.</param>
    /// <param name="item4">Значение четвертого элемента.</param>
    /// <param name="item5">Значение пятого элемента.</param>
    /// <param name="item6">Значение шестого элемента.</param>
    /// <param name="item7">Значение седьмого элемента.</param>
    /// <param name="item8">
    ///   Значение восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </param>
    /// <param name="item9">
    ///   Значение девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </param>
    /// <param name="item10">
    ///   Значение десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </param>
    /// <param name="item11">
    ///   Значение одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </param>
    /// <param name="item12">
    ///   Значение двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </param>
    /// <param name="item13">
    ///   Значение тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </param>
    /// <param name="item14">
    ///   Значение четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </param>
    /// <param name="item15">
    ///   Значение пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1 " />.
    /// </param>
    /// <param name="item16">
    ///   Значение шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </param>
    /// <param name="item17">
    ///   Значение семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </param>
    /// <param name="item18">
    ///   Значение восемнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item4" />.
    /// </param>
    /// <param name="item19">
    ///   Значение девятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item5" />.
    /// </param>
    /// <param name="item20">
    ///   Значение двадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item6" />.
    /// </param>
    /// <param name="item21">
    ///   Значение двадцать первого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item7" />.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">Тип восьмого элемента.</typeparam>
    /// <typeparam name="T9">Тип девятого элемента.</typeparam>
    /// <typeparam name="T10">Тип десятого элемента.</typeparam>
    /// <typeparam name="T11">Тип одиннадцатого элемента.</typeparam>
    /// <typeparam name="T12">Тип двенадцатого элемента.</typeparam>
    /// <typeparam name="T13">Тип тринадцатого элемента.</typeparam>
    /// <typeparam name="T14">Тип четырнадцатого элемента.</typeparam>
    /// <typeparam name="T15">Тип пятнадцатого элемента.</typeparam>
    /// <typeparam name="T16">Тип шестнадцатого элемента.</typeparam>
    /// <typeparam name="T17">Тип семнадцатого элемента.</typeparam>
    /// <typeparam name="T18">Тип восемнадцатого элемента.</typeparam>
    /// <typeparam name="T19">Тип девятнадцатого элемента.</typeparam>
    /// <typeparam name="T20">Тип двадцатого элемента.</typeparam>
    /// <typeparam name="T21">Тип двадцать первого элемента.</typeparam>
    public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19, out T20 item20, out T21 item21)
    {
      item1 = value.Item1;
      item2 = value.Item2;
      item3 = value.Item3;
      item4 = value.Item4;
      item5 = value.Item5;
      item6 = value.Item6;
      item7 = value.Item7;
      item8 = value.Rest.Item1;
      item9 = value.Rest.Item2;
      item10 = value.Rest.Item3;
      item11 = value.Rest.Item4;
      item12 = value.Rest.Item5;
      item13 = value.Rest.Item6;
      item14 = value.Rest.Item7;
      item15 = value.Rest.Rest.Item1;
      item16 = value.Rest.Rest.Item2;
      item17 = value.Rest.Rest.Item3;
      item18 = value.Rest.Rest.Item4;
      item19 = value.Rest.Rest.Item5;
      item20 = value.Rest.Rest.Item6;
      item21 = value.Rest.Rest.Item7;
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1> ToValueTuple<T1>(this Tuple<T1> value)
    {
      return ValueTuple.Create<T1>(value.Item1);
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2> ToValueTuple<T1, T2>(this Tuple<T1, T2> value)
    {
      return ValueTuple.Create<T1, T2>(value.Item1, value.Item2);
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3> ToValueTuple<T1, T2, T3>(this Tuple<T1, T2, T3> value)
    {
      return ValueTuple.Create<T1, T2, T3>(value.Item1, value.Item2, value.Item3);
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4> ToValueTuple<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> value)
    {
      return ValueTuple.Create<T1, T2, T3, T4>(value.Item1, value.Item2, value.Item3, value.Item4);
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5> ToValueTuple<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3, T4, T5> value)
    {
      return ValueTuple.Create<T1, T2, T3, T4, T5>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6> ToValueTuple<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3, T4, T5, T6> value)
    {
      return ValueTuple.Create<T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> ToValueTuple<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4, T5, T6, T7> value)
    {
      return ValueTuple.Create<T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8>(value.Rest.Item1));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9>(value.Rest.Item1, value.Rest.Item2));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10, T11>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10, T11, T12>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10, T11, T12, T13>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10, T11, T12, T13, T14>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15>(value.Rest.Rest.Item1)));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T16">
    ///   Тип шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16>(value.Rest.Rest.Item1, value.Rest.Rest.Item2)));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T16">
    ///   Тип шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T17">
    ///   Тип семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3)));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T16">
    ///   Тип шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T17">
    ///   Тип семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T18">
    ///   Тип восемнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item4" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17, T18>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4)));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T16">
    ///   Тип шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T17">
    ///   Тип семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T18">
    ///   Тип восемнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T19">
    ///   Тип девятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item5" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17, T18, T19>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5)));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T16">
    ///   Тип шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T17">
    ///   Тип семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T18">
    ///   Тип восемнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T19">
    ///   Тип девятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T20">
    ///   Тип двадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item6" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17, T18, T19, T20>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6)));
    }

    /// <summary>
    ///   Преобразует экземпляр класса <see langword="Tuple" /> в экземпляр структуры <see langword="ValueTuple" />.
    /// </summary>
    /// <param name="value">
    ///   Объект кортежа для преобразования в кортеж значений
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T16">
    ///   Тип шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T17">
    ///   Тип семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T18">
    ///   Тип восемнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T19">
    ///   Тип девятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T20">
    ///   Тип двадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T21">
    ///   Тип двадцать первого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item7" />.
    /// </typeparam>
    /// <returns>Преобразованный экземпляр кортежа значений.</returns>
    public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> value)
    {
      return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17, T18, T19, T20, T21>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6, value.Rest.Rest.Item7)));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1> ToTuple<T1>(this ValueTuple<T1> value)
    {
      return Tuple.Create<T1>(value.Item1);
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2> ToTuple<T1, T2>(this ValueTuple<T1, T2> value)
    {
      return Tuple.Create<T1, T2>(value.Item1, value.Item2);
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3> ToTuple<T1, T2, T3>(this ValueTuple<T1, T2, T3> value)
    {
      return Tuple.Create<T1, T2, T3>(value.Item1, value.Item2, value.Item3);
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4> ToTuple<T1, T2, T3, T4>(this ValueTuple<T1, T2, T3, T4> value)
    {
      return Tuple.Create<T1, T2, T3, T4>(value.Item1, value.Item2, value.Item3, value.Item4);
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5> ToTuple<T1, T2, T3, T4, T5>(this ValueTuple<T1, T2, T3, T4, T5> value)
    {
      return Tuple.Create<T1, T2, T3, T4, T5>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6> ToTuple<T1, T2, T3, T4, T5, T6>(this ValueTuple<T1, T2, T3, T4, T5, T6> value)
    {
      return Tuple.Create<T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7> ToTuple<T1, T2, T3, T4, T5, T6, T7>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7> value)
    {
      return Tuple.Create<T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8>(value.Rest.Item1));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9>(value.Rest.Item1, value.Rest.Item2));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10, T11>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10, T11, T12>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10, T11, T12, T13>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10, T11, T12, T13, T14>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15>(value.Rest.Rest.Item1)));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T16">
    ///   Тип шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16>(value.Rest.Rest.Item1, value.Rest.Rest.Item2)));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T16">
    ///   Тип шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T17">
    ///   Тип семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3)));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T16">
    ///   Тип шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T17">
    ///   Тип семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T18">
    ///   Тип восемнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item4" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17, T18>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4)));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T16">
    ///   Тип шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T17">
    ///   Тип семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T18">
    ///   Тип восемнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T19">
    ///   Тип девятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item5" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17, T18, T19>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5)));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T16">
    ///   Тип шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T17">
    ///   Тип семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T18">
    ///   Тип восемнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T19">
    ///   Тип девятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T20">
    ///   Тип двадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item6" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17, T18, T19, T20>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6)));
    }

    /// <summary>
    ///   Преобразует экземпляр структуры <see langword="ValueTuple" /> в экземпляр класса <see langword="Tuple" />.
    /// </summary>
    /// <param name="value">
    ///   Экземпляр кортежа значений для преобразования в кортеж.
    /// </param>
    /// <typeparam name="T1">Тип первого элемента.</typeparam>
    /// <typeparam name="T2">Тип второго элемента.</typeparam>
    /// <typeparam name="T3">Тип третьего элемента.</typeparam>
    /// <typeparam name="T4">Тип четвертого элемента.</typeparam>
    /// <typeparam name="T5">Тип пятого элемента.</typeparam>
    /// <typeparam name="T6">Тип шестого элемента.</typeparam>
    /// <typeparam name="T7">Тип седьмого элемента.</typeparam>
    /// <typeparam name="T8">
    ///   Тип восьмого элемента или <paramref name="value" /><see langword=".Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T9">
    ///   Тип девятого элемента или <paramref name="value" /><see langword=".Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T10">
    ///   Тип десятого элемента или <paramref name="value" /><see langword=".Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T11">
    ///   Тип одиннадцатого элемента или <paramref name="value" /><see langword=".Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T12">
    ///   Тип двенадцатого элемента или <paramref name="value" /><see langword=".Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T13">
    ///   Тип тринадцатого элемента или <paramref name="value" /><see langword=".Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T14">
    ///   Тип четырнадцатого элемента или <paramref name="value" /><see langword=".Rest.Item7" />.
    /// </typeparam>
    /// <typeparam name="T15">
    ///   Тип пятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item1" />.
    /// </typeparam>
    /// <typeparam name="T16">
    ///   Тип шестнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item2" />.
    /// </typeparam>
    /// <typeparam name="T17">
    ///   Тип семнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item3" />.
    /// </typeparam>
    /// <typeparam name="T18">
    ///   Тип восемнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item4" />.
    /// </typeparam>
    /// <typeparam name="T19">
    ///   Тип девятнадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item5" />.
    /// </typeparam>
    /// <typeparam name="T20">
    ///   Тип двадцатого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item6" />.
    /// </typeparam>
    /// <typeparam name="T21">
    ///   Тип двадцать первого элемента или <paramref name="value" /><see langword=".Rest.Rest.Item7" />.
    /// </typeparam>
    /// <returns>Преобразованный кортеж.</returns>
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>> value)
    {
      return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17, T18, T19, T20, T21>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6, value.Rest.Rest.Item7)));
    }

    private static ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> CreateLong<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest) where TRest : struct, ITuple
    {
      return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, rest);
    }

    private static Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> CreateLongRef<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest) where TRest : ITuple
    {
      return new Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, rest);
    }
  }
}
