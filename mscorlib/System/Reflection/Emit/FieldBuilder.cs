// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.FieldBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Определяет и представляет поле.
  ///    Этот класс не наследуется.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_FieldBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class FieldBuilder : FieldInfo, _FieldBuilder
  {
    private int m_fieldTok;
    private FieldToken m_tkField;
    private TypeBuilder m_typeBuilder;
    private string m_fieldName;
    private FieldAttributes m_Attributes;
    private Type m_fieldType;

    [SecurityCritical]
    internal FieldBuilder(TypeBuilder typeBuilder, string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
    {
      if (fieldName == null)
        throw new ArgumentNullException(nameof (fieldName));
      if (fieldName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (fieldName));
      if (fieldName[0] == char.MinValue)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), nameof (fieldName));
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (type == typeof (void))
        throw new ArgumentException(Environment.GetResourceString("Argument_BadFieldType"));
      this.m_fieldName = fieldName;
      this.m_typeBuilder = typeBuilder;
      this.m_fieldType = type;
      this.m_Attributes = attributes & ~FieldAttributes.ReservedMask;
      SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper(this.m_typeBuilder.Module);
      fieldSigHelper.AddArgument(type, requiredCustomModifiers, optionalCustomModifiers);
      int length;
      byte[] signature = fieldSigHelper.InternalGetSignature(out length);
      this.m_fieldTok = TypeBuilder.DefineField(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), typeBuilder.TypeToken.Token, fieldName, signature, length, this.m_Attributes);
      this.m_tkField = new FieldToken(this.m_fieldTok, type);
    }

    [SecurityCritical]
    internal void SetData(byte[] data, int size)
    {
      ModuleBuilder.SetFieldRVAContent(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), this.m_tkField.Token, data, size);
    }

    internal TypeBuilder GetTypeBuilder()
    {
      return this.m_typeBuilder;
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_fieldTok;
      }
    }

    /// <summary>
    ///   Возвращает модуль, в котором определен тип, который содержит это поле.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Module" /> представляющий динамический модуль, в котором определен в этом поле.
    /// </returns>
    public override Module Module
    {
      get
      {
        return this.m_typeBuilder.Module;
      }
    }

    /// <summary>
    ///   Указывает имя этого поля.
    ///    Это свойство доступно только для чтения.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> с именем этого поля.
    /// </returns>
    public override string Name
    {
      get
      {
        return this.m_fieldName;
      }
    }

    /// <summary>
    ///   Указывает ссылку на <see cref="T:System.Type" /> объект типа, который объявляет это поле.
    ///    Это свойство доступно только для чтения.
    /// </summary>
    /// <returns>
    ///   Ссылку на <see cref="T:System.Type" /> объект типа, который объявляет это поле.
    /// </returns>
    public override Type DeclaringType
    {
      get
      {
        if (this.m_typeBuilder.m_isHiddenGlobalType)
          return (Type) null;
        return (Type) this.m_typeBuilder;
      }
    }

    /// <summary>
    ///   Дает ссылку на <see cref="T:System.Type" /> объекта, из которого был получен данный объект.
    ///    Это свойство доступно только для чтения.
    /// </summary>
    /// <returns>
    ///   Ссылку на <see cref="T:System.Type" /> объекта, из которого был получен данный экземпляр.
    /// </returns>
    public override Type ReflectedType
    {
      get
      {
        if (this.m_typeBuilder.m_isHiddenGlobalType)
          return (Type) null;
        return (Type) this.m_typeBuilder;
      }
    }

    /// <summary>
    ///   Указывает <see cref="T:System.Type" /> представляющий тип этого поля.
    ///    Это свойство доступно только для чтения.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Представляющий тип этого поля.
    /// </returns>
    public override Type FieldType
    {
      get
      {
        return this.m_fieldType;
      }
    }

    /// <summary>
    ///   Получает значение поля, поддерживаемое данным объектом.
    /// </summary>
    /// <param name="obj">Объект для доступа к полю.</param>
    /// <returns>
    ///   <see cref="T:System.Object" /> Содержащий значение поля, отраженное этим экземпляром.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override object GetValue(object obj)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Задает значение поля, поддерживаемое данным объектом.
    /// </summary>
    /// <param name="obj">Объект для доступа к полю.</param>
    /// <param name="val">Значение, присваиваемое полю.</param>
    /// <param name="invokeAttr">
    ///   Член <see langword="IBinder" /> указывающий тип связывания (например, IBinder.CreateInstance, IBinder.ExactBinding).
    /// </param>
    /// <param name="binder">
    ///   Набор свойств и разрешение на связывание, приведение типов аргументов и вызов членов с помощью отражения.
    ///    Если привязки имеет значение null, используется IBinder.DefaultBinding.
    /// </param>
    /// <param name="culture">
    ///   Программные настройки конкретного языка и региональных параметров.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Указывает внутренний дескриптор метаданных для данного поля.
    ///    Это свойство доступно только для чтения.
    /// </summary>
    /// <returns>Внутренний дескриптор метаданных для данного поля.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override RuntimeFieldHandle FieldHandle
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    /// <summary>
    ///   Указывает атрибуты данного поля.
    ///    Это свойство доступно только для чтения.
    /// </summary>
    /// <returns>Атрибуты данного поля.</returns>
    public override FieldAttributes Attributes
    {
      get
      {
        return this.m_Attributes;
      }
    }

    /// <summary>
    ///   Возвращает пользовательские атрибуты, определенные для данного поля.
    /// </summary>
    /// <param name="inherit">
    ///   Управляет наследованием настраиваемых атрибутов базового класса.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Object" /> представляющий пользовательские атрибуты конструктора, который представлен этим <see cref="T:System.Reflection.Emit.FieldBuilder" /> экземпляра.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Возвращает пользовательские атрибуты, определенные для данного поля, определяемый заданным типом.
    /// </summary>
    /// <param name="attributeType">Тип настраиваемого атрибута.</param>
    /// <param name="inherit">
    ///   Управляет наследованием настраиваемых атрибутов базового класса.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Object" /> представляющий пользовательские атрибуты конструктора, который представлен этим <see cref="T:System.Reflection.Emit.FieldBuilder" /> экземпляра.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>
    ///   Указывает, определен ли для поля атрибут указанного типа.
    /// </summary>
    /// <param name="attributeType">Тип атрибута.</param>
    /// <param name="inherit">
    ///   Управляет наследованием настраиваемых атрибутов базового класса.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если один или несколько экземпляров <paramref name="attributeType" /> определен в этом поле; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   В настоящее время этот метод не поддерживается.
    ///    Получить поля с помощью <see cref="M:System.Type.GetField(System.String,System.Reflection.BindingFlags)" /> и вызов <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> для возвращенного <see cref="T:System.Reflection.FieldInfo" />.
    /// </exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>Возвращает токен, представляющий данное поле.</summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Reflection.Emit.FieldToken" /> представляющий маркер для этого поля.
    /// </returns>
    public FieldToken GetToken()
    {
      return this.m_tkField;
    }

    /// <summary>Указывает расположение поля.</summary>
    /// <param name="iOffset">
    ///   Смещение поля внутри содержащего это поле типа.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение параметра <paramref name="iOffset" /> меньше нуля.
    /// </exception>
    [SecuritySafeCritical]
    public void SetOffset(int iOffset)
    {
      this.m_typeBuilder.ThrowIfCreated();
      TypeBuilder.SetFieldLayoutOffset(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), this.GetToken().Token, iOffset);
    }

    /// <summary>Описывает маршалинг собственного поля.</summary>
    /// <param name="unmanagedMarshal">
    ///   Дескриптор, устанавливающий присущий данному объекту маршалинг данного поля.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="unmanagedMarshal" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
    {
      if (unmanagedMarshal == null)
        throw new ArgumentNullException(nameof (unmanagedMarshal));
      this.m_typeBuilder.ThrowIfCreated();
      byte[] bytes = unmanagedMarshal.InternalGetBytes();
      TypeBuilder.SetFieldMarshal(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), this.GetToken().Token, bytes, bytes.Length);
    }

    /// <summary>Задает значение по умолчанию для этого поля.</summary>
    /// <param name="defaultValue">
    ///   Новое значение по умолчанию для этого поля.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан с помощью <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Поле не является одним из поддерживаемых типов.
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="defaultValue" /> не соответствует типу поля.
    /// 
    ///   -или-
    /// 
    ///   Поле имеет тип <see cref="T:System.Object" /> или другой ссылочный тип, <paramref name="defaultValue" /> не <see langword="null" />, и значение не может быть назначен ссылочного типа.
    /// </exception>
    [SecuritySafeCritical]
    public void SetConstant(object defaultValue)
    {
      this.m_typeBuilder.ThrowIfCreated();
      TypeBuilder.SetConstantValue(this.m_typeBuilder.GetModuleBuilder(), this.GetToken().Token, this.m_fieldType, defaultValue);
    }

    /// <summary>
    ///   Задает настраиваемый атрибут с помощью большого двоичного объекта пользовательских атрибутов.
    /// </summary>
    /// <param name="con">Конструктор настраиваемого атрибута.</param>
    /// <param name="binaryAttribute">
    ///   Большой двоичный объект байтов, представляющий атрибуты.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="con" /> или <paramref name="binaryAttribute" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Родительский тип данного поля завершена.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (con));
      if (binaryAttribute == null)
        throw new ArgumentNullException(nameof (binaryAttribute));
      ModuleBuilder module = this.m_typeBuilder.Module as ModuleBuilder;
      this.m_typeBuilder.ThrowIfCreated();
      TypeBuilder.DefineCustomAttribute(module, this.m_tkField.Token, module.GetConstructorToken(con).Token, binaryAttribute, false, false);
    }

    /// <summary>
    ///   Задает настраиваемый атрибут с помощью построителя настраиваемых атрибутов.
    /// </summary>
    /// <param name="customBuilder">
    ///   Экземпляр вспомогательного класса для определения настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="con" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Родительский тип данного поля завершена.
    /// </exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException(nameof (customBuilder));
      this.m_typeBuilder.ThrowIfCreated();
      ModuleBuilder module = this.m_typeBuilder.Module as ModuleBuilder;
      customBuilder.CreateCustomAttribute(module, this.m_tkField.Token);
    }

    void _FieldBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _FieldBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _FieldBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _FieldBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
