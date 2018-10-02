// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.SymbolMethod
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Security;

namespace System.Reflection.Emit
{
  internal sealed class SymbolMethod : MethodInfo
  {
    private ModuleBuilder m_module;
    private Type m_containingType;
    private string m_name;
    private CallingConventions m_callingConvention;
    private Type m_returnType;
    private MethodToken m_mdMethod;
    private Type[] m_parameterTypes;
    private SignatureHelper m_signature;

    [SecurityCritical]
    internal SymbolMethod(ModuleBuilder mod, MethodToken token, Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      this.m_mdMethod = token;
      this.m_returnType = returnType;
      if (parameterTypes != null)
      {
        this.m_parameterTypes = new Type[parameterTypes.Length];
        Array.Copy((Array) parameterTypes, (Array) this.m_parameterTypes, parameterTypes.Length);
      }
      else
        this.m_parameterTypes = EmptyArray<Type>.Value;
      this.m_module = mod;
      this.m_containingType = arrayClass;
      this.m_name = methodName;
      this.m_callingConvention = callingConvention;
      this.m_signature = SignatureHelper.GetMethodSigHelper((Module) mod, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    internal override Type[] GetParameterTypes()
    {
      return this.m_parameterTypes;
    }

    internal MethodToken GetToken(ModuleBuilder mod)
    {
      return mod.GetArrayMethodToken(this.m_containingType, this.m_name, this.m_callingConvention, this.m_returnType, this.m_parameterTypes);
    }

    public override Module Module
    {
      get
      {
        return (Module) this.m_module;
      }
    }

    public override Type ReflectedType
    {
      get
      {
        return this.m_containingType;
      }
    }

    public override string Name
    {
      get
      {
        return this.m_name;
      }
    }

    public override Type DeclaringType
    {
      get
      {
        return this.m_containingType;
      }
    }

    public override ParameterInfo[] GetParameters()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
    }

    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
    }

    public override MethodAttributes Attributes
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
      }
    }

    public override CallingConventions CallingConvention
    {
      get
      {
        return this.m_callingConvention;
      }
    }

    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
      }
    }

    public override Type ReturnType
    {
      get
      {
        return this.m_returnType;
      }
    }

    public override ICustomAttributeProvider ReturnTypeCustomAttributes
    {
      get
      {
        return (ICustomAttributeProvider) null;
      }
    }

    public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
    }

    public override MethodInfo GetBaseDefinition()
    {
      return (MethodInfo) this;
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
    }

    public Module GetModule()
    {
      return (Module) this.m_module;
    }

    public MethodToken GetToken()
    {
      return this.m_mdMethod;
    }
  }
}
