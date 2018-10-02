// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.IRegistrationServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет набор служб для регистрации и отмены регистрации управляемых сборок для использования из COM.
  /// </summary>
  [Guid("CCBD682C-73A5-4568-B8B0-C7007E11ABA2")]
  [ComVisible(true)]
  public interface IRegistrationServices
  {
    /// <summary>
    ///   Регистрирует классы в управляемой сборке для поддержки создания из COM.
    /// </summary>
    /// <param name="assembly">Регистрируемая сборка.</param>
    /// <param name="flags">
    ///   <see cref="T:System.Runtime.InteropServices.AssemblyRegistrationFlags" /> Значение, указывающее, специальные параметры, необходимые при регистрации <paramref name="assembly" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="assembly" /> содержит типы, которые были успешно зарегистрирован; в противном случае <see langword="false" /> Если сборка не содержит подходящих типов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Полное имя <paramref name="assembly" /> — <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Метод, помеченный атрибутом <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> не <see langword="static" />.
    /// 
    ///   -или-
    /// 
    ///   Существует более одного метода, помеченного <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> на данном уровне иерархии.
    /// 
    ///   -или-
    /// 
    ///   Подпись метода, помеченного <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> является недопустимым.
    /// </exception>
    [SecurityCritical]
    bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags);

    /// <summary>Отменяет регистрацию классов в управляемой сборке.</summary>
    /// <param name="assembly">Сборка для отмены регистрации.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="assembly" /> содержит типы, которые были успешно удалена; в противном случае <see langword="false" /> Если сборка не содержит подходящих типов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="assembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Полное имя <paramref name="assembly" /> — <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Метод, помеченный атрибутом <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> не <see langword="static" />.
    /// 
    ///   -или-
    /// 
    ///   Существует более одного метода, помеченного <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> на данном уровне иерархии.
    /// 
    ///   -или-
    /// 
    ///   Подпись метода, помеченного <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> является недопустимым.
    /// </exception>
    [SecurityCritical]
    bool UnregisterAssembly(Assembly assembly);

    /// <summary>
    ///   Получает список классов в сборке, которая будет зарегистрирована с помощью вызова <see cref="M:System.Runtime.InteropServices.IRegistrationServices.RegisterAssembly(System.Reflection.Assembly,System.Runtime.InteropServices.AssemblyRegistrationFlags)" />.
    /// </summary>
    /// <param name="assembly">Сборка для поиска классов.</param>
    /// <returns>
    ///   A <see cref="T:System.Type" /> массив, содержащий список классов в <paramref name="assembly" />.
    /// </returns>
    [SecurityCritical]
    Type[] GetRegistrableTypesInAssembly(Assembly assembly);

    /// <summary>
    ///   Извлекает идентификатор ProgID COM для указанного типа.
    /// </summary>
    /// <param name="type">Тип запрашиваемого которого ProgID.</param>
    /// <returns>Идентификатор ProgID для указанного типа.</returns>
    [SecurityCritical]
    string GetProgIdForType(Type type);

    /// <summary>
    ///   Регистрирует указанный тип в COM, используя указанный идентификатор GUID.
    /// </summary>
    /// <param name="type">
    ///   Тип, регистрируемый для использования из COM.
    /// </param>
    /// <param name="g">
    ///   Идентификатор GUID, используемый для регистрации указанного типа.
    /// </param>
    [SecurityCritical]
    void RegisterTypeForComClients(Type type, ref Guid g);

    /// <summary>
    ///   Возвращает идентификатор GUID категории COM, содержащей управляемые классы.
    /// </summary>
    /// <returns>
    ///   Идентификатор GUID категории COM, содержащей управляемые классы.
    /// </returns>
    Guid GetManagedCategoryGuid();

    /// <summary>Определяет, требуется ли указанный тип регистрации.</summary>
    /// <param name="type">
    ///   Проверяемый тип требования к регистрации COM.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если этот тип должен быть зарегистрирован для использования из COM; в противном случае <see langword="false" />.
    /// </returns>
    [SecurityCritical]
    bool TypeRequiresRegistration(Type type);

    /// <summary>Определяет, является ли указанный тип COM-типом.</summary>
    /// <param name="type">Тип для определения COM-типом.</param>
    /// <returns>
    ///   <see langword="true" /> Если указанный тип является COM-типом; в противном случае <see langword="false" />.
    /// </returns>
    bool TypeRepresentsComType(Type type);
  }
}
