// Decompiled with JetBrains decompiler
// Type: System.Globalization.FORMATFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  internal enum FORMATFLAGS
  {
    None = 0,
    UseGenitiveMonth = 1,
    UseLeapYearMonth = 2,
    UseSpacesInMonthNames = 4,
    UseHebrewParsing = 8,
    UseSpacesInDayNames = 16, // 0x00000010
    UseDigitPrefixInTokens = 32, // 0x00000020
  }
}
