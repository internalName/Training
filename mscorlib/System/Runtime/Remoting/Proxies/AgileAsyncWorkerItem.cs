// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Proxies.AgileAsyncWorkerItem
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Proxies
{
  internal class AgileAsyncWorkerItem
  {
    private IMethodCallMessage _message;
    private AsyncResult _ar;
    private object _target;

    [SecurityCritical]
    public AgileAsyncWorkerItem(IMethodCallMessage message, AsyncResult ar, object target)
    {
      this._message = (IMethodCallMessage) new MethodCall((IMessage) message);
      this._ar = ar;
      this._target = target;
    }

    [SecurityCritical]
    public static void ThreadPoolCallBack(object o)
    {
      ((AgileAsyncWorkerItem) o).DoAsyncCall();
    }

    [SecurityCritical]
    public void DoAsyncCall()
    {
      new StackBuilderSink(this._target).AsyncProcessMessage((IMessage) this._message, (IMessageSink) this._ar);
    }
  }
}
