// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.TraceLoggingTypeInfo`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
  internal abstract class TraceLoggingTypeInfo<DataType> : TraceLoggingTypeInfo
  {
    private static TraceLoggingTypeInfo<DataType> instance;

    protected TraceLoggingTypeInfo()
      : base(typeof (DataType))
    {
    }

    protected TraceLoggingTypeInfo(string name, EventLevel level, EventOpcode opcode, EventKeywords keywords, EventTags tags)
      : base(typeof (DataType), name, level, opcode, keywords, tags)
    {
    }

    public static TraceLoggingTypeInfo<DataType> Instance
    {
      get
      {
        return TraceLoggingTypeInfo<DataType>.instance ?? TraceLoggingTypeInfo<DataType>.InitInstance();
      }
    }

    public abstract void WriteData(TraceLoggingDataCollector collector, ref DataType value);

    public override void WriteObjectData(TraceLoggingDataCollector collector, object value)
    {
      DataType dataType = value == null ? default (DataType) : (DataType) value;
      this.WriteData(collector, ref dataType);
    }

    internal static TraceLoggingTypeInfo<DataType> GetInstance(List<Type> recursionCheck)
    {
      if (TraceLoggingTypeInfo<DataType>.instance == null)
      {
        int count = recursionCheck.Count;
        TraceLoggingTypeInfo<DataType> defaultTypeInfo = Statics.CreateDefaultTypeInfo<DataType>(recursionCheck);
        Interlocked.CompareExchange<TraceLoggingTypeInfo<DataType>>(ref TraceLoggingTypeInfo<DataType>.instance, defaultTypeInfo, (TraceLoggingTypeInfo<DataType>) null);
        recursionCheck.RemoveRange(count, recursionCheck.Count - count);
      }
      return TraceLoggingTypeInfo<DataType>.instance;
    }

    private static TraceLoggingTypeInfo<DataType> InitInstance()
    {
      return TraceLoggingTypeInfo<DataType>.GetInstance(new List<Type>());
    }
  }
}
