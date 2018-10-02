// Decompiled with JetBrains decompiler
// Type: System.Globalization.TextInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Globalization
{
  /// <summary>
  ///   Определяет свойства и поведение текста, характерные для той или иной системы письма, например регистр.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class TextInfo : ICloneable, IDeserializationCallback
  {
    [OptionalField(VersionAdded = 2)]
    private string m_listSeparator;
    [OptionalField(VersionAdded = 2)]
    private bool m_isReadOnly;
    [OptionalField(VersionAdded = 3)]
    private string m_cultureName;
    [NonSerialized]
    private CultureData m_cultureData;
    [NonSerialized]
    private string m_textInfoName;
    [NonSerialized]
    private IntPtr m_dataHandle;
    [NonSerialized]
    private IntPtr m_handleOrigin;
    [NonSerialized]
    private TextInfo.Tristate m_IsAsciiCasingSameAsInvariant;
    internal static volatile TextInfo s_Invariant;
    [OptionalField(VersionAdded = 2)]
    private string customCultureName;
    [OptionalField(VersionAdded = 1)]
    internal int m_nDataItem;
    [OptionalField(VersionAdded = 1)]
    internal bool m_useUserOverride;
    [OptionalField(VersionAdded = 1)]
    internal int m_win32LangID;
    private const int wordSeparatorMask = 536672256;

    internal static TextInfo Invariant
    {
      get
      {
        if (TextInfo.s_Invariant == null)
          TextInfo.s_Invariant = new TextInfo(CultureData.Invariant);
        return TextInfo.s_Invariant;
      }
    }

    internal TextInfo(CultureData cultureData)
    {
      this.m_cultureData = cultureData;
      this.m_cultureName = this.m_cultureData.CultureName;
      this.m_textInfoName = this.m_cultureData.STEXTINFO;
      IntPtr handleOrigin;
      this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_textInfoName, out handleOrigin);
      this.m_handleOrigin = handleOrigin;
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.m_cultureData = (CultureData) null;
      this.m_cultureName = (string) null;
    }

    private void OnDeserialized()
    {
      if (this.m_cultureData != null)
        return;
      if (this.m_cultureName == null)
        this.m_cultureName = this.customCultureName == null ? (this.m_win32LangID != 0 ? CultureInfo.GetCultureInfo(this.m_win32LangID).m_cultureData.CultureName : "ar-SA") : this.customCultureName;
      this.m_cultureData = CultureInfo.GetCultureInfo(this.m_cultureName).m_cultureData;
      this.m_textInfoName = this.m_cultureData.STEXTINFO;
      IntPtr handleOrigin;
      this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_textInfoName, out handleOrigin);
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
      this.m_useUserOverride = false;
      this.customCultureName = this.m_cultureName;
      this.m_win32LangID = CultureInfo.GetCultureInfo(this.m_cultureName).LCID;
    }

    internal static int GetHashCodeOrdinalIgnoreCase(string s)
    {
      return TextInfo.GetHashCodeOrdinalIgnoreCase(s, false, 0L);
    }

    internal static int GetHashCodeOrdinalIgnoreCase(string s, bool forceRandomizedHashing, long additionalEntropy)
    {
      return TextInfo.Invariant.GetCaseInsensitiveHashCode(s, forceRandomizedHashing, additionalEntropy);
    }

    [SecuritySafeCritical]
    internal static bool TryFastFindStringOrdinalIgnoreCase(int searchFlags, string source, int startIndex, string value, int count, ref int foundIndex)
    {
      return TextInfo.InternalTryFindStringOrdinalIgnoreCase(searchFlags, source, count, startIndex, value, value.Length, ref foundIndex);
    }

    [SecuritySafeCritical]
    internal static int CompareOrdinalIgnoreCase(string str1, string str2)
    {
      return TextInfo.InternalCompareStringOrdinalIgnoreCase(str1, 0, str2, 0, str1.Length, str2.Length);
    }

    [SecuritySafeCritical]
    internal static int CompareOrdinalIgnoreCaseEx(string strA, int indexA, string strB, int indexB, int lengthA, int lengthB)
    {
      return TextInfo.InternalCompareStringOrdinalIgnoreCase(strA, indexA, strB, indexB, lengthA, lengthB);
    }

    internal static int IndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
    {
      if (source.Length == 0 && value.Length == 0)
        return 0;
      int foundIndex = -1;
      if (TextInfo.TryFastFindStringOrdinalIgnoreCase(4194304, source, startIndex, value, count, ref foundIndex))
        return foundIndex;
      for (int index = startIndex + count - value.Length; startIndex <= index; ++startIndex)
      {
        if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
          return startIndex;
      }
      return -1;
    }

    internal static int LastIndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
    {
      if (value.Length == 0)
        return startIndex;
      int foundIndex = -1;
      if (TextInfo.TryFastFindStringOrdinalIgnoreCase(8388608, source, startIndex, value, count, ref foundIndex))
        return foundIndex;
      int num = startIndex - count + 1;
      if (value.Length > 0)
        startIndex -= value.Length - 1;
      for (; startIndex >= num; --startIndex)
      {
        if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
          return startIndex;
      }
      return -1;
    }

    /// <summary>
    ///   Получает кодовую страницу в Американский национальный институт стандартов (ANSI), использует система письма, представленная текущим <see cref="T:System.Globalization.TextInfo" />.
    /// </summary>
    /// <returns>
    ///   Кодовая страница ANSI, использует система письма, представленная текущим <see cref="T:System.Globalization.TextInfo" />.
    /// </returns>
    public virtual int ANSICodePage
    {
      get
      {
        return this.m_cultureData.IDEFAULTANSICODEPAGE;
      }
    }

    /// <summary>
    ///   Возвращает кодовую страницу изготовителя оборудования (OEM), использует система письма, представленная текущим <see cref="T:System.Globalization.TextInfo" />.
    /// </summary>
    /// <returns>
    ///   Кодовая страница OEM, использует система письма, представленная текущим <see cref="T:System.Globalization.TextInfo" />.
    /// </returns>
    public virtual int OEMCodePage
    {
      get
      {
        return this.m_cultureData.IDEFAULTOEMCODEPAGE;
      }
    }

    /// <summary>
    ///   Возвращает кодовую страницу Macintosh, использует система письма, представленная текущим <see cref="T:System.Globalization.TextInfo" />.
    /// </summary>
    /// <returns>
    ///   Macintosh кодовая страница, используемая в системе письма, представленная текущим <see cref="T:System.Globalization.TextInfo" />.
    /// </returns>
    public virtual int MacCodePage
    {
      get
      {
        return this.m_cultureData.IDEFAULTMACCODEPAGE;
      }
    }

    /// <summary>
    ///   Возвращает кодовую страницу расширенных двоично-десятичном Interchange кода (EBCDIC), использует система письма, представленная текущим <see cref="T:System.Globalization.TextInfo" />.
    /// </summary>
    /// <returns>
    ///   Кодовой странице EBCDIC, использует система письма, представленная текущим <see cref="T:System.Globalization.TextInfo" />.
    /// </returns>
    public virtual int EBCDICCodePage
    {
      get
      {
        return this.m_cultureData.IDEFAULTEBCDICCODEPAGE;
      }
    }

    /// <summary>
    ///   Возвращает идентификатор языка и региональных параметров для языка и региональных параметров, связанных с текущим <see cref="T:System.Globalization.TextInfo" /> объекта.
    /// </summary>
    /// <returns>
    ///   Число, определяющее язык и региональные параметры, от которого текущий <see cref="T:System.Globalization.TextInfo" /> был создан объект.
    /// </returns>
    [ComVisible(false)]
    public int LCID
    {
      get
      {
        return CultureInfo.GetCultureInfo(this.m_textInfoName).LCID;
      }
    }

    /// <summary>
    ///   Возвращает имя языка и региональных параметров, связанных с текущим <see cref="T:System.Globalization.TextInfo" /> объекта.
    /// </summary>
    /// <returns>Имя языка и региональных параметров.</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string CultureName
    {
      [__DynamicallyInvokable] get
      {
        return this.m_textInfoName;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий <see cref="T:System.Globalization.TextInfo" /> объект доступен только для чтения.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если текущий <see cref="T:System.Globalization.TextInfo" /> объект только для чтения; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public bool IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.m_isReadOnly;
      }
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего <see cref="T:System.Globalization.TextInfo" /> объекта.
    /// </summary>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.Object" /> является копией текущего <see cref="T:System.Globalization.TextInfo" /> объекта.
    /// </returns>
    [ComVisible(false)]
    public virtual object Clone()
    {
      object obj = this.MemberwiseClone();
      ((TextInfo) obj).SetReadOnlyState(false);
      return obj;
    }

    /// <summary>
    ///   Возвращает только для чтения версию указанного <see cref="T:System.Globalization.TextInfo" /> объекта.
    /// </summary>
    /// <param name="textInfo">
    ///   Объект <see cref="T:System.Globalization.TextInfo" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Globalization.TextInfo" /> Объекта, заданного параметром <paramref name="textInfo" /> параметр, если <paramref name="textInfo" /> доступен только для чтения.
    /// 
    ///   -или-
    /// 
    ///   Только для чтения копией по <see cref="T:System.Globalization.TextInfo" /> объекта, заданного параметром <paramref name="textInfo" />, если <paramref name="textInfo" /> не только для чтения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="textInfo" /> имеет значение NULL.
    /// </exception>
    [ComVisible(false)]
    public static TextInfo ReadOnly(TextInfo textInfo)
    {
      if (textInfo == null)
        throw new ArgumentNullException(nameof (textInfo));
      if (textInfo.IsReadOnly)
        return textInfo;
      TextInfo textInfo1 = (TextInfo) textInfo.MemberwiseClone();
      textInfo1.SetReadOnlyState(true);
      return textInfo1;
    }

    private void VerifyWritable()
    {
      if (this.m_isReadOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
    }

    internal void SetReadOnlyState(bool readOnly)
    {
      this.m_isReadOnly = readOnly;
    }

    /// <summary>
    ///   Возвращает или задает строку, разделяющую элементы в списке.
    /// </summary>
    /// <returns>Строка, разделяющая элементы в списке.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение в наборе операций — null.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   В наборе операций текущим <see cref="T:System.Globalization.TextInfo" /> объект доступен только для чтения.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual string ListSeparator
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        if (this.m_listSeparator == null)
          this.m_listSeparator = this.m_cultureData.SLIST;
        return this.m_listSeparator;
      }
      [ComVisible(false), __DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value), Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.m_listSeparator = value;
      }
    }

    /// <summary>Преобразует заданный знак в нижний регистр.</summary>
    /// <param name="c">Знак для преобразования в нижний регистр.</param>
    /// <returns>Заданный знак, преобразуемый в нижний регистр.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual char ToLower(char c)
    {
      if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
        return TextInfo.ToLowerAsciiInvariant(c);
      return TextInfo.InternalChangeCaseChar(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, c, false);
    }

    /// <summary>Преобразует заданную строку в нижний регистр.</summary>
    /// <param name="str">
    ///   Строка для преобразования в нижний регистр.
    /// </param>
    /// <returns>Заданная строка, преобразованная в нижний регистр.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="str" /> имеет значение NULL.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual string ToLower(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      return TextInfo.InternalChangeCaseString(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, str, false);
    }

    private static char ToLowerAsciiInvariant(char c)
    {
      if ('A' <= c && c <= 'Z')
        c |= ' ';
      return c;
    }

    /// <summary>Преобразует заданный знак в верхний регистр.</summary>
    /// <param name="c">Знак для преобразования в верхний регистр.</param>
    /// <returns>Заданный знак, преобразуемый в верхний регистр.</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual char ToUpper(char c)
    {
      if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
        return TextInfo.ToUpperAsciiInvariant(c);
      return TextInfo.InternalChangeCaseChar(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, c, true);
    }

    /// <summary>Преобразует заданную строку в верхний регистр.</summary>
    /// <param name="str">
    ///   Строка для преобразования в верхний регистр.
    /// </param>
    /// <returns>Заданная строка, преобразуемая в верхний регистр.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="str" /> имеет значение NULL.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual string ToUpper(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      return TextInfo.InternalChangeCaseString(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, str, true);
    }

    private static char ToUpperAsciiInvariant(char c)
    {
      if ('a' <= c && c <= 'z')
        c &= '\xFFDF';
      return c;
    }

    private static bool IsAscii(char c)
    {
      return c < '\x0080';
    }

    private bool IsAsciiCasingSameAsInvariant
    {
      get
      {
        if (this.m_IsAsciiCasingSameAsInvariant == TextInfo.Tristate.NotInitialized)
          this.m_IsAsciiCasingSameAsInvariant = CultureInfo.GetCultureInfo(this.m_textInfoName).CompareInfo.Compare("abcdefghijklmnopqrstuvwxyz", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CompareOptions.IgnoreCase) == 0 ? TextInfo.Tristate.True : TextInfo.Tristate.False;
        return this.m_IsAsciiCasingSameAsInvariant == TextInfo.Tristate.True;
      }
    }

    /// <summary>
    ///   Определяет, представляет ли заданный объект ту же систему письма, что и текущий <see cref="T:System.Globalization.TextInfo" /> объекта.
    /// </summary>
    /// <param name="obj">
    ///   Объект, который требуется сравнить с текущим объектом <see cref="T:System.Globalization.TextInfo" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> представляет ту же систему письма, что и текущий <see cref="T:System.Globalization.TextInfo" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      TextInfo textInfo = obj as TextInfo;
      if (textInfo != null)
        return this.CultureName.Equals(textInfo.CultureName);
      return false;
    }

    /// <summary>
    ///   Служит хэш-функцией текущего класса <see cref="T:System.Globalization.TextInfo" /> для использования в алгоритмах и структурах данных хеширования, например в хэш-таблице.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Globalization.TextInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.CultureName.GetHashCode();
    }

    /// <summary>
    ///   Возвращает строку, которая представляет текущий объект <see cref="T:System.Globalization.TextInfo" />.
    /// </summary>
    /// <returns>
    ///   Строка, представляющая текущий объект <see cref="T:System.Globalization.TextInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return "TextInfo - " + this.m_cultureData.CultureName;
    }

    /// <summary>
    ///   Преобразует заданную строку в заглавные символы (за исключением слов полностью в верхнем регистре, которые считаются сокращениями).
    /// </summary>
    /// <param name="str">Строка для преобразования в прописные.</param>
    /// <returns>Заданная строка, преобразуемая в заглавные символы.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    public string ToTitleCase(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      if (str.Length == 0)
        return str;
      StringBuilder result = new StringBuilder();
      string str1 = (string) null;
      int index1;
      for (int index2 = 0; index2 < str.Length; index2 = index1 + 1)
      {
        int charLength;
        UnicodeCategory unicodeCategory1 = CharUnicodeInfo.InternalGetUnicodeCategory(str, index2, out charLength);
        if (char.CheckLetter(unicodeCategory1))
        {
          index1 = this.AddTitlecaseLetter(ref result, ref str, index2, charLength) + 1;
          int startIndex = index1;
          bool flag = unicodeCategory1 == UnicodeCategory.LowercaseLetter;
          while (index1 < str.Length)
          {
            UnicodeCategory unicodeCategory2 = CharUnicodeInfo.InternalGetUnicodeCategory(str, index1, out charLength);
            if (TextInfo.IsLetterCategory(unicodeCategory2))
            {
              if (unicodeCategory2 == UnicodeCategory.LowercaseLetter)
                flag = true;
              index1 += charLength;
            }
            else if (str[index1] == '\'')
            {
              ++index1;
              if (flag)
              {
                if (str1 == null)
                  str1 = this.ToLower(str);
                result.Append(str1, startIndex, index1 - startIndex);
              }
              else
                result.Append(str, startIndex, index1 - startIndex);
              startIndex = index1;
              flag = true;
            }
            else if (!TextInfo.IsWordSeparator(unicodeCategory2))
              index1 += charLength;
            else
              break;
          }
          int count = index1 - startIndex;
          if (count > 0)
          {
            if (flag)
            {
              if (str1 == null)
                str1 = this.ToLower(str);
              result.Append(str1, startIndex, count);
            }
            else
              result.Append(str, startIndex, count);
          }
          if (index1 < str.Length)
            index1 = TextInfo.AddNonLetter(ref result, ref str, index1, charLength);
        }
        else
          index1 = TextInfo.AddNonLetter(ref result, ref str, index2, charLength);
      }
      return result.ToString();
    }

    private static int AddNonLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
    {
      if (charLen == 2)
      {
        result.Append(input[inputIndex++]);
        result.Append(input[inputIndex]);
      }
      else
        result.Append(input[inputIndex]);
      return inputIndex;
    }

    private int AddTitlecaseLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
    {
      if (charLen == 2)
      {
        result.Append(this.ToUpper(input.Substring(inputIndex, charLen)));
        ++inputIndex;
      }
      else
      {
        switch (input[inputIndex])
        {
          case 'Ǆ':
          case 'ǅ':
          case 'ǆ':
            result.Append('ǅ');
            break;
          case 'Ǉ':
          case 'ǈ':
          case 'ǉ':
            result.Append('ǈ');
            break;
          case 'Ǌ':
          case 'ǋ':
          case 'ǌ':
            result.Append('ǋ');
            break;
          case 'Ǳ':
          case 'ǲ':
          case 'ǳ':
            result.Append('ǲ');
            break;
          default:
            result.Append(this.ToUpper(input[inputIndex]));
            break;
        }
      }
      return inputIndex;
    }

    private static bool IsWordSeparator(UnicodeCategory category)
    {
      return (uint) (536672256 & 1 << (int) (category & (UnicodeCategory.Format | UnicodeCategory.Surrogate))) > 0U;
    }

    private static bool IsLetterCategory(UnicodeCategory uc)
    {
      if (uc != UnicodeCategory.UppercaseLetter && uc != UnicodeCategory.LowercaseLetter && (uc != UnicodeCategory.TitlecaseLetter && uc != UnicodeCategory.ModifierLetter))
        return uc == UnicodeCategory.OtherLetter;
      return true;
    }

    /// <summary>
    ///   Возвращает значение, указывающее ли текущий <see cref="T:System.Globalization.TextInfo" /> объект представляет систему письма, в которых текст пишется справа налево.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если текст пишется справа налево; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public bool IsRightToLeft
    {
      [__DynamicallyInvokable] get
      {
        return this.m_cultureData.IsRightToLeft;
      }
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
      this.OnDeserialized();
    }

    [SecuritySafeCritical]
    internal int GetCaseInsensitiveHashCode(string str)
    {
      return this.GetCaseInsensitiveHashCode(str, false, 0L);
    }

    [SecuritySafeCritical]
    internal int GetCaseInsensitiveHashCode(string str, bool forceRandomizedHashing, long additionalEntropy)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      return TextInfo.InternalGetCaseInsHash(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, str, forceRandomizedHashing, additionalEntropy);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern char InternalChangeCaseChar(IntPtr handle, IntPtr handleOrigin, string localeName, char ch, bool isToUpper);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern string InternalChangeCaseString(IntPtr handle, IntPtr handleOrigin, string localeName, string str, bool isToUpper);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int InternalGetCaseInsHash(IntPtr handle, IntPtr handleOrigin, string localeName, string str, bool forceRandomizedHashing, long additionalEntropy);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int InternalCompareStringOrdinalIgnoreCase(string string1, int index1, string string2, int index2, int length1, int length2);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InternalTryFindStringOrdinalIgnoreCase(int searchFlags, string source, int sourceCount, int startIndex, string target, int targetCount, ref int foundIndex);

    private enum Tristate : byte
    {
      NotInitialized,
      True,
      False,
    }
  }
}
