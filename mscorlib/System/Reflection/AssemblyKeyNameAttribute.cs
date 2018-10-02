// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyKeyNameAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Задает имя контейнера ключей в поставщике служб шифрования, содержащего пару ключей, которая используется для создания строгого имени.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyKeyNameAttribute : Attribute
  {
    private string m_keyName;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.AssemblyKeyNameAttribute" /> класс с именем контейнера, содержащего пару ключей, используемую для создания строгого имени для сборки с данным атрибутом.
    /// </summary>
    /// <param name="keyName">
    ///   Имя контейнера, содержащего пару ключей.
    /// </param>
    [__DynamicallyInvokable]
    public AssemblyKeyNameAttribute(string keyName)
    {
      this.m_keyName = keyName;
    }

    /// <summary>
    ///   Возвращает имя контейнера, содержащего пару ключей, который используется для создания строгого имени для сборки с соответствующими атрибутами.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая имя контейнера, содержащего соответствующую пару ключей.
    /// </returns>
    [__DynamicallyInvokable]
    public string KeyName
    {
      [__DynamicallyInvokable] get
      {
        return this.m_keyName;
      }
    }
  }
}
