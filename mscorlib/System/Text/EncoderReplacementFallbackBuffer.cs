// Decompiled with JetBrains decompiler
// Type: System.Text.EncoderReplacementFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Text
{
  /// <summary>
  ///   Представляет заменяющую входную строку, используемую при первоначального входного символа, который не может быть закодирован.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class EncoderReplacementFallbackBuffer : EncoderFallbackBuffer
  {
    private int fallbackCount = -1;
    private int fallbackIndex = -1;
    private string strDefault;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Text.EncoderReplacementFallbackBuffer" /> класса с использованием значения <see cref="T:System.Text.EncoderReplacementFallback" /> объект.
    /// </summary>
    /// <param name="fallback">
    ///   Объект <see cref="T:System.Text.EncoderReplacementFallback" />.
    /// </param>
    public EncoderReplacementFallbackBuffer(EncoderReplacementFallback fallback)
    {
      this.strDefault = fallback.DefaultString + fallback.DefaultString;
    }

    /// <summary>
    ///   Подготавливает замещающий резервный буфер для использования текущей замещающей строки.
    /// </summary>
    /// <param name="charUnknown">
    ///   Входной символ.
    ///    Этот параметр игнорируется в данной операции, если не создается исключение.
    /// </param>
    /// <param name="index">
    ///   Позиция индекса символа во входном буфере.
    ///    Этот параметр игнорируется в данной операции.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если замещающая строка не пуста; <see langword="false" /> Если замещающая строка пуста.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Этот метод вызывается повторно до <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> метод считывания всех символов в замещающем резервном буфере.
    /// </exception>
    public override bool Fallback(char charUnknown, int index)
    {
      if (this.fallbackCount >= 1)
      {
        if (char.IsHighSurrogate(charUnknown) && this.fallbackCount >= 0 && char.IsLowSurrogate(this.strDefault[this.fallbackIndex + 1]))
          this.ThrowLastCharRecursive(char.ConvertToUtf32(charUnknown, this.strDefault[this.fallbackIndex + 1]));
        this.ThrowLastCharRecursive((int) charUnknown);
      }
      this.fallbackCount = this.strDefault.Length / 2;
      this.fallbackIndex = -1;
      return (uint) this.fallbackCount > 0U;
    }

    /// <summary>
    ///   Указывает ли строку замены может использоваться, если входная суррогатная пара не может быть закодирован или суррогатной пары можно проигнорировать.
    ///    Параметры указывают суррогатную пару и позицию индекса пары во входных данных.
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
    ///   <see langword="true" /> Если замещающая строка не пуста; <see langword="false" /> Если замещающая строка пуста.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Этот метод вызывается повторно до <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> метод считывания замены строки символов.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="charUnknownHigh" /> меньше U + D800 или больше, чем U + D8FF.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="charUnknownLow" /> меньше U + DC00 или больше, чем U + DFFF.
    /// </exception>
    public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
    {
      if (!char.IsHighSurrogate(charUnknownHigh))
        throw new ArgumentOutOfRangeException(nameof (charUnknownHigh), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 55296, (object) 56319));
      if (!char.IsLowSurrogate(charUnknownLow))
        throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 56320, (object) 57343));
      if (this.fallbackCount >= 1)
        this.ThrowLastCharRecursive(char.ConvertToUtf32(charUnknownHigh, charUnknownLow));
      this.fallbackCount = this.strDefault.Length;
      this.fallbackIndex = -1;
      return (uint) this.fallbackCount > 0U;
    }

    /// <summary>
    ///   Извлекает следующий символ в замещающем резервном буфере.
    /// </summary>
    /// <returns>
    ///   Следующий символ Юникода в замещающем резервном буфере, может быть закодирован приложением.
    /// </returns>
    public override char GetNextChar()
    {
      --this.fallbackCount;
      ++this.fallbackIndex;
      if (this.fallbackCount < 0)
        return char.MinValue;
      if (this.fallbackCount != int.MaxValue)
        return this.strDefault[this.fallbackIndex];
      this.fallbackCount = -1;
      return char.MinValue;
    }

    /// <summary>
    ///   Вызывает следующий вызов <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> для получения доступа к положение символа в замещающем резервном буфере до текущей позиции символа.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="M:System.Text.EncoderReplacementFallbackBuffer.MovePrevious" /> операция выполнена успешно; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool MovePrevious()
    {
      if (this.fallbackCount < -1 || this.fallbackIndex < 0)
        return false;
      --this.fallbackIndex;
      ++this.fallbackCount;
      return true;
    }

    /// <summary>
    ///   Возвращает количество символов в замещающем резервном буфере, остаются для обработки.
    /// </summary>
    /// <returns>
    ///   Количество символов в замещающем резервном буфере, которые еще не были обработаны.
    /// </returns>
    public override int Remaining
    {
      get
      {
        if (this.fallbackCount >= 0)
          return this.fallbackCount;
        return 0;
      }
    }

    /// <summary>
    ///   Инициализирует все сведения о внутреннем состоянии и данные в этот экземпляр <see cref="T:System.Text.EncoderReplacementFallbackBuffer" />.
    /// </summary>
    [SecuritySafeCritical]
    public override unsafe void Reset()
    {
      this.fallbackCount = -1;
      this.fallbackIndex = 0;
      this.charStart = (char*) null;
      this.bFallingBack = false;
    }
  }
}
