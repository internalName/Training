// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.DateTimeOffsetTypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal sealed class DateTimeOffsetTypeInfo : TraceLoggingTypeInfo<DateTimeOffset>
  {
    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      TraceLoggingMetadataCollector metadataCollector = collector.AddGroup(name);
      metadataCollector.AddScalar("Ticks", Statics.MakeDataType(TraceLoggingDataType.FileTime, format));
      metadataCollector.AddScalar("Offset", TraceLoggingDataType.Int64);
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref DateTimeOffset value)
    {
      long ticks = value.Ticks;
      collector.AddScalar(ticks < 504911232000000000L ? 0L : ticks - 504911232000000000L);
      collector.AddScalar(value.Offset.Ticks);
    }
  }
}
