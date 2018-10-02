// Decompiled with JetBrains decompiler
// Type: System.Text.ASCIIEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Text
{
  /// <summary>Представляет кодировку ASCII символов Юникода.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ASCIIEncoding : Encoding
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.ASCIIEncoding" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ASCIIEncoding()
      : base(20127)
    {
    }

    internal override void SetDefaultFallbacks()
    {
      this.encoderFallback = EncoderFallback.ReplacementFallback;
      this.decoderFallback = DecoderFallback.ReplacementFallback;
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
    ///   Результирующее число байтов больше максимального количества, которое можно вернуть как целочисленное значение.
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
        throw new ArgumentNullException(nameof (chars));
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
    /// <param name="chars">
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
    public override unsafe int GetBytes(string chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      if (chars == null || bytes == null)
        throw new ArgumentNullException(chars == null ? nameof (chars) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (charIndex < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(charIndex < 0 ? nameof (charIndex) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (chars.Length - charIndex < charCount)
        throw new ArgumentOutOfRangeException(nameof (chars), Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      if (byteIndex < 0 || byteIndex > bytes.Length)
        throw new ArgumentOutOfRangeException(nameof (byteIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      int byteCount = bytes.Length - byteIndex;
      if (bytes.Length == 0)
        bytes = new byte[1];
      string str = chars;
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
    ///   Параметры <paramref name="byteindex" /> и <paramref name="byteCount" /> не указывают допустимый диапазон в <paramref name="bytes" />.
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
    /// <param name="byteIndex">
    ///   Индекс первого декодируемого байта.
    /// </param>
    /// <param name="byteCount">Число байтов для декодирования.</param>
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
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe string GetString(byte[] bytes, int byteIndex, int byteCount)
    {
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (byteIndex < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(byteIndex < 0 ? nameof (byteIndex) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (bytes.Length - byteIndex < byteCount)
        throw new ArgumentOutOfRangeException(nameof (bytes), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (bytes.Length == 0)
        return string.Empty;
      fixed (byte* numPtr = bytes)
        return string.CreateStringFromEncoding(numPtr + byteIndex, byteCount, (Encoding) this);
    }

    [SecurityCritical]
    internal override unsafe int GetByteCount(char* chars, int charCount, EncoderNLS encoder)
    {
      char ch1 = char.MinValue;
      char* charEnd = chars + charCount;
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      EncoderReplacementFallback replacementFallback;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        replacementFallback = encoder.Fallback as EncoderReplacementFallback;
        if (encoder.InternalHasFallbackBuffer)
        {
          encoderFallbackBuffer = encoder.FallbackBuffer;
          if (encoderFallbackBuffer.Remaining > 0 && encoder.m_throwOnOverflow)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
          encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, false);
        }
      }
      else
        replacementFallback = this.EncoderFallback as EncoderReplacementFallback;
      if (replacementFallback != null && replacementFallback.MaxCharCount == 1)
      {
        if (ch1 > char.MinValue)
          ++charCount;
        return charCount;
      }
      int num = 0;
      if (ch1 > char.MinValue)
      {
        encoderFallbackBuffer = encoder.FallbackBuffer;
        encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, false);
        encoderFallbackBuffer.InternalFallback(ch1, ref chars);
      }
      char ch2;
      while ((ch2 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != char.MinValue || chars < charEnd)
      {
        if (ch2 == char.MinValue)
        {
          ch2 = *chars;
          chars += 2;
        }
        if (ch2 > '\x007F')
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - charCount, charEnd, encoder, false);
          }
          encoderFallbackBuffer.InternalFallback(ch2, ref chars);
        }
        else
          ++num;
      }
      return num;
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
    {
      char ch1 = char.MinValue;
      EncoderFallbackBuffer encoderFallbackBuffer = (EncoderFallbackBuffer) null;
      char* charEnd = chars + charCount;
      byte* numPtr1 = bytes;
      char* charStart = chars;
      EncoderReplacementFallback replacementFallback;
      if (encoder != null)
      {
        ch1 = encoder.charLeftOver;
        replacementFallback = encoder.Fallback as EncoderReplacementFallback;
        if (encoder.InternalHasFallbackBuffer)
        {
          encoderFallbackBuffer = encoder.FallbackBuffer;
          if (encoderFallbackBuffer.Remaining > 0 && encoder.m_throwOnOverflow)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.EncodingName, (object) encoder.Fallback.GetType()));
          encoderFallbackBuffer.InternalInitialize(charStart, charEnd, encoder, true);
        }
      }
      else
        replacementFallback = this.EncoderFallback as EncoderReplacementFallback;
      if (replacementFallback != null && replacementFallback.MaxCharCount == 1)
      {
        char ch2 = replacementFallback.DefaultString[0];
        if (ch2 <= '\x007F')
        {
          if (ch1 > char.MinValue)
          {
            if (byteCount == 0)
              this.ThrowBytesOverflow(encoder, true);
            *bytes++ = (byte) ch2;
            --byteCount;
          }
          if (byteCount < charCount)
          {
            this.ThrowBytesOverflow(encoder, byteCount < 1);
            charEnd = chars + byteCount;
          }
          while (chars < charEnd)
          {
            char ch3 = *chars++;
            *bytes++ = ch3 < '\x0080' ? (byte) ch3 : (byte) ch2;
          }
          if (encoder != null)
          {
            encoder.charLeftOver = char.MinValue;
            encoder.m_charsUsed = (int) (chars - charStart);
          }
          return (int) (bytes - numPtr1);
        }
      }
      byte* numPtr2 = bytes + byteCount;
      if (ch1 > char.MinValue)
      {
        encoderFallbackBuffer = encoder.FallbackBuffer;
        encoderFallbackBuffer.InternalInitialize(chars, charEnd, encoder, true);
        encoderFallbackBuffer.InternalFallback(ch1, ref chars);
      }
      char ch4;
      while ((ch4 = encoderFallbackBuffer == null ? char.MinValue : encoderFallbackBuffer.InternalGetNextChar()) != char.MinValue || chars < charEnd)
      {
        if (ch4 == char.MinValue)
        {
          ch4 = *chars;
          chars += 2;
        }
        if (ch4 > '\x007F')
        {
          if (encoderFallbackBuffer == null)
          {
            encoderFallbackBuffer = encoder != null ? encoder.FallbackBuffer : this.encoderFallback.CreateFallbackBuffer();
            encoderFallbackBuffer.InternalInitialize(charEnd - charCount, charEnd, encoder, true);
          }
          encoderFallbackBuffer.InternalFallback(ch4, ref chars);
        }
        else
        {
          if (bytes >= numPtr2)
          {
            if (encoderFallbackBuffer == null || !encoderFallbackBuffer.bFallingBack)
              chars -= 2;
            else
              encoderFallbackBuffer.MovePrevious();
            this.ThrowBytesOverflow(encoder, bytes == numPtr1);
            break;
          }
          *bytes = (byte) ch4;
          ++bytes;
        }
      }
      if (encoder != null)
      {
        if (encoderFallbackBuffer != null && !encoderFallbackBuffer.bUsedEncoder)
          encoder.charLeftOver = char.MinValue;
        encoder.m_charsUsed = (int) (chars - charStart);
      }
      return (int) (bytes - numPtr1);
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
    {
      DecoderReplacementFallback replacementFallback = decoder != null ? decoder.Fallback as DecoderReplacementFallback : this.DecoderFallback as DecoderReplacementFallback;
      if (replacementFallback != null && replacementFallback.MaxCharCount == 1)
        return count;
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      int num1 = count;
      byte[] bytes1 = new byte[1];
      byte* numPtr = bytes + count;
      while (bytes < numPtr)
      {
        byte num2 = *bytes;
        ++bytes;
        if (num2 >= (byte) 128)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.DecoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(numPtr - count, (char*) null);
          }
          bytes1[0] = num2;
          num1 = num1 - 1 + decoderFallbackBuffer.InternalFallback(bytes1, bytes);
        }
      }
      return num1;
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
    {
      byte* numPtr1 = bytes + byteCount;
      byte* numPtr2 = bytes;
      char* chPtr = chars;
      DecoderReplacementFallback replacementFallback = decoder != null ? decoder.Fallback as DecoderReplacementFallback : this.DecoderFallback as DecoderReplacementFallback;
      if (replacementFallback != null && replacementFallback.MaxCharCount == 1)
      {
        char ch = replacementFallback.DefaultString[0];
        if (charCount < byteCount)
        {
          this.ThrowCharsOverflow(decoder, charCount < 1);
          numPtr1 = bytes + charCount;
        }
        while (bytes < numPtr1)
        {
          byte num = *bytes++;
          *chars++ = num < (byte) 128 ? (char) num : ch;
        }
        if (decoder != null)
          decoder.m_bytesUsed = (int) (bytes - numPtr2);
        return (int) (chars - chPtr);
      }
      DecoderFallbackBuffer decoderFallbackBuffer = (DecoderFallbackBuffer) null;
      byte[] bytes1 = new byte[1];
      char* charEnd = chars + charCount;
      while (bytes < numPtr1)
      {
        byte num = *bytes;
        ++bytes;
        if (num >= (byte) 128)
        {
          if (decoderFallbackBuffer == null)
          {
            decoderFallbackBuffer = decoder != null ? decoder.FallbackBuffer : this.DecoderFallback.CreateFallbackBuffer();
            decoderFallbackBuffer.InternalInitialize(numPtr1 - byteCount, charEnd);
          }
          bytes1[0] = num;
          if (!decoderFallbackBuffer.InternalFallback(bytes1, bytes, ref chars))
          {
            --bytes;
            decoderFallbackBuffer.InternalReset();
            this.ThrowCharsOverflow(decoder, chars == chPtr);
            break;
          }
        }
        else
        {
          if (chars >= charEnd)
          {
            --bytes;
            this.ThrowCharsOverflow(decoder, chars == chPtr);
            break;
          }
          *chars = (char) num;
          chars += 2;
        }
      }
      if (decoder != null)
        decoder.m_bytesUsed = (int) (bytes - numPtr2);
      return (int) (chars - chPtr);
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
    [__DynamicallyInvokable]
    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException(nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) charCount + 1L;
      if (this.EncoderFallback.MaxCharCount > 1)
        num *= (long) this.EncoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
      return (int) num;
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
    [__DynamicallyInvokable]
    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      long num = (long) byteCount;
      if (this.DecoderFallback.MaxCharCount > 1)
        num *= (long) this.DecoderFallback.MaxCharCount;
      if (num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
      return (int) num;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, используются ли в текущей кодировке однобайтовые кодовые точки.
    /// </summary>
    /// <returns>
    ///   Данное свойство всегда имеет <see langword="true" />.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override bool IsSingleByte
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    /// <summary>
    ///   Получает декодер, преобразующий ASCII-закодированную последовательность байтов в последовательность символов Юникода.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.Decoder" /> преобразующий ASCII-закодированную последовательность байтов в последовательность символов Юникода.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override Decoder GetDecoder()
    {
      return (Decoder) new DecoderNLS((Encoding) this);
    }

    /// <summary>
    ///   Получает кодировщик, преобразующий последовательность символов Юникода в ASCII-закодированную последовательность байтов.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Text.Encoder" /> Преобразующий последовательность символов Юникода в ASCII-закодированную последовательность байтов.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override Encoder GetEncoder()
    {
      return (Encoder) new EncoderNLS((Encoding) this);
    }
  }
}
