// Decompiled with JetBrains decompiler
// Type: System.Collections.ReadOnlyCollectionBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Предоставляет базовый класс <see langword="abstract" /> для строго типизированной неуниверсальной коллекции только для чтения.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public abstract class ReadOnlyCollectionBase : ICollection, IEnumerable
  {
    private ArrayList list;

    /// <summary>
    ///   Возвращает список элементов, содержащихся в <see cref="T:System.Collections.ReadOnlyCollectionBase" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.ArrayList" /> Представляет <see cref="T:System.Collections.ReadOnlyCollectionBase" /> экземпляр.
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
    ///   Возвращает число элементов, содержащихся в <see cref="T:System.Collections.ReadOnlyCollectionBase" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в <see cref="T:System.Collections.ReadOnlyCollectionBase" /> экземпляра.
    /// 
    ///   Получение значения данного свойства является операцией порядка сложности O(1).
    /// </returns>
    public virtual int Count
    {
      get
      {
        return this.InnerList.Count;
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

    /// <summary>
    ///   Возвращает перечислитель, выполняющий перебор элементов <see cref="T:System.Collections.ReadOnlyCollectionBase" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   Перечислитель <see cref="T:System.Collections.IEnumerator" /> для экземпляра класса <see cref="T:System.Collections.ReadOnlyCollectionBase" />.
    /// </returns>
    public virtual IEnumerator GetEnumerator()
    {
      return this.InnerList.GetEnumerator();
    }
  }
}
