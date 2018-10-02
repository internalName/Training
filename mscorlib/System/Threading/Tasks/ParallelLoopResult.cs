// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelLoopResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Предоставляет состояние выполнения <see cref="T:System.Threading.Tasks.Parallel" /> цикла.
  /// </summary>
  [__DynamicallyInvokable]
  public struct ParallelLoopResult
  {
    internal bool m_completed;
    internal long? m_lowestBreakIteration;

    /// <summary>
    ///   Получает значение, указывающее цикл был завершен, таким образом, что выполнены все итерации цикла и он не получил запрос преждевременно.
    /// </summary>
    /// <returns>
    ///   значение true, если цикл был завершен; в противном случае — false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsCompleted
    {
      [__DynamicallyInvokable] get
      {
        return this.m_completed;
      }
    }

    /// <summary>
    ///   Получает индекс первую итерацию, из которого <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> был вызван.
    /// </summary>
    /// <returns>
    ///   Возвращает целое число, представляющее низшую итерацию, из которой был вызван оператор Break.
    /// </returns>
    [__DynamicallyInvokable]
    public long? LowestBreakIteration
    {
      [__DynamicallyInvokable] get
      {
        return this.m_lowestBreakIteration;
      }
    }
  }
}
