// Decompiled with JetBrains decompiler
// Type: System.Collections.ObjectModel.KeyedCollection`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections.ObjectModel
{
  /// <summary>
  ///   Предоставляет абстрактный базовый класс для коллекции, ключи которой внедрены в значения.
  /// </summary>
  /// <typeparam name="TKey">Тип ключей в коллекции.</typeparam>
  /// <typeparam name="TItem">Тип элементов в коллекции.</typeparam>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (Mscorlib_KeyedCollectionDebugView<,>))]
  [DebuggerDisplay("Count = {Count}")]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class KeyedCollection<TKey, TItem> : Collection<TItem>
  {
    private const int defaultThreshold = 0;
    private IEqualityComparer<TKey> comparer;
    private System.Collections.Generic.Dictionary<TKey, TItem> dict;
    private int keyCount;
    private int threshold;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> класс, который использует компаратор по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    protected KeyedCollection()
      : this((IEqualityComparer<TKey>) null, 0)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> класс, который использует заданную функцию сравнения.
    /// </summary>
    /// <param name="comparer">
    ///   Реализация <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> универсальный интерфейс для использования при сравнении ключей, или <see langword="null" /> использовать компаратор по умолчанию для типа ключа, полученные из <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.
    /// </param>
    [__DynamicallyInvokable]
    protected KeyedCollection(IEqualityComparer<TKey> comparer)
      : this(comparer, 0)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> класс, который использует указанный компаратор и создает словарь поиска при превышении указанного предела.
    /// </summary>
    /// <param name="comparer">
    ///   Реализация <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> универсальный интерфейс для использования при сравнении ключей, или <see langword="null" /> использовать компаратор по умолчанию для типа ключа, полученные из <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.
    /// </param>
    /// <param name="dictionaryCreationThreshold">
    ///   Количество элементов, которое может содержать коллекция без создания словаря поиска (при значении 0 словарь поиска создается при добавлении первого элемента), или –1, чтобы определить, что словарь поиска не будет создаться никогда.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="dictionaryCreationThreshold" /> — меньше -1.
    /// </exception>
    [__DynamicallyInvokable]
    protected KeyedCollection(IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold)
    {
      if (comparer == null)
        comparer = (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
      if (dictionaryCreationThreshold == -1)
        dictionaryCreationThreshold = int.MaxValue;
      if (dictionaryCreationThreshold < -1)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.dictionaryCreationThreshold, ExceptionResource.ArgumentOutOfRange_InvalidThreshold);
      this.comparer = comparer;
      this.threshold = dictionaryCreationThreshold;
    }

    /// <summary>
    ///   Получает универсальный компаратор, используемый для определения равенства ключей в коллекции.
    /// </summary>
    /// <returns>
    ///   Реализация <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> универсальный интерфейс, который используется для определения равенства ключей в коллекции.
    /// </returns>
    [__DynamicallyInvokable]
    public IEqualityComparer<TKey> Comparer
    {
      [__DynamicallyInvokable] get
      {
        return this.comparer;
      }
    }

    /// <summary>Получает элемент с указанным ключом.</summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется получить.
    /// </param>
    /// <returns>
    ///   Элемент с указанным ключом.
    ///    Если элемент с указанным ключом не найден, выдается исключение.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
    ///   Элемент с указанным ключом не существует в коллекции.
    /// </exception>
    [__DynamicallyInvokable]
    public TItem this[TKey key]
    {
      [__DynamicallyInvokable] get
      {
        if ((object) key == null)
          ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
        if (this.dict != null)
          return this.dict[key];
        foreach (TItem obj in (IEnumerable<TItem>) this.Items)
        {
          if (this.comparer.Equals(this.GetKeyForItem(obj), key))
            return obj;
        }
        ThrowHelper.ThrowKeyNotFoundException();
        return default (TItem);
      }
    }

    /// <summary>
    ///   Определяет, содержится ли в коллекции элемент с указанным ключом.
    /// </summary>
    /// <param name="key">
    ///   Ключ, который требуется найти в <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> содержит элемент с указанным ключом, в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Contains(TKey key)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      if (this.dict != null)
        return this.dict.ContainsKey(key);
      if ((object) key != null)
      {
        foreach (TItem obj in (IEnumerable<TItem>) this.Items)
        {
          if (this.comparer.Equals(this.GetKeyForItem(obj), key))
            return true;
        }
      }
      return false;
    }

    private bool ContainsItem(TItem item)
    {
      TKey keyForItem;
      if (this.dict == null || (object) (keyForItem = this.GetKeyForItem(item)) == null)
        return this.Items.Contains(item);
      TItem x;
      if (this.dict.TryGetValue(keyForItem, out x))
        return EqualityComparer<TItem>.Default.Equals(x, item);
      return false;
    }

    /// <summary>
    ///   Удаляет элемент с указанным ключом из <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ элемента, который требуется удалить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если элемент успешно удален; в противном случае — значение <see langword="false" />.
    ///     Этот метод также возвращает <see langword="false" />, если элемент <paramref name="key" /> не найден в коллекции <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public bool Remove(TKey key)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      if (this.dict != null)
      {
        if (this.dict.ContainsKey(key))
          return this.Remove(this.dict[key]);
        return false;
      }
      if ((object) key != null)
      {
        for (int index = 0; index < this.Items.Count; ++index)
        {
          if (this.comparer.Equals(this.GetKeyForItem(this.Items[index]), key))
          {
            this.RemoveItem(index);
            return true;
          }
        }
      }
      return false;
    }

    /// <summary>
    ///   Возвращает словарь поиска <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.
    /// </summary>
    /// <returns>
    ///   Словарь поиска <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />, если он существует; в противном случае — <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    protected IDictionary<TKey, TItem> Dictionary
    {
      [__DynamicallyInvokable] get
      {
        return (IDictionary<TKey, TItem>) this.dict;
      }
    }

    /// <summary>
    ///   Изменяет ключ, связанный с указанным элементом в словаре поиска.
    /// </summary>
    /// <param name="item">
    ///   Элемент, ключ которого требуется изменить.
    /// </param>
    /// <param name="newKey">
    ///   Новый ключ для <paramref name="item" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="item" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="item" /> не найден.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="key" /> уже существует в <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.
    /// </exception>
    [__DynamicallyInvokable]
    protected void ChangeItemKey(TItem item, TKey newKey)
    {
      if (!this.ContainsItem(item))
        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_ItemNotExist);
      TKey keyForItem = this.GetKeyForItem(item);
      if (this.comparer.Equals(keyForItem, newKey))
        return;
      if ((object) newKey != null)
        this.AddKey(newKey, item);
      if ((object) keyForItem == null)
        return;
      this.RemoveKey(keyForItem);
    }

    /// <summary>
    ///   Удаляет из коллекции <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> все элементы.
    /// </summary>
    [__DynamicallyInvokable]
    protected override void ClearItems()
    {
      base.ClearItems();
      if (this.dict != null)
        this.dict.Clear();
      this.keyCount = 0;
    }

    /// <summary>
    ///   При реализации в производном классе извлекает ключ из указанного элемента.
    /// </summary>
    /// <param name="item">
    ///   Элемент, из которого нужно извлечь ключ.
    /// </param>
    /// <returns>Ключ для указанного элемента.</returns>
    [__DynamicallyInvokable]
    protected abstract TKey GetKeyForItem(TItem item);

    /// <summary>
    ///   Вставляет элемент в коллекцию <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> по указанному индексу.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, по которому следует вставить элемент <paramref name="item" />.
    /// </param>
    /// <param name="item">Вставляемый объект.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="index" /> больше значения <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
    /// </exception>
    [__DynamicallyInvokable]
    protected override void InsertItem(int index, TItem item)
    {
      TKey keyForItem = this.GetKeyForItem(item);
      if ((object) keyForItem != null)
        this.AddKey(keyForItem, item);
      base.InsertItem(index, item);
    }

    /// <summary>
    ///   Удаляет элемент списка <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> с указанным индексом.
    /// </summary>
    /// <param name="index">
    ///   Индекс элемента, который должен быть удален.
    /// </param>
    [__DynamicallyInvokable]
    protected override void RemoveItem(int index)
    {
      TKey keyForItem = this.GetKeyForItem(this.Items[index]);
      if ((object) keyForItem != null)
        this.RemoveKey(keyForItem);
      base.RemoveItem(index);
    }

    /// <summary>
    ///   Заменяет элемент по заданному индексу указанным элементом.
    /// </summary>
    /// <param name="index">Индекс (с нуля) заменяемого элемента.</param>
    /// <param name="item">Новый элемент.</param>
    [__DynamicallyInvokable]
    protected override void SetItem(int index, TItem item)
    {
      TKey keyForItem1 = this.GetKeyForItem(item);
      TKey keyForItem2 = this.GetKeyForItem(this.Items[index]);
      if (this.comparer.Equals(keyForItem2, keyForItem1))
      {
        if ((object) keyForItem1 != null && this.dict != null)
          this.dict[keyForItem1] = item;
      }
      else
      {
        if ((object) keyForItem1 != null)
          this.AddKey(keyForItem1, item);
        if ((object) keyForItem2 != null)
          this.RemoveKey(keyForItem2);
      }
      base.SetItem(index, item);
    }

    private void AddKey(TKey key, TItem item)
    {
      if (this.dict != null)
        this.dict.Add(key, item);
      else if (this.keyCount == this.threshold)
      {
        this.CreateDictionary();
        this.dict.Add(key, item);
      }
      else
      {
        if (this.Contains(key))
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
        ++this.keyCount;
      }
    }

    private void CreateDictionary()
    {
      this.dict = new System.Collections.Generic.Dictionary<TKey, TItem>(this.comparer);
      foreach (TItem obj in (IEnumerable<TItem>) this.Items)
      {
        TKey keyForItem = this.GetKeyForItem(obj);
        if ((object) keyForItem != null)
          this.dict.Add(keyForItem, obj);
      }
    }

    private void RemoveKey(TKey key)
    {
      if (this.dict != null)
        this.dict.Remove(key);
      else
        --this.keyCount;
    }
  }
}
