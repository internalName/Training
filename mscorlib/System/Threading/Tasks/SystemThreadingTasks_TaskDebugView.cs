﻿// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.SystemThreadingTasks_TaskDebugView
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal class SystemThreadingTasks_TaskDebugView
  {
    private Task m_task;

    public SystemThreadingTasks_TaskDebugView(Task task)
    {
      this.m_task = task;
    }

    public object AsyncState
    {
      get
      {
        return this.m_task.AsyncState;
      }
    }

    public TaskCreationOptions CreationOptions
    {
      get
      {
        return this.m_task.CreationOptions;
      }
    }

    public Exception Exception
    {
      get
      {
        return (Exception) this.m_task.Exception;
      }
    }

    public int Id
    {
      get
      {
        return this.m_task.Id;
      }
    }

    public bool CancellationPending
    {
      get
      {
        if (this.m_task.Status == TaskStatus.WaitingToRun)
          return this.m_task.CancellationToken.IsCancellationRequested;
        return false;
      }
    }

    public TaskStatus Status
    {
      get
      {
        return this.m_task.Status;
      }
    }
  }
}
