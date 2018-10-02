// Decompiled with JetBrains decompiler
// Type: System.Collections.DictionaryBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Предоставляет базовый класс <see langword="abstract" /> для строго типизированной коллекции пар "ключ-значение".
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public abstract class DictionaryBase : IDictionary, ICollection, IEnumerable
  {
    private Hashtable hashtable;

    /// <summary>
    ///   Возвращает список элементов, содержащихся в <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Collections.Hashtable" /> представляет <see cref="T:System.Collections.DictionaryBase" /> экземпляр.
    /// </returns>
    protected Hashtable InnerHashtable
    {
      get
      {
        if (this.hashtable == null)
          this.hashtable = new Hashtable();
        return this.hashtable;
      }
    }

    /// <summary>
    ///   Возвращает список элементов, содержащихся в <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IDictionary" /> Представляет <see cref="T:System.Collections.DictionaryBase" /> экземпляр.
    /// </returns>
    protected IDictionary Dictionary
    {
      get
      {
        return (IDictionary) this;
      }
    }

    /// <summary>
    ///   Возвращает число элементов, содержащихся в <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </returns>
    public int Count
    {
      get
      {
        if (this.hashtable != null)
          return this.hashtable.Count;
        return 0;
      }
    }

    bool IDictionary.IsReadOnly
    {
      get
      {
        return this.InnerHashtable.IsReadOnly;
      }
    }

    bool IDictionary.IsFixedSize
    {
      get
      {
        return this.InnerHashtable.IsFixedSize;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return this.InnerHashtable.IsSynchronized;
      }
    }

    ICollection IDictionary.Keys
    {
      get
      {
        return this.InnerHashtable.Keys;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return this.InnerHashtable.SyncRoot;
      }
    }

    ICollection IDictionary.Values
    {
      get
      {
        return this.InnerHashtable.Values;
      }
    }

    /// <summary>
    ///   Копирует <see cref="T:System.Collections.DictionaryBase" /> элементы в одномерном массиве <see cref="T:System.Array" /> по указанному индексу.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" /> который является конечным <see cref="T:System.Collections.DictionaryEntry" /> объекты из <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
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
    ///   Количество элементов в исходной коллекции <see cref="T:System.Collections.DictionaryBase" /> больше, чем свободное пространство от <paramref name="index" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.InvalidCastException">
    ///   Тип источника <see cref="T:System.Collections.DictionaryBase" /> не может быть автоматически приведен к типу массива назначения <paramref name="array" />.
    /// </exception>
    public void CopyTo(Array array, int index)
    {
      this.InnerHashtable.CopyTo(array, index);
    }

    object IDictionary.this[object key]
    {
      get
      {
        object currentValue = this.InnerHashtable[key];
        this.OnGet(key, currentValue);
        return currentValue;
      }
      set
      {
        this.OnValidate(key, value);
        bool flag = true;
        object oldValue = this.InnerHashtable[key];
        if (oldValue == null)
          flag = this.InnerHashtable.Contains(key);
        this.OnSet(key, oldValue, value);
        this.InnerHashtable[key] = value;
        try
        {
          this.OnSetComplete(key, oldValue, value);
        }
        catch
        {
          if (flag)
            this.InnerHashtable[key] = oldValue;
          else
            this.InnerHashtable.Remove(key);
          throw;
        }
      }
    }

    bool IDictionary.Contains(object key)
    {
      return this.InnerHashtable.Contains(key);
    }

    void IDictionary.Add(object key, object value)
    {
      this.OnValidate(key, value);
      this.OnInsert(key, value);
      this.InnerHashtable.Add(key, value);
      try
      {
        this.OnInsertComplete(key, value);
      }
      catch
      {
        this.InnerHashtable.Remove(key);
        throw;
      }
    }

    /// <summary>
    ///   Удаляет содержимое экземпляра <see cref="T:System.Collections.DictionaryBase" />.
    /// </summary>
    public void Clear()
    {
      this.OnClear();
      this.InnerHashtable.Clear();
      this.OnClearComplete();
    }

    void IDictionary.Remove(object key)
    {
      if (!this.InnerHashtable.Contains(key))
        return;
      object obj = this.InnerHashtable[key];
      this.OnValidate(key, obj);
      this.OnRemove(key, obj);
      this.InnerHashtable.Remove(key);
      try
      {
        this.OnRemoveComplete(key, obj);
      }
      catch
      {
        this.InnerHashtable.Add(key, obj);
        throw;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IDictionaryEnumerator" /> который осуществляет перебор <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   Перечислитель <see cref="T:System.Collections.IDictionaryEnumerator" /> для экземпляра класса <see cref="T:System.Collections.DictionaryBase" />.
    /// </returns>
    public IDictionaryEnumerator GetEnumerator()
    {
      return this.InnerHashtable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.InnerHashtable.GetEnumerator();
    }

    /// <summary>
    ///   Возвращает элемент с указанным ключом и значением в <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется получить.
    /// </param>
    /// <param name="currentValue">
    ///   Текущее значение элемента, связанного с <paramref name="key" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Object" /> Содержащий элемент с указанным ключом и значением.
    /// </returns>
    protected virtual object OnGet(object key, object currentValue)
    {
      return currentValue;
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия перед заданием значения <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    /// <param name="key">Ключ элемента, который требуется найти.</param>
    /// <param name="oldValue">
    ///   Прежнее значение элемента, связанного с <paramref name="key" />.
    /// </param>
    /// <param name="newValue">
    ///   Новое значение элемента, связанного с <paramref name="key" />.
    /// </param>
    protected virtual void OnSet(object key, object oldValue, object newValue)
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия перед вставкой нового элемента в <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется вставить.
    /// </param>
    /// <param name="value">
    ///   Значение элемента, который требуется вставить.
    /// </param>
    protected virtual void OnInsert(object key, object value)
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия перед удалением содержимого из <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    protected virtual void OnClear()
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия перед удалением элемента из <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется удалить.
    /// </param>
    /// <param name="value">
    ///   Значение элемента, который нужно удалить.
    /// </param>
    protected virtual void OnRemove(object key, object value)
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские операции при проверке элемента с указанными ключом и значением.
    /// </summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется проверить.
    /// </param>
    /// <param name="value">
    ///   Значение элемента, который требуется проверить.
    /// </param>
    protected virtual void OnValidate(object key, object value)
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия после задания значения <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    /// <param name="key">Ключ элемента, который требуется найти.</param>
    /// <param name="oldValue">
    ///   Прежнее значение элемента, связанного с <paramref name="key" />.
    /// </param>
    /// <param name="newValue">
    ///   Новое значение элемента, связанного с <paramref name="key" />.
    /// </param>
    protected virtual void OnSetComplete(object key, object oldValue, object newValue)
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия после вставки нового элемента в <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется вставить.
    /// </param>
    /// <param name="value">
    ///   Значение элемента, который требуется вставить.
    /// </param>
    protected virtual void OnInsertComplete(object key, object value)
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия после очистки содержимого <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    protected virtual void OnClearComplete()
    {
    }

    /// <summary>
    ///   Выполняет дополнительные пользовательские действия после удаления элемента из <see cref="T:System.Collections.DictionaryBase" /> экземпляра.
    /// </summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется удалить.
    /// </param>
    /// <param name="value">
    ///   Значение элемента, который нужно удалить.
    /// </param>
    protected virtual void OnRemoveComplete(object key, object value)
    {
    }
  }
}
