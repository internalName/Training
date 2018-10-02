// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryObjectWithMapTyped
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class BinaryObjectWithMapTyped : IStreamable
  {
    internal BinaryHeaderEnum binaryHeaderEnum;
    internal int objectId;
    internal string name;
    internal int numMembers;
    internal string[] memberNames;
    internal BinaryTypeEnum[] binaryTypeEnumA;
    internal object[] typeInformationA;
    internal int[] memberAssemIds;
    internal int assemId;

    internal BinaryObjectWithMapTyped()
    {
    }

    internal BinaryObjectWithMapTyped(BinaryHeaderEnum binaryHeaderEnum)
    {
      this.binaryHeaderEnum = binaryHeaderEnum;
    }

    internal void Set(int objectId, string name, int numMembers, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, int assemId)
    {
      this.objectId = objectId;
      this.assemId = assemId;
      this.name = name;
      this.numMembers = numMembers;
      this.memberNames = memberNames;
      this.binaryTypeEnumA = binaryTypeEnumA;
      this.typeInformationA = typeInformationA;
      this.memberAssemIds = memberAssemIds;
      this.assemId = assemId;
      if (assemId > 0)
        this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapTypedAssemId;
      else
        this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapTyped;
    }

    public void Write(__BinaryWriter sout)
    {
      sout.WriteByte((byte) this.binaryHeaderEnum);
      sout.WriteInt32(this.objectId);
      sout.WriteString(this.name);
      sout.WriteInt32(this.numMembers);
      for (int index = 0; index < this.numMembers; ++index)
        sout.WriteString(this.memberNames[index]);
      for (int index = 0; index < this.numMembers; ++index)
        sout.WriteByte((byte) this.binaryTypeEnumA[index]);
      for (int index = 0; index < this.numMembers; ++index)
        BinaryConverter.WriteTypeInfo(this.binaryTypeEnumA[index], this.typeInformationA[index], this.memberAssemIds[index], sout);
      if (this.assemId <= 0)
        return;
      sout.WriteInt32(this.assemId);
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      this.objectId = input.ReadInt32();
      this.name = input.ReadString();
      this.numMembers = input.ReadInt32();
      this.memberNames = new string[this.numMembers];
      this.binaryTypeEnumA = new BinaryTypeEnum[this.numMembers];
      this.typeInformationA = new object[this.numMembers];
      this.memberAssemIds = new int[this.numMembers];
      for (int index = 0; index < this.numMembers; ++index)
        this.memberNames[index] = input.ReadString();
      for (int index = 0; index < this.numMembers; ++index)
        this.binaryTypeEnumA[index] = (BinaryTypeEnum) input.ReadByte();
      for (int index = 0; index < this.numMembers; ++index)
      {
        if (this.binaryTypeEnumA[index] != BinaryTypeEnum.ObjectUrt && this.binaryTypeEnumA[index] != BinaryTypeEnum.ObjectUser)
          this.typeInformationA[index] = BinaryConverter.ReadTypeInfo(this.binaryTypeEnumA[index], input, out this.memberAssemIds[index]);
        else
          BinaryConverter.ReadTypeInfo(this.binaryTypeEnumA[index], input, out this.memberAssemIds[index]);
      }
      if (this.binaryHeaderEnum != BinaryHeaderEnum.ObjectWithMapTypedAssemId)
        return;
      this.assemId = input.ReadInt32();
    }
  }
}
