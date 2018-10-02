// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._FieldInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет открытые члены <see cref="T:System.Reflection.FieldInfo" /> класс в неуправляемый код.
  /// </summary>
  [Guid("8A7C1442-A9FB-366B-80D8-4939FFA6DBE0")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [CLSCompliant(false)]
  [TypeLibImportClass(typeof (FieldInfo))]
  [ComVisible(true)]
  public interface _FieldInfo
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
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.MemberType" /> свойство.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Reflection.MemberTypes" /> значение, указывающее, что этот член является полем.
    /// </returns>
    MemberTypes MemberType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MemberInfo.Name" /> свойство.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.String" /> содержащий имя данного элемента.
    /// </returns>
    string Name { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MemberInfo.DeclaringType" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Объект для класса, который объявляет этот член.
    /// </returns>
    Type DeclaringType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.MemberInfo.ReflectedType" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Объекта, через который был получен данный объект.
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
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск атрибутов в цепочке наследования этого члена.
    /// </param>
    /// <returns>
    ///   Массив, содержащий настраиваемые атрибуты, или массив с нулем элементов, если атрибуты не определены.
    /// </returns>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> метод.
    /// </summary>
    /// <param name="attributeType">
    ///   <see cref="T:System.Type" /> Объекта, к которому применяются пользовательские атрибуты.
    /// </param>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск атрибутов в цепочке наследования этого члена.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если один или несколько экземпляров <paramref name="attributeType" /> применяется к данному члену; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsDefined(Type attributeType, bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.FieldType" /> свойство.
    /// </summary>
    /// <returns>Тип этого объекта поля.</returns>
    Type FieldType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.FieldInfo.GetValue(System.Object)" /> метод.
    /// </summary>
    /// <param name="obj">
    ///   Объект, значение поля которого будет возвращено.
    /// </param>
    /// <returns>
    ///   Объект, содержащий значение поля, отраженное этим экземпляром.
    /// </returns>
    object GetValue(object obj);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.FieldInfo.GetValueDirect(System.TypedReference)" /> метод.
    /// </summary>
    /// <param name="obj">
    ///   A <see cref="T:System.TypedReference" /> Структура, инкапсулирующая управляемый указатель на местоположение и представление времени выполнения типа, который может храниться в этом месте.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Object" /> Содержащий значение поля.
    /// </returns>
    object GetValueDirect(TypedReference obj);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.PropertyInfo.SetValue(System.Object,System.Object,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> метод.
    /// </summary>
    /// <param name="obj">
    ///   Объект, значение поля которого будет установлено.
    /// </param>
    /// <param name="value">Значение, присваиваемое полю.</param>
    /// <param name="invokeAttr">
    ///   Поле <see cref="T:System.Reflection.Binder" /> указывающий тип связывания (например, <see langword="Binder.CreateInstance" /> или <see langword="Binder.ExactBinding" />).
    /// </param>
    /// <param name="binder">
    ///   Набор свойств, который допускает привязку, приведение типов аргументов и вызов членов с помощью отражения.
    ///    Если <paramref name="binder" /> является <see langword="null" />, затем <see langword="Binder.DefaultBinding" /> используется.
    /// </param>
    /// <param name="culture">
    ///   Программные настройки конкретного языка и региональных параметров.
    /// </param>
    void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.FieldInfo.SetValueDirect(System.TypedReference,System.Object)" /> метод.
    /// </summary>
    /// <param name="obj">
    ///   Объект, значение поля которого будет установлено.
    /// </param>
    /// <param name="value">Значение, присваиваемое полю.</param>
    void SetValueDirect(TypedReference obj, object value);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.FieldHandle" /> свойство.
    /// </summary>
    /// <returns>
    ///   Дескриптор представления внутренних метаданных поля.
    /// </returns>
    RuntimeFieldHandle FieldHandle { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.Attributes" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.FieldAttributes" /> Для этого поля.
    /// </returns>
    FieldAttributes Attributes { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.FieldInfo.SetValue(System.Object,System.Object)" /> метод.
    /// </summary>
    /// <param name="obj">
    ///   Объект, значение поля которого будет установлено.
    /// </param>
    /// <param name="value">Значение, присваиваемое полю.</param>
    void SetValue(object obj, object value);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.IsPublic" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если это поле является открытым; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsPublic { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.IsPrivate" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле является закрытым; в противном случае; <see langword="false" />.
    /// </returns>
    bool IsPrivate { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.IsFamily" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле содержит <see langword="Family" /> атрибута задано; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsFamily { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.IsAssembly" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле содержит <see langword="Assembly" /> атрибута задано; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsAssembly { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.IsFamilyAndAssembly" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле содержит <see langword="FamANDAssem" /> атрибута задано; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsFamilyAndAssembly { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.IsFamilyOrAssembly" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле содержит <see langword="FamORAssem" /> атрибута задано; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsFamilyOrAssembly { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.IsStatic" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если это поле является статическим. в противном случае — <see langword="false" />.
    /// </returns>
    bool IsStatic { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.IsInitOnly" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле содержит <see langword="InitOnly" /> атрибута задано; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsInitOnly { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.IsLiteral" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле содержит <see langword="Literal" /> атрибута задано; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsLiteral { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.IsNotSerialized" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле содержит <see langword="NotSerialized" /> атрибута задано; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsNotSerialized { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.IsSpecialName" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see langword="SpecialName" /> установлен в <see cref="T:System.Reflection.FieldAttributes" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsSpecialName { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.FieldInfo.IsPinvokeImpl" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see langword="PinvokeImpl" /> установлен в <see cref="T:System.Reflection.FieldAttributes" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsPinvokeImpl { get; }
  }
}
