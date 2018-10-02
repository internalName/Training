// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyProductAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Определяет настраиваемый атрибут имени продукта для манифеста сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyProductAttribute : Attribute
  {
    private string m_product;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AssemblyProductAttribute" />.
    /// </summary>
    /// <param name="product">Сведения об имени продукта.</param>
    [__DynamicallyInvokable]
    public AssemblyProductAttribute(string product)
    {
      this.m_product = product;
    }

    /// <summary>Возвращает сведения об имени продукта.</summary>
    /// <returns>Строка, содержащая имя продукта.</returns>
    [__DynamicallyInvokable]
    public string Product
    {
      [__DynamicallyInvokable] get
      {
        return this.m_product;
      }
    }
  }
}
