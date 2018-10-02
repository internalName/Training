// Decompiled with JetBrains decompiler
// Type: System.Threading.SendOrPostCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  /// <summary>
  ///   Задает метод, вызываемый при отправке сообщения в контекст синхронизации.
  /// </summary>
  /// <param name="state">Передаваемый делегату объект.</param>
  [__DynamicallyInvokable]
  public delegate void SendOrPostCallback(object state);
}
