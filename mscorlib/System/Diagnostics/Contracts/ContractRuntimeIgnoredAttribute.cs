// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractRuntimeIgnoredAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>
  ///   Определяет член, для которого нет поведения во время выполнения.
  /// </summary>
  [Conditional("CONTRACTS_FULL")]
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  [__DynamicallyInvokable]
  public sealed class ContractRuntimeIgnoredAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Contracts.ContractRuntimeIgnoredAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ContractRuntimeIgnoredAttribute()
    {
    }
  }
}
