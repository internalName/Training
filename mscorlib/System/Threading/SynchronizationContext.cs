// Decompiled with JetBrains decompiler
// Type: System.Threading.SynchronizationContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Обеспечивает базовую функциональность для распространения контекста синхронизации в различных моделях синхронизации.
  /// </summary>
  [__DynamicallyInvokable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)]
  public class SynchronizationContext
  {
    private SynchronizationContextProperties _props;
    private static Type s_cachedPreparedType1;
    private static Type s_cachedPreparedType2;
    private static Type s_cachedPreparedType3;
    private static Type s_cachedPreparedType4;
    private static Type s_cachedPreparedType5;
    [SecurityCritical]
    private static WinRTSynchronizationContextFactoryBase s_winRTContextFactory;

    /// <summary>
    ///   Создает новый экземпляр класса <see cref="T:System.Threading.SynchronizationContext" />.
    /// </summary>
    [__DynamicallyInvokable]
    public SynchronizationContext()
    {
    }

    /// <summary>
    ///   Задает уведомление о требовании уведомления об ожидании и подготавливает метод обратного вызова. Таким образом, метод может быть вызван более надежным способом, когда происходит ожидание.
    /// </summary>
    [SecuritySafeCritical]
    protected void SetWaitNotificationRequired()
    {
      Type type = this.GetType();
      if (SynchronizationContext.s_cachedPreparedType1 != type && SynchronizationContext.s_cachedPreparedType2 != type && (SynchronizationContext.s_cachedPreparedType3 != type && SynchronizationContext.s_cachedPreparedType4 != type) && SynchronizationContext.s_cachedPreparedType5 != type)
      {
        RuntimeHelpers.PrepareDelegate((Delegate) new SynchronizationContext.WaitDelegate(this.Wait));
        if (SynchronizationContext.s_cachedPreparedType1 == (Type) null)
          SynchronizationContext.s_cachedPreparedType1 = type;
        else if (SynchronizationContext.s_cachedPreparedType2 == (Type) null)
          SynchronizationContext.s_cachedPreparedType2 = type;
        else if (SynchronizationContext.s_cachedPreparedType3 == (Type) null)
          SynchronizationContext.s_cachedPreparedType3 = type;
        else if (SynchronizationContext.s_cachedPreparedType4 == (Type) null)
          SynchronizationContext.s_cachedPreparedType4 = type;
        else if (SynchronizationContext.s_cachedPreparedType5 == (Type) null)
          SynchronizationContext.s_cachedPreparedType5 = type;
      }
      this._props |= SynchronizationContextProperties.RequireWaitNotification;
    }

    /// <summary>Определяет, нужно ли уведомление об ожидании.</summary>
    /// <returns>
    ///   Значение <see langword="true" />, если уведомление об ожидании требуется; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsWaitNotificationRequired()
    {
      return (uint) (this._props & SynchronizationContextProperties.RequireWaitNotification) > 0U;
    }

    /// <summary>
    ///   При переопределении в производном классе отправляет синхронное сообщение в контекст синхронизации.
    /// </summary>
    /// <param name="d">
    ///   Вызываемый делегат <see cref="T:System.Threading.SendOrPostCallback" />.
    /// </param>
    /// <param name="state">Передаваемый делегату объект.</param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод был вызван в магазине Windows.
    ///    Реализация <see cref="T:System.Threading.SynchronizationContext" /> для магазина Windows не поддерживает приложения <see cref="M:System.Threading.SynchronizationContext.Send(System.Threading.SendOrPostCallback,System.Object)" /> метод.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void Send(SendOrPostCallback d, object state)
    {
      d(state);
    }

    /// <summary>
    ///   При переопределении в производном классе отправляет асинхронное сообщение в контекст синхронизации.
    /// </summary>
    /// <param name="d">
    ///   Вызываемый делегат <see cref="T:System.Threading.SendOrPostCallback" />.
    /// </param>
    /// <param name="state">Передаваемый делегату объект.</param>
    [__DynamicallyInvokable]
    public virtual void Post(SendOrPostCallback d, object state)
    {
      ThreadPool.QueueUserWorkItem(new WaitCallback(d.Invoke), state);
    }

    /// <summary>
    ///   При переопределении в производном классе отвечает на уведомление о запуске операции.
    /// </summary>
    [__DynamicallyInvokable]
    public virtual void OperationStarted()
    {
    }

    /// <summary>
    ///   При переопределении в производном классе отвечает на уведомление о завершении операции.
    /// </summary>
    [__DynamicallyInvokable]
    public virtual void OperationCompleted()
    {
    }

    /// <summary>
    ///   Ожидает получения сигнала всеми элементами заданного массива или любым из его элементов.
    /// </summary>
    /// <param name="waitHandles">
    ///   Массив типа <see cref="T:System.IntPtr" />, содержащий собственные дескрипторы операционной системы.
    /// </param>
    /// <param name="waitAll">
    ///   Значение <see langword="true" /> для ожидания всех дескрипторов; <see langword="false" /> для ожидания любого дескриптора.
    /// </param>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   Индекс объекта, удовлетворившего операцию ожидания, в массиве.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="waitHandles" /> имеет значение NULL.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [PrePrepareMethod]
    public virtual int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
    {
      if (waitHandles == null)
        throw new ArgumentNullException(nameof (waitHandles));
      return SynchronizationContext.WaitHelper(waitHandles, waitAll, millisecondsTimeout);
    }

    /// <summary>
    ///   Вспомогательная функция, ожидающая сигнала от всех или некоторых элементов заданного массива.
    /// </summary>
    /// <param name="waitHandles">
    ///   Массив типа <see cref="T:System.IntPtr" />, содержащий собственные дескрипторы операционной системы.
    /// </param>
    /// <param name="waitAll">
    ///   Значение <see langword="true" /> для ожидания всех дескрипторов; <see langword="false" /> для ожидания любого дескриптора.
    /// </param>
    /// <param name="millisecondsTimeout">
    ///   Время ожидания в миллисекундах или <see cref="F:System.Threading.Timeout.Infinite" /> (-1) для неограниченного времени ожидания.
    /// </param>
    /// <returns>
    ///   Индекс объекта, удовлетворившего операцию ожидания, в массиве.
    /// </returns>
    [SecurityCritical]
    [CLSCompliant(false)]
    [PrePrepareMethod]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    protected static extern int WaitHelper(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout);

    /// <summary>Задает текущий контекст синхронизации.</summary>
    /// <param name="syncContext">
    ///   Задаваемый объект <see cref="T:System.Threading.SynchronizationContext" />.
    /// </param>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void SetSynchronizationContext(SynchronizationContext syncContext)
    {
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      executionContext.SynchronizationContext = syncContext;
      executionContext.SynchronizationContextNoFlow = syncContext;
    }

    /// <summary>Получает контекст синхронизации для текущего потока</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.SynchronizationContext" />, представляющий текущий контекст синхронизации.
    /// </returns>
    [__DynamicallyInvokable]
    public static SynchronizationContext Current
    {
      [__DynamicallyInvokable] get
      {
        return Thread.CurrentThread.GetExecutionContextReader().SynchronizationContext ?? SynchronizationContext.GetThreadLocalContext();
      }
    }

    internal static SynchronizationContext CurrentNoFlow
    {
      [FriendAccessAllowed] get
      {
        return Thread.CurrentThread.GetExecutionContextReader().SynchronizationContextNoFlow ?? SynchronizationContext.GetThreadLocalContext();
      }
    }

    private static SynchronizationContext GetThreadLocalContext()
    {
      SynchronizationContext synchronizationContext = (SynchronizationContext) null;
      if (synchronizationContext == null && Environment.IsWinRTSupported)
        synchronizationContext = SynchronizationContext.GetWinRTContext();
      return synchronizationContext;
    }

    [SecuritySafeCritical]
    private static SynchronizationContext GetWinRTContext()
    {
      if (!AppDomain.IsAppXModel())
        return (SynchronizationContext) null;
      object forCurrentThread = SynchronizationContext.GetWinRTDispatcherForCurrentThread();
      if (forCurrentThread != null)
        return SynchronizationContext.GetWinRTSynchronizationContextFactory().Create(forCurrentThread);
      return (SynchronizationContext) null;
    }

    [SecurityCritical]
    private static WinRTSynchronizationContextFactoryBase GetWinRTSynchronizationContextFactory()
    {
      WinRTSynchronizationContextFactoryBase contextFactoryBase = SynchronizationContext.s_winRTContextFactory;
      if (contextFactoryBase == null)
        SynchronizationContext.s_winRTContextFactory = contextFactoryBase = (WinRTSynchronizationContextFactoryBase) Activator.CreateInstance(Type.GetType("System.Threading.WinRTSynchronizationContextFactory, System.Runtime.WindowsRuntime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", true), true);
      return contextFactoryBase;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Interface)]
    private static extern object GetWinRTDispatcherForCurrentThread();

    /// <summary>
    ///   При переопределении в производном классе создает копию контекста синхронизации.
    /// </summary>
    /// <returns>
    ///   Новый объект <see cref="T:System.Threading.SynchronizationContext" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual SynchronizationContext CreateCopy()
    {
      return new SynchronizationContext();
    }

    [SecurityCritical]
    private static int InvokeWaitMethodHelper(SynchronizationContext syncContext, IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
    {
      return syncContext.Wait(waitHandles, waitAll, millisecondsTimeout);
    }

    private delegate int WaitDelegate(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout);
  }
}
