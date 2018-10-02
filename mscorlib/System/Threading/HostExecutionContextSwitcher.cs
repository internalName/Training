// Decompiled with JetBrains decompiler
// Type: System.Threading.HostExecutionContextSwitcher
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
  internal class HostExecutionContextSwitcher
  {
    internal ExecutionContext executionContext;
    internal HostExecutionContext previousHostContext;
    internal HostExecutionContext currentHostContext;

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static void Undo(object switcherObject)
    {
      if (switcherObject == null)
        return;
      HostExecutionContextManager.GetCurrentHostExecutionContextManager()?.Revert(switcherObject);
    }
  }
}
