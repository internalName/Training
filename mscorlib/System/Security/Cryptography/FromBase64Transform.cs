// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.FromBase64Transform
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Преобразует поток <see cref="T:System.Security.Cryptography.CryptoStream" /> из кодировки base64.
  /// </summary>
  [ComVisible(true)]
  public class FromBase64Transform : ICryptoTransform, IDisposable
  {
    private byte[] _inputBuffer = new byte[4];
    private int _inputIndex;
    private FromBase64TransformMode _whitespaces;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.FromBase64Transform" />.
    /// </summary>
    public FromBase64Transform()
      : this(FromBase64TransformMode.IgnoreWhiteSpaces)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Cryptography.FromBase64Transform" /> в соответствии с заданным режимом преобразования.
    /// </summary>
    /// <param name="whitespaces">
    ///   Одно из значений <see cref="T:System.Security.Cryptography.FromBase64Transform" />.
    /// </param>
    public FromBase64Transform(FromBase64TransformMode whitespaces)
    {
      this._whitespaces = whitespaces;
      this._inputIndex = 0;
    }

    /// <summary>Возвращает размер входного блока.</summary>
    /// <returns>Размер входного блока данных в байтах.</returns>
    public int InputBlockSize
    {
      get
      {
        return 1;
      }
    }

    /// <summary>Возвращает размер выходного блока.</summary>
    /// <returns>Размер выходного блока данных в байтах.</returns>
    public int OutputBlockSize
    {
      get
      {
        return 3;
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
    ///   Преобразует заданную область входного массива байтов из кодировки base64 и копирует результат в заданную область выходного массива байтов.
    /// </summary>
    /// <param name="inputBuffer">
    ///   Входные данные для вычисления преобразования из кодировки base64.
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
    ///   Текущий <see cref="T:System.Security.Cryptography.FromBase64Transform" /> объект уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="inputCount" /> используется недопустимое значение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="inputBuffer" /> имеет недопустимое значение смещения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="inputOffset" /> выходит за пределы диапазона.
    ///    Этот параметр требует неотрицательным числом.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="inputBuffer" /> имеет значение <see langword="null" />.
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
      if (this._inputBuffer == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_Generic"));
      byte[] numArray1 = new byte[inputCount];
      int byteCount;
      if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
      {
        numArray1 = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
        byteCount = numArray1.Length;
      }
      else
      {
        Buffer.InternalBlockCopy((Array) inputBuffer, inputOffset, (Array) numArray1, 0, inputCount);
        byteCount = inputCount;
      }
      if (byteCount + this._inputIndex < 4)
      {
        Buffer.InternalBlockCopy((Array) numArray1, 0, (Array) this._inputBuffer, this._inputIndex, byteCount);
        this._inputIndex += byteCount;
        return 0;
      }
      int num = (byteCount + this._inputIndex) / 4;
      byte[] bytes = new byte[this._inputIndex + byteCount];
      Buffer.InternalBlockCopy((Array) this._inputBuffer, 0, (Array) bytes, 0, this._inputIndex);
      Buffer.InternalBlockCopy((Array) numArray1, 0, (Array) bytes, this._inputIndex, byteCount);
      this._inputIndex = (byteCount + this._inputIndex) % 4;
      Buffer.InternalBlockCopy((Array) numArray1, byteCount - this._inputIndex, (Array) this._inputBuffer, 0, this._inputIndex);
      byte[] numArray2 = Convert.FromBase64CharArray(Encoding.ASCII.GetChars(bytes, 0, 4 * num), 0, 4 * num);
      Buffer.BlockCopy((Array) numArray2, 0, (Array) outputBuffer, outputOffset, numArray2.Length);
      return numArray2.Length;
    }

    /// <summary>
    ///   Преобразует заданную область заданного массива байтов из кодировки base64.
    /// </summary>
    /// <param name="inputBuffer">
    ///   Входные данные, которые необходимо преобразовать из кодировки base64.
    /// </param>
    /// <param name="inputOffset">
    ///   Смещение в массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="inputCount">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    /// <returns>Вычисляемое преобразование.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущий <see cref="T:System.Security.Cryptography.FromBase64Transform" /> объект уже был удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="inputBuffer" /> имеет недопустимое значение смещения.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="inputCount" /> имеет недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="inputOffset" /> выходит за пределы диапазона.
    ///    Этот параметр требует неотрицательным числом.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="inputBuffer" /> имеет значение <see langword="null" />.
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
      if (this._inputBuffer == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_Generic"));
      byte[] numArray1 = new byte[inputCount];
      int byteCount;
      if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
      {
        numArray1 = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
        byteCount = numArray1.Length;
      }
      else
      {
        Buffer.InternalBlockCopy((Array) inputBuffer, inputOffset, (Array) numArray1, 0, inputCount);
        byteCount = inputCount;
      }
      if (byteCount + this._inputIndex < 4)
      {
        this.Reset();
        return EmptyArray<byte>.Value;
      }
      int num = (byteCount + this._inputIndex) / 4;
      byte[] bytes = new byte[this._inputIndex + byteCount];
      Buffer.InternalBlockCopy((Array) this._inputBuffer, 0, (Array) bytes, 0, this._inputIndex);
      Buffer.InternalBlockCopy((Array) numArray1, 0, (Array) bytes, this._inputIndex, byteCount);
      this._inputIndex = (byteCount + this._inputIndex) % 4;
      Buffer.InternalBlockCopy((Array) numArray1, byteCount - this._inputIndex, (Array) this._inputBuffer, 0, this._inputIndex);
      byte[] numArray2 = Convert.FromBase64CharArray(Encoding.ASCII.GetChars(bytes, 0, 4 * num), 0, 4 * num);
      this.Reset();
      return numArray2;
    }

    private byte[] DiscardWhiteSpaces(byte[] inputBuffer, int inputOffset, int inputCount)
    {
      int num1 = 0;
      for (int index = 0; index < inputCount; ++index)
      {
        if (char.IsWhiteSpace((char) inputBuffer[inputOffset + index]))
          ++num1;
      }
      byte[] numArray = new byte[inputCount - num1];
      int num2 = 0;
      for (int index = 0; index < inputCount; ++index)
      {
        if (!char.IsWhiteSpace((char) inputBuffer[inputOffset + index]))
          numArray[num2++] = inputBuffer[inputOffset + index];
      }
      return numArray;
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Security.Cryptography.FromBase64Transform" />.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Reset()
    {
      this._inputIndex = 0;
    }

    /// <summary>
    ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.Security.Cryptography.FromBase64Transform" />.
    /// </summary>
    public void Clear()
    {
      this.Dispose();
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.Security.Cryptography.FromBase64Transform" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this._inputBuffer != null)
        Array.Clear((Array) this._inputBuffer, 0, this._inputBuffer.Length);
      this._inputBuffer = (byte[]) null;
      this._inputIndex = 0;
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые <see cref="T:System.Security.Cryptography.FromBase64Transform" />.
    /// </summary>
    ~FromBase64Transform()
    {
      this.Dispose(false);
    }
  }
}
