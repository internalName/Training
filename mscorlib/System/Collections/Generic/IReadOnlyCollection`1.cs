// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IReadOnlyCollection`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
  /// <summary>
  ///   Возвращает строго типизированную коллекцию элементов, доступную только для чтения.
  /// </summary>
  /// <typeparam name="T">Тип элементов.</typeparam>
  [TypeDependency("System.SZArrayHelper")]
  [__DynamicallyInvokable]
  public interface IReadOnlyCollection<out T> : IEnumerable<T>, IEnumerable
  {
    /// <summary>Возвращает количество элементов в коллекции.</summary>
    /// <returns>Количество элементов в коллекции.</returns>
    [__DynamicallyInvokable]
    int Count { [__DynamicallyInvokable] get; }
  }
}
