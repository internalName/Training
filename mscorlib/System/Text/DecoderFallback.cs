// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Threading;

namespace System.Text
{
  /// <summary>
  ///   Предоставляет механизм обработки ошибок, называемый резервным вариантом, закодированной входной последовательности байтов, которая не может быть преобразована в выходной символ.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class DecoderFallback
  {
    internal bool bIsMicrosoftBestFitFallback;
    private static volatile DecoderFallback replacementFallback;
    private static volatile DecoderFallback exceptionFallback;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (DecoderFallback.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref DecoderFallback.s_InternalSyncObject, obj, (object) null);
        }
        return DecoderFallback.s_InternalSyncObject;
      }
    }

    /// <summary>
    ///   Получает объект, выводящий замещающую строку вместо входной последовательности байтов, не может быть декодирована.
    /// </summary>
    /// <returns>
    ///   Тип, производный от <see cref="T:System.Text.DecoderFallback" /> класса.
    ///    Значение по умолчанию — <see cref="T:System.Text.DecoderReplacementFallback" /> объект, который передает вопросительный знак («?», U + 003F) вместо последовательности байтов неизвестно.
    /// </returns>
    [__DynamicallyInvokable]
    public static DecoderFallback ReplacementFallback
    {
      [__DynamicallyInvokable] get
      {
        if (DecoderFallback.replacementFallback == null)
        {
          lock (DecoderFallback.InternalSyncObject)
          {
            if (DecoderFallback.replacementFallback == null)
              DecoderFallback.replacementFallback = (DecoderFallback) new DecoderReplacementFallback();
          }
        }
        return DecoderFallback.replacementFallback;
      }
    }

    /// <summary>
    ///   Возвращает объект, который создает исключение, если входная последовательность байтов не может быть декодирована.
    /// </summary>
    /// <returns>
    ///   Тип, производный от <see cref="T:System.Text.DecoderFallback" /> класса.
    ///    Значение по умолчанию — <see cref="T:System.Text.DecoderExceptionFallback" /> объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public static DecoderFallback ExceptionFallback
    {
      [__DynamicallyInvokable] get
      {
        if (DecoderFallback.exceptionFallback == null)
        {
          lock (DecoderFallback.InternalSyncObject)
          {
            if (DecoderFallback.exceptionFallback == null)
              DecoderFallback.exceptionFallback = (DecoderFallback) new DecoderExceptionFallback();
          }
        }
        return DecoderFallback.exceptionFallback;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе инициализирует новый экземпляр <see cref="T:System.Text.DecoderFallbackBuffer" /> класса.
    /// </summary>
    /// <returns>
    ///   Объект, который предоставляет резервный буфер для декодера.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract DecoderFallbackBuffer CreateFallbackBuffer();

    /// <summary>
    ///   При переопределении в производном классе возвращает максимальное число символов в текущий <see cref="T:System.Text.DecoderFallback" /> может возвращать объект.
    /// </summary>
    /// <returns>
    ///   Максимальное количество символов текущего <see cref="T:System.Text.DecoderFallback" /> может возвращать объект.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract int MaxCharCount { [__DynamicallyInvokable] get; }

    internal bool IsMicrosoftBestFitFallback
    {
      get
      {
        return this.bIsMicrosoftBestFitFallback;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.DecoderFallback" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected DecoderFallback()
    {
    }
  }
}
