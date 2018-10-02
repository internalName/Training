// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RijndaelManaged
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Получает доступ к управляемой версии <see cref="T:System.Security.Cryptography.Rijndael" /> алгоритма.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class RijndaelManaged : Rijndael
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RijndaelManaged" />.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот класс не соответствует стандарту FIPS.
    /// </exception>
    public RijndaelManaged()
    {
      if (CryptoConfig.AllowOnlyFipsAlgorithms)
        throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
    }

    /// <summary>
    ///   Создает симметричный <see cref="T:System.Security.Cryptography.Rijndael" /> объект-шифратор с заданным <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> и вектором инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).
    /// </summary>
    /// <param name="rgbKey">
    ///   Секретный ключ для симметричного алгоритма.
    ///    Размер ключа должен быть 128, 192 и 256 бит.
    /// </param>
    /// <param name="rgbIV">
    ///   Вектор Инициализации, используемый в симметричном алгоритме.
    /// </param>
    /// <returns>
    ///   Симметричный <see cref="T:System.Security.Cryptography.Rijndael" /> объект-шифратор.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rgbKey" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="rgbIV" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Значение <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> свойство не <see cref="F:System.Security.Cryptography.CipherMode.ECB" />, <see cref="F:System.Security.Cryptography.CipherMode.CBC" />, или <see cref="F:System.Security.Cryptography.CipherMode.CFB" />.
    /// </exception>
    public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
    {
      return this.NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, RijndaelManagedTransformMode.Encrypt);
    }

    /// <summary>
    ///   Создает симметричный <see cref="T:System.Security.Cryptography.Rijndael" /> объект-дешифратор с указанным <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> и вектором инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).
    /// </summary>
    /// <param name="rgbKey">
    ///   Секретный ключ для симметричного алгоритма.
    ///    Размер ключа должен быть 128, 192 и 256 бит.
    /// </param>
    /// <param name="rgbIV">
    ///   Вектор Инициализации, используемый в симметричном алгоритме.
    /// </param>
    /// <returns>
    ///   Симметричный <see cref="T:System.Security.Cryptography.Rijndael" /> объект-дешифратор.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rgbKey" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="rgbIV" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Значение <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> свойство не <see cref="F:System.Security.Cryptography.CipherMode.ECB" />, <see cref="F:System.Security.Cryptography.CipherMode.CBC" />, или <see cref="F:System.Security.Cryptography.CipherMode.CFB" />.
    /// </exception>
    public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
    {
      return this.NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, RijndaelManagedTransformMode.Decrypt);
    }

    /// <summary>
    ///   Создает случайный <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> для алгоритма.
    /// </summary>
    public override void GenerateKey()
    {
      this.KeyValue = Utils.GenerateRandom(this.KeySizeValue / 8);
    }

    /// <summary>
    ///   Создает случайный вектор инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) для алгоритма.
    /// </summary>
    public override void GenerateIV()
    {
      this.IVValue = Utils.GenerateRandom(this.BlockSizeValue / 8);
    }

    private ICryptoTransform NewEncryptor(byte[] rgbKey, CipherMode mode, byte[] rgbIV, int feedbackSize, RijndaelManagedTransformMode encryptMode)
    {
      if (rgbKey == null)
        rgbKey = Utils.GenerateRandom(this.KeySizeValue / 8);
      if (rgbIV == null)
        rgbIV = Utils.GenerateRandom(this.BlockSizeValue / 8);
      return (ICryptoTransform) new RijndaelManagedTransform(rgbKey, mode, rgbIV, this.BlockSizeValue, feedbackSize, this.PaddingValue, encryptMode);
    }
  }
}
