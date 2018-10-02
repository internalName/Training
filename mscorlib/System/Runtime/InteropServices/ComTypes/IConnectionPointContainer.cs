// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IConnectionPointContainer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Предоставляет управляемое определение <see langword="IConnectionPointContainer" /> интерфейса.
  /// </summary>
  [Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IConnectionPointContainer
  {
    /// <summary>
    ///   Создает перечислитель всех точек соединения, поддерживаемых в объекте подключаемый одной точке соединения на каждый IID.
    /// </summary>
    /// <param name="ppEnum">
    ///   При возвращении данного метода содержит указатель интерфейса перечислителя.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void EnumConnectionPoints(out IEnumConnectionPoints ppEnum);

    /// <summary>
    ///   Запрашивает, доступный для соединения объект, если он содержит точку подключения для определенного IID и если да, возвращает <see langword="IConnectionPoint" /> указатель интерфейса на эту точку подключения.
    /// </summary>
    /// <param name="riid">
    ///   Ссылка на исходящий интерфейс IID, точка соединения которого запрашивается.
    /// </param>
    /// <param name="ppCP">
    ///   При возвращении данного метода содержит точку подключения, управляющую исходящий интерфейс <paramref name="riid" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void FindConnectionPoint([In] ref Guid riid, out IConnectionPoint ppCP);
  }
}
