// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.PrimitiveArray
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class PrimitiveArray
  {
    private InternalPrimitiveTypeE code;
    private bool[] booleanA;
    private char[] charA;
    private double[] doubleA;
    private short[] int16A;
    private int[] int32A;
    private long[] int64A;
    private sbyte[] sbyteA;
    private float[] singleA;
    private ushort[] uint16A;
    private uint[] uint32A;
    private ulong[] uint64A;

    internal PrimitiveArray(InternalPrimitiveTypeE code, Array array)
    {
      this.Init(code, array);
    }

    internal void Init(InternalPrimitiveTypeE code, Array array)
    {
      this.code = code;
      switch (code)
      {
        case InternalPrimitiveTypeE.Boolean:
          this.booleanA = (bool[]) array;
          break;
        case InternalPrimitiveTypeE.Char:
          this.charA = (char[]) array;
          break;
        case InternalPrimitiveTypeE.Double:
          this.doubleA = (double[]) array;
          break;
        case InternalPrimitiveTypeE.Int16:
          this.int16A = (short[]) array;
          break;
        case InternalPrimitiveTypeE.Int32:
          this.int32A = (int[]) array;
          break;
        case InternalPrimitiveTypeE.Int64:
          this.int64A = (long[]) array;
          break;
        case InternalPrimitiveTypeE.SByte:
          this.sbyteA = (sbyte[]) array;
          break;
        case InternalPrimitiveTypeE.Single:
          this.singleA = (float[]) array;
          break;
        case InternalPrimitiveTypeE.UInt16:
          this.uint16A = (ushort[]) array;
          break;
        case InternalPrimitiveTypeE.UInt32:
          this.uint32A = (uint[]) array;
          break;
        case InternalPrimitiveTypeE.UInt64:
          this.uint64A = (ulong[]) array;
          break;
      }
    }

    internal void SetValue(string value, int index)
    {
      switch (this.code)
      {
        case InternalPrimitiveTypeE.Boolean:
          this.booleanA[index] = bool.Parse(value);
          break;
        case InternalPrimitiveTypeE.Char:
          if (value[0] == '_' && value.Equals("_0x00_"))
          {
            this.charA[index] = char.MinValue;
            break;
          }
          this.charA[index] = char.Parse(value);
          break;
        case InternalPrimitiveTypeE.Double:
          this.doubleA[index] = double.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case InternalPrimitiveTypeE.Int16:
          this.int16A[index] = short.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case InternalPrimitiveTypeE.Int32:
          this.int32A[index] = int.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case InternalPrimitiveTypeE.Int64:
          this.int64A[index] = long.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case InternalPrimitiveTypeE.SByte:
          this.sbyteA[index] = sbyte.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case InternalPrimitiveTypeE.Single:
          this.singleA[index] = float.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case InternalPrimitiveTypeE.UInt16:
          this.uint16A[index] = ushort.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case InternalPrimitiveTypeE.UInt32:
          this.uint32A[index] = uint.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case InternalPrimitiveTypeE.UInt64:
          this.uint64A[index] = ulong.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
      }
    }
  }
}
