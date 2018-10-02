// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerBrowsableState
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>
  ///   Предоставляет инструкции по отображению для отладчика.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public enum DebuggerBrowsableState
  {
    [__DynamicallyInvokable] Never = 0,
    [__DynamicallyInvokable] Collapsed = 2,
    [__DynamicallyInvokable] RootHidden = 3,
  }
}
