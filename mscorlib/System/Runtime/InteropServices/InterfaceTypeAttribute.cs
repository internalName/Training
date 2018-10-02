// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.InterfaceTypeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, является ли управляемый интерфейс сдвоенным, диспетчеризации, или <see langword="IUnknown" /> -только в том случае, если он предоставлен модели COM.
  /// </summary>
  [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class InterfaceTypeAttribute : Attribute
  {
    internal ComInterfaceType _val;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> с заданным <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> член перечисления.
    /// </summary>
    /// <param name="interfaceType">
    ///   Один из <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> значений, описывающее способ предоставления интерфейса для COM-клиентов.
    /// </param>
    [__DynamicallyInvokable]
    public InterfaceTypeAttribute(ComInterfaceType interfaceType)
    {
      this._val = interfaceType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> с заданным <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> член перечисления.
    /// </summary>
    /// <param name="interfaceType">
    ///   Описывает способ предоставления интерфейса для COM-клиентов.
    /// </param>
    [__DynamicallyInvokable]
    public InterfaceTypeAttribute(short interfaceType)
    {
      this._val = (ComInterfaceType) interfaceType;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> значение, описывающее способ предоставления интерфейса COM.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> Значение, описывающее способ предоставления интерфейса COM.
    /// </returns>
    [__DynamicallyInvokable]
    public ComInterfaceType Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }
  }
}
