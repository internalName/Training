// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TCEAdapterGen.TCEAdapterGenerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
  internal class TCEAdapterGenerator
  {
    private Hashtable m_SrcItfToSrcItfInfoMap = new Hashtable();
    private ModuleBuilder m_Module;
    private static volatile CustomAttributeBuilder s_NoClassItfCABuilder;
    private static volatile CustomAttributeBuilder s_HiddenCABuilder;

    public void Process(ModuleBuilder ModBldr, ArrayList EventItfList)
    {
      this.m_Module = ModBldr;
      int count = EventItfList.Count;
      for (int index = 0; index < count; ++index)
      {
        EventItfInfo eventItf = (EventItfInfo) EventItfList[index];
        Type eventItfType = eventItf.GetEventItfType();
        Type srcItfType = eventItf.GetSrcItfType();
        string eventProviderName = eventItf.GetEventProviderName();
        Type SinkHelperType = new EventSinkHelperWriter(this.m_Module, srcItfType, eventItfType).Perform();
        new EventProviderWriter(this.m_Module, eventProviderName, eventItfType, srcItfType, SinkHelperType).Perform();
      }
    }

    internal static void SetClassInterfaceTypeToNone(TypeBuilder tb)
    {
      if (TCEAdapterGenerator.s_NoClassItfCABuilder == null)
        TCEAdapterGenerator.s_NoClassItfCABuilder = new CustomAttributeBuilder(typeof (ClassInterfaceAttribute).GetConstructor(new Type[1]
        {
          typeof (ClassInterfaceType)
        }), new object[1]
        {
          (object) ClassInterfaceType.None
        });
      tb.SetCustomAttribute(TCEAdapterGenerator.s_NoClassItfCABuilder);
    }

    internal static TypeBuilder DefineUniqueType(string strInitFullName, TypeAttributes attrs, Type BaseType, Type[] aInterfaceTypes, ModuleBuilder mb)
    {
      string str = strInitFullName;
      int num = 2;
      while (mb.GetType(str) != (Type) null)
      {
        str = strInitFullName + "_" + (object) num;
        ++num;
      }
      return mb.DefineType(str, attrs, BaseType, aInterfaceTypes);
    }

    internal static void SetHiddenAttribute(TypeBuilder tb)
    {
      if (TCEAdapterGenerator.s_HiddenCABuilder == null)
        TCEAdapterGenerator.s_HiddenCABuilder = new CustomAttributeBuilder(typeof (TypeLibTypeAttribute).GetConstructor(new Type[1]
        {
          typeof (TypeLibTypeFlags)
        }), new object[1]
        {
          (object) TypeLibTypeFlags.FHidden
        });
      tb.SetCustomAttribute(TCEAdapterGenerator.s_HiddenCABuilder);
    }

    internal static MethodInfo[] GetNonPropertyMethods(Type type)
    {
      ArrayList arrayList = new ArrayList((ICollection) type.GetMethods());
      foreach (PropertyInfo property in type.GetProperties())
      {
        foreach (MethodInfo accessor in property.GetAccessors())
        {
          for (int index = 0; index < arrayList.Count; ++index)
          {
            if ((MethodInfo) arrayList[index] == accessor)
              arrayList.RemoveAt(index);
          }
        }
      }
      MethodInfo[] methodInfoArray = new MethodInfo[arrayList.Count];
      arrayList.CopyTo((Array) methodInfoArray);
      return methodInfoArray;
    }

    internal static MethodInfo[] GetPropertyMethods(Type type)
    {
      type.GetMethods();
      ArrayList arrayList = new ArrayList();
      foreach (PropertyInfo property in type.GetProperties())
      {
        foreach (MethodInfo accessor in property.GetAccessors())
          arrayList.Add((object) accessor);
      }
      MethodInfo[] methodInfoArray = new MethodInfo[arrayList.Count];
      arrayList.CopyTo((Array) methodInfoArray);
      return methodInfoArray;
    }
  }
}
