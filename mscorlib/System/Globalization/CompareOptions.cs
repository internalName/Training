// Decompiled with JetBrains decompiler
// Type: System.Globalization.CompareOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Globalization
{
  /// <summary>
  ///   Определяет параметры сравнения строк для использования с <see cref="T:System.Globalization.CompareInfo" />.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum CompareOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] IgnoreCase = 1,
    [__DynamicallyInvokable] IgnoreNonSpace = 2,
    [__DynamicallyInvokable] IgnoreSymbols = 4,
    [__DynamicallyInvokable] IgnoreKanaType = 8,
    [__DynamicallyInvokable] IgnoreWidth = 16, // 0x00000010
    [__DynamicallyInvokable] OrdinalIgnoreCase = 268435456, // 0x10000000
    [__DynamicallyInvokable] StringSort = 536870912, // 0x20000000
    [__DynamicallyInvokable] Ordinal = 1073741824, // 0x40000000
  }
}
