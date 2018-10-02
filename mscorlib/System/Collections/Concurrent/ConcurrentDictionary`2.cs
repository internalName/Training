// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.ConcurrentDictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
  /// <summary>
  ///   Представляет потокобезопасную коллекцию пар "ключ-значение", доступ к которой могут одновременно получать несколько потоков.
  /// </summary>
  /// <typeparam name="TKey">Тип ключей в словаре.</typeparam>
  /// <typeparam name="TValue">Тип значений в словаре.</typeparam>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (Mscorlib_DictionaryDebugView<,>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
  {
    private static readonly bool s_isValueWriteAtomic = ConcurrentDictionary<TKey, TValue>.IsValueWriteAtomic();
    [NonSerialized]
    private volatile ConcurrentDictionary<TKey, TValue>.Tables m_tables;
    internal IEqualityComparer<TKey> m_comparer;
    [NonSerialized]
    private readonly bool m_growLockArray;
    [OptionalField]
    private int m_keyRehashCount;
    [NonSerialized]
    private int m_budget;
    private KeyValuePair<TKey, TValue>[] m_serializationArray;
    private int m_serializationConcurrencyLevel;
    private int m_serializationCapacity;
    private const int DEFAULT_CAPACITY = 31;
    private const int MAX_LOCK_NUMBER = 1024;

    private static bool IsValueWriteAtomic()
    {
      Type type = typeof (TValue);
      if (type.IsClass)
        return true;
      switch (Type.GetTypeCode(type))
      {
        case TypeCode.Boolean:
        case TypeCode.Char:
        case TypeCode.SByte:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.UInt16:
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Single:
          return true;
        case TypeCode.Int64:
        case TypeCode.UInt64:
        case TypeCode.Double:
          return IntPtr.Size == 8;
        default:
          return false;
      }
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, который обладает уровнем параллелизма и начальной емкостью по умолчанию, а также использует для типа ключа функцию сравнения по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public ConcurrentDictionary()
      : this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default)
    {
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, который обладает указанными уровнем параллелизма и емкостью, а также использует для типа ключей функцию сравнения по умолчанию.
    /// </summary>
    /// <param name="concurrencyLevel">
    ///   Предполагаемое количество потоков, которые будут параллельно обновлять коллекцию <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </param>
    /// <param name="capacity">
    ///   Начальное количество элементов, которое может содержать коллекция <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="concurrencyLevel" /> меньше 1.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="capacity" /> меньше 0.
    /// </exception>
    [__DynamicallyInvokable]
    public ConcurrentDictionary(int concurrencyLevel, int capacity)
      : this(concurrencyLevel, capacity, false, (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, который содержит элементы, скопированные из указанной коллекции <see cref="T:System.Collections.Generic.IEnumerable`1" />, обладает уровнем параллелизма и начальной емкостью по умолчанию, а также использует для типа ключа функцию сравнения по умолчанию.
    /// </summary>
    /// <param name="collection">
    ///   Объект <see cref="T:System.Collections.Generic.IEnumerable`1" />, элементы которого копируются в новый объект <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="collection" /> или любой из его ключей  <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="collection" /> содержит один или несколько повторяющихся ключей.
    /// </exception>
    [__DynamicallyInvokable]
    public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
      : this(collection, (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default)
    {
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, который имеет уровень параллелизма и емкость по умолчанию, а также использует указанный интерфейс <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.
    /// </summary>
    /// <param name="comparer">
    ///   Реализация сравнения равенства, используемая при сравнении ключей.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="comparer" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public ConcurrentDictionary(IEqualityComparer<TKey> comparer)
      : this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, comparer)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, который содержит элементы, скопированные из указанной коллекции <see cref="T:System.Collections.IEnumerable" />, обладает уровнем параллелизма по умолчанию, начальной емкостью по умолчанию, а также использует заданный интерфейс <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.
    /// </summary>
    /// <param name="collection">
    ///   Объект <see cref="T:System.Collections.Generic.IEnumerable`1" />, элементы которого копируются в новый объект <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </param>
    /// <param name="comparer">
    ///   Реализация интерфейса <see cref="T:System.Collections.Generic.IEqualityComparer`1" />, используемая при сравнении ключей.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="collection" /> или <paramref name="comparer" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
      : this(comparer)
    {
      if (collection == null)
        throw new ArgumentNullException(nameof (collection));
      this.InitializeFromCollection(collection);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, который содержит элементы, скопированные из заданной коллекции <see cref="T:System.Collections.IEnumerable" />, и использует указанный интерфейс <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.
    /// </summary>
    /// <param name="concurrencyLevel">
    ///   Предполагаемое количество потоков, которые будут параллельно обновлять коллекцию <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </param>
    /// <param name="collection">
    ///   Объект <see cref="T:System.Collections.Generic.IEnumerable`1" />, элементы которого копируются в новый объект <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </param>
    /// <param name="comparer">
    ///   Реализация интерфейса <see cref="T:System.Collections.Generic.IEqualityComparer`1" />, используемая при сравнении ключей.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="collection" /> или <paramref name="comparer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="concurrencyLevel" /> меньше 1.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="collection" /> содержит один или несколько ключей-дубликатов.
    /// </exception>
    [__DynamicallyInvokable]
    public ConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
      : this(concurrencyLevel, 31, false, comparer)
    {
      if (collection == null)
        throw new ArgumentNullException(nameof (collection));
      if (comparer == null)
        throw new ArgumentNullException(nameof (comparer));
      this.InitializeFromCollection(collection);
    }

    private void InitializeFromCollection(IEnumerable<KeyValuePair<TKey, TValue>> collection)
    {
      foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
      {
        if ((object) keyValuePair.Key == null)
          throw new ArgumentNullException("key");
        TValue resultingValue;
        if (!this.TryAddInternal(keyValuePair.Key, keyValuePair.Value, false, false, out resultingValue))
          throw new ArgumentException(this.GetResource("ConcurrentDictionary_SourceContainsDuplicateKeys"));
      }
      if (this.m_budget != 0)
        return;
      this.m_budget = this.m_tables.m_buckets.Length / this.m_tables.m_locks.Length;
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, который обладает указанными уровнем параллелизма и начальной емкостью, а также использует указанный интерфейс <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.
    /// </summary>
    /// <param name="concurrencyLevel">
    ///   Предполагаемое количество потоков, которые будут параллельно обновлять коллекцию <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </param>
    /// <param name="capacity">
    ///   Начальное количество элементов, которое может содержать коллекция <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </param>
    /// <param name="comparer">
    ///   Реализация интерфейса <see cref="T:System.Collections.Generic.IEqualityComparer`1" />, используемая при сравнении ключей.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="comparer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="concurrencyLevel" /> или <paramref name="capacity" /> меньше 1.
    /// </exception>
    [__DynamicallyInvokable]
    public ConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer)
      : this(concurrencyLevel, capacity, false, comparer)
    {
    }

    internal ConcurrentDictionary(int concurrencyLevel, int capacity, bool growLockArray, IEqualityComparer<TKey> comparer)
    {
      if (concurrencyLevel < 1)
        throw new ArgumentOutOfRangeException(nameof (concurrencyLevel), this.GetResource("ConcurrentDictionary_ConcurrencyLevelMustBePositive"));
      if (capacity < 0)
        throw new ArgumentOutOfRangeException(nameof (capacity), this.GetResource("ConcurrentDictionary_CapacityMustNotBeNegative"));
      if (comparer == null)
        throw new ArgumentNullException(nameof (comparer));
      if (capacity < concurrencyLevel)
        capacity = concurrencyLevel;
      object[] locks = new object[concurrencyLevel];
      for (int index = 0; index < locks.Length; ++index)
        locks[index] = new object();
      int[] countPerLock = new int[locks.Length];
      ConcurrentDictionary<TKey, TValue>.Node[] buckets = new ConcurrentDictionary<TKey, TValue>.Node[capacity];
      this.m_tables = new ConcurrentDictionary<TKey, TValue>.Tables(buckets, locks, countPerLock, comparer);
      this.m_growLockArray = growLockArray;
      this.m_budget = buckets.Length / locks.Length;
    }

    /// <summary>
    ///   Пытается добавить указанную пару "ключ-значение" в коллекцию <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </summary>
    /// <param name="key">Ключ добавляемого элемента.</param>
    /// <param name="value">
    ///   Добавляемое значение элемента.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если пара "ключ-значение" были добавлены в <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> успешно; значение <see langword="false" />, если ключ уже существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Словарь уже содержит максимальное количество элементов (<see cref="F:System.Int32.MaxValue" />).
    /// </exception>
    [__DynamicallyInvokable]
    public bool TryAdd(TKey key, TValue value)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      TValue resultingValue;
      return this.TryAddInternal(key, value, false, true, out resultingValue);
    }

    /// <summary>
    ///   Определяет, содержится ли указанный ключ в словаре <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ, который требуется найти в <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> содержит элемент с указанным ключом, в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool ContainsKey(TKey key)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      TValue obj;
      return this.TryGetValue(key, out obj);
    }

    /// <summary>
    ///   Пытается удалить и вернуть значение с указанным ключом из коллекции <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ удаляемого и возвращаемого элемента.
    /// </param>
    /// <param name="value">
    ///   Параметр, возвращаемый данным методом, содержит объект, удаленный из коллекции <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> или значение по умолчанию типа <see langword="TValue" />, если <paramref name="key" /> не существует.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если объект успешно удален; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool TryRemove(TKey key, out TValue value)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      return this.TryRemoveInternal(key, out value, false, default (TValue));
    }

    private bool TryRemoveInternal(TKey key, out TValue value, bool matchValue, TValue oldValue)
    {
label_0:
      ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
      IEqualityComparer<TKey> comparer = tables.m_comparer;
      int bucketNo;
      int lockNo;
      this.GetBucketAndLockNo(comparer.GetHashCode(key), out bucketNo, out lockNo, tables.m_buckets.Length, tables.m_locks.Length);
      lock (tables.m_locks[lockNo])
      {
        if (tables == this.m_tables)
        {
          ConcurrentDictionary<TKey, TValue>.Node node1 = (ConcurrentDictionary<TKey, TValue>.Node) null;
          for (ConcurrentDictionary<TKey, TValue>.Node node2 = tables.m_buckets[bucketNo]; node2 != null; node2 = node2.m_next)
          {
            if (comparer.Equals(node2.m_key, key))
            {
              if (matchValue && !EqualityComparer<TValue>.Default.Equals(oldValue, node2.m_value))
              {
                value = default (TValue);
                return false;
              }
              if (node1 == null)
                Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables.m_buckets[bucketNo], node2.m_next);
              else
                node1.m_next = node2.m_next;
              value = node2.m_value;
              --tables.m_countPerLock[lockNo];
              return true;
            }
            node1 = node2;
          }
        }
        else
          goto label_0;
      }
      value = default (TValue);
      return false;
    }

    /// <summary>
    ///   Пытается получить значение, связанное с указанным ключом, из коллекции <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ значения, которое необходимо получить.
    /// </param>
    /// <param name="value">
    ///   Параметр, возвращаемый этим методом, содержит объект из коллекции <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> с заданным ключом или значение по умолчанию, если операцию не удалось выполнить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если ключ был найден в коллекции <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool TryGetValue(TKey key, out TValue value)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
      IEqualityComparer<TKey> comparer = tables.m_comparer;
      int bucketNo;
      int lockNo;
      this.GetBucketAndLockNo(comparer.GetHashCode(key), out bucketNo, out lockNo, tables.m_buckets.Length, tables.m_locks.Length);
      for (ConcurrentDictionary<TKey, TValue>.Node node = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref tables.m_buckets[bucketNo]); node != null; node = node.m_next)
      {
        if (comparer.Equals(node.m_key, key))
        {
          value = node.m_value;
          return true;
        }
      }
      value = default (TValue);
      return false;
    }

    /// <summary>
    ///   Сравнивает существующее значение указанного ключа с заданным значением и в случае их равенства обновляет ключ третьим значением.
    /// </summary>
    /// <param name="key">
    ///   Ключ, значение которого сравнивается со значением параметра <paramref name="comparisonValue" /> и, возможно, заменяется.
    /// </param>
    /// <param name="newValue">
    ///   Значение, которым заменяется значение элемента, который задал ключ <paramref name="key" /> в случае положительного результата сравнения на равенство.
    /// </param>
    /// <param name="comparisonValue">
    ///   Значение, которое сравнивается со значением элемента с указанным ключом <paramref name="key" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение с ключом <paramref name="key" /> оказалось равным значению параметра <paramref name="comparisonValue" /> и было заменено значением <paramref name="newValue" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      IEqualityComparer<TValue> equalityComparer = (IEqualityComparer<TValue>) EqualityComparer<TValue>.Default;
