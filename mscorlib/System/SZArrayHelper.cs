// Decompiled with JetBrains decompiler
// Type: System.SZArrayHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
  internal sealed class SZArrayHelper
  {
    private SZArrayHelper()
    {
    }

    [SecuritySafeCritical]
    internal IEnumerator<T> GetEnumerator<T>()
    {
      T[] array = JitHelpers.UnsafeCast<T[]>((object) this);
      int length = array.Length;
      if (length != 0)
        return (IEnumerator<T>) new SZArrayHelper.SZGenericArrayEnumerator<T>(array, length);
      return (IEnumerator<T>) SZArrayHelper.SZGenericArrayEnumerator<T>.Empty;
    }

    [SecuritySafeCritical]
    private void CopyTo<T>(T[] array, int index)
    {
      if (array != null && array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
      T[] objArray = JitHelpers.UnsafeCast<T[]>((object) this);
      Array.Copy((Array) objArray, 0, (Array) array, index, objArray.Length);
    }

    [SecuritySafeCritical]
    internal int get_Count<T>()
    {
      return JitHelpers.UnsafeCast<T[]>((object) this).Length;
    }

    [SecuritySafeCritical]
    internal T get_Item<T>(int index)
    {
      T[] objArray = JitHelpers.UnsafeCast<T[]>((object) this);
      if ((uint) index >= (uint) objArray.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException();
      return objArray[index];
    }

    [SecuritySafeCritical]
    internal void set_Item<T>(int index, T value)
    {
      T[] objArray = JitHelpers.UnsafeCast<T[]>((object) this);
      if ((uint) index >= (uint) objArray.Length)
        ThrowHelper.ThrowArgumentOutOfRangeException();
      objArray[index] = value;
    }

    private void Add<T>(T value)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
    }

    [SecuritySafeCritical]
    private bool Contains<T>(T value)
    {
      return Array.IndexOf<T>(JitHelpers.UnsafeCast<T[]>((object) this), value) != -1;
    }

    private bool get_IsReadOnly<T>()
    {
      return true;
    }

    private void Clear<T>()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
    }

    [SecuritySafeCritical]
    private int IndexOf<T>(T value)
    {
      return Array.IndexOf<T>(JitHelpers.UnsafeCast<T[]>((object) this), value);
    }

    private void Insert<T>(int index, T value)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
    }

    private bool Remove<T>(T value)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
    }

    private void RemoveAt<T>(int index)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
    }

    [Serializable]
    private sealed class SZGenericArrayEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
    {
      internal static readonly SZArrayHelper.SZGenericArrayEnumerator<T> Empty = new SZArrayHelper.SZGenericArrayEnumerator<T>((T[]) null, -1);
      private T[] _array;
      private int _index;
      private int _endIndex;

      internal SZGenericArrayEnumerator(T[] array, int endIndex)
      {
        this._array = array;
        this._index = -1;
        this._endIndex = endIndex;
      }

      public bool MoveNext()
      {
        if (this._index >= this._endIndex)
          return false;
        ++this._index;
        return this._index < this._endIndex;
      }

      public T Current
      {
        get
        {
          if (this._index < 0)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._index >= this._endIndex)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          return this._array[this._index];
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      void IEnumerator.Reset()
      {
        this._index = -1;
      }

      public void Dispose()
      {
      }
    }
  }
}
