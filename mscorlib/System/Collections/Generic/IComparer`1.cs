// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  /// <summary>
  ///   Определяет метод, реализуемый типом для сравнения двух объектов.
  /// </summary>
  /// <typeparam name="T">Тип объектов для сравнения.</typeparam>
  [__DynamicallyInvokable]
  public interface IComparer<in T>
  {
    /// <summary>
    ///   Сравнение двух объектов и возврат значения, указывающего, является ли один объект меньшим, равным или большим другого.
    /// </summary>
    /// <param name="x">Первый из сравниваемых объектов.</param>
    /// <param name="y">Второй из сравниваемых объектов.</param>
    /// <returns>
    /// Знаковое целое число, которое определяет относительные значения параметров <paramref name="x" /> и <paramref name="y" />, как показано в следующей таблице.
    /// 
    ///         Значение
    /// 
    ///         Значение
    /// 
    ///         Меньше нуля
    /// 
    ///         Значение <paramref name="x" /> меньше <paramref name="y" />.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="x" /> равняется <paramref name="y" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Значение <paramref name="x" /> больше значения <paramref name="y" />.
    ///       </returns>
    [__DynamicallyInvokable]
    int Compare(T x, T y);
  }
}
