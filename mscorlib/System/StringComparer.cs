// Decompiled with JetBrains decompiler
// Type: System.StringComparer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Представляет операции сравнения строк, в которых используются правила сравнения с учетом регистра, языка и региональных параметров или правил сравнения по порядковому номеру.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class StringComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
  {
    private static readonly StringComparer _invariantCulture = (StringComparer) new CultureAwareComparer(CultureInfo.InvariantCulture, false);
    private static readonly StringComparer _invariantCultureIgnoreCase = (StringComparer) new CultureAwareComparer(CultureInfo.InvariantCulture, true);
    private static readonly StringComparer _ordinal = (StringComparer) new OrdinalComparer(false);
    private static readonly StringComparer _ordinalIgnoreCase = (StringComparer) new OrdinalComparer(true);

    /// <summary>
    ///   Получает объект <see cref="T:System.StringComparer" />, выполняющий сравнение строк с учетом регистра, используя правила сравнения строк по словам для инвариантных языка и региональных параметров.
    /// </summary>
    /// <returns>
    ///   Новый объект <see cref="T:System.StringComparer" />.
    /// </returns>
    public static StringComparer InvariantCulture
    {
      get
      {
        return StringComparer._invariantCulture;
      }
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.StringComparer" />, выполняющий сравнение строк без учета регистра, используя правила сравнения строк по словам для инвариантных языка и региональных параметров.
    /// </summary>
    /// <returns>
    ///   Новый объект <see cref="T:System.StringComparer" />.
    /// </returns>
    public static StringComparer InvariantCultureIgnoreCase
    {
      get
      {
        return StringComparer._invariantCultureIgnoreCase;
      }
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.StringComparer" />, выполняющий сравнение строк с учетом регистра, используя правила сравнения строк по словам для текущего языка и региональных параметров.
    /// </summary>
    /// <returns>
    ///   Новый объект <see cref="T:System.StringComparer" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static StringComparer CurrentCulture
    {
      [__DynamicallyInvokable] get
      {
        return (StringComparer) new CultureAwareComparer(CultureInfo.CurrentCulture, false);
      }
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.StringComparer" />, выполняющий сравнения строк без учета регистра, используя правила сравнения строк по словам для текущего языка и региональных параметров.
    /// </summary>
    /// <returns>
    ///   Новый объект <see cref="T:System.StringComparer" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static StringComparer CurrentCultureIgnoreCase
    {
      [__DynamicallyInvokable] get
      {
        return (StringComparer) new CultureAwareComparer(CultureInfo.CurrentCulture, true);
      }
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.StringComparer" />, выполняющий сравнение строк по порядковому номеру с учетом регистра.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.StringComparer" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static StringComparer Ordinal
    {
      [__DynamicallyInvokable] get
      {
        return StringComparer._ordinal;
      }
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.StringComparer" />, выполняющий сравнение строк по порядковому номеру без учета регистра.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.StringComparer" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static StringComparer OrdinalIgnoreCase
    {
      [__DynamicallyInvokable] get
      {
        return StringComparer._ordinalIgnoreCase;
      }
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.StringComparer" />, который сравнивает строки в соответствии с правилами заданного языка и региональных параметров.
    /// </summary>
    /// <param name="culture">
    ///   Язык и региональные параметры, лингвистические правила которых используются для сравнения строк.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы в операциях сравнения регистр не учитывался; значение <see langword="false" /> для учета регистра в операциях сравнения.
    /// </param>
    /// <returns>
    ///   Новый объект <see cref="T:System.StringComparer" />, выполняющий сравнение строк в соответствии с правилами сравнения, используемыми параметром <paramref name="culture" />, и правилом учета регистра параметра <paramref name="ignoreCase" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static StringComparer Create(CultureInfo culture, bool ignoreCase)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      return (StringComparer) new CultureAwareComparer(culture, ignoreCase);
    }

    /// <summary>
    ///   При переопределении в производном классе сравнивает два объекта и возвращает сведения об их относительном порядке сортировки.
    /// </summary>
    /// <param name="x">
    ///   Объект, сравниваемый с <paramref name="y" />.
    /// </param>
    /// <param name="y">
    ///   Объект, сравниваемый с <paramref name="x" />.
    /// </param>
    /// <returns>
    /// Знаковое целое число, которое определяет относительные значения параметров <paramref name="x" /> и <paramref name="y" />, как показано в следующей таблице.
    /// 
    ///         Значение
    /// 
    ///         Значение
    /// 
    ///         Меньше нуля
    /// 
    ///         <paramref name="x" /> предшествует <paramref name="y" /> в порядке сортировки.
    /// 
    ///         -или-
    /// 
    ///         <paramref name="x" /> имеет значение <see langword="null" />, а <paramref name="y" /> не имеет значения <see langword="null" />.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="x" /> равно <paramref name="y" />.
    /// 
    ///         -или-
    /// 
    ///         Оба параметра <paramref name="x" /> и <paramref name="y" /> имеют значение <see langword="null" />.
    /// 
    ///         Больше нуля
    /// 
    ///         <paramref name="x" /> следует за <paramref name="y" /> в порядке сортировки.
    /// 
    ///         -или-
    /// 
    ///         <paramref name="y" /> имеет значение <see langword="null" />, а <paramref name="x" /> не имеет значения <see langword="null" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Ни <paramref name="x" /> , ни <paramref name="y" /> — <see cref="T:System.String" /> объекта и не <paramref name="x" /> , ни <paramref name="y" /> реализует <see cref="T:System.IComparable" /> интерфейса.
    /// </exception>
    public int Compare(object x, object y)
    {
      if (x == y)
        return 0;
      if (x == null)
        return -1;
      if (y == null)
        return 1;
      string x1 = x as string;
      if (x1 != null)
      {
        string y1 = y as string;
        if (y1 != null)
          return this.Compare(x1, y1);
      }
      IComparable comparable = x as IComparable;
      if (comparable != null)
        return comparable.CompareTo(y);
      throw new ArgumentException(Environment.GetResourceString("Argument_ImplementIComparable"));
    }

    /// <summary>
    ///   При переопределении в производном классе позволяет определить, равны ли два объекта.
    /// </summary>
    /// <param name="x">
    ///   Объект, сравниваемый с <paramref name="y" />.
    /// </param>
    /// <param name="y">
    ///   Объект, сравниваемый с <paramref name="x" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметры <paramref name="x" /> и <paramref name="y" /> указывают на один и тот же объект, если параметры <paramref name="x" /> и <paramref name="y" /> относятся к одному и тому же типу объектов и эти объекты равны или если параметры <paramref name="x" /> и <paramref name="y" /> равны <see langword="null" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool Equals(object x, object y)
    {
      if (x == y)
        return true;
      if (x == null || y == null)
        return false;
      string x1 = x as string;
      if (x1 != null)
      {
        string y1 = y as string;
        if (y1 != null)
          return this.Equals(x1, y1);
      }
      return x.Equals(y);
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает хэш-код для указанного объекта.
    /// </summary>
    /// <param name="obj">Объект.</param>
    /// <returns>
    ///   32-разрядный хэш-код, вычисленный на основе значения параметра <paramref name="obj" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Недостаточно памяти для выделения буфера, который необходим для вычисления хэш-кода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    public int GetHashCode(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      string str = obj as string;
      if (str != null)
        return this.GetHashCode(str);
      return obj.GetHashCode();
    }

    /// <summary>
    ///   При переопределении в производном классе сравнивает две строки и возвращает сведения об их относительном порядке сортировки.
    /// </summary>
    /// <param name="x">
    ///   Строка, сравниваемая с параметром <paramref name="y" />.
    /// </param>
    /// <param name="y">
    ///   Строка, сравниваемая с параметром <paramref name="x" />.
    /// </param>
    /// <returns>
    /// Знаковое целое число, которое определяет относительные значения параметров <paramref name="x" /> и <paramref name="y" />, как показано в следующей таблице.
    /// 
    ///         Значение
    /// 
    ///         Значение
    /// 
    ///         Меньше нуля
    /// 
    ///         <paramref name="x" /> предшествует <paramref name="y" /> в порядке сортировки.
    /// 
    ///         -или-
    /// 
    ///         <paramref name="x" /> имеет значение <see langword="null" />, а <paramref name="y" /> не имеет значения <see langword="null" />.
    /// 
    ///         Нуль
    /// 
    ///         <paramref name="x" /> равно <paramref name="y" />.
    /// 
    ///         -или-
    /// 
    ///         Оба параметра <paramref name="x" /> и <paramref name="y" /> имеют значение <see langword="null" />.
    /// 
    ///         Больше нуля
    /// 
    ///         <paramref name="x" /> следует за <paramref name="y" /> в порядке сортировки.
    /// 
    ///         -или-
    /// 
    ///         <paramref name="y" /> имеет значение <see langword="null" />, а <paramref name="x" /> не имеет значения <see langword="null" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public abstract int Compare(string x, string y);

    /// <summary>
    ///   При переопределении в производном классе позволяет определить, равны ли две строки.
    /// </summary>
    /// <param name="x">
    ///   Строка, сравниваемая с параметром <paramref name="y" />.
    /// </param>
    /// <param name="y">
    ///   Строка, сравниваемая с параметром <paramref name="x" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметры <paramref name="x" /> и <paramref name="y" /> указывают не один и тот же объект, если параметры <paramref name="x" /> и <paramref name="y" /> равны или если параметры <paramref name="x" /> и <paramref name="y" /> равны <see langword="null" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool Equals(string x, string y);

    /// <summary>
    ///   При переопределении в производном классе возвращает хэш-код указанной строки.
    /// </summary>
    /// <param name="obj">Строка.</param>
    /// <returns>
    ///   32-разрядный хэш-код, вычисленный на основе значения параметра <paramref name="obj" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Недостаточно памяти для выделения буфера, который необходим для вычисления хэш-кода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract int GetHashCode(string obj);

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.StringComparer" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected StringComparer()
    {
    }
  }
}
