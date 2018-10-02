// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.EnumeratorToIteratorAdapter`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class EnumeratorToIteratorAdapter<T> : IIterator<T>, IBindableIterator
  {
    private bool m_firstItem = true;
    private IEnumerator<T> m_enumerator;
    private bool m_hasCurrent;

    internal EnumeratorToIteratorAdapter(IEnumerator<T> enumerator)
    {
      this.m_enumerator = enumerator;
    }

    public T Current
    {
      get
      {
        if (this.m_firstItem)
        {
          this.m_firstItem = false;
          this.MoveNext();
        }
        if (!this.m_hasCurrent)
          throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, (Exception) null);
        return this.m_enumerator.Current;
      }
    }

    object IBindableIterator.Current
    {
      get
      {
        return (object) this.Current;
      }
    }

    public bool HasCurrent
    {
      get
      {
        if (this.m_firstItem)
        {
          this.m_firstItem = false;
          this.MoveNext();
        }
        return this.m_hasCurrent;
      }
    }

    public bool MoveNext()
    {
      try
      {
        this.m_hasCurrent = this.m_enumerator.MoveNext();
      }
      catch (InvalidOperationException ex)
      {
        throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483636, (Exception) ex);
      }
      return this.m_hasCurrent;
    }

    public int GetMany(T[] items)
    {
      if (items == null)
        return 0;
      int index1;
      for (index1 = 0; index1 < items.Length && this.HasCurrent; ++index1)
      {
        items[index1] = this.Current;
        this.MoveNext();
      }
      if (typeof (T) == typeof (string))
      {
        string[] strArray = items as string[];
        for (int index2 = index1; index2 < items.Length; ++index2)
          strArray[index2] = string.Empty;
      }
      return index1;
    }
  }
}
