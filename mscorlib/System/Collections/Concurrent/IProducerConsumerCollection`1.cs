// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.IProducerConsumerCollection`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Collections.Concurrent
{
  /// <summary>
  ///   Определяет методы для управления потокобезопасными коллекциями, предназначенными для использования производителем и потребителем.
  ///    Этот интерфейс предоставляет единое представление для производителя/потребителя коллекции, который более высокого уровня абстракции такие как <see cref="T:System.Collections.Concurrent.BlockingCollection`1" /> коллекции можно использовать в качестве базового механизма хранения.
  /// </summary>
  /// <typeparam name="T">Задает тип элементов в коллекции.</typeparam>
  [__DynamicallyInvokable]
  public interface IProducerConsumerCollection<T> : IEnumerable<T>, IEnumerable, ICollection
  {
    /// <summary>
    ///   Копирует элементы коллекции <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> для <see cref="T:System.Array" />, начиная с указанного индекса.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы коллекции <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.
    /// 
    ///    Индекс в массиве должен начинаться с нуля.
    /// </param>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс в массиве <paramref name="array" />, указывающий начало копирования.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="array" /> является ссылкой на null (Nothing в Visual Basic).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="index" /> равно или больше, чем длина <paramref name="array" /> - или - число элементов в коллекции больше, чем свободное пространство от <paramref name="index" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    [__DynamicallyInvokable]
    void CopyTo(T[] array, int index);

    /// <summary>
    ///   Пытается добавить объект в коллекцию <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект, добавляемый в коллекцию <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.
    /// </param>
    /// <returns>
    ///   Значение true, если объект был успешно добавлен; в противном случае — значение false.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="item" /> Недопустим для данной коллекции.
    /// </exception>
    [__DynamicallyInvokable]
    bool TryAdd(T item);

    /// <summary>
    ///   Пытается удалить и вернуть объект из коллекции <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.
    /// </summary>
    /// <param name="item">
    ///   При возвращении данного метода, если объект был успешно удален и возвращен, <paramref name="item" /> содержит удаленный объект.
    ///    Если объект, доступный для удаления, не найден, значение не определено.
    /// </param>
    /// <returns>
    ///   значение true, если объект был успешно удален и возвращен; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    bool TryTake(out T item);

    /// <summary>
    ///   Копирует элементы, содержащиеся в <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" /> в новый массив.
    /// </summary>
    /// <returns>
    ///   Новый массив, содержащий элементы, скопированные из <see cref="T:System.Collections.Concurrent.IProducerConsumerCollection`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    T[] ToArray();
  }
}
