// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IVectorViewToIReadOnlyListAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [DebuggerDisplay("Count = {Count}")]
  internal sealed class IVectorViewToIReadOnlyListAdapter
  {
    private IVectorViewToIReadOnlyListAdapter()
    {
    }

    [SecurityCritical]
    internal T Indexer_Get<T>(int index)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      IVectorView<T> vectorView = JitHelpers.UnsafeCast<IVectorView<T>>((object) this);
      try
      {
        return vectorView.GetAt((uint) index);
      }
      catch (Exception ex)
      {
        if (-2147483637 == ex._HResult)
          throw new ArgumentOutOfRangeException(nameof (index));
        throw;
      }
    }

    [SecurityCritical]
    internal T Indexer_Get_Variance<T>(int index) where T : class
    {
      bool fUseString;
      Delegate ambiguousVariantCall = System.StubHelpers.StubHelpers.GetTargetForAmbiguousVariantCall((object) this, typeof (IReadOnlyList<T>).TypeHandle.Value, out fUseString);
      if ((object) ambiguousVariantCall != null)
        return JitHelpers.UnsafeCast<Indexer_Get_Delegate<T>>((object) ambiguousVariantCall)(index);
      if (fUseString)
        return JitHelpers.UnsafeCast<T>((object) this.Indexer_Get<string>(index));
      return this.Indexer_Get<T>(index);
    }
  }
}
