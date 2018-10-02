// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.SynchronizationAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>
  ///   Реализует домен синхронизации для текущего контекста и всех контекстов, совместно использовать тот же экземпляр.
  /// </summary>
  [SecurityCritical]
  [AttributeUsage(AttributeTargets.Class)]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class SynchronizationAttribute : ContextAttribute, IContributeServerContextSink, IContributeClientContextSink
  {
    private static readonly int _timeOut = -1;
    /// <summary>
    ///   Указывает, что класс, к которому применяется этот атрибут не может создаваться в контексте, имеющем синхронизацию.
    ///    Это поле является константой.
    /// </summary>
    public const int NOT_SUPPORTED = 1;
    /// <summary>
    ///   Указывает, что класс, к которому применяется этот атрибут не зависит от того, имеет ли контекст синхронизации.
    ///    Это поле является константой.
    /// </summary>
    public const int SUPPORTED = 2;
    /// <summary>
    ///   Указывает, что класс, к которому применяется этот атрибут должен создаваться в контексте, имеющем синхронизацию.
    ///    Это поле является константой.
    /// </summary>
    public const int REQUIRED = 4;
    /// <summary>
    ///   Указывает, что класс, к которому применяется этот атрибут необходимо создавать в контексте с новым экземпляром свойства синхронизации каждый раз.
    ///    Это поле является константой.
    /// </summary>
    public const int REQUIRES_NEW = 8;
    private const string PROPERTY_NAME = "Synchronization";
    [NonSerialized]
    internal AutoResetEvent _asyncWorkEvent;
    [NonSerialized]
    private RegisteredWaitHandle _waitHandle;
    [NonSerialized]
    internal Queue _workItemQueue;
    [NonSerialized]
    internal bool _locked;
    internal bool _bReEntrant;
    internal int _flavor;
    [NonSerialized]
    private SynchronizationAttribute _cliCtxAttr;
    [NonSerialized]
    private string _syncLcid;
    [NonSerialized]
    private ArrayList _asyncLcidList;

    /// <summary>
    ///   Возвращает или задает логическое значение, указывающее, является ли <see cref="T:System.Runtime.Remoting.Contexts.Context" /> реализующий экземпляр <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> заблокирован.
    /// </summary>
    /// <returns>
    ///   Логическое значение, указывающее, является ли <see cref="T:System.Runtime.Remoting.Contexts.Context" /> реализующий экземпляр <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> заблокирован.
    /// </returns>
    public virtual bool Locked
    {
      get
      {
        return this._locked;
      }
      set
      {
        this._locked = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает логическое значение, указывающее, необходим ли повторный ввод.
    /// </summary>
    /// <returns>
    ///   Логическое значение, указывающее, необходим ли повторный ввод.
    /// </returns>
    public virtual bool IsReEntrant
    {
      get
      {
        return this._bReEntrant;
      }
    }

    internal string SyncCallOutLCID
    {
      get
      {
        return this._syncLcid;
      }
      set
      {
        this._syncLcid = value;
      }
    }

    internal ArrayList AsyncCallOutLCIDList
    {
      get
      {
        return this._asyncLcidList;
      }
    }

    internal bool IsKnownLCID(IMessage reqMsg)
    {
      string logicalCallId = ((LogicalCallContext) reqMsg.Properties[(object) Message.CallContextKey]).RemotingData.LogicalCallID;
      if (!logicalCallId.Equals(this._syncLcid))
        return this._asyncLcidList.Contains((object) logicalCallId);
      return true;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> со значениями по умолчанию.
    /// </summary>
    public SynchronizationAttribute()
      : this(4, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> класса логическое значение, указывающее, необходим ли повторный ввод.
    /// </summary>
    /// <param name="reEntrant">
    ///   Логическое значение, указывающее, необходим ли повторный ввод.
    /// </param>
    public SynchronizationAttribute(bool reEntrant)
      : this(4, reEntrant)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> класса с флагом, указывающим поведение объекта, к которому применяется этот атрибут.
    /// </summary>
    /// <param name="flag">
    ///   Целое число, указывающее поведение объекта, к которому применяется этот атрибут.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="flag" /> Параметр не является одним из определенных флагов.
    /// </exception>
    public SynchronizationAttribute(int flag)
      : this(flag, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> с флагом, указывающим поведение объекта, к которому применяется этот атрибут и логическое значение, указывающее, необходим ли повторный ввод.
    /// </summary>
    /// <param name="flag">
    ///   Целое число, указывающее поведение объекта, к которому применяется этот атрибут.
    /// </param>
    /// <param name="reEntrant">
    ///   <see langword="true" /> Если требуется повторный ввод и выноски должны перехватываться и упорядочиваться; в противном случае — <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="flag" /> Параметр не является одним из определенных флагов.
    /// </exception>
    public SynchronizationAttribute(int flag, bool reEntrant)
      : base("Synchronization")
    {
      this._bReEntrant = reEntrant;
      switch (flag)
      {
        case 1:
        case 2:
        case 4:
        case 8:
          this._flavor = flag;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (flag));
      }
    }

    internal void Dispose()
    {
      if (this._waitHandle == null)
        return;
      this._waitHandle.Unregister((WaitHandle) null);
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, является ли параметр контекста требованиям атрибута контекста.
    /// </summary>
    /// <param name="ctx">Контекст для проверки.</param>
    /// <param name="msg">
    ///   Сведения, собранные во время создания контекстно-связанного объекта, отмеченного этим атрибутом.
    ///   <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> Проверки, добавление и удаление свойства из контекста при определении приемлемость контекста.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если переданный контекст приемлем; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="ctx" /> или параметра <paramref name="msg" /> — <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [ComVisible(true)]
    public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
    {
      if (ctx == null)
        throw new ArgumentNullException(nameof (ctx));
      if (msg == null)
        throw new ArgumentNullException(nameof (msg));
      bool flag = true;
      if (this._flavor == 8)
      {
        flag = false;
      }
      else
      {
        SynchronizationAttribute property = (SynchronizationAttribute) ctx.GetProperty("Synchronization");
        if (this._flavor == 1 && property != null || this._flavor == 4 && property == null)
          flag = false;
        if (this._flavor == 4)
          this._cliCtxAttr = property;
      }
      return flag;
    }

    /// <summary>
    ///   Добавляет <see langword="Synchronized" /> свойство контекста в указанный <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.
    /// </summary>
    /// <param name="ctorMsg">
    ///   <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> К которому необходимо добавить свойство.
    /// </param>
    [SecurityCritical]
    [ComVisible(true)]
    public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
    {
      if (this._flavor == 1 || this._flavor == 2 || ctorMsg == null)
        return;
      if (this._cliCtxAttr != null)
      {
        ctorMsg.ContextProperties.Add((object) this._cliCtxAttr);
        this._cliCtxAttr = (SynchronizationAttribute) null;
      }
      else
        ctorMsg.ContextProperties.Add((object) this);
    }

    internal virtual void InitIfNecessary()
    {
      lock (this)
      {
        if (this._asyncWorkEvent != null)
          return;
        this._asyncWorkEvent = new AutoResetEvent(false);
        this._workItemQueue = new Queue();
        this._asyncLcidList = new ArrayList();
        this._waitHandle = ThreadPool.RegisterWaitForSingleObject((WaitHandle) this._asyncWorkEvent, new WaitOrTimerCallback(this.DispatcherCallBack), (object) null, SynchronizationAttribute._timeOut, false);
      }
    }

    private void DispatcherCallBack(object stateIgnored, bool ignored)
    {
      WorkItem work;
      lock (this._workItemQueue)
        work = (WorkItem) this._workItemQueue.Dequeue();
      this.ExecuteWorkItem(work);
      this.HandleWorkCompletion();
    }

    internal virtual void HandleThreadExit()
    {
      this.HandleWorkCompletion();
    }

    internal virtual void HandleThreadReEntry()
    {
      WorkItem work = new WorkItem((IMessage) null, (IMessageSink) null, (IMessageSink) null);
      work.SetDummy();
      this.HandleWorkRequest(work);
    }

    internal virtual void HandleWorkCompletion()
    {
      WorkItem workItem = (WorkItem) null;
      bool flag = false;
      lock (this._workItemQueue)
      {
        if (this._workItemQueue.Count >= 1)
        {
          workItem = (WorkItem) this._workItemQueue.Peek();
          flag = true;
          workItem.SetSignaled();
        }
        else
          this._locked = false;
      }
      if (!flag)
        return;
      if (workItem.IsAsync())
      {
        this._asyncWorkEvent.Set();
      }
      else
      {
        lock (workItem)
          Monitor.Pulse((object) workItem);
      }
    }

    internal virtual void HandleWorkRequest(WorkItem work)
    {
      if (!this.IsNestedCall(work._reqMsg))
      {
        if (work.IsAsync())
        {
          lock (this._workItemQueue)
          {
            work.SetWaiting();
            this._workItemQueue.Enqueue((object) work);
            if (this._locked || this._workItemQueue.Count != 1)
              return;
            work.SetSignaled();
            this._locked = true;
            this._asyncWorkEvent.Set();
          }
        }
        else
        {
          lock (work)
          {
            bool flag;
            lock (this._workItemQueue)
            {
              if (!this._locked && this._workItemQueue.Count == 0)
              {
                this._locked = true;
                flag = false;
              }
              else
              {
                flag = true;
                work.SetWaiting();
                this._workItemQueue.Enqueue((object) work);
              }
            }
            if (flag)
            {
              Monitor.Wait((object) work);
              if (!work.IsDummy())
              {
                this.DispatcherCallBack((object) null, true);
              }
              else
              {
                lock (this._workItemQueue)
                  this._workItemQueue.Dequeue();
              }
            }
            else
            {
              if (work.IsDummy())
                return;
              work.SetSignaled();
              this.ExecuteWorkItem(work);
              this.HandleWorkCompletion();
            }
          }
        }
      }
      else
      {
        work.SetSignaled();
        work.Execute();
      }
    }

    internal void ExecuteWorkItem(WorkItem work)
    {
      work.Execute();
    }

    internal bool IsNestedCall(IMessage reqMsg)
    {
      bool flag = false;
      if (!this.IsReEntrant)
      {
        string syncCallOutLcid = this.SyncCallOutLCID;
        if (syncCallOutLcid != null)
        {
          LogicalCallContext property = (LogicalCallContext) reqMsg.Properties[(object) Message.CallContextKey];
          if (property != null && syncCallOutLcid.Equals(property.RemotingData.LogicalCallID))
            flag = true;
        }
        if (!flag && this.AsyncCallOutLCIDList.Count > 0 && this.AsyncCallOutLCIDList.Contains((object) ((LogicalCallContext) reqMsg.Properties[(object) Message.CallContextKey]).RemotingData.LogicalCallID))
          flag = true;
      }
      return flag;
    }

    /// <summary>
    ///   Создает синхронизированный диспетчерский сток и добавляет его перед предоставленной цепочке приемников на границе контекста на серверной стороне вызова удаленного взаимодействия.
    /// </summary>
    /// <param name="nextSink">
    ///   Сформированная на данный момент цепочка приемников.
    /// </param>
    /// <returns>
    ///   Составная цепочка приемников с новым синхронизированным диспетчерским приемником.
    /// </returns>
    [SecurityCritical]
    public virtual IMessageSink GetServerContextSink(IMessageSink nextSink)
    {
      this.InitIfNecessary();
      return (IMessageSink) new SynchronizedServerContextSink(this, nextSink);
    }

    /// <summary>
    ///   Создает приемник внешних вызовов и добавляет его перед предоставленной цепочке приемников на границе контекста на клиентской стороне вызова удаленного взаимодействия.
    /// </summary>
    /// <param name="nextSink">
    ///   Сформированная на данный момент цепочка приемников.
    /// </param>
    /// <returns>
    ///   Составная цепочка приемников с новым приемников внешних вызовов.
    /// </returns>
    [SecurityCritical]
    public virtual IMessageSink GetClientContextSink(IMessageSink nextSink)
    {
      this.InitIfNecessary();
      return (IMessageSink) new SynchronizedClientContextSink(this, nextSink);
    }
  }
}
