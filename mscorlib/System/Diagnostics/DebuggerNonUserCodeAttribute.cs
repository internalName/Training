// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerNonUserCodeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>
  ///   Определяет тип или член, который не является частью пользовательского кода для приложения.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DebuggerNonUserCodeAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.DebuggerNonUserCodeAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public DebuggerNonUserCodeAttribute()
    {
    }
  }
}
