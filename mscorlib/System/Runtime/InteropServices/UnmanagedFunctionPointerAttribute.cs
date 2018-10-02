// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Управляет поведением при маршалинге сигнатуры делегата, передаваемой как указатель неуправляемой функции в неуправляемый код или из него.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class UnmanagedFunctionPointerAttribute : Attribute
  {
    private CallingConvention m_callingConvention;
    /// <summary>
    ///   Указывает способ маршалинга параметров строки для метода и управляет искажением имени.
    /// </summary>
    [__DynamicallyInvokable]
    public CharSet CharSet;
    /// <summary>
    ///   Включает или отключает поведение наилучшего сопоставления при преобразовании знаков Юникода в символы ANSI.
    /// </summary>
    [__DynamicallyInvokable]
    public bool BestFitMapping;
    /// <summary>
    ///   Включает или отключает возникновение исключения при появлении несопоставимого символа Юникода, который преобразуется в символ ANSI «?» символов.
    /// </summary>
    [__DynamicallyInvokable]
    public bool ThrowOnUnmappableChar;
    /// <summary>
    ///   Указывает, вызывает ли <see langword="SetLastError" /> функции Win32 API перед возвращением из метода, использующего атрибуты.
    /// </summary>
    [__DynamicallyInvokable]
    public bool SetLastError;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute" /> класса указанное соглашение о вызовах.
    /// </summary>
    /// <param name="callingConvention">Соглашение о вызовах.</param>
    [__DynamicallyInvokable]
    public UnmanagedFunctionPointerAttribute(CallingConvention callingConvention)
    {
      this.m_callingConvention = callingConvention;
    }

    /// <summary>Возвращает значение соглашения о вызове.</summary>
    /// <returns>
    ///   Соглашение о вызовах, определяемое значение <see cref="M:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute.#ctor(System.Runtime.InteropServices.CallingConvention)" /> конструктор.
    /// </returns>
    [__DynamicallyInvokable]
    public CallingConvention CallingConvention
    {
      [__DynamicallyInvokable] get
      {
        return this.m_callingConvention;
      }
    }
  }
}
