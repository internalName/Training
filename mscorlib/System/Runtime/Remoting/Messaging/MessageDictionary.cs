// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MessageDictionary
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal abstract class MessageDictionary : IDictionary, ICollection, IEnumerable
  {
    internal string[] _keys;
    internal IDictionary _dict;

    internal MessageDictionary(string[] keys, IDictionary idict)
    {
      this._keys = keys;
      this._dict = idict;
    }

    internal bool HasUserData()
    {
      return this._dict != null && this._dict.Count > 0;
    }

    internal IDictionary InternalDictionary
    {
      get
      {
        return this._dict;
      }
    }

    internal abstract object GetMessageValue(int i);

    [SecurityCritical]
    internal abstract void SetSpecialKey(int keyNum, object value);

    public virtual bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public virtual bool IsSynchronized
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
        return false;
      }
    }

    public virtual object SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    public virtual bool Contains(object key)
    {
      if (this.ContainsSpecialKey(key))
        return true;
      if (this._dict != null)
        return this._dict.Contains(key);
      return false;
    }

    protected virtual bool ContainsSpecialKey(object key)
    {
      if (!(key is string))
        return false;
      string str = (string) key;
      for (int index = 0; index < this._keys.Length; ++index)
      {
        if (str.Equals(this._keys[index]))
          return true;
      }
      return false;
    }

    public virtual void CopyTo(Array array, int index)
    {
      for (int i = 0; i < this._keys.Length; ++i)
        array.SetValue(this.GetMessageValue(i), index + i);
      if (this._dict == null)
        return;
      this._dict.CopyTo(array, index + this._keys.Length);
    }

    public virtual object this[object key]
    {
      get
      {
        string str = key as string;
        if (str != null)
        {
          for (int i = 0; i < this._keys.Length; ++i)
          {
            if (str.Equals(this._keys[i]))
              return this.GetMessageValue(i);
          }
          if (this._dict != null)
            return this._dict[key];
        }
        return (object) null;
      }
      [SecuritySafeCritical] set
      {
        if (this.ContainsSpecialKey(key))
        {
          if (key.Equals((object) Message.UriKey))
          {
            this.SetSpecialKey(0, value);
          }
          else
          {
            if (!key.Equals((object) Message.CallContextKey))
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKey"));
            this.SetSpecialKey(1, value);
          }
        }
        else
        {
          if (this._dict == null)
            this._dict = (IDictionary) new Hashtable();
          this._dict[key] = value;
        }
      }
    }

    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      return (IDictionaryEnumerator) new MessageDictionaryEnumerator(this, this._dict);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotSupportedException();
    }

    public virtual void Add(object key, object value)
    {
      if (this.ContainsSpecialKey(key))
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKey"));
      if (this._dict == null)
        this._dict = (IDictionary) new Hashtable();
      this._dict.Add(key, value);
    }

    public virtual void Clear()
    {
      if (this._dict == null)
        return;
      this._dict.Clear();
    }

    public virtual void Remove(object key)
    {
      if (this.ContainsSpecialKey(key) || this._dict == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKey"));
      this._dict.Remove(key);
    }

    public virtual ICollection Keys
    {
      get
      {
        int length = this._keys.Length;
        ICollection c = this._dict != null ? this._dict.Keys : (ICollection) null;
        if (c != null)
          length += c.Count;
        ArrayList arrayList = new ArrayList(length);
        for (int index = 0; index < this._keys.Length; ++index)
          arrayList.Add((object) this._keys[index]);
        if (c != null)
          arrayList.AddRange(c);
        return (ICollection) arrayList;
      }
    }

    public virtual ICollection Values
    {
      get
      {
        int length = this._keys.Length;
        ICollection c = this._dict != null ? this._dict.Keys : (ICollection) null;
        if (c != null)
          length += c.Count;
        ArrayList arrayList = new ArrayList(length);
        for (int i = 0; i < this._keys.Length; ++i)
          arrayList.Add(this.GetMessageValue(i));
        if (c != null)
          arrayList.AddRange(c);
        return (ICollection) arrayList;
      }
    }

    public virtual int Count
    {
      get
      {
        if (this._dict != null)
          return this._dict.Count + this._keys.Length;
        return this._keys.Length;
      }
    }
  }
}
