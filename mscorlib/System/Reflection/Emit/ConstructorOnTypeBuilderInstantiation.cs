// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ConstructorOnTypeBuilderInstantiation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Reflection.Emit
{
  internal sealed class ConstructorOnTypeBuilderInstantiation : ConstructorInfo
  {
    internal ConstructorInfo m_ctor;
    private TypeBuilderInstantiation m_type;

    internal static ConstructorInfo GetConstructor(ConstructorInfo Constructor, TypeBuilderInstantiation type)
    {
      return (ConstructorInfo) new ConstructorOnTypeBuilderInstantiation(Constructor, type);
    }

    internal ConstructorOnTypeBuilderInstantiation(ConstructorInfo constructor, TypeBuilderInstantiation type)
    {
      this.m_ctor = constructor;
      this.m_type = type;
    }

    internal override Type[] GetParameterTypes()
    {
      return this.m_ctor.GetParameterTypes();
    }

    internal override Type GetReturnType()
    {
      return this.DeclaringType;
    }

    public override MemberTypes MemberType
    {
      get
      {
        return this.m_ctor.MemberType;
      }
    }

    public override string Name
    {
      get
      {
        return this.m_ctor.Name;
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
      return this.m_ctor.GetCustomAttributes(inherit);
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.m_ctor.GetCustomAttributes(attributeType, inherit);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.m_ctor.IsDefined(attributeType, inherit);
    }

    internal int MetadataTokenInternal
    {
      get
      {
        ConstructorBuilder ctor = this.m_ctor as ConstructorBuilder;
        if ((ConstructorInfo) ctor != (ConstructorInfo) null)
          return ctor.MetadataTokenInternal;
        return this.m_ctor.MetadataToken;
      }
    }

    public override Module Module
    {
      get
      {
        return this.m_ctor.Module;
      }
    }

    public new Type GetType()
    {
      return base.GetType();
    }

    public override ParameterInfo[] GetParameters()
    {
      return this.m_ctor.GetParameters();
    }

    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      return this.m_ctor.GetMethodImplementationFlags();
    }

    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        return this.m_ctor.MethodHandle;
      }
    }

    public override MethodAttributes Attributes
    {
      get
      {
        return this.m_ctor.Attributes;
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
        return this.m_ctor.CallingConvention;
      }
    }

    public override Type[] GetGenericArguments()
    {
      return this.m_ctor.GetGenericArguments();
    }

    public override bool IsGenericMethodDefinition
    {
      get
      {
        return false;
      }
    }

    public override bool ContainsGenericParameters
    {
      get
      {
        return false;
      }
    }

    public override bool IsGenericMethod
    {
      get
      {
        return false;
      }
    }

    public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      throw new InvalidOperationException();
    }
  }
}
