// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.Internal.ContractHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;

namespace System.Diagnostics.Contracts.Internal
{
  /// <summary>
  ///   Предоставляет методы, которые используются двоичным модулем перезаписи для обработки сбоев контракта.
  /// </summary>
  [Obsolete("Use the ContractHelper class in the System.Runtime.CompilerServices namespace instead.")]
  [__DynamicallyInvokable]
  public static class ContractHelper
  {
    /// <summary>
    ///   Используется двоичный метод перезаписи для активации сбоя поведение по умолчанию.
    /// </summary>
    /// <param name="failureKind">Тип сбоя.</param>
    /// <param name="userMessage">
    ///   Дополнительные сведения о пользователях.
    /// </param>
    /// <param name="conditionText">
    ///   Описание условия, вызвавшего сбой.
    /// </param>
    /// <param name="innerException">
    ///   Внутреннее исключение, вызвавшее текущее исключение.
    /// </param>
    /// <returns>
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic), если событие было обработано и не должно спровоцировать сбой; в противном случае — возвращает локализованное сообщение об ошибке.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="failureKind" /> не является допустимым значением <see cref="T:System.Diagnostics.Contracts.ContractFailureKind" />.
    /// </exception>
    [DebuggerNonUserCode]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static string RaiseContractFailedEvent(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
    {
      return System.Runtime.CompilerServices.ContractHelper.RaiseContractFailedEvent(failureKind, userMessage, conditionText, innerException);
    }

    /// <summary>Вызывает сбой поведение по умолчанию.</summary>
    /// <param name="kind">Тип сбоя.</param>
    /// <param name="displayMessage">Отображаемое сообщение.</param>
    /// <param name="userMessage">
    ///   Дополнительные сведения о пользователях.
    /// </param>
    /// <param name="conditionText">
    ///   Описание условия, вызвавшего сбой.
    /// </param>
    /// <param name="innerException">
    ///   Внутреннее исключение, вызвавшее текущее исключение.
    /// </param>
    [DebuggerNonUserCode]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void TriggerFailure(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
    {
      System.Runtime.CompilerServices.ContractHelper.TriggerFailure(kind, displayMessage, userMessage, conditionText, innerException);
    }
  }
}
