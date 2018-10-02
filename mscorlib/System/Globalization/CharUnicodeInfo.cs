// Decompiled with JetBrains decompiler
// Type: System.Globalization.CharUnicodeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Globalization
{
  /// <summary>
  ///   Получает сведения о символе Юникода.
  ///    Этот класс не наследуется.
  /// </summary>
  [__DynamicallyInvokable]
  public static class CharUnicodeInfo
  {
    private static bool s_initialized = CharUnicodeInfo.InitTable();
    internal const char HIGH_SURROGATE_START = '\xD800';
    internal const char HIGH_SURROGATE_END = '\xDBFF';
    internal const char LOW_SURROGATE_START = '\xDC00';
    internal const char LOW_SURROGATE_END = '\xDFFF';
    internal const int UNICODE_CATEGORY_OFFSET = 0;
    internal const int BIDI_CATEGORY_OFFSET = 1;
    [SecurityCritical]
    private static unsafe ushort* s_pCategoryLevel1Index;
    [SecurityCritical]
    private static unsafe byte* s_pCategoriesValue;
    [SecurityCritical]
    private static unsafe ushort* s_pNumericLevel1Index;
    [SecurityCritical]
    private static unsafe byte* s_pNumericValues;
    [SecurityCritical]
    private static unsafe CharUnicodeInfo.DigitValues* s_pDigitValues;
    internal const string UNICODE_INFO_FILE_NAME = "charinfo.nlp";
    internal const int UNICODE_PLANE01_START = 65536;

    [SecuritySafeCritical]
    private static unsafe bool InitTable()
    {
      byte* globalizationResourceBytePtr = GlobalizationAssembly.GetGlobalizationResourceBytePtr(typeof (CharUnicodeInfo).Assembly, "charinfo.nlp");
      CharUnicodeInfo.UnicodeDataHeader* unicodeDataHeaderPtr = (CharUnicodeInfo.UnicodeDataHeader*) globalizationResourceBytePtr;
      CharUnicodeInfo.s_pCategoryLevel1Index = (ushort*) (globalizationResourceBytePtr + unicodeDataHeaderPtr->OffsetToCategoriesIndex);
      CharUnicodeInfo.s_pCategoriesValue = globalizationResourceBytePtr + unicodeDataHeaderPtr->OffsetToCategoriesValue;
      CharUnicodeInfo.s_pNumericLevel1Index = (ushort*) (globalizationResourceBytePtr + unicodeDataHeaderPtr->OffsetToNumbericIndex);
      CharUnicodeInfo.s_pNumericValues = globalizationResourceBytePtr + unicodeDataHeaderPtr->OffsetToNumbericValue;
      CharUnicodeInfo.s_pDigitValues = (CharUnicodeInfo.DigitValues*) (globalizationResourceBytePtr + unicodeDataHeaderPtr->OffsetToDigitValue);
      return true;
    }

    internal static int InternalConvertToUtf32(string s, int index)
    {
      if (index < s.Length - 1)
      {
        int num1 = (int) s[index] - 55296;
        if (num1 >= 0 && num1 <= 1023)
        {
          int num2 = (int) s[index + 1] - 56320;
          if (num2 >= 0 && num2 <= 1023)
            return num1 * 1024 + num2 + 65536;
        }
      }
      return (int) s[index];
    }

    internal static int InternalConvertToUtf32(string s, int index, out int charLength)
    {
      charLength = 1;
      if (index < s.Length - 1)
      {
        int num1 = (int) s[index] - 55296;
        if (num1 >= 0 && num1 <= 1023)
        {
          int num2 = (int) s[index + 1] - 56320;
          if (num2 >= 0 && num2 <= 1023)
          {
            ++charLength;
            return num1 * 1024 + num2 + 65536;
          }
        }
      }
      return (int) s[index];
    }

    internal static bool IsWhiteSpace(string s, int index)
    {
      switch (CharUnicodeInfo.GetUnicodeCategory(s, index))
      {
        case UnicodeCategory.SpaceSeparator:
        case UnicodeCategory.LineSeparator:
        case UnicodeCategory.ParagraphSeparator:
          return true;
        default:
          return false;
      }
    }

    internal static bool IsWhiteSpace(char c)
    {
      switch (CharUnicodeInfo.GetUnicodeCategory(c))
      {
        case UnicodeCategory.SpaceSeparator:
        case UnicodeCategory.LineSeparator:
        case UnicodeCategory.ParagraphSeparator:
          return true;
        default:
          return false;
      }
    }

    [SecuritySafeCritical]
    internal static unsafe double InternalGetNumericValue(int ch)
    {
      ushort num1 = CharUnicodeInfo.s_pNumericLevel1Index[ch >> 8];
      ushort num2 = CharUnicodeInfo.s_pNumericLevel1Index[(int) num1 + (ch >> 4 & 15)];
      byte* numPtr = (byte*) (CharUnicodeInfo.s_pNumericLevel1Index + num2);
      return *(double*) (CharUnicodeInfo.s_pNumericValues + ((IntPtr) numPtr[ch & 15] * 8).ToInt64());
    }

    [SecuritySafeCritical]
    internal static unsafe CharUnicodeInfo.DigitValues* InternalGetDigitValues(int ch)
    {
      ushort num1 = CharUnicodeInfo.s_pNumericLevel1Index[ch >> 8];
      ushort num2 = CharUnicodeInfo.s_pNumericLevel1Index[(int) num1 + (ch >> 4 & 15)];
      byte* numPtr = (byte*) (CharUnicodeInfo.s_pNumericLevel1Index + num2);
      return CharUnicodeInfo.s_pDigitValues + numPtr[ch & 15];
    }

    [SecuritySafeCritical]
    internal static unsafe sbyte InternalGetDecimalDigitValue(int ch)
    {
      return CharUnicodeInfo.InternalGetDigitValues(ch)->decimalDigit;
    }

    [SecuritySafeCritical]
    internal static unsafe sbyte InternalGetDigitValue(int ch)
    {
      return CharUnicodeInfo.InternalGetDigitValues(ch)->digit;
    }

    /// <summary>
    ///   Возвращает числовое значение, связанное с указанным символом.
    /// </summary>
    /// <param name="ch">
    ///   Знак Юникода, для которого следует получить числовое значение.
    /// </param>
    /// <returns>
    ///   Числовое значение, связанное с указанным символом.
    /// 
    ///   -или-
    /// 
    ///   -1, если указанный символ не является числовым символом.
    /// </returns>
    [__DynamicallyInvokable]
    public static double GetNumericValue(char ch)
    {
      return CharUnicodeInfo.InternalGetNumericValue((int) ch);
    }

    /// <summary>
    ///   Возвращает числовое значение, связанное с символом по указанному индексу в указанной строке.
    /// </summary>
    /// <param name="s">
    ///   <see cref="T:System.String" /> Содержащий символ Юникода, для которого следует получить числовое значение.
    /// </param>
    /// <param name="index">
    ///   Индекс символа Юникода, для которого следует получить числовое значение.
    /// </param>
    /// <returns>
    ///   Числовое значение, связанное с символом по указанному индексу в указанной строке.
    /// 
    ///   -или-
    /// 
    ///   -1, если символ по указанному индексу в указанной строке не является числовым символом.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static double GetNumericValue(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return CharUnicodeInfo.InternalGetNumericValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
    }

    /// <summary>
    ///   Возвращает десятичное цифровое значение указанного числового символа.
    /// </summary>
    /// <param name="ch">
    ///   Знак Юникода, для которого следует получить десятичное цифровое значение.
    /// </param>
    /// <returns>
    ///   Десятичное цифровое значение указанного числового символа.
    /// 
    ///   -или-
    /// 
    ///   -1, если указанный символ не является десятичной цифрой.
    /// </returns>
    public static int GetDecimalDigitValue(char ch)
    {
      return (int) CharUnicodeInfo.InternalGetDecimalDigitValue((int) ch);
    }

    /// <summary>
    ///   Возвращает десятичное цифровое значение числового символа по указанному индексу в указанной строке.
    /// </summary>
    /// <param name="s">
    ///   <see cref="T:System.String" /> Содержащий символ Юникода, для которого следует получить десятичное цифровое значение.
    /// </param>
    /// <param name="index">
    ///   Индекс символа Юникода, для которого следует получить десятичное цифровое значение.
    /// </param>
    /// <returns>
    ///   Десятичное цифровое значение числового символа по указанному индексу в указанной строке.
    /// 
    ///   -или-
    /// 
    ///   -1, если символ по указанному индексу в указанной строке не является десятичной цифрой.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов в <paramref name="s" />.
    /// </exception>
    public static int GetDecimalDigitValue(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return (int) CharUnicodeInfo.InternalGetDecimalDigitValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
    }

    /// <summary>
    ///   Возвращает цифровое значение указанного числового символа.
    /// </summary>
    /// <param name="ch">
    ///   Знак Юникода, для которого следует получить цифровое значение.
    /// </param>
    /// <returns>
    ///   Цифровое значение указанного числового символа.
    /// 
    ///   -или-
    /// 
    ///   -1, если указанный символ не является цифрой.
    /// </returns>
    public static int GetDigitValue(char ch)
    {
      return (int) CharUnicodeInfo.InternalGetDigitValue((int) ch);
    }

    /// <summary>
    ///   Получает цифровое значение числового символа по указанному индексу в указанной строке.
    /// </summary>
    /// <param name="s">
    ///   <see cref="T:System.String" /> Содержащий символ Юникода, для которого следует получить цифровое значение.
    /// </param>
    /// <param name="index">
    ///   Индекс символа Юникода, для которого следует получить цифровое значение.
    /// </param>
    /// <returns>
    ///   Цифровое значение числового символа по указанному индексу в указанной строке.
    /// 
    ///   -или-
    /// 
    ///   -1, если символ по указанному индексу в указанной строке не является цифрой.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов в <paramref name="s" />.
    /// </exception>
    public static int GetDigitValue(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return (int) CharUnicodeInfo.InternalGetDigitValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
    }

    /// <summary>Получает категорию Юникода для указанного знака.</summary>
    /// <param name="ch">
    ///   Знак Юникода, для которого следует получить категорию Юникода.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.Globalization.UnicodeCategory" /> значение, указывающее, категория заданного символа.
    /// </returns>
    [__DynamicallyInvokable]
    public static UnicodeCategory GetUnicodeCategory(char ch)
    {
      return CharUnicodeInfo.InternalGetUnicodeCategory((int) ch);
    }

    /// <summary>
    ///   Получает категорию Юникода для символа по указанному индексу в указанной строке.
    /// </summary>
    /// <param name="s">
    ///   <see cref="T:System.String" /> Содержащий символ Юникода, для которого следует получить категорию Юникода.
    /// </param>
    /// <param name="index">
    ///   Индекс символа Юникода, для которого следует получить категорию Юникода.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.Globalization.UnicodeCategory" /> значение, указывающее, диапазон символов из указанной строки по указанному индексу.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static UnicodeCategory GetUnicodeCategory(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
    }

    internal static UnicodeCategory InternalGetUnicodeCategory(int ch)
    {
      return (UnicodeCategory) CharUnicodeInfo.InternalGetCategoryValue(ch, 0);
    }

    [SecuritySafeCritical]
    internal static unsafe byte InternalGetCategoryValue(int ch, int offset)
    {
      ushort num1 = CharUnicodeInfo.s_pCategoryLevel1Index[ch >> 8];
      ushort num2 = CharUnicodeInfo.s_pCategoryLevel1Index[(int) num1 + (ch >> 4 & 15)];
      byte num3 = *(byte*) ((IntPtr) (CharUnicodeInfo.s_pCategoryLevel1Index + num2) + (ch & 15));
      return CharUnicodeInfo.s_pCategoriesValue[(int) num3 * 2 + offset];
    }

    internal static BidiCategory GetBidiCategory(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      return (BidiCategory) CharUnicodeInfo.InternalGetCategoryValue(CharUnicodeInfo.InternalConvertToUtf32(s, index), 1);
    }

    internal static UnicodeCategory InternalGetUnicodeCategory(string value, int index)
    {
      return CharUnicodeInfo.InternalGetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(value, index));
    }

    internal static UnicodeCategory InternalGetUnicodeCategory(string str, int index, out int charLength)
    {
      return CharUnicodeInfo.InternalGetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(str, index, out charLength));
    }

    internal static bool IsCombiningCategory(UnicodeCategory uc)
    {
      if (uc != UnicodeCategory.NonSpacingMark && uc != UnicodeCategory.SpacingCombiningMark)
        return uc == UnicodeCategory.EnclosingMark;
      return true;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct UnicodeDataHeader
    {
      [FieldOffset(0)]
      internal char TableName;
      [FieldOffset(32)]
      internal ushort version;
      [FieldOffset(40)]
      internal uint OffsetToCategoriesIndex;
      [FieldOffset(44)]
      internal uint OffsetToCategoriesValue;
      [FieldOffset(48)]
      internal uint OffsetToNumbericIndex;
      [FieldOffset(52)]
      internal uint OffsetToDigitValue;
      [FieldOffset(56)]
      internal uint OffsetToNumbericValue;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct DigitValues
    {
      internal sbyte decimalDigit;
      internal sbyte digit;
    }
  }
}
