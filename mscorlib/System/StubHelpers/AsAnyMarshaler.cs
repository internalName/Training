// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.AsAnyMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  [SecurityCritical]
  internal struct AsAnyMarshaler
  {
    private const ushort VTHACK_ANSICHAR = 253;
    private const ushort VTHACK_WINBOOL = 254;
    private IntPtr pvArrayMarshaler;
    private AsAnyMarshaler.BackPropAction backPropAction;
    private Type layoutType;
    private CleanupWorkList cleanupWorkList;

    private static bool IsIn(int dwFlags)
    {
      return (uint) (dwFlags & 268435456) > 0U;
    }

    private static bool IsOut(int dwFlags)
    {
      return (uint) (dwFlags & 536870912) > 0U;
    }

    private static bool IsAnsi(int dwFlags)
    {
      return (uint) (dwFlags & 16711680) > 0U;
    }

    private static bool IsThrowOn(int dwFlags)
    {
      return (uint) (dwFlags & 65280) > 0U;
    }

    private static bool IsBestFit(int dwFlags)
    {
      return (uint) (dwFlags & (int) byte.MaxValue) > 0U;
    }

    internal AsAnyMarshaler(IntPtr pvArrayMarshaler)
    {
      this.pvArrayMarshaler = pvArrayMarshaler;
      this.backPropAction = AsAnyMarshaler.BackPropAction.None;
      this.layoutType = (Type) null;
      this.cleanupWorkList = (CleanupWorkList) null;
    }

    [SecurityCritical]
    private unsafe IntPtr ConvertArrayToNative(object pManagedHome, int dwFlags)
    {
      Type elementType = pManagedHome.GetType().GetElementType();
      VarEnum varEnum;
      switch (Type.GetTypeCode(elementType))
      {
        case TypeCode.Object:
          if (elementType == typeof (IntPtr))
          {
            varEnum = IntPtr.Size == 4 ? VarEnum.VT_I4 : VarEnum.VT_I8;
            break;
          }
          if (elementType == typeof (UIntPtr))
          {
            varEnum = IntPtr.Size == 4 ? VarEnum.VT_UI4 : VarEnum.VT_UI8;
            break;
          }
          goto default;
        case TypeCode.Boolean:
          varEnum = (VarEnum) 254;
          break;
        case TypeCode.Char:
          varEnum = AsAnyMarshaler.IsAnsi(dwFlags) ? (VarEnum) 253 : VarEnum.VT_UI2;
          break;
        case TypeCode.SByte:
          varEnum = VarEnum.VT_I1;
          break;
        case TypeCode.Byte:
          varEnum = VarEnum.VT_UI1;
          break;
        case TypeCode.Int16:
          varEnum = VarEnum.VT_I2;
          break;
        case TypeCode.UInt16:
          varEnum = VarEnum.VT_UI2;
          break;
        case TypeCode.Int32:
          varEnum = VarEnum.VT_I4;
          break;
        case TypeCode.UInt32:
          varEnum = VarEnum.VT_UI4;
          break;
        case TypeCode.Int64:
          varEnum = VarEnum.VT_I8;
          break;
        case TypeCode.UInt64:
          varEnum = VarEnum.VT_UI8;
          break;
        case TypeCode.Single:
          varEnum = VarEnum.VT_R4;
          break;
        case TypeCode.Double:
          varEnum = VarEnum.VT_R8;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_NDirectBadObject"));
      }
      int dwFlags1 = (int) varEnum;
      if (AsAnyMarshaler.IsBestFit(dwFlags))
        dwFlags1 |= 65536;
      if (AsAnyMarshaler.IsThrowOn(dwFlags))
        dwFlags1 |= 16777216;
      MngdNativeArrayMarshaler.CreateMarshaler(this.pvArrayMarshaler, IntPtr.Zero, dwFlags1);
      IntPtr num;
      IntPtr pNativeHome = new IntPtr((void*) &num);
      MngdNativeArrayMarshaler.ConvertSpaceToNative(this.pvArrayMarshaler, ref pManagedHome, pNativeHome);
      if (AsAnyMarshaler.IsIn(dwFlags))
        MngdNativeArrayMarshaler.ConvertContentsToNative(this.pvArrayMarshaler, ref pManagedHome, pNativeHome);
      if (AsAnyMarshaler.IsOut(dwFlags))
        this.backPropAction = AsAnyMarshaler.BackPropAction.Array;
      return num;
    }

    [SecurityCritical]
    private static IntPtr ConvertStringToNative(string pManagedHome, int dwFlags)
    {
      IntPtr dest;
      if (AsAnyMarshaler.IsAnsi(dwFlags))
      {
        dest = CSTRMarshaler.ConvertToNative(dwFlags & (int) ushort.MaxValue, pManagedHome, IntPtr.Zero);
      }
      else
      {
        System.StubHelpers.StubHelpers.CheckStringLength(pManagedHome.Length);
        int num = (pManagedHome.Length + 1) * 2;
        dest = Marshal.AllocCoTaskMem(num);
        string.InternalCopy(pManagedHome, dest, num);
      }
      return dest;
    }

    [SecurityCritical]
    private unsafe IntPtr ConvertStringBuilderToNative(StringBuilder pManagedHome, int dwFlags)
    {
      IntPtr dest;
      if (AsAnyMarshaler.IsAnsi(dwFlags))
      {
        System.StubHelpers.StubHelpers.CheckStringLength(pManagedHome.Capacity);
        int cb = pManagedHome.Capacity * Marshal.SystemMaxDBCSCharSize + 4;
        dest = Marshal.AllocCoTaskMem(cb);
        byte* pDest = (byte*) (void*) dest;
        *(pDest + cb - 3) = (byte) 0;
        *(pDest + cb - 2) = (byte) 0;
        *(pDest + cb - 1) = (byte) 0;
        if (AsAnyMarshaler.IsIn(dwFlags))
        {
          int cbLength;
          byte[] src = AnsiCharMarshaler.DoAnsiConversion(pManagedHome.ToString(), AsAnyMarshaler.IsBestFit(dwFlags), AsAnyMarshaler.IsThrowOn(dwFlags), out cbLength);
          Buffer.Memcpy(pDest, 0, src, 0, cbLength);
          pDest[cbLength] = (byte) 0;
        }
        if (AsAnyMarshaler.IsOut(dwFlags))
          this.backPropAction = AsAnyMarshaler.BackPropAction.StringBuilderAnsi;
      }
      else
      {
        int cb = pManagedHome.Capacity * 2 + 4;
        dest = Marshal.AllocCoTaskMem(cb);
        byte* numPtr = (byte*) (void*) dest;
        *(numPtr + cb - 1) = (byte) 0;
        *(numPtr + cb - 2) = (byte) 0;
        if (AsAnyMarshaler.IsIn(dwFlags))
        {
          int len = pManagedHome.Length * 2;
          pManagedHome.InternalCopy(dest, len);
          (numPtr + len)[0] = (byte) 0;
          (numPtr + len)[1] = (byte) 0;
        }
        if (AsAnyMarshaler.IsOut(dwFlags))
          this.backPropAction = AsAnyMarshaler.BackPropAction.StringBuilderUnicode;
      }
      return dest;
    }

    [SecurityCritical]
    private unsafe IntPtr ConvertLayoutToNative(object pManagedHome, int dwFlags)
    {
      IntPtr num = Marshal.AllocCoTaskMem(Marshal.SizeOfHelper(pManagedHome.GetType(), false));
      if (AsAnyMarshaler.IsIn(dwFlags))
        System.StubHelpers.StubHelpers.FmtClassUpdateNativeInternal(pManagedHome, (byte*) num.ToPointer(), ref this.cleanupWorkList);
      if (AsAnyMarshaler.IsOut(dwFlags))
        this.backPropAction = AsAnyMarshaler.BackPropAction.Layout;
      this.layoutType = pManagedHome.GetType();
      return num;
    }

    [SecurityCritical]
    internal IntPtr ConvertToNative(object pManagedHome, int dwFlags)
    {
      if (pManagedHome == null)
        return IntPtr.Zero;
      if (pManagedHome is ArrayWithOffset)
        throw new ArgumentException(Environment.GetResourceString("Arg_MarshalAsAnyRestriction"));
      if (pManagedHome.GetType().IsArray)
        return this.ConvertArrayToNative(pManagedHome, dwFlags);
      string pManagedHome1;
      if ((pManagedHome1 = pManagedHome as string) != null)
        return AsAnyMarshaler.ConvertStringToNative(pManagedHome1, dwFlags);
      StringBuilder pManagedHome2;
      if ((pManagedHome2 = pManagedHome as StringBuilder) != null)
        return this.ConvertStringBuilderToNative(pManagedHome2, dwFlags);
      if (pManagedHome.GetType().IsLayoutSequential || pManagedHome.GetType().IsExplicitLayout)
        return this.ConvertLayoutToNative(pManagedHome, dwFlags);
      throw new ArgumentException(Environment.GetResourceString("Arg_NDirectBadObject"));
    }

    [SecurityCritical]
    internal unsafe void ConvertToManaged(object pManagedHome, IntPtr pNativeHome)
    {
      switch (this.backPropAction)
      {
        case AsAnyMarshaler.BackPropAction.Array:
          MngdNativeArrayMarshaler.ConvertContentsToManaged(this.pvArrayMarshaler, ref pManagedHome, new IntPtr((void*) &pNativeHome));
          break;
        case AsAnyMarshaler.BackPropAction.Layout:
          System.StubHelpers.StubHelpers.FmtClassUpdateCLRInternal(pManagedHome, (byte*) pNativeHome.ToPointer());
          break;
        case AsAnyMarshaler.BackPropAction.StringBuilderAnsi:
          sbyte* pointer1 = (sbyte*) pNativeHome.ToPointer();
          ((StringBuilder) pManagedHome).ReplaceBufferAnsiInternal(pointer1, Win32Native.lstrlenA(pNativeHome));
          break;
        case AsAnyMarshaler.BackPropAction.StringBuilderUnicode:
          char* pointer2 = (char*) pNativeHome.ToPointer();
          ((StringBuilder) pManagedHome).ReplaceBufferInternal(pointer2, Win32Native.lstrlenW(pNativeHome));
          break;
      }
    }

    [SecurityCritical]
    internal void ClearNative(IntPtr pNativeHome)
    {
      if (pNativeHome != IntPtr.Zero)
      {
        if (this.layoutType != (Type) null)
          Marshal.DestroyStructure(pNativeHome, this.layoutType);
        Win32Native.CoTaskMemFree(pNativeHome);
      }
      System.StubHelpers.StubHelpers.DestroyCleanupList(ref this.cleanupWorkList);
    }

    private enum BackPropAction
    {
      None,
      Array,
      Layout,
      StringBuilderAnsi,
      StringBuilderUnicode,
    }
  }
}
