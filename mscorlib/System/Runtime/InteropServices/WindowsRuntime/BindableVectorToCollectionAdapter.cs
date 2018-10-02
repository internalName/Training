// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.BindableVectorToCollectionAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class BindableVectorToCollectionAdapter
  {
    private BindableVectorToCollectionAdapter()
    {
    }

    [SecurityCritical]
    internal int Count()
    {
      uint size = JitHelpers.UnsafeCast<IBindableVector>((object) this).Size;
      if ((uint) int.MaxValue < size)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
      return (int) size;
    }

    [SecurityCritical]
    internal bool IsSynchronized()
    {
      return false;
    }

    [SecurityCritical]
    internal object SyncRoot()
    {
      return (object) this;
    }

    [SecurityCritical]
    internal void CopyTo(Array array, int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      int lowerBound = array.GetLowerBound(0);
      int num = this.Count();
      int length = array.GetLength(0);
      if (arrayIndex < lowerBound)
        throw new ArgumentOutOfRangeException(nameof (arrayIndex));
      if (num > length - (arrayIndex - lowerBound))
        throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
      if (arrayIndex - lowerBound > length)
        throw new ArgumentException(Environment.GetResourceString("Argument_IndexOutOfArrayBounds"));
      IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>((object) this);
      for (uint index = 0; (long) index < (long) num; ++index)
        array.SetValue(bindableVector.GetAt(index), (long) index + (long) arrayIndex);
    }
  }
}
