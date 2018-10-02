// Decompiled with JetBrains decompiler
// Type: System.Nullable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Поддерживает тип значения, которое может быть назначено <see langword="null" />.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public static class Nullable
  {
    /// <summary>
    ///   Сравнивает относительные значения двух <see cref="T:System.Nullable`1" /> объектов.
    /// </summary>
    /// <param name="n1">
    ///   Объект <see cref="T:System.Nullable`1" />.
    /// </param>
    /// <param name="n2">
    ///   Объект <see cref="T:System.Nullable`1" />.
    /// </param>
    /// <typeparam name="T">
    ///   Базовый тип значения <paramref name="n1" /> и <paramref name="n2" /> параметров.
    /// </typeparam>
    /// <returns>
    /// Целое число, представляющее относительные значения <paramref name="n1" /> и <paramref name="n2" /> параметров.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Описание
    /// 
    ///         Меньше нуля
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойство <paramref name="n1" /> — <see langword="false" />и <see cref="P:System.Nullable`1.HasValue" /> свойство <paramref name="n2" /> — <see langword="true" />.
    /// 
    ///         -или-
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойства <paramref name="n1" /> и <paramref name="n2" /> , <see langword="true" />и значение <see cref="P:System.Nullable`1.Value" /> свойство для <paramref name="n1" /> меньше, чем значение <see cref="P:System.Nullable`1.Value" /> свойство <paramref name="n2" />.
    /// 
    ///         Нуль
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойства <paramref name="n1" /> и <paramref name="n2" /> являются <see langword="false" />.
    /// 
    ///         -или-
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойства для <paramref name="n1" /> и <paramref name="n2" /> , <see langword="true" />и значение <see cref="P:System.Nullable`1.Value" /> свойство для <paramref name="n1" /> равен значению <see cref="P:System.Nullable`1.Value" /> свойство <paramref name="n2" />.
    /// 
    ///         Больше нуля
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойство <paramref name="n1" /> — <see langword="true" />и <see cref="P:System.Nullable`1.HasValue" /> свойство <paramref name="n2" /> — <see langword="false" />.
    /// 
    ///         -или-
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойства <paramref name="n1" /> и <paramref name="n2" /> , <see langword="true" />и значение <see cref="P:System.Nullable`1.Value" /> свойство для <paramref name="n1" /> больше, чем значение <see cref="P:System.Nullable`1.Value" /> свойство <paramref name="n2" />.
    ///       </returns>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static int Compare<T>(T? n1, T? n2) where T : struct
    {
      if (n1.HasValue)
      {
        if (n2.HasValue)
          return Comparer<T>.Default.Compare(n1.value, n2.value);
        return 1;
      }
      return n2.HasValue ? -1 : 0;
    }

    /// <summary>
    ///   Указывает, равны ли значения двух заданных объектов <see cref="T:System.Nullable`1" />.
    /// </summary>
    /// <param name="n1">
    ///   Объект <see cref="T:System.Nullable`1" />.
    /// </param>
    /// <param name="n2">
    ///   Объект <see cref="T:System.Nullable`1" />.
    /// </param>
    /// <typeparam name="T">
    ///   Базовый тип значения <paramref name="n1" /> и <paramref name="n2" /> параметров.
    /// </typeparam>
    /// <returns>
    /// <see langword="true" /> Если <paramref name="n1" /> параметр равен <paramref name="n2" /> параметр; в противном случае — <see langword="false" />.
    /// 
    /// Возвращаемое значение зависит от <see cref="P:System.Nullable`1.HasValue" /> и <see cref="P:System.Nullable`1.Value" /> свойства двух параметров, которые прошли сравнение.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Описание
    /// 
    ///         <see langword="true" />
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойства <paramref name="n1" /> и <paramref name="n2" /> являются <see langword="false" />.
    /// 
    ///         -или-
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойства <paramref name="n1" /> и <paramref name="n2" /> являются <see langword="true" />, и <see cref="P:System.Nullable`1.Value" /> Свойства параметров равны.
    /// 
    ///         <see langword="false" />
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойство <see langword="true" /> для одного параметра и <see langword="false" /> для других параметров.
    /// 
    ///         -или-
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойства <paramref name="n1" /> и <paramref name="n2" /> являются <see langword="true" />, и <see cref="P:System.Nullable`1.Value" /> Свойства параметров не равны.
    ///       </returns>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static bool Equals<T>(T? n1, T? n2) where T : struct
    {
      if (n1.HasValue)
      {
        if (n2.HasValue)
          return EqualityComparer<T>.Default.Equals(n1.value, n2.value);
        return false;
      }
      return !n2.HasValue;
    }

    /// <summary>
    ///   Возвращает аргумент базового типа указанного типа, допускающих значение NULL.
    /// </summary>
    /// <param name="nullableType">
    ///   Объект <see cref="T:System.Type" /> , описывающий закрытый универсальный тип, допускающий значение NULL.
    /// </param>
    /// <returns>
    ///   Аргумент типа <paramref name="nullableType" /> параметр, если <paramref name="nullableType" /> параметр является закрытый универсальный тип, допускающий значение NULL; в противном случае <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="nullableType" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Type GetUnderlyingType(Type nullableType)
    {
      if ((object) nullableType == null)
        throw new ArgumentNullException(nameof (nullableType));
      Type type = (Type) null;
      if (nullableType.IsGenericType && !nullableType.IsGenericTypeDefinition && (object) nullableType.GetGenericTypeDefinition() == (object) typeof (Nullable<>))
        type = nullableType.GetGenericArguments()[0];
      return type;
    }
  }
}
