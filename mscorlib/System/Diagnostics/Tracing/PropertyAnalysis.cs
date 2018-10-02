// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.PropertyAnalysis
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Diagnostics.Tracing
{
  internal sealed class PropertyAnalysis
  {
    internal readonly string name;
    internal readonly MethodInfo getterInfo;
    internal readonly TraceLoggingTypeInfo typeInfo;
    internal readonly EventFieldAttribute fieldAttribute;

    public PropertyAnalysis(string name, MethodInfo getterInfo, TraceLoggingTypeInfo typeInfo, EventFieldAttribute fieldAttribute)
    {
      this.name = name;
      this.getterInfo = getterInfo;
      this.typeInfo = typeInfo;
      this.fieldAttribute = fieldAttribute;
    }
  }
}
