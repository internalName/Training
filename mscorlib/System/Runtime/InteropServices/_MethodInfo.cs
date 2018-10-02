// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._MethodInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет открытые члены <see cref="T:System.Reflection.MethodInfo" /> класс в неуправляемый код.
  /// </summary>
  [Guid("FFCC1B5D-ECB8-38DD-9B01-3DC8ABC2AA5F")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [CLSCompliant(false)]
  [TypeLibImportClass(typeof (MethodInfo))]
  [ComVisible(true)]
  public interface _MethodInfo
  {
    /// <summary>
    ///   Возвращает количество предоставляемых объектом интерфейсов для доступа к сведениям о типе (0 или 1).
    /// </summary>
    /// <param name="pcTInfo">
    ///   Когда выполнение этого метода завершается, содержит указатель, по которому записано число предоставляемых объектом интерфейсов, предназначенных для получения сведений о типе.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    void GetTypeInfoCount(out uint pcTInfo);

    /// <summary>
    ///   Возвращает сведения о типе объекта, которые можно использовать для получения сведений о типе интерфейса.
    /// </summary>
    /// <param name="iTInfo">Возвращаемые сведения о типе.</param>
    /// <param name="lcid">
    ///   Идентификатор языкового стандарта для сведений о типе.
    /// </param>
    /// <param name="ppTInfo">
    ///   Указатель на объект с запрошенными сведениями о типе.
    /// </param>
    void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

    /// <summary>
    ///   Сопоставляет набор имен соответствующему набору идентификаторов диспетчеризации.
    /// </summary>
    /// <param name="riid">
    ///   Зарезервировано для будущего использования.
    ///    Должно быть равным IID_NULL.
    /// </param>
    /// <param name="rgszNames">Массив сопоставляемых имен.</param>
    /// <param name="cNames">Количество сопоставляемых имен.</param>
    /// <param name="lcid">
    ///   Контекст языкового стандарта для интерпретации имен.
    /// </param>
    /// <param name="rgDispId">
    ///   Массив, зарезервированный вызывающим объектом, который получает идентификаторы, соответствующие именам.
    /// </param>
    void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

    /// <summary>
    ///   Предоставляет доступ к открытым свойствам и методам объекта.
    /// </summary>
    /// <param name="dispIdMember">Идентификатор элемента.</param>
    /// <param name="riid">
    ///   Зарезервировано для будущего использования.
    ///    Должно быть равным IID_NULL.
    /// </param>
    /// <param name="lcid">
    ///   Контекст языкового стандарта, в котором следует интерпретировать аргументы.
    /// </param>
    /// <param name="wFlags">Флаги, описывающие контекст вызова.</param>
    /// <param name="pDispParams">
    ///   Указатель на структуру, содержащую массив аргументов, массив аргументов DISPID для именованных аргументов, а также счетчики количества элементов в массивах.
    /// </param>
    /// <param name="pVarResult">
    ///   Указатель на место хранения результата.
    /// </param>
    /// <param name="pExcepInfo">
    ///   Указатель на структуру, содержащую сведения об исключении.
    /// </param>
    /// <param name="puArgErr">
    ///   Индекс первого аргумента, вызвавшего ошибку.
    /// </param>
    void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Object.ToString" /> метод.
    /// </summary>
    /// <returns>
    ///   Строка, представляющая текущий объект <see cref="T:System.Object" />.
    /// </returns>
    string ToString();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Object.Equals(System.Object)" /> метод.
    /// </summary>
    /// <param name="other">
    ///   Объект <see cref="T:System.Object" />, с которым сравнивается текущий объект <see cref="T:System.Object" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект <see cref="T:System.Object" /> равен текущему объекту <see cref="T:System.Object" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool Equals(object other);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Object.GetHashCode" /> метод.
    /// </summary>
    /// <returns>Хэш-код для текущего экземпляра.</returns>
    int GetHashCode();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Type.GetType" /> метод.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />.
    /// </returns>
    Type GetType();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MemberInfo.MemberType" /> свойство.
    /// </summary>
    /// <returns>
    ///   Один из <see cref="T:System.Reflection.MemberTypes" /> значения, указывающие тип члена.
    /// </returns>
    MemberTypes MemberType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MemberInfo.Name" /> свойство.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> объект, содержащий имя данного элемента.
    /// </returns>
    string Name { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MemberInfo.DeclaringType" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="Type" /> Объект для класса, который объявляет этот член.
    /// </returns>
    Type DeclaringType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MemberInfo.ReflectedType" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="Type" /> Объект, который был использован для получения данного <see langword="MemberInfo" /> объекта.
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
    ///   <see langword="true" /> для поиска цепочки наследования этого элемента для поиска атрибутов; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Массив настраиваемых атрибутов, примененных к данному члену, или массив нулей (0), если атрибуты не применялись.
    /// </returns>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="inherit">
    ///   <see langword="true" /> для поиска цепочки наследования этого элемента для поиска атрибутов; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Массив, содержащий настраиваемые атрибуты, или массив нулей (0), если атрибуты не определены.
    /// </returns>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> метод.
    /// </summary>
    /// <param name="attributeType">
    ///   Объект <see langword="Type" />, к которому применяются настраиваемые атрибуты.
    /// </param>
    /// <param name="inherit">
    ///   <see langword="true" /> для поиска цепочки наследования этого элемента для поиска атрибутов; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если один или несколько экземпляров <paramref name="attributeType" /> параметр применен к этому элементу, в противном случае — <see langword="false" />.
    /// </returns>
    bool IsDefined(Type attributeType, bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MethodBase.GetParameters" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Reflection.ParameterInfo" /> данных, которые соответствуют подписи метода (или конструктора) отражаемый данным экземпляром.
    /// </returns>
    ParameterInfo[] GetParameters();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MethodBase.GetMethodImplementationFlags" /> метод.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Reflection.MethodImplAttributes" />.
    /// </returns>
    MethodImplAttributes GetMethodImplementationFlags();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.MethodHandle" /> свойство.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.RuntimeMethodHandle" />.
    /// </returns>
    RuntimeMethodHandle MethodHandle { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.Attributes" /> свойство.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Reflection.MethodAttributes" />.
    /// </returns>
    MethodAttributes Attributes { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.CallingConvention" /> свойство.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Reflection.CallingConventions" />.
    /// </returns>
    CallingConventions CallingConvention { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MethodBase.Invoke(System.Object,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> метод.
    /// </summary>
    /// <param name="obj">Экземпляр, который создал этот метод.</param>
    /// <param name="invokeAttr">
    ///   Один из <see langword="BindingFlags" /> значений, определяющее тип привязки.
    /// </param>
    /// <param name="binder">
    ///   Параметр <see langword="Binder" />, который определяет набор свойств и обеспечивает привязку, приведение типов аргументов и вызов членов с помощью отражения.
    ///    Если <paramref name="binder" /> является <see langword="null" />, затем <see langword="Binder.DefaultBinding" /> используется.
    /// </param>
    /// <param name="parameters">
    ///   Массив объектов типа <see langword="Object" /> соответствует число, порядок и тип параметров для данного конструктора, с учетом ограничений <paramref name="binder" />.
    ///    Если этот конструктор не требует параметров, передается массив с нулевыми элементами, как и в Object[] parameters = new Object[0].
    ///    Любой объект этого массива, явно не инициализирован со значением будет содержать значение по умолчанию для данного типа объекта.
    ///    Для элементов ссылочного типа это значение равно <see langword="null" />.
    ///    Для элементов типа значения, это значение равно 0, 0,0 или <see langword="false" />, в зависимости от заданного типа элемента.
    /// </param>
    /// <param name="culture">
    ///   Объект <see cref="T:System.Globalization.CultureInfo" /> объект, используемый для управления приведением типов.
    ///    Если значение этого объекта — <see langword="null" />, для текущего потока используется <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <returns>Экземпляр класса, связанный с конструктором.</returns>
    object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsPublic" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод является открытым; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsPublic { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsPrivate" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если доступ к этому методу разрешен только элементам данного класса; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsPrivate { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsFamily" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если класс ограничен доступ для членов данного класса и членов производных классов; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsFamily { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsAssembly" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод может вызываться другими классами в той же сборке; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsAssembly { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsFamilyAndAssembly" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод ограничен доступ для членов данного класса и членов производных классов, которые находятся в одной и той же сборки; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsFamilyAndAssembly { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsFamilyOrAssembly" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если доступ к этому методу только члены самого класса, членов производных классов везде, где они находятся и членов класса в той же сборке; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsFamilyOrAssembly { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsStatic" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод является <see langword="static" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsStatic { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsFinal" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод является <see langword="final" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsFinal { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsVirtual" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод является <see langword="virtual" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsVirtual { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsHideBySig" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если элемент скрыт на основе подписи; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsHideBySig { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsAbstract" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если метод является абстрактным. в противном случае — <see langword="false" />.
    /// </returns>
    bool IsAbstract { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsSpecialName" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод имеет специальное имя; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsSpecialName { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodBase.IsConstructor" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод является конструктором; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsConstructor { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MethodBase.Invoke(System.Object,System.Object[])" /> метод.
    /// </summary>
    /// <param name="obj">Экземпляр, который создал этот метод.</param>
    /// <param name="parameters">
    ///   Список аргументов для вызываемого метода или конструктора.
    ///    Это массив объектов, в число, порядок и тип как параметры метода или конструктора.
    ///    Если нет параметров, <paramref name="parameters" /> должно быть <see langword="null" />.
    /// 
    ///   Если метод или конструктор, представленный экземпляром принимает <see langword="ref" /> параметр (<see langword="ByRef" /> в Visual Basic), нет специальные атрибуты не требуются для этого параметра для вызова метода или конструктора с использованием этой функции.
    ///    Любой объект этого массива, явно не инициализирован со значением будет содержать значение по умолчанию для данного типа объекта.
    ///    Для элементов ссылочного типа это значение равно <see langword="null" />.
    ///    Для элементов типа значения, это значение равно 0, 0,0 или <see langword="false" />, в зависимости от заданного типа элемента.
    /// </param>
    /// <returns>Экземпляр класса, связанный с конструктором.</returns>
    object Invoke(object obj, object[] parameters);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodInfo.ReturnType" /> свойство.
    /// </summary>
    /// <returns>Тип возвращаемого значения этого метода.</returns>
    Type ReturnType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MethodInfo.ReturnTypeCustomAttributes" /> свойство.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.ICustomAttributeProvider" />, представляющий пользовательские атрибуты для возвращаемого типа.
    /// </returns>
    ICustomAttributeProvider ReturnTypeCustomAttributes { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MethodInfo.GetBaseDefinition" /> метод.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> для первой реализации этого метода.
    /// </returns>
    MethodInfo GetBaseDefinition();
  }
}
