// Decompiled with JetBrains decompiler
// Type: System.Predicate`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Представляет метод, определяющий набор критериев и соответствие указанного объекта этим критериям.
  /// </summary>
  /// <param name="obj">
  ///   Объект, проверяемый на соответствие критериям, заданным в метод, представленный этим делегатом.
  /// </param>
  /// <typeparam name="T">Тип объект для сравнения.</typeparam>
  /// <returns>
  ///   <see langword="true" />Если <paramref name="obj" /> соответствует критериям, заданным в методе, который представляет этот делегат; в противном случае <see langword="false" />.
  /// </returns>
  [__DynamicallyInvokable]
  public delegate bool Predicate<in T>(T obj);
}
