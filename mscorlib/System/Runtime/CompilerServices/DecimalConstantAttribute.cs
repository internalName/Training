// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.DecimalConstantAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Сохраняет значение <see cref="T:System.Decimal" /> константы в метаданных.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DecimalConstantAttribute : Attribute
  {
    private Decimal dec;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.CompilerServices.DecimalConstantAttribute" /> класса со значениями указанного целого числа без знака.
    /// </summary>
    /// <param name="scale">
    ///   Степень числа 10, масштабирование коэффициент, на который указывает число цифр справа от десятичной запятой.
    ///    Допустимые значения: от 0 до 28 включительно.
    /// </param>
    /// <param name="sign">
    ///   Значение 0 указывает на положительное значение, а значение 1 указывает на отрицательное значение.
    /// </param>
    /// <param name="hi">
    ///   Старшие 32 разряда 96-разрядного <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.
    /// </param>
    /// <param name="mid">
    ///   Средние 32 разряда 96-разрядного <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.
    /// </param>
    /// <param name="low">
    ///   Младшие 32 разряда 96-разрядного <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="scale" /> &gt; 28.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public DecimalConstantAttribute(byte scale, byte sign, uint hi, uint mid, uint low)
    {
      this.dec = new Decimal((int) low, (int) mid, (int) hi, sign > (byte) 0, scale);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.CompilerServices.DecimalConstantAttribute" /> с заданным подписью целочисленных значений.
    /// </summary>
    /// <param name="scale">
    ///   Степень числа 10, масштабирование коэффициент, на который указывает число цифр справа от десятичной запятой.
    ///    Допустимые значения: от 0 до 28 включительно.
    /// </param>
    /// <param name="sign">
    ///   Значение 0 указывает на положительное значение, а значение 1 указывает на отрицательное значение.
    /// </param>
    /// <param name="hi">
    ///   Старшие 32 разряда 96-разрядного <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.
    /// </param>
    /// <param name="mid">
    ///   Средние 32 разряда 96-разрядного <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.
    /// </param>
    /// <param name="low">
    ///   Младшие 32 разряда 96-разрядного <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.
    /// </param>
    [__DynamicallyInvokable]
    public DecimalConstantAttribute(byte scale, byte sign, int hi, int mid, int low)
    {
      this.dec = new Decimal(low, mid, hi, sign > (byte) 0, scale);
    }

    /// <summary>
    ///   Возвращает десятичную константу, хранящуюся в данном атрибуте.
    /// </summary>
    /// <returns>Десятичная константа, хранящаяся в данном атрибуте.</returns>
    [__DynamicallyInvokable]
    public Decimal Value
    {
      [__DynamicallyInvokable] get
      {
        return this.dec;
      }
    }

    internal static Decimal GetRawDecimalConstant(CustomAttributeData attr)
    {
      foreach (CustomAttributeNamedArgument namedArgument in (IEnumerable<CustomAttributeNamedArgument>) attr.NamedArguments)
      {
        if (namedArgument.MemberInfo.Name.Equals("Value"))
          return (Decimal) namedArgument.TypedValue.Value;
      }
      ParameterInfo[] parameters = attr.Constructor.GetParameters();
      IList<CustomAttributeTypedArgument> constructorArguments = attr.ConstructorArguments;
      if (parameters[2].ParameterType == typeof (uint))
      {
        CustomAttributeTypedArgument attributeTypedArgument = constructorArguments[4];
        int lo = (int) (uint) attributeTypedArgument.Value;
        attributeTypedArgument = constructorArguments[3];
        int mid = (int) (uint) attributeTypedArgument.Value;
        attributeTypedArgument = constructorArguments[2];
        int hi = (int) (uint) attributeTypedArgument.Value;
        attributeTypedArgument = constructorArguments[1];
        byte num = (byte) attributeTypedArgument.Value;
        attributeTypedArgument = constructorArguments[0];
        byte scale = (byte) attributeTypedArgument.Value;
        return new Decimal(lo, mid, hi, num > (byte) 0, scale);
      }
      CustomAttributeTypedArgument attributeTypedArgument1 = constructorArguments[4];
      int lo1 = (int) attributeTypedArgument1.Value;
      attributeTypedArgument1 = constructorArguments[3];
      int mid1 = (int) attributeTypedArgument1.Value;
      attributeTypedArgument1 = constructorArguments[2];
      int hi1 = (int) attributeTypedArgument1.Value;
      attributeTypedArgument1 = constructorArguments[1];
      byte num1 = (byte) attributeTypedArgument1.Value;
      attributeTypedArgument1 = constructorArguments[0];
      byte scale1 = (byte) attributeTypedArgument1.Value;
      return new Decimal(lo1, mid1, hi1, num1 > (byte) 0, scale1);
    }
  }
}
