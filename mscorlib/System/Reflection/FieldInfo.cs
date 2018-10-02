// Decompiled with JetBrains decompiler
// Type: System.Reflection.FieldInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>
  ///   Обнаруживает атрибуты поля и обеспечивает доступ к его метаданным.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_FieldInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class FieldInfo : MemberInfo, _FieldInfo
  {
    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.FieldInfo" /> для поля, представленного указанным дескриптором.
    /// </summary>
    /// <param name="handle">
    ///   A <see cref="T:System.RuntimeFieldHandle" /> Структура, содержащая дескриптор представления внутренних метаданных поля.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.FieldInfo" /> объект, представляющий полю, указанному в <paramref name="handle" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="handle" /> недопустим.
    /// </exception>
    [__DynamicallyInvokable]
    public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
    {
      if (handle.IsNullHandle())
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
      FieldInfo fieldInfo = RuntimeType.GetFieldInfo(handle.GetRuntimeFieldInfo());
      Type declaringType = fieldInfo.DeclaringType;
      if (declaringType != (Type) null && declaringType.IsGenericType)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_FieldDeclaringTypeGeneric"), (object) fieldInfo.Name, (object) declaringType.GetGenericTypeDefinition()));
      return fieldInfo;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.FieldInfo" /> для поля, представленного заданным дескриптором для заданного универсального типа.
    /// </summary>
    /// <param name="handle">
    ///   A <see cref="T:System.RuntimeFieldHandle" /> Структура, содержащая дескриптор представления внутренних метаданных поля.
    /// </param>
    /// <param name="declaringType">
    ///   A <see cref="T:System.RuntimeTypeHandle" /> Структура, содержащая дескриптор универсального типа, который определяет это поле.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.FieldInfo" /> объект, представляющий полю, указанному в <paramref name="handle" />, в универсальный тип, указанный в <paramref name="declaringType" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="handle" /> недопустим.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="declaringType" /> несовместим с параметром <paramref name="handle" />.
    ///    Например <paramref name="declaringType" /> является дескриптор типа среды выполнения определения универсального типа и <paramref name="handle" /> поступает из сконструированного типа.
    ///    См. заметки.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
    {
      if (handle.IsNullHandle())
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
      return RuntimeType.GetFieldInfo(declaringType.GetRuntimeType(), handle.GetRuntimeFieldInfo());
    }

    /// <summary>
    ///   Определение равенства двух объектов <see cref="T:System.Reflection.FieldInfo" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(FieldInfo left, FieldInfo right)
    {
      if ((object) left == (object) right)
        return true;
      if ((object) left == null || (object) right == null || (left is RuntimeFieldInfo || right is RuntimeFieldInfo))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>
    ///   Определяет неравенство двух объектов <see cref="T:System.Reflection.FieldInfo" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="left" /> и <paramref name="right" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(FieldInfo left, FieldInfo right)
    {
      return !(left == right);
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим экземпляром, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="obj" /> равно типу и значению данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>
    ///   Возвращает значение <see cref="T:System.Reflection.MemberTypes" />, указывающее, что этот элемент является полем.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.Reflection.MemberTypes" />, указывающее, что этот элемент является полем.
    /// </returns>
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Field;
      }
    }

    /// <summary>
    ///   Возвращает массив типов, определяющих обязательные настраиваемые модификаторы для свойства.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, которые указывают обязательные настраиваемые модификаторы для текущего свойства, например <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.
    /// </returns>
    public virtual Type[] GetRequiredCustomModifiers()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает массив типов, определяющих необязательные настраиваемые модификаторы для поля.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, которые указывают дополнительные настраиваемые модификаторы для текущего поля, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" />.
    /// </returns>
    public virtual Type[] GetOptionalCustomModifiers()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Задает значение поля, которое поддерживается указанным объектом.
    /// </summary>
    /// <param name="obj">
    ///   Структура <see cref="T:System.TypedReference" />, которая инкапсулирует управляемый указатель на местоположение и представление типа среды выполнения (может храниться в этом расположении).
    /// </param>
    /// <param name="value">Значение, присваиваемое этому полю.</param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Для вызывающего объекта требуется альтернатива спецификации CLS, но вызван этот метод.
    /// </exception>
    [CLSCompliant(false)]
    public virtual void SetValueDirect(TypedReference obj, object value)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_AbstractNonCLS"));
    }

    /// <summary>
    ///   Возвращает значение поля, поддерживаемое данным объектом.
    /// </summary>
    /// <param name="obj">
    ///   A <see cref="T:System.TypedReference" /> Структура, инкапсулирующая управляемый указатель на местоположение и представление времени выполнения типа, который может храниться в этом месте.
    /// </param>
    /// <returns>
    ///   <see langword="Object" /> Содержащий значение поля.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывающий объект требует альтернатива общеязыковой спецификацией (CLS), но взамен вызывает этот метод.
    /// </exception>
    [CLSCompliant(false)]
    public virtual object GetValueDirect(TypedReference obj)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_AbstractNonCLS"));
    }

    /// <summary>
    ///   Возвращает <see langword="RuntimeFieldHandle" />, являющийся дескриптором представления внутренних метаданных поля.
    /// </summary>
    /// <returns>
    ///   Дескриптор представления внутренних метаданных поля.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract RuntimeFieldHandle FieldHandle { [__DynamicallyInvokable] get; }

    /// <summary>Возвращает тип этого объекта поля.</summary>
    /// <returns>Тип этого объекта поля.</returns>
    [__DynamicallyInvokable]
    public abstract Type FieldType { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   При переопределении в производном классе возвращает значение поля, поддерживаемое данным объектом.
    /// </summary>
    /// <param name="obj">
    ///   Объект, значение поля которого будет возвращено.
    /// </param>
    /// <returns>
    ///   Объект, содержащий значение поля, отраженное этим экземпляром.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение <see cref="T:System.Exception" />.
    /// 
    ///   Поле не является статическим, а параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Поле помечено как литерал, однако для него не задан допустимый тип литерала.
    /// </exception>
    /// <exception cref="T:System.FieldAccessException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.MemberAccessException" />.
    /// 
    ///   Вызывающий объект не имеет разрешения на доступ к этому полю.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Этот метод не объявлен в классе <paramref name="obj" /> и не унаследован таким классом.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract object GetValue(object obj);

    /// <summary>
    ///   Метод возвращает литеральное значение, связанное с этим свойством компилятором.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Object" /> — содержит литеральное значение, связанное с этим полем.
    ///    Если значение литерала является типом класса и при этом значение элемента равно нулю, возвращается значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Таблица констант в неуправляемых метаданных не содержит значение константы для текущего поля.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Тип значения не является одним из типов, разрешенных спецификацией CLS.
    ///    См. спецификацию ECMA, раздел II, логический формат метаданных (другие структуры и типы элементов, используемые в сигнатурах).
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Значение константы для поля не задано.
    /// </exception>
    public virtual object GetRawConstantValue()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_AbstractNonCLS"));
    }

    /// <summary>
    ///   При переопределении в производном классе задает значение поля, поддерживаемое данным объектом.
    /// </summary>
    /// <param name="obj">
    ///   Объект, значение поля которого будет установлено.
    /// </param>
    /// <param name="value">Значение, присваиваемое полю.</param>
    /// <param name="invokeAttr">
    ///   Поле <see langword="Binder" /> указывающий тип связывания (например, <see langword="Binder.CreateInstance" /> или <see langword="Binder.ExactBinding" />).
    /// </param>
    /// <param name="binder">
    ///   Набор свойств, который допускает привязку, приведение типов аргументов и вызов членов с помощью отражения.
    ///    Если <paramref name="binder" /> является <see langword="null" />, затем <see langword="Binder.DefaultBinding" /> используется.
    /// </param>
    /// <param name="culture">
    ///   Программные настройки конкретного языка и региональных параметров.
    /// </param>
    /// <exception cref="T:System.FieldAccessException">
    ///   Вызывающий объект не имеет разрешения на доступ к этому полю.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetException">
    ///   <paramref name="obj" /> Параметр <see langword="null" /> и поле является полем экземпляра.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Поле не существует в объекте.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> Параметр нельзя преобразованы и сохранены в поле.
    /// </exception>
    public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

    /// <summary>Возвращает атрибуты, связанные с этим полем.</summary>
    /// <returns>
    ///   <see langword="FieldAttributes" /> Для этого поля.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract FieldAttributes Attributes { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Задает значение поля, которое поддерживается указанным объектом.
    /// </summary>
    /// <param name="obj">
    ///   Объект, значение поля которого будет указано.
    /// </param>
    /// <param name="value">Значение, присваиваемое этому полю.</param>
    /// <exception cref="T:System.FieldAccessException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.MemberAccessException" />.
    /// 
    ///   Вызывающий объект не имеет разрешения на доступ к этому полю.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение <see cref="T:System.Exception" />.
    /// 
    ///   Параметр<paramref name="obj" /> имеет значение <see langword="null" />, а поле является полем экземпляра.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Это поле не существует в объекте.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="value" /> невозможно преобразовать и сохранить в поле.
    /// </exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public void SetValue(object obj, object value)
    {
      this.SetValue(obj, value, BindingFlags.Default, Type.DefaultBinder, (CultureInfo) null);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли поле открытым.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если это поле является открытым; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsPublic
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли поле закрытым.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле является закрытым; в противном случае; <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsPrivate
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее ли видимость этого поля описывается <see cref="F:System.Reflection.FieldAttributes.Family" />; это поле является видимым только в своем классе и производных классов.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если доступ к этому полю точно описываемых <see cref="F:System.Reflection.FieldAttributes.Family" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsFamily
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее ли потенциальные видимость этого поля описывается <see cref="F:System.Reflection.FieldAttributes.Assembly" />; то есть поля полностью доступно для других типов в одной сборке и является невидимым для производных типов за пределами сборки.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если видимость этого поля точно описываемых <see cref="F:System.Reflection.FieldAttributes.Assembly" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее ли видимость этого поля описывается <see cref="F:System.Reflection.FieldAttributes.FamANDAssem" />; то есть поле можно получить доступ из производных классов, но только в том случае, если они находятся в той же сборке.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если доступ к этому полю точно описываемых <see cref="F:System.Reflection.FieldAttributes.FamANDAssem" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsFamilyAndAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamANDAssem;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее ли потенциальные видимость этого поля описывается <see cref="F:System.Reflection.FieldAttributes.FamORAssem" />; то есть поле может осуществляться производными классами, независимо от их типа и классами в той же сборке.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если доступ к этому полю точно описываемых <see cref="F:System.Reflection.FieldAttributes.FamORAssem" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsFamilyOrAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamORAssem;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли поле статическим.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если это поле является статическим. в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsStatic
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & FieldAttributes.Static) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли поле можно задать только в теле конструктора.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле содержит <see langword="InitOnly" /> атрибута задано; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsInitOnly
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & FieldAttributes.InitOnly) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, записывается ли значение на время компиляции и не может быть изменено.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле содержит <see langword="Literal" /> атрибута задано; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsLiteral
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & FieldAttributes.Literal) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, имеет ли это поле <see langword="NotSerialized" /> атрибута.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле содержит <see langword="NotSerialized" /> атрибута задано; в противном случае — <see langword="false" />.
    /// </returns>
    public bool IsNotSerialized
    {
      get
      {
        return (uint) (this.Attributes & FieldAttributes.NotSerialized) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее ли соответствующий <see langword="SpecialName" /> установлен в <see cref="T:System.Reflection.FieldAttributes" /> перечислителя.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see langword="SpecialName" /> установлен в <see cref="T:System.Reflection.FieldAttributes" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsSpecialName
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & FieldAttributes.SpecialName) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее ли соответствующий <see langword="PinvokeImpl" /> установлен в <see cref="T:System.Reflection.FieldAttributes" />.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see langword="PinvokeImpl" /> установлен в <see cref="T:System.Reflection.FieldAttributes" />; в противном случае — <see langword="false" />.
    /// </returns>
    public bool IsPinvokeImpl
    {
      get
      {
        return (uint) (this.Attributes & FieldAttributes.PinvokeImpl) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущее поле критически важным для безопасности или защищенным критически важным для безопасности на текущем уровне доверия.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если текущее поле является критически важным для безопасности или защищенным критически важным для безопасности на текущем уровне доверия; <see langword="false" /> если он является прозрачным.
    /// </returns>
    public virtual bool IsSecurityCritical
    {
      get
      {
        return this.FieldHandle.IsSecurityCritical();
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущее поле защищенным критически важным для безопасности на текущем уровне доверия.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если текущее поле является защищенным критически важным для безопасности на текущем уровне доверия; <see langword="false" /> если он является критически важным для безопасности или прозрачным.
    /// </returns>
    public virtual bool IsSecuritySafeCritical
    {
      get
      {
        return this.FieldHandle.IsSecuritySafeCritical();
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий поле прозрачным на текущем уровне доверия.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если поле является прозрачным на текущем уровне доверия; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsSecurityTransparent
    {
      get
      {
        return this.FieldHandle.IsSecurityTransparent();
      }
    }

    Type _FieldInfo.GetType()
    {
      return this.GetType();
    }

    void _FieldInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _FieldInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _FieldInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _FieldInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
