// Decompiled with JetBrains decompiler
// Type: System.Reflection.ParameterInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
  /// <summary>
  ///   Обнаруживает атрибуты параметра и обеспечивает доступ к его метаданным.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_ParameterInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ParameterInfo : _ParameterInfo, ICustomAttributeProvider, IObjectReference
  {
    /// <summary>Имя параметра.</summary>
    protected string NameImpl;
    /// <summary>
    ///   Тип <see langword="Type" /> параметра.
    /// </summary>
    protected Type ClassImpl;
    /// <summary>
    ///   Отсчитываемая от нуля позиция параметра в списке параметров.
    /// </summary>
    protected int PositionImpl;
    /// <summary>Атрибуты параметра.</summary>
    protected ParameterAttributes AttrsImpl;
    /// <summary>Значение по умолчанию параметра.</summary>
    protected object DefaultValueImpl;
    /// <summary>Элемент, в котором реализовано данное поле.</summary>
    protected MemberInfo MemberImpl;
    [OptionalField]
    private IntPtr _importer;
    [OptionalField]
    private int _token;
    [OptionalField]
    private bool bExtraConstChecked;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="ParameterInfo" />.
    /// </summary>
    protected ParameterInfo()
    {
    }

    internal void SetName(string name)
    {
      this.NameImpl = name;
    }

    internal void SetAttributes(ParameterAttributes attributes)
    {
      this.AttrsImpl = attributes;
    }

    /// <summary>
    ///   Возвращает <see langword="Type" /> этого параметра.
    /// </summary>
    /// <returns>
    ///   <see langword="Type" /> Объект, представляющий <see langword="Type" /> этого параметра.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Type ParameterType
    {
      [__DynamicallyInvokable] get
      {
        return this.ClassImpl;
      }
    }

    /// <summary>Возвращает имя параметра.</summary>
    /// <returns>Простое имя этого параметра.</returns>
    [__DynamicallyInvokable]
    public virtual string Name
    {
      [__DynamicallyInvokable] get
      {
        return this.NameImpl;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли этот параметр имеет значение по умолчанию.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот параметр имеет значение по умолчанию. в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool HasDefaultValue
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее значение по умолчанию, если параметр имеет значение по умолчанию.
    /// </summary>
    /// <returns>
    ///   Значение по умолчанию параметра, или <see cref="F:System.DBNull.Value" /> Если параметр имеет значение по умолчанию отсутствует.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual object DefaultValue
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает значение по умолчанию, если оно задано для параметра.
    /// </summary>
    /// <returns>
    ///   Значение параметра по умолчанию или значение <see cref="F:System.DBNull.Value" />, если параметр не имеет значения по умолчанию.
    /// </returns>
    public virtual object RawDefaultValue
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает отсчитываемый от нуля позиция параметра в списке формальных параметров.
    /// </summary>
    /// <returns>
    ///   Целое число, представляющее позицию этот параметр занимает в списке параметров.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual int Position
    {
      [__DynamicallyInvokable] get
      {
        return this.PositionImpl;
      }
    }

    /// <summary>Возвращает атрибуты для этого параметра.</summary>
    /// <returns>
    ///   Объект <see langword="ParameterAttributes" /> объект, представляющий атрибуты для этого параметра.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual ParameterAttributes Attributes
    {
      [__DynamicallyInvokable] get
      {
        return this.AttrsImpl;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее член, в котором реализован данный параметр.
    /// </summary>
    /// <returns>
    ///   Член, который implanted параметра, представленного этим экземпляром <see cref="T:System.Reflection.ParameterInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual MemberInfo Member
    {
      [__DynamicallyInvokable] get
      {
        return this.MemberImpl;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли входной параметр.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если параметр является входным параметром; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsIn
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & ParameterAttributes.In) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли это выходной параметр.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если параметр является выходным; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsOut
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & ParameterAttributes.Out) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли этот параметр языкового стандарта (LCID).
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если параметр является кодом языка; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsLcid
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & ParameterAttributes.Lcid) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see langword="Retval" /> параметр.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если параметр является <see langword="Retval" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsRetval
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & ParameterAttributes.Retval) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли этот параметр является необязательным.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если параметр является необязательным. в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsOptional
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & ParameterAttributes.Optional) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, определяющее этот параметр в метаданных.
    /// </summary>
    /// <returns>
    ///   Значение, которое, в сочетании с модулем уникально идентифицирует этот параметр в метаданных.
    /// </returns>
    public virtual int MetadataToken
    {
      get
      {
        RuntimeParameterInfo runtimeParameterInfo = this as RuntimeParameterInfo;
        if (runtimeParameterInfo != null)
          return runtimeParameterInfo.MetadataToken;
        return 134217728;
      }
    }

    /// <summary>
    ///   Возвращает обязательные настраиваемые модификаторы параметра.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, которые указывают обязательные настраиваемые модификаторы для текущего параметра, например <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.
    /// </returns>
    public virtual Type[] GetRequiredCustomModifiers()
    {
      return EmptyArray<Type>.Value;
    }

    /// <summary>
    ///   Возвращает необязательные настраиваемые модификаторы параметра.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, указывающих дополнительные настраиваемые модификаторы для текущего параметра, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.
    /// </returns>
    public virtual Type[] GetOptionalCustomModifiers()
    {
      return EmptyArray<Type>.Value;
    }

    /// <summary>
    ///   Возвращает тип параметра и имя, представленное в виде строки.
    /// </summary>
    /// <returns>Строка, содержащая имя типа и имя параметра.</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.ParameterType.FormatTypeName() + " " + this.Name;
    }

    /// <summary>
    ///   Возвращает коллекцию, содержащую пользовательские атрибуты этого параметра.
    /// </summary>
    /// <returns>
    ///   Коллекция, содержащая пользовательские атрибуты этого параметра.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<CustomAttributeData> CustomAttributes
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<CustomAttributeData>) this.GetCustomAttributesData();
      }
    }

    /// <summary>
    ///   Возвращает все пользовательские атрибуты, определенные для этого параметра.
    /// </summary>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    ///    См. заметки.
    /// </param>
    /// <returns>
    ///   Массив, содержащий настраиваемые атрибуты, примененные к данному параметру.
    /// </returns>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual object[] GetCustomAttributes(bool inherit)
    {
      return EmptyArray<object>.Value;
    }

    /// <summary>
    ///   Возвращает настраиваемые атрибуты заданного типа или его производных типов, которые применяются к данному параметру.
    /// </summary>
    /// <param name="attributeType">
    ///   Пользовательские атрибуты, идентифицируемые по типу.
    /// </param>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    ///    См. заметки.
    /// </param>
    /// <returns>
    ///   Массив, содержащий настраиваемые атрибуты указанного типа или его производных типов.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип должен быть типом, предоставленным базовой системой среды выполнения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      return EmptyArray<object>.Value;
    }

    /// <summary>
    ///   Определяет, применяется ли пользовательский атрибут указанного типа или его производных типов к данному параметру.
    /// </summary>
    /// <param name="attributeType">
    ///   <see langword="Type" /> Объект для поиска.
    /// </param>
    /// <param name="inherit">
    ///   Данный аргумент не учитывается для объектов этого типа.
    ///    См. заметки.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если один или несколько экземпляров <paramref name="attributeType" /> или его производных типов применяются к данному параметру; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="attributeType" /> не <see cref="T:System.Type" /> объекта, предоставляемого средой CLR.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual bool IsDefined(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException(nameof (attributeType));
      return false;
    }

    /// <summary>
    ///   Возвращает список <see cref="T:System.Reflection.CustomAttributeData" /> объекты для текущего параметра, который может использоваться в контексте только для отражения.
    /// </summary>
    /// <returns>
    ///   Универсальный список <see cref="T:System.Reflection.CustomAttributeData" /> объекты, представляющие данные об атрибутах, которые были применены к текущим параметром.
    /// </returns>
    public virtual IList<CustomAttributeData> GetCustomAttributesData()
    {
      throw new NotImplementedException();
    }

    void _ParameterInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _ParameterInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _ParameterInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _ParameterInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает реальный объект, который необходимо десериализовать, вместо объекта, задаваемого сериализованным потоком.
    /// </summary>
    /// <param name="context">
    ///   Сериализованный поток, из которого десериализуется текущий объект.
    /// </param>
    /// <returns>Реальный объект, который помещается в граф.</returns>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Позиция параметра в списке параметров связанного с ним члена не является допустимым для типа этого члена.
    /// </exception>
    [SecurityCritical]
    public object GetRealObject(StreamingContext context)
    {
      if (this.MemberImpl == (MemberInfo) null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
      switch (this.MemberImpl.MemberType)
      {
        case MemberTypes.Constructor:
        case MemberTypes.Method:
          if (this.PositionImpl == -1)
          {
            if (this.MemberImpl.MemberType == MemberTypes.Method)
              return (object) ((MethodInfo) this.MemberImpl).ReturnParameter;
            throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
          }
          ParameterInfo[] parametersNoCopy1 = ((MethodBase) this.MemberImpl).GetParametersNoCopy();
          if (parametersNoCopy1 != null && this.PositionImpl < parametersNoCopy1.Length)
            return (object) parametersNoCopy1[this.PositionImpl];
          throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
        case MemberTypes.Property:
          ParameterInfo[] parametersNoCopy2 = ((RuntimePropertyInfo) this.MemberImpl).GetIndexParametersNoCopy();
          if (parametersNoCopy2 != null && this.PositionImpl > -1 && this.PositionImpl < parametersNoCopy2.Length)
            return (object) parametersNoCopy2[this.PositionImpl];
          throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
        default:
          throw new SerializationException(Environment.GetResourceString("Serialization_NoParameterInfo"));
      }
    }
  }
}
