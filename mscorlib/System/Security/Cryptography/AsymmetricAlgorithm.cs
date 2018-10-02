// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.AsymmetricAlgorithm
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет абстрактный базовый класс, от которого должны наследоваться все реализации алгоритмов асимметричного шифрования.
  /// </summary>
  [ComVisible(true)]
  public abstract class AsymmetricAlgorithm : IDisposable
  {
    /// <summary>
    ///   Представляет размер модуля ключа (в битах), используемого алгоритмом асимметричного шифрования.
    /// </summary>
    protected int KeySizeValue;
    /// <summary>
    ///   Задает размеры ключа, которые поддерживаются алгоритмом асимметричного шифрования.
    /// </summary>
    protected KeySizes[] LegalKeySizesValue;

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />.
    /// </summary>
    public void Dispose()
    {
      this.Clear();
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые классом <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />.
    /// </summary>
    public void Clear()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые классом <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> (при необходимости освобождает и управляемые ресурсы).
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>
    ///   Получает или задает размер модуля ключа (в битах), используемого алгоритмом асимметричного шифрования.
    /// </summary>
    /// <returns>
    ///   Размер модуля ключа (в битах), используемого алгоритмом асимметричного шифрования.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый размер модуля ключа.
    /// </exception>
    public virtual int KeySize
    {
      get
      {
        return this.KeySizeValue;
      }
      set
      {
        for (int index = 0; index < this.LegalKeySizesValue.Length; ++index)
        {
          if (this.LegalKeySizesValue[index].SkipSize == 0)
          {
            if (this.LegalKeySizesValue[index].MinSize == value)
            {
              this.KeySizeValue = value;
              return;
            }
          }
          else
          {
            int minSize = this.LegalKeySizesValue[index].MinSize;
            while (minSize <= this.LegalKeySizesValue[index].MaxSize)
            {
              if (minSize == value)
              {
                this.KeySizeValue = value;
                return;
              }
              minSize += this.LegalKeySizesValue[index].SkipSize;
            }
          }
        }
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
      }
    }

    /// <summary>
    ///   Возвращает размеры ключа, которые поддерживаются алгоритмом асимметричного шифрования.
    /// </summary>
    /// <returns>
    ///   Массив, в котором содержатся размеры ключа, поддерживаемые алгоритмом асимметричного шифрования.
    /// </returns>
    public virtual KeySizes[] LegalKeySizes
    {
      get
      {
        return (KeySizes[]) this.LegalKeySizesValue.Clone();
      }
    }

    /// <summary>
    ///   При реализации в производном классе возвращает имя алгоритма подписи.
    ///    В противном случае всегда создается исключение <see cref="T:System.NotImplementedException" />.
    /// </summary>
    /// <returns>Имя алгоритма подписи.</returns>
    public virtual string SignatureAlgorithm
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает имя алгоритма обмена ключами.
    ///    В противном случае создается исключение <see cref="T:System.NotImplementedException" />.
    /// </summary>
    /// <returns>Имя алгоритма обмена ключами.</returns>
    public virtual string KeyExchangeAlgorithm
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Создает криптографический объект по умолчанию, используемый для выполнения алгоритма асимметричного шифрования.
    /// </summary>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" />, если параметры по умолчанию не были изменены с помощью элемента &lt;cryptoClass&gt;.
    /// </returns>
    public static AsymmetricAlgorithm Create()
    {
      return AsymmetricAlgorithm.Create("System.Security.Cryptography.AsymmetricAlgorithm");
    }

    /// <summary>
    ///   Создает экземпляр заданной реализации алгоритма асимметричного шифрования.
    /// </summary>
    /// <param name="algName">
    /// Реализация асимметричного алгоритма, которую требуется использовать.
    ///  В следующей таблице представлены допустимые значения параметра <paramref name="algName" /> и алгоритмы, с которыми они сопоставляются.
    /// 
    ///         Значение параметра
    /// 
    ///         Инструменты
    /// 
    ///         System.Security.Cryptography.AsymmetricAlgorithm
    /// 
    ///         <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />
    /// 
    ///         RSA
    /// 
    ///         <see cref="T:System.Security.Cryptography.RSA" />
    /// 
    ///         System.Security.Cryptography.RSA
    /// 
    ///         <see cref="T:System.Security.Cryptography.RSA" />
    /// 
    ///         DSA
    /// 
    ///         <see cref="T:System.Security.Cryptography.DSA" />
    /// 
    ///         System.Security.Cryptography.DSA
    /// 
    ///         <see cref="T:System.Security.Cryptography.DSA" />
    /// 
    ///         ECDsa
    /// 
    ///         <see cref="T:System.Security.Cryptography.ECDsa" />
    /// 
    ///         ECDsaCng
    /// 
    ///         <see cref="T:System.Security.Cryptography.ECDsaCng" />
    /// 
    ///         System.Security.Cryptography.ECDsaCng
    /// 
    ///         <see cref="T:System.Security.Cryptography.ECDsaCng" />
    /// 
    ///         ECDH
    /// 
    ///         <see cref="T:System.Security.Cryptography.ECDiffieHellman" />
    /// 
    ///         ECDiffieHellman
    /// 
    ///         <see cref="T:System.Security.Cryptography.ECDiffieHellman" />
    /// 
    ///         ECDiffieHellmanCng
    /// 
    ///         <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" />
    /// 
    ///         System.Security.Cryptography.ECDiffieHellmanCng
    /// 
    ///         <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" />
    ///       </param>
    /// <returns>
    ///   Новый экземпляр заданной реализации алгоритма асимметричного шифрования.
    /// </returns>
    public static AsymmetricAlgorithm Create(string algName)
    {
      return (AsymmetricAlgorithm) CryptoConfig.CreateFromName(algName);
    }

    /// <summary>
    ///   Если переопределено в производном классе, восстанавливает объект <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> из XML-строки.
    ///    В противном случае создается исключение <see cref="T:System.NotImplementedException" />.
    /// </summary>
    /// <param name="xmlString">
    ///   XML-строка, используемая для восстановления объекта <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />.
    /// </param>
    public virtual void FromXmlString(string xmlString)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Если переопределено в производном классе, создает и возвращает представление текущего объекта <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> в виде XML-строки.
    ///    В противном случае создается исключение <see cref="T:System.NotImplementedException" />.
    /// </summary>
    /// <param name="includePrivateParameters">
    ///   Значение <see langword="true" /> для включения закрытых параметров; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Кодировка XML-строки текущего объекта <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" />.
    /// </returns>
    public virtual string ToXmlString(bool includePrivateParameters)
    {
      throw new NotImplementedException();
    }
  }
}
