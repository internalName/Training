// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IteratorToEnumeratorAdapter`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class IteratorToEnumeratorAdapter<T> : IEnumerator<T>, IDisposable, IEnumerator
  {
    private IIterator<T> m_iterator;
    private bool m_hadCurrent;
    private T m_current;
    private bool m_isInitialized;

    internal IteratorToEnumeratorAdapter(IIterator<T> iterator)
    {
      this.m_iterator = iterator;
      this.m_hadCurrent = true;
      this.m_isInitialized = false;
    }

    public T Current
    {
      get
      {
        if (!this.m_isInitialized)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumNotStarted);
        if (!this.m_hadCurrent)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumEnded);
        return this.m_current;
      }
    }

    object IEnumerator.Current
    {
      get
      {
        if (!this.m_isInitialized)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumNotStarted);
        if (!this.m_hadCurrent)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumEnded);
        return (object) this.m_current;
      }
    }

    [SecuritySafeCritical]
    public bool MoveNext()
    {
      if (!this.m_hadCurrent)
        return false;
      try
      {
        if (!this.m_isInitialized)
        {
          this.m_hadCurrent = this.m_iterator.HasCurrent;
          this.m_isInitialized = true;
        }
        else
          this.m_hadCurrent = this.m_iterator.MoveNext();
        if (this.m_hadCurrent)
          this.m_current = this.m_iterator.Current;
      }
      catch (Exception ex)
      {
        if (Marshal.GetHRForException(ex) == -2147483636)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
        else
          throw;
      }
      return this.m_hadCurrent;
    }

    public void Reset()
    {
      throw new NotSupportedException();
    }

    public void Dispose()
    {
    }
  }
}
