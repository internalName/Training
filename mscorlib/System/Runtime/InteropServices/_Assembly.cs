// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._Assembly
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Policy;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет открытые члены <see cref="T:System.Reflection.Assembly" /> класс в неуправляемый код.
  /// </summary>
  [Guid("17156360-2f1a-384a-bc52-fde93c215c5b")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [TypeLibImportClass(typeof (Assembly))]
  [CLSCompliant(false)]
  [ComVisible(true)]
  public interface _Assembly
  {
    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.ToString" /> метод.
    /// </summary>
    /// <returns>
    ///   Полное имя сборки или имя класса, если полное имя сборки не может быть определено.
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
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Object" />.
    /// </returns>
    int GetHashCode();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Object.GetType" /> метод.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />.
    /// </returns>
    Type GetType();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.Assembly.CodeBase" /> свойство.
    /// </summary>
    /// <returns>Первоначально заданное расположение сборки.</returns>
    string CodeBase { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.Assembly.EscapedCodeBase" /> свойство.
    /// </summary>
    /// <returns>
    ///   Универсальный идентификатор ресурса (URI) с escape-символами.
    /// </returns>
    string EscapedCodeBase { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetName" /> метод.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.AssemblyName" /> Для этой сборки.
    /// </returns>
    AssemblyName GetName();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetName(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="copiedName">
    ///   Значение <see langword="true" />, чтобы значение свойства <see cref="P:System.Reflection.Assembly.CodeBase" /> указывало на расположение сборки после создания ее теневой копии; значение <see langword="false" />, если значение свойства <see cref="P:System.Reflection.Assembly.CodeBase" /> должно указывать на первоначальное расположение.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Reflection.AssemblyName" /> Для этой сборки.
    /// </returns>
    AssemblyName GetName(bool copiedName);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.Assembly.FullName" /> свойство.
    /// </summary>
    /// <returns>Отображаемое имя сборки.</returns>
    string FullName { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.Assembly.EntryPoint" /> свойство.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> представляющий точку входа этой сборки.
    ///    Если точка входа не найдена (например, сборка является DLL-библиотекой), возвращается значение <see langword="null" />.
    /// </returns>
    MethodInfo EntryPoint { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetType(System.String)" /> метод.
    /// </summary>
    /// <param name="name">Полное имя типа.</param>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий указанный класс, или <see langword="null" /> если класс не найден.
    /// </returns>
    Type GetType(string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetType(System.String,System.Boolean)" /> метод.
    /// </summary>
    /// <param name="name">Полное имя типа.</param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" /> для создания исключения, если тип не найден, в обратном случае — значение <see langword="false" />, в результате чего будет возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий указанный класс.
    /// </returns>
    Type GetType(string name, bool throwOnError);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetExportedTypes" /> свойство.
    /// </summary>
    /// <returns>
    ///   Массив <see cref="T:System.Type" /> объекты, представляющие типы, определенные в этой сборке и видимые за пределами сборки.
    /// </returns>
    Type[] GetExportedTypes();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetTypes" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Type" /> содержащий объекты для всех типов, определенных в этой сборке.
    /// </returns>
    Type[] GetTypes();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetManifestResourceStream(System.Type,System.String)" /> метод.
    /// </summary>
    /// <param name="type">
    ///   Тип, пространством имен которого ограничена область действия имени ресурса манифеста.
    /// </param>
    /// <param name="name">
    ///   Имя запрашиваемого ресурса манифеста, зависящее от регистра.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.IO.Stream" />, представляющий этот ресурс манифеста.
    /// </returns>
    Stream GetManifestResourceStream(Type type, string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetManifestResourceStream(System.String)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   Имя запрашиваемого ресурса манифеста, зависящее от регистра.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.IO.Stream" />, представляющий этот ресурс манифеста.
    /// </returns>
    Stream GetManifestResourceStream(string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetFile(System.String)" /> метод.
    /// </summary>
    /// <param name="name">
    ///   Имя указанного файла.
    ///    Не должно содержать путь к файлу.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.IO.FileStream" /> для указанного файла или <see langword="null" /> Если файл не найден.
    /// </returns>
    FileStream GetFile(string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetFiles" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.IO.FileStream" />.
    /// </returns>
    FileStream[] GetFiles();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetFiles(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="getResourceModules">
    ///   Значение <see langword="true" />, если необходимо включать модули ресурсов; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.IO.FileStream" />.
    /// </returns>
    FileStream[] GetFiles(bool getResourceModules);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetManifestResourceNames" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив типа <see langword="String" />, содержащий имена всех ресурсов.
    /// </returns>
    string[] GetManifestResourceNames();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetManifestResourceInfo(System.String)" /> метод.
    /// </summary>
    /// <param name="resourceName">
    ///   Имя ресурса, зависящее от регистра.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.ManifestResourceInfo" /> объект со сведениями о топологии ресурса или <see langword="null" /> Если ресурс не найден.
    /// </returns>
    ManifestResourceInfo GetManifestResourceInfo(string resourceName);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.Assembly.Location" /> свойство.
    /// </summary>
    /// <returns>
    ///   Местоположение загруженного файла, содержащего манифест.
    ///    Если для загруженного файла был создан снимок состояния, местонахождение является местонахождением файла после теневого копирования.
    ///    Если сборка загружается из массива байтов, например, при использовании метода перегрузки <see cref="M:System.Reflection.Assembly.Load(System.Byte[])" />, возвращаемое значение является пустой строкой ("").
    /// </returns>
    string Location { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.Assembly.Evidence" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Policy.Evidence" /> Для этой сборки.
    /// </returns>
    Evidence Evidence { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Type,System.Boolean)" /> метод.
    /// </summary>
    /// <param name="attributeType">
    ///   <see cref="T:System.Type" /> Для которого должны быть возвращены настраиваемые атрибуты.
    /// </param>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов типа <see cref="T:System.Reflection.Assembly" />.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Object" /> содержащий настраиваемые атрибуты для этой сборки в соответствии с <paramref name="attributeType" />.
    /// </returns>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов типа <see cref="T:System.Reflection.Assembly" />.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see langword="Object" /> содержащий настраиваемые атрибуты для этой сборки.
    /// </returns>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.IsDefined(System.Type,System.Boolean)" /> метод.
    /// </summary>
    /// <param name="attributeType">
    ///   <see cref="T:System.Type" /> Настраиваемого атрибута, проверяемого для этой сборки.
    /// </param>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если пользовательский атрибут, заданный указанным <see cref="T:System.Type" /> определен; в противном случае — <see langword="false" />.
    /// </returns>
    bool IsDefined(Type attributeType, bool inherit);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> метод.
    /// </summary>
    /// <param name="info">
    ///   Объект, для которого будут заполнены сведения о сериализации.
    /// </param>
    /// <param name="context">Контекст назначения сериализации.</param>
    [SecurityCritical]
    void GetObjectData(SerializationInfo info, StreamingContext context);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="E:System.Reflection.Assembly.ModuleResolve" /> события.
    /// </summary>
    event ModuleResolveEventHandler ModuleResolve;

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetType(System.String,System.Boolean,System.Boolean)" /> метод.
    /// </summary>
    /// <param name="name">Полное имя типа.</param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" /> для создания исключения, если тип не найден, в обратном случае — значение <see langword="false" />, в результате чего будет возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы игнорировать регистр имени типа, в обратном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий указанный класс.
    /// </returns>
    Type GetType(string name, bool throwOnError, bool ignoreCase);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetSatelliteAssembly(System.Globalization.CultureInfo)" /> метод.
    /// </summary>
    /// <param name="culture">
    ///   Заданные язык и региональные параметры.
    /// </param>
    /// <returns>Указанная вспомогательная сборка.</returns>
    Assembly GetSatelliteAssembly(CultureInfo culture);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetSatelliteAssembly(System.Globalization.CultureInfo,System.Version)" /> метод.
    /// </summary>
    /// <param name="culture">
    ///   Заданные язык и региональные параметры.
    /// </param>
    /// <param name="version">Версия вспомогательной сборки.</param>
    /// <returns>Указанная вспомогательная сборка.</returns>
    Assembly GetSatelliteAssembly(CultureInfo culture, Version version);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.LoadModule(System.String,System.Byte[])" /> метод.
    /// </summary>
    /// <param name="moduleName">
    ///   Имя модуля.
    ///    Должно соответствовать имени файла в манифесте этой сборки.
    /// </param>
    /// <param name="rawModule">
    ///   Массив байтов, который является COFF-образом, содержащим передаваемый модуль или ресурс.
    /// </param>
    /// <returns>Загруженный модуль.</returns>
    Module LoadModule(string moduleName, byte[] rawModule);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.LoadModule(System.String,System.Byte[],System.Byte[])" /> метод.
    /// </summary>
    /// <param name="moduleName">
    ///   Имя модуля.
    ///    Должно соответствовать имени файла в манифесте этой сборки.
    /// </param>
    /// <param name="rawModule">
    ///   Массив байтов, который является COFF-образом, содержащим передаваемый модуль или ресурс.
    /// </param>
    /// <param name="rawSymbolStore">
    ///   Массив байтов, содержащий необработанные байты, представляющие собой символы для модуля.
    ///    Для файла ресурсов должно быть задано значение <see langword="null" />.
    /// </param>
    /// <returns>Загруженный модуль.</returns>
    Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.CreateInstance(System.String)" /> метод.
    /// </summary>
    /// <param name="typeName">
    ///   <see cref="P:System.Type.FullName" /> искомого типа.
    /// </param>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Object" /> предоставляет тип с языком и региональными параметрами, аргументы, привязки и активации установлены в <see langword="null" />, и <see cref="T:System.Reflection.BindingFlags" /> равным Public или Instance, или <see langword="null" /> Если <paramref name="typeName" /> не найден.
    /// </returns>
    object CreateInstance(string typeName);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.CreateInstance(System.String,System.Boolean)" /> метод.
    /// </summary>
    /// <param name="typeName">
    ///   <see cref="P:System.Type.FullName" /> искомого типа.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы игнорировать регистр имени типа, в обратном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Object" /> предоставляет тип с языком и региональными параметрами, аргументы, привязки и активации установлены в <see langword="null" />, и <see cref="T:System.Reflection.BindingFlags" /> равным Public или Instance, или <see langword="null" /> Если <paramref name="typeName" /> не найден.
    /// </returns>
    object CreateInstance(string typeName, bool ignoreCase);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.CreateInstance(System.String,System.Boolean,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo,System.Object[])" /> метод.
    /// </summary>
    /// <param name="typeName">
    ///   <see cref="P:System.Type.FullName" /> искомого типа.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы игнорировать регистр имени типа, в обратном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияет на то, как ведется поиск.
    ///    Значение является сочетанием одноразрядных флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see langword="MemberInfo" /> с помощью отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="args">
    ///   Массив объектов типа <see langword="Object" /> содержащий аргументы, передаваемые конструктору.
    ///    Этот массив аргументов должен по числу, порядку и типу аргументов соответствовать параметрам вызываемого конструктора.
    ///    Если нужен конструктор по умолчанию, <paramref name="args" /> должен быть пустым массивом или <see langword="null" />.
    /// </param>
    /// <param name="culture">
    ///   Экземпляр объекта <see langword="CultureInfo" />, используемого для управления приведением типов.
    ///    Если значение этого объекта — <see langword="null" />, для текущего потока используется <see langword="CultureInfo" />.
    ///    (Например, необходимо преобразовывать объект <see langword="String" />, представляющий 1000, в значение <see langword="Double" />, поскольку при разных языках и региональных параметрах 1000 представляется по-разному.)
    /// </param>
    /// <param name="activationAttributes">
    ///   Массив объектов типа <see langword="Object" /> содержащий один или несколько атрибутов активации, которые могут участвовать в активации.
    ///    — Пример атрибута активации:
    /// 
    ///   URLAttribute (http://hostname/appname/objectURI)
    /// </param>
    /// <returns>
    ///   Экземпляр <see langword="Object" /> представляющий указанный тип и соответствующий заданным критериям, или <see langword="null" /> Если <paramref name="typeName" /> не найден.
    /// </returns>
    object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetLoadedModules" /> метод.
    /// </summary>
    /// <returns>Массив модулей.</returns>
    Module[] GetLoadedModules();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetLoadedModules(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="getResourceModules">
    ///   Значение <see langword="true" />, если необходимо включать модули ресурсов; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>Массив модулей.</returns>
    Module[] GetLoadedModules(bool getResourceModules);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetModules" /> метод.
    /// </summary>
    /// <returns>Массив модулей.</returns>
    Module[] GetModules();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetModules(System.Boolean)" /> метод.
    /// </summary>
    /// <param name="getResourceModules">
    ///   Значение <see langword="true" />, если необходимо включать модули ресурсов; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>Массив модулей.</returns>
    Module[] GetModules(bool getResourceModules);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetModule(System.String)" /> метод.
    /// </summary>
    /// <param name="name">Имя запрашиваемого модуля.</param>
    /// <returns>
    ///   Запрашиваемый модуль или значение <see langword="null" />, если модуль не найден.
    /// </returns>
    Module GetModule(string name);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Reflection.Assembly.GetReferencedAssemblies" /> метод.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Reflection.AssemblyName" /> содержащий все сборки, на которые ссылается данная сборка.
    /// </returns>
    AssemblyName[] GetReferencedAssemblies();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Reflection.Assembly.GlobalAssemblyCache" /> свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если сборка была загружена из глобального кэша сборок, в обратном случае — значение <see langword="false" />.
    /// </returns>
    bool GlobalAssemblyCache { get; }
  }
}
