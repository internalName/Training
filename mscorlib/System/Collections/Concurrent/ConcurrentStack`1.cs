// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.ConcurrentStack`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
  /// <summary>
  ///   Предоставляет потокобезопасную коллекцию, обслуживаемую по принципу "последним поступил — первым обслужен" (LIFO).
  /// </summary>
  /// <typeparam name="T">Тип элементов в стеке.</typeparam>
  [DebuggerDisplay("Count = {Count}")]
  [DebuggerTypeProxy(typeof (SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<>))]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ConcurrentStack<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
  {
    [NonSerialized]
    private volatile ConcurrentStack<T>.Node m_head;
    private T[] m_serializationArray;
    private const int BACKOFF_MAX_YIELDS = 8;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ConcurrentStack()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />, который содержит элементы, скопированные из указанной коллекции.
    /// </summary>
    /// <param name="collection">
    ///   Коллекция, элементы которой копируются в новую коллекцию <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Аргумент <paramref name="collection" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    public ConcurrentStack(IEnumerable<T> collection)
    {
      if (collection == null)
        throw new ArgumentNullException(nameof (collection));
      this.InitializeFromCollection(collection);
    }

    private void InitializeFromCollection(IEnumerable<T> collection)
    {
      ConcurrentStack<T>.Node node = (ConcurrentStack<T>.Node) null;
      foreach (T obj in collection)
        node = new ConcurrentStack<T>.Node(obj)
        {
          m_next = node
        };
      this.m_head = node;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext context)
    {
      this.m_serializationArray = this.ToArray();
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
      ConcurrentStack<T>.Node node1 = (ConcurrentStack<T>.Node) null;
      ConcurrentStack<T>.Node node2 = (ConcurrentStack<T>.Node) null;
      for (int index = 0; index < this.m_serializationArray.Length; ++index)
      {
        ConcurrentStack<T>.Node node3 = new ConcurrentStack<T>.Node(this.m_serializationArray[index]);
        if (node1 == null)
          node2 = node3;
        else
          node1.m_next = node3;
        node1 = node3;
      }
      this.m_head = node2;
      this.m_serializationArray = (T[]) null;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли коллекция <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> пустой.
    /// </summary>
    /// <returns>
    ///   Значение true, если коллекция <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> является пустой; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsEmpty
    {
      [__DynamicallyInvokable] get
      {
        return this.m_head == null;
      }
    }

    /// <summary>
    ///   Получает число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в коллекции <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        int num = 0;
        for (ConcurrentStack<T>.Node node = this.m_head; node != null; node = node.m_next)
          ++num;
        return num;
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

    /// <summary>
    ///   Удаляет все объекты из <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Clear()
    {
      this.m_head = (ConcurrentStack<T>.Node) null;
    }

    [__DynamicallyInvokable]
    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      ((ICollection) this.ToList()).CopyTo(array, index);
    }

    /// <summary>
    ///   Копирует элементы коллекции <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> в существующий одномерный массив <see cref="T:System.Array" />, начиная с указанного значения индекса массива.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы коллекции <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
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
    ///   <paramref name="index" /> равно или больше, чем длина <paramref name="array" /> - или - количество элементов в исходной коллекции <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> больше, чем свободное пространство от <paramref name="index" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      this.ToList().CopyTo(array, index);
    }

    /// <summary>
    ///   Вставляет объект как верхний элемент стека <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект, вставляемый в <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    ///    Значение ссылочных типов может быть пустой ссылкой (Nothing в Visual Basic).
    /// </param>
    [__DynamicallyInvokable]
    public void Push(T item)
    {
      ConcurrentStack<T>.Node node = new ConcurrentStack<T>.Node(item);
      node.m_next = this.m_head;
      if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, node, node.m_next) == node.m_next)
        return;
      this.PushCore(node, node);
    }

    /// <summary>
    ///   Вставляет неделимым блоком несколько объектов в качестве верхнего элемента <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </summary>
    /// <param name="items">
    ///   Объекты, вставляемые в <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="items" /> является ссылкой на null (Nothing в Visual Basic).
    /// </exception>
    [__DynamicallyInvokable]
    public void PushRange(T[] items)
    {
      if (items == null)
        throw new ArgumentNullException(nameof (items));
      this.PushRange(items, 0, items.Length);
    }

    /// <summary>
    ///   Вставляет неделимым блоком несколько объектов в качестве верхнего элемента <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </summary>
    /// <param name="items">
    ///   Объекты, вставляемые в <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </param>
    /// <param name="startIndex">
    ///   Отсчитываемое от нуля смещение в массиве <paramref name="items" />, с которого начинается вставка элементов в начало <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </param>
    /// <param name="count">
    ///   Число элементов, вставляемых в начало <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="items" /> является ссылкой на null (Nothing в Visual Basic).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> или <paramref name="count" /> является отрицательным.
    ///    Или <paramref name="startIndex" /> больше или равно длине <paramref name="items" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение <paramref name="startIndex" /> + <paramref name="count" /> превышает длину <paramref name="items" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void PushRange(T[] items, int startIndex, int count)
    {
      this.ValidatePushPopRangeInput(items, startIndex, count);
      if (count == 0)
        return;
      ConcurrentStack<T>.Node tail;
      ConcurrentStack<T>.Node head = tail = new ConcurrentStack<T>.Node(items[startIndex]);
      for (int index = startIndex + 1; index < startIndex + count; ++index)
        head = new ConcurrentStack<T>.Node(items[index])
        {
          m_next = head
        };
      tail.m_next = this.m_head;
      if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, head, tail.m_next) == tail.m_next)
        return;
      this.PushCore(head, tail);
    }

    private void PushCore(ConcurrentStack<T>.Node head, ConcurrentStack<T>.Node tail)
    {
      SpinWait spinWait = new SpinWait();
      do
      {
        spinWait.SpinOnce();
        tail.m_next = this.m_head;
      }
      while (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, head, tail.m_next) != tail.m_next);
      if (!CDSCollectionETWBCLProvider.Log.IsEnabled())
        return;
      CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPushFailed(spinWait.Count);
    }

    private void ValidatePushPopRangeInput(T[] items, int startIndex, int count)
    {
      if (items == null)
        throw new ArgumentNullException(nameof (items));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ConcurrentStack_PushPopRange_CountOutOfRange"));
      int length = items.Length;
      if (startIndex >= length || startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ConcurrentStack_PushPopRange_StartOutOfRange"));
      if (length - count < startIndex)
        throw new ArgumentException(Environment.GetResourceString("ConcurrentStack_PushPopRange_InvalidCount"));
    }

    [__DynamicallyInvokable]
    bool IProducerConsumerCollection<T>.TryAdd(T item)
    {
      this.Push(item);
      return true;
    }

    /// <summary>
    ///   Пытается вернуть объект из начала <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> без его удаления.
    /// </summary>
    /// <param name="result">
    ///   Параметр <paramref name="result" />, возвращаемый данным методом, содержит объект, расположенный в начале <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />, или неопределенное значение, если операцию не удалось выполнить.
    /// </param>
    /// <returns>
    ///   Значение true, если объект был успешно возвращен; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool TryPeek(out T result)
    {
      ConcurrentStack<T>.Node head = this.m_head;
      if (head == null)
      {
        result = default (T);
        return false;
      }
      result = head.m_value;
      return true;
    }

    /// <summary>
    ///   Пытается извлечь и вернуть верхний объект <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </summary>
    /// <param name="result">
    ///   В случае успешного выполнения операции параметр <paramref name="result" />, возвращаемый данным методом, содержит удаленный объект.
    ///    Если объект, доступный для удаления, не найден, значение не определено.
    /// </param>
    /// <returns>
    ///   Значение true, если элемент был успешно удален и возвращен из начала <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool TryPop(out T result)
    {
      ConcurrentStack<T>.Node head = this.m_head;
      if (head == null)
      {
        result = default (T);
        return false;
      }
      if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, head.m_next, head) != head)
        return this.TryPopCore(out result);
      result = head.m_value;
      return true;
    }

    /// <summary>
    ///   Пытается извлечь и вернуть несколько объектов из начала <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> в виде неделимого блока.
    /// </summary>
    /// <param name="items">
    ///   Массив <see cref="T:System.Array" />, в который будут добавлены объекты, извлеченные из начала <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </param>
    /// <returns>
    ///   Число объектов, успешно извлеченных из начала <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> и вставленных в массив <paramref name="items" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="items" /> является аргументом null (Nothing в Visual Basic).
    /// </exception>
    [__DynamicallyInvokable]
    public int TryPopRange(T[] items)
    {
      if (items == null)
        throw new ArgumentNullException(nameof (items));
      return this.TryPopRange(items, 0, items.Length);
    }

    /// <summary>
    ///   Пытается извлечь и вернуть несколько объектов из начала <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> в виде неделимого блока.
    /// </summary>
    /// <param name="items">
    ///   Массив <see cref="T:System.Array" />, в который будут добавлены объекты, извлеченные из начала <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </param>
    /// <param name="startIndex">
    ///   Отсчитываемое от нуля смещение в массиве <paramref name="items" />, с которого начинается вставка элементов из начала <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </param>
    /// <param name="count">
    ///   Число элементов, извлекаемых из начала <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> и вставляемых в массив <paramref name="items" />.
    /// </param>
    /// <returns>
    ///   Число объектов, успешно извлеченных из верхней части стека и вставленных в <paramref name="items" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="items" /> является ссылкой на null (Nothing в Visual Basic).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> или <paramref name="count" /> является отрицательным.
    ///    Или <paramref name="startIndex" /> больше или равно длине <paramref name="items" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="startIndex" /> + <paramref name="count" /> больше, чем длина <paramref name="items" />.
    /// </exception>
    [__DynamicallyInvokable]
    public int TryPopRange(T[] items, int startIndex, int count)
    {
      this.ValidatePushPopRangeInput(items, startIndex, count);
      if (count == 0)
        return 0;
      ConcurrentStack<T>.Node poppedHead;
      int nodesCount = this.TryPopCore(count, out poppedHead);
      if (nodesCount > 0)
        this.CopyRemovedItems(poppedHead, items, startIndex, nodesCount);
      return nodesCount;
    }

    private bool TryPopCore(out T result)
    {
      ConcurrentStack<T>.Node poppedHead;
      if (this.TryPopCore(1, out poppedHead) == 1)
      {
        result = poppedHead.m_value;
        return true;
      }
      result = default (T);
      return false;
    }

    private int TryPopCore(int count, out ConcurrentStack<T>.Node poppedHead)
    {
      SpinWait spinWait = new SpinWait();
      int num1 = 1;
      Random random = new Random(Environment.TickCount & int.MaxValue);
      ConcurrentStack<T>.Node head;
      int num2;
      while (true)
      {
        head = this.m_head;
        if (head != null)
        {
          ConcurrentStack<T>.Node node = head;
          for (num2 = 1; num2 < count && node.m_next != null; ++num2)
            node = node.m_next;
          if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, node.m_next, head) != head)
          {
            for (int index = 0; index < num1; ++index)
              spinWait.SpinOnce();
            num1 = spinWait.NextSpinWillYield ? random.Next(1, 8) : num1 * 2;
          }
          else
            goto label_9;
        }
        else
          break;
      }
      if (count == 1 && CDSCollectionETWBCLProvider.Log.IsEnabled())
        CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPopFailed(spinWait.Count);
      poppedHead = (ConcurrentStack<T>.Node) null;
      return 0;
