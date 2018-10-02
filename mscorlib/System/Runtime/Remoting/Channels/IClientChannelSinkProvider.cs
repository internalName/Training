// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IClientChannelSinkProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Создает клиентские приемники канала для канала клиента, через которые передаются сообщения.
  /// </summary>
  [ComVisible(true)]
  public interface IClientChannelSinkProvider
  {
    /// <summary>Создает цепочку приемников.</summary>
    /// <param name="channel">
    ///   Канал, для которого создается текущий цепочку приемников.
    /// </param>
    /// <param name="url">
    ///   URL-адрес объекта подключения.
    ///    Этот параметр может иметь <see langword="null" /> Если подключение полностью основано на информации, содержащейся в <paramref name="remoteChannelData" /> параметр.
    /// </param>
    /// <param name="remoteChannelData">
    ///   Объект данных канала, описывающий канал на удаленном сервере.
    /// </param>
    /// <returns>
    ///   Первый приемник заново сформированной цепи приемников, или <see langword="null" />, который указывает, что этот поставщик не будет или не может обеспечить подключение для этой конечной точки.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData);

    /// <summary>
    ///   Возвращает или задает следующего поставщика приемников в цепи поставщиков приемников канала.
    /// </summary>
    /// <returns>
    ///   Следующего поставщика приемников в цепи поставщиков приемников канала.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    IClientChannelSinkProvider Next { [SecurityCritical] get; [SecurityCritical] set; }
  }
}
