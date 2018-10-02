// Decompiled with JetBrains decompiler
// Type: System.Collections.IComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Предоставляет метод, который сравнивает два объекта.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IComparer
  {
    /// <summary>
    ///   Сравнивает два объекта и возвращает значение, указывающее, что один объект меньше, равняется или больше другого.
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
    /// <exception cref="T:System.ArgumentException">
    ///   Ни <paramref name="x" /> ни <paramref name="y" /> реализует <see cref="T:System.IComparable" /> интерфейса.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="x" /> и <paramref name="y" /> имеют разные типы и ни один могут сравниваться с другими.
    /// </exception>
    [__DynamicallyInvokable]
    int Compare(object x, object y);
  }
}
