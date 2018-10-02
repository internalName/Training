// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CryptoKeyAccessRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Представляет правило доступа для ключа шифрования.
  ///    Правило доступа представляет сочетание идентификатора пользователя, маски доступа и типа управления доступом ("Разрешить" или "Запретить").
  ///    Объект правила доступа также содержит сведения о том, как правило наследуется дочерними объектами и как это наследование распространяется.
  /// </summary>
  public sealed class CryptoKeyAccessRule : AccessRule
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.CryptoKeyAccessRule" /> с использованием указанных значений.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило доступа.
    ///    Этот параметр должен быть объектом, который может быть приведен к <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    /// </param>
    /// <param name="cryptoKeyRights">
    ///   Операция с ключом шифрования для которой это правило доступа управление доступом.
    /// </param>
    /// <param name="type">Допустимый тип управления доступом.</param>
    public CryptoKeyAccessRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AccessControlType type)
      : this(identity, CryptoKeyAccessRule.AccessMaskFromRights(cryptoKeyRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.CryptoKeyAccessRule" /> с использованием указанных значений.
    /// </summary>
    /// <param name="identity">
    ///   Удостоверение, к которому применяется правило доступа.
    /// </param>
    /// <param name="cryptoKeyRights">
    ///   Операция с ключом шифрования для которой это правило доступа управление доступом.
    /// </param>
    /// <param name="type">Допустимый тип управления доступом.</param>
    public CryptoKeyAccessRule(string identity, CryptoKeyRights cryptoKeyRights, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), CryptoKeyAccessRule.AccessMaskFromRights(cryptoKeyRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    private CryptoKeyAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
    {
    }

    /// <summary>
    ///   Получает операцию шифрования ключа, для которой это правило доступа управляет доступом.
    /// </summary>
    /// <returns>
    ///   Операция с ключом шифрования для которой это правило доступа управление доступом.
    /// </returns>
    public CryptoKeyRights CryptoKeyRights
    {
      get
      {
        return CryptoKeyAccessRule.RightsFromAccessMask(this.AccessMask);
      }
    }

    private static int AccessMaskFromRights(CryptoKeyRights cryptoKeyRights, AccessControlType controlType)
    {
      switch (controlType)
      {
        case AccessControlType.Allow:
          cryptoKeyRights |= CryptoKeyRights.Synchronize;
          break;
        case AccessControlType.Deny:
          if (cryptoKeyRights != CryptoKeyRights.FullControl)
          {
            cryptoKeyRights &= ~CryptoKeyRights.Synchronize;
            break;
          }
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnumValue", (object) controlType, (object) nameof (controlType)), nameof (controlType));
      }
      return (int) cryptoKeyRights;
    }

    internal static CryptoKeyRights RightsFromAccessMask(int accessMask)
    {
      return (CryptoKeyRights) accessMask;
    }
  }
}
