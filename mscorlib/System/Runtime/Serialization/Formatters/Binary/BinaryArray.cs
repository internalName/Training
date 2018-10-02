// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryArray
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class BinaryArray : IStreamable
  {
    internal int objectId;
    internal int rank;
    internal int[] lengthA;
    internal int[] lowerBoundA;
    internal BinaryTypeEnum binaryTypeEnum;
    internal object typeInformation;
    internal int assemId;
    private BinaryHeaderEnum binaryHeaderEnum;
    internal BinaryArrayTypeEnum binaryArrayTypeEnum;

    internal BinaryArray()
    {
    }

    internal BinaryArray(BinaryHeaderEnum binaryHeaderEnum)
    {
      this.binaryHeaderEnum = binaryHeaderEnum;
    }

    internal void Set(int objectId, int rank, int[] lengthA, int[] lowerBoundA, BinaryTypeEnum binaryTypeEnum, object typeInformation, BinaryArrayTypeEnum binaryArrayTypeEnum, int assemId)
    {
      this.objectId = objectId;
      this.binaryArrayTypeEnum = binaryArrayTypeEnum;
      this.rank = rank;
      this.lengthA = lengthA;
      this.lowerBoundA = lowerBoundA;
      this.binaryTypeEnum = binaryTypeEnum;
      this.typeInformation = typeInformation;
      this.assemId = assemId;
      this.binaryHeaderEnum = BinaryHeaderEnum.Array;
      if (binaryArrayTypeEnum != BinaryArrayTypeEnum.Single)
        return;
      switch (binaryTypeEnum)
      {
        case BinaryTypeEnum.Primitive:
          this.binaryHeaderEnum = BinaryHeaderEnum.ArraySinglePrimitive;
          break;
        case BinaryTypeEnum.String:
          this.binaryHeaderEnum = BinaryHeaderEnum.ArraySingleString;
          break;
        case BinaryTypeEnum.Object:
          this.binaryHeaderEnum = BinaryHeaderEnum.ArraySingleObject;
          break;
      }
    }

    public void Write(__BinaryWriter sout)
    {
      switch (this.binaryHeaderEnum)
      {
        case BinaryHeaderEnum.ArraySinglePrimitive:
          sout.WriteByte((byte) this.binaryHeaderEnum);
          sout.WriteInt32(this.objectId);
          sout.WriteInt32(this.lengthA[0]);
          sout.WriteByte((byte) (InternalPrimitiveTypeE) this.typeInformation);
          break;
        case BinaryHeaderEnum.ArraySingleObject:
          sout.WriteByte((byte) this.binaryHeaderEnum);
          sout.WriteInt32(this.objectId);
          sout.WriteInt32(this.lengthA[0]);
          break;
        case BinaryHeaderEnum.ArraySingleString:
          sout.WriteByte((byte) this.binaryHeaderEnum);
          sout.WriteInt32(this.objectId);
          sout.WriteInt32(this.lengthA[0]);
          break;
        default:
          sout.WriteByte((byte) this.binaryHeaderEnum);
          sout.WriteInt32(this.objectId);
          sout.WriteByte((byte) this.binaryArrayTypeEnum);
          sout.WriteInt32(this.rank);
          for (int index = 0; index < this.rank; ++index)
            sout.WriteInt32(this.lengthA[index]);
          if (this.binaryArrayTypeEnum == BinaryArrayTypeEnum.SingleOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.JaggedOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.RectangularOffset)
          {
            for (int index = 0; index < this.rank; ++index)
              sout.WriteInt32(this.lowerBoundA[index]);
          }
          sout.WriteByte((byte) this.binaryTypeEnum);
          BinaryConverter.WriteTypeInfo(this.binaryTypeEnum, this.typeInformation, this.assemId, sout);
          break;
      }
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      switch (this.binaryHeaderEnum)
      {
        case BinaryHeaderEnum.ArraySinglePrimitive:
          this.objectId = input.ReadInt32();
          this.lengthA = new int[1];
          this.lengthA[0] = input.ReadInt32();
          this.binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
          this.rank = 1;
          this.lowerBoundA = new int[this.rank];
          this.binaryTypeEnum = BinaryTypeEnum.Primitive;
          this.typeInformation = (object) (InternalPrimitiveTypeE) input.ReadByte();
          break;
        case BinaryHeaderEnum.ArraySingleObject:
          this.objectId = input.ReadInt32();
          this.lengthA = new int[1];
          this.lengthA[0] = input.ReadInt32();
          this.binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
          this.rank = 1;
          this.lowerBoundA = new int[this.rank];
          this.binaryTypeEnum = BinaryTypeEnum.Object;
          this.typeInformation = (object) null;
          break;
        case BinaryHeaderEnum.ArraySingleString:
          this.objectId = input.ReadInt32();
          this.lengthA = new int[1];
          this.lengthA[0] = input.ReadInt32();
          this.binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
          this.rank = 1;
          this.lowerBoundA = new int[this.rank];
          this.binaryTypeEnum = BinaryTypeEnum.String;
          this.typeInformation = (object) null;
          break;
        default:
          this.objectId = input.ReadInt32();
          this.binaryArrayTypeEnum = (BinaryArrayTypeEnum) input.ReadByte();
          this.rank = input.ReadInt32();
          this.lengthA = new int[this.rank];
          this.lowerBoundA = new int[this.rank];
          for (int index = 0; index < this.rank; ++index)
            this.lengthA[index] = input.ReadInt32();
          if (this.binaryArrayTypeEnum == BinaryArrayTypeEnum.SingleOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.JaggedOffset || this.binaryArrayTypeEnum == BinaryArrayTypeEnum.RectangularOffset)
          {
            for (int index = 0; index < this.rank; ++index)
              this.lowerBoundA[index] = input.ReadInt32();
          }
          this.binaryTypeEnum = (BinaryTypeEnum) input.ReadByte();
          this.typeInformation = BinaryConverter.ReadTypeInfo(this.binaryTypeEnum, input, out this.assemId);
          break;
      }
    }
  }
}
