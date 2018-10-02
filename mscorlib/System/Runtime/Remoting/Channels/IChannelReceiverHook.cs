// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IChannelReceiverHook
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Указывает, что требуется подключить к службе внешнего прослушивателя используемого канала.
  /// </summary>
  [ComVisible(true)]
  public interface IChannelReceiverHook
  {
    /// <summary>Получает тип прослушивателя для обработки.</summary>
    /// <returns>
    ///   Тип прослушивателя для обработки (например, «http»).
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    string ChannelScheme { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, является ли <see cref="T:System.Runtime.Remoting.Channels.IChannelReceiverHook" /> должен вызываться службой внешнего слушателя.
    /// </summary>
    /// <returns>
    ///   Логическое значение, указывающее, является ли <see cref="T:System.Runtime.Remoting.Channels.IChannelReceiverHook" /> должен вызываться службой внешнего слушателя.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    bool WantsToListen { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает цепочку приемников канала, используемая текущим каналом.
    /// </summary>
    /// <returns>Цепочка приемников канала, текущим каналом.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    IServerChannelSink ChannelSinkChain { [SecurityCritical] get; }

    /// <summary>
    ///   Добавляет URI, на котором ловушка канала осуществляет прослушивание.
    /// </summary>
    /// <param name="channelUri">
    ///   URI, на котором ловушка канала осуществляет прослушивание.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void AddHookChannelUri(string channelUri);
  }
}
