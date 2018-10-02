// Decompiled with JetBrains decompiler
// Type: System.Variant
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;

namespace System
{
  [Serializable]
  internal struct Variant
  {
    internal static readonly Type[] ClassTypes = new Type[23]
    {
      typeof (System.Empty),
      typeof (void),
      typeof (bool),
      typeof (char),
      typeof (sbyte),
      typeof (byte),
      typeof (short),
      typeof (ushort),
      typeof (int),
      typeof (uint),
      typeof (long),
      typeof (ulong),
      typeof (float),
      typeof (double),
      typeof (string),
      typeof (void),
      typeof (DateTime),
      typeof (TimeSpan),
      typeof (object),
      typeof (Decimal),
      typeof (object),
      typeof (System.Reflection.Missing),
      typeof (System.DBNull)
    };
    internal static readonly Variant Empty = new Variant();
    internal static readonly Variant Missing = new Variant(22, Type.Missing, 0, 0);
    internal static readonly Variant DBNull = new Variant(23, (object) System.DBNull.Value, 0, 0);
    private object m_objref;
    private int m_data1;
    private int m_data2;
    private int m_flags;
    internal const int CV_EMPTY = 0;
    internal const int CV_VOID = 1;
    internal const int CV_BOOLEAN = 2;
    internal const int CV_CHAR = 3;
    internal const int CV_I1 = 4;
    internal const int CV_U1 = 5;
    internal const int CV_I2 = 6;
    internal const int CV_U2 = 7;
    internal const int CV_I4 = 8;
    internal const int CV_U4 = 9;
    internal const int CV_I8 = 10;
    internal const int CV_U8 = 11;
    internal const int CV_R4 = 12;
    internal const int CV_R8 = 13;
    internal const int CV_STRING = 14;
    internal const int CV_PTR = 15;
    internal const int CV_DATETIME = 16;
    internal const int CV_TIMESPAN = 17;
    internal const int CV_OBJECT = 18;
    internal const int CV_DECIMAL = 19;
    internal const int CV_ENUM = 21;
    internal const int CV_MISSING = 22;
    internal const int CV_NULL = 23;
    internal const int CV_LAST = 24;
    internal const int TypeCodeBitMask = 65535;
    internal const int VTBitMask = -16777216;
    internal const int VTBitShift = 24;
    internal const int ArrayBitMask = 65536;
    internal const int EnumI1 = 1048576;
    internal const int EnumU1 = 2097152;
    internal const int EnumI2 = 3145728;
    internal const int EnumU2 = 4194304;
    internal const int EnumI4 = 5242880;
    internal const int EnumU4 = 6291456;
    internal const int EnumI8 = 7340032;
    internal const int EnumU8 = 8388608;
    internal const int EnumMask = 15728640;

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern double GetR8FromVar();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern float GetR4FromVar();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern void SetFieldsR4(float val);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern void SetFieldsR8(double val);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern void SetFieldsObject(object val);

    internal long GetI8FromVar()
    {
      return (long) this.m_data2 << 32 | (long) this.m_data1 & (long) uint.MaxValue;
    }

    internal Variant(int flags, object or, int data1, int data2)
    {
      this.m_flags = flags;
      this.m_objref = or;
      this.m_data1 = data1;
      this.m_data2 = data2;
    }

    public Variant(bool val)
    {
      this.m_objref = (object) null;
      this.m_flags = 2;
      this.m_data1 = val ? 1 : 0;
      this.m_data2 = 0;
    }

    public Variant(sbyte val)
    {
      this.m_objref = (object) null;
      this.m_flags = 4;
      this.m_data1 = (int) val;
      this.m_data2 = (int) ((long) val >> 32);
    }

    public Variant(byte val)
    {
      this.m_objref = (object) null;
      this.m_flags = 5;
      this.m_data1 = (int) val;
      this.m_data2 = 0;
    }

