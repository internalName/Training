// Decompiled with JetBrains decompiler
// Type: System.Text.Encoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Text
{
  /// <summary>
  ///   Представляет кодировку символов.
  /// 
  ///   Исходный код .NET Framework для этого типа см. в указанном источнике.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Encoding : ICloneable
  {
    private static readonly UTF8Encoding.UTF8EncodingSealed s_defaultUtf8EncodingNoBom = new UTF8Encoding.UTF8EncodingSealed(false);
    [OptionalField(VersionAdded = 2)]
    private bool m_isReadOnly = true;
    private static volatile Encoding defaultEncoding;
    private static volatile Encoding unicodeEncoding;
    private static volatile Encoding bigEndianUnicode;
    private static volatile Encoding utf7Encoding;
    private static volatile Encoding utf8Encoding;
    private static volatile Encoding utf32Encoding;
    private static volatile Encoding asciiEncoding;
    private static volatile Encoding latin1Encoding;
    private static volatile Hashtable encodings;
    private const int MIMECONTF_MAILNEWS = 1;
    private const int MIMECONTF_BROWSER = 2;
    private const int MIMECONTF_SAVABLE_MAILNEWS = 256;
    private const int MIMECONTF_SAVABLE_BROWSER = 512;
    private const int CodePageDefault = 0;
    private const int CodePageNoOEM = 1;
    private const int CodePageNoMac = 2;
    private const int CodePageNoThread = 3;
    private const int CodePageNoSymbol = 42;
    private const int CodePageUnicode = 1200;
    private const int CodePageBigEndian = 1201;
    private const int CodePageWindows1252 = 1252;
    private const int CodePageMacGB2312 = 10008;
    private const int CodePageGB2312 = 20936;
    private const int CodePageMacKorean = 10003;
    private const int CodePageDLLKorean = 20949;
    private const int ISO2022JP = 50220;
    private const int ISO2022JPESC = 50221;
    private const int ISO2022JPSISO = 50222;
    private const int ISOKorean = 50225;
    private const int ISOSimplifiedCN = 50227;
    private const int EUCJP = 51932;
    private const int ChineseHZ = 52936;
    private const int DuplicateEUCCN = 51936;
    private const int EUCCN = 936;
    private const int EUCKR = 51949;
    internal const int CodePageASCII = 20127;
    internal const int ISO_8859_1 = 28591;
    private const int ISCIIAssemese = 57006;
    private const int ISCIIBengali = 57003;
    private const int ISCIIDevanagari = 57002;
    private const int ISCIIGujarathi = 57010;
    private const int ISCIIKannada = 57008;
    private const int ISCIIMalayalam = 57009;
    private const int ISCIIOriya = 57007;
    private const int ISCIIPanjabi = 57011;
    private const int ISCIITamil = 57004;
    private const int ISCIITelugu = 57005;
    private const int GB18030 = 54936;
    private const int ISO_8859_8I = 38598;
    private const int ISO_8859_8_Visual = 28598;
    private const int ENC50229 = 50229;
    private const int CodePageUTF7 = 65000;
    private const int CodePageUTF8 = 65001;
    private const int CodePageUTF32 = 12000;
    private const int CodePageUTF32BE = 12001;
    internal int m_codePage;
    internal CodePageDataItem dataItem;
    [NonSerialized]
    internal bool m_deserializedFromEverett;
    [OptionalField(VersionAdded = 2)]
    internal EncoderFallback encoderFallback;
    [OptionalField(VersionAdded = 2)]
    internal DecoderFallback decoderFallback;
    private static object s_InternalSyncObject;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.Encoding" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected Encoding()
      : this(0)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.Encoding" />, соответствующий заданной кодовой странице.
    /// </summary>
    /// <param name="codePage">
    ///   Идентификатор кодовой страницы предпочтительной кодировки.
    /// 
    ///   -или-
    /// 
    ///   0, если требуется использовать кодировку по умолчанию.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="codePage" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    protected Encoding(int codePage)
    {
      if (codePage < 0)
        throw new ArgumentOutOfRangeException(nameof (codePage));
      this.m_codePage = codePage;
      this.SetDefaultFallbacks();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.Encoding" />, соответствующий заданной кодовой странице, с использованием указанных стратегий резервирования кодировщика и декодера.
    /// </summary>
    /// <param name="codePage">
    ///   Идентификатор кодовой страницы кодировки.
    /// </param>
    /// <param name="encoderFallback">
    ///   Объект, предоставляющий процедуру обработки ошибок, когда символ не может быть закодирован с использованием текущей кодировки.
    /// </param>
    /// <param name="decoderFallback">
    ///   Объект, предоставляющий процедуру обработки ошибок, когда последовательность байтов не может быть декодирована с использованием текущей кодировки.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="codePage" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    protected Encoding(int codePage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
    {
      if (codePage < 0)
        throw new ArgumentOutOfRangeException(nameof (codePage));
      this.m_codePage = codePage;
      this.encoderFallback = encoderFallback ?? (EncoderFallback) new InternalEncoderBestFitFallback(this);
      this.decoderFallback = decoderFallback ?? (DecoderFallback) new InternalDecoderBestFitFallback(this);
    }

    internal virtual void SetDefaultFallbacks()
    {
      this.encoderFallback = (EncoderFallback) new InternalEncoderBestFitFallback(this);
      this.decoderFallback = (DecoderFallback) new InternalDecoderBestFitFallback(this);
    }

    internal void OnDeserializing()
    {
      this.encoderFallback = (EncoderFallback) null;
      this.decoderFallback = (DecoderFallback) null;
      this.m_isReadOnly = true;
    }

    internal void OnDeserialized()
    {
      if (this.encoderFallback == null || this.decoderFallback == null)
      {
        this.m_deserializedFromEverett = true;
        this.SetDefaultFallbacks();
      }
      this.dataItem = (CodePageDataItem) null;
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
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.dataItem = (CodePageDataItem) null;
    }

    internal void DeserializeEncoding(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.m_codePage = (int) info.GetValue("m_codePage", typeof (int));
      this.dataItem = (CodePageDataItem) null;
      try
      {
        this.m_isReadOnly = (bool) info.GetValue("m_isReadOnly", typeof (bool));
        this.encoderFallback = (EncoderFallback) info.GetValue("encoderFallback", typeof (EncoderFallback));
        this.decoderFallback = (DecoderFallback) info.GetValue("decoderFallback", typeof (DecoderFallback));
      }
      catch (SerializationException ex)
      {
        this.m_deserializedFromEverett = true;
        this.m_isReadOnly = true;
        this.SetDefaultFallbacks();
      }
    }

    internal void SerializeEncoding(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.AddValue("m_isReadOnly", this.m_isReadOnly);
      info.AddValue("encoderFallback", (object) this.EncoderFallback);
      info.AddValue("decoderFallback", (object) this.DecoderFallback);
      info.AddValue("m_codePage", this.m_codePage);
      info.AddValue("dataItem", (object) null);
      info.AddValue("Encoding+m_codePage", this.m_codePage);
      info.AddValue("Encoding+dataItem", (object) null);
    }

    /// <summary>
    ///   Преобразует весь массив байтов из одной кодировки в другую.
    /// </summary>
    /// <param name="srcEncoding">
    ///   Формат кодировки параметра <paramref name="bytes" />.
    /// </param>
    /// <param name="dstEncoding">Целевой формат кодировки.</param>
    /// <param name="bytes">Преобразуемые байты.</param>
    /// <returns>
    ///   Массив типа <see cref="T:System.Byte" />, содержащий результаты преобразования <paramref name="bytes" /> из <paramref name="srcEncoding" /> в <paramref name="dstEncoding" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="srcEncoding" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="dstEncoding" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   srcEncoding.<see cref="P:System.Text.Encoding.DecoderFallback" /> равен <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   dstEncoding.<see cref="P:System.Text.Encoding.EncoderFallback" /> равен <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes)
    {
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes));
      return Encoding.Convert(srcEncoding, dstEncoding, bytes, 0, bytes.Length);
    }

    /// <summary>
    ///   Преобразует диапазон байтов в массиве байтов из одной кодировки в другую.
    /// </summary>
    /// <param name="srcEncoding">
    ///   Кодировка исходного массива, <paramref name="bytes" />.
    /// </param>
    /// <param name="dstEncoding">Кодировка выходного массива.</param>
    /// <param name="bytes">Преобразуемый массив байтов.</param>
    /// <param name="index">
    ///   Индекс первого элемента преобразуемого массива байтов <paramref name="bytes" />.
    /// </param>
    /// <param name="count">
    ///   Число байтов, которые требуется преобразовать.
    /// </param>
    /// <returns>
    ///   Массив типа <see cref="T:System.Byte" />, содержащий результат преобразования диапазона байтов из массива <paramref name="bytes" /> из <paramref name="srcEncoding" /> в <paramref name="dstEncoding" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="srcEncoding" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="dstEncoding" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> и <paramref name="count" /> не определяют допустимый диапазон в массиве байтов.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   srcEncoding.<see cref="P:System.Text.Encoding.DecoderFallback" /> равен <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   dstEncoding.<see cref="P:System.Text.Encoding.EncoderFallback" /> равен <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes, int index, int count)
    {
      if (srcEncoding == null || dstEncoding == null)
        throw new ArgumentNullException(srcEncoding == null ? nameof (srcEncoding) : nameof (dstEncoding), Environment.GetResourceString("ArgumentNull_Array"));
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      return dstEncoding.GetBytes(srcEncoding.GetChars(bytes, index, count));
    }

    private static object InternalSyncObject
    {
      get
      {
        if (Encoding.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref Encoding.s_InternalSyncObject, obj, (object) null);
        }
        return Encoding.s_InternalSyncObject;
      }
    }

    /// <summary>Регистрирует поставщик кодировки.</summary>
    /// <param name="provider">
    ///   Подкласс класса <see cref="T:System.Text.EncodingProvider" />, который предоставляет доступ к дополнительным кодировкам символов.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="provider" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void RegisterProvider(EncodingProvider provider)
    {
      EncodingProvider.AddProvider(provider);
    }

    /// <summary>
    ///   Возвращает кодировку, связанную с указанным идентификатором кодовой страницы.
    /// </summary>
    /// <param name="codepage">
    ///   Идентификатор кодовой страницы предпочтительной кодировки.
    ///    Возможные значения перечислены в столбце кодовой страницы таблицы, которая отображается в теме класса <see cref="T:System.Text.Encoding" />.
    /// 
    ///   -или-
    /// 
    ///   0 (ноль), если требуется использовать кодировку по умолчанию.
    /// </param>
    /// <returns>Кодирование, связанное с заданной страницей кода.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="codepage" /> меньше нуля или больше, чем 65535.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="codepage" /> не поддерживается используемой платформой.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="codepage" /> не поддерживается используемой платформой.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Encoding GetEncoding(int codepage)
    {
      Encoding encoding = EncodingProvider.GetEncodingFromProvider(codepage);
      if (encoding != null)
        return encoding;
      if (codepage < 0 || codepage > (int) ushort.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (codepage), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) (int) ushort.MaxValue));
      if (Encoding.encodings != null)
        encoding = (Encoding) Encoding.encodings[(object) codepage];
      if (encoding == null)
      {
        lock (Encoding.InternalSyncObject)
        {
          if (Encoding.encodings == null)
            Encoding.encodings = new Hashtable();
          if ((encoding = (Encoding) Encoding.encodings[(object) codepage]) != null)
            return encoding;
          switch (codepage)
          {
            case 0:
              encoding = Encoding.Default;
              break;
            case 1:
            case 2:
            case 3:
            case 42:
              throw new ArgumentException(Environment.GetResourceString("Argument_CodepageNotSupported", (object) codepage), nameof (codepage));
            case 1200:
              encoding = Encoding.Unicode;
              break;
            case 1201:
              encoding = Encoding.BigEndianUnicode;
              break;
            case 1252:
              encoding = (Encoding) new SBCSCodePageEncoding(codepage);
              break;
            case 20127:
              encoding = Encoding.ASCII;
              break;
            case 28591:
              encoding = Encoding.Latin1;
              break;
            case 65001:
              encoding = Encoding.UTF8;
              break;
            default:
              encoding = Encoding.GetEncodingCodePage(codepage) ?? Encoding.GetEncodingRare(codepage);
              break;
          }
          Encoding.encodings.Add((object) codepage, (object) encoding);
        }
      }
      return encoding;
    }

    /// <summary>
    ///   Возвращает кодировку, связанную с указанным идентификатором кодовой страницы.
    ///    С помощью параметров задается обработчик ошибок для символов, которые не удается закодировать, и последовательностей байтов, которые не удается декодировать.
    /// </summary>
    /// <param name="codepage">
    ///   Идентификатор кодовой страницы предпочтительной кодировки.
    ///    Возможные значения перечислены в столбце кодовой страницы таблицы, которая отображается в теме класса <see cref="T:System.Text.Encoding" />.
    /// 
    ///   -или-
    /// 
    ///   0 (ноль), если требуется использовать кодировку по умолчанию.
    /// </param>
    /// <param name="encoderFallback">
    ///   Объект, предоставляющий процедуру обработки ошибок, когда символ не может быть закодирован с использованием текущей кодировки.
    /// </param>
    /// <param name="decoderFallback">
    ///   Объект, предоставляющий процедуру обработки ошибок, когда последовательность байтов не может быть декодирована с использованием текущей кодировки.
    /// </param>
    /// <returns>Кодирование, связанное с заданной страницей кода.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="codepage" /> меньше нуля или больше, чем 65535.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="codepage" /> не поддерживается используемой платформой.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="codepage" /> не поддерживается используемой платформой.
    /// </exception>
    [__DynamicallyInvokable]
    public static Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
    {
      Encoding encodingFromProvider = EncodingProvider.GetEncodingFromProvider(codepage, encoderFallback, decoderFallback);
      if (encodingFromProvider != null)
        return encodingFromProvider;
      Encoding encoding = (Encoding) Encoding.GetEncoding(codepage).Clone();
      encoding.EncoderFallback = encoderFallback;
      encoding.DecoderFallback = decoderFallback;
      return encoding;
    }

    [SecurityCritical]
    private static Encoding GetEncodingRare(int codepage)
    {
      switch (codepage)
      {
        case 10003:
          return (Encoding) new DBCSCodePageEncoding(10003, 20949);
        case 10008:
          return (Encoding) new DBCSCodePageEncoding(10008, 20936);
        case 12000:
          return Encoding.UTF32;
        case 12001:
          return (Encoding) new UTF32Encoding(true, true);
        case 38598:
          return (Encoding) new SBCSCodePageEncoding(codepage, 28598);
        case 50220:
        case 50221:
        case 50222:
        case 50225:
        case 52936:
          return (Encoding) new ISO2022Encoding(codepage);
        case 50227:
        case 51936:
          return (Encoding) new DBCSCodePageEncoding(codepage, 936);
        case 50229:
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_CodePage50229"));
        case 51932:
          return (Encoding) new EUCJPEncoding();
        case 51949:
          return (Encoding) new DBCSCodePageEncoding(codepage, 20949);
        case 54936:
          return (Encoding) new GB18030Encoding();
        case 57002:
        case 57003:
        case 57004:
        case 57005:
        case 57006:
        case 57007:
        case 57008:
        case 57009:
        case 57010:
        case 57011:
          return (Encoding) new ISCIIEncoding(codepage);
        case 65000:
          return Encoding.UTF7;
        default:
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", (object) codepage));
      }
    }

    [SecurityCritical]
    private static Encoding GetEncodingCodePage(int CodePage)
    {
      switch (BaseCodePageEncoding.GetCodePageByteSize(CodePage))
      {
        case 1:
          return (Encoding) new SBCSCodePageEncoding(CodePage);
        case 2:
          return (Encoding) new DBCSCodePageEncoding(CodePage);
        default:
          return (Encoding) null;
      }
    }

    /// <summary>
    ///   Возвращает кодировку, связанную с указанным именем кодовой страницы.
    /// </summary>
    /// <param name="name">
    ///   Имя кодовой страницы предпочтительной кодировки.
    ///    Любое значение, возвращаемое свойством <see cref="P:System.Text.Encoding.WebName" />, является допустимым.
    ///    Возможные значения перечислены в столбце "Имя" таблицы, отображаемой в разделе класса <see cref="T:System.Text.Encoding" />.
    /// </param>
    /// <returns>Кодировка, связанная с указанной кодовой страницей.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> не допустимое имя кодовой страницы.
    /// 
    ///   -или-
    /// 
    ///   Указывает кодовую страницу <paramref name="name" /> не поддерживается используемой платформой.
    /// </exception>
    [__DynamicallyInvokable]
    public static Encoding GetEncoding(string name)
    {
      return EncodingProvider.GetEncodingFromProvider(name) ?? Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name));
    }

    /// <summary>
    ///   Возвращает кодировку, связанную с указанным именем кодовой страницы.
    ///    С помощью параметров задается обработчик ошибок для символов, которые не удается закодировать, и последовательностей байтов, которые не удается декодировать.
    /// </summary>
    /// <param name="name">
    ///   Имя кодовой страницы предпочтительной кодировки.
    ///    Любое значение, возвращаемое свойством <see cref="P:System.Text.Encoding.WebName" />, является допустимым.
    ///    Возможные значения перечислены в столбце "Имя" таблицы, отображаемой в разделе класса <see cref="T:System.Text.Encoding" />.
    /// </param>
    /// <param name="encoderFallback">
    ///   Объект, предоставляющий процедуру обработки ошибок, когда символ не может быть закодирован с использованием текущей кодировки.
    /// </param>
    /// <param name="decoderFallback">
    ///   Объект, предоставляющий процедуру обработки ошибок, когда последовательность байтов не может быть декодирована с использованием текущей кодировки.
    /// </param>
    /// <returns>Кодирование, связанное с заданной страницей кода.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> не допустимое имя кодовой страницы.
    /// 
    ///   -или-
    /// 
    ///   Указывает кодовую страницу <paramref name="name" /> не поддерживается используемой платформой.
    /// </exception>
    [__DynamicallyInvokable]
    public static Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
    {
      return EncodingProvider.GetEncodingFromProvider(name, encoderFallback, decoderFallback) ?? Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name), encoderFallback, decoderFallback);
    }

    /// <summary>Возвращает массив, содержащий все кодировки.</summary>
    /// <returns>Массив, содержащий все кодировки.</returns>
    public static EncodingInfo[] GetEncodings()
    {
      return EncodingTable.GetEncodings();
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает последовательность байтов, задающую используемую кодировку.
    /// </summary>
    /// <returns>
    ///   Массив байтов, в котором содержится последовательность байтов, задающая используемую кодировку.
    /// 
    ///   -или-
    /// 
    ///   Массив байтов нулевой длины, если преамбула не требуется.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual byte[] GetPreamble()
    {
      return EmptyArray<byte>.Value;
    }

    private void GetDataItem()
    {
      if (this.dataItem != null)
        return;
      this.dataItem = EncodingTable.GetCodePageDataItem(this.m_codePage);
      if (this.dataItem == null)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", (object) this.m_codePage));
    }

    /// <summary>
    ///   При переопределении в производном классе получает имя текущей кодировки, которое может использоваться с тегами текста сообщения почтового агента.
    /// </summary>
    /// <returns>
    ///   Имя текущего объекта <see cref="T:System.Text.Encoding" />, которое может использоваться с тегами текста сообщения почтового агента.
    /// 
    ///   -или-
    /// 
    ///   Пустая строка (""), если текущий объект <see cref="T:System.Text.Encoding" /> не может использоваться.
    /// </returns>
    public virtual string BodyName
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return this.dataItem.BodyName;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает описание текущей кодировки, которое может быть прочитано пользователем.
    /// </summary>
    /// <returns>
    ///   Описание текущего объекта <see cref="T:System.Text.Encoding" />, которое может быть прочитано пользователем.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string EncodingName
    {
      [__DynamicallyInvokable] get
      {
        return Environment.GetResourceString("Globalization.cp_" + (object) this.m_codePage);
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает имя текущей кодировки, которое может использоваться с тегами заголовка сообщения почтового агента.
    /// </summary>
    /// <returns>
    ///   Имя текущего объекта <see cref="T:System.Text.Encoding" />, которое может использоваться с тегами заголовка сообщения почтового агента.
    /// 
    ///   -или-
    /// 
    ///   Пустая строка (""), если текущий объект <see cref="T:System.Text.Encoding" /> не может использоваться.
    /// </returns>
    public virtual string HeaderName
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return this.dataItem.HeaderName;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает для текущей кодировки имя, зарегистрированное в IANA (Internet Assigned Numbers Authority).
    /// </summary>
    /// <returns>
    ///   Имя IANA для текущего объекта <see cref="T:System.Text.Encoding" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string WebName
    {
      [__DynamicallyInvokable] get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return this.dataItem.WebName;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает кодовую страницу операционной системы Windows, наиболее точно соответствующую текущей кодировке.
    /// </summary>
    /// <returns>
    ///   Кодовая страница операционной системы Windows, наиболее точно соответствующая текущему объекту <see cref="T:System.Text.Encoding" />.
    /// </returns>
    public virtual int WindowsCodePage
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return this.dataItem.UIFamilyCodePage;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает значение, указывающее, может ли текущая кодировка использоваться клиентами браузера для отображения содержимого.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если текущий объект <see cref="T:System.Text.Encoding" /> может использоваться клиентами браузера для отображения содержимого; в противоположном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsBrowserDisplay
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return (this.dataItem.Flags & 2U) > 0U;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает значение, указывающее, может ли текущая кодировка использоваться клиентами браузера для сохранения содержимого.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если текущий объект <see cref="T:System.Text.Encoding" /> может использоваться клиентами браузера для сохранения содержимого; в противоположном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsBrowserSave
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return (this.dataItem.Flags & 512U) > 0U;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает значение, указывающее, может ли текущая кодировка использоваться клиентами электронной почты и новостей для отображения содержимого.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если текущий объект <see cref="T:System.Text.Encoding" /> может использоваться клиентами почты и новостей для отображения содержимого; в противоположном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsMailNewsDisplay
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return (this.dataItem.Flags & 1U) > 0U;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает значение, указывающее, может ли текущая кодировка использоваться клиентами электронной почты и новостей для сохранения содержимого.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если текущий объект <see cref="T:System.Text.Encoding" /> может использоваться клиентами почты и новостей для сохранения содержимого; в противоположном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsMailNewsSave
    {
      get
      {
        if (this.dataItem == null)
          this.GetDataItem();
        return (this.dataItem.Flags & 256U) > 0U;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает значение, указывающее, используются ли в текущей кодировке однобайтовые кодовые точки.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если в текущем объекте <see cref="T:System.Text.Encoding" /> используются однобайтовые кодовые точки; в противоположном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual bool IsSingleByte
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает или задает объект <see cref="T:System.Text.EncoderFallback" /> для текущего объекта <see cref="T:System.Text.Encoding" />.
    /// </summary>
    /// <returns>
    ///   Резервный объект кодировщика для текущего объекта <see cref="T:System.Text.Encoding" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   В операции задания значением является <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Невозможно назначить значение в наборе операций, поскольку текущий <see cref="T:System.Text.Encoding" /> объект доступен только для чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public EncoderFallback EncoderFallback
    {
      [__DynamicallyInvokable] get
      {
        return this.encoderFallback;
      }
      set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.encoderFallback = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает объект <see cref="T:System.Text.DecoderFallback" /> для текущего объекта <see cref="T:System.Text.Encoding" />.
    /// </summary>
    /// <returns>
    ///   Резервный объект декодера для текущего объекта <see cref="T:System.Text.Encoding" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   В операции задания значением является <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Невозможно назначить значение в наборе операций, поскольку текущий <see cref="T:System.Text.Encoding" /> объект доступен только для чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public DecoderFallback DecoderFallback
    {
      [__DynamicallyInvokable] get
      {
        return this.decoderFallback;
      }
      set
      {
        if (this.IsReadOnly)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.decoderFallback = value;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе создается неполная копия текущего объекта <see cref="T:System.Text.Encoding" />.
    /// </summary>
    /// <returns>
    ///   Копия текущего объекта <see cref="T:System.Text.Encoding" />.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual object Clone()
    {
      Encoding encoding = (Encoding) this.MemberwiseClone();
      encoding.m_isReadOnly = false;
      return (object) encoding;
    }

    /// <summary>
    ///   При переопределении в производном классе получает значение, указывающее, является ли текущая кодировка доступной только для чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see cref="T:System.Text.Encoding" /> доступен только для чтения, в противном случае — значение <see langword="false" />.
    ///    Значение по умолчанию — <see langword="true" />.
    /// </returns>
    [ComVisible(false)]
    public bool IsReadOnly
    {
      get
      {
        return this.m_isReadOnly;
      }
    }

    /// <summary>
    ///   Получает кодировку для набора символов ASCII (7-разрядных).
    /// </summary>
    /// <returns>Кодировка набора символов ASCII (7-разрядных).</returns>
    [__DynamicallyInvokable]
    public static Encoding ASCII
    {
      [__DynamicallyInvokable] get
      {
        if (Encoding.asciiEncoding == null)
          Encoding.asciiEncoding = (Encoding) new ASCIIEncoding();
        return Encoding.asciiEncoding;
      }
    }

    private static Encoding Latin1
    {
      get
      {
        if (Encoding.latin1Encoding == null)
          Encoding.latin1Encoding = (Encoding) new Latin1Encoding();
        return Encoding.latin1Encoding;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе вычисляет количество байтов, полученных при кодировании всех символов из заданного массива символов.
    /// </summary>
    /// <param name="chars">
    ///   Массив символов, содержащий символы, которые требуется закодировать.
    /// </param>
    /// <returns>
    ///   Количество байтов, полученных при кодировании всех символов из указанного массива символов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="chars" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int GetByteCount(char[] chars)
    {
      if (chars == null)
        throw new ArgumentNullException(nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      return this.GetByteCount(chars, 0, chars.Length);
    }

    /// <summary>
    ///   При переопределении в производном классе вычисляет число байтов, полученных при кодировании символов в заданной строке.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая набор символов для кодирования.
    /// </param>
    /// <returns>
    ///   Число байтов, полученных при кодировании заданных символов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int GetByteCount(string s)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      char[] charArray = s.ToCharArray();
      return this.GetByteCount(charArray, 0, charArray.Length);
    }

    /// <summary>
    ///   При переопределении в производном классе вычисляет количество байтов, полученных при кодировании набора символов из указанного массива символов.
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
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract int GetByteCount(char[] chars, int index, int count);

    /// <summary>
    ///   При переопределении в производном классе вычисляет количество байтов, полученных при кодировании набора символов, начиная с заданного указателя символа.
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
    public virtual unsafe int GetByteCount(char* chars, int count)
    {
      if ((IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException(nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      char[] chars1 = new char[count];
      for (int index = 0; index < count; ++index)
        chars1[index] = chars[index];
      return this.GetByteCount(chars1, 0, count);
    }

    [SecurityCritical]
    internal virtual unsafe int GetByteCount(char* chars, int count, EncoderNLS encoder)
    {
      return this.GetByteCount(chars, count);
    }

    /// <summary>
    ///   При переопределении в производном классе кодирует все символы из указанного массива символов в последовательность байтов.
    /// </summary>
    /// <param name="chars">
    ///   Массив символов, содержащий символы, которые требуется закодировать.
    /// </param>
    /// <returns>
    ///   Массив байтов, содержащий результаты кодирования указанного набора символов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="chars" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual byte[] GetBytes(char[] chars)
    {
      if (chars == null)
        throw new ArgumentNullException(nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      return this.GetBytes(chars, 0, chars.Length);
    }

    /// <summary>
    ///   При переопределении в производном классе кодирует набор символов из указанного массива символов в последовательность байтов.
    /// </summary>
    /// <param name="chars">
    ///   Массив символов, содержащий набор кодируемых символов.
    /// </param>
    /// <param name="index">Индекс первого кодируемого символа.</param>
    /// <param name="count">Число кодируемых символов.</param>
    /// <returns>
    ///   Массив байтов, содержащий результаты кодирования указанного набора символов.
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
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual byte[] GetBytes(char[] chars, int index, int count)
    {
      byte[] bytes = new byte[this.GetByteCount(chars, index, count)];
      this.GetBytes(chars, index, count, bytes, 0);
      return bytes;
    }

    /// <summary>
    ///   При переопределении в производном классе кодирует набор символов из указанного массива символов в указанный массив байтов.
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
    [__DynamicallyInvokable]
    public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex);

    /// <summary>
    ///   При переопределении в производном классе кодирует все символы заданной строки в последовательность байтов.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая символы, которые требуется закодировать.
    /// </param>
    /// <returns>
    ///   Массив байтов, содержащий результаты кодирования указанного набора символов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual byte[] GetBytes(string s)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s), Environment.GetResourceString("ArgumentNull_String"));
      byte[] bytes = new byte[this.GetByteCount(s)];
      this.GetBytes(s, 0, s.Length, bytes, 0);
      return bytes;
    }

    /// <summary>
    ///   При переопределении в производном классе кодирует набор символов из заданной строки в заданный массив байтов.
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
    [__DynamicallyInvokable]
    public virtual int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      return this.GetBytes(s.ToCharArray(), charIndex, charCount, bytes, byteIndex);
    }

    [SecurityCritical]
    internal virtual unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
    {
      return this.GetBytes(chars, charCount, bytes, byteCount);
    }

    /// <summary>
    ///   При переопределении в производном классе кодирует набор символов, начало которого задается указателем символа, в последовательность байтов, которые сохраняются, начиная с заданного указателя байта.
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
    public virtual unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
    {
      if ((IntPtr) bytes == IntPtr.Zero || (IntPtr) chars == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) bytes == IntPtr.Zero ? nameof (bytes) : nameof (chars), Environment.GetResourceString("ArgumentNull_Array"));
      if (charCount < 0 || byteCount < 0)
        throw new ArgumentOutOfRangeException(charCount < 0 ? nameof (charCount) : nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      char[] chars1 = new char[charCount];
      for (int index = 0; index < charCount; ++index)
        chars1[index] = chars[index];
      byte[] bytes1 = new byte[byteCount];
      int bytes2 = this.GetBytes(chars1, 0, charCount, bytes1, 0);
      if (bytes2 < byteCount)
        byteCount = bytes2;
      for (int index = 0; index < byteCount; ++index)
        bytes[index] = bytes1[index];
      return byteCount;
    }

    /// <summary>
    ///   При переопределении в производном классе вычисляет количество символов, полученных при декодировании всех байтов из заданного массива байтов.
    /// </summary>
    /// <param name="bytes">
    ///   Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <returns>
    ///   Число символов, полученных при декодировании заданной последовательности байтов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int GetCharCount(byte[] bytes)
    {
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      return this.GetCharCount(bytes, 0, bytes.Length);
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
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract int GetCharCount(byte[] bytes, int index, int count);

    /// <summary>
    ///   При переопределении в производном классе вычисляет количество символов, полученных при декодировании последовательности байтов, начало которой задается указателем байтов.
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
    public virtual unsafe int GetCharCount(byte* bytes, int count)
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

    [SecurityCritical]
    internal virtual unsafe int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
    {
      return this.GetCharCount(bytes, count);
    }

    /// <summary>
    ///   При переопределении в производном классе декодирует все байты из указанного массива байтов в набор символов.
    /// </summary>
    /// <param name="bytes">
    ///   Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <returns>
    ///   Массив символов, содержащий результаты декодирования указанной последовательности байтов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual char[] GetChars(byte[] bytes)
    {
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      return this.GetChars(bytes, 0, bytes.Length);
    }

    /// <summary>
    ///   При переопределении в производном классе декодирует последовательность байтов из указанного массива байтов в набор символов.
    /// </summary>
    /// <param name="bytes">
    ///   Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <param name="index">Индекс первого декодируемого байта.</param>
    /// <param name="count">Число байтов для декодирования.</param>
    /// <returns>
    ///   Массив символов, содержащий результаты декодирования указанной последовательности байтов.
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
    [__DynamicallyInvokable]
    public virtual char[] GetChars(byte[] bytes, int index, int count)
    {
      char[] chars = new char[this.GetCharCount(bytes, index, count)];
      this.GetChars(bytes, index, count, chars, 0);
      return chars;
    }

    /// <summary>
    ///   При переопределении в производном классе декодирует последовательность байтов из указанного массива байтов в указанный массив символов.
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
    [__DynamicallyInvokable]
    public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

    /// <summary>
    ///   При переопределении в производном классе декодирует последовательность байтов, которая начинается с заданного указателя байта, в набор символов, которые сохраняются, начиная с заданного указателя символа.
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
    public virtual unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
    {
      if ((IntPtr) chars == IntPtr.Zero || (IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException((IntPtr) chars == IntPtr.Zero ? nameof (chars) : nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (byteCount < 0 || charCount < 0)
        throw new ArgumentOutOfRangeException(byteCount < 0 ? nameof (byteCount) : nameof (charCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      byte[] bytes1 = new byte[byteCount];
      for (int index = 0; index < byteCount; ++index)
        bytes1[index] = bytes[index];
      char[] chars1 = new char[charCount];
      int chars2 = this.GetChars(bytes1, 0, byteCount, chars1, 0);
      if (chars2 < charCount)
        charCount = chars2;
      for (int index = 0; index < charCount; ++index)
        chars[index] = chars1[index];
      return charCount;
    }

    [SecurityCritical]
    internal virtual unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
    {
      return this.GetChars(bytes, byteCount, chars, charCount);
    }

    /// <summary>
    ///   При переопределении в производном классе декодирует указанное количество байтов начиная с указанного адреса в строку.
    /// </summary>
    /// <param name="bytes">Указатель на массив байтов.</param>
    /// <param name="byteCount">Число байтов для декодирования.</param>
    /// <returns>
    ///   Строка, содержащая результаты декодирования заданной последовательности байтов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="bytes" /> является пустым указателем.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="byteCount" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошла резервной (см. Кодировки в .NET Framework Подробное описание)
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public unsafe string GetString(byte* bytes, int byteCount)
    {
      if ((IntPtr) bytes == IntPtr.Zero)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException(nameof (byteCount), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      return string.CreateStringFromEncoding(bytes, byteCount, this);
    }

    /// <summary>
    ///   При переопределении в производном классе получает идентификатор кодовой страницы текущего объекта <see cref="T:System.Text.Encoding" />.
    /// </summary>
    /// <returns>
    ///   Идентификатор кодовой страницы текущего объекта <see cref="T:System.Text.Encoding" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual int CodePage
    {
      [__DynamicallyInvokable] get
      {
        return this.m_codePage;
      }
    }

    /// <summary>
    ///   Получает значение, которое указывает, является ли текущая кодировка всегда нормализованной с использованием формы нормализации по умолчанию.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если текущий объект <see cref="T:System.Text.Encoding" /> всегда нормализуется; в противоположном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public bool IsAlwaysNormalized()
    {
      return this.IsAlwaysNormalized(NormalizationForm.FormC);
    }

    /// <summary>
    ///   При переопределении в производном классе получает значение, которое указывает, является ли текущая кодировка всегда нормализованной с использованием заданной по умолчанию формы нормализации.
    /// </summary>
    /// <param name="form">
    ///   Одно из значений <see cref="T:System.Text.NormalizationForm" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если текущий объект <see cref="T:System.Text.Encoding" /> всегда нормализуется с использованием заданного значения <see cref="T:System.Text.NormalizationForm" />; в противоположном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public virtual bool IsAlwaysNormalized(NormalizationForm form)
    {
      return false;
    }

    /// <summary>
    ///   При переопределении в производном классе получает декодер, который преобразует последовательность байтов в последовательность символов.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.Decoder" />, преобразующий закодированную последовательность байтов в последовательность символов.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Decoder GetDecoder()
    {
      return (Decoder) new Encoding.DefaultDecoder(this);
    }

    [SecurityCritical]
    private static Encoding CreateDefaultEncoding()
    {
      int acp = Win32Native.GetACP();
      Encoding encoding;
      switch (acp)
      {
        case 1252:
          encoding = (Encoding) new SBCSCodePageEncoding(acp);
          break;
        case 65001:
          encoding = (Encoding) Encoding.s_defaultUtf8EncodingNoBom;
          break;
        default:
          encoding = Encoding.GetEncoding(acp);
          break;
      }
      return encoding;
    }

    /// <summary>
    ///   Получает кодировку для текущей кодовой страницы ANSI операционной системы.
    /// </summary>
    /// <returns>
    ///   Кодировка для текущей кодовой страницы ANSI операционной системы.
    /// </returns>
    public static Encoding Default
    {
      [SecuritySafeCritical] get
      {
        if (Encoding.defaultEncoding == null)
          Encoding.defaultEncoding = Encoding.CreateDefaultEncoding();
        return Encoding.defaultEncoding;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает кодировщик, который преобразует последовательность символов Юникода в закодированную последовательность байтов.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.Encoder" />, преобразующий последовательность символов Юникода в закодированную последовательность байтов.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Encoder GetEncoder()
    {
      return (Encoder) new Encoding.DefaultEncoder(this);
    }

    /// <summary>
    ///   При переопределении в производном классе вычисляет максимальное количество байтов, полученных при кодировании заданного количества символов.
    /// </summary>
    /// <param name="charCount">Число кодируемых символов.</param>
    /// <returns>
    ///   Максимальное количество байтов, полученных при кодировании заданного количества символов.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="charCount" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.EncoderFallback" /> задается значение <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract int GetMaxByteCount(int charCount);

    /// <summary>
    ///   При переопределении в производном классе вычисляет максимальное количество символов, полученных при декодировании заданного количества байтов.
    /// </summary>
    /// <param name="byteCount">Число байтов для декодирования.</param>
    /// <returns>
    ///   Максимальное количество символов, полученных при декодировании заданного количества байтов.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="byteCount" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract int GetMaxCharCount(int byteCount);

    /// <summary>
    ///   При переопределении в производном классе декодирует все байты из указанного массива байтов в строку.
    /// </summary>
    /// <param name="bytes">
    ///   Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <returns>
    ///   Строка, содержащая результаты декодирования заданной последовательности байтов.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив байтов содержит недопустимые точки кода Юникод.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="bytes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Произошел переход на резервный ресурс (полное объяснение см. в Кодировки в .NET Framework).
    /// 
    ///   - и -
    /// 
    ///   Параметру <see cref="P:System.Text.Encoding.DecoderFallback" /> задается значение <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual string GetString(byte[] bytes)
    {
      if (bytes == null)
        throw new ArgumentNullException(nameof (bytes), Environment.GetResourceString("ArgumentNull_Array"));
      return this.GetString(bytes, 0, bytes.Length);
    }

    /// <summary>
    ///   При переопределении в производном классе декодирует последовательность байтов из указанного массива байтов в строку.
    /// </summary>
    /// <param name="bytes">
    ///   Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <param name="index">Индекс первого декодируемого байта.</param>
    /// <param name="count">Число байтов для декодирования.</param>
    /// <returns>
    ///   Строка, содержащая результаты декодирования заданной последовательности байтов.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив байтов содержит недопустимые точки кода Юникод.
    /// </exception>
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
    [__DynamicallyInvokable]
    public virtual string GetString(byte[] bytes, int index, int count)
    {
      return new string(this.GetChars(bytes, index, count));
    }

    /// <summary>
    ///   Получает кодировку для формата UTF-16 с прямым порядком байтов.
    /// </summary>
    /// <returns>
    ///   Кодировка для формата UTF-16 с прямым порядком байтов.
    /// </returns>
    [__DynamicallyInvokable]
    public static Encoding Unicode
    {
      [__DynamicallyInvokable] get
      {
        if (Encoding.unicodeEncoding == null)
          Encoding.unicodeEncoding = (Encoding) new UnicodeEncoding(false, true);
        return Encoding.unicodeEncoding;
      }
    }

    /// <summary>
    ///   Получает кодировку для формата UTF-16 с обратным порядком байтов.
    /// </summary>
    /// <returns>
    ///   Объект кодировки для формата UTF-16 с обратным порядком байтов.
    /// </returns>
    [__DynamicallyInvokable]
    public static Encoding BigEndianUnicode
    {
      [__DynamicallyInvokable] get
      {
        if (Encoding.bigEndianUnicode == null)
          Encoding.bigEndianUnicode = (Encoding) new UnicodeEncoding(true, true);
        return Encoding.bigEndianUnicode;
      }
    }

    /// <summary>Получает кодировку для формата UTF-7.</summary>
    /// <returns>Кодировка для формата UTF-7.</returns>
    [__DynamicallyInvokable]
    public static Encoding UTF7
    {
      [__DynamicallyInvokable] get
      {
        if (Encoding.utf7Encoding == null)
          Encoding.utf7Encoding = (Encoding) new UTF7Encoding();
        return Encoding.utf7Encoding;
      }
    }

    /// <summary>Получает кодировку для формата UTF-8.</summary>
    /// <returns>Кодировка для формата UTF-8.</returns>
    [__DynamicallyInvokable]
    public static Encoding UTF8
    {
      [__DynamicallyInvokable] get
      {
        if (Encoding.utf8Encoding == null)
          Encoding.utf8Encoding = (Encoding) new UTF8Encoding(true);
        return Encoding.utf8Encoding;
      }
    }

    /// <summary>
    ///   Получает кодировку для формата UTF-32 с прямым порядком байтов.
    /// </summary>
    /// <returns>
    ///   Объект кодировки для формата UTF-32 с прямым порядком байтов.
    /// </returns>
    [__DynamicallyInvokable]
    public static Encoding UTF32
    {
      [__DynamicallyInvokable] get
      {
        if (Encoding.utf32Encoding == null)
          Encoding.utf32Encoding = (Encoding) new UTF32Encoding(false, true);
        return Encoding.utf32Encoding;
      }
    }

    /// <summary>
    ///   Определяет, равен ли указанный объект <see cref="T:System.Object" /> текущему экземпляру.
    /// </summary>
    /// <param name="value">
    ///   <see cref="T:System.Object" /> для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="value" /> является экземпляром <see cref="T:System.Text.Encoding" />, равным текущему экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      Encoding encoding = value as Encoding;
      if (encoding != null && this.m_codePage == encoding.m_codePage && this.EncoderFallback.Equals((object) encoding.EncoderFallback))
        return this.DecoderFallback.Equals((object) encoding.DecoderFallback);
      return false;
    }

    /// <summary>Возвращает хэш-код текущего экземпляра.</summary>
    /// <returns>Хэш-код для текущего экземпляра.</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.m_codePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode();
    }

    internal virtual char[] GetBestFitUnicodeToBytesData()
    {
      return EmptyArray<char>.Value;
    }

    internal virtual char[] GetBestFitBytesToUnicodeData()
    {
      return EmptyArray<char>.Value;
    }

    internal void ThrowBytesOverflow()
    {
      throw new ArgumentException(Environment.GetResourceString("Argument_EncodingConversionOverflowBytes", (object) this.EncodingName, (object) this.EncoderFallback.GetType()), "bytes");
    }

    [SecurityCritical]
    internal void ThrowBytesOverflow(EncoderNLS encoder, bool nothingEncoded)
    {
      if (((encoder == null ? 1 : (encoder.m_throwOnOverflow ? 1 : 0)) | (nothingEncoded ? 1 : 0)) != 0)
      {
        if (encoder != null && encoder.InternalHasFallbackBuffer)
          encoder.FallbackBuffer.InternalReset();
        this.ThrowBytesOverflow();
      }
      encoder.ClearMustFlush();
    }

    internal void ThrowCharsOverflow()
    {
      throw new ArgumentException(Environment.GetResourceString("Argument_EncodingConversionOverflowChars", (object) this.EncodingName, (object) this.DecoderFallback.GetType()), "chars");
    }

    [SecurityCritical]
    internal void ThrowCharsOverflow(DecoderNLS decoder, bool nothingDecoded)
    {
      if (((decoder == null ? 1 : (decoder.m_throwOnOverflow ? 1 : 0)) | (nothingDecoded ? 1 : 0)) != 0)
      {
        if (decoder != null && decoder.InternalHasFallbackBuffer)
          decoder.FallbackBuffer.InternalReset();
        this.ThrowCharsOverflow();
      }
      decoder.ClearMustFlush();
    }

    [Serializable]
    internal class DefaultEncoder : Encoder, ISerializable, IObjectReference
    {
      private Encoding m_encoding;
      [NonSerialized]
      private bool m_hasInitializedEncoding;
      [NonSerialized]
      internal char charLeftOver;

      public DefaultEncoder(Encoding encoding)
      {
        this.m_encoding = encoding;
        this.m_hasInitializedEncoding = true;
      }

      internal DefaultEncoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        this.m_encoding = (Encoding) info.GetValue("encoding", typeof (Encoding));
        try
        {
          this.m_fallback = (EncoderFallback) info.GetValue("m_fallback", typeof (EncoderFallback));
          this.charLeftOver = (char) info.GetValue(nameof (charLeftOver), typeof (char));
        }
        catch (SerializationException ex)
        {
        }
      }

      [SecurityCritical]
      public object GetRealObject(StreamingContext context)
      {
        if (this.m_hasInitializedEncoding)
          return (object) this;
        Encoder encoder = this.m_encoding.GetEncoder();
        if (this.m_fallback != null)
          encoder.m_fallback = this.m_fallback;
        if (this.charLeftOver != char.MinValue)
        {
          EncoderNLS encoderNls = encoder as EncoderNLS;
          if (encoderNls != null)
            encoderNls.charLeftOver = this.charLeftOver;
        }
        return (object) encoder;
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        info.AddValue("encoding", (object) this.m_encoding);
      }

      public override int GetByteCount(char[] chars, int index, int count, bool flush)
      {
        return this.m_encoding.GetByteCount(chars, index, count);
      }

      [SecurityCritical]
      public override unsafe int GetByteCount(char* chars, int count, bool flush)
      {
        return this.m_encoding.GetByteCount(chars, count);
      }

      public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
      {
        return this.m_encoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
      }

      [SecurityCritical]
      public override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
      {
        return this.m_encoding.GetBytes(chars, charCount, bytes, byteCount);
      }
    }

    [Serializable]
    internal class DefaultDecoder : Decoder, ISerializable, IObjectReference
    {
      private Encoding m_encoding;
      [NonSerialized]
      private bool m_hasInitializedEncoding;

      public DefaultDecoder(Encoding encoding)
      {
        this.m_encoding = encoding;
        this.m_hasInitializedEncoding = true;
      }

      internal DefaultDecoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        this.m_encoding = (Encoding) info.GetValue("encoding", typeof (Encoding));
        try
        {
          this.m_fallback = (DecoderFallback) info.GetValue("m_fallback", typeof (DecoderFallback));
        }
        catch (SerializationException ex)
        {
          this.m_fallback = (DecoderFallback) null;
        }
      }

      [SecurityCritical]
      public object GetRealObject(StreamingContext context)
      {
        if (this.m_hasInitializedEncoding)
          return (object) this;
        Decoder decoder = this.m_encoding.GetDecoder();
        if (this.m_fallback != null)
          decoder.m_fallback = this.m_fallback;
        return (object) decoder;
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        info.AddValue("encoding", (object) this.m_encoding);
      }

      public override int GetCharCount(byte[] bytes, int index, int count)
      {
        return this.GetCharCount(bytes, index, count, false);
      }

      public override int GetCharCount(byte[] bytes, int index, int count, bool flush)
      {
        return this.m_encoding.GetCharCount(bytes, index, count);
      }

      [SecurityCritical]
      public override unsafe int GetCharCount(byte* bytes, int count, bool flush)
      {
        return this.m_encoding.GetCharCount(bytes, count);
      }

      public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
      {
        return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
      }

      public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
      {
        return this.m_encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
      }

      [SecurityCritical]
      public override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
      {
        return this.m_encoding.GetChars(bytes, byteCount, chars, charCount);
      }
    }

    internal class EncodingCharBuffer
    {
      [SecurityCritical]
      private unsafe char* chars;
      [SecurityCritical]
      private unsafe char* charStart;
      [SecurityCritical]
      private unsafe char* charEnd;
      private int charCountResult;
      private Encoding enc;
      private DecoderNLS decoder;
      [SecurityCritical]
      private unsafe byte* byteStart;
      [SecurityCritical]
      private unsafe byte* byteEnd;
      [SecurityCritical]
      private unsafe byte* bytes;
      private DecoderFallbackBuffer fallbackBuffer;

      [SecurityCritical]
      internal unsafe EncodingCharBuffer(Encoding enc, DecoderNLS decoder, char* charStart, int charCount, byte* byteStart, int byteCount)
      {
        this.enc = enc;
        this.decoder = decoder;
        this.chars = charStart;
        this.charStart = charStart;
        this.charEnd = charStart + charCount;
        this.byteStart = byteStart;
        this.bytes = byteStart;
        this.byteEnd = byteStart + byteCount;
        this.fallbackBuffer = this.decoder != null ? this.decoder.FallbackBuffer : enc.DecoderFallback.CreateFallbackBuffer();
        this.fallbackBuffer.InternalInitialize(this.bytes, this.charEnd);
      }

      [SecurityCritical]
      internal unsafe bool AddChar(char ch, int numBytes)
      {
        if ((IntPtr) this.chars != IntPtr.Zero)
        {
          if (this.chars >= this.charEnd)
          {
            this.bytes -= numBytes;
            this.enc.ThrowCharsOverflow(this.decoder, this.bytes <= this.byteStart);
            return false;
          }
          *this.chars++ = ch;
        }
        ++this.charCountResult;
        return true;
      }

      [SecurityCritical]
      internal bool AddChar(char ch)
      {
        return this.AddChar(ch, 1);
      }

      [SecurityCritical]
      internal unsafe bool AddChar(char ch1, char ch2, int numBytes)
      {
        if ((UIntPtr) this.chars >= (UIntPtr) this.charEnd - new UIntPtr(2))
        {
          this.bytes -= numBytes;
          this.enc.ThrowCharsOverflow(this.decoder, this.bytes <= this.byteStart);
          return false;
        }
        if (this.AddChar(ch1, numBytes))
          return this.AddChar(ch2, numBytes);
        return false;
      }

      [SecurityCritical]
      internal unsafe void AdjustBytes(int count)
      {
        this.bytes += count;
      }

      internal unsafe bool MoreData
      {
        [SecurityCritical] get
        {
          return this.bytes < this.byteEnd;
        }
      }

      [SecurityCritical]
      internal unsafe bool EvenMoreData(int count)
      {
        return this.bytes <= this.byteEnd - count;
      }

      [SecurityCritical]
      internal unsafe byte GetNextByte()
      {
        if (this.bytes >= this.byteEnd)
          return 0;
        return *this.bytes++;
      }

      internal unsafe int BytesUsed
      {
        [SecurityCritical] get
        {
          return (int) (this.bytes - this.byteStart);
        }
      }

      [SecurityCritical]
      internal bool Fallback(byte fallbackByte)
      {
        return this.Fallback(new byte[1]{ fallbackByte });
      }

      [SecurityCritical]
      internal bool Fallback(byte byte1, byte byte2)
      {
        return this.Fallback(new byte[2]{ byte1, byte2 });
      }

      [SecurityCritical]
      internal bool Fallback(byte byte1, byte byte2, byte byte3, byte byte4)
      {
        return this.Fallback(new byte[4]
        {
          byte1,
          byte2,
          byte3,
          byte4
        });
      }

      [SecurityCritical]
      internal unsafe bool Fallback(byte[] byteBuffer)
      {
        if ((IntPtr) this.chars != IntPtr.Zero)
        {
          char* chars = this.chars;
          if (!this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes, ref this.chars))
          {
            this.bytes -= byteBuffer.Length;
            this.fallbackBuffer.InternalReset();
            this.enc.ThrowCharsOverflow(this.decoder, this.chars == this.charStart);
            return false;
          }
          this.charCountResult += (int) (this.chars - chars);
        }
        else
          this.charCountResult += this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes);
        return true;
      }

      internal int Count
      {
        get
        {
          return this.charCountResult;
        }
      }
    }

    internal class EncodingByteBuffer
    {
      [SecurityCritical]
      private unsafe byte* bytes;
      [SecurityCritical]
      private unsafe byte* byteStart;
      [SecurityCritical]
      private unsafe byte* byteEnd;
      [SecurityCritical]
      private unsafe char* chars;
      [SecurityCritical]
      private unsafe char* charStart;
      [SecurityCritical]
      private unsafe char* charEnd;
      private int byteCountResult;
      private Encoding enc;
      private EncoderNLS encoder;
      internal EncoderFallbackBuffer fallbackBuffer;

      [SecurityCritical]
      internal unsafe EncodingByteBuffer(Encoding inEncoding, EncoderNLS inEncoder, byte* inByteStart, int inByteCount, char* inCharStart, int inCharCount)
      {
        this.enc = inEncoding;
        this.encoder = inEncoder;
        this.charStart = inCharStart;
        this.chars = inCharStart;
        this.charEnd = inCharStart + inCharCount;
        this.bytes = inByteStart;
        this.byteStart = inByteStart;
        this.byteEnd = inByteStart + inByteCount;
        if (this.encoder == null)
        {
          this.fallbackBuffer = this.enc.EncoderFallback.CreateFallbackBuffer();
        }
        else
        {
          this.fallbackBuffer = this.encoder.FallbackBuffer;
          if (this.encoder.m_throwOnOverflow && this.encoder.InternalHasFallbackBuffer && this.fallbackBuffer.Remaining > 0)
            throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", (object) this.encoder.Encoding.EncodingName, (object) this.encoder.Fallback.GetType()));
        }
        this.fallbackBuffer.InternalInitialize(this.chars, this.charEnd, this.encoder, (IntPtr) this.bytes != IntPtr.Zero);
      }

      [SecurityCritical]
      internal unsafe bool AddByte(byte b, int moreBytesExpected)
      {
        if ((IntPtr) this.bytes != IntPtr.Zero)
        {
          if (this.bytes >= this.byteEnd - moreBytesExpected)
          {
            this.MovePrevious(true);
            return false;
          }
          *this.bytes++ = b;
        }
        ++this.byteCountResult;
        return true;
      }

      [SecurityCritical]
      internal bool AddByte(byte b1)
      {
        return this.AddByte(b1, 0);
      }

      [SecurityCritical]
      internal bool AddByte(byte b1, byte b2)
      {
        return this.AddByte(b1, b2, 0);
      }

      [SecurityCritical]
      internal bool AddByte(byte b1, byte b2, int moreBytesExpected)
      {
        if (this.AddByte(b1, 1 + moreBytesExpected))
          return this.AddByte(b2, moreBytesExpected);
        return false;
      }

      [SecurityCritical]
      internal bool AddByte(byte b1, byte b2, byte b3)
      {
        return this.AddByte(b1, b2, b3, 0);
      }

      [SecurityCritical]
      internal bool AddByte(byte b1, byte b2, byte b3, int moreBytesExpected)
      {
        if (this.AddByte(b1, 2 + moreBytesExpected) && this.AddByte(b2, 1 + moreBytesExpected))
          return this.AddByte(b3, moreBytesExpected);
        return false;
      }

      [SecurityCritical]
      internal bool AddByte(byte b1, byte b2, byte b3, byte b4)
      {
        if (this.AddByte(b1, 3) && this.AddByte(b2, 2) && this.AddByte(b3, 1))
          return this.AddByte(b4, 0);
        return false;
      }

      [SecurityCritical]
      internal unsafe void MovePrevious(bool bThrow)
      {
        if (this.fallbackBuffer.bFallingBack)
          this.fallbackBuffer.MovePrevious();
        else if (this.chars > this.charStart)
          this.chars -= 2;
        if (!bThrow)
          return;
        this.enc.ThrowBytesOverflow(this.encoder, this.bytes == this.byteStart);
      }

      [SecurityCritical]
      internal unsafe bool Fallback(char charFallback)
      {
        return this.fallbackBuffer.InternalFallback(charFallback, ref this.chars);
      }

      internal unsafe bool MoreData
      {
        [SecurityCritical] get
        {
          if (this.fallbackBuffer.Remaining <= 0)
            return this.chars < this.charEnd;
          return true;
        }
      }

      [SecurityCritical]
      internal unsafe char GetNextChar()
      {
        char ch = this.fallbackBuffer.InternalGetNextChar();
        if (ch == char.MinValue && this.chars < this.charEnd)
          ch = *this.chars++;
        return ch;
      }

      internal unsafe int CharsUsed
      {
        [SecurityCritical] get
        {
          return (int) (this.chars - this.charStart);
        }
      }

      internal int Count
      {
        get
        {
          return this.byteCountResult;
        }
      }
    }
  }
}
