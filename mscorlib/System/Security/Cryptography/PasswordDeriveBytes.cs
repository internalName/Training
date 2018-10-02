// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.PasswordDeriveBytes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Формирует ключ из пароля с помощью расширения алгоритма PBKDF1.
  /// </summary>
  [ComVisible(true)]
  public class PasswordDeriveBytes : DeriveBytes
  {
    private int _extraCount;
    private int _prefix;
    private int _iterations;
    private byte[] _baseValue;
    private byte[] _extra;
    private byte[] _salt;
    private string _hashName;
    private byte[] _password;
    private HashAlgorithm _hash;
    private CspParameters _cspParams;
    [SecurityCritical]
    private SafeProvHandle _safeProvHandle;

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
              SafeProvHandle safeProvHandle = Utils.AcquireProvHandle(this._cspParams);
              Thread.MemoryBarrier();
              this._safeProvHandle = safeProvHandle;
            }
          }
        }
        return this._safeProvHandle;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> паролем и солью ключа, которые используются для формирования ключа.
    /// </summary>
    /// <param name="strPassword">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="rgbSalt">
    ///   Соль ключа, используемая для формирования ключа.
    /// </param>
    public PasswordDeriveBytes(string strPassword, byte[] rgbSalt)
      : this(strPassword, rgbSalt, new CspParameters())
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> паролем и солью ключа, которые используются для формирования ключа.
    /// </summary>
    /// <param name="password">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="salt">
    ///   Соль ключа, используемая для формирования ключа.
    /// </param>
    public PasswordDeriveBytes(byte[] password, byte[] salt)
      : this(password, salt, new CspParameters())
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> паролем, солью ключа, именем хэша и числом итераций, которые используются для формирования ключа.
    /// </summary>
    /// <param name="strPassword">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="rgbSalt">
    ///   Соль ключа, используемая для формирования ключа.
    /// </param>
    /// <param name="strHashName">
    ///   Имя хэш-алгоритма для данной операции.
    /// </param>
    /// <param name="iterations">
    ///   Число итераций для данной операции.
    /// </param>
    public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations)
      : this(strPassword, rgbSalt, strHashName, iterations, new CspParameters())
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> паролем, солью ключа, именем хэша и числом итераций, которые используются для формирования ключа.
    /// </summary>
    /// <param name="password">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="salt">
    ///   Соль ключа, используемая для формирования ключа.
    /// </param>
    /// <param name="hashName">
    ///   Имя хэш-алгоритма, используемое для формирования ключа.
    /// </param>
    /// <param name="iterations">
    ///   Число итераций для данной операции.
    /// </param>
    public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations)
      : this(password, salt, hashName, iterations, new CspParameters())
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> паролем, солью ключа и параметрами поставщика служб шифрования (CSP), которые используются для формирования ключа.
    /// </summary>
    /// <param name="strPassword">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="rgbSalt">
    ///   Соль ключа, используемая для формирования ключа.
    /// </param>
    /// <param name="cspParams">Параметры CSP для данной операции.</param>
    public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, CspParameters cspParams)
      : this(strPassword, rgbSalt, "SHA1", 100, cspParams)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> паролем, солью ключа и параметрами поставщика служб шифрования (CSP), которые используются для формирования ключа.
    /// </summary>
    /// <param name="password">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="salt">
    ///   Соль ключа, используемая для формирования ключа.
    /// </param>
    /// <param name="cspParams">
    ///   Параметры поставщика служб шифрования (CSP) для данной операции.
    /// </param>
    public PasswordDeriveBytes(byte[] password, byte[] salt, CspParameters cspParams)
      : this(password, salt, "SHA1", 100, cspParams)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> паролем, солью ключа, именем хэша, числом итераций и параметрами поставщика служб шифрования (CSP), которые используются для формирования ключа.
    /// </summary>
    /// <param name="strPassword">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="rgbSalt">
    ///   Соль ключа, используемая для формирования ключа.
    /// </param>
    /// <param name="strHashName">
    ///   Имя хэш-алгоритма для данной операции.
    /// </param>
    /// <param name="iterations">
    ///   Число итераций для данной операции.
    /// </param>
    /// <param name="cspParams">Параметры CSP для данной операции.</param>
    public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations, CspParameters cspParams)
      : this(new UTF8Encoding(false).GetBytes(strPassword), rgbSalt, strHashName, iterations, cspParams)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> паролем, солью ключа, именем хэша, числом итераций и параметрами поставщика служб шифрования (CSP), которые используются для формирования ключа.
    /// </summary>
    /// <param name="password">
    ///   Пароль, используемый для формирования ключа.
    /// </param>
    /// <param name="salt">
    ///   Соль ключа, используемая для формирования ключа.
    /// </param>
    /// <param name="hashName">
    ///   Имя хэш-алгоритма, используемое для формирования ключа.
    /// </param>
    /// <param name="iterations">
    ///   Число итераций для данной операции.
    /// </param>
    /// <param name="cspParams">
    ///   Параметры поставщика служб шифрования (CSP) для данной операции.
    /// </param>
    [SecuritySafeCritical]
    public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations, CspParameters cspParams)
    {
      this.IterationCount = iterations;
      this.Salt = salt;
      this.HashName = hashName;
      this._password = password;
      this._cspParams = cspParams;
    }

    /// <summary>
    ///   Возвращает или задает имя хэш-алгоритма для данной операции.
    /// </summary>
    /// <returns>Имя хэш-алгоритма для данной операции.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Имя значения хэша фиксирован и попытка изменить это значение.
    /// </exception>
    public string HashName
    {
      get
      {
        return this._hashName;
      }
      set
      {
        if (this._baseValue != null)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_ValuesFixed", (object) nameof (HashName)));
        this._hashName = value;
        this._hash = (HashAlgorithm) CryptoConfig.CreateFromName(this._hashName);
      }
    }

    /// <summary>
    ///   Возвращает или задает число итераций для данной операции.
    /// </summary>
    /// <returns>Число итераций для данной операции.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Число итераций является фиксированным, и предпринята попытка изменить это значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Невозможно задать свойство, поскольку его значение находится вне допустимого диапазона.
    ///    Для этого свойства требуется неотрицательное число.
    /// </exception>
    public int IterationCount
    {
      get
      {
        return this._iterations;
      }
      set
      {
        if (value <= 0)
          throw new ArgumentOutOfRangeException(nameof (value), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
        if (this._baseValue != null)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_ValuesFixed", (object) nameof (IterationCount)));
        this._iterations = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение соли ключа для данной операции.
    /// </summary>
    /// <returns>Значение соли ключа для данной операции.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Расширяющее значение фиксируется, и предпринята попытка изменить это значение.
    /// </exception>
    public byte[] Salt
    {
      get
      {
        if (this._salt == null)
          return (byte[]) null;
        return (byte[]) this._salt.Clone();
      }
      set
      {
        if (this._baseValue != null)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_ValuesFixed", (object) nameof (Salt)));
        if (value == null)
          this._salt = (byte[]) null;
        else
          this._salt = (byte[]) value.Clone();
      }
    }

    /// <summary>Возвращает псевдослучайные байты ключа.</summary>
    /// <param name="cb">
    ///   Число генерируемых псевдослучайных байтов ключа.
    /// </param>
    /// <returns>
    ///   Массив байтов, заполненный псевдослучайными байтами ключа.
    /// </returns>
    [SecuritySafeCritical]
    [Obsolete("Rfc2898DeriveBytes replaces PasswordDeriveBytes for deriving key material from a password and is preferred in new applications.")]
    public override byte[] GetBytes(int cb)
    {
      int num = 0;
      byte[] numArray = new byte[cb];
      if (this._baseValue == null)
        this.ComputeBaseValue();
      else if (this._extra != null)
      {
        num = this._extra.Length - this._extraCount;
        if (num >= cb)
        {
          Buffer.InternalBlockCopy((Array) this._extra, this._extraCount, (Array) numArray, 0, cb);
          if (num > cb)
            this._extraCount += cb;
          else
            this._extra = (byte[]) null;
          return numArray;
        }
        Buffer.InternalBlockCopy((Array) this._extra, num, (Array) numArray, 0, num);
        this._extra = (byte[]) null;
      }
      byte[] bytes = this.ComputeBytes(cb - num);
      Buffer.InternalBlockCopy((Array) bytes, 0, (Array) numArray, num, cb - num);
      if (bytes.Length + num > cb)
      {
        this._extra = bytes;
        this._extraCount = cb - num;
      }
      return numArray;
    }

    /// <summary>Восстанавливает состояние данной операции.</summary>
    public override void Reset()
    {
      this._prefix = 0;
      this._extra = (byte[]) null;
      this._baseValue = (byte[]) null;
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые классом <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> (при необходимости освобождает и управляемые ресурсы).
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      if (this._hash != null)
        this._hash.Dispose();
      if (this._baseValue != null)
        Array.Clear((Array) this._baseValue, 0, this._baseValue.Length);
      if (this._extra != null)
        Array.Clear((Array) this._extra, 0, this._extra.Length);
      if (this._password != null)
        Array.Clear((Array) this._password, 0, this._password.Length);
      if (this._salt == null)
        return;
      Array.Clear((Array) this._salt, 0, this._salt.Length);
    }

    /// <summary>
    ///   Возвращает криптографический ключ из объекта <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" />.
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
      PasswordDeriveBytes.DeriveKey(this.ProvHandle, algId2, algId1, this._password, this._password.Length, keySize << 16, rgbIV, rgbIV.Length, JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DeriveKey(SafeProvHandle hProv, int algid, int algidHash, byte[] password, int cbPassword, int dwFlags, byte[] IV, int cbIV, ObjectHandleOnStack retKey);

    private byte[] ComputeBaseValue()
    {
      this._hash.Initialize();
      this._hash.TransformBlock(this._password, 0, this._password.Length, this._password, 0);
      if (this._salt != null)
        this._hash.TransformBlock(this._salt, 0, this._salt.Length, this._salt, 0);
      this._hash.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
      this._baseValue = this._hash.Hash;
      this._hash.Initialize();
      for (int index = 1; index < this._iterations - 1; ++index)
      {
        this._hash.ComputeHash(this._baseValue);
        this._baseValue = this._hash.Hash;
      }
      return this._baseValue;
    }

    [SecurityCritical]
    private byte[] ComputeBytes(int cb)
    {
      int dstOffsetBytes1 = 0;
      this._hash.Initialize();
      int byteCount = this._hash.HashSize / 8;
      byte[] numArray = new byte[(cb + byteCount - 1) / byteCount * byteCount];
      using (CryptoStream cs = new CryptoStream(Stream.Null, (ICryptoTransform) this._hash, CryptoStreamMode.Write))
      {
        this.HashPrefix(cs);
        cs.Write(this._baseValue, 0, this._baseValue.Length);
        cs.Close();
      }
      Buffer.InternalBlockCopy((Array) this._hash.Hash, 0, (Array) numArray, dstOffsetBytes1, byteCount);
      int dstOffsetBytes2 = dstOffsetBytes1 + byteCount;
      while (cb > dstOffsetBytes2)
      {
        this._hash.Initialize();
        using (CryptoStream cs = new CryptoStream(Stream.Null, (ICryptoTransform) this._hash, CryptoStreamMode.Write))
        {
          this.HashPrefix(cs);
          cs.Write(this._baseValue, 0, this._baseValue.Length);
          cs.Close();
        }
        Buffer.InternalBlockCopy((Array) this._hash.Hash, 0, (Array) numArray, dstOffsetBytes2, byteCount);
        dstOffsetBytes2 += byteCount;
      }
      return numArray;
    }

    private void HashPrefix(CryptoStream cs)
    {
      int index = 0;
      byte[] buffer = new byte[3]
      {
        (byte) 48,
        (byte) 48,
        (byte) 48
      };
      if (this._prefix > 999)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_TooManyBytes"));
      if (this._prefix >= 100)
      {
        buffer[0] += (byte) (this._prefix / 100);
        ++index;
      }
      if (this._prefix >= 10)
      {
        buffer[index] += (byte) (this._prefix % 100 / 10);
        ++index;
      }
      if (this._prefix > 0)
      {
        buffer[index] += (byte) (this._prefix % 10);
        int count = index + 1;
        cs.Write(buffer, 0, count);
      }
      ++this._prefix;
    }
  }
}
