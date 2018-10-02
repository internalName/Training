// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RNGCryptoServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Реализует криптографический генератор случайных чисел, используя реализацию, предоставляемую поставщиком служб шифрования (CSP).
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class RNGCryptoServiceProvider : RandomNumberGenerator
  {
    [SecurityCritical]
    private SafeProvHandle m_safeProvHandle;
    private bool m_ownsHandle;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" />.
    /// </summary>
    public RNGCryptoServiceProvider()
      : this((CspParameters) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" />.
    /// </summary>
    /// <param name="str">
    ///   Ввод строки.
    ///    Этот параметр не учитывается.
    /// </param>
    public RNGCryptoServiceProvider(string str)
      : this((CspParameters) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" />.
    /// </summary>
    /// <param name="rgb">
    ///   Массив байтов.
    ///    Это значение игнорируется.
    /// </param>
    public RNGCryptoServiceProvider(byte[] rgb)
      : this((CspParameters) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> с заданными параметрами.
    /// </summary>
    /// <param name="cspParams">
    ///   Параметры, передаваемые поставщику служб шифрования (CSP).
    /// </param>
    [SecuritySafeCritical]
    public RNGCryptoServiceProvider(CspParameters cspParams)
    {
      if (cspParams != null)
      {
        this.m_safeProvHandle = Utils.AcquireProvHandle(cspParams);
        this.m_ownsHandle = true;
      }
      else
      {
        this.m_safeProvHandle = Utils.StaticProvHandle;
        this.m_ownsHandle = false;
      }
    }

    [SecuritySafeCritical]
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || !this.m_ownsHandle)
        return;
      this.m_safeProvHandle.Dispose();
    }

    /// <summary>
    ///   Заполняет массив байтов криптостойкой последовательностью случайных значений.
    /// </summary>
    /// <param name="data">
    ///   Массив для заполнения криптостойкой последовательностью случайных значений.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось получить поставщик служб шифрования (CSP).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public override void GetBytes(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      RNGCryptoServiceProvider.GetBytes(this.m_safeProvHandle, data, data.Length);
    }

    /// <summary>
    ///   Заполняет массив байтов криптостойкой последовательностью случайных ненулевых значений.
    /// </summary>
    /// <param name="data">
    ///   Массив для заполнения криптостойкой последовательностью случайных ненулевых значений.
    /// </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Не удалось получить поставщик служб шифрования (CSP).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="data" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public override void GetNonZeroBytes(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      RNGCryptoServiceProvider.GetNonZeroBytes(this.m_safeProvHandle, data, data.Length);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetBytes(SafeProvHandle hProv, byte[] randomBytes, int count);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetNonZeroBytes(SafeProvHandle hProv, byte[] randomBytes, int count);
  }
}
