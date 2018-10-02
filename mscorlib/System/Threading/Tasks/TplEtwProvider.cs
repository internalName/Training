// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TplEtwProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
  [EventSource(Guid = "2e5dba47-a3d2-4d16-8ee0-6671ffdcd7b5", LocalizationResources = "mscorlib", Name = "System.Threading.Tasks.TplEventSource")]
  internal sealed class TplEtwProvider : EventSource
  {
    public static TplEtwProvider Log = new TplEtwProvider();
    internal bool TasksSetActivityIds;
    internal bool Debug;
    private bool DebugActivityId;
    private const EventKeywords ALL_KEYWORDS = EventKeywords.All;
    private const int PARALLELLOOPBEGIN_ID = 1;
    private const int PARALLELLOOPEND_ID = 2;
    private const int PARALLELINVOKEBEGIN_ID = 3;
    private const int PARALLELINVOKEEND_ID = 4;
    private const int PARALLELFORK_ID = 5;
    private const int PARALLELJOIN_ID = 6;
    private const int TASKSCHEDULED_ID = 7;
    private const int TASKSTARTED_ID = 8;
    private const int TASKCOMPLETED_ID = 9;
    private const int TASKWAITBEGIN_ID = 10;
    private const int TASKWAITEND_ID = 11;
    private const int AWAITTASKCONTINUATIONSCHEDULED_ID = 12;
    private const int TASKWAITCONTINUATIONCOMPLETE_ID = 13;
    private const int TASKWAITCONTINUATIONSTARTED_ID = 19;
    private const int TRACEOPERATIONSTART_ID = 14;
    private const int TRACEOPERATIONSTOP_ID = 15;
    private const int TRACEOPERATIONRELATION_ID = 16;
    private const int TRACESYNCHRONOUSWORKSTART_ID = 17;
    private const int TRACESYNCHRONOUSWORKSTOP_ID = 18;

    protected override void OnEventCommand(EventCommandEventArgs command)
    {
      if (command.Command == EventCommand.Enable)
        AsyncCausalityTracer.EnableToETW(true);
      else if (command.Command == EventCommand.Disable)
        AsyncCausalityTracer.EnableToETW(false);
      if (this.IsEnabled(EventLevel.Informational, (EventKeywords) 128))
        ActivityTracker.Instance.Enable();
      else
        this.TasksSetActivityIds = this.IsEnabled(EventLevel.Informational, (EventKeywords) 65536);
      this.Debug = this.IsEnabled(EventLevel.Informational, (EventKeywords) 131072);
      this.DebugActivityId = this.IsEnabled(EventLevel.Informational, (EventKeywords) 262144);
    }

    private TplEtwProvider()
    {
    }

    [SecuritySafeCritical]
    [Event(1, ActivityOptions = EventActivityOptions.Recursive, Level = EventLevel.Informational, Opcode = EventOpcode.Start, Task = (EventTask) 1)]
    public unsafe void ParallelLoopBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, TplEtwProvider.ForkJoinOperationType OperationType, long InclusiveFrom, long ExclusiveTo)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Informational, (EventKeywords) 4))
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[6];
      data->Size = 4;
      data->DataPointer = (IntPtr) ((void*) &OriginatingTaskSchedulerID);
      data[1].Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) &OriginatingTaskID);
      data[2].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &ForkJoinContextID);
      data[3].Size = 4;
      data[3].DataPointer = (IntPtr) ((void*) &OperationType);
      data[4].Size = 8;
      data[4].DataPointer = (IntPtr) ((void*) &InclusiveFrom);
      data[5].Size = 8;
      data[5].DataPointer = (IntPtr) ((void*) &ExclusiveTo);
      this.WriteEventCore(1, 6, data);
    }

    [SecuritySafeCritical]
    [Event(2, Level = EventLevel.Informational, Opcode = EventOpcode.Stop, Task = (EventTask) 1)]
    public unsafe void ParallelLoopEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, long TotalIterations)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Informational, (EventKeywords) 4))
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[4];
      data->Size = 4;
      data->DataPointer = (IntPtr) ((void*) &OriginatingTaskSchedulerID);
      data[1].Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) &OriginatingTaskID);
      data[2].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &ForkJoinContextID);
      data[3].Size = 8;
      data[3].DataPointer = (IntPtr) ((void*) &TotalIterations);
      this.WriteEventCore(2, 4, data);
    }

    [SecuritySafeCritical]
    [Event(3, ActivityOptions = EventActivityOptions.Recursive, Level = EventLevel.Informational, Opcode = EventOpcode.Start, Task = (EventTask) 2)]
    public unsafe void ParallelInvokeBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, TplEtwProvider.ForkJoinOperationType OperationType, int ActionCount)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Informational, (EventKeywords) 4))
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[5];
      data->Size = 4;
      data->DataPointer = (IntPtr) ((void*) &OriginatingTaskSchedulerID);
      data[1].Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) &OriginatingTaskID);
      data[2].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &ForkJoinContextID);
      data[3].Size = 4;
      data[3].DataPointer = (IntPtr) ((void*) &OperationType);
      data[4].Size = 4;
      data[4].DataPointer = (IntPtr) ((void*) &ActionCount);
      this.WriteEventCore(3, 5, data);
    }

    [Event(4, Level = EventLevel.Informational, Opcode = EventOpcode.Stop, Task = (EventTask) 2)]
    public void ParallelInvokeEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Informational, (EventKeywords) 4))
        return;
      this.WriteEvent(4, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
    }

    [Event(5, ActivityOptions = EventActivityOptions.Recursive, Level = EventLevel.Verbose, Opcode = EventOpcode.Start, Task = (EventTask) 5)]
    public void ParallelFork(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Verbose, (EventKeywords) 4))
        return;
      this.WriteEvent(5, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
    }

    [Event(6, Level = EventLevel.Verbose, Opcode = EventOpcode.Stop, Task = (EventTask) 5)]
    public void ParallelJoin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Verbose, (EventKeywords) 4))
        return;
      this.WriteEvent(6, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
    }

    [SecuritySafeCritical]
    [Event(7, Keywords = (EventKeywords) 3, Level = EventLevel.Informational, Opcode = EventOpcode.Send, Task = (EventTask) 6, Version = 1)]
    public unsafe void TaskScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, int CreatingTaskID, int TaskCreationOptions, int appDomain)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Informational, (EventKeywords) 3))
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[5];
      data->Size = 4;
      data->DataPointer = (IntPtr) ((void*) &OriginatingTaskSchedulerID);
      data[1].Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) &OriginatingTaskID);
      data[2].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &TaskID);
      data[3].Size = 4;
      data[3].DataPointer = (IntPtr) ((void*) &CreatingTaskID);
      data[4].Size = 4;
      data[4].DataPointer = (IntPtr) ((void*) &TaskCreationOptions);
      if (this.TasksSetActivityIds)
        this.WriteEventWithRelatedActivityIdCore(7, &TplEtwProvider.CreateGuidForTaskID(TaskID), 5, data);
      else
        this.WriteEventCore(7, 5, data);
    }

    [Event(8, Keywords = (EventKeywords) 2, Level = EventLevel.Informational)]
    public void TaskStarted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
    {
      if (!this.IsEnabled(EventLevel.Informational, (EventKeywords) 2))
        return;
      this.WriteEvent(8, OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
    }

    [SecuritySafeCritical]
    [Event(9, Keywords = (EventKeywords) 64, Level = EventLevel.Informational, Version = 1)]
    public unsafe void TaskCompleted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, bool IsExceptional)
    {
      if (!this.IsEnabled(EventLevel.Informational, (EventKeywords) 2))
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[4];
      int num = IsExceptional ? 1 : 0;
      data->Size = 4;
      data->DataPointer = (IntPtr) ((void*) &OriginatingTaskSchedulerID);
      data[1].Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) &OriginatingTaskID);
      data[2].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &TaskID);
      data[3].Size = 4;
      data[3].DataPointer = (IntPtr) ((void*) &num);
      this.WriteEventCore(9, 4, data);
    }

    [SecuritySafeCritical]
    [Event(10, Keywords = (EventKeywords) 3, Level = EventLevel.Informational, Opcode = EventOpcode.Send, Task = (EventTask) 4, Version = 3)]
    public unsafe void TaskWaitBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, TplEtwProvider.TaskWaitBehavior Behavior, int ContinueWithTaskID, int appDomain)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Informational, (EventKeywords) 3))
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[5];
      data->Size = 4;
      data->DataPointer = (IntPtr) ((void*) &OriginatingTaskSchedulerID);
      data[1].Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) &OriginatingTaskID);
      data[2].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &TaskID);
      data[3].Size = 4;
      data[3].DataPointer = (IntPtr) ((void*) &Behavior);
      data[4].Size = 4;
      data[4].DataPointer = (IntPtr) ((void*) &ContinueWithTaskID);
      if (this.TasksSetActivityIds)
        this.WriteEventWithRelatedActivityIdCore(10, &TplEtwProvider.CreateGuidForTaskID(TaskID), 5, data);
      else
        this.WriteEventCore(10, 5, data);
    }

    [Event(11, Keywords = (EventKeywords) 2, Level = EventLevel.Verbose)]
    public void TaskWaitEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Verbose, (EventKeywords) 2))
        return;
      this.WriteEvent(11, OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
    }

    [Event(13, Keywords = (EventKeywords) 64, Level = EventLevel.Verbose)]
    public void TaskWaitContinuationComplete(int TaskID)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Verbose, (EventKeywords) 2))
        return;
      this.WriteEvent(13, TaskID);
    }

    [Event(19, Keywords = (EventKeywords) 64, Level = EventLevel.Verbose)]
    public void TaskWaitContinuationStarted(int TaskID)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Verbose, (EventKeywords) 2))
        return;
      this.WriteEvent(19, TaskID);
    }

    [SecuritySafeCritical]
    [Event(12, Keywords = (EventKeywords) 3, Level = EventLevel.Informational, Opcode = EventOpcode.Send, Task = (EventTask) 7)]
    public unsafe void AwaitTaskContinuationScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ContinuwWithTaskId)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Informational, (EventKeywords) 3))
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[3];
      data->Size = 4;
      data->DataPointer = (IntPtr) ((void*) &OriginatingTaskSchedulerID);
      data[1].Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) &OriginatingTaskID);
      data[2].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &ContinuwWithTaskId);
      if (this.TasksSetActivityIds)
        this.WriteEventWithRelatedActivityIdCore(12, &TplEtwProvider.CreateGuidForTaskID(ContinuwWithTaskId), 3, data);
      else
        this.WriteEventCore(12, 3, data);
    }

    [SecuritySafeCritical]
    [Event(14, Keywords = (EventKeywords) 8, Level = EventLevel.Informational, Version = 1)]
    public unsafe void TraceOperationBegin(int TaskID, string OperationName, long RelatedContext)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Informational, (EventKeywords) 8))
        return;
      string str = OperationName;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[3];
      data->Size = 4;
      data->DataPointer = (IntPtr) ((void*) &TaskID);
      data[1].Size = (OperationName.Length + 1) * 2;
      data[1].DataPointer = (IntPtr) ((void*) chPtr);
      data[2].Size = 8;
      data[2].DataPointer = (IntPtr) ((void*) &RelatedContext);
      this.WriteEventCore(14, 3, data);
      str = (string) null;
    }

    [SecuritySafeCritical]
    [Event(16, Keywords = (EventKeywords) 16, Level = EventLevel.Informational, Version = 1)]
    public void TraceOperationRelation(int TaskID, CausalityRelation Relation)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Informational, (EventKeywords) 16))
        return;
      this.WriteEvent(16, TaskID, (int) Relation);
    }

    [SecuritySafeCritical]
    [Event(15, Keywords = (EventKeywords) 8, Level = EventLevel.Informational, Version = 1)]
    public void TraceOperationEnd(int TaskID, AsyncCausalityStatus Status)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Informational, (EventKeywords) 8))
        return;
      this.WriteEvent(15, TaskID, (int) Status);
    }

    [SecuritySafeCritical]
    [Event(17, Keywords = (EventKeywords) 32, Level = EventLevel.Informational, Version = 1)]
    public void TraceSynchronousWorkBegin(int TaskID, CausalitySynchronousWork Work)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Informational, (EventKeywords) 32))
        return;
      this.WriteEvent(17, TaskID, (int) Work);
    }

    [SecuritySafeCritical]
    [Event(18, Keywords = (EventKeywords) 32, Level = EventLevel.Informational, Version = 1)]
    public unsafe void TraceSynchronousWorkEnd(CausalitySynchronousWork Work)
    {
      if (!this.IsEnabled() || !this.IsEnabled(EventLevel.Informational, (EventKeywords) 32))
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[1];
      data->Size = 4;
      data->DataPointer = (IntPtr) ((void*) &Work);
      this.WriteEventCore(18, 1, data);
    }

    [NonEvent]
    [SecuritySafeCritical]
    public unsafe void RunningContinuation(int TaskID, object Object)
    {
      this.RunningContinuation(TaskID, (long) (ulong) *(IntPtr*) (void*) JitHelpers.UnsafeCastToStackPointer<object>(ref Object));
    }

    [Event(20, Keywords = (EventKeywords) 131072)]
    private void RunningContinuation(int TaskID, long Object)
    {
      if (!this.Debug)
        return;
      this.WriteEvent(20, (long) TaskID, Object);
    }

    [NonEvent]
    [SecuritySafeCritical]
    public unsafe void RunningContinuationList(int TaskID, int Index, object Object)
    {
      this.RunningContinuationList(TaskID, Index, (long) (ulong) *(IntPtr*) (void*) JitHelpers.UnsafeCastToStackPointer<object>(ref Object));
    }

    [Event(21, Keywords = (EventKeywords) 131072)]
    public void RunningContinuationList(int TaskID, int Index, long Object)
    {
      if (!this.Debug)
        return;
      this.WriteEvent(21, (long) TaskID, (long) Index, Object);
    }

    [Event(22, Keywords = (EventKeywords) 131072)]
    public void DebugMessage(string Message)
    {
      this.WriteEvent(22, Message);
    }

    [Event(23, Keywords = (EventKeywords) 131072)]
    public void DebugFacilityMessage(string Facility, string Message)
    {
      this.WriteEvent(23, Facility, Message);
    }

    [Event(24, Keywords = (EventKeywords) 131072)]
    public void DebugFacilityMessage1(string Facility, string Message, string Value1)
    {
      this.WriteEvent(24, Facility, Message, Value1);
    }

    [Event(25, Keywords = (EventKeywords) 262144)]
    public void SetActivityId(Guid NewId)
    {
      if (!this.DebugActivityId)
        return;
      this.WriteEvent(25, new object[1]{ (object) NewId });
    }

    [Event(26, Keywords = (EventKeywords) 131072)]
    public void NewID(int TaskID)
    {
      if (!this.Debug)
        return;
      this.WriteEvent(26, TaskID);
    }

    internal static Guid CreateGuidForTaskID(int taskID)
    {
      uint currentPid = EventSource.s_currentPid;
      int domainId = Thread.GetDomainID();
      return new Guid(taskID, (short) domainId, (short) (domainId >> 16), (byte) currentPid, (byte) (currentPid >> 8), (byte) (currentPid >> 16), (byte) (currentPid >> 24), byte.MaxValue, (byte) 220, (byte) 215, (byte) 181);
    }

    public enum ForkJoinOperationType
    {
      ParallelInvoke = 1,
      ParallelFor = 2,
      ParallelForEach = 3,
    }

    public enum TaskWaitBehavior
    {
      Synchronous = 1,
      Asynchronous = 2,
    }

    public class Tasks
    {
      public const EventTask Loop = (EventTask) 1;
      public const EventTask Invoke = (EventTask) 2;
      public const EventTask TaskExecute = (EventTask) 3;
      public const EventTask TaskWait = (EventTask) 4;
      public const EventTask ForkJoin = (EventTask) 5;
      public const EventTask TaskScheduled = (EventTask) 6;
      public const EventTask AwaitTaskContinuationScheduled = (EventTask) 7;
      public const EventTask TraceOperation = (EventTask) 8;
      public const EventTask TraceSynchronousWork = (EventTask) 9;
    }

    public class Keywords
    {
      public const EventKeywords TaskTransfer = (EventKeywords) 1;
      public const EventKeywords Tasks = (EventKeywords) 2;
      public const EventKeywords Parallel = (EventKeywords) 4;
      public const EventKeywords AsyncCausalityOperation = (EventKeywords) 8;
      public const EventKeywords AsyncCausalityRelation = (EventKeywords) 16;
      public const EventKeywords AsyncCausalitySynchronousWork = (EventKeywords) 32;
      public const EventKeywords TaskStops = (EventKeywords) 64;
      public const EventKeywords TasksFlowActivityIds = (EventKeywords) 128;
      public const EventKeywords TasksSetActivityIds = (EventKeywords) 65536;
      public const EventKeywords Debug = (EventKeywords) 131072;
      public const EventKeywords DebugActivityId = (EventKeywords) 262144;
    }
  }
}
