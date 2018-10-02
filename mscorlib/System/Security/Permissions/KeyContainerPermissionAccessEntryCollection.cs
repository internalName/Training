// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.KeyContainerPermissionAccessEntryCollection
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Представляет коллекцию объектов <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" />.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class KeyContainerPermissionAccessEntryCollection : ICollection, IEnumerable
  {
    private ArrayList m_list;
    private KeyContainerPermissionFlags m_globalFlags;

    private KeyContainerPermissionAccessEntryCollection()
    {
    }

    internal KeyContainerPermissionAccessEntryCollection(KeyContainerPermissionFlags globalFlags)
    {
      this.m_list = new ArrayList();
      this.m_globalFlags = globalFlags;
    }

    /// <summary>
    ///   Возвращает элемент по указанному индексу в коллекции.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс доступа к элементу.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> Объект по указанному индексу в коллекции.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> — больше или равно количеству коллекций.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="index" /> является отрицательным числом.
    /// </exception>
    public KeyContainerPermissionAccessEntry this[int index]
    {
      get
      {
        if (index < 0)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
        if (index >= this.Count)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
        return (KeyContainerPermissionAccessEntry) this.m_list[index];
      }
    }

    /// <summary>Возвращает количество элементов в коллекции.</summary>
    /// <returns>
    ///   Количество <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> объектов в коллекции.
    /// </returns>
    public int Count
    {
      get
      {
        return this.m_list.Count;
      }
    }

    /// <summary>
    ///   Добавляет объект <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> в коллекцию.
    /// </summary>
    /// <param name="accessEntry">
    ///   Добавляемый объект <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" />.
    /// </param>
    /// <returns>Индекс, в которой был вставлен новый элемент.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="accessEntry" /> имеет значение <see langword="null" />.
    /// </exception>
    public int Add(KeyContainerPermissionAccessEntry accessEntry)
    {
      if (accessEntry == null)
        throw new ArgumentNullException(nameof (accessEntry));
      int index = this.m_list.IndexOf((object) accessEntry);
      if (index == -1)
      {
        if (accessEntry.Flags != this.m_globalFlags)
          return this.m_list.Add((object) accessEntry);
        return -1;
      }
      ((KeyContainerPermissionAccessEntry) this.m_list[index]).Flags &= accessEntry.Flags;
      return index;
    }

    /// <summary>
    ///   Удаляет все <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> объекты из коллекции.
    /// </summary>
    public void Clear()
    {
      this.m_list.Clear();
    }

    /// <summary>
    ///   Возвращает индекс в коллекции указанного <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> объекта, если он существует в коллекции.
    /// </summary>
    /// <param name="accessEntry">
    ///   <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> Искомого объекта.
    /// </param>
    /// <returns>
    ///   Индекс указанного <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> объекта в коллекции или -1, если совпадение не найдено.
    /// </returns>
    public int IndexOf(KeyContainerPermissionAccessEntry accessEntry)
    {
      return this.m_list.IndexOf((object) accessEntry);
    }

    /// <summary>
    ///   Удаляет указанный <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> объекта из thecollection.
    /// </summary>
    /// <param name="accessEntry">
    ///   Удаляемый объект <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="accessEntry" /> имеет значение <see langword="null" />.
    /// </exception>
    public void Remove(KeyContainerPermissionAccessEntry accessEntry)
    {
      if (accessEntry == null)
        throw new ArgumentNullException(nameof (accessEntry));
      this.m_list.Remove((object) accessEntry);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> объект, который можно использовать для прохода по объектам в коллекции.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> объект, который может использоваться для итерации по коллекции.
    /// </returns>
    public KeyContainerPermissionAccessEntryEnumerator GetEnumerator()
    {
      return new KeyContainerPermissionAccessEntryEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new KeyContainerPermissionAccessEntryEnumerator(this);
    }

    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (index < 0 || index >= array.Length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (index + this.Count > array.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        array.SetValue((object) this[index1], index);
        ++index;
      }
    }

    /// <summary>
    ///   Копирует элементы коллекции в совместимый одномерный массив, начиная с указанного индекса целевого массива.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> массив, в который копируются элементы, скопированные из текущей коллекции.
    /// </param>
    /// <param name="index">
    ///   Индекс в <paramref name="array" /> с которого начинается копирование.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> меньше нижней границы массива <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="array" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Число элементов в коллекции больше, чем свободное пространство от <paramref name="index" /> до конца массива назначения <paramref name="array" />.
    /// </exception>
    public void CopyTo(KeyContainerPermissionAccessEntry[] array, int index)
    {
      ((ICollection) this).CopyTo((Array) array, index);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли доступ к коллекции синхронизированным (потокобезопасным).
    /// </summary>
    /// <returns>
    ///   Значение <see langword="false" /> во всех случаях.
    /// </returns>
    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает объект, который можно использовать для синхронизации доступа к коллекции.
    /// </summary>
    /// <returns>
    ///   Объект, который можно использовать для синхронизации доступа к коллекции.
    /// </returns>
    public object SyncRoot
    {
      get
      {
        return (object) this;
      }
    }
  }
}
