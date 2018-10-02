// Decompiled with JetBrains decompiler
// Type: System.IFormattable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Предоставляет функциональные возможности для форматирования значения объекта в представление строки.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IFormattable
  {
    /// <summary>
    ///   Форматирует значение текущего экземпляра, используя указанный формат.
    /// </summary>
    /// <param name="format">
    ///   Используемый формат.
    /// 
    ///   -или-
    /// 
    ///   Пустая (null) ссылка (<see langword="Nothing" /> в Visual Basic) для использования формата по умолчанию, заданного для типа реализации <see cref="T:System.IFormattable" />.
    /// </param>
    /// <param name="formatProvider">
    ///   Поставщик для использования формата значения.
    /// 
    ///   -или-
    /// 
    ///   Пустая (null) ссылка (<see langword="Nothing" /> в Visual Basic) для получения сведений о числовом формате из параметра текущего языка операционной системы.
    /// </param>
    /// <returns>Значение текущего экземпляра в указанном формате.</returns>
    [__DynamicallyInvokable]
    string ToString(string format, IFormatProvider formatProvider);
  }
}
