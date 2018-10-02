// Decompiled with JetBrains decompiler
// Type: System.Text.Decoder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  /// <summary>
  ///   Конвертирует последовательность закодированных байтов в набор символов.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Decoder
  {
    internal DecoderFallback m_fallback;
    [NonSerialized]
    internal DecoderFallbackBuffer m_fallbackBuffer;

    internal void SerializeDecoder(SerializationInfo info)
    {
      info.AddValue("m_fallback", (object) this.m_fallback);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.Decoder" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected Decoder()
    {
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Text.DecoderFallback" /> для текущего <see cref="T:System.Text.Decoder" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.DecoderFallback" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение в наборе операций <see langword="null " />(<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Невозможно назначить новое значение в наборе операций, поскольку текущий <see cref="T:System.Text.DecoderFallbackBuffer" /> объект содержит данные, которые еще не были декодировать.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public DecoderFallback Fallback
    {
      [__DynamicallyInvokable] get
      {
        return this.m_fallback;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (this.m_fallbackBuffer != null && this.m_fallbackBuffer.Remaining > 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_FallbackBufferNotEmpty"), nameof (value));
        this.m_fallback = value;
        this.m_fallbackBuffer = (DecoderFallbackBuffer) null;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Text.DecoderFallbackBuffer" /> объект, связанный с текущим <see cref="T:System.Text.Decoder" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.DecoderFallbackBuffer" />.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public DecoderFallbackBuffer FallbackBuffer
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_fallbackBuffer == null)
          this.m_fallbackBuffer = this.m_fallback == null ? DecoderFallback.ReplacementFallback.CreateFallbackBuffer() : this.m_fallback.CreateFallbackBuffer();
        return this.m_fallbackBuffer;
      }
    }

    internal bool InternalHasFallbackBuffer
    {
      get
      {
        return this.m_fallbackBuffer != null;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе, возвращает декодер в исходное состояние.
    /// </summary>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual void Reset()
    {
      byte[] bytes = new byte[0];
      char[] chars = new char[this.GetCharCount(bytes, 0, 0, true)];
      this.GetChars(bytes, 0, 0, chars, 0, true);
      if (this.m_fallbackBuffer == null)
        return;
      this.m_fallbackBuffer.Reset();
    }

    /// <summary>
    ///   При переопределении в производном классе вычисляет количество символов, полученных при декодировании последовательности байтов из заданного массива байтов.
    /// </summary>
    /// <param name="bytes">
    ///   Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <param name="index">Индекс первого декодируемого байта.</param>
    /// <param name="count">Число байтов для декодирования.</param>
    /// <returns>
    ///   Число символов, полученных при декодировании заданной последовательности байтов и байтов во внутреннем буфере.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="bytes" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> или <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон в <paramref name="bytes" />.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Decoder.Fallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract int GetCharCount(byte[] bytes, int index, int count);

    /// <summary>
    ///   При переопределении в производном классе вычисляет количество символов, полученных при декодировании последовательности байтов из заданного массива байтов.
    ///    Параметр указывает, следует ли очистить внутреннее состояние декодера после вычисления.
    /// </summary>
    /// <param name="bytes">
    ///   Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <param name="index">Индекс первого декодируемого байта.</param>
    /// <param name="count">Число байтов для декодирования.</param>
    /// <param name="flush">
    ///   <see langword="true" /> для имитации очистки внутреннего состояния кодировщика после расчета; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Число символов, полученных при декодировании заданной последовательности байтов и байтов во внутреннем буфере.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="bytes" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> или <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон в <paramref name="bytes" />.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Decoder.Fallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual int GetCharCount(byte[] bytes, int index, int count, bool flush)
    {
      return this.GetCharCount(bytes, index, count);
    }

    /// <summary>
    ///   При переопределении в производном классе вычисляет количество символов, полученных при декодировании последовательности байтов, начало которой задается указателем байтов.
    ///    Параметр указывает, следует ли очистить внутреннее состояние декодера после вычисления.
    /// </summary>
    /// <param name="bytes">Указатель на первый декодируемый байт.</param>
    /// <param name="count">Число байтов для декодирования.</param>
    /// <param name="flush">
    ///   <see langword="true" /> для имитации очистки внутреннего состояния кодировщика после расчета; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Число символов, полученных при декодировании заданной последовательности байтов и байтов во внутреннем буфере.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="bytes" /> — <see langword="null " />(<see langword="Nothing " />в Visual Basic .NET).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Decoder.Fallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe int GetCharCount(byte* bytes, int count, bool flush)
    {
      if ((IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      byte[] bytes1 = new byte[count];
      for (int index = 0; index < count; ++index)
        bytes1[index] = bytes[index];
      return this.GetCharCount(bytes1, 0, count);
    }

    /// <summary>
    ///   При переопределении в производном классе декодирует последовательность байтов из заданного массива байтов и все байты из внутреннего буфера в указанный массив символов.
    /// </summary>
    /// <param name="bytes">
    ///   Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <param name="byteIndex">
    ///   Индекс первого декодируемого байта.
    /// </param>
    /// <param name="byteCount">Число байтов для декодирования.</param>
    /// <param name="chars">
    ///   Массив символов, в который будет помещен результирующий набор символов.
    /// </param>
    /// <param name="charIndex">
    ///   Индекс, с которого начинается запись результирующего набора символов.
    /// </param>
    /// <returns>
    ///   Фактическое число символов, записанных в <paramref name="chars" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="bytes" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// 
    ///   -или-
    /// 
    ///   <paramref name="chars" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="byteIndex" /> или <paramref name="byteCount" /> или <paramref name="charIndex" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="byteindex" /> и <paramref name="byteCount" /> определяют допустимый диапазон в <paramref name="bytes" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="charIndex" /> не является допустимым индексом в <paramref name="chars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="chars" /> не имеет достаточную мощность от <paramref name="charIndex" /> до конца массива для размещения полученных символов.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Decoder.Fallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

    /// <summary>
    ///   При переопределении в производном классе декодирует последовательность байтов из заданного массива байтов и все байты из внутреннего буфера в указанный массив символов.
    ///    Параметр указывает, следует ли очистить внутреннее состояние декодера после преобразования.
    /// </summary>
    /// <param name="bytes">
    ///   Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <param name="byteIndex">
    ///   Индекс первого декодируемого байта.
    /// </param>
    /// <param name="byteCount">Число байтов для декодирования.</param>
    /// <param name="chars">
    ///   Массив символов, в который будет помещен результирующий набор символов.
    /// </param>
    /// <param name="charIndex">
    ///   Индекс, с которого начинается запись результирующего набора символов.
    /// </param>
    /// <param name="flush">
    ///   <see langword="true" /> Чтобы очистить внутреннее состояние декодера после преобразования; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Фактическое число символов, записанных в <paramref name="chars" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="bytes" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// 
    ///   -или-
    /// 
    ///   <paramref name="chars" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="byteIndex" /> или <paramref name="byteCount" /> или <paramref name="charIndex" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="byteindex" /> и <paramref name="byteCount" /> определяют допустимый диапазон в <paramref name="bytes" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="charIndex" /> не является допустимым индексом в <paramref name="chars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="chars" /> не имеет достаточную мощность от <paramref name="charIndex" /> до конца массива для размещения полученных символов.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Decoder.Fallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
    {
      return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
    }

    /// <summary>
    ///   При переопределении в производном классе декодирует последовательность байтов, начиная с заданного указателя байта и все байты из внутреннего буфера в набор символов, которые сохраняются начиная с заданного указателя символа.
    ///    Параметр указывает, следует ли очистить внутреннее состояние декодера после преобразования.
    /// </summary>
    /// <param name="bytes">Указатель на первый декодируемый байт.</param>
    /// <param name="byteCount">Число байтов для декодирования.</param>
    /// <param name="chars">
    ///   Указатель на положение, с которого начинается запись результирующего набора символов.
    /// </param>
    /// <param name="charCount">
    ///   Наибольшее количество символов для записи.
    /// </param>
    /// <param name="flush">
    ///   <see langword="true" /> Чтобы очистить внутреннее состояние декодера после преобразования; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Фактическое число символов, записанных в местоположение, которое задано параметром <paramref name="chars" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="bytes" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// 
    ///   -или-
    /// 
    ///   <paramref name="chars" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="byteCount" /> или <paramref name="charCount" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="charCount" /> меньше, чем количество символов.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Decoder.Fallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
    {
      if ((IntPtr) chars == IntPtr.Zero || (IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) chars == IntPtr.Zero ? nameof (chars) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (byteCount < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(byteCount < 0 ? nameof (byteCount) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      byte[] bytes1 = new byte[byteCount];
      for (int index = 0; index < byteCount; ++index)
        bytes1[index] = bytes[index];
      char[] chars1 = new char[charCount];
      int chars2 = this.GetChars(bytes1, 0, byteCount, chars1, 0, flush);
      if (chars2 < charCount)
        charCount = chars2;
      for (int index = 0; index < charCount; ++index)
        chars[index] = chars1[index];
      return charCount;
    }

    /// <summary>
    ///   Преобразует массив закодированных байтов в символы в кодировке UTF-16 и сохраняет результат в другом массиве символов.
    /// </summary>
    /// <param name="bytes">Преобразуемый массив байтов.</param>
    /// <param name="byteIndex">
    ///   Первый элемент преобразуемого массива <paramref name="bytes" />.
    /// </param>
    /// <param name="byteCount">
    ///   Число преобразуемых элементов <paramref name="bytes" />.
    /// </param>
    /// <param name="chars">
    ///   Массив для сохранения преобразованных символов.
    /// </param>
    /// <param name="charIndex">
    ///   Первый элемент массива <paramref name="chars" />, в котором сохраняются данные.
    /// </param>
    /// <param name="charCount">
    ///   Максимальное число элементов в <paramref name="chars" /> для использования при преобразовании.
    /// </param>
    /// <param name="flush">
    ///   Значение <see langword="true" /> указывает, что дальнейшие данные для преобразования отсутствуют. В противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="bytesUsed">
    ///   При возврате этот метод содержит число байтов, которые использовались при преобразовании.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="charsUsed">
    ///   При возврате этот метод содержит число символов из <paramref name="chars" />, которые были созданы при преобразовании.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="completed">
    ///   При возврате этот метод содержит значение <see langword="true" />, если все символы, заданные с помощью <paramref name="byteCount" />, были преобразованы. В противном случае — значение <see langword="false" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="chars" /> или <paramref name="bytes" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="charIndex" />, <paramref name="charCount" />, <paramref name="byteIndex" /> или <paramref name="byteCount" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Длина массива <paramref name="chars" /> минус <paramref name="charIndex" /> меньше <paramref name="charCount" />.
    /// 
    ///   -или-
    /// 
    ///   Длина массива <paramref name="bytes" /> минус <paramref name="byteIndex" /> меньше <paramref name="byteCount" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Выходной буфер слишком мал, чтобы содержать преобразованные входные данные.
    ///    Размер выходного буфера должен быть больше или равен размеру, указанному методом <see cref="Overload:System.Text.Decoder.GetCharCount" />.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Decoder.Fallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
    {
      if (bytes == null || chars == null)
        throw new ArgumentNullException(bytes == null ? nameof (bytes) : nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (byteIndex < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(byteIndex < 0 ? nameof (byteIndex) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? nameof (charIndex) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - byteIndex < byteCount)
        throw new ArgumentOutOfRangeException(nameof (bytes), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (chars.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException(nameof (chars), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      bytesUsed = byteCount;
      while (bytesUsed > 0)
      {
        if (this.GetCharCount(bytes, byteIndex, bytesUsed, flush) <= charCount)
        {
          charsUsed = this.GetChars(bytes, byteIndex, bytesUsed, chars, charIndex, flush);
          completed = bytesUsed == byteCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
          return;
        }
        flush = false;
        bytesUsed /= 2;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
    }

    /// <summary>
    ///   Преобразует буфер закодированных байтов в символы в кодировке UTF-16 и сохраняет результат в другом буфере.
    /// </summary>
    /// <param name="bytes">
    ///   Адрес буфера, содержащего последовательности байтов для преобразования.
    /// </param>
    /// <param name="byteCount">
    ///   Число байтов в <paramref name="bytes" />, которые требуется преобразовать.
    /// </param>
    /// <param name="chars">
    ///   Адрес буфера для хранения преобразованных символов.
    /// </param>
    /// <param name="charCount">
    ///   Максимальное число символов в <paramref name="chars" /> для использования при преобразовании.
    /// </param>
    /// <param name="flush">
    ///   Значение <see langword="true" /> указывает, что дальнейшие данные для преобразования отсутствуют. В противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="bytesUsed">
    ///   При возврате этот метод содержит число байтов, созданных при преобразовании.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="charsUsed">
    ///   При возврате этот метод содержит число символов из <paramref name="chars" />, которые использовались при преобразовании.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="completed">
    ///   При возврате этот метод содержит значение <see langword="true" />, если все символы, заданные с помощью <paramref name="byteCount" />, были преобразованы. В противном случае — значение <see langword="false" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="chars" /> или <paramref name="bytes" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="charCount" /> или <paramref name="byteCount" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Выходной буфер слишком мал, чтобы содержать преобразованные входные данные.
    ///    Размер выходного буфера должен быть больше или равен размеру, указанному методом <see cref="Overload:System.Text.Decoder.GetCharCount" />.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Decoder.Fallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
    {
      if ((IntPtr) chars == IntPtr.Zero || (IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) chars == IntPtr.Zero ? nameof (chars) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (byteCount < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(byteCount < 0 ? nameof (byteCount) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      bytesUsed = byteCount;
      while (bytesUsed > 0)
      {
        if (this.GetCharCount(bytes, bytesUsed, flush) <= charCount)
        {
          charsUsed = this.GetChars(bytes, bytesUsed, chars, charCount, flush);
          completed = bytesUsed == byteCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
          return;
        }
        flush = false;
        bytesUsed /= 2;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
    }
  }
}
