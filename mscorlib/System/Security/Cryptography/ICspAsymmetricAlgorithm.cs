// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.ICspAsymmetricAlgorithm
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Определяет методы, позволяющие классу <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> перечислять данные о контейнерах ключей и импортировать или экспортировать BLOB-объекты ключей, совместимые с API шифрования (Майкрософт) (CAPI).
  /// </summary>
  [ComVisible(true)]
  public interface ICspAsymmetricAlgorithm
  {
    /// <summary>
    ///   Возвращает объект <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" />, описывающий дополнительные сведения о паре ключей шифрования.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" />, описывающий дополнительные сведения о паре ключей шифрования.
    /// </returns>
    CspKeyContainerInfo CspKeyContainerInfo { get; }

    /// <summary>
    ///   Экспортирует большой двоичный объект, содержащий ключевые сведения, связанные с объектом <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />.
    /// </summary>
    /// <param name="includePrivateParameters">
    ///   <see langword="true" />, чтобы включить закрытый ключ; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Массив байтов, содержащий ключевые сведения, связанные с объектом <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />.
    /// </returns>
    byte[] ExportCspBlob(bool includePrivateParameters);

    /// <summary>
    ///   Импортирует большой двоичный объект, представляющий данные асимметричного ключа.
    /// </summary>
    /// <param name="rawData">
    ///   Массив байтов, представляющий большой двоичный объект асимметричного ключа.
    /// </param>
    void ImportCspBlob(byte[] rawData);
  }
}
