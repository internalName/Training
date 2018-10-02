// Decompiled with JetBrains decompiler
// Type: System.Collections.ObjectModel.Collection`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.ObjectModel
{
  /// <summary>
  ///   Предоставляет базовый класс для универсальной коллекции.
  /// </summary>
  /// <typeparam name="T">Тип элементов в коллекции.</typeparam>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  public class Collection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
  {
    private IList<T> items;
    [NonSerialized]
    private object _syncRoot;

    /// <summary>
    ///   Инициализирует новый экземпляр пустого класса <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </summary>
    [__DynamicallyInvokable]
    public Collection()
    {
      this.items = (IList<T>) new List<T>();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.ObjectModel.Collection`1" /> класс как оболочка для указанного списка.
    /// </summary>
    /// <param name="list">
    ///   Список, который является оболочкой для новой коллекции.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="list" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public Collection(IList<T> list)
    {
      if (list == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
      this.items = list;
    }

    /// <summary>
    ///   Возвращает число элементов, содержащихся в <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this.items.Count;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.Generic.IList`1" /> оболочка <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Collections.Generic.IList`1" /> оболочка <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    protected IList<T> Items
    {
      [__DynamicallyInvokable] get
      {
        return this.items;
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
    ///   Значение параметра <paramref name="index" /> больше или равно значению свойства <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public T this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return this.items[index];
      }
      [__DynamicallyInvokable] set
      {
        if (this.items.IsReadOnly)
          ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        if (index < 0 || index >= this.items.Count)
          ThrowHelper.ThrowArgumentOutOfRangeException();
        this.SetItem(index, value);
      }
    }

    /// <summary>
    ///   Добавляет объект в конец очереди <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект, добавляемый в конец коллекции <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    [__DynamicallyInvokable]
    public void Add(T item)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      this.InsertItem(this.items.Count, item);
    }

    /// <summary>
    ///   Удаляет все элементы из коллекции <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Clear()
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      this.ClearItems();
    }

    /// <summary>
    ///   Копирует целый массив <see cref="T:System.Collections.ObjectModel.Collection`1" /> в совместимый одномерный массив <see cref="T:System.Array" />, начиная с заданного индекса целевого массива.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.ObjectModel.Collection`1" />.
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
    ///   Количество элементов в исходной коллекции <see cref="T:System.Collections.ObjectModel.Collection`1" /> больше, чем свободное пространство от <paramref name="index" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int index)
    {
      this.items.CopyTo(array, index);
    }

    /// <summary>
    ///   Определяет, входит ли элемент в коллекцию <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект для поиска в <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="item" /> найден в коллекции <see cref="T:System.Collections.ObjectModel.Collection`1" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Contains(T item)
    {
      return this.items.Contains(item);
    }

    /// <summary>
    ///   Возвращает перечислитель, осуществляющий перебор элементов списка <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </summary>
    /// <returns>
    ///   Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1" /> для <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public IEnumerator<T> GetEnumerator()
    {
      return this.items.GetEnumerator();
    }

    /// <summary>
    ///   Осуществляет поиск указанного объекта и возвращает отсчитываемый от нуля индекс первого вхождения, найденного в пределах всего списка <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект для поиска в <see cref="T:System.Collections.Generic.List`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Индекс (с нуля) первого вхождения параметра <paramref name="item" />, если оно найдено в коллекции <see cref="T:System.Collections.ObjectModel.Collection`1" />; в противном случае -1.
    /// </returns>
    [__DynamicallyInvokable]
    public int IndexOf(T item)
    {
      return this.items.IndexOf(item);
    }

    /// <summary>
    ///   Вставляет элемент в коллекцию <see cref="T:System.Collections.ObjectModel.Collection`1" /> по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому следует вставить элемент <paramref name="item" />.
    /// </param>
    /// <param name="item">
    ///   Вставляемый объект.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="index" /> больше значения <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void Insert(int index, T item)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      if (index < 0 || index > this.items.Count)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
      this.InsertItem(index, item);
    }

    /// <summary>
    ///   Удаляет первое вхождение указанного объекта из коллекции <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </summary>
    /// <param name="item">
    ///   Объект, который необходимо удалить из коллекции <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если элемент <paramref name="item" /> успешно удален, в противном случае — значение <see langword="false" />.
    ///     Этот метод также возвращает <see langword="false" />, если объект <paramref name="item" /> не был найден в исходной коллекции <see cref="T:System.Collections.ObjectModel.Collection`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Remove(T item)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      int index = this.items.IndexOf(item);
      if (index < 0)
        return false;
      this.RemoveItem(index);
      return true;
    }

    /// <summary>
    ///   Удаляет элемент списка <see cref="T:System.Collections.ObjectModel.Collection`1" /> с указанным индексом.
    /// </summary>
    /// <param name="index">
    ///   Индекс (с нуля) элемента, который требуется удалить.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="index" /> больше или равно значению свойства <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void RemoveAt(int index)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      if (index < 0 || index >= this.items.Count)
        ThrowHelper.ThrowArgumentOutOfRangeException();
      this.RemoveItem(index);
    }

    /// <summary>
    ///   Удаляет из коллекции <see cref="T:System.Collections.ObjectModel.Collection`1" /> все элементы.
    /// </summary>
    [__DynamicallyInvokable]
    protected virtual void ClearItems()
    {
      this.items.Clear();
    }

    /// <summary>
    ///   Вставляет элемент в коллекцию <see cref="T:System.Collections.ObjectModel.Collection`1" /> по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому следует вставить элемент <paramref name="item" />.
    /// </param>
    /// <param name="item">
    ///   Вставляемый объект.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="index" /> больше значения <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    protected virtual void InsertItem(int index, T item)
    {
      this.items.Insert(index, item);
    }

    /// <summary>
    ///   Удаляет элемент списка <see cref="T:System.Collections.ObjectModel.Collection`1" /> с указанным индексом.
    /// </summary>
    /// <param name="index">
    ///   Индекс (с нуля) элемента, который требуется удалить.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="index" /> больше или равно значению свойства <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    protected virtual void RemoveItem(int index)
    {
      this.items.RemoveAt(index);
    }

    /// <summary>Заменяет элемент по указанному индексу.</summary>
    /// <param name="index">
    ///   Индекс (с нуля) элемента, который требуется заменить.
    /// </param>
    /// <param name="item">
    ///   Новое значение элемента по указанному индексу.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="index" /> больше значения <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    protected virtual void SetItem(int index, T item)
    {
      this.items[index] = item;
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.items.IsReadOnly;
      }
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.items.GetEnumerator();
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
        {
          ICollection items = this.items as ICollection;
          if (items != null)
            this._syncRoot = items.SyncRoot;
          else
            Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        }
        return this._syncRoot;
      }
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
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (array.Length - index < this.Count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      T[] array1 = array as T[];
      if (array1 != null)
      {
        this.items.CopyTo(array1, index);
      }
      else
      {
        Type elementType = array.GetType().GetElementType();
        Type c = typeof (T);
        if (!elementType.IsAssignableFrom(c) && !c.IsAssignableFrom(elementType))
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        object[] objArray = array as object[];
        if (objArray == null)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        int count = this.items.Count;
        try
        {
          for (int index1 = 0; index1 < count; ++index1)
            objArray[index++] = (object) this.items[index1];
        }
        catch (ArrayTypeMismatchException ex)
        {
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        }
      }
    }

    [__DynamicallyInvokable]
    object IList.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return (object) this.items[index];
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

    [__DynamicallyInvokable]
    bool IList.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.items.IsReadOnly;
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsFixedSize
    {
      [__DynamicallyInvokable] get
      {
        IList items = this.items as IList;
        if (items != null)
          return items.IsFixedSize;
        return this.items.IsReadOnly;
      }
    }

    [__DynamicallyInvokable]
    int IList.Add(object value)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
      try
      {
        this.Add((T) value);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
      }
      return this.Count - 1;
    }

    [__DynamicallyInvokable]
    bool IList.Contains(object value)
    {
      if (Collection<T>.IsCompatibleObject(value))
        return this.Contains((T) value);
      return false;
    }

    [__DynamicallyInvokable]
    int IList.IndexOf(object value)
    {
      if (Collection<T>.IsCompatibleObject(value))
        return this.IndexOf((T) value);
      return -1;
    }

    [__DynamicallyInvokable]
    void IList.Insert(int index, object value)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
      try
      {
        this.Insert(index, (T) value);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
      }
    }

    [__DynamicallyInvokable]
    void IList.Remove(object value)
    {
      if (this.items.IsReadOnly)
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      if (!Collection<T>.IsCompatibleObject(value))
        return;
      this.Remove((T) value);
    }

    private static bool IsCompatibleObject(object value)
    {
      if (value is T)
        return true;
      if (value == null)
        return (object) default (T) == null;
      return false;
    }
  }
}
