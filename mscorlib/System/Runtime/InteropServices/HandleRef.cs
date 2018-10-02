// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.HandleRef
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Инкапсулирует управляемый объект, который содержит дескриптор для ресурса (передается в неуправляемый код с помощью вызова платформы).
  /// </summary>
  [ComVisible(true)]
  public struct HandleRef
  {
    internal object m_wrapper;
    internal IntPtr m_handle;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.HandleRef" /> с объектом для перезаписи и дескриптор для ресурса при помощи неуправляемого кода.
    /// </summary>
    /// <param name="wrapper">
    ///   Возвращает управляемый объект, который не следует завершать до вызова платформы.
    /// </param>
    /// <param name="handle">
    ///   <see cref="T:System.IntPtr" /> Указывает дескриптор для ресурса.
    /// </param>
    public HandleRef(object wrapper, IntPtr handle)
    {
      this.m_wrapper = wrapper;
      this.m_handle = handle;
    }

    /// <summary>
    ///   Возвращает объект, содержащий дескриптор для ресурса.
    /// </summary>
    /// <returns>Объект, содержащий дескриптор для ресурса.</returns>
    public object Wrapper
    {
      get
      {
        return this.m_wrapper;
      }
    }

    /// <summary>Возвращает дескриптор для ресурса.</summary>
    /// <returns>Дескриптор для ресурса.</returns>
    public IntPtr Handle
    {
      get
      {
        return this.m_handle;
      }
    }

    /// <summary>
    ///   Возвращает дескриптор для ресурса указанного <see cref="T:System.Runtime.InteropServices.HandleRef" /> объекта.
    /// </summary>
    /// <param name="value">Объект для обработки.</param>
    /// <returns>
    ///   Дескриптор для ресурса указанного <see cref="T:System.Runtime.InteropServices.HandleRef" /> объекта.
    /// </returns>
    public static explicit operator IntPtr(HandleRef value)
    {
      return value.m_handle;
    }

    /// <summary>
    ///   Возвращает внутреннее целочисленное представление <see cref="T:System.Runtime.InteropServices.HandleRef" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   A <see cref="T:System.Runtime.InteropServices.HandleRef" /> объекта для извлечения внутреннего целочисленного представления.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.IntPtr" /> Объект, представляющий <see cref="T:System.Runtime.InteropServices.HandleRef" /> объекта.
    /// </returns>
    public static IntPtr ToIntPtr(HandleRef value)
    {
      return value.m_handle;
    }
  }
}
