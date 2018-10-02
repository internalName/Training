// Decompiled with JetBrains decompiler
// Type: System.Threading.ExecutionContextSwitcher
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Principal;

namespace System.Threading
{
  internal struct ExecutionContextSwitcher
  {
    internal ExecutionContext.Reader outerEC;
    internal bool outerECBelongsToScope;
    internal SecurityContextSwitcher scsw;
    internal object hecsw;
    internal WindowsIdentity wi;
    internal bool cachedAlwaysFlowImpersonationPolicy;
    internal bool wiIsValid;
    internal Thread thread;

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [HandleProcessCorruptedStateExceptions]
    internal bool UndoNoThrow()
    {
      try
      {
        this.Undo();
      }
      catch
      {
        return false;
      }
      return true;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal void Undo()
    {
      if (this.thread == null)
        return;
      Thread thread = this.thread;
      if (this.hecsw != null)
        HostExecutionContextSwitcher.Undo(this.hecsw);
      ExecutionContext.Reader executionContextReader = thread.GetExecutionContextReader();
      thread.SetExecutionContext(this.outerEC, this.outerECBelongsToScope);
      if (this.scsw.currSC != null)
        this.scsw.Undo();
      if (this.wiIsValid)
        SecurityContext.RestoreCurrentWI(this.outerEC, executionContextReader, this.wi, this.cachedAlwaysFlowImpersonationPolicy);
      this.thread = (Thread) null;
      ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), this.outerEC.DangerousGetRawExecutionContext());
    }
  }
}
