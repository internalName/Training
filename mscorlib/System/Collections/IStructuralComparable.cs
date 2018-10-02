// Decompiled with JetBrains decompiler
// Type: System.Collections.IStructuralComparable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections
{
  /// <summary>
  ///   Поддерживает структурное сравнение объектов коллекции.
  /// </summary>
  [__DynamicallyInvokable]
  public interface IStructuralComparable
  {
    /// <summary>
    ///   Определяет позицию текущего объекта коллекции относительно другого объекта в порядке сортировки (находится перед другим объектов, на одной позиции с ним или после другого объекта).
    /// </summary>
    /// <param name="other">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <param name="comparer">
    ///   Объект, который сравнивает с соответствующим членам члены текущего объекта коллекции <paramref name="other" />.
    /// </param>
    /// <returns>
    /// Целое число, показывающее связь между текущий объект коллекции <paramref name="other" />, как показано в следующей таблице.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Описание
    /// 
    ///         -1
    /// 
    ///         Текущий экземпляр предшествует параметру <paramref name="other" />.
    /// 
    ///         0
    /// 
    ///         Текущий экземпляр и <paramref name="other" /> равны.
    /// 
    ///         1
    /// 
    ///         Текущий экземпляр стоит после <paramref name="other" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Этот экземпляр и <paramref name="other" /> не принадлежат одному типу.
    /// </exception>
    [__DynamicallyInvokable]
    int CompareTo(object other, IComparer comparer);
  }
}
