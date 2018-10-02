// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.IDispatchConstantAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает, что значение по умолчанию для поля атрибута или параметра экземпляра <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />, где <see cref="P:System.Runtime.InteropServices.DispatchWrapper.WrappedObject" /> — <see langword="null" />.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class IDispatchConstantAttribute : CustomConstantAttribute
  {
    /// <summary>
    ///   Возвращает <see langword="IDispatch" /> константу, хранящуюся в данном атрибуте.
    /// </summary>
    /// <returns>
    ///   <see langword="IDispatch" /> Константу, хранящуюся в данном атрибуте.
    ///    Только <see langword="null" /> допустим для <see langword="IDispatch" /> постоянное значение.
    /// </returns>
    public override object Value
    {
      get
      {
        return (object) new DispatchWrapper((object) null);
      }
    }
  }
}
