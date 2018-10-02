// Decompiled with JetBrains decompiler
// Type: System.Globalization.DateTimeStyles
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>
  ///   Определяет параметры форматирования, регулирующие синтаксический анализ строк для некоторых методов синтаксического анализа дат и времени.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum DateTimeStyles
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] AllowLeadingWhite = 1,
    [__DynamicallyInvokable] AllowTrailingWhite = 2,
    [__DynamicallyInvokable] AllowInnerWhite = 4,
    [__DynamicallyInvokable] AllowWhiteSpaces = AllowInnerWhite | AllowTrailingWhite | AllowLeadingWhite, // 0x00000007
    [__DynamicallyInvokable] NoCurrentDateDefault = 8,
    [__DynamicallyInvokable] AdjustToUniversal = 16, // 0x00000010
    [__DynamicallyInvokable] AssumeLocal = 32, // 0x00000020
    [__DynamicallyInvokable] AssumeUniversal = 64, // 0x00000040
    [__DynamicallyInvokable] RoundtripKind = 128, // 0x00000080
  }
}
