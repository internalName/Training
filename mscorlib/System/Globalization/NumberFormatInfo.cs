// Decompiled with JetBrains decompiler
// Type: System.Globalization.NumberFormatInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Globalization
{
  /// <summary>
  ///   Предоставляет сведения для конкретного языка и региональных параметров для числовых значений форматирования и анализа.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class NumberFormatInfo : ICloneable, IFormatProvider
  {
    internal int[] numberGroupSizes = new int[1]{ 3 };
    internal int[] currencyGroupSizes = new int[1]{ 3 };
    internal int[] percentGroupSizes = new int[1]{ 3 };
    internal string positiveSign = "+";
    internal string negativeSign = "-";
    internal string numberDecimalSeparator = ".";
    internal string numberGroupSeparator = ",";
    internal string currencyGroupSeparator = ",";
    internal string currencyDecimalSeparator = ".";
    internal string currencySymbol = "¤";
    internal string nanSymbol = "NaN";
    internal string positiveInfinitySymbol = "Infinity";
    internal string negativeInfinitySymbol = "-Infinity";
    internal string percentDecimalSeparator = ".";
    internal string percentGroupSeparator = ",";
    internal string percentSymbol = "%";
    internal string perMilleSymbol = "‰";
    [OptionalField(VersionAdded = 2)]
    internal string[] nativeDigits = new string[10]
    {
      "0",
      "1",
      "2",
      "3",
      "4",
      "5",
      "6",
      "7",
      "8",
      "9"
    };
    internal int numberDecimalDigits = 2;
    internal int currencyDecimalDigits = 2;
    internal int numberNegativePattern = 1;
    internal int percentDecimalDigits = 2;
    [OptionalField(VersionAdded = 2)]
    internal int digitSubstitution = 1;
    [OptionalField(VersionAdded = 1)]
    internal bool validForParseAsNumber = true;
    [OptionalField(VersionAdded = 1)]
    internal bool validForParseAsCurrency = true;
    private static volatile NumberFormatInfo invariantInfo;
    internal string ansiCurrencySymbol;
    [OptionalField(VersionAdded = 1)]
    internal int m_dataItem;
    internal int currencyPositivePattern;
    internal int currencyNegativePattern;
    internal int percentPositivePattern;
    internal int percentNegativePattern;
    internal bool isReadOnly;
    [OptionalField(VersionAdded = 1)]
    internal bool m_useUserOverride;
    [OptionalField(VersionAdded = 2)]
    internal bool m_isInvariant;
    private const NumberStyles InvalidNumberStyles = ~(NumberStyles.Any | NumberStyles.AllowHexSpecifier);

    /// <summary>
    ///   Инициализирует новый доступный для записи экземпляр класса <see cref="T:System.Globalization.NumberFormatInfo" />, не зависящий от языка и региональных параметров (инвариантный).
    /// </summary>
    [__DynamicallyInvokable]
    public NumberFormatInfo()
      : this((CultureData) null)
    {
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.validForParseAsNumber = this.numberDecimalSeparator != this.numberGroupSeparator;
      if (this.numberDecimalSeparator != this.numberGroupSeparator && this.numberDecimalSeparator != this.currencyGroupSeparator && (this.currencyDecimalSeparator != this.numberGroupSeparator && this.currencyDecimalSeparator != this.currencyGroupSeparator))
        this.validForParseAsCurrency = true;
      else
        this.validForParseAsCurrency = false;
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
    }

    private static void VerifyDecimalSeparator(string decSep, string propertyName)
    {
      if (decSep == null)
        throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_String"));
      if (decSep.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyDecString"));
    }

    private static void VerifyGroupSeparator(string groupSep, string propertyName)
    {
      if (groupSep == null)
        throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_String"));
    }

    private static void VerifyNativeDigits(string[] nativeDig, string propertyName)
    {
      if (nativeDig == null)
        throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_Array"));
      if (nativeDig.Length != 10)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitCount"), propertyName);
      for (int index = 0; index < nativeDig.Length; ++index)
      {
        if (nativeDig[index] == null)
          throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_ArrayValue"));
        if (nativeDig[index].Length != 1)
        {
          if (nativeDig[index].Length != 2)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitValue"), propertyName);
          if (!char.IsSurrogatePair(nativeDig[index][0], nativeDig[index][1]))
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitValue"), propertyName);
        }
        if (CharUnicodeInfo.GetDecimalDigitValue(nativeDig[index], 0) != index && CharUnicodeInfo.GetUnicodeCategory(nativeDig[index], 0) != UnicodeCategory.PrivateUse)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitValue"), propertyName);
      }
    }

    private static void VerifyDigitSubstitution(DigitShapes digitSub, string propertyName)
    {
      switch (digitSub)
      {
        case DigitShapes.Context:
          break;
        case DigitShapes.None:
          break;
        case DigitShapes.NativeNational:
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDigitSubstitution"), propertyName);
      }
    }

    [SecuritySafeCritical]
    internal NumberFormatInfo(CultureData cultureData)
    {
      if (cultureData == null)
        return;
      cultureData.GetNFIValues(this);
      if (!cultureData.IsInvariantCulture)
        return;
      this.m_isInvariant = true;
    }

    private void VerifyWritable()
    {
      if (this.isReadOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Globalization.NumberFormatInfo" />, доступный только для чтения, который не зависит от языка и региональных параметров (инвариантный).
    /// </summary>
    /// <returns>
    ///   Объект, доступный только для чтения, который не зависит от языка и региональных параметров (инвариантный).
    /// </returns>
    [__DynamicallyInvokable]
    public static NumberFormatInfo InvariantInfo
    {
      [__DynamicallyInvokable] get
      {
        if (NumberFormatInfo.invariantInfo == null)
          NumberFormatInfo.invariantInfo = NumberFormatInfo.ReadOnly(new NumberFormatInfo()
          {
            m_isInvariant = true
          });
        return NumberFormatInfo.invariantInfo;
      }
    }

    /// <summary>
    ///   Возвращает класс <see cref="T:System.Globalization.NumberFormatInfo" />, связанный с заданным <see cref="T:System.IFormatProvider" />.
    /// </summary>
    /// <param name="formatProvider">
    ///   Объект <see cref="T:System.IFormatProvider" />, используемый для получения <see cref="T:System.Globalization.NumberFormatInfo" />.
    /// 
    ///   -или-
    /// 
    ///   Значение <see langword="null" />, чтобы получить <see cref="P:System.Globalization.NumberFormatInfo.CurrentInfo" />.
    /// </param>
    /// <returns>
    ///   Класс <see cref="T:System.Globalization.NumberFormatInfo" />, связанный с заданным классом <see cref="T:System.IFormatProvider" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static NumberFormatInfo GetInstance(IFormatProvider formatProvider)
    {
      CultureInfo cultureInfo = formatProvider as CultureInfo;
      if (cultureInfo != null && !cultureInfo.m_isInherited)
        return cultureInfo.numInfo ?? cultureInfo.NumberFormat;
      NumberFormatInfo numberFormatInfo = formatProvider as NumberFormatInfo;
      if (numberFormatInfo != null)
        return numberFormatInfo;
      if (formatProvider != null)
      {
        NumberFormatInfo format = formatProvider.GetFormat(typeof (NumberFormatInfo)) as NumberFormatInfo;
        if (format != null)
          return format;
      }
      return NumberFormatInfo.CurrentInfo;
    }

    /// <summary>
    ///   Создает неполную копию объекта <see cref="T:System.Globalization.NumberFormatInfo" />.
    /// </summary>
    /// <returns>
    ///   Новый объект, скопированный из исходного объекта <see cref="T:System.Globalization.NumberFormatInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public object Clone()
    {
      NumberFormatInfo numberFormatInfo = (NumberFormatInfo) this.MemberwiseClone();
      numberFormatInfo.isReadOnly = false;
      return (object) numberFormatInfo;
    }

    /// <summary>
    ///   Возвращает или задает число десятичных разрядов, используемое в значениях денежных сумм.
    /// </summary>
    /// <returns>
    ///   Число десятичных разрядов, используемое в значениях денежных сумм.
    ///    Значение по умолчанию для аргумента <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> равно 2.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Для свойства задано значение меньше 0 или больше 99.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства задается значение, а объект <see cref="T:System.Globalization.NumberFormatInfo" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public int CurrencyDecimalDigits
    {
      [__DynamicallyInvokable] get
      {
        return this.currencyDecimalDigits;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 99)
          throw new ArgumentOutOfRangeException(nameof (CurrencyDecimalDigits), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 99));
        this.VerifyWritable();
        this.currencyDecimalDigits = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, используемую в качестве десятичного разделителя в значениях денежных сумм.
    /// </summary>
    /// <returns>
    ///   Строка, используемая в качестве десятичного разделителя в значениях денежных сумм.
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> равно ".".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства задается значение, а объект <see cref="T:System.Globalization.NumberFormatInfo" /> доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Для свойства задается пустая строка.
    /// </exception>
    [__DynamicallyInvokable]
    public string CurrencyDecimalSeparator
    {
      [__DynamicallyInvokable] get
      {
        return this.currencyDecimalSeparator;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyDecimalSeparator(value, nameof (CurrencyDecimalSeparator));
        this.currencyDecimalSeparator = value;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли данный объект <see cref="T:System.Globalization.NumberFormatInfo" /> доступным только для чтения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если коллекция <see cref="T:System.Globalization.NumberFormatInfo" /> доступна только для чтения; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.isReadOnly;
      }
    }

    internal static void CheckGroupSize(string propName, int[] groupSize)
    {
      for (int index = 0; index < groupSize.Length; ++index)
      {
        if (groupSize[index] < 1)
        {
          if (index == groupSize.Length - 1 && groupSize[index] == 0)
            break;
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGroupSize"), propName);
        }
        if (groupSize[index] > 9)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGroupSize"), propName);
      }
    }

    /// <summary>
    ///   Возвращает или задает число цифр в каждой из групп целой части десятичной дроби в значениях денежных сумм.
    /// </summary>
    /// <returns>
    ///   Число цифр в каждой из групп целой части десятичной дроби в значениях денежных сумм.
    ///    Для свойства <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> по умолчанию используется одномерный массив с единственным элементом, для которого задано значение 3.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Для свойства задается значение, и массив содержит запись, которая меньше 0 или больше 9.
    /// 
    ///   -или-
    /// 
    ///   Для свойства задается значение, и массив содержит запись, отличную от последней записи, которая имеет значение 0.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства задается значение, а объект <see cref="T:System.Globalization.NumberFormatInfo" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public int[] CurrencyGroupSizes
    {
      [__DynamicallyInvokable] get
      {
        return (int[]) this.currencyGroupSizes.Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (CurrencyGroupSizes), Environment.GetResourceString("ArgumentNull_Obj"));
        this.VerifyWritable();
        int[] groupSize = (int[]) value.Clone();
        NumberFormatInfo.CheckGroupSize(nameof (CurrencyGroupSizes), groupSize);
        this.currencyGroupSizes = groupSize;
      }
    }

    /// <summary>
    ///   Возвращает или задает число цифр в каждой из групп целой части десятичной дроби в числовых значениях.
    /// </summary>
    /// <returns>
    ///   Число цифр в каждой из групп целой части десятичной дроби в числовых значениях.
    ///    Для свойства <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> по умолчанию используется одномерный массив с единственным элементом, для которого задано значение 3.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Для свойства задается значение, и массив содержит запись, которая меньше 0 или больше 9.
    /// 
    ///   -или-
    /// 
    ///   Для свойства задается значение, и массив содержит запись, отличную от последней записи, которая имеет значение 0.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public int[] NumberGroupSizes
    {
      [__DynamicallyInvokable] get
      {
        return (int[]) this.numberGroupSizes.Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (NumberGroupSizes), Environment.GetResourceString("ArgumentNull_Obj"));
        this.VerifyWritable();
        int[] groupSize = (int[]) value.Clone();
        NumberFormatInfo.CheckGroupSize(nameof (NumberGroupSizes), groupSize);
        this.numberGroupSizes = groupSize;
      }
    }

    /// <summary>
    ///   Возвращает или задает количество цифр в каждой из групп разрядов целой части десятичной дроби в значениях процентов.
    /// </summary>
    /// <returns>
    ///   Число цифр в каждой из групп целой части десятичной дроби в значениях процентов.
    ///    Для свойства <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> по умолчанию используется одномерный массив с единственным элементом, для которого задано значение 3.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Для свойства задается значение, и массив содержит запись, которая меньше 0 или больше 9.
    /// 
    ///   -или-
    /// 
    ///   Для свойства задается значение, и массив содержит запись, отличную от последней записи, которая имеет значение 0.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public int[] PercentGroupSizes
    {
      [__DynamicallyInvokable] get
      {
        return (int[]) this.percentGroupSizes.Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (PercentGroupSizes), Environment.GetResourceString("ArgumentNull_Obj"));
        this.VerifyWritable();
        int[] groupSize = (int[]) value.Clone();
        NumberFormatInfo.CheckGroupSize(nameof (PercentGroupSizes), groupSize);
        this.percentGroupSizes = groupSize;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, разделяющую разряды в целой части десятичной дроби в значениях денежных сумм.
    /// </summary>
    /// <returns>
    ///   Строка, разделяющая разряды в целой части десятичной дроби в значениях денежных сумм.
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> равно "-".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства задается значение, а объект <see cref="T:System.Globalization.NumberFormatInfo" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public string CurrencyGroupSeparator
    {
      [__DynamicallyInvokable] get
      {
        return this.currencyGroupSeparator;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyGroupSeparator(value, nameof (CurrencyGroupSeparator));
        this.currencyGroupSeparator = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, используемую в качестве знака денежной единицы.
    /// </summary>
    /// <returns>
    ///   Строка, используемая в качестве знака денежной единицы.
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> — "¤".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства задается значение, а объект <see cref="T:System.Globalization.NumberFormatInfo" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public string CurrencySymbol
    {
      [__DynamicallyInvokable] get
      {
        return this.currencySymbol;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (CurrencySymbol), Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.currencySymbol = value;
      }
    }

    /// <summary>
    ///   Возвращает доступный только для чтения объект <see cref="T:System.Globalization.NumberFormatInfo" />, форматирующий значения на основе текущего языка и региональных параметров.
    /// </summary>
    /// <returns>
    ///   Доступный только для чтения объект <see cref="T:System.Globalization.NumberFormatInfo" /> на основе языка и региональных параметров текущего потока.
    /// </returns>
    [__DynamicallyInvokable]
    public static NumberFormatInfo CurrentInfo
    {
      [__DynamicallyInvokable] get
      {
        CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
        if (!currentCulture.m_isInherited)
        {
          NumberFormatInfo numInfo = currentCulture.numInfo;
          if (numInfo != null)
            return numInfo;
        }
        return (NumberFormatInfo) currentCulture.GetFormat(typeof (NumberFormatInfo));
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, представляющую значение IEEE NaN (не числовое).
    /// </summary>
    /// <returns>
    ///   Строка, представляющая значение IEEE NaN (не числовое).
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> — "NaN".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public string NaNSymbol
    {
      [__DynamicallyInvokable] get
      {
        return this.nanSymbol;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (NaNSymbol), Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.nanSymbol = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает шаблон формата для отрицательных значений денежных сумм.
    /// </summary>
    /// <returns>
    ///   Шаблон формата для отрицательных значений денежных сумм.
    ///    По умолчанию для свойства <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> задано значение 0, представляющее "($n)", где "$" — это <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" />, а <paramref name="n" /> — число.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Для свойства задано значение меньше 0 или больше 15.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства задается значение, а объект <see cref="T:System.Globalization.NumberFormatInfo" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public int CurrencyNegativePattern
    {
      [__DynamicallyInvokable] get
      {
        return this.currencyNegativePattern;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 15)
          throw new ArgumentOutOfRangeException(nameof (CurrencyNegativePattern), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 15));
        this.VerifyWritable();
        this.currencyNegativePattern = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает шаблон формата для отрицательных числовых значений.
    /// </summary>
    /// <returns>Шаблон формата для отрицательных числовых значений.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Для свойства задано значение меньше 0 или больше 4.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public int NumberNegativePattern
    {
      [__DynamicallyInvokable] get
      {
        return this.numberNegativePattern;
      }
      [__DynamicallyInvokable] set
      {
        switch (value)
        {
          case 0:
          case 1:
          case 2:
          case 3:
          case 4:
            this.VerifyWritable();
            this.numberNegativePattern = value;
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof (NumberNegativePattern), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 4));
        }
      }
    }

    /// <summary>
    ///   Возвращает или задает шаблон формата для положительных значений процентов.
    /// </summary>
    /// <returns>
    ///   Шаблон формата для положительных значений процентов.
    ///    По умолчанию для свойства <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> задано значение 0, представляющее "n %", где "%" — это <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" />, а <paramref name="n" /> — число.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Для свойства задано значение меньше 0 или больше 3.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public int PercentPositivePattern
    {
      [__DynamicallyInvokable] get
      {
        return this.percentPositivePattern;
      }
      [__DynamicallyInvokable] set
      {
        switch (value)
        {
          case 0:
          case 1:
          case 2:
          case 3:
            this.VerifyWritable();
            this.percentPositivePattern = value;
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof (PercentPositivePattern), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 3));
        }
      }
    }

    /// <summary>
    ///   Возвращает или задает шаблон формата для отрицательных значений процентов.
    /// </summary>
    /// <returns>
    ///   Шаблон формата для отрицательных значений процентов.
    ///    По умолчанию для свойства <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> задано значение 0, представляющее "-n %", где "%" — это <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" />, а <paramref name="n" /> — число.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Свойство задано значение меньше 0 или больше 11.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public int PercentNegativePattern
    {
      [__DynamicallyInvokable] get
      {
        return this.percentNegativePattern;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 11)
          throw new ArgumentOutOfRangeException(nameof (PercentNegativePattern), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 11));
        this.VerifyWritable();
        this.percentNegativePattern = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, представляющую минус бесконечность.
    /// </summary>
    /// <returns>
    ///   Строка, представляющая минус бесконечность.
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> ‏‏— "Infinity".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public string NegativeInfinitySymbol
    {
      [__DynamicallyInvokable] get
      {
        return this.negativeInfinitySymbol;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (NegativeInfinitySymbol), Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.negativeInfinitySymbol = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, указывающую, что соответствующее число является отрицательным.
    /// </summary>
    /// <returns>
    ///   Строка, указывающая, что соответствующее число является отрицательным.
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> — "+".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public string NegativeSign
    {
      [__DynamicallyInvokable] get
      {
        return this.negativeSign;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (NegativeSign), Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.negativeSign = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает число десятичных разрядов, используемое в числовых значениях.
    /// </summary>
    /// <returns>
    ///   Число десятичных разрядов, используемое в числовых значениях.
    ///    Значение по умолчанию для аргумента <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> равно 2.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Для свойства задано значение меньше 0 или больше 99.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public int NumberDecimalDigits
    {
      [__DynamicallyInvokable] get
      {
        return this.numberDecimalDigits;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 99)
          throw new ArgumentOutOfRangeException(nameof (NumberDecimalDigits), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 99));
        this.VerifyWritable();
        this.numberDecimalDigits = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, используемую в качестве десятичного разделителя в числовых значениях.
    /// </summary>
    /// <returns>
    ///   Строка, используемая в качестве десятичного разделителя в числовых значениях.
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> равно ".".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства задается значение, а объект <see cref="T:System.Globalization.NumberFormatInfo" /> доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Для свойства задается пустая строка.
    /// </exception>
    [__DynamicallyInvokable]
    public string NumberDecimalSeparator
    {
      [__DynamicallyInvokable] get
      {
        return this.numberDecimalSeparator;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyDecimalSeparator(value, nameof (NumberDecimalSeparator));
        this.numberDecimalSeparator = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, разделяющую разряды в целой части десятичной дроби в числовых значениях.
    /// </summary>
    /// <returns>
    ///   Строка, разделяющая разряды в целой части десятичной дроби в числовых значениях.
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> равно "-".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public string NumberGroupSeparator
    {
      [__DynamicallyInvokable] get
      {
        return this.numberGroupSeparator;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyGroupSeparator(value, nameof (NumberGroupSeparator));
        this.numberGroupSeparator = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает шаблон формата для положительных значений денежных сумм.
    /// </summary>
    /// <returns>
    ///   Шаблон формата для положительных значений денежных сумм.
    ///    По умолчанию для свойства <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> задано значение 0, представляющее "$n", где "$" — это <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" />, а <paramref name="n" /> — число.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Для свойства задано значение меньше 0 или больше 3.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства задается значение, а объект <see cref="T:System.Globalization.NumberFormatInfo" /> доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public int CurrencyPositivePattern
    {
      [__DynamicallyInvokable] get
      {
        return this.currencyPositivePattern;
      }
      [__DynamicallyInvokable] set
      {
        switch (value)
        {
          case 0:
          case 1:
          case 2:
          case 3:
            this.VerifyWritable();
            this.currencyPositivePattern = value;
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof (CurrencyPositivePattern), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 3));
        }
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, представляющую плюс бесконечность.
    /// </summary>
    /// <returns>
    ///   Строка, представляющая плюс бесконечность.
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> ‏‏равно "Infinity".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public string PositiveInfinitySymbol
    {
      [__DynamicallyInvokable] get
      {
        return this.positiveInfinitySymbol;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (PositiveInfinitySymbol), Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.positiveInfinitySymbol = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, указывающую, что соответствующее число является положительным.
    /// </summary>
    /// <returns>
    ///   Строка, указывающая, что соответствующее число является положительным.
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> равно "+".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   В операции задания должно быть присвоено значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public string PositiveSign
    {
      [__DynamicallyInvokable] get
      {
        return this.positiveSign;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (PositiveSign), Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.positiveSign = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает количество десятичных разрядов, используемое в значениях процентов.
    /// </summary>
    /// <returns>
    ///   Число десятичных разрядов, используемое в значениях процентов.
    ///    Значение по умолчанию для аргумента <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> равно 2.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Для свойства задано значение меньше 0 или больше 99.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public int PercentDecimalDigits
    {
      [__DynamicallyInvokable] get
      {
        return this.percentDecimalDigits;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 99)
          throw new ArgumentOutOfRangeException(nameof (PercentDecimalDigits), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 99));
        this.VerifyWritable();
        this.percentDecimalDigits = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, используемую в качестве десятичного разделителя в значениях процентов.
    /// </summary>
    /// <returns>
    ///   Строка, используемая в качестве десятичного разделителя в значениях процентов.
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> равно ".".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства задается значение, а объект <see cref="T:System.Globalization.NumberFormatInfo" /> доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Для свойства задается пустая строка.
    /// </exception>
    [__DynamicallyInvokable]
    public string PercentDecimalSeparator
    {
      [__DynamicallyInvokable] get
      {
        return this.percentDecimalSeparator;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyDecimalSeparator(value, nameof (PercentDecimalSeparator));
        this.percentDecimalSeparator = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, разделяющую разряды в целой части десятичной дроби в значениях процентов.
    /// </summary>
    /// <returns>
    ///   Строка, разделяющая разряды в целой части десятичной дроби в значениях процентов.
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> равно "-".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public string PercentGroupSeparator
    {
      [__DynamicallyInvokable] get
      {
        return this.percentGroupSeparator;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyGroupSeparator(value, nameof (PercentGroupSeparator));
        this.percentGroupSeparator = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, используемую в качестве знака процентов.
    /// </summary>
    /// <returns>
    ///   Строка, используемая в качестве знака процентов.
    ///    Значение по умолчанию для объекта <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> равно "%".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public string PercentSymbol
    {
      [__DynamicallyInvokable] get
      {
        return this.percentSymbol;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (PercentSymbol), Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.percentSymbol = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, используемую в качестве знака промилле.
    /// </summary>
    /// <returns>
    ///   Строка, используемая в качестве знака промилле.
    ///    Значением по умолчанию для <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> является "‰", что соответствует символу Юникода U+2030.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задается значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство установлено и <see cref="T:System.Globalization.NumberFormatInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public string PerMilleSymbol
    {
      [__DynamicallyInvokable] get
      {
        return this.perMilleSymbol;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (PerMilleSymbol), Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.perMilleSymbol = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает массив строк собственных цифр, эквивалентных арабским цифрам от 0 до 9.
    /// </summary>
    /// <returns>
    ///   Массив строк, содержащий собственный эквивалент арабских цифр от 0 до 9.
    ///    Значение по умолчанию — массив, включающий элементы "0", "1", "2", "3", "4", "5", "6", "7", "8" и "9".
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий объект <see cref="T:System.Globalization.NumberFormatInfo" /> доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   В операции задания значением является <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   В операции над множеством элемент массива значений является <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   В операции над множеством массив значений не содержит 10 элементов.
    /// 
    ///   -или-
    /// 
    ///   В операции над множеством элемент массива значений не содержит ни один объект <see cref="T:System.Char" />, ни пару объектов <see cref="T:System.Char" />, составляющих суррогатную пару.
    /// 
    ///   -или-
    /// 
    ///   В наборе операций элемент массива значений не является цифрой, как определяется Unicode Standard.
    ///    То есть цифра в элементе массива не имеет значения общей категории Юникода <see langword="Number, Decimal Digit" /> (Nd).
    /// 
    ///   -или-
    /// 
    ///   В операции над множеством числовое значение элемента в массиве значений не соответствует положению элемента в массиве.
    ///    То есть элемент с индексом 0, который является первым элементом массива, не имеет числового значения 0, или элемент с индексом 1 не имеет числового значения 1.
    /// </exception>
    [ComVisible(false)]
    public string[] NativeDigits
    {
      get
      {
        return (string[]) this.nativeDigits.Clone();
      }
      set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyNativeDigits(value, nameof (NativeDigits));
        this.nativeDigits = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, определяющее, каким образом а графическом интерфейсе пользователя должны отображаться фигуры цифр.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, указывающее фигуру цифры, связанную с языком и региональными параметрами.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий объект <see cref="T:System.Globalization.NumberFormatInfo" /> доступен только для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение в операции задания не является допустимым значением <see cref="T:System.Globalization.DigitShapes" />.
    /// </exception>
    [ComVisible(false)]
    public DigitShapes DigitSubstitution
    {
      get
      {
        return (DigitShapes) this.digitSubstitution;
      }
      set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyDigitSubstitution(value, nameof (DigitSubstitution));
        this.digitSubstitution = (int) value;
      }
    }

    /// <summary>
    ///   Возвращает объект указанного типа, предоставляющий службу форматирования чисел.
    /// </summary>
    /// <param name="formatType">
    ///   <see cref="T:System.Type" /> требуемой службы форматирования.
    /// </param>
    /// <returns>
    ///   Текущий объект <see cref="T:System.Globalization.NumberFormatInfo" />, если параметр <paramref name="formatType" /> совпадает с типом текущего объекта <see cref="T:System.Globalization.NumberFormatInfo" />; в противном случае — значение <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    public object GetFormat(Type formatType)
    {
      if (!(formatType == typeof (NumberFormatInfo)))
        return (object) null;
      return (object) this;
    }

    /// <summary>
    ///   Возвращает программу-оболочку <see cref="T:System.Globalization.NumberFormatInfo" />, доступную только для чтения.
    /// </summary>
    /// <param name="nfi">
    ///   Класс <see cref="T:System.Globalization.NumberFormatInfo" />, для которого создается оболочка.
    /// </param>
    /// <returns>
    ///   Доступная только для чтения программа-оболочка <see cref="T:System.Globalization.NumberFormatInfo" /> для параметра <paramref name="nfi" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="nfi" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static NumberFormatInfo ReadOnly(NumberFormatInfo nfi)
    {
      if (nfi == null)
        throw new ArgumentNullException(nameof (nfi));
      if (nfi.IsReadOnly)
        return nfi;
      NumberFormatInfo numberFormatInfo = (NumberFormatInfo) nfi.MemberwiseClone();
      numberFormatInfo.isReadOnly = true;
      return numberFormatInfo;
    }

    internal static void ValidateParseStyleInteger(NumberStyles style)
    {
      if ((style & ~(NumberStyles.Any | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNumberStyles"), nameof (style));
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None && (style & ~NumberStyles.HexNumber) != NumberStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidHexStyle"));
    }

    internal static void ValidateParseStyleFloatingPoint(NumberStyles style)
    {
      if ((style & ~(NumberStyles.Any | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNumberStyles"), nameof (style));
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_HexStyleNotSupported"));
    }
  }
}
