// Decompiled with JetBrains decompiler
// Type: System.Globalization.SortKey
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
  /// <summary>
  ///   Представляет результат сопоставления строки ее ключу сортировки.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class SortKey
  {
    [OptionalField(VersionAdded = 3)]
    internal string localeName;
    [OptionalField(VersionAdded = 1)]
    internal int win32LCID;
    internal CompareOptions options;
    internal string m_String;
    internal byte[] m_KeyData;

    internal SortKey(string localeName, string str, CompareOptions options, byte[] keyData)
    {
      this.m_KeyData = keyData;
      this.localeName = localeName;
      this.options = options;
      this.m_String = str;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext context)
    {
      if (this.win32LCID != 0)
        return;
      this.win32LCID = CultureInfo.GetCultureInfo(this.localeName).LCID;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
      if (!string.IsNullOrEmpty(this.localeName) || this.win32LCID == 0)
        return;
      this.localeName = CultureInfo.GetCultureInfo(this.win32LCID).Name;
    }

    /// <summary>
    ///   Возвращает исходную строку, используемую для создания текущего <see cref="T:System.Globalization.SortKey" /> объекта.
    /// </summary>
    /// <returns>
    ///   Исходную строку, используемую для создания текущего <see cref="T:System.Globalization.SortKey" /> объекта.
    /// </returns>
    public virtual string OriginalString
    {
      get
      {
        return this.m_String;
      }
    }

    /// <summary>
    ///   Возвращает массив байтов, представляющий текущую <see cref="T:System.Globalization.SortKey" /> объекта.
    /// </summary>
    /// <returns>
    ///   Массив байтов, представляющий текущую <see cref="T:System.Globalization.SortKey" /> объекта.
    /// </returns>
    public virtual byte[] KeyData
    {
      get
      {
        return (byte[]) this.m_KeyData.Clone();
      }
    }

    /// <summary>Сравнивает два ключа сортировки.</summary>
    /// <param name="sortkey1">
    ///   Первый ключ сортировки для сравнения.
    /// </param>
    /// <param name="sortkey2">
    ///   Второй ключ сортировки для сравнения.
    /// </param>
    /// <returns>
    /// Целое число со знаком, показывающее связь между <paramref name="sortkey1" /> и <paramref name="sortkey2" />.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Меньше нуля
    /// 
    ///         Значение <paramref name="sortkey1" /> меньше <paramref name="sortkey2" />.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="sortkey1" /> равно <paramref name="sortkey2" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Значение <paramref name="sortkey1" /> больше значения <paramref name="sortkey2" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="sortkey1" /> или <paramref name="sortkey2" /> имеет значение <see langword="null" />.
    /// </exception>
    public static int Compare(SortKey sortkey1, SortKey sortkey2)
    {
      if (sortkey1 == null || sortkey2 == null)
        throw new ArgumentNullException(sortkey1 == null ? nameof (sortkey1) : nameof (sortkey2));
      byte[] keyData1 = sortkey1.m_KeyData;
      byte[] keyData2 = sortkey2.m_KeyData;
      if (keyData1.Length == 0)
        return keyData2.Length == 0 ? 0 : -1;
      if (keyData2.Length == 0)
        return 1;
      int num = keyData1.Length < keyData2.Length ? keyData1.Length : keyData2.Length;
      for (int index = 0; index < num; ++index)
      {
        if ((int) keyData1[index] > (int) keyData2[index])
          return 1;
        if ((int) keyData1[index] < (int) keyData2[index])
          return -1;
      }
      return 0;
    }

    /// <summary>
    ///   Определяет, равен ли заданный объект текущему объекту <see cref="T:System.Globalization.SortKey" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, сравниваемый с текущим <see cref="T:System.Globalization.SortKey" /> объекта.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="value" /> равно текущему объекту <see cref="T:System.Globalization.SortKey" />, в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public override bool Equals(object value)
    {
      SortKey sortkey2 = value as SortKey;
      if (sortkey2 != null)
        return SortKey.Compare(this, sortkey2) == 0;
      return false;
    }

    /// <summary>
    ///   Служит хэш-функцией текущего <see cref="T:System.Globalization.SortKey" /> объект, который подходит для использования в алгоритмах и структурах данных, таких как хэш-таблицы хэширования.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего <see cref="T:System.Globalization.SortKey" /> объекта.
    /// </returns>
    public override int GetHashCode()
    {
      return CompareInfo.GetCompareInfo(this.localeName).GetHashCodeOfString(this.m_String, this.options);
    }

    /// <summary>
    ///   Возвращает строку, представляющую текущий объект <see cref="T:System.Globalization.SortKey" />.
    /// </summary>
    /// <returns>
    ///   Строка, представляющая текущий объект <see cref="T:System.Globalization.SortKey" />.
    /// </returns>
    public override string ToString()
    {
      return "SortKey - " + this.localeName + ", " + (object) this.options + ", " + this.m_String;
    }
  }
}
