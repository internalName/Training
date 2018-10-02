// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyTitleAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>Задает описание сборки.</summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyTitleAttribute : Attribute
  {
    private string m_title;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AssemblyTitleAttribute" />.
    /// </summary>
    /// <param name="title">Заголовок сборки.</param>
    [__DynamicallyInvokable]
    public AssemblyTitleAttribute(string title)
    {
      this.m_title = title;
    }

    /// <summary>Возвращает сведения о заголовке сборки.</summary>
    /// <returns>Заголовок сборки.</returns>
    [__DynamicallyInvokable]
    public string Title
    {
      [__DynamicallyInvokable] get
      {
        return this.m_title;
      }
    }
  }
}
