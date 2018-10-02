// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.CustomPropertyImpl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class CustomPropertyImpl : ICustomProperty
  {
    private PropertyInfo m_property;

    public CustomPropertyImpl(PropertyInfo propertyInfo)
    {
      if (propertyInfo == (PropertyInfo) null)
        throw new ArgumentNullException(nameof (propertyInfo));
      this.m_property = propertyInfo;
    }

    public string Name
    {
      get
      {
        return this.m_property.Name;
      }
    }

    public bool CanRead
    {
      get
      {
        return this.m_property.GetGetMethod() != (MethodInfo) null;
      }
    }

    public bool CanWrite
    {
      get
      {
        return this.m_property.GetSetMethod() != (MethodInfo) null;
      }
    }

    public object GetValue(object target)
    {
      return this.InvokeInternal(target, (object[]) null, true);
    }

    public object GetValue(object target, object indexValue)
    {
      return this.InvokeInternal(target, new object[1]
      {
        indexValue
      }, true);
    }

    public void SetValue(object target, object value)
    {
      this.InvokeInternal(target, new object[1]{ value }, false);
    }

    public void SetValue(object target, object value, object indexValue)
    {
      this.InvokeInternal(target, new object[2]
      {
        indexValue,
        value
      }, false);
    }

    [SecuritySafeCritical]
    private object InvokeInternal(object target, object[] args, bool getValue)
    {
      IGetProxyTarget getProxyTarget = target as IGetProxyTarget;
      if (getProxyTarget != null)
        target = getProxyTarget.GetTarget();
      MethodInfo methodInfo = getValue ? this.m_property.GetGetMethod(true) : this.m_property.GetSetMethod(true);
      if (methodInfo == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString(getValue ? "Arg_GetMethNotFnd" : "Arg_SetMethNotFnd"));
      if (!methodInfo.IsPublic)
        throw new MethodAccessException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Arg_MethodAccessException_WithMethodName"), (object) methodInfo.ToString(), (object) methodInfo.DeclaringType.FullName));
      RuntimeMethodInfo runtimeMethodInfo = methodInfo as RuntimeMethodInfo;
      if ((MethodInfo) runtimeMethodInfo == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
      return runtimeMethodInfo.UnsafeInvoke(target, BindingFlags.Default, (Binder) null, args, (CultureInfo) null);
    }

    public Type Type
    {
      get
      {
        return this.m_property.PropertyType;
      }
    }
  }
}
