﻿// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.DecimalTypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal sealed class DecimalTypeInfo : TraceLoggingTypeInfo<Decimal>
  {
    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Double, format));
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref Decimal value)
    {
      collector.AddScalar((double) value);
    }
  }
}
