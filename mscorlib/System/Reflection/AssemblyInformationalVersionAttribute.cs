// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyInformationalVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Определяет дополнительные сведения о версии для манифеста сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyInformationalVersionAttribute : Attribute
  {
    private string m_informationalVersion;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AssemblyInformationalVersionAttribute" />.
    /// </summary>
    /// <param name="informationalVersion">
    ///   Сведения о версии сборки.
    /// </param>
    [__DynamicallyInvokable]
    public AssemblyInformationalVersionAttribute(string informationalVersion)
    {
      this.m_informationalVersion = informationalVersion;
    }

    /// <summary>Возвращает сведения о версии.</summary>
    /// <returns>Строка, содержащая сведения о версии.</returns>
    [__DynamicallyInvokable]
    public string InformationalVersion
    {
      [__DynamicallyInvokable] get
      {
        return this.m_informationalVersion;
      }
    }
  }
}
