// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractArgumentValidatorAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>
  ///   Включает факторизацию устаревшего <see langword="if-then-throw" /> в отдельные методы для повторного использования кода и обеспечивает полный контроль над созданными исключениями и аргументами.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  [Conditional("CONTRACTS_FULL")]
  [__DynamicallyInvokable]
  public sealed class ContractArgumentValidatorAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Contracts.ContractArgumentValidatorAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ContractArgumentValidatorAttribute()
    {
    }
  }
}
