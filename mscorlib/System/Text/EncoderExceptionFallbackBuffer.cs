// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderExceptionFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  /// <summary>
  ///   Вызывает <see cref="T:System.Text.EncoderFallbackException" /> при входной символ не преобразован в выходную последовательность закодированных байтов.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class EncoderExceptionFallbackBuffer : EncoderFallbackBuffer
  {
    /// <summary>
    ///   Создает исключение, так как входной символ не может быть закодирован.
    ///    Параметры указывают значение и позицию индекса символа, который не может быть преобразован.
    /// </summary>
    /// <param name="charUnknown">Входной символ.</param>
    /// <param name="index">
    ///   Позиция индекса символа во входном буфере.
    /// </param>
    /// <returns>
    ///   Отсутствует.
    ///    Значение не возвращается, поскольку <see cref="M:System.Text.EncoderExceptionFallbackBuffer.Fallback(System.Char,System.Int32)" /> метод всегда создает исключение.
    /// </returns>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   <paramref name="charUnknown" /> не может быть закодирован.
    ///    Этот метод всегда создает исключение, сообщающее значение <paramref name="charUnknown" /> и <paramref name="index" /> параметров.
    /// </exception>
    public override bool Fallback(char charUnknown, int index)
    {
      throw new EncoderFallbackException(Environment.GetResourceString("Argument_InvalidCodePageConversionIndex", (object) (int) charUnknown, (object) index), charUnknown, index);
    }

    /// <summary>
    ///   Создает исключение, так как входной символ не может быть закодирован.
    ///    Параметры указывают значение и позицию индекса суррогатной пары во входных данных, и Номинальное возвращаемое значение не используется.
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
    ///   Отсутствует.
    ///    Значение не возвращается, поскольку <see cref="M:System.Text.EncoderExceptionFallbackBuffer.Fallback(System.Char,System.Char,System.Int32)" /> метод всегда создает исключение.
    /// </returns>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///   Символ, представленный <paramref name="charUnknownHigh" /> и <paramref name="charUnknownLow" /> не может быть закодирован.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Либо <paramref name="charUnknownHigh" /> или <paramref name="charUnknownLow" /> является недопустимым.
    ///   <paramref name="charUnknownHigh" /> не от U + D800 до U + DBFF включительно, или <paramref name="charUnknownLow" /> не от U + DC00 до U + DFFF, включительно.
    /// </exception>
    public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
    {
      if (!char.IsHighSurrogate(charUnknownHigh))
        throw new ArgumentOutOfRangeException(nameof (charUnknownHigh), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 55296, (object) 56319));
      if (!char.IsLowSurrogate(charUnknownLow))
        throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 56320, (object) 57343));
      throw new EncoderFallbackException(Environment.GetResourceString("Argument_InvalidCodePageConversionIndex", (object) char.ConvertToUtf32(charUnknownHigh, charUnknownLow), (object) index), charUnknownHigh, charUnknownLow, index);
    }

    /// <summary>
    ///   Извлекает следующий символ в резервном буфере исключения.
    /// </summary>
    /// <returns>
    ///   Возвращаемое значение всегда равно NULL, символ Юникода (U + 0000).
    /// 
    ///   Возвращаемое значение определяется, хотя оно не изменяется, так как этот метод реализует абстрактный метод.
    /// </returns>
    public override char GetNextChar()
    {
      return char.MinValue;
    }

    /// <summary>
    ///   Вызывает следующий вызов <see cref="M:System.Text.EncoderExceptionFallbackBuffer.GetNextChar" /> для получения доступа к исключение данных буфера позиции, предшествующей текущей позиции.
    /// </summary>
    /// <returns>
    ///   Возвращаемое значение всегда равно <see langword="false" />.
    /// 
    ///   Возвращаемое значение определяется, хотя оно не изменяется, так как этот метод реализует абстрактный метод.
    /// </returns>
    public override bool MovePrevious()
    {
      return false;
    }

    /// <summary>
    ///   Возвращает число символов в текущем <see cref="T:System.Text.EncoderExceptionFallbackBuffer" /> объекта, остаются для обработки.
    /// </summary>
    /// <returns>
    ///   Возвращаемое значение всегда равно нулю.
    /// 
    ///   Возвращаемое значение определяется, хотя оно не изменяется, так как этот метод реализует абстрактный метод.
    /// </returns>
    public override int Remaining
    {
      get
      {
        return 0;
      }
    }
  }
}