    public Variant(short val)
    {
      this.m_objref = (object) null;
      this.m_flags = 6;
      this.m_data1 = (int) val;
      this.m_data2 = (int) ((long) val >> 32);
    }

    public Variant(ushort val)
    {
      this.m_objref = (object) null;
      this.m_flags = 7;
      this.m_data1 = (int) val;
      this.m_data2 = 0;
    }

    public Variant(char val)
    {
      this.m_objref = (object) null;
      this.m_flags = 3;
      this.m_data1 = (int) val;
      this.m_data2 = 0;
    }

    public Variant(int val)
    {
      this.m_objref = (object) null;
      this.m_flags = 8;
      this.m_data1 = val;
      this.m_data2 = val >> 31;
    }

    public Variant(uint val)
    {
      this.m_objref = (object) null;
      this.m_flags = 9;
      this.m_data1 = (int) val;
      this.m_data2 = 0;
    }

    public Variant(long val)
    {
      this.m_objref = (object) null;
      this.m_flags = 10;
      this.m_data1 = (int) val;
      this.m_data2 = (int) (val >> 32);
    }

    public Variant(ulong val)
    {
      this.m_objref = (object) null;
      this.m_flags = 11;
      this.m_data1 = (int) val;
      this.m_data2 = (int) (val >> 32);
    }

    [SecuritySafeCritical]
    public Variant(float val)
    {
      this.m_objref = (object) null;
      this.m_flags = 12;
      this.m_data1 = 0;
      this.m_data2 = 0;
      this.SetFieldsR4(val);
    }

    [SecurityCritical]
    public Variant(double val)
    {
      this.m_objref = (object) null;
      this.m_flags = 13;
      this.m_data1 = 0;
      this.m_data2 = 0;
      this.SetFieldsR8(val);
    }

    public Variant(DateTime val)
    {
      this.m_objref = (object) null;
      this.m_flags = 16;
      ulong ticks = (ulong) val.Ticks;
      this.m_data1 = (int) ticks;
      this.m_data2 = (int) (ticks >> 32);
    }

    public Variant(Decimal val)
    {
      this.m_objref = (object) val;
      this.m_flags = 19;
      this.m_data1 = 0;
      this.m_data2 = 0;
    }

    [SecuritySafeCritical]
    public Variant(object obj)
    {
      this.m_data1 = 0;
      this.m_data2 = 0;
      VarEnum varEnum = VarEnum.VT_EMPTY;
      if (obj is DateTime)
      {
        this.m_objref = (object) null;
        this.m_flags = 16;
        ulong ticks = (ulong) ((DateTime) obj).Ticks;
        this.m_data1 = (int) ticks;
        this.m_data2 = (int) (ticks >> 32);
      }
      else if (obj is string)
      {
        this.m_flags = 14;
        this.m_objref = obj;
      }
      else if (obj == null)
        this = Variant.Empty;
      else if (obj == System.DBNull.Value)
        this = Variant.DBNull;
      else if (obj == Type.Missing)
        this = Variant.Missing;
      else if (obj is Array)
      {
        this.m_flags = 65554;
        this.m_objref = obj;
      }
      else
      {
        this.m_flags = 0;
        this.m_objref = (object) null;
        if (obj is UnknownWrapper)
        {
          varEnum = VarEnum.VT_UNKNOWN;
          obj = ((UnknownWrapper) obj).WrappedObject;
        }
        else if (obj is DispatchWrapper)
        {
          varEnum = VarEnum.VT_DISPATCH;
          obj = ((DispatchWrapper) obj).WrappedObject;
        }
        else if (obj is ErrorWrapper)
        {
          varEnum = VarEnum.VT_ERROR;
          obj = (object) ((ErrorWrapper) obj).ErrorCode;
        }
        else if (obj is CurrencyWrapper)
        {
          varEnum = VarEnum.VT_CY;
          obj = (object) ((CurrencyWrapper) obj).WrappedObject;
        }
        else if (obj is BStrWrapper)
        {
          varEnum = VarEnum.VT_BSTR;
          obj = (object) ((BStrWrapper) obj).WrappedObject;
        }
        if (obj != null)
          this.SetFieldsObject(obj);
        if (varEnum == VarEnum.VT_EMPTY)
          return;
        this.m_flags |= (int) varEnum << 24;
      }
    }

