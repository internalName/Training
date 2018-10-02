// Decompiled with JetBrains decompiler
// Type: System.Text.UTF32Encoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Text
{
  /// <summary>
  ///   Представляет кодировку символов Юникода в формате UTF-32.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class UTF32Encoding : Encoding
  {
    private bool emitUTF32ByteOrderMark;
    private bool isThrowException;
    private bool bigEndian;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.UTF32Encoding" />.
    /// </summary>
    [__DynamicallyInvokable]
    public UTF32Encoding()
      : this(false, true, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.UTF32Encoding" />.
    ///    Параметры указывают, следует ли использовать обратный порядок байтов и возвращает ли метод <see cref="M:System.Text.UTF32Encoding.GetPreamble" /> метку порядка байтов Юникода.
    /// </summary>
    /// <param name="bigEndian">
    ///   Значение <see langword="true" /> соответствует использованию обратного порядка байтов (самый старший байт располагается на первом месте); значение <see langword="false" /> соответствует использованию прямого порядка байтов (на первом месте находится самый младший байт).
    /// </param>
    /// <param name="byteOrderMark">
    ///   Значение <see langword="true" /> указывает, что предоставляется метка порядка байтов Юникода; в противном случае — значение <see langword="false" />.
    /// </param>
    [__DynamicallyInvokable]
    public UTF32Encoding(bool bigEndian, bool byteOrderMark)
      : this(bigEndian, byteOrderMark, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.UTF32Encoding" />.
    ///    Параметры указывают, следует ли использовать обратный порядок байтов, должна ли предоставляться метка порядка байтов Юникода и следует ли создавать исключение при обнаружении недопустимой кодировки.
    /// </summary>
    /// <param name="bigEndian">
    ///   Значение <see langword="true" /> соответствует использованию обратного порядка байтов (самый старший байт располагается на первом месте); значение <see langword="false" /> соответствует использованию прямого порядка байтов (на первом месте находится самый младший байт).
    /// </param>
    /// <param name="byteOrderMark">
    ///   Значение <see langword="true" /> указывает, что предоставляется метка порядка байтов Юникода; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="throwOnInvalidCharacters">
    ///   Значение <see langword="true" /> указывает, что следует создавать исключение при обнаружении недопустимой кодировки; в противном случае — значение <see langword="false" />.
    /// </param>
    [__DynamicallyInvokable]
    public UTF32Encoding(bool bigEndian, bool byteOrderMark, bool throwOnInvalidCharacters)
      : base(bigEndian ? 12001 : 12000)
    {
      this.bigEndian = bigEndian;
      this.emitUTF32ByteOrderMark = byteOrderMark;
      this.isThrowException = throwOnInvalidCharacters;
      if (!this.isThrowException)
        return;
      this.SetDefaultFallbacks();
    }

    internal override void SetDefaultFallbacks()
    {
      if (this.isThrowException)
      {
        this.encoderFallback = EncoderFallback.ExceptionFallback;
        this.decoderFallback = DecoderFallback.ExceptionFallback;
      }
      else
      {
        this.encoderFallback = (EncoderFallback) new EncoderReplacementFallback("�");
        this.decoderFallback = (DecoderFallback) new DecoderReplacementFallback("�");
      }
    }

    /// <summary>
    ///   Вычисляет число байтов, полученных при кодировании набора символов из указанного массива символов.
    /// </summary>
    /// <param name="chars">
    ///   Массив символов, содержащий набор кодируемых символов.
    /// </param>
    /// <param name="index">Индекс первого кодируемого символа.</param>
    /// <param name="count">Число кодируемых символов.</param>
    /// <returns>
    ///   Число байтов, полученных при кодировании заданных символов.
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
    /// 
    ///   -или-
    /// 
    ///   Результирующее число байтов больше максимального количества, которое можно вернуть как целочисленное значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Обнаружение ошибок включено, и <paramref name="chars" /> содержит недопустимую последовательность символов.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetByteCount(char[] chars, int index, int count)
    {
      if (chars == null)
        throw new ArgumentNullException(nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (chars.Length - index < count)
        throw new ArgumentOutOfRangeException(nameof (chars), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (chars.Length == 0)
        return 0;
      fixed (char* chPtr = chars)
        return this.GetByteCount(chPtr + index, count, (EncoderNLS) null);
    }

    /// <summary>
    ///   Вычисляет количество байтов, полученных при кодировании символов в заданном объекте <see cref="T:System.String" />.
    /// </summary>
    /// <param name="s">
    ///   Объект <see cref="T:System.String" />, содержащий кодируемый набор символов.
    /// </param>
    /// <returns>
    ///   Число байтов, полученных при кодировании заданных символов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Результирующее количество байтов больше максимального количества, которое может возвращаться как целое число.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Обнаружение ошибок включено, и <paramref name="s" /> содержит недопустимую последовательность символов.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetByteCount(string s)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      string str = s;
      char* chars = (char*) str;
      if ((IntPtr) chars != IntPtr.Zero)
        chars += RuntimeHelpers.OffsetToStringData;
      return this.GetByteCount(chars, s.Length, (EncoderNLS) null);
    }

    /// <summary>
    ///   Вычисляет число байтов, полученных при кодировании набора символов начиная с заданного указателя символа.
    /// </summary>
    /// <param name="chars">Указатель на первый кодируемый символ.</param>
    /// <param name="count">Число кодируемых символов.</param>
    /// <returns>
    ///   Число байтов, полученных при кодировании заданных символов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="chars" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Результирующее число байтов больше максимального количества, которое можно вернуть как целочисленное значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Обнаружение ошибок включено, и <paramref name="chars" /> содержит недопустимую последовательность символов.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public override unsafe int GetByteCount(char* chars, int count)
    {
      if ((IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException(nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetByteCount(chars, count, (EncoderNLS) null);
    }

    /// <summary>
    ///   Кодирует набор символов из заданного объекта <see cref="T:System.String" /> в указанный массив байтов.
    /// </summary>
    /// <param name="s">
    ///   Объект <see cref="T:System.String" />, содержащий кодируемый набор символов.
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
    /// <returns>
    ///   Фактическое число байтов, записанных в <paramref name="bytes" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
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
    ///   Обнаружение ошибок включено, и <paramref name="s" /> содержит недопустимую последовательность символов.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="bytes" /> не имеет достаточную мощность от <paramref name="byteIndex" /> до конца массива для размещения полученных байтов.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      if (s == null || bytes == null)
        throw new ArgumentNullException(s == null ? nameof (s) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? nameof (charIndex) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (s.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException(nameof (s), Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      if (byteIndex < 0 || byteIndex > bytes.Length)
        throw new ArgumentOutOfRangeException(nameof (byteIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      int byteCount = bytes.Length - byteIndex;
      if (bytes.Length == 0)
        bytes = new byte[1];
      string str = s;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      fixed (byte* numPtr = bytes)
        return this.GetBytes(chPtr + charIndex, charCount, numPtr + byteIndex, byteCount, (EncoderNLS) null);
    }

    /// <summary>
    ///   Кодирует набор символов из заданного массива символов в указанный массив байтов.
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
    /// <returns>
    ///   Фактическое число байтов, записанных в <paramref name="bytes" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="chars" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
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
    ///   Обнаружение ошибок включено, и <paramref name="chars" /> содержит недопустимую последовательность символов.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="bytes" /> не имеет достаточную мощность от <paramref name="byteIndex" /> до конца массива для размещения полученных байтов.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      if (chars == null || bytes == null)
        throw new ArgumentNullException(chars == null ? nameof (chars) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? nameof (charIndex) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (chars.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException(nameof (chars), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (byteIndex < 0 || byteIndex > bytes.Length)
        throw new ArgumentOutOfRangeException(nameof (byteIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (chars.Length == 0)
        return 0;
      int byteCount = bytes.Length - byteIndex;
      if (bytes.Length == 0)
        bytes = new byte[1];
      fixed (char* chPtr = chars)
        fixed (byte* numPtr = bytes)
          return this.GetBytes(chPtr + charIndex, charCount, numPtr + byteIndex, byteCount, (EncoderNLS) null);
    }

    /// <summary>
    ///   Кодирует набор символов, начало которого задается указателем символа, в последовательность байтов, которые сохраняются начиная с заданного указателя байта.
    /// </summary>
    /// <param name="chars">Указатель на первый кодируемый символ.</param>
    /// <param name="charCount">Число кодируемых символов.</param>
    /// <param name="bytes">
    ///   Указатель на положение, с которого начинается запись результирующей последовательности байтов.
    /// </param>
    /// <param name="byteCount">
    ///   Максимальное число байтов для записи.
    /// </param>
    /// <returns>
    ///   Фактическое число байтов, записанных в местоположение, которое задано параметром <paramref name="bytes" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="chars" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="charCount" /> или <paramref name="byteCount" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Обнаружение ошибок включено, и <paramref name="chars" /> содержит недопустимую последовательность символов.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="byteCount" /> меньше, чем количество байтов.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? nameof (bytes) : nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? nameof (charCount) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetBytes(chars, charCount, bytes, byteCount, (EncoderNLS) null);
    }

    /// <summary>
    ///   Вычисляет количество символов, полученных при декодировании последовательности байтов из заданного массива байтов.
    /// </summary>
    /// <param name="bytes">
    ///   Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <param name="index">Индекс первого декодируемого байта.</param>
    /// <param name="count">Число байтов для декодирования.</param>
    /// <returns>
    ///   Число символов, полученных при декодировании заданной последовательности байтов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон в <paramref name="bytes" />.
    /// 
    ///   -или-
    /// 
    ///   Результирующее число байтов больше максимального количества, которое можно вернуть как целочисленное значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Обнаружение ошибок включено, и параметр <paramref name="bytes" /> содержит недопустимую последовательность байтов.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetCharCount(byte[] bytes, int index, int count)
    {
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - index < count)
        throw new ArgumentOutOfRangeException(nameof (bytes), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length == 0)
        return 0;
      fixed (byte* numPtr = bytes)
        return this.GetCharCount(numPtr + index, count, (DecoderNLS) null);
    }

    /// <summary>
    ///   Вычисляет количество символов, полученных при декодировании последовательности байтов начиная с заданного указателя байта.
    /// </summary>
    /// <param name="bytes">Указатель на первый декодируемый байт.</param>
    /// <param name="count">Число байтов для декодирования.</param>
    /// <returns>
    ///   Число символов, полученных при декодировании заданной последовательности байтов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Результирующее число байтов больше максимального количества, которое можно вернуть как целочисленное значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Обнаружение ошибок включено, и параметр <paramref name="bytes" /> содержит недопустимую последовательность байтов.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public override unsafe int GetCharCount(byte* bytes, int count)
    {
      if ((IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetCharCount(bytes, count, (DecoderNLS) null);
    }

    /// <summary>
    ///   Декодирует последовательность байтов из заданного массива байтов в указанный массив символов.
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
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="chars" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="byteIndex" /> или <paramref name="byteCount" /> или <paramref name="charIndex" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Параметры <paramref name="byteindex" /> и <paramref name="byteCount" /> не указывают допустимый диапазон в <paramref name="bytes" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="charIndex" /> не является допустимым индексом в <paramref name="chars" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Обнаружение ошибок включено, и параметр <paramref name="bytes" /> содержит недопустимую последовательность байтов.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="chars" /> не имеет достаточную мощность от <paramref name="charIndex" /> до конца массива для размещения полученных символов.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
    {
      if (bytes == null || chars == null)
        throw new ArgumentNullException(bytes == null ? nameof (bytes) : nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (byteIndex < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(byteIndex < 0 ? nameof (byteIndex) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - byteIndex < byteCount)
        throw new ArgumentOutOfRangeException(nameof (bytes), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (charIndex < 0 || charIndex > chars.Length)
        throw new ArgumentOutOfRangeException(nameof (charIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (bytes.Length == 0)
        return 0;
      int charCount = chars.Length - charIndex;
      if (chars.Length == 0)
        chars = new char[1];
      fixed (byte* numPtr = bytes)
        fixed (char* chPtr = chars)
          return this.GetChars(numPtr + byteIndex, byteCount, chPtr + charIndex, charCount, (DecoderNLS) null);
    }

    /// <summary>
    ///   Декодирует последовательность байтов, начало которой задается указателем байта, в набор символов, которые сохраняются начиная с заданного указателя символа.
    /// </summary>
    /// <param name="bytes">Указатель на первый декодируемый байт.</param>
    /// <param name="byteCount">Число байтов для декодирования.</param>
    /// <param name="chars">
    ///   Указатель на положение, с которого начинается запись результирующего набора символов.
    /// </param>
    /// <param name="charCount">
    ///   Наибольшее количество символов для записи.
    /// </param>
    /// <returns>
    ///   Фактическое число символов, записанных в местоположение, указанное с помощью параметра <paramref name="chars" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="chars" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="byteCount" /> или <paramref name="charCount" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Обнаружение ошибок включено, и параметр <paramref name="bytes" /> содержит недопустимую последовательность байтов.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="charCount" /> меньше, чем количество символов.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? nameof (bytes) : nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? nameof (charCount) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return this.GetChars(bytes, byteCount, chars, charCount, (DecoderNLS) null);
    }

    /// <summary>
    ///   Декодирует диапазон байтов из массива байтов в строку.
    /// </summary>
    /// <param name="bytes">
    ///   Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <param name="index">Индекс первого декодируемого байта.</param>
    /// <param name="count">Число байтов для декодирования.</param>
    /// <returns>
    ///   Строка, содержащая результаты декодирования заданной последовательности байтов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Параметры <paramref name="index" /> и <paramref name="count" /> не указывают допустимый диапазон в <paramref name="bytes" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Обнаружение ошибок включено, и параметр <paramref name="bytes" /> содержит недопустимую последовательность байтов.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошла резервной (см. Кодировки в .NET Framework Подробное описание)
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe string GetString(byte[] bytes, int index, int count)
    {
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - index < count)
        throw new ArgumentOutOfRangeException(nameof (bytes), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length == 0)
        return string.Empty;
      fixed (byte* numPtr = bytes)
        return string.CreateStringFromEncoding(numPtr + index, count, (Encoding) this);
    }

    [SecurityCritical]
    internal override unsafe int GetByteCount(char* chars, int count, EncoderNLS encoder)
    {
      char* charEnd = chars + count;
      char* charStart = chars;
      int num = 0;
      char ch1 = char.MinValue;
      EncoderFallbackBuffer fallbackBuffer;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        fallbackBuffer = encoder.FallbackBuffer;
        if (fallbackBuffer.Remaining > 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
      }
      else
        fallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
      fallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
      while (true)
      {
        char ch2;
        while ((ch2 = fallbackBuffer.InternalGetNextChar()) != char.MinValue || chars < charEnd)
        {
          if (ch2 == char.MinValue)
          {
            ch2 = *chars;
            chars += 2;
          }
          if (ch1 != char.MinValue)
          {
            if (char.IsLowSurrogate(ch2))
            {
              ch1 = char.MinValue;
              num += 4;
            }
            else
            {
              chars -= 2;
              fallbackBuffer.InternalFallback(ch1, ref chars);
              ch1 = char.MinValue;
            }
          }
          else if (char.IsHighSurrogate(ch2))
            ch1 = ch2;
          else if (char.IsLowSurrogate(ch2))
            fallbackBuffer.InternalFallback(ch2, ref chars);
          else
            num += 4;
        }
        if ((encoder == null || encoder.MustFlush) && ch1 > char.MinValue)
        {
          fallbackBuffer.InternalFallback(ch1, ref chars);
          ch1 = char.MinValue;
        }
        else
          break;
      }
      if (num < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return num;
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
    {
      char* charStart = chars;
      char* charEnd = chars + charCount;
      byte* numPtr1 = bytes;
      byte* numPtr2 = bytes + byteCount;
      char ch1 = char.MinValue;
      EncoderFallbackBuffer fallbackBuffer;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        fallbackBuffer = encoder.FallbackBuffer;
        if (encoder.m_throwOnOverflow && fallbackBuffer.Remaining > 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
      }
      else
        fallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
      fallbackBuffer.InternalInitialize(charStart, charEnd, encoder, true);
      while (true)
      {
        char ch2;
        while ((ch2 = fallbackBuffer.InternalGetNextChar()) != char.MinValue || chars < charEnd)
        {
          if (ch2 == char.MinValue)
          {
            ch2 = *chars;
            chars += 2;
          }
          if (ch1 != char.MinValue)
          {
            if (char.IsLowSurrogate(ch2))
            {
              uint surrogate = this.GetSurrogate(ch1, ch2);
              ch1 = char.MinValue;
              if (bytes + 3 >= numPtr2)
              {
                if (fallbackBuffer.bFallingBack)
                {
                  fallbackBuffer.MovePrevious();
                  fallbackBuffer.MovePrevious();
                }
                else
                  chars -= 2;
                this.ThrowBytesOverflow(encoder, bytes == numPtr1);
                ch1 = char.MinValue;
                break;
              }
              if (this.bigEndian)
              {
                *bytes++ = (byte) 0;
                *bytes++ = (byte) (surrogate >> 16);
                *bytes++ = (byte) (surrogate >> 8);
                *bytes++ = (byte) surrogate;
              }
              else
              {
                *bytes++ = (byte) surrogate;
                *bytes++ = (byte) (surrogate >> 8);
                *bytes++ = (byte) (surrogate >> 16);
                *bytes++ = (byte) 0;
              }
            }
            else
            {
              chars -= 2;
              fallbackBuffer.InternalFallback(ch1, ref chars);
              ch1 = char.MinValue;
            }
          }
          else if (char.IsHighSurrogate(ch2))
            ch1 = ch2;
          else if (char.IsLowSurrogate(ch2))
          {
            fallbackBuffer.InternalFallback(ch2, ref chars);
          }
          else
          {
            if (bytes + 3 >= numPtr2)
            {
              if (fallbackBuffer.bFallingBack)
                fallbackBuffer.MovePrevious();
              else
                chars -= 2;
              this.ThrowBytesOverflow(encoder, bytes == numPtr1);
              break;
            }
            if (this.bigEndian)
            {
              *bytes++ = (byte) 0;
              *bytes++ = (byte) 0;
              *bytes++ = (byte) ((uint) ch2 >> 8);
              *bytes++ = (byte) ch2;
            }
            else
            {
              *bytes++ = (byte) ch2;
              *bytes++ = (byte) ((uint) ch2 >> 8);
              *bytes++ = (byte) 0;
              *bytes++ = (byte) 0;
            }
          }
        }
        if ((encoder == null || encoder.MustFlush) && ch1 > char.MinValue)
        {
          fallbackBuffer.InternalFallback(ch1, ref chars);
          ch1 = char.MinValue;
        }
        else
          break;
      }
      if (encoder != null)
      {
        encoder.charLeftOver = ch1;
        encoder.m_charsUsed = (int) (chars - charStart);
      }
      return (int) (bytes - numPtr1);
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
    {
      UTF32Encoding.UTF32Decoder utF32Decoder = (UTF32Encoding.UTF32Decoder) baseDecoder;
      int num1 = 0;
      byte* numPtr = bytes + count;
      byte* byteStart = bytes;
      int length = 0;
      uint num2 = 0;
      DecoderFallbackBuffer fallbackBuffer;
      if (utF32Decoder != null)
      {
        length = utF32Decoder.readByteCount;
        num2 = (uint) utF32Decoder.iChar;
        fallbackBuffer = utF32Decoder.FallbackBuffer;
      }
      else
        fallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
      fallbackBuffer.InternalInitialize(byteStart, (char*) null);
      while (bytes < numPtr && num1 >= 0)
      {
        num2 = !this.bigEndian ? (num2 >> 8) + ((uint) *bytes++ << 24) : (num2 << 8) + (uint) *bytes++;
        ++length;
        if (length >= 4)
        {
          length = 0;
          if (num2 > 1114111U || num2 >= 55296U && num2 <= 57343U)
          {
            byte[] bytes1;
            if (this.bigEndian)
              bytes1 = new byte[4]
              {
                (byte) (num2 >> 24),
                (byte) (num2 >> 16),
                (byte) (num2 >> 8),
                (byte) num2
              };
            else
              bytes1 = new byte[4]
              {
                (byte) num2,
                (byte) (num2 >> 8),
                (byte) (num2 >> 16),
                (byte) (num2 >> 24)
              };
            num1 += fallbackBuffer.InternalFallback(bytes1, bytes);
            num2 = 0U;
          }
          else
          {
            if (num2 >= 65536U)
              ++num1;
            ++num1;
            num2 = 0U;
          }
        }
      }
      if (length > 0 && (utF32Decoder == null || utF32Decoder.MustFlush))
      {
        byte[] bytes1 = new byte[length];
        if (this.bigEndian)
        {
          while (length > 0)
          {
            bytes1[--length] = (byte) num2;
            num2 >>= 8;
          }
        }
        else
        {
          while (length > 0)
          {
            bytes1[--length] = (byte) (num2 >> 24);
            num2 <<= 8;
          }
        }
        num1 += fallbackBuffer.InternalFallback(bytes1, bytes);
      }
      if (num1 < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
    {
      UTF32Encoding.UTF32Decoder utF32Decoder = (UTF32Encoding.UTF32Decoder) baseDecoder;
      char* chPtr1 = chars;
      char* chPtr2 = chars + charCount;
      byte* numPtr1 = bytes;
      byte* numPtr2 = bytes + byteCount;
      int length = 0;
      uint iChar = 0;
      DecoderFallbackBuffer fallbackBuffer;
      if (utF32Decoder != null)
      {
        length = utF32Decoder.readByteCount;
        iChar = (uint) utF32Decoder.iChar;
        fallbackBuffer = baseDecoder.FallbackBuffer;
      }
      else
        fallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
      fallbackBuffer.InternalInitialize(bytes, chars + charCount);
      while (bytes < numPtr2)
      {
        iChar = !this.bigEndian ? (iChar >> 8) + ((uint) *bytes++ << 24) : (iChar << 8) + (uint) *bytes++;
        ++length;
        if (length >= 4)
        {
          length = 0;
          if (iChar > 1114111U || iChar >= 55296U && iChar <= 57343U)
          {
            byte[] bytes1;
            if (this.bigEndian)
              bytes1 = new byte[4]
              {
                (byte) (iChar >> 24),
                (byte) (iChar >> 16),
                (byte) (iChar >> 8),
                (byte) iChar
              };
            else
              bytes1 = new byte[4]
              {
                (byte) iChar,
                (byte) (iChar >> 8),
                (byte) (iChar >> 16),
                (byte) (iChar >> 24)
              };
            if (!fallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
            {
              bytes -= 4;
              iChar = 0U;
              fallbackBuffer.InternalReset();
              this.ThrowCharsOverflow((DecoderNLS) utF32Decoder, chars == chPtr1);
              break;
            }
            iChar = 0U;
          }
          else
          {
            if (iChar >= 65536U)
            {
              if ((UIntPtr) chars >= (UIntPtr) chPtr2 - new UIntPtr(2))
              {
                bytes -= 4;
                iChar = 0U;
                this.ThrowCharsOverflow((DecoderNLS) utF32Decoder, chars == chPtr1);
                break;
              }
              *chars++ = this.GetHighSurrogate(iChar);
              iChar = (uint) this.GetLowSurrogate(iChar);
            }
            else if (chars >= chPtr2)
            {
              bytes -= 4;
              iChar = 0U;
              this.ThrowCharsOverflow((DecoderNLS) utF32Decoder, chars == chPtr1);
              break;
            }
            *chars++ = (char) iChar;
            iChar = 0U;
          }
        }
      }
      if (length > 0 && (utF32Decoder == null || utF32Decoder.MustFlush))
      {
        byte[] bytes1 = new byte[length];
        int num = length;
        if (this.bigEndian)
        {
          while (num > 0)
          {
            bytes1[--num] = (byte) iChar;
            iChar >>= 8;
          }
        }
        else
        {
          while (num > 0)
          {
            bytes1[--num] = (byte) (iChar >> 24);
            iChar <<= 8;
          }
        }
        if (!fallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
        {
          fallbackBuffer.InternalReset();
          this.ThrowCharsOverflow((DecoderNLS) utF32Decoder, chars == chPtr1);
        }
        else
        {
          length = 0;
          iChar = 0U;
        }
      }
      if (utF32Decoder != null)
      {
        utF32Decoder.iChar = (int) iChar;
        utF32Decoder.readByteCount = length;
        utF32Decoder.m_bytesUsed = (int) (bytes - numPtr1);
      }
      return (int) (chars - chPtr1);
    }

    private uint GetSurrogate(char cHigh, char cLow)
    {
      return (uint) (((int) cHigh - 55296) * 1024 + ((int) cLow - 56320) + 65536);
    }

    private char GetHighSurrogate(uint iChar)
    {
      return (char) ((iChar - 65536U) / 1024U + 55296U);
    }

    private char GetLowSurrogate(uint iChar)
    {
      return (char) ((iChar - 65536U) % 1024U + 56320U);
    }

    /// <summary>
    ///   Получает средство декодирования, преобразующее последовательность байтов в кодировке UTF-32 в последовательность символов Юникода.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.Decoder" />, преобразующий последовательность байтов в кодировке UTF-32 в последовательность символов Юникода.
    /// </returns>
    [__DynamicallyInvokable]
    public override Decoder GetDecoder()
    {
      return (Decoder) new UTF32Encoding.UTF32Decoder(this);
    }

    /// <summary>
    ///   Получает средство кодирования, преобразующее последовательность символов Юникода в последовательность байтов в кодировке UTF-32.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.Encoder" />, преобразующий последовательность символов Юникода в последовательность байтов в кодировке UTF-32.
    /// </returns>
    [__DynamicallyInvokable]
    public override Encoder GetEncoder()
    {
      return (Encoder) new EncoderNLS((Encoding) this);
    }

    /// <summary>
    ///   Вычисляет максимальное количество байтов, полученных при кодировании заданного числа символов.
    /// </summary>
    /// <param name="charCount">Число кодируемых символов.</param>
    /// <returns>
    ///   Максимальное количество байтов, полученных при кодировании заданного количества символов.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="charCount" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Результирующее число байтов больше максимального количества, которое можно вернуть как целочисленное значение.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException(nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num1 = (long) charCount + 1L;
      if (this.EncoderFallback.MaxCharCount > 1)
        num1 *= (long) this.EncoderFallback.MaxCharCount;
      long num2 = num1 * 4L;
      if (num2 > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return (int) num2;
    }

    /// <summary>
    ///   Вычисляет максимальное количество символов, полученных при декодировании заданного числа байтов.
    /// </summary>
    /// <param name="byteCount">Число байтов для декодирования.</param>
    /// <returns>
    ///   Максимальное количество символов, полученных при декодировании заданного количества байтов.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="byteCount" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Результирующее число байтов больше максимального количества, которое можно вернуть как целочисленное значение.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      int num = byteCount / 2 + 2;
      if (this.DecoderFallback.MaxCharCount > 2)
        num = num * this.DecoderFallback.MaxCharCount / 2;
      if (num > int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return num;
    }

    /// <summary>
    ///   Возвращает метку порядка байтов Юникода в кодировке UTF-32, если объект <see cref="T:System.Text.UTF32Encoding" /> настроен для ее предоставления.
    /// </summary>
    /// <returns>
    ///   Массив байтов, содержащий метку порядка байтов Юникода, если объект <see cref="T:System.Text.UTF32Encoding" /> настроен для его предоставления.
    ///    В противном случае этот метод возвращает массив байтов нулевой длины.
    /// </returns>
    [__DynamicallyInvokable]
    public override byte[] GetPreamble()
    {
      if (!this.emitUTF32ByteOrderMark)
        return EmptyArray<byte>.Value;
      if (this.bigEndian)
        return new byte[4]
        {
          (byte) 0,
          (byte) 0,
          (byte) 254,
          byte.MaxValue
        };
      return new byte[4]
      {
        byte.MaxValue,
        (byte) 254,
        (byte) 0,
        (byte) 0
      };
    }

    /// <summary>
    ///   Определяет, равен ли заданный объект <see cref="T:System.Object" /> текущему объекту <see cref="T:System.Text.UTF32Encoding" />.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Object" />, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="value" /> является экземпляром класса <see cref="T:System.Text.UTF32Encoding" /> и равен текущему объекту; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      UTF32Encoding utF32Encoding = value as UTF32Encoding;
      if (utF32Encoding != null && this.emitUTF32ByteOrderMark == utF32Encoding.emitUTF32ByteOrderMark && (this.bigEndian == utF32Encoding.bigEndian && this.EncoderFallback.Equals((object) utF32Encoding.EncoderFallback)))
        return this.DecoderFallback.Equals((object) utF32Encoding.DecoderFallback);
      return false;
    }

    /// <summary>Возвращает хэш-код текущего экземпляра.</summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Text.UTF32Encoding" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode() + this.CodePage + (this.emitUTF32ByteOrderMark ? 4 : 0) + (this.bigEndian ? 8 : 0);
    }

    [Serializable]
    internal class UTF32Decoder : DecoderNLS
    {
      internal int iChar;
      internal int readByteCount;

      public UTF32Decoder(UTF32Encoding encoding)
        : base((Encoding) encoding)
      {
      }

      public override void Reset()
      {
        this.iChar = 0;
        this.readByteCount = 0;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }

      internal override bool HasState
      {
        get
        {
          return (uint) this.readByteCount > 0U;
        }
      }
    }
  }
}
