// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSA
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет базовый класс, от которого наследуются все реализации алгоритма <see cref="T:System.Security.Cryptography.RSA" />.
  /// </summary>
  [ComVisible(true)]
  public abstract class RSA : AsymmetricAlgorithm
  {
    /// <summary>
    ///   Создает экземпляр реализации алгоритма <see cref="T:System.Security.Cryptography.RSA" /> по умолчанию.
    /// </summary>
    /// <returns>
    ///   Новый экземпляр реализации объекта <see cref="T:System.Security.Cryptography.RSA" /> по умолчанию.
    /// </returns>
    public static RSA Create()
    {
      return RSA.Create("System.Security.Cryptography.RSA");
    }

    /// <summary>
    ///   Создает экземпляр заданной реализации класса <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    /// <param name="algName">
    ///   Имя используемой реализации <see cref="T:System.Security.Cryptography.RSA" />.
    /// </param>
    /// <returns>
    ///   Новый экземпляр заданной реализации класса <see cref="T:System.Security.Cryptography.RSA" />.
    /// </returns>
    public static RSA Create(string algName)
    {
      return (RSA) CryptoConfig.CreateFromName(algName);
    }

    public static RSA Create(int keySizeInBits)
    {
      RSA fromName = (RSA) CryptoConfig.CreateFromName("RSAPSS");
      fromName.KeySize = keySizeInBits;
      if (fromName.KeySize != keySizeInBits)
        throw new CryptographicException();
      return fromName;
    }

    public static RSA Create(RSAParameters parameters)
    {
      RSA fromName = (RSA) CryptoConfig.CreateFromName("RSAPSS");
      fromName.ImportParameters(parameters);
      return fromName;
    }

    /// <summary>
    ///   При переопределении в производном классе выполняет шифрование входных данных с использованием указанного режима заполнения.
    /// </summary>
    /// <param name="data">Данные, которые необходимо зашифровать.</param>
    /// <param name="padding">Режим заполнения.</param>
    /// <returns>Зашифрованные данные.</returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Производный класс должен переопределять этот метод.
    /// </exception>
    public virtual byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
    {
      throw RSA.DerivedClassMustOverride();
    }

    /// <summary>
    ///   При переопределении в производном классе выполняет расшифровку входных данных с использованием указанного режима заполнения.
    /// </summary>
    /// <param name="data">Расшифровываемые данные.</param>
    /// <param name="padding">Режим заполнения.</param>
    /// <returns>Расшифрованные данные.</returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Производный класс должен переопределять этот метод.
    /// </exception>
    public virtual byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
    {
      throw RSA.DerivedClassMustOverride();
    }

    /// <summary>
    ///   При переопределении в производном классе вычисляет подпись для указанного хэш-значения путем его шифрования с закрытым ключом с использованием указанного заполнения.
    /// </summary>
    /// <param name="hash">Хэш-значение подписываемых данных.</param>
    /// <param name="hashAlgorithm">
    ///   Хэш-алгоритм, используемый для создания хэш-значения данных.
    /// </param>
    /// <param name="padding">Заполнение.</param>
    /// <returns>Подпись RSA для указанного хэш-значения.</returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Производный класс должен переопределять этот метод.
    /// </exception>
    public virtual byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      throw RSA.DerivedClassMustOverride();
    }

    /// <summary>
    ///   Проверяет допустимость цифровой подписи путем определения хэш-значения в этой подписи с помощью указанного хэш-алгоритма и заполнения, сравнивая его с предоставленным хэш-значением.
    /// </summary>
    /// <param name="hash">Хэш-значение подписанных данных.</param>
    /// <param name="signature">
    ///   Данные подписи, которые требуется поверить.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Хэш-алгоритм, используемый для создания хэш-значения.
    /// </param>
    /// <param name="padding">Режим заполнения.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если подпись является допустимой; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Производный класс должен переопределять этот метод.
    /// </exception>
    public virtual bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      throw RSA.DerivedClassMustOverride();
    }

    /// <summary>
    ///   При переопределении в производном классе вычисляет хэш-значение указанного фрагмента массива байтов с помощью заданного хэш-алгоритма.
    /// </summary>
    /// <param name="data">
    ///   Данные, предназначенные для хэширования.
    /// </param>
    /// <param name="offset">
    ///   Индекс первого байта в <paramref name="data" />, хэширование которого требуется выполнить.
    /// </param>
    /// <param name="count">Количество байтов для хэширования.</param>
    /// <param name="hashAlgorithm">
    ///   Алгоритм, который должен использоваться при хэшировании данных.
    /// </param>
    /// <returns>Хэшированные данные.</returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Производный класс должен переопределять этот метод.
    /// </exception>
    protected virtual byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
    {
      throw RSA.DerivedClassMustOverride();
    }

    /// <summary>
    ///   При переопределении в производном классе вычисляет хэш-значение указанного двоичного потока с помощью заданного алгоритма хэширования.
    /// </summary>
    /// <param name="data">
    ///   Двоичный поток, хэширование которого требуется выполнить.
    /// </param>
    /// <param name="hashAlgorithm">Хэш-алгоритм.</param>
    /// <returns>Хэшированные данные.</returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Производный класс должен переопределять этот метод.
    /// </exception>
    protected virtual byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
    {
      throw RSA.DerivedClassMustOverride();
    }

    /// <summary>
    ///   Вычисляет хэш-значение заданного массива байтов с помощью указанного алгоритма хэширования и режима заполнения, а затем подписывает полученное хэш-значение.
    /// </summary>
    /// <param name="data">
    ///   Входные данные, для которых нужно вычислить хэш.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Хэш-алгоритм, который следует использовать для создания хэш-значения.
    /// </param>
    /// <param name="padding">Режим заполнения.</param>
    /// <returns>Подпись RSA для указанных данных.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="padding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /><paramref name="hashAlgorithm" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    public byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      return this.SignData(data, 0, data.Length, hashAlgorithm, padding);
    }

    /// <summary>
    ///   Вычисляет хэш-значение части заданного массива байтов с помощью указанного алгоритма хэширования и режима заполнения, а затем подписывает полученное хэш-значение.
    /// </summary>
    /// <param name="data">
    ///   Входные данные, для которых нужно вычислить хэш.
    /// </param>
    /// <param name="offset">
    ///   Смещение в массиве, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="count">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Хэш-алгоритм, который следует использовать для создания хэш-значения.
    /// </param>
    /// <param name="padding">Режим заполнения.</param>
    /// <returns>Подпись RSA для указанных данных.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="padding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /><paramref name="hashAlgorithm" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="offset" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset" /> + <paramref name="count" /> – 1 приводит к получению значения индекса, который выходит за пределы верхней границы <paramref name="data" />.
    /// </exception>
    public virtual byte[] SignData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      if (offset < 0 || offset > data.Length)
        throw new ArgumentOutOfRangeException(nameof (offset));
      if (count < 0 || count > data.Length - offset)
        throw new ArgumentOutOfRangeException(nameof (count));
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw RSA.HashAlgorithmNameNullOrEmpty();
      if (padding == (RSASignaturePadding) null)
        throw new ArgumentNullException(nameof (padding));
      return this.SignHash(this.HashData(data, offset, count, hashAlgorithm), hashAlgorithm, padding);
    }

    /// <summary>
    ///   Вычисляет хэш-значение заданного потока с помощью указанного алгоритма хэширования и режима заполнения, а затем подписывает полученное хэш-значение.
    /// </summary>
    /// <param name="data">
    ///   Входной поток, для которого нужно вычислить хэш.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Хэш-алгоритм, который следует использовать для создания хэш-значения.
    /// </param>
    /// <param name="padding">Режим заполнения.</param>
    /// <returns>Подпись RSA для указанных данных.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="padding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /><paramref name="hashAlgorithm" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    public virtual byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw RSA.HashAlgorithmNameNullOrEmpty();
      if (padding == (RSASignaturePadding) null)
        throw new ArgumentNullException(nameof (padding));
      return this.SignHash(this.HashData(data, hashAlgorithm), hashAlgorithm, padding);
    }

    /// <summary>
    ///   Проверяет допустимость цифровой подписи, вычисляя хэш-значение указанных данных с помощью указанного алгоритма хэширования и заполнения, а затем сравнивая его с предоставленной подписью.
    /// </summary>
    /// <param name="data">Подписанные данные.</param>
    /// <param name="signature">
    ///   Данные подписи, которые требуется поверить.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Хэш-алгоритм, используемый для создания хэш-значения данных.
    /// </param>
    /// <param name="padding">Режим заполнения.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если подпись является допустимой; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="signature" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="padding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /><paramref name="hashAlgorithm" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      return this.VerifyData(data, 0, data.Length, signature, hashAlgorithm, padding);
    }

    /// <summary>
    ///   Проверяет допустимость цифровой подписи, вычисляя хэш-значение данных во фрагменте массива байтов с помощью указанного алгоритма хэширования и заполнения и сравнивая его с предоставленной подписью.
    /// </summary>
    /// <param name="data">Подписанные данные.</param>
    /// <param name="offset">
    ///   Индекс, начиная с которого нужно вычислять хэш.
    /// </param>
    /// <param name="count">Количество байтов для хэширования.</param>
    /// <param name="signature">
    ///   Данные подписи, которые требуется поверить.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Хэш-алгоритм, используемый для создания хэш-значения данных.
    /// </param>
    /// <param name="padding">Режим заполнения.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если подпись является допустимой; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="signature" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="padding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /><paramref name="hashAlgorithm" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="offset" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset" /> + <paramref name="count" /> – 1 приводит к получению значения индекса, который выходит за пределы верхней границы <paramref name="data" />.
    /// </exception>
    public virtual bool VerifyData(byte[] data, int offset, int count, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      if (offset < 0 || offset > data.Length)
        throw new ArgumentOutOfRangeException(nameof (offset));
      if (count < 0 || count > data.Length - offset)
        throw new ArgumentOutOfRangeException(nameof (count));
      if (signature == null)
        throw new ArgumentNullException(nameof (signature));
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw RSA.HashAlgorithmNameNullOrEmpty();
      if (padding == (RSASignaturePadding) null)
        throw new ArgumentNullException(nameof (padding));
      return this.VerifyHash(this.HashData(data, offset, count, hashAlgorithm), signature, hashAlgorithm, padding);
    }

    /// <summary>
    ///   Проверяет допустимость цифровой подписи, вычисляя хэш-значение указанного потока с помощью указанного алгоритма хэширования и заполнения, а затем сравнивая его с предоставленной подписью.
    /// </summary>
    /// <param name="data">Подписанные данные.</param>
    /// <param name="signature">
    ///   Данные подписи, которые требуется поверить.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Хэш-алгоритм, используемый для создания хэш-значения данных.
    /// </param>
    /// <param name="padding">Режим заполнения.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если подпись является допустимой; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="signature" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="padding" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /><paramref name="hashAlgorithm" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    public bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      if (signature == null)
        throw new ArgumentNullException(nameof (signature));
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw RSA.HashAlgorithmNameNullOrEmpty();
      if (padding == (RSASignaturePadding) null)
        throw new ArgumentNullException(nameof (padding));
      return this.VerifyHash(this.HashData(data, hashAlgorithm), signature, hashAlgorithm, padding);
    }

    private static Exception DerivedClassMustOverride()
    {
      return (Exception) new NotImplementedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    internal static Exception HashAlgorithmNameNullOrEmpty()
    {
      return (Exception) new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
    }

    /// <summary>
    ///   Если переопределено в производном классе, расшифровывает входные данные с помощью закрытого ключа.
    /// </summary>
    /// <param name="rgb">
    ///   Зашифрованный текст, который необходимо расшифровать.
    /// </param>
    /// <returns>
    ///   Результат расшифровки значения параметра <paramref name="rgb" /> в форме обычного текста.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот вызов метода не поддерживается.
    ///    Это исключение выдается, начиная с .NET Framework 4.6.
    /// </exception>
    public virtual byte[] DecryptValue(byte[] rgb)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    /// <summary>
    ///   Если переопределено в производном классе, зашифровывает входные данные с помощью открытого ключа.
    /// </summary>
    /// <param name="rgb">
    ///   Обычный текст, который требуется зашифровать.
    /// </param>
    /// <returns>
    ///   Результат шифрования значения параметра <paramref name="rgb" /> в форме зашифрованного текста.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот вызов метода не поддерживается.
    ///    Это исключение выдается, начиная с .NET Framework 4.6.
    /// </exception>
    public virtual byte[] EncryptValue(byte[] rgb)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    /// <summary>
    ///   Получает имя алгоритма обмена ключами, доступного в этой реализации <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    /// <returns>Возвращает RSA.</returns>
    public override string KeyExchangeAlgorithm
    {
      get
      {
        return nameof (RSA);
      }
    }

    /// <summary>
    ///   Получает имя алгоритма подписи, доступного в этой реализации <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    /// <returns>Возвращает RSA.</returns>
    public override string SignatureAlgorithm
    {
      get
      {
        return nameof (RSA);
      }
    }

    /// <summary>
    ///   Инициализирует объект <see cref="T:System.Security.Cryptography.RSA" />, используя данные ключа из строки XML.
    /// </summary>
    /// <param name="xmlString">
    ///   Строка XML, содержащая данные ключа <see cref="T:System.Security.Cryptography.RSA" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="xmlString" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый формат параметра <paramref name="xmlString" />.
    /// </exception>
    public override void FromXmlString(string xmlString)
    {
      if (xmlString == null)
        throw new ArgumentNullException(nameof (xmlString));
      RSAParameters parameters = new RSAParameters();
      SecurityElement topElement = new Parser(xmlString).GetTopElement();
      string inputBuffer1 = topElement.SearchForTextOfLocalName("Modulus");
      if (inputBuffer1 == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) nameof (RSA), (object) "Modulus"));
      parameters.Modulus = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer1));
      string inputBuffer2 = topElement.SearchForTextOfLocalName("Exponent");
      if (inputBuffer2 == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) nameof (RSA), (object) "Exponent"));
      parameters.Exponent = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer2));
      string inputBuffer3 = topElement.SearchForTextOfLocalName("P");
      if (inputBuffer3 != null)
        parameters.P = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer3));
      string inputBuffer4 = topElement.SearchForTextOfLocalName("Q");
      if (inputBuffer4 != null)
        parameters.Q = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer4));
      string inputBuffer5 = topElement.SearchForTextOfLocalName("DP");
      if (inputBuffer5 != null)
        parameters.DP = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer5));
      string inputBuffer6 = topElement.SearchForTextOfLocalName("DQ");
      if (inputBuffer6 != null)
        parameters.DQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer6));
      string inputBuffer7 = topElement.SearchForTextOfLocalName("InverseQ");
      if (inputBuffer7 != null)
        parameters.InverseQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer7));
      string inputBuffer8 = topElement.SearchForTextOfLocalName("D");
      if (inputBuffer8 != null)
        parameters.D = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer8));
      this.ImportParameters(parameters);
    }

    /// <summary>
    ///   Создает и возвращает строку XML, содержащую ключ текущего объекта <see cref="T:System.Security.Cryptography.RSA" />.
    /// </summary>
    /// <param name="includePrivateParameters">
    ///   Значение <see langword="true" />, чтобы включать закрытый и открытый ключ RSA; значение <see langword="false" />, чтобы включать только открытый ключ.
    /// </param>
    /// <returns>
    ///   Строка XML, содержащая ключ текущего объекта <see cref="T:System.Security.Cryptography.RSA" />.
    /// </returns>
    public override string ToXmlString(bool includePrivateParameters)
    {
      RSAParameters rsaParameters = this.ExportParameters(includePrivateParameters);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<RSAKeyValue>");
      stringBuilder.Append("<Modulus>" + Convert.ToBase64String(rsaParameters.Modulus) + "</Modulus>");
      stringBuilder.Append("<Exponent>" + Convert.ToBase64String(rsaParameters.Exponent) + "</Exponent>");
      if (includePrivateParameters)
      {
        stringBuilder.Append("<P>" + Convert.ToBase64String(rsaParameters.P) + "</P>");
        stringBuilder.Append("<Q>" + Convert.ToBase64String(rsaParameters.Q) + "</Q>");
        stringBuilder.Append("<DP>" + Convert.ToBase64String(rsaParameters.DP) + "</DP>");
        stringBuilder.Append("<DQ>" + Convert.ToBase64String(rsaParameters.DQ) + "</DQ>");
        stringBuilder.Append("<InverseQ>" + Convert.ToBase64String(rsaParameters.InverseQ) + "</InverseQ>");
        stringBuilder.Append("<D>" + Convert.ToBase64String(rsaParameters.D) + "</D>");
      }
      stringBuilder.Append("</RSAKeyValue>");
      return stringBuilder.ToString();
    }

    /// <summary>
    ///   Если переопределено в производном классе, экспортирует объект <see cref="T:System.Security.Cryptography.RSAParameters" />.
    /// </summary>
    /// <param name="includePrivateParameters">
    ///   Значение <see langword="true" /> для включения закрытых параметров; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Параметры для <see cref="T:System.Security.Cryptography.DSA" />.
    /// </returns>
    public abstract RSAParameters ExportParameters(bool includePrivateParameters);

    /// <summary>
    ///   Если переопределено в производном классе, импортирует заданный объект <see cref="T:System.Security.Cryptography.RSAParameters" />.
    /// </summary>
    /// <param name="parameters">
    ///   Параметры для <see cref="T:System.Security.Cryptography.RSA" />.
    /// </param>
    public abstract void ImportParameters(RSAParameters parameters);
  }
}
