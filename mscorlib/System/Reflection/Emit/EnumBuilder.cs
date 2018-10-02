// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.EnumBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>Описывает и представляет тип перечисления.</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_EnumBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class EnumBuilder : TypeInfo, _EnumBuilder
  {
    internal TypeBuilder m_typeBuilder;
    private FieldBuilder m_underlyingField;

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
    ///   Определяет именованное статическое поле типа перечисления с указанным константным значением.
    /// </summary>
    /// <param name="literalName">Имя статического поля.</param>
    /// <param name="literalValue">Константное значение литерала.</param>
    /// <returns>Определенное поле.</returns>
    public FieldBuilder DefineLiteral(string literalName, object literalValue)
    {
      FieldBuilder fieldBuilder = this.m_typeBuilder.DefineField(literalName, (Type) this, FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal);
      fieldBuilder.SetConstant(literalValue);
      return fieldBuilder;
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Reflection.TypeInfo" />, представляющий это перечисление.
    /// </summary>
    /// <returns>Объект, представляющий это перечисление.</returns>
    public TypeInfo CreateTypeInfo()
    {
      return this.m_typeBuilder.CreateTypeInfo();
    }

    /// <summary>
    ///   Создает объект <see cref="T:System.Type" /> для этого перечисления.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> для этого перечисления.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот тип был создан ранее.
    /// 
    ///   -или-
    /// 
    ///   Включающий тип не создан.
    /// </exception>
    public Type CreateType()
    {
      return this.m_typeBuilder.CreateType();
    }

    /// <summary>
    ///   Возвращает маркер типа внутренних метаданных этого перечисления.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Маркер типа этого перечисления.
    /// </returns>
    public TypeToken TypeToken
    {
      get
      {
        return this.m_typeBuilder.TypeToken;
      }
    }

    /// <summary>Возвращает базовое поле для этого перечисления.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Базовое поле для этого перечисления.
    /// </returns>
    public FieldBuilder UnderlyingField
    {
      get
      {
        return this.m_underlyingField;
      }
    }

    /// <summary>Возвращает имя этого перечисления.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Имя этого перечисления.
    /// </returns>
    public override string Name
    {
      get
      {
        return this.m_typeBuilder.Name;
      }
    }

    /// <summary>Возвращает идентификатор GUID данного перечисления.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Идентификатор GUID данного перечисления.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override Guid GUID
    {
      get
      {
        return this.m_typeBuilder.GUID;
      }
    }

    /// <summary>
    ///   Вызывает указанный член.
    ///    Вызываемый метод должен быть доступен и должен обеспечивать наиболее точное соответствие заданному списку аргументов с учетом ограничений заданного модуля привязки и атрибутов вызова.
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
    ///    Такие сведения можно найти в спецификации метаданных.
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
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      return this.m_typeBuilder.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
    }

    /// <summary>
    ///   Извлекает динамический модуль, который содержит определение данного <see cref="T:System.Reflection.Emit.EnumBuilder" />.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Динамический модуль, который содержит определение данного <see cref="T:System.Reflection.Emit.EnumBuilder" />.
    /// </returns>
    public override Module Module
    {
      get
      {
        return this.m_typeBuilder.Module;
      }
    }

    /// <summary>
    ///   Извлекает динамическую сборку, которая содержит определение данного перечисления.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Динамическая сборка, которая содержит определение данного перечисления.
    /// </returns>
    public override Assembly Assembly
    {
      get
      {
        return this.m_typeBuilder.Assembly;
      }
    }

    /// <summary>Извлекает внутренний маркер этого перечисления.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Внутренний маркер этого перечисления.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Данное свойство в настоящий момент не поддерживается.
    /// </exception>
    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        return this.m_typeBuilder.TypeHandle;
      }
    }

    /// <summary>Возвращает полный путь этого перечисления.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Полный путь этого перечисления.
    /// </returns>
    public override string FullName
    {
      get
      {
        return this.m_typeBuilder.FullName;
      }
    }

    /// <summary>
    ///   Возвращает полный путь к этому перечислению, определенный отображаемым именем родительской сборки.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Полный путь к этому перечислению, определенный отображаемым именем родительской сборки.
    /// </returns>
    public override string AssemblyQualifiedName
    {
      get
      {
        return this.m_typeBuilder.AssemblyQualifiedName;
      }
    }

    /// <summary>Возвращает пространство имен этого перечисления.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Пространство имен этого перечисления.
    /// </returns>
    public override string Namespace
    {
      get
      {
        return this.m_typeBuilder.Namespace;
      }
    }

    /// <summary>
    ///   Возвращает родительский тип <see cref="T:System.Type" /> этого типа, который всегда является <see cref="T:System.Enum" />.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Родительский <see cref="T:System.Type" /> данного типа.
    /// </returns>
    public override Type BaseType
    {
      get
      {
        return this.m_typeBuilder.BaseType;
      }
    }

    protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      return this.m_typeBuilder.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.ConstructorInfo" />, представляющих открытые и закрытые конструкторы, определенные для этого класса, как указано.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.ConstructorInfo" />, представляющих указанные конструкторы, определенные для этого класса.
    ///    Если конструкторы не определены, возвращается пустой массив.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    [ComVisible(true)]
    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetConstructors(bindingAttr);
    }

    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (types == null)
        return this.m_typeBuilder.GetMethod(name, bindingAttr);
      return this.m_typeBuilder.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>
    ///   Возвращает все открытые и закрытые методы, объявленные или наследуемые данным типом, как указано.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />, например <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.MethodInfo" />, представляющих открытые и закрытые методы, определенные для данного типа, если используется <paramref name="nonPublic" />; в противном случае возвращаются только открытые методы.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetMethods(bindingAttr);
    }

    /// <summary>Возвращает поле, указанное данным именем.</summary>
    /// <param name="name">Имя получаемого поля.</param>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />: <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает объект <see cref="T:System.Reflection.FieldInfo" />, представляющий поле, объявленное или наследуемое этим типом, с указанным именем и открытым или закрытым модификатором.
    ///    Если совпадений нет, возвращается нулевое значение.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetField(name, bindingAttr);
    }

    /// <summary>
    ///   Возвращает открытые и не являющиеся открытыми поля, объявленные данным типом.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />, например InvokeMethod, NonPublic и т. д.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.FieldInfo" />, представляющих открытые и не являющиеся открытыми поля, объявленные или наследованные этим объектом.
    ///    Если заданные поля отсутствуют, возвращается пустой массив.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetFields(bindingAttr);
    }

    /// <summary>
    ///   Возвращает интерфейс, реализованный (прямо или косвенно) данным типом, с указанным полным именем.
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
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override Type GetInterface(string name, bool ignoreCase)
    {
      return this.m_typeBuilder.GetInterface(name, ignoreCase);
    }

    /// <summary>
    ///   Возвращает массив всех интерфейсов, реализованных для данного класса и его базовых классов.
    /// </summary>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Type" />, представляющих реализованные интерфейсы.
    ///    Если они не определены, возвращается пустой массив.
    /// </returns>
    public override Type[] GetInterfaces()
    {
      return this.m_typeBuilder.GetInterfaces();
    }

    /// <summary>Возвращает событие с указанным именем.</summary>
    /// <param name="name">Имя возвращаемого события.</param>
    /// <param name="bindingAttr">
    ///   Это атрибут вызова.
    ///    Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />: <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает объект <see cref="T:System.Reflection.EventInfo" />, представляющий событие с указанным именем, объявленное или унаследованное этим типом.
    ///    Если совпадений нет, возвращается значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetEvent(name, bindingAttr);
    }

    /// <summary>
    ///   Возвращает события для открытых событий, объявленных или наследуемых данным типом.
    /// </summary>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.EventInfo" />, представляющих открытые события, объявленные или наследуемые этим типом.
    ///    Если открытые события отсутствуют, возвращается пустой массив.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override EventInfo[] GetEvents()
    {
      return this.m_typeBuilder.GetEvents();
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
    ///   Возвращает массив объектов <see cref="T:System.Reflection.PropertyInfo" />, представляющих открытые и закрытые свойства, определенные для данного типа, если используется <paramref name="nonPublic" />; в противном случае возвращаются только открытые свойства.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetProperties(bindingAttr);
    }

    /// <summary>
    ///   Возвращает открытые и не являющиеся открытыми вложенные типы, объявленные или наследованные данным типом.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />, например <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, представляющий все типы, вложенные внутри текущего типа <see cref="T:System.Type" /> и удовлетворяющие указанным ограничениям привязки.
    /// 
    ///   Пустой массив типа <see cref="T:System.Type" />, если внутри текущего объекта <see cref="T:System.Type" /> нет вложенных типов, или ни один из вложенных типов не удовлетворяет ограничениям привязки.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetNestedTypes(bindingAttr);
    }

    /// <summary>
    ///   Возвращает указанный вложенный тип, который объявлен этим типом.
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
    ///   Если поиск выполнен успешно, возвращается объект <see cref="T:System.Type" />, предоставляющий вложенный тип, который соответствует указанным требованиям; в противном случае возвращается <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override Type GetNestedType(string name, BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetNestedType(name, bindingAttr);
    }

    /// <summary>
    ///   Возвращает все члены с указанным именем, типом и привязкой, объявленные или наследованные этим типом.
    /// </summary>
    /// <param name="name">Имя элемента.</param>
    /// <param name="type">Тип возвращаемого члена.</param>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющих открытые и не являющиеся открытыми члены, определенные для данного типа, если используется <paramref name="nonPublic" />; в противном случае возвращаются только открытые члены.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetMember(name, type, bindingAttr);
    }

    /// <summary>
    ///   Возвращает указанные члены, объявленные или наследованные данным типом.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющих открытые и не являющиеся открытыми члены, объявленные или наследованные этим типом.
    ///    Если соответствующие члены отсутствуют, возвращается пустой массив.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetMembers(bindingAttr);
    }

    /// <summary>
    ///   Возвращает сопоставление для интерфейса заданного типа.
    /// </summary>
    /// <param name="interfaceType">
    ///   Тип интерфейса, для которого должно быть получено сопоставление интерфейса.
    /// </param>
    /// <returns>Запрошенное сопоставление интерфейса.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип не реализует интерфейс.
    /// </exception>
    [ComVisible(true)]
    public override InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      return this.m_typeBuilder.GetInterfaceMap(interfaceType);
    }

    /// <summary>
    ///   Возвращает открытые и не являющиеся открытыми события, объявленные данным типом.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />, например <see langword="InvokeMethod" />, <see langword="NonPublic" /> и так далее.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.EventInfo" />, представляющих открытые и не являющиеся открытыми события, объявленные или наследованные этим объектом.
    ///    Если заданные события отсутствуют, возвращается пустой массив.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetEvents(bindingAttr);
    }

    protected override TypeAttributes GetAttributeFlagsImpl()
    {
      return this.m_typeBuilder.Attributes;
    }

    protected override bool IsArrayImpl()
    {
      return false;
    }

    protected override bool IsPrimitiveImpl()
    {
      return false;
    }

    protected override bool IsValueTypeImpl()
    {
      return true;
    }

    protected override bool IsByRefImpl()
    {
      return false;
    }

    protected override bool IsPointerImpl()
    {
      return false;
    }

    protected override bool IsCOMObjectImpl()
    {
      return false;
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
    ///   При вызове этого метода всегда возникает исключение <see cref="T:System.NotSupportedException" />.
    /// </summary>
    /// <returns>
    ///   Этот метод не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    /// </exception>
    public override Type GetElementType()
    {
      return this.m_typeBuilder.GetElementType();
    }

    protected override bool HasElementTypeImpl()
    {
      return this.m_typeBuilder.HasElementType;
    }

    /// <summary>
    ///   Возвращает базовый тип integer текущего перечисления, который устанавливается при определении построителя перечисления.
    /// </summary>
    /// <returns>Базовый тип.</returns>
    public override Type GetEnumUnderlyingType()
    {
      return this.m_underlyingField.FieldType;
    }

    /// <summary>
    ///   Возвращает базовый системный тип данного перечисления.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Возвращает базовый системный тип.
    /// </returns>
    public override Type UnderlyingSystemType
    {
      get
      {
        return this.GetEnumUnderlyingType();
      }
    }

    /// <summary>
    ///   Возвращает все настраиваемые атрибуты, определенные для данного конструктора.
    /// </summary>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск в цепочке наследования этого члена для нахождения атрибутов.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов, представляющих все настраиваемые атрибуты конструктора, который представлен этим экземпляром <see cref="T:System.Reflection.Emit.ConstructorBuilder" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.m_typeBuilder.GetCustomAttributes(inherit);
    }

    /// <summary>
    ///   Возвращает настраиваемые атрибуты, определяемые заданным типом.
    /// </summary>
    /// <param name="attributeType">
    ///   Объект <see langword="Type" />, к которому применяются настраиваемые атрибуты.
    /// </param>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск атрибутов в цепочке наследования этого члена.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов, предоставляющих атрибуты данного конструктора, имеют <see cref="T:System.Type" /><paramref name="attributeType" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.m_typeBuilder.GetCustomAttributes(attributeType, inherit);
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
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      this.m_typeBuilder.SetCustomAttribute(con, binaryAttribute);
    }

    /// <summary>
    ///   Задает настраиваемый атрибут с помощью построителя настраиваемых атрибутов.
    /// </summary>
    /// <param name="customBuilder">
    ///   Экземпляр вспомогательного класса для определения настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="con" /> имеет значение <see langword="null" />.
    /// </exception>
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      this.m_typeBuilder.SetCustomAttribute(customBuilder);
    }

    /// <summary>
    ///   Возвращает тип, объявивший этот <see cref="T:System.Reflection.Emit.EnumBuilder" />.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Тип, объявивший этот <see cref="T:System.Reflection.Emit.EnumBuilder" />.
    /// </returns>
    public override Type DeclaringType
    {
      get
      {
        return this.m_typeBuilder.DeclaringType;
      }
    }

    /// <summary>
    ///   Возвращает тип, который был использован для получения этого <see cref="T:System.Reflection.Emit.EnumBuilder" />.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Тип, который был использован для получения этого <see cref="T:System.Reflection.Emit.EnumBuilder" />.
    /// </returns>
    public override Type ReflectedType
    {
      get
      {
        return this.m_typeBuilder.ReflectedType;
      }
    }

    /// <summary>
    ///   Проверяет, определен ли заданный тип настраиваемого атрибута.
    /// </summary>
    /// <param name="attributeType">
    ///   Объект <see langword="Type" />, к которому применяются настраиваемые атрибуты.
    /// </param>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск атрибутов в цепочке наследования этого члена.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если для данного члена определен один или несколько экземпляров <paramref name="attributeType" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается в типах, которые не являются полными.
    /// </exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.m_typeBuilder.IsDefined(attributeType, inherit);
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_typeBuilder.MetadataTokenInternal;
      }
    }

    private EnumBuilder()
    {
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, который представляет указатель на текущий тип.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, который представляет указатель на текущий тип.
    /// </returns>
    public override Type MakePointerType()
    {
      return SymbolType.FormCompoundType("*".ToCharArray(), (Type) this, 0);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, который представляет текущий тип при передаче в качестве параметра ref (параметра ByRef в Visual Basic).
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, который представляет текущий тип при передаче в качестве параметра ref (параметра ByRef в Visual Basic).
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
    /// <param name="rank">
    ///   Размерность массива.
    ///    Это число должно быть меньше либо равно 32.
    /// </param>
    /// <returns>
    ///   Объект, представляющий массив текущего типа указанной размерности.
    /// </returns>
    /// <exception cref="T:System.IndexOutOfRangeException">
    ///   Значение параметра <paramref name="rank" /> меньше 1.
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

    [SecurityCritical]
    internal EnumBuilder(string name, Type underlyingType, TypeAttributes visibility, ModuleBuilder module)
    {
      if ((visibility & ~TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic)
        throw new ArgumentException(Environment.GetResourceString("Argument_ShouldOnlySetVisibilityFlags"), nameof (name));
      this.m_typeBuilder = new TypeBuilder(name, visibility | TypeAttributes.Sealed, typeof (Enum), (Type[]) null, module, PackingSize.Unspecified, 0, (TypeBuilder) null);
      this.m_underlyingField = this.m_typeBuilder.DefineField("value__", underlyingType, FieldAttributes.Public | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName);
    }

    void _EnumBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _EnumBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _EnumBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _EnumBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
