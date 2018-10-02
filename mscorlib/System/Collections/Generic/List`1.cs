// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.List`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Threading;

namespace System.Collections.Generic
{
  /// <summary>
  ///   Представляет строго типизированный список объектов, доступных по индексу.
  ///    Поддерживает методы для поиска по списку, выполнения сортировки и других операций со списками.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  /// <typeparam name="T">Тип элементов в списке.</typeparam>
  [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  public class List<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
  {
    private static readonly T[] _emptyArray = new T[0];
    private const int _defaultCapacity = 4;
    private T[] _items;
    private int _size;
    private int _version;
    [NonSerialized]
    private object _syncRoot;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Generic.List`1" />, который является пустым и имеет начальную емкость по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public List()
    {
      this._items = List<T>._emptyArray;
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.Generic.List`1" /> с указанной начальной емкостью.
    /// </summary>
    /// <param name="capacity">
    ///   Число элементов, которые может изначально вместить новый список.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше 0.
    /// </exception>
    [__DynamicallyInvokable]
    public List(int capacity)
    {
      if (capacity < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (capacity == 0)
        this._items = List<T>._emptyArray;
      else
        this._items = new T[capacity];
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Generic.List`1" />, который содержит элементы, скопированные из указанной коллекции, и имеет емкость, достаточную для размещения всех скопированных элементов.
    /// </summary>
    /// <param name="collection">
    ///   Коллекция, элементы которой копируются в новый список.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="collection" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public List(IEnumerable<T> collection)
    {
      if (collection == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
      ICollection<T> objs = collection as ICollection<T>;
      if (objs != null)
      {
        int count = objs.Count;
        if (count == 0)
        {
          this._items = List<T>._emptyArray;
        }
        else
        {
          this._items = new T[count];
          objs.CopyTo(this._items, 0);
          this._size = count;
        }
      }
      else
      {
        this._size = 0;
        this._items = List<T>._emptyArray;
        foreach (T obj in collection)
          this.Add(obj);
      }
    }

    /// <summary>
    ///   Возвращает или задает общее число элементов, которые может вместить внутренняя структура данных без изменения размера.
    /// </summary>
    /// <returns>
    ///   Число элементов, которые может вместить коллекция <see cref="T:System.Collections.Generic.List`1" />, прежде чем потребуется изменить ее размер.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Для <see cref="P:System.Collections.Generic.List`1.Capacity" /> установлено значение, которое меньше <see cref="P:System.Collections.Generic.List`1.Count" />.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти в системе.
    /// </exception>
    [__DynamicallyInvokable]
    public int Capacity
    {
      [__DynamicallyInvokable] get
      {
        return this._items.Length;
      }
      [__DynamicallyInvokable] set
      {
        if (value < this._size)
          ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
        if (value == this._items.Length)
          return;
        if (value > 0)
        {
          T[] objArray = new T[value];
          if (this._size > 0)
            Array.Copy((Array) this._items, 0, (Array) objArray, 0, this._size);
          this._items = objArray;
        }
        else
          this._items = List<T>._emptyArray;
      }
    }

    /// <summary>
    ///   Получает число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в коллекции <see cref="T:System.Collections.Generic.List`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this._size;
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsFixedSize
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return false;
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
        if (this._syncRoot == null)
          Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    /// <summary>
    ///   Возвращает или задает элемент по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс элемента, который требуется возвратить или задать.
    /// </param>
    /// <returns>Элемент, расположенный по указанному индексу.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="index" /> больше или равно значению свойства <see cref="P:System.Collections.Generic.List`1.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public T this[int index]
    {
      [__DynamicallyInvokable] get
      {
        if ((uint) index >= (uint) this._size)
          ThrowHelper.ThrowArgumentOutOfRangeException();
        return this._items[index];
      }
      [__DynamicallyInvokable] set
      {
        if ((uint) index >= (uint) this._size)
          ThrowHelper.ThrowArgumentOutOfRangeException();
        this._items[index] = value;
        ++this._version;
      }
    }

    private static bool IsCompatibleObject(object value)
    {
      if (value is T)
        return true;
      if (value == null)
        return (object) default (T) == null;
      return false;
    }

    [__DynamicallyInvokable]
    object IList.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return (object) this[index];
      }
      [__DynamicallyInvokable] set
      {
        ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
        try
        {
          this[index] = (T) value;
        }
        catch (InvalidCastException ex)
        {
          ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
        }
      }
    }

    /// <summary>
    ///   Добавляет объект в конец очереди <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект, добавляемый в конец коллекции <see cref="T:System.Collections.Generic.List`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    [__DynamicallyInvokable]
    public void Add(T item)
    {
      if (this._size == this._items.Length)
        this.EnsureCapacity(this._size + 1);
      this._items[this._size++] = item;
      ++this._version;
    }

    [__DynamicallyInvokable]
    int IList.Add(object item)
    {
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
      try
      {
        this.Add((T) item);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof (T));
      }
      return this.Count - 1;
    }

    /// <summary>
    ///   Добавляет элементы указанной коллекции в конец списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="collection">
    ///   Коллекция, элементы которой добавляются в конец списка <see cref="T:System.Collections.Generic.List`1" />.
    ///    Коллекция не может быть задана значением <see langword="null" />, но может содержать элементы <see langword="null" />, если тип <paramref name="T" /> является ссылочным типом.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="collection" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void AddRange(IEnumerable<T> collection)
    {
      this.InsertRange(this._size, collection);
    }

    /// <summary>
    ///   Возвращает для текущей коллекции оболочку <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />, доступную только для чтения.
    /// </summary>
    /// <returns>
    ///   Объект, который служит оболочкой, обеспечивающей доступность текущего списка <see cref="T:System.Collections.Generic.List`1" /> только для чтения.
    /// </returns>
    [__DynamicallyInvokable]
    public ReadOnlyCollection<T> AsReadOnly()
    {
      return new ReadOnlyCollection<T>((IList<T>) this);
    }

    /// <summary>
    ///   Выполняет поиск элемента в диапазоне элементов отсортированного списка <see cref="T:System.Collections.Generic.List`1" />, используя указанную функцию сравнения, и возвращает индекс элемента, отсчитываемый от нуля.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс начала диапазона поиска.
    /// </param>
    /// <param name="count">Длина диапазона поиска.</param>
    /// <param name="item">
    ///   Искомый объект.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <param name="comparer">
    ///   Реализация <see cref="T:System.Collections.Generic.IComparer`1" />, которую следует использовать при сравнении элементов, или <see langword="null" />, если должна использоваться функция сравнения по умолчанию <see cref="P:System.Collections.Generic.Comparer`1.Default" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс элемента <paramref name="item" /> в отсортированном списке <see cref="T:System.Collections.Generic.List`1" />, если элемент <paramref name="item" /> найден; в противном случае — отрицательное число, которое является поразрядным дополнением индекса следующего элемента, большего, чем <paramref name="item" />, или, если большего элемента не существует, поразрядным дополнением значения <see cref="P:System.Collections.Generic.List`1.Count" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон в <see cref="T:System.Collections.Generic.List`1" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="comparer" /> имеет значение <see langword="null" />, а функция сравнения по умолчанию <see cref="P:System.Collections.Generic.Comparer`1.Default" /> не может найти реализацию универсального интерфейса <see cref="T:System.IComparable`1" /> или интерфейса <see cref="T:System.IComparable" /> для типа <paramref name="T" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      return Array.BinarySearch<T>(this._items, index, count, item, comparer);
    }

