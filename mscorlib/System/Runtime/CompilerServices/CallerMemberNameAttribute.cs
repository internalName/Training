// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.CallerMemberNameAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Позволяет получить имя свойства или метода вызывающего метод объекта.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class CallerMemberNameAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public CallerMemberNameAttribute()
    {
    }
  }
}
