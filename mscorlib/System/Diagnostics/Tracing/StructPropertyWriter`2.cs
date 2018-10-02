// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.StructPropertyWriter`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal class StructPropertyWriter<ContainerType, ValueType> : PropertyAccessor<ContainerType>
  {
    private readonly TraceLoggingTypeInfo<ValueType> valueTypeInfo;
    private readonly StructPropertyWriter<ContainerType, ValueType>.Getter getter;

    public StructPropertyWriter(PropertyAnalysis property)
    {
      this.valueTypeInfo = (TraceLoggingTypeInfo<ValueType>) property.typeInfo;
      this.getter = (StructPropertyWriter<ContainerType, ValueType>.Getter) Statics.CreateDelegate(typeof (StructPropertyWriter<ContainerType, ValueType>.Getter), property.getterInfo);
    }

    public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
    {
      ValueType valueType = (object) container == null ? default (ValueType) : this.getter(ref container);
      this.valueTypeInfo.WriteData(collector, ref valueType);
    }

    public override object GetData(ContainerType container)
    {
      return (object) ((object) container == null ? default (ValueType) : this.getter(ref container));
    }

    private delegate ValueType Getter(ref ContainerType container);
  }
}
