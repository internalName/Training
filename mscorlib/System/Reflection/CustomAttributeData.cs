// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
  /// <summary>
  ///   Предоставляет доступ к данным настраиваемых атрибутов для сборок, модулей, типов, членов и параметров, загруженных в контекст, предназначенный только для отражения.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class CustomAttributeData
  {
    private ConstructorInfo m_ctor;
    private RuntimeModule m_scope;
    private MemberInfo[] m_members;
    private CustomAttributeCtorParameter[] m_ctorParams;
    private CustomAttributeNamedParameter[] m_namedParams;
    private IList<CustomAttributeTypedArgument> m_typedCtorArgs;
    private IList<CustomAttributeNamedArgument> m_namedArgs;

    /// <summary>
    ///   Возвращает список объектов <see cref="T:System.Reflection.CustomAttributeData" />, представляющих данные об атрибутах, примененных к целевому элементу.
    /// </summary>
    /// <param name="target">
    ///   Элемент, данные об атрибутах которого требуется извлечь.
    /// </param>
    /// <returns>
    ///   Список объектов, представляющих данные об атрибутах, примененных к целевому элементу.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="target" /> имеет значение <see langword="null" />.
    /// </exception>
    public static IList<CustomAttributeData> GetCustomAttributes(MemberInfo target)
    {
      if (target == (MemberInfo) null)
        throw new ArgumentNullException(nameof (target));
      return target.GetCustomAttributesData();
    }

    /// <summary>
    ///   Возвращает список объектов <see cref="T:System.Reflection.CustomAttributeData" />, представляющих данные об атрибутах, примененных к целевому модулю.
    /// </summary>
    /// <param name="target">
    ///   Модуль, данные настраиваемого атрибута которого требуется извлечь.
    /// </param>
    /// <returns>
    ///   Список объектов, представляющих данные об атрибутах, примененных к целевому модулю.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="target" /> имеет значение <see langword="null" />.
    /// </exception>
    public static IList<CustomAttributeData> GetCustomAttributes(Module target)
    {
      if (target == (Module) null)
        throw new ArgumentNullException(nameof (target));
      return target.GetCustomAttributesData();
    }

    /// <summary>
    ///   Возвращает список объектов <see cref="T:System.Reflection.CustomAttributeData" />, представляющих данные об атрибутах, примененных к целевой сборке.
    /// </summary>
    /// <param name="target">
    ///   Сборка, данные настраиваемого атрибута которой требуется извлечь.
    /// </param>
    /// <returns>
    ///   Список объектов, представляющих данные об атрибутах, примененных к целевой сборке.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="target" /> имеет значение <see langword="null" />.
    /// </exception>
    public static IList<CustomAttributeData> GetCustomAttributes(Assembly target)
    {
      if (target == (Assembly) null)
        throw new ArgumentNullException(nameof (target));
      return target.GetCustomAttributesData();
    }

    /// <summary>
    ///   Возвращает список объектов <see cref="T:System.Reflection.CustomAttributeData" />, представляющих данные об атрибутах, примененных к целевому параметру.
    /// </summary>
    /// <param name="target">
    ///   Параметр, данные об атрибутах которого требуется извлечь.
    /// </param>
    /// <returns>
    ///   Список объектов, представляющих данные об атрибутах, примененных к целевому параметру.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="target" /> имеет значение <see langword="null" />.
    /// </exception>
    public static IList<CustomAttributeData> GetCustomAttributes(ParameterInfo target)
    {
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      return target.GetCustomAttributesData();
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeType target)
    {
      IList<CustomAttributeData> customAttributes1 = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
      int count = 0;
      Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof (object) as RuntimeType, true, out count);
      if (count == 0)
        return customAttributes1;
      CustomAttributeData[] array = new CustomAttributeData[customAttributes1.Count + count];
      customAttributes1.CopyTo(array, count);
      for (int index = 0; index < count; ++index)
        array[index] = new CustomAttributeData(customAttributes2[index]);
      return (IList<CustomAttributeData>) Array.AsReadOnly<CustomAttributeData>(array);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeFieldInfo target)
    {
      IList<CustomAttributeData> customAttributes1 = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
      int count = 0;
      Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof (object) as RuntimeType, out count);
      if (count == 0)
        return customAttributes1;
      CustomAttributeData[] array = new CustomAttributeData[customAttributes1.Count + count];
      customAttributes1.CopyTo(array, count);
      for (int index = 0; index < count; ++index)
        array[index] = new CustomAttributeData(customAttributes2[index]);
      return (IList<CustomAttributeData>) Array.AsReadOnly<CustomAttributeData>(array);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeMethodInfo target)
    {
      IList<CustomAttributeData> customAttributes1 = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
      int count = 0;
      Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof (object) as RuntimeType, true, out count);
      if (count == 0)
        return customAttributes1;
      CustomAttributeData[] array = new CustomAttributeData[customAttributes1.Count + count];
      customAttributes1.CopyTo(array, count);
      for (int index = 0; index < count; ++index)
        array[index] = new CustomAttributeData(customAttributes2[index]);
      return (IList<CustomAttributeData>) Array.AsReadOnly<CustomAttributeData>(array);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeConstructorInfo target)
    {
      return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeEventInfo target)
    {
      return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimePropertyInfo target)
    {
      return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeModule target)
    {
      if (target.IsResource())
        return (IList<CustomAttributeData>) new List<CustomAttributeData>();
      return CustomAttributeData.GetCustomAttributes(target, target.MetadataToken);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeAssembly target)
    {
      IList<CustomAttributeData> customAttributes1 = CustomAttributeData.GetCustomAttributes((RuntimeModule) target.ManifestModule, RuntimeAssembly.GetToken(target.GetNativeHandle()));
      int count = 0;
      Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof (object) as RuntimeType, false, out count);
      if (count == 0)
        return customAttributes1;
      CustomAttributeData[] array = new CustomAttributeData[customAttributes1.Count + count];
      customAttributes1.CopyTo(array, count);
      for (int index = 0; index < count; ++index)
        array[index] = new CustomAttributeData(customAttributes2[index]);
      return (IList<CustomAttributeData>) Array.AsReadOnly<CustomAttributeData>(array);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeParameterInfo target)
    {
      IList<CustomAttributeData> customAttributes1 = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
      int count = 0;
      Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof (object) as RuntimeType, out count);
      if (count == 0)
        return customAttributes1;
      CustomAttributeData[] array = new CustomAttributeData[customAttributes1.Count + count];
      customAttributes1.CopyTo(array, count);
      for (int index = 0; index < count; ++index)
        array[index] = new CustomAttributeData(customAttributes2[index]);
      return (IList<CustomAttributeData>) Array.AsReadOnly<CustomAttributeData>(array);
    }

    private static CustomAttributeEncoding TypeToCustomAttributeEncoding(RuntimeType type)
    {
      if (type == (RuntimeType) typeof (int))
        return CustomAttributeEncoding.Int32;
      if (type.IsEnum)
        return CustomAttributeEncoding.Enum;
      if (type == (RuntimeType) typeof (string))
        return CustomAttributeEncoding.String;
      if (type == (RuntimeType) typeof (Type))
        return CustomAttributeEncoding.Type;
      if (type == (RuntimeType) typeof (object))
        return CustomAttributeEncoding.Object;
      if (type.IsArray)
        return CustomAttributeEncoding.Array;
      if (type == (RuntimeType) typeof (char))
        return CustomAttributeEncoding.Char;
      if (type == (RuntimeType) typeof (bool))
        return CustomAttributeEncoding.Boolean;
      if (type == (RuntimeType) typeof (byte))
        return CustomAttributeEncoding.Byte;
      if (type == (RuntimeType) typeof (sbyte))
        return CustomAttributeEncoding.SByte;
      if (type == (RuntimeType) typeof (short))
        return CustomAttributeEncoding.Int16;
      if (type == (RuntimeType) typeof (ushort))
        return CustomAttributeEncoding.UInt16;
      if (type == (RuntimeType) typeof (uint))
        return CustomAttributeEncoding.UInt32;
      if (type == (RuntimeType) typeof (long))
        return CustomAttributeEncoding.Int64;
      if (type == (RuntimeType) typeof (ulong))
        return CustomAttributeEncoding.UInt64;
      if (type == (RuntimeType) typeof (float))
        return CustomAttributeEncoding.Float;
      if (type == (RuntimeType) typeof (double))
        return CustomAttributeEncoding.Double;
      if (type == (RuntimeType) typeof (Enum) || type.IsClass || type.IsInterface)
        return CustomAttributeEncoding.Object;
      if (type.IsValueType)
        return CustomAttributeEncoding.Undefined;
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKindOfTypeForCA"), nameof (type));
    }

    private static CustomAttributeType InitCustomAttributeType(RuntimeType parameterType)
    {
      CustomAttributeEncoding attributeEncoding = CustomAttributeData.TypeToCustomAttributeEncoding(parameterType);
      CustomAttributeEncoding encodedArrayType = CustomAttributeEncoding.Undefined;
      CustomAttributeEncoding encodedEnumType = CustomAttributeEncoding.Undefined;
      string enumName = (string) null;
      if (attributeEncoding == CustomAttributeEncoding.Array)
      {
        parameterType = (RuntimeType) parameterType.GetElementType();
        encodedArrayType = CustomAttributeData.TypeToCustomAttributeEncoding(parameterType);
      }
      if (attributeEncoding == CustomAttributeEncoding.Enum || encodedArrayType == CustomAttributeEncoding.Enum)
      {
        encodedEnumType = CustomAttributeData.TypeToCustomAttributeEncoding((RuntimeType) Enum.GetUnderlyingType((Type) parameterType));
        enumName = parameterType.AssemblyQualifiedName;
      }
      return new CustomAttributeType(attributeEncoding, encodedArrayType, encodedEnumType, enumName);
    }

    [SecurityCritical]
    private static IList<CustomAttributeData> GetCustomAttributes(RuntimeModule module, int tkTarget)
    {
      CustomAttributeRecord[] attributeRecords = CustomAttributeData.GetCustomAttributeRecords(module, tkTarget);
      CustomAttributeData[] array = new CustomAttributeData[attributeRecords.Length];
      for (int index = 0; index < attributeRecords.Length; ++index)
        array[index] = new CustomAttributeData(module, attributeRecords[index]);
      return (IList<CustomAttributeData>) Array.AsReadOnly<CustomAttributeData>(array);
    }

    [SecurityCritical]
    internal static CustomAttributeRecord[] GetCustomAttributeRecords(RuntimeModule module, int targetToken)
    {
      MetadataImport metadataImport = module.MetadataImport;
      MetadataEnumResult result;
      metadataImport.EnumCustomAttributes(targetToken, out result);
      CustomAttributeRecord[] customAttributeRecordArray = new CustomAttributeRecord[result.Length];
      for (int index = 0; index < customAttributeRecordArray.Length; ++index)
        metadataImport.GetCustomAttributeProps(result[index], out customAttributeRecordArray[index].tkCtor.Value, out customAttributeRecordArray[index].blob);
      return customAttributeRecordArray;
    }

    internal static CustomAttributeTypedArgument Filter(IList<CustomAttributeData> attrs, Type caType, int parameter)
    {
      for (int index = 0; index < attrs.Count; ++index)
      {
        if (attrs[index].Constructor.DeclaringType == caType)
          return attrs[index].ConstructorArguments[parameter];
      }
      return new CustomAttributeTypedArgument();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.CustomAttributeData" />.
    /// </summary>
    protected CustomAttributeData()
    {
    }

    [SecuritySafeCritical]
    private CustomAttributeData(RuntimeModule scope, CustomAttributeRecord caRecord)
    {
      this.m_scope = scope;
      this.m_ctor = (ConstructorInfo) RuntimeType.GetMethodBase(scope, (int) caRecord.tkCtor);
      ParameterInfo[] parametersNoCopy = this.m_ctor.GetParametersNoCopy();
      this.m_ctorParams = new CustomAttributeCtorParameter[parametersNoCopy.Length];
      for (int index = 0; index < parametersNoCopy.Length; ++index)
        this.m_ctorParams[index] = new CustomAttributeCtorParameter(CustomAttributeData.InitCustomAttributeType((RuntimeType) parametersNoCopy[index].ParameterType));
      FieldInfo[] fields = this.m_ctor.DeclaringType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      PropertyInfo[] properties = this.m_ctor.DeclaringType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      this.m_namedParams = new CustomAttributeNamedParameter[properties.Length + fields.Length];
      for (int index = 0; index < fields.Length; ++index)
        this.m_namedParams[index] = new CustomAttributeNamedParameter(fields[index].Name, CustomAttributeEncoding.Field, CustomAttributeData.InitCustomAttributeType((RuntimeType) fields[index].FieldType));
      for (int index = 0; index < properties.Length; ++index)
        this.m_namedParams[index + fields.Length] = new CustomAttributeNamedParameter(properties[index].Name, CustomAttributeEncoding.Property, CustomAttributeData.InitCustomAttributeType((RuntimeType) properties[index].PropertyType));
      this.m_members = new MemberInfo[fields.Length + properties.Length];
      fields.CopyTo((Array) this.m_members, 0);
      properties.CopyTo((Array) this.m_members, fields.Length);
      CustomAttributeEncodedArgument.ParseAttributeArguments(caRecord.blob, ref this.m_ctorParams, ref this.m_namedParams, this.m_scope);
    }

    internal CustomAttributeData(Attribute attribute)
    {
      if (attribute is DllImportAttribute)
        this.Init((DllImportAttribute) attribute);
      else if (attribute is FieldOffsetAttribute)
        this.Init((FieldOffsetAttribute) attribute);
      else if (attribute is MarshalAsAttribute)
        this.Init((MarshalAsAttribute) attribute);
      else if (attribute is TypeForwardedToAttribute)
        this.Init((TypeForwardedToAttribute) attribute);
      else
        this.Init((object) attribute);
    }

    private void Init(DllImportAttribute dllImport)
    {
      Type type = typeof (DllImportAttribute);
      this.m_ctor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
      this.m_typedCtorArgs = (IList<CustomAttributeTypedArgument>) Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[1]
      {
        new CustomAttributeTypedArgument((object) dllImport.Value)
      });
      this.m_namedArgs = (IList<CustomAttributeNamedArgument>) Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[8]
      {
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("EntryPoint"), (object) dllImport.EntryPoint),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("CharSet"), (object) dllImport.CharSet),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("ExactSpelling"), (object) dllImport.ExactSpelling),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("SetLastError"), (object) dllImport.SetLastError),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("PreserveSig"), (object) dllImport.PreserveSig),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("CallingConvention"), (object) dllImport.CallingConvention),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("BestFitMapping"), (object) dllImport.BestFitMapping),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("ThrowOnUnmappableChar"), (object) dllImport.ThrowOnUnmappableChar)
      });
    }

    private void Init(FieldOffsetAttribute fieldOffset)
    {
      this.m_ctor = typeof (FieldOffsetAttribute).GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
      this.m_typedCtorArgs = (IList<CustomAttributeTypedArgument>) Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[1]
      {
        new CustomAttributeTypedArgument((object) fieldOffset.Value)
      });
      this.m_namedArgs = (IList<CustomAttributeNamedArgument>) Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[0]);
    }

    private void Init(MarshalAsAttribute marshalAs)
    {
      Type type = typeof (MarshalAsAttribute);
      this.m_ctor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
      this.m_typedCtorArgs = (IList<CustomAttributeTypedArgument>) Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[1]
      {
        new CustomAttributeTypedArgument((object) marshalAs.Value)
      });
      int num1 = 3;
      if (marshalAs.MarshalType != null)
        ++num1;
      if (marshalAs.MarshalTypeRef != (Type) null)
        ++num1;
      if (marshalAs.MarshalCookie != null)
        ++num1;
      int length = num1 + 1 + 1;
      if (marshalAs.SafeArrayUserDefinedSubType != (Type) null)
        ++length;
      CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[length];
      int num2 = 0;
      CustomAttributeNamedArgument[] attributeNamedArgumentArray1 = array;
      int index1 = num2;
      int num3 = index1 + 1;
      CustomAttributeNamedArgument attributeNamedArgument1 = new CustomAttributeNamedArgument((MemberInfo) type.GetField("ArraySubType"), (object) marshalAs.ArraySubType);
      attributeNamedArgumentArray1[index1] = attributeNamedArgument1;
      CustomAttributeNamedArgument[] attributeNamedArgumentArray2 = array;
      int index2 = num3;
      int num4 = index2 + 1;
      CustomAttributeNamedArgument attributeNamedArgument2 = new CustomAttributeNamedArgument((MemberInfo) type.GetField("SizeParamIndex"), (object) marshalAs.SizeParamIndex);
      attributeNamedArgumentArray2[index2] = attributeNamedArgument2;
      CustomAttributeNamedArgument[] attributeNamedArgumentArray3 = array;
      int index3 = num4;
      int num5 = index3 + 1;
      CustomAttributeNamedArgument attributeNamedArgument3 = new CustomAttributeNamedArgument((MemberInfo) type.GetField("SizeConst"), (object) marshalAs.SizeConst);
      attributeNamedArgumentArray3[index3] = attributeNamedArgument3;
      CustomAttributeNamedArgument[] attributeNamedArgumentArray4 = array;
      int index4 = num5;
      int num6 = index4 + 1;
      CustomAttributeNamedArgument attributeNamedArgument4 = new CustomAttributeNamedArgument((MemberInfo) type.GetField("IidParameterIndex"), (object) marshalAs.IidParameterIndex);
      attributeNamedArgumentArray4[index4] = attributeNamedArgument4;
      CustomAttributeNamedArgument[] attributeNamedArgumentArray5 = array;
      int index5 = num6;
      int num7 = index5 + 1;
      CustomAttributeNamedArgument attributeNamedArgument5 = new CustomAttributeNamedArgument((MemberInfo) type.GetField("SafeArraySubType"), (object) marshalAs.SafeArraySubType);
      attributeNamedArgumentArray5[index5] = attributeNamedArgument5;
      if (marshalAs.MarshalType != null)
        array[num7++] = new CustomAttributeNamedArgument((MemberInfo) type.GetField("MarshalType"), (object) marshalAs.MarshalType);
      if (marshalAs.MarshalTypeRef != (Type) null)
        array[num7++] = new CustomAttributeNamedArgument((MemberInfo) type.GetField("MarshalTypeRef"), (object) marshalAs.MarshalTypeRef);
      if (marshalAs.MarshalCookie != null)
        array[num7++] = new CustomAttributeNamedArgument((MemberInfo) type.GetField("MarshalCookie"), (object) marshalAs.MarshalCookie);
      if (marshalAs.SafeArrayUserDefinedSubType != (Type) null)
      {
        CustomAttributeNamedArgument[] attributeNamedArgumentArray6 = array;
        int index6 = num7;
        int num8 = index6 + 1;
        CustomAttributeNamedArgument attributeNamedArgument6 = new CustomAttributeNamedArgument((MemberInfo) type.GetField("SafeArrayUserDefinedSubType"), (object) marshalAs.SafeArrayUserDefinedSubType);
        attributeNamedArgumentArray6[index6] = attributeNamedArgument6;
      }
      this.m_namedArgs = (IList<CustomAttributeNamedArgument>) Array.AsReadOnly<CustomAttributeNamedArgument>(array);
    }

    private void Init(TypeForwardedToAttribute forwardedTo)
    {
      this.m_ctor = typeof (TypeForwardedToAttribute).GetConstructor(BindingFlags.Instance | BindingFlags.Public, (Binder) null, new Type[1]
      {
        typeof (Type)
      }, (ParameterModifier[]) null);
      this.m_typedCtorArgs = (IList<CustomAttributeTypedArgument>) Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[1]
      {
        new CustomAttributeTypedArgument(typeof (Type), (object) forwardedTo.Destination)
      });
      this.m_namedArgs = (IList<CustomAttributeNamedArgument>) Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[0]);
    }

    private void Init(object pca)
    {
      this.m_ctor = pca.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
      this.m_typedCtorArgs = (IList<CustomAttributeTypedArgument>) Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[0]);
      this.m_namedArgs = (IList<CustomAttributeNamedArgument>) Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[0]);
    }

    /// <summary>
    ///   Возвращает строковое представление настраиваемого атрибута.
    /// </summary>
    /// <returns>
    ///   Строковое значение, представляющее пользовательский атрибут.
    /// </returns>
    public override string ToString()
    {
      string str1 = "";
      for (int index = 0; index < this.ConstructorArguments.Count; ++index)
        str1 += string.Format((IFormatProvider) CultureInfo.CurrentCulture, index == 0 ? "{0}" : ", {0}", (object) this.ConstructorArguments[index]);
      string str2 = "";
      for (int index = 0; index < this.NamedArguments.Count; ++index)
        str2 += string.Format((IFormatProvider) CultureInfo.CurrentCulture, index != 0 || str1.Length != 0 ? ", {0}" : "{0}", (object) this.NamedArguments[index]);
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "[{0}({1}{2})]", (object) this.Constructor.DeclaringType.FullName, (object) str1, (object) str2);
    }

    /// <summary>Служит хэш-функцией для определенного типа.</summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Object" />.
    /// </returns>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим экземпляром, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="obj" /> равен текущему экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      return obj == this;
    }

    /// <summary>Возвращает тип атрибута.</summary>
    /// <returns>Тип атрибута.</returns>
    [__DynamicallyInvokable]
    public Type AttributeType
    {
      [__DynamicallyInvokable] get
      {
        return this.Constructor.DeclaringType;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Reflection.ConstructorInfo" />, представляющий конструктор, предназначенный для инициализации настраиваемого атрибута.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий конструктор, предназначенный для инициализации настраиваемого атрибута, представленного текущим экземпляром класса <see cref="T:System.Reflection.CustomAttributeData" />.
    /// </returns>
    [ComVisible(true)]
    public virtual ConstructorInfo Constructor
    {
      get
      {
        return this.m_ctor;
      }
    }

    /// <summary>
    ///   Получает список позиционные аргументы, заданные для экземпляра атрибута, представленного <see cref="T:System.Reflection.CustomAttributeData" /> объекта.
    /// </summary>
    /// <returns>
    ///   Коллекция структур, которые представляют позиционные аргументы, заданные для экземпляра настраиваемого атрибута.
    /// </returns>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public virtual IList<CustomAttributeTypedArgument> ConstructorArguments
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_typedCtorArgs == null)
        {
          CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[this.m_ctorParams.Length];
          for (int index = 0; index < array.Length; ++index)
          {
            CustomAttributeEncodedArgument attributeEncodedArgument = this.m_ctorParams[index].CustomAttributeEncodedArgument;
            array[index] = new CustomAttributeTypedArgument(this.m_scope, this.m_ctorParams[index].CustomAttributeEncodedArgument);
          }
          this.m_typedCtorArgs = (IList<CustomAttributeTypedArgument>) Array.AsReadOnly<CustomAttributeTypedArgument>(array);
        }
        return this.m_typedCtorArgs;
      }
    }

    /// <summary>
    ///   Получает список именованных аргументов, заданных для экземпляра атрибута, представленного <see cref="T:System.Reflection.CustomAttributeData" /> объекта.
    /// </summary>
    /// <returns>
    ///   Коллекция структур, которые представляют именованные аргументы, заданные для экземпляра настраиваемого атрибута.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual IList<CustomAttributeNamedArgument> NamedArguments
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_namedArgs == null)
        {
          if (this.m_namedParams == null)
            return (IList<CustomAttributeNamedArgument>) null;
          int length = 0;
          CustomAttributeType customAttributeType;
          for (int index = 0; index < this.m_namedParams.Length; ++index)
          {
            customAttributeType = this.m_namedParams[index].EncodedArgument.CustomAttributeType;
            if (customAttributeType.EncodedType != CustomAttributeEncoding.Undefined)
              ++length;
          }
          CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[length];
          int index1 = 0;
          int num = 0;
          for (; index1 < this.m_namedParams.Length; ++index1)
          {
            customAttributeType = this.m_namedParams[index1].EncodedArgument.CustomAttributeType;
            if (customAttributeType.EncodedType != CustomAttributeEncoding.Undefined)
              array[num++] = new CustomAttributeNamedArgument(this.m_members[index1], new CustomAttributeTypedArgument(this.m_scope, this.m_namedParams[index1].EncodedArgument));
          }
          this.m_namedArgs = (IList<CustomAttributeNamedArgument>) Array.AsReadOnly<CustomAttributeNamedArgument>(array);
        }
        return this.m_namedArgs;
      }
    }
  }
}
