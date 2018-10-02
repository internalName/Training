// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ConstructorBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Определяет и представляет конструктор динамического класса.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_ConstructorBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class ConstructorBuilder : ConstructorInfo, _ConstructorBuilder
  {
    private readonly MethodBuilder m_methodBuilder;
    internal bool m_isDefaultConstructor;

    private ConstructorBuilder()
    {
    }

    [SecurityCritical]
    internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers, ModuleBuilder mod, TypeBuilder type)
    {
      this.m_methodBuilder = new MethodBuilder(name, attributes, callingConvention, (Type) null, (Type[]) null, (Type[]) null, parameterTypes, requiredCustomModifiers, optionalCustomModifiers, mod, type, false);
      type.m_listMethods.Add(this.m_methodBuilder);
      int length;
      this.m_methodBuilder.GetMethodSignature().InternalGetSignature(out length);
      this.m_methodBuilder.GetToken();
    }

    [SecurityCritical]
    internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, ModuleBuilder mod, TypeBuilder type)
      : this(name, attributes, callingConvention, parameterTypes, (Type[][]) null, (Type[][]) null, mod, type)
    {
    }

    internal override Type[] GetParameterTypes()
    {
      return this.m_methodBuilder.GetParameterTypes();
    }

    private TypeBuilder GetTypeBuilder()
    {
      return this.m_methodBuilder.GetTypeBuilder();
    }

    internal ModuleBuilder GetModuleBuilder()
    {
      return this.GetTypeBuilder().GetModuleBuilder();
    }

    /// <summary>
    ///   Возвращает этот экземпляр <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> в виде <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Возвращает строку <see cref="T:System.String" />, содержащую имя, атрибуты и исключения данного конструктора, за которой следует текущий поток MSIL.
    /// </returns>
    public override string ToString()
    {
      return this.m_methodBuilder.ToString();
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_methodBuilder.MetadataTokenInternal;
      }
    }

    /// <summary>
    ///   Возвращает динамический модуль, в котором определен этот конструктор.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Module" />, представляющий динамический модуль, в котором определен этот конструктор.
    /// </returns>
    public override Module Module
    {
      get
      {
        return this.m_methodBuilder.Module;
      }
    }

    /// <summary>
    ///   Содержит ссылку на объект <see cref="T:System.Type" />, из которого был получен данный объект.
    /// </summary>
    /// <returns>
    ///   Возвращает объект <see langword="Type" />, из которого был получен данный объект.
    /// </returns>
    public override Type ReflectedType
    {
      get
      {
        return this.m_methodBuilder.ReflectedType;
      }
    }

    /// <summary>
    ///   Извлекает ссылку на объект <see cref="T:System.Type" /> для типа, который объявляет этот член.
    /// </summary>
    /// <returns>
    ///   Возвращает объект <see cref="T:System.Type" /> для типа, который объявляет этот член.
    /// </returns>
    public override Type DeclaringType
    {
      get
      {
        return this.m_methodBuilder.DeclaringType;
      }
    }

    /// <summary>Получает имя конструктора.</summary>
    /// <returns>Возвращает имя конструктора.</returns>
    public override string Name
    {
      get
      {
        return this.m_methodBuilder.Name;
      }
    }

    /// <summary>
    ///   Динамически вызывает конструктор, отраженный этим экземпляром с указанными аргументами, с учетом ограничений заданного параметра <see langword="Binder" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект, который необходимо повторно инициализировать.
    /// </param>
    /// <param name="invokeAttr">
    ///   Одно из значений <see langword="BindingFlags" />, указывающее нужный тип привязки.
    /// </param>
    /// <param name="binder">
    ///   Параметр <see langword="Binder" />, который определяет набор свойств и обеспечивает привязку, приведение типов аргументов и вызов членов с помощью отражения.
    ///    Если <paramref name="binder" /> имеет значение <see langword="null" />, то используется Binder.DefaultBinding.
    /// </param>
    /// <param name="parameters">
    ///   Список аргументов.
    ///    Это массив аргументов с тем же числом, порядком и типом, что и параметры вызываемого конструктора.
    ///    Если параметров нет, он должен быть пустой ссылкой (<see langword="Nothing" /> в Visual Basic).
    /// </param>
    /// <param name="culture">
    ///   Параметр <see cref="T:System.Globalization.CultureInfo" />, используемый для управления приведением типов.
    ///    Если параметр имеет значение NULL, для текущего потока используется <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <returns>Экземпляр класса, связанный с конструктором.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    ///    Конструктор можно получить с помощью <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> и вызвать <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> для возвращенного объекта <see cref="T:System.Reflection.ConstructorInfo" />.
    /// </exception>
    public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>Возвращает параметры этого конструктора.</summary>
    /// <returns>
    ///   Возвращает массив объектов <see cref="T:System.Reflection.ParameterInfo" />, представляющих параметры этого конструктора.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> не был вызван для этого типа конструктора в платформе .NET Framework версий 1.0 и 1.1.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> не был вызван для этого типа конструктора в платформе .NET Framework версии 2.0.
    /// </exception>
    public override ParameterInfo[] GetParameters()
    {
      return this.GetTypeBuilder().GetConstructor(this.m_methodBuilder.m_parameterTypes).GetParameters();
    }

    /// <summary>Извлекает атрибуты для данного конструктора.</summary>
    /// <returns>Возвращает атрибуты для данного конструктора.</returns>
    public override MethodAttributes Attributes
    {
      get
      {
        return this.m_methodBuilder.Attributes;
      }
    }

    /// <summary>
    ///   Возвращает флаги реализации метода для этого конструктора.
    /// </summary>
    /// <returns>Флаги реализации метода для этого конструктора.</returns>
    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      return this.m_methodBuilder.GetMethodImplementationFlags();
    }

    /// <summary>
    ///   Извлекает внутренний маркер метода.
    ///    Используйте этот дескриптор для доступа к дескриптору базовых метаданных.
    /// </summary>
    /// <returns>
    ///   Возвращает внутренний дескриптор метода.
    ///    Используйте этот дескриптор для доступа к дескриптору базовых метаданных.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Это свойство не поддерживается для этого класса.
    /// </exception>
    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        return this.m_methodBuilder.MethodHandle;
      }
    }

    /// <summary>
    ///   Вызывает конструктор, динамически отраженный этим экземпляром для данного объекта, передавая указанные параметры и учитывая ограничения данного модуля привязки.
    /// </summary>
    /// <param name="invokeAttr">
    ///   Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />, например InvokeMethod, NonPublic и т. д.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see langword="MemberInfo" /> с помощью отражения.
    ///    Если указан модуль привязки <see langword="null" />, используется модуль привязки по умолчанию.
    ///    См. раздел <see cref="T:System.Reflection.Binder" />.
    /// </param>
    /// <param name="parameters">
    ///   Список аргументов.
    ///    Это массив аргументов с тем же числом, порядком и типом, что и параметры вызываемого конструктора.
    ///    Если параметров нет, должно быть указано значение <see langword="null" />.
    /// </param>
    /// <param name="culture">
    ///   Экземпляр объекта <see cref="T:System.Globalization.CultureInfo" />, используемого для управления приведением типов.
    ///    Если значение параметра — NULL, для текущего потока используется <see cref="T:System.Globalization.CultureInfo" />.
    ///    (Например, необходимо преобразовывать объект <see cref="T:System.String" />, представляющий 1000, в значение <see cref="T:System.Double" />, поскольку в разных языках и региональных параметрах 1000 представляется по-разному.)
    /// </param>
    /// <returns>
    ///   Возвращает объект <see cref="T:System.Object" />, который является возвращаемым значением вызываемого конструктора.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    ///    Конструктор можно получить с помощью <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> и вызвать <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> для возвращенного объекта <see cref="T:System.Reflection.ConstructorInfo" />.
    /// </exception>
    public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Возвращает все настраиваемые атрибуты, определенные для этого конструктора.
    /// </summary>
    /// <param name="inherit">
    ///   Управляет наследованием настраиваемых атрибутов базового класса.
    ///    Этот параметр не учитывается.
    /// </param>
    /// <returns>
    ///   Возвращает массив объектов, представляющих все настраиваемые атрибуты конструктора, который представлен этим экземпляром <see cref="T:System.Reflection.Emit.ConstructorBuilder" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    /// </exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.m_methodBuilder.GetCustomAttributes(inherit);
    }

    /// <summary>
    ///   Возвращает настраиваемые атрибуты, определяемые заданным типом.
    /// </summary>
    /// <param name="attributeType">Тип настраиваемого атрибута.</param>
    /// <param name="inherit">
    ///   Управляет наследованием настраиваемых атрибутов базового класса.
    ///    Этот параметр не учитывается.
    /// </param>
    /// <returns>
    ///   Возвращает массив типа <see cref="T:System.Object" />, представляющий атрибуты данного конструктора.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    /// </exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.m_methodBuilder.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>
    ///   Проверяет, определен ли заданный тип настраиваемого атрибута.
    /// </summary>
    /// <param name="attributeType">Тип настраиваемого атрибута.</param>
    /// <param name="inherit">
    ///   Управляет наследованием настраиваемых атрибутов базового класса.
    ///    Этот параметр не учитывается.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если заданный тип настраиваемых атрибутов определен; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    ///    Конструктор можно получить с помощью <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> и вызвать <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> для возвращенного объекта <see cref="T:System.Reflection.ConstructorInfo" />.
    /// </exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.m_methodBuilder.IsDefined(attributeType, inherit);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Reflection.Emit.MethodToken" />, представляющий маркер для этого конструктора.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Reflection.Emit.MethodToken" /> конструктора.
    /// </returns>
    public MethodToken GetToken()
    {
      return this.m_methodBuilder.GetToken();
    }

    /// <summary>Определяет параметр данного конструктора.</summary>
    /// <param name="iSequence">
    ///   Позиция параметра в списке параметров.
    ///    Параметры индексируются, начиная с номера 1 для первого параметра.
    /// </param>
    /// <param name="attributes">Атрибуты параметра.</param>
    /// <param name="strParamName">
    ///   Имя параметра.
    ///    Имя может быть пустой строкой.
    /// </param>
    /// <returns>
    ///   Возвращает объект <see langword="ParameterBuilder" />, представляющий новый параметр этого конструктора.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="iSequence" /> меньше нуля или больше, чем число параметров конструктора.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    public ParameterBuilder DefineParameter(int iSequence, ParameterAttributes attributes, string strParamName)
    {
      attributes &= ~ParameterAttributes.ReservedMask;
      return this.m_methodBuilder.DefineParameter(iSequence, attributes, strParamName);
    }

    /// <summary>
    ///   Задает пользовательский атрибут этого конструктора, связанный с символьными данными.
    /// </summary>
    /// <param name="name">Имя пользовательского атрибута.</param>
    /// <param name="data">Значение пользовательского атрибута.</param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// 
    ///   -или-
    /// 
    ///   Для модуля не определен модуль записи символов.
    ///    Например, модуль не является отладочным.
    /// </exception>
    public void SetSymCustomAttribute(string name, byte[] data)
    {
      this.m_methodBuilder.SetSymCustomAttribute(name, data);
    }

    /// <summary>
    ///   Получает <see cref="T:System.Reflection.Emit.ILGenerator" /> для этого конструктора.
    /// </summary>
    /// <returns>
    ///   Возвращает объект <see cref="T:System.Reflection.Emit.ILGenerator" /> для этого конструктора.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Конструктор является конструктором по умолчанию.
    /// 
    ///   -или-
    /// 
    ///   У конструктора есть флаги <see cref="T:System.Reflection.MethodAttributes" /> или <see cref="T:System.Reflection.MethodImplAttributes" />, указывающие, что он не должен иметь тело метода.
    /// </exception>
    public ILGenerator GetILGenerator()
    {
      if (this.m_isDefaultConstructor)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorILGen"));
      return this.m_methodBuilder.GetILGenerator();
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Reflection.Emit.ILGenerator" /> с указанным размером потока MSIL, который может использоваться для построения тела метода для данного конструктора.
    /// </summary>
    /// <param name="streamSize">Размер потока MSIL (в байтах).</param>
    /// <returns>
    ///   <see cref="T:System.Reflection.Emit.ILGenerator" /> для этого конструктора.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Конструктор является конструктором по умолчанию.
    /// 
    ///   -или-
    /// 
    ///   У конструктора есть флаги <see cref="T:System.Reflection.MethodAttributes" /> или <see cref="T:System.Reflection.MethodImplAttributes" />, указывающие, что он не должен иметь тело метода.
    /// </exception>
    public ILGenerator GetILGenerator(int streamSize)
    {
      if (this.m_isDefaultConstructor)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorILGen"));
      return this.m_methodBuilder.GetILGenerator(streamSize);
    }

    /// <summary>
    ///   Создает тело конструктора с использованием указанного массива байтов инструкций промежуточного языка Майкрософт (MSIL).
    /// </summary>
    /// <param name="il">
    ///   Массив, содержащий допустимые инструкции MSIL.
    /// </param>
    /// <param name="maxStack">Максимальная глубина оценки стека.</param>
    /// <param name="localSignature">
    ///   Массив байтов, содержащий сериализованную структуру локальной переменной.
    ///    Укажите <see langword="null" />, если у конструктора нет локальных переменных.
    /// </param>
    /// <param name="exceptionHandlers">
    ///   Коллекция, содержащая обработчики исключений для конструктора.
    ///    Укажите <see langword="null" />, если у конструктора нет обработчиков исключений.
    /// </param>
    /// <param name="tokenFixups">
    ///   Коллекция значений, которые представляют смещения в <paramref name="il" />, каждый из которых задает начало токена, который может быть изменен.
    ///    Укажите <see langword="null" />, если у конструктора нет токенов, которые должны быть изменены.
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
    ///   Этот метод был вызван ранее с помощью этого объекта <see cref="T:System.Reflection.Emit.ConstructorBuilder" />.
    /// </exception>
    public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
    {
      if (this.m_isDefaultConstructor)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorDefineBody"));
      this.m_methodBuilder.SetMethodBody(il, maxStack, localSignature, exceptionHandlers, tokenFixups);
    }

    /// <summary>
    ///   Добавляет декларативную безопасность в этот конструктор.
    /// </summary>
    /// <param name="action">
    ///   Выполняемое действие безопасности, например Demand, Assert и т. д.
    /// </param>
    /// <param name="pset">
    ///   Набор разрешений, к которому применяется действие.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="action" /> является недопустимым (RequestMinimum, RequestOptional и RequestRefuse являются недопустимыми).
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан ранее с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
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
      if (pset == null)
        throw new ArgumentNullException(nameof (pset));
      if (!Enum.IsDefined(typeof (SecurityAction), (object) action) || action == SecurityAction.RequestMinimum || (action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse))
        throw new ArgumentOutOfRangeException(nameof (action));
      if (this.m_methodBuilder.IsTypeCreated())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
      byte[] blob = pset.EncodeXml();
      TypeBuilder.AddDeclarativeSecurity(this.GetModuleBuilder().GetNativeHandle(), this.GetToken().Token, action, blob, blob.Length);
    }

    /// <summary>
    ///   Получает значение <see cref="T:System.Reflection.CallingConventions" />, которое зависит от того, является ли объявляющий тип универсальным.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="F:System.Reflection.CallingConventions.HasThis" />, если объявляющий тип является универсальным; в противном случае — значение <see cref="F:System.Reflection.CallingConventions.Standard" />.
    /// </returns>
    public override CallingConventions CallingConvention
    {
      get
      {
        return this.DeclaringType.IsGenericType ? CallingConventions.HasThis : CallingConventions.Standard;
      }
    }

    /// <summary>
    ///   Возвращает ссылку на модуль, содержащий этот конструктор.
    /// </summary>
    /// <returns>Модуль, содержащий этот конструктор.</returns>
    public Module GetModule()
    {
      return this.m_methodBuilder.GetModule();
    }

    /// <summary>
    ///   Возвращает <see langword="null" />.
    /// </summary>
    /// <returns>
    ///   Возвращает <see langword="null" />.
    /// </returns>
    [Obsolete("This property has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public Type ReturnType
    {
      get
      {
        return this.GetReturnType();
      }
    }

    internal override Type GetReturnType()
    {
      return this.m_methodBuilder.ReturnType;
    }

    /// <summary>Извлекает сигнатуру поля в виде строки.</summary>
    /// <returns>Возвращает сигнатуру поля.</returns>
    public string Signature
    {
      get
      {
        return this.m_methodBuilder.Signature;
      }
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
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      this.m_methodBuilder.SetCustomAttribute(con, binaryAttribute);
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
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      this.m_methodBuilder.SetCustomAttribute(customBuilder);
    }

    /// <summary>
    ///   Задает флаги реализации метода для этого конструктора.
    /// </summary>
    /// <param name="attributes">Флаги реализации метода.</param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    public void SetImplementationFlags(MethodImplAttributes attributes)
    {
      this.m_methodBuilder.SetImplementationFlags(attributes);
    }

    /// <summary>
    ///   Получает или задает значение, указывающее необходимость инициализации локальных переменных в этом конструкторе нулями.
    /// </summary>
    /// <returns>
    ///   Чтение и запись.
    ///    Получает или задает значение, указывающее необходимость инициализации локальных переменных в этом конструкторе нулями.
    /// </returns>
    public bool InitLocals
    {
      get
      {
        return this.m_methodBuilder.InitLocals;
      }
      set
      {
        this.m_methodBuilder.InitLocals = value;
      }
    }

    void _ConstructorBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _ConstructorBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _ConstructorBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _ConstructorBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
