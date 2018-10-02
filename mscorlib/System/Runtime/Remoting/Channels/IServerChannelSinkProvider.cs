// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IServerChannelSinkProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Создает сервер приемники канала для канала сервера, через которые передаются сообщения.
  /// </summary>
  [ComVisible(true)]
  public interface IServerChannelSinkProvider
  {
    /// <summary>
    ///   Возвращает данные канала для канала, связанный с текущим приемником.
    /// </summary>
    /// <param name="channelData">
    ///   Объект <see cref="T:System.Runtime.Remoting.Channels.IChannelDataStore" /> объект, в котором должны быть возвращены данные канала.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void GetChannelData(IChannelDataStore channelData);

    /// <summary>Создает цепочку приемников.</summary>
    /// <param name="channel">
    ///   Канал, для которого необходимо создать цепочку приемников канала.
    /// </param>
    /// <returns>
    ///   Первый приемник заново сформированной цепи приемников, или <see langword="null" />, который указывает, что этот поставщик не будет или не может обеспечить подключение для этой конечной точки.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    IServerChannelSink CreateSink(IChannelReceiver channel);

    /// <summary>
    ///   Возвращает или задает следующего поставщика приемников в цепи поставщиков приемников канала.
    /// </summary>
    /// <returns>
    ///   Следующего поставщика приемников в цепи поставщиков приемников канала.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    IServerChannelSinkProvider Next { [SecurityCritical] get; [SecurityCritical] set; }
  }
}
