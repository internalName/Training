// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.RemotingConfiguration
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Предоставляет различные статические методы для конфигурации инфраструктуры удаленного взаимодействия.
  /// </summary>
  [ComVisible(true)]
  public static class RemotingConfiguration
  {
    private static volatile bool s_ListeningForActivationRequests;

    /// <summary>
    ///   Считывает файл конфигурации и настраивает инфраструктуру удаленного взаимодействия.
    ///   <see cref="M:System.Runtime.Remoting.RemotingConfiguration.Configure(System.String)" /> устарел.
    ///    Взамен рекомендуется использовать <see cref="M:System.Runtime.Remoting.RemotingConfiguration.Configure(System.String,System.Boolean)" />.
    /// </summary>
    /// <param name="filename">
    ///   Имя файла конфигурации удаленного взаимодействия.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Use System.Runtime.Remoting.RemotingConfiguration.Configure(string fileName, bool ensureSecurity) instead.", false)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void Configure(string filename)
    {
      RemotingConfiguration.Configure(filename, false);
    }

    /// <summary>
    ///   Считывает файл конфигурации и настраивает инфраструктуру удаленного взаимодействия.
    /// </summary>
    /// <param name="filename">
    ///   Имя файла конфигурации удаленного взаимодействия.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <param name="ensureSecurity">
    ///   Если значение <see langword="true" /> безопасности не требуется.
    ///    Если значение <see langword="false" />, безопасность не является обязательным, но по-прежнему может использоваться.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void Configure(string filename, bool ensureSecurity)
    {
      RemotingConfigHandler.DoConfiguration(filename, ensureSecurity);
      RemotingServices.InternalSetRemoteActivationConfigured();
    }

    /// <summary>
    ///   Возвращает или задает имя приложения удаленного взаимодействия.
    /// </summary>
    /// <returns>Имя приложения удаленного доступа.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    ///    Это исключение вызывается только при установке значения свойства.
    /// </exception>
    public static string ApplicationName
    {
      get
      {
        if (!RemotingConfigHandler.HasApplicationNameBeenSet())
          return (string) null;
        return RemotingConfigHandler.ApplicationName;
      }
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        RemotingConfigHandler.ApplicationName = value;
      }
    }

    /// <summary>Возвращает идентификатор текущего приложения.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий идентификатор текущего приложения.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static string ApplicationId
    {
      [SecurityCritical] get
      {
        return Identity.AppDomainUniqueId;
      }
    }

    /// <summary>
    ///   Возвращает идентификатор текущего выполняемого процесса.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий идентификатор текущего выполняемого процесса.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static string ProcessId
    {
      [SecurityCritical] get
      {
        return Identity.ProcessGuid;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, каким образом пользовательские ошибки обработки.
    /// </summary>
    /// <returns>
    ///   Член <see cref="T:System.Runtime.Remoting.CustomErrorsModes" /> перечисление, указывающее, каким образом пользовательские ошибки обрабатываются.
    /// </returns>
    public static CustomErrorsModes CustomErrorsMode
    {
      get
      {
        return RemotingConfigHandler.CustomErrorsMode;
      }
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)] set
      {
        RemotingConfigHandler.CustomErrorsMode = value;
      }
    }

    /// <summary>
    ///   Указывает, возвращают ли каналы сервера в этом домене приложения локальным или удаленным вызывающим объектам фильтрованные или полные сведения об исключениях.
    /// </summary>
    /// <param name="isLocalRequest">
    ///   <see langword="true" /> Чтобы указать локальные вызывающие операторы. <see langword="false" /> для указания удаленных вызывающих объектов.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если только фильтрованные сведения об исключениях возвращаются в локальные или удаленные вызывающие, как указано в <paramref name="isLocalRequest" /> параметр; <see langword="false" /> Если полные сведения об исключении возвращается.
    /// </returns>
    public static bool CustomErrorsEnabled(bool isLocalRequest)
    {
      switch (RemotingConfiguration.CustomErrorsMode)
      {
        case CustomErrorsModes.On:
          return true;
        case CustomErrorsModes.Off:
          return false;
        case CustomErrorsModes.RemoteOnly:
          return !isLocalRequest;
        default:
          return true;
      }
    }

    /// <summary>
    ///   Регистрирует тип указанного объекта со стороны службы, как тип, который может быть активирован по запросу клиента.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Объекта для регистрации.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterActivatedServiceType(Type type)
    {
      RemotingConfiguration.RegisterActivatedServiceType(new ActivatedServiceTypeEntry(type));
    }

    /// <summary>
    ///   Тип объекта, записанный в предоставленный регистры <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> со стороны службы, может быть активирован по запросу клиента.
    /// </summary>
    /// <param name="entry">
    ///   Параметры конфигурации для активируемого клиентом типа.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
    {
      RemotingConfigHandler.RegisterActivatedServiceType(entry);
      if (RemotingConfiguration.s_ListeningForActivationRequests)
        return;
      RemotingConfiguration.s_ListeningForActivationRequests = true;
      ActivationServices.StartListeningForRemoteRequests();
    }

    /// <summary>
    ///   Регистрирует объект <see cref="T:System.Type" /> со стороны службы в качестве хорошо известного типа, используя данные параметры для инициализации нового экземпляра <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" />.
    /// </summary>
    /// <param name="type">
    ///   Объект <see cref="T:System.Type" />.
    /// </param>
    /// <param name="objectUri">Объект URI.</param>
    /// <param name="mode">
    ///   Режим активации типа хорошо известного объекта, который регистрируется.
    ///    (См. раздел <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />).
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterWellKnownServiceType(Type type, string objectUri, WellKnownObjectMode mode)
    {
      RemotingConfiguration.RegisterWellKnownServiceType(new WellKnownServiceTypeEntry(type, objectUri, mode));
    }

    /// <summary>
    ///   Регистрирует объект <see cref="T:System.Type" /> записанный в предоставленный <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> со стороны службы в качестве хорошо известного типа.
    /// </summary>
    /// <param name="entry">
    ///   Параметры конфигурации для хорошо известного типа.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
    {
      RemotingConfigHandler.RegisterWellKnownServiceType(entry);
    }

    /// <summary>
    ///   Регистрирует объект <see cref="T:System.Type" /> со стороны клиента, как тип, который может быть активирован на сервере, используя данные параметры для инициализации нового экземпляра <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> класса.
    /// </summary>
    /// <param name="type">
    ///   Объект <see cref="T:System.Type" />.
    /// </param>
    /// <param name="appUrl">
    ///   URL-адрес приложения, где активируется этот тип.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="typeName" /> или параметра <paramref name="URI" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterActivatedClientType(Type type, string appUrl)
    {
      RemotingConfiguration.RegisterActivatedClientType(new ActivatedClientTypeEntry(type, appUrl));
    }

    /// <summary>
    ///   Регистрирует объект <see cref="T:System.Type" /> записанный в предоставленный <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> со стороны клиента в качестве типа, который может быть активирован сервером.
    /// </summary>
    /// <param name="entry">
    ///   Параметры конфигурации для активируемого клиентом типа.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
    {
      RemotingConfigHandler.RegisterActivatedClientType(entry);
      RemotingServices.InternalSetRemoteActivationConfigured();
    }

    /// <summary>
    ///   Регистрирует объект <see cref="T:System.Type" /> со стороны клиента в качестве хорошо известного типа, который может быть активирован на сервере, используя данные параметры для инициализации нового экземпляра <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> класса.
    /// </summary>
    /// <param name="type">
    ///   Объект <see cref="T:System.Type" />.
    /// </param>
    /// <param name="objectUrl">
    ///   URL-адрес хорошо известного объекта.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterWellKnownClientType(Type type, string objectUrl)
    {
      RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(type, objectUrl));
    }

    /// <summary>
    ///   Регистрирует объект <see cref="T:System.Type" /> записанный в предоставленный <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> со стороны клиента в качестве хорошо известного типа, который может быть активирован на сервере.
    /// </summary>
    /// <param name="entry">
    ///   Параметры конфигурации для хорошо известного типа.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
    {
      RemotingConfigHandler.RegisterWellKnownClientType(entry);
      RemotingServices.InternalSetRemoteActivationConfigured();
    }

    /// <summary>
    ///   Извлекает массив типов объектов, зарегистрированных со стороны службы, который может быть активирован по запросу клиента.
    /// </summary>
    /// <returns>
    ///   Массив типов объектов, зарегистрированных со стороны службы, который может быть активирован по запросу клиента.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
    {
      return RemotingConfigHandler.GetRegisteredActivatedServiceTypes();
    }

    /// <summary>
    ///   Извлекает массив типов объектов, зарегистрированных со стороны службы в качестве хорошо известных типов.
    /// </summary>
    /// <returns>
    ///   Массив типов объектов, зарегистрированных со стороны службы как хорошо известных типов.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
    {
      return RemotingConfigHandler.GetRegisteredWellKnownServiceTypes();
    }

    /// <summary>
    ///   Извлекает массив типов объектов, зарегистрированных со стороны клиента, как типы, которые будет активирован удаленно.
    /// </summary>
    /// <returns>
    ///   Массив типов объектов, зарегистрированных со стороны клиента, как типы, которые будет активирован удаленно.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
    {
      return RemotingConfigHandler.GetRegisteredActivatedClientTypes();
    }

    /// <summary>
    ///   Извлекает массив типов объектов, зарегистрированных со стороны клиента в качестве хорошо известных типов.
    /// </summary>
    /// <returns>
    ///   Массив типов объектов зарегистрирован на стороне клиента, как хорошо известных типов.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
    {
      return RemotingConfigHandler.GetRegisteredWellKnownClientTypes();
    }

    /// <summary>
    ///   Проверяет, является ли указанный объект <see cref="T:System.Type" /> зарегистрирован в качестве удаленно активированного типа клиента.
    /// </summary>
    /// <param name="svrType">Тип объекта для проверки.</param>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> Соответствующий указанному типу объекта.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(Type svrType)
    {
      if (svrType == (Type) null)
        throw new ArgumentNullException(nameof (svrType));
      RuntimeType svrType1 = svrType as RuntimeType;
      if (svrType1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      return RemotingConfigHandler.IsRemotelyActivatedClientType(svrType1);
    }

    /// <summary>
    ///   Проверяет, зарегистрирован ли объект, указанный по имени типа и имя сборки, в качестве удаленно активированного типа клиента.
    /// </summary>
    /// <param name="typeName">Имя типа объекта для проверки.</param>
    /// <param name="assemblyName">Имя сборки объекта.</param>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> Соответствующий указанному типу объекта.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
    {
      return RemotingConfigHandler.IsRemotelyActivatedClientType(typeName, assemblyName);
    }

    /// <summary>
    ///   Проверяет, является ли указанный объект <see cref="T:System.Type" /> зарегистрирован в качестве хорошо известного типа клиента.
    /// </summary>
    /// <param name="svrType">
    ///   Объект <see cref="T:System.Type" /> для проверки.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> Соответствующий указанному типу объекта.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static WellKnownClientTypeEntry IsWellKnownClientType(Type svrType)
    {
      if (svrType == (Type) null)
        throw new ArgumentNullException(nameof (svrType));
      RuntimeType svrType1 = svrType as RuntimeType;
      if (svrType1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      return RemotingConfigHandler.IsWellKnownClientType(svrType1);
    }

    /// <summary>
    ///   Проверяет, зарегистрирован ли объект, указанный по имени типа и имя сборки, в качестве хорошо известного типа клиента.
    /// </summary>
    /// <param name="typeName">Имя типа объекта для проверки.</param>
    /// <param name="assemblyName">Имя сборки объекта.</param>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> Соответствующий указанному типу объекта.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
    {
      return RemotingConfigHandler.IsWellKnownClientType(typeName, assemblyName);
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, является ли указанный <see cref="T:System.Type" /> может активироваться клиентом.
    /// </summary>
    /// <param name="svrType">
    ///   Объект <see cref="T:System.Type" /> для проверки.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанный <see cref="T:System.Type" /> разрешена активироваться клиентом; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static bool IsActivationAllowed(Type svrType)
    {
      RuntimeType svrType1 = svrType as RuntimeType;
      if (svrType != (Type) null && svrType1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      return RemotingConfigHandler.IsActivationAllowed(svrType1);
    }
  }
}
