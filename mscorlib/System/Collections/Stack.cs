// Decompiled with JetBrains decompiler
// Type: System.Collections.Stack
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
  ///   Представляет простую неуниверсальную коллекцию объектов, работающую по принципу «последним поступил — первым обслужен».
  /// </summary>
  [DebuggerTypeProxy(typeof (Stack.StackDebugView))]
  [DebuggerDisplay("Count = {Count}")]
  [ComVisible(true)]
  [Serializable]
  public class Stack : ICollection, IEnumerable, ICloneable
  {
    private object[] _array;
    private int _size;
    private int _version;
    [NonSerialized]
    private object _syncRoot;
    private const int _defaultCapacity = 10;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Stack" />, который является пустым и имеет начальную емкость по умолчанию.
    /// </summary>
    public Stack()
    {
      this._array = new object[10];
      this._size = 0;
      this._version = 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Stack" /> класс, который является пустым и обладает указанной начальной емкостью или начальной емкостью по умолчанию, какое значение больше.
    /// </summary>
    /// <param name="initialCapacity">
    ///   Начальное количество элементов, которое может содержать коллекция <see cref="T:System.Collections.Stack" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="initialCapacity" /> меньше нуля.
    /// </exception>
    public Stack(int initialCapacity)
    {
      if (initialCapacity < 0)
        throw new ArgumentOutOfRangeException(nameof (initialCapacity), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (initialCapacity < 10)
        initialCapacity = 10;
      this._array = new object[initialCapacity];
      this._size = 0;
      this._version = 0;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Stack" /> класс, который содержит элементы, скопированные из указанной коллекции и имеет начальной емкостью, равной количеству скопированных элементов.
    /// </summary>
    /// <param name="col">
    ///   <see cref="T:System.Collections.ICollection" /> Копировать элементы.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="col" /> имеет значение <see langword="null" />.
    /// </exception>
    public Stack(ICollection col)
      : this(col == null ? 32 : col.Count)
    {
      if (col == null)
        throw new ArgumentNullException(nameof (col));
      foreach (object obj in (IEnumerable) col)
        this.Push(obj);
    }

    /// <summary>
    ///   Получает число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Stack" />.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Stack" />.
    /// </returns>
    public virtual int Count
    {
      get
      {
        return this._size;
      }
    }

    /// <summary>
    ///   Возвращает значение, показывающее, является ли доступ к коллекции <see cref="T:System.Collections.Stack" /> синхронизированным (потокобезопасным).
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если доступ к <see cref="T:System.Collections.Stack" /> является синхронизированным (потокобезопасным); в противном случае — <see langword="false" />.
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
    ///   Получает объект, с помощью которого можно синхронизировать доступ к коллекции <see cref="T:System.Collections.Stack" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Object" />, который может использоваться для синхронизации доступа к коллекции <see cref="T:System.Collections.Stack" />.
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
    ///   Удаляет все объекты из <see cref="T:System.Collections.Stack" />.
    /// </summary>
    public virtual void Clear()
    {
      Array.Clear((Array) this._array, 0, this._size);
      this._size = 0;
      ++this._version;
    }

    /// <summary>
    ///   Создает неполную копию <see cref="T:System.Collections.Stack" />.
    /// </summary>
    /// <returns>
    ///   Неполная копия <see cref="T:System.Collections.Stack" />.
    /// </returns>
    public virtual object Clone()
    {
      Stack stack = new Stack(this._size);
      stack._size = this._size;
      Array.Copy((Array) this._array, 0, (Array) stack._array, 0, this._size);
      stack._version = this._version;
      return (object) stack;
    }

    /// <summary>
    ///   Определяет, входит ли элемент в коллекцию <see cref="T:System.Collections.Stack" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект для поиска в <see cref="T:System.Collections.Stack" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> найден в коллекции <see cref="T:System.Collections.Stack" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool Contains(object obj)
    {
      int size = this._size;
      while (size-- > 0)
      {
        if (obj == null)
        {
          if (this._array[size] == null)
            return true;
        }
        else if (this._array[size] != null && this._array[size].Equals(obj))
          return true;
      }
      return false;
    }

    /// <summary>
    ///   Копирует <see cref="T:System.Collections.Stack" /> в существующий одномерный массив <see cref="T:System.Array" />, начиная с указанного индекса массива.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.Stack" />.
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
    ///   Количество элементов в исходной коллекции <see cref="T:System.Collections.Stack" /> больше, чем свободное пространство от <paramref name="index" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Тип источника <see cref="T:System.Collections.Stack" /> не может быть автоматически приведен к типу массива назначения <paramref name="array" />.
    /// </exception>
    public virtual void CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - index < this._size)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      int num = 0;
      if (array is object[])
      {
        object[] objArray = (object[]) array;
        for (; num < this._size; ++num)
          objArray[num + index] = this._array[this._size - num - 1];
      }
      else
      {
        for (; num < this._size; ++num)
          array.SetValue(this._array[this._size - num - 1], num + index);
      }
    }

    /// <summary>
    ///   Возвращает перечислитель <see cref="T:System.Collections.IEnumerator" /> для словаря <see cref="T:System.Collections.Stack" />.
    /// </summary>
    /// <returns>
    ///   Интерфейс <see cref="T:System.Collections.IEnumerator" /> для <see cref="T:System.Collections.Stack" />.
    /// </returns>
    public virtual IEnumerator GetEnumerator()
    {
      return (IEnumerator) new Stack.StackEnumerator(this);
    }

    /// <summary>
    ///   Возвращает объект в верхней части <see cref="T:System.Collections.Stack" /> без его удаления.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Object" /> В верхней части <see cref="T:System.Collections.Stack" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Очередь <see cref="T:System.Collections.Stack" /> является пустой.
    /// </exception>
    public virtual object Peek()
    {
      if (this._size == 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
      return this._array[this._size - 1];
    }

    /// <summary>
    ///   Удаляет и возвращает объект, находящийся в начале <see cref="T:System.Collections.Stack" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Collections.Stack" />, удаленный из начала <see cref="T:System.Object" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Очередь <see cref="T:System.Collections.Stack" /> является пустой.
    /// </exception>
    public virtual object Pop()
    {
      if (this._size == 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
      ++this._version;
      object obj = this._array[--this._size];
      this._array[this._size] = (object) null;
      return obj;
    }

    /// <summary>
    ///   Вставляет объект как верхний элемент стека <see cref="T:System.Collections.Stack" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект <see cref="T:System.Object" />, вставляемый в <see cref="T:System.Collections.Stack" />.
    ///    Допускается значение <see langword="null" />.
    /// </param>
    public virtual void Push(object obj)
    {
      if (this._size == this._array.Length)
      {
        object[] objArray = new object[2 * this._array.Length];
        Array.Copy((Array) this._array, 0, (Array) objArray, 0, this._size);
        this._array = objArray;
      }
      this._array[this._size++] = obj;
      ++this._version;
    }

    /// <summary>
    ///   Возвращает синхронизированную (потокобезопасную) оболочку для <see cref="T:System.Collections.Stack" />.
    /// </summary>
    /// <param name="stack">
    ///   Коллекция <see cref="T:System.Collections.Stack" />, которую требуется синхронизировать.
    /// </param>
    /// <returns>
    ///   Создание синхронизированной оболочки <see cref="T:System.Collections.Stack" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="stack" /> имеет значение <see langword="null" />.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static Stack Synchronized(Stack stack)
    {
      if (stack == null)
        throw new ArgumentNullException(nameof (stack));
      return (Stack) new Stack.SyncStack(stack);
    }

    /// <summary>
    ///   Копирует <see cref="T:System.Collections.Stack" /> в новый массив.
    /// </summary>
    /// <returns>
    ///   Новый массив, содержащий копии элементов <see cref="T:System.Collections.Stack" />.
    /// </returns>
    public virtual object[] ToArray()
    {
      object[] objArray = new object[this._size];
      for (int index = 0; index < this._size; ++index)
        objArray[index] = this._array[this._size - index - 1];
      return objArray;
    }

    [Serializable]
    private class SyncStack : Stack
    {
      private Stack _s;
      private object _root;

      internal SyncStack(Stack stack)
      {
        this._s = stack;
        this._root = stack.SyncRoot;
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
          return this._root;
        }
      }

      public override int Count
      {
        get
        {
          lock (this._root)
            return this._s.Count;
        }
      }

      public override bool Contains(object obj)
      {
        lock (this._root)
          return this._s.Contains(obj);
      }

      public override object Clone()
      {
        lock (this._root)
          return (object) new Stack.SyncStack((Stack) this._s.Clone());
      }

      public override void Clear()
      {
        lock (this._root)
          this._s.Clear();
      }

      public override void CopyTo(Array array, int arrayIndex)
      {
        lock (this._root)
          this._s.CopyTo(array, arrayIndex);
      }

      public override void Push(object value)
      {
        lock (this._root)
          this._s.Push(value);
      }

      public override object Pop()
      {
        lock (this._root)
          return this._s.Pop();
      }

      public override IEnumerator GetEnumerator()
      {
        lock (this._root)
          return this._s.GetEnumerator();
      }

      public override object Peek()
      {
        lock (this._root)
          return this._s.Peek();
      }

      public override object[] ToArray()
      {
        lock (this._root)
          return this._s.ToArray();
      }
    }

    [Serializable]
    private class StackEnumerator : IEnumerator, ICloneable
    {
      private Stack _stack;
      private int _index;
      private int _version;
      private object currentElement;

      internal StackEnumerator(Stack stack)
      {
        this._stack = stack;
        this._version = this._stack._version;
        this._index = -2;
        this.currentElement = (object) null;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public virtual bool MoveNext()
      {
        if (this._version != this._stack._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this._index == -2)
        {
          this._index = this._stack._size - 1;
          bool flag = this._index >= 0;
          if (flag)
            this.currentElement = this._stack._array[this._index];
          return flag;
        }
        if (this._index == -1)
          return false;
        bool flag1 = --this._index >= 0;
        this.currentElement = !flag1 ? (object) null : this._stack._array[this._index];
        return flag1;
      }

      public virtual object Current
      {
        get
        {
          if (this._index == -2)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._index == -1)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          return this.currentElement;
        }
      }

      public virtual void Reset()
      {
        if (this._version != this._stack._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this._index = -2;
        this.currentElement = (object) null;
      }
    }

    internal class StackDebugView
    {
      private Stack stack;

      public StackDebugView(Stack stack)
      {
        if (stack == null)
          throw new ArgumentNullException(nameof (stack));
        this.stack = stack;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public object[] Items
      {
        get
        {
          return this.stack.ToArray();
        }
      }
    }
  }
}
