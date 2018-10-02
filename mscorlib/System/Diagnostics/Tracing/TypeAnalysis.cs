// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.TypeAnalysis
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
  internal sealed class TypeAnalysis
  {
    internal readonly EventLevel level = ~EventLevel.LogAlways;
    internal readonly EventOpcode opcode = ~EventOpcode.Info;
    internal readonly PropertyAnalysis[] properties;
    internal readonly string name;
    internal readonly EventKeywords keywords;
    internal readonly EventTags tags;

    public TypeAnalysis(Type dataType, EventDataAttribute eventAttrib, List<Type> recursionCheck)
    {
      IEnumerable<PropertyInfo> properties = Statics.GetProperties(dataType);
      List<PropertyAnalysis> propertyAnalysisList = new List<PropertyAnalysis>();
      foreach (PropertyInfo propInfo in properties)
      {
        if (!Statics.HasCustomAttribute(propInfo, typeof (EventIgnoreAttribute)) && propInfo.CanRead && propInfo.GetIndexParameters().Length == 0)
        {
          MethodInfo getMethod = Statics.GetGetMethod(propInfo);
          if (!(getMethod == (MethodInfo) null) && !getMethod.IsStatic && getMethod.IsPublic)
          {
            TraceLoggingTypeInfo typeInfoInstance = Statics.GetTypeInfoInstance(propInfo.PropertyType, recursionCheck);
            EventFieldAttribute customAttribute = Statics.GetCustomAttribute<EventFieldAttribute>(propInfo);
            string name = customAttribute == null || customAttribute.Name == null ? (Statics.ShouldOverrideFieldName(propInfo.Name) ? typeInfoInstance.Name : propInfo.Name) : customAttribute.Name;
            propertyAnalysisList.Add(new PropertyAnalysis(name, getMethod, typeInfoInstance, customAttribute));
          }
        }
      }
      this.properties = propertyAnalysisList.ToArray();
      foreach (PropertyAnalysis property in this.properties)
      {
        TraceLoggingTypeInfo typeInfo = property.typeInfo;
        this.level = (EventLevel) Statics.Combine((int) typeInfo.Level, (int) this.level);
        this.opcode = (EventOpcode) Statics.Combine((int) typeInfo.Opcode, (int) this.opcode);
        this.keywords |= typeInfo.Keywords;
        this.tags |= typeInfo.Tags;
      }
      if (eventAttrib != null)
      {
        this.level = (EventLevel) Statics.Combine((int) eventAttrib.Level, (int) this.level);
        this.opcode = (EventOpcode) Statics.Combine((int) eventAttrib.Opcode, (int) this.opcode);
        this.keywords |= eventAttrib.Keywords;
        this.tags |= eventAttrib.Tags;
        this.name = eventAttrib.Name;
      }
      if (this.name != null)
        return;
      this.name = dataType.Name;
    }
  }
}
