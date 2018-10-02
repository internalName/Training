// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeExtensions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Reflection
{
  /// <summary>
  ///   Содержит статические методы для извлечения настраиваемых атрибутов.
  /// </summary>
  [__DynamicallyInvokable]
  public static class CustomAttributeExtensions
  {
    /// <summary>
    ///   Извлекает пользовательский атрибут указанного типа, который применяется к указанной сборке.
    /// </summary>
    /// <param name="element">Сборка для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <returns>
    ///   Настраиваемый атрибут, который соответствует <paramref name="attributeType" />, или <see langword="null" /> если атрибут не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более одного из атрибутов.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(this Assembly element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType);
    }

    /// <summary>
    ///   Извлекает пользовательский атрибут указанного типа, который применяется для указанного модуля.
    /// </summary>
    /// <param name="element">Модуль для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <returns>
    ///   Настраиваемый атрибут, который соответствует <paramref name="attributeType" />, или <see langword="null" /> если атрибут не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более одного из атрибутов.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(this Module element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType);
    }

    /// <summary>
    ///   Извлекает пользовательский атрибут указанного типа, который применяется для указанного элемента.
    /// </summary>
    /// <param name="element">Элемент для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <returns>
    ///   Настраиваемый атрибут, который соответствует <paramref name="attributeType" />, или <see langword="null" /> если атрибут не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более одного из атрибутов.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType);
    }

    /// <summary>
    ///   Извлекает пользовательский атрибут указанного типа, который применен к заданному параметру.
    /// </summary>
    /// <param name="element">Параметр для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <returns>
    ///   Настраиваемый атрибут, который соответствует <paramref name="attributeType" />, или <see langword="null" /> если атрибут не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более одного из атрибутов.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType);
    }

    /// <summary>
    ///   Извлекает пользовательский атрибут указанного типа, который применяется к указанной сборке.
    /// </summary>
    /// <param name="element">Сборка для проверки.</param>
    /// <typeparam name="T">Тип атрибута для поиска.</typeparam>
    /// <returns>
    ///   Настраиваемый атрибут, который соответствует <paramref name="T" />, или <see langword="null" /> если атрибут не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более одного из атрибутов.
    /// </exception>
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this Assembly element) where T : Attribute
    {
      return (T) element.GetCustomAttribute(typeof (T));
    }

    /// <summary>
    ///   Извлекает пользовательский атрибут указанного типа, который применяется для указанного модуля.
    /// </summary>
    /// <param name="element">Модуль для проверки.</param>
    /// <typeparam name="T">Тип атрибута для поиска.</typeparam>
    /// <returns>
    ///   Настраиваемый атрибут, который соответствует <paramref name="T" />, или <see langword="null" /> если атрибут не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более одного из атрибутов.
    /// </exception>
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this Module element) where T : Attribute
    {
      return (T) element.GetCustomAttribute(typeof (T));
    }

    /// <summary>
    ///   Извлекает пользовательский атрибут указанного типа, который применяется для указанного элемента.
    /// </summary>
    /// <param name="element">Элемент для проверки.</param>
    /// <typeparam name="T">Тип атрибута для поиска.</typeparam>
    /// <returns>
    ///   Настраиваемый атрибут, который соответствует <paramref name="T" />, или <see langword="null" /> если атрибут не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более одного из атрибутов.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
    {
      return (T) element.GetCustomAttribute(typeof (T));
    }

    /// <summary>
    ///   Извлекает пользовательский атрибут указанного типа, который применен к заданному параметру.
    /// </summary>
    /// <param name="element">Параметр для проверки.</param>
    /// <typeparam name="T">Тип атрибута для поиска.</typeparam>
    /// <returns>
    ///   Настраиваемый атрибут, который соответствует <paramref name="T" />, или <see langword="null" /> если атрибут не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более одного из атрибутов.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this ParameterInfo element) where T : Attribute
    {
      return (T) element.GetCustomAttribute(typeof (T));
    }

    /// <summary>
    ///   Извлекает пользовательский атрибут указанного типа, применяется для заданного элемента и при необходимости проверяет предков этого члена.
    /// </summary>
    /// <param name="element">Элемент для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <param name="inherit">
    ///   <see langword="true" /> для проверки родительских элементов <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Настраиваемый атрибут, который соответствует <paramref name="attributeType" />, или <see langword="null" /> если атрибут не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более одного из атрибутов.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType, bool inherit)
    {
      return Attribute.GetCustomAttribute(element, attributeType, inherit);
    }

    /// <summary>
    ///   Извлекает пользовательский атрибут указанного типа, применяется к указанному параметру и при необходимости проверяет предков этого параметра.
    /// </summary>
    /// <param name="element">Параметр для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <param name="inherit">
    ///   <see langword="true" /> для проверки родительских элементов <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Соответствующий пользовательский атрибут <paramref name="attributeType" />, или <see langword="null" /> если атрибут не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более одного из атрибутов.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType, bool inherit)
    {
      return Attribute.GetCustomAttribute(element, attributeType, inherit);
    }

    /// <summary>
    ///   Извлекает пользовательский атрибут указанного типа, применяется для заданного элемента и при необходимости проверяет предков этого члена.
    /// </summary>
    /// <param name="element">Элемент для проверки.</param>
    /// <param name="inherit">
    ///   <see langword="true" /> для проверки родительских элементов <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <typeparam name="T">Тип атрибута для поиска.</typeparam>
    /// <returns>
    ///   Настраиваемый атрибут, который соответствует <paramref name="T" />, или <see langword="null" /> если атрибут не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более одного из атрибутов.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this MemberInfo element, bool inherit) where T : Attribute
    {
      return (T) element.GetCustomAttribute(typeof (T), inherit);
    }

    /// <summary>
    ///   Извлекает пользовательский атрибут указанного типа, применяется к указанному параметру и при необходимости проверяет предков этого параметра.
    /// </summary>
    /// <param name="element">Параметр для проверки.</param>
    /// <param name="inherit">
    ///   <see langword="true" /> для проверки родительских элементов <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <typeparam name="T">Тип атрибута для поиска.</typeparam>
    /// <returns>
    ///   Настраиваемый атрибут, который соответствует <paramref name="T" />, или <see langword="null" /> если атрибут не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более одного из атрибутов.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static T GetCustomAttribute<T>(this ParameterInfo element, bool inherit) where T : Attribute
    {
      return (T) element.GetCustomAttribute(typeof (T), inherit);
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов, применяемых к указанной сборке.
    /// </summary>
    /// <param name="element">Сборка для проверки.</param>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element);
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов, которые применяются для указанного модуля.
    /// </summary>
    /// <param name="element">Модуль для проверки.</param>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this Module element)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element);
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов, применяемых к указанному члену.
    /// </summary>
    /// <param name="element">Элемент для проверки.</param>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element);
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов, которые применены к заданному параметру.
    /// </summary>
    /// <param name="element">Параметр для проверки.</param>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element);
    }

    /// <summary>
    ///   Получает коллекцию настраиваемых атрибутов, применяемых к указанному члену и при необходимости проверяет предков этого члена.
    /// </summary>
    /// <param name="element">Элемент для проверки.</param>
    /// <param name="inherit">
    ///   <see langword="true" /> для проверки родительских элементов <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> соответствующих заданным критериям, или пустая коллекция, если атрибуты не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, bool inherit)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, inherit);
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов, которые применены к заданному параметру и при необходимости проверяет предков этого параметра.
    /// </summary>
    /// <param name="element">Параметр для проверки.</param>
    /// <param name="inherit">
    ///   <see langword="true" /> для проверки родительских элементов <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, bool inherit)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, inherit);
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов указанного типа, применяемых к указанной сборке.
    /// </summary>
    /// <param name="element">Сборка для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> и которые соответствуют <paramref name="attributeType" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element, Type attributeType)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, attributeType);
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов указанного типа, которые применены к заданному модулю.
    /// </summary>
    /// <param name="element">Модуль для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> и которые соответствуют <paramref name="attributeType" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this Module element, Type attributeType)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, attributeType);
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов указанного типа, применяемых к указанному члену.
    /// </summary>
    /// <param name="element">Элемент для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> и которые соответствуют <paramref name="attributeType" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, attributeType);
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов указанного типа, которые применены к заданному параметру.
    /// </summary>
    /// <param name="element">Параметр для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> и которые соответствуют <paramref name="attributeType" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, attributeType);
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов указанного типа, применяемых к указанной сборке.
    /// </summary>
    /// <param name="element">Сборка для проверки.</param>
    /// <typeparam name="T">Тип атрибута для поиска.</typeparam>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> и которые соответствуют <paramref name="T" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<T> GetCustomAttributes<T>(this Assembly element) where T : Attribute
    {
      return (IEnumerable<T>) element.GetCustomAttributes(typeof (T));
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов указанного типа, которые применены к заданному модулю.
    /// </summary>
    /// <param name="element">Модуль для проверки.</param>
    /// <typeparam name="T">Тип атрибута для поиска.</typeparam>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> и которые соответствуют <paramref name="T" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<T> GetCustomAttributes<T>(this Module element) where T : Attribute
    {
      return (IEnumerable<T>) element.GetCustomAttributes(typeof (T));
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов указанного типа, применяемых к указанному члену.
    /// </summary>
    /// <param name="element">Элемент для проверки.</param>
    /// <typeparam name="T">Тип атрибута для поиска.</typeparam>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> и которые соответствуют <paramref name="T" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element) where T : Attribute
    {
      return (IEnumerable<T>) element.GetCustomAttributes(typeof (T));
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов указанного типа, которые применены к заданному параметру.
    /// </summary>
    /// <param name="element">Параметр для проверки.</param>
    /// <typeparam name="T">Тип атрибута для поиска.</typeparam>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> и которые соответствуют <paramref name="T" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element) where T : Attribute
    {
      return (IEnumerable<T>) element.GetCustomAttributes(typeof (T));
    }

    /// <summary>
    ///   Получает коллекцию настраиваемых атрибутов указанного типа, применяемых к указанному члену и при необходимости проверяет предков этого члена.
    /// </summary>
    /// <param name="element">Элемент для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <param name="inherit">
    ///   <see langword="true" /> для проверки родительских элементов <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> и которые соответствуют <paramref name="attributeType" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType, bool inherit)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, attributeType, inherit);
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов указанного типа, которые применены к заданному параметру и при необходимости проверяет предков этого параметра.
    /// </summary>
    /// <param name="element">Параметр для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <param name="inherit">
    ///   <see langword="true" /> для проверки родительских элементов <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> и которые соответствуют <paramref name="attributeType" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType, bool inherit)
    {
      return (IEnumerable<Attribute>) Attribute.GetCustomAttributes(element, attributeType, inherit);
    }

    /// <summary>
    ///   Получает коллекцию настраиваемых атрибутов указанного типа, применяемых к указанному члену и при необходимости проверяет предков этого члена.
    /// </summary>
    /// <param name="element">Элемент для проверки.</param>
    /// <param name="inherit">
    ///   <see langword="true" /> для проверки родительских элементов <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <typeparam name="T">Тип атрибута для поиска.</typeparam>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> и которые соответствуют <paramref name="T" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element, bool inherit) where T : Attribute
    {
      return (IEnumerable<T>) CustomAttributeExtensions.GetCustomAttributes(element, typeof (T), inherit);
    }

    /// <summary>
    ///   Возвращает коллекцию настраиваемых атрибутов указанного типа, которые применены к заданному параметру и при необходимости проверяет предков этого параметра.
    /// </summary>
    /// <param name="element">Параметр для проверки.</param>
    /// <param name="inherit">
    ///   <see langword="true" /> для проверки родительских элементов <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <typeparam name="T">Тип атрибута для поиска.</typeparam>
    /// <returns>
    ///   Коллекция настраиваемых атрибутов, которые применяются к <paramref name="element" /> и которые соответствуют <paramref name="T" />, или пустая коллекция, если атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element, bool inherit) where T : Attribute
    {
      return (IEnumerable<T>) CustomAttributeExtensions.GetCustomAttributes(element, typeof (T), inherit);
    }

    /// <summary>
    ///   Указывает, применяются ли настраиваемые атрибуты заданного типа к указанной сборке.
    /// </summary>
    /// <param name="element">Сборка для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <returns>
    ///   <see langword="true" /> Если применяется атрибут указанного типа <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(this Assembly element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType);
    }

    /// <summary>
    ///   Указывает, применяются ли настраиваемые атрибуты заданного типа для указанного модуля.
    /// </summary>
    /// <param name="element">Модуль для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <returns>
    ///   <see langword="true" /> Если применяется атрибут указанного типа <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(this Module element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType);
    }

    /// <summary>
    ///   Указывает, применяются ли настраиваемые атрибуты заданного типа к указанному члену.
    /// </summary>
    /// <param name="element">Элемент для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <returns>
    ///   <see langword="true" /> Если применяется атрибут указанного типа <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(this MemberInfo element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType);
    }

    /// <summary>
    ///   Указывает, применяются ли настраиваемые атрибуты заданного типа к указанному параметру.
    /// </summary>
    /// <param name="element">Параметр для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <returns>
    ///   <see langword="true" /> Если применяется атрибут указанного типа <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(this ParameterInfo element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType);
    }

    /// <summary>
    ///   Указывает ли настраиваемые атрибуты заданного типа применяется к указанному члену и, при необходимости применения к его родительским элементам.
    /// </summary>
    /// <param name="element">Элемент для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <param name="inherit">
    ///   <see langword="true" /> для проверки родительских элементов <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если применяется атрибут указанного типа <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" /> не является конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(this MemberInfo element, Type attributeType, bool inherit)
    {
      return Attribute.IsDefined(element, attributeType, inherit);
    }

    /// <summary>
    ///   Указывает ли настраиваемые атрибуты заданного типа указанному параметру применяется, а, кроме того, к его родительским элементам.
    /// </summary>
    /// <param name="element">Параметр для проверки.</param>
    /// <param name="attributeType">Тип атрибута для поиска.</param>
    /// <param name="inherit">
    ///   <see langword="true" /> для проверки родительских элементов <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если применяется атрибут указанного типа <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(this ParameterInfo element, Type attributeType, bool inherit)
    {
      return Attribute.IsDefined(element, attributeType, inherit);
    }
  }
}
