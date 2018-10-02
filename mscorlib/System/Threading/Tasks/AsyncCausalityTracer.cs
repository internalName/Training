// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.AsyncCausalityTracer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using Windows.Foundation.Diagnostics;

namespace System.Threading.Tasks
{
  [FriendAccessAllowed]
  internal static class AsyncCausalityTracer
  {
    private static readonly Guid s_PlatformId = new Guid(1258385830U, (ushort) 62416, (ushort) 16800, (byte) 155, (byte) 51, (byte) 2, (byte) 85, (byte) 6, (byte) 82, (byte) 185, (byte) 149);
    private const CausalitySource s_CausalitySource = CausalitySource.Library;
    private static IAsyncCausalityTracerStatics s_TracerFactory;
    private static AsyncCausalityTracer.Loggers f_LoggingOn;

    internal static void EnableToETW(bool enabled)
    {
      if (enabled)
        AsyncCausalityTracer.f_LoggingOn |= AsyncCausalityTracer.Loggers.ETW;
      else
        AsyncCausalityTracer.f_LoggingOn &= ~AsyncCausalityTracer.Loggers.ETW;
    }

    [FriendAccessAllowed]
    internal static bool LoggingOn
    {
      [FriendAccessAllowed] get
      {
        return (uint) AsyncCausalityTracer.f_LoggingOn > 0U;
      }
    }

    [SecuritySafeCritical]
    static AsyncCausalityTracer()
    {
      if (!Environment.IsWinRTSupported)
        return;
      string activatableClassId = "Windows.Foundation.Diagnostics.AsyncCausalityTracer";
      Guid iid = new Guid(1350896422, (short) 9854, (short) 17691, (byte) 168, (byte) 144, (byte) 171, (byte) 106, (byte) 55, (byte) 2, (byte) 69, (byte) 238);
      object factory = (object) null;
      try
      {
        if (UnsafeNativeMethods.RoGetActivationFactory(activatableClassId, ref iid, out factory) < 0 || factory == null)
          return;
        AsyncCausalityTracer.s_TracerFactory = (IAsyncCausalityTracerStatics) factory;
        AsyncCausalityTracer.s_TracerFactory.add_TracingStatusChanged(new EventHandler<TracingStatusChangedEventArgs>(AsyncCausalityTracer.TracingStatusChangedHandler));
      }
      catch (Exception ex)
      {
        AsyncCausalityTracer.LogAndDisable(ex);
      }
    }

    [SecuritySafeCritical]
    private static void TracingStatusChangedHandler(object sender, TracingStatusChangedEventArgs args)
    {
      if (args.Enabled)
        AsyncCausalityTracer.f_LoggingOn |= AsyncCausalityTracer.Loggers.CausalityTracer;
      else
        AsyncCausalityTracer.f_LoggingOn &= ~AsyncCausalityTracer.Loggers.CausalityTracer;
    }

    [FriendAccessAllowed]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void TraceOperationCreation(CausalityTraceLevel traceLevel, int taskId, string operationName, ulong relatedContext)
    {
      try
      {
        if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers) 0)
          TplEtwProvider.Log.TraceOperationBegin(taskId, operationName, (long) relatedContext);
        if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) == (AsyncCausalityTracer.Loggers) 0)
          return;
        AsyncCausalityTracer.s_TracerFactory.TraceOperationCreation((Windows.Foundation.Diagnostics.CausalityTraceLevel) traceLevel, CausalitySource.Library, AsyncCausalityTracer.s_PlatformId, AsyncCausalityTracer.GetOperationId((uint) taskId), operationName, relatedContext);
      }
      catch (Exception ex)
      {
        AsyncCausalityTracer.LogAndDisable(ex);
      }
    }

    [FriendAccessAllowed]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void TraceOperationCompletion(CausalityTraceLevel traceLevel, int taskId, AsyncCausalityStatus status)
    {
      try
      {
        if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers) 0)
          TplEtwProvider.Log.TraceOperationEnd(taskId, status);
        if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) == (AsyncCausalityTracer.Loggers) 0)
          return;
        AsyncCausalityTracer.s_TracerFactory.TraceOperationCompletion((Windows.Foundation.Diagnostics.CausalityTraceLevel) traceLevel, CausalitySource.Library, AsyncCausalityTracer.s_PlatformId, AsyncCausalityTracer.GetOperationId((uint) taskId), (Windows.Foundation.Diagnostics.AsyncCausalityStatus) status);
      }
      catch (Exception ex)
      {
        AsyncCausalityTracer.LogAndDisable(ex);
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void TraceOperationRelation(CausalityTraceLevel traceLevel, int taskId, CausalityRelation relation)
    {
      try
      {
        if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers) 0)
          TplEtwProvider.Log.TraceOperationRelation(taskId, relation);
        if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) == (AsyncCausalityTracer.Loggers) 0)
          return;
        AsyncCausalityTracer.s_TracerFactory.TraceOperationRelation((Windows.Foundation.Diagnostics.CausalityTraceLevel) traceLevel, CausalitySource.Library, AsyncCausalityTracer.s_PlatformId, AsyncCausalityTracer.GetOperationId((uint) taskId), (Windows.Foundation.Diagnostics.CausalityRelation) relation);
      }
      catch (Exception ex)
      {
        AsyncCausalityTracer.LogAndDisable(ex);
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void TraceSynchronousWorkStart(CausalityTraceLevel traceLevel, int taskId, CausalitySynchronousWork work)
    {
      try
      {
        if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers) 0)
          TplEtwProvider.Log.TraceSynchronousWorkBegin(taskId, work);
        if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) == (AsyncCausalityTracer.Loggers) 0)
          return;
        AsyncCausalityTracer.s_TracerFactory.TraceSynchronousWorkStart((Windows.Foundation.Diagnostics.CausalityTraceLevel) traceLevel, CausalitySource.Library, AsyncCausalityTracer.s_PlatformId, AsyncCausalityTracer.GetOperationId((uint) taskId), (Windows.Foundation.Diagnostics.CausalitySynchronousWork) work);
      }
      catch (Exception ex)
      {
        AsyncCausalityTracer.LogAndDisable(ex);
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void TraceSynchronousWorkCompletion(CausalityTraceLevel traceLevel, CausalitySynchronousWork work)
    {
      try
      {
        if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.ETW) != (AsyncCausalityTracer.Loggers) 0)
          TplEtwProvider.Log.TraceSynchronousWorkEnd(work);
        if ((AsyncCausalityTracer.f_LoggingOn & AsyncCausalityTracer.Loggers.CausalityTracer) == (AsyncCausalityTracer.Loggers) 0)
          return;
        AsyncCausalityTracer.s_TracerFactory.TraceSynchronousWorkCompletion((Windows.Foundation.Diagnostics.CausalityTraceLevel) traceLevel, CausalitySource.Library, (Windows.Foundation.Diagnostics.CausalitySynchronousWork) work);
      }
      catch (Exception ex)
      {
        AsyncCausalityTracer.LogAndDisable(ex);
      }
    }

    private static void LogAndDisable(Exception ex)
    {
      AsyncCausalityTracer.f_LoggingOn = (AsyncCausalityTracer.Loggers) 0;
      Debugger.Log(0, nameof (AsyncCausalityTracer), ex.ToString());
    }

    private static ulong GetOperationId(uint taskId)
    {
      return ((ulong) AppDomain.CurrentDomain.Id << 32) + (ulong) taskId;
    }

    [Flags]
    private enum Loggers : byte
    {
      CausalityTracer = 1,
      ETW = 2,
    }
  }
}
