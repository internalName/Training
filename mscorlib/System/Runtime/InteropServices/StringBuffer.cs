// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.StringBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
  internal class StringBuffer : NativeBuffer
  {
    private uint _length;

    public StringBuffer(uint initialCapacity = 0)
      : base((ulong) initialCapacity * 2UL)
    {
    }

    public StringBuffer(string initialContents)
      : base(0UL)
    {
      if (initialContents == null)
        return;
      this.Append(initialContents, 0, -1);
    }

    public StringBuffer(StringBuffer initialContents)
      : base(0UL)
    {
      if (initialContents == null)
        return;
      this.Append(initialContents, 0U);
    }

    public unsafe char this[uint index]
    {
      [SecuritySafeCritical] get
      {
        if (index >= this._length)
          throw new ArgumentOutOfRangeException(nameof (index));
        return (char) *(ushort*) ((IntPtr) this.CharPointer + (IntPtr) ((long) index * 2L));
      }
      [SecuritySafeCritical] set
      {
        if (index >= this._length)
          throw new ArgumentOutOfRangeException(nameof (index));
        *(short*) ((IntPtr) this.CharPointer + (IntPtr) ((long) index * 2L)) = (short) value;
      }
    }

    public uint CharCapacity
    {
      [SecuritySafeCritical] get
      {
        ulong byteCapacity = this.ByteCapacity;
        ulong num = byteCapacity == 0UL ? 0UL : byteCapacity / 2UL;
        if (num <= (ulong) uint.MaxValue)
          return (uint) num;
        return uint.MaxValue;
      }
    }

    [SecuritySafeCritical]
    public void EnsureCharCapacity(uint minCapacity)
    {
      this.EnsureByteCapacity((ulong) minCapacity * 2UL);
    }

    public unsafe uint Length
    {
      get
      {
        return this._length;
      }
      [SecuritySafeCritical] set
      {
        if (value == uint.MaxValue)
          throw new ArgumentOutOfRangeException(nameof (Length));
        this.EnsureCharCapacity(value + 1U);
        *(short*) ((IntPtr) this.CharPointer + (IntPtr) ((long) value * 2L)) = (short) 0;
        this._length = value;
      }
    }

    [SecuritySafeCritical]
    public unsafe void SetLengthToFirstNull()
    {
      char* charPointer = this.CharPointer;
      uint charCapacity = this.CharCapacity;
      for (uint index = 0; index < charCapacity; ++index)
      {
        if (*(ushort*) ((IntPtr) charPointer + (IntPtr) ((long) index * 2L)) == (ushort) 0)
        {
          this._length = index;
          break;
        }
      }
    }

    internal unsafe char* CharPointer
    {
      [SecurityCritical] get
      {
        return (char*) this.VoidPointer;
      }
    }

    [SecurityCritical]
    public unsafe bool Contains(char value)
    {
      char* charPointer = this.CharPointer;
      uint length = this._length;
      for (uint index = 0; index < length; ++index)
      {
        if ((int) *charPointer++ == (int) value)
          return true;
      }
      return false;
    }

    [SecuritySafeCritical]
    public bool StartsWith(string value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (this._length < (uint) value.Length)
        return false;
      return this.SubstringEquals(value, 0U, value.Length);
    }

    [SecuritySafeCritical]
    public unsafe bool SubstringEquals(string value, uint startIndex = 0, int count = -1)
    {
      if (value == null)
        return false;
      if (count < -1)
        throw new ArgumentOutOfRangeException(nameof (count));
      if (startIndex > this._length)
        throw new ArgumentOutOfRangeException(nameof (startIndex));
      uint num = count == -1 ? this._length - startIndex : (uint) count;
      if (checked (startIndex + num) > this._length)
        throw new ArgumentOutOfRangeException(nameof (count));
      int length = value.Length;
      if ((int) num != length)
        return false;
      string str = value;
      char* chPtr1 = (char*) str;
      if ((IntPtr) chPtr1 != IntPtr.Zero)
        chPtr1 += RuntimeHelpers.OffsetToStringData;
      char* chPtr2 = (char*) ((IntPtr) this.CharPointer + (IntPtr) ((long) startIndex * 2L));
      for (int index = 0; index < length; ++index)
      {
        if ((int) *chPtr2++ != (int) chPtr1[index])
          return false;
      }
      str = (string) null;
      return true;
    }

    [SecuritySafeCritical]
    public void Append(string value, int startIndex = 0, int count = -1)
    {
      this.CopyFrom(this._length, value, startIndex, count);
    }

    public void Append(StringBuffer value, uint startIndex = 0)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (value.Length == 0U)
        return;
      value.CopyTo(startIndex, this, this._length, value.Length);
    }

    public void Append(StringBuffer value, uint startIndex, uint count)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (count == 0U)
        return;
      value.CopyTo(startIndex, this, this._length, count);
    }

    [SecuritySafeCritical]
    public unsafe void CopyTo(uint bufferIndex, StringBuffer destination, uint destinationIndex, uint count)
    {
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      if (destinationIndex > destination._length)
        throw new ArgumentOutOfRangeException(nameof (destinationIndex));
      if (bufferIndex >= this._length)
        throw new ArgumentOutOfRangeException(nameof (bufferIndex));
      if (this._length < checked (bufferIndex + count))
        throw new ArgumentOutOfRangeException(nameof (count));
      if (count == 0U)
        return;
      uint num = checked (destinationIndex + count);
      if (destination._length < num)
        destination.Length = num;
      Buffer.MemoryCopy((void*) ((IntPtr) this.CharPointer + (IntPtr) ((long) bufferIndex * 2L)), (void*) ((IntPtr) destination.CharPointer + (IntPtr) ((long) destinationIndex * 2L)), checked ((long) (destination.ByteCapacity - (ulong) (destinationIndex * 2U))), checked ((long) count * 2L));
    }

    [SecuritySafeCritical]
    public unsafe void CopyFrom(uint bufferIndex, string source, int sourceIndex = 0, int count = -1)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (bufferIndex > this._length)
        throw new ArgumentOutOfRangeException(nameof (bufferIndex));
      if (sourceIndex < 0 || sourceIndex >= source.Length)
        throw new ArgumentOutOfRangeException(nameof (sourceIndex));
      if (count == -1)
        count = source.Length - sourceIndex;
      if (count < 0 || source.Length - count < sourceIndex)
        throw new ArgumentOutOfRangeException(nameof (count));
      if (count == 0)
        return;
      uint num = bufferIndex + (uint) count;
      if (this._length < num)
        this.Length = num;
      string str = source;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      Buffer.MemoryCopy((void*) (chPtr + sourceIndex), (void*) ((IntPtr) this.CharPointer + (IntPtr) ((long) bufferIndex * 2L)), checked ((long) (this.ByteCapacity - (ulong) (bufferIndex * 2U))), (long) count * 2L);
      str = (string) null;
    }

    [SecuritySafeCritical]
    public unsafe void TrimEnd(char[] values)
    {
      if (values == null || values.Length == 0 || this._length == 0U)
        return;
      char* chPtr = (char*) ((IntPtr) this.CharPointer + (IntPtr) ((long) this._length * 2L) - 2);
      while (this._length > 0U && Array.IndexOf<char>(values, *chPtr) >= 0)
      {
        this.Length = this._length - 1U;
        chPtr -= 2;
      }
    }

    [SecuritySafeCritical]
    public override unsafe string ToString()
    {
      if (this._length == 0U)
        return string.Empty;
      if (this._length > (uint) int.MaxValue)
        throw new InvalidOperationException();
      return new string(this.CharPointer, 0, (int) this._length);
    }

    [SecuritySafeCritical]
    public unsafe string Substring(uint startIndex, int count = -1)
    {
      if (startIndex > (this._length == 0U ? 0U : this._length - 1U))
        throw new ArgumentOutOfRangeException(nameof (startIndex));
      if (count < -1)
        throw new ArgumentOutOfRangeException(nameof (count));
      uint num = count == -1 ? this._length - startIndex : (uint) count;
      if (num > (uint) int.MaxValue || checked (startIndex + num) > this._length)
        throw new ArgumentOutOfRangeException(nameof (count));
      if (num == 0U)
        return string.Empty;
      return new string((char*) ((IntPtr) this.CharPointer + (IntPtr) ((long) startIndex * 2L)), 0, (int) num);
    }

    [SecuritySafeCritical]
    public override void Free()
    {
      base.Free();
      this._length = 0U;
    }
  }
}
