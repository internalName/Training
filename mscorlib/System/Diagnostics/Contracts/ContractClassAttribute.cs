// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractClassAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>
  ///   Указывает, что отдельный тип содержит контракты кода для этого типа.
  /// </summary>
  [Conditional("CONTRACTS_FULL")]
  [Conditional("DEBUG")]
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class ContractClassAttribute : Attribute
  {
    private Type _typeWithContracts;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Contracts.ContractClassAttribute" />.
    /// </summary>
    /// <param name="typeContainingContracts">
    ///   Тип, содержащий контракты кода для этого типа.
    /// </param>
    [__DynamicallyInvokable]
    public ContractClassAttribute(Type typeContainingContracts)
    {
      this._typeWithContracts = typeContainingContracts;
    }

    /// <summary>
    ///   Возвращает тип, содержащий контракты кода для этого типа.
    /// </summary>
    /// <returns>Тип, содержащий контракты кода для этого типа.</returns>
    [__DynamicallyInvokable]
    public Type TypeContainingContracts
    {
      [__DynamicallyInvokable] get
      {
        return this._typeWithContracts;
      }
    }
  }
}
