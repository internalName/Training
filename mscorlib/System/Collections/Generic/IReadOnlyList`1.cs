// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IReadOnlyList`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
  /// <summary>
  ///   Представляет доступную только для чтения коллекцию элементов, доступ к которым может быть получен по индексу.
  /// </summary>
  /// <typeparam name="T">
  ///   Тип элементов в список только для чтения.
  /// </typeparam>
  [TypeDependency("System.SZArrayHelper")]
  [__DynamicallyInvokable]
  public interface IReadOnlyList<out T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
  {
    /// <summary>
    ///   Получает элемент в коллекции по указанному индексу в списке, доступном только для чтения.
    /// </summary>
    /// <param name="index">
    ///   Индекс элемента (с нуля), который требуется получить.
    /// </param>
    /// <returns>
    ///   Элемент в коллекции по указанному индексу в списке, доступном только для чтения.
    /// </returns>
    [__DynamicallyInvokable]
    T this[int index] { [__DynamicallyInvokable] get; }
  }
}
