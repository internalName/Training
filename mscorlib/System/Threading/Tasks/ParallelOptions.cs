// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  /// <summary>
  ///   Хранит параметры, настраивающие работу методов на <see cref="T:System.Threading.Tasks.Parallel" /> класса.
  /// </summary>
  [__DynamicallyInvokable]
  public class ParallelOptions
  {
    private TaskScheduler m_scheduler;
    private int m_maxDegreeOfParallelism;
    private CancellationToken m_cancellationToken;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Tasks.ParallelOptions" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ParallelOptions()
    {
      this.m_scheduler = TaskScheduler.Default;
      this.m_maxDegreeOfParallelism = -1;
      this.m_cancellationToken = CancellationToken.None;
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Threading.Tasks.TaskScheduler" /> связанный с этим <see cref="T:System.Threading.Tasks.ParallelOptions" /> экземпляра.
    ///    Этому свойству присвоить значение null указывает, что следует использовать текущий планировщик.
    /// </summary>
    /// <returns>
    ///   Планировщик заданий, связанный с данным экземпляром.
    /// </returns>
    [__DynamicallyInvokable]
    public TaskScheduler TaskScheduler
    {
      [__DynamicallyInvokable] get
      {
        return this.m_scheduler;
      }
      [__DynamicallyInvokable] set
      {
        this.m_scheduler = value;
      }
    }

    internal TaskScheduler EffectiveTaskScheduler
    {
      get
      {
        if (this.m_scheduler == null)
          return TaskScheduler.Current;
        return this.m_scheduler;
      }
    }

    /// <summary>
    ///   Возвращает или задает максимальное число параллельных задач, включаемые это <see cref="T:System.Threading.Tasks.ParallelOptions" /> экземпляра.
    /// </summary>
    /// <returns>
    ///   Целое число, представляющее максимальную степень параллелизма.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство задается равным нулю или значению меньше-1.
    /// </exception>
    [__DynamicallyInvokable]
    public int MaxDegreeOfParallelism
    {
      [__DynamicallyInvokable] get
      {
        return this.m_maxDegreeOfParallelism;
      }
      [__DynamicallyInvokable] set
      {
        if (value == 0 || value < -1)
          throw new ArgumentOutOfRangeException(nameof (MaxDegreeOfParallelism));
        this.m_maxDegreeOfParallelism = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Threading.CancellationToken" /> связанный с этим <see cref="T:System.Threading.Tasks.ParallelOptions" /> экземпляра.
    /// </summary>
    /// <returns>Токен, связанный с данным экземпляром.</returns>
    [__DynamicallyInvokable]
    public CancellationToken CancellationToken
    {
      [__DynamicallyInvokable] get
      {
        return this.m_cancellationToken;
      }
      [__DynamicallyInvokable] set
      {
        this.m_cancellationToken = value;
      }
    }

    internal int EffectiveMaxConcurrencyLevel
    {
      get
      {
        int val2 = this.MaxDegreeOfParallelism;
        int concurrencyLevel = this.EffectiveTaskScheduler.MaximumConcurrencyLevel;
        if (concurrencyLevel > 0 && concurrencyLevel != int.MaxValue)
          val2 = val2 == -1 ? concurrencyLevel : Math.Min(concurrencyLevel, val2);
        return val2;
      }
    }
  }
}
