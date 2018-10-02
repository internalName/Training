// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.MethodOnTypeBuilderInstantiation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Reflection.Emit
{
  internal sealed class MethodOnTypeBuilderInstantiation : MethodInfo
  {
    internal MethodInfo m_method;
    private TypeBuilderInstantiation m_type;

    internal static MethodInfo GetMethod(MethodInfo method, TypeBuilderInstantiation type)
    {
      return (MethodInfo) new MethodOnTypeBuilderInstantiation(method, type);
    }

    internal MethodOnTypeBuilderInstantiation(MethodInfo method, TypeBuilderInstantiation type)
    {
      this.m_method = method;
      this.m_type = type;
    }

    internal override Type[] GetParameterTypes()
    {
      return this.m_method.GetParameterTypes();
    }

    public override MemberTypes MemberType
    {
      get
      {
        return this.m_method.MemberType;
      }
    }

    public override string Name
    {
      get
      {
        return this.m_method.Name;
      }
    }

    public override Type DeclaringType
    {
      get
      {
        return (Type) this.m_type;
      }
    }

    public override Type ReflectedType
    {
      get
      {
        return (Type) this.m_type;
      }
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.m_method.GetCustomAttributes(inherit);
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.m_method.GetCustomAttributes(attributeType, inherit);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.m_method.IsDefined(attributeType, inherit);
    }

    internal int MetadataTokenInternal
    {
      get
      {
        MethodBuilder method = this.m_method as MethodBuilder;
        if ((MethodInfo) method != (MethodInfo) null)
          return method.MetadataTokenInternal;
        return this.m_method.MetadataToken;
      }
    }

    public override Module Module
    {
      get
      {
        return this.m_method.Module;
      }
    }

    public new Type GetType()
    {
      return base.GetType();
    }

    public override ParameterInfo[] GetParameters()
    {
      return this.m_method.GetParameters();
    }

    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      return this.m_method.GetMethodImplementationFlags();
    }

    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        return this.m_method.MethodHandle;
      }
    }

    public override MethodAttributes Attributes
    {
      get
      {
        return this.m_method.Attributes;
      }
    }

    public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      throw new NotSupportedException();
    }

    public override CallingConventions CallingConvention
    {
      get
      {
        return this.m_method.CallingConvention;
      }
    }

    public override Type[] GetGenericArguments()
    {
      return this.m_method.GetGenericArguments();
    }

    public override MethodInfo GetGenericMethodDefinition()
    {
      return this.m_method;
    }

    public override bool IsGenericMethodDefinition
    {
      get
      {
        return this.m_method.IsGenericMethodDefinition;
      }
    }

    public override bool ContainsGenericParameters
    {
      get
      {
        return this.m_method.ContainsGenericParameters;
      }
    }

    public override MethodInfo MakeGenericMethod(params Type[] typeArgs)
    {
      if (!this.IsGenericMethodDefinition)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition"));
      return MethodBuilderInstantiation.MakeGenericMethod((MethodInfo) this, typeArgs);
    }

    public override bool IsGenericMethod
    {
      get
      {
        return this.m_method.IsGenericMethod;
      }
    }

    public override Type ReturnType
    {
      get
      {
        return this.m_method.ReturnType;
      }
    }

    public override ParameterInfo ReturnParameter
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    public override ICustomAttributeProvider ReturnTypeCustomAttributes
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    public override MethodInfo GetBaseDefinition()
    {
      throw new NotSupportedException();
    }
  }
}
