// Decompiled with JetBrains decompiler
// Type: System.Globalization.IdnMapping
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Globalization
{
  /// <summary>
  ///   Поддерживает использование символов, не относящихся к ASCII, для имен доменов Интернета.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class IdnMapping
  {
    private static char[] M_Dots = new char[4]
    {
      '.',
      '。',
      '．',
      '｡'
    };
    private const int M_labelLimit = 63;
    private const int M_defaultNameLimit = 255;
    private const string M_strAcePrefix = "xn--";
    private bool m_bAllowUnassigned;
    private bool m_bUseStd3AsciiRules;
    private const int punycodeBase = 36;
    private const int tmin = 1;
    private const int tmax = 26;
    private const int skew = 38;
    private const int damp = 700;
    private const int initial_bias = 72;
    private const int initial_n = 128;
    private const char delimiter = '-';
    private const int maxint = 134217727;
    private const int IDN_ALLOW_UNASSIGNED = 1;
    private const int IDN_USE_STD3_ASCII_RULES = 2;
    private const int ERROR_INVALID_NAME = 123;

    /// <summary>
    ///   Возвращает или задает значение, указывающее, используются ли точки кода Юникод в операциях, выполняемых элементами текущего <see cref="T:System.Globalization.IdnMapping" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если точки кода без знака, используются в операциях; в противном случае — <see langword="false" />.
    /// </returns>
    public bool AllowUnassigned
    {
      get
      {
        return this.m_bAllowUnassigned;
      }
      set
      {
        this.m_bAllowUnassigned = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, используются ли в операциях, выполняемых элементами текущего соглашения об именовании стандартного или неявное <see cref="T:System.Globalization.IdnMapping" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если стандартным соглашениям об именах используются в операциях; в противном случае — <see langword="false" />.
    /// </returns>
    public bool UseStd3AsciiRules
    {
      get
      {
        return this.m_bUseStd3AsciiRules;
      }
      set
      {
        this.m_bUseStd3AsciiRules = value;
      }
    }

    /// <summary>
    ///   Кодирует строку меток доменных имен, состоящих из символов Юникода в строку отображаемых символов Юникода в диапазоне символов US-ASCII.
    ///    Строка форматируется в соответствии со стандартом IDNA.
    /// </summary>
    /// <param name="unicode">
    ///   Строка для преобразования, которая состоит из одного или нескольких меток доменного имени, разделенных особыми символами.
    /// </param>
    /// <returns>
    ///   Эквивалент строки, заданной параметром <paramref name="unicode" /> параметр, состоящей из отображаемых символов Юникода в US-ASCII (U + 0020 до U + 007E) диапазон символов и отформатированное в соответствии со стандартом IDNA.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="unicode" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="unicode" /> Недопустимый основана на <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> и <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> Свойства и стандартом IDNA.
    /// </exception>
    public string GetAscii(string unicode)
    {
      return this.GetAscii(unicode, 0);
    }

    /// <summary>
    ///   Кодирует подстроку меток доменных имен, которые содержат символы Юникода вне диапазона символов US-ASCII.
    ///    Подстрока преобразуется в строку отображаемых символов Юникода в диапазоне символов US-ASCII и отформатированное в соответствии со стандартом IDNA.
    /// </summary>
    /// <param name="unicode">
    ///   Строка для преобразования, которая состоит из одного или нескольких меток доменного имени, разделенных особыми символами.
    /// </param>
    /// <param name="index">
    ///   Отсчитываемый от нуля смещение в <paramref name="unicode" /> указывающее начало подстроки, которую требуется преобразовать.
    ///    Процедура преобразования продолжается до конца <paramref name="unicode" /> строку.
    /// </param>
    /// <returns>
    ///   Эквивалент подстроки, указанной параметрами <paramref name="unicode" /> и <paramref name="index" /> параметрами, состоящей из отображаемых символов Юникода в US-ASCII (U + 0020 до U + 007E) диапазон символов и отформатированное в соответствии со стандартом IDNA.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="unicode" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="index" /> больше, чем длина <paramref name="unicode" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="unicode" /> Недопустимый основана на <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> и <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> Свойства и стандартом IDNA.
    /// </exception>
    public string GetAscii(string unicode, int index)
    {
      if (unicode == null)
        throw new ArgumentNullException(nameof (unicode));
      return this.GetAscii(unicode, index, unicode.Length - index);
    }

    /// <summary>
    ///   Кодирует указанное количество символов в подстроке меток доменных имен, которые содержат символы Юникода вне диапазона символов US-ASCII.
    ///    Подстрока преобразуется в строку отображаемых символов Юникода в диапазоне символов US-ASCII и отформатированное в соответствии со стандартом IDNA.
    /// </summary>
    /// <param name="unicode">
    ///   Строка для преобразования, которая состоит из одного или нескольких меток доменного имени, разделенных особыми символами.
    /// </param>
    /// <param name="index">
    ///   Отсчитываемый от нуля смещение в <paramref name="unicode" /> указывающее начало подстроки.
    /// </param>
    /// <param name="count">
    ///   Количество символов для преобразования в подстроку, которая начинается с позиции, указанной параметром  <paramref name="index" /> в <paramref name="unicode" /> строку.
    /// </param>
    /// <returns>
    ///   Эквивалент подстроки, указанной параметрами <paramref name="unicode" />, <paramref name="index" />, и <paramref name="count" /> параметрами, состоящей из отображаемых символов Юникода в US-ASCII (U + 0020 до U + 007E) диапазон символов и отформатированное в соответствии со стандартом IDNA.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="unicode" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="index" /> больше, чем длина <paramref name="unicode" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="index" /> больше, чем длина <paramref name="unicode" /> минус <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="unicode" /> Недопустимый основана на <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> и <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> Свойства и стандартом IDNA.
    /// </exception>
    public string GetAscii(string unicode, int index, int count)
    {
      if (unicode == null)
        throw new ArgumentNullException(nameof (unicode));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (index > unicode.Length)
        throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (index > unicode.Length - count)
        throw new ArgumentOutOfRangeException(nameof (unicode), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      unicode = unicode.Substring(index, count);
      if (Environment.IsWindows8OrAbove)
        return this.GetAsciiUsingOS(unicode);
      if (IdnMapping.ValidateStd3AndAscii(unicode, this.UseStd3AsciiRules, true))
        return unicode;
      if (unicode[unicode.Length - 1] <= '\x001F')
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequence", (object) (unicode.Length - 1)), nameof (unicode));
      bool flag = unicode.Length > 0 && IdnMapping.IsDot(unicode[unicode.Length - 1]);
      unicode = unicode.Normalize(this.m_bAllowUnassigned ? (NormalizationForm) 13 : (NormalizationForm) 269);
      if (!flag && unicode.Length > 0 && IdnMapping.IsDot(unicode[unicode.Length - 1]))
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), nameof (unicode));
      if (this.UseStd3AsciiRules)
        IdnMapping.ValidateStd3AndAscii(unicode, true, false);
      return IdnMapping.punycode_encode(unicode);
    }

    [SecuritySafeCritical]
    private string GetAsciiUsingOS(string unicode)
    {
      if (unicode.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), nameof (unicode));
      if (unicode[unicode.Length - 1] == char.MinValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequence", (object) (unicode.Length - 1)), nameof (unicode));
      uint dwFlags = (uint) ((this.AllowUnassigned ? 1 : 0) | (this.UseStd3AsciiRules ? 2 : 0));
      int ascii1 = IdnMapping.IdnToAscii(dwFlags, unicode, unicode.Length, (char[]) null, 0);
      if (ascii1 == 0)
      {
        if (Marshal.GetLastWin32Error() == 123)
          throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), nameof (unicode));
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"), nameof (unicode));
      }
      char[] lpASCIICharStr = new char[ascii1];
      int ascii2 = IdnMapping.IdnToAscii(dwFlags, unicode, unicode.Length, lpASCIICharStr, ascii1);
      if (ascii2 != 0)
        return new string(lpASCIICharStr, 0, ascii2);
      if (Marshal.GetLastWin32Error() == 123)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), nameof (unicode));
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"), nameof (unicode));
    }

    /// <summary>
    ///   Декодирует строку один или несколько меток доменного имени, в соответствии со стандартом IDNA в строку символов Юникода.
    /// </summary>
    /// <param name="ascii">
    ///   Декодируемая строка, которая состоит из одной или нескольких меток в диапазоне символов US-ASCII (U + 0020 до U + 007E) кодируются в соответствии со стандартом IDNA.
    /// </param>
    /// <returns>
    ///   Символ Юникода, эквивалентный IDNA подстроки, указанной параметрами <paramref name="ascii" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="ascii" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="ascii" /> Недопустимый основана на <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> и <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> Свойства и стандартом IDNA.
    /// </exception>
    public string GetUnicode(string ascii)
    {
      return this.GetUnicode(ascii, 0);
    }

    /// <summary>
    ///   Декодирует подстроку, состоящую из одной или нескольких меток доменного имени, в соответствии со стандартом IDNA в строку символов Юникода.
    /// </summary>
    /// <param name="ascii">
    ///   Декодируемая строка, которая состоит из одной или нескольких меток в диапазоне символов US-ASCII (U + 0020 до U + 007E) кодируются в соответствии со стандартом IDNA.
    /// </param>
    /// <param name="index">
    ///   Отсчитываемый от нуля смещение в <paramref name="ascii" /> указывающее начало подстроки, которую требуется декодировать.
    ///    Операции декодирования продолжается до конца <paramref name="ascii" /> строку.
    /// </param>
    /// <returns>
    ///   Символ Юникода, эквивалентный подстроки IDNA, указанной в <paramref name="ascii" /> и <paramref name="index" /> параметров.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="ascii" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="index" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="index" /> больше, чем длина <paramref name="ascii" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="ascii" /> Недопустимый основана на <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> и <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> Свойства и стандартом IDNA.
    /// </exception>
    public string GetUnicode(string ascii, int index)
    {
      if (ascii == null)
        throw new ArgumentNullException(nameof (ascii));
      return this.GetUnicode(ascii, index, ascii.Length - index);
    }

    /// <summary>
    ///   Декодирует подстроку указанной длины, содержащий один или несколько меток доменного имени, в соответствии со стандартом IDNA в строку символов Юникода.
    /// </summary>
    /// <param name="ascii">
    ///   Декодируемая строка, которая состоит из одной или нескольких меток в диапазоне символов US-ASCII (U + 0020 до U + 007E) кодируются в соответствии со стандартом IDNA.
    /// </param>
    /// <param name="index">
    ///   Отсчитываемый от нуля смещение в <paramref name="ascii" /> указывающее начало подстроки.
    /// </param>
    /// <param name="count">
    ///   Количество символов для преобразования в подстроку, которая начинается с позиции, указанной параметром <paramref name="index" /> в <paramref name="ascii" /> строку.
    /// </param>
    /// <returns>
    ///   Символ Юникода, эквивалентный IDNA подстроки, указанной параметрами <paramref name="ascii" />, <paramref name="index" />, и <paramref name="count" /> параметров.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="ascii" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="index" /> больше, чем длина <paramref name="ascii" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="index" /> больше, чем длина <paramref name="ascii" /> минус <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="ascii" /> Недопустимый основана на <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> и <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> Свойства и стандартом IDNA.
    /// </exception>
    public string GetUnicode(string ascii, int index, int count)
    {
      if (ascii == null)
        throw new ArgumentNullException(nameof (ascii));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (index > ascii.Length)
        throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (index > ascii.Length - count)
        throw new ArgumentOutOfRangeException(nameof (ascii), Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (count > 0 && ascii[index + count - 1] == char.MinValue)
        throw new ArgumentException(nameof (ascii), Environment.GetResourceString("Argument_IdnBadPunycode"));
      ascii = ascii.Substring(index, count);
      if (Environment.IsWindows8OrAbove)
        return this.GetUnicodeUsingOS(ascii);
      string unicode = IdnMapping.punycode_decode(ascii);
      if (!ascii.Equals(this.GetAscii(unicode), StringComparison.OrdinalIgnoreCase))
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), nameof (ascii));
      return unicode;
    }

    [SecuritySafeCritical]
    private string GetUnicodeUsingOS(string ascii)
    {
      uint dwFlags = (uint) ((this.AllowUnassigned ? 1 : 0) | (this.UseStd3AsciiRules ? 2 : 0));
      int unicode1 = IdnMapping.IdnToUnicode(dwFlags, ascii, ascii.Length, (char[]) null, 0);
      if (unicode1 == 0)
      {
        if (Marshal.GetLastWin32Error() == 123)
          throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), nameof (ascii));
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), nameof (ascii));
      }
      char[] lpUnicodeCharStr = new char[unicode1];
      int unicode2 = IdnMapping.IdnToUnicode(dwFlags, ascii, ascii.Length, lpUnicodeCharStr, unicode1);
      if (unicode2 != 0)
        return new string(lpUnicodeCharStr, 0, unicode2);
      if (Marshal.GetLastWin32Error() == 123)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), nameof (ascii));
      throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), nameof (ascii));
    }

    /// <summary>
    ///   Указывает, является ли указанный объект и текущий <see cref="T:System.Globalization.IdnMapping" /> объекта равны.
    /// </summary>
    /// <param name="obj">Объект, сравниваемый с текущим объектом.</param>
    /// <returns>
    ///   <see langword="true" /> Если объект, заданный параметром <paramref name="obj" /> параметр является производным от <see cref="T:System.Globalization.IdnMapping" /> и его <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> и <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> свойства равны; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      IdnMapping idnMapping = obj as IdnMapping;
      if (idnMapping != null && this.m_bAllowUnassigned == idnMapping.m_bAllowUnassigned)
        return this.m_bUseStd3AsciiRules == idnMapping.m_bUseStd3AsciiRules;
      return false;
    }

    /// <summary>
    ///   Возвращает хэш-код для этого <see cref="T:System.Globalization.IdnMapping" /> объекта.
    /// </summary>
    /// <returns>
    ///   Один из четырех 32-разрядных констант со знаком на основе свойства <see cref="T:System.Globalization.IdnMapping" /> объекта.
    ///     Возвращаемое значение не имеет особого смысла и не подходит для использования в алгоритме хэш-кода.
    /// </returns>
    public override int GetHashCode()
    {
      return (this.m_bAllowUnassigned ? 100 : 200) + (this.m_bUseStd3AsciiRules ? 1000 : 2000);
    }

    private static bool IsSupplementary(int cTest)
    {
      return cTest >= 65536;
    }

    private static bool IsDot(char c)
    {
      if (c != '.' && c != '。' && c != '．')
        return c == '｡';
      return true;
    }

    private static bool ValidateStd3AndAscii(string unicode, bool bUseStd3, bool bCheckAscii)
    {
      if (unicode.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), nameof (unicode));
      int num = -1;
      for (int index = 0; index < unicode.Length; ++index)
      {
        if (unicode[index] <= '\x001F')
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequence", (object) index), nameof (unicode));
        if (bCheckAscii && unicode[index] >= '\x007F')
          return false;
        if (IdnMapping.IsDot(unicode[index]))
        {
          if (index == num + 1)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), nameof (unicode));
          if (index - num > 64)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "Unicode");
          if (bUseStd3 && index > 0)
            IdnMapping.ValidateStd3(unicode[index - 1], true);
          num = index;
        }
        else if (bUseStd3)
          IdnMapping.ValidateStd3(unicode[index], index == num + 1);
      }
      if (num == -1 && unicode.Length > 63)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), nameof (unicode));
      if (unicode.Length > (int) byte.MaxValue - (IdnMapping.IsDot(unicode[unicode.Length - 1]) ? 0 : 1))
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadNameSize", (object) ((int) byte.MaxValue - (IdnMapping.IsDot(unicode[unicode.Length - 1]) ? 0 : 1))), nameof (unicode));
      if (bUseStd3 && !IdnMapping.IsDot(unicode[unicode.Length - 1]))
        IdnMapping.ValidateStd3(unicode[unicode.Length - 1], true);
      return true;
    }

    private static void ValidateStd3(char c, bool bNextToDot)
    {
      if (c <= ',' || c == '/' || c >= ':' && c <= '@' || (c >= '[' && c <= '`' || c >= '{' && c <= '\x007F') || c == '-' & bNextToDot)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadStd3", (object) c), "Unicode");
    }

    private static bool HasUpperCaseFlag(char punychar)
    {
      if (punychar >= 'A')
        return punychar <= 'Z';
      return false;
    }

    private static bool basic(uint cp)
    {
      return cp < 128U;
    }

    private static int decode_digit(char cp)
    {
      if (cp >= '0' && cp <= '9')
        return (int) cp - 48 + 26;
      if (cp >= 'a' && cp <= 'z')
        return (int) cp - 97;
      if (cp >= 'A' && cp <= 'Z')
        return (int) cp - 65;
      throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
    }

    private static char encode_digit(int d)
    {
      if (d > 25)
        return (char) (d - 26 + 48);
      return (char) (d + 97);
    }

    private static char encode_basic(char bcp)
    {
      if (IdnMapping.HasUpperCaseFlag(bcp))
        bcp += ' ';
      return bcp;
    }

    private static int adapt(int delta, int numpoints, bool firsttime)
    {
      delta = firsttime ? delta / 700 : delta / 2;
      delta += delta / numpoints;
      uint num = 0;
      while (delta > 455)
      {
        delta /= 35;
        num += 36U;
      }
      return (int) ((long) num + (long) (36 * delta / (delta + 38)));
    }

    private static string punycode_encode(string unicode)
    {
      if (unicode.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), nameof (unicode));
      StringBuilder stringBuilder = new StringBuilder(unicode.Length);
      int num1 = 0;
      int num2 = 0;
      int startIndex = 0;
      while (num1 < unicode.Length)
      {
        num1 = unicode.IndexOfAny(IdnMapping.M_Dots, num2);
        if (num1 < 0)
          num1 = unicode.Length;
        if (num1 == num2)
        {
          if (num1 != unicode.Length)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), nameof (unicode));
          break;
        }
        stringBuilder.Append("xn--");
        bool flag = false;
        switch (CharUnicodeInfo.GetBidiCategory(unicode, num2))
        {
          case BidiCategory.RightToLeft:
          case BidiCategory.RightToLeftArabic:
            flag = true;
            int index1 = num1 - 1;
            if (char.IsLowSurrogate(unicode, index1))
              --index1;
            switch (CharUnicodeInfo.GetBidiCategory(unicode, index1))
            {
              case BidiCategory.RightToLeft:
              case BidiCategory.RightToLeftArabic:
                break;
              default:
                throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), nameof (unicode));
            }
        }
        int num3 = 0;
        for (int index2 = num2; index2 < num1; ++index2)
        {
          BidiCategory bidiCategory = CharUnicodeInfo.GetBidiCategory(unicode, index2);
          if (flag && bidiCategory == BidiCategory.LeftToRight)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), nameof (unicode));
          if (!flag && (bidiCategory == BidiCategory.RightToLeft || bidiCategory == BidiCategory.RightToLeftArabic))
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), nameof (unicode));
          if (IdnMapping.basic((uint) unicode[index2]))
          {
            stringBuilder.Append(IdnMapping.encode_basic(unicode[index2]));
            ++num3;
          }
          else if (char.IsSurrogatePair(unicode, index2))
            ++index2;
        }
        int num4 = num3;
        if (num4 == num1 - num2)
        {
          stringBuilder.Remove(startIndex, "xn--".Length);
        }
        else
        {
          if (unicode.Length - num2 >= "xn--".Length && unicode.Substring(num2, "xn--".Length).Equals("xn--", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), nameof (unicode));
          int num5 = 0;
          if (num4 > 0)
            stringBuilder.Append('-');
          int num6 = 128;
          int num7 = 0;
          int num8 = 72;
          while (num3 < num1 - num2)
          {
            int cTest = 134217727;
            int index2 = num2;
            while (index2 < num1)
            {
              int utf32 = char.ConvertToUtf32(unicode, index2);
              if (utf32 >= num6 && utf32 < cTest)
                cTest = utf32;
              index2 += IdnMapping.IsSupplementary(utf32) ? 2 : 1;
            }
            int delta = num7 + (cTest - num6) * (num3 - num5 + 1);
            int num9 = cTest;
            int index3 = num2;
            while (index3 < num1)
            {
              int utf32 = char.ConvertToUtf32(unicode, index3);
              if (utf32 < num9)
                ++delta;
              if (utf32 == num9)
              {
                int d = delta;
                int num10 = 36;
                while (true)
                {
                  int num11 = num10 <= num8 ? 1 : (num10 >= num8 + 26 ? 26 : num10 - num8);
                  if (d >= num11)
                  {
                    stringBuilder.Append(IdnMapping.encode_digit(num11 + (d - num11) % (36 - num11)));
                    d = (d - num11) / (36 - num11);
                    num10 += 36;
                  }
                  else
                    break;
                }
                stringBuilder.Append(IdnMapping.encode_digit(d));
                num8 = IdnMapping.adapt(delta, num3 - num5 + 1, num3 == num4);
                delta = 0;
                ++num3;
                if (IdnMapping.IsSupplementary(cTest))
                {
                  ++num3;
                  ++num5;
                }
              }
              index3 += IdnMapping.IsSupplementary(utf32) ? 2 : 1;
            }
            num7 = delta + 1;
            num6 = num9 + 1;
          }
        }
        if (stringBuilder.Length - startIndex > 63)
          throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), nameof (unicode));
        if (num1 != unicode.Length)
          stringBuilder.Append('.');
        num2 = num1 + 1;
        startIndex = stringBuilder.Length;
      }
      if (stringBuilder.Length > (int) byte.MaxValue - (IdnMapping.IsDot(unicode[unicode.Length - 1]) ? 0 : 1))
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadNameSize", (object) ((int) byte.MaxValue - (IdnMapping.IsDot(unicode[unicode.Length - 1]) ? 0 : 1))), nameof (unicode));
      return stringBuilder.ToString();
    }

    private static string punycode_decode(string ascii)
    {
      if (ascii.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), nameof (ascii));
      if (ascii.Length > (int) byte.MaxValue - (IdnMapping.IsDot(ascii[ascii.Length - 1]) ? 0 : 1))
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadNameSize", (object) ((int) byte.MaxValue - (IdnMapping.IsDot(ascii[ascii.Length - 1]) ? 0 : 1))), nameof (ascii));
      StringBuilder stringBuilder = new StringBuilder(ascii.Length);
      int num1 = 0;
      int startIndex = 0;
      int index1 = 0;
      while (num1 < ascii.Length)
      {
        num1 = ascii.IndexOf('.', startIndex);
        if (num1 < 0 || num1 > ascii.Length)
          num1 = ascii.Length;
        if (num1 == startIndex)
        {
          if (num1 != ascii.Length)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), nameof (ascii));
          break;
        }
        if (num1 - startIndex > 63)
          throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), nameof (ascii));
        if (ascii.Length < "xn--".Length + startIndex || !ascii.Substring(startIndex, "xn--".Length).Equals("xn--", StringComparison.OrdinalIgnoreCase))
        {
          stringBuilder.Append(ascii.Substring(startIndex, num1 - startIndex));
        }
        else
        {
          startIndex += "xn--".Length;
          int num2 = ascii.LastIndexOf('-', num1 - 1);
          if (num2 == num1 - 1)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), nameof (ascii));
          int num3;
          if (num2 <= startIndex)
          {
            num3 = 0;
          }
          else
          {
            num3 = num2 - startIndex;
            for (int index2 = startIndex; index2 < startIndex + num3; ++index2)
            {
              if (ascii[index2] > '\x007F')
                throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), nameof (ascii));
              stringBuilder.Append(ascii[index2] < 'A' || ascii[index2] > 'Z' ? ascii[index2] : (char) ((int) ascii[index2] - 65 + 97));
            }
          }
          int num4 = startIndex + (num3 > 0 ? num3 + 1 : 0);
          int num5 = 128;
          int num6 = 72;
          int num7 = 0;
          int num8 = 0;
