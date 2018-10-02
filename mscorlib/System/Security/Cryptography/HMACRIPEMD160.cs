// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HMACRIPEMD160
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Вычисляет хэш-сообщения проверки подлинности кода (HMAC) с помощью <see cref="T:System.Security.Cryptography.RIPEMD160" /> хеш-функции.
  /// </summary>
  [ComVisible(true)]
  public class HMACRIPEMD160 : HMAC
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.HMACRIPEMD160" /> класса генерируется случайным образом 64-разрядный ключ.
    /// </summary>
    public HMACRIPEMD160()
      : this(Utils.GenerateRandom(64))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.HMACRIPEMD160" /> указанными данными ключа.
    /// </summary>
    /// <param name="key">
    ///   Секретный ключ для шифрования <see cref="T:System.Security.Cryptography.HMACRIPEMD160" />.
    ///    Ключ может быть любой длины, но он не более 64 байт хэшируется (используя SHA-1) для получения 64-байтового ключа.
    ///    Таким образом рекомендуемый размер секретного ключа — 64 байт.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="key" /> имеет значение <see langword="null" />.
    /// </exception>
    public HMACRIPEMD160(byte[] key)
    {
      this.m_hashName = "RIPEMD160";
      this.m_hash1 = (HashAlgorithm) new RIPEMD160Managed();
      this.m_hash2 = (HashAlgorithm) new RIPEMD160Managed();
      this.HashSizeValue = 160;
      this.InitializeKey(key);
    }
  }
}
