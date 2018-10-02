// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.CLRIReferenceImpl`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class CLRIReferenceImpl<T> : CLRIPropertyValueImpl, IReference<T>, IPropertyValue, ICustomPropertyProvider
  {
    private T _value;

    public CLRIReferenceImpl(PropertyType type, T obj)
      : base(type, (object) obj)
    {
      this._value = obj;
    }

    public T Value
    {
      get
      {
        return this._value;
      }
    }

    public override string ToString()
    {
      if ((object) this._value != null)
        return this._value.ToString();
      return base.ToString();
    }

    ICustomProperty ICustomPropertyProvider.GetCustomProperty(string name)
    {
      return ICustomPropertyProviderImpl.CreateProperty((object) this._value, name);
    }

    ICustomProperty ICustomPropertyProvider.GetIndexedProperty(string name, Type indexParameterType)
    {
      return ICustomPropertyProviderImpl.CreateIndexedProperty((object) this._value, name, indexParameterType);
    }

    string ICustomPropertyProvider.GetStringRepresentation()
    {
      return ((object) this._value).ToString();
    }

    Type ICustomPropertyProvider.Type
    {
      get
      {
        return ((object) this._value).GetType();
      }
    }

    [FriendAccessAllowed]
    internal static object UnboxHelper(object wrapper)
    {
      return (object) ((IReference<T>) wrapper).Value;
    }
  }
}
