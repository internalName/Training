// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.FILETIME
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Представляет количество 100-наносекундных интервалов с 10 января 1601 года.
  ///    Эта структура представляет собой 64-разрядное значение.
  /// </summary>
  [__DynamicallyInvokable]
  public struct FILETIME
  {
    /// <summary>
    ///   Задает младшие 32 разряда <see langword="FILETIME" />.
    /// </summary>
    [__DynamicallyInvokable]
    public int dwLowDateTime;
    /// <summary>
    ///   Задает старшие 32 бита <see langword="FILETIME" />.
    /// </summary>
    [__DynamicallyInvokable]
    public int dwHighDateTime;
  }
}
