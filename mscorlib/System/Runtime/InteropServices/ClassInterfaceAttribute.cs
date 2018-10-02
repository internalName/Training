// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ClassInterfaceAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, какой тип интерфейса класса должен генерироваться для класса, предоставленного модели COM, если интерфейс создан.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ClassInterfaceAttribute : Attribute
  {
    internal ClassInterfaceType _val;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" /> класса с заданным <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> член перечисления.
    /// </summary>
    /// <param name="classInterfaceType">
    ///   Один из <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> значения, которое описывает тип интерфейса генерируется для класса.
    /// </param>
    [__DynamicallyInvokable]
    public ClassInterfaceAttribute(ClassInterfaceType classInterfaceType)
    {
      this._val = classInterfaceType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" /> с заданным <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> значение перечисления.
    /// </summary>
    /// <param name="classInterfaceType">
    ///   Описывает тип интерфейса, созданного для класса.
    /// </param>
    [__DynamicallyInvokable]
    public ClassInterfaceAttribute(short classInterfaceType)
    {
      this._val = (ClassInterfaceType) classInterfaceType;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> значение, описывающее, какой тип интерфейса следует создать для класса.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> Значение, описывающее, какой тип интерфейса следует создать для класса.
    /// </returns>
    [__DynamicallyInvokable]
    public ClassInterfaceType Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }
  }
}
