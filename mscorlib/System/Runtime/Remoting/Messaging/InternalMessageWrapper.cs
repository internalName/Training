// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.InternalMessageWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Создает оболочку для передачи данных удаленного взаимодействия между приемниками сообщений, либо для запросов от клиента к серверу, либо для последующих ответов.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  public class InternalMessageWrapper
  {
    /// <summary>
    ///   Представляет запрос или ответ <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" /> интерфейс, который является оболочкой для оболочки сообщения.
    /// </summary>
    protected IMessage WrappedMessage;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Messaging.InternalMessageWrapper" />.
    /// </summary>
    /// <param name="msg">
    ///   Сообщение, действующее как исходящий вызов метода удаленного объекта, либо как последующий ответ.
    /// </param>
    public InternalMessageWrapper(IMessage msg)
    {
      this.WrappedMessage = msg;
    }

    [SecurityCritical]
    internal object GetIdentityObject()
    {
      IInternalMessage wrappedMessage = this.WrappedMessage as IInternalMessage;
      if (wrappedMessage != null)
        return (object) wrappedMessage.IdentityObject;
      return (this.WrappedMessage as InternalMessageWrapper)?.GetIdentityObject();
    }

    [SecurityCritical]
    internal object GetServerIdentityObject()
    {
      IInternalMessage wrappedMessage = this.WrappedMessage as IInternalMessage;
      if (wrappedMessage != null)
        return (object) wrappedMessage.ServerIdentityObject;
      return (this.WrappedMessage as InternalMessageWrapper)?.GetServerIdentityObject();
    }
  }
}
