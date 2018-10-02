// Decompiled with JetBrains decompiler
// Type: System.DateTimeRawInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System
{
  internal struct DateTimeRawInfo
  {
    [SecurityCritical]
    private unsafe int* num;
    internal int numCount;
    internal int month;
    internal int year;
    internal int dayOfWeek;
    internal int era;
    internal DateTimeParse.TM timeMark;
    internal double fraction;
    internal bool hasSameDateAndTimeSeparators;
    internal bool timeZone;

    [SecurityCritical]
    internal unsafe void Init(int* numberBuffer)
    {
      this.month = -1;
      this.year = -1;
      this.dayOfWeek = -1;
      this.era = -1;
      this.timeMark = DateTimeParse.TM.NotSet;
      this.fraction = -1.0;
      this.num = numberBuffer;
    }

    [SecuritySafeCritical]
    internal unsafe void AddNumber(int value)
    {
      this.num[this.numCount++] = value;
    }

    [SecuritySafeCritical]
    internal unsafe int GetNumber(int index)
    {
      return this.num[index];
    }
  }
}
