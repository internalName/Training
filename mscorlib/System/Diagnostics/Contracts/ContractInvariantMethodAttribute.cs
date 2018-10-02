// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractInvariantMethodAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>Помечает метод как инвариантный метод для класса.</summary>
  [Conditional("CONTRACTS_FULL")]
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class ContractInvariantMethodAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Contracts.ContractInvariantMethodAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ContractInvariantMethodAttribute()
    {
    }
  }
}
