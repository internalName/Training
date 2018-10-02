// Decompiled with JetBrains decompiler
// Type: System.IComparable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Определяет обобщенный метод сравнения, который реализуется типом значения или классом для упорядочения или сортировки экземпляров.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IComparable
  {
    /// <summary>
    ///   Сравнивает текущий экземпляр с другим объектом того же типа и возвращает целое число, которое показывает, расположен ли текущий экземпляр перед, после или на той же позиции в порядке сортировки, что и другой объект.
    /// </summary>
    /// <param name="obj">
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
    ///         Данный экземпляр предшествует параметру <paramref name="obj" /> в порядке сортировки.
    /// 
    ///         Нуль
    /// 
    ///         Этот экземпляр выполняется в той же позиции в порядке сортировки, что <paramref name="obj" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Данный экземпляр следует за параметром <paramref name="obj" /> в порядке сортировки.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="obj" />не совпадает с типом данного экземпляра.
    /// </exception>
    [__DynamicallyInvokable]
    int CompareTo(object obj);
  }
}
