// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.ArrayTypeInfo`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal sealed class ArrayTypeInfo<ElementType> : TraceLoggingTypeInfo<ElementType[]>
  {
    private readonly TraceLoggingTypeInfo<ElementType> elementInfo;

    public ArrayTypeInfo(TraceLoggingTypeInfo<ElementType> elementInfo)
    {
      this.elementInfo = elementInfo;
    }

    public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
    {
      collector.BeginBufferedArray();
      this.elementInfo.WriteMetadata(collector, name, format);
      collector.EndBufferedArray();
    }

    public override void WriteData(TraceLoggingDataCollector collector, ref ElementType[] value)
    {
      int bookmark = collector.BeginBufferedArray();
      int count = 0;
      if (value != null)
      {
        count = value.Length;
        for (int index = 0; index < value.Length; ++index)
          this.elementInfo.WriteData(collector, ref value[index]);
      }
      collector.EndBufferedArray(bookmark, count);
    }

    public override object GetData(object value)
    {
      ElementType[] elementTypeArray = (ElementType[]) value;
      object[] objArray = new object[elementTypeArray.Length];
      for (int index = 0; index < elementTypeArray.Length; ++index)
        objArray[index] = this.elementInfo.GetData((object) elementTypeArray[index]);
      return (object) objArray;
    }
  }
}
