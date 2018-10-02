// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.Rfc2898DeriveBytes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Реализует функцию формирования ключа на основе пароля (PBKDF2) посредством генератора псевдослучайных чисел <see cref="T:System.Security.Cryptography.HMACSHA1" />.
  /// </summary>
  [ComVisible(true)]
  public class Rfc2898DeriveBytes : DeriveBytes
  {
    private CspParameters m_cspParams = new CspParameters();
    private byte[] m_buffer;
    private byte[] m_salt;
    private HMAC m_hmac;
    private byte[] m_password;
    private uint m_iterations;
    private uint m_block;
    private int m_startIndex;
    private int m_endIndex;
    private int m_blockSize;
    [SecurityCritical]
    private SafeProvHandle _safeProvHandle;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" />, используя пароль и соль для формирования ключа.
    /// </summary>
    /// <param name="password">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="saltSize">
    ///   Размер произвольной соли, которую должен создать класс.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Размер указанного расширяющего значения менее 8 байт.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Пароль или расширяющее значение <see langword="null" />.
    /// </exception>
    public Rfc2898DeriveBytes(string password, int saltSize)
      : this(password, saltSize, 1000)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" />, используя пароль, размер соли и число итераций для формирования ключа.
    /// </summary>
    /// <param name="password">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="saltSize">
    ///   Размер произвольной соли, которую должен создать класс.
    /// </param>
    /// <param name="iterations">
    ///   Число итераций для данной операции.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Указанный размер расширяющего значения менее 8 байт или меньше 1 — количество итераций.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Пароль или расширяющее значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="iterations " />находится вне диапазона.
    ///    Параметра должно быть неотрицательным числом.
    /// </exception>
    public Rfc2898DeriveBytes(string password, int saltSize, int iterations)
      : this(password, saltSize, iterations, HashAlgorithmName.SHA1)
    {
    }

    [SecuritySafeCritical]
    public Rfc2898DeriveBytes(string password, int saltSize, int iterations, HashAlgorithmName hashAlgorithm)
    {
      if (saltSize < 0)
        throw new ArgumentOutOfRangeException(nameof (saltSize), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), nameof (hashAlgorithm));
      HMAC hmac = HMAC.Create("HMAC" + hashAlgorithm.Name);
      if (hmac == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_UnknownHashAlgorithm", (object) hashAlgorithm.Name));
      byte[] data = new byte[saltSize];
      Utils.StaticRandomNumberGenerator.GetBytes(data);
      this.Salt = data;
      this.IterationCount = iterations;
      this.m_password = new UTF8Encoding(false).GetBytes(password);
      hmac.Key = this.m_password;
      this.m_hmac = hmac;
      this.m_blockSize = hmac.HashSize >> 3;
      this.Initialize();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" />, используя пароль и соль для формирования ключа.
    /// </summary>
    /// <param name="password">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="salt">
    ///   Соль, используемая для формирования ключа.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Размер указанного расширяющего значения менее 8 байт или меньше 1 — число итераций.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Пароль или расширяющее значение <see langword="null" />.
    /// </exception>
    public Rfc2898DeriveBytes(string password, byte[] salt)
      : this(password, salt, 1000)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" />, используя пароль, расширяющее значение и число итераций для формирования ключа.
    /// </summary>
    /// <param name="password">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="salt">
    ///   Соль, используемая для формирования ключа.
    /// </param>
    /// <param name="iterations">
    ///   Число итераций для данной операции.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Размер указанного расширяющего значения менее 8 байт или меньше 1 — число итераций.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Пароль или расширяющее значение <see langword="null" />.
    /// </exception>
    public Rfc2898DeriveBytes(string password, byte[] salt, int iterations)
      : this(password, salt, iterations, HashAlgorithmName.SHA1)
    {
    }

    public Rfc2898DeriveBytes(string password, byte[] salt, int iterations, HashAlgorithmName hashAlgorithm)
      : this(new UTF8Encoding(false).GetBytes(password), salt, iterations, hashAlgorithm)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" />, используя пароль, расширяющее значение и число итераций для формирования ключа.
    /// </summary>
    /// <param name="password">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="salt">
    ///   Соль, используемая для формирования ключа.
    /// </param>
    /// <param name="iterations">
    ///   Число итераций для данной операции.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Размер указанного расширяющего значения менее 8 байт или меньше 1 — число итераций.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Пароль или расширяющее значение <see langword="null" />.
    /// </exception>
    public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations)
      : this(password, salt, iterations, HashAlgorithmName.SHA1)
    {
    }

    [SecuritySafeCritical]
    public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations, HashAlgorithmName hashAlgorithm)
    {
      if (string.IsNullOrEmpty(hashAlgorithm.Name))
        throw new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), nameof (hashAlgorithm));
      HMAC hmac = HMAC.Create("HMAC" + hashAlgorithm.Name);
      if (hmac == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_UnknownHashAlgorithm", (object) hashAlgorithm.Name));
      this.Salt = salt;
      this.IterationCount = iterations;
      this.m_password = password;
      hmac.Key = password;
      this.m_hmac = hmac;
      this.m_blockSize = hmac.HashSize >> 3;
      this.Initialize();
    }

    /// <summary>
    ///   Возвращает или задает число итераций для данной операции.
    /// </summary>
    /// <returns>Число итераций для данной операции.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Число итераций равно, меньше 1.
    /// </exception>
    public int IterationCount
    {
      get
      {
        return (int) this.m_iterations;
      }
      set
      {
        if (value <= 0)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
        this.m_iterations = (uint) value;
        this.Initialize();
      }
    }

    /// <summary>
    ///   Возвращает или задает значение соли ключа для данной операции.
    /// </summary>
    /// <returns>Значение соли ключа для данной операции.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Размер указанного расширяющего значения менее 8 байт.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Расширяющее значение равно <see langword="null" />.
    /// </exception>
    public byte[] Salt
    {
      get
      {
        return (byte[]) this.m_salt.Clone();
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (value.Length < 8)
          throw new ArgumentException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_FewBytesSalt"));
        this.m_salt = (byte[]) value.Clone();
        this.Initialize();
      }
    }

    /// <summary>
    ///   Возвращает псевдослучайный ключ для данного объекта.
    /// </summary>
    /// <param name="cb">
    ///   Число генерируемых псевдослучайных байтов ключа.
    /// </param>
    /// <returns>
    ///   Массив байтов, заполненный псевдослучайными байтами ключа.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="cb " />выходит за пределы диапазона.
    ///    Этот параметр требует неотрицательным числом.
    /// </exception>
    public override byte[] GetBytes(int cb)
    {
      if (cb <= 0)
        throw new ArgumentOutOfRangeException(nameof (cb), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      byte[] numArray1 = new byte[cb];
      int dstOffsetBytes = 0;
      int byteCount = this.m_endIndex - this.m_startIndex;
      if (byteCount > 0)
      {
        if (cb >= byteCount)
        {
          Buffer.InternalBlockCopy((Array) this.m_buffer, this.m_startIndex, (Array) numArray1, 0, byteCount);
          this.m_startIndex = this.m_endIndex = 0;
          dstOffsetBytes += byteCount;
        }
        else
        {
          Buffer.InternalBlockCopy((Array) this.m_buffer, this.m_startIndex, (Array) numArray1, 0, cb);
          this.m_startIndex += cb;
          return numArray1;
        }
      }
      while (dstOffsetBytes < cb)
      {
        byte[] numArray2 = this.Func();
        int num1 = cb - dstOffsetBytes;
        if (num1 > this.m_blockSize)
        {
          Buffer.InternalBlockCopy((Array) numArray2, 0, (Array) numArray1, dstOffsetBytes, this.m_blockSize);
          dstOffsetBytes += this.m_blockSize;
        }
        else
        {
          Buffer.InternalBlockCopy((Array) numArray2, 0, (Array) numArray1, dstOffsetBytes, num1);
          int num2 = dstOffsetBytes + num1;
          Buffer.InternalBlockCopy((Array) numArray2, num1, (Array) this.m_buffer, this.m_startIndex, this.m_blockSize - num1);
          this.m_endIndex += this.m_blockSize - num1;
          return numArray1;
        }
      }
      return numArray1;
    }

    /// <summary>Восстанавливает состояние данной операции.</summary>
    public override void Reset()
    {
      this.Initialize();
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые классом <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> (при необходимости освобождает и управляемые ресурсы).
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      if (this.m_hmac != null)
        this.m_hmac.Dispose();
      if (this.m_buffer != null)
        Array.Clear((Array) this.m_buffer, 0, this.m_buffer.Length);
      if (this.m_salt == null)
        return;
      Array.Clear((Array) this.m_salt, 0, this.m_salt.Length);
    }

    private void Initialize()
    {
      if (this.m_buffer != null)
        Array.Clear((Array) this.m_buffer, 0, this.m_buffer.Length);
      this.m_buffer = new byte[this.m_blockSize];
      this.m_block = 1U;
      this.m_startIndex = this.m_endIndex = 0;
    }

    private byte[] Func()
    {
      byte[] inputBuffer = Utils.Int(this.m_block);
      this.m_hmac.TransformBlock(this.m_salt, 0, this.m_salt.Length, (byte[]) null, 0);
      this.m_hmac.TransformBlock(inputBuffer, 0, inputBuffer.Length, (byte[]) null, 0);
      this.m_hmac.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
      byte[] hashValue = this.m_hmac.HashValue;
      this.m_hmac.Initialize();
      byte[] numArray = hashValue;
      for (int index1 = 2; (long) index1 <= (long) this.m_iterations; ++index1)
      {
        this.m_hmac.TransformBlock(hashValue, 0, hashValue.Length, (byte[]) null, 0);
        this.m_hmac.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
        hashValue = this.m_hmac.HashValue;
        for (int index2 = 0; index2 < this.m_blockSize; ++index2)
          numArray[index2] ^= hashValue[index2];
        this.m_hmac.Initialize();
      }
      ++this.m_block;
      return numArray;
    }

    /// <summary>
    ///   Возвращает криптографический ключ из объекта <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" />.
    /// </summary>
    /// <param name="algname">
    ///   Имя алгоритма, используемого для формирования ключа.
    /// </param>
    /// <param name="alghashname">
    ///   Имя хэш-алгоритма, используемого для формирования ключа.
    /// </param>
    /// <param name="keySize">Размер формируемого ключа в битах.</param>
    /// <param name="rgbIV">
    ///   Вектор инициализации, используемый для формирования ключа.
    /// </param>
    /// <returns>Сформированный ключ.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   <paramref name="keySize" /> Неверный параметр.
    /// 
    ///   -или-
    /// 
    ///   Не удалось получить поставщик служб шифрования (CSP).
    /// 
    ///   -или-
    /// 
    ///   <paramref name="algname" /> Параметр не является допустимым именем алгоритма.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="alghashname" /> Параметр не является имя допустимым хэш-алгоритма.
    /// </exception>
    [SecuritySafeCritical]
    public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
    {
      if (keySize < 0)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
      int algId1 = X509Utils.NameOrOidToAlgId(alghashname, OidGroup.HashAlgorithm);
      if (algId1 == 0)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidAlgorithm"));
      int algId2 = X509Utils.NameOrOidToAlgId(algname, OidGroup.AllGroups);
      if (algId2 == 0)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidAlgorithm"));
      if (rgbIV == null)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidIV"));
      byte[] o = (byte[]) null;
      Rfc2898DeriveBytes.DeriveKey(this.ProvHandle, algId2, algId1, this.m_password, this.m_password.Length, keySize << 16, rgbIV, rgbIV.Length, JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
      return o;
    }

    private SafeProvHandle ProvHandle
    {
      [SecurityCritical] get
      {
        if (this._safeProvHandle == null)
        {
          lock (this)
          {
            if (this._safeProvHandle == null)
            {
              SafeProvHandle safeProvHandle = Utils.AcquireProvHandle(this.m_cspParams);
              Thread.MemoryBarrier();
              this._safeProvHandle = safeProvHandle;
            }
          }
        }
        return this._safeProvHandle;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DeriveKey(SafeProvHandle hProv, int algid, int algidHash, byte[] password, int cbPassword, int dwFlags, byte[] IV, int cbIV, ObjectHandleOnStack retKey);
  }
}
