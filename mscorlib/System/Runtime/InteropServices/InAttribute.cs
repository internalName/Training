// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.InAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, что данные необходимо маршалировать из вызывающего в вызываемый объект и не возвращать вызывающему объекту.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class InAttribute : Attribute
  {
    internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
    {
      if (!parameter.IsIn)
        return (Attribute) null;
      return (Attribute) new InAttribute();
    }

    internal static bool IsDefined(RuntimeParameterInfo parameter)
    {
      return parameter.IsIn;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.InAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public InAttribute()
    {
    }
  }
}
