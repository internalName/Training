// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.AsyncResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Инкапсулирует результаты асинхронной операции на делегате.
  /// </summary>
  [ComVisible(true)]
  public class AsyncResult : IAsyncResult, IMessageSink
  {
    private IMessageCtrl _mc;
    private AsyncCallback _acbd;
    private IMessage _replyMsg;
    private bool _isCompleted;
    private bool _endInvokeCalled;
    private ManualResetEvent _AsyncWaitHandle;
    private Delegate _asyncDelegate;
    private object _asyncState;

    [SecurityCritical]
    internal AsyncResult(Message m)
    {
      m.GetAsyncBeginInfo(out this._acbd, out this._asyncState);
      this._asyncDelegate = (Delegate) m.GetThisPtr();
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли сервер завершил вызов.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> После завершения вызова; сервером в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsCompleted
    {
      get
      {
        return this._isCompleted;
      }
    }

    /// <summary>
    ///   Возвращает объект делегата, для которого был вызван асинхронный вызов.
    /// </summary>
    /// <returns>
    ///   Объект делегата, для которого был вызван асинхронный вызов.
    /// </returns>
    public virtual object AsyncDelegate
    {
      get
      {
        return (object) this._asyncDelegate;
      }
    }

    /// <summary>
    ///   Возвращает объект, предоставленный как последний параметр <see langword="BeginInvoke" /> вызова метода.
    /// </summary>
    /// <returns>
    ///   Объект, предоставленный как последний параметр <see langword="BeginInvoke" /> вызова метода.
    /// </returns>
    public virtual object AsyncState
    {
      get
      {
        return this._asyncState;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see langword="BeginInvoke" /> вызова синхронно.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see langword="BeginInvoke" /> Вызов завершен синхронно; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool CompletedSynchronously
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, является ли <see langword="EndInvoke" /> был вызван для текущего <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" />.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see langword="EndInvoke" /> был вызван для текущего <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" />; в противном случае — <see langword="false" />.
    /// </returns>
    public bool EndInvokeCalled
    {
      get
      {
        return this._endInvokeCalled;
      }
      set
      {
        this._endInvokeCalled = value;
      }
    }

    private void FaultInWaitHandle()
    {
      lock (this)
      {
        if (this._AsyncWaitHandle != null)
          return;
        this._AsyncWaitHandle = new ManualResetEvent(false);
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Threading.WaitHandle" /> инкапсулирующий дескрипторы синхронизации Win32 и разрешает реализацию различных схем синхронизации.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.WaitHandle" /> инкапсулирующий дескрипторы синхронизации Win32 и разрешает реализацию различных схем синхронизации.
    /// </returns>
    public virtual WaitHandle AsyncWaitHandle
    {
      get
      {
        this.FaultInWaitHandle();
        return (WaitHandle) this._AsyncWaitHandle;
      }
    }

    /// <summary>
    ///   Наборы <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> текущего вызова удаленного метода, который предоставляет способ управления асинхронными сообщениями после их отправки.
    /// </summary>
    /// <param name="mc">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> Для текущего удаленного вызова метода.
    /// </param>
    public virtual void SetMessageCtrl(IMessageCtrl mc)
    {
      this._mc = mc;
    }

    /// <summary>
    ///   Синхронно обрабатывает ответное сообщение, возвращенное вызовом метода удаленного объекта.
    /// </summary>
    /// <param name="msg">
    ///   Сообщение ответа на вызов метода удаленного объекта.
    /// </param>
    /// <returns>
    ///   Возвращает <see langword="null" />.
    /// </returns>
    [SecurityCritical]
    public virtual IMessage SyncProcessMessage(IMessage msg)
    {
      this._replyMsg = msg != null ? (msg is IMethodReturnMessage ? msg : (IMessage) new ReturnMessage((Exception) new RemotingException(Environment.GetResourceString("Remoting_Message_BadType")), (IMethodCallMessage) new ErrorMessage())) : (IMessage) new ReturnMessage((Exception) new RemotingException(Environment.GetResourceString("Remoting_NullMessage")), (IMethodCallMessage) new ErrorMessage());
      this._isCompleted = true;
      this.FaultInWaitHandle();
      this._AsyncWaitHandle.Set();
      if (this._acbd != null)
        this._acbd((IAsyncResult) this);
      return (IMessage) null;
    }

    /// <summary>
    ///   Реализует интерфейс <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" />.
    /// </summary>
    /// <param name="msg">
    ///   Запрос <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> интерфейса.
    /// </param>
    /// <param name="replySink">
    ///   Ответ <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> интерфейса.
    /// </param>
    /// <returns>Возвращаемое значение отсутствует.</returns>
    [SecurityCritical]
    public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    /// <summary>
    ///   Возвращает следующий приемник сообщений в цепочке приемников.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> Интерфейс, который представляет следующее сообщение приемник в цепочке приемников.
    /// </returns>
    public IMessageSink NextSink
    {
      [SecurityCritical] get
      {
        return (IMessageSink) null;
      }
    }

    /// <summary>
    ///   Возвращает сообщение ответа для асинхронного вызова.
    /// </summary>
    /// <returns>
    ///   Сообщение удаленного взаимодействия, которое должно представлять ответ на вызов метода удаленного объекта.
    /// </returns>
    public virtual IMessage GetReplyMessage()
    {
      return this._replyMsg;
    }
  }
}
