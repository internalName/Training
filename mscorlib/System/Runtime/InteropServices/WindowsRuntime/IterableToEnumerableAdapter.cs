// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IterableToEnumerableAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class IterableToEnumerableAdapter
  {
    private IterableToEnumerableAdapter()
    {
    }

    [SecurityCritical]
    internal IEnumerator<T> GetEnumerator_Stub<T>()
    {
      return (IEnumerator<T>) new IteratorToEnumeratorAdapter<T>(JitHelpers.UnsafeCast<IIterable<T>>((object) this).First());
    }

    [SecurityCritical]
    internal IEnumerator<T> GetEnumerator_Variance_Stub<T>() where T : class
    {
      bool fUseString;
      Delegate ambiguousVariantCall = System.StubHelpers.StubHelpers.GetTargetForAmbiguousVariantCall((object) this, typeof (IEnumerable<T>).TypeHandle.Value, out fUseString);
      if ((object) ambiguousVariantCall != null)
        return JitHelpers.UnsafeCast<GetEnumerator_Delegate<T>>((object) ambiguousVariantCall)();
      if (fUseString)
        return JitHelpers.UnsafeCast<IEnumerator<T>>((object) this.GetEnumerator_Stub<string>());
      return this.GetEnumerator_Stub<T>();
    }
  }
}