label_49:
          while (num4 < num1)
          {
            int num9 = num7;
            int num10 = 1;
            int num11 = 36;
            while (num4 < num1)
            {
              int num12 = IdnMapping.decode_digit(ascii[num4++]);
              if (num12 > (134217727 - num7) / num10)
                throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), nameof (ascii));
              num7 += num12 * num10;
              int num13 = num11 <= num6 ? 1 : (num11 >= num6 + 26 ? 26 : num11 - num6);
              if (num12 >= num13)
              {
                if (num10 > 134217727 / (36 - num13))
                  throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), nameof (ascii));
                num10 *= 36 - num13;
                num11 += 36;
              }
              else
              {
                num6 = IdnMapping.adapt(num7 - num9, stringBuilder.Length - index1 - num8 + 1, num9 == 0);
                if (num7 / (stringBuilder.Length - index1 - num8 + 1) > 134217727 - num5)
                  throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), nameof (ascii));
                num5 += num7 / (stringBuilder.Length - index1 - num8 + 1);
                int num14 = num7 % (stringBuilder.Length - index1 - num8 + 1);
                if (num5 < 0 || num5 > 1114111 || num5 >= 55296 && num5 <= 57343)
                  throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), nameof (ascii));
                string str = char.ConvertFromUtf32(num5);
                int index2;
                if (num8 > 0)
                {
                  int num15 = num14;
                  index2 = index1;
                  while (num15 > 0)
                  {
                    if (index2 >= stringBuilder.Length)
                      throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), nameof (ascii));
                    if (char.IsSurrogate(stringBuilder[index2]))
                      ++index2;
                    --num15;
                    ++index2;
                  }
                }
                else
                  index2 = index1 + num14;
                stringBuilder.Insert(index2, str);
                if (IdnMapping.IsSupplementary(num5))
                  ++num8;
                num7 = num14 + 1;
                goto label_49;
              }
            }
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), nameof (ascii));
          }
          bool flag = false;
          BidiCategory bidiCategory = CharUnicodeInfo.GetBidiCategory(stringBuilder.ToString(), index1);
          switch (bidiCategory)
          {
            case BidiCategory.RightToLeft:
            case BidiCategory.RightToLeftArabic:
              flag = true;
              break;
          }
          for (int index2 = index1; index2 < stringBuilder.Length; ++index2)
          {
            if (!char.IsLowSurrogate(stringBuilder.ToString(), index2))
            {
              bidiCategory = CharUnicodeInfo.GetBidiCategory(stringBuilder.ToString(), index2);
              if (flag && bidiCategory == BidiCategory.LeftToRight || !flag && (bidiCategory == BidiCategory.RightToLeft || bidiCategory == BidiCategory.RightToLeftArabic))
                throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), nameof (ascii));
            }
          }
          if (flag && bidiCategory != BidiCategory.RightToLeft && bidiCategory != BidiCategory.RightToLeftArabic)
            throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), nameof (ascii));
        }
        if (num1 - startIndex > 63)
          throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), nameof (ascii));
        if (num1 != ascii.Length)
          stringBuilder.Append('.');
        startIndex = num1 + 1;
        index1 = stringBuilder.Length;
      }
      if (stringBuilder.Length > (int) byte.MaxValue - (IdnMapping.IsDot(stringBuilder[stringBuilder.Length - 1]) ? 0 : 1))
        throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadNameSize", (object) ((int) byte.MaxValue - (IdnMapping.IsDot(stringBuilder[stringBuilder.Length - 1]) ? 0 : 1))), nameof (ascii));
      return stringBuilder.ToString();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int IdnToAscii(uint dwFlags, [MarshalAs(UnmanagedType.LPWStr), In] string lpUnicodeCharStr, int cchUnicodeChar, [Out] char[] lpASCIICharStr, int cchASCIIChar);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int IdnToUnicode(uint dwFlags, [MarshalAs(UnmanagedType.LPWStr), In] string lpASCIICharStr, int cchASCIIChar, [Out] char[] lpUnicodeCharStr, int cchUnicodeChar);
  }
}
