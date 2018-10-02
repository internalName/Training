// Decompiled with JetBrains decompiler
// Type: System.Collections.IHashCodeProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Предоставляет хэш-код объекта, используя пользовательскую хеш-функцию.
  /// </summary>
  [Obsolete("Please use IEqualityComparer instead.")]
  [ComVisible(true)]
  public interface IHashCodeProvider
  {
    /// <summary>Возвращает хэш-код указанного объекта.</summary>
    /// <param name="obj">
    ///   <see cref="T:System.Object" /> Для которого должен быть возвращен хэш-код.
    /// </param>
    /// <returns>Хэш-код указанного объекта.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Тип <paramref name="obj" /> является ссылочным типом и <paramref name="obj" /> — <see langword="null" />.
    /// </exception>
    int GetHashCode(object obj);
  }
}
