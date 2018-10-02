// Decompiled with JetBrains decompiler
// Type: System.ParsingInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System
{
  internal struct ParsingInfo
  {
    internal Calendar calendar;
    internal int dayOfWeek;
    internal DateTimeParse.TM timeMark;
    internal bool fUseHour12;
    internal bool fUseTwoDigitYear;
    internal bool fAllowInnerWhite;
    internal bool fAllowTrailingWhite;
    internal bool fCustomNumberParser;
    internal DateTimeParse.MatchNumberDelegate parseNumberDelegate;

    internal void Init()
    {
      this.dayOfWeek = -1;
      this.timeMark = DateTimeParse.TM.NotSet;
    }
  }
}
