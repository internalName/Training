// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyDescriptionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>Предоставляет текстовое описание сборки.</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyDescriptionAttribute : Attribute
  {
    private string m_description;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AssemblyDescriptionAttribute" />.
    /// </summary>
    /// <param name="description">Описание сборки.</param>
    [__DynamicallyInvokable]
    public AssemblyDescriptionAttribute(string description)
    {
      this.m_description = description;
    }

    /// <summary>Возвращает сведения об описании сборки.</summary>
    /// <returns>Строка, содержащая описание сборки.</returns>
    [__DynamicallyInvokable]
    public string Description
    {
      [__DynamicallyInvokable] get
      {
        return this.m_description;
      }
    }
  }
}
