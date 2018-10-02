// Decompiled with JetBrains decompiler
// Type: System.AppDomain
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Deployment.Internal.Isolation.Manifest;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System
{
  /// <summary>
  ///   Представляет домен приложения, являющийся изолированной средой, в которой выполняются приложения.
  ///    Этот класс не наследуется.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_AppDomain))]
  [ComVisible(true)]
  public sealed class AppDomain : MarshalByRefObject, _AppDomain, IEvidenceFactory
  {
    [SecurityCritical]
    private AppDomainManager _domainManager;
    private Dictionary<string, object[]> _LocalStore;
    private AppDomainSetup _FusionStore;
    private Evidence _SecurityIdentity;
    private object[] _Policies;
    [SecurityCritical]
    private ResolveEventHandler _TypeResolve;
    [SecurityCritical]
    private ResolveEventHandler _ResourceResolve;
    [SecurityCritical]
    private ResolveEventHandler _AssemblyResolve;
    private Context _DefaultContext;
    private ActivationContext _activationContext;
    private ApplicationIdentity _applicationIdentity;
    private ApplicationTrust _applicationTrust;
    private IPrincipal _DefaultPrincipal;
    private DomainSpecificRemotingData _RemotingData;
    private EventHandler _processExit;
    private EventHandler _domainUnload;
    private UnhandledExceptionEventHandler _unhandledException;
    private string[] _aptcaVisibleAssemblies;
    private Dictionary<string, object> _compatFlags;
    private EventHandler<FirstChanceExceptionEventArgs> _firstChanceException;
    private IntPtr _pDomain;
    private PrincipalPolicy _PrincipalPolicy;
    private bool _HasSetPolicy;
    private bool _IsFastFullTrustDomain;
    private bool _compatFlagsInitialized;
    internal const string TargetFrameworkNameAppCompatSetting = "TargetFrameworkName";
    private static AppDomain.APPX_FLAGS s_flags;
    internal const int DefaultADID = 1;

    /// <summary>происходит, когда загружена сборка.</summary>
    public event AssemblyLoadEventHandler AssemblyLoad;

    /// <summary>
    ///   Происходит, когда разрешении типа завершается неудачей.
    /// </summary>
    public event ResolveEventHandler TypeResolve
    {
      [SecurityCritical] add
      {
        lock (this)
          this._TypeResolve += value;
      }
      [SecurityCritical] remove
      {
        lock (this)
          this._TypeResolve -= value;
      }
    }

    /// <summary>
    ///   происходит, когда разрешение ресурса завершается неудачей, из-за того, что он не является допустимым связанным или внедренным ресурсом в сборке.
    /// </summary>
    public event ResolveEventHandler ResourceResolve
    {
      [SecurityCritical] add
      {
        lock (this)
          this._ResourceResolve += value;
      }
      [SecurityCritical] remove
      {
        lock (this)
          this._ResourceResolve -= value;
      }
    }

    /// <summary>
    ///   Происходит, когда разрешение сборки завершается неудачей.
    /// </summary>
    public event ResolveEventHandler AssemblyResolve
    {
      [SecurityCritical] add
      {
        lock (this)
          this._AssemblyResolve += value;
      }
      [SecurityCritical] remove
      {
        lock (this)
          this._AssemblyResolve -= value;
      }
    }

    /// <summary>
    ///   Происходит, когда разрешение сборки завершается неудачей в контексте, поддерживающем только отражение.
    /// </summary>
    public event ResolveEventHandler ReflectionOnlyAssemblyResolve;

    private static AppDomain.APPX_FLAGS Flags
    {
      [SecuritySafeCritical] get
      {
        if (AppDomain.s_flags == (AppDomain.APPX_FLAGS) 0)
          AppDomain.s_flags = AppDomain.nGetAppXFlags();
        return AppDomain.s_flags;
      }
    }

    internal static bool ProfileAPICheck
    {
      [SecuritySafeCritical] get
      {
        return (uint) (AppDomain.Flags & AppDomain.APPX_FLAGS.APPX_FLAGS_API_CHECK) > 0U;
      }
    }

    internal static bool IsAppXNGen
    {
      [SecuritySafeCritical] get
      {
        return (uint) (AppDomain.Flags & AppDomain.APPX_FLAGS.APPX_FLAGS_APPX_NGEN) > 0U;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DisableFusionUpdatesFromADManager(AppDomainHandle domain);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.I4)]
    private static extern AppDomain.APPX_FLAGS nGetAppXFlags();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetAppDomainManagerType(AppDomainHandle domain, StringHandleOnStack retAssembly, StringHandleOnStack retType);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetAppDomainManagerType(AppDomainHandle domain, string assembly, string type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void nSetHostSecurityManagerFlags(HostSecurityManagerOptions flags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetSecurityHomogeneousFlag(AppDomainHandle domain, [MarshalAs(UnmanagedType.Bool)] bool runtimeSuppliedHomogenousGrantSet);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetLegacyCasPolicyEnabled(AppDomainHandle domain);

    [SecurityCritical]
    private void SetLegacyCasPolicyEnabled()
    {
      AppDomain.SetLegacyCasPolicyEnabled(this.GetNativeHandle());
    }

    internal AppDomainHandle GetNativeHandle()
    {
      if (this._pDomain.IsNull())
        throw new InvalidOperationException(Environment.GetResourceString("Argument_InvalidHandle"));
      return new AppDomainHandle(this._pDomain);
    }

    [SecuritySafeCritical]
    private void CreateAppDomainManager()
    {
      AppDomainSetup fusionStore = this.FusionStore;
      string assembly;
      string type;
      this.GetAppDomainManagerType(out assembly, out type);
      if (assembly != null)
      {
        if (type != null)
        {
          try
          {
            new PermissionSet(PermissionState.Unrestricted).Assert();
            this._domainManager = this.CreateInstanceAndUnwrap(assembly, type) as AppDomainManager;
            CodeAccessPermission.RevertAssert();
          }
          catch (FileNotFoundException ex)
          {
            throw new TypeLoadException(Environment.GetResourceString("Argument_NoDomainManager"), (Exception) ex);
          }
          catch (SecurityException ex)
          {
            throw new TypeLoadException(Environment.GetResourceString("Argument_NoDomainManager"), (Exception) ex);
          }
          catch (TypeLoadException ex)
          {
            throw new TypeLoadException(Environment.GetResourceString("Argument_NoDomainManager"), (Exception) ex);
          }
          if (this._domainManager == null)
            throw new TypeLoadException(Environment.GetResourceString("Argument_NoDomainManager"));
          this.FusionStore.AppDomainManagerAssembly = assembly;
          this.FusionStore.AppDomainManagerType = type;
          bool flag = this._domainManager.GetType() != typeof (AppDomainManager) && !this.DisableFusionUpdatesFromADManager();
          AppDomainSetup oldInfo = (AppDomainSetup) null;
          if (flag)
            oldInfo = new AppDomainSetup(this.FusionStore, true);
          this._domainManager.InitializeNewDomain(this.FusionStore);
          if (flag)
            this.SetupFusionStore(this._FusionStore, oldInfo);
          if ((this._domainManager.InitializationFlags & AppDomainManagerInitializationOptions.RegisterWithHost) == AppDomainManagerInitializationOptions.RegisterWithHost)
            this._domainManager.RegisterWithHost();
        }
      }
      this.InitializeCompatibilityFlags();
    }

    private void InitializeCompatibilityFlags()
    {
      AppDomainSetup fusionStore = this.FusionStore;
      if (fusionStore.GetCompatibilityFlags() != null)
        this._compatFlags = new Dictionary<string, object>((IDictionary<string, object>) fusionStore.GetCompatibilityFlags(), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this._compatFlagsInitialized = true;
      CompatibilitySwitches.InitializeSwitches();
    }

    [SecuritySafeCritical]
    internal string GetTargetFrameworkName()
    {
      string str = this._FusionStore.TargetFrameworkName;
      if (str == null && this.IsDefaultAppDomain() && !this._FusionStore.CheckedForTargetFrameworkName)
      {
        Assembly entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != (Assembly) null)
        {
          TargetFrameworkAttribute[] customAttributes = (TargetFrameworkAttribute[]) entryAssembly.GetCustomAttributes(typeof (TargetFrameworkAttribute));
          if (customAttributes != null && customAttributes.Length != 0)
          {
            str = customAttributes[0].FrameworkName;
            this._FusionStore.TargetFrameworkName = str;
          }
        }
        this._FusionStore.CheckedForTargetFrameworkName = true;
      }
      return str;
    }

    [SecuritySafeCritical]
    private void SetTargetFrameworkName(string targetFrameworkName)
    {
      if (this._FusionStore.CheckedForTargetFrameworkName)
        return;
      this._FusionStore.TargetFrameworkName = targetFrameworkName;
      this._FusionStore.CheckedForTargetFrameworkName = true;
    }

    [SecuritySafeCritical]
    internal bool DisableFusionUpdatesFromADManager()
    {
      return AppDomain.DisableFusionUpdatesFromADManager(this.GetNativeHandle());
    }

    [SecuritySafeCritical]
    internal static bool IsAppXModel()
    {
      return (uint) (AppDomain.Flags & AppDomain.APPX_FLAGS.APPX_FLAGS_APPX_MODEL) > 0U;
    }

    [SecuritySafeCritical]
    internal static bool IsAppXDesignMode()
    {
      return (AppDomain.Flags & AppDomain.APPX_FLAGS.APPX_FLAGS_APPX_MASK) == (AppDomain.APPX_FLAGS.APPX_FLAGS_APPX_MODEL | AppDomain.APPX_FLAGS.APPX_FLAGS_APPX_DESIGN_MODE);
    }

    [SecuritySafeCritical]
    internal static void CheckLoadFromSupported()
    {
      if (AppDomain.IsAppXModel())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "Assembly.LoadFrom"));
    }

    [SecuritySafeCritical]
    internal static void CheckLoadFileSupported()
    {
      if (AppDomain.IsAppXModel())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "Assembly.LoadFile"));
    }

    [SecuritySafeCritical]
    internal static void CheckReflectionOnlyLoadSupported()
    {
      if (AppDomain.IsAppXModel())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "Assembly.ReflectionOnlyLoad"));
    }

    [SecuritySafeCritical]
    internal static void CheckLoadWithPartialNameSupported(StackCrawlMark stackMark)
    {
      if (!AppDomain.IsAppXModel())
        return;
      RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
      if (!((Assembly) executingAssembly != (Assembly) null) || !executingAssembly.IsFrameworkAssembly())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "Assembly.LoadWithPartialName"));
    }

    [SecuritySafeCritical]
    internal static void CheckDefinePInvokeSupported()
    {
      if (AppDomain.IsAppXModel())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "DefinePInvokeMethod"));
    }

    [SecuritySafeCritical]
    internal static void CheckLoadByteArraySupported()
    {
      if (AppDomain.IsAppXModel())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "Assembly.Load(byte[], ...)"));
    }

    [SecuritySafeCritical]
    internal static void CheckCreateDomainSupported()
    {
      if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "AppDomain.CreateDomain"));
    }

    [SecuritySafeCritical]
    internal void GetAppDomainManagerType(out string assembly, out string type)
    {
      string s1 = (string) null;
      string s2 = (string) null;
      AppDomain.GetAppDomainManagerType(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref s1), JitHelpers.GetStringHandleOnStack(ref s2));
      assembly = s1;
      type = s2;
    }

    [SecuritySafeCritical]
    private void SetAppDomainManagerType(string assembly, string type)
    {
      AppDomain.SetAppDomainManagerType(this.GetNativeHandle(), assembly, type);
    }

    internal string[] PartialTrustVisibleAssemblies
    {
      get
      {
        return this._aptcaVisibleAssemblies;
      }
      [SecuritySafeCritical] set
      {
        this._aptcaVisibleAssemblies = value;
        string canonicalList = (string) null;
        if (value != null)
        {
          StringBuilder sb = StringBuilderCache.Acquire(16);
          for (int index = 0; index < value.Length; ++index)
          {
            if (value[index] != null)
            {
              sb.Append(value[index].ToUpperInvariant());
              if (index != value.Length - 1)
                sb.Append(';');
            }
          }
          canonicalList = StringBuilderCache.GetStringAndRelease(sb);
        }
        this.SetCanonicalConditionalAptcaList(canonicalList);
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetCanonicalConditionalAptcaList(AppDomainHandle appDomain, string canonicalList);

    [SecurityCritical]
    private void SetCanonicalConditionalAptcaList(string canonicalList)
    {
      AppDomain.SetCanonicalConditionalAptcaList(this.GetNativeHandle(), canonicalList);
    }

    private void SetupDefaultClickOnceDomain(string fullName, string[] manifestPaths, string[] activationData)
    {
      this.FusionStore.ActivationArguments = new ActivationArguments(fullName, manifestPaths, activationData);
    }

    [SecurityCritical]
    private void InitializeDomainSecurity(Evidence providedSecurityInfo, Evidence creatorsSecurityInfo, bool generateDefaultEvidence, IntPtr parentSecurityDescriptor, bool publishAppDomain)
    {
      AppDomainSetup fusionStore = this.FusionStore;
      if (CompatibilitySwitches.IsNetFx40LegacySecurityPolicy)
        this.SetLegacyCasPolicyEnabled();
      if (fusionStore.ActivationArguments != null)
      {
        ActivationContext activationContext = (ActivationContext) null;
        ApplicationIdentity applicationIdentity = (ApplicationIdentity) null;
        CmsUtils.CreateActivationContext(fusionStore.ActivationArguments.ApplicationFullName, fusionStore.ActivationArguments.ApplicationManifestPaths, fusionStore.ActivationArguments.UseFusionActivationContext, out applicationIdentity, out activationContext);
        string[] activationData = fusionStore.ActivationArguments.ActivationData;
        providedSecurityInfo = CmsUtils.MergeApplicationEvidence(providedSecurityInfo, applicationIdentity, activationContext, activationData, fusionStore.ApplicationTrust);
        this.SetupApplicationHelper(providedSecurityInfo, creatorsSecurityInfo, applicationIdentity, activationContext, activationData);
      }
      else
      {
        bool runtimeSuppliedHomogenousGrantSet = false;
        ApplicationTrust applicationTrust = fusionStore.ApplicationTrust;
        if (applicationTrust == null && !this.IsLegacyCasPolicyEnabled)
        {
          this._IsFastFullTrustDomain = true;
          runtimeSuppliedHomogenousGrantSet = true;
        }
        if (applicationTrust != null)
          this.SetupDomainSecurityForHomogeneousDomain(applicationTrust, runtimeSuppliedHomogenousGrantSet);
        else if (this._IsFastFullTrustDomain)
          AppDomain.SetSecurityHomogeneousFlag(this.GetNativeHandle(), runtimeSuppliedHomogenousGrantSet);
      }
      Evidence evidence = providedSecurityInfo != null ? providedSecurityInfo : creatorsSecurityInfo;
      if (evidence == null & generateDefaultEvidence)
        evidence = new Evidence((IRuntimeEvidenceFactory) new AppDomainEvidenceFactory(this));
      if (this._domainManager != null)
      {
        HostSecurityManager hostSecurityManager = this._domainManager.HostSecurityManager;
        if (hostSecurityManager != null)
        {
          AppDomain.nSetHostSecurityManagerFlags(hostSecurityManager.Flags);
          if ((hostSecurityManager.Flags & HostSecurityManagerOptions.HostAppDomainEvidence) == HostSecurityManagerOptions.HostAppDomainEvidence)
          {
            evidence = hostSecurityManager.ProvideAppDomainEvidence(evidence);
            if (evidence != null && evidence.Target == null)
              evidence.Target = (IRuntimeEvidenceFactory) new AppDomainEvidenceFactory(this);
          }
        }
      }
      this._SecurityIdentity = evidence;
      this.SetupDomainSecurity(evidence, parentSecurityDescriptor, publishAppDomain);
      if (this._domainManager == null)
        return;
      this.RunDomainManagerPostInitialization(this._domainManager);
    }

    [SecurityCritical]
    private void RunDomainManagerPostInitialization(AppDomainManager domainManager)
    {
      HostExecutionContextManager executionContextManager = domainManager.HostExecutionContextManager;
      if (!this.IsLegacyCasPolicyEnabled)
        return;
      HostSecurityManager hostSecurityManager = domainManager.HostSecurityManager;
      if (hostSecurityManager == null || (hostSecurityManager.Flags & HostSecurityManagerOptions.HostPolicyLevel) != HostSecurityManagerOptions.HostPolicyLevel)
        return;
      PolicyLevel domainPolicy = hostSecurityManager.DomainPolicy;
      if (domainPolicy == null)
        return;
      this.SetAppDomainPolicy(domainPolicy);
    }

    [SecurityCritical]
    private void SetupApplicationHelper(Evidence providedSecurityInfo, Evidence creatorsSecurityInfo, ApplicationIdentity appIdentity, ActivationContext activationContext, string[] activationData)
    {
      ApplicationTrust applicationTrust = AppDomain.CurrentDomain.HostSecurityManager.DetermineApplicationTrust(providedSecurityInfo, creatorsSecurityInfo, new TrustManagerContext());
      if (applicationTrust == null || !applicationTrust.IsApplicationTrustedToRun)
        throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"), -2146233320, (Exception) null);
      if (activationContext != null)
        this.SetupDomainForApplication(activationContext, activationData);
      this.SetupDomainSecurityForApplication(appIdentity, applicationTrust);
    }

    [SecurityCritical]
    private void SetupDomainForApplication(ActivationContext activationContext, string[] activationData)
    {
      if (this.IsDefaultAppDomain())
      {
        AppDomainSetup fusionStore = this.FusionStore;
        fusionStore.ActivationArguments = new ActivationArguments(activationContext, activationData);
        string entryPointFullPath = CmsUtils.GetEntryPointFullPath(activationContext);
        if (!string.IsNullOrEmpty(entryPointFullPath))
          fusionStore.SetupDefaults(entryPointFullPath, false);
        else
          fusionStore.ApplicationBase = activationContext.ApplicationDirectory;
        this.SetupFusionStore(fusionStore, (AppDomainSetup) null);
      }
      activationContext.PrepareForExecution();
      int num1 = (int) activationContext.SetApplicationState(ActivationContext.ApplicationState.Starting);
      int num2 = (int) activationContext.SetApplicationState(ActivationContext.ApplicationState.Running);
      IPermission permission = (IPermission) null;
      string dataDirectory = activationContext.DataDirectory;
      if (dataDirectory != null && dataDirectory.Length > 0)
        permission = (IPermission) new FileIOPermission(FileIOPermissionAccess.PathDiscovery, dataDirectory);
      this.SetData("DataDirectory", (object) dataDirectory, permission);
      this._activationContext = activationContext;
    }

    [SecurityCritical]
    private void SetupDomainSecurityForApplication(ApplicationIdentity appIdentity, ApplicationTrust appTrust)
    {
      this._applicationIdentity = appIdentity;
      this.SetupDomainSecurityForHomogeneousDomain(appTrust, false);
    }

    [SecurityCritical]
    private void SetupDomainSecurityForHomogeneousDomain(ApplicationTrust appTrust, bool runtimeSuppliedHomogenousGrantSet)
    {
      if (runtimeSuppliedHomogenousGrantSet)
        this._FusionStore.ApplicationTrust = (ApplicationTrust) null;
      this._applicationTrust = appTrust;
      AppDomain.SetSecurityHomogeneousFlag(this.GetNativeHandle(), runtimeSuppliedHomogenousGrantSet);
    }

    [SecuritySafeCritical]
    private int ActivateApplication()
    {
      return (int) Activator.CreateInstance(AppDomain.CurrentDomain.ActivationContext).Unwrap();
    }

    /// <summary>
    ///   Возвращает диспетчер домена, предоставленный средой размещения при инициализации домена приложения.
    /// </summary>
    /// <returns>
    ///   Объект, который представляет диспетчер домена, предоставленный средой размещения при инициализации домена приложения, или значение <see langword="null" />, если диспетчер домена не предоставлен.
    /// </returns>
    public AppDomainManager DomainManager
    {
      [SecurityCritical] get
      {
        return this._domainManager;
      }
    }

    internal HostSecurityManager HostSecurityManager
    {
      [SecurityCritical] get
      {
        HostSecurityManager hostSecurityManager = (HostSecurityManager) null;
        AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
        if (domainManager != null)
          hostSecurityManager = domainManager.HostSecurityManager;
        if (hostSecurityManager == null)
          hostSecurityManager = new HostSecurityManager();
        return hostSecurityManager;
      }
    }

    private Assembly ResolveAssemblyForIntrospection(object sender, ResolveEventArgs args)
    {
      return Assembly.ReflectionOnlyLoad(this.ApplyPolicy(args.Name));
    }

    [SecuritySafeCritical]
    private void EnableResolveAssembliesForIntrospection(string verifiedFileDirectory)
    {
      AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(this.ResolveAssemblyForIntrospection);
      string[] strArray = (string[]) null;
      if (verifiedFileDirectory != null)
        strArray = new string[1]{ verifiedFileDirectory };
      WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve += new EventHandler<NamespaceResolveEventArgs>(new AppDomain.NamespaceResolverForIntrospection((IEnumerable<string>) strArray).ResolveNamespace);
    }

    /// <summary>
    ///   Определяет динамическую сборку с указанным именем и режимом доступа.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим доступа для динамической сборки.
    /// </param>
    /// <returns>
    ///   Динамическая сборка с указанным именем и режимом доступа.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> начинается с пробела или содержит прямую либо обратную косую черту.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, (string) null, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Определяет динамическую сборку с указанным именем, режимом доступа и настраиваемыми атрибутами.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим доступа для динамической сборки.
    /// </param>
    /// <param name="assemblyAttributes">
    ///   Перечислимый список атрибутов, которые будут применены к сборке, или значение <see langword="null" />, если атрибуты отсутствуют.
    /// </param>
    /// <returns>Динамическая сборка с указанным именем и функциями.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="Name" /> Свойства <paramref name="name" /> начинается с пробела или содержит прямой или обратной косой черты.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, (string) null, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, assemblyAttributes, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Определяет динамическую сборку с помощью указанного имени, режима доступа и настраиваемых атрибутов, а также используя заданный источник для контекста безопасности.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим доступа для динамической сборки.
    /// </param>
    /// <param name="assemblyAttributes">
    ///   Перечислимый список атрибутов, которые будут применены к сборке, или значение <see langword="null" />, если атрибуты отсутствуют.
    /// </param>
    /// <param name="securityContextSource">
    ///   Источник контекста безопасности.
    /// </param>
    /// <returns>Динамическая сборка с указанным именем и функциями.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="Name" /> Свойства <paramref name="name" /> начинается с пробела или содержит прямой или обратной косой черты.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="securityContextSource" /> не является одним из значений перечисления.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes, SecurityContextSource securityContextSource)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, (string) null, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, assemblyAttributes, securityContextSource);
    }

    /// <summary>
    ///   Определяет динамическую сборку с помощью указанного имени, режима доступа и каталога хранения.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="dir">
    ///   Имя папки, в которой будет сохранена сборка.
    ///    Если параметр <paramref name="dir" /> имеет значение <see langword="null" />, по умолчанию используется текущий каталог.
    /// </param>
    /// <returns>Динамическая сборка с указанным именем и функциями.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> начинается с пробела или содержит прямую либо обратную косую черту.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Определяет динамическую сборку с помощью указанного имени, режима доступа и свидетельства.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="evidence">
    ///   Свидетельство, предоставляемое для динамической сборки.
    ///    Используемое свидетельство является постоянным, как конечный набор свидетельств, используемых для разрешения политики.
    /// </param>
    /// <returns>Динамическая сборка с указанным именем и функциями.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> начинается с пробела или содержит прямую либо обратную косую черту.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default.  See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, (string) null, evidence, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Определяет динамическую сборку с помощью указанного имени, режима доступа и запросов разрешений.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="requiredPermissions">
    ///   Запрос обязательных разрешений.
    /// </param>
    /// <param name="optionalPermissions">
    ///   Запрос дополнительных разрешений.
    /// </param>
    /// <param name="refusedPermissions">
    ///   Запрос разрешений, в которых отказано.
    /// </param>
    /// <returns>Динамическая сборка с указанным именем и функциями.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> начинается с пробела или содержит прямую либо обратную косую черту.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default.  See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, (string) null, (Evidence) null, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Определяет динамическую сборку с помощью указанного имени, режима доступа, каталога хранения и свидетельства.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="dir">
    ///   Имя папки, в которой будет сохранена сборка.
    ///    Если параметр <paramref name="dir" /> имеет значение <see langword="null" />, по умолчанию используется текущий каталог.
    /// </param>
    /// <param name="evidence">
    ///   Свидетельство, предоставляемое для динамической сборки.
    ///    Используемое свидетельство является постоянным, как конечный набор свидетельств, используемых для разрешения политики.
    /// </param>
    /// <returns>Динамическая сборка с указанным именем и функциями.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> начинается с пробела или содержит прямую либо обратную косую черту.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of DefineDynamicAssembly which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkId=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, evidence, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Определяет динамическую сборку с помощью указанного имени, режима доступа, каталога хранения и запросов разрешений.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="dir">
    ///   Имя папки, в которой будет сохранена сборка.
    ///    Если параметр <paramref name="dir" /> имеет значение <see langword="null" />, по умолчанию используется текущий каталог.
    /// </param>
    /// <param name="requiredPermissions">
    ///   Запрос обязательных разрешений.
    /// </param>
    /// <param name="optionalPermissions">
    ///   Запрос дополнительных разрешений.
    /// </param>
    /// <param name="refusedPermissions">
    ///   Запрос разрешений, в которых отказано.
    /// </param>
    /// <returns>Динамическая сборка с указанным именем и функциями.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> начинается с пробела или содержит прямую либо обратную косую черту.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, (Evidence) null, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Определяет динамическую сборку с помощью указанного имени, режима доступа, свидетельства и запросов разрешений.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="evidence">
    ///   Свидетельство, предоставляемое для динамической сборки.
    ///    Используемое свидетельство является постоянным, как конечный набор свидетельств, используемых для разрешения политики.
    /// </param>
    /// <param name="requiredPermissions">
    ///   Запрос обязательных разрешений.
    /// </param>
    /// <param name="optionalPermissions">
    ///   Запрос дополнительных разрешений.
    /// </param>
    /// <param name="refusedPermissions">
    ///   Запрос разрешений, в которых отказано.
    /// </param>
    /// <returns>Динамическая сборка с указанным именем и функциями.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> начинается с пробела или содержит прямую либо обратную косую черту.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, (string) null, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Определяет динамическую сборку с помощью указанного имени, режима доступа, каталога хранения, свидетельства и запросов разрешений.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="dir">
    ///   Имя папки, в которой будет сохранена сборка.
    ///    Если параметр <paramref name="dir" /> имеет значение <see langword="null" />, по умолчанию используется текущий каталог.
    /// </param>
    /// <param name="evidence">
    ///   Свидетельство, предоставляемое для динамической сборки.
    ///    Используемое свидетельство является постоянным, как конечный набор свидетельств, используемых для разрешения политики.
    /// </param>
    /// <param name="requiredPermissions">
    ///   Запрос обязательных разрешений.
    /// </param>
    /// <param name="optionalPermissions">
    ///   Запрос дополнительных разрешений.
    /// </param>
    /// <param name="refusedPermissions">
    ///   Запрос разрешений, в которых отказано.
    /// </param>
    /// <returns>Динамическая сборка с указанным именем и функциями.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> начинается с пробела или содержит прямую либо обратную косую черту.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default.  Please see http://go.microsoft.com/fwlink/?LinkId=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Определяет динамическую сборку с помощью указанного имени, режима доступа, каталога хранения, свидетельства, запросов разрешений и параметра синхронизации.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="dir">
    ///   Имя каталога, в котором будет сохранена динамическая сборка.
    ///    Если параметр <paramref name="dir" /> имеет значение <see langword="null" />, по умолчанию используется текущий каталог.
    /// </param>
    /// <param name="evidence">
    ///   Свидетельство, предоставляемое для динамической сборки.
    ///    Используемое свидетельство является постоянным, как конечный набор свидетельств, используемых для разрешения политики.
    /// </param>
    /// <param name="requiredPermissions">
    ///   Запрос обязательных разрешений.
    /// </param>
    /// <param name="optionalPermissions">
    ///   Запрос дополнительных разрешений.
    /// </param>
    /// <param name="refusedPermissions">
    ///   Запрос разрешений, в которых отказано.
    /// </param>
    /// <param name="isSynchronized">
    ///   Значение <see langword="true" />, чтобы синхронизировать создание модулей, типов и членов в динамической сборке, в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>Динамическая сборка с указанным именем и функциями.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> начинается с пробела или содержит прямую либо обратную косую черту.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Определяет динамическую сборку с помощью указанного имени, режима доступа, каталога хранения, свидетельства, запросов разрешений, параметра синхронизации и настраиваемых атрибутов.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="dir">
    ///   Имя каталога, в котором будет сохранена динамическая сборка.
    ///    Если параметр <paramref name="dir" /> имеет значение <see langword="null" />, используется текущий каталог.
    /// </param>
    /// <param name="evidence">
    ///   Свидетельство, предоставленное для динамической сборки.
    ///    Используемое свидетельство является постоянным, как конечный набор свидетельств, используемых для разрешения политики.
    /// </param>
    /// <param name="requiredPermissions">
    ///   Запрос обязательных разрешений.
    /// </param>
    /// <param name="optionalPermissions">
    ///   Запрос дополнительных разрешений.
    /// </param>
    /// <param name="refusedPermissions">
    ///   Запрос разрешений, в которых отказано.
    /// </param>
    /// <param name="isSynchronized">
    ///   Значение <see langword="true" />, чтобы синхронизировать создание модулей, типов и членов в динамической сборке, в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="assemblyAttributes">
    ///   Перечислимый список атрибутов, которые будут применены к сборке, или значение <see langword="null" />, если атрибуты отсутствуют.
    /// </param>
    /// <returns>Динамическая сборка с указанным именем и функциями.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="Name" /> Свойства <paramref name="name" /> начинается с пробела или содержит прямой или обратной косой черты.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, assemblyAttributes, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Определяет динамическую сборку с помощью указанного имени, режима доступа, каталога хранения и варианта синхронизации.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="dir">
    ///   Имя каталога, в котором будет сохранена динамическая сборка.
    ///    Если параметр <paramref name="dir" /> имеет значение <see langword="null" />, используется текущий каталог.
    /// </param>
    /// <param name="isSynchronized">
    ///   Значение <see langword="true" />, чтобы синхронизировать создание модулей, типов и членов в динамической сборке, в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="assemblyAttributes">
    ///   Перечислимый список атрибутов, которые будут применены к сборке, или значение <see langword="null" />, если атрибуты отсутствуют.
    /// </param>
    /// <returns>Динамическая сборка с указанным именем и функциями.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see langword="Name" /> параметра <paramref name="name" /> равно <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="Name" /> Свойства <paramref name="name" /> начинается с пробела или содержит прямой или обратной косой черты.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, bool isSynchronized, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, assemblyAttributes, SecurityContextSource.CurrentAssembly);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private AssemblyBuilder InternalDefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, ref StackCrawlMark stackMark, IEnumerable<CustomAttributeBuilder> assemblyAttributes, SecurityContextSource securityContextSource)
    {
      return AssemblyBuilder.InternalDefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, assemblyAttributes, securityContextSource);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern string nApplyPolicy(AssemblyName an);

    /// <summary>
    ///   Возвращает отображаемое имя сборки после применения политики.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки в форме, заданной свойством <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <returns>
    ///   Строка, содержащая отображаемое имя сборки после применения политики.
    /// </returns>
    [ComVisible(false)]
    public string ApplyPolicy(string assemblyName)
    {
      AssemblyName an = new AssemblyName(assemblyName);
      byte[] numArray = an.GetPublicKeyToken() ?? an.GetPublicKey();
      if (numArray == null || numArray.Length == 0)
        return assemblyName;
      return this.nApplyPolicy(an);
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в указанной сборке.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, заданного параметром <paramref name="typeName" />.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assemblyName" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typename" /> не найден в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Этот экземпляр имеет значение <see langword="null" />.
    /// </exception>
    public ObjectHandle CreateInstance(string assemblyName, string typeName)
    {
      if (this == null)
        throw new NullReferenceException();
      if (assemblyName == null)
        throw new ArgumentNullException(nameof (assemblyName));
      return Activator.CreateInstance(assemblyName, typeName);
    }

    [SecurityCritical]
    internal ObjectHandle InternalCreateInstanceWithNoSecurity(string assemblyName, string typeName)
    {
      PermissionSet.s_fullTrust.Assert();
      return this.CreateInstance(assemblyName, typeName);
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в указанном файле сборки.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя (включая путь) файла, который содержит сборку, определяющую запрошенный тип.
    ///    Эта сборка загружается с помощью метода <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, или значение <see langword="null" />, если объект <paramref name="typeName" /> не найден.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typeName" /> не найден в <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Найден открытый конструктор без параметров.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет достаточно разрешений для вызова этого конструктора.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyFile" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Этот экземпляр имеет значение <see langword="null" />.
    /// </exception>
    public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
    {
      if (this == null)
        throw new NullReferenceException();
      return Activator.CreateInstanceFrom(assemblyFile, typeName);
    }

    [SecurityCritical]
    internal ObjectHandle InternalCreateInstanceFromWithNoSecurity(string assemblyName, string typeName)
    {
      PermissionSet.s_fullTrust.Assert();
      return this.CreateInstanceFrom(assemblyName, typeName);
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа COM.
    ///    Параметры задают имя файла сборки, содержащей этот тип, и имя типа.
    /// </summary>
    /// <param name="assemblyName">
    ///   Имя файла, который содержит сборку, определяющую запрошенный тип.
    /// </param>
    /// <param name="typeName">Имя запрошенного типа.</param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, заданного параметром <paramref name="typeName" />.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assemblyName" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Открытый конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyName" /> не найден.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   <paramref name="typeName" /> является абстрактным классом.
    /// 
    ///   -или-
    /// 
    ///   Этот элемент был вызван при помощи механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="assemblyName" /> является пустой строкой ("").
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   COM-объект, который дается ссылка является <see langword="null" />.
    /// </exception>
    public ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
    {
      if (this == null)
        throw new NullReferenceException();
      return Activator.CreateComInstanceFrom(assemblyName, typeName);
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа COM.
    ///    Параметры задают имя файла сборки, содержащей этот тип, и имя типа.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, который содержит сборку, определяющую запрошенный тип.
    /// </param>
    /// <param name="typeName">Имя запрошенного типа.</param>
    /// <param name="hashValue">
    ///   Представляет значение вычисляемого хэш-кода.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Представляет хэш-алгоритм, используемый манифестом сборки.
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, заданного параметром <paramref name="typeName" />.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assemblyName" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Открытый конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyFile" /> не найден.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   <paramref name="typeName" /> является абстрактным классом.
    /// 
    ///   -или-
    /// 
    ///   Этот элемент был вызван при помощи механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="assemblyFile" /> является пустой строкой ("").
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   COM-объект, который дается ссылка является <see langword="null" />.
    /// </exception>
    public ObjectHandle CreateComInstanceFrom(string assemblyFile, string typeName, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
      if (this == null)
        throw new NullReferenceException();
      return Activator.CreateComInstanceFrom(assemblyFile, typeName, hashValue, hashAlgorithm);
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в указанной сборке.
    ///    Параметр определяет массив атрибутов активации.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Обычно это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом. Активация клиента — это устаревшая технология, которая сохраняется для обеспечения обратной совместимости, но не рекомендуется для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, заданного параметром <paramref name="typeName" />.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assemblyName" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typename" /> не найден в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Этот экземпляр имеет значение <see langword="null" />.
    /// </exception>
    public ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes)
    {
      if (this == null)
        throw new NullReferenceException();
      if (assemblyName == null)
        throw new ArgumentNullException(nameof (assemblyName));
      return Activator.CreateInstance(assemblyName, typeName, activationAttributes);
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в указанном файле сборки.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя (включая путь) файла, который содержит сборку, определяющую запрошенный тип.
    ///    Эта сборка загружается с помощью метода <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Обычно это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом. Активация клиента — это устаревшая технология, которая сохраняется для обеспечения обратной совместимости, но не рекомендуется для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, или значение <see langword="null" />, если объект <paramref name="typeName" /> не найден.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typeName" /> не найден в <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет достаточно разрешений для вызова этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyFile" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Этот экземпляр имеет значение <see langword="null" />.
    /// </exception>
    public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes)
    {
      if (this == null)
        throw new NullReferenceException();
      return Activator.CreateInstanceFrom(assemblyFile, typeName, activationAttributes);
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в указанной сборке.
    ///    Параметры определяют средство связывания, флаги привязки, аргументы конструктора, сведения, связанные с языком и региональными параметрами, используемые для интерпретации аргументов, атрибуты активации и авторизацию для создания типа.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при поиске.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see cref="T:System.Reflection.MemberInfo" /> с помощью отражения.
    ///    Если параметр <paramref name="binder" /> имеет значение null, то используется модуль привязки по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Аргументы для передачи конструктору.
    ///    Массив аргументов должен соответствовать по числу, порядку и типу параметров вызываемому конструктору.
    ///    Если предпочтителен конструктор по умолчанию, то объект <paramref name="args" /> должен быть пустым массивом или значением null.
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Обычно это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом. Активация клиента — это устаревшая технология, которая сохраняется для обеспечения обратной совместимости, но не рекомендуется для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <param name="securityAttributes">
    ///   Сведения, используемые для авторизации создания <paramref name="typeName" />.
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, заданного параметром <paramref name="typeName" />.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assemblyName" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="securityAttributes" /> не является <see langword="null" />.
    ///    Если устаревшая политика CAS не включена, <paramref name="securityAttributes" /> должно быть<see langword="null." />
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typename" /> не найден в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Этот экземпляр имеет значение <see langword="null" />.
    /// </exception>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      if (this == null)
        throw new NullReferenceException();
      if (assemblyName == null)
        throw new ArgumentNullException(nameof (assemblyName));
      if (securityAttributes != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в указанной сборке.
    ///    Параметры определяют средство связывания, флаги привязки, аргументы конструктора, сведения, связанные с языком и региональными параметрами, используемые для интерпретации аргументов, и необязательные атрибуты активации.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при поиске.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see cref="T:System.Reflection.MemberInfo" /> с помощью отражения.
    ///    Если параметр <paramref name="binder" /> имеет значение null, то используется модуль привязки по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Аргументы для передачи конструктору.
    ///    Массив аргументов должен соответствовать по числу, порядку и типу параметров вызываемому конструктору.
    ///    Если предпочтителен конструктор по умолчанию, то объект <paramref name="args" /> должен быть пустым массивом или значением null.
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Обычно это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, заданного параметром <paramref name="typeName" />.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assemblyName" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typename" /> не найден в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Этот экземпляр имеет значение <see langword="null" />.
    /// </exception>
    public ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      if (this == null)
        throw new NullReferenceException();
      if (assemblyName == null)
        throw new ArgumentNullException(nameof (assemblyName));
      return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
    }

    [SecurityCritical]
    internal ObjectHandle InternalCreateInstanceWithNoSecurity(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      PermissionSet.s_fullTrust.Assert();
      return this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в указанном файле сборки.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя (включая путь) файла, который содержит сборку, определяющую запрошенный тип.
    ///    Эта сборка загружается с помощью метода <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при поиске.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который допускает привязку, приведение типов аргументов, вызов элементов и извлечение объектов <see cref="T:System.Reflection.MemberInfo" /> путем отражения.
    ///    Если параметр <paramref name="binder" /> имеет значение null, то используется модуль привязки по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Аргументы для передачи конструктору.
    ///    Массив аргументов должен соответствовать по числу, порядку и типу параметров вызываемому конструктору.
    ///    Если предпочтителен конструктор по умолчанию, то объект <paramref name="args" /> должен быть пустым массивом или значением null.
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Обычно это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <param name="securityAttributes">
    ///   Сведения, используемые для авторизации создания <paramref name="typeName" />.
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, или значение <see langword="null" />, если объект <paramref name="typeName" /> не найден.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="securityAttributes" /> не является <see langword="null" />.
    ///    Если не включена политика разграничения доступа кода для кода предыдущей версии, <paramref name="securityAttributes" /> должно иметь значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typeName" /> не найден в <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет достаточно разрешений для вызова этого конструктора.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyFile" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Этот экземпляр имеет значение <see langword="null" />.
    /// </exception>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      if (this == null)
        throw new NullReferenceException();
      if (securityAttributes != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      return Activator.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в указанном файле сборки.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя (включая путь) файла, который содержит сборку, определяющую запрошенный тип.
    ///    Эта сборка загружается с помощью метода <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при поиске.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который допускает привязку, приведение типов аргументов, вызов элементов и извлечение объектов <see cref="T:System.Reflection.MemberInfo" /> путем отражения.
    ///    Если параметр <paramref name="binder" /> имеет значение null, то используется модуль привязки по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Аргументы для передачи конструктору.
    ///    Массив аргументов должен соответствовать по числу, порядку и типу параметров вызываемому конструктору.
    ///    Если предпочтителен конструктор по умолчанию, то объект <paramref name="args" /> должен быть пустым массивом или значением null.
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Обычно это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, или значение <see langword="null" />, если объект <paramref name="typeName" /> не найден.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typeName" /> не найден в <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет достаточно разрешений для вызова этого конструктора.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Сборка <paramref name="assemblyFile" /> была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Этот экземпляр имеет значение <see langword="null" />.
    /// </exception>
    public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      if (this == null)
        throw new NullReferenceException();
      return Activator.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
    }

    [SecurityCritical]
    internal ObjectHandle InternalCreateInstanceFromWithNoSecurity(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      PermissionSet.s_fullTrust.Assert();
      return this.CreateInstanceFrom(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
    }

    /// <summary>
    ///   Загружает сборку <see cref="T:System.Reflection.Assembly" />, заданную ее именем <see cref="T:System.Reflection.AssemblyName" />.
    /// </summary>
    /// <param name="assemblyRef">
    ///   Объект, который описывает сборку, подлежащую загрузке.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyRef" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyRef" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyRef" /> не является допустимым именем сборки.
    /// 
    ///   -или-
    /// 
    ///   Сейчас загружена среда CLR 2.0 или более поздней версии. Сборка <paramref name="assemblyRef" /> скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly Load(AssemblyName assemblyRef)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, (Evidence) null, (RuntimeAssembly) null, ref stackMark, true, false, false);
    }

    /// <summary>
    ///   Загружает сборку <see cref="T:System.Reflection.Assembly" />, заданную ее отображаемым именем.
    /// </summary>
    /// <param name="assemblyString">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="assemblyString" /> равно <see langword="null" />
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyString" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyString" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Сейчас загружена среда CLR 2.0 или более поздней версии. Сборка <paramref name="assemblyString" /> скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly Load(string assemblyString)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoad(assemblyString, (Evidence) null, ref stackMark, false);
    }

    /// <summary>
    ///   Загружает объект <see cref="T:System.Reflection.Assembly" /> с образом в формате COFF, содержащим порожденный объект <see cref="T:System.Reflection.Assembly" />.
    /// </summary>
    /// <param name="rawAssembly">
    ///   Массив типа <see langword="byte" />, который является образом в формате COFF, содержащим порожденную сборку.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rawAssembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="rawAssembly" /> не является допустимым именем сборки.
    /// 
    ///   -или-
    /// 
    ///   Сейчас загружена среда CLR 2.0 или более поздней версии. Сборка <paramref name="rawAssembly" /> скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly Load(byte[] rawAssembly)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, (byte[]) null, (Evidence) null, ref stackMark, false, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Загружает объект <see cref="T:System.Reflection.Assembly" /> с образом в формате COFF, содержащим порожденный объект <see cref="T:System.Reflection.Assembly" />.
    ///    Загружаются также необработанные байты, представляющие символы для <see cref="T:System.Reflection.Assembly" />.
    /// </summary>
    /// <param name="rawAssembly">
    ///   Массив типа <see langword="byte" />, который является образом в формате COFF, содержащим порожденную сборку.
    /// </param>
    /// <param name="rawSymbolStore">
    ///   Массив типа <see langword="byte" />, содержащий необработанные байты, которые представляют символы для сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rawAssembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="rawAssembly" /> не является допустимым именем сборки.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="rawAssembly" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, (Evidence) null, ref stackMark, false, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Загружает объект <see cref="T:System.Reflection.Assembly" /> с образом в формате COFF, содержащим порожденный объект <see cref="T:System.Reflection.Assembly" />.
    ///    Загружаются также необработанные байты, представляющие символы для <see cref="T:System.Reflection.Assembly" />.
    /// </summary>
    /// <param name="rawAssembly">
    ///   Массив типа <see langword="byte" />, который является образом в формате COFF, содержащим порожденную сборку.
    /// </param>
    /// <param name="rawSymbolStore">
    ///   Массив типа <see langword="byte" />, содержащий необработанные байты, которые представляют символы для сборки.
    /// </param>
    /// <param name="securityEvidence">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="rawAssembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="rawAssembly" /> не является допустимым именем сборки.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="rawAssembly" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="securityEvidence" /> не <see langword="null" />.
    ///    Если не включена политику разграничения доступа кода прежних версий, <paramref name="securityEvidence" /> должно быть <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkId=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
    public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
    {
      if (securityEvidence != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, securityEvidence, ref stackMark, false, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Загружает сборку <see cref="T:System.Reflection.Assembly" />, заданную ее именем <see cref="T:System.Reflection.AssemblyName" />.
    /// </summary>
    /// <param name="assemblyRef">
    ///   Объект, который описывает сборку, подлежащую загрузке.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="assemblyRef" /> равно <see langword="null" />
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyRef" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Тип <paramref name="assemblyRef" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="assemblyRef" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, assemblySecurity, (RuntimeAssembly) null, ref stackMark, true, false, false);
    }

    /// <summary>
    ///   Загружает сборку <see cref="T:System.Reflection.Assembly" />, заданную ее отображаемым именем.
    /// </summary>
    /// <param name="assemblyString">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="assemblyString" /> равно <see langword="null" />
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyString" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Тип <paramref name="assemblyString" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="assemblyString" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly Load(string assemblyString, Evidence assemblySecurity)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoad(assemblyString, assemblySecurity, ref stackMark, false);
    }

    /// <summary>Выполняет сборку, содержащуюся в указанном файле.</summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, которую необходимо выполнить.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyFile" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Тип <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="assemblyFile" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Указанная сборка имеет точки входа.
    /// </exception>
    public int ExecuteAssembly(string assemblyFile)
    {
      return this.ExecuteAssembly(assemblyFile, (string[]) null);
    }

    /// <summary>
    ///   Выполняет сборку, содержащуюся в указанном файле, с использованием заданного свидетельства.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, которую необходимо выполнить.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyFile" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Тип <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="assemblyFile" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Указанная сборка имеет точки входа.
    /// </exception>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of ExecuteAssembly which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity)
    {
      return this.ExecuteAssembly(assemblyFile, assemblySecurity, (string[]) null);
    }

    /// <summary>
    ///   Выполняет сборку, содержащуюся в указанном файле, с использованием заданного свидетельства и аргументов.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, которую необходимо выполнить.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Предоставленное свидетельство для сборки.
    /// </param>
    /// <param name="args">
    ///   Аргументы, передаваемые в точку входа сборки.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyFile" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Тип <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="assemblyFile" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="assemblySecurity" /> не <see langword="null" />.
    ///    Если не включена политику разграничения доступа кода прежних версий, <paramref name="assemblySecurity" /> должно быть <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Указанная сборка имеет точки входа.
    /// </exception>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of ExecuteAssembly which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args)
    {
      if (assemblySecurity != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.LoadFrom(assemblyFile, assemblySecurity);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>
    ///   Выполняет сборку, содержащуюся в указанном файле, с использованием заданных аргументов.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, которую необходимо выполнить.
    /// </param>
    /// <param name="args">
    ///   Аргументы, передаваемые в точку входа сборки.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyFile" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Тип <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Сборка <paramref name="assemblyFile" /> была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Указанная сборка имеет точки входа.
    /// </exception>
    public int ExecuteAssembly(string assemblyFile, string[] args)
    {
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.LoadFrom(assemblyFile);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>
    ///   Выполняет сборку, содержащуюся в указанном файле, с использованием заданного свидетельства, аргументов, хэш-значения и хэш-алгоритма.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, которую необходимо выполнить.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Предоставленное свидетельство для сборки.
    /// </param>
    /// <param name="args">
    ///   Аргументы, передаваемые в точку входа сборки.
    /// </param>
    /// <param name="hashValue">
    ///   Представляет значение вычисляемого хэш-кода.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Представляет хэш-алгоритм, используемый манифестом сборки.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyFile" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Тип <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="assemblyFile" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="assemblySecurity" /> не <see langword="null" />.
    ///    Если не включена политику разграничения доступа кода прежних версий, <paramref name="assemblySecurity" /> должно быть <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Указанная сборка имеет точки входа.
    /// </exception>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of ExecuteAssembly which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
      if (assemblySecurity != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.LoadFrom(assemblyFile, assemblySecurity, hashValue, hashAlgorithm);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>
    ///   Выполняет сборку, содержащуюся в указанном файле, с использованием заданных аргументов, хэш-значения и хэш-алгоритма.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, которую необходимо выполнить.
    /// </param>
    /// <param name="args">
    ///   Аргументы, передаваемые в точку входа сборки.
    /// </param>
    /// <param name="hashValue">
    ///   Представляет значение вычисляемого хэш-кода.
    /// </param>
    /// <param name="hashAlgorithm">
    ///   Представляет хэш-алгоритм, используемый манифестом сборки.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="assemblyFile" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Тип <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Сборка <paramref name="assemblyFile" /> была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Указанная сборка имеет точки входа.
    /// </exception>
    public int ExecuteAssembly(string assemblyFile, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.LoadFrom(assemblyFile, hashValue, hashAlgorithm);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>
    ///   Выполняет сборку, определенную ее отображаемым именем.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> не найден.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="assemblyName" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> найдена, но не может быть загружен.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Указанная сборка имеет точки входа.
    /// </exception>
    public int ExecuteAssemblyByName(string assemblyName)
    {
      return this.ExecuteAssemblyByName(assemblyName, (string[]) null);
    }

    /// <summary>
    ///   Выполняет сборку с заданным отображаемым именем с использованием заданного свидетельства.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> не найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> найдена, но не может быть загружен.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="assemblyName" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Указанная сборка имеет точки входа.
    /// </exception>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of ExecuteAssemblyByName which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity)
    {
      return this.ExecuteAssemblyByName(assemblyName, assemblySecurity, (string[]) null);
    }

    /// <summary>
    ///   Выполняет сборку с заданным отображаемым именем с использованием заданного свидетельства и аргументов.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <param name="args">
    ///   Аргументы командной строки для передачи при запуске процесса.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> не найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> найдена, но не может быть загружен.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Версии 2.0 или более поздней версии среды CLR в данный момент загружен и <paramref name="assemblyName" /> был скомпилирован в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="assemblySecurity" /> не <see langword="null" />.
    ///    Если не включена политику разграничения доступа кода прежних версий, <paramref name="assemblySecurity" /> должно быть <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Указанная сборка имеет точки входа.
    /// </exception>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of ExecuteAssemblyByName which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity, params string[] args)
    {
      if (assemblySecurity != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.Load(assemblyName, assemblySecurity);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>
    ///   Выполняет сборку с заданным отображаемым именем с использованием заданных аргументов.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="args">
    ///   Аргументы командной строки для передачи при запуске процесса.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> не найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> найдена, но не может быть загружен.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Указанная сборка имеет точки входа.
    /// </exception>
    public int ExecuteAssemblyByName(string assemblyName, params string[] args)
    {
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.Load(assemblyName);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>
    ///   Выполняет сборку с заданным <see cref="T:System.Reflection.AssemblyName" /> с использованием указанного свидетельства и аргументов.
    /// </summary>
    /// <param name="assemblyName">
    ///   Объект <see cref="T:System.Reflection.AssemblyName" />, представляющий имя сборки.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <param name="args">
    ///   Аргументы командной строки для передачи при запуске процесса.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" />, не найдена.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> найдена, но ее невозможно загрузить.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" />, недопустимая.
    /// 
    ///   -или-
    /// 
    ///   Сейчас загружена среда CLR 2.0 или более поздней версии. Сборка <paramref name="assemblyName" /> скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="assemblySecurity" /> не является <see langword="null" />.
    ///    Если не включена политика разграничения доступа кода для кода предыдущей версии, <paramref name="assemblySecurity" /> должно иметь значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Указанная сборка не имеет точку входа.
    /// </exception>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of ExecuteAssemblyByName which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public int ExecuteAssemblyByName(AssemblyName assemblyName, Evidence assemblySecurity, params string[] args)
    {
      if (assemblySecurity != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.Load(assemblyName, assemblySecurity);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>
    ///   Выполняет сборку с заданным <see cref="T:System.Reflection.AssemblyName" />, используя указанные аргументы.
    /// </summary>
    /// <param name="assemblyName">
    ///   Объект <see cref="T:System.Reflection.AssemblyName" />, представляющий имя сборки.
    /// </param>
    /// <param name="args">
    ///   Аргументы командной строки для передачи при запуске процесса.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> не найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> найдена, но не может быть загружен.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка, заданная параметром <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Указанная сборка имеет точки входа.
    /// </exception>
    public int ExecuteAssemblyByName(AssemblyName assemblyName, params string[] args)
    {
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.Load(assemblyName);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>
    ///   Возвращает текущий домен приложения для текущего объекта <see cref="T:System.Threading.Thread" />.
    /// </summary>
    /// <returns>Текущий домен приложения.</returns>
    public static AppDomain CurrentDomain
    {
      get
      {
        return Thread.GetDomain();
      }
    }

    /// <summary>
    ///   Возвращает свидетельство <see cref="T:System.Security.Policy.Evidence" />, связанное с этим доменом приложения.
    /// </summary>
    /// <returns>
    ///   Свидетельство, связанное с данным доменом приложений.
    /// </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    public Evidence Evidence
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, ControlEvidence = true)] get
      {
        return this.EvidenceNoDemand;
      }
    }

    internal Evidence EvidenceNoDemand
    {
      [SecurityCritical] get
      {
        if (this._SecurityIdentity != null)
          return this._SecurityIdentity.Clone();
        if (!this.IsDefaultAppDomain() && this.nIsDefaultAppDomainForEvidence())
          return AppDomain.GetDefaultDomain().Evidence;
        return new Evidence((IRuntimeEvidenceFactory) new AppDomainEvidenceFactory(this));
      }
    }

    internal Evidence InternalEvidence
    {
      get
      {
        return this._SecurityIdentity;
      }
    }

    internal EvidenceBase GetHostEvidence(Type type)
    {
      if (this._SecurityIdentity != null)
        return this._SecurityIdentity.GetHostEvidence(type);
      return new Evidence((IRuntimeEvidenceFactory) new AppDomainEvidenceFactory(this)).GetHostEvidence(type);
    }

    /// <summary>Возвращает понятное имя этого домена приложения.</summary>
    /// <returns>Понятное имя этого домена приложения.</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    public string FriendlyName
    {
      [SecuritySafeCritical] get
      {
        return this.nGetFriendlyName();
      }
    }

    /// <summary>
    ///   Возвращает базовый каталог, в котором распознаватель сборок производит поиск.
    /// </summary>
    /// <returns>
    ///   Базовый каталог, в котором распознаватель сборок производит поиск.
    /// </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    public string BaseDirectory
    {
      get
      {
        return this.FusionStore.ApplicationBase;
      }
    }

    /// <summary>
    ///   Возвращает путь, к каталогу, находящемуся в базовом каталоге, где распознаватель сборок будет производить поиск закрытых сборок.
    /// </summary>
    /// <returns>
    ///   Путь, к каталогу, находящемуся в базовом каталоге, где распознаватель сборок будет производить поиск закрытых сборок.
    /// </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    public string RelativeSearchPath
    {
      get
      {
        return this.FusionStore.PrivateBinPath;
      }
    }

    /// <summary>
    ///   Возвращает указание на то, настроен ли домен приложения для теневого копирования файлов.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если домен приложения настроен для теневого копирования файлов; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    public bool ShadowCopyFiles
    {
      get
      {
        string shadowCopyFiles = this.FusionStore.ShadowCopyFiles;
        return shadowCopyFiles != null && string.Compare(shadowCopyFiles, "true", StringComparison.OrdinalIgnoreCase) == 0;
      }
    }

    /// <summary>
    ///   Получает строку, включающую понятное имя домена приложения и политики контекста.
    /// </summary>
    /// <returns>
    ///   Строка, полученная путем сцепления литеральной строки "Name:", понятного имени домена приложения и либо строкового представления политик контекста, либо строки "Политики контекста отсутствуют".
    /// </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Домен приложения, представленный текущим <see cref="T:System.AppDomain" /> был выгружен.
    /// </exception>
    [SecuritySafeCritical]
    public override string ToString()
    {
      StringBuilder sb = StringBuilderCache.Acquire(16);
      string friendlyName = this.nGetFriendlyName();
      if (friendlyName != null)
      {
        sb.Append(Environment.GetResourceString("Loader_Name") + friendlyName);
        sb.Append(Environment.NewLine);
      }
      if (this._Policies == null || this._Policies.Length == 0)
      {
        sb.Append(Environment.GetResourceString("Loader_NoContextPolicies") + Environment.NewLine);
      }
      else
      {
        sb.Append(Environment.GetResourceString("Loader_ContextPolicies") + Environment.NewLine);
        for (int index = 0; index < this._Policies.Length; ++index)
        {
          sb.Append(this._Policies[index]);
          sb.Append(Environment.NewLine);
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>
    ///   Возвращает сборки, которые были загружены в контекст выполнения этого домена приложения.
    /// </summary>
    /// <returns>Массив сборок в этом домене приложения.</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    public Assembly[] GetAssemblies()
    {
      return this.nGetAssemblies(false);
    }

    /// <summary>
    ///   Возвращает сборки, которые были загружены в контекст, поддерживающий только отражение, домена приложения.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.Assembly" />, представляющих сборки, которые были загружены в контекст домена приложения, поддерживающий только отражение.
    /// </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Попытка выполнить операцию в незагруженном домене приложения.
    /// </exception>
    public Assembly[] ReflectionOnlyGetAssemblies()
    {
      return this.nGetAssemblies(true);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern Assembly[] nGetAssemblies(bool forIntrospection);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern bool IsUnloadingForcedFinalize();

    /// <summary>
    ///   Определяет, выгружен ли этот домен приложения, и были ли закрыты средой CLR объекты, которые он содержал.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если этот домен приложения выгружен, и среда CLR начала вызов методов завершения, в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern bool IsFinalizingForUnload();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void PublishAnonymouslyHostedDynamicMethodsAssembly(RuntimeAssembly assemblyHandle);

    /// <summary>
    ///   Добавляет указанное имя каталога к закрытому списку путей.
    /// </summary>
    /// <param name="path">
    ///   Имя каталога, который следует добавить в закрытый путь.
    /// </param>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecurityCritical]
    [Obsolete("AppDomain.AppendPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void AppendPrivatePath(string path)
    {
      if (path == null || path.Length == 0)
        return;
      string str = this.FusionStore.Value[5];
      StringBuilder sb = StringBuilderCache.Acquire(16);
      if (str != null && str.Length > 0)
      {
        sb.Append(str);
        if ((int) str[str.Length - 1] != (int) Path.PathSeparator && (int) path[0] != (int) Path.PathSeparator)
          sb.Append(Path.PathSeparator);
      }
      sb.Append(path);
      this.InternalSetPrivateBinPath(StringBuilderCache.GetStringAndRelease(sb));
    }

    /// <summary>
    ///   Сбрасывает путь, указывающий на размещение закрытых сборок, присваивая ему пустую строку ("").
    /// </summary>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecurityCritical]
    [Obsolete("AppDomain.ClearPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void ClearPrivatePath()
    {
      this.InternalSetPrivateBinPath(string.Empty);
    }

    /// <summary>
    ///   Сбрасывает список каталогов, содержащих теневые копии сборок, присваивая ему пустую строку ("").
    /// </summary>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecurityCritical]
    [Obsolete("AppDomain.ClearShadowCopyPath has been deprecated. Please investigate the use of AppDomainSetup.ShadowCopyDirectories instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void ClearShadowCopyPath()
    {
      this.InternalSetShadowCopyPath(string.Empty);
    }

    /// <summary>
    ///   Устанавливает заданный путь каталога в качестве места, куда копируются теневые сборки.
    /// </summary>
    /// <param name="path">
    ///   Полный путь к расположению теневых копий.
    /// </param>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecurityCritical]
    [Obsolete("AppDomain.SetCachePath has been deprecated. Please investigate the use of AppDomainSetup.CachePath instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetCachePath(string path)
    {
      this.InternalSetCachePath(path);
    }

    /// <summary>
    ///   Устанавливает заданное значение для свойства указанного домена приложения.
    /// </summary>
    /// <param name="name">
    ///   Имя пользовательского свойства домена приложения, которое требуется создать или изменить.
    /// </param>
    /// <param name="data">Значение свойства.</param>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecurityCritical]
    public void SetData(string name, object data)
    {
      this.SetDataHelper(name, data, (IPermission) null);
    }

    /// <summary>
    ///   Присваивает заданное значение заданному свойству домена приложения с заданными разрешениями, которые нужно запросить у вызывающего кода при извлечении свойства.
    /// </summary>
    /// <param name="name">
    ///   Имя пользовательского свойства домена приложения, которое требуется создать или изменить.
    /// </param>
    /// <param name="data">Значение свойства.</param>
    /// <param name="permission">
    ///   Разрешение, которое нужно запросить у вызывающего кода при извлечении свойства.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="name" /> Задает строковое значение свойства, определяемые системой и <paramref name="permission" /> не <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void SetData(string name, object data, IPermission permission)
    {
      this.SetDataHelper(name, data, permission);
    }

    [SecurityCritical]
    private void SetDataHelper(string name, object data, IPermission permission)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Equals("TargetFrameworkName"))
      {
        this._FusionStore.TargetFrameworkName = (string) data;
      }
      else
      {
        if (name.Equals("IgnoreSystemPolicy"))
        {
          lock (this)
          {
            if (!this._HasSetPolicy)
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_SetData"));
          }
          new PermissionSet(PermissionState.Unrestricted).Demand();
        }
        int index = AppDomainSetup.Locate(name);
        if (index == -1)
        {
          lock (((ICollection) this.LocalStore).SyncRoot)
            this.LocalStore[name] = new object[2]
            {
              data,
              (object) permission
            };
        }
        else
        {
          if (permission != null)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_SetData"));
          switch (index)
          {
            case 2:
              this.FusionStore.DynamicBase = (string) data;
              break;
            case 3:
              this.FusionStore.DeveloperPath = (string) data;
              break;
            case 7:
              this.FusionStore.ShadowCopyDirectories = (string) data;
              break;
            case 11:
              if (data != null)
              {
                this.FusionStore.DisallowPublisherPolicy = true;
                break;
              }
              this.FusionStore.DisallowPublisherPolicy = false;
              break;
            case 12:
              if (data != null)
              {
                this.FusionStore.DisallowCodeDownload = true;
                break;
              }
              this.FusionStore.DisallowCodeDownload = false;
              break;
            case 13:
              if (data != null)
              {
                this.FusionStore.DisallowBindingRedirects = true;
                break;
              }
              this.FusionStore.DisallowBindingRedirects = false;
              break;
            case 14:
              if (data != null)
              {
                this.FusionStore.DisallowApplicationBaseProbing = true;
                break;
              }
              this.FusionStore.DisallowApplicationBaseProbing = false;
              break;
            case 15:
              this.FusionStore.SetConfigurationBytes((byte[]) data);
              break;
            default:
              this.FusionStore.Value[index] = (string) data;
              break;
          }
        }
      }
    }

    /// <summary>
    ///   Возвращает значение, сохраненное в текущем домене приложения для заданного имени.
    /// </summary>
    /// <param name="name">
    ///   Имя предопределенного свойства домена приложения или имя определенного вами свойства домена приложения.
    /// </param>
    /// <returns>
    ///   Значение свойства <paramref name="name" /> или значение <see langword="null" />, если это свойство не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    public object GetData(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      switch (AppDomainSetup.Locate(name))
      {
        case -1:
          if (name.Equals(AppDomainSetup.LoaderOptimizationKey))
            return (object) this.FusionStore.LoaderOptimization;
          object[] objArray;
          lock (((ICollection) this.LocalStore).SyncRoot)
            this.LocalStore.TryGetValue(name, out objArray);
          if (objArray == null)
            return (object) null;
          if (objArray[1] != null)
            ((IPermission) objArray[1]).Demand();
          return objArray[0];
        case 0:
          return (object) this.FusionStore.ApplicationBase;
        case 1:
          return (object) this.FusionStore.ConfigurationFile;
        case 2:
          return (object) this.FusionStore.DynamicBase;
        case 3:
          return (object) this.FusionStore.DeveloperPath;
        case 4:
          return (object) this.FusionStore.ApplicationName;
        case 5:
          return (object) this.FusionStore.PrivateBinPath;
        case 6:
          return (object) this.FusionStore.PrivateBinPathProbe;
        case 7:
          return (object) this.FusionStore.ShadowCopyDirectories;
        case 8:
          return (object) this.FusionStore.ShadowCopyFiles;
        case 9:
          return (object) this.FusionStore.CachePath;
        case 10:
          return (object) this.FusionStore.LicenseFile;
        case 11:
          return (object) this.FusionStore.DisallowPublisherPolicy;
        case 12:
          return (object) this.FusionStore.DisallowCodeDownload;
        case 13:
          return (object) this.FusionStore.DisallowBindingRedirects;
        case 14:
          return (object) this.FusionStore.DisallowApplicationBaseProbing;
        case 15:
          return (object) this.FusionStore.GetConfigurationBytes();
        default:
          return (object) null;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, допускающее значения NULL, которое указывает, установлены ли какие-либо переключатели совместимости и, если установлены, установлен ли заданный переключатель совместимости.
    /// </summary>
    /// <param name="value">
    ///   Проверяемый переключатель совместимости.
    /// </param>
    /// <returns>
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic), если переключатели совместимости не установлены; в противном случае логическое значение, указывающее, установлен ли переключатель совместимости, заданный параметром <paramref name="value" />.
    /// </returns>
    public bool? IsCompatibilitySwitchSet(string value)
    {
      return this._compatFlagsInitialized ? new bool?(this._compatFlags != null && this._compatFlags.ContainsKey(value)) : new bool?();
    }

    /// <summary>Возвращает текущий идентификатор потока.</summary>
    /// <returns>
    ///   32-битовое целое число со знаком, являющееся идентификатором текущего потока.
    /// </returns>
    [Obsolete("AppDomain.GetCurrentThreadId has been deprecated because it does not provide a stable Id when managed threads are running on fibers (aka lightweight threads). To get a stable identifier for a managed thread, use the ManagedThreadId property on Thread.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
    [DllImport("kernel32.dll")]
    public static extern int GetCurrentThreadId();

    /// <summary>Выгружает заданный домен приложения.</summary>
    /// <param name="domain">
    ///   Домен приложения, который нужно выгрузить.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="domain" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.CannotUnloadAppDomainException">
    ///   <paramref name="domain" />не удалось выгрузить.
    /// </exception>
    /// <exception cref="T:System.Exception">
    ///   Произошла ошибка во время выгрузки.
    /// </exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.MayFail)]
    [SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
    public static void Unload(AppDomain domain)
    {
      if (domain == null)
        throw new ArgumentNullException(nameof (domain));
      try
      {
        int idForUnload = AppDomain.GetIdForUnload(domain);
        if (idForUnload == 0)
          throw new CannotUnloadAppDomainException();
        AppDomain.nUnload(idForUnload);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>
    ///   Устанавливает уровень политики безопасности для этого домена приложения.
    /// </summary>
    /// <param name="domainPolicy">Уровень политики безопасности.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="domainPolicy" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">
    ///   Уровень политики безопасности уже установлено.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecurityCritical]
    [Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public void SetAppDomainPolicy(PolicyLevel domainPolicy)
    {
      if (domainPolicy == null)
        throw new ArgumentNullException(nameof (domainPolicy));
      if (!this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      lock (this)
      {
        if (this._HasSetPolicy)
          throw new PolicyException(Environment.GetResourceString("Policy_PolicyAlreadySet"));
        this._HasSetPolicy = true;
        this.nChangeSecurityPolicy();
      }
      SecurityManager.PolicyManager.AddLevel(domainPolicy);
    }

    /// <summary>
    ///   Возвращает контекст активации по умолчанию для текущего домена приложения.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий контекст активации для текущего домена приложения, или значение <see langword="null" />, если этот домен не имеет контекста активации.
    /// </returns>
    public ActivationContext ActivationContext
    {
      [SecurityCritical] get
      {
        return this._activationContext;
      }
    }

    /// <summary>
    ///   Возвращает удостоверение приложения в данном домене приложения.
    /// </summary>
    /// <returns>
    ///   Объект, идентифицирующий приложение в данном домене приложения.
    /// </returns>
    public ApplicationIdentity ApplicationIdentity
    {
      [SecurityCritical] get
      {
        return this._applicationIdentity;
      }
    }

    /// <summary>
    ///   Возвращает информацию, описывающую разрешения, предоставленные приложению, и то, имеет ли приложение уровень доверия, позволяющий ему выполняться.
    /// </summary>
    /// <returns>
    ///   Объект, инкапсулирующий сведения о разрешениях и доверии для приложения в домене приложения.
    /// </returns>
    public ApplicationTrust ApplicationTrust
    {
      [SecurityCritical] get
      {
        if (this._applicationTrust == null && this._IsFastFullTrustDomain)
          this._applicationTrust = new ApplicationTrust(new PermissionSet(PermissionState.Unrestricted));
        return this._applicationTrust;
      }
    }

    /// <summary>
    ///   Задает объект-участник по умолчанию, который необходимо присоединять к потокам, в случае если они пытаются выполнить привязку к объекту-участнику во время выполнения в этом домене приложения.
    /// </summary>
    /// <param name="principal">
    ///   Объект-участник, который необходимо подключить к потоку.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="principal" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">
    ///   Основной поток уже установлено.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public void SetThreadPrincipal(IPrincipal principal)
    {
      if (principal == null)
        throw new ArgumentNullException(nameof (principal));
      lock (this)
      {
        if (this._DefaultPrincipal != null)
          throw new PolicyException(Environment.GetResourceString("Policy_PrincipalTwice"));
        this._DefaultPrincipal = principal;
      }
    }

    /// <summary>
    ///   Указывает, как участники и объекты удостоверений должны присоединяться к потоку, если поток пытается выполнить привязку к участнику при выполнении в этом домене приложения.
    /// </summary>
    /// <param name="policy">
    ///   Одно из значений <see cref="T:System.Security.Principal.PrincipalPolicy" />, определяющее тип объекта-участника, который необходимо подключить к потоку.
    /// </param>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public void SetPrincipalPolicy(PrincipalPolicy policy)
    {
      this._PrincipalPolicy = policy;
    }

    /// <summary>
    ///   Предоставляет объекту <see cref="T:System.AppDomain" /> бесконечное время существования, предотвращая создание аренды.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecurityCritical]
    public override object InitializeLifetimeService()
    {
      return (object) null;
    }

    /// <summary>
    ///   Выполняет код в другом домене приложения, который определен заданным делегатом.
    /// </summary>
    /// <param name="callBackDelegate">
    ///   Делегат, определяющий вызываемый метод.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="callBackDelegate" /> имеет значение <see langword="null" />.
    /// </exception>
    public void DoCallBack(CrossAppDomainDelegate callBackDelegate)
    {
      if (callBackDelegate == null)
        throw new ArgumentNullException(nameof (callBackDelegate));
      callBackDelegate();
    }

    /// <summary>
    ///   Возвращает каталог, в котором распознаватель сборок производит поиск динамически созданных сборок.
    /// </summary>
    /// <returns>
    ///   Каталог, в котором распознаватель сборок производит поиск динамически созданных сборок.
    /// </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    public string DynamicDirectory
    {
      [SecuritySafeCritical] get
      {
        string dynamicDir = this.GetDynamicDir();
        if (dynamicDir != null)
          FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, dynamicDir, false, true);
        return dynamicDir;
      }
    }

    /// <summary>
    ///   Создает новый домен приложения с заданным именем с помощью предоставленного свидетельства.
    /// </summary>
    /// <param name="friendlyName">
    ///   Понятное имя домена.
    ///    Это понятное имя может отображаться в пользовательском интерфейсе для определения домена.
    ///    Для получения дополнительной информации см. <see cref="P:System.AppDomain.FriendlyName" />.
    /// </param>
    /// <param name="securityInfo">
    ///   Свидетельство, идентифицирующее код, который выполняется в домене приложения.
    ///    Передайте значение <see langword="null" /> для использования свидетельства текущего домена приложения.
    /// </param>
    /// <returns>Вновь созданный домен приложения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="friendlyName" /> имеет значение <see langword="null" />.
    /// </exception>
    public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo)
    {
      return AppDomain.CreateDomain(friendlyName, securityInfo, (AppDomainSetup) null);
    }

    /// <summary>
    ///   Создает новый домен приложения с заданным именем с использованием свидетельства, пути к базовой папке приложения, относительного пути поиска и параметра, указывающего, должна ли загружаться теневая копия сборки в домен приложения.
    /// </summary>
    /// <param name="friendlyName">
    ///   Понятное имя домена.
    ///    Это понятное имя может отображаться в пользовательском интерфейсе для определения домена.
    ///    Для получения дополнительной информации см. <see cref="P:System.AppDomain.FriendlyName" />.
    /// </param>
    /// <param name="securityInfo">
    ///   Свидетельство, идентифицирующее код, который выполняется в домене приложения.
    ///    Передайте значение <see langword="null" /> для использования свидетельства текущего домена приложения.
    /// </param>
    /// <param name="appBasePath">
    ///   Базовый каталог, в котором распознаватель сборок производит поиск.
    ///    Для получения дополнительной информации см. <see cref="P:System.AppDomain.BaseDirectory" />.
    /// </param>
    /// <param name="appRelativeSearchPath">
    ///   Путь, соответствующий базовому каталогу, в котором распознаватель сборок будет производить поиск закрытых сборок.
    ///    Для получения дополнительной информации см. <see cref="P:System.AppDomain.RelativeSearchPath" />.
    /// </param>
    /// <param name="shadowCopyFiles">
    ///   Если значение <see langword="true" />, теневая копия сборки загружается в этот домен приложения.
    /// </param>
    /// <returns>Вновь созданный домен приложения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="friendlyName" /> имеет значение <see langword="null" />.
    /// </exception>
    public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles)
    {
      AppDomainSetup info = new AppDomainSetup();
      info.ApplicationBase = appBasePath;
      info.PrivateBinPath = appRelativeSearchPath;
      if (shadowCopyFiles)
        info.ShadowCopyFiles = "true";
      return AppDomain.CreateDomain(friendlyName, securityInfo, info);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern string GetDynamicDir();

    /// <summary>Создает новый домен приложения с заданным именем.</summary>
    /// <param name="friendlyName">Понятное имя домена.</param>
    /// <returns>Вновь созданный домен приложения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="friendlyName" /> имеет значение <see langword="null" />.
    /// </exception>
    public static AppDomain CreateDomain(string friendlyName)
    {
      return AppDomain.CreateDomain(friendlyName, (Evidence) null, (AppDomainSetup) null);
    }

    [SecurityCritical]
    private static byte[] MarshalObject(object o)
    {
      CodeAccessPermission.Assert(true);
      return AppDomain.Serialize(o);
    }

    [SecurityCritical]
    private static byte[] MarshalObjects(object o1, object o2, out byte[] blob2)
    {
      CodeAccessPermission.Assert(true);
      byte[] numArray = AppDomain.Serialize(o1);
      blob2 = AppDomain.Serialize(o2);
      return numArray;
    }

    [SecurityCritical]
    private static object UnmarshalObject(byte[] blob)
    {
      CodeAccessPermission.Assert(true);
      return AppDomain.Deserialize(blob);
    }

    [SecurityCritical]
    private static object UnmarshalObjects(byte[] blob1, byte[] blob2, out object o2)
    {
      CodeAccessPermission.Assert(true);
      object obj = AppDomain.Deserialize(blob1);
      o2 = AppDomain.Deserialize(blob2);
      return obj;
    }

    [SecurityCritical]
    private static byte[] Serialize(object o)
    {
      if (o == null)
        return (byte[]) null;
      if (o is ISecurityEncodable)
      {
        SecurityElement xml = ((ISecurityEncodable) o).ToXml();
        MemoryStream memoryStream = new MemoryStream(4096);
        memoryStream.WriteByte((byte) 0);
        StreamWriter writer = new StreamWriter((Stream) memoryStream, Encoding.UTF8);
        xml.ToWriter(writer);
        writer.Flush();
        return memoryStream.ToArray();
      }
      MemoryStream stm = new MemoryStream();
      stm.WriteByte((byte) 1);
      CrossAppDomainSerializer.SerializeObject(o, stm);
      return stm.ToArray();
    }

    [SecurityCritical]
    private static object Deserialize(byte[] blob)
    {
      if (blob == null)
        return (object) null;
      if (blob[0] == (byte) 0)
      {
        SecurityElement topElement = new Parser(blob, Tokenizer.ByteTokenEncoding.UTF8Tokens, 1).GetTopElement();
        if (topElement.Tag.Equals("IPermission") || topElement.Tag.Equals("Permission"))
        {
          IPermission permission = XMLUtil.CreatePermission(topElement, PermissionState.None, false);
          if (permission == null)
            return (object) null;
          permission.FromXml(topElement);
          return (object) permission;
        }
        if (topElement.Tag.Equals("PermissionSet"))
        {
          PermissionSet permissionSet = new PermissionSet();
          permissionSet.FromXml(topElement, false, false);
          return (object) permissionSet;
        }
        if (!topElement.Tag.Equals("PermissionToken"))
          return (object) null;
        PermissionToken permissionToken = new PermissionToken();
        permissionToken.FromXml(topElement);
        return (object) permissionToken;
      }
      using (MemoryStream stm = new MemoryStream(blob, 1, blob.Length - 1))
        return CrossAppDomainSerializer.DeserializeObject(stm);
    }

    [SecurityCritical]
    internal static void Pause()
    {
      AppDomainPauseManager.Instance.Pausing();
      AppDomainPauseManager.Instance.Paused();
    }

    [SecurityCritical]
    internal static void Resume()
    {
      if (!AppDomainPauseManager.IsPaused)
        return;
      AppDomainPauseManager.Instance.Resuming();
      AppDomainPauseManager.Instance.Resumed();
    }

    private AppDomain()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Constructor"));
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern int _nExecuteAssembly(RuntimeAssembly assembly, string[] args);

    internal int nExecuteAssembly(RuntimeAssembly assembly, string[] args)
    {
      return this._nExecuteAssembly(assembly, args);
    }

    internal void CreateRemotingData()
    {
      lock (this)
      {
        if (this._RemotingData != null)
          return;
        this._RemotingData = new DomainSpecificRemotingData();
      }
    }

    internal DomainSpecificRemotingData RemotingData
    {
      get
      {
        if (this._RemotingData == null)
          this.CreateRemotingData();
        return this._RemotingData;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern string nGetFriendlyName();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern bool nIsDefaultAppDomainForEvidence();

    /// <summary>
    ///   Происходит при завершении работы родительского процесса домена приложения по умолчанию.
    /// </summary>
    public event EventHandler ProcessExit
    {
      [SecuritySafeCritical] add
      {
        if (value == null)
          return;
        RuntimeHelpers.PrepareContractedDelegate((Delegate) value);
        lock (this)
          this._processExit += value;
      }
      remove
      {
        lock (this)
          this._processExit -= value;
      }
    }

    /// <summary>
    ///   Происходит непосредственно перед выгрузкой объекта <see cref="T:System.AppDomain" />.
    /// </summary>
    public event EventHandler DomainUnload
    {
      [SecuritySafeCritical] add
      {
        if (value == null)
          return;
        RuntimeHelpers.PrepareContractedDelegate((Delegate) value);
        lock (this)
          this._domainUnload += value;
      }
      remove
      {
        lock (this)
          this._domainUnload -= value;
      }
    }

    /// <summary>
    ///   Происходит, если выброшенное исключение остается не перехваченным.
    /// </summary>
    public event UnhandledExceptionEventHandler UnhandledException
    {
      [SecurityCritical] add
      {
        if (value == null)
          return;
        RuntimeHelpers.PrepareContractedDelegate((Delegate) value);
        lock (this)
          this._unhandledException += value;
      }
      [SecurityCritical] remove
      {
        lock (this)
          this._unhandledException -= value;
      }
    }

    /// <summary>
    ///   Происходит при создании исключения в управляемом коде, перед тем как среда выполнения ищет стек вызовов для обработчика исключений в домене приложений.
    /// </summary>
    public event EventHandler<FirstChanceExceptionEventArgs> FirstChanceException
    {
      [SecurityCritical] add
      {
        if (value == null)
          return;
        RuntimeHelpers.PrepareContractedDelegate((Delegate) value);
        lock (this)
          this._firstChanceException += value;
      }
      [SecurityCritical] remove
      {
        lock (this)
          this._firstChanceException -= value;
      }
    }

    private void OnAssemblyLoadEvent(RuntimeAssembly LoadedAssembly)
    {
      // ISSUE: reference to a compiler-generated field
      AssemblyLoadEventHandler assemblyLoad = this.AssemblyLoad;
      if (assemblyLoad == null)
        return;
      AssemblyLoadEventArgs args = new AssemblyLoadEventArgs((Assembly) LoadedAssembly);
      assemblyLoad((object) this, args);
    }

    [SecurityCritical]
    private RuntimeAssembly OnResourceResolveEvent(RuntimeAssembly assembly, string resourceName)
    {
      ResolveEventHandler resourceResolve = this._ResourceResolve;
      if (resourceResolve == null)
        return (RuntimeAssembly) null;
      Delegate[] invocationList = resourceResolve.GetInvocationList();
      int length = invocationList.Length;
      for (int index = 0; index < length; ++index)
      {
        RuntimeAssembly runtimeAssembly = AppDomain.GetRuntimeAssembly(((ResolveEventHandler) invocationList[index])((object) this, new ResolveEventArgs(resourceName, (Assembly) assembly)));
        if ((Assembly) runtimeAssembly != (Assembly) null)
          return runtimeAssembly;
      }
      return (RuntimeAssembly) null;
    }

    [SecurityCritical]
    private RuntimeAssembly OnTypeResolveEvent(RuntimeAssembly assembly, string typeName)
    {
      ResolveEventHandler typeResolve = this._TypeResolve;
      if (typeResolve == null)
        return (RuntimeAssembly) null;
      Delegate[] invocationList = typeResolve.GetInvocationList();
      int length = invocationList.Length;
      for (int index = 0; index < length; ++index)
      {
        RuntimeAssembly runtimeAssembly = AppDomain.GetRuntimeAssembly(((ResolveEventHandler) invocationList[index])((object) this, new ResolveEventArgs(typeName, (Assembly) assembly)));
        if ((Assembly) runtimeAssembly != (Assembly) null)
          return runtimeAssembly;
      }
      return (RuntimeAssembly) null;
    }

    [SecurityCritical]
    private RuntimeAssembly OnAssemblyResolveEvent(RuntimeAssembly assembly, string assemblyFullName)
    {
      ResolveEventHandler assemblyResolve = this._AssemblyResolve;
      if (assemblyResolve == null)
        return (RuntimeAssembly) null;
      Delegate[] invocationList = assemblyResolve.GetInvocationList();
      int length = invocationList.Length;
      for (int index = 0; index < length; ++index)
      {
        RuntimeAssembly runtimeAssembly = AppDomain.GetRuntimeAssembly(((ResolveEventHandler) invocationList[index])((object) this, new ResolveEventArgs(assemblyFullName, (Assembly) assembly)));
        if ((Assembly) runtimeAssembly != (Assembly) null)
          return runtimeAssembly;
      }
      return (RuntimeAssembly) null;
    }

    private RuntimeAssembly OnReflectionOnlyAssemblyResolveEvent(RuntimeAssembly assembly, string assemblyFullName)
    {
      // ISSUE: reference to a compiler-generated field
      ResolveEventHandler onlyAssemblyResolve = this.ReflectionOnlyAssemblyResolve;
      if (onlyAssemblyResolve != null)
      {
        Delegate[] invocationList = onlyAssemblyResolve.GetInvocationList();
        int length = invocationList.Length;
        for (int index = 0; index < length; ++index)
        {
          RuntimeAssembly runtimeAssembly = AppDomain.GetRuntimeAssembly(((ResolveEventHandler) invocationList[index])((object) this, new ResolveEventArgs(assemblyFullName, (Assembly) assembly)));
          if ((Assembly) runtimeAssembly != (Assembly) null)
            return runtimeAssembly;
        }
      }
      return (RuntimeAssembly) null;
    }

    private RuntimeAssembly[] OnReflectionOnlyNamespaceResolveEvent(RuntimeAssembly assembly, string namespaceName)
    {
      return WindowsRuntimeMetadata.OnReflectionOnlyNamespaceResolveEvent(this, assembly, namespaceName);
    }

    private string[] OnDesignerNamespaceResolveEvent(string namespaceName)
    {
      return WindowsRuntimeMetadata.OnDesignerNamespaceResolveEvent(this, namespaceName);
    }

    internal AppDomainSetup FusionStore
    {
      get
      {
        return this._FusionStore;
      }
    }

    internal static RuntimeAssembly GetRuntimeAssembly(Assembly asm)
    {
      if (asm == (Assembly) null)
        return (RuntimeAssembly) null;
      RuntimeAssembly runtimeAssembly = asm as RuntimeAssembly;
      if ((Assembly) runtimeAssembly != (Assembly) null)
        return runtimeAssembly;
      AssemblyBuilder assemblyBuilder = asm as AssemblyBuilder;
      if ((Assembly) assemblyBuilder != (Assembly) null)
        return (RuntimeAssembly) assemblyBuilder.InternalAssembly;
      return (RuntimeAssembly) null;
    }

    private Dictionary<string, object[]> LocalStore
    {
      get
      {
        if (this._LocalStore != null)
          return this._LocalStore;
        this._LocalStore = new Dictionary<string, object[]>();
        return this._LocalStore;
      }
    }

    private void TurnOnBindingRedirects()
    {
      this._FusionStore.DisallowBindingRedirects = false;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal static int GetIdForUnload(AppDomain domain)
    {
      if (RemotingServices.IsTransparentProxy((object) domain))
        return RemotingServices.GetServerDomainIdForProxy((object) domain);
      return domain.Id;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsDomainIdValid(int id);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern AppDomain GetDefaultDomain();

    internal IPrincipal GetThreadPrincipal()
    {
      IPrincipal principal;
      if (this._DefaultPrincipal == null)
      {
        switch (this._PrincipalPolicy)
        {
          case PrincipalPolicy.UnauthenticatedPrincipal:
            principal = (IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity("", ""), new string[1]
            {
              ""
            });
            break;
          case PrincipalPolicy.NoPrincipal:
            principal = (IPrincipal) null;
            break;
          case PrincipalPolicy.WindowsPrincipal:
            principal = (IPrincipal) new WindowsPrincipal(WindowsIdentity.GetCurrent());
            break;
          default:
            principal = (IPrincipal) null;
            break;
        }
      }
      else
        principal = this._DefaultPrincipal;
      return principal;
    }

    [SecurityCritical]
    internal void CreateDefaultContext()
    {
      lock (this)
      {
        if (this._DefaultContext != null)
          return;
        this._DefaultContext = Context.CreateDefaultContext();
      }
    }

    [SecurityCritical]
    internal Context GetDefaultContext()
    {
      if (this._DefaultContext == null)
        this.CreateDefaultContext();
      return this._DefaultContext;
    }

    [SecuritySafeCritical]
    internal static void CheckDomainCreationEvidence(AppDomainSetup creationDomainSetup, Evidence creationEvidence)
    {
      if (creationEvidence == null || AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled || creationDomainSetup != null && creationDomainSetup.ApplicationTrust != null)
        return;
      Zone hostEvidence1 = AppDomain.CurrentDomain.EvidenceNoDemand.GetHostEvidence<Zone>();
      SecurityZone securityZone = hostEvidence1 != null ? hostEvidence1.SecurityZone : SecurityZone.MyComputer;
      Zone hostEvidence2 = creationEvidence.GetHostEvidence<Zone>();
      if (hostEvidence2 != null && hostEvidence2.SecurityZone != securityZone && hostEvidence2.SecurityZone != SecurityZone.MyComputer)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
    }

    /// <summary>
    ///   Создает новый домен приложения с использованием заданного имени, свидетельства и сведений об установке домена приложения.
    /// </summary>
    /// <param name="friendlyName">
    ///   Понятное имя домена.
    ///    Это понятное имя может отображаться в пользовательском интерфейсе для определения домена.
    ///    Для получения дополнительной информации см. <see cref="P:System.AppDomain.FriendlyName" />.
    /// </param>
    /// <param name="securityInfo">
    ///   Свидетельство, идентифицирующее код, который выполняется в домене приложения.
    ///    Передайте значение <see langword="null" /> для использования свидетельства текущего домена приложения.
    /// </param>
    /// <param name="info">
    ///   Объект, в котором содержатся сведения об инициализации домена приложения.
    /// </param>
    /// <returns>Вновь созданный домен приложения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="friendlyName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
    public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info)
    {
      return AppDomain.InternalCreateDomain(friendlyName, securityInfo, info);
    }

    [SecurityCritical]
    internal static AppDomain InternalCreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info)
    {
      if (friendlyName == null)
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_String"));
      AppDomain.CheckCreateDomainSupported();
      if (info == null)
        info = new AppDomainSetup();
      if (info.TargetFrameworkName == null)
        info.TargetFrameworkName = AppDomain.CurrentDomain.GetTargetFrameworkName();
      AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
      if (domainManager != null)
        return domainManager.CreateDomain(friendlyName, securityInfo, info);
      if (securityInfo != null)
      {
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
        AppDomain.CheckDomainCreationEvidence(info, securityInfo);
      }
      return AppDomain.nCreateDomain(friendlyName, info, securityInfo, securityInfo == null ? AppDomain.CurrentDomain.InternalEvidence : (Evidence) null, AppDomain.CurrentDomain.GetSecurityDescriptor());
    }

    /// <summary>
    ///   Создает новый домен приложения с использованием заданного имени, свидетельства, сведений об установке домена приложения, используемого по умолчанию набора разрешений и массива сборок с полным доверием.
    /// </summary>
    /// <param name="friendlyName">
    ///   Понятное имя домена.
    ///    Это понятное имя может отображаться в пользовательском интерфейсе для определения домена.
    ///    Дополнительные сведения см. в описании <see cref="P:System.AppDomain.FriendlyName" />.
    /// </param>
    /// <param name="securityInfo">
    ///   Свидетельство, идентифицирующее код, который выполняется в домене приложения.
    ///    Передайте значение <see langword="null" /> для использования свидетельства текущего домена приложения.
    /// </param>
    /// <param name="info">
    ///   Объект, в котором содержатся сведения об инициализации домена приложения.
    /// </param>
    /// <param name="grantSet">
    ///   Набор разрешений по умолчанию, который предоставляется всем сборкам, загружаемым в новый домен приложения, который не имеет специальные разрешения.
    /// </param>
    /// <param name="fullTrustAssemblies">
    ///   Массив строгих имен, представляющих сборки, которые будут считаться обладающими полным доверием в новом домене приложения.
    /// </param>
    /// <returns>Вновь созданный домен приложения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="friendlyName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Домен приложения, <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <see cref="P:System.AppDomainSetup.ApplicationBase" /> Не установлено свойство <see cref="T:System.AppDomainSetup" /> объекта, предоставленного для <paramref name="info" />.
    /// </exception>
    public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info, PermissionSet grantSet, params StrongName[] fullTrustAssemblies)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      if (info.ApplicationBase == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AppDomainSandboxAPINeedsExplicitAppBase"));
      if (fullTrustAssemblies == null)
        fullTrustAssemblies = new StrongName[0];
      info.ApplicationTrust = new ApplicationTrust(grantSet, (IEnumerable<StrongName>) fullTrustAssemblies);
      return AppDomain.CreateDomain(friendlyName, securityInfo, info);
    }

    /// <summary>
    ///   Создает новый домен приложения с заданным именем с использованием свидетельства, пути к базовой папке приложения, относительного пути поиска и параметра, указывающего, должна ли загружаться теневая копия сборки в домен приложения.
    ///    Задает метод обратного вызова, вызываемый, когда инициализируется домен приложения, и массив строковых аргументов для передачи методу обратного вызова.
    /// </summary>
    /// <param name="friendlyName">
    ///   Понятное имя домена.
    ///    Это понятное имя может отображаться в пользовательском интерфейсе для определения домена.
    ///    Для получения дополнительной информации см. <see cref="P:System.AppDomain.FriendlyName" />.
    /// </param>
    /// <param name="securityInfo">
    ///   Свидетельство, идентифицирующее код, который выполняется в домене приложения.
    ///    Передайте значение <see langword="null" /> для использования свидетельства текущего домена приложения.
    /// </param>
    /// <param name="appBasePath">
    ///   Базовый каталог, в котором распознаватель сборок производит поиск.
    ///    Для получения дополнительной информации см. <see cref="P:System.AppDomain.BaseDirectory" />.
    /// </param>
    /// <param name="appRelativeSearchPath">
    ///   Путь, соответствующий базовому каталогу, в котором распознаватель сборок будет производить поиск закрытых сборок.
    ///    Для получения дополнительной информации см. <see cref="P:System.AppDomain.RelativeSearchPath" />.
    /// </param>
    /// <param name="shadowCopyFiles">
    ///   Значение <see langword="true" /> для загрузки теневой копии сборки в этот домен приложения.
    /// </param>
    /// <param name="adInit">
    ///   Делегат <see cref="T:System.AppDomainInitializer" />, представляющий метод обратного вызова, вызываемый при инициализации нового объекта <see cref="T:System.AppDomain" />.
    /// </param>
    /// <param name="adInitArgs">
    ///   Массив строковых аргументов для передачи в обратный вызов, представленный объектом <paramref name="adInit" />, когда инициализируется новый объект <see cref="T:System.AppDomain" />.
    /// </param>
    /// <returns>Вновь созданный домен приложения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="friendlyName" /> имеет значение <see langword="null" />.
    /// </exception>
    public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles, AppDomainInitializer adInit, string[] adInitArgs)
    {
      AppDomainSetup info = new AppDomainSetup();
      info.ApplicationBase = appBasePath;
      info.PrivateBinPath = appRelativeSearchPath;
      info.AppDomainInitializer = adInit;
      info.AppDomainInitializerArguments = adInitArgs;
      if (shadowCopyFiles)
        info.ShadowCopyFiles = "true";
      return AppDomain.CreateDomain(friendlyName, securityInfo, info);
    }

    [SecurityCritical]
    private void SetupFusionStore(AppDomainSetup info, AppDomainSetup oldInfo)
    {
      this._FusionStore = info;
      if (oldInfo == null)
      {
        if (info.Value[0] == null || info.Value[1] == null)
        {
          AppDomain defaultDomain = AppDomain.GetDefaultDomain();
          if (this == defaultDomain)
          {
            info.SetupDefaults(RuntimeEnvironment.GetModuleFileName(), true);
          }
          else
          {
            if (info.Value[1] == null)
              info.ConfigurationFile = defaultDomain.FusionStore.Value[1];
            if (info.Value[0] == null)
              info.ApplicationBase = defaultDomain.FusionStore.Value[0];
            if (info.Value[4] == null)
              info.ApplicationName = defaultDomain.FusionStore.Value[4];
          }
        }
        if (info.Value[5] == null)
          info.PrivateBinPath = Environment.nativeGetEnvironmentVariable(AppDomainSetup.PrivateBinPathEnvironmentVariable);
        if (info.DeveloperPath == null)
          info.DeveloperPath = RuntimeEnvironment.GetDeveloperPath();
      }
      IntPtr fusionContext = this.GetFusionContext();
      info.SetupFusionContext(fusionContext, oldInfo);
      if (info.LoaderOptimization == LoaderOptimization.NotSpecified && (oldInfo == null || info.LoaderOptimization == oldInfo.LoaderOptimization))
        return;
      this.UpdateLoaderOptimization(info.LoaderOptimization);
    }

    private static void RunInitializer(AppDomainSetup setup)
    {
      if (setup.AppDomainInitializer == null)
        return;
      string[] args = (string[]) null;
      if (setup.AppDomainInitializerArguments != null)
        args = (string[]) setup.AppDomainInitializerArguments.Clone();
      setup.AppDomainInitializer(args);
    }

    [SecurityCritical]
    private static object PrepareDataForSetup(string friendlyName, AppDomainSetup setup, Evidence providedSecurityInfo, Evidence creatorsSecurityInfo, IntPtr parentSecurityDescriptor, string sandboxName, string[] propertyNames, string[] propertyValues)
    {
      byte[] numArray = (byte[]) null;
      bool flag = false;
      AppDomain.EvidenceCollection evidenceCollection = (AppDomain.EvidenceCollection) null;
      if (providedSecurityInfo != null || creatorsSecurityInfo != null)
      {
        HostSecurityManager hostSecurityManager = AppDomain.CurrentDomain.DomainManager != null ? AppDomain.CurrentDomain.DomainManager.HostSecurityManager : (HostSecurityManager) null;
        if (hostSecurityManager == null || !(hostSecurityManager.GetType() != typeof (HostSecurityManager)) || (hostSecurityManager.Flags & HostSecurityManagerOptions.HostAppDomainEvidence) != HostSecurityManagerOptions.HostAppDomainEvidence)
        {
          if (providedSecurityInfo != null && providedSecurityInfo.IsUnmodified && (providedSecurityInfo.Target != null && providedSecurityInfo.Target is AppDomainEvidenceFactory))
          {
            providedSecurityInfo = (Evidence) null;
            flag = true;
          }
          if (creatorsSecurityInfo != null && creatorsSecurityInfo.IsUnmodified && (creatorsSecurityInfo.Target != null && creatorsSecurityInfo.Target is AppDomainEvidenceFactory))
          {
            creatorsSecurityInfo = (Evidence) null;
            flag = true;
          }
        }
      }
      if (providedSecurityInfo != null || creatorsSecurityInfo != null)
      {
        evidenceCollection = new AppDomain.EvidenceCollection();
        evidenceCollection.ProvidedSecurityInfo = providedSecurityInfo;
        evidenceCollection.CreatorsSecurityInfo = creatorsSecurityInfo;
      }
      if (evidenceCollection != null)
        numArray = CrossAppDomainSerializer.SerializeObject((object) evidenceCollection).GetBuffer();
      AppDomainInitializerInfo domainInitializerInfo = (AppDomainInitializerInfo) null;
      if (setup != null && setup.AppDomainInitializer != null)
        domainInitializerInfo = new AppDomainInitializerInfo(setup.AppDomainInitializer);
      AppDomainSetup appDomainSetup = new AppDomainSetup(setup, false);
      return (object) new object[9]
      {
        (object) friendlyName,
        (object) appDomainSetup,
        (object) parentSecurityDescriptor,
        (object) flag,
        (object) numArray,
        (object) domainInitializerInfo,
        (object) sandboxName,
        (object) propertyNames,
        (object) propertyValues
      };
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static object Setup(object arg)
    {
      object[] objArray = (object[]) arg;
      string friendlyName = (string) objArray[0];
      AppDomainSetup copy = (AppDomainSetup) objArray[1];
      IntPtr parentSecurityDescriptor = (IntPtr) objArray[2];
      bool generateDefaultEvidence = (bool) objArray[3];
      byte[] buffer = (byte[]) objArray[4];
      AppDomainInitializerInfo domainInitializerInfo = (AppDomainInitializerInfo) objArray[5];
      string str1 = (string) objArray[6];
      string[] strArray1 = (string[]) objArray[7];
      string[] strArray2 = (string[]) objArray[8];
      Evidence providedSecurityInfo = (Evidence) null;
      Evidence creatorsSecurityInfo = (Evidence) null;
      AppDomain currentDomain = AppDomain.CurrentDomain;
      AppDomainSetup info = new AppDomainSetup(copy, false);
      if (strArray1 != null && strArray2 != null)
      {
        for (int index = 0; index < strArray1.Length; ++index)
        {
          if (strArray1[index] == "APPBASE")
          {
            if (strArray2[index] == null)
              throw new ArgumentNullException("APPBASE");
            if (Path.IsRelative(strArray2[index]))
              throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
            info.ApplicationBase = AppDomain.NormalizePath(strArray2[index], true);
          }
          else if (strArray1[index] == "LOCATION_URI" && providedSecurityInfo == null)
          {
            providedSecurityInfo = new Evidence();
            providedSecurityInfo.AddHostEvidence<Url>(new Url(strArray2[index]));
            currentDomain.SetDataHelper(strArray1[index], (object) strArray2[index], (IPermission) null);
          }
          else if (strArray1[index] == "LOADER_OPTIMIZATION")
          {
            if (strArray2[index] == null)
              throw new ArgumentNullException("LOADER_OPTIMIZATION");
            string str2 = strArray2[index];
            if (!(str2 == "SingleDomain"))
            {
              if (!(str2 == "MultiDomain"))
              {
                if (!(str2 == "MultiDomainHost"))
                {
                  if (!(str2 == "NotSpecified"))
                    throw new ArgumentException(Environment.GetResourceString("Argument_UnrecognizedLoaderOptimization"), "LOADER_OPTIMIZATION");
                  info.LoaderOptimization = LoaderOptimization.NotSpecified;
                }
                else
                  info.LoaderOptimization = LoaderOptimization.MultiDomainHost;
              }
              else
                info.LoaderOptimization = LoaderOptimization.MultiDomain;
            }
            else
              info.LoaderOptimization = LoaderOptimization.SingleDomain;
          }
        }
      }
      AppDomainSortingSetupInfo sortingSetupInfo = info._AppDomainSortingSetupInfo;
      if (sortingSetupInfo != null && (sortingSetupInfo._pfnIsNLSDefinedString == IntPtr.Zero || sortingSetupInfo._pfnCompareStringEx == IntPtr.Zero || (sortingSetupInfo._pfnLCMapStringEx == IntPtr.Zero || sortingSetupInfo._pfnFindNLSStringEx == IntPtr.Zero) || (sortingSetupInfo._pfnCompareStringOrdinal == IntPtr.Zero || sortingSetupInfo._pfnGetNLSVersionEx == IntPtr.Zero)) && (!(sortingSetupInfo._pfnIsNLSDefinedString == IntPtr.Zero) || !(sortingSetupInfo._pfnCompareStringEx == IntPtr.Zero) || (!(sortingSetupInfo._pfnLCMapStringEx == IntPtr.Zero) || !(sortingSetupInfo._pfnFindNLSStringEx == IntPtr.Zero)) || (!(sortingSetupInfo._pfnCompareStringOrdinal == IntPtr.Zero) || !(sortingSetupInfo._pfnGetNLSVersionEx == IntPtr.Zero))))
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_NotAllCustomSortingFuncsDefined"));
      currentDomain.SetupFusionStore(info, (AppDomainSetup) null);
      AppDomainSetup fusionStore = currentDomain.FusionStore;
      if (buffer != null)
      {
        AppDomain.EvidenceCollection evidenceCollection = (AppDomain.EvidenceCollection) CrossAppDomainSerializer.DeserializeObject(new MemoryStream(buffer));
        providedSecurityInfo = evidenceCollection.ProvidedSecurityInfo;
        creatorsSecurityInfo = evidenceCollection.CreatorsSecurityInfo;
      }
      currentDomain.nSetupFriendlyName(friendlyName);
      if (copy != null && copy.SandboxInterop)
        currentDomain.nSetDisableInterfaceCache();
      if (fusionStore.AppDomainManagerAssembly != null && fusionStore.AppDomainManagerType != null)
        currentDomain.SetAppDomainManagerType(fusionStore.AppDomainManagerAssembly, fusionStore.AppDomainManagerType);
      currentDomain.PartialTrustVisibleAssemblies = fusionStore.PartialTrustVisibleAssemblies;
      currentDomain.CreateAppDomainManager();
      currentDomain.InitializeDomainSecurity(providedSecurityInfo, creatorsSecurityInfo, generateDefaultEvidence, parentSecurityDescriptor, true);
      if (domainInitializerInfo != null)
        fusionStore.AppDomainInitializer = domainInitializerInfo.Unwrap();
      AppDomain.RunInitializer(fusionStore);
      ObjectHandle objectHandle = (ObjectHandle) null;
      if (fusionStore.ActivationArguments != null && fusionStore.ActivationArguments.ActivateInstance)
        objectHandle = Activator.CreateInstance(currentDomain.ActivationContext);
      return (object) RemotingServices.MarshalInternal((MarshalByRefObject) objectHandle, (string) null, (Type) null);
    }

    [SecuritySafeCritical]
    internal static string NormalizePath(string path, bool fullCheck)
    {
      return Path.LegacyNormalizePath(path, fullCheck, 260, true);
    }

    [SecuritySafeCritical]
    [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
    private bool IsAssemblyOnAptcaVisibleList(RuntimeAssembly assembly)
    {
      if (this._aptcaVisibleAssemblies == null)
        return false;
      return Array.BinarySearch<string>(this._aptcaVisibleAssemblies, assembly.GetName().GetNameWithPublicKey().ToUpperInvariant(), (IComparer<string>) StringComparer.OrdinalIgnoreCase) >= 0;
    }

    [SecurityCritical]
    private unsafe bool IsAssemblyOnAptcaVisibleListRaw(char* namePtr, int nameLen, byte* keyTokenPtr, int keyTokenLen)
    {
      if (this._aptcaVisibleAssemblies == null)
        return false;
      string str = new string(namePtr, 0, nameLen);
      byte[] publicKeyToken = new byte[keyTokenLen];
      for (int index = 0; index < publicKeyToken.Length; ++index)
        publicKeyToken[index] = keyTokenPtr[index];
      AssemblyName assemblyName = new AssemblyName();
      assemblyName.Name = str;
      assemblyName.SetPublicKeyToken(publicKeyToken);
      try
      {
        return Array.BinarySearch((Array) this._aptcaVisibleAssemblies, (object) assemblyName, (IComparer) new AppDomain.CAPTCASearcher()) >= 0;
      }
      catch (InvalidOperationException ex)
      {
        return false;
      }
    }

    [SecurityCritical]
    private void SetupDomain(bool allowRedirects, string path, string configFile, string[] propertyNames, string[] propertyValues)
    {
      lock (this)
      {
        if (this._FusionStore != null)
          return;
        AppDomainSetup info = new AppDomainSetup();
        info.SetupDefaults(RuntimeEnvironment.GetModuleFileName(), true);
        if (path != null)
          info.Value[0] = path;
        if (configFile != null)
          info.Value[1] = configFile;
        if (!allowRedirects)
          info.DisallowBindingRedirects = true;
        if (propertyNames != null)
        {
          for (int index = 0; index < propertyNames.Length; ++index)
          {
            if (string.Equals(propertyNames[index], "PARTIAL_TRUST_VISIBLE_ASSEMBLIES", StringComparison.Ordinal) && propertyValues[index] != null)
            {
              if (propertyValues[index].Length > 0)
                info.PartialTrustVisibleAssemblies = propertyValues[index].Split(';');
              else
                info.PartialTrustVisibleAssemblies = new string[0];
            }
          }
        }
        this.PartialTrustVisibleAssemblies = info.PartialTrustVisibleAssemblies;
        this.SetupFusionStore(info, (AppDomainSetup) null);
      }
    }

    [SecurityCritical]
    private void SetupLoaderOptimization(LoaderOptimization policy)
    {
      if (policy == LoaderOptimization.NotSpecified)
        return;
      this.FusionStore.LoaderOptimization = policy;
      this.UpdateLoaderOptimization(this.FusionStore.LoaderOptimization);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern IntPtr GetFusionContext();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern IntPtr GetSecurityDescriptor();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern AppDomain nCreateDomain(string friendlyName, AppDomainSetup setup, Evidence providedSecurityInfo, Evidence creatorsSecurityInfo, IntPtr parentSecurityDescriptor);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern ObjRef nCreateInstance(string friendlyName, AppDomainSetup setup, Evidence providedSecurityInfo, Evidence creatorsSecurityInfo, IntPtr parentSecurityDescriptor);

    [SecurityCritical]
    private void SetupDomainSecurity(Evidence appDomainEvidence, IntPtr creatorsSecurityDescriptor, bool publishAppDomain)
    {
      Evidence o = appDomainEvidence;
      AppDomain.SetupDomainSecurity(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Evidence>(ref o), creatorsSecurityDescriptor, publishAppDomain);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetupDomainSecurity(AppDomainHandle appDomain, ObjectHandleOnStack appDomainEvidence, IntPtr creatorsSecurityDescriptor, [MarshalAs(UnmanagedType.Bool)] bool publishAppDomain);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void nSetupFriendlyName(string friendlyName);

    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void nSetDisableInterfaceCache();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern void UpdateLoaderOptimization(LoaderOptimization optimization);

    /// <summary>
    ///   Устанавливает заданный путь каталога в качестве места для теневого копирования сборок.
    /// </summary>
    /// <param name="path">
    ///   Список имен каталогов, разделенных точкой с запятой.
    /// </param>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecurityCritical]
    [Obsolete("AppDomain.SetShadowCopyPath has been deprecated. Please investigate the use of AppDomainSetup.ShadowCopyDirectories instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetShadowCopyPath(string path)
    {
      this.InternalSetShadowCopyPath(path);
    }

    /// <summary>Включает теневое копирование.</summary>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecurityCritical]
    [Obsolete("AppDomain.SetShadowCopyFiles has been deprecated. Please investigate the use of AppDomainSetup.ShadowCopyFiles instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetShadowCopyFiles()
    {
      this.InternalSetShadowCopyFiles();
    }

    /// <summary>
    ///   Устанавливает заданный путь каталога в качестве базового каталога для подкаталогов, в которых сохраняются и становятся доступными динамически созданные файлы.
    /// </summary>
    /// <param name="path">
    ///   Полный путь, который является базовым каталогом для подкаталогов, в которых хранятся динамические сборки.
    /// </param>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    [SecurityCritical]
    [Obsolete("AppDomain.SetDynamicBase has been deprecated. Please investigate the use of AppDomainSetup.DynamicBase instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetDynamicBase(string path)
    {
      this.InternalSetDynamicBase(path);
    }

    /// <summary>
    ///   Возвращает сведения о конфигурации домена приложения для этого экземпляра.
    /// </summary>
    /// <returns>Сведения об инициализации домена приложения.</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    public AppDomainSetup SetupInformation
    {
      get
      {
        return new AppDomainSetup(this.FusionStore, true);
      }
    }

    [SecurityCritical]
    internal void InternalSetShadowCopyPath(string path)
    {
      if (path != null)
        AppDomainSetup.UpdateContextProperty(this.GetFusionContext(), AppDomainSetup.ShadowCopyDirectoriesKey, (object) path);
      this.FusionStore.ShadowCopyDirectories = path;
    }

    [SecurityCritical]
    internal void InternalSetShadowCopyFiles()
    {
      AppDomainSetup.UpdateContextProperty(this.GetFusionContext(), AppDomainSetup.ShadowCopyFilesKey, (object) "true");
      this.FusionStore.ShadowCopyFiles = "true";
    }

    [SecurityCritical]
    internal void InternalSetCachePath(string path)
    {
      this.FusionStore.CachePath = path;
      if (this.FusionStore.Value[9] == null)
        return;
      AppDomainSetup.UpdateContextProperty(this.GetFusionContext(), AppDomainSetup.CachePathKey, (object) this.FusionStore.Value[9]);
    }

    [SecurityCritical]
    internal void InternalSetPrivateBinPath(string path)
    {
      AppDomainSetup.UpdateContextProperty(this.GetFusionContext(), AppDomainSetup.PrivateBinPathKey, (object) path);
      this.FusionStore.PrivateBinPath = path;
    }

    [SecurityCritical]
    internal void InternalSetDynamicBase(string path)
    {
      this.FusionStore.DynamicBase = path;
      if (this.FusionStore.Value[2] == null)
        return;
      AppDomainSetup.UpdateContextProperty(this.GetFusionContext(), AppDomainSetup.DynamicBaseKey, (object) this.FusionStore.Value[2]);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern string IsStringInterned(string str);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern string GetOrInternString(string str);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetGrantSet(AppDomainHandle domain, ObjectHandleOnStack retGrantSet);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetIsLegacyCasPolicyEnabled(AppDomainHandle domain);

    /// <summary>
    ///   Возвращает набор разрешений изолированного домена приложения.
    /// </summary>
    /// <returns>Набор разрешений изолированного домена приложения.</returns>
    public PermissionSet PermissionSet
    {
      [SecurityCritical] get
      {
        PermissionSet o = (PermissionSet) null;
        AppDomain.GetGrantSet(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o));
        if (o != null)
          return o.Copy();
        return new PermissionSet(PermissionState.Unrestricted);
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, выполняются ли сборки, загруженные в текущий домен приложения, с полным доверием.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если сборки, загруженные в текущий домен приложения, выполняются с полным доверием; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsFullyTrusted
    {
      [SecuritySafeCritical] get
      {
        PermissionSet o = (PermissionSet) null;
        AppDomain.GetGrantSet(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o));
        if (o != null)
          return o.IsUnrestricted();
        return true;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, имеет ли текущий домен приложения набор разрешений, которые предоставляются всем сборкам, загружаемым в домен приложения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий домен приложения имеет однородный набор разрешений; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsHomogenous
    {
      get
      {
        if (!this._IsFastFullTrustDomain)
          return this._applicationTrust != null;
        return true;
      }
    }

    internal bool IsLegacyCasPolicyEnabled
    {
      [SecuritySafeCritical] get
      {
        return AppDomain.GetIsLegacyCasPolicyEnabled(this.GetNativeHandle());
      }
    }

    [SecuritySafeCritical]
    internal PermissionSet GetHomogenousGrantSet(Evidence evidence)
    {
      if (this._IsFastFullTrustDomain)
        return new PermissionSet(PermissionState.Unrestricted);
      if (evidence.GetDelayEvaluatedHostEvidence<StrongName>() != null)
      {
        foreach (StrongName fullTrustAssembly in (IEnumerable<StrongName>) this.ApplicationTrust.FullTrustAssemblies)
        {
          StrongNameMembershipCondition membershipCondition = new StrongNameMembershipCondition(fullTrustAssembly.PublicKey, fullTrustAssembly.Name, fullTrustAssembly.Version);
          object usedEvidence = (object) null;
          if (((IReportMatchMembershipCondition) membershipCondition).Check(evidence, out usedEvidence))
          {
            IDelayEvaluatedEvidence evaluatedEvidence = usedEvidence as IDelayEvaluatedEvidence;
            if (usedEvidence != null)
              evaluatedEvidence.MarkUsed();
            return new PermissionSet(PermissionState.Unrestricted);
          }
        }
      }
      return this.ApplicationTrust.DefaultGrantSet.PermissionSet.Copy();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void nChangeSecurityPolicy();

    [SecurityCritical]
    [ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void nUnload(int domainInternal);

    /// <summary>
    ///   Создает новый экземпляр заданного типа.
    ///    Параметры задают сборку, где определен тип, и имя типа.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <returns>
    ///   Экземпляр объекта, заданного параметром <paramref name="typeName" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assemblyName" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось найти <paramref name="typename" /> в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    public object CreateInstanceAndUnwrap(string assemblyName, string typeName)
    {
      return this.CreateInstance(assemblyName, typeName)?.Unwrap();
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа.
    ///    Параметры задают сборку, где определен тип, имя типа и массив атрибутов активации.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Обычно это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом. Активация клиента — это устаревшая технология, которая сохраняется для обеспечения обратной совместимости, но не рекомендуется для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Экземпляр объекта, заданного параметром <paramref name="typeName" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assemblyName" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось найти <paramref name="typename" /> в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    public object CreateInstanceAndUnwrap(string assemblyName, string typeName, object[] activationAttributes)
    {
      return this.CreateInstance(assemblyName, typeName, activationAttributes)?.Unwrap();
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа.
    ///    Параметры определяют имя типа, а также способ его поиска и создания.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при поиске.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see cref="T:System.Reflection.MemberInfo" /> с помощью отражения.
    ///    Если параметр <paramref name="binder" /> имеет значение null, то используется модуль привязки по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Аргументы для передачи конструктору.
    ///    Массив аргументов должен соответствовать по числу, порядку и типу параметров вызываемому конструктору.
    ///    Если предпочтителен конструктор по умолчанию, то объект <paramref name="args" /> должен быть пустым массивом или значением null.
    /// </param>
    /// <param name="culture">
    ///   Объект, зависящий от языка и региональных параметров, который используется для управления приведением типов.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see langword="CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Обычно это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <param name="securityAttributes">
    ///   Сведения, используемые для авторизации создания <paramref name="typeName" />.
    /// </param>
    /// <returns>
    ///   Экземпляр объекта, заданного параметром <paramref name="typeName" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assemblyName" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typename" /> не найден в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceAndUnwrap which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      return this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes)?.Unwrap();
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в заданной сборке, указывая, игнорируется ли регистр имени типа; атрибуты привязки и средство привязки, используемые для выбора создаваемого типа; аргументы конструктора; язык и региональные параметры; и атрибуты активации.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при поиске.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see cref="T:System.Reflection.MemberInfo" /> с помощью отражения.
    ///    Если параметр <paramref name="binder" /> имеет значение null, то используется модуль привязки по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Аргументы для передачи конструктору.
    ///    Массив аргументов должен соответствовать по числу, порядку и типу параметров вызываемому конструктору.
    ///    Если предпочтителен конструктор по умолчанию, то объект <paramref name="args" /> должен быть пустым массивом или значением null.
    /// </param>
    /// <param name="culture">
    ///   Объект, зависящий от языка и региональных параметров, который используется для управления приведением типов.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see langword="CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, массив, который содержит единственный объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    ///    который определяет URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Экземпляр объекта, заданного параметром <paramref name="typeName" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assemblyName" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typename" /> не найден в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      return this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes)?.Unwrap();
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в указанном файле сборки.
    /// </summary>
    /// <param name="assemblyName">
    ///   Имя файла и путь сборки, которая определяет запрошенный тип.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <returns>
    ///   Запрашиваемый объект или значение <see langword="null" />, если объект <paramref name="typeName" /> не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyName" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typeName" /> не найден в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Найден открытый конструктор без параметров.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет достаточно разрешений для вызова этого конструктора.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName)
    {
      return this.CreateInstanceFrom(assemblyName, typeName)?.Unwrap();
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в указанном файле сборки.
    /// </summary>
    /// <param name="assemblyName">
    ///   Имя файла и путь сборки, которая определяет запрошенный тип.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Обычно это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом. Активация клиента — это устаревшая технология, которая сохраняется для обеспечения обратной совместимости, но не рекомендуется для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Запрашиваемый объект или значение <see langword="null" />, если объект <paramref name="typeName" /> не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyName" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typeName" /> не найден в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Найден открытый конструктор без параметров.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет достаточно разрешений для вызова этого конструктора.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, object[] activationAttributes)
    {
      return this.CreateInstanceFrom(assemblyName, typeName, activationAttributes)?.Unwrap();
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в указанном файле сборки.
    /// </summary>
    /// <param name="assemblyName">
    ///   Имя файла и путь сборки, которая определяет запрошенный тип.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при поиске.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который допускает привязку, приведение типов аргументов, вызов элементов и извлечение объектов <see cref="T:System.Reflection.MemberInfo" /> путем отражения.
    ///    Если параметр <paramref name="binder" /> имеет значение null, то используется модуль привязки по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Аргументы для передачи конструктору.
    ///    Массив аргументов должен соответствовать по числу, порядку и типу параметров вызываемому конструктору.
    ///    Если предпочтителен конструктор по умолчанию, то объект <paramref name="args" /> должен быть пустым массивом или значением null.
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Обычно это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <param name="securityAttributes">
    ///   Сведения, используемые для авторизации создания <paramref name="typeName" />.
    /// </param>
    /// <returns>
    ///   Запрашиваемый объект или значение <see langword="null" />, если объект <paramref name="typeName" /> не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyName" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typeName" /> не найден в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет достаточно разрешений для вызова этого конструктора.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В настоящий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка <paramref name="assemblyName" /> была скомпилирована в более поздней версии.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFromAndUnwrap which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      return this.CreateInstanceFrom(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes)?.Unwrap();
    }

    /// <summary>
    ///   Создает новый экземпляр заданного типа, определенного в заданном файле сборки, указывая, игнорируется ли регистр имени типа; атрибуты привязки и средство привязки, используемые для выбора создаваемого типа; аргументы конструктора; язык и региональные параметры; и атрибуты активации.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла и путь сборки, которая определяет запрошенный тип.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при поиске.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который допускает привязку, приведение типов аргументов, вызов элементов и извлечение объектов <see cref="T:System.Reflection.MemberInfo" /> путем отражения.
    ///    Если параметр <paramref name="binder" /> имеет значение null, то используется модуль привязки по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Аргументы для передачи конструктору.
    ///    Массив аргументов должен соответствовать по числу, порядку и типу параметров вызываемому конструктору.
    ///    Если предпочтителен конструктор по умолчанию, то объект <paramref name="args" /> должен быть пустым массивом или значением null.
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Обычно это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Запрашиваемый объект или значение <see langword="null" />, если объект <paramref name="typeName" /> не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyName" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.AppDomainUnloadedException">
    ///   Предпринята попытка выполнения операции с выгруженным доменом приложения.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typeName" /> не найден в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет достаточно разрешений для вызова этого конструктора.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   Сборка <paramref name="assemblyName" /> скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// </exception>
    public object CreateInstanceFromAndUnwrap(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      return this.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes)?.Unwrap();
    }

    /// <summary>
    ///   Возвращает целое число, однозначно идентифицирующее домен приложения в процессе.
    /// </summary>
    /// <returns>Целое число, идентифицирующее домен приложения.</returns>
    public int Id
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this.GetId();
      }
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern int GetId();

    /// <summary>
    ///   Возвращает значение, указывающее, является ли домен приложения используемым по умолчанию доменом для процесса.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see cref="T:System.AppDomain" /> представляет используемый по умолчанию домен приложения для процесса; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsDefaultAppDomain()
    {
      return this.GetId() == 1;
    }

    private static AppDomainSetup InternalCreateDomainSetup(string imageLocation)
    {
      int num = imageLocation.LastIndexOf('\\');
      AppDomainSetup appDomainSetup = new AppDomainSetup();
      appDomainSetup.ApplicationBase = imageLocation.Substring(0, num + 1);
      StringBuilder stringBuilder = new StringBuilder(imageLocation.Substring(num + 1));
      stringBuilder.Append(AppDomainSetup.ConfigurationExtension);
      appDomainSetup.ConfigurationFile = stringBuilder.ToString();
      return appDomainSetup;
    }

    private static AppDomain InternalCreateDomain(string imageLocation)
    {
      return AppDomain.CreateDomain("Validator", (Evidence) null, AppDomain.InternalCreateDomainSetup(imageLocation));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void nEnableMonitoring();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool nMonitoringIsEnabled();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern long nGetTotalProcessorTime();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern long nGetTotalAllocatedMemorySize();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern long nGetLastSurvivedMemorySize();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern long nGetLastSurvivedProcessMemorySize();

    /// <summary>
    ///   Возвращает или задает значение, указывающее, включен ли мониторинг ЦП и памяти доменов приложений для текущего процесса.
    ///    После того, как мониторинг для процесса включен, отключить его невозможно.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если мониторинг включен; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Текущий процесс попытался присвоить значение <see langword="false" /> этому свойству.
    /// </exception>
    public static bool MonitoringIsEnabled
    {
      [SecurityCritical] get
      {
        return AppDomain.nMonitoringIsEnabled();
      }
      [SecurityCritical] set
      {
        if (!value)
          throw new ArgumentException(Environment.GetResourceString("Arg_MustBeTrue"));
        AppDomain.nEnableMonitoring();
      }
    }

    /// <summary>
    ///   Возвращает общее процессорное время, использованное всеми потоками при выполнении в текущем домене приложения с момента запуска процесса.
    /// </summary>
    /// <returns>
    ///   Общее процессорное время для текущего домена приложения.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства <see langword="static" /> (<see langword="Shared" /> в Visual Basic) <see cref="P:System.AppDomain.MonitoringIsEnabled" /> задано значение <see langword="false" />.
    /// </exception>
    public TimeSpan MonitoringTotalProcessorTime
    {
      [SecurityCritical] get
      {
        long totalProcessorTime = this.nGetTotalProcessorTime();
        if (totalProcessorTime == -1L)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WithoutARM"));
        return new TimeSpan(totalProcessorTime);
      }
    }

    /// <summary>
    ///   Возвращает общий размер в байтах для всех операций выделения памяти, выполненных доменом приложения с момента его создания, без вычитания собранной памяти.
    /// </summary>
    /// <returns>Общий размер для всех операций выделения памяти.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства <see langword="static" /> (<see langword="Shared" /> в Visual Basic) <see cref="P:System.AppDomain.MonitoringIsEnabled" /> задано значение <see langword="false" />.
    /// </exception>
    public long MonitoringTotalAllocatedMemorySize
    {
      [SecurityCritical] get
      {
        long allocatedMemorySize = this.nGetTotalAllocatedMemorySize();
        if (allocatedMemorySize == -1L)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WithoutARM"));
        return allocatedMemorySize;
      }
    }

    /// <summary>
    ///   Возвращает количество байтов, оставшихся после последнего сбора, и про которые известно, что на них ссылается текущий домен приложения.
    /// </summary>
    /// <returns>Количество оставшихся байтов.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства <see langword="static" /> (<see langword="Shared" /> в Visual Basic) <see cref="P:System.AppDomain.MonitoringIsEnabled" /> задано значение <see langword="false" />.
    /// </exception>
    public long MonitoringSurvivedMemorySize
    {
      [SecurityCritical] get
      {
        long survivedMemorySize = this.nGetLastSurvivedMemorySize();
        if (survivedMemorySize == -1L)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WithoutARM"));
        return survivedMemorySize;
      }
    }

    /// <summary>
    ///   Возвращает общее количество байтов, оставшихся после последнего сбора для всех доменов приложений в процессе.
    /// </summary>
    /// <returns>Общее количество оставшихся байтов для процесса.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для свойства <see langword="static" /> (<see langword="Shared" /> в Visual Basic) <see cref="P:System.AppDomain.MonitoringIsEnabled" /> задано значение <see langword="false" />.
    /// </exception>
    public static long MonitoringSurvivedProcessMemorySize
    {
      [SecurityCritical] get
      {
        long processMemorySize = AppDomain.nGetLastSurvivedProcessMemorySize();
        if (processMemorySize == -1L)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WithoutARM"));
        return processMemorySize;
      }
    }

    [SecurityCritical]
    private void InternalSetDomainContext(string imageLocation)
    {
      this.SetupFusionStore(AppDomain.InternalCreateDomainSetup(imageLocation), (AppDomainSetup) null);
    }

    /// <summary>Возвращает тип текущего экземпляра.</summary>
    /// <returns>Тип текущего экземпляра.</returns>
    [__DynamicallyInvokable]
    public new Type GetType()
    {
      return base.GetType();
    }

    void _AppDomain.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _AppDomain.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _AppDomain.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _AppDomain.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    [System.Flags]
    private enum APPX_FLAGS
    {
      APPX_FLAGS_INITIALIZED = 1,
      APPX_FLAGS_APPX_MODEL = 2,
      APPX_FLAGS_APPX_DESIGN_MODE = 4,
      APPX_FLAGS_APPX_NGEN = 8,
      APPX_FLAGS_APPX_MASK = APPX_FLAGS_APPX_NGEN | APPX_FLAGS_APPX_DESIGN_MODE | APPX_FLAGS_APPX_MODEL, // 0x0000000E
      APPX_FLAGS_API_CHECK = 16, // 0x00000010
    }

    private class NamespaceResolverForIntrospection
    {
      private IEnumerable<string> _packageGraphFilePaths;

      public NamespaceResolverForIntrospection(IEnumerable<string> packageGraphFilePaths)
      {
        this._packageGraphFilePaths = packageGraphFilePaths;
      }

      [SecurityCritical]
      public void ResolveNamespace(object sender, NamespaceResolveEventArgs args)
      {
        foreach (string assemblyFile in WindowsRuntimeMetadata.ResolveNamespace(args.NamespaceName, (string) null, this._packageGraphFilePaths))
          args.ResolvedAssemblies.Add(Assembly.ReflectionOnlyLoadFrom(assemblyFile));
      }
    }

    [Serializable]
    private class EvidenceCollection
    {
      public Evidence ProvidedSecurityInfo;
      public Evidence CreatorsSecurityInfo;
    }

    private class CAPTCASearcher : IComparer
    {
      int IComparer.Compare(object lhs, object rhs)
      {
        AssemblyName assemblyName1 = new AssemblyName((string) lhs);
        AssemblyName assemblyName2 = (AssemblyName) rhs;
        int num1 = string.Compare(assemblyName1.Name, assemblyName2.Name, StringComparison.OrdinalIgnoreCase);
        if (num1 != 0)
          return num1;
        byte[] publicKeyToken1 = assemblyName1.GetPublicKeyToken();
        byte[] publicKeyToken2 = assemblyName2.GetPublicKeyToken();
        if (publicKeyToken1 == null)
          return -1;
        if (publicKeyToken2 == null)
          return 1;
        if (publicKeyToken1.Length < publicKeyToken2.Length)
          return -1;
        if (publicKeyToken1.Length > publicKeyToken2.Length)
          return 1;
        for (int index = 0; index < publicKeyToken1.Length; ++index)
        {
          byte num2 = publicKeyToken1[index];
          byte num3 = publicKeyToken2[index];
          if ((int) num2 < (int) num3)
            return -1;
          if ((int) num2 > (int) num3)
            return 1;
        }
        return 0;
      }
    }
  }
}
