// Decompiled with JetBrains decompiler
// Type: System.Threading.Timeout
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>
  ///   Содержит константы, определяющие бесконечные интервалы времени ожидания.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public static class Timeout
  {
    /// <summary>
    ///   Константа, которая используется для задания бесконечного периода ожидания для методов, которые принимают <see cref="T:System.TimeSpan" /> параметр.
    /// </summary>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);
    /// <summary>
    ///   Константа, которая используется для задания бесконечного периода ожидания для методов, которые принимают управления потоками <see cref="T:System.Int32" /> параметр.
    /// </summary>
    [__DynamicallyInvokable]
    public const int Infinite = -1;
    internal const uint UnsignedInfinite = 4294967295;
  }
}
