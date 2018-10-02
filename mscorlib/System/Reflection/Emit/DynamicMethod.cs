// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.DynamicMethod
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Определяет и представляет динамический метод, который можно скомпилировать, выполнить и удалить.
  ///    Удаленные методы доступны для сборки мусора.
  /// </summary>
  [ComVisible(true)]
  public sealed class DynamicMethod : MethodInfo
  {
    private static readonly object s_anonymouslyHostedDynamicMethodsModuleLock = new object();
    private RuntimeType[] m_parameterTypes;
    internal IRuntimeMethodInfo m_methodHandle;
    private RuntimeType m_returnType;
    private DynamicILGenerator m_ilGenerator;
    private DynamicILInfo m_DynamicILInfo;
    private bool m_fInitLocals;
    private RuntimeModule m_module;
    internal bool m_skipVisibility;
    internal RuntimeType m_typeOwner;
    private DynamicMethod.RTDynamicMethod m_dynMethod;
    internal DynamicResolver m_resolver;
    private bool m_profileAPICheck;
    private RuntimeAssembly m_creatorAssembly;
    internal bool m_restrictedSkipVisibility;
    internal CompressedStack m_creationContext;
    private static volatile InternalModuleBuilder s_anonymouslyHostedDynamicMethodsModule;

    private DynamicMethod()
    {
    }

    /// <summary>
    ///   Инициализирует анонимно размещенный динамический метод, указывая имя метода, возвращаемый тип и типы параметров.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического метода.
    ///    Это может быть строка нулевой длины, но не <see langword="null" />.
    /// </param>
    /// <param name="returnType">
    ///   Объект <see cref="T:System.Type" />, который указывает возвращаемый тип динамического метода, или значение <see langword="null" />, если метод не имеет возвращаемого типа.
    /// </param>
    /// <param name="parameterTypes">
    ///   Массив объектов <see cref="T:System.Type" />, указывающих типы параметров динамического метода, или значение <see langword="null" />, если метод не имеет параметров.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент <paramref name="parameterTypes" /> имеет значение <see langword="null" /> или <see cref="T:System.Void" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="returnType" /> — это тип, для которого <see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, Type returnType, Type[] parameterTypes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.Init(name, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, (Type) null, (Module) null, false, true, ref stackMark);
    }

    /// <summary>
    ///   Инициализирует анонимно размещенный динамический метод, указывая имя метода, возвращаемый тип, типы параметров и необходимость пропуска проверки видимости JIT для типов и членов, к которым получает доступ MSIL динамического метода.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического метода.
    ///    Это может быть строка нулевой длины, но не <see langword="null" />.
    /// </param>
    /// <param name="returnType">
    ///   Объект <see cref="T:System.Type" />, который указывает возвращаемый тип динамического метода, или значение <see langword="null" />, если метод не имеет возвращаемого типа.
    /// </param>
    /// <param name="parameterTypes">
    ///   Массив объектов <see cref="T:System.Type" />, указывающих типы параметров динамического метода, или значение <see langword="null" />, если метод не имеет параметров.
    /// </param>
    /// <param name="restrictedSkipVisibility">
    ///   <see langword="true" /> — пропускает проверки видимости JIT для типов и членов, к которым получает доступ MSIL динамического метода, с таким ограничением: уровень доверия сборок, содержащих эти типы и члены, должен быть равен уровню доверия стека вызовов, создающего динамический метод, или меньше его. В противном случае — <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент <paramref name="parameterTypes" /> имеет значение <see langword="null" /> или <see cref="T:System.Void" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="returnType" /> — это тип, для которого <see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, Type returnType, Type[] parameterTypes, bool restrictedSkipVisibility)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.Init(name, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, (Type) null, (Module) null, restrictedSkipVisibility, true, ref stackMark);
    }

    /// <summary>
    ///   Создает динамический метод, который является глобальным по отношению к модулю, используя имя метода, возвращаемый тип, типы параметров и модуль.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического метода.
    ///    Это может быть строка нулевой длины, но не <see langword="null" />.
    /// </param>
    /// <param name="returnType">
    ///   Объект <see cref="T:System.Type" />, который указывает возвращаемый тип динамического метода, или значение <see langword="null" />, если метод не имеет возвращаемого типа.
    /// </param>
    /// <param name="parameterTypes">
    ///   Массив объектов <see cref="T:System.Type" />, указывающих типы параметров динамического метода, или значение <see langword="null" />, если метод не имеет параметров.
    /// </param>
    /// <param name="m">
    ///   Объект <see cref="T:System.Reflection.Module" />, представляющий модуль, с которым должен быть логически связан динамический метод.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент <paramref name="parameterTypes" /> имеет значение <see langword="null" /> или <see cref="T:System.Void" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="m" /> — это модуль, предоставляющий анонимное размещение для динамических методов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="m" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="returnType" /> — это тип, для которого <see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Module m)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PerformSecurityCheck(m, ref stackMark, false);
      this.Init(name, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, (Type) null, m, false, false, ref stackMark);
    }

    /// <summary>
    ///   Создает динамический метод, который является глобальным для модуля, указывая имя метода, возвращаемый тип, типы параметров, модуль и необходимость пропуска проверки видимости JIT для типов и членов, к которым получает доступ MSIL динамического метода.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического метода.
    ///    Это может быть строка нулевой длины, но не <see langword="null" />.
    /// </param>
    /// <param name="returnType">
    ///   Объект <see cref="T:System.Type" />, который указывает возвращаемый тип динамического метода, или значение <see langword="null" />, если метод не имеет возвращаемого типа.
    /// </param>
    /// <param name="parameterTypes">
    ///   Массив объектов <see cref="T:System.Type" />, указывающих типы параметров динамического метода, или значение <see langword="null" />, если метод не имеет параметров.
    /// </param>
    /// <param name="m">
    ///   Объект <see cref="T:System.Reflection.Module" />, представляющий модуль, с которым должен быть логически связан динамический метод.
    /// </param>
    /// <param name="skipVisibility">
    ///   Значение <see langword="true" /> для пропуска проверки видимости JIT для типов и членов, к которым получает доступ MSIL динамического метода.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент <paramref name="parameterTypes" /> имеет значение <see langword="null" /> или <see cref="T:System.Void" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="m" /> — это модуль, предоставляющий анонимное размещение для динамических методов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="m" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="returnType" /> — это тип, для которого <see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Module m, bool skipVisibility)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PerformSecurityCheck(m, ref stackMark, skipVisibility);
      this.Init(name, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, (Type) null, m, skipVisibility, false, ref stackMark);
    }

    /// <summary>
    ///   Создает динамический метод, который является глобальным для модуля, указывая имя метода, атрибуты, соглашение о вызовах, возвращаемый тип, типы параметров, модуль и необходимость пропуска проверки видимости JIT для типов и членов, к которым получает доступ промежуточный язык Майкрософт (MSIL) динамического метода.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического метода.
    ///    Это может быть строка нулевой длины, но не <see langword="null" />.
    /// </param>
    /// <param name="attributes">
    ///   Битовая комбинация значений <see cref="T:System.Reflection.MethodAttributes" />, определяющая атрибуты динамического метода.
    ///    Разрешена только комбинация <see cref="F:System.Reflection.MethodAttributes.Public" /> и <see cref="F:System.Reflection.MethodAttributes.Static" />.
    /// </param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах для динамического метода.
    ///    Должно иметь значение <see cref="F:System.Reflection.CallingConventions.Standard" />.
    /// </param>
    /// <param name="returnType">
    ///   Объект <see cref="T:System.Type" />, который указывает возвращаемый тип динамического метода, или значение <see langword="null" />, если метод не имеет возвращаемого типа.
    /// </param>
    /// <param name="parameterTypes">
    ///   Массив объектов <see cref="T:System.Type" />, указывающих типы параметров динамического метода, или значение <see langword="null" />, если метод не имеет параметров.
    /// </param>
    /// <param name="m">
    ///   Объект <see cref="T:System.Reflection.Module" />, представляющий модуль, с которым должен быть логически связан динамический метод.
    /// </param>
    /// <param name="skipVisibility">
    ///   Значение <see langword="true" /> для пропуска проверки видимости JIT для типов и членов, к которым получает доступ MSIL динамического метода; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент <paramref name="parameterTypes" /> имеет значение <see langword="null" /> или <see cref="T:System.Void" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="m" /> — это модуль, предоставляющий анонимное размещение для динамических методов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="m" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="attributes" /> представляет собой сочетание флагов, отличных от <see cref="F:System.Reflection.MethodAttributes.Public" /> и <see cref="F:System.Reflection.MethodAttributes.Static" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="callingConvention" /> не является <see cref="F:System.Reflection.CallingConventions.Standard" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="returnType" /> — это тип, для которого <see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Module m, bool skipVisibility)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PerformSecurityCheck(m, ref stackMark, skipVisibility);
      this.Init(name, attributes, callingConvention, returnType, parameterTypes, (Type) null, m, skipVisibility, false, ref stackMark);
    }

    /// <summary>
    ///   Создает динамический метод, указывая имя метода, возвращаемый тип, типы параметров и тип, с которым логически связан динамический метод.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического метода.
    ///    Это может быть строка нулевой длины, но не <see langword="null" />.
    /// </param>
    /// <param name="returnType">
    ///   Объект <see cref="T:System.Type" />, который указывает возвращаемый тип динамического метода, или значение <see langword="null" />, если метод не имеет возвращаемого типа.
    /// </param>
    /// <param name="parameterTypes">
    ///   Массив объектов <see cref="T:System.Type" />, указывающих типы параметров динамического метода, или значение <see langword="null" />, если метод не имеет параметров.
    /// </param>
    /// <param name="owner">
    ///   Тип <see cref="T:System.Type" />, с которым логически связан динамический метод.
    ///    Динамический метод имеет доступ ко всем членам типа.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент <paramref name="parameterTypes" /> имеет значение <see langword="null" /> или <see cref="T:System.Void" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="owner" /> является интерфейсом, массивом, открытым универсальным типом или параметром универсального типа или метода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="owner" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="returnType" /> имеет значение <see langword="null" /> или является типом, для которого <see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PerformSecurityCheck(owner, ref stackMark, false);
      this.Init(name, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, (Module) null, false, false, ref stackMark);
    }

    /// <summary>
    ///   Создает динамический метод, указывая имя метода, возвращаемый тип, типы параметров, тип, с которым логически связан динамический метод, и необходимость пропуска проверки видимости JIT для типов и членов, к которым получает доступ MSIL динамического метода.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического метода.
    ///    Это может быть строка нулевой длины, но не <see langword="null" />.
    /// </param>
    /// <param name="returnType">
    ///   Объект <see cref="T:System.Type" />, который указывает возвращаемый тип динамического метода, или значение <see langword="null" />, если метод не имеет возвращаемого типа.
    /// </param>
    /// <param name="parameterTypes">
    ///   Массив объектов <see cref="T:System.Type" />, указывающих типы параметров динамического метода, или значение <see langword="null" />, если метод не имеет параметров.
    /// </param>
    /// <param name="owner">
    ///   Тип <see cref="T:System.Type" />, с которым логически связан динамический метод.
    ///    Динамический метод имеет доступ ко всем членам типа.
    /// </param>
    /// <param name="skipVisibility">
    ///   Значение <see langword="true" /> для пропуска проверки видимости JIT для типов и членов, к которым получает доступ MSIL динамического метода; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент <paramref name="parameterTypes" /> имеет значение <see langword="null" /> или <see cref="T:System.Void" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="owner" /> является интерфейсом, массивом, открытым универсальным типом или параметром универсального типа или метода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="owner" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="returnType" /> имеет значение <see langword="null" /> или является типом, для которого <see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PerformSecurityCheck(owner, ref stackMark, skipVisibility);
      this.Init(name, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, (Module) null, skipVisibility, false, ref stackMark);
    }

    /// <summary>
    ///   Создает динамический метод, указывая имя метода, атрибуты, соглашение о вызовах, возвращаемый тип, типы параметров, тип, с которым логически связан динамический метод, и необходимость пропуска проверки видимости JIT для типов и членов, к которым получает доступ промежуточный язык Майкрософт (MSIL) динамического метода.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического метода.
    ///    Это может быть строка нулевой длины, но не <see langword="null" />.
    /// </param>
    /// <param name="attributes">
    ///   Битовая комбинация значений <see cref="T:System.Reflection.MethodAttributes" />, определяющая атрибуты динамического метода.
    ///    Разрешена только комбинация <see cref="F:System.Reflection.MethodAttributes.Public" /> и <see cref="F:System.Reflection.MethodAttributes.Static" />.
    /// </param>
    /// <param name="callingConvention">
    ///   Соглашение о вызовах для динамического метода.
    ///    Должно иметь значение <see cref="F:System.Reflection.CallingConventions.Standard" />.
    /// </param>
    /// <param name="returnType">
    ///   Объект <see cref="T:System.Type" />, который указывает возвращаемый тип динамического метода, или значение <see langword="null" />, если метод не имеет возвращаемого типа.
    /// </param>
    /// <param name="parameterTypes">
    ///   Массив объектов <see cref="T:System.Type" />, указывающих типы параметров динамического метода, или значение <see langword="null" />, если метод не имеет параметров.
    /// </param>
    /// <param name="owner">
    ///   Тип <see cref="T:System.Type" />, с которым логически связан динамический метод.
    ///    Динамический метод имеет доступ ко всем членам типа.
    /// </param>
    /// <param name="skipVisibility">
    ///   Значение <see langword="true" /> для пропуска проверки видимости JIT для типов и членов, к которым получает доступ MSIL динамического метода; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Элемент <paramref name="parameterTypes" /> имеет значение <see langword="null" /> или <see cref="T:System.Void" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="owner" /> является интерфейсом, массивом, открытым универсальным типом или параметром универсального типа или метода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="owner" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="attributes" /> представляет собой сочетание флагов, отличных от <see cref="F:System.Reflection.MethodAttributes.Public" /> и <see cref="F:System.Reflection.MethodAttributes.Static" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="callingConvention" /> не является <see cref="F:System.Reflection.CallingConventions.Standard" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="returnType" /> — это тип, для которого <see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PerformSecurityCheck(owner, ref stackMark, skipVisibility);
      this.Init(name, attributes, callingConvention, returnType, parameterTypes, owner, (Module) null, skipVisibility, false, ref stackMark);
    }

    private static void CheckConsistency(MethodAttributes attributes, CallingConventions callingConvention)
    {
      if ((attributes & ~MethodAttributes.MemberAccessMask) != MethodAttributes.Static)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
      if ((attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
      if (callingConvention != CallingConventions.Standard && callingConvention != CallingConventions.VarArgs)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
      if (callingConvention == CallingConventions.VarArgs)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static RuntimeModule GetDynamicMethodsModule()
    {
      if ((Module) DynamicMethod.s_anonymouslyHostedDynamicMethodsModule != (Module) null)
        return (RuntimeModule) DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
      lock (DynamicMethod.s_anonymouslyHostedDynamicMethodsModuleLock)
      {
        if ((Module) DynamicMethod.s_anonymouslyHostedDynamicMethodsModule != (Module) null)
          return (RuntimeModule) DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
        CustomAttributeBuilder attributeBuilder1 = new CustomAttributeBuilder(typeof (SecurityTransparentAttribute).GetConstructor(Type.EmptyTypes), EmptyArray<object>.Value);
        List<CustomAttributeBuilder> attributeBuilderList = new List<CustomAttributeBuilder>();
        attributeBuilderList.Add(attributeBuilder1);
        CustomAttributeBuilder attributeBuilder2 = new CustomAttributeBuilder(typeof (SecurityRulesAttribute).GetConstructor(new Type[1]
        {
          typeof (SecurityRuleSet)
        }), new object[1]{ (object) SecurityRuleSet.Level1 });
        attributeBuilderList.Add(attributeBuilder2);
        AssemblyName name = new AssemblyName("Anonymously Hosted DynamicMethods Assembly");
        StackCrawlMark stackMark = StackCrawlMark.LookForMe;
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.InternalDefineDynamicAssembly(name, AssemblyBuilderAccess.Run, (string) null, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, (IEnumerable<CustomAttributeBuilder>) attributeBuilderList, SecurityContextSource.CurrentAssembly);
        AppDomain.PublishAnonymouslyHostedDynamicMethodsAssembly(assemblyBuilder.GetNativeHandle());
        DynamicMethod.s_anonymouslyHostedDynamicMethodsModule = (InternalModuleBuilder) assemblyBuilder.ManifestModule;
      }
      return (RuntimeModule) DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
    }

    [SecurityCritical]
    private void Init(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] signature, Type owner, Module m, bool skipVisibility, bool transparentMethod, ref StackCrawlMark stackMark)
    {
      DynamicMethod.CheckConsistency(attributes, callingConvention);
      if (signature != null)
      {
        this.m_parameterTypes = new RuntimeType[signature.Length];
        for (int index = 0; index < signature.Length; ++index)
        {
          if (signature[index] == (Type) null)
            throw new ArgumentException(Environment.GetResourceString("Arg_InvalidTypeInSignature"));
          this.m_parameterTypes[index] = signature[index].UnderlyingSystemType as RuntimeType;
          if (this.m_parameterTypes[index] == (RuntimeType) null || (object) this.m_parameterTypes[index] == null || this.m_parameterTypes[index] == (RuntimeType) typeof (void))
            throw new ArgumentException(Environment.GetResourceString("Arg_InvalidTypeInSignature"));
        }
      }
      else
        this.m_parameterTypes = new RuntimeType[0];
      this.m_returnType = returnType == (Type) null ? (RuntimeType) typeof (void) : returnType.UnderlyingSystemType as RuntimeType;
      if (this.m_returnType == (RuntimeType) null || (object) this.m_returnType == null || this.m_returnType.IsByRef)
        throw new NotSupportedException(Environment.GetResourceString("Arg_InvalidTypeInRetType"));
      if (transparentMethod)
      {
        this.m_module = DynamicMethod.GetDynamicMethodsModule();
        if (skipVisibility)
          this.m_restrictedSkipVisibility = true;
        this.m_creationContext = CompressedStack.Capture();
      }
      else
      {
        if (m != (Module) null)
        {
          this.m_module = m.ModuleHandle.GetRuntimeModule();
        }
        else
        {
          RuntimeType runtimeType = (RuntimeType) null;
          if (owner != (Type) null)
            runtimeType = owner.UnderlyingSystemType as RuntimeType;
          if (runtimeType != (RuntimeType) null)
          {
            if (runtimeType.HasElementType || runtimeType.ContainsGenericParameters || (runtimeType.IsGenericParameter || runtimeType.IsInterface))
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeForDynamicMethod"));
            this.m_typeOwner = runtimeType;
            this.m_module = runtimeType.GetRuntimeModule();
          }
        }
        this.m_skipVisibility = skipVisibility;
      }
      this.m_ilGenerator = (DynamicILGenerator) null;
      this.m_fInitLocals = true;
      this.m_methodHandle = (IRuntimeMethodInfo) null;
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (AppDomain.ProfileAPICheck)
      {
        if ((Assembly) this.m_creatorAssembly == (Assembly) null)
          this.m_creatorAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
        if ((Assembly) this.m_creatorAssembly != (Assembly) null && !this.m_creatorAssembly.IsFrameworkAssembly())
          this.m_profileAPICheck = true;
      }
      this.m_dynMethod = new DynamicMethod.RTDynamicMethod(this, name, attributes, callingConvention);
    }

    [SecurityCritical]
    private void PerformSecurityCheck(Module m, ref StackCrawlMark stackMark, bool skipVisibility)
    {
      if (m == (Module) null)
        throw new ArgumentNullException(nameof (m));
      ModuleBuilder moduleBuilder = m as ModuleBuilder;
      RuntimeModule runtimeModule = !((Module) moduleBuilder != (Module) null) ? m as RuntimeModule : (RuntimeModule) moduleBuilder.InternalModule;
      if ((Module) runtimeModule == (Module) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeModule"), nameof (m));
      if ((Module) runtimeModule == (Module) DynamicMethod.s_anonymouslyHostedDynamicMethodsModule)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"), nameof (m));
      if (skipVisibility)
        new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
      this.m_creatorAssembly = RuntimeMethodHandle.GetCallerType(ref stackMark).GetRuntimeAssembly();
      if (!(m.Assembly != (Assembly) this.m_creatorAssembly))
        return;
      CodeAccessSecurityEngine.ReflectionTargetDemandHelper(PermissionType.SecurityControlEvidence, m.Assembly.PermissionSet);
    }

    [SecurityCritical]
    private void PerformSecurityCheck(Type owner, ref StackCrawlMark stackMark, bool skipVisibility)
    {
      if (owner == (Type) null)
        throw new ArgumentNullException(nameof (owner));
      RuntimeType runtimeType = owner as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        runtimeType = owner.UnderlyingSystemType as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentNullException(nameof (owner), Environment.GetResourceString("Argument_MustBeRuntimeType"));
      RuntimeType callerType = RuntimeMethodHandle.GetCallerType(ref stackMark);
      if (skipVisibility)
        new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
      else if (callerType != runtimeType)
        new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
      this.m_creatorAssembly = callerType.GetRuntimeAssembly();
      if (!(runtimeType.Assembly != (Assembly) this.m_creatorAssembly))
        return;
      CodeAccessSecurityEngine.ReflectionTargetDemandHelper(PermissionType.SecurityControlEvidence, owner.Assembly.PermissionSet);
    }

    /// <summary>
    ///   Завершает динамический метод и создает делегата, который может использоваться для его выполнения.
    /// </summary>
    /// <param name="delegateType">
    ///   Тип делегата, подпись которого совпадает с подписью динамического метода.
    /// </param>
    /// <returns>
    ///   Делегат указанного типа, который может использоваться для выполнения динамического метода.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   У динамического метода отсутствует тело метода.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="delegateType" /> имеет неверное число параметров или неправильные типы параметров.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public override sealed Delegate CreateDelegate(Type delegateType)
    {
      if (this.m_restrictedSkipVisibility)
      {
        this.GetMethodDescriptor();
        RuntimeHelpers._CompileMethod(this.m_methodHandle);
      }
      MulticastDelegate delegateNoSecurityCheck = (MulticastDelegate) Delegate.CreateDelegateNoSecurityCheck(delegateType, (object) null, this.GetMethodDescriptor());
      delegateNoSecurityCheck.StoreDynamicMethod(this.GetMethodInfo());
      return (Delegate) delegateNoSecurityCheck;
    }

    /// <summary>
    ///   Завершает динамический метод и создает делегат, который может использоваться для его выполнения, указывая тип делегата и объект, к которому привязан делегат.
    /// </summary>
    /// <param name="delegateType">
    ///   Тип делегата, подпись которого совпадает с подписью динамического метода минус первый параметр.
    /// </param>
    /// <param name="target">
    ///   Объект, к которому привязан делегат.
    ///    Должен быть того же типа, что и первый параметр динамического метода.
    /// </param>
    /// <returns>
    ///   Делегат указанного типа, который может использоваться для выполнения динамического метода с указанным целевым объектом.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   У динамического метода отсутствует тело метода.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="target" /> не относится к тому же типу, что и первый параметр динамического метода, и не может быть назначен этому типу.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="delegateType" /> имеет неверное число параметров или неправильные типы параметров.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public override sealed Delegate CreateDelegate(Type delegateType, object target)
    {
      if (this.m_restrictedSkipVisibility)
      {
        this.GetMethodDescriptor();
        RuntimeHelpers._CompileMethod(this.m_methodHandle);
      }
      MulticastDelegate delegateNoSecurityCheck = (MulticastDelegate) Delegate.CreateDelegateNoSecurityCheck(delegateType, target, this.GetMethodDescriptor());
      delegateNoSecurityCheck.StoreDynamicMethod(this.GetMethodInfo());
      return (Delegate) delegateNoSecurityCheck;
    }

    internal bool ProfileAPICheck
    {
      get
      {
        return this.m_profileAPICheck;
      }
      [FriendAccessAllowed] set
      {
        this.m_profileAPICheck = value;
      }
    }

    [SecurityCritical]
    internal RuntimeMethodHandle GetMethodDescriptor()
    {
      if (this.m_methodHandle == null)
      {
        lock (this)
        {
          if (this.m_methodHandle == null)
          {
            if (this.m_DynamicILInfo != null)
            {
              this.m_DynamicILInfo.GetCallableMethod(this.m_module, this);
            }
            else
            {
              if (this.m_ilGenerator == null || this.m_ilGenerator.ILOffset == 0)
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadEmptyMethodBody", (object) this.Name));
              this.m_ilGenerator.GetCallableMethod(this.m_module, this);
            }
          }
        }
      }
      return new RuntimeMethodHandle(this.m_methodHandle);
    }

    /// <summary>
    ///   Возвращает сигнатуру метода, представленную в виде строки.
    /// </summary>
    /// <returns>Строка, представляющая сигнатуру метода.</returns>
    public override string ToString()
    {
      return this.m_dynMethod.ToString();
    }

    /// <summary>Получает имя динамического метода.</summary>
    /// <returns>Простое имя метода.</returns>
    public override string Name
    {
      get
      {
        return this.m_dynMethod.Name;
      }
    }

    /// <summary>
    ///   Возвращает тип, который объявляет метод, всегда являющийся <see langword="null" /> для динамических методов.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="null" />.
    /// </returns>
    public override Type DeclaringType
    {
      get
      {
        return this.m_dynMethod.DeclaringType;
      }
    }

    /// <summary>
    ///   Возвращает класс, который использовался в отражении для получения метода.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="null" />.
    /// </returns>
    public override Type ReflectedType
    {
      get
      {
        return this.m_dynMethod.ReflectedType;
      }
    }

    /// <summary>
    ///   Возвращает модуль, с которым логически связан динамический метод.
    /// </summary>
    /// <returns>
    ///   Модуль <see cref="T:System.Reflection.Module" />, с которым связан текущий динамический метод.
    /// </returns>
    public override Module Module
    {
      get
      {
        return this.m_dynMethod.Module;
      }
    }

    /// <summary>Не поддерживается для динамических методов.</summary>
    /// <returns>Не поддерживается для динамических методов.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Не разрешено для динамических методов.
    /// </exception>
    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
      }
    }

    /// <summary>
    ///   Возвращает атрибуты, указанные при создании динамического метода.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание значений <see cref="T:System.Reflection.MethodAttributes" />, представляющее атрибуты для метода.
    /// </returns>
    public override MethodAttributes Attributes
    {
      get
      {
        return this.m_dynMethod.Attributes;
      }
    }

    /// <summary>
    ///   Получает соглашение о вызовах, указанное при создании динамического метода.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Reflection.CallingConventions" />, указывающее соглашение о вызовах метода.
    /// </returns>
    public override CallingConventions CallingConvention
    {
      get
      {
        return this.m_dynMethod.CallingConvention;
      }
    }

    /// <summary>Возвращает базовую реализацию метода.</summary>
    /// <returns>Базовая реализация метода.</returns>
    public override MethodInfo GetBaseDefinition()
    {
      return (MethodInfo) this;
    }

    /// <summary>Возвращает параметры динамического метода.</summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.ParameterInfo" />, которые представляют параметры динамического метода.
    /// </returns>
    public override ParameterInfo[] GetParameters()
    {
      return this.m_dynMethod.GetParameters();
    }

    /// <summary>Возвращает флаги реализации для метода.</summary>
    /// <returns>
    ///   Битовая комбинация значений <see cref="T:System.Reflection.MethodImplAttributes" />, представляющая флаги реализации для метода.
    /// </returns>
    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      return this.m_dynMethod.GetMethodImplementationFlags();
    }

    /// <summary>
    ///   Получает значение, которое указывает, является ли текущий динамический метод критическим с точки зрения безопасности или надежным с точки зрения безопасности и, следовательно, может ли он выполнять важные операции.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий динамический метод является критическим с точки зрения безопасности или надежным с точки зрения безопасности; значение <see langword="false" />, если он является прозрачным.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   В этом динамическом методе отсутствует тело метода.
    /// </exception>
    public override bool IsSecurityCritical
    {
      [SecuritySafeCritical] get
      {
        if (this.m_methodHandle != null)
          return RuntimeMethodHandle.IsSecurityCritical(this.m_methodHandle);
        if (this.m_typeOwner != (RuntimeType) null)
          return (this.m_typeOwner.Assembly as RuntimeAssembly).IsAllSecurityCritical();
        return (this.m_module.Assembly as RuntimeAssembly).IsAllSecurityCritical();
      }
    }

    /// <summary>
    ///   Возвращает значение, которое указывает, является ли текущий динамический метод надежным с точки зрения безопасности на текущем уровне доверия и, следовательно, может ли он выполнять критически важные операции и предоставлять доступ прозрачному коду.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если динамический метод надежен с точки зрения безопасности на текущем уровне доверия; значение <see langword="false" />, если он является критическим с точки зрения безопасности или прозрачным.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   В этом динамическом методе отсутствует тело метода.
    /// </exception>
    public override bool IsSecuritySafeCritical
    {
      [SecuritySafeCritical] get
      {
        if (this.m_methodHandle != null)
          return RuntimeMethodHandle.IsSecuritySafeCritical(this.m_methodHandle);
        if (this.m_typeOwner != (RuntimeType) null)
          return (this.m_typeOwner.Assembly as RuntimeAssembly).IsAllPublicAreaSecuritySafeCritical();
        return (this.m_module.Assembly as RuntimeAssembly).IsAllSecuritySafeCritical();
      }
    }

    /// <summary>
    ///   Получает значение, которое указывает, является ли текущий динамический метод прозрачным на текущем уровне доверия и, следовательно, не может выполнять критические операции.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если этот динамический метод является прозрачным на текущем уровне доверия; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   В этом динамическом методе отсутствует тело метода.
    /// </exception>
    public override bool IsSecurityTransparent
    {
      [SecuritySafeCritical] get
      {
        if (this.m_methodHandle != null)
          return RuntimeMethodHandle.IsSecurityTransparent(this.m_methodHandle);
        if (this.m_typeOwner != (RuntimeType) null)
          return !(this.m_typeOwner.Assembly as RuntimeAssembly).IsAllSecurityCritical();
        return !(this.m_module.Assembly as RuntimeAssembly).IsAllSecurityCritical();
      }
    }

    /// <summary>
    ///   Вызывает динамический метод, используя указанные параметры и учитывая ограничения заданного модуля привязки и указанные сведения о языке и региональных параметрах.
    /// </summary>
    /// <param name="obj">
    ///   Эти параметры игнорируются для динамических методов, поскольку они являются статическими.
    ///    Задайте имя <see langword="null" />.
    /// </param>
    /// <param name="invokeAttr">
    ///   Битовая комбинация значений <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <param name="binder">
    ///   Объект <see cref="T:System.Reflection.Binder" />, который допускает привязку, приведение типов аргументов, вызов элементов и извлечение объектов <see cref="T:System.Reflection.MemberInfo" /> путем отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    ///    Дополнительные сведения см. в разделе <see cref="T:System.Reflection.Binder" />.
    /// </param>
    /// <param name="parameters">
    ///   Список аргументов.
    ///    Это массив аргументов с тем же числом, порядком и типом, что и параметры вызываемого метода.
    ///    Если параметров нет, этот параметр должен иметь значение <see langword="null" />.
    /// </param>
    /// <param name="culture">
    ///   Экземпляр объекта <see cref="T:System.Globalization.CultureInfo" />, используемого для управления приведением типов.
    ///    Если значение этого объекта — <see langword="null" />, для текущего потока используется <see cref="T:System.Globalization.CultureInfo" />.
    ///    Например, эти сведения необходимы для правильного преобразовывая объекта <see cref="T:System.String" />, представляющего 1000, в значение <see cref="T:System.Double" />, поскольку в разных языках и региональных параметрах 1000 представляется по-разному.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Object" />, содержащий возвращаемое значение вызванного метода.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Соглашение о вызовах <see cref="F:System.Reflection.CallingConventions.VarArgs" /> не поддерживается.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    ///   Число элементов в <paramref name="parameters" /> не соответствует числу параметров в динамическом методе.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип одного или нескольких элементов <paramref name="parameters" /> не соответствует типу соответствующего параметра динамического метода.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Динамический метод связан с модулем, не размещен анонимно и был создан с параметром <paramref name="skipVisibility" />, для которого задано значение <see langword="false" />, однако он получает доступ к членам, которые не имеют тип <see langword="public" /> или <see langword="internal" /> (<see langword="Friend" /> в Visual Basic).
    /// 
    ///   -или-
    /// 
    ///   Динамический метод размещен анонимно и был создан с параметром <paramref name="skipVisibility" />, для которого задано значение <see langword="false" />, однако он получает доступ к членам, которые не имеют тип <see langword="public" />.
    /// 
    ///   -или-
    /// 
    ///   Динамический метод содержит непроверяемый код.
    ///    См. подраздел "Проверка" в разделе примечаний для <see cref="T:System.Reflection.Emit.DynamicMethod" />.
    /// </exception>
    [SecuritySafeCritical]
    public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      if ((this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_CallToVarArg"));
      this.GetMethodDescriptor();
      Signature sig = new Signature(this.m_methodHandle, this.m_parameterTypes, this.m_returnType, this.CallingConvention);
      int length = sig.Arguments.Length;
      int num = parameters != null ? parameters.Length : 0;
      if (length != num)
        throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
      object obj1;
      if (num > 0)
      {
        object[] arguments = this.CheckArguments(parameters, binder, invokeAttr, culture, sig);
        obj1 = RuntimeMethodHandle.InvokeMethod((object) null, arguments, sig, false);
        for (int index = 0; index < arguments.Length; ++index)
          parameters[index] = arguments[index];
      }
      else
        obj1 = RuntimeMethodHandle.InvokeMethod((object) null, (object[]) null, sig, false);
      GC.KeepAlive((object) this);
      return obj1;
    }

    /// <summary>
    ///   Возвращает настраиваемые атрибуты заданного типа, которые были применены к методу.
    /// </summary>
    /// <param name="attributeType">
    ///   <see cref="T:System.Type" />, представляющий тип возвращаемого настраиваемого атрибута.
    /// </param>
    /// <param name="inherit">
    ///   <see langword="true" /> для поиска цепочки наследования метода для обнаружения настраиваемых атрибутов; <see langword="false" /> для проверки только текущего метода.
    /// </param>
    /// <returns>
    ///   Массив объектов, представляющих атрибуты метода, которые относятся к типу <paramref name="attributeType" /> или являются производными от типа <paramref name="attributeType" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.m_dynMethod.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>
    ///   Возвращает все настраиваемые атрибуты, определенные для метода.
    /// </summary>
    /// <param name="inherit">
    ///   <see langword="true" /> для поиска цепочки наследования метода для обнаружения настраиваемых атрибутов; <see langword="false" /> для проверки только текущего метода.
    /// </param>
    /// <returns>
    ///   Массив объектов, представляющих все настраиваемые атрибуты метода.
    /// </returns>
    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.m_dynMethod.GetCustomAttributes(inherit);
    }

    /// <summary>
    ///   Указывает, определен ли заданный тип настраиваемых атрибутов.
    /// </summary>
    /// <param name="attributeType">
    ///   <see cref="T:System.Type" />, представляющий тип искомого настраиваемого атрибута.
    /// </param>
    /// <param name="inherit">
    ///   Значение <see langword="true" /> для поиска цепочки наследования метода для обнаружения настраиваемых атрибутов; значение <see langword="false" /> для проверки только текущего метода.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если заданный тип настраиваемых атрибутов определен; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.m_dynMethod.IsDefined(attributeType, inherit);
    }

    /// <summary>
    ///   Получает тип возвращаемого значения для динамического метода.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, представляющий тип возвращаемого значения текущего метода; значение <see cref="T:System.Void" />, если метод не имеет возвращаемого типа.
    /// </returns>
    public override Type ReturnType
    {
      get
      {
        return this.m_dynMethod.ReturnType;
      }
    }

    /// <summary>Возвращает выходной параметр динамического метода.</summary>
    /// <returns>
    ///   Всегда <see langword="null" />.
    /// </returns>
    public override ParameterInfo ReturnParameter
    {
      get
      {
        return this.m_dynMethod.ReturnParameter;
      }
    }

    /// <summary>
    ///   Получает настраиваемые атрибуты возвращаемого типа для динамического метода.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.ICustomAttributeProvider" />, представляющий настраиваемые атрибуты возвращаемого типа для динамического метода.
    /// </returns>
    public override ICustomAttributeProvider ReturnTypeCustomAttributes
    {
      get
      {
        return this.m_dynMethod.ReturnTypeCustomAttributes;
      }
    }

    /// <summary>Определяет параметр динамического метода.</summary>
    /// <param name="position">
    ///   Позиция параметра в списке параметров.
    ///    Параметры индексируются, начиная с номера 1 для первого параметра.
    /// </param>
    /// <param name="attributes">
    ///   Битовая комбинация значений <see cref="T:System.Reflection.ParameterAttributes" />, определяющая атрибуты параметра.
    /// </param>
    /// <param name="parameterName">
    ///   Имя параметра.
    ///    Имя может быть строкой нулевой длины.
    /// </param>
    /// <returns>
    ///   Всегда возвращает значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Метод не имеет параметров.
    /// 
    ///   -или-
    /// 
    ///   Значение параметра <paramref name="position" /> меньше 0.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="position" /> превышает число параметров метода.
    /// </exception>
    public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string parameterName)
    {
      if (position < 0 || position > this.m_parameterTypes.Length)
        throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
      --position;
      if (position >= 0)
      {
        ParameterInfo[] parameterInfoArray = this.m_dynMethod.LoadParameters();
        parameterInfoArray[position].SetName(parameterName);
        parameterInfoArray[position].SetAttributes(attributes);
      }
      return (ParameterBuilder) null;
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Reflection.Emit.DynamicILInfo" />, который может использоваться для создания тела метода из маркеров метаданных, областей и потоков промежуточного языка Майкрософт (MSIL).
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.DynamicILInfo" />, который может использоваться для создания тела метода из маркеров метаданных, областей и потоков MSIL.
    /// </returns>
    [SecuritySafeCritical]
    public DynamicILInfo GetDynamicILInfo()
    {
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      if (this.m_DynamicILInfo != null)
        return this.m_DynamicILInfo;
      return this.GetDynamicILInfo(new DynamicScope());
    }

    [SecurityCritical]
    internal DynamicILInfo GetDynamicILInfo(DynamicScope scope)
    {
      if (this.m_DynamicILInfo == null)
      {
        byte[] signature = SignatureHelper.GetMethodSigHelper((Module) null, this.CallingConvention, this.ReturnType, (Type[]) null, (Type[]) null, (Type[]) this.m_parameterTypes, (Type[][]) null, (Type[][]) null).GetSignature(true);
        this.m_DynamicILInfo = new DynamicILInfo(scope, this, signature);
      }
      return this.m_DynamicILInfo;
    }

    /// <summary>
    ///   Возвращает генератор MSIL для этого метода с размером потока MSIL, по умолчанию равным 64 байтам.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.ILGenerator" /> для метода.
    /// </returns>
    public ILGenerator GetILGenerator()
    {
      return this.GetILGenerator(64);
    }

    /// <summary>
    ///   Возвращает генератор промежуточного языка Майкрософт (MSIL) для этого метода с указанным размером потока MSIL.
    /// </summary>
    /// <param name="streamSize">Размер потока MSIL (в байтах).</param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.ILGenerator" /> для метода с указанным размером потока MSIL.
    /// </returns>
    [SecuritySafeCritical]
    public ILGenerator GetILGenerator(int streamSize)
    {
      if (this.m_ilGenerator == null)
        this.m_ilGenerator = new DynamicILGenerator(this, SignatureHelper.GetMethodSigHelper((Module) null, this.CallingConvention, this.ReturnType, (Type[]) null, (Type[]) null, (Type[]) this.m_parameterTypes, (Type[][]) null, (Type[][]) null).GetSignature(true), streamSize);
      return (ILGenerator) this.m_ilGenerator;
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, инициализированы ли локальные переменные в методе нулевым значением.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если локальные переменные в методе инициализированы нулевым значением; в противном случае — значение <see langword="false" />.
    ///    Значение по умолчанию — <see langword="true" />.
    /// </returns>
    public bool InitLocals
    {
      get
      {
        return this.m_fInitLocals;
      }
      set
      {
        this.m_fInitLocals = value;
      }
    }

    internal MethodInfo GetMethodInfo()
    {
      return (MethodInfo) this.m_dynMethod;
    }

    internal class RTDynamicMethod : MethodInfo
    {
      internal DynamicMethod m_owner;
      private ParameterInfo[] m_parameters;
      private string m_name;
      private MethodAttributes m_attributes;
      private CallingConventions m_callingConvention;

      private RTDynamicMethod()
      {
      }

      internal RTDynamicMethod(DynamicMethod owner, string name, MethodAttributes attributes, CallingConventions callingConvention)
      {
        this.m_owner = owner;
        this.m_name = name;
        this.m_attributes = attributes;
        this.m_callingConvention = callingConvention;
      }

      public override string ToString()
      {
        return this.ReturnType.FormatTypeName() + " " + this.FormatNameAndSig();
      }

      public override string Name
      {
        get
        {
          return this.m_name;
        }
      }

      public override Type DeclaringType
      {
        get
        {
          return (Type) null;
        }
      }

      public override Type ReflectedType
      {
        get
        {
          return (Type) null;
        }
      }

      public override Module Module
      {
        get
        {
          return (Module) this.m_owner.m_module;
        }
      }

      public override RuntimeMethodHandle MethodHandle
      {
        get
        {
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
        }
      }

      public override MethodAttributes Attributes
      {
        get
        {
          return this.m_attributes;
        }
      }

      public override CallingConventions CallingConvention
      {
        get
        {
          return this.m_callingConvention;
        }
      }

      public override MethodInfo GetBaseDefinition()
      {
        return (MethodInfo) this;
      }

      public override ParameterInfo[] GetParameters()
      {
        ParameterInfo[] parameterInfoArray1 = this.LoadParameters();
        ParameterInfo[] parameterInfoArray2 = new ParameterInfo[parameterInfoArray1.Length];
        Array.Copy((Array) parameterInfoArray1, (Array) parameterInfoArray2, parameterInfoArray1.Length);
        return parameterInfoArray2;
      }

      public override MethodImplAttributes GetMethodImplementationFlags()
      {
        return MethodImplAttributes.NoInlining;
      }

      public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "this");
      }

      public override object[] GetCustomAttributes(Type attributeType, bool inherit)
      {
        if (attributeType == (Type) null)
          throw new ArgumentNullException(nameof (attributeType));
        if (!attributeType.IsAssignableFrom(typeof (MethodImplAttribute)))
          return EmptyArray<object>.Value;
        return new object[1]
        {
          (object) new MethodImplAttribute(this.GetMethodImplementationFlags())
        };
      }

      public override object[] GetCustomAttributes(bool inherit)
      {
        return new object[1]
        {
          (object) new MethodImplAttribute(this.GetMethodImplementationFlags())
        };
      }

      public override bool IsDefined(Type attributeType, bool inherit)
      {
        if (attributeType == (Type) null)
          throw new ArgumentNullException(nameof (attributeType));
        return attributeType.IsAssignableFrom(typeof (MethodImplAttribute));
      }

      public override bool IsSecurityCritical
      {
        get
        {
          return this.m_owner.IsSecurityCritical;
        }
      }

      public override bool IsSecuritySafeCritical
      {
        get
        {
          return this.m_owner.IsSecuritySafeCritical;
        }
      }

      public override bool IsSecurityTransparent
      {
        get
        {
          return this.m_owner.IsSecurityTransparent;
        }
      }

      public override Type ReturnType
      {
        get
        {
          return (Type) this.m_owner.m_returnType;
        }
      }

      public override ParameterInfo ReturnParameter
      {
        get
        {
          return (ParameterInfo) null;
        }
      }

      public override ICustomAttributeProvider ReturnTypeCustomAttributes
      {
        get
        {
          return this.GetEmptyCAHolder();
        }
      }

      internal ParameterInfo[] LoadParameters()
      {
        if (this.m_parameters == null)
        {
          Type[] parameterTypes = (Type[]) this.m_owner.m_parameterTypes;
          ParameterInfo[] parameterInfoArray = new ParameterInfo[parameterTypes.Length];
          for (int position = 0; position < parameterTypes.Length; ++position)
            parameterInfoArray[position] = (ParameterInfo) new RuntimeParameterInfo((MethodInfo) this, (string) null, parameterTypes[position], position);
          if (this.m_parameters == null)
            this.m_parameters = parameterInfoArray;
        }
        return this.m_parameters;
      }

      private ICustomAttributeProvider GetEmptyCAHolder()
      {
        return (ICustomAttributeProvider) new DynamicMethod.RTDynamicMethod.EmptyCAHolder();
      }

      private class EmptyCAHolder : ICustomAttributeProvider
      {
        internal EmptyCAHolder()
        {
        }

        object[] ICustomAttributeProvider.GetCustomAttributes(Type attributeType, bool inherit)
        {
          return EmptyArray<object>.Value;
        }

        object[] ICustomAttributeProvider.GetCustomAttributes(bool inherit)
        {
          return EmptyArray<object>.Value;
        }

        bool ICustomAttributeProvider.IsDefined(Type attributeType, bool inherit)
        {
          return false;
        }
      }
    }
  }
}
