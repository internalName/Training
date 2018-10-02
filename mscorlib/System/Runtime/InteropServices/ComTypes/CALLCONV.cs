// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.CALLCONV
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Определяет соглашение о вызове, используемое методом, описанным в структуре METHODDATA.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum CALLCONV
  {
    [__DynamicallyInvokable] CC_CDECL = 1,
    [__DynamicallyInvokable] CC_MSCPASCAL = 2,
    [__DynamicallyInvokable] CC_PASCAL = 2,
    [__DynamicallyInvokable] CC_MACPASCAL = 3,
    [__DynamicallyInvokable] CC_STDCALL = 4,
    [__DynamicallyInvokable] CC_RESERVED = 5,
    [__DynamicallyInvokable] CC_SYSCALL = 6,
    [__DynamicallyInvokable] CC_MPWCDECL = 7,
    [__DynamicallyInvokable] CC_MPWPASCAL = 8,
    [__DynamicallyInvokable] CC_MAX = 9,
  }
}
