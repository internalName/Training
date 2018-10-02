// Decompiled with JetBrains decompiler
// Type: System.Text.UnicodeEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  /// <summary>
  ///   Представляет кодировку символов Юникода в формате UTF-16.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class UnicodeEncoding : Encoding
  {
    internal bool byteOrderMark = true;
    [OptionalField(VersionAdded = 2)]
    internal bool isThrowException;
    internal bool bigEndian;
    /// <summary>
    ///   Представляет размер символа Юникода в байтах.
    ///    Это поле является константой.
    /// </summary>
    public const int CharSize = 2;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.UnicodeEncoding" />.
    /// </summary>
    [__DynamicallyInvokable]
    public UnicodeEncoding()
      : this(false, true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.UnicodeEncoding" />.
    ///    Параметры указывают, следует ли использовать обратный порядок байтов и возвращает ли метод <see cref="M:System.Text.UnicodeEncoding.GetPreamble" /> метку порядка байтов Юникода.
    /// </summary>
    /// <param name="bigEndian">
    ///   Значение <see langword="true" /> соответствует использованию обратного порядка байтов (самый старший байт располагается на первом месте); значение <see langword="false" /> соответствует использованию прямого порядка байтов (на первом месте находится самый младший байт).
    /// </param>
    /// <param name="byteOrderMark">
    ///   Значение <see langword="true" /> указывает, что метод <see cref="M:System.Text.UnicodeEncoding.GetPreamble" /> возвращает метку порядка байтов Юникода; в противном случае — значение <see langword="false" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </param>
    [__DynamicallyInvokable]
    public UnicodeEncoding(bool bigEndian, bool byteOrderMark)
      : this(bigEndian, byteOrderMark, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.UnicodeEncoding" />.
    ///    Параметры указывают, следует ли использовать обратный порядок байтов, должна ли предоставляться метка порядка байтов Юникода и следует ли создавать исключение при обнаружении недопустимой кодировки.
    /// </summary>
    /// <param name="bigEndian">
    ///   Значение <see langword="true" /> соответствует использованию обратного порядка байтов (самый старший байт располагается на первом месте); значение <see langword="false" /> соответствует использованию прямого порядка байтов (на первом месте находится самый младший байт).
    /// </param>
    /// <param name="byteOrderMark">
    ///   Значение <see langword="true" /> указывает, что метод <see cref="M:System.Text.UnicodeEncoding.GetPreamble" /> возвращает метку порядка байтов Юникода; в противном случае — значение <see langword="false" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </param>
    /// <param name="throwOnInvalidBytes">
    ///   Значение <see langword="true" /> указывает, что следует создавать исключение при обнаружении недопустимой кодировки; в противном случае — значение <see langword="false" />.
    /// </param>
    [__DynamicallyInvokable]
    public UnicodeEncoding(bool bigEndian, bool byteOrderMark, bool throwOnInvalidBytes)
      : base(bigEndian ? 1201 : 1200)
    {
      this.isThrowException = throwOnInvalidBytes;
      this.bigEndian = bigEndian;
      this.byteOrderMark = byteOrderMark;
      if (!this.isThrowException)
        return;
      this.SetDefaultFallbacks();
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.isThrowException = false;
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
    ///   <paramref name="chars" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> или <paramref name="count" /> меньше нуля.
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
    ///   Вычисляет количество байтов, полученных при кодировании символов в указанной строке.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая кодируемый набор символов.
    /// </param>
    /// <returns>
    ///   Число байтов, полученных при кодировании заданных символов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null " />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Результирующее количество байтов больше максимального количества, которое может возвращаться как целое число.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Включена функция обнаружения ошибок, и <paramref name="s" /> содержит недопустимую последовательность символов.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошла резервной (см. Кодировки в .NET Framework полное описание)
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
    ///   Свойство <paramref name="chars" /> имеет значение <see langword="null " />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Результирующее число байтов больше максимального количества, которое можно вернуть как целочисленное значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Включена функция обнаружения ошибок и <paramref name="chars" /> содержит недопустимую последовательность символов.
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
    [ComVisible(false)]
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
    ///   Строка, содержащая набор символов для кодирования.
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
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null " />.
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
    ///   Параметры <paramref name="charIndex" /> и <paramref name="charCount" /> не указывают допустимый диапазон в <paramref name="chars" />.
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
    [ComVisible(false)]
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
    ///   <paramref name="bytes" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> или <paramref name="count" /> меньше нуля.
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
    ///   <paramref name="bytes" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
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
    [ComVisible(false)]
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
    [ComVisible(false)]
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
    ///   Объект <see cref="T:System.String" />, содержащий результаты декодирования заданной последовательности байтов.
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
    [ComVisible(false)]
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
      int num1 = count << 1;
      if (num1 < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      char* charStart = chars;
      char* charEnd = chars + count;
      char ch1 = char.MinValue;
      bool flag = false;
      ulong* numPtr1 = (ulong*) (charEnd - 3);
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        if (ch1 > char.MinValue)
          num1 += 2;
        if (encoder.InternalHasFallbackBuffer)
        {
          encoderFallbackBuffer = encoder.FallbackBuffer;
          if (encoderFallbackBuffer.Remaining > 0)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
          encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
        }
      }
      while (true)
      {
        char ch2;
        while ((ch2 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != char.MinValue || chars < charEnd)
        {
          if (ch2 == char.MinValue)
          {
            if (!this.bigEndian && ch1 == char.MinValue && ((int) chars & 3) == 0)
            {
              ulong* numPtr2 = (ulong*) chars;
              while (numPtr2 < numPtr1)
              {
                if ((-9223231297218904064L & (long) *numPtr2) != 0L)
                {
                  ulong num2 = (ulong) (-576188069258921984L & (long) *numPtr2 ^ -2882066263381583872L);
                  if ((((long) num2 & -281474976710656L) == 0L || ((long) num2 & 281470681743360L) == 0L || (((long) num2 & 4294901760L) == 0L || ((long) num2 & (long) ushort.MaxValue) == 0L)) && (-287953294993589248L & (long) *numPtr2 ^ -2593835887162763264L) != 0L)
                    break;
                }
                numPtr2 += 8;
              }
              chars = (char*) numPtr2;
              if (chars >= charEnd)
                break;
            }
            ch2 = *chars;
            chars += 2;
          }
          else
            num1 += 2;
          if (ch2 >= '\xD800' && ch2 <= '\xDFFF')
          {
            if (ch2 <= '\xDBFF')
            {
              if (ch1 > char.MinValue)
              {
                chars -= 2;
                num1 -= 2;
                if (encoderFallbackBuffer == null)
                {
                  encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
                  encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
                }
                encoderFallbackBuffer.InternalFallback(ch1, ref chars);
                ch1 = char.MinValue;
              }
              else
                ch1 = ch2;
            }
            else if (ch1 == char.MinValue)
            {
              num1 -= 2;
              if (encoderFallbackBuffer == null)
              {
                encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
                encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
              }
              encoderFallbackBuffer.InternalFallback(ch2, ref chars);
            }
            else
              ch1 = char.MinValue;
          }
          else if (ch1 > char.MinValue)
          {
            chars -= 2;
            if (encoderFallbackBuffer == null)
            {
              encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
              encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
            }
            encoderFallbackBuffer.InternalFallback(ch1, ref chars);
            num1 -= 2;
            ch1 = char.MinValue;
          }
        }
        if (ch1 > char.MinValue)
        {
          num1 -= 2;
          if (encoder == null || encoder.MustFlush)
          {
            if (!flag)
            {
              if (encoderFallbackBuffer == null)
              {
                encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
                encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
              }
              encoderFallbackBuffer.InternalFallback(ch1, ref chars);
              ch1 = char.MinValue;
              flag = true;
            }
            else
              break;
          }
          else
            goto label_43;
        }
        else
          goto label_43;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", (object) ch1), nameof (chars));
label_43:
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
    {
      char ch1 = char.MinValue;
      bool flag = false;
      byte* numPtr1 = bytes + byteCount;
      char* charEnd = chars + charCount;
      byte* numPtr2 = bytes;
      char* charStart = chars;
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        if (encoder.InternalHasFallbackBuffer)
        {
          encoderFallbackBuffer = encoder.FallbackBuffer;
          if (encoderFallbackBuffer.Remaining > 0 && encoder.m_throwOnOverflow)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
          encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, false);
        }
      }
      while (true)
      {
        char ch2;
        while ((ch2 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != char.MinValue || chars < charEnd)
        {
          if (ch2 == char.MinValue)
          {
            if (!this.bigEndian && ((int) chars & 3) == 0 && (((int) bytes & 3) == 0 && ch1 == char.MinValue))
            {
              ulong* numPtr3 = (ulong*) ((IntPtr) (chars - 3) + (IntPtr) ((numPtr1 - bytes >> 1 < charEnd - chars ? numPtr1 - bytes >> 1 : charEnd - chars) * 2L));
              ulong* numPtr4 = (ulong*) chars;
              ulong* numPtr5 = (ulong*) bytes;
              while (numPtr4 < numPtr3)
              {
                if ((-9223231297218904064L & (long) *numPtr4) != 0L)
                {
                  ulong num = (ulong) (-576188069258921984L & (long) *numPtr4 ^ -2882066263381583872L);
                  if ((((long) num & -281474976710656L) == 0L || ((long) num & 281470681743360L) == 0L || (((long) num & 4294901760L) == 0L || ((long) num & (long) ushort.MaxValue) == 0L)) && (-287953294993589248L & (long) *numPtr4 ^ -2593835887162763264L) != 0L)
                    break;
                }
                *numPtr5 = *numPtr4;
                numPtr4 += 8;
                numPtr5 += 8;
              }
              chars = (char*) numPtr4;
              bytes = (byte*) numPtr5;
              if (chars >= charEnd)
                break;
            }
            else if (ch1 == char.MinValue && !this.bigEndian && (((int) chars & 3) != ((int) bytes & 3) && ((int) bytes & 1) == 0))
            {
              long num = numPtr1 - bytes >> 1 < charEnd - chars ? numPtr1 - bytes >> 1 : charEnd - chars;
              char* chPtr1 = (char*) bytes;
              char* chPtr2 = (char*) ((IntPtr) chars + (IntPtr) (num * 2L) - 2);
              while (chars < chPtr2)
              {
                if (*chars >= '\xD800' && *chars <= '\xDFFF')
                {
                  if (*chars >= '\xDC00' || *(ushort*) ((IntPtr) chars + 2) < (ushort) 56320 || *(ushort*) ((IntPtr) chars + 2) > (ushort) 57343)
                    break;
                }
                else if (*(ushort*) ((IntPtr) chars + 2) >= (ushort) 55296 && *(ushort*) ((IntPtr) chars + 2) <= (ushort) 57343)
                {
                  *chPtr1 = *chars;
                  chPtr1 += 2;
                  chars += 2;
                  continue;
                }
                *chPtr1 = *chars;
                *(short*) ((IntPtr) chPtr1 + 2) = (short) *(ushort*) ((IntPtr) chars + 2);
                chPtr1 += 2;
                chars += 2;
              }
              bytes = (byte*) chPtr1;
              if (chars >= charEnd)
                break;
            }
            ch2 = *chars;
            chars += 2;
          }
          if (ch2 >= '\xD800' && ch2 <= '\xDFFF')
          {
            if (ch2 <= '\xDBFF')
            {
              if (ch1 > char.MinValue)
              {
                chars -= 2;
                if (encoderFallbackBuffer == null)
                {
                  encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
                  encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, true);
                }
                encoderFallbackBuffer.InternalFallback(ch1, ref chars);
                ch1 = char.MinValue;
                continue;
              }
              ch1 = ch2;
              continue;
            }
            if (ch1 == char.MinValue)
            {
              if (encoderFallbackBuffer == null)
              {
                encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
                encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, true);
              }
              encoderFallbackBuffer.InternalFallback(ch2, ref chars);
              continue;
            }
            if (bytes + 3 >= numPtr1)
            {
              if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
              {
                encoderFallbackBuffer.MovePrevious();
                encoderFallbackBuffer.MovePrevious();
              }
              else
                chars -= 2;
              this.ThrowBytesOverflow(encoder, bytes == numPtr2);
              ch1 = char.MinValue;
              break;
            }
            if (this.bigEndian)
            {
              *bytes++ = (byte) ((uint) ch1 >> 8);
              *bytes++ = (byte) ch1;
            }
            else
            {
              *bytes++ = (byte) ch1;
              *bytes++ = (byte) ((uint) ch1 >> 8);
            }
            ch1 = char.MinValue;
          }
          else if (ch1 > char.MinValue)
          {
            chars -= 2;
            if (encoderFallbackBuffer == null)
            {
              encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
              encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, true);
            }
            encoderFallbackBuffer.InternalFallback(ch1, ref chars);
            ch1 = char.MinValue;
            continue;
          }
          if (bytes + 1 >= numPtr1)
          {
            if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
              encoderFallbackBuffer.MovePrevious();
            else
              chars -= 2;
            this.ThrowBytesOverflow(encoder, bytes == numPtr2);
            break;
          }
          if (this.bigEndian)
          {
            *bytes++ = (byte) ((uint) ch2 >> 8);
            *bytes++ = (byte) ch2;
          }
          else
          {
            *bytes++ = (byte) ch2;
            *bytes++ = (byte) ((uint) ch2 >> 8);
          }
        }
        if (ch1 > char.MinValue && (encoder == null || encoder.MustFlush))
        {
          if (!flag)
          {
            if (encoderFallbackBuffer == null)
            {
              encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
              encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, true);
            }
            encoderFallbackBuffer.InternalFallback(ch1, ref chars);
            ch1 = char.MinValue;
            flag = true;
          }
          else
            break;
        }
        else
          goto label_62;
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", (object) ch1), nameof (chars));
label_62:
      if (encoder != null)
      {
        encoder.charLeftOver = ch1;
        encoder.m_charsUsed = (int) (chars - charStart);
      }
      return (int) (bytes - numPtr2);
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
    {
      UnicodeEncoding.Decoder decoder = (UnicodeEncoding.Decoder) baseDecoder;
      byte* numPtr1 = bytes + count;
      byte* byteStart = bytes;
      int num1 = -1;
      char ch1 = char.MinValue;
      int num2 = count >> 1;
      ulong* numPtr2 = (ulong*) (numPtr1 - 7);
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      if (decoder != null)
      {
        num1 = decoder.lastByte;
        ch1 = decoder.lastChar;
        if (ch1 > char.MinValue)
          ++num2;
        if (num1 >= 0 && (count & 1) == 1)
          ++num2;
      }
      while (bytes < numPtr1)
      {
        if (!this.bigEndian && ((int) bytes & 3) == 0 && (num1 == -1 && ch1 == char.MinValue))
        {
          ulong* numPtr3 = (ulong*) bytes;
          while (numPtr3 < numPtr2)
          {
            if ((-9223231297218904064L & (long) *numPtr3) != 0L)
            {
              ulong num3 = (ulong) (-576188069258921984L & (long) *numPtr3 ^ -2882066263381583872L);
              if ((((long) num3 & -281474976710656L) == 0L || ((long) num3 & 281470681743360L) == 0L || (((long) num3 & 4294901760L) == 0L || ((long) num3 & (long) ushort.MaxValue) == 0L)) && (-287953294993589248L & (long) *numPtr3 ^ -2593835887162763264L) != 0L)
                break;
            }
            numPtr3 += 8;
          }
          bytes = (byte*) numPtr3;
          if (bytes >= numPtr1)
            break;
        }
        if (num1 < 0)
        {
          num1 = (int) *bytes++;
          if (bytes >= numPtr1)
            break;
        }
        char ch2 = !this.bigEndian ? (char) ((int) *bytes++ << 8 | num1) : (char) (num1 << 8 | (int) *bytes++);
        num1 = -1;
        if (ch2 >= '\xD800' && ch2 <= '\xDFFF')
        {
          if (ch2 <= '\xDBFF')
          {
            if (ch1 > char.MinValue)
            {
              int num3 = num2 - 1;
              byte[] bytes1;
              if (this.bigEndian)
                bytes1 = new byte[2]
                {
                  (byte) ((uint) ch1 >> 8),
                  (byte) ch1
                };
              else
                bytes1 = new byte[2]
                {
                  (byte) ch1,
                  (byte) ((uint) ch1 >> 8)
                };
              if (decoderFallbackBuffer == null)
              {
                decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
                decoderFallbackBuffer.InternalInitialize(byteStart, (char*) null);
              }
              num2 = num3 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
            }
            ch1 = ch2;
          }
          else if (ch1 == char.MinValue)
          {
            int num3 = num2 - 1;
            byte[] bytes1;
            if (this.bigEndian)
              bytes1 = new byte[2]
              {
                (byte) ((uint) ch2 >> 8),
                (byte) ch2
              };
            else
              bytes1 = new byte[2]
              {
                (byte) ch2,
                (byte) ((uint) ch2 >> 8)
              };
            if (decoderFallbackBuffer == null)
            {
              decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
              decoderFallbackBuffer.InternalInitialize(byteStart, (char*) null);
            }
            num2 = num3 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
          }
          else
            ch1 = char.MinValue;
        }
        else if (ch1 > char.MinValue)
        {
          int num3 = num2 - 1;
          byte[] bytes1;
          if (this.bigEndian)
            bytes1 = new byte[2]
            {
              (byte) ((uint) ch1 >> 8),
              (byte) ch1
            };
          else
            bytes1 = new byte[2]
            {
              (byte) ch1,
              (byte) ((uint) ch1 >> 8)
            };
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(byteStart, (char*) null);
          }
          num2 = num3 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
          ch1 = char.MinValue;
        }
      }
      if (decoder == null || decoder.MustFlush)
      {
        if (ch1 > char.MinValue)
        {
          int num3 = num2 - 1;
          byte[] bytes1;
          if (this.bigEndian)
            bytes1 = new byte[2]
            {
              (byte) ((uint) ch1 >> 8),
              (byte) ch1
            };
          else
            bytes1 = new byte[2]
            {
              (byte) ch1,
              (byte) ((uint) ch1 >> 8)
            };
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(byteStart, (char*) null);
          }
          num2 = num3 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
          ch1 = char.MinValue;
        }
        if (num1 >= 0)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(byteStart, (char*) null);
          }
          num2 += decoderFallbackBuffer.InternalFallback(new byte[1]
          {
            (byte) num1
          }, bytes);
        }
      }
      if (ch1 > char.MinValue)
        --num2;
      return num2;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
    {
      UnicodeEncoding.Decoder decoder = (UnicodeEncoding.Decoder) baseDecoder;
      int num1 = -1;
      char ch1 = char.MinValue;
      if (decoder != null)
      {
        num1 = decoder.lastByte;
        ch1 = decoder.lastChar;
      }
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      byte* numPtr1 = bytes + byteCount;
      char* charEnd = chars + charCount;
      byte* byteStart = bytes;
      char* chPtr = chars;
      while (bytes < numPtr1)
      {
        if (!this.bigEndian && ((int) chars & 3) == 0 && (((int) bytes & 3) == 0 && num1 == -1) && ch1 == char.MinValue)
        {
          ulong* numPtr2 = (ulong*) (bytes - 7 + (numPtr1 - bytes >> 1 < charEnd - chars ? (IntPtr) (numPtr1 - bytes) : (IntPtr) (charEnd - chars << 1)).ToInt64());
          ulong* numPtr3 = (ulong*) bytes;
          ulong* numPtr4 = (ulong*) chars;
          while (numPtr3 < numPtr2)
          {
            if ((-9223231297218904064L & (long) *numPtr3) != 0L)
            {
              ulong num2 = (ulong) (-576188069258921984L & (long) *numPtr3 ^ -2882066263381583872L);
              if ((((long) num2 & -281474976710656L) == 0L || ((long) num2 & 281470681743360L) == 0L || (((long) num2 & 4294901760L) == 0L || ((long) num2 & (long) ushort.MaxValue) == 0L)) && (-287953294993589248L & (long) *numPtr3 ^ -2593835887162763264L) != 0L)
                break;
            }
            *numPtr4 = *numPtr3;
            numPtr3 += 8;
            numPtr4 += 8;
          }
          chars = (char*) numPtr4;
          bytes = (byte*) numPtr3;
          if (bytes >= numPtr1)
            break;
        }
        if (num1 < 0)
        {
          num1 = (int) *bytes++;
        }
        else
        {
          char ch2 = !this.bigEndian ? (char) ((int) *bytes++ << 8 | num1) : (char) (num1 << 8 | (int) *bytes++);
          num1 = -1;
          if (ch2 >= '\xD800' && ch2 <= '\xDFFF')
          {
            if (ch2 <= '\xDBFF')
            {
              if (ch1 > char.MinValue)
              {
                byte[] bytes1;
                if (this.bigEndian)
                  bytes1 = new byte[2]
                  {
                    (byte) ((uint) ch1 >> 8),
                    (byte) ch1
                  };
                else
                  bytes1 = new byte[2]
                  {
                    (byte) ch1,
                    (byte) ((uint) ch1 >> 8)
                  };
                if (decoderFallbackBuffer == null)
                {
                  decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
                  decoderFallbackBuffer.InternalInitialize(byteStart, charEnd);
                }
                if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
                {
                  bytes -= 2;
                  decoderFallbackBuffer.InternalReset();
                  this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
                  break;
                }
              }
              ch1 = ch2;
              continue;
            }
            if (ch1 == char.MinValue)
            {
              byte[] bytes1;
              if (this.bigEndian)
                bytes1 = new byte[2]
                {
                  (byte) ((uint) ch2 >> 8),
                  (byte) ch2
                };
              else
                bytes1 = new byte[2]
                {
                  (byte) ch2,
                  (byte) ((uint) ch2 >> 8)
                };
              if (decoderFallbackBuffer == null)
              {
                decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
                decoderFallbackBuffer.InternalInitialize(byteStart, charEnd);
              }
              if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
              {
                bytes -= 2;
                decoderFallbackBuffer.InternalReset();
                this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
                break;
              }
              continue;
            }
            if ((UIntPtr) chars >= (UIntPtr) charEnd - new UIntPtr(2))
            {
              bytes -= 2;
              this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
              break;
            }
            *chars++ = ch1;
            ch1 = char.MinValue;
          }
          else if (ch1 > char.MinValue)
          {
            byte[] bytes1;
            if (this.bigEndian)
              bytes1 = new byte[2]
              {
                (byte) ((uint) ch1 >> 8),
                (byte) ch1
              };
            else
              bytes1 = new byte[2]
              {
                (byte) ch1,
                (byte) ((uint) ch1 >> 8)
              };
            if (decoderFallbackBuffer == null)
            {
              decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
              decoderFallbackBuffer.InternalInitialize(byteStart, charEnd);
            }
            if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
            {
              bytes -= 2;
              decoderFallbackBuffer.InternalReset();
              this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
              break;
            }
            ch1 = char.MinValue;
          }
          if (chars >= charEnd)
          {
            bytes -= 2;
            this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
            break;
          }
          *chars++ = ch2;
        }
      }
      if (decoder == null || decoder.MustFlush)
      {
        if (ch1 > char.MinValue)
        {
          byte[] bytes1;
          if (this.bigEndian)
            bytes1 = new byte[2]
            {
              (byte) ((uint) ch1 >> 8),
              (byte) ch1
            };
          else
            bytes1 = new byte[2]
            {
              (byte) ch1,
              (byte) ((uint) ch1 >> 8)
            };
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(byteStart, charEnd);
          }
          if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
          {
            bytes -= 2;
            if (num1 >= 0)
              --bytes;
            decoderFallbackBuffer.InternalReset();
            this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
            bytes += 2;
            if (num1 >= 0)
            {
              ++bytes;
              goto label_66;
            }
            else
              goto label_66;
          }
          else
            ch1 = char.MinValue;
        }
        if (num1 >= 0)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(byteStart, charEnd);
          }
          if (!decoderFallbackBuffer.InternalFallback(new byte[1]
          {
            (byte) num1
          }, bytes, ref chars))
          {
            --bytes;
            decoderFallbackBuffer.InternalReset();
            this.ThrowCharsOverflow((DecoderNLS) decoder, chars == chPtr);
            ++bytes;
          }
          else
            num1 = -1;
        }
      }
