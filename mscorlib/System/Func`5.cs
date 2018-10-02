// Decompiled with JetBrains decompiler
// Type: System.Func`5
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System
{
  /// <summary>
  ///   Инкапсулирует метод, который имеет четыре параметра и возвращает значение типа, указанного в параметре <paramref name="TResult" />.
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
  /// <typeparam name="TResult">
  ///   Тип возвращаемого значения метода, инкапсулируемого данным делегатом.
  /// </typeparam>
  /// <returns>
  ///   Возвращаемое значение метода, инкапсулируемого данным делегатом.
  /// </returns>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [__DynamicallyInvokable]
  public delegate TResult Func<in T1, in T2, in T3, in T4, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}
