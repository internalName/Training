// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DispIdAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает идентификатор диспетчера COM (DISPID) для метода, поля или свойства.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DispIdAttribute : Attribute
  {
    internal int _val;

    /// <summary>
    ///   Инициализирует новый экземпляр <see langword="DispIdAttribute" /> класса с указанным идентификатором DISPID.
    /// </summary>
    /// <param name="dispId">Идентификатор DISPID для члена.</param>
    [__DynamicallyInvokable]
    public DispIdAttribute(int dispId)
    {
      this._val = dispId;
    }

    /// <summary>Возвращает идентификатор DISPID для члена.</summary>
    /// <returns>Идентификатор DISPID для члена.</returns>
    [__DynamicallyInvokable]
    public int Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }
  }
}
