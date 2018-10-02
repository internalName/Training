// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityContextSwitcher
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security
{
  internal struct SecurityContextSwitcher : IDisposable
  {
    internal SecurityContext.Reader prevSC;
    internal SecurityContext currSC;
    internal ExecutionContext currEC;
    internal CompressedStackSwitcher cssw;
    internal WindowsImpersonationContext wic;

    [SecuritySafeCritical]
    public void Dispose()
    {
      this.Undo();
    }

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
    [HandleProcessCorruptedStateExceptions]
    public void Undo()
    {
      if (this.currSC == null)
        return;
      if (this.currEC != null)
        this.currEC.SecurityContext = this.prevSC.DangerousGetRawSecurityContext();
      this.currSC = (SecurityContext) null;
      bool flag = true;
      try
      {
        if (this.wic != null)
          flag &= this.wic.UndoNoThrow();
      }
      catch
      {
        flag &= this.cssw.UndoNoThrow();
        Environment.FailFast(Environment.GetResourceString("ExecutionContext_UndoFailed"));
      }
      if (flag & this.cssw.UndoNoThrow())
        return;
      Environment.FailFast(Environment.GetResourceString("ExecutionContext_UndoFailed"));
    }
  }
}
