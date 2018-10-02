// Decompiled with JetBrains decompiler
// Type: System.IO.TextReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>
  ///   Представляет средство чтения, позволяющее считывать последовательные наборы символов.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class TextReader : MarshalByRefObject, IDisposable
  {
    [NonSerialized]
    private static Func<object, string> _ReadLineDelegate = (Func<object, string>) (state => ((TextReader) state).ReadLine());
    [NonSerialized]
    private static Func<object, int> _ReadDelegate = (Func<object, int>) (state =>
    {
      Tuple<TextReader, char[], int, int> tuple = (Tuple<TextReader, char[], int, int>) state;
      return tuple.Item1.Read(tuple.Item2, tuple.Item3, tuple.Item4);
    });
    /// <summary>
    ///   Предоставляет <see langword="TextReader" /> без данных, доступных для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly TextReader Null = (TextReader) new TextReader.NullTextReader();

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.TextReader" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected TextReader()
    {
    }

    /// <summary>
    ///   Закрывает <see cref="T:System.IO.TextReader" /> и освобождает все системные ресурсы, связанные с <see langword="TextReader" />.
    /// </summary>
    public virtual void Close()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые объектом <see cref="T:System.IO.TextReader" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.IO.TextReader" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>
    ///   Выполняет чтение следующего символа, не изменяя состояние средства чтения или источника символа.
    ///    Возвращает следующий доступный символ, фактически не считывая его из средства чтения.
    /// </summary>
    /// <returns>
    ///   Целое число, представляющее следующий символ, чтение которого необходимо выполнить, или значение -1, если доступных символов больше нет или средство чтения не поддерживает поиск.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextReader" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int Peek()
    {
      return -1;
    }

    /// <summary>
    ///   Выполняет чтение следующего символа из средства чтения текста и перемещает положение символа на одну позицию вперед.
    /// </summary>
    /// <returns>
    ///   Следующий символ из средства чтения текста или значение -1, если доступных символов больше нет.
    ///    Реализация по умолчанию возвращает значение -1.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextReader" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int Read()
    {
      return -1;
    }

    /// <summary>
    ///   Считывает указанное максимальное количество символов из текущего средства чтения и записывает данные в буфер, начиная с заданного индекса.
    /// </summary>
    /// <param name="buffer">
    ///   При возвращении из этого метода содержит указанный массив символов, в котором значения в интервале от <paramref name="index" /> и (<paramref name="index" /> + <paramref name="count" /> - 1) заменены символами, считанными из текущего источника.
    /// </param>
    /// <param name="index">
    ///   Позиция в буфере <paramref name="buffer" />, с которого начинается запись.
    /// </param>
    /// <param name="count">
    ///   Максимальное число считываемых символов.
    ///    Если конец средства чтения достигнут, прежде чем в буфер считано указанное количество символов, метод возвращает управление.
    /// </param>
    /// <returns>
    ///   Количество считанных символов.
    ///    Количество будет меньше или равно <paramref name="count" /> в зависимости от доступности данных в средстве чтения.
    ///    Этот метод возвращает 0 (нуль), если его вызвать при отсутствии символов, доступных для чтения.
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
    ///   <see cref="T:System.IO.TextReader" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int Read([In, Out] char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      int num1 = 0;
      do
      {
        int num2 = this.Read();
        if (num2 != -1)
          buffer[index + num1++] = (char) num2;
        else
          break;
      }
      while (num1 < count);
      return num1;
    }

    /// <summary>
    ///   Считывает все символы, начиная с текущей позиции до конца средства чтения текста, и возвращает их в виде одной строки.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая все символы, начиная с текущей позиции до конца средства чтения текста.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextReader" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти для выделения буфера для возвращаемой строки.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Число символов в строке больше, чем <see cref="F:System.Int32.MaxValue" />
    /// </exception>
    [__DynamicallyInvokable]
    public virtual string ReadToEnd()
    {
      char[] buffer = new char[4096];
      StringBuilder stringBuilder = new StringBuilder(4096);
      int charCount;
      while ((charCount = this.Read(buffer, 0, buffer.Length)) != 0)
        stringBuilder.Append(buffer, 0, charCount);
      return stringBuilder.ToString();
    }

    /// <summary>
    ///   Считывает указанное максимальное количество символов из текущего средства чтения текста и записывает данные в буфер, начиная с заданного индекса.
    /// </summary>
    /// <param name="buffer">
    ///   При возвращении из этого метода параметр содержит указанный массив символов, в котором значения в интервале от <paramref name="index" /> до (<paramref name="index" /> + <paramref name="count" /> - 1) заменены символами, считанными из текущего источника.
    /// </param>
    /// <param name="index">
    ///   Позиция в буфере <paramref name="buffer" />, с которого начинается запись.
    /// </param>
    /// <param name="count">
    ///   Максимальное число считываемых символов.
    /// </param>
    /// <returns>
    ///   Количество считанных символов.
    ///    Число будет меньше или равно значению <paramref name="count" />, в зависимости от того, считаны ли все входящие символы.
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
    ///   <see cref="T:System.IO.TextReader" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int ReadBlock([In, Out] char[] buffer, int index, int count)
    {
      int num1 = 0;
      int num2;
      do
      {
        num1 += num2 = this.Read(buffer, index + num1, count - num1);
      }
      while (num2 > 0 && num1 < count);
      return num1;
    }

    /// <summary>
    ///   Выполняет чтение строки символов из средства чтения текста и возвращает данные в виде строки.
    /// </summary>
    /// <returns>
    ///   Следующая строка из средства чтения или значение <see langword="null" />, если все символы считаны.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти для выделения буфера для возвращаемой строки.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   <see cref="T:System.IO.TextReader" /> закрыт.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Число символов в строке больше, чем <see cref="F:System.Int32.MaxValue" />
    /// </exception>
    [__DynamicallyInvokable]
    public virtual string ReadLine()
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num;
      while (true)
      {
        num = this.Read();
        switch (num)
        {
          case -1:
            goto label_6;
          case 10:
          case 13:
            goto label_2;
          default:
            stringBuilder.Append((char) num);
            continue;
        }
      }
