// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HashAlgorithm
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет базовый класс, из которого создаются все реализации криптографических хэш-алгоритмов.
  /// </summary>
  [ComVisible(true)]
  public abstract class HashAlgorithm : IDisposable, ICryptoTransform
  {
    /// <summary>Представляет размер вычисляемого хэш-кода в битах.</summary>
    protected int HashSizeValue;
    /// <summary>Представляет значение вычисляемого хэш-кода.</summary>
    protected internal byte[] HashValue;
    /// <summary>Представляет состояние процесса вычисления хэша.</summary>
    protected int State;
    private bool m_bDisposed;

    /// <summary>Возвращает размер вычисляемого хэш-кода в битах.</summary>
    /// <returns>Размер вычисляемого хэш-кода в битах.</returns>
    public virtual int HashSize
    {
      get
      {
        return this.HashSizeValue;
      }
    }

    /// <summary>Возвращает значение вычисляемого хэш-кода.</summary>
    /// <returns>Текущее значение вычисляемого хэш-кода.</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    ///   Свойство <see cref="F:System.Security.Cryptography.HashAlgorithm.HashValue" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже был удален.
    /// </exception>
    public virtual byte[] Hash
    {
      get
      {
        if (this.m_bDisposed)
          throw new ObjectDisposedException((string) null);
        if (this.State != 0)
          throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_HashNotYetFinalized"));
        return (byte[]) this.HashValue.Clone();
      }
    }

    /// <summary>
    ///   Создает экземпляр реализации по умолчанию хэш-алгоритма.
    /// </summary>
    /// <returns>
    ///   Новый <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> экземпляр, если параметры по умолчанию были изменены с помощью.
    /// </returns>
    public static HashAlgorithm Create()
    {
      return HashAlgorithm.Create("System.Security.Cryptography.HashAlgorithm");
    }

    /// <summary>
    ///   Создает экземпляр заданной реализации хэш-алгоритма.
    /// </summary>
    /// <param name="hashName">
    /// Используемая реализация хэш-алгоритма.
    ///  В следующей таблице представлены допустимые значения параметра <paramref name="hashName" /> и алгоритмы, с которыми они сопоставляются.
    /// 
    ///         Значение параметра
    /// 
    ///          Инструменты
    /// 
    ///         SHA
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />
    /// 
    ///         SHA1
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />
    /// 
    ///         System.Security.Cryptography.SHA1
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />
    /// 
    ///         System.Security.Cryptography.HashAlgorithm
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />
    /// 
    ///         MD5
    /// 
    ///         <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />
    /// 
    ///         System.Security.Cryptography.MD5
    /// 
    ///         <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />
    /// 
    ///         SHA256
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA256Managed" />
    /// 
    ///         SHA-256
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA256Managed" />
    /// 
    ///         System.Security.Cryptography.SHA256
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA256Managed" />
    /// 
    ///         SHA384
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA384Managed" />
    /// 
    ///         SHA-384
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA384Managed" />
    /// 
    ///         System.Security.Cryptography.SHA384
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA384Managed" />
    /// 
    ///         SHA512
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA512Managed" />
    /// 
    ///         SHA-512
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA512Managed" />
    /// 
    ///         System.Security.Cryptography.SHA512
    /// 
    ///         <see cref="T:System.Security.Cryptography.SHA512Managed" />
    ///       </param>
    /// <returns>
    ///   Новый экземпляр заданного хэш-алгоритма или значение <see langword="null" />, если <paramref name="hashName" /> не является допустимым хэш-алгоритмом.
    /// </returns>
    public static HashAlgorithm Create(string hashName)
    {
      return (HashAlgorithm) CryptoConfig.CreateFromName(hashName);
    }

    /// <summary>
    ///   Вычисляет хэш-значение для заданного объекта <see cref="T:System.IO.Stream" />.
    /// </summary>
    /// <param name="inputStream">
    ///   Входные данные, для которых вычисляется хэш-код.
    /// </param>
    /// <returns>Вычисляемый хэш-код.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже удален.
    /// </exception>
    public byte[] ComputeHash(Stream inputStream)
    {
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null);
      byte[] numArray1 = new byte[4096];
      int cbSize;
      do
      {
        cbSize = inputStream.Read(numArray1, 0, 4096);
        if (cbSize > 0)
          this.HashCore(numArray1, 0, cbSize);
      }
      while (cbSize > 0);
      this.HashValue = this.HashFinal();
      byte[] numArray2 = (byte[]) this.HashValue.Clone();
      this.Initialize();
      return numArray2;
    }

    /// <summary>
    ///   Вычисляет хэш-значение для заданного массива байтов.
    /// </summary>
    /// <param name="buffer">
    ///   Входные данные, для которых вычисляется хэш-код.
    /// </param>
    /// <returns>Вычисляемый хэш-код.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже удален.
    /// </exception>
    public byte[] ComputeHash(byte[] buffer)
    {
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null);
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      this.HashCore(buffer, 0, buffer.Length);
      this.HashValue = this.HashFinal();
      byte[] numArray = (byte[]) this.HashValue.Clone();
      this.Initialize();
      return numArray;
    }

    /// <summary>
    ///   Вычисляет хэш-значение для заданной области заданного массива байтов.
    /// </summary>
    /// <param name="buffer">
    ///   Входные данные, для которых вычисляется хэш-код.
    /// </param>
    /// <param name="offset">
    ///   Смещение в массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="count">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    /// <returns>Вычисляемый хэш-код.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="count" /> не является допустимым значением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="buffer" /> Недопустимая длина.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset" /> выходит за пределы диапазона.
    ///    Этот параметр требует неотрицательным числом.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже был удален.
    /// </exception>
    public byte[] ComputeHash(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0 || count > buffer.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      if (buffer.Length - count < offset)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null);
      this.HashCore(buffer, offset, count);
      this.HashValue = this.HashFinal();
      byte[] numArray = (byte[]) this.HashValue.Clone();
      this.Initialize();
      return numArray;
    }

    /// <summary>
    ///   Если переопределено в производном классе, возвращает размер входного блока.
    /// </summary>
    /// <returns>Размер входного блока.</returns>
    public virtual int InputBlockSize
    {
      get
      {
        return 1;
      }
    }

    /// <summary>
    ///   Если переопределено в производном классе, возвращает размер выходного блока.
    /// </summary>
    /// <returns>Размер выходного блока.</returns>
    public virtual int OutputBlockSize
    {
      get
      {
        return 1;
      }
    }

    /// <summary>
    ///   Если переопределено в производном классе, возвращает значение, указывающее, возможно ли преобразование нескольких блоков.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если возможно преобразование нескольких блоков; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool CanTransformMultipleBlocks
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
    public virtual bool CanReuseTransform
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///   Вычисляет хэш-значение для заданной области входного массива байтов и копирует указанную область входного массива байтов в заданную область выходного массива байтов.
    /// </summary>
    /// <param name="inputBuffer">
    ///   Входные данные, для которых вычисляется хэш-код.
    /// </param>
    /// <param name="inputOffset">
    ///   Смещение во входном массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="inputCount">
    ///   Число байтов во входном массиве для использования в качестве данных.
    /// </param>
    /// <param name="outputBuffer">
    ///   Копия части входного массива, используемого для вычисления хэш-кода.
    /// </param>
    /// <param name="outputOffset">
    ///   Смещение в выходном массиве байтов, начиная с которого следует записывать данные.
    /// </param>
    /// <returns>Число записанных байтов.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="inputCount" /> используется недопустимое значение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="inputBuffer" /> имеет недопустимую длину.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="inputBuffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="inputOffset" /> выходит за пределы диапазона.
    ///    Этот параметр требует неотрицательным числом.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже был удален.
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
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null);
      this.State = 1;
      this.HashCore(inputBuffer, inputOffset, inputCount);
      if (outputBuffer != null && (inputBuffer != outputBuffer || inputOffset != outputOffset))
        Buffer.BlockCopy((Array) inputBuffer, inputOffset, (Array) outputBuffer, outputOffset, inputCount);
      return inputCount;
    }

    /// <summary>
    ///   Вычисляет хэш-значение для заданной области заданного массива байтов.
    /// </summary>
    /// <param name="inputBuffer">
    ///   Входные данные, для которых вычисляется хэш-код.
    /// </param>
    /// <param name="inputOffset">
    ///   Смещение в массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="inputCount">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    /// <returns>
    ///   Массив, который является копией хэшируемой части входных данных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="inputCount" /> используется недопустимое значение.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="inputBuffer" /> имеет недопустимое значение смещения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="inputBuffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="inputOffset" /> выходит за пределы диапазона.
    ///    Этот параметр требует неотрицательным числом.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект уже был удален.
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
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null);
      this.HashCore(inputBuffer, inputOffset, inputCount);
      this.HashValue = this.HashFinal();
      byte[] numArray;
      if (inputCount != 0)
      {
        numArray = new byte[inputCount];
        Buffer.InternalBlockCopy((Array) inputBuffer, inputOffset, (Array) numArray, 0, inputCount);
      }
      else
        numArray = EmptyArray<byte>.Value;
      this.State = 0;
      return numArray;
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Security.Cryptography.HashAlgorithm" />.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые классом <see cref="T:System.Security.Cryptography.HashAlgorithm" />.
    /// </summary>
    public void Clear()
    {
      this.Dispose();
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.Security.Cryptography.HashAlgorithm" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this.HashValue != null)
        Array.Clear((Array) this.HashValue, 0, this.HashValue.Length);
      this.HashValue = (byte[]) null;
      this.m_bDisposed = true;
    }

    /// <summary>
    ///   Инициализирует реализацию класса <see cref="T:System.Security.Cryptography.HashAlgorithm" />.
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    ///   Если переопределено в производном классе, передает данные, записанные в объект, на вход хэш-алгоритма для вычисления хэша.
    /// </summary>
    /// <param name="array">
    ///   Входные данные, для которых вычисляется хэш-код.
    /// </param>
    /// <param name="ibStart">
    ///   Смещение в массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="cbSize">
    ///   Число байтов в массиве для использования в качестве данных.
    /// </param>
    protected abstract void HashCore(byte[] array, int ibStart, int cbSize);

    /// <summary>
    ///   Если переопределено в производном классе, завершает вычисление хэша после обработки последних данных криптографическим потоковым объектом.
    /// </summary>
    /// <returns>Вычисляемый хэш-код.</returns>
    protected abstract byte[] HashFinal();
  }
}
