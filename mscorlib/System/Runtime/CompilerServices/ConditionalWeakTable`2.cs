// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ConditionalWeakTable`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Позволяет компиляторам динамически прикреплять поля к управляемым объектам.
  /// </summary>
  /// <typeparam name="TKey">
  ///   Ссылочный тип, к которому прикрепляется поле.
  /// </typeparam>
  /// <typeparam name="TValue">
  ///   Тип поля.
  ///    Это должен быть ссылочный тип.
  /// </typeparam>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  public sealed class ConditionalWeakTable<TKey, TValue> where TKey : class where TValue : class
  {
    private int[] _buckets;
    private ConditionalWeakTable<TKey, TValue>.Entry[] _entries;
    private int _freeList;
    private const int _initialCapacity = 5;
    private readonly object _lock;
    private bool _invalid;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.ConditionalWeakTable`2" />.
    /// </summary>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public ConditionalWeakTable()
    {
      this._buckets = new int[0];
      this._entries = new ConditionalWeakTable<TKey, TValue>.Entry[0];
      this._freeList = -1;
      this._lock = new object();
      this.Resize();
    }

    /// <summary>Получает значение заданного ключа.</summary>
    /// <param name="key">
    ///   Ключ, представляющий объект с вложенным свойством зависимостей.
    /// </param>
    /// <param name="value">
    ///   При возвращении этот метод содержит значение вложенного свойства.
    ///    Если ключ <paramref name="key" /> не найден, <paramref name="value" /> содержит значение по умолчанию.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если ключ <paramref name="key" /> найден; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool TryGetValue(TKey key, out TValue value)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      lock (this._lock)
      {
        this.VerifyIntegrity();
        return this.TryGetValueWorker(key, out value);
      }
    }

    /// <summary>Добавляет ключ в таблицу.</summary>
    /// <param name="key">
    ///   Добавляемый ключ.
    ///   <paramref name="key" /> представляет объект, к которому присоединяется свойство.
    /// </param>
    /// <param name="value">Значение свойства ключа.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="key" /> уже существует.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public void Add(TKey key, TValue value)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      lock (this._lock)
      {
        this.VerifyIntegrity();
        this._invalid = true;
        if (this.FindEntry(key) != -1)
        {
          this._invalid = false;
          ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
        }
        this.CreateEntry(key, value);
        this._invalid = false;
      }
    }

    /// <summary>Удаляет ключ и его значение из таблицы.</summary>
    /// <param name="key">Удаляемый ключ.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если ключ найден и удален; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Remove(TKey key)
    {
      if ((object) key == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
      lock (this._lock)
      {
        this.VerifyIntegrity();
        this._invalid = true;
        int num = RuntimeHelpers.GetHashCode((object) key) & int.MaxValue;
        int index1 = num % this._buckets.Length;
        int index2 = -1;
        for (int index3 = this._buckets[index1]; index3 != -1; index3 = this._entries[index3].next)
        {
          if (this._entries[index3].hashCode == num && this._entries[index3].depHnd.GetPrimary() == (object) key)
          {
            if (index2 == -1)
              this._buckets[index1] = this._entries[index3].next;
            else
              this._entries[index2].next = this._entries[index3].next;
            this._entries[index3].depHnd.Free();
            this._entries[index3].next = this._freeList;
            this._freeList = index3;
            this._invalid = false;
            return true;
          }
          index2 = index3;
        }
        this._invalid = false;
        return false;
      }
    }

    /// <summary>
    ///   Единым блоком выполняет поиск указанного ключа в таблице и возвращает соответствующее значение.
    ///    Если ключ в таблице не существует, метод вызывает метод обратного вызова для создания значения, связанного с заданным ключом.
    /// </summary>
    /// <param name="key">
    ///   Ключ, который нужно найти.
    ///   <paramref name="key" /> представляет объект, к которому присоединяется свойство.
    /// </param>
    /// <param name="createValueCallback">
    ///   Делегат метода, который может создать значение для заданного ключа <paramref name="key" />.
    ///    Имеет один параметр типа <paramref name="TKey" /> и возвращает значение типа <paramref name="TValue" />.
    /// </param>
    /// <returns>
    ///   Значение, прикрепляемое к ключу <paramref name="key" />, если ключ <paramref name="key" /> существует в таблице; в противном случае — новое значение, возвращаемое делегатом <paramref name="createValueCallback" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="key" /> или <paramref name="createValueCallback" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public TValue GetValue(TKey key, ConditionalWeakTable<TKey, TValue>.CreateValueCallback createValueCallback)
    {
      if (createValueCallback == null)
        throw new ArgumentNullException(nameof (createValueCallback));
      TValue obj1;
      if (this.TryGetValue(key, out obj1))
        return obj1;
      TValue obj2 = createValueCallback(key);
      lock (this._lock)
      {
        this.VerifyIntegrity();
        this._invalid = true;
        if (this.TryGetValueWorker(key, out obj1))
        {
          this._invalid = false;
          return obj1;
        }
        this.CreateEntry(key, obj2);
        this._invalid = false;
        return obj2;
      }
    }

    /// <summary>
    ///   Единым блоком выполняет поиск указанного ключа в таблице и возвращает соответствующее значение.
    ///    Если ключ в таблице не существует, метод вызывает конструктор класса по умолчанию, представляющего значение таблицы для создания значения, связанного с заданным ключом.
    /// </summary>
    /// <param name="key">
    ///   Ключ, который нужно найти.
    ///   <paramref name="key" /> представляет объект, к которому присоединяется свойство.
    /// </param>
    /// <returns>
    ///   Значение, соответствующее ключу <paramref name="key" />, если ключ <paramref name="key" /> существует в таблице; в противном случае — новое значение, созданное конструктором класса по умолчанию, определенного параметром универсального типа <paramref name="TValue" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.MissingMemberException" />.
    /// 
    ///   Класс, представляющий значение в таблице, не определяет конструктор по умолчанию.
    /// </exception>
    [__DynamicallyInvokable]
    public TValue GetOrCreateValue(TKey key)
    {
      return this.GetValue(key, (ConditionalWeakTable<TKey, TValue>.CreateValueCallback) (k => Activator.CreateInstance<TValue>()));
    }

    [SecuritySafeCritical]
    [FriendAccessAllowed]
    internal TKey FindEquivalentKeyUnsafe(TKey key, out TValue value)
    {
      lock (this._lock)
      {
        for (int index1 = 0; index1 < this._buckets.Length; ++index1)
        {
          for (int index2 = this._buckets[index1]; index2 != -1; index2 = this._entries[index2].next)
          {
            object primary;
            object secondary;
            this._entries[index2].depHnd.GetPrimaryAndSecondary(out primary, out secondary);
            if (object.Equals(primary, (object) key))
            {
              value = (TValue) secondary;
              return (TKey) primary;
            }
          }
        }
      }
      value = default (TValue);
      return default (TKey);
    }

    internal ICollection<TKey> Keys
    {
      [SecuritySafeCritical] get
      {
        List<TKey> keyList = new List<TKey>();
        lock (this._lock)
        {
          for (int index1 = 0; index1 < this._buckets.Length; ++index1)
          {
            for (int index2 = this._buckets[index1]; index2 != -1; index2 = this._entries[index2].next)
            {
              TKey primary = (TKey) this._entries[index2].depHnd.GetPrimary();
              if ((object) primary != null)
                keyList.Add(primary);
            }
          }
        }
        return (ICollection<TKey>) keyList;
      }
    }

    internal ICollection<TValue> Values
    {
      [SecuritySafeCritical] get
      {
        List<TValue> objList = new List<TValue>();
        lock (this._lock)
        {
          for (int index1 = 0; index1 < this._buckets.Length; ++index1)
          {
            for (int index2 = this._buckets[index1]; index2 != -1; index2 = this._entries[index2].next)
            {
              object primary = (object) null;
              object secondary = (object) null;
              this._entries[index2].depHnd.GetPrimaryAndSecondary(out primary, out secondary);
              if (primary != null)
                objList.Add((TValue) secondary);
            }
          }
        }
        return (ICollection<TValue>) objList;
      }
    }

    [SecuritySafeCritical]
    internal void Clear()
    {
      lock (this._lock)
      {
        for (int index = 0; index < this._buckets.Length; ++index)
          this._buckets[index] = -1;
        int index1;
        for (index1 = 0; index1 < this._entries.Length; ++index1)
        {
          if (this._entries[index1].depHnd.IsAllocated)
            this._entries[index1].depHnd.Free();
          this._entries[index1].next = index1 - 1;
        }
        this._freeList = index1 - 1;
      }
    }

    [SecurityCritical]
    private bool TryGetValueWorker(TKey key, out TValue value)
    {
      int entry = this.FindEntry(key);
      if (entry != -1)
      {
        object primary = (object) null;
        object secondary = (object) null;
        this._entries[entry].depHnd.GetPrimaryAndSecondary(out primary, out secondary);
        if (primary != null)
        {
          value = (TValue) secondary;
          return true;
        }
      }
      value = default (TValue);
      return false;
    }

    [SecurityCritical]
    private void CreateEntry(TKey key, TValue value)
    {
      if (this._freeList == -1)
        this.Resize();
      int num = RuntimeHelpers.GetHashCode((object) key) & int.MaxValue;
      int index = num % this._buckets.Length;
      int freeList = this._freeList;
      this._freeList = this._entries[freeList].next;
      this._entries[freeList].hashCode = num;
      this._entries[freeList].depHnd = new DependentHandle((object) key, (object) value);
      this._entries[freeList].next = this._buckets[index];
      this._buckets[index] = freeList;
    }

    [SecurityCritical]
    private void Resize()
    {
      int length = this._buckets.Length;
      bool flag = false;
      for (int index = 0; index < this._entries.Length; ++index)
      {
        if (this._entries[index].depHnd.IsAllocated && this._entries[index].depHnd.GetPrimary() == null)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        length = HashHelpers.GetPrime(this._buckets.Length == 0 ? 6 : this._buckets.Length * 2);
      int num = -1;
      int[] numArray = new int[length];
      for (int index = 0; index < length; ++index)
        numArray[index] = -1;
      ConditionalWeakTable<TKey, TValue>.Entry[] entryArray = new ConditionalWeakTable<TKey, TValue>.Entry[length];
      int index1;
      for (index1 = 0; index1 < this._entries.Length; ++index1)
      {
        DependentHandle depHnd = this._entries[index1].depHnd;
        if (depHnd.IsAllocated && depHnd.GetPrimary() != null)
        {
          int index2 = this._entries[index1].hashCode % length;
          entryArray[index1].depHnd = depHnd;
          entryArray[index1].hashCode = this._entries[index1].hashCode;
          entryArray[index1].next = numArray[index2];
          numArray[index2] = index1;
        }
        else
        {
          this._entries[index1].depHnd.Free();
          entryArray[index1].depHnd = new DependentHandle();
          entryArray[index1].next = num;
          num = index1;
        }
      }
      for (; index1 != entryArray.Length; ++index1)
      {
        entryArray[index1].depHnd = new DependentHandle();
        entryArray[index1].next = num;
        num = index1;
      }
      this._buckets = numArray;
      this._entries = entryArray;
      this._freeList = num;
    }

    [SecurityCritical]
    private int FindEntry(TKey key)
    {
      int num = RuntimeHelpers.GetHashCode((object) key) & int.MaxValue;
      for (int index = this._buckets[num % this._buckets.Length]; index != -1; index = this._entries[index].next)
      {
        if (this._entries[index].hashCode == num && this._entries[index].depHnd.GetPrimary() == (object) key)
          return index;
      }
      return -1;
    }

    private void VerifyIntegrity()
    {
      if (this._invalid)
        throw new InvalidOperationException(Environment.GetResourceString("CollectionCorrupted"));
    }

    /// <summary>
    ///   Обеспечивает освобождение ресурсов и выполнение других завершающих операций, когда сборщик мусора восстанавливает объект <see cref="T:System.Runtime.CompilerServices.ConditionalWeakTable`2" />.
    /// </summary>
    [SecuritySafeCritical]
    ~ConditionalWeakTable()
    {
      if (Environment.HasShutdownStarted || this._lock == null)
        return;
      lock (this._lock)
      {
        if (this._invalid)
          return;
        ConditionalWeakTable<TKey, TValue>.Entry[] entries = this._entries;
        this._invalid = true;
        this._entries = (ConditionalWeakTable<TKey, TValue>.Entry[]) null;
        this._buckets = (int[]) null;
        for (int index = 0; index < entries.Length; ++index)
          entries[index].depHnd.Free();
      }
    }

    /// <summary>
    ///   Представляет метод, который создает значение не по умолчанию для добавления в качестве пары "ключ-значение" в объект <see cref="T:System.Runtime.CompilerServices.ConditionalWeakTable`2" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ, которой принадлежит создаваемому значению.
    /// </param>
    /// <returns>
    ///   Экземпляр ссылочного типа, который представляет значение для добавления к заданному ключу.
    /// </returns>
    [__DynamicallyInvokable]
    public delegate TValue CreateValueCallback(TKey key) where TKey : class where TValue : class;

    private struct Entry
    {
      public DependentHandle depHnd;
      public int hashCode;
      public int next;
    }
  }
}
