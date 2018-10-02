// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ICustomPropertyProviderImpl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal static class ICustomPropertyProviderImpl
  {
    internal static ICustomProperty CreateProperty(object target, string propertyName)
    {
      PropertyInfo property = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
      if (property == (PropertyInfo) null)
        return (ICustomProperty) null;
      return (ICustomProperty) new CustomPropertyImpl(property);
    }

    [SecurityCritical]
    internal static unsafe ICustomProperty CreateIndexedProperty(object target, string propertyName, TypeNameNative* pIndexedParamType)
    {
      Type managedType = (Type) null;
      SystemTypeMarshaler.ConvertToManaged(pIndexedParamType, ref managedType);
      return ICustomPropertyProviderImpl.CreateIndexedProperty(target, propertyName, managedType);
    }

    internal static ICustomProperty CreateIndexedProperty(object target, string propertyName, Type indexedParamType)
    {
      PropertyInfo property = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, (Type) null, new Type[1]
      {
        indexedParamType
      }, (ParameterModifier[]) null);
      if (property == (PropertyInfo) null)
        return (ICustomProperty) null;
      return (ICustomProperty) new CustomPropertyImpl(property);
    }

    [SecurityCritical]
    internal static unsafe void GetType(object target, TypeNameNative* pIndexedParamType)
    {
      SystemTypeMarshaler.ConvertToNative(target.GetType(), pIndexedParamType);
    }
  }
}
