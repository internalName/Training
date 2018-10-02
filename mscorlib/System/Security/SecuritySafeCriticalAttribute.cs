// Decompiled with JetBrains decompiler
// Type: System.Security.SecuritySafeCriticalAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>
  ///   Определяет типы или члены как критически важные для безопасности и безопасно доступные для прозрачного кода.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class SecuritySafeCriticalAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecuritySafeCriticalAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public SecuritySafeCriticalAttribute()
    {
    }
  }
}
