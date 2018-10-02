// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.CLRIPropertyValueImpl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal class CLRIPropertyValueImpl : IPropertyValue
  {
    private PropertyType _type;
    private object _data;
    private static volatile Tuple<System.Type, PropertyType>[] s_numericScalarTypes;

    internal CLRIPropertyValueImpl(PropertyType type, object data)
    {
      this._type = type;
      this._data = data;
    }

    private static Tuple<System.Type, PropertyType>[] NumericScalarTypes
    {
      get
      {
        if (CLRIPropertyValueImpl.s_numericScalarTypes == null)
          CLRIPropertyValueImpl.s_numericScalarTypes = new Tuple<System.Type, PropertyType>[9]
          {
            new Tuple<System.Type, PropertyType>(typeof (byte), PropertyType.UInt8),
            new Tuple<System.Type, PropertyType>(typeof (short), PropertyType.Int16),
            new Tuple<System.Type, PropertyType>(typeof (ushort), PropertyType.UInt16),
            new Tuple<System.Type, PropertyType>(typeof (int), PropertyType.Int32),
            new Tuple<System.Type, PropertyType>(typeof (uint), PropertyType.UInt32),
            new Tuple<System.Type, PropertyType>(typeof (long), PropertyType.Int64),
            new Tuple<System.Type, PropertyType>(typeof (ulong), PropertyType.UInt64),
            new Tuple<System.Type, PropertyType>(typeof (float), PropertyType.Single),
            new Tuple<System.Type, PropertyType>(typeof (double), PropertyType.Double)
          };
        return CLRIPropertyValueImpl.s_numericScalarTypes;
      }
    }

    public PropertyType Type
    {
      get
      {
        return this._type;
      }
    }

    public bool IsNumericScalar
    {
      get
      {
        return CLRIPropertyValueImpl.IsNumericScalarImpl(this._type, this._data);
      }
    }

    public override string ToString()
    {
      if (this._data != null)
        return this._data.ToString();
      return base.ToString();
    }

    public byte GetUInt8()
    {
      return this.CoerceScalarValue<byte>(PropertyType.UInt8);
    }

    public short GetInt16()
    {
      return this.CoerceScalarValue<short>(PropertyType.Int16);
    }

    public ushort GetUInt16()
    {
      return this.CoerceScalarValue<ushort>(PropertyType.UInt16);
    }

    public int GetInt32()
    {
      return this.CoerceScalarValue<int>(PropertyType.Int32);
    }

    public uint GetUInt32()
    {
      return this.CoerceScalarValue<uint>(PropertyType.UInt32);
    }

    public long GetInt64()
    {
      return this.CoerceScalarValue<long>(PropertyType.Int64);
    }

    public ulong GetUInt64()
    {
      return this.CoerceScalarValue<ulong>(PropertyType.UInt64);
    }

    public float GetSingle()
    {
      return this.CoerceScalarValue<float>(PropertyType.Single);
    }

    public double GetDouble()
    {
      return this.CoerceScalarValue<double>(PropertyType.Double);
    }

    public char GetChar16()
    {
      if (this.Type != PropertyType.Char16)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "Char16"), -2147316576);
      return (char) this._data;
    }

    public bool GetBoolean()
    {
      if (this.Type != PropertyType.Boolean)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "Boolean"), -2147316576);
      return (bool) this._data;
    }

    public string GetString()
    {
      return this.CoerceScalarValue<string>(PropertyType.String);
    }

    public object GetInspectable()
    {
      if (this.Type != PropertyType.Inspectable)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "Inspectable"), -2147316576);
      return this._data;
    }

    public Guid GetGuid()
    {
      return this.CoerceScalarValue<Guid>(PropertyType.Guid);
    }

    public DateTimeOffset GetDateTime()
    {
      if (this.Type != PropertyType.DateTime)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "DateTime"), -2147316576);
      return (DateTimeOffset) this._data;
    }

    public TimeSpan GetTimeSpan()
    {
      if (this.Type != PropertyType.TimeSpan)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "TimeSpan"), -2147316576);
      return (TimeSpan) this._data;
    }

    [SecuritySafeCritical]
    public Point GetPoint()
    {
      if (this.Type != PropertyType.Point)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "Point"), -2147316576);
      return this.Unbox<Point>(IReferenceFactory.s_pointType);
    }

    [SecuritySafeCritical]
    public Size GetSize()
    {
      if (this.Type != PropertyType.Size)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "Size"), -2147316576);
      return this.Unbox<Size>(IReferenceFactory.s_sizeType);
    }

    [SecuritySafeCritical]
    public Rect GetRect()
    {
      if (this.Type != PropertyType.Rect)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "Rect"), -2147316576);
      return this.Unbox<Rect>(IReferenceFactory.s_rectType);
    }

    public byte[] GetUInt8Array()
    {
      return this.CoerceArrayValue<byte>(PropertyType.UInt8Array);
    }

    public short[] GetInt16Array()
    {
      return this.CoerceArrayValue<short>(PropertyType.Int16Array);
    }

    public ushort[] GetUInt16Array()
    {
      return this.CoerceArrayValue<ushort>(PropertyType.UInt16Array);
    }

    public int[] GetInt32Array()
    {
      return this.CoerceArrayValue<int>(PropertyType.Int32Array);
    }

    public uint[] GetUInt32Array()
    {
      return this.CoerceArrayValue<uint>(PropertyType.UInt32Array);
    }

    public long[] GetInt64Array()
    {
      return this.CoerceArrayValue<long>(PropertyType.Int64Array);
    }

    public ulong[] GetUInt64Array()
    {
      return this.CoerceArrayValue<ulong>(PropertyType.UInt64Array);
    }

    public float[] GetSingleArray()
    {
      return this.CoerceArrayValue<float>(PropertyType.SingleArray);
    }

    public double[] GetDoubleArray()
    {
      return this.CoerceArrayValue<double>(PropertyType.DoubleArray);
    }

    public char[] GetChar16Array()
    {
      if (this.Type != PropertyType.Char16Array)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "Char16[]"), -2147316576);
      return (char[]) this._data;
    }

    public bool[] GetBooleanArray()
    {
      if (this.Type != PropertyType.BooleanArray)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "Boolean[]"), -2147316576);
      return (bool[]) this._data;
    }

    public string[] GetStringArray()
    {
      return this.CoerceArrayValue<string>(PropertyType.StringArray);
    }

    public object[] GetInspectableArray()
    {
      if (this.Type != PropertyType.InspectableArray)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "Inspectable[]"), -2147316576);
      return (object[]) this._data;
    }

    public Guid[] GetGuidArray()
    {
      return this.CoerceArrayValue<Guid>(PropertyType.GuidArray);
    }

    public DateTimeOffset[] GetDateTimeArray()
    {
      if (this.Type != PropertyType.DateTimeArray)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "DateTimeOffset[]"), -2147316576);
      return (DateTimeOffset[]) this._data;
    }

    public TimeSpan[] GetTimeSpanArray()
    {
      if (this.Type != PropertyType.TimeSpanArray)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "TimeSpan[]"), -2147316576);
      return (TimeSpan[]) this._data;
    }

    [SecuritySafeCritical]
    public Point[] GetPointArray()
    {
      if (this.Type != PropertyType.PointArray)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "Point[]"), -2147316576);
      return this.UnboxArray<Point>(IReferenceFactory.s_pointType);
    }

    [SecuritySafeCritical]
    public Size[] GetSizeArray()
    {
      if (this.Type != PropertyType.SizeArray)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "Size[]"), -2147316576);
      return this.UnboxArray<Size>(IReferenceFactory.s_sizeType);
    }

    [SecuritySafeCritical]
    public Rect[] GetRectArray()
    {
      if (this.Type != PropertyType.RectArray)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) "Rect[]"), -2147316576);
      return this.UnboxArray<Rect>(IReferenceFactory.s_rectType);
    }

    private T[] CoerceArrayValue<T>(PropertyType unboxType)
    {
      if (this.Type == unboxType)
        return (T[]) this._data;
      Array data = this._data as Array;
      if (data == null)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this.Type, (object) typeof (T).MakeArrayType().Name), -2147316576);
      PropertyType type = this.Type - 1024;
      T[] objArray = new T[data.Length];
      for (int index = 0; index < data.Length; ++index)
      {
        try
        {
          objArray[index] = CLRIPropertyValueImpl.CoerceScalarValue<T>(type, data.GetValue(index));
        }
        catch (InvalidCastException ex)
        {
          Exception exception = (Exception) new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueArrayCoersion", (object) this.Type, (object) typeof (T).MakeArrayType().Name, (object) index, (object) ex.Message), (Exception) ex);
          exception.SetErrorCode(ex._HResult);
          throw exception;
        }
      }
      return objArray;
    }

    private T CoerceScalarValue<T>(PropertyType unboxType)
    {
      if (this.Type == unboxType)
        return (T) this._data;
      return CLRIPropertyValueImpl.CoerceScalarValue<T>(this.Type, this._data);
    }

    private static T CoerceScalarValue<T>(PropertyType type, object value)
    {
      if (!CLRIPropertyValueImpl.IsCoercable(type, value) && type != PropertyType.Inspectable)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) type, (object) typeof (T).Name), -2147316576);
      try
      {
        if (type == PropertyType.String && typeof (T) == typeof (Guid))
          return (T) (ValueType) Guid.Parse((string) value);
        if (type == PropertyType.Guid && typeof (T) == typeof (string))
          return (T) ((Guid) value).ToString("D", (IFormatProvider) CultureInfo.InvariantCulture);
        foreach (Tuple<System.Type, PropertyType> numericScalarType in CLRIPropertyValueImpl.NumericScalarTypes)
        {
          if (numericScalarType.Item1 == typeof (T))
            return (T) Convert.ChangeType(value, typeof (T), (IFormatProvider) CultureInfo.InvariantCulture);
        }
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) type, (object) typeof (T).Name), -2147316576);
      }
      catch (InvalidCastException ex)
      {
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) type, (object) typeof (T).Name), -2147316576);
      }
      catch (OverflowException ex)
      {
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueCoersion", (object) type, value, (object) typeof (T).Name), -2147352566);
      }
      IPropertyValue propertyValue = value as IPropertyValue;
      if (type == PropertyType.Inspectable && propertyValue != null)
      {
        if (typeof (T) == typeof (byte))
          return (T) (ValueType) propertyValue.GetUInt8();
        if (typeof (T) == typeof (short))
          return (T) (ValueType) propertyValue.GetInt16();
        if (typeof (T) == typeof (ushort))
          return (T) (ValueType) propertyValue.GetUInt16();
        if (typeof (T) == typeof (int))
          return (T) (ValueType) propertyValue.GetUInt32();
        if (typeof (T) == typeof (uint))
          return (T) (ValueType) propertyValue.GetUInt32();
        if (typeof (T) == typeof (long))
          return (T) (ValueType) propertyValue.GetInt64();
        if (typeof (T) == typeof (ulong))
          return (T) (ValueType) propertyValue.GetUInt64();
        if (typeof (T) == typeof (float))
          return (T) (ValueType) propertyValue.GetSingle();
        if (typeof (T) == typeof (double))
          return (T) (ValueType) propertyValue.GetDouble();
      }
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) type, (object) typeof (T).Name), -2147316576);
    }

    private static bool IsCoercable(PropertyType type, object data)
    {
      if (type == PropertyType.Guid || type == PropertyType.String)
        return true;
      return CLRIPropertyValueImpl.IsNumericScalarImpl(type, data);
    }

    private static bool IsNumericScalarImpl(PropertyType type, object data)
    {
      if (data.GetType().IsEnum)
        return true;
      foreach (Tuple<System.Type, PropertyType> numericScalarType in CLRIPropertyValueImpl.NumericScalarTypes)
      {
        if (numericScalarType.Item2 == type)
          return true;
      }
      return false;
    }

    [SecurityCritical]
    private unsafe T Unbox<T>(System.Type expectedBoxedType) where T : struct
    {
      if (this._data.GetType() != expectedBoxedType)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this._data.GetType(), (object) expectedBoxedType.Name), -2147316576);
      T instance = Activator.CreateInstance<T>();
      fixed (byte* src = &JitHelpers.GetPinningHelper(this._data).m_data)
        Buffer.Memcpy((byte*) (void*) JitHelpers.UnsafeCastToStackPointer<T>(ref instance), src, Marshal.SizeOf<T>(instance));
      return instance;
    }

    [SecurityCritical]
    private unsafe T[] UnboxArray<T>(System.Type expectedArrayElementType) where T : struct
    {
      Array data = this._data as Array;
      if (data == null || this._data.GetType().GetElementType() != expectedArrayElementType)
        throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", (object) this._data.GetType(), (object) expectedArrayElementType.MakeArrayType().Name), -2147316576);
      T[] arr = new T[data.Length];
      if (arr.Length != 0)
      {
        fixed (byte* numPtr1 = &JitHelpers.GetPinningHelper((object) data).m_data)
          fixed (byte* numPtr2 = &JitHelpers.GetPinningHelper((object) arr).m_data)
          {
            byte* src = (byte*) (void*) Marshal.UnsafeAddrOfPinnedArrayElement(data, 0);
            Buffer.Memcpy((byte*) (void*) Marshal.UnsafeAddrOfPinnedArrayElement<T>(arr, 0), src, checked (Marshal.SizeOf(typeof (T)) * arr.Length));
          }
      }
      return arr;
    }
  }
}
