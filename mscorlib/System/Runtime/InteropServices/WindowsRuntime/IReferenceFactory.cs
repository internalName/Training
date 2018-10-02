// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IReferenceFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal static class IReferenceFactory
  {
    internal static readonly Type s_pointType = Type.GetType("Windows.Foundation.Point, System.Runtime.WindowsRuntime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
    internal static readonly Type s_rectType = Type.GetType("Windows.Foundation.Rect, System.Runtime.WindowsRuntime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
    internal static readonly Type s_sizeType = Type.GetType("Windows.Foundation.Size, System.Runtime.WindowsRuntime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");

    [SecuritySafeCritical]
    internal static object CreateIReference(object obj)
    {
      Type type = obj.GetType();
      if (type.IsArray)
        return IReferenceFactory.CreateIReferenceArray((Array) obj);
      if (type == typeof (int))
        return (object) new CLRIReferenceImpl<int>(PropertyType.Int32, (int) obj);
      if (type == typeof (string))
        return (object) new CLRIReferenceImpl<string>(PropertyType.String, (string) obj);
      if (type == typeof (byte))
        return (object) new CLRIReferenceImpl<byte>(PropertyType.UInt8, (byte) obj);
      if (type == typeof (short))
        return (object) new CLRIReferenceImpl<short>(PropertyType.Int16, (short) obj);
      if (type == typeof (ushort))
        return (object) new CLRIReferenceImpl<ushort>(PropertyType.UInt16, (ushort) obj);
      if (type == typeof (uint))
        return (object) new CLRIReferenceImpl<uint>(PropertyType.UInt32, (uint) obj);
      if (type == typeof (long))
        return (object) new CLRIReferenceImpl<long>(PropertyType.Int64, (long) obj);
      if (type == typeof (ulong))
        return (object) new CLRIReferenceImpl<ulong>(PropertyType.UInt64, (ulong) obj);
      if (type == typeof (float))
        return (object) new CLRIReferenceImpl<float>(PropertyType.Single, (float) obj);
      if (type == typeof (double))
        return (object) new CLRIReferenceImpl<double>(PropertyType.Double, (double) obj);
      if (type == typeof (char))
        return (object) new CLRIReferenceImpl<char>(PropertyType.Char16, (char) obj);
      if (type == typeof (bool))
        return (object) new CLRIReferenceImpl<bool>(PropertyType.Boolean, (bool) obj);
      if (type == typeof (Guid))
        return (object) new CLRIReferenceImpl<Guid>(PropertyType.Guid, (Guid) obj);
      if (type == typeof (DateTimeOffset))
        return (object) new CLRIReferenceImpl<DateTimeOffset>(PropertyType.DateTime, (DateTimeOffset) obj);
      if (type == typeof (TimeSpan))
        return (object) new CLRIReferenceImpl<TimeSpan>(PropertyType.TimeSpan, (TimeSpan) obj);
      if (type == typeof (object))
        return (object) new CLRIReferenceImpl<object>(PropertyType.Inspectable, obj);
      if (type == typeof (RuntimeType))
        return (object) new CLRIReferenceImpl<Type>(PropertyType.Other, (Type) obj);
      PropertyType? nullable = new PropertyType?();
      if (type == IReferenceFactory.s_pointType)
        nullable = new PropertyType?(PropertyType.Point);
      else if (type == IReferenceFactory.s_rectType)
        nullable = new PropertyType?(PropertyType.Rect);
      else if (type == IReferenceFactory.s_sizeType)
        nullable = new PropertyType?(PropertyType.Size);
      else if (type.IsValueType || (object) (obj as Delegate) != null)
        nullable = new PropertyType?(PropertyType.Other);
      if (!nullable.HasValue)
        return (object) null;
      return Activator.CreateInstance(typeof (CLRIReferenceImpl<>).MakeGenericType(type), new object[2]
      {
        (object) nullable.Value,
        obj
      });
    }

    [SecuritySafeCritical]
    internal static object CreateIReferenceArray(Array obj)
    {
      Type elementType = obj.GetType().GetElementType();
      if (elementType == typeof (int))
        return (object) new CLRIReferenceArrayImpl<int>(PropertyType.Int32Array, (int[]) obj);
      if (elementType == typeof (string))
        return (object) new CLRIReferenceArrayImpl<string>(PropertyType.StringArray, (string[]) obj);
      if (elementType == typeof (byte))
        return (object) new CLRIReferenceArrayImpl<byte>(PropertyType.UInt8Array, (byte[]) obj);
      if (elementType == typeof (short))
        return (object) new CLRIReferenceArrayImpl<short>(PropertyType.Int16Array, (short[]) obj);
      if (elementType == typeof (ushort))
        return (object) new CLRIReferenceArrayImpl<ushort>(PropertyType.UInt16Array, (ushort[]) obj);
      if (elementType == typeof (uint))
        return (object) new CLRIReferenceArrayImpl<uint>(PropertyType.UInt32Array, (uint[]) obj);
      if (elementType == typeof (long))
        return (object) new CLRIReferenceArrayImpl<long>(PropertyType.Int64Array, (long[]) obj);
      if (elementType == typeof (ulong))
        return (object) new CLRIReferenceArrayImpl<ulong>(PropertyType.UInt64Array, (ulong[]) obj);
      if (elementType == typeof (float))
        return (object) new CLRIReferenceArrayImpl<float>(PropertyType.SingleArray, (float[]) obj);
      if (elementType == typeof (double))
        return (object) new CLRIReferenceArrayImpl<double>(PropertyType.DoubleArray, (double[]) obj);
      if (elementType == typeof (char))
        return (object) new CLRIReferenceArrayImpl<char>(PropertyType.Char16Array, (char[]) obj);
      if (elementType == typeof (bool))
        return (object) new CLRIReferenceArrayImpl<bool>(PropertyType.BooleanArray, (bool[]) obj);
      if (elementType == typeof (Guid))
        return (object) new CLRIReferenceArrayImpl<Guid>(PropertyType.GuidArray, (Guid[]) obj);
      if (elementType == typeof (DateTimeOffset))
        return (object) new CLRIReferenceArrayImpl<DateTimeOffset>(PropertyType.DateTimeArray, (DateTimeOffset[]) obj);
      if (elementType == typeof (TimeSpan))
        return (object) new CLRIReferenceArrayImpl<TimeSpan>(PropertyType.TimeSpanArray, (TimeSpan[]) obj);
      if (elementType == typeof (Type))
        return (object) new CLRIReferenceArrayImpl<Type>(PropertyType.OtherArray, (Type[]) obj);
      PropertyType? nullable = new PropertyType?();
      if (elementType == IReferenceFactory.s_pointType)
        nullable = new PropertyType?(PropertyType.PointArray);
      else if (elementType == IReferenceFactory.s_rectType)
        nullable = new PropertyType?(PropertyType.RectArray);
      else if (elementType == IReferenceFactory.s_sizeType)
        nullable = new PropertyType?(PropertyType.SizeArray);
      else if (elementType.IsValueType)
      {
        if (elementType.IsGenericType && elementType.GetGenericTypeDefinition() == typeof (KeyValuePair<,>))
        {
          object[] objArray = new object[obj.Length];
          for (int index = 0; index < objArray.Length; ++index)
            objArray[index] = obj.GetValue(index);
          obj = (Array) objArray;
        }
        else
          nullable = new PropertyType?(PropertyType.OtherArray);
      }
      else if (typeof (Delegate).IsAssignableFrom(elementType))
        nullable = new PropertyType?(PropertyType.OtherArray);
      if (!nullable.HasValue)
        return (object) new CLRIReferenceArrayImpl<object>(PropertyType.InspectableArray, (object[]) obj);
      return Activator.CreateInstance(typeof (CLRIReferenceArrayImpl<>).MakeGenericType(elementType), new object[2]
      {
        (object) nullable.Value,
        (object) obj
      });
    }
  }
}
