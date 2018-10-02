// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.NTAccount
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Principal
{
  /// <summary>
  ///   Представляет учетную запись пользователя или группы.
  /// </summary>
  [ComVisible(false)]
  public sealed class NTAccount : IdentityReference
  {
    private readonly string _Name;
    internal const int MaximumAccountNameLength = 256;
    internal const int MaximumDomainNameLength = 255;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Principal.NTAccount" /> используя указанное имя домена и имя учетной записи.
    /// </summary>
    /// <param name="domainName">
    ///   Имя домена.
    ///    Этот параметр может быть <see langword="null" /> или пустая строка.
    ///    Имена доменов, имеющие значения null рассматриваются как пустая строка.
    /// </param>
    /// <param name="accountName">
    ///   Имя учетной записи.
    ///    Этот параметр не может быть <see langword="null" /> или пустая строка.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="accountName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="accountName" /> равен пустой строке.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="accountName" /> указано слишком длинное.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="domainName" /> указано слишком длинное.
    /// </exception>
    public NTAccount(string domainName, string accountName)
    {
      if (accountName == null)
        throw new ArgumentNullException(nameof (accountName));
      if (accountName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), nameof (accountName));
      if (accountName.Length > 256)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_AccountNameTooLong"), nameof (accountName));
      if (domainName != null && domainName.Length > (int) byte.MaxValue)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_DomainNameTooLong"), nameof (domainName));
      if (domainName == null || domainName.Length == 0)
        this._Name = accountName;
      else
        this._Name = domainName + "\\" + accountName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Principal.NTAccount" /> используя указанное имя.
    /// </summary>
    /// <param name="name">
    ///   Имя, используемое для создания <see cref="T:System.Security.Principal.NTAccount" /> объекта.
    ///    Этот параметр не может быть <see langword="null" /> или пустая строка.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> равен пустой строке.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="name" /> указано слишком длинное.
    /// </exception>
    public NTAccount(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), nameof (name));
      if (name.Length > 512)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_AccountNameTooLong"), nameof (name));
      this._Name = name;
    }

    /// <summary>
    ///   Возвращает строку в верхнем регистре представление этого <see cref="T:System.Security.Principal.NTAccount" /> объекта.
    /// </summary>
    /// <returns>
    ///   Верхний регистр строковое представление данного объекта <see cref="T:System.Security.Principal.NTAccount" /> объекта.
    /// </returns>
    public override string Value
    {
      get
      {
        return this.ToString();
      }
    }

    /// <summary>
    ///   Возвращает значение, которое указывает, является ли указанный тип типом допустимых эквивалентов для <see cref="T:System.Security.Principal.NTAccount" /> класса.
    /// </summary>
    /// <param name="targetType">
    ///   Запрашиваемый тип для действия в качестве преобразования из <see cref="T:System.Security.Principal.NTAccount" />.
    ///    Допустимы следующие типы целевого объекта:
    /// 
    ///   - <see cref="T:System.Security.Principal.NTAccount" />
    /// 
    ///   - <see cref="T:System.Security.Principal.SecurityIdentifier" />
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="targetType" /> является допустимым перевода для <see cref="T:System.Security.Principal.NTAccount" /> класса; в противном случае <see langword="false" />.
    /// </returns>
    public override bool IsValidTargetType(Type targetType)
    {
      return targetType == typeof (SecurityIdentifier) || targetType == typeof (NTAccount);
    }

    /// <summary>
    ///   Преобразует имя учетной записи, представленной <see cref="T:System.Security.Principal.NTAccount" /> объекта в другой <see cref="T:System.Security.Principal.IdentityReference" />-производный тип.
    /// </summary>
    /// <param name="targetType">
    ///   Целевой тип для преобразования из <see cref="T:System.Security.Principal.NTAccount" />.
    ///    Целевой тип должен быть типом, которые считаются допустимыми, <see cref="M:System.Security.Principal.NTAccount.IsValidTargetType(System.Type)" /> метод.
    /// </param>
    /// <returns>Преобразованное удостоверение.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="targetType " />— <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="targetType " />не <see cref="T:System.Security.Principal.IdentityReference" />  типа.
    /// </exception>
    /// <exception cref="T:System.Security.Principal.IdentityNotMappedException">
    ///   Некоторые или ссылки на свойства нельзя преобразовать.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Указано слишком длинное имя учетной записи источника.
    /// 
    ///   -или-
    /// 
    ///   Возвращен код ошибки Win32.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
    public override IdentityReference Translate(Type targetType)
    {
      if (targetType == (Type) null)
        throw new ArgumentNullException(nameof (targetType));
      if (targetType == typeof (NTAccount))
        return (IdentityReference) this;
      if (!(targetType == typeof (SecurityIdentifier)))
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), nameof (targetType));
      return NTAccount.Translate(new IdentityReferenceCollection(1)
      {
        (IdentityReference) this
      }, targetType, true)[0];
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли это <see cref="T:System.Security.Principal.NTAccount" /> объект равен указанному объекту.
    /// </summary>
    /// <param name="o">
    ///   Объект для сравнения с данным <see cref="T:System.Security.Principal.NTAccount" /> объекта, или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="o" /> является объект с тем же базовым типом и значением, что <see cref="T:System.Security.Principal.NTAccount" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      if (o == null)
        return false;
      NTAccount ntAccount = o as NTAccount;
      if (ntAccount == (NTAccount) null)
        return false;
      return this == ntAccount;
    }

    /// <summary>
    ///   Служит хэш-функцией текущего <see cref="T:System.Security.Principal.NTAccount" /> объекта.
    ///   <see cref="M:System.Security.Principal.NTAccount.GetHashCode" /> Метод подходит для использования в алгоритмах и структурах данных подобных хэш-таблицу хеширования.
    /// </summary>
    /// <returns>
    ///   Хэш-значение для текущего <see cref="T:System.Security.Principal.NTAccount" /> объекта.
    /// </returns>
    public override int GetHashCode()
    {
      return StringComparer.InvariantCultureIgnoreCase.GetHashCode(this._Name);
    }

    /// <summary>
    ///   Возвращает имя учетной записи в домена\учетную запись формат для учетной записи, представленной <see cref="T:System.Security.Principal.NTAccount" /> объекта.
    /// </summary>
    /// <returns>Имя учетной записи, в домена\учетную запись формат.</returns>
    public override string ToString()
    {
      return this._Name;
    }

    [SecurityCritical]
    internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceAccounts, Type targetType, bool forceSuccess)
    {
      bool someFailed = false;
      IdentityReferenceCollection referenceCollection = NTAccount.Translate(sourceAccounts, targetType, out someFailed);
      if (forceSuccess & someFailed)
      {
        IdentityReferenceCollection unmappedIdentities = new IdentityReferenceCollection();
        foreach (IdentityReference identity in referenceCollection)
        {
          if (identity.GetType() != targetType)
            unmappedIdentities.Add(identity);
        }
        throw new IdentityNotMappedException(Environment.GetResourceString("IdentityReference_IdentityNotMapped"), unmappedIdentities);
      }
      return referenceCollection;
    }

    [SecurityCritical]
    internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceAccounts, Type targetType, out bool someFailed)
    {
      if (sourceAccounts == null)
        throw new ArgumentNullException(nameof (sourceAccounts));
      if (targetType == typeof (SecurityIdentifier))
        return NTAccount.TranslateToSids(sourceAccounts, out someFailed);
      throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), nameof (targetType));
    }

    /// <summary>
    ///   Сравнивает два <see cref="T:System.Security.Principal.NTAccount" /> объектов, чтобы определить, равны ли они.
    ///    Они считаются равными, если они имеют одинаковое представление каноническое имя как возвращенного <see cref="P:System.Security.Principal.NTAccount.Value" /> свойства или если они находятся <see langword="null" />.
    /// </summary>
    /// <param name="left">
    ///   Левый операнд, используемый для сравнения равенства.
    ///    Этот параметр может иметь значение <see langword="null" />.
    /// </param>
    /// <param name="right">
    ///   Правый операнд, используемый для сравнения равенства.
    ///    Этот параметр может иметь значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="left" /> и <paramref name="right" /> равны; в противном случае <see langword="false" />.
    /// </returns>
    public static bool operator ==(NTAccount left, NTAccount right)
    {
      object obj1 = (object) left;
      object obj2 = (object) right;
      if (obj1 == null && obj2 == null)
        return true;
      if (obj1 == null || obj2 == null)
        return false;
      return left.ToString().Equals(right.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///   Сравнивает два <see cref="T:System.Security.Principal.NTAccount" /> объектов, чтобы определить, не равны ли они.
    ///    Они считаются неравными, если они имеют разные канонические представления имени возвращенного <see cref="P:System.Security.Principal.NTAccount.Value" /> свойства или если один из объектов является <see langword="null" /> и другая — нет.
    /// </summary>
    /// <param name="left">
    ///   Левый операнд, используемый для сравнения неравенства.
    ///    Этот параметр может иметь значение <see langword="null" />.
    /// </param>
    /// <param name="right">
    ///   Правый операнд, используемый для сравнения неравенства.
    ///    Этот параметр может иметь значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="left" /> и <paramref name="right" /> не равны; в противном случае <see langword="false" />.
    /// </returns>
    public static bool operator !=(NTAccount left, NTAccount right)
    {
      return !(left == right);
    }

    [SecurityCritical]
    private static IdentityReferenceCollection TranslateToSids(IdentityReferenceCollection sourceAccounts, out bool someFailed)
    {
      if (sourceAccounts == null)
        throw new ArgumentNullException(nameof (sourceAccounts));
      if (sourceAccounts.Count == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_EmptyCollection"), nameof (sourceAccounts));
      SafeLsaPolicyHandle handle = SafeLsaPolicyHandle.InvalidHandle;
      SafeLsaMemoryHandle invalidHandle1 = SafeLsaMemoryHandle.InvalidHandle;
      SafeLsaMemoryHandle invalidHandle2 = SafeLsaMemoryHandle.InvalidHandle;
      try
      {
        Win32Native.UNICODE_STRING[] names = new Win32Native.UNICODE_STRING[sourceAccounts.Count];
        int index1 = 0;
        foreach (IdentityReference sourceAccount in sourceAccounts)
        {
          NTAccount ntAccount = sourceAccount as NTAccount;
          if (ntAccount == (NTAccount) null)
            throw new ArgumentException(Environment.GetResourceString("Argument_ImproperType"), nameof (sourceAccounts));
          names[index1].Buffer = ntAccount.ToString();
          if (names[index1].Buffer.Length * 2 + 2 > (int) ushort.MaxValue)
            throw new SystemException();
          names[index1].Length = (ushort) (names[index1].Buffer.Length * 2);
          names[index1].MaximumLength = (ushort) ((uint) names[index1].Length + 2U);
          ++index1;
        }
        handle = System.Security.Principal.Win32.LsaOpenPolicy((string) null, PolicyRights.POLICY_LOOKUP_NAMES);
        someFailed = false;
        uint num = !System.Security.Principal.Win32.LsaLookupNames2Supported ? Win32Native.LsaLookupNames(handle, sourceAccounts.Count, names, ref invalidHandle1, ref invalidHandle2) : Win32Native.LsaLookupNames2(handle, 0, sourceAccounts.Count, names, ref invalidHandle1, ref invalidHandle2);
        switch (num)
        {
          case 0:
            IdentityReferenceCollection referenceCollection = new IdentityReferenceCollection(sourceAccounts.Count);
            if (num == 0U || num == 263U)
            {
              if (System.Security.Principal.Win32.LsaLookupNames2Supported)
              {
                invalidHandle2.Initialize((uint) sourceAccounts.Count, (uint) Marshal.SizeOf(typeof (Win32Native.LSA_TRANSLATED_SID2)));
                System.Security.Principal.Win32.InitializeReferencedDomainsPointer(invalidHandle1);
                Win32Native.LSA_TRANSLATED_SID2[] array = new Win32Native.LSA_TRANSLATED_SID2[sourceAccounts.Count];
                invalidHandle2.ReadArray<Win32Native.LSA_TRANSLATED_SID2>(0UL, array, 0, array.Length);
                for (int index2 = 0; index2 < sourceAccounts.Count; ++index2)
                {
                  Win32Native.LSA_TRANSLATED_SID2 lsaTranslatedSiD2 = array[index2];
                  switch (lsaTranslatedSiD2.Use)
                  {
                    case 1:
                    case 2:
                    case 4:
                    case 5:
                    case 9:
                      referenceCollection.Add((IdentityReference) new SecurityIdentifier(lsaTranslatedSiD2.Sid, true));
                      break;
                    default:
                      someFailed = true;
                      referenceCollection.Add(sourceAccounts[index2]);
                      break;
                  }
                }
              }
              else
              {
                invalidHandle2.Initialize((uint) sourceAccounts.Count, (uint) Marshal.SizeOf(typeof (Win32Native.LSA_TRANSLATED_SID)));
                System.Security.Principal.Win32.InitializeReferencedDomainsPointer(invalidHandle1);
                Win32Native.LSA_REFERENCED_DOMAIN_LIST referencedDomainList = invalidHandle1.Read<Win32Native.LSA_REFERENCED_DOMAIN_LIST>(0UL);
                SecurityIdentifier[] securityIdentifierArray = new SecurityIdentifier[referencedDomainList.Entries];
                for (int index2 = 0; index2 < referencedDomainList.Entries; ++index2)
                {
                  Win32Native.LSA_TRUST_INFORMATION structure = (Win32Native.LSA_TRUST_INFORMATION) Marshal.PtrToStructure(new IntPtr((long) referencedDomainList.Domains + (long) (index2 * Marshal.SizeOf(typeof (Win32Native.LSA_TRUST_INFORMATION)))), typeof (Win32Native.LSA_TRUST_INFORMATION));
                  securityIdentifierArray[index2] = new SecurityIdentifier(structure.Sid, true);
                }
                Win32Native.LSA_TRANSLATED_SID[] array = new Win32Native.LSA_TRANSLATED_SID[sourceAccounts.Count];
                invalidHandle2.ReadArray<Win32Native.LSA_TRANSLATED_SID>(0UL, array, 0, array.Length);
                for (int index2 = 0; index2 < sourceAccounts.Count; ++index2)
                {
                  Win32Native.LSA_TRANSLATED_SID lsaTranslatedSid = array[index2];
                  switch (lsaTranslatedSid.Use)
                  {
                    case 1:
                    case 2:
                    case 4:
                    case 5:
                    case 9:
                      referenceCollection.Add((IdentityReference) new SecurityIdentifier(securityIdentifierArray[lsaTranslatedSid.DomainIndex], lsaTranslatedSid.Rid));
                      break;
                    default:
                      someFailed = true;
                      referenceCollection.Add(sourceAccounts[index2]);
                      break;
                  }
                }
              }
            }
            else
            {
              for (int index2 = 0; index2 < sourceAccounts.Count; ++index2)
                referenceCollection.Add(sourceAccounts[index2]);
            }
            return referenceCollection;
          case 263:
          case 3221225587:
            someFailed = true;
            goto case 0;
          case 3221225495:
          case 3221225626:
            throw new OutOfMemoryException();
          case 3221225506:
            throw new UnauthorizedAccessException();
          default:
            throw new SystemException(Win32Native.GetMessage(Win32Native.LsaNtStatusToWinError((int) num)));
        }
      }
      finally
      {
        handle.Dispose();
        invalidHandle1.Dispose();
        invalidHandle2.Dispose();
      }
    }
  }
}
