// Decompiled with JetBrains decompiler
// Type: System.FormattableString
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System
{
  /// <summary>
  ///   Представляет строку составного формата вместе с аргументами для форматирования.
  /// </summary>
  [__DynamicallyInvokable]
  public abstract class FormattableString : IFormattable
  {
    /// <summary>Возвращает строку составного формата.</summary>
    /// <returns>Строка составного формата.</returns>
    [__DynamicallyInvokable]
    public abstract string Format { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает массив объектов, содержащий один или несколько объектов для форматирования.
    /// </summary>
    /// <returns>
    ///   Массив объектов, содержащий один или несколько объектов для форматирования.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract object[] GetArguments();

    /// <summary>Возвращает число аргументов для форматирования.</summary>
    /// <returns>Число аргументов для форматирования.</returns>
    [__DynamicallyInvokable]
    public abstract int ArgumentCount { [__DynamicallyInvokable] get; }

    /// <summary>Возвращает аргумент в указанной позиции индекса.</summary>
    /// <param name="index">
    ///   Индекс аргумента.
    ///    Значение может меняться в диапазоне от нуля до единицы меньше, чем значение <see cref="P:System.FormattableString.ArgumentCount" />.
    /// </param>
    /// <returns>Аргумент.</returns>
    [__DynamicallyInvokable]
    public abstract object GetArgument(int index);

    /// <summary>
    ///   Возвращает строки, полученные от форматирования строки составного формата вместе с аргументов с использованием соглашений о форматировании для заданного языка и региональных параметров.
    /// </summary>
    /// <param name="formatProvider">
    ///   Объект, предоставляющий сведения о форматировании, связанные с языком и региональными параметрами.
    /// </param>
    /// <returns>
    ///   Результирующую строку в формате с использованием соглашений о <paramref name="formatProvider" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract string ToString(IFormatProvider formatProvider);

    [__DynamicallyInvokable]
    string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
    {
      return this.ToString(formatProvider);
    }

    /// <summary>
    ///   Возвращает результирующую строку, в котором аргументы форматируются с помощью соглашения инвариантных региональных параметров.
    /// </summary>
    /// <param name="formattable">
    ///   Объект, преобразуемый в результирующую строку.
    /// </param>
    /// <returns>
    ///   Строки, полученные от форматирования текущего экземпляра, используя соглашения инвариантных региональных параметров.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="formattable" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static string Invariant(FormattableString formattable)
    {
      if (formattable == null)
        throw new ArgumentNullException(nameof (formattable));
      return formattable.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///   Возвращает строки, полученные от форматирования строки составного формата вместе с аргументов с использованием соглашений о форматировании текущего языка и региональных параметров.
    /// </summary>
    /// <returns>
    ///   Строковое значение в формате с использованием правил текущего языка и региональных параметров.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///   Создает новый экземпляр класса <see cref="T:System.FormattableString" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected FormattableString()
    {
    }
  }
}
