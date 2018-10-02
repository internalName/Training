// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderReplacementFallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Text
{
  /// <summary>
  ///   Предоставляет механизм обработки ошибок, называемый резервным вариантом, для входного символа, который не может быть преобразован в выходную последовательность байтов.
  ///    В резервном варианте вместо первоначального входного символа используется заданная пользователем замещающая строка.
  ///    Этот класс не наследуется.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class EncoderReplacementFallback : EncoderFallback
  {
    private string strDefault;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Text.EncoderReplacementFallback" />.
    /// </summary>
    [__DynamicallyInvokable]
    public EncoderReplacementFallback()
      : this("?")
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Text.EncoderReplacementFallback" /> с использованием указанной строкой замены.
    /// </summary>
    /// <param name="replacement">
    ///   Строка, которая преобразуется в операции кодирования вместо входного символа, который не может быть закодирован.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="replacement" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="replacement" /> содержит недопустимую суррогатную пару.
    ///    Другими словами суррогат не состоит из одного старшего суррогатного компонента, за которым следует один компонент младшим символом-заместителем.
    /// </exception>
    [__DynamicallyInvokable]
    public EncoderReplacementFallback(string replacement)
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
    ///   Возвращает замещающую строку, которая является значение <see cref="T:System.Text.EncoderReplacementFallback" /> объекта.
    /// </summary>
    /// <returns>
    ///   Подставляемая строка, которая используется вместо входного символа, который не может быть закодирован.
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
    ///   Создает <see cref="T:System.Text.EncoderFallbackBuffer" /> объект, который инициализируется с замещающей строке <see cref="T:System.Text.EncoderReplacementFallback" /> объекта.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Text.EncoderFallbackBuffer" /> объекта равны <see cref="T:System.Text.EncoderReplacementFallback" /> объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public override EncoderFallbackBuffer CreateFallbackBuffer()
    {
      return (EncoderFallbackBuffer) new EncoderReplacementFallbackBuffer(this);
    }

    /// <summary>
    ///   Возвращает число символов в строку замены для <see cref="T:System.Text.EncoderReplacementFallback" /> объекта.
    /// </summary>
    /// <returns>
    ///   Количество символов в строке, используемой вместо входного символа, который не может быть закодирован.
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
    ///   Указывает, является ли значение указанного объекта равен <see cref="T:System.Text.EncoderReplacementFallback" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект <see cref="T:System.Text.EncoderReplacementFallback" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="value" /> параметр указывает <see cref="T:System.Text.EncoderReplacementFallback" /> объекта и строку замены этого объекта равна замещающей строке <see cref="T:System.Text.EncoderReplacementFallback" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      EncoderReplacementFallback replacementFallback = value as EncoderReplacementFallback;
      if (replacementFallback != null)
        return this.strDefault == replacementFallback.strDefault;
      return false;
    }

    /// <summary>
    ///   Извлекает хэш-код для значения <see cref="T:System.Text.EncoderReplacementFallback" /> объект.
    /// </summary>
    /// <returns>Хэш-код значения объекта.</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.strDefault.GetHashCode();
    }
  }
}
