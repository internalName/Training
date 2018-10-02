// Decompiled with JetBrains decompiler
// Type: System.Version
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
  /// <summary>
  ///   Представляет номер версии сборки, операционной системы или среды CLR.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class Version : ICloneable, IComparable, IComparable<Version>, IEquatable<Version>
  {
    private static readonly char[] SeparatorsArray = new char[1]
    {
      '.'
    };
    private int _Build = -1;
    private int _Revision = -1;
    private int _Major;
    private int _Minor;
    private const int ZERO_CHAR_VALUE = 48;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Version" /> класса с указанной основной и дополнительный номера, построения и редакции.
    /// </summary>
    /// <param name="major">Основной номер версии.</param>
    /// <param name="minor">Дополнительный номер версии.</param>
    /// <param name="build">Номер сборки.</param>
    /// <param name="revision">Номер редакции.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="major" />, <paramref name="minor" />, <paramref name="build" /> или <paramref name="revision" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public Version(int major, int minor, int build, int revision)
    {
      if (major < 0)
        throw new ArgumentOutOfRangeException(nameof (major), Environment.GetResourceString("ArgumentOutOfRange_Version"));
      if (minor < 0)
        throw new ArgumentOutOfRangeException(nameof (minor), Environment.GetResourceString("ArgumentOutOfRange_Version"));
      if (build < 0)
        throw new ArgumentOutOfRangeException(nameof (build), Environment.GetResourceString("ArgumentOutOfRange_Version"));
      if (revision < 0)
        throw new ArgumentOutOfRangeException(nameof (revision), Environment.GetResourceString("ArgumentOutOfRange_Version"));
      this._Major = major;
      this._Minor = minor;
      this._Build = build;
      this._Revision = revision;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Version" /> класса с помощью указанных основного и дополнительного и построить значения.
    /// </summary>
    /// <param name="major">Основной номер версии.</param>
    /// <param name="minor">Дополнительный номер версии.</param>
    /// <param name="build">Номер построения.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="major" />, <paramref name="minor" /> или <paramref name="build" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public Version(int major, int minor, int build)
    {
      if (major < 0)
        throw new ArgumentOutOfRangeException(nameof (major), Environment.GetResourceString("ArgumentOutOfRange_Version"));
      if (minor < 0)
        throw new ArgumentOutOfRangeException(nameof (minor), Environment.GetResourceString("ArgumentOutOfRange_Version"));
      if (build < 0)
        throw new ArgumentOutOfRangeException(nameof (build), Environment.GetResourceString("ArgumentOutOfRange_Version"));
      this._Major = major;
      this._Minor = minor;
      this._Build = build;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Version" /> класса, используя указанные значения основной и дополнительный.
    /// </summary>
    /// <param name="major">Основной номер версии.</param>
    /// <param name="minor">Дополнительный номер версии.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="major" /> или <paramref name="minor" /> меньше нуля.
    /// </exception>
    [__DynamicallyInvokable]
    public Version(int major, int minor)
    {
      if (major < 0)
        throw new ArgumentOutOfRangeException(nameof (major), Environment.GetResourceString("ArgumentOutOfRange_Version"));
      if (minor < 0)
        throw new ArgumentOutOfRangeException(nameof (minor), Environment.GetResourceString("ArgumentOutOfRange_Version"));
      this._Major = major;
      this._Minor = minor;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Version" />, используя указанную строку.
    /// </summary>
    /// <param name="version">
    ///   Строка, содержащая основной и дополнительный номера, построения и редакции, где каждое число отделено точкой (".").
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="version" />имеет менее двух или более четырех компонентов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="version" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Основной и дополнительный номер сборки или редакции меньше нуля.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   По крайней мере один компонент <paramref name="version" /> не распознан как десятичное число.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   По крайней мере один компонент <paramref name="version" /> представляет собой число больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public Version(string version)
    {
      Version version1 = Version.Parse(version);
      this._Major = version1.Major;
      this._Minor = version1.Minor;
      this._Build = version1.Build;
      this._Revision = version1.Revision;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Version" />.
    /// </summary>
    public Version()
    {
      this._Major = 0;
      this._Minor = 0;
    }

    /// <summary>
    ///   Возвращает значение компонента основной номер версии для текущего <see cref="T:System.Version" /> объекта.
    /// </summary>
    /// <returns>Основной номер версии.</returns>
    [__DynamicallyInvokable]
    public int Major
    {
      [__DynamicallyInvokable] get
      {
        return this._Major;
      }
    }

    /// <summary>
    ///   Возвращает значение компонента дополнительный номер версии для текущего <see cref="T:System.Version" /> объекта.
    /// </summary>
    /// <returns>Дополнительный номер версии.</returns>
    [__DynamicallyInvokable]
    public int Minor
    {
      [__DynamicallyInvokable] get
      {
        return this._Minor;
      }
    }

    /// <summary>
    ///   Возвращает значение компонента номера версии сборки для текущего <see cref="T:System.Version" /> объекта.
    /// </summary>
    /// <returns>
    ///   Номер построения или значение -1, если номер сборки не определен.
    /// </returns>
    [__DynamicallyInvokable]
    public int Build
    {
      [__DynamicallyInvokable] get
      {
        return this._Build;
      }
    }

    /// <summary>
    ///   Возвращает значение компонента номера версии редакции для текущего <see cref="T:System.Version" /> объекта.
    /// </summary>
    /// <returns>
    ///   Номер редакции или значение -1, если номер редакции не определен.
    /// </returns>
    [__DynamicallyInvokable]
    public int Revision
    {
      [__DynamicallyInvokable] get
      {
        return this._Revision;
      }
    }

    /// <summary>Возвращает старшие 16 разрядов номера редакции.</summary>
    /// <returns>16-разрядное знаковое целое число.</returns>
    [__DynamicallyInvokable]
    public short MajorRevision
    {
      [__DynamicallyInvokable] get
      {
        return (short) (this._Revision >> 16);
      }
    }

    /// <summary>Возвращает младшие 16 разрядов номера редакции.</summary>
    /// <returns>16-разрядное знаковое целое число.</returns>
    [__DynamicallyInvokable]
    public short MinorRevision
    {
      [__DynamicallyInvokable] get
      {
        return (short) (this._Revision & (int) ushort.MaxValue);
      }
    }

    /// <summary>
    ///   Возвращает новый <see cref="T:System.Version" /> , значение которого совпадает со значением текущего объекта <see cref="T:System.Version" /> объекта.
    /// </summary>
    /// <returns>
    ///   Новый <see cref="T:System.Object" /> , значения которого являются копию текущего <see cref="T:System.Version" /> объекта.
    /// </returns>
    public object Clone()
    {
      return (object) new Version()
      {
        _Major = this._Major,
        _Minor = this._Minor,
        _Build = this._Build,
        _Revision = this._Revision
      };
    }

    /// <summary>
    ///   Сравнивает текущий <see cref="T:System.Version" /> объект с указанным объектом и возвращает сведения об их относительных значениях.
    /// </summary>
    /// <param name="version">
    ///   Объект для сравнения или значение <see langword="null" />.
    /// </param>
    /// <returns>
    /// Целое число со знаком, которое определяет относительные значения двух объектов, как показано в следующей таблице.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         Меньше нуля
    /// 
    ///         Текущий <see cref="T:System.Version" /> объект — это версия перед <paramref name="version" />.
    /// 
    ///         Нуль
    /// 
    ///         Текущий <see cref="T:System.Version" /> объект находится в той же версии, <paramref name="version" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Текущий <see cref="T:System.Version" /> объект — это версия вызова <paramref name="version" />.
    /// 
    ///         -или-
    /// 
    ///         Свойство <paramref name="version" /> имеет значение <see langword="null" />.
    ///       </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="version" /> не является параметром типа <see cref="T:System.Version" />.
    /// </exception>
    public int CompareTo(object version)
    {
      if (version == null)
        return 1;
      Version version1 = version as Version;
      if (version1 == (Version) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeVersion"));
      if (this._Major != version1._Major)
        return this._Major > version1._Major ? 1 : -1;
      if (this._Minor != version1._Minor)
        return this._Minor > version1._Minor ? 1 : -1;
      if (this._Build != version1._Build)
        return this._Build > version1._Build ? 1 : -1;
      if (this._Revision == version1._Revision)
        return 0;
      return this._Revision > version1._Revision ? 1 : -1;
    }

    /// <summary>
    ///   Сравнивает текущий <see cref="T:System.Version" /> объекта с заданным <see cref="T:System.Version" /> объекта и возвращает сведения об их относительных значениях.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Version" /> объект, сравниваемый с текущим <see cref="T:System.Version" /> объекта, или <see langword="null" />.
    /// </param>
    /// <returns>
    /// Целое число со знаком, которое определяет относительные значения двух объектов, как показано в следующей таблице.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         Меньше нуля
    /// 
    ///         Текущий <see cref="T:System.Version" /> объект — это версия перед <paramref name="value" />.
    /// 
    ///         Нуль
    /// 
    ///         Текущий <see cref="T:System.Version" /> объект находится в той же версии, <paramref name="value" />.
    /// 
    ///         Больше нуля
    /// 
    ///         Текущий <see cref="T:System.Version" /> объект — это версия вызова <paramref name="value" />.
    /// 
    ///         -или-
    /// 
    ///         Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    ///       </returns>
    [__DynamicallyInvokable]
    public int CompareTo(Version value)
    {
      if (value == (Version) null)
        return 1;
      if (this._Major != value._Major)
        return this._Major > value._Major ? 1 : -1;
      if (this._Minor != value._Minor)
        return this._Minor > value._Minor ? 1 : -1;
      if (this._Build != value._Build)
        return this._Build > value._Build ? 1 : -1;
      if (this._Revision == value._Revision)
        return 0;
      return this._Revision > value._Revision ? 1 : -1;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий <see cref="T:System.Version" /> объект равен указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с текущим <see cref="T:System.Version" /> объекта, или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если текущий <see cref="T:System.Version" /> объекта и <paramref name="obj" /> оба <see cref="T:System.Version" /> объектов и все компоненты текущего <see cref="T:System.Version" /> объекта совпадает с соответствующим компонентом <paramref name="obj" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      Version version = obj as Version;
      return !(version == (Version) null) && this._Major == version._Major && (this._Minor == version._Minor && this._Build == version._Build) && this._Revision == version._Revision;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий <see cref="T:System.Version" /> и указанный объект <see cref="T:System.Version" /> объекта представляют одинаковое значение.
    /// </summary>
    /// <param name="obj">
    ///   Объект <see cref="T:System.Version" /> объект, сравниваемый с текущим <see cref="T:System.Version" /> объекта, или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если все компоненты текущего <see cref="T:System.Version" /> объекта совпадает с соответствующим компонентом <paramref name="obj" /> параметр; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(Version obj)
    {
      return !(obj == (Version) null) && this._Major == obj._Major && (this._Minor == obj._Minor && this._Build == obj._Build) && this._Revision == obj._Revision;
    }

    /// <summary>
    ///   Возвращает хэш-код для текущего <see cref="T:System.Version" /> объекта.
    /// </summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return 0 | (this._Major & 15) << 28 | (this._Minor & (int) byte.MaxValue) << 20 | (this._Build & (int) byte.MaxValue) << 12 | this._Revision & 4095;
    }

    /// <summary>
    ///   Преобразует значение текущего <see cref="T:System.Version" /> объекта в эквивалентное <see cref="T:System.String" /> представление.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.String" /> Представление значения основной и дополнительный номера, построения и редакции компонентов текущего <see cref="T:System.Version" /> объекта, как показано в следующем формате.
    ///    Все компоненты разделены точкой (.).
    ///    Квадратные скобки ("[" и "]") указывают на компонент, который не будет отображаться в возвращаемом значении, если он не определен.
    /// 
    ///   ОсновнойНомерВерсии.ДополнительныйНомерВерсии[.НомерПостроения[.НомерРедакции]]
    /// 
    ///   Например, если вы создаете <see cref="T:System.Version" /> с помощью конструктора по Version(1,1), возвращаемая строка является «1.1».
    ///    Если вы создаете <see cref="T:System.Version" /> с помощью конструктора по Version(1,3,4,2), возвращаемая строка является «1.3.4.2».
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      if (this._Build == -1)
        return this.ToString(2);
      if (this._Revision == -1)
        return this.ToString(3);
      return this.ToString(4);
    }

    /// <summary>
    ///   Преобразует значение текущего <see cref="T:System.Version" /> объекта в эквивалентное <see cref="T:System.String" /> представление.
    ///    Заданное количество обозначает число возвращаемых компонент.
    /// </summary>
    /// <param name="fieldCount">
    ///   Число возвращаемых компонентов.
    ///   <paramref name="fieldCount" /> Лежит в диапазоне от 0 до 4.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.String" /> Представление значения основной и дополнительный номера, построения и редакции компонентов текущего <see cref="T:System.Version" /> объекта, отделяя каждое символ точки (".").
    ///   <paramref name="fieldCount" /> Параметр определяет, какое количество компонентов возвращается.
    /// 
    ///           fieldCount
    /// 
    ///           Возвращаемое значение
    /// 
    ///           0
    /// 
    ///           Пустая строка (»»).
    /// 
    ///           1
    /// 
    ///           основной номер версии
    /// 
    ///           2
    /// 
    ///           ОсновнойНомерВерсии.ДополнительныйНомерВерсии
    /// 
    ///           3
    /// 
    ///           ОсновнойНомерВерсии.ДополнительныйНомерВерсии.НомерПостроения
    /// 
    ///           4
    /// 
    ///           ОсновнойНомерВерсии.ДополнительныйНомерВерсии.НомерПостроения.НомерРедакции
    /// 
    ///   Например, если вы создаете <see cref="T:System.Version" /> с помощью конструктора по Version(1,3,5), ToString(2) возвращает «1.3» и ToString(4) приводит к возникновению исключения.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="fieldCount" />— меньше 0 или больше 4.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="fieldCount" />больше, чем количество компонентов, определенные в текущем <see cref="T:System.Version" /> объекта.
    /// </exception>
    [__DynamicallyInvokable]
    public string ToString(int fieldCount)
    {
      switch (fieldCount)
      {
        case 0:
          return string.Empty;
        case 1:
          return this._Major.ToString();
        case 2:
          StringBuilder sb1 = StringBuilderCache.Acquire(16);
          Version.AppendPositiveNumber(this._Major, sb1);
          sb1.Append('.');
          Version.AppendPositiveNumber(this._Minor, sb1);
          return StringBuilderCache.GetStringAndRelease(sb1);
        default:
          if (this._Build == -1)
            throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper", (object) "0", (object) "2"), nameof (fieldCount));
          if (fieldCount == 3)
          {
            StringBuilder sb2 = StringBuilderCache.Acquire(16);
            Version.AppendPositiveNumber(this._Major, sb2);
            sb2.Append('.');
            Version.AppendPositiveNumber(this._Minor, sb2);
            sb2.Append('.');
            Version.AppendPositiveNumber(this._Build, sb2);
            return StringBuilderCache.GetStringAndRelease(sb2);
          }
          if (this._Revision == -1)
            throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper", (object) "0", (object) "3"), nameof (fieldCount));
          if (fieldCount == 4)
          {
            StringBuilder sb2 = StringBuilderCache.Acquire(16);
            Version.AppendPositiveNumber(this._Major, sb2);
            sb2.Append('.');
            Version.AppendPositiveNumber(this._Minor, sb2);
            sb2.Append('.');
            Version.AppendPositiveNumber(this._Build, sb2);
            sb2.Append('.');
            Version.AppendPositiveNumber(this._Revision, sb2);
            return StringBuilderCache.GetStringAndRelease(sb2);
          }
          throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper", (object) "0", (object) "4"), nameof (fieldCount));
      }
    }

    private static void AppendPositiveNumber(int num, StringBuilder sb)
    {
      int length = sb.Length;
      do
      {
        int num1 = num % 10;
        num /= 10;
        sb.Insert(length, (char) (48 + num1));
      }
      while (num > 0);
    }

    /// <summary>
    ///   Преобразует строковое представление номера версии в эквивалентный <see cref="T:System.Version" /> объекта.
    /// </summary>
    /// <param name="input">
    ///   Строка, содержащая преобразуемый номер версии.
    /// </param>
    /// <returns>
    ///   Объект, который соответствует номеру версии, указанной в <paramref name="input" /> параметра.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="input" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="input" />имеет менее двух или более четырех компонентов версии.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Хотя бы один компонент в <paramref name="input" /> меньше нуля.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Хотя бы один компонент в <paramref name="input" /> не является целым числом.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Хотя бы один компонент в <paramref name="input" /> представляет число, которое больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Version Parse(string input)
    {
      if (input == null)
        throw new ArgumentNullException(nameof (input));
      Version.VersionResult result = new Version.VersionResult();
      result.Init(nameof (input), true);
      if (!Version.TryParseVersion(input, ref result))
        throw result.GetVersionParseException();
      return result.m_parsedVersion;
    }

    /// <summary>
    ///   Предпринимает попытку преобразовать строковое представление номера версии в эквивалентный <see cref="T:System.Version" /> объекта и возвращает значение, указывающее, успешно ли выполнено преобразование.
    /// </summary>
    /// <param name="input">
    ///   Строка, содержащая преобразуемый номер версии.
    /// </param>
    /// <param name="result">
    ///   При возвращении этого метода содержит <see cref="T:System.Version" /> эквивалентное число, которое содержится в <paramref name="input" />, если преобразование выполнено успешно, или <see cref="T:System.Version" /> объекта которого основной и дополнительный номера версии равны 0, если преобразование завершилось неудачей.
    ///    Если <paramref name="input" /> — <see langword="null" /> или <see cref="F:System.String.Empty" />, <paramref name="result" /> — <see langword="null" /> когда этот метод возвращает.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр <paramref name="input" /> успешно преобразован; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool TryParse(string input, out Version result)
    {
      Version.VersionResult result1 = new Version.VersionResult();
      result1.Init(nameof (input), false);
      bool version = Version.TryParseVersion(input, ref result1);
      result = result1.m_parsedVersion;
      return version;
    }

    private static bool TryParseVersion(string version, ref Version.VersionResult result)
    {
      if (version == null)
      {
        result.SetFailure(Version.ParseFailureKind.ArgumentNullException);
        return false;
      }
      string[] strArray = version.Split(Version.SeparatorsArray);
      int length = strArray.Length;
      if (length < 2 || length > 4)
      {
        result.SetFailure(Version.ParseFailureKind.ArgumentException);
        return false;
      }
      int parsedComponent1;
      int parsedComponent2;
      if (!Version.TryParseComponent(strArray[0], nameof (version), ref result, out parsedComponent1) || !Version.TryParseComponent(strArray[1], nameof (version), ref result, out parsedComponent2))
        return false;
      int num = length - 2;
      if (num > 0)
      {
        int parsedComponent3;
        if (!Version.TryParseComponent(strArray[2], "build", ref result, out parsedComponent3))
          return false;
        if (num - 1 > 0)
        {
          int parsedComponent4;
          if (!Version.TryParseComponent(strArray[3], "revision", ref result, out parsedComponent4))
            return false;
          result.m_parsedVersion = new Version(parsedComponent1, parsedComponent2, parsedComponent3, parsedComponent4);
        }
        else
          result.m_parsedVersion = new Version(parsedComponent1, parsedComponent2, parsedComponent3);
      }
      else
        result.m_parsedVersion = new Version(parsedComponent1, parsedComponent2);
      return true;
    }

    private static bool TryParseComponent(string component, string componentName, ref Version.VersionResult result, out int parsedComponent)
    {
      if (!int.TryParse(component, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out parsedComponent))
      {
        result.SetFailure(Version.ParseFailureKind.FormatException, component);
        return false;
      }
      if (parsedComponent >= 0)
        return true;
      result.SetFailure(Version.ParseFailureKind.ArgumentOutOfRangeException, componentName);
      return false;
    }

    /// <summary>
    ///   Определение равенства двух заданных объектов <see cref="T:System.Version" />.
    /// </summary>
    /// <param name="v1">
    ///   Первый объект <see cref="T:System.Version" />.
    /// </param>
    /// <param name="v2">
    ///   Второй объект <see cref="T:System.Version" />.
    /// </param>
    /// <returns>
    ///   Если значение <paramref name="v1" /> равно <paramref name="v2" />, значение <see langword="true" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(Version v1, Version v2)
    {
      if ((object) v1 == null)
        return (object) v2 == null;
      return v1.Equals(v2);
    }

    /// <summary>
    ///   Определение неравенства двух заданных объектов <see cref="T:System.Version" />.
    /// </summary>
    /// <param name="v1">
    ///   Первый объект <see cref="T:System.Version" />.
    /// </param>
    /// <param name="v2">
    ///   Второй объект <see cref="T:System.Version" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значения параметров <paramref name="v1" /> и <paramref name="v2" /> не равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(Version v1, Version v2)
    {
      return !(v1 == v2);
    }

    /// <summary>
    ///   Определяет, является ли указанное <see cref="T:System.Version" /> объект меньше, чем второй указан <see cref="T:System.Version" /> объекта.
    /// </summary>
    /// <param name="v1">
    ///   Первый объект <see cref="T:System.Version" />.
    /// </param>
    /// <param name="v2">
    ///   Второй объект <see cref="T:System.Version" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение <paramref name="v1" /> меньше значения <paramref name="v2" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="v1" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool operator <(Version v1, Version v2)
    {
      if ((object) v1 == null)
        throw new ArgumentNullException(nameof (v1));
      return v1.CompareTo(v2) < 0;
    }

    /// <summary>
    ///   Определяет, является ли значение первого указанного <see cref="T:System.Version" /> объекта меньше или равно значению второго <see cref="T:System.Version" /> объекта.
    /// </summary>
    /// <param name="v1">
    ///   Первый объект <see cref="T:System.Version" />.
    /// </param>
    /// <param name="v2">
    ///   Второй объект <see cref="T:System.Version" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение <paramref name="v1" /> меньше или равно значению <paramref name="v2" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="v1" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool operator <=(Version v1, Version v2)
    {
      if ((object) v1 == null)
        throw new ArgumentNullException(nameof (v1));
      return v1.CompareTo(v2) <= 0;
    }

    /// <summary>
    ///   Определяет, является ли указанное <see cref="T:System.Version" /> объект больше второго заданного <see cref="T:System.Version" /> объекта.
    /// </summary>
    /// <param name="v1">
    ///   Первый объект <see cref="T:System.Version" />.
    /// </param>
    /// <param name="v2">
    ///   Второй объект <see cref="T:System.Version" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если <paramref name="v1" /> больше <paramref name="v2" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator >(Version v1, Version v2)
    {
      return v2 < v1;
    }

    /// <summary>
    ///   Определяет, является ли значение первого указанного <see cref="T:System.Version" /> объекта больше или равно значению второго указанного <see cref="T:System.Version" /> объекта.
    /// </summary>
    /// <param name="v1">
    ///   Первый объект <see cref="T:System.Version" />.
    /// </param>
    /// <param name="v2">
    ///   Второй объект <see cref="T:System.Version" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение <paramref name="v1" /> больше или равно значению <paramref name="v2" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator >=(Version v1, Version v2)
    {
      return v2 <= v1;
    }

    internal enum ParseFailureKind
    {
      ArgumentNullException,
      ArgumentException,
      ArgumentOutOfRangeException,
      FormatException,
    }

    internal struct VersionResult
    {
      internal Version m_parsedVersion;
      internal Version.ParseFailureKind m_failure;
      internal string m_exceptionArgument;
      internal string m_argumentName;
      internal bool m_canThrow;

      internal void Init(string argumentName, bool canThrow)
      {
        this.m_canThrow = canThrow;
        this.m_argumentName = argumentName;
      }

      internal void SetFailure(Version.ParseFailureKind failure)
      {
        this.SetFailure(failure, string.Empty);
      }

      internal void SetFailure(Version.ParseFailureKind failure, string argument)
      {
        this.m_failure = failure;
        this.m_exceptionArgument = argument;
        if (this.m_canThrow)
          throw this.GetVersionParseException();
      }

      internal Exception GetVersionParseException()
      {
        switch (this.m_failure)
        {
          case Version.ParseFailureKind.ArgumentNullException:
            return (Exception) new ArgumentNullException(this.m_argumentName);
          case Version.ParseFailureKind.ArgumentException:
            return (Exception) new ArgumentException(Environment.GetResourceString("Arg_VersionString"));
          case Version.ParseFailureKind.ArgumentOutOfRangeException:
            return (Exception) new ArgumentOutOfRangeException(this.m_exceptionArgument, Environment.GetResourceString("ArgumentOutOfRange_Version"));
          case Version.ParseFailureKind.FormatException:
            try
            {
              int.Parse(this.m_exceptionArgument, (IFormatProvider) CultureInfo.InvariantCulture);
            }
            catch (FormatException ex)
            {
              return (Exception) ex;
            }
            catch (OverflowException ex)
            {
              return (Exception) ex;
            }
            return (Exception) new FormatException(Environment.GetResourceString("Format_InvalidString"));
          default:
            return (Exception) new ArgumentException(Environment.GetResourceString("Arg_VersionString"));
        }
      }
    }
  }
}
