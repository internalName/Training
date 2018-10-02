// Decompiled with JetBrains decompiler
// Type: System.Converter`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Представляет метод, который преобразует объект из одного типа в другой тип.
  /// </summary>
  /// <param name="input">Преобразуемый объект.</param>
  /// <typeparam name="TInput">
  ///   Тип объекта, который требуется преобразовать.
  /// </typeparam>
  /// <typeparam name="TOutput">
  ///   Входной объект должен преобразовываться в тип.
  /// </typeparam>
  /// <returns>
  ///   <paramref name="TOutput" /> , Представляющий преобразованный <paramref name="TInput" />.
  /// </returns>
  public delegate TOutput Converter<in TInput, out TOutput>(TInput input);
}
