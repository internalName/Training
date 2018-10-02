// Decompiled with JetBrains decompiler
// Type: System.Threading.EventResetMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>
  ///   Указывает, является ли <see cref="T:System.Threading.EventWaitHandle" /> сбрасывается автоматически или вручную после получения сигнала.
  /// </summary>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  public enum EventResetMode
  {
    [__DynamicallyInvokable] AutoReset,
    [__DynamicallyInvokable] ManualReset,
  }
}
