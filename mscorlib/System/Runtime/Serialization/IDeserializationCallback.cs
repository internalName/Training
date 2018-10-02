// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.IDeserializationCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Указывает класс должен уведомляться при завершении десериализации всего графа объектов.
  ///    Обратите внимание, что этот интерфейс не вызывается при десериализации с помощью XmlSerializer (System.Xml.Serialization.XmlSerializer).
  /// </summary>
  [ComVisible(true)]
  public interface IDeserializationCallback
  {
    /// <summary>
    ///   Выполняется, когда полностью десериализован граф объектов.
    /// </summary>
    /// <param name="sender">
    ///   Объект, который инициализирует обратный вызов.
    ///    Данная функциональная возможность для этого параметра в настоящее время не реализуется.
    /// </param>
    void OnDeserialization(object sender);
  }
}
