// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderFallbackException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Text
{
  /// <summary>
  ///   Исключение, которое вызывается при сбое во время операции резервирования кодировщика.
  ///    Этот класс не наследуется.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class EncoderFallbackException : ArgumentException
  {
    private char charUnknown;
    private char charUnknownHigh;
    private char charUnknownLow;
    private int index;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.EncoderFallbackException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public EncoderFallbackException()
      : base(Environment.GetResourceString("Arg_ArgumentException"))
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.EncoderFallbackException" />.
    ///    Параметр определяет сообщение об ошибке.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    [__DynamicallyInvokable]
    public EncoderFallbackException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024809);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.EncoderFallbackException" />.
    ///    Параметры указывают сообщение об ошибке и внутреннее исключение, которое стало причиной данного исключения.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="innerException">
    ///   Исключение, вызвавшее данное исключение.
    /// </param>
    [__DynamicallyInvokable]
    public EncoderFallbackException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024809);
    }

    internal EncoderFallbackException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    internal EncoderFallbackException(string message, char charUnknown, int index)
      : base(message)
    {
      this.charUnknown = charUnknown;
      this.index = index;
    }

    internal EncoderFallbackException(string message, char charUnknownHigh, char charUnknownLow, int index)
      : base(message)
    {
      if (!char.IsHighSurrogate(charUnknownHigh))
        throw new ArgumentOutOfRangeException(nameof (charUnknownHigh), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 55296, (object) 56319));
      if (!char.IsLowSurrogate(charUnknownLow))
        throw new ArgumentOutOfRangeException(nameof (CharUnknownLow), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 56320, (object) 57343));
      this.charUnknownHigh = charUnknownHigh;
      this.charUnknownLow = charUnknownLow;
      this.index = index;
    }

    /// <summary>Получает входной символ, вызвавший исключение.</summary>
    /// <returns>Символ, который не может быть закодирован.</returns>
    [__DynamicallyInvokable]
    public char CharUnknown
    {
      [__DynamicallyInvokable] get
      {
        return this.charUnknown;
      }
    }

    /// <summary>
    ///   Возвращает символ старшего компонента суррогатной пары, вызвавший исключение.
    /// </summary>
    /// <returns>
    ///   Символ старшего компонента суррогатной пары, который не может быть закодирован.
    /// </returns>
    [__DynamicallyInvokable]
    public char CharUnknownHigh
    {
      [__DynamicallyInvokable] get
      {
        return this.charUnknownHigh;
      }
    }

    /// <summary>
    ///   Возвращает символ младшего компонента суррогатной пары, вызвавший исключение.
    /// </summary>
    /// <returns>
    ///   Символ младшего компонента суррогатной пары, который не может быть закодирован.
    /// </returns>
    [__DynamicallyInvokable]
    public char CharUnknownLow
    {
      [__DynamicallyInvokable] get
      {
        return this.charUnknownLow;
      }
    }

    /// <summary>
    ///   Возвращает позицию индекса во входном буфере символа, вызвавшего исключение.
    /// </summary>
    /// <returns>
    ///   Позиция индекса во входном буфере символа, который не может быть закодирован.
    /// </returns>
    [__DynamicallyInvokable]
    public int Index
    {
      [__DynamicallyInvokable] get
      {
        return this.index;
      }
    }

    /// <summary>
    ///   Указывает, является ли входные данные, вызвавшего исключение суррогатную пару.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если запрос был суррогатной парой; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsUnknownSurrogate()
    {
      return this.charUnknownHigh > char.MinValue;
    }
  }
}
