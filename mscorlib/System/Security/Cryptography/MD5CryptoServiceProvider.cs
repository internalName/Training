// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.MD5CryptoServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Вычисляет <see cref="T:System.Security.Cryptography.MD5" /> хэш-значение для входных данных, используя реализацию, предоставляемую поставщиком служб шифрования (CSP).
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class MD5CryptoServiceProvider : MD5
  {
    [SecurityCritical]
    private SafeHashHandle _safeHashHandle;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Политику FIPS-совместимого алгоритма не используется.
    /// </exception>
    [SecuritySafeCritical]
    public MD5CryptoServiceProvider()
    {
      if (CryptoConfig.AllowOnlyFipsAlgorithms)
        throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
      this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32771);
    }

    [SecuritySafeCritical]
    protected override void Dispose(bool disposing)
    {
      if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
        this._safeHashHandle.Dispose();
      base.Dispose(disposing);
    }

    /// <summary>
    ///   Инициализирует экземпляр <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />.
    /// </summary>
    [SecuritySafeCritical]
    public override void Initialize()
    {
      if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
        this._safeHashHandle.Dispose();
      this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32771);
    }

    [SecuritySafeCritical]
    protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
    {
      Utils.HashData(this._safeHashHandle, rgb, ibStart, cbSize);
    }

    [SecuritySafeCritical]
    protected override byte[] HashFinal()
    {
      return Utils.EndHash(this._safeHashHandle);
    }
  }
}
