// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityTransparentAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>
  ///   Указывает, что сборка не может вызывать повышение уровня привилегий.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class SecurityTransparentAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecurityTransparentAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public SecurityTransparentAttribute()
    {
    }
  }
}
