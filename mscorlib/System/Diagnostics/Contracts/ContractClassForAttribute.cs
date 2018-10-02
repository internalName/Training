// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractClassForAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>Указывает, что класс является контрактом для типа.</summary>
  [Conditional("CONTRACTS_FULL")]
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class ContractClassForAttribute : Attribute
  {
    private Type _typeIAmAContractFor;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.Contracts.ContractClassForAttribute" /> указанием текущий класс является контрактом для типа.
    /// </summary>
    /// <param name="typeContractsAreFor">
    ///   Тип текущий класс является контрактом.
    /// </param>
    [__DynamicallyInvokable]
    public ContractClassForAttribute(Type typeContractsAreFor)
    {
      this._typeIAmAContractFor = typeContractsAreFor;
    }

    /// <summary>
    ///   Возвращает тип, к которому применяется этот контракт кода.
    /// </summary>
    /// <returns>Тип, к которому применяется этот контракт.</returns>
    [__DynamicallyInvokable]
    public Type TypeContractsAreFor
    {
      [__DynamicallyInvokable] get
      {
        return this._typeIAmAContractFor;
      }
    }
  }
}
