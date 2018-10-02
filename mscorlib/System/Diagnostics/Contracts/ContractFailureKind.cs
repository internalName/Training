// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractFailureKind
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.Contracts
{
  /// <summary>Указывает тип контракта, завершившегося ошибкой.</summary>
  [__DynamicallyInvokable]
  public enum ContractFailureKind
  {
    [__DynamicallyInvokable] Precondition,
    [__DynamicallyInvokable] Postcondition,
    [__DynamicallyInvokable] PostconditionOnException,
    [__DynamicallyInvokable] Invariant,
    [__DynamicallyInvokable] Assert,
    [__DynamicallyInvokable] Assume,
  }
}
