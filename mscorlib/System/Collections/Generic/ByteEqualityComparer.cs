// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.ByteEqualityComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Collections.Generic
{
  [Serializable]
  internal class ByteEqualityComparer : EqualityComparer<byte>
  {
    public override bool Equals(byte x, byte y)
    {
      return (int) x == (int) y;
    }

    public override int GetHashCode(byte b)
    {
      return b.GetHashCode();
    }

    [SecuritySafeCritical]
    internal override unsafe int IndexOf(byte[] array, byte value, int startIndex, int count)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
      if (count > array.Length - startIndex)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (count == 0)
        return -1;
      fixed (byte* src = array)
        return Buffer.IndexOfByte(src, value, startIndex, count);
    }

    internal override int LastIndexOf(byte[] array, byte value, int startIndex, int count)
    {
      int num = startIndex - count + 1;
      for (int index = startIndex; index >= num; --index)
      {
        if ((int) array[index] == (int) value)
          return index;
      }
      return -1;
    }

    public override bool Equals(object obj)
    {
      return obj is ByteEqualityComparer;
    }

    public override int GetHashCode()
    {
      return this.GetType().Name.GetHashCode();
    }
  }
}
