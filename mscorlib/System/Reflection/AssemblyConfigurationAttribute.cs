// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyConfigurationAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Указывает конфигурацию сборки, например окончательную или отладочную, для сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyConfigurationAttribute : Attribute
  {
    private string m_configuration;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AssemblyConfigurationAttribute" />.
    /// </summary>
    /// <param name="configuration">Конфигурация сборки.</param>
    [__DynamicallyInvokable]
    public AssemblyConfigurationAttribute(string configuration)
    {
      this.m_configuration = configuration;
    }

    /// <summary>Возвращает сведения о конфигурации сборки.</summary>
    /// <returns>Строка, содержащая сведения о конфигурации сборки.</returns>
    [__DynamicallyInvokable]
    public string Configuration
    {
      [__DynamicallyInvokable] get
      {
        return this.m_configuration;
      }
    }
  }
}
