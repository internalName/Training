// Decompiled with JetBrains decompiler
// Type: System.Globalization.StringInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
  /// <summary>
  ///   Предоставляет функциональные возможности для разбиения строки на текстовые элементы и их последующего перебора.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class StringInfo
  {
    [OptionalField(VersionAdded = 2)]
    private string m_str;
    [NonSerialized]
    private int[] m_indexes;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.StringInfo" />.
    /// </summary>
    [__DynamicallyInvokable]
    public StringInfo()
      : this("")
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.StringInfo" /> в указанной строке.
    /// </summary>
    /// <param name="value">
    ///   Строка для инициализации объекта <see cref="T:System.Globalization.StringInfo" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringInfo(string value)
    {
      this.String = value;
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.m_str = string.Empty;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_str.Length != 0)
        return;
      this.m_indexes = (int[]) null;
    }

    /// <summary>
    ///   Указывает, равен ли текущий объект <see cref="T:System.Globalization.StringInfo" /> указанному объекту.
    /// </summary>
    /// <param name="value">Объект.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="value" /> параметр <see cref="T:System.Globalization.StringInfo" /> объекта и его <see cref="P:System.Globalization.StringInfo.String" /> равняется значению свойства <see cref="P:System.Globalization.StringInfo.String" /> это свойство <see cref="T:System.Globalization.StringInfo" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      StringInfo stringInfo = value as StringInfo;
      if (stringInfo != null)
        return this.m_str.Equals(stringInfo.m_str);
      return false;
    }

    /// <summary>
    ///   Вычисляет хэш-код для текущего значения <see cref="T:System.Globalization.StringInfo" /> объект.
    /// </summary>
    /// <returns>
    ///   На основе хэш-код 32-разрядного знакового целого числа в строковое значение <see cref="T:System.Globalization.StringInfo" /> объекта.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.m_str.GetHashCode();
    }

    private int[] Indexes
    {
      get
      {
        if (this.m_indexes == null && 0 < this.String.Length)
          this.m_indexes = StringInfo.ParseCombiningCharacters(this.String);
        return this.m_indexes;
      }
    }

    /// <summary>
    ///   Получает или задает значение текущего объекта <see cref="T:System.Globalization.StringInfo" />.
    /// </summary>
    /// <returns>
    ///   Строка, представляющая значение текущего объекта <see cref="T:System.Globalization.StringInfo" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   В операции задания значением является <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public string String
    {
      [__DynamicallyInvokable] get
      {
        return this.m_str;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (String), Environment.GetResourceString("ArgumentNull_String"));
        this.m_str = value;
        this.m_indexes = (int[]) null;
      }
    }

    /// <summary>
    ///   Получает число текстовых элементов в текущем объекте <see cref="T:System.Globalization.StringInfo" />.
    /// </summary>
    /// <returns>
    ///   Количество базовых символов, суррогатных пар и последовательностей несамостоятельных знаков в этом объекте <see cref="T:System.Globalization.StringInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public int LengthInTextElements
    {
      [__DynamicallyInvokable] get
      {
        if (this.Indexes == null)
          return 0;
        return this.Indexes.Length;
      }
    }

    /// <summary>
    ///   Извлекает подстроку элементов текста из текущего <see cref="T:System.Globalization.StringInfo" /> объекта, начиная с указанного элемента текста и продолжая до последнего текстового элемента.
    /// </summary>
    /// <param name="startingTextElement">
    ///   Отсчитываемый от нуля индекс текстового элемента в этом <see cref="T:System.Globalization.StringInfo" /> объекта.
    /// </param>
    /// <returns>
    ///   Подстроку элементов текста в этом <see cref="T:System.Globalization.StringInfo" /> объекте, начиная с индекса элемента текста, указанного параметром <paramref name="startingTextElement" /> параметр и до последнего элемента текста в этом объекте.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="startingTextElement" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Строка, являющаяся значением текущего <see cref="T:System.Globalization.StringInfo" /> объект является пустой строкой («»).
    /// </exception>
    public string SubstringByTextElements(int startingTextElement)
    {
      if (this.Indexes != null)
        return this.SubstringByTextElements(startingTextElement, this.Indexes.Length - startingTextElement);
      if (startingTextElement < 0)
        throw new ArgumentOutOfRangeException(nameof (startingTextElement), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      throw new ArgumentOutOfRangeException(nameof (startingTextElement), Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
    }

    /// <summary>
    ///   Извлекает подстроку элементов текста из текущего <see cref="T:System.Globalization.StringInfo" /> объекта, начиная с указанного элемента текста и продолжая до указанного количества элементов текста.
    /// </summary>
    /// <param name="startingTextElement">
    ///   Отсчитываемый от нуля индекс текстового элемента в этом <see cref="T:System.Globalization.StringInfo" /> объекта.
    /// </param>
    /// <param name="lengthInTextElements">
    ///   Количество извлекаемых элементов текста.
    /// </param>
    /// <returns>
    ///   Подстроку элементов текста в этом <see cref="T:System.Globalization.StringInfo" /> объекта.
    ///    Подстрока состоит из числа элементов текста, определяемое параметром <paramref name="lengthInTextElements" /> параметр и начинается с индекса элемента текста, указанного параметром <paramref name="startingTextElement" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="startingTextElement" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startingTextElement" /> больше или равно длине строки, который является значением текущего <see cref="T:System.Globalization.StringInfo" /> объекта.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="lengthInTextElements" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   Строка, являющаяся значением текущего <see cref="T:System.Globalization.StringInfo" /> объект является пустой строкой («»).
    /// 
    ///   -или-
    /// 
    ///   <paramref name="startingTextElement" /> + <paramref name="lengthInTextElements" /> Задайте индекс, больше, чем число элементов текста в этом <see cref="T:System.Globalization.StringInfo" /> объекта.
    /// </exception>
    public string SubstringByTextElements(int startingTextElement, int lengthInTextElements)
    {
      if (startingTextElement < 0)
        throw new ArgumentOutOfRangeException(nameof (startingTextElement), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (this.String.Length == 0 || startingTextElement >= this.Indexes.Length)
        throw new ArgumentOutOfRangeException(nameof (startingTextElement), Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
      if (lengthInTextElements < 0)
        throw new ArgumentOutOfRangeException(nameof (lengthInTextElements), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (startingTextElement > this.Indexes.Length - lengthInTextElements)
        throw new ArgumentOutOfRangeException(nameof (lengthInTextElements), Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
      int index = this.Indexes[startingTextElement];
      if (startingTextElement + lengthInTextElements == this.Indexes.Length)
        return this.String.Substring(index);
      return this.String.Substring(index, this.Indexes[lengthInTextElements + startingTextElement] - index);
    }

    /// <summary>
    ///   Возвращает первый текстовый элемент в указанной строке.
    /// </summary>
    /// <param name="str">
    ///   Строка, из которой нужно получить текстовый элемент.
    /// </param>
    /// <returns>
    ///   Строка, содержащая первый текстовый элемент в указанной строке.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string GetNextTextElement(string str)
    {
      return StringInfo.GetNextTextElement(str, 0);
    }

    internal static int GetCurrentTextElementLen(string str, int index, int len, ref UnicodeCategory ucCurrent, ref int currentCharCount)
    {
      if (index + currentCharCount == len)
        return currentCharCount;
      int charLength;
      UnicodeCategory unicodeCategory1 = CharUnicodeInfo.InternalGetUnicodeCategory(str, index + currentCharCount, out charLength);
      if (CharUnicodeInfo.IsCombiningCategory(unicodeCategory1) && !CharUnicodeInfo.IsCombiningCategory(ucCurrent) && (ucCurrent != UnicodeCategory.Format && ucCurrent != UnicodeCategory.Control) && (ucCurrent != UnicodeCategory.OtherNotAssigned && ucCurrent != UnicodeCategory.Surrogate))
      {
        int num = index;
        index += currentCharCount + charLength;
        while (index < len)
        {
          UnicodeCategory unicodeCategory2 = CharUnicodeInfo.InternalGetUnicodeCategory(str, index, out charLength);
          if (!CharUnicodeInfo.IsCombiningCategory(unicodeCategory2))
          {
            ucCurrent = unicodeCategory2;
            currentCharCount = charLength;
            break;
          }
          index += charLength;
        }
        return index - num;
      }
      int num1 = currentCharCount;
      ucCurrent = unicodeCategory1;
      currentCharCount = charLength;
      return num1;
    }

    /// <summary>
    ///   Возвращает текстовый элемент по указанному индексу в указанной строке.
    /// </summary>
    /// <param name="str">
    ///   Строка, из которой нужно получить текстовый элемент.
    /// </param>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, с которого начинается элемент текста.
    /// </param>
    /// <returns>
    ///   Строка, содержащая элемент текста по указанному индексу в указанной строке.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов для <paramref name="str" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string GetNextTextElement(string str, int index)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      int length = str.Length;
      if (index < 0 || index >= length)
      {
        if (index == length)
          return string.Empty;
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }
      int charLength;
      UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index, out charLength);
      return str.Substring(index, StringInfo.GetCurrentTextElementLen(str, index, length, ref unicodeCategory, ref charLength));
    }

    /// <summary>
    ///   Возвращает перечислитель, выполняющий итерацию по текстовым элементам всей строки.
    /// </summary>
    /// <param name="str">Строка для итерации.</param>
    /// <returns>
    ///   Объект <see cref="T:System.Globalization.TextElementEnumerator" /> для всей строки.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static TextElementEnumerator GetTextElementEnumerator(string str)
    {
      return StringInfo.GetTextElementEnumerator(str, 0);
    }

    /// <summary>
    ///   Возвращает перечислитель, выполняющий итерацию по текстовым элементам строки, начиная с указанного индекса.
    /// </summary>
    /// <param name="str">Строка для итерации.</param>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, с которого необходимо начать итерацию.
    /// </param>
    /// <returns>
    ///   Значение <see cref="T:System.Globalization.TextElementEnumerator" /> для строки, начиная с <paramref name="index" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> находится вне диапазона допустимых индексов для <paramref name="str" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static TextElementEnumerator GetTextElementEnumerator(string str, int index)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      int length = str.Length;
      if (index < 0 || index > length)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return new TextElementEnumerator(str, index, length);
    }

    /// <summary>
    ///   Возвращает индексы каждого базового знака, старший суррогат или управляющий символ в пределах указанной строки.
    /// </summary>
    /// <param name="str">Строка для поиска.</param>
    /// <returns>
    ///   Массив целых чисел, который содержит отсчитываемые с нуля индексы каждого базового знака, старший символ-заместитель или управляющий символ в пределах указанной строки.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int[] ParseCombiningCharacters(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      int length1 = str.Length;
      int[] numArray1 = new int[length1];
      if (length1 == 0)
        return numArray1;
      int length2 = 0;
      int index = 0;
      int charLength;
      UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, 0, out charLength);
      while (index < length1)
      {
        numArray1[length2++] = index;
        index += StringInfo.GetCurrentTextElementLen(str, index, length1, ref unicodeCategory, ref charLength);
      }
      if (length2 >= length1)
        return numArray1;
      int[] numArray2 = new int[length2];
      Array.Copy((Array) numArray1, (Array) numArray2, length2);
      return numArray2;
    }
  }
}
