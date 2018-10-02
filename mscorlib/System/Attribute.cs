// Decompiled with JetBrains decompiler
// Type: System.Attribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  /// <summary>
  ///   Представляет базовый класс для настраиваемых атрибутов.
  /// </summary>
  [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Attribute))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Attribute : _Attribute
  {
    private static Attribute[] InternalGetCustomAttributes(PropertyInfo element, Type type, bool inherit)
    {
      Attribute[] customAttributes1 = (Attribute[]) element.GetCustomAttributes(type, inherit);
      if (!inherit)
        return customAttributes1;
      Dictionary<Type, AttributeUsageAttribute> types = new Dictionary<Type, AttributeUsageAttribute>(11);
      List<Attribute> attributeList = new List<Attribute>();
      Attribute.CopyToArrayList(attributeList, customAttributes1, types);
      Type[] indexParameterTypes = Attribute.GetIndexParameterTypes(element);
      for (PropertyInfo parentDefinition = Attribute.GetParentDefinition(element, indexParameterTypes); parentDefinition != (PropertyInfo) null; parentDefinition = Attribute.GetParentDefinition(parentDefinition, indexParameterTypes))
      {
        Attribute[] customAttributes2 = Attribute.GetCustomAttributes((MemberInfo) parentDefinition, type, false);
        Attribute.AddAttributesToList(attributeList, customAttributes2, types);
      }
      Array attributeArrayHelper = (Array) Attribute.CreateAttributeArrayHelper(type, attributeList.Count);
      Array.Copy((Array) attributeList.ToArray(), 0, attributeArrayHelper, 0, attributeList.Count);
      return (Attribute[]) attributeArrayHelper;
    }

    private static bool InternalIsDefined(PropertyInfo element, Type attributeType, bool inherit)
    {
      if (element.IsDefined(attributeType, inherit))
        return true;
      if (inherit && Attribute.InternalGetAttributeUsage(attributeType).Inherited)
      {
        Type[] indexParameterTypes = Attribute.GetIndexParameterTypes(element);
        for (PropertyInfo parentDefinition = Attribute.GetParentDefinition(element, indexParameterTypes); parentDefinition != (PropertyInfo) null; parentDefinition = Attribute.GetParentDefinition(parentDefinition, indexParameterTypes))
        {
          if (parentDefinition.IsDefined(attributeType, false))
            return true;
        }
      }
      return false;
    }

    private static PropertyInfo GetParentDefinition(PropertyInfo property, Type[] propertyParameters)
    {
      MethodInfo methodInfo = property.GetGetMethod(true);
      if (methodInfo == (MethodInfo) null)
        methodInfo = property.GetSetMethod(true);
      RuntimeMethodInfo runtimeMethodInfo = methodInfo as RuntimeMethodInfo;
      if ((MethodInfo) runtimeMethodInfo != (MethodInfo) null)
      {
        RuntimeMethodInfo parentDefinition = runtimeMethodInfo.GetParentDefinition();
        if ((MethodInfo) parentDefinition != (MethodInfo) null)
          return parentDefinition.DeclaringType.GetProperty(property.Name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, property.PropertyType, propertyParameters, (ParameterModifier[]) null);
      }
      return (PropertyInfo) null;
    }

    private static Attribute[] InternalGetCustomAttributes(EventInfo element, Type type, bool inherit)
    {
      Attribute[] customAttributes1 = (Attribute[]) element.GetCustomAttributes(type, inherit);
      if (!inherit)
        return customAttributes1;
      Dictionary<Type, AttributeUsageAttribute> types = new Dictionary<Type, AttributeUsageAttribute>(11);
      List<Attribute> attributeList = new List<Attribute>();
      Attribute.CopyToArrayList(attributeList, customAttributes1, types);
      for (EventInfo parentDefinition = Attribute.GetParentDefinition(element); parentDefinition != (EventInfo) null; parentDefinition = Attribute.GetParentDefinition(parentDefinition))
      {
        Attribute[] customAttributes2 = Attribute.GetCustomAttributes((MemberInfo) parentDefinition, type, false);
        Attribute.AddAttributesToList(attributeList, customAttributes2, types);
      }
      Array attributeArrayHelper = (Array) Attribute.CreateAttributeArrayHelper(type, attributeList.Count);
      Array.Copy((Array) attributeList.ToArray(), 0, attributeArrayHelper, 0, attributeList.Count);
      return (Attribute[]) attributeArrayHelper;
    }

    private static EventInfo GetParentDefinition(EventInfo ev)
    {
      RuntimeMethodInfo addMethod = ev.GetAddMethod(true) as RuntimeMethodInfo;
      if ((MethodInfo) addMethod != (MethodInfo) null)
      {
        RuntimeMethodInfo parentDefinition = addMethod.GetParentDefinition();
        if ((MethodInfo) parentDefinition != (MethodInfo) null)
          return parentDefinition.DeclaringType.GetEvent(ev.Name);
      }
      return (EventInfo) null;
    }

    private static bool InternalIsDefined(EventInfo element, Type attributeType, bool inherit)
    {
      if (element.IsDefined(attributeType, inherit))
        return true;
      if (inherit && Attribute.InternalGetAttributeUsage(attributeType).Inherited)
      {
        for (EventInfo parentDefinition = Attribute.GetParentDefinition(element); parentDefinition != (EventInfo) null; parentDefinition = Attribute.GetParentDefinition(parentDefinition))
        {
          if (parentDefinition.IsDefined(attributeType, false))
            return true;
        }
      }
      return false;
    }

    private static ParameterInfo GetParentDefinition(ParameterInfo param)
    {
      RuntimeMethodInfo member = param.Member as RuntimeMethodInfo;
      if ((MethodInfo) member != (MethodInfo) null)
      {
        RuntimeMethodInfo parentDefinition = member.GetParentDefinition();
        if ((MethodInfo) parentDefinition != (MethodInfo) null)
          return parentDefinition.GetParameters()[param.Position];
      }
      return (ParameterInfo) null;
    }

    private static Attribute[] InternalParamGetCustomAttributes(ParameterInfo param, Type type, bool inherit)
    {
      List<Type> typeList = new List<Type>();
      if (type == (Type) null)
        type = typeof (Attribute);
      object[] customAttributes1 = param.GetCustomAttributes(type, false);
      for (int index = 0; index < customAttributes1.Length; ++index)
      {
        Type type1 = customAttributes1[index].GetType();
        if (!Attribute.InternalGetAttributeUsage(type1).AllowMultiple)
          typeList.Add(type1);
      }
      Attribute[] attributeArray1 = customAttributes1.Length != 0 ? (Attribute[]) customAttributes1 : Attribute.CreateAttributeArrayHelper(type, 0);
      if (param.Member.DeclaringType == (Type) null || !inherit)
        return attributeArray1;
      for (ParameterInfo parentDefinition = Attribute.GetParentDefinition(param); parentDefinition != null; parentDefinition = Attribute.GetParentDefinition(parentDefinition))
      {
        object[] customAttributes2 = parentDefinition.GetCustomAttributes(type, false);
        int elementCount = 0;
        for (int index = 0; index < customAttributes2.Length; ++index)
        {
          Type type1 = customAttributes2[index].GetType();
          AttributeUsageAttribute attributeUsage = Attribute.InternalGetAttributeUsage(type1);
          if (attributeUsage.Inherited && !typeList.Contains(type1))
          {
            if (!attributeUsage.AllowMultiple)
              typeList.Add(type1);
            ++elementCount;
          }
          else
            customAttributes2[index] = (object) null;
        }
        Attribute[] attributeArrayHelper = Attribute.CreateAttributeArrayHelper(type, elementCount);
        int index1 = 0;
        for (int index2 = 0; index2 < customAttributes2.Length; ++index2)
        {
          if (customAttributes2[index2] != null)
          {
            attributeArrayHelper[index1] = (Attribute) customAttributes2[index2];
            ++index1;
          }
        }
        Attribute[] attributeArray2 = attributeArray1;
        attributeArray1 = Attribute.CreateAttributeArrayHelper(type, attributeArray2.Length + index1);
        Array.Copy((Array) attributeArray2, (Array) attributeArray1, attributeArray2.Length);
        int length = attributeArray2.Length;
        for (int index2 = 0; index2 < attributeArrayHelper.Length; ++index2)
          attributeArray1[length + index2] = attributeArrayHelper[index2];
      }
      return attributeArray1;
    }

    private static bool InternalParamIsDefined(ParameterInfo param, Type type, bool inherit)
    {
      if (param.IsDefined(type, false))
        return true;
      if (param.Member.DeclaringType == (Type) null || !inherit)
        return false;
      for (ParameterInfo parentDefinition = Attribute.GetParentDefinition(param); parentDefinition != null; parentDefinition = Attribute.GetParentDefinition(parentDefinition))
      {
        object[] customAttributes = parentDefinition.GetCustomAttributes(type, false);
        for (int index = 0; index < customAttributes.Length; ++index)
        {
          AttributeUsageAttribute attributeUsage = Attribute.InternalGetAttributeUsage(customAttributes[index].GetType());
          if (customAttributes[index] is Attribute && attributeUsage.Inherited)
            return true;
        }
      }
      return false;
    }

    private static void CopyToArrayList(List<Attribute> attributeList, Attribute[] attributes, Dictionary<Type, AttributeUsageAttribute> types)
    {
      for (int index = 0; index < attributes.Length; ++index)
      {
        attributeList.Add(attributes[index]);
        Type type = attributes[index].GetType();
        if (!types.ContainsKey(type))
          types[type] = Attribute.InternalGetAttributeUsage(type);
      }
    }

    private static Type[] GetIndexParameterTypes(PropertyInfo element)
    {
      ParameterInfo[] indexParameters = element.GetIndexParameters();
      if (indexParameters.Length == 0)
        return Array.Empty<Type>();
      Type[] typeArray = new Type[indexParameters.Length];
      for (int index = 0; index < indexParameters.Length; ++index)
        typeArray[index] = indexParameters[index].ParameterType;
      return typeArray;
    }

    private static void AddAttributesToList(List<Attribute> attributeList, Attribute[] attributes, Dictionary<Type, AttributeUsageAttribute> types)
    {
      for (int index = 0; index < attributes.Length; ++index)
      {
        Type type = attributes[index].GetType();
        AttributeUsageAttribute attributeUsageAttribute = (AttributeUsageAttribute) null;
        types.TryGetValue(type, out attributeUsageAttribute);
        if (attributeUsageAttribute == null)
        {
          attributeUsageAttribute = Attribute.InternalGetAttributeUsage(type);
          types[type] = attributeUsageAttribute;
          if (attributeUsageAttribute.Inherited)
            attributeList.Add(attributes[index]);
        }
        else if (attributeUsageAttribute.Inherited && attributeUsageAttribute.AllowMultiple)
          attributeList.Add(attributes[index]);
      }
    }

    private static AttributeUsageAttribute InternalGetAttributeUsage(Type type)
    {
      object[] customAttributes = type.GetCustomAttributes(typeof (AttributeUsageAttribute), false);
      if (customAttributes.Length == 1)
        return (AttributeUsageAttribute) customAttributes[0];
      if (customAttributes.Length == 0)
        return AttributeUsageAttribute.Default;
      throw new FormatException(Environment.GetResourceString("Format_AttributeUsage", (object) type));
    }

    [SecuritySafeCritical]
    private static Attribute[] CreateAttributeArrayHelper(Type elementType, int elementCount)
    {
      return (Attribute[]) Array.UnsafeCreateInstance(elementType, elementCount);
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к члену типа.
    ///    Параметры определяют член и тип настраиваемого атрибута для поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.MemberInfo" /> класс, описывающий членом класса, конструктор, события, поля, метода или свойства.
    /// </param>
    /// <param name="type">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Attribute" /> Массив, содержащий настраиваемые атрибуты типа <paramref name="type" /> применены к <paramref name="element" />, или пустой массив, если такие настраиваемые атрибуты отсутствуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" />не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" />не конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(MemberInfo element, Type type)
    {
      return Attribute.GetCustomAttributes(element, type, true);
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к члену типа.
    ///    Параметры определяют член и тип настраиваемого атрибута для поиска и Признак поиска родительского элемента.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.MemberInfo" /> класс, описывающий членом класса, конструктор, события, поля, метода или свойства.
    /// </param>
    /// <param name="type">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Если <see langword="true" />, задает поиск родительских элементов <paramref name="element" /> для настраиваемых атрибутов.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Attribute" /> Массив, содержащий настраиваемые атрибуты типа <paramref name="type" /> применены к <paramref name="element" />, или пустой массив, если такие настраиваемые атрибуты отсутствуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" />не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" />не конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(MemberInfo element, Type type, bool inherit)
    {
      if (element == (MemberInfo) null)
        throw new ArgumentNullException(nameof (element));
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (!type.IsSubclassOf(typeof (Attribute)) && type != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      switch (element.MemberType)
      {
        case MemberTypes.Event:
          return Attribute.InternalGetCustomAttributes((EventInfo) element, type, inherit);
        case MemberTypes.Property:
          return Attribute.InternalGetCustomAttributes((PropertyInfo) element, type, inherit);
        default:
          return element.GetCustomAttributes(type, inherit) as Attribute[];
      }
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к члену типа.
    ///    Параметр определяет элемент.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.MemberInfo" /> класс, описывающий членом класса, конструктор, события, поля, метода или свойства.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Attribute" />, содержащий настраиваемые атрибуты, примененные к <paramref name="element" />, или пустой массив, если такие настраиваемые атрибуты не существуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" />не конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(MemberInfo element)
    {
      return Attribute.GetCustomAttributes(element, true);
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к члену типа.
    ///    Параметры определяют член и тип настраиваемого атрибута для поиска и Признак поиска родительского элемента.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.MemberInfo" /> класс, описывающий членом класса, конструктор, события, поля, метода или свойства.
    /// </param>
    /// <param name="inherit">
    ///   Если <see langword="true" />, задает поиск родительских элементов <paramref name="element" /> для настраиваемых атрибутов.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Attribute" />, содержащий настраиваемые атрибуты, примененные к <paramref name="element" />, или пустой массив, если такие настраиваемые атрибуты не существуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" />не конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(MemberInfo element, bool inherit)
    {
      if (element == (MemberInfo) null)
        throw new ArgumentNullException(nameof (element));
      switch (element.MemberType)
      {
        case MemberTypes.Event:
          return Attribute.InternalGetCustomAttributes((EventInfo) element, typeof (Attribute), inherit);
        case MemberTypes.Property:
          return Attribute.InternalGetCustomAttributes((PropertyInfo) element, typeof (Attribute), inherit);
        default:
          return element.GetCustomAttributes(typeof (Attribute), inherit) as Attribute[];
      }
    }

    /// <summary>
    ///   Определяет, применены ли какие-либо пользовательские атрибуты для члена типа.
    ///    Параметры определяют член и тип настраиваемого атрибута для поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.MemberInfo" /> класс, описывающий членом класса, конструктор, событие, поле, метод, тип или свойство.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если пользовательский атрибут типа <paramref name="attributeType" /> применяется к <paramref name="element" />; в противном случае — <see langword="false" />.
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
    public static bool IsDefined(MemberInfo element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType, true);
    }

    /// <summary>
    ///   Определяет, применяются ли все настраиваемые атрибуты для члена типа.
    ///    Параметры определяют член и тип настраиваемого атрибута для поиска и Признак поиска родительского элемента.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.MemberInfo" /> класс, описывающий членом класса, конструктор, событие, поле, метод, тип или свойство.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Если <see langword="true" />, задает поиск родительских элементов <paramref name="element" /> для настраиваемых атрибутов.
    /// </param>
    /// <returns>
    ///   <see langword="true" />Если пользовательский атрибут типа <paramref name="attributeType" /> применяется к <paramref name="element" />; в противном случае <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" />не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" />не конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(MemberInfo element, Type attributeType, bool inherit)
    {
      if (element == (MemberInfo) null)
        throw new ArgumentNullException(nameof (element));
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      switch (element.MemberType)
      {
        case MemberTypes.Event:
          return Attribute.InternalIsDefined((EventInfo) element, attributeType, inherit);
        case MemberTypes.Property:
          return Attribute.InternalIsDefined((PropertyInfo) element, attributeType, inherit);
        default:
          return element.IsDefined(attributeType, inherit);
      }
    }

    /// <summary>
    ///   Извлекает настраиваемый атрибут, примененный к члену типа.
    ///    Параметры определяют член и тип настраиваемого атрибута для поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.MemberInfo" /> класс, описывающий членом класса, конструктор, события, поля, метода или свойства.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <returns>
    ///   Ссылку на один пользовательский атрибут типа <paramref name="attributeType" /> , применяемый к <paramref name="element" />, или <see langword="null" /> Если атрибут отсутствует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" />не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" />не конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более чем один из запрошенных атрибутов.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType, true);
    }

    /// <summary>
    ///   Извлекает настраиваемый атрибут, примененный к члену типа.
    ///    Параметры определяют член и тип настраиваемого атрибута для поиска и Признак поиска родительского элемента.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.MemberInfo" /> класс, описывающий членом класса, конструктор, события, поля, метода или свойства.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Если <see langword="true" />, задает поиск родительских элементов <paramref name="element" /> для настраиваемых атрибутов.
    /// </param>
    /// <returns>
    ///   Ссылку на один пользовательский атрибут типа <paramref name="attributeType" /> , применяемый к <paramref name="element" />, или <see langword="null" /> Если атрибут отсутствует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" />не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="element" />не конструктор, метод, свойство, событие, тип или поле.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более чем один из запрошенных атрибутов.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType, bool inherit)
    {
      Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
      if (customAttributes == null || customAttributes.Length == 0)
        return (Attribute) null;
      if (customAttributes.Length == 1)
        return customAttributes[0];
      throw new AmbiguousMatchException(Environment.GetResourceString("RFLCT.AmbigCust"));
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к параметру метода.
    ///    Параметр указывает параметр метода.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.ParameterInfo" /> класс, описывающий параметру члена класса.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Attribute" />, содержащий настраиваемые атрибуты, примененные к <paramref name="element" />, или пустой массив, если такие настраиваемые атрибуты не существуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(ParameterInfo element)
    {
      return Attribute.GetCustomAttributes(element, true);
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к параметру метода.
    ///    Параметры определяют параметр метода и тип настраиваемого атрибута для поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.ParameterInfo" /> класс, описывающий параметру члена класса.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Attribute" /> Массив, содержащий настраиваемые атрибуты типа <paramref name="attributeType" /> применен к <paramref name="element" />, или пустой массив, если таких пользовательских атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(ParameterInfo element, Type attributeType)
    {
      return Attribute.GetCustomAttributes(element, attributeType, true);
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к параметру метода.
    ///    Параметры определяют параметр метода, тип настраиваемого атрибута для поиска и Признак поиска родительского элемента параметра метода.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.ParameterInfo" /> класс, описывающий параметру члена класса.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Если <see langword="true" />, задает поиск родительских элементов <paramref name="element" /> для настраиваемых атрибутов.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Attribute" /> Массив, содержащий настраиваемые атрибуты типа <paramref name="attributeType" /> применен к <paramref name="element" />, или пустой массив, если таких пользовательских атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(ParameterInfo element, Type attributeType, bool inherit)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      if (element.Member == (MemberInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidParameterInfo"), nameof (element));
      if (element.Member.MemberType == MemberTypes.Method & inherit)
        return Attribute.InternalParamGetCustomAttributes(element, attributeType, inherit);
      return element.GetCustomAttributes(attributeType, inherit) as Attribute[];
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к параметру метода.
    ///    Параметры определяют параметр метода и Признак поиска родительского элемента параметра метода.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.ParameterInfo" /> класс, описывающий параметру члена класса.
    /// </param>
    /// <param name="inherit">
    ///   Если <see langword="true" />, задает поиск родительских элементов <paramref name="element" /> для настраиваемых атрибутов.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Attribute" />, содержащий настраиваемые атрибуты, примененные к <paramref name="element" />, или пустой массив, если такие настраиваемые атрибуты не существуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="P:System.Reflection.ParameterInfo.Member" /> Свойство <paramref name="element" /> — <see langword="null." /><see langword="" />
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(ParameterInfo element, bool inherit)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (element.Member == (MemberInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidParameterInfo"), nameof (element));
      if (element.Member.MemberType == MemberTypes.Method & inherit)
        return Attribute.InternalParamGetCustomAttributes(element, (Type) null, inherit);
      return element.GetCustomAttributes(typeof (Attribute), inherit) as Attribute[];
    }

    /// <summary>
    ///   Определяет, применяются ли какие-либо пользовательские атрибуты к параметру метода.
    ///    Параметры определяют параметр метода и тип настраиваемого атрибута для поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.ParameterInfo" /> класс, описывающий параметру члена класса.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если пользовательский атрибут типа <paramref name="attributeType" /> применяется к <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(ParameterInfo element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType, true);
    }

    /// <summary>
    ///   Определяет, применяются ли какие-либо пользовательские атрибуты к параметру метода.
    ///    Параметры определяют параметр метода, тип настраиваемого атрибута для поиска и Признак поиска родительского элемента параметра метода.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.ParameterInfo" /> класс, описывающий параметру члена класса.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Если <see langword="true" />, задает поиск родительских элементов <paramref name="element" /> для настраиваемых атрибутов.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если пользовательский атрибут типа <paramref name="attributeType" /> применяется к <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.ExecutionEngineException">
    ///   <paramref name="element" /> не является метод, конструктор или типа.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(ParameterInfo element, Type attributeType, bool inherit)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      switch (element.Member.MemberType)
      {
        case MemberTypes.Constructor:
          return element.IsDefined(attributeType, false);
        case MemberTypes.Method:
          return Attribute.InternalParamIsDefined(element, attributeType, inherit);
        case MemberTypes.Property:
          return element.IsDefined(attributeType, false);
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidParamInfo"));
      }
    }

    /// <summary>
    ///   Извлекает настраиваемый атрибут, примененный к параметру метода.
    ///    Параметры определяют параметр метода и тип настраиваемого атрибута для поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.ParameterInfo" /> класс, описывающий параметру члена класса.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <returns>
    ///   Ссылку на один пользовательский атрибут типа <paramref name="attributeType" /> , применяемый к <paramref name="element" />, или <see langword="null" /> Если атрибут отсутствует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" />не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более чем один из запрошенных атрибутов.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(ParameterInfo element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType, true);
    }

    /// <summary>
    ///   Извлекает настраиваемый атрибут, примененный к параметру метода.
    ///    Параметры определяют параметр метода, тип настраиваемого атрибута для поиска и следует ли выполнять поиск родительских элементов параметра метода.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.ParameterInfo" /> класс, описывающий параметру члена класса.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Если <see langword="true" />, задает поиск родительских элементов <paramref name="element" /> для настраиваемых атрибутов.
    /// </param>
    /// <returns>
    ///   Ссылку на один пользовательский атрибут типа <paramref name="attributeType" /> , применяемый к <paramref name="element" />, или <see langword="null" /> Если атрибут отсутствует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" />не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более чем один из запрошенных атрибутов.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(ParameterInfo element, Type attributeType, bool inherit)
    {
      Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
      if (customAttributes == null || customAttributes.Length == 0)
        return (Attribute) null;
      if (customAttributes.Length == 0)
        return (Attribute) null;
      if (customAttributes.Length == 1)
        return customAttributes[0];
      throw new AmbiguousMatchException(Environment.GetResourceString("RFLCT.AmbigCust"));
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к модулю.
    ///    Параметры задают модуль и тип настраиваемого атрибута для поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.Module" /> класс, который описывает переносимый исполняемый файл.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Attribute" /> Массив, содержащий настраиваемые атрибуты типа <paramref name="attributeType" /> применен к <paramref name="element" />, или пустой массив, если таких пользовательских атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    public static Attribute[] GetCustomAttributes(Module element, Type attributeType)
    {
      return Attribute.GetCustomAttributes(element, attributeType, true);
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к модулю.
    ///    Параметр задает модуль.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.Module" /> класс, который описывает переносимый исполняемый файл.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Attribute" />, содержащий настраиваемые атрибуты, примененные к <paramref name="element" />, или пустой массив, если такие настраиваемые атрибуты не существуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    public static Attribute[] GetCustomAttributes(Module element)
    {
      return Attribute.GetCustomAttributes(element, true);
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к модулю.
    ///    Параметры задают модуль и игнорируемый параметр поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.Module" /> класс, который описывает переносимый исполняемый файл.
    /// </param>
    /// <param name="inherit">
    ///   Этот параметр игнорируется и не влияет на работу данного метода.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Attribute" />, содержащий настраиваемые атрибуты, примененные к <paramref name="element" />, или пустой массив, если такие настраиваемые атрибуты не существуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    public static Attribute[] GetCustomAttributes(Module element, bool inherit)
    {
      if (element == (Module) null)
        throw new ArgumentNullException(nameof (element));
      return (Attribute[]) element.GetCustomAttributes(typeof (Attribute), inherit);
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к модулю.
    ///    Параметры определяют модуль, тип настраиваемого атрибута для поиска и игнорируемый параметр поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.Module" /> класс, который описывает переносимый исполняемый файл.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Этот параметр игнорируется и не влияет на работу данного метода.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Attribute" /> Массив, содержащий настраиваемые атрибуты типа <paramref name="attributeType" /> применен к <paramref name="element" />, или пустой массив, если таких пользовательских атрибутов не существует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    public static Attribute[] GetCustomAttributes(Module element, Type attributeType, bool inherit)
    {
      if (element == (Module) null)
        throw new ArgumentNullException(nameof (element));
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      return (Attribute[]) element.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>
    ///   Определяет, применяются ли какие-либо пользовательские атрибуты заданного типа к модулю.
    ///    Параметры задают модуль и тип настраиваемого атрибута для поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.Module" /> класс, который описывает переносимый исполняемый файл.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если пользовательский атрибут типа <paramref name="attributeType" /> применяется к <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    public static bool IsDefined(Module element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType, false);
    }

    /// <summary>
    ///   Определяет, применены ли какие-либо пользовательские атрибуты к модулю.
    ///    Параметры определяют модуль, тип настраиваемого атрибута для поиска и игнорируемый параметр поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.Module" /> класс, который описывает переносимый исполняемый файл.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Этот параметр игнорируется и не влияет на работу данного метода.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если пользовательский атрибут типа <paramref name="attributeType" /> применяется к <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    public static bool IsDefined(Module element, Type attributeType, bool inherit)
    {
      if (element == (Module) null)
        throw new ArgumentNullException(nameof (element));
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      return element.IsDefined(attributeType, false);
    }

    /// <summary>
    ///   Извлекает настраиваемый атрибут, примененный к модулю.
    ///    Параметры задают модуль и тип настраиваемого атрибута для поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.Module" /> класс, который описывает переносимый исполняемый файл.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <returns>
    ///   Ссылку на один пользовательский атрибут типа <paramref name="attributeType" /> применяемый к <paramref name="element" />, или <see langword="null" /> Если атрибут отсутствует.
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
    public static Attribute GetCustomAttribute(Module element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType, true);
    }

    /// <summary>
    ///   Извлекает настраиваемый атрибут, примененный к модулю.
    ///    Параметры определяют модуль, тип настраиваемого атрибута для поиска и игнорируемый параметр поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от <see cref="T:System.Reflection.Module" /> класс, который описывает переносимый исполняемый файл.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Этот параметр игнорируется и не влияет на работу данного метода.
    /// </param>
    /// <returns>
    ///   Ссылку на один пользовательский атрибут типа <paramref name="attributeType" /> , применяемый к <paramref name="element" />, или <see langword="null" /> Если атрибут отсутствует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" />не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более чем один из запрошенных атрибутов.
    /// </exception>
    public static Attribute GetCustomAttribute(Module element, Type attributeType, bool inherit)
    {
      Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
      if (customAttributes == null || customAttributes.Length == 0)
        return (Attribute) null;
      if (customAttributes.Length == 1)
        return customAttributes[0];
      throw new AmbiguousMatchException(Environment.GetResourceString("RFLCT.AmbigCust"));
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к сборке.
    ///    Параметры задают сборку и тип настраиваемого атрибута для поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от класса <see cref="T:System.Reflection.Assembly" />, который описывает многократно используемую коллекцию модулей.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Attribute" /> Массив, содержащий настраиваемые атрибуты типа <paramref name="attributeType" /> применены к <paramref name="element" />, или пустой массив, если такие настраиваемые атрибуты отсутствуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" />не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(Assembly element, Type attributeType)
    {
      return Attribute.GetCustomAttributes(element, attributeType, true);
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к сборке.
    ///    Параметры задают сборку, тип настраиваемого атрибута для поиска и игнорируемый параметр поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от класса <see cref="T:System.Reflection.Assembly" />, который описывает многократно используемую коллекцию модулей.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Этот параметр игнорируется и не влияет на работу данного метода.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Attribute" /> Массив, содержащий настраиваемые атрибуты типа <paramref name="attributeType" /> применены к <paramref name="element" />, или пустой массив, если такие настраиваемые атрибуты отсутствуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" />не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    public static Attribute[] GetCustomAttributes(Assembly element, Type attributeType, bool inherit)
    {
      if (element == (Assembly) null)
        throw new ArgumentNullException(nameof (element));
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      return (Attribute[]) element.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к сборке.
    ///    Параметр указывает сборку.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от класса <see cref="T:System.Reflection.Assembly" />, который описывает многократно используемую коллекцию модулей.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Attribute" />, содержащий настраиваемые атрибуты, примененные к <paramref name="element" />, или пустой массив, если такие настраиваемые атрибуты не существуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="element" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute[] GetCustomAttributes(Assembly element)
    {
      return Attribute.GetCustomAttributes(element, true);
    }

    /// <summary>
    ///   Извлекает массив настраиваемых атрибутов, примененных к сборке.
    ///    Параметры задают сборку и игнорируемый параметр поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от класса <see cref="T:System.Reflection.Assembly" />, который описывает многократно используемую коллекцию модулей.
    /// </param>
    /// <param name="inherit">
    ///   Этот параметр игнорируется и не влияет на работу данного метода.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Attribute" />, содержащий настраиваемые атрибуты, примененные к <paramref name="element" />, или пустой массив, если такие настраиваемые атрибуты не существуют.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    public static Attribute[] GetCustomAttributes(Assembly element, bool inherit)
    {
      if (element == (Assembly) null)
        throw new ArgumentNullException(nameof (element));
      return (Attribute[]) element.GetCustomAttributes(typeof (Attribute), inherit);
    }

    /// <summary>
    ///   Определяет, применяются ли все настраиваемые атрибуты для сборки.
    ///    Параметры задают сборку и тип настраиваемого атрибута для поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от класса <see cref="T:System.Reflection.Assembly" />, который описывает многократно используемую коллекцию модулей.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <returns>
    ///   <see langword="true" />Если пользовательский атрибут типа <paramref name="attributeType" /> применяется к <paramref name="element" />; в противном случае <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" />не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool IsDefined(Assembly element, Type attributeType)
    {
      return Attribute.IsDefined(element, attributeType, true);
    }

    /// <summary>
    ///   Определяет, применяются ли какие-либо пользовательские атрибуты для сборки.
    ///    Параметры задают сборку, тип настраиваемого атрибута для поиска и игнорируемый параметр поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от класса <see cref="T:System.Reflection.Assembly" />, который описывает многократно используемую коллекцию модулей.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Этот параметр игнорируется и не влияет на работу данного метода.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если пользовательский атрибут типа <paramref name="attributeType" /> применяется к <paramref name="element" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    public static bool IsDefined(Assembly element, Type attributeType, bool inherit)
    {
      if (element == (Assembly) null)
        throw new ArgumentNullException(nameof (element));
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      if (!attributeType.IsSubclassOf(typeof (Attribute)) && attributeType != typeof (Attribute))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustHaveAttributeBaseClass"));
      return element.IsDefined(attributeType, false);
    }

    /// <summary>
    ///   Извлекает настраиваемый атрибут, примененный для заданной сборки.
    ///    Параметры задают сборку и тип настраиваемого атрибута для поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от класса <see cref="T:System.Reflection.Assembly" />, который описывает многократно используемую коллекцию модулей.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <returns>
    ///   Ссылку на один пользовательский атрибут типа <paramref name="attributeType" /> , применяемый к <paramref name="element" />, или <see langword="null" /> Если атрибут отсутствует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="element" /> или <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" />не является производным от <see cref="T:System.Attribute" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найден более чем один из запрошенных атрибутов.
    /// </exception>
    [__DynamicallyInvokable]
    public static Attribute GetCustomAttribute(Assembly element, Type attributeType)
    {
      return Attribute.GetCustomAttribute(element, attributeType, true);
    }

    /// <summary>
    ///   Извлекает настраиваемый атрибут, примененный к сборке.
    ///    Параметры задают сборку, тип настраиваемого атрибута для поиска и игнорируемый параметр поиска.
    /// </summary>
    /// <param name="element">
    ///   Объект, производный от класса <see cref="T:System.Reflection.Assembly" />, который описывает многократно используемую коллекцию модулей.
    /// </param>
    /// <param name="attributeType">
    ///   Тип или базовый тип настраиваемого атрибута для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Этот параметр игнорируется и не влияет на работу данного метода.
    /// </param>
    /// <returns>
    ///   Ссылку на один пользовательский атрибут типа <paramref name="attributeType" /> применяемый к <paramref name="element" />, или <see langword="null" /> Если атрибут отсутствует.
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
    public static Attribute GetCustomAttribute(Assembly element, Type attributeType, bool inherit)
    {
      Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
      if (customAttributes == null || customAttributes.Length == 0)
        return (Attribute) null;
      if (customAttributes.Length == 1)
        return customAttributes[0];
      throw new AmbiguousMatchException(Environment.GetResourceString("RFLCT.AmbigCust"));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Attribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected Attribute()
    {
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Object" /> Для сравнения с данным экземпляром или <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="obj" /> равно типу и значению данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      RuntimeType type = (RuntimeType) this.GetType();
      if ((RuntimeType) obj.GetType() != type)
        return false;
      object obj1 = (object) this;
      FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      for (int index = 0; index < fields.Length; ++index)
      {
        if (!Attribute.AreFieldValuesEqual(((RtFieldInfo) fields[index]).UnsafeGetValue(obj1), ((RtFieldInfo) fields[index]).UnsafeGetValue(obj)))
          return false;
      }
      return true;
    }

    private static bool AreFieldValuesEqual(object thisValue, object thatValue)
    {
      if (thisValue == null && thatValue == null)
        return true;
      if (thisValue == null || thatValue == null)
        return false;
      if (thisValue.GetType().IsArray)
      {
        if (!thisValue.GetType().Equals(thatValue.GetType()))
          return false;
        Array array1 = thisValue as Array;
        Array array2 = thatValue as Array;
        if (array1.Length != array2.Length)
          return false;
        for (int index = 0; index < array1.Length; ++index)
        {
          if (!Attribute.AreFieldValuesEqual(array1.GetValue(index), array2.GetValue(index)))
            return false;
        }
      }
      else if (!thisValue.Equals(thatValue))
        return false;
      return true;
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      Type type = this.GetType();
      FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      object obj1 = (object) null;
      for (int index = 0; index < fields.Length; ++index)
      {
        object obj2 = ((RtFieldInfo) fields[index]).UnsafeGetValue((object) this);
        if (obj2 != null && !obj2.GetType().IsArray)
          obj1 = obj2;
        if (obj1 != null)
          break;
      }
      if (obj1 != null)
        return obj1.GetHashCode();
      return type.GetHashCode();
    }

    /// <summary>
    ///   В случае реализации в производном классе возвращает уникальный идентификатор для этого атрибута <see cref="T:System.Attribute" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Object" /> , Уникальный идентификатор для атрибута.
    /// </returns>
    public virtual object TypeId
    {
      get
      {
        return (object) this.GetType();
      }
    }

    /// <summary>
    ///   При переопределении в производном классе, возвращает значение, указывающее, равен ли данный экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Object" /> Для сравнения с данным экземпляром <see cref="T:System.Attribute" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />Если этот экземпляр равен <paramref name="obj" />; в противном случае <see langword="false" />.
    /// </returns>
    public virtual bool Match(object obj)
    {
      return this.Equals(obj);
    }

    /// <summary>
    ///   При переопределении в производном классе указывает, является ли значение этого экземпляра значением по умолчанию для производного класса.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если этот экземпляр является атрибутом по умолчанию для класса; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool IsDefaultAttribute()
    {
      return false;
    }

    void _Attribute.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _Attribute.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _Attribute.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _Attribute.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
