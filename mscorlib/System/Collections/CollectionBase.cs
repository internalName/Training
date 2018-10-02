// Decompiled with JetBrains decompiler
// Type: System.Collections.CollectionBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Предоставляет базовый класс <see langword="abstract" /> для строго типизированной коллекции.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class CollectionBase : IList, ICollection, IEnumerable
  {
    private ArrayList list;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.CollectionBase" /> класса с начальной емкостью по умолчанию.
    /// </summary>
    protected CollectionBase()
    {
      this.list = new ArrayList();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.CollectionBase" /> класса с указанной емкостью.
    /// </summary>
    /// <param name="capacity">
    ///   Число элементов, которые может изначально вместить новый список.
    /// </param>
    protected CollectionBase(int capacity)
    {
      this.list = new ArrayList(capacity);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.ArrayList" /> со списком элементов в <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.ArrayList" /> Представляет <see cref="T:System.Collections.CollectionBase" /> экземпляр.
    /// 
    ///   Получение значения данного свойства является операцией порядка сложности O(1).
    /// </returns>
    protected ArrayList InnerList
    {
      get
      {
        if (this.list == null)
          this.list = new ArrayList();
        return this.list;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IList" /> со списком элементов в <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IList" /> Представляет <see cref="T:System.Collections.CollectionBase" /> экземпляр.
    /// </returns>
    protected IList List
    {
      get
      {
        return (IList) this;
      }
    }

    /// <summary>
    ///   Возвращает или задает число элементов, которое может содержать список <see cref="T:System.Collections.CollectionBase" />.
    /// </summary>
    /// <returns>
    ///   Количество элементов, которое может содержать коллекция <see cref="T:System.Collections.CollectionBase" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Для <see cref="P:System.Collections.CollectionBase.Capacity" /> установлено значение, которое меньше <see cref="P:System.Collections.CollectionBase.Count" />.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти в системе.
    /// </exception>
    [ComVisible(false)]
    public int Capacity
    {
      get
      {
        return this.InnerList.Capacity;
      }
      set
      {
        this.InnerList.Capacity = value;
      }
    }

    /// <summary>
    ///   Возвращает число элементов, содержащихся в <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    ///    Это свойство нельзя переопределить.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    /// 
    ///   Получение значения данного свойства является операцией порядка сложности O(1).
    /// </returns>
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        if (this.list != null)
          return this.list.Count;
        return 0;
      }
    }

    /// <summary>
    ///   Удаляет все объекты из <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    ///    Этот метод не может быть переопределен.
    /// </summary>
    [__DynamicallyInvokable]
    public void Clear()
    {
      this.OnClear();
      this.InnerList.Clear();
      this.OnClearComplete();
    }

    /// <summary>
    ///   Удаляет элемент по указанному индексу <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    ///    Этот метод не является переопределяемым.
    /// </summary>
    /// <param name="index">
    ///   Индекс (с нуля) элемента, который требуется удалить.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="index" /> больше или равно значению свойства <see cref="P:System.Collections.CollectionBase.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    public void RemoveAt(int index)
    {
      if (index < 0 || index >= this.Count)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      object inner = this.InnerList[index];
      this.OnValidate(inner);
      this.OnRemove(index, inner);
      this.InnerList.RemoveAt(index);
      try
      {
        this.OnRemoveComplete(index, inner);
      }
      catch
      {
        this.InnerList.Insert(index, inner);
        throw;
      }
    }

    bool IList.IsReadOnly
    {
      get
      {
        return this.InnerList.IsReadOnly;
      }
    }

    bool IList.IsFixedSize
    {
      get
      {
        return this.InnerList.IsFixedSize;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return this.InnerList.IsSynchronized;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return this.InnerList.SyncRoot;
      }
    }

    void ICollection.CopyTo(Array array, int index)
    {
      this.InnerList.CopyTo(array, index);
    }

    object IList.this[int index]
    {
      get
      {
        if (index < 0 || index >= this.Count)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        return this.InnerList[index];
      }
      set
      {
        if (index < 0 || index >= this.Count)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        this.OnValidate(value);
        object inner = this.InnerList[index];
        this.OnSet(index, inner, value);
        this.InnerList[index] = value;
        try
        {
          this.OnSetComplete(index, inner, value);
        }
        catch
        {
          this.InnerList[index] = inner;
          throw;
        }
      }
    }

    bool IList.Contains(object value)
    {
      return this.InnerList.Contains(value);
    }

    int IList.Add(object value)
    {
      this.OnValidate(value);
      this.OnInsert(this.InnerList.Count, value);
      int index = this.InnerList.Add(value);
      try
      {
        this.OnInsertComplete(index, value);
      }
      catch
      {
        this.InnerList.RemoveAt(index);
        throw;
      }
      return index;
    }

    void IList.Remove(object value)
    {
      this.OnValidate(value);
      int index = this.InnerList.IndexOf(value);
      if (index < 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_RemoveArgNotFound"));
      this.OnRemove(index, value);
      this.InnerList.RemoveAt(index);
      try
      {
        this.OnRemoveComplete(index, value);
      }
      catch
      {
        this.InnerList.Insert(index, value);
        throw;
      }
    }

    int IList.IndexOf(object value)
    {
      return this.InnerList.IndexOf(value);
    }

    void IList.Insert(int index, object value)
    {
      if (index < 0 || index > this.Count)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      this.OnValidate(value);
      this.OnInsert(index, value);
      this.InnerList.Insert(index, value);
      try
      {
        this.OnInsertComplete(index, value);
      }
      catch
      {
        this.InnerList.RemoveAt(index);
        throw;
      }
    }

    /// <summary>
    ///   Возвращает перечислитель, выполняющий перебор элементов <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   Перечислитель <see cref="T:System.Collections.IEnumerator" /> для экземпляра класса <see cref="T:System.Collections.CollectionBase" />.
    /// </returns>
    [__DynamicallyInvokable]
    public IEnumerator GetEnumerator()
    {
      return this.InnerList.GetEnumerator();
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия перед заданием значения <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому <paramref name="oldValue" /> можно найти.
    /// </param>
    /// <param name="oldValue">
    ///   Значение для замены <paramref name="newValue" />.
    /// </param>
    /// <param name="newValue">
    ///   Новое значение элемента по <paramref name="index" />.
    /// </param>
    protected virtual void OnSet(int index, object oldValue, object newValue)
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия перед вставкой нового элемента в <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому следует вставить <paramref name="value" />.
    /// </param>
    /// <param name="value">
    ///   Новое значение элемента по <paramref name="index" />.
    /// </param>
    protected virtual void OnInsert(int index, object value)
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия при удалении содержимого <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    /// </summary>
    protected virtual void OnClear()
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия при удалении элемента из <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому <paramref name="value" /> можно найти.
    /// </param>
    /// <param name="value">
    ///   Значение элемента, удаляемого из <paramref name="index" />.
    /// </param>
    protected virtual void OnRemove(int index, object value)
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские операции при проверке значения.
    /// </summary>
    /// <param name="value">Объект для проверки.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    protected virtual void OnValidate(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия после задания значения <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому <paramref name="oldValue" /> можно найти.
    /// </param>
    /// <param name="oldValue">
    ///   Значение для замены <paramref name="newValue" />.
    /// </param>
    /// <param name="newValue">
    ///   Новое значение элемента по <paramref name="index" />.
    /// </param>
    protected virtual void OnSetComplete(int index, object oldValue, object newValue)
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия после вставки нового элемента в <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому следует вставить <paramref name="value" />.
    /// </param>
    /// <param name="value">
    ///   Новое значение элемента по <paramref name="index" />.
    /// </param>
    protected virtual void OnInsertComplete(int index, object value)
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия после очистки содержимого <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    /// </summary>
    protected virtual void OnClearComplete()
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия после удаления элемента из <see cref="T:System.Collections.CollectionBase" /> экземпляра.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому <paramref name="value" /> можно найти.
    /// </param>
    /// <param name="value">
    ///   Значение элемента, удаляемого из <paramref name="index" />.
    /// </param>
    protected virtual void OnRemoveComplete(int index, object value)
    {
    }
  }
}
