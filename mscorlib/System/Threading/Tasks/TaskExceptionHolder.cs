// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskExceptionHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading.Tasks
{
  internal class TaskExceptionHolder
  {
    private static readonly bool s_failFastOnUnobservedException = TaskExceptionHolder.ShouldFailFastOnUnobservedException();
    private static volatile bool s_domainUnloadStarted;
    private static volatile EventHandler s_adUnloadEventHandler;
    private readonly Task m_task;
    private volatile List<ExceptionDispatchInfo> m_faultExceptions;
    private ExceptionDispatchInfo m_cancellationException;
    private volatile bool m_isHandled;

    internal TaskExceptionHolder(Task task)
    {
      this.m_task = task;
      TaskExceptionHolder.EnsureADUnloadCallbackRegistered();
    }

    [SecuritySafeCritical]
    private static bool ShouldFailFastOnUnobservedException()
    {
      return CLRConfig.CheckThrowUnobservedTaskExceptions();
    }

    private static void EnsureADUnloadCallbackRegistered()
    {
      if (TaskExceptionHolder.s_adUnloadEventHandler != null || Interlocked.CompareExchange<EventHandler>(ref TaskExceptionHolder.s_adUnloadEventHandler, new EventHandler(TaskExceptionHolder.AppDomainUnloadCallback), (EventHandler) null) != null)
        return;
      AppDomain.CurrentDomain.DomainUnload += TaskExceptionHolder.s_adUnloadEventHandler;
    }

    private static void AppDomainUnloadCallback(object sender, EventArgs e)
    {
      TaskExceptionHolder.s_domainUnloadStarted = true;
    }

    ~TaskExceptionHolder()
    {
      if (this.m_faultExceptions == null || this.m_isHandled || (Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload()) || TaskExceptionHolder.s_domainUnloadStarted)
        return;
      foreach (ExceptionDispatchInfo faultException in this.m_faultExceptions)
      {
        Exception sourceException = faultException.SourceException;
        AggregateException aggregateException = sourceException as AggregateException;
        if (aggregateException != null)
        {
          foreach (Exception innerException in aggregateException.Flatten().InnerExceptions)
          {
            if (innerException is ThreadAbortException)
              return;
          }
        }
        else if (sourceException is ThreadAbortException)
          return;
      }
      AggregateException exception = new AggregateException(Environment.GetResourceString("TaskExceptionHolder_UnhandledException"), (IEnumerable<ExceptionDispatchInfo>) this.m_faultExceptions);
      UnobservedTaskExceptionEventArgs ueea = new UnobservedTaskExceptionEventArgs(exception);
      TaskScheduler.PublishUnobservedTaskException((object) this.m_task, ueea);
      if (TaskExceptionHolder.s_failFastOnUnobservedException && !ueea.m_observed)
        throw exception;
    }

    internal bool ContainsFaultList
    {
      get
      {
        return this.m_faultExceptions != null;
      }
    }

    internal void Add(object exceptionObject)
    {
      this.Add(exceptionObject, false);
    }

    internal void Add(object exceptionObject, bool representsCancellation)
    {
      if (representsCancellation)
        this.SetCancellationException(exceptionObject);
      else
        this.AddFaultException(exceptionObject);
    }

    private void SetCancellationException(object exceptionObject)
    {
      OperationCanceledException canceledException = exceptionObject as OperationCanceledException;
      this.m_cancellationException = canceledException == null ? exceptionObject as ExceptionDispatchInfo : ExceptionDispatchInfo.Capture((Exception) canceledException);
      this.MarkAsHandled(false);
    }

    private void AddFaultException(object exceptionObject)
    {
      List<ExceptionDispatchInfo> exceptionDispatchInfoList = this.m_faultExceptions;
      if (exceptionDispatchInfoList == null)
        this.m_faultExceptions = exceptionDispatchInfoList = new List<ExceptionDispatchInfo>(1);
      Exception source1 = exceptionObject as Exception;
      if (source1 != null)
      {
        exceptionDispatchInfoList.Add(ExceptionDispatchInfo.Capture(source1));
      }
      else
      {
        ExceptionDispatchInfo exceptionDispatchInfo = exceptionObject as ExceptionDispatchInfo;
        if (exceptionDispatchInfo != null)
        {
          exceptionDispatchInfoList.Add(exceptionDispatchInfo);
        }
        else
        {
          IEnumerable<Exception> exceptions = exceptionObject as IEnumerable<Exception>;
          if (exceptions != null)
          {
            foreach (Exception source2 in exceptions)
              exceptionDispatchInfoList.Add(ExceptionDispatchInfo.Capture(source2));
          }
          else
          {
            IEnumerable<ExceptionDispatchInfo> collection = exceptionObject as IEnumerable<ExceptionDispatchInfo>;
            if (collection == null)
              throw new ArgumentException(Environment.GetResourceString("TaskExceptionHolder_UnknownExceptionType"), nameof (exceptionObject));
            exceptionDispatchInfoList.AddRange(collection);
          }
        }
      }
      for (int index = 0; index < exceptionDispatchInfoList.Count; ++index)
      {
        Type type = exceptionDispatchInfoList[index].SourceException.GetType();
        if (type != typeof (ThreadAbortException) && type != typeof (AppDomainUnloadedException))
        {
          this.MarkAsUnhandled();
          break;
        }
        if (index == exceptionDispatchInfoList.Count - 1)
          this.MarkAsHandled(false);
      }
    }

    private void MarkAsUnhandled()
    {
      if (!this.m_isHandled)
        return;
      GC.ReRegisterForFinalize((object) this);
      this.m_isHandled = false;
    }

    internal void MarkAsHandled(bool calledFromFinalizer)
    {
      if (this.m_isHandled)
        return;
      if (!calledFromFinalizer)
        GC.SuppressFinalize((object) this);
      this.m_isHandled = true;
    }

    internal AggregateException CreateExceptionObject(bool calledFromFinalizer, Exception includeThisException)
    {
      List<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
      this.MarkAsHandled(calledFromFinalizer);
      if (includeThisException == null)
        return new AggregateException((IEnumerable<ExceptionDispatchInfo>) faultExceptions);
      Exception[] exceptionArray = new Exception[faultExceptions.Count + 1];
      for (int index = 0; index < exceptionArray.Length - 1; ++index)
        exceptionArray[index] = faultExceptions[index].SourceException;
      exceptionArray[exceptionArray.Length - 1] = includeThisException;
      return new AggregateException(exceptionArray);
    }

    internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
    {
      List<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
      this.MarkAsHandled(false);
      return new ReadOnlyCollection<ExceptionDispatchInfo>((IList<ExceptionDispatchInfo>) faultExceptions);
    }

    internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
    {
      return this.m_cancellationException;
    }
  }
}
