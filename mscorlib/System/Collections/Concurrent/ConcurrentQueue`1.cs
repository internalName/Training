// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.ConcurrentQueue`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
  /// <summary>
  ///   Предоставляет потокобезопасную коллекцию, обслуживаемую по принципу "первым поступил — первым обслужен" (FIFO).
  /// </summary>
  /// <typeparam name="T">Тип элементов в очереди.</typeparam>
  [ComVisible(false)]
  [DebuggerDisplay("Count = {Count}")]
  [DebuggerTypeProxy(typeof (SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<>))]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ConcurrentQueue<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
  {
    [NonSerialized]
    private volatile ConcurrentQueue<T>.Segment m_head;
    [NonSerialized]
    private volatile ConcurrentQueue<T>.Segment m_tail;
    private T[] m_serializationArray;
    private const int SEGMENT_SIZE = 32;
    [NonSerialized]
    internal volatile int m_numSnapshotTakers;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ConcurrentQueue()
    {
      this.m_head = this.m_tail = new ConcurrentQueue<T>.Segment(0L, this);
    }

    private void InitializeFromCollection(IEnumerable<T> collection)
    {
      ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(0L, this);
      this.m_head = segment;
      int num = 0;
      foreach (T obj in collection)
      {
        segment.UnsafeAdd(obj);
        ++num;
        if (num >= 32)
        {
          segment = segment.UnsafeGrow();
          num = 0;
        }
      }
      this.m_tail = segment;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />, который содержит элементы, скопированные из указанной коллекции.
    /// </summary>
    /// <param name="collection">
    ///   Коллекция, элементы которой копируются в новую коллекцию <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="collection" /> Аргумент имеет значение null.
    /// </exception>
    [__DynamicallyInvokable]
    public ConcurrentQueue(IEnumerable<T> collection)
    {
      if (collection == null)
        throw new ArgumentNullException(nameof (collection));
      this.InitializeFromCollection(collection);
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext context)
    {
      this.m_serializationArray = this.ToArray();
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
      this.InitializeFromCollection((IEnumerable<T>) this.m_serializationArray);
      this.m_serializationArray = (T[]) null;
    }

    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      ((ICollection) this.ToList()).CopyTo(array, index);
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

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    [__DynamicallyInvokable]
    bool IProducerConsumerCollection<T>.TryAdd(T item)
    {
      this.Enqueue(item);
      return true;
    }

    [__DynamicallyInvokable]
    bool IProducerConsumerCollection<T>.TryTake(out T item)
    {
      return this.TryDequeue(out item);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли коллекция <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> пустой.
    /// </summary>
    /// <returns>
    ///   Значение true, если коллекция <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> является пустой; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsEmpty
    {
      [__DynamicallyInvokable] get
      {
        ConcurrentQueue<T>.Segment head = this.m_head;
        if (!head.IsEmpty)
          return false;
        if (head.Next == null)
          return true;
        SpinWait spinWait = new SpinWait();
        for (; head.IsEmpty; head = this.m_head)
        {
          if (head.Next == null)
            return true;
          spinWait.SpinOnce();
        }
        return false;
      }
    }

    /// <summary>
    ///   Копирует элементы, хранящиеся в <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />, в новый массив.
    /// </summary>
    /// <returns>
    ///   Новый массив, содержащий снимок элементов, скопированных из коллекции <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public T[] ToArray()
    {
      return this.ToList().ToArray();
    }

    private List<T> ToList()
    {
      Interlocked.Increment(ref this.m_numSnapshotTakers);
      List<T> list = new List<T>();
      try
      {
        ConcurrentQueue<T>.Segment head;
        ConcurrentQueue<T>.Segment tail;
        int headLow;
        int tailHigh;
        this.GetHeadTailPositions(out head, out tail, out headLow, out tailHigh);
        if (head == tail)
        {
          head.AddToList(list, headLow, tailHigh);
        }
        else
        {
          head.AddToList(list, headLow, 31);
          for (ConcurrentQueue<T>.Segment next = head.Next; next != tail; next = next.Next)
            next.AddToList(list, 0, 31);
          tail.AddToList(list, 0, tailHigh);
        }
      }
      finally
      {
        Interlocked.Decrement(ref this.m_numSnapshotTakers);
      }
      return list;
    }

    private void GetHeadTailPositions(out ConcurrentQueue<T>.Segment head, out ConcurrentQueue<T>.Segment tail, out int headLow, out int tailHigh)
    {
      head = this.m_head;
      tail = this.m_tail;
      headLow = head.Low;
      tailHigh = tail.High;
      SpinWait spinWait = new SpinWait();
      while (head != this.m_head || tail != this.m_tail || (headLow != head.Low || tailHigh != tail.High) || head.m_index > tail.m_index)
      {
        spinWait.SpinOnce();
        head = this.m_head;
        tail = this.m_tail;
        headLow = head.Low;
        tailHigh = tail.High;
      }
    }

    /// <summary>
    ///   Получает число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        ConcurrentQueue<T>.Segment head;
        ConcurrentQueue<T>.Segment tail;
        int headLow;
        int tailHigh;
        this.GetHeadTailPositions(out head, out tail, out headLow, out tailHigh);
        if (head == tail)
          return tailHigh - headLow + 1;
        return 32 - headLow + 32 * (int) (tail.m_index - head.m_index - 1L) + (tailHigh + 1);
      }
    }

    /// <summary>
    ///   Копирует элементы коллекции <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> в существующий одномерный массив <see cref="T:System.Array" />, начиная с указанного значения индекса массива.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы коллекции <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.
    ///    Массив <see cref="T:System.Array" /> должен иметь индексацию, начинающуюся с нуля.
    /// </param>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс в массиве <paramref name="array" />, указывающий начало копирования.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="array" /> является ссылкой на null (Nothing в Visual Basic).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="index" /> равно или больше, чем длина <paramref name="array" /> - или - количество элементов в исходной коллекции <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> больше, чем свободное пространство от <paramref name="index" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      this.ToList().CopyTo(array, index);
    }

    /// <summary>
    ///   Возвращает перечислитель, осуществляющий перебор элементов списка <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.
    /// </summary>
    /// <returns>
    ///   Перечислитель для содержимого <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public IEnumerator<T> GetEnumerator()
    {
      Interlocked.Increment(ref this.m_numSnapshotTakers);
      ConcurrentQueue<T>.Segment head;
      ConcurrentQueue<T>.Segment tail;
      int headLow;
      int tailHigh;
      this.GetHeadTailPositions(out head, out tail, out headLow, out tailHigh);
      return this.GetEnumerator(head, tail, headLow, tailHigh);
    }

    private IEnumerator<T> GetEnumerator(ConcurrentQueue<T>.Segment head, ConcurrentQueue<T>.Segment tail, int headLow, int tailHigh)
    {
      try
      {
        SpinWait spin = new SpinWait();
        if (head == tail)
        {
          for (int i = headLow; i <= tailHigh; ++i)
          {
            spin.Reset();
            while (!head.m_state[i].m_value)
              spin.SpinOnce();
            yield return head.m_array[i];
          }
        }
        else
        {
          for (int i = headLow; i < 32; ++i)
          {
            spin.Reset();
            while (!head.m_state[i].m_value)
              spin.SpinOnce();
            yield return head.m_array[i];
          }
          ConcurrentQueue<T>.Segment curr;
          for (curr = head.Next; curr != tail; curr = curr.Next)
          {
            for (int i = 0; i < 32; ++i)
            {
              spin.Reset();
              while (!curr.m_state[i].m_value)
                spin.SpinOnce();
              yield return curr.m_array[i];
            }
          }
          for (int i = 0; i <= tailHigh; ++i)
          {
            spin.Reset();
            while (!tail.m_state[i].m_value)
              spin.SpinOnce();
            yield return tail.m_array[i];
          }
          curr = (ConcurrentQueue<T>.Segment) null;
        }
      }
      finally
      {
        Interlocked.Decrement(ref this.m_numSnapshotTakers);
      }
    }

    /// <summary>
    ///   Добавляет объект в конец очереди <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект, добавляемый в конец <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />.
    ///    Значение ссылочных типов может быть пустой ссылкой (Nothing в Visual Basic).
    /// </param>
    [__DynamicallyInvokable]
    public void Enqueue(T item)
    {
      SpinWait spinWait = new SpinWait();
      while (!this.m_tail.TryAppend(item))
        spinWait.SpinOnce();
    }

    /// <summary>
    ///   Пытается удалить и вернуть объект, находящийся в начале параллельной очереди.
    /// </summary>
    /// <param name="result">
    ///   В случае успешного выполнения операции параметр <paramref name="result" />, возвращаемый данным методом, содержит удаленный объект.
    ///    Если объект, доступный для удаления, не найден, значение не определено.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если элемент был успешно удален и возвращен из начала <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool TryDequeue(out T result)
    {
      while (!this.IsEmpty)
      {
        if (this.m_head.TryRemove(out result))
          return true;
      }
      result = default (T);
      return false;
    }

    /// <summary>
    ///   Пытается вернуть объект из начала <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" /> без его удаления.
    /// </summary>
    /// <param name="result">
    ///   Параметр <paramref name="result" />, возвращаемый данным методом, содержит объект, расположенный в начале <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1" />, или неопределенное значение, если операцию не удалось выполнить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если объект был успешно возвращен; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool TryPeek(out T result)
    {
      Interlocked.Increment(ref this.m_numSnapshotTakers);
      while (!this.IsEmpty)
      {
        if (this.m_head.TryPeek(out result))
        {
          Interlocked.Decrement(ref this.m_numSnapshotTakers);
          return true;
        }
      }
      result = default (T);
      Interlocked.Decrement(ref this.m_numSnapshotTakers);
      return false;
    }

    private class Segment
    {
      internal volatile T[] m_array;
      internal volatile VolatileBool[] m_state;
      private volatile ConcurrentQueue<T>.Segment m_next;
      internal readonly long m_index;
      private volatile int m_low;
      private volatile int m_high;
      private volatile ConcurrentQueue<T> m_source;

      internal Segment(long index, ConcurrentQueue<T> source)
      {
        this.m_array = new T[32];
        this.m_state = new VolatileBool[32];
        this.m_high = -1;
        this.m_index = index;
        this.m_source = source;
      }

      internal ConcurrentQueue<T>.Segment Next
      {
        get
        {
          return this.m_next;
        }
      }

      internal bool IsEmpty
      {
        get
        {
          return this.Low > this.High;
        }
      }

      internal void UnsafeAdd(T value)
      {
        ++this.m_high;
        this.m_array[this.m_high] = value;
        this.m_state[this.m_high].m_value = true;
      }

      internal ConcurrentQueue<T>.Segment UnsafeGrow()
      {
        ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(this.m_index + 1L, this.m_source);
        this.m_next = segment;
        return segment;
      }

      internal void Grow()
      {
        this.m_next = new ConcurrentQueue<T>.Segment(this.m_index + 1L, this.m_source);
        this.m_source.m_tail = this.m_next;
      }

      internal bool TryAppend(T value)
      {
        if (this.m_high >= 31)
          return false;
        int index = 32;
        try
        {
        }
        finally
        {
          index = Interlocked.Increment(ref this.m_high);
          if (index <= 31)
          {
            this.m_array[index] = value;
            this.m_state[index].m_value = true;
          }
          if (index == 31)
            this.Grow();
        }
        return index <= 31;
      }

      internal bool TryRemove(out T result)
      {
        SpinWait spinWait1 = new SpinWait();
        int low = this.Low;
        for (int high = this.High; low <= high; high = this.High)
        {
          if (Interlocked.CompareExchange(ref this.m_low, low + 1, low) == low)
          {
            SpinWait spinWait2 = new SpinWait();
            while (!this.m_state[low].m_value)
              spinWait2.SpinOnce();
            result = this.m_array[low];
            if (this.m_source.m_numSnapshotTakers <= 0)
              this.m_array[low] = default (T);
            if (low + 1 >= 32)
            {
              SpinWait spinWait3 = new SpinWait();
              while (this.m_next == null)
                spinWait3.SpinOnce();
              this.m_source.m_head = this.m_next;
            }
            return true;
          }
          spinWait1.SpinOnce();
          low = this.Low;
        }
        result = default (T);
        return false;
      }

      internal bool TryPeek(out T result)
      {
        result = default (T);
        int low = this.Low;
        if (low > this.High)
          return false;
        SpinWait spinWait = new SpinWait();
        while (!this.m_state[low].m_value)
          spinWait.SpinOnce();
        result = this.m_array[low];
        return true;
      }

      internal void AddToList(List<T> list, int start, int end)
      {
        for (int index = start; index <= end; ++index)
        {
          SpinWait spinWait = new SpinWait();
          while (!this.m_state[index].m_value)
            spinWait.SpinOnce();
          list.Add(this.m_array[index]);
        }
      }

      internal int Low
      {
        get
        {
          return Math.Min(this.m_low, 32);
        }
      }

      internal int High
      {
        get
        {
          return Math.Min(this.m_high, 31);
        }
      }
    }
  }
}
