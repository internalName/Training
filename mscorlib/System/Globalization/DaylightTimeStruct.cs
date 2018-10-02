// Decompiled with JetBrains decompiler
// Type: System.Globalization.DaylightTimeStruct
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  internal struct DaylightTimeStruct
  {
    public DaylightTimeStruct(DateTime start, DateTime end, TimeSpan delta)
    {
      this.Start = start;
      this.End = end;
      this.Delta = delta;
    }

    public DateTime Start { get; }

    public DateTime End { get; }

    public TimeSpan Delta { get; }
  }
}
