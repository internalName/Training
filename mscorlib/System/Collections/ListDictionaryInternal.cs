// Decompiled with JetBrains decompiler
// Type: System.Collections.ListDictionaryInternal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Threading;

namespace System.Collections
{
  [Serializable]
  internal class ListDictionaryInternal : IDictionary, ICollection, IEnumerable
  {
    private ListDictionaryInternal.DictionaryNode head;
    private int version;
    private int count;
    [NonSerialized]
    private object _syncRoot;

    public object this[object key]
    {
      get
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
        for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
        {
          if (dictionaryNode.key.Equals(key))
            return dictionaryNode.value;
        }
        return (object) null;
      }
      set
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
        if (!key.GetType().IsSerializable)
          throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), nameof (key));
        if (value != null && !value.GetType().IsSerializable)
          throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), nameof (value));
        ++this.version;
        ListDictionaryInternal.DictionaryNode dictionaryNode1 = (ListDictionaryInternal.DictionaryNode) null;
        ListDictionaryInternal.DictionaryNode dictionaryNode2;
        for (dictionaryNode2 = this.head; dictionaryNode2 != null && !dictionaryNode2.key.Equals(key); dictionaryNode2 = dictionaryNode2.next)
          dictionaryNode1 = dictionaryNode2;
        if (dictionaryNode2 != null)
        {
          dictionaryNode2.value = value;
        }
        else
        {
          ListDictionaryInternal.DictionaryNode dictionaryNode3 = new ListDictionaryInternal.DictionaryNode();
          dictionaryNode3.key = key;
          dictionaryNode3.value = value;
          if (dictionaryNode1 != null)
            dictionaryNode1.next = dictionaryNode3;
          else
            this.head = dictionaryNode3;
          ++this.count;
        }
      }
    }

    public int Count
    {
      get
      {
        return this.count;
      }
    }

    public ICollection Keys
    {
      get
      {
        return (ICollection) new ListDictionaryInternal.NodeKeyValueCollection(this, true);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool IsFixedSize
    {
      get
      {
        return false;
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    public object SyncRoot
    {
      get
      {
        if (this._syncRoot == null)
          Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    public ICollection Values
    {
      get
      {
        return (ICollection) new ListDictionaryInternal.NodeKeyValueCollection(this, false);
      }
    }

    public void Add(object key, object value)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
      if (!key.GetType().IsSerializable)
        throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), nameof (key));
      if (value != null && !value.GetType().IsSerializable)
        throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), nameof (value));
      ++this.version;
      ListDictionaryInternal.DictionaryNode dictionaryNode1 = (ListDictionaryInternal.DictionaryNode) null;
      ListDictionaryInternal.DictionaryNode dictionaryNode2;
      for (dictionaryNode2 = this.head; dictionaryNode2 != null; dictionaryNode2 = dictionaryNode2.next)
      {
        if (dictionaryNode2.key.Equals(key))
          throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", dictionaryNode2.key, key));
        dictionaryNode1 = dictionaryNode2;
      }
      if (dictionaryNode2 != null)
      {
        dictionaryNode2.value = value;
      }
      else
      {
        ListDictionaryInternal.DictionaryNode dictionaryNode3 = new ListDictionaryInternal.DictionaryNode();
        dictionaryNode3.key = key;
        dictionaryNode3.value = value;
        if (dictionaryNode1 != null)
          dictionaryNode1.next = dictionaryNode3;
        else
          this.head = dictionaryNode3;
        ++this.count;
      }
    }

    public void Clear()
    {
      this.count = 0;
      this.head = (ListDictionaryInternal.DictionaryNode) null;
      ++this.version;
    }

    public bool Contains(object key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
      for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
      {
        if (dictionaryNode.key.Equals(key))
          return true;
      }
      return false;
    }

    public void CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Length - index < this.Count)
        throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Index"), nameof (index));
      for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
      {
        array.SetValue((object) new DictionaryEntry(dictionaryNode.key, dictionaryNode.value), index);
        ++index;
      }
    }

    public IDictionaryEnumerator GetEnumerator()
    {
      return (IDictionaryEnumerator) new ListDictionaryInternal.NodeEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new ListDictionaryInternal.NodeEnumerator(this);
    }

    public void Remove(object key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key), Environment.GetResourceString("ArgumentNull_Key"));
      ++this.version;
      ListDictionaryInternal.DictionaryNode dictionaryNode1 = (ListDictionaryInternal.DictionaryNode) null;
      ListDictionaryInternal.DictionaryNode dictionaryNode2;
      for (dictionaryNode2 = this.head; dictionaryNode2 != null && !dictionaryNode2.key.Equals(key); dictionaryNode2 = dictionaryNode2.next)
        dictionaryNode1 = dictionaryNode2;
      if (dictionaryNode2 == null)
        return;
      if (dictionaryNode2 == this.head)
        this.head = dictionaryNode2.next;
      else
        dictionaryNode1.next = dictionaryNode2.next;
      --this.count;
    }

    private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
    {
      private ListDictionaryInternal list;
      private ListDictionaryInternal.DictionaryNode current;
      private int version;
      private bool start;

      public NodeEnumerator(ListDictionaryInternal list)
      {
        this.list = list;
        this.version = list.version;
        this.start = true;
        this.current = (ListDictionaryInternal.DictionaryNode) null;
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
          if (this.current == null)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return new DictionaryEntry(this.current.key, this.current.value);
        }
      }

      public object Key
      {
        get
        {
          if (this.current == null)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return this.current.key;
        }
      }

      public object Value
      {
        get
        {
          if (this.current == null)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
          return this.current.value;
        }
      }

      public bool MoveNext()
      {
        if (this.version != this.list.version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this.start)
        {
          this.current = this.list.head;
          this.start = false;
        }
        else if (this.current != null)
          this.current = this.current.next;
        return this.current != null;
      }

      public void Reset()
      {
        if (this.version != this.list.version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this.start = true;
        this.current = (ListDictionaryInternal.DictionaryNode) null;
      }
    }

    private class NodeKeyValueCollection : ICollection, IEnumerable
    {
      private ListDictionaryInternal list;
      private bool isKeys;

      public NodeKeyValueCollection(ListDictionaryInternal list, bool isKeys)
      {
        this.list = list;
        this.isKeys = isKeys;
      }

      void ICollection.CopyTo(Array array, int index)
      {
        if (array == null)
          throw new ArgumentNullException(nameof (array));
        if (array.Rank != 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
        if (index < 0)
          throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        if (array.Length - index < this.list.Count)
          throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Index"), nameof (index));
        for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
        {
          array.SetValue(this.isKeys ? dictionaryNode.key : dictionaryNode.value, index);
          ++index;
        }
      }

      int ICollection.Count
      {
        get
        {
          int num = 0;
          for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
            ++num;
          return num;
        }
      }

      bool ICollection.IsSynchronized
      {
        get
        {
          return false;
        }
      }

      object ICollection.SyncRoot
      {
        get
        {
          return this.list.SyncRoot;
        }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) new ListDictionaryInternal.NodeKeyValueCollection.NodeKeyValueEnumerator(this.list, this.isKeys);
      }

      private class NodeKeyValueEnumerator : IEnumerator
      {
        private ListDictionaryInternal list;
        private ListDictionaryInternal.DictionaryNode current;
        private int version;
        private bool isKeys;
        private bool start;

        public NodeKeyValueEnumerator(ListDictionaryInternal list, bool isKeys)
        {
          this.list = list;
          this.isKeys = isKeys;
          this.version = list.version;
          this.start = true;
          this.current = (ListDictionaryInternal.DictionaryNode) null;
        }

        public object Current
        {
          get
          {
            if (this.current == null)
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
            if (!this.isKeys)
              return this.current.value;
            return this.current.key;
          }
        }

        public bool MoveNext()
        {
          if (this.version != this.list.version)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
          if (this.start)
          {
            this.current = this.list.head;
            this.start = false;
          }
          else if (this.current != null)
            this.current = this.current.next;
          return this.current != null;
        }

        public void Reset()
        {
          if (this.version != this.list.version)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
          this.start = true;
          this.current = (ListDictionaryInternal.DictionaryNode) null;
        }
      }
    }

    [Serializable]
    private class DictionaryNode
    {
      public object key;
      public object value;
      public ListDictionaryInternal.DictionaryNode next;
    }
  }
}
