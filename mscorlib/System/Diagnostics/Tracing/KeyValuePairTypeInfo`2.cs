﻿// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.KeyValuePairTypeInfo`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
  internal sealed class KeyValuePairTypeInfo<K, V> : TraceLoggingTypeInfo<KeyValuePair<K, V>>
  {
    private readonly TraceLoggingTypeInfo<K> keyInfo;
    private readonly TraceLoggingTypeInfo<V> valueInfo;

    public KeyValuePairTypeInfo(List<Type> recursionCheck)
    {
      this.keyInfo = TraceLoggingTypeInfo<K>.GetInstance(recursionCheck);
      this.valueInfo = TraceLoggingTypeInfo<V>.GetInstance(recursionCheck);
    }

    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      TraceLoggingMetadataCollector collector1 = collector.AddGroup(name);
      this.keyInfo.WriteMetadata(collector1, "Key", EventFieldFormat.Default);
      this.valueInfo.WriteMetadata(collector1, "Value", format);
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref KeyValuePair<K, V> value)
    {
      K key = value.Key;
      V v = value.Value;
      this.keyInfo.WriteData(collector, ref key);
      this.valueInfo.WriteData(collector, ref v);
    }

    public override object GetData(object value)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      KeyValuePair<K, V> keyValuePair = (KeyValuePair<K, V>) value;
      dictionary.Add("Key", this.keyInfo.GetData((object) keyValuePair.Key));
      dictionary.Add("Value", this.valueInfo.GetData((object) keyValuePair.Value));
      return (object) dictionary;
    }
  }
}
