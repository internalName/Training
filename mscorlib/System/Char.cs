// Decompiled with JetBrains decompiler
// Type: System.Char
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System
{
  /// <summary>Представляет символ как элемент кода UTF-16.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Char : IComparable, IConvertible, IComparable<char>, IEquatable<char>
  {
    private static readonly byte[] categoryForLatin1 = new byte[256]
    {
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 11,
      (byte) 24,
      (byte) 24,
      (byte) 24,
      (byte) 26,
      (byte) 24,
      (byte) 24,
      (byte) 24,
      (byte) 20,
      (byte) 21,
      (byte) 24,
      (byte) 25,
      (byte) 24,
      (byte) 19,
      (byte) 24,
      (byte) 24,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 8,
      (byte) 24,
      (byte) 24,
      (byte) 25,
      (byte) 25,
      (byte) 25,
      (byte) 24,
      (byte) 24,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 20,
      (byte) 24,
      (byte) 21,
      (byte) 27,
      (byte) 18,
      (byte) 27,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 20,
      (byte) 25,
      (byte) 21,
      (byte) 25,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 14,
      (byte) 11,
      (byte) 24,
      (byte) 26,
      (byte) 26,
      (byte) 26,
      (byte) 26,
      (byte) 28,
      (byte) 28,
      (byte) 27,
      (byte) 28,
      (byte) 1,
      (byte) 22,
      (byte) 25,
      (byte) 19,
      (byte) 28,
      (byte) 27,
      (byte) 28,
      (byte) 25,
      (byte) 10,
      (byte) 10,
      (byte) 27,
      (byte) 1,
      (byte) 28,
      (byte) 24,
      (byte) 27,
      (byte) 10,
      (byte) 1,
      (byte) 23,
      (byte) 10,
      (byte) 10,
      (byte) 10,
      (byte) 24,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 25,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 25,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 1
    };
    internal char m_value;
    /// <summary>
    ///   Представляет наибольшее возможное значение типа <see cref="T:System.Char" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const char MaxValue = '\xFFFF';
    /// <summary>
    ///   Представляет минимально допустимое значение типа <see cref="T:System.Char" />.
    ///    Это поле является константой.
    /// </summary>
    [__DynamicallyInvokable]
    public const char MinValue = '\0';
    internal const int UNICODE_PLANE00_END = 65535;
    internal const int UNICODE_PLANE01_START = 65536;
    internal const int UNICODE_PLANE16_END = 1114111;
    internal const int HIGH_SURROGATE_START = 55296;
    internal const int LOW_SURROGATE_END = 57343;

    private static bool IsLatin1(char ch)
    {
      return ch <= 'ÿ';
    }

    private static bool IsAscii(char ch)
    {
      return ch <= '\x007F';
    }

    private static UnicodeCategory GetLatin1UnicodeCategory(char ch)
    {
      return (UnicodeCategory) char.categoryForLatin1[(int) ch];
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return (int) this | (int) this << 16;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим экземпляром или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> является экземпляром типа <see cref="T:System.Char" /> и равен значению данного экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is char))
        return false;
      return (int) this == (int) (char) obj;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли данный экземпляр указанному объекту <see cref="T:System.Char" />.
    /// </summary>
    /// <param name="obj">Объект, сравниваемый с этим экземпляром.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="obj" /> равен значению этого экземпляра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public bool Equals(char obj)
    {
      return (int) this == (int) obj;
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с заданным объектом и показывает, расположен ли данный экземпляр перед, после или на той же позиции в порядке сортировки, что и заданный объект <see cref="T:System.Object" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, сравниваемый с этим экземпляром, или <see langword="null" />.
    /// </param>
    /// <returns>
    /// Число со знаком, которое показывает позицию данного экземпляра в порядке сортировки по отношению к параметру <paramref name="value" />.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Описание
    /// 
    ///         Меньше нуля
    /// 
    ///         Данный экземпляр предшествует параметру <paramref name="value" />.
    /// 
    ///         Нуль
    /// 
    ///         Данный экземпляр имеет ту же позицию в порядке сортировки, что и <paramref name="value" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Данный экземпляр стоит после параметра <paramref name="value" />.
    /// 
    ///         -или-
    /// 
    ///         Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> не является объектом <see cref="T:System.Char" />.
    /// </exception>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is char))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeChar"));
      return (int) this - (int) (char) value;
    }

    /// <summary>
    ///   Сравнивает данный экземпляр с заданным объектом <see cref="T:System.Char" /> и показывает, расположен ли данный экземпляр перед, после или на той же позиции в порядке сортировки, что и заданный объект <see cref="T:System.Char" />.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Char" /> для сравнения.
    /// </param>
    /// <returns>
    /// Число со знаком, которое показывает позицию данного экземпляра в порядке сортировки по отношению к параметру <paramref name="value" />.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Описание
    /// 
    ///         Меньше нуля
    /// 
    ///         Данный экземпляр предшествует параметру <paramref name="value" />.
    /// 
    ///         Нуль
    /// 
    ///         Данный экземпляр имеет ту же позицию в порядке сортировки, что и <paramref name="value" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Данный экземпляр стоит после параметра <paramref name="value" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public int CompareTo(char value)
    {
      return (int) this - (int) value;
    }

    /// <summary>
    ///   Преобразует значение этого экземпляра в эквивалентное ему строковое представление.
    /// </summary>
    /// <returns>Строковое представление значения этого экземпляра.</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return char.ToString(this);
    }

    /// <summary>
    ///   Преобразует значение этого экземпляра в эквивалентное ему строковое представление с использованием указанных сведений об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="provider">
    ///   (Зарезервирован.) Объект, предоставляющий сведения о форматировании, связанные с определенным языком и региональными параметрами.
    /// </param>
    /// <returns>
    ///   Строковое представление значения данного экземпляра, определяемое параметром <paramref name="provider" />.
    /// </returns>
    public string ToString(IFormatProvider provider)
    {
      return char.ToString(this);
    }

    /// <summary>
    ///   Преобразует указанный символ Юникода в эквивалентное ему строковое представление.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Строковое представление значения <paramref name="c" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static string ToString(char c)
    {
      return new string(c, 1);
    }

    /// <summary>
    ///   Преобразует значение указанной строки в эквивалентный символ Юникода.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая один символ, или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Символ Юникода, эквивалентный единственному символу в <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Длина <paramref name="s" /> не равна 1.
    /// </exception>
    [__DynamicallyInvokable]
    public static char Parse(string s)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if (s.Length != 1)
        throw new FormatException(Environment.GetResourceString("Format_NeedSingleChar"));
      return s[0];
    }

    /// <summary>
    ///   Преобразует значение указанной строки в эквивалентный символ Юникода.
    ///    Возвращает код, позволяющий определить, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая один символ, или <see langword="null" />.
    /// </param>
    /// <param name="result">
    ///   По возвращении из этого метода содержит символ Юникода, эквивалентный единственному символу в <paramref name="s" />, если преобразование выполнено успешно, или значение без изменений, если преобразование завершилось неудачей.
    ///    Преобразование не удается выполнить, если параметр <paramref name="s" /> имеет значение <see langword="null" /> или длина параметра <paramref name="s" /> не равна 1.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out char result)
    {
      result = char.MinValue;
      if (s == null || s.Length != 1)
        return false;
      result = s[0];
      return true;
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ Юникода к категории десятичных цифр.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="c" /> является десятичной цифрой; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsDigit(char c)
    {
      if (!char.IsLatin1(c))
        return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber;
      if (c >= '0')
        return c <= '9';
      return false;
    }

    internal static bool CheckLetter(UnicodeCategory uc)
    {
      switch (uc)
      {
        case UnicodeCategory.UppercaseLetter:
        case UnicodeCategory.LowercaseLetter:
        case UnicodeCategory.TitlecaseLetter:
        case UnicodeCategory.ModifierLetter:
        case UnicodeCategory.OtherLetter:
          return true;
        default:
          return false;
      }
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ Юникода к категории букв Юникода.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="c" /> является буквой; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsLetter(char c)
    {
      if (!char.IsLatin1(c))
        return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(c));
      if (!char.IsAscii(c))
        return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
      c |= ' ';
      if (c >= 'a')
        return c <= 'z';
      return false;
    }

    private static bool IsWhiteSpaceLatin1(char c)
    {
      return c == ' ' || c >= '\t' && c <= '\r' || (c == ' ' || c == '\x0085');
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ Юникода к категории пробелов.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="c" /> является пробелом; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsWhiteSpace(char c)
    {
      if (char.IsLatin1(c))
        return char.IsWhiteSpaceLatin1(c);
      return CharUnicodeInfo.IsWhiteSpace(c);
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ Юникода к категории букв верхнего регистра.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="c" /> является буквой верхнего регистра; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsUpper(char c)
    {
      if (!char.IsLatin1(c))
        return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
      if (!char.IsAscii(c))
        return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
      if (c >= 'A')
        return c <= 'Z';
      return false;
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ Юникода к категории букв нижнего регистра.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="c" /> является буквой в нижнем регистре; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsLower(char c)
    {
      if (!char.IsLatin1(c))
        return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
      if (!char.IsAscii(c))
        return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
      if (c >= 'a')
        return c <= 'z';
      return false;
    }

    internal static bool CheckPunctuation(UnicodeCategory uc)
    {
      switch (uc)
      {
        case UnicodeCategory.ConnectorPunctuation:
        case UnicodeCategory.DashPunctuation:
        case UnicodeCategory.OpenPunctuation:
        case UnicodeCategory.ClosePunctuation:
        case UnicodeCategory.InitialQuotePunctuation:
        case UnicodeCategory.FinalQuotePunctuation:
        case UnicodeCategory.OtherPunctuation:
          return true;
        default:
          return false;
      }
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ Юникода к категории знаков препинания.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="c" /> является знаком препинания; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsPunctuation(char c)
    {
      if (char.IsLatin1(c))
        return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
      return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(c));
    }

    internal static bool CheckLetterOrDigit(UnicodeCategory uc)
    {
      switch (uc)
      {
        case UnicodeCategory.UppercaseLetter:
        case UnicodeCategory.LowercaseLetter:
        case UnicodeCategory.TitlecaseLetter:
        case UnicodeCategory.ModifierLetter:
        case UnicodeCategory.OtherLetter:
        case UnicodeCategory.DecimalDigitNumber:
          return true;
        default:
          return false;
      }
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ Юникода к категории букв или десятичных цифр.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="c" /> является буквой или десятичной цифрой; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsLetterOrDigit(char c)
    {
      if (char.IsLatin1(c))
        return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
      return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(c));
    }

    /// <summary>
    ///   Преобразует значение указанного символа Юникода в его эквивалент в верхнем регистре, используя указанные сведения о форматировании, связанные с языком и региональными параметрами.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <param name="culture">
    ///   Объект, задающий правила определения регистра для языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Эквивалент <paramref name="c" /> в верхнем регистре, измененный согласно <paramref name="culture" />, или значение <paramref name="c" /> без изменений, если <paramref name="c" /> уже является буквой верхнего регистра, не имеет эквивалента в верхнем регистре или не является буквой алфавита.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static char ToUpper(char c, CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      return culture.TextInfo.ToUpper(c);
    }

    /// <summary>
    ///   Преобразует значение символа Юникода в эквивалентный символ верхнего регистра.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Эквивалент параметра <paramref name="c" /> в верхнем регистре или значение <paramref name="c" /> без изменений, если <paramref name="c" /> уже является буквой верхнего регистра, не имеет эквивалента в верхнем регистре или не является буквой алфавита.
    /// </returns>
    [__DynamicallyInvokable]
    public static char ToUpper(char c)
    {
      return char.ToUpper(c, CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение символа Юникода в его эквивалент в верхнем регистре, используя правила изменения регистра, связанные с инвариантным языком и региональными параметрами.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Эквивалент параметра <paramref name="c" /> в верхнем регистре или значение <paramref name="c" /> без изменений, если <paramref name="c" /> уже является буквой верхнего регистра или не является буквой алфавита.
    /// </returns>
    [__DynamicallyInvokable]
    public static char ToUpperInvariant(char c)
    {
      return char.ToUpper(c, CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Преобразует значение указанного символа Юникода в его эквивалент в нижнем регистре, используя указанные сведения о форматировании, связанные с языком и региональными параметрами.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <param name="culture">
    ///   Объект, задающий правила определения регистра для языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Эквивалент <paramref name="c" /> в нижнем регистре, измененный согласно <paramref name="culture" />, или значение <paramref name="c" /> без изменений, если <paramref name="c" /> уже является буквой нижнего регистра или не является буквой алфавита.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static char ToLower(char c, CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      return culture.TextInfo.ToLower(c);
    }

    /// <summary>
    ///   Преобразует значение символа Юникода в его эквивалент в нижнем регистре.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Эквивалент <paramref name="c" /> в нижнем регистре или значение <paramref name="c" /> без изменений, если <paramref name="c" /> уже является буквой нижнего регистра или не является буквой алфавита.
    /// </returns>
    [__DynamicallyInvokable]
    public static char ToLower(char c)
    {
      return char.ToLower(c, CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Преобразует значение символа Юникода в его эквивалент в нижнем регистре, используя правила изменения регистра, связанные с инвариантным языком и региональными параметрами.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Эквивалент параметра <paramref name="c" /> в нижнем регистре или значение <paramref name="c" /> без изменений, если <paramref name="c" /> уже является буквой нижнего регистра или не является буквой алфавита.
    /// </returns>
    [__DynamicallyInvokable]
    public static char ToLowerInvariant(char c)
    {
      return char.ToLower(c, CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.TypeCode" /> для типа значения <see cref="T:System.Char" />.
    /// </summary>
    /// <returns>
    ///   Константа перечислимого типа, <see cref="F:System.TypeCode.Char" />.
    /// </returns>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Char;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Char), (object) "Boolean"));
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      return this;
    }

    [__DynamicallyInvokable]
    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      return Convert.ToSByte(this);
    }

    [__DynamicallyInvokable]
    byte IConvertible.ToByte(IFormatProvider provider)
    {
      return Convert.ToByte(this);
    }

    [__DynamicallyInvokable]
    short IConvertible.ToInt16(IFormatProvider provider)
    {
      return Convert.ToInt16(this);
    }

    [__DynamicallyInvokable]
    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      return Convert.ToUInt16(this);
    }

    [__DynamicallyInvokable]
    int IConvertible.ToInt32(IFormatProvider provider)
    {
      return Convert.ToInt32(this);
    }

    [__DynamicallyInvokable]
    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      return Convert.ToUInt32(this);
    }

    [__DynamicallyInvokable]
    long IConvertible.ToInt64(IFormatProvider provider)
    {
      return Convert.ToInt64(this);
    }

    [__DynamicallyInvokable]
    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      return Convert.ToUInt64(this);
    }

    [__DynamicallyInvokable]
    float IConvertible.ToSingle(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Char), (object) "Single"));
    }

    [__DynamicallyInvokable]
    double IConvertible.ToDouble(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Char), (object) "Double"));
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Char), (object) "Decimal"));
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) nameof (Char), (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ Юникода к категории управляющих символов.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="c" /> является управляющим символом; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsControl(char c)
    {
      if (char.IsLatin1(c))
        return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
      return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Control;
    }

    /// <summary>
    ///   Показывает, относится ли символ в указанной позиции в указанной строке к категории управляющих символов.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если символ позиции <paramref name="index" /> в <paramref name="s" /> является управляющим; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsControl(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      char ch = s[index];
      if (char.IsLatin1(ch))
        return char.GetLatin1UnicodeCategory(ch) == UnicodeCategory.Control;
      return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.Control;
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ Юникода в указанной позиции в указанной строке к категории десятичных цифр.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если символ в позиции <paramref name="index" /> в <paramref name="s" /> является десятичной цифрой; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsDigit(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      char ch = s[index];
      if (!char.IsLatin1(ch))
        return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.DecimalDigitNumber;
      if (ch >= '0')
        return ch <= '9';
      return false;
    }

    /// <summary>
    ///   Показывает, относится ли символ в указанной позиции в указанной строке к категории букв Юникода.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если знак в позиции <paramref name="index" /> в <paramref name="s" /> является буквой; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsLetter(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      char ch1 = s[index];
      if (!char.IsLatin1(ch1))
        return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(s, index));
      if (!char.IsAscii(ch1))
        return char.CheckLetter(char.GetLatin1UnicodeCategory(ch1));
      char ch2 = (char) ((uint) ch1 | 32U);
      if (ch2 >= 'a')
        return ch2 <= 'z';
      return false;
    }

    /// <summary>
    ///   Показывает, относится ли символ в указанной позиции в указанной строке к категории букв или десятичных цифр.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если знак в позиции <paramref name="index" /> в <paramref name="s" /> является буквой или десятичной цифрой; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsLetterOrDigit(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      char ch = s[index];
      if (char.IsLatin1(ch))
        return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(ch));
      return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(s, index));
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ в указанной позиции в указанной строке к категории букв нижнего регистра.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если символ в позиции <paramref name="index" /> в <paramref name="s" /> является буквой в нижнем регистре; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsLower(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      char ch = s[index];
      if (!char.IsLatin1(ch))
        return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.LowercaseLetter;
      if (!char.IsAscii(ch))
        return char.GetLatin1UnicodeCategory(ch) == UnicodeCategory.LowercaseLetter;
      if (ch >= 'a')
        return ch <= 'z';
      return false;
    }

    internal static bool CheckNumber(UnicodeCategory uc)
    {
      switch (uc)
      {
        case UnicodeCategory.DecimalDigitNumber:
        case UnicodeCategory.LetterNumber:
        case UnicodeCategory.OtherNumber:
          return true;
        default:
          return false;
      }
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ Юникода к категории цифр.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="c" /> является цифрой; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsNumber(char c)
    {
      if (!char.IsLatin1(c))
        return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(c));
      if (!char.IsAscii(c))
        return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
      if (c >= '0')
        return c <= '9';
      return false;
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ в указанной позиции в указанной строке к категории цифр.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если символ в позиции <paramref name="index" /> в <paramref name="s" /> является цифрой; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsNumber(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      char ch = s[index];
      if (!char.IsLatin1(ch))
        return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(s, index));
      if (!char.IsAscii(ch))
        return char.CheckNumber(char.GetLatin1UnicodeCategory(ch));
      if (ch >= '0')
        return ch <= '9';
      return false;
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ в указанной позиции в указанной строке к категории знаков препинания.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если знак в позиции <paramref name="index" /> в <paramref name="s" /> является знаком препинания; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsPunctuation(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      char ch = s[index];
      if (char.IsLatin1(ch))
        return char.CheckPunctuation(char.GetLatin1UnicodeCategory(ch));
      return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(s, index));
    }

    internal static bool CheckSeparator(UnicodeCategory uc)
    {
      switch (uc)
      {
        case UnicodeCategory.SpaceSeparator:
        case UnicodeCategory.LineSeparator:
        case UnicodeCategory.ParagraphSeparator:
          return true;
        default:
          return false;
      }
    }

    private static bool IsSeparatorLatin1(char c)
    {
      if (c != ' ')
        return c == ' ';
      return true;
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ Юникода к категории знаков-разделителей.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="c" /> является знаком-разделителем; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsSeparator(char c)
    {
      if (char.IsLatin1(c))
        return char.IsSeparatorLatin1(c);
      return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(c));
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ в указанной позиции в указанной строке к категории знаков-разделителей.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если символ в позиции <paramref name="index" /> в <paramref name="s" /> является знаком-разделителем; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsSeparator(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      char ch = s[index];
      if (char.IsLatin1(ch))
        return char.IsSeparatorLatin1(ch);
      return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(s, index));
    }

    /// <summary>
    ///   Указывает, имеет ли заданный символ заменяющую кодовую единицу.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="c" /> имеет большее или меньшее заменяющее значение; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsSurrogate(char c)
    {
      if (c >= '\xD800')
        return c <= '\xDFFF';
      return false;
    }

    /// <summary>
    ///   Указывает, имеет ли указанный символ в указанной позиции в указанной строке заменяющую кодовую единицу.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если символ в позиции <paramref name="index" /> в <paramref name="s" /> является большим или меньшим заменяющим значением; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsSurrogate(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      return char.IsSurrogate(s[index]);
    }

    internal static bool CheckSymbol(UnicodeCategory uc)
    {
      switch (uc)
      {
        case UnicodeCategory.MathSymbol:
        case UnicodeCategory.CurrencySymbol:
        case UnicodeCategory.ModifierSymbol:
        case UnicodeCategory.OtherSymbol:
          return true;
        default:
          return false;
      }
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ Юникода к категории символьных знаков.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="c" /> является символьным знаком; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsSymbol(char c)
    {
      if (char.IsLatin1(c))
        return char.CheckSymbol(char.GetLatin1UnicodeCategory(c));
      return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(c));
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ в указанной позиции в указанной строке к категории символьных знаков.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если символ в позиции <paramref name="index" /> в <paramref name="s" /> является символьным знаком; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsSymbol(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      if (char.IsLatin1(s[index]))
        return char.CheckSymbol(char.GetLatin1UnicodeCategory(s[index]));
      return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(s, index));
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ в указанной позиции в указанной строке к категории букв верхнего регистра.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если сивел в позиции <paramref name="index" /> в <paramref name="s" /> является буквой верхнего регистра; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsUpper(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      char ch = s[index];
      if (!char.IsLatin1(ch))
        return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.UppercaseLetter;
      if (!char.IsAscii(ch))
        return char.GetLatin1UnicodeCategory(ch) == UnicodeCategory.UppercaseLetter;
      if (ch >= 'A')
        return ch <= 'Z';
      return false;
    }

    /// <summary>
    ///   Показывает, относится ли указанный символ в указанной позиции в указанной строке к категории пробелов.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если символ в позиции <paramref name="index" /> в <paramref name="s" /> является пробелом; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsWhiteSpace(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      if (char.IsLatin1(s[index]))
        return char.IsWhiteSpaceLatin1(s[index]);
      return CharUnicodeInfo.IsWhiteSpace(s, index);
    }

    /// <summary>
    ///   Относит указанный символ Юникода к группе, определенной одним из значений <see cref="T:System.Globalization.UnicodeCategory" />.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который подлежит категоризации.
    /// </param>
    /// <returns>
    ///   Значение <see cref="T:System.Globalization.UnicodeCategory" />, которое определяет группу, содержащую <paramref name="c" />.
    /// </returns>
    public static UnicodeCategory GetUnicodeCategory(char c)
    {
      if (char.IsLatin1(c))
        return char.GetLatin1UnicodeCategory(c);
      return CharUnicodeInfo.InternalGetUnicodeCategory((int) c);
    }

    /// <summary>
    ///   Относит символ Юникода в указанной позиции к группе, определенной одним из значений <see cref="T:System.Globalization.UnicodeCategory" />.
    /// </summary>
    /// <param name="s">
    ///   Объект <see cref="T:System.String" />.
    /// </param>
    /// <param name="index">
    ///   Позиция символа в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Перечислимая константа <see cref="T:System.Globalization.UnicodeCategory" />, определяющая группу, которая содержит символ в позиции <paramref name="index" /> в <paramref name="s" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    public static UnicodeCategory GetUnicodeCategory(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      if (char.IsLatin1(s[index]))
        return char.GetLatin1UnicodeCategory(s[index]);
      return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
    }

    /// <summary>
    ///   Преобразует указанный числовой символ Юникода в число двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо преобразовать.
    /// </param>
    /// <returns>
    ///   Числовое значение параметра <paramref name="c" />, если данный символ представляет число; в противном случае — значение -1,0.
    /// </returns>
    [__DynamicallyInvokable]
    public static double GetNumericValue(char c)
    {
      return CharUnicodeInfo.GetNumericValue(c);
    }

    /// <summary>
    ///   Преобразует числовой символ Юникода в указанной позиции в указанной строке в число двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="s">
    ///   Объект <see cref="T:System.String" />.
    /// </param>
    /// <param name="index">
    ///   Позиция символа в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Числовое значение символа в позиции <paramref name="index" /> в <paramref name="s" />, если данный символ представляет число; в противном случае — значение -1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="index" /> имеет значение меньше нуля или больше последней позиции в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static double GetNumericValue(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if ((uint) index >= (uint) s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      return CharUnicodeInfo.GetNumericValue(s, index);
    }

    /// <summary>
    ///   Определяет, является ли заданный объект <see cref="T:System.Char" /> старшим символом-заместителем.
    /// </summary>
    /// <param name="c">
    ///   Знак Юникода, который необходимо вычислить.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если числовое значение параметра <paramref name="c" /> лежит в диапазоне от U+D800 до U+DBFF; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsHighSurrogate(char c)
    {
      if (c >= '\xD800')
        return c <= '\xDBFF';
      return false;
    }

    /// <summary>
    ///   Определяет, является ли объект <see cref="T:System.Char" /> в заданной позиции в строке старшим символом-заместителем.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если числовое значение заданного символа в параметре <paramref name="s" /> лежит в диапазоне от U+D800 до U+DBFF; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> не является позицией в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsHighSurrogate(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      return char.IsHighSurrogate(s[index]);
    }

    /// <summary>
    ///   Определяет, является ли заданный объект <see cref="T:System.Char" /> младшим символом-заместителем.
    /// </summary>
    /// <param name="c">Символ, который необходимо вычислить.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если числовое значение параметра <paramref name="c" /> лежит в диапазоне от U+DC00 до U+DFFF; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsLowSurrogate(char c)
    {
      if (c >= '\xDC00')
        return c <= '\xDFFF';
      return false;
    }

    /// <summary>
    ///   Определяет, является ли объект <see cref="T:System.Char" /> в заданной позиции в строке младшим символом-заместителем.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Позиция символа, который необходимо вычислить в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если числовое значение заданного символа в параметре <paramref name="s" /> лежит в диапазоне от U+DC00 до U+DFFF; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> не является позицией в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsLowSurrogate(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      return char.IsLowSurrogate(s[index]);
    }

    /// <summary>
    ///   Определяет, образуют ли два смежных объекта <see cref="T:System.Char" /> в заданной позиции в строке суррогатную пару.
    /// </summary>
    /// <param name="s">Строка.</param>
    /// <param name="index">
    ///   Начальная позиция пары символов для вычисления в строке <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="s" /> включает соседние знаки в позициях <paramref name="index" /> и <paramref name="index" /> + 1, числовое значение знака в позиции <paramref name="index" /> лежит в диапазоне от U+D800 до U+DBFF и числовое значение знака в позиции <paramref name="index" /> +1 лежит в диапазоне от U+DC00 до U+DFFF; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> не является позицией в <paramref name="s" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsSurrogatePair(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      if (index + 1 < s.Length)
        return char.IsSurrogatePair(s[index], s[index + 1]);
      return false;
    }

    /// <summary>
    ///   Определяет, образуют ли два заданных объекта <see cref="T:System.Char" /> суррогатную пару.
    /// </summary>
    /// <param name="highSurrogate">
    ///   Символ, который необходимо вычислить в качестве старшего знака-заменителя в паре.
    /// </param>
    /// <param name="lowSurrogate">
    ///   Символ, который необходимо вычислить в качестве младшего знака-заменителя в паре.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если числовое значение параметра <paramref name="highSurrogate" /> лежит в диапазоне от U+D800 до U+DBFF, а числовое значение параметра <paramref name="lowSurrogate" /> лежит в диапазоне от U+DC00 до U+DFFF; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
    {
      if (highSurrogate >= '\xD800' && highSurrogate <= '\xDBFF' && lowSurrogate >= '\xDC00')
        return lowSurrogate <= '\xDFFF';
      return false;
    }

    /// <summary>
    ///   Преобразует заданную кодовую точку Юникода в строку в кодировке UTF-16.
    /// </summary>
    /// <param name="utf32">21-битовая кодовая точка Юникода.</param>
    /// <returns>
    ///   Строка, состоящая из одного объекта <see cref="T:System.Char" /> или суррогатной пары объектов <see cref="T:System.Char" />, эквивалентной кодовой точке, заданной в параметре <paramref name="utf32" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="utf32" /> является недопустимой 21-битовой кодовой точкой Юникода, входящей в диапазон начиная от U+0 до U+10FFFF, за исключением суррогатной пары в диапазоне от U+D800 до U+DBFF.
    /// </exception>
    [__DynamicallyInvokable]
    public static string ConvertFromUtf32(int utf32)
    {
      if (utf32 < 0 || utf32 > 1114111 || utf32 >= 55296 && utf32 <= 57343)
        throw new ArgumentOutOfRangeException(nameof (utf32), Environment.GetResourceString("ArgumentOutOfRange_InvalidUTF32"));
      if (utf32 < 65536)
        return char.ToString((char) utf32);
      utf32 -= 65536;
      return new string(new char[2]
      {
        (char) (utf32 / 1024 + 55296),
        (char) (utf32 % 1024 + 56320)
      });
    }

    /// <summary>
    ///   Преобразует значение суррогатной пары в кодировке UTF-16 в кодовую точку Юникода.
    /// </summary>
    /// <param name="highSurrogate">
    ///   Старшая замещающая единица кода (то есть единица кода в диапазоне от U+D800 до U+DBFF).
    /// </param>
    /// <param name="lowSurrogate">
    ///   Младшая замещающая единица кода (то есть единица кода в диапазоне от U+DC00 до U+DFFF).
    /// </param>
    /// <returns>
    ///   21-битовая кодовая точка Юникода, представленная параметрами <paramref name="highSurrogate" /> и <paramref name="lowSurrogate" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="highSurrogate" /> не находится в диапазоне от U+D800 до U+DBFF или <paramref name="lowSurrogate" /> не находится в диапазоне от U+DC00 до U+DFFF.
    /// </exception>
    [__DynamicallyInvokable]
    public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
    {
      if (!char.IsHighSurrogate(highSurrogate))
        throw new ArgumentOutOfRangeException(nameof (highSurrogate), Environment.GetResourceString("ArgumentOutOfRange_InvalidHighSurrogate"));
      if (!char.IsLowSurrogate(lowSurrogate))
        throw new ArgumentOutOfRangeException(nameof (lowSurrogate), Environment.GetResourceString("ArgumentOutOfRange_InvalidLowSurrogate"));
      return ((int) highSurrogate - 55296) * 1024 + ((int) lowSurrogate - 56320) + 65536;
    }

    /// <summary>
    ///   Преобразует значение символа в кодировке UTF-16 или суррогатную пару в заданной позиции в строке в кодовую точку Юникода.
    /// </summary>
    /// <param name="s">
    ///   Строка, содержащая символ или суррогатную пару.
    /// </param>
    /// <param name="index">
    ///   Позиция индекса символа или суррогатной пары в <paramref name="s" />.
    /// </param>
    /// <returns>
    ///   21-битовая кодовая точка Юникода, представленная символом или суррогатной парой в позиции в строке <paramref name="s" />, заданной параметром <paramref name="index" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> не является позицией в <paramref name="s" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Заданная позиция индекса содержит суррогатную пару. Первый символ в паре не является допустимым старшим заместителем, или второй символ в паре не является допустимым младшим заместителем.
    /// </exception>
    [__DynamicallyInvokable]
    public static int ConvertToUtf32(string s, int index)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      if (index < 0 || index >= s.Length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      int num1 = (int) s[index] - 55296;
      if (num1 < 0 || num1 > 2047)
        return (int) s[index];
      if (num1 <= 1023)
      {
        if (index < s.Length - 1)
        {
          int num2 = (int) s[index + 1] - 56320;
          if (num2 >= 0 && num2 <= 1023)
            return num1 * 1024 + num2 + 65536;
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHighSurrogate", (object) index), nameof (s));
        }
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHighSurrogate", (object) index), nameof (s));
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidLowSurrogate", (object) index), nameof (s));
    }
  }
}
