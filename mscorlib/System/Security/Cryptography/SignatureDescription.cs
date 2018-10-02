// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SignatureDescription
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>Содержит сведения о свойствах цифровой подписи.</summary>
  [ComVisible(true)]
  public class SignatureDescription
  {
    private string _strKey;
    private string _strDigest;
    private string _strFormatter;
    private string _strDeformatter;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.SignatureDescription" />.
    /// </summary>
    public SignatureDescription()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.SignatureDescription" /> из указанного потока <see cref="T:System.Security.SecurityElement" />.
    /// </summary>
    /// <param name="el">
    ///   <see cref="T:System.Security.SecurityElement" /> Из которого необходимо получить алгоритмы для описания подписи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="el" /> имеет значение <see langword="null" />.
    /// </exception>
    public SignatureDescription(SecurityElement el)
    {
      if (el == null)
        throw new ArgumentNullException(nameof (el));
      this._strKey = el.SearchForTextOfTag("Key");
      this._strDigest = el.SearchForTextOfTag("Digest");
      this._strFormatter = el.SearchForTextOfTag("Formatter");
      this._strDeformatter = el.SearchForTextOfTag("Deformatter");
    }

    /// <summary>
    ///   Возвращает или задает алгоритм с ключом для описания подписи.
    /// </summary>
    /// <returns>Алгоритм ключа для описания подписи.</returns>
    public string KeyAlgorithm
    {
      get
      {
        return this._strKey;
      }
      set
      {
        this._strKey = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает алгоритм дайджеста для описания подписи.
    /// </summary>
    /// <returns>Алгоритм дайджеста для описания подписи.</returns>
    public string DigestAlgorithm
    {
      get
      {
        return this._strDigest;
      }
      set
      {
        this._strDigest = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает алгоритм для описания подписи.
    /// </summary>
    /// <returns>
    ///   Алгоритм модуля форматирования для описания подписи.
    /// </returns>
    public string FormatterAlgorithm
    {
      get
      {
        return this._strFormatter;
      }
      set
      {
        this._strFormatter = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает алгоритм проверки подписи для описания подписи.
    /// </summary>
    /// <returns>Алгоритм проверки подписи для описания подписи.</returns>
    public string DeformatterAlgorithm
    {
      get
      {
        return this._strDeformatter;
      }
      set
      {
        this._strDeformatter = value;
      }
    }

    /// <summary>
    ///   Создает <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> экземпляра с использованием указанного ключа с помощью <see cref="P:System.Security.Cryptography.SignatureDescription.DeformatterAlgorithm" /> свойство.
    /// </summary>
    /// <param name="key">
    ///   Ключ, используемый в <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" />.
    /// </param>
    /// <returns>
    ///   Вновь созданный <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> экземпляра.
    /// </returns>
    public virtual AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
    {
      AsymmetricSignatureDeformatter fromName = (AsymmetricSignatureDeformatter) CryptoConfig.CreateFromName(this._strDeformatter);
      fromName.SetKey(key);
      return fromName;
    }

    /// <summary>
    ///   Создает <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> экземпляра с использованием указанного ключа с помощью <see cref="P:System.Security.Cryptography.SignatureDescription.FormatterAlgorithm" /> свойство.
    /// </summary>
    /// <param name="key">
    ///   Ключ, используемый в <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" />.
    /// </param>
    /// <returns>
    ///   Вновь созданный <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> экземпляра.
    /// </returns>
    public virtual AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
    {
      AsymmetricSignatureFormatter fromName = (AsymmetricSignatureFormatter) CryptoConfig.CreateFromName(this._strFormatter);
      fromName.SetKey(key);
      return fromName;
    }

    /// <summary>
    ///   Создает <see cref="T:System.Security.Cryptography.HashAlgorithm" /> экземпляра с помощью <see cref="P:System.Security.Cryptography.SignatureDescription.DigestAlgorithm" /> свойство.
    /// </summary>
    /// <returns>
    ///   Вновь созданный <see cref="T:System.Security.Cryptography.HashAlgorithm" /> экземпляра.
    /// </returns>
    public virtual HashAlgorithm CreateDigest()
    {
      return (HashAlgorithm) CryptoConfig.CreateFromName(this._strDigest);
    }
  }
}
