// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeTypedArgument
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
  /// <summary>
  ///   Представляет аргумент настраиваемого атрибута в контексте только для отражения или элемент аргумента массива.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct CustomAttributeTypedArgument
  {
    private object m_value;
    private Type m_argumentType;

    /// <summary>
    ///   Проверяет равенство двух структур <see cref="T:System.Reflection.CustomAttributeTypedArgument" />.
    /// </summary>
    /// <param name="left">
    ///   Структура <see cref="T:System.Reflection.CustomAttributeTypedArgument" />, которая находится слева от оператора равенства.
    /// </param>
    /// <param name="right">
    ///   Структура <see cref="T:System.Reflection.CustomAttributeTypedArgument" />, которая находится справа от оператора равенства.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если обе структуры <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
    {
      return left.Equals((object) right);
    }

    /// <summary>
    ///   Определяет равенство двух структур <see cref="T:System.Reflection.CustomAttributeTypedArgument" />.
    /// </summary>
    /// <param name="left">
    ///   Структура <see cref="T:System.Reflection.CustomAttributeTypedArgument" />, которая находится слева от оператора неравенства.
    /// </param>
    /// <param name="right">
    ///   Структура <see cref="T:System.Reflection.CustomAttributeTypedArgument" />, которая находится справа от оператора неравенства.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если две структуры <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> различаются; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
    {
      return !left.Equals((object) right);
    }

    private static Type CustomAttributeEncodingToType(CustomAttributeEncoding encodedType)
    {
      switch (encodedType)
      {
        case CustomAttributeEncoding.Boolean:
          return typeof (bool);
        case CustomAttributeEncoding.Char:
          return typeof (char);
        case CustomAttributeEncoding.SByte:
          return typeof (sbyte);
        case CustomAttributeEncoding.Byte:
          return typeof (byte);
        case CustomAttributeEncoding.Int16:
          return typeof (short);
        case CustomAttributeEncoding.UInt16:
          return typeof (ushort);
        case CustomAttributeEncoding.Int32:
          return typeof (int);
        case CustomAttributeEncoding.UInt32:
          return typeof (uint);
        case CustomAttributeEncoding.Int64:
          return typeof (long);
        case CustomAttributeEncoding.UInt64:
          return typeof (ulong);
        case CustomAttributeEncoding.Float:
          return typeof (float);
        case CustomAttributeEncoding.Double:
          return typeof (double);
        case CustomAttributeEncoding.String:
          return typeof (string);
        case CustomAttributeEncoding.Array:
          return typeof (Array);
        case CustomAttributeEncoding.Type:
          return typeof (Type);
        case CustomAttributeEncoding.Object:
          return typeof (object);
        case CustomAttributeEncoding.Enum:
          return typeof (Enum);
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) encodedType), nameof (encodedType));
      }
    }

    [SecuritySafeCritical]
    private static unsafe object EncodedValueToRawValue(long val, CustomAttributeEncoding encodedType)
    {
      switch (encodedType)
      {
        case CustomAttributeEncoding.Boolean:
          return (object) ((byte) val > (byte) 0);
        case CustomAttributeEncoding.Char:
          return (object) (char) val;
        case CustomAttributeEncoding.SByte:
          return (object) (sbyte) val;
        case CustomAttributeEncoding.Byte:
          return (object) (byte) val;
        case CustomAttributeEncoding.Int16:
          return (object) (short) val;
        case CustomAttributeEncoding.UInt16:
          return (object) (ushort) val;
        case CustomAttributeEncoding.Int32:
          return (object) (int) val;
        case CustomAttributeEncoding.UInt32:
          return (object) (uint) val;
        case CustomAttributeEncoding.Int64:
          return (object) val;
        case CustomAttributeEncoding.UInt64:
          return (object) (ulong) val;
        case CustomAttributeEncoding.Float:
          return (object) *(float*) &val;
        case CustomAttributeEncoding.Double:
          return (object) *(double*) &val;
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) (int) val), nameof (val));
      }
    }

    private static RuntimeType ResolveType(RuntimeModule scope, string typeName)
    {
      RuntimeType nameUsingCaRules = RuntimeTypeHandle.GetTypeByNameUsingCARules(typeName, scope);
      if (nameUsingCaRules == (RuntimeType) null)
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Arg_CATypeResolutionFailed"), (object) typeName));
      return nameUsingCaRules;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> класса с указанным типом и значением.
    /// </summary>
    /// <param name="argumentType">
    ///   Тип аргумента настраиваемого атрибута.
    /// </param>
    /// <param name="value">
    ///   Значение аргумента настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="argumentType" /> имеет значение <see langword="null" />.
    /// </exception>
    public CustomAttributeTypedArgument(Type argumentType, object value)
    {
      if (argumentType == (Type) null)
        throw new ArgumentNullException(nameof (argumentType));
      this.m_value = value == null ? (object) null : CustomAttributeTypedArgument.CanonicalizeValue(value);
      this.m_argumentType = argumentType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> класса с указанным значением.
    /// </summary>
    /// <param name="value">
    ///   Значение аргумента настраиваемого атрибута.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public CustomAttributeTypedArgument(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      this.m_value = CustomAttributeTypedArgument.CanonicalizeValue(value);
      this.m_argumentType = value.GetType();
    }

    private static object CanonicalizeValue(object value)
    {
      if (value.GetType().IsEnum)
        return ((Enum) value).GetValue();
      return value;
    }

    internal CustomAttributeTypedArgument(RuntimeModule scope, CustomAttributeEncodedArgument encodedArg)
    {
      CustomAttributeEncoding encodedType = encodedArg.CustomAttributeType.EncodedType;
      switch (encodedType)
      {
        case CustomAttributeEncoding.Undefined:
          throw new ArgumentException(nameof (encodedArg));
        case CustomAttributeEncoding.String:
          this.m_argumentType = typeof (string);
          this.m_value = (object) encodedArg.StringValue;
          break;
        case CustomAttributeEncoding.Array:
          CustomAttributeEncoding encodedArrayType = encodedArg.CustomAttributeType.EncodedArrayType;
          this.m_argumentType = (encodedArrayType != CustomAttributeEncoding.Enum ? CustomAttributeTypedArgument.CustomAttributeEncodingToType(encodedArrayType) : (Type) CustomAttributeTypedArgument.ResolveType(scope, encodedArg.CustomAttributeType.EnumName)).MakeArrayType();
          if (encodedArg.ArrayValue == null)
          {
            this.m_value = (object) null;
            break;
          }
          CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[encodedArg.ArrayValue.Length];
          for (int index = 0; index < array.Length; ++index)
            array[index] = new CustomAttributeTypedArgument(scope, encodedArg.ArrayValue[index]);
          this.m_value = (object) Array.AsReadOnly<CustomAttributeTypedArgument>(array);
          break;
        case CustomAttributeEncoding.Type:
          this.m_argumentType = typeof (Type);
          this.m_value = (object) null;
          if (encodedArg.StringValue == null)
            break;
          this.m_value = (object) CustomAttributeTypedArgument.ResolveType(scope, encodedArg.StringValue);
          break;
        case CustomAttributeEncoding.Enum:
          RuntimeModule scope1 = scope;
          CustomAttributeType customAttributeType = encodedArg.CustomAttributeType;
          string enumName = customAttributeType.EnumName;
          this.m_argumentType = (Type) CustomAttributeTypedArgument.ResolveType(scope1, enumName);
          long primitiveValue = encodedArg.PrimitiveValue;
          customAttributeType = encodedArg.CustomAttributeType;
          int encodedEnumType = (int) customAttributeType.EncodedEnumType;
          this.m_value = CustomAttributeTypedArgument.EncodedValueToRawValue(primitiveValue, (CustomAttributeEncoding) encodedEnumType);
          break;
        default:
          this.m_argumentType = CustomAttributeTypedArgument.CustomAttributeEncodingToType(encodedType);
          this.m_value = CustomAttributeTypedArgument.EncodedValueToRawValue(encodedArg.PrimitiveValue, encodedType);
          break;
      }
    }

    /// <summary>
    ///   Возвращает строку, состоящую из имени аргумента, знака равенства и строкового представления значения аргумента.
    /// </summary>
    /// <returns>
    ///   Строка, состоящая из имени аргумента, знака равенства и строкового представления значения аргумента.
    /// </returns>
    public override string ToString()
    {
      return this.ToString(false);
    }

    internal string ToString(bool typed)
    {
      if (this.m_argumentType == (Type) null)
        return base.ToString();
      if (this.ArgumentType.IsEnum)
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, (object) this.ArgumentType.FullName);
      if (this.Value == null)
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, typed ? "null" : "({0})null", (object) this.ArgumentType.Name);
      if (this.ArgumentType == typeof (string))
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "\"{0}\"", this.Value);
      if (this.ArgumentType == typeof (char))
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "'{0}'", this.Value);
      if (this.ArgumentType == typeof (Type))
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "typeof({0})", (object) ((Type) this.Value).FullName);
      if (!this.ArgumentType.IsArray)
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, (object) this.ArgumentType.Name);
      string str1 = (string) null;
      IList<CustomAttributeTypedArgument> attributeTypedArgumentList = this.Value as IList<CustomAttributeTypedArgument>;
      Type elementType = this.ArgumentType.GetElementType();
      string str2 = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "new {0}[{1}] {{ ", elementType.IsEnum ? (object) elementType.FullName : (object) elementType.Name, (object) attributeTypedArgumentList.Count);
      for (int index = 0; index < attributeTypedArgumentList.Count; ++index)
        str2 += string.Format((IFormatProvider) CultureInfo.CurrentCulture, index == 0 ? "{0}" : ", {0}", (object) attributeTypedArgumentList[index].ToString(elementType != typeof (object)));
      return str1 = str2 + " }";
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   32-разрядное целое число со знаком, являющееся хэш-кодом для данного экземпляра.
    /// </returns>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>
    ///   Указывает, равен ли этот экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">Другой объект, подлежащий сравнению.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="obj" /> и данный экземпляр относятся к одному типу и представляют одинаковые значения; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      return obj == (ValueType) this;
    }

    /// <summary>
    ///   Возвращает тип аргумента или аргумента элемента массива.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий тип аргумента или элемента массива.
    /// </returns>
    [__DynamicallyInvokable]
    public Type ArgumentType
    {
      [__DynamicallyInvokable] get
      {
        return this.m_argumentType;
      }
    }

    /// <summary>
    ///   Возвращает значение аргумента для простого аргумента или элемента аргумента массива; Возвращает коллекцию значений для аргумента-массива.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий значение аргумента или элемента или универсальный <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> из <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> объектов, представляющих значения аргумента типа массива.
    /// </returns>
    [__DynamicallyInvokable]
    public object Value
    {
      [__DynamicallyInvokable] get
      {
        return this.m_value;
      }
    }
  }
}
