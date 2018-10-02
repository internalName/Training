// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.AccessedThroughPropertyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Задает имя свойства, которое обращается к полю с атрибутами.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AccessedThroughPropertyAttribute : Attribute
  {
    private readonly string propertyName;

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="AccessedThroughPropertyAttribute" /> класс с именем свойства, используемого для доступа к полю с атрибутами.
    /// </summary>
    /// <param name="propertyName">
    ///   Имя свойства, используемого для доступа к полю с атрибутами.
    /// </param>
    [__DynamicallyInvokable]
    public AccessedThroughPropertyAttribute(string propertyName)
    {
      this.propertyName = propertyName;
    }

    /// <summary>
    ///   Возвращает имя свойства, используемого для доступа к полю с атрибутами.
    /// </summary>
    /// <returns>
    ///   Имя свойства, используемого для доступа к полю с атрибутами.
    /// </returns>
    [__DynamicallyInvokable]
    public string PropertyName
    {
      [__DynamicallyInvokable] get
      {
        return this.propertyName;
      }
    }
  }
}
