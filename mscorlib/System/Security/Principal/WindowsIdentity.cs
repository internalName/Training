// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.WindowsIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Security.Principal
{
  /// <summary>Представляет пользователя Windows.</summary>
  [ComVisible(true)]
  [Serializable]
  public class WindowsIdentity : ClaimsIdentity, ISerializable, IDeserializationCallback, IDisposable
  {
    [SecurityCritical]
    private SafeAccessTokenHandle m_safeTokenHandle = SafeAccessTokenHandle.InvalidHandle;
    private int m_isAuthenticated = -1;
    [NonSerialized]
    private string m_issuerName = "AD AUTHORITY";
    [NonSerialized]
    private object m_claimsIntiailizedLock = new object();
    [SecurityCritical]
    private static SafeAccessTokenHandle s_invalidTokenHandle = SafeAccessTokenHandle.InvalidHandle;
    private static RuntimeConstructorInfo s_specialSerializationCtor = typeof (WindowsIdentity).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new Type[1]
    {
      typeof (SerializationInfo)
    }, (ParameterModifier[]) null) as RuntimeConstructorInfo;
    private string m_name;
    private SecurityIdentifier m_owner;
    private SecurityIdentifier m_user;
    private object m_groups;
    private string m_authType;
    private volatile TokenImpersonationLevel m_impersonationLevel;
    private volatile bool m_impersonationLevelInitialized;
    /// <summary>
    ///   Определяет имя издателя <see cref="T:System.Security.Claims.ClaimsIdentity" /> по умолчанию.
    /// </summary>
    [NonSerialized]
    public new const string DefaultIssuer = "AD AUTHORITY";
    [NonSerialized]
    private volatile bool m_claimsInitialized;
    [NonSerialized]
    private List<Claim> m_deviceClaims;
    [NonSerialized]
    private List<Claim> m_userClaims;

    [SecuritySafeCritical]
    static WindowsIdentity()
    {
    }

    [SecurityCritical]
    private WindowsIdentity()
      : base((IIdentity) null, (IEnumerable<Claim>) null, (string) null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
    {
    }

    [SecurityCritical]
    internal WindowsIdentity(SafeAccessTokenHandle safeTokenHandle)
      : this(safeTokenHandle.DangerousGetHandle(), (string) null, -1)
    {
      GC.KeepAlive((object) safeTokenHandle);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.WindowsIdentity" /> для пользователя, представленного заданным токеном учетной записи Windows.
    /// </summary>
    /// <param name="userToken">
    ///   Токен учетной записи для пользователя, от лица которого выполняется код.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="userToken" /> имеет значение 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="userToken" /> — дублирован и недопустим для олицетворения.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта нет нужных разрешений.
    /// 
    ///   -или-
    /// 
    ///   Произошла ошибка Win32.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public WindowsIdentity(IntPtr userToken)
      : this(userToken, (string) null, -1)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.WindowsIdentity" /> для пользователя, представленного заданным токеном учетной записи Windows и заданным типом проверки подлинности.
    /// </summary>
    /// <param name="userToken">
    ///   Токен учетной записи для пользователя, от лица которого выполняется код.
    /// </param>
    /// <param name="type">
    ///   (Использовать только для справки.)
    ///    Тип проверки подлинности, применяемой для идентификации пользователя.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="userToken" /> имеет значение 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="userToken" /> — дублирован и недопустим для олицетворения.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта нет нужных разрешений.
    /// 
    ///   -или-
    /// 
    ///   Произошла ошибка Win32.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public WindowsIdentity(IntPtr userToken, string type)
      : this(userToken, type, -1)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.WindowsIdentity" /> для пользователя, представленного заданными токеном учетной записи Windows, типом проверки подлинности и типом учетной записи Windows.
    /// </summary>
    /// <param name="userToken">
    ///   Токен учетной записи для пользователя, от лица которого выполняется код.
    /// </param>
    /// <param name="type">
    ///   (Использовать только для справки.)
    ///    Тип проверки подлинности, применяемой для идентификации пользователя.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </param>
    /// <param name="acctType">Одно из значений перечисления.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="userToken" /> имеет значение 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="userToken" /> — дублирован и недопустим для олицетворения.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта нет нужных разрешений.
    /// 
    ///   -или-
    /// 
    ///   Произошла ошибка Win32.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType)
      : this(userToken, type, -1)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.WindowsIdentity" /> для пользователя, представленного указанными токеном учетной записи Windows, типом проверки подлинности, типом учетной записи Windows и состоянием проверки подлинности.
    /// </summary>
    /// <param name="userToken">
    ///   Токен учетной записи для пользователя, от лица которого выполняется код.
    /// </param>
    /// <param name="type">
    ///   (Использовать только для справки.)
    ///    Тип проверки подлинности, применяемой для идентификации пользователя.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </param>
    /// <param name="acctType">Одно из значений перечисления.</param>
    /// <param name="isAuthenticated">
    ///   Значение <see langword="true" /> — пользователь прошел проверку подлинности; в противном случае — <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="userToken" /> имеет значение 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="userToken" /> — дублирован и недопустим для олицетворения.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта нет нужных разрешений.
    /// 
    ///   -или-
    /// 
    ///   Произошла ошибка Win32.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType, bool isAuthenticated)
      : this(userToken, type, isAuthenticated ? 1 : 0)
    {
    }

    [SecurityCritical]
    private WindowsIdentity(IntPtr userToken, string authType, int isAuthenticated)
      : base((IIdentity) null, (IEnumerable<Claim>) null, (string) null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
    {
      this.CreateFromToken(userToken);
      this.m_authType = authType;
      this.m_isAuthenticated = isAuthenticated;
    }

    [SecurityCritical]
    private void CreateFromToken(IntPtr userToken)
    {
      if (userToken == IntPtr.Zero)
        throw new ArgumentException(Environment.GetResourceString("Argument_TokenZero"));
      uint ReturnLength = (uint) Marshal.SizeOf(typeof (uint));
      Win32Native.GetTokenInformation(userToken, 8U, SafeLocalAllocHandle.InvalidHandle, 0U, out ReturnLength);
      if (Marshal.GetLastWin32Error() == 6)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
      if (!Win32Native.DuplicateHandle(Win32Native.GetCurrentProcess(), userToken, Win32Native.GetCurrentProcess(), ref this.m_safeTokenHandle, 0U, true, 2U))
        throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.WindowsIdentity" /> для пользователя, представленного указанным именем участника-пользователя.
    /// </summary>
    /// <param name="sUserPrincipalName">
    ///   Имя участника-пользователя, от лица которого выполняется код.
    /// </param>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Возвращенный код состояния Windows NT STATUS_ACCESS_DENIED Windows.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта нет нужных разрешений.
    /// 
    ///   -или-
    /// 
    ///   Компьютер не присоединен к Windows 2003 или более поздней версии домена.
    /// 
    ///   -или-
    /// 
    ///   Компьютер не работает под управлением Windows 2003 или более поздней версии.
    /// 
    ///   -или-
    /// 
    ///   Пользователь не является членом домена, которому присоединен компьютер.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public WindowsIdentity(string sUserPrincipalName)
      : this(sUserPrincipalName, (string) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.WindowsIdentity" /> для пользователя, представленного заданным именем участника-пользователя и типом проверки подлинности.
    /// </summary>
    /// <param name="sUserPrincipalName">
    ///   Имя участника-пользователя, от лица которого выполняется код.
    /// </param>
    /// <param name="type">
    ///   (Использовать только для справки.)
    ///    Тип проверки подлинности, применяемой для идентификации пользователя.
    ///    Дополнительные сведения см. в разделе "Замечания".
    /// </param>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Возвращенный код состояния Windows NT STATUS_ACCESS_DENIED Windows.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта нет нужных разрешений.
    /// 
    ///   -или-
    /// 
    ///   Компьютер не присоединен к Windows 2003 или более поздней версии домена.
    /// 
    ///   -или-
    /// 
    ///   Компьютер не работает под управлением Windows 2003 или более поздней версии.
    /// 
    ///   -или-
    /// 
    ///   Пользователь не является членом домена, которому присоединен компьютер.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public WindowsIdentity(string sUserPrincipalName, string type)
      : base((IIdentity) null, (IEnumerable<Claim>) null, (string) null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
    {
      WindowsIdentity.KerbS4ULogon(sUserPrincipalName, ref this.m_safeTokenHandle);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.WindowsIdentity" /> для пользователя, представленного данными в потоке <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сведения учетной записи пользователя.
    /// </param>
    /// <param name="context">
    ///   Объект, указывающий характеристики потока.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Объект <see cref="T:System.Security.Principal.WindowsIdentity" /> не может быть сериализован в процессах.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта нет нужных разрешений.
    /// 
    ///   -или-
    /// 
    ///   Произошла ошибка Win32.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public WindowsIdentity(SerializationInfo info, StreamingContext context)
      : this(info)
    {
    }

    [SecurityCritical]
    private WindowsIdentity(SerializationInfo info)
      : base(info)
    {
      this.m_claimsInitialized = false;
      IntPtr userToken = (IntPtr) info.GetValue("m_userToken", typeof (IntPtr));
      if (!(userToken != IntPtr.Zero))
        return;
      this.CreateFromToken(userToken);
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      this.GetObjectData(info, context);
      info.AddValue("m_userToken", (object) this.m_safeTokenHandle.DangerousGetHandle());
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Security.Principal.WindowsIdentity" />, представляющий текущего пользователя Windows.
    /// </summary>
    /// <returns>Объект, представляющий текущего пользователя.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта нет нужных разрешений.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public static WindowsIdentity GetCurrent()
    {
      return WindowsIdentity.GetCurrentInternal(TokenAccessLevels.MaximumAllowed, false);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Security.Principal.WindowsIdentity" />, представляющий идентификатор Windows для потока или процесса в зависимости от значения параметра <paramref name="ifImpersonating" />.
    /// </summary>
    /// <param name="ifImpersonating">
    ///   Значение <see langword="true" /> для возврата объекта <see cref="T:System.Security.Principal.WindowsIdentity" />, только если олицетворение потока выполняется в данный момент; значение <see langword="false" /> для возврата объекта <see cref="T:System.Security.Principal.WindowsIdentity" /> потока, если олицетворение потока выполняется, или объекта <see cref="T:System.Security.Principal.WindowsIdentity" /> процесса, если олицетворение потока в настоящий момент не выполняется.
    /// </param>
    /// <returns>Объект, представляющий пользователя Windows.</returns>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public static WindowsIdentity GetCurrent(bool ifImpersonating)
    {
      return WindowsIdentity.GetCurrentInternal(TokenAccessLevels.MaximumAllowed, ifImpersonating);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Security.Principal.WindowsIdentity" />, представляющий текущего пользователя Windows, используя указанный требуемый уровень доступа к токену.
    /// </summary>
    /// <param name="desiredAccess">
    ///   Побитовое сочетание значений перечисления.
    /// </param>
    /// <returns>Объект, представляющий текущего пользователя.</returns>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public static WindowsIdentity GetCurrent(TokenAccessLevels desiredAccess)
    {
      return WindowsIdentity.GetCurrentInternal(desiredAccess, false);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Security.Principal.WindowsIdentity" />, который можно использовать в качестве контрольного значения в коде, чтобы представлять анонимного пользователя.
    ///    Значение свойства не представляет встроенный анонимный идентификатор, используемый операционной системой Windows.
    /// </summary>
    /// <returns>Объект, представляющий анонимного пользователя.</returns>
    [SecuritySafeCritical]
    public static WindowsIdentity GetAnonymous()
    {
      return new WindowsIdentity();
    }

    /// <summary>
    ///   Получает тип аутентификации, используемой для идентификации пользователя.
    /// </summary>
    /// <returns>
    ///   Тип проверки подлинности, применяемой для идентификации пользователя.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Возвращенный код состояния Windows NT STATUS_ACCESS_DENIED Windows.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта нет нужных разрешений.
    /// 
    ///   -или-
    /// 
    ///   Компьютер не присоединен к Windows 2003 или более поздней версии домена.
    /// 
    ///   -или-
    /// 
    ///   Компьютер не работает под управлением Windows 2003 или более поздней версии.
    /// 
    ///   -или-
    /// 
    ///   Пользователь не является членом домена, которому присоединен компьютер.
    /// </exception>
    public override sealed string AuthenticationType
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return string.Empty;
        if (this.m_authType != null)
          return this.m_authType;
        Win32Native.LUID logonAuthId = WindowsIdentity.GetLogonAuthId(this.m_safeTokenHandle);
        if (logonAuthId.LowPart == 998U)
          return string.Empty;
        SafeLsaReturnBufferHandle invalidHandle = SafeLsaReturnBufferHandle.InvalidHandle;
        try
        {
          int logonSessionData = Win32Native.LsaGetLogonSessionData(ref logonAuthId, ref invalidHandle);
          if (logonSessionData < 0)
            throw WindowsIdentity.GetExceptionFromNtStatus(logonSessionData);
          invalidHandle.Initialize((ulong) (uint) Marshal.SizeOf(typeof (Win32Native.SECURITY_LOGON_SESSION_DATA)));
          return Marshal.PtrToStringUni(invalidHandle.Read<Win32Native.SECURITY_LOGON_SESSION_DATA>(0UL).AuthenticationPackage.Buffer);
        }
        finally
        {
          if (!invalidHandle.IsInvalid)
            invalidHandle.Dispose();
        }
      }
    }

    /// <summary>Возвращает уровень олицетворения для пользователя.</summary>
    /// <returns>
    ///   Одно из значений перечисления, указывающее уровень олицетворения.
    /// </returns>
    [ComVisible(false)]
    public TokenImpersonationLevel ImpersonationLevel
    {
      [SecuritySafeCritical] get
      {
        if (!this.m_impersonationLevelInitialized)
        {
          this.m_impersonationLevel = !this.m_safeTokenHandle.IsInvalid ? (this.GetTokenInformation<int>(TokenInformationClass.TokenType) != 1 ? (TokenImpersonationLevel) (this.GetTokenInformation<int>(TokenInformationClass.TokenImpersonationLevel) + 1) : TokenImpersonationLevel.None) : TokenImpersonationLevel.Anonymous;
          this.m_impersonationLevelInitialized = true;
        }
        return this.m_impersonationLevel;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, прошел ли пользователь проверку подлинности в Windows.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если пользователь прошел проверку подлинности; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsAuthenticated
    {
      get
      {
        if (this.m_isAuthenticated == -1)
          this.m_isAuthenticated = this.CheckNtTokenForSid(new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[1]
          {
            11
          })) ? 1 : 0;
        return this.m_isAuthenticated == 1;
      }
    }

    [SecuritySafeCritical]
    [ComVisible(false)]
    private bool CheckNtTokenForSid(SecurityIdentifier sid)
    {
      if (this.m_safeTokenHandle.IsInvalid)
        return false;
      SafeAccessTokenHandle invalidHandle = SafeAccessTokenHandle.InvalidHandle;
      TokenImpersonationLevel impersonationLevel = this.ImpersonationLevel;
      bool IsMember = false;
      try
      {
        if (impersonationLevel == TokenImpersonationLevel.None && !Win32Native.DuplicateTokenEx(this.m_safeTokenHandle, 8U, IntPtr.Zero, 2U, 2U, ref invalidHandle))
          throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
        if (!Win32Native.CheckTokenMembership(impersonationLevel != TokenImpersonationLevel.None ? this.m_safeTokenHandle : invalidHandle, sid.BinaryForm, ref IsMember))
          throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
      }
      finally
      {
        if (invalidHandle != SafeAccessTokenHandle.InvalidHandle)
          invalidHandle.Dispose();
      }
      return IsMember;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, определена ли в системе учетная запись пользователя как учетная запись <see cref="F:System.Security.Principal.WindowsAccountType.Guest" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если учетная запись пользователя является учетной записью <see cref="F:System.Security.Principal.WindowsAccountType.Guest" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool IsGuest
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return false;
        return this.CheckNtTokenForSid(new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[2]
        {
          32,
          546
        }));
      }
    }

    /// <summary>
    ///   Возвращает значение, показывающее, определена ли в системе учетная запись пользователя как учетная запись <see cref="F:System.Security.Principal.WindowsAccountType.System" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если учетная запись пользователя является учетной записью <see cref="F:System.Security.Principal.WindowsAccountType.System" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool IsSystem
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return false;
        return this.User == new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[1]
        {
          18
        });
      }
    }

    /// <summary>
    ///   Возвращает значение, показывающее, определена ли в системе учетная запись пользователя как анонимная.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если учетная запись пользователя является анонимной; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool IsAnonymous
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return true;
        return this.User == new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[1]
        {
          7
        });
      }
    }

    /// <summary>Возвращает имя входа пользователя Windows.</summary>
    /// <returns>
    ///   Имя входа пользователя Windows, от лица которого запущен код.
    /// </returns>
    public override string Name
    {
      [SecuritySafeCritical] get
      {
        return this.GetName();
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal string GetName()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (this.m_safeTokenHandle.IsInvalid)
        return string.Empty;
      if (this.m_name == null)
      {
        using (WindowsIdentity.SafeRevertToSelf(ref stackMark))
          this.m_name = (this.User.Translate(typeof (NTAccount)) as NTAccount).ToString();
      }
      return this.m_name;
    }

    /// <summary>
    ///   Возвращает идентификатор безопасности (ИД безопасности) для владельца токена.
    /// </summary>
    /// <returns>Объект для владельца токена.</returns>
    [ComVisible(false)]
    public SecurityIdentifier Owner
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return (SecurityIdentifier) null;
        if (this.m_owner == (SecurityIdentifier) null)
        {
          using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenOwner))
            this.m_owner = new SecurityIdentifier(tokenInformation.Read<IntPtr>(0UL), true);
        }
        return this.m_owner;
      }
    }

    /// <summary>
    ///   Возвращает идентификатор безопасности (ИД безопасности) для пользователя.
    /// </summary>
    /// <returns>Объект для пользователя.</returns>
    [ComVisible(false)]
    public SecurityIdentifier User
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return (SecurityIdentifier) null;
        if (this.m_user == (SecurityIdentifier) null)
        {
          using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenUser))
            this.m_user = new SecurityIdentifier(tokenInformation.Read<IntPtr>(0UL), true);
        }
        return this.m_user;
      }
    }

    /// <summary>
    ///   Возвращает группы, к которым относится текущий пользователь Windows.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий группы, к которым принадлежит текущий пользователь Windows.
    /// </returns>
    public IdentityReferenceCollection Groups
    {
      [SecuritySafeCritical] get
      {
        if (this.m_safeTokenHandle.IsInvalid)
          return (IdentityReferenceCollection) null;
        if (this.m_groups == null)
        {
          IdentityReferenceCollection referenceCollection = new IdentityReferenceCollection();
          using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenGroups))
          {
            if (tokenInformation.Read<uint>(0UL) != 0U)
            {
              Win32Native.SID_AND_ATTRIBUTES[] array = new Win32Native.SID_AND_ATTRIBUTES[(int) tokenInformation.Read<Win32Native.TOKEN_GROUPS>(0UL).GroupCount];
              tokenInformation.ReadArray<Win32Native.SID_AND_ATTRIBUTES>((ulong) (uint) Marshal.OffsetOf(typeof (Win32Native.TOKEN_GROUPS), nameof (Groups)).ToInt32(), array, 0, array.Length);
              foreach (Win32Native.SID_AND_ATTRIBUTES sidAndAttributes in array)
              {
                uint num = 3221225492;
                if (((int) sidAndAttributes.Attributes & (int) num) == 4)
                  referenceCollection.Add((IdentityReference) new SecurityIdentifier(sidAndAttributes.Sid, true));
              }
            }
          }
          Interlocked.CompareExchange(ref this.m_groups, (object) referenceCollection, (object) null);
        }
        return this.m_groups as IdentityReferenceCollection;
      }
    }

    /// <summary>
    ///   Возвращает токен учетной записи Windows для пользователя.
    /// </summary>
    /// <returns>
    ///   Дескриптор токена доступа, связанный с текущим выполняемым потоком.
    /// </returns>
    public virtual IntPtr Token
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.m_safeTokenHandle.DangerousGetHandle();
      }
    }

    /// <summary>
    ///   Выполняет указанное действие с олицетворенным удостоверением Windows.
    ///    Вместо олицетворенного вызова метода и выполнения функции в контексте <see cref="T:System.Security.Principal.WindowsImpersonationContext" /> можно использовать метод <see cref="M:System.Security.Principal.WindowsIdentity.RunImpersonated(Microsoft.Win32.SafeHandles.SafeAccessTokenHandle,System.Action)" /> и предоставить функцию непосредственно в качестве параметра.
    /// </summary>
    /// <param name="safeAccessTokenHandle">
    ///   Дескриптор SafeAccessTokenHandle олицетворенного удостоверения Windows.
    /// </param>
    /// <param name="action">System.Action для запуска.</param>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RunImpersonated(SafeAccessTokenHandle safeAccessTokenHandle, Action action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      WindowsIdentity wi = (WindowsIdentity) null;
      if (!safeAccessTokenHandle.IsInvalid)
        wi = new WindowsIdentity(safeAccessTokenHandle);
      using (WindowsIdentity.SafeImpersonate(safeAccessTokenHandle, wi, ref stackMark))
        action();
    }

    /// <summary>
    ///   Выполняет указанную функцию с олицетворенным удостоверением Windows.
    ///    Вместо олицетворенного вызова метода и выполнения функции в контексте <see cref="T:System.Security.Principal.WindowsImpersonationContext" /> можно использовать метод <see cref="M:System.Security.Principal.WindowsIdentity.RunImpersonated(Microsoft.Win32.SafeHandles.SafeAccessTokenHandle,System.Action)" /> и предоставить функцию непосредственно в качестве параметра.
    /// </summary>
    /// <param name="safeAccessTokenHandle">
    ///   Дескриптор SafeAccessTokenHandle олицетворенного удостоверения Windows.
    /// </param>
    /// <param name="func">System.Func для запуска.</param>
    /// <typeparam name="T">
    ///   Тип объекта, который используется и возвращается функцией.
    /// </typeparam>
    /// <returns>Возвращает результат функции.</returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T RunImpersonated<T>(SafeAccessTokenHandle safeAccessTokenHandle, Func<T> func)
    {
      if (func == null)
        throw new ArgumentNullException(nameof (func));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      WindowsIdentity wi = (WindowsIdentity) null;
      if (!safeAccessTokenHandle.IsInvalid)
        wi = new WindowsIdentity(safeAccessTokenHandle);
      T obj = default (T);
      using (WindowsIdentity.SafeImpersonate(safeAccessTokenHandle, wi, ref stackMark))
        return func();
    }

    /// <summary>
    ///   Олицетворяет пользователя, представленного объектом <see cref="T:System.Security.Principal.WindowsIdentity" />.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий пользователя Windows до олицетворения; может использоваться для возврата к исходному контексту пользователя.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Предпринята попытка выполнить олицетворение Анонимное удостоверение.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Произошла ошибка Win32.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public virtual WindowsImpersonationContext Impersonate()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.Impersonate(ref stackMark);
    }

    /// <summary>
    ///   Олицетворяет пользователя, представленного заданным токеном.
    /// </summary>
    /// <param name="userToken">
    ///   Дескриптор токена учетной записи Windows.
    ///    Этот токен обычно извлекается посредством вызова неуправляемого кода, например, вызова функции <see langword="LogonUser" /> API Win32.
    /// </param>
    /// <returns>
    ///   Объект, представляющий пользователя Windows до олицетворения; может использоваться для возврата к исходному контексту пользователя.
    /// </returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   Возвращенный код состояния Windows NT STATUS_ACCESS_DENIED Windows.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Не хватает памяти.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта нет нужных разрешений.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlPrincipal)]
    public static WindowsImpersonationContext Impersonate(IntPtr userToken)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (userToken == IntPtr.Zero)
        return WindowsIdentity.SafeRevertToSelf(ref stackMark);
      return new WindowsIdentity(userToken, (string) null, -1).Impersonate(ref stackMark);
    }

    [SecurityCritical]
    internal WindowsImpersonationContext Impersonate(ref StackCrawlMark stackMark)
    {
      if (this.m_safeTokenHandle.IsInvalid)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AnonymousCannotImpersonate"));
      return WindowsIdentity.SafeImpersonate(this.m_safeTokenHandle, this, ref stackMark);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.Security.Principal.WindowsIdentity" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [SecuritySafeCritical]
    [ComVisible(false)]
    protected virtual void Dispose(bool disposing)
    {
      if (disposing && this.m_safeTokenHandle != null && !this.m_safeTokenHandle.IsClosed)
        this.m_safeTokenHandle.Dispose();
      this.m_name = (string) null;
      this.m_owner = (SecurityIdentifier) null;
      this.m_user = (SecurityIdentifier) null;
    }

    /// <summary>
    ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.Security.Principal.WindowsIdentity" />.
    /// </summary>
    [ComVisible(false)]
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" /> для данного экземпляра <see cref="T:System.Security.Principal.WindowsIdentity" />.
    /// </summary>
    /// <returns>
    ///   Возвращает значение типа <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" />.
    /// </returns>
    public SafeAccessTokenHandle AccessToken
    {
      [SecurityCritical] get
      {
        return this.m_safeTokenHandle;
      }
    }

    [SecurityCritical]
    internal static WindowsImpersonationContext SafeRevertToSelf(ref StackCrawlMark stackMark)
    {
      return WindowsIdentity.SafeImpersonate(WindowsIdentity.s_invalidTokenHandle, (WindowsIdentity) null, ref stackMark);
    }

    [SecurityCritical]
    internal static WindowsImpersonationContext SafeImpersonate(SafeAccessTokenHandle userToken, WindowsIdentity wi, ref StackCrawlMark stackMark)
    {
      int hr = 0;
      bool isImpersonating;
      SafeAccessTokenHandle currentToken = WindowsIdentity.GetCurrentToken(TokenAccessLevels.MaximumAllowed, false, out isImpersonating, out hr);
      if (currentToken == null || currentToken.IsInvalid)
        throw new SecurityException(Win32Native.GetMessage(hr));
      FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
      if (securityObjectForFrame == null)
        throw new SecurityException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
      WindowsImpersonationContext impersonationContext = new WindowsImpersonationContext(currentToken, WindowsIdentity.GetCurrentThreadWI(), isImpersonating, securityObjectForFrame);
      if (userToken.IsInvalid)
      {
        int self = System.Security.Principal.Win32.RevertToSelf();
        if (self < 0)
          Environment.FailFast(Win32Native.GetMessage(self));
        WindowsIdentity.UpdateThreadWI(wi);
        securityObjectForFrame.SetTokenHandles(currentToken, wi == null ? (SafeAccessTokenHandle) null : wi.AccessToken);
      }
      else
      {
        int self = System.Security.Principal.Win32.RevertToSelf();
        if (self < 0)
          Environment.FailFast(Win32Native.GetMessage(self));
        if (System.Security.Principal.Win32.ImpersonateLoggedOnUser(userToken) < 0)
        {
          impersonationContext.Undo();
          throw new SecurityException(Environment.GetResourceString("Argument_ImpersonateUser"));
        }
        WindowsIdentity.UpdateThreadWI(wi);
        securityObjectForFrame.SetTokenHandles(currentToken, wi == null ? (SafeAccessTokenHandle) null : wi.AccessToken);
      }
      return impersonationContext;
    }

    [SecurityCritical]
    internal static WindowsIdentity GetCurrentThreadWI()
    {
      return SecurityContext.GetCurrentWI(Thread.CurrentThread.GetExecutionContextReader());
    }

    [SecurityCritical]
    internal static void UpdateThreadWI(WindowsIdentity wi)
    {
      Thread currentThread = Thread.CurrentThread;
      if (currentThread.GetExecutionContextReader().SecurityContext.WindowsIdentity == wi)
        return;
      ExecutionContext executionContext = currentThread.GetMutableExecutionContext();
      SecurityContext securityContext = executionContext.SecurityContext;
      if (wi != null && securityContext == null)
      {
        securityContext = new SecurityContext();
        executionContext.SecurityContext = securityContext;
      }
      if (securityContext == null)
        return;
      securityContext.WindowsIdentity = wi;
    }

    [SecurityCritical]
    internal static WindowsIdentity GetCurrentInternal(TokenAccessLevels desiredAccess, bool threadOnly)
    {
      int hr = 0;
      bool isImpersonating;
      SafeAccessTokenHandle currentToken = WindowsIdentity.GetCurrentToken(desiredAccess, threadOnly, out isImpersonating, out hr);
      if (currentToken == null || currentToken.IsInvalid)
      {
        if (threadOnly && !isImpersonating)
          return (WindowsIdentity) null;
        throw new SecurityException(Win32Native.GetMessage(hr));
      }
      WindowsIdentity windowsIdentity = new WindowsIdentity();
      windowsIdentity.m_safeTokenHandle.Dispose();
      windowsIdentity.m_safeTokenHandle = currentToken;
      return windowsIdentity;
    }

    internal static RuntimeConstructorInfo GetSpecialSerializationCtor()
    {
      return WindowsIdentity.s_specialSerializationCtor;
    }

    private static int GetHRForWin32Error(int dwLastError)
    {
      if (((long) dwLastError & 2147483648L) == 2147483648L)
        return dwLastError;
      return dwLastError & (int) ushort.MaxValue | -2147024896;
    }

    [SecurityCritical]
    private static Exception GetExceptionFromNtStatus(int status)
    {
      switch (status)
      {
        case -1073741801:
        case -1073741670:
          return (Exception) new OutOfMemoryException();
        case -1073741790:
          return (Exception) new UnauthorizedAccessException();
        default:
          return (Exception) new SecurityException(Win32Native.GetMessage(Win32Native.LsaNtStatusToWinError(status)));
      }
    }

    [SecurityCritical]
    private static SafeAccessTokenHandle GetCurrentToken(TokenAccessLevels desiredAccess, bool threadOnly, out bool isImpersonating, out int hr)
    {
      isImpersonating = true;
      SafeAccessTokenHandle accessTokenHandle = WindowsIdentity.GetCurrentThreadToken(desiredAccess, out hr);
      if (accessTokenHandle == null && hr == WindowsIdentity.GetHRForWin32Error(1008))
      {
        isImpersonating = false;
        if (!threadOnly)
          accessTokenHandle = WindowsIdentity.GetCurrentProcessToken(desiredAccess, out hr);
      }
      return accessTokenHandle;
    }

    [SecurityCritical]
    private static SafeAccessTokenHandle GetCurrentProcessToken(TokenAccessLevels desiredAccess, out int hr)
    {
      hr = 0;
      SafeAccessTokenHandle TokenHandle;
      if (!Win32Native.OpenProcessToken(Win32Native.GetCurrentProcess(), desiredAccess, out TokenHandle))
        hr = WindowsIdentity.GetHRForWin32Error(Marshal.GetLastWin32Error());
      return TokenHandle;
    }

    [SecurityCritical]
    internal static SafeAccessTokenHandle GetCurrentThreadToken(TokenAccessLevels desiredAccess, out int hr)
    {
      SafeAccessTokenHandle phThreadToken;
      hr = System.Security.Principal.Win32.OpenThreadToken(desiredAccess, WinSecurityContext.Both, out phThreadToken);
      return phThreadToken;
    }

    [SecurityCritical]
    private T GetTokenInformation<T>(TokenInformationClass tokenInformationClass) where T : struct
    {
      using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, tokenInformationClass))
        return tokenInformation.Read<T>(0UL);
    }

    [SecurityCritical]
    internal static ImpersonationQueryResult QueryImpersonation()
    {
      SafeAccessTokenHandle phThreadToken = (SafeAccessTokenHandle) null;
      int num = System.Security.Principal.Win32.OpenThreadToken(TokenAccessLevels.Query, WinSecurityContext.Thread, out phThreadToken);
      if (phThreadToken != null)
      {
        phThreadToken.Close();
        return ImpersonationQueryResult.Impersonated;
      }
      if (num == WindowsIdentity.GetHRForWin32Error(5))
        return ImpersonationQueryResult.Impersonated;
      return num == WindowsIdentity.GetHRForWin32Error(1008) ? ImpersonationQueryResult.NotImpersonated : ImpersonationQueryResult.Failed;
    }

    [SecurityCritical]
    private static Win32Native.LUID GetLogonAuthId(SafeAccessTokenHandle safeTokenHandle)
    {
      using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(safeTokenHandle, TokenInformationClass.TokenStatistics))
        return tokenInformation.Read<Win32Native.TOKEN_STATISTICS>(0UL).AuthenticationId;
    }

    [SecurityCritical]
    private static SafeLocalAllocHandle GetTokenInformation(SafeAccessTokenHandle tokenHandle, TokenInformationClass tokenInformationClass)
    {
      SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
      uint ReturnLength = (uint) Marshal.SizeOf(typeof (uint));
      Win32Native.GetTokenInformation(tokenHandle, (uint) tokenInformationClass, invalidHandle, 0U, out ReturnLength);
      int lastWin32Error = Marshal.GetLastWin32Error();
      switch (lastWin32Error)
      {
        case 6:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
        case 24:
        case 122:
          UIntPtr sizetdwBytes = new UIntPtr(ReturnLength);
          invalidHandle.Dispose();
          SafeLocalAllocHandle TokenInformation = Win32Native.LocalAlloc(0, sizetdwBytes);
          if (TokenInformation == null || TokenInformation.IsInvalid)
            throw new OutOfMemoryException();
          TokenInformation.Initialize((ulong) ReturnLength);
          if (!Win32Native.GetTokenInformation(tokenHandle, (uint) tokenInformationClass, TokenInformation, ReturnLength, out ReturnLength))
            throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
          return TokenInformation;
        default:
          throw new SecurityException(Win32Native.GetMessage(lastWin32Error));
      }
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    private static unsafe SafeAccessTokenHandle KerbS4ULogon(string upn, ref SafeAccessTokenHandle safeTokenHandle)
    {
      byte[] array = new byte[3]
      {
        (byte) 67,
        (byte) 76,
        (byte) 82
      };
      using (SafeLocalAllocHandle buffer1 = Win32Native.LocalAlloc(64, new UIntPtr((uint) (array.Length + 1))))
      {
        if (buffer1 == null || buffer1.IsInvalid)
          throw new OutOfMemoryException();
        buffer1.Initialize((ulong) array.Length + 1UL);
        buffer1.WriteArray<byte>(0UL, array, 0, array.Length);
        Win32Native.UNICODE_INTPTR_STRING unicodeIntptrString = new Win32Native.UNICODE_INTPTR_STRING(array.Length, buffer1);
        SafeLsaLogonProcessHandle invalidHandle1 = SafeLsaLogonProcessHandle.InvalidHandle;
        SafeLsaReturnBufferHandle invalidHandle2 = SafeLsaReturnBufferHandle.InvalidHandle;
        try
        {
          Privilege privilege = (Privilege) null;
          RuntimeHelpers.PrepareConstrainedRegions();
          int status1;
          try
          {
            try
            {
              privilege = new Privilege("SeTcbPrivilege");
              privilege.Enable();
            }
            catch (PrivilegeNotHeldException ex)
            {
            }
            IntPtr zero = IntPtr.Zero;
            status1 = Win32Native.LsaRegisterLogonProcess(ref unicodeIntptrString, ref invalidHandle1, ref zero);
            if (5 == Win32Native.LsaNtStatusToWinError(status1))
              status1 = Win32Native.LsaConnectUntrusted(ref invalidHandle1);
          }
          catch
          {
            privilege?.Revert();
            throw;
          }
          finally
          {
            privilege?.Revert();
          }
          if (status1 < 0)
            throw WindowsIdentity.GetExceptionFromNtStatus(status1);
          byte[] numArray = new byte["Kerberos".Length + 1];
          Encoding.ASCII.GetBytes("Kerberos", 0, "Kerberos".Length, numArray, 0);
          using (SafeLocalAllocHandle buffer2 = Win32Native.LocalAlloc(0, new UIntPtr((uint) numArray.Length)))
          {
            if (buffer2 == null || buffer2.IsInvalid)
              throw new OutOfMemoryException();
            buffer2.Initialize((ulong) (uint) numArray.Length);
            buffer2.WriteArray<byte>(0UL, numArray, 0, numArray.Length);
            Win32Native.UNICODE_INTPTR_STRING PackageName = new Win32Native.UNICODE_INTPTR_STRING("Kerberos".Length, buffer2);
            uint AuthenticationPackage = 0;
            int status2 = Win32Native.LsaLookupAuthenticationPackage(invalidHandle1, ref PackageName, ref AuthenticationPackage);
            if (status2 < 0)
              throw WindowsIdentity.GetExceptionFromNtStatus(status2);
            Win32Native.TOKEN_SOURCE SourceContext = new Win32Native.TOKEN_SOURCE();
            if (!Win32Native.AllocateLocallyUniqueId(ref SourceContext.SourceIdentifier))
              throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
            SourceContext.Name = new char[8];
            SourceContext.Name[0] = 'C';
            SourceContext.Name[1] = 'L';
            SourceContext.Name[2] = 'R';
            uint ProfileBufferLength = 0;
            Win32Native.LUID LogonId = new Win32Native.LUID();
            Win32Native.QUOTA_LIMITS Quotas = new Win32Native.QUOTA_LIMITS();
            int SubStatus = 0;
            byte[] bytes = Encoding.Unicode.GetBytes(upn);
            uint num = (uint) (Marshal.SizeOf(typeof (Win32Native.KERB_S4U_LOGON)) + bytes.Length);
            using (SafeLocalAllocHandle localAllocHandle = Win32Native.LocalAlloc(64, new UIntPtr(num)))
            {
              if (localAllocHandle == null || localAllocHandle.IsInvalid)
                throw new OutOfMemoryException();
              localAllocHandle.Initialize((ulong) num);
              ulong byteOffset = (ulong) Marshal.SizeOf(typeof (Win32Native.KERB_S4U_LOGON));
              localAllocHandle.WriteArray<byte>(byteOffset, bytes, 0, bytes.Length);
              byte* pointer = (byte*) null;
              RuntimeHelpers.PrepareConstrainedRegions();
              try
              {
                localAllocHandle.AcquirePointer(ref pointer);
                localAllocHandle.Write<Win32Native.KERB_S4U_LOGON>(0UL, new Win32Native.KERB_S4U_LOGON()
                {
                  MessageType = 12U,
                  Flags = 0U,
                  ClientUpn = new Win32Native.UNICODE_INTPTR_STRING(bytes.Length, new IntPtr((void*) (pointer + byteOffset)))
                });
                int status3 = Win32Native.LsaLogonUser(invalidHandle1, ref unicodeIntptrString, 3U, AuthenticationPackage, new IntPtr((void*) pointer), (uint) localAllocHandle.ByteLength, IntPtr.Zero, ref SourceContext, ref invalidHandle2, ref ProfileBufferLength, ref LogonId, ref safeTokenHandle, ref Quotas, ref SubStatus);
                if (status3 == -1073741714 && SubStatus < 0)
                  status3 = SubStatus;
                if (status3 < 0)
                  throw WindowsIdentity.GetExceptionFromNtStatus(status3);
                if (SubStatus < 0)
                  throw WindowsIdentity.GetExceptionFromNtStatus(SubStatus);
              }
              finally
              {
                if ((IntPtr) pointer != IntPtr.Zero)
                  localAllocHandle.ReleasePointer();
              }
            }
            return safeTokenHandle;
          }
        }
        finally
        {
          if (!invalidHandle1.IsInvalid)
            invalidHandle1.Dispose();
          if (!invalidHandle2.IsInvalid)
            invalidHandle2.Dispose();
        }
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.WindowsIdentity" />, используя указанный объект <see cref="T:System.Security.Principal.WindowsIdentity" />.
    /// </summary>
    /// <param name="identity">
    ///   Объект, из которого создается новый экземпляр <see cref="T:System.Security.Principal.WindowsIdentity" />.
    /// </param>
    [SecuritySafeCritical]
    protected WindowsIdentity(WindowsIdentity identity)
      : base((IIdentity) identity, (IEnumerable<Claim>) null, identity.m_authType, (string) null, (string) null, false)
    {
      if (identity == null)
        throw new ArgumentNullException(nameof (identity));
      bool success = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        if (identity.m_safeTokenHandle.IsInvalid || identity.m_safeTokenHandle == SafeAccessTokenHandle.InvalidHandle || !(identity.m_safeTokenHandle.DangerousGetHandle() != IntPtr.Zero))
          return;
        identity.m_safeTokenHandle.DangerousAddRef(ref success);
        if (!identity.m_safeTokenHandle.IsInvalid && identity.m_safeTokenHandle.DangerousGetHandle() != IntPtr.Zero)
          this.CreateFromToken(identity.m_safeTokenHandle.DangerousGetHandle());
        this.m_authType = identity.m_authType;
        this.m_isAuthenticated = identity.m_isAuthenticated;
      }
      finally
      {
        if (success)
          identity.m_safeTokenHandle.DangerousRelease();
      }
    }

    [SecurityCritical]
    internal IntPtr GetTokenInternal()
    {
      return this.m_safeTokenHandle.DangerousGetHandle();
    }

    [SecurityCritical]
    internal WindowsIdentity(ClaimsIdentity claimsIdentity, IntPtr userToken)
      : base(claimsIdentity)
    {
      if (!(userToken != IntPtr.Zero) || userToken.ToInt64() <= 0L)
        return;
      this.CreateFromToken(userToken);
    }

    internal ClaimsIdentity CloneAsBase()
    {
      return base.Clone();
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>Копия текущего экземпляра.</returns>
    public override ClaimsIdentity Clone()
    {
      return (ClaimsIdentity) new WindowsIdentity(this);
    }

    /// <summary>
    ///   Возвращает утверждения, имеющие ключ свойства <see cref="F:System.Security.Claims.ClaimTypes.WindowsUserClaim" />.
    /// </summary>
    /// <returns>
    ///   Коллекция утверждений, имеющих ключ свойства <see cref="F:System.Security.Claims.ClaimTypes.WindowsUserClaim" />.
    /// </returns>
    public virtual IEnumerable<Claim> UserClaims
    {
      get
      {
        this.InitializeClaims();
        return (IEnumerable<Claim>) this.m_userClaims.AsReadOnly();
      }
    }

    /// <summary>
    ///   Возвращает утверждения, имеющие ключ свойства <see cref="F:System.Security.Claims.ClaimTypes.WindowsDeviceClaim" />.
    /// </summary>
    /// <returns>
    ///   Коллекция утверждений, имеющих ключ свойства <see cref="F:System.Security.Claims.ClaimTypes.WindowsDeviceClaim" />.
    /// </returns>
    public virtual IEnumerable<Claim> DeviceClaims
    {
      get
      {
        this.InitializeClaims();
        return (IEnumerable<Claim>) this.m_deviceClaims.AsReadOnly();
      }
    }

    /// <summary>
    ///   Возвращает все утверждения для пользователя, представленного этим идентификатором Windows.
    /// </summary>
    /// <returns>
    ///   Коллекция утверждений для этого объекта <see cref="T:System.Security.Principal.WindowsIdentity" />.
    /// </returns>
    public override IEnumerable<Claim> Claims
    {
      get
      {
        if (!this.m_claimsInitialized)
          this.InitializeClaims();
        foreach (Claim claim in base.Claims)
          yield return claim;
        foreach (Claim userClaim in this.m_userClaims)
          yield return userClaim;
        foreach (Claim deviceClaim in this.m_deviceClaims)
          yield return deviceClaim;
      }
    }

    [SecuritySafeCritical]
    private void InitializeClaims()
    {
      if (this.m_claimsInitialized)
        return;
      lock (this.m_claimsIntiailizedLock)
      {
        if (this.m_claimsInitialized)
          return;
        this.m_userClaims = new List<Claim>();
        this.m_deviceClaims = new List<Claim>();
        if (!string.IsNullOrEmpty(this.Name))
          this.m_userClaims.Add(new Claim(this.NameClaimType, this.Name, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this));
        this.AddPrimarySidClaim(this.m_userClaims);
        this.AddGroupSidClaims(this.m_userClaims);
        if (Environment.IsWindows8OrAbove)
        {
          this.AddDeviceGroupSidClaims(this.m_deviceClaims, TokenInformationClass.TokenDeviceGroups);
          this.AddTokenClaims(this.m_userClaims, TokenInformationClass.TokenUserClaimAttributes, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim");
          this.AddTokenClaims(this.m_deviceClaims, TokenInformationClass.TokenDeviceClaimAttributes, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim");
        }
        this.m_claimsInitialized = true;
      }
    }

    [SecurityCritical]
    private void AddDeviceGroupSidClaims(List<Claim> instanceClaims, TokenInformationClass tokenInformationClass)
    {
      if (this.m_safeTokenHandle.IsInvalid)
        return;
      SafeLocalAllocHandle localAllocHandle = SafeLocalAllocHandle.InvalidHandle;
      try
      {
        localAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, tokenInformationClass);
        int num1 = Marshal.ReadInt32(localAllocHandle.DangerousGetHandle());
        IntPtr ptr = new IntPtr((long) localAllocHandle.DangerousGetHandle() + (long) Marshal.OffsetOf(typeof (Win32Native.TOKEN_GROUPS), "Groups"));
        for (int index = 0; index < num1; ++index)
        {
          Win32Native.SID_AND_ATTRIBUTES structure = (Win32Native.SID_AND_ATTRIBUTES) Marshal.PtrToStructure(ptr, typeof (Win32Native.SID_AND_ATTRIBUTES));
          uint num2 = 3221225492;
          SecurityIdentifier securityIdentifier = new SecurityIdentifier(structure.Sid, true);
          if (((int) structure.Attributes & (int) num2) == 4)
          {
            string type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup";
            instanceClaims.Add(new Claim(type, securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture))
            {
              Properties = {
                {
                  type,
                  ""
                }
              }
            });
          }
          else if (((int) structure.Attributes & (int) num2) == 16)
          {
            string type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup";
            instanceClaims.Add(new Claim(type, securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture))
            {
              Properties = {
                {
                  type,
                  ""
                }
              }
            });
          }
          ptr = new IntPtr((long) ptr + Win32Native.SID_AND_ATTRIBUTES.SizeOf);
        }
      }
      finally
      {
        localAllocHandle.Close();
      }
    }

    [SecurityCritical]
    private void AddGroupSidClaims(List<Claim> instanceClaims)
    {
      if (this.m_safeTokenHandle.IsInvalid)
        return;
      SafeLocalAllocHandle localAllocHandle1 = SafeLocalAllocHandle.InvalidHandle;
      SafeLocalAllocHandle localAllocHandle2 = SafeLocalAllocHandle.InvalidHandle;
      try
      {
        localAllocHandle2 = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenPrimaryGroup);
        SecurityIdentifier securityIdentifier1 = new SecurityIdentifier(((Win32Native.TOKEN_PRIMARY_GROUP) Marshal.PtrToStructure(localAllocHandle2.DangerousGetHandle(), typeof (Win32Native.TOKEN_PRIMARY_GROUP))).PrimaryGroup, true);
        bool flag = false;
        localAllocHandle1 = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenGroups);
        int num1 = Marshal.ReadInt32(localAllocHandle1.DangerousGetHandle());
        IntPtr ptr = new IntPtr((long) localAllocHandle1.DangerousGetHandle() + (long) Marshal.OffsetOf(typeof (Win32Native.TOKEN_GROUPS), "Groups"));
        for (int index = 0; index < num1; ++index)
        {
          Win32Native.SID_AND_ATTRIBUTES structure = (Win32Native.SID_AND_ATTRIBUTES) Marshal.PtrToStructure(ptr, typeof (Win32Native.SID_AND_ATTRIBUTES));
          uint num2 = 3221225492;
          SecurityIdentifier securityIdentifier2 = new SecurityIdentifier(structure.Sid, true);
          if (((int) structure.Attributes & (int) num2) == 4)
          {
            if (!flag && StringComparer.Ordinal.Equals(securityIdentifier2.Value, securityIdentifier1.Value))
            {
              instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier2.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture)));
              flag = true;
            }
            instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier2.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture)));
          }
          else if (((int) structure.Attributes & (int) num2) == 16)
          {
            if (!flag && StringComparer.Ordinal.Equals(securityIdentifier2.Value, securityIdentifier1.Value))
            {
              instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier2.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture)));
              flag = true;
            }
            instanceClaims.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier2.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture)));
          }
          ptr = new IntPtr((long) ptr + Win32Native.SID_AND_ATTRIBUTES.SizeOf);
        }
      }
      finally
      {
        localAllocHandle1.Close();
        localAllocHandle2.Close();
      }
    }

    [SecurityCritical]
    private void AddPrimarySidClaim(List<Claim> instanceClaims)
    {
      if (this.m_safeTokenHandle.IsInvalid)
        return;
      SafeLocalAllocHandle localAllocHandle = SafeLocalAllocHandle.InvalidHandle;
      try
      {
        localAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenUser);
        Win32Native.SID_AND_ATTRIBUTES structure = (Win32Native.SID_AND_ATTRIBUTES) Marshal.PtrToStructure(localAllocHandle.DangerousGetHandle(), typeof (Win32Native.SID_AND_ATTRIBUTES));
        uint num = 16;
        SecurityIdentifier securityIdentifier = new SecurityIdentifier(structure.Sid, true);
        if (structure.Attributes == 0U)
        {
          instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid", securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture)));
        }
        else
        {
          if (((int) structure.Attributes & (int) num) != 16)
            return;
          instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid", securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString((object) securityIdentifier.IdentifierAuthority, (IFormatProvider) CultureInfo.InvariantCulture)));
        }
      }
      finally
      {
        localAllocHandle.Close();
      }
    }

    [SecurityCritical]
    private void AddTokenClaims(List<Claim> instanceClaims, TokenInformationClass tokenInformationClass, string propertyValue)
    {
      if (this.m_safeTokenHandle.IsInvalid)
        return;
      SafeLocalAllocHandle localAllocHandle = SafeLocalAllocHandle.InvalidHandle;
      try
      {
        SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
        localAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, tokenInformationClass);
        Win32Native.CLAIM_SECURITY_ATTRIBUTES_INFORMATION structure1 = (Win32Native.CLAIM_SECURITY_ATTRIBUTES_INFORMATION) Marshal.PtrToStructure(localAllocHandle.DangerousGetHandle(), typeof (Win32Native.CLAIM_SECURITY_ATTRIBUTES_INFORMATION));
        long num = 0;
        for (int index1 = 0; (long) index1 < (long) structure1.AttributeCount; ++index1)
        {
          Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1 structure2 = (Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1) Marshal.PtrToStructure(new IntPtr(structure1.Attribute.pAttributeV1.ToInt64() + num), typeof (Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1));
          switch (structure2.ValueType)
          {
            case 1:
              long[] destination1 = new long[(int) structure2.ValueCount];
              Marshal.Copy(structure2.Values.pInt64, destination1, 0, (int) structure2.ValueCount);
              for (int index2 = 0; (long) index2 < (long) structure2.ValueCount; ++index2)
                instanceClaims.Add(new Claim(structure2.Name, Convert.ToString(destination1[index2], (IFormatProvider) CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#integer64", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, propertyValue, string.Empty));
              break;
            case 2:
              long[] destination2 = new long[(int) structure2.ValueCount];
              Marshal.Copy(structure2.Values.pUint64, destination2, 0, (int) structure2.ValueCount);
              for (int index2 = 0; (long) index2 < (long) structure2.ValueCount; ++index2)
                instanceClaims.Add(new Claim(structure2.Name, Convert.ToString((ulong) destination2[index2], (IFormatProvider) CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#uinteger64", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, propertyValue, string.Empty));
              break;
            case 3:
              IntPtr[] destination3 = new IntPtr[(int) structure2.ValueCount];
              Marshal.Copy(structure2.Values.ppString, destination3, 0, (int) structure2.ValueCount);
              for (int index2 = 0; (long) index2 < (long) structure2.ValueCount; ++index2)
                instanceClaims.Add(new Claim(structure2.Name, Marshal.PtrToStringAuto(destination3[index2]), "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, propertyValue, string.Empty));
              break;
            case 6:
              long[] destination4 = new long[(int) structure2.ValueCount];
              Marshal.Copy(structure2.Values.pUint64, destination4, 0, (int) structure2.ValueCount);
              for (int index2 = 0; (long) index2 < (long) structure2.ValueCount; ++index2)
                instanceClaims.Add(new Claim(structure2.Name, destination4[index2] == 0L ? Convert.ToString(false, (IFormatProvider) CultureInfo.InvariantCulture) : Convert.ToString(true, (IFormatProvider) CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#boolean", this.m_issuerName, this.m_issuerName, (ClaimsIdentity) this, propertyValue, string.Empty));
              break;
          }
          num += (long) Marshal.SizeOf<Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1>(structure2);
        }
      }
      finally
      {
        localAllocHandle.Close();
      }
    }
  }
}
