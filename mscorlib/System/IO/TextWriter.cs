// Decompiled with JetBrains decompiler
// Type: System.IO.TextWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>
  ///   Представляет модуль записи, который может записывать последовательные наборы символов.
  ///    Это абстрактный класс.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class TextWriter : MarshalByRefObject, IDisposable
  {
    /// <summary>
    ///   Предоставляет <see langword="TextWriter" /> без резервного хранилища, в который можно осуществлять запись, но из которого нельзя считывать данные.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly TextWriter Null = (TextWriter) new TextWriter.NullTextWriter();
    [NonSerialized]
    private static Action<object> _WriteCharDelegate = (Action<object>) (state =>
    {
      Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>) state;
      tuple.Item1.Write(tuple.Item2);
    });
    [NonSerialized]
    private static Action<object> _WriteStringDelegate = (Action<object>) (state =>
    {
      Tuple<TextWriter, string> tuple = (Tuple<TextWriter, string>) state;
      tuple.Item1.Write(tuple.Item2);
    });
    [NonSerialized]
    private static Action<object> _WriteCharArrayRangeDelegate = (Action<object>) (state =>
    {
      Tuple<TextWriter, char[], int, int> tuple = (Tuple<TextWriter, char[], int, int>) state;
      tuple.Item1.Write(tuple.Item2, tuple.Item3, tuple.Item4);
    });
    [NonSerialized]
    private static Action<object> _WriteLineCharDelegate = (Action<object>) (state =>
    {
      Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>) state;
      tuple.Item1.WriteLine(tuple.Item2);
    });
    [NonSerialized]
    private static Action<object> _WriteLineStringDelegate = (Action<object>) (state =>
    {
      Tuple<TextWriter, string> tuple = (Tuple<TextWriter, string>) state;
      tuple.Item1.WriteLine(tuple.Item2);
    });
    [NonSerialized]
    private static Action<object> _WriteLineCharArrayRangeDelegate = (Action<object>) (state =>
    {
      Tuple<TextWriter, char[], int, int> tuple = (Tuple<TextWriter, char[], int, int>) state;
      tuple.Item1.WriteLine(tuple.Item2, tuple.Item3, tuple.Item4);
    });
    [NonSerialized]
    private static Action<object> _FlushDelegate = (Action<object>) (state => ((TextWriter) state).Flush());
    /// <summary>
    ///   Сохраняет символы новой строки, используемые для данного <see langword="TextWriter" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected char[] CoreNewLine = new char[2]{ '\r', '\n' };
    private const string InitialNewLine = "\r\n";
    private IFormatProvider InternalFormatProvider;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.TextWriter" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected TextWriter()
    {
      this.InternalFormatProvider = (IFormatProvider) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.TextWriter" /> указанным поставщиком формата.
    /// </summary>
    /// <param name="formatProvider">
    ///   Объект <see cref="T:System.IFormatProvider" />, управляющий форматированием.
    /// </param>
    [__DynamicallyInvokable]
    protected TextWriter(IFormatProvider formatProvider)
    {
      this.InternalFormatProvider = formatProvider;
    }

    /// <summary>Возвращает объект, управляющий форматированием.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.IFormatProvider" /> для указанного языка и региональных параметров или форматирование текущего языка и региональных параметров, если не заданы другие.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual IFormatProvider FormatProvider
    {
      [__DynamicallyInvokable] get
      {
        if (this.InternalFormatProvider == null)
          return (IFormatProvider) Thread.CurrentThread.CurrentCulture;
        return this.InternalFormatProvider;
      }
    }

    /// <summary>
    ///   Закрывает текущий модуль записи и освобождает все системные ресурсы, связанные с ним.
    /// </summary>
    public virtual void Close()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.IO.TextWriter" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые объектом <see cref="T:System.IO.TextWriter" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Очищает все буферы текущего модуля записи и вызывает немедленную запись всех буферизованных данных на базовое устройство.
    /// </summary>
    [__DynamicallyInvokable]
    public virtual void Flush()
    {
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает кодировку символов, в которой записаны выходные данные.
    /// </summary>
    /// <returns>
    ///   Кодировка символов, в которой осуществляется запись выходных данных.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract Encoding Encoding { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает или задает признак конца строки, используемой текущим <see langword="TextWriter" />.
    /// </summary>
    /// <returns>
    ///   Признак конца строки для текущего <see langword="TextWriter" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string NewLine
    {
      [__DynamicallyInvokable] get
      {
        return new string(this.CoreNewLine);
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          value = "\r\n";
        this.CoreNewLine = value.ToCharArray();
      }
    }

    /// <summary>
    ///   Создает потокобезопасную оболочку для указанного объекта <see langword="TextWriter" />.
    /// </summary>
    /// <param name="writer">
    ///   Коллекция <see langword="TextWriter" />, которую требуется синхронизировать.
    /// </param>
    /// <returns>Потокобезопасная программа-оболочка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="writer" /> имеет значение <see langword="null" />.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static TextWriter Synchronized(TextWriter writer)
    {
      if (writer == null)
        throw new ArgumentNullException(nameof (writer));
      if (writer is TextWriter.SyncTextWriter)
        return writer;
      return (TextWriter) new TextWriter.SyncTextWriter(writer);
    }

    /// <summary>
    ///   Выполняет запись символа в текстовую строку или поток.
    /// </summary>
    /// <param name="value">
    ///   Символ, записываемый в текстовый поток.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(char value)
    {
    }

    /// <summary>
    ///   Выполняет запись массива символов в текстовую строку или поток.
    /// </summary>
    /// <param name="buffer">
    ///   Массив символов, записываемый в текстовый поток.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(char[] buffer)
    {
      if (buffer == null)
        return;
      this.Write(buffer, 0, buffer.Length);
    }

    /// <summary>
    ///   Записывает дочерний массив символов в текстовую строку или поток.
    /// </summary>
    /// <param name="buffer">
    ///   Массив символов, из которого записываются данные.
    /// </param>
    /// <param name="index">
    ///   Положение символа в буфере, с которого начинается извлечение данных.
    /// </param>
    /// <param name="count">Количество символов для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      for (int index1 = 0; index1 < count; ++index1)
        this.Write(buffer[index + index1]);
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление значения <see langword="Boolean" />.
    /// </summary>
    /// <param name="value">
    ///   Значение <see langword="Boolean" /> для записи.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(bool value)
    {
      this.Write(value ? "True" : "False");
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление целого числа со знаком размером 4 байта.
    /// </summary>
    /// <param name="value">
    ///   Записываемое целое число со знаком размером 4 байта.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(int value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление целого числа без знака размером 4 байта.
    /// </summary>
    /// <param name="value">
    ///   Записываемое целое число без знака размером 4 байта.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void Write(uint value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление целого числа со знаком размером 8 байт.
    /// </summary>
    /// <param name="value">
    ///   Записываемое целое число со знаком размером 8 байт.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(long value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление целого числа без знака размером 8 байт.
    /// </summary>
    /// <param name="value">
    ///   Записываемое целое число без знака размером 8 байт.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void Write(ulong value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление значения с плавающей запятой размером 4 байта.
    /// </summary>
    /// <param name="value">
    ///   Записываемое значение с плавающей запятой размером 4 байта.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(float value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление значения с плавающей запятой размером 8 байт.
    /// </summary>
    /// <param name="value">
    ///   Записываемое значение с плавающей запятой размером 8 байт.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(double value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>
    ///   Записывает текстовое представление десятичного значения в текстовую строку или поток.
    /// </summary>
    /// <param name="value">
    ///   Десятичное значение, которое необходимо записать.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(Decimal value)
    {
      this.Write(value.ToString(this.FormatProvider));
    }

    /// <summary>
    ///   Асинхронно записывает строку в текстовую строку или поток.
    /// </summary>
    /// <param name="value">Строка для записи.</param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(string value)
    {
      if (value == null)
        return;
      this.Write(value.ToCharArray());
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление объекта с помощью вызова метода <see langword="ToString" /> для этого объекта.
    /// </summary>
    /// <param name="value">Записываемый объект.</param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(object value)
    {
      if (value == null)
        return;
      IFormattable formattable = value as IFormattable;
      if (formattable != null)
        this.Write(formattable.ToString((string) null, this.FormatProvider));
      else
        this.Write(value.ToString());
    }

    /// <summary>
    ///   Записывает форматированную строку в текстовую строку или поток, используя ту же семантику, что и метод <see cref="M:System.String.Format(System.String,System.Object)" />.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">Объект для форматирования и записи.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> не является допустимым составного формата строкой.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формат меньше 0 (ноль) или больше или равно числу объектов для форматирования (который перегрузка метода — это).
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(string format, object arg0)
    {
      this.Write(string.Format(this.FormatProvider, format, arg0));
    }

    /// <summary>
    ///   Записывает форматированную строку в текстовую строку или поток, используя ту же семантику, что и метод <see cref="M:System.String.Format(System.String,System.Object,System.Object)" />.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">
    ///   Первый объект для форматирования и записи.
    /// </param>
    /// <param name="arg1">
    ///   Второй объект для форматирования и записи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> не является допустимым составного формата строкой.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формат меньше 0 (нуля) или больше или равно числу объектов для форматирования (который для перегрузка метода используется 2).
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(string format, object arg0, object arg1)
    {
      this.Write(string.Format(this.FormatProvider, format, arg0, arg1));
    }

    /// <summary>
    ///   Записывает форматированную строку в текстовую строку или поток, используя ту же семантику, что и метод <see cref="M:System.String.Format(System.String,System.Object,System.Object,System.Object)" />.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">
    ///   Первый объект для форматирования и записи.
    /// </param>
    /// <param name="arg1">
    ///   Второй объект для форматирования и записи.
    /// </param>
    /// <param name="arg2">
    ///   Третий объект для форматирования и записи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> не является допустимым составного формата строкой.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формат меньше 0 (ноль) или больше или равно числу объектов для форматирования (который для этой перегрузки метода — три).
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(string format, object arg0, object arg1, object arg2)
    {
      this.Write(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
    }

    /// <summary>
    ///   Записывает форматированную строку в текстовую строку или поток, используя ту же семантику, что и метод <see cref="M:System.String.Format(System.String,System.Object[])" />.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg">
    ///   Массив объектов, содержащий от нуля и более объектов, которые необходимо форматировать и записать.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="format" /> или <paramref name="arg" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" />не является допустимым составного форматирования строкой.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формата меньше нуля или больше либо равен длине массива <paramref name="arg" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(string format, params object[] arg)
    {
      this.Write(string.Format(this.FormatProvider, format, arg));
    }

    /// <summary>
    ///   Записывает признак конца строки в текстовую строку или поток.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine()
    {
      this.Write(this.CoreNewLine);
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток символ, за которым следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Символ, записываемый в текстовый поток.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(char value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток массив символов, за которыми следует признак конца строки.
    /// </summary>
    /// <param name="buffer">
    ///   Массив символов, из которого считываются данные.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(char[] buffer)
    {
      this.Write(buffer);
      this.WriteLine();
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток дочерний массив символов, за которыми следует признак конца строки.
    /// </summary>
    /// <param name="buffer">
    ///   Массив символов, из которого считываются данные.
    /// </param>
    /// <param name="index">
    ///   Положение символа в <paramref name="buffer" />, с которого начинается чтение данных.
    /// </param>
    /// <param name="count">
    ///   Наибольшее количество символов для записи.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(char[] buffer, int index, int count)
    {
      this.Write(buffer, index, count);
      this.WriteLine();
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление значения <see langword="Boolean" />, за которым следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Значение <see langword="Boolean" /> для записи.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(bool value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление целого числа со знаком размером 4 байта, за которым следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Записываемое целое число со знаком размером 4 байта.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(int value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление целого числа без знака размером 4 байта, за которым следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Записываемое целое число без знака размером 4 байта.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void WriteLine(uint value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление целого числа со знаком размером 8 байт, за которым следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Записываемое целое число со знаком размером 8 байт.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(long value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление целого числа без знака размером 8 байт, за которым следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Записываемое целое число без знака размером 8 байт.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void WriteLine(ulong value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление значения с плавающей запятой размером 4 байта, за которым следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Записываемое значение с плавающей запятой размером 4 байта.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(float value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление значения с плавающей запятой размером 8 байта, за которым следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Записываемое значение с плавающей запятой размером 8 байт.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(double value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление десятичного значения, за которым следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Десятичное значение, которое необходимо записать.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(Decimal value)
    {
      this.Write(value);
      this.WriteLine();
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток строку, за которой следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Строка для записи.
    ///    Если <paramref name="value" /> имеет значение <see langword="null" />, записывается только признак конца строки.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(string value)
    {
      if (value == null)
      {
        this.WriteLine();
      }
      else
      {
        int length1 = value.Length;
        int length2 = this.CoreNewLine.Length;
        char[] chArray = new char[length1 + length2];
        value.CopyTo(0, chArray, 0, length1);
        switch (length2)
        {
          case 1:
            chArray[length1] = this.CoreNewLine[0];
            break;
          case 2:
            chArray[length1] = this.CoreNewLine[0];
            chArray[length1 + 1] = this.CoreNewLine[1];
            break;
          default:
            Buffer.InternalBlockCopy((Array) this.CoreNewLine, 0, (Array) chArray, length1 * 2, length2 * 2);
            break;
        }
        this.Write(chArray, 0, length1 + length2);
      }
    }

    /// <summary>
    ///   Записывает в текстовую строку или поток текстовое представление объекта путем вызова метода <see langword="ToString" /> для этого объекта, за которым следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Записываемый объект.
    ///    Если <paramref name="value" /> имеет значение <see langword="null" />, записывается только признак конца строки.
    /// </param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(object value)
    {
      if (value == null)
      {
        this.WriteLine();
      }
      else
      {
        IFormattable formattable = value as IFormattable;
        if (formattable != null)
          this.WriteLine(formattable.ToString((string) null, this.FormatProvider));
        else
          this.WriteLine(value.ToString());
      }
    }

    /// <summary>
    ///   Записывает форматированную строку и новую строку в текстовую строку или поток, используя ту же семантику, что и метод <see cref="M:System.String.Format(System.String,System.Object)" />.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">Объект для форматирования и записи.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> не является допустимым составного формата строкой.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формат меньше 0 (ноль) или больше или равно числу объектов для форматирования (который перегрузка метода — это).
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(string format, object arg0)
    {
      this.WriteLine(string.Format(this.FormatProvider, format, arg0));
    }

    /// <summary>
    ///   Записывает форматированную строку и новую строку в текстовую строку или поток, используя ту же семантику, что и метод <see cref="M:System.String.Format(System.String,System.Object,System.Object)" />.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">
    ///   Первый объект для форматирования и записи.
    /// </param>
    /// <param name="arg1">
    ///   Второй объект для форматирования и записи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> не является допустимым составного формата строкой.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формат меньше 0 (ноль) или больше или равно числу объектов для форматирования (который для перегрузка метода используется 2).
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(string format, object arg0, object arg1)
    {
      this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1));
    }

    /// <summary>
    ///   Записывает отформатированную строку и новую строку, используя ту же семантику, что и <see cref="M:System.String.Format(System.String,System.Object)" />.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg0">
    ///   Первый объект для форматирования и записи.
    /// </param>
    /// <param name="arg1">
    ///   Второй объект для форматирования и записи.
    /// </param>
    /// <param name="arg2">
    ///   Третий объект для форматирования и записи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> не является допустимым составного формата строкой.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формат меньше 0 (ноль) или больше или равно числу объектов для форматирования (который для этой перегрузки метода — три).
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(string format, object arg0, object arg1, object arg2)
    {
      this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
    }

    /// <summary>
    ///   Записывает отформатированную строку и новую строку, используя ту же семантику, что и <see cref="M:System.String.Format(System.String,System.Object)" />.
    /// </summary>
    /// <param name="format">
    ///   Строка составного формата (см. примечания).
    /// </param>
    /// <param name="arg">
    ///   Массив объектов, содержащий от нуля и более объектов, которые необходимо форматировать и записать.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Строка или объект передается в качестве <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextWriter" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   <paramref name="format" /> не является допустимым составного формата строкой.
    /// 
    ///   -или-
    /// 
    ///   Индекс элемента формат меньше 0 (ноль) или больше или равно длине <paramref name="arg" /> массива.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void WriteLine(string format, params object[] arg)
    {
      this.WriteLine(string.Format(this.FormatProvider, format, arg));
    }

    /// <summary>
    ///   Выполняет асинхронную запись символа в текстовую строку или поток.
    /// </summary>
    /// <param name="value">
    ///   Символ, записываемый в текстовый поток.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи текста, удаляется.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи текста в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteAsync(char value)
    {
      Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
      return Task.Factory.StartNew(TextWriter._WriteCharDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>
    ///   Выполняет асинхронную запись строки в текстовую строку или поток.
    /// </summary>
    /// <param name="value">
    ///   Строка для записи.
    ///    Если параметр <paramref name="value" /> имеет значение <see langword="null" />, в текстовый поток ничего не записывается.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи текста, удаляется.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи текста в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteAsync(string value)
    {
      Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
      return Task.Factory.StartNew(TextWriter._WriteStringDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>
    ///   Выполняет асинхронную запись массива символов в текстовую строку или поток.
    /// </summary>
    /// <param name="buffer">
    ///   Массив символов, записываемый в текстовый поток.
    ///    Если <paramref name="buffer" /> имеет значение <see langword="null" />, запись не выполняется.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи текста, удаляется.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи текста в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public Task WriteAsync(char[] buffer)
    {
      if (buffer == null)
        return Task.CompletedTask;
      return this.WriteAsync(buffer, 0, buffer.Length);
    }

    /// <summary>
    ///   Асинхронно записывает дочерний массив символов в текстовую строку или поток.
    /// </summary>
    /// <param name="buffer">
    ///   Массив символов, из которого записываются данные.
    /// </param>
    /// <param name="index">
    ///   Положение символа в буфере, с которого начинается извлечение данных.
    /// </param>
    /// <param name="count">Количество символов для записи.</param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма значений <paramref name="index" /> и <paramref name="count" /> превышает длину буфера.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи текста, удаляется.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи текста в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteAsync(char[] buffer, int index, int count)
    {
      Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
      return Task.Factory.StartNew(TextWriter._WriteCharArrayRangeDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>
    ///   Асинхронно записывает в текстовую строку или поток символ, за которым следует признак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Символ, записываемый в текстовый поток.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи текста, удаляется.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи текста в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteLineAsync(char value)
    {
      Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
      return Task.Factory.StartNew(TextWriter._WriteLineCharDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>
    ///   Асинхронно записывает в текстовую строку или поток строку, за которой следует знак конца строки.
    /// </summary>
    /// <param name="value">
    ///   Строка для записи.
    ///    Если значение равно <see langword="null" />, записывается только знак конца строки.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи текста, удаляется.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи текста в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteLineAsync(string value)
    {
      Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
      return Task.Factory.StartNew(TextWriter._WriteLineStringDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>
    ///   Асинхронно записывает в текстовую строку или поток массив символов, за которыми следует признак конца строки.
    /// </summary>
    /// <param name="buffer">
    ///   Массив символов, записываемый в текстовый поток.
    ///    Если массив символов имеет значение <see langword="null" />, записывается только признак конца строки.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи текста, удаляется.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи текста в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public Task WriteLineAsync(char[] buffer)
    {
      if (buffer == null)
        return Task.CompletedTask;
      return this.WriteLineAsync(buffer, 0, buffer.Length);
    }

    /// <summary>
    ///   Асинхронно записывает в текстовую строку или поток дочерний массив символов, за которыми следует признак конца строки.
    /// </summary>
    /// <param name="buffer">
    ///   Массив символов, из которого записываются данные.
    /// </param>
    /// <param name="index">
    ///   Положение символа в буфере, с которого начинается извлечение данных.
    /// </param>
    /// <param name="count">Количество символов для записи.</param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма значений <paramref name="index" /> и <paramref name="count" /> превышает длину буфера.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи текста, удаляется.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи текста в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteLineAsync(char[] buffer, int index, int count)
    {
      Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
      return Task.Factory.StartNew(TextWriter._WriteLineCharArrayRangeDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>
    ///   Асинхронно записывает признак конца строки в текстовую строку или поток.
    /// </summary>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи текста, удаляется.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи текста в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task WriteLineAsync()
    {
      return this.WriteAsync(this.CoreNewLine);
    }

    /// <summary>
    ///   Асинхронно очищает все буферы текущего средства записи и вызывает запись всех буферизованных данных в базовое устройство.
    /// </summary>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию очистки.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи текста, удаляется.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи в данный момент используется предыдущая операция записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task FlushAsync()
    {
      return Task.Factory.StartNew(TextWriter._FlushDelegate, (object) this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    [Serializable]
    private sealed class NullTextWriter : TextWriter
    {
      internal NullTextWriter()
        : base((IFormatProvider) CultureInfo.InvariantCulture)
      {
      }

      public override Encoding Encoding
      {
        get
        {
          return Encoding.Default;
        }
      }

      public override void Write(char[] buffer, int index, int count)
      {
      }

      public override void Write(string value)
      {
      }

      public override void WriteLine()
      {
      }

      public override void WriteLine(string value)
      {
      }

      public override void WriteLine(object value)
      {
      }
    }

    [Serializable]
    internal sealed class SyncTextWriter : TextWriter, IDisposable
    {
      private TextWriter _out;

      internal SyncTextWriter(TextWriter t)
        : base(t.FormatProvider)
      {
        this._out = t;
      }

      public override Encoding Encoding
      {
        get
        {
          return this._out.Encoding;
        }
      }

      public override IFormatProvider FormatProvider
      {
        get
        {
          return this._out.FormatProvider;
        }
      }

      public override string NewLine
      {
        [MethodImpl(MethodImplOptions.Synchronized)] get
        {
          return this._out.NewLine;
        }
        [MethodImpl(MethodImplOptions.Synchronized)] set
        {
          this._out.NewLine = value;
        }
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Close()
      {
        this._out.Close();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      protected override void Dispose(bool disposing)
      {
        if (!disposing)
          return;
        this._out.Dispose();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Flush()
      {
        this._out.Flush();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(char value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(char[] buffer)
      {
        this._out.Write(buffer);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(char[] buffer, int index, int count)
      {
        this._out.Write(buffer, index, count);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(bool value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(int value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(uint value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(long value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(ulong value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(float value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(double value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(Decimal value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(string value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(object value)
      {
        this._out.Write(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(string format, object arg0)
      {
        this._out.Write(format, arg0);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(string format, object arg0, object arg1)
      {
        this._out.Write(format, arg0, arg1);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(string format, object arg0, object arg1, object arg2)
      {
        this._out.Write(format, arg0, arg1, arg2);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Write(string format, params object[] arg)
      {
        this._out.Write(format, arg);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine()
      {
        this._out.WriteLine();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(char value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(Decimal value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(char[] buffer)
      {
        this._out.WriteLine(buffer);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(char[] buffer, int index, int count)
      {
        this._out.WriteLine(buffer, index, count);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(bool value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(int value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(uint value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(long value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(ulong value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(float value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(double value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(string value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(object value)
      {
        this._out.WriteLine(value);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(string format, object arg0)
      {
        this._out.WriteLine(format, arg0);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(string format, object arg0, object arg1)
      {
        this._out.WriteLine(format, arg0, arg1);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(string format, object arg0, object arg1, object arg2)
      {
        this._out.WriteLine(format, arg0, arg1, arg2);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void WriteLine(string format, params object[] arg)
      {
        this._out.WriteLine(format, arg);
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task WriteAsync(char value)
      {
        this.Write(value);
        return Task.CompletedTask;
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task WriteAsync(string value)
      {
        this.Write(value);
        return Task.CompletedTask;
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task WriteAsync(char[] buffer, int index, int count)
      {
        this.Write(buffer, index, count);
        return Task.CompletedTask;
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task WriteLineAsync(char value)
      {
        this.WriteLine(value);
        return Task.CompletedTask;
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task WriteLineAsync(string value)
      {
        this.WriteLine(value);
        return Task.CompletedTask;
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task WriteLineAsync(char[] buffer, int index, int count)
      {
        this.WriteLine(buffer, index, count);
        return Task.CompletedTask;
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task FlushAsync()
      {
        this.Flush();
        return Task.CompletedTask;
      }
    }
  }
}
