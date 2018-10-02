// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.CleanupWorkList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  [SecurityCritical]
  internal sealed class CleanupWorkList
  {
    private List<CleanupWorkListElement> m_list = new List<CleanupWorkListElement>();

    public void Add(CleanupWorkListElement elem)
    {
      this.m_list.Add(elem);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public void Destroy()
    {
      for (int index = this.m_list.Count - 1; index >= 0; --index)
      {
        if (this.m_list[index].m_owned)
          System.StubHelpers.StubHelpers.SafeHandleRelease(this.m_list[index].m_handle);
      }
    }
  }
}
