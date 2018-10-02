// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._PropertyInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет открытые члены <see cref="T:System.Reflection.PropertyInfo" /> класс в неуправляемый код.
  /// </summary>
  [Guid("F59ED4E4-E68F-3218-BD77-061AA82824BF")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [CLSCompliant(false)]
  [TypeLibImportClass(typeof (PropertyInfo))]
  [ComVisible(true)]
  public interface _PropertyInfo
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
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Object.GetType" /> метод.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />.
    /// </returns>
    Type GetType();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.PropertyInfo.MemberType" /> свойство.
    /// </summary>
    /// <returns>
    ///   Один из <see cref="T:System.Reflection.MemberTypes" /> значения, указывающие, что этот член является свойством.
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
    ///   значение true для поиска цепочки наследования этого члена для поиска атрибутов; в противном случае — значение false.
    /// </param>
    /// <returns>
    ///   Массив настраиваемых атрибутов, примененных к данному члену, или массив нулей (0), если атрибуты не применялись.
    /// </returns>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="inherit">
    ///   значение true для поиска цепочки наследования этого члена для поиска атрибутов; в противном случае — значение false.
    /// </param>
    /// <returns>
    ///   Массив, содержащий настраиваемые атрибуты, или массив с нулем элементов, если атрибуты не определены.
    /// </returns>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> метод.
    /// </summary>
    /// <param name="attributeType">
    ///   Объект <see cref="T:System.Type" />, к которому применяются настраиваемые атрибуты.
    /// </param>
    /// <param name="inherit">
    ///   значение true для поиска цепочки наследования этого члена для поиска атрибутов; в противном случае — значение false.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если один или несколько экземпляров <paramref name="attributeType" /> параметров, примененных к данному члену, в противном случае — <see langword="false" />.
    /// </returns>
    bool IsDefined(Type attributeType, bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> свойство.
    /// </summary>
    /// <returns>Тип этого свойства.</returns>
    Type PropertyType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.PropertyInfo.GetValue(System.Object,System.Object[])" /> метод.
    /// </summary>
    /// <param name="obj">
    ///   Объект, свойство которого будет возвращено.
    /// </param>
    /// <param name="index">
    ///   Необязательные значения индекса для индексированных свойств.
    ///    Для неиндексированных свойств это значение должно быть равно <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение свойства <paramref name="obj" /> параметра.
    /// </returns>
    object GetValue(object obj, object[] index);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.PropertyInfo.GetValue(System.Object,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> метод.
    /// </summary>
    /// <param name="obj">
    ///   Объект, свойство которого будет возвращено.
    /// </param>
    /// <param name="invokeAttr">
    ///   Атрибут вызова.
    ///    Это должен быть одноразрядный флаг из <see langword="BindingFlags" />: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, или <see langword="SetProperty" />.
    ///    Необходимо указать подходящий атрибут вызова.
    ///    Если будет вызываться статический член, <see langword="Static" /> флаг <see langword="BindingFlags" /> необходимо задать.
    /// </param>
    /// <param name="binder">
    ///   Объект, который допускает привязку, приведение типов аргументов, вызов элементов и извлечение объектов <see langword="MemberInfo" /> путем отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="index">
    ///   Необязательные значения индекса для индексированных свойств.
    ///    Для неиндексированных свойств это значение должно быть равно <see langword="null" />.
    /// </param>
    /// <param name="culture">
    ///   <see langword="CultureInfo" /> Объект, представляющий язык и региональные параметры, для которого будет локализован ресурс.
    ///    Обратите внимание, что если ресурс не локализован для данной культуры, <see langword="CultureInfo.Parent" /> будет последовательно вызываться метод поиска соответствия.
    ///    Если это значение равно <see langword="null" />,  <see langword="CultureInfo" /> получается из <see langword="CultureInfo.CurrentUICulture" /> свойство.
    /// </param>
    /// <returns>
    ///   Значение свойства <paramref name="obj" /> параметра.
    /// </returns>
    object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.PropertyInfo.SetValue(System.Object,System.Object,System.Object[])" /> метод.
    /// </summary>
    /// <param name="obj">
    ///   Объект, значение свойства которого будет установлено.
    /// </param>
    /// <param name="value">Новое значение для этого свойства.</param>
    /// <param name="index">
    ///   Необязательные значения индекса для индексированных свойств.
    ///    Для неиндексированных свойств это значение должно быть равно <see langword="null" />.
    /// </param>
    void SetValue(object obj, object value, object[] index);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.FieldInfo.SetValue(System.Object,System.Object,System.Reflection.BindingFlags,System.Reflection.Binder,System.Globalization.CultureInfo)" /> метод.
    /// </summary>
    /// <param name="obj">
    ///   Объект, свойство которого будет возвращено.
    /// </param>
    /// <param name="value">Новое значение для этого свойства.</param>
    /// <param name="invokeAttr">
    ///   Атрибут вызова.
    ///    Это должен быть одноразрядный флаг из <see cref="T:System.Reflection.BindingFlags" />: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, или <see langword="SetProperty" />.
    ///    Необходимо указать подходящий атрибут вызова.
    ///    Если будет вызываться статический член, <see langword="Static" /> флаг <see langword="BindingFlags" /> необходимо задать.
    /// </param>
    /// <param name="binder">
    ///   Объект, который допускает привязку, приведение типов аргументов, вызов элементов и извлечение объектов <see cref="T:System.Reflection.MemberInfo" /> путем отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="index">
    ///   Необязательные значения индекса для индексированных свойств.
    ///    Для неиндексированных свойств это значение должно быть равно <see langword="null" />.
    /// </param>
    /// <param name="culture">
    ///   <see cref="T:System.Globalization.CultureInfo" /> Объект, представляющий язык и региональные параметры, для которого будет локализован ресурс.
    ///    Обратите внимание, что если ресурс не локализован для данной культуры, <see langword="CultureInfo.Parent" /> будет последовательно вызываться метод поиска соответствия.
    ///    Если это значение равно <see langword="null" />,  <see langword="CultureInfo" /> получается из <see langword="CultureInfo.CurrentUICulture" /> свойство.
    /// </param>
    void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.PropertyInfo.GetAccessors(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="nonPublic">
    ///   <see langword="true" /> Чтобы включить неоткрытые методы в возвращаемый <see langword="MethodInfo" /> массива; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MethodInfo" />, элементы которого отражают методы <see langword="get" />, <see langword="set" /> и другие методы доступа к свойству, отражаемому текущим экземпляром.
    ///    Если <paramref name="nonPublic" /> параметр <see langword="true" />, этот массив содержит открытые и закрытые <see langword="get" />, <see langword="set" />, и другие методы доступа.
    ///    Если свойство <paramref name="nonPublic" /> равно <see langword="false" />, этот массив содержит только открытые методы <see langword="get" />, <see langword="set" /> и другие методы доступа.
    ///    Если методы доступа с указанным статусом видимости не найдены, этот метод возвращает массив с нулевым (0) числом элементов.
    /// </returns>
    MethodInfo[] GetAccessors(bool nonPublic);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.PropertyInfo.GetGetMethod(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="nonPublic">
    ///   <see langword="true" /> для возврата, не являющиеся открытыми <see langword="get" /> доступа; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> объект, представляющий <see langword="get" /> для этого свойства, если <paramref name="nonPublic" /> параметр <see langword="true" />.
    ///    Или <see langword="null" /> при <paramref name="nonPublic" /> — <see langword="false" /> и <see langword="get" /> доступа не является открытым, или если <paramref name="nonPublic" /> является <see langword="true" /> но не <see langword="get" /> существуют методы доступа.
    /// </returns>
    MethodInfo GetGetMethod(bool nonPublic);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.PropertyInfo.GetSetMethod(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="nonPublic">
    ///   <see langword="true" /> Для получения доступа к закрытым; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    /// Одно из значений, перечисленных в следующей таблице.
    /// 
    ///         Значение
    /// 
    ///         Значение
    /// 
    ///         Объект <see cref="T:System.Reflection.MethodInfo" />, предоставляющий метод <see langword="Set" /> для этого свойства.
    /// 
    ///         Метод доступа <see langword="set" /> является открытым.
    /// 
    ///         -или-
    /// 
    ///         <paramref name="nonPublic" /> Параметр <see langword="true" /> и <see langword="set" /> доступа не является открытым.
    /// 
    ///         <see langword="null" />
    /// 
    ///         <paramref name="nonPublic" /> Параметр <see langword="true" />, но свойство доступно только для чтения.
    /// 
    ///         -или-
    /// 
    ///         <paramref name="nonPublic" /> Параметр <see langword="false" /> и <see langword="set" /> доступа не является открытым.
    /// 
    ///         -или-
    /// 
    ///         Метод доступа <see langword="set" /> не существует.
    ///       </returns>
    MethodInfo GetSetMethod(bool nonPublic);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.PropertyInfo.GetIndexParameters" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив элементов типа <see cref="T:System.Reflection.ParameterInfo" />, содержащий параметры для индексов.
    /// </returns>
    ParameterInfo[] GetIndexParameters();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.PropertyInfo.Attributes" /> свойство.
    /// </summary>
    /// <returns>Атрибуты данного свойства.</returns>
    PropertyAttributes Attributes { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.PropertyInfo.CanRead" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если свойство доступно для чтения; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool CanRead { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.PropertyInfo.CanWrite" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если свойство доступно для записи; в обратном случае — значение <see langword="false" />.
    /// </returns>
    bool CanWrite { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.PropertyInfo.GetAccessors" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив <see cref="T:System.Reflection.MethodInfo" /> объекты, которые отражают открытые <see langword="get" />, <see langword="set" />, и другие методы доступа к свойству отражаемому текущим экземпляром, если эти методы существуют; в противном случае этот метод возвращает массив с нулевым (0) числом элементов.
    /// </returns>
    MethodInfo[] GetAccessors();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.PropertyInfo.GetGetMethod" /> метод.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, предоставляющий открытый метод доступа <see langword="get" /> для этого свойства, или значение <see langword="null" />, если метод доступа <see langword="get" /> не является открытым либо не существует.
    /// </returns>
    MethodInfo GetGetMethod();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.PropertyInfo.GetSetMethod" /> метод.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, представляющий метод <see langword="Set" /> для этого свойства, если метод доступа <see langword="set" /> является открытым, или значение <see langword="null" />, если метод <see langword="set" /> не является открытым.
    /// </returns>
    MethodInfo GetSetMethod();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.PropertyInfo.IsSpecialName" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если свойство является особым именем; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool IsSpecialName { get; }
  }
}
