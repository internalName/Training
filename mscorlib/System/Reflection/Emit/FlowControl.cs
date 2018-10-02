// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.FlowControl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Описывает, каким образом инструкция меняет поток управления.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum FlowControl
  {
    [__DynamicallyInvokable] Branch,
    [__DynamicallyInvokable] Break,
    [__DynamicallyInvokable] Call,
    [__DynamicallyInvokable] Cond_Branch,
    [__DynamicallyInvokable] Meta,
    [__DynamicallyInvokable] Next,
    [Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")] Phi,
    [__DynamicallyInvokable] Return,
    [__DynamicallyInvokable] Throw,
  }
}
