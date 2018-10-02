// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyTrademarkAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Определяет настраиваемый атрибут товарного знака для манифеста сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyTrademarkAttribute : Attribute
  {
    private string m_trademark;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AssemblyTrademarkAttribute" />.
    /// </summary>
    /// <param name="trademark">Сведения о товарном знаке.</param>
    [__DynamicallyInvokable]
    public AssemblyTrademarkAttribute(string trademark)
    {
      this.m_trademark = trademark;
    }

    /// <summary>Возвращает сведения о товарном знаке.</summary>
    /// <returns>
    ///   A <see langword="String" /> содержащий сведения о товарном знаке.
    /// </returns>
    [__DynamicallyInvokable]
    public string Trademark
    {
      [__DynamicallyInvokable] get
      {
        return this.m_trademark;
      }
    }
  }
}
