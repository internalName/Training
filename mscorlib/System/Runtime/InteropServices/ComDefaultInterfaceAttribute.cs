// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComDefaultInterfaceAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Определяет интерфейс по умолчанию, предоставляемый COM.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ComDefaultInterfaceAttribute : Attribute
  {
    internal Type _val;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ComDefaultInterfaceAttribute" /> с заданным <see cref="T:System.Type" /> объект как интерфейс по умолчанию, предоставляемый для COM.
    /// </summary>
    /// <param name="defaultInterface">
    ///   A <see cref="T:System.Type" /> значение, показывающее интерфейс по умолчанию, предоставляемый для COM.
    /// </param>
    [__DynamicallyInvokable]
    public ComDefaultInterfaceAttribute(Type defaultInterface)
    {
      this._val = defaultInterface;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Type" /> объект, определяющий интерфейс по умолчанию, предоставляемый для COM.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Объект, определяющий интерфейс по умолчанию, предоставляемый для COM.
    /// </returns>
    [__DynamicallyInvokable]
    public Type Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }
  }
}
