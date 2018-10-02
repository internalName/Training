// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventFieldFormat
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Указывает способ форматирования значения типа, определяемого пользователем, и может использоваться для переопределения форматирования поля по умолчанию.
  /// </summary>
  [__DynamicallyInvokable]
  public enum EventFieldFormat
  {
    [__DynamicallyInvokable] Default = 0,
    [__DynamicallyInvokable] String = 2,
    [__DynamicallyInvokable] Boolean = 3,
    [__DynamicallyInvokable] Hexadecimal = 4,
    [__DynamicallyInvokable] Xml = 11, // 0x0000000B
    [__DynamicallyInvokable] Json = 12, // 0x0000000C
    [__DynamicallyInvokable] HResult = 15, // 0x0000000F
  }
}
