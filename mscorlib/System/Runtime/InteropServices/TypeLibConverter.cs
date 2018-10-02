// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibConverter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.TCEAdapterGen;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет набор служб, преобразующих управляемую сборку в библиотеку типов COM и наоборот.
  /// </summary>
  [Guid("F1C3BF79-C3E4-11d3-88E7-00902754C43A")]
  [ClassInterface(ClassInterfaceType.None)]
  [ComVisible(true)]
  public sealed class TypeLibConverter : ITypeLibConverter
  {
    private const string s_strTypeLibAssemblyTitlePrefix = "TypeLib ";
    private const string s_strTypeLibAssemblyDescPrefix = "Assembly generated from typelib ";
    private const int MAX_NAMESPACE_LENGTH = 1024;

    /// <summary>Преобразует библиотеку типов COM в сборку.</summary>
    /// <param name="typeLib">
    ///   Объект, реализующий интерфейс <see langword="ITypeLib" />.
    /// </param>
    /// <param name="asmFileName">Имя файла итоговой сборки.</param>
    /// <param name="flags">
    ///   Объект <see cref="T:System.Runtime.InteropServices.TypeLibImporterFlags" /> показывающее специальные параметры.
    /// </param>
    /// <param name="notifySink">
    ///   <see cref="T:System.Runtime.InteropServices.ITypeLibImporterNotifySink" /> интерфейс, реализованный вызывающим объектом.
    /// </param>
    /// <param name="publicKey">
    ///   A <see langword="byte" /> массив, содержащий открытый ключ.
    /// </param>
    /// <param name="keyPair">
    ///   Объект <see cref="T:System.Reflection.StrongNameKeyPair" /> объект, содержащий пару открытого и закрытого криптографических ключей.
    /// </param>
    /// <param name="unsafeInterfaces">
    ///   Если <see langword="true" />, для интерфейса необходимы проверки времени компоновки для <see cref="F:System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode" /> разрешение.
    ///    Если <see langword="false" />, интерфейсов необходимы проверки во время выполнения, для которых требуется стека становятся более ресурсоемкими, помогают обеспечить большую защищенность.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> Объект, содержащий преобразованную библиотеку типов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeLib" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="asmFileName" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="notifySink" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="asmFileName" /> равен пустой строке.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="asmFileName" /> превышает MAX_PATH.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="flags" /> не <see cref="F:System.Runtime.InteropServices.TypeLibImporterFlags.PrimaryInteropAssembly" />.
    /// 
    ///   -или-
    /// 
    ///   Параметрам <paramref name="publicKey" /> и <paramref name="keyPair" /> присвоено значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">
    ///   Созданные метаданные содержат ошибки, предотвращая загрузки любых типов.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, int flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, bool unsafeInterfaces)
    {
      return this.ConvertTypeLibToAssembly(typeLib, asmFileName, unsafeInterfaces ? TypeLibImporterFlags.UnsafeInterfaces : TypeLibImporterFlags.None, notifySink, publicKey, keyPair, (string) null, (Version) null);
    }

    /// <summary>Преобразует библиотеку типов COM в сборку.</summary>
    /// <param name="typeLib">
    ///   Объект, реализующий интерфейс <see langword="ITypeLib" />.
    /// </param>
    /// <param name="asmFileName">Имя файла итоговой сборки.</param>
    /// <param name="flags">
    ///   Объект <see cref="T:System.Runtime.InteropServices.TypeLibImporterFlags" /> показывающее специальные параметры.
    /// </param>
    /// <param name="notifySink">
    ///   <see cref="T:System.Runtime.InteropServices.ITypeLibImporterNotifySink" /> интерфейс, реализованный вызывающим объектом.
    /// </param>
    /// <param name="publicKey">
    ///   A <see langword="byte" /> массив, содержащий открытый ключ.
    /// </param>
    /// <param name="keyPair">
    ///   Объект <see cref="T:System.Reflection.StrongNameKeyPair" /> объект, содержащий пару открытого и закрытого криптографических ключей.
    /// </param>
    /// <param name="asmNamespace">
    ///   Пространство имен для итоговой сборки.
    /// </param>
    /// <param name="asmVersion">
    ///   Версия итоговой сборки.
    ///    Если <see langword="null" />, используется версия библиотеки типов.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> Объект, содержащий преобразованную библиотеку типов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeLib" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="asmFileName" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="notifySink" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="asmFileName" /> равен пустой строке.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="asmFileName" /> превышает MAX_PATH.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="flags" /> не <see cref="F:System.Runtime.InteropServices.TypeLibImporterFlags.PrimaryInteropAssembly" />.
    /// 
    ///   -или-
    /// 
    ///   Параметрам <paramref name="publicKey" /> и <paramref name="keyPair" /> присвоено значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">
    ///   Созданные метаданные содержат ошибки, предотвращая загрузки любых типов.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, string asmNamespace, Version asmVersion)
    {
      if (typeLib == null)
        throw new ArgumentNullException(nameof (typeLib));
      if (asmFileName == null)
        throw new ArgumentNullException(nameof (asmFileName));
      if (notifySink == null)
        throw new ArgumentNullException(nameof (notifySink));
      if (string.Empty.Equals(asmFileName))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileName"), nameof (asmFileName));
      if (asmFileName.Length > 260)
        throw new ArgumentException(Environment.GetResourceString("IO.PathTooLong"), asmFileName);
      if ((flags & TypeLibImporterFlags.PrimaryInteropAssembly) != TypeLibImporterFlags.None && publicKey == null && keyPair == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_PIAMustBeStrongNamed"));
      ArrayList eventItfInfoList = (ArrayList) null;
      AssemblyNameFlags asmNameFlags = AssemblyNameFlags.None;
      AssemblyName assemblyNameFromTypelib = TypeLibConverter.GetAssemblyNameFromTypelib(typeLib, asmFileName, publicKey, keyPair, asmVersion, asmNameFlags);
      AssemblyBuilder assemblyForTypeLib = TypeLibConverter.CreateAssemblyForTypeLib(typeLib, asmFileName, assemblyNameFromTypelib, (uint) (flags & TypeLibImporterFlags.PrimaryInteropAssembly) > 0U, (uint) (flags & TypeLibImporterFlags.ReflectionOnlyLoading) > 0U, (uint) (flags & TypeLibImporterFlags.NoDefineVersionResource) > 0U);
      string fileName = Path.GetFileName(asmFileName);
      ModuleBuilder moduleBuilder = assemblyForTypeLib.DefineDynamicModule(fileName, fileName);
      if (asmNamespace == null)
        asmNamespace = assemblyNameFromTypelib.Name;
      TypeLibConverter.TypeResolveHandler typeResolveHandler = new TypeLibConverter.TypeResolveHandler(moduleBuilder, notifySink);
      AppDomain domain = Thread.GetDomain();
      ResolveEventHandler resolveEventHandler1 = new ResolveEventHandler(typeResolveHandler.ResolveEvent);
      ResolveEventHandler resolveEventHandler2 = new ResolveEventHandler(typeResolveHandler.ResolveAsmEvent);
      ResolveEventHandler resolveEventHandler3 = new ResolveEventHandler(typeResolveHandler.ResolveROAsmEvent);
      domain.TypeResolve += resolveEventHandler1;
      domain.AssemblyResolve += resolveEventHandler2;
      domain.ReflectionOnlyAssemblyResolve += resolveEventHandler3;
      TypeLibConverter.nConvertTypeLibToMetadata(typeLib, (RuntimeAssembly) assemblyForTypeLib.InternalAssembly, (RuntimeModule) moduleBuilder.InternalModule, asmNamespace, flags, (ITypeLibImporterNotifySink) typeResolveHandler, out eventItfInfoList);
      TypeLibConverter.UpdateComTypesInAssembly(assemblyForTypeLib, moduleBuilder);
      if (eventItfInfoList.Count > 0)
        new TCEAdapterGenerator().Process(moduleBuilder, eventItfInfoList);
      domain.TypeResolve -= resolveEventHandler1;
      domain.AssemblyResolve -= resolveEventHandler2;
      domain.ReflectionOnlyAssemblyResolve -= resolveEventHandler3;
      return assemblyForTypeLib;
    }

    /// <summary>Преобразует сборку в библиотеку COM-типов.</summary>
    /// <param name="assembly">Сборка для преобразования.</param>
    /// <param name="strTypeLibName">
    ///   Имя файла итоговой библиотеки типов.
    /// </param>
    /// <param name="flags">
    ///   Объект <see cref="T:System.Runtime.InteropServices.TypeLibExporterFlags" /> показывающее специальные параметры.
    /// </param>
    /// <param name="notifySink">
    ///   <see cref="T:System.Runtime.InteropServices.ITypeLibExporterNotifySink" /> Интерфейс, реализованный вызывающим объектом.
    /// </param>
    /// <returns>
    ///   Объект, реализующий интерфейс <see langword="ITypeLib" />.
    /// </returns>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public object ConvertAssemblyToTypeLib(Assembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink)
    {
      AssemblyBuilder assemblyBuilder = assembly as AssemblyBuilder;
      return TypeLibConverter.nConvertAssemblyToTypeLib(!((Assembly) assemblyBuilder != (Assembly) null) ? assembly as RuntimeAssembly : (RuntimeAssembly) assemblyBuilder.InternalAssembly, strTypeLibName, flags, notifySink);
    }

    /// <summary>
    ///   Возвращает имя и базу кода основной сборки взаимодействия для указанной библиотеки типов.
    /// </summary>
    /// <param name="g">Идентификатор GUID библиотеки типов.</param>
    /// <param name="major">
    ///   Основной номер версии библиотеки типов.
    /// </param>
    /// <param name="minor">
    ///   Дополнительный номер версии библиотеки типов.
    /// </param>
    /// <param name="lcid">Идентификатор LCID библиотеки типов.</param>
    /// <param name="asmName">
    ///   При удачном возвращении имя основной сборки взаимодействия, связанной с <paramref name="g" />.
    /// </param>
    /// <param name="asmCodeBase">
    ///   При удачном возвращении база кода основной сборки взаимодействия связано с <paramref name="g" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если основная сборка взаимодействия был найден в реестре; в противном случае <see langword="false" />.
    /// </returns>
    public bool GetPrimaryInteropAssembly(Guid g, int major, int minor, int lcid, out string asmName, out string asmCodeBase)
    {
      string name1 = "{" + g.ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      string name2 = major.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture) + "." + minor.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture);
      asmName = (string) null;
      asmCodeBase = (string) null;
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("TypeLib", false))
      {
        if (registryKey1 != null)
        {
          using (RegistryKey registryKey2 = registryKey1.OpenSubKey(name1))
          {
            if (registryKey2 != null)
            {
              using (RegistryKey registryKey3 = registryKey2.OpenSubKey(name2, false))
              {
                if (registryKey3 != null)
                {
                  asmName = (string) registryKey3.GetValue("PrimaryInteropAssemblyName");
                  asmCodeBase = (string) registryKey3.GetValue("PrimaryInteropAssemblyCodeBase");
                }
              }
            }
          }
        }
      }
      return asmName != null;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static AssemblyBuilder CreateAssemblyForTypeLib(object typeLib, string asmFileName, AssemblyName asmName, bool bPrimaryInteropAssembly, bool bReflectionOnly, bool bNoDefineVersionResource)
    {
      AppDomain domain = Thread.GetDomain();
      string dir = (string) null;
      if (asmFileName != null)
      {
        dir = Path.GetDirectoryName(asmFileName);
        if (string.IsNullOrEmpty(dir))
          dir = (string) null;
      }
      AssemblyBuilderAccess access = !bReflectionOnly ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.ReflectionOnly;
      AssemblyBuilder asmBldr = domain.DefineDynamicAssembly(asmName, access, dir, false, (System.Collections.Generic.IEnumerable<CustomAttributeBuilder>) new List<CustomAttributeBuilder>()
      {
        new CustomAttributeBuilder(typeof (SecurityRulesAttribute).GetConstructor(new Type[1]
        {
          typeof (SecurityRuleSet)
        }), new object[1]{ (object) SecurityRuleSet.Level2 })
      });
      TypeLibConverter.SetGuidAttributeOnAssembly(asmBldr, typeLib);
      TypeLibConverter.SetImportedFromTypeLibAttrOnAssembly(asmBldr, typeLib);
      if (bNoDefineVersionResource)
        TypeLibConverter.SetTypeLibVersionAttribute(asmBldr, typeLib);
      else
        TypeLibConverter.SetVersionInformation(asmBldr, typeLib, asmName);
      if (bPrimaryInteropAssembly)
        TypeLibConverter.SetPIAAttributeOnAssembly(asmBldr, typeLib);
      return asmBldr;
    }

    [SecurityCritical]
    internal static AssemblyName GetAssemblyNameFromTypelib(object typeLib, string asmFileName, byte[] publicKey, StrongNameKeyPair keyPair, Version asmVersion, AssemblyNameFlags asmNameFlags)
    {
      string strName = (string) null;
      string strDocString = (string) null;
      int dwHelpContext = 0;
      string strHelpFile = (string) null;
      ITypeLib typeLibrary = (ITypeLib) typeLib;
      typeLibrary.GetDocumentation(-1, out strName, out strDocString, out dwHelpContext, out strHelpFile);
      if (asmFileName == null)
      {
        asmFileName = strName;
      }
      else
      {
        string fileName = Path.GetFileName(asmFileName);
        if (!".dll".Equals(Path.GetExtension(asmFileName), StringComparison.OrdinalIgnoreCase))
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileExtension"));
        asmFileName = fileName.Substring(0, fileName.Length - ".dll".Length);
      }
      if (asmVersion == (Version) null)
      {
        int major;
        int minor;
        Marshal.GetTypeLibVersion(typeLibrary, out major, out minor);
        asmVersion = new Version(major, minor, 0, 0);
      }
      AssemblyName assemblyName = new AssemblyName();
      assemblyName.Init(asmFileName, publicKey, (byte[]) null, asmVersion, (CultureInfo) null, AssemblyHashAlgorithm.None, AssemblyVersionCompatibility.SameMachine, (string) null, asmNameFlags, keyPair);
      return assemblyName;
    }

    private static void UpdateComTypesInAssembly(AssemblyBuilder asmBldr, ModuleBuilder modBldr)
    {
      AssemblyBuilderData assemblyData = asmBldr.m_assemblyData;
      Type[] types = modBldr.GetTypes();
      int length = types.Length;
      for (int index = 0; index < length; ++index)
        assemblyData.AddPublicComType(types[index]);
    }

    [SecurityCritical]
    private static void SetGuidAttributeOnAssembly(AssemblyBuilder asmBldr, object typeLib)
    {
      CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(typeof (GuidAttribute).GetConstructor(new Type[1]
      {
        typeof (string)
      }), new object[1]
      {
        (object) Marshal.GetTypeLibGuid((ITypeLib) typeLib).ToString()
      });
      asmBldr.SetCustomAttribute(customBuilder);
    }

    [SecurityCritical]
    private static void SetImportedFromTypeLibAttrOnAssembly(AssemblyBuilder asmBldr, object typeLib)
    {
      CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(typeof (ImportedFromTypeLibAttribute).GetConstructor(new Type[1]
      {
        typeof (string)
      }), new object[1]
      {
        (object) Marshal.GetTypeLibName((ITypeLib) typeLib)
      });
      asmBldr.SetCustomAttribute(customBuilder);
    }

    [SecurityCritical]
    private static void SetTypeLibVersionAttribute(AssemblyBuilder asmBldr, object typeLib)
    {
      ConstructorInfo constructor = typeof (TypeLibVersionAttribute).GetConstructor(new Type[2]
      {
        typeof (int),
        typeof (int)
      });
      int major;
      int minor;
      Marshal.GetTypeLibVersion((ITypeLib) typeLib, out major, out minor);
      object[] constructorArgs = new object[2]
      {
        (object) major,
        (object) minor
      };
      CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(constructor, constructorArgs);
      asmBldr.SetCustomAttribute(customBuilder);
    }

    [SecurityCritical]
    private static void SetVersionInformation(AssemblyBuilder asmBldr, object typeLib, AssemblyName asmName)
    {
      string strName = (string) null;
      string strDocString = (string) null;
      int dwHelpContext = 0;
      string strHelpFile = (string) null;
      ((ITypeLib) typeLib).GetDocumentation(-1, out strName, out strDocString, out dwHelpContext, out strHelpFile);
      string product = string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("TypeLibConverter_ImportedTypeLibProductName"), (object) strName);
      asmBldr.DefineVersionInfoResource(product, asmName.Version.ToString(), (string) null, (string) null, (string) null);
      TypeLibConverter.SetTypeLibVersionAttribute(asmBldr, typeLib);
    }

    [SecurityCritical]
    private static void SetPIAAttributeOnAssembly(AssemblyBuilder asmBldr, object typeLib)
    {
      IntPtr ppTLibAttr = IntPtr.Zero;
      ITypeLib typeLib1 = (ITypeLib) typeLib;
      int num1 = 0;
      int num2 = 0;
      ConstructorInfo constructor = typeof (PrimaryInteropAssemblyAttribute).GetConstructor(new Type[2]
      {
        typeof (int),
        typeof (int)
      });
      try
      {
        typeLib1.GetLibAttr(out ppTLibAttr);
        System.Runtime.InteropServices.ComTypes.TYPELIBATTR structure = (System.Runtime.InteropServices.ComTypes.TYPELIBATTR) Marshal.PtrToStructure(ppTLibAttr, typeof (System.Runtime.InteropServices.ComTypes.TYPELIBATTR));
        num1 = (int) structure.wMajorVerNum;
        num2 = (int) structure.wMinorVerNum;
      }
      finally
      {
        if (ppTLibAttr != IntPtr.Zero)
          typeLib1.ReleaseTLibAttr(ppTLibAttr);
      }
      object[] constructorArgs = new object[2]
      {
        (object) num1,
        (object) num2
      };
      CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(constructor, constructorArgs);
      asmBldr.SetCustomAttribute(customBuilder);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void nConvertTypeLibToMetadata(object typeLib, RuntimeAssembly asmBldr, RuntimeModule modBldr, string nameSpace, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, out ArrayList eventItfInfoList);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object nConvertAssemblyToTypeLib(RuntimeAssembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void LoadInMemoryTypeByName(RuntimeModule module, string className);

    private class TypeResolveHandler : ITypeLibImporterNotifySink
    {
      private List<RuntimeAssembly> m_AsmList = new List<RuntimeAssembly>();
      private ModuleBuilder m_Module;
      private ITypeLibImporterNotifySink m_UserSink;

      public TypeResolveHandler(ModuleBuilder mod, ITypeLibImporterNotifySink userSink)
      {
        this.m_Module = mod;
        this.m_UserSink = userSink;
      }

      public void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg)
      {
        this.m_UserSink.ReportEvent(eventKind, eventCode, eventMsg);
      }

      public Assembly ResolveRef(object typeLib)
      {
        Assembly assembly = this.m_UserSink.ResolveRef(typeLib);
        if (assembly == (Assembly) null)
          throw new ArgumentNullException();
        RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
        if ((Assembly) runtimeAssembly == (Assembly) null)
        {
          AssemblyBuilder assemblyBuilder = assembly as AssemblyBuilder;
          if ((Assembly) assemblyBuilder != (Assembly) null)
            runtimeAssembly = (RuntimeAssembly) assemblyBuilder.InternalAssembly;
        }
        if ((Assembly) runtimeAssembly == (Assembly) null)
          throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
        this.m_AsmList.Add(runtimeAssembly);
        return (Assembly) runtimeAssembly;
      }

      [SecurityCritical]
      public Assembly ResolveEvent(object sender, ResolveEventArgs args)
      {
        try
        {
          TypeLibConverter.LoadInMemoryTypeByName(this.m_Module.GetNativeHandle(), args.Name);
          return this.m_Module.Assembly;
        }
        catch (TypeLoadException ex)
        {
          if (ex.ResourceId != -2146233054)
            throw;
        }
        foreach (RuntimeAssembly asm in this.m_AsmList)
        {
          try
          {
            asm.GetType(args.Name, true, false);
            return (Assembly) asm;
          }
          catch (TypeLoadException ex)
          {
            if (ex._HResult != -2146233054)
              throw;
          }
        }
        return (Assembly) null;
      }

      public Assembly ResolveAsmEvent(object sender, ResolveEventArgs args)
      {
        foreach (RuntimeAssembly asm in this.m_AsmList)
        {
          if (string.Compare(asm.FullName, args.Name, StringComparison.OrdinalIgnoreCase) == 0)
            return (Assembly) asm;
        }
        return (Assembly) null;
      }

      public Assembly ResolveROAsmEvent(object sender, ResolveEventArgs args)
      {
        foreach (RuntimeAssembly asm in this.m_AsmList)
        {
          if (string.Compare(asm.FullName, args.Name, StringComparison.OrdinalIgnoreCase) == 0)
            return (Assembly) asm;
        }
        return Assembly.ReflectionOnlyLoad(AppDomain.CurrentDomain.ApplyPolicy(args.Name));
      }
    }
  }
}
