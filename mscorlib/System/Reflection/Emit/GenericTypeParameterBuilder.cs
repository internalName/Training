// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.GenericTypeParameterBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Определяет и создает параметры универсального типа для динамически определяемых универсальных типов и методов.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class GenericTypeParameterBuilder : TypeInfo
  {
    internal TypeBuilder m_type;

    /// <summary>
    ///   Вызывает исключение <see cref="T:System.NotSupportedException" /> во всех случаях.
    /// </summary>
    /// <param name="typeInfo">Объект для тестирования.</param>
    /// <returns>
    ///   Вызывает исключение <see cref="T:System.NotSupportedException" /> во всех случаях.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override bool IsAssignableFrom(TypeInfo typeInfo)
    {
      if ((Type) typeInfo == (Type) null)
        return false;
      return this.IsAssignableFrom(typeInfo.AsType());
    }

    internal GenericTypeParameterBuilder(TypeBuilder type)
    {
      this.m_type = type;
    }

    /// <summary>
    ///   Возвращает строковое представление текущего параметра универсального типа.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая имя параметра универсального типа.
    /// </returns>
    public override string ToString()
    {
      return this.m_type.Name;
    }

    /// <summary>
    ///   Проверяет, является ли указанный объект экземпляром <see langword="EventToken" /> и равен ли он текущему экземпляру.
    /// </summary>
    /// <param name="o">
    ///   Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   Возвращает значение <see langword="true" />, если <paramref name="o" /> является экземпляром <see langword="EventToken" /> и равен текущему экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      GenericTypeParameterBuilder parameterBuilder = o as GenericTypeParameterBuilder;
      if ((Type) parameterBuilder == (Type) null)
        return false;
      return parameterBuilder.m_type == this.m_type;
    }

    /// <summary>
    ///   Возвращает хэш-код в виде 32-разрядного целого числа для текущего экземпляра.
    /// </summary>
    /// <returns>Хэш-код в виде 32-разрядного целого числа.</returns>
    public override int GetHashCode()
    {
      return this.m_type.GetHashCode();
    }

    /// <summary>
    ///   Возвращает определение универсального типа или определение универсального метода, которому принадлежит параметр универсального типа.
    /// </summary>
    /// <returns>
    ///   Если параметр типа относится к универсальному типу, объект <see cref="T:System.Type" />, представляющий этот универсальный тип. Если параметр типа относится к универсальному методу, объект <see cref="T:System.Type" />, представляющий тип, который объявил этот универсальный метод.
    /// </returns>
    public override Type DeclaringType
    {
      get
      {
        return this.m_type.DeclaringType;
      }
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Type" />, который использовался для получения <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, который использовался для получения <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.
    /// </returns>
    public override Type ReflectedType
    {
      get
      {
        return this.m_type.ReflectedType;
      }
    }

    /// <summary>Возвращает имя универсального параметра типа.</summary>
    /// <returns>Имя универсального параметра типа.</returns>
    public override string Name
    {
      get
      {
        return this.m_type.Name;
      }
    }

    /// <summary>
    ///   Возвращает динамический модуль, содержащий параметр универсального типа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Module" />, который представляет динамический модуль, содержащий параметр универсального типа.
    /// </returns>
    public override Module Module
    {
      get
      {
        return this.m_type.Module;
      }
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_type.MetadataTokenInternal;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, который представляет указатель на текущий параметр универсального типа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, который представляет указатель на текущий параметр универсального типа.
    /// </returns>
    public override Type MakePointerType()
    {
      return SymbolType.FormCompoundType("*".ToCharArray(), (Type) this, 0);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, который представляет текущий параметр универсального типа, когда выполняется передача в качестве параметра ссылки.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, который представляет текущий параметр универсального типа, когда выполняется передача в качестве параметра ссылки.
    /// </returns>
    public override Type MakeByRefType()
    {
      return SymbolType.FormCompoundType("&".ToCharArray(), (Type) this, 0);
    }

    /// <summary>
    ///   Возвращает тип одномерного массива, тип элементов которого является параметром универсального типа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, представляющий тип одномерного массива, тип элементов которого является параметром универсального типа.
    /// </returns>
    public override Type MakeArrayType()
    {
      return SymbolType.FormCompoundType("[]".ToCharArray(), (Type) this, 0);
    }

    /// <summary>
    ///   Возвращает тип массива, тип элементов которого является параметром универсального типа с указанным количеством измерений.
    /// </summary>
    /// <param name="rank">Размерность массива.</param>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, представляющий тип массива, тип элементов которого является параметром универсального типа с указанным количеством измерений.
    /// </returns>
    /// <exception cref="T:System.IndexOutOfRangeException">
    ///   <paramref name="rank" /> не является допустимым количеством измерений.
    ///    Например, его значение меньше 1.
    /// </exception>
    public override Type MakeArrayType(int rank)
    {
      if (rank <= 0)
        throw new IndexOutOfRangeException();
      string str = "";
      if (rank == 1)
      {
        str = "*";
      }
      else
      {
        for (int index = 1; index < rank; ++index)
          str += ",";
      }
      return (Type) (SymbolType.FormCompoundType(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "[{0}]", (object) str).ToCharArray(), (Type) this, 0) as SymbolType);
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override Guid GUID
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="name">Не поддерживается.</param>
    /// <param name="invokeAttr">Не поддерживается.</param>
    /// <param name="binder">Не поддерживается.</param>
    /// <param name="target">Не поддерживается.</param>
    /// <param name="args">Не поддерживается.</param>
    /// <param name="modifiers">Не поддерживается.</param>
    /// <param name="culture">Не поддерживается.</param>
    /// <param name="namedParameters">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Reflection.Assembly" />, представляющий динамическую сборку, содержащую определение универсального типа, которой принадлежит текущий параметр типа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Assembly" />, представляющий динамическую сборку, содержащую определение универсального типа, которой принадлежит текущий параметр типа.
    /// </returns>
    public override Assembly Assembly
    {
      get
      {
        return this.m_type.Assembly;
      }
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    /// <summary>
    ///   Получает значение <see langword="null" /> во всех случаях.
    /// </summary>
    /// <returns>
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) во всех случаях.
    /// </returns>
    public override string FullName
    {
      get
      {
        return (string) null;
      }
    }

    /// <summary>
    ///   Получает значение <see langword="null" /> во всех случаях.
    /// </summary>
    /// <returns>
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) во всех случаях.
    /// </returns>
    public override string Namespace
    {
      get
      {
        return (string) null;
      }
    }

    /// <summary>
    ///   Получает значение <see langword="null" /> во всех случаях.
    /// </summary>
    /// <returns>
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) во всех случаях.
    /// </returns>
    public override string AssemblyQualifiedName
    {
      get
      {
        return (string) null;
      }
    }

    /// <summary>
    ///   Возвращает ограничение базового типа для текущего параметра универсального типа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, представляющий ограничение базового типа для параметра универсального типа, или <see langword="null" />, если параметр типа не имеет ограничения базового типа.
    /// </returns>
    public override Type BaseType
    {
      get
      {
        return this.m_type.BaseType;
      }
    }

    protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="bindingAttr">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    [ComVisible(true)]
    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="bindingAttr">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="name">Не поддерживается.</param>
    /// <param name="bindingAttr">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="bindingAttr">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="name">Имя интерфейса.</param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" /> для поиска без учета регистра; значение <see langword="false" /> для поиска с учетом регистра.
    /// </param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override Type GetInterface(string name, bool ignoreCase)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override Type[] GetInterfaces()
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="name">Не поддерживается.</param>
    /// <param name="bindingAttr">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override EventInfo[] GetEvents()
    {
      throw new NotSupportedException();
    }

    protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="bindingAttr">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="bindingAttr">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="name">Не поддерживается.</param>
    /// <param name="bindingAttr">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override Type GetNestedType(string name, BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="name">Не поддерживается.</param>
    /// <param name="type">Не поддерживается.</param>
    /// <param name="bindingAttr">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="interfaceType">
    ///   Объект <see cref="T:System.Type" />, представляющий тип интерфейса, для которого будет извлекаться сопоставление.
    /// </param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    [ComVisible(true)]
    public override InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="bindingAttr">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="bindingAttr">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    protected override TypeAttributes GetAttributeFlagsImpl()
    {
      return TypeAttributes.Public;
    }

    protected override bool IsArrayImpl()
    {
      return false;
    }

    protected override bool IsByRefImpl()
    {
      return false;
    }

    protected override bool IsPointerImpl()
    {
      return false;
    }

    protected override bool IsPrimitiveImpl()
    {
      return false;
    }

    protected override bool IsCOMObjectImpl()
    {
      return false;
    }

    /// <summary>
    ///   Во всех случаях вызывает исключение <see cref="T:System.NotSupportedException" />.
    /// </summary>
    /// <returns>
    ///   Тип, на который ссылается текущий тип массива, тип указателя или тип <see langword="ByRef" />; или значение <see langword="null" />, если текущий тип не является типом массива, типом указателя и не передается по ссылке.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override Type GetElementType()
    {
      throw new NotSupportedException();
    }

    protected override bool HasElementTypeImpl()
    {
      return false;
    }

    /// <summary>Возвращает текущий параметр универсального типа.</summary>
    /// <returns>
    ///   Текущий объект <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.
    /// </returns>
    public override Type UnderlyingSystemType
    {
      get
      {
        return (Type) this;
      }
    }

    /// <summary>Не допустимо для параметров универсального типа.</summary>
    /// <returns>Не допустимо для параметров универсального типа.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Во всех случаях.
    /// </exception>
    public override Type[] GetGenericArguments()
    {
      throw new InvalidOperationException();
    }

    /// <summary>
    ///   Получает значение <see langword="false" /> во всех случаях.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="false" /> во всех случаях.
    /// </returns>
    public override bool IsGenericTypeDefinition
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение <see langword="false" /> во всех случаях.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="false" /> во всех случаях.
    /// </returns>
    public override bool IsGenericType
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Получает значение <see langword="true" /> во всех случаях.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" /> во всех случаях.
    /// </returns>
    public override bool IsGenericParameter
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, представляет ли этот данный объект сконструированный универсальный тип.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если этот объект представляет сконструированный универсальный тип; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsConstructedGenericType
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает позицию параметра типа в списке параметров типа универсального типа или метода, который объявил этот параметр.
    /// </summary>
    /// <returns>
    ///   Позиция параметра типа в списке параметров типа универсального типа или метода, который объявил этот параметр.
    /// </returns>
    public override int GenericParameterPosition
    {
      get
      {
        return this.m_type.GenericParameterPosition;
      }
    }

    /// <summary>
    ///   Получает значение <see langword="true" /> во всех случаях.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" /> во всех случаях.
    /// </returns>
    public override bool ContainsGenericParameters
    {
      get
      {
        return this.m_type.ContainsGenericParameters;
      }
    }

    /// <summary>
    ///   Возвращает сочетание флагов <see cref="T:System.Reflection.GenericParameterAttributes" />, описывающих ковариацию и особые ограничения текущего параметра универсального типа.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание значений, которое описывает ковариацию и особые ограничения текущего параметра универсального типа.
    /// </returns>
    public override GenericParameterAttributes GenericParameterAttributes
    {
      get
      {
        return this.m_type.GenericParameterAttributes;
      }
    }

    /// <summary>
    ///   Возвращает метод <see cref="T:System.Reflection.MethodInfo" />, который представляет объявляемый метод, если текущий <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> представляет параметр типа универсального метода.
    /// </summary>
    /// <returns>
    ///   Метод <see cref="T:System.Reflection.MethodInfo" />, который представляет объявляющий метод, если текущий <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> представляет параметр типа универсального метода; в противном случае —значение <see langword="null" />.
    /// </returns>
    public override MethodBase DeclaringMethod
    {
      get
      {
        return this.m_type.DeclaringMethod;
      }
    }

    /// <summary>Не допустимо для параметров универсального типа.</summary>
    /// <returns>Не допустимо для параметров универсального типа.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Во всех случаях.
    /// </exception>
    public override Type GetGenericTypeDefinition()
    {
      throw new InvalidOperationException();
    }

    /// <summary>
    ///   Недопустимо для неполных параметров универсального типа.
    /// </summary>
    /// <param name="typeArguments">Массив аргументов типа.</param>
    /// <returns>
    ///   Этот метод недопустим для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Во всех случаях.
    /// </exception>
    public override Type MakeGenericType(params Type[] typeArguments)
    {
      throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericTypeDefinition"));
    }

    protected override bool IsValueTypeImpl()
    {
      return false;
    }

    /// <summary>
    ///   Вызывает исключение <see cref="T:System.NotSupportedException" /> во всех случаях.
    /// </summary>
    /// <param name="c">Объект для тестирования.</param>
    /// <returns>
    ///   Вызывает исключение <see cref="T:System.NotSupportedException" /> во всех случаях.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override bool IsAssignableFrom(Type c)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="c">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    [ComVisible(true)]
    public override bool IsSubclassOf(Type c)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск атрибутов в цепочке наследования этого члена.
    /// </param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="attributeType">
    ///   Тип атрибута для поиска.
    ///    Возвращаются только те атрибуты, которые можно назначить этому типу.
    /// </param>
    /// <param name="inherit">
    ///   Указывает, следует ли выполнять поиск атрибутов в цепочке наследования этого члена.
    /// </param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </summary>
    /// <param name="attributeType">Не поддерживается.</param>
    /// <param name="inherit">Не поддерживается.</param>
    /// <returns>
    ///   Не поддерживается для неполных параметров универсального типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Задает настраиваемый атрибут с помощью большого двоичного объекта пользовательских атрибутов.
    /// </summary>
    /// <param name="con">Конструктор настраиваемого атрибута.</param>
    /// <param name="binaryAttribute">
    ///   Большой двоичный объект байтов, представляющий атрибут.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="con" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="binaryAttribute" /> является пустой ссылкой.
    /// </exception>
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      this.m_type.SetGenParamCustomAttribute(con, binaryAttribute);
    }

    /// <summary>
    ///   Задает настраиваемый атрибут с помощью построителя настраиваемых атрибутов.
    /// </summary>
    /// <param name="customBuilder">
    ///   Экземпляр вспомогательного класса, определяющий настраиваемый атрибут.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="customBuilder" /> имеет значение <see langword="null" />.
    /// </exception>
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      this.m_type.SetGenParamCustomAttribute(customBuilder);
    }

    /// <summary>
    ///   Задает базовый тип, который должен наследоваться типом, для подстановки вместо параметра типа.
    /// </summary>
    /// <param name="baseTypeConstraint">
    ///   Тип <see cref="T:System.Type" />, который должен наследоваться любым типом, подставляемым вместо параметра типа.
    /// </param>
    public void SetBaseTypeConstraint(Type baseTypeConstraint)
    {
      this.m_type.CheckContext(new Type[1]
      {
        baseTypeConstraint
      });
      this.m_type.SetParent(baseTypeConstraint);
    }

    /// <summary>
    ///   Задает интерфейсы, которые должны быть реализованы типом для использования вместо параметра типа.
    /// </summary>
    /// <param name="interfaceConstraints">
    ///   Массив объектов <see cref="T:System.Type" />, представляющих интерфейсы, которые должны быть реализованы типом для использования вместо параметра типа.
    /// </param>
    [ComVisible(true)]
    public void SetInterfaceConstraints(params Type[] interfaceConstraints)
    {
      this.m_type.CheckContext(interfaceConstraints);
      this.m_type.SetInterfaces(interfaceConstraints);
    }

    /// <summary>
    ///   Задает характеристики расхождения и особые ограничения универсального параметра, например ограничение конструктора без параметров.
    /// </summary>
    /// <param name="genericParameterAttributes">
    ///   Побитовое сочетание значений <see cref="T:System.Reflection.GenericParameterAttributes" />, представляющих характеристики расхождения и особые ограничения параметра универсального типа.
    /// </param>
    public void SetGenericParameterAttributes(GenericParameterAttributes genericParameterAttributes)
    {
      this.m_type.SetGenParamAttributes(genericParameterAttributes);
    }
  }
}
