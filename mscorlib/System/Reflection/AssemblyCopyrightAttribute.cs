// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyCopyrightAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Определяет настраиваемый атрибут уведомления об авторских правах для манифеста сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyCopyrightAttribute : Attribute
  {
    private string m_copyright;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AssemblyCopyrightAttribute" />.
    /// </summary>
    /// <param name="copyright">Сведения об авторских правах.</param>
    [__DynamicallyInvokable]
    public AssemblyCopyrightAttribute(string copyright)
    {
      this.m_copyright = copyright;
    }

    /// <summary>Возвращает сведения об авторских правах.</summary>
    /// <returns>Строка, содержащая сведения об авторских правах.</returns>
    [__DynamicallyInvokable]
    public string Copyright
    {
      [__DynamicallyInvokable] get
      {
        return this.m_copyright;
      }
    }
  }
}
