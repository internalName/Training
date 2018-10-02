// Decompiled with JetBrains decompiler
// Type: System.Globalization.CompareInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
  /// <summary>
  ///   Реализует ряд методов для сравнения строк с учетом языка и региональных параметров.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class CompareInfo : IDeserializationCallback
  {
    private const CompareOptions ValidIndexMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
    private const CompareOptions ValidCompareMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort);
    private const CompareOptions ValidHashCodeOfStringMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
    [OptionalField(VersionAdded = 2)]
    private string m_name;
    [NonSerialized]
    private string m_sortName;
    [NonSerialized]
    private IntPtr m_dataHandle;
    [NonSerialized]
    private IntPtr m_handleOrigin;
    [OptionalField(VersionAdded = 1)]
    private int win32LCID;
    private int culture;
    private const int LINGUISTIC_IGNORECASE = 16;
    private const int NORM_IGNORECASE = 1;
    private const int NORM_IGNOREKANATYPE = 65536;
    private const int LINGUISTIC_IGNOREDIACRITIC = 32;
    private const int NORM_IGNORENONSPACE = 2;
    private const int NORM_IGNORESYMBOLS = 4;
    private const int NORM_IGNOREWIDTH = 131072;
    private const int SORT_STRINGSORT = 4096;
    private const int COMPARE_OPTIONS_ORDINAL = 1073741824;
    internal const int NORM_LINGUISTIC_CASING = 134217728;
    private const int RESERVED_FIND_ASCII_STRING = 536870912;
    private const int SORT_VERSION_WHIDBEY = 4096;
    private const int SORT_VERSION_V4 = 393473;
    [OptionalField(VersionAdded = 3)]
    private SortVersion m_SortVersion;

    internal CompareInfo(CultureInfo culture)
    {
      this.m_name = culture.m_name;
      this.m_sortName = culture.SortName;
      IntPtr handleOrigin;
      this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_sortName, out handleOrigin);
      this.m_handleOrigin = handleOrigin;
    }

    /// <summary>
    ///   Инициализирует новый <see cref="T:System.Globalization.CompareInfo" /> объект, связанный с указанным языком и региональными параметрами, и использующий методы сравнения строк в заданном <see cref="T:System.Reflection.Assembly" />.
    /// </summary>
    /// <param name="culture">
    ///   Целое число, представляющее идентификатор языка и региональных параметров.
    /// </param>
    /// <param name="assembly">
    ///   <see cref="T:System.Reflection.Assembly" /> Содержащий используемые методы сравнения строк для использования.
    /// </param>
    /// <returns>
    ///   Новый <see cref="T:System.Globalization.CompareInfo" /> объекта, связанного с языком и региональными параметрами с заданным идентификатором и использующий методы сравнения строк в текущем <see cref="T:System.Reflection.Assembly" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="assembly" /> имеет недопустимый тип.
    /// </exception>
    public static CompareInfo GetCompareInfo(int culture, Assembly assembly)
    {
      if (assembly == (Assembly) null)
        throw new ArgumentNullException(nameof (assembly));
      if (assembly != typeof (object).Module.Assembly)
        throw new ArgumentException(Environment.GetResourceString("Argument_OnlyMscorlib"));
      return CompareInfo.GetCompareInfo(culture);
    }

    /// <summary>
    ///   Инициализирует новый <see cref="T:System.Globalization.CompareInfo" /> объект, связанный с указанным языком и региональными параметрами, и использующий методы сравнения строк в заданном <see cref="T:System.Reflection.Assembly" />.
    /// </summary>
    /// <param name="name">
    ///   Строка, представляющая имя языка и региональных параметров.
    /// </param>
    /// <param name="assembly">
    ///   <see cref="T:System.Reflection.Assembly" /> Содержащий используемые методы сравнения строк для использования.
    /// </param>
    /// <returns>
    ///   Новый <see cref="T:System.Globalization.CompareInfo" /> объекта, связанного с языком и региональными параметрами с заданным идентификатором и использующий методы сравнения строк в текущем <see cref="T:System.Reflection.Assembly" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="assembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> имеет недопустимое имя культуры.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="assembly" /> имеет недопустимый тип.
    /// </exception>
    public static CompareInfo GetCompareInfo(string name, Assembly assembly)
    {
      if (name == null || assembly == (Assembly) null)
        throw new ArgumentNullException(name == null ? nameof (name) : nameof (assembly));
      if (assembly != typeof (object).Module.Assembly)
        throw new ArgumentException(Environment.GetResourceString("Argument_OnlyMscorlib"));
      return CompareInfo.GetCompareInfo(name);
    }

    /// <summary>
    ///   Инициализирует новый <see cref="T:System.Globalization.CompareInfo" /> объект, связанный с языком и региональными параметрами с указанным идентификатором.
    /// </summary>
    /// <param name="culture">
    ///   Целое число, представляющее идентификатор языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Новый <see cref="T:System.Globalization.CompareInfo" /> объекта, связанного с языком и региональными параметрами с заданным идентификатором и использующий методы сравнения строк в текущем <see cref="T:System.Reflection.Assembly" />.
    /// </returns>
    public static CompareInfo GetCompareInfo(int culture)
    {
      if (CultureData.IsCustomCultureId(culture))
        throw new ArgumentException(Environment.GetResourceString("Argument_CustomCultureCannotBePassedByNumber", (object) nameof (culture)));
      return CultureInfo.GetCultureInfo(culture).CompareInfo;
    }

    /// <summary>
    ///   Инициализирует новый <see cref="T:System.Globalization.CompareInfo" /> объект, связанный с языком и региональными параметрами с заданным именем.
    /// </summary>
    /// <param name="name">
    ///   Строка, представляющая имя языка и региональных параметров.
    /// </param>
    /// <returns>
    ///   Новый <see cref="T:System.Globalization.CompareInfo" /> объекта, связанного с языком и региональными параметрами с заданным идентификатором и использующий методы сравнения строк в текущем <see cref="T:System.Reflection.Assembly" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> имеет недопустимое имя культуры.
    /// </exception>
    [__DynamicallyInvokable]
    public static CompareInfo GetCompareInfo(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      return CultureInfo.GetCultureInfo(name).CompareInfo;
    }

    /// <summary>
    ///   Показывает, подлежит ли указанный знак Юникода сортировке.
    /// </summary>
    /// <param name="ch">Знак Юникода.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="ch" /> поддерживает сортировку; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public static bool IsSortable(char ch)
    {
      return CompareInfo.IsSortable(ch.ToString());
    }

    /// <summary>
    ///   Показывает, подлежит ли указанная строка Юникода сортировке.
    /// </summary>
    /// <param name="text">
    ///   Строка длиной от нуля и более знаков Юникода.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="str" /> параметр не является пустой строкой («») и все символы Юникода в <paramref name="str" /> поддерживают сортировку; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public static bool IsSortable(string text)
    {
      if (text == null)
        throw new ArgumentNullException(nameof (text));
      if (text.Length == 0)
        return false;
      CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
      return CompareInfo.InternalIsSortable(compareInfo.m_dataHandle, compareInfo.m_handleOrigin, compareInfo.m_sortName, text, text.Length);
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.m_name = (string) null;
    }

    private void OnDeserialized()
    {
      CultureInfo cultureInfo;
      if (this.m_name == null)
      {
        cultureInfo = CultureInfo.GetCultureInfo(this.culture);
        this.m_name = cultureInfo.m_name;
      }
      else
        cultureInfo = CultureInfo.GetCultureInfo(this.m_name);
      this.m_sortName = cultureInfo.SortName;
      IntPtr handleOrigin;
      this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_sortName, out handleOrigin);
      this.m_handleOrigin = handleOrigin;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this.OnDeserialized();
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.culture = CultureInfo.GetCultureInfo(this.Name).LCID;
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
      this.OnDeserialized();
    }

    /// <summary>
    ///   Возвращает имя языка и региональных параметров, используемые для операций сортировки <see cref="T:System.Globalization.CompareInfo" /> объекта.
    /// </summary>
    /// <returns>Имя языка и региональных параметров.</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual string Name
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_name == "zh-CHT" || this.m_name == "zh-CHS")
          return this.m_name;
        return this.m_sortName;
      }
    }

    internal static int GetNativeCompareFlags(CompareOptions options)
    {
      int num = 134217728;
      if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
        num |= 1;
      if ((options & CompareOptions.IgnoreKanaType) != CompareOptions.None)
        num |= 65536;
      if ((options & CompareOptions.IgnoreNonSpace) != CompareOptions.None)
        num |= 2;
      if ((options & CompareOptions.IgnoreSymbols) != CompareOptions.None)
        num |= 4;
      if ((options & CompareOptions.IgnoreWidth) != CompareOptions.None)
        num |= 131072;
      if ((options & CompareOptions.StringSort) != CompareOptions.None)
        num |= 4096;
      if (options == CompareOptions.Ordinal)
        num = 1073741824;
      return num;
    }

    /// <summary>Сравнивает две строки.</summary>
    /// <param name="string1">Первая сравниваемая строка.</param>
    /// <param name="string2">Вторая сравниваемая строка.</param>
    /// <returns>
    /// 32-разрядное целое число со знаком, выражающее лексическое соотношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         нуль
    /// 
    ///         Эти две строки совпадают.
    /// 
    ///         меньше нуля
    /// 
    ///         Значение <paramref name="string1" /> меньше <paramref name="string2" />.
    /// 
    ///         больше нуля
    /// 
    ///         Значение <paramref name="string1" /> больше значения <paramref name="string2" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public virtual int Compare(string string1, string string2)
    {
      return this.Compare(string1, string2, CompareOptions.None);
    }

    /// <summary>
    ///   Сравнивает две строки с помощью заданного <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </summary>
    /// <param name="string1">Первая сравниваемая строка.</param>
    /// <param name="string2">Вторая сравниваемая строка.</param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="string1" /> и <paramref name="string2" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, и <see cref="F:System.Globalization.CompareOptions.StringSort" />.
    /// </param>
    /// <returns>
    /// 32-разрядное целое число со знаком, выражающее лексическое соотношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         нуль
    /// 
    ///         Эти две строки совпадают.
    /// 
    ///         меньше нуля
    /// 
    ///         Значение <paramref name="string1" /> меньше <paramref name="string2" />.
    /// 
    ///         больше нуля
    /// 
    ///         Значение <paramref name="string1" /> больше значения <paramref name="string2" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int Compare(string string1, string string2, CompareOptions options)
    {
      if (options == CompareOptions.OrdinalIgnoreCase)
        return string.Compare(string1, string2, StringComparison.OrdinalIgnoreCase);
      if ((options & CompareOptions.Ordinal) != CompareOptions.None)
      {
        if (options != CompareOptions.Ordinal)
          throw new ArgumentException(Environment.GetResourceString("Argument_CompareOptionOrdinal"), nameof (options));
        return string.CompareOrdinal(string1, string2);
      }
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (options));
      if (string1 == null)
        return string2 == null ? 0 : -1;
      if (string2 == null)
        return 1;
      return CompareInfo.InternalCompareString(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, string1, 0, string1.Length, string2, 0, string2.Length, CompareInfo.GetNativeCompareFlags(options));
    }

    /// <summary>
    ///   Сравнивает часть одной строки с частью другой строки.
    /// </summary>
    /// <param name="string1">Первая сравниваемая строка.</param>
    /// <param name="offset1">
    ///   Отсчитываемый от нуля индекс символа в <paramref name="string1" /> с которого начинается сравнение.
    /// </param>
    /// <param name="length1">
    ///   Число последовательных знаков в <paramref name="string1" /> для сравнения.
    /// </param>
    /// <param name="string2">Вторая сравниваемая строка.</param>
    /// <param name="offset2">
    ///   Отсчитываемый от нуля индекс символа в <paramref name="string2" /> с которого начинается сравнение.
    /// </param>
    /// <param name="length2">
    ///   Число последовательных знаков в <paramref name="string2" /> для сравнения.
    /// </param>
    /// <returns>
    /// 32-разрядное целое число со знаком, выражающее лексическое соотношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         нуль
    /// 
    ///         Эти две строки совпадают.
    /// 
    ///         меньше нуля
    /// 
    ///         Указанный раздел <paramref name="string1" /> меньше, чем указанный раздел <paramref name="string2" />.
    /// 
    ///         больше нуля
    /// 
    ///         Указанный раздел <paramref name="string1" /> больше, чем указанный раздел <paramref name="string2" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset1" /> или <paramref name="length1" /> или <paramref name="offset2" /> или <paramref name="length2" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset1" /> больше или равно числу символов в <paramref name="string1" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset2" /> больше или равно числу символов в <paramref name="string2" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="length1" /> больше, чем количество символов из <paramref name="offset1" /> в конец <paramref name="string1" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="length2" /> больше, чем количество символов из <paramref name="offset2" /> в конец <paramref name="string2" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2)
    {
      return this.Compare(string1, offset1, length1, string2, offset2, length2, CompareOptions.None);
    }

    /// <summary>
    ///   Сравнивает конечную часть строки с конечной частью другой строки с использованием указанного <see cref="T:System.Globalization.CompareOptions" /> значения.
    /// </summary>
    /// <param name="string1">Первая сравниваемая строка.</param>
    /// <param name="offset1">
    ///   Отсчитываемый от нуля индекс символа в <paramref name="string1" /> с которого начинается сравнение.
    /// </param>
    /// <param name="string2">Вторая сравниваемая строка.</param>
    /// <param name="offset2">
    ///   Отсчитываемый от нуля индекс символа в <paramref name="string2" /> с которого начинается сравнение.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="string1" /> и <paramref name="string2" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, и <see cref="F:System.Globalization.CompareOptions.StringSort" />.
    /// </param>
    /// <returns>
    /// 32-разрядное целое число со знаком, выражающее лексическое соотношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         нуль
    /// 
    ///         Эти две строки совпадают.
    /// 
    ///         меньше нуля
    /// 
    ///         Указанный раздел <paramref name="string1" /> меньше, чем указанный раздел <paramref name="string2" />.
    /// 
    ///         больше нуля
    /// 
    ///         Указанный раздел <paramref name="string1" /> больше, чем указанный раздел <paramref name="string2" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset1" /> или <paramref name="offset2" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset1" /> больше или равно числу символов в <paramref name="string1" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset2" /> больше или равно числу символов в <paramref name="string2" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int Compare(string string1, int offset1, string string2, int offset2, CompareOptions options)
    {
      return this.Compare(string1, offset1, string1 == null ? 0 : string1.Length - offset1, string2, offset2, string2 == null ? 0 : string2.Length - offset2, options);
    }

    /// <summary>
    ///   Сравнивает конечную часть одной строки с конечной частью другой строки.
    /// </summary>
    /// <param name="string1">Первая сравниваемая строка.</param>
    /// <param name="offset1">
    ///   Отсчитываемый от нуля индекс символа в <paramref name="string1" /> с которого начинается сравнение.
    /// </param>
    /// <param name="string2">Вторая сравниваемая строка.</param>
    /// <param name="offset2">
    ///   Отсчитываемый от нуля индекс символа в <paramref name="string2" /> с которого начинается сравнение.
    /// </param>
    /// <returns>
    /// 32-разрядное целое число со знаком, выражающее лексическое соотношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         нуль
    /// 
    ///         Эти две строки совпадают.
    /// 
    ///         меньше нуля
    /// 
    ///         Указанный раздел <paramref name="string1" /> меньше, чем указанный раздел <paramref name="string2" />.
    /// 
    ///         больше нуля
    /// 
    ///         Указанный раздел <paramref name="string1" /> больше, чем указанный раздел <paramref name="string2" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset1" /> или <paramref name="offset2" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset1" /> больше или равно числу символов в <paramref name="string1" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset2" /> больше или равно числу символов в <paramref name="string2" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int Compare(string string1, int offset1, string string2, int offset2)
    {
      return this.Compare(string1, offset1, string2, offset2, CompareOptions.None);
    }

    /// <summary>
    ///   Сравнивает часть одной строки с частью другой строки с использованием указанного <see cref="T:System.Globalization.CompareOptions" /> значения.
    /// </summary>
    /// <param name="string1">Первая сравниваемая строка.</param>
    /// <param name="offset1">
    ///   Отсчитываемый от нуля индекс символа в <paramref name="string1" /> с которого начинается сравнение.
    /// </param>
    /// <param name="length1">
    ///   Число последовательных знаков в <paramref name="string1" /> для сравнения.
    /// </param>
    /// <param name="string2">Вторая сравниваемая строка.</param>
    /// <param name="offset2">
    ///   Отсчитываемый от нуля индекс символа в <paramref name="string2" /> с которого начинается сравнение.
    /// </param>
    /// <param name="length2">
    ///   Число последовательных знаков в <paramref name="string2" /> для сравнения.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="string1" /> и <paramref name="string2" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, и <see cref="F:System.Globalization.CompareOptions.StringSort" />.
    /// </param>
    /// <returns>
    /// 32-разрядное целое число со знаком, выражающее лексическое соотношение двух сравниваемых значений.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         нуль
    /// 
    ///         Эти две строки совпадают.
    /// 
    ///         меньше нуля
    /// 
    ///         Указанный раздел <paramref name="string1" /> меньше, чем указанный раздел <paramref name="string2" />.
    /// 
    ///         больше нуля
    /// 
    ///         Указанный раздел <paramref name="string1" /> больше, чем указанный раздел <paramref name="string2" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="offset1" /> или <paramref name="length1" /> или <paramref name="offset2" /> или <paramref name="length2" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset1" /> больше или равно числу символов в <paramref name="string1" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="offset2" /> больше или равно числу символов в <paramref name="string2" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="length1" /> больше, чем количество символов из <paramref name="offset1" /> в конец <paramref name="string1" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="length2" /> больше, чем количество символов из <paramref name="offset2" /> в конец <paramref name="string2" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options)
    {
      if (options == CompareOptions.OrdinalIgnoreCase)
      {
        int num = string.Compare(string1, offset1, string2, offset2, length1 < length2 ? length1 : length2, StringComparison.OrdinalIgnoreCase);
        if (length1 == length2 || num != 0)
          return num;
        return length1 <= length2 ? -1 : 1;
      }
      if (length1 < 0 || length2 < 0)
        throw new ArgumentOutOfRangeException(length1 < 0 ? nameof (length1) : nameof (length2), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (offset1 < 0 || offset2 < 0)
        throw new ArgumentOutOfRangeException(offset1 < 0 ? nameof (offset1) : nameof (offset2), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (offset1 > (string1 == null ? 0 : string1.Length) - length1)
        throw new ArgumentOutOfRangeException(nameof (string1), Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
      if (offset2 > (string2 == null ? 0 : string2.Length) - length2)
        throw new ArgumentOutOfRangeException(nameof (string2), Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
      if ((options & CompareOptions.Ordinal) != CompareOptions.None)
      {
        if (options != CompareOptions.Ordinal)
          throw new ArgumentException(Environment.GetResourceString("Argument_CompareOptionOrdinal"), nameof (options));
      }
      else if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (options));
      if (string1 == null)
        return string2 == null ? 0 : -1;
      if (string2 == null)
        return 1;
      if (options == CompareOptions.Ordinal)
        return CompareInfo.CompareOrdinal(string1, offset1, length1, string2, offset2, length2);
      return CompareInfo.InternalCompareString(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, string1, offset1, length1, string2, offset2, length2, CompareInfo.GetNativeCompareFlags(options));
    }

    [SecurityCritical]
    private static int CompareOrdinal(string string1, int offset1, int length1, string string2, int offset2, int length2)
    {
      int num = string.nativeCompareOrdinalEx(string1, offset1, string2, offset2, length1 < length2 ? length1 : length2);
      if (length1 == length2 || num != 0)
        return num;
      return length1 <= length2 ? -1 : 1;
    }

    /// <summary>
    ///   Определяет, начинается ли указанная строка источника с указанного префикса, с использованием указанного <see cref="T:System.Globalization.CompareOptions" /> значения.
    /// </summary>
    /// <param name="source">Строка, в которой выполняется поиск.</param>
    /// <param name="prefix">
    ///   Строка, сравниваемая с началом <paramref name="source" />.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="prefix" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если длина <paramref name="prefix" /> меньше или равна длине <paramref name="source" /> и <paramref name="source" /> начинается с <paramref name="prefix" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="prefix" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual bool IsPrefix(string source, string prefix, CompareOptions options)
    {
      if (source == null || prefix == null)
        throw new ArgumentNullException(source == null ? nameof (source) : nameof (prefix), Environment.GetResourceString("ArgumentNull_String"));
      if (prefix.Length == 0)
        return true;
      if (options == CompareOptions.OrdinalIgnoreCase)
        return source.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
      if (options == CompareOptions.Ordinal)
        return source.StartsWith(prefix, StringComparison.Ordinal);
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (options));
      return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 1048576 | (!source.IsAscii() || !prefix.IsAscii() ? 0 : 536870912), source, source.Length, 0, prefix, prefix.Length) > -1;
    }

    /// <summary>
    ///   Определяет, начинается ли указанная строка источника с указанного префикса.
    /// </summary>
    /// <param name="source">Строка, в которой выполняется поиск.</param>
    /// <param name="prefix">
    ///   Строка, сравниваемая с началом <paramref name="source" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если длина <paramref name="prefix" /> меньше или равна длине <paramref name="source" /> и <paramref name="source" /> начинается с <paramref name="prefix" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="prefix" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual bool IsPrefix(string source, string prefix)
    {
      return this.IsPrefix(source, prefix, CompareOptions.None);
    }

    /// <summary>
    ///   Определяет, заканчивается ли указанная строка источника указанным суффиксом, с использованием указанного <see cref="T:System.Globalization.CompareOptions" /> значения.
    /// </summary>
    /// <param name="source">Строка, в которой выполняется поиск.</param>
    /// <param name="suffix">
    ///   Строка, сравниваемая с концом <paramref name="source" />.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="suffix" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" /> используется самостоятельно, или Побитовая комбинация одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если длина <paramref name="suffix" /> меньше или равна длине <paramref name="source" /> и <paramref name="source" /> заканчивается <paramref name="suffix" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="suffix" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual bool IsSuffix(string source, string suffix, CompareOptions options)
    {
      if (source == null || suffix == null)
        throw new ArgumentNullException(source == null ? nameof (source) : nameof (suffix), Environment.GetResourceString("ArgumentNull_String"));
      if (suffix.Length == 0)
        return true;
      if (options == CompareOptions.OrdinalIgnoreCase)
        return source.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
      if (options == CompareOptions.Ordinal)
        return source.EndsWith(suffix, StringComparison.Ordinal);
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (options));
      return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 2097152 | (!source.IsAscii() || !suffix.IsAscii() ? 0 : 536870912), source, source.Length, source.Length - 1, suffix, suffix.Length) >= 0;
    }

    /// <summary>
    ///   Определяет, заканчивается ли указанная строка источника указанным суффиксом.
    /// </summary>
    /// <param name="source">Строка, в которой выполняется поиск.</param>
    /// <param name="suffix">
    ///   Строка, сравниваемая с концом <paramref name="source" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если длина <paramref name="suffix" /> меньше или равна длине <paramref name="source" /> и <paramref name="source" /> заканчивается <paramref name="suffix" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="suffix" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual bool IsSuffix(string source, string suffix)
    {
      return this.IsSuffix(source, suffix, CompareOptions.None);
    }

    /// <summary>
    ///   Осуществляет поиск указанного знака и возвращает отсчитываемый с нуля индекс первого найденного экземпляра во всей строке источника.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Символ, который необходимо найти в <paramref name="source" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" />, если есть, в <paramref name="source" />; в противном случае — значение -1.
    ///    Возвращает 0 (нуль), если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, char value)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
    }

    /// <summary>
    ///   Осуществляет поиск указанной подстроки и возвращает отсчитываемый с нуля индекс первого найденного экземпляра во всей строке источника.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Строка для поиска в <paramref name="source" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" />, если есть, в <paramref name="source" />; в противном случае — значение -1.
    ///    Возвращает 0 (нуль), если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, string value)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
    }

    /// <summary>
    ///   Осуществляет поиск указанного знака и возвращает отсчитываемый от нуля индекс первого найденного экземпляра во всей строке источника с использованием заданного <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Символ, который необходимо найти в <paramref name="source" />.
    /// </param>
    /// <param name="options">
    ///   Значение, определяющее способ сравнения строк.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" />, если есть, в <paramref name="source" />, с помощью указанных параметров сравнения; в противном случае — значение -1.
    ///    Возвращает 0 (нуль), если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, char value, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return this.IndexOf(source, value, 0, source.Length, options);
    }

    /// <summary>
    ///   Осуществляет поиск указанной подстроки и возвращает отсчитываемый от нуля индекс первого найденного экземпляра во всей строке источника с использованием заданного <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Строка для поиска в <paramref name="source" />.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="value" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" />, если есть, в <paramref name="source" />, с помощью указанных параметров сравнения; в противном случае — значение -1.
    ///    Возвращает 0 (нуль), если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, string value, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return this.IndexOf(source, value, 0, source.Length, options);
    }

    /// <summary>
    ///   Осуществляет поиск указанного знака и возвращает отсчитываемый с нуля индекс первого экземпляра в части строки источника от указанного индекса до конца строки.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Символ, который необходимо найти в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начальной позиции поиска.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> начиная с <paramref name="startIndex" /> до конца <paramref name="source" />; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// </exception>
    public virtual int IndexOf(string source, char value, int startIndex)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
    }

    /// <summary>
    ///   Осуществляет поиск указанной подстроки и возвращает отсчитываемый с нуля индекс первого экземпляра в части строки источника от указанного индекса до конца строки.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Строка для поиска в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начальной позиции поиска.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> начиная с <paramref name="startIndex" /> до конца <paramref name="source" />; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// </exception>
    public virtual int IndexOf(string source, string value, int startIndex)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
    }

    /// <summary>
    ///   Осуществляет поиск указанного знака и возвращает отсчитываемый от нуля индекс первого экземпляра в разделе строки источника от указанного индекса до конца строки с использованием заданного <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Символ, который необходимо найти в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начальной позиции поиска.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="value" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> начиная с <paramref name="startIndex" /> до конца <paramref name="source" />, с помощью указанных параметров сравнения; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, char value, int startIndex, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
    }

    /// <summary>
    ///   Осуществляет поиск указанной подстроки и возвращает отсчитываемый от нуля индекс первого экземпляра в разделе строки источника от указанного индекса до конца строки с использованием заданного <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Строка для поиска в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начальной позиции поиска.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="value" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> начиная с <paramref name="startIndex" /> до конца <paramref name="source" />, с помощью указанных параметров сравнения; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, string value, int startIndex, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
    }

    /// <summary>
    ///   Осуществляет поиск указанного знака и возвращает отсчитываемый с нуля индекс первого экземпляра в части строки источника, который начинается с указанного индекса и содержит указанное количество элементов.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Символ, который необходимо найти в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начальной позиции поиска.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> начинается с <paramref name="startIndex" /> и содержит количество элементов, определяемое параметром <paramref name="count" />; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> и <paramref name="count" /> не указан недопустимый раздел <paramref name="source" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, char value, int startIndex, int count)
    {
      return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
    }

    /// <summary>
    ///   Осуществляет поиск указанной подстроки и возвращает отсчитываемый с нуля индекс первого экземпляра в части строки источника, которая начинается с указанного индекса и содержит указанное число элементов.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Строка для поиска в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начальной позиции поиска.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> начинается с <paramref name="startIndex" /> и содержит количество элементов, определяемое параметром <paramref name="count" />; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> и <paramref name="count" /> не указан недопустимый раздел <paramref name="source" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, string value, int startIndex, int count)
    {
      return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
    }

    /// <summary>
    ///   Осуществляет поиск указанного знака и возвращает отсчитываемый от нуля индекс первого экземпляра в разделе строки источника, который начинается с указанного индекса и содержит указанное число элементов, с использованием указанного <see cref="T:System.Globalization.CompareOptions" /> значения.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Символ, который необходимо найти в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начальной позиции поиска.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="value" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> начинается с <paramref name="startIndex" /> и содержит количество элементов, определяемое параметром <paramref name="count" />, с помощью указанных параметров сравнения; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> и <paramref name="count" /> не указан недопустимый раздел <paramref name="source" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, char value, int startIndex, int count, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (startIndex < 0 || startIndex > source.Length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > source.Length - count)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
      if (options == CompareOptions.OrdinalIgnoreCase)
        return source.IndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (options));
      return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 4194304 | (!source.IsAscii() || value > '\x007F' ? 0 : 536870912), source, count, startIndex, new string(value, 1), 1);
    }

    /// <summary>
    ///   Осуществляет поиск указанной подстроки и возвращает отсчитываемый от нуля индекс первого экземпляра в разделе строки источника, который начинается с указанного индекса и содержит указанное число элементов, с использованием указанного <see cref="T:System.Globalization.CompareOptions" /> значения.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Строка для поиска в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начальной позиции поиска.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="value" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс первого вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> начинается с <paramref name="startIndex" /> и содержит количество элементов, определяемое параметром <paramref name="count" />, с помощью указанных параметров сравнения; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> и <paramref name="count" /> не указан недопустимый раздел <paramref name="source" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int IndexOf(string source, string value, int startIndex, int count, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (startIndex > source.Length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (source.Length == 0)
        return value.Length == 0 ? 0 : -1;
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > source.Length - count)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
      if (options == CompareOptions.OrdinalIgnoreCase)
        return source.IndexOf(value, startIndex, count, StringComparison.OrdinalIgnoreCase);
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (options));
      return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 4194304 | (!source.IsAscii() || !value.IsAscii() ? 0 : 536870912), source, count, startIndex, value, value.Length);
    }

    /// <summary>
    ///   Осуществляет поиск указанного знака и возвращает отсчитываемый с нуля индекс последнего найденного экземпляра во всей строке источника.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Символ, который необходимо найти в <paramref name="source" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" />, если есть, в <paramref name="source" />; в противном случае — значение -1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, char value)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
    }

    /// <summary>
    ///   Выполняет поиск указанной подстроки и возвращает начинающийся с нуля индекс последнего экземпляра в рамках всей исходной строки.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Строка для поиска в <paramref name="source" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" />, если есть, в <paramref name="source" />; в противном случае — значение -1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, string value)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
    }

    /// <summary>
    ///   Осуществляет поиск указанного знака и возвращает отсчитываемый от нуля индекс последнего найденного экземпляра во всей строке источника с использованием заданного <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Символ, который необходимо найти в <paramref name="source" />.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="value" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" />, если есть, в <paramref name="source" />, с помощью указанных параметров сравнения; в противном случае — значение -1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, char value, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
    }

    /// <summary>
    ///   Осуществляет поиск указанной подстроки и возвращает отсчитываемый от нуля индекс последнего найденного экземпляра во всей строке источника с использованием заданного <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Строка для поиска в <paramref name="source" />.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="value" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" />, если есть, в <paramref name="source" />, с помощью указанных параметров сравнения; в противном случае — значение -1.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, string value, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
    }

    /// <summary>
    ///   Осуществляет поиск указанного знака и возвращает отсчитываемый с нуля индекс последнего экземпляра в разделе строки источника от начала строки до указанного индекса.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Символ, который необходимо найти в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> начиная с самого начала <paramref name="source" /> для <paramref name="startIndex" />; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// </exception>
    public virtual int LastIndexOf(string source, char value, int startIndex)
    {
      return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
    }

    /// <summary>
    ///   Осуществляет поиск указанной подстроки и возвращает отсчитываемый с нуля индекс последнего экземпляра в части строки источника от начала строки до указанного индекса.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Строка для поиска в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> начиная с самого начала <paramref name="source" /> для <paramref name="startIndex" />; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// </exception>
    public virtual int LastIndexOf(string source, string value, int startIndex)
    {
      return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
    }

    /// <summary>
    ///   Осуществляет поиск указанного знака и возвращает отсчитываемый от нуля индекс последнего экземпляра в разделе строки источника от начала строки до указанного индекса с использованием заданного <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Символ, который необходимо найти в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="value" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> начиная с самого начала <paramref name="source" /> для <paramref name="startIndex" />, с помощью указанных параметров сравнения; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, char value, int startIndex, CompareOptions options)
    {
      return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
    }

    /// <summary>
    ///   Осуществляет поиск указанной подстроки и возвращает отсчитываемый от нуля индекс последнего экземпляра в разделе строки источника от начала строки до указанного индекса с использованием заданного <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Строка для поиска в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="value" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> начиная с самого начала <paramref name="source" /> для <paramref name="startIndex" />, с помощью указанных параметров сравнения; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, string value, int startIndex, CompareOptions options)
    {
      return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
    }

    /// <summary>
    ///   Осуществляет поиск указанного знака и возвращает отсчитываемый с нуля индекс последнего экземпляра в части строки источника, которая содержит указанное количество элементов и заканчивается на указанном индексе.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Символ, который необходимо найти в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> содержит количество элементов, определяемое параметром <paramref name="count" /> и заканчивается на <paramref name="startIndex" />; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> и <paramref name="count" /> не указан недопустимый раздел <paramref name="source" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, char value, int startIndex, int count)
    {
      return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
    }

    /// <summary>
    ///   Осуществляет поиск указанной подстроки и возвращает отсчитываемый с нуля индекс последнего экземпляра в части строки источника, который содержит указанное количество элементов и заканчивается на указанном индексе.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Строка для поиска в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> содержит количество элементов, определяемое параметром <paramref name="count" /> и заканчивается на <paramref name="startIndex" />; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> и <paramref name="count" /> не указан недопустимый раздел <paramref name="source" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, string value, int startIndex, int count)
    {
      return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
    }

    /// <summary>
    ///   Осуществляет поиск указанного знака и возвращает отсчитываемый от нуля индекс последнего экземпляра в разделе строки источника, который содержит указанное количество элементов и заканчивается по указанному индексу с использованием указанного <see cref="T:System.Globalization.CompareOptions" /> значения.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Символ, который необходимо найти в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="value" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> содержит количество элементов, определяемое параметром <paramref name="count" /> и заканчивается на <paramref name="startIndex" />, с помощью указанных параметров сравнения; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> и <paramref name="count" /> не указан недопустимый раздел <paramref name="source" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, char value, int startIndex, int count, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (options));
      if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
        return -1;
      if (startIndex < 0 || startIndex > source.Length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (startIndex == source.Length)
      {
        --startIndex;
        if (count > 0)
          --count;
      }
      if (count < 0 || startIndex - count + 1 < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
      if (options == CompareOptions.OrdinalIgnoreCase)
        return source.LastIndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
      return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 8388608 | (!source.IsAscii() || value > '\x007F' ? 0 : 536870912), source, count, startIndex, new string(value, 1), 1);
    }

    /// <summary>
    ///   Осуществляет поиск указанной подстроки и возвращает отсчитываемый от нуля индекс последнего экземпляра в разделе строки источника, который содержит указанное количество элементов и заканчивается по указанному индексу с использованием указанного <see cref="T:System.Globalization.CompareOptions" /> значения.
    /// </summary>
    /// <param name="source">Строка для поиска.</param>
    /// <param name="value">
    ///   Строка для поиска в <paramref name="source" />.
    /// </param>
    /// <param name="startIndex">
    ///   Индекс (с нуля) начала диапазона поиска в обратном направлении.
    /// </param>
    /// <param name="count">
    ///   Число элементов в диапазоне, в котором выполняется поиск.
    /// </param>
    /// <param name="options">
    ///   Значение, которое определяет как <paramref name="source" /> и <paramref name="value" /> подлежат сравнению.
    ///   <paramref name="options" /> значение перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" />, или побитовой комбинацией одного или нескольких из следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, и <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
    /// </param>
    /// <returns>
    ///   Отсчитываемый от нуля индекс последнего вхождения <paramref name="value" />, если найден в разделе <paramref name="source" /> содержит количество элементов, определяемое параметром <paramref name="count" /> и заканчивается на <paramref name="startIndex" />, с помощью указанных параметров сравнения; в противном случае — значение -1.
    ///    Возвращает <paramref name="startIndex" /> Если <paramref name="value" /> — игнорируемый символ.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startIndex" /> находится вне диапазона допустимых индексов для <paramref name="source" />.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="count" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startIndex" /> и <paramref name="count" /> не указан недопустимый раздел <paramref name="source" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int LastIndexOf(string source, string value, int startIndex, int count, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (options));
      if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
        return value.Length != 0 ? -1 : 0;
      if (startIndex < 0 || startIndex > source.Length)
        throw new ArgumentOutOfRangeException(nameof (startIndex), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (startIndex == source.Length)
      {
        --startIndex;
        if (count > 0)
          --count;
        if (value.Length == 0 && count >= 0 && startIndex - count + 1 >= 0)
          return startIndex;
      }
      if (count < 0 || startIndex - count + 1 < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Count"));
      if (options == CompareOptions.OrdinalIgnoreCase)
        return source.LastIndexOf(value, startIndex, count, StringComparison.OrdinalIgnoreCase);
      return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 8388608 | (!source.IsAscii() || !value.IsAscii() ? 0 : 536870912), source, count, startIndex, value, value.Length);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Globalization.SortKey" /> объекта для указанной строки с помощью заданного <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </summary>
    /// <param name="source">
    ///   Строка, для которой <see cref="T:System.Globalization.SortKey" /> получен объект.
    /// </param>
    /// <param name="options">
    ///   Побитовая комбинация одного или нескольких из следующих значений перечисления, определяющих, как вычисляется ключ сортировки: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, и <see cref="F:System.Globalization.CompareOptions.StringSort" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Globalization.SortKey" /> Объект, содержащий ключ сортировки для указанной строки.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> содержит недопустимое <see cref="T:System.Globalization.CompareOptions" /> значение.
    /// </exception>
    public virtual SortKey GetSortKey(string source, CompareOptions options)
    {
      return this.CreateSortKey(source, options);
    }

    /// <summary>Получает ключ сортировки для указанной строки.</summary>
    /// <param name="source">
    ///   Строка, для которой <see cref="T:System.Globalization.SortKey" /> получен объект.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Globalization.SortKey" /> Объект, содержащий ключ сортировки для указанной строки.
    /// </returns>
    public virtual SortKey GetSortKey(string source)
    {
      return this.CreateSortKey(source, CompareOptions.None);
    }

    [SecuritySafeCritical]
    private SortKey CreateSortKey(string source, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (options));
      byte[] numArray = (byte[]) null;
      if (string.IsNullOrEmpty(source))
      {
        numArray = EmptyArray<byte>.Value;
        source = "\0";
      }
      int nativeCompareFlags = CompareInfo.GetNativeCompareFlags(options);
      int sortKey = CompareInfo.InternalGetSortKey(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, nativeCompareFlags, source, source.Length, (byte[]) null, 0);
      if (sortKey == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (source));
      if (numArray == null)
      {
        numArray = new byte[sortKey];
        CompareInfo.InternalGetSortKey(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, nativeCompareFlags, source, source.Length, numArray, numArray.Length);
      }
      else
        source = string.Empty;
      return new SortKey(this.Name, source, options, numArray);
    }

    /// <summary>
    ///   Определяет, равен ли заданный объект текущему объекту <see cref="T:System.Globalization.CompareInfo" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, который требуется сравнить с текущим объектом <see cref="T:System.Globalization.CompareInfo" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанный объект равен текущему объекту <see cref="T:System.Globalization.CompareInfo" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      CompareInfo compareInfo = value as CompareInfo;
      if (compareInfo != null)
        return this.Name == compareInfo.Name;
      return false;
    }

    /// <summary>
    ///   Служит хэш-функцией текущего <see cref="T:System.Globalization.CompareInfo" /> для использования в алгоритмах и структурах данных, таких как хэш-таблицу хеширования.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Globalization.CompareInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    /// <summary>
    ///   Возвращает хэш-код для строки на основании указанных параметров сравнения.
    /// </summary>
    /// <param name="source">
    ///   Строка, хэш-код — должны быть возвращены.
    /// </param>
    /// <param name="options">
    ///   Значение, определяющее способ сравнения строк.
    /// </param>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="source" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int GetHashCode(string source, CompareOptions options)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (options == CompareOptions.Ordinal)
        return source.GetHashCode();
      if (options == CompareOptions.OrdinalIgnoreCase)
        return TextInfo.GetHashCodeOrdinalIgnoreCase(source);
      return this.GetHashCodeOfString(source, options, false, 0L);
    }

    internal int GetHashCodeOfString(string source, CompareOptions options)
    {
      return this.GetHashCodeOfString(source, options, false, 0L);
    }

    [SecuritySafeCritical]
    internal int GetHashCodeOfString(string source, CompareOptions options, bool forceRandomizedHashing, long additionalEntropy)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (options));
      if (source.Length == 0)
        return 0;
      return CompareInfo.InternalGetGlobalizedHashCode(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, source, source.Length, CompareInfo.GetNativeCompareFlags(options), forceRandomizedHashing, additionalEntropy);
    }

    /// <summary>
    ///   Возвращает строку, представляющую текущий объект <see cref="T:System.Globalization.CompareInfo" />.
    /// </summary>
    /// <returns>
    ///   Строка, представляющая текущий объект <see cref="T:System.Globalization.CompareInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return "CompareInfo - " + this.Name;
    }

    /// <summary>
    ///   Возвращает идентификатор языка и региональных параметров для текущего <see cref="T:System.Globalization.CompareInfo" />.
    /// </summary>
    /// <returns>
    ///   Идентификатор языка и региональных параметров текущего <see cref="T:System.Globalization.CompareInfo" />.
    /// </returns>
    public int LCID
    {
      get
      {
        return CultureInfo.GetCultureInfo(this.Name).LCID;
      }
    }

    [SecuritySafeCritical]
    internal static IntPtr InternalInitSortHandle(string localeName, out IntPtr handleOrigin)
    {
      return CompareInfo.NativeInternalInitSortHandle(localeName, out handleOrigin);
    }

    internal static bool IsLegacy20SortingBehaviorRequested
    {
      get
      {
        return CompareInfo.InternalSortVersion == 4096U;
      }
    }

    private static uint InternalSortVersion
    {
      [SecuritySafeCritical] get
      {
        return CompareInfo.InternalGetSortVersion();
      }
    }

    /// <summary>
    ///   Получает сведения о версии Юникода, используемой для сравнения и сортировки строк.
    /// </summary>
    /// <returns>
    ///   Объект, содержащий сведения о версии Юникода, используемой для сравнения и сортировки строк.
    /// </returns>
    public SortVersion Version
    {
      [SecuritySafeCritical] get
      {
        if (this.m_SortVersion == (SortVersion) null)
        {
          Win32Native.NlsVersionInfoEx lpNlsVersionInformation = new Win32Native.NlsVersionInfoEx();
          lpNlsVersionInformation.dwNLSVersionInfoSize = Marshal.SizeOf(typeof (Win32Native.NlsVersionInfoEx));
          CompareInfo.InternalGetNlsVersionEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, ref lpNlsVersionInformation);
          this.m_SortVersion = new SortVersion(lpNlsVersionInformation.dwNLSVersion, lpNlsVersionInformation.dwEffectiveId != 0 ? lpNlsVersionInformation.dwEffectiveId : this.LCID, lpNlsVersionInformation.guidCustomVersion);
        }
        return this.m_SortVersion;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InternalGetNlsVersionEx(IntPtr handle, IntPtr handleOrigin, string localeName, ref Win32Native.NlsVersionInfoEx lpNlsVersionInformation);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern uint InternalGetSortVersion();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern IntPtr NativeInternalInitSortHandle(string localeName, out IntPtr handleOrigin);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int InternalGetGlobalizedHashCode(IntPtr handle, IntPtr handleOrigin, string localeName, string source, int length, int dwFlags, bool forceRandomizedHashing, long additionalEntropy);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InternalIsSortable(IntPtr handle, IntPtr handleOrigin, string localeName, string source, int length);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int InternalCompareString(IntPtr handle, IntPtr handleOrigin, string localeName, string string1, int offset1, int length1, string string2, int offset2, int length2, int flags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int InternalFindNLSStringEx(IntPtr handle, IntPtr handleOrigin, string localeName, int flags, string source, int sourceCount, int startIndex, string target, int targetCount);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int InternalGetSortKey(IntPtr handle, IntPtr handleOrigin, string localeName, int flags, string source, int sourceCount, byte[] target, int targetCount);
  }
}
