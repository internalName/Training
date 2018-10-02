// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderExceptionFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  /// <summary>
  ///   Предоставляет механизм обработки ошибок, называемый резервным вариантом, для закодированной входной последовательности байтов, которая не может быть преобразована во входной символ.
  ///    Этот резервный механизм выдает исключение вместо декодирования входной последовательности байтов.
  ///    Этот класс не наследуется.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DecoderExceptionFallback : DecoderFallback
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.DecoderExceptionFallback" />.
    /// </summary>
    [__DynamicallyInvokable]
    public DecoderExceptionFallback()
    {
    }

    /// <summary>
    ///   Возвращает резерва декодера буфер, который выдает исключение, если не может преобразовать последовательность байтов в символ.
    /// </summary>
    /// <returns>
    ///   Буфер резерва декодера, который выдает исключение, когда не может декодировать последовательность байтов.
    /// </returns>
    [__DynamicallyInvokable]
    public override DecoderFallbackBuffer CreateFallbackBuffer()
    {
      return (DecoderFallbackBuffer) new DecoderExceptionFallbackBuffer();
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
    ///   Указывает, является ли текущий <see cref="T:System.Text.DecoderExceptionFallback" /> объекта и заданный объект равны.
    /// </summary>
    /// <param name="value">
    ///   Объект, который является производным от <see cref="T:System.Text.DecoderExceptionFallback" /> класса.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="value" /> не <see langword="null" /> и <see cref="T:System.Text.DecoderExceptionFallback" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      return value is DecoderExceptionFallback;
    }

    /// <summary>Извлекает хэш-код для этого экземпляра.</summary>
    /// <returns>
    ///   Возвращаемое значение всегда является одинаковым произвольным значением и не имеет особой важности.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return 879;
    }
  }
}
