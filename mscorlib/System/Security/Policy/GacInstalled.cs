// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.GacInstalled
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
  /// <summary>
  ///   Подтверждает, что источником сборки кода в глобальный кэш сборок (GAC) в качестве свидетельства для оценки политики.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class GacInstalled : EvidenceBase, IIdentityPermissionFactory
  {
    /// <summary>
    ///   Создает новое разрешение идентификации, соответствующее текущему объекту.
    /// </summary>
    /// <param name="evidence">
    ///   <see cref="T:System.Security.Policy.Evidence" /> Из которого создается разрешение идентификации.
    /// </param>
    /// <returns>
    ///   Новое разрешение идентификации, соответствующее текущему объекту.
    /// </returns>
    public IPermission CreateIdentityPermission(Evidence evidence)
    {
      return (IPermission) new GacIdentityPermission();
    }

    /// <summary>
    ///   Указывает, эквивалентен ли текущий объект указанному объекту.
    /// </summary>
    /// <param name="o">
    ///   Объект, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="o" /> является <see cref="T:System.Security.Policy.GacInstalled" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      return o is GacInstalled;
    }

    /// <summary>Возвращает хэш-код для текущего объекта.</summary>
    /// <returns>Хэш-код для текущего объекта.</returns>
    public override int GetHashCode()
    {
      return 0;
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new GacInstalled();
    }

    /// <summary>Создает эквивалентную копию текущего объекта.</summary>
    /// <returns>
    ///   Эквивалентная копия <see cref="T:System.Security.Policy.GacInstalled" />.
    /// </returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement(this.GetType().FullName);
      securityElement.AddAttribute("version", "1");
      return securityElement;
    }

    /// <summary>
    ///   Возвращает строковое представление текущего объекта.
    /// </summary>
    /// <returns>Строковое представление текущего объекта.</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }
  }
}