    [SecurityCritical]
    public unsafe Variant(void* voidPointer, Type pointerType)
    {
      if (pointerType == (Type) null)
        throw new ArgumentNullException(nameof (pointerType));
      if (!pointerType.IsPointer)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), nameof (pointerType));
      this.m_objref = (object) pointerType;
      this.m_flags = 15;
      this.m_data1 = (int) voidPointer;
      this.m_data2 = 0;
    }

    internal int CVType
    {
      get
      {
        return this.m_flags & (int) ushort.MaxValue;
      }
    }

    [SecuritySafeCritical]
    public object ToObject()
    {
      switch (this.CVType)
      {
        case 0:
          return (object) null;
        case 2:
          return (object) ((uint) this.m_data1 > 0U);
        case 3:
          return (object) (char) this.m_data1;
        case 4:
          return (object) (sbyte) this.m_data1;
        case 5:
          return (object) (byte) this.m_data1;
        case 6:
          return (object) (short) this.m_data1;
        case 7:
          return (object) (ushort) this.m_data1;
        case 8:
          return (object) this.m_data1;
        case 9:
          return (object) (uint) this.m_data1;
        case 10:
          return (object) this.GetI8FromVar();
        case 11:
          return (object) (ulong) this.GetI8FromVar();
        case 12:
          return (object) this.GetR4FromVar();
        case 13:
          return (object) this.GetR8FromVar();
        case 16:
          return (object) new DateTime(this.GetI8FromVar());
        case 17:
          return (object) new TimeSpan(this.GetI8FromVar());
        case 21:
          return this.BoxEnum();
        case 22:
          return Type.Missing;
        case 23:
          return (object) System.DBNull.Value;
        default:
          return this.m_objref;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern object BoxEnum();

    [SecuritySafeCritical]
    internal static void MarshalHelperConvertObjectToVariant(object o, ref Variant v)
    {
      IConvertible convertible = RemotingServices.IsTransparentProxy(o) ? (IConvertible) null : o as IConvertible;
      if (o == null)
        v = Variant.Empty;
      else if (convertible == null)
      {
        v = new Variant(o);
      }
      else
      {
        IFormatProvider invariantCulture = (IFormatProvider) CultureInfo.InvariantCulture;
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Empty:
            v = Variant.Empty;
            break;
          case TypeCode.Object:
            v = new Variant(o);
            break;
          case TypeCode.DBNull:
            v = Variant.DBNull;
            break;
          case TypeCode.Boolean:
            v = new Variant(convertible.ToBoolean(invariantCulture));
            break;
          case TypeCode.Char:
            v = new Variant(convertible.ToChar(invariantCulture));
            break;
          case TypeCode.SByte:
            v = new Variant(convertible.ToSByte(invariantCulture));
            break;
          case TypeCode.Byte:
            v = new Variant(convertible.ToByte(invariantCulture));
            break;
          case TypeCode.Int16:
            v = new Variant(convertible.ToInt16(invariantCulture));
            break;
          case TypeCode.UInt16:
            v = new Variant(convertible.ToUInt16(invariantCulture));
            break;
          case TypeCode.Int32:
            v = new Variant(convertible.ToInt32(invariantCulture));
            break;
          case TypeCode.UInt32:
            v = new Variant(convertible.ToUInt32(invariantCulture));
            break;
          case TypeCode.Int64:
            v = new Variant(convertible.ToInt64(invariantCulture));
            break;
          case TypeCode.UInt64:
            v = new Variant(convertible.ToUInt64(invariantCulture));
            break;
          case TypeCode.Single:
            v = new Variant(convertible.ToSingle(invariantCulture));
            break;
          case TypeCode.Double:
            v = new Variant(convertible.ToDouble(invariantCulture));
            break;
          case TypeCode.Decimal:
            v = new Variant(convertible.ToDecimal(invariantCulture));
            break;
          case TypeCode.DateTime:
            v = new Variant(convertible.ToDateTime(invariantCulture));
            break;
          case TypeCode.String:
            v = new Variant((object) convertible.ToString(invariantCulture));
            break;
          default:
            throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnknownTypeCode", (object) convertible.GetTypeCode()));
        }
      }
    }

    internal static object MarshalHelperConvertVariantToObject(ref Variant v)
    {
      return v.ToObject();
    }

    [SecurityCritical]
    internal static void MarshalHelperCastVariant(object pValue, int vt, ref Variant v)
    {
      IConvertible convertible = pValue as IConvertible;
      if (convertible == null)
      {
        switch (vt)
        {
          case 8:
            if (pValue != null)
              throw new InvalidCastException(Environment.GetResourceString("InvalidCast_CannotCoerceByRefVariant"));
            v = new Variant((object) null);
            v.m_flags = 14;
            break;
          case 9:
            v = new Variant((object) new DispatchWrapper(pValue));
            break;
          case 12:
            v = new Variant(pValue);
            break;
          case 13:
            v = new Variant((object) new UnknownWrapper(pValue));
            break;
          case 36:
            v = new Variant(pValue);
            break;
          default:
            throw new InvalidCastException(Environment.GetResourceString("InvalidCast_CannotCoerceByRefVariant"));
        }
      }
      else
      {
        IFormatProvider invariantCulture = (IFormatProvider) CultureInfo.InvariantCulture;
        switch (vt)
        {
          case 0:
            v = Variant.Empty;
            break;
          case 1:
            v = Variant.DBNull;
            break;
          case 2:
            v = new Variant(convertible.ToInt16(invariantCulture));
            break;
          case 3:
            v = new Variant(convertible.ToInt32(invariantCulture));
            break;
          case 4:
            v = new Variant(convertible.ToSingle(invariantCulture));
            break;
          case 5:
            v = new Variant(convertible.ToDouble(invariantCulture));
            break;
          case 6:
            v = new Variant((object) new CurrencyWrapper(convertible.ToDecimal(invariantCulture)));
            break;
          case 7:
            v = new Variant(convertible.ToDateTime(invariantCulture));
            break;
          case 8:
            v = new Variant((object) convertible.ToString(invariantCulture));
            break;
          case 9:
            v = new Variant((object) new DispatchWrapper((object) convertible));
            break;
          case 10:
            v = new Variant((object) new ErrorWrapper(convertible.ToInt32(invariantCulture)));
            break;
          case 11:
            v = new Variant(convertible.ToBoolean(invariantCulture));
            break;
          case 12:
            v = new Variant((object) convertible);
            break;
          case 13:
            v = new Variant((object) new UnknownWrapper((object) convertible));
            break;
          case 14:
            v = new Variant(convertible.ToDecimal(invariantCulture));
            break;
          case 16:
            v = new Variant(convertible.ToSByte(invariantCulture));
            break;
          case 17:
            v = new Variant(convertible.ToByte(invariantCulture));
            break;
          case 18:
            v = new Variant(convertible.ToUInt16(invariantCulture));
            break;
          case 19:
            v = new Variant(convertible.ToUInt32(invariantCulture));
            break;
          case 20:
            v = new Variant(convertible.ToInt64(invariantCulture));
            break;
          case 21:
            v = new Variant(convertible.ToUInt64(invariantCulture));
            break;
          case 22:
            v = new Variant(convertible.ToInt32(invariantCulture));
            break;
          case 23:
            v = new Variant(convertible.ToUInt32(invariantCulture));
            break;
          default:
            throw new InvalidCastException(Environment.GetResourceString("InvalidCast_CannotCoerceByRefVariant"));
        }
      }
    }
  }
}
