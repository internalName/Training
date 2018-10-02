// Decompiled with JetBrains decompiler
// Type: System.Runtime.ConstrainedExecution.ReliabilityContractAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.ConstrainedExecution
{
  /// <summary>
  ///   Определяет контракт о надежности между автором кода и разработчиками, зависящими от этого кода.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false)]
  public sealed class ReliabilityContractAttribute : Attribute
  {
    private Consistency _consistency;
    private Cer _cer;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.ConstrainedExecution.ReliabilityContractAttribute" /> с указанной гарантией <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> и значением <see cref="T:System.Runtime.ConstrainedExecution.Cer" />.
    /// </summary>
    /// <param name="consistencyGuarantee">
    ///   Одно из значений <see cref="T:System.Runtime.ConstrainedExecution.Consistency" />.
    /// </param>
    /// <param name="cer">
    ///   Одно из значений <see cref="T:System.Runtime.ConstrainedExecution.Cer" />.
    /// </param>
    public ReliabilityContractAttribute(Consistency consistencyGuarantee, Cer cer)
    {
      this._consistency = consistencyGuarantee;
      this._cer = cer;
    }

    /// <summary>
    ///   Возвращает значение <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> контрактом надежности.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Runtime.ConstrainedExecution.Consistency" />.
    /// </returns>
    public Consistency ConsistencyGuarantee
    {
      get
      {
        return this._consistency;
      }
    }

    /// <summary>
    ///   Получает значение, определяющее поведение метода, типа или сборки при вызове в области ограниченного исполнения (CER).
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Runtime.ConstrainedExecution.Cer" />.
    /// </returns>
    public Cer Cer
    {
      get
      {
        return this._cer;
      }
    }
  }
}
