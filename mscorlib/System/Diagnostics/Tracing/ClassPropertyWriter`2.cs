// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.ClassPropertyWriter`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal class ClassPropertyWriter<ContainerType, ValueType> : PropertyAccessor<ContainerType>
  {
    private readonly TraceLoggingTypeInfo<ValueType> valueTypeInfo;
    private readonly ClassPropertyWriter<ContainerType, ValueType>.Getter getter;

    public ClassPropertyWriter(PropertyAnalysis property)
    {
      this.valueTypeInfo = (TraceLoggingTypeInfo<ValueType>) property.typeInfo;
      this.getter = (ClassPropertyWriter<ContainerType, ValueType>.Getter) Statics.CreateDelegate(typeof (ClassPropertyWriter<ContainerType, ValueType>.Getter), property.getterInfo);
    }

    public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
    {
      ValueType valueType = (object) container == null ? default (ValueType) : this.getter(container);
      this.valueTypeInfo.WriteData(collector, ref valueType);
    }

    public override object GetData(ContainerType container)
    {
      return (object) ((object) container == null ? default (ValueType) : this.getter(container));
    }

    private delegate ValueType Getter(ContainerType container);
  }
}
