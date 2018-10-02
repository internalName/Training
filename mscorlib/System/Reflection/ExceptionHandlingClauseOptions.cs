// Decompiled with JetBrains decompiler
// Type: System.Reflection.ExceptionHandlingClauseOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>Определяет типы предложений обработки исключений.</summary>
  [Flags]
  [ComVisible(true)]
  public enum ExceptionHandlingClauseOptions
  {
    Clause = 0,
    Filter = 1,
    Finally = 2,
    Fault = 4,
  }
}
