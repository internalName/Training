// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ModuleBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Определяет и представляет модуль в динамической сборке.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_ModuleBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public class ModuleBuilder : Module, _ModuleBuilder
  {
    private Dictionary<string, Type> m_TypeBuilderDict;
    private ISymbolWriter m_iSymWriter;
    internal ModuleBuilderData m_moduleData;
    private MethodToken m_EntryPoint;
    internal InternalModuleBuilder m_internalModuleBuilder;
    private AssemblyBuilder m_assemblyBuilder;

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr nCreateISymWriterForDynamicModule(Module module, string filename);

    internal static string UnmangleTypeName(string typeName)
    {
      int startIndex = typeName.Length - 1;
      int num1;
      while (true)
      {
        num1 = typeName.LastIndexOf('+', startIndex);
        if (num1 != -1)
        {
          bool flag = true;
          int num2 = num1;
          while (typeName[--num2] == '\\')
            flag = !flag;
          if (!flag)
            startIndex = num2;
          else
            break;
        }
        else
          break;
      }
      return typeName.Substring(num1 + 1);
    }

    internal AssemblyBuilder ContainingAssemblyBuilder
    {
      get
      {
        return this.m_assemblyBuilder;
      }
    }

    internal ModuleBuilder(AssemblyBuilder assemblyBuilder, InternalModuleBuilder internalModuleBuilder)
    {
      this.m_internalModuleBuilder = internalModuleBuilder;
      this.m_assemblyBuilder = assemblyBuilder;
    }

    internal void AddType(string name, Type type)
    {
      this.m_TypeBuilderDict.Add(name, type);
    }

    internal void CheckTypeNameConflict(string strTypeName, Type enclosingType)
    {
      Type type = (Type) null;
      if (this.m_TypeBuilderDict.TryGetValue(strTypeName, out type) && (object) type.DeclaringType == (object) enclosingType)
        throw new ArgumentException(Environment.GetResourceString("Argument_DuplicateTypeName"));
    }

    private Type GetType(string strFormat, Type baseType)
    {
      if (strFormat == null || strFormat.Equals(string.Empty))
        return baseType;
      return SymbolType.FormCompoundType(strFormat.ToCharArray(), baseType, 0);
    }

    internal void CheckContext(params Type[][] typess)
    {
      this.ContainingAssemblyBuilder.CheckContext(typess);
    }

    internal void CheckContext(params Type[] types)
    {
      this.ContainingAssemblyBuilder.CheckContext(types);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetTypeRef(RuntimeModule module, string strFullName, RuntimeModule refedModule, string strRefedModuleFileName, int tkResolution);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetMemberRef(RuntimeModule module, RuntimeModule refedModule, int tr, int defToken);

    [SecurityCritical]
    private int GetMemberRef(Module refedModule, int tr, int defToken)
    {
      return ModuleBuilder.GetMemberRef(this.GetNativeHandle(), ModuleBuilder.GetRuntimeModuleFromModule(refedModule).GetNativeHandle(), tr, defToken);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetMemberRefFromSignature(RuntimeModule module, int tr, string methodName, byte[] signature, int length);

    [SecurityCritical]
    private int GetMemberRefFromSignature(int tr, string methodName, byte[] signature, int length)
    {
      return ModuleBuilder.GetMemberRefFromSignature(this.GetNativeHandle(), tr, methodName, signature, length);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetMemberRefOfMethodInfo(RuntimeModule module, int tr, IRuntimeMethodInfo method);

    [SecurityCritical]
    private int GetMemberRefOfMethodInfo(int tr, RuntimeMethodInfo method)
    {
      if (this.ContainingAssemblyBuilder.ProfileAPICheck && (method.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) method.FullName));
      return ModuleBuilder.GetMemberRefOfMethodInfo(this.GetNativeHandle(), tr, (IRuntimeMethodInfo) method);
    }

    [SecurityCritical]
    private int GetMemberRefOfMethodInfo(int tr, RuntimeConstructorInfo method)
    {
      if (this.ContainingAssemblyBuilder.ProfileAPICheck && (method.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) method.FullName));
      return ModuleBuilder.GetMemberRefOfMethodInfo(this.GetNativeHandle(), tr, (IRuntimeMethodInfo) method);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetMemberRefOfFieldInfo(RuntimeModule module, int tkType, RuntimeTypeHandle declaringType, int tkField);

    [SecurityCritical]
    private int GetMemberRefOfFieldInfo(int tkType, RuntimeTypeHandle declaringType, RuntimeFieldInfo runtimeField)
    {
      if (this.ContainingAssemblyBuilder.ProfileAPICheck)
      {
        RtFieldInfo rtFieldInfo = runtimeField as RtFieldInfo;
        if ((FieldInfo) rtFieldInfo != (FieldInfo) null && (rtFieldInfo.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtFieldInfo.FullName));
      }
      return ModuleBuilder.GetMemberRefOfFieldInfo(this.GetNativeHandle(), tkType, declaringType, runtimeField.MetadataToken);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetTokenFromTypeSpec(RuntimeModule pModule, byte[] signature, int length);

    [SecurityCritical]
    private int GetTokenFromTypeSpec(byte[] signature, int length)
    {
      return ModuleBuilder.GetTokenFromTypeSpec(this.GetNativeHandle(), signature, length);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetArrayMethodToken(RuntimeModule module, int tkTypeSpec, string methodName, byte[] signature, int sigLength);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetStringConstant(RuntimeModule module, string str, int length);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void PreSavePEFile(RuntimeModule module, int portableExecutableKind, int imageFileMachine);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SavePEFile(RuntimeModule module, string fileName, int entryPoint, int isExe, bool isManifestFile);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddResource(RuntimeModule module, string strName, byte[] resBytes, int resByteCount, int tkFile, int attribute, int portableExecutableKind, int imageFileMachine);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetModuleName(RuntimeModule module, string strModuleName);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetFieldRVAContent(RuntimeModule module, int fdToken, byte[] data, int length);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DefineNativeResourceFile(RuntimeModule module, string strFilename, int portableExecutableKind, int ImageFileMachine);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DefineNativeResourceBytes(RuntimeModule module, byte[] pbResource, int cbResource, int portableExecutableKind, int imageFileMachine);

    [SecurityCritical]
    internal void DefineNativeResource(PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
    {
      string resourceFileName = this.m_moduleData.m_strResourceFileName;
      byte[] resourceBytes = this.m_moduleData.m_resourceBytes;
      if (resourceFileName != null)
      {
        ModuleBuilder.DefineNativeResourceFile(this.GetNativeHandle(), resourceFileName, (int) portableExecutableKind, (int) imageFileMachine);
      }
      else
      {
        if (resourceBytes == null)
          return;
        ModuleBuilder.DefineNativeResourceBytes(this.GetNativeHandle(), resourceBytes, resourceBytes.Length, (int) portableExecutableKind, (int) imageFileMachine);
      }
    }

    internal virtual Type FindTypeBuilderWithName(string strTypeName, bool ignoreCase)
    {
      if (ignoreCase)
      {
        foreach (string key in this.m_TypeBuilderDict.Keys)
        {
          if (string.Compare(key, strTypeName, StringComparison.OrdinalIgnoreCase) == 0)
            return this.m_TypeBuilderDict[key];
        }
      }
      else
      {
        Type type;
        if (this.m_TypeBuilderDict.TryGetValue(strTypeName, out type))
          return type;
      }
      return (Type) null;
    }

    internal void SetEntryPoint(MethodToken entryPoint)
    {
      this.m_EntryPoint = entryPoint;
    }

    [SecurityCritical]
    internal void PreSave(string fileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
    {
      if (this.m_moduleData.m_isSaved)
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("InvalidOperation_ModuleHasBeenSaved"), (object) this.m_moduleData.m_strModuleName));
      if (!this.m_moduleData.m_fGlobalBeenCreated && this.m_moduleData.m_fHasGlobal)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GlobalFunctionNotBaked"));
      foreach (Type type in this.m_TypeBuilderDict.Values)
      {
        TypeBuilder typeBuilder = !(type is TypeBuilder) ? ((EnumBuilder) type).m_typeBuilder : (TypeBuilder) type;
        if (!typeBuilder.IsCreated())
          throw new NotSupportedException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("NotSupported_NotAllTypesAreBaked"), (object) typeBuilder.FullName));
      }
      ModuleBuilder.PreSavePEFile(this.GetNativeHandle(), (int) portableExecutableKind, (int) imageFileMachine);
    }

    [SecurityCritical]
    internal void Save(string fileName, bool isAssemblyFile, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
    {
      if (this.m_moduleData.m_embeddedRes != null)
      {
        for (ResWriterData resWriterData = this.m_moduleData.m_embeddedRes; resWriterData != null; resWriterData = resWriterData.m_nextResWriter)
        {
          if (resWriterData.m_resWriter != null)
            resWriterData.m_resWriter.Generate();
          byte[] numArray = new byte[resWriterData.m_memoryStream.Length];
          resWriterData.m_memoryStream.Flush();
          resWriterData.m_memoryStream.Position = 0L;
          resWriterData.m_memoryStream.Read(numArray, 0, numArray.Length);
          ModuleBuilder.AddResource(this.GetNativeHandle(), resWriterData.m_strName, numArray, numArray.Length, this.m_moduleData.FileToken, (int) resWriterData.m_attribute, (int) portableExecutableKind, (int) imageFileMachine);
        }
      }
      this.DefineNativeResource(portableExecutableKind, imageFileMachine);
      PEFileKinds peFileKinds = isAssemblyFile ? this.ContainingAssemblyBuilder.m_assemblyData.m_peFileKind : PEFileKinds.Dll;
      ModuleBuilder.SavePEFile(this.GetNativeHandle(), fileName, this.m_EntryPoint.Token, (int) peFileKinds, isAssemblyFile);
      this.m_moduleData.m_isSaved = true;
    }

    [SecurityCritical]
    private int GetTypeRefNested(Type type, Module refedModule, string strRefedModuleFileName)
    {
      Type declaringType = type.DeclaringType;
      int tkResolution = 0;
      string str = type.FullName;
      if (declaringType != (Type) null)
      {
        tkResolution = this.GetTypeRefNested(declaringType, refedModule, strRefedModuleFileName);
        str = ModuleBuilder.UnmangleTypeName(str);
      }
      if (this.ContainingAssemblyBuilder.ProfileAPICheck)
      {
        RuntimeType runtimeType = type as RuntimeType;
        if (runtimeType != (RuntimeType) null && (runtimeType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) runtimeType.FullName));
      }
      return ModuleBuilder.GetTypeRef(this.GetNativeHandle(), str, ModuleBuilder.GetRuntimeModuleFromModule(refedModule).GetNativeHandle(), strRefedModuleFileName, tkResolution);
    }

    [SecurityCritical]
    internal MethodToken InternalGetConstructorToken(ConstructorInfo con, bool usingRef)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (con));
      ConstructorBuilder constructorBuilder;
      int str;
      if ((ConstructorInfo) (constructorBuilder = con as ConstructorBuilder) != (ConstructorInfo) null)
      {
        if (!usingRef && constructorBuilder.Module.Equals((object) this))
          return constructorBuilder.GetToken();
        int token = this.GetTypeTokenInternal(con.ReflectedType).Token;
        str = this.GetMemberRef(con.ReflectedType.Module, token, constructorBuilder.GetToken().Token);
      }
      else
      {
        ConstructorOnTypeBuilderInstantiation builderInstantiation;
        if ((ConstructorInfo) (builderInstantiation = con as ConstructorOnTypeBuilderInstantiation) != (ConstructorInfo) null)
        {
          if (usingRef)
            throw new InvalidOperationException();
          int token = this.GetTypeTokenInternal(con.DeclaringType).Token;
          str = this.GetMemberRef(con.DeclaringType.Module, token, builderInstantiation.MetadataTokenInternal);
        }
        else
        {
          RuntimeConstructorInfo method;
          if ((ConstructorInfo) (method = con as RuntimeConstructorInfo) != (ConstructorInfo) null && !con.ReflectedType.IsArray)
          {
            str = this.GetMemberRefOfMethodInfo(this.GetTypeTokenInternal(con.ReflectedType).Token, method);
          }
          else
          {
            ParameterInfo[] parameters = con.GetParameters();
            if (parameters == null)
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidConstructorInfo"));
            int length1 = parameters.Length;
            Type[] parameterTypes = new Type[length1];
            Type[][] requiredParameterTypeCustomModifiers = new Type[length1][];
            Type[][] optionalParameterTypeCustomModifiers = new Type[length1][];
            for (int index = 0; index < length1; ++index)
            {
              if (parameters[index] == null)
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidConstructorInfo"));
              parameterTypes[index] = parameters[index].ParameterType;
              requiredParameterTypeCustomModifiers[index] = parameters[index].GetRequiredCustomModifiers();
              optionalParameterTypeCustomModifiers[index] = parameters[index].GetOptionalCustomModifiers();
            }
            int token = this.GetTypeTokenInternal(con.ReflectedType).Token;
            int length2;
            byte[] signature = SignatureHelper.GetMethodSigHelper((Module) this, con.CallingConvention, (Type) null, (Type[]) null, (Type[]) null, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers).InternalGetSignature(out length2);
            str = this.GetMemberRefFromSignature(token, con.Name, signature, length2);
          }
        }
      }
      return new MethodToken(str);
    }

    [SecurityCritical]
    internal void Init(string strModuleName, string strFileName, int tkFile)
    {
      this.m_moduleData = new ModuleBuilderData(this, strModuleName, strFileName, tkFile);
      this.m_TypeBuilderDict = new Dictionary<string, Type>();
    }

    [SecurityCritical]
    internal void ModifyModuleName(string name)
    {
      this.m_moduleData.ModifyModuleName(name);
      ModuleBuilder.SetModuleName(this.GetNativeHandle(), name);
    }

    internal void SetSymWriter(ISymbolWriter writer)
    {
      this.m_iSymWriter = writer;
    }

    internal object SyncRoot
    {
      get
      {
        return this.ContainingAssemblyBuilder.SyncRoot;
      }
    }

    internal InternalModuleBuilder InternalModule
    {
      get
      {
        return this.m_internalModuleBuilder;
      }
    }

    internal override ModuleHandle GetModuleHandle()
    {
      return new ModuleHandle(this.GetNativeHandle());
    }

    internal RuntimeModule GetNativeHandle()
    {
      return this.InternalModule.GetNativeHandle();
    }

    private static RuntimeModule GetRuntimeModuleFromModule(Module m)
    {
      ModuleBuilder moduleBuilder = m as ModuleBuilder;
      if ((Module) moduleBuilder != (Module) null)
        return (RuntimeModule) moduleBuilder.InternalModule;
      return m as RuntimeModule;
    }

    [SecurityCritical]
    private int GetMemberRefToken(MethodBase method, IEnumerable<Type> optionalParameterTypes)
    {
      int cGenericParameters = 0;
      if (method.IsGenericMethod)
      {
        if (!method.IsGenericMethodDefinition)
          throw new InvalidOperationException();
        cGenericParameters = method.GetGenericArguments().Length;
      }
      if (optionalParameterTypes != null && (method.CallingConvention & CallingConventions.VarArgs) == (CallingConventions) 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
      MethodInfo method1 = method as MethodInfo;
      Type[] parameterTypes;
      Type methodBaseReturnType;
      if (method.DeclaringType.IsGenericType)
      {
        MethodOnTypeBuilderInstantiation builderInstantiation1;
        MethodBase method2;
        if ((MethodInfo) (builderInstantiation1 = method as MethodOnTypeBuilderInstantiation) != (MethodInfo) null)
        {
          method2 = (MethodBase) builderInstantiation1.m_method;
        }
        else
        {
          ConstructorOnTypeBuilderInstantiation builderInstantiation2;
          if ((ConstructorInfo) (builderInstantiation2 = method as ConstructorOnTypeBuilderInstantiation) != (ConstructorInfo) null)
            method2 = (MethodBase) builderInstantiation2.m_ctor;
          else if (method is MethodBuilder || method is ConstructorBuilder)
            method2 = method;
          else if (method.IsGenericMethod)
          {
            MethodBase methodDefinition = (MethodBase) method1.GetGenericMethodDefinition();
            method2 = methodDefinition.Module.ResolveMethod(method.MetadataToken, methodDefinition.DeclaringType != (Type) null ? methodDefinition.DeclaringType.GetGenericArguments() : (Type[]) null, methodDefinition.GetGenericArguments());
          }
          else
            method2 = method.Module.ResolveMethod(method.MetadataToken, method.DeclaringType != (Type) null ? method.DeclaringType.GetGenericArguments() : (Type[]) null, (Type[]) null);
        }
        parameterTypes = method2.GetParameterTypes();
        methodBaseReturnType = MethodBuilder.GetMethodBaseReturnType(method2);
      }
      else
      {
        parameterTypes = method.GetParameterTypes();
        methodBaseReturnType = MethodBuilder.GetMethodBaseReturnType(method);
      }
      int length1;
      byte[] signature = this.GetMemberRefSignature(method.CallingConvention, methodBaseReturnType, parameterTypes, optionalParameterTypes, cGenericParameters).InternalGetSignature(out length1);
      int length2;
      return this.GetMemberRefFromSignature(!method.DeclaringType.IsGenericType ? (method.Module.Equals((object) this) ? (!(method1 != (MethodInfo) null) ? this.GetConstructorToken(method as ConstructorInfo).Token : this.GetMethodToken(method1).Token) : this.GetTypeToken(method.DeclaringType).Token) : this.GetTokenFromTypeSpec(SignatureHelper.GetTypeSigToken((Module) this, method.DeclaringType).InternalGetSignature(out length2), length2), method.Name, signature, length1);
    }

    [SecurityCritical]
    internal SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, IEnumerable<Type> optionalParameterTypes, int cGenericParameters)
    {
      int num1 = parameterTypes == null ? 0 : parameterTypes.Length;
      SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper((Module) this, call, returnType, cGenericParameters);
      for (int index = 0; index < num1; ++index)
        methodSigHelper.AddArgument(parameterTypes[index]);
      if (optionalParameterTypes != null)
      {
        int num2 = 0;
        foreach (Type optionalParameterType in optionalParameterTypes)
        {
          if (num2 == 0)
            methodSigHelper.AddSentinel();
          methodSigHelper.AddArgument(optionalParameterType);
          ++num2;
        }
      }
      return methodSigHelper;
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
      return this.InternalModule.Equals(obj);
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    public override int GetHashCode()
    {
      return this.InternalModule.GetHashCode();
    }

    /// <summary>
    ///   Возвращает пользовательские атрибуты, которые были применены к текущему <see cref="T:System.Reflection.Emit.ModuleBuilder" />.
    /// </summary>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    /// </param>
    /// <returns>
    ///   Массив, содержащий настраиваемые атрибуты; массив является пустым, если атрибуты отсутствуют.
    /// </returns>
    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.InternalModule.GetCustomAttributes(inherit);
    }

    /// <summary>
    ///   Возвращает пользовательские атрибуты, которые были применены к текущему <see cref="T:System.Reflection.Emit.ModuleBuilder" />, и которые являются производными от типа указанного атрибута.
    /// </summary>
    /// <param name="attributeType">
    ///   Базовый тип, от которого наследуют атрибуты.
    /// </param>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    /// </param>
    /// <returns>
    ///   Массив, содержащий настраиваемые атрибуты, которые являются производными, на любом уровне <paramref name="attributeType" />; массив пуст, если атрибуты отсутствуют.
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
      return this.InternalModule.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, применен ли указанный тип атрибута для данного модуля.
    /// </summary>
    /// <param name="attributeType">
    ///   Тип настраиваемого атрибута для проверки.
    /// </param>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если один или несколько экземпляров <paramref name="attributeType" /> был применен к этому модулю; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является объектом <see cref="T:System.Type" />, предоставляемым средой выполнения.
    ///    Например, <paramref name="attributeType" /> является объектом <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// </exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.InternalModule.IsDefined(attributeType, inherit);
    }

    /// <summary>
    ///   Возвращает сведения об атрибутах, примененных к текущему объекту <see cref="T:System.Reflection.Emit.ModuleBuilder" />; сведения представляют собой объекты <see cref="T:System.Reflection.CustomAttributeData" />.
    /// </summary>
    /// <returns>
    ///   Универсальный список объектов <see cref="T:System.Reflection.CustomAttributeData" />, представляющих данные об атрибутах, которые были применены к текущему модулю.
    /// </returns>
    public override IList<CustomAttributeData> GetCustomAttributesData()
    {
      return this.InternalModule.GetCustomAttributesData();
    }

    /// <summary>
    ///   Возвращает все классы, определенные в данном модуле.
    /// </summary>
    /// <returns>
    ///   Массив, содержащий типы, определенные в модуле, отраженный этим экземпляром.
    /// </returns>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">
    ///   Один или несколько классов модуля не может быть загружен.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public override Type[] GetTypes()
    {
      lock (this.SyncRoot)
        return this.GetTypesNoLock();
    }

    internal Type[] GetTypesNoLock()
    {
      int count = this.m_TypeBuilderDict.Count;
      Type[] typeArray = new Type[this.m_TypeBuilderDict.Count];
      int num = 0;
      foreach (Type type in this.m_TypeBuilderDict.Values)
      {
        EnumBuilder enumBuilder = type as EnumBuilder;
        TypeBuilder typeBuilder = !((Type) enumBuilder != (Type) null) ? (TypeBuilder) type : enumBuilder.m_typeBuilder;
        typeArray[num++] = !typeBuilder.IsCreated() ? type : typeBuilder.UnderlyingSystemType;
      }
      return typeArray;
    }

    /// <summary>Получает именованный тип, определенный в модуле.</summary>
    /// <param name="className">
    ///   Имя <see cref="T:System.Type" /> для получения.
    /// </param>
    /// <returns>
    ///   Запрошенный тип, если тип определен в этом модуле; в противном случае — <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина <paramref name="className" /> равно нулю или больше, чем 1023.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="className" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Запрошенный <see cref="T:System.Type" /> не является открытым и вызывающий объект не имеет <see cref="T:System.Security.Permissions.ReflectionPermission" /> для отражения закрытых объектов за пределами текущей сборки.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Инициализатор класса вызывается и создает исключение.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Ошибка при загрузке <see cref="T:System.Type" />.
    /// </exception>
    [ComVisible(true)]
    public override Type GetType(string className)
    {
      return this.GetType(className, false, false);
    }

    /// <summary>
    ///   Получает именованный тип, определенный в модуле, при необходимости игнорировать регистр имени типа.
    /// </summary>
    /// <param name="className">
    ///   Имя <see cref="T:System.Type" /> для получения.
    /// </param>
    /// <param name="ignoreCase">
    ///   Если значение <see langword="true" />, при поиске не учитывается регистр.
    ///    Если значение <see langword="false" />, при поиске учитывается регистр.
    /// </param>
    /// <returns>
    ///   Запрошенный тип, если тип определен в этом модуле; в противном случае — <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина <paramref name="className" /> равно нулю или больше, чем 1023.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="className" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Запрошенный <see cref="T:System.Type" /> не является открытым и вызывающий объект не имеет <see cref="T:System.Security.Permissions.ReflectionPermission" /> для отражения закрытых объектов за пределами текущей сборки.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Инициализатор класса вызывается и создает исключение.
    /// </exception>
    [ComVisible(true)]
    public override Type GetType(string className, bool ignoreCase)
    {
      return this.GetType(className, false, ignoreCase);
    }

    /// <summary>
    ///   Получает именованный тип, определенный в модуле, при необходимости игнорировать регистр имени типа.
    ///    При необходимости вызывает исключение, если тип не найден.
    /// </summary>
    /// <param name="className">
    ///   Имя <see cref="T:System.Type" /> для получения.
    /// </param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" />, чтобы создать исключение, если тип не удается найти; значение <see langword="false" />, чтобы вернуть значение <see langword="null" />.
    /// </param>
    /// <param name="ignoreCase">
    ///   Если значение <see langword="true" />, при поиске не учитывается регистр.
    ///    Если значение <see langword="false" />, при поиске учитывается регистр.
    /// </param>
    /// <returns>
    ///   Указанного типа, если тип, объявленный в этом модуле; в противном случае — <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина <paramref name="className" /> равно нулю или больше, чем 1023.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="className" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Запрошенный <see cref="T:System.Type" /> не является открытым и вызывающий объект не имеет <see cref="T:System.Security.Permissions.ReflectionPermission" /> для отражения закрытых объектов за пределами текущей сборки.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Инициализатор класса вызывается и создает исключение.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="throwOnError" /> — <see langword="true" /> и указанный тип не найден.
    /// </exception>
    [ComVisible(true)]
    public override Type GetType(string className, bool throwOnError, bool ignoreCase)
    {
      lock (this.SyncRoot)
        return this.GetTypeNoLock(className, throwOnError, ignoreCase);
    }

    private Type GetTypeNoLock(string className, bool throwOnError, bool ignoreCase)
    {
      Type baseType = this.InternalModule.GetType(className, throwOnError, ignoreCase);
      if (baseType != (Type) null)
        return baseType;
      string str1 = (string) null;
      string strFormat = (string) null;
      int num1;
      for (int startIndex = 0; startIndex <= className.Length; startIndex = num1 + 1)
      {
        num1 = className.IndexOfAny(new char[3]
        {
          '[',
          '*',
          '&'
        }, startIndex);
        if (num1 == -1)
        {
          str1 = className;
          strFormat = (string) null;
          break;
        }
        int num2 = 0;
        for (int index = num1 - 1; index >= 0 && className[index] == '\\'; --index)
          ++num2;
        if (num2 % 2 != 1)
        {
          str1 = className.Substring(0, num1);
          strFormat = className.Substring(num1);
          break;
        }
      }
      if (str1 == null)
      {
        str1 = className;
        strFormat = (string) null;
      }
      string str2 = str1.Replace("\\\\", "\\").Replace("\\[", "[").Replace("\\*", "*").Replace("\\&", "&");
      if (strFormat != null)
        baseType = this.InternalModule.GetType(str2, false, ignoreCase);
      if (baseType == (Type) null)
      {
        baseType = this.FindTypeBuilderWithName(str2, ignoreCase);
        if (baseType == (Type) null && this.Assembly is AssemblyBuilder)
        {
          List<ModuleBuilder> moduleBuilderList = this.ContainingAssemblyBuilder.m_assemblyData.m_moduleBuilderList;
          int count = moduleBuilderList.Count;
          for (int index = 0; index < count && baseType == (Type) null; ++index)
            baseType = moduleBuilderList[index].FindTypeBuilderWithName(str2, ignoreCase);
        }
        if (baseType == (Type) null)
          return (Type) null;
      }
      if (strFormat == null)
        return baseType;
      return this.GetType(strFormat, baseType);
    }

    /// <summary>
    ///   Возвращает <see langword="String" /> предоставляющее полное имя и путь для данного модуля.
    /// </summary>
    /// <returns>Полное имя модуля.</returns>
    public override string FullyQualifiedName
    {
      [SecuritySafeCritical] get
      {
        string str = this.m_moduleData.m_strFileName;
        if (str == null)
          return (string) null;
        if (this.ContainingAssemblyBuilder.m_assemblyData.m_strDir != null)
          str = Path.UnsafeGetFullPath(Path.Combine(this.ContainingAssemblyBuilder.m_assemblyData.m_strDir, str));
        if (this.ContainingAssemblyBuilder.m_assemblyData.m_strDir != null && str != null)
          new FileIOPermission(FileIOPermissionAccess.PathDiscovery, str).Demand();
        return str;
      }
    }

    /// <summary>
    ///   Возвращает большой двоичный объект подписи, определенный токеном метаданных.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий подпись в модуле.
    /// </param>
    /// <returns>
    ///   Массив байтов, представляющий большой двоичный объект подписи.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является допустимым <see langword="MemberRef" />, <see langword="MethodDef" />, <see langword="TypeSpec" />, подписи или <see langword="FieldDef" /> маркеров в области текущего модуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public override byte[] ResolveSignature(int metadataToken)
    {
      return this.InternalModule.ResolveSignature(metadataToken);
    }

    /// <summary>
    ///   Возвращает метод или конструктор, определенный заданным маркером метаданных, в контексте, определенном заданными параметрами универсального типа.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий метод или конструктор в модуле.
    /// </param>
    /// <param name="genericTypeArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для типа, если маркер находится в области видимости, или <see langword="null" /> Если этот тип не является универсальным.
    /// </param>
    /// <param name="genericMethodArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для метода, если маркер находится в области видимости, или <see langword="null" /> Если этот метод не является универсальным.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodBase" /> объект, представляющий метод, который определяется заданным токеном метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для метода или конструктора в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="MethodSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> (параметр типа универсального типа) или <see langword="mvar" /> (параметр типа универсального метода), а аргументы необходимые универсального типа не были указаны для одной или обеих <paramref name="genericTypeArguments" /> и <paramref name="genericMethodArguments" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return this.InternalModule.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
    }

    /// <summary>
    ///   Возвращает поля, определенного указанным токеном метаданных, в контексте, определенном заданными параметрами универсального типа.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий поле в модуле.
    /// </param>
    /// <param name="genericTypeArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для типа, если маркер находится в области видимости, или <see langword="null" /> Если этот тип не является универсальным.
    /// </param>
    /// <param name="genericMethodArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для метода, если маркер находится в области видимости, или <see langword="null" /> Если этот метод не является универсальным.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.FieldInfo" /> объект, предоставляющий поле, которое определяется заданным токеном метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для поля в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> Определяет поле, родительская <see langword="TypeSpec" /> имеет подпись, содержащую тип элемента <see langword="var" /> (параметр типа универсального типа) или <see langword="mvar" /> (параметр типа универсального метода), а аргументы необходимые универсального типа не были указаны для одной или обеих <paramref name="genericTypeArguments" /> и <paramref name="genericMethodArguments" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return this.InternalModule.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
    }

    /// <summary>
    ///   Возвращает тип, определенный указанным токеном метаданных, в контексте, определенном заданными параметрами универсального типа.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий тип в модуле.
    /// </param>
    /// <param name="genericTypeArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для типа, если маркер находится в области видимости, или <see langword="null" /> Если этот тип не является универсальным.
    /// </param>
    /// <param name="genericMethodArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для метода, если маркер находится в области видимости, или <see langword="null" /> Если этот метод не является универсальным.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий тип, который определяется заданным токеном метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для типа в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="TypeSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> (параметр типа универсального типа) или <see langword="mvar" /> (параметр типа универсального метода), а аргументы необходимые универсального типа не были указаны для одной или обеих <paramref name="genericTypeArguments" /> и <paramref name="genericMethodArguments" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return this.InternalModule.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
    }

    /// <summary>
    ///   Возвращает тип или член, определяемый указанным токеном метаданных, в контексте, определенном заданными параметрами универсального типа.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий тип или член в модуле.
    /// </param>
    /// <param name="genericTypeArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для типа, если маркер находится в области видимости, или <see langword="null" /> Если этот тип не является универсальным.
    /// </param>
    /// <param name="genericMethodArguments">
    ///   Массив <see cref="T:System.Type" /> объектов, представляющих аргументы универсального типа для метода, если маркер находится в области видимости, или <see langword="null" /> Если этот метод не является универсальным.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MemberInfo" /> объект, представляющий тип или член, который определяется заданным токеном метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для типа или члена в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="MethodSpec" /> или <see langword="TypeSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> (параметр типа универсального типа) или <see langword="mvar" /> (параметр типа универсального метода), а аргументы необходимые универсального типа не были указаны для одной или обеих <paramref name="genericTypeArguments" /> и <paramref name="genericMethodArguments" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> идентифицирует свойство или событие.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return this.InternalModule.ResolveMember(metadataToken, genericTypeArguments, genericMethodArguments);
    }

    /// <summary>
    ///   Возвращает строку, определенную заданным токеном метаданных.
    /// </summary>
    /// <param name="metadataToken">
    ///   Токен метаданных, определяющий строку в куче строк модуля.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.String" /> содержащий строковое значение из кучи строк метаданных.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="metadataToken" /> не является маркером для строки в области текущего модуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// </exception>
    public override string ResolveString(int metadataToken)
    {
      return this.InternalModule.ResolveString(metadataToken);
    }

    /// <summary>
    ///   Возвращает пару значений, определяющих природу кода в модуле и платформе, модуль.
    /// </summary>
    /// <param name="peKind">
    ///   При возвращении данного метода сочетание <see cref="T:System.Reflection.PortableExecutableKinds" /> значений, определяющих природу кода в модуле.
    /// </param>
    /// <param name="machine">
    ///   Если этот метод возвращает, один из <see cref="T:System.Reflection.ImageFileMachine" /> значения, указывающие платформы в модуле.
    /// </param>
    public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
    {
      this.InternalModule.GetPEKind(out peKind, out machine);
    }

    /// <summary>Возвращает версию потока метаданных.</summary>
    /// <returns>
    ///   32-разрядное целое число, представляющее версию потока метаданных.
    ///    Старшие байты два представляют основной номер версии, а два байта низкого порядка дополнительный номер версии.
    /// </returns>
    public override int MDStreamVersion
    {
      get
      {
        return this.InternalModule.MDStreamVersion;
      }
    }

    /// <summary>
    ///   Возвращает универсальный уникальный идентификатор (UUID), который можно использовать для различения двух версий модуля.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Guid" /> можно использовать для различения двух версий модуля.
    /// </returns>
    public override Guid ModuleVersionId
    {
      get
      {
        return this.InternalModule.ModuleVersionId;
      }
    }

    /// <summary>
    ///   Возвращает токен, который определяет текущий динамический модуль в метаданных.
    /// </summary>
    /// <returns>
    ///   Целочисленный токен, который идентифицирует текущий модуль в метаданных.
    /// </returns>
    public override int MetadataToken
    {
      get
      {
        return this.InternalModule.MetadataToken;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли объект ресурсом.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если объект является ресурсом; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool IsResource()
    {
      return this.InternalModule.IsResource();
    }

    /// <summary>
    ///   Возвращает все поля, определенные в области .sdata переносимого исполняемого (PE) файла, которые соответствуют заданным флагам привязки.
    /// </summary>
    /// <param name="bindingFlags">
    ///   Сочетание <see langword="BindingFlags" /> битовые флаги, используемые для управления поиском.
    /// </param>
    /// <returns>
    ///   Массив полей, соответствующих заданным флагам; Если таких полей нет, массив пуст.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public override FieldInfo[] GetFields(BindingFlags bindingFlags)
    {
      return this.InternalModule.GetFields(bindingFlags);
    }

    /// <summary>
    ///   Возвращает поле уровня модуля, определенное в области .sdata переносимого исполняемого (PE) файла, с указанным именем и атрибутами привязки.
    /// </summary>
    /// <param name="name">Имя поля.</param>
    /// <param name="bindingAttr">
    ///   Сочетание <see langword="BindingFlags" /> битовые флаги, используемые для управления поиском.
    /// </param>
    /// <returns>
    ///   Поле с указанным именем и атрибутами привязки, или <see langword="null" /> Если поле не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      return this.InternalModule.GetField(name, bindingAttr);
    }

    /// <summary>
    ///   Возвращает все методы, определенные на уровне модуля в текущем <see cref="T:System.Reflection.Emit.ModuleBuilder" />, и которые соответствуют заданным флагам привязки.
    /// </summary>
    /// <param name="bindingFlags">
    ///   Сочетание <see langword="BindingFlags" /> битовые флаги, используемые для управления поиском.
    /// </param>
    /// <returns>
    ///   Массив, содержащий все методы уровня модуля, соответствующие <paramref name="bindingFlags" />.
    /// </returns>
    public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
    {
      return this.InternalModule.GetMethods(bindingFlags);
    }

    /// <summary>
    ///   Возвращает метод уровня модуля, который соответствует указанным критериям.
    /// </summary>
    /// <param name="name">Имя метода.</param>
    /// <param name="bindingAttr">
    ///   Сочетание <see langword="BindingFlags" /> битовые флаги, используемые для управления поиском.
    /// </param>
    /// <param name="binder">
    ///   Объект, реализующий <see langword="Binder" />, содержащий свойства, связанные с этим методом.
    /// </param>
    /// <param name="callConvention">
    ///   Соглашение о вызовах для метода.
    /// </param>
    /// <param name="types">Типы параметров метода.</param>
    /// <param name="modifiers">
    ///   Массив модификаторов параметров, используемый для работы привязки с подписями параметров, в которых были изменены типы.
    /// </param>
    /// <returns>
    ///   Метод, который определен на уровне модуля и соответствующий заданным критериям; или <see langword="null" /> Если такого метода не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="name" /> — <see langword="null" />, <paramref name="types" /> — <see langword="null" />, или элемент <paramref name="types" /> — <see langword="null" />.
    /// </exception>
    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      return this.InternalModule.GetMethodInternal(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>
    ///   Возвращает строку, представляющую имя динамического модуля.
    /// </summary>
    /// <returns>Имя динамического модуля.</returns>
    public override string ScopeName
    {
      get
      {
        return this.InternalModule.ScopeName;
      }
    }

    /// <summary>
    ///   Строка, указывающая, что это модуль, расположенный в памяти.
    /// </summary>
    /// <returns>
    ///   Текст, который указывает, что это модуль, расположенный в памяти.
    /// </returns>
    public override string Name
    {
      get
      {
        return this.InternalModule.Name;
      }
    }

    /// <summary>
    ///   Возвращает динамическую сборку, который определен этот экземпляр <see cref="T:System.Reflection.Emit.ModuleBuilder" />.
    /// </summary>
    /// <returns>
    ///   Динамическая сборка, который определен текущий динамический модуль.
    /// </returns>
    public override Assembly Assembly
    {
      get
      {
        return (Assembly) this.m_assemblyBuilder;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> объект, соответствующий сертификату, включаемому в подпись Authenticode сборки, которой принадлежит этот модуль.
    ///    Если сборка не была, с подписью Authenticode <see langword="null" /> возвращается.
    /// </summary>
    /// <returns>
    ///   Сертификат, или <see langword="null" /> сборки, к которой относится этот модуль не подпись Authenticode.
    /// </returns>
    public override X509Certificate GetSignerCertificate()
    {
      return this.InternalModule.GetSignerCertificate();
    }

    /// <summary>
    ///   Создает <see langword="TypeBuilder" /> для закрытого типа с указанным именем в этом модуле.
    /// </summary>
    /// <param name="name">
    ///   Полный путь типа, включая пространство имен.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <returns>Закрытый тип с указанным именем.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип с заданным именем существует в родительской сборке этого модуля.
    /// 
    ///   -или-
    /// 
    ///   Атрибуты вложенного типа установлены для типа, который не является вложенным.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineType(string name)
    {
      lock (this.SyncRoot)
        return this.DefineTypeNoLock(name, TypeAttributes.NotPublic, (Type) null, (Type[]) null, PackingSize.Unspecified, 0);
    }

    /// <summary>
    ///   Создает <see langword="TypeBuilder" /> заданного имени типа и атрибуты типа.
    /// </summary>
    /// <param name="name">
    ///   Полный путь типа.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attr">Атрибуты определенного типа.</param>
    /// <returns>
    ///   A <see langword="TypeBuilder" /> создан с учетом всех запрошенных атрибутов.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип с заданным именем существует в родительской сборке этого модуля.
    /// 
    ///   -или-
    /// 
    ///   Атрибуты вложенного типа установлены для типа, который не является вложенным.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineType(string name, TypeAttributes attr)
    {
      lock (this.SyncRoot)
        return this.DefineTypeNoLock(name, attr, (Type) null, (Type[]) null, PackingSize.Unspecified, 0);
    }

    /// <summary>
    ///   Создает <see langword="TypeBuilder" /> заданным именем, атрибутами и типом, который расширяет определенный тип.
    /// </summary>
    /// <param name="name">
    ///   Полный путь типа.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attr">Атрибут, который будет связан с типом.</param>
    /// <param name="parent">
    ///   Тип, который расширяет определенный тип.
    /// </param>
    /// <returns>
    ///   A <see langword="TypeBuilder" /> создан с учетом всех запрошенных атрибутов.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип с заданным именем существует в родительской сборке этого модуля.
    /// 
    ///   -или-
    /// 
    ///   Атрибуты вложенного типа установлены для типа, который не является вложенным.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent)
    {
      lock (this.SyncRoot)
      {
        this.CheckContext(new Type[1]{ parent });
        return this.DefineTypeNoLock(name, attr, parent, (Type[]) null, PackingSize.Unspecified, 0);
      }
    }

    /// <summary>
    ///   Создает <see langword="TypeBuilder" /> заданного имени типа, атрибуты, тип, который расширяет определенный тип и общий размер типа.
    /// </summary>
    /// <param name="name">
    ///   Полный путь типа.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attr">Атрибуты определенного типа.</param>
    /// <param name="parent">
    ///   Тип, который расширяет определенный тип.
    /// </param>
    /// <param name="typesize">Общий размер типа.</param>
    /// <returns>
    ///   Объект <see langword="TypeBuilder" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип с заданным именем существует в родительской сборке этого модуля.
    /// 
    ///   -или-
    /// 
    ///   Атрибуты вложенного типа установлены для типа, который не является вложенным.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, int typesize)
    {
      lock (this.SyncRoot)
        return this.DefineTypeNoLock(name, attr, parent, (Type[]) null, PackingSize.Unspecified, typesize);
    }

    /// <summary>
    ///   Создает <see langword="TypeBuilder" /> заданного имени типа, атрибуты, тип, который расширяет определенный тип, упаковочный размер определенного типа и общий размер определенного типа.
    /// </summary>
    /// <param name="name">
    ///   Полный путь типа.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attr">Атрибуты определенного типа.</param>
    /// <param name="parent">
    ///   Тип, который расширяет определенный тип.
    /// </param>
    /// <param name="packingSize">Размер упаковки типа.</param>
    /// <param name="typesize">Общий размер типа.</param>
    /// <returns>
    ///   A <see langword="TypeBuilder" /> создан с учетом всех запрошенных атрибутов.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип с заданным именем существует в родительской сборке этого модуля.
    /// 
    ///   -или-
    /// 
    ///   Атрибуты вложенного типа установлены для типа, который не является вложенным.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, PackingSize packingSize, int typesize)
    {
      lock (this.SyncRoot)
        return this.DefineTypeNoLock(name, attr, parent, (Type[]) null, packingSize, typesize);
    }

    /// <summary>
    ///   Создает <see langword="TypeBuilder" /> заданного имени типа, атрибуты, тип, который расширяет определенный тип и интерфейсы, которые реализует определенный тип.
    /// </summary>
    /// <param name="name">
    ///   Полный путь типа.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attr">
    ///   Атрибуты, которые будут связаны с типом.
    /// </param>
    /// <param name="parent">
    ///   Тип, который расширяет определенный тип.
    /// </param>
    /// <param name="interfaces">
    ///   Список интерфейсов, реализуемых типом.
    /// </param>
    /// <returns>
    ///   A <see langword="TypeBuilder" /> создан с учетом всех запрошенных атрибутов.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип с заданным именем существует в родительской сборке этого модуля.
    /// 
    ///   -или-
    /// 
    ///   Атрибуты вложенного типа установлены для типа, который не является вложенным.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
    {
      lock (this.SyncRoot)
        return this.DefineTypeNoLock(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
    }

    [SecurityCritical]
    private TypeBuilder DefineTypeNoLock(string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packingSize, int typesize)
    {
      return new TypeBuilder(name, attr, parent, interfaces, this, packingSize, typesize, (TypeBuilder) null);
    }

    /// <summary>
    ///   Создает <see langword="TypeBuilder" /> заданного имени типа, атрибуты, тип, который расширяет определенный тип и упаковочный размер типа.
    /// </summary>
    /// <param name="name">
    ///   Полный путь типа.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attr">Атрибуты определенного типа.</param>
    /// <param name="parent">
    ///   Тип, который расширяет определенный тип.
    /// </param>
    /// <param name="packsize">Размер упаковки типа.</param>
    /// <returns>
    ///   Объект <see langword="TypeBuilder" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип с заданным именем существует в родительской сборке этого модуля.
    /// 
    ///   -или-
    /// 
    ///   Атрибуты вложенного типа установлены для типа, который не является вложенным.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, PackingSize packsize)
    {
      lock (this.SyncRoot)
        return this.DefineTypeNoLock(name, attr, parent, packsize);
    }

    [SecurityCritical]
    private TypeBuilder DefineTypeNoLock(string name, TypeAttributes attr, Type parent, PackingSize packsize)
    {
      return new TypeBuilder(name, attr, parent, (Type[]) null, this, packsize, 0, (TypeBuilder) null);
    }

    /// <summary>
    ///   Определяет тип перечисления, который является типом значения с одним полем нестатических называется <paramref name="value__" /> указанного типа.
    /// </summary>
    /// <param name="name">
    ///   Полный путь типа перечисления.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="visibility">
    ///   Атрибуты типа перечисления.
    ///    Атрибуты, битов, определенных <see cref="F:System.Reflection.TypeAttributes.VisibilityMask" />.
    /// </param>
    /// <param name="underlyingType">
    ///   Базовый тип для перечисления.
    ///    Это должен быть встроенный целочисленный тип.
    /// </param>
    /// <returns>Определенное перечисление.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Приведены атрибуты, не являющиеся атрибутами видимости.
    /// 
    ///   -или-
    /// 
    ///   Перечисление с указанным именем существует в родительской сборке этого модуля.
    /// 
    ///   -или-
    /// 
    ///   Атрибуты видимости не соответствуют области действия перечисления.
    ///    Например <see cref="F:System.Reflection.TypeAttributes.NestedPublic" /> указан для <paramref name="visibility" />, но перечисления не является вложенным типом.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public EnumBuilder DefineEnum(string name, TypeAttributes visibility, Type underlyingType)
    {
      this.CheckContext(new Type[1]{ underlyingType });
      lock (this.SyncRoot)
      {
        EnumBuilder enumBuilder = this.DefineEnumNoLock(name, visibility, underlyingType);
        this.m_TypeBuilderDict[name] = (Type) enumBuilder;
        return enumBuilder;
      }
    }

    [SecurityCritical]
    private EnumBuilder DefineEnumNoLock(string name, TypeAttributes visibility, Type underlyingType)
    {
      return new EnumBuilder(name, underlyingType, visibility, this);
    }

    /// <summary>
    ///   Определяет именованный управляемый внедренный ресурс для хранения в этом модуле.
    /// </summary>
    /// <param name="name">
    ///   Имя ресурса.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="description">Описание ресурса.</param>
    /// <returns>Интерфейс записи ресурса для определенного ресурса.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот модуль является временным.
    /// 
    ///   -или-
    /// 
    ///   Соответствующая сборка не является сохраненной.
    /// </exception>
    public IResourceWriter DefineResource(string name, string description)
    {
      return this.DefineResource(name, description, ResourceAttributes.Public);
    }

    /// <summary>
    ///   Определяет именованный управляемый внедренный ресурс с заданными атрибутами, должен храниться в этом модуле.
    /// </summary>
    /// <param name="name">
    ///   Имя ресурса.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="description">Описание ресурса.</param>
    /// <param name="attribute">Атрибуты ресурса.</param>
    /// <returns>Интерфейс записи ресурса для определенного ресурса.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение NULL.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот модуль является временным.
    /// 
    ///   -или-
    /// 
    ///   Соответствующая сборка не является сохраненной.
    /// </exception>
    public IResourceWriter DefineResource(string name, string description, ResourceAttributes attribute)
    {
      lock (this.SyncRoot)
        return this.DefineResourceNoLock(name, description, attribute);
    }

    private IResourceWriter DefineResourceNoLock(string name, string description, ResourceAttributes attribute)
    {
      if (this.IsTransient())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadResourceContainer"));
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      if (!this.m_assemblyBuilder.IsPersistable())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadResourceContainer"));
      this.m_assemblyBuilder.m_assemblyData.CheckResNameConflict(name);
      MemoryStream memoryStream = new MemoryStream();
      ResourceWriter resWriter = new ResourceWriter((Stream) memoryStream);
      this.m_moduleData.m_embeddedRes = new ResWriterData(resWriter, (Stream) memoryStream, name, string.Empty, string.Empty, attribute)
      {
        m_nextResWriter = this.m_moduleData.m_embeddedRes
      };
      return (IResourceWriter) resWriter;
    }

    /// <summary>
    ///   Определяет двоичных больших объектов (BLOB), представляющий ресурс манифеста должен быть внедрен в динамическую сборку.
    /// </summary>
    /// <param name="name">Имя ресурса с учетом регистра.</param>
    /// <param name="stream">Поток, содержащий байты для ресурса.</param>
    /// <param name="attribute">
    ///   Значение перечисления, указывающее, является ли ресурс открытым или закрытым.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="stream" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> представляет собой строку нулевой длины.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Динамические сборки, содержащей текущий модуль является несохраняемым; то есть, имя файла не был указан при <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineDynamicModule(System.String,System.String)" /> был вызван.
    /// </exception>
    public void DefineManifestResource(string name, Stream stream, ResourceAttributes attribute)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (stream == null)
        throw new ArgumentNullException(nameof (stream));
      lock (this.SyncRoot)
        this.DefineManifestResourceNoLock(name, stream, attribute);
    }

    private void DefineManifestResourceNoLock(string name, Stream stream, ResourceAttributes attribute)
    {
      if (this.IsTransient())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadResourceContainer"));
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      if (!this.m_assemblyBuilder.IsPersistable())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadResourceContainer"));
      this.m_assemblyBuilder.m_assemblyData.CheckResNameConflict(name);
      this.m_moduleData.m_embeddedRes = new ResWriterData((ResourceWriter) null, stream, name, string.Empty, string.Empty, attribute)
      {
        m_nextResWriter = this.m_moduleData.m_embeddedRes
      };
    }

    /// <summary>
    ///   Определяет неуправляемый внедренный ресурс закрытый большой двоичный объект (BLOB) байтов.
    /// </summary>
    /// <param name="resource">
    ///   Закрытый большой двоичный объект, представляющий неуправляемый ресурс
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Неуправляемый ресурс уже определен в сборке модуля.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="resource" /> имеет значение <see langword="null" />.
    /// </exception>
    public void DefineUnmanagedResource(byte[] resource)
    {
      lock (this.SyncRoot)
        this.DefineUnmanagedResourceInternalNoLock(resource);
    }

    internal void DefineUnmanagedResourceInternalNoLock(byte[] resource)
    {
      if (resource == null)
        throw new ArgumentNullException(nameof (resource));
      if (this.m_moduleData.m_strResourceFileName != null || this.m_moduleData.m_resourceBytes != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
      this.m_moduleData.m_resourceBytes = new byte[resource.Length];
      Array.Copy((Array) resource, (Array) this.m_moduleData.m_resourceBytes, resource.Length);
    }

    /// <summary>
    ///   Определяет неуправляемый ресурс заданного имени файла ресурсов Win32.
    /// </summary>
    /// <param name="resourceFileName">
    ///   Имя файла неуправляемых ресурсов.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Неуправляемый ресурс уже определен в сборке модуля.
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
    [SecuritySafeCritical]
    public void DefineUnmanagedResource(string resourceFileName)
    {
      lock (this.SyncRoot)
        this.DefineUnmanagedResourceFileInternalNoLock(resourceFileName);
    }

    [SecurityCritical]
    internal void DefineUnmanagedResourceFileInternalNoLock(string resourceFileName)
    {
      if (resourceFileName == null)
        throw new ArgumentNullException(nameof (resourceFileName));
      if (this.m_moduleData.m_resourceBytes != null || this.m_moduleData.m_strResourceFileName != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
      string fullPath = Path.UnsafeGetFullPath(resourceFileName);
      new FileIOPermission(FileIOPermissionAccess.Read, fullPath).Demand();
      new EnvironmentPermission(PermissionState.Unrestricted).Assert();
      try
      {
        if (!File.UnsafeExists(resourceFileName))
          throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound_FileName", (object) resourceFileName), resourceFileName);
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
      this.m_moduleData.m_strResourceFileName = fullPath;
    }

    /// <summary>
    ///   Определяет глобальный метод с указанным именем, атрибуты, тип возвращаемого значения и типы параметров.
    /// </summary>
    /// <param name="name">
    ///   Имя метода.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attributes">
    ///   Атрибуты метода.
    ///   <paramref name="attributes" /> необходимо включить <see cref="F:System.Reflection.MethodAttributes.Static" />.
    /// </param>
    /// <param name="returnType">Тип возвращаемых данных метода.</param>
    /// <param name="parameterTypes">Типы параметров метода.</param>
    /// <returns>Глобальные определенный метод.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Метод не является статическим.
    ///    То есть <paramref name="attributes" /> не включает <see cref="F:System.Reflection.MethodAttributes.Static" />.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="name" /> равно нулю
    /// 
    ///   -или-
    /// 
    ///   Элемент в <see cref="T:System.Type" /> массив <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> был вызван ранее.
    /// </exception>
    public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
    {
      return this.DefineGlobalMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
    }

    /// <summary>
    ///   Определяет глобальный метод с указанным именем, атрибуты, соглашение о вызовах, возвращаемый тип и типы параметров.
    /// </summary>
    /// <param name="name">
    ///   Имя метода.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attributes">
    ///   Атрибуты метода.
    ///   <paramref name="attributes" /> необходимо включить <see cref="F:System.Reflection.MethodAttributes.Static" />.
    /// </param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах для метода.
    /// </param>
    /// <param name="returnType">Тип возвращаемых данных метода.</param>
    /// <param name="parameterTypes">Типы параметров метода.</param>
    /// <returns>Глобальные определенный метод.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Метод не является статическим.
    ///    То есть <paramref name="attributes" /> не включает <see cref="F:System.Reflection.MethodAttributes.Static" />.
    /// 
    ///   -или-
    /// 
    ///   Элемент в <see cref="T:System.Type" /> массив <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> был вызван ранее.
    /// </exception>
    public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      return this.DefineGlobalMethod(name, attributes, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>
    ///   Определяет глобальный метод с указанным именем, атрибуты, соглашение о вызовах, возвращаемый тип пользовательские модификаторы для возвращаемого типа, типы параметров и пользовательские модификаторы для типов параметров.
    /// </summary>
    /// <param name="name">
    ///   Имя метода.
    ///   <paramref name="name" /> не может содержать внедренные символы null.
    /// </param>
    /// <param name="attributes">
    ///   Атрибуты метода.
    ///   <paramref name="attributes" /> необходимо включить <see cref="F:System.Reflection.MethodAttributes.Static" />.
    /// </param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах для метода.
    /// </param>
    /// <param name="returnType">Тип возвращаемых данных метода.</param>
    /// <param name="requiredReturnTypeCustomModifiers">
    ///   Массив типов, представляющих обязательные настраиваемые модификаторы для возвращаемого типа, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsBoxed" />.
    ///    Если возвращаемый тип не содержит требуемых настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="optionalReturnTypeCustomModifiers">
    ///   Массив типов, представляющих необязательные настраиваемые модификаторы для возвращаемого типа, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsBoxed" />.
    ///    Если возвращаемый тип не содержит необязательных настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="parameterTypes">Типы параметров метода.</param>
    /// <param name="requiredParameterTypeCustomModifiers">
    ///   Массив массивов типов.
    ///    Каждый массив типов представляет обязательные настраиваемые модификаторы для соответствующего параметра глобального метода.
    ///    Если определенный аргумент не содержит требуемые пользовательские модификаторы, укажите <see langword="null" /> вместо массива типов.
    ///    Если глобальный метод не содержит аргументов или если аргументы не содержат пользовательские модификаторы, укажите <see langword="null" /> вместо массива массивов.
    /// </param>
    /// <param name="optionalParameterTypeCustomModifiers">
    ///   Массив массивов типов.
    ///    Каждый массив типов представляет собой необязательные пользовательские модификаторы для соответствующего параметра.
    ///    Если определенный аргумент не содержит необязательные пользовательские модификаторы, укажите <see langword="null" /> вместо массива типов.
    ///    Если глобальный метод не содержит аргументов или если аргументы не содержат необязательные пользовательские модификаторы, укажите <see langword="null" /> вместо массива массивов.
    /// </param>
    /// <returns>Глобальные определенный метод.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Метод не является статическим.
    ///    То есть <paramref name="attributes" /> не включает <see cref="F:System.Reflection.MethodAttributes.Static" />.
    /// 
    ///   -или-
    /// 
    ///   Элемент в <see cref="T:System.Type" /> массив <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> Метод был вызван ранее.
    /// </exception>
    public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      lock (this.SyncRoot)
        return this.DefineGlobalMethodNoLock(name, attributes, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
    }

    private MethodBuilder DefineGlobalMethodNoLock(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      if (this.m_moduleData.m_fGlobalBeenCreated)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GlobalsHaveBeenCreated"));
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
        throw new ArgumentException(Environment.GetResourceString("Argument_GlobalFunctionHasToBeStatic"));
      this.CheckContext(new Type[1]{ returnType });
      this.CheckContext(requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes);
      this.CheckContext(requiredParameterTypeCustomModifiers);
      this.CheckContext(optionalParameterTypeCustomModifiers);
      this.m_moduleData.m_fHasGlobal = true;
      return this.m_moduleData.m_globalTypeBuilder.DefineMethod(name, attributes, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
    }

    /// <summary>
    ///   Определяет <see langword="PInvoke" /> метод с указанным именем, имя DLL, в котором этот метод определен, атрибуты метода, соглашение о вызовах, возвращаемый тип метода, типы параметров метода и <see langword="PInvoke" /> флаги.
    /// </summary>
    /// <param name="name">
    ///   Имя метода <see langword="PInvoke" />.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="dllName">
    ///   Имя библиотеки DLL, в которой определен метод <see langword="PInvoke" />.
    /// </param>
    /// <param name="attributes">Атрибуты метода.</param>
    /// <param name="callingConvention">
    ///   Соглашение о вызове метода.
    /// </param>
    /// <param name="returnType">Возвращаемый тип метода.</param>
    /// <param name="parameterTypes">Типы параметров метода.</param>
    /// <param name="nativeCallConv">
    ///   Собственное соглашение о вызове.
    /// </param>
    /// <param name="nativeCharSet">Собственная кодировка метода.</param>
    /// <returns>
    ///   Определенный метод <see langword="PInvoke" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Метод не является статическим или если содержащий тип является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Метод является абстрактным.
    /// 
    ///   -или-
    /// 
    ///   Метод был определен ранее.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="dllName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />
    /// </exception>
    public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      return this.DefinePInvokeMethod(name, dllName, name, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
    }

    /// <summary>
    ///   Определяет <see langword="PInvoke" /> метод с указанным именем, имя DLL, в котором этот метод определен, атрибуты метода, соглашение о вызовах, возвращаемый тип метода, типы параметров метода и <see langword="PInvoke" /> флаги.
    /// </summary>
    /// <param name="name">
    ///   Имя метода <see langword="PInvoke" />.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="dllName">
    ///   Имя библиотеки DLL, в которой определен метод <see langword="PInvoke" />.
    /// </param>
    /// <param name="entryName">Имя точки входа в библиотеке DLL.</param>
    /// <param name="attributes">Атрибуты метода.</param>
    /// <param name="callingConvention">
    ///   Соглашение о вызове метода.
    /// </param>
    /// <param name="returnType">Возвращаемый тип метода.</param>
    /// <param name="parameterTypes">Типы параметров метода.</param>
    /// <param name="nativeCallConv">
    ///   Собственное соглашение о вызове.
    /// </param>
    /// <param name="nativeCharSet">Собственная кодировка метода.</param>
    /// <returns>
    ///   Определенный метод <see langword="PInvoke" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Метод не является статическим, или если содержащий тип является интерфейсом, или метод является абстрактным, если метод был ранее определен.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="dllName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />
    /// </exception>
    public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      lock (this.SyncRoot)
        return this.DefinePInvokeMethodNoLock(name, dllName, entryName, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
    }

    private MethodBuilder DefinePInvokeMethodNoLock(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
        throw new ArgumentException(Environment.GetResourceString("Argument_GlobalFunctionHasToBeStatic"));
      this.CheckContext(new Type[1]{ returnType });
      this.CheckContext(parameterTypes);
      this.m_moduleData.m_fHasGlobal = true;
      return this.m_moduleData.m_globalTypeBuilder.DefinePInvokeMethod(name, dllName, entryName, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
    }

    /// <summary>
    ///   Завершает определения глобальной функции и глобальных данных для этого динамического модуля.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот метод был вызван ранее.
    /// </exception>
    public void CreateGlobalFunctions()
    {
      lock (this.SyncRoot)
        this.CreateGlobalFunctionsNoLock();
    }

    private void CreateGlobalFunctionsNoLock()
    {
      if (this.m_moduleData.m_fGlobalBeenCreated)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
      this.m_moduleData.m_globalTypeBuilder.CreateType();
      this.m_moduleData.m_fGlobalBeenCreated = true;
    }

    /// <summary>
    ///   Определяет инициализированное поле данных в разделе .sdata переносимого исполняемого (PE) файла.
    /// </summary>
    /// <param name="name">
    ///   Имя, используемое для ссылки на данные.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="data">Большой двоичный объект (BLOB) данных.</param>
    /// <param name="attributes">
    ///   Атрибуты поля.
    ///    Значение по умолчанию — <see langword="Static" />.
    /// </param>
    /// <returns>Поле для ссылки на данные.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Размер <paramref name="data" /> меньше или равно нулю или больше или равен 0x3f0000.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="data" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> был вызван ранее.
    /// </exception>
    public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
    {
      lock (this.SyncRoot)
        return this.DefineInitializedDataNoLock(name, data, attributes);
    }

    private FieldBuilder DefineInitializedDataNoLock(string name, byte[] data, FieldAttributes attributes)
    {
      if (this.m_moduleData.m_fGlobalBeenCreated)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GlobalsHaveBeenCreated"));
      this.m_moduleData.m_fHasGlobal = true;
      return this.m_moduleData.m_globalTypeBuilder.DefineInitializedData(name, data, attributes);
    }

    /// <summary>
    ///   Определяет неинициализированное поле данных в разделе .sdata переносимого исполняемого (PE) файла.
    /// </summary>
    /// <param name="name">
    ///   Имя, используемое для ссылки на данные.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="size">Размер поля данных.</param>
    /// <param name="attributes">Атрибуты поля.</param>
    /// <returns>Поле для ссылки на данные.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="size" /> меньше или равно нулю либо больше или равно 0x003f0000.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> был вызван ранее.
    /// </exception>
    public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
    {
      lock (this.SyncRoot)
        return this.DefineUninitializedDataNoLock(name, size, attributes);
    }

    private FieldBuilder DefineUninitializedDataNoLock(string name, int size, FieldAttributes attributes)
    {
      if (this.m_moduleData.m_fGlobalBeenCreated)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GlobalsHaveBeenCreated"));
      this.m_moduleData.m_fHasGlobal = true;
      return this.m_moduleData.m_globalTypeBuilder.DefineUninitializedData(name, size, attributes);
    }

    [SecurityCritical]
    internal TypeToken GetTypeTokenInternal(Type type)
    {
      return this.GetTypeTokenInternal(type, false);
    }

    [SecurityCritical]
    private TypeToken GetTypeTokenInternal(Type type, bool getGenericDefinition)
    {
      lock (this.SyncRoot)
        return this.GetTypeTokenWorkerNoLock(type, getGenericDefinition);
    }

    /// <summary>
    ///   Возвращает маркер, используемый для идентификации указанного типа в данном модуле.
    /// </summary>
    /// <param name="type">
    ///   Тип объекта, который представляет тип класса.
    /// </param>
    /// <returns>
    ///   Маркер, используемый для идентификации указанного типа в данном модуле.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> является <see langword="ByRef" /> типа.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Это не временный модуль, который ссылается на несохраняемый модуль.
    /// </exception>
    [SecuritySafeCritical]
    public TypeToken GetTypeToken(Type type)
    {
      return this.GetTypeTokenInternal(type, true);
    }

    [SecurityCritical]
    private TypeToken GetTypeTokenWorkerNoLock(Type type, bool getGenericDefinition)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      this.CheckContext(new Type[1]{ type });
      if (type.IsByRef)
        throw new ArgumentException(Environment.GetResourceString("Argument_CannotGetTypeTokenForByRef"));
      if (type.IsGenericType && (!type.IsGenericTypeDefinition || !getGenericDefinition) || (type.IsGenericParameter || type.IsArray || type.IsPointer))
      {
        int length;
        return new TypeToken(this.GetTokenFromTypeSpec(SignatureHelper.GetTypeSigToken((Module) this, type).InternalGetSignature(out length), length));
      }
      Module module = type.Module;
      if (module.Equals((object) this))
      {
        EnumBuilder enumBuilder = type as EnumBuilder;
        TypeBuilder typeBuilder = !((Type) enumBuilder != (Type) null) ? type as TypeBuilder : enumBuilder.m_typeBuilder;
        if ((Type) typeBuilder != (Type) null)
          return typeBuilder.TypeToken;
        GenericTypeParameterBuilder parameterBuilder;
        if ((Type) (parameterBuilder = type as GenericTypeParameterBuilder) != (Type) null)
          return new TypeToken(parameterBuilder.MetadataTokenInternal);
        return new TypeToken(this.GetTypeRefNested(type, (Module) this, string.Empty));
      }
      ModuleBuilder moduleBuilder = module as ModuleBuilder;
      if (!this.IsTransient() & ((Module) moduleBuilder != (Module) null ? moduleBuilder.IsTransient() : ((RuntimeModule) module).IsTransientInternal()))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadTransientModuleReference"));
      string strRefedModuleFileName = string.Empty;
      if (module.Assembly.Equals((object) this.Assembly))
      {
        if ((Module) moduleBuilder == (Module) null)
          moduleBuilder = this.ContainingAssemblyBuilder.GetModuleBuilder((InternalModuleBuilder) module);
        strRefedModuleFileName = moduleBuilder.m_moduleData.m_strFileName;
      }
      return new TypeToken(this.GetTypeRefNested(type, module, strRefedModuleFileName));
    }

    /// <summary>
    ///   Возвращает маркер, используемый для идентификации типа с указанным именем.
    /// </summary>
    /// <param name="name">Имя класса, включая пространство имен.</param>
    /// <returns>
    ///   Токен, используемый для идентификации типа с указанным именем в данном модуле.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> является пустой строкой ("").
    /// 
    ///   -или-
    /// 
    ///   <paramref name="name" /> Представляет <see langword="ByRef" /> типа.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Тип, заданный параметром <paramref name="name" /> не найден.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Это не временный модуль, который ссылается на несохраняемый модуль.
    /// </exception>
    public TypeToken GetTypeToken(string name)
    {
      return this.GetTypeToken(this.InternalModule.GetType(name, false, true));
    }

    /// <summary>
    ///   Возвращает маркер, используемый для идентификации указанного метода в данном модуле.
    /// </summary>
    /// <param name="method">Метод, чтобы получить маркер.</param>
    /// <returns>
    ///   Маркер, используемый для идентификации указанного метода в данном модуле.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Объявляющий тип метода не в этом модуле.
    /// </exception>
    [SecuritySafeCritical]
    public MethodToken GetMethodToken(MethodInfo method)
    {
      lock (this.SyncRoot)
        return this.GetMethodTokenNoLock(method, true);
    }

    [SecurityCritical]
    internal MethodToken GetMethodTokenInternal(MethodInfo method)
    {
      lock (this.SyncRoot)
        return this.GetMethodTokenNoLock(method, false);
    }

    [SecurityCritical]
    private MethodToken GetMethodTokenNoLock(MethodInfo method, bool getGenericTypeDefinition)
    {
      if (method == (MethodInfo) null)
        throw new ArgumentNullException(nameof (method));
      MethodBuilder methodBuilder;
      int str;
      if ((MethodInfo) (methodBuilder = method as MethodBuilder) != (MethodInfo) null)
      {
        int metadataTokenInternal = methodBuilder.MetadataTokenInternal;
        if (method.Module.Equals((object) this))
          return new MethodToken(metadataTokenInternal);
        if (method.DeclaringType == (Type) null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule"));
        int tr = getGenericTypeDefinition ? this.GetTypeToken(method.DeclaringType).Token : this.GetTypeTokenInternal(method.DeclaringType).Token;
        str = this.GetMemberRef(method.DeclaringType.Module, tr, metadataTokenInternal);
      }
      else
      {
        if (method is MethodOnTypeBuilderInstantiation)
          return new MethodToken(this.GetMemberRefToken((MethodBase) method, (IEnumerable<Type>) null));
        SymbolMethod symbolMethod;
        if ((MethodInfo) (symbolMethod = method as SymbolMethod) != (MethodInfo) null)
        {
          if (symbolMethod.GetModule() == (Module) this)
            return symbolMethod.GetToken();
          return symbolMethod.GetToken(this);
        }
        Type declaringType = method.DeclaringType;
        if (declaringType == (Type) null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule"));
        if (declaringType.IsArray)
        {
          ParameterInfo[] parameters = method.GetParameters();
          Type[] parameterTypes = new Type[parameters.Length];
          for (int index = 0; index < parameters.Length; ++index)
            parameterTypes[index] = parameters[index].ParameterType;
          return this.GetArrayMethodToken(declaringType, method.Name, method.CallingConvention, method.ReturnType, parameterTypes);
        }
        RuntimeMethodInfo method1;
        if ((MethodInfo) (method1 = method as RuntimeMethodInfo) != (MethodInfo) null)
        {
          str = this.GetMemberRefOfMethodInfo(getGenericTypeDefinition ? this.GetTypeToken(method.DeclaringType).Token : this.GetTypeTokenInternal(method.DeclaringType).Token, method1);
        }
        else
        {
          ParameterInfo[] parameters = method.GetParameters();
          Type[] parameterTypes = new Type[parameters.Length];
          Type[][] requiredParameterTypeCustomModifiers = new Type[parameterTypes.Length][];
          Type[][] optionalParameterTypeCustomModifiers = new Type[parameterTypes.Length][];
          for (int index = 0; index < parameters.Length; ++index)
          {
            parameterTypes[index] = parameters[index].ParameterType;
            requiredParameterTypeCustomModifiers[index] = parameters[index].GetRequiredCustomModifiers();
            optionalParameterTypeCustomModifiers[index] = parameters[index].GetOptionalCustomModifiers();
          }
          int tr = getGenericTypeDefinition ? this.GetTypeToken(method.DeclaringType).Token : this.GetTypeTokenInternal(method.DeclaringType).Token;
          SignatureHelper methodSigHelper;
          try
          {
            methodSigHelper = SignatureHelper.GetMethodSigHelper((Module) this, method.CallingConvention, method.ReturnType, method.ReturnParameter.GetRequiredCustomModifiers(), method.ReturnParameter.GetOptionalCustomModifiers(), parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
          }
          catch (NotImplementedException ex)
          {
            methodSigHelper = SignatureHelper.GetMethodSigHelper((Module) this, method.ReturnType, parameterTypes);
          }
          int length;
          byte[] signature = methodSigHelper.InternalGetSignature(out length);
          str = this.GetMemberRefFromSignature(tr, method.Name, signature, length);
        }
      }
      return new MethodToken(str);
    }

    /// <summary>
    ///   Возвращает маркер, используемый для идентификации конструктора, который имеет указанные атрибуты и типы параметров в данном модуле.
    /// </summary>
    /// <param name="constructor">
    ///   Конструктор, чтобы получить маркер.
    /// </param>
    /// <param name="optionalParameterTypes">
    ///   Коллекция типов необязательных параметров в конструктор.
    /// </param>
    /// <returns>
    ///   Маркер, используемый для идентификации указанного конструктора в данном модуле.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="constructor" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public MethodToken GetConstructorToken(ConstructorInfo constructor, IEnumerable<Type> optionalParameterTypes)
    {
      if (constructor == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (constructor));
      lock (this.SyncRoot)
        return new MethodToken(this.GetMethodTokenInternal((MethodBase) constructor, optionalParameterTypes, false));
    }

    /// <summary>
    ///   Возвращает маркер, используемый для идентификации метода, имеющий указанные атрибуты и типы параметров в данном модуле.
    /// </summary>
    /// <param name="method">Метод, чтобы получить маркер.</param>
    /// <param name="optionalParameterTypes">
    ///   Коллекция типов необязательных параметров для метода.
    /// </param>
    /// <returns>
    ///   Маркер, используемый для идентификации указанного метода в данном модуле.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Объявляющий тип метода не в этом модуле.
    /// </exception>
    [SecuritySafeCritical]
    public MethodToken GetMethodToken(MethodInfo method, IEnumerable<Type> optionalParameterTypes)
    {
      if (method == (MethodInfo) null)
        throw new ArgumentNullException(nameof (method));
      lock (this.SyncRoot)
        return new MethodToken(this.GetMethodTokenInternal((MethodBase) method, optionalParameterTypes, true));
    }

    [SecurityCritical]
    internal int GetMethodTokenInternal(MethodBase method, IEnumerable<Type> optionalParameterTypes, bool useMethodDef)
    {
      MethodInfo method1 = method as MethodInfo;
      int num;
      if (method.IsGenericMethod)
      {
        MethodInfo method2 = method1;
        bool methodDefinition = method1.IsGenericMethodDefinition;
        if (!methodDefinition)
          method2 = method1.GetGenericMethodDefinition();
        int tkParent = !this.Equals((object) method2.Module) || method2.DeclaringType != (Type) null && method2.DeclaringType.IsGenericType ? this.GetMemberRefToken((MethodBase) method2, (IEnumerable<Type>) null) : this.GetMethodTokenInternal(method2).Token;
        if (methodDefinition & useMethodDef)
          return tkParent;
        int length;
        byte[] signature = SignatureHelper.GetMethodSpecSigHelper((Module) this, method1.GetGenericArguments()).InternalGetSignature(out length);
        num = TypeBuilder.DefineMethodSpec(this.GetNativeHandle(), tkParent, signature, length);
      }
      else
        num = (method.CallingConvention & CallingConventions.VarArgs) != (CallingConventions) 0 || !(method.DeclaringType == (Type) null) && method.DeclaringType.IsGenericType ? this.GetMemberRefToken(method, optionalParameterTypes) : (!(method1 != (MethodInfo) null) ? this.GetConstructorToken(method as ConstructorInfo).Token : this.GetMethodTokenInternal(method1).Token);
      return num;
    }

    /// <summary>
    ///   Возвращает маркер для именованного метода класса массива.
    /// </summary>
    /// <param name="arrayClass">Объект массива.</param>
    /// <param name="methodName">Строка, содержащая имя метода.</param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах для метода.
    /// </param>
    /// <param name="returnType">
    ///   Тип возвращаемого значения метода.
    /// </param>
    /// <param name="parameterTypes">Типы параметров метода.</param>
    /// <returns>Токен для именованного метода класса массива.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="arrayClass" /> не является массивом.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="methodName" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="arrayClass" /> или <paramref name="methodName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public MethodToken GetArrayMethodToken(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      lock (this.SyncRoot)
        return this.GetArrayMethodTokenNoLock(arrayClass, methodName, callingConvention, returnType, parameterTypes);
    }

    [SecurityCritical]
    private MethodToken GetArrayMethodTokenNoLock(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      if (arrayClass == (Type) null)
        throw new ArgumentNullException(nameof (arrayClass));
      if (methodName == null)
        throw new ArgumentNullException(nameof (methodName));
      if (methodName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (methodName));
      if (!arrayClass.IsArray)
        throw new ArgumentException(Environment.GetResourceString("Argument_HasToBeArrayClass"));
      this.CheckContext(returnType, arrayClass);
      this.CheckContext(parameterTypes);
      int length;
      byte[] signature = SignatureHelper.GetMethodSigHelper((Module) this, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null).InternalGetSignature(out length);
      return new MethodToken(ModuleBuilder.GetArrayMethodToken(this.GetNativeHandle(), this.GetTypeTokenInternal(arrayClass).Token, methodName, signature, length));
    }

    /// <summary>Возвращает именованный метод класса массива.</summary>
    /// <param name="arrayClass">Класс массива.</param>
    /// <param name="methodName">Имя метода класса массива.</param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах метода.
    /// </param>
    /// <param name="returnType">
    ///   Тип возвращаемого значения метода.
    /// </param>
    /// <param name="parameterTypes">Типы параметров метода.</param>
    /// <returns>Именованный метод класса массива.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="arrayClass" /> не является массивом.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="arrayClass" /> или <paramref name="methodName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public MethodInfo GetArrayMethod(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      this.CheckContext(returnType, arrayClass);
      this.CheckContext(parameterTypes);
      return (MethodInfo) new SymbolMethod(this, this.GetArrayMethodToken(arrayClass, methodName, callingConvention, returnType, parameterTypes), arrayClass, methodName, callingConvention, returnType, parameterTypes);
    }

    /// <summary>
    ///   Возвращает маркер, используемый для идентификации указанного конструктора в данном модуле.
    /// </summary>
    /// <param name="con">Конструктор, чтобы получить маркер.</param>
    /// <returns>
    ///   Маркер, используемый для идентификации указанного конструктора в данном модуле.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="con" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public MethodToken GetConstructorToken(ConstructorInfo con)
    {
      return this.InternalGetConstructorToken(con, false);
    }

    /// <summary>
    ///   Возвращает маркер, используемый для идентификации указанного поля в данном модуле.
    /// </summary>
    /// <param name="field">Поле, чтобы получить маркер.</param>
    /// <returns>
    ///   Маркер, используемый для идентификации указанного поля в данном модуле.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="field" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public FieldToken GetFieldToken(FieldInfo field)
    {
      lock (this.SyncRoot)
        return this.GetFieldTokenNoLock(field);
    }

    [SecurityCritical]
    private FieldToken GetFieldTokenNoLock(FieldInfo field)
    {
      if (field == (FieldInfo) null)
        throw new ArgumentNullException("con");
      FieldBuilder fieldBuilder;
      int field1;
      if ((FieldInfo) (fieldBuilder = field as FieldBuilder) != (FieldInfo) null)
      {
        if (field.DeclaringType != (Type) null && field.DeclaringType.IsGenericType)
        {
          int length;
          field1 = this.GetMemberRef((Module) this, this.GetTokenFromTypeSpec(SignatureHelper.GetTypeSigToken((Module) this, field.DeclaringType).InternalGetSignature(out length), length), fieldBuilder.GetToken().Token);
        }
        else
        {
          if (fieldBuilder.Module.Equals((object) this))
            return fieldBuilder.GetToken();
          if (field.DeclaringType == (Type) null)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule"));
          int token = this.GetTypeTokenInternal(field.DeclaringType).Token;
          field1 = this.GetMemberRef(field.ReflectedType.Module, token, fieldBuilder.GetToken().Token);
        }
      }
      else
      {
        RuntimeFieldInfo runtimeField;
        if ((FieldInfo) (runtimeField = field as RuntimeFieldInfo) != (FieldInfo) null)
        {
          if (field.DeclaringType == (Type) null)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule"));
          int length;
          field1 = !(field.DeclaringType != (Type) null) || !field.DeclaringType.IsGenericType ? this.GetMemberRefOfFieldInfo(this.GetTypeTokenInternal(field.DeclaringType).Token, field.DeclaringType.GetTypeHandleInternal(), runtimeField) : this.GetMemberRefOfFieldInfo(this.GetTokenFromTypeSpec(SignatureHelper.GetTypeSigToken((Module) this, field.DeclaringType).InternalGetSignature(out length), length), field.DeclaringType.GetTypeHandleInternal(), runtimeField);
        }
        else
        {
          FieldOnTypeBuilderInstantiation builderInstantiation;
          if ((FieldInfo) (builderInstantiation = field as FieldOnTypeBuilderInstantiation) != (FieldInfo) null)
          {
            int length;
            field1 = this.GetMemberRef(builderInstantiation.FieldInfo.ReflectedType.Module, this.GetTokenFromTypeSpec(SignatureHelper.GetTypeSigToken((Module) this, field.DeclaringType).InternalGetSignature(out length), length), builderInstantiation.MetadataTokenInternal);
          }
          else
          {
            int token = this.GetTypeTokenInternal(field.ReflectedType).Token;
            SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper((Module) this);
            fieldSigHelper.AddArgument(field.FieldType, field.GetRequiredCustomModifiers(), field.GetOptionalCustomModifiers());
            int length;
            byte[] signature = fieldSigHelper.InternalGetSignature(out length);
            field1 = this.GetMemberRefFromSignature(token, field.Name, signature, length);
          }
        }
      }
      return new FieldToken(field1, field.GetType());
    }

    /// <summary>
    ///   Возвращает токен заданной строки из пула констант модуля.
    /// </summary>
    /// <param name="str">
    ///   Строка для добавления в пул констант модуля.
    /// </param>
    /// <returns>Токен строки в пуле констант.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="str" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public StringToken GetStringConstant(string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      return new StringToken(ModuleBuilder.GetStringConstant(this.GetNativeHandle(), str, str.Length));
    }

    /// <summary>
    ///   Определяет маркер для подписи, который определен указанным <see cref="T:System.Reflection.Emit.SignatureHelper" />.
    /// </summary>
    /// <param name="sigHelper">Сигнатура.</param>
    /// <returns>Токен для определенной сигнатуры.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="sigHelper" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public SignatureToken GetSignatureToken(SignatureHelper sigHelper)
    {
      if (sigHelper == null)
        throw new ArgumentNullException(nameof (sigHelper));
      int length;
      return new SignatureToken(TypeBuilder.GetTokenFromSig(this.GetNativeHandle(), sigHelper.InternalGetSignature(out length), length), this);
    }

    /// <summary>
    ///   Определяет маркер для подписи, которая имеет указанный символ массива и длиной подписи.
    /// </summary>
    /// <param name="sigBytes">
    ///   Подпись большой двоичный объект (BLOB).
    /// </param>
    /// <param name="sigLength">
    ///   Длина большой двоичный объект подписи.
    /// </param>
    /// <returns>Токен для указанной подписи.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="sigBytes" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public SignatureToken GetSignatureToken(byte[] sigBytes, int sigLength)
    {
      if (sigBytes == null)
        throw new ArgumentNullException(nameof (sigBytes));
      byte[] signature = new byte[sigBytes.Length];
      Array.Copy((Array) sigBytes, (Array) signature, sigBytes.Length);
      return new SignatureToken(TypeBuilder.GetTokenFromSig(this.GetNativeHandle(), signature, sigLength), this);
    }

    /// <summary>
    ///   Применяет пользовательский атрибут для этого модуля с помощью указанного двоичных больших объектов (BLOB), представляющий атрибут.
    /// </summary>
    /// <param name="con">Конструктор настраиваемого атрибута.</param>
    /// <param name="binaryAttribute">
    ///   Байт BLOB-объект, представляющий атрибут.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="con" /> или <paramref name="binaryAttribute" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (con));
      if (binaryAttribute == null)
        throw new ArgumentNullException(nameof (binaryAttribute));
      TypeBuilder.DefineCustomAttribute(this, 1, this.GetConstructorToken(con).Token, binaryAttribute, false, false);
    }

    /// <summary>
    ///   Применяет пользовательский атрибут для этого модуля с помощью построителя настраиваемых атрибутов.
    /// </summary>
    /// <param name="customBuilder">
    ///   Вспомогательный класс, который определяет настраиваемый атрибут, чтобы применить экземпляр.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="customBuilder" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException(nameof (customBuilder));
      customBuilder.CreateCustomAttribute(this, 1);
    }

    /// <summary>
    ///   Возвращает средство записи символов, связанный с данным динамическим модулем.
    /// </summary>
    /// <returns>
    ///   Модуль записи символов, связанный с данным динамическим модулем.
    /// </returns>
    public ISymbolWriter GetSymWriter()
    {
      return this.m_iSymWriter;
    }

    /// <summary>Определяет документ-источник.</summary>
    /// <param name="url">URL-адрес для документа.</param>
    /// <param name="language">
    ///   GUID, идентифицирующий язык документа.
    ///    Это может быть <see cref="F:System.Guid.Empty" />.
    /// </param>
    /// <param name="languageVendor">
    ///   Идентификатор GUID, определяющий поставщика языка документа.
    ///    Это может быть <see cref="F:System.Guid.Empty" />.
    /// </param>
    /// <param name="documentType">
    ///   Идентификатор GUID, определяющий тип документа.
    ///    Это может быть <see cref="F:System.Guid.Empty" />.
    /// </param>
    /// <returns>Определенный документ.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="url" /> имеет значение <see langword="null" />.
    ///    Это отличие от более ранних версий платформы .NET Framework.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот метод вызывается для динамического модуля, не является отладочным.
    /// </exception>
    public ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
    {
      if (url == null)
        throw new ArgumentNullException(nameof (url));
      lock (this.SyncRoot)
        return this.DefineDocumentNoLock(url, language, languageVendor, documentType);
    }

    private ISymbolDocumentWriter DefineDocumentNoLock(string url, Guid language, Guid languageVendor, Guid documentType)
    {
      if (this.m_iSymWriter == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
      return this.m_iSymWriter.DefineDocument(url, language, languageVendor, documentType);
    }

    /// <summary>Задает точку входа пользователя.</summary>
    /// <param name="entryPoint">Точки входа пользователя.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="entryPoint" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот метод вызывается для динамического модуля, не является отладочным.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="entryPoint" /> не содержится в данном динамическом модуле.
    /// </exception>
    [SecuritySafeCritical]
    public void SetUserEntryPoint(MethodInfo entryPoint)
    {
      lock (this.SyncRoot)
        this.SetUserEntryPointNoLock(entryPoint);
    }

    [SecurityCritical]
    private void SetUserEntryPointNoLock(MethodInfo entryPoint)
    {
      if (entryPoint == (MethodInfo) null)
        throw new ArgumentNullException(nameof (entryPoint));
      if (this.m_iSymWriter == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
      if (entryPoint.DeclaringType != (Type) null)
      {
        if (!entryPoint.Module.Equals((object) this))
          throw new InvalidOperationException(Environment.GetResourceString("Argument_NotInTheSameModuleBuilder"));
      }
      else
      {
        MethodBuilder methodBuilder = entryPoint as MethodBuilder;
        if ((MethodInfo) methodBuilder != (MethodInfo) null && (Module) methodBuilder.GetModuleBuilder() != (Module) this)
          throw new InvalidOperationException(Environment.GetResourceString("Argument_NotInTheSameModuleBuilder"));
      }
      this.m_iSymWriter.SetUserEntryPoint(new SymbolToken(this.GetMethodTokenInternal(entryPoint).Token));
    }

    /// <summary>Этот метод не выполняет никаких действий.</summary>
    /// <param name="name">Имя настраиваемого атрибута</param>
    /// <param name="data">
    ///   Непрозрачный большой двоичный объект (BLOB) байтов, представляющий значение настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="url" /> имеет значение <see langword="null" />.
    /// </exception>
    public void SetSymCustomAttribute(string name, byte[] data)
    {
      lock (this.SyncRoot)
        this.SetSymCustomAttributeNoLock(name, data);
    }

    private void SetSymCustomAttributeNoLock(string name, byte[] data)
    {
      if (this.m_iSymWriter == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли этот динамический модуль является несохраняемым.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если данный динамический модуль является несохраняемым; в противном случае — <see langword="false" />.
    /// </returns>
    public bool IsTransient()
    {
      return this.InternalModule.IsTransientInternal();
    }

    void _ModuleBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _ModuleBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _ModuleBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _ModuleBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
