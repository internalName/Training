// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IList`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
  /// <summary>
  ///   Представляет коллекцию объектов, доступ к которым может быть получен индивидуально по индексу.
  /// </summary>
  /// <typeparam name="T">Тип элементов в списке.</typeparam>
  [TypeDependency("System.SZArrayHelper")]
  [__DynamicallyInvokable]
  public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
  {
    /// <summary>
    ///   Возвращает или задает элемент по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс элемента, который требуется возвратить или задать.
    /// </param>
    /// <returns>Элемент, расположенный по указанному индексу.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> не является допустимым индексом в <see cref="T:System.Collections.Generic.IList`1" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Свойство задано, и список <see cref="T:System.Collections.Generic.IList`1" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    T this[int index] { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>
    ///   Определяет индекс заданного элемента в списке <see cref="T:System.Collections.Generic.IList`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект для поиска в <see cref="T:System.Collections.Generic.IList`1" />.
    /// </param>
    /// <returns>
    ///   Индекс <paramref name="item" />, если он найден в списке; в противном случае — значение -1.
    /// </returns>
    [__DynamicallyInvokable]
    int IndexOf(T item);

    /// <summary>
    ///   Вставляет элемент в список <see cref="T:System.Collections.Generic.IList`1" /> по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому следует вставить элемент <paramref name="item" />.
    /// </param>
    /// <param name="item">
    ///   Объект, вставляемый в коллекцию <see cref="T:System.Collections.Generic.IList`1" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> не является допустимым индексом в <see cref="T:System.Collections.Generic.IList`1" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.Generic.IList`1" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    void Insert(int index, T item);

    /// <summary>
    ///   Удаляет элемент <see cref="T:System.Collections.Generic.IList`1" />, расположенный по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс удаляемого элемента.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> не является допустимым индексом в <see cref="T:System.Collections.Generic.IList`1" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.Generic.IList`1" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    void RemoveAt(int index);
  }
}
