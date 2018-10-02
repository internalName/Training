// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.IFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Предоставляет функциональные возможности для форматирования сериализованных объектов.
  /// </summary>
  [ComVisible(true)]
  public interface IFormatter
  {
    /// <summary>
    ///   Десериализует данные в предоставленный поток и воспроизводит граф объектов.
    /// </summary>
    /// <param name="serializationStream">
    ///   Поток, содержащий десериализуемые данные.
    /// </param>
    /// <returns>Верхний объект десериализованного графа.</returns>
    object Deserialize(Stream serializationStream);

    /// <summary>
    ///   Сериализует объект или граф объектов с заданным корнем в указанный поток.
    /// </summary>
    /// <param name="serializationStream">
    ///   Поток, в который модуль форматирования помещает сериализованные данные.
    ///    Этот поток можно ссылаться различные резервные хранилища (файлы, сеть, память и т.д.).
    /// </param>
    /// <param name="graph">
    ///   Объект или корень графа объекта для сериализации.
    ///    Все дочерние объекты этого корневого объекта сериализуются автоматически.
    /// </param>
    void Serialize(Stream serializationStream, object graph);

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> используемые текущего модуля форматирования.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> Используемых данного модуля форматирования.
    /// </returns>
    ISurrogateSelector SurrogateSelector { get; set; }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Runtime.Serialization.SerializationBinder" /> выполняет поиск типа во время десериализации.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Serialization.SerializationBinder" /> Выполняет поиск типа во время десериализации.
    /// </returns>
    SerializationBinder Binder { get; set; }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Runtime.Serialization.StreamingContext" /> используется для сериализации и десериализации.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Serialization.StreamingContext" /> Используется для сериализации и десериализации.
    /// </returns>
    StreamingContext Context { get; set; }
  }
}
