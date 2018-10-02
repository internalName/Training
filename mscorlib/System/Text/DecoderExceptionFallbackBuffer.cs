// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderExceptionFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Text
{
  /// <summary>
  ///   Вызывает <see cref="T:System.Text.DecoderFallbackException" /> при закодированной входной последовательности байтов не удается преобразовать декодированные выходной символ.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class DecoderExceptionFallbackBuffer : DecoderFallbackBuffer
  {
    /// <summary>
    ///   Создает <see cref="T:System.Text.DecoderFallbackException" /> Если входная последовательность байтов не может быть декодирована.
    ///    Номинальное возвращаемое значение не используется.
    /// </summary>
    /// <param name="bytesUnknown">Входной массив байтов.</param>
    /// <param name="index">
    ///   Позиция индекса байта во входных данных.
    /// </param>
    /// <returns>
    ///   Отсутствует.
    ///    Значение не возвращается, поскольку <see cref="M:System.Text.DecoderExceptionFallbackBuffer.Fallback(System.Byte[],System.Int32)" /> метод всегда создает исключение.
    /// 
    ///   Номинальное возвращаемое значение равно <see langword="true" />.
    ///    Возвращаемое значение определяется, хотя оно не изменяется, так как этот метод реализует абстрактный метод.
    /// </returns>
    /// <exception cref="T:System.Text.DecoderFallbackException">
    ///   Этот метод всегда создает исключение, которое сообщает значение и позицию индекса входного байта, который не удается декодировать.
    /// </exception>
    public override bool Fallback(byte[] bytesUnknown, int index)
    {
      this.Throw(bytesUnknown, index);
      return true;
    }

    /// <summary>
    ///   Извлекает следующий символ в буфере данных исключения.
    /// </summary>
    /// <returns>
    ///   Возвращаемое значение всегда является символом Юникода NULL (U + 0000).
    /// 
    ///   Возвращаемое значение определяется, хотя оно не изменяется, так как этот метод реализует абстрактный метод.
    /// </returns>
    public override char GetNextChar()
    {
      return char.MinValue;
    }

    /// <summary>
    ///   Вызывает следующий вызов <see cref="M:System.Text.DecoderExceptionFallbackBuffer.GetNextChar" /> для доступа к исключение данных буфера позиции, предшествующей текущей позиции.
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
    ///   Возвращает число символов в текущем <see cref="T:System.Text.DecoderExceptionFallbackBuffer" /> объекта, остаются для обработки.
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

    private void Throw(byte[] bytesUnknown, int index)
    {
      StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
      int index1;
      for (index1 = 0; index1 < bytesUnknown.Length && index1 < 20; ++index1)
      {
        stringBuilder.Append("[");
        stringBuilder.Append(bytesUnknown[index1].ToString("X2", (IFormatProvider) CultureInfo.InvariantCulture));
        stringBuilder.Append("]");
      }
      if (index1 == 20)
        stringBuilder.Append(" ...");
      throw new DecoderFallbackException(Environment.GetResourceString("Argument_InvalidCodePageBytesIndex", (object) stringBuilder, (object) index), bytesUnknown, index);
    }
  }
}
