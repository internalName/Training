// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.MngdHiddenLengthArrayMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class MngdHiddenLengthArrayMarshaler
  {
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CreateMarshaler(IntPtr pMarshalState, IntPtr pMT, IntPtr cbElementSize, ushort vt);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertSpaceToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertContentsToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

    [SecurityCritical]
    internal static unsafe void ConvertContentsToNative_DateTime(ref DateTimeOffset[] managedArray, IntPtr pNativeHome)
    {
      if (managedArray == null)
        return;
      DateTimeNative* dateTimeNativePtr = (DateTimeNative*) *(IntPtr*) (void*) pNativeHome;
      for (int index = 0; index < managedArray.Length; ++index)
        DateTimeOffsetMarshaler.ConvertToNative(ref managedArray[index], out dateTimeNativePtr[index]);
    }

    [SecurityCritical]
    internal static unsafe void ConvertContentsToNative_Type(ref Type[] managedArray, IntPtr pNativeHome)
    {
      if (managedArray == null)
        return;
      TypeNameNative* typeNameNativePtr = (TypeNameNative*) *(IntPtr*) (void*) pNativeHome;
      for (int index = 0; index < managedArray.Length; ++index)
        SystemTypeMarshaler.ConvertToNative(managedArray[index], typeNameNativePtr + index);
    }

    [SecurityCritical]
    internal static unsafe void ConvertContentsToNative_Exception(ref Exception[] managedArray, IntPtr pNativeHome)
    {
      if (managedArray == null)
        return;
      int* numPtr = (int*) *(IntPtr*) (void*) pNativeHome;
      for (int index = 0; index < managedArray.Length; ++index)
        numPtr[index] = HResultExceptionMarshaler.ConvertToNative(managedArray[index]);
    }

    [SecurityCritical]
    internal static unsafe void ConvertContentsToNative_Nullable<T>(ref T?[] managedArray, IntPtr pNativeHome) where T : struct
    {
      if (managedArray == null)
        return;
      IntPtr* numPtr = (IntPtr*) *(IntPtr*) (void*) pNativeHome;
      for (int index = 0; index < managedArray.Length; ++index)
        numPtr[index] = NullableMarshaler.ConvertToNative<T>(ref managedArray[index]);
    }

    [SecurityCritical]
    internal static unsafe void ConvertContentsToNative_KeyValuePair<K, V>(ref KeyValuePair<K, V>[] managedArray, IntPtr pNativeHome)
    {
      if (managedArray == null)
        return;
      IntPtr* numPtr = (IntPtr*) *(IntPtr*) (void*) pNativeHome;
      for (int index = 0; index < managedArray.Length; ++index)
        numPtr[index] = KeyValuePairMarshaler.ConvertToNative<K, V>(ref managedArray[index]);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertSpaceToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome, int elementCount);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ConvertContentsToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

    [SecurityCritical]
    internal static unsafe void ConvertContentsToManaged_DateTime(ref DateTimeOffset[] managedArray, IntPtr pNativeHome)
    {
      if (managedArray == null)
        return;
      DateTimeNative* dateTimeNativePtr = (DateTimeNative*) *(IntPtr*) (void*) pNativeHome;
      for (int index = 0; index < managedArray.Length; ++index)
        DateTimeOffsetMarshaler.ConvertToManaged(out managedArray[index], ref dateTimeNativePtr[index]);
    }

    [SecurityCritical]
    internal static unsafe void ConvertContentsToManaged_Type(ref Type[] managedArray, IntPtr pNativeHome)
    {
      if (managedArray == null)
        return;
      TypeNameNative* typeNameNativePtr = (TypeNameNative*) *(IntPtr*) (void*) pNativeHome;
      for (int index = 0; index < managedArray.Length; ++index)
        SystemTypeMarshaler.ConvertToManaged(typeNameNativePtr + index, ref managedArray[index]);
    }

    [SecurityCritical]
    internal static unsafe void ConvertContentsToManaged_Exception(ref Exception[] managedArray, IntPtr pNativeHome)
    {
      if (managedArray == null)
        return;
      int* numPtr = (int*) *(IntPtr*) (void*) pNativeHome;
      for (int index = 0; index < managedArray.Length; ++index)
        managedArray[index] = HResultExceptionMarshaler.ConvertToManaged(numPtr[index]);
    }

    [SecurityCritical]
    internal static unsafe void ConvertContentsToManaged_Nullable<T>(ref T?[] managedArray, IntPtr pNativeHome) where T : struct
    {
      if (managedArray == null)
        return;
      IntPtr* numPtr = (IntPtr*) *(IntPtr*) (void*) pNativeHome;
      for (int index = 0; index < managedArray.Length; ++index)
        managedArray[index] = NullableMarshaler.ConvertToManaged<T>(numPtr[index]);
    }

    [SecurityCritical]
    internal static unsafe void ConvertContentsToManaged_KeyValuePair<K, V>(ref KeyValuePair<K, V>[] managedArray, IntPtr pNativeHome)
    {
      if (managedArray == null)
        return;
      IntPtr* numPtr = (IntPtr*) *(IntPtr*) (void*) pNativeHome;
      for (int index = 0; index < managedArray.Length; ++index)
        managedArray[index] = KeyValuePairMarshaler.ConvertToManaged<K, V>(numPtr[index]);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ClearNativeContents(IntPtr pMarshalState, IntPtr pNativeHome, int cElements);

    [SecurityCritical]
    internal static unsafe void ClearNativeContents_Type(IntPtr pNativeHome, int cElements)
    {
      TypeNameNative* pNativeType = (TypeNameNative*) *(IntPtr*) (void*) pNativeHome;
      if ((IntPtr) pNativeType == IntPtr.Zero)
        return;
      for (int index = 0; index < cElements; ++index)
      {
        SystemTypeMarshaler.ClearNative(pNativeType);
        ++pNativeType;
      }
    }
  }
}
