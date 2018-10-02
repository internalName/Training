// Decompiled with JetBrains decompiler
// Type: System.Text.UTF7Encoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  /// <summary>Представляет кодировку UTF-7 символов Юникода.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class UTF7Encoding : Encoding
  {
    private const string base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    private const string directChars = "\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private const string optionalChars = "!\"#$%&*;<=>@[]^_`{|}";
    private byte[] base64Bytes;
    private sbyte[] base64Values;
    private bool[] directEncode;
    [OptionalField(VersionAdded = 2)]
    private bool m_allowOptionals;
    private const int UTF7_CODEPAGE = 65000;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.UTF7Encoding" />.
    /// </summary>
    [__DynamicallyInvokable]
    public UTF7Encoding()
      : this(false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.UTF7Encoding" />.
    ///    Параметр указывает, следует ли разрешить дополнительные символы.
    /// </summary>
    /// <param name="allowOptionals">
    ///   <see langword="true" /> Чтобы указать, что дополнительные символы разрешены; в противном случае — <see langword="false" />.
    /// </param>
    [__DynamicallyInvokable]
    public UTF7Encoding(bool allowOptionals)
      : base(65000)
    {
      this.m_allowOptionals = allowOptionals;
      this.MakeTables();
    }

    private void MakeTables()
    {
      this.base64Bytes = new byte[64];
      for (int index = 0; index < 64; ++index)
        this.base64Bytes[index] = (byte) "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[index];
      this.base64Values = new sbyte[128];
      for (int index = 0; index < 128; ++index)
        this.base64Values[index] = (sbyte) -1;
      for (int index = 0; index < 64; ++index)
        this.base64Values[(int) this.base64Bytes[index]] = (sbyte) index;
      this.directEncode = new bool[128];
      int length1 = "\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Length;
      for (int index = 0; index < length1; ++index)
        this.directEncode[(int) "\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"[index]] = true;
      if (!this.m_allowOptionals)
        return;
      int length2 = "!\"#$%&*;<=>@[]^_`{|}".Length;
      for (int index = 0; index < length2; ++index)
        this.directEncode[(int) "!\"#$%&*;<=>@[]^_`{|}"[index]] = true;
    }

    internal override void SetDefaultFallbacks()
    {
      this.encoderFallback = (EncoderFallback) new EncoderReplacementFallback(string.Empty);
      this.decoderFallback = (DecoderFallback) new UTF7Encoding.DecoderUTF7Fallback();
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.OnDeserializing();
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this.OnDeserialized();
      if (this.m_deserializedFromEverett)
        this.m_allowOptionals = this.directEncode[(int) "!\"#$%&*;<=>@[]^_`{|}"[0]];
      this.MakeTables();
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли указанный объект текущему <see cref="T:System.Text.UTF7Encoding" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект для сравнения с текущим <see cref="T:System.Text.UTF7Encoding" /> объекта.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="value" /> является <see cref="T:System.Text.UTF7Encoding" /> объекта и равен текущему объекту <see cref="T:System.Text.UTF7Encoding" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      UTF7Encoding utF7Encoding = value as UTF7Encoding;
      if (utF7Encoding != null && this.m_allowOptionals == utF7Encoding.m_allowOptionals && this.EncoderFallback.Equals((object) utF7Encoding.EncoderFallback))
        return this.DecoderFallback.Equals((object) utF7Encoding.DecoderFallback);
      return false;
    }

    /// <summary>
    ///   Возвращает хэш-код для текущего <see cref="T:System.Text.UTF7Encoding" /> объекта.
    /// </summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.CodePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode();
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
    ///   Вычисляет количество байтов, полученных при кодировании символов в указанном объекте <see cref="T:System.String" />.
    /// </summary>
    /// <param name="s">
    ///   Объект <see cref="T:System.String" />, содержащий набор символов для кодирования.
    /// </param>
    /// <returns>
    ///   Число байтов, полученных при кодировании заданных символов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="s" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
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
    [ComVisible(false)]
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
    ///   <paramref name="chars" /> — <see langword="null " />(<see langword="Nothing " />в Visual Basic .NET).
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
    ///   <paramref name="s" /> имеет значение <see langword="null " /> (<see langword="Nothing" />).
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
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
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
    ///   Результирующее число символов больше максимального количества, которое можно вернуть как целочисленное значение.
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
    ///   Результирующее число символов больше максимального количества, которое можно вернуть как целочисленное значение.
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
      return this.GetBytes(chars, count, (byte*) null, 0, baseEncoder);
    }

    [SecurityCritical]
    internal override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS baseEncoder)
    {
      UTF7Encoding.Encoder encoder = (UTF7Encoding.Encoder) baseEncoder;
      int num1 = 0;
      int num2 = -1;
      Encoding.EncodingByteBuffer encodingByteBuffer = new Encoding.EncodingByteBuffer((Encoding) this, (EncoderNLS) encoder, bytes, byteCount, chars, charCount);
      if (encoder != null)
      {
        num1 = encoder.bits;
        num2 = encoder.bitCount;
        while (num2 >= 6)
        {
          num2 -= 6;
          if (!encodingByteBuffer.AddByte(this.base64Bytes[num1 >> num2 & 63]))
            this.ThrowBytesOverflow((EncoderNLS) encoder, encodingByteBuffer.Count == 0);
        }
      }
      while (encodingByteBuffer.MoreData)
      {
        char nextChar = encodingByteBuffer.GetNextChar();
        if (nextChar < '\x0080' && this.directEncode[(int) nextChar])
        {
          if (num2 >= 0)
          {
            if (num2 > 0)
            {
              if (encodingByteBuffer.AddByte(this.base64Bytes[num1 << 6 - num2 & 63]))
                num2 = 0;
              else
                break;
            }
            if (encodingByteBuffer.AddByte((byte) 45))
              num2 = -1;
            else
              break;
          }
          if (!encodingByteBuffer.AddByte((byte) nextChar))
            break;
        }
        else if (num2 < 0 && nextChar == '+')
        {
          if (!encodingByteBuffer.AddByte((byte) 43, (byte) 45))
            break;
        }
        else
        {
          if (num2 < 0)
          {
            if (encodingByteBuffer.AddByte((byte) 43))
              num2 = 0;
            else
              break;
          }
          num1 = num1 << 16 | (int) nextChar;
          num2 += 16;
          while (num2 >= 6)
          {
            num2 -= 6;
            if (!encodingByteBuffer.AddByte(this.base64Bytes[num1 >> num2 & 63]))
            {
              num2 += 6;
              encodingByteBuffer.GetNextChar();
              break;
            }
          }
          if (num2 >= 6)
            break;
        }
      }
      if (num2 >= 0 && (encoder == null || encoder.MustFlush))
      {
        if (num2 > 0 && encodingByteBuffer.AddByte(this.base64Bytes[num1 << 6 - num2 & 63]))
          num2 = 0;
        if (encodingByteBuffer.AddByte((byte) 45))
        {
          num1 = 0;
          num2 = -1;
        }
        else
        {
          int nextChar = (int) encodingByteBuffer.GetNextChar();
        }
      }
      if ((IntPtr) bytes != IntPtr.Zero && encoder != null)
      {
        encoder.bits = num1;
        encoder.bitCount = num2;
        encoder.m_charsUsed = encodingByteBuffer.CharsUsed;
      }
      return encodingByteBuffer.Count;
    }

    [SecurityCritical]
    internal override unsafe int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
    {
      return this.GetChars(bytes, count, (char*) null, 0, baseDecoder);
    }

    [SecurityCritical]
    internal override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
    {
      UTF7Encoding.Decoder decoder = (UTF7Encoding.Decoder) baseDecoder;
      Encoding.EncodingCharBuffer encodingCharBuffer = new Encoding.EncodingCharBuffer((Encoding) this, (DecoderNLS) decoder, chars, charCount, bytes, byteCount);
      int num1 = 0;
      int num2 = -1;
      bool flag = false;
      if (decoder != null)
      {
        num1 = decoder.bits;
        num2 = decoder.bitCount;
        flag = decoder.firstByte;
      }
      if (num2 >= 16)
      {
        if (!encodingCharBuffer.AddChar((char) (num1 >> num2 - 16 & (int) ushort.MaxValue)))
          this.ThrowCharsOverflow((DecoderNLS) decoder, true);
        num2 -= 16;
      }
      while (encodingCharBuffer.MoreData)
      {
        byte nextByte = encodingCharBuffer.GetNextByte();
        int num3;
        if (num2 >= 0)
        {
          sbyte base64Value;
          if (nextByte < (byte) 128 && (base64Value = this.base64Values[(int) nextByte]) >= (sbyte) 0)
          {
            flag = false;
            num1 = num1 << 6 | (int) (byte) base64Value;
            num2 += 6;
            if (num2 >= 16)
            {
              num3 = num1 >> num2 - 16 & (int) ushort.MaxValue;
              num2 -= 16;
            }
            else
              continue;
          }
          else
          {
            num2 = -1;
            if (nextByte != (byte) 45)
            {
              if (encodingCharBuffer.Fallback(nextByte))
                continue;
              break;
            }
            if (flag)
              num3 = 43;
            else
              continue;
          }
        }
        else
        {
          if (nextByte == (byte) 43)
          {
            num2 = 0;
            flag = true;
            continue;
          }
          if (nextByte >= (byte) 128)
          {
            if (encodingCharBuffer.Fallback(nextByte))
              continue;
            break;
          }
          num3 = (int) nextByte;
        }
        if (num3 >= 0 && !encodingCharBuffer.AddChar((char) num3))
        {
          if (num2 >= 0)
          {
            encodingCharBuffer.AdjustBytes(1);
            num2 += 16;
            break;
          }
          break;
        }
      }
      if ((IntPtr) chars != IntPtr.Zero && decoder != null)
      {
        if (decoder.MustFlush)
        {
          decoder.bits = 0;
          decoder.bitCount = -1;
          decoder.firstByte = false;
        }
        else
        {
          decoder.bits = num1;
          decoder.bitCount = num2;
          decoder.firstByte = flag;
        }
        decoder.m_bytesUsed = encodingCharBuffer.BytesUsed;
      }
      return encodingCharBuffer.Count;
    }

    /// <summary>
    ///   Получает декодер, преобразующий последовательность байтов в кодировке UTF-7 в последовательность символов Юникода.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.Decoder" /> преобразующий последовательность байтов в кодировке UTF-7 в последовательность символов Юникода.
    /// </returns>
    [__DynamicallyInvokable]
    public override System.Text.Decoder GetDecoder()
    {
      return (System.Text.Decoder) new UTF7Encoding.Decoder(this);
    }

    /// <summary>
    ///   Получает кодировщик, преобразующий последовательность символов Юникода в последовательность байтов в кодировке UTF-7.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.Encoder" /> преобразующий последовательность символов Юникода в последовательность байтов в кодировке UTF-7.
    /// </returns>
    [__DynamicallyInvokable]
    public override System.Text.Encoder GetEncoder()
    {
      return (System.Text.Encoder) new UTF7Encoding.Encoder(this);
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
      long num = (long) charCount * 3L + 2L;
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
    ///   Результирующее число символов больше максимального количества, которое можно вернуть как целочисленное значение.
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
      int num = byteCount;
      if (num == 0)
        num = 1;
      return num;
    }

    [Serializable]
    private class Decoder : DecoderNLS, ISerializable
    {
      internal int bits;
      internal int bitCount;
      internal bool firstByte;

      public Decoder(UTF7Encoding encoding)
        : base((Encoding) encoding)
      {
      }

      internal Decoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        this.bits = (int) info.GetValue(nameof (bits), typeof (int));
        this.bitCount = (int) info.GetValue(nameof (bitCount), typeof (int));
        this.firstByte = (bool) info.GetValue(nameof (firstByte), typeof (bool));
        this.m_encoding = (Encoding) info.GetValue("encoding", typeof (Encoding));
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        info.AddValue("encoding", (object) this.m_encoding);
        info.AddValue("bits", this.bits);
        info.AddValue("bitCount", this.bitCount);
        info.AddValue("firstByte", this.firstByte);
      }

      public override void Reset()
      {
        this.bits = 0;
        this.bitCount = -1;
        this.firstByte = false;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }

      internal override bool HasState
      {
        get
        {
          return this.bitCount != -1;
        }
      }
    }

    [Serializable]
    private class Encoder : EncoderNLS, ISerializable
    {
      internal int bits;
      internal int bitCount;

      public Encoder(UTF7Encoding encoding)
        : base((Encoding) encoding)
      {
      }

      internal Encoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        this.bits = (int) info.GetValue(nameof (bits), typeof (int));
        this.bitCount = (int) info.GetValue(nameof (bitCount), typeof (int));
        this.m_encoding = (Encoding) info.GetValue("encoding", typeof (Encoding));
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        info.AddValue("encoding", (object) this.m_encoding);
        info.AddValue("bits", this.bits);
        info.AddValue("bitCount", this.bitCount);
      }

      public override void Reset()
      {
        this.bitCount = -1;
        this.bits = 0;
        if (this.m_fallbackBuffer == null)
          return;
        this.m_fallbackBuffer.Reset();
      }

      internal override bool HasState
      {
        get
        {
          if (this.bits == 0)
            return this.bitCount != -1;
          return true;
        }
      }
    }

    [Serializable]
    internal sealed class DecoderUTF7Fallback : DecoderFallback
    {
      public override DecoderFallbackBuffer CreateFallbackBuffer()
      {
        return (DecoderFallbackBuffer) new UTF7Encoding.DecoderUTF7FallbackBuffer(this);
      }

      public override int MaxCharCount
      {
        get
        {
          return 1;
        }
      }

      public override bool Equals(object value)
      {
        return value is UTF7Encoding.DecoderUTF7Fallback;
      }

      public override int GetHashCode()
      {
        return 984;
      }
    }

    internal sealed class DecoderUTF7FallbackBuffer : DecoderFallbackBuffer
    {
      private int iCount = -1;
      private char cFallback;
      private int iSize;

      public DecoderUTF7FallbackBuffer(UTF7Encoding.DecoderUTF7Fallback fallback)
      {
      }

      public override bool Fallback(byte[] bytesUnknown, int index)
      {
        this.cFallback = (char) bytesUnknown[0];
        if (this.cFallback == char.MinValue)
          return false;
        this.iCount = this.iSize = 1;
        return true;
      }

      public override char GetNextChar()
      {
        if (this.iCount-- > 0)
          return this.cFallback;
        return char.MinValue;
      }

      public override bool MovePrevious()
      {
        if (this.iCount >= 0)
          ++this.iCount;
        if (this.iCount >= 0)
          return this.iCount <= this.iSize;
        return false;
      }

      public override int Remaining
      {
        get
        {
          if (this.iCount <= 0)
            return 0;
          return this.iCount;
        }
      }

      [SecuritySafeCritical]
      public override unsafe void Reset()
      {
        this.iCount = -1;
        this.byteStart = (byte*) null;
      }

      [SecurityCritical]
      internal override unsafe int InternalFallback(byte[] bytes, byte* pBytes)
      {
        if (bytes.Length != 1)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
        return bytes[0] != (byte) 0 ? 1 : 0;
      }
    }
  }
}
