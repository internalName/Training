// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.SimpleEventTypes`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Threading;

namespace System.Diagnostics.Tracing
{
  internal class SimpleEventTypes<T> : TraceLoggingEventTypes
  {
    private static SimpleEventTypes<T> instance;
    internal readonly TraceLoggingTypeInfo<T> typeInfo;

    private SimpleEventTypes(TraceLoggingTypeInfo<T> typeInfo)
      : base(typeInfo.Name, typeInfo.Tags, new TraceLoggingTypeInfo[1]
      {
        (TraceLoggingTypeInfo) typeInfo
      })
    {
      this.typeInfo = typeInfo;
    }

    public static SimpleEventTypes<T> Instance
    {
      get
      {
        return SimpleEventTypes<T>.instance ?? SimpleEventTypes<T>.InitInstance();
      }
    }

    private static SimpleEventTypes<T> InitInstance()
    {
      SimpleEventTypes<T> simpleEventTypes = new SimpleEventTypes<T>(TraceLoggingTypeInfo<T>.Instance);
      Interlocked.CompareExchange<SimpleEventTypes<T>>(ref SimpleEventTypes<T>.instance, simpleEventTypes, (SimpleEventTypes<T>) null);
      return SimpleEventTypes<T>.instance;
    }
  }
}
