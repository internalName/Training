// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ITypeLibExporterNotifySink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет механизм обратного вызова для преобразователя сборок, сообщающий вызывающему объекту состояние преобразования, а также включать вызывающий объект в процесс преобразования.
  /// </summary>
  [Guid("F1C3BF77-C3E4-11d3-88E7-00902754C43A")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  public interface ITypeLibExporterNotifySink
  {
    /// <summary>
    ///   Сообщает вызывающему оператору, что во время преобразования сборки произошло событие.
    /// </summary>
    /// <param name="eventKind">
    ///   <see cref="T:System.Runtime.InteropServices.ExporterEventKind" /> Значение, указывающее тип события.
    /// </param>
    /// <param name="eventCode">
    ///   Указывает Дополнительные сведения о событии.
    /// </param>
    /// <param name="eventMsg">Сообщение, созданное событием.</param>
    void ReportEvent(ExporterEventKind eventKind, int eventCode, string eventMsg);

    /// <summary>
    ///   Запрашивает у пользователя разрешение ссылки на другую сборку.
    /// </summary>
    /// <param name="assembly">Для разрешения сборки.</param>
    /// <returns>
    ///   Библиотека типов для <paramref name="assembly" />.
    /// </returns>
    [return: MarshalAs(UnmanagedType.Interface)]
    object ResolveRef(Assembly assembly);
  }
}
