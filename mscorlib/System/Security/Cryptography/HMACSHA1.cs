// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HMACSHA1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Вычисляет хеш основанный код проверки подлинности сообщения (HMAC) с помощью <see cref="T:System.Security.Cryptography.SHA1" /> хеш-функции.
  /// </summary>
  [ComVisible(true)]
  public class HMACSHA1 : HMAC
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.HMACSHA1" /> класса случайный ключ.
    /// </summary>
    public HMACSHA1()
      : this(Utils.GenerateRandom(64))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.HMACSHA1" /> указанными данными ключа.
    /// </summary>
    /// <param name="key">
    ///   Секретный ключ для шифрования <see cref="T:System.Security.Cryptography.HMACSHA1" />.
    ///    Ключ может быть любой длины, но он не более 64 байт хэшируется (используя SHA-1) для получения 64-байтового ключа.
    ///    Таким образом рекомендуемый размер секретного ключа — 64 байт.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    public HMACSHA1(byte[] key)
      : this(key, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.HMACSHA1" /> с указанными данными ключа и значение, указывающее необходимость использования управляемой версии алгоритма SHA1.
    /// </summary>
    /// <param name="key">
    ///   Секретный ключ для шифрования <see cref="T:System.Security.Cryptography.HMACSHA1" />.
    ///    Ключ может быть любой длины, но если это более чем 64 байта, он хэшируется (используя SHA-1) для получения 64-байтового ключа.
    ///    Таким образом рекомендуемый размер секретного ключа — 64 байт.
    /// </param>
    /// <param name="useManagedSha1">
    ///   <see langword="true" /> использовать управляемую реализацию алгоритма SHA1 ( <see cref="T:System.Security.Cryptography.SHA1Managed" /> класса); <see langword="false" /> использовать неуправляемые реализацию ( <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />  класса).
    /// </param>
    public HMACSHA1(byte[] key, bool useManagedSha1)
    {
      this.m_hashName = "SHA1";
      if (useManagedSha1)
      {
        this.m_hash1 = (HashAlgorithm) new SHA1Managed();
        this.m_hash2 = (HashAlgorithm) new SHA1Managed();
      }
      else
      {
        this.m_hash1 = (HashAlgorithm) new SHA1CryptoServiceProvider();
        this.m_hash2 = (HashAlgorithm) new SHA1CryptoServiceProvider();
      }
      this.HashSizeValue = 160;
      this.InitializeKey(key);
    }
  }
}
