// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.RuntimeCompatibilityAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает, нужно ли заключать исключения, которые не являются производными от <see cref="T:System.Exception" /> класса <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> объекта.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class RuntimeCompatibilityAttribute : Attribute
  {
    private bool m_wrapNonExceptionThrows;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.RuntimeCompatibilityAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public RuntimeCompatibilityAttribute()
    {
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, нужно ли заключать исключения, которые не являются производными от <see cref="T:System.Exception" /> класса <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если исключения, которые не являются производными от <see cref="T:System.Exception" /> класс должен отображаться оболочку с <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool WrapNonExceptionThrows
    {
      [__DynamicallyInvokable] get
      {
        return this.m_wrapNonExceptionThrows;
      }
      [__DynamicallyInvokable] set
      {
        this.m_wrapNonExceptionThrows = value;
      }
    }
  }
}
