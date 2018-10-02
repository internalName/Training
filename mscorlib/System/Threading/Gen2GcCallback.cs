// Decompiled with JetBrains decompiler
// Type: System.Threading.Gen2GcCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  internal sealed class Gen2GcCallback : CriticalFinalizerObject
  {
    private Func<object, bool> m_callback;
    private GCHandle m_weakTargetObj;

    [SecuritySafeCritical]
    public Gen2GcCallback()
    {
    }

    public static void Register(Func<object, bool> callback, object targetObj)
    {
      new Gen2GcCallback().Setup(callback, targetObj);
    }

    [SecuritySafeCritical]
    private void Setup(Func<object, bool> callback, object targetObj)
    {
      this.m_callback = callback;
      this.m_weakTargetObj = GCHandle.Alloc(targetObj, GCHandleType.Weak);
    }

    [SecuritySafeCritical]
    ~Gen2GcCallback()
    {
      if (!this.m_weakTargetObj.IsAllocated)
        return;
      object target = this.m_weakTargetObj.Target;
      if (target == null)
      {
        this.m_weakTargetObj.Free();
      }
      else
      {
        try
        {
          if (!this.m_callback(target))
            return;
        }
        catch
        {
        }
        if (Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload())
          return;
        GC.ReRegisterForFinalize((object) this);
      }
    }
  }
}
