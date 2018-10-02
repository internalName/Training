// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.NullableMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class NullableMarshaler
  {
    [SecurityCritical]
    internal static IntPtr ConvertToNative<T>(ref T? pManaged) where T : struct
    {
      if (pManaged.HasValue)
        return Marshal.GetComInterfaceForObject(IReferenceFactory.CreateIReference((object) pManaged), typeof (IReference<T>));
      return IntPtr.Zero;
    }

    [SecurityCritical]
    internal static void ConvertToManagedRetVoid<T>(IntPtr pNative, ref T? retObj) where T : struct
    {
      retObj = NullableMarshaler.ConvertToManaged<T>(pNative);
    }

    [SecurityCritical]
    internal static T? ConvertToManaged<T>(IntPtr pNative) where T : struct
    {
      if (pNative != IntPtr.Zero)
        return (T?) CLRIReferenceImpl<T>.UnboxHelper(InterfaceMarshaler.ConvertToManagedWithoutUnboxing(pNative));
      return new T?();
    }
  }
}
