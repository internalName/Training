// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IRemotingFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Предоставляет интерфейс удаленной процедуры вызова (RPC) для всех модулей форматирования.
  /// </summary>
  [ComVisible(true)]
  public interface IRemotingFormatter : IFormatter
  {
    /// <summary>
    ///   Начинает процесс десериализации удаленного вызова процедур (RPC).
    /// </summary>
    /// <param name="serializationStream">
    ///   <see cref="T:System.IO.Stream" /> Из которого десериализуется данные.
    /// </param>
    /// <param name="handler">
    ///   Делегат, предназначенный для обработки <see cref="T:System.Runtime.Remoting.Messaging.Header" /> объектов.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <returns>Корень десериализованного графа объекта.</returns>
    object Deserialize(Stream serializationStream, HeaderHandler handler);

    /// <summary>
    ///   Запускает процесс сериализации удаленного вызова процедур (RPC).
    /// </summary>
    /// <param name="serializationStream">
    ///   <see cref="T:System.IO.Stream" /> На который сериализуется заданный граф.
    /// </param>
    /// <param name="graph">Корень графа объекта для сериализации.</param>
    /// <param name="headers">
    ///   Массив <see cref="T:System.Runtime.Remoting.Messaging.Header" /> объектов для передачи с диаграммой, определяемое <paramref name="graph" /> параметр.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    void Serialize(Stream serializationStream, object graph, Header[] headers);
  }
}
