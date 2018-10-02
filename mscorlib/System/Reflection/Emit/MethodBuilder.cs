// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.MethodBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Определяет и представляет метод (или конструктор) для динамического класса.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_MethodBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class MethodBuilder : MethodInfo, _MethodBuilder
  {
    private int m_maxStack = 16;
    internal string m_strName;
    private MethodToken m_tkMethod;
    private ModuleBuilder m_module;
    internal TypeBuilder m_containingType;
    private int[] m_mdMethodFixups;
    private byte[] m_localSignature;
    internal LocalSymInfo m_localSymInfo;
    internal ILGenerator m_ilGenerator;
    private byte[] m_ubBody;
    private ExceptionHandler[] m_exceptions;
    private const int DefaultMaxStack = 16;
    internal bool m_bIsBaked;
    private bool m_bIsGlobalMethod;
    private bool m_fInitLocals;
    private MethodAttributes m_iAttributes;
    private CallingConventions m_callingConvention;
    private MethodImplAttributes m_dwMethodImplFlags;
    private SignatureHelper m_signature;
    internal Type[] m_parameterTypes;
    private ParameterBuilder m_retParam;
    private Type m_returnType;
    private Type[] m_returnTypeRequiredCustomModifiers;
    private Type[] m_returnTypeOptionalCustomModifiers;
    private Type[][] m_parameterTypeRequiredCustomModifiers;
    private Type[][] m_parameterTypeOptionalCustomModifiers;
    private GenericTypeParameterBuilder[] m_inst;
    private bool m_bIsGenMethDef;
    private List<MethodBuilder.SymCustomAttr> m_symCustomAttrs;
    internal bool m_canBeRuntimeImpl;
    internal bool m_isDllImport;

    internal MethodBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, ModuleBuilder mod, TypeBuilder type, bool bIsGlobalMethod)
    {
      this.Init(name, attributes, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null, mod, type, bIsGlobalMethod);
    }

    internal MethodBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, ModuleBuilder mod, TypeBuilder type, bool bIsGlobalMethod)
    {
      this.Init(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, mod, type, bIsGlobalMethod);
    }

    private void Init(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, ModuleBuilder mod, TypeBuilder type, bool bIsGlobalMethod)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      if (name[0] == char.MinValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), nameof (name));
      if ((Module) mod == (Module) null)
        throw new ArgumentNullException(nameof (mod));
      if (parameterTypes != null)
      {
        foreach (Type parameterType in parameterTypes)
        {
          if (parameterType == (Type) null)
            throw new ArgumentNullException(nameof (parameterTypes));
        }
      }
      this.m_strName = name;
      this.m_module = mod;
      this.m_containingType = type;
      this.m_returnType = returnType;
      if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
        callingConvention |= CallingConventions.HasThis;
      else if ((attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
        throw new ArgumentException(Environment.GetResourceString("Arg_NoStaticVirtual"));
      if ((attributes & MethodAttributes.SpecialName) != MethodAttributes.SpecialName && (type.Attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask && ((attributes & (MethodAttributes.Virtual | MethodAttributes.Abstract)) != (MethodAttributes.Virtual | MethodAttributes.Abstract) && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope))
        throw new ArgumentException(Environment.GetResourceString("Argument_BadAttributeOnInterfaceMethod"));
      this.m_callingConvention = callingConvention;
      if (parameterTypes != null)
      {
        this.m_parameterTypes = new Type[parameterTypes.Length];
        Array.Copy((Array) parameterTypes, (Array) this.m_parameterTypes, parameterTypes.Length);
      }
      else
        this.m_parameterTypes = (Type[]) null;
      this.m_returnTypeRequiredCustomModifiers = returnTypeRequiredCustomModifiers;
      this.m_returnTypeOptionalCustomModifiers = returnTypeOptionalCustomModifiers;
      this.m_parameterTypeRequiredCustomModifiers = parameterTypeRequiredCustomModifiers;
      this.m_parameterTypeOptionalCustomModifiers = parameterTypeOptionalCustomModifiers;
      this.m_iAttributes = attributes;
      this.m_bIsGlobalMethod = bIsGlobalMethod;
      this.m_bIsBaked = false;
      this.m_fInitLocals = true;
      this.m_localSymInfo = new LocalSymInfo();
      this.m_ubBody = (byte[]) null;
      this.m_ilGenerator = (ILGenerator) null;
      this.m_dwMethodImplFlags = MethodImplAttributes.IL;
    }

    internal void CheckContext(params Type[][] typess)
    {
      this.m_module.CheckContext(typess);
    }

    internal void CheckContext(params Type[] types)
    {
      this.m_module.CheckContext(types);
    }

    [SecurityCritical]
    internal void CreateMethodBodyHelper(ILGenerator il)
    {
      if (il == null)
        throw new ArgumentNullException(nameof (il));
      int num = 0;
      ModuleBuilder module = this.m_module;
      this.m_containingType.ThrowIfCreated();
      if (this.m_bIsBaked)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodHasBody"));
      if (il.m_methodBuilder != (MethodInfo) this && il.m_methodBuilder != (MethodInfo) null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadILGeneratorUsage"));
      this.ThrowIfShouldNotHaveBody();
      if (il.m_ScopeTree.m_iOpenScopeCount != 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OpenLocalVariableScope"));
      this.m_ubBody = il.BakeByteArray();
      this.m_mdMethodFixups = il.GetTokenFixups();
      __ExceptionInfo[] exceptions = il.GetExceptions();
      int numberOfExceptions = this.CalculateNumberOfExceptions(exceptions);
      if (numberOfExceptions > 0)
      {
        this.m_exceptions = new ExceptionHandler[numberOfExceptions];
        for (int index1 = 0; index1 < exceptions.Length; ++index1)
        {
          int[] filterAddresses = exceptions[index1].GetFilterAddresses();
          int[] catchAddresses = exceptions[index1].GetCatchAddresses();
          int[] catchEndAddresses = exceptions[index1].GetCatchEndAddresses();
          Type[] catchClass = exceptions[index1].GetCatchClass();
          int numberOfCatches = exceptions[index1].GetNumberOfCatches();
          int startAddress = exceptions[index1].GetStartAddress();
          int endAddress = exceptions[index1].GetEndAddress();
          int[] exceptionTypes = exceptions[index1].GetExceptionTypes();
          for (int index2 = 0; index2 < numberOfCatches; ++index2)
          {
            int exceptionTypeToken = 0;
            if (catchClass[index2] != (Type) null)
              exceptionTypeToken = module.GetTypeTokenInternal(catchClass[index2]).Token;
            switch (exceptionTypes[index2])
            {
              case 0:
              case 1:
              case 4:
                this.m_exceptions[num++] = new ExceptionHandler(startAddress, endAddress, filterAddresses[index2], catchAddresses[index2], catchEndAddresses[index2], exceptionTypes[index2], exceptionTypeToken);
                break;
              case 2:
                this.m_exceptions[num++] = new ExceptionHandler(startAddress, exceptions[index1].GetFinallyEndAddress(), filterAddresses[index2], catchAddresses[index2], catchEndAddresses[index2], exceptionTypes[index2], exceptionTypeToken);
                break;
            }
          }
        }
      }
      this.m_bIsBaked = true;
      if (module.GetSymWriter() == null)
        return;
      SymbolToken method = new SymbolToken(this.MetadataTokenInternal);
      ISymbolWriter symWriter = module.GetSymWriter();
      symWriter.OpenMethod(method);
      symWriter.OpenScope(0);
      if (this.m_symCustomAttrs != null)
      {
        foreach (MethodBuilder.SymCustomAttr symCustomAttr in this.m_symCustomAttrs)
          module.GetSymWriter().SetSymAttribute(new SymbolToken(this.MetadataTokenInternal), symCustomAttr.m_name, symCustomAttr.m_data);
      }
      if (this.m_localSymInfo != null)
        this.m_localSymInfo.EmitLocalSymInfo(symWriter);
      il.m_ScopeTree.EmitScopeTree(symWriter);
      il.m_LineNumberInfo.EmitLineNumberInfo(symWriter);
      symWriter.CloseScope(il.ILOffset);
      symWriter.CloseMethod();
    }

    internal void ReleaseBakedStructures()
    {
      if (!this.m_bIsBaked)
        return;
      this.m_ubBody = (byte[]) null;
      this.m_localSymInfo = (LocalSymInfo) null;
      this.m_mdMethodFixups = (int[]) null;
      this.m_localSignature = (byte[]) null;
      this.m_exceptions = (ExceptionHandler[]) null;
    }

    internal override Type[] GetParameterTypes()
    {
      if (this.m_parameterTypes == null)
        this.m_parameterTypes = EmptyArray<Type>.Value;
      return this.m_parameterTypes;
    }

    internal static Type GetMethodBaseReturnType(MethodBase method)
    {
      MethodInfo methodInfo;
      if ((methodInfo = method as MethodInfo) != (MethodInfo) null)
        return methodInfo.ReturnType;
      ConstructorInfo constructorInfo;
      if ((constructorInfo = method as ConstructorInfo) != (ConstructorInfo) null)
        return constructorInfo.GetReturnType();
      return (Type) null;
    }

    internal void SetToken(MethodToken token)
    {
      this.m_tkMethod = token;
    }

    internal byte[] GetBody()
    {
      return this.m_ubBody;
    }

    internal int[] GetTokenFixups()
    {
      return this.m_mdMethodFixups;
    }

    [SecurityCritical]
    internal SignatureHelper GetMethodSignature()
    {
      if (this.m_parameterTypes == null)
        this.m_parameterTypes = EmptyArray<Type>.Value;
      this.m_signature = SignatureHelper.GetMethodSigHelper((Module) this.m_module, this.m_callingConvention, this.m_inst != null ? this.m_inst.Length : 0, this.m_returnType == (Type) null ? typeof (void) : this.m_returnType, this.m_returnTypeRequiredCustomModifiers, this.m_returnTypeOptionalCustomModifiers, this.m_parameterTypes, this.m_parameterTypeRequiredCustomModifiers, this.m_parameterTypeOptionalCustomModifiers);
      return this.m_signature;
    }

    internal byte[] GetLocalSignature(out int signatureLength)
    {
      if (this.m_localSignature != null)
      {
        signatureLength = this.m_localSignature.Length;
        return this.m_localSignature;
      }
      if (this.m_ilGenerator != null && this.m_ilGenerator.m_localCount != 0)
        return this.m_ilGenerator.m_localSignature.InternalGetSignature(out signatureLength);
      return SignatureHelper.GetLocalVarSigHelper((Module) this.m_module).InternalGetSignature(out signatureLength);
    }

    internal int GetMaxStack()
    {
      if (this.m_ilGenerator != null)
        return this.m_ilGenerator.GetMaxStackSize() + this.ExceptionHandlerCount;
      return this.m_maxStack;
    }

    internal ExceptionHandler[] GetExceptionHandlers()
    {
      return this.m_exceptions;
    }

    internal int ExceptionHandlerCount
    {
      get
      {
        if (this.m_exceptions == null)
          return 0;
        return this.m_exceptions.Length;
      }
    }

    internal int CalculateNumberOfExceptions(__ExceptionInfo[] excp)
    {
      int num = 0;
      if (excp == null)
        return 0;
      for (int index = 0; index < excp.Length; ++index)
        num += excp[index].GetNumberOfCatches();
      return num;
    }

    internal bool IsTypeCreated()
    {
      if ((Type) this.m_containingType != (Type) null)
        return this.m_containingType.IsCreated();
      return false;
    }

    internal TypeBuilder GetTypeBuilder()
    {
      return this.m_containingType;
    }

    internal ModuleBuilder GetModuleBuilder()
    {
      return this.m_module;
    }

    /// <summary>
    ///   Определяет, равен ли данный объект этому экземпляру.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим экземпляром <see langword="MethodBuilder" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="obj" /> является экземпляром <see langword="MethodBuilder" /> и равен этому объекту; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    public override bool Equals(object obj)
    {
      return obj is MethodBuilder && this.m_strName.Equals(((MethodBuilder) obj).m_strName) && (this.m_iAttributes == ((MethodBuilder) obj).m_iAttributes && ((MethodBuilder) obj).GetMethodSignature().Equals((object) this.GetMethodSignature()));
    }

    /// <summary>Возвращает хэш-код для этого метода.</summary>
    /// <returns>Хэш-код для этого метода.</returns>
    public override int GetHashCode()
    {
      return this.m_strName.GetHashCode();
    }

    /// <summary>
    ///   Возвращает этот экземпляр <see langword="MethodBuilder" /> в виде строки.
    /// </summary>
    /// <returns>
    ///   Возвращает строку, содержащую имя, атрибуты, сигнатуру метода, исключения и локальную сигнатуру данного метода, за которой следует текущий поток MSIL.
    /// </returns>
    [SecuritySafeCritical]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(1000);
      stringBuilder.Append("Name: " + this.m_strName + " " + Environment.NewLine);
      stringBuilder.Append("Attributes: " + (object) this.m_iAttributes + Environment.NewLine);
      stringBuilder.Append("Method Signature: " + (object) this.GetMethodSignature() + Environment.NewLine);
      stringBuilder.Append(Environment.NewLine);
      return stringBuilder.ToString();
    }

    /// <summary>Извлекает имя данного метода.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Извлекает строку, содержащую простое имя этого метода.
    /// </returns>
    public override string Name
    {
      get
      {
        return this.m_strName;
      }
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.GetToken().Token;
      }
    }

    /// <summary>
    ///   Возвращает модуль, в котором определяется текущий метод.
    /// </summary>
    /// <returns>
    ///   Модуль <see cref="T:System.Reflection.Module" />, в котором определяется член, представленный текущим <see cref="T:System.Reflection.MemberInfo" />.
    /// </returns>
    public override Module Module
    {
      get
      {
        return this.m_containingType.Module;
      }
    }

    /// <summary>Возвращает тип, объявляющий этот метод.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Тип, объявляющий этот метод.
    /// </returns>
    public override Type DeclaringType
    {
      get
      {
        if (this.m_containingType.m_isHiddenGlobalType)
          return (Type) null;
        return (Type) this.m_containingType;
      }
    }

    /// <summary>
    ///   Возвращает настраиваемые атрибуты возвращаемого типа метода.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Настраиваемые атрибуты типа возвращаемого значения метода.
    /// </returns>
    public override ICustomAttributeProvider ReturnTypeCustomAttributes
    {
      get
      {
        return (ICustomAttributeProvider) null;
      }
    }

    /// <summary>
    ///   Возвращает класс, который использовался в отражении для получения этого объекта.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Тип, используемый для получения этого метода.
    /// </returns>
    public override Type ReflectedType
    {
      get
      {
        return this.DeclaringType;
      }
    }

    /// <summary>
    ///   Динамически вызывает метод, отраженный этим экземпляром для данного объекта, передавая указанные параметры и учитывая ограничения данного модуля привязки.
    /// </summary>
    /// <param name="obj">
    ///   Объект, для которого нужно вызвать указанный метод.
    ///    Если метод является статическим, этот параметр игнорируется.
    /// </param>
    /// <param name="invokeAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов членов, а также поиск объектов MemberInfo с помощью отражения.
    ///    Если модуль привязки имеет значение <see langword="null" />, используется модуль привязки по умолчанию.
    ///    Дополнительные сведения см. в разделе <see cref="T:System.Reflection.Binder" />.
    /// </param>
    /// <param name="parameters">
    ///   Список аргументов.
    ///    Это массив аргументов с тем же числом, порядком и типом, что и параметры вызываемого метода.
    ///    Если параметров нет, должно быть указано значение <see langword="null" />.
    /// </param>
    /// <param name="culture">
    ///   Экземпляр объекта <see cref="T:System.Globalization.CultureInfo" />, используемого для управления приведением типов.
    ///    Если значение параметра — NULL, для текущего потока используется <see cref="T:System.Globalization.CultureInfo" />.
    ///    (Это необходимо, например, чтобы преобразовывать объект <see cref="T:System.String" />, представляющий 1000, в значение <see cref="T:System.Double" />, поскольку в разных языках и региональных параметрах 1000 представляется по-разному.)
    /// </param>
    /// <returns>
    ///   Возвращает объект, содержащий возвращаемое значение вызываемого элемента.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    ///    Извлеките метод с помощью <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> и вызовите <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" /> в возвращенном методе <see cref="T:System.Reflection.MethodInfo" />.
    /// </exception>
    public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>Возвращает флаги реализации для метода.</summary>
    /// <returns>Возвращает флаги реализации для метода.</returns>
    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      return this.m_dwMethodImplFlags;
    }

    /// <summary>Извлекает атрибуты для данного метода.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Извлекает <see langword="MethodAttributes" /> для данного метода.
    /// </returns>
    public override MethodAttributes Attributes
    {
      get
      {
        return this.m_iAttributes;
      }
    }

    /// <summary>Возвращает соглашение о вызовах метода.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Соглашение о вызовах метода.
    /// </returns>
    public override CallingConventions CallingConvention
    {
      get
      {
        return this.m_callingConvention;
      }
    }

    /// <summary>
    ///   Извлекает внутренний маркер метода.
    ///    Используйте этот маркер для доступа к основному дескриптору метаданных.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Внутренний маркер метода.
    ///    Используйте этот маркер для доступа к основному дескриптору метаданных.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    ///    Извлеките метод с помощью <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> и вызовите <see cref="P:System.Reflection.MethodBase.MethodHandle" /> в возвращенном методе <see cref="T:System.Reflection.MethodInfo" />.
    /// </exception>
    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    /// <summary>
    ///   Во всех случаях вызывает исключение <see cref="T:System.NotSupportedException" />.
    /// </summary>
    /// <returns>
    ///   Во всех случаях вызывает исключение <see cref="T:System.NotSupportedException" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    ///    Это свойство не поддерживается в динамических сборках.
    ///    См. заметки.
    /// </exception>
    public override bool IsSecurityCritical
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    /// <summary>
    ///   Во всех случаях вызывает исключение <see cref="T:System.NotSupportedException" />.
    /// </summary>
    /// <returns>
    ///   Во всех случаях вызывает исключение <see cref="T:System.NotSupportedException" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    ///    Это свойство не поддерживается в динамических сборках.
    ///    См. заметки.
    /// </exception>
    public override bool IsSecuritySafeCritical
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    /// <summary>
    ///   Во всех случаях вызывает исключение <see cref="T:System.NotSupportedException" />.
    /// </summary>
    /// <returns>
    ///   Во всех случаях вызывает исключение <see cref="T:System.NotSupportedException" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    ///    Это свойство не поддерживается в динамических сборках.
    ///    См. заметки.
    /// </exception>
    public override bool IsSecurityTransparent
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    /// <summary>Возвращает базовую реализацию метода.</summary>
    /// <returns>Базовая реализация этого метода.</returns>
    public override MethodInfo GetBaseDefinition()
    {
      return (MethodInfo) this;
    }

    /// <summary>
    ///   Возвращает метод, представленный этим <see cref="T:System.Reflection.Emit.MethodBuilder" />.
    /// </summary>
    /// <returns>Возвращаемый тип метода.</returns>
    public override Type ReturnType
    {
      get
      {
        return this.m_returnType;
      }
    }

    /// <summary>Возвращает параметры данного метода.</summary>
    /// <returns>
    ///   Массив объектов <see langword="ParameterInfo" />, которые представляют параметры метода.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    ///    Извлеките метод с помощью <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> и вызовите <see langword="GetParameters" /> в возвращенном методе <see cref="T:System.Reflection.MethodInfo" />.
    /// </exception>
    public override ParameterInfo[] GetParameters()
    {
      if (!this.m_bIsBaked || (Type) this.m_containingType == (Type) null || this.m_containingType.BakedRuntimeType == (RuntimeType) null)
        throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_TypeNotCreated"));
      return this.m_containingType.GetMethod(this.m_strName, this.m_parameterTypes).GetParameters();
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Reflection.ParameterInfo" />, который содержит сведения о типе возвращаемого значения этого метода, например, имеет ли возвращаемый тип пользовательские модификаторы.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.ParameterInfo" />, содержащий сведения о типе возвращаемого значения.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Объявляющий тип не создан.
    /// </exception>
    public override ParameterInfo ReturnParameter
    {
      get
      {
        if (!this.m_bIsBaked || (Type) this.m_containingType == (Type) null || this.m_containingType.BakedRuntimeType == (RuntimeType) null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeNotCreated"));
        return this.m_containingType.GetMethod(this.m_strName, this.m_parameterTypes).ReturnParameter;
      }
    }

    /// <summary>
    ///   Возвращает все настраиваемые атрибуты, определенные для данного метода.
    /// </summary>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск в цепочке наследования этого члена для нахождения настраиваемых атрибутов.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов, представляющих все настраиваемые атрибуты этого метода.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    ///    Извлеките метод с помощью <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> и вызовите <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> в возвращенном методе <see cref="T:System.Reflection.MethodInfo" />.
    /// </exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Возвращает настраиваемые атрибуты, определяемые заданным типом.
    /// </summary>
    /// <param name="attributeType">Тип настраиваемого атрибута.</param>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск в цепочке наследования этого члена для нахождения настраиваемых атрибутов.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов, представляющих атрибуты этого метода, которые имеют тип <paramref name="attributeType" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    ///    Извлеките метод с помощью <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> и вызовите <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> в возвращенном методе <see cref="T:System.Reflection.MethodInfo" />.
    /// </exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Проверяет, определен ли заданный тип настраиваемого атрибута.
    /// </summary>
    /// <param name="attributeType">Тип настраиваемого атрибута.</param>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск в цепочке наследования этого члена для нахождения настраиваемых атрибутов.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если заданный тип настраиваемых атрибутов определен; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    ///    Извлеките метод с помощью <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> и вызовите <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> в возвращенном методе <see cref="T:System.Reflection.MethodInfo" />.
    /// </exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Получает значение, указывающее, представляет ли текущий объект <see cref="T:System.Reflection.Emit.MethodBuilder" /> определение универсального метода.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see cref="T:System.Reflection.Emit.MethodBuilder" /> представляет определение универсального метода; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsGenericMethodDefinition
    {
      get
      {
        return this.m_bIsGenMethDef;
      }
    }

    /// <summary>Не поддерживается для этого типа.</summary>
    /// <returns>Не поддерживается.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызванный метод не поддерживается в базовом классе.
    /// </exception>
    public override bool ContainsGenericParameters
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    /// <summary>Возвращает этот метод.</summary>
    /// <returns>
    ///   Текущий экземпляр <see cref="T:System.Reflection.Emit.MethodBuilder" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий метод не является универсальным.
    ///    То есть свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> возвращает значение <see langword="false" />.
    /// </exception>
    public override MethodInfo GetGenericMethodDefinition()
    {
      if (!this.IsGenericMethod)
        throw new InvalidOperationException();
      return (MethodInfo) this;
    }

    /// <summary>
    ///   Получает значение, которое указывает, является ли метод универсальным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если метод является универсальным; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsGenericMethod
    {
      get
      {
        return this.m_inst != null;
      }
    }

    /// <summary>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />, которые представляют параметры типа для метода, если он является универсальным.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />, представляющих параметры типа, если метод является универсальным, или <see langword="null" />, если метод не является универсальным.
    /// </returns>
    public override Type[] GetGenericArguments()
    {
      return (Type[]) this.m_inst;
    }

    /// <summary>
    ///   Возвращает универсальный метод, построенный на основе определения текущего универсального метода с использованием указанных аргументов универсального типа.
    /// </summary>
    /// <param name="typeArguments">
    ///   Массив объектов <see cref="T:System.Type" />, которые представляют аргументы типа для универсального метода.
    /// </param>
    /// <returns>
    ///   Сведения <see cref="T:System.Reflection.MethodInfo" />, представляющие универсальный метод, построенный на основе определения текущего универсального метода с использованием указанных аргументов универсального типа.
    /// </returns>
    public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
    {
      return MethodBuilderInstantiation.MakeGenericMethod((MethodInfo) this, typeArguments);
    }

    /// <summary>
    ///   Задает количество параметров универсального типа для текущего метода, указывает их имена и возвращает массив объектов <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />, которые можно использовать для определения их ограничений.
    /// </summary>
    /// <param name="names">
    ///   Массив строк, представляющих имена параметров универсального типа.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />, представляющих параметры типа универсального метода.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для этого метода уже были определены параметры универсального типа.
    /// 
    ///   -или-
    /// 
    ///   Метод уже завершен.
    /// 
    ///   -или-
    /// 
    ///   Метод <see cref="M:System.Reflection.Emit.MethodBuilder.SetImplementationFlags(System.Reflection.MethodImplAttributes)" /> был вызван для текущего метода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="names" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Элемент <paramref name="names" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="names" /> является пустым массивом.
    /// </exception>
    public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
    {
      if (names == null)
        throw new ArgumentNullException(nameof (names));
      if (names.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_EmptyArray"), nameof (names));
      if (this.m_inst != null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GenericParametersAlreadySet"));
      for (int index = 0; index < names.Length; ++index)
      {
        if (names[index] == null)
          throw new ArgumentNullException(nameof (names));
      }
      if (this.m_tkMethod.Token != 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBuilderBaked"));
      this.m_bIsGenMethDef = true;
      this.m_inst = new GenericTypeParameterBuilder[names.Length];
      for (int genParamPos = 0; genParamPos < names.Length; ++genParamPos)
        this.m_inst[genParamPos] = new GenericTypeParameterBuilder(new TypeBuilder(names[genParamPos], genParamPos, this));
      return this.m_inst;
    }

    internal void ThrowIfGeneric()
    {
      if (this.IsGenericMethod && !this.IsGenericMethodDefinition)
        throw new InvalidOperationException();
    }

    /// <summary>
    ///   Возвращает объект <see langword="MethodToken" />, представляющий маркер для этого метода.
    /// </summary>
    /// <returns>
    ///   Возвращает объект <see langword="MethodToken" /> этого метода.
    /// </returns>
    [SecuritySafeCritical]
    public MethodToken GetToken()
    {
      if (this.m_tkMethod.Token != 0)
        return this.m_tkMethod;
      MethodToken methodToken = new MethodToken(0);
      lock (this.m_containingType.m_listMethods)
      {
        if (this.m_tkMethod.Token != 0)
          return this.m_tkMethod;
        int index;
        for (index = this.m_containingType.m_lastTokenizedMethod + 1; index < this.m_containingType.m_listMethods.Count; ++index)
        {
          MethodBuilder listMethod = this.m_containingType.m_listMethods[index];
          methodToken = listMethod.GetTokenNoLock();
          if ((MethodInfo) listMethod == (MethodInfo) this)
            break;
        }
        this.m_containingType.m_lastTokenizedMethod = index;
      }
      return methodToken;
    }

    [SecurityCritical]
    private MethodToken GetTokenNoLock()
    {
      int length;
      int num = TypeBuilder.DefineMethod(this.m_module.GetNativeHandle(), this.m_containingType.MetadataTokenInternal, this.m_strName, this.GetMethodSignature().InternalGetSignature(out length), length, this.Attributes);
      this.m_tkMethod = new MethodToken(num);
      if (this.m_inst != null)
      {
        foreach (GenericTypeParameterBuilder parameterBuilder in this.m_inst)
        {
          if (!parameterBuilder.m_type.IsCreated())
            parameterBuilder.m_type.CreateType();
        }
      }
      TypeBuilder.SetMethodImpl(this.m_module.GetNativeHandle(), num, this.m_dwMethodImplFlags);
      return this.m_tkMethod;
    }

    /// <summary>Задает количество и типы параметров для метода.</summary>
    /// <param name="parameterTypes">
    ///   Массив объектов <see cref="T:System.Type" />, представляющих типы параметров.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий метод является универсальным, но определение метода универсальным не является.
    ///    Свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, однако свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    public void SetParameters(params Type[] parameterTypes)
    {
      this.CheckContext(parameterTypes);
      this.SetSignature((Type) null, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>Задает возвращаемый тип метода.</summary>
    /// <param name="returnType">
    ///   Объект <see cref="T:System.Type" />, представляющий возвращаемый тип метода.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий метод является универсальным, но не является универсальным определением метода.
    ///    Свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, однако свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    public void SetReturnType(Type returnType)
    {
      this.CheckContext(new Type[1]{ returnType });
      this.SetSignature(returnType, (Type[]) null, (Type[]) null, (Type[]) null, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>
    ///   Задает сигнатуру метода, включая возвращаемый тип, типы параметров, а также обязательные и необязательные настраиваемые модификаторы для возвращаемого типа и типов параметров.
    /// </summary>
    /// <param name="returnType">Тип возвращаемых данных метода.</param>
    /// <param name="returnTypeRequiredCustomModifiers">
    ///   Массив типов, представляющих обязательные настраиваемые модификаторы, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />, для возвращаемого типа метода.
    ///    Если возвращаемый тип не содержит требуемых настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="returnTypeOptionalCustomModifiers">
    ///   Массив типов, представляющих необязательные настраиваемые модификаторы, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />, для возвращаемого типа метода.
    ///    Если возвращаемый тип не содержит необязательных настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="parameterTypes">Типы параметров метода.</param>
    /// <param name="parameterTypeRequiredCustomModifiers">
    ///   Массив массивов типов.
    ///    Каждый массив типов представляет обязательные настраиваемые модификаторы для соответствующего параметра, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />.
    ///    Если определенный параметр не имеет обязательных настраиваемых модификаторов, укажите <see langword="null" /> вместо массива массивов.
    ///    Если ни один из параметров не имеет обязательных настраиваемых модификаторов, укажите <see langword="null" /> вместо массива массивов.
    /// </param>
    /// <param name="parameterTypeOptionalCustomModifiers">
    ///   Массив массивов типов.
    ///    Каждый массив типов представляет необязательные настраиваемые модификаторы для соответствующего параметра, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />.
    ///    Если определенный параметр не имеет необязательных настраиваемых модификаторов, укажите <see langword="null" /> вместо массива массивов.
    ///    Если ни один из параметров не имеет необязательных настраиваемых модификаторов, укажите <see langword="null" /> вместо массива массивов.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий метод является универсальным, но не является универсальным определением метода.
    ///    Свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, однако свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    public void SetSignature(Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      if (this.m_tkMethod.Token != 0)
        return;
      this.CheckContext(new Type[1]{ returnType });
      this.CheckContext(returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes);
      this.CheckContext(parameterTypeRequiredCustomModifiers);
      this.CheckContext(parameterTypeOptionalCustomModifiers);
      this.ThrowIfGeneric();
      if (returnType != (Type) null)
        this.m_returnType = returnType;
      if (parameterTypes != null)
      {
        this.m_parameterTypes = new Type[parameterTypes.Length];
        Array.Copy((Array) parameterTypes, (Array) this.m_parameterTypes, parameterTypes.Length);
      }
      this.m_returnTypeRequiredCustomModifiers = returnTypeRequiredCustomModifiers;
      this.m_returnTypeOptionalCustomModifiers = returnTypeOptionalCustomModifiers;
      this.m_parameterTypeRequiredCustomModifiers = parameterTypeRequiredCustomModifiers;
      this.m_parameterTypeOptionalCustomModifiers = parameterTypeOptionalCustomModifiers;
    }

    /// <summary>
    ///   Задает атрибуты параметров и имя параметра этого метода или возвращаемого значения данного метода.
    ///    Возвращает ParameterBuilder, который можно использовать для применения настраиваемых атрибутов.
    /// </summary>
    /// <param name="position">
    ///   Позиция параметра в списке параметров.
    ///    Параметры индексируются, начиная с номера 1 для первого параметра; число 0 представляет возвращаемое значение метода.
    /// </param>
    /// <param name="attributes">Атрибуты параметра.</param>
    /// <param name="strParamName">
    ///   Имя параметра.
    ///    Имя может быть пустой строкой.
    /// </param>
    /// <returns>
    ///   Возвращает объект <see langword="ParameterBuilder" />, представляющий параметр этого метода или возвращаемое значение этого метода.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Метод не имеет параметров.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="position" /> меньше нуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="position" /> превышает число параметров метода.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего метода свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    [SecuritySafeCritical]
    public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string strParamName)
    {
      if (position < 0)
        throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
      this.ThrowIfGeneric();
      this.m_containingType.ThrowIfCreated();
      if (position > 0 && (this.m_parameterTypes == null || position > this.m_parameterTypes.Length))
        throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
      attributes &= ~ParameterAttributes.ReservedMask;
      return new ParameterBuilder(this, position, attributes, strParamName);
    }

    /// <summary>
    ///   Задает сведения о маршалинге для возвращаемого типа этого метода.
    /// </summary>
    /// <param name="unmanagedMarshal">
    ///   Сведения о маршалинге для возвращаемого типа этого метода.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего метода свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
    {
      this.ThrowIfGeneric();
      this.m_containingType.ThrowIfCreated();
      if (this.m_retParam == null)
        this.m_retParam = new ParameterBuilder(this, 0, ParameterAttributes.None, (string) null);
      this.m_retParam.SetMarshal(unmanagedMarshal);
    }

    /// <summary>
    ///   Задает символьный настраиваемый атрибут с помощью большого двоичного объекта.
    /// </summary>
    /// <param name="name">
    ///   Имя символьного настраиваемого атрибута.
    /// </param>
    /// <param name="data">
    ///   Большой двоичный объект байтов, представляющий значение символьного настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Модуль, содержащий этот метод, не является модулем отладки.
    /// 
    ///   -или-
    /// 
    ///   Для текущего метода свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    public void SetSymCustomAttribute(string name, byte[] data)
    {
      this.ThrowIfGeneric();
      this.m_containingType.ThrowIfCreated();
      if (this.m_module.GetSymWriter() == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
      if (this.m_symCustomAttrs == null)
        this.m_symCustomAttrs = new List<MethodBuilder.SymCustomAttr>();
      this.m_symCustomAttrs.Add(new MethodBuilder.SymCustomAttr(name, data));
    }

    /// <summary>Добавляет декларативную безопасность в этот метод.</summary>
    /// <param name="action">
    ///   Выполняемое действие для безопасности (Demand, Assert и т. д.).
    /// </param>
    /// <param name="pset">
    ///   Набор разрешений, к которому применяется действие.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="action" /> является недопустимым (<see langword="RequestMinimum" />, <see langword="RequestOptional" /> и <see langword="RequestRefuse" /> являются недопустимыми).
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Набор разрешений <paramref name="pset" /> содержит действие, добавленное ранее с помощью <see cref="M:System.Reflection.Emit.MethodBuilder.AddDeclarativeSecurity(System.Security.Permissions.SecurityAction,System.Security.PermissionSet)" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего метода свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="pset" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
    {
      if (pset == null)
        throw new ArgumentNullException(nameof (pset));
      this.ThrowIfGeneric();
      if (!Enum.IsDefined(typeof (SecurityAction), (object) action) || action == SecurityAction.RequestMinimum || (action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse))
        throw new ArgumentOutOfRangeException(nameof (action));
      this.m_containingType.ThrowIfCreated();
      byte[] blob = (byte[]) null;
      int cb = 0;
      if (!pset.IsEmpty())
      {
        blob = pset.EncodeXml();
        cb = blob.Length;
      }
      TypeBuilder.AddDeclarativeSecurity(this.m_module.GetNativeHandle(), this.MetadataTokenInternal, action, blob, cb);
    }

    /// <summary>
    ///   Создает тело метода с использованием указанного массива байтов инструкций промежуточного языка Майкрософт (MSIL).
    /// </summary>
    /// <param name="il">
    ///   Массив, содержащий допустимые инструкции MSIL.
    /// </param>
    /// <param name="maxStack">Максимальная глубина оценки стека.</param>
    /// <param name="localSignature">
    ///   Массив байтов, содержащий сериализованную структуру локальной переменной.
    ///    Укажите <see langword="null" />, если у метода нет локальных переменных.
    /// </param>
    /// <param name="exceptionHandlers">
    ///   Коллекция, содержащая обработчики исключений для метода.
    ///    Укажите <see langword="null" />, если у метода нет обработчиков исключений.
    /// </param>
    /// <param name="tokenFixups">
    ///   Коллекция значений, представляющих смещения в <paramref name="il" />, каждое из которых задает начало маркера, который может быть изменен.
    ///    Укажите <see langword="null" />, если у метода нет маркеров, которые должны быть изменены.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="il" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="maxStack" /> является отрицательным значением.
    /// 
    ///   -или-
    /// 
    ///   Один из <paramref name="exceptionHandlers" /> указывает смещение за пределами <paramref name="il" />.
    /// 
    ///   -или-
    /// 
    ///   Один из <paramref name="tokenFixups" /> указывает смещение, которое находится за пределами массива <paramref name="il" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан ранее с помощью метода <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Этот метод был вызван ранее с помощью этого объекта <see cref="T:System.Reflection.Emit.MethodBuilder" />.
    /// </exception>
    public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
    {
      if (il == null)
        throw new ArgumentNullException(nameof (il), Environment.GetResourceString("ArgumentNull_Array"));
      if (maxStack < 0)
        throw new ArgumentOutOfRangeException(nameof (maxStack), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this.m_bIsBaked)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
      this.m_containingType.ThrowIfCreated();
      this.ThrowIfGeneric();
      byte[] numArray1 = (byte[]) null;
      ExceptionHandler[] exceptionHandlers1 = (ExceptionHandler[]) null;
      int[] numArray2 = (int[]) null;
      byte[] numArray3 = (byte[]) il.Clone();
      if (localSignature != null)
        numArray1 = (byte[]) localSignature.Clone();
      if (exceptionHandlers != null)
      {
        exceptionHandlers1 = MethodBuilder.ToArray<ExceptionHandler>(exceptionHandlers);
        MethodBuilder.CheckExceptionHandlerRanges(exceptionHandlers1, numArray3.Length);
      }
      if (tokenFixups != null)
      {
        numArray2 = MethodBuilder.ToArray<int>(tokenFixups);
        int num = numArray3.Length - 4;
        for (int index = 0; index < numArray2.Length; ++index)
        {
          if (numArray2[index] < 0 || numArray2[index] > num)
            throw new ArgumentOutOfRangeException("tokenFixups[" + (object) index + "]", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) num));
        }
      }
      this.m_ubBody = numArray3;
      this.m_localSignature = numArray1;
      this.m_exceptions = exceptionHandlers1;
      this.m_mdMethodFixups = numArray2;
      this.m_maxStack = maxStack;
      this.m_ilGenerator = (ILGenerator) null;
      this.m_bIsBaked = true;
    }

    private static T[] ToArray<T>(IEnumerable<T> sequence)
    {
      T[] objArray = sequence as T[];
      if (objArray != null)
        return (T[]) objArray.Clone();
      return new List<T>(sequence).ToArray();
    }

    private static void CheckExceptionHandlerRanges(ExceptionHandler[] exceptionHandlers, int maxOffset)
    {
      for (int index = 0; index < exceptionHandlers.Length; ++index)
      {
        ExceptionHandler exceptionHandler = exceptionHandlers[index];
        if (exceptionHandler.m_filterOffset > maxOffset || exceptionHandler.m_tryEndOffset > maxOffset || exceptionHandler.m_handlerEndOffset > maxOffset)
          throw new ArgumentOutOfRangeException("exceptionHandlers[" + (object) index + "]", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) maxOffset));
        if (exceptionHandler.Kind == ExceptionHandlingClauseOptions.Clause && exceptionHandler.ExceptionTypeToken == 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeToken", (object) exceptionHandler.ExceptionTypeToken), "exceptionHandlers[" + (object) index + "]");
      }
    }

    /// <summary>
    ///   Создает тело метода с использованием предоставленного массива байтов инструкций промежуточного языка Майкрософт (MSIL).
    /// </summary>
    /// <param name="il">
    ///   Массив, содержащий допустимые инструкции MSIL.
    ///    Если этот параметр равен <see langword="null" />, тело метода очищается.
    /// </param>
    /// <param name="count">
    ///   Число допустимых байтов в массиве MSIL.
    ///    Это значение игнорируется, если MSIL — <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="count" /> выходит за пределы диапазона индексов предоставленного массива инструкций MSIL, и <paramref name="il" /> не равно <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Этот метод был вызван ранее применительно к этому <see langword="MethodBuilder" /> с аргументом <paramref name="il" />, не равным <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего метода свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    public void CreateMethodBody(byte[] il, int count)
    {
      this.ThrowIfGeneric();
      if (this.m_bIsBaked)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
      this.m_containingType.ThrowIfCreated();
      if (il != null && (count < 0 || count > il.Length))
        throw new ArgumentOutOfRangeException(nameof (count), Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (il == null)
      {
        this.m_ubBody = (byte[]) null;
      }
      else
      {
        this.m_ubBody = new byte[count];
        Array.Copy((Array) il, (Array) this.m_ubBody, count);
        this.m_localSignature = (byte[]) null;
        this.m_exceptions = (ExceptionHandler[]) null;
        this.m_mdMethodFixups = (int[]) null;
        this.m_maxStack = 16;
        this.m_bIsBaked = true;
      }
    }

    /// <summary>Задает флаги реализации для этого метода.</summary>
    /// <param name="attributes">Флаги реализации для установки.</param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего метода свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    [SecuritySafeCritical]
    public void SetImplementationFlags(MethodImplAttributes attributes)
    {
      this.ThrowIfGeneric();
      this.m_containingType.ThrowIfCreated();
      this.m_dwMethodImplFlags = attributes;
      this.m_canBeRuntimeImpl = true;
      TypeBuilder.SetMethodImpl(this.m_module.GetNativeHandle(), this.MetadataTokenInternal, attributes);
    }

    /// <summary>
    ///   Возвращает <see langword="ILGenerator" /> для этого метода с потоком промежуточного языка Майкрософт (MSIL), имеющим размер по умолчанию (64 байта).
    /// </summary>
    /// <returns>
    ///   Возвращает объект <see langword="ILGenerator" /> для этого метода.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод не должен иметь тело из-за своих флагов <see cref="T:System.Reflection.MethodAttributes" /> или <see cref="T:System.Reflection.MethodImplAttributes" />, например, потому что имеет флаг <see cref="F:System.Reflection.MethodAttributes.PinvokeImpl" />.
    /// 
    ///   -или-
    /// 
    ///   Метод является универсальным методом, но не определением универсального метода.
    ///    То есть свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, однако свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    public ILGenerator GetILGenerator()
    {
      this.ThrowIfGeneric();
      this.ThrowIfShouldNotHaveBody();
      if (this.m_ilGenerator == null)
        this.m_ilGenerator = new ILGenerator((MethodInfo) this);
      return this.m_ilGenerator;
    }

    /// <summary>
    ///   Возвращает <see langword="ILGenerator" /> для этого метода с указанным размером потока MSIL.
    /// </summary>
    /// <param name="size">Размер потока MSIL (в байтах).</param>
    /// <returns>
    ///   Возвращает объект <see langword="ILGenerator" /> для этого метода.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод не должен иметь тело из-за своих флагов <see cref="T:System.Reflection.MethodAttributes" /> или <see cref="T:System.Reflection.MethodImplAttributes" />, например, потому что имеет флаг <see cref="F:System.Reflection.MethodAttributes.PinvokeImpl" />.
    /// 
    ///   -или-
    /// 
    ///   Метод является универсальным методом, но не определением универсального метода.
    ///    То есть свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, однако свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    public ILGenerator GetILGenerator(int size)
    {
      this.ThrowIfGeneric();
      this.ThrowIfShouldNotHaveBody();
      if (this.m_ilGenerator == null)
        this.m_ilGenerator = new ILGenerator((MethodInfo) this, size);
      return this.m_ilGenerator;
    }

    private void ThrowIfShouldNotHaveBody()
    {
      if ((this.m_dwMethodImplFlags & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.m_dwMethodImplFlags & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL || ((this.m_iAttributes & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope || this.m_isDllImport))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ShouldNotHaveMethodBody"));
    }

    /// <summary>
    ///   Возвращает или задает логическое значение, указывающее, инициализируются ли локальные переменные в этом методе нулевым значением.
    ///    По умолчанию этому свойству присваивается значение <see langword="true" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если локальные переменные в этом методе должны быть инициализированы нулевым значением. В противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для текущего метода свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    ///    (Получить или установить.)
    /// </exception>
    public bool InitLocals
    {
      get
      {
        this.ThrowIfGeneric();
        return this.m_fInitLocals;
      }
      set
      {
        this.ThrowIfGeneric();
        this.m_fInitLocals = value;
      }
    }

    /// <summary>Возвращает ссылку на модуль, содержащий этот метод.</summary>
    /// <returns>Возвращает ссылку на модуль, содержащий этот метод.</returns>
    public Module GetModule()
    {
      return (Module) this.GetModuleBuilder();
    }

    /// <summary>Извлекает сигнатуру метода.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Строка, содержащая сигнатуру метода, отражаемую этим экземпляром <see langword="MethodBase" />.
    /// </returns>
    public string Signature
    {
      [SecuritySafeCritical] get
      {
        return this.GetMethodSignature().ToString();
      }
    }

    /// <summary>
    ///   Задает настраиваемый атрибут с помощью большого двоичного объекта пользовательских атрибутов.
    /// </summary>
    /// <param name="con">Конструктор настраиваемого атрибута.</param>
    /// <param name="binaryAttribute">
    ///   Большой двоичный объект байтов, представляющий атрибуты.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="con" /> или <paramref name="binaryAttribute" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для текущего метода свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (con));
      if (binaryAttribute == null)
        throw new ArgumentNullException(nameof (binaryAttribute));
      this.ThrowIfGeneric();
      TypeBuilder.DefineCustomAttribute(this.m_module, this.MetadataTokenInternal, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
      if (!this.IsKnownCA(con))
        return;
      this.ParseCA(con, binaryAttribute);
    }

    /// <summary>
    ///   Задает настраиваемый атрибут с помощью построителя настраиваемых атрибутов.
    /// </summary>
    /// <param name="customBuilder">
    ///   Экземпляр вспомогательного класса для описания настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="customBuilder" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для текущего метода свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException(nameof (customBuilder));
      this.ThrowIfGeneric();
      customBuilder.CreateCustomAttribute(this.m_module, this.MetadataTokenInternal);
      if (!this.IsKnownCA(customBuilder.m_con))
        return;
      this.ParseCA(customBuilder.m_con, customBuilder.m_blob);
    }

    private bool IsKnownCA(ConstructorInfo con)
    {
      Type declaringType = con.DeclaringType;
      return declaringType == typeof (MethodImplAttribute) || declaringType == typeof (DllImportAttribute);
    }

    private void ParseCA(ConstructorInfo con, byte[] blob)
    {
      Type declaringType = con.DeclaringType;
      if (declaringType == typeof (MethodImplAttribute))
      {
        this.m_canBeRuntimeImpl = true;
      }
      else
      {
        if (!(declaringType == typeof (DllImportAttribute)))
          return;
        this.m_canBeRuntimeImpl = true;
        this.m_isDllImport = true;
      }
    }

    void _MethodBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _MethodBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    private struct SymCustomAttr
    {
      public string m_name;
      public byte[] m_data;

      public SymCustomAttr(string name, byte[] data)
      {
        this.m_name = name;
        this.m_data = data;
      }
    }
  }
}
