﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._EventInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет открытые члены <see cref="T:System.Reflection.EventInfo" /> класс в неуправляемый код.
  /// </summary>
  [Guid("9DE59C64-D889-35A1-B897-587D74469E5B")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [CLSCompliant(false)]
  [TypeLibImportClass(typeof (EventInfo))]
  [ComVisible(true)]
  public interface _EventInfo
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
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.EventInfo.MemberType" /> свойство.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Reflection.MemberTypes" /> значение, указывающее, что этот член является событием.
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
    ///   <see cref="T:System.Type" /> Объект, который использовался для получения этого объекта.
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
    ///   <see langword="true" /> для поиска цепочки наследования элемента для поиска атрибутов; в противном случае — значение false.
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
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.EventInfo.GetAddMethod(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="nonPublic">
    ///   <see langword="true" /> для возврата закрытые методы; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> объект, представляющий метод, используемый для добавления делегата обработчика событий для источника событий.
    /// </returns>
    MethodInfo GetAddMethod(bool nonPublic);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.EventInfo.GetRemoveMethod(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="nonPublic">
    ///   <see langword="true" /> для возврата закрытые методы; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> объект, представляющий метод, используемый для удаления делегата обработчика событий из источника событий.
    /// </returns>
    MethodInfo GetRemoveMethod(bool nonPublic);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.EventInfo.GetRaiseMethod(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="nonPublic">
    ///   <see langword="true" /> для возврата закрытые методы; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Reflection.MethodInfo" /> Вызываемый при возникновении события.
    /// </returns>
    MethodInfo GetRaiseMethod(bool nonPublic);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.EventInfo.Attributes" /> свойство.
    /// </summary>
    /// <returns>Атрибуты только для чтения для данного события.</returns>
    EventAttributes Attributes { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.EventInfo.GetAddMethod" /> метод.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> объект, представляющий метод, используемый для добавления делегата обработчика событий для источника событий.
    /// </returns>
    MethodInfo GetAddMethod();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.EventInfo.GetRemoveMethod" /> метод.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> объект, представляющий метод, используемый для удаления делегата обработчика событий из источника событий.
    /// </returns>
    MethodInfo GetRemoveMethod();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.EventInfo.GetRaiseMethod" /> метод.
    /// </summary>
    /// <returns>Метод, вызываемый при возникновении события.</returns>
    MethodInfo GetRaiseMethod();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> метод.
    /// </summary>
    /// <param name="target">Источник события.</param>
    /// <param name="handler">
    ///   Метод или методы, вызываемый при возникновении события целевым объектом.
    /// </param>
    void AddEventHandler(object target, Delegate handler);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.EventInfo.RemoveEventHandler(System.Object,System.Delegate)" /> метод.
    /// </summary>
    /// <param name="target">Источник события.</param>
    /// <param name="handler">
    ///   Делегат, который отделяется от события, вызываемые целевой.
    /// </param>
    void RemoveEventHandler(object target, Delegate handler);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.EventInfo.EventHandlerType" /> свойство.
    /// </summary>
    /// <returns>
    ///   Только для чтения <see cref="T:System.Type" /> объект, представляющий обработчик событий делегата.
    /// </returns>
    Type EventHandlerType { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.EventInfo.IsSpecialName" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если у события есть специальное имя; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsSpecialName { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.EventInfo.IsMulticast" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если делегат является экземпляром многоадресного делегата; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsMulticast { get; }
  }
}
