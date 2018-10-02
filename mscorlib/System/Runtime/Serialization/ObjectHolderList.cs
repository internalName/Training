// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ObjectHolderList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization
{
  internal class ObjectHolderList
  {
    internal const int DefaultInitialSize = 8;
    internal ObjectHolder[] m_values;
    internal int m_count;

    internal ObjectHolderList()
      : this(8)
    {
    }

    internal ObjectHolderList(int startingSize)
    {
      this.m_count = 0;
      this.m_values = new ObjectHolder[startingSize];
    }

    internal virtual void Add(ObjectHolder value)
    {
      if (this.m_count == this.m_values.Length)
        this.EnlargeArray();
      this.m_values[this.m_count++] = value;
    }

    internal ObjectHolderListEnumerator GetFixupEnumerator()
    {
      return new ObjectHolderListEnumerator(this, true);
    }

    private void EnlargeArray()
    {
      int length = this.m_values.Length * 2;
      if (length < 0)
      {
        if (length == int.MaxValue)
          throw new SerializationException(Environment.GetResourceString("Serialization_TooManyElements"));
        length = int.MaxValue;
      }
      ObjectHolder[] objectHolderArray = new ObjectHolder[length];
      Array.Copy((Array) this.m_values, (Array) objectHolderArray, this.m_count);
      this.m_values = objectHolderArray;
    }

    internal int Version
    {
      get
      {
        return this.m_count;
      }
    }

    internal int Count
    {
      get
      {
        return this.m_count;
      }
    }
  }
}
