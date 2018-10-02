// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SymmetricAlgorithm
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет абстрактный базовый класс, от которого наследуются все реализации симметричных алгоритмов шифрования.
  /// </summary>
  [ComVisible(true)]
  public abstract class SymmetricAlgorithm : IDisposable
  {
    /// <summary>
    ///   Представляет размер блока криптографической операции (в битах).
    /// </summary>
    protected int BlockSizeValue;
    /// <summary>
    ///   Представляет размер ответа в криптографической операции (в битах).
    /// </summary>
    protected int FeedbackSizeValue;
    /// <summary>
    ///   Представляет вектор инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) для симметричного алгоритма.
    /// </summary>
    protected byte[] IVValue;
    /// <summary>
    ///   Представляет секретный ключ для симметричного алгоритма.
    /// </summary>
    protected byte[] KeyValue;
    /// <summary>
    ///   Задает размеры блоков (в битах), которые поддерживаются симметричным алгоритмом.
    /// </summary>
    protected KeySizes[] LegalBlockSizesValue;
    /// <summary>
    ///   Задает размеры ключей в битах, которые поддерживаются симметричным алгоритмом.
    /// </summary>
    protected KeySizes[] LegalKeySizesValue;
    /// <summary>
    ///   Представляет размер секретного ключа (в битах), используемого симметричным алгоритмом.
    /// </summary>
    protected int KeySizeValue;
    /// <summary>
    ///   Представляет режим шифрования, используемый в симметричном алгоритме.
    /// </summary>
    protected CipherMode ModeValue;
    /// <summary>
    ///   Представляет режим заполнения, используемый в симметричном алгоритме.
    /// </summary>
    protected PaddingMode PaddingValue;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" />.
    /// </summary>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Реализация класса, производного от алгоритма симметричного шифрования не является допустимым.
    /// </exception>
    protected SymmetricAlgorithm()
    {
      this.ModeValue = CipherMode.CBC;
      this.PaddingValue = PaddingMode.PKCS7;
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" />.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые классом <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" />.
    /// </summary>
    public void Clear()
    {
      this.Dispose();
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this.KeyValue != null)
      {
        Array.Clear((Array) this.KeyValue, 0, this.KeyValue.Length);
        this.KeyValue = (byte[]) null;
      }
      if (this.IVValue == null)
        return;
      Array.Clear((Array) this.IVValue, 0, this.IVValue.Length);
      this.IVValue = (byte[]) null;
    }

    /// <summary>
    ///   Возвращает или задает размер блока криптографической операции (в битах).
    /// </summary>
    /// <returns>Размер блока в битах.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый размер блока.
    /// </exception>
    public virtual int BlockSize
    {
      get
      {
        return this.BlockSizeValue;
      }
      set
      {
        for (int index = 0; index < this.LegalBlockSizesValue.Length; ++index)
        {
          if (this.LegalBlockSizesValue[index].SkipSize == 0)
          {
            if (this.LegalBlockSizesValue[index].MinSize == value)
            {
              this.BlockSizeValue = value;
              this.IVValue = (byte[]) null;
              return;
            }
          }
          else
          {
            int minSize = this.LegalBlockSizesValue[index].MinSize;
            while (minSize <= this.LegalBlockSizesValue[index].MaxSize)
            {
              if (minSize == value)
              {
                if (this.BlockSizeValue == value)
                  return;
                this.BlockSizeValue = value;
                this.IVValue = (byte[]) null;
                return;
              }
              minSize += this.LegalBlockSizesValue[index].SkipSize;
            }
          }
        }
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidBlockSize"));
      }
    }

    /// <summary>
    ///   Возвращает или задает размер ответа в криптографической операции (в битах).
    /// </summary>
    /// <returns>Размер ответа в битах.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Размер данных обратной связи превышает размер блока.
    /// </exception>
    public virtual int FeedbackSize
    {
      get
      {
        return this.FeedbackSizeValue;
      }
      set
      {
        if (value <= 0 || value > this.BlockSizeValue || value % 8 != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFeedbackSize"));
        this.FeedbackSizeValue = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает вектор инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) для симметричного алгоритма.
    /// </summary>
    /// <returns>Вектор инициализации.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Попытка установить вектора инициализации <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Предпринята попытка задания недопустимого размера вектора инициализации.
    /// </exception>
    public virtual byte[] IV
    {
      get
      {
        if (this.IVValue == null)
          this.GenerateIV();
        return (byte[]) this.IVValue.Clone();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (value.Length != this.BlockSizeValue / 8)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidIVSize"));
        this.IVValue = (byte[]) value.Clone();
      }
    }

    /// <summary>
    ///   Возвращает или задает секретный ключ для симметричного алгоритма.
    /// </summary>
    /// <returns>
    ///   Секретный ключ, который должен использоваться для симметричного алгоритма.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Предпринята попытка установить для этого ключа значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый размер ключа.
    /// </exception>
    public virtual byte[] Key
    {
      get
      {
        if (this.KeyValue == null)
          this.GenerateKey();
        return (byte[]) this.KeyValue.Clone();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (!this.ValidKeySize(value.Length * 8))
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
        this.KeyValue = (byte[]) value.Clone();
        this.KeySizeValue = value.Length * 8;
      }
    }

    /// <summary>
    ///   Возвращает размеры блоков (в битах), которые поддерживаются симметричным алгоритмом.
    /// </summary>
    /// <returns>
    ///   Массив, в котором содержатся размеры блоков, поддерживаемые данным алгоритмом.
    /// </returns>
    public virtual KeySizes[] LegalBlockSizes
    {
      get
      {
        return (KeySizes[]) this.LegalBlockSizesValue.Clone();
      }
    }

    /// <summary>
    ///   Возвращает размеры ключа (в битах), которые поддерживаются симметричным алгоритмом.
    /// </summary>
    /// <returns>
    ///   Массив, содержащий размеры ключа, поддерживаемые алгоритмом.
    /// </returns>
    public virtual KeySizes[] LegalKeySizes
    {
      get
      {
        return (KeySizes[]) this.LegalKeySizesValue.Clone();
      }
    }

    /// <summary>
    ///   Возвращает или задает размер секретного ключа (в битах), используемого симметричным алгоритмом.
    /// </summary>
    /// <returns>
    ///   Размер секретного ключа (в битах), используемого симметричным алгоритмом.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый размер ключа.
    /// </exception>
    public virtual int KeySize
    {
      get
      {
        return this.KeySizeValue;
      }
      set
      {
        if (!this.ValidKeySize(value))
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
        this.KeySizeValue = value;
        this.KeyValue = (byte[]) null;
      }
    }

    /// <summary>
    ///   Возвращает или задает режим функционирования симметричного алгоритма.
    /// </summary>
    /// <returns>
    ///   Режим функционирования симметричного алгоритма.
    ///    Значение по умолчанию — <see cref="F:System.Security.Cryptography.CipherMode.CBC" />.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Режим шифрования не является одним из <see cref="T:System.Security.Cryptography.CipherMode" /> значения.
    /// </exception>
    public virtual CipherMode Mode
    {
      get
      {
        return this.ModeValue;
      }
      set
      {
        if (value < CipherMode.CBC || CipherMode.CFB < value)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidCipherMode"));
        this.ModeValue = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает режим заполнения, используемый в симметричном алгоритме.
    /// </summary>
    /// <returns>
    ///   Режим заполнения, используемый в симметричном алгоритме.
    ///    Значение по умолчанию — <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" />.
    /// </returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Режим заполнения не является одним из <see cref="T:System.Security.Cryptography.PaddingMode" /> значения.
    /// </exception>
    public virtual PaddingMode Padding
    {
      get
      {
        return this.PaddingValue;
      }
      set
      {
        if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
        this.PaddingValue = value;
      }
    }

    /// <summary>
    ///   Определяет допустимость указанного размера ключа для текущего алгоритма.
    /// </summary>
    /// <param name="bitLength">
    ///   Длина в битах для проверки допустимости размера ключа.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если заданный размер ключа допустим для текущего алгоритма; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool ValidKeySize(int bitLength)
    {
      KeySizes[] legalKeySizes = this.LegalKeySizes;
      if (legalKeySizes == null)
        return false;
      for (int index = 0; index < legalKeySizes.Length; ++index)
      {
        if (legalKeySizes[index].SkipSize == 0)
        {
          if (legalKeySizes[index].MinSize == bitLength)
            return true;
        }
        else
        {
          int minSize = legalKeySizes[index].MinSize;
          while (minSize <= legalKeySizes[index].MaxSize)
          {
            if (minSize == bitLength)
              return true;
            minSize += legalKeySizes[index].SkipSize;
          }
        }
      }
      return false;
    }

    /// <summary>
    ///   Создает криптографический объект по умолчанию, используемый для выполнения симметричного алгоритма.
    /// </summary>
    /// <returns>
    ///   Криптографический объект по умолчанию, используемый для выполнения симметричного алгоритма.
    /// </returns>
    public static SymmetricAlgorithm Create()
    {
      return SymmetricAlgorithm.Create("System.Security.Cryptography.SymmetricAlgorithm");
    }

    /// <summary>
    ///   Создает заданный криптографический объект, используемый для выполнения симметричного алгоритма.
    /// </summary>
    /// <param name="algName">
    ///   Имя конкретной реализации класса <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> для использования.
    /// </param>
    /// <returns>
    ///   Криптографический объект, используемый для выполнения симметричного алгоритма.
    /// </returns>
    public static SymmetricAlgorithm Create(string algName)
    {
      return (SymmetricAlgorithm) CryptoConfig.CreateFromName(algName);
    }

    /// <summary>
    ///   Создает симметричный объект-шифратор с текущим свойством <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> и вектором инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).
    /// </summary>
    /// <returns>Симметричный объект-шифратор.</returns>
    public virtual ICryptoTransform CreateEncryptor()
    {
      return this.CreateEncryptor(this.Key, this.IV);
    }

    /// <summary>
    ///   Если переопределено в производном классе, создает симметричный объект-шифратор с заданным свойством <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> и вектором инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).
    /// </summary>
    /// <param name="rgbKey">
    ///   Секретный ключ, который должен использоваться для симметричного алгоритма.
    /// </param>
    /// <param name="rgbIV">
    ///   Вектор инициализации, который должен использоваться для симметричного алгоритма.
    /// </param>
    /// <returns>Симметричный объект-шифратор.</returns>
    public abstract ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV);

    /// <summary>
    ///   Создает симметричный объект-дешифратор с текущим свойством <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> и вектором инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).
    /// </summary>
    /// <returns>Симметричный объект-дешифратор.</returns>
    public virtual ICryptoTransform CreateDecryptor()
    {
      return this.CreateDecryptor(this.Key, this.IV);
    }

    /// <summary>
    ///   При переопределении в производном классе создает симметричный объект-дешифратор с указанным свойством <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> и вектором инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).
    /// </summary>
    /// <param name="rgbKey">
    ///   Секретный ключ, который должен использоваться для симметричного алгоритма.
    /// </param>
    /// <param name="rgbIV">
    ///   Вектор инициализации, который должен использоваться для симметричного алгоритма.
    /// </param>
    /// <returns>Симметричный объект-дешифратор.</returns>
    public abstract ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV);

    /// <summary>
    ///   Если переопределено в производном классе, генерирует произвольный ключ (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />), используемый для алгоритма.
    /// </summary>
    public abstract void GenerateKey();

    /// <summary>
    ///   Если переопределено в производном классе, создает произвольный вектор инициализации (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />), используемый для алгоритма.
    /// </summary>
    public abstract void GenerateIV();
  }
}
