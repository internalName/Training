// Decompiled with JetBrains decompiler
// Type: System.UnhandledExceptionEventHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Представляет метод, который будет обрабатывать событие, вызванное исключением, которое не обрабатывается доменом приложения.
  /// </summary>
  /// <param name="sender">
  ///   Источник события необработанного исключения.
  /// </param>
  /// <param name="e">
  ///   Объект класса <paramref name="UnhandledExceptionEventArgs" />, содержащий данные события.
  /// </param>
  [ComVisible(true)]
  [Serializable]
  public delegate void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e);
}
