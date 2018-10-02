// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.OrderablePartitioner`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Collections.Concurrent
{
  /// <summary>
  ///   Представляет определенный способ разделения упорядочиваемого источника данных на несколько разделов.
  /// </summary>
  /// <typeparam name="TSource">Тип элементов в коллекции.</typeparam>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public abstract class OrderablePartitioner<TSource> : Partitioner<TSource>
  {
    /// <summary>
    ///   Вызывается из конструкторов в производных классах для инициализации <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> класса с заданным ограничениям для ключей индекса.
    /// </summary>
    /// <param name="keysOrderedInEachPartition">
    ///   Указывает, формируется ли элементы каждого раздела в порядке возрастания ключей.
    /// </param>
    /// <param name="keysOrderedAcrossPartitions">
    ///   Указывает, является ли элементы более раннего раздела всегда находятся перед элементами более позднего раздела.
    ///    Значение true, если каждый элемент в раздел 0 имеет меньше порядкового ключа любого элемента раздела 1, каждый элемент в разделе 1 имеет меньше порядкового ключа любого элемента раздела 2 и т. д.
    /// </param>
    /// <param name="keysNormalized">
    ///   Указывает, нормализованы ли ключи.
    ///    Если значение равно true, все порядковые ключи являются несовпадающими целыми числами в диапазоне [0..
    ///    numberOfElements-1].
    ///    Если false, необходимо по-прежнему упорядочивания ключей отличаться, но считается только их относительного порядка, не абсолютные значения.
    /// </param>
    [__DynamicallyInvokable]
    protected OrderablePartitioner(bool keysOrderedInEachPartition, bool keysOrderedAcrossPartitions, bool keysNormalized)
    {
      this.KeysOrderedInEachPartition = keysOrderedInEachPartition;
      this.KeysOrderedAcrossPartitions = keysOrderedAcrossPartitions;
      this.KeysNormalized = keysNormalized;
    }

    /// <summary>
    ///   Делит базовую коллекцию в указанное число упорядочиваемый секций.
    /// </summary>
    /// <param name="partitionCount">Число создаваемых разделов.</param>
    /// <returns>
    ///   Значение, содержащее список <paramref name="partitionCount" /> перечислителей.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount);

    /// <summary>
    ///   Создает объект, который может разделить базовую коллекцию переменное число разделов.
    /// </summary>
    /// <returns>
    ///   Объект, который может создать разделы базового источника данных.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Динамическое секционирование не поддерживается этим модулем разделения.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
    {
      throw new NotSupportedException(Environment.GetResourceString("Partitioner_DynamicPartitionsNotSupported"));
    }

    /// <summary>
    ///   Получает значение, указывающее, упорядочиваются элементы каждого раздела в порядке возрастания ключей.
    /// </summary>
    /// <returns>
    ///   значение true, если в каждом разделе элементы размещены в порядке возрастания ключей; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool KeysOrderedInEachPartition { [__DynamicallyInvokable] get; private set; }

    /// <summary>
    ///   Получает значение, указывающее элементы более раннего раздела всегда находятся перед элементами более позднего раздела.
    /// </summary>
    /// <returns>
    ///   значение true, если элементы более раннего раздела всегда находятся перед элементами более позднего раздела; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool KeysOrderedAcrossPartitions { [__DynamicallyInvokable] get; private set; }

    /// <summary>
    ///   Получает значение, указывающее порядок ключи нормализованы.
    /// </summary>
    /// <returns>
    ///   значение true, если ключи нормализованы; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public bool KeysNormalized { [__DynamicallyInvokable] get; private set; }

    /// <summary>
    ///   Делит базовую коллекцию на указанное число упорядоченных разделов.
    /// </summary>
    /// <param name="partitionCount">Число создаваемых разделов.</param>
    /// <returns>
    ///   Значение, содержащее список <paramref name="partitionCount" /> перечислителей.
    /// </returns>
    [__DynamicallyInvokable]
    public override IList<IEnumerator<TSource>> GetPartitions(int partitionCount)
    {
      IList<IEnumerator<KeyValuePair<long, TSource>>> orderablePartitions = this.GetOrderablePartitions(partitionCount);
      if (orderablePartitions.Count != partitionCount)
        throw new InvalidOperationException("OrderablePartitioner_GetPartitions_WrongNumberOfPartitions");
      IEnumerator<TSource>[] enumeratorArray = new IEnumerator<TSource>[partitionCount];
      for (int index = 0; index < partitionCount; ++index)
        enumeratorArray[index] = (IEnumerator<TSource>) new OrderablePartitioner<TSource>.EnumeratorDropIndices(orderablePartitions[index]);
      return (IList<IEnumerator<TSource>>) enumeratorArray;
    }

    /// <summary>
    ///   Создает объект, который может разделить базовую коллекцию переменное число разделов.
    /// </summary>
    /// <returns>
    ///   Объект, который может создать разделы базового источника данных.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Создание динамических разделов не поддерживается базовым классом.
    ///    Он должен быть реализован в производных классах.
    /// </exception>
    [__DynamicallyInvokable]
    public override IEnumerable<TSource> GetDynamicPartitions()
    {
      return (IEnumerable<TSource>) new OrderablePartitioner<TSource>.EnumerableDropIndices(this.GetOrderableDynamicPartitions());
    }

    private class EnumerableDropIndices : IEnumerable<TSource>, IEnumerable, IDisposable
    {
      private readonly IEnumerable<KeyValuePair<long, TSource>> m_source;

      public EnumerableDropIndices(IEnumerable<KeyValuePair<long, TSource>> source)
      {
        this.m_source = source;
      }

      public IEnumerator<TSource> GetEnumerator()
      {
        return (IEnumerator<TSource>) new OrderablePartitioner<TSource>.EnumeratorDropIndices(this.m_source.GetEnumerator());
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) this.GetEnumerator();
      }

      public void Dispose()
      {
        (this.m_source as IDisposable)?.Dispose();
      }
    }

    private class EnumeratorDropIndices : IEnumerator<TSource>, IDisposable, IEnumerator
    {
      private readonly IEnumerator<KeyValuePair<long, TSource>> m_source;

      public EnumeratorDropIndices(IEnumerator<KeyValuePair<long, TSource>> source)
      {
        this.m_source = source;
      }

      public bool MoveNext()
      {
        return this.m_source.MoveNext();
      }

      public TSource Current
      {
        get
        {
          return this.m_source.Current.Value;
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      public void Dispose()
      {
        this.m_source.Dispose();
      }

      public void Reset()
      {
        this.m_source.Reset();
      }
    }
  }
}
