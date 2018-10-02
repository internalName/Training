// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ITypeLibImporterNotifySink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет механизм обратного вызова для преобразователя библиотеки типов, сообщающий вызывающему объекту состояние преобразования, а также включать вызывающий объект в процесс преобразования.
  /// </summary>
  [Guid("F1C3BF76-C3E4-11d3-88E7-00902754C43A")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  public interface ITypeLibImporterNotifySink
  {
    /// <summary>
    ///   Сообщает вызывающему оператору, что произошло событие во время преобразования библиотеки типов.
    /// </summary>
    /// <param name="eventKind">
    ///   <see cref="T:System.Runtime.InteropServices.ImporterEventKind" /> Значение, указывающее тип события.
    /// </param>
    /// <param name="eventCode">
    ///   Указывает Дополнительные сведения о событии.
    /// </param>
    /// <param name="eventMsg">Сообщение, созданное событием.</param>
    void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg);

    /// <summary>
    ///   Пользователю предлагается разрешить ссылку на другую библиотеку типов.
    /// </summary>
    /// <param name="typeLib">
    ///   Объект, реализующий <see langword="ITypeLib" /> интерфейс, который необходимо разрешить.
    /// </param>
    /// <returns>
    ///   Сборки, соответствующий <paramref name="typeLib" />.
    /// </returns>
    Assembly ResolveRef([MarshalAs(UnmanagedType.Interface)] object typeLib);
  }
}
