// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.MD5
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет абстрактный класс, из которого создаются все реализации <see cref="T:System.Security.Cryptography.MD5" /> хэш-алгоритм наследование.
  /// </summary>
  [ComVisible(true)]
  public abstract class MD5 : HashAlgorithm
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.MD5" />.
    /// </summary>
    protected MD5()
    {
      this.HashSizeValue = 128;
    }

    /// <summary>
    ///   Создает экземпляр реализации по умолчанию <see cref="T:System.Security.Cryptography.MD5" /> хэш-алгоритма.
    /// </summary>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.Security.Cryptography.MD5" /> хэш-алгоритма.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Этот алгоритм был использован с включенным режимом FIPS, однако он не совместим с FIPS.
    /// </exception>
    public static MD5 Create()
    {
      return MD5.Create("System.Security.Cryptography.MD5");
    }

    /// <summary>
    ///   Создает экземпляр заданной реализации <see cref="T:System.Security.Cryptography.MD5" /> хэш-алгоритма.
    /// </summary>
    /// <param name="algName">
    ///   Имя конкретной реализации <see cref="T:System.Security.Cryptography.MD5" /> для использования.
    /// </param>
    /// <returns>
    ///   Новый экземпляр заданной реализации класса <see cref="T:System.Security.Cryptography.MD5" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Алгоритм, описание которого <paramref name="algName" /> параметр использовался с включенным режимом федеральным стандартам обработки информации (FIPS), но не является FIPS-совместимым.
    /// </exception>
    public static MD5 Create(string algName)
    {
      return (MD5) CryptoConfig.CreateFromName(algName);
    }
  }
}
