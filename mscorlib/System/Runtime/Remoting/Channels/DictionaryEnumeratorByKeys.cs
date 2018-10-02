// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.DictionaryEnumeratorByKeys
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Runtime.Remoting.Channels
{
  internal class DictionaryEnumeratorByKeys : IDictionaryEnumerator, IEnumerator
  {
    private IDictionary _properties;
    private IEnumerator _keyEnum;

    public DictionaryEnumeratorByKeys(IDictionary properties)
    {
      this._properties = properties;
      this._keyEnum = properties.Keys.GetEnumerator();
    }

    public bool MoveNext()
    {
      return this._keyEnum.MoveNext();
    }

    public void Reset()
    {
      this._keyEnum.Reset();
    }

    public object Current
    {
      get
      {
        return (object) this.Entry;
      }
    }

    public DictionaryEntry Entry
    {
      get
      {
        return new DictionaryEntry(this.Key, this.Value);
      }
    }

    public object Key
    {
      get
      {
        return this._keyEnum.Current;
      }
    }

    public object Value
    {
      get
      {
        return this._properties[this.Key];
      }
    }
  }
}
