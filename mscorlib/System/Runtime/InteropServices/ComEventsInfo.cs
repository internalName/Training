// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComEventsInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.InteropServices
{
  [SecurityCritical]
  internal class ComEventsInfo
  {
    private ComEventsSink _sinks;
    private object _rcw;

    private ComEventsInfo(object rcw)
    {
      this._rcw = rcw;
    }

    [SecuritySafeCritical]
    ~ComEventsInfo()
    {
      this._sinks = ComEventsSink.RemoveAll(this._sinks);
    }

    [SecurityCritical]
    internal static ComEventsInfo Find(object rcw)
    {
      return (ComEventsInfo) Marshal.GetComObjectData(rcw, (object) typeof (ComEventsInfo));
    }

    [SecurityCritical]
    internal static ComEventsInfo FromObject(object rcw)
    {
      ComEventsInfo comEventsInfo = ComEventsInfo.Find(rcw);
      if (comEventsInfo == null)
      {
        comEventsInfo = new ComEventsInfo(rcw);
        Marshal.SetComObjectData(rcw, (object) typeof (ComEventsInfo), (object) comEventsInfo);
      }
      return comEventsInfo;
    }

    internal ComEventsSink FindSink(ref Guid iid)
    {
      return ComEventsSink.Find(this._sinks, ref iid);
    }

    internal ComEventsSink AddSink(ref Guid iid)
    {
      this._sinks = ComEventsSink.Add(this._sinks, new ComEventsSink(this._rcw, iid));
      return this._sinks;
    }

    [SecurityCritical]
    internal ComEventsSink RemoveSink(ComEventsSink sink)
    {
      this._sinks = ComEventsSink.Remove(this._sinks, sink);
      return this._sinks;
    }
  }
}