label_3:
      ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
      IEqualityComparer<TKey> comparer = tables.m_comparer;
      int hashCode = comparer.GetHashCode(key);
      int bucketNo;
      int lockNo;
      this.GetBucketAndLockNo(hashCode, out bucketNo, out lockNo, tables.m_buckets.Length, tables.m_locks.Length);
      lock (tables.m_locks[lockNo])
      {
        if (tables == this.m_tables)
        {
          ConcurrentDictionary<TKey, TValue>.Node node1 = (ConcurrentDictionary<TKey, TValue>.Node) null;
          for (ConcurrentDictionary<TKey, TValue>.Node node2 = tables.m_buckets[bucketNo]; node2 != null; node2 = node2.m_next)
          {
            if (comparer.Equals(node2.m_key, key))
            {
              if (!equalityComparer.Equals(node2.m_value, comparisonValue))
                return false;
              if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
              {
                node2.m_value = newValue;
              }
              else
              {
                ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2.m_key, newValue, hashCode, node2.m_next);
                if (node1 == null)
                  tables.m_buckets[bucketNo] = node3;
                else
                  node1.m_next = node3;
              }
              return true;
            }
            node1 = node2;
          }
          return false;
        }
        goto label_3;
      }
    }

    /// <summary>
    ///   Удаляет все ключи и значения из словаря <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Clear()
    {
      int locksAcquired = 0;
      try
      {
        this.AcquireAllLocks(ref locksAcquired);
        ConcurrentDictionary<TKey, TValue>.Tables tables = new ConcurrentDictionary<TKey, TValue>.Tables(new ConcurrentDictionary<TKey, TValue>.Node[31], this.m_tables.m_locks, new int[this.m_tables.m_countPerLock.Length], this.m_tables.m_comparer);
        this.m_tables = tables;
        this.m_budget = Math.Max(1, tables.m_buckets.Length / tables.m_locks.Length);
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), this.GetResource("ConcurrentDictionary_IndexIsNegative"));
      int locksAcquired = 0;
      try
      {
        this.AcquireAllLocks(ref locksAcquired);
        int num = 0;
        for (int index1 = 0; index1 < this.m_tables.m_locks.Length && num >= 0; ++index1)
          num += this.m_tables.m_countPerLock[index1];
        if (array.Length - num < index || num < 0)
          throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
        this.CopyToPairs(array, index);
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    /// <summary>
    ///   Копирует пары "ключ-значение", хранящиеся в коллекции <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, в новый массив.
    /// </summary>
    /// <returns>
    ///   Новый массив, содержащий снимок пар "ключ-значение", скопированных из коллекции <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public KeyValuePair<TKey, TValue>[] ToArray()
    {
      int locksAcquired = 0;
      try
      {
        this.AcquireAllLocks(ref locksAcquired);
        int length = 0;
        int index = 0;
        while (index < this.m_tables.m_locks.Length)
        {
          checked { length += this.m_tables.m_countPerLock[index]; }
          checked { ++index; }
        }
        if (length == 0)
          return Array.Empty<KeyValuePair<TKey, TValue>>();
        KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[length];
        this.CopyToPairs(array, 0);
        return array;
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    private void CopyToPairs(KeyValuePair<TKey, TValue>[] array, int index)
    {
      foreach (ConcurrentDictionary<TKey, TValue>.Node bucket in this.m_tables.m_buckets)
      {
        for (ConcurrentDictionary<TKey, TValue>.Node node = bucket; node != null; node = node.m_next)
        {
          array[index] = new KeyValuePair<TKey, TValue>(node.m_key, node.m_value);
          ++index;
        }
      }
    }

    private void CopyToEntries(DictionaryEntry[] array, int index)
    {
      foreach (ConcurrentDictionary<TKey, TValue>.Node bucket in this.m_tables.m_buckets)
      {
        for (ConcurrentDictionary<TKey, TValue>.Node node = bucket; node != null; node = node.m_next)
        {
          array[index] = new DictionaryEntry((object) node.m_key, (object) node.m_value);
          ++index;
        }
      }
    }

    private void CopyToObjects(object[] array, int index)
    {
      foreach (ConcurrentDictionary<TKey, TValue>.Node bucket in this.m_tables.m_buckets)
      {
        for (ConcurrentDictionary<TKey, TValue>.Node node = bucket; node != null; node = node.m_next)
        {
          array[index] = (object) new KeyValuePair<TKey, TValue>(node.m_key, node.m_value);
          ++index;
        }
      }
    }

    /// <summary>
    ///   Возвращает перечислитель, осуществляющий перебор элементов списка <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </summary>
    /// <returns>
    ///   Перечислитель для коллекции <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      foreach (ConcurrentDictionary<TKey, TValue>.Node bucket in this.m_tables.m_buckets)
      {
        ConcurrentDictionary<TKey, TValue>.Node current;
        for (current = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref bucket); current != null; current = current.m_next)
          yield return new KeyValuePair<TKey, TValue>(current.m_key, current.m_value);
        current = (ConcurrentDictionary<TKey, TValue>.Node) null;
      }
    }

    private bool TryAddInternal(TKey key, TValue value, bool updateIfExists, bool acquireLock, out TValue resultingValue)
    {
label_0:
      ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
      IEqualityComparer<TKey> comparer = tables.m_comparer;
      int hashCode = comparer.GetHashCode(key);
      int bucketNo;
      int lockNo;
      this.GetBucketAndLockNo(hashCode, out bucketNo, out lockNo, tables.m_buckets.Length, tables.m_locks.Length);
      bool flag1 = false;
      bool lockTaken = false;
      bool flag2 = false;
      try
      {
        if (acquireLock)
          Monitor.Enter(tables.m_locks[lockNo], ref lockTaken);
        if (tables == this.m_tables)
        {
          int num = 0;
          ConcurrentDictionary<TKey, TValue>.Node node1 = (ConcurrentDictionary<TKey, TValue>.Node) null;
          for (ConcurrentDictionary<TKey, TValue>.Node node2 = tables.m_buckets[bucketNo]; node2 != null; node2 = node2.m_next)
          {
            if (comparer.Equals(node2.m_key, key))
            {
              if (updateIfExists)
              {
                if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
                {
                  node2.m_value = value;
                }
                else
                {
                  ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2.m_key, value, hashCode, node2.m_next);
                  if (node1 == null)
                    tables.m_buckets[bucketNo] = node3;
                  else
                    node1.m_next = node3;
                }
                resultingValue = value;
              }
              else
                resultingValue = node2.m_value;
              return false;
            }
            node1 = node2;
            ++num;
          }
          if (num > 100 && HashHelpers.IsWellKnownEqualityComparer((object) comparer))
          {
            flag1 = true;
            flag2 = true;
          }
          Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables.m_buckets[bucketNo], new ConcurrentDictionary<TKey, TValue>.Node(key, value, hashCode, tables.m_buckets[bucketNo]));
          checked { ++tables.m_countPerLock[lockNo]; }
          if (tables.m_countPerLock[lockNo] > this.m_budget)
            flag1 = true;
        }
        else
          goto label_0;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(tables.m_locks[lockNo]);
      }
      if (flag1)
      {
        if (flag2)
          this.GrowTable(tables, (IEqualityComparer<TKey>) HashHelpers.GetRandomizedEqualityComparer((object) comparer), true, this.m_keyRehashCount);
        else
          this.GrowTable(tables, tables.m_comparer, false, this.m_keyRehashCount);
      }
      resultingValue = value;
      return true;
    }

    /// <summary>
    ///   Возвращает или задает значение, связанное с указанным ключом.
    /// </summary>
    /// <param name="key">
    ///   Ключ, значение которого требуется получить или задать.
    /// </param>
    /// <returns>
    ///   Значение пары "ключ-значение" по указанному индексу.
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
        TValue obj;
        if (!this.TryGetValue(key, out obj))
          throw new KeyNotFoundException();
        return obj;
      }
      [__DynamicallyInvokable] set
      {
        if ((object) key == null)
          throw new ArgumentNullException(nameof (key));
        TValue resultingValue;
        this.TryAddInternal(key, value, true, true, out resultingValue);
      }
    }

    /// <summary>
    ///   Возвращает число пар "ключ-значение", содержащихся в словаре <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </summary>
    /// <returns>
    ///   Число пар "ключ-значение", содержащихся в словаре <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Словарь уже содержит максимальное количество элементов (<see cref="F:System.Int32.MaxValue" />).
    /// </exception>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        int locksAcquired = 0;
        try
        {
          this.AcquireAllLocks(ref locksAcquired);
          return this.GetCountInternal();
        }
        finally
        {
          this.ReleaseLocks(0, locksAcquired);
        }
      }
    }

    private int GetCountInternal()
    {
      int num = 0;
      for (int index = 0; index < this.m_tables.m_countPerLock.Length; ++index)
        num += this.m_tables.m_countPerLock[index];
      return num;
    }

    /// <summary>
    ///   Добавляет пару "ключ-значение" в <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, используя указанную функцию, если ключ еще не существует.
    /// </summary>
    /// <param name="key">Ключ добавляемого элемента.</param>
    /// <param name="valueFactory">
    ///   Функция, используемая для создания значения для ключа
    /// </param>
    /// <returns>
    ///   Значение для ключа.
    ///    Этим значением будет существующее значение ключа, если ключ уже имеется в словаре, или новое значение, возвращенное функцией valueFactory, если ключ не существовал в словаре.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="key" /> или <paramref name="valueFactory" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Словарь уже содержит максимальное количество элементов (<see cref="F:System.Int32.MaxValue" />).
    /// </exception>
    [__DynamicallyInvokable]
    public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      if (valueFactory == null)
        throw new ArgumentNullException(nameof (valueFactory));
      TValue resultingValue;
      if (this.TryGetValue(key, out resultingValue))
        return resultingValue;
      this.TryAddInternal(key, valueFactory(key), false, true, out resultingValue);
      return resultingValue;
    }

    /// <summary>
    ///   Добавляет пару "ключ-значение" в коллекцию <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, если ключ еще не существует.
    /// </summary>
    /// <param name="key">Ключ добавляемого элемента.</param>
    /// <param name="value">
    ///   Значение, которое необходимо добавить, если ключ еще не существует
    /// </param>
    /// <returns>
    ///   Значение для ключа.
    ///    Этим значением будет существующее значение ключа, если ключ уже имеется в словаре, или новое значение, если ключ не существовал в словаре.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Словарь уже содержит максимальное количество элементов (<see cref="F:System.Int32.MaxValue" />).
    /// </exception>
    [__DynamicallyInvokable]
    public TValue GetOrAdd(TKey key, TValue value)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      TValue resultingValue;
      this.TryAddInternal(key, value, false, true, out resultingValue);
      return resultingValue;
    }

    public TValue GetOrAdd<TArg>(TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      if (valueFactory == null)
        throw new ArgumentNullException(nameof (valueFactory));
      TValue resultingValue;
      if (!this.TryGetValue(key, out resultingValue))
        this.TryAddInternal(key, valueFactory(key, factoryArgument), false, true, out resultingValue);
      return resultingValue;
    }

    public TValue AddOrUpdate<TArg>(TKey key, Func<TKey, TArg, TValue> addValueFactory, Func<TKey, TValue, TArg, TValue> updateValueFactory, TArg factoryArgument)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      if (addValueFactory == null)
        throw new ArgumentNullException(nameof (addValueFactory));
      if (updateValueFactory == null)
        throw new ArgumentNullException(nameof (updateValueFactory));
      TValue comparisonValue;
      TValue newValue;
      do
      {
        while (!this.TryGetValue(key, out comparisonValue))
        {
          TValue resultingValue;
          if (this.TryAddInternal(key, addValueFactory(key, factoryArgument), false, true, out resultingValue))
            return resultingValue;
        }
        newValue = updateValueFactory(key, comparisonValue, factoryArgument);
      }
      while (!this.TryUpdate(key, newValue, comparisonValue));
      return newValue;
    }

    /// <summary>
    ///   Использует заданные функции, чтобы добавить пару "ключ-значение" в коллекцию <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, если данный ключ еще не существует, или чтобы обновить пару "ключ-значение" в коллекции <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> в случае наличия ключа.
    /// </summary>
    /// <param name="key">
    ///   Ключ, который добавляется или значение которого обновляется
    /// </param>
    /// <param name="addValueFactory">
    ///   Функция, используемая для создания значения для несуществующего ключа
    /// </param>
    /// <param name="updateValueFactory">
    ///   Функция, используемая для создания нового значения для существующего ключа на основе его текущего значения
    /// </param>
    /// <returns>
    ///   Новое значение для ключа.
    ///    Это значение будет результатом выполнения функции addValueFactory (если ключ не существовал) или updateValueFactory (если ключ имелся).
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="key" />, <paramref name="addValueFactory" /> или <paramref name="updateValueFactory" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Словарь уже содержит максимальное количество элементов (<see cref="F:System.Int32.MaxValue" />).
    /// </exception>
    [__DynamicallyInvokable]
    public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      if (addValueFactory == null)
        throw new ArgumentNullException(nameof (addValueFactory));
      if (updateValueFactory == null)
        throw new ArgumentNullException(nameof (updateValueFactory));
      TValue comparisonValue;
      TValue newValue;
      do
      {
        while (!this.TryGetValue(key, out comparisonValue))
        {
          TValue obj = addValueFactory(key);
          TValue resultingValue;
          if (this.TryAddInternal(key, obj, false, true, out resultingValue))
            return resultingValue;
        }
        newValue = updateValueFactory(key, comparisonValue);
      }
      while (!this.TryUpdate(key, newValue, comparisonValue));
      return newValue;
    }

    /// <summary>
    ///   Добавляет пару "ключ-значение" в коллекцию <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, если данный ключ еще не существует, или обновляет пару "ключ-значение" в коллекции <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, используя указанную функцию, в случае наличия ключа.
    /// </summary>
    /// <param name="key">
    ///   Ключ, который добавляется или значение которого обновляется
    /// </param>
    /// <param name="addValue">
    ///   Значение, добавляемое для несуществующего ключа
    /// </param>
    /// <param name="updateValueFactory">
    ///   Функция, используемая для создания нового значения для существующего ключа на основе его текущего значения
    /// </param>
    /// <returns>
    ///   Новое значение для ключа.
    ///    Это может быть либо значение addValue (если ключ отсутствовал), либо результат updateValueFactory (если ключ имелся).
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="key" /> или <paramref name="updateValueFactory" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Словарь уже содержит максимальное количество элементов (<see cref="F:System.Int32.MaxValue" />).
    /// </exception>
    [__DynamicallyInvokable]
    public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      if (updateValueFactory == null)
        throw new ArgumentNullException(nameof (updateValueFactory));
      TValue comparisonValue;
      TValue newValue;
      do
      {
        while (!this.TryGetValue(key, out comparisonValue))
        {
          TValue resultingValue;
          if (this.TryAddInternal(key, addValue, false, true, out resultingValue))
            return resultingValue;
        }
        newValue = updateValueFactory(key, comparisonValue);
      }
      while (!this.TryUpdate(key, newValue, comparisonValue));
      return newValue;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли коллекция <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> пустой.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> пуст; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsEmpty
    {
      [__DynamicallyInvokable] get
      {
        int locksAcquired = 0;
        try
        {
          this.AcquireAllLocks(ref locksAcquired);
          for (int index = 0; index < this.m_tables.m_countPerLock.Length; ++index)
          {
            if (this.m_tables.m_countPerLock[index] != 0)
              return false;
          }
        }
        finally
        {
          this.ReleaseLocks(0, locksAcquired);
        }
        return true;
      }
    }

    [__DynamicallyInvokable]
    void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
    {
      if (!this.TryAdd(key, value))
        throw new ArgumentException(this.GetResource("ConcurrentDictionary_KeyAlreadyExisted"));
    }

    [__DynamicallyInvokable]
    bool IDictionary<TKey, TValue>.Remove(TKey key)
    {
      TValue obj;
      return this.TryRemove(key, out obj);
    }

    /// <summary>
    ///   Возвращает коллекцию, содержащую ключи из словаря <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </summary>
    /// <returns>
    ///   Коллекция ключей в <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public ICollection<TKey> Keys
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection<TKey>) this.GetKeys();
      }
    }

    [__DynamicallyInvokable]
    IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<TKey>) this.GetKeys();
      }
    }

    /// <summary>
    ///   Возвращает коллекцию, содержащую значения <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </summary>
    /// <returns>
    ///   Коллекция, содержащая значения словаря <see cref="T:System.Collections.Generic.Dictionary`2" />.
    /// </returns>
    [__DynamicallyInvokable]
    public ICollection<TValue> Values
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection<TValue>) this.GetValues();
      }
    }

    [__DynamicallyInvokable]
    IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<TValue>) this.GetValues();
      }
    }

    [__DynamicallyInvokable]
    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
    {
      ((IDictionary<TKey, TValue>) this).Add(keyValuePair.Key, keyValuePair.Value);
    }

    [__DynamicallyInvokable]
    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
    {
      TValue x;
      if (!this.TryGetValue(keyValuePair.Key, out x))
        return false;
      return EqualityComparer<TValue>.Default.Equals(x, keyValuePair.Value);
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
    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
    {
      if ((object) keyValuePair.Key == null)
        throw new ArgumentNullException(this.GetResource("ConcurrentDictionary_ItemKeyIsNull"));
      TValue obj;
      return this.TryRemoveInternal(keyValuePair.Key, out obj, true, keyValuePair.Value);
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    [__DynamicallyInvokable]
    void IDictionary.Add(object key, object value)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (!(key is TKey))
        throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
      TValue obj;
      try
      {
        obj = (TValue) value;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
      }
      ((IDictionary<TKey, TValue>) this).Add((TKey) key, obj);
    }

    [__DynamicallyInvokable]
    bool IDictionary.Contains(object key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (key is TKey)
        return this.ContainsKey((TKey) key);
      return false;
    }

    [__DynamicallyInvokable]
    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      return (IDictionaryEnumerator) new ConcurrentDictionary<TKey, TValue>.DictionaryEnumerator(this);
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
        return (ICollection) this.GetKeys();
      }
    }

    [__DynamicallyInvokable]
    void IDictionary.Remove(object key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (!(key is TKey))
        return;
      TValue obj;
      this.TryRemove((TKey) key, out obj);
    }

    [__DynamicallyInvokable]
    ICollection IDictionary.Values
    {
      [__DynamicallyInvokable] get
      {
        return (ICollection) this.GetValues();
      }
    }

    [__DynamicallyInvokable]
    object IDictionary.this[object key]
    {
      [__DynamicallyInvokable] get
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        TValue obj;
        if (key is TKey && this.TryGetValue((TKey) key, out obj))
          return (object) obj;
        return (object) null;
      }
      [__DynamicallyInvokable] set
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        if (!(key is TKey))
          throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfKeyIncorrect"));
        if (!(value is TValue))
          throw new ArgumentException(this.GetResource("ConcurrentDictionary_TypeOfValueIncorrect"));
        this[(TKey) key] = (TValue) value;
      }
    }

    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), this.GetResource("ConcurrentDictionary_IndexIsNegative"));
      int locksAcquired = 0;
      try
      {
        this.AcquireAllLocks(ref locksAcquired);
        ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
        int num = 0;
        for (int index1 = 0; index1 < tables.m_locks.Length && num >= 0; ++index1)
          num += tables.m_countPerLock[index1];
        if (array.Length - num < index || num < 0)
          throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayNotLargeEnough"));
        KeyValuePair<TKey, TValue>[] array1 = array as KeyValuePair<TKey, TValue>[];
        if (array1 != null)
        {
          this.CopyToPairs(array1, index);
        }
        else
        {
          DictionaryEntry[] array2 = array as DictionaryEntry[];
          if (array2 != null)
          {
            this.CopyToEntries(array2, index);
          }
          else
          {
            object[] array3 = array as object[];
            if (array3 == null)
              throw new ArgumentException(this.GetResource("ConcurrentDictionary_ArrayIncorrectType"), nameof (array));
            this.CopyToObjects(array3, index);
          }
        }
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
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
        throw new NotSupportedException(Environment.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported"));
      }
    }

    private void GrowTable(ConcurrentDictionary<TKey, TValue>.Tables tables, IEqualityComparer<TKey> newComparer, bool regenerateHashKeys, int rehashCount)
    {
      int locksAcquired = 0;
      try
      {
        this.AcquireLocks(0, 1, ref locksAcquired);
        if (regenerateHashKeys && rehashCount == this.m_keyRehashCount)
        {
          tables = this.m_tables;
        }
        else
        {
          if (tables != this.m_tables)
            return;
          long num = 0;
          for (int index = 0; index < tables.m_countPerLock.Length; ++index)
            num += (long) tables.m_countPerLock[index];
          if (num < (long) (tables.m_buckets.Length / 4))
          {
            this.m_budget = 2 * this.m_budget;
            if (this.m_budget >= 0)
              return;
            this.m_budget = int.MaxValue;
            return;
          }
        }
        int length1 = 0;
        bool flag = false;
        try
        {
          length1 = checked (tables.m_buckets.Length * 2 + 1);
          while (length1 % 3 == 0 || length1 % 5 == 0 || length1 % 7 == 0)
            checked { length1 += 2; }
          if (length1 > 2146435071)
            flag = true;
        }
        catch (OverflowException ex)
        {
          flag = true;
        }
        if (flag)
        {
          length1 = 2146435071;
          this.m_budget = int.MaxValue;
        }
        this.AcquireLocks(1, tables.m_locks.Length, ref locksAcquired);
        object[] locks = tables.m_locks;
        if (this.m_growLockArray && tables.m_locks.Length < 1024)
        {
          locks = new object[tables.m_locks.Length * 2];
          Array.Copy((Array) tables.m_locks, (Array) locks, tables.m_locks.Length);
          for (int length2 = tables.m_locks.Length; length2 < locks.Length; ++length2)
            locks[length2] = new object();
        }
        ConcurrentDictionary<TKey, TValue>.Node[] buckets = new ConcurrentDictionary<TKey, TValue>.Node[length1];
        int[] countPerLock = new int[locks.Length];
        ConcurrentDictionary<TKey, TValue>.Node next;
        for (int index = 0; index < tables.m_buckets.Length; ++index)
        {
          for (ConcurrentDictionary<TKey, TValue>.Node node = tables.m_buckets[index]; node != null; node = next)
          {
            next = node.m_next;
            int hashcode = node.m_hashcode;
            if (regenerateHashKeys)
              hashcode = newComparer.GetHashCode(node.m_key);
            int bucketNo;
            int lockNo;
            this.GetBucketAndLockNo(hashcode, out bucketNo, out lockNo, buckets.Length, locks.Length);
            buckets[bucketNo] = new ConcurrentDictionary<TKey, TValue>.Node(node.m_key, node.m_value, hashcode, buckets[bucketNo]);
            checked { ++countPerLock[lockNo]; }
          }
        }
        if (regenerateHashKeys)
          ++this.m_keyRehashCount;
        this.m_budget = Math.Max(1, buckets.Length / locks.Length);
        this.m_tables = new ConcurrentDictionary<TKey, TValue>.Tables(buckets, locks, countPerLock, newComparer);
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    private void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount, int lockCount)
    {
      bucketNo = (hashcode & int.MaxValue) % bucketCount;
      lockNo = bucketNo % lockCount;
    }

    private static int DefaultConcurrencyLevel
    {
      get
      {
        return PlatformHelper.ProcessorCount;
      }
    }

    private void AcquireAllLocks(ref int locksAcquired)
    {
      if (CDSCollectionETWBCLProvider.Log.IsEnabled())
        CDSCollectionETWBCLProvider.Log.ConcurrentDictionary_AcquiringAllLocks(this.m_tables.m_buckets.Length);
      this.AcquireLocks(0, 1, ref locksAcquired);
      this.AcquireLocks(1, this.m_tables.m_locks.Length, ref locksAcquired);
    }

    private void AcquireLocks(int fromInclusive, int toExclusive, ref int locksAcquired)
    {
      object[] locks = this.m_tables.m_locks;
      for (int index = fromInclusive; index < toExclusive; ++index)
      {
        bool lockTaken = false;
        try
        {
          Monitor.Enter(locks[index], ref lockTaken);
        }
        finally
        {
          if (lockTaken)
            ++locksAcquired;
        }
      }
    }

    private void ReleaseLocks(int fromInclusive, int toExclusive)
    {
      for (int index = fromInclusive; index < toExclusive; ++index)
        Monitor.Exit(this.m_tables.m_locks[index]);
    }

    private ReadOnlyCollection<TKey> GetKeys()
    {
      int locksAcquired = 0;
      try
      {
        this.AcquireAllLocks(ref locksAcquired);
        int countInternal = this.GetCountInternal();
        if (countInternal < 0)
          throw new OutOfMemoryException();
        List<TKey> keyList = new List<TKey>(countInternal);
        for (int index = 0; index < this.m_tables.m_buckets.Length; ++index)
        {
          for (ConcurrentDictionary<TKey, TValue>.Node node = this.m_tables.m_buckets[index]; node != null; node = node.m_next)
            keyList.Add(node.m_key);
        }
        return new ReadOnlyCollection<TKey>((IList<TKey>) keyList);
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    private ReadOnlyCollection<TValue> GetValues()
    {
      int locksAcquired = 0;
      try
      {
        this.AcquireAllLocks(ref locksAcquired);
        int countInternal = this.GetCountInternal();
        if (countInternal < 0)
          throw new OutOfMemoryException();
        List<TValue> objList = new List<TValue>(countInternal);
        for (int index = 0; index < this.m_tables.m_buckets.Length; ++index)
        {
          for (ConcurrentDictionary<TKey, TValue>.Node node = this.m_tables.m_buckets[index]; node != null; node = node.m_next)
            objList.Add(node.m_value);
        }
        return new ReadOnlyCollection<TValue>((IList<TValue>) objList);
      }
      finally
      {
        this.ReleaseLocks(0, locksAcquired);
      }
    }

    [Conditional("DEBUG")]
    private void Assert(bool condition)
    {
    }

    private string GetResource(string key)
    {
      return Environment.GetResourceString(key);
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext context)
    {
      ConcurrentDictionary<TKey, TValue>.Tables tables = this.m_tables;
      this.m_serializationArray = this.ToArray();
      this.m_serializationConcurrencyLevel = tables.m_locks.Length;
      this.m_serializationCapacity = tables.m_buckets.Length;
      this.m_comparer = (IEqualityComparer<TKey>) HashHelpers.GetEqualityComparerForSerialization((object) tables.m_comparer);
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
      KeyValuePair<TKey, TValue>[] serializationArray = this.m_serializationArray;
      ConcurrentDictionary<TKey, TValue>.Node[] buckets = new ConcurrentDictionary<TKey, TValue>.Node[this.m_serializationCapacity];
      int[] countPerLock = new int[this.m_serializationConcurrencyLevel];
      object[] locks = new object[this.m_serializationConcurrencyLevel];
      for (int index = 0; index < locks.Length; ++index)
        locks[index] = new object();
      this.m_tables = new ConcurrentDictionary<TKey, TValue>.Tables(buckets, locks, countPerLock, this.m_comparer);
      this.InitializeFromCollection((IEnumerable<KeyValuePair<TKey, TValue>>) serializationArray);
      this.m_serializationArray = (KeyValuePair<TKey, TValue>[]) null;
    }

    private class Tables
    {
      internal readonly ConcurrentDictionary<TKey, TValue>.Node[] m_buckets;
      internal readonly object[] m_locks;
      internal volatile int[] m_countPerLock;
      internal readonly IEqualityComparer<TKey> m_comparer;

      internal Tables(ConcurrentDictionary<TKey, TValue>.Node[] buckets, object[] locks, int[] countPerLock, IEqualityComparer<TKey> comparer)
      {
        this.m_buckets = buckets;
        this.m_locks = locks;
        this.m_countPerLock = countPerLock;
        this.m_comparer = comparer;
      }
    }

    private class Node
    {
      internal TKey m_key;
      internal TValue m_value;
      internal volatile ConcurrentDictionary<TKey, TValue>.Node m_next;
      internal int m_hashcode;

      internal Node(TKey key, TValue value, int hashcode, ConcurrentDictionary<TKey, TValue>.Node next)
      {
        this.m_key = key;
        this.m_value = value;
        this.m_next = next;
        this.m_hashcode = hashcode;
      }
    }

    private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
    {
      private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;

      internal DictionaryEnumerator(ConcurrentDictionary<TKey, TValue> dictionary)
      {
        this.m_enumerator = dictionary.GetEnumerator();
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
  }
}
