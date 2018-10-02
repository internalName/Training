// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SHA1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Вычисляет <see cref="T:System.Security.Cryptography.SHA1" /> хэш для входных данных.
  /// </summary>
  [ComVisible(true)]
  public abstract class SHA1 : HashAlgorithm
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.SHA1" />.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Политика для этого объекта не соответствует стандарту FIPS.
    /// </exception>
    protected SHA1()
    {
      this.HashSizeValue = 160;
    }

    /// <summary>
    ///   Создает экземпляр реализации по умолчанию <see cref="T:System.Security.Cryptography.SHA1" />.
    /// </summary>
    /// <returns>
    ///   Новый экземпляр класса <see cref="T:System.Security.Cryptography.SHA1" />.
    /// </returns>
    public static SHA1 Create()
    {
      return SHA1.Create("System.Security.Cryptography.SHA1");
    }

    /// <summary>
    ///   Создает экземпляр заданной реализации класса <see cref="T:System.Security.Cryptography.SHA1" />.
    /// </summary>
    /// <param name="hashName">
    ///   Имя конкретной реализации <see cref="T:System.Security.Cryptography.SHA1" /> для использования.
    /// </param>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.Security.Cryptography.SHA1" /> с помощью заданной реализации.
    /// </returns>
    public static SHA1 Create(string hashName)
    {
      return (SHA1) CryptoConfig.CreateFromName(hashName);
    }
  }
}
