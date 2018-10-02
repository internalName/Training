// Decompiled with JetBrains decompiler
// Type: System.Reflection.RuntimeAssembly
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Reflection
{
  [Serializable]
  internal class RuntimeAssembly : Assembly, ICustomQueryInterface
  {
    private static string[] s_unsafeFrameworkAssemblyNames = new string[2]
    {
      "System.Reflection.Context",
      "Microsoft.VisualBasic"
    };
    private const uint COR_E_LOADING_REFERENCE_ASSEMBLY = 2148733016;
    private string m_fullname;
    private object m_syncRoot;
    private IntPtr m_assembly;
    private RuntimeAssembly.ASSEMBLY_FLAGS m_flags;
    private const string s_localFilePrefix = "file:";

    [SecurityCritical]
    CustomQueryInterfaceResult ICustomQueryInterface.GetInterface([In] ref Guid iid, out IntPtr ppv)
    {
      if (iid == typeof (NativeMethods.IDispatch).GUID)
      {
        ppv = Marshal.GetComInterfaceForObject((object) this, typeof (_Assembly));
        return CustomQueryInterfaceResult.Handled;
      }
      ppv = IntPtr.Zero;
      return CustomQueryInterfaceResult.NotHandled;
    }

    internal RuntimeAssembly()
    {
      throw new NotSupportedException();
    }

    private event ModuleResolveEventHandler _ModuleResolve;

    internal int InvocableAttributeCtorToken
    {
      get
      {
        return (int) (this.Flags & RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_TOKEN_MASK | RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_FRAMEWORK | RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_SAFE_REFLECTION);
      }
    }

    private RuntimeAssembly.ASSEMBLY_FLAGS Flags
    {
      [SecuritySafeCritical] get
      {
        if ((this.m_flags & RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_INITIALIZED) == RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_UNKNOWN)
        {
          RuntimeAssembly.ASSEMBLY_FLAGS assemblyFlags = RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_UNKNOWN;
          if (RuntimeAssembly.IsFrameworkAssembly(this.GetName()))
          {
            assemblyFlags |= RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_FRAMEWORK | RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_SAFE_REFLECTION;
            foreach (string frameworkAssemblyName in RuntimeAssembly.s_unsafeFrameworkAssemblyNames)
            {
              if (string.Compare(this.GetSimpleName(), frameworkAssemblyName, StringComparison.OrdinalIgnoreCase) == 0)
              {
                assemblyFlags &= ~RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_SAFE_REFLECTION;
                break;
              }
            }
            Type type = this.GetType("__DynamicallyInvokableAttribute", false);
            if (type != (Type) null)
            {
              int metadataToken = type.GetConstructor(Type.EmptyTypes).MetadataToken;
              assemblyFlags |= (RuntimeAssembly.ASSEMBLY_FLAGS) (metadataToken & 16777215);
            }
          }
          else if (this.IsDesignerBindingContext())
            assemblyFlags = RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_SAFE_REFLECTION;
          this.m_flags = assemblyFlags | RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_INITIALIZED;
        }
        return this.m_flags;
      }
    }

    internal object SyncRoot
    {
      get
      {
        if (this.m_syncRoot == null)
          Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), (object) null);
        return this.m_syncRoot;
      }
    }

    public override event ModuleResolveEventHandler ModuleResolve
    {
      [SecurityCritical] add
      {
        this._ModuleResolve += value;
      }
      [SecurityCritical] remove
      {
        this._ModuleResolve -= value;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetCodeBase(RuntimeAssembly assembly, bool copiedName, StringHandleOnStack retString);

    [SecurityCritical]
    internal string GetCodeBase(bool copiedName)
    {
      string s = (string) null;
      RuntimeAssembly.GetCodeBase(this.GetNativeHandle(), copiedName, JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }

    public override string CodeBase
    {
      [SecuritySafeCritical] get
      {
        string codeBase = this.GetCodeBase(false);
        this.VerifyCodeBaseDiscovery(codeBase);
        return codeBase;
      }
    }

    internal RuntimeAssembly GetNativeHandle()
    {
      return this;
    }

    [SecuritySafeCritical]
    public override AssemblyName GetName(bool copiedName)
    {
      AssemblyName assemblyName = new AssemblyName();
      string codeBase = this.GetCodeBase(copiedName);
      this.VerifyCodeBaseDiscovery(codeBase);
      assemblyName.Init(this.GetSimpleName(), this.GetPublicKey(), (byte[]) null, this.GetVersion(), this.GetLocale(), this.GetHashAlgorithm(), AssemblyVersionCompatibility.SameMachine, codeBase, this.GetFlags() | AssemblyNameFlags.PublicKey, (StrongNameKeyPair) null);
      Module manifestModule = this.ManifestModule;
      if (manifestModule != (Module) null && manifestModule.MDStreamVersion > 65536)
      {
        PortableExecutableKinds peKind;
        ImageFileMachine machine;
        this.ManifestModule.GetPEKind(out peKind, out machine);
        assemblyName.SetProcArchIndex(peKind, machine);
      }
      return assemblyName;
    }

    [SecurityCritical]
    [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
    private string GetNameForConditionalAptca()
    {
      return this.GetName().GetNameWithPublicKey();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetFullName(RuntimeAssembly assembly, StringHandleOnStack retString);

    public override string FullName
    {
      [SecuritySafeCritical] get
      {
        if (this.m_fullname == null)
        {
          string s = (string) null;
          RuntimeAssembly.GetFullName(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref s));
          Interlocked.CompareExchange<string>(ref this.m_fullname, s, (string) null);
        }
        return this.m_fullname;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetEntryPoint(RuntimeAssembly assembly, ObjectHandleOnStack retMethod);

    public override MethodInfo EntryPoint
    {
      [SecuritySafeCritical] get
      {
        IRuntimeMethodInfo o = (IRuntimeMethodInfo) null;
        RuntimeAssembly.GetEntryPoint(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref o));
        if (o == null)
          return (MethodInfo) null;
        return (MethodInfo) RuntimeType.GetMethodBase(o);
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetType(RuntimeAssembly assembly, string name, bool throwOnError, bool ignoreCase, ObjectHandleOnStack type);

    [SecuritySafeCritical]
    public override Type GetType(string name, bool throwOnError, bool ignoreCase)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      RuntimeType o = (RuntimeType) null;
      RuntimeAssembly.GetType(this.GetNativeHandle(), name, throwOnError, ignoreCase, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return (Type) o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void GetForwardedTypes(RuntimeAssembly assembly, ObjectHandleOnStack retTypes);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetExportedTypes(RuntimeAssembly assembly, ObjectHandleOnStack retTypes);

    [SecuritySafeCritical]
    public override Type[] GetExportedTypes()
    {
      Type[] o = (Type[]) null;
      RuntimeAssembly.GetExportedTypes(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref o));
      return o;
    }

    public override IEnumerable<TypeInfo> DefinedTypes
    {
      [SecuritySafeCritical] get
      {
        List<RuntimeType> runtimeTypeList = new List<RuntimeType>();
        foreach (RuntimeModule runtimeModule in this.GetModulesInternal(true, false))
          runtimeTypeList.AddRange((IEnumerable<RuntimeType>) runtimeModule.GetDefinedTypes());
        return (IEnumerable<TypeInfo>) runtimeTypeList.ToArray();
      }
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public override Stream GetManifestResourceStream(Type type, string name)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.GetManifestResourceStream(type, name, false, ref stackMark);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public override Stream GetManifestResourceStream(string name)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.GetManifestResourceStream(name, ref stackMark, false);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetEvidence(RuntimeAssembly assembly, ObjectHandleOnStack retEvidence);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern SecurityRuleSet GetSecurityRuleSet(RuntimeAssembly assembly);

    public override Evidence Evidence
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, ControlEvidence = true)] get
      {
        return this.EvidenceNoDemand.Clone();
      }
    }

    internal Evidence EvidenceNoDemand
    {
      [SecurityCritical] get
      {
        Evidence o = (Evidence) null;
        RuntimeAssembly.GetEvidence(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Evidence>(ref o));
        return o;
      }
    }

    public override PermissionSet PermissionSet
    {
      [SecurityCritical] get
      {
        PermissionSet newGrant = (PermissionSet) null;
        PermissionSet newDenied = (PermissionSet) null;
        this.GetGrantSet(out newGrant, out newDenied);
        if (newGrant != null)
          return newGrant.Copy();
        return new PermissionSet(PermissionState.Unrestricted);
      }
    }

    public override SecurityRuleSet SecurityRuleSet
    {
      [SecuritySafeCritical] get
      {
        return RuntimeAssembly.GetSecurityRuleSet(this.GetNativeHandle());
      }
    }

    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      UnitySerializationHolder.GetUnitySerializationInfo(info, 6, this.FullName, this);
    }

    public override Module ManifestModule
    {
      get
      {
        return (Module) RuntimeAssembly.GetManifestModule(this.GetNativeHandle());
      }
    }

    public override object[] GetCustomAttributes(bool inherit)
    {
      return CustomAttribute.GetCustomAttributes(this, typeof (object) as RuntimeType);
    }

    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      RuntimeType underlyingSystemType = attributeType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (attributeType));
      return CustomAttribute.GetCustomAttributes(this, underlyingSystemType);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      RuntimeType underlyingSystemType = attributeType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "caType");
      return CustomAttribute.IsDefined(this, underlyingSystemType);
    }

    public override IList<CustomAttributeData> GetCustomAttributesData()
    {
      return CustomAttributeData.GetCustomAttributesInternal(this);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static RuntimeAssembly InternalLoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm, bool forIntrospection, bool suppressSecurityChecks, ref StackCrawlMark stackMark)
    {
      if (assemblyFile == null)
        throw new ArgumentNullException(nameof (assemblyFile));
      if (securityEvidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      AssemblyName assemblyRef = new AssemblyName();
      assemblyRef.CodeBase = assemblyFile;
      assemblyRef.SetHashControl(hashValue, hashAlgorithm);
      return RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, securityEvidence, (RuntimeAssembly) null, ref stackMark, true, forIntrospection, suppressSecurityChecks);
    }

    [SecurityCritical]
    internal static RuntimeAssembly InternalLoad(string assemblyString, Evidence assemblySecurity, ref StackCrawlMark stackMark, bool forIntrospection)
    {
      return RuntimeAssembly.InternalLoad(assemblyString, assemblySecurity, ref stackMark, IntPtr.Zero, forIntrospection);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static RuntimeAssembly InternalLoad(string assemblyString, Evidence assemblySecurity, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool forIntrospection)
    {
      RuntimeAssembly assemblyFromResolveEvent;
      AssemblyName assemblyName = RuntimeAssembly.CreateAssemblyName(assemblyString, forIntrospection, out assemblyFromResolveEvent);
      if ((Assembly) assemblyFromResolveEvent != (Assembly) null)
        return assemblyFromResolveEvent;
      return RuntimeAssembly.InternalLoadAssemblyName(assemblyName, assemblySecurity, (RuntimeAssembly) null, ref stackMark, pPrivHostBinder, true, forIntrospection, false);
    }

    [SecurityCritical]
    internal static AssemblyName CreateAssemblyName(string assemblyString, bool forIntrospection, out RuntimeAssembly assemblyFromResolveEvent)
    {
      if (assemblyString == null)
        throw new ArgumentNullException(nameof (assemblyString));
      if (assemblyString.Length == 0 || assemblyString[0] == char.MinValue)
        throw new ArgumentException(Environment.GetResourceString("Format_StringZeroLength"));
      if (forIntrospection)
        AppDomain.CheckReflectionOnlyLoadSupported();
      AssemblyName assemblyName = new AssemblyName();
      assemblyName.Name = assemblyString;
      assemblyName.nInit(out assemblyFromResolveEvent, forIntrospection, true);
      return assemblyName;
    }

    [SecurityCritical]
    internal static RuntimeAssembly InternalLoadAssemblyName(AssemblyName assemblyRef, Evidence assemblySecurity, RuntimeAssembly reqAssembly, ref StackCrawlMark stackMark, bool throwOnFileNotFound, bool forIntrospection, bool suppressSecurityChecks)
    {
      return RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, assemblySecurity, reqAssembly, ref stackMark, IntPtr.Zero, true, forIntrospection, suppressSecurityChecks);
    }

    [SecurityCritical]
    internal static RuntimeAssembly InternalLoadAssemblyName(AssemblyName assemblyRef, Evidence assemblySecurity, RuntimeAssembly reqAssembly, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool throwOnFileNotFound, bool forIntrospection, bool suppressSecurityChecks)
    {
      if (assemblyRef == null)
        throw new ArgumentNullException(nameof (assemblyRef));
      if (assemblyRef.CodeBase != null)
        AppDomain.CheckLoadFromSupported();
      assemblyRef = (AssemblyName) assemblyRef.Clone();
      if (assemblySecurity != null)
      {
        if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
        if (!suppressSecurityChecks)
          new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
      }
      string str = RuntimeAssembly.VerifyCodeBase(assemblyRef.CodeBase);
      if (str != null && !suppressSecurityChecks)
      {
        if (string.Compare(str, 0, "file:", 0, 5, StringComparison.OrdinalIgnoreCase) != 0)
          RuntimeAssembly.CreateWebPermission(assemblyRef.EscapedCodeBase).Demand();
        else
          new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, new URLString(str, true).GetFileName()).Demand();
      }
      return RuntimeAssembly.nLoad(assemblyRef, str, assemblySecurity, reqAssembly, ref stackMark, pPrivHostBinder, throwOnFileNotFound, forIntrospection, suppressSecurityChecks);
    }

    [SecuritySafeCritical]
    internal bool IsFrameworkAssembly()
    {
      return (this.Flags & RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_FRAMEWORK) > RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_UNKNOWN;
    }

    internal bool IsSafeForReflection()
    {
      return (this.Flags & RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_SAFE_REFLECTION) > RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_UNKNOWN;
    }

    [SecuritySafeCritical]
    private bool IsDesignerBindingContext()
    {
      return RuntimeAssembly.nIsDesignerBindingContext(this);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool nIsDesignerBindingContext(RuntimeAssembly assembly);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern RuntimeAssembly _nLoad(AssemblyName fileName, string codeBase, Evidence assemblySecurity, RuntimeAssembly locationHint, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool throwOnFileNotFound, bool forIntrospection, bool suppressSecurityChecks);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsFrameworkAssembly(AssemblyName assemblyName);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsNewPortableAssembly(AssemblyName assemblyName);

    [SecurityCritical]
    private static RuntimeAssembly nLoad(AssemblyName fileName, string codeBase, Evidence assemblySecurity, RuntimeAssembly locationHint, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool throwOnFileNotFound, bool forIntrospection, bool suppressSecurityChecks)
    {
      return RuntimeAssembly._nLoad(fileName, codeBase, assemblySecurity, locationHint, ref stackMark, pPrivHostBinder, throwOnFileNotFound, forIntrospection, suppressSecurityChecks);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static RuntimeAssembly LoadWithPartialNameHack(string partialName, bool cropPublicKey)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      AssemblyName assemblyName = new AssemblyName(partialName);
      if (!RuntimeAssembly.IsSimplyNamed(assemblyName))
      {
        if (cropPublicKey)
        {
          assemblyName.SetPublicKey((byte[]) null);
          assemblyName.SetPublicKeyToken((byte[]) null);
        }
        if (RuntimeAssembly.IsFrameworkAssembly(assemblyName) || !AppDomain.IsAppXModel())
        {
          AssemblyName assemblyRef = RuntimeAssembly.EnumerateCache(assemblyName);
          if (assemblyRef != null)
            return RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, (Evidence) null, (RuntimeAssembly) null, ref stackMark, true, false, false);
          return (RuntimeAssembly) null;
        }
      }
      if (!AppDomain.IsAppXModel())
        return (RuntimeAssembly) null;
      assemblyName.Version = (Version) null;
      return RuntimeAssembly.nLoad(assemblyName, (string) null, (Evidence) null, (RuntimeAssembly) null, ref stackMark, IntPtr.Zero, false, false, false);
    }

    [SecurityCritical]
    internal static RuntimeAssembly LoadWithPartialNameInternal(string partialName, Evidence securityEvidence, ref StackCrawlMark stackMark)
    {
      return RuntimeAssembly.LoadWithPartialNameInternal(new AssemblyName(partialName), securityEvidence, ref stackMark);
    }

    [SecurityCritical]
    internal static RuntimeAssembly LoadWithPartialNameInternal(AssemblyName an, Evidence securityEvidence, ref StackCrawlMark stackMark)
    {
      if (securityEvidence != null)
      {
        if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
      }
      AppDomain.CheckLoadWithPartialNameSupported(stackMark);
      RuntimeAssembly runtimeAssembly = (RuntimeAssembly) null;
      try
      {
        runtimeAssembly = RuntimeAssembly.nLoad(an, (string) null, securityEvidence, (RuntimeAssembly) null, ref stackMark, IntPtr.Zero, true, false, false);
      }
      catch (Exception ex)
      {
        if (ex.IsTransient)
          throw ex;
        if (RuntimeAssembly.IsUserError(ex))
          throw;
        else if (RuntimeAssembly.IsFrameworkAssembly(an) || !AppDomain.IsAppXModel())
        {
          if (RuntimeAssembly.IsSimplyNamed(an))
            return (RuntimeAssembly) null;
          AssemblyName assemblyRef = RuntimeAssembly.EnumerateCache(an);
          if (assemblyRef != null)
            runtimeAssembly = RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, securityEvidence, (RuntimeAssembly) null, ref stackMark, true, false, false);
        }
        else
        {
          an.Version = (Version) null;
          runtimeAssembly = RuntimeAssembly.nLoad(an, (string) null, securityEvidence, (RuntimeAssembly) null, ref stackMark, IntPtr.Zero, false, false, false);
        }
      }
      return runtimeAssembly;
    }

    [SecuritySafeCritical]
    private static bool IsUserError(Exception e)
    {
      return e.HResult == -2146234280;
    }

    private static bool IsSimplyNamed(AssemblyName partialName)
    {
      byte[] publicKeyToken = partialName.GetPublicKeyToken();
      if (publicKeyToken != null && publicKeyToken.Length == 0)
        return true;
      byte[] publicKey = partialName.GetPublicKey();
      return publicKey != null && publicKey.Length == 0;
    }

    [SecurityCritical]
    private static AssemblyName EnumerateCache(AssemblyName partialName)
    {
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
      partialName.Version = (Version) null;
      ArrayList alAssems = new ArrayList();
      Fusion.ReadCache(alAssems, partialName.FullName, 2U);
      IEnumerator enumerator = alAssems.GetEnumerator();
      AssemblyName assemblyName1 = (AssemblyName) null;
      CultureInfo cultureInfo = partialName.CultureInfo;
      while (enumerator.MoveNext())
      {
        AssemblyName assemblyName2 = new AssemblyName((string) enumerator.Current);
        if (RuntimeAssembly.CulturesEqual(cultureInfo, assemblyName2.CultureInfo))
        {
          if (assemblyName1 == null)
            assemblyName1 = assemblyName2;
          else if (assemblyName2.Version > assemblyName1.Version)
            assemblyName1 = assemblyName2;
        }
      }
      return assemblyName1;
    }

    private static bool CulturesEqual(CultureInfo refCI, CultureInfo defCI)
    {
      bool flag = defCI.Equals((object) CultureInfo.InvariantCulture);
      if (refCI == null || refCI.Equals((object) CultureInfo.InvariantCulture))
        return flag;
      return !flag && defCI.Equals((object) refCI);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsReflectionOnly(RuntimeAssembly assembly);

    [ComVisible(false)]
    public override bool ReflectionOnly
    {
      [SecuritySafeCritical] get
      {
        return RuntimeAssembly.IsReflectionOnly(this.GetNativeHandle());
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void LoadModule(RuntimeAssembly assembly, string moduleName, byte[] rawModule, int cbModule, byte[] rawSymbolStore, int cbSymbolStore, ObjectHandleOnStack retModule);

    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
    public override Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
    {
      RuntimeModule o = (RuntimeModule) null;
      RuntimeAssembly.LoadModule(this.GetNativeHandle(), moduleName, rawModule, rawModule != null ? rawModule.Length : 0, rawSymbolStore, rawSymbolStore != null ? rawSymbolStore.Length : 0, JitHelpers.GetObjectHandleOnStack<RuntimeModule>(ref o));
      return (Module) o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetModule(RuntimeAssembly assembly, string name, ObjectHandleOnStack retModule);

    [SecuritySafeCritical]
    public override Module GetModule(string name)
    {
      Module o = (Module) null;
      RuntimeAssembly.GetModule(this.GetNativeHandle(), name, JitHelpers.GetObjectHandleOnStack<Module>(ref o));
      return o;
    }

    [SecuritySafeCritical]
    public override FileStream GetFile(string name)
    {
      RuntimeModule module = (RuntimeModule) this.GetModule(name);
      if ((Module) module == (Module) null)
        return (FileStream) null;
      return new FileStream(module.GetFullyQualifiedName(), FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
    }

    [SecuritySafeCritical]
    public override FileStream[] GetFiles(bool getResourceModules)
    {
      Module[] modules = this.GetModules(getResourceModules);
      int length = modules.Length;
      FileStream[] fileStreamArray = new FileStream[length];
      for (int index = 0; index < length; ++index)
        fileStreamArray[index] = new FileStream(((RuntimeModule) modules[index]).GetFullyQualifiedName(), FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
      return fileStreamArray;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern string[] GetManifestResourceNames(RuntimeAssembly assembly);

    [SecuritySafeCritical]
    public override string[] GetManifestResourceNames()
    {
      return RuntimeAssembly.GetManifestResourceNames(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetExecutingAssembly(StackCrawlMarkHandle stackMark, ObjectHandleOnStack retAssembly);

    [SecurityCritical]
    internal static RuntimeAssembly GetExecutingAssembly(ref StackCrawlMark stackMark)
    {
      RuntimeAssembly o = (RuntimeAssembly) null;
      RuntimeAssembly.GetExecutingAssembly(JitHelpers.GetStackCrawlMarkHandle(ref stackMark), JitHelpers.GetObjectHandleOnStack<RuntimeAssembly>(ref o));
      return o;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern AssemblyName[] GetReferencedAssemblies(RuntimeAssembly assembly);

    [SecuritySafeCritical]
    public override AssemblyName[] GetReferencedAssemblies()
    {
      return RuntimeAssembly.GetReferencedAssemblies(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetManifestResourceInfo(RuntimeAssembly assembly, string resourceName, ObjectHandleOnStack assemblyRef, StringHandleOnStack retFileName, StackCrawlMarkHandle stackMark);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
    {
      RuntimeAssembly o = (RuntimeAssembly) null;
      string s = (string) null;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      int manifestResourceInfo = RuntimeAssembly.GetManifestResourceInfo(this.GetNativeHandle(), resourceName, JitHelpers.GetObjectHandleOnStack<RuntimeAssembly>(ref o), JitHelpers.GetStringHandleOnStack(ref s), JitHelpers.GetStackCrawlMarkHandle(ref stackMark));
      if (manifestResourceInfo == -1)
        return (ManifestResourceInfo) null;
      return new ManifestResourceInfo((Assembly) o, s, (ResourceLocation) manifestResourceInfo);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetLocation(RuntimeAssembly assembly, StringHandleOnStack retString);

    public override string Location
    {
      [SecuritySafeCritical] get
      {
        string s = (string) null;
        RuntimeAssembly.GetLocation(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref s));
        if (s != null)
          new FileIOPermission(FileIOPermissionAccess.PathDiscovery, s).Demand();
        return s;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetImageRuntimeVersion(RuntimeAssembly assembly, StringHandleOnStack retString);

    [ComVisible(false)]
    public override string ImageRuntimeVersion
    {
      [SecuritySafeCritical] get
      {
        string s = (string) null;
        RuntimeAssembly.GetImageRuntimeVersion(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref s));
        return s;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool IsGlobalAssemblyCache(RuntimeAssembly assembly);

    public override bool GlobalAssemblyCache
    {
      [SecuritySafeCritical] get
      {
        return RuntimeAssembly.IsGlobalAssemblyCache(this.GetNativeHandle());
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern long GetHostContext(RuntimeAssembly assembly);

    public override long HostContext
    {
      [SecuritySafeCritical] get
      {
        return RuntimeAssembly.GetHostContext(this.GetNativeHandle());
      }
    }

    private static string VerifyCodeBase(string codebase)
    {
      if (codebase == null)
        return (string) null;
      int length = codebase.Length;
      if (length == 0)
        return (string) null;
      int num = codebase.IndexOf(':');
      if (num != -1 && num + 2 < length && (codebase[num + 1] == '/' || codebase[num + 1] == '\\') && (codebase[num + 2] == '/' || codebase[num + 2] == '\\'))
        return codebase;
      if (length > 2 && codebase[0] == '\\' && codebase[1] == '\\')
        return "file://" + codebase;
      return "file:///" + Path.GetFullPathInternal(codebase);
    }

    [SecurityCritical]
    internal Stream GetManifestResourceStream(Type type, string name, bool skipSecurityCheck, ref StackCrawlMark stackMark)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (type == (Type) null)
      {
        if (name == null)
          throw new ArgumentNullException(nameof (type));
      }
      else
      {
        string str = type.Namespace;
        if (str != null)
        {
          stringBuilder.Append(str);
          if (name != null)
            stringBuilder.Append(Type.Delimiter);
        }
      }
      if (name != null)
        stringBuilder.Append(name);
      return this.GetManifestResourceStream(stringBuilder.ToString(), ref stackMark, skipSecurityCheck);
    }

    internal bool IsStrongNameVerified
    {
      [SecurityCritical] get
      {
        return RuntimeAssembly.GetIsStrongNameVerified(this.GetNativeHandle());
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool GetIsStrongNameVerified(RuntimeAssembly assembly);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe byte* GetResource(RuntimeAssembly assembly, string resourceName, out ulong length, StackCrawlMarkHandle stackMark, bool skipSecurityCheck);

    [SecurityCritical]
    internal unsafe Stream GetManifestResourceStream(string name, ref StackCrawlMark stackMark, bool skipSecurityCheck)
    {
      ulong length = 0;
      byte* resource = RuntimeAssembly.GetResource(this.GetNativeHandle(), name, out length, JitHelpers.GetStackCrawlMarkHandle(ref stackMark), skipSecurityCheck);
      if ((IntPtr) resource == IntPtr.Zero)
        return (Stream) null;
      if (length > (ulong) long.MaxValue)
        throw new NotImplementedException(Environment.GetResourceString("NotImplemented_ResourcesLongerThan2^63"));
      return (Stream) new UnmanagedMemoryStream(resource, (long) length, (long) length, FileAccess.Read, true);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetVersion(RuntimeAssembly assembly, out int majVer, out int minVer, out int buildNum, out int revNum);

    [SecurityCritical]
    internal Version GetVersion()
    {
      int majVer;
      int minVer;
      int buildNum;
      int revNum;
      RuntimeAssembly.GetVersion(this.GetNativeHandle(), out majVer, out minVer, out buildNum, out revNum);
      return new Version(majVer, minVer, buildNum, revNum);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetLocale(RuntimeAssembly assembly, StringHandleOnStack retString);

    [SecurityCritical]
    internal CultureInfo GetLocale()
    {
      string s = (string) null;
      RuntimeAssembly.GetLocale(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref s));
      if (s == null)
        return CultureInfo.InvariantCulture;
      return new CultureInfo(s);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool FCallIsDynamic(RuntimeAssembly assembly);

    public override bool IsDynamic
    {
      [SecuritySafeCritical] get
      {
        return RuntimeAssembly.FCallIsDynamic(this.GetNativeHandle());
      }
    }

    [SecurityCritical]
    private void VerifyCodeBaseDiscovery(string codeBase)
    {
      if (CodeAccessSecurityEngine.QuickCheckForAllDemands() || codeBase == null || string.Compare(codeBase, 0, "file:", 0, 5, StringComparison.OrdinalIgnoreCase) != 0)
        return;
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new URLString(codeBase, true).GetFileName()).Demand();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetSimpleName(RuntimeAssembly assembly, StringHandleOnStack retSimpleName);

    [SecuritySafeCritical]
    internal string GetSimpleName()
    {
      string s = (string) null;
      RuntimeAssembly.GetSimpleName(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern AssemblyHashAlgorithm GetHashAlgorithm(RuntimeAssembly assembly);

    [SecurityCritical]
    private AssemblyHashAlgorithm GetHashAlgorithm()
    {
      return RuntimeAssembly.GetHashAlgorithm(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern AssemblyNameFlags GetFlags(RuntimeAssembly assembly);

    [SecurityCritical]
    private AssemblyNameFlags GetFlags()
    {
      return RuntimeAssembly.GetFlags(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetRawBytes(RuntimeAssembly assembly, ObjectHandleOnStack retRawBytes);

    [SecuritySafeCritical]
    internal byte[] GetRawBytes()
    {
      byte[] o = (byte[]) null;
      RuntimeAssembly.GetRawBytes(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetPublicKey(RuntimeAssembly assembly, ObjectHandleOnStack retPublicKey);

    [SecurityCritical]
    internal byte[] GetPublicKey()
    {
      byte[] o = (byte[]) null;
      RuntimeAssembly.GetPublicKey(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetGrantSet(RuntimeAssembly assembly, ObjectHandleOnStack granted, ObjectHandleOnStack denied);

    [SecurityCritical]
    internal void GetGrantSet(out PermissionSet newGrant, out PermissionSet newDenied)
    {
      PermissionSet o1 = (PermissionSet) null;
      PermissionSet o2 = (PermissionSet) null;
      RuntimeAssembly.GetGrantSet(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o1), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o2));
      newGrant = o1;
      newDenied = o2;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsAllSecurityCritical(RuntimeAssembly assembly);

    [SecuritySafeCritical]
    internal bool IsAllSecurityCritical()
    {
      return RuntimeAssembly.IsAllSecurityCritical(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsAllSecuritySafeCritical(RuntimeAssembly assembly);

    [SecuritySafeCritical]
    internal bool IsAllSecuritySafeCritical()
    {
      return RuntimeAssembly.IsAllSecuritySafeCritical(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsAllPublicAreaSecuritySafeCritical(RuntimeAssembly assembly);

    [SecuritySafeCritical]
    internal bool IsAllPublicAreaSecuritySafeCritical()
    {
      return RuntimeAssembly.IsAllPublicAreaSecuritySafeCritical(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsAllSecurityTransparent(RuntimeAssembly assembly);

    [SecuritySafeCritical]
    internal bool IsAllSecurityTransparent()
    {
      return RuntimeAssembly.IsAllSecurityTransparent(this.GetNativeHandle());
    }

    [SecurityCritical]
    private static void DemandPermission(string codeBase, bool havePath, int demandFlag)
    {
      FileIOPermissionAccess access = FileIOPermissionAccess.PathDiscovery;
      switch (demandFlag)
      {
        case 1:
          access = FileIOPermissionAccess.Read;
          break;
        case 2:
          access = FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery;
          break;
        case 3:
          RuntimeAssembly.CreateWebPermission(AssemblyName.EscapeCodeBase(codeBase)).Demand();
          return;
      }
      if (!havePath)
        codeBase = new URLString(codeBase, true).GetFileName();
      codeBase = Path.GetFullPathInternal(codeBase);
      new FileIOPermission(access, codeBase).Demand();
    }

    private static IPermission CreateWebPermission(string codeBase)
    {
      Assembly assembly = Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
      Type type1 = assembly.GetType("System.Net.NetworkAccess", true);
      IPermission permission = (IPermission) null;
      if (type1.IsEnum && type1.IsVisible)
      {
        object[] objArray = new object[2]
        {
          (object) (Enum) Enum.Parse(type1, "Connect", true),
          null
        };
        if (objArray[0] != null)
        {
          objArray[1] = (object) codeBase;
          Type type2 = assembly.GetType("System.Net.WebPermission", true);
          if (type2.IsVisible)
            permission = (IPermission) Activator.CreateInstance(type2, objArray);
        }
      }
      if (permission == null)
        throw new InvalidOperationException();
      return permission;
    }

    [SecurityCritical]
    private RuntimeModule OnModuleResolveEvent(string moduleName)
    {
      // ISSUE: reference to a compiler-generated field
      ModuleResolveEventHandler moduleResolve = this._ModuleResolve;
      if (moduleResolve == null)
        return (RuntimeModule) null;
      Delegate[] invocationList = moduleResolve.GetInvocationList();
      int length = invocationList.Length;
      for (int index = 0; index < length; ++index)
      {
        RuntimeModule runtimeModule = (RuntimeModule) ((ModuleResolveEventHandler) invocationList[index])((object) this, new ResolveEventArgs(moduleName, (Assembly) this));
        if ((Module) runtimeModule != (Module) null)
          return runtimeModule;
      }
      return (RuntimeModule) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public override Assembly GetSatelliteAssembly(CultureInfo culture)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalGetSatelliteAssembly(culture, (Version) null, ref stackMark);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalGetSatelliteAssembly(culture, version, ref stackMark);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal Assembly InternalGetSatelliteAssembly(CultureInfo culture, Version version, ref StackCrawlMark stackMark)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      return (Assembly) this.InternalGetSatelliteAssembly(this.GetSimpleName() + ".resources", culture, version, true, ref stackMark);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UseRelativeBindForSatellites();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal RuntimeAssembly InternalGetSatelliteAssembly(string name, CultureInfo culture, Version version, bool throwOnFileNotFound, ref StackCrawlMark stackMark)
    {
      AssemblyName assemblyName = new AssemblyName();
      assemblyName.SetPublicKey(this.GetPublicKey());
      assemblyName.Flags = this.GetFlags() | AssemblyNameFlags.PublicKey;
      assemblyName.Version = !(version == (Version) null) ? version : this.GetVersion();
      assemblyName.CultureInfo = culture;
      assemblyName.Name = name;
      RuntimeAssembly runtimeAssembly = (RuntimeAssembly) null;
      bool useLoadFile = AppDomain.IsAppXDesignMode();
      bool flag1 = false;
      if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
        flag1 = this.IsFrameworkAssembly() || RuntimeAssembly.UseRelativeBindForSatellites();
      if (useLoadFile | flag1)
      {
        if (this.GlobalAssemblyCache)
        {
          ArrayList alAssems = new ArrayList();
          bool flag2 = false;
          try
          {
            Fusion.ReadCache(alAssems, assemblyName.FullName, 2U);
          }
          catch (Exception ex)
          {
            if (ex.IsTransient)
              throw;
            else if (!AppDomain.IsAppXModel())
              flag2 = true;
          }
          if (alAssems.Count > 0 | flag2)
            runtimeAssembly = RuntimeAssembly.nLoad(assemblyName, (string) null, (Evidence) null, this, ref stackMark, IntPtr.Zero, throwOnFileNotFound, false, false);
        }
        else
        {
          string codeBase = this.CodeBase;
          if (codeBase != null && string.Compare(codeBase, 0, "file:", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
          {
            runtimeAssembly = this.InternalProbeForSatelliteAssemblyNextToParentAssembly(assemblyName, name, codeBase, culture, throwOnFileNotFound, useLoadFile, ref stackMark);
            if ((Assembly) runtimeAssembly != (Assembly) null && !RuntimeAssembly.IsSimplyNamed(assemblyName))
            {
              AssemblyName name1 = runtimeAssembly.GetName();
              if (!AssemblyName.ReferenceMatchesDefinitionInternal(assemblyName, name1, false))
                runtimeAssembly = (RuntimeAssembly) null;
            }
          }
          else if (!useLoadFile)
            runtimeAssembly = RuntimeAssembly.nLoad(assemblyName, (string) null, (Evidence) null, this, ref stackMark, IntPtr.Zero, throwOnFileNotFound, false, false);
        }
      }
      else
        runtimeAssembly = RuntimeAssembly.nLoad(assemblyName, (string) null, (Evidence) null, this, ref stackMark, IntPtr.Zero, throwOnFileNotFound, false, false);
      if ((Assembly) runtimeAssembly == (Assembly) this || (Assembly) runtimeAssembly == (Assembly) null & throwOnFileNotFound)
        throw new FileNotFoundException(string.Format((IFormatProvider) culture, Environment.GetResourceString("IO.FileNotFound_FileName"), (object) assemblyName.Name));
      return runtimeAssembly;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private RuntimeAssembly InternalProbeForSatelliteAssemblyNextToParentAssembly(AssemblyName an, string name, string codeBase, CultureInfo culture, bool throwOnFileNotFound, bool useLoadFile, ref StackCrawlMark stackMark)
    {
      string str1 = (string) null;
      if (useLoadFile)
        str1 = this.Location;
      FileNotFoundException notFoundException = (FileNotFoundException) null;
      StringBuilder stringBuilder = new StringBuilder(useLoadFile ? str1 : codeBase, 0, useLoadFile ? str1.LastIndexOf('\\') + 1 : codeBase.LastIndexOf('/') + 1, 260);
      stringBuilder.Append(an.CultureInfo.Name);
      stringBuilder.Append(useLoadFile ? '\\' : '/');
      stringBuilder.Append(name);
      stringBuilder.Append(".DLL");
      string str2 = stringBuilder.ToString();
      AssemblyName fileName = (AssemblyName) null;
      if (!useLoadFile)
      {
        fileName = new AssemblyName();
        fileName.CodeBase = str2;
      }
      RuntimeAssembly runtimeAssembly;
      try
      {
        try
        {
          runtimeAssembly = useLoadFile ? RuntimeAssembly.nLoadFile(str2, (Evidence) null) : RuntimeAssembly.nLoad(fileName, str2, (Evidence) null, this, ref stackMark, IntPtr.Zero, throwOnFileNotFound, false, false);
        }
        catch (FileNotFoundException ex)
        {
          notFoundException = new FileNotFoundException(string.Format((IFormatProvider) culture, Environment.GetResourceString("IO.FileNotFound_FileName"), (object) str2), str2);
          runtimeAssembly = (RuntimeAssembly) null;
        }
        if ((Assembly) runtimeAssembly == (Assembly) null)
        {
          stringBuilder.Remove(stringBuilder.Length - 4, 4);
          stringBuilder.Append(".EXE");
          string str3 = stringBuilder.ToString();
          if (!useLoadFile)
            fileName.CodeBase = str3;
          try
          {
            runtimeAssembly = useLoadFile ? RuntimeAssembly.nLoadFile(str3, (Evidence) null) : RuntimeAssembly.nLoad(fileName, str3, (Evidence) null, this, ref stackMark, IntPtr.Zero, false, false, false);
          }
          catch (FileNotFoundException ex)
          {
            runtimeAssembly = (RuntimeAssembly) null;
          }
          if ((Assembly) runtimeAssembly == (Assembly) null & throwOnFileNotFound)
            throw notFoundException;
        }
      }
      catch (DirectoryNotFoundException ex)
      {
        if (throwOnFileNotFound)
          throw;
        else
          runtimeAssembly = (RuntimeAssembly) null;
      }
      return runtimeAssembly;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeAssembly nLoadFile(string path, Evidence evidence);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeAssembly nLoadImage(byte[] rawAssembly, byte[] rawSymbolStore, Evidence evidence, ref StackCrawlMark stackMark, bool fIntrospection, bool fSkipIntegrityCheck, SecurityContextSource securityContextSource);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetModules(RuntimeAssembly assembly, bool loadIfNotFound, bool getResourceModules, ObjectHandleOnStack retModuleHandles);

    [SecuritySafeCritical]
    private RuntimeModule[] GetModulesInternal(bool loadIfNotFound, bool getResourceModules)
    {
      RuntimeModule[] o = (RuntimeModule[]) null;
      RuntimeAssembly.GetModules(this.GetNativeHandle(), loadIfNotFound, getResourceModules, JitHelpers.GetObjectHandleOnStack<RuntimeModule[]>(ref o));
      return o;
    }

    public override Module[] GetModules(bool getResourceModules)
    {
      return (Module[]) this.GetModulesInternal(true, getResourceModules);
    }

    public override Module[] GetLoadedModules(bool getResourceModules)
    {
      return (Module[]) this.GetModulesInternal(false, getResourceModules);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeModule GetManifestModule(RuntimeAssembly assembly);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool AptcaCheck(RuntimeAssembly targetAssembly, RuntimeAssembly sourceAssembly);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetToken(RuntimeAssembly assembly);

    private enum ASSEMBLY_FLAGS : uint
    {
      ASSEMBLY_FLAGS_UNKNOWN = 0,
      ASSEMBLY_FLAGS_TOKEN_MASK = 16777215, // 0x00FFFFFF
      ASSEMBLY_FLAGS_INITIALIZED = 16777216, // 0x01000000
      ASSEMBLY_FLAGS_FRAMEWORK = 33554432, // 0x02000000
      ASSEMBLY_FLAGS_SAFE_REFLECTION = 67108864, // 0x04000000
    }
  }
}
