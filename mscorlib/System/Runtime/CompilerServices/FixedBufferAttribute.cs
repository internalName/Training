// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.FixedBufferAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает, что поле должно считаться содержащим фиксированное число элементов указанного типа-примитива.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class FixedBufferAttribute : Attribute
  {
    private Type elementType;
    private int length;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.FixedBufferAttribute" />.
    /// </summary>
    /// <param name="elementType">
    ///   Тип элементов, содержащихся в буфере.
    /// </param>
    /// <param name="length">Количество элементов в буфере.</param>
    [__DynamicallyInvokable]
    public FixedBufferAttribute(Type elementType, int length)
    {
      this.elementType = elementType;
      this.length = length;
    }

    /// <summary>
    ///   Возвращает тип элементов, содержащихся в фиксированном буфере.
    /// </summary>
    /// <returns>Тип элементов.</returns>
    [__DynamicallyInvokable]
    public Type ElementType
    {
      [__DynamicallyInvokable] get
      {
        return this.elementType;
      }
    }

    /// <summary>Возвращает число элементов в фиксированном буфере.</summary>
    /// <returns>Число элементов в фиксированном буфере.</returns>
    [__DynamicallyInvokable]
    public int Length
    {
      [__DynamicallyInvokable] get
      {
        return this.length;
      }
    }
  }
}
