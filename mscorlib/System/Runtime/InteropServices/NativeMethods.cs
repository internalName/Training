// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.NativeMethods
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.InteropServices
{
  internal static class NativeMethods
  {
    [SuppressUnmanagedCodeSecurity]
    [SecurityCritical]
    [DllImport("oleaut32.dll", PreserveSig = false)]
    internal static extern void VariantClear(IntPtr variant);

    [SuppressUnmanagedCodeSecurity]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00020400-0000-0000-C000-000000000046")]
    [ComImport]
    internal interface IDispatch
    {
      [SecurityCritical]
      void GetTypeInfoCount(out uint pctinfo);

      [SecurityCritical]
      void GetTypeInfo(uint iTInfo, int lcid, out IntPtr info);

      [SecurityCritical]
      void GetIDsOfNames(ref Guid iid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2, ArraySubType = UnmanagedType.LPWStr)] string[] names, uint cNames, int lcid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2, ArraySubType = UnmanagedType.I4), Out] int[] rgDispId);

      [SecurityCritical]
      void Invoke(int dispIdMember, ref Guid riid, int lcid, System.Runtime.InteropServices.ComTypes.INVOKEKIND wFlags, ref System.Runtime.InteropServices.ComTypes.DISPPARAMS pDispParams, IntPtr pvarResult, IntPtr pExcepInfo, IntPtr puArgErr);
    }
  }
}
