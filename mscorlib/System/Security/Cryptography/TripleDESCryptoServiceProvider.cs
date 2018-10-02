// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.TripleDESCryptoServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Определяет объект обертки для доступа к версии служб шифрования (CSP) <see cref="T:System.Security.Cryptography.TripleDES" /> алгоритма.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class TripleDESCryptoServiceProvider : TripleDES
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.TripleDESCryptoServiceProvider" />.
    /// </summary>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   <see cref="T:System.Security.Cryptography.TripleDES" /> Поставщик служб шифрования недоступен.
    /// </exception>
    [SecuritySafeCritical]
    public TripleDESCryptoServiceProvider()
    {
      if (!Utils.HasAlgorithm(26115, 0))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_AlgorithmNotAvailable"));
      this.FeedbackSizeValue = 8;
    }

    /// <summary>
    ///   Создает симметричный <see cref="T:System.Security.Cryptography.TripleDES" /> объект-шифратор с заданным ключом (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) и вектором инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).
    /// </summary>
    /// <param name="rgbKey">
    ///   Секретный ключ, который должен использоваться для симметричного алгоритма.
    /// </param>
    /// <param name="rgbIV">
    /// Вектор инициализации, который должен использоваться для симметричного алгоритма.
    /// 
    ///   Вектор инициализации должен быть длиной 8 байт.
    ///    Если превышает 8 байт, оно усекается и исключение не создается.
    ///    Перед вызовом метода <see cref="M:System.Security.Cryptography.TripleDESCryptoServiceProvider.CreateEncryptor(System.Byte[],System.Byte[])" />, проверьте длину вектора инициализации и создания исключения, если она слишком велика.
    /// </param>
    /// <returns>
    ///   Симметричный <see cref="T:System.Security.Cryptography.TripleDES" /> объект-шифратор.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Значение свойства <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> равно <see cref="F:System.Security.Cryptography.CipherMode.OFB" />.
    /// 
    ///   -или-
    /// 
    ///   Значение <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> свойство <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> и значение <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize" /> свойство не 8.
    /// 
    ///   -или-
    /// 
    ///   Использован недопустимый размер ключа.
    /// 
    ///   -или-
    /// 
    ///   Размер ключа алгоритма была недоступна.
    /// </exception>
    [SecuritySafeCritical]
    public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
    {
      if (TripleDES.IsWeakKey(rgbKey))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), "TripleDES");
      return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, CryptoAPITransformMode.Encrypt);
    }

    /// <summary>
    ///   Создает симметричный <see cref="T:System.Security.Cryptography.TripleDES" /> объект-дешифратор с указанным ключом (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) и вектором инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).
    /// </summary>
    /// <param name="rgbKey">
    ///   Секретный ключ, который должен использоваться для симметричного алгоритма.
    /// </param>
    /// <param name="rgbIV">
    ///   Вектор инициализации, который должен использоваться для симметричного алгоритма.
    /// </param>
    /// <returns>
    ///   Симметричный <see cref="T:System.Security.Cryptography.TripleDES" /> объект-дешифратор.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Значение свойства <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> равно <see cref="F:System.Security.Cryptography.CipherMode.OFB" />.
    /// 
    ///   -или-
    /// 
    ///   Значение <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> свойство <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> и значение <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize" /> свойство не 8.
    /// 
    ///   -или-
    /// 
    ///   Использован недопустимый размер ключа.
    /// 
    ///   -или-
    /// 
    ///   Размер ключа алгоритма была недоступна.
    /// </exception>
    [SecuritySafeCritical]
    public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
    {
      if (TripleDES.IsWeakKey(rgbKey))
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), "TripleDES");
      return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, CryptoAPITransformMode.Decrypt);
    }

    /// <summary>
    ///   Создает случайный <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> для алгоритма.
    /// </summary>
    public override void GenerateKey()
    {
      this.KeyValue = new byte[this.KeySizeValue / 8];
      Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
      while (TripleDES.IsWeakKey(this.KeyValue))
        Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
    }

    /// <summary>
    ///   Создает случайный вектор инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) для использования данным алгоритмом.
    /// </summary>
    public override void GenerateIV()
    {
      this.IVValue = new byte[8];
      Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
    }

    [SecurityCritical]
    private ICryptoTransform _NewEncryptor(byte[] rgbKey, CipherMode mode, byte[] rgbIV, int feedbackSize, CryptoAPITransformMode encryptMode)
    {
      int cArgs = 0;
      int[] rgArgIds = new int[10];
      object[] rgArgValues = new object[10];
      int algid = 26115;
      if (mode == CipherMode.OFB)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_OFBNotSupported"));
      if (mode == CipherMode.CFB && feedbackSize != 8)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_CFBSizeNotSupported"));
      if (rgbKey == null)
      {
        rgbKey = new byte[this.KeySizeValue / 8];
        Utils.StaticRandomNumberGenerator.GetBytes(rgbKey);
      }
      if (mode != CipherMode.CBC)
      {
        rgArgIds[cArgs] = 4;
        rgArgValues[cArgs] = (object) mode;
        ++cArgs;
      }
      if (mode != CipherMode.ECB)
      {
        if (rgbIV == null)
        {
          rgbIV = new byte[8];
          Utils.StaticRandomNumberGenerator.GetBytes(rgbIV);
        }
        if (rgbIV.Length < 8)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidIVSize"));
        rgArgIds[cArgs] = 1;
        rgArgValues[cArgs] = (object) rgbIV;
        ++cArgs;
      }
      if (mode == CipherMode.OFB || mode == CipherMode.CFB)
      {
        rgArgIds[cArgs] = 5;
        rgArgValues[cArgs] = (object) feedbackSize;
        ++cArgs;
      }
      if (rgbKey.Length == 16)
        algid = 26121;
      return (ICryptoTransform) new CryptoAPITransform(algid, cArgs, rgArgIds, rgArgValues, rgbKey, this.PaddingValue, mode, this.BlockSizeValue, feedbackSize, false, encryptMode);
    }
  }
}
