// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.FormattableStringFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Предоставляет статический метод для создания объекта <see cref="T:System.FormattableString" /> на основе строки составного формата и ее аргументов.
  /// </summary>
  [__DynamicallyInvokable]
  public static class FormattableStringFactory
  {
    /// <summary>
    ///   Создает экземпляр <see cref="T:System.FormattableString" /> на основе строки составного формата и ее аргументов.
    /// </summary>
    /// <param name="format">Строка составного формата.</param>
    /// <param name="arguments">
    ///   Аргументы, строковые представления которых должны быть вставлены в строку результатов.
    /// </param>
    /// <returns>
    ///   Объект, представляющий строку составного формата и ее аргументы.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="format" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="arguments" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static FormattableString Create(string format, params object[] arguments)
    {
      if (format == null)
        throw new ArgumentNullException(nameof (format));
      if (arguments == null)
        throw new ArgumentNullException(nameof (arguments));
      return (FormattableString) new FormattableStringFactory.ConcreteFormattableString(format, arguments);
    }

    private sealed class ConcreteFormattableString : FormattableString
    {
      private readonly string _format;
      private readonly object[] _arguments;

      internal ConcreteFormattableString(string format, object[] arguments)
      {
        this._format = format;
        this._arguments = arguments;
      }

      public override string Format
      {
        get
        {
          return this._format;
        }
      }

      public override object[] GetArguments()
      {
        return this._arguments;
      }

      public override int ArgumentCount
      {
        get
        {
          return this._arguments.Length;
        }
      }

      public override object GetArgument(int index)
      {
        return this._arguments[index];
      }

      public override string ToString(IFormatProvider formatProvider)
      {
        return string.Format(formatProvider, this._format, this._arguments);
      }
    }
  }
}
