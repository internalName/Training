// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.NonGenericProperytWriter`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Diagnostics.Tracing
{
  internal class NonGenericProperytWriter<ContainerType> : PropertyAccessor<ContainerType>
  {
    private readonly TraceLoggingTypeInfo typeInfo;
    private readonly MethodInfo getterInfo;

    public NonGenericProperytWriter(PropertyAnalysis property)
    {
      this.getterInfo = property.getterInfo;
      this.typeInfo = property.typeInfo;
    }

    public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
    {
      object obj = (object) container == null ? (object) null : this.getterInfo.Invoke((object) container, (object[]) null);
      this.typeInfo.WriteObjectData(collector, obj);
    }

    public override object GetData(ContainerType container)
    {
      if ((object) container != null)
        return this.getterInfo.Invoke((object) container, (object[]) null);
      return (object) null;
    }
  }
}
