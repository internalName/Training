// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HMACSHA512
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   На основе хэша вычисляет код проверки подлинности сообщения (HMAC) с помощью <see cref="T:System.Security.Cryptography.SHA512" /> хеш-функции.
  /// </summary>
  [ComVisible(true)]
  public class HMACSHA512 : HMAC
  {
    private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.HMACSHA512" /> класса случайный ключ.
    /// </summary>
    public HMACSHA512()
      : this(Utils.GenerateRandom(128))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.HMACSHA512" /> указанными данными ключа.
    /// </summary>
    /// <param name="key">
    ///   Секретный ключ для шифрования <see cref="T:System.Security.Cryptography.HMACSHA512" />.
    ///    Ключ может быть любой длины.
    ///    Однако рекомендуемый размер — 128 байт.
    ///    Если ключ не более 128 байт, он хэшируется (используя SHA-512) для формирования 128-разрядный ключ.
    ///    Если это менее 128 байт, он дополняется до 128 байт.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public HMACSHA512(byte[] key)
    {
      this.m_hashName = "SHA512";
      Func<HashAlgorithm> func1 = (Func<HashAlgorithm>) (() => (HashAlgorithm) new SHA512Managed());
      Func<HashAlgorithm> createStandardHashAlgorithmCallback1;
      this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(createStandardHashAlgorithmCallback1, (Func<HashAlgorithm>) (() => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider")));
      Func<HashAlgorithm> func2 = (Func<HashAlgorithm>) (() => (HashAlgorithm) new SHA512Managed());
      Func<HashAlgorithm> createStandardHashAlgorithmCallback2;
      this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(createStandardHashAlgorithmCallback2, (Func<HashAlgorithm>) (() => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider")));
      this.HashSizeValue = 512;
      this.BlockSizeValue = this.BlockSize;
      this.InitializeKey(key);
    }

    private int BlockSize
    {
      get
      {
        return !this.m_useLegacyBlockSize ? 128 : 64;
      }
    }

    /// <summary>
    ///   Предоставляет решение для .NET Framework 2.0 реализация <see cref="T:System.Security.Cryptography.HMACSHA512" /> алгоритма, который не соответствует .NET Framework 2.0 с пакетом обновления 1 (SP1) реализации.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Чтобы включить .NET Framework 2.0 с пакетом обновления 1 (SP1) приложениям взаимодействовать с .NET Framework 2.0 приложений; в противном случае — <see langword="false" />.
    /// </returns>
    public bool ProduceLegacyHmacValues
    {
      get
      {
        return this.m_useLegacyBlockSize;
      }
      set
      {
        this.m_useLegacyBlockSize = value;
        this.BlockSizeValue = this.BlockSize;
        this.InitializeKey(this.KeyValue);
      }
    }
  }
}
