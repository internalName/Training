// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HMAC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет абстрактный класс, от которого должны наследоваться все реализации хэш-кода проверки подлинности сообщения (HMAC).
  /// </summary>
  [ComVisible(true)]
  public abstract class HMAC : KeyedHashAlgorithm
  {
    private int blockSizeValue = 64;
    internal string m_hashName;
    internal HashAlgorithm m_hash1;
    internal HashAlgorithm m_hash2;
    private byte[] m_inner;
    private byte[] m_outer;
    private bool m_hashing;

    /// <summary>
    ///   Возвращает или задает размер блока, используемый в хэш-значении.
    /// </summary>
    /// <returns>Размер блока, используемый в хэш значении.</returns>
    protected int BlockSizeValue
    {
      get
      {
        return this.blockSizeValue;
      }
      set
      {
        this.blockSizeValue = value;
      }
    }

    private void UpdateIOPadBuffers()
    {
      if (this.m_inner == null)
        this.m_inner = new byte[this.BlockSizeValue];
      if (this.m_outer == null)
        this.m_outer = new byte[this.BlockSizeValue];
      for (int index = 0; index < this.BlockSizeValue; ++index)
      {
        this.m_inner[index] = (byte) 54;
        this.m_outer[index] = (byte) 92;
      }
      for (int index = 0; index < this.KeyValue.Length; ++index)
      {
        this.m_inner[index] ^= this.KeyValue[index];
        this.m_outer[index] ^= this.KeyValue[index];
      }
    }

    internal void InitializeKey(byte[] key)
    {
      this.m_inner = (byte[]) null;
      this.m_outer = (byte[]) null;
      if (key.Length > this.BlockSizeValue)
        this.KeyValue = this.m_hash1.ComputeHash(key);
      else
        this.KeyValue = (byte[]) key.Clone();
      this.UpdateIOPadBuffers();
    }

    /// <summary>
    ///   Возвращает или задает ключ, используемый в хэш-алгоритме.
    /// </summary>
    /// <returns>Ключ, используемый в хэш-алгоритме.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Попытка изменить <see cref="P:System.Security.Cryptography.HMAC.Key" /> свойство после начала хеширования.
    /// </exception>
    public override byte[] Key
    {
      get
      {
        return (byte[]) this.KeyValue.Clone();
      }
      set
      {
        if (this.m_hashing)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_HashKeySet"));
        this.InitializeKey(value);
      }
    }

    /// <summary>
    ///   Возвращает или задает имя используемого хэш-алгоритма.
    /// </summary>
    /// <returns>Имя хэш-алгоритма.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Невозможно изменить текущий хэш-алгоритм.
    /// </exception>
    public string HashName
    {
      get
      {
        return this.m_hashName;
      }
      set
      {
        if (this.m_hashing)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_HashNameSet"));
        this.m_hashName = value;
        this.m_hash1 = HashAlgorithm.Create(this.m_hashName);
        this.m_hash2 = HashAlgorithm.Create(this.m_hashName);
      }
    }

    /// <summary>
    ///   Создает экземпляр реализации хэш-кода проверки подлинности сообщения (HMAC) по умолчанию.
    /// </summary>
    /// <returns>
    ///   Новый экземпляр SHA-1, если параметры по умолчанию не были изменены с помощью элемента &lt;cryptoClass&gt;.
    /// </returns>
    public static HMAC Create()
    {
      return HMAC.Create("System.Security.Cryptography.HMAC");
    }

    /// <summary>
    ///   Создает экземпляр заданной реализации хэш-кода проверки подлинности сообщения (HMAC).
    /// </summary>
    /// <param name="algorithmName">
    /// Реализация кода HMAC для использования.
    ///  В следующей таблице представлены допустимые значения параметра <paramref name="algorithmName" /> и алгоритмы, с которыми они сопоставляются.
    /// 
    ///         Значение параметра
    /// 
    ///         Инструменты
    /// 
    ///         System.Security.Cryptography.HMAC
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACSHA1" />
    /// 
    ///         System.Security.Cryptography.KeyedHashAlgorithm
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACSHA1" />
    /// 
    ///         HMACMD5
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACMD5" />
    /// 
    ///         System.Security.Cryptography.HMACMD5
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACMD5" />
    /// 
    ///         HMACRIPEMD160
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACRIPEMD160" />
    /// 
    ///         System.Security.Cryptography.HMACRIPEMD160
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACRIPEMD160" />
    /// 
    ///         HMAC-SHA1
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACSHA1" />
    /// 
    ///         System.Security.Cryptography.HMACSHA1
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACSHA1" />
    /// 
    ///         HMACSHA256
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACSHA256" />
    /// 
    ///         System.Security.Cryptography.HMACSHA256
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACSHA256" />
    /// 
    ///         HMACSHA384
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACSHA384" />
    /// 
    ///         System.Security.Cryptography.HMACSHA384
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACSHA384" />
    /// 
    ///         HMACSHA512
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACSHA512" />
    /// 
    ///         System.Security.Cryptography.HMACSHA512
    /// 
    ///         <see cref="T:System.Security.Cryptography.HMACSHA512" />
    /// 
    ///         MACTripleDES
    /// 
    ///         <see cref="T:System.Security.Cryptography.MACTripleDES" />
    /// 
    ///         System.Security.Cryptography.MACTripleDES
    /// 
    ///         <see cref="T:System.Security.Cryptography.MACTripleDES" />
    ///       </param>
    /// <returns>Новый экземпляр заданной реализации кода HMAC.</returns>
    public static HMAC Create(string algorithmName)
    {
      return (HMAC) CryptoConfig.CreateFromName(algorithmName);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр реализации по умолчанию класса <see cref="T:System.Security.Cryptography.HMAC" />.
    /// </summary>
    public override void Initialize()
    {
      this.m_hash1.Initialize();
      this.m_hash2.Initialize();
      this.m_hashing = false;
    }

    /// <summary>
    ///   Если переопределено в производном классе, передает данные, записанные в объект, на вход хэш-алгоритма <see cref="T:System.Security.Cryptography.HMAC" /> по умолчанию для вычисления значения хэша.
    /// </summary>
    /// <param name="rgb">Входные данные.</param>
    /// <param name="ib">
    ///   Смещение в массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="cb">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    protected override void HashCore(byte[] rgb, int ib, int cb)
    {
      if (!this.m_hashing)
      {
        this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
        this.m_hashing = true;
      }
      this.m_hash1.TransformBlock(rgb, ib, cb, rgb, ib);
    }

    /// <summary>
    ///   Если переопределено в производном классе, завершает вычисление хэша после обработки последних данных криптографическим потоковым объектом.
    /// </summary>
    /// <returns>Вычисленный хэш-код в массиве байтов.</returns>
    protected override byte[] HashFinal()
    {
      if (!this.m_hashing)
      {
        this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
        this.m_hashing = true;
      }
      this.m_hash1.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
      byte[] hashValue = this.m_hash1.HashValue;
      this.m_hash2.TransformBlock(this.m_outer, 0, this.m_outer.Length, this.m_outer, 0);
      this.m_hash2.TransformBlock(hashValue, 0, hashValue.Length, hashValue, 0);
      this.m_hashing = false;
      this.m_hash2.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
      return this.m_hash2.HashValue;
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.Security.Cryptography.HMAC" />, и, если допускается изменение ключа, опционально освобождает управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.m_hash1 != null)
          this.m_hash1.Dispose();
        if (this.m_hash2 != null)
          this.m_hash2.Dispose();
        if (this.m_inner != null)
          Array.Clear((Array) this.m_inner, 0, this.m_inner.Length);
        if (this.m_outer != null)
          Array.Clear((Array) this.m_outer, 0, this.m_outer.Length);
      }
      base.Dispose(disposing);
    }

    internal static HashAlgorithm GetHashAlgorithmWithFipsFallback(Func<HashAlgorithm> createStandardHashAlgorithmCallback, Func<HashAlgorithm> createFipsHashAlgorithmCallback)
    {
      if (!CryptoConfig.AllowOnlyFipsAlgorithms)
        return createStandardHashAlgorithmCallback();
      try
      {
        return createFipsHashAlgorithmCallback();
      }
      catch (PlatformNotSupportedException ex)
      {
        throw new InvalidOperationException(ex.Message, (Exception) ex);
      }
    }
  }
}
