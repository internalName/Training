// Decompiled with JetBrains decompiler
// Type: System.Threading.WaitCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>
  ///   Представляет метод обратного вызова, выполняющегося потоком из пула потоков.
  /// </summary>
  /// <param name="state">
  ///   Объект, содержащий информацию, используемую методом обратного вызова.
  /// </param>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public delegate void WaitCallback(object state);
}
