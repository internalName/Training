// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.IMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>
  ///   Задает проверку, чтобы определить, является ли сборка кода членом группы кода.
  /// </summary>
  [ComVisible(true)]
  public interface IMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable
  {
    /// <summary>
    ///   Определяет, удовлетворяет ли указанное свидетельство условию членства.
    /// </summary>
    /// <param name="evidence">
    ///   Набор свидетельств, для которого производится проверка.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанное свидетельство удовлетворяет условию членства; в противном случае — <see langword="false" />.
    /// </returns>
    bool Check(Evidence evidence);

    /// <summary>Создает эквивалентную копию условия членства.</summary>
    /// <returns>Новая, идентичная копия текущего условия членства.</returns>
    IMembershipCondition Copy();

    /// <summary>
    ///   Создает и возвращает строковое представление условия членства.
    /// </summary>
    /// <returns>
    ///   Строковое представление состояния текущего условия членства.
    /// </returns>
    string ToString();

    /// <summary>
    ///   Определяет, равен ли указанный объект <see cref="T:System.Object" /> текущему объекту <see cref="T:System.Object" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект <see cref="T:System.Object" />, с которым сравнивается текущий объект <see cref="T:System.Object" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект <see cref="T:System.Object" /> равен текущему объекту <see cref="T:System.Object" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool Equals(object obj);
  }
}
