// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyDefaultAliasAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Определяет понятный псевдоним по умолчанию для манифеста сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyDefaultAliasAttribute : Attribute
  {
    private string m_defaultAlias;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AssemblyDefaultAliasAttribute" />.
    /// </summary>
    /// <param name="defaultAlias">
    ///   Сведения о псевдониме по умолчанию сборки.
    /// </param>
    [__DynamicallyInvokable]
    public AssemblyDefaultAliasAttribute(string defaultAlias)
    {
      this.m_defaultAlias = defaultAlias;
    }

    /// <summary>Возвращает сведения о псевдониме по умолчанию.</summary>
    /// <returns>
    ///   Строка, содержащая сведения о псевдониме по умолчанию.
    /// </returns>
    [__DynamicallyInvokable]
    public string DefaultAlias
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultAlias;
      }
    }
  }
}
