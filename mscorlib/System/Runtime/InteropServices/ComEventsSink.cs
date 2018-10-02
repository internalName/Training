// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComEventsSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.Runtime.InteropServices
{
  [SecurityCritical]
  internal class ComEventsSink : NativeMethods.IDispatch, ICustomQueryInterface
  {
    private static Guid IID_IManagedObject = new Guid("{C3FCC19E-A970-11D2-8B5A-00A0C9B7C9C4}");
    private Guid _iidSourceItf;
    private IConnectionPoint _connectionPoint;
    private int _cookie;
    private ComEventsMethod _methods;
    private ComEventsSink _next;
    private const VarEnum VT_BYREF_VARIANT = VarEnum.VT_VARIANT | VarEnum.VT_BYREF;
    private const VarEnum VT_TYPEMASK = (VarEnum) 4095;
    private const VarEnum VT_BYREF_TYPEMASK = (VarEnum) 20479;

    internal ComEventsSink(object rcw, Guid iid)
    {
      this._iidSourceItf = iid;
      this.Advise(rcw);
    }

    internal static ComEventsSink Find(ComEventsSink sinks, ref Guid iid)
    {
      ComEventsSink comEventsSink = sinks;
      while (comEventsSink != null && comEventsSink._iidSourceItf != iid)
        comEventsSink = comEventsSink._next;
      return comEventsSink;
    }

    internal static ComEventsSink Add(ComEventsSink sinks, ComEventsSink sink)
    {
      sink._next = sinks;
      return sink;
    }

    [SecurityCritical]
    internal static ComEventsSink RemoveAll(ComEventsSink sinks)
    {
      for (; sinks != null; sinks = sinks._next)
        sinks.Unadvise();
      return (ComEventsSink) null;
    }

    [SecurityCritical]
    internal static ComEventsSink Remove(ComEventsSink sinks, ComEventsSink sink)
    {
      if (sink == sinks)
      {
        sinks = sinks._next;
      }
      else
      {
        ComEventsSink comEventsSink = sinks;
        while (comEventsSink != null && comEventsSink._next != sink)
          comEventsSink = comEventsSink._next;
        if (comEventsSink != null)
          comEventsSink._next = sink._next;
      }
      sink.Unadvise();
      return sinks;
    }

    public ComEventsMethod RemoveMethod(ComEventsMethod method)
    {
      this._methods = ComEventsMethod.Remove(this._methods, method);
      return this._methods;
    }

    public ComEventsMethod FindMethod(int dispid)
    {
      return ComEventsMethod.Find(this._methods, dispid);
    }

    public ComEventsMethod AddMethod(int dispid)
    {
      ComEventsMethod method = new ComEventsMethod(dispid);
      this._methods = ComEventsMethod.Add(this._methods, method);
      return method;
    }

    [SecurityCritical]
    void NativeMethods.IDispatch.GetTypeInfoCount(out uint pctinfo)
    {
      pctinfo = 0U;
    }

    [SecurityCritical]
    void NativeMethods.IDispatch.GetTypeInfo(uint iTInfo, int lcid, out IntPtr info)
    {
      throw new NotImplementedException();
    }

    [SecurityCritical]
    void NativeMethods.IDispatch.GetIDsOfNames(ref Guid iid, string[] names, uint cNames, int lcid, int[] rgDispId)
    {
      throw new NotImplementedException();
    }

    private static unsafe Variant* GetVariant(Variant* pSrc)
    {
      if (pSrc->VariantType == (VarEnum.VT_VARIANT | VarEnum.VT_BYREF))
      {
        Variant* asByRefVariant = (Variant*) (void*) pSrc->AsByRefVariant;
        if ((asByRefVariant->VariantType & (VarEnum) 20479) == (VarEnum.VT_VARIANT | VarEnum.VT_BYREF))
          return asByRefVariant;
      }
      return pSrc;
    }

    [SecurityCritical]
    unsafe void NativeMethods.IDispatch.Invoke(int dispid, ref Guid riid, int lcid, System.Runtime.InteropServices.ComTypes.INVOKEKIND wFlags, ref System.Runtime.InteropServices.ComTypes.DISPPARAMS pDispParams, IntPtr pvarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      ComEventsMethod method = this.FindMethod(dispid);
      if (method == null)
        return;
      object[] args = new object[pDispParams.cArgs];
      int[] numArray = new int[pDispParams.cArgs];
      bool[] flagArray = new bool[pDispParams.cArgs];
      Variant* rgvarg = (Variant*) (void*) pDispParams.rgvarg;
      int* rgdispidNamedArgs = (int*) (void*) pDispParams.rgdispidNamedArgs;
      int index1;
      for (index1 = 0; index1 < pDispParams.cNamedArgs; ++index1)
      {
        int index2 = rgdispidNamedArgs[index1];
        Variant* variant = ComEventsSink.GetVariant(rgvarg + index1);
        args[index2] = variant->ToObject();
        flagArray[index2] = true;
        numArray[index2] = !variant->IsByRef ? -1 : index1;
      }
      int index3 = 0;
      for (; index1 < pDispParams.cArgs; ++index1)
      {
        while (flagArray[index3])
          ++index3;
        Variant* variant = ComEventsSink.GetVariant(rgvarg + (pDispParams.cArgs - 1 - index1));
        args[index3] = variant->ToObject();
        numArray[index3] = !variant->IsByRef ? -1 : pDispParams.cArgs - 1 - index1;
        ++index3;
      }
      object obj = method.Invoke(args);
      if (pvarResult != IntPtr.Zero)
        Marshal.GetNativeVariantForObject(obj, pvarResult);
      for (int index2 = 0; index2 < pDispParams.cArgs; ++index2)
      {
        int num = numArray[index2];
        if (num != -1)
          ComEventsSink.GetVariant(rgvarg + num)->CopyFromIndirect(args[index2]);
      }
    }

    [SecurityCritical]
    CustomQueryInterfaceResult ICustomQueryInterface.GetInterface(ref Guid iid, out IntPtr ppv)
    {
      ppv = IntPtr.Zero;
      if (iid == this._iidSourceItf || iid == typeof (NativeMethods.IDispatch).GUID)
      {
        ppv = Marshal.GetComInterfaceForObject((object) this, typeof (NativeMethods.IDispatch), CustomQueryInterfaceMode.Ignore);
        return CustomQueryInterfaceResult.Handled;
      }
      return iid == ComEventsSink.IID_IManagedObject ? CustomQueryInterfaceResult.Failed : CustomQueryInterfaceResult.NotHandled;
    }

    private void Advise(object rcw)
    {
      IConnectionPoint ppCP;
      ((IConnectionPointContainer) rcw).FindConnectionPoint(ref this._iidSourceItf, out ppCP);
      object pUnkSink = (object) this;
      ppCP.Advise(pUnkSink, out this._cookie);
      this._connectionPoint = ppCP;
    }

    [SecurityCritical]
    private void Unadvise()
    {
      try
      {
        this._connectionPoint.Unadvise(this._cookie);
        Marshal.ReleaseComObject((object) this._connectionPoint);
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this._connectionPoint = (IConnectionPoint) null;
      }
    }
  }
}
