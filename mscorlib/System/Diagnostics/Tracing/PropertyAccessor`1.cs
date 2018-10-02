// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.PropertyAccessor`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Tracing
{
  internal abstract class PropertyAccessor<ContainerType>
  {
    public abstract void Write(TraceLoggingDataCollector collector, ref ContainerType value);

    public abstract object GetData(ContainerType value);

    public static PropertyAccessor<ContainerType> Create(PropertyAnalysis property)
    {
      Type returnType = property.getterInfo.ReturnType;
      if (!Statics.IsValueType(typeof (ContainerType)))
      {
        if (returnType == typeof (int))
          return (PropertyAccessor<ContainerType>) new ClassPropertyWriter<ContainerType, int>(property);
        if (returnType == typeof (long))
          return (PropertyAccessor<ContainerType>) new ClassPropertyWriter<ContainerType, long>(property);
        if (returnType == typeof (string))
          return (PropertyAccessor<ContainerType>) new ClassPropertyWriter<ContainerType, string>(property);
      }
      return (PropertyAccessor<ContainerType>) new NonGenericProperytWriter<ContainerType>(property);
    }
  }
}
