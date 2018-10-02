// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.ICollection`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
  /// <summary>
  ///   Определяет методы для управления универсальными коллекциями.
  /// </summary>
  /// <typeparam name="T">Тип элементов в коллекции.</typeparam>
  [TypeDependency("System.SZArrayHelper")]
  [__DynamicallyInvokable]
  public interface ICollection<T> : IEnumerable<T>, IEnumerable
  {
    /// <summary>
    ///   Получает число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    int Count { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Получает значение, указывающее, является ли объект <see cref="T:System.Collections.Generic.ICollection`1" /> доступным только для чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если интерфейс <see cref="T:System.Collections.Generic.ICollection`1" /> доступен только для чтения; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool IsReadOnly { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Добавляет элемент в коллекцию <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект, добавляемый в коллекцию <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.Generic.ICollection`1" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    void Add(T item);

    /// <summary>
    ///   Удаляет все элементы из коллекции <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.Generic.ICollection`1" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    void Clear();

    /// <summary>
    ///   Определяет, содержит ли коллекция <see cref="T:System.Collections.Generic.ICollection`1" /> указанное значение.
    /// </summary>
    /// <param name="item">
    ///   Объект для поиска в <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="item" /> найден в коллекции <see cref="T:System.Collections.Generic.ICollection`1" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool Contains(T item);

    /// <summary>
    ///   Копирует элементы коллекции <see cref="T:System.Collections.Generic.ICollection`1" /> в массив <see cref="T:System.Array" />, начиная с указанного индекса массива <see cref="T:System.Array" />.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.Generic.ICollection`1" />.
    ///    Массив <see cref="T:System.Array" /> должен иметь индексацию, начинающуюся с нуля.
    /// </param>
    /// <param name="arrayIndex">
    ///   Отсчитываемый от нуля индекс в массиве <paramref name="array" />, указывающий начало копирования.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="arrayIndex" /> меньше 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Количество элементов в исходной коллекции <see cref="T:System.Collections.Generic.ICollection`1" /> больше, чем свободное пространство от <paramref name="arrayIndex" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    [__DynamicallyInvokable]
    void CopyTo(T[] array, int arrayIndex);

    /// <summary>
    ///   Удаляет первое вхождение указанного объекта из коллекции <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект, который необходимо удалить из коллекции <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <paramref name="item" /> успешно удален из <see cref="T:System.Collections.Generic.ICollection`1" />; в противном случае — значение <see langword="false" />.
    ///    Этот метод также возвращает значение <see langword="false" />, если значение <paramref name="item" /> не найдено в исходной коллекции <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.Generic.ICollection`1" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    bool Remove(T item);
  }
}
