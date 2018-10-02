// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComEventInterfaceAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Определяет исходный интерфейс и класс, реализующий методы интерфейса событий, созданного при импорте компонентного класса из библиотеки типов COM.
  /// </summary>
  [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ComEventInterfaceAttribute : Attribute
  {
    internal Type _SourceInterface;
    internal Type _EventProvider;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ComEventInterfaceAttribute" /> класса источника интерфейса и событие класса поставщика.
    /// </summary>
    /// <param name="SourceInterface">
    ///   Объект <see cref="T:System.Type" /> содержащий исходный интерфейс источника из библиотеки типов.
    ///    COM использует этот интерфейс для обратного вызова управляемого класса.
    /// </param>
    /// <param name="EventProvider">
    ///   Объект <see cref="T:System.Type" /> содержащий класс, реализующий методы интерфейса события.
    /// </param>
    [__DynamicallyInvokable]
    public ComEventInterfaceAttribute(Type SourceInterface, Type EventProvider)
    {
      this._SourceInterface = SourceInterface;
      this._EventProvider = EventProvider;
    }

    /// <summary>
    ///   Возвращает исходный интерфейс источника из библиотеки типов.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> содержит интерфейс источника.
    /// </returns>
    [__DynamicallyInvokable]
    public Type SourceInterface
    {
      [__DynamicallyInvokable] get
      {
        return this._SourceInterface;
      }
    }

    /// <summary>
    ///   Возвращает класс, реализующий методы интерфейса события.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> содержащий класс, реализующий методы интерфейса события.
    /// </returns>
    [__DynamicallyInvokable]
    public Type EventProvider
    {
      [__DynamicallyInvokable] get
      {
        return this._EventProvider;
      }
    }
  }
}
