// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.__BinaryWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class __BinaryWriter
  {
    private int chunkSize = 4096;
    internal Stream sout;
    internal FormatterTypeStyle formatterTypeStyle;
    internal Hashtable objectMapTable;
    internal ObjectWriter objectWriter;
    internal BinaryWriter dataWriter;
    internal int m_nestedObjectCount;
    private int nullCount;
    internal BinaryMethodCall binaryMethodCall;
    internal BinaryMethodReturn binaryMethodReturn;
    internal BinaryObject binaryObject;
    internal BinaryObjectWithMap binaryObjectWithMap;
    internal BinaryObjectWithMapTyped binaryObjectWithMapTyped;
    internal BinaryObjectString binaryObjectString;
    internal BinaryCrossAppDomainString binaryCrossAppDomainString;
    internal BinaryArray binaryArray;
    private byte[] byteBuffer;
    internal MemberPrimitiveUnTyped memberPrimitiveUnTyped;
    internal MemberPrimitiveTyped memberPrimitiveTyped;
    internal ObjectNull objectNull;
    internal MemberReference memberReference;
    internal BinaryAssembly binaryAssembly;
    internal BinaryCrossAppDomainAssembly crossAppDomainAssembly;

    internal __BinaryWriter(Stream sout, ObjectWriter objectWriter, FormatterTypeStyle formatterTypeStyle)
    {
      this.sout = sout;
      this.formatterTypeStyle = formatterTypeStyle;
      this.objectWriter = objectWriter;
      this.m_nestedObjectCount = 0;
      this.dataWriter = new BinaryWriter(sout, Encoding.UTF8);
    }

    internal void WriteBegin()
    {
    }

    internal void WriteEnd()
    {
      this.dataWriter.Flush();
    }

    internal void WriteBoolean(bool value)
    {
      this.dataWriter.Write(value);
    }

    internal void WriteByte(byte value)
    {
      this.dataWriter.Write(value);
    }

    private void WriteBytes(byte[] value)
    {
      this.dataWriter.Write(value);
    }

    private void WriteBytes(byte[] byteA, int offset, int size)
    {
      this.dataWriter.Write(byteA, offset, size);
    }

    internal void WriteChar(char value)
    {
      this.dataWriter.Write(value);
    }

    internal void WriteChars(char[] value)
    {
      this.dataWriter.Write(value);
    }

    internal void WriteDecimal(Decimal value)
    {
      this.WriteString(value.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }

    internal void WriteSingle(float value)
    {
      this.dataWriter.Write(value);
    }

    internal void WriteDouble(double value)
    {
      this.dataWriter.Write(value);
    }

    internal void WriteInt16(short value)
    {
      this.dataWriter.Write(value);
    }

    internal void WriteInt32(int value)
    {
      this.dataWriter.Write(value);
    }

    internal void WriteInt64(long value)
    {
      this.dataWriter.Write(value);
    }

    internal void WriteSByte(sbyte value)
    {
      this.WriteByte((byte) value);
    }

    internal void WriteString(string value)
    {
      this.dataWriter.Write(value);
    }

    internal void WriteTimeSpan(TimeSpan value)
    {
      this.WriteInt64(value.Ticks);
    }

    internal void WriteDateTime(DateTime value)
    {
      this.WriteInt64(value.ToBinaryRaw());
    }

    internal void WriteUInt16(ushort value)
    {
      this.dataWriter.Write(value);
    }

    internal void WriteUInt32(uint value)
    {
      this.dataWriter.Write(value);
    }

    internal void WriteUInt64(ulong value)
    {
      this.dataWriter.Write(value);
    }

    internal void WriteObjectEnd(NameInfo memberNameInfo, NameInfo typeNameInfo)
    {
    }

    internal void WriteSerializationHeaderEnd()
    {
      MessageEnd messageEnd = new MessageEnd();
      messageEnd.Dump(this.sout);
      messageEnd.Write(this);
    }

    internal void WriteSerializationHeader(int topId, int headerId, int minorVersion, int majorVersion)
    {
      SerializationHeaderRecord serializationHeaderRecord = new SerializationHeaderRecord(BinaryHeaderEnum.SerializedStreamHeader, topId, headerId, minorVersion, majorVersion);
      serializationHeaderRecord.Dump();
      serializationHeaderRecord.Write(this);
    }

    internal void WriteMethodCall()
    {
      if (this.binaryMethodCall == null)
        this.binaryMethodCall = new BinaryMethodCall();
      this.binaryMethodCall.Dump();
      this.binaryMethodCall.Write(this);
    }

    internal object[] WriteCallArray(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, object callContext, object[] properties)
    {
      if (this.binaryMethodCall == null)
        this.binaryMethodCall = new BinaryMethodCall();
      return this.binaryMethodCall.WriteArray(uri, methodName, typeName, instArgs, args, methodSignature, callContext, properties);
    }

    internal void WriteMethodReturn()
    {
      if (this.binaryMethodReturn == null)
        this.binaryMethodReturn = new BinaryMethodReturn();
      this.binaryMethodReturn.Dump();
      this.binaryMethodReturn.Write(this);
    }

    internal object[] WriteReturnArray(object returnValue, object[] args, Exception exception, object callContext, object[] properties)
    {
      if (this.binaryMethodReturn == null)
        this.binaryMethodReturn = new BinaryMethodReturn();
      return this.binaryMethodReturn.WriteArray(returnValue, args, exception, callContext, properties);
    }

    internal void WriteObject(NameInfo nameInfo, NameInfo typeNameInfo, int numMembers, string[] memberNames, Type[] memberTypes, WriteObjectInfo[] memberObjectInfos)
    {
      this.InternalWriteItemNull();
      int niobjectId = (int) nameInfo.NIobjectId;
      string name = niobjectId >= 0 ? nameInfo.NIname : typeNameInfo.NIname;
      if (this.objectMapTable == null)
        this.objectMapTable = new Hashtable();
      ObjectMapInfo objectMapInfo = (ObjectMapInfo) this.objectMapTable[(object) name];
      if (objectMapInfo != null && objectMapInfo.isCompatible(numMembers, memberNames, memberTypes))
      {
        if (this.binaryObject == null)
          this.binaryObject = new BinaryObject();
        this.binaryObject.Set(niobjectId, objectMapInfo.objectId);
        this.binaryObject.Write(this);
      }
      else if (!typeNameInfo.NItransmitTypeOnObject)
      {
        if (this.binaryObjectWithMap == null)
          this.binaryObjectWithMap = new BinaryObjectWithMap();
        int niassemId = (int) typeNameInfo.NIassemId;
        this.binaryObjectWithMap.Set(niobjectId, name, numMembers, memberNames, niassemId);
        this.binaryObjectWithMap.Dump();
        this.binaryObjectWithMap.Write(this);
        if (objectMapInfo != null)
          return;
        this.objectMapTable.Add((object) name, (object) new ObjectMapInfo(niobjectId, numMembers, memberNames, memberTypes));
      }
      else
      {
        BinaryTypeEnum[] binaryTypeEnumA = new BinaryTypeEnum[numMembers];
        object[] typeInformationA = new object[numMembers];
        int[] memberAssemIds = new int[numMembers];
        int assemId;
        for (int index = 0; index < numMembers; ++index)
        {
          object typeInformation = (object) null;
          binaryTypeEnumA[index] = BinaryConverter.GetBinaryTypeInfo(memberTypes[index], memberObjectInfos[index], (string) null, this.objectWriter, out typeInformation, out assemId);
          typeInformationA[index] = typeInformation;
          memberAssemIds[index] = assemId;
        }
        if (this.binaryObjectWithMapTyped == null)
          this.binaryObjectWithMapTyped = new BinaryObjectWithMapTyped();
        assemId = (int) typeNameInfo.NIassemId;
        this.binaryObjectWithMapTyped.Set(niobjectId, name, numMembers, memberNames, binaryTypeEnumA, typeInformationA, memberAssemIds, assemId);
        this.binaryObjectWithMapTyped.Write(this);
        if (objectMapInfo != null)
          return;
        this.objectMapTable.Add((object) name, (object) new ObjectMapInfo(niobjectId, numMembers, memberNames, memberTypes));
      }
    }

    internal void WriteObjectString(int objectId, string value)
    {
      this.InternalWriteItemNull();
      if (this.binaryObjectString == null)
        this.binaryObjectString = new BinaryObjectString();
      this.binaryObjectString.Set(objectId, value);
      this.binaryObjectString.Write(this);
    }

    [SecurityCritical]
    internal void WriteSingleArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound, Array array)
    {
      this.InternalWriteItemNull();
      int[] lengthA = new int[1]{ length };
      int[] lowerBoundA = (int[]) null;
      object typeInformation = (object) null;
      BinaryArrayTypeEnum binaryArrayTypeEnum;
      if (lowerBound == 0)
      {
        binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
      }
      else
      {
        binaryArrayTypeEnum = BinaryArrayTypeEnum.SingleOffset;
        lowerBoundA = new int[1]{ lowerBound };
      }
      int assemId;
      BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out typeInformation, out assemId);
      if (this.binaryArray == null)
        this.binaryArray = new BinaryArray();
      this.binaryArray.Set((int) arrayNameInfo.NIobjectId, 1, lengthA, lowerBoundA, binaryTypeInfo, typeInformation, binaryArrayTypeEnum, assemId);
      long niobjectId = arrayNameInfo.NIobjectId;
      this.binaryArray.Write(this);
      if (!Converter.IsWriteAsByteArray(arrayElemTypeNameInfo.NIprimitiveTypeEnum) || lowerBound != 0)
        return;
      if (arrayElemTypeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Byte)
        this.WriteBytes((byte[]) array);
      else if (arrayElemTypeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Char)
        this.WriteChars((char[]) array);
      else
        this.WriteArrayAsBytes(array, Converter.TypeLength(arrayElemTypeNameInfo.NIprimitiveTypeEnum));
    }

    [SecurityCritical]
    private void WriteArrayAsBytes(Array array, int typeLength)
    {
      this.InternalWriteItemNull();
      int num1 = array.Length * typeLength;
      int num2 = 0;
      if (this.byteBuffer == null)
        this.byteBuffer = new byte[this.chunkSize];
      while (num2 < array.Length)
      {
        int num3 = Math.Min(this.chunkSize / typeLength, array.Length - num2);
        int num4 = num3 * typeLength;
        Buffer.InternalBlockCopy(array, num2 * typeLength, (Array) this.byteBuffer, 0, num4);
        this.WriteBytes(this.byteBuffer, 0, num4);
        num2 += num3;
      }
    }

    internal void WriteJaggedArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound)
    {
      this.InternalWriteItemNull();
      int[] lengthA = new int[1]{ length };
      int[] lowerBoundA = (int[]) null;
      object typeInformation = (object) null;
      int assemId = 0;
      BinaryArrayTypeEnum binaryArrayTypeEnum;
      if (lowerBound == 0)
      {
        binaryArrayTypeEnum = BinaryArrayTypeEnum.Jagged;
      }
      else
      {
        binaryArrayTypeEnum = BinaryArrayTypeEnum.JaggedOffset;
        lowerBoundA = new int[1]{ lowerBound };
      }
      BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out typeInformation, out assemId);
      if (this.binaryArray == null)
        this.binaryArray = new BinaryArray();
      this.binaryArray.Set((int) arrayNameInfo.NIobjectId, 1, lengthA, lowerBoundA, binaryTypeInfo, typeInformation, binaryArrayTypeEnum, assemId);
      long niobjectId = arrayNameInfo.NIobjectId;
      this.binaryArray.Write(this);
    }

    internal void WriteRectangleArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int rank, int[] lengthA, int[] lowerBoundA)
    {
      this.InternalWriteItemNull();
      BinaryArrayTypeEnum binaryArrayTypeEnum = BinaryArrayTypeEnum.Rectangular;
      object typeInformation = (object) null;
      int assemId = 0;
      BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out typeInformation, out assemId);
      if (this.binaryArray == null)
        this.binaryArray = new BinaryArray();
      for (int index = 0; index < rank; ++index)
      {
        if (lowerBoundA[index] != 0)
        {
          binaryArrayTypeEnum = BinaryArrayTypeEnum.RectangularOffset;
          break;
        }
      }
      this.binaryArray.Set((int) arrayNameInfo.NIobjectId, rank, lengthA, lowerBoundA, binaryTypeInfo, typeInformation, binaryArrayTypeEnum, assemId);
      long niobjectId = arrayNameInfo.NIobjectId;
      this.binaryArray.Write(this);
    }

    [SecurityCritical]
    internal void WriteObjectByteArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound, byte[] byteA)
    {
      this.InternalWriteItemNull();
      this.WriteSingleArray(memberNameInfo, arrayNameInfo, objectInfo, arrayElemTypeNameInfo, length, lowerBound, (Array) byteA);
    }

    internal void WriteMember(NameInfo memberNameInfo, NameInfo typeNameInfo, object value)
    {
      this.InternalWriteItemNull();
      InternalPrimitiveTypeE niprimitiveTypeEnum = typeNameInfo.NIprimitiveTypeEnum;
      if (memberNameInfo.NItransmitTypeOnMember)
      {
        if (this.memberPrimitiveTyped == null)
          this.memberPrimitiveTyped = new MemberPrimitiveTyped();
        this.memberPrimitiveTyped.Set(niprimitiveTypeEnum, value);
        int num = memberNameInfo.NIisArrayItem ? 1 : 0;
        this.memberPrimitiveTyped.Dump();
        this.memberPrimitiveTyped.Write(this);
      }
      else
      {
        if (this.memberPrimitiveUnTyped == null)
          this.memberPrimitiveUnTyped = new MemberPrimitiveUnTyped();
        this.memberPrimitiveUnTyped.Set(niprimitiveTypeEnum, value);
        int num = memberNameInfo.NIisArrayItem ? 1 : 0;
        this.memberPrimitiveUnTyped.Dump();
        this.memberPrimitiveUnTyped.Write(this);
      }
    }

    internal void WriteNullMember(NameInfo memberNameInfo, NameInfo typeNameInfo)
    {
      this.InternalWriteItemNull();
      if (this.objectNull == null)
        this.objectNull = new ObjectNull();
      if (memberNameInfo.NIisArrayItem)
        return;
      this.objectNull.SetNullCount(1);
      this.objectNull.Dump();
      this.objectNull.Write(this);
      this.nullCount = 0;
    }

    internal void WriteMemberObjectRef(NameInfo memberNameInfo, int idRef)
    {
      this.InternalWriteItemNull();
      if (this.memberReference == null)
        this.memberReference = new MemberReference();
      this.memberReference.Set(idRef);
      int num = memberNameInfo.NIisArrayItem ? 1 : 0;
      this.memberReference.Dump();
      this.memberReference.Write(this);
    }

    internal void WriteMemberNested(NameInfo memberNameInfo)
    {
      this.InternalWriteItemNull();
      int num = memberNameInfo.NIisArrayItem ? 1 : 0;
    }

    internal void WriteMemberString(NameInfo memberNameInfo, NameInfo typeNameInfo, string value)
    {
      this.InternalWriteItemNull();
      int num = memberNameInfo.NIisArrayItem ? 1 : 0;
      this.WriteObjectString((int) typeNameInfo.NIobjectId, value);
    }

    internal void WriteItem(NameInfo itemNameInfo, NameInfo typeNameInfo, object value)
    {
      this.InternalWriteItemNull();
      this.WriteMember(itemNameInfo, typeNameInfo, value);
    }

    internal void WriteNullItem(NameInfo itemNameInfo, NameInfo typeNameInfo)
    {
      ++this.nullCount;
      this.InternalWriteItemNull();
    }

    internal void WriteDelayedNullItem()
    {
      ++this.nullCount;
    }

    internal void WriteItemEnd()
    {
      this.InternalWriteItemNull();
    }

    private void InternalWriteItemNull()
    {
      if (this.nullCount <= 0)
        return;
      if (this.objectNull == null)
        this.objectNull = new ObjectNull();
      this.objectNull.SetNullCount(this.nullCount);
      this.objectNull.Dump();
      this.objectNull.Write(this);
      this.nullCount = 0;
    }

    internal void WriteItemObjectRef(NameInfo nameInfo, int idRef)
    {
      this.InternalWriteItemNull();
      this.WriteMemberObjectRef(nameInfo, idRef);
    }

    internal void WriteAssembly(Type type, string assemblyString, int assemId, bool isNew)
    {
      this.InternalWriteItemNull();
      if (assemblyString == null)
        assemblyString = string.Empty;
      if (!isNew)
        return;
      if (this.binaryAssembly == null)
        this.binaryAssembly = new BinaryAssembly();
      this.binaryAssembly.Set(assemId, assemblyString);
      this.binaryAssembly.Dump();
      this.binaryAssembly.Write(this);
    }

    internal void WriteValue(InternalPrimitiveTypeE code, object value)
    {
      switch (code)
      {
        case InternalPrimitiveTypeE.Boolean:
          this.WriteBoolean(Convert.ToBoolean(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case InternalPrimitiveTypeE.Byte:
          this.WriteByte(Convert.ToByte(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case InternalPrimitiveTypeE.Char:
          this.WriteChar(Convert.ToChar(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case InternalPrimitiveTypeE.Decimal:
          this.WriteDecimal(Convert.ToDecimal(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case InternalPrimitiveTypeE.Double:
          this.WriteDouble(Convert.ToDouble(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case InternalPrimitiveTypeE.Int16:
          this.WriteInt16(Convert.ToInt16(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case InternalPrimitiveTypeE.Int32:
          this.WriteInt32(Convert.ToInt32(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case InternalPrimitiveTypeE.Int64:
          this.WriteInt64(Convert.ToInt64(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case InternalPrimitiveTypeE.SByte:
          this.WriteSByte(Convert.ToSByte(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case InternalPrimitiveTypeE.Single:
          this.WriteSingle(Convert.ToSingle(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case InternalPrimitiveTypeE.TimeSpan:
          this.WriteTimeSpan((TimeSpan) value);
          break;
        case InternalPrimitiveTypeE.DateTime:
          this.WriteDateTime((DateTime) value);
          break;
        case InternalPrimitiveTypeE.UInt16:
          this.WriteUInt16(Convert.ToUInt16(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case InternalPrimitiveTypeE.UInt32:
          this.WriteUInt32(Convert.ToUInt32(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case InternalPrimitiveTypeE.UInt64:
          this.WriteUInt64(Convert.ToUInt64(value, (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        default:
          throw new SerializationException(Environment.GetResourceString("Serialization_TypeCode", (object) code.ToString()));
      }
    }
  }
}
