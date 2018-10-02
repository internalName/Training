// Decompiled with JetBrains decompiler
// Type: System.Threading.TimerCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>
  ///   Представляет метод, который обрабатывает вызовы из <see cref="T:System.Threading.Timer" />.
  /// </summary>
  /// <param name="state">
  ///   Объект, содержащий сведения о приложении, которые важны для метода, вызванного этим делегатом или <see langword="null" />.
  /// </param>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public delegate void TimerCallback(object state);
}
