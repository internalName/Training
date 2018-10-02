// Decompiled with JetBrains decompiler
// Type: System.Security.Claims.ClaimTypes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Claims
{
  /// <summary>
  ///   Определяет константы для хорошо известных типов утверждений, которые могут быть назначены субъекту.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(false)]
  public static class ClaimTypes
  {
    internal const string ClaimTypeNamespace = "http://schemas.microsoft.com/ws/2008/06/identity/claims";
    /// <summary>
    ///   URI для утверждения с указанием момент, когда сущность была выполнена проверка подлинности; http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/authenticationinstant.
    /// </summary>
    public const string AuthenticationInstant = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant";
    /// <summary>
    ///   URI для утверждения, которое задает метод, с помощью которого сущность была выполнена проверка подлинности; http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/authenticationmethod.
    /// </summary>
    public const string AuthenticationMethod = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod";
    /// <summary>
    ///   URI для утверждения с указанием пути cookie; http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/cookiePath.
    /// </summary>
    public const string CookiePath = "http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath";
    /// <summary>
    ///   URI для утверждения с указанием только основной SID для сущности; http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/denyonlyprimarysid.
    ///    SID "только запрет" запрещает указанному утверждению доступ к защищаемому объекту.
    /// </summary>
    public const string DenyOnlyPrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid";
    /// <summary>
    ///   URI для утверждения с указанием только SID основной группы для сущности; http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/denyonlyprimarygroupsid.
    ///    SID "только запрет" запрещает указанному утверждению доступ к защищаемому объекту.
    /// </summary>
    public const string DenyOnlyPrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid";
    /// <summary>
    ///   http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/denyonlywindowsdevicegroup.
    /// </summary>
    public const string DenyOnlyWindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup";
    /// <summary>
    ///   http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/DSA.
    /// </summary>
    public const string Dsa = "http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa";
    /// <summary>
    ///   http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/expiration.
    /// </summary>
    public const string Expiration = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration";
    /// <summary>
    ///   http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/EXPIRED.
    /// </summary>
    public const string Expired = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expired";
    /// <summary>
    ///   URI для утверждения с указанием SID группы по сущности, http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid.
    /// </summary>
    public const string GroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid";
    /// <summary>
    ///   http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/isPersistent.
    /// </summary>
    public const string IsPersistent = "http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent";
    /// <summary>
    ///   URI для утверждения с указанием основной ИД безопасности группы сущности, http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid.
    /// </summary>
    public const string PrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid";
    /// <summary>
    ///   URI для утверждения с ИД безопасности основной сущности, http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid.
    /// </summary>
    public const string PrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid";
    /// <summary>
    ///   URI для утверждения с указанием роль сущности, http://schemas.microsoft.com/ws/2008/06/identity/claims/role.
    /// </summary>
    public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    /// <summary>
    ///   URI для утверждения с указанием последовательный номер, http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber.
    /// </summary>
    public const string SerialNumber = "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber";
    /// <summary>
    ///   http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/UserData.
    /// </summary>
    public const string UserData = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata";
    /// <summary>
    ///   http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/Version.
    /// </summary>
    public const string Version = "http://schemas.microsoft.com/ws/2008/06/identity/claims/version";
    /// <summary>
    ///   URI для утверждения с указанием сущности, имя учетной записи домена Windows http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname.
    /// </summary>
    public const string WindowsAccountName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname";
    /// <summary>
    ///   http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/windowsdeviceclaim.
    /// </summary>
    public const string WindowsDeviceClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim";
    /// <summary>
    ///   http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/windowsdevicegroup.
    /// </summary>
    public const string WindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup";
    /// <summary>
    ///   http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/windowsuserclaim.
    /// </summary>
    public const string WindowsUserClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim";
    /// <summary>
    ///   http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/windowsfqbnversion.
    /// </summary>
    public const string WindowsFqbnVersion = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsfqbnversion";
    /// <summary>
    ///   http://schemas.Microsoft.com/ws/2008/06/IDENTITY/claims/windowssubauthority.
    /// </summary>
    public const string WindowsSubAuthority = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority";
    internal const string ClaimType2005Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
    /// <summary>
    ///   URI для утверждения с указанием анонимного пользователя; http://schemas.xmlsoap.org/ws/2005/05/IDENTITY/claims/Anonymous.
    /// </summary>
    public const string Anonymous = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous";
    /// <summary>
    ///   URI для утверждения с указанием о подлинность, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authenticated.
    /// </summary>
    public const string Authentication = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication";
    /// <summary>
    ///   URI для утверждения с указанием принятия решения об авторизации сущности; http://schemas.xmlsoap.org/ws/2005/05/IDENTITY/claims/authorizationdecision.
    /// </summary>
    public const string AuthorizationDecision = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision";
    /// <summary>
    ///   URI для утверждения с указанием страны или региона, в котором находится сущность, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country.
    /// </summary>
    public const string Country = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country";
    /// <summary>
    ///   URI для утверждения с указанием даты рождения сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth.
    /// </summary>
    public const string DateOfBirth = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth";
    /// <summary>
    ///   URI для утверждения с указанием DNS-имя, связанное с именем компьютера или с альтернативным именем субъекта или издателя сертификата X.509, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns.
    /// </summary>
    public const string Dns = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns";
    /// <summary>
    ///   URI для утверждения с указанием идентификатора только запрет безопасности (SID) для сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid.
    ///    SID "только запрет" запрещает указанному утверждению доступ к защищаемому объекту.
    /// </summary>
    public const string DenyOnlySid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid";
    /// <summary>
    ///   URI для утверждения с указанием адреса электронной почты сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/email.
    /// </summary>
    public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    /// <summary>
    ///   URI для утверждения с указанием пола сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender.
    /// </summary>
    public const string Gender = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender";
    /// <summary>
    ///   URI для утверждения с указанием имени сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname.
    /// </summary>
    public const string GivenName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
    /// <summary>
    ///   URI для утверждения с указанием значения хэша, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash.
    /// </summary>
    public const string Hash = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash";
    /// <summary>
    ///   URI для утверждения с указанием номера домашнего телефона сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone.
    /// </summary>
    public const string HomePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone";
    /// <summary>
    ///   URI для утверждения с указанием языкового стандарта, в котором находится сущность, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality.
    /// </summary>
    public const string Locality = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality";
    /// <summary>
    ///   URI для утверждения с указанием номера мобильного телефона сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone.
    /// </summary>
    public const string MobilePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";
    /// <summary>
    ///   URI для утверждения с указанием имени сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name.
    /// </summary>
    public const string Name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    /// <summary>
    ///   URI для утверждения с указанием имени сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier.
    /// </summary>
    public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
    /// <summary>
    ///   URI для утверждения с указанием альтернативного номера телефона сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone.
    /// </summary>
    public const string OtherPhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone";
    /// <summary>
    ///   URI для утверждения с указанием почтового индекса сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode.
    /// </summary>
    public const string PostalCode = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode";
    /// <summary>
    ///   URI для утверждения с указанием http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa ключ RSA.
    /// </summary>
    public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";
    /// <summary>
    ///   URI для утверждения с указанием идентификатора безопасности (SID), http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid.
    /// </summary>
    public const string Sid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid";
    /// <summary>
    ///   URI для утверждения с указанием имени участника-службы (SPN) утверждений http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn.
    /// </summary>
    public const string Spn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn";
    /// <summary>
    ///   URI для утверждения с указанием штата или провинции, где находится сущность, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince.
    /// </summary>
    public const string StateOrProvince = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince";
    /// <summary>
    ///   URI для утверждения с указанием почтового адреса сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress.
    /// </summary>
    public const string StreetAddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress";
    /// <summary>
    ///   URI для утверждения с указанием фамилии сущности, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname.
    /// </summary>
    public const string Surname = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
    /// <summary>
    ///   URI для утверждения с указанием системной сущности http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system.
    /// </summary>
    public const string System = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system";
    /// <summary>
    ///   URI для утверждения с указанием отпечатка пальца, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint.
    ///    Отпечаток — это глобальный уникальный хэш SHA-1 сертификата X.509.
    /// </summary>
    public const string Thumbprint = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint";
    /// <summary>
    ///   URI для утверждения с указанием имени участника-пользователя (UPN), http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn.
    /// </summary>
    public const string Upn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";
    /// <summary>
    ///   URI для утверждения с указанием URI, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri.
    /// </summary>
    public const string Uri = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri";
    /// <summary>
    ///   URI для утверждения, которое указывает веб-страницу сущности: http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage.
    /// </summary>
    public const string Webpage = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage";
    /// <summary>
    ///   URI для утверждения с различающимся именем сертификата X.509, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname.
    ///    В стандарте X.500 определена методика определения различающихся имен, которые используются сертификаты X.509.
    /// </summary>
    public const string X500DistinguishedName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname";
    internal const string ClaimType2009Namespace = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims";
    /// <summary>
    ///   http://schemas.xmlsoap.org/ws/2009/09/IDENTITY/claims/Actor.
    /// </summary>
    public const string Actor = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor";
  }
}
