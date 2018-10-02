// Decompiled with JetBrains decompiler
// Type: System.Text.Encoder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  /// <summary>
  ///   Конвертирует набор символов в последовательность байтов.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Encoder
  {
    internal EncoderFallback m_fallback;
    [NonSerialized]
    internal EncoderFallbackBuffer m_fallbackBuffer;

    internal void SerializeEncoder(SerializationInfo info)
    {
      info.AddValue("m_fallback", (object) this.m_fallback);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.Encoder" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected Encoder()
    {
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Text.EncoderFallback" /> для текущего <see cref="T:System.Text.Encoder" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.EncoderFallback" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение в наборе операций <see langword="null " />(<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Невозможно назначить новое значение в наборе операций, поскольку текущий <see cref="T:System.Text.EncoderFallbackBuffer" /> объект содержит данные, которые еще не были закодированы.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в статье Кодировка символов в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoder.Fallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public EncoderFallback Fallback
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
        this.m_fallbackBuffer = (EncoderFallbackBuffer) null;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Text.EncoderFallbackBuffer" /> объект, связанный с текущим <see cref="T:System.Text.Encoder" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.EncoderFallbackBuffer" />.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public EncoderFallbackBuffer FallbackBuffer
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_fallbackBuffer == null)
          this.m_fallbackBuffer = this.m_fallback == null ? EncoderFallback.ReplacementFallback.CreateFallbackBuffer() : this.m_fallback.CreateFallbackBuffer();
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
    ///   При переопределении в производном классе, возвращает кодировщик в исходное состояние.
    /// </summary>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual void Reset()
    {
      char[] chars = new char[0];
      byte[] bytes = new byte[this.GetByteCount(chars, 0, 0, true)];
      this.GetBytes(chars, 0, 0, bytes, 0, true);
      if (this.m_fallbackBuffer == null)
        return;
      this.m_fallbackBuffer.Reset();
    }

    /// <summary>
    ///   При переопределении в производном классе вычисляет количество байтов, полученных при кодировании набора символов из указанного массива символов.
    ///    Параметр указывает, следует ли очистить внутреннее состояние кодировщика после расчета.
    /// </summary>
    /// <param name="chars">
    ///   Массив символов, содержащий набор кодируемых символов.
    /// </param>
    /// <param name="index">Индекс первого кодируемого символа.</param>
    /// <param name="count">Число кодируемых символов.</param>
    /// <param name="flush">
    ///   <see langword="true" /> для имитации очистки внутреннего состояния кодировщика после расчета; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Число байтов, полученных при кодировании заданных символов и знаков внутреннего буфера.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="chars" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон в <paramref name="chars" />.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в статье Кодировка символов в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoder.Fallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract int GetByteCount(char[] chars, int index, int count, bool flush);

    /// <summary>
    ///   При переопределении в производном классе вычисляет количество байтов, полученных при кодировании набора символов, начиная с заданного указателя символа.
    ///    Параметр указывает, следует ли очистить внутреннее состояние кодировщика после расчета.
    /// </summary>
    /// <param name="chars">Указатель на первый кодируемый символ.</param>
    /// <param name="count">Число кодируемых символов.</param>
    /// <param name="flush">
    ///   <see langword="true" /> для имитации очистки внутреннего состояния кодировщика после расчета; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Число байтов, полученных при кодировании заданных символов и знаков внутреннего буфера.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="chars" /> — <see langword="null" /> (<see langword="Nothing" /> в Visual Basic .NET).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в статье Кодировка символов в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoder.Fallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe int GetByteCount(char* chars, int count, bool flush)
    {
      if ((IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException(nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      char[] chars1 = new char[count];
      for (int index = 0; index < count; ++index)
        chars1[index] = chars[index];
      return this.GetByteCount(chars1, 0, count, flush);
    }

    /// <summary>
    ///   При переопределении в производном классе кодирует набор символов из указанного массива символов и все символы в внутреннего буфера в указанный массив байтов.
    ///    Параметр указывает, следует ли очистить внутреннее состояние кодировщика после преобразования.
    /// </summary>
    /// <param name="chars">
    ///   Массив символов, содержащий набор кодируемых символов.
    /// </param>
    /// <param name="charIndex">
    ///   Индекс первого кодируемого символа.
    /// </param>
    /// <param name="charCount">Число кодируемых символов.</param>
    /// <param name="bytes">
    ///   Массив байтов, в который будет помещена результирующая последовательность байтов.
    /// </param>
    /// <param name="byteIndex">
    ///   Индекс, с которого начинается запись результирующей последовательности байтов.
    /// </param>
    /// <param name="flush">
    ///   <see langword="true" /> Чтобы очистить внутреннее состояние кодировщика после преобразования; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Фактическое число байтов, записанных в <paramref name="bytes" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="chars" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// 
    ///   -или-
    /// 
    ///   <paramref name="bytes" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="charIndex" /> или <paramref name="charCount" /> или <paramref name="byteIndex" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="charIndex" /> и <paramref name="charCount" /> определяют допустимый диапазон в <paramref name="chars" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="byteIndex" /> не является допустимым индексом в <paramref name="bytes" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="bytes" /> не имеет достаточную мощность от <paramref name="byteIndex" /> до конца массива для размещения полученных байтов.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в статье Кодировка символов в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoder.Fallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush);

    /// <summary>
    ///   При переопределении в производном классе кодирует набор символов, начиная с заданного указателя символа и все символы во внутреннем буфере, в последовательность байтов, которые сохраняются начиная с заданного указателя байта.
    ///    Параметр указывает, следует ли очистить внутреннее состояние кодировщика после преобразования.
    /// </summary>
    /// <param name="chars">Указатель на первый кодируемый символ.</param>
    /// <param name="charCount">Число кодируемых символов.</param>
    /// <param name="bytes">
    ///   Указатель на положение, с которого начинается запись результирующей последовательности байтов.
    /// </param>
    /// <param name="byteCount">
    ///   Максимальное число байтов для записи.
    /// </param>
    /// <param name="flush">
    ///   <see langword="true" /> Чтобы очистить внутреннее состояние кодировщика после преобразования; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Фактическое число байтов, записанных в местоположение, которое задано параметром <paramref name="bytes" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="chars" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// 
    ///   -или-
    /// 
    ///   <paramref name="bytes" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="charCount" /> или <paramref name="byteCount" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="byteCount" /> меньше, чем количество байтов.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в статье Кодировка символов в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoder.Fallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? nameof (bytes) : nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? nameof (charCount) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      char[] chars1 = new char[charCount];
      for (int index = 0; index < charCount; ++index)
        chars1[index] = chars[index];
      byte[] bytes1 = new byte[byteCount];
      int bytes2 = this.GetBytes(chars1, 0, charCount, bytes1, 0, flush);
      if (bytes2 < byteCount)
        byteCount = bytes2;
      for (int index = 0; index < byteCount; ++index)
        bytes[index] = bytes1[index];
      return byteCount;
    }

    /// <summary>
    ///   Преобразует массив символов Юникода в закодированную последовательность байтов и сохраняет результат в массиве байтов.
    /// </summary>
    /// <param name="chars">Массив символов для преобразования.</param>
    /// <param name="charIndex">
    ///   Первый элемент преобразуемого массива <paramref name="chars" />.
    /// </param>
    /// <param name="charCount">
    ///   Число преобразуемых элементов <paramref name="chars" />.
    /// </param>
    /// <param name="bytes">
    ///   Массив, где хранятся преобразованные байты.
    /// </param>
    /// <param name="byteIndex">
    ///   Первый элемент массива <paramref name="bytes" />, в котором сохраняются данные.
    /// </param>
    /// <param name="byteCount">
    ///   Максимальное число элементов в <paramref name="bytes" /> для использования при преобразовании.
    /// </param>
    /// <param name="flush">
    ///   Значение <see langword="true" /> указывает, что дальнейшие данные для преобразования отсутствуют. В противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="charsUsed">
    ///   При возврате этот метод содержит число символов из <paramref name="chars" />, которые использовались при преобразовании.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="bytesUsed">
    ///   При возврате этот метод содержит число байтов, созданных при преобразовании.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="completed">
    ///   При возврате этот метод содержит значение <see langword="true" />, если все символы, заданные с помощью <paramref name="charCount" />, были преобразованы. В противном случае — значение <see langword="false" />.
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
    ///    Размер выходного буфера должен быть больше или равен размеру, указанному методом <see cref="Overload:System.Text.Encoder.GetByteCount" />.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошла отработка отказа (см. полное объяснение в статье Кодировка символов в .NET Framework)
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoder.Fallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
    {
      if (chars == null || bytes == null)
        throw new ArgumentNullException(chars == null ? nameof (chars) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? nameof (charIndex) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (byteIndex < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(byteIndex < 0 ? nameof (byteIndex) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (chars.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException(nameof (chars), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length - byteIndex < byteCount)
        throw new ArgumentOutOfRangeException(nameof (bytes), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      charsUsed = charCount;
      while (charsUsed > 0)
      {
        if (this.GetByteCount(chars, charIndex, charsUsed, flush) <= byteCount)
        {
          bytesUsed = this.GetBytes(chars, charIndex, charsUsed, bytes, byteIndex, flush);
          completed = charsUsed == charCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
          return;
        }
        flush = false;
        charsUsed /= 2;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
    }

    /// <summary>
    ///   Преобразует буфер символов Юникода в закодированную последовательность байтов и сохраняет результат в другом буфере.
    /// </summary>
    /// <param name="chars">
    ///   Адрес строки символов в кодировке UTF-16 для преобразования.
    /// </param>
    /// <param name="charCount">
    ///   Число символов в подстроке <paramref name="chars" />, подлежащих преобразованию.
    /// </param>
    /// <param name="bytes">
    ///   Адрес буфера для хранения преобразованных байтов.
    /// </param>
    /// <param name="byteCount">
    ///   Максимальное число байтов в <paramref name="bytes" /> для использования при преобразовании.
    /// </param>
    /// <param name="flush">
    ///   Значение <see langword="true" /> указывает, что дальнейшие данные для преобразования отсутствуют. В противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="charsUsed">
    ///   При возврате этот метод содержит число символов из <paramref name="chars" />, которые использовались при преобразовании.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="bytesUsed">
    ///   При возврате этот метод содержит число байтов, которые использовались при преобразовании.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="completed">
    ///   При возврате этот метод содержит значение <see langword="true" />, если все символы, заданные с помощью <paramref name="charCount" />, были преобразованы. В противном случае — значение <see langword="false" />.
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
    ///    Размер выходного буфера должен быть больше или равен размеру, указанному методом <see cref="Overload:System.Text.Encoder.GetByteCount" />.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошла отработка отказа (см. полное объяснение в статье Кодировка символов в .NET Framework)
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoder.Fallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public virtual unsafe void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? nameof (bytes) : nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? nameof (charCount) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      charsUsed = charCount;
      while (charsUsed > 0)
      {
        if (this.GetByteCount(chars, charsUsed, flush) <= byteCount)
        {
          bytesUsed = this.GetBytes(chars, charsUsed, bytes, byteCount, flush);
          completed = charsUsed == charCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
          return;
        }
        flush = false;
        charsUsed /= 2;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
    }
  }
}
