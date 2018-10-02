// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CryptoAPITransform
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Выполняет криптографическое преобразование данных.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class CryptoAPITransform : ICryptoTransform, IDisposable
  {
    private int BlockSizeValue;
    private byte[] IVValue;
    private CipherMode ModeValue;
    private PaddingMode PaddingValue;
    private CryptoAPITransformMode encryptOrDecrypt;
    private byte[] _rgbKey;
    private byte[] _depadBuffer;
    [SecurityCritical]
    private SafeKeyHandle _safeKeyHandle;
    [SecurityCritical]
    private SafeProvHandle _safeProvHandle;

    private CryptoAPITransform()
    {
    }

    [SecurityCritical]
    internal CryptoAPITransform(int algid, int cArgs, int[] rgArgIds, object[] rgArgValues, byte[] rgbKey, PaddingMode padding, CipherMode cipherChainingMode, int blockSize, int feedbackSize, bool useSalt, CryptoAPITransformMode encDecMode)
    {
      this.BlockSizeValue = blockSize;
      this.ModeValue = cipherChainingMode;
      this.PaddingValue = padding;
      this.encryptOrDecrypt = encDecMode;
      int[] numArray1 = new int[rgArgIds.Length];
      Array.Copy((Array) rgArgIds, (Array) numArray1, rgArgIds.Length);
      this._rgbKey = new byte[rgbKey.Length];
      Array.Copy((Array) rgbKey, (Array) this._rgbKey, rgbKey.Length);
      object[] objArray = new object[rgArgValues.Length];
      for (int index = 0; index < rgArgValues.Length; ++index)
      {
        if (rgArgValues[index] is byte[])
        {
          byte[] rgArgValue = (byte[]) rgArgValues[index];
          byte[] numArray2 = new byte[rgArgValue.Length];
          Array.Copy((Array) rgArgValue, (Array) numArray2, rgArgValue.Length);
          objArray[index] = (object) numArray2;
        }
        else if (rgArgValues[index] is int)
          objArray[index] = (object) (int) rgArgValues[index];
        else if (rgArgValues[index] is CipherMode)
          objArray[index] = (object) (int) rgArgValues[index];
      }
      this._safeProvHandle = Utils.AcquireProvHandle(new CspParameters(24));
      SafeKeyHandle invalidHandle = SafeKeyHandle.InvalidHandle;
      Utils._ImportBulkKey(this._safeProvHandle, algid, useSalt, this._rgbKey, ref invalidHandle);
      this._safeKeyHandle = invalidHandle;
      for (int index = 0; index < cArgs; ++index)
      {
        int dwValue;
        switch (rgArgIds[index])
        {
          case 1:
            this.IVValue = (byte[]) objArray[index];
            byte[] ivValue = this.IVValue;
            Utils.SetKeyParamRgb(this._safeKeyHandle, numArray1[index], ivValue, ivValue.Length);
            continue;
          case 4:
            this.ModeValue = (CipherMode) objArray[index];
            dwValue = (int) objArray[index];
            break;
          case 5:
            dwValue = (int) objArray[index];
            break;
          case 19:
            dwValue = (int) objArray[index];
            break;
          default:
            throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeyParameter"), "_rgArgIds[i]");
        }
        Utils.SetKeyParamDw(this._safeKeyHandle, numArray1[index], dwValue);
      }
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Security.Cryptography.CryptoAPITransform" />.
    /// </summary>
    public void Dispose()
    {
      this.Clear();
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> метод.
    /// </summary>
    [SecuritySafeCritical]
    public void Clear()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    [SecurityCritical]
    private void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this._rgbKey != null)
      {
        Array.Clear((Array) this._rgbKey, 0, this._rgbKey.Length);
        this._rgbKey = (byte[]) null;
      }
      if (this.IVValue != null)
      {
        Array.Clear((Array) this.IVValue, 0, this.IVValue.Length);
        this.IVValue = (byte[]) null;
      }
      if (this._depadBuffer != null)
      {
        Array.Clear((Array) this._depadBuffer, 0, this._depadBuffer.Length);
        this._depadBuffer = (byte[]) null;
      }
      if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
        this._safeKeyHandle.Dispose();
      if (this._safeProvHandle == null || this._safeProvHandle.IsClosed)
        return;
      this._safeProvHandle.Dispose();
    }

    /// <summary>Получает дескриптор ключа.</summary>
    /// <returns>Дескриптор ключа.</returns>
    public IntPtr KeyHandle
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this._safeKeyHandle.DangerousGetHandle();
      }
    }

    /// <summary>Возвращает размер входного блока.</summary>
    /// <returns>Размер входного блока в байтах.</returns>
    public int InputBlockSize
    {
      get
      {
        return this.BlockSizeValue / 8;
      }
    }

    /// <summary>Возвращает размер выходного блока.</summary>
    /// <returns>Размер выходного блока в байтах.</returns>
    public int OutputBlockSize
    {
      get
      {
        return this.BlockSizeValue / 8;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, возможно ли преобразование нескольких блоков.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если возможно преобразование нескольких блоков; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool CanTransformMultipleBlocks
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, возможно ли повторное использование текущего преобразования.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="true" />.
    /// </returns>
    public bool CanReuseTransform
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///   Восстанавливает внутреннее состояние <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> чтобы его можно использовать повторно для выполнения различных шифрования или расшифровки.
    /// </summary>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void Reset()
    {
      this._depadBuffer = (byte[]) null;
      byte[] outputBuffer = (byte[]) null;
      Utils._EncryptData(this._safeKeyHandle, EmptyArray<byte>.Value, 0, 0, ref outputBuffer, 0, this.PaddingValue, true);
    }

    /// <summary>
    ///   Вычисляет преобразование для заданной области входного массива байтов и копирует результирующее преобразование в заданную область выходного массива байтов.
    /// </summary>
    /// <param name="inputBuffer">
    ///   Входные данные для выполнения операции.
    /// </param>
    /// <param name="inputOffset">
    ///   Смещение в массиве байтов ввода, из которого следует использовать данные из.
    /// </param>
    /// <param name="inputCount">
    ///   Число байтов во входном массиве для использования в качестве данных.
    /// </param>
    /// <param name="outputBuffer">
    ///   Выходные данные для записи данных.
    /// </param>
    /// <param name="outputOffset">
    ///   Смещение в выходном массиве байтов, начиная с которого следует записывать данные.
    /// </param>
    /// <returns>Число записанных байтов.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="inputBuffer" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="outputBuffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина входного буфера меньше суммы входного смещения и счетчика ввода.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="inputOffset" /> выходит за пределы диапазона.
    ///    Этот параметр требует неотрицательным числом.
    /// </exception>
    [SecuritySafeCritical]
    public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
    {
      if (inputBuffer == null)
        throw new ArgumentNullException(nameof (inputBuffer));
      if (outputBuffer == null)
        throw new ArgumentNullException(nameof (outputBuffer));
      if (inputOffset < 0)
        throw new ArgumentOutOfRangeException(nameof (inputOffset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (inputCount <= 0 || inputCount % this.InputBlockSize != 0 || inputCount > inputBuffer.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      if (inputBuffer.Length - inputCount < inputOffset)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.encryptOrDecrypt == CryptoAPITransformMode.Encrypt)
        return Utils._EncryptData(this._safeKeyHandle, inputBuffer, inputOffset, inputCount, ref outputBuffer, outputOffset, this.PaddingValue, false);
      if (this.PaddingValue == PaddingMode.Zeros || this.PaddingValue == PaddingMode.None)
        return Utils._DecryptData(this._safeKeyHandle, inputBuffer, inputOffset, inputCount, ref outputBuffer, outputOffset, this.PaddingValue, false);
      if (this._depadBuffer == null)
      {
        this._depadBuffer = new byte[this.InputBlockSize];
        int cb = inputCount - this.InputBlockSize;
        Buffer.InternalBlockCopy((Array) inputBuffer, inputOffset + cb, (Array) this._depadBuffer, 0, this.InputBlockSize);
        return Utils._DecryptData(this._safeKeyHandle, inputBuffer, inputOffset, cb, ref outputBuffer, outputOffset, this.PaddingValue, false);
      }
      Utils._DecryptData(this._safeKeyHandle, this._depadBuffer, 0, this._depadBuffer.Length, ref outputBuffer, outputOffset, this.PaddingValue, false);
      outputOffset += this.OutputBlockSize;
      int cb1 = inputCount - this.InputBlockSize;
      Buffer.InternalBlockCopy((Array) inputBuffer, inputOffset + cb1, (Array) this._depadBuffer, 0, this.InputBlockSize);
      return this.OutputBlockSize + Utils._DecryptData(this._safeKeyHandle, inputBuffer, inputOffset, cb1, ref outputBuffer, outputOffset, this.PaddingValue, false);
    }

    /// <summary>
    ///   Вычисляет преобразование для заданной области заданного массива байтов.
    /// </summary>
    /// <param name="inputBuffer">
    ///   Входные данные для выполнения операции.
    /// </param>
    /// <param name="inputOffset">
    ///   Смещение в массиве байтов, с которого следует начать использование данных из.
    /// </param>
    /// <param name="inputCount">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    /// <returns>Вычисленное преобразование.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="inputBuffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="inputOffset" /> Меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="inputCount" /> Меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Длина входного буфера меньше суммы входного смещения и счетчика ввода.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" /> Заполнение является недопустимым.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="inputOffset" /> Выходит за пределы диапазона.
    ///    Этот параметр требует неотрицательным числом.
    /// </exception>
    [SecuritySafeCritical]
    public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    {
      if (inputBuffer == null)
        throw new ArgumentNullException(nameof (inputBuffer));
      if (inputOffset < 0)
        throw new ArgumentOutOfRangeException(nameof (inputOffset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (inputCount < 0 || inputCount > inputBuffer.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      if (inputBuffer.Length - inputCount < inputOffset)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.encryptOrDecrypt == CryptoAPITransformMode.Encrypt)
      {
        byte[] outputBuffer = (byte[]) null;
        Utils._EncryptData(this._safeKeyHandle, inputBuffer, inputOffset, inputCount, ref outputBuffer, 0, this.PaddingValue, true);
        this.Reset();
        return outputBuffer;
      }
      if (inputCount % this.InputBlockSize != 0)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_SSD_InvalidDataSize"));
      if (this._depadBuffer == null)
      {
        byte[] outputBuffer = (byte[]) null;
        Utils._DecryptData(this._safeKeyHandle, inputBuffer, inputOffset, inputCount, ref outputBuffer, 0, this.PaddingValue, true);
        this.Reset();
        return outputBuffer;
      }
      byte[] data = new byte[this._depadBuffer.Length + inputCount];
      Buffer.InternalBlockCopy((Array) this._depadBuffer, 0, (Array) data, 0, this._depadBuffer.Length);
      Buffer.InternalBlockCopy((Array) inputBuffer, inputOffset, (Array) data, this._depadBuffer.Length, inputCount);
      byte[] outputBuffer1 = (byte[]) null;
      Utils._DecryptData(this._safeKeyHandle, data, 0, data.Length, ref outputBuffer1, 0, this.PaddingValue, true);
      this.Reset();
      return outputBuffer1;
    }
  }
}
