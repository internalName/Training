// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ITransportHeaders
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Хранит коллекцию заголовков, используемых в канале приемников.
  /// </summary>
  [ComVisible(true)]
  public interface ITransportHeaders
  {
    /// <summary>
    ///   Возвращает или задает транспортный заголовок, связанный с указанным ключом.
    /// </summary>
    /// <param name="key">
    ///   Ключ запрошенный транспортный заголовок, связанный с.
    /// </param>
    /// <returns>
    ///   Транспортный заголовок, связанный с указанным ключом.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    object this[object key] { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IEnumerator" /> проходящий по всем элементам <see cref="T:System.Runtime.Remoting.Channels.ITransportHeaders" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Collections.IEnumerator" /> который перебирает все операции в <see cref="T:System.Runtime.Remoting.Channels.ITransportHeaders" /> объекта.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    IEnumerator GetEnumerator();
  }
}
