// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.Dictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic
{
  /// <summary>
  ///   Представляет коллекцию ключей и значений.
  /// 
  ///   Исходный код .NET Framework для этого типа см. в указанном источнике.
  /// </summary>
  /// <typeparam name="TKey">Тип ключей в словаре.</typeparam>
  /// <typeparam name="TValue">Тип значений в словаре.</typeparam>
  [DebuggerTypeProxy(typeof (Mscorlib_DictionaryDebugView<,>))]
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(false)]
  [__DynamicallyInvokable]
  [Serializable]
  public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, ISerializable, IDeserializationCallback
  {
    private int[] buckets;
    private Dictionary<TKey, TValue>.Entry[] entries;
    private int count;
    private int version;
    private int freeList;
    private int freeCount;
    private IEqualityComparer<TKey> comparer;
    private Dictionary<TKey, TValue>.KeyCollection keys;
    private Dictionary<TKey, TValue>.ValueCollection values;
    private object _syncRoot;
    private const string VersionName = "Version";
    private const string HashSizeName = "HashSize";
    private const string KeyValuePairsName = "KeyValuePairs";
    private const string ComparerName = "Comparer";

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Generic.Dictionary`2" />, имеющий начальную емкость по умолчанию и использующий функцию сравнения по умолчанию, проверяющую равенство для данного типа ключа.
    /// </summary>
    [__DynamicallyInvokable]
    public Dictionary()
      : this(0, (IEqualityComparer<TKey>) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Generic.Dictionary`2" />, имеющий заданную начальную емкость и использующий функцию сравнения по умолчанию, проверяющую равенство для данного типа ключа.
    /// </summary>
    /// <param name="capacity">
    ///   Начальное количество элементов, которое может содержать коллекция <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше 0.
    /// </exception>
    [__DynamicallyInvokable]
    public Dictionary(int capacity)
      : this(capacity, (IEqualityComparer<TKey>) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Generic.Dictionary`2" /> начальной емкостью по умолчанию, использующий указанную функцию сравнения <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.
    /// </summary>
    /// <param name="comparer">
    ///   Реализация <see cref="T:System.Collections.Generic.IEqualityComparer`1" />, которую следует использовать при сравнении ключей, или <see langword="null" />, если для данного типа ключа должна использоваться реализация <see cref="T:System.Collections.Generic.EqualityComparer`1" /> по умолчанию.
    /// </param>
    [__DynamicallyInvokable]
    public Dictionary(IEqualityComparer<TKey> comparer)
      : this(0, comparer)
    {
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Generic.Dictionary`2" /> заданной начальной емкостью, использующий указанную функцию сравнения <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.
    /// </summary>
    /// <param name="capacity">
    ///   Начальное количество элементов, которое может содержать коллекция <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </param>
    /// <param name="comparer">
    ///   Реализация <see cref="T:System.Collections.Generic.IEqualityComparer`1" />, которую следует использовать при сравнении ключей, или <see langword="null" />, если для данного типа ключа должна использоваться реализация <see cref="T:System.Collections.Generic.EqualityComparer`1" /> по умолчанию.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше 0.
    /// </exception>
    [__DynamicallyInvokable]
    public Dictionary(int capacity, IEqualityComparer<TKey> comparer)
    {
      if (capacity < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
      if (capacity > 0)
        this.Initialize(capacity);
      this.comparer = comparer ?? (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Generic.Dictionary`2" />, который содержит элементы, скопированные из заданной коллекции <see cref="T:System.Collections.Generic.IDictionary`2" />, и использует функцию сравнения по умолчанию, проверяющую равенство для данного типа ключа.
    /// </summary>
    /// <param name="dictionary">
    ///   Объект <see cref="T:System.Collections.Generic.IDictionary`2" />, элементы которого копируются в новый объект <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="dictionary" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="dictionary" /> содержит один или несколько ключей-дубликатов.
    /// </exception>
    [__DynamicallyInvokable]
    public Dictionary(IDictionary<TKey, TValue> dictionary)
      : this(dictionary, (IEqualityComparer<TKey>) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Generic.Dictionary`2" />, который содержит элементы, скопированные из заданной коллекции <see cref="T:System.Collections.Generic.IDictionary`2" />, и использует указанный интерфейс <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.
    /// </summary>
    /// <param name="dictionary">
    ///   Объект <see cref="T:System.Collections.Generic.IDictionary`2" />, элементы которого копируются в новый объект <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </param>
    /// <param name="comparer">
    ///   Реализация <see cref="T:System.Collections.Generic.IEqualityComparer`1" />, которую следует использовать при сравнении ключей, или <see langword="null" />, если для данного типа ключа должна использоваться реализация <see cref="T:System.Collections.Generic.EqualityComparer`1" /> по умолчанию.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="dictionary" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="dictionary" /> содержит один или несколько ключей-дубликатов.
    /// </exception>
    [__DynamicallyInvokable]
    public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
      : this(dictionary != null ? dictionary.Count : 0, comparer)
    {
      if (dictionary == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
      foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) dictionary)
        this.Add(keyValuePair.Key, keyValuePair.Value);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Generic.Dictionary`2" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, который содержит сведения, требуемые для сериализации коллекции <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </param>
    /// <param name="context">
    ///   Структура <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащая исходный объект и объект назначения для сериализованного потока, связанного с коллекцией <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </param>
    protected Dictionary(SerializationInfo info, StreamingContext context)
    {
      HashHelpers.SerializationInfoTable.Add((object) this, info);
    }

    /// <summary>
    ///   Возвращает интерфейс <see cref="T:System.Collections.Generic.IEqualityComparer`1" />, используемый для установления равенства ключей словаря.
    /// </summary>
    /// <returns>
    ///   Реализация универсального интерфейса <see cref="T:System.Collections.Generic.IEqualityComparer`1" />, используемая для установления равенства ключей текущего словаря <see cref="T:System.Collections.Generic.Dictionary`2" /> и для задания хэш-кодов ключей.
    /// </returns>
    [__DynamicallyInvokable]
    public IEqualityComparer<TKey> Comparer
    {
      [__DynamicallyInvokable] get
      {
        return this.comparer;
      }
    }

    /// <summary>
    ///   Возвращает число пар "ключ-значение", содержащихся в словаре <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </summary>
    /// <returns>
    ///   Число пар "ключ-значение", содержащихся в словаре <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this.count - this.freeCount;
      }
    }

    /// <summary>
    ///   Возвращает коллекцию, содержащую ключи из словаря <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </summary>
    /// <returns>
    ///   Интерфейс <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, содержащий ключи из <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public Dictionary<TKey, TValue>.KeyCollection Keys
    {
      [__DynamicallyInvokable] get
      {
        if (this.keys == null)
          this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
        return this.keys;
      }
    }

    [__DynamicallyInvokable]
    ICollection<TKey> IDictionary<TKey, TValue>.Keys
    {
      [__DynamicallyInvokable] get
      {
        if (this.keys == null)
          this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
        return (ICollection<TKey>) this.keys;
      }
    }

    [__DynamicallyInvokable]
    IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
    {
      [__DynamicallyInvokable] get
      {
        if (this.keys == null)
          this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
        return (IEnumerable<TKey>) this.keys;
      }
    }

    /// <summary>
    ///   Возвращает коллекцию, содержащую значения из словаря <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </summary>
    /// <returns>
    ///   Коллекция <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />, содержащая значения из словаря <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public Dictionary<TKey, TValue>.ValueCollection Values
    {
      [__DynamicallyInvokable] get
      {
        if (this.values == null)
          this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
        return this.values;
      }
    }

    [__DynamicallyInvokable]
    ICollection<TValue> IDictionary<TKey, TValue>.Values
    {
      [__DynamicallyInvokable] get
      {
        if (this.values == null)
          this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
        return (ICollection<TValue>) this.values;
      }
    }

    [__DynamicallyInvokable]
    IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
    {
      [__DynamicallyInvokable] get
      {
        if (this.values == null)
          this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
        return (IEnumerable<TValue>) this.values;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, связанное с указанным ключом.
    /// </summary>
    /// <param name="key">
    ///   Ключ, значение которого требуется получить или задать.
    /// </param>
    /// <returns>
    ///   Значение, связанное с указанным ключом.
    ///    Если указанный ключ не найден, операция получения создает исключение <see cref="T:System.Collections.Generic.KeyNotFoundException" />, а операция задания значения создает новый элемент с указанным ключом.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
    ///   Свойство получено, а <paramref name="key" /> не существует в коллекции.
    /// </exception>
    [__DynamicallyInvokable]
    public TValue this[TKey key]
    {
      [__DynamicallyInvokable] get
      {
        int entry = this.FindEntry(key);
        if (entry >= 0)
          return this.entries[entry].value;
        ThrowHelper.ThrowKeyNotFoundException();
        return default (TValue);
      }
      [__DynamicallyInvokable] set
      {
        this.Insert(key, value, false);
      }
    }

    /// <summary>Добавляет указанные ключ и значение в словарь.</summary>
    /// <param name="key">Ключ добавляемого элемента.</param>
    /// <param name="value">
    ///   Добавляемое значение элемента.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент с таким ключом уже существует в <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void Add(TKey key, TValue value)
    {
      this.Insert(key, value, true);
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
    {
      this.Add(keyValuePair.Key, keyValuePair.Value);
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
    {
      int entry = this.FindEntry(keyValuePair.Key);
      return entry >= 0 && EqualityComparer<TValue>.Default.Equals(this.entries[entry].value, keyValuePair.Value);
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
    {
      int entry = this.FindEntry(keyValuePair.Key);
      if (entry < 0 || !EqualityComparer<TValue>.Default.Equals(this.entries[entry].value, keyValuePair.Value))
        return false;
      this.Remove(keyValuePair.Key);
      return true;
    }

    /// <summary>
    ///   Удаляет все ключи и значения из словаря <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Clear()
    {
      if (this.count <= 0)
        return;
      for (int index = 0; index < this.buckets.Length; ++index)
        this.buckets[index] = -1;
      Array.Clear((Array) this.entries, 0, this.count);
      this.freeList = -1;
      this.count = 0;
      this.freeCount = 0;
      ++this.version;
    }

    /// <summary>
    ///   Определяет, содержится ли указанный ключ в словаре <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ, который требуется найти в <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если <see cref="T:System.Collections.Generic.Dictionary`2" /> содержит элемент с указанным ключом, в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool ContainsKey(TKey key)
    {
      return this.FindEntry(key) >= 0;
    }

    /// <summary>
    ///   Определяет, содержит ли коллекция <see cref="T:System.Collections.Generic.Dictionary`2" /> указанное значение.
    /// </summary>
    /// <param name="value">
    ///   Значение, которое требуется найти в словаре <see cref="T:System.Collections.Generic.Dictionary`2" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Collections.Generic.Dictionary`2" /> содержит элемент с указанным значением; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool ContainsValue(TValue value)
    {
      if ((object) value == null)
      {
        for (int index = 0; index < this.count; ++index)
        {
          if (this.entries[index].hashCode >= 0 && (object) this.entries[index].value == null)
            return true;
        }
      }
      else
      {
        EqualityComparer<TValue> equalityComparer = EqualityComparer<TValue>.Default;
        for (int index = 0; index < this.count; ++index)
        {
          if (this.entries[index].hashCode >= 0 && equalityComparer.Equals(this.entries[index].value, value))
            return true;
        }
      }
      return false;
    }

    private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
    {
      if (array == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
      if (index < 0 || index > array.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (array.Length - index < this.Count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      int count = this.count;
      Dictionary<TKey, TValue>.Entry[] entries = this.entries;
      for (int index1 = 0; index1 < count; ++index1)
      {
        if (entries[index1].hashCode >= 0)
          array[index++] = new KeyValuePair<TKey, TValue>(entries[index1].key, entries[index1].value);
      }
    }

    /// <summary>
    ///   Возвращает перечислитель, осуществляющий перебор элементов списка <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </summary>
    /// <returns>
    ///   Структура <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator" /> для <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
    {
      return new Dictionary<TKey, TValue>.Enumerator(this, 2);
    }

    [__DynamicallyInvokable]
    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<TKey, TValue>>) new Dictionary<TKey, TValue>.Enumerator(this, 2);
    }

    /// <summary>
    ///   Реализует интерфейс <see cref="T:System.Runtime.Serialization.ISerializable" /> и возвращает данные, необходимые для сериализации экземпляра <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, который содержит сведения, требуемые для сериализации экземпляра <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </param>
    /// <param name="context">
    ///   Структура <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащая исходный и конечный объекты сериализованного потока, связанного с экземпляром <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
      info.AddValue("Version", this.version);
      info.AddValue("Comparer", HashHelpers.GetEqualityComparerForSerialization((object) this.comparer), typeof (IEqualityComparer<TKey>));
      info.AddValue("HashSize", this.buckets == null ? 0 : this.buckets.Length);
      if (this.buckets == null)
        return;
      KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this.Count];
      this.CopyTo(array, 0);
      info.AddValue("KeyValuePairs", (object) array, typeof (KeyValuePair<TKey, TValue>[]));
    }

    private int FindEntry(TKey key)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      if (this.buckets != null)
      {
        int num = this.comparer.GetHashCode(key) & int.MaxValue;
        for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next)
        {
          if (this.entries[index].hashCode == num && this.comparer.Equals(this.entries[index].key, key))
            return index;
        }
      }
      return -1;
    }

    private void Initialize(int capacity)
    {
      int prime = HashHelpers.GetPrime(capacity);
      this.buckets = new int[prime];
      for (int index = 0; index < this.buckets.Length; ++index)
        this.buckets[index] = -1;
      this.entries = new Dictionary<TKey, TValue>.Entry[prime];
      this.freeList = -1;
    }

    private void Insert(TKey key, TValue value, bool add)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      if (this.buckets == null)
        this.Initialize(0);
      int num1 = this.comparer.GetHashCode(key) & int.MaxValue;
      int index1 = num1 % this.buckets.Length;
      int num2 = 0;
      for (int index2 = this.buckets[index1]; index2 >= 0; index2 = this.entries[index2].next)
      {
        if (this.entries[index2].hashCode == num1 && this.comparer.Equals(this.entries[index2].key, key))
        {
          if (add)
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
          this.entries[index2].value = value;
          ++this.version;
          return;
        }
        ++num2;
      }
      int index3;
      if (this.freeCount > 0)
      {
        index3 = this.freeList;
        this.freeList = this.entries[index3].next;
        --this.freeCount;
      }
      else
      {
        if (this.count == this.entries.Length)
        {
          this.Resize();
          index1 = num1 % this.buckets.Length;
        }
        index3 = this.count;
        ++this.count;
      }
      this.entries[index3].hashCode = num1;
      this.entries[index3].next = this.buckets[index1];
      this.entries[index3].key = key;
      this.entries[index3].value = value;
      this.buckets[index1] = index3;
      ++this.version;
      if (num2 <= 100 || !HashHelpers.IsWellKnownEqualityComparer((object) this.comparer))
        return;
      this.comparer = (IEqualityComparer<TKey>) HashHelpers.GetRandomizedEqualityComparer((object) this.comparer);
      this.Resize(this.entries.Length, true);
    }

    /// <summary>
    ///   Реализует интерфейс <see cref="T:System.Runtime.Serialization.ISerializable" /> и вызывает событие десериализации при завершении десериализации.
    /// </summary>
    /// <param name="sender">Источник события десериализации.</param>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <see cref="T:System.Runtime.Serialization.SerializationInfo" /> Объект, связанный с текущим <see cref="T:System.Collections.Generic.Dictionary`2" /> экземпляр является недопустимым.
    /// </exception>
    public virtual void OnDeserialization(object sender)
    {
      SerializationInfo serializationInfo;
      HashHelpers.SerializationInfoTable.TryGetValue((object) this, out serializationInfo);
      if (serializationInfo == null)
        return;
      int int32_1 = serializationInfo.GetInt32("Version");
      int int32_2 = serializationInfo.GetInt32("HashSize");
      this.comparer = (IEqualityComparer<TKey>) serializationInfo.GetValue("Comparer", typeof (IEqualityComparer<TKey>));
      if (int32_2 != 0)
      {
        this.buckets = new int[int32_2];
        for (int index = 0; index < this.buckets.Length; ++index)
          this.buckets[index] = -1;
        this.entries = new Dictionary<TKey, TValue>.Entry[int32_2];
        this.freeList = -1;
        KeyValuePair<TKey, TValue>[] keyValuePairArray = (KeyValuePair<TKey, TValue>[]) serializationInfo.GetValue("KeyValuePairs", typeof (KeyValuePair<TKey, TValue>[]));
        if (keyValuePairArray == null)
          ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
        for (int index = 0; index < keyValuePairArray.Length; ++index)
        {
          if ((object) keyValuePairArray[index].Key == null)
            ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_NullKey);
          this.Insert(keyValuePairArray[index].Key, keyValuePairArray[index].Value, true);
        }
      }
      else
        this.buckets = (int[]) null;
      this.version = int32_1;
      HashHelpers.SerializationInfoTable.Remove((object) this);
    }

    private void Resize()
    {
      this.Resize(HashHelpers.ExpandPrime(this.count), false);
    }

    private void Resize(int newSize, bool forceNewHashCodes)
    {
      int[] numArray = new int[newSize];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = -1;
      Dictionary<TKey, TValue>.Entry[] entryArray = new Dictionary<TKey, TValue>.Entry[newSize];
      Array.Copy((Array) this.entries, 0, (Array) entryArray, 0, this.count);
      if (forceNewHashCodes)
      {
        for (int index = 0; index < this.count; ++index)
        {
          if (entryArray[index].hashCode != -1)
            entryArray[index].hashCode = this.comparer.GetHashCode(entryArray[index].key) & int.MaxValue;
        }
      }
      for (int index1 = 0; index1 < this.count; ++index1)
      {
        if (entryArray[index1].hashCode >= 0)
        {
          int index2 = entryArray[index1].hashCode % newSize;
          entryArray[index1].next = numArray[index2];
          numArray[index2] = index1;
        }
      }
      this.buckets = numArray;
      this.entries = entryArray;
    }

    /// <summary>
    ///   Удаляет значение с указанным ключом из <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется удалить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если элемент был найден и удален; в противном случае — значение <see langword="false" />.
    ///     Этот метод возвращает значение <see langword="false" />, если ключ <paramref name="key" /> не удалось найти в <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Remove(TKey key)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      if (this.buckets != null)
      {
        int num = this.comparer.GetHashCode(key) & int.MaxValue;
        int index1 = num % this.buckets.Length;
        int index2 = -1;
        for (int index3 = this.buckets[index1]; index3 >= 0; index3 = this.entries[index3].next)
        {
          if (this.entries[index3].hashCode == num && this.comparer.Equals(this.entries[index3].key, key))
          {
            if (index2 < 0)
              this.buckets[index1] = this.entries[index3].next;
            else
              this.entries[index2].next = this.entries[index3].next;
            this.entries[index3].hashCode = -1;
            this.entries[index3].next = this.freeList;
            this.entries[index3].key = default (TKey);
            this.entries[index3].value = default (TValue);
            this.freeList = index3;
            ++this.freeCount;
            ++this.version;
            return true;
          }
          index2 = index3;
        }
      }
      return false;
    }

    /// <summary>Возвращает значение, связанное с заданным ключом.</summary>
    /// <param name="key">
    ///   Ключ значения, которое необходимо получить.
    /// </param>
    /// <param name="value">
    ///   Этот метод возвращает значение, связанное с указанным ключом, если он найден; в противном случае — значение по умолчанию для типа параметра <paramref name="value" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если <see cref="T:System.Collections.Generic.Dictionary`2" /> содержит элемент с указанным ключом, в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool TryGetValue(TKey key, out TValue value)
    {
      int entry = this.FindEntry(key);
      if (entry >= 0)
      {
        value = this.entries[entry].value;
        return true;
      }
      value = default (TValue);
      return false;
    }

    internal TValue GetValueOrDefault(TKey key)
    {
      int entry = this.FindEntry(key);
      if (entry >= 0)
        return this.entries[entry].value;
      return default (TValue);
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
    {
      this.CopyTo(array, index);
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
        this.CopyTo(array1, index);
      else if (array is DictionaryEntry[])
      {
        DictionaryEntry[] dictionaryEntryArray = array as DictionaryEntry[];
        Dictionary<TKey, TValue>.Entry[] entries = this.entries;
        for (int index1 = 0; index1 < this.count; ++index1)
        {
          if (entries[index1].hashCode >= 0)
            dictionaryEntryArray[index++] = new DictionaryEntry((object) entries[index1].key, (object) entries[index1].value);
        }
      }
      else
      {
        object[] objArray = array as object[];
        if (objArray == null)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        try
        {
          int count = this.count;
          Dictionary<TKey, TValue>.Entry[] entries = this.entries;
          for (int index1 = 0; index1 < count; ++index1)
          {
            if (entries[index1].hashCode >= 0)
              objArray[index++] = (object) new KeyValuePair<TKey, TValue>(entries[index1].key, entries[index1].value);
          }
        }
        catch (ArrayTypeMismatchException ex)
        {
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        }
      }
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new Dictionary<TKey, TValue>.Enumerator(this, 2);
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
        if (this._syncRoot == null)
          Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    [__DynamicallyInvokable]
    bool IDictionary.IsFixedSize
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    bool IDictionary.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return false;
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
        if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
        {
          int entry = this.FindEntry((TKey) key);
          if (entry >= 0)
            return (object) this.entries[entry].value;
        }
        return (object) null;
      }
      [__DynamicallyInvokable] set
      {
        if (key == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
        ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
        try
        {
          TKey index = (TKey) key;
          try
          {
            this[index] = (TValue) value;
          }
          catch (InvalidCastException ex)
          {
            ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (TValue));
          }
        }
        catch (InvalidCastException ex)
        {
          ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof (TKey));
        }
      }
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
      if (key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
      try
      {
        TKey key1 = (TKey) key;
        try
        {
          this.Add(key1, (TValue) value);
        }
        catch (InvalidCastException ex)
        {
          ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (TValue));
        }
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof (TKey));
      }
    }

    [__DynamicallyInvokable]
    bool IDictionary.Contains(object key)
    {
      if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
        return this.ContainsKey((TKey) key);
      return false;
    }

    [__DynamicallyInvokable]
    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      return (IDictionaryEnumerator) new Dictionary<TKey, TValue>.Enumerator(this, 1);
    }

    [__DynamicallyInvokable]
    void IDictionary.Remove(object key)
    {
      if (!Dictionary<TKey, TValue>.IsCompatibleKey(key))
        return;
      this.Remove((TKey) key);
    }

    private struct Entry
    {
      public int hashCode;
      public int next;
      public TKey key;
      public TValue value;
    }

    /// <summary>
    ///   Выполняет перечисление элементов коллекции <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </summary>
    [__DynamicallyInvokable]
    [Serializable]
    public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
    {
      private Dictionary<TKey, TValue> dictionary;
      private int version;
      private int index;
      private KeyValuePair<TKey, TValue> current;
      private int getEnumeratorRetType;
      internal const int DictEntry = 1;
      internal const int KeyValuePair = 2;

      internal Enumerator(Dictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
      {
        this.dictionary = dictionary;
        this.version = dictionary.version;
        this.index = 0;
        this.getEnumeratorRetType = getEnumeratorRetType;
        this.current = new KeyValuePair<TKey, TValue>();
      }

      /// <summary>
      ///   Перемещает перечислитель к следующему элементу коллекции <see cref="T:System.Collections.Generic.Dictionary`2" />.
      /// </summary>
      /// <returns>
      ///   Значение <see langword="true" />, если перечислитель был успешно перемещен к следующему элементу; значение <see langword="false" />, если перечислитель достиг конца коллекции.
      /// </returns>
      /// <exception cref="T:System.InvalidOperationException">
      ///   Коллекция была изменена после создания перечислителя.
      /// </exception>
      [__DynamicallyInvokable]
      public bool MoveNext()
      {
        if (this.version != this.dictionary.version)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
        for (; (uint) this.index < (uint) this.dictionary.count; ++this.index)
        {
          if (this.dictionary.entries[this.index].hashCode >= 0)
          {
            this.current = new KeyValuePair<TKey, TValue>(this.dictionary.entries[this.index].key, this.dictionary.entries[this.index].value);
            ++this.index;
            return true;
          }
        }
        this.index = this.dictionary.count + 1;
        this.current = new KeyValuePair<TKey, TValue>();
        return false;
      }

      /// <summary>
      ///   Возвращает элемент, расположенный в текущей позиции перечислителя.
      /// </summary>
      /// <returns>
      ///   Элемент коллекции <see cref="T:System.Collections.Generic.Dictionary`2" />, находящийся в текущей позиции перечислителя.
      /// </returns>
      [__DynamicallyInvokable]
      public KeyValuePair<TKey, TValue> Current
      {
        [__DynamicallyInvokable] get
        {
          return this.current;
        }
      }

      /// <summary>
      ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator" />.
      /// </summary>
      [__DynamicallyInvokable]
      public void Dispose()
      {
      }

      [__DynamicallyInvokable]
      object IEnumerator.Current
      {
        [__DynamicallyInvokable] get
        {
          if (this.index == 0 || this.index == this.dictionary.count + 1)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
          if (this.getEnumeratorRetType == 1)
            return (object) new DictionaryEntry((object) this.current.Key, (object) this.current.Value);
          return (object) new KeyValuePair<TKey, TValue>(this.current.Key, this.current.Value);
        }
      }

      [__DynamicallyInvokable]
      void IEnumerator.Reset()
      {
        if (this.version != this.dictionary.version)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
        this.index = 0;
        this.current = new KeyValuePair<TKey, TValue>();
      }

      [__DynamicallyInvokable]
      DictionaryEntry IDictionaryEnumerator.Entry
      {
        [__DynamicallyInvokable] get
        {
          if (this.index == 0 || this.index == this.dictionary.count + 1)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
          return new DictionaryEntry((object) this.current.Key, (object) this.current.Value);
        }
      }

      [__DynamicallyInvokable]
      object IDictionaryEnumerator.Key
      {
        [__DynamicallyInvokable] get
        {
          if (this.index == 0 || this.index == this.dictionary.count + 1)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
          return (object) this.current.Key;
        }
      }

      [__DynamicallyInvokable]
      object IDictionaryEnumerator.Value
      {
        [__DynamicallyInvokable] get
        {
          if (this.index == 0 || this.index == this.dictionary.count + 1)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
          return (object) this.current.Value;
        }
      }
    }

    /// <summary>
    ///   Представляет коллекцию ключей в <see cref="T:System.Collections.Generic.Dictionary`2" />.
    ///    Этот класс не наследуется.
    /// </summary>
    [DebuggerTypeProxy(typeof (Mscorlib_DictionaryKeyCollectionDebugView<,>))]
    [DebuggerDisplay("Count = {Count}")]
    [__DynamicallyInvokable]
    [Serializable]
    public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
    {
      private Dictionary<TKey, TValue> dictionary;

      /// <summary>
      ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> класс, который отражает ключи в указанном <see cref="T:System.Collections.Generic.Dictionary`2" />.
      /// </summary>
      /// <param name="dictionary">
      ///   <see cref="T:System.Collections.Generic.Dictionary`2" /> Ключи которых отражаются в новом <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.
      /// </param>
      /// <exception cref="T:System.ArgumentNullException">
      ///   Свойство <paramref name="dictionary" /> имеет значение <see langword="null" />.
      /// </exception>
      [__DynamicallyInvokable]
      public KeyCollection(Dictionary<TKey, TValue> dictionary)
      {
        if (dictionary == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
        this.dictionary = dictionary;
      }

      /// <summary>
      ///   Возвращает перечислитель, осуществляющий перебор элементов списка <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.
      /// </summary>
      /// <returns>
      ///   Новый объект <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator" /> для <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.
      /// </returns>
      [__DynamicallyInvokable]
      public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
      {
        return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
      }

      /// <summary>
      ///   Копирует элементы коллекции <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> в существующий одномерный массив <see cref="T:System.Array" />, начиная с указанного значения индекса массива.
      /// </summary>
      /// <param name="array">
      ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.
      ///    Массив <see cref="T:System.Array" /> должен иметь индексацию, начинающуюся с нуля.
      /// </param>
      /// <param name="index">
      ///   Отсчитываемый от нуля индекс в массиве <paramref name="array" />, указывающий начало копирования.
      /// </param>
      /// <exception cref="T:System.ArgumentNullException">
      ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
      /// </exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      ///   Значение параметра <paramref name="index" /> меньше нуля.
      /// </exception>
      /// <exception cref="T:System.ArgumentException">
      ///   Количество элементов в исходной коллекции <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> больше, чем свободное пространство от <paramref name="index" /> до конца массива назначения <paramref name="array" />.
      /// </exception>
      [__DynamicallyInvokable]
      public void CopyTo(TKey[] array, int index)
      {
        if (array == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
        if (index < 0 || index > array.Length)
          ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
        if (array.Length - index < this.dictionary.Count)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
        int count = this.dictionary.count;
        Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
        for (int index1 = 0; index1 < count; ++index1)
        {
          if (entries[index1].hashCode >= 0)
            array[index++] = entries[index1].key;
        }
      }

      /// <summary>
      ///   Получает число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.
      /// </summary>
      /// <returns>
      ///   Число элементов, содержащихся в коллекции <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.
      /// 
      ///   Получение значения данного свойства является операцией порядка сложности O(1).
      /// </returns>
      [__DynamicallyInvokable]
      public int Count
      {
        [__DynamicallyInvokable] get
        {
          return this.dictionary.Count;
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
      void ICollection<TKey>.Add(TKey item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
      }

      [__DynamicallyInvokable]
      void ICollection<TKey>.Clear()
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
      }

      [__DynamicallyInvokable]
      bool ICollection<TKey>.Contains(TKey item)
      {
        return this.dictionary.ContainsKey(item);
      }

      [__DynamicallyInvokable]
      bool ICollection<TKey>.Remove(TKey item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
        return false;
      }

      [__DynamicallyInvokable]
      IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
      {
        return (IEnumerator<TKey>) new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
      }

      [__DynamicallyInvokable]
      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
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
        if (array.Length - index < this.dictionary.Count)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
        TKey[] array1 = array as TKey[];
        if (array1 != null)
        {
          this.CopyTo(array1, index);
        }
        else
        {
          object[] objArray = array as object[];
          if (objArray == null)
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          int count = this.dictionary.count;
          Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
          try
          {
            for (int index1 = 0; index1 < count; ++index1)
            {
              if (entries[index1].hashCode >= 0)
                objArray[index++] = (object) entries[index1].key;
            }
          }
          catch (ArrayTypeMismatchException ex)
          {
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
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
          return ((ICollection) this.dictionary).SyncRoot;
        }
      }

      /// <summary>
      ///   Выполняет перечисление элементов коллекции <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.
      /// </summary>
      [__DynamicallyInvokable]
      [Serializable]
      public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
      {
        private Dictionary<TKey, TValue> dictionary;
        private int index;
        private int version;
        private TKey currentKey;

        internal Enumerator(Dictionary<TKey, TValue> dictionary)
        {
          this.dictionary = dictionary;
          this.version = dictionary.version;
          this.index = 0;
          this.currentKey = default (TKey);
        }

        /// <summary>
        ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator" />.
        /// </summary>
        [__DynamicallyInvokable]
        public void Dispose()
        {
        }

        /// <summary>
        ///   Перемещает перечислитель к следующему элементу коллекции <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.
        /// </summary>
        /// <returns>
        ///   Значение <see langword="true" />, если перечислитель был успешно перемещен к следующему элементу; значение <see langword="false" />, если перечислитель достиг конца коллекции.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        ///   Коллекция была изменена после создания перечислителя.
        /// </exception>
        [__DynamicallyInvokable]
        public bool MoveNext()
        {
          if (this.version != this.dictionary.version)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
          for (; (uint) this.index < (uint) this.dictionary.count; ++this.index)
          {
            if (this.dictionary.entries[this.index].hashCode >= 0)
            {
              this.currentKey = this.dictionary.entries[this.index].key;
              ++this.index;
              return true;
            }
          }
          this.index = this.dictionary.count + 1;
          this.currentKey = default (TKey);
          return false;
        }

        /// <summary>
        ///   Возвращает элемент, расположенный в текущей позиции перечислителя.
        /// </summary>
        /// <returns>
        ///   Элемент коллекции <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, находящийся в текущей позиции перечислителя.
        /// </returns>
        [__DynamicallyInvokable]
        public TKey Current
        {
          [__DynamicallyInvokable] get
          {
            return this.currentKey;
          }
        }

        [__DynamicallyInvokable]
        object IEnumerator.Current
        {
          [__DynamicallyInvokable] get
          {
            if (this.index == 0 || this.index == this.dictionary.count + 1)
              ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
            return (object) this.currentKey;
          }
        }

        [__DynamicallyInvokable]
        void IEnumerator.Reset()
        {
          if (this.version != this.dictionary.version)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
          this.index = 0;
          this.currentKey = default (TKey);
        }
      }
    }

    /// <summary>
    ///   Представляет коллекцию значений в <see cref="T:System.Collections.Generic.Dictionary`2" />.
    ///    Этот класс не наследуется.
    /// </summary>
    [DebuggerTypeProxy(typeof (Mscorlib_DictionaryValueCollectionDebugView<,>))]
    [DebuggerDisplay("Count = {Count}")]
    [__DynamicallyInvokable]
    [Serializable]
    public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
    {
      private Dictionary<TKey, TValue> dictionary;

      /// <summary>
      ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> класс, который отражает значения в указанном <see cref="T:System.Collections.Generic.Dictionary`2" />.
      /// </summary>
      /// <param name="dictionary">
      ///   <see cref="T:System.Collections.Generic.Dictionary`2" /> Значения которых отражаются в новом <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.
      /// </param>
      /// <exception cref="T:System.ArgumentNullException">
      ///   Свойство <paramref name="dictionary" /> имеет значение <see langword="null" />.
      /// </exception>
      [__DynamicallyInvokable]
      public ValueCollection(Dictionary<TKey, TValue> dictionary)
      {
        if (dictionary == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
        this.dictionary = dictionary;
      }

      /// <summary>
      ///   Возвращает перечислитель, осуществляющий перебор элементов списка <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.
      /// </summary>
      /// <returns>
      ///   Новый объект <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator" /> для <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.
      /// </returns>
      [__DynamicallyInvokable]
      public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
      {
        return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
      }

      /// <summary>
      ///   Копирует элементы коллекции <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> в существующий одномерный массив <see cref="T:System.Array" />, начиная с указанного значения индекса массива.
      /// </summary>
      /// <param name="array">
      ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.
      ///    Массив <see cref="T:System.Array" /> должен иметь индексацию, начинающуюся с нуля.
      /// </param>
      /// <param name="index">
      ///   Отсчитываемый от нуля индекс в массиве <paramref name="array" />, указывающий начало копирования.
      /// </param>
      /// <exception cref="T:System.ArgumentNullException">
      ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
      /// </exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      ///   Значение параметра <paramref name="index" /> меньше нуля.
      /// </exception>
      /// <exception cref="T:System.ArgumentException">
      ///   Число элементов в исходном массиве <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> больше доступного места от положения, заданного значением параметра <paramref name="index" />, до конца массива назначения <paramref name="array" />.
      /// </exception>
      [__DynamicallyInvokable]
      public void CopyTo(TValue[] array, int index)
      {
        if (array == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
        if (index < 0 || index > array.Length)
          ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
        if (array.Length - index < this.dictionary.Count)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
        int count = this.dictionary.count;
        Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
        for (int index1 = 0; index1 < count; ++index1)
        {
          if (entries[index1].hashCode >= 0)
            array[index++] = entries[index1].value;
        }
      }

      /// <summary>
      ///   Получает число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.
      /// </summary>
      /// <returns>
      ///   Число элементов, содержащихся в коллекции <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.
      /// </returns>
      [__DynamicallyInvokable]
      public int Count
      {
        [__DynamicallyInvokable] get
        {
          return this.dictionary.Count;
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
      void ICollection<TValue>.Add(TValue item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
      }

      [__DynamicallyInvokable]
      bool ICollection<TValue>.Remove(TValue item)
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
        return false;
      }

      [__DynamicallyInvokable]
      void ICollection<TValue>.Clear()
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
      }

      [__DynamicallyInvokable]
      bool ICollection<TValue>.Contains(TValue item)
      {
        return this.dictionary.ContainsValue(item);
      }

      [__DynamicallyInvokable]
      IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
      {
        return (IEnumerator<TValue>) new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
      }

      [__DynamicallyInvokable]
      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
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
        if (array.Length - index < this.dictionary.Count)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
        TValue[] array1 = array as TValue[];
        if (array1 != null)
        {
          this.CopyTo(array1, index);
        }
        else
        {
          object[] objArray = array as object[];
          if (objArray == null)
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          int count = this.dictionary.count;
          Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
          try
          {
            for (int index1 = 0; index1 < count; ++index1)
            {
              if (entries[index1].hashCode >= 0)
                objArray[index++] = (object) entries[index1].value;
            }
          }
          catch (ArrayTypeMismatchException ex)
          {
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
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
          return ((ICollection) this.dictionary).SyncRoot;
        }
      }

      /// <summary>
      ///   Выполняет перечисление элементов коллекции <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.
      /// </summary>
      [__DynamicallyInvokable]
      [Serializable]
      public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
      {
        private Dictionary<TKey, TValue> dictionary;
        private int index;
        private int version;
        private TValue currentValue;

        internal Enumerator(Dictionary<TKey, TValue> dictionary)
        {
          this.dictionary = dictionary;
          this.version = dictionary.version;
          this.index = 0;
          this.currentValue = default (TValue);
        }

        /// <summary>
        ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator" />.
        /// </summary>
        [__DynamicallyInvokable]
        public void Dispose()
        {
        }

        /// <summary>
        ///   Перемещает перечислитель к следующему элементу коллекции <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.
        /// </summary>
        /// <returns>
        ///   Значение <see langword="true" />, если перечислитель был успешно перемещен к следующему элементу; значение <see langword="false" />, если перечислитель достиг конца коллекции.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        ///   Коллекция была изменена после создания перечислителя.
        /// </exception>
        [__DynamicallyInvokable]
        public bool MoveNext()
        {
          if (this.version != this.dictionary.version)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
          for (; (uint) this.index < (uint) this.dictionary.count; ++this.index)
          {
            if (this.dictionary.entries[this.index].hashCode >= 0)
            {
              this.currentValue = this.dictionary.entries[this.index].value;
              ++this.index;
              return true;
            }
          }
          this.index = this.dictionary.count + 1;
          this.currentValue = default (TValue);
          return false;
        }

        /// <summary>
        ///   Возвращает элемент, расположенный в текущей позиции перечислителя.
        /// </summary>
        /// <returns>
        ///   Элемент коллекции <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />, находящийся в текущей позиции перечислителя.
        /// </returns>
        [__DynamicallyInvokable]
        public TValue Current
        {
          [__DynamicallyInvokable] get
          {
            return this.currentValue;
          }
        }

        [__DynamicallyInvokable]
        object IEnumerator.Current
        {
          [__DynamicallyInvokable] get
          {
            if (this.index == 0 || this.index == this.dictionary.count + 1)
              ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
            return (object) this.currentValue;
          }
        }

        [__DynamicallyInvokable]
        void IEnumerator.Reset()
        {
          if (this.version != this.dictionary.version)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
          this.index = 0;
          this.currentValue = default (TValue);
        }
      }
    }
  }
}
