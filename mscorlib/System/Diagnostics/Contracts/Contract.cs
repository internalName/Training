// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Contracts.Contract
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Diagnostics.Contracts
{
  /// <summary>
  ///   Содержит статические методы для представления контрактов программы, таких как предусловие, постусловие и инвариантность объектов.
  /// </summary>
  [__DynamicallyInvokable]
  public static class Contract
  {
    [ThreadStatic]
    private static bool _assertingMustUseRewriter;

    /// <summary>
    ///   Инструктирует инструменты анализа кода полагать, что указанным условием является <see langword="true" />, даже если статически невозможно подтвердить постоянное <see langword="true" />.
    /// </summary>
    /// <param name="condition">
    ///   Предполагаемое условное выражение <see langword="true" />.
    /// </param>
    [Conditional("DEBUG")]
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Assume(bool condition)
    {
      if (condition)
        return;
      Contract.ReportFailure(ContractFailureKind.Assume, (string) null, (string) null, (Exception) null);
    }

    /// <summary>
    ///   Инструктирует инструменты анализа кода полагать, что условием является <see langword="true" />, даже если статически невозможно подтвердить постоянное <see langword="true" />, и отображает сообщение, если предположение было ошибочным.
    /// </summary>
    /// <param name="condition">
    ///   Предполагаемое условное выражение <see langword="true" />.
    /// </param>
    /// <param name="userMessage">
    ///   Сообщение, создаваемое при ошибочном предположении.
    /// </param>
    [Conditional("DEBUG")]
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Assume(bool condition, string userMessage)
    {
      if (condition)
        return;
      Contract.ReportFailure(ContractFailureKind.Assume, userMessage, (string) null, (Exception) null);
    }

    /// <summary>
    ///   Проверяет условие; Если условие равно <see langword="false" />, следует политике эскалации, установленной для анализатора.
    /// </summary>
    /// <param name="condition">
    ///   Условное выражение, которое требуется подвергнуть проверке.
    /// </param>
    [Conditional("DEBUG")]
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Assert(bool condition)
    {
      if (condition)
        return;
      Contract.ReportFailure(ContractFailureKind.Assert, (string) null, (string) null, (Exception) null);
    }

    /// <summary>
    ///   Проверяет условие; Если условие равно <see langword="false" />, следует политике эскалации, установленной анализатором и отображает указанное сообщение.
    /// </summary>
    /// <param name="condition">
    ///   Условное выражение, которое требуется подвергнуть проверке.
    /// </param>
    /// <param name="userMessage">
    ///   Сообщение, отображаемое при несоответствии условия.
    /// </param>
    [Conditional("DEBUG")]
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Assert(bool condition, string userMessage)
    {
      if (condition)
        return;
      Contract.ReportFailure(ContractFailureKind.Assert, userMessage, (string) null, (Exception) null);
    }

    /// <summary>
    ///   Указывает контракт предусловия для включающего метода или свойства.
    /// </summary>
    /// <param name="condition">
    ///   Условное выражение, которое требуется подвергнуть проверке.
    /// </param>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Requires(bool condition)
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, nameof (Requires));
    }

    /// <summary>
    ///   Указывает контракт предусловия для включающего метода или свойства и отображает сообщение, если условие для контракта не выполняется.
    /// </summary>
    /// <param name="condition">
    ///   Условное выражение, которое требуется подвергнуть проверке.
    /// </param>
    /// <param name="userMessage">
    ///   Сообщение, отображаемое, если условие равно <see langword="false" />.
    /// </param>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Requires(bool condition, string userMessage)
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, nameof (Requires));
    }

    /// <summary>
    ///   Указывает контракт предусловия для включающего метода или свойства и выдает исключение, если условие для контракта не выполняется.
    /// </summary>
    /// <param name="condition">
    ///   Условное выражение, которое требуется подвергнуть проверке.
    /// </param>
    /// <typeparam name="TException">
    ///   Исключение, если условие равно <see langword="false" />.
    /// </typeparam>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Requires<TException>(bool condition) where TException : Exception
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires<TException>");
    }

    /// <summary>
    ///   Задает контракт предусловия для включающего метода или свойства и создает исключение с предоставленным сообщением, если условие для контракта не выполняется.
    /// </summary>
    /// <param name="condition">
    ///   Условное выражение, которое требуется подвергнуть проверке.
    /// </param>
    /// <param name="userMessage">
    ///   Сообщение, отображаемое, если условие равно <see langword="false" />.
    /// </param>
    /// <typeparam name="TException">
    ///   Исключение, если условие равно <see langword="false" />.
    /// </typeparam>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Requires<TException>(bool condition, string userMessage) where TException : Exception
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires<TException>");
    }

    /// <summary>
    ///   Указывает контракт постусловия для включающего метода или свойства.
    /// </summary>
    /// <param name="condition">
    ///   Условное выражение, которое требуется подвергнуть проверке.
    ///    Выражение может включать <see cref="M:System.Diagnostics.Contracts.Contract.OldValue``1(``0)" />, <see cref="M:System.Diagnostics.Contracts.Contract.ValueAtReturn``1(``0@)" />, и <see cref="M:System.Diagnostics.Contracts.Contract.Result``1" /> значения.
    /// </param>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Ensures(bool condition)
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Postcondition, nameof (Ensures));
    }

    /// <summary>
    ///   Задает контракт постусловия для предоставленного выходного условия и сообщение, отображаемое, если условие равно <see langword="false" />.
    /// </summary>
    /// <param name="condition">
    ///   Условное выражение, которое требуется подвергнуть проверке.
    ///    Выражение может включать <see cref="M:System.Diagnostics.Contracts.Contract.OldValue``1(``0)" /> и <see cref="M:System.Diagnostics.Contracts.Contract.Result``1" /> значения.
    /// </param>
    /// <param name="userMessage">
    ///   Сообщение, отображаемое, если выражение не является <see langword="true" />.
    /// </param>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Ensures(bool condition, string userMessage)
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Postcondition, nameof (Ensures));
    }

    /// <summary>
    ///   Задает контракт постусловия для включающего метода или свойства на основе предоставленных исключения и состояния.
    /// </summary>
    /// <param name="condition">
    ///   Условное выражение, которое требуется подвергнуть проверке.
    /// </param>
    /// <typeparam name="TException">
    ///   Тип исключения, вызвавшего проверку постусловия.
    /// </typeparam>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void EnsuresOnThrow<TException>(bool condition) where TException : Exception
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.PostconditionOnException, nameof (EnsuresOnThrow));
    }

    /// <summary>
    ///   Задает контракт постусловия и сообщение, отображаемое, если условие равно <see langword="false" /> для включающего метода или свойства на основе предоставленных исключения и состояния.
    /// </summary>
    /// <param name="condition">
    ///   Условное выражение, которое требуется подвергнуть проверке.
    /// </param>
    /// <param name="userMessage">
    ///   Сообщение, отображаемое, если выражение является <see langword="false" />.
    /// </param>
    /// <typeparam name="TException">
    ///   Тип исключения, вызвавшего проверку постусловия.
    /// </typeparam>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void EnsuresOnThrow<TException>(bool condition, string userMessage) where TException : Exception
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.PostconditionOnException, nameof (EnsuresOnThrow));
    }

    /// <summary>
    ///   Представляет возвращаемое значение метода или свойства.
    /// </summary>
    /// <typeparam name="T">
    ///   Тип возвращаемого значения включающего метода или свойства.
    /// </typeparam>
    /// <returns>
    ///   Возвращаемое значение включающего метода или свойства.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static T Result<T>()
    {
      return default (T);
    }

    /// <summary>
    ///   Представляет итоговое (выходное) значение <see langword="out" /> параметра при возврате из метода.
    /// </summary>
    /// <param name="value">
    ///   <see langword="out" /> Параметр.
    /// </param>
    /// <typeparam name="T">
    ///   Тип параметра <see langword="out" />.
    /// </typeparam>
    /// <returns>
    ///   Выходное значение <see langword="out" /> параметр.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static T ValueAtReturn<T>(out T value)
    {
      value = default (T);
      return value;
    }

    /// <summary>
    ///   Представляет значения, какими они были в начале метода или свойства.
    /// </summary>
    /// <param name="value">
    ///   Представляемое значение (поле или параметр).
    /// </param>
    /// <typeparam name="T">Тип значения.</typeparam>
    /// <returns>
    ///   Значение параметра или поля при запуске метода или свойства.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static T OldValue<T>(T value)
    {
      return default (T);
    }

    /// <summary>
    ///   Указывает инвариантный контракт для включающего метода или свойства.
    /// </summary>
    /// <param name="condition">
    ///   Условное выражение, которое требуется подвергнуть проверке.
    /// </param>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Invariant(bool condition)
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Invariant, nameof (Invariant));
    }

    /// <summary>
    ///   Указывает инвариантный контракт для включающего метода или свойства и отображает сообщение, если условие для контракта не выполняется.
    /// </summary>
    /// <param name="condition">
    ///   Условное выражение, которое требуется подвергнуть проверке.
    /// </param>
    /// <param name="userMessage">
    ///   Сообщение, отображаемое, если условие равно <see langword="false" />.
    /// </param>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static void Invariant(bool condition, string userMessage)
    {
      Contract.AssertMustUseRewriter(ContractFailureKind.Invariant, nameof (Invariant));
    }

    /// <summary>
    ///   Определяет, выполняется ли определенное условие для всех целых чисел в указанном диапазоне.
    /// </summary>
    /// <param name="fromInclusive">
    ///   Первое число для передачи <paramref name="predicate" />.
    /// </param>
    /// <param name="toExclusive">
    ///   Более чем на последнее целое число для передачи <paramref name="predicate" />.
    /// </param>
    /// <param name="predicate">
    ///   Функция, оцениваемая, чтобы установить существование целых чисел в указанном диапазоне.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="predicate" /> возвращает <see langword="true" /> для всех целых чисел, начиная с <paramref name="fromInclusive" /> для <paramref name="toExclusive" /> - 1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="predicate" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="toExclusive " />— меньше, чем <paramref name="fromInclusive" />.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static bool ForAll(int fromInclusive, int toExclusive, Predicate<int> predicate)
    {
      if (fromInclusive > toExclusive)
        throw new ArgumentException(Environment.GetResourceString("Argument_ToExclusiveLessThanFromExclusive"));
      if (predicate == null)
        throw new ArgumentNullException(nameof (predicate));
      for (int index = fromInclusive; index < toExclusive; ++index)
      {
        if (!predicate(index))
          return false;
      }
      return true;
    }

    /// <summary>
    ///   Определяет, существуют ли в рамках функции все элементы в коллекции.
    /// </summary>
    /// <param name="collection">
    ///   Коллекция, из которой элементы типа <paramref name="T" /> будет отображаться для передачи в <paramref name="predicate" />.
    /// </param>
    /// <param name="predicate">
    ///   Функция, оцениваемая на предмет наличия всех элементов в <paramref name="collection" />.
    /// </param>
    /// <typeparam name="T">
    ///   Тип, содержащийся в <paramref name="collection" />.
    /// </typeparam>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="predicate" /> возвращает <see langword="true" /> для всех элементов типа <paramref name="T" /> в <paramref name="collection" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="collection" /> или <paramref name="predicate" /> имеет значение <see langword="null" />.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static bool ForAll<T>(IEnumerable<T> collection, Predicate<T> predicate)
    {
      if (collection == null)
        throw new ArgumentNullException(nameof (collection));
      if (predicate == null)
        throw new ArgumentNullException(nameof (predicate));
      foreach (T obj in collection)
      {
        if (!predicate(obj))
          return false;
      }
      return true;
    }

    /// <summary>
    ///   Определяет, удовлетворяет ли проверке какое-либо целое число в диапазоне целых чисел.
    /// </summary>
    /// <param name="fromInclusive">
    ///   Первое число для передачи <paramref name="predicate" />.
    /// </param>
    /// <param name="toExclusive">
    ///   Более чем на последнее целое число для передачи <paramref name="predicate" />.
    /// </param>
    /// <param name="predicate">
    ///   Функция, оцениваемая на предмет любого значения целого числа в указанном диапазоне.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="predicate" /> возвращает <see langword="true" /> для любого целого числа, начиная с <paramref name="fromInclusive" /> для <paramref name="toExclusive" /> - 1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="predicate" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="toExclusive " />— меньше, чем <paramref name="fromInclusive" />.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static bool Exists(int fromInclusive, int toExclusive, Predicate<int> predicate)
    {
      if (fromInclusive > toExclusive)
        throw new ArgumentException(Environment.GetResourceString("Argument_ToExclusiveLessThanFromExclusive"));
      if (predicate == null)
        throw new ArgumentNullException(nameof (predicate));
      for (int index = fromInclusive; index < toExclusive; ++index)
      {
        if (predicate(index))
          return true;
      }
      return false;
    }

    /// <summary>
    ///   Определяет, существует ли в рамках функции элемент в указанной коллекции элементов.
    /// </summary>
    /// <param name="collection">
    ///   Коллекция, из которой элементы типа <paramref name="T" /> будет отображаться для передачи в <paramref name="predicate" />.
    /// </param>
    /// <param name="predicate">
    ///   Функция, оцениваемая на предмет элемента в <paramref name="collection" />.
    /// </param>
    /// <typeparam name="T">
    ///   Тип, содержащийся в <paramref name="collection" />.
    /// </typeparam>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="predicate" /> возвращает <see langword="true" /> для любого элемента типа <paramref name="T" /> в <paramref name="collection" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="collection" /> или <paramref name="predicate" /> имеет значение <see langword="null" />.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public static bool Exists<T>(IEnumerable<T> collection, Predicate<T> predicate)
    {
      if (collection == null)
        throw new ArgumentNullException(nameof (collection));
      if (predicate == null)
        throw new ArgumentNullException(nameof (predicate));
      foreach (T obj in collection)
      {
        if (predicate(obj))
          return true;
      }
      return false;
    }

    /// <summary>
    ///   Помечает конец раздела контракта, когда контракты метода содержат только предусловия вида <see langword="if" />-<see langword="then" />-<see langword="throw" /> формы.
    /// </summary>
    [Conditional("CONTRACTS_FULL")]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void EndContractBlock()
    {
    }

    [DebuggerNonUserCode]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private static void ReportFailure(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
    {
      switch (failureKind)
      {
        case ContractFailureKind.Precondition:
        case ContractFailureKind.Postcondition:
        case ContractFailureKind.PostconditionOnException:
        case ContractFailureKind.Invariant:
        case ContractFailureKind.Assert:
        case ContractFailureKind.Assume:
          string displayMessage = ContractHelper.RaiseContractFailedEvent(failureKind, userMessage, conditionText, innerException);
          if (displayMessage == null)
            break;
          ContractHelper.TriggerFailure(failureKind, displayMessage, userMessage, conditionText, innerException);
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) failureKind), nameof (failureKind));
      }
    }

    [SecuritySafeCritical]
    private static void AssertMustUseRewriter(ContractFailureKind kind, string contractKind)
    {
      if (Contract._assertingMustUseRewriter)
        Assert.Fail("Asserting that we must use the rewriter went reentrant.", "Didn't rewrite this mscorlib?");
      Contract._assertingMustUseRewriter = true;
      Assembly assembly1 = typeof (Contract).Assembly;
      StackTrace stackTrace = new StackTrace();
      Assembly assembly2 = (Assembly) null;
      for (int index = 0; index < stackTrace.FrameCount; ++index)
      {
        Assembly assembly3 = stackTrace.GetFrame(index).GetMethod().DeclaringType.Assembly;
        if (assembly3 != assembly1)
        {
          assembly2 = assembly3;
          break;
        }
      }
      if (assembly2 == (Assembly) null)
        assembly2 = assembly1;
      string name = assembly2.GetName().Name;
      ContractHelper.TriggerFailure(kind, Environment.GetResourceString("MustUseCCRewrite", (object) contractKind, (object) name), (string) null, (string) null, (Exception) null);
      Contract._assertingMustUseRewriter = false;
    }

    /// <summary>Происходит, когда контракт не выполняется.</summary>
    [__DynamicallyInvokable]
    public static event EventHandler<ContractFailedEventArgs> ContractFailed
    {
      [SecurityCritical, __DynamicallyInvokable] add
      {
        ContractHelper.InternalContractFailed += value;
      }
      [SecurityCritical, __DynamicallyInvokable] remove
      {
        ContractHelper.InternalContractFailed -= value;
      }
    }
  }
}
