// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CryptoKeyAuditRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет правило аудита для ключа шифрования.
  ///    Правило аудита представляет сочетание идентификатора пользователя и маски доступа.
  ///    Правило аудита также содержит сведения о том, как правило наследуется дочерними объектами, как это наследование распространяется, а также об условиях проводится аудит.
  /// </summary>
  public sealed class CryptoKeyAuditRule : AuditRule
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.CryptoKeyAuditRule" /> с использованием указанных значений.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило аудита.
    ///    Этот параметр должен быть объектом, который может быть приведен к <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="cryptoKeyRights">
    ///   Операция с ключом шифрования для которого это правило аудита создает записи об аудите.
    /// </param>
    /// <param name="flags">Условия проведения аудита.</param>
    public CryptoKeyAuditRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags)
      : this(identity, CryptoKeyAuditRule.AccessMaskFromRights(cryptoKeyRights), false, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.CryptoKeyAuditRule" /> с использованием указанных значений.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило аудита.
    /// </param>
    /// <param name="cryptoKeyRights">
    ///   Операция с ключом шифрования для которого это правило аудита создает записи об аудите.
    /// </param>
    /// <param name="flags">Условия проведения аудита.</param>
    public CryptoKeyAuditRule(string identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags)
      : this((IdentityReference) new NTAccount(identity), CryptoKeyAuditRule.AccessMaskFromRights(cryptoKeyRights), false, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    private CryptoKeyAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
    {
    }

    /// <summary>
    ///   Получает операцию шифрования ключа, для которого это правило аудита создает записи об аудите.
    /// </summary>
    /// <returns>
    ///   Операция с ключом шифрования для которого это правило аудита создает записи об аудите.
    /// </returns>
    public CryptoKeyRights CryptoKeyRights
    {
      get
      {
        return CryptoKeyAuditRule.RightsFromAccessMask(this.AccessMask);
      }
    }

    private static int AccessMaskFromRights(CryptoKeyRights cryptoKeyRights)
    {
      return (int) cryptoKeyRights;
    }

    internal static CryptoKeyRights RightsFromAccessMask(int accessMask)
    {
      return (CryptoKeyRights) accessMask;
    }
  }
}
