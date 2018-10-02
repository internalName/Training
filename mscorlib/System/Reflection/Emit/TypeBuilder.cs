// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.TypeBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Определяет и создает новые экземпляры классов во время выполнения.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_TypeBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class TypeBuilder : TypeInfo, _TypeBuilder
  {
    /// <summary>Представляет, что общий размер для типа не указан.</summary>
    public const int UnspecifiedTypeSize = 0;
    private List<TypeBuilder.CustAttr> m_ca;
    private TypeToken m_tdType;
    private ModuleBuilder m_module;
    private string m_strName;
    private string m_strNameSpace;
    private string m_strFullQualName;
    private Type m_typeParent;
    private List<Type> m_typeInterfaces;
    private TypeAttributes m_iAttr;
    private GenericParameterAttributes m_genParamAttributes;
    internal List<MethodBuilder> m_listMethods;
    internal int m_lastTokenizedMethod;
    private int m_constructorCount;
    private int m_iTypeSize;
    private PackingSize m_iPackingSize;
    private TypeBuilder m_DeclaringType;
    private Type m_enumUnderlyingType;
    internal bool m_isHiddenGlobalType;
    private bool m_hasBeenCreated;
    private RuntimeType m_bakedRuntimeType;
    private int m_genParamPos;
    private GenericTypeParameterBuilder[] m_inst;
    private bool m_bIsGenParam;
    private MethodBuilder m_declMeth;
    private TypeBuilder m_genTypeDef;

    /// <summary>
    ///   Получает значение, указывающее, может ли заданный объект <see cref="T:System.Reflection.TypeInfo" /> быть назначен этому объекту.
    /// </summary>
    /// <param name="typeInfo">Объект для тестирования.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="typeInfo" /> может быть назначен этому объекту. В противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsAssignableFrom(TypeInfo typeInfo)
    {
      if ((Type) typeInfo == (Type) null)
        return false;
      return this.IsAssignableFrom(typeInfo.AsType());
    }

    /// <summary>
    ///   Возвращает метод указанного сконструированного универсального типа, соответствующего указанному методу определения универсального типа.
    /// </summary>
    /// <param name="type">
    ///   Сконструированный универсальный тип, метод которого возвращается.
    /// </param>
    /// <param name="method">
    ///   Метод определения универсального типа <paramref name="type" />, который указывает, какой метод <paramref name="type" /> следует вернуть.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, представляющий метод <paramref name="type" />, соответствующий <paramref name="method" />, который указывает метод, принадлежащий определению универсального типа <paramref name="type" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="method" /> представляет универсальный метод, который не является определением универсального метода.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> не представляет универсальный тип.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="type" /> не является параметром типа <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// 
    ///   -или-
    /// 
    ///   Объявляющий тип <paramref name="method" /> не является определением универсального типа.
    /// 
    ///   -или-
    /// 
    ///   Объявляющий тип <paramref name="method" /> не является определением универсального типа <paramref name="type" />.
    /// </exception>
    public static MethodInfo GetMethod(Type type, MethodInfo method)
    {
      if (!(type is TypeBuilder) && !(type is TypeBuilderInstantiation))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeTypeBuilder"));
      if (method.IsGenericMethod && !method.IsGenericMethodDefinition)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedGenericMethodDefinition"), nameof (method));
      if (method.DeclaringType == (Type) null || !method.DeclaringType.IsGenericTypeDefinition)
        throw new ArgumentException(Environment.GetResourceString("Argument_MethodNeedGenericDeclaringType"), nameof (method));
      if (type.GetGenericTypeDefinition() != method.DeclaringType)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidMethodDeclaringType"), nameof (type));
      if (type.IsGenericTypeDefinition)
        type = type.MakeGenericType(type.GetGenericArguments());
      if (!(type is TypeBuilderInstantiation))
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), nameof (type));
      return MethodOnTypeBuilderInstantiation.GetMethod(method, type as TypeBuilderInstantiation);
    }

    /// <summary>
    ///   Возвращает конструктор указанного сконструированного универсального типа, соответствующего указанному конструктору определения универсального типа.
    /// </summary>
    /// <param name="type">
    ///   Сконструированный универсальный тип, конструктор которого возвращается.
    /// </param>
    /// <param name="constructor">
    ///   Конструктор в определении универсального типа <paramref name="type" />, который указывает, какой конструктор <paramref name="type" /> следует вернуть.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.ConstructorInfo" />, представляющий конструктор <paramref name="type" />, соответствующий <paramref name="constructor" />, который указывает конструктор, принадлежащий определению универсального типа <paramref name="type" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не представляет универсальный тип.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="type" /> не является параметром типа <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// 
    ///   -или-
    /// 
    ///   Объявляющий тип <paramref name="constructor" /> не является определением универсального типа.
    /// 
    ///   -или-
    /// 
    ///   Объявляющий тип <paramref name="constructor" /> не является определением универсального типа <paramref name="type" />.
    /// </exception>
    public static ConstructorInfo GetConstructor(Type type, ConstructorInfo constructor)
    {
      if (!(type is TypeBuilder) && !(type is TypeBuilderInstantiation))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeTypeBuilder"));
      if (!constructor.DeclaringType.IsGenericTypeDefinition)
        throw new ArgumentException(Environment.GetResourceString("Argument_ConstructorNeedGenericDeclaringType"), nameof (constructor));
      if (!(type is TypeBuilderInstantiation))
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), nameof (type));
      if (type is TypeBuilder && type.IsGenericTypeDefinition)
        type = type.MakeGenericType(type.GetGenericArguments());
      if (type.GetGenericTypeDefinition() != constructor.DeclaringType)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidConstructorDeclaringType"), nameof (type));
      return ConstructorOnTypeBuilderInstantiation.GetConstructor(constructor, type as TypeBuilderInstantiation);
    }

    /// <summary>
    ///   Возвращает поле указанного сконструированного универсального типа, соответствующее указанному полю определения универсального типа.
    /// </summary>
    /// <param name="type">
    ///   Сконструированный универсальный тип, поле которого возвращается.
    /// </param>
    /// <param name="field">
    ///   Поле определения универсального типа <paramref name="type" />, которое указывает, какое поле <paramref name="type" /> следует вернуть.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.FieldInfo" />, представляющий поле <paramref name="type" />, соответствующее <paramref name="field" />, который указывает поле, принадлежащее определению универсального типа <paramref name="type" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не представляет универсальный тип.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="type" /> не является параметром типа <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// 
    ///   -или-
    /// 
    ///   Объявляющий тип <paramref name="field" /> не является определением универсального типа.
    /// 
    ///   -или-
    /// 
    ///   Объявляющий тип <paramref name="field" /> не является определением универсального типа <paramref name="type" />.
    /// </exception>
    public static FieldInfo GetField(Type type, FieldInfo field)
    {
      if (!(type is TypeBuilder) && !(type is TypeBuilderInstantiation))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeTypeBuilder"));
      if (!field.DeclaringType.IsGenericTypeDefinition)
        throw new ArgumentException(Environment.GetResourceString("Argument_FieldNeedGenericDeclaringType"), nameof (field));
      if (!(type is TypeBuilderInstantiation))
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), nameof (type));
      if (type is TypeBuilder && type.IsGenericTypeDefinition)
        type = type.MakeGenericType(type.GetGenericArguments());
      if (type.GetGenericTypeDefinition() != field.DeclaringType)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFieldDeclaringType"), nameof (type));
      return FieldOnTypeBuilderInstantiation.GetField(field, type as TypeBuilderInstantiation);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetParentType(RuntimeModule module, int tdTypeDef, int tkParent);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddInterfaceImpl(RuntimeModule module, int tdTypeDef, int tkInterface);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int DefineMethod(RuntimeModule module, int tkParent, string name, byte[] signature, int sigLength, MethodAttributes attributes);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int DefineMethodSpec(RuntimeModule module, int tkParent, byte[] signature, int sigLength);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int DefineField(RuntimeModule module, int tkParent, string name, byte[] signature, int sigLength, FieldAttributes attributes);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetMethodIL(RuntimeModule module, int tk, bool isInitLocals, byte[] body, int bodyLength, byte[] LocalSig, int sigLength, int maxStackSize, ExceptionHandler[] exceptions, int numExceptions, int[] tokenFixups, int numTokenFixups);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DefineCustomAttribute(RuntimeModule module, int tkAssociate, int tkConstructor, byte[] attr, int attrLength, bool toDisk, bool updateCompilerFlags);

    [SecurityCritical]
    internal static void DefineCustomAttribute(ModuleBuilder module, int tkAssociate, int tkConstructor, byte[] attr, bool toDisk, bool updateCompilerFlags)
    {
      byte[] attr1 = (byte[]) null;
      if (attr != null)
      {
        attr1 = new byte[attr.Length];
        Array.Copy((Array) attr, (Array) attr1, attr.Length);
      }
      TypeBuilder.DefineCustomAttribute(module.GetNativeHandle(), tkAssociate, tkConstructor, attr1, attr1 != null ? attr1.Length : 0, toDisk, updateCompilerFlags);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetPInvokeData(RuntimeModule module, string DllName, string name, int token, int linkFlags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int DefineProperty(RuntimeModule module, int tkParent, string name, PropertyAttributes attributes, byte[] signature, int sigLength);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int DefineEvent(RuntimeModule module, int tkParent, string name, EventAttributes attributes, int tkEventType);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void DefineMethodSemantics(RuntimeModule module, int tkAssociation, MethodSemanticsAttributes semantics, int tkMethod);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void DefineMethodImpl(RuntimeModule module, int tkType, int tkBody, int tkDecl);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetMethodImpl(RuntimeModule module, int tkMethod, MethodImplAttributes MethodImplAttributes);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int SetParamInfo(RuntimeModule module, int tkMethod, int iSequence, ParameterAttributes iParamAttributes, string strParamName);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int GetTokenFromSig(RuntimeModule module, byte[] signature, int sigLength);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetFieldLayoutOffset(RuntimeModule module, int fdToken, int iOffset);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetClassLayout(RuntimeModule module, int tk, PackingSize iPackingSize, int iTypeSize);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetFieldMarshal(RuntimeModule module, int tk, byte[] ubMarshal, int ubSize);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe void SetConstantValue(RuntimeModule module, int tk, int corType, void* pValue);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void AddDeclarativeSecurity(RuntimeModule module, int parent, SecurityAction action, byte[] blob, int cb);

    private static bool IsPublicComType(Type type)
    {
      Type declaringType = type.DeclaringType;
      if (declaringType != (Type) null)
      {
        if (TypeBuilder.IsPublicComType(declaringType) && (type.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic)
          return true;
      }
      else if ((type.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.Public)
        return true;
      return false;
    }

    internal static bool IsTypeEqual(Type t1, Type t2)
    {
      if (t1 == t2)
        return true;
      TypeBuilder typeBuilder1 = (TypeBuilder) null;
      TypeBuilder typeBuilder2 = (TypeBuilder) null;
      Type type1;
      if (t1 is TypeBuilder)
      {
        typeBuilder1 = (TypeBuilder) t1;
        type1 = (Type) typeBuilder1.m_bakedRuntimeType;
      }
      else
        type1 = t1;
      Type type2;
      if (t2 is TypeBuilder)
      {
        typeBuilder2 = (TypeBuilder) t2;
        type2 = (Type) typeBuilder2.m_bakedRuntimeType;
      }
      else
        type2 = t2;
      return (Type) typeBuilder1 != (Type) null && (Type) typeBuilder2 != (Type) null && typeBuilder1 == typeBuilder2 || type1 != (Type) null && type2 != (Type) null && type1 == type2;
    }

    [SecurityCritical]
    internal static unsafe void SetConstantValue(ModuleBuilder module, int tk, Type destType, object value)
    {
      if (value != null)
      {
        Type c = value.GetType();
        if (destType.IsByRef)
          destType = destType.GetElementType();
        if (destType.IsEnum)
        {
          EnumBuilder enumBuilder;
          Type type;
          if ((Type) (enumBuilder = destType as EnumBuilder) != (Type) null)
          {
            type = enumBuilder.GetEnumUnderlyingType();
            if (c != (Type) enumBuilder.m_typeBuilder.m_bakedRuntimeType && c != type)
              throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
          }
          else
          {
            TypeBuilder typeBuilder;
            if ((Type) (typeBuilder = destType as TypeBuilder) != (Type) null)
            {
              type = typeBuilder.m_enumUnderlyingType;
              if (type == (Type) null || c != typeBuilder.UnderlyingSystemType && c != type)
                throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
            }
            else
            {
              type = Enum.GetUnderlyingType(destType);
              if (c != destType && c != type)
                throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
            }
          }
          c = type;
        }
        else if (!destType.IsAssignableFrom(c))
          throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
        CorElementType corElementType = RuntimeTypeHandle.GetCorElementType((RuntimeType) c);
        switch (corElementType)
        {
          case CorElementType.Boolean:
          case CorElementType.Char:
          case CorElementType.I1:
          case CorElementType.U1:
          case CorElementType.I2:
          case CorElementType.U2:
          case CorElementType.I4:
          case CorElementType.U4:
          case CorElementType.I8:
          case CorElementType.U8:
          case CorElementType.R4:
          case CorElementType.R8:
            fixed (byte* numPtr = &JitHelpers.GetPinningHelper(value).m_data)
              TypeBuilder.SetConstantValue(module.GetNativeHandle(), tk, (int) corElementType, (void*) numPtr);
            break;
          default:
            if (c == typeof (string))
            {
              string str = (string) value;
              char* chPtr = (char*) str;
              if ((IntPtr) chPtr != IntPtr.Zero)
                chPtr += RuntimeHelpers.OffsetToStringData;
              TypeBuilder.SetConstantValue(module.GetNativeHandle(), tk, 14, (void*) chPtr);
              str = (string) null;
              break;
            }
            if (c == typeof (DateTime))
            {
              long ticks = ((DateTime) value).Ticks;
              TypeBuilder.SetConstantValue(module.GetNativeHandle(), tk, 10, (void*) &ticks);
              break;
            }
            throw new ArgumentException(Environment.GetResourceString("Argument_ConstantNotSupported", (object) c.ToString()));
        }
      }
      else
      {
        if (destType.IsValueType && (!destType.IsGenericType || !(destType.GetGenericTypeDefinition() == typeof (Nullable<>))))
          throw new ArgumentException(Environment.GetResourceString("Argument_ConstantNull"));
        TypeBuilder.SetConstantValue(module.GetNativeHandle(), tk, 18, (void*) null);
      }
    }

    internal TypeBuilder(ModuleBuilder module)
    {
      this.m_tdType = new TypeToken(33554432);
      this.m_isHiddenGlobalType = true;
      this.m_module = module;
      this.m_listMethods = new List<MethodBuilder>();
      this.m_lastTokenizedMethod = -1;
    }

    internal TypeBuilder(string szName, int genParamPos, MethodBuilder declMeth)
    {
      this.m_declMeth = declMeth;
      this.m_DeclaringType = this.m_declMeth.GetTypeBuilder();
      this.m_module = declMeth.GetModuleBuilder();
      this.InitAsGenericParam(szName, genParamPos);
    }

    private TypeBuilder(string szName, int genParamPos, TypeBuilder declType)
    {
      this.m_DeclaringType = declType;
      this.m_module = declType.GetModuleBuilder();
      this.InitAsGenericParam(szName, genParamPos);
    }

    private void InitAsGenericParam(string szName, int genParamPos)
    {
      this.m_strName = szName;
      this.m_genParamPos = genParamPos;
      this.m_bIsGenParam = true;
      this.m_typeInterfaces = new List<Type>();
    }

    [SecurityCritical]
    internal TypeBuilder(string name, TypeAttributes attr, Type parent, Type[] interfaces, ModuleBuilder module, PackingSize iPackingSize, int iTypeSize, TypeBuilder enclosingType)
    {
      this.Init(name, attr, parent, interfaces, module, iPackingSize, iTypeSize, enclosingType);
    }

    [SecurityCritical]
    private void Init(string fullname, TypeAttributes attr, Type parent, Type[] interfaces, ModuleBuilder module, PackingSize iPackingSize, int iTypeSize, TypeBuilder enclosingType)
    {
      if (fullname == null)
        throw new ArgumentNullException(nameof (fullname));
      if (fullname.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (fullname));
      if (fullname[0] == char.MinValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), nameof (fullname));
      if (fullname.Length > 1023)
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeNameTooLong"), nameof (fullname));
      this.m_module = module;
      this.m_DeclaringType = enclosingType;
      AssemblyBuilder containingAssemblyBuilder = this.m_module.ContainingAssemblyBuilder;
      containingAssemblyBuilder.m_assemblyData.CheckTypeNameConflict(fullname, enclosingType);
      if ((Type) enclosingType != (Type) null && ((attr & TypeAttributes.VisibilityMask) == TypeAttributes.Public || (attr & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic))
        throw new ArgumentException(Environment.GetResourceString("Argument_BadNestedTypeFlags"), nameof (attr));
      int[] interfaceTokens = (int[]) null;
      if (interfaces != null)
      {
        for (int index = 0; index < interfaces.Length; ++index)
        {
          if (interfaces[index] == (Type) null)
            throw new ArgumentNullException(nameof (interfaces));
        }
        interfaceTokens = new int[interfaces.Length + 1];
        for (int index = 0; index < interfaces.Length; ++index)
          interfaceTokens[index] = this.m_module.GetTypeTokenInternal(interfaces[index]).Token;
      }
      int length = fullname.LastIndexOf('.');
      switch (length)
      {
        case -1:
        case 0:
          this.m_strNameSpace = string.Empty;
          this.m_strName = fullname;
          break;
        default:
          this.m_strNameSpace = fullname.Substring(0, length);
          this.m_strName = fullname.Substring(length + 1);
          break;
      }
      this.VerifyTypeAttributes(attr);
      this.m_iAttr = attr;
      this.SetParent(parent);
      this.m_listMethods = new List<MethodBuilder>();
      this.m_lastTokenizedMethod = -1;
      this.SetInterfaces(interfaces);
      int tkParent = 0;
      if (this.m_typeParent != (Type) null)
        tkParent = this.m_module.GetTypeTokenInternal(this.m_typeParent).Token;
      int tkEnclosingType = 0;
      if ((Type) enclosingType != (Type) null)
        tkEnclosingType = enclosingType.m_tdType.Token;
      this.m_tdType = new TypeToken(TypeBuilder.DefineType(this.m_module.GetNativeHandle(), fullname, tkParent, this.m_iAttr, tkEnclosingType, interfaceTokens));
      this.m_iPackingSize = iPackingSize;
      this.m_iTypeSize = iTypeSize;
      if (this.m_iPackingSize != PackingSize.Unspecified || this.m_iTypeSize != 0)
        TypeBuilder.SetClassLayout(this.GetModuleBuilder().GetNativeHandle(), this.m_tdType.Token, this.m_iPackingSize, this.m_iTypeSize);
      if (TypeBuilder.IsPublicComType((Type) this))
      {
        if (containingAssemblyBuilder.IsPersistable() && !this.m_module.IsTransient())
          containingAssemblyBuilder.m_assemblyData.AddPublicComType((Type) this);
        if (!this.m_module.Equals((object) containingAssemblyBuilder.ManifestModule))
          containingAssemblyBuilder.DefineExportedTypeInMemory((Type) this, this.m_module.m_moduleData.FileToken, this.m_tdType.Token);
      }
      this.m_module.AddType(this.FullName, (Type) this);
    }

    [SecurityCritical]
    private MethodBuilder DefinePInvokeMethodHelper(string name, string dllName, string importName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      this.CheckContext(new Type[1]{ returnType });
      this.CheckContext(returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes);
      this.CheckContext(parameterTypeRequiredCustomModifiers);
      this.CheckContext(parameterTypeOptionalCustomModifiers);
      AppDomain.CheckDefinePInvokeSupported();
      lock (this.SyncRoot)
        return this.DefinePInvokeMethodHelperNoLock(name, dllName, importName, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, nativeCallConv, nativeCharSet);
    }

    [SecurityCritical]
    private MethodBuilder DefinePInvokeMethodHelperNoLock(string name, string dllName, string importName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      if (dllName == null)
        throw new ArgumentNullException(nameof (dllName));
      if (dllName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (dllName));
      if (importName == null)
        throw new ArgumentNullException(nameof (importName));
      if (importName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (importName));
      if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadPInvokeMethod"));
      if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadPInvokeOnInterface"));
      this.ThrowIfCreated();
      attributes |= MethodAttributes.PinvokeImpl;
      MethodBuilder methodBuilder = new MethodBuilder(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, this.m_module, this, false);
      int length;
      methodBuilder.GetMethodSignature().InternalGetSignature(out length);
      if (this.m_listMethods.Contains(methodBuilder))
        throw new ArgumentException(Environment.GetResourceString("Argument_MethodRedefined"));
      this.m_listMethods.Add(methodBuilder);
      MethodToken token = methodBuilder.GetToken();
      int linkFlags = 0;
      switch (nativeCallConv)
      {
        case CallingConvention.Winapi:
          linkFlags = 256;
          break;
        case CallingConvention.Cdecl:
          linkFlags = 512;
          break;
        case CallingConvention.StdCall:
          linkFlags = 768;
          break;
        case CallingConvention.ThisCall:
          linkFlags = 1024;
          break;
        case CallingConvention.FastCall:
          linkFlags = 1280;
          break;
      }
      switch (nativeCharSet)
      {
        case CharSet.None:
          linkFlags |= 0;
          break;
        case CharSet.Ansi:
          linkFlags |= 2;
          break;
        case CharSet.Unicode:
          linkFlags |= 4;
          break;
        case CharSet.Auto:
          linkFlags |= 6;
          break;
      }
      TypeBuilder.SetPInvokeData(this.m_module.GetNativeHandle(), dllName, importName, token.Token, linkFlags);
      methodBuilder.SetToken(token);
      return methodBuilder;
    }

    [SecurityCritical]
    private FieldBuilder DefineDataHelper(string name, byte[] data, int size, FieldAttributes attributes)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      if (size <= 0 || size >= 4128768)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadSizeForData"));
      this.ThrowIfCreated();
      string str = "$ArrayType$" + (object) size;
      TypeBuilder typeBuilder = this.m_module.FindTypeBuilderWithName(str, false) as TypeBuilder;
      if ((Type) typeBuilder == (Type) null)
      {
        TypeAttributes attr = TypeAttributes.Public | TypeAttributes.ExplicitLayout | TypeAttributes.Sealed;
        typeBuilder = this.m_module.DefineType(str, attr, typeof (ValueType), PackingSize.Size1, size);
        typeBuilder.CreateType();
      }
      FieldBuilder fieldBuilder = this.DefineField(name, (Type) typeBuilder, attributes | FieldAttributes.Static);
      fieldBuilder.SetData(data, size);
      return fieldBuilder;
    }

    private void VerifyTypeAttributes(TypeAttributes attr)
    {
      if (this.DeclaringType == (Type) null)
      {
        if ((attr & TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.VisibilityMask) != TypeAttributes.Public)
          throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeAttrNestedVisibilityOnNonNestedType"));
      }
      else if ((attr & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic || (attr & TypeAttributes.VisibilityMask) == TypeAttributes.Public)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeAttrNonNestedVisibilityNestedType"));
      if ((attr & TypeAttributes.LayoutMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.LayoutMask) != TypeAttributes.SequentialLayout && (attr & TypeAttributes.LayoutMask) != TypeAttributes.ExplicitLayout)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeAttrInvalidLayout"));
      if ((attr & TypeAttributes.ReservedMask) != TypeAttributes.NotPublic)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeAttrReservedBitsSet"));
    }

    /// <summary>
    ///   Возвращает значение, которое показывает, был ли создан текущий динамический тип.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если был вызван метод <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsCreated()
    {
      return this.m_hasBeenCreated;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int DefineType(RuntimeModule module, string fullname, int tkParent, TypeAttributes attributes, int tkEnclosingType, int[] interfaceTokens);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int DefineGenericParam(RuntimeModule module, string name, int tkParent, GenericParameterAttributes attributes, int position, int[] constraints);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void TermCreateClass(RuntimeModule module, int tk, ObjectHandleOnStack type);

    internal void ThrowIfCreated()
    {
      if (this.IsCreated())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
    }

    internal object SyncRoot
    {
      get
      {
        return this.m_module.SyncRoot;
      }
    }

    internal ModuleBuilder GetModuleBuilder()
    {
      return this.m_module;
    }

    internal RuntimeType BakedRuntimeType
    {
      get
      {
        return this.m_bakedRuntimeType;
      }
    }

    internal void SetGenParamAttributes(GenericParameterAttributes genericParameterAttributes)
    {
      this.m_genParamAttributes = genericParameterAttributes;
    }

    internal void SetGenParamCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      TypeBuilder.CustAttr ca = new TypeBuilder.CustAttr(con, binaryAttribute);
      lock (this.SyncRoot)
        this.SetGenParamCustomAttributeNoLock(ca);
    }

    internal void SetGenParamCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      TypeBuilder.CustAttr ca = new TypeBuilder.CustAttr(customBuilder);
      lock (this.SyncRoot)
        this.SetGenParamCustomAttributeNoLock(ca);
    }

    private void SetGenParamCustomAttributeNoLock(TypeBuilder.CustAttr ca)
    {
      if (this.m_ca == null)
        this.m_ca = new List<TypeBuilder.CustAttr>();
      this.m_ca.Add(ca);
    }

    /// <summary>Возвращает имя типа, исключая пространство имен.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Имя типа, исключая пространство имен.
    /// </returns>
    public override string ToString()
    {
      return TypeNameBuilder.ToString((Type) this, TypeNameBuilder.Format.ToString);
    }

    /// <summary>Возвращает тип, объявивший этот тип.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Тип, объявивший этот тип.
    /// </returns>
    public override Type DeclaringType
    {
      get
      {
        return (Type) this.m_DeclaringType;
      }
    }

    /// <summary>
    ///   Возвращает тип, который был использован для получения этого типа.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Тип, который был использован для получения этого типа.
    /// </returns>
    public override Type ReflectedType
    {
      get
      {
        return (Type) this.m_DeclaringType;
      }
    }

    /// <summary>Извлекает имя данного типа.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Извлекает имя <see cref="T:System.String" /> данного типа.
    /// </returns>
    public override string Name
    {
      get
      {
        return this.m_strName;
      }
    }

    /// <summary>
    ///   Извлекает динамический модуль, который содержит определение данного типа.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Извлекает динамический модуль, который содержит определение данного типа.
    /// </returns>
    public override Module Module
    {
      get
      {
        return (Module) this.GetModuleBuilder();
      }
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_tdType.Token;
      }
    }

    /// <summary>Получает идентификатор GUID этого типа.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Получает идентификатор GUID этого типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается для неполных типов.
    /// </exception>
    public override Guid GUID
    {
      get
      {
        if (!this.IsCreated())
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
        return this.m_bakedRuntimeType.GUID;
      }
    }

    /// <summary>
    ///   Вызывает указанный член.
    ///    Вызываемый метод должен быть доступен и обеспечивать наиболее точное соответствие заданному списку аргументов с учетом ограничений заданного модуля привязки и атрибутов вызова.
    /// </summary>
    /// <param name="name">
    ///   Имя вызываемого члена.
    ///    Это может быть конструктор, метод, свойство или поле.
    ///    Необходимо указать подходящий атрибут вызова.
    ///    Обратите внимание, что можно вызвать член класса, заданный по умолчанию, передав в качестве имени члена пустую строку.
    /// </param>
    /// <param name="invokeAttr">
    ///   Атрибут вызова.
    ///    Это должен быть одноразрядный флаг из <see langword="BindingFlags" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see langword="MemberInfo" /> с помощью отражения.
    ///    Если указан модуль привязки <see langword="null" />, используется модуль привязки по умолчанию.
    ///    См. раздел <see cref="T:System.Reflection.Binder" />.
    /// </param>
    /// <param name="target">
    ///   Объект, для которого следует вызвать указанный член.
    ///    Если член является статическим, этот параметр игнорируется.
    /// </param>
    /// <param name="args">
    ///   Список аргументов.
    ///    Это массив объектов, содержащий число, порядок и тип параметров вызываемого члена.
    ///    Если параметров нет, должно быть указано значение NULL.
    /// </param>
    /// <param name="modifiers">
    ///   Массив с такой же длиной, как у <paramref name="args" /> с элементами, представляющими атрибуты, связанные с аргументами вызываемого члена.
    ///    Параметр имеет атрибуты, связанные с ним в метаданных.
    ///    Они используются различными службами взаимодействия.
    ///    Дополнительные сведения см. в спецификации метаданных.
    /// </param>
    /// <param name="culture">
    ///   Экземпляр объекта <see langword="CultureInfo" />, используемого для управления приведением типов.
    ///    Если параметр имеет значение NULL, для текущего потока используется <see langword="CultureInfo" />.
    ///    (Обратите внимание, что необходимо, например, преобразовать строку, представляющую 1000, в число с двойной точностью, поскольку для разных языков и региональных параметров 1000 представляется по-разному.)
    /// </param>
    /// <param name="namedParameters">
    ///   Каждый параметр в массиве <paramref name="namedParameters" /> получает значение в соответствующем элементе в массиве <paramref name="args" />.
    ///    Если длина <paramref name="args" /> превышает длину <paramref name="namedParameters" />, оставшиеся значения аргументов передаются по порядку.
    /// </param>
    /// <returns>Возвращает возвращаемое значение вызываемого члена.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается для неполных типов.
    /// </exception>
    public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
    }

    /// <summary>
    ///   Извлекает динамическую сборку, которая содержит определение данного типа.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Извлекает динамическую сборку, которая содержит определение данного типа.
    /// </returns>
    public override Assembly Assembly
    {
      get
      {
        return this.m_module.Assembly;
      }
    }

    /// <summary>Не поддерживается в динамических модулях.</summary>
    /// <returns>Только для чтения.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Не поддерживается в динамических модулях.
    /// </exception>
    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    /// <summary>Извлекает полный путь данного типа.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Извлекает полный путь данного типа.
    /// </returns>
    public override string FullName
    {
      get
      {
        if (this.m_strFullQualName == null)
          this.m_strFullQualName = TypeNameBuilder.ToString((Type) this, TypeNameBuilder.Format.FullName);
        return this.m_strFullQualName;
      }
    }

    /// <summary>
    ///   Получает пространство имен, в котором определен этот объект <see langword="TypeBuilder" />.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Получает пространство имен, в котором определен этот объект <see langword="TypeBuilder" />.
    /// </returns>
    public override string Namespace
    {
      get
      {
        return this.m_strNameSpace;
      }
    }

    /// <summary>
    ///   Возвращает полное имя этого типа, дополненное отображаемым именем сборки.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Полное имя этого типа, дополненное отображаемым именем сборки.
    /// </returns>
    public override string AssemblyQualifiedName
    {
      get
      {
        return TypeNameBuilder.ToString((Type) this, TypeNameBuilder.Format.AssemblyQualifiedName);
      }
    }

    /// <summary>Возвращает базовый тип этого типа.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Возвращает базовый тип этого типа.
    /// </returns>
    public override Type BaseType
    {
      get
      {
        return this.m_typeParent;
      }
    }

    protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.ConstructorInfo" />, представляющих открытые и не открытые конструкторы, определенные для этого класса, как указано.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />, как в <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.ConstructorInfo" />, представляющих указанные конструкторы, определенные для этого класса.
    ///    Если конструкторы не определены, возвращается пустой массив.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    [ComVisible(true)]
    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetConstructors(bindingAttr);
    }

    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      if (types == null)
        return this.m_bakedRuntimeType.GetMethod(name, bindingAttr);
      return this.m_bakedRuntimeType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>
    ///   Возвращает все открытые и закрытые методы, объявленные или наследуемые данным типом, как указано.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />, как в <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.MethodInfo" />, представляющих открытые и закрытые методы, определенные для данного типа, если используется <paramref name="nonPublic" />; в противном случае возвращаются только открытые методы.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetMethods(bindingAttr);
    }

    /// <summary>Возвращает поле, указанное данным именем.</summary>
    /// <param name="name">Имя получаемого поля.</param>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />, как в <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает объект <see cref="T:System.Reflection.FieldInfo" />, представляющий поле, объявленное или наследуемое этим типом, с указанным именем и открытым или закрытым модификатором.
    ///    Если совпадений нет, возвращается значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetField(name, bindingAttr);
    }

    /// <summary>
    ///   Возвращает открытые и не являющиеся открытыми поля, объявленные данным типом.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.FieldInfo" />, представляющих открытые и не являющиеся открытыми поля, объявленные или наследованные этим типом.
    ///    Если заданные поля отсутствуют, возвращается пустой массив.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    public override FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetFields(bindingAttr);
    }

    /// <summary>
    ///   Возвращает интерфейс, реализованный (прямо или косвенно) данным классом с полным именем, совпадающим с именем данного интерфейса.
    /// </summary>
    /// <param name="name">Имя интерфейса.</param>
    /// <param name="ignoreCase">
    ///   Если значение <see langword="true" />, при поиске не учитывается регистр.
    ///    Если значение <see langword="false" />, при поиске учитывается регистр.
    /// </param>
    /// <returns>
    ///   Возвращает объект <see cref="T:System.Type" />, предоставляющий реализованный интерфейс.
    ///    Возвращает значение null, если совпадающее имя интерфейса не найдено.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    public override Type GetInterface(string name, bool ignoreCase)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetInterface(name, ignoreCase);
    }

    /// <summary>
    ///   Возвращает массив всех интерфейсов, реализованных для данного типа и его базовых типов.
    /// </summary>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Type" />, представляющих реализованные интерфейсы.
    ///    Если они не определены, возвращается пустой массив.
    /// </returns>
    public override Type[] GetInterfaces()
    {
      if (this.m_bakedRuntimeType != (RuntimeType) null)
        return this.m_bakedRuntimeType.GetInterfaces();
      if (this.m_typeInterfaces == null)
        return EmptyArray<Type>.Value;
      return this.m_typeInterfaces.ToArray();
    }

    /// <summary>Возвращает событие с указанным именем.</summary>
    /// <param name="name">Имя искомого события.</param>
    /// <param name="bindingAttr">
    ///   Битовая комбинация значений <see cref="T:System.Reflection.BindingFlags" />, определяющая границы поиска.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.EventInfo" />, представляющий событие, объявленное или наследованное этим типом с указанным именем; или значение <see langword="null" />, если совпадений не обнаружено.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetEvent(name, bindingAttr);
    }

    /// <summary>
    ///   Возвращает открытые события, объявленные или наследованные данным типом.
    /// </summary>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.EventInfo" />, представляющих открытые события, объявленные или наследуемые этим типом.
    ///    Если открытые события отсутствуют, возвращается пустой массив.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    public override EventInfo[] GetEvents()
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetEvents();
    }

    protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Возвращает все открытые и закрытые свойства, объявленные или наследуемые данным типом, как указано.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Это атрибут вызова.
    ///    Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />: <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see langword="PropertyInfo" />, представляющих открытые и закрытые свойства, определенные для данного типа, если используется <paramref name="nonPublic" />; в противном случае возвращаются только открытые свойства.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetProperties(bindingAttr);
    }

    /// <summary>
    ///   Возвращает открытые и не являющиеся открытыми вложенные типы, объявленные или наследованные данным типом.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />, как в <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, представляющий все типы, вложенные внутри текущего типа <see cref="T:System.Type" /> и удовлетворяющие указанным ограничениям привязки.
    /// 
    ///   Пустой массив типа <see cref="T:System.Type" />, если внутри текущего объекта <see cref="T:System.Type" /> нет вложенных типов или ни один из вложенных типов не удовлетворяет ограничениям привязки.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    public override Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetNestedTypes(bindingAttr);
    }

    /// <summary>
    ///   Возвращает открытые и не являющиеся открытыми вложенные типы, объявленные этим типом.
    /// </summary>
    /// <param name="name">
    ///   Строка <see cref="T:System.String" />, содержащая имя искомого вложенного типа.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль для выполнения поиска открытых методов с учетом регистра.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен успешно, возвращается объект <see cref="T:System.Type" />, предоставляющий вложенный тип, который соответствует указанным требованиям; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    public override Type GetNestedType(string name, BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetNestedType(name, bindingAttr);
    }

    /// <summary>
    ///   Возвращает все открытые и закрытые члены, объявленные или наследуемые данным типом, как указано.
    /// </summary>
    /// <param name="name">Имя элемента.</param>
    /// <param name="type">Тип возвращаемого элемента.</param>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />, как в <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющих открытые и закрытые члены, определенные для данного типа, если используется <paramref name="nonPublic" />; в противном случае возвращаются только открытые члены.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetMember(name, type, bindingAttr);
    }

    /// <summary>
    ///   Возвращает сопоставление для запрошенного интерфейса.
    /// </summary>
    /// <param name="interfaceType">
    ///   Тип <see cref="T:System.Type" /> интерфейса, для которого должно быть получено сопоставление.
    /// </param>
    /// <returns>Возвращает запрошенное сопоставление интерфейса.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    [ComVisible(true)]
    public override InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetInterfaceMap(interfaceType);
    }

    /// <summary>
    ///   Возвращает открытые и не являющиеся открытыми события, объявленные данным типом.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая комбинация значений <see cref="T:System.Reflection.BindingFlags" />, определяющая границы поиска.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.EventInfo" />, представляющих события, которые объявлены или унаследованы этим типом и удовлетворяют указанным флагам привязки.
    ///    Если соответствующие события отсутствуют, возвращается пустой массив.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    public override EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetEvents(bindingAttr);
    }

    /// <summary>
    ///   Возвращает открытые и не являющиеся открытыми члены, объявленные или наследуемые данным типом.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />, например <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющих открытые и не являющиеся открытыми члены, объявленные или наследованные этим типом.
    ///    Если соответствующие члены отсутствуют, возвращается пустой массив.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован для неполных типов.
    /// </exception>
    public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetMembers(bindingAttr);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, может ли заданный <see cref="T:System.Type" /> быть назначен этому объекту.
    /// </summary>
    /// <param name="c">Объект для тестирования.</param>
    /// <returns>
    ///   <see langword="true" />, если параметр <paramref name="c" /> и текущий тип представляют один и тот же тип, или если текущий тип находится в иерархии наследования для <paramref name="c" />, или если текущий тип является интерфейсом, который поддерживает <paramref name="c" />.
    ///    Значение <see langword="false" />, если не действует ни одно из этих условий или если значение параметра <paramref name="c" /> равно <see langword="null" />.
    /// </returns>
    public override bool IsAssignableFrom(Type c)
    {
      if (TypeBuilder.IsTypeEqual(c, (Type) this))
        return true;
      TypeBuilder typeBuilder = c as TypeBuilder;
      Type c1 = !((Type) typeBuilder != (Type) null) ? c : (Type) typeBuilder.m_bakedRuntimeType;
      if (c1 != (Type) null && (object) (c1 as RuntimeType) != null)
      {
        if (this.m_bakedRuntimeType == (RuntimeType) null)
          return false;
        return this.m_bakedRuntimeType.IsAssignableFrom(c1);
      }
      if ((Type) typeBuilder == (Type) null)
        return false;
      if (typeBuilder.IsSubclassOf((Type) this))
        return true;
      if (!this.IsInterface)
        return false;
      Type[] interfaces = typeBuilder.GetInterfaces();
      for (int index = 0; index < interfaces.Length; ++index)
      {
        if (TypeBuilder.IsTypeEqual(interfaces[index], (Type) this) || interfaces[index].IsSubclassOf((Type) this))
          return true;
      }
      return false;
    }

    protected override TypeAttributes GetAttributeFlagsImpl()
    {
      return this.m_iAttr;
    }

    protected override bool IsArrayImpl()
    {
      return false;
    }

    protected override bool IsByRefImpl()
    {
      return false;
    }

    protected override bool IsPointerImpl()
    {
      return false;
    }

    protected override bool IsPrimitiveImpl()
    {
      return false;
    }

    protected override bool IsCOMObjectImpl()
    {
      return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) != TypeAttributes.NotPublic;
    }

    /// <summary>
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.NotSupportedException" />.
    /// </summary>
    /// <returns>
    ///   Этот метод не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override Type GetElementType()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    protected override bool HasElementTypeImpl()
    {
      return false;
    }

    /// <summary>
    ///   Получает значение, которое указывает, является ли текущий тип критическим с точки зрения безопасности или надежным с точки зрения безопасности и, следовательно, может ли он выполнять важные операции.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий тип является критическим с точки зрения безопасности или надежным с точки зрения безопасности; значение <see langword="false" />, если он является прозрачным.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий динамический тип не был создан путем вызова метода <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    public override bool IsSecurityCritical
    {
      get
      {
        if (!this.IsCreated())
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
        return this.m_bakedRuntimeType.IsSecurityCritical;
      }
    }

    /// <summary>
    ///   Получает значение, которое указывает, является ли текущий тип надежным с точки зрения безопасности и, следовательно, может ли он выполнять критически важные операции и предоставлять доступ прозрачному коду.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий тип является надежным с точки зрения безопасности; значение <see langword="false" />, если он является критическим с точки зрения безопасности или прозрачным.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий динамический тип не был создан путем вызова метода <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    public override bool IsSecuritySafeCritical
    {
      get
      {
        if (!this.IsCreated())
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
        return this.m_bakedRuntimeType.IsSecuritySafeCritical;
      }
    }

    /// <summary>
    ///   Получает значение, которое указывает, является ли текущий тип прозрачным и, следовательно, не может выполнять критические операции.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если тип является прозрачным; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий динамический тип не был создан путем вызова метода <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    public override bool IsSecurityTransparent
    {
      get
      {
        if (!this.IsCreated())
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
        return this.m_bakedRuntimeType.IsSecurityTransparent;
      }
    }

    /// <summary>
    ///   Определяет, является ли этот тип производным от указанного типа.
    /// </summary>
    /// <param name="c">
    ///   Проверяемый тип <see cref="T:System.Type" />.
    /// </param>
    /// <returns>
    ///   Только для чтения.
    ///    Возвращает значение <see langword="true" />, если тип совпадает с типом <paramref name="c" /> или является подтипом типа <paramref name="c" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [ComVisible(true)]
    public override bool IsSubclassOf(Type c)
    {
      Type t1 = (Type) this;
      if (TypeBuilder.IsTypeEqual(t1, c))
        return false;
      for (Type baseType = t1.BaseType; baseType != (Type) null; baseType = baseType.BaseType)
      {
        if (TypeBuilder.IsTypeEqual(baseType, c))
          return true;
      }
      return false;
    }

    /// <summary>
    ///   Возвращает базовый системный тип для данного <see langword="TypeBuilder" />.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Возвращает базовый системный тип.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип является перечислением, но базовый системный тип отсутствует.
    /// </exception>
    public override Type UnderlyingSystemType
    {
      get
      {
        if (this.m_bakedRuntimeType != (RuntimeType) null)
          return (Type) this.m_bakedRuntimeType;
        if (!this.IsEnum)
          return (Type) this;
        if (this.m_enumUnderlyingType == (Type) null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoUnderlyingTypeOnEnum"));
        return this.m_enumUnderlyingType;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, который представляет тип неуправляемого указателя на текущий тип.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, который представляет тип неуправляемого указателя на текущий тип.
    /// </returns>
    public override Type MakePointerType()
    {
      return SymbolType.FormCompoundType("*".ToCharArray(), (Type) this, 0);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, который представляет текущий тип при передаче в качестве параметра <see langword="ref" /> (параметра <see langword="ByRef" /> в Visual Basic).
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, который представляет текущий тип при передаче в качестве параметра <see langword="ref" /> (параметра <see langword="ByRef" /> в Visual Basic).
    /// </returns>
    public override Type MakeByRefType()
    {
      return SymbolType.FormCompoundType("&".ToCharArray(), (Type) this, 0);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, представляющий одномерный массив текущего типа с нижней границей, равной нулю.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, представляющий одномерный массив текущего типа с нижней границей, равной нулю.
    /// </returns>
    public override Type MakeArrayType()
    {
      return SymbolType.FormCompoundType("[]".ToCharArray(), (Type) this, 0);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, представляющий массив текущего типа указанной размерности.
    /// </summary>
    /// <param name="rank">Размерность массива.</param>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, который представляет одномерный массив текущего типа.
    /// </returns>
    /// <exception cref="T:System.IndexOutOfRangeException">
    ///   <paramref name="rank" /> не является допустимой размерностью массива.
    /// </exception>
    public override Type MakeArrayType(int rank)
    {
      if (rank <= 0)
        throw new IndexOutOfRangeException();
      string str = "";
      if (rank == 1)
      {
        str = "*";
      }
      else
      {
        for (int index = 1; index < rank; ++index)
          str += ",";
      }
      return SymbolType.FormCompoundType(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "[{0}]", (object) str).ToCharArray(), (Type) this, 0);
    }

    /// <summary>
    ///   Возвращает все настраиваемые атрибуты, определенные для данного типа.
    /// </summary>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск в цепочке наследования этого члена для нахождения атрибутов.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов, представляющих все настраиваемые атрибуты этого типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается для неполных типов.
    ///    Извлеките тип с помощью <see cref="M:System.Type.GetType" /> и вызовите <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> в возвращенном типе <see cref="T:System.Type" />.
    /// </exception>
    [SecuritySafeCritical]
    public override object[] GetCustomAttributes(bool inherit)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return CustomAttribute.GetCustomAttributes(this.m_bakedRuntimeType, typeof (object) as RuntimeType, inherit);
    }

    /// <summary>
    ///   Возвращает все настраиваемые атрибуты текущего типа, которые можно назначить указанному типу.
    /// </summary>
    /// <param name="attributeType">
    ///   Тип атрибута для поиска.
    ///    Возвращаются только те атрибуты, которые можно назначить этому типу.
    /// </param>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск атрибутов в цепочке наследования этого члена.
    /// </param>
    /// <returns>
    ///   Массив настраиваемых атрибутов, определенных для текущего типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается для неполных типов.
    ///    Извлеките тип с помощью <see cref="M:System.Type.GetType" /> и вызовите <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> в возвращенном типе <see cref="T:System.Type" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип должен быть типом, предоставленным базовой системой среды выполнения.
    /// </exception>
    [SecuritySafeCritical]
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      RuntimeType underlyingSystemType = attributeType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (attributeType));
      return CustomAttribute.GetCustomAttributes(this.m_bakedRuntimeType, underlyingSystemType, inherit);
    }

    /// <summary>
    ///   Определяет, применяется ли настраиваемый атрибут к текущему типу.
    /// </summary>
    /// <param name="attributeType">
    ///   Тип атрибута для поиска.
    ///    Возвращаются только те атрибуты, которые можно назначить этому типу.
    /// </param>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск атрибутов в цепочке наследования этого члена.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если для данного типа определены один или несколько экземпляров <paramref name="attributeType" /> или атрибут является производным от <paramref name="attributeType" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается для неполных типов.
    ///    Извлеките тип с помощью <see cref="M:System.Type.GetType" /> и вызовите <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> в возвращенном типе <see cref="T:System.Type" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не определен.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      RuntimeType underlyingSystemType = attributeType.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "caType");
      return CustomAttribute.IsDefined(this.m_bakedRuntimeType, underlyingSystemType, inherit);
    }

    /// <summary>
    ///   Получает значение, указывающее ковариацию и особые ограничения текущего параметра универсального типа.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание значений <see cref="T:System.Reflection.GenericParameterAttributes" />, которое описывает ковариацию и особые ограничения текущего параметра универсального типа.
    /// </returns>
    public override GenericParameterAttributes GenericParameterAttributes
    {
      get
      {
        return this.m_genParamAttributes;
      }
    }

    internal void SetInterfaces(params Type[] interfaces)
    {
      this.ThrowIfCreated();
      this.m_typeInterfaces = new List<Type>();
      if (interfaces == null)
        return;
      this.m_typeInterfaces.AddRange((IEnumerable<Type>) interfaces);
    }

    /// <summary>
    ///   Определяет параметры универсального типа для текущего типа, указывая их количество и имена, и возвращает массив объектов <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />, которые можно использовать для задания их ограничений.
    /// </summary>
    /// <param name="names">
    ///   Массив имен для параметров универсального типа.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />, которые можно использовать для определения ограничений параметров универсального типа для текущего типа.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для этого типа уже были определены параметры универсального типа.
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
        throw new ArgumentException();
      for (int index = 0; index < names.Length; ++index)
      {
        if (names[index] == null)
          throw new ArgumentNullException(nameof (names));
      }
      if (this.m_inst != null)
        throw new InvalidOperationException();
      this.m_inst = new GenericTypeParameterBuilder[names.Length];
      for (int genParamPos = 0; genParamPos < names.Length; ++genParamPos)
        this.m_inst[genParamPos] = new GenericTypeParameterBuilder(new TypeBuilder(names[genParamPos], genParamPos, this));
      return this.m_inst;
    }

    /// <summary>
    ///   Замещает элементы массива типов для параметров типа определения текущего универсального типа и возвращает результирующий сконструированный тип.
    /// </summary>
    /// <param name="typeArguments">
    ///   Массив типов, который должен быть замещен параметрами типа определения текущего универсального типа.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Type" /> представляет сконструированный тип, сформированный путем замещения элементов объекта <paramref name="typeArguments" /> параметрами текущего универсального типа.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий тип не представляет определение универсального типа.
    ///    То есть <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> возвращает <see langword="false" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeArguments" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Любой элемент <paramref name="typeArguments" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <see cref="P:System.Type.Module" /> любого элемента <paramref name="typeArguments" /> — <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <see cref="P:System.Reflection.Module.Assembly" /> модуля любого элемента <paramref name="typeArguments" /> — <see langword="null" />.
    /// </exception>
    public override Type MakeGenericType(params Type[] typeArguments)
    {
      this.CheckContext(typeArguments);
      return TypeBuilderInstantiation.MakeGenericType((Type) this, typeArguments);
    }

    /// <summary>
    ///   Возвращает массив объектов <see cref="T:System.Type" />, которые представляют аргументы универсального типа или параметры определения универсального типа.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />.
    ///    Элементы массива, которые представляют аргументы универсального типа или параметры определения универсального типа.
    /// </returns>
    public override Type[] GetGenericArguments()
    {
      return (Type[]) this.m_inst;
    }

    /// <summary>
    ///   Возвращает значение, определяющее, представляет ли текущий объект <see cref="T:System.Reflection.Emit.TypeBuilder" /> определение универсального типа, на основе которого можно конструировать другие универсальные типы.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если этот объект <see cref="T:System.Reflection.Emit.TypeBuilder" /> представляет определение универсального типа. В противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsGenericTypeDefinition
    {
      get
      {
        return this.IsGenericType;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий тип универсальным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если тип, представленный текущим объектом <see cref="T:System.Reflection.Emit.TypeBuilder" />, является универсальным; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsGenericType
    {
      get
      {
        return this.m_inst != null;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий тип параметром универсального типа.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see cref="T:System.Reflection.Emit.TypeBuilder" /> представляет параметр универсального типа; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool IsGenericParameter
    {
      get
      {
        return this.m_bIsGenParam;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, представляет ли этот данный объект сконструированный универсальный тип.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если этот объект представляет сконструированный универсальный тип; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsConstructedGenericType
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает позицию параметра типа в списке параметров типа универсального типа, в котором объявлен этот параметр.
    /// </summary>
    /// <returns>
    ///   Если текущий объект <see cref="T:System.Reflection.Emit.TypeBuilder" /> представляет параметр универсального типа, позиция параметра типа в списке параметров типа универсального типа, который объявил параметр. В противном случае — не определено.
    /// </returns>
    public override int GenericParameterPosition
    {
      get
      {
        return this.m_genParamPos;
      }
    }

    /// <summary>
    ///   Возвращает метод, который объявил текущий параметр универсального типа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodBase" />, представляющий метод, который объявил текущий тип, если текущий тип является параметром универсального типа; в противном случае — <see langword="null" />.
    /// </returns>
    public override MethodBase DeclaringMethod
    {
      get
      {
        return (MethodBase) this.m_declMeth;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, представляющий определение универсального типа, на основе которого можно получить текущий тип.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, представляющий определение универсального типа, на основе которого можно получить текущий тип.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий тип не является универсальным.
    ///    То есть <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> возвращает <see langword="false" />.
    /// </exception>
    public override Type GetGenericTypeDefinition()
    {
      if (this.IsGenericTypeDefinition)
        return (Type) this;
      if ((Type) this.m_genTypeDef == (Type) null)
        throw new InvalidOperationException();
      return (Type) this.m_genTypeDef;
    }

    /// <summary>
    ///   Задает тело данного метода, реализующее объявление данного метода, возможно, с другим именем.
    /// </summary>
    /// <param name="methodInfoBody">
    ///   Тело метода, которое будет использоваться.
    ///    Должно быть объектом <see langword="MethodBuilder" />.
    /// </param>
    /// <param name="methodInfoDeclaration">
    ///   Метод, объявление которого будет использоваться.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Объект <paramref name="methodInfoBody" /> не принадлежит к этому классу.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="methodInfoBody" /> или <paramref name="methodInfoDeclaration" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Объявляющий тип <paramref name="methodInfoBody" /> не является типом, представленным этим <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// </exception>
    [SecuritySafeCritical]
    public void DefineMethodOverride(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
    {
      lock (this.SyncRoot)
        this.DefineMethodOverrideNoLock(methodInfoBody, methodInfoDeclaration);
    }

    [SecurityCritical]
    private void DefineMethodOverrideNoLock(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
    {
      if (methodInfoBody == (MethodInfo) null)
        throw new ArgumentNullException(nameof (methodInfoBody));
      if (methodInfoDeclaration == (MethodInfo) null)
        throw new ArgumentNullException(nameof (methodInfoDeclaration));
      this.ThrowIfCreated();
      if ((object) methodInfoBody.DeclaringType != (object) this)
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_BadMethodImplBody"));
      TypeBuilder.DefineMethodImpl(this.m_module.GetNativeHandle(), this.m_tdType.Token, this.m_module.GetMethodTokenInternal(methodInfoBody).Token, this.m_module.GetMethodTokenInternal(methodInfoDeclaration).Token);
    }

    /// <summary>
    ///   Добавляет новый метод в тип с указанным именем, атрибутами метода и сигнатурой метода.
    /// </summary>
    /// <param name="name">
    ///   Имя метода.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attributes">Атрибуты метода.</param>
    /// <param name="returnType">Тип возвращаемых данных метода.</param>
    /// <param name="parameterTypes">Типы параметров метода.</param>
    /// <returns>Определенный метод.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Тип родительского элемента данного метода — интерфейс, и этот метод не является виртуальным (<see langword="Overridable" /> в Visual Basic).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего динамического типа свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    public MethodBuilder DefineMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
    {
      return this.DefineMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
    }

    /// <summary>
    ///   Добавляет новый метод в тип с указанным именем и атрибутами метода.
    /// </summary>
    /// <param name="name">
    ///   Имя метода.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attributes">Атрибуты метода.</param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.MethodBuilder" />, представляющий вновь определенный метод.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Тип родительского элемента данного метода — интерфейс, и этот метод не является виртуальным (<see langword="Overridable" /> в Visual Basic).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего динамического типа свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    public MethodBuilder DefineMethod(string name, MethodAttributes attributes)
    {
      return this.DefineMethod(name, attributes, CallingConventions.Standard, (Type) null, (Type[]) null);
    }

    /// <summary>
    ///   Добавляет новый метод в тип с указанным именем, атрибутами метода, соглашением о вызовах.
    /// </summary>
    /// <param name="name">
    ///   Имя метода.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attributes">Атрибуты метода.</param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах метода.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.MethodBuilder" />, представляющий вновь определенный метод.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Тип родительского элемента данного метода является интерфейсом, и этот метод не является виртуальным (<see langword="Overridable" /> в Visual Basic).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего динамического типа свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention)
    {
      return this.DefineMethod(name, attributes, callingConvention, (Type) null, (Type[]) null);
    }

    /// <summary>
    ///   Добавляет новый метод в тип с указанным именем, атрибутами метода, соглашением о вызовах и сигнатурой метода.
    /// </summary>
    /// <param name="name">
    ///   Имя метода.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attributes">Атрибуты метода.</param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах метода.
    /// </param>
    /// <param name="returnType">Тип возвращаемых данных метода.</param>
    /// <param name="parameterTypes">Типы параметров метода.</param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.MethodBuilder" />, представляющий вновь определенный метод.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Тип родительского элемента данного метода — интерфейс, и этот метод не является виртуальным (<see langword="Overridable" /> в Visual Basic).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего динамического типа свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      return this.DefineMethod(name, attributes, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>
    ///   Добавляет новый метод в тип с указанным именем, атрибутами метода, соглашением о вызовах, сигнатурой метода и настраиваемыми модификаторами.
    /// </summary>
    /// <param name="name">
    ///   Имя метода.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attributes">Атрибуты метода.</param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах метода.
    /// </param>
    /// <param name="returnType">Возвращаемый тип метода.</param>
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
    ///    Если ни один из параметров не имеет необязательных пользовательских модификаторов, укажите <see langword="null" /> вместо массива типов.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.MethodBuilder" />, представляющий добавленный метод.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Тип родительского элемента данного метода — интерфейс, и этот метод не является виртуальным (<see langword="Overridable" /> в Visual Basic).
    /// 
    ///   -или-
    /// 
    ///   Размер <paramref name="parameterTypeRequiredCustomModifiers" /> или <paramref name="parameterTypeOptionalCustomModifiers" /> не равен размеру <paramref name="parameterTypes" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего динамического типа свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      lock (this.SyncRoot)
        return this.DefineMethodNoLock(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
    }

    private MethodBuilder DefineMethodNoLock(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      this.CheckContext(new Type[1]{ returnType });
      this.CheckContext(returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes);
      this.CheckContext(parameterTypeRequiredCustomModifiers);
      this.CheckContext(parameterTypeOptionalCustomModifiers);
      if (parameterTypes != null)
      {
        if (parameterTypeOptionalCustomModifiers != null && parameterTypeOptionalCustomModifiers.Length != parameterTypes.Length)
          throw new ArgumentException(Environment.GetResourceString("Argument_MismatchedArrays", (object) nameof (parameterTypeOptionalCustomModifiers), (object) nameof (parameterTypes)));
        if (parameterTypeRequiredCustomModifiers != null && parameterTypeRequiredCustomModifiers.Length != parameterTypes.Length)
          throw new ArgumentException(Environment.GetResourceString("Argument_MismatchedArrays", (object) nameof (parameterTypeRequiredCustomModifiers), (object) nameof (parameterTypes)));
      }
      this.ThrowIfCreated();
      if (!this.m_isHiddenGlobalType && (this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask && ((attributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope))
        throw new ArgumentException(Environment.GetResourceString("Argument_BadAttributeOnInterfaceMethod"));
      MethodBuilder methodBuilder = new MethodBuilder(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, this.m_module, this, false);
      if (!this.m_isHiddenGlobalType && (methodBuilder.Attributes & MethodAttributes.SpecialName) != MethodAttributes.PrivateScope && methodBuilder.Name.Equals(ConstructorInfo.ConstructorName))
        ++this.m_constructorCount;
      this.m_listMethods.Add(methodBuilder);
      return methodBuilder;
    }

    /// <summary>Определяет инициализатор для этого типа.</summary>
    /// <returns>Возвращает инициализатор типа.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public ConstructorBuilder DefineTypeInitializer()
    {
      lock (this.SyncRoot)
        return this.DefineTypeInitializerNoLock();
    }

    [SecurityCritical]
    private ConstructorBuilder DefineTypeInitializerNoLock()
    {
      this.ThrowIfCreated();
      MethodAttributes attributes = MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.SpecialName;
      return new ConstructorBuilder(ConstructorInfo.TypeConstructorName, attributes, CallingConventions.Standard, (Type[]) null, this.m_module, this);
    }

    /// <summary>
    ///   Определяет конструктор по умолчанию.
    ///    Определенный здесь конструктор просто вызовет конструктор по умолчанию родительского элемента.
    /// </summary>
    /// <param name="attributes">
    ///   Объект <see langword="MethodAttributes" />, представляющий атрибуты, применяемые к конструктору.
    /// </param>
    /// <returns>Возвращает конструктор.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   У родительского типа (базового типа) нет конструктора по умолчанию.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего динамического типа свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    [ComVisible(true)]
    public ConstructorBuilder DefineDefaultConstructor(MethodAttributes attributes)
    {
      if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConstructorNotAllowedOnInterface"));
      lock (this.SyncRoot)
        return this.DefineDefaultConstructorNoLock(attributes);
    }

    private ConstructorBuilder DefineDefaultConstructorNoLock(MethodAttributes attributes)
    {
      ConstructorInfo con = (ConstructorInfo) null;
      if (this.m_typeParent is TypeBuilderInstantiation)
      {
        Type type1 = this.m_typeParent.GetGenericTypeDefinition();
        if (type1 is TypeBuilder)
          type1 = (Type) ((TypeBuilder) type1).m_bakedRuntimeType;
        if (type1 == (Type) null)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
        Type type2 = type1.MakeGenericType(this.m_typeParent.GetGenericArguments());
        con = !(type2 is TypeBuilderInstantiation) ? type2.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, Type.EmptyTypes, (ParameterModifier[]) null) : TypeBuilder.GetConstructor(type2, type1.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, Type.EmptyTypes, (ParameterModifier[]) null));
      }
      if (con == (ConstructorInfo) null)
        con = this.m_typeParent.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, Type.EmptyTypes, (ParameterModifier[]) null);
      if (con == (ConstructorInfo) null)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoParentDefaultConstructor"));
      ConstructorBuilder constructorBuilder = this.DefineConstructor(attributes, CallingConventions.Standard, (Type[]) null);
      ++this.m_constructorCount;
      ILGenerator ilGenerator = constructorBuilder.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Call, con);
      ilGenerator.Emit(OpCodes.Ret);
      constructorBuilder.m_isDefaultConstructor = true;
      return constructorBuilder;
    }

    /// <summary>
    ///   Добавляет в тип новый конструктор с заданными атрибутами и сигнатурой.
    /// </summary>
    /// <param name="attributes">Атрибуты конструктора.</param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах конструктора.
    /// </param>
    /// <param name="parameterTypes">Типы параметров конструктора.</param>
    /// <returns>Определенный конструктор.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    [ComVisible(true)]
    public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes)
    {
      return this.DefineConstructor(attributes, callingConvention, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>
    ///   Добавляет в тип новый конструктор с заданными атрибутами, сигнатурой и настраиваемыми модификаторами.
    /// </summary>
    /// <param name="attributes">Атрибуты конструктора.</param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах конструктора.
    /// </param>
    /// <param name="parameterTypes">Типы параметров конструктора.</param>
    /// <param name="requiredCustomModifiers">
    ///   Массив массивов типов.
    ///    Каждый массив типов представляет обязательные настраиваемые модификаторы для соответствующего параметра, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />.
    ///    Если определенный параметр не имеет обязательных настраиваемых модификаторов, укажите <see langword="null" /> вместо массива массивов.
    ///    Если ни один из параметров не имеет обязательных настраиваемых модификаторов, укажите <see langword="null" /> вместо массива массивов.
    /// </param>
    /// <param name="optionalCustomModifiers">
    ///   Массив массивов типов.
    ///    Каждый массив типов представляет необязательные настраиваемые модификаторы для соответствующего параметра, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />.
    ///    Если определенный параметр не имеет необязательных настраиваемых модификаторов, укажите <see langword="null" /> вместо массива массивов.
    ///    Если ни один из параметров не имеет необязательных настраиваемых модификаторов, укажите <see langword="null" /> вместо массива массивов.
    /// </param>
    /// <returns>Определенный конструктор.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Размер <paramref name="requiredCustomModifiers" /> или <paramref name="optionalCustomModifiers" /> не равен размеру <paramref name="parameterTypes" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего динамического типа свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
    {
      if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask && (attributes & MethodAttributes.Static) != MethodAttributes.Static)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConstructorNotAllowedOnInterface"));
      lock (this.SyncRoot)
        return this.DefineConstructorNoLock(attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers);
    }

    [SecurityCritical]
    private ConstructorBuilder DefineConstructorNoLock(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
    {
      this.CheckContext(parameterTypes);
      this.CheckContext(requiredCustomModifiers);
      this.CheckContext(optionalCustomModifiers);
      this.ThrowIfCreated();
      string name = (attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope ? ConstructorInfo.TypeConstructorName : ConstructorInfo.ConstructorName;
      attributes |= MethodAttributes.SpecialName;
      ConstructorBuilder constructorBuilder = new ConstructorBuilder(name, attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers, this.m_module, this);
      ++this.m_constructorCount;
      return constructorBuilder;
    }

    /// <summary>
    ///   Определяет метод <see langword="PInvoke" /> с учетом его имени, имени библиотеки DLL, в которой определен метод, атрибутов метода, соглашения о вызове метода, возвращаемого типа метода, типов параметров метода и флагов <see langword="PInvoke" />.
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
    ///   Метод не является статическим.
    /// 
    ///   -или-
    /// 
    ///   Родительский тип является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Метод является абстрактным.
    /// 
    ///   -или-
    /// 
    ///   Метод был определен ранее.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="name" /> или <paramref name="dllName" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="dllName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    [SecuritySafeCritical]
    public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      return this.DefinePInvokeMethodHelper(name, dllName, name, attributes, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null, nativeCallConv, nativeCharSet);
    }

    /// <summary>
    ///   Определяет метод <see langword="PInvoke" /> с учетом его имени, имени библиотеки DLL, в которой определен метод, имени точки входа, атрибутов метода, соглашения о вызове метода, возвращаемого типа метода, типов параметров метода и флагов <see langword="PInvoke" />.
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
    ///   Метод не является статическим.
    /// 
    ///   -или-
    /// 
    ///   Родительский тип является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Метод является абстрактным.
    /// 
    ///   -или-
    /// 
    ///   Метод был определен ранее.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="name" />, <paramref name="dllName" /> или <paramref name="entryName" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="name" />, <paramref name="dllName" /> или <paramref name="entryName" /> равно <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    [SecuritySafeCritical]
    public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      return this.DefinePInvokeMethodHelper(name, dllName, entryName, attributes, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null, nativeCallConv, nativeCharSet);
    }

    /// <summary>
    ///   Определяет метод <see langword="PInvoke" /> с учетом его имени, имени библиотеки DLL, в которой определен метод, имени точки входа, атрибутов метода, соглашения о вызове метода, возвращаемого типа метода, типов параметров метода, флагов <see langword="PInvoke" /> и настраиваемых модификаторов для параметров и возвращаемого типа.
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
    /// <param name="returnTypeRequiredCustomModifiers">
    ///   Массив типов, представляющих обязательные настраиваемые модификаторы, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />, для возвращаемого типа метода.
    ///    Если возвращаемый тип не содержит требуемых настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="returnTypeOptionalCustomModifiers">
    ///   Массив типов, представляющих необязательные настраиваемые модификаторы, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />, для возвращаемого типа метода.
    ///    Если возвращаемый тип не содержит необязательные настраиваемые модификаторы, укажите <see langword="null" />.
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
    ///    Если ни один из параметров не имеет необязательных настраиваемых модификаторов, укажите <see langword="null" /> вместо массива типов.
    /// </param>
    /// <param name="nativeCallConv">
    ///   Собственное соглашение о вызове.
    /// </param>
    /// <param name="nativeCharSet">Собственная кодировка метода.</param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.MethodBuilder" />, представляющий определенный метод <see langword="PInvoke" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Метод не является статическим.
    /// 
    ///   -или-
    /// 
    ///   Родительский тип является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Метод является абстрактным.
    /// 
    ///   -или-
    /// 
    ///   Метод был определен ранее.
    /// 
    ///   -или-
    /// 
    ///   Длина параметра <paramref name="name" />, <paramref name="dllName" /> или <paramref name="entryName" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Размер <paramref name="parameterTypeRequiredCustomModifiers" /> или <paramref name="parameterTypeOptionalCustomModifiers" /> не равен размеру <paramref name="parameterTypes" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" />, <paramref name="dllName" /> или <paramref name="entryName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего динамического типа свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    [SecuritySafeCritical]
    public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      return this.DefinePInvokeMethodHelper(name, dllName, entryName, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, nativeCallConv, nativeCharSet);
    }

    /// <summary>Определяет вложенный тип с заданным именем.</summary>
    /// <param name="name">
    ///   Краткое имя типа.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <returns>Определенный вложенный тип.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина <paramref name="name" /> равна нулю или больше 1023.
    /// 
    ///   -или-
    /// 
    ///   Эта операция создаст тип с повторяющимся <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> в текущей сборке.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineNestedType(string name)
    {
      lock (this.SyncRoot)
        return this.DefineNestedTypeNoLock(name, TypeAttributes.NestedPrivate, (Type) null, (Type[]) null, PackingSize.Unspecified, 0);
    }

    /// <summary>
    ///   Определяет вложенный тип по заданным имени, атрибутам, типу, который он расширяет, и интерфейсам, которые он реализует.
    /// </summary>
    /// <param name="name">
    ///   Краткое имя типа.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attr">Атрибуты типа.</param>
    /// <param name="parent">Тип, расширяемый вложенным типом.</param>
    /// <param name="interfaces">
    ///   Интерфейсы, реализуемые вложенным типом.
    /// </param>
    /// <returns>Определенный вложенный тип.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не указан вложенный атрибут.
    /// 
    ///   -или-
    /// 
    ///   Этот тип запечатан.
    /// 
    ///   -или-
    /// 
    ///   Этот тип является массивом.
    /// 
    ///   -или-
    /// 
    ///   Этот тип является интерфейсом, а вложенный тип не является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="name" /> равна нулю или больше 1023.
    /// 
    ///   -или-
    /// 
    ///   Эта операция создаст тип с повторяющимся <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> в текущей сборке.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Элемент массива <paramref name="interfaces" /> является <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
    {
      lock (this.SyncRoot)
      {
        this.CheckContext(new Type[1]{ parent });
        this.CheckContext(interfaces);
        return this.DefineNestedTypeNoLock(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
      }
    }

    /// <summary>
    ///   Определяет вложенный тип на основе его имени, атрибутов и типа, который он расширяет.
    /// </summary>
    /// <param name="name">
    ///   Краткое имя типа.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attr">Атрибуты типа.</param>
    /// <param name="parent">Тип, расширяемый вложенным типом.</param>
    /// <returns>Определенный вложенный тип.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не указан вложенный атрибут.
    /// 
    ///   -или-
    /// 
    ///   Этот тип запечатан.
    /// 
    ///   -или-
    /// 
    ///   Этот тип является массивом.
    /// 
    ///   -или-
    /// 
    ///   Этот тип является интерфейсом, а вложенный тип не является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="name" /> равна нулю или больше 1023.
    /// 
    ///   -или-
    /// 
    ///   Эта операция создаст тип с повторяющимся <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> в текущей сборке.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent)
    {
      lock (this.SyncRoot)
        return this.DefineNestedTypeNoLock(name, attr, parent, (Type[]) null, PackingSize.Unspecified, 0);
    }

    /// <summary>
    ///   Определяет вложенный тип с заданным именем и атрибутами.
    /// </summary>
    /// <param name="name">
    ///   Краткое имя типа.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attr">Атрибуты типа.</param>
    /// <returns>Определенный вложенный тип.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не указан вложенный атрибут.
    /// 
    ///   -или-
    /// 
    ///   Этот тип запечатан.
    /// 
    ///   -или-
    /// 
    ///   Этот тип является массивом.
    /// 
    ///   -или-
    /// 
    ///   Этот тип является интерфейсом, а вложенный тип не является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="name" /> равна нулю или больше 1023.
    /// 
    ///   -или-
    /// 
    ///   Эта операция создаст тип с повторяющимся <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> в текущей сборке.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineNestedType(string name, TypeAttributes attr)
    {
      lock (this.SyncRoot)
        return this.DefineNestedTypeNoLock(name, attr, (Type) null, (Type[]) null, PackingSize.Unspecified, 0);
    }

    /// <summary>
    ///   Определяет вложенный тип по заданным имени, атрибутам, общему размеру типа и типу, который он расширяет.
    /// </summary>
    /// <param name="name">
    ///   Краткое имя типа.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attr">Атрибуты типа.</param>
    /// <param name="parent">Тип, расширяемый вложенным типом.</param>
    /// <param name="typeSize">Общий размер типа.</param>
    /// <returns>Определенный вложенный тип.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не указан вложенный атрибут.
    /// 
    ///   -или-
    /// 
    ///   Этот тип запечатан.
    /// 
    ///   -или-
    /// 
    ///   Этот тип является массивом.
    /// 
    ///   -или-
    /// 
    ///   Этот тип является интерфейсом, а вложенный тип не является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="name" /> равна нулю или больше 1023.
    /// 
    ///   -или-
    /// 
    ///   Эта операция создаст тип с повторяющимся <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> в текущей сборке.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, int typeSize)
    {
      lock (this.SyncRoot)
        return this.DefineNestedTypeNoLock(name, attr, parent, (Type[]) null, PackingSize.Unspecified, typeSize);
    }

    /// <summary>
    ///   Определяет вложенный тип по заданным имени, атрибутам, типу, который он расширяет, и размеру упаковки.
    /// </summary>
    /// <param name="name">
    ///   Краткое имя типа.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attr">Атрибуты типа.</param>
    /// <param name="parent">Тип, который вложенный тип расширяет.</param>
    /// <param name="packSize">Размер упаковки типа.</param>
    /// <returns>Определенный вложенный тип.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не указан вложенный атрибут.
    /// 
    ///   -или-
    /// 
    ///   Этот тип запечатан.
    /// 
    ///   -или-
    /// 
    ///   Этот тип является массивом.
    /// 
    ///   -или-
    /// 
    ///   Этот тип является интерфейсом, а вложенный тип не является интерфейсом.
    /// 
    ///   -или-
    /// 
    ///   Длина <paramref name="name" /> равна нулю или больше 1023.
    /// 
    ///   -или-
    /// 
    ///   Эта операция создаст тип с повторяющимся <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> в текущей сборке.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize)
    {
      lock (this.SyncRoot)
        return this.DefineNestedTypeNoLock(name, attr, parent, (Type[]) null, packSize, 0);
    }

    /// <summary>
    ///   Определяет вложенный тип на основе его имени, атрибутов, размера и типа, который он расширяет.
    /// </summary>
    /// <param name="name">
    ///   Краткое имя объекта.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attr">Атрибуты типа.</param>
    /// <param name="parent">Тип, который вложенный тип расширяет.</param>
    /// <param name="packSize">Размер упаковки типа.</param>
    /// <param name="typeSize">Общий размер типа.</param>
    /// <returns>Определенный вложенный тип.</returns>
    [SecuritySafeCritical]
    public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize, int typeSize)
    {
      lock (this.SyncRoot)
        return this.DefineNestedTypeNoLock(name, attr, parent, (Type[]) null, packSize, typeSize);
    }

    [SecurityCritical]
    private TypeBuilder DefineNestedTypeNoLock(string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packSize, int typeSize)
    {
      return new TypeBuilder(name, attr, parent, interfaces, this.m_module, packSize, typeSize, this);
    }

    /// <summary>
    ///   Добавляет новое поле в тип с заданным именем, атрибутами и типом поля.
    /// </summary>
    /// <param name="fieldName">
    ///   Имя поля.
    ///    Параметр <paramref name="fieldName" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="type">Тип поля.</param>
    /// <param name="attributes">Атрибуты поля.</param>
    /// <returns>Определенное поле.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="fieldName" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="type" /> имеет значение System.Void.
    /// 
    ///   -или-
    /// 
    ///   Общий размер был указан для родительского класса этого поля.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="fieldName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    public FieldBuilder DefineField(string fieldName, Type type, FieldAttributes attributes)
    {
      return this.DefineField(fieldName, type, (Type[]) null, (Type[]) null, attributes);
    }

    /// <summary>
    ///   Добавляет новое поле в тип с заданным именем, атрибутами, типом поля и настраиваемыми модификаторами.
    /// </summary>
    /// <param name="fieldName">
    ///   Имя поля.
    ///    Параметр <paramref name="fieldName" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="type">Тип поля.</param>
    /// <param name="requiredCustomModifiers">
    ///   Массив типов, представляющих обязательные настраиваемые модификаторы для поля, например <see cref="T:Microsoft.VisualC.IsConstModifier" />.
    /// </param>
    /// <param name="optionalCustomModifiers">
    ///   Массив типов, представляющих необязательные настраиваемые модификаторы для поля, например <see cref="T:Microsoft.VisualC.IsConstModifier" />.
    /// </param>
    /// <param name="attributes">Атрибуты поля.</param>
    /// <returns>Определенное поле.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="fieldName" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="type" /> имеет значение System.Void.
    /// 
    ///   -или-
    /// 
    ///   Общий размер был указан для родительского класса этого поля.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="fieldName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    [SecuritySafeCritical]
    public FieldBuilder DefineField(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
    {
      lock (this.SyncRoot)
        return this.DefineFieldNoLock(fieldName, type, requiredCustomModifiers, optionalCustomModifiers, attributes);
    }

    [SecurityCritical]
    private FieldBuilder DefineFieldNoLock(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
    {
      this.ThrowIfCreated();
      this.CheckContext(new Type[1]{ type });
      this.CheckContext(requiredCustomModifiers);
      if (this.m_enumUnderlyingType == (Type) null && this.IsEnum && (attributes & FieldAttributes.Static) == FieldAttributes.PrivateScope)
        this.m_enumUnderlyingType = type;
      return new FieldBuilder(this, fieldName, type, requiredCustomModifiers, optionalCustomModifiers, attributes);
    }

    /// <summary>
    ///   Определяет инициализированное поле данных в разделе .sdata переносимого исполняемого (PE) файла.
    /// </summary>
    /// <param name="name">
    ///   Имя, используемое для ссылки на данные.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="data">Большой двоичный объект данных.</param>
    /// <param name="attributes">Атрибуты поля.</param>
    /// <returns>Поле для ссылки на данные.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// 
    ///   -или-
    /// 
    ///   Размер данных не больше нуля либо не меньше 0x3f0000.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="data" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван ранее.
    /// </exception>
    [SecuritySafeCritical]
    public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
    {
      lock (this.SyncRoot)
        return this.DefineInitializedDataNoLock(name, data, attributes);
    }

    [SecurityCritical]
    private FieldBuilder DefineInitializedDataNoLock(string name, byte[] data, FieldAttributes attributes)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      return this.DefineDataHelper(name, data, data.Length, attributes);
    }

    /// <summary>
    ///   Определяет неинициализированное поле данных в разделе <see langword=".sdata" /> переносимого исполняемого (PE) файла.
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
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    [SecuritySafeCritical]
    public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
    {
      lock (this.SyncRoot)
        return this.DefineUninitializedDataNoLock(name, size, attributes);
    }

    [SecurityCritical]
    private FieldBuilder DefineUninitializedDataNoLock(string name, int size, FieldAttributes attributes)
    {
      return this.DefineDataHelper(name, (byte[]) null, size, attributes);
    }

    /// <summary>
    ///   Добавляет новое свойство в тип с заданным именем и сигнатурой свойства.
    /// </summary>
    /// <param name="name">
    ///   Имя свойства.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attributes">Атрибуты свойства.</param>
    /// <param name="returnType">Возвращаемый тип свойства.</param>
    /// <param name="parameterTypes">Типы параметров свойства.</param>
    /// <returns>Заданное свойство.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из элементов массива <paramref name="parameterTypes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] parameterTypes)
    {
      return this.DefineProperty(name, attributes, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>
    ///   Добавляет новое свойство в тип с заданным именем, атрибутами, соглашением о вызове и сигнатурой свойства.
    /// </summary>
    /// <param name="name">
    ///   Имя свойства.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attributes">Атрибуты свойства.</param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах методов доступа свойства.
    /// </param>
    /// <param name="returnType">Возвращаемый тип свойства.</param>
    /// <param name="parameterTypes">Типы параметров свойства.</param>
    /// <returns>Заданное свойство.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из элементов массива <paramref name="parameterTypes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      return this.DefineProperty(name, attributes, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>
    ///   Добавляет новое свойство в тип с заданным именем, сигнатурой свойства и настраиваемыми модификаторами.
    /// </summary>
    /// <param name="name">
    ///   Имя свойства.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attributes">Атрибуты свойства.</param>
    /// <param name="returnType">Возвращаемый тип свойства.</param>
    /// <param name="returnTypeRequiredCustomModifiers">
    ///   Массив типов, представляющих обязательные настраиваемые модификаторы, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />, для возвращаемого типа свойства.
    ///    Если возвращаемый тип не содержит требуемых настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="returnTypeOptionalCustomModifiers">
    ///   Массив типов, представляющих необязательные настраиваемые модификаторы, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />, для возвращаемого типа свойства.
    ///    Если возвращаемый тип не содержит необязательных настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="parameterTypes">Типы параметров свойства.</param>
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
    /// <returns>Заданное свойство.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="name" /> равно <see langword="null" />
    /// 
    ///   -или-
    /// 
    ///   Один из элементов массива <paramref name="parameterTypes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      return this.DefineProperty(name, attributes, (CallingConventions) 0, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
    }

    /// <summary>
    ///   Добавляет новое свойство в тип с заданным именем, соглашением о вызове, сигнатурой свойства и настраиваемыми модификаторами.
    /// </summary>
    /// <param name="name">
    ///   Имя свойства.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attributes">Атрибуты свойства.</param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах методов доступа свойства.
    /// </param>
    /// <param name="returnType">Возвращаемый тип свойства.</param>
    /// <param name="returnTypeRequiredCustomModifiers">
    ///   Массив типов, представляющих обязательные настраиваемые модификаторы, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />, для возвращаемого типа свойства.
    ///    Если возвращаемый тип не содержит требуемых настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="returnTypeOptionalCustomModifiers">
    ///   Массив типов, представляющих необязательные настраиваемые модификаторы, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />, для возвращаемого типа свойства.
    ///    Если возвращаемый тип не содержит необязательных настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="parameterTypes">Типы параметров свойства.</param>
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
    /// <returns>Заданное свойство.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из элементов массива <paramref name="parameterTypes" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    [SecuritySafeCritical]
    public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      lock (this.SyncRoot)
        return this.DefinePropertyNoLock(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
    }

    [SecurityCritical]
    private PropertyBuilder DefinePropertyNoLock(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      this.CheckContext(new Type[1]{ returnType });
      this.CheckContext(returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes);
      this.CheckContext(parameterTypeRequiredCustomModifiers);
      this.CheckContext(parameterTypeOptionalCustomModifiers);
      this.ThrowIfCreated();
      SignatureHelper propertySigHelper = SignatureHelper.GetPropertySigHelper((Module) this.m_module, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
      int length;
      byte[] signature = propertySigHelper.InternalGetSignature(out length);
      PropertyToken prToken = new PropertyToken(TypeBuilder.DefineProperty(this.m_module.GetNativeHandle(), this.m_tdType.Token, name, attributes, signature, length));
      return new PropertyBuilder(this.m_module, name, propertySigHelper, attributes, returnType, prToken, this);
    }

    /// <summary>
    ///   Добавляет новое событие в тип с заданным именем, атрибутами и типом события.
    /// </summary>
    /// <param name="name">
    ///   Имя события.
    ///    Параметр <paramref name="name" /> не может содержать внедренные значения NULL.
    /// </param>
    /// <param name="attributes">Атрибуты события.</param>
    /// <param name="eventtype">Тип события.</param>
    /// <returns>Определенное событие.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="name" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="eventtype" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    [SecuritySafeCritical]
    public EventBuilder DefineEvent(string name, EventAttributes attributes, Type eventtype)
    {
      lock (this.SyncRoot)
        return this.DefineEventNoLock(name, attributes, eventtype);
    }

    [SecurityCritical]
    private EventBuilder DefineEventNoLock(string name, EventAttributes attributes, Type eventtype)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      if (name[0] == char.MinValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), nameof (name));
      this.CheckContext(new Type[1]{ eventtype });
      this.ThrowIfCreated();
      int token = this.m_module.GetTypeTokenInternal(eventtype).Token;
      EventToken evToken = new EventToken(TypeBuilder.DefineEvent(this.m_module.GetNativeHandle(), this.m_tdType.Token, name, attributes, token));
      return new EventBuilder(this.m_module, name, attributes, this, evToken);
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Reflection.TypeInfo" />, представляющий этот тип.
    /// </summary>
    /// <returns>Объект, представляющий этот тип.</returns>
    [SecuritySafeCritical]
    public TypeInfo CreateTypeInfo()
    {
      lock (this.SyncRoot)
        return this.CreateTypeNoLock();
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Type" /> для этого класса.
    ///    После определения полей и методов в классе вызывается метод <see langword="CreateType" /> для загрузки его объекта <see langword="Type" />.
    /// </summary>
    /// <returns>
    ///   Возвращает новый объект <see cref="T:System.Type" /> для этого класса.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Включающий тип не создан.
    /// 
    ///   -или-
    /// 
    ///   Этот тип не является абстрактным, а содержит абстрактный метод.
    /// 
    ///   -или-
    /// 
    ///   Этот тип не является абстрактным классом или интерфейсом, а содержит метод без тела метода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип содержит неверный код на языке MSIL.
    /// 
    ///   -или-
    /// 
    ///   Конечный адрес ветвления задан с использованием однобайтового смещения, но он находится на расстоянии более 127 байт от ветви.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип.
    ///    Например, он содержит метод <see langword="static" />, имеющий соглашение о вызовах <see cref="F:System.Reflection.CallingConventions.HasThis" />.
    /// </exception>
    [SecuritySafeCritical]
    public Type CreateType()
    {
      lock (this.SyncRoot)
        return (Type) this.CreateTypeNoLock();
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
    private TypeInfo CreateTypeNoLock()
    {
      if (this.IsCreated())
        return (TypeInfo) this.m_bakedRuntimeType;
      this.ThrowIfCreated();
      if (this.m_typeInterfaces == null)
        this.m_typeInterfaces = new List<Type>();
      int[] numArray1 = new int[this.m_typeInterfaces.Count];
      TypeToken typeTokenInternal;
      for (int index1 = 0; index1 < this.m_typeInterfaces.Count; ++index1)
      {
        int[] numArray2 = numArray1;
        int index2 = index1;
        typeTokenInternal = this.m_module.GetTypeTokenInternal(this.m_typeInterfaces[index1]);
        int token = typeTokenInternal.Token;
        numArray2[index2] = token;
      }
      int tkParent = 0;
      if (this.m_typeParent != (Type) null)
      {
        typeTokenInternal = this.m_module.GetTypeTokenInternal(this.m_typeParent);
        tkParent = typeTokenInternal.Token;
      }
      if (this.IsGenericParameter)
      {
        int[] constraints;
        if (this.m_typeParent != (Type) null)
        {
          constraints = new int[this.m_typeInterfaces.Count + 2];
          constraints[constraints.Length - 2] = tkParent;
        }
        else
          constraints = new int[this.m_typeInterfaces.Count + 1];
        for (int index1 = 0; index1 < this.m_typeInterfaces.Count; ++index1)
        {
          int[] numArray2 = constraints;
          int index2 = index1;
          typeTokenInternal = this.m_module.GetTypeTokenInternal(this.m_typeInterfaces[index1]);
          int token = typeTokenInternal.Token;
          numArray2[index2] = token;
        }
        this.m_tdType = new TypeToken(TypeBuilder.DefineGenericParam(this.m_module.GetNativeHandle(), this.m_strName, (MethodInfo) this.m_declMeth == (MethodInfo) null ? this.m_DeclaringType.m_tdType.Token : this.m_declMeth.GetToken().Token, this.m_genParamAttributes, this.m_genParamPos, constraints));
        if (this.m_ca != null)
        {
          foreach (TypeBuilder.CustAttr custAttr in this.m_ca)
            custAttr.Bake(this.m_module, this.MetadataTokenInternal);
        }
        this.m_hasBeenCreated = true;
        return (TypeInfo) this;
      }
      if ((this.m_tdType.Token & 16777215) != 0 && (tkParent & 16777215) != 0)
        TypeBuilder.SetParentType(this.m_module.GetNativeHandle(), this.m_tdType.Token, tkParent);
      if (this.m_inst != null)
      {
        foreach (Type type in this.m_inst)
        {
          if (type is GenericTypeParameterBuilder)
            ((GenericTypeParameterBuilder) type).m_type.CreateType();
        }
      }
      if (!this.m_isHiddenGlobalType && this.m_constructorCount == 0 && ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this.IsValueType) && (this.m_iAttr & (TypeAttributes.Abstract | TypeAttributes.Sealed)) != (TypeAttributes.Abstract | TypeAttributes.Sealed))
        this.DefineDefaultConstructor(MethodAttributes.Public);
      int count = this.m_listMethods.Count;
      for (int index = 0; index < count; ++index)
      {
        MethodBuilder listMethod = this.m_listMethods[index];
        if (listMethod.IsGenericMethodDefinition)
          listMethod.GetToken();
        MethodAttributes attributes = listMethod.Attributes;
        if ((listMethod.GetMethodImplementationFlags() & (MethodImplAttributes.CodeTypeMask | MethodImplAttributes.ManagedMask | MethodImplAttributes.PreserveSig)) == MethodImplAttributes.IL && (attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
        {
          int signatureLength;
          byte[] localSignature = listMethod.GetLocalSignature(out signatureLength);
          if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope && (this.m_iAttr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadTypeAttributesNotAbstract"));
          byte[] body = listMethod.GetBody();
          if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope)
          {
            if (body != null)
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadMethodBody"));
          }
          else if (body == null || body.Length == 0)
          {
            if (listMethod.m_ilGenerator != null)
              listMethod.CreateMethodBodyHelper(listMethod.GetILGenerator());
            body = listMethod.GetBody();
            if ((body == null || body.Length == 0) && !listMethod.m_canBeRuntimeImpl)
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadEmptyMethodBody", (object) listMethod.Name));
          }
          int maxStack = listMethod.GetMaxStack();
          ExceptionHandler[] exceptionHandlers = listMethod.GetExceptionHandlers();
          int[] tokenFixups = listMethod.GetTokenFixups();
          TypeBuilder.SetMethodIL(this.m_module.GetNativeHandle(), listMethod.GetToken().Token, listMethod.InitLocals, body, body != null ? body.Length : 0, localSignature, signatureLength, maxStack, exceptionHandlers, exceptionHandlers != null ? exceptionHandlers.Length : 0, tokenFixups, tokenFixups != null ? tokenFixups.Length : 0);
          if (this.m_module.ContainingAssemblyBuilder.m_assemblyData.m_access == AssemblyBuilderAccess.Run)
            listMethod.ReleaseBakedStructures();
        }
      }
      this.m_hasBeenCreated = true;
      RuntimeType o = (RuntimeType) null;
      TypeBuilder.TermCreateClass(this.m_module.GetNativeHandle(), this.m_tdType.Token, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      if (this.m_isHiddenGlobalType)
        return (TypeInfo) null;
      this.m_bakedRuntimeType = o;
      if ((Type) this.m_DeclaringType != (Type) null && this.m_DeclaringType.m_bakedRuntimeType != (RuntimeType) null)
        this.m_DeclaringType.m_bakedRuntimeType.InvalidateCachedNestedType();
      return (TypeInfo) o;
    }

    /// <summary>Получает общий размер типа.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Получает общий размер этого типа.
    /// </returns>
    public int Size
    {
      get
      {
        return this.m_iTypeSize;
      }
    }

    /// <summary>Получает размер упаковки данного типа.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Получает размер упаковки данного типа.
    /// </returns>
    public PackingSize PackingSize
    {
      get
      {
        return this.m_iPackingSize;
      }
    }

    /// <summary>
    ///   Задает базовый тип конструируемого в настоящий момент типа.
    /// </summary>
    /// <param name="parent">Новый базовый тип.</param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="parent" /> имеет значение <see langword="null" />, и текущий экземпляр представляет интерфейс, атрибуты которого не включают <see cref="F:System.Reflection.TypeAttributes.Abstract" />.
    /// 
    ///   -или-
    /// 
    ///   Для текущего динамического типа свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="parent" /> является интерфейсом.
    ///    Это условие исключения было впервые представлено в .NET Framework 2.0.
    /// </exception>
    public void SetParent(Type parent)
    {
      this.ThrowIfCreated();
      if (parent != (Type) null)
      {
        this.CheckContext(new Type[1]{ parent });
        if (parent.IsInterface)
          throw new ArgumentException(Environment.GetResourceString("Argument_CannotSetParentToInterface"));
        this.m_typeParent = parent;
      }
      else if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) != TypeAttributes.ClassSemanticsMask)
      {
        this.m_typeParent = typeof (object);
      }
      else
      {
        if ((this.m_iAttr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadInterfaceNotAbstract"));
        this.m_typeParent = (Type) null;
      }
    }

    /// <summary>Добавляет интерфейс, реализуемый данным типом.</summary>
    /// <param name="interfaceType">
    ///   Интерфейс, реализуемый данным типом.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="interfaceType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void AddInterfaceImplementation(Type interfaceType)
    {
      if (interfaceType == (Type) null)
        throw new ArgumentNullException(nameof (interfaceType));
      this.CheckContext(new Type[1]{ interfaceType });
      this.ThrowIfCreated();
      TypeBuilder.AddInterfaceImpl(this.m_module.GetNativeHandle(), this.m_tdType.Token, this.m_module.GetTypeTokenInternal(interfaceType).Token);
      this.m_typeInterfaces.Add(interfaceType);
    }

    /// <summary>Добавляет декларативную безопасность в этот тип.</summary>
    /// <param name="action">
    ///   Выполняемое действие безопасности, например Demand, Assert и т. д.
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
    ///   Набор разрешений <paramref name="pset" /> содержит действие, добавленное ранее с помощью <see langword="AddDeclarativeSecurity" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="pset" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
    {
      lock (this.SyncRoot)
        this.AddDeclarativeSecurityNoLock(action, pset);
    }

    [SecurityCritical]
    private void AddDeclarativeSecurityNoLock(SecurityAction action, PermissionSet pset)
    {
      if (pset == null)
        throw new ArgumentNullException(nameof (pset));
      if (!Enum.IsDefined(typeof (SecurityAction), (object) action) || action == SecurityAction.RequestMinimum || (action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse))
        throw new ArgumentOutOfRangeException(nameof (action));
      this.ThrowIfCreated();
      byte[] blob = (byte[]) null;
      int cb = 0;
      if (!pset.IsEmpty())
      {
        blob = pset.EncodeXml();
        cb = blob.Length;
      }
      TypeBuilder.AddDeclarativeSecurity(this.m_module.GetNativeHandle(), this.m_tdType.Token, action, blob, cb);
    }

    /// <summary>Возвращает токен типа этого типа.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Возвращает <see langword="TypeToken" /> этого типа.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    public TypeToken TypeToken
    {
      get
      {
        if (this.IsGenericParameter)
          this.ThrowIfCreated();
        return this.m_tdType;
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
    ///   Для текущего динамического типа свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (con));
      if (binaryAttribute == null)
        throw new ArgumentNullException(nameof (binaryAttribute));
      TypeBuilder.DefineCustomAttribute(this.m_module, this.m_tdType.Token, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
    }

    /// <summary>
    ///   Задает настраиваемый атрибут с помощью построителя настраиваемых атрибутов.
    /// </summary>
    /// <param name="customBuilder">
    ///   Экземпляр вспомогательного класса для определения настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="customBuilder" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Для текущего динамического типа свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> имеет значение <see langword="true" />, но свойство <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> имеет значение <see langword="false" />.
    /// </exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException(nameof (customBuilder));
      customBuilder.CreateCustomAttribute(this.m_module, this.m_tdType.Token);
    }

    void _TypeBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _TypeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _TypeBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _TypeBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    private class CustAttr
    {
      private ConstructorInfo m_con;
      private byte[] m_binaryAttribute;
      private CustomAttributeBuilder m_customBuilder;

      public CustAttr(ConstructorInfo con, byte[] binaryAttribute)
      {
        if (con == (ConstructorInfo) null)
          throw new ArgumentNullException(nameof (con));
        if (binaryAttribute == null)
          throw new ArgumentNullException(nameof (binaryAttribute));
        this.m_con = con;
        this.m_binaryAttribute = binaryAttribute;
      }

      public CustAttr(CustomAttributeBuilder customBuilder)
      {
        if (customBuilder == null)
          throw new ArgumentNullException(nameof (customBuilder));
        this.m_customBuilder = customBuilder;
      }

      [SecurityCritical]
      public void Bake(ModuleBuilder module, int token)
      {
        if (this.m_customBuilder == null)
          TypeBuilder.DefineCustomAttribute(module, token, module.GetConstructorToken(this.m_con).Token, this.m_binaryAttribute, false, false);
        else
          this.m_customBuilder.CreateCustomAttribute(module, token);
      }
    }
  }
}
