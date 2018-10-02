// Decompiled with JetBrains decompiler
// Type: System.IO.BinaryReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.IO
{
  /// <summary>
  ///   Считывает примитивные типы данных как двоичные значения в заданной кодировке.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public class BinaryReader : IDisposable
  {
    private const int MaxCharBytesSize = 128;
    private Stream m_stream;
    private byte[] m_buffer;
    private Decoder m_decoder;
    private byte[] m_charBytes;
    private char[] m_singleChar;
    private char[] m_charBuffer;
    private int m_maxCharsSize;
    private bool m_2BytesPerChar;
    private bool m_isMemoryStream;
    private bool m_leaveOpen;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.BinaryReader" /> на основании указанного потока с использованием кодировки UTF-8.
    /// </summary>
    /// <param name="input">Входной поток.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Поток не поддерживает чтение, является <see langword="null" />, или уже закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public BinaryReader(Stream input)
      : this(input, (Encoding) new UTF8Encoding(), false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.BinaryReader" /> на основе указанного потока и кодировки символов.
    /// </summary>
    /// <param name="input">Входной поток.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Поток не поддерживает чтение, является <see langword="null" />, или уже закрыт.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public BinaryReader(Stream input, Encoding encoding)
      : this(input, encoding, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.BinaryReader" /> на основе указанного потока и кодировки символов, а также при необходимости оставляет поток открытым.
    /// </summary>
    /// <param name="input">Входной поток.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <param name="leaveOpen">
    ///   Значение <see langword="true" />, чтобы оставить поток открытым после удаления объекта <see cref="T:System.IO.BinaryReader" />; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Поток не поддерживает чтение, является <see langword="null" />, или уже закрыт.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="encoding" /> или <paramref name="input" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public BinaryReader(Stream input, Encoding encoding, bool leaveOpen)
    {
      if (input == null)
        throw new ArgumentNullException(nameof (input));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      if (!input.CanRead)
        throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"));
      this.m_stream = input;
      this.m_decoder = encoding.GetDecoder();
      this.m_maxCharsSize = encoding.GetMaxCharCount(128);
      int length = encoding.GetMaxByteCount(1);
      if (length < 16)
        length = 16;
      this.m_buffer = new byte[length];
      this.m_2BytesPerChar = encoding is UnicodeEncoding;
      this.m_isMemoryStream = this.m_stream.GetType() == typeof (MemoryStream);
      this.m_leaveOpen = leaveOpen;
    }

    /// <summary>
    ///   Предоставляет доступ к базовому потоку объекта <see cref="T:System.IO.BinaryReader" />.
    /// </summary>
    /// <returns>
    ///   Базовый поток, связанный с объектом <see langword="BinaryReader" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Stream BaseStream
    {
      [__DynamicallyInvokable] get
      {
        return this.m_stream;
      }
    }

    /// <summary>
    ///   Закрывает текущий поток чтения и связанный с ним базовый поток.
    /// </summary>
    public virtual void Close()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые классом <see cref="T:System.IO.BinaryReader" /> (при необходимости освобождает и управляемые ресурсы).
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        Stream stream = this.m_stream;
        this.m_stream = (Stream) null;
        if (stream != null && !this.m_leaveOpen)
          stream.Close();
      }
      this.m_stream = (Stream) null;
      this.m_buffer = (byte[]) null;
      this.m_decoder = (Decoder) null;
      this.m_charBytes = (byte[]) null;
      this.m_singleChar = (char[]) null;
      this.m_charBuffer = (char[]) null;
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.IO.BinaryReader" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Возвращает следующий доступный для чтения символ, не перемещая позицию байта или символа вперед.
    /// </summary>
    /// <returns>
    ///   Следующий доступный символ или значение -1, если в потоке больше нет символов, или поток не поддерживает поиск.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Текущий символ не удается декодировать в буфер внутренних символов с помощью <see cref="T:System.Text.Encoding" /> выбранного для потока.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int PeekChar()
    {
      if (this.m_stream == null)
        __Error.FileNotOpen();
      if (!this.m_stream.CanSeek)
        return -1;
      long position = this.m_stream.Position;
      int num = this.Read();
      this.m_stream.Position = position;
      return num;
    }

    /// <summary>
    ///   Выполняет чтение знаков из базового потока и перемещает текущую позицию в потоке вперед в соответствии с используемым значением <see langword="Encoding" /> и конкретным знаком в потоке, чтение которого выполняется в настоящий момент.
    /// </summary>
    /// <returns>
    ///   Следующий символ из входного потока или значение -1, если в настоящее время доступных символов нет.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int Read()
    {
      if (this.m_stream == null)
        __Error.FileNotOpen();
      return this.InternalReadOneChar();
    }

    /// <summary>
    ///   Считывает значение <see langword="Boolean" /> из текущего потока и перемещает текущую позицию в потоке на один байт вперед.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если байт не равен нулю; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual bool ReadBoolean()
    {
      this.FillBuffer(1);
      return this.m_buffer[0] > (byte) 0;
    }

    /// <summary>
    ///   Считывает из текущего потока следующий байт и перемещает текущую позицию в потоке на один байт вперед.
    /// </summary>
    /// <returns>Следующий байт, считанный из текущего потока.</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual byte ReadByte()
    {
      if (this.m_stream == null)
        __Error.FileNotOpen();
      int num = this.m_stream.ReadByte();
      if (num == -1)
        __Error.EndOfFile();
      return (byte) num;
    }

    /// <summary>
    ///   Считывает из текущего потока байт со знаком и перемещает текущую позицию в потоке на один байт вперед.
    /// </summary>
    /// <returns>Байт со знаком, считанный из текущего потока.</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual sbyte ReadSByte()
    {
      this.FillBuffer(1);
      return (sbyte) this.m_buffer[0];
    }

    /// <summary>
    ///   Считывает следующий знак из текущего потока и изменяет текущую позицию в потоке в соответствии с используемым значением <see langword="Encoding" /> и конкретным знаком в потоке, чтение которого выполняется в настоящий момент.
    /// </summary>
    /// <returns>Символ, считанный из текущего потока.</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Прочитано символом-заместителем.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual char ReadChar()
    {
      int num = this.Read();
      if (num == -1)
        __Error.EndOfFile();
      return (char) num;
    }

    /// <summary>
    ///   Считывает целое число со знаком длиной 2 байта из текущего потока и перемещает текущую позицию в потоке на два байта вперед.
    /// </summary>
    /// <returns>
    ///   Целое число со знаком длиной 2 байта, считанное из текущего потока.
    /// </returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual short ReadInt16()
    {
      this.FillBuffer(2);
      return (short) ((int) this.m_buffer[0] | (int) this.m_buffer[1] << 8);
    }

    /// <summary>
    ///   Считывает целое число без знака длиной 2 байта в формате с прямым порядком байтов из текущего потока и перемещает текущую позицию в потоке на два байта вперед.
    /// </summary>
    /// <returns>
    ///   Целое число без знака длиной 2 байта, считанное из текущего потока.
    /// </returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual ushort ReadUInt16()
    {
      this.FillBuffer(2);
      return (ushort) ((uint) this.m_buffer[0] | (uint) this.m_buffer[1] << 8);
    }

    /// <summary>
    ///   Считывает целое число со знаком длиной 4 байта из текущего потока и перемещает текущую позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <returns>
    ///   Целое число со знаком длиной 2 байта, считанное из текущего потока.
    /// </returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int ReadInt32()
    {
      if (this.m_isMemoryStream)
      {
        if (this.m_stream == null)
          __Error.FileNotOpen();
        return (this.m_stream as MemoryStream).InternalReadInt32();
      }
      this.FillBuffer(4);
      return (int) this.m_buffer[0] | (int) this.m_buffer[1] << 8 | (int) this.m_buffer[2] << 16 | (int) this.m_buffer[3] << 24;
    }

    /// <summary>
    ///   Считывает целое число без знака длиной 4 байта из текущего потока и перемещает текущую позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <returns>
    ///   Целое число без знака длиной 4 байта, считанное из текущего потока.
    /// </returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual uint ReadUInt32()
    {
      this.FillBuffer(4);
      return (uint) ((int) this.m_buffer[0] | (int) this.m_buffer[1] << 8 | (int) this.m_buffer[2] << 16 | (int) this.m_buffer[3] << 24);
    }

    /// <summary>
    ///   Считывает целое число со знаком длиной 8 байта из текущего потока и перемещает текущую позицию в потоке на восемь байт вперед.
    /// </summary>
    /// <returns>
    ///   Целое число со знаком длиной 8 байт, считанное из текущего потока.
    /// </returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual long ReadInt64()
    {
      this.FillBuffer(8);
      return (long) (uint) ((int) this.m_buffer[4] | (int) this.m_buffer[5] << 8 | (int) this.m_buffer[6] << 16 | (int) this.m_buffer[7] << 24) << 32 | (long) (uint) ((int) this.m_buffer[0] | (int) this.m_buffer[1] << 8 | (int) this.m_buffer[2] << 16 | (int) this.m_buffer[3] << 24);
    }

    /// <summary>
    ///   Считывает целое число без знака длиной 8 байт из текущего потока и перемещает текущую позицию в потоке на восемь байтов вперед.
    /// </summary>
    /// <returns>
    ///   Целое число без знака длиной 8 байт, считанное из текущего потока.
    /// </returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual ulong ReadUInt64()
    {
      this.FillBuffer(8);
      return (ulong) (uint) ((int) this.m_buffer[4] | (int) this.m_buffer[5] << 8 | (int) this.m_buffer[6] << 16 | (int) this.m_buffer[7] << 24) << 32 | (ulong) (uint) ((int) this.m_buffer[0] | (int) this.m_buffer[1] << 8 | (int) this.m_buffer[2] << 16 | (int) this.m_buffer[3] << 24);
    }

    /// <summary>
    ///   Считывает число с плавающей запятой длиной 4 байта из текущего потока и перемещает текущую позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <returns>
    ///   Число с плавающей запятой длиной 4 байта, считанное из текущего потока.
    /// </returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual unsafe float ReadSingle()
    {
      this.FillBuffer(4);
      return *(float*) &(uint) ((int) this.m_buffer[0] | (int) this.m_buffer[1] << 8 | (int) this.m_buffer[2] << 16 | (int) this.m_buffer[3] << 24);
    }

    /// <summary>
    ///   Считывает число с плавающей запятой длиной 8 байт из текущего потока и перемещает текущую позицию в потоке на восемь байт вперед.
    /// </summary>
    /// <returns>
    ///   Число с плавающей запятой длиной 8 байт, считанное из текущего потока.
    /// </returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual unsafe double ReadDouble()
    {
      this.FillBuffer(8);
      return *(double*) &((ulong) (uint) ((int) this.m_buffer[4] | (int) this.m_buffer[5] << 8 | (int) this.m_buffer[6] << 16 | (int) this.m_buffer[7] << 24) << 32 | (ulong) (uint) ((int) this.m_buffer[0] | (int) this.m_buffer[1] << 8 | (int) this.m_buffer[2] << 16 | (int) this.m_buffer[3] << 24));
    }

    /// <summary>
    ///   Считывает десятичное значение из текущего потока и перемещает текущую позицию в потоке на шестнадцать байтов вперед.
    /// </summary>
    /// <returns>Десятичное значение, считанное из текущего потока.</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Decimal ReadDecimal()
    {
      this.FillBuffer(16);
      try
      {
        return Decimal.ToDecimal(this.m_buffer);
      }
      catch (ArgumentException ex)
      {
        throw new IOException(Environment.GetResourceString("Arg_DecBitCtor"), (Exception) ex);
      }
    }

    /// <summary>
    ///   Считывает строку из текущего потока.
    ///    Строка предваряется значением длины строки, которое закодировано как целое число блоками по семь битов.
    /// </summary>
    /// <returns>Считываемая строка.</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual string ReadString()
    {
      if (this.m_stream == null)
        __Error.FileNotOpen();
      int num = 0;
      int capacity = this.Read7BitEncodedInt();
      if (capacity < 0)
        throw new IOException(Environment.GetResourceString("IO.IO_InvalidStringLen_Len", (object) capacity));
      if (capacity == 0)
        return string.Empty;
      if (this.m_charBytes == null)
        this.m_charBytes = new byte[128];
      if (this.m_charBuffer == null)
        this.m_charBuffer = new char[this.m_maxCharsSize];
      StringBuilder sb = (StringBuilder) null;
      do
      {
        int byteCount = this.m_stream.Read(this.m_charBytes, 0, capacity - num > 128 ? 128 : capacity - num);
        if (byteCount == 0)
          __Error.EndOfFile();
        int chars = this.m_decoder.GetChars(this.m_charBytes, 0, byteCount, this.m_charBuffer, 0);
        if (num == 0 && byteCount == capacity)
          return new string(this.m_charBuffer, 0, chars);
        if (sb == null)
          sb = StringBuilderCache.Acquire(capacity);
        sb.Append(this.m_charBuffer, 0, chars);
        num += byteCount;
      }
      while (num < capacity);
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>
    ///   Считывает указанное количество символов из потока, начиная с заданной точки в массиве символов.
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, в который должны считываться данные.
    /// </param>
    /// <param name="index">
    ///   Стартовая точка в буфере, начиная с которой считываемые данные записываются в буфер.
    /// </param>
    /// <param name="count">Число символов для чтения.</param>
    /// <returns>
    ///   Общее количество символов, считанных в буфер.
    ///    Количество символов может быть меньше указанного числа, если в потоке осталось меньше символов, чем следует прочитать. Количество символов также может равняться нулю, если достигнут конец потока.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// 
    ///   -или-
    /// 
    ///   Число декодированных знаков для чтения больше, чем <paramref name="count" />.
    ///    Это может произойти, если декодер Юникода возвращает резервные символы или суррогатную пару.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual int Read(char[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.m_stream == null)
        __Error.FileNotOpen();
      return this.InternalReadChars(buffer, index, count);
    }

    [SecurityCritical]
    private unsafe int InternalReadChars(char[] buffer, int index, int count)
    {
      int charCount = count;
      if (this.m_charBytes == null)
        this.m_charBytes = new byte[128];
      while (charCount > 0)
      {
        int count1 = charCount;
        DecoderNLS decoder = this.m_decoder as DecoderNLS;
        if (decoder != null && decoder.HasState && count1 > 1)
          --count1;
        if (this.m_2BytesPerChar)
          count1 <<= 1;
        if (count1 > 128)
          count1 = 128;
        int num = 0;
        int byteCount;
        byte[] numArray;
        if (this.m_isMemoryStream)
        {
          MemoryStream stream = this.m_stream as MemoryStream;
          num = stream.InternalGetPosition();
          byteCount = stream.InternalEmulateRead(count1);
          numArray = stream.InternalGetBuffer();
        }
        else
        {
          byteCount = this.m_stream.Read(this.m_charBytes, 0, count1);
          numArray = this.m_charBytes;
        }
        if (byteCount == 0)
          return count - charCount;
        if (num < 0 || byteCount < 0 || checked (num + byteCount) > numArray.Length)
          throw new ArgumentOutOfRangeException("byteCount");
        if (index < 0 || charCount < 0 || checked (index + charCount) > buffer.Length)
          throw new ArgumentOutOfRangeException("charsRemaining");
        int chars;
        fixed (byte* numPtr = numArray)
          fixed (char* chPtr = buffer)
            chars = this.m_decoder.GetChars((byte*) checked (unchecked ((IntPtr) numPtr) + num), byteCount, (char*) checked (unchecked ((UIntPtr) chPtr) + unchecked ((UIntPtr) checked (unchecked ((IntPtr) index) * 2))), charCount, false);
        charCount -= chars;
        index += chars;
      }
      return count - charCount;
    }

    private int InternalReadOneChar()
    {
      int num1 = 0;
      long num2;
      long num3 = num2 = 0L;
      if (this.m_stream.CanSeek)
        num3 = this.m_stream.Position;
      if (this.m_charBytes == null)
        this.m_charBytes = new byte[128];
      if (this.m_singleChar == null)
        this.m_singleChar = new char[1];
      while (num1 == 0)
      {
        int byteCount = this.m_2BytesPerChar ? 2 : 1;
        int num4 = this.m_stream.ReadByte();
        this.m_charBytes[0] = (byte) num4;
        if (num4 == -1)
          byteCount = 0;
        if (byteCount == 2)
        {
          int num5 = this.m_stream.ReadByte();
          this.m_charBytes[1] = (byte) num5;
          if (num5 == -1)
            byteCount = 1;
        }
        if (byteCount == 0)
          return -1;
        try
        {
          num1 = this.m_decoder.GetChars(this.m_charBytes, 0, byteCount, this.m_singleChar, 0);
        }
        catch
        {
          if (this.m_stream.CanSeek)
            this.m_stream.Seek(num3 - this.m_stream.Position, SeekOrigin.Current);
          throw;
        }
      }
      if (num1 == 0)
        return -1;
      return (int) this.m_singleChar[0];
    }

    /// <summary>
    ///   Считывает указанное количество символов из текущего потока, возвращает данные в массив символов и перемещает текущую позицию в соответствии с используемой <see langword="Encoding" /> и определенным символом, считываемым из потока.
    /// </summary>
    /// <param name="count">Число символов для чтения.</param>
    /// <returns>
    ///   Массив символов, в котором содержатся данные, считанные из базового потока.
    ///    Если при чтении был достигнут конец потока, это число может быть меньше, чем количество запрошенных символов.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Число декодированных знаков для чтения больше, чем <paramref name="count" />.
    ///    Это может произойти, если декодер Юникода возвращает резервные символы или суррогатную пару.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="count" /> является отрицательным значением.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual char[] ReadChars(int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this.m_stream == null)
        __Error.FileNotOpen();
      if (count == 0)
        return EmptyArray<char>.Value;
      char[] buffer = new char[count];
      int length = this.InternalReadChars(buffer, 0, count);
      if (length != count)
      {
        char[] chArray = new char[length];
        Buffer.InternalBlockCopy((Array) buffer, 0, (Array) chArray, 0, 2 * length);
        buffer = chArray;
      }
      return buffer;
    }

    /// <summary>
    ///   Считывает указанное количество байтов из потока, начиная с заданной точки в массиве байтов.
    /// </summary>
    /// <param name="buffer">
    ///   Буфер, в который должны считываться данные.
    /// </param>
    /// <param name="index">
    ///   Стартовая точка в буфере, начиная с которой считываемые данные записываются в буфер.
    /// </param>
    /// <param name="count">
    ///   Количество байтов, чтение которых необходимо выполнить.
    /// </param>
    /// <returns>
    ///   Количество байтов, считанных в <paramref name="buffer" />.
    ///    Количество символов может быть меньше указанного числа, если в потоке осталось меньше байтов, чем следует считать. Количество символов также может равняться нулю, если достигнут конец потока.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// 
    ///   -или-
    /// 
    ///   Число декодированных знаков для чтения больше, чем <paramref name="count" />.
    ///    Это может произойти, если декодер Юникода возвращает резервные символы или суррогатную пару.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int Read(byte[] buffer, int index, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer), Environment.GetResourceString("ArgumentNull_Buffer"));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - index < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.m_stream == null)
        __Error.FileNotOpen();
      return this.m_stream.Read(buffer, index, count);
    }

    /// <summary>
    ///   Считывает указанное количество байтов из текущего потока в массив байтов и перемещает текущую позицию на это количество байтов.
    /// </summary>
    /// <param name="count">
    ///   Количество байтов, чтение которых необходимо выполнить.
    ///    Это значение должно быть равно 0 или быть больше 0, иначе возникнет исключение.
    /// </param>
    /// <returns>
    ///   Массив байтов, в котором содержатся данные, считанные из базового потока.
    ///    Если при чтении был достигнут конец потока, это число может быть меньше, чем количество запрошенных байтов.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Число декодированных знаков для чтения больше, чем <paramref name="count" />.
    ///    Это может произойти, если декодер Юникода возвращает резервные символы или суррогатную пару.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="count" /> является отрицательным значением.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual byte[] ReadBytes(int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this.m_stream == null)
        __Error.FileNotOpen();
      if (count == 0)
        return EmptyArray<byte>.Value;
      byte[] buffer = new byte[count];
      int length = 0;
      do
      {
        int num = this.m_stream.Read(buffer, length, count);
        if (num != 0)
        {
          length += num;
          count -= num;
        }
        else
          break;
      }
      while (count > 0);
      if (length != buffer.Length)
      {
        byte[] numArray = new byte[length];
        Buffer.InternalBlockCopy((Array) buffer, 0, (Array) numArray, 0, length);
        buffer = numArray;
      }
      return buffer;
    }

    /// <summary>
    ///   Заполняет внутренний буфер указанным количеством байтов, которые были cчитаны из потока.
    /// </summary>
    /// <param name="numBytes">
    ///   Количество байтов, чтение которых необходимо выполнить.
    /// </param>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока до <paramref name="numBytes" /> может быть прочитан.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Запрошенный <paramref name="numBytes" /> больше, чем размер внутреннего буфера.
    /// </exception>
    [__DynamicallyInvokable]
    protected virtual void FillBuffer(int numBytes)
    {
      if (this.m_buffer != null && (numBytes < 0 || numBytes > this.m_buffer.Length))
        throw new ArgumentOutOfRangeException(nameof (numBytes), Environment.GetResourceString("ArgumentOutOfRange_BinaryReaderFillBuffer"));
      int offset = 0;
      if (this.m_stream == null)
        __Error.FileNotOpen();
      if (numBytes == 1)
      {
        int num = this.m_stream.ReadByte();
        if (num == -1)
          __Error.EndOfFile();
        this.m_buffer[0] = (byte) num;
      }
      else
      {
        do
        {
          int num = this.m_stream.Read(this.m_buffer, offset, numBytes - offset);
          if (num == 0)
            __Error.EndOfFile();
          offset += num;
        }
        while (offset < numBytes);
      }
    }

    /// <summary>
    ///   Считывает 32-разрядное целое число в сжатом формате.
    /// </summary>
    /// <returns>32-разрядное целое число в сжатом формате.</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Поток поврежден.
    /// </exception>
    [__DynamicallyInvokable]
    protected internal int Read7BitEncodedInt()
    {
      int num1 = 0;
      int num2 = 0;
      while (num2 != 35)
      {
        byte num3 = this.ReadByte();
        num1 |= ((int) num3 & (int) sbyte.MaxValue) << num2;
        num2 += 7;
        if (((int) num3 & 128) == 0)
          return num1;
      }
      throw new FormatException(Environment.GetResourceString("Format_Bad7BitInt32"));
    }
  }
}
