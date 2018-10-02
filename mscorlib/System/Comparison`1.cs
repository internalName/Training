// Decompiled with JetBrains decompiler
// Type: System.Comparison`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Представляет метод, который сравнивает два объекта одного типа.
  /// </summary>
  /// <param name="x">Первый из сравниваемых объектов.</param>
  /// <param name="y">Второй из сравниваемых объектов.</param>
  /// <typeparam name="T">Тип объектов для сравнения.</typeparam>
  /// <returns>
  /// Знаковое целое число, которое определяет относительные значения параметров <paramref name="x" /> и <paramref name="y" />, как показано в следующей таблице.
  /// 
  ///         Значение
  /// 
  ///         Значение
  /// 
  ///         Меньше 0
  /// 
  ///         Значение <paramref name="x" /> меньше <paramref name="y" />.
  /// 
  ///         0
  /// 
  ///         <paramref name="x" /> равняется <paramref name="y" />.
  /// 
  ///         Больше 0
  /// 
  ///         Значение <paramref name="x" /> больше значения <paramref name="y" />.
  ///       </returns>
  [__DynamicallyInvokable]
  public delegate int Comparison<in T>(T x, T y);
}
