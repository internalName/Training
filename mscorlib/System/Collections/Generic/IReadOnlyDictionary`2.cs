// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IReadOnlyDictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  /// <summary>
  ///   Представляет универсальную коллекцию пар «ключ-значение», доступную только для чтения.
  /// </summary>
  /// <typeparam name="TKey">
  ///   Тип ключей в словаре только для чтения.
  /// </typeparam>
  /// <typeparam name="TValue">
  ///   Тип значений в словаре только для чтения.
  /// </typeparam>
  [__DynamicallyInvokable]
  public interface IReadOnlyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
  {
    /// <summary>
    ///   Определяет, содержит ли словарь только для чтения элемент с указанным ключом.
    /// </summary>
    /// <param name="key">Искомый ключ.</param>
    /// <returns>
    ///   <see langword="true" /> Если словарь только для чтения, содержит элемент с указанным ключом; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    bool ContainsKey(TKey key);

    /// <summary>Возвращает значение, связанное с указанным ключом.</summary>
    /// <param name="key">Искомый ключ.</param>
    /// <param name="value">
    ///   Этот метод возвращает значение, связанное с указанным ключом, если он найден; в противном случае — значение по умолчанию для данного типа параметра <paramref name="value" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если объект, реализующий <see cref="T:System.Collections.Generic.IReadOnlyDictionary`2" /> интерфейс содержит элемент с указанным ключом; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    bool TryGetValue(TKey key, out TValue value);

    /// <summary>
    ///   Возвращает элемент, имеющий указанный ключ в словаре только для чтения.
    /// </summary>
    /// <param name="key">Искомый ключ.</param>
    /// <returns>
    ///   Элемент, имеющий указанный ключ в словаре только для чтения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
    ///   Свойство получено и <paramref name="key" /> не найден.
    /// </exception>
    [__DynamicallyInvokable]
    TValue this[TKey key] { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Получает перечисляемую коллекция, содержащую ключи в словаре только для чтения.
    /// </summary>
    /// <returns>
    ///   Перечисляемая коллекция, содержащая ключи в словаре только для чтения.
    /// </returns>
    [__DynamicallyInvokable]
    IEnumerable<TKey> Keys { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Получает перечисляемую коллекцию, содержащая значения в словаре только для чтения.
    /// </summary>
    /// <returns>
    ///   Перечисляемая коллекция, содержащая значения в словаре только для чтения.
    /// </returns>
    [__DynamicallyInvokable]
    IEnumerable<TValue> Values { [__DynamicallyInvokable] get; }
  }
}
