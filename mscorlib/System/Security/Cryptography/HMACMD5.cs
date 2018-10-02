// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HMACMD5
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Вычисляет хэш-сообщения проверки подлинности кода (HMAC) с помощью <see cref="T:System.Security.Cryptography.MD5" /> хеш-функции.
  /// </summary>
  [ComVisible(true)]
  public class HMACMD5 : HMAC
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.HMACMD5" /> используя случайный ключ.
    /// </summary>
    public HMACMD5()
      : this(Utils.GenerateRandom(64))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.HMACMD5" /> используя указанный ключ.
    /// </summary>
    /// <param name="key">
    ///   Секретный ключ для шифрования <see cref="T:System.Security.Cryptography.HMACMD5" />.
    ///    Ключ может быть любой длины, но если это более чем 64 байта хэшируется (используя SHA-1) для получения 64-байтового ключа.
    ///    Таким образом рекомендуемый размер секретного ключа — 64 байт.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    public HMACMD5(byte[] key)
    {
      this.m_hashName = "MD5";
      this.m_hash1 = (HashAlgorithm) new MD5CryptoServiceProvider();
      this.m_hash2 = (HashAlgorithm) new MD5CryptoServiceProvider();
      this.HashSizeValue = 128;
      this.InitializeKey(key);
    }
  }
}
