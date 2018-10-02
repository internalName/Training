// Decompiled with JetBrains decompiler
// Type: System.Threading.HostExecutionContextManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Предоставляет функцию, которая предоставляет общие хост-среды выполнения языка для участия в поток или миграции контекста выполнения.
  /// </summary>
  public class HostExecutionContextManager
  {
    private static volatile bool _fIsHostedChecked;
    private static volatile bool _fIsHosted;
    private static HostExecutionContextManager _hostExecutionContextManager;

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool HostSecurityManagerPresent();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int ReleaseHostSecurityContext(IntPtr context);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int CloneHostSecurityContext(SafeHandle context, SafeHandle clonedContext);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int CaptureHostSecurityContext(SafeHandle capturedContext);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int SetHostSecurityContext(SafeHandle context, bool fReturnPrevious, SafeHandle prevContext);

    [SecurityCritical]
    internal static bool CheckIfHosted()
    {
      if (!HostExecutionContextManager._fIsHostedChecked)
      {
        HostExecutionContextManager._fIsHosted = HostExecutionContextManager.HostSecurityManagerPresent();
        HostExecutionContextManager._fIsHostedChecked = true;
      }
      return HostExecutionContextManager._fIsHosted;
    }

    /// <summary>
    ///   Перехватывает контекст выполнения хоста из текущего потока.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.HostExecutionContext" /> объект, представляющий контекст выполнения хоста из текущего потока.
    /// </returns>
    [SecuritySafeCritical]
    public virtual HostExecutionContext Capture()
    {
      HostExecutionContext executionContext = (HostExecutionContext) null;
      if (HostExecutionContextManager.CheckIfHosted())
      {
        IUnknownSafeHandle iunknownSafeHandle = new IUnknownSafeHandle();
        executionContext = new HostExecutionContext((object) iunknownSafeHandle);
        HostExecutionContextManager.CaptureHostSecurityContext((SafeHandle) iunknownSafeHandle);
      }
      return executionContext;
    }

    /// <summary>
    ///   Задает контекст выполнения текущего узла контекста выполнения заданного узла.
    /// </summary>
    /// <param name="hostExecutionContext">
    ///   Задаваемый объект <see cref="T:System.Threading.HostExecutionContext" />.
    /// </param>
    /// <returns>
    ///   Объект для восстановления <see cref="T:System.Threading.HostExecutionContext" /> в предыдущее состояние.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="hostExecutionContext" /> не был получен через операции записи.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="hostExecutionContext" /> был аргумент предыдущей <see cref="M:System.Threading.HostExecutionContextManager.SetHostExecutionContext(System.Threading.HostExecutionContext)" />  вызова метода.
    /// </exception>
    [SecurityCritical]
    public virtual object SetHostExecutionContext(HostExecutionContext hostExecutionContext)
    {
      if (hostExecutionContext == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
      HostExecutionContextSwitcher executionContextSwitcher = new HostExecutionContextSwitcher();
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      executionContextSwitcher.executionContext = executionContext;
      executionContextSwitcher.currentHostContext = hostExecutionContext;
      executionContextSwitcher.previousHostContext = (HostExecutionContext) null;
      if (HostExecutionContextManager.CheckIfHosted() && hostExecutionContext.State is IUnknownSafeHandle)
      {
        IUnknownSafeHandle iunknownSafeHandle = new IUnknownSafeHandle();
        executionContextSwitcher.previousHostContext = new HostExecutionContext((object) iunknownSafeHandle);
        HostExecutionContextManager.SetHostSecurityContext((SafeHandle) hostExecutionContext.State, true, (SafeHandle) iunknownSafeHandle);
      }
      executionContext.HostExecutionContext = hostExecutionContext;
      return (object) executionContextSwitcher;
    }

    /// <summary>
    ///   Восстанавливает контекст выполнения хоста в предыдущее состояние.
    /// </summary>
    /// <param name="previousState">
    ///   Восстановить предыдущее состояние контекста.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство <paramref name="previousState" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="previousState" /> не был создан в текущем потоке.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="previousState" /> Последнее состояние не является <see cref="T:System.Threading.HostExecutionContext" />.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public virtual void Revert(object previousState)
    {
      HostExecutionContextSwitcher executionContextSwitcher = previousState as HostExecutionContextSwitcher;
      if (executionContextSwitcher == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotOverrideSetWithoutRevert"));
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      if (executionContext != executionContextSwitcher.executionContext)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseSwitcherOtherThread"));
      executionContextSwitcher.executionContext = (ExecutionContext) null;
      if (executionContext.HostExecutionContext != executionContextSwitcher.currentHostContext)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseSwitcherOtherThread"));
      HostExecutionContext previousHostContext = executionContextSwitcher.previousHostContext;
      if (HostExecutionContextManager.CheckIfHosted() && previousHostContext != null && previousHostContext.State is IUnknownSafeHandle)
        HostExecutionContextManager.SetHostSecurityContext((SafeHandle) previousHostContext.State, false, (SafeHandle) null);
      executionContext.HostExecutionContext = previousHostContext;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static HostExecutionContext CaptureHostExecutionContext()
    {
      HostExecutionContext executionContext = (HostExecutionContext) null;
      HostExecutionContextManager executionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
      if (executionContextManager != null)
        executionContext = executionContextManager.Capture();
      return executionContext;
    }

    [SecurityCritical]
    internal static object SetHostExecutionContextInternal(HostExecutionContext hostContext)
    {
      HostExecutionContextManager executionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
      object obj = (object) null;
      if (executionContextManager != null)
        obj = executionContextManager.SetHostExecutionContext(hostContext);
      return obj;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static HostExecutionContextManager GetCurrentHostExecutionContextManager()
    {
      return AppDomainManager.CurrentAppDomainManager?.HostExecutionContextManager;
    }

    internal static HostExecutionContextManager GetInternalHostExecutionContextManager()
    {
      if (HostExecutionContextManager._hostExecutionContextManager == null)
        HostExecutionContextManager._hostExecutionContextManager = new HostExecutionContextManager();
      return HostExecutionContextManager._hostExecutionContextManager;
    }
  }
}
