// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Указывает версию сборки, которой присваиваются атрибуты.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyVersionAttribute : Attribute
  {
    private string m_version;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="AssemblyVersionAttribute" /> с номером версии сборки, которой присваиваются атрибуты.
    /// </summary>
    /// <param name="version">
    ///   Номер версии сборки с соответствующими атрибутами.
    /// </param>
    [__DynamicallyInvokable]
    public AssemblyVersionAttribute(string version)
    {
      this.m_version = version;
    }

    /// <summary>
    ///   Возвращает номер версии сборки с соответствующими атрибутами.
    /// </summary>
    /// <returns>Строка, содержащая номер версии сборки.</returns>
    [__DynamicallyInvokable]
    public string Version
    {
      [__DynamicallyInvokable] get
      {
        return this.m_version;
      }
    }
  }
}
