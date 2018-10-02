// Decompiled with JetBrains decompiler
// Type: System.Collections.Comparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections
{
  /// <summary>
  ///   Сравнивает два объекта на равенство, где сравнение строк с учетом регистра.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class Comparer : IComparer, ISerializable
  {
    /// <summary>
    ///   Представляет экземпляр <see cref="T:System.Collections.Comparer" /> связанный с <see cref="P:System.Threading.Thread.CurrentCulture" /> текущего потока.
    ///    Это поле доступно только для чтения.
    /// </summary>
    public static readonly Comparer Default = new Comparer(CultureInfo.CurrentCulture);
    /// <summary>
    ///   Представляет экземпляр <see cref="T:System.Collections.Comparer" /> связанный с <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.
    ///    Это поле доступно только для чтения.
    /// </summary>
    public static readonly Comparer DefaultInvariant = new Comparer(CultureInfo.InvariantCulture);
    private CompareInfo m_compareInfo;
    private const string CompareInfoName = "CompareInfo";

    private Comparer()
    {
      this.m_compareInfo = (CompareInfo) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Comparer" /> с использованием указанного объекта <see cref="T:System.Globalization.CultureInfo" />.
    /// </summary>
    /// <param name="culture">
    ///   <see cref="T:System.Globalization.CultureInfo" /> Для использования нового <see cref="T:System.Collections.Comparer" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    public Comparer(CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      this.m_compareInfo = culture.CompareInfo;
    }

    private Comparer(SerializationInfo info, StreamingContext context)
    {
      this.m_compareInfo = (CompareInfo) null;
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Name == "CompareInfo")
          this.m_compareInfo = (CompareInfo) info.GetValue("CompareInfo", typeof (CompareInfo));
      }
    }

    /// <summary>
    ///   Выполняет сравнение двух объектов одного типа с учетом регистра и возвращает значение, указывающее, что один объект меньше, равняется или больше другого.
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
    ///         Значение <paramref name="a" /> меньше <paramref name="b" />.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="a" /> равняется <paramref name="b" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Значение <paramref name="a" /> больше значения <paramref name="b" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Ни <paramref name="a" /> ни <paramref name="b" /> реализует <see cref="T:System.IComparable" /> интерфейса.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="a" /> и <paramref name="b" /> имеют разные типы и ни один могут сравниваться с другими.
    /// </exception>
    public int Compare(object a, object b)
    {
      if (a == b)
        return 0;
      if (a == null)
        return -1;
      if (b == null)
        return 1;
      if (this.m_compareInfo != null)
      {
        string string1 = a as string;
        string string2 = b as string;
        if (string1 != null && string2 != null)
          return this.m_compareInfo.Compare(string1, string2);
      }
      IComparable comparable1 = a as IComparable;
      if (comparable1 != null)
        return comparable1.CompareTo(b);
      IComparable comparable2 = b as IComparable;
      if (comparable2 != null)
        return -comparable2.CompareTo(a);
      throw new ArgumentException(Environment.GetResourceString("Argument_ImplementIComparable"));
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект с данными, необходимыми для сериализации.
    /// </summary>
    /// <param name="info">
    ///   Объект, который требуется заполнить данными.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении сериализации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      if (this.m_compareInfo == null)
        return;
      info.AddValue("CompareInfo", (object) this.m_compareInfo);
    }
  }
}
