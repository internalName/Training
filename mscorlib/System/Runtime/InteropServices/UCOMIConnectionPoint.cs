// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIConnectionPoint
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.IConnectionPoint" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IConnectionPoint instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIConnectionPoint
  {
    /// <summary>
    ///   Возвращает IID выходящего интерфейса, управляемого с использованием этой точки подключения.
    /// </summary>
    /// <param name="pIID">
    ///   При удачном возвращении содержит IID выходящего интерфейса, управляемого с использованием этой точки подключения.
    /// </param>
    void GetConnectionInterface(out Guid pIID);

    /// <summary>
    ///   Извлекает <see langword="IConnectionPointContainer" /> указатель интерфейса на доступный для соединения объект, которому принадлежит эта точка соединения.
    /// </summary>
    /// <param name="ppCPC">
    ///   При удачном возвращении содержит доступный для соединения объект <see langword="IConnectionPointContainer" /> интерфейса.
    /// </param>
    void GetConnectionPointContainer(out UCOMIConnectionPointContainer ppCPC);

    /// <summary>
    ///   Устанавливает вспомогательное соединение между точкой подключения и объектом приемника вызывающего.
    /// </summary>
    /// <param name="pUnkSink">
    ///   Ссылка на приемник для получения вызовов выходящего интерфейса, управляемого с использованием этой точки подключения.
    /// </param>
    /// <param name="pdwCookie">
    ///   При удачном возвращении содержит файл cookie подключения.
    /// </param>
    void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

    /// <summary>
    ///   Завершает вспомогательное соединение, установленное ранее при помощи <see cref="M:System.Runtime.InteropServices.UCOMIConnectionPoint.Advise(System.Object,System.Int32@)" />.
    /// </summary>
    /// <param name="dwCookie">
    ///   Файл cookie подключения, ранее возвращенный из <see cref="M:System.Runtime.InteropServices.UCOMIConnectionPoint.Advise(System.Object,System.Int32@)" />.
    /// </param>
    void Unadvise(int dwCookie);

    /// <summary>
    ///   Создает объект перечислителя для перебора соединений, существующих для этой точки подключения.
    /// </summary>
    /// <param name="ppEnum">
    ///   При удачном возвращении содержит созданный перечислитель.
    /// </param>
    void EnumConnections(out UCOMIEnumConnections ppEnum);
  }
}