    /// <summary>
    ///   Выполняет поиск элемента по всему отсортированному списку <see cref="T:System.Collections.Generic.List`1" />, используя компаратор по умолчанию, и возвращает индекс элемента, отсчитываемый от нуля.
    /// </summary>
    /// <param name="item">
    ///   Искомый объект.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс элемента <paramref name="item" /> в отсортированном списке <see cref="T:System.Collections.Generic.List`1" />, если элемент <paramref name="item" /> найден; в противном случае — отрицательное число, которое является поразрядным дополнением индекса следующего элемента, большего, чем <paramref name="item" />, или, если большего элемента не существует, поразрядным дополнением значения <see cref="P:System.Collections.Generic.List`1.Count" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Функция сравнения по умолчанию <see cref="P:System.Collections.Generic.Comparer`1.Default" /> не может найти реализацию универсального интерфейса <see cref="T:System.IComparable`1" /> или интерфейса <see cref="T:System.IComparable" /> для типа <paramref name="T" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int BinarySearch(T item)
    {
      return this.BinarySearch(0, this.Count, item, (IComparer<T>) null);
    }

    /// <summary>
    ///   Выполняет поиск элемента по всему отсортированному списку <see cref="T:System.Collections.Generic.List`1" />, используя указанный компаратор, и возвращает индекс элемента, отсчитываемый от нуля.
    /// </summary>
    /// <param name="item">
    ///   Искомый объект.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <param name="comparer">
    ///   Реализация интерфейса <see cref="T:System.Collections.Generic.IComparer`1" />, которая используется при сравнении элементов.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />, если необходимо использовать функцию сравнения по умолчанию <see cref="P:System.Collections.Generic.Comparer`1.Default" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс элемента <paramref name="item" /> в отсортированном списке <see cref="T:System.Collections.Generic.List`1" />, если элемент <paramref name="item" /> найден; в противном случае — отрицательное число, которое является поразрядным дополнением индекса следующего элемента, большего, чем <paramref name="item" />, или, если большего элемента не существует, поразрядным дополнением значения <see cref="P:System.Collections.Generic.List`1.Count" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="comparer" /> имеет значение <see langword="null" />, а функция сравнения по умолчанию <see cref="P:System.Collections.Generic.Comparer`1.Default" /> не может найти реализацию универсального интерфейса <see cref="T:System.IComparable`1" /> или интерфейса <see cref="T:System.IComparable" /> для типа <paramref name="T" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int BinarySearch(T item, IComparer<T> comparer)
    {
      return this.BinarySearch(0, this.Count, item, comparer);
    }

    /// <summary>
    ///   Удаляет все элементы из коллекции <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Clear()
    {
      if (this._size > 0)
      {
        Array.Clear((Array) this._items, 0, this._size);
        this._size = 0;
      }
      ++this._version;
    }

    /// <summary>
    ///   Определяет, входит ли элемент в коллекцию <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект для поиска в <see cref="T:System.Collections.Generic.List`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="item" /> найден в коллекции <see cref="T:System.Collections.Generic.List`1" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Contains(T item)
    {
      if ((object) item == null)
      {
        for (int index = 0; index < this._size; ++index)
        {
          if ((object) this._items[index] == null)
            return true;
        }
        return false;
      }
      EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
      for (int index = 0; index < this._size; ++index)
      {
        if (equalityComparer.Equals(this._items[index], item))
          return true;
      }
      return false;
    }

