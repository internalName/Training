// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.ObjectMap
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class ObjectMap
  {
    internal bool isInitObjectInfo = true;
    internal string objectName;
    internal Type objectType;
    internal BinaryTypeEnum[] binaryTypeEnumA;
    internal object[] typeInformationA;
    internal Type[] memberTypes;
    internal string[] memberNames;
    internal ReadObjectInfo objectInfo;
    internal ObjectReader objectReader;
    internal int objectId;
    internal BinaryAssemblyInfo assemblyInfo;

    [SecurityCritical]
    internal ObjectMap(string objectName, Type objectType, string[] memberNames, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo)
    {
      this.objectName = objectName;
      this.objectType = objectType;
      this.memberNames = memberNames;
      this.objectReader = objectReader;
      this.objectId = objectId;
      this.assemblyInfo = assemblyInfo;
      this.objectInfo = objectReader.CreateReadObjectInfo(objectType);
      this.memberTypes = this.objectInfo.GetMemberTypes(memberNames, objectType);
      this.binaryTypeEnumA = new BinaryTypeEnum[this.memberTypes.Length];
      this.typeInformationA = new object[this.memberTypes.Length];
      for (int index = 0; index < this.memberTypes.Length; ++index)
      {
        object typeInformation = (object) null;
        BinaryTypeEnum parserBinaryTypeInfo = BinaryConverter.GetParserBinaryTypeInfo(this.memberTypes[index], out typeInformation);
        this.binaryTypeEnumA[index] = parserBinaryTypeInfo;
        this.typeInformationA[index] = typeInformation;
      }
    }

    [SecurityCritical]
    internal ObjectMap(string objectName, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo, SizedArray assemIdToAssemblyTable)
    {
      this.objectName = objectName;
      this.memberNames = memberNames;
      this.binaryTypeEnumA = binaryTypeEnumA;
      this.typeInformationA = typeInformationA;
      this.objectReader = objectReader;
      this.objectId = objectId;
      this.assemblyInfo = assemblyInfo;
      if (assemblyInfo == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_Assembly", (object) objectName));
      this.objectType = objectReader.GetType(assemblyInfo, objectName);
      this.memberTypes = new Type[memberNames.Length];
      for (int index = 0; index < memberNames.Length; ++index)
      {
        InternalPrimitiveTypeE primitiveTypeEnum;
        string typeString;
        Type type;
        bool isVariant;
        BinaryConverter.TypeFromInfo(binaryTypeEnumA[index], typeInformationA[index], objectReader, (BinaryAssemblyInfo) assemIdToAssemblyTable[memberAssemIds[index]], out primitiveTypeEnum, out typeString, out type, out isVariant);
        this.memberTypes[index] = type;
      }
      this.objectInfo = objectReader.CreateReadObjectInfo(this.objectType, memberNames, (Type[]) null);
      if (this.objectInfo.isSi)
        return;
      this.objectInfo.GetMemberTypes(memberNames, this.objectInfo.objectType);
    }

    internal ReadObjectInfo CreateObjectInfo(ref SerializationInfo si, ref object[] memberData)
    {
      if (this.isInitObjectInfo)
      {
        this.isInitObjectInfo = false;
        this.objectInfo.InitDataStore(ref si, ref memberData);
        return this.objectInfo;
      }
      this.objectInfo.PrepareForReuse();
      this.objectInfo.InitDataStore(ref si, ref memberData);
      return this.objectInfo;
    }

    [SecurityCritical]
    internal static ObjectMap Create(string name, Type objectType, string[] memberNames, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo)
    {
      return new ObjectMap(name, objectType, memberNames, objectReader, objectId, assemblyInfo);
    }

    [SecurityCritical]
    internal static ObjectMap Create(string name, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo, SizedArray assemIdToAssemblyTable)
    {
      return new ObjectMap(name, memberNames, binaryTypeEnumA, typeInformationA, memberAssemIds, objectReader, objectId, assemblyInfo, assemIdToAssemblyTable);
    }
  }
}
