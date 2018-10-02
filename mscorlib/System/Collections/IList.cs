// Decompiled with JetBrains decompiler
// Type: System.Collections.IList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Представляет неуниверсальную коллекцию объектов, к каждому из которых можно получить индивидуальный доступ по индексу.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IList : ICollection, IEnumerable
  {
    /// <summary>
    ///   Возвращает или задает элемент по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс элемента, который требуется возвратить или задать.
    /// </param>
    /// <returns>Элемент, расположенный по указанному индексу.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> не является допустимым индексом в <see cref="T:System.Collections.IList" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Свойство задано, и список <see cref="T:System.Collections.IList" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    object this[int index] { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>
    ///   Добавляет элемент в коллекцию <see cref="T:System.Collections.IList" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, добавляемый в коллекцию <see cref="T:System.Collections.IList" />.
    /// </param>
    /// <returns>
    ///   Позиция, в которую вставлен новый элемент, или значение -1, если элемент не вставлен в коллекцию.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.IList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.IList" /> Имеет фиксированный размер.
    /// </exception>
    [__DynamicallyInvokable]
    int Add(object value);

    /// <summary>
    ///   Определяет, содержит ли коллекция <see cref="T:System.Collections.IList" /> указанное значение.
    /// </summary>
    /// <param name="value">
    ///   Объект для поиска в <see cref="T:System.Collections.IList" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Object" /> находится в <see cref="T:System.Collections.IList" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool Contains(object value);

    /// <summary>
    ///   Удаляет все элементы из коллекции <see cref="T:System.Collections.IList" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.IList" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    void Clear();

    /// <summary>
    ///   Получает значение, указывающее, является ли объект <see cref="T:System.Collections.IList" /> доступным только для чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если коллекция <see cref="T:System.Collections.IList" /> доступна только для чтения; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool IsReadOnly { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Получает значение, указывающее, имеет ли список <see cref="T:System.Collections.IList" /> фиксированный размер.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если словарь <see cref="T:System.Collections.IList" /> имеет фиксированный размер; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool IsFixedSize { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Определяет индекс заданного элемента в списке <see cref="T:System.Collections.IList" />.
    /// </summary>
    /// <param name="value">
    ///   Объект для поиска в <see cref="T:System.Collections.IList" />.
    /// </param>
    /// <returns>
    ///   Индекс <paramref name="value" />, если он найден в списке; в противном случае — значение -1.
    /// </returns>
    [__DynamicallyInvokable]
    int IndexOf(object value);

    /// <summary>
    ///   Вставляет элемент в список <see cref="T:System.Collections.IList" /> по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому следует вставить элемент <paramref name="value" />.
    /// </param>
    /// <param name="value">
    ///   Объект, вставляемый в коллекцию <see cref="T:System.Collections.IList" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> не является допустимым индексом в <see cref="T:System.Collections.IList" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.IList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.IList" /> имеет фиксированный размер.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   <paramref name="value" /> является пустой ссылкой в <see cref="T:System.Collections.IList" />.
    /// </exception>
    [__DynamicallyInvokable]
    void Insert(int index, object value);

    /// <summary>
    ///   Удаляет первое вхождение указанного объекта из коллекции <see cref="T:System.Collections.IList" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, который необходимо удалить из коллекции <see cref="T:System.Collections.IList" />.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.IList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.IList" /> имеет фиксированный размер.
    /// </exception>
    [__DynamicallyInvokable]
    void Remove(object value);

    /// <summary>
    ///   Удаляет элемент <see cref="T:System.Collections.IList" />, расположенный по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс удаляемого элемента.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> не является допустимым индексом в <see cref="T:System.Collections.IList" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.IList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.IList" /> имеет фиксированный размер.
    /// </exception>
    [__DynamicallyInvokable]
    void RemoveAt(int index);
  }
}
