// Decompiled with JetBrains decompiler
// Type: System.IO.BinaryWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.IO
{
  /// <summary>
  ///   Записывает примитивные типы в двоичный поток и поддерживает запись строк в заданной кодировке.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class BinaryWriter : IDisposable
  {
    /// <summary>
    ///   Указывает <see cref="T:System.IO.BinaryWriter" /> без резервного хранилища.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly BinaryWriter Null = new BinaryWriter();
    /// <summary>Содержит базовый поток.</summary>
    [__DynamicallyInvokable]
    protected Stream OutStream;
    private byte[] _buffer;
    private Encoding _encoding;
    private Encoder _encoder;
    [OptionalField]
    private bool _leaveOpen;
    [OptionalField]
    private char[] _tmpOneCharBuffer;
    private byte[] _largeByteBuffer;
    private int _maxChars;
    private const int LargeByteBufferSize = 256;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.BinaryWriter" /> класс, который записывает в поток.
    /// </summary>
    [__DynamicallyInvokable]
    protected BinaryWriter()
    {
      this.OutStream = Stream.Null;
      this._buffer = new byte[16];
      this._encoding = (Encoding) new UTF8Encoding(false, true);
      this._encoder = this._encoding.GetEncoder();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.BinaryWriter" /> на основании указанного потока с использованием кодировки UTF-8.
    /// </summary>
    /// <param name="output">Выходной поток.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Поток не поддерживает запись или уже закрыт.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="output" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public BinaryWriter(Stream output)
      : this(output, (Encoding) new UTF8Encoding(false, true), false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.BinaryWriter" /> на основе указанного потока и кодировки символов.
    /// </summary>
    /// <param name="output">Выходной поток.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Поток не поддерживает запись или уже закрыт.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="output" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public BinaryWriter(Stream output, Encoding encoding)
      : this(output, encoding, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.BinaryWriter" /> на основе указанного потока и кодировки символов, а также при необходимости оставляет поток открытым.
    /// </summary>
    /// <param name="output">Выходной поток.</param>
    /// <param name="encoding">
    ///   Кодировка символов, которую нужно использовать.
    /// </param>
    /// <param name="leaveOpen">
    ///   Значение <see langword="true" />, чтобы оставить поток открытым после удаления объекта <see cref="T:System.IO.BinaryWriter" />; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Поток не поддерживает запись или уже закрыт.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="output" /> или <paramref name="encoding" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public BinaryWriter(Stream output, Encoding encoding, bool leaveOpen)
    {
      if (output == null)
        throw new ArgumentNullException(nameof (output));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      if (!output.CanWrite)
        throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotWritable"));
      this.OutStream = output;
      this._buffer = new byte[16];
      this._encoding = encoding;
      this._encoder = this._encoding.GetEncoder();
      this._leaveOpen = leaveOpen;
    }

    /// <summary>
    ///   Закрывает текущий <see cref="T:System.IO.BinaryWriter" /> и основной поток.
    /// </summary>
    public virtual void Close()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.IO.BinaryWriter" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this._leaveOpen)
        this.OutStream.Flush();
      else
        this.OutStream.Close();
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.IO.BinaryWriter" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Возвращает основной поток <see cref="T:System.IO.BinaryWriter" />.
    /// </summary>
    /// <returns>
    ///   Базовый поток, связанный с объектом <see langword="BinaryWriter" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Stream BaseStream
    {
      [__DynamicallyInvokable] get
      {
        this.Flush();
        return this.OutStream;
      }
    }

    /// <summary>
    ///   Очищает все буферы текущего модуля записи и вызывает немедленную запись всех буферизованных данных на базовое устройство.
    /// </summary>
    [__DynamicallyInvokable]
    public virtual void Flush()
    {
      this.OutStream.Flush();
    }

    /// <summary>Задает позицию в текущем потоке.</summary>
    /// <param name="offset">
    ///   Смещение байтов относительно <paramref name="origin" />.
    /// </param>
    /// <param name="origin">
    ///   Поле <see cref="T:System.IO.SeekOrigin" /> задает опорную точку, из которого будет получить новую позицию.
    /// </param>
    /// <returns>Позиция, с текущим потоком.</returns>
    /// <exception cref="T:System.IO.IOException">
    ///   Указатель на файл был перемещен в недопустимом месте.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="T:System.IO.SeekOrigin" /> Недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual long Seek(int offset, SeekOrigin origin)
    {
      return this.OutStream.Seek((long) offset, origin);
    }

    /// <summary>
    ///   Записывает однобайтовое <see langword="Boolean" /> значение в текущий поток, представляющий 0 <see langword="false" /> и 1, представляющие <see langword="true" />.
    /// </summary>
    /// <param name="value">
    ///   <see langword="Boolean" /> Значение для записи (0 или 1).
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(bool value)
    {
      this._buffer[0] = value ? (byte) 1 : (byte) 0;
      this.OutStream.Write(this._buffer, 0, 1);
    }

    /// <summary>
    ///   Записывает байт без знака в текущий поток и перемещает позицию в потоке на один байт.
    /// </summary>
    /// <param name="value">Байт без знака для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(byte value)
    {
      this.OutStream.WriteByte(value);
    }

    /// <summary>
    ///   Записывает байт со знаком в текущий поток и перемещает позицию в потоке на один байт.
    /// </summary>
    /// <param name="value">Записываемый байт со знаком.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void Write(sbyte value)
    {
      this.OutStream.WriteByte((byte) value);
    }

    /// <summary>Записывает массив байтов в базовый поток.</summary>
    /// <param name="buffer">
    ///   Массив байтов, содержащий данные для записи.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      this.OutStream.Write(buffer, 0, buffer.Length);
    }

    /// <summary>Записывает области массива байтов в текущий поток.</summary>
    /// <param name="buffer">
    ///   Массив байтов, содержащий данные для записи.
    /// </param>
    /// <param name="index">
    ///   Стартовая точка в <paramref name="buffer" /> с которого начинается запись.
    /// </param>
    /// <param name="count">Количество записываемых байтов.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(byte[] buffer, int index, int count)
    {
      this.OutStream.Write(buffer, index, count);
    }

    /// <summary>
    ///   Записывает знак Юникода в текущий поток и перемещает текущую позицию в потоке в соответствии с <see langword="Encoding" /> используются и записанными в поток знаками.
    /// </summary>
    /// <param name="ch">
    ///   Символ Юникода не символ-заместитель, для записи.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="ch" /> Представляет единичный символ-заместитель.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual unsafe void Write(char ch)
    {
      if (char.IsSurrogate(ch))
        throw new ArgumentException(Environment.GetResourceString("Arg_SurrogatesNotAllowedAsSingleChar"));
      int bytes1;
      fixed (byte* bytes2 = this._buffer)
        bytes1 = this._encoder.GetBytes(&ch, 1, bytes2, this._buffer.Length, true);
      this.OutStream.Write(this._buffer, 0, bytes1);
    }

    /// <summary>
    ///   Записывает массив символов в текущий поток и перемещает текущую позицию в потоке в соответствии с <see langword="Encoding" /> используется и записанными в поток знаками.
    /// </summary>
    /// <param name="chars">
    ///   Массив символов, содержащий записываемые в поток данные.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="chars" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(char[] chars)
    {
      if (chars == null)
        throw new ArgumentNullException(nameof (chars));
      byte[] bytes = this._encoding.GetBytes(chars, 0, chars.Length);
      this.OutStream.Write(bytes, 0, bytes.Length);
    }

    /// <summary>
    ///   Выполняет запись части массива символов в текущий поток и перемещает текущую позицию в потоке в соответствии с <see langword="Encoding" /> используются и возможно, знаками, записанными в поток.
    /// </summary>
    /// <param name="chars">
    ///   Массив символов, содержащий записываемые в поток данные.
    /// </param>
    /// <param name="index">
    ///   Стартовая точка в <paramref name="chars" /> с которого начинается запись.
    /// </param>
    /// <param name="count">Количество символов для записи.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина буфера минус <paramref name="index" /> меньше <paramref name="count" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="chars" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="index" /> или <paramref name="count" /> является отрицательным значением.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(char[] chars, int index, int count)
    {
      byte[] bytes = this._encoding.GetBytes(chars, index, count);
      this.OutStream.Write(bytes, 0, bytes.Length);
    }

    /// <summary>
    ///   Записывает число с плавающей запятой длиной в текущий поток и перемещает позицию в потоке на восемь байт вперед.
    /// </summary>
    /// <param name="value">
    ///   8 байтовое значение с плавающей запятой для записи.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual unsafe void Write(double value)
    {
      ulong num = (ulong) *(long*) &value;
      this._buffer[0] = (byte) num;
      this._buffer[1] = (byte) (num >> 8);
      this._buffer[2] = (byte) (num >> 16);
      this._buffer[3] = (byte) (num >> 24);
      this._buffer[4] = (byte) (num >> 32);
      this._buffer[5] = (byte) (num >> 40);
      this._buffer[6] = (byte) (num >> 48);
      this._buffer[7] = (byte) (num >> 56);
      this.OutStream.Write(this._buffer, 0, 8);
    }

    /// <summary>
    ///   Записывает десятичное число в текущий поток и перемещает позицию в потоке на шестнадцать байтов вперед.
    /// </summary>
    /// <param name="value">
    ///   Десятичное значение, которое необходимо записать.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(Decimal value)
    {
      Decimal.GetBytes(value, this._buffer);
      this.OutStream.Write(this._buffer, 0, 16);
    }

    /// <summary>
    ///   Записывает двухбайтное целое число со знаком в текущий поток и перемещает позицию в потоке на два байта.
    /// </summary>
    /// <param name="value">
    ///   Двухбайтовое целое число со знаком для записи.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(short value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) ((uint) value >> 8);
      this.OutStream.Write(this._buffer, 0, 2);
    }

    /// <summary>
    ///   Записывает двухбайтное целое число без знака в текущий поток и перемещает позицию в потоке на два байта.
    /// </summary>
    /// <param name="value">
    ///   Записываемое целое число без знака 2 байта.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void Write(ushort value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) ((uint) value >> 8);
      this.OutStream.Write(this._buffer, 0, 2);
    }

    /// <summary>
    ///   Записывает четырехбайтовое целое число со знаком в текущий поток и перемещает позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <param name="value">
    ///   Четырехбайтовое целое число со знаком для записи.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(int value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) (value >> 8);
      this._buffer[2] = (byte) (value >> 16);
      this._buffer[3] = (byte) (value >> 24);
      this.OutStream.Write(this._buffer, 0, 4);
    }

    /// <summary>
    ///   Записывает четырехбайтовое целое число без знака в текущий поток и перемещает позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <param name="value">
    ///   Четырехбайтовое целое число без знака для записи.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void Write(uint value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) (value >> 8);
      this._buffer[2] = (byte) (value >> 16);
      this._buffer[3] = (byte) (value >> 24);
      this.OutStream.Write(this._buffer, 0, 4);
    }

    /// <summary>
    ///   Записывает 8 байтовое целое число со знаком в текущий поток и перемещает позицию в потоке на восемь байт вперед.
    /// </summary>
    /// <param name="value">
    ///   8 байтовое целое число со знаком для записи.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Write(long value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) (value >> 8);
      this._buffer[2] = (byte) (value >> 16);
      this._buffer[3] = (byte) (value >> 24);
      this._buffer[4] = (byte) (value >> 32);
      this._buffer[5] = (byte) (value >> 40);
      this._buffer[6] = (byte) (value >> 48);
      this._buffer[7] = (byte) (value >> 56);
      this.OutStream.Write(this._buffer, 0, 8);
    }

    /// <summary>
    ///   Записывает целое число без знака в текущий поток и перемещает позицию в потоке на восемь байт.
    /// </summary>
    /// <param name="value">
    ///   8 байтовое целое число без знака для записи.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public virtual void Write(ulong value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) (value >> 8);
      this._buffer[2] = (byte) (value >> 16);
      this._buffer[3] = (byte) (value >> 24);
      this._buffer[4] = (byte) (value >> 32);
      this._buffer[5] = (byte) (value >> 40);
      this._buffer[6] = (byte) (value >> 48);
      this._buffer[7] = (byte) (value >> 56);
      this.OutStream.Write(this._buffer, 0, 8);
    }

    /// <summary>
    ///   Записывает значение с плавающей запятой 4 байта в текущий поток и перемещает позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <param name="value">
    ///   Записываемое значение с плавающей запятой 4 байта.
    /// </param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual unsafe void Write(float value)
    {
      uint num = *(uint*) &value;
      this._buffer[0] = (byte) num;
      this._buffer[1] = (byte) (num >> 8);
      this._buffer[2] = (byte) (num >> 16);
      this._buffer[3] = (byte) (num >> 24);
      this.OutStream.Write(this._buffer, 0, 4);
    }

    /// <summary>
    ///   Записывает строку с префиксом, обозначающим длину потока в текущей кодировки <see cref="T:System.IO.BinaryWriter" />, и перемещает текущую позицию в потоке в соответствии с кодировку, используемую и символов, записываемый в поток.
    /// </summary>
    /// <param name="value">Значение для записи.</param>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual unsafe void Write(string value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      int byteCount = this._encoding.GetByteCount(value);
      this.Write7BitEncodedInt(byteCount);
      if (this._largeByteBuffer == null)
      {
        this._largeByteBuffer = new byte[256];
        this._maxChars = this._largeByteBuffer.Length / this._encoding.GetMaxByteCount(1);
      }
      if (byteCount <= this._largeByteBuffer.Length)
      {
        this._encoding.GetBytes(value, 0, value.Length, this._largeByteBuffer, 0);
        this.OutStream.Write(this._largeByteBuffer, 0, byteCount);
      }
      else
      {
        int num = 0;
        int length = value.Length;
        while (length > 0)
        {
          int charCount = length > this._maxChars ? this._maxChars : length;
          if (num < 0 || charCount < 0 || checked (num + charCount) > value.Length)
            throw new ArgumentOutOfRangeException("charCount");
          string str = value;
          char* chPtr = (char*) str;
          if ((IntPtr) chPtr != IntPtr.Zero)
            chPtr += RuntimeHelpers.OffsetToStringData;
          int bytes1;
          fixed (byte* bytes2 = this._largeByteBuffer)
            bytes1 = this._encoder.GetBytes((char*) checked (unchecked ((UIntPtr) chPtr) + unchecked ((UIntPtr) checked (unchecked ((IntPtr) num) * 2))), charCount, bytes2, this._largeByteBuffer.Length, charCount == length);
          str = (string) null;
          this.OutStream.Write(this._largeByteBuffer, 0, bytes1);
          num += charCount;
          length -= charCount;
        }
      }
    }

    /// <summary>
    ///   Записывает 32-разрядное целое число в сжатом формате.
    /// </summary>
    /// <param name="value">32-разрядное целое для записи.</param>
    /// <exception cref="T:System.IO.EndOfStreamException">
    ///   Достигнут конец потока.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Поток закрыт.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Поток закрыт.
    /// </exception>
    [__DynamicallyInvokable]
    protected void Write7BitEncodedInt(int value)
    {
      uint num = (uint) value;
      while (num >= 128U)
      {
        this.Write((byte) (num | 128U));
        num >>= 7;
      }
      this.Write((byte) num);
    }
  }
}
