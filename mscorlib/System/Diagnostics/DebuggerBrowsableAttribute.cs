// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerBrowsableAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>
  ///   Определяет наличие и способ отображения членов в окнах переменных отладчика.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DebuggerBrowsableAttribute : Attribute
  {
    private DebuggerBrowsableState state;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.DebuggerBrowsableAttribute" />.
    /// </summary>
    /// <param name="state">
    ///   Один из <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> значений, определяющих способ отображения элемента.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="state" /> не является одним из значений <see cref="T:System.Diagnostics.DebuggerBrowsableState" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DebuggerBrowsableAttribute(DebuggerBrowsableState state)
    {
      switch (state)
      {
        case DebuggerBrowsableState.Never:
        case (DebuggerBrowsableState) 1:
        case DebuggerBrowsableState.Collapsed:
        case DebuggerBrowsableState.RootHidden:
          this.state = state;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (state));
      }
    }

    /// <summary>Получает состояние отображения атрибута.</summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Diagnostics.DebuggerBrowsableState" />.
    /// </returns>
    [__DynamicallyInvokable]
    public DebuggerBrowsableState State
    {
      [__DynamicallyInvokable] get
      {
        return this.state;
      }
    }
  }
}
