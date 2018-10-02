// Decompiled with JetBrains decompiler
// Type: System.Collections.SortedList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
  /// <summary>
  ///   Предоставляет коллекцию пар "ключ-значение", упорядоченных по ключам. Доступ к парам можно получить по ключу и индексу.
  /// </summary>
  [DebuggerTypeProxy(typeof (SortedList.SortedListDebugView))]
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(true)]
  [Serializable]
  public class SortedList : IDictionary, ICollection, IEnumerable, ICloneable
  {
    private static object[] emptyArray = EmptyArray<object>.Value;
    private object[] keys;
    private object[] values;
    private int _size;
    private int version;
    private IComparer comparer;
    private SortedList.KeyList keyList;
    private SortedList.ValueList valueList;
    [NonSerialized]
    private object _syncRoot;
    private const int _defaultCapacity = 16;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.SortedList" /> класс, который является пустым, обладает начальной емкостью по умолчанию и упорядоченный в соответствии с <see cref="T:System.IComparable" /> интерфейс, реализуемый каждым ключом, добавленным к <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    public SortedList()
    {
      this.Init();
    }

    private void Init()
    {
      this.keys = SortedList.emptyArray;
      this.values = SortedList.emptyArray;
      this._size = 0;
      this.comparer = (IComparer) new Comparer(CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.SortedList" /> класс, который является пустым, обладает указанной начальной емкостью и упорядоченный в соответствии с <see cref="T:System.IComparable" /> интерфейс, реализуемый каждым ключом, добавленным к <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <param name="initialCapacity">
    ///   Начальное количество элементов, <see cref="T:System.Collections.SortedList" /> может содержать объект.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="initialCapacity" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не недостаточно памяти для создания <see cref="T:System.Collections.SortedList" /> объекта с указанным <paramref name="initialCapacity" />.
    /// </exception>
    public SortedList(int initialCapacity)
    {
      if (initialCapacity < 0)
        throw new ArgumentOutOfRangeException(nameof (initialCapacity), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.keys = new object[initialCapacity];
      this.values = new object[initialCapacity];
      this.comparer = (IComparer) new Comparer(CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.SortedList" /> класс, который является пустым, обладает начальной емкостью по умолчанию и упорядоченный в соответствии с указанным <see cref="T:System.Collections.IComparer" /> интерфейса.
    /// </summary>
    /// <param name="comparer">
    ///   Реализация интерфейса <see cref="T:System.Collections.IComparer" />, используемая при сравнении ключей.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> для использования <see cref="T:System.IComparable" /> реализацию каждого ключа.
    /// </param>
    public SortedList(IComparer comparer)
      : this()
    {
      if (comparer == null)
        return;
      this.comparer = comparer;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.SortedList" /> класс, который является пустым, обладает указанной начальной емкостью и упорядоченный в соответствии с указанным <see cref="T:System.Collections.IComparer" /> интерфейса.
    /// </summary>
    /// <param name="comparer">
    ///   Реализация интерфейса <see cref="T:System.Collections.IComparer" />, используемая при сравнении ключей.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> для использования <see cref="T:System.IComparable" /> реализацию каждого ключа.
    /// </param>
    /// <param name="capacity">
    ///   Начальное количество элементов, <see cref="T:System.Collections.SortedList" /> может содержать объект.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не недостаточно памяти для создания <see cref="T:System.Collections.SortedList" /> объекта с указанным <paramref name="capacity" />.
    /// </exception>
    public SortedList(IComparer comparer, int capacity)
      : this(comparer)
    {
      this.Capacity = capacity;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.SortedList" /> класс, который содержит элементы, скопированные из указанного словаря имеет начальной емкостью, равной количеству скопированных элементов и упорядоченный в соответствии с <see cref="T:System.IComparable" /> интерфейс реализован каждым ключом.
    /// </summary>
    /// <param name="d">
    ///   <see cref="T:System.Collections.IDictionary" /> Реализацию, чтобы скопировать в новый <see cref="T:System.Collections.SortedList" /> объекта.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="d" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Один или несколько элементов в <paramref name="d" /> не реализуют <see cref="T:System.IComparable" /> интерфейса.
    /// </exception>
    public SortedList(IDictionary d)
      : this(d, (IComparer) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.SortedList" /> класс, который содержит элементы, скопированные из указанного словаря, обладающий начальной емкостью, равной количеству скопированных элементов и упорядоченный в соответствии с указанным <see cref="T:System.Collections.IComparer" /> интерфейса.
    /// </summary>
    /// <param name="d">
    ///   <see cref="T:System.Collections.IDictionary" /> Реализацию, чтобы скопировать в новый <see cref="T:System.Collections.SortedList" /> объекта.
    /// </param>
    /// <param name="comparer">
    ///   Реализация интерфейса <see cref="T:System.Collections.IComparer" />, используемая при сравнении ключей.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> для использования <see cref="T:System.IComparable" /> реализацию каждого ключа.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="d" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   <paramref name="comparer" /> является <see langword="null" />, и один или несколько элементов в <paramref name="d" /> не реализуют <see cref="T:System.IComparable" /> интерфейса.
    /// </exception>
    public SortedList(IDictionary d, IComparer comparer)
      : this(comparer, d != null ? d.Count : 0)
    {
      if (d == null)
        throw new ArgumentNullException(nameof (d), Environment.GetResourceString("ArgumentNull_Dictionary"));
      d.Keys.CopyTo((Array) this.keys, 0);
      d.Values.CopyTo((Array) this.values, 0);
      Array.Sort((Array) this.keys, (Array) this.values, comparer);
      this._size = d.Count;
    }

    /// <summary>
    ///   Добавляет элемент с указанным ключом и значением в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <param name="key">Ключ добавляемого элемента.</param>
    /// <param name="value">
    ///   Добавляемое значение элемента.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент с указанным <paramref name="key" /> уже существует в <see cref="T:System.Collections.SortedList" /> объекта.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.SortedList" /> Используется <see cref="T:System.IComparable" /> интерфейс, и <paramref name="key" /> не реализует <see cref="T:System.IComparable" /> интерфейса.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.SortedList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.SortedList" /> Имеет фиксированный размер.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти для добавления элемента нет <see cref="T:System.Collections.SortedList" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Компаратор вызывает исключение.
    /// </exception>
    public virtual void Add(object key, object value)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
      int index = Array.BinarySearch((Array) this.keys, 0, this._size, key, this.comparer);
      if (index >= 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", this.GetKey(index), key));
      this.Insert(~index, key, value);
    }

    /// <summary>
    ///   Возвращает или задает емкость объекта <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <returns>
    ///   Число элементов, <see cref="T:System.Collections.SortedList" /> может содержать объект.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Присвоенное значение меньше, чем текущее число элементов в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти в системе.
    /// </exception>
    public virtual int Capacity
    {
      get
      {
        return this.keys.Length;
      }
      set
      {
        if (value < this.Count)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        if (value == this.keys.Length)
          return;
        if (value > 0)
        {
          object[] objArray1 = new object[value];
          object[] objArray2 = new object[value];
          if (this._size > 0)
          {
            Array.Copy((Array) this.keys, 0, (Array) objArray1, 0, this._size);
            Array.Copy((Array) this.values, 0, (Array) objArray2, 0, this._size);
          }
          this.keys = objArray1;
          this.values = objArray2;
        }
        else
        {
          this.keys = SortedList.emptyArray;
          this.values = SortedList.emptyArray;
        }
      }
    }

    /// <summary>
    ///   Возвращает число элементов, содержащихся в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </returns>
    public virtual int Count
    {
      get
      {
        return this._size;
      }
    }

    /// <summary>
    ///   Возвращает ключи <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.ICollection" /> Объект, содержащий ключи в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </returns>
    public virtual ICollection Keys
    {
      get
      {
        return (ICollection) this.GetKeyList();
      }
    }

    /// <summary>
    ///   Возвращает значения в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.ICollection" /> Объект, содержащий значения в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </returns>
    public virtual ICollection Values
    {
      get
      {
        return (ICollection) this.GetValueList();
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see cref="T:System.Collections.SortedList" /> объект доступен только для чтения.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Collections.SortedList" /> объект только для чтения; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public virtual bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see cref="T:System.Collections.SortedList" /> объект имеет фиксированный размер.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Collections.SortedList" /> объект имеет фиксированный размер; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public virtual bool IsFixedSize
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли доступ к <see cref="T:System.Collections.SortedList" /> синхронизированным (потокобезопасным).
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если доступ к объекту <see cref="T:System.Collections.SortedList" /> является синхронизированным (потокобезопасным); в противном случае — значение <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public virtual bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает объект, который может использоваться для синхронизации доступа к <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект, который позволяет синхронизировать доступ к объекту <see cref="T:System.Collections.SortedList" />.
    /// </returns>
    public virtual object SyncRoot
    {
      get
      {
        if (this._syncRoot == null)
          Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    /// <summary>
    ///   Удаляет все элементы из объекта <see cref="T:System.Collections.SortedList" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="T:System.Collections.SortedList" /> Объект доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.SortedList" /> имеет фиксированный размер.
    /// </exception>
    public virtual void Clear()
    {
      ++this.version;
      Array.Clear((Array) this.keys, 0, this._size);
      Array.Clear((Array) this.values, 0, this._size);
      this._size = 0;
    }

    /// <summary>
    ///   Создает неполную копию объекта <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <returns>
    ///   Неполная копия <see cref="T:System.Collections.SortedList" /> объекта.
    /// </returns>
    public virtual object Clone()
    {
      SortedList sortedList = new SortedList(this._size);
      Array.Copy((Array) this.keys, 0, (Array) sortedList.keys, 0, this._size);
      Array.Copy((Array) this.values, 0, (Array) sortedList.values, 0, this._size);
      sortedList._size = this._size;
      sortedList.version = this.version;
      sortedList.comparer = this.comparer;
      return (object) sortedList;
    }

    /// <summary>
    ///   Определяет, является ли <see cref="T:System.Collections.SortedList" /> объект содержит конкретный ключ.
    /// </summary>
    /// <param name="key">
    ///   Ключ для размещения в объекте <see cref="T:System.Collections.SortedList" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Collections.SortedList" /> объект содержит элемент с указанным <paramref name="key" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Компаратор вызывает исключение.
    /// </exception>
    public virtual bool Contains(object key)
    {
      return this.IndexOfKey(key) >= 0;
    }

    /// <summary>
    ///   Определяет, является ли <see cref="T:System.Collections.SortedList" /> объект содержит конкретный ключ.
    /// </summary>
    /// <param name="key">
    ///   Ключ для размещения в объекте <see cref="T:System.Collections.SortedList" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Collections.SortedList" /> объект содержит элемент с указанным <paramref name="key" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Компаратор вызывает исключение.
    /// </exception>
    public virtual bool ContainsKey(object key)
    {
      return this.IndexOfKey(key) >= 0;
    }

    /// <summary>
    ///   Определяет, является ли <see cref="T:System.Collections.SortedList" /> объект содержит определенное значение.
    /// </summary>
    /// <param name="value">
    ///   Значение для поиска в <see cref="T:System.Collections.SortedList" /> объекта.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Collections.SortedList" /> объект содержит элемент с указанным <paramref name="value" />; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool ContainsValue(object value)
    {
      return this.IndexOfValue(value) >= 0;
    }

    /// <summary>
    ///   Копирует <see cref="T:System.Collections.SortedList" /> элементы в одномерном массиве <see cref="T:System.Array" /> объекта, начиная с указанного индекса в массиве.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" /> объект, который является конечным <see cref="T:System.Collections.DictionaryEntry" /> объекты из <see cref="T:System.Collections.SortedList" />.
    ///    Массив <see cref="T:System.Array" /> должен иметь индексацию, начинающуюся с нуля.
    /// </param>
    /// <param name="arrayIndex">
    ///   Отсчитываемый от нуля индекс в массиве <paramref name="array" />, указывающий начало копирования.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="arrayIndex" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="array" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Количество элементов в исходной коллекции <see cref="T:System.Collections.SortedList" /> объект больше, чем свободное пространство от <paramref name="arrayIndex" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Тип источника <see cref="T:System.Collections.SortedList" /> не может быть автоматически приведен к типу массива назначения <paramref name="array" />.
    /// </exception>
    public virtual void CopyTo(Array array, int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array), Environment.GetResourceString("ArgumentNull_Array"));
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (arrayIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (arrayIndex), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - arrayIndex < this.Count)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
      for (int index = 0; index < this.Count; ++index)
      {
        DictionaryEntry dictionaryEntry = new DictionaryEntry(this.keys[index], this.values[index]);
        array.SetValue((object) dictionaryEntry, index + arrayIndex);
      }
    }

    internal virtual KeyValuePairs[] ToKeyValuePairsArray()
    {
      KeyValuePairs[] keyValuePairsArray = new KeyValuePairs[this.Count];
      for (int index = 0; index < this.Count; ++index)
        keyValuePairsArray[index] = new KeyValuePairs(this.keys[index], this.values[index]);
      return keyValuePairsArray;
    }

    private void EnsureCapacity(int min)
    {
      int num = this.keys.Length == 0 ? 16 : this.keys.Length * 2;
      if ((uint) num > 2146435071U)
        num = 2146435071;
      if (num < min)
        num = min;
      this.Capacity = num;
    }

    /// <summary>
    ///   Возвращает значение по указанному индексу <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс значения, которое нужно получить.
    /// </param>
    /// <returns>
    ///   Значение по указанному индексу из <see cref="T:System.Collections.SortedList" /> объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.SortedList" /> объекта.
    /// </exception>
    public virtual object GetByIndex(int index)
    {
      if (index < 0 || index >= this.Count)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return this.values[index];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new SortedList.SortedListEnumerator(this, 0, this._size, 3);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IDictionaryEnumerator" /> объект, выполняющий перебор элементов <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Collections.IDictionaryEnumerator" /> для объекта <see cref="T:System.Collections.SortedList" />.
    /// </returns>
    public virtual IDictionaryEnumerator GetEnumerator()
    {
      return (IDictionaryEnumerator) new SortedList.SortedListEnumerator(this, 0, this._size, 3);
    }

    /// <summary>
    ///   Возвращает ключ по указанному индексу <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс получаемого ключа.
    /// </param>
    /// <returns>
    ///   Ключ по указанному индексу <see cref="T:System.Collections.SortedList" /> объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.SortedList" /> объекта.
    /// </exception>
    public virtual object GetKey(int index)
    {
      if (index < 0 || index >= this.Count)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return this.keys[index];
    }

    /// <summary>
    ///   Возвращает ключи <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IList" /> Объект, содержащий ключи в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </returns>
    public virtual IList GetKeyList()
    {
      if (this.keyList == null)
        this.keyList = new SortedList.KeyList(this);
      return (IList) this.keyList;
    }

    /// <summary>
    ///   Возвращает значения в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IList" /> Объект, содержащий значения в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </returns>
    public virtual IList GetValueList()
    {
      if (this.valueList == null)
        this.valueList = new SortedList.ValueList(this);
      return (IList) this.valueList;
    }

    /// <summary>
    ///   Возвращает или задает значение, связанное с указанным ключом в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <param name="key">
    ///   Ключ, связанный со значением, чтобы получить или задать.
    /// </param>
    /// <returns>
    ///   Значение, связанное с <paramref name="key" /> параметр в <see cref="T:System.Collections.SortedList" /> объекта, если <paramref name="key" /> найден; в противном случае — <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Свойство имеет значение и <see cref="T:System.Collections.SortedList" /> объект доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Задано свойство <paramref name="key" /> не существует в коллекции, а <see cref="T:System.Collections.SortedList" /> имеет фиксированный размер.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти для добавления элемента нет <see cref="T:System.Collections.SortedList" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Компаратор вызывает исключение.
    /// </exception>
    public virtual object this[object key]
    {
      get
      {
        int index = this.IndexOfKey(key);
        if (index >= 0)
          return this.values[index];
        return (object) null;
      }
      set
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
        int index = Array.BinarySearch((Array) this.keys, 0, this._size, key, this.comparer);
        if (index >= 0)
        {
          this.values[index] = value;
          ++this.version;
        }
        else
          this.Insert(~index, key, value);
      }
    }

    /// <summary>
    ///   Возвращает отсчитываемый от нуля индекс указанного ключа в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <param name="key">
    ///   Ключ для размещения в объекте <see cref="T:System.Collections.SortedList" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс <paramref name="key" /> параметр, если <paramref name="key" /> находится в <see cref="T:System.Collections.SortedList" /> объекта; в противном случае — значение -1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Компаратор вызывает исключение.
    /// </exception>
    public virtual int IndexOfKey(object key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
      int num = Array.BinarySearch((Array) this.keys, 0, this._size, key, this.comparer);
      if (num < 0)
        return -1;
      return num;
    }

    /// <summary>
    ///   Возвращает отсчитываемый от нуля индекс первого вхождения указанного значения в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Значение для поиска в <see cref="T:System.Collections.SortedList" /> объекта.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" /> параметр, если <paramref name="value" /> находится в <see cref="T:System.Collections.SortedList" /> объекта; в противном случае — значение -1.
    /// </returns>
    public virtual int IndexOfValue(object value)
    {
      return Array.IndexOf<object>(this.values, value, 0, this._size);
    }

    private void Insert(int index, object key, object value)
    {
      if (this._size == this.keys.Length)
        this.EnsureCapacity(this._size + 1);
      if (index < this._size)
      {
        Array.Copy((Array) this.keys, index, (Array) this.keys, index + 1, this._size - index);
        Array.Copy((Array) this.values, index, (Array) this.values, index + 1, this._size - index);
      }
      this.keys[index] = key;
      this.values[index] = value;
      ++this._size;
      ++this.version;
    }

    /// <summary>
    ///   Удаляет элемент по указанному индексу <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <param name="index">
    ///   Индекс (с нуля) элемента, который требуется удалить.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.SortedList" /> объекта.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.SortedList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.SortedList" /> имеет фиксированный размер.
    /// </exception>
    public virtual void RemoveAt(int index)
    {
      if (index < 0 || index >= this.Count)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      --this._size;
      if (index < this._size)
      {
        Array.Copy((Array) this.keys, index + 1, (Array) this.keys, index, this._size - index);
        Array.Copy((Array) this.values, index + 1, (Array) this.values, index, this._size - index);
      }
      this.keys[this._size] = (object) null;
      this.values[this._size] = (object) null;
      ++this.version;
    }

    /// <summary>
    ///   Удаляет элемент с указанным ключом из <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется удалить.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="T:System.Collections.SortedList" /> Объект доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.SortedList" /> имеет фиксированный размер.
    /// </exception>
    public virtual void Remove(object key)
    {
      int index = this.IndexOfKey(key);
      if (index < 0)
        return;
      this.RemoveAt(index);
    }

    /// <summary>
    ///   Заменяет значение по указанному индексу в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому необходимо сохранить <paramref name="value" />.
    /// </param>
    /// <param name="value">
    ///   <see cref="T:System.Object" /> Для сохранения в <see cref="T:System.Collections.SortedList" /> объекта.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.SortedList" /> объекта.
    /// </exception>
    public virtual void SetByIndex(int index, object value)
    {
      if (index < 0 || index >= this.Count)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      this.values[index] = value;
      ++this.version;
    }

    /// <summary>
    ///   Возвращает синхронизированной (потокобезопасной) обертки для <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <param name="list">
    ///   Синхронизируемый объект <see cref="T:System.Collections.SortedList" />.
    /// </param>
    /// <returns>
    ///   Синхронизированная (потокобезопасная) оболочка для <see cref="T:System.Collections.SortedList" /> объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="list" /> имеет значение <see langword="null" />.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static SortedList Synchronized(SortedList list)
    {
      if (list == null)
        throw new ArgumentNullException(nameof (list));
      return (SortedList) new SortedList.SyncSortedList(list);
    }

    /// <summary>
    ///   Устанавливает емкость равной фактическому числу элементов в <see cref="T:System.Collections.SortedList" /> объекта.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="T:System.Collections.SortedList" /> Объект доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.SortedList" /> Имеет фиксированный размер.
    /// </exception>
    public virtual void TrimToSize()
    {
      this.Capacity = this._size;
    }

    [Serializable]
    private class SyncSortedList : SortedList
    {
      private SortedList _list;
      private object _root;

      internal SyncSortedList(SortedList list)
      {
        this._list = list;
        this._root = list.SyncRoot;
      }

      public override int Count
      {
        get
        {
          lock (this._root)
            return this._list.Count;
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._root;
        }
      }

      public override bool IsReadOnly
      {
        get
        {
          return this._list.IsReadOnly;
        }
      }

      public override bool IsFixedSize
      {
        get
        {
          return this._list.IsFixedSize;
        }
      }

      public override bool IsSynchronized
      {
        get
        {
          return true;
        }
      }

      public override object this[object key]
      {
        get
        {
          lock (this._root)
            return this._list[key];
        }
        set
        {
          lock (this._root)
            this._list[key] = value;
        }
      }

      public override void Add(object key, object value)
      {
        lock (this._root)
          this._list.Add(key, value);
      }

      public override int Capacity
      {
        get
        {
          lock (this._root)
            return this._list.Capacity;
        }
      }

      public override void Clear()
      {
        lock (this._root)
          this._list.Clear();
      }

      public override object Clone()
      {
        lock (this._root)
          return this._list.Clone();
      }

      public override bool Contains(object key)
      {
        lock (this._root)
          return this._list.Contains(key);
      }

      public override bool ContainsKey(object key)
      {
        lock (this._root)
          return this._list.ContainsKey(key);
      }

      public override bool ContainsValue(object key)
      {
        lock (this._root)
          return this._list.ContainsValue(key);
      }

      public override void CopyTo(Array array, int index)
      {
        lock (this._root)
          this._list.CopyTo(array, index);
      }

      public override object GetByIndex(int index)
      {
        lock (this._root)
          return this._list.GetByIndex(index);
      }

      public override IDictionaryEnumerator GetEnumerator()
      {
        lock (this._root)
          return this._list.GetEnumerator();
      }

      public override object GetKey(int index)
      {
        lock (this._root)
          return this._list.GetKey(index);
      }

      public override IList GetKeyList()
      {
        lock (this._root)
          return this._list.GetKeyList();
      }

      public override IList GetValueList()
      {
        lock (this._root)
          return this._list.GetValueList();
      }

      public override int IndexOfKey(object key)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
        lock (this._root)
          return this._list.IndexOfKey(key);
      }

      public override int IndexOfValue(object value)
      {
        lock (this._root)
          return this._list.IndexOfValue(value);
      }

      public override void RemoveAt(int index)
      {
        lock (this._root)
          this._list.RemoveAt(index);
      }

      public override void Remove(object key)
      {
        lock (this._root)
          this._list.Remove(key);
      }

      public override void SetByIndex(int index, object value)
      {
        lock (this._root)
          this._list.SetByIndex(index, value);
      }

      internal override KeyValuePairs[] ToKeyValuePairsArray()
      {
        return this._list.ToKeyValuePairsArray();
      }

      public override void TrimToSize()
      {
        lock (this._root)
          this._list.TrimToSize();
      }
    }

    [Serializable]
    private class SortedListEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
    {
      private SortedList sortedList;
      private object key;
      private object value;
      private int index;
      private int startIndex;
      private int endIndex;
      private int version;
      private bool current;
      private int getObjectRetType;
      internal const int Keys = 1;
      internal const int Values = 2;
      internal const int DictEntry = 3;

      internal SortedListEnumerator(SortedList sortedList, int index, int count, int getObjRetType)
      {
        this.sortedList = sortedList;
        this.index = index;
        this.startIndex = index;
        this.endIndex = index + count;
        this.version = sortedList.version;
        this.getObjectRetType = getObjRetType;
        this.current = false;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public virtual object Key
      {
        get
        {
          if (this.version != this.sortedList.version)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return this.key;
        }
      }

      public virtual bool MoveNext()
      {
        if (this.version != this.sortedList.version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this.index < this.endIndex)
        {
          this.key = this.sortedList.keys[this.index];
          this.value = this.sortedList.values[this.index];
          ++this.index;
          this.current = true;
          return true;
        }
        this.key = (object) null;
        this.value = (object) null;
        this.current = false;
        return false;
      }

      public virtual DictionaryEntry Entry
      {
        get
        {
          if (this.version != this.sortedList.version)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return new DictionaryEntry(this.key, this.value);
        }
      }

      public virtual object Current
      {
        get
        {
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          if (this.getObjectRetType == 1)
            return this.key;
          if (this.getObjectRetType == 2)
            return this.value;
          return (object) new DictionaryEntry(this.key, this.value);
        }
      }

      public virtual object Value
      {
        get
        {
          if (this.version != this.sortedList.version)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
          if (!this.current)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return this.value;
        }
      }

      public virtual void Reset()
      {
        if (this.version != this.sortedList.version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this.index = this.startIndex;
        this.current = false;
        this.key = (object) null;
        this.value = (object) null;
      }
    }

    [Serializable]
    private class KeyList : IList, ICollection, IEnumerable
    {
      private SortedList sortedList;

      internal KeyList(SortedList sortedList)
      {
        this.sortedList = sortedList;
      }

      public virtual int Count
      {
        get
        {
          return this.sortedList._size;
        }
      }

      public virtual bool IsReadOnly
      {
        get
        {
          return true;
        }
      }

      public virtual bool IsFixedSize
      {
        get
        {
          return true;
        }
      }

      public virtual bool IsSynchronized
      {
        get
        {
          return this.sortedList.IsSynchronized;
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this.sortedList.SyncRoot;
        }
      }

      public virtual int Add(object key)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual void Clear()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual bool Contains(object key)
      {
        return this.sortedList.Contains(key);
      }

      public virtual void CopyTo(Array array, int arrayIndex)
      {
        if (array != null && array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        Array.Copy((Array) this.sortedList.keys, 0, array, arrayIndex, this.sortedList.Count);
      }

      public virtual void Insert(int index, object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual object this[int index]
      {
        get
        {
          return this.sortedList.GetKey(index);
        }
        set
        {
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
        }
      }

      public virtual IEnumerator GetEnumerator()
      {
        return (IEnumerator) new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 1);
      }

      public virtual int IndexOf(object key)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
        int num = Array.BinarySearch((Array) this.sortedList.keys, 0, this.sortedList.Count, key, this.sortedList.comparer);
        if (num >= 0)
          return num;
        return -1;
      }

      public virtual void Remove(object key)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual void RemoveAt(int index)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }
    }

    [Serializable]
    private class ValueList : IList, ICollection, IEnumerable
    {
      private SortedList sortedList;

      internal ValueList(SortedList sortedList)
      {
        this.sortedList = sortedList;
      }

      public virtual int Count
      {
        get
        {
          return this.sortedList._size;
        }
      }

      public virtual bool IsReadOnly
      {
        get
        {
          return true;
        }
      }

      public virtual bool IsFixedSize
      {
        get
        {
          return true;
        }
      }

      public virtual bool IsSynchronized
      {
        get
        {
          return this.sortedList.IsSynchronized;
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this.sortedList.SyncRoot;
        }
      }

      public virtual int Add(object key)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual void Clear()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual bool Contains(object value)
      {
        return this.sortedList.ContainsValue(value);
      }

      public virtual void CopyTo(Array array, int arrayIndex)
      {
        if (array != null && array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        Array.Copy((Array) this.sortedList.values, 0, array, arrayIndex, this.sortedList.Count);
      }

      public virtual void Insert(int index, object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual object this[int index]
      {
        get
        {
          return this.sortedList.GetByIndex(index);
        }
        set
        {
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
        }
      }

      public virtual IEnumerator GetEnumerator()
      {
        return (IEnumerator) new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 2);
      }

      public virtual int IndexOf(object value)
      {
        return Array.IndexOf<object>(this.sortedList.values, value, 0, this.sortedList.Count);
      }

      public virtual void Remove(object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }

      public virtual void RemoveAt(int index)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
      }
    }

    internal class SortedListDebugView
    {
      private SortedList sortedList;

      public SortedListDebugView(SortedList sortedList)
      {
        if (sortedList == null)
          throw new ArgumentNullException(nameof (sortedList));
        this.sortedList = sortedList;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public KeyValuePairs[] Items
      {
        get
        {
          return this.sortedList.ToKeyValuePairsArray();
        }
      }
    }
  }
}
