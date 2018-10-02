// Decompiled with JetBrains decompiler
// Type: System.Collections.Queue
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
  /// <summary>
  ///   Представляет коллекцию объектов, основанную на принципе "первым поступил — первым обслужен".
  /// </summary>
  [DebuggerTypeProxy(typeof (Queue.QueueDebugView))]
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(true)]
  [Serializable]
  public class Queue : ICollection, IEnumerable, ICloneable
  {
    private object[] _array;
    private int _head;
    private int _tail;
    private int _size;
    private int _growFactor;
    private int _version;
    [NonSerialized]
    private object _syncRoot;
    private const int _MinimumGrow = 4;
    private const int _ShrinkThreshold = 32;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Queue" /> класс, который является пустым, обладает начальной емкостью по умолчанию и использует по умолчанию коэффициентом роста.
    /// </summary>
    public Queue()
      : this(32, 2f)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Queue" /> класс, который является пустым, обладает указанной начальной емкостью и коэффициентом роста по умолчанию.
    /// </summary>
    /// <param name="capacity">
    ///   Начальное количество элементов, которое может содержать коллекция <see cref="T:System.Collections.Queue" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// </exception>
    public Queue(int capacity)
      : this(capacity, 2f)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Queue" /> класс, который является пустым, обладает указанной начальной емкостью и указанным инкрементом.
    /// </summary>
    /// <param name="capacity">
    ///   Начальное количество элементов, которое может содержать коллекция <see cref="T:System.Collections.Queue" />.
    /// </param>
    /// <param name="growFactor">
    ///   Коэффициент мощности <see cref="T:System.Collections.Queue" /> расширяется.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="capacity" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="growFactor" /> — меньше 1.0 или больше 10.0.
    /// </exception>
    public Queue(int capacity, float growFactor)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException(nameof (capacity), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if ((double) growFactor < 1.0 || (double) growFactor > 10.0)
        throw new ArgumentOutOfRangeException(nameof (growFactor), Environment.GetResourceString("ArgumentOutOfRange_QueueGrowFactor", (object) 1, (object) 10));
      this._array = new object[capacity];
      this._head = 0;
      this._tail = 0;
      this._size = 0;
      this._growFactor = (int) ((double) growFactor * 100.0);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Queue" /> класс, который содержит элементы, скопированные из указанной коллекции, имеет начальной емкостью, равной количеству скопированных элементов и заданным по умолчанию коэффициентом роста.
    /// </summary>
    /// <param name="col">
    ///   <see cref="T:System.Collections.ICollection" /> Копировать элементы.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="col" /> имеет значение <see langword="null" />.
    /// </exception>
    public Queue(ICollection col)
      : this(col == null ? 32 : col.Count)
    {
      if (col == null)
        throw new ArgumentNullException(nameof (col));
      foreach (object obj in (IEnumerable) col)
        this.Enqueue(obj);
    }

    /// <summary>
    ///   Получает число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Queue" />.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Queue" />.
    /// </returns>
    public virtual int Count
    {
      get
      {
        return this._size;
      }
    }

    /// <summary>
    ///   Создает неполную копию объекта <see cref="T:System.Collections.Queue" />.
    /// </summary>
    /// <returns>
    ///   Неполная копия <see cref="T:System.Collections.Queue" />.
    /// </returns>
    public virtual object Clone()
    {
      Queue queue = new Queue(this._size);
      queue._size = this._size;
      int size = this._size;
      int length1 = this._array.Length - this._head < size ? this._array.Length - this._head : size;
      Array.Copy((Array) this._array, this._head, (Array) queue._array, 0, length1);
      int length2 = size - length1;
      if (length2 > 0)
        Array.Copy((Array) this._array, 0, (Array) queue._array, this._array.Length - this._head, length2);
      queue._version = this._version;
      return (object) queue;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, является ли доступ к коллекции <see cref="T:System.Collections.Queue" /> синхронизированным (потокобезопасным).
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если доступ к классу <see cref="T:System.Collections.Queue" /> является синхронизированным (потокобезопасным); в противном случае — <see langword="false" />.
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
    ///   Получает объект, с помощью которого можно синхронизировать доступ к коллекции <see cref="T:System.Collections.Queue" />.
    /// </summary>
    /// <returns>
    ///   Объект, который может использоваться для синхронизации доступа к <see cref="T:System.Collections.Queue" />.
    /// </returns>
    public virtual object SyncRoot
    {
      get
      {
        if (this._syncRoot == null)
          Interlocked.CompareExchange(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    /// <summary>
    ///   Удаляет все объекты из <see cref="T:System.Collections.Queue" />.
    /// </summary>
    public virtual void Clear()
    {
      if (this._head < this._tail)
      {
        Array.Clear((Array) this._array, this._head, this._size);
      }
      else
      {
        Array.Clear((Array) this._array, this._head, this._array.Length - this._head);
        Array.Clear((Array) this._array, 0, this._tail);
      }
      this._head = 0;
      this._tail = 0;
      this._size = 0;
      ++this._version;
    }

    /// <summary>
    ///   Копирует элементы коллекции <see cref="T:System.Collections.Queue" /> в существующий одномерный массив <see cref="T:System.Array" />, начиная с указанного значения индекса массива.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.Queue" />.
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
    ///   Массив <paramref name="array" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Количество элементов в исходной коллекции <see cref="T:System.Collections.Queue" /> больше, чем свободное пространство от <paramref name="index" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.ArrayTypeMismatchException">
    ///   Тип источника <see cref="T:System.Collections.Queue" /> не может быть автоматически приведен к типу массива назначения <paramref name="array" />.
    /// </exception>
    public virtual void CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (array.Length - index < this._size)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      int size = this._size;
      if (size == 0)
        return;
      int length1 = this._array.Length - this._head < size ? this._array.Length - this._head : size;
      Array.Copy((Array) this._array, this._head, array, index, length1);
      int length2 = size - length1;
      if (length2 <= 0)
        return;
      Array.Copy((Array) this._array, 0, array, index + this._array.Length - this._head, length2);
    }

    /// <summary>
    ///   Добавляет объект в конец очереди <see cref="T:System.Collections.Queue" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект, добавляемый в коллекцию <see cref="T:System.Collections.Queue" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    public virtual void Enqueue(object obj)
    {
      if (this._size == this._array.Length)
      {
        int capacity = (int) ((long) this._array.Length * (long) this._growFactor / 100L);
        if (capacity < this._array.Length + 4)
          capacity = this._array.Length + 4;
        this.SetCapacity(capacity);
      }
      this._array[this._tail] = obj;
      this._tail = (this._tail + 1) % this._array.Length;
      ++this._size;
      ++this._version;
    }

    /// <summary>
    ///   Возвращает перечислитель, осуществляющий перебор элементов списка <see cref="T:System.Collections.Queue" />.
    /// </summary>
    /// <returns>
    ///   Интерфейс <see cref="T:System.Collections.IEnumerator" /> для <see cref="T:System.Collections.Queue" />.
    /// </returns>
    public virtual IEnumerator GetEnumerator()
    {
      return (IEnumerator) new Queue.QueueEnumerator(this);
    }

    /// <summary>
    ///   Удаляет объект из начала очереди <see cref="T:System.Collections.Queue" /> и возвращает его.
    /// </summary>
    /// <returns>
    ///   Объект, удаляемый из начала очереди <see cref="T:System.Collections.Queue" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Очередь <see cref="T:System.Collections.Queue" /> является пустой.
    /// </exception>
    public virtual object Dequeue()
    {
      if (this.Count == 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyQueue"));
      object obj = this._array[this._head];
      this._array[this._head] = (object) null;
      this._head = (this._head + 1) % this._array.Length;
      --this._size;
      ++this._version;
      return obj;
    }

    /// <summary>
    ///   Возвращает объект, находящийся в начале очереди <see cref="T:System.Collections.Queue" />, но не удаляет его.
    /// </summary>
    /// <returns>
    ///   Объект, находящийся в начале очереди <see cref="T:System.Collections.Queue" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Очередь <see cref="T:System.Collections.Queue" /> является пустой.
    /// </exception>
    public virtual object Peek()
    {
      if (this.Count == 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyQueue"));
      return this._array[this._head];
    }

    /// <summary>
    ///   Возвращает новый <see cref="T:System.Collections.Queue" /> включает исходную очередь и является потокобезопасным.
    /// </summary>
    /// <param name="queue">
    ///   Коллекция <see cref="T:System.Collections.Queue" />, которую требуется синхронизировать.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.Collections.Queue" /> синхронизированная оболочка (потокобезопасным).
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="queue" /> имеет значение <see langword="null" />.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static Queue Synchronized(Queue queue)
    {
      if (queue == null)
        throw new ArgumentNullException(nameof (queue));
      return (Queue) new Queue.SynchronizedQueue(queue);
    }

    /// <summary>
    ///   Определяет, входит ли элемент в коллекцию <see cref="T:System.Collections.Queue" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект <see cref="T:System.Object" />, который требуется найти в коллекции <see cref="T:System.Collections.Queue" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> найден в коллекции <see cref="T:System.Collections.Queue" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool Contains(object obj)
    {
      int index = this._head;
      int size = this._size;
      while (size-- > 0)
      {
        if (obj == null)
        {
          if (this._array[index] == null)
            return true;
        }
        else if (this._array[index] != null && this._array[index].Equals(obj))
          return true;
        index = (index + 1) % this._array.Length;
      }
      return false;
    }

    internal object GetElement(int i)
    {
      return this._array[(this._head + i) % this._array.Length];
    }

    /// <summary>
    ///   Копирует элементы <see cref="T:System.Collections.Queue" /> в новый массив.
    /// </summary>
    /// <returns>
    ///   Новый массив, содержащий элементы, скопированные из <see cref="T:System.Collections.Queue" />.
    /// </returns>
    public virtual object[] ToArray()
    {
      object[] objArray = new object[this._size];
      if (this._size == 0)
        return objArray;
      if (this._head < this._tail)
      {
        Array.Copy((Array) this._array, this._head, (Array) objArray, 0, this._size);
      }
      else
      {
        Array.Copy((Array) this._array, this._head, (Array) objArray, 0, this._array.Length - this._head);
        Array.Copy((Array) this._array, 0, (Array) objArray, this._array.Length - this._head, this._tail);
      }
      return objArray;
    }

    private void SetCapacity(int capacity)
    {
      object[] objArray = new object[capacity];
      if (this._size > 0)
      {
        if (this._head < this._tail)
        {
          Array.Copy((Array) this._array, this._head, (Array) objArray, 0, this._size);
        }
        else
        {
          Array.Copy((Array) this._array, this._head, (Array) objArray, 0, this._array.Length - this._head);
          Array.Copy((Array) this._array, 0, (Array) objArray, this._array.Length - this._head, this._tail);
        }
      }
      this._array = objArray;
      this._head = 0;
      this._tail = this._size == capacity ? 0 : this._size;
      ++this._version;
    }

    /// <summary>
    ///   Задает значение емкости, равное действительному количеству элементов в <see cref="T:System.Collections.Queue" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Collections.Queue" /> доступен только для чтения.
    /// </exception>
    public virtual void TrimToSize()
    {
      this.SetCapacity(this._size);
    }

    [Serializable]
    private class SynchronizedQueue : Queue
    {
      private Queue _q;
      private object root;

      internal SynchronizedQueue(Queue q)
      {
        this._q = q;
        this.root = this._q.SyncRoot;
      }

      public override bool IsSynchronized
      {
        get
        {
          return true;
        }
      }

      public override object SyncRoot
      {
        get
        {
          return this.root;
        }
      }

      public override int Count
      {
        get
        {
          lock (this.root)
            return this._q.Count;
        }
      }

      public override void Clear()
      {
        lock (this.root)
          this._q.Clear();
      }

      public override object Clone()
      {
        lock (this.root)
          return (object) new Queue.SynchronizedQueue((Queue) this._q.Clone());
      }

      public override bool Contains(object obj)
      {
        lock (this.root)
          return this._q.Contains(obj);
      }

      public override void CopyTo(Array array, int arrayIndex)
      {
        lock (this.root)
          this._q.CopyTo(array, arrayIndex);
      }

      public override void Enqueue(object value)
      {
        lock (this.root)
          this._q.Enqueue(value);
      }

      public override object Dequeue()
      {
        lock (this.root)
          return this._q.Dequeue();
      }

      public override IEnumerator GetEnumerator()
      {
        lock (this.root)
          return this._q.GetEnumerator();
      }

      public override object Peek()
      {
        lock (this.root)
          return this._q.Peek();
      }

      public override object[] ToArray()
      {
        lock (this.root)
          return this._q.ToArray();
      }

      public override void TrimToSize()
      {
        lock (this.root)
          this._q.TrimToSize();
      }
    }

    [Serializable]
    private class QueueEnumerator : IEnumerator, ICloneable
    {
      private Queue _q;
      private int _index;
      private int _version;
      private object currentElement;

      internal QueueEnumerator(Queue q)
      {
        this._q = q;
        this._version = this._q._version;
        this._index = 0;
        this.currentElement = (object) this._q._array;
        if (this._q._size != 0)
          return;
        this._index = -1;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public virtual bool MoveNext()
      {
        if (this._version != this._q._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this._index < 0)
        {
          this.currentElement = (object) this._q._array;
          return false;
        }
        this.currentElement = this._q.GetElement(this._index);
        ++this._index;
        if (this._index == this._q._size)
          this._index = -1;
        return true;
      }

      public virtual object Current
      {
        get
        {
          if (this.currentElement != this._q._array)
            return this.currentElement;
          if (this._index == 0)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
        }
      }

      public virtual void Reset()
      {
        if (this._version != this._q._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this._index = this._q._size != 0 ? 0 : -1;
        this.currentElement = (object) this._q._array;
      }
    }

    internal class QueueDebugView
    {
      private Queue queue;

      public QueueDebugView(Queue queue)
      {
        if (queue == null)
          throw new ArgumentNullException(nameof (queue));
        this.queue = queue;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public object[] Items
      {
        get
        {
          return this.queue.ToArray();
        }
      }
    }
  }
}
