// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.IDispatchImplAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, какой <see langword="IDispatch" /> реализации среды CLR используется при предоставлении сдвоенные интерфейсы и диспетчерские интерфейсы модели COM.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
  [Obsolete("This attribute is deprecated and will be removed in a future version.", false)]
  [ComVisible(true)]
  public sealed class IDispatchImplAttribute : Attribute
  {
    internal IDispatchImplType _val;

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="IDispatchImplAttribute" /> определен класс с <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> значение.
    /// </summary>
    /// <param name="implType">
    ///   Указывает, какой <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> перечисления будет использоваться.
    /// </param>
    public IDispatchImplAttribute(IDispatchImplType implType)
    {
      this._val = implType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="IDispatchImplAttribute" /> определен класс с <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> значение.
    /// </summary>
    /// <param name="implType">
    ///   Указывает, какой <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> перечисления будет использоваться.
    /// </param>
    public IDispatchImplAttribute(short implType)
    {
      this._val = (IDispatchImplType) implType;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> значение, используемое классом.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> Значение, используемое классом.
    /// </returns>
    public IDispatchImplType Value
    {
      get
      {
        return this._val;
      }
    }
  }
}
