// Decompiled with JetBrains decompiler
// Type: System.EventHandler`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Представляет метод, обрабатывающий событие, когда событие предоставляет данные.
  /// </summary>
  /// <param name="sender">Источник события.</param>
  /// <param name="e">Объект, содержащий данные о событии.</param>
  /// <typeparam name="TEventArgs">
  ///   Тип данных события, создаваемых событием.
  /// </typeparam>
  [__DynamicallyInvokable]
  [Serializable]
  public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
}
