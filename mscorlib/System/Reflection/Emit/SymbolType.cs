// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.SymbolType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  internal sealed class SymbolType : TypeInfo
  {
    private bool m_isSzArray = true;
    internal TypeKind m_typeKind;
    internal Type m_baseType;
    internal int m_cRank;
    internal int[] m_iaLowerBound;
    internal int[] m_iaUpperBound;
    private char[] m_bFormat;

    public override bool IsAssignableFrom(TypeInfo typeInfo)
    {
      if ((Type) typeInfo == (Type) null)
        return false;
      return this.IsAssignableFrom(typeInfo.AsType());
    }

    internal static Type FormCompoundType(char[] bFormat, Type baseType, int curIndex)
    {
      if (bFormat == null || curIndex == bFormat.Length)
        return baseType;
      if (bFormat[curIndex] == '&')
      {
        SymbolType symbolType = new SymbolType(TypeKind.IsByRef);
        symbolType.SetFormat(bFormat, curIndex, 1);
        ++curIndex;
        if (curIndex != bFormat.Length)
          throw new ArgumentException(Environment.GetResourceString("Argument_BadSigFormat"));
        symbolType.SetElementType(baseType);
        return (Type) symbolType;
      }
      if (bFormat[curIndex] == '[')
      {
        SymbolType symbolType = new SymbolType(TypeKind.IsArray);
        int curIndex1 = curIndex;
        ++curIndex;
        int lower = 0;
        int upper = -1;
        while (bFormat[curIndex] != ']')
        {
          if (bFormat[curIndex] == '*')
          {
            symbolType.m_isSzArray = false;
            ++curIndex;
          }
          if (bFormat[curIndex] >= '0' && bFormat[curIndex] <= '9' || bFormat[curIndex] == '-')
          {
            bool flag = false;
            if (bFormat[curIndex] == '-')
            {
              flag = true;
              ++curIndex;
            }
            for (; bFormat[curIndex] >= '0' && bFormat[curIndex] <= '9'; ++curIndex)
              lower = lower * 10 + ((int) bFormat[curIndex] - 48);
            if (flag)
              lower = -lower;
            upper = lower - 1;
          }
          if (bFormat[curIndex] == '.')
          {
            ++curIndex;
            if (bFormat[curIndex] != '.')
              throw new ArgumentException(Environment.GetResourceString("Argument_BadSigFormat"));
            ++curIndex;
            if (bFormat[curIndex] >= '0' && bFormat[curIndex] <= '9' || bFormat[curIndex] == '-')
            {
              bool flag = false;
              upper = 0;
              if (bFormat[curIndex] == '-')
              {
                flag = true;
                ++curIndex;
              }
              for (; bFormat[curIndex] >= '0' && bFormat[curIndex] <= '9'; ++curIndex)
                upper = upper * 10 + ((int) bFormat[curIndex] - 48);
              if (flag)
                upper = -upper;
              if (upper < lower)
                throw new ArgumentException(Environment.GetResourceString("Argument_BadSigFormat"));
            }
          }
          if (bFormat[curIndex] == ',')
          {
            ++curIndex;
            symbolType.SetBounds(lower, upper);
            lower = 0;
            upper = -1;
          }
          else if (bFormat[curIndex] != ']')
            throw new ArgumentException(Environment.GetResourceString("Argument_BadSigFormat"));
        }
        symbolType.SetBounds(lower, upper);
        ++curIndex;
        symbolType.SetFormat(bFormat, curIndex1, curIndex - curIndex1);
        symbolType.SetElementType(baseType);
        return SymbolType.FormCompoundType(bFormat, (Type) symbolType, curIndex);
      }
      if (bFormat[curIndex] != '*')
        return (Type) null;
      SymbolType symbolType1 = new SymbolType(TypeKind.IsPointer);
      symbolType1.SetFormat(bFormat, curIndex, 1);
      ++curIndex;
      symbolType1.SetElementType(baseType);
      return SymbolType.FormCompoundType(bFormat, (Type) symbolType1, curIndex);
    }

    internal SymbolType(TypeKind typeKind)
    {
      this.m_typeKind = typeKind;
      this.m_iaLowerBound = new int[4];
      this.m_iaUpperBound = new int[4];
    }

    internal void SetElementType(Type baseType)
    {
      if (baseType == (Type) null)
        throw new ArgumentNullException(nameof (baseType));
      this.m_baseType = baseType;
    }

    private void SetBounds(int lower, int upper)
    {
      if (lower != 0 || upper != -1)
        this.m_isSzArray = false;
      if (this.m_iaLowerBound.Length <= this.m_cRank)
      {
        int[] numArray = new int[this.m_cRank * 2];
        Array.Copy((Array) this.m_iaLowerBound, (Array) numArray, this.m_cRank);
        this.m_iaLowerBound = numArray;
        Array.Copy((Array) this.m_iaUpperBound, (Array) numArray, this.m_cRank);
        this.m_iaUpperBound = numArray;
      }
      this.m_iaLowerBound[this.m_cRank] = lower;
      this.m_iaUpperBound[this.m_cRank] = upper;
      ++this.m_cRank;
    }

    internal void SetFormat(char[] bFormat, int curIndex, int length)
    {
      char[] chArray = new char[length];
      Array.Copy((Array) bFormat, curIndex, (Array) chArray, 0, length);
      this.m_bFormat = chArray;
    }

    internal override bool IsSzArray
    {
      get
      {
        if (this.m_cRank > 1)
          return false;
        return this.m_isSzArray;
      }
    }

    public override Type MakePointerType()
    {
      return SymbolType.FormCompoundType((new string(this.m_bFormat) + "*").ToCharArray(), this.m_baseType, 0);
    }

    public override Type MakeByRefType()
    {
      return SymbolType.FormCompoundType((new string(this.m_bFormat) + "&").ToCharArray(), this.m_baseType, 0);
    }

    public override Type MakeArrayType()
    {
      return SymbolType.FormCompoundType((new string(this.m_bFormat) + "[]").ToCharArray(), this.m_baseType, 0);
    }

    public override Type MakeArrayType(int rank)
    {
      if (rank <= 0)
        throw new IndexOutOfRangeException();
      string str = "";
      if (rank == 1)
      {
        str = "*";
      }
      else
      {
        for (int index = 1; index < rank; ++index)
          str += ",";
      }
      return (Type) (SymbolType.FormCompoundType((new string(this.m_bFormat) + string.Format((IFormatProvider) CultureInfo.InvariantCulture, "[{0}]", (object) str)).ToCharArray(), this.m_baseType, 0) as SymbolType);
    }

    public override int GetArrayRank()
    {
      if (!this.IsArray)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
      return this.m_cRank;
    }

    public override Guid GUID
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
      }
    }

    public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override Module Module
    {
      get
      {
        Type baseType = this.m_baseType;
        while (baseType is SymbolType)
          baseType = ((SymbolType) baseType).m_baseType;
        return baseType.Module;
      }
    }

    public override Assembly Assembly
    {
      get
      {
        Type baseType = this.m_baseType;
        while (baseType is SymbolType)
          baseType = ((SymbolType) baseType).m_baseType;
        return baseType.Assembly;
      }
    }

    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
      }
    }

    public override string Name
    {
      get
      {
        string str = new string(this.m_bFormat);
        Type baseType;
        for (baseType = this.m_baseType; baseType is SymbolType; baseType = ((SymbolType) baseType).m_baseType)
          str = new string(((SymbolType) baseType).m_bFormat) + str;
        return baseType.Name + str;
      }
    }

    public override string FullName
    {
      get
      {
        return TypeNameBuilder.ToString((Type) this, TypeNameBuilder.Format.FullName);
      }
    }

    public override string AssemblyQualifiedName
    {
      get
      {
        return TypeNameBuilder.ToString((Type) this, TypeNameBuilder.Format.AssemblyQualifiedName);
      }
    }

    public override string ToString()
    {
      return TypeNameBuilder.ToString((Type) this, TypeNameBuilder.Format.ToString);
    }

    public override string Namespace
    {
      get
      {
        return this.m_baseType.Namespace;
      }
    }

    public override Type BaseType
    {
      get
      {
        return typeof (Array);
      }
    }

    protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    [ComVisible(true)]
    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override Type GetInterface(string name, bool ignoreCase)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override Type[] GetInterfaces()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override EventInfo[] GetEvents()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override Type GetNestedType(string name, BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    [ComVisible(true)]
    public override InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    protected override TypeAttributes GetAttributeFlagsImpl()
    {
      Type baseType = this.m_baseType;
      while (baseType is SymbolType)
        baseType = ((SymbolType) baseType).m_baseType;
      return baseType.Attributes;
    }

    protected override bool IsArrayImpl()
    {
      return this.m_typeKind == TypeKind.IsArray;
    }

    protected override bool IsPointerImpl()
    {
      return this.m_typeKind == TypeKind.IsPointer;
    }

    protected override bool IsByRefImpl()
    {
      return this.m_typeKind == TypeKind.IsByRef;
    }

    protected override bool IsPrimitiveImpl()
    {
      return false;
    }

    protected override bool IsValueTypeImpl()
    {
      return false;
    }

    protected override bool IsCOMObjectImpl()
    {
      return false;
    }

    public override bool IsConstructedGenericType
    {
      get
      {
        return false;
      }
    }

    public override Type GetElementType()
    {
      return this.m_baseType;
    }

    protected override bool HasElementTypeImpl()
    {
      return this.m_baseType != (Type) null;
    }

    public override Type UnderlyingSystemType
    {
      get
      {
        return (Type) this;
      }
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonReflectedType"));
    }
  }
}
