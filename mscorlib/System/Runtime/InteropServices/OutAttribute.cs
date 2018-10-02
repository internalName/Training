// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.OutAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, что данные необходимо маршалировать из вызываемого объекта обратно в вызывающий объект.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class OutAttribute : Attribute
  {
    internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
    {
      if (!parameter.IsOut)
        return (Attribute) null;
      return (Attribute) new OutAttribute();
    }

    internal static bool IsDefined(RuntimeParameterInfo parameter)
    {
      return parameter.IsOut;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.OutAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public OutAttribute()
    {
    }
  }
}
