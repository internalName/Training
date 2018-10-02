// Decompiled with JetBrains decompiler
// Type: System.Reflection.TypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Представляет объявления типов для классов, интерфейсов, массивов, значений, перечислений, параметров, определений универсальных типов и открытых или закрытых сконструированных универсальных типов.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class TypeInfo : Type, IReflectableType
  {
    [FriendAccessAllowed]
    internal TypeInfo()
    {
    }

    [__DynamicallyInvokable]
    TypeInfo IReflectableType.GetTypeInfo()
    {
      return this;
    }

    /// <summary>
    ///   Возвращает текущий тип в виде объекта <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>Текущий тип.</returns>
    [__DynamicallyInvokable]
    public virtual Type AsType()
    {
      return (Type) this;
    }

    /// <summary>
    ///   Возвращает массив параметров универсального типа для текущего экземпляра.
    /// </summary>
    /// <returns>
    ///   Массив, содержащий параметры текущего экземпляра универсального типа или массив <see cref="P:System.Array.Length" /> 0, если текущий экземпляр не имеет параметры универсального типа.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Type[] GenericTypeParameters
    {
      [__DynamicallyInvokable] get
      {
        if (this.IsGenericTypeDefinition)
          return this.GetGenericArguments();
        return Type.EmptyTypes;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, можно ли назначить указанный тип текущему типу.
    /// </summary>
    /// <param name="typeInfo">Проверяемый тип.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный тип может быть присвоен этому типу; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsAssignableFrom(TypeInfo typeInfo)
    {
      if ((Type) typeInfo == (Type) null)
        return false;
      if ((Type) this == (Type) typeInfo || typeInfo.IsSubclassOf((Type) this))
        return true;
      if (this.IsInterface)
        return typeInfo.ImplementInterface((Type) this);
      if (!this.IsGenericParameter)
        return false;
      foreach (Type parameterConstraint in this.GetGenericParameterConstraints())
      {
        if (!parameterConstraint.IsAssignableFrom((Type) typeInfo))
          return false;
      }
      return true;
    }

    /// <summary>
    ///   Возвращает объект, представляющий указанное открытое событие, объявленное текущим типом.
    /// </summary>
    /// <param name="name">Имя события.</param>
    /// <returns>
    ///   Объект, представляющий указанное событие, если оно найдено; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual EventInfo GetDeclaredEvent(string name)
    {
      return this.GetEvent(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>
    ///   Возвращает объект, представляющий указанное открытое поле, объявленное текущим типом.
    /// </summary>
    /// <param name="name">Имя поля.</param>
    /// <returns>
    ///   Объект, представляющий указанное поле, если оно найдено; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual FieldInfo GetDeclaredField(string name)
    {
      return this.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>
    ///   Возвращает объект, представляющий указанный открытый метод, объявленный текущим типом.
    /// </summary>
    /// <param name="name">Имя метода.</param>
    /// <returns>
    ///   Объект, представляющий указанный метод, если такой метод есть; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual MethodInfo GetDeclaredMethod(string name)
    {
      return this.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>
    ///   Возвращает коллекцию, содержащую все открытые методы, объявленные в текущем типе, которые соответствуют заданному имени.
    /// </summary>
    /// <param name="name">Имя метода для поиска.</param>
    /// <returns>
    ///   Коллекция, содержащая методы, соответствующие <paramref name="name" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual IEnumerable<MethodInfo> GetDeclaredMethods(string name)
    {
      MethodInfo[] methodInfoArray = this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      for (int index = 0; index < methodInfoArray.Length; ++index)
      {
        MethodInfo methodInfo = methodInfoArray[index];
        if (methodInfo.Name == name)
          yield return methodInfo;
      }
      methodInfoArray = (MethodInfo[]) null;
    }

    /// <summary>
    ///   Возвращает объект, представляющий указанный открытый вложенный тип, объявленный текущим типом.
    /// </summary>
    /// <param name="name">Имя вложенного типа.</param>
    /// <returns>
    ///   Объект, представляющий указанный вложенный тип, если он найдено; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual TypeInfo GetDeclaredNestedType(string name)
    {
      Type nestedType = this.GetNestedType(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      if (nestedType == (Type) null)
        return (TypeInfo) null;
      return nestedType.GetTypeInfo();
    }

    /// <summary>
    ///   Возвращает объект, представляющий указанное открытое свойство, объявленное текущим типом.
    /// </summary>
    /// <param name="name">Имя свойства.</param>
    /// <returns>
    ///   Объект, представляющий указанное свойство, если оно найдено; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual PropertyInfo GetDeclaredProperty(string name)
    {
      return this.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>
    ///   Возвращает коллекцию конструкторов, объявленных текущим типом.
    /// </summary>
    /// <returns>Коллекция конструкторов, объявленных текущим типом.</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<ConstructorInfo> DeclaredConstructors
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<ConstructorInfo>) this.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
    }

    /// <summary>
    ///   Возвращает коллекцию событий, определенных текущим типом.
    /// </summary>
    /// <returns>Коллекция событий, определенных текущим типом.</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<EventInfo> DeclaredEvents
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<EventInfo>) this.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
    }

    /// <summary>
    ///   Возвращает коллекцию полей, определенных текущим типом.
    /// </summary>
    /// <returns>Коллекция полей, определенных текущим типом.</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<FieldInfo> DeclaredFields
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<FieldInfo>) this.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
    }

    /// <summary>
    ///   Возвращает коллекцию членов, определенных текущим типом.
    /// </summary>
    /// <returns>Коллекция членов, определенных текущим типом.</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<MemberInfo> DeclaredMembers
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<MemberInfo>) this.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
    }

    /// <summary>
    ///   Возвращает коллекцию методов, определенных текущим типом.
    /// </summary>
    /// <returns>Коллекция методов, определенных текущим типом.</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<MethodInfo> DeclaredMethods
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<MethodInfo>) this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
    }

    /// <summary>
    ///   Возвращает коллекцию вложенных типов, определенных текущим типом.
    /// </summary>
    /// <returns>
    ///   Коллекция вложенных типов, определенных текущим типом.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<TypeInfo> DeclaredNestedTypes
    {
      [__DynamicallyInvokable] get
      {
        Type[] typeArray = this.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        for (int index = 0; index < typeArray.Length; ++index)
          yield return typeArray[index].GetTypeInfo();
        typeArray = (Type[]) null;
      }
    }

    /// <summary>
    ///   Возвращает коллекцию свойств, определенных текущим типом.
    /// </summary>
    /// <returns>Коллекция свойств, определенных текущим типом.</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<PropertyInfo> DeclaredProperties
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<PropertyInfo>) this.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
    }

    /// <summary>
    ///   Возвращает коллекцию интерфейсов, реализованных текущим типом.
    /// </summary>
    /// <returns>Коллекция интерфейсов, реализованных текущим типом.</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<Type> ImplementedInterfaces
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<Type>) this.GetInterfaces();
      }
    }
  }
}
