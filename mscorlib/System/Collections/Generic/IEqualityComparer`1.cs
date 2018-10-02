// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.IEqualityComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Generic
{
  /// <summary>
  ///   Определяет методы, поддерживающие сравнение объектов на предмет равенства.
  /// </summary>
  /// <typeparam name="T">Тип объектов для сравнения.</typeparam>
  [__DynamicallyInvokable]
  public interface IEqualityComparer<in T>
  {
    /// <summary>Определяет, равны ли два указанных объекта.</summary>
    /// <param name="x">
    ///   Первый объект типа <paramref name="T" /> для сравнения.
    /// </param>
    /// <param name="y">
    ///   Второй объект типа <paramref name="T" /> для сравнения.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если указанные объекты равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool Equals(T x, T y);

    /// <summary>Возвращает хэш-код указанного объекта.</summary>
    /// <param name="obj">
    ///   <see cref="T:System.Object" /> Для которого должен быть возвращен хэш-код.
    /// </param>
    /// <returns>Хэш-код указанного объекта.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Тип <paramref name="obj" /> является ссылочным типом и <paramref name="obj" /> — <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    int GetHashCode(T obj);
  }
}
