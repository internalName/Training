// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.WinRTClassActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class WinRTClassActivator : MarshalByRefObject, IWinRTClassActivator
  {
    [SecurityCritical]
    public object ActivateInstance(string activatableClassId)
    {
      return WindowsRuntimeMarshal.GetManagedActivationFactory(this.LoadWinRTType(activatableClassId)).ActivateInstance();
    }

    [SecurityCritical]
    public IntPtr GetActivationFactory(string activatableClassId, ref Guid iid)
    {
      IntPtr pUnk = IntPtr.Zero;
      try
      {
        pUnk = WindowsRuntimeMarshal.GetActivationFactoryForType(this.LoadWinRTType(activatableClassId));
        IntPtr ppv = IntPtr.Zero;
        int errorCode = Marshal.QueryInterface(pUnk, ref iid, out ppv);
        if (errorCode < 0)
          Marshal.ThrowExceptionForHR(errorCode);
        return ppv;
      }
      finally
      {
        if (pUnk != IntPtr.Zero)
          Marshal.Release(pUnk);
      }
    }

    private Type LoadWinRTType(string acid)
    {
      Type type = Type.GetType(acid + ", Windows, ContentType=WindowsRuntime");
      if (type == (Type) null)
        throw new COMException(-2147221164);
      return type;
    }

    [SecurityCritical]
    internal IntPtr GetIWinRTClassActivator()
    {
      return Marshal.GetComInterfaceForObject((object) this, typeof (IWinRTClassActivator));
    }
  }
}