    [__DynamicallyInvokable]
    bool IList.Contains(object item)
    {
      if (List<T>.IsCompatibleObject(item))
        return this.Contains((T) item);
      return false;
    }

    /// <summary>
    ///   Преобразует элементы текущего списка <see cref="T:System.Collections.Generic.List`1" /> в другой тип и возвращает список преобразованных элементов.
    /// </summary>
    /// <param name="converter">
    ///   Делегат <see cref="T:System.Converter`2" />, преобразующий каждый элемент из одного типа в другой.
    /// </param>
    /// <typeparam name="TOutput">
    ///   Тип элементов массива назначения.
    /// </typeparam>
    /// <returns>
    ///   Список <see cref="T:System.Collections.Generic.List`1" /> с элементами конечного типа, преобразованными из текущего списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="converter" /> имеет значение <see langword="null" />.
    /// </exception>
    public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
    {
      if (converter == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.converter);
      List<TOutput> outputList = new List<TOutput>(this._size);
      for (int index = 0; index < this._size; ++index)
        outputList._items[index] = converter(this._items[index]);
      outputList._size = this._size;
      return outputList;
    }

    /// <summary>
    ///   Копирует весь список <see cref="T:System.Collections.Generic.List`1" /> в совместимый одномерный массив, начиная с первого элемента целевого массива.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.Generic.List`1" />.
    ///    Массив <see cref="T:System.Array" /> должен иметь индексацию, начинающуюся с нуля.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Число элементов в исходном массиве <see cref="T:System.Collections.Generic.List`1" /> больше числа элементов, которые может содержать массив назначения <paramref name="array" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array)
    {
      this.CopyTo(array, 0);
    }

    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int arrayIndex)
    {
      if (array != null && array.Rank != 1)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
      try
      {
        Array.Copy((Array) this._items, 0, array, arrayIndex, this._size);
      }
      catch (ArrayTypeMismatchException ex)
      {
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
      }
    }

    /// <summary>
    ///   Копирует диапазон элементов из списка <see cref="T:System.Collections.Generic.List`1" /> в совместимый одномерный массив, начиная с указанного индекса конечного массива.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс исходного списка <see cref="T:System.Collections.Generic.List`1" />, с которого начинается копирование.
    /// </param>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.Generic.List`1" />.
    ///    Массив <see cref="T:System.Array" /> должен иметь индексацию, начинающуюся с нуля.
    /// </param>
    /// <param name="arrayIndex">
    ///   Отсчитываемый от нуля индекс в массиве <paramref name="array" />, указывающий начало копирования.
    /// </param>
    /// <param name="count">Число элементов для копирования.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="arrayIndex" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение параметра <paramref name="index" /> больше или равно значению <see cref="P:System.Collections.Generic.List`1.Count" /> исходного списка <see cref="T:System.Collections.Generic.List`1" />.
    /// 
    ///   -или-
    /// 
    ///   Число элементов от <paramref name="index" /> до конца исходного списка <see cref="T:System.Collections.Generic.List`1" /> больше доступного места от положения, заданного значением параметра <paramref name="arrayIndex" />, до конца массива назначения <paramref name="array" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      Array.Copy((Array) this._items, index, (Array) array, arrayIndex, count);
    }

    /// <summary>
    ///   Копирует <see cref="T:System.Collections.Generic.List`1" /> целиком в совместимый одномерный массив, начиная с указанного индекса конечного массива.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.Generic.List`1" />.
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
    ///   Число элементов в исходной коллекции <see cref="T:System.Collections.Generic.List`1" /> больше доступного места от положения, заданного значением параметра <paramref name="arrayIndex" />, до конца массива назначения <paramref name="array" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int arrayIndex)
    {
      Array.Copy((Array) this._items, 0, (Array) array, arrayIndex, this._size);
    }

    private void EnsureCapacity(int min)
    {
      if (this._items.Length >= min)
        return;
      int num = this._items.Length == 0 ? 4 : this._items.Length * 2;
      if ((uint) num > 2146435071U)
        num = 2146435071;
      if (num < min)
        num = min;
      this.Capacity = num;
    }

    /// <summary>
    ///   Определяет, содержит ли <see cref="T:System.Collections.Generic.List`1" /> элементы, удовлетворяющие условиям указанного предиката.
    /// </summary>
    /// <param name="match">
    ///   Делегат <see cref="T:System.Predicate`1" />, определяющий условия поиска элементов.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если <see cref="T:System.Collections.Generic.List`1" /> содержит один или несколько элементов, удовлетворяющих условиям указанного предиката, в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="match" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Exists(Predicate<T> match)
    {
      return this.FindIndex(match) != -1;
    }

    /// <summary>
    ///   Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает первое найденное вхождение в пределах всего списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="match">
    ///   Делегат <see cref="T:System.Predicate`1" />, определяющий условия поиска элемента.
    /// </param>
    /// <returns>
    ///   Первый элемент, удовлетворяющий условиям указанного предиката, если такой элемент найден; в противном случае — значение по умолчанию для типа <paramref name="T" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="match" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public T Find(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      for (int index = 0; index < this._size; ++index)
      {
        if (match(this._items[index]))
          return this._items[index];
      }
      return default (T);
    }

    /// <summary>
    ///   Извлекает все элементы, удовлетворяющие условиям указанного предиката.
    /// </summary>
    /// <param name="match">
    ///   Делегат <see cref="T:System.Predicate`1" />, определяющий условия поиска элементов.
    /// </param>
    /// <returns>
    ///   Список <see cref="T:System.Collections.Generic.List`1" />, содержащий все элементы, удовлетворяющие условиям указанного предиката, если такие элементы найдены; в противном случае — пустой список <see cref="T:System.Collections.Generic.List`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="match" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public List<T> FindAll(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      List<T> objList = new List<T>();
      for (int index = 0; index < this._size; ++index)
      {
        if (match(this._items[index]))
          objList.Add(this._items[index]);
      }
      return objList;
    }

    /// <summary>
    ///   Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает отсчитываемый от нуля индекс первого найденного вхождения в пределах всего списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="match">
    ///   Делегат <see cref="T:System.Predicate`1" />, определяющий условия поиска элемента.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения элемента, удовлетворяющего условиям предиката <paramref name="match" />, если такой элемент найден; в противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="match" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int FindIndex(Predicate<T> match)
    {
      return this.FindIndex(0, this._size, match);
    }

    /// <summary>
    ///   Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает отсчитываемый от нуля индекс первого вхождения в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" />, начиная с заданного индекса и заканчивая последним элементом.
    /// </summary>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начальной позиции поиска.
    /// </param>
    /// <param name="match">
    ///   Делегат <see cref="T:System.Predicate`1" />, определяющий условия поиска элемента.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения элемента, удовлетворяющего условиям предиката <paramref name="match" />, если такой элемент найден; в противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="match" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.Generic.List`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int FindIndex(int startIndex, Predicate<T> match)
    {
      return this.FindIndex(startIndex, this._size - startIndex, match);
    }

    /// <summary>
    ///   Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает отсчитываемый от нуля индекс первого вхождения в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" />, начинающемся с заданного индекса и содержащем указанное число элементов.
    /// </summary>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начальной позиции поиска.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <param name="match">
    ///   Делегат <see cref="T:System.Predicate`1" />, определяющий условия поиска элемента.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения элемента, удовлетворяющего условиям предиката <paramref name="match" />, если такой элемент найден; в противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="match" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.Generic.List`1" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> и <paramref name="count" /> не задают допустимый раздел в <see cref="T:System.Collections.Generic.List`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int FindIndex(int startIndex, int count, Predicate<T> match)
    {
      if ((uint) startIndex > (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (count < 0 || startIndex > this._size - count)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      int num = startIndex + count;
      for (int index = startIndex; index < num; ++index)
      {
        if (match(this._items[index]))
          return index;
      }
      return -1;
    }

    /// <summary>
    ///   Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает последнее найденное вхождение в пределах всего списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="match">
    ///   Делегат <see cref="T:System.Predicate`1" />, определяющий условия поиска элемента.
    /// </param>
    /// <returns>
    ///   Последний элемент, удовлетворяющий условиям указанного предиката, если такой элемент найден; в противном случае — значение по умолчанию для типа <paramref name="T" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="match" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public T FindLast(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      for (int index = this._size - 1; index >= 0; --index)
      {
        if (match(this._items[index]))
          return this._items[index];
      }
      return default (T);
    }

    /// <summary>
    ///   Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает отсчитываемый от нуля индекс последнего найденного вхождения в пределах всего списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="match">
    ///   Делегат <see cref="T:System.Predicate`1" />, определяющий условия поиска элемента.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения элемента, удовлетворяющего условиям предиката <paramref name="match" />, если такой элемент найден; в противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="match" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int FindLastIndex(Predicate<T> match)
    {
      return this.FindLastIndex(this._size - 1, this._size, match);
    }

    /// <summary>
    ///   Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает отсчитываемый от нуля индекс последнего вхождения в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" />, начиная с первого элемента и заканчивая элементом с заданным индексом.
    /// </summary>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <param name="match">
    ///   Делегат <see cref="T:System.Predicate`1" />, определяющий условия поиска элемента.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения элемента, удовлетворяющего условиям предиката <paramref name="match" />, если такой элемент найден; в противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="match" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.Generic.List`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int FindLastIndex(int startIndex, Predicate<T> match)
    {
      return this.FindLastIndex(startIndex, startIndex + 1, match);
    }

    /// <summary>
    ///   Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает отсчитываемый от нуля индекс последнего вхождения в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" />, содержащем указанное число элементов и заканчивающемся элементом с заданным индексом.
    /// </summary>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <param name="match">
    ///   Делегат <see cref="T:System.Predicate`1" />, определяющий условия поиска элемента.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения элемента, удовлетворяющего условиям предиката <paramref name="match" />, если такой элемент найден; в противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="match" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.Generic.List`1" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> и <paramref name="count" /> не задают допустимый раздел в <see cref="T:System.Collections.Generic.List`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int FindLastIndex(int startIndex, int count, Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      if (this._size == 0)
      {
        if (startIndex != -1)
          ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      }
      else if ((uint) startIndex >= (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
      if (count < 0 || startIndex - count + 1 < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
      int num = startIndex - count;
      for (int index = startIndex; index > num; --index)
      {
        if (match(this._items[index]))
          return index;
      }
      return -1;
    }

    /// <summary>
    ///   Выполняет указанное действие с каждым элементом списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="action">
    ///   Делегат <see cref="T:System.Action`1" />, выполняемый для каждого элемента списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="action" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// Элемент в коллекции изменен.
    /// 
    ///   Это исключение вызывается, начиная с .NET Framework 4.5.
    /// </exception>
    [__DynamicallyInvokable]
    public void ForEach(Action<T> action)
    {
      if (action == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      int version = this._version;
      for (int index = 0; index < this._size && (version == this._version || !BinaryCompatibility.TargetsAtLeast_Desktop_V4_5); ++index)
        action(this._items[index]);
      if (version == this._version || !BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
        return;
      ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
    }

    /// <summary>
    ///   Возвращает перечислитель, осуществляющий перебор элементов списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <returns>
    ///   Новый объект <see cref="T:System.Collections.Generic.List`1.Enumerator" /> для <see cref="T:System.Collections.Generic.List`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public List<T>.Enumerator GetEnumerator()
    {
      return new List<T>.Enumerator(this);
    }

    [__DynamicallyInvokable]
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return (IEnumerator<T>) new List<T>.Enumerator(this);
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new List<T>.Enumerator(this);
    }

    /// <summary>
    ///   Создает неполную копию диапазона элементов исходного списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс списка <see cref="T:System.Collections.Generic.List`1" />, с которого начинается диапазон.
    /// </param>
    /// <param name="count">Число элементов в диапазоне.</param>
    /// <returns>
    ///   Неполная копия диапазона элементов исходного списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон элементов в списке <see cref="T:System.Collections.Generic.List`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public List<T> GetRange(int index, int count)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      List<T> objList = new List<T>(count);
      Array.Copy((Array) this._items, index, (Array) objList._items, 0, count);
      objList._size = count;
      return objList;
    }

    /// <summary>
    ///   Осуществляет поиск указанного объекта и возвращает отсчитываемый от нуля индекс первого вхождения, найденного в пределах всего списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект для поиска в <see cref="T:System.Collections.Generic.List`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения элемента <paramref name="item" /> в пределах всей коллекции <see cref="T:System.Collections.Generic.List`1" />, если элемент найден; в противном случае — значение –1.
    /// </returns>
    [__DynamicallyInvokable]
    public int IndexOf(T item)
    {
      return Array.IndexOf<T>(this._items, item, 0, this._size);
    }

    [__DynamicallyInvokable]
    int IList.IndexOf(object item)
    {
      if (List<T>.IsCompatibleObject(item))
        return this.IndexOf((T) item);
      return -1;
    }

    /// <summary>
    ///   Осуществляет поиск указанного объекта и возвращает отсчитываемый от нуля индекс первого вхождения в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" />, начиная с заданного индекса и до последнего элемента.
    /// </summary>
    /// <param name="item">
    ///   Объект для поиска в <see cref="T:System.Collections.Generic.List`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <param name="index">
    ///   Индекс (с нуля) начальной позиции поиска.
    ///    Значение 0 (ноль) действительно в пустом списке.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения элемента <paramref name="item" /> в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" />, начиная с позиции <paramref name="index" /> и до конца списка, если элемент найден; в противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.Generic.List`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int IndexOf(T item, int index)
    {
      if (index > this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
      return Array.IndexOf<T>(this._items, item, index, this._size - index);
    }

    /// <summary>
    ///   Выполняет поиск указанного объекта и возвращает отсчитываемый от нуля индекс первого вхождения в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" />, начинающемся с заданного индекса и содержащем указанное число элементов.
    /// </summary>
    /// <param name="item">
    ///   Объект для поиска в <see cref="T:System.Collections.Generic.List`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <param name="index">
    ///   Индекс (с нуля) начальной позиции поиска.
    ///    Значение 0 (ноль) действительно в пустом списке.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="item" /> в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" />, который начинается с позиции <paramref name="index" /> и содержит <paramref name="count" /> элементов, если искомый объект найден; в противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.Generic.List`1" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="index" /> и <paramref name="count" /> не определяют допустимый раздел в <see cref="T:System.Collections.Generic.List`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int IndexOf(T item, int index, int count)
    {
      if (index > this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
      if (count < 0 || index > this._size - count)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
      return Array.IndexOf<T>(this._items, item, index, count);
    }

    /// <summary>
    ///   Вставляет элемент в коллекцию <see cref="T:System.Collections.Generic.List`1" /> по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому следует вставить элемент <paramref name="item" />.
    /// </param>
    /// <param name="item">
    ///   Вставляемый объект.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="index" /> больше значения <see cref="P:System.Collections.Generic.List`1.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void Insert(int index, T item)
    {
      if ((uint) index > (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
      if (this._size == this._items.Length)
        this.EnsureCapacity(this._size + 1);
      if (index < this._size)
        Array.Copy((Array) this._items, index, (Array) this._items, index + 1, this._size - index);
      this._items[index] = item;
      ++this._size;
      ++this._version;
    }

    [__DynamicallyInvokable]
    void IList.Insert(int index, object item)
    {
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
      try
      {
        this.Insert(index, (T) item);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof (T));
      }
    }

    /// <summary>
    ///   Вставляет элементы коллекции в список <see cref="T:System.Collections.Generic.List`1" /> в позиции с указанным индексом.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс места вставки новых элементов.
    /// </param>
    /// <param name="collection">
    ///   Коллекция, элементы которой следует вставить в список <see cref="T:System.Collections.Generic.List`1" />.
    ///    Коллекция не может быть задана значением <see langword="null" />, но может содержать элементы <see langword="null" />, если тип <paramref name="T" /> является ссылочным типом.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="collection" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="index" /> больше значения <see cref="P:System.Collections.Generic.List`1.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void InsertRange(int index, IEnumerable<T> collection)
    {
      if (collection == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
      if ((uint) index > (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
      ICollection<T> objs = collection as ICollection<T>;
      if (objs != null)
      {
        int count = objs.Count;
        if (count > 0)
        {
          this.EnsureCapacity(this._size + count);
          if (index < this._size)
            Array.Copy((Array) this._items, index, (Array) this._items, index + count, this._size - index);
          if (this == objs)
          {
            Array.Copy((Array) this._items, 0, (Array) this._items, index, index);
            Array.Copy((Array) this._items, index + count, (Array) this._items, index * 2, this._size - index);
          }
          else
          {
            T[] array = new T[count];
            objs.CopyTo(array, 0);
            array.CopyTo((Array) this._items, index);
          }
          this._size += count;
        }
      }
      else
      {
        foreach (T obj in collection)
          this.Insert(index++, obj);
      }
      ++this._version;
    }

    /// <summary>
    ///   Осуществляет поиск указанного объекта и возвращает отсчитываемый от нуля индекс последнего вхождения, найденного в пределах всего списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект для поиска в <see cref="T:System.Collections.Generic.List`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="item" /> в пределах всего списка <see cref="T:System.Collections.Generic.List`1" />, если элемент найден; в противном случае — значение –1.
    /// </returns>
    [__DynamicallyInvokable]
    public int LastIndexOf(T item)
    {
      if (this._size == 0)
        return -1;
      return this.LastIndexOf(item, this._size - 1, this._size);
    }

    /// <summary>
    ///   Осуществляет поиск указанного объекта и возвращает отсчитываемый от нуля индекс последнего вхождения в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" />, начиная с первого элемента и до позиции с заданным индексом.
    /// </summary>
    /// <param name="item">
    ///   Объект для поиска в <see cref="T:System.Collections.Generic.List`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <param name="index">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения элемента <paramref name="item" /> в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" />, начиная с первого элемента и до позиции <paramref name="index" />, если элемент найден; в противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.Generic.List`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(T item, int index)
    {
      if (index >= this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
      return this.LastIndexOf(item, index, index + 1);
    }

    /// <summary>
    ///   Выполняет поиск указанного объекта и возвращает отсчитываемый от нуля индекс последнего вхождения в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" />, содержащем указанное число элементов и заканчивающемся в позиции с указанным индексом.
    /// </summary>
    /// <param name="item">
    ///   Объект для поиска в <see cref="T:System.Collections.Generic.List`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <param name="index">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="item" /> в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" />, состоящем из <paramref name="count" /> элементов и заканчивающемся в позиции <paramref name="index" />, если элемент найден; в противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.Generic.List`1" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="index" /> и <paramref name="count" /> не задают допустимый раздел в <see cref="T:System.Collections.Generic.List`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(T item, int index, int count)
    {
      if (this.Count != 0 && index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this.Count != 0 && count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size == 0)
        return -1;
      if (index >= this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
      if (count > index + 1)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
      return Array.LastIndexOf<T>(this._items, item, index, count);
    }

    /// <summary>
    ///   Удаляет первое вхождение указанного объекта из коллекции <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект, который необходимо удалить из коллекции <see cref="T:System.Collections.Generic.List`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если элемент <paramref name="item" /> успешно удален, в противном случае — значение <see langword="false" />.
    ///     Этот метод также возвращает <see langword="false" />, если элемент <paramref name="item" /> не найден в коллекции <see cref="T:System.Collections.Generic.List`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Remove(T item)
    {
      int index = this.IndexOf(item);
      if (index < 0)
        return false;
      this.RemoveAt(index);
      return true;
    }

    [__DynamicallyInvokable]
    void IList.Remove(object item)
    {
      if (!List<T>.IsCompatibleObject(item))
        return;
      this.Remove((T) item);
    }

    /// <summary>
    ///   Удаляет все элементы, удовлетворяющие условиям указанного предиката.
    /// </summary>
    /// <param name="match">
    ///   Делегат <see cref="T:System.Predicate`1" />, определяющий условия удаления элемента.
    /// </param>
    /// <returns>
    ///   Число элементов, удаляемых из списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="match" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int RemoveAll(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      int index1 = 0;
      while (index1 < this._size && !match(this._items[index1]))
        ++index1;
      if (index1 >= this._size)
        return 0;
      int index2 = index1 + 1;
      while (index2 < this._size)
      {
        while (index2 < this._size && match(this._items[index2]))
          ++index2;
        if (index2 < this._size)
          this._items[index1++] = this._items[index2++];
      }
      Array.Clear((Array) this._items, index1, this._size - index1);
      int num = this._size - index1;
      this._size = index1;
      ++this._version;
      return num;
    }

    /// <summary>
    ///   Удаляет элемент списка <see cref="T:System.Collections.Generic.List`1" /> с указанным индексом.
    /// </summary>
    /// <param name="index">
    ///   Индекс (с нуля) элемента, который требуется удалить.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="index" /> больше или равно значению свойства <see cref="P:System.Collections.Generic.List`1.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void RemoveAt(int index)
    {
      if ((uint) index >= (uint) this._size)
        ThrowHelper.ThrowArgumentOutOfRangeException();
      --this._size;
      if (index < this._size)
        Array.Copy((Array) this._items, index + 1, (Array) this._items, index, this._size - index);
      this._items[this._size] = default (T);
      ++this._version;
    }

    /// <summary>
    ///   Удаляет диапазон элементов из списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс начала диапазона элементов, которые требуется удалить.
    /// </param>
    /// <param name="count">Число удаляемых элементов.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон элементов в списке <see cref="T:System.Collections.Generic.List`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void RemoveRange(int index, int count)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      if (count <= 0)
        return;
      int size = this._size;
      this._size -= count;
      if (index < this._size)
        Array.Copy((Array) this._items, index + count, (Array) this._items, index, this._size - index);
      Array.Clear((Array) this._items, this._size, count);
      ++this._version;
    }

    /// <summary>
    ///   Изменяет порядок элементов во всем списке <see cref="T:System.Collections.Generic.List`1" /> на обратный.
    /// </summary>
    [__DynamicallyInvokable]
    public void Reverse()
    {
      this.Reverse(0, this.Count);
    }

    /// <summary>Изменяет порядок элементов в указанном диапазоне.</summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс начала диапазона, порядок элементов которого требуется изменить.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, порядок сортировки в котором требуется изменить.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон элементов в списке <see cref="T:System.Collections.Generic.List`1" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void Reverse(int index, int count)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      Array.Reverse((Array) this._items, index, count);
      ++this._version;
    }

    /// <summary>
    ///   Сортирует элементы во всем списке <see cref="T:System.Collections.Generic.List`1" /> с помощью функции сравнения по умолчанию.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Функция сравнения по умолчанию <see cref="P:System.Collections.Generic.Comparer`1.Default" /> не может найти реализацию универсального интерфейса <see cref="T:System.IComparable`1" /> или интерфейса <see cref="T:System.IComparable" /> для типа <paramref name="T" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void Sort()
    {
      this.Sort(0, this.Count, (IComparer<T>) null);
    }

    /// <summary>
    ///   Сортирует элементы во всем списке <see cref="T:System.Collections.Generic.List`1" /> с помощью указанной функции сравнения.
    /// </summary>
    /// <param name="comparer">
    ///   Реализация <see cref="T:System.Collections.Generic.IComparer`1" />, которую следует использовать при сравнении элементов, или <see langword="null" />, если должна использоваться функция сравнения по умолчанию <see cref="P:System.Collections.Generic.Comparer`1.Default" />.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="comparer" /> имеет значение <see langword="null" />, и функция сравнения по умолчанию <see cref="P:System.Collections.Generic.Comparer`1.Default" /> не может найти реализацию универсального интерфейса <see cref="T:System.IComparable`1" /> или интерфейса <see cref="T:System.IComparable" /> для типа <paramref name="T" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Реализация <paramref name="comparer" /> вызвала ошибку во время сортировки.
    ///    Например, <paramref name="comparer" /> не может возвратить 0 при сравнении элемента с самим собой.
    /// </exception>
    [__DynamicallyInvokable]
    public void Sort(IComparer<T> comparer)
    {
      this.Sort(0, this.Count, comparer);
    }

    /// <summary>
    ///   Сортирует элементы в диапазоне элементов списка <see cref="T:System.Collections.Generic.List`1" /> с помощью указанной функции сравнения.
    /// </summary>
    /// <param name="index">
    ///   Индекс (с нуля) начала диапазона, который требуется отсортировать.
    /// </param>
    /// <param name="count">Длина диапазона сортировки.</param>
    /// <param name="comparer">
    ///   Реализация <see cref="T:System.Collections.Generic.IComparer`1" />, которую следует использовать при сравнении элементов, или <see langword="null" />, если должна использоваться функция сравнения по умолчанию <see cref="P:System.Collections.Generic.Comparer`1.Default" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон в <see cref="T:System.Collections.Generic.List`1" />.
    /// 
    ///   -или-
    /// 
    ///   Реализация <paramref name="comparer" /> вызвала ошибку во время сортировки.
    ///    Например, <paramref name="comparer" /> может не возвратить 0 при сравнении элемента с самим собой.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="comparer" /> является <see langword="null" />, и функция сравнения по умолчанию <see cref="P:System.Collections.Generic.Comparer`1.Default" /> не может найти реализацию универсального интерфейса <see cref="T:System.IComparable`1" /> или интерфейса <see cref="T:System.IComparable" /> для типа <paramref name="T" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void Sort(int index, int count, IComparer<T> comparer)
    {
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (count < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (this._size - index < count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
      Array.Sort<T>(this._items, index, count, comparer);
      ++this._version;
    }

    /// <summary>
    ///   Сортирует элементы во всем списке <see cref="T:System.Collections.Generic.List`1" /> с использованием указанного <see cref="T:System.Comparison`1" />.
    /// </summary>
    /// <param name="comparison">
    ///   <see cref="T:System.Comparison`1" />, используемый при сравнении элементов.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="comparison" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Реализация <paramref name="comparison" /> вызвала ошибку во время сортировки.
    ///    Например, <paramref name="comparison" /> может не возвратить 0 при сравнении элемента с самим собой.
    /// </exception>
    [__DynamicallyInvokable]
    public void Sort(Comparison<T> comparison)
    {
      if (comparison == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      if (this._size <= 0)
        return;
      Array.Sort<T>(this._items, 0, this._size, (IComparer<T>) new Array.FunctorComparer<T>(comparison));
    }

    /// <summary>
    ///   Копирует элементы списка <see cref="T:System.Collections.Generic.List`1" /> в новый массив.
    /// </summary>
    /// <returns>
    ///   Массив, содержащий копии элементов списка <see cref="T:System.Collections.Generic.List`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public T[] ToArray()
    {
      T[] objArray = new T[this._size];
      Array.Copy((Array) this._items, 0, (Array) objArray, 0, this._size);
      return objArray;
    }

    /// <summary>
    ///   Задает емкость, равную фактическому числу элементов в списке <see cref="T:System.Collections.Generic.List`1" />, если это число меньше порогового значения.
    /// </summary>
    [__DynamicallyInvokable]
    public void TrimExcess()
    {
      if (this._size >= (int) ((double) this._items.Length * 0.9))
        return;
      this.Capacity = this._size;
    }

    /// <summary>
    ///   Определяет, все ли элементы списка <see cref="T:System.Collections.Generic.List`1" /> удовлетворяют условиям указанного предиката.
    /// </summary>
    /// <param name="match">
    ///   Делегат <see cref="T:System.Predicate`1" />, определяющий условия, проверяемые для элементов.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если каждый элемент списка <see cref="T:System.Collections.Generic.List`1" /> удовлетворяет условиям заданного предиката, в противном случае — <see langword="false" />.
    ///    Если в списке нет элементов, возвращается <see langword="true" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="match" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool TrueForAll(Predicate<T> match)
    {
      if (match == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
      for (int index = 0; index < this._size; ++index)
      {
        if (!match(this._items[index]))
          return false;
      }
      return true;
    }

    internal static IList<T> Synchronized(List<T> list)
    {
      return (IList<T>) new List<T>.SynchronizedList(list);
    }

    [Serializable]
    internal class SynchronizedList : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
      private List<T> _list;
      private object _root;

      internal SynchronizedList(List<T> list)
      {
        this._list = list;
        this._root = ((ICollection) list).SyncRoot;
      }

      public int Count
      {
        get
        {
          lock (this._root)
            return this._list.Count;
        }
      }

      public bool IsReadOnly
      {
        get
        {
          return ((ICollection<T>) this._list).IsReadOnly;
        }
      }

      public void Add(T item)
      {
        lock (this._root)
          this._list.Add(item);
      }

      public void Clear()
      {
        lock (this._root)
          this._list.Clear();
      }

      public bool Contains(T item)
      {
        lock (this._root)
          return this._list.Contains(item);
      }

      public void CopyTo(T[] array, int arrayIndex)
      {
        lock (this._root)
          this._list.CopyTo(array, arrayIndex);
      }

      public bool Remove(T item)
      {
        lock (this._root)
          return this._list.Remove(item);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        lock (this._root)
          return (IEnumerator) this._list.GetEnumerator();
      }

      IEnumerator<T> IEnumerable<T>.GetEnumerator()
      {
        lock (this._root)
          return ((IEnumerable<T>) this._list).GetEnumerator();
      }

      public T this[int index]
      {
        get
        {
          lock (this._root)
            return this._list[index];
        }
        set
        {
          lock (this._root)
            this._list[index] = value;
        }
      }

      public int IndexOf(T item)
      {
        lock (this._root)
          return this._list.IndexOf(item);
      }

      public void Insert(int index, T item)
      {
        lock (this._root)
          this._list.Insert(index, item);
      }

      public void RemoveAt(int index)
      {
        lock (this._root)
          this._list.RemoveAt(index);
      }
    }

    /// <summary>
    ///   Выполняет перечисление элементов коллекции <see cref="T:System.Collections.Generic.List`1" />.
    /// </summary>
    [__DynamicallyInvokable]
    [Serializable]
    public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
      private List<T> list;
      private int index;
      private int version;
      private T current;

      internal Enumerator(List<T> list)
      {
        this.list = list;
        this.index = 0;
        this.version = list._version;
        this.current = default (T);
      }

      /// <summary>
      ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.Collections.Generic.List`1.Enumerator" />.
      /// </summary>
      [__DynamicallyInvokable]
      public void Dispose()
      {
      }

      /// <summary>
      ///   Перемещает перечислитель к следующему элементу коллекции <see cref="T:System.Collections.Generic.List`1" />.
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
        List<T> list = this.list;
        if (this.version != list._version || (uint) this.index >= (uint) list._size)
          return this.MoveNextRare();
        this.current = list._items[this.index];
        ++this.index;
        return true;
      }

      private bool MoveNextRare()
      {
        if (this.version != this.list._version)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
        this.index = this.list._size + 1;
        this.current = default (T);
        return false;
      }

      /// <summary>
      ///   Возвращает элемент, расположенный в текущей позиции перечислителя.
      /// </summary>
      /// <returns>
      ///   Элемент коллекции <see cref="T:System.Collections.Generic.List`1" />, находящийся в текущей позиции перечислителя.
      /// </returns>
      [__DynamicallyInvokable]
      public T Current
      {
        [__DynamicallyInvokable] get
        {
          return this.current;
        }
      }

      [__DynamicallyInvokable]
      object IEnumerator.Current
      {
        [__DynamicallyInvokable] get
        {
          if (this.index == 0 || this.index == this.list._size + 1)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
          return (object) this.Current;
        }
      }

      [__DynamicallyInvokable]
      void IEnumerator.Reset()
      {
        if (this.version != this.list._version)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
        this.index = 0;
        this.current = default (T);
      }
    }
  }
}
