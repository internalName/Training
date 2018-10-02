// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.AsymmetricSignatureDeformatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет абстрактный базовый класс, от которого наследуются все реализации модуля асимметричного удаления формата подписи.
  /// </summary>
  [ComVisible(true)]
  public abstract class AsymmetricSignatureDeformatter
  {
    /// <summary>
    ///   При переопределении в производном классе задает открытый ключ для проверки подписи.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр реализации <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />, который содержит открытый ключ.
    /// </param>
    public abstract void SetKey(AsymmetricAlgorithm key);

    /// <summary>
    ///   Если переопределено в производном классе, задает хэш-алгоритм для подтверждения подписи.
    /// </summary>
    /// <param name="strName">
    ///   Имя хэш-алгоритма для подтверждения подписи.
    /// </param>
    public abstract void SetHashAlgorithm(string strName);

    /// <summary>Проверяет подпись из указанного хэш-значения.</summary>
    /// <param name="hash">
    ///   Хэш-алгоритм, который следует использовать для проверки подписи.
    /// </param>
    /// <param name="rgbSignature">Подпись для проверки.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если подпись допустима для хэша; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="hash" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual bool VerifySignature(HashAlgorithm hash, byte[] rgbSignature)
    {
      if (hash == null)
        throw new ArgumentNullException(nameof (hash));
      this.SetHashAlgorithm(hash.ToString());
      return this.VerifySignature(hash.Hash, rgbSignature);
    }

    /// <summary>
    ///   При переопределении в производном классе проверяет подпись с использованием указанных данных.
    /// </summary>
    /// <param name="rgbHash">
    ///   Данные, подписанные с помощью <paramref name="rgbSignature" />.
    /// </param>
    /// <param name="rgbSignature">
    ///   Подпись, которую требуется проверить с использованием <paramref name="rgbHash" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="rgbSignature" /> совпадает с подписью, вычисленной с помощью указанного хэш-алгоритма и ключа для <paramref name="rgbHash" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);
  }
}
