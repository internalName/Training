// Decompiled with JetBrains decompiler
// Type: System.EventHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Представляет метод, обрабатывающий событие, не имеющее данных.
  /// </summary>
  /// <param name="sender">Источник события.</param>
  /// <param name="e">Объект, не содержащий данных события.</param>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public delegate void EventHandler(object sender, EventArgs e);
}
