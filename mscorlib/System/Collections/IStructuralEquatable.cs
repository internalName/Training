// Decompiled with JetBrains decompiler
// Type: System.Collections.IStructuralEquatable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections
{
  /// <summary>
  ///   Определяет методы, поддерживающие сравнение объектов на предмет структурного равенства.
  /// </summary>
  [__DynamicallyInvokable]
  public interface IStructuralEquatable
  {
    /// <summary>
    ///   Определяет, является ли объект структурное равенство с текущим экземпляром.
    /// </summary>
    /// <param name="other">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <param name="comparer">
    ///   Объект, определяющий, равны ли текущий экземпляр и объект <paramref name="other" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если эти два объекта равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool Equals(object other, IEqualityComparer comparer);

    /// <summary>Возвращает хэш-код текущего экземпляра.</summary>
    /// <param name="comparer">
    ///   Объект, вычисляющий хэш-код текущего объекта.
    /// </param>
    /// <returns>Хэш-код для текущего экземпляра.</returns>
    [__DynamicallyInvokable]
    int GetHashCode(IEqualityComparer comparer);
  }
}
