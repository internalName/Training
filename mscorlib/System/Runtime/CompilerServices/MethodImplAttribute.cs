// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.MethodImplAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Сообщает подробные сведения о реализации метода.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class MethodImplAttribute : Attribute
  {
    internal MethodImplOptions _val;
    /// <summary>
    ///   A <see cref="T:System.Runtime.CompilerServices.MethodCodeType" /> значение, указывающее, какого вида реализация предоставляется для этого метода.
    /// </summary>
    public MethodCodeType MethodCodeType;

    internal MethodImplAttribute(MethodImplAttributes methodImplAttributes)
    {
      MethodImplOptions methodImplOptions = MethodImplOptions.Unmanaged | MethodImplOptions.ForwardRef | MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall | MethodImplOptions.Synchronized | MethodImplOptions.NoInlining | MethodImplOptions.AggressiveInlining | MethodImplOptions.NoOptimization;
      this._val = (MethodImplOptions) (methodImplAttributes & (MethodImplAttributes) methodImplOptions);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> заданным значением <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" />.
    /// </summary>
    /// <param name="methodImplOptions">
    ///   A <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> значение, указывающее свойства метод с атрибутом.
    /// </param>
    [__DynamicallyInvokable]
    public MethodImplAttribute(MethodImplOptions methodImplOptions)
    {
      this._val = methodImplOptions;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> заданным значением <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" />.
    /// </summary>
    /// <param name="value">
    ///   Битовая маска, представляющая требуемый <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> значение, которое задает свойства метод с атрибутом.
    /// </param>
    public MethodImplAttribute(short value)
    {
      this._val = (MethodImplOptions) value;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" />.
    /// </summary>
    public MethodImplAttribute()
    {
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> значение, описывающее метод с атрибутами.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> Значение, описывающее метод с атрибутами.
    /// </returns>
    [__DynamicallyInvokable]
    public MethodImplOptions Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }
  }
}
