// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.Partitioner`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Collections.Concurrent
{
  /// <summary>
  ///   Представляет определенный способ разделения источника данных на несколько разделов.
  /// </summary>
  /// <typeparam name="TSource">Тип элементов в коллекции.</typeparam>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public abstract class Partitioner<TSource>
  {
    /// <summary>
    ///   Делит базовую коллекцию на указанное число разделов.
    /// </summary>
    /// <param name="partitionCount">Число создаваемых разделов.</param>
    /// <returns>
    ///   Значение, содержащее список <paramref name="partitionCount" /> перечислителей.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract IList<IEnumerator<TSource>> GetPartitions(int partitionCount);

    /// <summary>
    ///   Получает значение, указывающее дополнительные разделы могут создаваться динамически.
    /// </summary>
    /// <returns>
    ///   значение true, если <see cref="T:System.Collections.Concurrent.Partitioner`1" /> разделы можно создать динамически по запросу; значение false, если <see cref="T:System.Collections.Concurrent.Partitioner`1" /> только можно выделить секций статически.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool SupportsDynamicPartitions
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>
    ///   Создает объект, который может разделить базовую коллекцию переменное число разделов.
    /// </summary>
    /// <returns>
    ///   Объект, который может создать разделы базового источника данных.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Создание динамических разделов не поддерживается базовым классом.
    ///    Вам необходимо реализовать в производном классе.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual IEnumerable<TSource> GetDynamicPartitions()
    {
      throw new NotSupportedException(Environment.GetResourceString("Partitioner_DynamicPartitionsNotSupported"));
    }

    /// <summary>Создает новый экземпляр модуля разделения.</summary>
    [__DynamicallyInvokable]
    protected Partitioner()
    {
    }
  }
}
