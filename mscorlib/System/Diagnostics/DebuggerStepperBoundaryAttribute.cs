// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerStepperBoundaryAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>
  ///   Указывает, что код, следующий атрибут должен выполняться в режиме выполнения, а не шаг.
  /// </summary>
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class DebuggerStepperBoundaryAttribute : Attribute
  {
  }
}
