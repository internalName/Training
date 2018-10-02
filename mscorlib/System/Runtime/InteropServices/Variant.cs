// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.Variant
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.InteropServices
{
  [SecurityCritical]
  [StructLayout(LayoutKind.Explicit)]
  internal struct Variant
  {
    [FieldOffset(0)]
    private Variant.TypeUnion _typeUnion;
    [FieldOffset(0)]
    private Decimal _decimal;

    internal static bool IsPrimitiveType(VarEnum varEnum)
    {
      switch (varEnum)
      {
        case VarEnum.VT_I2:
        case VarEnum.VT_I4:
        case VarEnum.VT_R4:
        case VarEnum.VT_R8:
        case VarEnum.VT_DATE:
        case VarEnum.VT_BSTR:
        case VarEnum.VT_BOOL:
        case VarEnum.VT_DECIMAL:
        case VarEnum.VT_I1:
        case VarEnum.VT_UI1:
        case VarEnum.VT_UI2:
        case VarEnum.VT_UI4:
        case VarEnum.VT_I8:
        case VarEnum.VT_UI8:
        case VarEnum.VT_INT:
        case VarEnum.VT_UINT:
          return true;
        default:
          return false;
      }
    }

    public unsafe void CopyFromIndirect(object value)
    {
      VarEnum varEnum = this.VariantType & ~VarEnum.VT_BYREF;
      if (value == null)
      {
        if (varEnum != VarEnum.VT_DISPATCH && varEnum != VarEnum.VT_UNKNOWN && varEnum != VarEnum.VT_BSTR)
          return;
        *(IntPtr*) (void*) this._typeUnion._unionTypes._byref = IntPtr.Zero;
      }
      else
      {
        switch (varEnum)
        {
          case VarEnum.VT_I2:
            *(short*) (void*) this._typeUnion._unionTypes._byref = (short) value;
            break;
          case VarEnum.VT_I4:
          case VarEnum.VT_INT:
            *(int*) (void*) this._typeUnion._unionTypes._byref = (int) value;
            break;
          case VarEnum.VT_R4:
            *(float*) (void*) this._typeUnion._unionTypes._byref = (float) value;
            break;
          case VarEnum.VT_R8:
            *(double*) (void*) this._typeUnion._unionTypes._byref = (double) value;
            break;
          case VarEnum.VT_CY:
            *(long*) (void*) this._typeUnion._unionTypes._byref = Decimal.ToOACurrency((Decimal) value);
            break;
          case VarEnum.VT_DATE:
            *(double*) (void*) this._typeUnion._unionTypes._byref = ((DateTime) value).ToOADate();
            break;
          case VarEnum.VT_BSTR:
            *(IntPtr*) (void*) this._typeUnion._unionTypes._byref = Marshal.StringToBSTR((string) value);
            break;
          case VarEnum.VT_DISPATCH:
            *(IntPtr*) (void*) this._typeUnion._unionTypes._byref = Marshal.GetIDispatchForObject(value);
            break;
          case VarEnum.VT_ERROR:
            *(int*) (void*) this._typeUnion._unionTypes._byref = ((ErrorWrapper) value).ErrorCode;
            break;
          case VarEnum.VT_BOOL:
            *(short*) (void*) this._typeUnion._unionTypes._byref = (bool) value ? (short) -1 : (short) 0;
            break;
          case VarEnum.VT_VARIANT:
            Marshal.GetNativeVariantForObject(value, this._typeUnion._unionTypes._byref);
            break;
          case VarEnum.VT_UNKNOWN:
            *(IntPtr*) (void*) this._typeUnion._unionTypes._byref = Marshal.GetIUnknownForObject(value);
            break;
          case VarEnum.VT_DECIMAL:
            *(Decimal*) (void*) this._typeUnion._unionTypes._byref = (Decimal) value;
            break;
          case VarEnum.VT_I1:
            *(sbyte*) (void*) this._typeUnion._unionTypes._byref = (sbyte) value;
            break;
          case VarEnum.VT_UI1:
            *(sbyte*) (void*) this._typeUnion._unionTypes._byref = (sbyte) (byte) value;
            break;
          case VarEnum.VT_UI2:
            *(short*) (void*) this._typeUnion._unionTypes._byref = (short) (ushort) value;
            break;
          case VarEnum.VT_UI4:
          case VarEnum.VT_UINT:
            *(int*) (void*) this._typeUnion._unionTypes._byref = (int) (uint) value;
            break;
          case VarEnum.VT_I8:
            *(long*) (void*) this._typeUnion._unionTypes._byref = (long) value;
            break;
          case VarEnum.VT_UI8:
            *(long*) (void*) this._typeUnion._unionTypes._byref = (long) (ulong) value;
            break;
          default:
            throw new ArgumentException("invalid argument type");
        }
      }
    }

    public unsafe object ToObject()
    {
      if (this.IsEmpty)
        return (object) null;
      switch (this.VariantType)
      {
        case VarEnum.VT_NULL:
          return (object) DBNull.Value;
        case VarEnum.VT_I2:
          return (object) this.AsI2;
        case VarEnum.VT_I4:
          return (object) this.AsI4;
        case VarEnum.VT_R4:
          return (object) this.AsR4;
        case VarEnum.VT_R8:
          return (object) this.AsR8;
        case VarEnum.VT_CY:
          return (object) this.AsCy;
        case VarEnum.VT_DATE:
          return (object) this.AsDate;
        case VarEnum.VT_BSTR:
          return (object) this.AsBstr;
        case VarEnum.VT_DISPATCH:
          return this.AsDispatch;
        case VarEnum.VT_ERROR:
          return (object) this.AsError;
        case VarEnum.VT_BOOL:
          return (object) this.AsBool;
        case VarEnum.VT_UNKNOWN:
          return this.AsUnknown;
        case VarEnum.VT_DECIMAL:
          return (object) this.AsDecimal;
        case VarEnum.VT_I1:
          return (object) this.AsI1;
        case VarEnum.VT_UI1:
          return (object) this.AsUi1;
        case VarEnum.VT_UI2:
          return (object) this.AsUi2;
        case VarEnum.VT_UI4:
          return (object) this.AsUi4;
        case VarEnum.VT_I8:
          return (object) this.AsI8;
        case VarEnum.VT_UI8:
          return (object) this.AsUi8;
        case VarEnum.VT_INT:
          return (object) this.AsInt;
        case VarEnum.VT_UINT:
          return (object) this.AsUint;
        default:
          try
          {
            fixed (Variant* variantPtr = &this)
              return Marshal.GetObjectForNativeVariant((IntPtr) ((void*) variantPtr));
          }
          catch (Exception ex)
          {
            throw new NotImplementedException("Variant.ToObject cannot handle" + (object) this.VariantType, ex);
          }
      }
    }

    public unsafe void Clear()
    {
      VarEnum variantType = this.VariantType;
      if ((variantType & VarEnum.VT_BYREF) != VarEnum.VT_EMPTY)
        this.VariantType = VarEnum.VT_EMPTY;
      else if ((variantType & VarEnum.VT_ARRAY) != VarEnum.VT_EMPTY || variantType == VarEnum.VT_BSTR || (variantType == VarEnum.VT_UNKNOWN || variantType == VarEnum.VT_DISPATCH) || (variantType == VarEnum.VT_VARIANT || variantType == VarEnum.VT_RECORD || variantType == VarEnum.VT_VARIANT))
      {
        fixed (Variant* variantPtr = &this)
          NativeMethods.VariantClear((IntPtr) ((void*) variantPtr));
      }
      else
        this.VariantType = VarEnum.VT_EMPTY;
    }

    public VarEnum VariantType
    {
      get
      {
        return (VarEnum) this._typeUnion._vt;
      }
      set
      {
        this._typeUnion._vt = (ushort) value;
      }
    }

    internal bool IsEmpty
    {
      get
      {
        return this._typeUnion._vt == (ushort) 0;
      }
    }

    internal bool IsByRef
    {
      get
      {
        return ((uint) this._typeUnion._vt & 16384U) > 0U;
      }
    }

    public void SetAsNULL()
    {
      this.VariantType = VarEnum.VT_NULL;
    }

    public sbyte AsI1
    {
      get
      {
        return this._typeUnion._unionTypes._i1;
      }
      set
      {
        this.VariantType = VarEnum.VT_I1;
        this._typeUnion._unionTypes._i1 = value;
      }
    }

    public short AsI2
    {
      get
      {
        return this._typeUnion._unionTypes._i2;
      }
      set
      {
        this.VariantType = VarEnum.VT_I2;
        this._typeUnion._unionTypes._i2 = value;
      }
    }

    public int AsI4
    {
      get
      {
        return this._typeUnion._unionTypes._i4;
      }
      set
      {
        this.VariantType = VarEnum.VT_I4;
        this._typeUnion._unionTypes._i4 = value;
      }
    }

    public long AsI8
    {
      get
      {
        return this._typeUnion._unionTypes._i8;
      }
      set
      {
        this.VariantType = VarEnum.VT_I8;
        this._typeUnion._unionTypes._i8 = value;
      }
    }

    public byte AsUi1
    {
      get
      {
        return this._typeUnion._unionTypes._ui1;
      }
      set
      {
        this.VariantType = VarEnum.VT_UI1;
        this._typeUnion._unionTypes._ui1 = value;
      }
    }

    public ushort AsUi2
    {
      get
      {
        return this._typeUnion._unionTypes._ui2;
      }
      set
      {
        this.VariantType = VarEnum.VT_UI2;
        this._typeUnion._unionTypes._ui2 = value;
      }
    }

    public uint AsUi4
    {
      get
      {
        return this._typeUnion._unionTypes._ui4;
      }
      set
      {
        this.VariantType = VarEnum.VT_UI4;
        this._typeUnion._unionTypes._ui4 = value;
      }
    }

    public ulong AsUi8
    {
      get
      {
        return this._typeUnion._unionTypes._ui8;
      }
      set
      {
        this.VariantType = VarEnum.VT_UI8;
        this._typeUnion._unionTypes._ui8 = value;
      }
    }

    public int AsInt
    {
      get
      {
        return this._typeUnion._unionTypes._int;
      }
      set
      {
        this.VariantType = VarEnum.VT_INT;
        this._typeUnion._unionTypes._int = value;
      }
    }

    public uint AsUint
    {
      get
      {
        return this._typeUnion._unionTypes._uint;
      }
      set
      {
        this.VariantType = VarEnum.VT_UINT;
        this._typeUnion._unionTypes._uint = value;
      }
    }

    public bool AsBool
    {
      get
      {
        return (uint) this._typeUnion._unionTypes._bool > 0U;
      }
      set
      {
        this.VariantType = VarEnum.VT_BOOL;
        this._typeUnion._unionTypes._bool = value ? (short) -1 : (short) 0;
      }
    }

    public int AsError
    {
      get
      {
        return this._typeUnion._unionTypes._error;
      }
      set
      {
        this.VariantType = VarEnum.VT_ERROR;
        this._typeUnion._unionTypes._error = value;
      }
    }

    public float AsR4
    {
      get
      {
        return this._typeUnion._unionTypes._r4;
      }
      set
      {
        this.VariantType = VarEnum.VT_R4;
        this._typeUnion._unionTypes._r4 = value;
      }
    }

    public double AsR8
    {
      get
      {
        return this._typeUnion._unionTypes._r8;
      }
      set
      {
        this.VariantType = VarEnum.VT_R8;
        this._typeUnion._unionTypes._r8 = value;
      }
    }

    public Decimal AsDecimal
    {
      get
      {
        Variant variant = this;
        variant._typeUnion._vt = (ushort) 0;
        return variant._decimal;
      }
      set
      {
        this.VariantType = VarEnum.VT_DECIMAL;
        this._decimal = value;
        this._typeUnion._vt = (ushort) 14;
      }
    }

    public Decimal AsCy
    {
      get
      {
        return Decimal.FromOACurrency(this._typeUnion._unionTypes._cy);
      }
      set
      {
        this.VariantType = VarEnum.VT_CY;
        this._typeUnion._unionTypes._cy = Decimal.ToOACurrency(value);
      }
    }

    public DateTime AsDate
    {
      get
      {
        return DateTime.FromOADate(this._typeUnion._unionTypes._date);
      }
      set
      {
        this.VariantType = VarEnum.VT_DATE;
        this._typeUnion._unionTypes._date = value.ToOADate();
      }
    }

    public string AsBstr
    {
      get
      {
        return Marshal.PtrToStringBSTR(this._typeUnion._unionTypes._bstr);
      }
      set
      {
        this.VariantType = VarEnum.VT_BSTR;
        this._typeUnion._unionTypes._bstr = Marshal.StringToBSTR(value);
      }
    }

    public object AsUnknown
    {
      get
      {
        if (this._typeUnion._unionTypes._unknown == IntPtr.Zero)
          return (object) null;
        return Marshal.GetObjectForIUnknown(this._typeUnion._unionTypes._unknown);
      }
      set
      {
        this.VariantType = VarEnum.VT_UNKNOWN;
        if (value == null)
          this._typeUnion._unionTypes._unknown = IntPtr.Zero;
        else
          this._typeUnion._unionTypes._unknown = Marshal.GetIUnknownForObject(value);
      }
    }

    public object AsDispatch
    {
      get
      {
        if (this._typeUnion._unionTypes._dispatch == IntPtr.Zero)
          return (object) null;
        return Marshal.GetObjectForIUnknown(this._typeUnion._unionTypes._dispatch);
      }
      set
      {
        this.VariantType = VarEnum.VT_DISPATCH;
        if (value == null)
          this._typeUnion._unionTypes._dispatch = IntPtr.Zero;
        else
          this._typeUnion._unionTypes._dispatch = Marshal.GetIDispatchForObject(value);
      }
    }

    internal IntPtr AsByRefVariant
    {
      get
      {
        return this._typeUnion._unionTypes._pvarVal;
      }
    }

    private struct TypeUnion
    {
      internal ushort _vt;
      internal ushort _wReserved1;
      internal ushort _wReserved2;
      internal ushort _wReserved3;
      internal Variant.UnionTypes _unionTypes;
    }

    private struct Record
    {
      private IntPtr _record;
      private IntPtr _recordInfo;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct UnionTypes
    {
      [FieldOffset(0)]
      internal sbyte _i1;
      [FieldOffset(0)]
      internal short _i2;
      [FieldOffset(0)]
      internal int _i4;
      [FieldOffset(0)]
      internal long _i8;
      [FieldOffset(0)]
      internal byte _ui1;
      [FieldOffset(0)]
      internal ushort _ui2;
      [FieldOffset(0)]
      internal uint _ui4;
      [FieldOffset(0)]
      internal ulong _ui8;
      [FieldOffset(0)]
      internal int _int;
      [FieldOffset(0)]
      internal uint _uint;
      [FieldOffset(0)]
      internal short _bool;
      [FieldOffset(0)]
      internal int _error;
      [FieldOffset(0)]
      internal float _r4;
      [FieldOffset(0)]
      internal double _r8;
      [FieldOffset(0)]
      internal long _cy;
      [FieldOffset(0)]
      internal double _date;
      [FieldOffset(0)]
      internal IntPtr _bstr;
      [FieldOffset(0)]
      internal IntPtr _unknown;
      [FieldOffset(0)]
      internal IntPtr _dispatch;
      [FieldOffset(0)]
      internal IntPtr _pvarVal;
      [FieldOffset(0)]
      internal IntPtr _byref;
      [FieldOffset(0)]
      internal Variant.Record _record;
    }
  }
}
