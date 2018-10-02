// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIConnectionPointContainer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.IConnectionPointContainer" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IConnectionPointContainer instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIConnectionPointContainer
  {
    /// <summary>
    ///   Создает перечислитель всех точек соединения, поддерживаемых в объекте подключаемый одной точке соединения на каждый IID.
    /// </summary>
    /// <param name="ppEnum">
    ///   При удачном возвращении содержит указатель интерфейса перечислителя.
    /// </param>
    void EnumConnectionPoints(out UCOMIEnumConnectionPoints ppEnum);

    /// <summary>
    ///   Запрашивает, доступный для соединения объект, если он содержит точку подключения для определенного IID и если да, возвращает <see langword="IConnectionPoint" /> указатель интерфейса на эту точку подключения.
    /// </summary>
    /// <param name="riid">
    ///   Ссылка на исходящий интерфейс IID, точка соединения которого запрашивается.
    /// </param>
    /// <param name="ppCP">
    ///   При удачном возвращении содержит точку подключения, которая управляет исходящий интерфейс <paramref name="riid" />.
    /// </param>
    void FindConnectionPoint(ref Guid riid, out UCOMIConnectionPoint ppCP);
  }
}
