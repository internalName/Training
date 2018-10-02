// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.ISoapMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>
  ///   Предоставляет интерфейс для объекта, который содержит имена и типы параметров, необходимых при сериализации SOAP RPC (удаленный вызов процедуры).
  /// </summary>
  [ComVisible(true)]
  public interface ISoapMessage
  {
    /// <summary>
    ///   Возвращает или задает имена параметров вызова метода.
    /// </summary>
    /// <returns>Имена параметров вызова метода.</returns>
    string[] ParamNames { get; set; }

    /// <summary>
    ///   Возвращает или задает значения параметров вызова метода.
    /// </summary>
    /// <returns>Значения параметров вызова метода.</returns>
    object[] ParamValues { get; set; }

    /// <summary>
    ///   Возвращает или задает типы параметров вызова метода.
    /// </summary>
    /// <returns>Типы параметров вызова метода.</returns>
    Type[] ParamTypes { get; set; }

    /// <summary>Возвращает или задает имя вызванного метода.</summary>
    /// <returns>Имя вызванного метода.</returns>
    string MethodName { get; set; }

    /// <summary>
    ///   Возвращает или задает пространство имен XML SOAP RPC (удаленный вызов процедуры) <see cref="P:System.Runtime.Serialization.Formatters.ISoapMessage.MethodName" /> элемента.
    /// </summary>
    /// <returns>
    ///   Имя пространства имен XML, где находится объект, содержащий вызванный метод.
    /// </returns>
    string XmlNameSpace { get; set; }

    /// <summary>
    ///   Возвращает или задает данные по каналу для вызова метода.
    /// </summary>
    /// <returns>Данные по каналу вызова метода.</returns>
    Header[] Headers { get; set; }
  }
}
