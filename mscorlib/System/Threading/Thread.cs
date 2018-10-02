// Decompiled with JetBrains decompiler
// Type: System.Threading.Thread
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Threading
{
  /// <summary>
  ///   Создает и контролирует поток, задает приоритет и возвращает статус.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Thread))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class Thread : CriticalFinalizerObject, _Thread
  {
    private Context m_Context;
    private ExecutionContext m_ExecutionContext;
    private string m_Name;
    private Delegate m_Delegate;
    private CultureInfo m_CurrentCulture;
    private CultureInfo m_CurrentUICulture;
    private object m_ThreadStartArg;
    private IntPtr DONT_USE_InternalThread;
    private int m_Priority;
    private int m_ManagedThreadId;
    private bool m_ExecutionContextBelongsToOuterScope;
    private static LocalDataStoreMgr s_LocalDataStoreMgr;
    [ThreadStatic]
    private static LocalDataStoreHolder s_LocalDataStore;
    private static AsyncLocal<CultureInfo> s_asyncLocalCurrentCulture;
    private static AsyncLocal<CultureInfo> s_asyncLocalCurrentUICulture;

    private static void AsyncLocalSetCurrentCulture(AsyncLocalValueChangedArgs<CultureInfo> args)
    {
      Thread.CurrentThread.m_CurrentCulture = args.CurrentValue;
    }

    private static void AsyncLocalSetCurrentUICulture(AsyncLocalValueChangedArgs<CultureInfo> args)
    {
      Thread.CurrentThread.m_CurrentUICulture = args.CurrentValue;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Thread" />.
    /// </summary>
    /// <param name="start">
    ///   Делегат <see cref="T:System.Threading.ThreadStart" />, указывающий на методы, которые вызываются при запуске потока.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="start" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public Thread(ThreadStart start)
    {
      if (start == null)
        throw new ArgumentNullException(nameof (start));
      this.SetStartHelper((Delegate) start, 0);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Thread" />, указывая максимальный размер стека для потока.
    /// </summary>
    /// <param name="start">
    ///   Делегат <see cref="T:System.Threading.ThreadStart" />, указывающий на методы, которые вызываются при запуске потока.
    /// </param>
    /// <param name="maxStackSize">
    ///   Максимальный размер стека в байтах, используемый потоком, или же 0 для использования максимального размера по умолчанию, указывается в заголовке исполняемого файла.
    /// 
    ///   Внимание! Для частично доверенного кода значение параметра <paramref name="maxStackSize" /> игнорируется, если оно превышает размер стека по умолчанию.
    ///    Исключение не возникает.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="start" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="maxStackSize" /> меньше нуля.
    /// </exception>
    [SecuritySafeCritical]
    public Thread(ThreadStart start, int maxStackSize)
    {
      if (start == null)
        throw new ArgumentNullException(nameof (start));
      if (0 > maxStackSize)
        throw new ArgumentOutOfRangeException(nameof (maxStackSize), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.SetStartHelper((Delegate) start, maxStackSize);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Thread" />, при этом указывается делегат, позволяющий объекту быть переданным в поток при запуске потока.
    /// </summary>
    /// <param name="start">
    ///   Делегат, указывающий на методы, которые вызываются при запуске потока.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="start" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public Thread(ParameterizedThreadStart start)
    {
      if (start == null)
        throw new ArgumentNullException(nameof (start));
      this.SetStartHelper((Delegate) start, 0);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.Thread" />, при этом указывается делегат, позволяющий объекту быть переданным в поток при запуске потока с указанием максимального размера стека для потока.
    /// </summary>
    /// <param name="start">
    ///   Делегат <see cref="T:System.Threading.ParameterizedThreadStart" />, указывающий на методы, которые вызываются при запуске потока.
    /// </param>
    /// <param name="maxStackSize">
    ///   Максимальный размер стека в байтах, используемый потоком, или же 0 для использования максимального размера по умолчанию, указывается в заголовке исполняемого файла.
    /// 
    ///   Внимание! Для частично доверенного кода значение параметра <paramref name="maxStackSize" /> игнорируется, если оно превышает размер стека по умолчанию.
    ///    Исключение не возникает.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="start" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="maxStackSize" /> меньше нуля.
    /// </exception>
    [SecuritySafeCritical]
    public Thread(ParameterizedThreadStart start, int maxStackSize)
    {
      if (start == null)
        throw new ArgumentNullException(nameof (start));
      if (0 > maxStackSize)
        throw new ArgumentOutOfRangeException(nameof (maxStackSize), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.SetStartHelper((Delegate) start, maxStackSize);
    }

    /// <summary>Возвращает хэш-код текущего потока.</summary>
    /// <returns>Целочисленное значение хэш-кода.</returns>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      return this.m_ManagedThreadId;
    }

    /// <summary>
    ///   Возвращает уникальный идентификатор текущего управляемого потока.
    /// </summary>
    /// <returns>
    ///   Целочисленное значение, представляющее уникальный идентификатор для этого управляемого потока.
    /// </returns>
    [__DynamicallyInvokable]
    public extern int ManagedThreadId { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    internal ThreadHandle GetNativeHandle()
    {
      IntPtr useInternalThread = this.DONT_USE_InternalThread;
      if (useInternalThread.IsNull())
        throw new ArgumentException((string) null, Environment.GetResourceString("Argument_InvalidHandle"));
      return new ThreadHandle(useInternalThread);
    }

    /// <summary>
    ///   Вынуждает операционную систему изменить состояние текущего экземпляра на <see cref="F:System.Threading.ThreadState.Running" />.
    /// </summary>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Поток уже запущен.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти для запуска этого потока.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public void Start()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.Start(ref stackMark);
    }

    /// <summary>
    ///   Заставляет операционную систему изменить состояние текущего экземпляра на <see cref="F:System.Threading.ThreadState.Running" />, а также (необязательно) передает объект с данными, используемыми методом в потоке.
    /// </summary>
    /// <param name="parameter">
    ///   Объект, содержащий данные, используемые методом, который выполняется потоком.
    /// </param>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Поток уже запущен.
    /// </exception>
    /// <exception cref="T:System.OutOfMemoryException">
    ///   Недостаточно памяти для запуска этого потока.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот поток был создан с помощью делегата <see cref="T:System.Threading.ThreadStart" />, а не делегата <see cref="T:System.Threading.ParameterizedThreadStart" />.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public void Start(object parameter)
    {
      if (this.m_Delegate is ThreadStart)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ThreadWrongThreadStart"));
      this.m_ThreadStartArg = parameter;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.Start(ref stackMark);
    }

    [SecuritySafeCritical]
    private void Start(ref StackCrawlMark stackMark)
    {
      this.StartupSetApartmentStateInternal();
      if ((object) this.m_Delegate != null)
        ((ThreadHelper) this.m_Delegate.Target).SetExecutionContextHelper(ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx));
      this.StartInternal(CallContext.Principal, ref stackMark);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal ExecutionContext.Reader GetExecutionContextReader()
    {
      return new ExecutionContext.Reader(this.m_ExecutionContext);
    }

    internal bool ExecutionContextBelongsToCurrentScope
    {
      get
      {
        return !this.m_ExecutionContextBelongsToOuterScope;
      }
      set
      {
        this.m_ExecutionContextBelongsToOuterScope = !value;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Threading.ExecutionContext" />, содержащий сведения о различных контекстах текущего потока.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.ExecutionContext" />, содержащий консолидированную информацию о контекстах текущего потока.
    /// </returns>
    public ExecutionContext ExecutionContext
    {
      [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)] get
      {
        return this != Thread.CurrentThread ? this.m_ExecutionContext : this.GetMutableExecutionContext();
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal ExecutionContext GetMutableExecutionContext()
    {
      if (this.m_ExecutionContext == null)
        this.m_ExecutionContext = new ExecutionContext();
      else if (!this.ExecutionContextBelongsToCurrentScope)
        this.m_ExecutionContext = this.m_ExecutionContext.CreateMutableCopy();
      this.ExecutionContextBelongsToCurrentScope = true;
      return this.m_ExecutionContext;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal void SetExecutionContext(ExecutionContext value, bool belongsToCurrentScope)
    {
      this.m_ExecutionContext = value;
      this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal void SetExecutionContext(ExecutionContext.Reader value, bool belongsToCurrentScope)
    {
      this.m_ExecutionContext = value.DangerousGetRawExecutionContext();
      this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void StartInternal(IPrincipal principal, ref StackCrawlMark stackMark);

    /// <summary>
    ///   Применяет записанное значение <see cref="T:System.Threading.CompressedStack" /> к текущему потоку.
    /// </summary>
    /// <param name="stack">
    ///   Объект <see cref="T:System.Threading.CompressedStack" />, который будет применен к текущему потоку.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Во всех случаях.
    /// </exception>
    [SecurityCritical]
    [Obsolete("Thread.SetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
    public void SetCompressedStack(CompressedStack stack)
    {
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ThreadAPIsNotSupported"));
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern IntPtr SetAppDomainStack(SafeCompressedStackHandle csHandle);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern void RestoreAppDomainStack(IntPtr appDomainStack);

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Threading.CompressedStack" />, который может быть использован для записи стека текущего потока.
    /// </summary>
    /// <returns>Отсутствует.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Во всех случаях.
    /// </exception>
    [SecurityCritical]
    [Obsolete("Thread.GetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
    public CompressedStack GetCompressedStack()
    {
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ThreadAPIsNotSupported"));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr InternalGetCurrentThread();

    /// <summary>
    ///   Вызывает исключение <see cref="T:System.Threading.ThreadAbortException" /> в вызвавшем его потоке для того, чтобы начать процесс завершения потока, в то же время предоставляя сведения об исключении касательно исключения завершения потока.
    ///    Вызов данного метода обычно завершает поток.
    /// </summary>
    /// <param name="stateInfo">
    ///   Объект, который содержит информацию об определенном приложении, например состояние, которое может использоваться аварийно завершающимся потоком.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Поток, который прерывается, в настоящий момент приостановлен.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public void Abort(object stateInfo)
    {
      this.AbortReason = stateInfo;
      this.AbortInternal();
    }

    /// <summary>
    ///   Вызывает исключение <see cref="T:System.Threading.ThreadAbortException" /> в вызвавшем его потоке для того, чтобы начать процесс завершения потока.
    ///    Вызов данного метода обычно завершает поток.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Поток, который прерывается, в настоящий момент приостановлен.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public void Abort()
    {
      this.AbortInternal();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void AbortInternal();

    /// <summary>
    ///   Отменяет метод <see cref="M:System.Threading.Thread.Abort(System.Object)" />, запрошенный для текущего потока.
    /// </summary>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Метод <see langword="Abort" /> не был вызван в текущем потоке.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающая сторона не имеет требуемого разрешения безопасности для текущего потока.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public static void ResetAbort()
    {
      Thread currentThread = Thread.CurrentThread;
      if ((currentThread.ThreadState & ThreadState.AbortRequested) == ThreadState.Running)
        throw new ThreadStateException(Environment.GetResourceString("ThreadState_NoAbortRequested"));
      currentThread.ResetAbortNative();
      currentThread.ClearAbortReason();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void ResetAbortNative();

    /// <summary>
    ///   Приостанавливает работу потока; если работа потока уже приостановлена, не оказывает влияния.
    /// </summary>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Поток не запущен или удален.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует соответствующее разрешение <see cref="T:System.Security.Permissions.SecurityPermission" />.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Thread.Suspend has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public void Suspend()
    {
      this.SuspendInternal();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void SuspendInternal();

    /// <summary>Возобновляет приостановленную работу потока.</summary>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Поток не был запущен, бездействует или не находится в приостановленном состоянии.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует соответствующее разрешение <see cref="T:System.Security.Permissions.SecurityPermission" />.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Thread.Resume has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public void Resume()
    {
      this.ResumeInternal();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void ResumeInternal();

    /// <summary>
    ///   Прерывает работу потока, находящегося в состоянии <see langword="WaitSleepJoin" />.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует соответствующее разрешение <see cref="T:System.Security.Permissions.SecurityPermission" />.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public void Interrupt()
    {
      this.InterruptInternal();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void InterruptInternal();

    /// <summary>
    ///   Возвращает или задает значение, указывающее на планируемый приоритет потока.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Threading.ThreadPriority" />.
    ///    Значение по умолчанию — <see cref="F:System.Threading.ThreadPriority.Normal" />.
    /// </returns>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Поток достиг конечного состояния, например <see cref="F:System.Threading.ThreadState.Aborted" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение, указанное для операции задания, не является допустимым значением <see cref="T:System.Threading.ThreadPriority" />.
    /// </exception>
    public ThreadPriority Priority
    {
      [SecuritySafeCritical] get
      {
        return (ThreadPriority) this.GetPriorityNative();
      }
      [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true)] set
      {
        this.SetPriorityNative((int) value);
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern int GetPriorityNative();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void SetPriorityNative(int priority);

    /// <summary>
    ///   Возвращает значение, показывающее статус выполнения текущего потока.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если этот поток был запущен и не был завершен обычным образом либо был прерван; в противном случае — значение <see langword="false" />.
    /// </returns>
    public extern bool IsAlive { [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>
    ///   Возвращает значение, показывающее, принадлежит ли поток к группе управляемых потоков.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если этот поток принадлежит группе управляемых потоков; в противном случае — значение <see langword="false" />.
    /// </returns>
    public extern bool IsThreadPoolThread { [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)] get; }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern bool JoinInternal(int millisecondsTimeout);

    /// <summary>
    ///   Блокирует вызывающий поток до завершения потока, представленного экземпляром, продолжая отправлять стандартные сообщения COM и <see langword="SendMessage" />.
    /// </summary>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Вызывающая сторона пыталась присоединиться к потоку, который находится в состоянии <see cref="F:System.Threading.ThreadState.Unstarted" />.
    /// </exception>
    /// <exception cref="T:System.Threading.ThreadInterruptedException">
    ///   Выполнение потока прервано во время ожидания.
    /// </exception>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public void Join()
    {
      this.JoinInternal(-1);
    }

    /// <summary>
    ///   Блокирует вызывающий поток до завершения потока, представленного экземпляром, или истечения указанного времени, продолжая отправлять стандартные сообщения COM и SendMessage.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Количество миллисекунд ожидания завершения потока.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если поток завершился; значение <see langword="false" />, если поток не завершился по истечении количества времени, заданного параметром <paramref name="millisecondsTimeout" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="millisecondsTimeout" /> является отрицательным и не равно <see cref="F:System.Threading.Timeout.Infinite" /> (в миллисекундах).
    /// </exception>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Поток не запущен.
    /// </exception>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public bool Join(int millisecondsTimeout)
    {
      return this.JoinInternal(millisecondsTimeout);
    }

    /// <summary>
    ///   Блокирует вызывающий поток до завершения потока, представленного экземпляром, или истечения указанного времени, продолжая отправлять стандартные сообщения COM и SendMessage.
    /// </summary>
    /// <param name="timeout">
    ///   Объект <see cref="T:System.TimeSpan" />, в качестве значения которого задано время ожидания завершения процесса.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если поток завершился; <see langword="false" />, если поток не завершился по истечении количества времени, заданного параметром <paramref name="timeout" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="timeout" /> является отрицательным и не равно <see cref="F:System.Threading.Timeout.Infinite" /> (в миллисекундах) или больше <see cref="F:System.Int32.MaxValue" /> миллисекунд.
    /// </exception>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Вызывающая сторона пыталась присоединиться к потоку, который находится в состоянии <see cref="F:System.Threading.ThreadState.Unstarted" />.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public bool Join(TimeSpan timeout)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return this.Join((int) totalMilliseconds);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void SleepInternal(int millisecondsTimeout);

    /// <summary>
    ///   Приостанавливает текущий поток на заданное количество миллисекунд.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Количество миллисекунд, на которое приостанавливается поток.
    ///    Если значение аргумента <paramref name="millisecondsTimeout" /> равно нулю, поток освобождает оставшуюся часть своего интервала времени для любого потока с таким же приоритетом, готовым к выполнению.
    ///    Если других готовых к выполнению потоков с таким же приоритетом нет, выполнение текущего потока не приостанавливается.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение времени ожидания является отрицательной величиной и не равно <see cref="F:System.Threading.Timeout.Infinite" />.
    /// </exception>
    [SecuritySafeCritical]
    public static void Sleep(int millisecondsTimeout)
    {
      Thread.SleepInternal(millisecondsTimeout);
      if (!AppDomainPauseManager.IsPaused)
        return;
      AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
    }

    /// <summary>Приостанавливает текущий поток на заданное время.</summary>
    /// <param name="timeout">
    ///   Время, на которое приостанавливается поток.
    ///    Если значение аргумента <paramref name="millisecondsTimeout" /> равно <see cref="F:System.TimeSpan.Zero" />, поток освобождает оставшуюся часть своего интервала времени для любого потока с таким же приоритетом, готовым к выполнению.
    ///    Если других готовых к выполнению потоков с таким же приоритетом нет, выполнение текущего потока не приостанавливается.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="timeout" /> является отрицательным и не равно <see cref="F:System.Threading.Timeout.Infinite" /> (в миллисекундах) или больше <see cref="F:System.Int32.MaxValue" /> миллисекунд.
    /// </exception>
    public static void Sleep(TimeSpan timeout)
    {
      long totalMilliseconds = (long) timeout.TotalMilliseconds;
      if (totalMilliseconds < -1L || totalMilliseconds > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (timeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      Thread.Sleep((int) totalMilliseconds);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    private static extern void SpinWaitInternal(int iterations);

    /// <summary>
    ///   Вынуждает поток выполнять ожидание столько раз, сколько определено параметром <paramref name="iterations" />.
    /// </summary>
    /// <param name="iterations">
    ///   32-разрядное знаковое целое число, определяющее, как долго потоку ожидать.
    /// </param>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public static void SpinWait(int iterations)
    {
      Thread.SpinWaitInternal(iterations);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    private static extern bool YieldInternal();

    /// <summary>
    ///   Позволяет вызвавшему потоку передать выполнение другому потоку, готовому к использованию на текущем процессоре.
    ///    Операционная система выбирает, какому потоку передается выполнение.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если операционная система переключила выполнение на другой поток, в противном случае — значение <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public static bool Yield()
    {
      return Thread.YieldInternal();
    }

    /// <summary>Возвращает выполняющийся в данный момент поток.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.Thread" />, представляющий собой выполняющийся в данный момент поток.
    /// </returns>
    [__DynamicallyInvokable]
    public static Thread CurrentThread
    {
      [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail), __DynamicallyInvokable] get
      {
        return Thread.GetCurrentThreadNative();
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern Thread GetCurrentThreadNative();

    [SecurityCritical]
    private void SetStartHelper(Delegate start, int maxStackSize)
    {
      ulong defaultStackSize = Thread.GetProcessDefaultStackSize();
      if ((ulong) (uint) maxStackSize > defaultStackSize)
      {
        try
        {
          CodeAccessPermission.Demand(PermissionType.FullTrust);
        }
        catch (SecurityException ex)
        {
          maxStackSize = (int) Math.Min(defaultStackSize, (ulong) int.MaxValue);
        }
      }
      ThreadHelper threadHelper = new ThreadHelper(start);
      if (start is ThreadStart)
        this.SetStart((Delegate) new ThreadStart(threadHelper.ThreadStart), maxStackSize);
      else
        this.SetStart((Delegate) new ParameterizedThreadStart(threadHelper.ThreadStart), maxStackSize);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern ulong GetProcessDefaultStackSize();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void SetStart(Delegate start, int maxStackSize);

    /// <summary>
    ///   Обеспечивает освобождение ресурсов и выполнение других завершающих операций, когда сборщик мусора восстанавливает объект <see cref="T:System.Threading.Thread" />.
    /// </summary>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    ~Thread()
    {
      this.InternalFinalize();
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void InternalFinalize();

    /// <summary>
    ///   Отключает автоматическую очистку вызываемых оболочек времени выполнения (RCW) для текущего потока.
    /// </summary>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern void DisableComObjectEagerCleanup();

    /// <summary>
    ///   Возвращает или задает значение, показывающее, является ли поток фоновым.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если этот поток является или станет фоновым потоком; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Поток не работает.
    /// </exception>
    public bool IsBackground
    {
      [SecuritySafeCritical] get
      {
        return this.IsBackgroundNative();
      }
      [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true)] set
      {
        this.SetBackgroundNative(value);
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern bool IsBackgroundNative();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void SetBackgroundNative(bool isBackground);

    /// <summary>
    ///   Возвращает значение, содержащее состояния текущего потока.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Threading.ThreadState" />, показывающее состояние текущего потока.
    ///    Начальное значение — <see langword="Unstarted" />.
    /// </returns>
    public ThreadState ThreadState
    {
      [SecuritySafeCritical] get
      {
        return (ThreadState) this.GetThreadStateNative();
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern int GetThreadStateNative();

    /// <summary>
    ///   Возвращает или задает модель "apartment" для данного потока.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Threading.ApartmentState" />.
    ///    Начальное значение — <see langword="Unknown" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Предпринята попытка задать в этом свойстве состояние, которое не является допустимым состоянием подразделения (состояние, отличное от однопотокового подразделения (<see langword="STA" />) или многопотокового подразделения (<see langword="MTA" />)).
    /// </exception>
    [Obsolete("The ApartmentState property has been deprecated.  Use GetApartmentState, SetApartmentState or TrySetApartmentState instead.", false)]
    public ApartmentState ApartmentState
    {
      [SecuritySafeCritical] get
      {
        return (ApartmentState) this.GetApartmentStateNative();
      }
      [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true, Synchronization = true)] set
      {
        this.SetApartmentStateNative((int) value, true);
      }
    }

    /// <summary>
    ///   Возвращает значение типа <see cref="T:System.Threading.ApartmentState" />, показывающее состояние апартамента.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Threading.ApartmentState" />, показывающее состояние подразделения управляемого потока.
    ///    Значение по умолчанию — <see cref="F:System.Threading.ApartmentState.Unknown" />.
    /// </returns>
    [SecuritySafeCritical]
    public ApartmentState GetApartmentState()
    {
      return (ApartmentState) this.GetApartmentStateNative();
    }

    /// <summary>
    ///   Задает модель "apartment" для потока до его запуска.
    /// </summary>
    /// <param name="state">Новая модель "apartment".</param>
    /// <returns>
    ///   Значение <see langword="true" />, если задана модель "apartment"; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="state" /> не является допустимым состоянием подразделения.
    /// </exception>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Поток уже запущен.
    /// </exception>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true, Synchronization = true)]
    public bool TrySetApartmentState(ApartmentState state)
    {
      return this.SetApartmentStateHelper(state, false);
    }

    /// <summary>
    ///   Задает модель "apartment" для потока до его запуска.
    /// </summary>
    /// <param name="state">Новая модель "apartment".</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="state" /> не является допустимым состоянием подразделения.
    /// </exception>
    /// <exception cref="T:System.Threading.ThreadStateException">
    ///   Поток уже запущен.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Состояние подразделения уже инициализировано.
    /// </exception>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true, Synchronization = true)]
    public void SetApartmentState(ApartmentState state)
    {
      if (!this.SetApartmentStateHelper(state, true))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ApartmentStateSwitchFailed"));
    }

    [SecurityCritical]
    private bool SetApartmentStateHelper(ApartmentState state, bool fireMDAOnMismatch)
    {
      ApartmentState apartmentState = (ApartmentState) this.SetApartmentStateNative((int) state, fireMDAOnMismatch);
      return state == ApartmentState.Unknown && apartmentState == ApartmentState.MTA || apartmentState == state;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern int GetApartmentStateNative();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern int SetApartmentStateNative(int state, bool fireMDAOnMismatch);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void StartupSetApartmentStateInternal();

    /// <summary>
    ///   Выделяет неименованную область данных всем потокам.
    ///    Для улучшения производительности используйте поля, отмеченные атрибутом <see cref="T:System.ThreadStaticAttribute" />.
    /// </summary>
    /// <returns>Выделенная именованная область данных всем потокам.</returns>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, SharedState = true)]
    public static LocalDataStoreSlot AllocateDataSlot()
    {
      return Thread.LocalDataStoreManager.AllocateDataSlot();
    }

    /// <summary>
    ///   Выделяет именованную область данных всем потокам.
    ///    Для улучшения производительности используйте поля, отмеченные атрибутом <see cref="T:System.ThreadStaticAttribute" />.
    /// </summary>
    /// <param name="name">Имя выделяемой области данных.</param>
    /// <returns>Выделенная именованная область данных всем потокам.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Именованная область данных с указанным именем уже существует.
    /// </exception>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, SharedState = true)]
    public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
    {
      return Thread.LocalDataStoreManager.AllocateNamedDataSlot(name);
    }

    /// <summary>
    ///   Ищет именованную область данных.
    ///    Для улучшения производительности используйте поля, отмеченные атрибутом <see cref="T:System.ThreadStaticAttribute" />.
    /// </summary>
    /// <param name="name">Имя локальной области данных.</param>
    /// <returns>
    ///   Объект <see cref="T:System.LocalDataStoreSlot" />, выделенный для данного потока.
    /// </returns>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, SharedState = true)]
    public static LocalDataStoreSlot GetNamedDataSlot(string name)
    {
      return Thread.LocalDataStoreManager.GetNamedDataSlot(name);
    }

    /// <summary>
    ///   Удаляет связь между названием и областью для всех потоков в процессе.
    ///    Для улучшения производительности используйте поля, отмеченные атрибутом <see cref="T:System.ThreadStaticAttribute" />.
    /// </summary>
    /// <param name="name">Имя освобождаемой области данных.</param>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, SharedState = true)]
    public static void FreeNamedDataSlot(string name)
    {
      Thread.LocalDataStoreManager.FreeNamedDataSlot(name);
    }

    /// <summary>
    ///   Извлекает значение из заданной области текущего потока, внутри текущей области текущего потока.
    ///    Для улучшения производительности используйте поля, отмеченные атрибутом <see cref="T:System.ThreadStaticAttribute" />.
    /// </summary>
    /// <param name="slot">
    ///   Объект <see cref="T:System.LocalDataStoreSlot" />, из которого возвращается значение.
    /// </param>
    /// <returns>Извлекаемое значение.</returns>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, SharedState = true)]
    public static object GetData(LocalDataStoreSlot slot)
    {
      LocalDataStoreHolder localDataStore = Thread.s_LocalDataStore;
      if (localDataStore != null)
        return localDataStore.Store.GetData(slot);
      Thread.LocalDataStoreManager.ValidateSlot(slot);
      return (object) null;
    }

    /// <summary>
    ///   Задает данные в указанной области для текущей области потока, выполняющегося в данный момент.
    ///    Для улучшения производительности используйте поля, отмеченные атрибутом <see cref="T:System.ThreadStaticAttribute" />.
    /// </summary>
    /// <param name="slot">
    ///   Объект <see cref="T:System.LocalDataStoreSlot" />, для которого задается значение.
    /// </param>
    /// <param name="data">Задаваемое значение.</param>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, SharedState = true)]
    public static void SetData(LocalDataStoreSlot slot, object data)
    {
      LocalDataStoreHolder localDataStore = Thread.s_LocalDataStore;
      if (localDataStore == null)
      {
        localDataStore = Thread.LocalDataStoreManager.CreateLocalDataStore();
        Thread.s_LocalDataStore = localDataStore;
      }
      localDataStore.Store.SetData(slot, data);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool nativeGetSafeCulture(Thread t, int appDomainId, bool isUI, ref CultureInfo safeCulture);

    /// <summary>
    ///   Возвращает или задает текущие язык и региональные параметры, используемые диспетчером ресурсов для поиска ресурсов, связанных с языком и региональными параметрами, во время выполнения.
    /// </summary>
    /// <returns>Объект, представляющий текущие языковые стандарты.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задано значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойству присвоено имя языка и региональных параметров, которое не может использоваться для нахождения файла ресурсов.
    ///    Имена файлов ресурсов могут содержать только буквы, цифры, дефисы или символы подчеркивания.
    /// </exception>
    [__DynamicallyInvokable]
    public CultureInfo CurrentUICulture
    {
      [__DynamicallyInvokable] get
      {
        if (AppDomain.IsAppXModel())
          return CultureInfo.GetCultureInfoForUserPreferredLanguageInAppX() ?? this.GetCurrentUICultureNoAppX();
        return this.GetCurrentUICultureNoAppX();
      }
      [SecuritySafeCritical, __DynamicallyInvokable, HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        CultureInfo.VerifyCultureName(value, true);
        if (!Thread.nativeSetThreadUILocale(value.SortName))
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidResourceCultureName", (object) value.Name));
        value.StartCrossDomainTracking();
        if (!AppContextSwitches.NoAsyncCurrentCulture)
        {
          if (Thread.s_asyncLocalCurrentUICulture == null)
            Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentUICulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentUICulture)), (AsyncLocal<CultureInfo>) null);
          Thread.s_asyncLocalCurrentUICulture.Value = value;
        }
        else
          this.m_CurrentUICulture = value;
      }
    }

    [SecuritySafeCritical]
    internal CultureInfo GetCurrentUICultureNoAppX()
    {
      if (this.m_CurrentUICulture == null)
        return CultureInfo.DefaultThreadCurrentUICulture ?? CultureInfo.UserDefaultUICulture;
      CultureInfo safeCulture = (CultureInfo) null;
      if (!Thread.nativeGetSafeCulture(this, Thread.GetDomainID(), true, ref safeCulture) || safeCulture == null)
        return CultureInfo.UserDefaultUICulture;
      return safeCulture;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool nativeSetThreadUILocale(string locale);

    /// <summary>
    ///   Возвращает или задает язык и региональные параметры для текущего потока.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий язык и региональные параметры, используемые текущим потоком.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Для свойства задано значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public CultureInfo CurrentCulture
    {
      [__DynamicallyInvokable] get
      {
        if (AppDomain.IsAppXModel())
          return CultureInfo.GetCultureInfoForUserPreferredLanguageInAppX() ?? this.GetCurrentCultureNoAppX();
        return this.GetCurrentCultureNoAppX();
      }
      [SecuritySafeCritical, __DynamicallyInvokable, SecurityPermission(SecurityAction.Demand, ControlThread = true)] set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        CultureInfo.nativeSetThreadLocale(value.SortName);
        value.StartCrossDomainTracking();
        if (!AppContextSwitches.NoAsyncCurrentCulture)
        {
          if (Thread.s_asyncLocalCurrentCulture == null)
            Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentCulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentCulture)), (AsyncLocal<CultureInfo>) null);
          Thread.s_asyncLocalCurrentCulture.Value = value;
        }
        else
          this.m_CurrentCulture = value;
      }
    }

    [SecuritySafeCritical]
    private CultureInfo GetCurrentCultureNoAppX()
    {
      if (this.m_CurrentCulture == null)
        return CultureInfo.DefaultThreadCurrentCulture ?? CultureInfo.UserDefaultCulture;
      CultureInfo safeCulture = (CultureInfo) null;
      if (!Thread.nativeGetSafeCulture(this, Thread.GetDomainID(), false, ref safeCulture) || safeCulture == null)
        return CultureInfo.UserDefaultCulture;
      return safeCulture;
    }

    /// <summary>
    ///   Возвращает текущий контекст, в котором выполняется поток.
    /// </summary>
    /// <returns>
    ///   Класс <see cref="T:System.Runtime.Remoting.Contexts.Context" />, представляющий текущий контекст потока.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public static Context CurrentContext
    {
      [SecurityCritical] get
      {
        return Thread.CurrentThread.GetCurrentContextInternal();
      }
    }

    [SecurityCritical]
    internal Context GetCurrentContextInternal()
    {
      if (this.m_Context == null)
        this.m_Context = Context.DefaultContext;
      return this.m_Context;
    }

    /// <summary>
    ///   Возвращает или задает текущего участника потока (для безопасности на основе ролей).
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.Security.Principal.IPrincipal" />, представляющее контекст безопасности.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект не имеет разрешений, необходимых для задания участника.
    /// </exception>
    public static IPrincipal CurrentPrincipal
    {
      [SecuritySafeCritical] get
      {
        lock (Thread.CurrentThread)
        {
          IPrincipal principal = CallContext.Principal;
          if (principal == null)
          {
            principal = Thread.GetDomain().GetThreadPrincipal();
            CallContext.Principal = principal;
          }
          return principal;
        }
      }
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)] set
      {
        CallContext.Principal = value;
      }
    }

    [SecurityCritical]
    private void SetPrincipalInternal(IPrincipal principal)
    {
      this.GetMutableExecutionContext().LogicalCallContext.SecurityData.Principal = principal;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Context GetContextInternal(IntPtr id);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern object InternalCrossContextCallback(Context ctx, IntPtr ctxID, int appDomainID, InternalCrossContextDelegate ftnToCall, object[] args);

    [SecurityCritical]
    internal object InternalCrossContextCallback(Context ctx, InternalCrossContextDelegate ftnToCall, object[] args)
    {
      return this.InternalCrossContextCallback(ctx, ctx.InternalContextID, 0, ftnToCall, args);
    }

    private static object CompleteCrossContextCallback(InternalCrossContextDelegate ftnToCall, object[] args)
    {
      return ftnToCall(args);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern AppDomain GetDomainInternal();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern AppDomain GetFastDomainInternal();

    /// <summary>
    ///   Возвращает текущую область, в которой выполняется текущий поток.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.AppDomain" />, представляющий собой текущий домен приложения выполняющегося потока.
    /// </returns>
    [SecuritySafeCritical]
    public static AppDomain GetDomain()
    {
      return Thread.GetFastDomainInternal() ?? Thread.GetDomainInternal();
    }

    /// <summary>
    ///   Возвращает уникальный идентификатор домена приложения.
    /// </summary>
    /// <returns>
    ///   32-разрядное знаковое целое число, однозначно определяющее домен приложения.
    /// </returns>
    public static int GetDomainID()
    {
      return Thread.GetDomain().GetId();
    }

    /// <summary>Получает или задает имя потока.</summary>
    /// <returns>
    ///   Строка, содержащая имя потока или <see langword="null" />, если имя не задано.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Запрошена операция задания, но свойство <see langword="Name" /> уже задано.
    /// </exception>
    public string Name
    {
      get
      {
        return this.m_Name;
      }
      [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)] set
      {
        lock (this)
        {
          if (this.m_Name != null)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WriteOnce"));
          this.m_Name = value;
          Thread.InformThreadNameChange(this.GetNativeHandle(), value, value != null ? value.Length : 0);
        }
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void InformThreadNameChange(ThreadHandle t, string name, int len);

    internal object AbortReason
    {
      [SecurityCritical] get
      {
        try
        {
          return this.GetAbortReason();
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ExceptionStateCrossAppDomain"), ex);
        }
      }
      [SecurityCritical] set
      {
        this.SetAbortReason(value);
      }
    }

    /// <summary>
    ///   Уведомляет узел, что выполнение близится ко входу в область кода, в которой эффекты прерывания выполнения или неуправляемого выполнения могут повлиять на другие задачи в домене приложения.
    /// </summary>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public static extern void BeginCriticalRegion();

    /// <summary>
    ///   Уведомляет хост, что выполнение близится ко входу в область кода, в которой эффекты прерывания выполнения или неуправляемой ошибки ограничены текущей задачей.
    /// </summary>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public static extern void EndCriticalRegion();

    /// <summary>
    ///   Уведомляет узел, что управляемый код близок к выполнению инструкций, зависящих от идентификации текущего потока операционной системы.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void BeginThreadAffinity();

    /// <summary>
    ///   Уведомляет узел об окончании выполнения кодом инструкций, которые зависят от идентификатора текущего потока в операционной системе.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void EndThreadAffinity();

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static byte VolatileRead(ref byte address)
    {
      byte num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static short VolatileRead(ref short address)
    {
      short num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int VolatileRead(ref int address)
    {
      int num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static long VolatileRead(ref long address)
    {
      long num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static sbyte VolatileRead(ref sbyte address)
    {
      sbyte num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ushort VolatileRead(ref ushort address)
    {
      ushort num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static uint VolatileRead(ref uint address)
    {
      uint num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static IntPtr VolatileRead(ref IntPtr address)
    {
      IntPtr num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static UIntPtr VolatileRead(ref UIntPtr address)
    {
      UIntPtr num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ulong VolatileRead(ref ulong address)
    {
      ulong num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static float VolatileRead(ref float address)
    {
      float num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static double VolatileRead(ref double address)
    {
      double num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение поля.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров и от состояния кэш-буфера процессоров.
    /// </summary>
    /// <param name="address">Поле для чтения.</param>
    /// <returns>
    ///   Последнее значение, записанное в поле любым процессором.
    /// </returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static object VolatileRead(ref object address)
    {
      object obj = address;
      Thread.MemoryBarrier();
      return obj;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref byte address, byte value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref short address, short value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref int address, int value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref long address, long value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref sbyte address, sbyte value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref ushort address, ushort value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref uint address, uint value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref IntPtr address, IntPtr value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref UIntPtr address, UIntPtr value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref ulong address, ulong value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref float address, float value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref double address, double value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Записывает значение непосредственно в поле, так что оно становится видимым для всех процессоров компьютера.
    /// </summary>
    /// <param name="address">
    ///   Поле, в которое требуется записать значение.
    /// </param>
    /// <param name="value">Записываемое значение.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref object address, object value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>
    ///   Синхронизирует доступ к памяти следующим образом: процессор, выполняющий текущий поток, не способен упорядочить инструкции так, чтобы обращения к памяти до вызова метода <see cref="M:System.Threading.Thread.MemoryBarrier" /> выполнялись после обращений к памяти, следующих за вызовом метода <see cref="M:System.Threading.Thread.MemoryBarrier" />.
    /// </summary>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void MemoryBarrier();

    private static LocalDataStoreMgr LocalDataStoreManager
    {
      get
      {
        if (Thread.s_LocalDataStoreMgr == null)
          Interlocked.CompareExchange<LocalDataStoreMgr>(ref Thread.s_LocalDataStoreMgr, new LocalDataStoreMgr(), (LocalDataStoreMgr) null);
        return Thread.s_LocalDataStoreMgr;
      }
    }

    void _Thread.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _Thread.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _Thread.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _Thread.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern void SetAbortReason(object o);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern object GetAbortReason();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern void ClearAbortReason();
  }
}
