// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IDictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  /// <summary>
  ///   Представляет универсальную коллекцию пар «ключ-значение».
  /// </summary>
  /// <typeparam name="TKey">Тип ключей в словаре.</typeparam>
  /// <typeparam name="TValue">Тип значений в словаре.</typeparam>
  [__DynamicallyInvokable]
  public interface IDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
  {
    /// <summary>Возвращает или задает элемент с указанным ключом.</summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется получить или задать.
    /// </param>
    /// <returns>Элемент с указанным ключом.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
    ///   Свойство получено и <paramref name="key" /> не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Свойство задано, и список <see cref="T:System.Collections.Generic.IDictionary`2" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    TValue this[TKey key] { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>
    ///   Возвращает интерфейс <see cref="T:System.Collections.Generic.ICollection`1" />, содержащий ключи <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.Generic.ICollection`1" /> Содержит ключи объекта, который реализует <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    ICollection<TKey> Keys { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает интерфейс <see cref="T:System.Collections.Generic.ICollection`1" />, содержащий значения из <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.Generic.ICollection`1" /> Содержит значения в объект, реализующий <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    ICollection<TValue> Values { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Определяет, содержится ли элемент с указанным ключом в <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ, который требуется найти в <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если в <see cref="T:System.Collections.Generic.IDictionary`2" /> содержится элемент с данным ключом; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    bool ContainsKey(TKey key);

    /// <summary>
    ///   Добавляет элемент с указанными ключом и значением в объект <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </summary>
    /// <param name="key">
    ///   Объект, используемый в качестве ключа добавляемого элемента.
    /// </param>
    /// <param name="value">
    ///   Объект, используемый в качестве значения добавляемого элемента.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент с таким ключом уже существует в <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.Generic.IDictionary`2" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    void Add(TKey key, TValue value);

    /// <summary>
    ///   Удаляет элемент с указанным ключом из <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется удалить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если элемент успешно удален; в противном случае — значение <see langword="false" />.
    ///     Этот метод также возвращает <see langword="false" />, если объект <paramref name="key" /> не был найден в исходной коллекции <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.Generic.IDictionary`2" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    bool Remove(TKey key);

    /// <summary>Возвращает значение, связанное с заданным ключом.</summary>
    /// <param name="key">
    ///   Ключ, значение которого необходимо получить.
    /// </param>
    /// <param name="value">
    ///   Этот метод возвращает значение, связанное с указанным ключом, если он найден; в противном случае — значение по умолчанию для данного типа параметра <paramref name="value" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если объект, реализующий <see cref="T:System.Collections.Generic.IDictionary`2" /> содержит элемент с указанным ключом; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    bool TryGetValue(TKey key, out TValue value);
  }
}
