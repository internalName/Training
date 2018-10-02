// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UnknownWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Инкапсулирует объекты, которые необходимо маршалировать, как <see langword="VT_UNKNOWN" />.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class UnknownWrapper
  {
    private object m_WrappedObject;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.UnknownWrapper" /> класса Инкапсулируемый объект.
    /// </summary>
    /// <param name="obj">Инкапсулируемый объект.</param>
    [__DynamicallyInvokable]
    public UnknownWrapper(object obj)
    {
      this.m_WrappedObject = obj;
    }

    /// <summary>Возвращает объект, содержащийся в эту оболочку.</summary>
    /// <returns>Объект оболочки.</returns>
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
