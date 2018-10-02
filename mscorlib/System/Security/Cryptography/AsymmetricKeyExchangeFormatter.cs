// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.AsymmetricKeyExchangeFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет базовый класс, от которого наследуют все модули форматирования асимметричного обмена ключами.
  /// </summary>
  [ComVisible(true)]
  public abstract class AsymmetricKeyExchangeFormatter
  {
    /// <summary>
    ///   При переопределении в производном классе получает параметры для обмена асимметричными ключами.
    /// </summary>
    /// <returns>
    ///   XML-строка, содержащая параметры операции обмена асимметричными ключами.
    /// </returns>
    public abstract string Parameters { get; }

    /// <summary>
    ///   При переопределении в производном классе задает открытый ключ, используемый для шифрования секретных данных.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр реализации <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />, который содержит открытый ключ.
    /// </param>
    public abstract void SetKey(AsymmetricAlgorithm key);

    /// <summary>
    ///   При переопределении в производном классе создает зашифрованные данные обмена ключами из указанных входных данных.
    /// </summary>
    /// <param name="data">
    ///   Секретные сведения, которые будут переданы при обмене ключами.
    /// </param>
    /// <returns>
    ///   Зашифрованные данные для обмена ключами, отправляемые указанному получателю.
    /// </returns>
    public abstract byte[] CreateKeyExchange(byte[] data);

    /// <summary>
    ///   При переопределении в производном классе создает зашифрованные данные обмена ключами из указанных входных данных.
    /// </summary>
    /// <param name="data">
    ///   Секретные сведения, которые будут переданы при обмене ключами.
    /// </param>
    /// <param name="symAlgType">
    ///   Этот параметр не используется в текущей версии.
    /// </param>
    /// <returns>
    ///   Зашифрованные данные для обмена ключами, отправляемые указанному получателю.
    /// </returns>
    public abstract byte[] CreateKeyExchange(byte[] data, Type symAlgType);
  }
}
