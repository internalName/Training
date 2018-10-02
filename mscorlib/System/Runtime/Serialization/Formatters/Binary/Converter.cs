// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.Converter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class Converter
  {
    private static int primitiveTypeEnumLength = 17;
    internal static Type typeofISerializable = typeof (ISerializable);
    internal static Type typeofString = typeof (string);
    internal static Type typeofConverter = typeof (Converter);
    internal static Type typeofBoolean = typeof (bool);
    internal static Type typeofByte = typeof (byte);
    internal static Type typeofChar = typeof (char);
    internal static Type typeofDecimal = typeof (Decimal);
    internal static Type typeofDouble = typeof (double);
    internal static Type typeofInt16 = typeof (short);
    internal static Type typeofInt32 = typeof (int);
    internal static Type typeofInt64 = typeof (long);
    internal static Type typeofSByte = typeof (sbyte);
    internal static Type typeofSingle = typeof (float);
    internal static Type typeofTimeSpan = typeof (TimeSpan);
    internal static Type typeofDateTime = typeof (DateTime);
    internal static Type typeofUInt16 = typeof (ushort);
    internal static Type typeofUInt32 = typeof (uint);
    internal static Type typeofUInt64 = typeof (ulong);
    internal static Type typeofObject = typeof (object);
    internal static Type typeofSystemVoid = typeof (void);
    internal static Assembly urtAssembly = Assembly.GetAssembly(Converter.typeofString);
    internal static string urtAssemblyString = Converter.urtAssembly.FullName;
    internal static Type typeofTypeArray = typeof (Type[]);
    internal static Type typeofObjectArray = typeof (object[]);
    internal static Type typeofStringArray = typeof (string[]);
    internal static Type typeofBooleanArray = typeof (bool[]);
    internal static Type typeofByteArray = typeof (byte[]);
    internal static Type typeofCharArray = typeof (char[]);
    internal static Type typeofDecimalArray = typeof (Decimal[]);
    internal static Type typeofDoubleArray = typeof (double[]);
    internal static Type typeofInt16Array = typeof (short[]);
    internal static Type typeofInt32Array = typeof (int[]);
    internal static Type typeofInt64Array = typeof (long[]);
    internal static Type typeofSByteArray = typeof (sbyte[]);
    internal static Type typeofSingleArray = typeof (float[]);
    internal static Type typeofTimeSpanArray = typeof (TimeSpan[]);
    internal static Type typeofDateTimeArray = typeof (DateTime[]);
    internal static Type typeofUInt16Array = typeof (ushort[]);
    internal static Type typeofUInt32Array = typeof (uint[]);
    internal static Type typeofUInt64Array = typeof (ulong[]);
    internal static Type typeofMarshalByRefObject = typeof (MarshalByRefObject);
    private static volatile Type[] typeA;
    private static volatile Type[] arrayTypeA;
    private static volatile string[] valueA;
    private static volatile TypeCode[] typeCodeA;
    private static volatile InternalPrimitiveTypeE[] codeA;

    private Converter()
    {
    }

    internal static InternalPrimitiveTypeE ToCode(Type type)
    {
      return (object) type == null || type.IsPrimitive ? Converter.ToPrimitiveTypeEnum(Type.GetTypeCode(type)) : ((object) type != (object) Converter.typeofDateTime ? ((object) type != (object) Converter.typeofTimeSpan ? ((object) type != (object) Converter.typeofDecimal ? InternalPrimitiveTypeE.Invalid : InternalPrimitiveTypeE.Decimal) : InternalPrimitiveTypeE.TimeSpan) : InternalPrimitiveTypeE.DateTime);
    }

    internal static bool IsWriteAsByteArray(InternalPrimitiveTypeE code)
    {
      bool flag = false;
      switch (code)
      {
        case InternalPrimitiveTypeE.Boolean:
        case InternalPrimitiveTypeE.Byte:
        case InternalPrimitiveTypeE.Char:
        case InternalPrimitiveTypeE.Double:
        case InternalPrimitiveTypeE.Int16:
        case InternalPrimitiveTypeE.Int32:
        case InternalPrimitiveTypeE.Int64:
        case InternalPrimitiveTypeE.SByte:
        case InternalPrimitiveTypeE.Single:
        case InternalPrimitiveTypeE.UInt16:
        case InternalPrimitiveTypeE.UInt32:
        case InternalPrimitiveTypeE.UInt64:
          flag = true;
          break;
      }
      return flag;
    }

    internal static int TypeLength(InternalPrimitiveTypeE code)
    {
      int num = 0;
      switch (code)
      {
        case InternalPrimitiveTypeE.Boolean:
          num = 1;
          break;
        case InternalPrimitiveTypeE.Byte:
          num = 1;
          break;
        case InternalPrimitiveTypeE.Char:
          num = 2;
          break;
        case InternalPrimitiveTypeE.Double:
          num = 8;
          break;
        case InternalPrimitiveTypeE.Int16:
          num = 2;
          break;
        case InternalPrimitiveTypeE.Int32:
          num = 4;
          break;
        case InternalPrimitiveTypeE.Int64:
          num = 8;
          break;
        case InternalPrimitiveTypeE.SByte:
          num = 1;
          break;
        case InternalPrimitiveTypeE.Single:
          num = 4;
          break;
        case InternalPrimitiveTypeE.UInt16:
          num = 2;
          break;
        case InternalPrimitiveTypeE.UInt32:
          num = 4;
          break;
        case InternalPrimitiveTypeE.UInt64:
          num = 8;
          break;
      }
      return num;
    }

    internal static InternalNameSpaceE GetNameSpaceEnum(InternalPrimitiveTypeE code, Type type, WriteObjectInfo objectInfo, out string typeName)
    {
      InternalNameSpaceE internalNameSpaceE = InternalNameSpaceE.None;
      typeName = (string) null;
      switch (code)
      {
        case InternalPrimitiveTypeE.Boolean:
        case InternalPrimitiveTypeE.Byte:
        case InternalPrimitiveTypeE.Char:
        case InternalPrimitiveTypeE.Double:
        case InternalPrimitiveTypeE.Int16:
        case InternalPrimitiveTypeE.Int32:
        case InternalPrimitiveTypeE.Int64:
        case InternalPrimitiveTypeE.SByte:
        case InternalPrimitiveTypeE.Single:
        case InternalPrimitiveTypeE.TimeSpan:
        case InternalPrimitiveTypeE.DateTime:
        case InternalPrimitiveTypeE.UInt16:
        case InternalPrimitiveTypeE.UInt32:
        case InternalPrimitiveTypeE.UInt64:
          internalNameSpaceE = InternalNameSpaceE.XdrPrimitive;
          typeName = "System." + Converter.ToComType(code);
          break;
        case InternalPrimitiveTypeE.Decimal:
          internalNameSpaceE = InternalNameSpaceE.UrtSystem;
          typeName = "System." + Converter.ToComType(code);
          break;
      }
      if (internalNameSpaceE == InternalNameSpaceE.None && (object) type != null)
      {
        if ((object) type == (object) Converter.typeofString)
          internalNameSpaceE = InternalNameSpaceE.XdrString;
        else if (objectInfo == null)
        {
          typeName = type.FullName;
          internalNameSpaceE = !(type.Assembly == Converter.urtAssembly) ? InternalNameSpaceE.UrtUser : InternalNameSpaceE.UrtSystem;
        }
        else
        {
          typeName = objectInfo.GetTypeFullName();
          internalNameSpaceE = !objectInfo.GetAssemblyString().Equals(Converter.urtAssemblyString) ? InternalNameSpaceE.UrtUser : InternalNameSpaceE.UrtSystem;
        }
      }
      return internalNameSpaceE;
    }

    internal static Type ToArrayType(InternalPrimitiveTypeE code)
    {
      if (Converter.arrayTypeA == null)
        Converter.InitArrayTypeA();
      return Converter.arrayTypeA[(int) code];
    }

    private static void InitTypeA()
    {
      Type[] typeArray = new Type[Converter.primitiveTypeEnumLength];
      typeArray[0] = (Type) null;
      typeArray[1] = Converter.typeofBoolean;
      typeArray[2] = Converter.typeofByte;
      typeArray[3] = Converter.typeofChar;
      typeArray[5] = Converter.typeofDecimal;
      typeArray[6] = Converter.typeofDouble;
      typeArray[7] = Converter.typeofInt16;
      typeArray[8] = Converter.typeofInt32;
      typeArray[9] = Converter.typeofInt64;
      typeArray[10] = Converter.typeofSByte;
      typeArray[11] = Converter.typeofSingle;
      typeArray[12] = Converter.typeofTimeSpan;
      typeArray[13] = Converter.typeofDateTime;
      typeArray[14] = Converter.typeofUInt16;
      typeArray[15] = Converter.typeofUInt32;
      typeArray[16] = Converter.typeofUInt64;
      Converter.typeA = typeArray;
    }

    private static void InitArrayTypeA()
    {
      Type[] typeArray = new Type[Converter.primitiveTypeEnumLength];
      typeArray[0] = (Type) null;
      typeArray[1] = Converter.typeofBooleanArray;
      typeArray[2] = Converter.typeofByteArray;
      typeArray[3] = Converter.typeofCharArray;
      typeArray[5] = Converter.typeofDecimalArray;
      typeArray[6] = Converter.typeofDoubleArray;
      typeArray[7] = Converter.typeofInt16Array;
      typeArray[8] = Converter.typeofInt32Array;
      typeArray[9] = Converter.typeofInt64Array;
      typeArray[10] = Converter.typeofSByteArray;
      typeArray[11] = Converter.typeofSingleArray;
      typeArray[12] = Converter.typeofTimeSpanArray;
      typeArray[13] = Converter.typeofDateTimeArray;
      typeArray[14] = Converter.typeofUInt16Array;
      typeArray[15] = Converter.typeofUInt32Array;
      typeArray[16] = Converter.typeofUInt64Array;
      Converter.arrayTypeA = typeArray;
    }

    internal static Type ToType(InternalPrimitiveTypeE code)
    {
      if (Converter.typeA == null)
        Converter.InitTypeA();
      return Converter.typeA[(int) code];
    }

    internal static Array CreatePrimitiveArray(InternalPrimitiveTypeE code, int length)
    {
      Array array = (Array) null;
      switch (code)
      {
        case InternalPrimitiveTypeE.Boolean:
          array = (Array) new bool[length];
          break;
        case InternalPrimitiveTypeE.Byte:
          array = (Array) new byte[length];
          break;
        case InternalPrimitiveTypeE.Char:
          array = (Array) new char[length];
          break;
        case InternalPrimitiveTypeE.Decimal:
          array = (Array) new Decimal[length];
          break;
        case InternalPrimitiveTypeE.Double:
          array = (Array) new double[length];
          break;
        case InternalPrimitiveTypeE.Int16:
          array = (Array) new short[length];
          break;
        case InternalPrimitiveTypeE.Int32:
          array = (Array) new int[length];
          break;
        case InternalPrimitiveTypeE.Int64:
          array = (Array) new long[length];
          break;
        case InternalPrimitiveTypeE.SByte:
          array = (Array) new sbyte[length];
          break;
        case InternalPrimitiveTypeE.Single:
          array = (Array) new float[length];
          break;
        case InternalPrimitiveTypeE.TimeSpan:
          array = (Array) new TimeSpan[length];
          break;
        case InternalPrimitiveTypeE.DateTime:
          array = (Array) new DateTime[length];
          break;
        case InternalPrimitiveTypeE.UInt16:
          array = (Array) new ushort[length];
          break;
        case InternalPrimitiveTypeE.UInt32:
          array = (Array) new uint[length];
          break;
        case InternalPrimitiveTypeE.UInt64:
          array = (Array) new ulong[length];
          break;
      }
      return array;
    }

    internal static bool IsPrimitiveArray(Type type, out object typeInformation)
    {
      typeInformation = (object) null;
      bool flag = true;
      if ((object) type == (object) Converter.typeofBooleanArray)
        typeInformation = (object) InternalPrimitiveTypeE.Boolean;
      else if ((object) type == (object) Converter.typeofByteArray)
        typeInformation = (object) InternalPrimitiveTypeE.Byte;
      else if ((object) type == (object) Converter.typeofCharArray)
        typeInformation = (object) InternalPrimitiveTypeE.Char;
      else if ((object) type == (object) Converter.typeofDoubleArray)
        typeInformation = (object) InternalPrimitiveTypeE.Double;
      else if ((object) type == (object) Converter.typeofInt16Array)
        typeInformation = (object) InternalPrimitiveTypeE.Int16;
      else if ((object) type == (object) Converter.typeofInt32Array)
        typeInformation = (object) InternalPrimitiveTypeE.Int32;
      else if ((object) type == (object) Converter.typeofInt64Array)
        typeInformation = (object) InternalPrimitiveTypeE.Int64;
      else if ((object) type == (object) Converter.typeofSByteArray)
        typeInformation = (object) InternalPrimitiveTypeE.SByte;
      else if ((object) type == (object) Converter.typeofSingleArray)
        typeInformation = (object) InternalPrimitiveTypeE.Single;
      else if ((object) type == (object) Converter.typeofUInt16Array)
        typeInformation = (object) InternalPrimitiveTypeE.UInt16;
      else if ((object) type == (object) Converter.typeofUInt32Array)
        typeInformation = (object) InternalPrimitiveTypeE.UInt32;
      else if ((object) type == (object) Converter.typeofUInt64Array)
        typeInformation = (object) InternalPrimitiveTypeE.UInt64;
      else
        flag = false;
      return flag;
    }

    private static void InitValueA()
    {
      string[] strArray = new string[Converter.primitiveTypeEnumLength];
      strArray[0] = (string) null;
      strArray[1] = "Boolean";
      strArray[2] = "Byte";
      strArray[3] = "Char";
      strArray[5] = "Decimal";
      strArray[6] = "Double";
      strArray[7] = "Int16";
      strArray[8] = "Int32";
      strArray[9] = "Int64";
      strArray[10] = "SByte";
      strArray[11] = "Single";
      strArray[12] = "TimeSpan";
      strArray[13] = "DateTime";
      strArray[14] = "UInt16";
      strArray[15] = "UInt32";
      strArray[16] = "UInt64";
      Converter.valueA = strArray;
    }

    internal static string ToComType(InternalPrimitiveTypeE code)
    {
      if (Converter.valueA == null)
        Converter.InitValueA();
      return Converter.valueA[(int) code];
    }

    private static void InitTypeCodeA()
    {
      TypeCode[] typeCodeArray = new TypeCode[Converter.primitiveTypeEnumLength];
      typeCodeArray[0] = TypeCode.Object;
      typeCodeArray[1] = TypeCode.Boolean;
      typeCodeArray[2] = TypeCode.Byte;
      typeCodeArray[3] = TypeCode.Char;
      typeCodeArray[5] = TypeCode.Decimal;
      typeCodeArray[6] = TypeCode.Double;
      typeCodeArray[7] = TypeCode.Int16;
      typeCodeArray[8] = TypeCode.Int32;
      typeCodeArray[9] = TypeCode.Int64;
      typeCodeArray[10] = TypeCode.SByte;
      typeCodeArray[11] = TypeCode.Single;
      typeCodeArray[12] = TypeCode.Object;
      typeCodeArray[13] = TypeCode.DateTime;
      typeCodeArray[14] = TypeCode.UInt16;
      typeCodeArray[15] = TypeCode.UInt32;
      typeCodeArray[16] = TypeCode.UInt64;
      Converter.typeCodeA = typeCodeArray;
    }

    internal static TypeCode ToTypeCode(InternalPrimitiveTypeE code)
    {
      if (Converter.typeCodeA == null)
        Converter.InitTypeCodeA();
      return Converter.typeCodeA[(int) code];
    }

    private static void InitCodeA()
    {
      Converter.codeA = new InternalPrimitiveTypeE[19]
      {
        InternalPrimitiveTypeE.Invalid,
        InternalPrimitiveTypeE.Invalid,
        InternalPrimitiveTypeE.Invalid,
        InternalPrimitiveTypeE.Boolean,
        InternalPrimitiveTypeE.Char,
        InternalPrimitiveTypeE.SByte,
        InternalPrimitiveTypeE.Byte,
        InternalPrimitiveTypeE.Int16,
        InternalPrimitiveTypeE.UInt16,
        InternalPrimitiveTypeE.Int32,
        InternalPrimitiveTypeE.UInt32,
        InternalPrimitiveTypeE.Int64,
        InternalPrimitiveTypeE.UInt64,
        InternalPrimitiveTypeE.Single,
        InternalPrimitiveTypeE.Double,
        InternalPrimitiveTypeE.Decimal,
        InternalPrimitiveTypeE.DateTime,
        InternalPrimitiveTypeE.Invalid,
        InternalPrimitiveTypeE.Invalid
      };
    }

    internal static InternalPrimitiveTypeE ToPrimitiveTypeEnum(TypeCode typeCode)
    {
      if (Converter.codeA == null)
        Converter.InitCodeA();
      return Converter.codeA[(int) typeCode];
    }

    internal static object FromString(string value, InternalPrimitiveTypeE code)
    {
      return code == InternalPrimitiveTypeE.Invalid ? (object) value : Convert.ChangeType((object) value, Converter.ToTypeCode(code), (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }
}
