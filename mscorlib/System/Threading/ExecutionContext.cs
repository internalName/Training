// Decompiled with JetBrains decompiler
// Type: System.Threading.ExecutionContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Управляет контекстом выполнения текущего потока.
  ///    Этот класс не наследуется.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class ExecutionContext : IDisposable, ISerializable
  {
    private static readonly ExecutionContext s_dummyDefaultEC = new ExecutionContext(true);
    private HostExecutionContext _hostExecutionContext;
    private SynchronizationContext _syncContext;
    private SynchronizationContext _syncContextNoFlow;
    private SecurityContext _securityContext;
    [SecurityCritical]
    private LogicalCallContext _logicalCallContext;
    private IllogicalCallContext _illogicalCallContext;
    private ExecutionContext.Flags _flags;
    private Dictionary<IAsyncLocal, object> _localValues;
    private List<IAsyncLocal> _localChangeNotifications;

    internal bool isNewCapture
    {
      get
      {
        return (uint) (this._flags & (ExecutionContext.Flags.IsNewCapture | ExecutionContext.Flags.IsPreAllocatedDefault)) > 0U;
      }
      set
      {
        if (value)
          this._flags |= ExecutionContext.Flags.IsNewCapture;
        else
          this._flags &= ~ExecutionContext.Flags.IsNewCapture;
      }
    }

    internal bool isFlowSuppressed
    {
      get
      {
        return (uint) (this._flags & ExecutionContext.Flags.IsFlowSuppressed) > 0U;
      }
      set
      {
        if (value)
          this._flags |= ExecutionContext.Flags.IsFlowSuppressed;
        else
          this._flags &= ~ExecutionContext.Flags.IsFlowSuppressed;
      }
    }

    internal static ExecutionContext PreAllocatedDefault
    {
      [SecuritySafeCritical] get
      {
        return ExecutionContext.s_dummyDefaultEC;
      }
    }

    internal bool IsPreAllocatedDefault
    {
      get
      {
        return (this._flags & ExecutionContext.Flags.IsPreAllocatedDefault) != ExecutionContext.Flags.None;
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal ExecutionContext()
    {
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal ExecutionContext(bool isPreAllocatedDefault)
    {
      if (!isPreAllocatedDefault)
        return;
      this._flags = ExecutionContext.Flags.IsPreAllocatedDefault;
    }

    [SecurityCritical]
    internal static object GetLocalValue(IAsyncLocal local)
    {
      return Thread.CurrentThread.GetExecutionContextReader().GetLocalValue(local);
    }

    [SecurityCritical]
    internal static void SetLocalValue(IAsyncLocal local, object newValue, bool needChangeNotifications)
    {
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      object previousValue = (object) null;
      bool flag = executionContext._localValues != null && executionContext._localValues.TryGetValue(local, out previousValue);
      if (previousValue == newValue)
        return;
      executionContext._localValues = executionContext._localValues != null ? new Dictionary<IAsyncLocal, object>((IDictionary<IAsyncLocal, object>) executionContext._localValues) : new Dictionary<IAsyncLocal, object>();
      executionContext._localValues[local] = newValue;
      if (!needChangeNotifications)
        return;
      if (!flag)
      {
        executionContext._localChangeNotifications = executionContext._localChangeNotifications != null ? new List<IAsyncLocal>((IEnumerable<IAsyncLocal>) executionContext._localChangeNotifications) : new List<IAsyncLocal>();
        executionContext._localChangeNotifications.Add(local);
      }
      local.OnValueChanged(previousValue, newValue, false);
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    internal static void OnAsyncLocalContextChanged(ExecutionContext previous, ExecutionContext current)
    {
      List<IAsyncLocal> asyncLocalList1 = previous == null ? (List<IAsyncLocal>) null : previous._localChangeNotifications;
      if (asyncLocalList1 != null)
      {
        foreach (IAsyncLocal key in asyncLocalList1)
        {
          object previousValue = (object) null;
          if (previous != null && previous._localValues != null)
            previous._localValues.TryGetValue(key, out previousValue);
          object currentValue = (object) null;
          if (current != null && current._localValues != null)
            current._localValues.TryGetValue(key, out currentValue);
          if (previousValue != currentValue)
            key.OnValueChanged(previousValue, currentValue, true);
        }
      }
      List<IAsyncLocal> asyncLocalList2 = current == null ? (List<IAsyncLocal>) null : current._localChangeNotifications;
      if (asyncLocalList2 == null)
        return;
      if (asyncLocalList2 == asyncLocalList1)
        return;
      try
      {
        foreach (IAsyncLocal key in asyncLocalList2)
        {
          object previousValue = (object) null;
          if (previous == null || previous._localValues == null || !previous._localValues.TryGetValue(key, out previousValue))
          {
            object currentValue = (object) null;
            if (current != null && current._localValues != null)
              current._localValues.TryGetValue(key, out currentValue);
            if (previousValue != currentValue)
              key.OnValueChanged(previousValue, currentValue, true);
          }
        }
      }
      catch (Exception ex)
      {
        Environment.FailFast(Environment.GetResourceString("ExecutionContext_ExceptionInAsyncLocalNotification"), ex);
      }
    }

    internal LogicalCallContext LogicalCallContext
    {
      [SecurityCritical] get
      {
        if (this._logicalCallContext == null)
          this._logicalCallContext = new LogicalCallContext();
        return this._logicalCallContext;
      }
      [SecurityCritical] set
      {
        this._logicalCallContext = value;
      }
    }

    internal IllogicalCallContext IllogicalCallContext
    {
      get
      {
        if (this._illogicalCallContext == null)
          this._illogicalCallContext = new IllogicalCallContext();
        return this._illogicalCallContext;
      }
      set
      {
        this._illogicalCallContext = value;
      }
    }

    internal SynchronizationContext SynchronizationContext
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this._syncContext;
      }
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] set
      {
        this._syncContext = value;
      }
    }

    internal SynchronizationContext SynchronizationContextNoFlow
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this._syncContextNoFlow;
      }
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] set
      {
        this._syncContextNoFlow = value;
      }
    }

    internal HostExecutionContext HostExecutionContext
    {
      get
      {
        return this._hostExecutionContext;
      }
      set
      {
        this._hostExecutionContext = value;
      }
    }

    internal SecurityContext SecurityContext
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this._securityContext;
      }
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] set
      {
        this._securityContext = value;
        if (value == null)
          return;
        this._securityContext.ExecutionContext = this;
      }
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Threading.ExecutionContext" />.
    /// </summary>
    public void Dispose()
    {
      if (this.IsPreAllocatedDefault)
        return;
      if (this._hostExecutionContext != null)
        this._hostExecutionContext.Dispose();
      if (this._securityContext == null)
        return;
      this._securityContext.Dispose();
    }

    /// <summary>
    ///   Выполняет метод в указанном контексте выполнения в текущем потоке.
    /// </summary>
    /// <param name="executionContext">
    ///   Записываемая задача <see cref="T:System.Threading.ExecutionContext" />.
    /// </param>
    /// <param name="callback">
    ///   A <see cref="T:System.Threading.ContextCallback" /> делегат, представляющий метод, выполняемый в контексте выполнения указанных.
    /// </param>
    /// <param name="state">
    ///   Данный объект передается в метод обратного вызова.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство <paramref name="executionContext" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="executionContext" /> не был получен через операции записи.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="executionContext" /> уже используется в качестве аргумента для <see cref="M:System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext,System.Threading.ContextCallback,System.Object)" /> вызова.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void Run(ExecutionContext executionContext, ContextCallback callback, object state)
    {
      if (executionContext == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullContext"));
      if (!executionContext.isNewCapture)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
      ExecutionContext.Run(executionContext, callback, state, false);
    }

    [SecurityCritical]
    [FriendAccessAllowed]
    internal static void Run(ExecutionContext executionContext, ContextCallback callback, object state, bool preserveSyncCtx)
    {
      ExecutionContext.RunInternal(executionContext, callback, state, preserveSyncCtx);
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    internal static void RunInternal(ExecutionContext executionContext, ContextCallback callback, object state, bool preserveSyncCtx)
    {
      if (!executionContext.IsPreAllocatedDefault)
        executionContext.isNewCapture = false;
      Thread currentThread = Thread.CurrentThread;
      ExecutionContextSwitcher ecsw = new ExecutionContextSwitcher();
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
        if ((executionContextReader.IsNull || executionContextReader.IsDefaultFTContext(preserveSyncCtx)) && (SecurityContext.CurrentlyInDefaultFTSecurityContext(executionContextReader) && executionContext.IsDefaultFTContext(preserveSyncCtx)) && executionContextReader.HasSameLocalValues(executionContext))
        {
          ExecutionContext.EstablishCopyOnWriteScope(currentThread, true, ref ecsw);
        }
        else
        {
          if (executionContext.IsPreAllocatedDefault)
            executionContext = new ExecutionContext();
          ecsw = ExecutionContext.SetExecutionContext(executionContext, preserveSyncCtx);
        }
        callback(state);
      }
      finally
      {
        ecsw.Undo();
      }
    }

    [SecurityCritical]
    internal static void EstablishCopyOnWriteScope(ref ExecutionContextSwitcher ecsw)
    {
      ExecutionContext.EstablishCopyOnWriteScope(Thread.CurrentThread, false, ref ecsw);
    }

    [SecurityCritical]
    private static void EstablishCopyOnWriteScope(Thread currentThread, bool knownNullWindowsIdentity, ref ExecutionContextSwitcher ecsw)
    {
      ecsw.outerEC = currentThread.GetExecutionContextReader();
      ecsw.outerECBelongsToScope = currentThread.ExecutionContextBelongsToCurrentScope;
      ecsw.cachedAlwaysFlowImpersonationPolicy = SecurityContext.AlwaysFlowImpersonationPolicy;
      if (!knownNullWindowsIdentity)
        ecsw.wi = SecurityContext.GetCurrentWI(ecsw.outerEC, ecsw.cachedAlwaysFlowImpersonationPolicy);
      ecsw.wiIsValid = true;
      currentThread.ExecutionContextBelongsToCurrentScope = false;
      ecsw.thread = currentThread;
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static ExecutionContextSwitcher SetExecutionContext(ExecutionContext executionContext, bool preserveSyncCtx)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      ExecutionContextSwitcher executionContextSwitcher = new ExecutionContextSwitcher();
      Thread currentThread = Thread.CurrentThread;
      ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
      executionContextSwitcher.thread = currentThread;
      executionContextSwitcher.outerEC = executionContextReader;
      executionContextSwitcher.outerECBelongsToScope = currentThread.ExecutionContextBelongsToCurrentScope;
      if (preserveSyncCtx)
        executionContext.SynchronizationContext = executionContextReader.SynchronizationContext;
      executionContext.SynchronizationContextNoFlow = executionContextReader.SynchronizationContextNoFlow;
      currentThread.SetExecutionContext(executionContext, true);
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), executionContext);
        SecurityContext securityContext1 = executionContext.SecurityContext;
        if (securityContext1 != null)
        {
          SecurityContext.Reader securityContext2 = executionContextReader.SecurityContext;
          executionContextSwitcher.scsw = SecurityContext.SetSecurityContext(securityContext1, securityContext2, false, ref stackMark);
        }
        else if (!SecurityContext.CurrentlyInDefaultFTSecurityContext(executionContextSwitcher.outerEC))
        {
          SecurityContext.Reader securityContext2 = executionContextReader.SecurityContext;
          executionContextSwitcher.scsw = SecurityContext.SetSecurityContext(SecurityContext.FullTrustSecurityContext, securityContext2, false, ref stackMark);
        }
        HostExecutionContext executionContext1 = executionContext.HostExecutionContext;
        if (executionContext1 != null)
          executionContextSwitcher.hecsw = HostExecutionContextManager.SetHostExecutionContextInternal(executionContext1);
      }
      catch
      {
        executionContextSwitcher.UndoNoThrow();
        throw;
      }
      return executionContextSwitcher;
    }

    /// <summary>Создает копию текущего контекста выполнения.</summary>
    /// <returns>
    ///   <see cref="T:System.Threading.ExecutionContext" /> Объект, представляющий текущий контекст выполнения.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Не удается скопировать этот контекст, так как она используется.
    ///    Могут быть скопированы только заново зарегистрированные контексты.
    /// </exception>
    [SecuritySafeCritical]
    public ExecutionContext CreateCopy()
    {
      if (!this.isNewCapture)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotCopyUsedContext"));
      ExecutionContext executionContext = new ExecutionContext();
      executionContext.isNewCapture = true;
      executionContext._syncContext = this._syncContext == null ? (SynchronizationContext) null : this._syncContext.CreateCopy();
      executionContext._localValues = this._localValues;
      executionContext._localChangeNotifications = this._localChangeNotifications;
      executionContext._hostExecutionContext = this._hostExecutionContext == null ? (HostExecutionContext) null : this._hostExecutionContext.CreateCopy();
      if (this._securityContext != null)
      {
        executionContext._securityContext = this._securityContext.CreateCopy();
        executionContext._securityContext.ExecutionContext = executionContext;
      }
      if (this._logicalCallContext != null)
        executionContext.LogicalCallContext = (LogicalCallContext) this.LogicalCallContext.Clone();
      return executionContext;
    }

    [SecuritySafeCritical]
    internal ExecutionContext CreateMutableCopy()
    {
      ExecutionContext executionContext = new ExecutionContext();
      executionContext._syncContext = this._syncContext;
      executionContext._syncContextNoFlow = this._syncContextNoFlow;
      executionContext._hostExecutionContext = this._hostExecutionContext == null ? (HostExecutionContext) null : this._hostExecutionContext.CreateCopy();
      if (this._securityContext != null)
      {
        executionContext._securityContext = this._securityContext.CreateMutableCopy();
        executionContext._securityContext.ExecutionContext = executionContext;
      }
      if (this._logicalCallContext != null)
        executionContext.LogicalCallContext = (LogicalCallContext) this.LogicalCallContext.Clone();
      if (this._illogicalCallContext != null)
        executionContext.IllogicalCallContext = this.IllogicalCallContext.CreateCopy();
      executionContext._localValues = this._localValues;
      executionContext._localChangeNotifications = this._localChangeNotifications;
      executionContext.isFlowSuppressed = this.isFlowSuppressed;
      return executionContext;
    }

    /// <summary>
    ///   Подавляет действие контекста выполнения между асинхронными потоками.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Threading.AsyncFlowControl" /> Структура для восстановления потока.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Поток контекста уже подавлен.
    /// </exception>
    [SecurityCritical]
    public static AsyncFlowControl SuppressFlow()
    {
      if (ExecutionContext.IsFlowSuppressed())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotSupressFlowMultipleTimes"));
      AsyncFlowControl asyncFlowControl = new AsyncFlowControl();
      asyncFlowControl.Setup();
      return asyncFlowControl;
    }

    /// <summary>
    ///   Восстанавливает перемещение контекста выполнения между асинхронными потоками.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Невозможно восстановить поток контекста, поскольку не подавляется.
    /// </exception>
    [SecuritySafeCritical]
    public static void RestoreFlow()
    {
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      if (!executionContext.isFlowSuppressed)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRestoreUnsupressedFlow"));
      executionContext.isFlowSuppressed = false;
    }

    /// <summary>
    ///   Указывает ли контекста выполнения в настоящее время подавляется.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если подавляется; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool IsFlowSuppressed()
    {
      return Thread.CurrentThread.GetExecutionContextReader().IsFlowSuppressed;
    }

    /// <summary>
    ///   Перехватывает контекст выполнения из текущего потока.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Threading.ExecutionContext" /> Объект, представляющий контекст выполнения для текущего потока.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ExecutionContext Capture()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.None);
    }

    [SecuritySafeCritical]
    [FriendAccessAllowed]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static ExecutionContext FastCapture()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
    }

    [SecurityCritical]
    internal static ExecutionContext Capture(ref StackCrawlMark stackMark, ExecutionContext.CaptureOptions options)
    {
      ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
      if (executionContextReader.IsFlowSuppressed)
        return (ExecutionContext) null;
      SecurityContext securityContext = SecurityContext.Capture(executionContextReader, ref stackMark);
      HostExecutionContext executionContext1 = HostExecutionContextManager.CaptureHostExecutionContext();
      SynchronizationContext synchronizationContext = (SynchronizationContext) null;
      LogicalCallContext logicalCallContext = (LogicalCallContext) null;
      if (!executionContextReader.IsNull)
      {
        if ((options & ExecutionContext.CaptureOptions.IgnoreSyncCtx) == ExecutionContext.CaptureOptions.None)
          synchronizationContext = executionContextReader.SynchronizationContext == null ? (SynchronizationContext) null : executionContextReader.SynchronizationContext.CreateCopy();
        if (executionContextReader.LogicalCallContext.HasInfo)
          logicalCallContext = executionContextReader.LogicalCallContext.Clone();
      }
      Dictionary<IAsyncLocal, object> dictionary = (Dictionary<IAsyncLocal, object>) null;
      List<IAsyncLocal> asyncLocalList = (List<IAsyncLocal>) null;
      if (!executionContextReader.IsNull)
      {
        dictionary = executionContextReader.DangerousGetRawExecutionContext()._localValues;
        asyncLocalList = executionContextReader.DangerousGetRawExecutionContext()._localChangeNotifications;
      }
      if ((options & ExecutionContext.CaptureOptions.OptimizeDefaultCase) != ExecutionContext.CaptureOptions.None && securityContext == null && (executionContext1 == null && synchronizationContext == null) && ((logicalCallContext == null || !logicalCallContext.HasInfo) && (dictionary == null && asyncLocalList == null)))
        return ExecutionContext.s_dummyDefaultEC;
      ExecutionContext executionContext2 = new ExecutionContext();
      executionContext2.SecurityContext = securityContext;
      if (executionContext2.SecurityContext != null)
        executionContext2.SecurityContext.ExecutionContext = executionContext2;
      executionContext2._hostExecutionContext = executionContext1;
      executionContext2._syncContext = synchronizationContext;
      executionContext2.LogicalCallContext = logicalCallContext;
      executionContext2._localValues = dictionary;
      executionContext2._localChangeNotifications = asyncLocalList;
      executionContext2.isNewCapture = true;
      return executionContext2;
    }

    /// <summary>
    ///   Задает указанный <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объекта с информацией логического контекста, необходимой для повторного создания экземпляра текущего контекста выполнения.
    /// </summary>
    /// <param name="info">
    ///   <see cref="T:System.Runtime.Serialization.SerializationInfo" /> Будут заполнены сведения о сериализации объекта.
    /// </param>
    /// <param name="context">
    ///   <see cref="T:System.Runtime.Serialization.StreamingContext" /> Структура, представляющая контекст назначения сериализации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      if (this._logicalCallContext == null)
        return;
      info.AddValue("LogicalCallContext", (object) this._logicalCallContext, typeof (LogicalCallContext));
    }

    [SecurityCritical]
    private ExecutionContext(SerializationInfo info, StreamingContext context)
    {
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Name.Equals(nameof (LogicalCallContext)))
          this._logicalCallContext = (LogicalCallContext) enumerator.Value;
      }
    }

    [SecurityCritical]
    internal bool IsDefaultFTContext(bool ignoreSyncCtx)
    {
      return this._hostExecutionContext == null && (ignoreSyncCtx || this._syncContext == null) && ((this._securityContext == null || this._securityContext.IsDefaultFTSecurityContext()) && (this._logicalCallContext == null || !this._logicalCallContext.HasInfo)) && (this._illogicalCallContext == null || !this._illogicalCallContext.HasUserData);
    }

    private enum Flags
    {
      None = 0,
      IsNewCapture = 1,
      IsFlowSuppressed = 2,
      IsPreAllocatedDefault = 4,
    }

    internal struct Reader
    {
      private ExecutionContext m_ec;

      public Reader(ExecutionContext ec)
      {
        this.m_ec = ec;
      }

      public ExecutionContext DangerousGetRawExecutionContext()
      {
        return this.m_ec;
      }

      public bool IsNull
      {
        get
        {
          return this.m_ec == null;
        }
      }

      [SecurityCritical]
      public bool IsDefaultFTContext(bool ignoreSyncCtx)
      {
        return this.m_ec.IsDefaultFTContext(ignoreSyncCtx);
      }

      public bool IsFlowSuppressed
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          if (!this.IsNull)
            return this.m_ec.isFlowSuppressed;
          return false;
        }
      }

      public bool IsSame(ExecutionContext.Reader other)
      {
        return this.m_ec == other.m_ec;
      }

      public SynchronizationContext SynchronizationContext
      {
        get
        {
          if (!this.IsNull)
            return this.m_ec.SynchronizationContext;
          return (SynchronizationContext) null;
        }
      }

      public SynchronizationContext SynchronizationContextNoFlow
      {
        get
        {
          if (!this.IsNull)
            return this.m_ec.SynchronizationContextNoFlow;
          return (SynchronizationContext) null;
        }
      }

      public SecurityContext.Reader SecurityContext
      {
        [SecurityCritical, MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new SecurityContext.Reader(this.IsNull ? (SecurityContext) null : this.m_ec.SecurityContext);
        }
      }

      public LogicalCallContext.Reader LogicalCallContext
      {
        [SecurityCritical] get
        {
          return new LogicalCallContext.Reader(this.IsNull ? (LogicalCallContext) null : this.m_ec.LogicalCallContext);
        }
      }

      public IllogicalCallContext.Reader IllogicalCallContext
      {
        [SecurityCritical] get
        {
          return new IllogicalCallContext.Reader(this.IsNull ? (IllogicalCallContext) null : this.m_ec.IllogicalCallContext);
        }
      }

      [SecurityCritical]
      public object GetLocalValue(IAsyncLocal local)
      {
        if (this.IsNull)
          return (object) null;
        if (this.m_ec._localValues == null)
          return (object) null;
        object obj;
        this.m_ec._localValues.TryGetValue(local, out obj);
        return obj;
      }

      [SecurityCritical]
      public bool HasSameLocalValues(ExecutionContext other)
      {
        return (this.IsNull ? (Dictionary<IAsyncLocal, object>) null : this.m_ec._localValues) == (other == null ? (Dictionary<IAsyncLocal, object>) null : other._localValues);
      }

      [SecurityCritical]
      public bool HasLocalValues()
      {
        if (!this.IsNull)
          return this.m_ec._localValues != null;
        return false;
      }
    }

    [System.Flags]
    internal enum CaptureOptions
    {
      None = 0,
      IgnoreSyncCtx = 1,
      OptimizeDefaultCase = 2,
    }
  }
}
