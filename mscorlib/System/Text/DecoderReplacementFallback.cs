// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderReplacementFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  /// <summary>
  ///   Предоставляет механизм обработки ошибок, называемый резервным вариантом, закодированной входной последовательности байтов, которая не может быть преобразована в выходной символ.
  ///    В резервном варианте вместо декодированной последовательности байтов выпускается заданная пользователем замещающая строка.
  ///    Этот класс не наследуется.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DecoderReplacementFallback : DecoderFallback
  {
    private string strDefault;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.DecoderReplacementFallback" />.
    /// </summary>
    [__DynamicallyInvokable]
    public DecoderReplacementFallback()
      : this("?")
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Text.DecoderReplacementFallback" /> с использованием указанной строкой замены.
    /// </summary>
    /// <param name="replacement">
    ///   Строка, которая выпущена в операции декодирования вместо входной последовательности байтов, которая не может быть декодирована.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="replacement" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="replacement" /> содержит недопустимую суррогатную пару.
    ///    Другими словами суррогатная пара не состоит из одного старшего суррогатного компонента, за которым следует один компонент младшим символом-заместителем.
    /// </exception>
    [__DynamicallyInvokable]
    public DecoderReplacementFallback(string replacement)
    {
      if (replacement == null)
        throw new ArgumentNullException(nameof (replacement));
      bool flag = false;
      for (int index = 0; index < replacement.Length; ++index)
      {
        if (char.IsSurrogate(replacement, index))
        {
          if (char.IsHighSurrogate(replacement, index))
          {
            if (!flag)
              flag = true;
            else
              break;
          }
          else
          {
            if (!flag)
            {
              flag = true;
              break;
            }
            flag = false;
          }
        }
        else if (flag)
          break;
      }
      if (flag)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex", (object) nameof (replacement)));
      this.strDefault = replacement;
    }

    /// <summary>
    ///   Возвращает замещающую строку, которая является значение <see cref="T:System.Text.DecoderReplacementFallback" /> объекта.
    /// </summary>
    /// <returns>
    ///   Замещающая строка, которая выпущена вместо входной последовательности байтов, которая не может быть декодирована.
    /// </returns>
    [__DynamicallyInvokable]
    public string DefaultString
    {
      [__DynamicallyInvokable] get
      {
        return this.strDefault;
      }
    }

    /// <summary>
    ///   Создает <see cref="T:System.Text.DecoderFallbackBuffer" /> объект, который инициализируется с замещающей строке <see cref="T:System.Text.DecoderReplacementFallback" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Text.DecoderFallbackBuffer" /> объект, который указывает строку для использования вместо исходной входной последовательности операции декодирования.
    /// </returns>
    [__DynamicallyInvokable]
    public override DecoderFallbackBuffer CreateFallbackBuffer()
    {
      return (DecoderFallbackBuffer) new DecoderReplacementFallbackBuffer(this);
    }

    /// <summary>
    ///   Возвращает число символов в строку замены для <see cref="T:System.Text.DecoderReplacementFallback" /> объекта.
    /// </summary>
    /// <returns>
    ///   Количество символов в строке, которая выпущена вместо последовательности байтов, которая не может быть декодирована, то есть длина строки, возвращенной <see cref="P:System.Text.DecoderReplacementFallback.DefaultString" /> свойство.
    /// </returns>
    [__DynamicallyInvokable]
    public override int MaxCharCount
    {
      [__DynamicallyInvokable] get
      {
        return this.strDefault.Length;
      }
    }

    /// <summary>
    ///   Указывает, является ли значение указанного объекта равен <see cref="T:System.Text.DecoderReplacementFallback" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Text.DecoderReplacementFallback" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="value" /> является <see cref="T:System.Text.DecoderReplacementFallback" /> наличие объекта <see cref="P:System.Text.DecoderReplacementFallback.DefaultString" /> свойство, которое равно <see cref="P:System.Text.DecoderReplacementFallback.DefaultString" /> свойство текущего <see cref="T:System.Text.DecoderReplacementFallback" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      DecoderReplacementFallback replacementFallback = value as DecoderReplacementFallback;
      if (replacementFallback != null)
        return this.strDefault == replacementFallback.strDefault;
      return false;
    }

    /// <summary>
    ///   Извлекает хэш-код для значения <see cref="T:System.Text.DecoderReplacementFallback" /> объект.
    /// </summary>
    /// <returns>Хэш-код значения объекта.</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.strDefault.GetHashCode();
    }
  }
}
