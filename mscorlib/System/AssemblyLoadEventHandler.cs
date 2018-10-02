// Decompiled with JetBrains decompiler
// Type: System.AssemblyLoadEventHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Представляет метод, обрабатывающий <see cref="E:System.AppDomain.AssemblyLoad" /> событие <see cref="T:System.AppDomain" />.
  /// </summary>
  /// <param name="sender">Источник события.</param>
  /// <param name="args">
  ///   Объект класса <see cref="T:System.AssemblyLoadEventArgs" />, содержащий данные события.
  /// </param>
  [ComVisible(true)]
  [Serializable]
  public delegate void AssemblyLoadEventHandler(object sender, AssemblyLoadEventArgs args);
}
