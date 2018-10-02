// Decompiled with JetBrains decompiler
// Type: System.IO.StringReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>
  ///   Реализует <see cref="T:System.IO.TextReader" /> считывает данные из строки.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class StringReader : TextReader
  {
    private string _s;
    private int _pos;
    private int _length;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.StringReader" /> класс, который считывает из указанной строки.
    /// </summary>
    /// <param name="s">
    ///   Строки, на которую <see cref="T:System.IO.StringReader" /> должен быть инициализирован.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringReader(string s)
    {
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      this._s = s;
      this._length = s == null ? 0 : s.Length;
    }

    /// <summary>
    ///   Закрывает объект <see cref="T:System.IO.StringReader" />.
    /// </summary>
    public override void Close()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.IO.StringReader" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected override void Dispose(bool disposing)
    {
      this._s = (string) null;
      this._pos = 0;
      this._length = 0;
      base.Dispose(disposing);
    }

    /// <summary>
    ///   Возвращает следующий доступный символ, но не использует его.
    /// </summary>
    /// <returns>
    ///   Целое число, представляющее следующий символ для прочтения, или -1, если доступных символов больше нет или поток не поддерживает поиск.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущее средство чтения закрывается.
    /// </exception>
    [__DynamicallyInvokable]
    public override int Peek()
    {
      if (this._s == null)
        __Error.ReaderClosed();
      if (this._pos == this._length)
        return -1;
      return (int) this._s[this._pos];
    }

    /// <summary>
    ///   Считывает следующий символ из входной строки и перемещает положение символа на один символ.
    /// </summary>
    /// <returns>
    ///   Следующий символ из основной строки или значение -1, если доступных символов больше нет.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущее средство чтения закрывается.
    /// </exception>
    [__DynamicallyInvokable]
    public override int Read()
    {
      if (this._s == null)
        __Error.ReaderClosed();
      if (this._pos == this._length)
        return -1;
      return (int) this._s[this._pos++];
    }

    /// <summary>
    ///   Считывает блок символов из строки ввода и перемещает положение символа, <paramref name="count" />.
    /// </summary>
    /// <param name="buffer">
    ///   При возвращении из этого метода содержит указанный массив символов, в котором значения в интервале от <paramref name="index" /> и (<paramref name="index" /> + <paramref name="count" /> - 1) заменены символами, считанными из текущего источника.
    /// </param>
    /// <param name="index">Начальный индекс в буфере.</param>
    /// <param name="count">Число символов для чтения.</param>
    /// <returns>
    ///   Общее количество символов, считанных в буфер.
    ///    Это может быть меньше запрошенного числа символов, если что число символов в настоящее время недоступны, или ноль, если достигнут конец основной строки.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущее средство чтения закрывается.
    /// </exception>
    [__DynamicallyInvokable]
    public override int Read([In, Out] char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this._s == null)
        __Error.ReaderClosed();
      int count1 = this._length - this._pos;
      if (count1 > 0)
      {
        if (count1 > count)
          count1 = count;
        this._s.CopyTo(this._pos, buffer, index, count1);
        this._pos += count1;
      }
      return count1;
    }

    /// <summary>
    ///   Считывает все символы, начиная с текущей позиции до конца строки и возвращает их в виде одной строки.
    /// </summary>
    /// <returns>
    ///   Содержимое, начиная с текущей позиции до конца основной строки.
    /// </returns>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти для выделения буфера для возвращаемой строки.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущее средство чтения закрывается.
    /// </exception>
    [__DynamicallyInvokable]
    public override string ReadToEnd()
    {
      if (this._s == null)
        __Error.ReaderClosed();
      string str = this._pos != 0 ? this._s.Substring(this._pos, this._length - this._pos) : this._s;
      this._pos = this._length;
      return str;
    }

    /// <summary>
    ///   Считывает строку символов из текущей строки и возвращает данные в виде строки.
    /// </summary>
    /// <returns>
    ///   Следующая строка из текущей строки или <see langword="null" /> при достижении конца строки.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Текущее средство чтения закрывается.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти для выделения буфера для возвращаемой строки.
    /// </exception>
    [__DynamicallyInvokable]
    public override string ReadLine()
    {
      if (this._s == null)
        __Error.ReaderClosed();
      int pos;
      for (pos = this._pos; pos < this._length; ++pos)
      {
        char ch = this._s[pos];
        switch (ch)
        {
          case '\n':
          case '\r':
            string str = this._s.Substring(this._pos, pos - this._pos);
            this._pos = pos + 1;
            if (ch == '\r' && this._pos < this._length && this._s[this._pos] == '\n')
              ++this._pos;
            return str;
          default:
            continue;
        }
      }
      if (pos <= this._pos)
        return (string) null;
      string str1 = this._s.Substring(this._pos, pos - this._pos);
      this._pos = pos;
      return str1;
    }

    /// <summary>
    ///   Асинхронно выполняет чтение строки символов из текущей строки и возвращает данные в виде строки.
    /// </summary>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение <paramref name="TResult" /> содержит следующую строку из средства чтения строки, или параметр <see langword="null" /> если все символы считаны.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Количество символов в следующей строке больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Средство чтения строки был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override Task<string> ReadLineAsync()
    {
      return Task.FromResult<string>(this.ReadLine());
    }

    /// <summary>
    ///   Асинхронно считывает все символы с текущей позиции до конца строки и возвращает их в виде одной строки.
    /// </summary>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение <paramref name="TResult" /> содержит строку с символами от текущего положения до конца строки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Количество символов больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Средство чтения строки был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override Task<string> ReadToEndAsync()
    {
      return Task.FromResult<string>(this.ReadToEnd());
    }

    /// <summary>
    ///   Асинхронно считывает указанное максимальное число знаков текущей строки и записывает данные в буфер, начиная с указанного индекса.
    /// </summary>
    /// <param name="buffer">
    ///   При возвращении из этого метода содержит указанный массив символов, в котором значения в интервале от <paramref name="index" /> и (<paramref name="index" /> + <paramref name="count" /> - 1) заменены символами, считанными из текущего источника.
    /// </param>
    /// <param name="index">
    ///   Позиция в буфере <paramref name="buffer" />, с которого начинается запись.
    /// </param>
    /// <param name="count">
    ///   Максимальное число считываемых символов.
    ///    Если достигнут конца строки, прежде чем указанное количество символов, записанных в буфер, метод возвращает.
    /// </param>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение параметра <paramref name="TResult" /> содержит общее число байтов, считанных в буфер.
    ///    Значение результата может быть меньше запрошенного числа байтов, если число текущих доступных байтов меньше запрошенного числа, или она может быть 0 (нуль), если достигнут конец строки.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма <paramref name="index" /> и <paramref name="count" /> больше, чем длина буфера.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Средство чтения строки был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return Task.FromResult<int>(this.ReadBlock(buffer, index, count));
    }

    /// <summary>
    ///   Асинхронно считывает указанное максимальное число знаков текущей строки и записывает данные в буфер, начиная с указанного индекса.
    /// </summary>
    /// <param name="buffer">
    ///   При возвращении из этого метода содержит указанный массив символов, в котором значения в интервале от <paramref name="index" /> и (<paramref name="index" /> + <paramref name="count" /> - 1) заменены символами, считанными из текущего источника.
    /// </param>
    /// <param name="index">
    ///   Позиция в буфере <paramref name="buffer" />, с которого начинается запись.
    /// </param>
    /// <param name="count">
    ///   Максимальное число считываемых символов.
    ///    Если достигнут конца строки, прежде чем указанное количество символов, записанных в буфер, метод возвращает.
    /// </param>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение параметра <paramref name="TResult" /> содержит общее число байтов, считанных в буфер.
    ///    Значение результата может быть меньше запрошенного числа байтов, если число текущих доступных байтов меньше запрошенного числа, или она может быть 0 (нуль), если достигнут конец строки.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма <paramref name="index" /> и <paramref name="count" /> больше, чем длина буфера.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Средство чтения строки был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override Task<int> ReadAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return Task.FromResult<int>(this.Read(buffer, index, count));
    }
  }
}
