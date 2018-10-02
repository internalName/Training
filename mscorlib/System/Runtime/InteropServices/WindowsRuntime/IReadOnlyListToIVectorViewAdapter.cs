// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IReadOnlyListToIVectorViewAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [DebuggerDisplay("Size = {Size}")]
  internal sealed class IReadOnlyListToIVectorViewAdapter
  {
    private IReadOnlyListToIVectorViewAdapter()
    {
    }

    [SecurityCritical]
    internal T GetAt<T>(uint index)
    {
      IReadOnlyList<T> objList = JitHelpers.UnsafeCast<IReadOnlyList<T>>((object) this);
      IReadOnlyListToIVectorViewAdapter.EnsureIndexInt32(index, objList.Count);
      try
      {
        return objList[(int) index];
      }
      catch (ArgumentOutOfRangeException ex)
      {
        ex.SetErrorCode(-2147483637);
        throw;
      }
    }

    [SecurityCritical]
    internal uint Size<T>()
    {
      return (uint) JitHelpers.UnsafeCast<IReadOnlyList<T>>((object) this).Count;
    }

    [SecurityCritical]
    internal bool IndexOf<T>(T value, out uint index)
    {
      IReadOnlyList<T> objList = JitHelpers.UnsafeCast<IReadOnlyList<T>>((object) this);
      int num = -1;
      int count = objList.Count;
      for (int index1 = 0; index1 < count; ++index1)
      {
        if (EqualityComparer<T>.Default.Equals(value, objList[index1]))
        {
          num = index1;
          break;
        }
      }
      if (-1 == num)
      {
        index = 0U;
        return false;
      }
      index = (uint) num;
      return true;
    }

    [SecurityCritical]
    internal uint GetMany<T>(uint startIndex, T[] items)
    {
      IReadOnlyList<T> objList = JitHelpers.UnsafeCast<IReadOnlyList<T>>((object) this);
      if ((long) startIndex == (long) objList.Count)
        return 0;
      IReadOnlyListToIVectorViewAdapter.EnsureIndexInt32(startIndex, objList.Count);
      if (items == null)
        return 0;
      uint num = Math.Min((uint) items.Length, (uint) objList.Count - startIndex);
      for (uint index = 0; index < num; ++index)
        items[(int) index] = objList[(int) index + (int) startIndex];
      if (typeof (T) == typeof (string))
      {
        string[] strArray = items as string[];
        for (uint index = num; (long) index < (long) items.Length; ++index)
          strArray[(int) index] = string.Empty;
      }
      return num;
    }

    private static void EnsureIndexInt32(uint index, int listCapacity)
    {
      if ((uint) int.MaxValue <= index || index >= (uint) listCapacity)
      {
        Exception exception = (Exception) new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_IndexLargerThanMaxValue"));
        exception.SetErrorCode(-2147483637);
        throw exception;
      }
    }
  }
}
