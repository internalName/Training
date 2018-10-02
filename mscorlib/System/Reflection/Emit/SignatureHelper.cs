// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.SignatureHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Reflection.Emit
{
  /// <summary>Предоставляет методы для создания подписи.</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_SignatureHelper))]
  [ComVisible(true)]
  public sealed class SignatureHelper : _SignatureHelper
  {
    private const int NO_SIZE_IN_SIG = -1;
    private byte[] m_signature;
    private int m_currSig;
    private int m_sizeLoc;
    private ModuleBuilder m_module;
    private bool m_sigDone;
    private int m_argCount;

    /// <summary>
    ///   Возвращает вспомогательный объект подписи для метода со стандартным соглашением о вызове, для указанного модуля, тип возвращаемого значения и типы аргументов метода.
    /// </summary>
    /// <param name="mod">
    ///   <see cref="T:System.Reflection.Emit.ModuleBuilder" /> Содержащий метод, для которого <see langword="SignatureHelper" /> запрашивается.
    /// </param>
    /// <param name="returnType">
    ///   Типом возвращаемого значения метода или <see langword="null" /> для типом возвращаемого значения void (<see langword="Sub" /> процедуры в Visual Basic).
    /// </param>
    /// <param name="parameterTypes">
    ///   Типы аргументов метода, или <see langword="null" /> Если метод не имеет аргументов.
    /// </param>
    /// <returns>
    ///   <see langword="SignatureHelper" /> Объекта для метода.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Элемент <paramref name="parameterTypes" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="mod" /> не является объектом <see cref="T:System.Reflection.Emit.ModuleBuilder" />.
    /// </exception>
    [SecuritySafeCritical]
    public static SignatureHelper GetMethodSigHelper(Module mod, Type returnType, Type[] parameterTypes)
    {
      return SignatureHelper.GetMethodSigHelper(mod, CallingConventions.Standard, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    [SecurityCritical]
    internal static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, Type returnType, int cGenericParam)
    {
      return SignatureHelper.GetMethodSigHelper(mod, callingConvention, cGenericParam, returnType, (Type[]) null, (Type[]) null, (Type[]) null, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>
    ///   Возвращает вспомогательный объект подписи для метода с учетом соответствующего модуля, соглашения о вызовах и типа возвращаемого значения.
    /// </summary>
    /// <param name="mod">
    ///   <see cref="T:System.Reflection.Emit.ModuleBuilder" /> — содержит метод, для которого запрашивается <see langword="SignatureHelper" />.
    /// </param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах метода.
    /// </param>
    /// <param name="returnType">
    ///   Тип возвращаемого значения метода или <see langword="null" /> для типа возвращаемого значения void (процедура <see langword="Sub" /> в Visual Basic).
    /// </param>
    /// <returns>
    ///   Объект <see langword="SignatureHelper" /> для метода.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mod" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="mod" /> не является объектом <see cref="T:System.Reflection.Emit.ModuleBuilder" />.
    /// </exception>
    [SecuritySafeCritical]
    public static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, Type returnType)
    {
      return SignatureHelper.GetMethodSigHelper(mod, callingConvention, returnType, (Type[]) null, (Type[]) null, (Type[]) null, (Type[][]) null, (Type[][]) null);
    }

    internal static SignatureHelper GetMethodSpecSigHelper(Module scope, Type[] inst)
    {
      SignatureHelper signatureHelper = new SignatureHelper(scope, System.Reflection.MdSigCallingConvention.GenericInst);
      signatureHelper.AddData(inst.Length);
      foreach (Type clsArgument in inst)
        signatureHelper.AddArgument(clsArgument);
      return signatureHelper;
    }

    [SecurityCritical]
    internal static SignatureHelper GetMethodSigHelper(Module scope, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      return SignatureHelper.GetMethodSigHelper(scope, callingConvention, 0, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
    }

    [SecurityCritical]
    internal static SignatureHelper GetMethodSigHelper(Module scope, CallingConventions callingConvention, int cGenericParam, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      if (returnType == (Type) null)
        returnType = typeof (void);
      System.Reflection.MdSigCallingConvention callingConvention1 = System.Reflection.MdSigCallingConvention.Default;
      if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
        callingConvention1 = System.Reflection.MdSigCallingConvention.Vararg;
      if (cGenericParam > 0)
        callingConvention1 |= System.Reflection.MdSigCallingConvention.Generic;
      if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
        callingConvention1 |= System.Reflection.MdSigCallingConvention.HasThis;
      SignatureHelper signatureHelper = new SignatureHelper(scope, callingConvention1, cGenericParam, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers);
      signatureHelper.AddArguments(parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
      return signatureHelper;
    }

    /// <summary>
    ///   Возвращает вспомогательный объект подписи для неуправляемого метода, получая на вход модуль метода, соглашение о вызовах и тип возвращаемого значения.
    /// </summary>
    /// <param name="mod">
    ///   <see cref="T:System.Reflection.Emit.ModuleBuilder" /> Содержащий метод, для которого <see langword="SignatureHelper" /> запрашивается.
    /// </param>
    /// <param name="unmanagedCallConv">
    ///   Неуправляемые соглашение о вызовах метода.
    /// </param>
    /// <param name="returnType">
    ///   Типом возвращаемого значения метода или <see langword="null" /> для типом возвращаемого значения void (<see langword="Sub" /> процедуры в Visual Basic).
    /// </param>
    /// <returns>
    ///   <see langword="SignatureHelper" /> Объекта для метода.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mod" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="mod" /> не является объектом <see cref="T:System.Reflection.Emit.ModuleBuilder" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="unmanagedCallConv" /> имеет неизвестный Неуправляемое соглашение о вызове.
    /// </exception>
    [SecuritySafeCritical]
    public static SignatureHelper GetMethodSigHelper(Module mod, CallingConvention unmanagedCallConv, Type returnType)
    {
      if (returnType == (Type) null)
        returnType = typeof (void);
      System.Reflection.MdSigCallingConvention callingConvention;
      switch (unmanagedCallConv)
      {
        case CallingConvention.Winapi:
        case CallingConvention.StdCall:
          callingConvention = System.Reflection.MdSigCallingConvention.StdCall;
          break;
        case CallingConvention.Cdecl:
          callingConvention = System.Reflection.MdSigCallingConvention.C;
          break;
        case CallingConvention.ThisCall:
          callingConvention = System.Reflection.MdSigCallingConvention.ThisCall;
          break;
        case CallingConvention.FastCall:
          callingConvention = System.Reflection.MdSigCallingConvention.FastCall;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_UnknownUnmanagedCallConv"), nameof (unmanagedCallConv));
      }
      return new SignatureHelper(mod, callingConvention, returnType, (Type[]) null, (Type[]) null);
    }

    /// <summary>
    ///   Возвращает вспомогательный объект подписи для локальной переменной.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.SignatureHelper" /> для локальной переменной.
    /// </returns>
    public static SignatureHelper GetLocalVarSigHelper()
    {
      return SignatureHelper.GetLocalVarSigHelper((Module) null);
    }

    /// <summary>
    ///   Возвращает вспомогательный объект подписи для метода, получая метода соглашение о вызовах и тип возвращаемого значения.
    /// </summary>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах метода.
    /// </param>
    /// <param name="returnType">
    ///   Типом возвращаемого значения метода или <see langword="null" /> для типом возвращаемого значения void (<see langword="Sub" /> процедуры в Visual Basic).
    /// </param>
    /// <returns>
    ///   <see langword="SignatureHelper" /> Объекта для метода.
    /// </returns>
    public static SignatureHelper GetMethodSigHelper(CallingConventions callingConvention, Type returnType)
    {
      return SignatureHelper.GetMethodSigHelper((Module) null, callingConvention, returnType);
    }

    /// <summary>
    ///   Возвращает вспомогательный объект подписи для метода, получая метод неуправляемых вызовах соглашение и типом возвращаемого значения.
    /// </summary>
    /// <param name="unmanagedCallingConvention">
    ///   Неуправляемые соглашение о вызовах метода.
    /// </param>
    /// <param name="returnType">
    ///   Типом возвращаемого значения метода или <see langword="null" /> для типом возвращаемого значения void (<see langword="Sub" /> процедуры в Visual Basic).
    /// </param>
    /// <returns>
    ///   <see langword="SignatureHelper" /> Объекта для метода.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="unmanagedCallConv" /> имеет неизвестный Неуправляемое соглашение о вызове.
    /// </exception>
    public static SignatureHelper GetMethodSigHelper(CallingConvention unmanagedCallingConvention, Type returnType)
    {
      return SignatureHelper.GetMethodSigHelper((Module) null, unmanagedCallingConvention, returnType);
    }

    /// <summary>
    ///   Возвращает вспомогательный объект подписи для локальной переменной.
    /// </summary>
    /// <param name="mod">
    ///   Динамический модуль, который содержит локальную переменную, для которой <see langword="SignatureHelper" /> запрашивается.
    /// </param>
    /// <returns>
    ///   <see langword="SignatureHelper" /> Объект для локальной переменной.
    /// </returns>
    public static SignatureHelper GetLocalVarSigHelper(Module mod)
    {
      return new SignatureHelper(mod, System.Reflection.MdSigCallingConvention.LocalSig);
    }

    /// <summary>Возвращает вспомогательный объект подписи для поля.</summary>
    /// <param name="mod">
    ///   Динамический модуль, который содержит поле, для которого <see langword="SignatureHelper" /> запрашивается.
    /// </param>
    /// <returns>
    ///   <see langword="SignatureHelper" /> Объект для поля.
    /// </returns>
    public static SignatureHelper GetFieldSigHelper(Module mod)
    {
      return new SignatureHelper(mod, System.Reflection.MdSigCallingConvention.Field);
    }

    /// <summary>
    ///   Возвращает вспомогательный объект подписи для свойства, получая на вход модуль, содержащий свойство, тип свойства и аргументы свойства.
    /// </summary>
    /// <param name="mod">
    ///   <see cref="T:System.Reflection.Emit.ModuleBuilder" /> Содержащий свойство, для которого <see cref="T:System.Reflection.Emit.SignatureHelper" /> запрашивается.
    /// </param>
    /// <param name="returnType">Тип свойства.</param>
    /// <param name="parameterTypes">
    ///   Типы аргументов или <see langword="null" /> Если свойство не имеет аргументов.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.SignatureHelper" /> объект свойства.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Элемент <paramref name="parameterTypes" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="mod" /> не является объектом <see cref="T:System.Reflection.Emit.ModuleBuilder" />.
    /// </exception>
    public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, Type[] parameterTypes)
    {
      return SignatureHelper.GetPropertySigHelper(mod, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>
    ///   Возвращает вспомогательный объект подписи для свойства, получая на вход модуль, содержащий свойство, тип свойства, аргументы свойства и пользовательские модификаторы для возвращаемого типа и аргументов.
    /// </summary>
    /// <param name="mod">
    ///   <see cref="T:System.Reflection.Emit.ModuleBuilder" /> Содержащий свойство, для которого <see cref="T:System.Reflection.Emit.SignatureHelper" /> запрашивается.
    /// </param>
    /// <param name="returnType">Тип свойства.</param>
    /// <param name="requiredReturnTypeCustomModifiers">
    ///   Массив типов, представляющих обязательные настраиваемые модификаторы для возвращаемого типа, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsBoxed" />.
    ///    Если возвращаемый тип не содержит требуемых настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="optionalReturnTypeCustomModifiers">
    ///   Массив типов, представляющих необязательные настраиваемые модификаторы для возвращаемого типа, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsBoxed" />.
    ///    Если возвращаемый тип не содержит необязательных настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="parameterTypes">
    ///   Типы аргументов свойств или <see langword="null" /> Если свойство не имеет аргументов.
    /// </param>
    /// <param name="requiredParameterTypeCustomModifiers">
    ///   Массив массивов типов.
    ///    Каждый массив типов представляет обязательные настраиваемые модификаторы для соответствующего свойства.
    ///    Если определенный аргумент не содержит требуемые пользовательские модификаторы, укажите <see langword="null" /> вместо массива типов.
    ///    Если свойство не содержит аргументов или если аргументы не содержат пользовательские модификаторы, укажите <see langword="null" /> вместо массива массивов.
    /// </param>
    /// <param name="optionalParameterTypeCustomModifiers">
    ///   Массив массивов типов.
    ///    Каждый массив типов представляет необязательные настраиваемые модификаторы для соответствующего свойства.
    ///    Если определенный аргумент не содержит необязательные пользовательские модификаторы, укажите <see langword="null" /> вместо массива типов.
    ///    Если свойство не содержит аргументов или если аргументы не содержат необязательные пользовательские модификаторы, укажите <see langword="null" /> вместо массива массивов.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.SignatureHelper" /> объект свойства.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Элемент <paramref name="parameterTypes" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из указанных пользовательских модификаторов является <see langword="null" />.
    ///    (Однако <see langword="null" /> может быть указано для массива пользовательских модификаторов для любого аргумента.)
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Подпись уже была закончена.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="mod" /> не является объектом <see cref="T:System.Reflection.Emit.ModuleBuilder" />.
    /// 
    ///   -или-
    /// 
    ///   Один из указанных пользовательских модификаторов является типом массива.
    /// 
    ///   -или-
    /// 
    ///   Один из указанных пользовательских модификаторов является открытым универсальным типом.
    ///    То есть <see cref="P:System.Type.ContainsGenericParameters" /> свойство <see langword="true" /> для пользовательские модификаторы.
    /// 
    ///   -или-
    /// 
    ///   Размер <paramref name="requiredParameterTypeCustomModifiers" /> или <paramref name="optionalParameterTypeCustomModifiers" /> не равна размеру <paramref name="parameterTypes" />.
    /// </exception>
    public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      return SignatureHelper.GetPropertySigHelper(mod, (CallingConventions) 0, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
    }

    /// <summary>
    ///   Возвращает вспомогательный объект подписи для свойства, получая на вход модуль, содержащий свойство, соглашение о вызовах, тип свойства, аргументы свойства и пользовательские модификаторы для возвращаемого типа и аргументов.
    /// </summary>
    /// <param name="mod">
    ///   <see cref="T:System.Reflection.Emit.ModuleBuilder" /> Содержащий свойство, для которого <see cref="T:System.Reflection.Emit.SignatureHelper" /> запрашивается.
    /// </param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах методов доступа свойства.
    /// </param>
    /// <param name="returnType">Тип свойства.</param>
    /// <param name="requiredReturnTypeCustomModifiers">
    ///   Массив типов, представляющих обязательные настраиваемые модификаторы для возвращаемого типа, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsBoxed" />.
    ///    Если возвращаемый тип не содержит требуемых настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="optionalReturnTypeCustomModifiers">
    ///   Массив типов, представляющих необязательные настраиваемые модификаторы для возвращаемого типа, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsBoxed" />.
    ///    Если возвращаемый тип не содержит необязательных настраиваемых модификаторов, укажите <see langword="null" />.
    /// </param>
    /// <param name="parameterTypes">
    ///   Типы аргументов свойств или <see langword="null" /> Если свойство не имеет аргументов.
    /// </param>
    /// <param name="requiredParameterTypeCustomModifiers">
    ///   Массив массивов типов.
    ///    Каждый массив типов представляет обязательные настраиваемые модификаторы для соответствующего свойства.
    ///    Если определенный аргумент не содержит требуемые пользовательские модификаторы, укажите <see langword="null" /> вместо массива типов.
    ///    Если свойство не содержит аргументов или если аргументы не содержат пользовательские модификаторы, укажите <see langword="null" /> вместо массива массивов.
    /// </param>
    /// <param name="optionalParameterTypeCustomModifiers">
    ///   Массив массивов типов.
    ///    Каждый массив типов представляет необязательные настраиваемые модификаторы для соответствующего свойства.
    ///    Если определенный аргумент не содержит необязательные пользовательские модификаторы, укажите <see langword="null" /> вместо массива типов.
    ///    Если свойство не содержит аргументов или если аргументы не содержат необязательные пользовательские модификаторы, укажите <see langword="null" /> вместо массива массивов.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.SignatureHelper" /> объект свойства.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Элемент <paramref name="parameterTypes" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из указанных пользовательских модификаторов является <see langword="null" />.
    ///    (Однако <see langword="null" /> может быть указано для массива пользовательских модификаторов для любого аргумента.)
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Подпись уже была закончена.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="mod" /> не является объектом <see cref="T:System.Reflection.Emit.ModuleBuilder" />.
    /// 
    ///   -или-
    /// 
    ///   Один из указанных пользовательских модификаторов является типом массива.
    /// 
    ///   -или-
    /// 
    ///   Один из указанных пользовательских модификаторов является открытым универсальным типом.
    ///    То есть <see cref="P:System.Type.ContainsGenericParameters" /> свойство <see langword="true" /> для пользовательские модификаторы.
    /// 
    ///   -или-
    /// 
    ///   Размер <paramref name="requiredParameterTypeCustomModifiers" /> или <paramref name="optionalParameterTypeCustomModifiers" /> не равна размеру <paramref name="parameterTypes" />.
    /// </exception>
    [SecuritySafeCritical]
    public static SignatureHelper GetPropertySigHelper(Module mod, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      if (returnType == (Type) null)
        returnType = typeof (void);
      System.Reflection.MdSigCallingConvention callingConvention1 = System.Reflection.MdSigCallingConvention.Property;
      if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
        callingConvention1 |= System.Reflection.MdSigCallingConvention.HasThis;
      SignatureHelper signatureHelper = new SignatureHelper(mod, callingConvention1, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers);
      signatureHelper.AddArguments(parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
      return signatureHelper;
    }

    [SecurityCritical]
    internal static SignatureHelper GetTypeSigToken(Module mod, Type type)
    {
      if (mod == (Module) null)
        throw new ArgumentNullException("module");
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      return new SignatureHelper(mod, type);
    }

    private SignatureHelper(Module mod, System.Reflection.MdSigCallingConvention callingConvention)
    {
      this.Init(mod, callingConvention);
    }

    [SecurityCritical]
    private SignatureHelper(Module mod, System.Reflection.MdSigCallingConvention callingConvention, int cGenericParameters, Type returnType, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
    {
      this.Init(mod, callingConvention, cGenericParameters);
      if (callingConvention == System.Reflection.MdSigCallingConvention.Field)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadFieldSig"));
      this.AddOneArgTypeHelper(returnType, requiredCustomModifiers, optionalCustomModifiers);
    }

    [SecurityCritical]
    private SignatureHelper(Module mod, System.Reflection.MdSigCallingConvention callingConvention, Type returnType, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
      : this(mod, callingConvention, 0, returnType, requiredCustomModifiers, optionalCustomModifiers)
    {
    }

    [SecurityCritical]
    private SignatureHelper(Module mod, Type type)
    {
      this.Init(mod);
      this.AddOneArgTypeHelper(type);
    }

    private void Init(Module mod)
    {
      this.m_signature = new byte[32];
      this.m_currSig = 0;
      this.m_module = mod as ModuleBuilder;
      this.m_argCount = 0;
      this.m_sigDone = false;
      this.m_sizeLoc = -1;
      if ((Module) this.m_module == (Module) null && mod != (Module) null)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_MustBeModuleBuilder"));
    }

    private void Init(Module mod, System.Reflection.MdSigCallingConvention callingConvention)
    {
      this.Init(mod, callingConvention, 0);
    }

    private void Init(Module mod, System.Reflection.MdSigCallingConvention callingConvention, int cGenericParam)
    {
      this.Init(mod);
      this.AddData((int) callingConvention);
      if (callingConvention == System.Reflection.MdSigCallingConvention.Field || callingConvention == System.Reflection.MdSigCallingConvention.GenericInst)
      {
        this.m_sizeLoc = -1;
      }
      else
      {
        if (cGenericParam > 0)
          this.AddData(cGenericParam);
        this.m_sizeLoc = this.m_currSig++;
      }
    }

    [SecurityCritical]
    private void AddOneArgTypeHelper(Type argument, bool pinned)
    {
      if (pinned)
        this.AddElementType(CorElementType.Pinned);
      this.AddOneArgTypeHelper(argument);
    }

    [SecurityCritical]
    private void AddOneArgTypeHelper(Type clsArgument, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
    {
      TypeToken typeToken;
      if (optionalCustomModifiers != null)
      {
        for (int index = 0; index < optionalCustomModifiers.Length; ++index)
        {
          Type optionalCustomModifier = optionalCustomModifiers[index];
          if (optionalCustomModifier == (Type) null)
            throw new ArgumentNullException(nameof (optionalCustomModifiers));
          if (optionalCustomModifier.HasElementType)
            throw new ArgumentException(Environment.GetResourceString("Argument_ArraysInvalid"), nameof (optionalCustomModifiers));
          if (optionalCustomModifier.ContainsGenericParameters)
            throw new ArgumentException(Environment.GetResourceString("Argument_GenericsInvalid"), nameof (optionalCustomModifiers));
          this.AddElementType(CorElementType.CModOpt);
          typeToken = this.m_module.GetTypeToken(optionalCustomModifier);
          this.AddToken(typeToken.Token);
        }
      }
      if (requiredCustomModifiers != null)
      {
        for (int index = 0; index < requiredCustomModifiers.Length; ++index)
        {
          Type requiredCustomModifier = requiredCustomModifiers[index];
          if (requiredCustomModifier == (Type) null)
            throw new ArgumentNullException(nameof (requiredCustomModifiers));
          if (requiredCustomModifier.HasElementType)
            throw new ArgumentException(Environment.GetResourceString("Argument_ArraysInvalid"), nameof (requiredCustomModifiers));
          if (requiredCustomModifier.ContainsGenericParameters)
            throw new ArgumentException(Environment.GetResourceString("Argument_GenericsInvalid"), nameof (requiredCustomModifiers));
          this.AddElementType(CorElementType.CModReqd);
          typeToken = this.m_module.GetTypeToken(requiredCustomModifier);
          this.AddToken(typeToken.Token);
        }
      }
      this.AddOneArgTypeHelper(clsArgument);
    }

    [SecurityCritical]
    private void AddOneArgTypeHelper(Type clsArgument)
    {
      this.AddOneArgTypeHelperWorker(clsArgument, false);
    }

    [SecurityCritical]
    private void AddOneArgTypeHelperWorker(Type clsArgument, bool lastWasGenericInst)
    {
      if (clsArgument.IsGenericParameter)
      {
        if (clsArgument.DeclaringMethod != (MethodBase) null)
          this.AddElementType(CorElementType.MVar);
        else
          this.AddElementType(CorElementType.Var);
        this.AddData(clsArgument.GenericParameterPosition);
      }
      else if (clsArgument.IsGenericType && (!clsArgument.IsGenericTypeDefinition || !lastWasGenericInst))
      {
        this.AddElementType(CorElementType.GenericInst);
        this.AddOneArgTypeHelperWorker(clsArgument.GetGenericTypeDefinition(), true);
        Type[] genericArguments = clsArgument.GetGenericArguments();
        this.AddData(genericArguments.Length);
        foreach (Type clsArgument1 in genericArguments)
          this.AddOneArgTypeHelper(clsArgument1);
      }
      else if (clsArgument is TypeBuilder)
      {
        TypeBuilder typeBuilder = (TypeBuilder) clsArgument;
        TypeToken clsToken = !typeBuilder.Module.Equals((object) this.m_module) ? this.m_module.GetTypeToken(clsArgument) : typeBuilder.TypeToken;
        if (clsArgument.IsValueType)
          this.InternalAddTypeToken(clsToken, CorElementType.ValueType);
        else
          this.InternalAddTypeToken(clsToken, CorElementType.Class);
      }
      else if (clsArgument is EnumBuilder)
      {
        TypeBuilder typeBuilder = ((EnumBuilder) clsArgument).m_typeBuilder;
        TypeToken clsToken = !typeBuilder.Module.Equals((object) this.m_module) ? this.m_module.GetTypeToken(clsArgument) : typeBuilder.TypeToken;
        if (clsArgument.IsValueType)
          this.InternalAddTypeToken(clsToken, CorElementType.ValueType);
        else
          this.InternalAddTypeToken(clsToken, CorElementType.Class);
      }
      else if (clsArgument.IsByRef)
      {
        this.AddElementType(CorElementType.ByRef);
        clsArgument = clsArgument.GetElementType();
        this.AddOneArgTypeHelper(clsArgument);
      }
      else if (clsArgument.IsPointer)
      {
        this.AddElementType(CorElementType.Ptr);
        this.AddOneArgTypeHelper(clsArgument.GetElementType());
      }
      else if (clsArgument.IsArray)
      {
        if (clsArgument.IsSzArray)
        {
          this.AddElementType(CorElementType.SzArray);
          this.AddOneArgTypeHelper(clsArgument.GetElementType());
        }
        else
        {
          this.AddElementType(CorElementType.Array);
          this.AddOneArgTypeHelper(clsArgument.GetElementType());
          int arrayRank = clsArgument.GetArrayRank();
          this.AddData(arrayRank);
          this.AddData(0);
          this.AddData(arrayRank);
          for (int index = 0; index < arrayRank; ++index)
            this.AddData(0);
        }
      }
      else
      {
        CorElementType corElementType = CorElementType.Max;
        if ((object) (clsArgument as RuntimeType) != null)
        {
          corElementType = RuntimeTypeHandle.GetCorElementType((RuntimeType) clsArgument);
          if (corElementType == CorElementType.Class)
          {
            if (clsArgument == typeof (object))
              corElementType = CorElementType.Object;
            else if (clsArgument == typeof (string))
              corElementType = CorElementType.String;
          }
        }
        if (SignatureHelper.IsSimpleType(corElementType))
          this.AddElementType(corElementType);
        else if ((Module) this.m_module == (Module) null)
          this.InternalAddRuntimeType(clsArgument);
        else if (clsArgument.IsValueType)
          this.InternalAddTypeToken(this.m_module.GetTypeToken(clsArgument), CorElementType.ValueType);
        else
          this.InternalAddTypeToken(this.m_module.GetTypeToken(clsArgument), CorElementType.Class);
      }
    }

    private void AddData(int data)
    {
      if (this.m_currSig + 4 > this.m_signature.Length)
        this.m_signature = this.ExpandArray(this.m_signature);
      if (data <= (int) sbyte.MaxValue)
        this.m_signature[this.m_currSig++] = (byte) (data & (int) byte.MaxValue);
      else if (data <= 16383)
      {
        this.m_signature[this.m_currSig++] = (byte) (data >> 8 | 128);
        this.m_signature[this.m_currSig++] = (byte) (data & (int) byte.MaxValue);
      }
      else
      {
        if (data > 536870911)
          throw new ArgumentException(Environment.GetResourceString("Argument_LargeInteger"));
        this.m_signature[this.m_currSig++] = (byte) (data >> 24 | 192);
        this.m_signature[this.m_currSig++] = (byte) (data >> 16 & (int) byte.MaxValue);
        this.m_signature[this.m_currSig++] = (byte) (data >> 8 & (int) byte.MaxValue);
        this.m_signature[this.m_currSig++] = (byte) (data & (int) byte.MaxValue);
      }
    }

    private void AddData(uint data)
    {
      if (this.m_currSig + 4 > this.m_signature.Length)
        this.m_signature = this.ExpandArray(this.m_signature);
      this.m_signature[this.m_currSig++] = (byte) (data & (uint) byte.MaxValue);
      this.m_signature[this.m_currSig++] = (byte) (data >> 8 & (uint) byte.MaxValue);
      this.m_signature[this.m_currSig++] = (byte) (data >> 16 & (uint) byte.MaxValue);
      this.m_signature[this.m_currSig++] = (byte) (data >> 24 & (uint) byte.MaxValue);
    }

    private void AddData(ulong data)
    {
      if (this.m_currSig + 8 > this.m_signature.Length)
        this.m_signature = this.ExpandArray(this.m_signature);
      this.m_signature[this.m_currSig++] = (byte) (data & (ulong) byte.MaxValue);
      this.m_signature[this.m_currSig++] = (byte) (data >> 8 & (ulong) byte.MaxValue);
      this.m_signature[this.m_currSig++] = (byte) (data >> 16 & (ulong) byte.MaxValue);
      this.m_signature[this.m_currSig++] = (byte) (data >> 24 & (ulong) byte.MaxValue);
      this.m_signature[this.m_currSig++] = (byte) (data >> 32 & (ulong) byte.MaxValue);
      this.m_signature[this.m_currSig++] = (byte) (data >> 40 & (ulong) byte.MaxValue);
      this.m_signature[this.m_currSig++] = (byte) (data >> 48 & (ulong) byte.MaxValue);
      this.m_signature[this.m_currSig++] = (byte) (data >> 56 & (ulong) byte.MaxValue);
    }

    private void AddElementType(CorElementType cvt)
    {
      if (this.m_currSig + 1 > this.m_signature.Length)
        this.m_signature = this.ExpandArray(this.m_signature);
      this.m_signature[this.m_currSig++] = (byte) cvt;
    }

    private void AddToken(int token)
    {
      int num = token & 16777215;
      MetadataTokenType metadataTokenType = (MetadataTokenType) (token & -16777216);
      if (num > 67108863)
        throw new ArgumentException(Environment.GetResourceString("Argument_LargeInteger"));
      int data = num << 2;
      switch (metadataTokenType)
      {
        case MetadataTokenType.TypeRef:
          data |= 1;
          break;
        case MetadataTokenType.TypeSpec:
          data |= 2;
          break;
      }
      this.AddData(data);
    }

    private void InternalAddTypeToken(TypeToken clsToken, CorElementType CorType)
    {
      this.AddElementType(CorType);
      this.AddToken(clsToken.Token);
    }

    [SecurityCritical]
    private unsafe void InternalAddRuntimeType(Type type)
    {
      this.AddElementType(CorElementType.Internal);
      IntPtr num = type.GetTypeHandleInternal().Value;
      if (this.m_currSig + sizeof (void*) > this.m_signature.Length)
        this.m_signature = this.ExpandArray(this.m_signature);
      byte* numPtr = (byte*) &num;
      for (int index = 0; index < sizeof (void*); ++index)
        this.m_signature[this.m_currSig++] = numPtr[index];
    }

    private byte[] ExpandArray(byte[] inArray)
    {
      return this.ExpandArray(inArray, inArray.Length * 2);
    }

    private byte[] ExpandArray(byte[] inArray, int requiredLength)
    {
      if (requiredLength < inArray.Length)
        requiredLength = inArray.Length * 2;
      byte[] numArray = new byte[requiredLength];
      Array.Copy((Array) inArray, (Array) numArray, inArray.Length);
      return numArray;
    }

    private void IncrementArgCounts()
    {
      if (this.m_sizeLoc == -1)
        return;
      ++this.m_argCount;
    }

    private void SetNumberOfSignatureElements(bool forceCopy)
    {
      int currSig = this.m_currSig;
      if (this.m_sizeLoc == -1)
        return;
      if (this.m_argCount < 128 && !forceCopy)
      {
        this.m_signature[this.m_sizeLoc] = (byte) this.m_argCount;
      }
      else
      {
        int num = this.m_argCount >= 128 ? (this.m_argCount >= 16384 ? 4 : 2) : 1;
        byte[] numArray = new byte[this.m_currSig + num - 1];
        numArray[0] = this.m_signature[0];
        Array.Copy((Array) this.m_signature, this.m_sizeLoc + 1, (Array) numArray, this.m_sizeLoc + num, currSig - (this.m_sizeLoc + 1));
        this.m_signature = numArray;
        this.m_currSig = this.m_sizeLoc;
        this.AddData(this.m_argCount);
        this.m_currSig = currSig + (num - 1);
      }
    }

    internal int ArgumentCount
    {
      get
      {
        return this.m_argCount;
      }
    }

    internal static bool IsSimpleType(CorElementType type)
    {
      return type <= CorElementType.String || type == CorElementType.TypedByRef || (type == CorElementType.I || type == CorElementType.U) || type == CorElementType.Object;
    }

    internal byte[] InternalGetSignature(out int length)
    {
      if (!this.m_sigDone)
      {
        this.m_sigDone = true;
        this.SetNumberOfSignatureElements(false);
      }
      length = this.m_currSig;
      return this.m_signature;
    }

    internal byte[] InternalGetSignatureArray()
    {
      int argCount = this.m_argCount;
      int currSig = this.m_currSig;
      int num1 = currSig;
      int length = argCount >= (int) sbyte.MaxValue ? (argCount >= 16383 ? num1 + 4 : num1 + 2) : num1 + 1;
      byte[] numArray1 = new byte[length];
      int num2 = 0;
      byte[] numArray2 = numArray1;
      int index1 = num2;
      int num3 = index1 + 1;
      int num4 = (int) this.m_signature[0];
      numArray2[index1] = (byte) num4;
      int destinationIndex;
      if (argCount <= (int) sbyte.MaxValue)
      {
        byte[] numArray3 = numArray1;
        int index2 = num3;
        destinationIndex = index2 + 1;
        int num5 = (int) (byte) (argCount & (int) byte.MaxValue);
        numArray3[index2] = (byte) num5;
      }
      else if (argCount <= 16383)
      {
        byte[] numArray3 = numArray1;
        int index2 = num3;
        int num5 = index2 + 1;
        int num6 = (int) (byte) (argCount >> 8 | 128);
        numArray3[index2] = (byte) num6;
        byte[] numArray4 = numArray1;
        int index3 = num5;
        destinationIndex = index3 + 1;
        int num7 = (int) (byte) (argCount & (int) byte.MaxValue);
        numArray4[index3] = (byte) num7;
      }
      else
      {
        if (argCount > 536870911)
          throw new ArgumentException(Environment.GetResourceString("Argument_LargeInteger"));
        byte[] numArray3 = numArray1;
        int index2 = num3;
        int num5 = index2 + 1;
        int num6 = (int) (byte) (argCount >> 24 | 192);
        numArray3[index2] = (byte) num6;
        byte[] numArray4 = numArray1;
        int index3 = num5;
        int num7 = index3 + 1;
        int num8 = (int) (byte) (argCount >> 16 & (int) byte.MaxValue);
        numArray4[index3] = (byte) num8;
        byte[] numArray5 = numArray1;
        int index4 = num7;
        int num9 = index4 + 1;
        int num10 = (int) (byte) (argCount >> 8 & (int) byte.MaxValue);
        numArray5[index4] = (byte) num10;
        byte[] numArray6 = numArray1;
        int index5 = num9;
        destinationIndex = index5 + 1;
        int num11 = (int) (byte) (argCount & (int) byte.MaxValue);
        numArray6[index5] = (byte) num11;
      }
      Array.Copy((Array) this.m_signature, 2, (Array) numArray1, destinationIndex, currSig - 2);
      numArray1[length - 1] = (byte) 0;
      return numArray1;
    }

    /// <summary>Добавляет аргумент к подписи.</summary>
    /// <param name="clsArgument">Тип аргумента.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Подпись уже была закончена.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="clsArgument" /> имеет значение <see langword="null" />.
    /// </exception>
    public void AddArgument(Type clsArgument)
    {
      this.AddArgument(clsArgument, (Type[]) null, (Type[]) null);
    }

    /// <summary>
    ///   Добавляет аргумент указанного типа к подписи, определяя, закреплен ли аргумент.
    /// </summary>
    /// <param name="argument">Тип аргумента.</param>
    /// <param name="pinned">
    ///   <see langword="true" /> Если аргумент закреплен; в противном случае — <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="argument" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public void AddArgument(Type argument, bool pinned)
    {
      if (argument == (Type) null)
        throw new ArgumentNullException(nameof (argument));
      this.IncrementArgCounts();
      this.AddOneArgTypeHelper(argument, pinned);
    }

    /// <summary>
    ///   Добавляет набор аргументов к подписи с указанными пользовательскими модификаторами.
    /// </summary>
    /// <param name="arguments">
    ///   Типы аргументов должны быть добавлены.
    /// </param>
    /// <param name="requiredCustomModifiers">
    ///   Массив массивов типов.
    ///    Каждый массив типов представляет обязательные настраиваемые модификаторы для соответствующего аргумента, например <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsBoxed" />.
    ///    Если определенный аргумент не содержит требуемые пользовательские модификаторы, укажите <see langword="null" /> вместо массива типов.
    ///    Если ни один из аргументов не содержит требуемые пользовательские модификаторы, укажите <see langword="null" /> вместо массива массивов.
    /// </param>
    /// <param name="optionalCustomModifiers">
    ///   Массив массивов типов.
    ///    Каждый массив типов представляет необязательные настраиваемые модификаторы для соответствующего аргумента, например <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsBoxed" />.
    ///    Если определенный аргумент не содержит необязательные пользовательские модификаторы, укажите <see langword="null" /> вместо массива типов.
    ///    Если аргументы не содержат необязательные пользовательские модификаторы, укажите <see langword="null" /> вместо массива массивов.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Элемент <paramref name="arguments" /> — <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из указанных пользовательских модификаторов является <see langword="null" />.
    ///    (Однако <see langword="null" /> может быть указано для массива пользовательских модификаторов для любого аргумента.)
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Подпись уже была закончена.
    /// 
    ///   -или-
    /// 
    ///   Один из указанных пользовательских модификаторов является типом массива.
    /// 
    ///   -или-
    /// 
    ///   Один из указанных пользовательских модификаторов является открытым универсальным типом.
    ///    То есть <see cref="P:System.Type.ContainsGenericParameters" /> свойство <see langword="true" /> для пользовательские модификаторы.
    /// 
    ///   -или-
    /// 
    ///   Размер <paramref name="requiredCustomModifiers" /> или <paramref name="optionalCustomModifiers" /> не равна размеру <paramref name="arguments" />.
    /// </exception>
    public void AddArguments(Type[] arguments, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
    {
      if (requiredCustomModifiers != null && (arguments == null || requiredCustomModifiers.Length != arguments.Length))
        throw new ArgumentException(Environment.GetResourceString("Argument_MismatchedArrays", (object) nameof (requiredCustomModifiers), (object) nameof (arguments)));
      if (optionalCustomModifiers != null && (arguments == null || optionalCustomModifiers.Length != arguments.Length))
        throw new ArgumentException(Environment.GetResourceString("Argument_MismatchedArrays", (object) nameof (optionalCustomModifiers), (object) nameof (arguments)));
      if (arguments == null)
        return;
      for (int index = 0; index < arguments.Length; ++index)
        this.AddArgument(arguments[index], requiredCustomModifiers == null ? (Type[]) null : requiredCustomModifiers[index], optionalCustomModifiers == null ? (Type[]) null : optionalCustomModifiers[index]);
    }

    /// <summary>
    ///   Добавляет аргумент к подписи с указанными пользовательскими модификаторами.
    /// </summary>
    /// <param name="argument">Тип аргумента.</param>
    /// <param name="requiredCustomModifiers">
    ///   Массив типов, представляющих обязательные настраиваемые модификаторы для аргумента, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsBoxed" />.
    ///    Если аргумент не содержит требуемые пользовательские модификаторы, укажите <see langword="null" />.
    /// </param>
    /// <param name="optionalCustomModifiers">
    ///   Массив типов, представляющих необязательные настраиваемые модификаторы для аргумента, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsBoxed" />.
    ///    Если аргумент не содержит необязательные пользовательские модификаторы, укажите <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="argument" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Элемент <paramref name="requiredCustomModifiers" /> или <paramref name="optionalCustomModifiers" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Подпись уже была закончена.
    /// 
    ///   -или-
    /// 
    ///   Один из указанных пользовательских модификаторов является типом массива.
    /// 
    ///   -или-
    /// 
    ///   Один из указанных пользовательских модификаторов является открытым универсальным типом.
    ///    То есть <see cref="P:System.Type.ContainsGenericParameters" /> свойство <see langword="true" /> для пользовательские модификаторы.
    /// </exception>
    [SecuritySafeCritical]
    public void AddArgument(Type argument, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
    {
      if (this.m_sigDone)
        throw new ArgumentException(Environment.GetResourceString("Argument_SigIsFinalized"));
      if (argument == (Type) null)
        throw new ArgumentNullException(nameof (argument));
      this.IncrementArgCounts();
      this.AddOneArgTypeHelper(argument, requiredCustomModifiers, optionalCustomModifiers);
    }

    /// <summary>
    ///   Метки в конце vararg фиксированной части.
    ///    Используется, только если вызывающий оператор создает веб-узел вызова подписи с переменным количеством аргументов.
    /// </summary>
    public void AddSentinel()
    {
      this.AddElementType(CorElementType.Sentinel);
    }

    /// <summary>
    ///   Проверяет, является ли этот экземпляр равен данному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, с которым нужно сравнить данный экземпляр.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если данный объект <see langword="SignatureHelper" /> и представляет ту же сигнатуру; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (!(obj is SignatureHelper))
        return false;
      SignatureHelper signatureHelper = (SignatureHelper) obj;
      if (!signatureHelper.m_module.Equals((object) this.m_module) || signatureHelper.m_currSig != this.m_currSig || (signatureHelper.m_sizeLoc != this.m_sizeLoc || signatureHelper.m_sigDone != this.m_sigDone))
        return false;
      for (int index = 0; index < this.m_currSig; ++index)
      {
        if ((int) this.m_signature[index] != (int) signatureHelper.m_signature[index])
          return false;
      }
      return true;
    }

    /// <summary>
    ///   Создает и возвращает хэш-код для данного экземпляра.
    /// </summary>
    /// <returns>Возвращает хэш-код на основе имени.</returns>
    public override int GetHashCode()
    {
      int num = this.m_module.GetHashCode() + this.m_currSig + this.m_sizeLoc;
      if (this.m_sigDone)
        ++num;
      for (int index = 0; index < this.m_currSig; ++index)
        num += this.m_signature[index].GetHashCode();
      return num;
    }

    /// <summary>
    ///   Добавляет к подписи конечный маркер и помечает подпись как законченную, поэтому больше никакие маркеры не могут быть добавлены.
    /// </summary>
    /// <returns>
    ///   Возвращает массив байтов, содержащий подпись целиком.
    /// </returns>
    public byte[] GetSignature()
    {
      return this.GetSignature(false);
    }

    internal byte[] GetSignature(bool appendEndOfSig)
    {
      if (!this.m_sigDone)
      {
        if (appendEndOfSig)
          this.AddElementType(CorElementType.End);
        this.SetNumberOfSignatureElements(true);
        this.m_sigDone = true;
      }
      if (this.m_signature.Length > this.m_currSig)
      {
        byte[] numArray = new byte[this.m_currSig];
        Array.Copy((Array) this.m_signature, (Array) numArray, this.m_currSig);
        this.m_signature = numArray;
      }
      return this.m_signature;
    }

    /// <summary>
    ///   Возвращает строку, представляющую аргументы подписи.
    /// </summary>
    /// <returns>
    ///   Возвращает строку, представляющую аргументы этой подписи.
    /// </returns>
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("Length: " + (object) this.m_currSig + Environment.NewLine);
      if (this.m_sizeLoc != -1)
        stringBuilder.Append("Arguments: " + (object) this.m_signature[this.m_sizeLoc] + Environment.NewLine);
      else
        stringBuilder.Append("Field Signature" + Environment.NewLine);
      stringBuilder.Append("Signature: " + Environment.NewLine);
      for (int index = 0; index <= this.m_currSig; ++index)
        stringBuilder.Append(((int) this.m_signature[index]).ToString() + "  ");
      stringBuilder.Append(Environment.NewLine);
      return stringBuilder.ToString();
    }

    void _SignatureHelper.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _SignatureHelper.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _SignatureHelper.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _SignatureHelper.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
