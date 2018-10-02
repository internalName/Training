// Decompiled with JetBrains decompiler
// Type: System.Threading.CompressedStackSwitcher
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading
{
  internal struct CompressedStackSwitcher : IDisposable
  {
    internal CompressedStack curr_CS;
    internal CompressedStack prev_CS;
    internal IntPtr prev_ADStack;

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is CompressedStackSwitcher))
        return false;
      CompressedStackSwitcher compressedStackSwitcher = (CompressedStackSwitcher) obj;
      if (this.curr_CS == compressedStackSwitcher.curr_CS && this.prev_CS == compressedStackSwitcher.prev_CS)
        return this.prev_ADStack == compressedStackSwitcher.prev_ADStack;
      return false;
    }

    public override int GetHashCode()
    {
      return this.ToString().GetHashCode();
    }

    public static bool operator ==(CompressedStackSwitcher c1, CompressedStackSwitcher c2)
    {
      return c1.Equals((object) c2);
    }

    public static bool operator !=(CompressedStackSwitcher c1, CompressedStackSwitcher c2)
    {
      return !c1.Equals((object) c2);
    }

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
    public void Undo()
    {
      if (this.curr_CS == null && this.prev_CS == null)
        return;
      if (this.prev_ADStack != (IntPtr) 0)
        CompressedStack.RestoreAppDomainStack(this.prev_ADStack);
      CompressedStack.SetCompressedStackThread(this.prev_CS);
      this.prev_CS = (CompressedStack) null;
      this.curr_CS = (CompressedStack) null;
      this.prev_ADStack = (IntPtr) 0;
    }
  }
}
