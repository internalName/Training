// Decompiled with JetBrains decompiler
// Type: System.Text.DecoderReplacementFallbackBuffer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Text
{
  /// <summary>
  ///   Представляет замещающую выходную строку, которая создается, когда не удается декодировать исходной входной последовательности байтов.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class DecoderReplacementFallbackBuffer : DecoderFallbackBuffer
  {
    private int fallbackCount = -1;
    private int fallbackIndex = -1;
    private string strDefault;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Text.DecoderReplacementFallbackBuffer" /> класса с использованием значения <see cref="T:System.Text.DecoderReplacementFallback" /> объект.
    /// </summary>
    /// <param name="fallback">
    ///   Объект <see cref="T:System.Text.DecoderReplacementFallback" /> содержащий строку замены.
    /// </param>
    public DecoderReplacementFallbackBuffer(DecoderReplacementFallback fallback)
    {
      this.strDefault = fallback.DefaultString;
    }

    /// <summary>
    ///   Подготавливает замещающий резервный буфер для использования текущей замещающей строки.
    /// </summary>
    /// <param name="bytesUnknown">
    ///   Входной последовательности байтов.
    ///    Этот параметр учитывается, если не создается исключение.
    /// </param>
    /// <param name="index">
    ///   Позиция индекса байта в <paramref name="bytesUnknown" />.
    ///    Этот параметр игнорируется в данной операции.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если замещающая строка не пуста; <see langword="false" /> Если замещающая строка пуста.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Этот метод вызывается повторно до <see cref="M:System.Text.DecoderReplacementFallbackBuffer.GetNextChar" /> метод считывания всех символов в замещающем резервном буфере.
    /// </exception>
    public override bool Fallback(byte[] bytesUnknown, int index)
    {
      if (this.fallbackCount >= 1)
        this.ThrowLastBytesRecursive(bytesUnknown);
      if (this.strDefault.Length == 0)
        return false;
      this.fallbackCount = this.strDefault.Length;
      this.fallbackIndex = -1;
      return true;
    }

    /// <summary>
    ///   Извлекает следующий символ в замещающем резервном буфере.
    /// </summary>
    /// <returns>Следующий символ в замещающем резервном буфере.</returns>
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
    ///   Вызывает следующий вызов <see cref="M:System.Text.DecoderReplacementFallbackBuffer.GetNextChar" /> для доступа к положение символа в замещающем резервном буфере до текущей позиции символа.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="M:System.Text.DecoderReplacementFallbackBuffer.MovePrevious" /> операция выполнена успешно; в противном случае — <see langword="false" />.
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
    ///   Инициализирует все сведения о внутреннем состоянии и данные в <see cref="T:System.Text.DecoderReplacementFallbackBuffer" /> объекта.
    /// </summary>
    [SecuritySafeCritical]
    public override unsafe void Reset()
    {
      this.fallbackCount = -1;
      this.fallbackIndex = -1;
      this.byteStart = (byte*) null;
    }

    [SecurityCritical]
    internal override unsafe int InternalFallback(byte[] bytes, byte* pBytes)
    {
      return this.strDefault.Length;
    }
  }
}
