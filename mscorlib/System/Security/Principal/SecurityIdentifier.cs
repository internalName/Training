// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.SecurityIdentifier
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Principal
{
  /// <summary>
  ///   Представляет идентификатор безопасности (SID) и предоставляет операции маршалинга и сравнения для идентификаторов безопасности.
  /// </summary>
  [ComVisible(false)]
  public sealed class SecurityIdentifier : IdentityReference, IComparable<SecurityIdentifier>
  {
    internal static readonly long MaxIdentifierAuthority = 281474976710655;
    internal static readonly byte MaxSubAuthorities = 15;
    /// <summary>
    ///   Возвращает минимальный размер в байтах двоичного представления идентификатора безопасности.
    /// </summary>
    public static readonly int MinBinaryLength = 8;
    /// <summary>
    ///   Возвращает максимальный размер в байтах двоичного представления идентификатора безопасности.
    /// </summary>
    public static readonly int MaxBinaryLength = 8 + (int) SecurityIdentifier.MaxSubAuthorities * 4;
    private IdentifierAuthority _IdentifierAuthority;
    private int[] _SubAuthorities;
    private byte[] _BinaryForm;
    private SecurityIdentifier _AccountDomainSid;
    private bool _AccountDomainSidInitialized;
    private string _SddlForm;

    private void CreateFromParts(IdentifierAuthority identifierAuthority, int[] subAuthorities)
    {
      if (subAuthorities == null)
        throw new ArgumentNullException(nameof (subAuthorities));
      if (subAuthorities.Length > (int) SecurityIdentifier.MaxSubAuthorities)
        throw new ArgumentOutOfRangeException("subAuthorities.Length", (object) subAuthorities.Length, Environment.GetResourceString("IdentityReference_InvalidNumberOfSubauthorities", (object) SecurityIdentifier.MaxSubAuthorities));
      if (identifierAuthority < IdentifierAuthority.NullAuthority || identifierAuthority > (IdentifierAuthority) SecurityIdentifier.MaxIdentifierAuthority)
        throw new ArgumentOutOfRangeException(nameof (identifierAuthority), (object) identifierAuthority, Environment.GetResourceString("IdentityReference_IdentifierAuthorityTooLarge"));
      this._IdentifierAuthority = identifierAuthority;
      this._SubAuthorities = new int[subAuthorities.Length];
      subAuthorities.CopyTo((Array) this._SubAuthorities, 0);
      this._BinaryForm = new byte[8 + 4 * this.SubAuthorityCount];
      this._BinaryForm[0] = SecurityIdentifier.Revision;
      this._BinaryForm[1] = (byte) this.SubAuthorityCount;
      for (byte index = 0; index < (byte) 6; ++index)
        this._BinaryForm[2 + (int) index] = (byte) ((ulong) this._IdentifierAuthority >> (5 - (int) index) * 8 & (ulong) byte.MaxValue);
      for (byte index1 = 0; (int) index1 < this.SubAuthorityCount; ++index1)
      {
        for (byte index2 = 0; index2 < (byte) 4; ++index2)
          this._BinaryForm[8 + 4 * (int) index1 + (int) index2] = (byte) ((ulong) this._SubAuthorities[(int) index1] >> (int) index2 * 8);
      }
    }

    private void CreateFromBinaryForm(byte[] binaryForm, int offset)
    {
      if (binaryForm == null)
        throw new ArgumentNullException(nameof (binaryForm));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), (object) offset, Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (binaryForm.Length - offset < SecurityIdentifier.MinBinaryLength)
        throw new ArgumentOutOfRangeException(nameof (binaryForm), Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
      if ((int) binaryForm[offset] != (int) SecurityIdentifier.Revision)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidSidRevision"), nameof (binaryForm));
      if ((int) binaryForm[offset + 1] > (int) SecurityIdentifier.MaxSubAuthorities)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidNumberOfSubauthorities", (object) SecurityIdentifier.MaxSubAuthorities), nameof (binaryForm));
      int num = 8 + 4 * (int) binaryForm[offset + 1];
      if (binaryForm.Length - offset < num)
        throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"), nameof (binaryForm));
      IdentifierAuthority identifierAuthority = (IdentifierAuthority) (((long) binaryForm[offset + 2] << 40) + ((long) binaryForm[offset + 3] << 32) + ((long) binaryForm[offset + 4] << 24) + ((long) binaryForm[offset + 5] << 16) + ((long) binaryForm[offset + 6] << 8) + (long) binaryForm[offset + 7]);
      int[] subAuthorities = new int[(int) binaryForm[offset + 1]];
      for (byte index = 0; (int) index < (int) binaryForm[offset + 1]; ++index)
        subAuthorities[(int) index] = (int) binaryForm[offset + 8 + 4 * (int) index + 0] + ((int) binaryForm[offset + 8 + 4 * (int) index + 1] << 8) + ((int) binaryForm[offset + 8 + 4 * (int) index + 2] << 16) + ((int) binaryForm[offset + 8 + 4 * (int) index + 3] << 24);
      this.CreateFromParts(identifierAuthority, subAuthorities);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Principal.SecurityIdentifier" /> используя указанный идентификатор безопасности (SID) в формате языка определения дескрипторов безопасности (SDDL).
    /// </summary>
    /// <param name="sddlForm">
    ///   Строка SDDL ИД безопасности используется для создания <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </param>
    [SecuritySafeCritical]
    public SecurityIdentifier(string sddlForm)
    {
      if (sddlForm == null)
        throw new ArgumentNullException(nameof (sddlForm));
      byte[] resultSid;
      int sidFromString = System.Security.Principal.Win32.CreateSidFromString(sddlForm, out resultSid);
      switch (sidFromString)
      {
        case 0:
          this.CreateFromBinaryForm(resultSid, 0);
          break;
        case 8:
          throw new OutOfMemoryException();
        case 1337:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"), nameof (sddlForm));
        default:
          throw new SystemException(Win32Native.GetMessage(sidFromString));
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Principal.SecurityIdentifier" /> используя указанный двоичное представление идентификатора безопасности (SID).
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтов, представляющий идентификатор SID.
    /// </param>
    /// <param name="offset">
    ///   Смещение для использования в качестве начальный индекс в байтов <paramref name="binaryForm" />.
    /// </param>
    public SecurityIdentifier(byte[] binaryForm, int offset)
    {
      this.CreateFromBinaryForm(binaryForm, offset);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Principal.SecurityIdentifier" /> используя целое число, представляющее идентификатор безопасности (SID) в двоичном виде.
    /// </summary>
    /// <param name="binaryForm">
    ///   Целое число, представляющее двоичную форму ИД безопасности.
    /// </param>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public SecurityIdentifier(IntPtr binaryForm)
      : this(binaryForm, true)
    {
    }

    [SecurityCritical]
    internal SecurityIdentifier(IntPtr binaryForm, bool noDemand)
      : this(System.Security.Principal.Win32.ConvertIntPtrSidToByteArraySid(binaryForm), 0)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Principal.SecurityIdentifier" /> класса с помощью указанного известный идентификатор типа безопасности и SID домена.
    /// </summary>
    /// <param name="sidType">
    ///   Одно из значений перечисления.
    ///    Это значение не должно быть <see cref="F:System.Security.Principal.WellKnownSidType.LogonIdsSid" />.
    /// </param>
    /// <param name="domainSid">
    ///   ИД безопасности домена.
    ///    Это значение является обязательным для следующих <see cref="T:System.Security.Principal.WellKnownSidType" /> значения.
    ///    Этот параметр учитывается для любых других <see cref="T:System.Security.Principal.WellKnownSidType" /> значения.
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountAdministratorSid" />
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountGuestSid" />
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountKrbtgtSid" />
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountDomainAdminsSid" />
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountDomainUsersSid" />
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountDomainGuestsSid" />
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountComputersSid" />
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountControllersSid" />
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountCertAdminsSid" />
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountSchemaAdminsSid" />
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountEnterpriseAdminsSid" />
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountPolicyAdminsSid" />
    /// 
    ///   - <see cref="F:System.Security.Principal.WellKnownSidType.AccountRasAndIasServersSid" />
    /// </param>
    [SecuritySafeCritical]
    public SecurityIdentifier(WellKnownSidType sidType, SecurityIdentifier domainSid)
    {
      if (sidType == WellKnownSidType.LogonIdsSid)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_CannotCreateLogonIdsSid"), nameof (sidType));
      if (!System.Security.Principal.Win32.WellKnownSidApisSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
      if (sidType < WellKnownSidType.NullSid || sidType > WellKnownSidType.WinBuiltinTerminalServerLicenseServersSid)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"), nameof (sidType));
      if (sidType >= WellKnownSidType.AccountAdministratorSid && sidType <= WellKnownSidType.AccountRasAndIasServersSid)
      {
        if (domainSid == (SecurityIdentifier) null)
          throw new ArgumentNullException(nameof (domainSid), Environment.GetResourceString("IdentityReference_DomainSidRequired", (object) sidType));
        SecurityIdentifier resultSid;
        int accountDomainSid = System.Security.Principal.Win32.GetWindowsAccountDomainSid(domainSid, out resultSid);
        switch (accountDomainSid)
        {
          case 0:
            if (resultSid != domainSid)
              throw new ArgumentException(Environment.GetResourceString("IdentityReference_NotAWindowsDomain"), nameof (domainSid));
            break;
          case 122:
            throw new OutOfMemoryException();
          case 1257:
            throw new ArgumentException(Environment.GetResourceString("IdentityReference_NotAWindowsDomain"), nameof (domainSid));
          default:
            throw new SystemException(Win32Native.GetMessage(accountDomainSid));
        }
      }
      byte[] resultSid1;
      int wellKnownSid = System.Security.Principal.Win32.CreateWellKnownSid(sidType, domainSid, out resultSid1);
      switch (wellKnownSid)
      {
        case 0:
          this.CreateFromBinaryForm(resultSid1, 0);
          break;
        case 87:
          throw new ArgumentException(Win32Native.GetMessage(wellKnownSid), "sidType/domainSid");
        default:
          throw new SystemException(Win32Native.GetMessage(wellKnownSid));
      }
    }

    internal SecurityIdentifier(SecurityIdentifier domainSid, uint rid)
    {
      int[] subAuthorities = new int[domainSid.SubAuthorityCount + 1];
      int index;
      for (index = 0; index < domainSid.SubAuthorityCount; ++index)
        subAuthorities[index] = domainSid.GetSubAuthority(index);
      subAuthorities[index] = (int) rid;
      this.CreateFromParts(domainSid.IdentifierAuthority, subAuthorities);
    }

    internal SecurityIdentifier(IdentifierAuthority identifierAuthority, int[] subAuthorities)
    {
      this.CreateFromParts(identifierAuthority, subAuthorities);
    }

    internal static byte Revision
    {
      get
      {
        return 1;
      }
    }

    internal byte[] BinaryForm
    {
      get
      {
        return this._BinaryForm;
      }
    }

    internal IdentifierAuthority IdentifierAuthority
    {
      get
      {
        return this._IdentifierAuthority;
      }
    }

    internal int SubAuthorityCount
    {
      get
      {
        return this._SubAuthorities.Length;
      }
    }

    /// <summary>
    ///   Возвращает длину в байтах, идентификатор безопасности (SID), представленного <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </summary>
    /// <returns>
    ///   Длина в байтах, SID, представленного <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </returns>
    public int BinaryLength
    {
      get
      {
        return this._BinaryForm.Length;
      }
    }

    /// <summary>
    ///   Возвращает раздел идентификатор безопасности учетной записи домена из SID, представленного <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта, если этот ИД безопасности представляет ИД безопасности учетной записи Windows.
    ///    Если идентификатор не представляет ИД безопасности учетной записи Windows, это свойство возвращает <see cref="T:System.ArgumentNullException" />.
    /// </summary>
    /// <returns>
    ///   Часть SID учетной записи домена с SID, представленного <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта, если Windows представляет идентификатор безопасности SID учетной записи; в противном случае возвращает <see cref="T:System.ArgumentNullException" />.
    /// </returns>
    public SecurityIdentifier AccountDomainSid
    {
      [SecuritySafeCritical] get
      {
        if (!this._AccountDomainSidInitialized)
        {
          this._AccountDomainSid = this.GetAccountDomainSid();
          this._AccountDomainSidInitialized = true;
        }
        return this._AccountDomainSid;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли это <see cref="T:System.Security.Principal.SecurityIdentifier" /> объект равен указанному объекту.
    /// </summary>
    /// <param name="o">
    ///   Объект для сравнения с данным <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта, или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="o" /> является объект с тем же базовым типом и значением, что <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      if (o == null)
        return false;
      SecurityIdentifier securityIdentifier = o as SecurityIdentifier;
      if (securityIdentifier == (SecurityIdentifier) null)
        return false;
      return this == securityIdentifier;
    }

    /// <summary>
    ///   Указывает, является ли указанный <see cref="T:System.Security.Principal.SecurityIdentifier" /> объект равен текущему объекту <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </summary>
    /// <param name="sid">
    ///   Объект, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="sid" /> равно значению текущего <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </returns>
    public bool Equals(SecurityIdentifier sid)
    {
      if (sid == (SecurityIdentifier) null)
        return false;
      return this == sid;
    }

    /// <summary>
    ///   Служит хэш-функцией текущего <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    ///   <see cref="M:System.Security.Principal.SecurityIdentifier.GetHashCode" /> Метод подходит для использования в алгоритмах и структурах данных подобных хэш-таблицу хеширования.
    /// </summary>
    /// <returns>
    ///   Хэш-значение для текущего <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </returns>
    public override int GetHashCode()
    {
      int hashCode = ((long) this.IdentifierAuthority).GetHashCode();
      for (int index = 0; index < this.SubAuthorityCount; ++index)
        hashCode ^= this.GetSubAuthority(index);
      return hashCode;
    }

    /// <summary>
    ///   Возвращает идентификатор безопасности (SID) в формате языка определения дескрипторов безопасности (SDDL) для учетной записи, представленной <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    ///    Пример формата SDDL — S-1-5-9.
    /// </summary>
    /// <returns>
    ///   Идентификатор безопасности на языке SDDL для учетной записи, представленной <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </returns>
    public override string ToString()
    {
      if (this._SddlForm == null)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("S-1-{0}", (object) this._IdentifierAuthority);
        for (int index = 0; index < this.SubAuthorityCount; ++index)
          stringBuilder.AppendFormat("-{0}", (object) (uint) this._SubAuthorities[index]);
        this._SddlForm = stringBuilder.ToString();
      }
      return this._SddlForm;
    }

    /// <summary>
    ///   Возвращает строку в верхнем регистре языка определения дескрипторов безопасности (SDDL) для идентификатора безопасности (SID), представленный этим <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </summary>
    /// <returns>
    ///   Строку SDDL для идентификатора безопасности, представленный в верхнем регистре <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </returns>
    public override string Value
    {
      get
      {
        return this.ToString().ToUpper(CultureInfo.InvariantCulture);
      }
    }

    internal static bool IsValidTargetTypeStatic(Type targetType)
    {
      return targetType == typeof (NTAccount) || targetType == typeof (SecurityIdentifier);
    }

    /// <summary>
    ///   Возвращает значение, которое указывает, является ли указанный тип типом допустимых эквивалентов для <see cref="T:System.Security.Principal.SecurityIdentifier" /> класса.
    /// </summary>
    /// <param name="targetType">
    ///   Запрашиваемый тип для действия в качестве преобразования из <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    ///    Допустимы следующие типы целевого объекта:
    /// 
    ///   - <see cref="T:System.Security.Principal.NTAccount" />
    /// 
    ///   - <see cref="T:System.Security.Principal.SecurityIdentifier" />
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="targetType" /> является допустимым перевода для <see cref="T:System.Security.Principal.SecurityIdentifier" /> класса; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool IsValidTargetType(Type targetType)
    {
      return SecurityIdentifier.IsValidTargetTypeStatic(targetType);
    }

    [SecurityCritical]
    internal SecurityIdentifier GetAccountDomainSid()
    {
      SecurityIdentifier resultSid;
      int accountDomainSid = System.Security.Principal.Win32.GetWindowsAccountDomainSid(this, out resultSid);
      switch (accountDomainSid)
      {
        case 0:
          return resultSid;
        case 122:
          throw new OutOfMemoryException();
        case 1257:
          resultSid = (SecurityIdentifier) null;
          goto case 0;
        default:
          throw new SystemException(Win32Native.GetMessage(accountDomainSid));
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли идентификатор безопасности (SID), представленное объектом <see cref="T:System.Security.Principal.SecurityIdentifier" /> объект является допустимым ИД безопасности учетной записи Windows.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если идентификатор безопасности, представленный этим <see cref="T:System.Security.Principal.SecurityIdentifier" /> объект является допустимым ИД безопасности учетной записи Windows; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    public bool IsAccountSid()
    {
      if (!this._AccountDomainSidInitialized)
      {
        this._AccountDomainSid = this.GetAccountDomainSid();
        this._AccountDomainSidInitialized = true;
      }
      return !(this._AccountDomainSid == (SecurityIdentifier) null);
    }

    /// <summary>
    ///   Преобразует имя учетной записи, представленной <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта в другой <see cref="T:System.Security.Principal.IdentityReference" />-производный тип.
    /// </summary>
    /// <param name="targetType">
    ///   Целевой тип для преобразования из <see cref="T:System.Security.Principal.SecurityIdentifier" />.
    ///    Целевой тип должен быть типом, которые считаются допустимыми, <see cref="M:System.Security.Principal.SecurityIdentifier.IsValidTargetType(System.Type)" /> метод.
    /// </param>
    /// <returns>Преобразованное удостоверение.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="targetType " />— <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="targetType " />не <see cref="T:System.Security.Principal.IdentityReference" /> типа.
    /// </exception>
    /// <exception cref="T:System.Security.Principal.IdentityNotMappedException">
    ///   Некоторые или ссылки на свойства нельзя преобразовать.
    /// </exception>
    /// <exception cref="T:System.SystemException">
    ///   Возвращен код ошибки Win32.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
    public override IdentityReference Translate(Type targetType)
    {
      if (targetType == (Type) null)
        throw new ArgumentNullException(nameof (targetType));
      if (targetType == typeof (SecurityIdentifier))
        return (IdentityReference) this;
      if (!(targetType == typeof (NTAccount)))
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), nameof (targetType));
      return SecurityIdentifier.Translate(new IdentityReferenceCollection(1)
      {
        (IdentityReference) this
      }, targetType, true)[0];
    }

    /// <summary>
    ///   Сравнивает два <see cref="T:System.Security.Principal.SecurityIdentifier" /> объектов, чтобы определить, равны ли они.
    ///    Они считаются равными, если они имеют одинаковое каноническое представление как возвращенного <see cref="P:System.Security.Principal.SecurityIdentifier.Value" /> свойства или если они находятся <see langword="null" />.
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
    ///   Значение <see langword="true" />, если <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator ==(SecurityIdentifier left, SecurityIdentifier right)
    {
      object obj1 = (object) left;
      object obj2 = (object) right;
      if (obj1 == null && obj2 == null)
        return true;
      if (obj1 == null || obj2 == null)
        return false;
      return left.CompareTo(right) == 0;
    }

    /// <summary>
    ///   Сравнивает два <see cref="T:System.Security.Principal.SecurityIdentifier" /> объектов, чтобы определить, не равны ли они.
    ///    Они считаются неравными, если они имеют разные канонические представления имени возвращенного <see cref="P:System.Security.Principal.SecurityIdentifier.Value" /> свойства или если один из объектов является <see langword="null" /> и другая — нет.
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
    ///   Значение <see langword="true" />, если <paramref name="left" /> и <paramref name="right" /> не равны друг другу; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator !=(SecurityIdentifier left, SecurityIdentifier right)
    {
      return !(left == right);
    }

    /// <summary>
    ///   Сравнивает текущий <see cref="T:System.Security.Principal.SecurityIdentifier" /> объект с указанным <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </summary>
    /// <param name="sid">
    ///   Объект, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    /// Знаковое число, представляющее относительные значения этого экземпляра и параметра <paramref name="sid" />.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Описание
    /// 
    ///         Меньше нуля
    /// 
    ///         Этот экземпляр меньше параметра <paramref name="sid" />.
    /// 
    ///         Нуль
    /// 
    ///         Этот экземпляр и параметр <paramref name="sid" /> равны.
    /// 
    ///         Больше нуля
    /// 
    ///         Этот экземпляр больше параметра <paramref name="sid" />.
    ///       </returns>
    public int CompareTo(SecurityIdentifier sid)
    {
      if (sid == (SecurityIdentifier) null)
        throw new ArgumentNullException(nameof (sid));
      if (this.IdentifierAuthority < sid.IdentifierAuthority)
        return -1;
      if (this.IdentifierAuthority > sid.IdentifierAuthority)
        return 1;
      if (this.SubAuthorityCount < sid.SubAuthorityCount)
        return -1;
      if (this.SubAuthorityCount > sid.SubAuthorityCount)
        return 1;
      for (int index = 0; index < this.SubAuthorityCount; ++index)
      {
        int num = this.GetSubAuthority(index) - sid.GetSubAuthority(index);
        if (num != 0)
          return num;
      }
      return 0;
    }

    internal int GetSubAuthority(int index)
    {
      return this._SubAuthorities[index];
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта совпадает с типом хорошо известных безопасности (ИД).
    /// </summary>
    /// <param name="type">
    ///   Значение для сравнения с <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="type" /> тип SID для <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    public bool IsWellKnown(WellKnownSidType type)
    {
      return System.Security.Principal.Win32.IsWellKnownSid(this, type);
    }

    /// <summary>
    ///   Копирует двоичное представление идентификатор безопасности (SID), представленного <see cref="T:System.Security.Principal.SecurityIdentifier" /> класс в массив байтов.
    /// </summary>
    /// <param name="binaryForm">
    ///   Массив байтов для копируемого ИД безопасности.
    /// </param>
    /// <param name="offset">
    ///   Смещение для использования в качестве начальный индекс в байтов <paramref name="binaryForm" />.
    /// </param>
    public void GetBinaryForm(byte[] binaryForm, int offset)
    {
      this._BinaryForm.CopyTo((Array) binaryForm, offset);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли идентификатор безопасности (SID), представленное объектом <see cref="T:System.Security.Principal.SecurityIdentifier" /> объект находится в том же домене, что указанный идентификатор SID.
    /// </summary>
    /// <param name="sid">
    ///   Идентификатор безопасности для сравнения с данным <see cref="T:System.Security.Principal.SecurityIdentifier" /> объекта.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если идентификатор безопасности, представленный этим <see cref="T:System.Security.Principal.SecurityIdentifier" /> объект находится в том же домене, что <paramref name="sid" /> SID; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    public bool IsEqualDomainSid(SecurityIdentifier sid)
    {
      return System.Security.Principal.Win32.IsEqualDomainSid(this, sid);
    }

    [SecurityCritical]
    private static IdentityReferenceCollection TranslateToNTAccounts(IdentityReferenceCollection sourceSids, out bool someFailed)
    {
      if (sourceSids == null)
        throw new ArgumentNullException(nameof (sourceSids));
      if (sourceSids.Count == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_EmptyCollection"), nameof (sourceSids));
      IntPtr[] sids = new IntPtr[sourceSids.Count];
      GCHandle[] gcHandleArray = new GCHandle[sourceSids.Count];
      SafeLsaPolicyHandle handle = SafeLsaPolicyHandle.InvalidHandle;
      SafeLsaMemoryHandle invalidHandle1 = SafeLsaMemoryHandle.InvalidHandle;
      SafeLsaMemoryHandle invalidHandle2 = SafeLsaMemoryHandle.InvalidHandle;
      try
      {
        int index1 = 0;
        foreach (IdentityReference sourceSid in sourceSids)
        {
          SecurityIdentifier securityIdentifier = sourceSid as SecurityIdentifier;
          if (securityIdentifier == (SecurityIdentifier) null)
            throw new ArgumentException(Environment.GetResourceString("Argument_ImproperType"), nameof (sourceSids));
          gcHandleArray[index1] = GCHandle.Alloc((object) securityIdentifier.BinaryForm, GCHandleType.Pinned);
          sids[index1] = gcHandleArray[index1].AddrOfPinnedObject();
          ++index1;
        }
        handle = System.Security.Principal.Win32.LsaOpenPolicy((string) null, PolicyRights.POLICY_LOOKUP_NAMES);
        someFailed = false;
        uint num = Win32Native.LsaLookupSids(handle, sourceSids.Count, sids, ref invalidHandle1, ref invalidHandle2);
        switch (num)
        {
          case 0:
            invalidHandle2.Initialize((uint) sourceSids.Count, (uint) Marshal.SizeOf(typeof (Win32Native.LSA_TRANSLATED_NAME)));
            System.Security.Principal.Win32.InitializeReferencedDomainsPointer(invalidHandle1);
            IdentityReferenceCollection referenceCollection = new IdentityReferenceCollection(sourceSids.Count);
            if (num == 0U || num == 263U)
            {
              Win32Native.LSA_REFERENCED_DOMAIN_LIST referencedDomainList = invalidHandle1.Read<Win32Native.LSA_REFERENCED_DOMAIN_LIST>(0UL);
              string[] strArray = new string[referencedDomainList.Entries];
              for (int index2 = 0; index2 < referencedDomainList.Entries; ++index2)
              {
                Win32Native.LSA_TRUST_INFORMATION structure = (Win32Native.LSA_TRUST_INFORMATION) Marshal.PtrToStructure(new IntPtr((long) referencedDomainList.Domains + (long) (index2 * Marshal.SizeOf(typeof (Win32Native.LSA_TRUST_INFORMATION)))), typeof (Win32Native.LSA_TRUST_INFORMATION));
                strArray[index2] = Marshal.PtrToStringUni(structure.Name.Buffer, (int) structure.Name.Length / 2);
              }
              Win32Native.LSA_TRANSLATED_NAME[] array = new Win32Native.LSA_TRANSLATED_NAME[sourceSids.Count];
              invalidHandle2.ReadArray<Win32Native.LSA_TRANSLATED_NAME>(0UL, array, 0, array.Length);
              for (int index2 = 0; index2 < sourceSids.Count; ++index2)
              {
                Win32Native.LSA_TRANSLATED_NAME lsaTranslatedName = array[index2];
                switch (lsaTranslatedName.Use)
                {
                  case 1:
                  case 2:
                  case 4:
                  case 5:
                  case 9:
                    string stringUni = Marshal.PtrToStringUni(lsaTranslatedName.Name.Buffer, (int) lsaTranslatedName.Name.Length / 2);
                    string domainName = strArray[lsaTranslatedName.DomainIndex];
                    referenceCollection.Add((IdentityReference) new NTAccount(domainName, stringUni));
                    break;
                  default:
                    someFailed = true;
                    referenceCollection.Add(sourceSids[index2]);
                    break;
                }
              }
            }
            else
            {
              for (int index2 = 0; index2 < sourceSids.Count; ++index2)
                referenceCollection.Add(sourceSids[index2]);
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
        for (int index = 0; index < sourceSids.Count; ++index)
        {
          if (gcHandleArray[index].IsAllocated)
            gcHandleArray[index].Free();
        }
        handle.Dispose();
        invalidHandle1.Dispose();
        invalidHandle2.Dispose();
      }
    }

    [SecurityCritical]
    internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceSids, Type targetType, bool forceSuccess)
    {
      bool someFailed = false;
      IdentityReferenceCollection referenceCollection = SecurityIdentifier.Translate(sourceSids, targetType, out someFailed);
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
    internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceSids, Type targetType, out bool someFailed)
    {
      if (sourceSids == null)
        throw new ArgumentNullException(nameof (sourceSids));
      if (targetType == typeof (NTAccount))
        return SecurityIdentifier.TranslateToNTAccounts(sourceSids, out someFailed);
      throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), nameof (targetType));
    }
  }
}
