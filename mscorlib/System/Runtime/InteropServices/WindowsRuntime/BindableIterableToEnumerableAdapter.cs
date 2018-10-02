// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.BindableIterableToEnumerableAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class BindableIterableToEnumerableAdapter
  {
    private BindableIterableToEnumerableAdapter()
    {
    }

    [SecurityCritical]
    internal IEnumerator GetEnumerator_Stub()
    {
      return (IEnumerator) new IteratorToEnumeratorAdapter<object>((IIterator<object>) new BindableIterableToEnumerableAdapter.NonGenericToGenericIterator(JitHelpers.UnsafeCast<IBindableIterable>((object) this).First()));
    }

    private sealed class NonGenericToGenericIterator : IIterator<object>
    {
      private IBindableIterator iterator;

      public NonGenericToGenericIterator(IBindableIterator iterator)
      {
        this.iterator = iterator;
      }

      public object Current
      {
        get
        {
          return this.iterator.Current;
        }
      }

      public bool HasCurrent
      {
        get
        {
          return this.iterator.HasCurrent;
        }
      }

      public bool MoveNext()
      {
        return this.iterator.MoveNext();
      }

      public int GetMany(object[] items)
      {
        throw new NotSupportedException();
      }
    }
  }
}
