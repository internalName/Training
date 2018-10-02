// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.FormatterServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Text;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Предоставляет статические методы для реализации <see cref="T:System.Runtime.Serialization.Formatter" /> для сериализации.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public static class FormatterServices
  {
    internal static ConcurrentDictionary<MemberHolder, MemberInfo[]> m_MemberInfoTable = new ConcurrentDictionary<MemberHolder, MemberInfo[]>();
    [SecurityCritical]
    private static bool unsafeTypeForwardersIsEnabled = false;
    [SecurityCritical]
    private static volatile bool unsafeTypeForwardersIsEnabledInitialized = false;
    private static readonly Type[] advancedTypes = new Type[4]
    {
      typeof (DelegateSerializationHolder),
      typeof (ObjRef),
      typeof (IEnvoyInfo),
      typeof (ISponsor)
    };
    private static Binder s_binder = Type.DefaultBinder;

    [SecuritySafeCritical]
    static FormatterServices()
    {
    }

    private static MemberInfo[] GetSerializableMembers(RuntimeType type)
    {
      FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      int length = 0;
      for (int index = 0; index < fields.Length; ++index)
      {
        if ((fields[index].Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
          ++length;
      }
      if (length == fields.Length)
        return (MemberInfo[]) fields;
      FieldInfo[] fieldInfoArray = new FieldInfo[length];
      int index1 = 0;
      for (int index2 = 0; index2 < fields.Length; ++index2)
      {
        if ((fields[index2].Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
        {
          fieldInfoArray[index1] = fields[index2];
          ++index1;
        }
      }
      return (MemberInfo[]) fieldInfoArray;
    }

    private static bool CheckSerializable(RuntimeType type)
    {
      return type.IsSerializable;
    }

    private static MemberInfo[] InternalGetSerializableMembers(RuntimeType type)
    {
      if (type.IsInterface)
        return new MemberInfo[0];
      if (!FormatterServices.CheckSerializable(type))
        throw new SerializationException(Environment.GetResourceString("Serialization_NonSerType", (object) type.FullName, (object) type.Module.Assembly.FullName));
      MemberInfo[] memberInfoArray1 = FormatterServices.GetSerializableMembers(type);
      RuntimeType baseType = (RuntimeType) type.BaseType;
      if (baseType != (RuntimeType) null && baseType != (RuntimeType) typeof (object))
      {
        RuntimeType[] parentTypes1 = (RuntimeType[]) null;
        int parentTypeCount = 0;
        bool parentTypes2 = FormatterServices.GetParentTypes(baseType, out parentTypes1, out parentTypeCount);
        if (parentTypeCount > 0)
        {
          List<SerializationFieldInfo> serializationFieldInfoList = new List<SerializationFieldInfo>();
          for (int index = 0; index < parentTypeCount; ++index)
          {
            RuntimeType type1 = parentTypes1[index];
            if (!FormatterServices.CheckSerializable(type1))
              throw new SerializationException(Environment.GetResourceString("Serialization_NonSerType", (object) type1.FullName, (object) type1.Module.Assembly.FullName));
            FieldInfo[] fields = type1.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            string namePrefix = parentTypes2 ? type1.Name : type1.FullName;
            foreach (FieldInfo fieldInfo in fields)
            {
              if (!fieldInfo.IsNotSerialized)
                serializationFieldInfoList.Add(new SerializationFieldInfo((RuntimeFieldInfo) fieldInfo, namePrefix));
            }
          }
          if (serializationFieldInfoList != null && serializationFieldInfoList.Count > 0)
          {
            MemberInfo[] memberInfoArray2 = new MemberInfo[serializationFieldInfoList.Count + memberInfoArray1.Length];
            Array.Copy((Array) memberInfoArray1, (Array) memberInfoArray2, memberInfoArray1.Length);
            ((ICollection) serializationFieldInfoList).CopyTo((Array) memberInfoArray2, memberInfoArray1.Length);
            memberInfoArray1 = memberInfoArray2;
          }
        }
      }
      return memberInfoArray1;
    }

    private static bool GetParentTypes(RuntimeType parentType, out RuntimeType[] parentTypes, out int parentTypeCount)
    {
      parentTypes = (RuntimeType[]) null;
      parentTypeCount = 0;
      bool flag = true;
      RuntimeType runtimeType1 = (RuntimeType) typeof (object);
      for (RuntimeType runtimeType2 = parentType; runtimeType2 != runtimeType1; runtimeType2 = (RuntimeType) runtimeType2.BaseType)
      {
        if (!runtimeType2.IsInterface)
        {
          string name1 = runtimeType2.Name;
          for (int index = 0; flag && index < parentTypeCount; ++index)
          {
            string name2 = parentTypes[index].Name;
            if (name2.Length == name1.Length && (int) name2[0] == (int) name1[0] && name1 == name2)
            {
              flag = false;
              break;
            }
          }
          if (parentTypes == null || parentTypeCount == parentTypes.Length)
          {
            RuntimeType[] runtimeTypeArray = new RuntimeType[Math.Max(parentTypeCount * 2, 12)];
            if (parentTypes != null)
              Array.Copy((Array) parentTypes, 0, (Array) runtimeTypeArray, 0, parentTypeCount);
            parentTypes = runtimeTypeArray;
          }
          parentTypes[parentTypeCount++] = runtimeType2;
        }
      }
      return flag;
    }

    /// <summary>
    ///   Возвращает все сериализуемые члены для класса заданного <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="type">Сериализуемый тип.</param>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Reflection.MemberInfo" /> непереходного, нестатических членов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    public static MemberInfo[] GetSerializableMembers(Type type)
    {
      return FormatterServices.GetSerializableMembers(type, new StreamingContext(StreamingContextStates.All));
    }

    /// <summary>
    ///   Возвращает все сериализуемые члены для класса заданного <see cref="T:System.Type" /> и указанные в <see cref="T:System.Runtime.Serialization.StreamingContext" />.
    /// </summary>
    /// <param name="type">Сериализуемый или клонируемый тип.</param>
    /// <param name="context">
    ///   Контекст, в котором происходит сериализация.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Reflection.MemberInfo" /> непереходного, нестатических членов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    public static MemberInfo[] GetSerializableMembers(Type type, StreamingContext context)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      if ((object) (type as RuntimeType) == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", (object) type.ToString()));
      MemberHolder key = new MemberHolder(type, context);
      return FormatterServices.m_MemberInfoTable.GetOrAdd(key, (Func<MemberHolder, MemberInfo[]>) (_ => FormatterServices.InternalGetSerializableMembers((RuntimeType) type)));
    }

    /// <summary>
    ///   Определяет, является ли указанный <see cref="T:System.Type" /> может быть десериализован с <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> свойству присвоено <see langword="Low" />.
    /// </summary>
    /// <param name="t">
    ///   <see cref="T:System.Type" /> Для проверки возможности десериализации.
    /// </param>
    /// <param name="securityLevel">
    ///   Значение свойства <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" />.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   <paramref name="t" /> Параметр является дополнительным типом и не может быть десериализован при <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> свойству <see langword="Low" />.
    /// </exception>
    public static void CheckTypeSecurity(Type t, TypeFilterLevel securityLevel)
    {
      if (securityLevel != TypeFilterLevel.Low)
        return;
      for (int index = 0; index < FormatterServices.advancedTypes.Length; ++index)
      {
        if (FormatterServices.advancedTypes[index].IsAssignableFrom(t))
          throw new SecurityException(Environment.GetResourceString("Serialization_TypeSecurity", (object) FormatterServices.advancedTypes[index].FullName, (object) t.FullName));
      }
    }

    /// <summary>Создает новый экземпляр заданного типа объекта.</summary>
    /// <param name="type">Тип создаваемого объекта.</param>
    /// <returns>Обнуленная объект указанного типа.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    public static object GetUninitializedObject(Type type)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      if ((object) (type as RuntimeType) == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", (object) type.ToString()));
      return FormatterServices.nativeGetUninitializedObject((RuntimeType) type);
    }

    /// <summary>Создает новый экземпляр заданного типа объекта.</summary>
    /// <param name="type">Тип создаваемого объекта.</param>
    /// <returns>Обнуленная объект указанного типа.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <paramref name="type" /> Параметр не является допустимым типом среды выполнения.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    public static object GetSafeUninitializedObject(Type type)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      if ((object) (type as RuntimeType) == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", (object) type.ToString()));
      if ((object) type != (object) typeof (ConstructionCall) && (object) type != (object) typeof (LogicalCallContext))
      {
        if ((object) type != (object) typeof (SynchronizationAttribute))
        {
          try
          {
            return FormatterServices.nativeGetSafeUninitializedObject((RuntimeType) type);
          }
          catch (SecurityException ex)
          {
            throw new SerializationException(Environment.GetResourceString("Serialization_Security", (object) type.FullName), (Exception) ex);
          }
        }
      }
      return FormatterServices.nativeGetUninitializedObject((RuntimeType) type);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object nativeGetSafeUninitializedObject(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object nativeGetUninitializedObject(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool GetEnableUnsafeTypeForwarders();

    [SecuritySafeCritical]
    internal static bool UnsafeTypeForwardersIsEnabled()
    {
      if (!FormatterServices.unsafeTypeForwardersIsEnabledInitialized)
      {
        FormatterServices.unsafeTypeForwardersIsEnabled = FormatterServices.GetEnableUnsafeTypeForwarders();
        FormatterServices.unsafeTypeForwardersIsEnabledInitialized = true;
      }
      return FormatterServices.unsafeTypeForwardersIsEnabled;
    }

    [SecurityCritical]
    internal static void SerializationSetValue(MemberInfo fi, object target, object value)
    {
      RtFieldInfo rtFieldInfo = fi as RtFieldInfo;
      if ((FieldInfo) rtFieldInfo != (FieldInfo) null)
      {
        rtFieldInfo.CheckConsistency(target);
        rtFieldInfo.UnsafeSetValue(target, value, BindingFlags.Default, FormatterServices.s_binder, (CultureInfo) null);
      }
      else
      {
        SerializationFieldInfo serializationFieldInfo = fi as SerializationFieldInfo;
        if (!((FieldInfo) serializationFieldInfo != (FieldInfo) null))
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFieldInfo"));
        serializationFieldInfo.InternalSetValue(target, value, BindingFlags.Default, FormatterServices.s_binder, (CultureInfo) null);
      }
    }

    /// <summary>
    ///   Заполняет указанный объект значения для каждого поля, скопированного из массива данных объектов.
    /// </summary>
    /// <param name="obj">Объект для заполнения.</param>
    /// <param name="members">
    ///   Массив <see cref="T:System.Reflection.MemberInfo" /> описывающий, какие поля и свойства для заполнения.
    /// </param>
    /// <param name="data">
    ///   Массив <see cref="T:System.Object" /> задающий значения для каждого поля и свойства для заполнения.
    /// </param>
    /// <returns>Вновь заполненный объект.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" />, <paramref name="members" /> или <paramref name="data" /> имеет значение <see langword="null" />.
    /// 
    ///   Элемент <paramref name="members" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина <paramref name="members" /> не соответствует длине из <paramref name="data" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент <paramref name="members" /> не является экземпляром <see cref="T:System.Reflection.FieldInfo" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    public static object PopulateObjectMembers(object obj, MemberInfo[] members, object[] data)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (members == null)
        throw new ArgumentNullException(nameof (members));
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      if (members.Length != data.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_DataLengthDifferent"));
      for (int index = 0; index < members.Length; ++index)
      {
        MemberInfo member = members[index];
        if (member == (MemberInfo) null)
          throw new ArgumentNullException(nameof (members), Environment.GetResourceString("ArgumentNull_NullMember", (object) index));
        if (data[index] != null)
        {
          if (member.MemberType != MemberTypes.Field)
            throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMemberInfo"));
          FormatterServices.SerializationSetValue(member, obj, data[index]);
        }
      }
      return obj;
    }

    /// <summary>
    ///   Извлекает данные из указанного объекта и возвращает его в виде массива объектов.
    /// </summary>
    /// <param name="obj">
    ///   Объект для записи в модуль форматирования.
    /// </param>
    /// <param name="members">Члены, извлекаемые из объекта.</param>
    /// <returns>
    ///   Массив <see cref="T:System.Object" /> содержащий данные, хранящиеся в <paramref name="members" /> и связанных с <paramref name="obj" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="obj" /> или параметра <paramref name="members" /> — <see langword="null" />.
    /// 
    ///   Элемент <paramref name="members" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Элемент <paramref name="members" /> представляет поле.
    /// </exception>
    [SecurityCritical]
    public static object[] GetObjectData(object obj, MemberInfo[] members)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (members == null)
        throw new ArgumentNullException(nameof (members));
      int length = members.Length;
      object[] objArray = new object[length];
      for (int index = 0; index < length; ++index)
      {
        MemberInfo member = members[index];
        if (member == (MemberInfo) null)
          throw new ArgumentNullException(nameof (members), Environment.GetResourceString("ArgumentNull_NullMember", (object) index));
        if (member.MemberType != MemberTypes.Field)
          throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMemberInfo"));
        RtFieldInfo rtFieldInfo = member as RtFieldInfo;
        if ((FieldInfo) rtFieldInfo != (FieldInfo) null)
        {
          rtFieldInfo.CheckConsistency(obj);
          objArray[index] = rtFieldInfo.UnsafeGetValue(obj);
        }
        else
          objArray[index] = ((SerializationFieldInfo) member).InternalGetValue(obj);
      }
      return objArray;
    }

    /// <summary>
    ///   Возвращает суррогат сериализации для указанного <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" />.
    /// </summary>
    /// <param name="innerSurrogate">Заданный суррогат.</param>
    /// <returns>
    ///   <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" /> Для указанного <paramref name="innerSurrogate" />.
    /// </returns>
    [SecurityCritical]
    [ComVisible(false)]
    public static ISerializationSurrogate GetSurrogateForCyclicalReference(ISerializationSurrogate innerSurrogate)
    {
      if (innerSurrogate == null)
        throw new ArgumentNullException(nameof (innerSurrogate));
      return (ISerializationSurrogate) new SurrogateForCyclicalReference(innerSurrogate);
    }

    /// <summary>
    ///   Ищет <see cref="T:System.Type" /> заданного объекта в предоставленном <see cref="T:System.Reflection.Assembly" />.
    /// </summary>
    /// <param name="assem">Сборка, где вы хотите найти объект.</param>
    /// <param name="name">Имя объекта.</param>
    /// <returns>
    ///   <see cref="T:System.Type" /> Именованного объекта.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assem" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    public static Type GetTypeFromAssembly(Assembly assem, string name)
    {
      if (assem == (Assembly) null)
        throw new ArgumentNullException(nameof (assem));
      return assem.GetType(name, false, false);
    }

    internal static Assembly LoadAssemblyFromString(string assemblyName)
    {
      return Assembly.Load(assemblyName);
    }

    internal static Assembly LoadAssemblyFromStringNoThrow(string assemblyName)
    {
      try
      {
        return FormatterServices.LoadAssemblyFromString(assemblyName);
      }
      catch (Exception ex)
      {
      }
      return (Assembly) null;
    }

    internal static string GetClrAssemblyName(Type type, out bool hasTypeForwardedFrom)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      object[] customAttributes = type.GetCustomAttributes(typeof (TypeForwardedFromAttribute), false);
      if (customAttributes != null && customAttributes.Length != 0)
      {
        hasTypeForwardedFrom = true;
        return ((TypeForwardedFromAttribute) customAttributes[0]).AssemblyFullName;
      }
      hasTypeForwardedFrom = false;
      return type.Assembly.FullName;
    }

    internal static string GetClrTypeFullName(Type type)
    {
      if (type.IsArray)
        return FormatterServices.GetClrTypeFullNameForArray(type);
      return FormatterServices.GetClrTypeFullNameForNonArrayTypes(type);
    }

    private static string GetClrTypeFullNameForArray(Type type)
    {
      int arrayRank = type.GetArrayRank();
      if (arrayRank == 1)
        return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}{1}", (object) FormatterServices.GetClrTypeFullName(type.GetElementType()), (object) "[]");
      StringBuilder stringBuilder = new StringBuilder(FormatterServices.GetClrTypeFullName(type.GetElementType())).Append("[");
      for (int index = 1; index < arrayRank; ++index)
        stringBuilder.Append(",");
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    private static string GetClrTypeFullNameForNonArrayTypes(Type type)
    {
      if (!type.IsGenericType)
        return type.FullName;
      Type[] genericArguments = type.GetGenericArguments();
      StringBuilder stringBuilder = new StringBuilder(type.GetGenericTypeDefinition().FullName).Append("[");
      foreach (Type type1 in genericArguments)
      {
        stringBuilder.Append("[").Append(FormatterServices.GetClrTypeFullName(type1)).Append(", ");
        bool hasTypeForwardedFrom;
        stringBuilder.Append(FormatterServices.GetClrAssemblyName(type1, out hasTypeForwardedFrom)).Append("],");
      }
      return stringBuilder.Remove(stringBuilder.Length - 1, 1).Append("]").ToString();
    }
  }
}
