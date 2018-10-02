// Decompiled with JetBrains decompiler
// Type: System.Guid
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>
  ///   Представляет глобальный уникальный идентификатор (GUID).
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [ComVisible(true)]
  [NonVersionable]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Guid : IFormattable, IComparable, IComparable<Guid>, IEquatable<Guid>
  {
    /// <summary>
    ///   Только для чтения экземпляр <see cref="T:System.Guid" /> структуры, значение которого равно нулей.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly Guid Empty;
    private int _a;
    private short _b;
    private short _c;
    private byte _d;
    private byte _e;
    private byte _f;
    private byte _g;
    private byte _h;
    private byte _i;
    private byte _j;
    private byte _k;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Guid" /> структуры с помощью указанного массива байтов.
    /// </summary>
    /// <param name="b">
    ///   Массив 16 байтов, содержащий значения для инициализации GUID.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="b" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="b" />не содержит 16 байтов.
    /// </exception>
    [__DynamicallyInvokable]
    public Guid(byte[] b)
    {
      if (b == null)
        throw new ArgumentNullException(nameof (b));
      if (b.Length != 16)
        throw new ArgumentException(Environment.GetResourceString("Arg_GuidArrayCtor", (object) "16"));
      this._a = (int) b[3] << 24 | (int) b[2] << 16 | (int) b[1] << 8 | (int) b[0];
      this._b = (short) ((int) b[5] << 8 | (int) b[4]);
      this._c = (short) ((int) b[7] << 8 | (int) b[6]);
      this._d = b[8];
      this._e = b[9];
      this._f = b[10];
      this._g = b[11];
      this._h = b[12];
      this._i = b[13];
      this._j = b[14];
      this._k = b[15];
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Guid" /> структуру, используя указанный неподписанные целых чисел и байтов.
    /// </summary>
    /// <param name="a">Первые 4 байта GUID.</param>
    /// <param name="b">Следующие 2 байта GUID.</param>
    /// <param name="c">Следующие 2 байта GUID.</param>
    /// <param name="d">Следующий байт GUID.</param>
    /// <param name="e">Следующий байт GUID.</param>
    /// <param name="f">Следующий байт GUID.</param>
    /// <param name="g">Следующий байт GUID.</param>
    /// <param name="h">Следующий байт GUID.</param>
    /// <param name="i">Следующий байт GUID.</param>
    /// <param name="j">Следующий байт GUID.</param>
    /// <param name="k">Следующий байт GUID.</param>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public Guid(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
    {
      this._a = (int) a;
      this._b = (short) b;
      this._c = (short) c;
      this._d = d;
      this._e = e;
      this._f = f;
      this._g = g;
      this._h = h;
      this._i = i;
      this._j = j;
      this._k = k;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Guid" /> структуры с помощью указанных целых чисел и массива байтов.
    /// </summary>
    /// <param name="a">Первые 4 байта GUID.</param>
    /// <param name="b">Следующие 2 байта GUID.</param>
    /// <param name="c">Следующие 2 байта GUID.</param>
    /// <param name="d">Оставшиеся 8 байтов GUID.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="d" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="d" />не содержит 8 байтов.
    /// </exception>
    [__DynamicallyInvokable]
    public Guid(int a, short b, short c, byte[] d)
    {
      if (d == null)
        throw new ArgumentNullException(nameof (d));
      if (d.Length != 8)
        throw new ArgumentException(Environment.GetResourceString("Arg_GuidArrayCtor", (object) "8"));
      this._a = a;
      this._b = b;
      this._c = c;
      this._d = d[0];
      this._e = d[1];
      this._f = d[2];
      this._g = d[3];
      this._h = d[4];
      this._i = d[5];
      this._j = d[6];
      this._k = d[7];
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Guid" /> структуры с помощью указанных целых чисел и байтов.
    /// </summary>
    /// <param name="a">Первые 4 байта GUID.</param>
    /// <param name="b">Следующие 2 байта GUID.</param>
    /// <param name="c">Следующие 2 байта GUID.</param>
    /// <param name="d">Следующий байт GUID.</param>
    /// <param name="e">Следующий байт GUID.</param>
    /// <param name="f">Следующий байт GUID.</param>
    /// <param name="g">Следующий байт GUID.</param>
    /// <param name="h">Следующий байт GUID.</param>
    /// <param name="i">Следующий байт GUID.</param>
    /// <param name="j">Следующий байт GUID.</param>
    /// <param name="k">Следующий байт GUID.</param>
    [__DynamicallyInvokable]
    public Guid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
    {
      this._a = a;
      this._b = b;
      this._c = c;
      this._d = d;
      this._e = e;
      this._f = f;
      this._g = g;
      this._h = h;
      this._i = i;
      this._j = j;
      this._k = k;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Guid" /> структуры с помощью значения, представленного указанной строки.
    /// </summary>
    /// <param name="g">
    ///   Строка, содержащая GUID в одном из указанных ниже форматов (здесь "d" обозначает шестнадцатеричную цифру, регистр которой игнорируется).
    /// 
    ///   Непрерывная последовательность из 32 цифр:
    /// 
    ///   dddddddddddddddddddddddddddddddd
    /// 
    ///   -или-
    /// 
    ///   Группы из 8, 4, 4, 4 и 12 цифр с дефисами между группами.
    ///    GUID целиком может быть заключен в фигурные или круглые скобки:
    /// 
    ///   dddddddd-dddd-dddd-dddd-dddddddddddd
    /// 
    ///   -или-
    /// 
    ///   {dddddddd-dddd-dddd-dddd-dddddddddddd}
    /// 
    ///   -или-
    /// 
    ///   (dddddddd-dddd-dddd-dddd-dddddddddddd)
    /// 
    ///   -или-
    /// 
    ///   Группы из 8, 4 и 4 цифр и подмножество восьми групп по 2 цифры; все группы снабжены префиксом "0x" или "0X" и разделены запятыми.
    ///    Весь GUID, так же как и подмножество, заключается в фигурные скобки:
    /// 
    ///   {0xdddddddd, 0xdddd, 0xdddd,{0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd}}
    /// 
    ///   Все фигурные скобки, запятые и префиксы "0x" необходимы.
    ///    Все внутренние пробелы игнорируются.
    ///    Все нули в начале группы игнорируются.
    /// 
    ///   Цифры, указанные в группе, определяют наибольшее количество значащих цифр в данной группе.
    ///    Можно задавать цифры от 1 до количества, указанного для группы.
    ///    Предполагается, что задаваемые цифры являются младшими цифрами группы.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="g" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Формат <paramref name="g" /> является недопустимым.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Формат <paramref name="g" /> является недопустимым.
    /// </exception>
    [__DynamicallyInvokable]
    public Guid(string g)
    {
      if (g == null)
        throw new ArgumentNullException(nameof (g));
      this = Guid.Empty;
      Guid.GuidResult result = new Guid.GuidResult();
      result.Init(Guid.GuidParseThrowStyle.All);
      if (!Guid.TryParseGuid(g, Guid.GuidStyles.Any, ref result))
        throw result.GetGuidParseException();
      this = result.parsedGuid;
    }

    /// <summary>
    ///   Преобразует строковое представление идентификатора GUID в его эквивалент <see cref="T:System.Guid" /> структуры.
    /// </summary>
    /// <param name="input">Преобразуемая строка.</param>
    /// <returns>Структура, содержащая анализируемое значение.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="input" />не является распознаваемым форматом.
    /// </exception>
    [__DynamicallyInvokable]
    public static Guid Parse(string input)
    {
      if (input == null)
        throw new ArgumentNullException(nameof (input));
      Guid.GuidResult result = new Guid.GuidResult();
      result.Init(Guid.GuidParseThrowStyle.AllButOverflow);
      if (Guid.TryParseGuid(input, Guid.GuidStyles.Any, ref result))
        return result.parsedGuid;
      throw result.GetGuidParseException();
    }

    /// <summary>
    ///   Преобразует строковое представление идентификатора GUID в его эквивалент <see cref="T:System.Guid" /> структуры.
    /// </summary>
    /// <param name="input">
    ///   Время в формате GUID, которое требуется преобразовать.
    /// </param>
    /// <param name="result">
    ///   Структура, которая будет содержать значение после синтаксического анализа.
    ///    Если метод возвращает <see langword="true" />, <paramref name="result" /> содержит допустимый <see cref="T:System.Guid" />.
    ///    Если метод возвращает <see langword="false" />, <paramref name="result" /> равняется <see cref="F:System.Guid.Empty" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />Если операция анализа прошла успешно; в противном случае <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string input, out Guid result)
    {
      Guid.GuidResult result1 = new Guid.GuidResult();
      result1.Init(Guid.GuidParseThrowStyle.None);
      if (Guid.TryParseGuid(input, Guid.GuidStyles.Any, ref result1))
      {
        result = result1.parsedGuid;
        return true;
      }
      result = Guid.Empty;
      return false;
    }

    /// <summary>
    ///   Преобразует строковое представление идентификатора GUID в его эквивалент <see cref="T:System.Guid" /> структуры при условии, что строка находится в указанном формате.
    /// </summary>
    /// <param name="input">
    ///   Время в формате GUID, которое требуется преобразовать.
    /// </param>
    /// <param name="format">
    ///   Один из следующих описателей, указывающих точный формат, используемый при интерпретации <paramref name="input" />: «N», «D», «B», «P» или «X».
    /// </param>
    /// <returns>Структура, содержащая анализируемое значение.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="input" /> или <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="input" />не находится в формате, заданном параметром <paramref name="format" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Guid ParseExact(string input, string format)
    {
      if (input == null)
        throw new ArgumentNullException(nameof (input));
      if (format == null)
        throw new ArgumentNullException(nameof (format));
      if (format.Length != 1)
        throw new FormatException(Environment.GetResourceString("Format_InvalidGuidFormatSpecification"));
      Guid.GuidStyles flags;
      switch (format[0])
      {
        case 'B':
        case 'b':
          flags = Guid.GuidStyles.BraceFormat;
          break;
        case 'D':
        case 'd':
          flags = Guid.GuidStyles.RequireDashes;
          break;
        case 'N':
        case 'n':
          flags = Guid.GuidStyles.None;
          break;
        case 'P':
        case 'p':
          flags = Guid.GuidStyles.ParenthesisFormat;
          break;
        case 'X':
        case 'x':
          flags = Guid.GuidStyles.HexFormat;
          break;
        default:
          throw new FormatException(Environment.GetResourceString("Format_InvalidGuidFormatSpecification"));
      }
      Guid.GuidResult result = new Guid.GuidResult();
      result.Init(Guid.GuidParseThrowStyle.AllButOverflow);
      if (Guid.TryParseGuid(input, flags, ref result))
        return result.parsedGuid;
      throw result.GetGuidParseException();
    }

    /// <summary>
    ///   Преобразует строковое представление идентификатора GUID в его эквивалент <see cref="T:System.Guid" /> структуры при условии, что строка находится в указанном формате.
    /// </summary>
    /// <param name="input">
    ///   Время в формате GUID, которое требуется преобразовать.
    /// </param>
    /// <param name="format">
    ///   Один из следующих описателей, указывающих точный формат, используемый при интерпретации <paramref name="input" />: «N», «D», «B», «P» или «X».
    /// </param>
    /// <param name="result">
    ///   Структура, которая будет содержать значение после синтаксического анализа.
    ///    Если метод возвращает <see langword="true" />, <paramref name="result" /> содержит допустимый <see cref="T:System.Guid" />.
    ///    Если метод возвращает <see langword="false" />, <paramref name="result" /> равняется <see cref="F:System.Guid.Empty" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />Если операция анализа прошла успешно; в противном случае <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParseExact(string input, string format, out Guid result)
    {
      if (format == null || format.Length != 1)
      {
        result = Guid.Empty;
        return false;
      }
      Guid.GuidStyles flags;
      switch (format[0])
      {
        case 'B':
        case 'b':
          flags = Guid.GuidStyles.BraceFormat;
          break;
        case 'D':
        case 'd':
          flags = Guid.GuidStyles.RequireDashes;
          break;
        case 'N':
        case 'n':
          flags = Guid.GuidStyles.None;
          break;
        case 'P':
        case 'p':
          flags = Guid.GuidStyles.ParenthesisFormat;
          break;
        case 'X':
        case 'x':
          flags = Guid.GuidStyles.HexFormat;
          break;
        default:
          result = Guid.Empty;
          return false;
      }
      Guid.GuidResult result1 = new Guid.GuidResult();
      result1.Init(Guid.GuidParseThrowStyle.None);
      if (Guid.TryParseGuid(input, flags, ref result1))
      {
        result = result1.parsedGuid;
        return true;
      }
      result = Guid.Empty;
      return false;
    }

    private static bool TryParseGuid(string g, Guid.GuidStyles flags, ref Guid.GuidResult result)
    {
      if (g == null)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
        return false;
      }
      string guidString = g.Trim();
      if (guidString.Length == 0)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
        return false;
      }
      bool flag1 = guidString.IndexOf('-', 0) >= 0;
      if (flag1)
      {
        if ((flags & (Guid.GuidStyles.AllowDashes | Guid.GuidStyles.RequireDashes)) == Guid.GuidStyles.None)
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
          return false;
        }
      }
      else if ((flags & Guid.GuidStyles.RequireDashes) != Guid.GuidStyles.None)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
        return false;
      }
      bool flag2 = guidString.IndexOf('{', 0) >= 0;
      if (flag2)
      {
        if ((flags & (Guid.GuidStyles.AllowBraces | Guid.GuidStyles.RequireBraces)) == Guid.GuidStyles.None)
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
          return false;
        }
      }
      else if ((flags & Guid.GuidStyles.RequireBraces) != Guid.GuidStyles.None)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
        return false;
      }
      if (guidString.IndexOf('(', 0) >= 0)
      {
        if ((flags & (Guid.GuidStyles.AllowParenthesis | Guid.GuidStyles.RequireParenthesis)) == Guid.GuidStyles.None)
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
          return false;
        }
      }
      else if ((flags & Guid.GuidStyles.RequireParenthesis) != Guid.GuidStyles.None)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidUnrecognized");
        return false;
      }
      try
      {
        if (flag1)
          return Guid.TryParseGuidWithDashes(guidString, ref result);
        if (flag2)
          return Guid.TryParseGuidWithHexPrefix(guidString, ref result);
        return Guid.TryParseGuidWithNoStyle(guidString, ref result);
      }
      catch (IndexOutOfRangeException ex)
      {
        result.SetFailure(Guid.ParseFailureKind.FormatWithInnerException, "Format_GuidUnrecognized", (object) null, (string) null, (Exception) ex);
        return false;
      }
      catch (ArgumentException ex)
      {
        result.SetFailure(Guid.ParseFailureKind.FormatWithInnerException, "Format_GuidUnrecognized", (object) null, (string) null, (Exception) ex);
        return false;
      }
    }

    private static bool TryParseGuidWithHexPrefix(string guidString, ref Guid.GuidResult result)
    {
      guidString = Guid.EatAllWhitespace(guidString);
      if (string.IsNullOrEmpty(guidString) || guidString[0] != '{')
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidBrace");
        return false;
      }
      if (!Guid.IsHexPrefix(guidString, 1))
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidHexPrefix", (object) "{0xdddddddd, etc}");
        return false;
      }
      int startIndex1 = 3;
      int length1 = guidString.IndexOf(',', startIndex1) - startIndex1;
      if (length1 <= 0)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidComma");
        return false;
      }
      if (!Guid.StringToInt(guidString.Substring(startIndex1, length1), -1, 4096, out result.parsedGuid._a, ref result))
        return false;
      if (!Guid.IsHexPrefix(guidString, startIndex1 + length1 + 1))
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidHexPrefix", (object) "{0xdddddddd, 0xdddd, etc}");
        return false;
      }
      int startIndex2 = startIndex1 + length1 + 3;
      int length2 = guidString.IndexOf(',', startIndex2) - startIndex2;
      if (length2 <= 0)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidComma");
        return false;
      }
      if (!Guid.StringToShort(guidString.Substring(startIndex2, length2), -1, 4096, out result.parsedGuid._b, ref result))
        return false;
      if (!Guid.IsHexPrefix(guidString, startIndex2 + length2 + 1))
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidHexPrefix", (object) "{0xdddddddd, 0xdddd, 0xdddd, etc}");
        return false;
      }
      int startIndex3 = startIndex2 + length2 + 3;
      int length3 = guidString.IndexOf(',', startIndex3) - startIndex3;
      if (length3 <= 0)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidComma");
        return false;
      }
      if (!Guid.StringToShort(guidString.Substring(startIndex3, length3), -1, 4096, out result.parsedGuid._c, ref result))
        return false;
      if (guidString.Length <= startIndex3 + length3 + 1 || guidString[startIndex3 + length3 + 1] != '{')
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidBrace");
        return false;
      }
      int length4 = length3 + 1;
      byte[] numArray = new byte[8];
      for (int index = 0; index < 8; ++index)
      {
        if (!Guid.IsHexPrefix(guidString, startIndex3 + length4 + 1))
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidHexPrefix", (object) "{... { ... 0xdd, ...}}");
          return false;
        }
        startIndex3 = startIndex3 + length4 + 3;
        if (index < 7)
        {
          length4 = guidString.IndexOf(',', startIndex3) - startIndex3;
          if (length4 <= 0)
          {
            result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidComma");
            return false;
          }
        }
        else
        {
          length4 = guidString.IndexOf('}', startIndex3) - startIndex3;
          if (length4 <= 0)
          {
            result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidBraceAfterLastNumber");
            return false;
          }
        }
        uint int32 = (uint) Convert.ToInt32(guidString.Substring(startIndex3, length4), 16);
        if (int32 > (uint) byte.MaxValue)
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Overflow_Byte");
          return false;
        }
        numArray[index] = (byte) int32;
      }
      result.parsedGuid._d = numArray[0];
      result.parsedGuid._e = numArray[1];
      result.parsedGuid._f = numArray[2];
      result.parsedGuid._g = numArray[3];
      result.parsedGuid._h = numArray[4];
      result.parsedGuid._i = numArray[5];
      result.parsedGuid._j = numArray[6];
      result.parsedGuid._k = numArray[7];
      if (startIndex3 + length4 + 1 >= guidString.Length || guidString[startIndex3 + length4 + 1] != '}')
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidEndBrace");
        return false;
      }
      if (startIndex3 + length4 + 1 == guidString.Length - 1)
        return true;
      result.SetFailure(Guid.ParseFailureKind.Format, "Format_ExtraJunkAtEnd");
      return false;
    }

    private static bool TryParseGuidWithNoStyle(string guidString, ref Guid.GuidResult result)
    {
      int startIndex1 = 0;
      if (guidString.Length != 32)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
        return false;
      }
      for (int index = 0; index < guidString.Length; ++index)
      {
        char c = guidString[index];
        if (c < '0' || c > '9')
        {
          char upper = char.ToUpper(c, CultureInfo.InvariantCulture);
          if (upper < 'A' || upper > 'F')
          {
            result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvalidChar");
            return false;
          }
        }
      }
      if (!Guid.StringToInt(guidString.Substring(startIndex1, 8), -1, 4096, out result.parsedGuid._a, ref result))
        return false;
      int startIndex2 = startIndex1 + 8;
      if (!Guid.StringToShort(guidString.Substring(startIndex2, 4), -1, 4096, out result.parsedGuid._b, ref result))
        return false;
      int startIndex3 = startIndex2 + 4;
      if (!Guid.StringToShort(guidString.Substring(startIndex3, 4), -1, 4096, out result.parsedGuid._c, ref result))
        return false;
      int startIndex4 = startIndex3 + 4;
      int result1;
      if (!Guid.StringToInt(guidString.Substring(startIndex4, 4), -1, 4096, out result1, ref result))
        return false;
      int flags = startIndex4 + 4;
      int parsePos = flags;
      long result2;
      if (!Guid.StringToLong(guidString, ref parsePos, flags, out result2, ref result))
        return false;
      if (parsePos - flags != 12)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
        return false;
      }
      result.parsedGuid._d = (byte) (result1 >> 8);
      result.parsedGuid._e = (byte) result1;
      int num1 = (int) (result2 >> 32);
      result.parsedGuid._f = (byte) (num1 >> 8);
      result.parsedGuid._g = (byte) num1;
      int num2 = (int) result2;
      result.parsedGuid._h = (byte) (num2 >> 24);
      result.parsedGuid._i = (byte) (num2 >> 16);
      result.parsedGuid._j = (byte) (num2 >> 8);
      result.parsedGuid._k = (byte) num2;
      return true;
    }

    private static bool TryParseGuidWithDashes(string guidString, ref Guid.GuidResult result)
    {
      int num1 = 0;
      if (guidString[0] == '{')
      {
        if (guidString.Length != 38 || guidString[37] != '}')
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
          return false;
        }
        num1 = 1;
      }
      else if (guidString[0] == '(')
      {
        if (guidString.Length != 38 || guidString[37] != ')')
        {
          result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
          return false;
        }
        num1 = 1;
      }
      else if (guidString.Length != 36)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
        return false;
      }
      if (guidString[8 + num1] != '-' || guidString[13 + num1] != '-' || (guidString[18 + num1] != '-' || guidString[23 + num1] != '-'))
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidDashes");
        return false;
      }
      int parsePos = num1;
      int result1;
      if (!Guid.StringToInt(guidString, ref parsePos, 8, 8192, out result1, ref result))
        return false;
      result.parsedGuid._a = result1;
      ++parsePos;
      if (!Guid.StringToInt(guidString, ref parsePos, 4, 8192, out result1, ref result))
        return false;
      result.parsedGuid._b = (short) result1;
      ++parsePos;
      if (!Guid.StringToInt(guidString, ref parsePos, 4, 8192, out result1, ref result))
        return false;
      result.parsedGuid._c = (short) result1;
      ++parsePos;
      if (!Guid.StringToInt(guidString, ref parsePos, 4, 8192, out result1, ref result))
        return false;
      ++parsePos;
      int num2 = parsePos;
      long result2;
      if (!Guid.StringToLong(guidString, ref parsePos, 8192, out result2, ref result))
        return false;
      if (parsePos - num2 != 12)
      {
        result.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvLen");
        return false;
      }
      result.parsedGuid._d = (byte) (result1 >> 8);
      result.parsedGuid._e = (byte) result1;
      result1 = (int) (result2 >> 32);
      result.parsedGuid._f = (byte) (result1 >> 8);
      result.parsedGuid._g = (byte) result1;
      result1 = (int) result2;
      result.parsedGuid._h = (byte) (result1 >> 24);
      result.parsedGuid._i = (byte) (result1 >> 16);
      result.parsedGuid._j = (byte) (result1 >> 8);
      result.parsedGuid._k = (byte) result1;
      return true;
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToShort(string str, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
    {
      return Guid.StringToShort(str, (int*) null, requiredLength, flags, out result, ref parseResult);
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToShort(string str, ref int parsePos, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
    {
      fixed (int* parsePos1 = &parsePos)
        return Guid.StringToShort(str, parsePos1, requiredLength, flags, out result, ref parseResult);
    }

    [SecurityCritical]
    private static unsafe bool StringToShort(string str, int* parsePos, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
    {
      result = (short) 0;
      int result1;
      bool flag = Guid.StringToInt(str, parsePos, requiredLength, flags, out result1, ref parseResult);
      result = (short) result1;
      return flag;
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToInt(string str, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
    {
      return Guid.StringToInt(str, (int*) null, requiredLength, flags, out result, ref parseResult);
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToInt(string str, ref int parsePos, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
    {
      fixed (int* parsePos1 = &parsePos)
        return Guid.StringToInt(str, parsePos1, requiredLength, flags, out result, ref parseResult);
    }

    [SecurityCritical]
    private static unsafe bool StringToInt(string str, int* parsePos, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
    {
      result = 0;
      int num = (IntPtr) parsePos == IntPtr.Zero ? 0 : *parsePos;
      try
      {
        result = ParseNumbers.StringToInt(str, 16, flags, parsePos);
      }
      catch (OverflowException ex)
      {
        if (parseResult.throwStyle == Guid.GuidParseThrowStyle.All)
        {
          throw;
        }
        else
        {
          if (parseResult.throwStyle == Guid.GuidParseThrowStyle.AllButOverflow)
            throw new FormatException(Environment.GetResourceString("Format_GuidUnrecognized"), (Exception) ex);
          parseResult.SetFailure((Exception) ex);
          return false;
        }
      }
      catch (Exception ex)
      {
        if (parseResult.throwStyle == Guid.GuidParseThrowStyle.None)
        {
          parseResult.SetFailure(ex);
          return false;
        }
        throw;
      }
      if (requiredLength == -1 || (IntPtr) parsePos == IntPtr.Zero || *parsePos - num == requiredLength)
        return true;
      parseResult.SetFailure(Guid.ParseFailureKind.Format, "Format_GuidInvalidChar");
      return false;
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToLong(string str, int flags, out long result, ref Guid.GuidResult parseResult)
    {
      return Guid.StringToLong(str, (int*) null, flags, out result, ref parseResult);
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToLong(string str, ref int parsePos, int flags, out long result, ref Guid.GuidResult parseResult)
    {
      fixed (int* parsePos1 = &parsePos)
        return Guid.StringToLong(str, parsePos1, flags, out result, ref parseResult);
    }

    [SecuritySafeCritical]
    private static unsafe bool StringToLong(string str, int* parsePos, int flags, out long result, ref Guid.GuidResult parseResult)
    {
      result = 0L;
      try
      {
        result = ParseNumbers.StringToLong(str, 16, flags, parsePos);
      }
      catch (OverflowException ex)
      {
        if (parseResult.throwStyle == Guid.GuidParseThrowStyle.All)
        {
          throw;
        }
        else
        {
          if (parseResult.throwStyle == Guid.GuidParseThrowStyle.AllButOverflow)
            throw new FormatException(Environment.GetResourceString("Format_GuidUnrecognized"), (Exception) ex);
          parseResult.SetFailure((Exception) ex);
          return false;
        }
      }
      catch (Exception ex)
      {
        if (parseResult.throwStyle == Guid.GuidParseThrowStyle.None)
        {
          parseResult.SetFailure(ex);
          return false;
        }
        throw;
      }
      return true;
    }

    private static string EatAllWhitespace(string str)
    {
      int length = 0;
      char[] chArray = new char[str.Length];
      for (int index = 0; index < str.Length; ++index)
      {
        char c = str[index];
        if (!char.IsWhiteSpace(c))
          chArray[length++] = c;
      }
      return new string(chArray, 0, length);
    }

    private static bool IsHexPrefix(string str, int i)
    {
      return str.Length > i + 1 && str[i] == '0' && char.ToLower(str[i + 1], CultureInfo.InvariantCulture) == 'x';
    }

    /// <summary>
    ///   Возвращает массив байтов из 16 элементов, содержащий значение данного экземпляра.
    /// </summary>
    /// <returns>Массив байтов из 16 элементов.</returns>
    [__DynamicallyInvokable]
    public byte[] ToByteArray()
    {
      return new byte[16]
      {
        (byte) this._a,
        (byte) (this._a >> 8),
        (byte) (this._a >> 16),
        (byte) (this._a >> 24),
        (byte) this._b,
        (byte) ((uint) this._b >> 8),
        (byte) this._c,
        (byte) ((uint) this._c >> 8),
        this._d,
        this._e,
        this._f,
        this._g,
        this._h,
        this._i,
        this._j,
        this._k
      };
    }

    /// <summary>
    ///   Возвращает строковое представление значения этого экземпляра в формате реестра.
    /// </summary>
    /// <returns>
    ///   Значение этого аргумента <see cref="T:System.Guid" />отформатированную с помощью описателя формата «D», следующим образом:
    /// 
    ///   xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
    /// 
    ///   где значение GUID представлено в виде последовательности шестнадцатеричных цифр в нижнем регистре, сгруппированных по 8, 4, 4, 4 и 12 цифр и разделенных дефисами.
    ///    В данном случае возвращаемое значение таково: "382c74c3-721d-4f34-80e5-57657b6cbc27".
    ///    Чтобы преобразовать шестнадцатеричными цифрами от до f в верхний регистр, вызовите <see cref="M:System.String.ToUpper" /> метод возвращаемой строки.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.ToString("D", (IFormatProvider) null);
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>Хэш-код данного экземпляра.</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this._a ^ ((int) this._b << 16 | (int) (ushort) this._c) ^ ((int) this._f << 24 | (int) this._k);
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли экземпляр указанному объекту.
    /// </summary>
    /// <param name="o">Объект, сравниваемый с данным экземпляром.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="o" /> является <see cref="T:System.Guid" /> имеет то же значение, что и данный экземпляр; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object o)
    {
      if (o == null || !(o is Guid))
        return false;
      Guid guid = (Guid) o;
      return guid._a == this._a && (int) guid._b == (int) this._b && ((int) guid._c == (int) this._c && (int) guid._d == (int) this._d) && ((int) guid._e == (int) this._e && (int) guid._f == (int) this._f && ((int) guid._g == (int) this._g && (int) guid._h == (int) this._h)) && ((int) guid._i == (int) this._i && (int) guid._j == (int) this._j && (int) guid._k == (int) this._k);
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, представляют ли этот экземпляр и заданный объект <see cref="T:System.Guid" /> одно и то же значение.
    /// </summary>
    /// <param name="g">Объект, сравниваемый с этим экземпляром.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="g" /> равно данному экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(Guid g)
    {
      return g._a == this._a && (int) g._b == (int) this._b && ((int) g._c == (int) this._c && (int) g._d == (int) this._d) && ((int) g._e == (int) this._e && (int) g._f == (int) this._f && ((int) g._g == (int) this._g && (int) g._h == (int) this._h)) && ((int) g._i == (int) this._i && (int) g._j == (int) this._j && (int) g._k == (int) this._k);
    }

    private int GetResult(uint me, uint them)
    {
      return me < them ? -1 : 1;
    }

    /// <summary>
    ///   Сравнивает этот экземпляр с заданным объектом и возвращает значение, указывающее, как соотносятся значения этих объектов.
    /// </summary>
    /// <param name="value">
    ///   Объект для сравнения или значение <see langword="null" />.
    /// </param>
    /// <returns>
    /// Знаковое число, представляющее относительные значения этого экземпляра и параметра <paramref name="value" />.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Описание
    /// 
    ///         Отрицательное целое число
    /// 
    ///         Этот экземпляр меньше параметра <paramref name="value" />.
    /// 
    ///         Нуль
    /// 
    ///         Этот экземпляр и параметр <paramref name="value" /> равны.
    /// 
    ///         Положительное целое число
    /// 
    ///         Этот экземпляр больше параметра <paramref name="value" />, или <paramref name="value" /> — <see langword="null" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="value" /> не является объектом <see cref="T:System.Guid" />.
    /// </exception>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is Guid))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeGuid"));
      Guid guid = (Guid) value;
      if (guid._a != this._a)
        return this.GetResult((uint) this._a, (uint) guid._a);
      if ((int) guid._b != (int) this._b)
        return this.GetResult((uint) this._b, (uint) guid._b);
      if ((int) guid._c != (int) this._c)
        return this.GetResult((uint) this._c, (uint) guid._c);
      if ((int) guid._d != (int) this._d)
        return this.GetResult((uint) this._d, (uint) guid._d);
      if ((int) guid._e != (int) this._e)
        return this.GetResult((uint) this._e, (uint) guid._e);
      if ((int) guid._f != (int) this._f)
        return this.GetResult((uint) this._f, (uint) guid._f);
      if ((int) guid._g != (int) this._g)
        return this.GetResult((uint) this._g, (uint) guid._g);
      if ((int) guid._h != (int) this._h)
        return this.GetResult((uint) this._h, (uint) guid._h);
      if ((int) guid._i != (int) this._i)
        return this.GetResult((uint) this._i, (uint) guid._i);
      if ((int) guid._j != (int) this._j)
        return this.GetResult((uint) this._j, (uint) guid._j);
      if ((int) guid._k != (int) this._k)
        return this.GetResult((uint) this._k, (uint) guid._k);
      return 0;
    }

    /// <summary>
    ///   Сравнивает этот экземпляр с заданным объектом <see cref="T:System.Guid" /> и возвращает значение, указывающее, как соотносятся значения этих объектов.
    /// </summary>
    /// <param name="value">
    ///   Объект, сравниваемый с этим экземпляром.
    /// </param>
    /// <returns>
    /// Знаковое число, представляющее относительные значения этого экземпляра и параметра <paramref name="value" />.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Описание
    /// 
    ///         Отрицательное целое число
    /// 
    ///         Этот экземпляр меньше параметра <paramref name="value" />.
    /// 
    ///         Нуль
    /// 
    ///         Этот экземпляр и параметр <paramref name="value" /> равны.
    /// 
    ///         Положительное целое число
    /// 
    ///         Этот экземпляр больше параметра <paramref name="value" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public int CompareTo(Guid value)
    {
      if (value._a != this._a)
        return this.GetResult((uint) this._a, (uint) value._a);
      if ((int) value._b != (int) this._b)
        return this.GetResult((uint) this._b, (uint) value._b);
      if ((int) value._c != (int) this._c)
        return this.GetResult((uint) this._c, (uint) value._c);
      if ((int) value._d != (int) this._d)
        return this.GetResult((uint) this._d, (uint) value._d);
      if ((int) value._e != (int) this._e)
        return this.GetResult((uint) this._e, (uint) value._e);
      if ((int) value._f != (int) this._f)
        return this.GetResult((uint) this._f, (uint) value._f);
      if ((int) value._g != (int) this._g)
        return this.GetResult((uint) this._g, (uint) value._g);
      if ((int) value._h != (int) this._h)
        return this.GetResult((uint) this._h, (uint) value._h);
      if ((int) value._i != (int) this._i)
        return this.GetResult((uint) this._i, (uint) value._i);
      if ((int) value._j != (int) this._j)
        return this.GetResult((uint) this._j, (uint) value._j);
      if ((int) value._k != (int) this._k)
        return this.GetResult((uint) this._k, (uint) value._k);
      return 0;
    }

    /// <summary>
    ///   Показывает, определен ли значения двух <see cref="T:System.Guid" /> объекты равны.
    /// </summary>
    /// <param name="a">Первый из сравниваемых объектов.</param>
    /// <param name="b">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(Guid a, Guid b)
    {
      return a._a == b._a && (int) a._b == (int) b._b && ((int) a._c == (int) b._c && (int) a._d == (int) b._d) && ((int) a._e == (int) b._e && (int) a._f == (int) b._f && ((int) a._g == (int) b._g && (int) a._h == (int) b._h)) && ((int) a._i == (int) b._i && (int) a._j == (int) b._j && (int) a._k == (int) b._k);
    }

    /// <summary>
    ///   Показывает, определен ли значения двух <see cref="T:System.Guid" /> объекты не равны.
    /// </summary>
    /// <param name="a">Первый из сравниваемых объектов.</param>
    /// <param name="b">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="a" /> и <paramref name="b" /> не равны друг другу; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(Guid a, Guid b)
    {
      return !(a == b);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.Guid" />.
    /// </summary>
    /// <returns>Новый объект GUID.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Guid NewGuid()
    {
      Guid guid;
      Marshal.ThrowExceptionForHR(Win32Native.CoCreateGuid(out guid), new IntPtr(-1));
      return guid;
    }

    /// <summary>
    ///   Возвращает строковое представление значения данного объекта <see cref="T:System.Guid" /> экземпляр в соответствии с заданным описателем формата.
    /// </summary>
    /// <param name="format">
    ///   Единственный описатель формата, указывающий, как следует форматировать значение данного <see cref="T:System.Guid" />.
    ///    Параметр <paramref name="format" /> может принимать значения N, D, B, P или X.
    ///    Если параметр <paramref name="format" /> имеет значение <see langword="null" /> или равен пустой строке (""), используется значение D.
    /// </param>
    /// <returns>
    ///   Значение данного <see cref="T:System.Guid" />, представленное в виде последовательности шестнадцатеричных цифр в нижнем регистре в указанном формате.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   Значением <paramref name="format" /> не является <see langword="null" />, пустая строка (""), N, D, B, P или X.
    /// </exception>
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return this.ToString(format, (IFormatProvider) null);
    }

    private static char HexToChar(int a)
    {
      a &= 15;
      return a > 9 ? (char) (a - 10 + 97) : (char) (a + 48);
    }

    [SecurityCritical]
    private static unsafe int HexsToChars(char* guidChars, int offset, int a, int b)
    {
      return Guid.HexsToChars(guidChars, offset, a, b, false);
    }

    [SecurityCritical]
    private static unsafe int HexsToChars(char* guidChars, int offset, int a, int b, bool hex)
    {
      if (hex)
      {
        guidChars[offset++] = '0';
        guidChars[offset++] = 'x';
      }
      guidChars[offset++] = Guid.HexToChar(a >> 4);
      guidChars[offset++] = Guid.HexToChar(a);
      if (hex)
      {
        guidChars[offset++] = ',';
        guidChars[offset++] = '0';
        guidChars[offset++] = 'x';
      }
      guidChars[offset++] = Guid.HexToChar(b >> 4);
      guidChars[offset++] = Guid.HexToChar(b);
      return offset;
    }

    /// <summary>
    ///   Возвращает строковое представление значения этого экземпляра класса <see cref="T:System.Guid" /> в соответствии с заданным описателем формата и сведениями об особенностях форматирования, связанных с языком и региональными параметрами.
    /// </summary>
    /// <param name="format">
    ///   Единственный описатель формата, указывающий, как следует форматировать значение данного <see cref="T:System.Guid" />.
    ///    Параметр <paramref name="format" /> может принимать значения N, D, B, P или X.
    ///    Если параметр <paramref name="format" /> имеет значение <see langword="null" /> или равен пустой строке (""), используется значение D.
    /// </param>
    /// <param name="provider">
    ///   (Зарезервирован.) Объект, предоставляющий сведения о форматировании, связанные с определенным языком и региональными параметрами.
    /// </param>
    /// <returns>
    ///   Значение данного <see cref="T:System.Guid" />, представленное в виде последовательности шестнадцатеричных цифр в нижнем регистре в указанном формате.
    /// </returns>
    /// <exception cref="T:System.FormatException">
    ///   Значением <paramref name="format" /> не является <see langword="null" />, пустая строка (""), N, D, B, P или X.
    /// </exception>
    [SecuritySafeCritical]
    public unsafe string ToString(string format, IFormatProvider provider)
    {
      if (format == null || format.Length == 0)
        format = "D";
      int offset1 = 0;
      bool flag1 = true;
      bool flag2 = false;
      if (format.Length != 1)
        throw new FormatException(Environment.GetResourceString("Format_InvalidGuidFormatSpecification"));
      string str1;
      switch (format[0])
      {
        case 'B':
        case 'b':
          str1 = string.FastAllocateString(38);
          string str2 = str1;
          char* chPtr1 = (char*) str2;
          if ((IntPtr) chPtr1 != IntPtr.Zero)
            chPtr1 += RuntimeHelpers.OffsetToStringData;
          chPtr1[offset1++] = '{';
          chPtr1[37] = '}';
          str2 = (string) null;
          break;
        case 'D':
        case 'd':
          str1 = string.FastAllocateString(36);
          break;
        case 'N':
        case 'n':
          str1 = string.FastAllocateString(32);
          flag1 = false;
          break;
        case 'P':
        case 'p':
          str1 = string.FastAllocateString(38);
          string str3 = str1;
          char* chPtr2 = (char*) str3;
          if ((IntPtr) chPtr2 != IntPtr.Zero)
            chPtr2 += RuntimeHelpers.OffsetToStringData;
          chPtr2[offset1++] = '(';
          chPtr2[37] = ')';
          str3 = (string) null;
          break;
        case 'X':
        case 'x':
          str1 = string.FastAllocateString(68);
          string str4 = str1;
          char* chPtr3 = (char*) str4;
          if ((IntPtr) chPtr3 != IntPtr.Zero)
            chPtr3 += RuntimeHelpers.OffsetToStringData;
          chPtr3[offset1++] = '{';
          chPtr3[67] = '}';
          str4 = (string) null;
          flag1 = false;
          flag2 = true;
          break;
        default:
          throw new FormatException(Environment.GetResourceString("Format_InvalidGuidFormatSpecification"));
      }
      string str5 = str1;
      char* guidChars = (char*) str5;
      if ((IntPtr) guidChars != IntPtr.Zero)
        guidChars += RuntimeHelpers.OffsetToStringData;
      int num1;
      if (flag2)
      {
        char* chPtr4 = guidChars;
        int num2 = offset1;
        int num3 = num2 + 1;
        IntPtr num4 = (IntPtr) num2 * 2;
        *(short*) ((IntPtr) chPtr4 + num4) = (short) 48;
        char* chPtr5 = guidChars;
        int num5 = num3;
        int offset2 = num5 + 1;
        IntPtr num6 = (IntPtr) num5 * 2;
        *(short*) ((IntPtr) chPtr5 + num6) = (short) 120;
        int chars1 = Guid.HexsToChars(guidChars, offset2, this._a >> 24, this._a >> 16);
        int chars2 = Guid.HexsToChars(guidChars, chars1, this._a >> 8, this._a);
        char* chPtr6 = guidChars;
        int num7 = chars2;
        int num8 = num7 + 1;
        IntPtr num9 = (IntPtr) num7 * 2;
        *(short*) ((IntPtr) chPtr6 + num9) = (short) 44;
        char* chPtr7 = guidChars;
        int num10 = num8;
        int num11 = num10 + 1;
        IntPtr num12 = (IntPtr) num10 * 2;
        *(short*) ((IntPtr) chPtr7 + num12) = (short) 48;
        char* chPtr8 = guidChars;
        int num13 = num11;
        int offset3 = num13 + 1;
        IntPtr num14 = (IntPtr) num13 * 2;
        *(short*) ((IntPtr) chPtr8 + num14) = (short) 120;
        int chars3 = Guid.HexsToChars(guidChars, offset3, (int) this._b >> 8, (int) this._b);
        char* chPtr9 = guidChars;
        int num15 = chars3;
        int num16 = num15 + 1;
        IntPtr num17 = (IntPtr) num15 * 2;
        *(short*) ((IntPtr) chPtr9 + num17) = (short) 44;
        char* chPtr10 = guidChars;
        int num18 = num16;
        int num19 = num18 + 1;
        IntPtr num20 = (IntPtr) num18 * 2;
        *(short*) ((IntPtr) chPtr10 + num20) = (short) 48;
        char* chPtr11 = guidChars;
        int num21 = num19;
        int offset4 = num21 + 1;
        IntPtr num22 = (IntPtr) num21 * 2;
        *(short*) ((IntPtr) chPtr11 + num22) = (short) 120;
        int chars4 = Guid.HexsToChars(guidChars, offset4, (int) this._c >> 8, (int) this._c);
        char* chPtr12 = guidChars;
        int num23 = chars4;
        int num24 = num23 + 1;
        IntPtr num25 = (IntPtr) num23 * 2;
        *(short*) ((IntPtr) chPtr12 + num25) = (short) 44;
        char* chPtr13 = guidChars;
        int num26 = num24;
        int offset5 = num26 + 1;
        IntPtr num27 = (IntPtr) num26 * 2;
        *(short*) ((IntPtr) chPtr13 + num27) = (short) 123;
        int chars5 = Guid.HexsToChars(guidChars, offset5, (int) this._d, (int) this._e, true);
        char* chPtr14 = guidChars;
        int num28 = chars5;
        int offset6 = num28 + 1;
        IntPtr num29 = (IntPtr) num28 * 2;
        *(short*) ((IntPtr) chPtr14 + num29) = (short) 44;
        int chars6 = Guid.HexsToChars(guidChars, offset6, (int) this._f, (int) this._g, true);
        char* chPtr15 = guidChars;
        int num30 = chars6;
        int offset7 = num30 + 1;
        IntPtr num31 = (IntPtr) num30 * 2;
        *(short*) ((IntPtr) chPtr15 + num31) = (short) 44;
        int chars7 = Guid.HexsToChars(guidChars, offset7, (int) this._h, (int) this._i, true);
        char* chPtr16 = guidChars;
        int num32 = chars7;
        int offset8 = num32 + 1;
        IntPtr num33 = (IntPtr) num32 * 2;
        *(short*) ((IntPtr) chPtr16 + num33) = (short) 44;
        int chars8 = Guid.HexsToChars(guidChars, offset8, (int) this._j, (int) this._k, true);
        char* chPtr17 = guidChars;
        int num34 = chars8;
        num1 = num34 + 1;
        IntPtr num35 = (IntPtr) num34 * 2;
        *(short*) ((IntPtr) chPtr17 + num35) = (short) 125;
      }
      else
      {
        int chars1 = Guid.HexsToChars(guidChars, offset1, this._a >> 24, this._a >> 16);
        int chars2 = Guid.HexsToChars(guidChars, chars1, this._a >> 8, this._a);
        if (flag1)
          guidChars[chars2++] = '-';
        int chars3 = Guid.HexsToChars(guidChars, chars2, (int) this._b >> 8, (int) this._b);
        if (flag1)
          guidChars[chars3++] = '-';
        int chars4 = Guid.HexsToChars(guidChars, chars3, (int) this._c >> 8, (int) this._c);
        if (flag1)
          guidChars[chars4++] = '-';
        int chars5 = Guid.HexsToChars(guidChars, chars4, (int) this._d, (int) this._e);
        if (flag1)
          guidChars[chars5++] = '-';
        int chars6 = Guid.HexsToChars(guidChars, chars5, (int) this._f, (int) this._g);
        int chars7 = Guid.HexsToChars(guidChars, chars6, (int) this._h, (int) this._i);
        num1 = Guid.HexsToChars(guidChars, chars7, (int) this._j, (int) this._k);
      }
      str5 = (string) null;
      return str1;
    }

    [Flags]
    private enum GuidStyles
    {
      None = 0,
      AllowParenthesis = 1,
      AllowBraces = 2,
      AllowDashes = 4,
      AllowHexPrefix = 8,
      RequireParenthesis = 16, // 0x00000010
      RequireBraces = 32, // 0x00000020
      RequireDashes = 64, // 0x00000040
      RequireHexPrefix = 128, // 0x00000080
      HexFormat = RequireHexPrefix | RequireBraces, // 0x000000A0
      NumberFormat = 0,
      DigitFormat = RequireDashes, // 0x00000040
      BraceFormat = DigitFormat | RequireBraces, // 0x00000060
      ParenthesisFormat = DigitFormat | RequireParenthesis, // 0x00000050
      Any = AllowHexPrefix | AllowDashes | AllowBraces | AllowParenthesis, // 0x0000000F
    }

    private enum GuidParseThrowStyle
    {
      None,
      All,
      AllButOverflow,
    }

    private enum ParseFailureKind
    {
      None,
      ArgumentNull,
      Format,
      FormatWithParameter,
      NativeException,
      FormatWithInnerException,
    }

    private struct GuidResult
    {
      internal Guid parsedGuid;
      internal Guid.GuidParseThrowStyle throwStyle;
      internal Guid.ParseFailureKind m_failure;
      internal string m_failureMessageID;
      internal object m_failureMessageFormatArgument;
      internal string m_failureArgumentName;
      internal Exception m_innerException;

      internal void Init(Guid.GuidParseThrowStyle canThrow)
      {
        this.parsedGuid = Guid.Empty;
        this.throwStyle = canThrow;
      }

      internal void SetFailure(Exception nativeException)
      {
        this.m_failure = Guid.ParseFailureKind.NativeException;
        this.m_innerException = nativeException;
      }

      internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID)
      {
        this.SetFailure(failure, failureMessageID, (object) null, (string) null, (Exception) null);
      }

      internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
      {
        this.SetFailure(failure, failureMessageID, failureMessageFormatArgument, (string) null, (Exception) null);
      }

      internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName, Exception innerException)
      {
        this.m_failure = failure;
        this.m_failureMessageID = failureMessageID;
        this.m_failureMessageFormatArgument = failureMessageFormatArgument;
        this.m_failureArgumentName = failureArgumentName;
        this.m_innerException = innerException;
        if (this.throwStyle != Guid.GuidParseThrowStyle.None)
          throw this.GetGuidParseException();
      }

      internal Exception GetGuidParseException()
      {
        switch (this.m_failure)
        {
          case Guid.ParseFailureKind.ArgumentNull:
            return (Exception) new ArgumentNullException(this.m_failureArgumentName, Environment.GetResourceString(this.m_failureMessageID));
          case Guid.ParseFailureKind.Format:
            return (Exception) new FormatException(Environment.GetResourceString(this.m_failureMessageID));
          case Guid.ParseFailureKind.FormatWithParameter:
            return (Exception) new FormatException(Environment.GetResourceString(this.m_failureMessageID, this.m_failureMessageFormatArgument));
          case Guid.ParseFailureKind.NativeException:
            return this.m_innerException;
          case Guid.ParseFailureKind.FormatWithInnerException:
            return (Exception) new FormatException(Environment.GetResourceString(this.m_failureMessageID), this.m_innerException);
          default:
            return (Exception) new FormatException(Environment.GetResourceString("Format_GuidUnrecognized"));
        }
      }
    }
  }
}
