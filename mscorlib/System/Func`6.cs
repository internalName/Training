// Decompiled with JetBrains decompiler
// Type: System.Func`6
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Инкапсулирует метод, который имеет пять параметров и возвращает значение типа, указанного параметром <paramref name="TResult" /> параметр.
  /// </summary>
  /// <param name="arg1">
  ///   Первый параметр метода, инкапсулируемого этим делегатом.
  /// </param>
  /// <param name="arg2">
  ///   Второй параметр метода, инкапсулируемого этим делегатом.
  /// </param>
  /// <param name="arg3">
  ///   Третий параметр метода, инкапсулируемого этим делегатом.
  /// </param>
  /// <param name="arg4">
  ///   Четвертый параметр метода, инкапсулируемого этим делегатом.
  /// </param>
  /// <param name="arg5">
  ///   Пятого параметра метода, инкапсулируемого данным делегатом.
  /// </param>
  /// <typeparam name="T1">
  ///   Тип первого параметра метода, инкапсулируемого этим делегатом.
  /// </typeparam>
  /// <typeparam name="T2">
  ///   Тип второго параметра метода, инкапсулируемого этим делегатом.
  /// </typeparam>
  /// <typeparam name="T3">
  ///   Тип третьего параметра метода, инкапсулируемого этим делегатом.
  /// </typeparam>
  /// <typeparam name="T4">
  ///   Тип четвертого параметра метода, инкапсулируемого этим делегатом.
  /// </typeparam>
  /// <typeparam name="T5">
  ///   Тип пятого параметра метода, инкапсулируемого данным делегатом.
  /// </typeparam>
  /// <typeparam name="TResult">
  ///   Тип возвращаемого значения метода, инкапсулируемого данным делегатом.
  /// </typeparam>
  /// <returns>
  ///   Возвращаемое значение метода, инкапсулируемого данным делегатом.
  /// </returns>
  [__DynamicallyInvokable]
  public delegate TResult Func<in T1, in T2, in T3, in T4, in T5, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
}
