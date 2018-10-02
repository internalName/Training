// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Предоставляет базовую реализацию объекта канала, который предоставляет интерфейс словаря к его свойствам.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public abstract class BaseChannelObjectWithProperties : IDictionary, ICollection, IEnumerable
  {
    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IDictionary" /> свойств канала, связанных с объектом канала.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Collections.IDictionary" /> свойств канала, связанных с объектом канала.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public virtual IDictionary Properties
    {
      [SecurityCritical] get
      {
        return (IDictionary) this;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе Возвращает или задает свойство, связанное с указанным ключом.
    /// </summary>
    /// <param name="key">
    ///   Ключ свойства, чтобы получить или задать.
    /// </param>
    /// <returns>Свойство, связанное с указанным ключом.</returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Обращение к свойству.
    /// </exception>
    public virtual object this[object key]
    {
      [SecuritySafeCritical] get
      {
        return (object) null;
      }
      [SecuritySafeCritical] set
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает <see cref="T:System.Collections.ICollection" /> ключей, связанных с помощью свойств объекта канала.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Collections.ICollection" /> ключей, связанных с помощью свойств объекта канала.
    /// </returns>
    public virtual ICollection Keys
    {
      [SecuritySafeCritical] get
      {
        return (ICollection) null;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.ICollection" /> значений свойств, связанных с объектом канала.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Collections.ICollection" /> значений свойств, связанных с объектом канала.
    /// </returns>
    public virtual ICollection Values
    {
      [SecuritySafeCritical] get
      {
        ICollection keys = this.Keys;
        if (keys == null)
          return (ICollection) null;
        ArrayList arrayList = new ArrayList();
        foreach (object index in (IEnumerable) keys)
          arrayList.Add(this[index]);
        return (ICollection) arrayList;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, содержит ли объект канала свойство, связанное с указанным ключом.
    /// </summary>
    /// <param name="key">Ключ свойства для поиска.</param>
    /// <returns>
    ///   <see langword="true" /> Если объект канала содержит свойство, связанное с указанным ключом; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    public virtual bool Contains(object key)
    {
      if (key == null)
        return false;
      ICollection keys = this.Keys;
      if (keys == null)
        return false;
      string strA = key as string;
      foreach (object obj in (IEnumerable) keys)
      {
        if (strA != null)
        {
          string strB = obj as string;
          if (strB != null)
          {
            if (string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) == 0)
              return true;
            continue;
          }
        }
        if (key.Equals(obj))
          return true;
      }
      return false;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли коллекция свойств в объекте канала только для чтения.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если коллекция свойств в объекте канала доступна только для чтения; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsReadOnly
    {
      [SecuritySafeCritical] get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, фиксировано ли число свойств, которые могут быть введены в объект канала.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если число свойств, которые могут быть введены в объект канала является постоянной величиной; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsFixedSize
    {
      [SecuritySafeCritical] get
      {
        return true;
      }
    }

    /// <summary>
    ///   Создает исключение <see cref="T:System.NotSupportedException" />.
    /// </summary>
    /// <param name="key">
    ///   Ключ, который связан с объектом в <paramref name="value" /> параметр.
    /// </param>
    /// <param name="value">Значение для сложения.</param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод вызван.
    /// </exception>
    [SecuritySafeCritical]
    public virtual void Add(object key, object value)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Создает исключение <see cref="T:System.NotSupportedException" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод вызван.
    /// </exception>
    [SecuritySafeCritical]
    public virtual void Clear()
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Создает исключение <see cref="T:System.NotSupportedException" />.
    /// </summary>
    /// <param name="key">Ключ удаляемого объекта.</param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод вызван.
    /// </exception>
    [SecuritySafeCritical]
    public virtual void Remove(object key)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IDictionaryEnumerator" /> перечисляет все свойства, связанные с объектом канала.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Collections.IDictionaryEnumerator" /> перечисляет все свойства, связанные с объектом канала.
    /// </returns>
    [SecuritySafeCritical]
    public virtual IDictionaryEnumerator GetEnumerator()
    {
      return (IDictionaryEnumerator) new DictionaryEnumeratorByKeys((IDictionary) this);
    }

    /// <summary>
    ///   Создает исключение <see cref="T:System.NotSupportedException" />.
    /// </summary>
    /// <param name="array">Массив для копирования свойств.</param>
    /// <param name="index">
    ///   Индекс, с которого начинается копирование.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод вызван.
    /// </exception>
    [SecuritySafeCritical]
    public virtual void CopyTo(Array array, int index)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Возвращает число свойств, связанных с объектом канала.
    /// </summary>
    /// <returns>Число свойств, связанных с объектом канала.</returns>
    public virtual int Count
    {
      [SecuritySafeCritical] get
      {
        ICollection keys = this.Keys;
        if (keys == null)
          return 0;
        return keys.Count;
      }
    }

    /// <summary>
    ///   Возвращает объект, который используется для синхронизации доступа к <see cref="T:System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties" />.
    /// </summary>
    /// <returns>
    ///   Объект, который используется для синхронизации доступа к <see cref="T:System.Runtime.Remoting.Channels.BaseChannelObjectWithProperties" />.
    /// </returns>
    public virtual object SyncRoot
    {
      [SecuritySafeCritical] get
      {
        return (object) this;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, синхронизирован ли словарь свойств объекта канала.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если словарь свойств объекта канала синхронизирован; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsSynchronized
    {
      [SecuritySafeCritical] get
      {
        return false;
      }
    }

    [SecuritySafeCritical]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new DictionaryEnumeratorByKeys((IDictionary) this);
    }
  }
}
