// Decompiled with JetBrains decompiler
// Type: System.ICustomFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Определяет метод, поддерживающий пользовательское форматирование значения объекта.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface ICustomFormatter
  {
    /// <summary>
    ///   Преобразует значение указанного объекта в эквивалентное ему строковое представление с использованием указанного формата и сведений об особенностях форматирования для данного языка и региональных параметров.
    /// </summary>
    /// <param name="format">
    ///   Строка формата, содержащая спецификации форматирования.
    /// </param>
    /// <param name="arg">Объект для форматирования.</param>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о формате для текущего экземпляра.
    /// </param>
    /// <returns>
    ///   Строковое представление значения <paramref name="arg" />, отформатированного в соответствии с <paramref name="format" /> и <paramref name="formatProvider" />.
    /// </returns>
    [__DynamicallyInvokable]
    string Format(string format, object arg, IFormatProvider formatProvider);
  }
}
