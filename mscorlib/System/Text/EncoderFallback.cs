// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Threading;

namespace System.Text
{
  /// <summary>
  ///   Предоставляет механизм обработки ошибок, называемый резервным вариантом, для входного символа, который не может быть преобразован в выходную последовательность закодированных байтов.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class EncoderFallback
  {
    internal bool bIsMicrosoftBestFitFallback;
    private static volatile EncoderFallback replacementFallback;
    private static volatile EncoderFallback exceptionFallback;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (EncoderFallback.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange<object>(ref EncoderFallback.s_InternalSyncObject, obj, (object) null);
        }
        return EncoderFallback.s_InternalSyncObject;
      }
    }

    /// <summary>
    ///   Получает объект, выводящий замещающую строку вместо входного символа, который не может быть закодирован.
    /// </summary>
    /// <returns>
    ///   Тип, производный от <see cref="T:System.Text.EncoderFallback" /> класса.
    ///    Значение по умолчанию — <see cref="T:System.Text.EncoderReplacementFallback" /> объект, который заменяет неизвестных символов входной символ вопросительного знака («?», U + 003F).
    /// </returns>
    [__DynamicallyInvokable]
    public static EncoderFallback ReplacementFallback
    {
      [__DynamicallyInvokable] get
      {
        if (EncoderFallback.replacementFallback == null)
        {
          lock (EncoderFallback.InternalSyncObject)
          {
            if (EncoderFallback.replacementFallback == null)
              EncoderFallback.replacementFallback = (EncoderFallback) new EncoderReplacementFallback();
          }
        }
        return EncoderFallback.replacementFallback;
      }
    }

    /// <summary>
    ///   Возвращает объект, который создает исключение, если входной символ не может быть закодирован.
    /// </summary>
    /// <returns>
    ///   Тип, производный от <see cref="T:System.Text.EncoderFallback" /> класса.
    ///    Значение по умолчанию — <see cref="T:System.Text.EncoderExceptionFallback" /> объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public static EncoderFallback ExceptionFallback
    {
      [__DynamicallyInvokable] get
      {
        if (EncoderFallback.exceptionFallback == null)
        {
          lock (EncoderFallback.InternalSyncObject)
          {
            if (EncoderFallback.exceptionFallback == null)
              EncoderFallback.exceptionFallback = (EncoderFallback) new EncoderExceptionFallback();
          }
        }
        return EncoderFallback.exceptionFallback;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе инициализирует новый экземпляр <see cref="T:System.Text.EncoderFallbackBuffer" /> класса.
    /// </summary>
    /// <returns>
    ///   Объект, который предоставляет резервный буфер кодировщика.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract EncoderFallbackBuffer CreateFallbackBuffer();

    /// <summary>
    ///   При переопределении в производном классе возвращает максимальное число символов в текущий <see cref="T:System.Text.EncoderFallback" /> может возвращать объект.
    /// </summary>
    /// <returns>
    ///   Максимальное количество символов текущего <see cref="T:System.Text.EncoderFallback" /> может возвращать объект.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract int MaxCharCount { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.EncoderFallback" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected EncoderFallback()
    {
    }
  }
}
