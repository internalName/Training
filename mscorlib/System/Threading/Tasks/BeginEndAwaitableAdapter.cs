// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.BeginEndAwaitableAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
  internal sealed class BeginEndAwaitableAdapter : ICriticalNotifyCompletion, INotifyCompletion
  {
    private static readonly Action CALLBACK_RAN = (Action) (() => {});
    public static readonly AsyncCallback Callback = (AsyncCallback) (asyncResult =>
    {
      BeginEndAwaitableAdapter asyncState = (BeginEndAwaitableAdapter) asyncResult.AsyncState;
      asyncState._asyncResult = asyncResult;
      Action action = Interlocked.Exchange<Action>(ref asyncState._continuation, BeginEndAwaitableAdapter.CALLBACK_RAN);
      if (action == null)
        return;
      action();
    });
    private IAsyncResult _asyncResult;
    private Action _continuation;

    public BeginEndAwaitableAdapter GetAwaiter()
    {
      return this;
    }

    public bool IsCompleted
    {
      get
      {
        return this._continuation == BeginEndAwaitableAdapter.CALLBACK_RAN;
      }
    }

    [SecurityCritical]
    public void UnsafeOnCompleted(Action continuation)
    {
      this.OnCompleted(continuation);
    }

    public void OnCompleted(Action continuation)
    {
      if (!(this._continuation == BeginEndAwaitableAdapter.CALLBACK_RAN) && !(Interlocked.CompareExchange<Action>(ref this._continuation, continuation, (Action) null) == BeginEndAwaitableAdapter.CALLBACK_RAN))
        return;
      Task.Run(continuation);
    }

    public IAsyncResult GetResult()
    {
      IAsyncResult asyncResult = this._asyncResult;
      this._asyncResult = (IAsyncResult) null;
      this._continuation = (Action) null;
      return asyncResult;
    }
  }
}
