// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.AssemblyBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection.Emit
{
  /// <summary>Определяет и представляет динамическую сборку.</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_AssemblyBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class AssemblyBuilder : Assembly, _AssemblyBuilder
  {
    internal AssemblyBuilderData m_assemblyData;
    private InternalAssemblyBuilder m_internalAssemblyBuilder;
    private ModuleBuilder m_manifestModuleBuilder;
    private bool m_fManifestModuleUsedAsDefinedModule;
    internal const string MANIFEST_MODULE_NAME = "RefEmit_InMemoryManifestModule";
    private ModuleBuilder m_onDiskAssemblyModuleBuilder;
    private bool m_profileAPICheck;

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern RuntimeModule GetInMemoryAssemblyModule(RuntimeAssembly assembly);

    [SecurityCritical]
    private Module nGetInMemoryAssemblyModule()
    {
      return (Module) AssemblyBuilder.GetInMemoryAssemblyModule(this.GetNativeHandle());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern RuntimeModule GetOnDiskAssemblyModule(RuntimeAssembly assembly);

    [SecurityCritical]
    private ModuleBuilder GetOnDiskAssemblyModuleBuilder()
    {
      if ((Module) this.m_onDiskAssemblyModuleBuilder == (Module) null)
      {
        ModuleBuilder moduleBuilder = new ModuleBuilder(this, (InternalModuleBuilder) AssemblyBuilder.GetOnDiskAssemblyModule(this.InternalAssembly.GetNativeHandle()));
        moduleBuilder.Init("RefEmit_OnDiskManifestModule", (string) null, 0);
        this.m_onDiskAssemblyModuleBuilder = moduleBuilder;
      }
      return this.m_onDiskAssemblyModuleBuilder;
    }

    internal ModuleBuilder GetModuleBuilder(InternalModuleBuilder module)
    {
      lock (this.SyncRoot)
      {
        foreach (ModuleBuilder moduleBuilder in this.m_assemblyData.m_moduleBuilderList)
        {
          if ((Module) moduleBuilder.InternalModule == (Module) module)
            return moduleBuilder;
        }
        if ((Module) this.m_onDiskAssemblyModuleBuilder != (Module) null && (Module) this.m_onDiskAssemblyModuleBuilder.InternalModule == (Module) module)
          return this.m_onDiskAssemblyModuleBuilder;
        if ((Module) this.m_manifestModuleBuilder.InternalModule == (Module) module)
          return this.m_manifestModuleBuilder;
        throw new ArgumentException(nameof (module));
      }
    }

    internal object SyncRoot
    {
      get
      {
        return this.InternalAssembly.SyncRoot;
      }
    }

    internal InternalAssemblyBuilder InternalAssembly
    {
      get
      {
        return this.m_internalAssemblyBuilder;
      }
    }

    internal RuntimeAssembly GetNativeHandle()
    {
      return this.InternalAssembly.GetNativeHandle();
    }

    [SecurityCritical]
    internal Version GetVersion()
    {
      return this.InternalAssembly.GetVersion();
    }

    internal bool ProfileAPICheck
    {
      get
      {
        return this.m_profileAPICheck;
      }
    }

    [SecurityCritical]
    internal AssemblyBuilder(AppDomain domain, AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, ref StackCrawlMark stackMark, IEnumerable<CustomAttributeBuilder> unsafeAssemblyAttributes, SecurityContextSource securityContextSource)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (access != AssemblyBuilderAccess.Run && access != AssemblyBuilderAccess.Save && (access != AssemblyBuilderAccess.RunAndSave && access != AssemblyBuilderAccess.ReflectionOnly) && access != AssemblyBuilderAccess.RunAndCollect)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) access), nameof (access));
      switch (securityContextSource)
      {
        case SecurityContextSource.CurrentAppDomain:
        case SecurityContextSource.CurrentAssembly:
          name = (AssemblyName) name.Clone();
          if (name.KeyPair != null)
            name.SetPublicKey(name.KeyPair.PublicKey);
          if (evidence != null)
            new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
          if (access == AssemblyBuilderAccess.RunAndCollect)
            new PermissionSet(PermissionState.Unrestricted).Demand();
          List<CustomAttributeBuilder> attributeBuilderList = (List<CustomAttributeBuilder>) null;
          DynamicAssemblyFlags flags = DynamicAssemblyFlags.None;
          byte[] securityRulesBlob = (byte[]) null;
          byte[] aptcaBlob = (byte[]) null;
          if (unsafeAssemblyAttributes != null)
          {
            attributeBuilderList = new List<CustomAttributeBuilder>(unsafeAssemblyAttributes);
            foreach (CustomAttributeBuilder attributeBuilder in attributeBuilderList)
            {
              if (attributeBuilder.m_con.DeclaringType == typeof (SecurityTransparentAttribute))
                flags |= DynamicAssemblyFlags.Transparent;
              else if (attributeBuilder.m_con.DeclaringType == typeof (SecurityCriticalAttribute))
              {
                SecurityCriticalScope securityCriticalScope = SecurityCriticalScope.Everything;
                if (attributeBuilder.m_constructorArgs != null && attributeBuilder.m_constructorArgs.Length == 1 && attributeBuilder.m_constructorArgs[0] is SecurityCriticalScope)
                  securityCriticalScope = (SecurityCriticalScope) attributeBuilder.m_constructorArgs[0];
                flags |= DynamicAssemblyFlags.Critical;
                if (securityCriticalScope == SecurityCriticalScope.Everything)
                  flags |= DynamicAssemblyFlags.AllCritical;
              }
              else if (attributeBuilder.m_con.DeclaringType == typeof (SecurityRulesAttribute))
              {
                securityRulesBlob = new byte[attributeBuilder.m_blob.Length];
                Array.Copy((Array) attributeBuilder.m_blob, (Array) securityRulesBlob, securityRulesBlob.Length);
              }
              else if (attributeBuilder.m_con.DeclaringType == typeof (SecurityTreatAsSafeAttribute))
                flags |= DynamicAssemblyFlags.TreatAsSafe;
              else if (attributeBuilder.m_con.DeclaringType == typeof (AllowPartiallyTrustedCallersAttribute))
              {
                flags |= DynamicAssemblyFlags.Aptca;
                aptcaBlob = new byte[attributeBuilder.m_blob.Length];
                Array.Copy((Array) attributeBuilder.m_blob, (Array) aptcaBlob, aptcaBlob.Length);
              }
            }
          }
          this.m_internalAssemblyBuilder = (InternalAssemblyBuilder) AssemblyBuilder.nCreateDynamicAssembly(domain, name, evidence, ref stackMark, requiredPermissions, optionalPermissions, refusedPermissions, securityRulesBlob, aptcaBlob, access, flags, securityContextSource);
          this.m_assemblyData = new AssemblyBuilderData(this.m_internalAssemblyBuilder, name.Name, access, dir);
          this.m_assemblyData.AddPermissionRequests(requiredPermissions, optionalPermissions, refusedPermissions);
          if (AppDomain.ProfileAPICheck)
          {
            RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
            if ((Assembly) executingAssembly != (Assembly) null && !executingAssembly.IsFrameworkAssembly())
              this.m_profileAPICheck = true;
          }
          this.InitManifestModule();
          if (attributeBuilderList == null)
            break;
          using (List<CustomAttributeBuilder>.Enumerator enumerator = attributeBuilderList.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.SetCustomAttribute(enumerator.Current);
            break;
          }
        default:
          throw new ArgumentOutOfRangeException(nameof (securityContextSource));
      }
    }

    [SecurityCritical]
    private void InitManifestModule()
    {
      this.m_manifestModuleBuilder = new ModuleBuilder(this, (InternalModuleBuilder) this.nGetInMemoryAssemblyModule());
      this.m_manifestModuleBuilder.Init("RefEmit_InMemoryManifestModule", (string) null, 0);
      this.m_fManifestModuleUsedAsDefinedModule = false;
    }

    /// <summary>
    ///   Определяет динамическую сборку, которая имеет указанные имя и права доступа.
    /// </summary>
    /// <param name="name">Имя сборки.</param>
    /// <param name="access">Права доступа сборки.</param>
    /// <returns>Объект, представляющий новую сборку.</returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return AssemblyBuilder.InternalDefineDynamicAssembly(name, access, (string) null, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>
    ///   Определяет новую сборку, которая имеет указанные имя, права доступа и атрибуты.
    /// </summary>
    /// <param name="name">Имя сборки.</param>
    /// <param name="access">Права доступа сборки.</param>
    /// <param name="assemblyAttributes">
    ///   Коллекция, содержащая атрибуты сборки.
    /// </param>
    /// <returns>Объект, представляющий новую сборку.</returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return AssemblyBuilder.InternalDefineDynamicAssembly(name, access, (string) null, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, assemblyAttributes, SecurityContextSource.CurrentAssembly);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern Assembly nCreateDynamicAssembly(AppDomain domain, AssemblyName name, Evidence identity, ref StackCrawlMark stackMark, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, byte[] securityRulesBlob, byte[] aptcaBlob, AssemblyBuilderAccess access, DynamicAssemblyFlags flags, SecurityContextSource securityContextSource);

    [SecurityCritical]
    internal static AssemblyBuilder InternalDefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, ref StackCrawlMark stackMark, IEnumerable<CustomAttributeBuilder> unsafeAssemblyAttributes, SecurityContextSource securityContextSource)
    {
      if (evidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      lock (typeof (AssemblyBuilder.AssemblyBuilderLock))
        return new AssemblyBuilder(AppDomain.CurrentDomain, name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, unsafeAssemblyAttributes, securityContextSource);
    }

    /// <summary>
    ///   Определяет именованный временный динамический модуль в этой сборке.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического модуля.
    ///    Длина должна быть менее 260 символов.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.ModuleBuilder" />, представляющий определенный динамический модуль.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> начинается с пробела.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="name" /> больше или равна 260.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ExecutionEngineException">
    ///   Не удается загрузить сборку для модуля записи символов по умолчанию.
    /// 
    ///   -или-
    /// 
    ///   Не удается найти тип, реализующий интерфейс модуля записи символов по умолчанию.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ModuleBuilder DefineDynamicModule(string name)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.DefineDynamicModuleInternal(name, false, ref stackMark);
    }

    /// <summary>
    ///   Определяет именованный несохраняемый динамический модуль в данной сборке и указывает, следует ли отправлять сведения символа.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического модуля.
    ///    Длина должна быть менее 260 символов.
    /// </param>
    /// <param name="emitSymbolInfo">
    ///   <see langword="true" /> Если символьная информация должна выдаваться; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.ModuleBuilder" />, представляющий определенный динамический модуль.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> начинается с пробела.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="name" /> больше или равна 260.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ExecutionEngineException">
    ///   Не удается загрузить сборку для модуля записи символов по умолчанию.
    /// 
    ///   -или-
    /// 
    ///   Не удается найти тип, реализующий интерфейс модуля записи символов по умолчанию.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ModuleBuilder DefineDynamicModule(string name, bool emitSymbolInfo)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.DefineDynamicModuleInternal(name, emitSymbolInfo, ref stackMark);
    }

    [SecurityCritical]
    private ModuleBuilder DefineDynamicModuleInternal(string name, bool emitSymbolInfo, ref StackCrawlMark stackMark)
    {
      lock (this.SyncRoot)
        return this.DefineDynamicModuleInternalNoLock(name, emitSymbolInfo, ref stackMark);
    }

    [SecurityCritical]
    private ModuleBuilder DefineDynamicModuleInternalNoLock(string name, bool emitSymbolInfo, ref StackCrawlMark stackMark)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      if (name[0] == char.MinValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), nameof (name));
      ISymbolWriter writer = (ISymbolWriter) null;
      IntPtr pInternalSymWriter = new IntPtr();
      this.m_assemblyData.CheckNameConflict(name);
      ModuleBuilder dynModule;
      if (this.m_fManifestModuleUsedAsDefinedModule)
      {
        int tkFile;
        dynModule = new ModuleBuilder(this, (InternalModuleBuilder) AssemblyBuilder.DefineDynamicModule((RuntimeAssembly) this.InternalAssembly, emitSymbolInfo, name, name, ref stackMark, ref pInternalSymWriter, true, out tkFile));
        dynModule.Init(name, (string) null, tkFile);
      }
      else
      {
        this.m_manifestModuleBuilder.ModifyModuleName(name);
        dynModule = this.m_manifestModuleBuilder;
        if (emitSymbolInfo)
          pInternalSymWriter = ModuleBuilder.nCreateISymWriterForDynamicModule((Module) dynModule.InternalModule, name);
      }
      if (emitSymbolInfo)
      {
        Type type = this.LoadISymWrapper().GetType("System.Diagnostics.SymbolStore.SymWriter", true, false);
        if (type != (Type) null && !type.IsVisible)
          type = (Type) null;
        if (type == (Type) null)
          throw new TypeLoadException(Environment.GetResourceString("MissingType", (object) "SymWriter"));
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
        try
        {
          new PermissionSet(PermissionState.Unrestricted).Assert();
          writer = (ISymbolWriter) Activator.CreateInstance(type);
          writer.SetUnderlyingWriter(pInternalSymWriter);
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
        }
      }
      dynModule.SetSymWriter(writer);
      this.m_assemblyData.AddModule(dynModule);
      if ((Module) dynModule == (Module) this.m_manifestModuleBuilder)
        this.m_fManifestModuleUsedAsDefinedModule = true;
      return dynModule;
    }

    /// <summary>
    ///   Определяет сохраняемый динамический модуль с заданным именем, который будет сохранен в указанном файле.
    ///    Данные символов не созданы.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического модуля.
    ///    Длина должна быть менее 260 символов.
    /// </param>
    /// <param name="fileName">
    ///   Имя файла, в котором должен сохраняться динамический модуль.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.ModuleBuilder" />, представляющий определенный динамический модуль.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> или <paramref name="fileName" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="name" /> больше или равна 260.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="fileName" /> содержит спецификацию пути (например, компонент каталога).
    /// 
    ///   -или-
    /// 
    ///   Имеется конфликт с именем другого файла, который принадлежит этой сборке.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Эта сборка была ранее сохранена.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Сборка была вызвана в динамической сборке с атрибутом <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Run" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ExecutionEngineException">
    ///   Не удается загрузить сборку для модуля записи символов по умолчанию.
    /// 
    ///   -или-
    /// 
    ///   Не удается найти тип, реализующий интерфейс модуля записи символов по умолчанию.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ModuleBuilder DefineDynamicModule(string name, string fileName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.DefineDynamicModuleInternal(name, fileName, false, ref stackMark);
    }

    /// <summary>
    ///   Определяет сохраняемый динамический модуль с указанием имени модуля, имени файла, в котором модуль будет сохранен, и необходимости создания символьной информации с помощью модуля записи символов по умолчанию.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического модуля.
    ///    Длина должна быть менее 260 символов.
    /// </param>
    /// <param name="fileName">
    ///   Имя файла, в котором должен сохраняться динамический модуль.
    /// </param>
    /// <param name="emitSymbolInfo">
    ///   Если это <see langword="true" />, символьная информация записывается с помощью модуля записи символов по умолчанию.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.ModuleBuilder" />, представляющий определенный динамический модуль.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> или <paramref name="fileName" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="name" /> больше или равна 260.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="fileName" /> содержит спецификацию пути (например, компонент каталога).
    /// 
    ///   -или-
    /// 
    ///   Имеется конфликт с именем другого файла, который принадлежит этой сборке.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Эта сборка была ранее сохранена.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Сборка была вызвана для динамической сборки с атрибутом <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Run" />.
    /// </exception>
    /// <exception cref="T:System.ExecutionEngineException">
    ///   Не удается загрузить сборку для модуля записи символов по умолчанию.
    /// 
    ///   -или-
    /// 
    ///   Не удается найти тип, реализующий интерфейс модуля записи символов по умолчанию.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.DefineDynamicModuleInternal(name, fileName, emitSymbolInfo, ref stackMark);
    }

    [SecurityCritical]
    private ModuleBuilder DefineDynamicModuleInternal(string name, string fileName, bool emitSymbolInfo, ref StackCrawlMark stackMark)
    {
      lock (this.SyncRoot)
        return this.DefineDynamicModuleInternalNoLock(name, fileName, emitSymbolInfo, ref stackMark);
    }

    [SecurityCritical]
    private ModuleBuilder DefineDynamicModuleInternalNoLock(string name, string fileName, bool emitSymbolInfo, ref StackCrawlMark stackMark)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      if (name[0] == char.MinValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), nameof (name));
      if (fileName == null)
        throw new ArgumentNullException(nameof (fileName));
      if (fileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (fileName));
      if (!string.Equals(fileName, Path.GetFileName(fileName)))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotSimpleFileName"), nameof (fileName));
      if (this.m_assemblyData.m_access == AssemblyBuilderAccess.Run)
        throw new NotSupportedException(Environment.GetResourceString("Argument_BadPersistableModuleInTransientAssembly"));
      if (this.m_assemblyData.m_isSaved)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotAlterAssembly"));
      ISymbolWriter writer = (ISymbolWriter) null;
      IntPtr pInternalSymWriter = new IntPtr();
      this.m_assemblyData.CheckNameConflict(name);
      this.m_assemblyData.CheckFileNameConflict(fileName);
      int tkFile;
      ModuleBuilder dynModule = new ModuleBuilder(this, (InternalModuleBuilder) AssemblyBuilder.DefineDynamicModule((RuntimeAssembly) this.InternalAssembly, emitSymbolInfo, name, fileName, ref stackMark, ref pInternalSymWriter, false, out tkFile));
      dynModule.Init(name, fileName, tkFile);
      if (emitSymbolInfo)
      {
        Type type = this.LoadISymWrapper().GetType("System.Diagnostics.SymbolStore.SymWriter", true, false);
        if (type != (Type) null && !type.IsVisible)
          type = (Type) null;
        if (type == (Type) null)
          throw new TypeLoadException(Environment.GetResourceString("MissingType", (object) "SymWriter"));
        try
        {
          new PermissionSet(PermissionState.Unrestricted).Assert();
          writer = (ISymbolWriter) Activator.CreateInstance(type);
          writer.SetUnderlyingWriter(pInternalSymWriter);
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
        }
      }
      dynModule.SetSymWriter(writer);
      this.m_assemblyData.AddModule(dynModule);
      return dynModule;
    }

    private Assembly LoadISymWrapper()
    {
      if (this.m_assemblyData.m_ISymWrapperAssembly != (Assembly) null)
        return this.m_assemblyData.m_ISymWrapperAssembly;
      Assembly assembly = Assembly.Load("ISymWrapper, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
      this.m_assemblyData.m_ISymWrapperAssembly = assembly;
      return assembly;
    }

    internal void CheckContext(params Type[][] typess)
    {
      if (typess == null)
        return;
      foreach (Type[] typeArray in typess)
      {
        if (typeArray != null)
          this.CheckContext(typeArray);
      }
    }

    internal void CheckContext(params Type[] types)
    {
      if (types == null)
        return;
      foreach (Type type in types)
      {
        if (!(type == (Type) null))
        {
          if (type.Module == (Module) null || type.Module.Assembly == (Assembly) null)
            throw new ArgumentException(Environment.GetResourceString("Argument_TypeNotValid"));
          if (!(type.Module.Assembly == typeof (object).Module.Assembly))
          {
            if (type.Module.Assembly.ReflectionOnly && !this.ReflectionOnly)
              throw new InvalidOperationException(Environment.GetResourceString("Arugment_EmitMixedContext1", (object) type.AssemblyQualifiedName));
            if (!type.Module.Assembly.ReflectionOnly && this.ReflectionOnly)
              throw new InvalidOperationException(Environment.GetResourceString("Arugment_EmitMixedContext2", (object) type.AssemblyQualifiedName));
          }
        }
      }
    }

    /// <summary>
    ///   Определяет автономный управляемый ресурс для данной сборки с атрибутом открытого ресурса по умолчанию.
    /// </summary>
    /// <param name="name">Логическое имя ресурса.</param>
    /// <param name="description">Текстовое описание ресурса.</param>
    /// <param name="fileName">
    ///   Имя физического файла (RESOURCES-файл), с которым сопоставляется логическое имя.
    ///    Оно не должно содержать путь.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Resources.ResourceWriter" /> для указанного ресурса.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> было определено ранее.
    /// 
    ///   -или-
    /// 
    ///   В сборке уже имеется другой файл с именем <paramref name="fileName" />.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="fileName" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="fileName" /> содержит путь.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public IResourceWriter DefineResource(string name, string description, string fileName)
    {
      return this.DefineResource(name, description, fileName, ResourceAttributes.Public);
    }

    /// <summary>
    ///   Определяет автономный управляемый ресурс для данной сборки.
    ///    Для управляемого ресурса можно задать атрибуты.
    /// </summary>
    /// <param name="name">Логическое имя ресурса.</param>
    /// <param name="description">Текстовое описание ресурса.</param>
    /// <param name="fileName">
    ///   Имя физического файла (RESOURCES-файл), с которым сопоставляется логическое имя.
    ///    Оно не должно содержать путь.
    /// </param>
    /// <param name="attribute">Атрибуты ресурса.</param>
    /// <returns>
    ///   Объект <see cref="T:System.Resources.ResourceWriter" /> для указанного ресурса.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Имя <paramref name="name" /> было определено ранее, или в сборке имеется другой файл с именем <paramref name="fileName" />.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="fileName" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="fileName" /> содержит путь.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public IResourceWriter DefineResource(string name, string description, string fileName, ResourceAttributes attribute)
    {
      lock (this.SyncRoot)
        return this.DefineResourceNoLock(name, description, fileName, attribute);
    }

    private IResourceWriter DefineResourceNoLock(string name, string description, string fileName, ResourceAttributes attribute)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), name);
      if (fileName == null)
        throw new ArgumentNullException(nameof (fileName));
      if (fileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (fileName));
      if (!string.Equals(fileName, Path.GetFileName(fileName)))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotSimpleFileName"), nameof (fileName));
      this.m_assemblyData.CheckResNameConflict(name);
      this.m_assemblyData.CheckFileNameConflict(fileName);
      string str;
      ResourceWriter resWriter;
      if (this.m_assemblyData.m_strDir == null)
      {
        str = Path.Combine(Environment.CurrentDirectory, fileName);
        resWriter = new ResourceWriter(str);
      }
      else
      {
        str = Path.Combine(this.m_assemblyData.m_strDir, fileName);
        resWriter = new ResourceWriter(str);
      }
      string fullPath = Path.GetFullPath(str);
      fileName = Path.GetFileName(fullPath);
      this.m_assemblyData.AddResWriter(new ResWriterData(resWriter, (Stream) null, name, fileName, fullPath, attribute));
      return (IResourceWriter) resWriter;
    }

    /// <summary>Добавляет существующий файл ресурсов в эту сборку.</summary>
    /// <param name="name">Логическое имя ресурса.</param>
    /// <param name="fileName">
    ///   Имя физического файла (RESOURCES-файл), с которым сопоставляется логическое имя.
    ///    Оно не должно включать путь; файл должен быть в том же каталоге, что и сборка, в которую он добавляется.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> было определено ранее.
    /// 
    ///   -или-
    /// 
    ///   В сборке уже имеется другой файл с именем <paramref name="fileName" />.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="fileName" /> равна нулю, или <paramref name="fileName" /> включает в себя путь.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл <paramref name="fileName" /> не найден.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public void AddResourceFile(string name, string fileName)
    {
      this.AddResourceFile(name, fileName, ResourceAttributes.Public);
    }

    /// <summary>Добавляет существующий файл ресурсов в эту сборку.</summary>
    /// <param name="name">Логическое имя ресурса.</param>
    /// <param name="fileName">
    ///   Имя физического файла (RESOURCES-файл), с которым сопоставляется логическое имя.
    ///    Оно не должно включать путь; файл должен быть в том же каталоге, что и сборка, в которую он добавляется.
    /// </param>
    /// <param name="attribute">Атрибуты ресурса.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> было определено ранее.
    /// 
    ///   -или-
    /// 
    ///   В сборке уже имеется другой файл с именем <paramref name="fileName" />.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="name" /> равна нулю, или если длина <paramref name="fileName" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="fileName" /> содержит путь.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Если файл <paramref name="fileName" /> не найден.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public void AddResourceFile(string name, string fileName, ResourceAttributes attribute)
    {
      lock (this.SyncRoot)
        this.AddResourceFileNoLock(name, fileName, attribute);
    }

    [SecuritySafeCritical]
    private void AddResourceFileNoLock(string name, string fileName, ResourceAttributes attribute)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), name);
      if (fileName == null)
        throw new ArgumentNullException(nameof (fileName));
      if (fileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), fileName);
      if (!string.Equals(fileName, Path.GetFileName(fileName)))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotSimpleFileName"), nameof (fileName));
      this.m_assemblyData.CheckResNameConflict(name);
      this.m_assemblyData.CheckFileNameConflict(fileName);
      string fullPath = Path.UnsafeGetFullPath(this.m_assemblyData.m_strDir != null ? Path.Combine(this.m_assemblyData.m_strDir, fileName) : Path.Combine(Environment.CurrentDirectory, fileName));
      fileName = Path.GetFileName(fullPath);
      if (!File.UnsafeExists(fullPath))
        throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound_FileName", (object) fileName), fileName);
      this.m_assemblyData.AddResWriter(new ResWriterData((ResourceWriter) null, (Stream) null, name, fileName, fullPath, attribute));
    }

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли данный экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим экземпляром, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="obj" /> равно типу и значению данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      return this.InternalAssembly.Equals(obj);
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    public override int GetHashCode()
    {
      return this.InternalAssembly.GetHashCode();
    }

    /// <summary>
    ///   Возвращает все настраиваемые атрибуты, которые были применены к текущему <see cref="T:System.Reflection.Emit.AssemblyBuilder" />.
    /// </summary>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    /// </param>
    /// <returns>
    ///   Массив, содержащий настраиваемые атрибуты; массив является пустым, если атрибуты отсутствуют.
    /// </returns>
    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.InternalAssembly.GetCustomAttributes(inherit);
    }

    /// <summary>
    ///   Возвращает настраиваемые атрибуты, примененные к текущему <see cref="T:System.Reflection.Emit.AssemblyBuilder" />, которые являются производными от указанного типа атрибута.
    /// </summary>
    /// <param name="attributeType">
    ///   Базовый тип, от которого наследуют атрибуты.
    /// </param>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    /// </param>
    /// <returns>
    ///   Массив, содержащий настраиваемые атрибуты, которые являются производными на любом уровне от <paramref name="attributeType" />; массив является пустым, если атрибуты отсутствуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является объектом <see cref="T:System.Type" />, предоставляемым средой выполнения.
    ///    Например, <paramref name="attributeType" /> является объектом <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// </exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.InternalAssembly.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, применяется ли к данному члену один или несколько экземпляров определенного типа атрибута.
    /// </summary>
    /// <param name="attributeType">Тип атрибута для тестирования.</param>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если один или несколько экземпляров <paramref name="attributeType" /> применяются к этой динамической сборке; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.InternalAssembly.IsDefined(attributeType, inherit);
    }

    /// <summary>
    ///   Возвращает объекты <see cref="T:System.Reflection.CustomAttributeData" />, содержащие сведения об атрибутах, которые были применены к текущему <see cref="T:System.Reflection.Emit.AssemblyBuilder" />.
    /// </summary>
    /// <returns>
    ///   Универсальный список объектов <see cref="T:System.Reflection.CustomAttributeData" />, представляющих данные об атрибутах, которые были применены к текущему модулю.
    /// </returns>
    public override IList<CustomAttributeData> GetCustomAttributesData()
    {
      return this.InternalAssembly.GetCustomAttributesData();
    }

    /// <summary>Загружает указанный ресурс манифеста из сборки.</summary>
    /// <returns>
    ///   Массив типа <see langword="String" />, содержащий имена всех ресурсов.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается для динамической сборки.
    ///    Для получения имен ресурсов манифеста используйте <see cref="M:System.Reflection.Assembly.GetManifestResourceNames" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public override string[] GetManifestResourceNames()
    {
      return this.InternalAssembly.GetManifestResourceNames();
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.IO.FileStream" /> для указанного файла из таблицы файлов манифеста данной сборки.
    /// </summary>
    /// <param name="name">Имя указанного файла.</param>
    /// <returns>
    ///   <see cref="T:System.IO.FileStream" /> для указанного файла или <see langword="null" />, если файл не найден.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public override FileStream GetFile(string name)
    {
      return this.InternalAssembly.GetFile(name);
    }

    /// <summary>
    ///   Получает файлы из таблицы манифеста сборки с указанием включать или не включать модули ресурсов.
    /// </summary>
    /// <param name="getResourceModules">
    ///   Значение <see langword="true" />, если необходимо включать модули ресурсов; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.IO.FileStream" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public override FileStream[] GetFiles(bool getResourceModules)
    {
      return this.InternalAssembly.GetFiles(getResourceModules);
    }

    /// <summary>
    ///   Загружает из сборки указанный ресурс манифеста с учетом ограничения области действия пространства имен по типу.
    /// </summary>
    /// <param name="type">
    ///   Тип, пространством имен которого ограничена область действия имени ресурса манифеста.
    /// </param>
    /// <param name="name">Имя запрашиваемого ресурса манифеста.</param>
    /// <returns>
    ///   <see cref="T:System.IO.Stream" />, представляющий этот ресурс манифеста.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public override Stream GetManifestResourceStream(Type type, string name)
    {
      return this.InternalAssembly.GetManifestResourceStream(type, name);
    }

    /// <summary>Загружает указанный ресурс манифеста из сборки.</summary>
    /// <param name="name">Имя запрашиваемого ресурса манифеста.</param>
    /// <returns>
    ///   <see cref="T:System.IO.Stream" />, представляющий этот ресурс манифеста.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public override Stream GetManifestResourceStream(string name)
    {
      return this.InternalAssembly.GetManifestResourceStream(name);
    }

    /// <summary>Возвращает сведения о сохранении заданного ресурса.</summary>
    /// <param name="resourceName">Имя ресурса.</param>
    /// <returns>
    ///   <see cref="T:System.Reflection.ManifestResourceInfo" />, заполненный сведениями о топологии ресурса, или <see langword="null" />, если ресурс не найден.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
    {
      return this.InternalAssembly.GetManifestResourceInfo(resourceName);
    }

    /// <summary>
    ///   Возвращает расположение (в формате базы кода) загруженного файла, содержащего манифест, если он не является теневой копией.
    /// </summary>
    /// <returns>
    ///   Местоположение загруженного файла, содержащего манифест.
    ///    Если для загруженного файла была создана теневая копия, <see langword="Location" /> является расположением файла до теневого копирования.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public override string Location
    {
      get
      {
        return this.InternalAssembly.Location;
      }
    }

    /// <summary>
    ///   Возвращает версию среды CLR, которая будет сохранена в файле, содержащем манифест.
    /// </summary>
    /// <returns>Строка, представляющая версию среды CLR.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public override string ImageRuntimeVersion
    {
      get
      {
        return this.InternalAssembly.ImageRuntimeVersion;
      }
    }

    /// <summary>
    ///   Получает первоначально заданное расположение сборки (например, в объекте <see cref="T:System.Reflection.AssemblyName" />).
    /// </summary>
    /// <returns>Первоначально заданное расположение сборки.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public override string CodeBase
    {
      get
      {
        return this.InternalAssembly.CodeBase;
      }
    }

    /// <summary>Возвращает точку входа для этой сборки.</summary>
    /// <returns>Точка входа для этой сборки.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public override MethodInfo EntryPoint
    {
      get
      {
        return this.m_assemblyData.m_entryPointMethod;
      }
    }

    /// <summary>
    ///   Возвращает экспортированные типы, определенные в этой сборке.
    /// </summary>
    /// <returns>
    ///   Массив <see cref="T:System.Type" />, содержащий экспортированные типы, определенные в этой сборке.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public override Type[] GetExportedTypes()
    {
      return this.InternalAssembly.GetExportedTypes();
    }

    /// <summary>
    ///   Получает имя <see cref="T:System.Reflection.AssemblyName" />, которое было указано при создании текущей динамической сборки, и устанавливает базу кода в соответствии с указанием.
    /// </summary>
    /// <param name="copiedName">
    ///   Значение <see langword="true" />, чтобы база кода устанавливалась в расположении сборки после создания ее теневой копии; значение <see langword="false" />, чтобы база кода устанавливалась в исходном расположении.
    /// </param>
    /// <returns>Имя динамической сборки.</returns>
    public override AssemblyName GetName(bool copiedName)
    {
      return this.InternalAssembly.GetName(copiedName);
    }

    /// <summary>
    ///   Получает отображаемое имя текущей динамической сборки.
    /// </summary>
    /// <returns>Отображаемое имя текущей динамической сборки.</returns>
    public override string FullName
    {
      get
      {
        return this.InternalAssembly.FullName;
      }
    }

    /// <summary>
    ///   Возвращает указанный тип из типов, которые определены и созданы в текущем <see cref="T:System.Reflection.Emit.AssemblyBuilder" />.
    /// </summary>
    /// <param name="name">Имя искомого типа.</param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" /> для вызова исключения, если тип не найден, в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы игнорировать регистр имени типа при поиске; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Указанный тип или значение <see langword="null" />, если тип не найден или еще не создан.
    /// </returns>
    public override Type GetType(string name, bool throwOnError, bool ignoreCase)
    {
      return this.InternalAssembly.GetType(name, throwOnError, ignoreCase);
    }

    /// <summary>Получает свидетельство для этой сборки.</summary>
    /// <returns>Свидетельство для этой сборки.</returns>
    public override Evidence Evidence
    {
      get
      {
        return this.InternalAssembly.Evidence;
      }
    }

    /// <summary>
    ///   Получает набор разрешений текущей динамической сборки.
    /// </summary>
    /// <returns>Набор разрешений текущей динамической сборки.</returns>
    public override PermissionSet PermissionSet
    {
      [SecurityCritical] get
      {
        return this.InternalAssembly.PermissionSet;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее набор правил безопасности, которые применяются средой CLR к данной сборке.
    /// </summary>
    /// <returns>
    ///   Набор правил безопасности, которые применяются средой CLR к данной сборке.
    /// </returns>
    public override SecurityRuleSet SecurityRuleSet
    {
      get
      {
        return this.InternalAssembly.SecurityRuleSet;
      }
    }

    /// <summary>
    ///   Получает модуль в текущем <see cref="T:System.Reflection.Emit.AssemblyBuilder" />, содержащий манифест сборки.
    /// </summary>
    /// <returns>Модуль манифеста.</returns>
    public override Module ManifestModule
    {
      get
      {
        return (Module) this.m_manifestModuleBuilder.InternalModule;
      }
    }

    /// <summary>
    ///   Получает значение, которое указывает, находится ли эта динамическая сборка в контексте только отражения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если динамическая сборка находится в контексте только отражения; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool ReflectionOnly
    {
      get
      {
        return this.InternalAssembly.ReflectionOnly;
      }
    }

    /// <summary>Получает указанный модуль этой сборки.</summary>
    /// <param name="name">Имя запрошенного модуля.</param>
    /// <returns>
    ///   Запрашиваемый модуль или значение <see langword="null" />, если модуль не найден.
    /// </returns>
    public override Module GetModule(string name)
    {
      return this.InternalAssembly.GetModule(name);
    }

    /// <summary>
    ///   Получает неполный список объектов <see cref="T:System.Reflection.AssemblyName" /> для сборок, на которые ссылается этот <see cref="T:System.Reflection.Emit.AssemblyBuilder" />.
    /// </summary>
    /// <returns>
    ///   Массив имен сборок для связанных сборок.
    ///    Этот массив не является полным списком.
    /// </returns>
    public override AssemblyName[] GetReferencedAssemblies()
    {
      return this.InternalAssembly.GetReferencedAssemblies();
    }

    /// <summary>
    ///   Получает значение, указывающее, была ли сборка загружена из глобального кэша сборок.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="false" />.
    /// </returns>
    public override bool GlobalAssemblyCache
    {
      get
      {
        return this.InternalAssembly.GlobalAssemblyCache;
      }
    }

    /// <summary>
    ///   Получает контекст узла, где создается динамическая сборка.
    /// </summary>
    /// <returns>
    ///   Значение, указывающее контекст узла, где создается динамическая сборка.
    /// </returns>
    public override long HostContext
    {
      get
      {
        return this.InternalAssembly.HostContext;
      }
    }

    /// <summary>
    ///   Получает все модули, входящие в эту сборку, и при необходимости включает модули ресурсов.
    /// </summary>
    /// <param name="getResourceModules">
    ///   Значение <see langword="true" />, если необходимо включать модули ресурсов; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>Модули, являющиеся частью этой сборки.</returns>
    public override Module[] GetModules(bool getResourceModules)
    {
      return this.InternalAssembly.GetModules(getResourceModules);
    }

    /// <summary>
    ///   Возвращает все загруженные модули, входящие в эту сборку, и при необходимости включает модули ресурсов.
    /// </summary>
    /// <param name="getResourceModules">
    ///   Значение <see langword="true" />, если необходимо включать модули ресурсов; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>Загруженные модули, являющиеся частью этой сборки.</returns>
    public override Module[] GetLoadedModules(bool getResourceModules)
    {
      return this.InternalAssembly.GetLoadedModules(getResourceModules);
    }

    /// <summary>
    ///   Получает сопутствующую сборку для указанной культуры.
    /// </summary>
    /// <param name="culture">
    ///   Заданные язык и региональные параметры.
    /// </param>
    /// <returns>Указанная вспомогательная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти сборку.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Вспомогательная сборка с соответствующим именем файла была найдена, но параметр <see langword="CultureInfo" /> не соответствует указанному.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Вспомогательная сборка не является допустимой сборкой.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public override Assembly GetSatelliteAssembly(CultureInfo culture)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalAssembly.InternalGetSatelliteAssembly(culture, (Version) null, ref stackMark);
    }

    /// <summary>
    ///   Получает указанную версию вспомогательной сборки для указанной культуры.
    /// </summary>
    /// <param name="culture">
    ///   Заданные язык и региональные параметры.
    /// </param>
    /// <param name="version">Версия вспомогательной сборки.</param>
    /// <returns>Указанная вспомогательная сборка.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="culture" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Вспомогательная сборка с соответствующим именем файла была найдена, но <see langword="CultureInfo" /> или версия не соответствуют указанным.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти сборку.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Вспомогательная сборка не является допустимой сборкой.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalAssembly.InternalGetSatelliteAssembly(culture, version, ref stackMark);
    }

    /// <summary>
    ///   Получает значение, указывающее, что текущая сборка является динамической.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="true" />.
    /// </returns>
    public override bool IsDynamic
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///   Определяет неуправляемую версию информационного ресурса для этой сборки с учетом заданных спецификаций.
    /// </summary>
    /// <param name="product">
    ///   Имя продукта, с которым поставляется данная сборка.
    /// </param>
    /// <param name="productVersion">
    ///   Версия продукта, с которым поставляется данная сборка.
    /// </param>
    /// <param name="company">
    ///   Название организации, которая является создателем сборки.
    /// </param>
    /// <param name="copyright">
    ///   Описывает все уведомления об авторских правах, товарные знаки и охраняемые товарные знаки, применимые к этой сборке.
    ///    Это должен быть полный текст всех уведомлений, допустимых символов, сроки действия прав, номера товарных знаков и так далее.
    ///    На русском языке эта строка должна быть в формате "© Корпорация Майкрософт (Microsoft Corporation) 1990-2001".
    /// </param>
    /// <param name="trademark">
    ///   Описывает товарные знаки и охраняемые товарные знаки, применимые к этой сборке.
    ///    Это должен быть полный текст всех уведомлений, допустимых символов, номера товарных знаков и так далее.
    ///    На русском языке эта строка должна быть в формате "Windows является товарным знаком корпорации Майкрософт".
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Неуправляемая версия информационного ресурса была определена ранее.
    /// 
    ///   -или-
    /// 
    ///   Объем неуправляемых сведений о версии слишком велик для сохранения.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public void DefineVersionInfoResource(string product, string productVersion, string company, string copyright, string trademark)
    {
      lock (this.SyncRoot)
        this.DefineVersionInfoResourceNoLock(product, productVersion, company, copyright, trademark);
    }

    private void DefineVersionInfoResourceNoLock(string product, string productVersion, string company, string copyright, string trademark)
    {
      if (this.m_assemblyData.m_strResourceFileName != null || this.m_assemblyData.m_resourceBytes != null || this.m_assemblyData.m_nativeVersion != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
      this.m_assemblyData.m_nativeVersion = new NativeVersionInfo();
      this.m_assemblyData.m_nativeVersion.m_strCopyright = copyright;
      this.m_assemblyData.m_nativeVersion.m_strTrademark = trademark;
      this.m_assemblyData.m_nativeVersion.m_strCompany = company;
      this.m_assemblyData.m_nativeVersion.m_strProduct = product;
      this.m_assemblyData.m_nativeVersion.m_strProductVersion = productVersion;
      this.m_assemblyData.m_hasUnmanagedVersionInfo = true;
      this.m_assemblyData.m_OverrideUnmanagedVersionInfo = true;
    }

    /// <summary>
    ///   Определяет неуправляемый ресурс сведений о версии с помощью сведений, указанных в объекте AssemblyName и настраиваемых атрибутах сборки.
    /// </summary>
    /// <exception cref="T:System.ArgumentException">
    ///   Неуправляемый ресурс сведений о версии был определен ранее.
    /// 
    ///   -или-
    /// 
    ///   Объем неуправляемых сведений о версии слишком велик для сохранения.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public void DefineVersionInfoResource()
    {
      lock (this.SyncRoot)
        this.DefineVersionInfoResourceNoLock();
    }

    private void DefineVersionInfoResourceNoLock()
    {
      if (this.m_assemblyData.m_strResourceFileName != null || this.m_assemblyData.m_resourceBytes != null || this.m_assemblyData.m_nativeVersion != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
      this.m_assemblyData.m_hasUnmanagedVersionInfo = true;
      this.m_assemblyData.m_nativeVersion = new NativeVersionInfo();
    }

    /// <summary>
    ///   Определяет неуправляемый ресурс для данной сборки как непрозрачный BLOB-объект байтов.
    /// </summary>
    /// <param name="resource">
    ///   Непрозрачный BLOB-объект байтов, представляющий неуправляемый ресурс.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Неуправляемый ресурс был определен ранее.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="resource" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public void DefineUnmanagedResource(byte[] resource)
    {
      if (resource == null)
        throw new ArgumentNullException(nameof (resource));
      lock (this.SyncRoot)
        this.DefineUnmanagedResourceNoLock(resource);
    }

    private void DefineUnmanagedResourceNoLock(byte[] resource)
    {
      if (this.m_assemblyData.m_strResourceFileName != null || this.m_assemblyData.m_resourceBytes != null || this.m_assemblyData.m_nativeVersion != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
      this.m_assemblyData.m_resourceBytes = new byte[resource.Length];
      Array.Copy((Array) resource, (Array) this.m_assemblyData.m_resourceBytes, resource.Length);
    }

    /// <summary>
    ///   Определяет файл неуправляемого ресурса для данной сборки по заданному имени файла ресурсов.
    /// </summary>
    /// <param name="resourceFileName">Имя файла ресурсов.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Неуправляемый ресурс был определен ранее.
    /// 
    ///   -или-
    /// 
    ///   Файл <paramref name="resourceFileName" /> нечитаем.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="resourceFileName" /> является пустой строкой ("").
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="resourceFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="resourceFileName" /> не найден.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="resourceFileName" /> является каталогом.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public void DefineUnmanagedResource(string resourceFileName)
    {
      if (resourceFileName == null)
        throw new ArgumentNullException(nameof (resourceFileName));
      lock (this.SyncRoot)
        this.DefineUnmanagedResourceNoLock(resourceFileName);
    }

    [SecurityCritical]
    private void DefineUnmanagedResourceNoLock(string resourceFileName)
    {
      if (this.m_assemblyData.m_strResourceFileName != null || this.m_assemblyData.m_resourceBytes != null || this.m_assemblyData.m_nativeVersion != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
      string str = this.m_assemblyData.m_strDir != null ? Path.Combine(this.m_assemblyData.m_strDir, resourceFileName) : Path.Combine(Environment.CurrentDirectory, resourceFileName);
      string fullPath = Path.GetFullPath(resourceFileName);
      new FileIOPermission(FileIOPermissionAccess.Read, fullPath).Demand();
      if (!File.Exists(fullPath))
        throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound_FileName", (object) resourceFileName), resourceFileName);
      this.m_assemblyData.m_strResourceFileName = fullPath;
    }

    /// <summary>Возвращает динамический модуль с указанным именем.</summary>
    /// <param name="name">Имя запрошенного динамического модуля.</param>
    /// <returns>
    ///   Объект ModuleBuilder, представляющий запрошенный динамический модуль.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public ModuleBuilder GetDynamicModule(string name)
    {
      lock (this.SyncRoot)
        return this.GetDynamicModuleNoLock(name);
    }

    private ModuleBuilder GetDynamicModuleNoLock(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      int count = this.m_assemblyData.m_moduleBuilderList.Count;
      for (int index = 0; index < count; ++index)
      {
        ModuleBuilder moduleBuilder = this.m_assemblyData.m_moduleBuilderList[index];
        if (moduleBuilder.m_moduleData.m_strModuleName.Equals(name))
          return moduleBuilder;
      }
      return (ModuleBuilder) null;
    }

    /// <summary>
    ///   Задает точку входа для этой динамической сборки при условии, что выполняется сборка консольного приложения.
    /// </summary>
    /// <param name="entryMethod">
    ///   Ссылка на метод, представляющий точку входа для этой динамической сборки.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="entryMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод <paramref name="entryMethod" /> не содержится в данной сборке.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public void SetEntryPoint(MethodInfo entryMethod)
    {
      this.SetEntryPoint(entryMethod, PEFileKinds.ConsoleApplication);
    }

    /// <summary>
    ///   Задает точку входа для этой сборки и определяет тип переносимого исполняемого файла (PE-файла), построение которого выполняется.
    /// </summary>
    /// <param name="entryMethod">
    ///   Ссылка на метод, представляющий точку входа для этой динамической сборки.
    /// </param>
    /// <param name="fileKind">
    ///   Тип исполняемого файла сборки, построение которого выполняется.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="entryMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод <paramref name="entryMethod" /> не содержится в данной сборке.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public void SetEntryPoint(MethodInfo entryMethod, PEFileKinds fileKind)
    {
      lock (this.SyncRoot)
        this.SetEntryPointNoLock(entryMethod, fileKind);
    }

    private void SetEntryPointNoLock(MethodInfo entryMethod, PEFileKinds fileKind)
    {
      if (entryMethod == (MethodInfo) null)
        throw new ArgumentNullException(nameof (entryMethod));
      Module module = entryMethod.Module;
      if (module == (Module) null || !this.InternalAssembly.Equals((object) module.Assembly))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EntryMethodNotDefinedInAssembly"));
      this.m_assemblyData.m_entryPointMethod = entryMethod;
      this.m_assemblyData.m_peFileKind = fileKind;
      ModuleBuilder moduleBuilder = module as ModuleBuilder;
      this.m_assemblyData.m_entryPointModule = !((Module) moduleBuilder != (Module) null) ? this.GetModuleBuilder((InternalModuleBuilder) module) : moduleBuilder;
      this.m_assemblyData.m_entryPointModule.SetEntryPoint(this.m_assemblyData.m_entryPointModule.GetMethodToken(entryMethod));
    }

    /// <summary>
    ///   Задает настраиваемый атрибут для этой сборки с помощью большого двоичного объекта настраиваемого атрибута.
    /// </summary>
    /// <param name="con">Конструктор настраиваемого атрибута.</param>
    /// <param name="binaryAttribute">
    ///   Большой двоичный объект байтов, представляющий атрибуты.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="con" /> или <paramref name="binaryAttribute" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="con" /> не является объектом <see langword="RuntimeConstructorInfo" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (con));
      if (binaryAttribute == null)
        throw new ArgumentNullException(nameof (binaryAttribute));
      lock (this.SyncRoot)
        this.SetCustomAttributeNoLock(con, binaryAttribute);
    }

    [SecurityCritical]
    private void SetCustomAttributeNoLock(ConstructorInfo con, byte[] binaryAttribute)
    {
      TypeBuilder.DefineCustomAttribute(this.m_manifestModuleBuilder, 536870913, this.m_manifestModuleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, typeof (DebuggableAttribute) == con.DeclaringType);
      if (this.m_assemblyData.m_access == AssemblyBuilderAccess.Run)
        return;
      this.m_assemblyData.AddCustomAttribute(con, binaryAttribute);
    }

    /// <summary>
    ///   Задает настраиваемый атрибут для этой сборки с помощью построителя настраиваемого атрибута.
    /// </summary>
    /// <param name="customBuilder">
    ///   Экземпляр вспомогательного класса для определения настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="con" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException(nameof (customBuilder));
      lock (this.SyncRoot)
        this.SetCustomAttributeNoLock(customBuilder);
    }

    [SecurityCritical]
    private void SetCustomAttributeNoLock(CustomAttributeBuilder customBuilder)
    {
      customBuilder.CreateCustomAttribute(this.m_manifestModuleBuilder, 536870913);
      if (this.m_assemblyData.m_access == AssemblyBuilderAccess.Run)
        return;
      this.m_assemblyData.AddCustomAttribute(customBuilder);
    }

    /// <summary>Сохраняет динамическую сборку на диск.</summary>
    /// <param name="assemblyFileName">Имя файла сборки.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина <paramref name="assemblyFileName" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   В сборке есть два или более файлов ресурсов модулей с тем же именем.
    /// 
    ///   -или-
    /// 
    ///   Целевой каталог сборки недопустим.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="assemblyFileName" /> не является простым именем файла (например, содержит каталог или букву диска), или в этой сборке определено несколько неуправляемых ресурсов, включая ресурс сведений о версии.
    /// 
    ///   -или-
    /// 
    ///   Строка <see langword="CultureInfo" /> в <see cref="T:System.Reflection.AssemblyCultureAttribute" /> не является допустимой строкой, и <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource(System.String,System.String,System.String,System.String,System.String)" /> был вызван до вызова этого метода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Эта сборка была сохранена ранее.
    /// 
    ///   -или-
    /// 
    ///   Эта сборка имеет доступ <see langword="Run" /><see cref="T:System.Reflection.Emit.AssemblyBuilderAccess" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Во время сохранения возникает ошибка вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> не был вызван для какого-либо типа в модулях сборки, которые должны быть записаны на диск.
    /// </exception>
    public void Save(string assemblyFileName)
    {
      this.Save(assemblyFileName, PortableExecutableKinds.ILOnly, ImageFileMachine.I386);
    }

    /// <summary>
    ///   Сохраняет эту динамическую сборку на диске, указывая природу кода в исполняемых файлах сборки и целевую платформу.
    /// </summary>
    /// <param name="assemblyFileName">Имя файла сборки.</param>
    /// <param name="portableExecutableKind">
    ///   Побитовое сочетание значений <see cref="T:System.Reflection.PortableExecutableKinds" />, определяющее природу кода.
    /// </param>
    /// <param name="imageFileMachine">
    ///   Одно из значений <see cref="T:System.Reflection.ImageFileMachine" />, определяющее целевую платформу.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина <paramref name="assemblyFileName" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   В сборке есть два или более файлов ресурсов модулей с тем же именем.
    /// 
    ///   -или-
    /// 
    ///   Целевой каталог сборки недопустим.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="assemblyFileName" /> не является простым именем файла (например, содержит каталог или букву диска), или в этой сборке определено несколько неуправляемых ресурсов, включая ресурсы сведений о версии.
    /// 
    ///   -или-
    /// 
    ///   Строка <see langword="CultureInfo" /> в <see cref="T:System.Reflection.AssemblyCultureAttribute" /> не является допустимой строкой, и <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource(System.String,System.String,System.String,System.String,System.String)" /> был вызван до вызова этого метода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assemblyFileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Эта сборка была сохранена ранее.
    /// 
    ///   -или-
    /// 
    ///   Эта сборка имеет доступ <see langword="Run" /><see cref="T:System.Reflection.Emit.AssemblyBuilderAccess" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Во время сохранения возникает ошибка вывода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> не был вызван для какого-либо типа в модулях сборки, которые должны быть записаны на диск.
    /// </exception>
    [SecuritySafeCritical]
    public void Save(string assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
    {
      lock (this.SyncRoot)
        this.SaveNoLock(assemblyFileName, portableExecutableKind, imageFileMachine);
    }

    [SecurityCritical]
    private void SaveNoLock(string assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
    {
      if (assemblyFileName == null)
        throw new ArgumentNullException(nameof (assemblyFileName));
      if (assemblyFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), nameof (assemblyFileName));
      if (!string.Equals(assemblyFileName, Path.GetFileName(assemblyFileName)))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotSimpleFileName"), nameof (assemblyFileName));
      int[] numArray1 = (int[]) null;
      int[] numArray2 = (int[]) null;
      string s = (string) null;
      try
      {
        if (this.m_assemblyData.m_iCABuilder != 0)
          numArray1 = new int[this.m_assemblyData.m_iCABuilder];
        if (this.m_assemblyData.m_iCAs != 0)
          numArray2 = new int[this.m_assemblyData.m_iCAs];
        if (this.m_assemblyData.m_isSaved)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AssemblyHasBeenSaved", (object) this.InternalAssembly.GetSimpleName()));
        if ((this.m_assemblyData.m_access & AssemblyBuilderAccess.Save) != AssemblyBuilderAccess.Save)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CantSaveTransientAssembly"));
        ModuleBuilder moduleWithFileName = this.m_assemblyData.FindModuleWithFileName(assemblyFileName);
        if ((Module) moduleWithFileName != (Module) null)
        {
          this.m_onDiskAssemblyModuleBuilder = moduleWithFileName;
          moduleWithFileName.m_moduleData.FileToken = 0;
        }
        else
          this.m_assemblyData.CheckFileNameConflict(assemblyFileName);
        if (this.m_assemblyData.m_strDir == null)
          this.m_assemblyData.m_strDir = Environment.CurrentDirectory;
        else if (!Directory.Exists(this.m_assemblyData.m_strDir))
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectory", (object) this.m_assemblyData.m_strDir));
        assemblyFileName = Path.Combine(this.m_assemblyData.m_strDir, assemblyFileName);
        assemblyFileName = Path.GetFullPath(assemblyFileName);
        new FileIOPermission(FileIOPermissionAccess.Write | FileIOPermissionAccess.Append, assemblyFileName).Demand();
        if ((Module) moduleWithFileName != (Module) null)
        {
          for (int index = 0; index < this.m_assemblyData.m_iCABuilder; ++index)
            numArray1[index] = this.m_assemblyData.m_CABuilders[index].PrepareCreateCustomAttributeToDisk(moduleWithFileName);
          for (int index = 0; index < this.m_assemblyData.m_iCAs; ++index)
            numArray2[index] = moduleWithFileName.InternalGetConstructorToken(this.m_assemblyData.m_CACons[index], true).Token;
          moduleWithFileName.PreSave(assemblyFileName, portableExecutableKind, imageFileMachine);
        }
        AssemblyBuilder.PrepareForSavingManifestToDisk(this.GetNativeHandle(), (Module) moduleWithFileName != (Module) null ? moduleWithFileName.ModuleHandle.GetRuntimeModule() : (RuntimeModule) null);
        ModuleBuilder assemblyModuleBuilder = this.GetOnDiskAssemblyModuleBuilder();
        if (this.m_assemblyData.m_strResourceFileName != null)
          assemblyModuleBuilder.DefineUnmanagedResourceFileInternalNoLock(this.m_assemblyData.m_strResourceFileName);
        else if (this.m_assemblyData.m_resourceBytes != null)
          assemblyModuleBuilder.DefineUnmanagedResourceInternalNoLock(this.m_assemblyData.m_resourceBytes);
        else if (this.m_assemblyData.m_hasUnmanagedVersionInfo)
        {
          this.m_assemblyData.FillUnmanagedVersionInfo();
          string fileVersion = this.m_assemblyData.m_nativeVersion.m_strFileVersion ?? this.GetVersion().ToString();
          AssemblyBuilder.CreateVersionInfoResource(assemblyFileName, this.m_assemblyData.m_nativeVersion.m_strTitle, (string) null, this.m_assemblyData.m_nativeVersion.m_strDescription, this.m_assemblyData.m_nativeVersion.m_strCopyright, this.m_assemblyData.m_nativeVersion.m_strTrademark, this.m_assemblyData.m_nativeVersion.m_strCompany, this.m_assemblyData.m_nativeVersion.m_strProduct, this.m_assemblyData.m_nativeVersion.m_strProductVersion, fileVersion, this.m_assemblyData.m_nativeVersion.m_lcid, this.m_assemblyData.m_peFileKind == PEFileKinds.Dll, JitHelpers.GetStringHandleOnStack(ref s));
          assemblyModuleBuilder.DefineUnmanagedResourceFileInternalNoLock(s);
        }
        if ((Module) moduleWithFileName == (Module) null)
        {
          for (int index = 0; index < this.m_assemblyData.m_iCABuilder; ++index)
            numArray1[index] = this.m_assemblyData.m_CABuilders[index].PrepareCreateCustomAttributeToDisk(assemblyModuleBuilder);
          for (int index = 0; index < this.m_assemblyData.m_iCAs; ++index)
            numArray2[index] = assemblyModuleBuilder.InternalGetConstructorToken(this.m_assemblyData.m_CACons[index], true).Token;
        }
        int count1 = this.m_assemblyData.m_moduleBuilderList.Count;
        for (int index = 0; index < count1; ++index)
        {
          ModuleBuilder moduleBuilder = this.m_assemblyData.m_moduleBuilderList[index];
          if (!moduleBuilder.IsTransient() && (Module) moduleBuilder != (Module) moduleWithFileName)
          {
            string str = moduleBuilder.m_moduleData.m_strFileName;
            if (this.m_assemblyData.m_strDir != null)
              str = Path.GetFullPath(Path.Combine(this.m_assemblyData.m_strDir, str));
            new FileIOPermission(FileIOPermissionAccess.Write | FileIOPermissionAccess.Append, str).Demand();
            moduleBuilder.m_moduleData.FileToken = AssemblyBuilder.AddFile(this.GetNativeHandle(), moduleBuilder.m_moduleData.m_strFileName);
            moduleBuilder.PreSave(str, portableExecutableKind, imageFileMachine);
            moduleBuilder.Save(str, false, portableExecutableKind, imageFileMachine);
            AssemblyBuilder.SetFileHashValue(this.GetNativeHandle(), moduleBuilder.m_moduleData.FileToken, str);
          }
        }
        for (int index = 0; index < this.m_assemblyData.m_iPublicComTypeCount; ++index)
        {
          Type publicComType = this.m_assemblyData.m_publicComTypeList[index];
          if ((object) (publicComType as RuntimeType) != null)
          {
            ModuleBuilder moduleBuilder = this.GetModuleBuilder((InternalModuleBuilder) publicComType.Module);
            if ((Module) moduleBuilder != (Module) moduleWithFileName)
              this.DefineNestedComType(publicComType, moduleBuilder.m_moduleData.FileToken, publicComType.MetadataToken);
          }
          else
          {
            TypeBuilder typeBuilder = (TypeBuilder) publicComType;
            ModuleBuilder moduleBuilder = typeBuilder.GetModuleBuilder();
            if ((Module) moduleBuilder != (Module) moduleWithFileName)
              this.DefineNestedComType(publicComType, moduleBuilder.m_moduleData.FileToken, typeBuilder.MetadataTokenInternal);
          }
        }
        if ((Module) assemblyModuleBuilder != (Module) this.m_manifestModuleBuilder)
        {
          for (int index = 0; index < this.m_assemblyData.m_iCABuilder; ++index)
            this.m_assemblyData.m_CABuilders[index].CreateCustomAttribute(assemblyModuleBuilder, 536870913, numArray1[index], true);
          for (int index = 0; index < this.m_assemblyData.m_iCAs; ++index)
            TypeBuilder.DefineCustomAttribute(assemblyModuleBuilder, 536870913, numArray2[index], this.m_assemblyData.m_CABytes[index], true, false);
        }
        if (this.m_assemblyData.m_RequiredPset != null)
          this.AddDeclarativeSecurity(this.m_assemblyData.m_RequiredPset, SecurityAction.RequestMinimum);
        if (this.m_assemblyData.m_RefusedPset != null)
          this.AddDeclarativeSecurity(this.m_assemblyData.m_RefusedPset, SecurityAction.RequestRefuse);
        if (this.m_assemblyData.m_OptionalPset != null)
          this.AddDeclarativeSecurity(this.m_assemblyData.m_OptionalPset, SecurityAction.RequestOptional);
        int count2 = this.m_assemblyData.m_resWriterList.Count;
        for (int index = 0; index < count2; ++index)
        {
          ResWriterData resWriterData = (ResWriterData) null;
          try
          {
            resWriterData = this.m_assemblyData.m_resWriterList[index];
            if (resWriterData.m_resWriter != null)
              new FileIOPermission(FileIOPermissionAccess.Write | FileIOPermissionAccess.Append, resWriterData.m_strFullFileName).Demand();
          }
          finally
          {
            if (resWriterData != null && resWriterData.m_resWriter != null)
              resWriterData.m_resWriter.Close();
          }
          AssemblyBuilder.AddStandAloneResource(this.GetNativeHandle(), resWriterData.m_strName, resWriterData.m_strFileName, resWriterData.m_strFullFileName, (int) resWriterData.m_attribute);
        }
        if ((Module) moduleWithFileName == (Module) null)
        {
          assemblyModuleBuilder.DefineNativeResource(portableExecutableKind, imageFileMachine);
          int entryPoint = (Module) this.m_assemblyData.m_entryPointModule != (Module) null ? this.m_assemblyData.m_entryPointModule.m_moduleData.FileToken : 0;
          AssemblyBuilder.SaveManifestToDisk(this.GetNativeHandle(), assemblyFileName, entryPoint, (int) this.m_assemblyData.m_peFileKind, (int) portableExecutableKind, (int) imageFileMachine);
        }
        else
        {
          if ((Module) this.m_assemblyData.m_entryPointModule != (Module) null && (Module) this.m_assemblyData.m_entryPointModule != (Module) moduleWithFileName)
            moduleWithFileName.SetEntryPoint(new MethodToken(this.m_assemblyData.m_entryPointModule.m_moduleData.FileToken));
          moduleWithFileName.Save(assemblyFileName, true, portableExecutableKind, imageFileMachine);
        }
        this.m_assemblyData.m_isSaved = true;
      }
      finally
      {
        if (s != null)
          File.Delete(s);
      }
    }

    [SecurityCritical]
    private void AddDeclarativeSecurity(PermissionSet pset, SecurityAction action)
    {
      byte[] blob = pset.EncodeXml();
      AssemblyBuilder.AddDeclarativeSecurity(this.GetNativeHandle(), action, blob, blob.Length);
    }

    internal bool IsPersistable()
    {
      return (this.m_assemblyData.m_access & AssemblyBuilderAccess.Save) == AssemblyBuilderAccess.Save;
    }

    [SecurityCritical]
    private int DefineNestedComType(Type type, int tkResolutionScope, int tkTypeDef)
    {
      Type declaringType = type.DeclaringType;
      if (declaringType == (Type) null)
        return AssemblyBuilder.AddExportedTypeOnDisk(this.GetNativeHandle(), type.FullName, tkResolutionScope, tkTypeDef, type.Attributes);
      tkResolutionScope = this.DefineNestedComType(declaringType, tkResolutionScope, tkTypeDef);
      return AssemblyBuilder.AddExportedTypeOnDisk(this.GetNativeHandle(), type.Name, tkResolutionScope, tkTypeDef, type.Attributes);
    }

    [SecurityCritical]
    internal int DefineExportedTypeInMemory(Type type, int tkResolutionScope, int tkTypeDef)
    {
      Type declaringType = type.DeclaringType;
      if (declaringType == (Type) null)
        return AssemblyBuilder.AddExportedTypeInMemory(this.GetNativeHandle(), type.FullName, tkResolutionScope, tkTypeDef, type.Attributes);
      tkResolutionScope = this.DefineExportedTypeInMemory(declaringType, tkResolutionScope, tkTypeDef);
      return AssemblyBuilder.AddExportedTypeInMemory(this.GetNativeHandle(), type.Name, tkResolutionScope, tkTypeDef, type.Attributes);
    }

    private AssemblyBuilder()
    {
    }

    void _AssemblyBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _AssemblyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _AssemblyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _AssemblyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DefineDynamicModule(RuntimeAssembly containingAssembly, bool emitSymbolInfo, string name, string filename, StackCrawlMarkHandle stackMark, ref IntPtr pInternalSymWriter, ObjectHandleOnStack retModule, bool fIsTransient, out int tkFile);

    [SecurityCritical]
    private static Module DefineDynamicModule(RuntimeAssembly containingAssembly, bool emitSymbolInfo, string name, string filename, ref StackCrawlMark stackMark, ref IntPtr pInternalSymWriter, bool fIsTransient, out int tkFile)
    {
      RuntimeModule o = (RuntimeModule) null;
      AssemblyBuilder.DefineDynamicModule(containingAssembly.GetNativeHandle(), emitSymbolInfo, name, filename, JitHelpers.GetStackCrawlMarkHandle(ref stackMark), ref pInternalSymWriter, JitHelpers.GetObjectHandleOnStack<RuntimeModule>(ref o), fIsTransient, out tkFile);
      return (Module) o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void PrepareForSavingManifestToDisk(RuntimeAssembly assembly, RuntimeModule assemblyModule);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SaveManifestToDisk(RuntimeAssembly assembly, string strFileName, int entryPoint, int fileKind, int portableExecutableKind, int ImageFileMachine);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int AddFile(RuntimeAssembly assembly, string strFileName);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetFileHashValue(RuntimeAssembly assembly, int tkFile, string strFullFileName);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int AddExportedTypeInMemory(RuntimeAssembly assembly, string strComTypeName, int tkAssemblyRef, int tkTypeDef, TypeAttributes flags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int AddExportedTypeOnDisk(RuntimeAssembly assembly, string strComTypeName, int tkAssemblyRef, int tkTypeDef, TypeAttributes flags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddStandAloneResource(RuntimeAssembly assembly, string strName, string strFileName, string strFullFileName, int attribute);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddDeclarativeSecurity(RuntimeAssembly assembly, SecurityAction action, byte[] blob, int length);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void CreateVersionInfoResource(string filename, string title, string iconFilename, string description, string copyright, string trademark, string company, string product, string productVersion, string fileVersion, int lcid, bool isDll, StringHandleOnStack retFileName);

    private class AssemblyBuilderLock
    {
    }
  }
}
