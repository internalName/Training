// Decompiled with JetBrains decompiler
// Type: System.ResolveEventHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Представляет метод, обрабатывающий <see cref="E:System.AppDomain.TypeResolve" />, <see cref="E:System.AppDomain.ResourceResolve" />, или <see cref="E:System.AppDomain.AssemblyResolve" /> событие <see cref="T:System.AppDomain" />.
  /// </summary>
  /// <param name="sender">Источник события.</param>
  /// <param name="args">Данные события.</param>
  /// <returns>
  ///   Сборки, которая разрешает тип, сборку или ресурс; или <see langword="null" /> Если сборку не удается разрешить.
  /// </returns>
  [ComVisible(true)]
  [Serializable]
  public delegate Assembly ResolveEventHandler(object sender, ResolveEventArgs args);
}
