// Decompiled with JetBrains decompiler
// Type: System.IO.StringWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
  /// <summary>
  ///   Реализует объект <see cref="T:System.IO.TextWriter" /> для записи сведений в строку.
  ///    Сведения хранятся в базовом <see cref="T:System.Text.StringBuilder" />.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class StringWriter : TextWriter
  {
    private static volatile UnicodeEncoding m_encoding;
    private StringBuilder _sb;
    private bool _isOpen;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StringWriter" />.
    /// </summary>
    [__DynamicallyInvokable]
    public StringWriter()
      : this(new StringBuilder(), (IFormatProvider) CultureInfo.CurrentCulture)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StringWriter" /> указанным элементом управления формата.
    /// </summary>
    /// <param name="formatProvider">
    ///   Объект <see cref="T:System.IFormatProvider" />, управляющий форматированием.
    /// </param>
    [__DynamicallyInvokable]
    public StringWriter(IFormatProvider formatProvider)
      : this(new StringBuilder(), formatProvider)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StringWriter" />, который производит запись в указанный объект <see cref="T:System.Text.StringBuilder" />.
    /// </summary>
    /// <param name="sb">
    ///   Объект <see cref="T:System.Text.StringBuilder" />, куда выполняется запись.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="sb" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringWriter(StringBuilder sb)
      : this(sb, (IFormatProvider) CultureInfo.CurrentCulture)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.StringWriter" />, который выполняет запись в указанный объект <see cref="T:System.Text.StringBuilder" /> и имеет указанный поставщик формата.
    /// </summary>
    /// <param name="sb">
    ///   Объект <see cref="T:System.Text.StringBuilder" />, запись в который выполняется.
    /// </param>
    /// <param name="formatProvider">
    ///   Объект <see cref="T:System.IFormatProvider" />, управляющий форматированием.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="sb" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public StringWriter(StringBuilder sb, IFormatProvider formatProvider)
      : base(formatProvider)
    {
      if (sb == null)
        throw new ArgumentNullException(nameof (sb), Environment.GetResourceString("ArgumentNull_Buffer"));
      this._sb = sb;
      this._isOpen = true;
    }

    /// <summary>
    ///   Закрывает текущий <see cref="T:System.IO.StringWriter" /> и соответствующий поток.
    /// </summary>
    public override void Close()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.IO.StringWriter" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected override void Dispose(bool disposing)
    {
      this._isOpen = false;
      base.Dispose(disposing);
    }

    /// <summary>
    ///   Получает кодировку <see cref="T:System.Text.Encoding" />, в которой осуществляется запись выходных данных.
    /// </summary>
    /// <returns>
    ///   Кодировка <see langword="Encoding" />, в которой осуществляется запись выходных данных.
    /// </returns>
    [__DynamicallyInvokable]
    public override Encoding Encoding
    {
      [__DynamicallyInvokable] get
      {
        if (StringWriter.m_encoding == null)
          StringWriter.m_encoding = new UnicodeEncoding(false, false);
        return (Encoding) StringWriter.m_encoding;
      }
    }

    /// <summary>
    ///   Возвращает базовый объект <see cref="T:System.Text.StringBuilder" />.
    /// </summary>
    /// <returns>
    ///   Базовый объект <see langword="StringBuilder" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual StringBuilder GetStringBuilder()
    {
      return this._sb;
    }

    /// <summary>Записывает символ в строку.</summary>
    /// <param name="value">Записываемый символ.</param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public override void Write(char value)
    {
      if (!this._isOpen)
        __Error.WriterClosed();
      this._sb.Append(value);
    }

    /// <summary>Записывает в поток дочерний массив символов.</summary>
    /// <param name="buffer">
    ///   Массив символов, из которого записываются данные.
    /// </param>
    /// <param name="index">
    ///   Позиция в буфере, с которой начинается чтение данных.
    /// </param>
    /// <param name="count">
    ///   Наибольшее количество символов для записи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   (<paramref name="index" /> + <paramref name="count" />)&gt; <paramref name="buffer" />.
    ///   <see langword="Length" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public override void Write(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (!this._isOpen)
        __Error.WriterClosed();
      this._sb.Append(buffer, index, count);
    }

    /// <summary>Записывает строку в текущую строку.</summary>
    /// <param name="value">Строка для записи.</param>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public override void Write(string value)
    {
      if (!this._isOpen)
        __Error.WriterClosed();
      if (value == null)
        return;
      this._sb.Append(value);
    }

    /// <summary>Асинхронно записывает символ в строку.</summary>
    /// <param name="value">Символ, записываемый в строку.</param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи строк удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи строк сейчас используется предыдущей операцией записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(char value)
    {
      this.Write(value);
      return Task.CompletedTask;
    }

    /// <summary>Асинхронно записывает строку в текущую строку.</summary>
    /// <param name="value">
    ///   Строка для записи.
    ///    Если параметр <paramref name="value" /> имеет значение <see langword="null" />, в текстовый поток ничего не записывается.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи строк удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи строк сейчас используется предыдущей операцией записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(string value)
    {
      this.Write(value);
      return Task.CompletedTask;
    }

    /// <summary>Асинхронно записывает подмассив символов в строку.</summary>
    /// <param name="buffer">
    ///   Массив символов, из которого записываются данные.
    /// </param>
    /// <param name="index">
    ///   Позиция в буфере, с которой начинается чтение данных.
    /// </param>
    /// <param name="count">
    ///   Наибольшее количество символов для записи.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма значений <paramref name="index" /> и <paramref name="count" /> превышает длину буфера.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи строк удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи строк сейчас используется предыдущей операцией записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteAsync(char[] buffer, int index, int count)
    {
      this.Write(buffer, index, count);
      return Task.CompletedTask;
    }

    /// <summary>
    ///   Асинхронно записывает в строку символ, за которым следует знак завершения строки.
    /// </summary>
    /// <param name="value">Символ, записываемый в строку.</param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи строк удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи строк сейчас используется предыдущей операцией записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync(char value)
    {
      this.WriteLine(value);
      return Task.CompletedTask;
    }

    /// <summary>
    ///   Асинхронно записывает в текущую строку строку, за которой следует символ завершения строки.
    /// </summary>
    /// <param name="value">
    ///   Строка для записи.
    ///    Если значение равно <see langword="null" />, записывается только знак конца строки.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи строк удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи строк сейчас используется предыдущей операцией записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync(string value)
    {
      this.WriteLine(value);
      return Task.CompletedTask;
    }

    /// <summary>
    ///   Асинхронно записывает в строку подмассив символов, за которым следует символ конца строки.
    /// </summary>
    /// <param name="buffer">
    ///   Массив символов, из которого записываются данные.
    /// </param>
    /// <param name="index">
    ///   Позиция в буфере, с которой начинается чтение данных.
    /// </param>
    /// <param name="count">
    ///   Наибольшее количество символов для записи.
    /// </param>
    /// <returns>Задача, представляющая асинхронную операцию записи.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Сумма значений <paramref name="index" /> и <paramref name="count" /> превышает длину буфера.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Модуль записи строк удален.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль записи строк сейчас используется предыдущей операцией записи.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task WriteLineAsync(char[] buffer, int index, int count)
    {
      this.WriteLine(buffer, index, count);
      return Task.CompletedTask;
    }

    /// <summary>
    ///   Асинхронно очищает все буферы текущего средства записи и вызывает запись всех буферизованных данных в базовое устройство.
    /// </summary>
    /// <returns>
    ///   Задача, представляющая асинхронную операцию очистки.
    /// </returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    public override Task FlushAsync()
    {
      return Task.CompletedTask;
    }

    /// <summary>
    ///   Возвращает строку, содержащую символы, записанные в текущий <see langword="StringWriter" /> к текущему моменту.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая символы, записанные в текущий <see langword="StringWriter" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this._sb.ToString();
    }
  }
}
