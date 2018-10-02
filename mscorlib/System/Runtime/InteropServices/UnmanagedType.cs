// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UnmanagedType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Определяет порядок маршалинга параметров или полей в неуправляемый код.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum UnmanagedType
  {
    [__DynamicallyInvokable] Bool = 2,
    [__DynamicallyInvokable] I1 = 3,
    [__DynamicallyInvokable] U1 = 4,
    [__DynamicallyInvokable] I2 = 5,
    [__DynamicallyInvokable] U2 = 6,
    [__DynamicallyInvokable] I4 = 7,
    [__DynamicallyInvokable] U4 = 8,
    [__DynamicallyInvokable] I8 = 9,
    [__DynamicallyInvokable] U8 = 10, // 0x0000000A
    [__DynamicallyInvokable] R4 = 11, // 0x0000000B
    [__DynamicallyInvokable] R8 = 12, // 0x0000000C
    [__DynamicallyInvokable] Currency = 15, // 0x0000000F
    [__DynamicallyInvokable] BStr = 19, // 0x00000013
    [__DynamicallyInvokable] LPStr = 20, // 0x00000014
    [__DynamicallyInvokable] LPWStr = 21, // 0x00000015
    [__DynamicallyInvokable] LPTStr = 22, // 0x00000016
    [__DynamicallyInvokable] ByValTStr = 23, // 0x00000017
    [__DynamicallyInvokable] IUnknown = 25, // 0x00000019
    [__DynamicallyInvokable] IDispatch = 26, // 0x0000001A
    [__DynamicallyInvokable] Struct = 27, // 0x0000001B
    [__DynamicallyInvokable] Interface = 28, // 0x0000001C
    [__DynamicallyInvokable] SafeArray = 29, // 0x0000001D
    [__DynamicallyInvokable] ByValArray = 30, // 0x0000001E
    [__DynamicallyInvokable] SysInt = 31, // 0x0000001F
    [__DynamicallyInvokable] SysUInt = 32, // 0x00000020
    [__DynamicallyInvokable] VBByRefStr = 34, // 0x00000022
    [__DynamicallyInvokable] AnsiBStr = 35, // 0x00000023
    [__DynamicallyInvokable] TBStr = 36, // 0x00000024
    [__DynamicallyInvokable] VariantBool = 37, // 0x00000025
    [__DynamicallyInvokable] FunctionPtr = 38, // 0x00000026
    [__DynamicallyInvokable] AsAny = 40, // 0x00000028
    [__DynamicallyInvokable] LPArray = 42, // 0x0000002A
    [__DynamicallyInvokable] LPStruct = 43, // 0x0000002B
    [__DynamicallyInvokable] CustomMarshaler = 44, // 0x0000002C
    [__DynamicallyInvokable] Error = 45, // 0x0000002D
    [ComVisible(false), __DynamicallyInvokable] IInspectable = 46, // 0x0000002E
    [ComVisible(false), __DynamicallyInvokable] HString = 47, // 0x0000002F
    [ComVisible(false)] LPUTF8Str = 48, // 0x00000030
  }
}
