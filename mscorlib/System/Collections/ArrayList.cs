// Decompiled with JetBrains decompiler
// Type: System.Collections.ArrayList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
  /// <summary>
  ///   Реализует интерфейс <see cref="T:System.Collections.IList" /> с помощью массива с динамическим изменением размера по требованию.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [DebuggerTypeProxy(typeof (ArrayList.ArrayListDebugView))]
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(true)]
  [Serializable]
  public class ArrayList : IList, ICollection, IEnumerable, ICloneable
  {
    private static readonly object[] emptyArray = EmptyArray<object>.Value;
    private object[] _items;
    private int _size;
    private int _version;
    [NonSerialized]
    private object _syncRoot;
    private const int _defaultCapacity = 4;

    internal ArrayList(bool trash)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.ArrayList" />, который является пустым и имеет начальную емкость по умолчанию.
    /// </summary>
    public ArrayList()
    {
      this._items = ArrayList.emptyArray;
    }

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Collections.ArrayList" /> с указанной начальной емкостью.
    /// </summary>
    /// <param name="capacity">
    ///   Число элементов, которые может изначально вместить новый список.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// </exception>
    public ArrayList(int capacity)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException(nameof (capacity), Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", (object) nameof (capacity)));
      if (capacity == 0)
        this._items = ArrayList.emptyArray;
      else
        this._items = new object[capacity];
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.ArrayList" />, который содержит элементы, скопированные из указанной коллекции, и обладает начальной емкостью, равной количеству скопированных элементов.
    /// </summary>
    /// <param name="c">
    ///   Интерфейс <see cref="T:System.Collections.ICollection" />, элементы которого копируются в новый список.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="c" /> имеет значение <see langword="null" />.
    /// </exception>
    public ArrayList(ICollection c)
    {
      if (c == null)
        throw new ArgumentNullException(nameof (c), Environment.GetResourceString("ArgumentNull_Collection"));
      int count = c.Count;
      if (count == 0)
      {
        this._items = ArrayList.emptyArray;
      }
      else
      {
        this._items = new object[count];
        this.AddRange(c);
      }
    }

    /// <summary>
    ///   Возвращает или задает число элементов, которое может содержать список <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <returns>
    ///   Количество элементов, которое может содержать коллекция <see cref="T:System.Collections.ArrayList" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Для <see cref="P:System.Collections.ArrayList.Capacity" /> установлено значение, которое меньше <see cref="P:System.Collections.ArrayList.Count" />.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти в системе.
    /// </exception>
    public virtual int Capacity
    {
      get
      {
        return this._items.Length;
      }
      set
      {
        if (value < this._size)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        if (value == this._items.Length)
          return;
        if (value > 0)
        {
          object[] objArray = new object[value];
          if (this._size > 0)
            Array.Copy((Array) this._items, 0, (Array) objArray, 0, this._size);
          this._items = objArray;
        }
        else
          this._items = new object[4];
      }
    }

    /// <summary>
    ///   Возвращает число элементов, содержащихся в <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в <see cref="T:System.Collections.ArrayList" />.
    /// </returns>
    public virtual int Count
    {
      get
      {
        return this._size;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, имеет ли список <see cref="T:System.Collections.ArrayList" /> фиксированный размер.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если словарь <see cref="T:System.Collections.ArrayList" /> имеет фиксированный размер; в противном случае — значение <see langword="false" />.
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
    ///   Получает значение, указывающее, является ли объект <see cref="T:System.Collections.ArrayList" /> доступным только для чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если коллекция <see cref="T:System.Collections.ArrayList" /> доступна только для чтения; в противном случае — значение <see langword="false" />.
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
    ///   Возвращает значение, показывающее, является ли доступ к коллекции <see cref="T:System.Collections.ArrayList" /> синхронизированным (потокобезопасным).
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если доступ к классу <see cref="T:System.Collections.ArrayList" /> является синхронизированным (потокобезопасным); в противном случае — <see langword="false" />.
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
    ///   Получает объект, с помощью которого можно синхронизировать доступ к коллекции <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <returns>
    ///   Объект, который может использоваться для синхронизации доступа к <see cref="T:System.Collections.ArrayList" />.
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
    ///   Возвращает или задает элемент по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс элемента, который требуется возвратить или задать.
    /// </param>
    /// <returns>Элемент, расположенный по указанному индексу.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="index" /> больше или равно значению свойства <see cref="P:System.Collections.ArrayList.Count" />.
    /// </exception>
    public virtual object this[int index]
    {
      get
      {
        if (index < 0 || index >= this._size)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        return this._items[index];
      }
      set
      {
        if (index < 0 || index >= this._size)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        this._items[index] = value;
        ++this._version;
      }
    }

    /// <summary>
    ///   Создает оболочку класса <see cref="T:System.Collections.ArrayList" /> для указанного интерфейса <see cref="T:System.Collections.IList" />.
    /// </summary>
    /// <param name="list">
    ///   Класс <see cref="T:System.Collections.IList" />, для которого создается оболочка.
    /// </param>
    /// <returns>
    ///   Оболочка <see cref="T:System.Collections.ArrayList" /> интерфейса <see cref="T:System.Collections.IList" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="list" /> имеет значение <see langword="null" />.
    /// </exception>
    public static ArrayList Adapter(IList list)
    {
      if (list == null)
        throw new ArgumentNullException(nameof (list));
      return (ArrayList) new ArrayList.IListWrapper(list);
    }

    /// <summary>
    ///   Добавляет объект в конец очереди <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Object" />, добавляемый в конец коллекции <see cref="T:System.Collections.ArrayList" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Индекс <see cref="T:System.Collections.ArrayList" />, по которому добавлен параметр <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.ArrayList" /> имеет фиксированный размер.
    /// </exception>
    public virtual int Add(object value)
    {
      if (this._size == this._items.Length)
        this.EnsureCapacity(this._size + 1);
      this._items[this._size] = value;
      ++this._version;
      return this._size++;
    }

    /// <summary>
    ///   Добавляет элементы интерфейса <see cref="T:System.Collections.ICollection" /> в конец списка <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <param name="c">
    ///   Интерфейс <see cref="T:System.Collections.ICollection" />, элементы которого добавляются в конец списка <see cref="T:System.Collections.ArrayList" />.
    ///    Сама коллекция не может иметь значение <see langword="null" />, но может содержать элементы со значением <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="c" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.ArrayList" /> имеет фиксированный размер.
    /// </exception>
    public virtual void AddRange(ICollection c)
    {
      this.InsertRange(this._size, c);
    }

    /// <summary>
    ///   Выполняет поиск элемента в диапазоне элементов отсортированного списка <see cref="T:System.Collections.ArrayList" />, используя указанную функцию сравнения, и возвращает индекс элемента, отсчитываемый от нуля.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс начала диапазона поиска.
    /// </param>
    /// <param name="count">Длина диапазона поиска.</param>
    /// <param name="value">
    ///   Искомый объект <see cref="T:System.Object" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <param name="comparer">
    ///   Реализация интерфейса <see cref="T:System.Collections.IComparer" />, которая используется при сравнении элементов.
    /// 
    ///   -или-
    /// 
    ///   Значение <see langword="null" />, чтобы использовать функцию сравнения по умолчанию, которая является реализацией <see cref="T:System.IComparable" /> для каждого элемента.
    /// </param>
    /// <returns>
    ///   Начинающийся с нуля индекс элемента <paramref name="value" /> в отсортированном списке <see cref="T:System.Collections.ArrayList" />, если элемент <paramref name="value" /> найден; в противном случае — отрицательное число, которое является двоичным дополнением индекса следующего элемента, большего, чем <paramref name="value" />, или, если большего элемента не существует, двоичным дополнением значения <see cref="P:System.Collections.ArrayList.Count" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон в <see cref="T:System.Collections.ArrayList" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="comparer" /> имеет значение <see langword="null" />, и ни <paramref name="value" />, ни элементы <see cref="T:System.Collections.ArrayList" /> не реализуют интерфейс <see cref="T:System.IComparable" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="comparer" /> имеет значение <see langword="null" />, и тип <paramref name="value" /> не совпадает с типом элементов <see cref="T:System.Collections.ArrayList" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    public virtual int BinarySearch(int index, int count, object value, IComparer comparer)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return Array.BinarySearch((Array) this._items, index, count, value, comparer);
    }

    /// <summary>
    ///   Выполняет поиск элемента по всему отсортированному списку <see cref="T:System.Collections.ArrayList" />, используя компаратор по умолчанию, и возвращает индекс элемента, отсчитываемый от нуля.
    /// </summary>
    /// <param name="value">
    ///   Искомый объект <see cref="T:System.Object" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Начинающийся с нуля индекс элемента <paramref name="value" /> в отсортированном списке <see cref="T:System.Collections.ArrayList" />, если элемент <paramref name="value" /> найден; в противном случае — отрицательное число, которое является двоичным дополнением индекса следующего элемента, большего, чем <paramref name="value" />, или, если большего элемента не существует, двоичным дополнением значения <see cref="P:System.Collections.ArrayList.Count" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Ни один из элементов <paramref name="value" /> и <see cref="T:System.Collections.ArrayList" /> не реализуют интерфейс <see cref="T:System.IComparable" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Тип <paramref name="value" /> не совпадает с типом элементов <see cref="T:System.Collections.ArrayList" />.
    /// </exception>
    public virtual int BinarySearch(object value)
    {
      return this.BinarySearch(0, this.Count, value, (IComparer) null);
    }

    /// <summary>
    ///   Выполняет поиск элемента по всему отсортированному списку <see cref="T:System.Collections.ArrayList" />, используя указанный компаратор, и возвращает индекс элемента, отсчитываемый от нуля.
    /// </summary>
    /// <param name="value">
    ///   Искомый объект <see cref="T:System.Object" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <param name="comparer">
    ///   Реализация интерфейса <see cref="T:System.Collections.IComparer" />, которая используется при сравнении элементов.
    /// 
    ///   -или-
    /// 
    ///   Значение <see langword="null" />, чтобы использовать функцию сравнения по умолчанию, которая является реализацией <see cref="T:System.IComparable" /> для каждого элемента.
    /// </param>
    /// <returns>
    ///   Начинающийся с нуля индекс элемента <paramref name="value" /> в отсортированном списке <see cref="T:System.Collections.ArrayList" />, если элемент <paramref name="value" /> найден; в противном случае — отрицательное число, которое является двоичным дополнением индекса следующего элемента, большего, чем <paramref name="value" />, или, если большего элемента не существует, двоичным дополнением значения <see cref="P:System.Collections.ArrayList.Count" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="comparer" /> имеет значение <see langword="null" />, и ни <paramref name="value" />, ни элементы <see cref="T:System.Collections.ArrayList" /> не реализуют интерфейс <see cref="T:System.IComparable" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="comparer" /> имеет значение <see langword="null" />, и тип <paramref name="value" /> не совпадает с типом элементов <see cref="T:System.Collections.ArrayList" />.
    /// </exception>
    public virtual int BinarySearch(object value, IComparer comparer)
    {
      return this.BinarySearch(0, this.Count, value, comparer);
    }

    /// <summary>
    ///   Удаляет все элементы из коллекции <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.ArrayList" /> имеет фиксированный размер.
    /// </exception>
    public virtual void Clear()
    {
      if (this._size > 0)
      {
        Array.Clear((Array) this._items, 0, this._size);
        this._size = 0;
      }
      ++this._version;
    }

    /// <summary>
    ///   Создает неполную копию объекта <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <returns>
    ///   Неполная копия <see cref="T:System.Collections.ArrayList" />.
    /// </returns>
    public virtual object Clone()
    {
      ArrayList arrayList = new ArrayList(this._size);
      arrayList._size = this._size;
      arrayList._version = this._version;
      Array.Copy((Array) this._items, 0, (Array) arrayList._items, 0, this._size);
      return (object) arrayList;
    }

    /// <summary>
    ///   Определяет, входит ли элемент в коллекцию <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <param name="item">
    ///   Объект <see cref="T:System.Object" />, который требуется найти в коллекции <see cref="T:System.Collections.ArrayList" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="item" /> найден в коллекции <see cref="T:System.Collections.ArrayList" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool Contains(object item)
    {
      if (item == null)
      {
        for (int index = 0; index < this._size; ++index)
        {
          if (this._items[index] == null)
            return true;
        }
        return false;
      }
      for (int index = 0; index < this._size; ++index)
      {
        if (this._items[index] != null && this._items[index].Equals(item))
          return true;
      }
      return false;
    }

    /// <summary>
    ///   Копирует весь список <see cref="T:System.Collections.ArrayList" /> в совместимый одномерный массив <see cref="T:System.Array" />, начиная с начального элемента целевого массива.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.ArrayList" />.
    ///    Массив <see cref="T:System.Array" /> должен иметь индексацию, начинающуюся с нуля.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="array" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Число элементов в исходном массиве <see cref="T:System.Collections.ArrayList" /> больше числа элементов, которые может содержать массив назначения <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Тип источника <see cref="T:System.Collections.ArrayList" /> не может быть автоматически приведен к типу массива назначения <paramref name="array" />.
    /// </exception>
    public virtual void CopyTo(Array array)
    {
      this.CopyTo(array, 0);
    }

    /// <summary>
    ///   Копирует целый массив <see cref="T:System.Collections.ArrayList" /> в совместимый одномерный массив <see cref="T:System.Array" />, начиная с заданного индекса целевого массива.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.ArrayList" />.
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
    ///   Число элементов в исходном массиве <see cref="T:System.Collections.ArrayList" /> больше доступного места от положения, заданного значением параметра <paramref name="arrayIndex" />, до конца массива назначения <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Тип исходного массива <see cref="T:System.Collections.ArrayList" /> не может быть автоматически приведен к типу массива назначения <paramref name="array" />.
    /// </exception>
    public virtual void CopyTo(Array array, int arrayIndex)
    {
      if (array != null && array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      Array.Copy((Array) this._items, 0, array, arrayIndex, this._size);
    }

    /// <summary>
    ///   Копирует диапазон элементов из списка <see cref="T:System.Collections.ArrayList" /> в совместимый одномерный массив <see cref="T:System.Array" />, начиная с указанного индекса целевого массива.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс исходного списка <see cref="T:System.Collections.ArrayList" />, с которого начинается копирование.
    /// </param>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.ArrayList" />.
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
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="arrayIndex" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="array" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="index" /> больше или равно значению <see cref="P:System.Collections.ArrayList.Count" /> исходного списка <see cref="T:System.Collections.ArrayList" />.
    /// 
    ///   -или-
    /// 
    ///   Число элементов от <paramref name="index" /> до конца исходного списка <see cref="T:System.Collections.ArrayList" /> больше доступного места от положения, заданного значением параметра <paramref name="arrayIndex" />, до конца массива назначения <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Тип исходного массива <see cref="T:System.Collections.ArrayList" /> не может быть автоматически приведен к типу массива назначения <paramref name="array" />.
    /// </exception>
    public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
    {
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (array != null && array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      Array.Copy((Array) this._items, index, array, arrayIndex, count);
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
    ///   Возвращает оболочку <see cref="T:System.Collections.IList" /> фиксированного размера.
    /// </summary>
    /// <param name="list">
    ///   Класс <see cref="T:System.Collections.IList" />, для которого создается оболочка.
    /// </param>
    /// <returns>
    ///   Оболочка <see cref="T:System.Collections.IList" /> фиксированного размера.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="list" /> имеет значение <see langword="null" />.
    /// </exception>
    public static IList FixedSize(IList list)
    {
      if (list == null)
        throw new ArgumentNullException(nameof (list));
      return (IList) new ArrayList.FixedSizeList(list);
    }

    /// <summary>
    ///   Возвращает оболочку <see cref="T:System.Collections.ArrayList" /> фиксированного размера.
    /// </summary>
    /// <param name="list">
    ///   Класс <see cref="T:System.Collections.ArrayList" />, для которого создается оболочка.
    /// </param>
    /// <returns>
    ///   Оболочка <see cref="T:System.Collections.ArrayList" /> фиксированного размера.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="list" /> имеет значение <see langword="null" />.
    /// </exception>
    public static ArrayList FixedSize(ArrayList list)
    {
      if (list == null)
        throw new ArgumentNullException(nameof (list));
      return (ArrayList) new ArrayList.FixedSizeArrayList(list);
    }

    /// <summary>
    ///   Возвращает перечислитель для всего <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IEnumerator" /> Для всего <see cref="T:System.Collections.ArrayList" />.
    /// </returns>
    public virtual IEnumerator GetEnumerator()
    {
      return (IEnumerator) new ArrayList.ArrayListEnumeratorSimple(this);
    }

    /// <summary>
    ///   Возвращает перечислитель для диапазона элементов в списке <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля начальный индекс участка списка <see cref="T:System.Collections.ArrayList" />, на который должен ссылаться перечислитель.
    /// </param>
    /// <param name="count">
    ///   Количество элементов в участке списка <see cref="T:System.Collections.ArrayList" />, на который должен ссылаться перечислитель.
    /// </param>
    /// <returns>
    ///   Интерфейс <see cref="T:System.Collections.IEnumerator" /> для указанного диапазона элементов списка <see cref="T:System.Collections.ArrayList" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон в <see cref="T:System.Collections.ArrayList" />.
    /// </exception>
    public virtual IEnumerator GetEnumerator(int index, int count)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return (IEnumerator) new ArrayList.ArrayListEnumerator(this, index, count);
    }

    /// <summary>
    ///   Осуществляет поиск указанного объекта <see cref="T:System.Object" /> и возвращает отсчитываемый от нуля индекс первого вхождения в коллекцию <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Object" />, который требуется найти в коллекции <see cref="T:System.Collections.ArrayList" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Индекс (с нуля) первого вхождения параметра <paramref name="value" />, если оно найдено в коллекции <see cref="T:System.Collections.ArrayList" />; в противном случае -1.
    /// </returns>
    public virtual int IndexOf(object value)
    {
      return Array.IndexOf((Array) this._items, value, 0, this._size);
    }

    /// <summary>
    ///   Осуществляет поиск указанного объекта <see cref="T:System.Object" /> и возвращает отсчитываемый от нуля индекс первого вхождения в диапазоне элементов списка <see cref="T:System.Collections.ArrayList" />, начиная с заданного индекса и до последнего элемента.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Object" />, который требуется найти в коллекции <see cref="T:System.Collections.ArrayList" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начальной позиции поиска.
    ///    Значение 0 (ноль) действительно в пустом списке.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения элемента <paramref name="value" /> в диапазоне элементов списка <see cref="T:System.Collections.ArrayList" />, начиная с позиции <paramref name="startIndex" /> и до конца списка, если элемент найден; в противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.ArrayList" />.
    /// </exception>
    public virtual int IndexOf(object value, int startIndex)
    {
      if (startIndex > this._size)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return Array.IndexOf((Array) this._items, value, startIndex, this._size - startIndex);
    }

    /// <summary>
    ///   Выполняет поиск указанного объекта <see cref="T:System.Object" /> и возвращает отсчитываемый от нуля индекс первого вхождения в диапазоне элементов списка <see cref="T:System.Collections.ArrayList" />, который начинается с заданного индекса и содержит указанное число элементов.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Object" />, который требуется найти в коллекции <see cref="T:System.Collections.ArrayList" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начальной позиции поиска.
    ///    Значение 0 (ноль) действительно в пустом списке.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" /> в диапазоне элементов списка <see cref="T:System.Collections.ArrayList" />, который начинается с позиции <paramref name="startIndex" /> и содержит число элементов <paramref name="count" />, если искомый объект найден; в противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.ArrayList" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> и <paramref name="count" /> не задают допустимый раздел в <see cref="T:System.Collections.ArrayList" />.
    /// </exception>
    public virtual int IndexOf(object value, int startIndex, int count)
    {
      if (startIndex > this._size)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > this._size - count)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
      return Array.IndexOf((Array) this._items, value, startIndex, count);
    }

    /// <summary>
    ///   Вставляет элемент в коллекцию <see cref="T:System.Collections.ArrayList" /> по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому следует вставить элемент <paramref name="value" />.
    /// </param>
    /// <param name="value">
    ///   Вставляемый объект <see cref="T:System.Object" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="index" /> больше значения <see cref="P:System.Collections.ArrayList.Count" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.ArrayList" /> имеет фиксированный размер.
    /// </exception>
    public virtual void Insert(int index, object value)
    {
      if (index < 0 || index > this._size)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_ArrayListInsert"));
      if (this._size == this._items.Length)
        this.EnsureCapacity(this._size + 1);
      if (index < this._size)
        Array.Copy((Array) this._items, index, (Array) this._items, index + 1, this._size - index);
      this._items[index] = value;
      ++this._size;
      ++this._version;
    }

    /// <summary>
    ///   Вставляет элементы коллекции в список <see cref="T:System.Collections.ArrayList" /> в позиции с указанным индексом.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс места вставки новых элементов.
    /// </param>
    /// <param name="c">
    ///   Коллекция <see cref="T:System.Collections.ICollection" />, элементы которой требуется вставить в список <see cref="T:System.Collections.ArrayList" />.
    ///    Сама коллекция не может иметь значение <see langword="null" />, но может содержать элементы со значением <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="c" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="index" /> больше значения <see cref="P:System.Collections.ArrayList.Count" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.ArrayList" /> имеет фиксированный размер.
    /// </exception>
    public virtual void InsertRange(int index, ICollection c)
    {
      if (c == null)
        throw new ArgumentNullException(nameof (c), Environment.GetResourceString("ArgumentNull_Collection"));
      if (index < 0 || index > this._size)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      int count = c.Count;
      if (count <= 0)
        return;
      this.EnsureCapacity(this._size + count);
      if (index < this._size)
        Array.Copy((Array) this._items, index, (Array) this._items, index + count, this._size - index);
      object[] objArray = new object[count];
      c.CopyTo((Array) objArray, 0);
      objArray.CopyTo((Array) this._items, index);
      this._size += count;
      ++this._version;
    }

    /// <summary>
    ///   Осуществляет поиск указанного объекта <see cref="T:System.Object" /> и возвращает отсчитываемый от нуля индекс последнего вхождения в коллекцию <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Object" />, который требуется найти в коллекции <see cref="T:System.Collections.ArrayList" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" /> в пределах всего списка <see cref="T:System.Collections.ArrayList" />, если элемент найден. В противном случае — значение –1.
    /// </returns>
    public virtual int LastIndexOf(object value)
    {
      return this.LastIndexOf(value, this._size - 1, this._size);
    }

    /// <summary>
    ///   Осуществляет поиск указанного объекта <see cref="T:System.Object" /> и возвращает отсчитываемый от нуля индекс последнего вхождения в диапазоне элементов списка <see cref="T:System.Collections.ArrayList" />, начиная с первого элемента и заканчивая элементом с заданным индексом.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Object" />, который требуется найти в коллекции <see cref="T:System.Collections.ArrayList" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения элемента <paramref name="value" /> в диапазоне элементов списка <see cref="T:System.Collections.ArrayList" />, начиная с первого элемента и до позиции <paramref name="startIndex" />, если элемент найден; в противном случае — значение -1.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.ArrayList" />.
    /// </exception>
    public virtual int LastIndexOf(object value, int startIndex)
    {
      if (startIndex >= this._size)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return this.LastIndexOf(value, startIndex, startIndex + 1);
    }

    /// <summary>
    ///   Выполняет поиск указанного объекта <see cref="T:System.Object" /> и возвращает отсчитываемый от нуля индекс последнего вхождения в диапазоне элементов списка <see cref="T:System.Collections.ArrayList" />, содержащем указанное число элементов и заканчивающемся в позиции с указанным индексом.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Object" />, который требуется найти в коллекции <see cref="T:System.Collections.ArrayList" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" /> в диапазоне элементов списка <see cref="T:System.Collections.ArrayList" />, состоящем из <paramref name="count" /> элементов и заканчивающемся в позиции <paramref name="startIndex" />, если элемент найден. В противном случае — значение –1.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <see cref="T:System.Collections.ArrayList" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> и <paramref name="count" /> не задают допустимый раздел в <see cref="T:System.Collections.ArrayList" />.
    /// </exception>
    public virtual int LastIndexOf(object value, int startIndex, int count)
    {
      if (this.Count != 0 && (startIndex < 0 || count < 0))
        throw new ArgumentOutOfRangeException(startIndex < 0 ? nameof (startIndex) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size == 0)
        return -1;
      if (startIndex >= this._size || count > startIndex + 1)
        throw new ArgumentOutOfRangeException(startIndex >= this._size ? nameof (startIndex) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_BiggerThanCollection"));
      return Array.LastIndexOf((Array) this._items, value, startIndex, count);
    }

    /// <summary>
    ///   Возвращает программу-оболочку <see cref="T:System.Collections.IList" />, доступную только для чтения.
    /// </summary>
    /// <param name="list">
    ///   Класс <see cref="T:System.Collections.IList" />, для которого создается оболочка.
    /// </param>
    /// <returns>
    ///   Доступная только для чтения программа-оболочка <see cref="T:System.Collections.IList" /> для параметра <paramref name="list" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="list" /> имеет значение <see langword="null" />.
    /// </exception>
    public static IList ReadOnly(IList list)
    {
      if (list == null)
        throw new ArgumentNullException(nameof (list));
      return (IList) new ArrayList.ReadOnlyList(list);
    }

    /// <summary>
    ///   Возвращает программу-оболочку <see cref="T:System.Collections.ArrayList" />, доступную только для чтения.
    /// </summary>
    /// <param name="list">
    ///   Класс <see cref="T:System.Collections.ArrayList" />, для которого создается оболочка.
    /// </param>
    /// <returns>
    ///   Доступная только для чтения программа-оболочка <see cref="T:System.Collections.ArrayList" /> для параметра <paramref name="list" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="list" /> имеет значение <see langword="null" />.
    /// </exception>
    public static ArrayList ReadOnly(ArrayList list)
    {
      if (list == null)
        throw new ArgumentNullException(nameof (list));
      return (ArrayList) new ArrayList.ReadOnlyArrayList(list);
    }

    /// <summary>
    ///   Удаляет первое вхождение указанного объекта из коллекции <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <param name="obj">
    ///   Элемент <see cref="T:System.Object" />, который требуется удалить из <see cref="T:System.Collections.ArrayList" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.ArrayList" /> имеет фиксированный размер.
    /// </exception>
    public virtual void Remove(object obj)
    {
      int index = this.IndexOf(obj);
      if (index < 0)
        return;
      this.RemoveAt(index);
    }

    /// <summary>
    ///   Удаляет элемент списка <see cref="T:System.Collections.ArrayList" /> с указанным индексом.
    /// </summary>
    /// <param name="index">
    ///   Индекс (с нуля) элемента, который требуется удалить.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="index" /> больше или равно значению свойства <see cref="P:System.Collections.ArrayList.Count" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.ArrayList" /> Имеет фиксированный размер.
    /// </exception>
    public virtual void RemoveAt(int index)
    {
      if (index < 0 || index >= this._size)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      --this._size;
      if (index < this._size)
        Array.Copy((Array) this._items, index + 1, (Array) this._items, index, this._size - index);
      this._items[this._size] = (object) null;
      ++this._version;
    }

    /// <summary>
    ///   Удаляет диапазон элементов из списка <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс начала диапазона элементов, которые требуется удалить.
    /// </param>
    /// <param name="count">Число удаляемых элементов.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон элементов в списке <see cref="T:System.Collections.ArrayList" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.ArrayList" /> имеет фиксированный размер.
    /// </exception>
    public virtual void RemoveRange(int index, int count)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (count <= 0)
        return;
      int size = this._size;
      this._size -= count;
      if (index < this._size)
        Array.Copy((Array) this._items, index + count, (Array) this._items, index, this._size - index);
      while (size > this._size)
        this._items[--size] = (object) null;
      ++this._version;
    }

    /// <summary>
    ///   Возвращает список <see cref="T:System.Collections.ArrayList" />, элементы которого являются копиями указанного значения.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Object" />, который требуется скопировать несколько раз в новый список <see cref="T:System.Collections.ArrayList" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <param name="count">
    ///   Количество копий значения <paramref name="value" />.
    /// </param>
    /// <returns>
    ///   Список <see cref="T:System.Collections.ArrayList" /> с количеством элементов, равным <paramref name="count" />, которые являются копиями объекта <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    public static ArrayList Repeat(object value, int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      ArrayList arrayList = new ArrayList(count > 4 ? count : 4);
      for (int index = 0; index < count; ++index)
        arrayList.Add(value);
      return arrayList;
    }

    /// <summary>
    ///   Изменяет порядок элементов во всем списке <see cref="T:System.Collections.ArrayList" /> на обратный.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// </exception>
    public virtual void Reverse()
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
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон элементов в списке <see cref="T:System.Collections.ArrayList" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// </exception>
    public virtual void Reverse(int index, int count)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      Array.Reverse((Array) this._items, index, count);
      ++this._version;
    }

    /// <summary>
    ///   Копирует элементы коллекции в диапазон элементов списка <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс списка <see cref="T:System.Collections.ArrayList" />, с которого начинается копирование элементов коллекции <paramref name="c" />.
    /// </param>
    /// <param name="c">
    ///   Интерфейс <see cref="T:System.Collections.ICollection" />, элементы которого копируются в список <see cref="T:System.Collections.ArrayList" />.
    ///    Сама коллекция не может иметь значение <see langword="null" />, но может содержать элементы со значением <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="index" /> плюс количество элементов в <paramref name="c" /> больше, чем <see cref="P:System.Collections.ArrayList.Count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="c" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// </exception>
    public virtual void SetRange(int index, ICollection c)
    {
      if (c == null)
        throw new ArgumentNullException(nameof (c), Environment.GetResourceString("ArgumentNull_Collection"));
      int count = c.Count;
      if (index < 0 || index > this._size - count)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count <= 0)
        return;
      c.CopyTo((Array) this._items, index);
      ++this._version;
    }

    /// <summary>
    ///   Возвращает список <see cref="T:System.Collections.ArrayList" />, представляющий подмножество элементов исходного списка <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс списка <see cref="T:System.Collections.ArrayList" />, с которого начинается диапазон.
    /// </param>
    /// <param name="count">Число элементов в диапазоне.</param>
    /// <returns>
    ///   Список <see cref="T:System.Collections.ArrayList" />, представляющий подмножество элементов исходного списка <see cref="T:System.Collections.ArrayList" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон элементов в списке <see cref="T:System.Collections.ArrayList" />.
    /// </exception>
    public virtual ArrayList GetRange(int index, int count)
    {
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return (ArrayList) new ArrayList.Range(this, index, count);
    }

    /// <summary>
    ///   Сортирует элементы во всем списке <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// </exception>
    public virtual void Sort()
    {
      this.Sort(0, this.Count, (IComparer) Comparer.Default);
    }

    /// <summary>
    ///   Сортирует элементы во всем списке <see cref="T:System.Collections.ArrayList" /> с помощью указанной функции сравнения.
    /// </summary>
    /// <param name="comparer">
    ///   Реализация интерфейса <see cref="T:System.Collections.IComparer" />, которая используется при сравнении элементов.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования реализации <see cref="T:System.IComparable" /> каждого элемента.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   При сравнении двух элементов возникла ошибка.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see langword="null" /> передается для <paramref name="comparer" />, а элементы в списке не реализуют <see cref="T:System.IComparable" />.
    /// </exception>
    public virtual void Sort(IComparer comparer)
    {
      this.Sort(0, this.Count, comparer);
    }

    /// <summary>
    ///   Сортирует элементы в диапазоне элементов списка <see cref="T:System.Collections.ArrayList" /> с помощью указанной функции сравнения.
    /// </summary>
    /// <param name="index">
    ///   Индекс (с нуля) начала диапазона, который требуется отсортировать.
    /// </param>
    /// <param name="count">Длина диапазона сортировки.</param>
    /// <param name="comparer">
    ///   Реализация интерфейса <see cref="T:System.Collections.IComparer" />, которая используется при сравнении элементов.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования реализации <see cref="T:System.IComparable" /> каждого элемента.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон в <see cref="T:System.Collections.ArrayList" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   При сравнении двух элементов возникла ошибка.
    /// </exception>
    public virtual void Sort(int index, int count, IComparer comparer)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this._size - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      Array.Sort((Array) this._items, index, count, comparer);
      ++this._version;
    }

    /// <summary>
    ///   Возвращает синхронизированную (потокобезопасную) оболочку <see cref="T:System.Collections.IList" />.
    /// </summary>
    /// <param name="list">
    ///   Коллекция <see cref="T:System.Collections.IList" />, которую требуется синхронизировать.
    /// </param>
    /// <returns>
    ///   Синхронизированная (потокобезопасная) оболочка <see cref="T:System.Collections.IList" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="list" /> имеет значение <see langword="null" />.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static IList Synchronized(IList list)
    {
      if (list == null)
        throw new ArgumentNullException(nameof (list));
      return (IList) new ArrayList.SyncIList(list);
    }

    /// <summary>
    ///   Возвращает синхронизированную (потокобезопасную) оболочку <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <param name="list">
    ///   Коллекция <see cref="T:System.Collections.ArrayList" />, которую требуется синхронизировать.
    /// </param>
    /// <returns>
    ///   Синхронизированная (потокобезопасная) оболочка <see cref="T:System.Collections.ArrayList" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="list" /> имеет значение <see langword="null" />.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static ArrayList Synchronized(ArrayList list)
    {
      if (list == null)
        throw new ArgumentNullException(nameof (list));
      return (ArrayList) new ArrayList.SyncArrayList(list);
    }

    /// <summary>
    ///   Копирует элементы списка <see cref="T:System.Collections.ArrayList" /> в новый массив <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    ///   Массив <see cref="T:System.Object" />, содержащий копии элементов списка <see cref="T:System.Collections.ArrayList" />.
    /// </returns>
    public virtual object[] ToArray()
    {
      object[] objArray = new object[this._size];
      Array.Copy((Array) this._items, 0, (Array) objArray, 0, this._size);
      return objArray;
    }

    /// <summary>
    ///   Копирует элементы списка <see cref="T:System.Collections.ArrayList" /> в новый массив с элементами указанного типа.
    /// </summary>
    /// <param name="type">
    ///   Элемент <see cref="T:System.Type" /> массива назначения, используемый для создания и копирования элементов в этот массив.
    /// </param>
    /// <returns>
    ///   Массив элементов указанного типа, содержащий копии элементов массива <see cref="T:System.Collections.ArrayList" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Тип исходного списка <see cref="T:System.Collections.ArrayList" /> не может быть автоматически приведен к указанному типу.
    /// </exception>
    [SecuritySafeCritical]
    public virtual Array ToArray(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      Array instance = Array.UnsafeCreateInstance(type, this._size);
      Array.Copy((Array) this._items, 0, instance, 0, this._size);
      return instance;
    }

    /// <summary>
    ///   Задает значение емкости, равное действительному количеству элементов в <see cref="T:System.Collections.ArrayList" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.ArrayList" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Collections.ArrayList" /> имеет фиксированный размер.
    /// </exception>
    public virtual void TrimToSize()
    {
      this.Capacity = this._size;
    }

    [Serializable]
    private class IListWrapper : ArrayList
    {
      private IList _list;

      internal IListWrapper(IList list)
      {
        this._list = list;
        this._version = 0;
      }

      public override int Capacity
      {
        get
        {
          return this._list.Count;
        }
        set
        {
          if (value < this.Count)
            throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        }
      }

      public override int Count
      {
        get
        {
          return this._list.Count;
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
          return this._list.IsSynchronized;
        }
      }

      public override object this[int index]
      {
        get
        {
          return this._list[index];
        }
        set
        {
          this._list[index] = value;
          ++this._version;
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._list.SyncRoot;
        }
      }

      public override int Add(object obj)
      {
        int num = this._list.Add(obj);
        ++this._version;
        return num;
      }

      public override void AddRange(ICollection c)
      {
        this.InsertRange(this.Count, c);
      }

      public override int BinarySearch(int index, int count, object value, IComparer comparer)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        if (comparer == null)
          comparer = (IComparer) Comparer.Default;
        int num1 = index;
        int num2 = index + count - 1;
        while (num1 <= num2)
        {
          int index1 = (num1 + num2) / 2;
          int num3 = comparer.Compare(value, this._list[index1]);
          if (num3 == 0)
            return index1;
          if (num3 < 0)
            num2 = index1 - 1;
          else
            num1 = index1 + 1;
        }
        return ~num1;
      }

      public override void Clear()
      {
        if (this._list.IsFixedSize)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
        this._list.Clear();
        ++this._version;
      }

      public override object Clone()
      {
        return (object) new ArrayList.IListWrapper(this._list);
      }

      public override bool Contains(object obj)
      {
        return this._list.Contains(obj);
      }

      public override void CopyTo(Array array, int index)
      {
        this._list.CopyTo(array, index);
      }

      public override void CopyTo(int index, Array array, int arrayIndex, int count)
      {
        if (array == null)
          throw new ArgumentNullException(nameof (array));
        if (index < 0 || arrayIndex < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (arrayIndex), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (count < 0)
          throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (array.Length - arrayIndex < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        if (array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        if (this._list.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        for (int index1 = index; index1 < index + count; ++index1)
          array.SetValue(this._list[index1], arrayIndex++);
      }

      public override IEnumerator GetEnumerator()
      {
        return this._list.GetEnumerator();
      }

      public override IEnumerator GetEnumerator(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._list.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        return (IEnumerator) new ArrayList.IListWrapper.IListWrapperEnumWrapper(this, index, count);
      }

      public override int IndexOf(object value)
      {
        return this._list.IndexOf(value);
      }

      public override int IndexOf(object value, int startIndex)
      {
        return this.IndexOf(value, startIndex, this._list.Count - startIndex);
      }

      public override int IndexOf(object value, int startIndex, int count)
      {
        if (startIndex < 0 || startIndex > this.Count)
          throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (count < 0 || startIndex > this.Count - count)
          throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
        int num = startIndex + count;
        if (value == null)
        {
          for (int index = startIndex; index < num; ++index)
          {
            if (this._list[index] == null)
              return index;
          }
          return -1;
        }
        for (int index = startIndex; index < num; ++index)
        {
          if (this._list[index] != null && this._list[index].Equals(value))
            return index;
        }
        return -1;
      }

      public override void Insert(int index, object obj)
      {
        this._list.Insert(index, obj);
        ++this._version;
      }

      public override void InsertRange(int index, ICollection c)
      {
        if (c == null)
          throw new ArgumentNullException(nameof (c), Environment.GetResourceString("ArgumentNull_Collection"));
        if (index < 0 || index > this.Count)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (c.Count <= 0)
          return;
        ArrayList list = this._list as ArrayList;
        if (list != null)
        {
          list.InsertRange(index, c);
        }
        else
        {
          foreach (object obj in (IEnumerable) c)
            this._list.Insert(index++, obj);
        }
        ++this._version;
      }

      public override int LastIndexOf(object value)
      {
        return this.LastIndexOf(value, this._list.Count - 1, this._list.Count);
      }

      public override int LastIndexOf(object value, int startIndex)
      {
        return this.LastIndexOf(value, startIndex, startIndex + 1);
      }

      public override int LastIndexOf(object value, int startIndex, int count)
      {
        if (this._list.Count == 0)
          return -1;
        if (startIndex < 0 || startIndex >= this._list.Count)
          throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (count < 0 || count > startIndex + 1)
          throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
        int num = startIndex - count + 1;
        if (value == null)
        {
          for (int index = startIndex; index >= num; --index)
          {
            if (this._list[index] == null)
              return index;
          }
          return -1;
        }
        for (int index = startIndex; index >= num; --index)
        {
          if (this._list[index] != null && this._list[index].Equals(value))
            return index;
        }
        return -1;
      }

      public override void Remove(object value)
      {
        int index = this.IndexOf(value);
        if (index < 0)
          return;
        this.RemoveAt(index);
      }

      public override void RemoveAt(int index)
      {
        this._list.RemoveAt(index);
        ++this._version;
      }

      public override void RemoveRange(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._list.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        if (count > 0)
          ++this._version;
        for (; count > 0; --count)
          this._list.RemoveAt(index);
      }

      public override void Reverse(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._list.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        int index1 = index;
        object obj;
        for (int index2 = index + count - 1; index1 < index2; this._list[index2--] = obj)
        {
          obj = this._list[index1];
          this._list[index1++] = this._list[index2];
        }
        ++this._version;
      }

      public override void SetRange(int index, ICollection c)
      {
        if (c == null)
          throw new ArgumentNullException(nameof (c), Environment.GetResourceString("ArgumentNull_Collection"));
        if (index < 0 || index > this._list.Count - c.Count)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (c.Count <= 0)
          return;
        foreach (object obj in (IEnumerable) c)
          this._list[index++] = obj;
        ++this._version;
      }

      public override ArrayList GetRange(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._list.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        return (ArrayList) new ArrayList.Range((ArrayList) this, index, count);
      }

      public override void Sort(int index, int count, IComparer comparer)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._list.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        object[] objArray = new object[count];
        this.CopyTo(index, (Array) objArray, 0, count);
        Array.Sort((Array) objArray, 0, count, comparer);
        for (int index1 = 0; index1 < count; ++index1)
          this._list[index1 + index] = objArray[index1];
        ++this._version;
      }

      public override object[] ToArray()
      {
        object[] objArray = new object[this.Count];
        this._list.CopyTo((Array) objArray, 0);
        return objArray;
      }

      [SecuritySafeCritical]
      public override Array ToArray(Type type)
      {
        if (type == (Type) null)
          throw new ArgumentNullException(nameof (type));
        Array instance = Array.UnsafeCreateInstance(type, this._list.Count);
        this._list.CopyTo(instance, 0);
        return instance;
      }

      public override void TrimToSize()
      {
      }

      [Serializable]
      private sealed class IListWrapperEnumWrapper : IEnumerator, ICloneable
      {
        private IEnumerator _en;
        private int _remaining;
        private int _initialStartIndex;
        private int _initialCount;
        private bool _firstCall;

        private IListWrapperEnumWrapper()
        {
        }

        internal IListWrapperEnumWrapper(ArrayList.IListWrapper listWrapper, int startIndex, int count)
        {
          this._en = listWrapper.GetEnumerator();
          this._initialStartIndex = startIndex;
          this._initialCount = count;
          do
            ;
          while (startIndex-- > 0 && this._en.MoveNext());
          this._remaining = count;
          this._firstCall = true;
        }

        public object Clone()
        {
          return (object) new ArrayList.IListWrapper.IListWrapperEnumWrapper()
          {
            _en = (IEnumerator) ((ICloneable) this._en).Clone(),
            _initialStartIndex = this._initialStartIndex,
            _initialCount = this._initialCount,
            _remaining = this._remaining,
            _firstCall = this._firstCall
          };
        }

        public bool MoveNext()
        {
          if (this._firstCall)
          {
            this._firstCall = false;
            if (this._remaining-- > 0)
              return this._en.MoveNext();
            return false;
          }
          if (this._remaining < 0 || !this._en.MoveNext())
            return false;
          return this._remaining-- > 0;
        }

        public object Current
        {
          get
          {
            if (this._firstCall)
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
            if (this._remaining < 0)
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
            return this._en.Current;
          }
        }

        public void Reset()
        {
          this._en.Reset();
          int initialStartIndex = this._initialStartIndex;
          do
            ;
          while (initialStartIndex-- > 0 && this._en.MoveNext());
          this._remaining = this._initialCount;
          this._firstCall = true;
        }
      }
    }

    [Serializable]
    private class SyncArrayList : ArrayList
    {
      private ArrayList _list;
      private object _root;

      internal SyncArrayList(ArrayList list)
        : base(false)
      {
        this._list = list;
        this._root = list.SyncRoot;
      }

      public override int Capacity
      {
        get
        {
          lock (this._root)
            return this._list.Capacity;
        }
        set
        {
          lock (this._root)
            this._list.Capacity = value;
        }
      }

      public override int Count
      {
        get
        {
          lock (this._root)
            return this._list.Count;
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

      public override object this[int index]
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

      public override object SyncRoot
      {
        get
        {
          return this._root;
        }
      }

      public override int Add(object value)
      {
        lock (this._root)
          return this._list.Add(value);
      }

      public override void AddRange(ICollection c)
      {
        lock (this._root)
          this._list.AddRange(c);
      }

      public override int BinarySearch(object value)
      {
        lock (this._root)
          return this._list.BinarySearch(value);
      }

      public override int BinarySearch(object value, IComparer comparer)
      {
        lock (this._root)
          return this._list.BinarySearch(value, comparer);
      }

      public override int BinarySearch(int index, int count, object value, IComparer comparer)
      {
        lock (this._root)
          return this._list.BinarySearch(index, count, value, comparer);
      }

      public override void Clear()
      {
        lock (this._root)
          this._list.Clear();
      }

      public override object Clone()
      {
        lock (this._root)
          return (object) new ArrayList.SyncArrayList((ArrayList) this._list.Clone());
      }

      public override bool Contains(object item)
      {
        lock (this._root)
          return this._list.Contains(item);
      }

      public override void CopyTo(Array array)
      {
        lock (this._root)
          this._list.CopyTo(array);
      }

      public override void CopyTo(Array array, int index)
      {
        lock (this._root)
          this._list.CopyTo(array, index);
      }

      public override void CopyTo(int index, Array array, int arrayIndex, int count)
      {
        lock (this._root)
          this._list.CopyTo(index, array, arrayIndex, count);
      }

      public override IEnumerator GetEnumerator()
      {
        lock (this._root)
          return this._list.GetEnumerator();
      }

      public override IEnumerator GetEnumerator(int index, int count)
      {
        lock (this._root)
          return this._list.GetEnumerator(index, count);
      }

      public override int IndexOf(object value)
      {
        lock (this._root)
          return this._list.IndexOf(value);
      }

      public override int IndexOf(object value, int startIndex)
      {
        lock (this._root)
          return this._list.IndexOf(value, startIndex);
      }

      public override int IndexOf(object value, int startIndex, int count)
      {
        lock (this._root)
          return this._list.IndexOf(value, startIndex, count);
      }

      public override void Insert(int index, object value)
      {
        lock (this._root)
          this._list.Insert(index, value);
      }

      public override void InsertRange(int index, ICollection c)
      {
        lock (this._root)
          this._list.InsertRange(index, c);
      }

      public override int LastIndexOf(object value)
      {
        lock (this._root)
          return this._list.LastIndexOf(value);
      }

      public override int LastIndexOf(object value, int startIndex)
      {
        lock (this._root)
          return this._list.LastIndexOf(value, startIndex);
      }

      public override int LastIndexOf(object value, int startIndex, int count)
      {
        lock (this._root)
          return this._list.LastIndexOf(value, startIndex, count);
      }

      public override void Remove(object value)
      {
        lock (this._root)
          this._list.Remove(value);
      }

      public override void RemoveAt(int index)
      {
        lock (this._root)
          this._list.RemoveAt(index);
      }

      public override void RemoveRange(int index, int count)
      {
        lock (this._root)
          this._list.RemoveRange(index, count);
      }

      public override void Reverse(int index, int count)
      {
        lock (this._root)
          this._list.Reverse(index, count);
      }

      public override void SetRange(int index, ICollection c)
      {
        lock (this._root)
          this._list.SetRange(index, c);
      }

      public override ArrayList GetRange(int index, int count)
      {
        lock (this._root)
          return this._list.GetRange(index, count);
      }

      public override void Sort()
      {
        lock (this._root)
          this._list.Sort();
      }

      public override void Sort(IComparer comparer)
      {
        lock (this._root)
          this._list.Sort(comparer);
      }

      public override void Sort(int index, int count, IComparer comparer)
      {
        lock (this._root)
          this._list.Sort(index, count, comparer);
      }

      public override object[] ToArray()
      {
        lock (this._root)
          return this._list.ToArray();
      }

      public override Array ToArray(Type type)
      {
        lock (this._root)
          return this._list.ToArray(type);
      }

      public override void TrimToSize()
      {
        lock (this._root)
          this._list.TrimToSize();
      }
    }

    [Serializable]
    private class SyncIList : IList, ICollection, IEnumerable
    {
      private IList _list;
      private object _root;

      internal SyncIList(IList list)
      {
        this._list = list;
        this._root = list.SyncRoot;
      }

      public virtual int Count
      {
        get
        {
          lock (this._root)
            return this._list.Count;
        }
      }

      public virtual bool IsReadOnly
      {
        get
        {
          return this._list.IsReadOnly;
        }
      }

      public virtual bool IsFixedSize
      {
        get
        {
          return this._list.IsFixedSize;
        }
      }

      public virtual bool IsSynchronized
      {
        get
        {
          return true;
        }
      }

      public virtual object this[int index]
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

      public virtual object SyncRoot
      {
        get
        {
          return this._root;
        }
      }

      public virtual int Add(object value)
      {
        lock (this._root)
          return this._list.Add(value);
      }

      public virtual void Clear()
      {
        lock (this._root)
          this._list.Clear();
      }

      public virtual bool Contains(object item)
      {
        lock (this._root)
          return this._list.Contains(item);
      }

      public virtual void CopyTo(Array array, int index)
      {
        lock (this._root)
          this._list.CopyTo(array, index);
      }

      public virtual IEnumerator GetEnumerator()
      {
        lock (this._root)
          return this._list.GetEnumerator();
      }

      public virtual int IndexOf(object value)
      {
        lock (this._root)
          return this._list.IndexOf(value);
      }

      public virtual void Insert(int index, object value)
      {
        lock (this._root)
          this._list.Insert(index, value);
      }

      public virtual void Remove(object value)
      {
        lock (this._root)
          this._list.Remove(value);
      }

      public virtual void RemoveAt(int index)
      {
        lock (this._root)
          this._list.RemoveAt(index);
      }
    }

    [Serializable]
    private class FixedSizeList : IList, ICollection, IEnumerable
    {
      private IList _list;

      internal FixedSizeList(IList l)
      {
        this._list = l;
      }

      public virtual int Count
      {
        get
        {
          return this._list.Count;
        }
      }

      public virtual bool IsReadOnly
      {
        get
        {
          return this._list.IsReadOnly;
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
          return this._list.IsSynchronized;
        }
      }

      public virtual object this[int index]
      {
        get
        {
          return this._list[index];
        }
        set
        {
          this._list[index] = value;
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this._list.SyncRoot;
        }
      }

      public virtual int Add(object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public virtual void Clear()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public virtual bool Contains(object obj)
      {
        return this._list.Contains(obj);
      }

      public virtual void CopyTo(Array array, int index)
      {
        this._list.CopyTo(array, index);
      }

      public virtual IEnumerator GetEnumerator()
      {
        return this._list.GetEnumerator();
      }

      public virtual int IndexOf(object value)
      {
        return this._list.IndexOf(value);
      }

      public virtual void Insert(int index, object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public virtual void Remove(object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public virtual void RemoveAt(int index)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }
    }

    [Serializable]
    private class FixedSizeArrayList : ArrayList
    {
      private ArrayList _list;

      internal FixedSizeArrayList(ArrayList l)
      {
        this._list = l;
        this._version = this._list._version;
      }

      public override int Count
      {
        get
        {
          return this._list.Count;
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
          return true;
        }
      }

      public override bool IsSynchronized
      {
        get
        {
          return this._list.IsSynchronized;
        }
      }

      public override object this[int index]
      {
        get
        {
          return this._list[index];
        }
        set
        {
          this._list[index] = value;
          this._version = this._list._version;
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._list.SyncRoot;
        }
      }

      public override int Add(object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override void AddRange(ICollection c)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override int BinarySearch(int index, int count, object value, IComparer comparer)
      {
        return this._list.BinarySearch(index, count, value, comparer);
      }

      public override int Capacity
      {
        get
        {
          return this._list.Capacity;
        }
        set
        {
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
        }
      }

      public override void Clear()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override object Clone()
      {
        return (object) new ArrayList.FixedSizeArrayList(this._list)
        {
          _list = (ArrayList) this._list.Clone()
        };
      }

      public override bool Contains(object obj)
      {
        return this._list.Contains(obj);
      }

      public override void CopyTo(Array array, int index)
      {
        this._list.CopyTo(array, index);
      }

      public override void CopyTo(int index, Array array, int arrayIndex, int count)
      {
        this._list.CopyTo(index, array, arrayIndex, count);
      }

      public override IEnumerator GetEnumerator()
      {
        return this._list.GetEnumerator();
      }

      public override IEnumerator GetEnumerator(int index, int count)
      {
        return this._list.GetEnumerator(index, count);
      }

      public override int IndexOf(object value)
      {
        return this._list.IndexOf(value);
      }

      public override int IndexOf(object value, int startIndex)
      {
        return this._list.IndexOf(value, startIndex);
      }

      public override int IndexOf(object value, int startIndex, int count)
      {
        return this._list.IndexOf(value, startIndex, count);
      }

      public override void Insert(int index, object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override void InsertRange(int index, ICollection c)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override int LastIndexOf(object value)
      {
        return this._list.LastIndexOf(value);
      }

      public override int LastIndexOf(object value, int startIndex)
      {
        return this._list.LastIndexOf(value, startIndex);
      }

      public override int LastIndexOf(object value, int startIndex, int count)
      {
        return this._list.LastIndexOf(value, startIndex, count);
      }

      public override void Remove(object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override void RemoveAt(int index)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override void RemoveRange(int index, int count)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }

      public override void SetRange(int index, ICollection c)
      {
        this._list.SetRange(index, c);
        this._version = this._list._version;
      }

      public override ArrayList GetRange(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        return (ArrayList) new ArrayList.Range((ArrayList) this, index, count);
      }

      public override void Reverse(int index, int count)
      {
        this._list.Reverse(index, count);
        this._version = this._list._version;
      }

      public override void Sort(int index, int count, IComparer comparer)
      {
        this._list.Sort(index, count, comparer);
        this._version = this._list._version;
      }

      public override object[] ToArray()
      {
        return this._list.ToArray();
      }

      public override Array ToArray(Type type)
      {
        return this._list.ToArray(type);
      }

      public override void TrimToSize()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
      }
    }

    [Serializable]
    private class ReadOnlyList : IList, ICollection, IEnumerable
    {
      private IList _list;

      internal ReadOnlyList(IList l)
      {
        this._list = l;
      }

      public virtual int Count
      {
        get
        {
          return this._list.Count;
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
          return this._list.IsSynchronized;
        }
      }

      public virtual object this[int index]
      {
        get
        {
          return this._list[index];
        }
        set
        {
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
        }
      }

      public virtual object SyncRoot
      {
        get
        {
          return this._list.SyncRoot;
        }
      }

      public virtual int Add(object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public virtual void Clear()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public virtual bool Contains(object obj)
      {
        return this._list.Contains(obj);
      }

      public virtual void CopyTo(Array array, int index)
      {
        this._list.CopyTo(array, index);
      }

      public virtual IEnumerator GetEnumerator()
      {
        return this._list.GetEnumerator();
      }

      public virtual int IndexOf(object value)
      {
        return this._list.IndexOf(value);
      }

      public virtual void Insert(int index, object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public virtual void Remove(object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public virtual void RemoveAt(int index)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }
    }

    [Serializable]
    private class ReadOnlyArrayList : ArrayList
    {
      private ArrayList _list;

      internal ReadOnlyArrayList(ArrayList l)
      {
        this._list = l;
      }

      public override int Count
      {
        get
        {
          return this._list.Count;
        }
      }

      public override bool IsReadOnly
      {
        get
        {
          return true;
        }
      }

      public override bool IsFixedSize
      {
        get
        {
          return true;
        }
      }

      public override bool IsSynchronized
      {
        get
        {
          return this._list.IsSynchronized;
        }
      }

      public override object this[int index]
      {
        get
        {
          return this._list[index];
        }
        set
        {
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this._list.SyncRoot;
        }
      }

      public override int Add(object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override void AddRange(ICollection c)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override int BinarySearch(int index, int count, object value, IComparer comparer)
      {
        return this._list.BinarySearch(index, count, value, comparer);
      }

      public override int Capacity
      {
        get
        {
          return this._list.Capacity;
        }
        set
        {
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
        }
      }

      public override void Clear()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override object Clone()
      {
        return (object) new ArrayList.ReadOnlyArrayList(this._list)
        {
          _list = (ArrayList) this._list.Clone()
        };
      }

      public override bool Contains(object obj)
      {
        return this._list.Contains(obj);
      }

      public override void CopyTo(Array array, int index)
      {
        this._list.CopyTo(array, index);
      }

      public override void CopyTo(int index, Array array, int arrayIndex, int count)
      {
        this._list.CopyTo(index, array, arrayIndex, count);
      }

      public override IEnumerator GetEnumerator()
      {
        return this._list.GetEnumerator();
      }

      public override IEnumerator GetEnumerator(int index, int count)
      {
        return this._list.GetEnumerator(index, count);
      }

      public override int IndexOf(object value)
      {
        return this._list.IndexOf(value);
      }

      public override int IndexOf(object value, int startIndex)
      {
        return this._list.IndexOf(value, startIndex);
      }

      public override int IndexOf(object value, int startIndex, int count)
      {
        return this._list.IndexOf(value, startIndex, count);
      }

      public override void Insert(int index, object obj)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override void InsertRange(int index, ICollection c)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override int LastIndexOf(object value)
      {
        return this._list.LastIndexOf(value);
      }

      public override int LastIndexOf(object value, int startIndex)
      {
        return this._list.LastIndexOf(value, startIndex);
      }

      public override int LastIndexOf(object value, int startIndex, int count)
      {
        return this._list.LastIndexOf(value, startIndex, count);
      }

      public override void Remove(object value)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override void RemoveAt(int index)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override void RemoveRange(int index, int count)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override void SetRange(int index, ICollection c)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override ArrayList GetRange(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this.Count - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        return (ArrayList) new ArrayList.Range((ArrayList) this, index, count);
      }

      public override void Reverse(int index, int count)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override void Sort(int index, int count, IComparer comparer)
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }

      public override object[] ToArray()
      {
        return this._list.ToArray();
      }

      public override Array ToArray(Type type)
      {
        return this._list.ToArray(type);
      }

      public override void TrimToSize()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
      }
    }

    [Serializable]
    private sealed class ArrayListEnumerator : IEnumerator, ICloneable
    {
      private ArrayList list;
      private int index;
      private int endIndex;
      private int version;
      private object currentElement;
      private int startIndex;

      internal ArrayListEnumerator(ArrayList list, int index, int count)
      {
        this.list = list;
        this.startIndex = index;
        this.index = index - 1;
        this.endIndex = this.index + count;
        this.version = list._version;
        this.currentElement = (object) null;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public bool MoveNext()
      {
        if (this.version != this.list._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this.index < this.endIndex)
        {
          this.currentElement = this.list[++this.index];
          return true;
        }
        this.index = this.endIndex + 1;
        return false;
      }

      public object Current
      {
        get
        {
          if (this.index < this.startIndex)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this.index > this.endIndex)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          return this.currentElement;
        }
      }

      public void Reset()
      {
        if (this.version != this.list._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this.index = this.startIndex - 1;
      }
    }

    [Serializable]
    private class Range : ArrayList
    {
      private ArrayList _baseList;
      private int _baseIndex;
      private int _baseSize;
      private int _baseVersion;

      internal Range(ArrayList list, int index, int count)
        : base(false)
      {
        this._baseList = list;
        this._baseIndex = index;
        this._baseSize = count;
        this._baseVersion = list._version;
        this._version = list._version;
      }

      private void InternalUpdateRange()
      {
        if (this._baseVersion != this._baseList._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnderlyingArrayListChanged"));
      }

      private void InternalUpdateVersion()
      {
        ++this._baseVersion;
        ++this._version;
      }

      public override int Add(object value)
      {
        this.InternalUpdateRange();
        this._baseList.Insert(this._baseIndex + this._baseSize, value);
        this.InternalUpdateVersion();
        return this._baseSize++;
      }

      public override void AddRange(ICollection c)
      {
        if (c == null)
          throw new ArgumentNullException(nameof (c));
        this.InternalUpdateRange();
        int count = c.Count;
        if (count <= 0)
          return;
        this._baseList.InsertRange(this._baseIndex + this._baseSize, c);
        this.InternalUpdateVersion();
        this._baseSize += count;
      }

      public override int BinarySearch(int index, int count, object value, IComparer comparer)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        int num = this._baseList.BinarySearch(this._baseIndex + index, count, value, comparer);
        if (num >= 0)
          return num - this._baseIndex;
        return num + this._baseIndex;
      }

      public override int Capacity
      {
        get
        {
          return this._baseList.Capacity;
        }
        set
        {
          if (value < this.Count)
            throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        }
      }

      public override void Clear()
      {
        this.InternalUpdateRange();
        if (this._baseSize == 0)
          return;
        this._baseList.RemoveRange(this._baseIndex, this._baseSize);
        this.InternalUpdateVersion();
        this._baseSize = 0;
      }

      public override object Clone()
      {
        this.InternalUpdateRange();
        return (object) new ArrayList.Range(this._baseList, this._baseIndex, this._baseSize)
        {
          _baseList = (ArrayList) this._baseList.Clone()
        };
      }

      public override bool Contains(object item)
      {
        this.InternalUpdateRange();
        if (item == null)
        {
          for (int index = 0; index < this._baseSize; ++index)
          {
            if (this._baseList[this._baseIndex + index] == null)
              return true;
          }
          return false;
        }
        for (int index = 0; index < this._baseSize; ++index)
        {
          if (this._baseList[this._baseIndex + index] != null && this._baseList[this._baseIndex + index].Equals(item))
            return true;
        }
        return false;
      }

      public override void CopyTo(Array array, int index)
      {
        if (array == null)
          throw new ArgumentNullException(nameof (array));
        if (array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        if (index < 0)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (array.Length - index < this._baseSize)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        this._baseList.CopyTo(this._baseIndex, array, index, this._baseSize);
      }

      public override void CopyTo(int index, Array array, int arrayIndex, int count)
      {
        if (array == null)
          throw new ArgumentNullException(nameof (array));
        if (array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (array.Length - arrayIndex < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        this._baseList.CopyTo(this._baseIndex + index, array, arrayIndex, count);
      }

      public override int Count
      {
        get
        {
          this.InternalUpdateRange();
          return this._baseSize;
        }
      }

      public override bool IsReadOnly
      {
        get
        {
          return this._baseList.IsReadOnly;
        }
      }

      public override bool IsFixedSize
      {
        get
        {
          return this._baseList.IsFixedSize;
        }
      }

      public override bool IsSynchronized
      {
        get
        {
          return this._baseList.IsSynchronized;
        }
      }

      public override IEnumerator GetEnumerator()
      {
        return this.GetEnumerator(0, this._baseSize);
      }

      public override IEnumerator GetEnumerator(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        return this._baseList.GetEnumerator(this._baseIndex + index, count);
      }

      public override ArrayList GetRange(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        return (ArrayList) new ArrayList.Range((ArrayList) this, index, count);
      }

      public override object SyncRoot
      {
        get
        {
          return this._baseList.SyncRoot;
        }
      }

      public override int IndexOf(object value)
      {
        this.InternalUpdateRange();
        int num = this._baseList.IndexOf(value, this._baseIndex, this._baseSize);
        if (num >= 0)
          return num - this._baseIndex;
        return -1;
      }

      public override int IndexOf(object value, int startIndex)
      {
        if (startIndex < 0)
          throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (startIndex > this._baseSize)
          throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        this.InternalUpdateRange();
        int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, this._baseSize - startIndex);
        if (num >= 0)
          return num - this._baseIndex;
        return -1;
      }

      public override int IndexOf(object value, int startIndex, int count)
      {
        if (startIndex < 0 || startIndex > this._baseSize)
          throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (count < 0 || startIndex > this._baseSize - count)
          throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
        this.InternalUpdateRange();
        int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, count);
        if (num >= 0)
          return num - this._baseIndex;
        return -1;
      }

      public override void Insert(int index, object value)
      {
        if (index < 0 || index > this._baseSize)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        this.InternalUpdateRange();
        this._baseList.Insert(this._baseIndex + index, value);
        this.InternalUpdateVersion();
        ++this._baseSize;
      }

      public override void InsertRange(int index, ICollection c)
      {
        if (index < 0 || index > this._baseSize)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (c == null)
          throw new ArgumentNullException(nameof (c));
        this.InternalUpdateRange();
        int count = c.Count;
        if (count <= 0)
          return;
        this._baseList.InsertRange(this._baseIndex + index, c);
        this._baseSize += count;
        this.InternalUpdateVersion();
      }

      public override int LastIndexOf(object value)
      {
        this.InternalUpdateRange();
        int num = this._baseList.LastIndexOf(value, this._baseIndex + this._baseSize - 1, this._baseSize);
        if (num >= 0)
          return num - this._baseIndex;
        return -1;
      }

      public override int LastIndexOf(object value, int startIndex)
      {
        return this.LastIndexOf(value, startIndex, startIndex + 1);
      }

      public override int LastIndexOf(object value, int startIndex, int count)
      {
        this.InternalUpdateRange();
        if (this._baseSize == 0)
          return -1;
        if (startIndex >= this._baseSize)
          throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        if (startIndex < 0)
          throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        int num = this._baseList.LastIndexOf(value, this._baseIndex + startIndex, count);
        if (num >= 0)
          return num - this._baseIndex;
        return -1;
      }

      public override void RemoveAt(int index)
      {
        if (index < 0 || index >= this._baseSize)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        this.InternalUpdateRange();
        this._baseList.RemoveAt(this._baseIndex + index);
        this.InternalUpdateVersion();
        --this._baseSize;
      }

      public override void RemoveRange(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        if (count <= 0)
          return;
        this._baseList.RemoveRange(this._baseIndex + index, count);
        this.InternalUpdateVersion();
        this._baseSize -= count;
      }

      public override void Reverse(int index, int count)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        this._baseList.Reverse(this._baseIndex + index, count);
        this.InternalUpdateVersion();
      }

      public override void SetRange(int index, ICollection c)
      {
        this.InternalUpdateRange();
        if (index < 0 || index >= this._baseSize)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        this._baseList.SetRange(this._baseIndex + index, c);
        if (c.Count <= 0)
          return;
        this.InternalUpdateVersion();
      }

      public override void Sort(int index, int count, IComparer comparer)
      {
        if (index < 0 || count < 0)
          throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (this._baseSize - index < count)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        this.InternalUpdateRange();
        this._baseList.Sort(this._baseIndex + index, count, comparer);
        this.InternalUpdateVersion();
      }

      public override object this[int index]
      {
        get
        {
          this.InternalUpdateRange();
          if (index < 0 || index >= this._baseSize)
            throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
          return this._baseList[this._baseIndex + index];
        }
        set
        {
          this.InternalUpdateRange();
          if (index < 0 || index >= this._baseSize)
            throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
          this._baseList[this._baseIndex + index] = value;
          this.InternalUpdateVersion();
        }
      }

      public override object[] ToArray()
      {
        this.InternalUpdateRange();
        object[] objArray = new object[this._baseSize];
        Array.Copy((Array) this._baseList._items, this._baseIndex, (Array) objArray, 0, this._baseSize);
        return objArray;
      }

      [SecuritySafeCritical]
      public override Array ToArray(Type type)
      {
        if (type == (Type) null)
          throw new ArgumentNullException(nameof (type));
        this.InternalUpdateRange();
        Array instance = Array.UnsafeCreateInstance(type, this._baseSize);
        this._baseList.CopyTo(this._baseIndex, instance, 0, this._baseSize);
        return instance;
      }

      public override void TrimToSize()
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RangeCollection"));
      }
    }

    [Serializable]
    private sealed class ArrayListEnumeratorSimple : IEnumerator, ICloneable
    {
      private static object dummyObject = new object();
      private ArrayList list;
      private int index;
      private int version;
      private object currentElement;
      [NonSerialized]
      private bool isArrayList;

      internal ArrayListEnumeratorSimple(ArrayList list)
      {
        this.list = list;
        this.index = -1;
        this.version = list._version;
        this.isArrayList = list.GetType() == typeof (ArrayList);
        this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public bool MoveNext()
      {
        if (this.version != this.list._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this.isArrayList)
        {
          if (this.index < this.list._size - 1)
          {
            this.currentElement = this.list._items[++this.index];
            return true;
          }
          this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
          this.index = this.list._size;
          return false;
        }
        if (this.index < this.list.Count - 1)
        {
          this.currentElement = this.list[++this.index];
          return true;
        }
        this.index = this.list.Count;
        this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
        return false;
      }

      public object Current
      {
        get
        {
          object currentElement = this.currentElement;
          if (ArrayList.ArrayListEnumeratorSimple.dummyObject != currentElement)
            return currentElement;
          if (this.index == -1)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
        }
      }

      public void Reset()
      {
        if (this.version != this.list._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this.currentElement = ArrayList.ArrayListEnumeratorSimple.dummyObject;
        this.index = -1;
      }
    }

    internal class ArrayListDebugView
    {
      private ArrayList arrayList;

      public ArrayListDebugView(ArrayList arrayList)
      {
        if (arrayList == null)
          throw new ArgumentNullException(nameof (arrayList));
        this.arrayList = arrayList;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public object[] Items
      {
        get
        {
          return this.arrayList.ToArray();
        }
      }
    }
  }
}
