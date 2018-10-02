// Decompiled with JetBrains decompiler
// Type: System.ParamArrayAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает, что метод может быть вызван с переменным числом аргументов.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ParamArrayAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ParamArrayAttribute" /> стандартными свойствами.
    /// </summary>
    [__DynamicallyInvokable]
    public ParamArrayAttribute()
    {
    }
  }
}
