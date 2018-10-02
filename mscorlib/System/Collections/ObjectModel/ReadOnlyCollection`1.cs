// Decompiled with JetBrains decompiler
// Type: System.Collections.ObjectModel.ReadOnlyCollection`1
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
  ///   Предоставляет базовый класс для универсальной коллекции, доступной только для чтения.
  /// </summary>
  /// <typeparam name="T">Тип элементов в коллекции.</typeparam>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  public class ReadOnlyCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
  {
    private IList<T> list;
    [NonSerialized]
    private object _syncRoot;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> класс, являющийся оболочкой вокруг указанного списка только для чтения.
    /// </summary>
    /// <param name="list">
    ///   Список, для которого создается оболочка.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="list" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public ReadOnlyCollection(IList<T> list)
    {
      if (list == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
      this.list = list;
    }

    /// <summary>
    ///   Возвращает число элементов, содержащихся в <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> экземпляра.
    /// </returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this.list.Count;
      }
    }

    /// <summary>Получает элемент с указанным индексом.</summary>
    /// <param name="index">
    ///   Индекс элемента (с нуля), который требуется получить.
    /// </param>
    /// <returns>Элемент, расположенный по указанному индексу.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="index" /> больше или равно значению свойства <see cref="P:System.Collections.ObjectModel.ReadOnlyCollection`1.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public T this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return this.list[index];
      }
    }

    /// <summary>
    ///   Определяет, входит ли элемент в коллекцию <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.
    /// </summary>
    /// <param name="value">
    ///   Объект для поиска в <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="value" /> найден в коллекции <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Contains(T value)
    {
      return this.list.Contains(value);
    }

    /// <summary>
    ///   Копирует целый массив <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> в совместимый одномерный массив <see cref="T:System.Array" />, начиная с заданного индекса целевого массива.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.
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
    ///   Количество элементов в исходной коллекции <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> больше, чем свободное пространство от <paramref name="index" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void CopyTo(T[] array, int index)
    {
      this.list.CopyTo(array, index);
    }

    /// <summary>
    ///   Возвращает перечислитель, осуществляющий перебор элементов списка <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.
    /// </summary>
    /// <returns>
    ///   Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1" /> для <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.
    /// </returns>
    [__DynamicallyInvokable]
    public IEnumerator<T> GetEnumerator()
    {
      return this.list.GetEnumerator();
    }

    /// <summary>
    ///   Осуществляет поиск указанного объекта и возвращает отсчитываемый от нуля индекс первого вхождения, найденного в пределах всего списка <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.
    /// </summary>
    /// <param name="value">
    ///   Объект для поиска в <see cref="T:System.Collections.Generic.List`1" />.
    ///    Для ссылочных типов допускается значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Индекс (с нуля) первого вхождения параметра <paramref name="item" />, если оно найдено в коллекции <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />; в противном случае -1.
    /// </returns>
    [__DynamicallyInvokable]
    public int IndexOf(T value)
    {
      return this.list.IndexOf(value);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.Generic.IList`1" /><see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> создает оболочку.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.Generic.IList`1" /><see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> Создает оболочку.
    /// </returns>
    [__DynamicallyInvokable]
    protected IList<T> Items
    {
      [__DynamicallyInvokable] get
      {
        return this.list;
      }
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    T IList<T>.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return this.list[index];
      }
      [__DynamicallyInvokable] set
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }
    }

    [__DynamicallyInvokable]
    void ICollection<T>.Add(T value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void ICollection<T>.Clear()
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void IList<T>.Insert(int index, T value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    bool ICollection<T>.Remove(T value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      return false;
    }

    [__DynamicallyInvokable]
    void IList<T>.RemoveAt(int index)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.list.GetEnumerator();
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
          ICollection list = this.list as ICollection;
          if (list != null)
            this._syncRoot = list.SyncRoot;
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
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (array.Length - index < this.Count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      T[] array1 = array as T[];
      if (array1 != null)
      {
        this.list.CopyTo(array1, index);
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
        int count = this.list.Count;
        try
        {
          for (int index1 = 0; index1 < count; ++index1)
            objArray[index++] = (object) this.list[index1];
        }
        catch (ArrayTypeMismatchException ex)
        {
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
        }
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsFixedSize
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    bool IList.IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    [__DynamicallyInvokable]
    object IList.this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return (object) this.list[index];
      }
      [__DynamicallyInvokable] set
      {
        ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      }
    }

    [__DynamicallyInvokable]
    int IList.Add(object value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
      return -1;
    }

    [__DynamicallyInvokable]
    void IList.Clear()
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
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
    bool IList.Contains(object value)
    {
      if (ReadOnlyCollection<T>.IsCompatibleObject(value))
        return this.Contains((T) value);
      return false;
    }

    [__DynamicallyInvokable]
    int IList.IndexOf(object value)
    {
      if (ReadOnlyCollection<T>.IsCompatibleObject(value))
        return this.IndexOf((T) value);
      return -1;
    }

    [__DynamicallyInvokable]
    void IList.Insert(int index, object value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void IList.Remove(object value)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }

    [__DynamicallyInvokable]
    void IList.RemoveAt(int index)
    {
      ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
    }
  }
}
