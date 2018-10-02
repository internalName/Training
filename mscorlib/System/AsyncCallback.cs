// Decompiled with JetBrains decompiler
// Type: System.AsyncCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Ссылается на метод, который должен вызываться при завершении соответствующей асинхронной операции.
  /// </summary>
  /// <param name="ar">Результат асинхронной операции.</param>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public delegate void AsyncCallback(IAsyncResult ar);
}
