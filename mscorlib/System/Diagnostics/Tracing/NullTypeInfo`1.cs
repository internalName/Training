// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.NullTypeInfo`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal sealed class NullTypeInfo<DataType> : TraceLoggingTypeInfo<DataType>
  {
    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      collector.AddGroup(name);
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref DataType value)
    {
    }

    public override object GetData(object value)
    {
      return (object) null;
    }
  }
}
