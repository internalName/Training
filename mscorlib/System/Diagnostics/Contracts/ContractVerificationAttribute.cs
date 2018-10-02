// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractVerificationAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>
  ///   Указывает, что средствам анализа следует принимать правильность сборки, типа или члена без выполнения статической проверки.
  /// </summary>
  [Conditional("CONTRACTS_FULL")]
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
  [__DynamicallyInvokable]
  public sealed class ContractVerificationAttribute : Attribute
  {
    private bool _value;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Contracts.ContractVerificationAttribute" />.
    /// </summary>
    /// <param name="value">
    ///   <see langword="true" /> Чтобы потребовать проверки; в противном случае — <see langword="false" />.
    /// </param>
    [__DynamicallyInvokable]
    public ContractVerificationAttribute(bool value)
    {
      this._value = value;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, следует ли проверять контракт целевого объекта.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если проверка необходима; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Value
    {
      [__DynamicallyInvokable] get
      {
        return this._value;
      }
    }
  }
}
