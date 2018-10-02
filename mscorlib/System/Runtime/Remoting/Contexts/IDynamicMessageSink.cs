// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IDynamicMessageSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>
  ///   Указывает, что реализующий приемник сообщений будет предоставлен динамически регистрируемыми свойствами.
  /// </summary>
  [ComVisible(true)]
  public interface IDynamicMessageSink
  {
    /// <summary>Указывает, что вызов.</summary>
    /// <param name="reqMsg">Сообщение запроса.</param>
    /// <param name="bCliSide">
    ///   Значение <see langword="true" /> если метод вызван на клиентской стороне и <see langword="false" /> Если используется метод на стороне сервера.
    /// </param>
    /// <param name="bAsync">
    ///   Значение <see langword="true" /> Если это вызов асинхронным и <see langword="false" /> Если это синхронный вызов.
    /// </param>
    [SecurityCritical]
    void ProcessMessageStart(IMessage reqMsg, bool bCliSide, bool bAsync);

    /// <summary>Указывает, что вызов возвращается.</summary>
    /// <param name="replyMsg">Ответное сообщение.</param>
    /// <param name="bCliSide">
    ///   Значение <see langword="true" /> если метод вызывается на стороне клиента и <see langword="false" /> при вызове на стороне сервера.
    /// </param>
    /// <param name="bAsync">
    ///   Значение <see langword="true" /> Если это вызов асинхронным и <see langword="false" /> Если это синхронный вызов.
    /// </param>
    [SecurityCritical]
    void ProcessMessageFinish(IMessage replyMsg, bool bCliSide, bool bAsync);
  }
}
