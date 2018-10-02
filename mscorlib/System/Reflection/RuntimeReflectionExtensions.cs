// Decompiled with JetBrains decompiler
// Type: System.Reflection.RuntimeReflectionExtensions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Reflection
{
  /// <summary>
  ///   Предоставляет методы, получающие сведения о типах во время выполнения.
  /// </summary>
  [__DynamicallyInvokable]
  public static class RuntimeReflectionExtensions
  {
    private const BindingFlags everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    private static void CheckAndThrow(Type t)
    {
      if (t == (Type) null)
        throw new ArgumentNullException("type");
      if ((object) (t as RuntimeType) == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
    }

    private static void CheckAndThrow(MethodInfo m)
    {
      if (m == (MethodInfo) null)
        throw new ArgumentNullException("method");
      if (!(m is RuntimeMethodInfo))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
    }

    /// <summary>
    ///   Получает коллекцию, которая представляет все свойства, определенные для указанного типа.
    /// </summary>
    /// <param name="type">Тип, содержащий свойства.</param>
    /// <returns>Коллекция свойств для указанного типа.</returns>
    [__DynamicallyInvokable]
    public static IEnumerable<PropertyInfo> GetRuntimeProperties(this Type type)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return (IEnumerable<PropertyInfo>) type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>
    ///   Возвращает коллекцию, представляющий все события, определенные для указанного типа.
    /// </summary>
    /// <param name="type">Тип, содержащий события.</param>
    /// <returns>Коллекция событий для указанного типа.</returns>
    [__DynamicallyInvokable]
    public static IEnumerable<EventInfo> GetRuntimeEvents(this Type type)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return (IEnumerable<EventInfo>) type.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>
    ///   Возвращает коллекцию, представляющую все методы, определенные в указанном типе.
    /// </summary>
    /// <param name="type">Тип, который содержит методы.</param>
    /// <returns>Набор методов для указанного типа.</returns>
    [__DynamicallyInvokable]
    public static IEnumerable<MethodInfo> GetRuntimeMethods(this Type type)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return (IEnumerable<MethodInfo>) type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>
    ///   Возвращает коллекцию, представляющую все поля, определенные в указанном типе.
    /// </summary>
    /// <param name="type">Тип, содержащий поля.</param>
    /// <returns>Коллекция полей для указанного типа.</returns>
    [__DynamicallyInvokable]
    public static IEnumerable<FieldInfo> GetRuntimeFields(this Type type)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return (IEnumerable<FieldInfo>) type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>Получает объект, представляющий указанное свойство.</summary>
    /// <param name="type">Тип, содержащий свойство.</param>
    /// <param name="name">Имя свойства.</param>
    /// <returns>
    ///   Объект, представляющий указанное свойство, или <see langword="null" /> если свойство не найдено.
    /// </returns>
    [__DynamicallyInvokable]
    public static PropertyInfo GetRuntimeProperty(this Type type, string name)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return type.GetProperty(name);
    }

    /// <summary>Получает объект, представляющий указанное событие.</summary>
    /// <param name="type">Тип, содержащий события.</param>
    /// <param name="name">Имя события.</param>
    /// <returns>
    ///   Объект, представляющий указанное событие, или <see langword="null" /> Если событие не найдено.
    /// </returns>
    [__DynamicallyInvokable]
    public static EventInfo GetRuntimeEvent(this Type type, string name)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return type.GetEvent(name);
    }

    /// <summary>Получает объект, представляющий указанный метод.</summary>
    /// <param name="type">Тип, содержащий метод.</param>
    /// <param name="name">Имя метода.</param>
    /// <param name="parameters">
    ///   Массив, содержащий параметры метода.
    /// </param>
    /// <returns>
    ///   Объект, представляющий заданный метод, или <see langword="null" /> Если метод не найден.
    /// </returns>
    [__DynamicallyInvokable]
    public static MethodInfo GetRuntimeMethod(this Type type, string name, Type[] parameters)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return type.GetMethod(name, parameters);
    }

    /// <summary>
    ///   Получает объект, представляющий значение заданного поля.
    /// </summary>
    /// <param name="type">Тип, содержащий поле.</param>
    /// <param name="name">Имя поля.</param>
    /// <returns>
    ///   Объект, представляющий указанное поле или <see langword="null" /> Если поле не найдено.
    /// </returns>
    [__DynamicallyInvokable]
    public static FieldInfo GetRuntimeField(this Type type, string name)
    {
      RuntimeReflectionExtensions.CheckAndThrow(type);
      return type.GetField(name);
    }

    /// <summary>
    ///   Получает объект, представляющий указанный метод прямой или косвенный базовый класс, где был первоначально объявлен метод.
    /// </summary>
    /// <param name="method">Для получения сведений о метод.</param>
    /// <returns>
    ///   Объект, представляющий указанный метод исходного объявления в базовом классе.
    /// </returns>
    [__DynamicallyInvokable]
    public static MethodInfo GetRuntimeBaseDefinition(this MethodInfo method)
    {
      RuntimeReflectionExtensions.CheckAndThrow(method);
      return method.GetBaseDefinition();
    }

    /// <summary>
    ///   Возвращает сопоставление интерфейса для указанного типа, указанного интерфейса.
    /// </summary>
    /// <param name="typeInfo">
    ///   Тип, для которого требуется извлечь сопоставление.
    /// </param>
    /// <param name="interfaceType">
    ///   Интерфейс, чтобы извлечь сопоставление.
    /// </param>
    /// <returns>
    ///   Объект, представляющий сопоставление интерфейса для типа и указанного интерфейса.
    /// </returns>
    [__DynamicallyInvokable]
    public static InterfaceMapping GetRuntimeInterfaceMap(this TypeInfo typeInfo, Type interfaceType)
    {
      if ((Type) typeInfo == (Type) null)
        throw new ArgumentNullException(nameof (typeInfo));
      if ((object) (typeInfo as RuntimeType) == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      return typeInfo.GetInterfaceMap(interfaceType);
    }

    /// <summary>
    ///   Возвращает объект, представляющий метод, представленный заданным делегатом.
    /// </summary>
    /// <param name="del">Делегат, для проверки.</param>
    /// <returns>Объект, представляющий метод.</returns>
    [__DynamicallyInvokable]
    public static MethodInfo GetMethodInfo(this Delegate del)
    {
      if ((object) del == null)
        throw new ArgumentNullException(nameof (del));
      return del.Method;
    }
  }
}
