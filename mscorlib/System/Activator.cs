// Decompiled with JetBrains decompiler
// Type: System.Activator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Configuration.Assemblies;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Policy;
using System.Threading;

namespace System
{
  /// <summary>
  ///   Содержит методы, позволяющие локально или удаленно создавать типы объектов или получать ссылки на существующие удаленные объекты.
  ///    Этот класс не наследуется.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Activator))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class Activator : _Activator
  {
    internal const int LookupMask = 255;
    internal const BindingFlags ConLookup = BindingFlags.Instance | BindingFlags.Public;
    internal const BindingFlags ConstructorDefault = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;

    private Activator()
    {
    }

    /// <summary>
    ///   Создает экземпляр указанного типа, используя конструктор, соответствующий заданным параметрам.
    /// </summary>
    /// <param name="type">Тип создаваемого объекта.</param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="type" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который использует параметры <paramref name="bindingAttr" /> и <paramref name="args" /> для поиска и идентификации конструктора <paramref name="type" />.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Массив аргументов, число, порядок и тип которых соответствуют параметрам вызываемого конструктора.
    ///    Если параметр <paramref name="args" /> предоставляет пустой массив или имеет значение <see langword="null" />, то вызывается конструктор, который не принимает никаких параметров (конструктор, вызываемый по умолчанию).
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="type" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <returns>Ссылка на вновь созданный объект.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="type" /> является открытым универсальным типом (то есть свойство <see cref="P:System.Type.ContainsGenericParameters" /> возвращает <see langword="true" />).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип <paramref name="type" /> не может иметь значение <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// 
    ///   -или-
    /// 
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   Сборка, содержащая <paramref name="type" />, является динамической сборкой, которая была создана с использованием <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.
    /// 
    ///   -или-
    /// 
    ///   Конструктор, который наилучшим образом соответствует <paramref name="args" />, имеет аргументы <see langword="varargs" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызываемый конструктор создает исключение.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   <paramref name="type" /> — это объект модели COM, но идентификатор класса, используемый для получения типа, является недопустимым, или идентифицируемый класс не зарегистрирован.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="type" /> не является допустимым типом.
    /// </exception>
    public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture)
    {
      return Activator.CreateInstance(type, bindingAttr, binder, args, culture, (object[]) null);
    }

