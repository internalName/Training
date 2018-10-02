// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.OperandType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>Описывает тип операнда инструкции MSIL.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum OperandType
  {
    [__DynamicallyInvokable] InlineBrTarget = 0,
    [__DynamicallyInvokable] InlineField = 1,
    [__DynamicallyInvokable] InlineI = 2,
    [__DynamicallyInvokable] InlineI8 = 3,
    [__DynamicallyInvokable] InlineMethod = 4,
    [__DynamicallyInvokable] InlineNone = 5,
    [Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")] InlinePhi = 6,
    [__DynamicallyInvokable] InlineR = 7,
    [__DynamicallyInvokable] InlineSig = 9,
    [__DynamicallyInvokable] InlineString = 10, // 0x0000000A
    [__DynamicallyInvokable] InlineSwitch = 11, // 0x0000000B
    [__DynamicallyInvokable] InlineTok = 12, // 0x0000000C
    [__DynamicallyInvokable] InlineType = 13, // 0x0000000D
    [__DynamicallyInvokable] InlineVar = 14, // 0x0000000E
    [__DynamicallyInvokable] ShortInlineBrTarget = 15, // 0x0000000F
    [__DynamicallyInvokable] ShortInlineI = 16, // 0x00000010
    [__DynamicallyInvokable] ShortInlineR = 17, // 0x00000011
    [__DynamicallyInvokable] ShortInlineVar = 18, // 0x00000012
  }
}
