// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderExceptionFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  /// <summary>
  ///   Предоставляет механизм обработки ошибок, называемый резервным вариантом, для входного символа, который не может быть преобразован в выходную последовательность байтов.
  ///    Резервным механизм создает исключение, если входной символ не может быть преобразован в закодированную выходную последовательность байтов.
  ///    Этот класс не наследуется.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class EncoderExceptionFallback : EncoderFallback
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.EncoderExceptionFallback" />.
    /// </summary>
    [__DynamicallyInvokable]
    public EncoderExceptionFallback()
    {
    }

    /// <summary>
    ///   Возвращает буфер резерва кодировщика, который выдает исключение, когда не может преобразовать последовательность символов в последовательность байтов.
    /// </summary>
    /// <returns>
    ///   Буфер резерва кодировщика, который выдает исключение, когда не может закодировать последовательность символов.
    /// </returns>
    [__DynamicallyInvokable]
    public override EncoderFallbackBuffer CreateFallbackBuffer()
    {
      return (EncoderFallbackBuffer) new EncoderExceptionFallbackBuffer();
    }

    /// <summary>
    ///   Получает максимальное число символов, которые может вернуть этот экземпляр.
    /// </summary>
    /// <returns>Возвращаемое значение всегда равно нулю.</returns>
    [__DynamicallyInvokable]
    public override int MaxCharCount
    {
      [__DynamicallyInvokable] get
      {
        return 0;
      }
    }

    /// <summary>
    ///   Указывает, является ли текущий <see cref="T:System.Text.EncoderExceptionFallback" /> объекта и заданный объект равны.
    /// </summary>
    /// <param name="value">
    ///   Объект, который является производным от <see cref="T:System.Text.EncoderExceptionFallback" /> класса.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="value" /> не <see langword="null" /> (<see langword="Nothing" /> в Visual Basic .NET) и <see cref="T:System.Text.EncoderExceptionFallback" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      return value is EncoderExceptionFallback;
    }

    /// <summary>Извлекает хэш-код для этого экземпляра.</summary>
    /// <returns>
    ///   Возвращаемое значение всегда является одинаковым произвольным значением и не имеет особой важности.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return 654;
    }
  }
}
