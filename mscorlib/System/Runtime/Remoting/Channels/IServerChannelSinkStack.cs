// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IServerChannelSinkStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Обеспечивает функционирование стека для стека канала сервера приемников.
  /// </summary>
  [ComVisible(true)]
  public interface IServerChannelSinkStack : IServerResponseChannelSinkStack
  {
    /// <summary>
    ///   Переносит указанный приемник и сведения, связанные с ним, в стек приемников.
    /// </summary>
    /// <param name="sink">
    ///   Приемник, который необходимо поместить в стек приемников.
    /// </param>
    /// <param name="state">
    ///   Сведения, созданные на стороне запроса, которые требуются на стороне ответа.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    void Push(IServerChannelSink sink, object state);

    /// <summary>
    ///   Выводит сведения, связанные со всеми приемниками: от стека приемников до и включая указанный приемник.
    /// </summary>
    /// <param name="sink">
    ///   Приемник, удаляемый и возвращаемый из стека приемников.
    /// </param>
    /// <returns>
    ///   Сведения, созданные на стороне запроса и связанные с указанным приемником.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    object Pop(IServerChannelSink sink);

    /// <summary>
    ///   Сохраняет приемник сообщений и связанное с ним состояние для последующей асинхронной обработки.
    /// </summary>
    /// <param name="sink">Приемник канала сервера.</param>
    /// <param name="state">
    ///   Состояние, связанное с <paramref name="sink" />.
    /// </param>
    [SecurityCritical]
    void Store(IServerChannelSink sink, object state);

    /// <summary>
    ///   Сохраняет приемник сообщений и связанное состояние, а затем отправляет сообщение асинхронно с помощью только что сохраненного приемника и других сохраненных приемников.
    /// </summary>
    /// <param name="sink">Приемник канала сервера.</param>
    /// <param name="state">
    ///   Состояние, связанное с <paramref name="sink" />.
    /// </param>
    [SecurityCritical]
    void StoreAndDispatch(IServerChannelSink sink, object state);

    /// <summary>
    ///   Представляет обратный вызов делегата для обработки обратного вызова после асинхронно отправлено сообщение.
    /// </summary>
    /// <param name="ar">
    ///   Статус и состояние асинхронной операции для удаленного объекта.
    /// </param>
    [SecurityCritical]
    void ServerCallback(IAsyncResult ar);
  }
}
