// Decompiled with JetBrains decompiler
// Type: System.Security.ISecurityPolicyEncodable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
  /// <summary>
  ///   Поддерживает методы, которые преобразуют состояние объекта разрешения и из XML-представление элемента.
  /// </summary>
  [ComVisible(true)]
  public interface ISecurityPolicyEncodable
  {
    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния.
    /// </summary>
    /// <param name="level">
    ///   Контекст уровня политики для разрешения именованного разрешение ссылок на наборы.
    /// </param>
    /// <returns>
    ///   Корневой элемент XML-представления объекта политики.
    /// </returns>
    SecurityElement ToXml(PolicyLevel level);

    /// <summary>
    ///   Восстанавливает объект безопасности с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="e">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <param name="level">
    ///   Контекст уровня политики для разрешения именованного разрешение ссылок на наборы.
    /// </param>
    void FromXml(SecurityElement e, PolicyLevel level);
  }
}
