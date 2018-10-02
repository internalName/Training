// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security
{
  /// <summary>
  ///   Инкапсулирует и распространяет все связанные с безопасностью данные контекстов выполнения для различных потоков.
  ///    Этот класс не наследуется.
  /// </summary>
  public sealed class SecurityContext : IDisposable
  {
    private static bool _LegacyImpersonationPolicy = SecurityContext.GetImpersonationFlowMode() == WindowsImpersonationFlowMode.IMP_NOFLOW;
    private static bool _alwaysFlowImpersonationPolicy = SecurityContext.GetImpersonationFlowMode() == WindowsImpersonationFlowMode.IMP_ALWAYSFLOW;
    private ExecutionContext _executionContext;
    private volatile WindowsIdentity _windowsIdentity;
    private volatile CompressedStack _compressedStack;
    private static volatile SecurityContext _fullTrustSC;
    internal volatile bool isNewCapture;
    internal volatile SecurityContextDisableFlow _disableFlow;
    internal static volatile RuntimeHelpers.TryCode tryCode;
    internal static volatile RuntimeHelpers.CleanupCode cleanupCode;

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal SecurityContext()
    {
    }

    internal static SecurityContext FullTrustSecurityContext
    {
      [SecurityCritical] get
      {
        if (SecurityContext._fullTrustSC == null)
          SecurityContext._fullTrustSC = SecurityContext.CreateFullTrustSecurityContext();
        return SecurityContext._fullTrustSC;
      }
    }

    internal ExecutionContext ExecutionContext
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] set
      {
        this._executionContext = value;
      }
    }

    internal WindowsIdentity WindowsIdentity
    {
      get
      {
        return this._windowsIdentity;
      }
      set
      {
        this._windowsIdentity = value;
      }
    }

    internal CompressedStack CompressedStack
    {
      get
      {
        return this._compressedStack;
      }
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] set
      {
        this._compressedStack = value;
      }
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Security.SecurityContext" />.
    /// </summary>
    public void Dispose()
    {
      if (this._windowsIdentity == null)
        return;
      this._windowsIdentity.Dispose();
    }

    /// <summary>
    ///   Подавляет действие контекста безопасности между асинхронными потоками.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Threading.AsyncFlowControl" /> Структура для восстановления потока.
    /// </returns>
    [SecurityCritical]
    public static AsyncFlowControl SuppressFlow()
    {
      return SecurityContext.SuppressFlow(SecurityContextDisableFlow.All);
    }

    /// <summary>
    ///   Подавляет действие удостоверения Windows текущего контекста безопасности на асинхронных потоках.
    /// </summary>
    /// <returns>Структура для восстановления потока.</returns>
    [SecurityCritical]
    public static AsyncFlowControl SuppressFlowWindowsIdentity()
    {
      return SecurityContext.SuppressFlow(SecurityContextDisableFlow.WI);
    }

    [SecurityCritical]
    internal static AsyncFlowControl SuppressFlow(SecurityContextDisableFlow flags)
    {
      if (SecurityContext.IsFlowSuppressed(flags))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotSupressFlowMultipleTimes"));
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      if (executionContext.SecurityContext == null)
        executionContext.SecurityContext = new SecurityContext();
      AsyncFlowControl asyncFlowControl = new AsyncFlowControl();
      asyncFlowControl.Setup(flags);
      return asyncFlowControl;
    }

    /// <summary>
    ///   Восстанавливает поток контекста безопасности по асинхронным потокам.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Контекст безопасности — <see langword="null" /> или пустая строка.
    /// </exception>
    [SecuritySafeCritical]
    public static void RestoreFlow()
    {
      SecurityContext securityContext = Thread.CurrentThread.GetMutableExecutionContext().SecurityContext;
      if (securityContext == null || securityContext._disableFlow == SecurityContextDisableFlow.Nothing)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRestoreUnsupressedFlow"));
      securityContext._disableFlow = SecurityContextDisableFlow.Nothing;
    }

    /// <summary>
    ///   Определяет, было ли подавлено действие контекста безопасности.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если было подавлено; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool IsFlowSuppressed()
    {
      return SecurityContext.IsFlowSuppressed(SecurityContextDisableFlow.All);
    }

    /// <summary>
    ///   Определяет, было ли подавлено действие удостоверения Windows текущего контекста безопасности.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если было подавлено; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool IsWindowsIdentityFlowSuppressed()
    {
      if (!SecurityContext._LegacyImpersonationPolicy)
        return SecurityContext.IsFlowSuppressed(SecurityContextDisableFlow.WI);
      return true;
    }

    [SecuritySafeCritical]
    internal static bool IsFlowSuppressed(SecurityContextDisableFlow flags)
    {
      return Thread.CurrentThread.GetExecutionContextReader().SecurityContext.IsFlowSuppressed(flags);
    }

    /// <summary>
    ///   Выполняет указанный метод в указанном контексте безопасности в текущем потоке.
    /// </summary>
    /// <param name="securityContext">
    ///   Задаваемый контекст безопасности.
    /// </param>
    /// <param name="callback">
    ///   Делегат, представляющий метод, выполняемый в указанном контексте безопасности.
    /// </param>
    /// <param name="state">
    ///   Данный объект передается в метод обратного вызова.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Свойство <paramref name="securityContext" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="securityContext" /> не был получен через операции записи.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="securityContext" /> уже используется в качестве аргумента для <see cref="M:System.Security.SecurityContext.Run(System.Security.SecurityContext,System.Threading.ContextCallback,System.Object)" /> вызова метода.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Run(SecurityContext securityContext, ContextCallback callback, object state)
    {
      if (securityContext == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullContext"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMe;
      if (!securityContext.isNewCapture)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
      securityContext.isNewCapture = false;
      if (SecurityContext.CurrentlyInDefaultFTSecurityContext(Thread.CurrentThread.GetExecutionContextReader()) && securityContext.IsDefaultFTSecurityContext())
      {
        callback(state);
        if (SecurityContext.GetCurrentWI(Thread.CurrentThread.GetExecutionContextReader()) == null)
          return;
        WindowsIdentity.SafeRevertToSelf(ref stackMark);
      }
      else
        SecurityContext.RunInternal(securityContext, callback, state);
    }

    [SecurityCritical]
    internal static void RunInternal(SecurityContext securityContext, ContextCallback callBack, object state)
    {
      if (SecurityContext.cleanupCode == null)
      {
        SecurityContext.tryCode = new RuntimeHelpers.TryCode(SecurityContext.runTryCode);
        SecurityContext.cleanupCode = new RuntimeHelpers.CleanupCode(SecurityContext.runFinallyCode);
      }
      SecurityContext.SecurityContextRunData securityContextRunData = new SecurityContext.SecurityContextRunData(securityContext, callBack, state);
      RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(SecurityContext.tryCode, SecurityContext.cleanupCode, (object) securityContextRunData);
    }

    [SecurityCritical]
    internal static void runTryCode(object userData)
    {
      SecurityContext.SecurityContextRunData securityContextRunData = (SecurityContext.SecurityContextRunData) userData;
      securityContextRunData.scsw = SecurityContext.SetSecurityContext(securityContextRunData.sc, Thread.CurrentThread.GetExecutionContextReader().SecurityContext, true);
      securityContextRunData.callBack(securityContextRunData.state);
    }

    [SecurityCritical]
    [PrePrepareMethod]
    internal static void runFinallyCode(object userData, bool exceptionThrown)
    {
      ((SecurityContext.SecurityContextRunData) userData).scsw.Undo();
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static SecurityContextSwitcher SetSecurityContext(SecurityContext sc, SecurityContext.Reader prevSecurityContext, bool modifyCurrentExecutionContext)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return SecurityContext.SetSecurityContext(sc, prevSecurityContext, modifyCurrentExecutionContext, ref stackMark);
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    internal static SecurityContextSwitcher SetSecurityContext(SecurityContext sc, SecurityContext.Reader prevSecurityContext, bool modifyCurrentExecutionContext, ref StackCrawlMark stackMark)
    {
      SecurityContextDisableFlow disableFlow = sc._disableFlow;
      sc._disableFlow = SecurityContextDisableFlow.Nothing;
      SecurityContextSwitcher securityContextSwitcher = new SecurityContextSwitcher();
      securityContextSwitcher.currSC = sc;
      securityContextSwitcher.prevSC = prevSecurityContext;
      if (modifyCurrentExecutionContext)
      {
        ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
        securityContextSwitcher.currEC = executionContext;
        executionContext.SecurityContext = sc;
      }
      if (sc != null)
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          securityContextSwitcher.wic = (WindowsImpersonationContext) null;
          if (!SecurityContext._LegacyImpersonationPolicy)
          {
            if (sc.WindowsIdentity != null)
              securityContextSwitcher.wic = sc.WindowsIdentity.Impersonate(ref stackMark);
            else if ((disableFlow & SecurityContextDisableFlow.WI) == SecurityContextDisableFlow.Nothing && prevSecurityContext.WindowsIdentity != null)
              securityContextSwitcher.wic = WindowsIdentity.SafeRevertToSelf(ref stackMark);
          }
          securityContextSwitcher.cssw = CompressedStack.SetCompressedStack(sc.CompressedStack, prevSecurityContext.CompressedStack);
        }
        catch
        {
          securityContextSwitcher.UndoNoThrow();
          throw;
        }
      }
      return securityContextSwitcher;
    }

    /// <summary>Создает копию текущего контекста безопасности.</summary>
    /// <returns>Контекст безопасности текущего потока.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий контекст безопасности ранее использовалось был маршалинга между доменами приложений и не был получен <see cref="M:System.Security.SecurityContext.Capture" /> метод.
    /// </exception>
    [SecuritySafeCritical]
    public SecurityContext CreateCopy()
    {
      if (!this.isNewCapture)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
      SecurityContext securityContext = new SecurityContext();
      securityContext.isNewCapture = true;
      securityContext._disableFlow = this._disableFlow;
      if (this.WindowsIdentity != null)
        securityContext._windowsIdentity = new WindowsIdentity(this.WindowsIdentity.AccessToken);
      if (this._compressedStack != null)
        securityContext._compressedStack = this._compressedStack.CreateCopy();
      return securityContext;
    }

    [SecuritySafeCritical]
    internal SecurityContext CreateMutableCopy()
    {
      SecurityContext securityContext = new SecurityContext();
      securityContext._disableFlow = this._disableFlow;
      if (this.WindowsIdentity != null)
        securityContext._windowsIdentity = new WindowsIdentity(this.WindowsIdentity.AccessToken);
      if (this._compressedStack != null)
        securityContext._compressedStack = this._compressedStack.CreateCopy();
      return securityContext;
    }

    /// <summary>
    ///   Перехватывает контекст безопасности текущего потока.
    /// </summary>
    /// <returns>Контекст безопасности текущего потока.</returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static SecurityContext Capture()
    {
      if (SecurityContext.IsFlowSuppressed())
        return (SecurityContext) null;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return SecurityContext.Capture(Thread.CurrentThread.GetExecutionContextReader(), ref stackMark) ?? SecurityContext.CreateFullTrustSecurityContext();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SecurityContext Capture(ExecutionContext.Reader currThreadEC, ref StackCrawlMark stackMark)
    {
      if (currThreadEC.SecurityContext.IsFlowSuppressed(SecurityContextDisableFlow.All))
        return (SecurityContext) null;
      if (SecurityContext.CurrentlyInDefaultFTSecurityContext(currThreadEC))
        return (SecurityContext) null;
      return SecurityContext.CaptureCore(currThreadEC, ref stackMark);
    }

    [SecurityCritical]
    private static SecurityContext CaptureCore(ExecutionContext.Reader currThreadEC, ref StackCrawlMark stackMark)
    {
      SecurityContext securityContext = new SecurityContext();
      securityContext.isNewCapture = true;
      if (!SecurityContext.IsWindowsIdentityFlowSuppressed())
      {
        WindowsIdentity currentWi = SecurityContext.GetCurrentWI(currThreadEC);
        if (currentWi != null)
          securityContext._windowsIdentity = new WindowsIdentity(currentWi.AccessToken);
      }
      else
        securityContext._disableFlow = SecurityContextDisableFlow.WI;
      securityContext.CompressedStack = CompressedStack.GetCompressedStack(ref stackMark);
      return securityContext;
    }

    [SecurityCritical]
    internal static SecurityContext CreateFullTrustSecurityContext()
    {
      SecurityContext securityContext = new SecurityContext();
      securityContext.isNewCapture = true;
      if (SecurityContext.IsWindowsIdentityFlowSuppressed())
        securityContext._disableFlow = SecurityContextDisableFlow.WI;
      securityContext.CompressedStack = new CompressedStack((SafeCompressedStackHandle) null);
      return securityContext;
    }

    internal static bool AlwaysFlowImpersonationPolicy
    {
      get
      {
        return SecurityContext._alwaysFlowImpersonationPolicy;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static WindowsIdentity GetCurrentWI(ExecutionContext.Reader threadEC)
    {
      return SecurityContext.GetCurrentWI(threadEC, SecurityContext._alwaysFlowImpersonationPolicy);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static WindowsIdentity GetCurrentWI(ExecutionContext.Reader threadEC, bool cachedAlwaysFlowImpersonationPolicy)
    {
      if (cachedAlwaysFlowImpersonationPolicy)
        return WindowsIdentity.GetCurrentInternal(TokenAccessLevels.MaximumAllowed, true);
      return threadEC.SecurityContext.WindowsIdentity;
    }

    [SecurityCritical]
    internal static void RestoreCurrentWI(ExecutionContext.Reader currentEC, ExecutionContext.Reader prevEC, WindowsIdentity targetWI, bool cachedAlwaysFlowImpersonationPolicy)
    {
      if (!cachedAlwaysFlowImpersonationPolicy && prevEC.SecurityContext.WindowsIdentity == targetWI)
        return;
      SecurityContext.RestoreCurrentWIInternal(targetWI);
    }

    [SecurityCritical]
    private static void RestoreCurrentWIInternal(WindowsIdentity targetWI)
    {
      int self = System.Security.Principal.Win32.RevertToSelf();
      if (self < 0)
        Environment.FailFast(Win32Native.GetMessage(self));
      if (targetWI == null)
        return;
      SafeAccessTokenHandle accessToken = targetWI.AccessToken;
      if (accessToken == null || accessToken.IsInvalid)
        return;
      int errorCode = System.Security.Principal.Win32.ImpersonateLoggedOnUser(accessToken);
      if (errorCode >= 0)
        return;
      Environment.FailFast(Win32Native.GetMessage(errorCode));
    }

    [SecurityCritical]
    internal bool IsDefaultFTSecurityContext()
    {
      if (this.WindowsIdentity != null)
        return false;
      if (this.CompressedStack != null)
        return this.CompressedStack.CompressedStackHandle == null;
      return true;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool CurrentlyInDefaultFTSecurityContext(ExecutionContext.Reader threadEC)
    {
      if (SecurityContext.IsDefaultThreadSecurityInfo())
        return SecurityContext.GetCurrentWI(threadEC) == null;
      return false;
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern WindowsImpersonationFlowMode GetImpersonationFlowMode();

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsDefaultThreadSecurityInfo();

    internal struct Reader
    {
      private SecurityContext m_sc;

      public Reader(SecurityContext sc)
      {
        this.m_sc = sc;
      }

      public SecurityContext DangerousGetRawSecurityContext()
      {
        return this.m_sc;
      }

      public bool IsNull
      {
        get
        {
          return this.m_sc == null;
        }
      }

      public bool IsSame(SecurityContext sc)
      {
        return this.m_sc == sc;
      }

      public bool IsSame(SecurityContext.Reader sc)
      {
        return this.m_sc == sc.m_sc;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public bool IsFlowSuppressed(SecurityContextDisableFlow flags)
      {
        if (this.m_sc != null)
          return (this.m_sc._disableFlow & flags) == flags;
        return false;
      }

      public CompressedStack CompressedStack
      {
        get
        {
          if (!this.IsNull)
            return this.m_sc.CompressedStack;
          return (CompressedStack) null;
        }
      }

      public WindowsIdentity WindowsIdentity
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          if (!this.IsNull)
            return this.m_sc.WindowsIdentity;
          return (WindowsIdentity) null;
        }
      }
    }

    internal class SecurityContextRunData
    {
      internal SecurityContext sc;
      internal ContextCallback callBack;
      internal object state;
      internal SecurityContextSwitcher scsw;

      internal SecurityContextRunData(SecurityContext securityContext, ContextCallback cb, object state)
      {
        this.sc = securityContext;
        this.callBack = cb;
        this.state = state;
        this.scsw = new SecurityContextSwitcher();
      }
    }
  }
}
