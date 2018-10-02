// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.CustomConstantAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Определяет постоянное значение, которое компилятор может сохранять для поля или параметра метода.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class CustomConstantAttribute : Attribute
  {
    /// <summary>
    ///   Возвращает постоянное значение, хранящееся в данном атрибуте.
    /// </summary>
    /// <returns>Постоянное значение, хранящееся в данном атрибуте.</returns>
    [__DynamicallyInvokable]
    public abstract object Value { [__DynamicallyInvokable] get; }

    internal static object GetRawConstant(CustomAttributeData attr)
    {
      foreach (CustomAttributeNamedArgument namedArgument in (IEnumerable<CustomAttributeNamedArgument>) attr.NamedArguments)
      {
        if (namedArgument.MemberInfo.Name.Equals("Value"))
          return namedArgument.TypedValue.Value;
      }
      return (object) DBNull.Value;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.CustomConstantAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected CustomConstantAttribute()
    {
    }
  }
}
