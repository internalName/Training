// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ContractHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Предоставляет методы, которые используются двоичным модулем перезаписи для обработки сбоев контракта.
  /// </summary>
  [__DynamicallyInvokable]
  public static class ContractHelper
  {
    private static readonly object lockObject = new object();
    private static volatile EventHandler<ContractFailedEventArgs> contractFailedEvent;
    internal const int COR_E_CODECONTRACTFAILED = -2146233022;

    /// <summary>
    ///   Используется двоичный метод перезаписи для активации сбоя поведение по умолчанию.
    /// </summary>
    /// <param name="failureKind">
    ///   Одно из значений перечисления, определяющее тип ошибки.
    /// </param>
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
      string resultFailureMessage = "Contract failed";
      ContractHelper.RaiseContractFailedEventImplementation(failureKind, userMessage, conditionText, innerException, ref resultFailureMessage);
      return resultFailureMessage;
    }

    /// <summary>Вызывает сбой поведение по умолчанию.</summary>
    /// <param name="kind">
    ///   Одно из значений перечисления, определяющее тип ошибки.
    /// </param>
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
      ContractHelper.TriggerFailureImplementation(kind, displayMessage, userMessage, conditionText, innerException);
    }

    [DebuggerNonUserCode]
    [SecuritySafeCritical]
    private static void RaiseContractFailedEventImplementation(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException, ref string resultFailureMessage)
    {
      switch (failureKind)
      {
        case ContractFailureKind.Precondition:
        case ContractFailureKind.Postcondition:
        case ContractFailureKind.PostconditionOnException:
        case ContractFailureKind.Invariant:
        case ContractFailureKind.Assert:
        case ContractFailureKind.Assume:
          string str1 = "contract failed.";
          ContractFailedEventArgs e = (ContractFailedEventArgs) null;
          RuntimeHelpers.PrepareConstrainedRegions();
          string str2;
          try
          {
            str1 = ContractHelper.GetDisplayMessage(failureKind, userMessage, conditionText);
            EventHandler<ContractFailedEventArgs> contractFailedEvent = ContractHelper.contractFailedEvent;
            if (contractFailedEvent != null)
            {
              e = new ContractFailedEventArgs(failureKind, str1, conditionText, innerException);
              foreach (EventHandler<ContractFailedEventArgs> invocation in contractFailedEvent.GetInvocationList())
              {
                try
                {
                  invocation((object) null, e);
                }
                catch (Exception ex)
                {
                  e.thrownDuringHandler = ex;
                  e.SetUnwind();
                }
              }
              if (e.Unwind)
              {
                if (Environment.IsCLRHosted)
                  ContractHelper.TriggerCodeContractEscalationPolicy(failureKind, str1, conditionText, innerException);
                if (innerException == null)
                  innerException = e.thrownDuringHandler;
                throw new ContractException(failureKind, str1, userMessage, conditionText, innerException);
              }
            }
          }
          finally
          {
            str2 = e == null || !e.Handled ? str1 : (string) null;
          }
          resultFailureMessage = str2;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) failureKind), nameof (failureKind));
      }
    }

    [DebuggerNonUserCode]
    [SecuritySafeCritical]
    private static void TriggerFailureImplementation(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
    {
      if (Environment.IsCLRHosted)
      {
        ContractHelper.TriggerCodeContractEscalationPolicy(kind, displayMessage, conditionText, innerException);
        throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
      }
      if (!Environment.UserInteractive)
        throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
      string resourceString = Environment.GetResourceString(ContractHelper.GetResourceNameForFailure(kind));
      Assert.Fail(conditionText, displayMessage, resourceString, -2146233022, StackTrace.TraceFormat.Normal, 2);
    }

    internal static event EventHandler<ContractFailedEventArgs> InternalContractFailed
    {
      [SecurityCritical] add
      {
        RuntimeHelpers.PrepareContractedDelegate((Delegate) value);
        lock (ContractHelper.lockObject)
          ContractHelper.contractFailedEvent += value;
      }
      [SecurityCritical] remove
      {
        lock (ContractHelper.lockObject)
          ContractHelper.contractFailedEvent -= value;
      }
    }

    private static string GetResourceNameForFailure(ContractFailureKind failureKind)
    {
      string str;
      switch (failureKind)
      {
        case ContractFailureKind.Precondition:
          str = "PreconditionFailed";
          break;
        case ContractFailureKind.Postcondition:
          str = "PostconditionFailed";
          break;
        case ContractFailureKind.PostconditionOnException:
          str = "PostconditionOnExceptionFailed";
          break;
        case ContractFailureKind.Invariant:
          str = "InvariantFailed";
          break;
        case ContractFailureKind.Assert:
          str = "AssertionFailed";
          break;
        case ContractFailureKind.Assume:
          str = "AssumptionFailed";
          break;
        default:
          Contract.Assume(false, "Unreachable code");
          str = "AssumptionFailed";
          break;
      }
      return str;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private static string GetDisplayMessage(ContractFailureKind failureKind, string userMessage, string conditionText)
    {
      string resourceNameForFailure = ContractHelper.GetResourceNameForFailure(failureKind);
      string resourceString;
      if (!string.IsNullOrEmpty(conditionText))
        resourceString = Environment.GetResourceString(resourceNameForFailure + "_Cnd", (object) conditionText);
      else
        resourceString = Environment.GetResourceString(resourceNameForFailure);
      if (!string.IsNullOrEmpty(userMessage))
        return resourceString + "  " + userMessage;
      return resourceString;
    }

    [SecuritySafeCritical]
    [DebuggerNonUserCode]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private static void TriggerCodeContractEscalationPolicy(ContractFailureKind failureKind, string message, string conditionText, Exception innerException)
    {
      string exceptionAsString = (string) null;
      if (innerException != null)
        exceptionAsString = innerException.ToString();
      Environment.TriggerCodeContractFailure(failureKind, message, conditionText, exceptionAsString);
    }
  }
}
