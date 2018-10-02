// Decompiled with JetBrains decompiler
// Type: System.Action`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Инкапсулирует метод, который принимает один параметр и не возвращает значения.
  /// 
  ///   Для просмотра исходного кода .NET Framework для этого типа, в разделе Reference Source.
  /// </summary>
  /// <param name="obj">
  ///   Параметр метода, инкапсулируемого данным делегатом.
  /// </param>
  /// <typeparam name="T">
  ///   Тип параметра метода, инкапсулируемого данным делегатом.
  /// </typeparam>
  [__DynamicallyInvokable]
  public delegate void Action<in T>(T obj);
}
