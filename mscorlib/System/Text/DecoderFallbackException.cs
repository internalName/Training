// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderFallbackException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Text
{
  /// <summary>
  ///   Исключение создается при сбое операции резервирования декодера.
  ///    Этот класс не наследуется.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DecoderFallbackException : ArgumentException
  {
    private byte[] bytesUnknown;
    private int index;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.DecoderFallbackException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public DecoderFallbackException()
      : base(Environment.GetResourceString("Arg_ArgumentException"))
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.DecoderFallbackException" />.
    ///    Параметр определяет сообщение об ошибке.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    [__DynamicallyInvokable]
    public DecoderFallbackException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.DecoderFallbackException" />.
    ///    Параметры указывают сообщение об ошибке и внутреннее исключение, которое стало причиной данного исключения.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="innerException">
    ///   Исключение, вызвавшее данное исключение.
    /// </param>
    [__DynamicallyInvokable]
    public DecoderFallbackException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024809);
    }

    internal DecoderFallbackException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.DecoderFallbackException" />.
    ///    Параметры указывают сообщение об ошибке, массив байтов декодируемого и индекс байта, который не удается декодировать.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="bytesUnknown">Входной массив байтов.</param>
    /// <param name="index">
    ///   Положение индекса в <paramref name="bytesUnknown" /> байта, который не удается декодировать.
    /// </param>
    [__DynamicallyInvokable]
    public DecoderFallbackException(string message, byte[] bytesUnknown, int index)
      : base(message)
    {
      this.bytesUnknown = bytesUnknown;
      this.index = index;
    }

    /// <summary>
    ///   Возвращает входной последовательности байтов, вызвавшего исключение.
    /// </summary>
    /// <returns>Входной массив байтов, не может быть декодирована.</returns>
    [__DynamicallyInvokable]
    public byte[] BytesUnknown
    {
      [__DynamicallyInvokable] get
      {
        return this.bytesUnknown;
      }
    }

    /// <summary>
    ///   Возвращает позицию индекса во входной последовательности байтов байта, вызвавшего исключение.
    /// </summary>
    /// <returns>
    ///   Индекс позиции в массиве байтов байта, который не удается декодировать.
    ///    Индекс начинается с нуля.
    /// </returns>
    [__DynamicallyInvokable]
    public int Index
    {
      [__DynamicallyInvokable] get
      {
        return this.index;
      }
    }
  }
}
