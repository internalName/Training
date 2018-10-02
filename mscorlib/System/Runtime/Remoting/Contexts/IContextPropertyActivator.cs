// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IContextPropertyActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>
  ///   Указывает, что реализующее свойство заинтересовано в участии в активации и могло не предоставить приемник сообщений.
  /// </summary>
  [ComVisible(true)]
  public interface IContextPropertyActivator
  {
    /// <summary>
    ///   Указывает, является ли он все права для активации типа объекта, указанного в <paramref name="msg" /> параметре.
    /// </summary>
    /// <param name="msg">
    ///   Объект <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.
    /// </param>
    /// <returns>
    ///   Логическое значение, указывающее, можно ли активировать запрошенного типа.
    /// </returns>
    [SecurityCritical]
    bool IsOKToActivate(IConstructionCallMessage msg);

    /// <summary>
    ///   Вызывается на каждом клиентском контекстном свойстве, имеющим данный интерфейс, прежде чем запрос на конструирование покидает клиента.
    /// </summary>
    /// <param name="msg">
    ///   Объект <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.
    /// </param>
    [SecurityCritical]
    void CollectFromClientContext(IConstructionCallMessage msg);

    /// <summary>
    ///   Вызывается на каждом клиентском контекстном свойстве, имеющим данный интерфейс, когда запрос на конструирование возвращается клиенту с сервера.
    /// </summary>
    /// <param name="msg">
    ///   Объект <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" /> в случае успешного выполнения; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecurityCritical]
    bool DeliverClientContextToServerContext(IConstructionCallMessage msg);

    /// <summary>
    ///   Вызывается в каждом серверном контекстном свойстве, имеющим данный интерфейс, перед ответ конструирования отправляется от сервера клиенту.
    /// </summary>
    /// <param name="msg">
    ///   Объект <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />.
    /// </param>
    [SecurityCritical]
    void CollectFromServerContext(IConstructionReturnMessage msg);

    /// <summary>
    ///   Вызывается на каждом клиентском контекстном свойстве, имеющим данный интерфейс, когда запрос на конструирование возвращается клиенту с сервера.
    /// </summary>
    /// <param name="msg">
    ///   Объект <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" /> в случае успешного выполнения; в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecurityCritical]
    bool DeliverServerContextToClientContext(IConstructionReturnMessage msg);
  }
}
