// Decompiled with JetBrains decompiler
// Type: System.Collections.CaseInsensitiveComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>Сравнивает два объекта без учета регистра строк.</summary>
  [ComVisible(true)]
  [Serializable]
  public class CaseInsensitiveComparer : IComparer
  {
    private CompareInfo m_compareInfo;
    private static volatile CaseInsensitiveComparer m_InvariantCaseInsensitiveComparer;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.CaseInsensitiveComparer" /> класса <see cref="P:System.Threading.Thread.CurrentCulture" /> текущего потока.
    /// </summary>
    public CaseInsensitiveComparer()
    {
      this.m_compareInfo = CultureInfo.CurrentCulture.CompareInfo;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.CaseInsensitiveComparer" /> с использованием указанного объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </summary>
    /// <param name="culture">
    ///   <see cref="T:System.Globalization.CultureInfo" /> Для использования нового <see cref="T:System.Collections.CaseInsensitiveComparer" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    public CaseInsensitiveComparer(CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      this.m_compareInfo = culture.CompareInfo;
    }

    /// <summary>
    ///   Возвращает экземпляр класса <see cref="T:System.Collections.CaseInsensitiveComparer" /> связанный с <see cref="P:System.Threading.Thread.CurrentCulture" /> из текущего потока и доступна всегда.
    /// </summary>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Collections.CaseInsensitiveComparer" /> связанный с <see cref="P:System.Threading.Thread.CurrentCulture" /> текущего потока.
    /// </returns>
    public static CaseInsensitiveComparer Default
    {
      get
      {
        return new CaseInsensitiveComparer(CultureInfo.CurrentCulture);
      }
    }

    /// <summary>
    ///   Возвращает экземпляр класса <see cref="T:System.Collections.CaseInsensitiveComparer" /> связанный с <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> и всегда доступна.
    /// </summary>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Collections.CaseInsensitiveComparer" /> связанный с <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.
    /// </returns>
    public static CaseInsensitiveComparer DefaultInvariant
    {
      get
      {
        if (CaseInsensitiveComparer.m_InvariantCaseInsensitiveComparer == null)
          CaseInsensitiveComparer.m_InvariantCaseInsensitiveComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
        return CaseInsensitiveComparer.m_InvariantCaseInsensitiveComparer;
      }
    }

    /// <summary>
    ///   Выполняет сравнение без учета регистра два объекта одного типа и возвращает значение, указывающее, что один объект меньше, равняется или больше другого.
    /// </summary>
    /// <param name="a">Первый из сравниваемых объектов.</param>
    /// <param name="b">Второй из сравниваемых объектов.</param>
    /// <returns>
    /// Знаковое целое число, которое определяет относительные значения параметров <paramref name="a" /> и <paramref name="b" />, как показано в следующей таблице.
    /// 
    ///         Значение
    /// 
    ///         Значение
    /// 
    ///         Меньше нуля
    /// 
    ///         <paramref name="a" /> является менее <paramref name="b" />, без учета регистра.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="a" /> равняется <paramref name="b" />, без учета регистра.
    /// 
    ///         Больше нуля
    /// 
    ///         <paramref name="a" /> больше, чем <paramref name="b" />, без учета регистра.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Ни <paramref name="a" /> ни <paramref name="b" /> реализует <see cref="T:System.IComparable" /> интерфейса.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="a" /> и <paramref name="b" /> принадлежат к разным типам.
    /// </exception>
    public int Compare(object a, object b)
    {
      string string1 = a as string;
      string string2 = b as string;
      if (string1 != null && string2 != null)
        return this.m_compareInfo.Compare(string1, string2, CompareOptions.IgnoreCase);
      return Comparer.Default.Compare(a, b);
    }
  }
}
