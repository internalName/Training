// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyCultureAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Указывает, какой язык и региональные параметры поддерживает сборка.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyCultureAttribute : Attribute
  {
    private string m_culture;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.AssemblyCultureAttribute" /> класса культуру, поддерживаемую сборки.
    /// </summary>
    /// <param name="culture">
    ///   Язык и региональные параметры, поддерживаемые сборкой с данным атрибутом.
    /// </param>
    [__DynamicallyInvokable]
    public AssemblyCultureAttribute(string culture)
    {
      this.m_culture = culture;
    }

    /// <summary>
    ///   Возвращает поддерживаемую культуру сборки с соответствующими атрибутами.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая имя поддерживаемых языка и региональных параметров.
    /// </returns>
    [__DynamicallyInvokable]
    public string Culture
    {
      [__DynamicallyInvokable] get
      {
        return this.m_culture;
      }
    }
  }
}
