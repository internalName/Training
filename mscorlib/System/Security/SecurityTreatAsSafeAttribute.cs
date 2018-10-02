// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityTreatAsSafeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>
  ///   Определяет, какие из nonpublic <see cref="T:System.Security.SecurityCriticalAttribute" /> члены доступны из прозрачного кода в сборке.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [Obsolete("SecurityTreatAsSafe is only used for .NET 2.0 transparency compatibility.  Please use the SecuritySafeCriticalAttribute instead.")]
  public sealed class SecurityTreatAsSafeAttribute : Attribute
  {
  }
}
