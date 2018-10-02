// Decompiled with JetBrains decompiler
// Type: System.Reflection.ModuleResolveEventHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Представляет метод, обрабатывающий <see cref="E:System.Reflection.Assembly.ModuleResolve" /> событие <see cref="T:System.Reflection.Assembly" />.
  /// </summary>
  /// <param name="sender">
  ///   Сборки, которое было источником события.
  /// </param>
  /// <param name="e">
  ///   Аргументы, предоставленные объектом, описывающим событие.
  /// </param>
  /// <returns>Модуль, удовлетворяющий запросу.</returns>
  [ComVisible(true)]
  [Serializable]
  public delegate Module ModuleResolveEventHandler(object sender, ResolveEventArgs e);
}
