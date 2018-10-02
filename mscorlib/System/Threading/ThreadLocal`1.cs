// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadLocal`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Предоставляет хранилище для данных, локальных для потока.
  /// </summary>
  /// <typeparam name="T">
  ///   Указывает тип данных, хранящихся на поток.
  /// </typeparam>
  [DebuggerTypeProxy(typeof (SystemThreading_ThreadLocalDebugView<>))]
  [DebuggerDisplay("IsValueCreated={IsValueCreated}, Value={ValueForDebugDisplay}, Count={ValuesCountForDebugDisplay}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ThreadLocal<T> : IDisposable
  {
    private static ThreadLocal<T>.IdManager s_idManager = new ThreadLocal<T>.IdManager();
    private ThreadLocal<T>.LinkedSlot m_linkedSlot = new ThreadLocal<T>.LinkedSlot((ThreadLocal<T>.LinkedSlotVolatile[]) null);
    private Func<T> m_valueFactory;
    [ThreadStatic]
    private static ThreadLocal<T>.LinkedSlotVolatile[] ts_slotArray;
    [ThreadStatic]
    private static ThreadLocal<T>.FinalizationHelper ts_finalizationHelper;
    private int m_idComplement;
    private volatile bool m_initialized;
    private bool m_trackAllValues;

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Threading.ThreadLocal`1" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ThreadLocal()
    {
      this.Initialize((Func<T>) null, false);
    }

    /// <summary>
    ///   Инициализирует <see cref="T:System.Threading.ThreadLocal`1" /> экземпляр и указывает, доступны ли все значения из любого потока.
    /// </summary>
    /// <param name="trackAllValues">
    ///   <see langword="true" /> Чтобы отслеживать все значения в экземпляре и предоставлять их через <see cref="P:System.Threading.ThreadLocal`1.Values" /> Свойства; <see langword="false" /> в противном случае.
    /// </param>
    [__DynamicallyInvokable]
    public ThreadLocal(bool trackAllValues)
    {
      this.Initialize((Func<T>) null, trackAllValues);
    }

    /// <summary>
    ///   Инициализирует <see cref="T:System.Threading.ThreadLocal`1" /> экземпляр с заданным <paramref name="valueFactory" /> функции.
    /// </summary>
    /// <param name="valueFactory">
    ///   <see cref="T:System.Func`1" /> Вызывается для получения значения лениво инициализирован, когда совершается попытка получить <see cref="P:System.Threading.ThreadLocal`1.Value" /> без его ранее был инициализирован.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="valueFactory" /> является ссылкой на null (Nothing в Visual Basic).
    /// </exception>
    [__DynamicallyInvokable]
    public ThreadLocal(Func<T> valueFactory)
    {
      if (valueFactory == null)
        throw new ArgumentNullException(nameof (valueFactory));
      this.Initialize(valueFactory, false);
    }

    /// <summary>
    ///   Инициализирует <see cref="T:System.Threading.ThreadLocal`1" /> экземпляр с заданным <paramref name="valueFactory" /> функции и флаг, указывающий, является ли все значения доступны из любого потока.
    /// </summary>
    /// <param name="valueFactory">
    ///   <see cref="T:System.Func`1" /> Вызывается для получения значения лениво инициализирован, когда совершается попытка получить <see cref="P:System.Threading.ThreadLocal`1.Value" /> без его ранее был инициализирован.
    /// </param>
    /// <param name="trackAllValues">
    ///   <see langword="true" /> Чтобы отслеживать все значения в экземпляре и предоставлять их через <see cref="P:System.Threading.ThreadLocal`1.Values" /> Свойства; <see langword="false" /> в противном случае.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="valueFactory" /> является пустой (<see langword="null" />) ссылкой (<see langword="Nothing" /> в Visual Basic).
    /// </exception>
    [__DynamicallyInvokable]
    public ThreadLocal(Func<T> valueFactory, bool trackAllValues)
    {
      if (valueFactory == null)
        throw new ArgumentNullException(nameof (valueFactory));
      this.Initialize(valueFactory, trackAllValues);
    }

    private void Initialize(Func<T> valueFactory, bool trackAllValues)
    {
      this.m_valueFactory = valueFactory;
      this.m_trackAllValues = trackAllValues;
      try
      {
      }
      finally
      {
        this.m_idComplement = ~ThreadLocal<T>.s_idManager.GetId();
        this.m_initialized = true;
      }
    }

    /// <summary>
    ///   Освобождает ресурсы, используемые <see cref="T:System.Threading.ThreadLocal`1" /> экземпляра.
    /// </summary>
    [__DynamicallyInvokable]
    ~ThreadLocal()
    {
      this.Dispose(false);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Threading.ThreadLocal`1" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает ресурсы, используемые <see cref="T:System.Threading.ThreadLocal`1" /> экземпляра.
    /// </summary>
    /// <param name="disposing">
    ///   Логическое значение, указывающее, вызывается ли данный метод из-за вызова задачи <see cref="M:System.Threading.ThreadLocal`1.Dispose" />.
    /// </param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      int id;
      lock (ThreadLocal<T>.s_idManager)
      {
        id = ~this.m_idComplement;
        this.m_idComplement = 0;
        if (id < 0 || !this.m_initialized)
          return;
        this.m_initialized = false;
        for (ThreadLocal<T>.LinkedSlot next = this.m_linkedSlot.Next; next != null; next = next.Next)
        {
          ThreadLocal<T>.LinkedSlotVolatile[] slotArray = next.SlotArray;
          if (slotArray != null)
          {
            next.SlotArray = (ThreadLocal<T>.LinkedSlotVolatile[]) null;
            slotArray[id].Value.Value = default (T);
            slotArray[id].Value = (ThreadLocal<T>.LinkedSlot) null;
          }
        }
      }
      this.m_linkedSlot = (ThreadLocal<T>.LinkedSlot) null;
      ThreadLocal<T>.s_idManager.ReturnId(id);
    }

    /// <summary>
    ///   Создает и возвращает строковое представление данного экземпляра для текущего потока.
    /// </summary>
    /// <returns>
    ///   Результат вызова метода <see cref="M:System.Object.ToString" /> на <see cref="P:System.Threading.ThreadLocal`1.Value" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.ThreadLocal`1" /> Экземпляр был удален.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   <see cref="P:System.Threading.ThreadLocal`1.Value" /> Для текущего потока является пустой ссылкой (Nothing в Visual Basic).
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Попытка обращения к функции инициализации <see cref="P:System.Threading.ThreadLocal`1.Value" /> рекурсивно.
    /// </exception>
    /// <exception cref="T:System.MissingMemberException">
    ///   Нет конструктора по умолчанию предоставляется и не значение фабрики.
    /// </exception>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.Value.ToString();
    }

    /// <summary>
    ///   Возвращает или задает значение данного экземпляра для текущего потока.
    /// </summary>
    /// <returns>
    ///   Возвращает экземпляр объекта, что данный ThreadLocal отвечает за инициализацию.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Экземпляр <see cref="T:System.Threading.ThreadLocal`1" /> удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Попытка обращения к функции инициализации <see cref="P:System.Threading.ThreadLocal`1.Value" /> рекурсивно.
    /// </exception>
    /// <exception cref="T:System.MissingMemberException">
    ///   Нет конструктора по умолчанию предоставляется и не значение фабрики.
    /// </exception>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [__DynamicallyInvokable]
    public T Value
    {
      [__DynamicallyInvokable] get
      {
        ThreadLocal<T>.LinkedSlotVolatile[] tsSlotArray = ThreadLocal<T>.ts_slotArray;
        int index = ~this.m_idComplement;
        ThreadLocal<T>.LinkedSlot linkedSlot;
        if (tsSlotArray != null && index >= 0 && (index < tsSlotArray.Length && (linkedSlot = tsSlotArray[index].Value) != null) && this.m_initialized)
          return linkedSlot.Value;
        return this.GetValueSlow();
      }
      [__DynamicallyInvokable] set
      {
        ThreadLocal<T>.LinkedSlotVolatile[] tsSlotArray = ThreadLocal<T>.ts_slotArray;
        int index = ~this.m_idComplement;
        ThreadLocal<T>.LinkedSlot linkedSlot;
        if (tsSlotArray != null && index >= 0 && (index < tsSlotArray.Length && (linkedSlot = tsSlotArray[index].Value) != null) && this.m_initialized)
          linkedSlot.Value = value;
        else
          this.SetValueSlow(value, tsSlotArray);
      }
    }

    private T GetValueSlow()
    {
      if (~this.m_idComplement < 0)
        throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
      Debugger.NotifyOfCrossThreadDependency();
      T obj;
      if (this.m_valueFactory == null)
      {
        obj = default (T);
      }
      else
      {
        obj = this.m_valueFactory();
        if (this.IsValueCreated)
          throw new InvalidOperationException(Environment.GetResourceString("ThreadLocal_Value_RecursiveCallsToValue"));
      }
      this.Value = obj;
      return obj;
    }

    private void SetValueSlow(T value, ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
    {
      int id = ~this.m_idComplement;
      if (id < 0)
        throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
      if (slotArray == null)
      {
        slotArray = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(id + 1)];
        ThreadLocal<T>.ts_finalizationHelper = new ThreadLocal<T>.FinalizationHelper(slotArray, this.m_trackAllValues);
        ThreadLocal<T>.ts_slotArray = slotArray;
      }
      if (id >= slotArray.Length)
      {
        this.GrowTable(ref slotArray, id + 1);
        ThreadLocal<T>.ts_finalizationHelper.SlotArray = slotArray;
        ThreadLocal<T>.ts_slotArray = slotArray;
      }
      if (slotArray[id].Value == null)
      {
        this.CreateLinkedSlot(slotArray, id, value);
      }
      else
      {
        ThreadLocal<T>.LinkedSlot linkedSlot = slotArray[id].Value;
        if (!this.m_initialized)
          throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
        linkedSlot.Value = value;
      }
    }

    private void CreateLinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, int id, T value)
    {
      ThreadLocal<T>.LinkedSlot linkedSlot = new ThreadLocal<T>.LinkedSlot(slotArray);
      lock (ThreadLocal<T>.s_idManager)
      {
        if (!this.m_initialized)
          throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
        ThreadLocal<T>.LinkedSlot next = this.m_linkedSlot.Next;
        linkedSlot.Next = next;
        linkedSlot.Previous = this.m_linkedSlot;
        linkedSlot.Value = value;
        if (next != null)
          next.Previous = linkedSlot;
        this.m_linkedSlot.Next = linkedSlot;
        slotArray[id].Value = linkedSlot;
      }
    }

    /// <summary>
    ///   Возвращает список всех значений, хранящихся в настоящее время для всех потоков, которые обращались к данного экземпляра.
    /// </summary>
    /// <returns>
    ///   Список всех значений, хранящихся в настоящее время для всех потоков, которые обращались к данного экземпляра.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Значения хранятся все потоки недоступны, поскольку этот экземпляр был инициализирован <paramref name="trackAllValues" /> аргументу присвоено <see langword="false" /> в вызове конструктора класса.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Экземпляр <see cref="T:System.Threading.ThreadLocal`1" /> удален.
    /// </exception>
    [__DynamicallyInvokable]
    public IList<T> Values
    {
      [__DynamicallyInvokable] get
      {
        if (!this.m_trackAllValues)
          throw new InvalidOperationException(Environment.GetResourceString("ThreadLocal_ValuesNotAvailable"));
        List<T> valuesAsList = this.GetValuesAsList();
        if (valuesAsList == null)
          throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
        return (IList<T>) valuesAsList;
      }
    }

    private List<T> GetValuesAsList()
    {
      List<T> objList = new List<T>();
      if (~this.m_idComplement == -1)
        return (List<T>) null;
      for (ThreadLocal<T>.LinkedSlot next = this.m_linkedSlot.Next; next != null; next = next.Next)
        objList.Add(next.Value);
      return objList;
    }

    private int ValuesCountForDebugDisplay
    {
      get
      {
        int num = 0;
        for (ThreadLocal<T>.LinkedSlot next = this.m_linkedSlot.Next; next != null; next = next.Next)
          ++num;
        return num;
      }
    }

    /// <summary>
    ///   Получает ли <see cref="P:System.Threading.ThreadLocal`1.Value" /> инициализирован в текущем потоке.
    /// </summary>
    /// <returns>
    ///   значение true, если <see cref="P:System.Threading.ThreadLocal`1.Value" /> инициализирован в текущем потоке; в противном случае — значение false.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.Threading.ThreadLocal`1" /> Экземпляр был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public bool IsValueCreated
    {
      [__DynamicallyInvokable] get
      {
        int index = ~this.m_idComplement;
        if (index < 0)
          throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
        ThreadLocal<T>.LinkedSlotVolatile[] tsSlotArray = ThreadLocal<T>.ts_slotArray;
        if (tsSlotArray != null && index < tsSlotArray.Length)
          return tsSlotArray[index].Value != null;
        return false;
      }
    }

    internal T ValueForDebugDisplay
    {
      get
      {
        ThreadLocal<T>.LinkedSlotVolatile[] tsSlotArray = ThreadLocal<T>.ts_slotArray;
        int index = ~this.m_idComplement;
        ThreadLocal<T>.LinkedSlot linkedSlot;
        if (tsSlotArray == null || index >= tsSlotArray.Length || ((linkedSlot = tsSlotArray[index].Value) == null || !this.m_initialized))
          return default (T);
        return linkedSlot.Value;
      }
    }

    internal List<T> ValuesForDebugDisplay
    {
      get
      {
        return this.GetValuesAsList();
      }
    }

    private void GrowTable(ref ThreadLocal<T>.LinkedSlotVolatile[] table, int minLength)
    {
      ThreadLocal<T>.LinkedSlotVolatile[] linkedSlotVolatileArray = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(minLength)];
      lock (ThreadLocal<T>.s_idManager)
      {
        for (int index = 0; index < table.Length; ++index)
        {
          ThreadLocal<T>.LinkedSlot linkedSlot = table[index].Value;
          if (linkedSlot != null && linkedSlot.SlotArray != null)
          {
            linkedSlot.SlotArray = linkedSlotVolatileArray;
            linkedSlotVolatileArray[index] = table[index];
          }
        }
      }
      table = linkedSlotVolatileArray;
    }

    private static int GetNewTableSize(int minSize)
    {
      if ((uint) minSize > 2146435071U)
        return int.MaxValue;
      int num1 = minSize - 1;
      int num2 = num1 | num1 >> 1;
      int num3 = num2 | num2 >> 2;
      int num4 = num3 | num3 >> 4;
      int num5 = num4 | num4 >> 8;
      int num6 = (num5 | num5 >> 16) + 1;
      if ((uint) num6 > 2146435071U)
        num6 = 2146435071;
      return num6;
    }

    private struct LinkedSlotVolatile
    {
      internal volatile ThreadLocal<T>.LinkedSlot Value;
    }

    private sealed class LinkedSlot
    {
      internal volatile ThreadLocal<T>.LinkedSlot Next;
      internal volatile ThreadLocal<T>.LinkedSlot Previous;
      internal volatile ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;
      internal T Value;

      internal LinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
      {
        this.SlotArray = slotArray;
      }
    }

    private class IdManager
    {
      private List<bool> m_freeIds = new List<bool>();
      private int m_nextIdToTry;

      internal int GetId()
      {
        lock (this.m_freeIds)
        {
          int nextIdToTry = this.m_nextIdToTry;
          while (nextIdToTry < this.m_freeIds.Count && !this.m_freeIds[nextIdToTry])
            ++nextIdToTry;
          if (nextIdToTry == this.m_freeIds.Count)
            this.m_freeIds.Add(false);
          else
            this.m_freeIds[nextIdToTry] = false;
          this.m_nextIdToTry = nextIdToTry + 1;
          return nextIdToTry;
        }
      }

      internal void ReturnId(int id)
      {
        lock (this.m_freeIds)
        {
          this.m_freeIds[id] = true;
          if (id >= this.m_nextIdToTry)
            return;
          this.m_nextIdToTry = id;
        }
      }
    }

    private class FinalizationHelper
    {
      internal ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;
      private bool m_trackAllValues;

      internal FinalizationHelper(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, bool trackAllValues)
      {
        this.SlotArray = slotArray;
        this.m_trackAllValues = trackAllValues;
      }

      ~FinalizationHelper()
      {
        foreach (ThreadLocal<T>.LinkedSlotVolatile slot in this.SlotArray)
        {
          ThreadLocal<T>.LinkedSlot linkedSlot = slot.Value;
          if (linkedSlot != null)
          {
            if (this.m_trackAllValues)
            {
              linkedSlot.SlotArray = (ThreadLocal<T>.LinkedSlotVolatile[]) null;
            }
            else
            {
              lock (ThreadLocal<T>.s_idManager)
              {
                if (linkedSlot.Next != null)
                  linkedSlot.Next.Previous = linkedSlot.Previous;
                linkedSlot.Previous.Next = linkedSlot.Next;
              }
            }
          }
        }
      }
    }
  }
}
