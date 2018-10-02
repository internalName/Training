// Decompiled with JetBrains decompiler
// Type: System.Globalization.TimeSpanStyles
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  /// <summary>
  ///   Определяет параметры форматирования, регулирующие синтаксический анализ строк для <see cref="Overload:System.TimeSpan.ParseExact" /> и <see cref="Overload:System.TimeSpan.TryParseExact" /> методы.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum TimeSpanStyles
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] AssumeNegative = 1,
  }
}
