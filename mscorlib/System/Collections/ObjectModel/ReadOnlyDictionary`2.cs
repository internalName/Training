// Decompiled with JetBrains decompiler
// Type: System.Collections.ObjectModel.ReadOnlyDictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace System.Collections.ObjectModel
{
  /// <summary>
  ///   Представляет универсальную коллекцию пар «ключ-значение», доступную только для чтения.
  /// </summary>
  /// <typeparam name="TKey">Тип ключей в словаре.</typeparam>
  /// <typeparam name="TValue">Тип значений в словаре.</typeparam>
  [DebuggerTypeProxy(typeof (Mscorlib_DictionaryDebugView<,>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
  {
    private readonly IDictionary<TKey, TValue> m_dictionary;
    [NonSerialized]
    private object m_syncRoot;
    [NonSerialized]
    private ReadOnlyDictionary<TKey, TValue>.KeyCollection m_keys;
    [NonSerialized]
    private ReadOnlyDictionary<TKey, TValue>.ValueCollection m_values;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> класс, являющийся оболочкой указанного словаря.
    /// </summary>
    /// <param name="dictionary">Словарь для упаковки.</param>
    [__DynamicallyInvokable]
    public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
    {
      if (dictionary == null)
        throw new ArgumentNullException(nameof (dictionary));
      this.m_dictionary = dictionary;
    }

    /// <summary>
    ///   Возвращает словарь, который является оболочкой для которого это <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> объекта.
    /// </summary>
    /// <returns>
    ///   Словарь, который является оболочкой для этого объекта.
    /// </returns>
    [__DynamicallyInvokable]
    protected IDictionary<TKey, TValue> Dictionary
    {
      [__DynamicallyInvokable] get
      {
        return this.m_dictionary;
      }
    }

    /// <summary>
    ///   Возвращает коллекцию ключей, содержащий ключи словаря.
    /// </summary>
    /// <returns>Коллекция ключей, содержащий ключи словаря.</returns>
    [__DynamicallyInvokable]
    public ReadOnlyDictionary<TKey, TValue>.KeyCollection Keys
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_keys == null)
          this.m_keys = new ReadOnlyDictionary<TKey, TValue>.KeyCollection(this.m_dictionary.Keys);
        return this.m_keys;
      }
    }

    /// <summary>Возвращает коллекцию, содержащую значения словаря.</summary>
    /// <returns>
    ///   Коллекция, содержащая значения в объект, реализующий <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public ReadOnlyDictionary<TKey, TValue>.ValueCollection Values
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_values == null)
          this.m_values = new ReadOnlyDictionary<TKey, TValue>.ValueCollection(this.m_dictionary.Values);
        return this.m_values;
      }
    }

    /// <summary>
    ///   Определяет, содержит ли словарь элемент с указанным ключом.
    /// </summary>
    /// <param name="key">Ключ, который нужно найти в словаре.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если словарь содержит элемент с указанным ключом; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool ContainsKey(TKey key)
    {
      return this.m_dictionary.ContainsKey(key);
    }

    [__DynamicallyInvokable]
    ICollection<TKey> IDictionary<TKey, TValue>.Keys
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection<TKey>) this.Keys;
      }
    }

    /// <summary>Извлекает значение, связанное с указанным ключом.</summary>
    /// <param name="key">Ключ, значение которого нужно получить.</param>
    /// <param name="value">
    ///   Этот метод возвращает значение, связанное с указанным ключом, если он найден; в противном случае — значение по умолчанию для данного типа параметра <paramref name="value" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если объект, реализующий <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> содержит элемент с указанным ключом; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool TryGetValue(TKey key, out TValue value)
    {
      return this.m_dictionary.TryGetValue(key, out value);
    }

    [__DynamicallyInvokable]
    ICollection<TValue> IDictionary<TKey, TValue>.Values
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection<TValue>) this.Values;
      }
    }

    /// <summary>Возвращает элемент, имеющий указанный ключ.</summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется получить.
    /// </param>
    /// <returns>Элемент с указанным ключом.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
    ///   Свойство получено и <paramref name="key" /> не найден.
    /// </exception>
    [__DynamicallyInvokable]
    public TValue this[TKey key]
    {
      [__DynamicallyInvokable] get
      {
        return this.m_dictionary[key];
      }
    }

    [__DynamicallyInvokable]
    void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    bool IDictionary<TKey, TValue>.Remove(TKey key)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      return false;
    }

    [__DynamicallyInvokable]
    TValue IDictionary<TKey, TValue>.this[TKey key]
    {
      [__DynamicallyInvokable] get
      {
        return this.m_dictionary[key];
      }
      [__DynamicallyInvokable] set
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }
    }

    /// <summary>Возвращает количество элементов в словаре.</summary>
    /// <returns>Число элементов в словаре.</returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this.m_dictionary.Count;
      }
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
    {
      return this.m_dictionary.Contains(item);
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      this.m_dictionary.CopyTo(array, arrayIndex);
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.Clear()
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      return false;
    }

    /// <summary>
    ///   Возвращает перечислитель, осуществляющий перебор элементов списка <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" />.
    /// </summary>
    /// <returns>
    ///   Перечислитель, который можно использовать для итерации по коллекции.
    /// </returns>
    [__DynamicallyInvokable]
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return this.m_dictionary.GetEnumerator();
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.m_dictionary.GetEnumerator();
    }

    private static bool IsCompatibleKey(object key)
    {
      if (key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      return key is TKey;
    }

    [__DynamicallyInvokable]
    void IDictionary.Add(object key, object value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void IDictionary.Clear()
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    bool IDictionary.Contains(object key)
    {
      if (ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key))
        return this.ContainsKey((TKey) key);
      return false;
    }

    [__DynamicallyInvokable]
    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      IDictionary dictionary = this.m_dictionary as IDictionary;
      if (dictionary != null)
        return dictionary.GetEnumerator();
      return (IDictionaryEnumerator) new ReadOnlyDictionary<TKey, TValue>.DictionaryEnumerator(this.m_dictionary);
    }

    [__DynamicallyInvokable]
    bool IDictionary.IsFixedSize
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    bool IDictionary.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    ICollection IDictionary.Keys
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection) this.Keys;
      }
    }

    [__DynamicallyInvokable]
    void IDictionary.Remove(object key)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    ICollection IDictionary.Values
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection) this.Values;
      }
    }

    [__DynamicallyInvokable]
    object IDictionary.this[object key]
    {
      [__DynamicallyInvokable] get
      {
        if (ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key))
          return (object) this[(TKey) key];
        return (object) null;
      }
      [__DynamicallyInvokable] set
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }
    }

    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
      if (array.Rank != 1)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
      if (array.GetLowerBound(0) != 0)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
      if (index < 0 || index > array.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (array.Length - index < this.Count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      KeyValuePair<TKey, TValue>[] array1 = array as KeyValuePair<TKey, TValue>[];
      if (array1 != null)
      {
        this.m_dictionary.CopyTo(array1, index);
      }
      else
      {
        DictionaryEntry[] dictionaryEntryArray = array as DictionaryEntry[];
        if (dictionaryEntryArray != null)
        {
          foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) this.m_dictionary)
            dictionaryEntryArray[index++] = new DictionaryEntry((object) keyValuePair.Key, (object) keyValuePair.Value);
        }
        else
        {
          object[] objArray = array as object[];
          if (objArray == null)
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          try
          {
            foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) this.m_dictionary)
              objArray[index++] = (object) new KeyValuePair<TKey, TValue>(keyValuePair.Key, keyValuePair.Value);
          }
          catch (ArrayTypeMismatchException ex)
          {
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          }
        }
      }
    }

    [__DynamicallyInvokable]
    bool ICollection.IsSynchronized
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    object ICollection.SyncRoot
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_syncRoot == null)
        {
          ICollection dictionary = this.m_dictionary as ICollection;
          if (dictionary != null)
            this.m_syncRoot = dictionary.SyncRoot;
          else
            Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), (object) null);
        }
        return this.m_syncRoot;
      }
    }

    [__DynamicallyInvokable]
    IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<TKey>) this.Keys;
      }
    }

    [__DynamicallyInvokable]
    IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<TValue>) this.Values;
      }
    }

    [Serializable]
    private struct DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
    {
      private readonly IDictionary<TKey, TValue> m_dictionary;
      private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;

      public DictionaryEnumerator(IDictionary<TKey, TValue> dictionary)
      {
        this.m_dictionary = dictionary;
        this.m_enumerator = this.m_dictionary.GetEnumerator();
      }

      public DictionaryEntry Entry
      {
        get
        {
          KeyValuePair<TKey, TValue> current = this.m_enumerator.Current;
          __Boxed<TKey> key = (object) current.Key;
          current = this.m_enumerator.Current;
          __Boxed<TValue> local = (object) current.Value;
          return new DictionaryEntry((object) key, (object) local);
        }
      }

      public object Key
      {
        get
        {
          return (object) this.m_enumerator.Current.Key;
        }
      }

      public object Value
      {
        get
        {
          return (object) this.m_enumerator.Current.Value;
        }
      }

      public object Current
      {
        get
        {
          return (object) this.Entry;
        }
      }

      public bool MoveNext()
      {
        return this.m_enumerator.MoveNext();
      }

      public void Reset()
      {
        this.m_enumerator.Reset();
      }
    }

    /// <summary>
    ///   Представляет доступную только для чтения коллекцию ключей объекта <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" />.
    /// </summary>
    [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [__DynamicallyInvokable]
    [Serializable]
    public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
    {
      private readonly ICollection<TKey> m_collection;
      [NonSerialized]
      private object m_syncRoot;

      internal KeyCollection(ICollection<TKey> collection)
      {
        if (collection == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
        this.m_collection = collection;
      }

      [__DynamicallyInvokable]
      void ICollection<TKey>.Add(TKey item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }

      [__DynamicallyInvokable]
      void ICollection<TKey>.Clear()
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }

      [__DynamicallyInvokable]
      bool ICollection<TKey>.Contains(TKey item)
      {
        return this.m_collection.Contains(item);
      }

      /// <summary>
      ///   Копирует элементы коллекции в массив, начиная с заданного индекса в массиве.
      /// </summary>
      /// <param name="array">
      ///   Одномерный массив, куда копируются элементы из данной коллекции.
      ///    Индекс в массиве должен начинаться с нуля.
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
      ///   Массив <paramref name="array" /> является многомерным.
      /// 
      ///   -или-
      /// 
      ///   Число элементов в исходной коллекции больше, чем свободное пространство от <paramref name="arrayIndex" /> до конца массива назначения <paramref name="array" />.
      /// 
      ///   -или-
      /// 
      ///   Тип <paramref name="T" /> не может быть автоматически приведен к типу целевого массива <paramref name="array" />.
      /// </exception>
      [__DynamicallyInvokable]
      public void CopyTo(TKey[] array, int arrayIndex)
      {
        this.m_collection.CopyTo(array, arrayIndex);
      }

      /// <summary>Возвращает количество элементов в коллекции.</summary>
      /// <returns>Количество элементов в коллекции.</returns>
      [__DynamicallyInvokable]
      public int Count
      {
        [__DynamicallyInvokable] get
        {
          return this.m_collection.Count;
        }
      }

      [__DynamicallyInvokable]
      bool ICollection<TKey>.IsReadOnly
      {
        [__DynamicallyInvokable] get
        {
          return true;
        }
      }

      [__DynamicallyInvokable]
      bool ICollection<TKey>.Remove(TKey item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        return false;
      }

      /// <summary>
      ///   Возвращает перечислитель, выполняющий перебор элементов в коллекции.
      /// </summary>
      /// <returns>
      ///   Перечислитель, который можно использовать для итерации по коллекции.
      /// </returns>
      [__DynamicallyInvokable]
      public IEnumerator<TKey> GetEnumerator()
      {
        return this.m_collection.GetEnumerator();
      }

      [__DynamicallyInvokable]
      IEnumerator IEnumerable.GetEnumerator()
      {
        return this.m_collection.GetEnumerator();
      }

      [__DynamicallyInvokable]
      void ICollection.CopyTo(Array array, int index)
      {
        ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TKey>(this.m_collection, array, index);
      }

      [__DynamicallyInvokable]
      bool ICollection.IsSynchronized
      {
        [__DynamicallyInvokable] get
        {
          return false;
        }
      }

      [__DynamicallyInvokable]
      object ICollection.SyncRoot
      {
        [__DynamicallyInvokable] get
        {
          if (this.m_syncRoot == null)
          {
            ICollection collection = this.m_collection as ICollection;
            if (collection != null)
              this.m_syncRoot = collection.SyncRoot;
            else
              Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), (object) null);
          }
          return this.m_syncRoot;
        }
      }
    }

    /// <summary>
    ///   Представляет доступную только для чтения коллекцию значений объекта <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" />.
    /// </summary>
    [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [__DynamicallyInvokable]
    [Serializable]
    public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
    {
      private readonly ICollection<TValue> m_collection;
      [NonSerialized]
      private object m_syncRoot;

      internal ValueCollection(ICollection<TValue> collection)
      {
        if (collection == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
        this.m_collection = collection;
      }

      [__DynamicallyInvokable]
      void ICollection<TValue>.Add(TValue item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }

      [__DynamicallyInvokable]
      void ICollection<TValue>.Clear()
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }

      [__DynamicallyInvokable]
      bool ICollection<TValue>.Contains(TValue item)
      {
        return this.m_collection.Contains(item);
      }

      /// <summary>
      ///   Копирует элементы коллекции в массив, начиная с заданного индекса в массиве.
      /// </summary>
      /// <param name="array">
      ///   Одномерный массив, куда копируются элементы из данной коллекции.
      ///    Индекс в массиве должен начинаться с нуля.
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
      ///   Массив <paramref name="array" /> является многомерным.
      /// 
      ///   -или-
      /// 
      ///   Число элементов в исходной коллекции больше, чем свободное пространство от <paramref name="arrayIndex" /> до конца массива назначения <paramref name="array" />.
      /// 
      ///   -или-
      /// 
      ///   Тип <paramref name="T" /> не может быть автоматически приведен к типу целевого массива <paramref name="array" />.
      /// </exception>
      [__DynamicallyInvokable]
      public void CopyTo(TValue[] array, int arrayIndex)
      {
        this.m_collection.CopyTo(array, arrayIndex);
      }

      /// <summary>Возвращает количество элементов в коллекции.</summary>
      /// <returns>Количество элементов в коллекции.</returns>
      [__DynamicallyInvokable]
      public int Count
      {
        [__DynamicallyInvokable] get
        {
          return this.m_collection.Count;
        }
      }

      [__DynamicallyInvokable]
      bool ICollection<TValue>.IsReadOnly
      {
        [__DynamicallyInvokable] get
        {
          return true;
        }
      }

      [__DynamicallyInvokable]
      bool ICollection<TValue>.Remove(TValue item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        return false;
      }

      /// <summary>
      ///   Возвращает перечислитель, выполняющий перебор элементов в коллекции.
      /// </summary>
      /// <returns>
      ///   Перечислитель, который можно использовать для итерации по коллекции.
      /// </returns>
      [__DynamicallyInvokable]
      public IEnumerator<TValue> GetEnumerator()
      {
        return this.m_collection.GetEnumerator();
      }

      [__DynamicallyInvokable]
      IEnumerator IEnumerable.GetEnumerator()
      {
        return this.m_collection.GetEnumerator();
      }

      [__DynamicallyInvokable]
      void ICollection.CopyTo(Array array, int index)
      {
        ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TValue>(this.m_collection, array, index);
      }

      [__DynamicallyInvokable]
      bool ICollection.IsSynchronized
      {
        [__DynamicallyInvokable] get
        {
          return false;
        }
      }

      [__DynamicallyInvokable]
      object ICollection.SyncRoot
      {
        [__DynamicallyInvokable] get
        {
          if (this.m_syncRoot == null)
          {
            ICollection collection = this.m_collection as ICollection;
            if (collection != null)
              this.m_syncRoot = collection.SyncRoot;
            else
              Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), (object) null);
          }
          return this.m_syncRoot;
        }
      }
    }
  }
}
