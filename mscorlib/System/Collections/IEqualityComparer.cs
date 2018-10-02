// Decompiled with JetBrains decompiler
// Type: System.Collections.IEqualityComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Определяет методы, поддерживающие сравнение объектов на предмет равенства.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IEqualityComparer
  {
    /// <summary>Определяет, равны ли два указанных объекта.</summary>
    /// <param name="x">Первый из сравниваемых объектов.</param>
    /// <param name="y">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если указанные объекты равны; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="x" /> и <paramref name="y" /> имеют разные типы и ни один могут сравниваться с другими.
    /// </exception>
    [__DynamicallyInvokable]
    bool Equals(object x, object y);

    /// <summary>Возвращает хэш-код указанного объекта.</summary>
    /// <param name="obj">
    ///   <see cref="T:System.Object" /> Для которого должен быть возвращен хэш-код.
    /// </param>
    /// <returns>Хэш-код указанного объекта.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Тип <paramref name="obj" /> является ссылочным типом и <paramref name="obj" /> — <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    int GetHashCode(object obj);
  }
}
