// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.MACTripleDES
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Вычисляет код проверки подлинности сообщения (MAC) с помощью алгоритма <see cref="T:System.Security.Cryptography.TripleDES" /> для входных данных <see cref="T:System.Security.Cryptography.CryptoStream" />.
  /// </summary>
  [ComVisible(true)]
  public class MACTripleDES : KeyedHashAlgorithm
  {
    private ICryptoTransform m_encryptor;
    private CryptoStream _cs;
    private TailStream _ts;
    private const int m_bitsPerByte = 8;
    private int m_bytesPerBlock;
    private TripleDES des;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.MACTripleDES" />.
    /// </summary>
    public MACTripleDES()
    {
      this.KeyValue = new byte[24];
      Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
      this.des = TripleDES.Create();
      this.HashSizeValue = this.des.BlockSize;
      this.m_bytesPerBlock = this.des.BlockSize / 8;
      this.des.IV = new byte[this.m_bytesPerBlock];
      this.des.Padding = PaddingMode.Zeros;
      this.m_encryptor = (ICryptoTransform) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.MACTripleDES" /> указанными данными ключа.
    /// </summary>
    /// <param name="rgbKey">
    ///   Секретный ключ для шифрования <see cref="T:System.Security.Cryptography.MACTripleDES" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rgbKey" /> имеет значение <see langword="null" />.
    /// </exception>
    public MACTripleDES(byte[] rgbKey)
      : this("System.Security.Cryptography.TripleDES", rgbKey)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.MACTripleDES" /> указанными данными ключа и используя  заданную реализацию <see cref="T:System.Security.Cryptography.TripleDES" />.
    /// </summary>
    /// <param name="strTripleDES">
    ///   Имя используемой реализации <see cref="T:System.Security.Cryptography.TripleDES" />.
    /// </param>
    /// <param name="rgbKey">
    ///   Секретный ключ для шифрования <see cref="T:System.Security.Cryptography.MACTripleDES" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="rgbKey" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    ///   <paramref name="strTripleDES" /> Параметр не является допустимым именем <see cref="T:System.Security.Cryptography.TripleDES" /> реализации.
    /// </exception>
    public MACTripleDES(string strTripleDES, byte[] rgbKey)
    {
      if (rgbKey == null)
        throw new ArgumentNullException(nameof (rgbKey));
      this.des = strTripleDES != null ? TripleDES.Create(strTripleDES) : TripleDES.Create();
      this.HashSizeValue = this.des.BlockSize;
      this.KeyValue = (byte[]) rgbKey.Clone();
      this.m_bytesPerBlock = this.des.BlockSize / 8;
      this.des.IV = new byte[this.m_bytesPerBlock];
      this.des.Padding = PaddingMode.Zeros;
      this.m_encryptor = (ICryptoTransform) null;
    }

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Security.Cryptography.MACTripleDES" />.
    /// </summary>
    public override void Initialize()
    {
      this.m_encryptor = (ICryptoTransform) null;
    }

    /// <summary>
    ///   Возвращает или задает режим заполнения, используемый в хэш-алгоритме.
    /// </summary>
    /// <returns>Режим заполнения, используемый в хэш-алгоритме.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Невозможно задать свойство, поскольку указан недопустимый режим заполнения.
    /// </exception>
    [ComVisible(false)]
    public PaddingMode Padding
    {
      get
      {
        return this.des.Padding;
      }
      set
      {
        if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
        this.des.Padding = value;
      }
    }

    /// <summary>
    ///   Передает данные, записанные в объект, в шифратор <see cref="T:System.Security.Cryptography.TripleDES" /> для вычисления кода проверки подлинности сообщения (MAC).
    /// </summary>
    /// <param name="rgbData">Входные данные.</param>
    /// <param name="ibStart">
    ///   Смещение в массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="cbSize">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    protected override void HashCore(byte[] rgbData, int ibStart, int cbSize)
    {
      if (this.m_encryptor == null)
      {
        this.des.Key = this.Key;
        this.m_encryptor = this.des.CreateEncryptor();
        this._ts = new TailStream(this.des.BlockSize / 8);
        this._cs = new CryptoStream((Stream) this._ts, this.m_encryptor, CryptoStreamMode.Write);
      }
      this._cs.Write(rgbData, ibStart, cbSize);
    }

    /// <summary>
    ///   Возвращает вычисленный код проверки подлинности сообщения (MAC) после записи в объект всех данных.
    /// </summary>
    /// <returns>Вычисленный код MAC.</returns>
    protected override byte[] HashFinal()
    {
      if (this.m_encryptor == null)
      {
        this.des.Key = this.Key;
        this.m_encryptor = this.des.CreateEncryptor();
        this._ts = new TailStream(this.des.BlockSize / 8);
        this._cs = new CryptoStream((Stream) this._ts, this.m_encryptor, CryptoStreamMode.Write);
      }
      this._cs.FlushFinalBlock();
      return this._ts.Buffer;
    }

    /// <summary>
    ///   Освобождает ресурсы, используемые экземпляром <see cref="T:System.Security.Cryptography.MACTripleDES" />.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" />, если метод вызывается из реализации <see cref="M:System.IDisposable.Dispose" />; в противном случае — значение <see langword="false" />.
    /// </param>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.des != null)
          this.des.Clear();
        if (this.m_encryptor != null)
          this.m_encryptor.Dispose();
        if (this._cs != null)
          this._cs.Clear();
        if (this._ts != null)
          this._ts.Clear();
      }
      base.Dispose(disposing);
    }
  }
}