label_9:
      if (count == 1 && CDSCollectionETWBCLProvider.Log.IsEnabled())
        CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPopFailed(spinWait.Count);
      poppedHead = head;
      return num2;
    }

    private void CopyRemovedItems(ConcurrentStack<T>.Node head, T[] collection, int startIndex, int nodesCount)
    {
      ConcurrentStack<T>.Node node = head;
      for (int index = startIndex; index < startIndex + nodesCount; ++index)
      {
        collection[index] = node.m_value;
        node = node.m_next;
      }
    }

    [__DynamicallyInvokable]
    bool IProducerConsumerCollection<T>.TryTake(out T item)
    {
      return this.TryPop(out item);
    }

    /// <summary>
    ///   Копирует элементы, хранящиеся в <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />, в новый массив.
    /// </summary>
    /// <returns>
    ///   Новый массив, содержащий снимок элементов, скопированных из коллекции <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public T[] ToArray()
    {
      return this.ToList().ToArray();
    }

    private List<T> ToList()
    {
      List<T> objList = new List<T>();
      for (ConcurrentStack<T>.Node node = this.m_head; node != null; node = node.m_next)
        objList.Add(node.m_value);
      return objList;
    }

    /// <summary>
    ///   Возвращает перечислитель, осуществляющий перебор элементов списка <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </summary>
    /// <returns>
    ///   Перечислитель для коллекции <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public IEnumerator<T> GetEnumerator()
    {
      return this.GetEnumerator(this.m_head);
    }

    private IEnumerator<T> GetEnumerator(ConcurrentStack<T>.Node head)
    {
      for (ConcurrentStack<T>.Node current = head; current != null; current = current.m_next)
        yield return current.m_value;
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    private class Node
    {
      internal readonly T m_value;
      internal ConcurrentStack<T>.Node m_next;

      internal Node(T value)
      {
        this.m_value = value;
        this.m_next = (ConcurrentStack<T>.Node) null;
      }
    }
  }
}
