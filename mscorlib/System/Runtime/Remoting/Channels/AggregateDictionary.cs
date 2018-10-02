// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.AggregateDictionary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Runtime.Remoting.Channels
{
  internal class AggregateDictionary : IDictionary, ICollection, IEnumerable
  {
    private ICollection _dictionaries;

    public AggregateDictionary(ICollection dictionaries)
    {
      this._dictionaries = dictionaries;
    }

    public virtual object this[object key]
    {
      get
      {
        foreach (IDictionary dictionary in (IEnumerable) this._dictionaries)
        {
          if (dictionary.Contains(key))
            return dictionary[key];
        }
        return (object) null;
      }
      set
      {
        foreach (IDictionary dictionary in (IEnumerable) this._dictionaries)
        {
          if (dictionary.Contains(key))
            dictionary[key] = value;
        }
      }
    }

    public virtual ICollection Keys
    {
      get
      {
        ArrayList arrayList = new ArrayList();
        foreach (IDictionary dictionary in (IEnumerable) this._dictionaries)
        {
          ICollection keys = dictionary.Keys;
          if (keys != null)
          {
            foreach (object obj in (IEnumerable) keys)
              arrayList.Add(obj);
          }
        }
        return (ICollection) arrayList;
      }
    }

    public virtual ICollection Values
    {
      get
      {
        ArrayList arrayList = new ArrayList();
        foreach (IDictionary dictionary in (IEnumerable) this._dictionaries)
        {
          ICollection values = dictionary.Values;
          if (values != null)
          {
            foreach (object obj in (IEnumerable) values)
              arrayList.Add(obj);
          }
        }
        return (ICollection) arrayList;
      }
    }

    public virtual bool Contains(object key)
    {
      foreach (IDictionary dictionary in (IEnumerable) this._dictionaries)
      {
        if (dictionary.Contains(key))
          return true;
      }
      return false;
    }

    public virtual bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public virtual bool IsFixedSize
    {
      get
      {
        return true;
      }
    }

    public virtual void Add(object key, object value)
    {
      throw new NotSupportedException();
    }

    public virtual void Clear()
    {
      throw new NotSupportedException();
    }

    public virtual void Remove(object key)
    {
      throw new NotSupportedException();
    }

    public virtual IDictionaryEnumerator GetEnumerator()
    {
      return (IDictionaryEnumerator) new DictionaryEnumeratorByKeys((IDictionary) this);
    }

    public virtual void CopyTo(Array array, int index)
    {
      throw new NotSupportedException();
    }

    public virtual int Count
    {
      get
      {
        int num = 0;
        foreach (IDictionary dictionary in (IEnumerable) this._dictionaries)
          num += dictionary.Count;
        return num;
      }
    }

    public virtual object SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    public virtual bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new DictionaryEnumeratorByKeys((IDictionary) this);
    }
  }
}
