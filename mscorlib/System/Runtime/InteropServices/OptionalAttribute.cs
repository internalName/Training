// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.OptionalAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>Указывает, что параметр является необязательным.</summary>
  [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class OptionalAttribute : Attribute
  {
    internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
    {
      if (!parameter.IsOptional)
        return (Attribute) null;
      return (Attribute) new OptionalAttribute();
    }

    internal static bool IsDefined(RuntimeParameterInfo parameter)
    {
      return parameter.IsOptional;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="OptionalAttribute" /> со значениями по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public OptionalAttribute()
    {
    }
  }
}
