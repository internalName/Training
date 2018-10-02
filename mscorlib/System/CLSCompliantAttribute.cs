// Decompiled with JetBrains decompiler
// Type: System.CLSCompliantAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает, соответствует ли элемент программы спецификации CLS.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class CLSCompliantAttribute : Attribute
  {
    private bool m_compliant;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.CLSCompliantAttribute" /> класса логическое значение, указывающее, является ли указанный элемент программы CLS-совместимым.
    /// </summary>
    /// <param name="isCompliant">
    ///   <see langword="true" />Если CLS-совместимыми. в противном случае <see langword="false" />.
    /// </param>
    [__DynamicallyInvokable]
    public CLSCompliantAttribute(bool isCompliant)
    {
      this.m_compliant = isCompliant;
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, является ли указанный элемент программы CLS-совместимым.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если элемент программы CLS-совместимыми. в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsCompliant
    {
      [__DynamicallyInvokable] get
      {
        return this.m_compliant;
      }
    }
  }
}
