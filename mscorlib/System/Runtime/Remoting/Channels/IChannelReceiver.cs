// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IChannelReceiver
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Предоставляет требуемые функции и свойства для каналов получателя.
  /// </summary>
  [ComVisible(true)]
  public interface IChannelReceiver : IChannel
  {
    /// <summary>Возвращает данные, относящиеся к каналу.</summary>
    /// <returns>Данные канала.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    object ChannelData { [SecurityCritical] get; }

    /// <summary>Возвращает массив всех URL-адресов для URI.</summary>
    /// <param name="objectURI">
    ///   URI, для которого требуются URL-адреса.
    /// </param>
    /// <returns>Массив URL-адресов.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    string[] GetUrlsForUri(string objectURI);

    /// <summary>
    ///   Указывает, что текущий канал начинает прослушивать запросы.
    /// </summary>
    /// <param name="data">
    ///   Дополнительные сведения об инициализации.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void StartListening(object data);

    /// <summary>
    ///   Указывает, что текущий канал остановка прослушивания запросов.
    /// </summary>
    /// <param name="data">
    ///   Дополнительные сведения о состоянии канала.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void StopListening(object data);
  }
}
