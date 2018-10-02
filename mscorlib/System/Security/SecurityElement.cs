// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityElement
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;

namespace System.Security
{
  /// <summary>
  ///   Представляет объектную модель XML для кодирования объектов безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SecurityElement : ISecurityElementFactory
  {
    private static readonly char[] s_tagIllegalCharacters = new char[3]
    {
      ' ',
      '<',
      '>'
    };
    private static readonly char[] s_textIllegalCharacters = new char[2]
    {
      '<',
      '>'
    };
    private static readonly char[] s_valueIllegalCharacters = new char[3]
    {
      '<',
      '>',
      '"'
    };
    private static readonly string[] s_escapeStringPairs = new string[10]
    {
      "<",
      "&lt;",
      ">",
      "&gt;",
      "\"",
      "&quot;",
      "'",
      "&apos;",
      "&",
      "&amp;"
    };
    private static readonly char[] s_escapeChars = new char[5]
    {
      '<',
      '>',
      '"',
      '\'',
      '&'
    };
    internal string m_strTag;
    internal string m_strText;
    private ArrayList m_lChildren;
    internal ArrayList m_lAttributes;
    internal SecurityElementType m_type;
    private const string s_strIndent = "   ";
    private const int c_AttributesTypical = 8;
    private const int c_ChildrenTypical = 1;

    internal SecurityElement()
    {
    }

    SecurityElement ISecurityElementFactory.CreateSecurityElement()
    {
      return this;
    }

    string ISecurityElementFactory.GetTag()
    {
      return this.Tag;
    }

    object ISecurityElementFactory.Copy()
    {
      return (object) this.Copy();
    }

    string ISecurityElementFactory.Attribute(string attributeName)
    {
      return this.Attribute(attributeName);
    }

    /// <summary>
    ///   Создает элемент безопасности на основе строки в XML-кодировке.
    /// </summary>
    /// <param name="xml">
    ///   Строка в XML-кодировке, на основе которой следует создать элемент безопасности.
    /// </param>
    /// <returns>
    ///   Элемент <see cref="T:System.Security.SecurityElement" />, созданный на основе XML.
    /// </returns>
    /// <exception cref="T:System.Security.XmlSyntaxException">
    ///   <paramref name="xml" /> содержит один или несколько символов одинарных кавычек.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="xml" /> имеет значение <see langword=" null" />.
    /// </exception>
    public static SecurityElement FromString(string xml)
    {
      if (xml == null)
        throw new ArgumentNullException(nameof (xml));
      return new Parser(xml).GetTopElement();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.SecurityElement" /> класса с указанным тегом.
    /// </summary>
    /// <param name="tag">Имя тега элемента XML.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="tag" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tag" /> Параметр недопустим в XML.
    /// </exception>
    public SecurityElement(string tag)
    {
      if (tag == null)
        throw new ArgumentNullException(nameof (tag));
      if (!SecurityElement.IsValidTag(tag))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), (object) tag));
      this.m_strTag = tag;
      this.m_strText = (string) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.SecurityElement" /> класса с указанным тегом и текстом.
    /// </summary>
    /// <param name="tag">Имя тега элемента XML.</param>
    /// <param name="text">Текстовое содержимое элемента.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="tag" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="tag" /> Параметр или <paramref name="text" /> параметр недопустим в XML.
    /// </exception>
    public SecurityElement(string tag, string text)
    {
      if (tag == null)
        throw new ArgumentNullException(nameof (tag));
      if (!SecurityElement.IsValidTag(tag))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), (object) tag));
      if (text != null && !SecurityElement.IsValidText(text))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementText"), (object) text));
      this.m_strTag = tag;
      this.m_strText = text;
    }

    /// <summary>Возвращает или задает имя тега элемента XML.</summary>
    /// <returns>Имя тега элемента XML.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Тег <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Тег не является допустимым в XML.
    /// </exception>
    public string Tag
    {
      get
      {
        return this.m_strTag;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (Tag));
        if (!SecurityElement.IsValidTag(value))
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), (object) value));
        this.m_strTag = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает атрибуты элемента XML в виде пар имя значение.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.Hashtable" /> Объект для значений атрибутов элемента XML.
    /// </returns>
    /// <exception cref="T:System.InvalidCastException">
    ///   Имя или значение <see cref="T:System.Collections.Hashtable" /> Недопустимый объект.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Имя не является допустимым именем атрибута XML.
    /// </exception>
    public Hashtable Attributes
    {
      get
      {
        if (this.m_lAttributes == null || this.m_lAttributes.Count == 0)
          return (Hashtable) null;
        Hashtable hashtable = new Hashtable(this.m_lAttributes.Count / 2);
        int count = this.m_lAttributes.Count;
        int index = 0;
        while (index < count)
        {
          hashtable.Add(this.m_lAttributes[index], this.m_lAttributes[index + 1]);
          index += 2;
        }
        return hashtable;
      }
      set
      {
        if (value == null || value.Count == 0)
        {
          this.m_lAttributes = (ArrayList) null;
        }
        else
        {
          ArrayList arrayList = new ArrayList(value.Count);
          IDictionaryEnumerator enumerator = value.GetEnumerator();
          while (enumerator.MoveNext())
          {
            string key = (string) enumerator.Key;
            string str = (string) enumerator.Value;
            if (!SecurityElement.IsValidAttributeName(key))
              throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementName"), (object) (string) enumerator.Current));
            if (!SecurityElement.IsValidAttributeValue(str))
              throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementValue"), (object) (string) enumerator.Value));
            arrayList.Add((object) key);
            arrayList.Add((object) str);
          }
          this.m_lAttributes = arrayList;
        }
      }
    }

    /// <summary>Возвращает или задает текст внутри элемента XML.</summary>
    /// <returns>Значение текста внутри элемента XML.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Текст не является допустимым в XML.
    /// </exception>
    public string Text
    {
      get
      {
        return SecurityElement.Unescape(this.m_strText);
      }
      set
      {
        if (value == null)
        {
          this.m_strText = (string) null;
        }
        else
        {
          if (!SecurityElement.IsValidText(value))
            throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), (object) value));
          this.m_strText = value;
        }
      }
    }

    /// <summary>
    ///   Возвращает или задает массив дочерних элементов элемента XML.
    /// </summary>
    /// <returns>
    ///   Упорядоченные дочерние элементы элемента XML в виде элементов безопасности.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Дочерний элемент родительского узла XML равен <see langword="null" />.
    /// </exception>
    public ArrayList Children
    {
      get
      {
        this.ConvertSecurityElementFactories();
        return this.m_lChildren;
      }
      set
      {
        if (value != null)
        {
          foreach (object obj in value)
          {
            if (obj == null)
              throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Child"));
          }
        }
        this.m_lChildren = value;
      }
    }

    internal void ConvertSecurityElementFactories()
    {
      if (this.m_lChildren == null)
        return;
      for (int index = 0; index < this.m_lChildren.Count; ++index)
      {
        ISecurityElementFactory lChild = this.m_lChildren[index] as ISecurityElementFactory;
        if (lChild != null && !(this.m_lChildren[index] is SecurityElement))
          this.m_lChildren[index] = (object) lChild.CreateSecurityElement();
      }
    }

    internal ArrayList InternalChildren
    {
      get
      {
        return this.m_lChildren;
      }
    }

    internal void AddAttributeSafe(string name, string value)
    {
      if (this.m_lAttributes == null)
      {
        this.m_lAttributes = new ArrayList(8);
      }
      else
      {
        int count = this.m_lAttributes.Count;
        int index = 0;
        while (index < count)
        {
          if (string.Equals((string) this.m_lAttributes[index], name))
            throw new ArgumentException(Environment.GetResourceString("Argument_AttributeNamesMustBeUnique"));
          index += 2;
        }
      }
      this.m_lAttributes.Add((object) name);
      this.m_lAttributes.Add((object) value);
    }

    /// <summary>Добавляет атрибут имени и значения элемента XML.</summary>
    /// <param name="name">Имя атрибута.</param>
    /// <param name="value">Значение атрибута.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="name" /> Параметр или <paramref name="value" /> параметр <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> Параметр или <paramref name="value" /> параметр недопустим в XML.
    /// 
    ///   -или-
    /// 
    ///   Атрибут с именем, указанным <paramref name="name" /> параметр уже существует.
    /// </exception>
    public void AddAttribute(string name, string value)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (!SecurityElement.IsValidAttributeName(name))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementName"), (object) name));
      if (!SecurityElement.IsValidAttributeValue(value))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementValue"), (object) value));
      this.AddAttributeSafe(name, value);
    }

    /// <summary>Добавляет дочерний элемент в элемент XML.</summary>
    /// <param name="child">Добавляемый дочерний элемент.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="child" /> имеет значение <see langword="null" />.
    /// </exception>
    public void AddChild(SecurityElement child)
    {
      if (child == null)
        throw new ArgumentNullException(nameof (child));
      if (this.m_lChildren == null)
        this.m_lChildren = new ArrayList(1);
      this.m_lChildren.Add((object) child);
    }

    internal void AddChild(ISecurityElementFactory child)
    {
      if (child == null)
        throw new ArgumentNullException(nameof (child));
      if (this.m_lChildren == null)
        this.m_lChildren = new ArrayList(1);
      this.m_lChildren.Add((object) child);
    }

    internal void AddChildNoDuplicates(ISecurityElementFactory child)
    {
      if (child == null)
        throw new ArgumentNullException(nameof (child));
      if (this.m_lChildren == null)
      {
        this.m_lChildren = new ArrayList(1);
        this.m_lChildren.Add((object) child);
      }
      else
      {
        for (int index = 0; index < this.m_lChildren.Count; ++index)
        {
          if (this.m_lChildren[index] == child)
            return;
        }
        this.m_lChildren.Add((object) child);
      }
    }

    /// <summary>
    ///   Сравнивает два объекта элементов XML, для проверки на равенство.
    /// </summary>
    /// <param name="other">
    ///   Объект элемента XML с которым сравнивается текущий объект элемента XML.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если тег, имена атрибутов и значений, дочерние элементы и текстовые поля текущего элемента XML идентичны их аналогов в <paramref name="other" /> параметр; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equal(SecurityElement other)
    {
      if (other == null || !string.Equals(this.m_strTag, other.m_strTag) || !string.Equals(this.m_strText, other.m_strText))
        return false;
      if (this.m_lAttributes == null || other.m_lAttributes == null)
      {
        if (this.m_lAttributes != other.m_lAttributes)
          return false;
      }
      else
      {
        int count = this.m_lAttributes.Count;
        if (count != other.m_lAttributes.Count)
          return false;
        for (int index = 0; index < count; ++index)
        {
          if (!string.Equals((string) this.m_lAttributes[index], (string) other.m_lAttributes[index]))
            return false;
        }
      }
      if (this.m_lChildren == null || other.m_lChildren == null)
      {
        if (this.m_lChildren != other.m_lChildren)
          return false;
      }
      else
      {
        if (this.m_lChildren.Count != other.m_lChildren.Count)
          return false;
        this.ConvertSecurityElementFactories();
        other.ConvertSecurityElementFactories();
        IEnumerator enumerator1 = this.m_lChildren.GetEnumerator();
        IEnumerator enumerator2 = other.m_lChildren.GetEnumerator();
        while (enumerator1.MoveNext())
        {
          enumerator2.MoveNext();
          SecurityElement current1 = (SecurityElement) enumerator1.Current;
          SecurityElement current2 = (SecurityElement) enumerator2.Current;
          if (current1 == null || !current1.Equal(current2))
            return false;
        }
      }
      return true;
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего <see cref="T:System.Security.SecurityElement" /> объекта.
    /// </summary>
    /// <returns>
    ///   Копия текущего объекта <see cref="T:System.Security.SecurityElement" />.
    /// </returns>
    [ComVisible(false)]
    public SecurityElement Copy()
    {
      return new SecurityElement(this.m_strTag, this.m_strText)
      {
        m_lChildren = this.m_lChildren == null ? (ArrayList) null : new ArrayList((ICollection) this.m_lChildren),
        m_lAttributes = this.m_lAttributes == null ? (ArrayList) null : new ArrayList((ICollection) this.m_lAttributes)
      };
    }

    /// <summary>Определяет, является ли строка допустимым тегом.</summary>
    /// <param name="tag">
    ///   Тег, допустимость которого требуется проверить.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="tag" /> параметр является допустимым тегом XML; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool IsValidTag(string tag)
    {
      if (tag == null)
        return false;
      return tag.IndexOfAny(SecurityElement.s_tagIllegalCharacters) == -1;
    }

    /// <summary>
    ///   Определяет, является ли строка допустимым как текст внутри элемента XML.
    /// </summary>
    /// <param name="text">Текст для проверки.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="text" /> параметр является допустимым текстовым элементом XML; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool IsValidText(string text)
    {
      if (text == null)
        return false;
      return text.IndexOfAny(SecurityElement.s_textIllegalCharacters) == -1;
    }

    /// <summary>
    ///   Определяет, является ли строка допустимым именем атрибута.
    /// </summary>
    /// <param name="name">Имя атрибута для проверки.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="name" /> параметр является допустимым XML-атрибут name, в противном случае — <see langword="false" />.
    /// </returns>
    public static bool IsValidAttributeName(string name)
    {
      return SecurityElement.IsValidTag(name);
    }

    /// <summary>
    ///   Определяет, является ли строка допустимым значением атрибута.
    /// </summary>
    /// <param name="value">
    ///   Значение атрибута, допустимость которого требуется проверить.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="value" /> параметр является допустимым значением атрибута XML; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool IsValidAttributeValue(string value)
    {
      if (value == null)
        return false;
      return value.IndexOfAny(SecurityElement.s_valueIllegalCharacters) == -1;
    }

    private static string GetEscapeSequence(char c)
    {
      int length = SecurityElement.s_escapeStringPairs.Length;
      int index = 0;
      while (index < length)
      {
        string escapeStringPair1 = SecurityElement.s_escapeStringPairs[index];
        string escapeStringPair2 = SecurityElement.s_escapeStringPairs[index + 1];
        if ((int) escapeStringPair1[0] == (int) c)
          return escapeStringPair2;
        index += 2;
      }
      return c.ToString();
    }

    /// <summary>
    ///   Заменяет недопустимые символы XML в строке с их допустимыми эквивалентами в XML.
    /// </summary>
    /// <param name="str">
    ///   Строка, в которой выполняется обработка недопустимых знаков.
    /// </param>
    /// <returns>
    ///   Входная строка с замененными недопустимыми символами.
    /// </returns>
    public static string Escape(string str)
    {
      if (str == null)
        return (string) null;
      StringBuilder stringBuilder = (StringBuilder) null;
      int length = str.Length;
      int startIndex = 0;
      while (true)
      {
        int index = str.IndexOfAny(SecurityElement.s_escapeChars, startIndex);
        if (index != -1)
        {
          if (stringBuilder == null)
            stringBuilder = new StringBuilder();
          stringBuilder.Append(str, startIndex, index - startIndex);
          stringBuilder.Append(SecurityElement.GetEscapeSequence(str[index]));
          startIndex = index + 1;
        }
        else
          break;
      }
      if (stringBuilder == null)
        return str;
      stringBuilder.Append(str, startIndex, length - startIndex);
      return stringBuilder.ToString();
    }

    private static string GetUnescapeSequence(string str, int index, out int newIndex)
    {
      int num = str.Length - index;
      int length1 = SecurityElement.s_escapeStringPairs.Length;
      int index1 = 0;
      while (index1 < length1)
      {
        string escapeStringPair1 = SecurityElement.s_escapeStringPairs[index1];
        string escapeStringPair2 = SecurityElement.s_escapeStringPairs[index1 + 1];
        int length2 = escapeStringPair2.Length;
        if (length2 <= num && string.Compare(escapeStringPair2, 0, str, index, length2, StringComparison.Ordinal) == 0)
        {
          newIndex = index + escapeStringPair2.Length;
          return escapeStringPair1;
        }
        index1 += 2;
      }
      newIndex = index + 1;
      return str[index].ToString();
    }

    private static string Unescape(string str)
    {
      if (str == null)
        return (string) null;
      StringBuilder stringBuilder = (StringBuilder) null;
      int length = str.Length;
      int newIndex = 0;
      while (true)
      {
        int index = str.IndexOf('&', newIndex);
        if (index != -1)
        {
          if (stringBuilder == null)
            stringBuilder = new StringBuilder();
          stringBuilder.Append(str, newIndex, index - newIndex);
          stringBuilder.Append(SecurityElement.GetUnescapeSequence(str, index, out newIndex));
        }
        else
          break;
      }
      if (stringBuilder == null)
        return str;
      stringBuilder.Append(str, newIndex, length - newIndex);
      return stringBuilder.ToString();
    }

    private static void ToStringHelperStringBuilder(object obj, string str)
    {
      ((StringBuilder) obj).Append(str);
    }

    private static void ToStringHelperStreamWriter(object obj, string str)
    {
      ((TextWriter) obj).Write(str);
    }

    /// <summary>
    ///   Создает строковое представление XML-элемента и его составляющие атрибуты, дочерние элементы и текст.
    /// </summary>
    /// <returns>XML-элемент и его содержимое.</returns>
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      this.ToString("", (object) stringBuilder, new SecurityElement.ToStringHelperFunc(SecurityElement.ToStringHelperStringBuilder));
      return stringBuilder.ToString();
    }

    internal void ToWriter(StreamWriter writer)
    {
      this.ToString("", (object) writer, new SecurityElement.ToStringHelperFunc(SecurityElement.ToStringHelperStreamWriter));
    }

    private void ToString(string indent, object obj, SecurityElement.ToStringHelperFunc func)
    {
      func(obj, "<");
      switch (this.m_type)
      {
        case SecurityElementType.Format:
          func(obj, "?");
          break;
        case SecurityElementType.Comment:
          func(obj, "!");
          break;
      }
      func(obj, this.m_strTag);
      if (this.m_lAttributes != null && this.m_lAttributes.Count > 0)
      {
        func(obj, " ");
        int count = this.m_lAttributes.Count;
        int index = 0;
        while (index < count)
        {
          string lAttribute1 = (string) this.m_lAttributes[index];
          string lAttribute2 = (string) this.m_lAttributes[index + 1];
          func(obj, lAttribute1);
          func(obj, "=\"");
          func(obj, lAttribute2);
          func(obj, "\"");
          if (index != this.m_lAttributes.Count - 2)
          {
            if (this.m_type == SecurityElementType.Regular)
              func(obj, Environment.NewLine);
            else
              func(obj, " ");
          }
          index += 2;
        }
      }
      if (this.m_strText == null && (this.m_lChildren == null || this.m_lChildren.Count == 0))
      {
        switch (this.m_type)
        {
          case SecurityElementType.Format:
            func(obj, " ?>");
            break;
          case SecurityElementType.Comment:
            func(obj, ">");
            break;
          default:
            func(obj, "/>");
            break;
        }
        func(obj, Environment.NewLine);
      }
      else
      {
        func(obj, ">");
        func(obj, this.m_strText);
        if (this.m_lChildren != null)
        {
          this.ConvertSecurityElementFactories();
          func(obj, Environment.NewLine);
          for (int index = 0; index < this.m_lChildren.Count; ++index)
            ((SecurityElement) this.m_lChildren[index]).ToString("", obj, func);
        }
        func(obj, "</");
        func(obj, this.m_strTag);
        func(obj, ">");
        func(obj, Environment.NewLine);
      }
    }

    /// <summary>Находит атрибут с заданным именем в элементе XML.</summary>
    /// <param name="name">Имя атрибута для поиска.</param>
    /// <returns>
    ///   Значение, связанное с именованным атрибутом, или <see langword="null" /> Если атрибута с <paramref name="name" /> существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public string Attribute(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (this.m_lAttributes == null)
        return (string) null;
      int count = this.m_lAttributes.Count;
      int index = 0;
      while (index < count)
      {
        if (string.Equals((string) this.m_lAttributes[index], name))
          return SecurityElement.Unescape((string) this.m_lAttributes[index + 1]);
        index += 2;
      }
      return (string) null;
    }

    /// <summary>Выполняет поиск дочернего по имени тега.</summary>
    /// <param name="tag">
    ///   Тег, по которому выполняется поиск в дочерних элементах.
    /// </param>
    /// <returns>
    ///   Первый дочерний элемент XML с указанным значением тега, или <see langword="null" /> Если дочернего элемента с <paramref name="tag" /> существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="tag" /> имеет значение <see langword="null" />.
    /// </exception>
    public SecurityElement SearchForChildByTag(string tag)
    {
      if (tag == null)
        throw new ArgumentNullException(nameof (tag));
      if (this.m_lChildren == null)
        return (SecurityElement) null;
      foreach (SecurityElement lChild in this.m_lChildren)
      {
        if (lChild != null && string.Equals(lChild.Tag, tag))
          return lChild;
      }
      return (SecurityElement) null;
    }

    internal IPermission ToPermission(bool ignoreTypeLoadFailures)
    {
      IPermission permission = XMLUtil.CreatePermission(this, PermissionState.None, ignoreTypeLoadFailures);
      if (permission == null)
        return (IPermission) null;
      permission.FromXml(this);
      PermissionToken.GetToken(permission);
      return permission;
    }

    [SecurityCritical]
    internal object ToSecurityObject()
    {
      if (!(this.m_strTag == "PermissionSet"))
        return (object) this.ToPermission(false);
      PermissionSet permissionSet = new PermissionSet(PermissionState.None);
      permissionSet.FromXml(this);
      return (object) permissionSet;
    }

    internal string SearchForTextOfLocalName(string strLocalName)
    {
      if (strLocalName == null)
        throw new ArgumentNullException(nameof (strLocalName));
      if (this.m_strTag == null)
        return (string) null;
      if (this.m_strTag.Equals(strLocalName) || this.m_strTag.EndsWith(":" + strLocalName, StringComparison.Ordinal))
        return SecurityElement.Unescape(this.m_strText);
      if (this.m_lChildren == null)
        return (string) null;
      foreach (SecurityElement lChild in this.m_lChildren)
      {
        string str = lChild.SearchForTextOfLocalName(strLocalName);
        if (str != null)
          return str;
      }
      return (string) null;
    }

    /// <summary>
    ///   Выполняет поиск дочернего по имени тега и возвращает содержащегося текста.
    /// </summary>
    /// <param name="tag">
    ///   Тег, по которому выполняется поиск в дочерних элементах.
    /// </param>
    /// <returns>
    ///   Текстовое содержимое первого дочернего элемента с указанным значением тега.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="tag" /> имеет значение <see langword="null" />.
    /// </exception>
    public string SearchForTextOfTag(string tag)
    {
      if (tag == null)
        throw new ArgumentNullException(nameof (tag));
      if (string.Equals(this.m_strTag, tag))
        return SecurityElement.Unescape(this.m_strText);
      if (this.m_lChildren == null)
        return (string) null;
      IEnumerator enumerator = this.m_lChildren.GetEnumerator();
      this.ConvertSecurityElementFactories();
      while (enumerator.MoveNext())
      {
        string str = ((SecurityElement) enumerator.Current).SearchForTextOfTag(tag);
        if (str != null)
          return str;
      }
      return (string) null;
    }

    private delegate void ToStringHelperFunc(object obj, string str);
  }
}
