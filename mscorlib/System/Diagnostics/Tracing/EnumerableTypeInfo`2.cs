// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EnumerableTypeInfo`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
  internal sealed class EnumerableTypeInfo<IterableType, ElementType> : TraceLoggingTypeInfo<IterableType> where IterableType : IEnumerable<ElementType>
  {
    private readonly TraceLoggingTypeInfo<ElementType> elementInfo;

    public EnumerableTypeInfo(TraceLoggingTypeInfo<ElementType> elementInfo)
    {
      this.elementInfo = elementInfo;
    }

    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      collector.BeginBufferedArray();
      this.elementInfo.WriteMetadata(collector, name, format);
      collector.EndBufferedArray();
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref IterableType value)
    {
      int bookmark = collector.BeginBufferedArray();
      int count = 0;
      if ((object) value != null)
      {
        using (IEnumerator<ElementType> enumerator = value.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            ElementType current = enumerator.Current;
            this.elementInfo.WriteData(collector, ref current);
            ++count;
          }
        }
      }
      collector.EndBufferedArray(bookmark, count);
    }

    public override object GetData(object value)
    {
      IterableType iterableType = (IterableType) value;
      List<object> objectList = new List<object>();
      using (IEnumerator<ElementType> enumerator = iterableType.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ElementType current = enumerator.Current;
          objectList.Add(this.elementInfo.GetData((object) current));
        }
      }
      return (object) objectList.ToArray();
    }
  }
}
