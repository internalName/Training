// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeNamedArgument
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Представляет именованный аргумент настраиваемого атрибута в контексте только для отражения.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct CustomAttributeNamedArgument
  {
    private MemberInfo m_memberInfo;
    private CustomAttributeTypedArgument m_value;

    /// <summary>
    ///   Проверяет равенство двух структур <see cref="T:System.Reflection.CustomAttributeNamedArgument" />.
    /// </summary>
    /// <param name="left">
    ///   Структура, которая находится слева от оператора равенства.
    /// </param>
    /// <param name="right">
    ///   Структура, которая находится справа от оператора равенства.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если обе структуры <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
    {
      return left.Equals((object) right);
    }

    /// <summary>
    ///   Определяет равенство двух структур <see cref="T:System.Reflection.CustomAttributeNamedArgument" />.
    /// </summary>
    /// <param name="left">
    ///   Структура, которая находится слева от оператора неравенства.
    /// </param>
    /// <param name="right">
    ///   Структура, которая находится справа от оператора неравенства.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если две структуры <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> различаются; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
    {
      return !left.Equals((object) right);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> класс, который представляет указанного поля или свойства настраиваемого атрибута и задает значение поля или свойства.
    /// </summary>
    /// <param name="memberInfo">
    ///   Поле или свойство настраиваемого атрибута.
    ///    Новый <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> объектов представляет этот элемент и его значение.
    /// </param>
    /// <param name="value">
    ///   Значение поля или свойства настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="memberInfo" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="memberInfo" /> не является полем или свойством настраиваемого атрибута.
    /// </exception>
    public CustomAttributeNamedArgument(MemberInfo memberInfo, object value)
    {
      if (memberInfo == (MemberInfo) null)
        throw new ArgumentNullException(nameof (memberInfo));
      FieldInfo fieldInfo = memberInfo as FieldInfo;
      PropertyInfo propertyInfo = memberInfo as PropertyInfo;
      Type argumentType;
      if (fieldInfo != (FieldInfo) null)
      {
        argumentType = fieldInfo.FieldType;
      }
      else
      {
        if (!(propertyInfo != (PropertyInfo) null))
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidMemberForNamedArgument"));
        argumentType = propertyInfo.PropertyType;
      }
      this.m_memberInfo = memberInfo;
      this.m_value = new CustomAttributeTypedArgument(argumentType, value);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> класс, который представляет указанного поля или свойства настраиваемого атрибута и указывает <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> объект, который описывает тип и значение поля или свойства.
    /// </summary>
    /// <param name="memberInfo">
    ///   Поле или свойство настраиваемого атрибута.
    ///    Новый <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> объектов представляет этот элемент и его значение.
    /// </param>
    /// <param name="typedArgument">
    ///   Объект, который описывает тип и значение поля или свойства.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="memberInfo" /> имеет значение <see langword="null" />.
    /// </exception>
    public CustomAttributeNamedArgument(MemberInfo memberInfo, CustomAttributeTypedArgument typedArgument)
    {
      if (memberInfo == (MemberInfo) null)
        throw new ArgumentNullException(nameof (memberInfo));
      this.m_memberInfo = memberInfo;
      this.m_value = typedArgument;
    }

    /// <summary>
    ///   Возвращает строку, состоящую из имени аргумента, знака равенства и строкового представления значения аргумента.
    /// </summary>
    /// <returns>
    ///   Строка, состоящая из имени аргумента, знака равенства и строкового представления значения аргумента.
    /// </returns>
    public override string ToString()
    {
      if (this.m_memberInfo == (MemberInfo) null)
        return base.ToString();
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{0} = {1}", (object) this.MemberInfo.Name, (object) this.TypedValue.ToString(this.ArgumentType != typeof (object)));
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
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
    ///   <see langword="true" />, если значение параметра <paramref name="obj" /> равно типу и значению данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      return obj == (ValueType) this;
    }

    internal Type ArgumentType
    {
      get
      {
        if ((object) (this.m_memberInfo as FieldInfo) == null)
          return ((PropertyInfo) this.m_memberInfo).PropertyType;
        return ((FieldInfo) this.m_memberInfo).FieldType;
      }
    }

    /// <summary>
    ///   Возвращает член атрибута, который будет использоваться для установки значения именованного аргумента.
    /// </summary>
    /// <returns>
    ///   Элемент атрибута, который будет использоваться для установки значения именованного аргумента.
    /// </returns>
    public MemberInfo MemberInfo
    {
      get
      {
        return this.m_memberInfo;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> структуры, который можно использовать для получения типу и значению текущего объекта с именем аргумента.
    /// </summary>
    /// <returns>
    ///   Структура, которая может использоваться для получения типу и значению текущего именованного аргумента.
    /// </returns>
    [__DynamicallyInvokable]
    public CustomAttributeTypedArgument TypedValue
    {
      [__DynamicallyInvokable] get
      {
        return this.m_value;
      }
    }

    /// <summary>
    ///   Возвращает имя элемента атрибута, который будет использоваться для установки значения именованного аргумента.
    /// </summary>
    /// <returns>
    ///   Имя атрибута элемента, который будет использоваться для установки значения именованного аргумента.
    /// </returns>
    [__DynamicallyInvokable]
    public string MemberName
    {
      [__DynamicallyInvokable] get
      {
        return this.MemberInfo.Name;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли именованный аргумент полем.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если именованный аргумент представляет собой поле; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsField
    {
      [__DynamicallyInvokable] get
      {
        return this.MemberInfo is FieldInfo;
      }
    }
  }
}
