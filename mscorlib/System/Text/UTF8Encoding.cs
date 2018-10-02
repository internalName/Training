// Decompiled with JetBrains decompiler
// Type: System.Text.UTF8Encoding
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
  ///   Представляет кодировку символов Юникода в формате UTF-8.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class UTF8Encoding : Encoding
  {
    private const int UTF8_CODEPAGE = 65001;
    private bool emitUTF8Identifier;
    private bool isThrowException;
    private const int FinalByte = 536870912;
    private const int SupplimentarySeq = 268435456;
    private const int ThreeByteSeq = 134217728;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.UTF8Encoding" />.
    /// </summary>
    [__DynamicallyInvokable]
    public UTF8Encoding()
      : this(false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.UTF8Encoding" />.
    ///    Параметр указывает, нужно ли предоставлять метку порядка байтов Юникода.
    /// </summary>
    /// <param name="encoderShouldEmitUTF8Identifier">
    ///   Значение <see langword="true" /> указывает, что метод <see cref="M:System.Text.UTF8Encoding.GetPreamble" /> возвращает метку порядка байтов Юникода; в противном случае — значение <see langword="false" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </param>
    [__DynamicallyInvokable]
    public UTF8Encoding(bool encoderShouldEmitUTF8Identifier)
      : this(encoderShouldEmitUTF8Identifier, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.UTF8Encoding" />.
    ///    Параметры указывают, должна ли предоставляться метка порядка байтов Юникода и следует ли создавать исключение при обнаружении недопустимой кодировки.
    /// </summary>
    /// <param name="encoderShouldEmitUTF8Identifier">
    ///   Значение <see langword="true" /> указывает, что метод <see cref="M:System.Text.UTF8Encoding.GetPreamble" /> должен возвращать метку порядка байтов Юникода; в противном случае — значение <see langword="false" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </param>
    /// <param name="throwOnInvalidBytes">
    ///   Значение <see langword="true" /> указывает, что следует создавать исключение при обнаружении недопустимой кодировки; в противном случае — значение <see langword="false" />.
    /// </param>
    [__DynamicallyInvokable]
    public UTF8Encoding(bool encoderShouldEmitUTF8Identifier, bool throwOnInvalidBytes)
      : base(65001)
    {
      this.emitUTF8Identifier = encoderShouldEmitUTF8Identifier;
      this.isThrowException = throwOnInvalidBytes;
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
    ///   Свойству <see cref="P:System.Text.Encoding.EncoderFallback" /> задано значение <see cref="T:System.Text.EncoderExceptionFallback" />.
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
    /// <param name="chars">
    ///   Объект <see cref="T:System.String" />, содержащий кодируемый набор символов.
    /// </param>
    /// <returns>
    ///   Число байтов, полученных при кодировании заданных символов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="chars" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Результирующее количество байтов больше максимального количества, которое может возвращаться как целое число.
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
    public override unsafe int GetByteCount(string chars)
    {
      if (chars == null)
        throw new ArgumentNullException("s");
      string str = chars;
      char* chars1 = (char*) str;
      if ((IntPtr) chars1 != IntPtr.Zero)
        chars1 += RuntimeHelpers.OffsetToStringData;
      return this.GetByteCount(chars1, chars.Length, (EncoderNLS) null);
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
    ///   Произошла резервной (см. Кодировки в .NET Framework Подробное описание)
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
    ///   Фактическое число байтов, записанных в местоположение, указанное с помощью параметра <paramref name="bytes" />.
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
    ///   <paramref name="byteindex" /> и <paramref name="byteCount" /> определяют допустимый диапазон в <paramref name="bytes" />.
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
    internal override unsafe int GetByteCount(char* chars, int count, EncoderNLS baseEncoder)
    {
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      char* chars1 = chars;
      char* chPtr1 = chars1 + count;
      int num1 = count;
      int ch1 = 0;
      if (baseEncoder != null)
      {
        UTF8Encoding.UTF8Encoder utF8Encoder = (UTF8Encoding.UTF8Encoder) baseEncoder;
        ch1 = utF8Encoder.surrogateChar;
        if (utF8Encoder.InternalHasFallbackBuffer)
        {
          encoderFallbackBuffer = utF8Encoder.FallbackBuffer;
          if (encoderFallbackBuffer.Remaining > 0)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) utF8Encoder.Fallback.GetType()));
          encoderFallbackBuffer.InternalInitialize(chars, chPtr1, (EncoderNLS) utF8Encoder, false);
        }
      }
      while (true)
      {
        if (chars1 >= chPtr1)
        {
          if (ch1 == 0)
          {
            ch1 = encoderFallbackBuffer != null ? (int) encoderFallbackBuffer.InternalGetNextChar() : 0;
            if (ch1 > 0)
            {
              ++num1;
              goto label_23;
            }
          }
          else if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
          {
            ch1 = (int) encoderFallbackBuffer.InternalGetNextChar();
            ++num1;
            if (UTF8Encoding.InRange(ch1, 56320, 57343))
            {
              ch1 = 65533;
              ++num1;
              goto label_25;
            }
            else if (ch1 > 0)
              goto label_23;
            else
              break;
          }
          if (ch1 > 0 && (baseEncoder == null || baseEncoder.MustFlush))
          {
            ++num1;
            goto label_25;
          }
          else
            goto label_83;
        }
        else if (ch1 > 0)
        {
          int ch2 = (int) *chars1;
          ++num1;
          if (UTF8Encoding.InRange(ch2, 56320, 57343))
          {
            ch1 = 65533;
            chars1 += 2;
            goto label_25;
          }
          else
            goto label_25;
        }
        else
        {
          if (encoderFallbackBuffer != null)
          {
            ch1 = (int) encoderFallbackBuffer.InternalGetNextChar();
            if (ch1 > 0)
            {
              ++num1;
              goto label_23;
            }
          }
          ch1 = (int) *chars1;
          chars1 += 2;
        }
label_23:
        if (UTF8Encoding.InRange(ch1, 55296, 56319))
        {
          --num1;
          continue;
        }
label_25:
        if (UTF8Encoding.InRange(ch1, 55296, 57343))
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = baseEncoder != null ? baseEncoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(chars, chars + count, baseEncoder, false);
          }
          encoderFallbackBuffer.InternalFallback((char) ch1, ref chars1);
          --num1;
          ch1 = 0;
        }
        else
        {
          if (ch1 > (int) sbyte.MaxValue)
          {
            if (ch1 > 2047)
              ++num1;
            ++num1;
          }
          if (encoderFallbackBuffer != null && (ch1 = (int) encoderFallbackBuffer.InternalGetNextChar()) != 0)
          {
            ++num1;
            goto label_23;
          }
          else
          {
            int num2 = UTF8Encoding.PtrDiff(chPtr1, chars1);
            if (num2 <= 13)
            {
              char* chPtr2 = chPtr1;
              while (chars1 < chPtr2)
              {
                ch1 = (int) *chars1;
                chars1 += 2;
                if (ch1 > (int) sbyte.MaxValue)
                  goto label_23;
              }
              goto label_83;
            }
            else
            {
              char* chPtr2 = chars1 + num2 - 7;
label_81:
              while (chars1 < chPtr2)
              {
                int ch2 = (int) *chars1;
                chars1 += 2;
                if (ch2 > (int) sbyte.MaxValue)
                {
                  if (ch2 > 2047)
                  {
                    if ((ch2 & 63488) != 55296)
                      ++num1;
                    else
                      goto label_74;
                  }
                  ++num1;
                }
                if (((int) chars1 & 2) != 0)
                {
                  ch2 = (int) *chars1;
                  chars1 += 2;
                  if (ch2 > (int) sbyte.MaxValue)
                  {
                    if (ch2 > 2047)
                    {
                      if ((ch2 & 63488) != 55296)
                        ++num1;
                      else
                        goto label_74;
                    }
                    ++num1;
                  }
                }
                while (chars1 < chPtr2)
                {
                  int num3 = *(int*) chars1;
                  int num4 = *(int*) (chars1 + 2);
                  if (((num3 | num4) & -8323200) != 0)
                  {
                    if (((num3 | num4) & -134154240) == 0)
                    {
                      if ((num3 & -8388608) != 0)
                        ++num1;
                      if ((num3 & 65408) != 0)
                        ++num1;
                      if ((num4 & -8388608) != 0)
                        ++num1;
                      if ((num4 & 65408) != 0)
                        ++num1;
                    }
                    else
                      goto label_73;
                  }
                  chars1 += 4;
                  num3 = *(int*) chars1;
                  int num5 = *(int*) (chars1 + 2);
                  if (((num3 | num5) & -8323200) != 0)
                  {
                    if (((num3 | num5) & -134154240) == 0)
                    {
                      if ((num3 & -8388608) != 0)
                        ++num1;
                      if ((num3 & 65408) != 0)
                        ++num1;
                      if ((num5 & -8388608) != 0)
                        ++num1;
                      if ((num5 & 65408) != 0)
                        ++num1;
                    }
                    else
                      goto label_73;
                  }
                  chars1 += 4;
                  continue;
label_73:
                  ch2 = (int) (ushort) num3;
                  chars1 += 2;
                  if (ch2 <= (int) sbyte.MaxValue)
                    goto label_81;
                  else
                    goto label_74;
                }
                break;
label_74:
                if (ch2 > 2047)
                {
                  if (UTF8Encoding.InRange(ch2, 55296, 57343))
                  {
                    int ch3 = (int) *chars1;
                    if (ch2 > 56319 || !UTF8Encoding.InRange(ch3, 56320, 57343))
                    {
                      chars1 -= 2;
                      break;
                    }
                    chars1 += 2;
                  }
                  ++num1;
                }
                ++num1;
              }
              ch1 = 0;
            }
          }
        }
      }
      --num1;
label_83:
      return num1;
    }

    [SecurityCritical]
    private static unsafe int PtrDiff(char* a, char* b)
    {
      return (int) ((uint) ((sbyte*) a - (sbyte*) b) >> 1);
    }

    [SecurityCritical]
    private static unsafe int PtrDiff(byte* a, byte* b)
    {
      return (int) (a - b);
    }

    private static bool InRange(int ch, int start, int end)
    {
      return (uint) (ch - start) <= (uint) (end - start);
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS baseEncoder)
    {
      UTF8Encoding.UTF8Encoder utF8Encoder = (UTF8Encoding.UTF8Encoder) null;
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      char* chars1 = chars;
      byte* b = bytes;
      char* chPtr1 = chars1 + charCount;
      byte* a = b + byteCount;
      int ch1 = 0;
      if (baseEncoder != null)
      {
        utF8Encoder = (UTF8Encoding.UTF8Encoder) baseEncoder;
        ch1 = utF8Encoder.surrogateChar;
        if (utF8Encoder.InternalHasFallbackBuffer)
        {
          encoderFallbackBuffer = utF8Encoder.FallbackBuffer;
          if (encoderFallbackBuffer.Remaining > 0 && utF8Encoder.m_throwOnOverflow)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) utF8Encoder.Fallback.GetType()));
          encoderFallbackBuffer.InternalInitialize(chars, chPtr1, (EncoderNLS) utF8Encoder, true);
        }
      }
      while (true)
      {
        if (chars1 >= chPtr1)
        {
          if (ch1 == 0)
          {
            ch1 = encoderFallbackBuffer != null ? (int) encoderFallbackBuffer.InternalGetNextChar() : 0;
            if (ch1 > 0)
              goto label_19;
          }
          else if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
          {
            int num = ch1;
            ch1 = (int) encoderFallbackBuffer.InternalGetNextChar();
            if (UTF8Encoding.InRange(ch1, 56320, 57343))
            {
              ch1 = ch1 + (num << 10) - 56613888;
              goto label_20;
            }
            else if (ch1 <= 0)
              goto label_80;
            else
              goto label_19;
          }
          if (ch1 <= 0 || utF8Encoder != null && !utF8Encoder.MustFlush)
            goto label_80;
          else
            goto label_20;
        }
        else if (ch1 > 0)
        {
          int ch2 = (int) *chars1;
          if (UTF8Encoding.InRange(ch2, 56320, 57343))
          {
            ch1 = ch2 + (ch1 << 10) - 56613888;
            chars1 += 2;
            goto label_20;
          }
          else
            goto label_20;
        }
        else
        {
          if (encoderFallbackBuffer != null)
          {
            ch1 = (int) encoderFallbackBuffer.InternalGetNextChar();
            if (ch1 > 0)
              goto label_19;
          }
          ch1 = (int) *chars1;
          chars1 += 2;
        }
label_19:
        if (UTF8Encoding.InRange(ch1, 55296, 56319))
          continue;
label_20:
        if (UTF8Encoding.InRange(ch1, 55296, 57343))
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = baseEncoder != null ? baseEncoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(chars, chPtr1, baseEncoder, true);
          }
          encoderFallbackBuffer.InternalFallback((char) ch1, ref chars1);
          ch1 = 0;
        }
        else
        {
          int num1 = 1;
          if (ch1 > (int) sbyte.MaxValue)
          {
            if (ch1 > 2047)
            {
              if (ch1 > (int) ushort.MaxValue)
                ++num1;
              ++num1;
            }
            ++num1;
          }
          if (b <= a - num1)
          {
            if (ch1 <= (int) sbyte.MaxValue)
            {
              *b = (byte) ch1;
            }
            else
            {
              int num2;
              if (ch1 <= 2047)
              {
                num2 = (int) (byte) (-64 | ch1 >> 6);
              }
              else
              {
                int num3;
                if (ch1 <= (int) ushort.MaxValue)
                {
                  num3 = (int) (byte) (-32 | ch1 >> 12);
                }
                else
                {
                  *b = (byte) (-16 | ch1 >> 18);
                  ++b;
                  num3 = (int) sbyte.MinValue | ch1 >> 12 & 63;
                }
                *b = (byte) num3;
                ++b;
                num2 = (int) sbyte.MinValue | ch1 >> 6 & 63;
              }
              *b = (byte) num2;
              ++b;
              *b = (byte) ((int) sbyte.MinValue | ch1 & 63);
            }
            ++b;
            if (encoderFallbackBuffer == null || (ch1 = (int) encoderFallbackBuffer.InternalGetNextChar()) == 0)
            {
              int num2 = UTF8Encoding.PtrDiff(chPtr1, chars1);
              int num3 = UTF8Encoding.PtrDiff(a, b);
              if (num2 <= 13)
              {
                if (num3 < num2)
                {
                  ch1 = 0;
                }
                else
                {
                  char* chPtr2 = chPtr1;
                  while (chars1 < chPtr2)
                  {
                    ch1 = (int) *chars1;
                    chars1 += 2;
                    if (ch1 <= (int) sbyte.MaxValue)
                    {
                      *b = (byte) ch1;
                      ++b;
                    }
                    else
                      goto label_19;
                  }
                  goto label_54;
                }
              }
              else
              {
                if (num3 < num2)
                  num2 = num3;
                char* chPtr2 = chars1 + num2 - 5;
                while (chars1 < chPtr2)
                {
                  int ch2 = (int) *chars1;
                  chars1 += 2;
                  if (ch2 <= (int) sbyte.MaxValue)
                  {
                    *b = (byte) ch2;
                    ++b;
                    if (((int) chars1 & 2) != 0)
                    {
                      ch2 = (int) *chars1;
                      chars1 += 2;
                      if (ch2 <= (int) sbyte.MaxValue)
                      {
                        *b = (byte) ch2;
                        ++b;
                      }
                      else
                        goto label_67;
                    }
                    while (chars1 < chPtr2)
                    {
                      int num4 = *(int*) chars1;
                      int num5 = *(int*) (chars1 + 2);
                      if (((num4 | num5) & -8323200) == 0)
                      {
                        *b = (byte) num4;
                        b[1] = (byte) (num4 >> 16);
                        chars1 += 4;
                        b[2] = (byte) num5;
                        b[3] = (byte) (num5 >> 16);
                        b += 4;
                      }
                      else
                      {
                        ch2 = (int) (ushort) num4;
                        chars1 += 2;
                        if (ch2 <= (int) sbyte.MaxValue)
                        {
                          *b = (byte) ch2;
                          ++b;
                          break;
                        }
                        goto label_67;
                      }
                    }
                    continue;
                  }
label_67:
                  int num6;
                  if (ch2 <= 2047)
                  {
                    num6 = -64 | ch2 >> 6;
                  }
                  else
                  {
                    int num4;
                    if (!UTF8Encoding.InRange(ch2, 55296, 57343))
                    {
                      num4 = -32 | ch2 >> 12;
                    }
                    else
                    {
                      if (ch2 > 56319)
                      {
                        chars1 -= 2;
                        break;
                      }
                      int ch3 = (int) *chars1;
                      chars1 += 2;
                      if (!UTF8Encoding.InRange(ch3, 56320, 57343))
                      {
                        chars1 -= 2;
                        break;
                      }
                      ch2 = ch3 + (ch2 << 10) - 56613888;
                      *b = (byte) (-16 | ch2 >> 18);
                      ++b;
                      num4 = (int) sbyte.MinValue | ch2 >> 12 & 63;
                    }
                    *b = (byte) num4;
                    chPtr2 -= 2;
                    ++b;
                    num6 = (int) sbyte.MinValue | ch2 >> 6 & 63;
                  }
                  *b = (byte) num6;
                  chPtr2 -= 2;
                  byte* numPtr = b + 1;
                  *numPtr = (byte) ((int) sbyte.MinValue | ch2 & 63);
                  b = numPtr + 1;
                }
                ch1 = 0;
              }
            }
            else
              goto label_19;
          }
          else
            break;
        }
      }
      if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
      {
        encoderFallbackBuffer.MovePrevious();
        if (ch1 > (int) ushort.MaxValue)
          encoderFallbackBuffer.MovePrevious();
      }
      else
      {
        chars1 -= 2;
        if (ch1 > (int) ushort.MaxValue)
          chars1 -= 2;
      }
      this.ThrowBytesOverflow((EncoderNLS) utF8Encoder, b == bytes);
      ch1 = 0;
      goto label_80;
