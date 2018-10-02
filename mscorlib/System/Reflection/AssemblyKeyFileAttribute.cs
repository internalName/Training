// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyKeyFileAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Задает имя файла, содержащего пару ключей, которая используется для создания строгого имени.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyKeyFileAttribute : Attribute
  {
    private string m_keyFile;

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="AssemblyKeyFileAttribute" /> класс с именем файла, содержащего пару ключей для создания строгого имени для сборки с данным атрибутом.
    /// </summary>
    /// <param name="keyFile">Имя файла, содержащего пару ключей.</param>
    [__DynamicallyInvokable]
    public AssemblyKeyFileAttribute(string keyFile)
    {
      this.m_keyFile = keyFile;
    }

    /// <summary>
    ///   Возвращает имя файла, содержащего пару ключей, используемую для создания строгого имени для сборки с соответствующими атрибутами.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая имя файла, содержащего пару ключей.
    /// </returns>
    [__DynamicallyInvokable]
    public string KeyFile
    {
      [__DynamicallyInvokable] get
      {
        return this.m_keyFile;
      }
    }
  }
}
