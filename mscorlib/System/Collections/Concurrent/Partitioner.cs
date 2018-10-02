// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.Partitioner
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
  /// <summary>
  ///   Предоставляет общие стратегии разделения для массивов, списков и перечислимых значений.
  /// </summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public static class Partitioner
  {
    private const int DEFAULT_BYTES_PER_CHUNK = 512;

    /// <summary>
    ///   Создает упорядочиваемый разделитель из <see cref="T:System.Collections.Generic.IList`1" /> экземпляра.
    /// </summary>
    /// <param name="list">Разделяемый список.</param>
    /// <param name="loadBalance">
    ///   Логическое значение, указывающее, ли созданный модуль разделения динамически распределять нагрузку между разделами или статически секции.
    /// </param>
    /// <typeparam name="TSource">
    ///   Тип элементов исходного списка.
    /// </typeparam>
    /// <returns>
    ///   Упорядочиваемый разделитель, на основе входного списка.
    /// </returns>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<TSource> Create<TSource>(IList<TSource> list, bool loadBalance)
    {
      if (list == null)
        throw new ArgumentNullException(nameof (list));
      if (loadBalance)
        return (OrderablePartitioner<TSource>) new Partitioner.DynamicPartitionerForIList<TSource>(list);
      return (OrderablePartitioner<TSource>) new Partitioner.StaticIndexRangePartitionerForIList<TSource>(list);
    }

    /// <summary>
    ///   Создает упорядочиваемый разделитель из <see cref="T:System.Array" /> экземпляра.
    /// </summary>
    /// <param name="array">Разделяемый массив.</param>
    /// <param name="loadBalance">
    ///   Логическое значение, указывающее, ли созданный модуль разделения динамически распределять нагрузку между разделами или статически секции.
    /// </param>
    /// <typeparam name="TSource">
    ///   Тип элементов исходного массива.
    /// </typeparam>
    /// <returns>
    ///   Упорядочиваемый разделитель, на основе входного массива.
    /// </returns>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<TSource> Create<TSource>(TSource[] array, bool loadBalance)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (loadBalance)
        return (OrderablePartitioner<TSource>) new Partitioner.DynamicPartitionerForArray<TSource>(array);
      return (OrderablePartitioner<TSource>) new Partitioner.StaticIndexRangePartitionerForArray<TSource>(array);
    }

    /// <summary>
    ///   Создает упорядочиваемый разделитель из <see cref="T:System.Collections.Generic.IEnumerable`1" /> экземпляра.
    /// </summary>
    /// <param name="source">Разделяемая перечисляемая коллекция.</param>
    /// <typeparam name="TSource">
    ///   Тип элементов исходной перечисляемой коллекции.
    /// </typeparam>
    /// <returns>
    ///   Упорядочиваемый разделитель, на основе входного массива.
    /// </returns>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source)
    {
      return Partitioner.Create<TSource>(source, EnumerablePartitionerOptions.None);
    }

    /// <summary>
    ///   Создает упорядочиваемый разделитель из <see cref="T:System.Collections.Generic.IEnumerable`1" /> экземпляра.
    /// </summary>
    /// <param name="source">Разделяемая перечисляемая коллекция.</param>
    /// <param name="partitionerOptions">
    ///   Параметры для управления поведением буферизации модуля разделения.
    /// </param>
    /// <typeparam name="TSource">
    ///   Тип элементов исходной перечисляемой коллекции.
    /// </typeparam>
    /// <returns>
    ///   Упорядочиваемый разделитель, на основе входного массива.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="partitionerOptions" /> Аргумент задает недопустимое значение для <see cref="T:System.Collections.Concurrent.EnumerablePartitionerOptions" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if ((partitionerOptions & ~EnumerablePartitionerOptions.NoBuffering) != EnumerablePartitionerOptions.None)
        throw new ArgumentOutOfRangeException(nameof (partitionerOptions));
      return (OrderablePartitioner<TSource>) new Partitioner.DynamicPartitionerForIEnumerable<TSource>(source, partitionerOptions);
    }

    /// <summary>
    ///   Создает модуль разделения, разделяет диапазон, определяемый пользователем.
    /// </summary>
    /// <param name="fromInclusive">
    ///   Ниже включительно границу диапазона.
    /// </param>
    /// <param name="toExclusive">
    ///   Верхний монопольного границу диапазона.
    /// </param>
    /// <returns>Разделитель.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="toExclusive" /> Аргумент меньше или равно <paramref name="fromInclusive" /> аргумент.
    /// </exception>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive)
    {
      int num = 3;
      if (toExclusive <= fromInclusive)
        throw new ArgumentOutOfRangeException(nameof (toExclusive));
      long rangeSize = (toExclusive - fromInclusive) / (long) (PlatformHelper.ProcessorCount * num);
      if (rangeSize == 0L)
        rangeSize = 1L;
      return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
    }

    /// <summary>
    ///   Создает модуль разделения, разделяет диапазон, определяемый пользователем.
    /// </summary>
    /// <param name="fromInclusive">
    ///   Ниже включительно границу диапазона.
    /// </param>
    /// <param name="toExclusive">
    ///   Верхний монопольного границу диапазона.
    /// </param>
    /// <param name="rangeSize">Размер каждого поддиапазона.</param>
    /// <returns>Разделитель.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="toExclusive" /> Аргумент меньше или равно <paramref name="fromInclusive" /> аргумент.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="rangeSize" /> Аргумент меньше или равно 0.
    /// </exception>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive, long rangeSize)
    {
      if (toExclusive <= fromInclusive)
        throw new ArgumentOutOfRangeException(nameof (toExclusive));
      if (rangeSize <= 0L)
        throw new ArgumentOutOfRangeException(nameof (rangeSize));
      return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
    }

    private static IEnumerable<Tuple<long, long>> CreateRanges(long fromInclusive, long toExclusive, long rangeSize)
    {
      bool shouldQuit = false;
      long i = fromInclusive;
      while (i < toExclusive && !shouldQuit)
      {
        long num1 = i;
        long num2;
        try
        {
          num2 = checked (i + rangeSize);
        }
        catch (OverflowException ex)
        {
          num2 = toExclusive;
          shouldQuit = true;
        }
        if (num2 > toExclusive)
          num2 = toExclusive;
        yield return new Tuple<long, long>(num1, num2);
        i += rangeSize;
      }
    }

    /// <summary>
    ///   Создает модуль разделения, разделяет диапазон, определяемый пользователем.
    /// </summary>
    /// <param name="fromInclusive">
    ///   Ниже включительно границу диапазона.
    /// </param>
    /// <param name="toExclusive">
    ///   Верхний монопольного границу диапазона.
    /// </param>
    /// <returns>Разделитель.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="toExclusive" /> Аргумент меньше или равно <paramref name="fromInclusive" /> аргумент.
    /// </exception>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive)
    {
      int num = 3;
      if (toExclusive <= fromInclusive)
        throw new ArgumentOutOfRangeException(nameof (toExclusive));
      int rangeSize = (toExclusive - fromInclusive) / (PlatformHelper.ProcessorCount * num);
      if (rangeSize == 0)
        rangeSize = 1;
      return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
    }

    /// <summary>
    ///   Создает модуль разделения, разделяет диапазон, определяемый пользователем.
    /// </summary>
    /// <param name="fromInclusive">
    ///   Ниже включительно границу диапазона.
    /// </param>
    /// <param name="toExclusive">
    ///   Верхний монопольного границу диапазона.
    /// </param>
    /// <param name="rangeSize">Размер каждого поддиапазона.</param>
    /// <returns>Разделитель.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="toExclusive" /> Аргумент меньше или равно <paramref name="fromInclusive" /> аргумент.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="rangeSize" /> Аргумент меньше или равно 0.
    /// </exception>
    [__DynamicallyInvokable]
    public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive, int rangeSize)
    {
      if (toExclusive <= fromInclusive)
        throw new ArgumentOutOfRangeException(nameof (toExclusive));
      if (rangeSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (rangeSize));
      return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
    }

    private static IEnumerable<Tuple<int, int>> CreateRanges(int fromInclusive, int toExclusive, int rangeSize)
    {
      bool shouldQuit = false;
      int i = fromInclusive;
      while (i < toExclusive && !shouldQuit)
      {
        int num1 = i;
        int num2;
        try
        {
          num2 = checked (i + rangeSize);
        }
        catch (OverflowException ex)
        {
          num2 = toExclusive;
          shouldQuit = true;
        }
        if (num2 > toExclusive)
          num2 = toExclusive;
        yield return new Tuple<int, int>(num1, num2);
        i += rangeSize;
      }
    }

    private static int GetDefaultChunkSize<TSource>()
    {
      return !typeof (TSource).IsValueType ? 512 / IntPtr.Size : (typeof (TSource).StructLayoutAttribute.Value != LayoutKind.Explicit ? 128 : Math.Max(1, 512 / Marshal.SizeOf(typeof (TSource))));
    }

    private abstract class DynamicPartitionEnumerator_Abstract<TSource, TSourceReader> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
    {
      protected static int s_defaultMaxChunkSize = Partitioner.GetDefaultChunkSize<TSource>();
      protected readonly TSourceReader m_sharedReader;
      protected Partitioner.SharedInt m_currentChunkSize;
      protected Partitioner.SharedInt m_localOffset;
      private const int CHUNK_DOUBLING_RATE = 3;
      private int m_doublingCountdown;
      protected readonly int m_maxChunkSize;
      protected readonly Partitioner.SharedLong m_sharedIndex;

      protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex)
        : this(sharedReader, sharedIndex, false)
      {
      }

      protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex, bool useSingleChunking)
      {
        this.m_sharedReader = sharedReader;
        this.m_sharedIndex = sharedIndex;
        this.m_maxChunkSize = useSingleChunking ? 1 : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>.s_defaultMaxChunkSize;
      }

      protected abstract bool GrabNextChunk(int requestedChunkSize);

      protected abstract bool HasNoElementsLeft { get; set; }

      public abstract KeyValuePair<long, TSource> Current { get; }

      public abstract void Dispose();

      public void Reset()
      {
        throw new NotSupportedException();
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      public bool MoveNext()
      {
        if (this.m_localOffset == null)
        {
          this.m_localOffset = new Partitioner.SharedInt(-1);
          this.m_currentChunkSize = new Partitioner.SharedInt(0);
          this.m_doublingCountdown = 3;
        }
        if (this.m_localOffset.Value < this.m_currentChunkSize.Value - 1)
        {
          ++this.m_localOffset.Value;
          return true;
        }
        int requestedChunkSize;
        if (this.m_currentChunkSize.Value == 0)
          requestedChunkSize = 1;
        else if (this.m_doublingCountdown > 0)
        {
          requestedChunkSize = this.m_currentChunkSize.Value;
        }
        else
        {
          requestedChunkSize = Math.Min(this.m_currentChunkSize.Value * 2, this.m_maxChunkSize);
          this.m_doublingCountdown = 3;
        }
        --this.m_doublingCountdown;
        if (!this.GrabNextChunk(requestedChunkSize))
          return false;
        this.m_localOffset.Value = 0;
        return true;
      }
    }

    private class DynamicPartitionerForIEnumerable<TSource> : OrderablePartitioner<TSource>
    {
      private IEnumerable<TSource> m_source;
      private readonly bool m_useSingleChunking;

      internal DynamicPartitionerForIEnumerable(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
        : base(true, false, true)
      {
        this.m_source = source;
        this.m_useSingleChunking = (uint) (partitionerOptions & EnumerablePartitionerOptions.NoBuffering) > 0U;
      }

      public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
      {
        if (partitionCount <= 0)
          throw new ArgumentOutOfRangeException(nameof (partitionCount));
        IEnumerator<KeyValuePair<long, TSource>>[] enumeratorArray = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
        IEnumerable<KeyValuePair<long, TSource>> keyValuePairs = (IEnumerable<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this.m_source.GetEnumerator(), this.m_useSingleChunking, true);
        for (int index = 0; index < partitionCount; ++index)
          enumeratorArray[index] = keyValuePairs.GetEnumerator();
        return (IList<IEnumerator<KeyValuePair<long, TSource>>>) enumeratorArray;
      }

      public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
      {
        return (IEnumerable<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this.m_source.GetEnumerator(), this.m_useSingleChunking, false);
      }

      public override bool SupportsDynamicPartitions
      {
        get
        {
          return true;
        }
      }

      private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable, IDisposable
      {
        private readonly IEnumerator<TSource> m_sharedReader;
        private Partitioner.SharedLong m_sharedIndex;
        private volatile KeyValuePair<long, TSource>[] m_FillBuffer;
        private volatile int m_FillBufferSize;
        private volatile int m_FillBufferCurrentPosition;
        private volatile int m_activeCopiers;
        private Partitioner.SharedBool m_hasNoElementsLeft;
        private Partitioner.SharedBool m_sourceDepleted;
        private object m_sharedLock;
        private bool m_disposed;
        private Partitioner.SharedInt m_activePartitionCount;
        private readonly bool m_useSingleChunking;

        internal InternalPartitionEnumerable(IEnumerator<TSource> sharedReader, bool useSingleChunking, bool isStaticPartitioning)
        {
          this.m_sharedReader = sharedReader;
          this.m_sharedIndex = new Partitioner.SharedLong(-1L);
          this.m_hasNoElementsLeft = new Partitioner.SharedBool(false);
          this.m_sourceDepleted = new Partitioner.SharedBool(false);
          this.m_sharedLock = new object();
          this.m_useSingleChunking = useSingleChunking;
          if (!this.m_useSingleChunking)
            this.m_FillBuffer = new KeyValuePair<long, TSource>[(PlatformHelper.ProcessorCount > 4 ? 4 : 1) * Partitioner.GetDefaultChunkSize<TSource>()];
          if (isStaticPartitioning)
            this.m_activePartitionCount = new Partitioner.SharedInt(0);
          else
            this.m_activePartitionCount = (Partitioner.SharedInt) null;
        }

        public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
        {
          if (this.m_disposed)
            throw new ObjectDisposedException(Environment.GetResourceString("PartitionerStatic_CanNotCallGetEnumeratorAfterSourceHasBeenDisposed"));
          return (IEnumerator<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex, this.m_hasNoElementsLeft, this.m_sharedLock, this.m_activePartitionCount, this, this.m_useSingleChunking);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
          return (IEnumerator) this.GetEnumerator();
        }

        private void TryCopyFromFillBuffer(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
        {
          actualNumElementsGrabbed = 0;
          KeyValuePair<long, TSource>[] fillBuffer = this.m_FillBuffer;
          if (fillBuffer == null || this.m_FillBufferCurrentPosition >= this.m_FillBufferSize)
            return;
          Interlocked.Increment(ref this.m_activeCopiers);
          int num = Interlocked.Add(ref this.m_FillBufferCurrentPosition, requestedChunkSize);
          int sourceIndex = num - requestedChunkSize;
          if (sourceIndex < this.m_FillBufferSize)
          {
            actualNumElementsGrabbed = num < this.m_FillBufferSize ? num : this.m_FillBufferSize - sourceIndex;
            Array.Copy((Array) fillBuffer, sourceIndex, (Array) destArray, 0, actualNumElementsGrabbed);
          }
          Interlocked.Decrement(ref this.m_activeCopiers);
        }

        internal bool GrabChunk(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
        {
          actualNumElementsGrabbed = 0;
          if (this.m_hasNoElementsLeft.Value)
            return false;
          if (this.m_useSingleChunking)
            return this.GrabChunk_Single(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
          return this.GrabChunk_Buffered(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
        }

        internal bool GrabChunk_Single(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
        {
          lock (this.m_sharedLock)
          {
            if (this.m_hasNoElementsLeft.Value)
              return false;
            try
            {
              if (this.m_sharedReader.MoveNext())
              {
                checked { ++this.m_sharedIndex.Value; }
                destArray[0] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
                actualNumElementsGrabbed = 1;
                return true;
              }
              this.m_sourceDepleted.Value = true;
              this.m_hasNoElementsLeft.Value = true;
              return false;
            }
            catch
            {
              this.m_sourceDepleted.Value = true;
              this.m_hasNoElementsLeft.Value = true;
              throw;
            }
          }
        }

        internal bool GrabChunk_Buffered(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
        {
          this.TryCopyFromFillBuffer(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
          if (actualNumElementsGrabbed == requestedChunkSize)
            return true;
          if (this.m_sourceDepleted.Value)
          {
            this.m_hasNoElementsLeft.Value = true;
            this.m_FillBuffer = (KeyValuePair<long, TSource>[]) null;
            return actualNumElementsGrabbed > 0;
          }
          lock (this.m_sharedLock)
          {
            if (this.m_sourceDepleted.Value)
              return actualNumElementsGrabbed > 0;
            try
            {
              if (this.m_activeCopiers > 0)
              {
                SpinWait spinWait = new SpinWait();
                while (this.m_activeCopiers > 0)
                  spinWait.SpinOnce();
              }
              while (actualNumElementsGrabbed < requestedChunkSize)
              {
                if (this.m_sharedReader.MoveNext())
                {
                  checked { ++this.m_sharedIndex.Value; }
                  destArray[actualNumElementsGrabbed] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
                  ++actualNumElementsGrabbed;
                }
                else
                {
                  this.m_sourceDepleted.Value = true;
                  break;
                }
              }
              KeyValuePair<long, TSource>[] fillBuffer = this.m_FillBuffer;
              if (!this.m_sourceDepleted.Value)
              {
                if (fillBuffer != null)
                {
                  if (this.m_FillBufferCurrentPosition >= fillBuffer.Length)
                  {
                    for (int index = 0; index < fillBuffer.Length; ++index)
                    {
                      if (this.m_sharedReader.MoveNext())
                      {
                        checked { ++this.m_sharedIndex.Value; }
                        fillBuffer[index] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
                      }
                      else
                      {
                        this.m_sourceDepleted.Value = true;
                        this.m_FillBufferSize = index;
                        break;
                      }
                    }
                    this.m_FillBufferCurrentPosition = 0;
                  }
                }
              }
            }
            catch
            {
              this.m_sourceDepleted.Value = true;
              this.m_hasNoElementsLeft.Value = true;
              throw;
            }
          }
          return actualNumElementsGrabbed > 0;
        }

        public void Dispose()
        {
          if (this.m_disposed)
            return;
          this.m_disposed = true;
          this.m_sharedReader.Dispose();
        }
      }

      private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, IEnumerator<TSource>>
      {
        private KeyValuePair<long, TSource>[] m_localList;
        private readonly Partitioner.SharedBool m_hasNoElementsLeft;
        private readonly object m_sharedLock;
        private readonly Partitioner.SharedInt m_activePartitionCount;
        private Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable m_enumerable;

        internal InternalPartitionEnumerator(IEnumerator<TSource> sharedReader, Partitioner.SharedLong sharedIndex, Partitioner.SharedBool hasNoElementsLeft, object sharedLock, Partitioner.SharedInt activePartitionCount, Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable enumerable, bool useSingleChunking)
          : base(sharedReader, sharedIndex, useSingleChunking)
        {
          this.m_hasNoElementsLeft = hasNoElementsLeft;
          this.m_sharedLock = sharedLock;
          this.m_enumerable = enumerable;
          this.m_activePartitionCount = activePartitionCount;
          if (this.m_activePartitionCount == null)
            return;
          Interlocked.Increment(ref this.m_activePartitionCount.Value);
        }

        protected override bool GrabNextChunk(int requestedChunkSize)
        {
          if (this.HasNoElementsLeft)
            return false;
          if (this.m_localList == null)
            this.m_localList = new KeyValuePair<long, TSource>[this.m_maxChunkSize];
          return this.m_enumerable.GrabChunk(this.m_localList, requestedChunkSize, ref this.m_currentChunkSize.Value);
        }

        protected override bool HasNoElementsLeft
        {
          get
          {
            return this.m_hasNoElementsLeft.Value;
          }
          set
          {
            this.m_hasNoElementsLeft.Value = true;
          }
        }

        public override KeyValuePair<long, TSource> Current
        {
          get
          {
            if (this.m_currentChunkSize == null)
              throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
            return this.m_localList[this.m_localOffset.Value];
          }
        }

        public override void Dispose()
        {
          if (this.m_activePartitionCount == null || Interlocked.Decrement(ref this.m_activePartitionCount.Value) != 0)
            return;
          this.m_enumerable.Dispose();
        }
      }
    }

    private abstract class DynamicPartitionerForIndexRange_Abstract<TSource, TCollection> : OrderablePartitioner<TSource>
    {
      private TCollection m_data;

      protected DynamicPartitionerForIndexRange_Abstract(TCollection data)
        : base(true, false, true)
      {
        this.m_data = data;
      }

      protected abstract IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TCollection data);

      public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
      {
        if (partitionCount <= 0)
          throw new ArgumentOutOfRangeException(nameof (partitionCount));
        IEnumerator<KeyValuePair<long, TSource>>[] enumeratorArray = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
        IEnumerable<KeyValuePair<long, TSource>> partitionsFactory = this.GetOrderableDynamicPartitions_Factory(this.m_data);
        for (int index = 0; index < partitionCount; ++index)
          enumeratorArray[index] = partitionsFactory.GetEnumerator();
        return (IList<IEnumerator<KeyValuePair<long, TSource>>>) enumeratorArray;
      }

      public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
      {
        return this.GetOrderableDynamicPartitions_Factory(this.m_data);
      }

      public override bool SupportsDynamicPartitions
      {
        get
        {
          return true;
        }
      }
    }

    private abstract class DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSourceReader> : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>
    {
      protected int m_startIndex;

      protected DynamicPartitionEnumeratorForIndexRange_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex)
        : base(sharedReader, sharedIndex)
      {
      }

      protected abstract int SourceCount { get; }

      protected override bool GrabNextChunk(int requestedChunkSize)
      {
        while (!this.HasNoElementsLeft)
        {
          long comparand = Volatile.Read(ref this.m_sharedIndex.Value);
          if (this.HasNoElementsLeft)
            return false;
          long num = Math.Min((long) (this.SourceCount - 1), comparand + (long) requestedChunkSize);
          if (Interlocked.CompareExchange(ref this.m_sharedIndex.Value, num, comparand) == comparand)
          {
            this.m_currentChunkSize.Value = (int) (num - comparand);
            this.m_localOffset.Value = -1;
            this.m_startIndex = (int) (comparand + 1L);
            return true;
          }
        }
        return false;
      }

      protected override bool HasNoElementsLeft
      {
        get
        {
          return Volatile.Read(ref this.m_sharedIndex.Value) >= (long) (this.SourceCount - 1);
        }
        set
        {
        }
      }

      public override void Dispose()
      {
      }
    }

    private class DynamicPartitionerForIList<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, IList<TSource>>
    {
      internal DynamicPartitionerForIList(IList<TSource> source)
        : base(source)
      {
      }

      protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(IList<TSource> m_data)
      {
        return (IEnumerable<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerable(m_data);
      }

      private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
      {
        private readonly IList<TSource> m_sharedReader;
        private Partitioner.SharedLong m_sharedIndex;

        internal InternalPartitionEnumerable(IList<TSource> sharedReader)
        {
          this.m_sharedReader = sharedReader;
          this.m_sharedIndex = new Partitioner.SharedLong(-1L);
        }

        public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
        {
          return (IEnumerator<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
          return (IEnumerator) this.GetEnumerator();
        }
      }

      private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, IList<TSource>>
      {
        internal InternalPartitionEnumerator(IList<TSource> sharedReader, Partitioner.SharedLong sharedIndex)
          : base(sharedReader, sharedIndex)
        {
        }

        protected override int SourceCount
        {
          get
          {
            return this.m_sharedReader.Count;
          }
        }

        public override KeyValuePair<long, TSource> Current
        {
          get
          {
            if (this.m_currentChunkSize == null)
              throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
            return new KeyValuePair<long, TSource>((long) (this.m_startIndex + this.m_localOffset.Value), this.m_sharedReader[this.m_startIndex + this.m_localOffset.Value]);
          }
        }
      }
    }

    private class DynamicPartitionerForArray<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, TSource[]>
    {
      internal DynamicPartitionerForArray(TSource[] source)
        : base(source)
      {
      }

      protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TSource[] m_data)
      {
        return (IEnumerable<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerable(m_data);
      }

      private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
      {
        private readonly TSource[] m_sharedReader;
        private Partitioner.SharedLong m_sharedIndex;

        internal InternalPartitionEnumerable(TSource[] sharedReader)
        {
          this.m_sharedReader = sharedReader;
          this.m_sharedIndex = new Partitioner.SharedLong(-1L);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
          return (IEnumerator) this.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
        {
          return (IEnumerator<KeyValuePair<long, TSource>>) new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex);
        }
      }

      private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSource[]>
      {
        internal InternalPartitionEnumerator(TSource[] sharedReader, Partitioner.SharedLong sharedIndex)
          : base(sharedReader, sharedIndex)
        {
        }

        protected override int SourceCount
        {
          get
          {
            return this.m_sharedReader.Length;
          }
        }

        public override KeyValuePair<long, TSource> Current
        {
          get
          {
            if (this.m_currentChunkSize == null)
              throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
            return new KeyValuePair<long, TSource>((long) (this.m_startIndex + this.m_localOffset.Value), this.m_sharedReader[this.m_startIndex + this.m_localOffset.Value]);
          }
        }
      }
    }

    private abstract class StaticIndexRangePartitioner<TSource, TCollection> : OrderablePartitioner<TSource>
    {
      protected StaticIndexRangePartitioner()
        : base(true, true, true)
      {
      }

      protected abstract int SourceCount { get; }

      protected abstract IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex);

      public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
      {
        if (partitionCount <= 0)
          throw new ArgumentOutOfRangeException(nameof (partitionCount));
        int result;
        int num = Math.DivRem(this.SourceCount, partitionCount, out result);
        IEnumerator<KeyValuePair<long, TSource>>[] enumeratorArray = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
        int endIndex = -1;
        for (int index = 0; index < partitionCount; ++index)
        {
          int startIndex = endIndex + 1;
          endIndex = index >= result ? startIndex + num - 1 : startIndex + num;
          enumeratorArray[index] = this.CreatePartition(startIndex, endIndex);
        }
        return (IList<IEnumerator<KeyValuePair<long, TSource>>>) enumeratorArray;
      }
    }

    private abstract class StaticIndexRangePartition<TSource> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
    {
      protected readonly int m_startIndex;
      protected readonly int m_endIndex;
      protected volatile int m_offset;

      protected StaticIndexRangePartition(int startIndex, int endIndex)
      {
        this.m_startIndex = startIndex;
        this.m_endIndex = endIndex;
        this.m_offset = startIndex - 1;
      }

      public abstract KeyValuePair<long, TSource> Current { get; }

      public void Dispose()
      {
      }

      public void Reset()
      {
        throw new NotSupportedException();
      }

      public bool MoveNext()
      {
        if (this.m_offset < this.m_endIndex)
        {
          ++this.m_offset;
          return true;
        }
        this.m_offset = this.m_endIndex + 1;
        return false;
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }
    }

    private class StaticIndexRangePartitionerForIList<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, IList<TSource>>
    {
      private IList<TSource> m_list;

      internal StaticIndexRangePartitionerForIList(IList<TSource> list)
      {
        this.m_list = list;
      }

      protected override int SourceCount
      {
        get
        {
          return this.m_list.Count;
        }
      }

      protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
      {
        return (IEnumerator<KeyValuePair<long, TSource>>) new Partitioner.StaticIndexRangePartitionForIList<TSource>(this.m_list, startIndex, endIndex);
      }
    }

    private class StaticIndexRangePartitionForIList<TSource> : Partitioner.StaticIndexRangePartition<TSource>
    {
      private volatile IList<TSource> m_list;

      internal StaticIndexRangePartitionForIList(IList<TSource> list, int startIndex, int endIndex)
        : base(startIndex, endIndex)
      {
        this.m_list = list;
      }

      public override KeyValuePair<long, TSource> Current
      {
        get
        {
          if (this.m_offset < this.m_startIndex)
            throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
          return new KeyValuePair<long, TSource>((long) this.m_offset, this.m_list[this.m_offset]);
        }
      }
    }

    private class StaticIndexRangePartitionerForArray<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, TSource[]>
    {
      private TSource[] m_array;

      internal StaticIndexRangePartitionerForArray(TSource[] array)
      {
        this.m_array = array;
      }

      protected override int SourceCount
      {
        get
        {
          return this.m_array.Length;
        }
      }

      protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
      {
        return (IEnumerator<KeyValuePair<long, TSource>>) new Partitioner.StaticIndexRangePartitionForArray<TSource>(this.m_array, startIndex, endIndex);
      }
    }

    private class StaticIndexRangePartitionForArray<TSource> : Partitioner.StaticIndexRangePartition<TSource>
    {
      private volatile TSource[] m_array;

      internal StaticIndexRangePartitionForArray(TSource[] array, int startIndex, int endIndex)
        : base(startIndex, endIndex)
      {
        this.m_array = array;
      }

      public override KeyValuePair<long, TSource> Current
      {
        get
        {
          if (this.m_offset < this.m_startIndex)
            throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
          return new KeyValuePair<long, TSource>((long) this.m_offset, this.m_array[this.m_offset]);
        }
      }
    }

    private class SharedInt
    {
      internal volatile int Value;

      internal SharedInt(int value)
      {
        this.Value = value;
      }
    }

    private class SharedBool
    {
      internal volatile bool Value;

      internal SharedBool(bool value)
      {
        this.Value = value;
      }
    }

    private class SharedLong
    {
      internal long Value;

      internal SharedLong(long value)
      {
        this.Value = value;
      }
    }
  }
}
