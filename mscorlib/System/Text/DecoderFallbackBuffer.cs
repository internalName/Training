// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Security;

namespace System.Text
{
  /// <summary>
  ///   Предоставляет буфер, позволяющий резервному обработчику возвращать в декодер альтернативную строку, если он не может декодировать входную последовательность байтов.
  /// </summary>
  [__DynamicallyInvokable]
  public abstract class DecoderFallbackBuffer
  {
    [SecurityCritical]
    internal unsafe byte* byteStart;
    [SecurityCritical]
    internal unsafe char* charEnd;

    /// <summary>
    ///   При переопределении в производном классе готовит резервный буфер для обработки указанной входной последовательности байтов.
    /// </summary>
    /// <param name="bytesUnknown">Входной массив байтов.</param>
    /// <param name="index">
    ///   Позиция индекса байта в <paramref name="bytesUnknown" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если резервный буфер может обработать <paramref name="bytesUnknown" />; <see langword="false" /> Если резервный буфер игнорирует <paramref name="bytesUnknown" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool Fallback(byte[] bytesUnknown, int index);

    /// <summary>
    ///   При переопределении в производном классе извлекает следующий символ в резервном буфере.
    /// </summary>
    /// <returns>Следующий символ в резервном буфере.</returns>
    [__DynamicallyInvokable]
    public abstract char GetNextChar();

    /// <summary>
    ///   При переопределении в производном классе, вызывает следующий вызов <see cref="M:System.Text.DecoderFallbackBuffer.GetNextChar" /> для доступа к позиции буфера данных, предшествующей текущей позиции символа.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="M:System.Text.DecoderFallbackBuffer.MovePrevious" /> операция выполнена успешно; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool MovePrevious();

    /// <summary>
    ///   При переопределении в производном классе, возвращает число символов в текущем <see cref="T:System.Text.DecoderFallbackBuffer" /> объекта, остаются для обработки.
    /// </summary>
    /// <returns>
    ///   Число символов в текущем резервном буфере, которые еще не были обработаны.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract int Remaining { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Инициализирует все данные и состояние сведения, относящиеся к этому резервному буферу.
    /// </summary>
    [__DynamicallyInvokable]
    public virtual void Reset()
    {
      do
        ;
      while (this.GetNextChar() != char.MinValue);
    }

    [SecurityCritical]
    internal unsafe void InternalReset()
    {
      this.byteStart = (byte*) null;
      this.Reset();
    }

    [SecurityCritical]
    internal unsafe void InternalInitialize(byte* byteStart, char* charEnd)
    {
      this.byteStart = byteStart;
      this.charEnd = charEnd;
    }

    [SecurityCritical]
    internal virtual unsafe bool InternalFallback(byte[] bytes, byte* pBytes, ref char* chars)
    {
      if (this.Fallback(bytes, (int) (pBytes - this.byteStart - (long) bytes.Length)))
      {
        char* chPtr = chars;
        bool flag = false;
        char nextChar;
        while ((nextChar = this.GetNextChar()) != char.MinValue)
        {
          if (char.IsSurrogate(nextChar))
          {
            if (char.IsHighSurrogate(nextChar))
            {
              if (flag)
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
              flag = true;
            }
            else
            {
              if (!flag)
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
              flag = false;
            }
          }
          if (chPtr >= this.charEnd)
            return false;
          *chPtr++ = nextChar;
        }
        if (flag)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
        chars = chPtr;
      }
      return true;
    }

    [SecurityCritical]
    internal virtual unsafe int InternalFallback(byte[] bytes, byte* pBytes)
    {
      if (!this.Fallback(bytes, (int) (pBytes - this.byteStart - (long) bytes.Length)))
        return 0;
      int num = 0;
      bool flag = false;
      char nextChar;
      while ((nextChar = this.GetNextChar()) != char.MinValue)
      {
        if (char.IsSurrogate(nextChar))
        {
          if (char.IsHighSurrogate(nextChar))
          {
            if (flag)
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
            flag = true;
          }
          else
          {
            if (!flag)
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
            flag = false;
          }
        }
        ++num;
      }
      if (flag)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
      return num;
    }

    internal void ThrowLastBytesRecursive(byte[] bytesUnknown)
    {
      StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
      int index;
      for (index = 0; index < bytesUnknown.Length && index < 20; ++index)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(" ");
        stringBuilder.Append(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "\\x{0:X2}", (object) bytesUnknown[index]));
      }
      if (index == 20)
        stringBuilder.Append(" ...");
      throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallbackBytes", (object) stringBuilder.ToString()), nameof (bytesUnknown));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.DecoderFallbackBuffer" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected DecoderFallbackBuffer()
    {
    }
  }
}
