// Decompiled with JetBrains decompiler
// Type: System.Collections.ICollection
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Определяет размер, перечислители и методы синхронизации для всех неуниверсальных коллекций.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface ICollection : IEnumerable
  {
    /// <summary>
    ///   Копирует элементы коллекции <see cref="T:System.Collections.ICollection" /> в массив <see cref="T:System.Array" />, начиная с указанного индекса массива <see cref="T:System.Array" />.
    /// </summary>
    /// <param name="array">
    ///   Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса <see cref="T:System.Collections.ICollection" />.
    ///    Массив <see cref="T:System.Array" /> должен иметь индексацию, начинающуюся с нуля.
    /// </param>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс в массиве <paramref name="array" />, указывающий начало копирования.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="array" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Число элементов в исходном массиве <see cref="T:System.Collections.ICollection" /> больше доступного места от положения, заданного значением параметра <paramref name="index" />, до конца массива назначения <paramref name="array" />.
    /// 
    ///   -или-
    /// 
    ///   Тип исходного массива <see cref="T:System.Collections.ICollection" /> не может быть автоматически приведен к типу массива назначения <paramref name="array" />.
    /// </exception>
    [__DynamicallyInvokable]
    void CopyTo(Array array, int index);

    /// <summary>
    ///   Получает число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.ICollection" />.
    /// </summary>
    /// <returns>
    ///   Число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.ICollection" />.
    /// </returns>
    [__DynamicallyInvokable]
    int Count { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Получает объект, с помощью которого можно синхронизировать доступ к коллекции <see cref="T:System.Collections.ICollection" />.
    /// </summary>
    /// <returns>
    ///   Объект, который может использоваться для синхронизации доступа к <see cref="T:System.Collections.ICollection" />.
    /// </returns>
    [__DynamicallyInvokable]
    object SyncRoot { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает значение, показывающее, является ли доступ к коллекции <see cref="T:System.Collections.ICollection" /> синхронизированным (потокобезопасным).
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если доступ к классу <see cref="T:System.Collections.ICollection" /> является синхронизированным (потокобезопасным); в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool IsSynchronized { [__DynamicallyInvokable] get; }
  }
}
