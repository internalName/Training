// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.EnumerableToBindableIterableAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class EnumerableToBindableIterableAdapter
  {
    private EnumerableToBindableIterableAdapter()
    {
    }

    [SecurityCritical]
    internal IBindableIterator First_Stub()
    {
      return (IBindableIterator) new EnumeratorToIteratorAdapter<object>((IEnumerator<object>) new EnumerableToBindableIterableAdapter.NonGenericToGenericEnumerator(JitHelpers.UnsafeCast<IEnumerable>((object) this).GetEnumerator()));
    }

    internal sealed class NonGenericToGenericEnumerator : IEnumerator<object>, IDisposable, IEnumerator
    {
      private IEnumerator enumerator;

      public NonGenericToGenericEnumerator(IEnumerator enumerator)
      {
        this.enumerator = enumerator;
      }

      public object Current
      {
        get
        {
          return this.enumerator.Current;
        }
      }

      public bool MoveNext()
      {
        return this.enumerator.MoveNext();
      }

      public void Reset()
      {
        this.enumerator.Reset();
      }

      public void Dispose()
      {
      }
    }
  }
}
