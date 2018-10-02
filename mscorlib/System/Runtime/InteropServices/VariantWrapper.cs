// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.VariantWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Упаковывает данные типа <see langword="VT_VARIANT | VT_BYREF" /> из управляемого в неуправляемый код.
  ///    Этот класс не наследуется.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class VariantWrapper
  {
    private object m_WrappedObject;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> для указанного <see cref="T:System.Object" /> параметр.
    /// </summary>
    /// <param name="obj">Объект для маршалинга.</param>
    [__DynamicallyInvokable]
    public VariantWrapper(object obj)
    {
      this.m_WrappedObject = obj;
    }

    /// <summary>
    ///   Возвращает объект, перезаписанный <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект, перезаписанный <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public object WrappedObject
    {
      [__DynamicallyInvokable] get
      {
        return this.m_WrappedObject;
      }
    }
  }
}
