// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IReflect
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
  [Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
  internal interface IReflect
  {
    MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

    MethodInfo GetMethod(string name, BindingFlags bindingAttr);

    MethodInfo[] GetMethods(BindingFlags bindingAttr);

    FieldInfo GetField(string name, BindingFlags bindingAttr);

    FieldInfo[] GetFields(BindingFlags bindingAttr);

    PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

    PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

    PropertyInfo[] GetProperties(BindingFlags bindingAttr);

    MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

    MemberInfo[] GetMembers(BindingFlags bindingAttr);

    object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

    Type UnderlyingSystemType { get; }
  }
}
