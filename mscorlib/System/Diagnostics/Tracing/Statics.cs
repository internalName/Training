// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.Statics
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Diagnostics.Tracing
{
  internal static class Statics
  {
    public static readonly TraceLoggingDataType IntPtrType = IntPtr.Size == 8 ? TraceLoggingDataType.Int64 : TraceLoggingDataType.Int32;
    public static readonly TraceLoggingDataType UIntPtrType = IntPtr.Size == 8 ? TraceLoggingDataType.UInt64 : TraceLoggingDataType.UInt32;
    public static readonly TraceLoggingDataType HexIntPtrType = IntPtr.Size == 8 ? TraceLoggingDataType.HexInt64 : TraceLoggingDataType.HexInt32;
    public const byte DefaultLevel = 5;
    public const byte TraceLoggingChannel = 11;
    public const byte InTypeMask = 31;
    public const byte InTypeFixedCountFlag = 32;
    public const byte InTypeVariableCountFlag = 64;
    public const byte InTypeCustomCountFlag = 96;
    public const byte InTypeCountMask = 96;
    public const byte InTypeChainFlag = 128;
    public const byte OutTypeMask = 127;
    public const byte OutTypeChainFlag = 128;
    public const EventTags EventTagsMask = (EventTags) 268435455;

    public static byte[] MetadataForString(string name, int prefixSize, int suffixSize, int additionalSize)
    {
      Statics.CheckName(name);
      int length = Encoding.UTF8.GetByteCount(name) + 3 + prefixSize + suffixSize;
      byte[] bytes = new byte[length];
      ushort num = checked ((ushort) (length + additionalSize));
      bytes[0] = (byte) num;
      bytes[1] = (byte) ((uint) num >> 8);
      Encoding.UTF8.GetBytes(name, 0, name.Length, bytes, 2 + prefixSize);
      return bytes;
    }

    public static void EncodeTags(int tags, ref int pos, byte[] metadata)
    {
      int num1 = tags & 268435455;
      bool flag;
      do
      {
        byte num2 = (byte) (num1 >> 21 & (int) sbyte.MaxValue);
        flag = (uint) (num1 & 2097151) > 0U;
        byte num3 = (byte) ((int) num2 | (flag ? 128 : 0));
        num1 <<= 7;
        if (metadata != null)
          metadata[pos] = num3;
        ++pos;
      }
      while (flag);
    }

    public static byte Combine(int settingValue, byte defaultValue)
    {
      if ((int) (byte) settingValue != settingValue)
        return defaultValue;
      return (byte) settingValue;
    }

    public static byte Combine(int settingValue1, int settingValue2, byte defaultValue)
    {
      if ((int) (byte) settingValue1 == settingValue1)
        return (byte) settingValue1;
      if ((int) (byte) settingValue2 != settingValue2)
        return defaultValue;
      return (byte) settingValue2;
    }

    public static int Combine(int settingValue1, int settingValue2)
    {
      if ((int) (byte) settingValue1 != settingValue1)
        return settingValue2;
      return settingValue1;
    }

    public static void CheckName(string name)
    {
      if (name != null && 0 <= name.IndexOf(char.MinValue))
        throw new ArgumentOutOfRangeException(nameof (name));
    }

    public static bool ShouldOverrideFieldName(string fieldName)
    {
      if (fieldName.Length <= 2)
        return fieldName[0] == '_';
      return false;
    }

    public static TraceLoggingDataType MakeDataType(TraceLoggingDataType baseType, EventFieldFormat format)
    {
      return baseType & (TraceLoggingDataType) 31 | (TraceLoggingDataType) ((int) format << 8);
    }

    public static TraceLoggingDataType Format8(EventFieldFormat format, TraceLoggingDataType native)
    {
      switch (format)
      {
        case EventFieldFormat.Default:
          return native;
        case EventFieldFormat.String:
          return TraceLoggingDataType.Char8;
        case EventFieldFormat.Boolean:
          return TraceLoggingDataType.Boolean8;
        case EventFieldFormat.Hexadecimal:
          return TraceLoggingDataType.HexInt8;
        default:
          return Statics.MakeDataType(native, format);
      }
    }

    public static TraceLoggingDataType Format16(EventFieldFormat format, TraceLoggingDataType native)
    {
      switch (format)
      {
        case EventFieldFormat.Default:
          return native;
        case EventFieldFormat.String:
          return TraceLoggingDataType.Char16;
        case EventFieldFormat.Hexadecimal:
          return TraceLoggingDataType.HexInt16;
        default:
          return Statics.MakeDataType(native, format);
      }
    }

    public static TraceLoggingDataType Format32(EventFieldFormat format, TraceLoggingDataType native)
    {
      switch (format)
      {
        case EventFieldFormat.Default:
          return native;
        case EventFieldFormat.Boolean:
          return TraceLoggingDataType.Boolean32;
        case EventFieldFormat.Hexadecimal:
          return TraceLoggingDataType.HexInt32;
        case EventFieldFormat.HResult:
          return TraceLoggingDataType.HResult;
        default:
          return Statics.MakeDataType(native, format);
      }
    }

    public static TraceLoggingDataType Format64(EventFieldFormat format, TraceLoggingDataType native)
    {
      if (format == EventFieldFormat.Default)
        return native;
      if (format == EventFieldFormat.Hexadecimal)
        return TraceLoggingDataType.HexInt64;
      return Statics.MakeDataType(native, format);
    }

    public static TraceLoggingDataType FormatPtr(EventFieldFormat format, TraceLoggingDataType native)
    {
      if (format == EventFieldFormat.Default)
        return native;
      if (format == EventFieldFormat.Hexadecimal)
        return Statics.HexIntPtrType;
      return Statics.MakeDataType(native, format);
    }

    public static object CreateInstance(Type type, params object[] parameters)
    {
      return Activator.CreateInstance(type, parameters);
    }

    public static bool IsValueType(Type type)
    {
      return type.IsValueType;
    }

    public static bool IsEnum(Type type)
    {
      return type.IsEnum;
    }

    public static IEnumerable<PropertyInfo> GetProperties(Type type)
    {
      return (IEnumerable<PropertyInfo>) type.GetProperties();
    }

    public static MethodInfo GetGetMethod(PropertyInfo propInfo)
    {
      return propInfo.GetGetMethod();
    }

    public static MethodInfo GetDeclaredStaticMethod(Type declaringType, string name)
    {
      return declaringType.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
    }

    public static bool HasCustomAttribute(PropertyInfo propInfo, Type attributeType)
    {
      return (uint) propInfo.GetCustomAttributes(attributeType, false).Length > 0U;
    }

    public static AttributeType GetCustomAttribute<AttributeType>(PropertyInfo propInfo) where AttributeType : Attribute
    {
      AttributeType attributeType = default (AttributeType);
      object[] customAttributes = propInfo.GetCustomAttributes(typeof (AttributeType), false);
      if (customAttributes.Length != 0)
        attributeType = (AttributeType) customAttributes[0];
      return attributeType;
    }

    public static AttributeType GetCustomAttribute<AttributeType>(Type type) where AttributeType : Attribute
    {
      AttributeType attributeType = default (AttributeType);
      object[] customAttributes = type.GetCustomAttributes(typeof (AttributeType), false);
      if (customAttributes.Length != 0)
        attributeType = (AttributeType) customAttributes[0];
      return attributeType;
    }

    public static Type[] GetGenericArguments(Type type)
    {
      return type.GetGenericArguments();
    }

    public static Type FindEnumerableElementType(Type type)
    {
      Type type1 = (Type) null;
      if (Statics.IsGenericMatch(type, (object) typeof (IEnumerable<>)))
      {
        type1 = Statics.GetGenericArguments(type)[0];
      }
      else
      {
        foreach (Type type2 in type.FindInterfaces(new TypeFilter(Statics.IsGenericMatch), (object) typeof (IEnumerable<>)))
        {
          if (type1 != (Type) null)
          {
            type1 = (Type) null;
            break;
          }
          type1 = Statics.GetGenericArguments(type2)[0];
        }
      }
      return type1;
    }

    public static bool IsGenericMatch(Type type, object openType)
    {
      if (type.IsGenericType)
        return type.GetGenericTypeDefinition() == (Type) openType;
      return false;
    }

    public static Delegate CreateDelegate(Type delegateType, MethodInfo methodInfo)
    {
      return Delegate.CreateDelegate(delegateType, methodInfo);
    }

    public static TraceLoggingTypeInfo GetTypeInfoInstance(Type dataType, List<Type> recursionCheck)
    {
      TraceLoggingTypeInfo traceLoggingTypeInfo;
      if (dataType == typeof (int))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) TraceLoggingTypeInfo<int>.Instance;
      else if (dataType == typeof (long))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) TraceLoggingTypeInfo<long>.Instance;
      else if (dataType == typeof (string))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) TraceLoggingTypeInfo<string>.Instance;
      else
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) Statics.GetDeclaredStaticMethod(typeof (TraceLoggingTypeInfo<>).MakeGenericType(dataType), "GetInstance").Invoke((object) null, new object[1]
        {
          (object) recursionCheck
        });
      return traceLoggingTypeInfo;
    }

    public static TraceLoggingTypeInfo<DataType> CreateDefaultTypeInfo<DataType>(List<Type> recursionCheck)
    {
      Type type = typeof (DataType);
      if (recursionCheck.Contains(type))
        throw new NotSupportedException(Environment.GetResourceString("EventSource_RecursiveTypeDefinition"));
      recursionCheck.Add(type);
      EventDataAttribute customAttribute = Statics.GetCustomAttribute<EventDataAttribute>(type);
      TraceLoggingTypeInfo traceLoggingTypeInfo;
      if (customAttribute != null || Statics.GetCustomAttribute<CompilerGeneratedAttribute>(type) != null)
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new InvokeTypeInfo<DataType>(new TypeAnalysis(type, customAttribute, recursionCheck));
      else if (type.IsArray)
      {
        Type elementType = type.GetElementType();
        if (elementType == typeof (bool))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new BooleanArrayTypeInfo();
        else if (elementType == typeof (byte))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new ByteArrayTypeInfo();
        else if (elementType == typeof (sbyte))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new SByteArrayTypeInfo();
        else if (elementType == typeof (short))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new Int16ArrayTypeInfo();
        else if (elementType == typeof (ushort))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new UInt16ArrayTypeInfo();
        else if (elementType == typeof (int))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new Int32ArrayTypeInfo();
        else if (elementType == typeof (uint))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new UInt32ArrayTypeInfo();
        else if (elementType == typeof (long))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new Int64ArrayTypeInfo();
        else if (elementType == typeof (ulong))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new UInt64ArrayTypeInfo();
        else if (elementType == typeof (char))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new CharArrayTypeInfo();
        else if (elementType == typeof (double))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new DoubleArrayTypeInfo();
        else if (elementType == typeof (float))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new SingleArrayTypeInfo();
        else if (elementType == typeof (IntPtr))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new IntPtrArrayTypeInfo();
        else if (elementType == typeof (UIntPtr))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new UIntPtrArrayTypeInfo();
        else if (elementType == typeof (Guid))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new GuidArrayTypeInfo();
        else
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) Statics.CreateInstance(typeof (ArrayTypeInfo<>).MakeGenericType(elementType), (object) Statics.GetTypeInfoInstance(elementType, recursionCheck));
      }
      else if (Statics.IsEnum(type))
      {
        Type underlyingType = Enum.GetUnderlyingType(type);
        if (underlyingType == typeof (int))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new EnumInt32TypeInfo<DataType>();
        else if (underlyingType == typeof (uint))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new EnumUInt32TypeInfo<DataType>();
        else if (underlyingType == typeof (byte))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new EnumByteTypeInfo<DataType>();
        else if (underlyingType == typeof (sbyte))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new EnumSByteTypeInfo<DataType>();
        else if (underlyingType == typeof (short))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new EnumInt16TypeInfo<DataType>();
        else if (underlyingType == typeof (ushort))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new EnumUInt16TypeInfo<DataType>();
        else if (underlyingType == typeof (long))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new EnumInt64TypeInfo<DataType>();
        else if (underlyingType == typeof (ulong))
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) new EnumUInt64TypeInfo<DataType>();
        else
          throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedEnumType", (object) type.Name, (object) underlyingType.Name));
      }
      else if (type == typeof (string))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new StringTypeInfo();
      else if (type == typeof (bool))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new BooleanTypeInfo();
      else if (type == typeof (byte))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new ByteTypeInfo();
      else if (type == typeof (sbyte))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new SByteTypeInfo();
      else if (type == typeof (short))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new Int16TypeInfo();
      else if (type == typeof (ushort))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new UInt16TypeInfo();
      else if (type == typeof (int))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new Int32TypeInfo();
      else if (type == typeof (uint))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new UInt32TypeInfo();
      else if (type == typeof (long))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new Int64TypeInfo();
      else if (type == typeof (ulong))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new UInt64TypeInfo();
      else if (type == typeof (char))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new CharTypeInfo();
      else if (type == typeof (double))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new DoubleTypeInfo();
      else if (type == typeof (float))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new SingleTypeInfo();
      else if (type == typeof (DateTime))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new DateTimeTypeInfo();
      else if (type == typeof (Decimal))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new DecimalTypeInfo();
      else if (type == typeof (IntPtr))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new IntPtrTypeInfo();
      else if (type == typeof (UIntPtr))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new UIntPtrTypeInfo();
      else if (type == typeof (Guid))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new GuidTypeInfo();
      else if (type == typeof (TimeSpan))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new TimeSpanTypeInfo();
      else if (type == typeof (DateTimeOffset))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new DateTimeOffsetTypeInfo();
      else if (type == typeof (EmptyStruct))
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) new NullTypeInfo<EmptyStruct>();
      else if (Statics.IsGenericMatch(type, (object) typeof (KeyValuePair<,>)))
      {
        Type[] genericArguments = Statics.GetGenericArguments(type);
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) Statics.CreateInstance(typeof (KeyValuePairTypeInfo<,>).MakeGenericType(genericArguments[0], genericArguments[1]), (object) recursionCheck);
      }
      else if (Statics.IsGenericMatch(type, (object) typeof (Nullable<>)))
      {
        traceLoggingTypeInfo = (TraceLoggingTypeInfo) Statics.CreateInstance(typeof (NullableTypeInfo<>).MakeGenericType(Statics.GetGenericArguments(type)[0]), (object) recursionCheck);
      }
      else
      {
        Type enumerableElementType = Statics.FindEnumerableElementType(type);
        if (enumerableElementType != (Type) null)
          traceLoggingTypeInfo = (TraceLoggingTypeInfo) Statics.CreateInstance(typeof (EnumerableTypeInfo<,>).MakeGenericType(type, enumerableElementType), (object) Statics.GetTypeInfoInstance(enumerableElementType, recursionCheck));
        else
          throw new ArgumentException(Environment.GetResourceString("EventSource_NonCompliantTypeError", (object) type.Name));
      }
      return (TraceLoggingTypeInfo<DataType>) traceLoggingTypeInfo;
    }
  }
}
