// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.NullableTypeInfo`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
  internal sealed class NullableTypeInfo<T> : TraceLoggingTypeInfo<T?> where T : struct
  {
    private readonly TraceLoggingTypeInfo<T> valueInfo;

    public NullableTypeInfo(List<Type> recursionCheck)
    {
      this.valueInfo = TraceLoggingTypeInfo<T>.GetInstance(recursionCheck);
    }

    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      TraceLoggingMetadataCollector collector1 = collector.AddGroup(name);
      collector1.AddScalar("HasValue", TraceLoggingDataType.Boolean8);
      this.valueInfo.WriteMetadata(collector1, "Value", format);
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref T? value)
    {
      bool hasValue = value.HasValue;
      collector.AddScalar(hasValue);
      T obj = hasValue ? value.Value : default (T);
      this.valueInfo.WriteData(collector, ref obj);
    }
  }
}