    /// <summary>
    ///   Создает экземпляр указанного типа, используя конструктор, соответствующий заданным параметрам.
    /// </summary>
    /// <param name="type">Тип создаваемого объекта.</param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="type" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который использует параметры <paramref name="bindingAttr" /> и <paramref name="args" /> для поиска и идентификации конструктора <paramref name="type" />.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Массив аргументов, число, порядок и тип которых соответствуют параметрам вызываемого конструктора.
    ///    Если параметр <paramref name="args" /> предоставляет пустой массив или имеет значение <see langword="null" />, то вызывается конструктор, который не принимает никаких параметров (конструктор, вызываемый по умолчанию).
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="type" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>Ссылка на вновь созданный объект.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="type" /> является открытым универсальным типом (то есть свойство <see cref="P:System.Type.ContainsGenericParameters" /> возвращает <see langword="true" />).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип <paramref name="type" /> не может иметь значение <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// 
    ///   -или-
    /// 
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="activationAttributes" /> не является пустым массивом, а создаваемый тип не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// 
    ///   -или-
    /// 
    ///   Сборка, содержащая <paramref name="type" />, является динамической сборкой, которая была создана с использованием <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.
    /// 
    ///   -или-
    /// 
    ///   Конструктор, который наилучшим образом соответствует <paramref name="args" />, имеет аргументы <see langword="varargs" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызываемый конструктор создает исключение.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   <paramref name="type" /> — это объект модели COM, но идентификатор класса, используемый для получения типа, является недопустимым, или идентифицируемый класс не зарегистрирован.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="type" /> не является допустимым типом.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      if (type is TypeBuilder)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_CreateInstanceWithTypeBuilder"));
      if ((bindingAttr & (BindingFlags) 255) == BindingFlags.Default)
        bindingAttr |= BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
      if (activationAttributes != null && activationAttributes.Length != 0)
      {
        if (!type.IsMarshalByRef)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_ActivAttrOnNonMBR"));
        if (!type.IsContextful && (activationAttributes.Length > 1 || !(activationAttributes[0] is UrlAttribute)))
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonUrlAttrOnMBR"));
      }
      RuntimeType underlyingSystemType = type.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (type));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return underlyingSystemType.CreateInstanceImpl(bindingAttr, binder, args, culture, activationAttributes, ref stackMark);
    }

    /// <summary>
    ///   Создает экземпляр указанного типа, используя конструктор, соответствующий заданным параметрам.
    /// </summary>
    /// <param name="type">Тип создаваемого объекта.</param>
    /// <param name="args">
    ///   Массив аргументов, число, порядок и тип которых соответствуют параметрам вызываемого конструктора.
    ///    Если параметр <paramref name="args" /> предоставляет пустой массив или имеет значение <see langword="null" />, то вызывается конструктор, который не принимает никаких параметров (конструктор, вызываемый по умолчанию).
    /// </param>
    /// <returns>Ссылка на вновь созданный объект.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="type" /> является открытым универсальным типом (то есть свойство <see cref="P:System.Type.ContainsGenericParameters" /> возвращает <see langword="true" />).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип <paramref name="type" /> не может иметь значение <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// 
    ///   -или-
    /// 
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   Сборка, содержащая <paramref name="type" />, является динамической сборкой, которая была создана с использованием <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.
    /// 
    ///   -или-
    /// 
    ///   Конструктор, который наилучшим образом соответствует <paramref name="args" />, имеет аргументы <see langword="varargs" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызываемый конструктор создает исключение.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.MemberAccessException" />.
    /// 
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.MissingMemberException" />.
    /// 
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   <paramref name="type" /> — это COM-объект, но идентификатор класса, используемый для получения типа, является недопустимым, или идентифицируемый класс не зарегистрирован.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="type" /> не является допустимым типом.
    /// </exception>
    [__DynamicallyInvokable]
    public static object CreateInstance(Type type, params object[] args)
    {
      return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, args, (CultureInfo) null, (object[]) null);
    }

    /// <summary>
    ///   Создает экземпляр указанного типа, используя конструктор, соответствующий заданным параметрам.
    /// </summary>
    /// <param name="type">Тип создаваемого объекта.</param>
    /// <param name="args">
    ///   Массив аргументов, число, порядок и тип которых соответствуют параметрам вызываемого конструктора.
    ///    Если параметр <paramref name="args" /> предоставляет пустой массив или имеет значение <see langword="null" />, то вызывается конструктор, который не принимает никаких параметров (конструктор, вызываемый по умолчанию).
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>Ссылка на вновь созданный объект.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="type" /> является открытым универсальным типом (то есть свойство <see cref="P:System.Type.ContainsGenericParameters" /> возвращает <see langword="true" />).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип <paramref name="type" /> не может иметь значение <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// 
    ///   -или-
    /// 
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="activationAttributes" /> не является пустым массивом, а создаваемый тип не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// 
    ///   -или-
    /// 
    ///   Сборка, содержащая <paramref name="type" />, является динамической сборкой, которая была создана с использованием <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.
    /// 
    ///   -или-
    /// 
    ///   Конструктор, который наилучшим образом соответствует <paramref name="args" />, имеет аргументы <see langword="varargs" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызываемый конструктор создает исключение.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   <paramref name="type" /> — это COM-объект, но идентификатор класса, используемый для получения типа, является недопустимым, или идентифицируемый класс не зарегистрирован.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="type" /> не является допустимым типом.
    /// </exception>
    public static object CreateInstance(Type type, object[] args, object[] activationAttributes)
    {
      return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, args, (CultureInfo) null, activationAttributes);
    }

    /// <summary>
    ///   Создает экземпляр указанного типа, используя конструктор, заданный для этого типа по умолчанию.
    /// </summary>
    /// <param name="type">Тип создаваемого объекта.</param>
    /// <returns>Ссылка на вновь созданный объект.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="type" /> является открытым универсальным типом (то есть свойство <see cref="P:System.Type.ContainsGenericParameters" /> возвращает <see langword="true" />).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип <paramref name="type" /> не может иметь значение <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// 
    ///   -или-
    /// 
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   Сборка, содержащая <paramref name="type" />, является динамической сборкой, которая была создана с использованием <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызываемый конструктор создает исключение.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///     В .NET for Windows Store apps или переносимой библиотеки классов, перехватывайте это исключение базовый класс <see cref="T:System.MemberAccessException" />, вместо этого.
    /// 
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///     В .NET for Windows Store apps или переносимой библиотеки классов, перехватывайте это исключение базовый класс <see cref="T:System.MissingMemberException" />, вместо этого.
    /// 
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   <paramref name="type" /> — это COM-объект, но идентификатор класса, используемый для получения типа, является недопустимым, или идентифицируемый класс не зарегистрирован.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="type" /> не является допустимым типом.
    /// </exception>
    [__DynamicallyInvokable]
    public static object CreateInstance(Type type)
    {
      return Activator.CreateInstance(type, false);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем, используя для этого именованную сборку и конструктор по умолчанию.
    /// </summary>
    /// <param name="assemblyName">
    ///   Имя сборки, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    ///    Дополнительные сведения см. в разделе "Примечания".
    ///    Если параметр <paramref name="assemblyName" /> имеет значение <see langword="null" />, поиск осуществляется в выполняющейся сборке.
    /// </param>
    /// <param name="typeName">Полное имя предпочтительного типа.</param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
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
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот член был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// 
    ///   -или-
    /// 
    ///   Недопустимые имя сборки или кодовая база.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ObjectHandle CreateInstance(string assemblyName, string typeName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Activator.CreateInstance(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, (object[]) null, (CultureInfo) null, (object[]) null, (Evidence) null, ref stackMark);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем, используя для этого именованную сборку и конструктор по умолчанию.
    /// </summary>
    /// <param name="assemblyName">
    ///   Имя сборки, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    ///    Если параметр <paramref name="assemblyName" /> имеет значение <see langword="null" />, поиск осуществляется в выполняющейся сборке.
    /// </param>
    /// <param name="typeName">Полное имя предпочтительного типа.</param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
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
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="activationAttributes" /> не является пустым массивом, а создаваемый тип не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="activationAttributes" /> не является объектом <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    /// 
    ///   Массив.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимым именем сборки.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// 
    ///   -или-
    /// 
    ///   Недопустимое имя сборки или кодовая база.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Произошла ошибка при попытке удаленной активации в целевом объекте, указанном в <paramref name="activationAttributes" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Activator.CreateInstance(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, (object[]) null, (CultureInfo) null, activationAttributes, (Evidence) null, ref stackMark);
    }

    /// <summary>
    ///   Создает экземпляр указанного типа, используя конструктор, заданный для этого типа по умолчанию.
    /// </summary>
    /// <param name="type">Тип создаваемого объекта.</param>
    /// <param name="nonPublic">
    ///   Значение <see langword="true" />, если можно сопоставить как открытый, так и закрытый конструктор по умолчанию; значение <see langword="false" />, если можно сопоставить только открытый конструктор по умолчанию.
    /// </param>
    /// <returns>Ссылка на вновь созданный объект.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="type" /> является открытым универсальным типом (то есть свойство <see cref="P:System.Type.ContainsGenericParameters" /> возвращает <see langword="true" />).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип <paramref name="type" /> не может иметь значение <see cref="T:System.Reflection.Emit.TypeBuilder" />.
    /// 
    ///   -или-
    /// 
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   Сборка, содержащая <paramref name="type" />, является динамической сборкой, которая была создана с использованием <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызываемый конструктор создает исключение.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   <paramref name="type" /> — это COM-объект, но идентификатор класса, используемый для получения типа, является недопустимым, или идентифицируемый класс не зарегистрирован.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="type" /> не является допустимым типом.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static object CreateInstance(Type type, bool nonPublic)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      RuntimeType underlyingSystemType = type.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (type));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return underlyingSystemType.CreateInstanceDefaultCtor(!nonPublic, false, true, ref stackMark);
    }

    /// <summary>
    ///   Создает экземпляр типа, объявленного в указанном параметре универсального типа, с помощью конструктора без параметров.
    /// </summary>
    /// <typeparam name="T">Создаваемый тип данных.</typeparam>
    /// <returns>Ссылка на вновь созданный объект.</returns>
    /// <exception cref="T:System.MissingMethodException">
    ///     В .NET for Windows Store apps или переносимой библиотеки классов, перехватывайте это исключение базовый класс <see cref="T:System.MissingMemberException" />, вместо этого.
    /// 
    ///   Тип, который задан для <paramref name="T" />, не имеет конструктора без параметров.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T CreateInstance<T>()
    {
      RuntimeType runtimeType = typeof (T) as RuntimeType;
      if (runtimeType.HasElementType)
        throw new MissingMethodException(Environment.GetResourceString("Arg_NoDefCTor"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (T) runtimeType.CreateInstanceDefaultCtor(true, true, true, ref stackMark);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем, используя для этого файл именованной сборки и конструктор по умолчанию.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    /// </param>
    /// <param name="typeName">Имя предпочтительного типа.</param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось найти <paramref name="typename" /> в <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект имеет необходимые <see cref="T:System.Security.Permissions.FileIOPermission" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
    {
      return Activator.CreateInstanceFrom(assemblyFile, typeName, (object[]) null);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем, используя для этого файл именованной сборки и конструктор по умолчанию.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    /// </param>
    /// <param name="typeName">Имя предпочтительного типа.</param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось найти <paramref name="typename" /> в <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="activationAttributes" /> не является пустым массивом, а создаваемый тип не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект имеет необходимые <see cref="T:System.Security.Permissions.FileIOPermission" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes)
    {
      return Activator.CreateInstanceFrom(assemblyFile, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, (object[]) null, (CultureInfo) null, activationAttributes);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем, используя для этого именованную сборку и конструктор, который наилучшим образом соответствует заданным параметрам.
    /// </summary>
    /// <param name="assemblyName">
    ///   Имя сборки, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    ///    Если параметр <paramref name="assemblyName" /> имеет значение <see langword="null" />, поиск осуществляется в выполняющейся сборке.
    /// </param>
    /// <param name="typeName">Полное имя предпочтительного типа.</param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" /> для выполнения поиска <paramref name="typeName" /> без учета регистра; значение <see langword="false" /> для выполнения поиска с учетом регистра.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который использует параметры <paramref name="bindingAttr" /> и <paramref name="args" /> для поиска и идентификации конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Массив аргументов, число, порядок и тип которых соответствуют параметрам вызываемого конструктора.
    ///    Если параметр <paramref name="args" /> предоставляет пустой массив или имеет значение <see langword="null" />, то вызывается конструктор, который не принимает никаких параметров (конструктор, вызываемый по умолчанию).
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <param name="securityInfo">
    ///   Сведения, используемые для принятия решений согласно политике безопасности и предоставления разрешений для кода.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
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
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="activationAttributes" /> не является пустым массивом, а создаваемый тип не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// 
    ///   -или-
    /// 
    ///   Конструктор, который наилучшим образом соответствует <paramref name="args" />, имеет аргументы <see langword="varargs" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// 
    ///   -или-
    /// 
    ///   Недопустимые имя сборки или кодовая база.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityInfo, ref stackMark);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем, используя для этого именованную сборку и конструктор, который наилучшим образом соответствует заданным параметрам.
    /// </summary>
    /// <param name="assemblyName">
    ///   Имя сборки, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    ///    Если параметр <paramref name="assemblyName" /> имеет значение <see langword="null" />, поиск осуществляется в выполняющейся сборке.
    /// </param>
    /// <param name="typeName">Полное имя предпочтительного типа.</param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" /> для выполнения поиска <paramref name="typeName" /> без учета регистра; значение <see langword="false" /> для выполнения поиска с учетом регистра.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который использует параметры <paramref name="bindingAttr" /> и <paramref name="args" /> для поиска и идентификации конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Массив аргументов, число, порядок и тип которых соответствуют параметрам вызываемого конструктора.
    ///    Если параметр <paramref name="args" /> предоставляет пустой массив или имеет значение <see langword="null" />, то вызывается конструктор, который не принимает никаких параметров (конструктор, вызываемый по умолчанию).
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
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
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="activationAttributes" /> не является пустым массивом, а создаваемый тип не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// 
    ///   -или-
    /// 
    ///   Конструктор, который наилучшим образом соответствует <paramref name="args" />, имеет аргументы <see langword="varargs" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// 
    ///   -или-
    /// 
    ///   Недопустимое имя сборки или кодовая база.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, (Evidence) null, ref stackMark);
    }

    [SecurityCritical]
    internal static ObjectHandle CreateInstance(string assemblyString, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo, ref StackCrawlMark stackMark)
    {
      if (securityInfo != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      Type type = (Type) null;
      Assembly assembly = (Assembly) null;
      if (assemblyString == null)
      {
        assembly = (Assembly) RuntimeAssembly.GetExecutingAssembly(ref stackMark);
      }
      else
      {
        RuntimeAssembly assemblyFromResolveEvent;
        AssemblyName assemblyName = RuntimeAssembly.CreateAssemblyName(assemblyString, false, out assemblyFromResolveEvent);
        if ((Assembly) assemblyFromResolveEvent != (Assembly) null)
          assembly = (Assembly) assemblyFromResolveEvent;
        else if (assemblyName.ContentType == AssemblyContentType.WindowsRuntime)
          type = Type.GetType(typeName + ", " + assemblyString, true, ignoreCase);
        else
          assembly = (Assembly) RuntimeAssembly.InternalLoadAssemblyName(assemblyName, securityInfo, (RuntimeAssembly) null, ref stackMark, true, false, false);
      }
      if (type == (Type) null)
      {
        if (assembly == (Assembly) null)
          return (ObjectHandle) null;
        type = assembly.GetType(typeName, true, ignoreCase);
      }
      object instance = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
      if (instance == null)
        return (ObjectHandle) null;
      return new ObjectHandle(instance);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем, используя для этого файл именованной сборки и конструктор, который наилучшим образом соответствует заданным параметрам.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    /// </param>
    /// <param name="typeName">Имя предпочтительного типа.</param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" /> для выполнения поиска <paramref name="typeName" /> без учета регистра; значение <see langword="false" /> для выполнения поиска с учетом регистра.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который использует параметры <paramref name="bindingAttr" /> и <paramref name="args" /> для поиска и идентификации конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Массив аргументов, число, порядок и тип которых соответствуют параметрам вызываемого конструктора.
    ///    Если параметр <paramref name="args" /> предоставляет пустой массив или имеет значение <see langword="null" />, то вызывается конструктор, который не принимает никаких параметров (конструктор, вызываемый по умолчанию).
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <param name="securityInfo">
    ///   Сведения, используемые для принятия решений согласно политике безопасности и предоставления разрешений для кода.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typename" /> не найден в <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект не имеет необходимых <see cref="T:System.Security.Permissions.FileIOPermission" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="activationAttributes" /> не является пустым массивом, а создаваемый тип не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
    {
      if (securityInfo != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      return Activator.CreateInstanceFromInternal(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityInfo);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем, используя для этого файл именованной сборки и конструктор, который наилучшим образом соответствует заданным параметрам.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    /// </param>
    /// <param name="typeName">Имя предпочтительного типа.</param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" /> для выполнения поиска <paramref name="typeName" /> без учета регистра; значение <see langword="false" /> для выполнения поиска с учетом регистра.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который использует параметры <paramref name="bindingAttr" /> и <paramref name="args" /> для поиска и идентификации конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Массив аргументов, число, порядок и тип которых соответствуют параметрам вызываемого конструктора.
    ///    Если параметр <paramref name="args" /> предоставляет пустой массив или имеет значение <see langword="null" />, то вызывается конструктор, который не принимает никаких параметров (конструктор, вызываемый по умолчанию).
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typename" /> не найден в <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект не имеет необходимых <see cref="T:System.Security.Permissions.FileIOPermission" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="activationAttributes" /> не является пустым массивом, а создаваемый тип не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      return Activator.CreateInstanceFromInternal(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, (Evidence) null);
    }

    private static ObjectHandle CreateInstanceFromInternal(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
    {
      object instance = Activator.CreateInstance(Assembly.LoadFrom(assemblyFile, securityInfo).GetType(typeName, true, ignoreCase), bindingAttr, binder, args, culture, activationAttributes);
      if (instance == null)
        return (ObjectHandle) null;
      return new ObjectHandle(instance);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем в указанном удаленном домене, используя для этого именованную сборку и конструктор по умолчанию.
    /// </summary>
    /// <param name="domain">
    ///   Удаленный домен, в котором создан тип с именем <paramref name="typeName" />.
    /// </param>
    /// <param name="assemblyName">
    ///   Имя сборки, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    ///    Если параметр <paramref name="assemblyName" /> имеет значение <see langword="null" />, поиск осуществляется в выполняющейся сборке.
    /// </param>
    /// <param name="typeName">Полное имя предпочтительного типа.</param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="typeName" /> или <paramref name="domain" /> имеет значение <see langword="null" />.
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
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного типа.
    /// 
    ///   -или-
    /// 
    ///   Этот элемент был вызван при помощи механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// 
    ///   -или-
    /// 
    ///   Недопустимые имя сборки или кодовая база.
    /// </exception>
    [SecurityCritical]
    public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName)
    {
      if (domain == null)
        throw new ArgumentNullException(nameof (domain));
      return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем в указанном удаленном домене, используя для этого именованную сборку и конструктор, который наилучшим образом соответствует заданным параметрам.
    /// </summary>
    /// <param name="domain">
    ///   Домен, в котором создан тип с именем <paramref name="typeName" />.
    /// </param>
    /// <param name="assemblyName">
    ///   Имя сборки, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    ///    Если параметр <paramref name="assemblyName" /> имеет значение <see langword="null" />, поиск осуществляется в выполняющейся сборке.
    /// </param>
    /// <param name="typeName">Полное имя предпочтительного типа.</param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" /> для выполнения поиска <paramref name="typeName" /> без учета регистра; значение <see langword="false" /> для выполнения поиска с учетом регистра.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который использует параметры <paramref name="bindingAttr" /> и <paramref name="args" /> для поиска и идентификации конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Массив аргументов, число, порядок и тип которых соответствуют параметрам вызываемого конструктора.
    ///    Если параметр <paramref name="args" /> предоставляет пустой массив или имеет значение <see langword="null" />, то вызывается конструктор, который не принимает никаких параметров (конструктор, вызываемый по умолчанию).
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, это массив, содержащий единственный объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    ///    Атрибут <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> указывает URL-адрес, требуемый для активации удаленного объекта.
    /// </param>
    /// <param name="securityAttributes">
    ///   Сведения, используемые для принятия решений согласно политике безопасности и предоставления разрешений для кода.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="domain" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
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
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="activationAttributes" /> не является пустым массивом, а создаваемый тип не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// 
    ///   -или-
    /// 
    ///   Конструктор, который наилучшим образом соответствует <paramref name="args" />, имеет аргументы <see langword="varargs" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// 
    ///   -или-
    /// 
    ///   Недопустимые имя сборки или кодовая база.
    /// </exception>
    [SecurityCritical]
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      if (domain == null)
        throw new ArgumentNullException(nameof (domain));
      if (securityAttributes != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем в указанном удаленном домене, используя для этого именованную сборку и конструктор, который наилучшим образом соответствует заданным параметрам.
    /// </summary>
    /// <param name="domain">
    ///   Домен, в котором создан тип с именем <paramref name="typeName" />.
    /// </param>
    /// <param name="assemblyName">
    ///   Имя сборки, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    ///    Если параметр <paramref name="assemblyName" /> имеет значение <see langword="null" />, поиск осуществляется в выполняющейся сборке.
    /// </param>
    /// <param name="typeName">Полное имя предпочтительного типа.</param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" /> для выполнения поиска <paramref name="typeName" /> без учета регистра; значение <see langword="false" /> для выполнения поиска с учетом регистра.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который использует параметры <paramref name="bindingAttr" /> и <paramref name="args" /> для поиска и идентификации конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Массив аргументов, число, порядок и тип которых соответствуют параметрам вызываемого конструктора.
    ///    Если параметр <paramref name="args" /> предоставляет пустой массив или имеет значение <see langword="null" />, то вызывается конструктор, который не принимает никаких параметров (конструктор, вызываемый по умолчанию).
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="domain" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
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
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">
    ///   Тип модели COM не был получен с помощью <see cref="Overload:System.Type.GetTypeFromProgID" /> или <see cref="Overload:System.Type.GetTypeFromCLSID" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, <see cref="T:System.Void" /> и <see cref="T:System.RuntimeArgumentHandle" /> или массивов этих типов не поддерживается.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="activationAttributes" /> не является пустым массивом, а создаваемый тип не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// 
    ///   -или-
    /// 
    ///   Конструктор, который наилучшим образом соответствует <paramref name="args" />, имеет аргументы <see langword="varargs" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyName" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами.
    /// 
    ///   -или-
    /// 
    ///   Недопустимые имя сборки или кодовая база.
    /// </exception>
    [SecurityCritical]
    public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      if (domain == null)
        throw new ArgumentNullException(nameof (domain));
      return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, (Evidence) null);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем в указанном удаленном домене, используя для этого файл именованной сборки и конструктор по умолчанию.
    /// </summary>
    /// <param name="domain">
    ///   Удаленный домен, в котором создан тип с именем <paramref name="typeName" />.
    /// </param>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    /// </param>
    /// <param name="typeName">Имя предпочтительного типа.</param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="domain" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий общий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось найти <paramref name="typename" /> в <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект имеет необходимые <see cref="T:System.Security.Permissions.FileIOPermission" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    [SecurityCritical]
    public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName)
    {
      if (domain == null)
        throw new ArgumentNullException(nameof (domain));
      return domain.InternalCreateInstanceFromWithNoSecurity(assemblyFile, typeName);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем в указанном удаленном домене, используя для этого файл именованной сборки и конструктор, который наилучшим образом соответствует заданным параметрам.
    /// </summary>
    /// <param name="domain">
    ///   Удаленный домен, в котором создан тип с именем <paramref name="typeName" />.
    /// </param>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    /// </param>
    /// <param name="typeName">Имя предпочтительного типа.</param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" /> для выполнения поиска <paramref name="typeName" /> без учета регистра; значение <see langword="false" /> для выполнения поиска с учетом регистра.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который использует параметры <paramref name="bindingAttr" /> и <paramref name="args" /> для поиска и идентификации конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Массив аргументов, число, порядок и тип которых соответствуют параметрам вызываемого конструктора.
    ///    Если параметр <paramref name="args" /> предоставляет пустой массив или имеет значение <see langword="null" />, то вызывается конструктор, который не принимает никаких параметров (конструктор, вызываемый по умолчанию).
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <param name="securityAttributes">
    ///   Сведения, используемые для принятия решений согласно политике безопасности и предоставления разрешений для кода.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="domain" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typename" /> не найден в <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект имеет необходимые <see cref="T:System.Security.Permissions.FileIOPermission" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="activationAttributes" /> не является пустым массивом, а создаваемый тип не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   В данный момент загружена среда CLR версии 2.0 или более поздней. Объект <paramref name="assemblyName" /> был скомпилирован для версии среды CLR, более поздней, чем загруженная.
    ///    Обратите внимание, что .NET Framework версий 2.0, 3.0 и 3.5 использует среду CLR версии 2.0.
    /// </exception>
    [SecurityCritical]
    [Obsolete("Methods which use Evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      if (domain == null)
        throw new ArgumentNullException(nameof (domain));
      if (securityAttributes != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      return domain.InternalCreateInstanceFromWithNoSecurity(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
    }

    /// <summary>
    ///   Создает экземпляр типа с заданным именем в указанном удаленном домене, используя для этого файл именованной сборки и конструктор, который наилучшим образом соответствует заданным параметрам.
    /// </summary>
    /// <param name="domain">
    ///   Удаленный домен, в котором создан тип с именем <paramref name="typeName" />.
    /// </param>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    /// </param>
    /// <param name="typeName">Имя предпочтительного типа.</param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" /> для выполнения поиска <paramref name="typeName" /> без учета регистра; значение <see langword="false" /> для выполнения поиска с учетом регистра.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который использует параметры <paramref name="bindingAttr" /> и <paramref name="args" /> для поиска и идентификации конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Массив аргументов, число, порядок и тип которых соответствуют параметрам вызываемого конструктора.
    ///    Если параметр <paramref name="args" /> предоставляет пустой массив или имеет значение <see langword="null" />, то вызывается конструктор, который не принимает никаких параметров (конструктор, вызываемый по умолчанию).
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, это массив, содержащий один объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />, определяющий URL-адрес, необходимый для активации удаленного объекта.
    /// 
    ///   Этот параметр связан с объектами, активируемыми клиентом.
    ///    Активация клиентом — это устаревшая технология, которая сохраняется с целью обеспечения обратной совместимости; ее не рекомендуется использовать для разработки новых приложений.
    ///    Сейчас в распределенных приложениях следует использовать Windows Communication Foundation.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="domain" /> или <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typename" /> не найден в <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удалось найти <paramref name="assemblyFile" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на вызов этого конструктора.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Конструктор, который был вызван с помощью отражения, создал исключение.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект имеет необходимые <see cref="T:System.Security.Permissions.FileIOPermission" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="activationAttributes" /> не является пустым массивом, а создаваемый тип не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="assemblyFile" /> не является допустимой сборкой.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="assemblyName" />был скомпилирован для версии, более поздней версии, чем версия, которая в настоящий момент загружена среда.
    /// </exception>
    [SecurityCritical]
    public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      if (domain == null)
        throw new ArgumentNullException(nameof (domain));
      return domain.InternalCreateInstanceFromWithNoSecurity(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, (Evidence) null);
    }

    /// <summary>
    ///   Создает экземпляр типа, назначенного указанным объектом <see cref="T:System.ActivationContext" />.
    /// </summary>
    /// <param name="activationContext">
    ///   Объект контекста активации, задающий объект, который необходимо создать.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному объекту.
    /// </returns>
    [SecuritySafeCritical]
    public static ObjectHandle CreateInstance(ActivationContext activationContext)
    {
      return (AppDomain.CurrentDomain.DomainManager ?? new AppDomainManager()).ApplicationActivator.CreateInstance(activationContext);
    }

    /// <summary>
    ///   Создает экземпляр типа, назначенного указанным объектом <see cref="T:System.ActivationContext" /> и активированного с помощью указанных пользовательских данных активации.
    /// </summary>
    /// <param name="activationContext">
    ///   Объект контекста активации, задающий объект, который необходимо создать.
    /// </param>
    /// <param name="activationCustomData">
    ///   Массив строк в кодировке Юникод, содержащих пользовательские данные активации.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному объекту.
    /// </returns>
    [SecuritySafeCritical]
    public static ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData)
    {
      return (AppDomain.CurrentDomain.DomainManager ?? new AppDomainManager()).ApplicationActivator.CreateInstance(activationContext, activationCustomData);
    }

    /// <summary>
    ///   Создает экземпляр COM-объекта с заданным именем, используя для этого файл именованной сборки и конструктор по умолчанию.
    /// </summary>
    /// <param name="assemblyName">
    ///   Имя файла, содержащего сборку, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    /// </param>
    /// <param name="typeName">Имя предпочтительного типа.</param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="typeName" /> или <paramref name="assemblyName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Невозможно создать экземпляр с помощью модели COM.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typename" /> не найден в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Файл <paramref name="assemblyName" /> не найден, или модуль, который вы пытаетесь загрузить, не указывает расширение имени файла.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Не удалось создать экземпляр абстрактного класса.
    /// 
    ///   -или-
    /// 
    ///   Этот элемент был вызван при помощи механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="assemblyName" /> является пустой строкой ("").
    /// </exception>
    public static ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
    {
      return Activator.CreateComInstanceFrom(assemblyName, typeName, (byte[]) null, AssemblyHashAlgorithm.None);
    }

    /// <summary>
    ///   Создает экземпляр COM-объекта с заданным именем, используя для этого файл именованной сборки и конструктор по умолчанию.
    /// </summary>
    /// <param name="assemblyName">
    ///   Имя файла, содержащего сборку, в которой выполняется поиск типа, заданного параметром <paramref name="typeName" />.
    /// </param>
    /// <param name="typeName">Имя предпочтительного типа.</param>
    /// <param name="hashValue">Значение вычисленного хэш-кода.</param>
    /// <param name="hashAlgorithm">
    ///   Хэш-алгоритм, используемый для хэширования файлов и создания строгого имени.
    /// </param>
    /// <returns>
    ///   Дескриптор, оболочку которого нужно удалить, чтобы получить доступ к вновь созданному экземпляру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="typeName" /> или <paramref name="assemblyName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="assemblyName" /> является пустой строкой ("").
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    ///   Сборка или модуль был загружен дважды с двумя разными свидетельствами, или имя сборки содержит больше MAX_PATH знаков.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Тип <paramref name="assemblyName" /> не найден, либо в модуле, который вы пытаетесь загрузить, не указано расширение имени файла.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Тип <paramref name="assemblyName" /> найден, но не может быть загружен.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Тип <paramref name="assemblyName" /> не является допустимой сборкой.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   База кода, которая не начинается с "file://", была указана без требуемого разрешения <see langword="WebPermission" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Невозможно создать экземпляр с помощью COM-модели.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typename" /> не найден в <paramref name="assemblyName" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Соответствующий конструктор не найден.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Не удается создать экземпляр абстрактного класса.
    /// 
    ///   -или-
    /// 
    ///   Этот элемент был вызван при помощи механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект не может предоставить атрибуты активации для объекта, который не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    public static ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
      Assembly assembly = Assembly.LoadFrom(assemblyName, hashValue, hashAlgorithm);
      Type type = assembly.GetType(typeName, true, false);
      object[] customAttributes = type.GetCustomAttributes(typeof (ComVisibleAttribute), false);
      if (customAttributes.Length != 0 && !((ComVisibleAttribute) customAttributes[0]).Value)
        throw new TypeLoadException(Environment.GetResourceString("Argument_TypeMustBeVisibleFromCom"));
      if (assembly == (Assembly) null)
        return (ObjectHandle) null;
      object instance = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, (object[]) null, (CultureInfo) null, (object[]) null);
      if (instance == null)
        return (ObjectHandle) null;
      return new ObjectHandle(instance);
    }

    /// <summary>
    ///   Создает прокси для хорошо известного объекта, определенного заданным типом и URL.
    /// </summary>
    /// <param name="type">
    ///   Тип хорошо известного объекта, к которому нужно подключиться.
    /// </param>
    /// <param name="url">URL-адрес хорошо известного объекта.</param>
    /// <returns>
    ///   Прокси, который указывает на конечную точку, используемую требуемым хорошо известным объектом.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> или <paramref name="url" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="type" />не маршалируется по ссылке и не является интерфейсом.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Этот элемент был вызван при помощи механизма позднего связывания.
    /// </exception>
    [SecurityCritical]
    public static object GetObject(Type type, string url)
    {
      return Activator.GetObject(type, url, (object) null);
    }

    /// <summary>
    ///   Создает прокси для хорошо известного объекта, который идентифицируется по заданному типу, URL и данным канала.
    /// </summary>
    /// <param name="type">
    ///   Тип хорошо известного объекта, к которому нужно подключиться.
    /// </param>
    /// <param name="url">URL-адрес хорошо известного объекта.</param>
    /// <param name="state">
    ///   Данные, зависящие от канала, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Прокси, который указывает на конечную точку, используемую требуемым хорошо известным объектом.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> или <paramref name="url" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="type" />не маршалируется по ссылке и не является интерфейсом.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Этот элемент был вызван при помощи механизма позднего связывания.
    /// </exception>
    [SecurityCritical]
    public static object GetObject(Type type, string url, object state)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      return RemotingServices.Connect(type, url, state);
    }

    [Conditional("_DEBUG")]
    private static void Log(bool test, string title, string success, string failure)
    {
      int num = test ? 1 : 0;
    }

    void _Activator.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _Activator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _Activator.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _Activator.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
