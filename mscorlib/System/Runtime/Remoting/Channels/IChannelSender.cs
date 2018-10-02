// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IChannelSender
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Предоставляет требуемые функции и свойства для каналов отправителя.
  /// </summary>
  [ComVisible(true)]
  public interface IChannelSender : IChannel
  {
    /// <summary>
    ///   Возвращает приемник сообщений канала, который доставляет сообщения на указанный URL-адрес или канала объект данных.
    /// </summary>
    /// <param name="url">
    ///   URL-адрес, по которому новый приемник доставляет сообщения.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <param name="remoteChannelData">
    ///   Объект данных канала удаленного узла, по которому новый приемник доставляет сообщения.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <param name="objectURI">
    ///   При возвращении данного метода содержит URI нового приемник сообщений канала, который доставляет сообщения объекту данных канала или указанный URL-адрес.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Приемник сообщений канала, который доставляет сообщения объекту данных канала, или указанный URL-адрес или <see langword="null" /> Если канал не может подключиться к данному каналу.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI);
  }
}
