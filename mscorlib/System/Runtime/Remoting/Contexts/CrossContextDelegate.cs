// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.CrossContextDelegate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>
  ///   Представляет метод, который будет обрабатывать запросы на выполнение некоторого кода в другом контексте.
  /// </summary>
  [ComVisible(true)]
  public delegate void CrossContextDelegate();
}
