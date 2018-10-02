// Decompiled with JetBrains decompiler
// Type: System.Func`3
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System
{
  /// <summary>
  ///   Инкапсулирует метод, который имеет два параметра и возвращает значение типа, указанного параметром <paramref name="TResult" />.
  /// </summary>
  /// <param name="arg1">
  ///   Первый параметр метода, инкапсулируемого этим делегатом.
  /// </param>
  /// <param name="arg2">
  ///   Второй параметр метода, инкапсулируемого данным делегатом.
  /// </param>
  /// <typeparam name="T1">
  ///   Тип первого параметра метода, инкапсулируемого данным делегатом.
  /// </typeparam>
  /// <typeparam name="T2">
  ///   Тип второго параметра метода, инкапсулируемого данным делегатом.
  /// </typeparam>
  /// <typeparam name="TResult">
  ///   Тип возвращаемого значения метода, инкапсулируемого данным делегатом.
  /// </typeparam>
  /// <returns>
  ///   Возвращаемое значение метода, инкапсулируемого данным делегатом.
  /// </returns>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [__DynamicallyInvokable]
  public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
}
