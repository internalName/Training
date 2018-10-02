// Decompiled with JetBrains decompiler
// Type: System.IComparable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Определяет обобщенный метод сравнения, тип значения или класс, который используется для создания метода сравнения с целью упорядочения или сортировки экземпляров.
  /// </summary>
  /// <typeparam name="T">Тип объекта для сравнения.</typeparam>
  [__DynamicallyInvokable]
  public interface IComparable<in T>
  {
    /// <summary>
    ///   Сравнивает текущий экземпляр с другим объектом того же типа и возвращает целое число, которое показывает, расположен ли текущий экземпляр перед, после или на той же позиции в порядке сортировки, что и другой объект.
    /// </summary>
    /// <param name="other">
    ///   Объект для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    /// Значение, указывающее, каков относительный порядок сравниваемых объектов.
    ///  Возвращаемые значения представляют следующие результаты сравнения.
    /// 
    ///         Значение
    /// 
    ///         Значение
    /// 
    ///         Меньше нуля
    /// 
    ///         Данный экземпляр предшествует параметру <paramref name="other" /> в порядке сортировки.
    /// 
    ///         Нуль
    /// 
    ///         В той же позиции в порядке сортировки, что происходит этот экземпляр <paramref name="other" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Данный экземпляр следует за параметром <paramref name="other" /> в порядке сортировки.
    ///       </returns>
    [__DynamicallyInvokable]
    int CompareTo(T other);
  }
}
