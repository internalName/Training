// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerStepThroughAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>
  ///   Отдает отладчику указание о сквозной обработке кода (вместо обработки изнутри).
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DebuggerStepThroughAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.DebuggerStepThroughAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public DebuggerStepThroughAttribute()
    {
    }
  }
}
