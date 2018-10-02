// Decompiled with JetBrains decompiler
// Type: System.DateTimeResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System
{
  internal struct DateTimeResult
  {
    internal int Year;
    internal int Month;
    internal int Day;
    internal int Hour;
    internal int Minute;
    internal int Second;
    internal double fraction;
    internal int era;
    internal ParseFlags flags;
    internal TimeSpan timeZoneOffset;
    internal Calendar calendar;
    internal DateTime parsedDate;
    internal ParseFailureKind failure;
    internal string failureMessageID;
    internal object failureMessageFormatArgument;
    internal string failureArgumentName;

    internal void Init()
    {
      this.Year = -1;
      this.Month = -1;
      this.Day = -1;
      this.fraction = -1.0;
      this.era = -1;
    }

    internal void SetDate(int year, int month, int day)
    {
      this.Year = year;
      this.Month = month;
      this.Day = day;
    }

    internal void SetFailure(ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
    {
      this.failure = failure;
      this.failureMessageID = failureMessageID;
      this.failureMessageFormatArgument = failureMessageFormatArgument;
    }

    internal void SetFailure(ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName)
    {
      this.failure = failure;
      this.failureMessageID = failureMessageID;
      this.failureMessageFormatArgument = failureMessageFormatArgument;
      this.failureArgumentName = failureArgumentName;
    }
  }
}