label_66:
      if (decoder != null)
      {
        decoder.m_bytesUsed = (int) (bytes - byteStart);
        decoder.lastChar = ch1;
        decoder.lastByte = num1;
      }
      return (int) (chars - chPtr);
    }

    /// <summary>
    ///   Получает средство кодирования, преобразующее последовательность символов Юникода в последовательность байтов в кодировке UTF-16.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.Encoder" />, преобразующий последовательность символов Юникода в последовательность байтов в кодировке UTF-16.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override Encoder GetEncoder()
    {
      return (Encoder) new EncoderNLS((Encoding) this);
    }

    /// <summary>
    ///   Получает средство декодирования, преобразующее последовательность байтов в кодировке UTF-16 в последовательность символов Юникода.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.Decoder" />, преобразующий последовательность байтов в кодировке UTF-16 в последовательность символов Юникода.
    /// </returns>
    [__DynamicallyInvokable]
    public override System.Text.Decoder GetDecoder()
    {
      return (System.Text.Decoder) new UnicodeEncoding.Decoder(this);
    }

    /// <summary>
    ///   Возвращает метку порядка байтов Юникода, закодированную в формате UTF-16, если конструктор данного экземпляра запрашивает метку порядка байтов.
    /// </summary>
    /// <returns>
    ///   Массив байтов, содержащий метку порядка байтов Юникода, если объект <see cref="T:System.Text.UnicodeEncoding" /> настроен для его предоставления.
    ///    В противном случае этот метод возвращает массив байтов нулевой длины.
    /// </returns>
    [__DynamicallyInvokable]
    public override byte[] GetPreamble()
    {
      if (!this.byteOrderMark)
        return EmptyArray<byte>.Value;
      if (this.bigEndian)
        return new byte[2]{ (byte) 254, byte.MaxValue };
      return new byte[2]{ byte.MaxValue, (byte) 254 };
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
      long num2 = num1 << 1;
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
      long num = (long) (byteCount >> 1) + (long) (byteCount & 1) + 1L;
      if (this.DecoderFallback.MaxCharCount > 1)
        num *= (long) this.DecoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return (int) num;
    }

    /// <summary>
    ///   Определяет, равен ли заданный объект <see cref="T:System.Object" /> текущему объекту <see cref="T:System.Text.UnicodeEncoding" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="value" /> является экземпляром класса <see cref="T:System.Text.UnicodeEncoding" /> и равен текущему объекту; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      UnicodeEncoding unicodeEncoding = value as UnicodeEncoding;
      if (unicodeEncoding != null && this.CodePage == unicodeEncoding.CodePage && (this.byteOrderMark == unicodeEncoding.byteOrderMark && this.bigEndian == unicodeEncoding.bigEndian) && this.EncoderFallback.Equals((object) unicodeEncoding.EncoderFallback))
        return this.DecoderFallback.Equals((object) unicodeEncoding.DecoderFallback);
      return false;
    }

    /// <summary>Возвращает хэш-код текущего экземпляра.</summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Text.UnicodeEncoding" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.CodePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode() + (this.byteOrderMark ? 4 : 0) + (this.bigEndian ? 8 : 0);
    }

    [Serializable]
    private class Decoder : DecoderNLS, ISerializable
    {
      internal int lastByte = -1;
      internal char lastChar;

      public Decoder(UnicodeEncoding encoding)
        : base((Encoding) encoding)
      {
      }

      internal Decoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        this.lastByte = (int) info.GetValue(nameof (lastByte), typeof (int));
        try
        {
          this.m_encoding = (Encoding) info.GetValue("m_encoding", typeof (Encoding));
          this.lastChar = (char) info.GetValue(nameof (lastChar), typeof (char));
          this.m_fallback = (DecoderFallback) info.GetValue("m_fallback", typeof (DecoderFallback));
        }
        catch (SerializationException ex)
        {
          this.m_encoding = (Encoding) new UnicodeEncoding((bool) info.GetValue("bigEndian", typeof (bool)), false);
        }
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        info.AddValue("m_encoding", (object) this.m_encoding);
        info.AddValue("m_fallback", (object) this.m_fallback);
        info.AddValue("lastChar", this.lastChar);
        info.AddValue("lastByte", this.lastByte);
        info.AddValue("bigEndian", ((UnicodeEncoding) this.m_encoding).bigEndian);
      }

      public override void Reset()
      {
        this.lastByte = -1;
        this.lastChar = char.MinValue;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }

      internal override bool HasState
      {
        get
        {
          if (this.lastByte == -1)
            return this.lastChar > char.MinValue;
          return true;
        }
      }
    }
  }
}
