// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.TraceLoggingTypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal abstract class TraceLoggingTypeInfo
  {
    private readonly EventLevel level = ~EventLevel.LogAlways;
    private readonly EventOpcode opcode = ~EventOpcode.Info;
    private readonly string name;
    private readonly EventKeywords keywords;
    private readonly EventTags tags;
    private readonly Type dataType;

    internal TraceLoggingTypeInfo(Type dataType)
    {
      if (dataType == (Type) null)
        throw new ArgumentNullException(nameof (dataType));
      this.name = dataType.Name;
      this.dataType = dataType;
    }

    internal TraceLoggingTypeInfo(Type dataType, string name, EventLevel level, EventOpcode opcode, EventKeywords keywords, EventTags tags)
    {
      if (dataType == (Type) null)
        throw new ArgumentNullException(nameof (dataType));
      if (name == null)
        throw new ArgumentNullException("eventName");
      Statics.CheckName(name);
      this.name = name;
      this.keywords = keywords;
      this.level = level;
      this.opcode = opcode;
      this.tags = tags;
      this.dataType = dataType;
    }

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public EventLevel Level
    {
      get
      {
        return this.level;
      }
    }

    public EventOpcode Opcode
    {
      get
      {
        return this.opcode;
      }
    }

    public EventKeywords Keywords
    {
      get
      {
        return this.keywords;
      }
    }

    public EventTags Tags
    {
      get
      {
        return this.tags;
      }
    }

    internal Type DataType
    {
      get
      {
        return this.dataType;
      }
    }

    public abstract void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format);

    public abstract void WriteObjectData(TraceLoggingDataCollector collector, object value);

    public virtual object GetData(object value)
    {
      return value;
    }
  }
}
