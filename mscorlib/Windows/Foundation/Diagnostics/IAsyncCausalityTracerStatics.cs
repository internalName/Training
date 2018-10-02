// Decompiled with JetBrains decompiler
// Type: Windows.Foundation.Diagnostics.IAsyncCausalityTracerStatics
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Windows.Foundation.Diagnostics
{
  [Guid("50850B26-267E-451B-A890-AB6A370245EE")]
  [ComImport]
  internal interface IAsyncCausalityTracerStatics
  {
    void TraceOperationCreation(CausalityTraceLevel traceLevel, CausalitySource source, Guid platformId, ulong operationId, string operationName, ulong relatedContext);

    void TraceOperationCompletion(CausalityTraceLevel traceLevel, CausalitySource source, Guid platformId, ulong operationId, AsyncCausalityStatus status);

    void TraceOperationRelation(CausalityTraceLevel traceLevel, CausalitySource source, Guid platformId, ulong operationId, CausalityRelation relation);

    void TraceSynchronousWorkStart(CausalityTraceLevel traceLevel, CausalitySource source, Guid platformId, ulong operationId, CausalitySynchronousWork work);

    void TraceSynchronousWorkCompletion(CausalityTraceLevel traceLevel, CausalitySource source, CausalitySynchronousWork work);

    EventRegistrationToken add_TracingStatusChanged(EventHandler<TracingStatusChangedEventArgs> eventHandler);

    void remove_TracingStatusChanged(EventRegistrationToken token);
  }
}
