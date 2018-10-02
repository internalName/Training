// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CallingConvention
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Задает соглашение о вызовах, которое требуется для вызова методов, реализованных в неуправляемом коде.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum CallingConvention
  {
    [__DynamicallyInvokable] Winapi = 1,
    [__DynamicallyInvokable] Cdecl = 2,
    [__DynamicallyInvokable] StdCall = 3,
    [__DynamicallyInvokable] ThisCall = 4,
    FastCall = 5,
  }
}
