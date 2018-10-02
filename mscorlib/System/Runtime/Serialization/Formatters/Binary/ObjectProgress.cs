// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.ObjectProgress
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class ObjectProgress
  {
    internal static int opRecordIdCount = 1;
    internal BinaryTypeEnum expectedType = BinaryTypeEnum.ObjectUrt;
    internal ParseRecord pr = new ParseRecord();
    internal int opRecordId;
    internal bool isInitial;
    internal int count;
    internal object expectedTypeInformation;
    internal string name;
    internal InternalObjectTypeE objectTypeEnum;
    internal InternalMemberTypeE memberTypeEnum;
    internal InternalMemberValueE memberValueEnum;
    internal Type dtType;
    internal int numItems;
    internal BinaryTypeEnum binaryTypeEnum;
    internal object typeInformation;
    internal int nullCount;
    internal int memberLength;
    internal BinaryTypeEnum[] binaryTypeEnumA;
    internal object[] typeInformationA;
    internal string[] memberNames;
    internal Type[] memberTypes;

    internal ObjectProgress()
    {
    }

    [Conditional("SER_LOGGING")]
    private void Counter()
    {
      lock (this)
      {
        this.opRecordId = ObjectProgress.opRecordIdCount++;
        if (ObjectProgress.opRecordIdCount <= 1000)
          return;
        ObjectProgress.opRecordIdCount = 1;
      }
    }

    internal void Init()
    {
      this.isInitial = false;
      this.count = 0;
      this.expectedType = BinaryTypeEnum.ObjectUrt;
      this.expectedTypeInformation = (object) null;
      this.name = (string) null;
      this.objectTypeEnum = InternalObjectTypeE.Empty;
      this.memberTypeEnum = InternalMemberTypeE.Empty;
      this.memberValueEnum = InternalMemberValueE.Empty;
      this.dtType = (Type) null;
      this.numItems = 0;
      this.nullCount = 0;
      this.typeInformation = (object) null;
      this.memberLength = 0;
      this.binaryTypeEnumA = (BinaryTypeEnum[]) null;
      this.typeInformationA = (object[]) null;
      this.memberNames = (string[]) null;
      this.memberTypes = (Type[]) null;
      this.pr.Init();
    }

    internal void ArrayCountIncrement(int value)
    {
      this.count += value;
    }

    internal bool GetNext(out BinaryTypeEnum outBinaryTypeEnum, out object outTypeInformation)
    {
      outBinaryTypeEnum = BinaryTypeEnum.Primitive;
      outTypeInformation = (object) null;
      if (this.objectTypeEnum == InternalObjectTypeE.Array)
      {
        if (this.count == this.numItems)
          return false;
        outBinaryTypeEnum = this.binaryTypeEnum;
        outTypeInformation = this.typeInformation;
        if (this.count == 0)
          this.isInitial = false;
        ++this.count;
        return true;
      }
      if (this.count == this.memberLength && !this.isInitial)
        return false;
      outBinaryTypeEnum = this.binaryTypeEnumA[this.count];
      outTypeInformation = this.typeInformationA[this.count];
      if (this.count == 0)
        this.isInitial = false;
      this.name = this.memberNames[this.count];
      Type[] memberTypes = this.memberTypes;
      this.dtType = this.memberTypes[this.count];
      ++this.count;
      return true;
    }
  }
}
