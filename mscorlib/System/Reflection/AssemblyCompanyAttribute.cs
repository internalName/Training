// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyCompanyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Определяет настраиваемый атрибут имени организации для манифеста сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyCompanyAttribute : Attribute
  {
    private string m_company;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AssemblyCompanyAttribute" />.
    /// </summary>
    /// <param name="company">Сведения об имени компании.</param>
    [__DynamicallyInvokable]
    public AssemblyCompanyAttribute(string company)
    {
      this.m_company = company;
    }

    /// <summary>Возвращает сведения об имени компании.</summary>
    /// <returns>Строка, содержащая имя компании.</returns>
    [__DynamicallyInvokable]
    public string Company
    {
      [__DynamicallyInvokable] get
      {
        return this.m_company;
      }
    }
  }
}
