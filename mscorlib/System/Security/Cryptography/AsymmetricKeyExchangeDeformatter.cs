// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.AsymmetricKeyExchangeDeformatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет базовый класс, от которого наследуются все модули удаления форматирования асимметричного обмена ключами.
  /// </summary>
  [ComVisible(true)]
  public abstract class AsymmetricKeyExchangeDeformatter
  {
    /// <summary>
    ///   При переопределении в производном классе получает или задает параметры для обмена асимметричными ключами.
    /// </summary>
    /// <returns>
    ///   XML-строка, содержащая параметры операции обмена асимметричными ключами.
    /// </returns>
    public abstract string Parameters { get; set; }

    /// <summary>
    ///   Если переопределено в производном классе, задает закрытый ключ для расшифровки конфиденциальных данных.
    /// </summary>
    /// <param name="key">
    ///   Экземпляр реализации <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />, который содержит закрытый ключ.
    /// </param>
    public abstract void SetKey(AsymmetricAlgorithm key);

    /// <summary>
    ///   При переопределении в производном классе извлекает конфиденциальные сведения из зашифрованных данных обмена ключами.
    /// </summary>
    /// <param name="rgb">
    ///   Данные обмена ключами, в которых скрыты конфиденциальные сведения.
    /// </param>
    /// <returns>
    ///   Конфиденциальные сведения, извлекаемые из данных обмена ключами.
    /// </returns>
    public abstract byte[] DecryptKeyExchange(byte[] rgb);
  }
}
