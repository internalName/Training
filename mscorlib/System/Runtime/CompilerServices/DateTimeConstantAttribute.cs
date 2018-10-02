// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.DateTimeConstantAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Сохраняет 8-байтовое <see cref="T:System.DateTime" /> константы для поля или параметра.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DateTimeConstantAttribute : CustomConstantAttribute
  {
    private DateTime date;

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="DateTimeConstantAttribute" /> класса количество 100-наносекундных тактов, представляющее дату и время создания данного экземпляра.
    /// </summary>
    /// <param name="ticks">
    ///   Число 100-наносекундных тактов, представляющие дату и время создания данного экземпляра.
    /// </param>
    [__DynamicallyInvokable]
    public DateTimeConstantAttribute(long ticks)
    {
      this.date = new DateTime(ticks);
    }

    /// <summary>
    ///   Возвращает число 100-наносекундных тактов, представляющее дату и время создания данного экземпляра.
    /// </summary>
    /// <returns>
    ///   Число 100-наносекундных тактов, представляющие дату и время создания данного экземпляра.
    /// </returns>
    [__DynamicallyInvokable]
    public override object Value
    {
      [__DynamicallyInvokable] get
      {
        return (object) this.date;
      }
    }

    internal static DateTime GetRawDateTimeConstant(CustomAttributeData attr)
    {
      foreach (CustomAttributeNamedArgument namedArgument in (IEnumerable<CustomAttributeNamedArgument>) attr.NamedArguments)
      {
        if (namedArgument.MemberInfo.Name.Equals("Value"))
          return new DateTime((long) namedArgument.TypedValue.Value);
      }
      return new DateTime((long) attr.ConstructorArguments[0].Value);
    }
  }
}
