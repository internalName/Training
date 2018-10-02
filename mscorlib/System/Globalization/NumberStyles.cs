// Decompiled with JetBrains decompiler
// Type: System.Globalization.NumberStyles
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>
  ///   Определяет стили, разрешенные в аргументах числовой строки, передаваемые <see langword="Parse" /> и <see langword="TryParse" /> методы числовых типов целочисленные и с плавающей запятой.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum NumberStyles
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] AllowLeadingWhite = 1,
    [__DynamicallyInvokable] AllowTrailingWhite = 2,
    [__DynamicallyInvokable] AllowLeadingSign = 4,
    [__DynamicallyInvokable] AllowTrailingSign = 8,
    [__DynamicallyInvokable] AllowParentheses = 16, // 0x00000010
    [__DynamicallyInvokable] AllowDecimalPoint = 32, // 0x00000020
    [__DynamicallyInvokable] AllowThousands = 64, // 0x00000040
    [__DynamicallyInvokable] AllowExponent = 128, // 0x00000080
    [__DynamicallyInvokable] AllowCurrencySymbol = 256, // 0x00000100
    [__DynamicallyInvokable] AllowHexSpecifier = 512, // 0x00000200
    [__DynamicallyInvokable] Integer = AllowLeadingSign | AllowTrailingWhite | AllowLeadingWhite, // 0x00000007
    [__DynamicallyInvokable] HexNumber = AllowHexSpecifier | AllowTrailingWhite | AllowLeadingWhite, // 0x00000203
    [__DynamicallyInvokable] Number = Integer | AllowThousands | AllowDecimalPoint | AllowTrailingSign, // 0x0000006F
    [__DynamicallyInvokable] Float = Integer | AllowExponent | AllowDecimalPoint, // 0x000000A7
    [__DynamicallyInvokable] Currency = Number | AllowCurrencySymbol | AllowParentheses, // 0x0000017F
    [__DynamicallyInvokable] Any = Currency | AllowExponent, // 0x000001FF
  }
}
