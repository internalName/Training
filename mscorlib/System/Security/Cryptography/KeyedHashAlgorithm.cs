// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.KeyedHashAlgorithm
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет абстрактный класс, из которого создаются все реализации хэш-алгоритмов с ключом.
  /// </summary>
  [ComVisible(true)]
  public abstract class KeyedHashAlgorithm : HashAlgorithm
  {
    /// <summary>Ключ, используемый в хэш-алгоритме.</summary>
    protected byte[] KeyValue;

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.KeyValue != null)
          Array.Clear((Array) this.KeyValue, 0, this.KeyValue.Length);
        this.KeyValue = (byte[]) null;
      }
      base.Dispose(disposing);
    }

    /// <summary>
    ///   Возвращает или задает ключ, используемый в хэш-алгоритме.
    /// </summary>
    /// <returns>Ключ, используемый в хэш-алгоритме.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Попытка изменить <see cref="P:System.Security.Cryptography.KeyedHashAlgorithm.Key" /> свойство после начала хеширования.
    /// </exception>
    public virtual byte[] Key
    {
      get
      {
        return (byte[]) this.KeyValue.Clone();
      }
      set
      {
        if (this.State != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_HashKeySet"));
        this.KeyValue = (byte[]) value.Clone();
      }
    }

    /// <summary>
    ///   Создает экземпляр реализации по умолчанию хэш-алгоритма с ключом.
    /// </summary>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.Security.Cryptography.HMACSHA1" />, если параметры по умолчанию не изменены.
    /// </returns>
    public static KeyedHashAlgorithm Create()
    {
      return KeyedHashAlgorithm.Create("System.Security.Cryptography.KeyedHashAlgorithm");
    }

    /// <summary>
    ///   Создает экземпляр заданной реализации хэш-алгоритма с ключом.
    /// </summary>
    /// <param name="algName">
    /// Реализация хэш-алгоритма с ключом, которую требуется использовать.
    ///  В следующей таблице представлены допустимые значения параметра <paramref name="algName" /> и алгоритмы, с которыми они сопоставляются.
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
    /// <returns>Новый экземпляр заданного хэш-алгоритма с ключом.</returns>
    public static KeyedHashAlgorithm Create(string algName)
    {
      return (KeyedHashAlgorithm) CryptoConfig.CreateFromName(algName);
    }
  }
}