label_2:
      if (num == 13 && this.Peek() == 10)
        this.Read();
      return stringBuilder.ToString();
label_6:
      if (stringBuilder.Length > 0)
        return stringBuilder.ToString();
      return (string) null;
    }

    /// <summary>
    ///   Асинхронно считывает строку символов и возвращает данные в виде строки.
    /// </summary>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение параметра <paramref name="TResult" /> содержит следующую строку из средства чтения текста или значение <see langword="null" />, если все символы считаны.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Количество символов в следующей строке больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Средства чтения текста был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task<string> ReadLineAsync()
    {
      return Task<string>.Factory.StartNew(TextReader._ReadLineDelegate, (object) this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>
    ///   Асинхронно считывает все символы с текущей позиции до конца средства чтения текста и возвращает их в виде одной строки.
    /// </summary>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение параметра <paramref name="TResult" /> содержит строку с символами от текущего положения до конца средства чтения текста.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Количество символов больше <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Средства чтения текста был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual async Task<string> ReadToEndAsync()
    {
      char[] chars = new char[4096];
      StringBuilder sb = new StringBuilder(4096);
      while (true)
      {
        int num = await this.ReadAsyncInternal(chars, 0, chars.Length).ConfigureAwait(false);
        int len;
        if ((len = num) != 0)
          sb.Append(chars, 0, len);
        else
          break;
      }
      return sb.ToString();
    }

    /// <summary>
    ///   Асинхронно считывает указанное максимальное количество символов из текущего средства чтения текста и записывает данные в буфер, начиная с указанного индекса.
    /// </summary>
    /// <param name="buffer">
    ///   При возвращении из этого метода содержит указанный массив символов, в котором значения в интервале от <paramref name="index" /> и (<paramref name="index" /> + <paramref name="count" /> - 1) заменены символами, считанными из текущего источника.
    /// </param>
    /// <param name="index">
    ///   Позиция в буфере <paramref name="buffer" />, с которого начинается запись.
    /// </param>
    /// <param name="count">
    ///   Максимальное число считываемых символов.
    ///    Если конец текста достигнут, прежде чем указанное количество символов считывается в буфер, текущий метод возвращает управление.
    /// </param>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение параметра <paramref name="TResult" /> содержит общее число байтов, считанных в буфер.
    ///    Значение результата может быть меньше запрошенного числа байтов, если число текущих доступных байтов меньше запрошенного числа, или результат может быть равен 0 (нулю), если был достигнут конец текста.
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
    ///   Средства чтения текста был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task<int> ReadAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return this.ReadAsyncInternal(buffer, index, count);
    }

    internal virtual Task<int> ReadAsyncInternal(char[] buffer, int index, int count)
    {
      Tuple<TextReader, char[], int, int> tuple = new Tuple<TextReader, char[], int, int>(this, buffer, index, count);
      return Task<int>.Factory.StartNew(TextReader._ReadDelegate, (object) tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    /// <summary>
    ///   Асинхронно считывает указанное максимальное количество символов из текущего средства чтения текста и записывает данные в буфер, начиная с указанного индекса.
    /// </summary>
    /// <param name="buffer">
    ///   При возвращении из этого метода содержит указанный массив символов, в котором значения в интервале от <paramref name="index" /> и (<paramref name="index" /> + <paramref name="count" /> - 1) заменены символами, считанными из текущего источника.
    /// </param>
    /// <param name="index">
    ///   Позиция в буфере <paramref name="buffer" />, с которого начинается запись.
    /// </param>
    /// <param name="count">
    ///   Максимальное число считываемых символов.
    ///    Если конец текста достигнут, прежде чем указанное количество символов считывается в буфер, текущий метод возвращает управление.
    /// </param>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию чтения.
    ///    Значение параметра <paramref name="TResult" /> содержит общее число байтов, считанных в буфер.
    ///    Значение результата может быть меньше запрошенного числа байтов, если число текущих доступных байтов меньше запрошенного числа, или результат может быть равен 0 (нулю), если был достигнут конец текста.
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
    ///   Средства чтения текста был удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения в настоящее время используется предыдущей операцией чтения.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public virtual Task<int> ReadBlockAsync(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0 || count < 0)
        throw new ArgumentOutOfRangeException(index < 0 ? nameof (index) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      return this.ReadBlockAsyncInternal(buffer, index, count);
    }

    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    private async Task<int> ReadBlockAsyncInternal(char[] buffer, int index, int count)
    {
      int n = 0;
      int num;
      do
      {
        num = await this.ReadAsyncInternal(buffer, index + n, count - n).ConfigureAwait(false);
        n += num;
      }
      while (num > 0 && n < count);
      return n;
    }

    /// <summary>
    ///   Создает потокобезопасную оболочку для указанного объекта <see langword="TextReader" />.
    /// </summary>
    /// <param name="reader">
    ///   Коллекция <see langword="TextReader" />, которую требуется синхронизировать.
    /// </param>
    /// <returns>
    ///   Потокобезопасный объект <see cref="T:System.IO.TextReader" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="reader" /> имеет значение <see langword="null" />.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static TextReader Synchronized(TextReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof (reader));
      if (reader is TextReader.SyncTextReader)
        return reader;
      return (TextReader) new TextReader.SyncTextReader(reader);
    }

    [Serializable]
    private sealed class NullTextReader : TextReader
    {
      public override int Read(char[] buffer, int index, int count)
      {
        return 0;
      }

      public override string ReadLine()
      {
        return (string) null;
      }
    }

    [Serializable]
    internal sealed class SyncTextReader : TextReader
    {
      internal TextReader _in;

      internal SyncTextReader(TextReader t)
      {
        this._in = t;
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void Close()
      {
        this._in.Close();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      protected override void Dispose(bool disposing)
      {
        if (!disposing)
          return;
        this._in.Dispose();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override int Peek()
      {
        return this._in.Peek();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override int Read()
      {
        return this._in.Read();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override int Read([In, Out] char[] buffer, int index, int count)
      {
        return this._in.Read(buffer, index, count);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override int ReadBlock([In, Out] char[] buffer, int index, int count)
      {
        return this._in.ReadBlock(buffer, index, count);
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override string ReadLine()
      {
        return this._in.ReadLine();
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override string ReadToEnd()
      {
        return this._in.ReadToEnd();
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task<string> ReadLineAsync()
      {
        return Task.FromResult<string>(this.ReadLine());
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override Task<string> ReadToEndAsync()
      {
        return Task.FromResult<string>(this.ReadToEnd());
      }

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
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

      [ComVisible(false)]
      [MethodImpl(MethodImplOptions.Synchronized)]
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
}
