// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.UInt64TypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal sealed class UInt64TypeInfo : TraceLoggingTypeInfo<ulong>
  {
    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref ulong value)
    {
      collector.AddScalar(value);
    }
  }
}
