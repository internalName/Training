// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security
{
  /// <summary>
  ///   Исключение, которое возникает при обнаружении ошибки безопасности.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class SecurityException : SystemException
  {
    private string m_debugString;
    private SecurityAction m_action;
    [NonSerialized]
    private Type m_typeOfPermissionThatFailed;
    private string m_permissionThatFailed;
    private string m_demanded;
    private string m_granted;
    private string m_refused;
    private string m_denied;
    private string m_permitOnly;
    private AssemblyName m_assemblyName;
    private byte[] m_serializedMethodInfo;
    private string m_strMethodInfo;
    private SecurityZone m_zone;
    private string m_url;
    private const string ActionName = "Action";
    private const string FirstPermissionThatFailedName = "FirstPermissionThatFailed";
    private const string DemandedName = "Demanded";
    private const string GrantedSetName = "GrantedSet";
    private const string RefusedSetName = "RefusedSet";
    private const string DeniedName = "Denied";
    private const string PermitOnlyName = "PermitOnly";
    private const string Assembly_Name = "Assembly";
    private const string MethodName_Serialized = "Method";
    private const string MethodName_String = "Method_String";
    private const string ZoneName = "Zone";
    private const string UrlName = "Url";

    [SecuritySafeCritical]
    internal static string GetResString(string sResourceName)
    {
      PermissionSet.s_fullTrust.Assert();
      return Environment.GetResourceString(sResourceName);
    }

    [SecurityCritical]
    internal static Exception MakeSecurityException(AssemblyName asmName, Evidence asmEvidence, PermissionSet granted, PermissionSet refused, RuntimeMethodHandleInternal rmh, SecurityAction action, object demand, IPermission permThatFailed)
    {
      HostProtectionPermission protectionPermission = permThatFailed as HostProtectionPermission;
      if (protectionPermission != null)
        return (Exception) new HostProtectionException(SecurityException.GetResString("HostProtection_HostProtection"), HostProtectionPermission.protectedResources, protectionPermission.Resources);
      string message = "";
      MethodInfo method = (MethodInfo) null;
      try
      {
        message = granted != null || refused != null || demand != null ? (demand == null || !(demand is IPermission) ? (permThatFailed == null ? SecurityException.GetResString("Security_GenericNoType") : string.Format((IFormatProvider) CultureInfo.InvariantCulture, SecurityException.GetResString("Security_Generic"), (object) permThatFailed.GetType().AssemblyQualifiedName)) : string.Format((IFormatProvider) CultureInfo.InvariantCulture, SecurityException.GetResString("Security_Generic"), (object) demand.GetType().AssemblyQualifiedName)) : SecurityException.GetResString("Security_NoAPTCA");
        method = SecurityRuntime.GetMethodInfo(rmh);
      }
      catch (Exception ex)
      {
        if (ex is ThreadAbortException)
          throw;
      }
      return (Exception) new SecurityException(message, asmName, granted, refused, method, action, demand, permThatFailed, asmEvidence);
    }

    private static byte[] ObjectToByteArray(object obj)
    {
      if (obj == null)
        return (byte[]) null;
      MemoryStream memoryStream = new MemoryStream();
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      try
      {
        binaryFormatter.Serialize((Stream) memoryStream, obj);
        return memoryStream.ToArray();
      }
      catch (NotSupportedException ex)
      {
        return (byte[]) null;
      }
    }

    private static object ByteArrayToObject(byte[] array)
    {
      if (array == null || array.Length == 0)
        return (object) null;
      return new BinaryFormatter().Deserialize((Stream) new MemoryStream(array));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecurityException" /> стандартными свойствами.
    /// </summary>
    [__DynamicallyInvokable]
    public SecurityException()
      : base(SecurityException.GetResString("Arg_SecurityException"))
    {
      this.SetErrorCode(-2146233078);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecurityException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    [__DynamicallyInvokable]
    public SecurityException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233078);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecurityException" /> с указанным сообщением об ошибке и типом разрешения, которое стало причиной текущего исключения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="type">Тип разрешения, вызвавшего исключение.</param>
    [SecuritySafeCritical]
    public SecurityException(string message, Type type)
      : base(message)
    {
      PermissionSet.s_fullTrust.Assert();
      this.SetErrorCode(-2146233078);
      this.m_typeOfPermissionThatFailed = type;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecurityException" /> с указанным сообщением об ошибке, типом разрешения, которое стало причиной текущего исключения, и состоянием разрешения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="type">Тип разрешения, вызвавшего исключение.</param>
    /// <param name="state">
    ///   Состояние разрешения, вызвавшего исключение.
    /// </param>
    [SecuritySafeCritical]
    public SecurityException(string message, Type type, string state)
      : base(message)
    {
      PermissionSet.s_fullTrust.Assert();
      this.SetErrorCode(-2146233078);
      this.m_typeOfPermissionThatFailed = type;
      this.m_demanded = state;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecurityException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public SecurityException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233078);
    }

    [SecurityCritical]
    internal SecurityException(PermissionSet grantedSetObj, PermissionSet refusedSetObj)
      : base(SecurityException.GetResString("Arg_SecurityException"))
    {
      PermissionSet.s_fullTrust.Assert();
      this.SetErrorCode(-2146233078);
      if (grantedSetObj != null)
        this.m_granted = grantedSetObj.ToXml().ToString();
      if (refusedSetObj == null)
        return;
      this.m_refused = refusedSetObj.ToXml().ToString();
    }

    [SecurityCritical]
    internal SecurityException(string message, PermissionSet grantedSetObj, PermissionSet refusedSetObj)
      : base(message)
    {
      PermissionSet.s_fullTrust.Assert();
      this.SetErrorCode(-2146233078);
      if (grantedSetObj != null)
        this.m_granted = grantedSetObj.ToXml().ToString();
      if (refusedSetObj == null)
        return;
      this.m_refused = refusedSetObj.ToXml().ToString();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecurityException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="info " /> имеет значение <see langword=" null" />.
    /// </exception>
    [SecuritySafeCritical]
    protected SecurityException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      try
      {
        this.m_action = (SecurityAction) info.GetValue(nameof (Action), typeof (SecurityAction));
        this.m_permissionThatFailed = (string) info.GetValueNoThrow(nameof (FirstPermissionThatFailed), typeof (string));
        this.m_demanded = (string) info.GetValueNoThrow(nameof (Demanded), typeof (string));
        this.m_granted = (string) info.GetValueNoThrow(nameof (GrantedSet), typeof (string));
        this.m_refused = (string) info.GetValueNoThrow(nameof (RefusedSet), typeof (string));
        this.m_denied = (string) info.GetValueNoThrow("Denied", typeof (string));
        this.m_permitOnly = (string) info.GetValueNoThrow("PermitOnly", typeof (string));
        this.m_assemblyName = (AssemblyName) info.GetValueNoThrow("Assembly", typeof (AssemblyName));
        this.m_serializedMethodInfo = (byte[]) info.GetValueNoThrow(nameof (Method), typeof (byte[]));
        this.m_strMethodInfo = (string) info.GetValueNoThrow("Method_String", typeof (string));
        this.m_zone = (SecurityZone) info.GetValue(nameof (Zone), typeof (SecurityZone));
        this.m_url = (string) info.GetValueNoThrow(nameof (Url), typeof (string));
      }
      catch
      {
        this.m_action = (SecurityAction) 0;
        this.m_permissionThatFailed = "";
        this.m_demanded = "";
        this.m_granted = "";
        this.m_refused = "";
        this.m_denied = "";
        this.m_permitOnly = "";
        this.m_assemblyName = (AssemblyName) null;
        this.m_serializedMethodInfo = (byte[]) null;
        this.m_strMethodInfo = (string) null;
        this.m_zone = SecurityZone.NoZone;
        this.m_url = "";
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecurityException" /> для исключения, вызванного недостаточным набором прав.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="assemblyName">
    ///   Объект <see cref="T:System.Reflection.AssemblyName" />, указывающий зону сборки, вызвавшую исключение.
    /// </param>
    /// <param name="grant">
    ///   Объект <see cref="T:System.Security.PermissionSet" />, представляющий разрешения, предоставленные сборке.
    /// </param>
    /// <param name="refused">
    ///   Объект <see cref="T:System.Security.PermissionSet" />, представляющий отклоненные разрешения или набор разрешений.
    /// </param>
    /// <param name="method">
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, представляющий метод, который обнаружил исключение.
    /// </param>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    /// <param name="demanded">
    ///   Затребованное разрешение, набор разрешений или коллекция наборов разрешений.
    /// </param>
    /// <param name="permThatFailed">
    ///   Объект <see cref="T:System.Security.IPermission" />, представляющий разрешение, которое вызвало сбой.
    /// </param>
    /// <param name="evidence">
    ///   <see cref="T:System.Security.Policy.Evidence" /> для сборки, вызвавшей исключение.
    /// </param>
    [SecuritySafeCritical]
    public SecurityException(string message, AssemblyName assemblyName, PermissionSet grant, PermissionSet refused, MethodInfo method, SecurityAction action, object demanded, IPermission permThatFailed, Evidence evidence)
      : base(message)
    {
      PermissionSet.s_fullTrust.Assert();
      this.SetErrorCode(-2146233078);
      this.Action = action;
      if (permThatFailed != null)
        this.m_typeOfPermissionThatFailed = permThatFailed.GetType();
      this.FirstPermissionThatFailed = permThatFailed;
      this.Demanded = demanded;
      this.m_granted = grant == null ? "" : grant.ToXml().ToString();
      this.m_refused = refused == null ? "" : refused.ToXml().ToString();
      this.m_denied = "";
      this.m_permitOnly = "";
      this.m_assemblyName = assemblyName;
      this.Method = method;
      this.m_url = "";
      this.m_zone = SecurityZone.NoZone;
      if (evidence != null)
      {
        System.Security.Policy.Url hostEvidence1 = evidence.GetHostEvidence<System.Security.Policy.Url>();
        if (hostEvidence1 != null)
          this.m_url = hostEvidence1.GetURLString().ToString();
        System.Security.Policy.Zone hostEvidence2 = evidence.GetHostEvidence<System.Security.Policy.Zone>();
        if (hostEvidence2 != null)
          this.m_zone = hostEvidence2.SecurityZone;
      }
      this.m_debugString = this.ToString(true, false);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.SecurityException" /> для исключения, вызванного отказом в стеке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="deny">
    ///   Отклоненное разрешение или набор разрешений.
    /// </param>
    /// <param name="permitOnly">
    ///   Разрешение "только на разрешение" или набор разрешений.
    /// </param>
    /// <param name="method">
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, определяющий метод, который обнаружил исключение.
    /// </param>
    /// <param name="demanded">
    ///   Затребованное разрешение, набор разрешений или коллекция наборов разрешений.
    /// </param>
    /// <param name="permThatFailed">
    ///   <see cref="T:System.Security.IPermission" />, определяющий разрешение, которое вызвало сбой.
    /// </param>
    [SecuritySafeCritical]
    public SecurityException(string message, object deny, object permitOnly, MethodInfo method, object demanded, IPermission permThatFailed)
      : base(message)
    {
      PermissionSet.s_fullTrust.Assert();
      this.SetErrorCode(-2146233078);
      this.Action = SecurityAction.Demand;
      if (permThatFailed != null)
        this.m_typeOfPermissionThatFailed = permThatFailed.GetType();
      this.FirstPermissionThatFailed = permThatFailed;
      this.Demanded = demanded;
      this.m_granted = "";
      this.m_refused = "";
      this.DenySetInstance = deny;
      this.PermitOnlySetInstance = permitOnly;
      this.m_assemblyName = (AssemblyName) null;
      this.Method = method;
      this.m_zone = SecurityZone.NoZone;
      this.m_url = "";
      this.m_debugString = this.ToString(true, false);
    }

    /// <summary>
    ///   Возвращает или задает действие по обеспечению безопасности, вызвавшее исключение.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </returns>
    [ComVisible(false)]
    public SecurityAction Action
    {
      get
      {
        return this.m_action;
      }
      set
      {
        this.m_action = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает тип разрешения, вызвавшего сбой.
    /// </summary>
    /// <returns>Тип разрешения, вызвавшего сбой.</returns>
    public Type PermissionType
    {
      [SecuritySafeCritical] get
      {
        if (this.m_typeOfPermissionThatFailed == (Type) null)
        {
          object obj = XMLUtil.XmlStringToSecurityObject(this.m_permissionThatFailed) ?? XMLUtil.XmlStringToSecurityObject(this.m_demanded);
          if (obj != null)
            this.m_typeOfPermissionThatFailed = obj.GetType();
        }
        return this.m_typeOfPermissionThatFailed;
      }
      set
      {
        this.m_typeOfPermissionThatFailed = value;
      }
    }

    /// <summary>
    ///   Получает или задает первое разрешение в наборе разрешений или коллекции наборов разрешений, вызвавшее сбой требования.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.IPermission" />, представляющий первое разрешение, которое завершилось ошибкой.
    /// </returns>
    public IPermission FirstPermissionThatFailed
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return (IPermission) XMLUtil.XmlStringToSecurityObject(this.m_permissionThatFailed);
      }
      set
      {
        this.m_permissionThatFailed = XMLUtil.SecurityObjectToXmlString((object) value);
      }
    }

    /// <summary>
    ///   Возвращает или задает состояние разрешения, вызывающее исключение.
    /// </summary>
    /// <returns>
    ///   Состояние разрешения на момент возникновения этого исключения.
    /// </returns>
    public string PermissionState
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this.m_demanded;
      }
      set
      {
        this.m_demanded = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает затребованное разрешение безопасности, набор разрешений или коллекцию наборов разрешений, вызвавших сбой.
    /// </summary>
    /// <returns>
    ///   Разрешение, набор разрешений или объект коллекции наборов разрешений.
    /// </returns>
    [ComVisible(false)]
    public object Demanded
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return XMLUtil.XmlStringToSecurityObject(this.m_demanded);
      }
      set
      {
        this.m_demanded = XMLUtil.SecurityObjectToXmlString(value);
      }
    }

    /// <summary>
    ///   Получает или задает набор предоставленных разрешений сборки, вызвавшей <see cref="T:System.Security.SecurityException" />.
    /// </summary>
    /// <returns>
    ///   XML-представление набора предоставленных разрешений для сборки.
    /// </returns>
    public string GrantedSet
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this.m_granted;
      }
      set
      {
        this.m_granted = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает отклоненный набор разрешений сборки, вызвавшей <see cref="T:System.Security.SecurityException" />.
    /// </summary>
    /// <returns>
    ///   XML-представление отклоненного набора разрешений сборки.
    /// </returns>
    public string RefusedSet
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this.m_refused;
      }
      set
      {
        this.m_refused = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает отклоненные разрешения, наборы разрешений или коллекции наборов разрешений, ставшие причиной несоблюдения требований безопасности.
    /// </summary>
    /// <returns>
    ///   Разрешение, набор разрешений или объект коллекции наборов разрешений.
    /// </returns>
    [ComVisible(false)]
    public object DenySetInstance
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return XMLUtil.XmlStringToSecurityObject(this.m_denied);
      }
      set
      {
        this.m_denied = XMLUtil.SecurityObjectToXmlString(value);
      }
    }

    /// <summary>
    ///   Получает или задает разрешение, набор разрешений или коллекцию наборов разрешений, которые являются частью кадра стека ограничения разрешений PermitOnly, вызвавшего сбой проверки безопасности.
    /// </summary>
    /// <returns>
    ///   Разрешение, набор разрешений или объект коллекции наборов разрешений.
    /// </returns>
    [ComVisible(false)]
    public object PermitOnlySetInstance
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return XMLUtil.XmlStringToSecurityObject(this.m_permitOnly);
      }
      set
      {
        this.m_permitOnly = XMLUtil.SecurityObjectToXmlString(value);
      }
    }

    /// <summary>
    ///   Получает или задает сведения о сборке, вызвавшей сбой.
    /// </summary>
    /// <returns>
    ///   Имя <see cref="T:System.Reflection.AssemblyName" />, определяющее сборку, вызвавшую сбой.
    /// </returns>
    [ComVisible(false)]
    public AssemblyName FailedAssemblyInfo
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this.m_assemblyName;
      }
      set
      {
        this.m_assemblyName = value;
      }
    }

    private MethodInfo getMethod()
    {
      return (MethodInfo) SecurityException.ByteArrayToObject(this.m_serializedMethodInfo);
    }

    /// <summary>
    ///   Возвращает или задает сведения о методе, связанном с исключением.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, описывающий метод.
    /// </returns>
    [ComVisible(false)]
    public MethodInfo Method
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this.getMethod();
      }
      set
      {
        RuntimeMethodInfo runtimeMethodInfo = value as RuntimeMethodInfo;
        this.m_serializedMethodInfo = SecurityException.ObjectToByteArray((object) runtimeMethodInfo);
        if (!((MethodInfo) runtimeMethodInfo != (MethodInfo) null))
          return;
        this.m_strMethodInfo = runtimeMethodInfo.ToString();
      }
    }

    /// <summary>
    ///   Получает или задает зону сборки, вызвавшую исключение.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.SecurityZone" />, которое определяет зону сборки, вызвавшую исключение.
    /// </returns>
    public SecurityZone Zone
    {
      get
      {
        return this.m_zone;
      }
      set
      {
        this.m_zone = value;
      }
    }

    /// <summary>
    ///   Получает или задает задает URL-адрес сборки, вызвавшей исключение.
    /// </summary>
    /// <returns>URL-адрес, определяющий расположение сборки.</returns>
    public string Url
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this.m_url;
      }
      set
      {
        this.m_url = value;
      }
    }

    private void ToStringHelper(StringBuilder sb, string resourceString, object attr)
    {
      if (attr == null)
        return;
      string str = attr as string ?? attr.ToString();
      if (str.Length == 0)
        return;
      sb.Append(Environment.NewLine);
      sb.Append(SecurityException.GetResString(resourceString));
      sb.Append(Environment.NewLine);
      sb.Append(str);
    }

    [SecurityCritical]
    private string ToString(bool includeSensitiveInfo, bool includeBaseInfo)
    {
      PermissionSet.s_fullTrust.Assert();
      StringBuilder sb = new StringBuilder();
      if (includeBaseInfo)
        sb.Append(base.ToString());
      if (this.Action > (SecurityAction) 0)
        this.ToStringHelper(sb, "Security_Action", (object) this.Action);
      this.ToStringHelper(sb, "Security_TypeFirstPermThatFailed", (object) this.PermissionType);
      if (includeSensitiveInfo)
      {
        this.ToStringHelper(sb, "Security_FirstPermThatFailed", (object) this.m_permissionThatFailed);
        this.ToStringHelper(sb, "Security_Demanded", (object) this.m_demanded);
        this.ToStringHelper(sb, "Security_GrantedSet", (object) this.m_granted);
        this.ToStringHelper(sb, "Security_RefusedSet", (object) this.m_refused);
        this.ToStringHelper(sb, "Security_Denied", (object) this.m_denied);
        this.ToStringHelper(sb, "Security_PermitOnly", (object) this.m_permitOnly);
        this.ToStringHelper(sb, "Security_Assembly", (object) this.m_assemblyName);
        this.ToStringHelper(sb, "Security_Method", (object) this.m_strMethodInfo);
      }
      if (this.m_zone != SecurityZone.NoZone)
        this.ToStringHelper(sb, "Security_Zone", (object) this.m_zone);
      if (includeSensitiveInfo)
        this.ToStringHelper(sb, "Security_Url", (object) this.m_url);
      return sb.ToString();
    }

    [SecurityCritical]
    private bool CanAccessSensitiveInfo()
    {
      bool flag = false;
      try
      {
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy).Demand();
        flag = true;
      }
      catch (SecurityException ex)
      {
      }
      return flag;
    }

    /// <summary>
    ///   Возвращает представление текущего <see cref="T:System.Security.SecurityException" />.
    /// </summary>
    /// <returns>
    ///   Строковое представление текущего <see cref="T:System.Security.SecurityException" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.ToString(this.CanAccessSensitiveInfo(), true);
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Runtime.Serialization.SerializationInfo" /> сведениями об исключении <see cref="T:System.Security.SecurityException" />.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      base.GetObjectData(info, context);
      info.AddValue("Action", (object) this.m_action, typeof (SecurityAction));
      info.AddValue("FirstPermissionThatFailed", (object) this.m_permissionThatFailed, typeof (string));
      info.AddValue("Demanded", (object) this.m_demanded, typeof (string));
      info.AddValue("GrantedSet", (object) this.m_granted, typeof (string));
      info.AddValue("RefusedSet", (object) this.m_refused, typeof (string));
      info.AddValue("Denied", (object) this.m_denied, typeof (string));
      info.AddValue("PermitOnly", (object) this.m_permitOnly, typeof (string));
      info.AddValue("Assembly", (object) this.m_assemblyName, typeof (AssemblyName));
      info.AddValue("Method", (object) this.m_serializedMethodInfo, typeof (byte[]));
      info.AddValue("Method_String", (object) this.m_strMethodInfo, typeof (string));
      info.AddValue("Zone", (object) this.m_zone, typeof (SecurityZone));
      info.AddValue("Url", (object) this.m_url, typeof (string));
    }
  }
}
