// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RIPEMD160
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет абстрактный класс, от которого наследуются все реализации хэш-алгоритма MD160.
  /// </summary>
  [ComVisible(true)]
  public abstract class RIPEMD160 : HashAlgorithm
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RIPEMD160" />.
    /// </summary>
    protected RIPEMD160()
    {
      this.HashSizeValue = 160;
    }

    /// <summary>
    ///   Создает экземпляр реализации по умолчанию <see cref="T:System.Security.Cryptography.RIPEMD160" /> хэш-алгоритма.
    /// </summary>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.Security.Cryptography.RIPEMD160" /> хэш-алгоритма.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Алгоритм использовался с включенным режимом федеральным стандартам обработки информации (FIPS), но не является FIPS-совместимым.
    /// </exception>
    public static RIPEMD160 Create()
    {
      return RIPEMD160.Create("System.Security.Cryptography.RIPEMD160");
    }

    /// <summary>
    ///   Создает экземпляр заданной реализации <see cref="T:System.Security.Cryptography.RIPEMD160" /> хэш-алгоритма.
    /// </summary>
    /// <param name="hashName">
    ///   Имя конкретной реализации <see cref="T:System.Security.Cryptography.RIPEMD160" /> для использования.
    /// </param>
    /// <returns>
    ///   Новый экземпляр заданной реализации класса <see cref="T:System.Security.Cryptography.RIPEMD160" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Алгоритм, описание которого <paramref name="hashName" /> параметр использовался с включенным режимом федеральным стандартам обработки информации (FIPS), но не является FIPS-совместимым.
    /// </exception>
    public static RIPEMD160 Create(string hashName)
    {
      return (RIPEMD160) CryptoConfig.CreateFromName(hashName);
    }
  }
}
