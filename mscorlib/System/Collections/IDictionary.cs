// Decompiled with JetBrains decompiler
// Type: System.Collections.IDictionary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Представляет небазовую коллекцию пар "ключ-значение".
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IDictionary : ICollection, IEnumerable
  {
    /// <summary>Возвращает или задает элемент с указанным ключом.</summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется получить или задать.
    /// </param>
    /// <returns>
    ///   Элемент с указанным ключом либо <see langword="null" />, если такого ключа не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Свойство имеет значение и <see cref="T:System.Collections.IDictionary" /> объект доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Задано свойство <paramref name="key" /> не существует в коллекции, а <see cref="T:System.Collections.IDictionary" /> имеет фиксированный размер.
    /// </exception>
    [__DynamicallyInvokable]
    object this[object key] { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.ICollection" /> объект, содержащий ключи из <see cref="T:System.Collections.IDictionary" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.ICollection" /> Объект, содержащий ключи из <see cref="T:System.Collections.IDictionary" /> объекта.
    /// </returns>
    [__DynamicallyInvokable]
    ICollection Keys { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.ICollection" /> объект, содержащий значения в <see cref="T:System.Collections.IDictionary" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.ICollection" /> Объект, содержащий значения в <see cref="T:System.Collections.IDictionary" /> объекта.
    /// </returns>
    [__DynamicallyInvokable]
    ICollection Values { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Определяет, содержится ли элемент с указанным ключом в объекте <see cref="T:System.Collections.IDictionary" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ для размещения в объекте <see cref="T:System.Collections.IDictionary" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если в <see cref="T:System.Collections.IDictionary" /> содержится элемент с данным ключом; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    bool Contains(object key);

    /// <summary>
    ///   Добавляет элемент с указанными ключом и значением в объект <see cref="T:System.Collections.IDictionary" />.
    /// </summary>
    /// <param name="key">
    ///   Объект <see cref="T:System.Object" /> используется в качестве ключа добавляемого элемента.
    /// </param>
    /// <param name="value">
    ///   Объект <see cref="T:System.Object" /> используется в качестве значения добавляемого элемента.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент с таким ключом уже существует в <see cref="T:System.Collections.IDictionary" /> объекта.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.IDictionary" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.IDictionary" /> Имеет фиксированный размер.
    /// </exception>
    [__DynamicallyInvokable]
    void Add(object key, object value);

    /// <summary>
    ///   Удаляет все элементы из объекта <see cref="T:System.Collections.IDictionary" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="T:System.Collections.IDictionary" /> Объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    void Clear();

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see cref="T:System.Collections.IDictionary" /> объект доступен только для чтения.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Collections.IDictionary" /> объект только для чтения; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool IsReadOnly { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see cref="T:System.Collections.IDictionary" /> объект имеет фиксированный размер.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Collections.IDictionary" /> объект имеет фиксированный размер; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool IsFixedSize { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Collections.IDictionaryEnumerator" /> для объекта <see cref="T:System.Collections.IDictionary" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Collections.IDictionaryEnumerator" /> для объекта <see cref="T:System.Collections.IDictionary" />.
    /// </returns>
    [__DynamicallyInvokable]
    IDictionaryEnumerator GetEnumerator();

    /// <summary>
    ///   Удаляет элемент с указанным ключом из объекта <see cref="T:System.Collections.IDictionary" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется удалить.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="T:System.Collections.IDictionary" /> Объект доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.IDictionary" /> имеет фиксированный размер.
    /// </exception>
    [__DynamicallyInvokable]
    void Remove(object key);
  }
}
