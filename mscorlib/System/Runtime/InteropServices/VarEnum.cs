// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.VarEnum
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, каким образом следует маршалировать элементы массива при маршалинге массива из управляемого в неуправляемый код как <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum VarEnum
  {
    [__DynamicallyInvokable] VT_EMPTY = 0,
    [__DynamicallyInvokable] VT_NULL = 1,
    [__DynamicallyInvokable] VT_I2 = 2,
    [__DynamicallyInvokable] VT_I4 = 3,
    [__DynamicallyInvokable] VT_R4 = 4,
    [__DynamicallyInvokable] VT_R8 = 5,
    [__DynamicallyInvokable] VT_CY = 6,
    [__DynamicallyInvokable] VT_DATE = 7,
    [__DynamicallyInvokable] VT_BSTR = 8,
    [__DynamicallyInvokable] VT_DISPATCH = 9,
    [__DynamicallyInvokable] VT_ERROR = 10, // 0x0000000A
    [__DynamicallyInvokable] VT_BOOL = 11, // 0x0000000B
    [__DynamicallyInvokable] VT_VARIANT = 12, // 0x0000000C
    [__DynamicallyInvokable] VT_UNKNOWN = 13, // 0x0000000D
    [__DynamicallyInvokable] VT_DECIMAL = 14, // 0x0000000E
    [__DynamicallyInvokable] VT_I1 = 16, // 0x00000010
    [__DynamicallyInvokable] VT_UI1 = 17, // 0x00000011
    [__DynamicallyInvokable] VT_UI2 = 18, // 0x00000012
    [__DynamicallyInvokable] VT_UI4 = 19, // 0x00000013
    [__DynamicallyInvokable] VT_I8 = 20, // 0x00000014
    [__DynamicallyInvokable] VT_UI8 = 21, // 0x00000015
    [__DynamicallyInvokable] VT_INT = 22, // 0x00000016
    [__DynamicallyInvokable] VT_UINT = 23, // 0x00000017
    [__DynamicallyInvokable] VT_VOID = 24, // 0x00000018
    [__DynamicallyInvokable] VT_HRESULT = 25, // 0x00000019
    [__DynamicallyInvokable] VT_PTR = 26, // 0x0000001A
    [__DynamicallyInvokable] VT_SAFEARRAY = 27, // 0x0000001B
    [__DynamicallyInvokable] VT_CARRAY = 28, // 0x0000001C
    [__DynamicallyInvokable] VT_USERDEFINED = 29, // 0x0000001D
    [__DynamicallyInvokable] VT_LPSTR = 30, // 0x0000001E
    [__DynamicallyInvokable] VT_LPWSTR = 31, // 0x0000001F
    [__DynamicallyInvokable] VT_RECORD = 36, // 0x00000024
    [__DynamicallyInvokable] VT_FILETIME = 64, // 0x00000040
    [__DynamicallyInvokable] VT_BLOB = 65, // 0x00000041
    [__DynamicallyInvokable] VT_STREAM = 66, // 0x00000042
    [__DynamicallyInvokable] VT_STORAGE = 67, // 0x00000043
    [__DynamicallyInvokable] VT_STREAMED_OBJECT = 68, // 0x00000044
    [__DynamicallyInvokable] VT_STORED_OBJECT = 69, // 0x00000045
    [__DynamicallyInvokable] VT_BLOB_OBJECT = 70, // 0x00000046
    [__DynamicallyInvokable] VT_CF = 71, // 0x00000047
    [__DynamicallyInvokable] VT_CLSID = 72, // 0x00000048
    [__DynamicallyInvokable] VT_VECTOR = 4096, // 0x00001000
    [__DynamicallyInvokable] VT_ARRAY = 8192, // 0x00002000
    [__DynamicallyInvokable] VT_BYREF = 16384, // 0x00004000
  }
}
