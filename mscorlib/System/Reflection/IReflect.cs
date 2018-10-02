// Decompiled with JetBrains decompiler
// Type: System.Reflection.IReflect
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>Взаимодействует с интерфейса IDispatch.</summary>
  [Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
  [ComVisible(true)]
  public interface IReflect
  {
    /// <summary>
    ///   Извлекает <see cref="T:System.Reflection.MethodInfo" /> объект, соответствующий заданному методу, с помощью <see cref="T:System.Type" /> массива для выбора из одного из перегруженных методов.
    /// </summary>
    /// <param name="name">Имя члена, который требуется найти.</param>
    /// <param name="bindingAttr">
    ///   Атрибуты привязки, используемые для управления поиском.
    /// </param>
    /// <param name="binder">
    ///   Объект, реализующий интерфейс <see cref="T:System.Reflection.Binder" />, содержащий свойства, связанные с этим методом.
    /// </param>
    /// <param name="types">
    ///   Массив, используемый для выбора одного из перегруженных методов.
    /// </param>
    /// <param name="modifiers">
    ///   Массив модификаторов параметров, используемый для работы привязки с подписями параметров, в которых были изменены типы.
    /// </param>
    /// <returns>
    ///   Запрошенный метод, который соответствует всем заданным параметрам.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Объект реализует несколько методов с одинаковыми именами.
    /// </exception>
    MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   Извлекает <see cref="T:System.Reflection.MethodInfo" /> объект, соответствующий заданному методу при заданных ограничениях поиска.
    /// </summary>
    /// <param name="name">Имя члена, который требуется найти.</param>
    /// <param name="bindingAttr">
    ///   Атрибуты привязки, используемые для управления поиском.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> объект, содержащий данные метода, отвечающих критериям, основанного на указанный в ограничения и имя метода <paramref name="bindingAttr" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Объект реализует несколько методов с одинаковыми именами.
    /// </exception>
    MethodInfo GetMethod(string name, BindingFlags bindingAttr);

    /// <summary>
    ///   Извлекает массив <see cref="T:System.Reflection.MethodInfo" /> объектов со все открытые методы или всеми методами текущего класса.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Атрибуты привязки, используемые для управления поиском.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Reflection.MethodInfo" /> объектов, содержащий все методы, определенные для этого объекта отражения, который удовлетворяет ограничениям поиска, указанного в <paramref name="bindingAttr" />.
    /// </returns>
    MethodInfo[] GetMethods(BindingFlags bindingAttr);

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.FieldInfo" /> объект, соответствующий указанному полю и флаг привязки.
    /// </summary>
    /// <param name="name">Имя поля для поиска.</param>
    /// <param name="bindingAttr">
    ///   Атрибуты привязки, используемые для управления поиском.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.FieldInfo" /> объект, содержащий данные поля для именованного объекта, который удовлетворяет ограничениям поиска, задаваемым параметром <paramref name="bindingAttr" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   В объекте реализовано несколько полей с тем же именем.
    /// </exception>
    FieldInfo GetField(string name, BindingFlags bindingAttr);

    /// <summary>
    ///   Возвращает массив <see cref="T:System.Reflection.FieldInfo" /> объекты, которые соответствуют всем полям текущего класса.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Атрибуты привязки, используемые для управления поиском.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Reflection.FieldInfo" /> объектов, содержащий все сведения о полях для этого объекта отражения, который удовлетворяет ограничениям поиска, задаваемым параметром <paramref name="bindingAttr" />.
    /// </returns>
    FieldInfo[] GetFields(BindingFlags bindingAttr);

    /// <summary>
    ///   Извлекает <see cref="T:System.Reflection.PropertyInfo" /> объект, соответствующий заданному свойству при заданных ограничениях поиска.
    /// </summary>
    /// <param name="name">Имя искомого свойства.</param>
    /// <param name="bindingAttr">
    ///   Атрибуты привязки, используемые для управления поиском.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.PropertyInfo" /> объект для найденного свойства, который удовлетворяет ограничениям поиска, задаваемым параметром <paramref name="bindingAttr" />, или <see langword="null" /> Если свойство не было найдено.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   В объекте реализовано несколько полей с тем же именем.
    /// </exception>
    PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

    /// <summary>
    ///   Извлекает <see cref="T:System.Reflection.PropertyInfo" /> объект, соответствующий заданному свойству при заданных ограничениях поиска.
    /// </summary>
    /// <param name="name">Имя члена, который требуется найти.</param>
    /// <param name="bindingAttr">
    ///   Атрибут привязки, используемый для управления поиском.
    /// </param>
    /// <param name="binder">
    ///   Объект, реализующий <see cref="T:System.Reflection.Binder" />, содержащий свойства, связанные с этим методом.
    /// </param>
    /// <param name="returnType">Тип свойства.</param>
    /// <param name="types">
    ///   Массив, используемый для выбора одного из перегруженных методов с тем же именем.
    /// </param>
    /// <param name="modifiers">
    ///   Массив, используемый для выбора модификаторов параметров.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.PropertyInfo" /> объект для найденного свойства, если свойство с заданным именем было найдено в данном объекте отражения, или <see langword="null" /> Если свойство не было найдено.
    /// </returns>
    PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   Извлекает массив <see cref="T:System.Reflection.PropertyInfo" /> объектов, соответствующих всем открытым свойствам или всем свойствам текущего класса.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Атрибут привязки, используемый для управления поиском.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Reflection.PropertyInfo" /> объектов для всех свойств, определенных для данного объекта отражения.
    /// </returns>
    PropertyInfo[] GetProperties(BindingFlags bindingAttr);

    /// <summary>
    ///   Извлекает массив <see cref="T:System.Reflection.MemberInfo" /> объектов, соответствующих всем открытым членам или всем членам, которые удовлетворяют заданному имени.
    /// </summary>
    /// <param name="name">Имя члена, который требуется найти.</param>
    /// <param name="bindingAttr">
    ///   Атрибуты привязки, используемые для управления поиском.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Reflection.MemberInfo" /> объектов сопоставления <paramref name="name" /> параметр.
    /// </returns>
    MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

    /// <summary>
    ///   Извлекает массив <see cref="T:System.Reflection.MemberInfo" /> объектов, соответствующих всем открытым членам или всем членам текущего класса.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Атрибуты привязки, используемые для управления поиском.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Reflection.MemberInfo" /> объектов, содержащий все сведения о членстве для данного объекта отражения.
    /// </returns>
    MemberInfo[] GetMembers(BindingFlags bindingAttr);

    /// <summary>Вызывает указанный член.</summary>
    /// <param name="name">Имя члена, который требуется найти.</param>
    /// <param name="invokeAttr">
    ///   Один из <see cref="T:System.Reflection.BindingFlags" /> атрибутов вызова.
    ///   <paramref name="invokeAttr" /> Параметр может быть конструктор, метод, свойство или поле.
    ///    Необходимо указать подходящий атрибут вызова.
    ///    Вызван член по умолчанию класса, передав пустую строку («») как имя элемента.
    /// </param>
    /// <param name="binder">
    ///   Один из <see cref="T:System.Reflection.BindingFlags" /> битовые флаги.
    ///    Реализует <see cref="T:System.Reflection.Binder" />, содержащий свойства, связанные с этим методом.
    /// </param>
    /// <param name="target">
    ///   Объект, для которого следует вызвать указанный член.
    ///    Этот параметр учитывается для статических элементов.
    /// </param>
    /// <param name="args">
    ///   Массив объектов, содержащий число, порядок и тип параметров члена.
    ///    Это пустой массив, если нет параметров.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />.
    ///    Массив такой же длины, как <paramref name="args" /> параметр, предоставляющий атрибуты аргументов вызываемого элемента в метаданных.
    ///    Параметр может иметь следующие атрибуты: <see langword="pdIn" />, <see langword="pdOut" />, <see langword="pdRetval" />, <see langword="pdOptional" />, и <see langword="pdHasDefault" />.
    ///    Эти значения представляют [In], [Out], [retval], [необязательно] и параметр по умолчанию, соответственно.
    ///    Эти атрибуты используются различными службами взаимодействия.
    /// </param>
    /// <param name="culture">
    ///   Экземпляр объекта <see cref="T:System.Globalization.CultureInfo" />, используемого для управления приведением типов.
    ///    Например <paramref name="culture" /> преобразует <see langword="String" /> представляющий 1000, в <see langword="Double" /> значение, поскольку в разных культурах 1000 представляется по-разному.
    ///    Если этот параметр равен <see langword="null" />,  <see cref="T:System.Globalization.CultureInfo" /> для текущего потока используется.
    /// </param>
    /// <param name="namedParameters">
    ///   A <see langword="String" /> массив параметров.
    /// </param>
    /// <returns>Указанный член.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="invokeAttr" /> — <see cref="F:System.Reflection.BindingFlags.CreateInstance" /> и другой двоичный флаг также устанавливается.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="invokeAttr" /> не <see cref="F:System.Reflection.BindingFlags.CreateInstance" /> и <paramref name="name" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="invokeAttr" /> не является атрибутом вызова из <see cref="T:System.Reflection.BindingFlags" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="invokeAttr" /> Указывает, как <see langword="get" /> и <see langword="set" /> для свойства или поля.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="invokeAttr" /> Определяет поле как <see langword="set" /> и <see langword="Invoke" /> метод.
    ///   <paramref name="args" /> предоставляются для поля <see langword="get" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Для поля задано более одного аргумента <see langword="set" />.
    /// </exception>
    /// <exception cref="T:System.MissingFieldException">
    ///   Невозможно найти поле или свойство.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Не удается найти метод.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Закрытый член вызван без необходимого <see cref="T:System.Security.Permissions.ReflectionPermission" />.
    /// </exception>
    object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

    /// <summary>
    ///   Возвращает базовый тип, представляющий <see cref="T:System.Reflection.IReflect" /> объекта.
    /// </summary>
    /// <returns>
    ///   Базовый тип, представляющий <see cref="T:System.Reflection.IReflect" /> объекта.
    /// </returns>
    Type UnderlyingSystemType { get; }
  }
}
