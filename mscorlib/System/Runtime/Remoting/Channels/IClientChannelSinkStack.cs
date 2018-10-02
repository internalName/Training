// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IClientChannelSinkStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Предоставляет функциональные возможности для стека клиента приемников каналов, который должен вызываться при декодировании асинхронного ответа.
  /// </summary>
  [ComVisible(true)]
  public interface IClientChannelSinkStack : IClientResponseChannelSinkStack
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
    void Push(IClientChannelSink sink, object state);

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
    object Pop(IClientChannelSink sink);
  }
}
