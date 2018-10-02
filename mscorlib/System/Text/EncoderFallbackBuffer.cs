// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Text
{
  /// <summary>
  ///   Предоставляет буфер, позволяющий резервному обработчику возвращать в кодировщик альтернативную строку, если он не может закодировать входной символ.
  /// </summary>
  [__DynamicallyInvokable]
  public abstract class EncoderFallbackBuffer
  {
    [SecurityCritical]
    internal unsafe char* charStart;
    [SecurityCritical]
    internal unsafe char* charEnd;
    internal EncoderNLS encoder;
    internal bool setEncoder;
    internal bool bUsedEncoder;
    internal bool bFallingBack;
    internal int iRecursionCount;
    private const int iMaxRecursion = 250;

    /// <summary>
    ///   При переопределении в производном классе готовит резервный буфер для обработки указанного входного символа.
    /// </summary>
    /// <param name="charUnknown">Входной символ.</param>
    /// <param name="index">
    ///   Позиция индекса символа во входном буфере.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если резервный буфер может обработать <paramref name="charUnknown" />; <see langword="false" /> Если резервный буфер игнорирует <paramref name="charUnknown" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool Fallback(char charUnknown, int index);

    /// <summary>
    ///   При переопределении в производном классе готовит резервный буфер для обработки указанной суррогатной пары.
    /// </summary>
    /// <param name="charUnknownHigh">
    ///   Старший символ-заместитель входной пары.
    /// </param>
    /// <param name="charUnknownLow">
    ///   Младший символ-заместитель входной пары.
    /// </param>
    /// <param name="index">
    ///   Позиция индекса суррогатной пары во входном буфере.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если резервный буфер может обработать <paramref name="charUnknownHigh" /> и <paramref name="charUnknownLow" />; <see langword="false" /> Если резервный буфер игнорирует значение суррогатной пары.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool Fallback(char charUnknownHigh, char charUnknownLow, int index);

    /// <summary>
    ///   При переопределении в производном классе извлекает следующий символ в резервном буфере.
    /// </summary>
    /// <returns>Следующий символ в резервном буфере.</returns>
    [__DynamicallyInvokable]
    public abstract char GetNextChar();

    /// <summary>
    ///   При переопределении в производном классе, вызывает следующий вызов <see cref="M:System.Text.EncoderFallbackBuffer.GetNextChar" /> для доступа к позиции буфера данных, предшествующей текущей позиции символа.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="M:System.Text.EncoderFallbackBuffer.MovePrevious" /> операция выполнена успешно; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool MovePrevious();

    /// <summary>
    ///   При переопределении в производном классе, возвращает число символов в текущем <see cref="T:System.Text.EncoderFallbackBuffer" /> объекта, остаются для обработки.
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
      this.charStart = (char*) null;
      this.bFallingBack = false;
      this.iRecursionCount = 0;
      this.Reset();
    }

    [SecurityCritical]
    internal unsafe void InternalInitialize(char* charStart, char* charEnd, EncoderNLS encoder, bool setEncoder)
    {
      this.charStart = charStart;
      this.charEnd = charEnd;
      this.encoder = encoder;
      this.setEncoder = setEncoder;
      this.bUsedEncoder = false;
      this.bFallingBack = false;
      this.iRecursionCount = 0;
    }

    internal char InternalGetNextChar()
    {
      char nextChar = this.GetNextChar();
      this.bFallingBack = nextChar > char.MinValue;
      if (nextChar == char.MinValue)
        this.iRecursionCount = 0;
      return nextChar;
    }

    [SecurityCritical]
    internal virtual unsafe bool InternalFallback(char ch, ref char* chars)
    {
      int index = (int) (chars - this.charStart) - 1;
      if (char.IsHighSurrogate(ch))
      {
        if (chars >= this.charEnd)
        {
          if (this.encoder != null && !this.encoder.MustFlush)
          {
            if (this.setEncoder)
            {
              this.bUsedEncoder = true;
              this.encoder.charLeftOver = ch;
            }
            this.bFallingBack = false;
            return false;
          }
        }
        else
        {
          char ch1 = *chars;
          if (char.IsLowSurrogate(ch1))
          {
            if (this.bFallingBack && this.iRecursionCount++ > 250)
              this.ThrowLastCharRecursive(char.ConvertToUtf32(ch, ch1));
            chars += 2;
            this.bFallingBack = this.Fallback(ch, ch1, index);
            return this.bFallingBack;
          }
        }
      }
      if (this.bFallingBack && this.iRecursionCount++ > 250)
        this.ThrowLastCharRecursive((int) ch);
      this.bFallingBack = this.Fallback(ch, index);
      return this.bFallingBack;
    }

    internal void ThrowLastCharRecursive(int charRecursive)
    {
      throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", (object) charRecursive), "chars");
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.EncoderFallbackBuffer" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected EncoderFallbackBuffer()
    {
    }
  }
}
