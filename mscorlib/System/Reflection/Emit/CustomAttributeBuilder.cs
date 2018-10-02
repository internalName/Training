// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.CustomAttributeBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Reflection.Emit
{
  /// <summary>Помогает в построении пользовательских атрибутов.</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_CustomAttributeBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public class CustomAttributeBuilder : _CustomAttributeBuilder
  {
    internal ConstructorInfo m_con;
    internal object[] m_constructorArgs;
    internal byte[] m_blob;

    /// <summary>
    ///   Инициализирует экземпляр класса <see langword="CustomAttributeBuilder" />, передавая конструктор для настраиваемого атрибута и аргументы в конструктор.
    /// </summary>
    /// <param name="con">Конструктор настраиваемого атрибута.</param>
    /// <param name="constructorArgs">
    ///   Аргументы, передаваемые конструктору настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="con" /> является статическим или закрытым.
    /// 
    ///   -или-
    /// 
    ///   Число переданных аргументов не соответствует числу параметров конструктора в соответствии с требованиями соглашения о вызовах конструктора.
    /// 
    ///   -или-
    /// 
    ///   Тип переданного аргумента не соответствует типу параметра, объявленного в конструкторе.
    /// 
    ///   -или-
    /// 
    ///   Ссылочный тип переданного аргумента отличается от <see cref="T:System.String" /> или <see cref="T:System.Type" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="con" /> или <paramref name="constructorArgs" /> имеет значение <see langword="null" />.
    /// </exception>
    public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs)
    {
      this.InitCustomAttributeBuilder(con, constructorArgs, new PropertyInfo[0], new object[0], new FieldInfo[0], new object[0]);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="CustomAttributeBuilder" /> передачей конструктора для пользовательских атрибутов, аргументы конструктора, а также набора поименованных пар значений или свойств класса.
    /// </summary>
    /// <param name="con">Конструктор настраиваемого атрибута.</param>
    /// <param name="constructorArgs">
    ///   Аргументы конструктора пользовательского атрибута.
    /// </param>
    /// <param name="namedProperties">
    ///   Указанные свойства пользовательского атрибута.
    /// </param>
    /// <param name="propertyValues">
    ///   Значения указанных свойств пользовательского атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Длины <paramref name="namedProperties" /> и <paramref name="propertyValues" /> массивы отличаются.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="con" /> является статическим или закрытым.
    /// 
    ///   -или-
    /// 
    ///   Количество переданных аргументов не соответствует числу параметров конструктора, при необходимости, соглашение о вызовах конструктора.
    /// 
    ///   -или-
    /// 
    ///   Тип переданного аргумента не соответствует типу параметра, объявленного в конструкторе.
    /// 
    ///   -или-
    /// 
    ///   Типы значений свойств не соответствуют типам указанных свойств.
    /// 
    ///   -или-
    /// 
    ///   Свойство не имеет установочного метода.
    /// 
    ///   -или-
    /// 
    ///   Свойство не принадлежит тому же классу или базовый класс как конструктор.
    /// 
    ///   -или-
    /// 
    ///   Указанный аргумент или именованное свойство имеет ссылочный тип не <see cref="T:System.String" /> или <see cref="T:System.Type" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Один из параметров является <see langword="null" />.
    /// </exception>
    public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues)
    {
      this.InitCustomAttributeBuilder(con, constructorArgs, namedProperties, propertyValues, new FieldInfo[0], new object[0]);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="CustomAttributeBuilder" /> передачей конструктора для пользовательских атрибутов, аргументы конструктора, а также набора пар поля и значения класса.
    /// </summary>
    /// <param name="con">Конструктор настраиваемого атрибута.</param>
    /// <param name="constructorArgs">
    ///   Аргументы конструктора пользовательского атрибута.
    /// </param>
    /// <param name="namedFields">
    ///   Указанные поля пользовательского атрибута.
    /// </param>
    /// <param name="fieldValues">
    ///   Значения указанных полей пользовательского атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Длины <paramref name="namedFields" /> и <paramref name="fieldValues" /> массивы отличаются.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="con" /> является статическим или закрытым.
    /// 
    ///   -или-
    /// 
    ///   Количество переданных аргументов не соответствует числу параметров конструктора, при необходимости, соглашение о вызовах конструктора.
    /// 
    ///   -или-
    /// 
    ///   Тип переданного аргумента не соответствует типу параметра, объявленного в конструкторе.
    /// 
    ///   -или-
    /// 
    ///   Типы значений полей не соответствуют типам указанных полей.
    /// 
    ///   -или-
    /// 
    ///   Поле не принадлежит тому же классу или базовый класс как конструктор.
    /// 
    ///   -или-
    /// 
    ///   Указанный аргумент или именованного поля является ссылочным типом не <see cref="T:System.String" /> или <see cref="T:System.Type" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Один из параметров является <see langword="null" />.
    /// </exception>
    public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, FieldInfo[] namedFields, object[] fieldValues)
    {
      this.InitCustomAttributeBuilder(con, constructorArgs, new PropertyInfo[0], new object[0], namedFields, fieldValues);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="CustomAttributeBuilder" /> передачей конструктора для пользовательских атрибутов, аргументы конструктора, набора поименованных пар значений или свойств и набор классов с именем пар значений или полей.
    /// </summary>
    /// <param name="con">Конструктор настраиваемого атрибута.</param>
    /// <param name="constructorArgs">
    ///   Аргументы конструктора пользовательского атрибута.
    /// </param>
    /// <param name="namedProperties">
    ///   Указанные свойства пользовательского атрибута.
    /// </param>
    /// <param name="propertyValues">
    ///   Значения указанных свойств пользовательского атрибута.
    /// </param>
    /// <param name="namedFields">
    ///   Указанные поля пользовательского атрибута.
    /// </param>
    /// <param name="fieldValues">
    ///   Значения указанных полей пользовательского атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Длины <paramref name="namedProperties" /> и <paramref name="propertyValues" /> массивы отличаются.
    /// 
    ///   -или-
    /// 
    ///   Длины <paramref name="namedFields" /> и <paramref name="fieldValues" /> массивы отличаются.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="con" /> является статическим или закрытым.
    /// 
    ///   -или-
    /// 
    ///   Количество переданных аргументов не соответствует числу параметров конструктора, при необходимости, соглашение о вызовах конструктора.
    /// 
    ///   -или-
    /// 
    ///   Тип переданного аргумента не соответствует типу параметра, объявленного в конструкторе.
    /// 
    ///   -или-
    /// 
    ///   Типы значений свойств не соответствуют типам указанных свойств.
    /// 
    ///   -или-
    /// 
    ///   Типы значений полей не соответствуют типам соответствующих типов полей.
    /// 
    ///   -или-
    /// 
    ///   Свойство не имеет метода setter.
    /// 
    ///   -или-
    /// 
    ///   Свойство или поле не принадлежит тому же классу или базовый класс как конструктор.
    /// 
    ///   -или-
    /// 
    ///   Указанный аргумент, именованное свойство или именованного поля является ссылочным типом не <see cref="T:System.String" /> или <see cref="T:System.Type" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Один из параметров является <see langword="null" />.
    /// </exception>
    public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
    {
      this.InitCustomAttributeBuilder(con, constructorArgs, namedProperties, propertyValues, namedFields, fieldValues);
    }

    private bool ValidateType(Type t)
    {
      if (t.IsPrimitive || t == typeof (string) || t == typeof (Type))
        return true;
      if (t.IsEnum)
      {
        switch (Type.GetTypeCode(Enum.GetUnderlyingType(t)))
        {
          case TypeCode.SByte:
          case TypeCode.Byte:
          case TypeCode.Int16:
          case TypeCode.UInt16:
          case TypeCode.Int32:
          case TypeCode.UInt32:
          case TypeCode.Int64:
          case TypeCode.UInt64:
            return true;
          default:
            return false;
        }
      }
      else
      {
        if (!t.IsArray)
          return t == typeof (object);
        if (t.GetArrayRank() != 1)
          return false;
        return this.ValidateType(t.GetElementType());
      }
    }

    internal void InitCustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (con));
      if (constructorArgs == null)
        throw new ArgumentNullException(nameof (constructorArgs));
      if (namedProperties == null)
        throw new ArgumentNullException(nameof (namedProperties));
      if (propertyValues == null)
        throw new ArgumentNullException(nameof (propertyValues));
      if (namedFields == null)
        throw new ArgumentNullException(nameof (namedFields));
      if (fieldValues == null)
        throw new ArgumentNullException(nameof (fieldValues));
      if (namedProperties.Length != propertyValues.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"), "namedProperties, propertyValues");
      if (namedFields.Length != fieldValues.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"), "namedFields, fieldValues");
      if ((con.Attributes & MethodAttributes.Static) == MethodAttributes.Static || (con.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadConstructor"));
      if ((con.CallingConvention & CallingConventions.Standard) != CallingConventions.Standard)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadConstructorCallConv"));
      this.m_con = con;
      this.m_constructorArgs = new object[constructorArgs.Length];
      Array.Copy((Array) constructorArgs, (Array) this.m_constructorArgs, constructorArgs.Length);
      Type[] parameterTypes = con.GetParameterTypes();
      if (parameterTypes.Length != constructorArgs.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadParameterCountsForConstructor"));
      for (int index = 0; index < parameterTypes.Length; ++index)
      {
        if (!this.ValidateType(parameterTypes[index]))
          throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeInCustomAttribute"));
      }
      for (int index = 0; index < parameterTypes.Length; ++index)
      {
        if (constructorArgs[index] != null)
        {
          TypeCode typeCode = Type.GetTypeCode(parameterTypes[index]);
          if (typeCode != Type.GetTypeCode(constructorArgs[index].GetType()) && (typeCode != TypeCode.Object || !this.ValidateType(constructorArgs[index].GetType())))
            throw new ArgumentException(Environment.GetResourceString("Argument_BadParameterTypeForConstructor", (object) index));
        }
      }
      BinaryWriter writer = new BinaryWriter((Stream) new MemoryStream());
      writer.Write((ushort) 1);
      for (int index = 0; index < constructorArgs.Length; ++index)
        this.EmitValue(writer, parameterTypes[index], constructorArgs[index]);
      writer.Write((ushort) (namedProperties.Length + namedFields.Length));
      for (int index = 0; index < namedProperties.Length; ++index)
      {
        if (namedProperties[index] == (PropertyInfo) null)
          throw new ArgumentNullException("namedProperties[" + (object) index + "]");
        Type propertyType = namedProperties[index].PropertyType;
        if (propertyValues[index] == null && propertyType.IsPrimitive)
          throw new ArgumentNullException("propertyValues[" + (object) index + "]");
        if (!this.ValidateType(propertyType))
          throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeInCustomAttribute"));
        if (!namedProperties[index].CanWrite)
          throw new ArgumentException(Environment.GetResourceString("Argument_NotAWritableProperty"));
        if (namedProperties[index].DeclaringType != con.DeclaringType && !(con.DeclaringType is TypeBuilderInstantiation) && (!con.DeclaringType.IsSubclassOf(namedProperties[index].DeclaringType) && !TypeBuilder.IsTypeEqual(namedProperties[index].DeclaringType, con.DeclaringType)) && (!(namedProperties[index].DeclaringType is TypeBuilder) || !con.DeclaringType.IsSubclassOf((Type) ((TypeBuilder) namedProperties[index].DeclaringType).BakedRuntimeType)))
          throw new ArgumentException(Environment.GetResourceString("Argument_BadPropertyForConstructorBuilder"));
        if (propertyValues[index] != null && propertyType != typeof (object) && Type.GetTypeCode(propertyValues[index].GetType()) != Type.GetTypeCode(propertyType))
          throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
        writer.Write((byte) 84);
        this.EmitType(writer, propertyType);
        this.EmitString(writer, namedProperties[index].Name);
        this.EmitValue(writer, propertyType, propertyValues[index]);
      }
      for (int index = 0; index < namedFields.Length; ++index)
      {
        if (namedFields[index] == (FieldInfo) null)
          throw new ArgumentNullException("namedFields[" + (object) index + "]");
        Type fieldType = namedFields[index].FieldType;
        if (fieldValues[index] == null && fieldType.IsPrimitive)
          throw new ArgumentNullException("fieldValues[" + (object) index + "]");
        if (!this.ValidateType(fieldType))
          throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeInCustomAttribute"));
        if (namedFields[index].DeclaringType != con.DeclaringType && !(con.DeclaringType is TypeBuilderInstantiation) && (!con.DeclaringType.IsSubclassOf(namedFields[index].DeclaringType) && !TypeBuilder.IsTypeEqual(namedFields[index].DeclaringType, con.DeclaringType)) && (!(namedFields[index].DeclaringType is TypeBuilder) || !con.DeclaringType.IsSubclassOf((Type) ((TypeBuilder) namedFields[index].DeclaringType).BakedRuntimeType)))
          throw new ArgumentException(Environment.GetResourceString("Argument_BadFieldForConstructorBuilder"));
        if (fieldValues[index] != null && fieldType != typeof (object) && Type.GetTypeCode(fieldValues[index].GetType()) != Type.GetTypeCode(fieldType))
          throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
        writer.Write((byte) 83);
        this.EmitType(writer, fieldType);
        this.EmitString(writer, namedFields[index].Name);
        this.EmitValue(writer, fieldType, fieldValues[index]);
      }
      this.m_blob = ((MemoryStream) writer.BaseStream).ToArray();
    }

    private void EmitType(BinaryWriter writer, Type type)
    {
      if (type.IsPrimitive)
      {
        switch (Type.GetTypeCode(type))
        {
          case TypeCode.Boolean:
            writer.Write((byte) 2);
            break;
          case TypeCode.Char:
            writer.Write((byte) 3);
            break;
          case TypeCode.SByte:
            writer.Write((byte) 4);
            break;
          case TypeCode.Byte:
            writer.Write((byte) 5);
            break;
          case TypeCode.Int16:
            writer.Write((byte) 6);
            break;
          case TypeCode.UInt16:
            writer.Write((byte) 7);
            break;
          case TypeCode.Int32:
            writer.Write((byte) 8);
            break;
          case TypeCode.UInt32:
            writer.Write((byte) 9);
            break;
          case TypeCode.Int64:
            writer.Write((byte) 10);
            break;
          case TypeCode.UInt64:
            writer.Write((byte) 11);
            break;
          case TypeCode.Single:
            writer.Write((byte) 12);
            break;
          case TypeCode.Double:
            writer.Write((byte) 13);
            break;
        }
      }
      else if (type.IsEnum)
      {
        writer.Write((byte) 85);
        this.EmitString(writer, type.AssemblyQualifiedName);
      }
      else if (type == typeof (string))
        writer.Write((byte) 14);
      else if (type == typeof (Type))
        writer.Write((byte) 80);
      else if (type.IsArray)
      {
        writer.Write((byte) 29);
        this.EmitType(writer, type.GetElementType());
      }
      else
        writer.Write((byte) 81);
    }

    private void EmitString(BinaryWriter writer, string str)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(str);
      uint length = (uint) bytes.Length;
      if (length <= (uint) sbyte.MaxValue)
        writer.Write((byte) length);
      else if (length <= 16383U)
      {
        writer.Write((byte) (length >> 8 | 128U));
        writer.Write((byte) (length & (uint) byte.MaxValue));
      }
      else
      {
        writer.Write((byte) (length >> 24 | 192U));
        writer.Write((byte) (length >> 16 & (uint) byte.MaxValue));
        writer.Write((byte) (length >> 8 & (uint) byte.MaxValue));
        writer.Write((byte) (length & (uint) byte.MaxValue));
      }
      writer.Write(bytes);
    }

    private void EmitValue(BinaryWriter writer, Type type, object value)
    {
      if (type.IsEnum)
      {
        switch (Type.GetTypeCode(Enum.GetUnderlyingType(type)))
        {
          case TypeCode.SByte:
            writer.Write((sbyte) value);
            break;
          case TypeCode.Byte:
            writer.Write((byte) value);
            break;
          case TypeCode.Int16:
            writer.Write((short) value);
            break;
          case TypeCode.UInt16:
            writer.Write((ushort) value);
            break;
          case TypeCode.Int32:
            writer.Write((int) value);
            break;
          case TypeCode.UInt32:
            writer.Write((uint) value);
            break;
          case TypeCode.Int64:
            writer.Write((long) value);
            break;
          case TypeCode.UInt64:
            writer.Write((ulong) value);
            break;
        }
      }
      else if (type == typeof (string))
      {
        if (value == null)
          writer.Write(byte.MaxValue);
        else
          this.EmitString(writer, (string) value);
      }
      else if (type == typeof (Type))
      {
        if (value == null)
        {
          writer.Write(byte.MaxValue);
        }
        else
        {
          string str = TypeNameBuilder.ToString((Type) value, TypeNameBuilder.Format.AssemblyQualifiedName);
          if (str == null)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeForCA", (object) value.GetType()));
          this.EmitString(writer, str);
        }
      }
      else if (type.IsArray)
      {
        if (value == null)
        {
          writer.Write(uint.MaxValue);
        }
        else
        {
          Array array = (Array) value;
          Type elementType = type.GetElementType();
          writer.Write(array.Length);
          for (int index = 0; index < array.Length; ++index)
            this.EmitValue(writer, elementType, array.GetValue(index));
        }
      }
      else if (type.IsPrimitive)
      {
        switch (Type.GetTypeCode(type))
        {
          case TypeCode.Boolean:
            writer.Write((bool) value ? (byte) 1 : (byte) 0);
            break;
          case TypeCode.Char:
            writer.Write(Convert.ToUInt16((char) value));
            break;
          case TypeCode.SByte:
            writer.Write((sbyte) value);
            break;
          case TypeCode.Byte:
            writer.Write((byte) value);
            break;
          case TypeCode.Int16:
            writer.Write((short) value);
            break;
          case TypeCode.UInt16:
            writer.Write((ushort) value);
            break;
          case TypeCode.Int32:
            writer.Write((int) value);
            break;
          case TypeCode.UInt32:
            writer.Write((uint) value);
            break;
          case TypeCode.Int64:
            writer.Write((long) value);
            break;
          case TypeCode.UInt64:
            writer.Write((ulong) value);
            break;
          case TypeCode.Single:
            writer.Write((float) value);
            break;
          case TypeCode.Double:
            writer.Write((double) value);
            break;
        }
      }
      else if (type == typeof (object))
      {
        Type type1 = value == null ? typeof (string) : ((object) (value as Type) != null ? typeof (Type) : value.GetType());
        if (type1 == typeof (object))
          throw new ArgumentException(Environment.GetResourceString("Argument_BadParameterTypeForCAB", (object) type1.ToString()));
        this.EmitType(writer, type1);
        this.EmitValue(writer, type1, value);
      }
      else
      {
        string str = "null";
        if (value != null)
          str = value.GetType().ToString();
        throw new ArgumentException(Environment.GetResourceString("Argument_BadParameterTypeForCAB", (object) str));
      }
    }

    [SecurityCritical]
    internal void CreateCustomAttribute(ModuleBuilder mod, int tkOwner)
    {
      this.CreateCustomAttribute(mod, tkOwner, mod.GetConstructorToken(this.m_con).Token, false);
    }

    [SecurityCritical]
    internal int PrepareCreateCustomAttributeToDisk(ModuleBuilder mod)
    {
      return mod.InternalGetConstructorToken(this.m_con, true).Token;
    }

    [SecurityCritical]
    internal void CreateCustomAttribute(ModuleBuilder mod, int tkOwner, int tkAttrib, bool toDisk)
    {
      TypeBuilder.DefineCustomAttribute(mod, tkOwner, tkAttrib, this.m_blob, toDisk, typeof (DebuggableAttribute) == this.m_con.DeclaringType);
    }

    void _CustomAttributeBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _CustomAttributeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _CustomAttributeBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _CustomAttributeBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
