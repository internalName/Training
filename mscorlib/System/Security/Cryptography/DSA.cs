// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DSA
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
  ///   Представляет абстрактный базовый класс, от которого должны наследоваться все реализации алгоритма цифровой подписи (<see cref="T:System.Security.Cryptography.DSA" />).
  /// </summary>
  [ComVisible(true)]
  public abstract class DSA : AsymmetricAlgorithm
  {
    /// <summary>
    ///   Создает криптографический объект по умолчанию, используемый для выполнения алгоритма асимметричного шифрования.
    /// </summary>
    /// <returns>
    ///   Криптографический объект, используемый для выполнения алгоритма асимметричного шифрования.
    /// </returns>
    public static DSA Create()
    {
      return DSA.Create("System.Security.Cryptography.DSA");
    }

    /// <summary>
    ///   Создает заданный криптографический объект, используемый для выполнения алгоритма асимметричного шифрования.
    /// </summary>
    /// <param name="algName">
    ///   Имя конкретной реализации <see cref="T:System.Security.Cryptography.DSA" /> для использования.
    /// </param>
    /// <returns>
    ///   Криптографический объект, используемый для выполнения алгоритма асимметричного шифрования.
    /// </returns>
    public static DSA Create(string algName)
    {
      return (DSA) CryptoConfig.CreateFromName(algName);
    }

    public static DSA Create(int keySizeInBits)
    {
      DSA fromName = (DSA) CryptoConfig.CreateFromName("DSA-FIPS186-3");
      fromName.KeySize = keySizeInBits;
      if (fromName.KeySize != keySizeInBits)
        throw new CryptographicException();
      return fromName;
    }

    public static DSA Create(DSAParameters parameters)
    {
      DSA fromName = (DSA) CryptoConfig.CreateFromName("DSA-FIPS186-3");
      fromName.ImportParameters(parameters);
      return fromName;
    }

    /// <summary>
    ///   При переопределении в производном классе создает <see cref="T:System.Security.Cryptography.DSA" /> подпись для указанных данных.
    /// </summary>
    /// <param name="rgbHash">
    ///   Данные, которые должны быть подписаны.
    /// </param>
    /// <returns>Цифровая подпись для указанных данных.</returns>
    public abstract byte[] CreateSignature(byte[] rgbHash);

    /// <summary>
    ///   При переопределении в производном классе проверяет <see cref="T:System.Security.Cryptography.DSA" /> подпись для указанных данных.
    /// </summary>
    /// <param name="rgbHash">
    ///   Хэш данных подписан <paramref name="rgbSignature" />.
    /// </param>
    /// <param name="rgbSignature">
    ///   Подпись для <paramref name="rgbData" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="rgbSignature" /> совпадает с подписью, вычисленной с помощью указанного хэш-алгоритма и ключа для <paramref name="rgbHash" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);

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
    ///   Алгоритм, который будет использоваться для хэширования данных.
    /// </param>
    /// <returns>Хэшированные данные.</returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Производный класс должен переопределять этот метод.
    /// </exception>
    protected virtual byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
    {
      throw DSA.DerivedClassMustOverride();
    }

    /// <summary>
    ///   При переопределении в производном классе вычисляет хэш-значение указанного двоичного потока с помощью заданного алгоритма хэширования.
    /// </summary>
    /// <param name="data">
    ///   Двоичный поток, хэширование которого требуется выполнить.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Алгоритм, который будет использоваться для хэширования данных.
    /// </param>
    /// <returns>Хэшированные данные.</returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Производный класс должен переопределять этот метод.
    /// </exception>
    protected virtual byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
    {
      throw DSA.DerivedClassMustOverride();
    }

    /// <summary>
    ///   Вычисляет хэш-значение заданного массива байтов с помощью указанного алгоритма хэширования и подписывает результирующее хэш-значение.
    /// </summary>
    /// <param name="data">
    ///   Входные данные, для которых нужно вычислить хэш.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Хэш-алгоритм, который следует использовать для создания хэш-значения.
    /// </param>
    /// <returns>Подпись DSA для указанных данных.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /><paramref name="hashAlgorithm" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    public byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      return this.SignData(data, 0, data.Length, hashAlgorithm);
    }

    /// <summary>
    ///   Вычисляет хэш-значение фрагмента заданного массива байтов с помощью указанного алгоритма хэширования и подписывает результирующее хэш-значение.
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
    /// <returns>Подпись DSA для указанных данных.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
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
    public virtual byte[] SignData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      if (offset < 0 || offset > data.Length)
        throw new ArgumentOutOfRangeException(nameof (offset));
      if (count < 0 || count > data.Length - offset)
        throw new ArgumentOutOfRangeException(nameof (count));
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw DSA.HashAlgorithmNameNullOrEmpty();
      return this.CreateSignature(this.HashData(data, offset, count, hashAlgorithm));
    }

    /// <summary>
    ///   Вычисляет хэш-значение заданного потока с помощью указанного алгоритма хэширования и подписывает результирующее хэш-значение.
    /// </summary>
    /// <param name="data">
    ///   Входной поток, для которого нужно вычислить хэш.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Хэш-алгоритм, который следует использовать для создания хэш-значения.
    /// </param>
    /// <returns>Подпись DSA для указанных данных.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /><paramref name="hashAlgorithm" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    public virtual byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw DSA.HashAlgorithmNameNullOrEmpty();
      return this.CreateSignature(this.HashData(data, hashAlgorithm));
    }

    /// <summary>
    ///   Проверяет допустимость цифровой подписи путем вычисления хэш-значения заданных данных с помощью указанного алгоритма хэширования и его сравнения с предоставленной подписью.
    /// </summary>
    /// <param name="data">Подписанные данные.</param>
    /// <param name="signature">
    ///   Данные подписи, которые требуется поверить.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Алгоритм хэширования, используемый для создания хэш-значения данных.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если цифровая подпись является допустимой. В противном случае значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="signature" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /><paramref name="hashAlgorithm" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      return this.VerifyData(data, 0, data.Length, signature, hashAlgorithm);
    }

    /// <summary>
    ///   Проверяет допустимость цифровой подписи путем вычисления хэш-значения данных во фрагменте массива байтов с помощью указанного алгоритма хэширования и его сравнения с предоставленной подписью.
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
    ///   Алгоритм хэширования, используемый для создания хэш-значения данных.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если цифровая подпись является допустимой. В противном случае значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="signature" /> имеет значение <see langword="null" />.
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
    public virtual bool VerifyData(byte[] data, int offset, int count, byte[] signature, HashAlgorithmName hashAlgorithm)
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
        throw DSA.HashAlgorithmNameNullOrEmpty();
      return this.VerifySignature(this.HashData(data, offset, count, hashAlgorithm), signature);
    }

    /// <summary>
    ///   Проверяет допустимость цифровой подписи путем вычисления хэш-значения заданного потока с помощью указанного алгоритма хэширования и его сравнения с предоставленной подписью.
    /// </summary>
    /// <param name="data">Подписанные данные.</param>
    /// <param name="signature">
    ///   Данные подписи, которые требуется поверить.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Алгоритм хэширования, используемый для создания хэш-значения данных.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если цифровая подпись является допустимой. В противном случае значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="signature" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /><paramref name="hashAlgorithm" /> имеет значение <see langword="null" /> или <see cref="F:System.String.Empty" />.
    /// </exception>
    public virtual bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      if (signature == null)
        throw new ArgumentNullException(nameof (signature));
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw DSA.HashAlgorithmNameNullOrEmpty();
      return this.VerifySignature(this.HashData(data, hashAlgorithm), signature);
    }

    /// <summary>
    ///   Восстанавливает <see cref="T:System.Security.Cryptography.DSA" /> объекта из XML-строки.
    /// </summary>
    /// <param name="xmlString">
    ///   XML-строка, используемая для восстановления объекта <see cref="T:System.Security.Cryptography.DSA" />.
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
      DSAParameters parameters = new DSAParameters();
      SecurityElement topElement = new Parser(xmlString).GetTopElement();
      string inputBuffer1 = topElement.SearchForTextOfLocalName("P");
      if (inputBuffer1 == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) nameof (DSA), (object) "P"));
      parameters.P = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer1));
      string inputBuffer2 = topElement.SearchForTextOfLocalName("Q");
      if (inputBuffer2 == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) nameof (DSA), (object) "Q"));
      parameters.Q = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer2));
      string inputBuffer3 = topElement.SearchForTextOfLocalName("G");
      if (inputBuffer3 == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) nameof (DSA), (object) "G"));
      parameters.G = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer3));
      string inputBuffer4 = topElement.SearchForTextOfLocalName("Y");
      if (inputBuffer4 == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) nameof (DSA), (object) "Y"));
      parameters.Y = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer4));
      string inputBuffer5 = topElement.SearchForTextOfLocalName("J");
      if (inputBuffer5 != null)
        parameters.J = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer5));
      string inputBuffer6 = topElement.SearchForTextOfLocalName("X");
      if (inputBuffer6 != null)
        parameters.X = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer6));
      string inputBuffer7 = topElement.SearchForTextOfLocalName("Seed");
      string inputBuffer8 = topElement.SearchForTextOfLocalName("PgenCounter");
      if (inputBuffer7 != null && inputBuffer8 != null)
      {
        parameters.Seed = Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer7));
        parameters.Counter = Utils.ConvertByteArrayToInt(Convert.FromBase64String(Utils.DiscardWhiteSpaces(inputBuffer8)));
      }
      else if (inputBuffer7 != null || inputBuffer8 != null)
      {
        if (inputBuffer7 == null)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) nameof (DSA), (object) "Seed"));
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", (object) nameof (DSA), (object) "PgenCounter"));
      }
      this.ImportParameters(parameters);
    }

    /// <summary>
    ///   Создает и возвращает представление XML-строки текущего <see cref="T:System.Security.Cryptography.DSA" /> объекта.
    /// </summary>
    /// <param name="includePrivateParameters">
    ///   Значение <see langword="true" /> для включения закрытых параметров; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Кодировка XML-строки текущего объекта <see cref="T:System.Security.Cryptography.DSA" />.
    /// </returns>
    public override string ToXmlString(bool includePrivateParameters)
    {
      DSAParameters dsaParameters = this.ExportParameters(includePrivateParameters);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<DSAKeyValue>");
      stringBuilder.Append("<P>" + Convert.ToBase64String(dsaParameters.P) + "</P>");
      stringBuilder.Append("<Q>" + Convert.ToBase64String(dsaParameters.Q) + "</Q>");
      stringBuilder.Append("<G>" + Convert.ToBase64String(dsaParameters.G) + "</G>");
      stringBuilder.Append("<Y>" + Convert.ToBase64String(dsaParameters.Y) + "</Y>");
      if (dsaParameters.J != null)
        stringBuilder.Append("<J>" + Convert.ToBase64String(dsaParameters.J) + "</J>");
      if (dsaParameters.Seed != null)
      {
        stringBuilder.Append("<Seed>" + Convert.ToBase64String(dsaParameters.Seed) + "</Seed>");
        stringBuilder.Append("<PgenCounter>" + Convert.ToBase64String(Utils.ConvertIntToByteArray(dsaParameters.Counter)) + "</PgenCounter>");
      }
      if (includePrivateParameters)
        stringBuilder.Append("<X>" + Convert.ToBase64String(dsaParameters.X) + "</X>");
      stringBuilder.Append("</DSAKeyValue>");
      return stringBuilder.ToString();
    }

    /// <summary>
    ///   Если переопределено в производном классе, экспортирует объект <see cref="T:System.Security.Cryptography.DSAParameters" />.
    /// </summary>
    /// <param name="includePrivateParameters">
    ///   Значение <see langword="true" /> для включения закрытых параметров; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Параметры для <see cref="T:System.Security.Cryptography.DSA" />.
    /// </returns>
    public abstract DSAParameters ExportParameters(bool includePrivateParameters);

    /// <summary>
    ///   Если переопределено в производном классе, импортирует заданный объект <see cref="T:System.Security.Cryptography.DSAParameters" />.
    /// </summary>
    /// <param name="parameters">
    ///   Параметры для <see cref="T:System.Security.Cryptography.DSA" />.
    /// </param>
    public abstract void ImportParameters(DSAParameters parameters);

    private static Exception DerivedClassMustOverride()
    {
      return (Exception) new NotImplementedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    internal static Exception HashAlgorithmNameNullOrEmpty()
    {
      return (Exception) new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
    }
  }
}
