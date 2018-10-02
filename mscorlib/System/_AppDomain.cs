// Decompiled with JetBrains decompiler
// Type: System._AppDomain
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;
using System.Security.Principal;

namespace System
{
  /// <summary>
  ///   Предоставляет открытые элементы <see cref="T:System.AppDomain" /> класса в неуправляемый код.
  /// </summary>
  [Guid("05F696DC-2B29-3663-AD8B-C4389CF2A713")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [CLSCompliant(false)]
  [ComVisible(true)]
  public interface _AppDomain
  {
    /// <summary>
    ///   Возвращает количество предоставляемых объектом интерфейсов для доступа к сведениям о типе (0 или 1).
    /// </summary>
    /// <param name="pcTInfo">
    ///   Указатель, по которому записывается число предоставляемых объектом интерфейсов, предназначенных для получения сведений о типе.
    /// </param>
    /// <exception cref="T:System.NotImplementedException">
    ///   Этот метод вызывается с поздним связыванием с помощью COM-интерфейса IDispatch.
    /// </exception>
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
    /// <exception cref="T:System.NotImplementedException">
    ///   Этот метод вызывается с поздним связыванием с помощью COM-интерфейса IDispatch.
    /// </exception>
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
    /// <exception cref="T:System.NotImplementedException">
    ///   Этот метод вызывается с поздним связыванием с помощью COM-интерфейса IDispatch.
    /// </exception>
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
    /// <exception cref="T:System.NotImplementedException">
    ///   Этот метод вызывается с поздним связыванием с помощью COM-интерфейса IDispatch.
    /// </exception>
    void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.ToString" /> метод.
    /// </summary>
    /// <returns>
    ///   Строка, полученная путем сцепления литеральной строки "Name:", понятного имени домена приложения и либо строкового представления политик контекста, либо строки "Политики контекста отсутствуют".
    /// </returns>
    string ToString();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии наследуемые <see cref="M:System.Object.Equals(System.Object)" /> метод.
    /// </summary>
    /// <param name="other">
    ///   <see cref="T:System.Object" /> Для сравнения с текущим <see cref="T:System.Object" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект <see cref="T:System.Object" /> равен текущему объекту <see cref="T:System.Object" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool Equals(object other);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии наследуемые <see cref="M:System.Object.GetHashCode" /> метод.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Object" />.
    /// </returns>
    int GetHashCode();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.GetType" /> метод.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> представляет тип текущего экземпляра.
    /// </returns>
    Type GetType();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.InitializeLifetimeService" /> метод.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="null" />.
    /// </returns>
    [SecurityCritical]
    object InitializeLifetimeService();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии наследуемые <see cref="M:System.MarshalByRefObject.GetLifetimeService" /> метод.
    /// </summary>
    /// <returns>
    ///   Объект типа <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> используется для управления политикой времени существования данного экземпляра.
    /// </returns>
    [SecurityCritical]
    object GetLifetimeService();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.AppDomain.Evidence" /> свойство.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Security.Policy.Evidence" /> связанных с этим доменом приложения, который используется в качестве входных данных для политики безопасности.
    /// </returns>
    Evidence Evidence { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="E:System.AppDomain.DomainUnload" /> события.
    /// </summary>
    event EventHandler DomainUnload;

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к <see cref="E:System.AppDomain.AssemblyLoad" /> события.
    /// </summary>
    event AssemblyLoadEventHandler AssemblyLoad;

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="E:System.AppDomain.ProcessExit" /> события.
    /// </summary>
    event EventHandler ProcessExit;

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="E:System.AppDomain.TypeResolve" /> события.
    /// </summary>
    event ResolveEventHandler TypeResolve;

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="E:System.AppDomain.ResourceResolve" /> события.
    /// </summary>
    event ResolveEventHandler ResourceResolve;

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к <see cref="E:System.AppDomain.AssemblyResolve" /> события.
    /// </summary>
    event ResolveEventHandler AssemblyResolve;

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к <see cref="E:System.AppDomain.UnhandledException" /> события.
    /// </summary>
    event UnhandledExceptionEventHandler UnhandledException;

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess)" /> перегрузка метода.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим доступа для динамической сборки.
    /// </param>
    /// <returns>Представляет созданную динамическую сборку.</returns>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String)" /> перегрузка метода.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="dir">
    ///   Имя папки, в которой будет сохранена сборка.
    ///    Если параметр <paramref name="dir" /> имеет значение <see langword="null" />, по умолчанию используется текущий каталог.
    /// </param>
    /// <returns>Представляет созданную динамическую сборку.</returns>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.Security.Policy.Evidence)" /> перегрузка метода.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="evidence">
    ///   Свидетельство, предоставляемое для динамической сборки.
    ///    Используемое свидетельство является постоянным, как конечный набор свидетельств, используемых для разрешения политики.
    /// </param>
    /// <returns>Представляет созданную динамическую сборку.</returns>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> перегрузка метода.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="requiredPermissions">
    ///   Запрос обязательных разрешений.
    /// </param>
    /// <param name="optionalPermissions">
    ///   Запрос дополнительных разрешений.
    /// </param>
    /// <param name="refusedPermissions">
    ///   Запрос разрешений, в которых отказано.
    /// </param>
    /// <returns>Представляет созданную динамическую сборку.</returns>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.Policy.Evidence)" /> перегрузка метода.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="dir">
    ///   Имя папки, в которой будет сохранена сборка.
    ///    Если параметр <paramref name="dir" /> имеет значение <see langword="null" />, по умолчанию используется текущий каталог.
    /// </param>
    /// <param name="evidence">
    ///   Свидетельство, предоставляемое для динамической сборки.
    ///    Используемое свидетельство является постоянным, как конечный набор свидетельств, используемых для разрешения политики.
    /// </param>
    /// <returns>Представляет созданную динамическую сборку.</returns>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> перегрузка метода.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="dir">
    ///   Имя папки, в которой будет сохранена сборка.
    ///    Если параметр <paramref name="dir" /> имеет значение <see langword="null" />, по умолчанию используется текущий каталог.
    /// </param>
    /// <param name="requiredPermissions">
    ///   Запрос обязательных разрешений.
    /// </param>
    /// <param name="optionalPermissions">
    ///   Запрос дополнительных разрешений.
    /// </param>
    /// <param name="refusedPermissions">
    ///   Запрос разрешений, в которых отказано.
    /// </param>
    /// <returns>Представляет созданную динамическую сборку.</returns>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.Security.Policy.Evidence,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> перегрузка метода.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="evidence">
    ///   Свидетельство, предоставляемое для динамической сборки.
    ///    Используемое свидетельство является постоянным, как конечный набор свидетельств, используемых для разрешения политики.
    /// </param>
    /// <param name="requiredPermissions">
    ///   Запрос обязательных разрешений.
    /// </param>
    /// <param name="optionalPermissions">
    ///   Запрос дополнительных разрешений.
    /// </param>
    /// <param name="refusedPermissions">
    ///   Запрос разрешений, в которых отказано.
    /// </param>
    /// <returns>Представляет созданную динамическую сборку.</returns>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.Policy.Evidence,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> перегрузка метода.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="dir">
    ///   Имя папки, в которой будет сохранена сборка.
    ///    Если параметр <paramref name="dir" /> имеет значение <see langword="null" />, по умолчанию используется текущий каталог.
    /// </param>
    /// <param name="evidence">
    ///   Свидетельство, предоставляемое для динамической сборки.
    ///    Используемое свидетельство является постоянным, как конечный набор свидетельств, используемых для разрешения политики.
    /// </param>
    /// <param name="requiredPermissions">
    ///   Запрос обязательных разрешений.
    /// </param>
    /// <param name="optionalPermissions">
    ///   Запрос дополнительных разрешений.
    /// </param>
    /// <param name="refusedPermissions">
    ///   Запрос разрешений, в которых отказано.
    /// </param>
    /// <returns>Представляет созданную динамическую сборку.</returns>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.Policy.Evidence,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet,System.Boolean)" /> перегрузка метода.
    /// </summary>
    /// <param name="name">
    ///   Уникальный идентификатор динамической сборки.
    /// </param>
    /// <param name="access">
    ///   Режим, в котором будет осуществляться доступ к динамической сборке.
    /// </param>
    /// <param name="dir">
    ///   Имя каталога, в котором будет сохранена динамическая сборка.
    ///    Если параметр <paramref name="dir" /> имеет значение <see langword="null" />, по умолчанию используется текущий каталог.
    /// </param>
    /// <param name="evidence">
    ///   Свидетельство, предоставляемое для динамической сборки.
    ///    Используемое свидетельство является постоянным, как конечный набор свидетельств, используемых для разрешения политики.
    /// </param>
    /// <param name="requiredPermissions">
    ///   Запрос обязательных разрешений.
    /// </param>
    /// <param name="optionalPermissions">
    ///   Запрос дополнительных разрешений.
    /// </param>
    /// <param name="refusedPermissions">
    ///   Запрос разрешений, в которых отказано.
    /// </param>
    /// <param name="isSynchronized">
    ///   Значение <see langword="true" />, чтобы синхронизировать создание модулей, типов и членов в динамической сборке, в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>Представляет созданную динамическую сборку.</returns>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized);

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к <see cref="M:System.AppDomain.CreateInstance(System.String,System.String)" /> метод.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, заданного параметром <paramref name="typeName" />.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    ObjectHandle CreateInstance(string assemblyName, string typeName);

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к перегрузке метода <see cref="M:System.AppDomain.CreateInstanceFrom(System.String,System.String)" />.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя (включая путь) файла, который содержит сборку, определяющую запрошенный тип.
    ///    Эта сборка загружается с помощью метода <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, или значение <see langword="null" />, если объект <paramref name="typeName" /> не найден.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName);

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к перегрузке метода <see cref="M:System.AppDomain.CreateInstance(System.String,System.String,System.Object[])" />.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, массив, который содержит единственный объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    ///    Атрибут <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> указывает URL-адрес, требуемый для активации удаленного объекта.
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, заданного параметром <paramref name="typeName" />.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes);

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к перегрузке метода <see cref="M:System.AppDomain.CreateInstanceFrom(System.String,System.String,System.Object[])" />.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя (включая путь) файла, который содержит сборку, определяющую запрошенный тип.
    ///    Эта сборка загружается с помощью метода <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, массив, который содержит единственный объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    ///    Атрибут <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> указывает URL-адрес, требуемый для активации удаленного объекта.
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, или значение <see langword="null" />, если объект <paramref name="typeName" /> не найден.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes);

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к перегрузке метода <see cref="M:System.AppDomain.CreateInstance(System.String,System.String,System.Boolean,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo,System.Object[],System.Security.Policy.Evidence)" />.
    /// </summary>
    /// <param name="assemblyName">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при поиске.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see cref="T:System.Reflection.MemberInfo" /> с помощью отражения.
    ///    Если параметр <paramref name="binder" /> имеет значение null, то используется модуль привязки по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Аргументы для передачи конструктору.
    ///    Массив аргументов должен соответствовать по числу, порядку и типу параметров вызываемому конструктору.
    ///    Если предпочтителен конструктор по умолчанию, то объект <paramref name="args" /> должен быть пустым массивом или значением null.
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, массив, который содержит единственный объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    ///    Атрибут <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> указывает URL-адрес, требуемый для активации удаленного объекта.
    /// </param>
    /// <param name="securityAttributes">
    ///   Сведения, используемые для авторизации создания <paramref name="typeName" />.
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, заданного параметром <paramref name="typeName" />.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к перегрузке метода <see cref="M:System.AppDomain.CreateInstanceFrom(System.String,System.String,System.Boolean,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo,System.Object[],System.Security.Policy.Evidence)" />.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя (включая путь) файла, который содержит сборку, определяющую запрошенный тип.
    ///    Эта сборка загружается с помощью метода <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />.
    /// </param>
    /// <param name="typeName">
    ///   Полное имя запрошенного типа, включая пространство имен, но не сборку (см. описание свойства <see cref="P:System.Type.FullName" />).
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при поиске.
    /// </param>
    /// <param name="bindingAttr">
    ///   Сочетание битовых флагов (от нуля и более), влияющих на поиск конструктора <paramref name="typeName" />.
    ///    Если значение параметра <paramref name="bindingAttr" /> равно нулю, проводится поиск открытых конструкторов с учетом регистра.
    /// </param>
    /// <param name="binder">
    ///   Объект, который допускает привязку, приведение типов аргументов, вызов элементов и извлечение объектов <see cref="T:System.Reflection.MemberInfo" /> путем отражения.
    ///    Если параметр <paramref name="binder" /> имеет значение null, то используется модуль привязки по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Аргументы для передачи конструктору.
    ///    Массив аргументов должен соответствовать по числу, порядку и типу параметров вызываемому конструктору.
    ///    Если предпочтителен конструктор по умолчанию, то объект <paramref name="args" /> должен быть пустым массивом или значением null.
    /// </param>
    /// <param name="culture">
    ///   Сведения о языке и региональных параметрах, которые влияют на приведение <paramref name="args" /> к формальным типам, объявленным для конструктора <paramref name="typeName" />.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив, состоящий из одного или нескольких атрибутов, которые могут участвовать в активации.
    ///    Как правило, массив, который содержит единственный объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    ///    Атрибут <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> указывает URL-адрес, требуемый для активации удаленного объекта.
    /// </param>
    /// <param name="securityAttributes">
    ///   Сведения, используемые для авторизации создания <paramref name="typeName" />.
    /// </param>
    /// <returns>
    ///   Объект, являющийся оболочкой для нового экземпляра, или значение <see langword="null" />, если объект <paramref name="typeName" /> не найден.
    ///    Необходимо распаковать возвращенное значение, чтобы получить доступ к реальному объекту.
    /// </returns>
    ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.Load(System.Reflection.AssemblyName)" /> перегрузка метода.
    /// </summary>
    /// <param name="assemblyRef">
    ///   Объект, который описывает сборку, подлежащую загрузке.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    Assembly Load(AssemblyName assemblyRef);

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к перегрузке метода <see cref="M:System.AppDomain.Load(System.String)" />.
    /// </summary>
    /// <param name="assemblyString">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    Assembly Load(string assemblyString);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.Load(System.Byte[])" /> перегрузка метода.
    /// </summary>
    /// <param name="rawAssembly">
    ///   Массив типа <see langword="byte" />, который является образом в формате COFF, содержащим порожденную сборку.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    Assembly Load(byte[] rawAssembly);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.Load(System.Byte[],System.Byte[])" /> перегрузка метода.
    /// </summary>
    /// <param name="rawAssembly">
    ///   Массив типа <see langword="byte" />, который является образом в формате COFF, содержащим порожденную сборку.
    /// </param>
    /// <param name="rawSymbolStore">
    ///   Массив типа <see langword="byte" />, содержащий необработанные байты, которые представляют символы для сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.Load(System.Byte[],System.Byte[],System.Security.Policy.Evidence)" /> перегрузка метода.
    /// </summary>
    /// <param name="rawAssembly">
    ///   Массив типа <see langword="byte" />, который является образом в формате COFF, содержащим порожденную сборку.
    /// </param>
    /// <param name="rawSymbolStore">
    ///   Массив типа <see langword="byte" />, содержащий необработанные байты, которые представляют символы для сборки.
    /// </param>
    /// <param name="securityEvidence">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.Load(System.Reflection.AssemblyName,System.Security.Policy.Evidence)" /> перегрузка метода.
    /// </summary>
    /// <param name="assemblyRef">
    ///   Объект, который описывает сборку, подлежащую загрузке.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity);

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к перегрузке метода <see cref="M:System.AppDomain.Load(System.String,System.Security.Policy.Evidence)" />.
    /// </summary>
    /// <param name="assemblyString">
    ///   Отображаемое имя сборки.
    ///    См. раздел <see cref="P:System.Reflection.Assembly.FullName" />.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Загруженная сборка.</returns>
    Assembly Load(string assemblyString, Evidence assemblySecurity);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.ExecuteAssembly(System.String,System.Security.Policy.Evidence)" /> перегрузка метода.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, которую необходимо выполнить.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Свидетельство для загрузки сборки.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" /> перегрузка метода.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, которую необходимо выполнить.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    int ExecuteAssembly(string assemblyFile);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.ExecuteAssembly(System.String,System.Security.Policy.Evidence,System.String[])" /> перегрузка метода.
    /// </summary>
    /// <param name="assemblyFile">
    ///   Имя файла, содержащего сборку, которую необходимо выполнить.
    /// </param>
    /// <param name="assemblySecurity">
    ///   Предоставленное свидетельство для сборки.
    /// </param>
    /// <param name="args">
    ///   Аргументы, передаваемые в точку входа сборки.
    /// </param>
    /// <returns>Значение, возвращаемое точкой входа сборки.</returns>
    int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.AppDomain.FriendlyName" /> свойство.
    /// </summary>
    /// <returns>Понятное имя этого домена приложения.</returns>
    string FriendlyName { get; }

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к <see cref="P:System.AppDomain.BaseDirectory" /> свойство.
    /// </summary>
    /// <returns>
    ///   Базовый каталог, в котором распознаватель сборок производит поиск.
    /// </returns>
    string BaseDirectory { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.AppDomain.RelativeSearchPath" /> свойство.
    /// </summary>
    /// <returns>
    ///   Путь, к каталогу, находящемуся в базовом каталоге, где распознаватель сборок будет производить поиск закрытых сборок.
    /// </returns>
    string RelativeSearchPath { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.AppDomain.ShadowCopyFiles" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если домен приложения настроен для теневого копирования файлов; в противном случае — значение <see langword="false" />.
    /// </returns>
    bool ShadowCopyFiles { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.GetAssemblies" /> метод.
    /// </summary>
    /// <returns>Массив сборок в этом домене приложения.</returns>
    Assembly[] GetAssemblies();

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к <see cref="M:System.AppDomain.AppendPrivatePath(System.String)" /> метод.
    /// </summary>
    /// <param name="path">
    ///   Имя каталога, который следует добавить в закрытый путь.
    /// </param>
    [SecurityCritical]
    void AppendPrivatePath(string path);

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к <see cref="M:System.AppDomain.ClearPrivatePath" /> метод.
    /// </summary>
    [SecurityCritical]
    void ClearPrivatePath();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.SetShadowCopyPath(System.String)" /> метод.
    /// </summary>
    /// <param name="s">
    ///   Список имен каталогов, разделенных точкой с запятой.
    /// </param>
    [SecurityCritical]
    void SetShadowCopyPath(string s);

    /// <summary>
    ///   Предоставляет COM-объекты с независящим от версии доступом к <see cref="M:System.AppDomain.ClearShadowCopyPath" /> метод.
    /// </summary>
    [SecurityCritical]
    void ClearShadowCopyPath();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.SetCachePath(System.String)" /> метод.
    /// </summary>
    /// <param name="s">Полный путь к расположению теневых копий.</param>
    [SecurityCritical]
    void SetCachePath(string s);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.SetData(System.String,System.Object)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   Имя пользовательского свойства домена приложения, которое требуется создать или изменить.
    /// </param>
    /// <param name="data">Значение свойства.</param>
    [SecurityCritical]
    void SetData(string name, object data);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.GetData(System.String)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   Имя предопределенного свойства домена приложения или имя определенного вами свойства домена приложения.
    /// </param>
    /// <returns>
    ///   Значение свойства <paramref name="name" />.
    /// </returns>
    object GetData(string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.SetAppDomainPolicy(System.Security.Policy.PolicyLevel)" /> метод.
    /// </summary>
    /// <param name="domainPolicy">Уровень политики безопасности.</param>
    [SecurityCritical]
    void SetAppDomainPolicy(PolicyLevel domainPolicy);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.SetThreadPrincipal(System.Security.Principal.IPrincipal)" /> метод.
    /// </summary>
    /// <param name="principal">
    ///   Объект-участник, который необходимо подключить к потоку.
    /// </param>
    void SetThreadPrincipal(IPrincipal principal);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy)" /> метод.
    /// </summary>
    /// <param name="policy">
    ///   Одно из значений <see cref="T:System.Security.Principal.PrincipalPolicy" />, определяющее тип объекта-участника, который необходимо подключить к потоку.
    /// </param>
    void SetPrincipalPolicy(PrincipalPolicy policy);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.AppDomain.DoCallBack(System.CrossAppDomainDelegate)" /> метод.
    /// </summary>
    /// <param name="theDelegate">
    ///   Делегат, определяющий вызываемый метод.
    /// </param>
    void DoCallBack(CrossAppDomainDelegate theDelegate);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.AppDomain.DynamicDirectory" /> свойство.
    /// </summary>
    /// <returns>
    ///   Получите каталог, распознаватель сборок производит поиск динамически созданных сборок.
    /// </returns>
    string DynamicDirectory { get; }
  }
}
