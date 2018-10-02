// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelLoopState
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Позволяет итерациям параллельных циклов взаимодействовать с другими итерациями.
  ///    Экземпляр этого класса предоставляется каждому циклу классом <see cref="T:System.Threading.Tasks.Parallel" />; невозможно создавать экземпляры в коде.
  /// </summary>
  [DebuggerDisplay("ShouldExitCurrentIteration = {ShouldExitCurrentIteration}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class ParallelLoopState
  {
    private ParallelLoopStateFlags m_flagsBase;

    internal ParallelLoopState(ParallelLoopStateFlags fbase)
    {
      this.m_flagsBase = fbase;
    }

    internal virtual bool InternalShouldExitCurrentIteration
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("ParallelState_NotSupportedException_UnsupportedMethod"));
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, следует ли текущей итерации цикла завершить работу на основе запросов от этой или других итераций.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущая итерация должна завершать работу; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool ShouldExitCurrentIteration
    {
      [__DynamicallyInvokable] get
      {
        return this.InternalShouldExitCurrentIteration;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, вызывала ли какая-либо итерация цикла метод <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если какая-либо итерация остановила цикл, вызвав метод <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsStopped
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.m_flagsBase.LoopStateFlags & ParallelLoopStateFlags.PLS_STOPPED) > 0U;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, возникло ли в какой-либо итерации цикла исключение, не обработанное данной итерацией.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если было вызвано необработанное исключение; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsExceptional
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.m_flagsBase.LoopStateFlags & ParallelLoopStateFlags.PLS_EXCEPTIONAL) > 0U;
      }
    }

    internal virtual long? InternalLowestBreakIteration
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("ParallelState_NotSupportedException_UnsupportedMethod"));
      }
    }

    /// <summary>
    ///   Получает первую итерацию цикла, из которой был вызван метод <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" />.
    /// </summary>
    /// <returns>
    ///   Получает первую итерацию, из которой был вызван метод <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" />.
    ///    В случае цикла <see cref="M:System.Threading.Tasks.Parallel.ForEach``1(System.Collections.Concurrent.Partitioner{``0},System.Action{``0})" /> значение основано на внутренне создаваемом индексе.
    /// </returns>
    [__DynamicallyInvokable]
    public long? LowestBreakIteration
    {
      [__DynamicallyInvokable] get
      {
        return this.InternalLowestBreakIteration;
      }
    }

    /// <summary>
    ///   Сообщает, что цикл <see cref="T:System.Threading.Tasks.Parallel" /> должен прекратить выполнение в первый удобный для системы момент.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> Метод был вызван ранее.
    ///   <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> и <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> не могут использоваться в сочетании с итерациями же цикла.
    /// </exception>
    [__DynamicallyInvokable]
    public void Stop()
    {
      this.m_flagsBase.Stop();
    }

    internal virtual void InternalBreak()
    {
      throw new NotSupportedException(Environment.GetResourceString("ParallelState_NotSupportedException_UnsupportedMethod"));
    }

    /// <summary>
    ///   Сообщает, что цикл <see cref="T:System.Threading.Tasks.Parallel" /> должен прекратить выполнение итераций после текущей в первый удобный для системы момент.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> Метод был вызван ранее.
    ///   <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> и <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> не могут использоваться в сочетании с итерациями же цикла.
    /// </exception>
    [__DynamicallyInvokable]
    public void Break()
    {
      this.InternalBreak();
    }

    internal static void Break(int iteration, ParallelLoopStateFlags32 pflags)
    {
      int plsNone = ParallelLoopStateFlags.PLS_NONE;
      if (!pflags.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_BROKEN, ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED, ref plsNone))
      {
        if ((plsNone & ParallelLoopStateFlags.PLS_STOPPED) != 0)
          throw new InvalidOperationException(Environment.GetResourceString("ParallelState_Break_InvalidOperationException_BreakAfterStop"));
      }
      else
      {
        int lowestBreakIteration = pflags.m_lowestBreakIteration;
        if (iteration >= lowestBreakIteration)
          return;
        SpinWait spinWait = new SpinWait();
        while (Interlocked.CompareExchange(ref pflags.m_lowestBreakIteration, iteration, lowestBreakIteration) != lowestBreakIteration)
        {
          spinWait.SpinOnce();
          lowestBreakIteration = pflags.m_lowestBreakIteration;
          if (iteration > lowestBreakIteration)
            break;
        }
      }
    }

    internal static void Break(long iteration, ParallelLoopStateFlags64 pflags)
    {
      int plsNone = ParallelLoopStateFlags.PLS_NONE;
      if (!pflags.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_BROKEN, ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED, ref plsNone))
      {
        if ((plsNone & ParallelLoopStateFlags.PLS_STOPPED) != 0)
          throw new InvalidOperationException(Environment.GetResourceString("ParallelState_Break_InvalidOperationException_BreakAfterStop"));
      }
      else
      {
        long lowestBreakIteration = pflags.LowestBreakIteration;
        if (iteration >= lowestBreakIteration)
          return;
        SpinWait spinWait = new SpinWait();
        while (Interlocked.CompareExchange(ref pflags.m_lowestBreakIteration, iteration, lowestBreakIteration) != lowestBreakIteration)
        {
          spinWait.SpinOnce();
          lowestBreakIteration = pflags.LowestBreakIteration;
          if (iteration > lowestBreakIteration)
            break;
        }
      }
    }
  }
}
