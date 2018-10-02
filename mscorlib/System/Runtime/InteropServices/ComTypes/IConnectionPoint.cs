// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IConnectionPoint
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Предоставляет управляемое определение <see langword="IConnectionPoint" /> интерфейса.
  /// </summary>
  [Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IConnectionPoint
  {
    /// <summary>
    ///   Возвращает IID выходящего интерфейса, управляемого с использованием этой точки подключения.
    /// </summary>
    /// <param name="pIID">
    ///   При возвращении этого параметра содержит IID выходящего интерфейса, управляемого с использованием этой точки подключения.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetConnectionInterface(out Guid pIID);

    /// <summary>
    ///   Извлекает <see langword="IConnectionPointContainer" /> указатель интерфейса на доступный для соединения объект, которому принадлежит эта точка соединения.
    /// </summary>
    /// <param name="ppCPC">
    ///   При возвращении этого параметра содержит доступный для соединения объект <see langword="IConnectionPointContainer" /> интерфейса.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetConnectionPointContainer(out IConnectionPointContainer ppCPC);

    /// <summary>
    ///   Устанавливает вспомогательное соединение между точкой подключения и объектом приемника вызывающего.
    /// </summary>
    /// <param name="pUnkSink">
    ///   Ссылка на приемник для получения вызовов выходящего интерфейса, управляемого с использованием этой точки подключения.
    /// </param>
    /// <param name="pdwCookie">
    ///   При возвращении данного метода содержит cookie соединения.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

    /// <summary>
    ///   Завершает вспомогательное соединение, установленное ранее при помощи <see cref="M:System.Runtime.InteropServices.ComTypes.IConnectionPoint.Advise(System.Object,System.Int32@)" /> метод.
    /// </summary>
    /// <param name="dwCookie">
    ///   Файл cookie подключения, ранее возвращенный из <see cref="M:System.Runtime.InteropServices.ComTypes.IConnectionPoint.Advise(System.Object,System.Int32@)" /> метода.
    /// </param>
    [__DynamicallyInvokable]
    void Unadvise(int dwCookie);

    /// <summary>
    ///   Создает объект перечислителя для перебора соединений, существующих для этой точки подключения.
    /// </summary>
    /// <param name="ppEnum">
    ///   При возвращении данного метода содержит только что созданный перечислитель.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void EnumConnections(out IEnumConnections ppEnum);
  }
}
