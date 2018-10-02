// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.IAsyncStateMachine
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Представляет конечные автоматы, созданные для асинхронных методов.
  ///    Этот тип предназначен только для внутреннего использования компиляторами.
  /// </summary>
  [__DynamicallyInvokable]
  public interface IAsyncStateMachine
  {
    /// <summary>Конечный автомат переходит в следующее состояние.</summary>
    [__DynamicallyInvokable]
    void MoveNext();

    /// <summary>
    ///   Настраивает конечный автомат с репликой, выделенными в куче.
    /// </summary>
    /// <param name="stateMachine">Реплика куче.</param>
    [__DynamicallyInvokable]
    void SetStateMachine(IAsyncStateMachine stateMachine);
  }
}
