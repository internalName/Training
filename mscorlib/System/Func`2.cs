// Decompiled with JetBrains decompiler
// Type: System.Func`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System
{
  /// <summary>
  ///   Инкапсулирует метод с одним параметром, который возвращает значение типа, указанного в параметре <paramref name="TResult" />.
  /// 
  ///   Для просмотра исходного кода .NET Framework для этого типа, в разделе Reference Source.
  /// </summary>
  /// <param name="arg">
  ///   Параметр метода, инкапсулируемого данным делегатом.
  /// </param>
  /// <typeparam name="T">
  ///   Тип параметра метода, инкапсулируемого данным делегатом.
  /// </typeparam>
  /// <typeparam name="TResult">
  ///   Тип возвращаемого значения метода, инкапсулируемого данным делегатом.
  /// </typeparam>
  /// <returns>
  ///   Возвращаемое значение метода, инкапсулируемого данным делегатом.
  /// </returns>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [__DynamicallyInvokable]
  public delegate TResult Func<in T, out TResult>(T arg);
}
