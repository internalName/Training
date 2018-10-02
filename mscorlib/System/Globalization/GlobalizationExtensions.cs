// Decompiled with JetBrains decompiler
// Type: System.Globalization.GlobalizationExtensions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  /// <summary>
  ///   Предоставляет методы расширения, относящиеся к глобализации.
  /// </summary>
  public static class GlobalizationExtensions
  {
    private const CompareOptions ValidCompareMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort);

    /// <summary>
    ///   Возвращает <see cref="T:System.StringComparer" /> в соответствии с правилами сравнения строк с учетом языка и региональных параметров указанного объекта <see cref="T:System.Globalization.CompareInfo" />.
    /// </summary>
    /// <param name="compareInfo">
    ///   Объект, поддерживающий сравнение строк с учетом языка и региональных параметров.
    /// </param>
    /// <param name="options">
    ///   Значение, определяющее способ сравнения строк.
    ///   <paramref name="options" /> является значением перечисления <see cref="F:System.Globalization.CompareOptions.Ordinal" /> или побитовой комбинацией одного или нескольких следующих значений: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" /> и <see cref="F:System.Globalization.CompareOptions.StringSort" />.
    /// </param>
    /// <returns>
    ///   Объект, который может использоваться для сравнения строк.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="compareInfo" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="options" /> недопустим.
    /// </exception>
    public static StringComparer GetStringComparer(this CompareInfo compareInfo, CompareOptions options)
    {
      if (compareInfo == null)
        throw new ArgumentNullException(nameof (compareInfo));
      if (options == CompareOptions.Ordinal)
        return StringComparer.Ordinal;
      if (options == CompareOptions.OrdinalIgnoreCase)
        return StringComparer.OrdinalIgnoreCase;
      if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (options));
      return (StringComparer) new CultureAwareComparer(compareInfo, options);
    }
  }
}
