// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HMACSHA256
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Вычисляет хэш-проверки подлинности сообщения код (HMAC) с помощью <see cref="T:System.Security.Cryptography.SHA256" /> хеш-функции.
  /// </summary>
  [ComVisible(true)]
  public class HMACSHA256 : HMAC
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.HMACSHA256" /> класса случайный ключ.
    /// </summary>
    public HMACSHA256()
      : this(Utils.GenerateRandom(64))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.HMACSHA256" /> указанными данными ключа.
    /// </summary>
    /// <param name="key">
    ///   Секретный ключ для шифрования <see cref="T:System.Security.Cryptography.HMACSHA256" />.
    ///    Значением ключа может быть любой длины.
    ///    Тем не менее рекомендуемый размер — 64 байта.
    ///    Если ключ длиной более 64 байт, он хэшируется (с использованием SHA-256) для получения 64-байтового ключа.
    ///    Если это не более 64 байта, он дополняется до 64 байт.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    public HMACSHA256(byte[] key)
    {
      this.m_hashName = "SHA256";
      Func<HashAlgorithm> func1 = (Func<HashAlgorithm>) (() => (HashAlgorithm) new SHA256Managed());
      Func<HashAlgorithm> createStandardHashAlgorithmCallback1;
      this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(createStandardHashAlgorithmCallback1, (Func<HashAlgorithm>) (() => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider")));
      Func<HashAlgorithm> func2 = (Func<HashAlgorithm>) (() => (HashAlgorithm) new SHA256Managed());
      Func<HashAlgorithm> createStandardHashAlgorithmCallback2;
      this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(createStandardHashAlgorithmCallback2, (Func<HashAlgorithm>) (() => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider")));
      this.HashSizeValue = 256;
      this.InitializeKey(key);
    }
  }
}
