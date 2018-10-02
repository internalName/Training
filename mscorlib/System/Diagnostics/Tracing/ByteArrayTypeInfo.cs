// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.ByteArrayTypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal sealed class ByteArrayTypeInfo : TraceLoggingTypeInfo<byte[]>
  {
    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      switch (format)
      {
        case EventFieldFormat.String:
          collector.AddBinary(name, TraceLoggingDataType.CountedMbcsString);
          break;
        case EventFieldFormat.Boolean:
          collector.AddArray(name, TraceLoggingDataType.Boolean8);
          break;
        case EventFieldFormat.Hexadecimal:
          collector.AddArray(name, TraceLoggingDataType.HexInt8);
          break;
        case EventFieldFormat.Xml:
          collector.AddBinary(name, TraceLoggingDataType.CountedMbcsXml);
          break;
        case EventFieldFormat.Json:
          collector.AddBinary(name, TraceLoggingDataType.CountedMbcsJson);
          break;
        default:
          collector.AddBinary(name, Statics.MakeDataType(TraceLoggingDataType.Binary, format));
          break;
      }
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref byte[] value)
    {
      collector.AddBinary(value);
    }
  }
}
