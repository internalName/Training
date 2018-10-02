// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AuthorizationRuleCollection
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет коллекцию объектов <see cref="T:System.Security.AccessControl.AuthorizationRule" />.
  /// </summary>
  public sealed class AuthorizationRuleCollection : ReadOnlyCollectionBase
  {
    /// <summary>
    ///   Добавляет объект <see cref="T:System.Web.Configuration.AuthorizationRule" /> в коллекцию.
    /// </summary>
    /// <param name="rule">
    ///   Объект <see cref="T:System.Web.Configuration.AuthorizationRule" /> для добавления в коллекцию.
    /// </param>
    public void AddRule(AuthorizationRule rule)
    {
      this.InnerList.Add((object) rule);
    }

    /// <summary>Копирует содержимое коллекции в массив.</summary>
    /// <param name="rules">
    ///   Массив, в который следует скопировать содержимое коллекции.
    /// </param>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс, с которого должно начаться копирование.
    /// </param>
    public void CopyTo(AuthorizationRule[] rules, int index)
    {
      this.CopyTo((Array) rules, index);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Security.AccessControl.AuthorizationRule" /> по указанному индексу коллекции.
    /// </summary>
    /// <param name="index">
    ///   Отсчитываемый от нуля индекс возвращаемого объекта <see cref="T:System.Security.AccessControl.AuthorizationRule" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.AccessControl.AuthorizationRule" />, расположенный по указанному индексу.
    /// </returns>
    public AuthorizationRule this[int index]
    {
      get
      {
        return this.InnerList[index] as AuthorizationRule;
      }
    }
  }
}
