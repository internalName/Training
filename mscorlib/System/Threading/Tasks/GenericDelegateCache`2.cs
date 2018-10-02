// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.GenericDelegateCache`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal static class GenericDelegateCache<TAntecedentResult, TResult>
  {
    internal static Func<Task<Task>, object, TResult> CWAnyFuncDelegate = (Func<Task<Task>, object, TResult>) ((wrappedWinner, state) => ((Func<Task<TAntecedentResult>, TResult>) state)((Task<TAntecedentResult>) wrappedWinner.Result));
    internal static Func<Task<Task>, object, TResult> CWAnyActionDelegate = (Func<Task<Task>, object, TResult>) ((wrappedWinner, state) =>
    {
      ((Action<Task<TAntecedentResult>>) state)((Task<TAntecedentResult>) wrappedWinner.Result);
      return default (TResult);
    });
    internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllFuncDelegate = (Func<Task<Task<TAntecedentResult>[]>, object, TResult>) ((wrappedAntecedents, state) =>
    {
      wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
      return ((Func<Task<TAntecedentResult>[], TResult>) state)(wrappedAntecedents.Result);
    });
    internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllActionDelegate = (Func<Task<Task<TAntecedentResult>[]>, object, TResult>) ((wrappedAntecedents, state) =>
    {
      wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
      ((Action<Task<TAntecedentResult>[]>) state)(wrappedAntecedents.Result);
      return default (TResult);
    });
  }
}
