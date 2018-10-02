// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.ToBase64Transform
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Преобразует <see cref="T:System.Security.Cryptography.CryptoStream" /> в кодировку base64.
  /// </summary>
  [ComVisible(true)]
  public class ToBase64Transform : ICryptoTransform, IDisposable
  {
    /// <summary>Возвращает размер входного блока.</summary>
    /// <returns>Размер входного блока данных в байтах.</returns>
    public int InputBlockSize
    {
      get
      {
        return 3;
      }
    }

    /// <summary>Возвращает размер выходного блока.</summary>
    /// <returns>Размер выходного блока данных в байтах.</returns>
    public int OutputBlockSize
    {
      get
      {
        return 4;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее на возможность преобразования нескольких блоков.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="false" />.
    /// </returns>
    public bool CanTransformMultipleBlocks
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, возможно ли повторное использование текущего преобразования.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="true" />.
    /// </returns>
    public virtual bool CanReuseTransform
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///   Преобразует заданную область входного массива байтов в кодировку base64 и копирует результат в заданную область выходного массива байтов.
    /// </summary>
    /// <param name="inputBuffer">
    ///   Входные данные, подлежащие преобразованию в кодировку base64.
    /// </param>
    /// <param name="inputOffset">
    ///   Смещение во входном массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="inputCount">
    ///   Число байтов во входном массиве для использования в качестве данных.
    /// </param>
    /// <param name="outputBuffer">
    ///   Выходной массив, в который записывается результат.
    /// </param>
    /// <param name="outputOffset">
    ///   Смещение в выходном массиве байтов, начиная с которого следует записывать данные.
    /// </param>
    /// <returns>Число записанных байтов.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий <see cref="T:System.Security.Cryptography.ToBase64Transform" /> объект уже был удален.
    /// </exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    ///   Недопустимый размер данных.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="inputBuffer" /> Параметр содержит недопустимое значение смещения.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="inputCount" /> Параметр содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="inputBuffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="inputBuffer" /> Параметра должно быть неотрицательным числом.
    /// </exception>
    public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
    {
      if (inputBuffer == null)
        throw new ArgumentNullException(nameof (inputBuffer));
      if (inputOffset < 0)
        throw new ArgumentOutOfRangeException(nameof (inputOffset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (inputCount < 0 || inputCount > inputBuffer.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      if (inputBuffer.Length - inputCount < inputOffset)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      char[] chArray = new char[4];
      Convert.ToBase64CharArray(inputBuffer, inputOffset, 3, chArray, 0);
      byte[] bytes = Encoding.ASCII.GetBytes(chArray);
      if (bytes.Length != 4)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_SSE_InvalidDataSize"));
      Buffer.BlockCopy((Array) bytes, 0, (Array) outputBuffer, outputOffset, bytes.Length);
      return bytes.Length;
    }

    /// <summary>
    ///   Преобразует заданную область заданного массива байтов в кодировку base64.
    /// </summary>
    /// <param name="inputBuffer">
    ///   Входные данные, подлежащие преобразованию в кодировку base64.
    /// </param>
    /// <param name="inputOffset">
    ///   Смещение в массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="inputCount">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    /// <returns>Вычисляемое преобразование в кодировку base64.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий <see cref="T:System.Security.Cryptography.ToBase64Transform" /> объект уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="inputBuffer" /> Параметр содержит недопустимое значение смещения.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="inputCount" /> Параметр содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="inputBuffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="inputBuffer" /> Параметра должно быть неотрицательным числом.
    /// </exception>
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
      if (inputCount == 0)
        return EmptyArray<byte>.Value;
      char[] chArray = new char[4];
      Convert.ToBase64CharArray(inputBuffer, inputOffset, inputCount, chArray, 0);
      return Encoding.ASCII.GetBytes(chArray);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Security.Cryptography.ToBase64Transform" />.
    /// </summary>
    public void Dispose()
    {
      this.Clear();
    }

    /// <summary>
    ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.Security.Cryptography.ToBase64Transform" />.
    /// </summary>
    public void Clear()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.Security.Cryptography.ToBase64Transform" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые <see cref="T:System.Security.Cryptography.ToBase64Transform" />.
    /// </summary>
    ~ToBase64Transform()
    {
      this.Dispose(false);
    }
  }
}
