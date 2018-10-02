// Decompiled with JetBrains decompiler
// Type: System.__Filters
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System
{
  [Serializable]
  internal class __Filters
  {
    internal static readonly __Filters Instance = new __Filters();

    internal virtual bool FilterAttribute(MemberInfo m, object filterCriteria)
    {
      if (filterCriteria == null)
        throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritInt"));
      switch (m.MemberType)
      {
        case MemberTypes.Constructor:
        case MemberTypes.Method:
          MethodAttributes methodAttributes1;
          try
          {
            methodAttributes1 = (MethodAttributes) filterCriteria;
          }
          catch
          {
            throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritInt"));
          }
          MethodAttributes methodAttributes2 = m.MemberType != MemberTypes.Method ? ((MethodBase) m).Attributes : ((MethodBase) m).Attributes;
          return ((methodAttributes1 & MethodAttributes.MemberAccessMask) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.MemberAccessMask) == (methodAttributes1 & MethodAttributes.MemberAccessMask)) && ((methodAttributes1 & MethodAttributes.Static) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Static) != MethodAttributes.PrivateScope) && (((methodAttributes1 & MethodAttributes.Final) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Final) != MethodAttributes.PrivateScope) && ((methodAttributes1 & MethodAttributes.Virtual) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)) && (((methodAttributes1 & MethodAttributes.Abstract) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Abstract) != MethodAttributes.PrivateScope) && ((methodAttributes1 & MethodAttributes.SpecialName) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.SpecialName) != MethodAttributes.PrivateScope));
        case MemberTypes.Field:
          FieldAttributes fieldAttributes;
          try
          {
            fieldAttributes = (FieldAttributes) filterCriteria;
          }
          catch
          {
            throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritInt"));
          }
          FieldAttributes attributes = ((FieldInfo) m).Attributes;
          return ((fieldAttributes & FieldAttributes.FieldAccessMask) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.FieldAccessMask) == (fieldAttributes & FieldAttributes.FieldAccessMask)) && ((fieldAttributes & FieldAttributes.Static) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope) && (((fieldAttributes & FieldAttributes.InitOnly) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.Literal) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.Literal) != FieldAttributes.PrivateScope)) && (((fieldAttributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.NotSerialized) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.PinvokeImpl) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.PinvokeImpl) != FieldAttributes.PrivateScope));
        default:
          return false;
      }
    }

    internal virtual bool FilterName(MemberInfo m, object filterCriteria)
    {
      if (filterCriteria == null || !(filterCriteria is string))
        throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritString"));
      string str1 = ((string) filterCriteria).Trim();
      string str2 = m.Name;
      if (m.MemberType == MemberTypes.NestedType)
        str2 = str2.Substring(str2.LastIndexOf('+') + 1);
      if (str1.Length <= 0 || str1[str1.Length - 1] != '*')
        return str2.Equals(str1);
      string str3 = str1.Substring(0, str1.Length - 1);
      return str2.StartsWith(str3, StringComparison.Ordinal);
    }

    internal virtual bool FilterIgnoreCase(MemberInfo m, object filterCriteria)
    {
      if (filterCriteria == null || !(filterCriteria is string))
        throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritString"));
      string strA = ((string) filterCriteria).Trim();
      string str = m.Name;
      if (m.MemberType == MemberTypes.NestedType)
        str = str.Substring(str.LastIndexOf('+') + 1);
      if (strA.Length <= 0 || strA[strA.Length - 1] != '*')
        return string.Compare(strA, str, StringComparison.OrdinalIgnoreCase) == 0;
      string strB = strA.Substring(0, strA.Length - 1);
      return string.Compare(str, 0, strB, 0, strB.Length, StringComparison.OrdinalIgnoreCase) == 0;
    }
  }
}
