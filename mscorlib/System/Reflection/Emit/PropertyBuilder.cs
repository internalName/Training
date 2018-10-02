// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.PropertyBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>Определяет свойства для типа.</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_PropertyBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class PropertyBuilder : PropertyInfo, _PropertyBuilder
  {
    private string m_name;
    private PropertyToken m_prToken;
    private int m_tkProperty;
    private ModuleBuilder m_moduleBuilder;
    private SignatureHelper m_signature;
    private PropertyAttributes m_attributes;
    private Type m_returnType;
    private MethodInfo m_getMethod;
    private MethodInfo m_setMethod;
    private TypeBuilder m_containingType;

    private PropertyBuilder()
    {
    }

    internal PropertyBuilder(ModuleBuilder mod, string name, SignatureHelper sig, PropertyAttributes attr, Type returnType, PropertyToken prToken, TypeBuilder containingType)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (name));
      if (name[0] == char.MinValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), nameof (name));
      this.m_name = name;
      this.m_moduleBuilder = mod;
      this.m_signature = sig;
      this.m_attributes = attr;
      this.m_returnType = returnType;
      this.m_prToken = prToken;
      this.m_tkProperty = prToken.Token;
      this.m_containingType = containingType;
    }

    /// <summary>Задает значение по умолчанию этого свойства.</summary>
    /// <param name="defaultValue">
    ///   Значение этого свойства по умолчанию.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван для включающего типа.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство не является одним из поддерживаемых типов.
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="defaultValue" /> не соответствует типу свойства.
    /// 
    ///   -или-
    /// 
    ///   Свойство имеет тип <see cref="T:System.Object" /> или другой ссылочный тип, <paramref name="defaultValue" /> не <see langword="null" />, и значение не может быть назначен ссылочного типа.
    /// </exception>
    [SecuritySafeCritical]
    public void SetConstant(object defaultValue)
    {
      this.m_containingType.ThrowIfCreated();
      TypeBuilder.SetConstantValue(this.m_moduleBuilder, this.m_prToken.Token, this.m_returnType, defaultValue);
    }

    /// <summary>Получает маркер для этого свойства.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Получает маркер для этого свойства.
    /// </returns>
    public PropertyToken PropertyToken
    {
      get
      {
        return this.m_prToken;
      }
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_tkProperty;
      }
    }

    /// <summary>
    ///   Возвращает модуль, в котором определен тип, объявляющий текущее свойство.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.Module" /> В которой определен тип, объявляющий текущее свойство.
    /// </returns>
    public override Module Module
    {
      get
      {
        return this.m_containingType.Module;
      }
    }

    [SecurityCritical]
    private void SetMethodSemantics(MethodBuilder mdBuilder, MethodSemanticsAttributes semantics)
    {
      if ((MethodInfo) mdBuilder == (MethodInfo) null)
        throw new ArgumentNullException(nameof (mdBuilder));
      this.m_containingType.ThrowIfCreated();
      TypeBuilder.DefineMethodSemantics(this.m_moduleBuilder.GetNativeHandle(), this.m_prToken.Token, semantics, mdBuilder.GetToken().Token);
    }

    /// <summary>Задает метод, который возвращает значение свойства.</summary>
    /// <param name="mdBuilder">
    ///   Объект <see langword="MethodBuilder" /> объект, который представляет метод, который возвращает значение свойства.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mdBuilder" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван для включающего типа.
    /// </exception>
    [SecuritySafeCritical]
    public void SetGetMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Getter);
      this.m_getMethod = (MethodInfo) mdBuilder;
    }

    /// <summary>Задает метод, который задает значение свойства.</summary>
    /// <param name="mdBuilder">
    ///   Объект <see langword="MethodBuilder" /> объект, который представляет метод, который задает значение свойства.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mdBuilder" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван для включающего типа.
    /// </exception>
    [SecuritySafeCritical]
    public void SetSetMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Setter);
      this.m_setMethod = (MethodInfo) mdBuilder;
    }

    /// <summary>
    ///   Добавляет один из дополнительных методов, связанных с этим свойством.
    /// </summary>
    /// <param name="mdBuilder">
    ///   Объект <see langword="MethodBuilder" /> представляющий другой метод.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="mdBuilder" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван для включающего типа.
    /// </exception>
    [SecuritySafeCritical]
    public void AddOtherMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Other);
    }

    /// <summary>
    ///   Задает настраиваемый атрибут с помощью большого двоичного объекта настраиваемых атрибутов.
    /// </summary>
    /// <param name="con">Конструктор настраиваемого атрибута.</param>
    /// <param name="binaryAttribute">
    ///   Большой двоичный объект байтов, представляющий атрибуты.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="con" /> или <paramref name="binaryAttribute" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван для включающего типа.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (con));
      if (binaryAttribute == null)
        throw new ArgumentNullException(nameof (binaryAttribute));
      this.m_containingType.ThrowIfCreated();
      TypeBuilder.DefineCustomAttribute(this.m_moduleBuilder, this.m_prToken.Token, this.m_moduleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, false);
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
    ///   Если <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> был вызван для включающего типа.
    /// </exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException(nameof (customBuilder));
      this.m_containingType.ThrowIfCreated();
      customBuilder.CreateCustomAttribute(this.m_moduleBuilder, this.m_prToken.Token);
    }

    /// <summary>
    ///   Возвращает значение индексированного свойства, вызвав метод получения значения свойства.
    /// </summary>
    /// <param name="obj">
    ///   Объект, свойство которого будет возвращено.
    /// </param>
    /// <param name="index">
    ///   Необязательные значения индекса для индексированных свойств.
    ///    Для неиндексированных свойств это значение должно быть равно <see langword="null" />.
    /// </param>
    /// <returns>Значение указанного индексированного свойства.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override object GetValue(object obj, object[] index)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Возвращает значение свойства с указанным связыванием, индексом и <see langword="CultureInfo" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект, свойство которого будет возвращено.
    /// </param>
    /// <param name="invokeAttr">
    ///   Атрибут вызова.
    ///    Это должен быть одноразрядный флаг из <see langword="BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, или <see langword="SetProperty" />.
    ///    Необходимо указать подходящий атрибут вызова.
    ///    Должен быть вызван, если статический член <see langword="Static" /> флаг <see langword="BindingFlags" /> необходимо задать.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see langword="MemberInfo" /> с помощью отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="index">
    ///   Необязательные значения индекса для индексированных свойств.
    ///    Для неиндексированных свойств это значение должно быть равно <see langword="null" />.
    /// </param>
    /// <param name="culture">
    ///   <see langword="CultureInfo" /> Объект, представляющий язык и региональные параметры, для которого ресурс будет локализовано.
    ///    Обратите внимание, что если ресурс не локализован для данной культуры, <see langword="CultureInfo.Parent" /> будет последовательно вызываться метод поиска соответствия.
    ///    Если это значение равно <see langword="null" />,  <see langword="CultureInfo" /> получается из <see langword="CultureInfo.CurrentUICulture" /> свойство.
    /// </param>
    /// <returns>
    ///   Значение свойства для <paramref name="obj" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Задает значение свойства с необязательными значениями индекса для индексированных свойств.
    /// </summary>
    /// <param name="obj">
    ///   Объект, значение свойства которого будет установлено.
    /// </param>
    /// <param name="value">Новое значение для этого свойства.</param>
    /// <param name="index">
    ///   Необязательные значения индекса для индексированных свойств.
    ///    Для неиндексированных свойств это значение должно быть равно <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override void SetValue(object obj, object value, object[] index)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Задает значение свойства для данного объекта на заданное значение.
    /// </summary>
    /// <param name="obj">
    ///   Объект, свойство которого будет возвращено.
    /// </param>
    /// <param name="value">Новое значение для этого свойства.</param>
    /// <param name="invokeAttr">
    ///   Атрибут вызова.
    ///    Это должен быть одноразрядный флаг из <see langword="BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, или <see langword="SetProperty" />.
    ///    Необходимо указать подходящий атрибут вызова.
    ///    Должен быть вызван, если статический член <see langword="Static" /> флаг <see langword="BindingFlags" /> необходимо задать.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see langword="MemberInfo" /> с помощью отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="index">
    ///   Необязательные значения индекса для индексированных свойств.
    ///    Для неиндексированных свойств это значение должно быть равно <see langword="null" />.
    /// </param>
    /// <param name="culture">
    ///   <see langword="CultureInfo" /> Объект, представляющий язык и региональные параметры, для которого ресурс будет локализовано.
    ///    Обратите внимание, что если ресурс не локализован для данной культуры, <see langword="CultureInfo.Parent" /> будет последовательно вызываться метод поиска соответствия.
    ///    Если это значение равно <see langword="null" />,  <see langword="CultureInfo" /> получается из <see langword="CultureInfo.CurrentUICulture" /> свойство.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Возвращает массив открытые и закрытые <see langword="get" /> и <see langword="set" /> методы доступа для данного свойства.
    /// </summary>
    /// <param name="nonPublic">
    ///   Указывает, должны ли возвращаться закрытые методы в <see langword="MethodInfo" /> массива.
    ///   <see langword="true" />, если неоткрытые методы должны быть включены; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see langword="MethodInfo" /> содержащий открытые или закрытые методы доступа или пустой массив, если для данного свойства аксессоры отсутствуют.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override MethodInfo[] GetAccessors(bool nonPublic)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Возвращает открытые и закрытые получения доступа для этого свойства.
    /// </summary>
    /// <param name="nonPublic">
    ///   Указывает, должны ли возвращаться методы доступа get, не являющиеся открытыми.
    ///   <see langword="true" />, если неоткрытые методы должны быть включены; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see langword="MethodInfo" /> объект, представляющий метод доступа get для этого свойства, если <paramref name="nonPublic" /> является <see langword="true" />.
    ///    Возвращает <see langword="null" /> Если <paramref name="nonPublic" /> является <see langword="false" /> и метод доступа get не является открытым, или если <paramref name="nonPublic" /> является <see langword="true" /> но аксессор чтения отсутствует.
    /// </returns>
    public override MethodInfo GetGetMethod(bool nonPublic)
    {
      if (nonPublic || this.m_getMethod == (MethodInfo) null || (this.m_getMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
        return this.m_getMethod;
      return (MethodInfo) null;
    }

    /// <summary>Возвращает метод доступа для этого свойства.</summary>
    /// <param name="nonPublic">
    ///   Указывает, должен ли возвращаться метод доступа, если он не является открытым.
    ///   <see langword="true" />, если неоткрытые методы должны быть включены; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    /// Метод свойства <see langword="Set" /> или <see langword="null" />, как показано в следующей таблице.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Объект <see cref="T:System.Reflection.MethodInfo" />, который предоставляет метод Set для этого свойства.
    /// 
    ///         Метод доступа является открытым.
    /// 
    ///         <paramref name="nonPublic" /> имеет значение True, а закрытые методы могут быть возвращены.
    /// 
    ///         null
    /// 
    ///         <paramref name="nonPublic" /> имеет значение True, но свойство доступно только для чтения.
    /// 
    ///         <paramref name="nonPublic" /> имеет значение False, а метод доступа не является открытым.
    ///       </returns>
    public override MethodInfo GetSetMethod(bool nonPublic)
    {
      if (nonPublic || this.m_setMethod == (MethodInfo) null || (this.m_setMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
        return this.m_setMethod;
      return (MethodInfo) null;
    }

    /// <summary>
    ///   Возвращает массив всех параметров индекса для свойства.
    /// </summary>
    /// <returns>
    ///   Массив элементов типа <see langword="ParameterInfo" />, содержащий параметры для индексов.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override ParameterInfo[] GetIndexParameters()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>Возвращает тип поля данного свойства.</summary>
    /// <returns>Тип этого свойства.</returns>
    public override Type PropertyType
    {
      get
      {
        return this.m_returnType;
      }
    }

    /// <summary>Получает атрибуты данного свойства.</summary>
    /// <returns>Атрибуты данного свойства.</returns>
    public override PropertyAttributes Attributes
    {
      get
      {
        return this.m_attributes;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, можно ли выполнить считывание данного свойства.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если свойство доступно для чтения; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool CanRead
    {
      get
      {
        return this.m_getMethod != (MethodInfo) null;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, можно ли производить запись в данное свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если свойство доступно для записи; в обратном случае — значение <see langword="false" />.
    /// </returns>
    public override bool CanWrite
    {
      get
      {
        return this.m_setMethod != (MethodInfo) null;
      }
    }

    /// <summary>
    ///   Возвращает массив всех настраиваемых атрибутов для этого свойства.
    /// </summary>
    /// <param name="inherit">
    ///   Если <see langword="true" />, просматривается цепочка наследования это свойство, чтобы найти настраиваемые атрибуты
    /// </param>
    /// <returns>Массив настраиваемых атрибутов.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Возвращает массив пользовательских атрибутов, идентифицируемых <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="attributeType">
    ///   Массив настраиваемых атрибутов, принадлежащих указанному типу.
    /// </param>
    /// <param name="inherit">
    ///   Если <see langword="true" />, просматривается цепочка наследования это свойство для обнаружения настраиваемых атрибутов.
    /// </param>
    /// <returns>
    ///   Массив пользовательских атрибутов, определенных для данного отраженного члена или <see langword="null" /> Если атрибуты не определены для этого члена.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Указывает, является ли один или несколько экземпляров <paramref name="attributeType" /> определенные для данного свойства.
    /// </summary>
    /// <param name="attributeType">
    ///   Объект <see langword="Type" />, к которому применяются настраиваемые атрибуты.
    /// </param>
    /// <param name="inherit">
    ///   Указывает, следует ли для обхода цепочку наследования это свойство, чтобы найти настраиваемые атрибуты.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если один или несколько экземпляров <paramref name="attributeType" /> для данного свойства определен; в противном случае <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    void _PropertyBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _PropertyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _PropertyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _PropertyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    /// <summary>Возвращает имя данного элемента.</summary>
    /// <returns>
    ///   A <see cref="T:System.String" /> содержащий имя данного элемента.
    /// </returns>
    public override string Name
    {
      get
      {
        return this.m_name;
      }
    }

    /// <summary>Возвращает класс, объявивший этот член.</summary>
    /// <returns>
    ///   <see langword="Type" /> Объект для класса, который объявляет этот член.
    /// </returns>
    public override Type DeclaringType
    {
      get
      {
        return (Type) this.m_containingType;
      }
    }

    /// <summary>
    ///   Получает объект класса, который использовался для извлечения данного экземпляра объекта <see langword="MemberInfo" />.
    /// </summary>
    /// <returns>
    ///   Объект <see langword="Type" />, с помощью которого был получен данный объект <see langword="MemberInfo" />.
    /// </returns>
    public override Type ReflectedType
    {
      get
      {
        return (Type) this.m_containingType;
      }
    }
  }
}
