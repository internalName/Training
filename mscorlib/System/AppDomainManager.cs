// Decompiled with JetBrains decompiler
// Type: System.AppDomainManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System
{
  /// <summary>
  ///   Предоставляет управляемый эквивалент неуправляемого основного приложения.
  /// </summary>
  /// <exception cref="T:System.Security.SecurityException">
  ///   У вызывающего объекта нет нужных разрешений.
  ///    См. раздел "Требования".
  /// </exception>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class AppDomainManager : MarshalByRefObject
  {
    private AppDomainManagerInitializationOptions m_flags;
    private ApplicationActivator m_appActivator;
    private Assembly m_entryAssembly;

    /// <summary>Возвращает новый или существующий домен приложения.</summary>
    /// <param name="friendlyName">Понятное имя домена.</param>
    /// <param name="securityInfo">
    ///   Задать объект, который содержит свидетельство, отображаемое через политику безопасности для разрешений начало стека.
    /// </param>
    /// <param name="appDomainInfo">
    ///   Объект, в котором содержатся сведения об инициализации домена приложения.
    /// </param>
    /// <returns>Новый или существующий домен приложения.</returns>
    [SecurityCritical]
    public virtual AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup appDomainInfo)
    {
      return AppDomainManager.CreateDomainHelper(friendlyName, securityInfo, appDomainInfo);
    }

    /// <summary>
    ///   Предоставляет вспомогательный метод для создания домена приложения.
    /// </summary>
    /// <param name="friendlyName">Понятное имя домена.</param>
    /// <param name="securityInfo">
    ///   Задать объект, который содержит свидетельство, отображаемое через политику безопасности для разрешений начало стека.
    /// </param>
    /// <param name="appDomainInfo">
    ///   Объект, в котором содержатся сведения об инициализации домена приложения.
    /// </param>
    /// <returns>Вновь созданный домен приложения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="friendlyName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
    protected static AppDomain CreateDomainHelper(string friendlyName, Evidence securityInfo, AppDomainSetup appDomainInfo)
    {
      if (friendlyName == null)
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_String"));
      if (securityInfo != null)
      {
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
        AppDomain.CheckDomainCreationEvidence(appDomainInfo, securityInfo);
      }
      if (appDomainInfo == null)
        appDomainInfo = new AppDomainSetup();
      if (appDomainInfo.AppDomainManagerAssembly == null || appDomainInfo.AppDomainManagerType == null)
      {
        string assembly;
        string type;
        AppDomain.CurrentDomain.GetAppDomainManagerType(out assembly, out type);
        if (appDomainInfo.AppDomainManagerAssembly == null)
          appDomainInfo.AppDomainManagerAssembly = assembly;
        if (appDomainInfo.AppDomainManagerType == null)
          appDomainInfo.AppDomainManagerType = type;
      }
      if (appDomainInfo.TargetFrameworkName == null)
        appDomainInfo.TargetFrameworkName = AppDomain.CurrentDomain.GetTargetFrameworkName();
      return AppDomain.nCreateDomain(friendlyName, appDomainInfo, securityInfo, securityInfo == null ? AppDomain.CurrentDomain.InternalEvidence : (Evidence) null, AppDomain.CurrentDomain.GetSecurityDescriptor());
    }

    /// <summary>Инициализирует новый домен приложения.</summary>
    /// <param name="appDomainInfo">
    ///   Объект, в котором содержатся сведения об инициализации домена приложения.
    /// </param>
    [SecurityCritical]
    public virtual void InitializeNewDomain(AppDomainSetup appDomainInfo)
    {
    }

    /// <summary>
    ///   Возвращает флаги инициализации для диспетчеров пользовательских доменов приложений.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание значений перечисления, описывающие действие для выполнения инициализации.
    ///    Значение по умолчанию — <see cref="F:System.AppDomainManagerInitializationOptions.None" />.
    /// </returns>
    public AppDomainManagerInitializationOptions InitializationFlags
    {
      get
      {
        return this.m_flags;
      }
      set
      {
        this.m_flags = value;
      }
    }

    /// <summary>
    ///   Получает активатор приложения, который обрабатывает активации надстроек и приложений на основе манифеста для домена.
    /// </summary>
    /// <returns>Активатор приложения.</returns>
    public virtual ApplicationActivator ApplicationActivator
    {
      get
      {
        if (this.m_appActivator == null)
          this.m_appActivator = new ApplicationActivator();
        return this.m_appActivator;
      }
    }

    /// <summary>
    ///   Возвращает диспетчер безопасности узла, который участвует в системе безопасности принятии решений для домена приложения.
    /// </summary>
    /// <returns>Диспетчер безопасности основного приложения.</returns>
    public virtual HostSecurityManager HostSecurityManager
    {
      get
      {
        return (HostSecurityManager) null;
      }
    }

    /// <summary>
    ///   Возвращает диспетчер контекста выполнения, который управляет потоком контекст выполнения для узла.
    /// </summary>
    /// <returns>Диспетчер контекста выполнения узла.</returns>
    public virtual HostExecutionContextManager HostExecutionContextManager
    {
      get
      {
        return HostExecutionContextManager.GetInternalHostExecutionContextManager();
      }
    }

    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetEntryAssembly(ObjectHandleOnStack retAssembly);

    /// <summary>Возвращает сборку записей для приложения.</summary>
    /// <returns>Входная сборка для приложения.</returns>
    public virtual Assembly EntryAssembly
    {
      [SecurityCritical] get
      {
        if (this.m_entryAssembly == (Assembly) null)
        {
          AppDomain currentDomain = AppDomain.CurrentDomain;
          if (currentDomain.IsDefaultAppDomain() && currentDomain.ActivationContext != null)
          {
            this.m_entryAssembly = (Assembly) new ManifestRunner(currentDomain, currentDomain.ActivationContext).EntryAssembly;
          }
          else
          {
            RuntimeAssembly o = (RuntimeAssembly) null;
            AppDomainManager.GetEntryAssembly(JitHelpers.GetObjectHandleOnStack<RuntimeAssembly>(ref o));
            this.m_entryAssembly = (Assembly) o;
          }
        }
        return this.m_entryAssembly;
      }
    }

    internal static AppDomainManager CurrentAppDomainManager
    {
      [SecurityCritical] get
      {
        return AppDomain.CurrentDomain.DomainManager;
      }
    }

    /// <summary>
    ///   Указывает, может ли заданная операция в домене приложения.
    /// </summary>
    /// <param name="state">
    ///   Подкласс <see cref="T:System.Security.SecurityState" /> , определяющий операции, состояние безопасности.
    /// </param>
    /// <returns>
    ///   <see langword="true" />Если узел разрешает операции, заданной параметром <paramref name="state" /> быть выполнено в домене приложения; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool CheckSecuritySettings(SecurityState state)
    {
      return false;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool HasHost();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void RegisterWithHost(IntPtr appDomainManager);

    internal void RegisterWithHost()
    {
      if (!AppDomainManager.HasHost())
        return;
      IntPtr num = IntPtr.Zero;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        num = Marshal.GetIUnknownForObject((object) this);
        AppDomainManager.RegisterWithHost(num);
      }
      finally
      {
        if (!num.IsNull())
          Marshal.Release(num);
      }
    }
  }
}
