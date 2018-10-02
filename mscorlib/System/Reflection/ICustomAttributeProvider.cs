// Decompiled with JetBrains decompiler
// Type: System.Reflection.ICustomAttributeProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Предоставляет настраиваемые атрибуты для объектов отражения, которые их поддерживают.
  /// </summary>
  [ComVisible(true)]
  public interface ICustomAttributeProvider
  {
    /// <summary>
    ///   Возвращает массив настраиваемых атрибутов, определенных для этого элемента с учетом типа, или пустой массив, если отсутствуют настраиваемые атрибуты определенного типа.
    /// </summary>
    /// <param name="attributeType">Тип настраиваемых атрибутов.</param>
    /// <param name="inherit">
    ///   Значение <see langword="true" />, если требуется просмотреть цепочку иерархии для поиска унаследованного настраиваемого атрибута.
    /// </param>
    /// <returns>
    ///   Массив объектов, представляющих настраиваемые атрибуты, или пустой массив.
    /// </returns>
    /// <exception cref="T:System.TypeLoadException">
    ///   Невозможно загрузить тип настраиваемого атрибута.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>
    ///   Возвращает массив всех настраиваемых атрибутов, определенных для этого элемента, за исключением именованных атрибутов, или пустой массив, если атрибуты отсутствуют.
    /// </summary>
    /// <param name="inherit">
    ///   Значение <see langword="true" />, если требуется просмотреть цепочку иерархии для поиска унаследованного настраиваемого атрибута.
    /// </param>
    /// <returns>
    ///   Массив объектов, представляющих настраиваемые атрибуты, или пустой массив.
    /// </returns>
    /// <exception cref="T:System.TypeLoadException">
    ///   Невозможно загрузить тип настраиваемого атрибута.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Для этого элемента определено более одного атрибута типа <paramref name="attributeType" />.
    /// </exception>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>
    ///   Указывает, сколько экземпляров <paramref name="attributeType" /> определено для этого элемента.
    /// </summary>
    /// <param name="attributeType">Тип настраиваемых атрибутов.</param>
    /// <param name="inherit">
    ///   Значение <see langword="true" />, если требуется просмотреть цепочку иерархии для поиска унаследованного настраиваемого атрибута.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если для этого элемента определен тип <paramref name="attributeType" />. В противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsDefined(Type attributeType, bool inherit);
  }
}
