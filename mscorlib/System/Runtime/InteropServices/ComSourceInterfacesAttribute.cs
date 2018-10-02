// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComSourceInterfacesAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Определяет список интерфейсов, предоставляемых в виде источников событий COM для класса с атрибутом.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, Inherited = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ComSourceInterfacesAttribute : Attribute
  {
    internal string _val;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> класс с именем события интерфейса источника.
    /// </summary>
    /// <param name="sourceInterfaces">
    ///   Список имен полное событий исходного интерфейса, разделенных нулями.
    /// </param>
    [__DynamicallyInvokable]
    public ComSourceInterfacesAttribute(string sourceInterfaces)
    {
      this._val = sourceInterfaces;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> класса типа для использования в качестве исходного интерфейса.
    /// </summary>
    /// <param name="sourceInterface">
    ///   <see cref="T:System.Type" /> Исходного интерфейса.
    /// </param>
    [__DynamicallyInvokable]
    public ComSourceInterfacesAttribute(Type sourceInterface)
    {
      this._val = sourceInterface.FullName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> класса типов для использования в качестве интерфейсов источника.
    /// </summary>
    /// <param name="sourceInterface1">
    ///   <see cref="T:System.Type" /> Исходного интерфейса по умолчанию.
    /// </param>
    /// <param name="sourceInterface2">
    ///   <see cref="T:System.Type" /> Исходного интерфейса.
    /// </param>
    [__DynamicallyInvokable]
    public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2)
    {
      this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="ComSourceInterfacesAttribute" /> класса типов для использования в качестве интерфейсов источника.
    /// </summary>
    /// <param name="sourceInterface1">
    ///   <see cref="T:System.Type" /> Исходного интерфейса по умолчанию.
    /// </param>
    /// <param name="sourceInterface2">
    ///   <see cref="T:System.Type" /> Исходного интерфейса.
    /// </param>
    /// <param name="sourceInterface3">
    ///   <see cref="T:System.Type" /> Исходного интерфейса.
    /// </param>
    [__DynamicallyInvokable]
    public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3)
    {
      this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName + "\0" + sourceInterface3.FullName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> класса типов для использования в качестве интерфейсов источника.
    /// </summary>
    /// <param name="sourceInterface1">
    ///   <see cref="T:System.Type" /> Исходного интерфейса по умолчанию.
    /// </param>
    /// <param name="sourceInterface2">
    ///   <see cref="T:System.Type" /> Исходного интерфейса.
    /// </param>
    /// <param name="sourceInterface3">
    ///   <see cref="T:System.Type" /> Исходного интерфейса.
    /// </param>
    /// <param name="sourceInterface4">
    ///   <see cref="T:System.Type" /> Исходного интерфейса.
    /// </param>
    [__DynamicallyInvokable]
    public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3, Type sourceInterface4)
    {
      this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName + "\0" + sourceInterface3.FullName + "\0" + sourceInterface4.FullName;
    }

    /// <summary>Возвращает полное имя интерфейса-источника событий.</summary>
    /// <returns>Полное имя интерфейса-источника событий.</returns>
    [__DynamicallyInvokable]
    public string Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }
  }
}
