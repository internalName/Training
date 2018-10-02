// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryConverter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal static class BinaryConverter
  {
    internal static BinaryTypeEnum GetBinaryTypeInfo(Type type, WriteObjectInfo objectInfo, string typeName, ObjectWriter objectWriter, out object typeInformation, out int assemId)
    {
      assemId = 0;
      typeInformation = (object) null;
      BinaryTypeEnum binaryTypeEnum;
      if ((object) type == (object) Converter.typeofString)
        binaryTypeEnum = BinaryTypeEnum.String;
      else if ((objectInfo == null || objectInfo != null && !objectInfo.isSi) && (object) type == (object) Converter.typeofObject)
        binaryTypeEnum = BinaryTypeEnum.Object;
      else if ((object) type == (object) Converter.typeofStringArray)
        binaryTypeEnum = BinaryTypeEnum.StringArray;
      else if ((object) type == (object) Converter.typeofObjectArray)
        binaryTypeEnum = BinaryTypeEnum.ObjectArray;
      else if (Converter.IsPrimitiveArray(type, out typeInformation))
      {
        binaryTypeEnum = BinaryTypeEnum.PrimitiveArray;
      }
      else
      {
        InternalPrimitiveTypeE code = objectWriter.ToCode(type);
        if (code == InternalPrimitiveTypeE.Invalid)
        {
          string str;
          if (objectInfo == null)
          {
            str = type.Assembly.FullName;
            typeInformation = (object) type.FullName;
          }
          else
          {
            str = objectInfo.GetAssemblyString();
            typeInformation = (object) objectInfo.GetTypeFullName();
          }
          if (str.Equals(Converter.urtAssemblyString))
          {
            binaryTypeEnum = BinaryTypeEnum.ObjectUrt;
            assemId = 0;
          }
          else
          {
            binaryTypeEnum = BinaryTypeEnum.ObjectUser;
            assemId = (int) objectInfo.assemId;
            if (assemId == 0)
              throw new SerializationException(Environment.GetResourceString("Serialization_AssemblyId", typeInformation));
          }
        }
        else
        {
          binaryTypeEnum = BinaryTypeEnum.Primitive;
          typeInformation = (object) code;
        }
      }
      return binaryTypeEnum;
    }

    internal static BinaryTypeEnum GetParserBinaryTypeInfo(Type type, out object typeInformation)
    {
      typeInformation = (object) null;
      BinaryTypeEnum binaryTypeEnum;
      if ((object) type == (object) Converter.typeofString)
        binaryTypeEnum = BinaryTypeEnum.String;
      else if ((object) type == (object) Converter.typeofObject)
        binaryTypeEnum = BinaryTypeEnum.Object;
      else if ((object) type == (object) Converter.typeofObjectArray)
        binaryTypeEnum = BinaryTypeEnum.ObjectArray;
      else if ((object) type == (object) Converter.typeofStringArray)
        binaryTypeEnum = BinaryTypeEnum.StringArray;
      else if (Converter.IsPrimitiveArray(type, out typeInformation))
      {
        binaryTypeEnum = BinaryTypeEnum.PrimitiveArray;
      }
      else
      {
        InternalPrimitiveTypeE code = Converter.ToCode(type);
        if (code == InternalPrimitiveTypeE.Invalid)
        {
          binaryTypeEnum = !(Assembly.GetAssembly(type) == Converter.urtAssembly) ? BinaryTypeEnum.ObjectUser : BinaryTypeEnum.ObjectUrt;
          typeInformation = (object) type.FullName;
        }
        else
        {
          binaryTypeEnum = BinaryTypeEnum.Primitive;
          typeInformation = (object) code;
        }
      }
      return binaryTypeEnum;
    }

    internal static void WriteTypeInfo(BinaryTypeEnum binaryTypeEnum, object typeInformation, int assemId, __BinaryWriter sout)
    {
      switch (binaryTypeEnum)
      {
        case BinaryTypeEnum.Primitive:
        case BinaryTypeEnum.PrimitiveArray:
          sout.WriteByte((byte) (InternalPrimitiveTypeE) typeInformation);
          break;
        case BinaryTypeEnum.String:
          break;
        case BinaryTypeEnum.Object:
          break;
        case BinaryTypeEnum.ObjectUrt:
          sout.WriteString(typeInformation.ToString());
          break;
        case BinaryTypeEnum.ObjectUser:
          sout.WriteString(typeInformation.ToString());
          sout.WriteInt32(assemId);
          break;
        case BinaryTypeEnum.ObjectArray:
          break;
        case BinaryTypeEnum.StringArray:
          break;
        default:
          throw new SerializationException(Environment.GetResourceString("Serialization_TypeWrite", (object) binaryTypeEnum.ToString()));
      }
    }

    internal static object ReadTypeInfo(BinaryTypeEnum binaryTypeEnum, __BinaryParser input, out int assemId)
    {
      object obj = (object) null;
      int num = 0;
      switch (binaryTypeEnum)
      {
        case BinaryTypeEnum.Primitive:
        case BinaryTypeEnum.PrimitiveArray:
          obj = (object) (InternalPrimitiveTypeE) input.ReadByte();
          goto case BinaryTypeEnum.String;
        case BinaryTypeEnum.String:
        case BinaryTypeEnum.Object:
        case BinaryTypeEnum.ObjectArray:
        case BinaryTypeEnum.StringArray:
          assemId = num;
          return obj;
        case BinaryTypeEnum.ObjectUrt:
          obj = (object) input.ReadString();
          goto case BinaryTypeEnum.String;
        case BinaryTypeEnum.ObjectUser:
          obj = (object) input.ReadString();
          num = input.ReadInt32();
          goto case BinaryTypeEnum.String;
        default:
          throw new SerializationException(Environment.GetResourceString("Serialization_TypeRead", (object) binaryTypeEnum.ToString()));
      }
    }

    [SecurityCritical]
    internal static void TypeFromInfo(BinaryTypeEnum binaryTypeEnum, object typeInformation, ObjectReader objectReader, BinaryAssemblyInfo assemblyInfo, out InternalPrimitiveTypeE primitiveTypeEnum, out string typeString, out Type type, out bool isVariant)
    {
      isVariant = false;
      primitiveTypeEnum = InternalPrimitiveTypeE.Invalid;
      typeString = (string) null;
      type = (Type) null;
      switch (binaryTypeEnum)
      {
        case BinaryTypeEnum.Primitive:
          primitiveTypeEnum = (InternalPrimitiveTypeE) typeInformation;
          typeString = Converter.ToComType(primitiveTypeEnum);
          type = Converter.ToType(primitiveTypeEnum);
          break;
        case BinaryTypeEnum.String:
          type = Converter.typeofString;
          break;
        case BinaryTypeEnum.Object:
          type = Converter.typeofObject;
          isVariant = true;
          break;
        case BinaryTypeEnum.ObjectUrt:
        case BinaryTypeEnum.ObjectUser:
          if (typeInformation == null)
            break;
          typeString = typeInformation.ToString();
          type = objectReader.GetType(assemblyInfo, typeString);
          if ((object) type != (object) Converter.typeofObject)
            break;
          isVariant = true;
          break;
        case BinaryTypeEnum.ObjectArray:
          type = Converter.typeofObjectArray;
          break;
        case BinaryTypeEnum.StringArray:
          type = Converter.typeofStringArray;
          break;
        case BinaryTypeEnum.PrimitiveArray:
          primitiveTypeEnum = (InternalPrimitiveTypeE) typeInformation;
          type = Converter.ToArrayType(primitiveTypeEnum);
          break;
        default:
          throw new SerializationException(Environment.GetResourceString("Serialization_TypeRead", (object) binaryTypeEnum.ToString()));
      }
    }
  }
}
