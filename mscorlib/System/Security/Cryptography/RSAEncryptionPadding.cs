// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAEncryptionPadding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Задает режим заполнения и параметры для использования с операциями шифрования или расшифровки RSA.
  /// </summary>
  public sealed class RSAEncryptionPadding : IEquatable<RSAEncryptionPadding>
  {
    private static readonly RSAEncryptionPadding s_pkcs1 = new RSAEncryptionPadding(RSAEncryptionPaddingMode.Pkcs1, new HashAlgorithmName());
    private static readonly RSAEncryptionPadding s_oaepSHA1 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA1);
    private static readonly RSAEncryptionPadding s_oaepSHA256 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA256);
    private static readonly RSAEncryptionPadding s_oaepSHA384 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA384);
    private static readonly RSAEncryptionPadding s_oaepSHA512 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA512);
    private RSAEncryptionPaddingMode _mode;
    private HashAlgorithmName _oaepHashAlgorithm;

    /// <summary>
    ///   Получает объект, представляющий стандарт шифрования PKCS #1.
    /// </summary>
    /// <returns>Объект, представляющий стандарт шифрования PKCS #1.</returns>
    public static RSAEncryptionPadding Pkcs1
    {
      get
      {
        return RSAEncryptionPadding.s_pkcs1;
      }
    }

    /// <summary>
    ///   Получает объект, представляющий стандарт шифрования OAEP с хэш-алгоритмом SHA1.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий стандарт шифрования OAEP с хэш-алгоритмом SHA1.
    /// </returns>
    public static RSAEncryptionPadding OaepSHA1
    {
      get
      {
        return RSAEncryptionPadding.s_oaepSHA1;
      }
    }

    /// <summary>
    ///   Получает объект, представляющий стандарт шифрования OAEP с хэш-алгоритмом SHA256.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий стандарт шифрования OAEP с хэш-алгоритмом SHA256.
    /// </returns>
    public static RSAEncryptionPadding OaepSHA256
    {
      get
      {
        return RSAEncryptionPadding.s_oaepSHA256;
      }
    }

    /// <summary>
    ///   Получает объект, представляющий стандарт шифрования OAEP с хэш-алгоритмом SHA-384.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий стандарт шифрования OAEP с хэш-алгоритмом SHA384.
    /// </returns>
    public static RSAEncryptionPadding OaepSHA384
    {
      get
      {
        return RSAEncryptionPadding.s_oaepSHA384;
      }
    }

    /// <summary>
    ///   Получает объект, представляющий стандарт шифрования OAEP с хэш-алгоритмом SHA512.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий стандарт шифрования OAEP с хэш-алгоритмом SHA512.
    /// </returns>
    public static RSAEncryptionPadding OaepSHA512
    {
      get
      {
        return RSAEncryptionPadding.s_oaepSHA512;
      }
    }

    private RSAEncryptionPadding(RSAEncryptionPaddingMode mode, HashAlgorithmName oaepHashAlgorithm)
    {
      this._mode = mode;
      this._oaepHashAlgorithm = oaepHashAlgorithm;
    }

    /// <summary>
    ///   Создает новый экземпляр <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" />, у которого <see cref="P:System.Security.Cryptography.RSAEncryptionPadding.Mode" /> — <see cref="F:System.Security.Cryptography.RSAEncryptionPaddingMode.Oaep" /> с указанным хэш-алгоритмом.
    /// </summary>
    /// <param name="hashAlgorithm">Хэш-алгоритм.</param>
    /// <returns>
    ///   Объект, чей режим <see cref="P:System.Security.Cryptography.RSAEncryptionPadding.Mode" /> — <see cref="F:System.Security.Cryptography.RSAEncryptionPaddingMode.Oaep" /> с алгоритмом хеширования, заданным <paramref name="hashAlgorithm" />.
    ///    .
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /><paramref name="hashAlgorithm" /> — <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    public static RSAEncryptionPadding CreateOaep(HashAlgorithmName hashAlgorithm)
    {
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), nameof (hashAlgorithm));
      return new RSAEncryptionPadding(RSAEncryptionPaddingMode.Oaep, hashAlgorithm);
    }

    /// <summary>
    ///   Получает режим заполнения, представленный этим экземпляром <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" />.
    /// </summary>
    /// <returns>Режим заполнения.</returns>
    public RSAEncryptionPaddingMode Mode
    {
      get
      {
        return this._mode;
      }
    }

    /// <summary>
    ///   Получает хэш-алгоритм, используемый в сочетании с режимом заполнения <see cref="F:System.Security.Cryptography.RSAEncryptionPaddingMode.Oaep" />.
    ///    Если значение свойства <see cref="P:System.Security.Cryptography.RSAEncryptionPadding.Mode" /> не равно <see cref="F:System.Security.Cryptography.RSAEncryptionPaddingMode.Oaep" />, то <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> имеет значение <see langword="null" />.
    /// </summary>
    /// <returns>Хэш-алгоритм.</returns>
    public HashAlgorithmName OaepHashAlgorithm
    {
      get
      {
        return this._oaepHashAlgorithm;
      }
    }

    /// <summary>
    ///   Возвращает хэш-код данного объекта <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" />.
    /// </summary>
    /// <returns>Хэш-код данного экземпляра.</returns>
    public override int GetHashCode()
    {
      return RSAEncryptionPadding.CombineHashCodes(this._mode.GetHashCode(), this._oaepHashAlgorithm.GetHashCode());
    }

    private static int CombineHashCodes(int h1, int h2)
    {
      return (h1 << 5) + h1 ^ h2;
    }

    /// <summary>
    ///   Определяет, равен ли текущий экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">Объект для сравнения.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="obj" /> равен текущему экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      return this.Equals(obj as RSAEncryptionPadding);
    }

    /// <summary>
    ///   Определяет, равен ли текущий экземпляр указанному объекту <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" />.
    /// </summary>
    /// <param name="other">Объект для сравнения.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="other" /> равен текущему экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool Equals(RSAEncryptionPadding other)
    {
      if (other != (RSAEncryptionPadding) null && this._mode == other._mode)
        return this._oaepHashAlgorithm == other._oaepHashAlgorithm;
      return false;
    }

    /// <summary>
    ///   Указывает, равны ли значения двух заданных объектов <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <see langword="left" /> и <see langword="right" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator ==(RSAEncryptionPadding left, RSAEncryptionPadding right)
    {
      if ((object) left == null)
        return (object) right == null;
      return left.Equals(right);
    }

    /// <summary>
    ///   Указывает, равны ли значения двух заданных объектов <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <see langword="left" /> и <see langword="right" /> не равны друг другу; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator !=(RSAEncryptionPadding left, RSAEncryptionPadding right)
    {
      return !(left == right);
    }

    /// <summary>
    ///   Возвращает строковое представление текущего экземпляра <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" />.
    /// </summary>
    /// <returns>Строковое представление текущего объекта.</returns>
    public override string ToString()
    {
      return this._mode.ToString() + this._oaepHashAlgorithm.Name;
    }
  }
}