label_54:
      ch1 = 0;
label_80:
      if (utF8Encoder != null)
      {
        utF8Encoder.surrogateChar = ch1;
        utF8Encoder.m_charsUsed = (int) (chars1 - chars);
      }
      return (int) (b - bytes);
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
    {
      byte* numPtr1 = bytes;
      byte* a = numPtr1 + count;
      int num1 = count;
      int ch = 0;
      DecoderFallbackBuffer fallback = (DecoderFallbackBuffer) null;
      if (baseDecoder != null)
      {
        ch = ((UTF8Encoding.UTF8Decoder) baseDecoder).bits;
        num1 -= ch >> 30;
      }
label_2:
      while (numPtr1 < a)
      {
        if (ch != 0)
        {
          int num2 = (int) *numPtr1;
          ++numPtr1;
          if ((num2 & -64) != 128)
          {
            --numPtr1;
            num1 += ch >> 30;
          }
          else
          {
            ch = ch << 6 | num2 & 63;
            if ((ch & 536870912) == 0)
            {
              if ((ch & 268435456) != 0)
              {
                if ((ch & 8388608) != 0 || UTF8Encoding.InRange(ch & 496, 16, 256))
                  continue;
              }
              else if ((ch & 992) != 0 && (ch & 992) != 864)
                continue;
            }
            else if ((ch & 270467072) == 268435456)
            {
              --num1;
              goto label_27;
            }
            else
              goto label_27;
          }
        }
        else
          goto label_15;
label_12:
        if (fallback == null)
        {
          fallback = baseDecoder != null ? baseDecoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
          fallback.InternalInitialize(bytes, (char*) null);
        }
        num1 += this.FallbackInvalidByteSequence(numPtr1, ch, fallback);
        ch = 0;
        continue;
label_15:
        ch = (int) *numPtr1;
        ++numPtr1;
label_16:
        if (ch > (int) sbyte.MaxValue)
        {
          --num1;
          if ((ch & 64) != 0)
          {
            if ((ch & 32) != 0)
            {
              if ((ch & 16) != 0)
              {
                int num2 = ch & 15;
                if (num2 > 4)
                {
                  ch = num2 | 240;
                  goto label_12;
                }
                else
                {
                  ch = num2 | 1347226624;
                  --num1;
                  continue;
                }
              }
              else
              {
                ch = ch & 15 | 1210220544;
                --num1;
                continue;
              }
            }
            else
            {
              int num2 = ch & 31;
              if (num2 <= 1)
              {
                ch = num2 | 192;
                goto label_12;
              }
              else
              {
                ch = num2 | 8388608;
                continue;
              }
            }
          }
          else
            goto label_12;
        }
label_27:
        int num3 = UTF8Encoding.PtrDiff(a, numPtr1);
        if (num3 <= 13)
        {
          byte* numPtr2 = a;
          while (numPtr1 < numPtr2)
          {
            ch = (int) *numPtr1;
            ++numPtr1;
            if (ch > (int) sbyte.MaxValue)
              goto label_16;
          }
          ch = 0;
          break;
        }
        byte* numPtr3 = numPtr1 + num3 - 7;
        while (numPtr1 < numPtr3)
        {
          int num2 = (int) *numPtr1;
          ++numPtr1;
          if (num2 <= (int) sbyte.MaxValue)
          {
            if (((int) numPtr1 & 1) != 0)
            {
              num2 = (int) *numPtr1;
              ++numPtr1;
              if (num2 > (int) sbyte.MaxValue)
                goto label_45;
            }
            int num4;
            if (((int) numPtr1 & 2) != 0)
            {
              num4 = (int) *(ushort*) numPtr1;
              if ((num4 & 32896) == 0)
                numPtr1 += 2;
              else
                goto label_44;
            }
            while (numPtr1 < numPtr3)
            {
              num4 = *(int*) numPtr1;
              int num5 = *(int*) (numPtr1 + 4);
              if (((num4 | num5) & -2139062144) == 0)
              {
                numPtr1 += 8;
                if (numPtr1 < numPtr3)
                {
                  num4 = *(int*) numPtr1;
                  int num6 = *(int*) (numPtr1 + 4);
                  if (((num4 | num6) & -2139062144) == 0)
                    numPtr1 += 8;
                  else
                    goto label_44;
                }
                else
                  break;
              }
              else
                goto label_44;
            }
            break;
label_44:
            num2 = num4 & (int) byte.MaxValue;
            ++numPtr1;
            if (num2 <= (int) sbyte.MaxValue)
              continue;
          }
label_45:
          int num7 = (int) *numPtr1;
          ++numPtr1;
          if ((num2 & 64) != 0 && (num7 & -64) == 128)
          {
            int num4 = num7 & 63;
            if ((num2 & 32) != 0)
            {
              int num5 = num4 | (num2 & 15) << 6;
              if ((num2 & 16) != 0)
              {
                int num6 = (int) *numPtr1;
                if (UTF8Encoding.InRange(num5 >> 4, 1, 16) && (num6 & -64) == 128)
                {
                  int num8 = num5 << 6 | num6 & 63;
                  if (((int) numPtr1[1] & -64) == 128)
                  {
                    numPtr1 += 2;
                    --num1;
                  }
                  else
                    goto label_57;
                }
                else
                  goto label_57;
              }
              else
              {
                int num6 = (int) *numPtr1;
                if ((num5 & 992) != 0 && (num5 & 992) != 864 && (num6 & -64) == 128)
                {
                  ++numPtr1;
                  --num1;
                }
                else
                  goto label_57;
              }
            }
            else if ((num2 & 30) == 0)
              goto label_57;
            --num1;
            continue;
          }
label_57:
          numPtr1 -= 2;
          ch = 0;
          goto label_2;
        }
        ch = 0;
      }
      if (ch != 0)
      {
        num1 += ch >> 30;
        if (baseDecoder == null || baseDecoder.MustFlush)
        {
          if (fallback == null)
          {
            fallback = baseDecoder != null ? baseDecoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
            fallback.InternalInitialize(bytes, (char*) null);
          }
          num1 += this.FallbackInvalidByteSequence(numPtr1, ch, fallback);
        }
      }
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
    {
      byte* pSrc = bytes;
      char* pTarget = chars;
      byte* a = pSrc + byteCount;
      char* chPtr1 = pTarget + charCount;
      int ch = 0;
      DecoderFallbackBuffer fallback = (DecoderFallbackBuffer) null;
      if (baseDecoder != null)
        ch = ((UTF8Encoding.UTF8Decoder) baseDecoder).bits;
label_2:
      while (pSrc < a)
      {
        if (ch != 0)
        {
          int num = (int) *pSrc;
          ++pSrc;
          if ((num & -64) != 128)
          {
            --pSrc;
          }
          else
          {
            ch = ch << 6 | num & 63;
            if ((ch & 536870912) == 0)
            {
              if ((ch & 268435456) != 0)
              {
                if ((ch & 8388608) != 0 || UTF8Encoding.InRange(ch & 496, 16, 256))
                  continue;
              }
              else if ((ch & 992) != 0 && (ch & 992) != 864)
                continue;
            }
            else if ((ch & 270467072) > 268435456 && pTarget < chPtr1)
            {
              *pTarget = (char) ((ch >> 10 & 2047) - 10304);
              pTarget += 2;
              ch = (ch & 1023) + 56320;
              goto label_29;
            }
            else
              goto label_29;
          }
        }
        else
          goto label_17;
label_12:
        if (fallback == null)
        {
          fallback = baseDecoder != null ? baseDecoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
          fallback.InternalInitialize(bytes, chPtr1);
        }
        if (!this.FallbackInvalidByteSequence(ref pSrc, ch, fallback, ref pTarget))
        {
          fallback.InternalReset();
          this.ThrowCharsOverflow(baseDecoder, pTarget == chars);
          ch = 0;
          break;
        }
        ch = 0;
        continue;
label_17:
        ch = (int) *pSrc;
        ++pSrc;
label_18:
        if (ch > (int) sbyte.MaxValue)
        {
          if ((ch & 64) != 0)
          {
            if ((ch & 32) != 0)
            {
              if ((ch & 16) != 0)
              {
                int num = ch & 15;
                if (num > 4)
                {
                  ch = num | 240;
                  goto label_12;
                }
                else
                {
                  ch = num | 1347226624;
                  continue;
                }
              }
              else
              {
                ch = ch & 15 | 1210220544;
                continue;
              }
            }
            else
            {
              int num = ch & 31;
              if (num <= 1)
              {
                ch = num | 192;
                goto label_12;
              }
              else
              {
                ch = num | 8388608;
                continue;
              }
            }
          }
          else
            goto label_12;
        }
label_29:
        if (pTarget >= chPtr1)
        {
          int num = ch & 2097151;
          if (num > (int) sbyte.MaxValue)
          {
            if (num > 2047)
            {
              if (num >= 56320 && num <= 57343)
              {
                --pSrc;
                pTarget -= 2;
              }
              else if (num > (int) ushort.MaxValue)
                --pSrc;
              --pSrc;
            }
            --pSrc;
          }
          --pSrc;
          this.ThrowCharsOverflow(baseDecoder, pTarget == chars);
          ch = 0;
          break;
        }
        *pTarget = (char) ch;
        pTarget += 2;
        int num1 = UTF8Encoding.PtrDiff(chPtr1, pTarget);
        int num2 = UTF8Encoding.PtrDiff(a, pSrc);
        if (num2 <= 13)
        {
          if (num1 < num2)
          {
            ch = 0;
          }
          else
          {
            byte* numPtr = a;
            while (pSrc < numPtr)
            {
              ch = (int) *pSrc;
              ++pSrc;
              if (ch <= (int) sbyte.MaxValue)
              {
                *pTarget = (char) ch;
                pTarget += 2;
              }
              else
                goto label_18;
            }
            ch = 0;
            break;
          }
        }
        else
        {
          if (num1 < num2)
            num2 = num1;
          char* chPtr2 = pTarget + num2 - 7;
          while (pTarget < chPtr2)
          {
            int num3 = (int) *pSrc;
            ++pSrc;
            if (num3 <= (int) sbyte.MaxValue)
            {
              *pTarget = (char) num3;
              pTarget += 2;
              if (((int) pSrc & 1) != 0)
              {
                num3 = (int) *pSrc;
                ++pSrc;
                if (num3 <= (int) sbyte.MaxValue)
                {
                  *pTarget = (char) num3;
                  pTarget += 2;
                }
                else
                  goto label_62;
              }
              int num4;
              if (((int) pSrc & 2) != 0)
              {
                num4 = (int) *(ushort*) pSrc;
                if ((num4 & 32896) == 0)
                {
                  *pTarget = (char) (num4 & (int) sbyte.MaxValue);
                  pSrc += 2;
                  *(short*) ((IntPtr) pTarget + 2) = (short) (ushort) (num4 >> 8 & (int) sbyte.MaxValue);
                  pTarget += 2;
                }
                else
                  goto label_60;
              }
              while (pTarget < chPtr2)
              {
                num4 = *(int*) pSrc;
                int num5 = *(int*) (pSrc + 4);
                if (((num4 | num5) & -2139062144) == 0)
                {
                  *pTarget = (char) (num4 & (int) sbyte.MaxValue);
                  *(short*) ((IntPtr) pTarget + 2) = (short) (ushort) (num4 >> 8 & (int) sbyte.MaxValue);
                  pTarget[2] = (char) (num4 >> 16 & (int) sbyte.MaxValue);
                  pTarget[3] = (char) (num4 >> 24 & (int) sbyte.MaxValue);
                  pSrc += 8;
                  pTarget[4] = (char) (num5 & (int) sbyte.MaxValue);
                  pTarget[5] = (char) (num5 >> 8 & (int) sbyte.MaxValue);
                  pTarget[6] = (char) (num5 >> 16 & (int) sbyte.MaxValue);
                  pTarget[7] = (char) (num5 >> 24 & (int) sbyte.MaxValue);
                  pTarget += 8;
                }
                else
                  goto label_60;
              }
              break;
label_60:
              num3 = num4 & (int) byte.MaxValue;
              ++pSrc;
              if (num3 <= (int) sbyte.MaxValue)
              {
                *pTarget = (char) num3;
                pTarget += 2;
                continue;
              }
            }
label_62:
            int num6 = (int) *pSrc;
            ++pSrc;
            if ((num3 & 64) != 0 && (num6 & -64) == 128)
            {
              int num4 = num6 & 63;
              int num5;
              if ((num3 & 32) != 0)
              {
                int num7 = num4 | (num3 & 15) << 6;
                if ((num3 & 16) != 0)
                {
                  int num8 = (int) *pSrc;
                  if (UTF8Encoding.InRange(num7 >> 4, 1, 16) && (num8 & -64) == 128)
                  {
                    int num9 = num7 << 6 | num8 & 63;
                    int num10 = (int) pSrc[1];
                    if ((num10 & -64) == 128)
                    {
                      pSrc += 2;
                      int num11 = num9 << 6 | num10 & 63;
                      *pTarget = (char) ((num11 >> 10 & 2047) - 10304);
                      pTarget += 2;
                      num5 = (num11 & 1023) - 9216;
                      chPtr2 -= 2;
                    }
                    else
                      goto label_75;
                  }
                  else
                    goto label_75;
                }
                else
                {
                  int num8 = (int) *pSrc;
                  if ((num7 & 992) != 0 && (num7 & 992) != 864 && (num8 & -64) == 128)
                  {
                    ++pSrc;
                    num5 = num7 << 6 | num8 & 63;
                    chPtr2 -= 2;
                  }
                  else
                    goto label_75;
                }
              }
              else
              {
                int num7 = num3 & 31;
                if (num7 > 1)
                  num5 = num7 << 6 | num4;
                else
                  goto label_75;
              }
              *pTarget = (char) num5;
              pTarget += 2;
              chPtr2 -= 2;
              continue;
            }
label_75:
            pSrc -= 2;
            ch = 0;
            goto label_2;
          }
          ch = 0;
        }
      }
      if (ch != 0 && (baseDecoder == null || baseDecoder.MustFlush))
      {
        if (fallback == null)
        {
          fallback = baseDecoder != null ? baseDecoder.FallbackBuffer : this.decoderFallback.CreateFallbackBuffer();
          fallback.InternalInitialize(bytes, chPtr1);
        }
        if (!this.FallbackInvalidByteSequence(ref pSrc, ch, fallback, ref pTarget))
        {
          fallback.InternalReset();
          this.ThrowCharsOverflow(baseDecoder, pTarget == chars);
        }
        ch = 0;
      }
      if (baseDecoder != null)
      {
        ((UTF8Encoding.UTF8Decoder) baseDecoder).bits = ch;
        baseDecoder.m_bytesUsed = (int) (pSrc - bytes);
      }
      return UTF8Encoding.PtrDiff(pTarget, chars);
    }

    [SecurityCritical]
    private unsafe bool FallbackInvalidByteSequence(ref byte* pSrc, int ch, DecoderFallbackBuffer fallback, ref char* pTarget)
    {
      byte* pSrc1 = pSrc;
      byte[] bytesUnknown = this.GetBytesUnknown(ref pSrc1, ch);
      if (fallback.InternalFallback(bytesUnknown, pSrc, ref pTarget))
        return true;
      pSrc = pSrc1;
      return false;
    }

    [SecurityCritical]
    private unsafe int FallbackInvalidByteSequence(byte* pSrc, int ch, DecoderFallbackBuffer fallback)
    {
      byte[] bytesUnknown = this.GetBytesUnknown(ref pSrc, ch);
      return fallback.InternalFallback(bytesUnknown, pSrc);
    }

    [SecurityCritical]
    private unsafe byte[] GetBytesUnknown(ref byte* pSrc, int ch)
    {
      byte[] numArray;
      if (ch < 256 && ch >= 0)
      {
        --pSrc;
        numArray = new byte[1]{ (byte) ch };
      }
      else if ((ch & 402653184) == 0)
      {
        --pSrc;
        numArray = new byte[1]{ (byte) (ch & 31 | 192) };
      }
      else if ((ch & 268435456) != 0)
      {
        if ((ch & 8388608) != 0)
        {
          pSrc -= 3;
          numArray = new byte[3]
          {
            (byte) (ch >> 12 & 7 | 240),
            (byte) (ch >> 6 & 63 | 128),
            (byte) (ch & 63 | 128)
          };
        }
        else if ((ch & 131072) != 0)
        {
          pSrc -= 2;
          numArray = new byte[2]
          {
            (byte) (ch >> 6 & 7 | 240),
            (byte) (ch & 63 | 128)
          };
        }
        else
        {
          --pSrc;
          numArray = new byte[1]{ (byte) (ch & 7 | 240) };
        }
      }
      else if ((ch & 8388608) != 0)
      {
        pSrc -= 2;
        numArray = new byte[2]
        {
          (byte) (ch >> 6 & 15 | 224),
          (byte) (ch & 63 | 128)
        };
      }
      else
      {
        --pSrc;
        numArray = new byte[1]{ (byte) (ch & 15 | 224) };
      }
      return numArray;
    }

    /// <summary>
    ///   Получает средство декодирования, преобразующее последовательность байтов в кодировке UTF-8 в последовательность символов Юникода.
    /// </summary>
    /// <returns>
    ///   Средство декодирования, преобразующее последовательность байтов в кодировке UTF-8 в последовательность символов Юникода.
    /// </returns>
    [__DynamicallyInvokable]
    public override Decoder GetDecoder()
    {
      return (Decoder) new UTF8Encoding.UTF8Decoder(this);
    }

    /// <summary>
    ///   Получает средство кодирования, преобразующее последовательность символов Юникода в последовательность байтов в кодировке UTF-8.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.Encoder" />, преобразующий последовательность символов Юникода в последовательность байтов в кодировке UTF-8.
    /// </returns>
    [__DynamicallyInvokable]
    public override Encoder GetEncoder()
    {
      return (Encoder) new UTF8Encoding.UTF8Encoder(this);
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
      long num2 = num1 * 3L;
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
      long num = (long) byteCount + 1L;
      if (this.DecoderFallback.MaxCharCount > 1)
        num *= (long) this.DecoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return (int) num;
    }

    /// <summary>
    ///   Возвращает метку порядка байтов Юникода в кодировке UTF-8, если кодирующий объект <see cref="T:System.Text.UTF8Encoding" /> настроен для ее предоставления.
    /// </summary>
    /// <returns>
    ///   Массив байтов, содержащий метку порядка байтов Юникода, если кодирующий объект <see cref="T:System.Text.UTF8Encoding" /> настроен для ее предоставления.
    ///    В противном случае этот метод возвращает массив байтов нулевой длины.
    /// </returns>
    [__DynamicallyInvokable]
    public override byte[] GetPreamble()
    {
      if (!this.emitUTF8Identifier)
        return EmptyArray<byte>.Value;
      return new byte[3]
      {
        (byte) 239,
        (byte) 187,
        (byte) 191
      };
    }

    /// <summary>
    ///   Определяет, равен ли заданный объект текущему объекту <see cref="T:System.Text.UTF8Encoding" />.
    /// </summary>
    /// <param name="value">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="value" /> является экземпляром класса <see cref="T:System.Text.UTF8Encoding" /> и равен текущему объекту; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      UTF8Encoding utF8Encoding = value as UTF8Encoding;
      if (utF8Encoding != null && this.emitUTF8Identifier == utF8Encoding.emitUTF8Identifier && this.EncoderFallback.Equals((object) utF8Encoding.EncoderFallback))
        return this.DecoderFallback.Equals((object) utF8Encoding.DecoderFallback);
      return false;
    }

    /// <summary>Возвращает хэш-код текущего экземпляра.</summary>
    /// <returns>Хэш-код для текущего экземпляра.</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode() + 65001 + (this.emitUTF8Identifier ? 1 : 0);
    }

    internal sealed class UTF8EncodingSealed : UTF8Encoding
    {
      public UTF8EncodingSealed(bool encoderShouldEmitUTF8Identifier)
        : base(encoderShouldEmitUTF8Identifier)
      {
      }
    }

    [Serializable]
    internal class UTF8Encoder : EncoderNLS, ISerializable
    {
      internal int surrogateChar;

      public UTF8Encoder(UTF8Encoding encoding)
        : base((Encoding) encoding)
      {
      }

      internal UTF8Encoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        this.m_encoding = (Encoding) info.GetValue("encoding", typeof (Encoding));
        this.surrogateChar = (int) info.GetValue(nameof (surrogateChar), typeof (int));
        try
        {
          this.m_fallback = (EncoderFallback) info.GetValue("m_fallback", typeof (EncoderFallback));
        }
        catch (SerializationException ex)
        {
          this.m_fallback = (EncoderFallback) null;
        }
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        info.AddValue("encoding", (object) this.m_encoding);
        info.AddValue("surrogateChar", this.surrogateChar);
        info.AddValue("m_fallback", (object) this.m_fallback);
        info.AddValue("storedSurrogate", this.surrogateChar > 0);
        info.AddValue("mustFlush", false);
      }

      public override void Reset()
      {
        this.surrogateChar = 0;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }

      internal override bool HasState
      {
        get
        {
          return (uint) this.surrogateChar > 0U;
        }
      }
    }

    [Serializable]
    internal class UTF8Decoder : DecoderNLS, ISerializable
    {
      internal int bits;

      public UTF8Decoder(UTF8Encoding encoding)
        : base((Encoding) encoding)
      {
      }

      internal UTF8Decoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        this.m_encoding = (Encoding) info.GetValue("encoding", typeof (Encoding));
        try
        {
          this.bits = (int) info.GetValue("wbits", typeof (int));
          this.m_fallback = (DecoderFallback) info.GetValue("m_fallback", typeof (DecoderFallback));
        }
        catch (SerializationException ex)
        {
          this.bits = 0;
          this.m_fallback = (DecoderFallback) null;
        }
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        info.AddValue("encoding", (object) this.m_encoding);
        info.AddValue("wbits", this.bits);
        info.AddValue("m_fallback", (object) this.m_fallback);
        info.AddValue("bits", 0);
        info.AddValue("trailCount", 0);
        info.AddValue("isSurrogate", false);
        info.AddValue("byteSequence", 0);
      }

      public override void Reset()
      {
        this.bits = 0;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }

      internal override bool HasState
      {
        get
        {
          return (uint) this.bits > 0U;
        }
      }
    }
  }
}
