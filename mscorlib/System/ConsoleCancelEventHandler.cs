// Decompiled with JetBrains decompiler
// Type: System.ConsoleCancelEventHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Представляет метод, обрабатывающий событие <see cref="E:System.Console.CancelKeyPress" /> объекта <see cref="T:System.Console" />.
  /// </summary>
  /// <param name="sender">Источник события.</param>
  /// <param name="e">
  ///   Объект <see cref="T:System.ConsoleCancelEventArgs" />, содержащий данные события.
  /// </param>
  public delegate void ConsoleCancelEventHandler(object sender, ConsoleCancelEventArgs e);
}
