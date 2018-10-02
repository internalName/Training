// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.ContractFailedEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Diagnostics.Contracts
{
  /// <summary>
  ///   Предоставляет методы и данные для <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> события.
  /// </summary>
  [__DynamicallyInvokable]
  public sealed class ContractFailedEventArgs : EventArgs
  {
    private ContractFailureKind _failureKind;
    private string _message;
    private string _condition;
    private Exception _originalException;
    private bool _handled;
    private bool _unwind;
    internal Exception thrownDuringHandler;

    /// <summary>
    ///   Предоставляет данные для события <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" />.
    /// </summary>
    /// <param name="failureKind">
    ///   Одно из значений перечисления, указывающее контракт, который не удалось.
    /// </param>
    /// <param name="message">Сообщение для события.</param>
    /// <param name="condition">Условие для события.</param>
    /// <param name="originalException">
    ///   Исключение, вызвавшее событие.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public ContractFailedEventArgs(ContractFailureKind failureKind, string message, string condition, Exception originalException)
    {
      this._failureKind = failureKind;
      this._message = message;
      this._condition = condition;
      this._originalException = originalException;
    }

    /// <summary>
    ///   Возвращает сообщение, описывающее <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> события.
    /// </summary>
    /// <returns>Сообщение, описывающее событие.</returns>
    [__DynamicallyInvokable]
    public string Message
    {
      [__DynamicallyInvokable] get
      {
        return this._message;
      }
    }

    /// <summary>Получает условие сбоя контракта.</summary>
    /// <returns>Условие сбоя.</returns>
    [__DynamicallyInvokable]
    public string Condition
    {
      [__DynamicallyInvokable] get
      {
        return this._condition;
      }
    }

    /// <summary>Получает тип контракта, вызвавшего сбой.</summary>
    /// <returns>
    ///   Одно из значений перечисления, указывающее тип контракта, вызвавшего сбой.
    /// </returns>
    [__DynamicallyInvokable]
    public ContractFailureKind FailureKind
    {
      [__DynamicallyInvokable] get
      {
        return this._failureKind;
      }
    }

    /// <summary>
    ///   Возвращает исходное исключение, вызвавшее <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> события.
    /// </summary>
    /// <returns>Исключение, вызвавшее событие.</returns>
    [__DynamicallyInvokable]
    public Exception OriginalException
    {
      [__DynamicallyInvokable] get
      {
        return this._originalException;
      }
    }

    /// <summary>
    ///   Указывает, является ли <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> обработано событие.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если событие было обработано; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Handled
    {
      [__DynamicallyInvokable] get
      {
        return this._handled;
      }
    }

    /// <summary>
    ///   Задает для свойства <see cref="P:System.Diagnostics.Contracts.ContractFailedEventArgs.Handled" /> значение <see langword="true" />.
    /// </summary>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public void SetHandled()
    {
      this._handled = true;
    }

    /// <summary>
    ///   Указывает, должна ли применяться политика эскалации контракта кода.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Чтобы применить политику укрупнения. в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Unwind
    {
      [__DynamicallyInvokable] get
      {
        return this._unwind;
      }
    }

    /// <summary>
    ///   Задает для свойства <see cref="P:System.Diagnostics.Contracts.ContractFailedEventArgs.Unwind" /> значение <see langword="true" />.
    /// </summary>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public void SetUnwind()
    {
      this._unwind = true;
    }
  }
}
