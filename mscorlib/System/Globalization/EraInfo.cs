// Decompiled with JetBrains decompiler
// Type: System.Globalization.EraInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Globalization
{
  [Serializable]
  internal class EraInfo
  {
    internal int era;
    internal long ticks;
    internal int yearOffset;
    internal int minEraYear;
    internal int maxEraYear;
    [OptionalField(VersionAdded = 4)]
    internal string eraName;
    [OptionalField(VersionAdded = 4)]
    internal string abbrevEraName;
    [OptionalField(VersionAdded = 4)]
    internal string englishEraName;

    internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear)
    {
      this.era = era;
      this.yearOffset = yearOffset;
      this.minEraYear = minEraYear;
      this.maxEraYear = maxEraYear;
      this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
    }

    internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear, string eraName, string abbrevEraName, string englishEraName)
    {
      this.era = era;
      this.yearOffset = yearOffset;
      this.minEraYear = minEraYear;
      this.maxEraYear = maxEraYear;
      this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
      this.eraName = eraName;
      this.abbrevEraName = abbrevEraName;
      this.englishEraName = englishEraName;
    }
  }
}
