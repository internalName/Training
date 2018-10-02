// Decompiled with JetBrains decompiler
// Type: System.Text.EncodingInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  /// <summary>Основные сведения о кодировке.</summary>
  [Serializable]
  public sealed class EncodingInfo
  {
    private int iCodePage;
    private string strEncodingName;
    private string strDisplayName;

    internal EncodingInfo(int codePage, string name, string displayName)
    {
      this.iCodePage = codePage;
      this.strEncodingName = name;
      this.strDisplayName = displayName;
    }

    /// <summary>Получает идентификатор кодовой страницы кодировки.</summary>
    /// <returns>Идентификатор кодовой страницы кодировки.</returns>
    public int CodePage
    {
      get
      {
        return this.iCodePage;
      }
    }

    /// <summary>
    ///   Возвращает имя, зарегистрированное в Интернет назначенный номера центра (IANA) для кодирования.
    /// </summary>
    /// <returns>
    ///   Имя IANA для кодирования.
    ///    Дополнительные сведения о разделе www.iana.org.
    /// </returns>
    public string Name
    {
      get
      {
        return this.strEncodingName;
      }
    }

    /// <summary>
    ///   Возвращает удобное для восприятия описание кодировки.
    /// </summary>
    /// <returns>Понятное описание кодировки.</returns>
    public string DisplayName
    {
      get
      {
        return this.strDisplayName;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Text.Encoding" /> объекта, который соответствует текущему <see cref="T:System.Text.EncodingInfo" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.Encoding" /> объект, соответствующий текущему <see cref="T:System.Text.EncodingInfo" /> объекта.
    /// </returns>
    public Encoding GetEncoding()
    {
      return Encoding.GetEncoding(this.iCodePage);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли указанный объект текущему <see cref="T:System.Text.EncodingInfo" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект для сравнения с текущим <see cref="T:System.Text.EncodingInfo" /> объекта.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="value" /> является <see cref="T:System.Text.EncodingInfo" /> объекта и равен текущему объекту <see cref="T:System.Text.EncodingInfo" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object value)
    {
      EncodingInfo encodingInfo = value as EncodingInfo;
      if (encodingInfo != null)
        return this.CodePage == encodingInfo.CodePage;
      return false;
    }

    /// <summary>
    ///   Возвращает хэш-код для текущего <see cref="T:System.Text.EncodingInfo" /> объекта.
    /// </summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    public override int GetHashCode()
    {
      return this.CodePage;
    }
  }
}
