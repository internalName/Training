// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._Type
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет открытые члены <see cref="T:System.Type" /> класс в неуправляемый код.
  /// </summary>
  [Guid("BCA8B44D-AAD6-3A86-8AB7-03349F4F2DA2")]
  [CLSCompliant(false)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [TypeLibImportClass(typeof (Type))]
  [ComVisible(true)]
  public interface _Type
  {
    /// <summary>
    ///   Возвращает количество предоставляемых объектом интерфейсов для доступа к сведениям о типе (0 или 1).
    /// </summary>
    /// <param name="pcTInfo">
    ///   Указатель, по которому записывается число предоставляемых объектом интерфейсов, предназначенных для получения сведений о типе.
    /// </param>
    void GetTypeInfoCount(out uint pcTInfo);

    /// <summary>
    ///   Возвращает сведения о типе объекта, которые затем могут использоваться для получения сведений о типе интерфейса.
    /// </summary>
    /// <param name="iTInfo">Возвращаемые сведения о типе.</param>
    /// <param name="lcid">
    ///   Идентификатор языкового стандарта для сведений о типе.
    /// </param>
    /// <param name="ppTInfo">
    ///   Получает указатель на объект с запрошенными сведениями о типе.
    /// </param>
    void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

    /// <summary>
    ///   Сопоставляет набор имен соответствующему набору идентификаторов диспетчеризации.
    /// </summary>
    /// <param name="riid">
    ///   Зарезервировано для будущего использования.
    ///    Должно быть равным IID_NULL.
    /// </param>
    /// <param name="rgszNames">
    ///   Переданный массив имен, которые необходимо сопоставить.
    /// </param>
    /// <param name="cNames">Количество сопоставляемых имен.</param>
    /// <param name="lcid">
    ///   Контекст языкового стандарта для интерпретации имен.
    /// </param>
    /// <param name="rgDispId">
    ///   Массив, зарезервированный вызывающим объектом, куда помещаются идентификаторы, соответствующие именам.
    /// </param>
    void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

    /// <summary>
    ///   Предоставляет доступ к открытым свойствам и методам объекта.
    /// </summary>
    /// <param name="dispIdMember">Идентифицирует член.</param>
    /// <param name="riid">
    ///   Зарезервировано для будущего использования.
    ///    Должно быть равным IID_NULL.
    /// </param>
    /// <param name="lcid">
    ///   Контекст языкового стандарта, в котором следует интерпретировать аргументы.
    /// </param>
    /// <param name="wFlags">Флаги, описывающие контекст вызова.</param>
    /// <param name="pDispParams">
    ///   Указатель на структуру, содержащую массив аргументов, массив DISPID для именованных аргументов, а также счетчики количества элементов в массивах.
    /// </param>
    /// <param name="pVarResult">
    ///   Указатель, по которому будет сохранен результат.
    /// </param>
    /// <param name="pExcepInfo">
    ///   Указатель на структуру, содержащую сведения об исключении.
    /// </param>
    /// <param name="puArgErr">
    ///   Индекс первого аргумента, вызвавшего ошибку.
    /// </param>
    void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.ToString" /> метод.
    /// </summary>
    /// <returns>
    ///   Объект типа <see cref="T:System.String" />, представляющий имя текущего объекта <see cref="T:System.Type" />.
    /// </returns>
    string ToString();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.Equals(System.Object)" /> метод.
    /// </summary>
    /// <param name="other">
    ///   <see cref="T:System.Object" /> —, Базовый системный тип которого сравнивается с базовым системным типом текущего объекта <see cref="T:System.Type" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если базовый системный тип параметра <paramref name="o" /> совпадает с базовым системным типом текущего объекта <see cref="T:System.Type" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool Equals(object other);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetHashCode" /> метод.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Int32" /> Содержащий хэш-код данного экземпляра.
    /// </returns>
    int GetHashCode();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetType" /> метод.
    /// </summary>
    /// <returns>
    ///   Текущий <see cref="T:System.Type" />.
    /// </returns>
    Type GetType();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.MemberType" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.Reflection.MemberTypes" />, позволяющее определить, каким типом является этот член: обычным или вложенным.
    /// </returns>
    MemberTypes MemberType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MemberInfo.Name" /> свойство.
    /// </summary>
    /// <returns>
    ///   Имя <see cref="T:System.Type" />.
    /// </returns>
    string Name { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.DeclaringType" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Объект для класса, который объявляет этот член.
    ///    Если тип является вложенным типом, это свойство возвращает включающего типа.
    /// </returns>
    Type DeclaringType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.ReflectedType" /> свойство.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, с помощью которого был получен данный объект <see cref="T:System.Reflection.MemberInfo" />.
    /// </returns>
    Type ReflectedType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Type,System.Boolean)" /> метод.
    /// </summary>
    /// <param name="attributeType">
    ///   Тип атрибута для поиска.
    ///    Возвращаются только те атрибуты, которые можно назначить этому типу.
    /// </param>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск атрибутов в цепочке наследования этого члена.
    /// </param>
    /// <returns>
    ///   Массив настраиваемых атрибутов, примененных к данному члену, или массив нулей (0), если атрибуты не применялись.
    /// </returns>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск атрибутов в цепочке наследования этого члена.
    /// </param>
    /// <returns>
    ///   Массив настраиваемых атрибутов, примененных к данному члену, или массив нулей (0), если атрибуты не применялись.
    /// </returns>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> метод.
    /// </summary>
    /// <param name="attributeType">
    ///   Объект <see langword="Type" />, к которому применяются настраиваемые атрибуты.
    /// </param>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск атрибутов в цепочке наследования этого члена.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если один или несколько экземпляров <paramref name="attributeType" /> применяется к данному члену; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsDefined(Type attributeType, bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.GUID" /> свойство.
    /// </summary>
    /// <returns>
    ///   Идентификатор GUID, связанный с объектом <see cref="T:System.Type" />.
    /// </returns>
    Guid GUID { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.Module" /> свойство.
    /// </summary>
    /// <returns>
    ///   Имя модуля, в котором текущий <see cref="T:System.Type" /> определен.
    /// </returns>
    Module Module { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.Assembly" /> свойство.
    /// </summary>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Reflection.Assembly" />, описывающий сборку, которая содержит текущий тип.
    /// </returns>
    Assembly Assembly { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.TypeHandle" /> свойство.
    /// </summary>
    /// <returns>
    ///   Дескриптор текущего объекта <see cref="T:System.Type" />.
    /// </returns>
    RuntimeTypeHandle TypeHandle { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.FullName" /> свойство.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая полное имя <see cref="T:System.Type" />, включая пространство имен <see cref="T:System.Type" /> но не сборку.
    /// </returns>
    string FullName { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.Namespace" /> свойство.
    /// </summary>
    /// <returns>
    ///   Пространство имен <see cref="T:System.Type" />.
    /// </returns>
    string Namespace { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.AssemblyQualifiedName" /> свойство.
    /// </summary>
    /// <returns>
    ///   Имя сборки <see cref="T:System.Type" />, включая имя сборки, из которой <see cref="T:System.Type" /> была загружена.
    /// </returns>
    string AssemblyQualifiedName { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetArrayRank" /> метод.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Int32" /> Содержащее количество измерений текущего <see cref="T:System.Type" />.
    /// </returns>
    int GetArrayRank();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.BaseType" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Из которого текущий <see cref="T:System.Type" /> прямо наследует или <see langword="null" /> Если текущий <see langword="Type" /> представляет <see cref="T:System.Object" /> класса.
    /// </returns>
    Type BaseType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetConstructors(System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.ConstructorInfo" />, представляющий все конструкторы, определенные для текущего объекта <see cref="T:System.Type" /> и удовлетворяющие указанным ограничениям привязки, в том числе и инициализатор типа, если он определен.
    ///    Возвращает пустой массив типа <see cref="T:System.Reflection.ConstructorInfo" /> если не определены конструкторы для текущего <see cref="T:System.Type" />, если ни один из определенных конструкторов не соответствует ограничениям привязки, или если текущий <see cref="T:System.Type" /> представляет параметр типа в определении универсального типа или метода.
    /// </returns>
    ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetInterface(System.String,System.Boolean)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащее имя интерфейса.
    ///    Для универсальных интерфейсов это искаженное имя.
    /// </param>
    /// <param name="ignoreCase">
    ///   <see langword="true" /> Чтобы выполнить поиск без учета регистра для <paramref name="name" />.
    /// 
    ///   -или-
    /// 
    ///   <see langword="false" /> для выполнения поиска с учетом регистра для <paramref name="name" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий интерфейс с заданным именем, реализованных или наследуемых текущим объектом <see cref="T:System.Type" />, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    Type GetInterface(string name, bool ignoreCase);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetInterfaces" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, представляющий все интерфейсы, реализуемые или наследуемые текущим типом <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Type" /> в случае отсутствия интерфейсов, реализуемых или наследуемых текущим типом <see cref="T:System.Type" />.
    /// </returns>
    Type[] GetInterfaces();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.FindInterfaces(System.Reflection.TypeFilter,System.Object)" /> метод.
    /// </summary>
    /// <param name="filter">
    ///   <see cref="T:System.Reflection.TypeFilter" /> Делегат, сравнивающий интерфейсы с параметром <paramref name="filterCriteria" />.
    /// </param>
    /// <param name="filterCriteria">
    ///   Критерий поиска, определяющий, должен ли тот или иной интерфейс включаться в возвращаемый массив.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Type" /> объектов, представляющий отфильтрованный список интерфейсов, реализованных или наследуемых текущим <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Type" />, если отсутствуют интерфейсы, совпадающих с фильтром, реализованные или наследуемые текущим <see cref="T:System.Type" />.
    /// </returns>
    Type[] FindInterfaces(TypeFilter filter, object filterCriteria);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetEvent(System.String,System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя события, которое объявлено или унаследовано в текущем объекте <see cref="T:System.Type" />.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Reflection.EventInfo" /> Объект, представляющий указанное событие, которое объявлено или унаследовано в текущем объекте <see cref="T:System.Type" />, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    EventInfo GetEvent(string name, BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetEvents" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив <see cref="T:System.Reflection.EventInfo" /> объектов, представляющих все открытые события, которые объявлены или унаследованы текущим объектом <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.EventInfo" />, если в текущем объекте <see cref="T:System.Type" /> нет открытых событий.
    /// </returns>
    EventInfo[] GetEvents();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetEvents(System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.EventInfo" />, представляющий все события, которые объявлены или унаследованы данным объектом <see cref="T:System.Type" /> и удовлетворяют указанным ограничениям привязки.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.EventInfo" />, если у текущего типа <see cref="T:System.Type" /> нет событий или ни одно событие не удовлетворяет ограничениям привязки.
    /// </returns>
    EventInfo[] GetEvents(BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetNestedTypes(System.Reflection.BindingFlags)" /> метод и ищет типы, вложенные в текущий <see cref="T:System.Type" />, используя заданные ограничения привязки.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, представляющий все типы, вложенные внутри текущего типа <see cref="T:System.Type" /> и удовлетворяющие указанным ограничениям привязки.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Type" />, если внутри текущего объекта <see cref="T:System.Type" /> нет вложенных типов или ни один из вложенных типов не удовлетворяет ограничениям привязки.
    /// </returns>
    Type[] GetNestedTypes(BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetNestedType(System.String,System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя искомого вложенного типа.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен успешно, возвращается объект <see cref="T:System.Type" />, предоставляющий вложенный тип, который соответствует указанным требованиям; в противном случае — значение <see langword="null" />.
    /// </returns>
    Type GetNestedType(string name, BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMember(System.String,System.Reflection.MemberTypes,System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя для поиска элементов.
    /// </param>
    /// <param name="type">
    ///   <see cref="T:System.Reflection.MemberTypes" /> Значение для поиска.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Ноль для возвращения пустого массива.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющий открытые члены с заданным именем, если такие члены есть, и пустой массив, если таких членов нет.
    /// </returns>
    MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetDefaultMembers" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющий все члены по умолчанию текущего объекта <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.MemberInfo" />, если у текущего типа <see cref="T:System.Type" /> нет членов по умолчанию.
    /// </returns>
    MemberInfo[] GetDefaultMembers();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.FindMembers(System.Reflection.MemberTypes,System.Reflection.BindingFlags,System.Reflection.MemberFilter,System.Object)" /> метод.
    /// </summary>
    /// <param name="memberType">
    ///   Объект <see langword="MemberTypes" /> показывающий тип элемента для поиска.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="filter">
    ///   Делегат, выполняющий сравнение и возвращающий <see langword="true" />, если проверяемый член соответствует условиям, заданным в параметре <paramref name="filterCriteria" />, и <see langword="false" /> в противном случае.
    ///    Можно использовать делегаты <see langword="FilterAttribute" />, <see langword="FilterName" /> и <see langword="FilterNameIgnoreCase" />, предоставляемые этим классом.
    ///    Первый делегат в качестве условий поиска использует поля классов <see langword="FieldAttributes" />, <see langword="MethodAttributes" /> и <see langword="MethodImplAttributes" />, а два других делегата — объекты <see langword="String" />.
    /// </param>
    /// <param name="filterCriteria">
    ///   Условие поиска, определяющее, будет ли член включен в возвращаемый массив объектов <see langword="MemberInfo" />.
    /// 
    ///   Поля классов <see langword="FieldAttributes" />, <see langword="MethodAttributes" /> и <see langword="MethodImplAttributes" /> могут использоваться вместе с делегатом <see langword="FilterAttribute" />, предоставляемым этим классом.
    /// </param>
    /// <returns>
    ///   Отфильтрованный массив объектов <see cref="T:System.Reflection.MemberInfo" />, имеющих тип указанного члена.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Reflection.MemberInfo" />, если у текущего типа <see cref="T:System.Type" /> нет членов типа <paramref name="memberType" />, удовлетворяющих условиям фильтра.
    /// </returns>
    MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetElementType" /> метод.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Входящей объекта, на который ссылается текущий тип массива, указателя или ссылки.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> Если текущий <see cref="T:System.Type" /> не является массивом или указателем, не передается по ссылке или представляет универсальный тип или параметр типа в определении универсального типа или метода.
    /// </returns>
    Type GetElementType();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.IsSubclassOf(System.Type)" /> метод.
    /// </summary>
    /// <param name="c">
    ///   Объект <see cref="T:System.Type" />, с которым сравнивается текущий объект <see cref="T:System.Type" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Type" /> представленного <paramref name="c" /> параметра и текущий <see cref="T:System.Type" /> представляют классы и класс, представленный текущим <see cref="T:System.Type" /> является производным от класса, представленного параметром <paramref name="c" />; в противном случае — <see langword="false" />.
    ///    Этот метод также возвращает <see langword="false" /> Если <paramref name="c" /> и текущий <see cref="T:System.Type" /> представляют того же класса.
    /// </returns>
    bool IsSubclassOf(Type c);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.IsInstanceOfType(System.Object)" /> метод.
    /// </summary>
    /// <param name="o">
    ///   Объект, который требуется сравнить с текущим объектом <see cref="T:System.Type" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see cref="T:System.Type" /> входит в иерархию наследования объекта, представленного параметром <paramref name="o" /> или если текущий объект <see cref="T:System.Type" /> является интерфейсом, поддерживаемым параметром <paramref name="o" />.
    ///    Значение <see langword="false" />, если не выполняется ни одно из перечисленных условий, или параметр <paramref name="o" /> имеет значение <see langword="null" />, или текущий объект <see cref="T:System.Type" /> является открытым универсальным типом (то есть свойство <see cref="P:System.Type.ContainsGenericParameters" /> возвращает значение <see langword="true" />).
    /// </returns>
    bool IsInstanceOfType(object o);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.IsAssignableFrom(System.Type)" /> метод.
    /// </summary>
    /// <param name="c">
    ///   Объект <see cref="T:System.Type" />, с которым сравнивается текущий объект <see cref="T:System.Type" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="c" /> и текущий <see cref="T:System.Type" /> представляют один и тот же тип, или если текущий <see cref="T:System.Type" /> входит в иерархию наследования <paramref name="c" />, или, если текущий <see cref="T:System.Type" /> является интерфейсом, <paramref name="c" /> реализует, или, если <paramref name="c" /> является параметром универсального типа и текущий <see cref="T:System.Type" /> представляет одно из ограничений <paramref name="c" />.
    ///   <see langword="false" /> Если ни одно из этих условий или если <paramref name="c" /> является <see langword="null" />.
    /// </returns>
    bool IsAssignableFrom(Type c);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetInterfaceMap(System.Type)" /> метод.
    /// </summary>
    /// <param name="interfaceType">
    ///   <see cref="T:System.Type" /> Интерфейса, для которого необходимо найти сопоставление.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Reflection.InterfaceMapping" /> Объект, представляющий сопоставление интерфейса для <paramref name="interfaceType" />.
    /// </returns>
    InterfaceMapping GetInterfaceMap(Type interfaceType);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type[],System.Reflection.ParameterModifier[])" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого метода.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект <see cref="T:System.Reflection.Binder" /> объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />, чтобы использовать <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого метода.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить метод, который не имеет параметров.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> объект, представляющий метод, который соответствует указанным требованиям, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого метода.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> объект, представляющий метод, который соответствует указанным требованиям, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    MethodInfo GetMethod(string name, BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMethods(System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MethodInfo" />, представляющий все методы, определенные для текущего типа <see cref="T:System.Type" /> и удовлетворяющие указанным ограничениям привязки.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.MethodInfo" />, если для текущего объекта <see cref="T:System.Type" /> не определены методы или ни один из определенных методов не удовлетворяет ограничениям привязки.
    /// </returns>
    MethodInfo[] GetMethods(BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetField(System.String,System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого поля данных.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.FieldInfo" /> объект, представляющий поле, которое соответствует указанным требованиям, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    FieldInfo GetField(string name, BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetFields(System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.FieldInfo" />, представляющий все поля, определенные для текущего типа <see cref="T:System.Type" /> и удовлетворяющие указанным ограничениям привязки.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.FieldInfo" />, если для текущего объекта <see cref="T:System.Type" /> не определены поля или ни одно из определенных полей не удовлетворяет ограничениям привязки.
    /// </returns>
    FieldInfo[] GetFields(BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetProperty(System.String,System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащее имя свойства.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.PropertyInfo" /> объект, предоставляющий свойство, которое соответствует указанным требованиям, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetProperty(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type,System.Type[],System.Reflection.ParameterModifier[])" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащее имя свойства.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект <see cref="T:System.Reflection.Binder" /> объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />, чтобы использовать <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="returnType">Возвращаемый тип свойства.</param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого индексированного свойства.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить неиндексированное свойство.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.PropertyInfo" /> объект, предоставляющий свойство, которое соответствует указанным требованиям, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetProperties(System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.PropertyInfo" />, представляющий все свойства текущего типа <see cref="T:System.Type" />, которые удовлетворяют указанным ограничениям привязки.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.PropertyInfo" />, если у текущего типа <see cref="T:System.Type" /> нет свойств или ни одно свойство не удовлетворяет ограничениям привязки.
    /// </returns>
    PropertyInfo[] GetProperties(BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMember(System.String,System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя для поиска элементов.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Ноль для возвращения пустого массива.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющий открытые члены с заданным именем, если такие члены есть, и пустой массив, если таких членов нет.
    /// </returns>
    MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMembers(System.Reflection.BindingFlags)" /> метод.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющий все члены, определенные для текущего типа <see cref="T:System.Type" /> и удовлетворяющие указанным ограничениям привязки.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.MemberInfo" />, если для текущего объекта <see cref="T:System.Type" /> не определены члены или ни один из определенных членов не удовлетворяет ограничениям привязки.
    /// </returns>
    MemberInfo[] GetMembers(BindingFlags bindingAttr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащий имя конструктора, метода, свойства или поля.
    /// 
    ///   -или-
    /// 
    ///   Пустая строка ("") — в этом случае будет вызван член по умолчанию.
    /// 
    ///   -или-
    /// 
    ///   Для членов интерфейса IDispatch, строка, представляющая идентификатор DispID, например «[DispID = 3]».
    /// </param>
    /// <param name="invokeAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    ///    Тип доступа может быть обозначен одним из флагов <see langword="BindingFlags" />, например <see langword="Public" />, <see langword="NonPublic" />, <see langword="Private" />, <see langword="InvokeMethod" />, <see langword="GetField" /> и т.д.
    ///    Тип поиска указывать необязательно.
    ///    Если тип поиска указан, <see langword="BindingFlags.Public" /> | <see langword="BindingFlags.Instance" /> будут применены.
    /// </param>
    /// <param name="binder">
    ///   Объект <see cref="T:System.Reflection.Binder" /> объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />, чтобы использовать <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="target">
    ///   <see cref="T:System.Object" /> Для которого следует вызвать указанный член.
    /// </param>
    /// <param name="args">
    ///   Массив с аргументами, передаваемыми вызываемому члену.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="args" />.
    ///    Атрибуты, связанные с параметром, хранятся в сигнатуре члена.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <param name="culture">
    ///   Объект <see cref="T:System.Globalization.CultureInfo" />, представляющий используемый языковой стандарт глобализации. Он может понадобиться для выполнения преобразований, зависящих от языкового стандарта, например приведения числа в строковом формате к типу Double.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> Чтобы использовать текущий поток <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="namedParameters">
    ///   Массив, содержащий имена параметров, в которые передаются значения элементов массива <paramref name="args" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Object" /> Представляет возвращаемое значение вызываемого элемента.
    /// </returns>
    object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.UnderlyingSystemType" /> свойство.
    /// </summary>
    /// <returns>
    ///   Базовый системный тип текущего типа <see cref="T:System.Type" />.
    /// </returns>
    Type UnderlyingSystemType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Globalization.CultureInfo)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащий имя конструктора, метода, свойства или поля.
    /// 
    ///   -или-
    /// 
    ///   Пустая строка ("") — в этом случае будет вызван член по умолчанию.
    /// 
    ///   -или-
    /// 
    ///   Для членов интерфейса IDispatch, строка, представляющая идентификатор DispID, например «[DispID = 3]».
    /// </param>
    /// <param name="invokeAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    ///    Тип доступа может быть обозначен одним из флагов <see langword="BindingFlags" />, например <see langword="Public" />, <see langword="NonPublic" />, <see langword="Private" />, <see langword="InvokeMethod" />, <see langword="GetField" /> и т.д.
    ///    Тип поиска указывать необязательно.
    ///    Если тип поиска указан, <see langword="BindingFlags.Public" /> | <see langword="BindingFlags.Instance" /> будут применены.
    /// </param>
    /// <param name="binder">
    ///   Объект <see cref="T:System.Reflection.Binder" /> объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />, чтобы использовать <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="target">
    ///   <see cref="T:System.Object" /> Для которого следует вызвать указанный член.
    /// </param>
    /// <param name="args">
    ///   Массив с аргументами, передаваемыми вызываемому члену.
    /// </param>
    /// <param name="culture">
    ///   Объект <see cref="T:System.Globalization.CultureInfo" />, представляющий используемый языковой стандарт глобализации. Он может понадобиться для выполнения преобразований, зависящих от языкового стандарта, например приведения числа в строковом формате к типу Double.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" /> Чтобы использовать текущий поток <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Object" /> Представляет возвращаемое значение вызываемого элемента.
    /// </returns>
    object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[])" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащий имя конструктора, метода, свойства или поля.
    /// 
    ///   -или-
    /// 
    ///   Пустая строка ("") — в этом случае будет вызван член по умолчанию.
    /// 
    ///   -или-
    /// 
    ///   Для членов интерфейса IDispatch, строка, представляющая идентификатор DispID, например «[DispID = 3]».
    /// </param>
    /// <param name="invokeAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    ///    Тип доступа может быть обозначен одним из флагов <see langword="BindingFlags" />, например <see langword="Public" />, <see langword="NonPublic" />, <see langword="Private" />, <see langword="InvokeMethod" />, <see langword="GetField" /> и т.д.
    ///    Тип поиска указывать необязательно.
    ///    Если тип поиска указан, <see langword="BindingFlags.Public" /> | <see langword="BindingFlags.Instance" /> будут применены.
    /// </param>
    /// <param name="binder">
    ///   Объект <see cref="T:System.Reflection.Binder" /> объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />, чтобы использовать <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="target">
    ///   <see cref="T:System.Object" /> Для которого следует вызвать указанный член.
    /// </param>
    /// <param name="args">
    ///   Массив с аргументами, передаваемыми вызываемому члену.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Object" /> Представляет возвращаемое значение вызываемого элемента.
    /// </returns>
    object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> метод.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект <see cref="T:System.Reflection.Binder" /> объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />, чтобы использовать <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="callConvention">
    ///   <see cref="T:System.Reflection.CallingConventions" /> Объекта, который задает набор правил, касающихся порядка и расположения аргументов, способа передачи возвращаемого значения, регистров, используемых для аргументов, и очистки стека.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров, извлекаемых конструктором.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить конструктор, который не имеет параметров.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен удачно, возвращается объект <see cref="T:System.Reflection.ConstructorInfo" />, представляющий конструктор, который соответствует указанным требованиям; в противном случае возвращается значение <see langword="null" />.
    /// </returns>
    ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Type[],System.Reflection.ParameterModifier[])" /> метод.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект <see cref="T:System.Reflection.Binder" /> объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />, чтобы использовать <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров, извлекаемых конструктором.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить конструктор, который не имеет параметров.
    /// 
    ///   -или-
    /// 
    ///   <see cref="F:System.Type.EmptyTypes" />.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве типов параметра.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен удачно, возвращается объект <see cref="T:System.Reflection.ConstructorInfo" />, представляющий конструктор, который соответствует указанным требованиям; в противном случае возвращается значение <see langword="null" />.
    /// </returns>
    ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetConstructor(System.Type[])" /> метод.
    /// </summary>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющих число, порядок и тип параметров нужного конструктора.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов <see cref="T:System.Type" /> для получения конструктора, не имеющего параметров.
    ///    Подобный пустой массив предоставляется полем <see langword="static" /> с описателем <see cref="F:System.Type.EmptyTypes" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.ConstructorInfo" /> объект, представляющий конструктор экземпляра, параметры которого соответствуют типам в массиве типов параметров, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    ConstructorInfo GetConstructor(Type[] types);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetConstructors" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.ConstructorInfo" />, представляющий все открытые конструкторы экземпляров, определенные для текущего типа <see cref="T:System.Type" />, за исключением инициализатора типа (статический конструктор).
    ///    Если для текущего определены открытые конструкторы экземпляров <see cref="T:System.Type" />, или если текущий <see cref="T:System.Type" /> представляет параметр типа универсального типа или метода определения, пустой массив типа <see cref="T:System.Reflection.ConstructorInfo" /> возвращается.
    /// </returns>
    ConstructorInfo[] GetConstructors();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.TypeInitializer" /> свойство.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Reflection.ConstructorInfo" /> содержащий имя конструктора класса <see cref="T:System.Type" />.
    /// </returns>
    ConstructorInfo TypeInitializer { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого метода.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект <see cref="T:System.Reflection.Binder" /> объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />, чтобы использовать <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="callConvention">
    ///   <see cref="T:System.Reflection.CallingConventions" /> Определяющий набор применяемых правил, касающихся порядка и расположения аргументов, способа передачи возвращаемого значения, регистров, используемых для аргументов, и способа очистки стека.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого метода.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить метод, который не имеет параметров.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> объект, представляющий метод, который соответствует указанным требованиям, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMethod(System.String,System.Type[],System.Reflection.ParameterModifier[])" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого открытого метода.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого метода.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить метод, который не имеет параметров.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> объект, представляющий открытый метод, который соответствует указанным требованиям, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMethod(System.String,System.Type[])" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого открытого метода.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого метода.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить метод, который не имеет параметров.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> объект, представляющий открытый метод, параметры которого соответствуют указанным типам аргументов, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    MethodInfo GetMethod(string name, Type[] types);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMethod(System.String)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого открытого метода.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> объект, представляющий открытый метод с заданным именем, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    MethodInfo GetMethod(string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMethods" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MethodInfo" />, представляющий все открытые методы, определенные для текущего объекта <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.MethodInfo" />, если для текущего типа <see cref="T:System.Type" /> открытые методы не определены.
    /// </returns>
    MethodInfo[] GetMethods();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetField(System.String)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого поля данных.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.FieldInfo" /> объект, представляющий открытое поле с указанным именем, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    FieldInfo GetField(string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetFields" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.FieldInfo" />, представляющий все открытые поля, определенные для текущего объекта <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.FieldInfo" />, если для текущего типа <see cref="T:System.Type" /> открытые поля не определены.
    /// </returns>
    FieldInfo[] GetFields();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetInterface(System.String)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащее имя интерфейса.
    ///    Для универсальных интерфейсов это искаженное имя.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий интерфейс с заданным именем, реализованных или наследуемых текущим объектом <see cref="T:System.Type" />, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    Type GetInterface(string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetEvent(System.String)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.EventInfo" />, представляющий все события, которые объявлены или унаследованы данным объектом <see cref="T:System.Type" /> и удовлетворяют указанным ограничениям привязки.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.EventInfo" />, если у текущего типа <see cref="T:System.Type" /> нет событий или ни одно событие не удовлетворяет ограничениям привязки.
    /// </returns>
    EventInfo GetEvent(string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetProperty(System.String,System.Type,System.Type[],System.Reflection.ParameterModifier[])" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого открытого свойства.
    /// </param>
    /// <param name="returnType">
    ///   Тип возвращаемого значения свойства.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого индексированного свойства.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить неиндексированное свойство.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.PropertyInfo" /> объект, представляющий открытое свойство, которое соответствует указанным требованиям, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetProperty(System.String,System.Type,System.Type[])" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого открытого свойства.
    /// </param>
    /// <param name="returnType">
    ///   Тип возвращаемого значения свойства.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого индексированного свойства.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить неиндексированное свойство.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.PropertyInfo" /> объект, представляющий открытое свойство, параметры которого соответствуют указанным типам аргументов, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    PropertyInfo GetProperty(string name, Type returnType, Type[] types);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetProperty(System.String,System.Type[])" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого открытого свойства.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого индексированного свойства.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить неиндексированное свойство.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.PropertyInfo" /> объект, представляющий открытое свойство, параметры которого соответствуют указанным типам аргументов, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    PropertyInfo GetProperty(string name, Type[] types);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetProperty(System.String,System.Type)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого открытого свойства.
    /// </param>
    /// <param name="returnType">
    ///   Тип возвращаемого значения свойства.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.PropertyInfo" /> объект, представляющий открытое свойство с заданным именем, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    PropertyInfo GetProperty(string name, Type returnType);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetProperty(System.String)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомого открытого свойства.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.PropertyInfo" /> объект, представляющий открытое свойство с заданным именем, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    PropertyInfo GetProperty(string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetProperties" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.PropertyInfo" />, представляющий все открытые свойства текущего типа <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.PropertyInfo" />, если у текущего типа <see cref="T:System.Type" /> нет открытых свойств.
    /// </returns>
    PropertyInfo[] GetProperties();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetNestedTypes" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив <see cref="T:System.Type" /> объекты, представляющие все типы, вложенные в текущий <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Type" />, если отсутствуют типы, вложенные в текущий <see cref="T:System.Type" />.
    /// </returns>
    Type[] GetNestedTypes();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetNestedType(System.String)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя искомого вложенного типа.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий открытый вложенный тип с заданным именем, если он найден; в противном случае — <see langword="null" />.
    /// </returns>
    Type GetNestedType(string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMember(System.String)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   <see cref="T:System.String" /> Содержащая имя искомых открытых членов для получения.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющий открытые члены с заданным именем, если такие члены есть, и пустой массив, если таких членов нет.
    /// </returns>
    MemberInfo[] GetMember(string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetMembers" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющий все открытые члены текущего типа <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.MemberInfo" />, если у текущего типа <see cref="T:System.Type" /> нет открытых членов.
    /// </returns>
    MemberInfo[] GetMembers();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.Attributes" /> свойство.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.TypeAttributes" />, представляющий набор атрибутов типа <see cref="T:System.Type" />, если <see cref="T:System.Type" /> не представляет параметр универсального типа. В противном случае это значение не определено.
    /// </returns>
    TypeAttributes Attributes { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsNotPublic" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если верхнего уровня <see cref="T:System.Type" /> не объявляется открытый; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsNotPublic { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsPublic" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если верхнего уровня <see cref="T:System.Type" /> объявлен как открытый; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsPublic { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsNestedPublic" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если данный класс является вложенным и объявленным как открытый; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsNestedPublic { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsNestedPrivate" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является вложенным и объявленным как закрытый; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsNestedPrivate { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsNestedFamily" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является вложенным и видимым только внутри собственного семейства; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsNestedFamily { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsNestedAssembly" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является вложенным и видимым только в своей сборке; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsNestedAssembly { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsNestedFamANDAssem" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является вложенным и видимым только классам, принадлежащим одновременно к семейству и сборке этого объекта; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsNestedFamANDAssem { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsNestedFamORAssem" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является вложенным и видимым только классам, принадлежащим его семейству или его сборке; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsNestedFamORAssem { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsAutoLayout" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если атрибут размещения класса <see langword="AutoLayout" /> выбран для <see cref="T:System.Type" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsAutoLayout { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsLayoutSequential" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если атрибут размещения класса <see langword="SequentialLayout" /> выбран для <see cref="T:System.Type" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsLayoutSequential { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsExplicitLayout" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если атрибут размещения класса <see langword="ExplicitLayout" /> выбран для <see cref="T:System.Type" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsExplicitLayout { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsClass" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является классом; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsClass { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsInterface" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является интерфейсом; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsInterface { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsValueType" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если тип <see cref="T:System.Type" /> является типом значения; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsValueType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsAbstract" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если класс <see cref="T:System.Type" /> является абстрактным; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsAbstract { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsSealed" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> объявлен как запечатанный; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsSealed { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsEnum" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see cref="T:System.Type" /> представляет перечисление; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsEnum { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsSpecialName" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Type" /> имеет имя, которое требует специальной обработки; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsSpecialName { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsImport" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Type" /> имеет <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsImport { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsSerializable" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является сериализуемым; в противным случае — значение <see langword="false" />.
    /// </returns>
    bool IsSerializable { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsAnsiClass" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если для данного объекта <see langword="AnsiClass" /> выбран атрибут формата строки <see cref="T:System.Type" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsAnsiClass { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsUnicodeClass" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если для данного объекта <see langword="UnicodeClass" /> выбран атрибут формата строки <see cref="T:System.Type" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsUnicodeClass { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsAutoClass" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если для данного объекта <see langword="AutoClass" /> выбран атрибут формата строки <see cref="T:System.Type" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsAutoClass { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsArray" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Type" /> является массивом; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsArray { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsByRef" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> передан по ссылке; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsByRef { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsPointer" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Type" /> является указателем; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsPointer { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsPrimitive" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является одним из типов-примитивов; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsPrimitive { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsCOMObject" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Type" /> является COM-объектом, в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsCOMObject { get; }

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к <see cref="P:System.Type.HasElementType" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является массивом, указателем или параметром, переданным по ссылке; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool HasElementType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsContextful" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> может быть помещен в контекст; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsContextful { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Type.IsMarshalByRef" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> маршалируется по ссылке; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsMarshalByRef { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.Equals(System.Type)" /> метод.
    /// </summary>
    /// <param name="o">
    ///   <see cref="T:System.Type" /> —, Базовый системный тип которого сравнивается с базовым системным типом текущего объекта <see cref="T:System.Type" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если базовый системный тип параметра <paramref name="o" /> совпадает с базовым системным типом текущего объекта <see cref="T:System.Type" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool Equals(Type o);
  }
}
