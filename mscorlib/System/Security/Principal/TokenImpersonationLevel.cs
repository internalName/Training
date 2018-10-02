// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.TokenImpersonationLevel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>
  ///   Определяет уровни олицетворения безопасности.
  ///    Уровни олицетворения безопасности, указывающие степень, до которой серверный процесс может действовать от лица клиентского процесса.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum TokenImpersonationLevel
  {
    [__DynamicallyInvokable] None,
    [__DynamicallyInvokable] Anonymous,
    [__DynamicallyInvokable] Identification,
    [__DynamicallyInvokable] Impersonation,
    [__DynamicallyInvokable] Delegation,
  }
}
