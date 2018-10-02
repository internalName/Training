// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.AsymmetricSignatureFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет базовый класс, от которого наследуются все реализации асимметричного форматирования подписи.
  /// </summary>
  [ComVisible(true)]
  public abstract class AsymmetricSignatureFormatter
  {
    /// <summary>
    ///   При переопределении в производном классе задает асимметричный алгоритм, используемый для создания подписи.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр реализации <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />, используемый для создания подписи.
    /// </param>
    public abstract void SetKey(AsymmetricAlgorithm key);

    /// <summary>
    ///   При переопределении в производном классе задает хэш-алгоритм, используемый для создания подписи.
    /// </summary>
    /// <param name="strName">
    ///   Имя хэш-алгоритма для создания подписи.
    /// </param>
    public abstract void SetHashAlgorithm(string strName);

    /// <summary>Создает подпись из указанного хэш-значения.</summary>
    /// <param name="hash">
    ///   Хэш-алгоритм, который следует использовать для создания подписи.
    /// </param>
    /// <returns>Подпись для указанного хэш-значения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="hash" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual byte[] CreateSignature(HashAlgorithm hash)
    {
      if (hash == null)
        throw new ArgumentNullException(nameof (hash));
      this.SetHashAlgorithm(hash.ToString());
      return this.CreateSignature(hash.Hash);
    }

    /// <summary>
    ///   При переопределении в производном классе создает подпись для указанных данных.
    /// </summary>
    /// <param name="rgbHash">Данные, предназначенные для подписи.</param>
    /// <returns>
    ///   Цифровая подпись для параметра <paramref name="rgbHash" />.
    /// </returns>
    public abstract byte[] CreateSignature(byte[] rgbHash);
  }
}
