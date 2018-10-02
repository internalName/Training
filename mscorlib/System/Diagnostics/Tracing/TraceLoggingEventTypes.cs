// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.TraceLoggingEventTypes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
  internal class TraceLoggingEventTypes
  {
    internal readonly TraceLoggingTypeInfo[] typeInfos;
    internal readonly string name;
    internal readonly EventTags tags;
    internal readonly byte level;
    internal readonly byte opcode;
    internal readonly EventKeywords keywords;
    internal readonly byte[] typeMetadata;
    internal readonly int scratchSize;
    internal readonly int dataCount;
    internal readonly int pinCount;
    private ConcurrentSet<KeyValuePair<string, EventTags>, NameInfo> nameInfos;

    internal TraceLoggingEventTypes(string name, EventTags tags, params Type[] types)
      : this(tags, name, TraceLoggingEventTypes.MakeArray(types))
    {
    }

    internal TraceLoggingEventTypes(string name, EventTags tags, params TraceLoggingTypeInfo[] typeInfos)
      : this(tags, name, TraceLoggingEventTypes.MakeArray(typeInfos))
    {
    }

    internal TraceLoggingEventTypes(string name, EventTags tags, ParameterInfo[] paramInfos)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      this.typeInfos = this.MakeArray(paramInfos);
      this.name = name;
      this.tags = tags;
      this.level = (byte) 5;
      TraceLoggingMetadataCollector collector = new TraceLoggingMetadataCollector();
      for (int index = 0; index < this.typeInfos.Length; ++index)
      {
        TraceLoggingTypeInfo typeInfo = this.typeInfos[index];
        this.level = Statics.Combine((int) typeInfo.Level, this.level);
        this.opcode = Statics.Combine((int) typeInfo.Opcode, this.opcode);
        this.keywords |= typeInfo.Keywords;
        string name1 = paramInfos[index].Name;
        if (Statics.ShouldOverrideFieldName(name1))
          name1 = typeInfo.Name;
        typeInfo.WriteMetadata(collector, name1, EventFieldFormat.Default);
      }
      this.typeMetadata = collector.GetMetadata();
      this.scratchSize = collector.ScratchSize;
      this.dataCount = collector.DataCount;
      this.pinCount = collector.PinCount;
    }

    private TraceLoggingEventTypes(EventTags tags, string defaultName, TraceLoggingTypeInfo[] typeInfos)
    {
      if (defaultName == null)
        throw new ArgumentNullException(nameof (defaultName));
      this.typeInfos = typeInfos;
      this.name = defaultName;
      this.tags = tags;
      this.level = (byte) 5;
      TraceLoggingMetadataCollector collector = new TraceLoggingMetadataCollector();
      foreach (TraceLoggingTypeInfo typeInfo in typeInfos)
      {
        this.level = Statics.Combine((int) typeInfo.Level, this.level);
        this.opcode = Statics.Combine((int) typeInfo.Opcode, this.opcode);
        this.keywords |= typeInfo.Keywords;
        typeInfo.WriteMetadata(collector, (string) null, EventFieldFormat.Default);
      }
      this.typeMetadata = collector.GetMetadata();
      this.scratchSize = collector.ScratchSize;
      this.dataCount = collector.DataCount;
      this.pinCount = collector.PinCount;
    }

    internal string Name
    {
      get
      {
        return this.name;
      }
    }

    internal EventLevel Level
    {
      get
      {
        return (EventLevel) this.level;
      }
    }

    internal EventOpcode Opcode
    {
      get
      {
        return (EventOpcode) this.opcode;
      }
    }

    internal EventKeywords Keywords
    {
      get
      {
        return this.keywords;
      }
    }

    internal EventTags Tags
    {
      get
      {
        return this.tags;
      }
    }

    internal NameInfo GetNameInfo(string name, EventTags tags)
    {
      return this.nameInfos.TryGet(new KeyValuePair<string, EventTags>(name, tags)) ?? this.nameInfos.GetOrAdd(new NameInfo(name, tags, this.typeMetadata.Length));
    }

    private TraceLoggingTypeInfo[] MakeArray(ParameterInfo[] paramInfos)
    {
      if (paramInfos == null)
        throw new ArgumentNullException(nameof (paramInfos));
      List<Type> recursionCheck = new List<Type>(paramInfos.Length);
      TraceLoggingTypeInfo[] traceLoggingTypeInfoArray = new TraceLoggingTypeInfo[paramInfos.Length];
      for (int index = 0; index < paramInfos.Length; ++index)
        traceLoggingTypeInfoArray[index] = Statics.GetTypeInfoInstance(paramInfos[index].ParameterType, recursionCheck);
      return traceLoggingTypeInfoArray;
    }

    private static TraceLoggingTypeInfo[] MakeArray(Type[] types)
    {
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      List<Type> recursionCheck = new List<Type>(types.Length);
      TraceLoggingTypeInfo[] traceLoggingTypeInfoArray = new TraceLoggingTypeInfo[types.Length];
      for (int index = 0; index < types.Length; ++index)
        traceLoggingTypeInfoArray[index] = Statics.GetTypeInfoInstance(types[index], recursionCheck);
      return traceLoggingTypeInfoArray;
    }

    private static TraceLoggingTypeInfo[] MakeArray(TraceLoggingTypeInfo[] typeInfos)
    {
      if (typeInfos == null)
        throw new ArgumentNullException(nameof (typeInfos));
      return (TraceLoggingTypeInfo[]) typeInfos.Clone();
    }
  }
}
